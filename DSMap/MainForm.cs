using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using DSHL;
using DSHL.Formats.Nitro;
using DSHL.Formats.Nitro.Models;
using DSHL.Formats.Pokémon;
using DSHL.Formats.Pokémon.Scripting;

using OpenTK.Graphics.OpenGL;
using ScintillaNET;

namespace DSMap
{
    public partial class MainForm : Form
    {
        // main
        private ROM rom = new ROM();
        private Ini ini = new Ini();
        private Color[] movementsPalette = null;

        // stuff we need
        private NARC mapData;
        private uint headerTable;
        private string[] headerNames;
        private NARC mapTextureData;
        private NARC encounterData;
        private PkmnText locationNames;
        private NARC scriptData;
        private NARC textData;

        private CommandDatabase scriptCommands = new CommandDatabase();
        private Decompiler scriptDecompiler = null;// = new Decompiler();

        // editing
        private int selectedMap = -1;
        private Map map = null;
        private Header header = null;
        private Dictionary<int, int> mapHeaders = new Dictionary<int, int>();
        private PkmnText mapTexts = null;
        private WildPokémon encounters = null;

        private int selectedObj = -1;

        private NSBTX mapTextures;
        private bool mapTexturesSet = false;
        private Dictionary<int, int> mapTextureAssoc = new Dictionary<int, int>();

        private bool mc = false;

        private ModelDisplaySettings mapModelSettings = new ModelDisplaySettings()
        {
            AngleX = -180f,
            AngleY = 0f,
            AngleZ = 45f,

            TranslateX = 0f,
            TranslateY = -1.5f,
            TranslateZ = 0f,

            Zoom = 0.2f
        };

