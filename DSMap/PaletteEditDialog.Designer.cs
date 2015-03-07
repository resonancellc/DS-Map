namespace DSMap
{
    partial class PaletteEditDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pColors = new System.Windows.Forms.PictureBox();
            this.trkBlue = new System.Windows.Forms.TrackBar();
            this.trkGreen = new System.Windows.Forms.TrackBar();
            this.trkRed = new System.Windows.Forms.TrackBar();
            this.pColor = new System.Windows.Forms.PictureBox();
            this.txtBlue = new DSMap.NumericTextBox();
            this.txtGreen = new DSMap.NumericTextBox();
            this.txtRed = new DSMap.NumericTextBox();
            this.lblColor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pColors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pColor)).BeginInit();
            this.SuspendLayout();
            // 
            // pColors
            // 
            this.pColors.Location = new System.Drawing.Point(12, 12);
            this.pColors.Name = "pColors";
            this.pColors.Size = new System.Drawing.Size(256, 256);
            this.pColors.TabIndex = 1;
            this.pColors.TabStop = false;
            this.pColors.Paint += new System.Windows.Forms.PaintEventHandler(this.pColors_Paint);
            this.pColors.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pColors_MouseClick);
            // 
            // trkBlue
            // 
            this.trkBlue.Location = new System.Drawing.Point(50, 326);
            this.trkBlue.Maximum = 255;
            this.trkBlue.Name = "trkBlue";
            this.trkBlue.Size = new System.Drawing.Size(166, 45);
            this.trkBlue.TabIndex = 9;
            this.trkBlue.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkBlue.ValueChanged += new System.EventHandler(this.trkColors_ValueChanged);
            // 
            // trkGreen
            // 
            this.trkGreen.Location = new System.Drawing.Point(50, 300);
            this.trkGreen.Maximum = 255;
            this.trkGreen.Name = "trkGreen";
            this.trkGreen.Size = new System.Drawing.Size(166, 45);
            this.trkGreen.TabIndex = 8;
            this.trkGreen.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkGreen.ValueChanged += new System.EventHandler(this.trkColors_ValueChanged);
            // 
            // trkRed
            // 
            this.trkRed.Location = new System.Drawing.Point(50, 274);
            this.trkRed.Maximum = 255;
            this.trkRed.Name = "trkRed";
            this.trkRed.Size = new System.Drawing.Size(166, 45);
            this.trkRed.TabIndex = 7;
            this.trkRed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkRed.ValueChanged += new System.EventHandler(this.trkColors_ValueChanged);
            // 
            // pColor
            // 
            this.pColor.Location = new System.Drawing.Point(12, 274);
            this.pColor.Name = "pColor";
            this.pColor.Size = new System.Drawing.Size(32, 32);
            this.pColor.TabIndex = 10;
            this.pColor.TabStop = false;
            this.pColor.Paint += new System.Windows.Forms.PaintEventHandler(this.pColor_Paint);
            // 
            // txtBlue
            // 
            this.txtBlue.ForeColor = System.Drawing.Color.Blue;
            this.txtBlue.Location = new System.Drawing.Point(222, 326);
            this.txtBlue.MaximumValue = ((uint)(255u));
            this.txtBlue.MinimumValue = ((uint)(0u));
            this.txtBlue.Name = "txtBlue";
            this.txtBlue.Size = new System.Drawing.Size(46, 20);
            this.txtBlue.TabIndex = 13;
            this.txtBlue.Text = "0";
            this.txtBlue.Value = ((uint)(0u));
            this.txtBlue.TextChanged += new System.EventHandler(this.txtColors_TextChanged);
            // 
            // txtGreen
            // 
            this.txtGreen.ForeColor = System.Drawing.Color.Green;
            this.txtGreen.Location = new System.Drawing.Point(222, 300);
            this.txtGreen.MaximumValue = ((uint)(255u));
            this.txtGreen.MinimumValue = ((uint)(0u));
            this.txtGreen.Name = "txtGreen";
            this.txtGreen.Size = new System.Drawing.Size(46, 20);
            this.txtGreen.TabIndex = 12;
            this.txtGreen.Text = "0";
            this.txtGreen.Value = ((uint)(0u));
            this.txtGreen.TextChanged += new System.EventHandler(this.txtColors_TextChanged);
            // 
            // txtRed
            // 
            this.txtRed.ForeColor = System.Drawing.Color.Red;
            this.txtRed.Location = new System.Drawing.Point(222, 274);
            this.txtRed.MaximumValue = ((uint)(255u));
            this.txtRed.MinimumValue = ((uint)(0u));
            this.txtRed.Name = "txtRed";
            this.txtRed.Size = new System.Drawing.Size(46, 20);
            this.txtRed.TabIndex = 11;
            this.txtRed.Text = "0";
            this.txtRed.Value = ((uint)(0u));
            this.txtRed.TextChanged += new System.EventHandler(this.txtColors_TextChanged);
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(12, 309);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(13, 13);
            this.lblColor.TabIndex = 14;
            this.lblColor.Text = "0";
            // 
            // PaletteEditDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 358);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.txtBlue);
            this.Controls.Add(this.txtGreen);
            this.Controls.Add(this.txtRed);
            this.Controls.Add(this.pColor);
            this.Controls.Add(this.trkBlue);
            this.Controls.Add(this.trkGreen);
            this.Controls.Add(this.trkRed);
            this.Controls.Add(this.pColors);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaletteEditDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Palette";
            this.Load += new System.EventHandler(this.PaletteEditDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pColors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pColors;
        private System.Windows.Forms.TrackBar trkBlue;
        private System.Windows.Forms.TrackBar trkGreen;
        private System.Windows.Forms.TrackBar trkRed;
        private System.Windows.Forms.PictureBox pColor;
        private NumericTextBox txtRed;
        private NumericTextBox txtGreen;
        private NumericTextBox txtBlue;
        private System.Windows.Forms.Label lblColor;
    }
}