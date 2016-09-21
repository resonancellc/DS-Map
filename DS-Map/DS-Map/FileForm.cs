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
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;

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

                var type = GuessFileType(file);
                node.ImageIndex = type;
                node.SelectedImageIndex = type;

                // add to parent
                parent.Nodes.Add(node);
            }
        }

        int GuessFileType(string filename)
        {
            // first try to guess based on extension
            switch (Path.GetExtension(filename).ToUpper())
            {
                case ".NARC":
                    return 2;
                case ".TXT":
                    return 3;
                case ".DAT":
                case ".BIN":
                    return 4;
                case ".SDAT":
                    return 5;
                case ".NCLR":
                    return 6;
                case ".NCGR":
                    return 7;
                case ".NSBMD":
                    return 8;
                case ".NANR":
                    return 9;
            }

            // read first four bytes of the file
            // we can compare it against known file types
            var buffer = new byte[4];
            using (var fs = File.OpenRead(filename))
                fs.Read(buffer, 0, 4);

            // convert to string, ignore bad values
            string id = "";
            for (int i = 0; i < 4; i++)
            {
                if (buffer[i] == 0)
                    break;
                else
                    id += (char)buffer[i];
            }
            
            // compare against known values IDs :)
            switch (id)
            {
                case "NARC":
                case "CARC":
                    return 2;
                case "SDAT":
                    return 5;
                case "RLCN":
                case "RPCN":
                    return 6;
                case "RGCN":
                case "BTX0":
                    return 7;
                case "BMD0":
                    return 8;
                case "RNAN":
                    return 9;

                default:
                    return 1;
            }
        }
    }
}