        private struct ModelDisplaySettings
        {
            public float AngleX, AngleY, AngleZ;
            public float TranslateX, TranslateY, TranslateZ;
            public float Zoom;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Check if this exists
            if (!File.Exists("ndstool.exe"))
            {
                MessageBox.Show("Could not find ndstool.exe!\nPlease place a copy in this tool's directory before use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            // Load Games.ini
            if (!File.Exists("assets\\games.ini"))
            {
                MessageBox.Show("Could not find games.ini!\nPlease place a copy in the assets directory before use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            else
            {
                ini.Load("assets\\games.ini");
            }

            // Load movements palette
            if (File.Exists("assets\\movements.act"))
            {
                using (FileStream fs = File.OpenRead("assets\\movements.act"))
                {
                    movementsPalette = new Color[256];
                    for (int i = 0; i < 256; i++)
                    {
                        int r = fs.ReadByte();
                        int g = fs.ReadByte();
                        int b = fs.ReadByte();

                        movementsPalette[i] = Color.FromArgb(140, r, g, b);
                    }
                }
            }
            else
            {
                MessageBox.Show("Could not find movements.act!\nPlease place a copy in the assets directory before use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            // Finally, load behaviours
            // TODO: Other text (for language?)
            if (File.Exists("assets\\text.ini"))
            {
                Ini text = new Ini("assets\\text.ini");

                // Fill movement permissions
                cMovePermission.Items.Clear();
                for (int i = 0; i < 256; i++)
                {
                    string b = text["Behaviours", i.ToString()];
                    if (b == string.Empty) cMovePermission.Items.Add(i.ToString());
                    else cMovePermission.Items.Add(b);
                }
            }
            else
            {
                MessageBox.Show("Could not find text.ini!\nPlease place a copy in the assets directory before use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            // Create temporary data directory
            Temporary.Create();

            // GL
            glMapModel.Context.MakeCurrent(glMapModel.WindowInfo);
            GL.Viewport(0, 0, glMapModel.Width, glMapModel.Height);

            // Console stuff
            ConfigureScriptStyles();

            // Hide wild Pokémon editor
            tabControlWilds.Visible = false;
            lblNoWilds.Visible = false;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Clean up temporary file data
            Temporary.Dispose();
        }

        #region Menu

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openDialog.FileName = "";
            openDialog.Filter = "NDS ROMs|*.nds";
            openDialog.Title = "Load ROM";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                // Set the ROM
                rom.Load(openDialog.FileName);
                Stopwatch watch = Stopwatch.StartNew();

                // Do stuff with it
                pBanner.Image = rom.Banner.Image;
                lblROM.Text = "Name: " + rom.Header.Title;
                lblROM.Text += "\nCode: " + rom.Header.Code;

                LoadROMData();
                watch.Stop();

                MessageBox.Show("Done!\nYour ROM was loaded in " + watch.Elapsed.TotalSeconds + " s!");
            }
        }

        private void buildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Build
            // It will know whether we can or not
            rom.Build();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 
            if (!rom.IsLoaded() || selectedMap == -1) return;

            // Save data
            mapData.ReplaceFile(selectedMap, map.Save());
            if (encounters != null && header.WildPokemon < 0xFFFF)
            {
                encounterData.ReplaceFile(header.WildPokemon, encounters.Save());
            }
            header.Save(rom.GetFullFilePath("arm9.bin"), headerTable, mapHeaders[selectedMap]);

            // Save NARCs
            mapData.Save();
            encounterData.Save();
        }

        private void createPatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // A ROM doesn't need to be loaded for this
            // Get the base ROM
            openDialog.Title = "Select Original (Base) ROM";
            openDialog.Filter = "NDS ROMs|*.nds";
            openDialog.FileName = "";

            if (openDialog.ShowDialog() != DialogResult.OK) return;
            string original = openDialog.FileName;

            // Get the modified ROM
            openDialog.Title = "Select Modified (Hacked) ROM";
            openDialog.FileName = "";

            if (openDialog.ShowDialog() != DialogResult.OK) return;
            string modified = openDialog.FileName;

            // Get the output file
            saveDialog.Title = "Save Patch To";
            saveDialog.Filter = "Patch File|*.patch";
            saveDialog.FileName = "";

            if (saveDialog.ShowDialog() != DialogResult.OK) return;
            string patch = saveDialog.FileName;

            try
            {
                Patching.uCreatePatch(original, modified, patch);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Patch creation failed!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Patch created successfully!", "Yay~", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void applyPatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get the patch
            openDialog.FileName = "";
            openDialog.Filter = "Patch Files|*.patch";
            openDialog.Title = "Select Patch To Apply";

            if (openDialog.ShowDialog() != DialogResult.OK) return;
            string patch = openDialog.FileName;

            // Get the ROM
            openDialog.Title = "Select ROM To Patch";
            openDialog.Filter = "NDS ROMs|*.nds";
            openDialog.FileName = "";

            if (openDialog.ShowDialog() != DialogResult.OK) return;
            string mod = openDialog.FileName;

            try
            {
                Patching.ApplyPatch(patch, mod);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Patch application failed!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Patch applied successfully!", "Yay~", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        private void LoadROMData()
        {
            if (!rom.IsLoaded()) return;

            mc = true;
            try
            {
                // Load the map and matrix NARCs
                NARC matrixData = new NARC(GetROMFilePathFromIni("MatrixData"));
                mapData = new NARC(GetROMFilePathFromIni("MapData"));

                // Load the map texture NARC
                mapTextureData = new NARC(GetROMFilePathFromIni("MapTextureData"));

                // Load the encounter NARC
                encounterData = new NARC(GetROMFilePathFromIni("EncounterData"));

                // Load the map names
                string[] mapModelNames = Map.LoadMapNames(mapData);

                // Load the header names
                headerNames = Header.LoadHeaderNames(GetROMFilePathFromIni("HeaderNames"));

                // Match the map headers to the maps
                //headerTable = Convert.ToUInt32(ini[rom.Header.Code, "HeaderTable"], 16);
                headerTable = ini.GetUInt32(rom.Header.Code, "HeaderTable", 16);
                Dictionary<int, int> headerMatrixMatches = Header.LoadHeaderMatrixMatches(rom.GetFullFilePath("arm9.bin"), headerTable, headerNames.Length);
                Dictionary<int, List<int>> headerMapMatches = Matrix.LoadHeaderMapMatches(matrixData, headerMatrixMatches);

                // Load scipt data
                scriptData = new NARC(GetROMFilePathFromIni("ScriptData"));
                scriptCommands.Load("assets\\" + ini[rom.Header.Code, "ScriptCommands"]);

                scriptDecompiler = new Decompiler(scriptCommands);
                //txtMovements.SetKeywords(0, scriptCommands.GetAllCommandNames());
                txtScripts.SetKeywords(0, scriptCommands.GetAllCommandNames());
                txtFunctions.SetKeywords(0, scriptCommands.GetAllCommandNames());

                // Load text that we need
                #region Text
                textData = new NARC(GetROMFilePathFromIni("MessageData"));
                int mapNamesFileID = ini.GetInt32(rom.Header.Code, "Text~MapNames");
                int pkmnNamesFileID = ini.GetInt32(rom.Header.Code, "Text~PokemonNames");

                locationNames = new PkmnText(textData.GetFileMemoryStream(mapNamesFileID), true);
                PkmnText pokemonNames = new PkmnText(textData.GetFileMemoryStream(pkmnNamesFileID), true);

                // Add the pokemon names to every combobox that needs it
                cWildsWalking0.Items.Clear();
                cWildsWalking1.Items.Clear();
                cWildsWalking2.Items.Clear();
                cWildsWalking3.Items.Clear();
                cWildsWalking4.Items.Clear();
                cWildsWalking5.Items.Clear();
                cWildsWalking6.Items.Clear();
                cWildsWalking7.Items.Clear();
                cWildsWalking8.Items.Clear();
                cWildsWalking9.Items.Clear();
                cWildsWalking10.Items.Clear();
                cWildsWalking11.Items.Clear();
                cWildsRuby0.Items.Clear();
                cWildsRuby1.Items.Clear();
                cWildsSapp0.Items.Clear();
                cWildsSapp1.Items.Clear();
                cWildsEm0.Items.Clear();
                cWildsEm1.Items.Clear();
                cWildsFire0.Items.Clear();
                cWildsFire1.Items.Clear();
                cWildsLeaf0.Items.Clear();
                cWildsLeaf1.Items.Clear();
                cWildsRadar0.Items.Clear();
                cWildsRadar1.Items.Clear();
                cWildsRadar2.Items.Clear();
                cWildsRadar3.Items.Clear();
                cWildsMorn0.Items.Clear();
                cWildsMorn1.Items.Clear();
                cWildsDay0.Items.Clear();
                cWildsDay1.Items.Clear();
                cWildsNight0.Items.Clear();
                cWildsNight1.Items.Clear();
                cWildsSurfing0.Items.Clear();
                cWildsSurfing1.Items.Clear();
                cWildsSurfing2.Items.Clear();
                cWildsSurfing3.Items.Clear();
                cWildsSurfing4.Items.Clear();
                cWildsOR0.Items.Clear();
                cWildsOR1.Items.Clear();
                cWildsOR2.Items.Clear();
                cWildsOR3.Items.Clear();
                cWildsOR4.Items.Clear();
                cWildsGR0.Items.Clear();
                cWildsGR1.Items.Clear();
                cWildsGR2.Items.Clear();
                cWildsGR3.Items.Clear();
                cWildsGR4.Items.Clear();
                cWildsSR0.Items.Clear();
                cWildsSR1.Items.Clear();
                cWildsSR2.Items.Clear();
                cWildsSR3.Items.Clear();
                cWildsSR4.Items.Clear();

                for (int i = 0; i < pokemonNames.Count; i++)
                {
                    cWildsWalking0.Items.Add(pokemonNames[i]);
                    cWildsWalking1.Items.Add(pokemonNames[i]);
                    cWildsWalking2.Items.Add(pokemonNames[i]);
                    cWildsWalking3.Items.Add(pokemonNames[i]);
                    cWildsWalking4.Items.Add(pokemonNames[i]);
                    cWildsWalking5.Items.Add(pokemonNames[i]);
                    cWildsWalking6.Items.Add(pokemonNames[i]);
                    cWildsWalking7.Items.Add(pokemonNames[i]);
                    cWildsWalking8.Items.Add(pokemonNames[i]);
                    cWildsWalking9.Items.Add(pokemonNames[i]);
                    cWildsWalking10.Items.Add(pokemonNames[i]);
                    cWildsWalking11.Items.Add(pokemonNames[i]);

                    cWildsRuby0.Items.Add(pokemonNames[i]);
                    cWildsRuby1.Items.Add(pokemonNames[i]);
                    cWildsSapp0.Items.Add(pokemonNames[i]);
                    cWildsSapp1.Items.Add(pokemonNames[i]);
                    cWildsEm0.Items.Add(pokemonNames[i]);
                    cWildsEm1.Items.Add(pokemonNames[i]);
                    cWildsFire0.Items.Add(pokemonNames[i]);
                    cWildsFire1.Items.Add(pokemonNames[i]);
                    cWildsLeaf0.Items.Add(pokemonNames[i]);
                    cWildsLeaf1.Items.Add(pokemonNames[i]);

                    cWildsRadar0.Items.Add(pokemonNames[i]);
                    cWildsRadar1.Items.Add(pokemonNames[i]);
                    cWildsRadar2.Items.Add(pokemonNames[i]);
                    cWildsRadar3.Items.Add(pokemonNames[i]);
                    cWildsMorn0.Items.Add(pokemonNames[i]);
                    cWildsMorn1.Items.Add(pokemonNames[i]);
                    cWildsDay0.Items.Add(pokemonNames[i]);
                    cWildsDay1.Items.Add(pokemonNames[i]);
                    cWildsNight0.Items.Add(pokemonNames[i]);
                    cWildsNight1.Items.Add(pokemonNames[i]);

                    cWildsSurfing0.Items.Add(pokemonNames[i]);
                    cWildsSurfing1.Items.Add(pokemonNames[i]);
                    cWildsSurfing2.Items.Add(pokemonNames[i]);
                    cWildsSurfing3.Items.Add(pokemonNames[i]);
                    cWildsSurfing4.Items.Add(pokemonNames[i]);

                    cWildsOR0.Items.Add(pokemonNames[i]);
                    cWildsOR1.Items.Add(pokemonNames[i]);
                    cWildsOR2.Items.Add(pokemonNames[i]);
                    cWildsOR3.Items.Add(pokemonNames[i]);
                    cWildsOR4.Items.Add(pokemonNames[i]);

                    cWildsGR0.Items.Add(pokemonNames[i]);
                    cWildsGR1.Items.Add(pokemonNames[i]);
                    cWildsGR2.Items.Add(pokemonNames[i]);
                    cWildsGR3.Items.Add(pokemonNames[i]);
                    cWildsGR4.Items.Add(pokemonNames[i]);

                    cWildsSR0.Items.Add(pokemonNames[i]);
                    cWildsSR1.Items.Add(pokemonNames[i]);
                    cWildsSR2.Items.Add(pokemonNames[i]);
                    cWildsSR3.Items.Add(pokemonNames[i]);
                    cWildsSR4.Items.Add(pokemonNames[i]);
                }

                cHeaderName.Items.Clear();
                for (int i = 0; i < locationNames.Count; i++)
                {
                    cHeaderName.Items.Add(locationNames[i]);
                }
                #endregion

                // Fill the treeview with headers and associated maps
                treeMaps.Nodes.Clear(); mapHeaders.Clear();
                for (int header = 0; header < headerNames.Length; header++)
                {
                    TreeNode node = new TreeNode(headerNames[header]);
                    node.Tag = -1; // All headers will be -1
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;

                    int[] maps = headerMapMatches[header].ToArray();
                    foreach (int map in maps)
                    {
                        TreeNode jr = new TreeNode(mapModelNames[map]);
                        jr.Tag = map; // For easy loading
                        jr.ImageIndex = 1;
                        jr.SelectedImageIndex = 1;

                        node.Nodes.Add(jr);
                        mapHeaders[map] = header;
                    }

                    treeMaps.Nodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace);
            }
            mc = false;
        }

        private string GetROMFilePathFromIni(string iniSection)
        {
            if (rom.IsLoaded())
                return rom.GetFullFilePath(ini[rom.Header.Code, iniSection]);
            else
                return string.Empty;
        }

        private void treeMaps_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Make sure the ROM has been loaded
            if (!rom.IsLoaded() || treeMaps.SelectedNode == null) return;

            // Get current selection
            TreeNode selectedNode = treeMaps.SelectedNode;
            int tag = (int)selectedNode.Tag;
            if (tag == -1) return;

            // Load and display the map
            try
            {
                selectedMap = tag;
                LoadAll();
                DisplayAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace);
            }
        }

        private void LoadAll()
        {
            // Load the map
            using (MemoryStream ms = mapData.GetFileMemoryStream(selectedMap))
            {
                map = new Map(ms);
            }

            // Load the header;
            header = new Header(rom.GetFullFilePath("arm9.bin"), headerTable, mapHeaders[selectedMap]);
            
            // Load map textures
            using (MemoryStream ms = mapTextureData.GetFileMemoryStream(header.MapTextures))
            {
                mapTextures = NSBTXLoader.LoadBTX0(ms);
                mapTexturesSet = true; // This will allow textures to be loaded
            }

            // Load scripts
            if (header.Scripts < 0xFFFF)
            {
                File.WriteAllBytes("script.bin", scriptData.GetFile(header.Scripts));
                scriptDecompiler.Decompile(scriptData.GetFile(header.Scripts));
            }
            else
            {
                // todo
            }

            // Load text
            if (header.Texts < 0xFFFF)
            {
                mapTexts = new PkmnText(textData.GetFileMemoryStream(header.Texts));
            }
            else
            {
                mapTexts = null;
            }

            // Load encounters
            if (header.WildPokemon < 0xFFFF)
            {
                using (MemoryStream ms = encounterData.GetFileMemoryStream(header.WildPokemon))
                {
                    encounters = new WildPokémon(ms);
                }
            }
            else
            {
                encounters = null;
            }
        }

        private void DisplayAll()
        {
            mc = true;
            this.Text = "DS Map ~ " + headerNames[mapHeaders[selectedMap]] + " > " + locationNames[header.Name] + " > " + map.Name;

            #region Map
            // Display
            LoadAllMapTextures();
            glMapModel.Invalidate();
            txtMapModelName.Text = map.Model.Name;

            // Movements
            pMovements.Invalidate();
            cMovePermission.SelectedIndex = 0;
            pObjMap.Invalidate();

            // Objects
            listObjects.Items.Clear();
            foreach (var obj in map.Objects)
            {
                var item = new ListViewItem(obj.Number.ToString());
                item.SubItems.Add("???");
                listObjects.Items.Add(item);
            }
            selectedObj = -1;
            #endregion

            #region Scripts

            if (header.Scripts < 0xFFFF)
            {
                tabControlScripts.Visible = true;
                txtScripts.Text = scriptDecompiler.ScriptsToString();
                txtFunctions.Text = scriptDecompiler.FunctionsToString();
                txtMovements.Text = scriptDecompiler.MovementsToString();
            }
            else
            {
                tabControlScripts.Visible = false;
            }

            #endregion

            #region Text

            string ttt = "";
            for (int i = 0; i < mapTexts.Count; i++)
            {
                ttt += "text" + i + "=" + mapTexts[i] + "\n";
            }
            txtText.Text = ttt;

            #endregion

            #region Wild Pokemon
            if (header.WildPokemon == 0xFFFF)
            {
                lblNoWilds.Visible = true;
                tabControlWilds.Visible = false;
            }
            else
            {
                lblNoWilds.Visible = false;
                tabControlWilds.Visible = true;

                txtWildsWalkingRate.Value = encounters.WalkingRate;
                cWildsWalking0.SelectedIndex = encounters.WalkingSpecies[0];
                cWildsWalking1.SelectedIndex = encounters.WalkingSpecies[1];
                cWildsWalking2.SelectedIndex = encounters.WalkingSpecies[2];
                cWildsWalking3.SelectedIndex = encounters.WalkingSpecies[3];
                cWildsWalking4.SelectedIndex = encounters.WalkingSpecies[4];
                cWildsWalking5.SelectedIndex = encounters.WalkingSpecies[5];
                cWildsWalking6.SelectedIndex = encounters.WalkingSpecies[6];
                cWildsWalking7.SelectedIndex = encounters.WalkingSpecies[7];
                cWildsWalking8.SelectedIndex = encounters.WalkingSpecies[8];
                cWildsWalking9.SelectedIndex = encounters.WalkingSpecies[9];
                cWildsWalking10.SelectedIndex = encounters.WalkingSpecies[10];
                cWildsWalking11.SelectedIndex = encounters.WalkingSpecies[11];
                txtWildsWalking0.Value = encounters.WalkingLevels[0];
                txtWildsWalking1.Value = encounters.WalkingLevels[1];
                txtWildsWalking2.Value = encounters.WalkingLevels[2];
                txtWildsWalking3.Value = encounters.WalkingLevels[3];
                txtWildsWalking4.Value = encounters.WalkingLevels[4];
                txtWildsWalking5.Value = encounters.WalkingLevels[5];
                txtWildsWalking6.Value = encounters.WalkingLevels[6];
                txtWildsWalking7.Value = encounters.WalkingLevels[7];
                txtWildsWalking8.Value = encounters.WalkingLevels[8];
                txtWildsWalking9.Value = encounters.WalkingLevels[9];
                txtWildsWalking10.Value = encounters.WalkingLevels[10];
                txtWildsWalking11.Value = encounters.WalkingLevels[11];

                cWildsMorn0.SelectedIndex = encounters.Morning[0];
                cWildsMorn1.SelectedIndex = encounters.Morning[1];
                cWildsDay0.SelectedIndex = encounters.Day[0];
                cWildsDay1.SelectedIndex = encounters.Day[1];
                cWildsNight0.SelectedIndex = encounters.Night[0];
                cWildsNight1.SelectedIndex = encounters.Night[1];

                cWildsRuby0.SelectedIndex = encounters.Ruby[0];
                cWildsRuby1.SelectedIndex = encounters.Ruby[1];
                cWildsSapp0.SelectedIndex = encounters.Sapphire[0];
                cWildsSapp1.SelectedIndex = encounters.Sapphire[1];
                cWildsFire0.SelectedIndex = encounters.FireRed[0];
                cWildsFire1.SelectedIndex = encounters.FireRed[1];
                cWildsLeaf0.SelectedIndex = encounters.LeafGreen[0];
                cWildsLeaf1.SelectedIndex = encounters.LeafGreen[1];
                cWildsEm0.SelectedIndex = encounters.Emerald[0];
                cWildsEm1.SelectedIndex = encounters.Emerald[1];

                cWildsRadar0.SelectedIndex = encounters.Radar[0];
                cWildsRadar1.SelectedIndex = encounters.Radar[1];
                cWildsRadar2.SelectedIndex = encounters.Radar[0];
                cWildsRadar3.SelectedIndex = encounters.Radar[1];

                txtWildsSurfingRate.Value = encounters.SurfingRate;
                cWildsSurfing0.SelectedIndex = encounters.SurfingSpecies[0];
                cWildsSurfing1.SelectedIndex = encounters.SurfingSpecies[1];
                cWildsSurfing2.SelectedIndex = encounters.SurfingSpecies[2];
                cWildsSurfing3.SelectedIndex = encounters.SurfingSpecies[3];
                cWildsSurfing4.SelectedIndex = encounters.SurfingSpecies[4];
                txtWildsSurfingMin0.Value = encounters.SurfingMinLevels[0];
                txtWildsSurfingMin1.Value = encounters.SurfingMinLevels[1];
                txtWildsSurfingMin2.Value = encounters.SurfingMinLevels[2];
                txtWildsSurfingMin3.Value = encounters.SurfingMinLevels[3];
                txtWildsSurfingMin4.Value = encounters.SurfingMinLevels[4];
                txtWildsSurfingMax0.Value = encounters.SurfingMaxLevels[0];
                txtWildsSurfingMax1.Value = encounters.SurfingMaxLevels[1];
                txtWildsSurfingMax2.Value = encounters.SurfingMaxLevels[2];
                txtWildsSurfingMax3.Value = encounters.SurfingMaxLevels[3];
                txtWildsSurfingMax4.Value = encounters.SurfingMaxLevels[4];

                txtWildsORRate.Value = encounters.OldRodRate;
                cWildsOR0.SelectedIndex = encounters.OldRodSpecies[0];
                cWildsOR1.SelectedIndex = encounters.OldRodSpecies[1];
                cWildsOR2.SelectedIndex = encounters.OldRodSpecies[2];
                cWildsOR3.SelectedIndex = encounters.OldRodSpecies[3];
                cWildsOR4.SelectedIndex = encounters.OldRodSpecies[4];
                txtWildsORMin0.Value = encounters.OldRodMinLevels[0];
                txtWildsORMin1.Value = encounters.OldRodMinLevels[1];
                txtWildsORMin2.Value = encounters.OldRodMinLevels[2];
                txtWildsORMin3.Value = encounters.OldRodMinLevels[3];
                txtWildsORMin4.Value = encounters.OldRodMinLevels[4];
                txtWildsORMax0.Value = encounters.OldRodMaxLevels[0];
                txtWildsORMax1.Value = encounters.OldRodMaxLevels[1];
                txtWildsORMax2.Value = encounters.OldRodMaxLevels[2];
                txtWildsORMax3.Value = encounters.OldRodMaxLevels[3];
                txtWildsORMax4.Value = encounters.OldRodMaxLevels[4];

                txtWildsGRRate.Value = encounters.GoodRodRate;
                cWildsGR0.SelectedIndex = encounters.GoodRodSpecies[0];
                cWildsGR1.SelectedIndex = encounters.GoodRodSpecies[1];
                cWildsGR2.SelectedIndex = encounters.GoodRodSpecies[2];
                cWildsGR3.SelectedIndex = encounters.GoodRodSpecies[3];
                cWildsGR4.SelectedIndex = encounters.GoodRodSpecies[4];
                txtWildsGRMin0.Value = encounters.GoodRodMinLevels[0];
                txtWildsGRMin1.Value = encounters.GoodRodMinLevels[1];
                txtWildsGRMin2.Value = encounters.GoodRodMinLevels[2];
                txtWildsGRMin3.Value = encounters.GoodRodMinLevels[3];
                txtWildsGRMin4.Value = encounters.GoodRodMinLevels[4];
                txtWildsGRMax0.Value = encounters.GoodRodMaxLevels[0];
                txtWildsGRMax1.Value = encounters.GoodRodMaxLevels[1];
                txtWildsGRMax2.Value = encounters.GoodRodMaxLevels[2];
                txtWildsGRMax3.Value = encounters.GoodRodMaxLevels[3];
                txtWildsGRMax4.Value = encounters.GoodRodMaxLevels[4];

                txtWildsSRRate.Value = encounters.SuperRodRate;
                cWildsSR0.SelectedIndex = encounters.SuperRodSpecies[0];
                cWildsSR1.SelectedIndex = encounters.SuperRodSpecies[1];
                cWildsSR2.SelectedIndex = encounters.SuperRodSpecies[2];
                cWildsSR3.SelectedIndex = encounters.SuperRodSpecies[3];
                cWildsSR4.SelectedIndex = encounters.SuperRodSpecies[4];
                txtWildsSRMin0.Value = encounters.SuperRodMinLevels[0];
                txtWildsSRMin1.Value = encounters.SuperRodMinLevels[1];
                txtWildsSRMin2.Value = encounters.SuperRodMinLevels[2];
                txtWildsSRMin3.Value = encounters.SuperRodMinLevels[3];
                txtWildsSRMin4.Value = encounters.SuperRodMinLevels[4];
                txtWildsSRMax0.Value = encounters.SuperRodMaxLevels[0];
                txtWildsSRMax1.Value = encounters.SuperRodMaxLevels[1];
                txtWildsSRMax2.Value = encounters.SuperRodMaxLevels[2];
                txtWildsSRMax3.Value = encounters.SuperRodMaxLevels[3];
                txtWildsSRMax4.Value = encounters.SuperRodMaxLevels[4];
            }
            #endregion

            #region Header
            txtHeaderMapTextures.Value = header.MapTextures;
            txtHeaderObjectTextures.Value = header.ObjectTexutres;

            cHeaderName.SelectedIndex = header.Name;
            txtHeaderName.Text = locationNames[header.Name];
            txtHeaderNameStyle.Value = header.NameStyle;
            txtHeaderNameFrame.Value = header.NameFrame;
            txtHeaderMusicDay.Value = header.MusicDay;
            txtHeaderMusicNight.Value = header.MusicNight;
            txtHeaderCamera.Value = header.Camera;
            txtHeaderWeather.Value = header.Weather;
            txtHeaderLvlScripts.Value = header.LevelScripts;
            txtHeaderFlags.Value = header.Flags;
            txtHeaderEvents.Value = header.Events;
            txtHeaderScripts.Value = header.Scripts;
            txtHeaderText.Value = header.Texts;
            txtHeaderMatrix.Value = header.Matrix;
            txtHeaderWildPokemon.Value = header.WildPokemon;
            #endregion

            mc = false;
        }

        #region Map

        #region Movements

        private void pMovements_Paint(object sender, PaintEventArgs e)
        {
            if (map == null) return;

            Font f = new Font("Arial", 8.5f);
            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 32; y++)
                {
                    Rectangle dest = new Rectangle(x * 16, y * 16, 16, 16);

                    // flags
                    byte value = map.Movements[x, y].Flag;

                    if (value == 0x80)
                        e.Graphics.FillRectangle(Brushes.Red, dest);
                    else
                        e.Graphics.FillRectangle(Brushes.Green, dest);

                    // permissions
                    if (rMoveBehaviours.Checked)
                    {
                        value = map.Movements[x, y].Behaviour;
                        e.Graphics.FillRectangle(new SolidBrush(movementsPalette[value]), dest);

                        if (value != 0) e.Graphics.DrawString(value.ToString("X2"), f,
                             Brushes.Black, (x * 16) + 0, (y * 16) + 2);
                    }
                }
            }
            f.Dispose();
        }

        private void rMove_CheckChanged(object sender, EventArgs e)
        {
            pMovements.Invalidate();
            if (rMoveBehaviours.Checked)
            {
                cMovePermission.Visible = true;
                label3.Visible = false;
            }
            else
            {
                cMovePermission.Visible = false;
                label3.Visible = true;
            }
        }

        private void pMovements_Clicked(MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;
            //lblXY.Text = string.Format("X: {0}, Y: {1}", x, y);

            if (map == null) return;

            if (e.Button == MouseButtons.Left)
            {
                if (rMoveFlags.Checked)
                {
                    map.Movements[x, y].Flag = 0;
                    pMovements.Invalidate();
                }
                else
                {
                    map.Movements[x, y].Behaviour = (byte)cMovePermission.SelectedIndex;
                    pMovements.Invalidate();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (rMoveFlags.Checked)
                {
                    map.Movements[x, y].Flag = 0x80;
                    pMovements.Invalidate();
                }
                else
                {
                    cMovePermission.SelectedIndex = map.Movements[x, y].Behaviour;
                }
            }
        }

        private void pMovements_MouseDown(object sender, MouseEventArgs e)
        {
            pMovements_Clicked(e);
        }

        private void pMovements_MouseMove(object sender, MouseEventArgs e)
        {
            pMovements_Clicked(e);
        }

        private void bMoveColors_Click(object sender, EventArgs e)
        {
            // Enable editing of the palette
            PaletteEditDialog ped = new PaletteEditDialog(ref movementsPalette);
            ped.Text = "Edit Behaviour Colors";
            ped.ShowDialog();

            // Show the changes
            pMovements.Invalidate();

            // Save the palette
            using (FileStream fs = File.OpenWrite("assets\\movements.act"))
            {
                for (int i = 0; i < 256; i++)
                {
                    fs.WriteByte(movementsPalette[i].R);
                    fs.WriteByte(movementsPalette[i].G);
                    fs.WriteByte(movementsPalette[i].B);
                }
            }
        }

        #endregion

        #region Objects

        private void pObjMap_Paint(object sender, PaintEventArgs e)
        {
            if (map == null) return;

            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 32; y++)
                {
                    Rectangle dest = new Rectangle(x * 8, y * 8, 8, 8);
                    byte value = map.Movements[x, y].Flag;

                    if (value == 0x80)
                        e.Graphics.FillRectangle(Brushes.Red, dest);
                    else
                        e.Graphics.FillRectangle(Brushes.Green, dest);
                }
            }

            if (selectedObj > -1)
            {
                e.Graphics.FillRectangle(Brushes.Purple,
                        (float)(txtObjX.Value - 1) * 8,
                        (float)(txtObjZ.Value - 1) * 8, 8, 8);
            }
        }

        private void listObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (map == null) return;

            int index = -1;
            foreach (int i in listObjects.SelectedIndices) index = i;
            if (index == -1) return;

            selectedObj = index;
            var obj = map.Objects[selectedObj];

            mc = true;
            txtObjModel.Value = (uint)obj.Number;
            
            txtObjX.Value = obj.X;
            txtObjY.Value = obj.Y;
            txtObjZ.Value = obj.Z;

            txtObjXFlag.Value = obj.XFlag;
            txtObjYFlag.Value = obj.YFlag;
            txtObjZFlag.Value = obj.ZFlag;

            txtObjWidth.Value = (uint)obj.Width;
            txtObjLength.Value = (uint)obj.Length;
            txtObjHeight.Value = (uint)obj.Height;

            pObjMap.Invalidate();
            mc = false;
        }

