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

using OpenTK.Graphics.OpenGL;

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

        // editing
        private int selectedMap = -1;
        private Map map = null;
        private Header header = null;
        private Dictionary<int, int> mapHeaders = new Dictionary<int, int>();

        private int selectedObj = -1;

        //private Model mapModel;
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

            // GL
            glMapModel.Context.MakeCurrent(glMapModel.WindowInfo);
            GL.Viewport(0, 0, glMapModel.Width, glMapModel.Height);
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

                // Load the map texture NARC
                mapTextureData = new NARC(GetROMFilePathFromIni("MapTextureData"));

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

            // Reset angle settings
            //mapModelSettings.AngleX = -180f;
            //mapModelSettings.AngleY = 0f;
            //mapModelSettings.AngleZ = 45f;
            
            // Load map textures
            using (MemoryStream ms = mapTextureData.GetFileMemoryStream(header.MapTextures))
            {
                mapTextures = NSBTXLoader.LoadBTX0(ms);
                mapTexturesSet = true; // This will allow textures to be loaded
            }

            // Display
            //mapModel = map.Model;
            LoadAllMapTextures();
            glMapModel.Invalidate();

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
            GL.ClearColor(0f, 0f, 0f, 1f); // That cool looking gray
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

        #endregion

    }
}
