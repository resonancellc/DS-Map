using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DSMap
{
    public partial class PaletteEditDialog : Form
    {
        private Color[] _colors;
        private int _color = 0;

        private bool xyz = false;

        public PaletteEditDialog(ref Color[] palette)
        {
            InitializeComponent();

            if (palette.Length != 256) throw new Exception("Bad palette size!");
            _colors = palette;
        }

        private void PaletteEditDialog_Load(object sender, EventArgs e)
        {
            NewColor();
        }

        private void pColors_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.FillRectangle(Brushes.Black, 0, 0, 64, 256);

            if (_colors == null) return;

            // draw palette
            for (int i = 0; i < 256; i++)
            {
                //if (i >= 256) break;

                SolidBrush b = new SolidBrush(_colors[i]);

                int x = (i % 16);
                int y = (i / 16);
                e.Graphics.FillRectangle(b, x * 16, y * 16, 16, 16);
            }

            // draw selection
            Color invert = Color.FromArgb(255 - _colors[_color].R, 255 - _colors[_color].G, 255 - _colors[_color].B);
            Pen p = new Pen(new SolidBrush(invert));
            e.Graphics.DrawRectangle(p, _color % 16 * 16, _color / 16 * 16, 15, 15);
        }

        private void pColor_Paint(object sender, PaintEventArgs e)
        {
            if (_color >= 0 && _color <= 255)
            {
                Color color = Color.FromArgb(_colors[_color].R, _colors[_color].G, _colors[_color].B);
                e.Graphics.FillRectangle(new SolidBrush(color), 0, 0, 48, 48);
            }
        }

        private void NewColor()
        {
            if (_color >= 0 && _color <= 255)
            {
                xyz = true;

                Color color = _colors[_color];

                txtRed.Value = color.R;
                txtGreen.Value = color.G;
                txtBlue.Value = color.B;

                trkRed.Value = color.R;
                trkGreen.Value = color.G;
                trkBlue.Value = color.B;

                pColor.Invalidate();
                pColors.Invalidate();

                lblColor.Text = _color.ToString();

                xyz = false;
            }
        }

        private void txtColors_TextChanged(object sender, EventArgs e)
        {
            if (xyz) return;

            if (_color >= 0 && _color <= 255)
            {
                xyz = true;

                // Update
                trkRed.Value = (int)txtRed.Value;
                trkGreen.Value = (int)txtGreen.Value;
                trkBlue.Value = (int)txtBlue.Value;
                
                //
                _colors[_color] = Color.FromArgb(140, (int)txtRed.Value, (int)txtGreen.Value, (int)txtBlue.Value);

                //
                pColor.Invalidate();
                pColors.Invalidate();

                xyz = false;
            }
        }

        private void trkColors_ValueChanged(object sender, EventArgs e)
        {
            if (xyz) return;

            if (_color >= 0 && _color <= 255)
            {
                xyz = true;

                txtRed.Value = (uint)trkRed.Value;
                txtGreen.Value = (uint)trkGreen.Value;
                txtBlue.Value = (uint)trkBlue.Value;

                //
                _colors[_color] = Color.FromArgb(140, (int)txtRed.Value, (int)txtGreen.Value, (int)txtBlue.Value);

                //
                pColor.Invalidate();
                pColors.Invalidate();

                xyz = false;
            }
        }

        private void pColors_MouseClick(object sender, MouseEventArgs e)
        {
            if (_colors == null) return;

            int x = e.X / 16;
            int y = e.Y / 16;
            _color = x + y * 16;

            NewColor();
            
        }
    }
}
