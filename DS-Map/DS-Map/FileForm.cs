using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lost
{
    public partial class FileForm : Form
    {
        TreeNode root;

        public FileForm(string rootDirectory)
        {
            InitializeComponent();

            root = new TreeNode("root");
            FindFiles(rootDirectory, root);

            treeFiles.Nodes.Clear();
            treeFiles.Nodes.Add(root);

            root.Expand();
        }

        void FindFiles(string path, TreeNode parent)
        {
            // add directories
            foreach (var dir in Directory.GetDirectories(path))
            {
                // create a node
                var node = new TreeNode(Path.GetFileName(dir));
                node.Tag = $"D:{dir}";

                // fill node with children
                FindFiles(dir, node);

                // add to parent
                parent.Nodes.Add(node);
            }

            // add files
            foreach (var file in Directory.GetFiles(path))
            {
                // create a node
                var node = new TreeNode(Path.GetFileName(file));
                node.Tag = $"F:{file}";

                // add to parent
                parent.Nodes.Add(node);
            }
        }
    }
}
