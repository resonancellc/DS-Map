using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using DSMap.NDS;
using DSMap.Formats;

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

        // editing
        private int selectedMap = -1;
        private Map map = null;
        private Header header = null;
        private Dictionary<int, int> mapHeaders = new Dictionary<int, int>();

        private int selectedObj = -1;

        private NSBTX nsbtx;
        private int selectedTex = -1, selectedPal = -1;

        private bool mc = false;

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
            if (!File.Exists("Games.ini"))
            {
                MessageBox.Show("Could not find Games.ini!\nPlease place a copy in this tool's directory before use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            else
            {
                ini.Load("Games.ini");
            }

            // Load movements palette
            if (File.Exists("Movements.act"))
            {
                using (FileStream fs = File.OpenRead("Movements.act"))
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
                MessageBox.Show("Could not find Movements.act!\nPlease place a copy in this tool's directory before use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            // Finally, load behaviours
            // TODO: Other text (for language?)
            if (File.Exists("Text.ini"))
            {
                Ini text = new Ini("Text.ini");

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
                MessageBox.Show("Could not find Games.ini!\nPlease place a copy in this tool's directory before use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            // Create temporary data directory
            Temporary.Create();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Clean up temporary file data
            Temporary.Dispose();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openDialog.FileName = "";
            openDialog.Filter = "NDS ROMs|*.nds";
            openDialog.Title = "Load ROM";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                // Set the ROM
                rom.Load(openDialog.FileName);

                // Do stuff with it
                pBanner.Image = rom.Banner.Image;
                lblROM.Text = "Name: " + rom.Header.Title;
                lblROM.Text += "\nCode: " + rom.Header.Code;

                LoadROMData();
                //MessageBox.Show("Code: '" + rom.Header.Code + "'");
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

            // Save NARCs
            mapData.Save();
        }

        private void LoadROMData()
        {
            if (!rom.IsLoaded()) return;

            try
            {
                // Load the map and matrix NARCs
                NARC matrixData = new NARC(GetROMFilePathFromIni("MatrixData"));
                mapData = new NARC(GetROMFilePathFromIni("MapData"));

                // Load the map names
                string[] mapNames = Map.LoadMapNames(mapData);

                // Load the header names
                headerNames = Header.LoadHeaderNames(GetROMFilePathFromIni("HeaderNames"));

                // Match the map headers to the maps
                headerTable = Convert.ToUInt32(ini[rom.Header.Code, "HeaderTable"], 16);
                Dictionary<int, int> headerMatrixMatches = Header.LoadHeaderMatrixMatches(rom.GetFullFilePath("arm9.bin"), headerTable, headerNames.Length);
                Dictionary<int, List<int>> headerMapMatches = Matrix.LoadHeaderMapMatches(matrixData, headerMatrixMatches);

                // Fill the treeview with headers and associated maps
                treeMaps.Nodes.Clear(); mapHeaders.Clear();
                for (int header = 0; header < headerNames.Length; header++)
                {
                    TreeNode node = new TreeNode(headerNames[header]);
                    node.Tag = -1; // All headers will be -1

                    int[] maps = headerMapMatches[header].ToArray();
                    foreach (int map in maps)
                    {
                        TreeNode jr = new TreeNode(mapNames[map]);
                        jr.Tag = map; // For easy loading

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
        }

        private string GetROMFilePathFromIni(string iniSection)
        {
            if (rom.IsLoaded())
                return rom.GetFullFilePath(ini[rom.Header.Code, iniSection]);
            else
                return string.Empty;
        }

        private void treeMaps_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void treeMaps_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Make sure the ROM has been loaded
            if (!rom.IsLoaded() || treeMaps.SelectedNode == null) return;

            // Get current selection
            TreeNode selectedNode = treeMaps.SelectedNode;
            int tag = (int)selectedNode.Tag;
            if (tag == -1) return;

            // Load the map
            try
            {
                selectedMap = tag;
                LoadAll();
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
            

            // Display

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

            // Show header
            txtHMapTextures.Value = header.MapTextures;
            txtHObjectTextures.Value = header.ObjectTexutres;
        }

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
            using (FileStream fs = File.OpenWrite("Movements.act"))
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

        private void loadAnNSBTXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openDialog.FileName = "";
            openDialog.Filter = "All Files|*.*";
            openDialog.Title = "Open an NSBTX";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                nsbtx = NSBTXLoader.LoadBTX0(openDialog.FileName);

                listBox1.Items.Clear();
                listBox2.Items.Clear();

                foreach (var tex in nsbtx.Textures)
                {
                    listBox1.Items.Add(tex.Name);
                }

                foreach (var pal in nsbtx.Palettes)
                {
                    listBox2.Items.Add(pal.Name);
                }

                selectedTex = -1;
                selectedPal = -1;

                //pictureBox1.Image = NSBTXDrawer.DrawTexture(nsbtx.Textures[0], nsbtx.Palettes[0]);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //selectedTex = listBox1.SelectedIndex;

            if (selectedTex>=0 && selectedPal >= 0)
                pictureBox1.Image = NSBTXDrawer.DrawTexture(nsbtx.Textures[listBox1.SelectedIndex], nsbtx.Palettes[listBox2.SelectedIndex]);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //selectedPal = listBox2.SelectedIndex;

            if (selectedTex >= 0 && selectedPal >= 0)
                pictureBox1.Image = NSBTXDrawer.DrawTexture(nsbtx.Textures[listBox1.SelectedIndex], nsbtx.Palettes[listBox2.SelectedIndex]);
        }

        private void loadAnNSBMDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openDialog.FileName = "";
            openDialog.Filter = "All Files|*.*";
            openDialog.Title = "Open an NSBMD";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    NSBMD bmd = NSBMDLoader.LoadBMD0(openDialog.FileName);

                    listBox1.Items.Clear();
                    listBox1.Items.Add(bmd.MDL0.Name);

                    listBox2.Items.Clear();
                    foreach (var mat in bmd.MDL0.Materials)
                    {
                        listBox2.Items.Add(mat.Name);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace);
                }
                //pictureBox1.Image = NSBTXDrawer.DrawTexture(nsbtx.Textures[0], nsbtx.Palettes[0]);
            }
        }


    }
}
