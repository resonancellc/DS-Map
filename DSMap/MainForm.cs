﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DSMap
{
    public partial class MainForm : Form
    {
        private ROM rom = new ROM();
        private Ini ini = new Ini();

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
            openDialog.FileName = "";
            openDialog.Filter = "Textures|*.bin";
            openDialog.Title = "Open an NSBTX!";

            if (openDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                NDS.TEX0 texture = NDS.NSBTX.LoadBTX0(openDialog.FileName);
                
                listBox1.Items.Clear();
                listBox1.Items.AddRange(texture.TextureInfo.NameBlock);

                listBox2.Items.Clear();
                listBox2.Items.AddRange(texture.PaletteInfo.NameBlock);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        
    }
}
