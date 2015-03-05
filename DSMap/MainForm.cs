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
        private ROM rom = new ROM();
        private Ini ini = new Ini();

        private int selTex = -1, selPal = -1;

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

        }

        private void LoadROMData()
        {
            if (!rom.IsLoaded()) return;

            try
            {
                // Load the map and matrix NARCs
                NARC matrixData = new NARC(GetROMFilePathFromIni("MatrixData"));
                NARC mapData = new NARC(GetROMFilePathFromIni("MapData"));

                // Load the map names
                string[] mapNames = Map.LoadMapNames(mapData);

                // Load the header names
                int headerCount = 0;
                string[] headerNames = Header.LoadHeaderNames(GetROMFilePathFromIni("HeaderNames"), out headerCount);

                // Match the map headers to the maps
                uint headerTable = Convert.ToUInt32(ini[rom.Header.Code, "HeaderTable"], 16);
                Dictionary<int, int> headerMatrixMatches = Header.LoadHeaderMatrixMatches(rom.GetFullFilePath("arm9.bin"), headerTable, headerCount);
                Dictionary<int, List<int>> headerMapMatches = Matrix.LoadHeaderMapMatches(matrixData, headerMatrixMatches);

                // Fill the treeview with headers and associated maps
                treeMaps.Nodes.Clear();
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
        
    }
}
