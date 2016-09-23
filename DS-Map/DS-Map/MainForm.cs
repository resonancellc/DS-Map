using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lost
{
    public partial class MainForm : Form
    {
        string baseDirectory = string.Empty;
        string rootDirectory = string.Empty;

        Archive mapFile;
        Archive matrixFile;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!File.Exists("ndstool.exe"))
            {
                MessageBox.Show("ndstool.exe could not be found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var o = new OpenFileDialog()
            {
                FileName = "",
                Filter = "NDS ROMs|*.nds",
                Title = "Open ROM"
            })
            {
                if (o.ShowDialog() != DialogResult.OK)
                    return;

                var directory = Path.Combine(Path.GetDirectoryName(o.FileName), Path.GetFileNameWithoutExtension(o.FileName));
                if (Directory.Exists(directory))
                {
                    if (MessageBox.Show($"A directory for {o.FileName} was found!\nExtract ROM anyway?",
                        "Extract?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        ExtractROM(o.FileName, directory);
                }
                else
                {
                    ExtractROM(o.FileName, directory);
                }

                OpenROM(directory);
            }
        }
        
        void OpenROM(string directory)
        {
            // TODO
            baseDirectory = directory;
            rootDirectory = Path.Combine(directory, "root");

            var he = ROM.LoadHeader(Path.Combine(baseDirectory, "header.bin"));

            var banner = ROM.LoadBanner(Path.Combine(baseDirectory, "banner.bin"));
            pIcon.Image = banner.Icon;
            lblROM.Text = $"{banner.EnglishTitle}\n{he.Title} [{he.Code}]";

            mapFile = new Archive(Path.Combine(rootDirectory, @"fielddata\land_data\land_data.narc"));
            matrixFile = new Archive(Path.Combine(rootDirectory, @"fielddata\mapmatrix\map_matrix.narc"));

            var headerNames = Header.LoadNames(Path.Combine(rootDirectory, @"fielddata\maptable\mapname.bin"));
            var mapNames = Map.LoadNames(mapFile);

            var headerTable = 0xE601Cu;
            var headerMatrixMatches = Header.LoadHeaderMatrixMatches(Path.Combine(baseDirectory, "arm9.bin"), headerTable, headerNames.Length);
            var headerMapMatches = Matrix.LoadHeaderMapMatches(matrixFile, headerMatrixMatches);

            // populate the tree with maps
            // TODO: catch any free maps
            for (int h = 0; h < headerNames.Length; h++)
            {
                // node tag pattern:
                // header:map
                // -1 for invalid

                var headerNode = new TreeNode(headerNames[h]);
                headerNode.Tag = $"{h}:-1";
                headerNode.ImageIndex = 1;
                headerNode.SelectedImageIndex = 1;

                foreach (var m in headerMapMatches[h])
                {
                    var mapNode = new TreeNode(mapNames[m]);
                    mapNode.Tag = $"{h}:{m}";
                    mapNode.ImageIndex = 0;
                    mapNode.SelectedImageIndex = 0;

                    headerNode.Nodes.Add(mapNode);
                }

                treeMaps.Nodes.Add(headerNode);
            }
        }

        void ExtractROM(string filename, string directory)
        {
            // delete an existing directory, if needed
            if (Directory.Exists(directory))
                Directory.Delete(directory, true);

            var root = Path.Combine(directory, "root");
            var overlay = Path.Combine(directory, "overlay");

            // create the extraction directory
            Directory.CreateDirectory(directory);
            Directory.CreateDirectory(root);
            Directory.CreateDirectory(overlay);

            // use ndstool to extract the ROM
            var proc = new Process();
            proc.StartInfo.FileName = "ndstool.exe";
            proc.StartInfo.Arguments =
                $"-x \"{filename}\" " +
                $" -9 \"{Path.Combine(directory, "arm9.bin")}\" " +
                $" -7 \"{Path.Combine(directory, "arm7.bin")}\" " +
                $" -y9 \"{Path.Combine(directory, "y9.bin")}\" " +
                $" -y7 \"{Path.Combine(directory, "y7.bin")}\" " +
                $" -d \"{root}\" " +
                $" -y \"{overlay}\" " +
                $" -t \"{Path.Combine(directory, "banner.bin")}\" " +
                $" -h \"{Path.Combine(directory, "header.bin")}\""
                ;

            proc.Start();
            proc.WaitForExit();
        }

        void BuildROM(string directory, string filename)
        {
            var root = Path.Combine(directory, "root");
            var overlay = Path.Combine(directory, "overlay");

            // use ndstool to extract the ROM
            var proc = new Process();
            proc.StartInfo.FileName = "ndstool.exe";
            proc.StartInfo.Arguments =
                $"-c \"{filename}\" " +
                $" -9 \"{Path.Combine(directory, "arm9.bin")}\" " +
                $" -7 \"{Path.Combine(directory, "arm7.bin")}\" " +
                $" -y9 \"{Path.Combine(directory, "y9.bin")}\" " +
                $" -y7 \"{Path.Combine(directory, "y7.bin")}\" " +
                $" -d \"{root}\" " +
                $" -y \"{overlay}\" " +
                $" -t \"{Path.Combine(directory, "banner.bin")}\" " +
                $" -h \"{Path.Combine(directory, "header.bin")}\""
                ;

            proc.Start();
            proc.WaitForExit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rootDirectory == string.Empty)
                return;

            using (var editor = new FileForm(rootDirectory))
                editor.ShowDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var map = new Map(mapFile.GetFileStream(listBox1.SelectedIndex));
            //Text = map.Model.Name;
        }
    }
}
