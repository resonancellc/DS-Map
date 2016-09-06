using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lost
{
    public partial class MainForm : Form
    {
        ROM rom;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            rom?.Dispose();
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

                rom?.Dispose();
                rom = new ROM(o.FileName);

                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(P(rom.Root));
            }
        }

        TreeNode P(ROM.Directory d)
        {
            var n = new TreeNode($"{d.ID:X3} {d.Name}");

            // children
            foreach (var c in d.Directories)
                n.Nodes.Add(P(c));

            // files
            foreach (var f in d.Files)
                n.Nodes.Add(new TreeNode($"{f.ID:X4} {f.Name} - 0x{f.Offset:X6}"));

            return n;
        }
    }
}