        private void txtObjModel_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedObj > -1)
            {
                listObjects.Items[selectedObj].Text = txtObjModel.Value.ToString();
                map.Objects[selectedObj].Number = (int)txtObjModel.Value;
            }
        }

        private void txtObjXYZ_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedObj > -1)
            {
                map.Objects[selectedObj].X = (short)txtObjX.Value;
                map.Objects[selectedObj].Y = (short)txtObjY.Value;
                map.Objects[selectedObj].Z = (short)txtObjZ.Value;

                pObjMap.Invalidate();
            }
        }

        private void txtObjXYZFlags_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedObj > -1)
            {
                map.Objects[selectedObj].XFlag = (ushort)txtObjXFlag.Value;
                map.Objects[selectedObj].YFlag = (ushort)txtObjYFlag.Value;
                map.Objects[selectedObj].ZFlag = (ushort)txtObjZFlag.Value;
            }
        }

        private void txtObjWHL_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedObj > -1)
            {
                map.Objects[selectedObj].Width = (int)txtObjWidth.Value;
                map.Objects[selectedObj].Height = (int)txtObjHeight.Value;
                map.Objects[selectedObj].Length = (int)txtObjLength.Value;
            }
        }

        #endregion

        #region Map Model

        #region Rendering

        private void glMapModel_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glMapModel.Width, glMapModel.Height);
        }

        private void glMapModel_Paint(object sender, PaintEventArgs e)
        {
            GL.ClearColor(0.1f, 0.1f, 0.1f, 1f); // That cool looking gray
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.PushMatrix();    // For translation and scale

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            // Rotate, translate, zoom
            GL.Rotate(mapModelSettings.AngleX, 0f, 1f, 0f);
            GL.Rotate(mapModelSettings.AngleY, 0f, 0f, 1f);
            GL.Rotate(mapModelSettings.AngleZ, 1f, 0f, 0f);
            GL.Scale(-mapModelSettings.Zoom, mapModelSettings.Zoom, mapModelSettings.Zoom);
            GL.Translate(mapModelSettings.TranslateX, mapModelSettings.TranslateY, mapModelSettings.TranslateZ);

            //GL.Scale(-mapModelSettings.Zoom, mapModelSettings.Zoom, mapModelSettings.Zoom);

            GL.Disable(EnableCap.Texture2D);

            if (map == null)
            {
                glMapModel.SwapBuffers();
                return;
            }

            if (!mapTexturesSet)
            {
                glMapModel.SwapBuffers();
                return;
            }

            GL.Enable(EnableCap.PolygonSmooth);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.AlphaTest);
            //GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            
            GL.Enable(EnableCap.Blend);
            GL.AlphaFunc(AlphaFunction.Greater, 0f);
            GL.Disable(EnableCap.CullFace);
            //pm = PolygonMode.Line;
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float)TextureWrapMode.Repeat);
            //GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)TextureEnvMode.Replace);
            GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)TextureEnvMode.Replace);

            foreach (var poly in map.Model.Polygons)
            {
                // Get the material
                var mat = map.Model.Materials[poly.MaterialID];
                if (!mat.MatchedTex || !mat.MatchedPal) continue;

                // Get the texture and palette
                var tex = mapTextures.GetTexture(mat.Texture.Name); // so we can scale things
                //var pal = mapTextures.GetPalette(mat.Palette.Name);

                if (mapTextureAssoc.ContainsKey(poly.MaterialID))
                {
                    GL.BindTexture(TextureTarget.Texture2D, mapTextureAssoc[poly.MaterialID]);
                }
                else
                {
                    GL.BindTexture(TextureTarget.Texture2D, 0);
                }

                GL.MatrixMode(MatrixMode.Texture);
                GL.LoadIdentity();

                GL.Scale(1.0f / (float)tex.Width, 1.0f / (float)tex.Height, 1.0f); // Scale the texture to fill the polygon
                //BMD0Loader.GeometryCommands(poly.commands);
                DoGeometryCommands(poly);
            }

            GL.PopMatrix();

            GL.Flush();
            glMapModel.SwapBuffers();
        }

        private void LoadAllMapTextures()
        {
            // Prevent if this don't work out
            if (!mapTexturesSet || map == null)
            {
                return;
            }

            // Delete existing textures
            if (mapTextureAssoc.Count > 0)
            {
                foreach (int texID in mapTextureAssoc.Keys)
                {
                    GL.DeleteTexture(mapTextureAssoc[texID]);
                }
            }

            // Load new textures
            mapTextureAssoc.Clear();
            for (int i = 0; i < map.Model.Materials.Length; i++)
            {
                var mat = map.Model.Materials[i];
                if (mat.MatchedPal && mat.MatchedTex)
                {
                    var tex = mapTextures.GetTexture(mat.Texture.Name);
                    var pal = mapTextures.GetPalette(mat.Palette.Name);
                    mapTextureAssoc.Add(i, LoadTexture(tex, pal));
                }
            }
        }

        private int LoadTexture(NSBTX.Texture tex, NSBTX.Palette pal)
        {
            int texID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texID);

            Bitmap bmp = NSBTXDrawer.DrawTexture(tex, pal);
            var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmpData.Width, bmpData.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmpData.Scan0);

            bmp.UnlockBits(bmpData);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Nearest);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float)TextureWrapMode.Repeat);

            return texID;
        }

        public static void DoGeometryCommands(Model.Polygon poly)
        {
            OpenTK.Vector3 vector = new OpenTK.Vector3();

            foreach (var cmd in poly.Commands)
            {

                switch ((GeometryCmd)cmd.Value)
                {
                    case GeometryCmd.NOP:
                        break;
                    case GeometryCmd.MTX_MODE:
                        break;
                    case GeometryCmd.MTX_PUSH:
                        break;
                    case GeometryCmd.MTX_POP:
                        break;
                    case GeometryCmd.MTX_STORE:
                        break;
                    case GeometryCmd.MTX_RESTORE:
                        break;
                    case GeometryCmd.MTX_IDENTITY:
                        break;
                    case GeometryCmd.MTX_LOAD_4x4:
                        break;
                    case GeometryCmd.MTX_LOAD_4x3:
                        break;
                    case GeometryCmd.MTX_MULT_4x4:
                        break;
                    case GeometryCmd.MTX_MULT_4x3:
                        break;
                    case GeometryCmd.MTX_MULT_3x3:
                        break;
                    case GeometryCmd.MTX_SCALE:
                        break;
                    case GeometryCmd.MTX_TRANS:
                        break;

                    #region Vertex commands
                    // Multiply by the clipmatrix
                    case GeometryCmd.VTX_16:
                        vector.X = NSBMDLoader.GetDouble((int)(cmd.Parameters[0] & 0xFFFF), true, 3, 12);
                        vector.Y = NSBMDLoader.GetDouble((int)(cmd.Parameters[0] >> 16), true, 3, 12);
                        vector.Z = NSBMDLoader.GetDouble((int)(cmd.Parameters[1] & 0xFFFF), true, 3, 12);

                        GL.Vertex3(vector);

                        break;

                    case GeometryCmd.VTX_10:
                        vector.X = NSBMDLoader.GetDouble((int)(cmd.Parameters[0] & 0x3FF), true, 3, 6);
                        vector.Y = NSBMDLoader.GetDouble((int)((cmd.Parameters[0] >> 10) & 0x3FF), true, 3, 6);
                        vector.Z = NSBMDLoader.GetDouble((int)(cmd.Parameters[0] >> 20), true, 3, 6);

                        GL.Vertex3(vector);
                        break;

                    case GeometryCmd.VTX_XY:
                        vector.X = NSBMDLoader.GetDouble((int)(cmd.Parameters[0] & 0xFFFF), true, 3, 12);
                        vector.Y = NSBMDLoader.GetDouble((int)(cmd.Parameters[0] >> 16), true, 3, 12);

                        GL.Vertex3(vector);

                        break;

                    case GeometryCmd.VTX_XZ:
                        vector.X = NSBMDLoader.GetDouble((int)(cmd.Parameters[0] & 0xFFFF), true, 3, 12);
                        vector.Z = NSBMDLoader.GetDouble((int)(cmd.Parameters[0] >> 16), true, 3, 12);

                        GL.Vertex3(vector);

                        break;

                    case GeometryCmd.VTX_YZ:
                        vector.Y = NSBMDLoader.GetDouble((int)(cmd.Parameters[0] & 0xFFFF), true, 3, 12);
                        vector.Z = NSBMDLoader.GetDouble((int)(cmd.Parameters[0] >> 16), true, 3, 12);

                        GL.Vertex3(vector);

                        break;

                    case GeometryCmd.VTX_DIFF:
                        float diffX, diffY, diffZ;

                        diffX = NSBMDLoader.GetDouble((int)(cmd.Parameters[0] & 0x3FF), true, 0, 9);
                        diffY = NSBMDLoader.GetDouble((int)((cmd.Parameters[0] >> 10) & 0x3FFF), true, 0, 9);
                        diffZ = NSBMDLoader.GetDouble((int)(cmd.Parameters[0] >> 20), true, 0, 9);

                        vector.X += (diffX / 8);
                        vector.Y += (diffY / 8);
                        vector.Z += (diffZ / 8);

                        GL.Vertex3(vector);
                        break;
                    #endregion

                    case GeometryCmd.COLOR:
                        // Convert the param to RGB555 color
                        int r = (int)(cmd.Parameters[0] & 0x1F);
                        int g = (int)((cmd.Parameters[0] >> 5) & 0x1F);
                        int b = (int)((cmd.Parameters[0] >> 10) & 0x1F);

                        GL.Color3((float)r / 31.0f, (float)g / 31.0f, (float)b / 31.0f);
                        break;
                    case GeometryCmd.POLYGON_ATTR:
                        break;

                    #region Texture attributes
                    case GeometryCmd.TEXCOORD:
                        double s, t;
                        s = NSBMDLoader.GetDouble((int)(cmd.Parameters[0] & 0xFFFF), true, 11, 4);
                        t = NSBMDLoader.GetDouble((int)(cmd.Parameters[0] >> 16), true, 11, 4);
                        GL.TexCoord2(s, t);
                        break;

                    case GeometryCmd.TEXIMAGE_PARAM:
                        break;
                    case GeometryCmd.PLTT_BASE:
                        break;
                    #endregion

                    case GeometryCmd.DIF_AMB:
                        break;
                    case GeometryCmd.SPE_EMI:
                        break;
                    case GeometryCmd.LIGHT_VECTOR:
                        break;
                    case GeometryCmd.LIGHT_COLOR:
                        break;
                    case GeometryCmd.SHININESS:
                        break;

                    case GeometryCmd.NORMAL:
                        float x, y, z;
                        x = NSBMDLoader.GetDouble((int)(cmd.Parameters[0] & 0x3FFF), true, 0, 9);
                        y = NSBMDLoader.GetDouble((int)((cmd.Parameters[0] >> 10) & 0x3FFF), true, 0, 9);
                        z = NSBMDLoader.GetDouble((int)(cmd.Parameters[0] >> 20), true, 0, 9);

                        // Multiplay by the directional matrix
                        GL.Normal3(x, y, z);
                        break;

                    case GeometryCmd.BEGIN_VTXS:
                        if (cmd.Parameters[0] == 0)
                            GL.Begin(BeginMode.Triangles);
                        else if (cmd.Parameters[0] == 1)
                            GL.Begin(BeginMode.Quads);
                        else if (cmd.Parameters[0] == 2)
                            GL.Begin(BeginMode.TriangleStrip);
                        else if (cmd.Parameters[0] == 3)
                            GL.Begin(BeginMode.QuadStrip);
                        break;
                    case GeometryCmd.END_VTXS:
                        GL.End();
                        break;

                    case GeometryCmd.SWAP_BUFFERS:
                        break;
                    case GeometryCmd.VIEWPORT:
                        break;
                    case GeometryCmd.BOX_TEST:
                        break;
                    case GeometryCmd.POS_TEST:
                        break;
                    case GeometryCmd.VEC_TEST:
                        break;
                    default:
                        break;
                }
            }

            GL.Flush();
        }

        #endregion

        private void bModelExport_Click(object sender, EventArgs e)
        {
            saveDialog.FileName = map.Model.Name;
            saveDialog.Filter = "Nitro Models|*.nsbmd";
            saveDialog.Title = "Export Map Model";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                // This is much easier than replacing, let me tell you.
                File.WriteAllBytes(saveDialog.FileName, map.GetModelData());
            }
        }

        private void bModelImport_Click(object sender, EventArgs e)
        {
            openDialog.FileName = "";
            openDialog.Filter = "Nitro Models|*.nsbmd";
            openDialog.Title = "Import Map Model";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                // Try to import a new model
                if (map.SetModelData(File.ReadAllBytes(openDialog.FileName)))
                {
                    // Success, reload textures
                    LoadAllMapTextures();
                    glMapModel.Invalidate();
                }
            }
        }

        private void txtMapModelName_TextChanged(object sender, EventArgs e)
        {
            if (map != null)
            {
                if (txtMapModelName.TextLength > 0) map.Name = txtMapModelName.Text;
            }
        }

        #endregion

        #endregion

        #region Wild Pokémon

        #region Grass

        private void txtWildsWalkingRate_TextChanged(object sender, EventArgs e)
        {
            // Label
            lblWildsWalkingRate.Text = Math.Round(((double)txtWildsWalkingRate.Value / 255d) * 100d, 1) + "%";

            // Set data
            if (!mc)
            {
                encounters.WalkingRate = txtWildsWalkingRate.Value;
            }
        }

        private void cWildsWalking_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc || encounters == null) return;

            // Set walking Pokémon species
            encounters.WalkingSpecies[0] = cWildsWalking0.SelectedIndex;
            encounters.WalkingSpecies[1] = cWildsWalking1.SelectedIndex;
            encounters.WalkingSpecies[2] = cWildsWalking2.SelectedIndex;
            encounters.WalkingSpecies[3] = cWildsWalking3.SelectedIndex;
            encounters.WalkingSpecies[4] = cWildsWalking4.SelectedIndex;
            encounters.WalkingSpecies[5] = cWildsWalking5.SelectedIndex;
            encounters.WalkingSpecies[6] = cWildsWalking6.SelectedIndex;
            encounters.WalkingSpecies[7] = cWildsWalking7.SelectedIndex;
            encounters.WalkingSpecies[8] = cWildsWalking8.SelectedIndex;
            encounters.WalkingSpecies[9] = cWildsWalking9.SelectedIndex;
            encounters.WalkingSpecies[10] = cWildsWalking10.SelectedIndex;
            encounters.WalkingSpecies[11] = cWildsWalking11.SelectedIndex;//*/
        }

        private void txtWildsWalking_TextChanged(object sender, EventArgs e)
        {
            if (mc || encounters == null) return;

            // Set walking Pokémon levels
            encounters.WalkingLevels[0] = txtWildsWalking0.Value;
            encounters.WalkingLevels[1] = txtWildsWalking1.Value;
            encounters.WalkingLevels[2] = txtWildsWalking2.Value;
            encounters.WalkingLevels[3] = txtWildsWalking3.Value;
            encounters.WalkingLevels[4] = txtWildsWalking4.Value;
            encounters.WalkingLevels[5] = txtWildsWalking5.Value;
            encounters.WalkingLevels[6] = txtWildsWalking6.Value;
            encounters.WalkingLevels[7] = txtWildsWalking7.Value;
            encounters.WalkingLevels[8] = txtWildsWalking8.Value;
            encounters.WalkingLevels[9] = txtWildsWalking9.Value;
            encounters.WalkingLevels[10] = txtWildsWalking10.Value;
            encounters.WalkingLevels[11] = txtWildsWalking11.Value;
        }

        private void cWildsTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc || encounters == null) return;

            encounters.Morning[0] = cWildsMorn0.SelectedIndex;
            encounters.Morning[1] = cWildsMorn1.SelectedIndex;
            encounters.Day[0] = cWildsDay0.SelectedIndex;
            encounters.Day[1] = cWildsDay1.SelectedIndex;
            encounters.Night[0] = cWildsNight0.SelectedIndex;
            encounters.Night[1] = cWildsNight1.SelectedIndex;
        }

        private void cWildsRadar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc || encounters == null) return;

            encounters.Radar[0] = cWildsRadar0.SelectedIndex;
            encounters.Radar[1] = cWildsRadar1.SelectedIndex;
            encounters.Radar[2] = cWildsRadar2.SelectedIndex;
            encounters.Radar[3] = cWildsRadar3.SelectedIndex;
        }

        private void cWildsDual_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc || encounters == null) return;

            encounters.Ruby[0] = cWildsRuby0.SelectedIndex;
            encounters.Ruby[1] = cWildsRuby1.SelectedIndex;
            encounters.Sapphire[0] = cWildsSapp0.SelectedIndex;
            encounters.Sapphire[1] = cWildsSapp1.SelectedIndex;
            encounters.FireRed[0] = cWildsFire0.SelectedIndex;
            encounters.FireRed[1] = cWildsFire1.SelectedIndex;
            encounters.LeafGreen[0] = cWildsLeaf0.SelectedIndex;
            encounters.LeafGreen[1] = cWildsLeaf1.SelectedIndex;
            encounters.Emerald[0] = cWildsEm0.SelectedIndex;
            encounters.Emerald[1] = cWildsEm1.SelectedIndex;
        }

        #endregion

        #region Water

        private void txtWildsSurfingRate_TextChanged(object sender, EventArgs e)
        {
            // Label
            lblWildsSurfingRate.Text = Math.Round(((double)txtWildsSurfingRate.Value / 255d) * 100d, 1) + "%";

            // Set data
            if (!mc && encounters != null)
            {
                encounters.SurfingRate = txtWildsSurfingRate.Value;
            }
        }

        private void txtWildsORRate_TextChanged(object sender, EventArgs e)
        {
            // Label
            lblWildsORRate.Text = Math.Round(((double)txtWildsORRate.Value / 255d) * 100d, 1) + "%";

            // Set data
            if (!mc && encounters != null)
            {
                encounters.OldRodRate = txtWildsORRate.Value;
            }
        }

        private void txtWildsGRRate_TextChanged(object sender, EventArgs e)
        {
            // Label
            lblWildsGRRate.Text = Math.Round(((double)txtWildsGRRate.Value / 255d) * 100d, 1) + "%";

            // Set data
            if (!mc && encounters != null)
            {
                encounters.GoodRodRate = txtWildsGRRate.Value;
            }
        }

        private void txtWildsSRRate_TextChanged(object sender, EventArgs e)
        {
            // Label
            lblWildsSRRate.Text = Math.Round(((double)txtWildsSRRate.Value / 255d) * 100d, 1) + "%";

            // Set data
            if (!mc && encounters != null)
            {
                encounters.SuperRodRate = txtWildsSRRate.Value;
            }
        }

        private void cWildsSurfing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc || encounters == null) return;

            // Set surfing Pokémon species
            encounters.SurfingSpecies[0] = cWildsSurfing0.SelectedIndex;
            encounters.SurfingSpecies[1] = cWildsSurfing1.SelectedIndex;
            encounters.SurfingSpecies[2] = cWildsSurfing2.SelectedIndex;
            encounters.SurfingSpecies[3] = cWildsSurfing3.SelectedIndex;
            encounters.SurfingSpecies[4] = cWildsSurfing4.SelectedIndex;
        }

        private void txtWildsSurfing_TextChanged(object sender, EventArgs e)
        {
            if (mc || encounters == null) return;

            // Set surfing Pokémon levels
            encounters.SurfingMinLevels[0] = (byte)txtWildsSurfingMin0.Value;
            encounters.SurfingMinLevels[1] = (byte)txtWildsSurfingMin1.Value;
            encounters.SurfingMinLevels[2] = (byte)txtWildsSurfingMin2.Value;
            encounters.SurfingMinLevels[3] = (byte)txtWildsSurfingMin3.Value;
            encounters.SurfingMinLevels[4] = (byte)txtWildsSurfingMin4.Value;

            encounters.SurfingMaxLevels[0] = (byte)txtWildsSurfingMax0.Value;
            encounters.SurfingMaxLevels[1] = (byte)txtWildsSurfingMax1.Value;
            encounters.SurfingMaxLevels[2] = (byte)txtWildsSurfingMax2.Value;
            encounters.SurfingMaxLevels[3] = (byte)txtWildsSurfingMax3.Value;
            encounters.SurfingMaxLevels[4] = (byte)txtWildsSurfingMax4.Value;
        }

        private void cWildsOR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc || encounters == null) return;

            // Set Old Rod Pokémon species
            encounters.OldRodSpecies[0] = cWildsOR0.SelectedIndex;
            encounters.OldRodSpecies[1] = cWildsOR1.SelectedIndex;
            encounters.OldRodSpecies[2] = cWildsOR2.SelectedIndex;
            encounters.OldRodSpecies[3] = cWildsOR3.SelectedIndex;
            encounters.OldRodSpecies[4] = cWildsOR4.SelectedIndex;
        }

        private void txtWildsOR_TextChanged(object sender, EventArgs e)
        {
            if (mc || encounters == null) return;

            // Set Old Rod Pokémon levels
            encounters.OldRodMinLevels[0] = (byte)txtWildsORMin0.Value;
            encounters.OldRodMinLevels[1] = (byte)txtWildsORMin1.Value;
            encounters.OldRodMinLevels[2] = (byte)txtWildsORMin2.Value;
            encounters.OldRodMinLevels[3] = (byte)txtWildsORMin3.Value;
            encounters.OldRodMinLevels[4] = (byte)txtWildsORMin4.Value;

            encounters.OldRodMaxLevels[0] = (byte)txtWildsORMax0.Value;
            encounters.OldRodMaxLevels[1] = (byte)txtWildsORMax1.Value;
            encounters.OldRodMaxLevels[2] = (byte)txtWildsORMax2.Value;
            encounters.OldRodMaxLevels[3] = (byte)txtWildsORMax3.Value;
            encounters.OldRodMaxLevels[4] = (byte)txtWildsORMax4.Value;
        }

        private void cWildsGR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc || encounters == null) return;

            // Set Good Rod Pokémon species
            encounters.GoodRodSpecies[0] = cWildsGR0.SelectedIndex;
            encounters.GoodRodSpecies[1] = cWildsGR1.SelectedIndex;
            encounters.GoodRodSpecies[2] = cWildsGR2.SelectedIndex;
            encounters.GoodRodSpecies[3] = cWildsGR3.SelectedIndex;
            encounters.GoodRodSpecies[4] = cWildsGR4.SelectedIndex;
        }

        private void txtWildsGR_TextChanged(object sender, EventArgs e)
        {
            if (mc || encounters == null) return;

            // Set Good Rod Pokémon levels
            encounters.GoodRodMinLevels[0] = (byte)txtWildsGRMin0.Value;
            encounters.GoodRodMinLevels[1] = (byte)txtWildsGRMin1.Value;
            encounters.GoodRodMinLevels[2] = (byte)txtWildsGRMin2.Value;
            encounters.GoodRodMinLevels[3] = (byte)txtWildsGRMin3.Value;
            encounters.GoodRodMinLevels[4] = (byte)txtWildsGRMin4.Value;

            encounters.GoodRodMaxLevels[0] = (byte)txtWildsGRMax0.Value;
            encounters.GoodRodMaxLevels[1] = (byte)txtWildsGRMax1.Value;
            encounters.GoodRodMaxLevels[2] = (byte)txtWildsGRMax2.Value;
            encounters.GoodRodMaxLevels[3] = (byte)txtWildsGRMax3.Value;
            encounters.GoodRodMaxLevels[4] = (byte)txtWildsGRMax4.Value;
        }

        private void cWildsSR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc || encounters == null) return;

            // Set Old Rod Pokémon species
            encounters.SuperRodSpecies[0] = cWildsSR0.SelectedIndex;
            encounters.SuperRodSpecies[1] = cWildsSR1.SelectedIndex;
            encounters.SuperRodSpecies[2] = cWildsSR2.SelectedIndex;
            encounters.SuperRodSpecies[3] = cWildsSR3.SelectedIndex;
            encounters.SuperRodSpecies[4] = cWildsSR4.SelectedIndex;
        }

        private void txtWildsSR_TextChanged(object sender, EventArgs e)
        {
            if (mc || encounters == null) return;

            // Set Old Rod Pokémon levels
            encounters.SuperRodMinLevels[0] = (byte)txtWildsSRMin0.Value;
            encounters.SuperRodMinLevels[1] = (byte)txtWildsSRMin1.Value;
            encounters.SuperRodMinLevels[2] = (byte)txtWildsSRMin2.Value;
            encounters.SuperRodMinLevels[3] = (byte)txtWildsSRMin3.Value;
            encounters.SuperRodMinLevels[4] = (byte)txtWildsSRMin4.Value;

            encounters.SuperRodMaxLevels[0] = (byte)txtWildsSRMax0.Value;
            encounters.SuperRodMaxLevels[1] = (byte)txtWildsSRMax1.Value;
            encounters.SuperRodMaxLevels[2] = (byte)txtWildsSRMax2.Value;
            encounters.SuperRodMaxLevels[3] = (byte)txtWildsSRMax3.Value;
            encounters.SuperRodMaxLevels[4] = (byte)txtWildsSRMax4.Value;
        }

        #endregion

        #endregion

        #region Header

        private void cHeaderName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mc && selectedMap > -1)
            {
                // Set the data
                header.Name = (ushort)cHeaderName.SelectedIndex;

                // Update display
                mc = true;
                txtHeaderName.Text = locationNames[header.Name];
                this.Text = "DS Map ~ " + headerNames[mapHeaders[selectedMap]] + " > " + locationNames[header.Name] + " > " + map.Name;
                mc = false;
            }
        }

        private void txtHeaderNameStyle_TextChanged(object sender, EventArgs e)
        {
            if (mc || selectedMap == -1) return;

            header.NameStyle = (byte)txtHeaderNameStyle.Value;
        }

        private void txtHeaderNameFrame_TextChanged(object sender, EventArgs e)
        {
            if (mc || selectedMap == -1) return;

            header.NameFrame = (byte)txtHeaderNameFrame.Value;
        }

        private void bHeaderName_Click(object sender, EventArgs e)
        {
            if (mc || selectedMap == -1) return;

            if (txtHeaderName.TextLength > 0)
            {
                // TODO: Save the name
                locationNames[header.Name] = txtHeaderName.Text;
                
                // Update display
                mc = true;
                cHeaderName.Items[header.Name] = txtHeaderName.Text;
                this.Text = "DS Map ~ " + headerNames[mapHeaders[selectedMap]] + " > " + locationNames[header.Name] + " > " + map.Name;
                mc = false;
            }
        }

        private void txtHeaderOptions_TextChanged(object sender, EventArgs e)
        {
            if (mc || selectedMap == -1) return;

            header.MusicDay = (ushort)txtHeaderMusicDay.Value;
            header.MusicNight = (ushort)txtHeaderMusicNight.Value;
            header.Camera = (byte)txtHeaderCamera.Value;
            header.Weather = (byte)txtHeaderWeather.Value;
            header.Flags = (byte)txtHeaderFlags.Value;
        }

        private void txtHeaderFiles_TextChanged(object sender, EventArgs e)
        {
            if (mc || selectedMap == -1) return;

            header.Events = (ushort)txtHeaderEvents.Value;
            header.Scripts = (ushort)txtHeaderScripts.Value;
            header.LevelScripts = (ushort)txtHeaderLvlScripts.Value;
            header.Texts = (ushort)txtHeaderText.Value;
        }

        private void bHeaderTex_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not yet~! :P");
        }

        #endregion

        #region Scripts

        private void bTokenize_Click(object sender, EventArgs e)
        {
            TokenReader scripts = Tokenizer.Tokenize(txtScripts.Text);
            TokenReader functions = Tokenizer.Tokenize(txtFunctions.Text);
            TokenReader moves = Tokenizer.Tokenize(txtMovements.Text);

            string ss = "Scripts:\n";
            foreach (var t in scripts)
            {
                ss += t.ToString() + " ";
            }

            ss += "\n\nFunctions:\n";
            foreach (var t in functions)
            {
                ss += t.ToString() + " ";
            }

            ss += "\n\nMovements:\n";
            foreach (var t in moves)
            {
                ss += t.ToString() + " ";
            }

            txtTokens.Text = ss;
        }

        private void bCompile_Click(object sender, EventArgs e)
        {
            txtTokens.Text = "Trying to compile...\n\n";
            try
            {
                TokenReader scripts = Tokenizer.Tokenize(txtScripts.Text);
                TokenReader functions = Tokenizer.Tokenize(txtFunctions.Text);
                TokenReader moves = Tokenizer.Tokenize(txtMovements.Text);

                Compiler compiler = new Compiler(scriptCommands);
                Block[] blocks = compiler.Compile(scripts, functions, moves);

                string s = "";
                foreach (var block in blocks)
                {
                    s += block.ToString() + "\n\n";
                }
                txtTokens.Text += s;
            }
            catch (Exception ex)
            {
                txtTokens.Text = "Error:\n" + ex.Message + "\n\n" + ex.StackTrace + "\n\n";
            }
            txtTokens.Text += "Done!";
        }

        private void ConfigureScriptStyles()
        {
            #region txtScripts

            // Set line numbers visible
            txtScripts.Margins[0].Type = MarginType.Number;
            txtScripts.Margins[0].Width = 16;

            // Configuring the default style with properties
            // we have common to every lexer style saves time.
            txtScripts.StyleResetDefault();
            txtScripts.Styles[Style.Default].Font = "Consolas";
            txtScripts.Styles[Style.Default].Size = 10;
            txtScripts.StyleClearAll();

            txtScripts.Styles[Style.Cpp.CommentLine].ForeColor = Color.Green;
            txtScripts.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            txtScripts.Styles[Style.Cpp.Word2].ForeColor = Color.Maroon;

            // Outputs:
            // Primary keywords and identifiers
            // Secondary keywords and identifiers
            // Documentation comment keywords
            // Global classes and typedefs
            // Preprocessor definitions
            // Task marker and error marker keywords
            // keywords 0 are set in when a ROM is loaded
            txtScripts.SetKeywords(1, "@");

            // Instruct the lexer to calculate folding
            txtScripts.SetProperty("fold", "1");
            txtScripts.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            txtScripts.Margins[1].Type = MarginType.Symbol;
            txtScripts.Margins[1].Mask = Marker.MaskFolders;
            txtScripts.Margins[1].Sensitive = true;
            txtScripts.Margins[1].Width = 16;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                txtScripts.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                txtScripts.Markers[i].SetBackColor(SystemColors.ControlDark);
            }

            // Configure folding markers with respective symbols
            txtScripts.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            txtScripts.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            txtScripts.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            txtScripts.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            txtScripts.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            txtScripts.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            txtScripts.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            txtScripts.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

            #endregion

            #region txtFunctions

            // Set line numbers visible
            txtFunctions.Margins[0].Type = MarginType.Number;
            txtFunctions.Margins[0].Width = 16;

            // Configuring the default style with properties
            // we have common to every lexer style saves time.
            txtFunctions.StyleResetDefault();
            txtFunctions.Styles[Style.Default].Font = "Consolas";
            txtFunctions.Styles[Style.Default].Size = 10;
            txtFunctions.StyleClearAll();

            txtFunctions.Styles[Style.Cpp.CommentLine].ForeColor = Color.Green;
            txtFunctions.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            txtFunctions.Styles[Style.Cpp.Word2].ForeColor = Color.Maroon;



            // Outputs:
            // Primary keywords and identifiers
            // Secondary keywords and identifiers
            // Documentation comment keywords
            // Global classes and typedefs
            // Preprocessor definitions
            // Task marker and error marker keywords
            txtFunctions.SetKeywords(1, "@");

            // Instruct the lexer to calculate folding
            txtFunctions.SetProperty("fold", "1");
            txtFunctions.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            txtFunctions.Margins[1].Type = MarginType.Symbol;
            txtFunctions.Margins[1].Mask = Marker.MaskFolders;
            txtFunctions.Margins[1].Sensitive = true;
            txtFunctions.Margins[1].Width = 16;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                txtFunctions.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                txtFunctions.Markers[i].SetBackColor(SystemColors.ControlDark);
            }

            // Configure folding markers with respective symbols
            txtFunctions.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            txtFunctions.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            txtFunctions.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            txtFunctions.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            txtFunctions.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            txtFunctions.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            txtFunctions.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            txtFunctions.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

            #endregion

            #region txtMovements
            // Set line numbers visible
            txtMovements.Margins[0].Type = MarginType.Number;
            txtMovements.Margins[0].Width = 16;

            // Configuring the default style with properties
            // we have common to every lexer style saves time.
            txtMovements.StyleResetDefault();
            txtMovements.Styles[Style.Default].Font = "Consolas";
            txtMovements.Styles[Style.Default].Size = 10;
            txtMovements.StyleClearAll();

            
            txtMovements.Styles[Style.Cpp.CommentLine].ForeColor = Color.Green;
            txtMovements.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            txtMovements.Styles[Style.Cpp.Word2].ForeColor = Color.Maroon;
            //txtMovements.Lexer = Lexer.Cpp;

            // Outputs:
            // Primary keywords and identifiers
            // Secondary keywords and identifiers
            // Documentation comment keywords
            // Global classes and typedefs
            // Preprocessor definitions
            // Task marker and error marker keywords
            txtMovements.SetKeywords(1, "$");

            // Instruct the lexer to calculate folding
            txtMovements.SetProperty("fold", "1");
            txtMovements.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            txtMovements.Margins[1].Type = MarginType.Symbol;
            txtMovements.Margins[1].Mask = Marker.MaskFolders;
            txtMovements.Margins[1].Sensitive = true;
            txtMovements.Margins[1].Width = 16;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                txtMovements.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                txtMovements.Markers[i].SetBackColor(SystemColors.ControlDark);
            }

            // Configure folding markers with respective symbols
            txtMovements.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            txtMovements.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            txtMovements.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            txtMovements.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            txtMovements.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            txtMovements.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            txtMovements.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            txtMovements.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);
            #endregion
        }

        private int txtMovementsMaxLineNumberCharLength;
        private void txtMovements_TextChanged(object sender, EventArgs e)
        {
            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = txtMovements.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == txtMovementsMaxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            txtMovements.Margins[0].Width = txtMovements.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            txtMovementsMaxLineNumberCharLength = maxLineNumberCharLength;
        }

        private int txtScriptsMaxLineNumberCharLength;
        private void txtScripts_TextChanged(object sender, EventArgs e)
        {
            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = txtScripts.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == txtScriptsMaxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            txtScripts.Margins[0].Width = txtScripts.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            txtScriptsMaxLineNumberCharLength = maxLineNumberCharLength;
        }

        private int txtFunctionsMaxLineNumberCharLength;
        private void txtFunctions_TextChanged(object sender, EventArgs e)
        {
            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = txtFunctions.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == txtFunctionsMaxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            txtFunctions.Margins[0].Width = txtFunctions.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            txtFunctionsMaxLineNumberCharLength = maxLineNumberCharLength;
        }

        #endregion

        private void ConfigureTextStyle()
        {
            // Set line numbers visible
            txtText.Margins[0].Type = MarginType.Number;
            txtText.Margins[0].Width = 16;
            
            //txtText.Margins[1].Width = 0; // And hide the first one.
        }

        private int txtTextMaxLineNumberCharLength;
        private void txtText_TextChanged(object sender, EventArgs e)
        {
            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = txtText.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == txtTextMaxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            txtText.Margins[0].Width = txtText.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            txtTextMaxLineNumberCharLength = maxLineNumberCharLength;
        }

    }
}
