using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DSHL.Formats.Nitro.Models
{
    // Deals with Wavefront Object files
    public class OBJExporter
    {
        public void ExportModel(string file, NSBMD model)
        {
            if (!model.HasTEX0) throw new Exception("Expected textures with model!");

            ExportModel(file, model, model.TEX0);
        }

        public void ExportModel(string file, NSBMD model, NSBTX textures)
        {
            string objFile = file;
            string mtlFile = Path.ChangeExtension(file, ".mtl");
            string mtlDir = Path.GetDirectoryName(file);

            // Write material file
            using (StreamWriter sw = File.CreateText(mtlFile))
            {
                // Have to do that stuff...
                foreach (var mat in model.MDL0.Materials)
                {
                    // Get the associated texture/palette
                    if (!mat.MatchedPal || !mat.MatchedTex) continue;

                    var tex = textures.GetTexture(mat.Texture.Name);
                    var pal = textures.GetPalette(mat.Palette.Name);

                    sw.WriteLine(string.Format("newmtl {0}", tex.Name));
                    sw.WriteLine(string.Format("Ka {0} {1} {2}", 0.0d, 0.0d, 0.0d));
                    sw.WriteLine(string.Format("Kd {0} {1} {2}", 0.0d, 0.0d, 0.0d));
                    sw.WriteLine("d 1.0000");
                }
            }
        }
    }
}
