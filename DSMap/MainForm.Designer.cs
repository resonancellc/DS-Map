namespace DSMap
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.rOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bLoadROM = new System.Windows.Forms.ToolStripButton();
            this.bBuildROM = new System.Windows.Forms.ToolStripButton();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.treeMaps = new System.Windows.Forms.TreeView();
            this.lblROM = new System.Windows.Forms.Label();
            this.pBanner = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMap = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabModel = new System.Windows.Forms.TabPage();
            this.tabMovements = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pMovements = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabObjects = new System.Windows.Forms.TabPage();
            this.tabHeader = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rMovePermissions = new System.Windows.Forms.RadioButton();
            this.rMoveFlags = new System.Windows.Forms.RadioButton();
            this.cMovePermission = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bMoveColors = new System.Windows.Forms.Button();
            this.txtHObjectTextures = new DSMap.NumericTextBox();
            this.txtHMapTextures = new DSMap.NumericTextBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBanner)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabMap.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabMovements.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pMovements)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabHeader.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rOMToolStripMenuItem,
            this.mapToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(629, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // rOMToolStripMenuItem
            // 
            this.rOMToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.buildToolStripMenuItem});
            this.rOMToolStripMenuItem.Name = "rOMToolStripMenuItem";
            this.rOMToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.rOMToolStripMenuItem.Text = "ROM";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("loadToolStripMenuItem.Image")));
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // buildToolStripMenuItem
            // 
            this.buildToolStripMenuItem.Image = global::DSMap.Properties.Resources.compile;
            this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            this.buildToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.buildToolStripMenuItem.Text = "Build";
            this.buildToolStripMenuItem.Click += new System.EventHandler(this.buildToolStripMenuItem_Click);
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mapToolStripMenuItem.Text = "Map";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bLoadROM,
            this.bBuildROM});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(629, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // bLoadROM
            // 
            this.bLoadROM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bLoadROM.Image = global::DSMap.Properties.Resources.folder_open;
            this.bLoadROM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bLoadROM.Name = "bLoadROM";
            this.bLoadROM.Size = new System.Drawing.Size(23, 22);
            this.bLoadROM.Text = "Load ROM (Ctrl + O)";
            this.bLoadROM.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // bBuildROM
            // 
            this.bBuildROM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bBuildROM.Image = global::DSMap.Properties.Resources.compile;
            this.bBuildROM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bBuildROM.Name = "bBuildROM";
            this.bBuildROM.Size = new System.Drawing.Size(23, 22);
            this.bBuildROM.Text = "Build ROM";
            this.bBuildROM.Click += new System.EventHandler(this.buildToolStripMenuItem_Click);
            // 
            // openDialog
            // 
            this.openDialog.FileName = "openFileDialog1";
            // 
            // treeMaps
            // 
            this.treeMaps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeMaps.HideSelection = false;
            this.treeMaps.Location = new System.Drawing.Point(12, 52);
            this.treeMaps.Name = "treeMaps";
            this.treeMaps.Size = new System.Drawing.Size(164, 370);
            this.treeMaps.TabIndex = 6;
            this.treeMaps.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeMaps_NodeMouseDoubleClick);
            this.treeMaps.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeMaps_MouseDoubleClick);
            // 
            // lblROM
            // 
            this.lblROM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblROM.AutoSize = true;
            this.lblROM.Location = new System.Drawing.Point(50, 425);
            this.lblROM.Name = "lblROM";
            this.lblROM.Size = new System.Drawing.Size(77, 13);
            this.lblROM.TabIndex = 8;
            this.lblROM.Text = "Load a ROM...";
            // 
            // pBanner
            // 
            this.pBanner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pBanner.Location = new System.Drawing.Point(12, 428);
            this.pBanner.Name = "pBanner";
            this.pBanner.Size = new System.Drawing.Size(32, 32);
            this.pBanner.TabIndex = 7;
            this.pBanner.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabMap);
            this.tabControl1.Controls.Add(this.tabHeader);
            this.tabControl1.Location = new System.Drawing.Point(182, 52);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(435, 408);
            this.tabControl1.TabIndex = 9;
            // 
            // tabMap
            // 
            this.tabMap.Controls.Add(this.tabControl2);
            this.tabMap.Location = new System.Drawing.Point(4, 22);
            this.tabMap.Name = "tabMap";
            this.tabMap.Padding = new System.Windows.Forms.Padding(3);
            this.tabMap.Size = new System.Drawing.Size(427, 382);
            this.tabMap.TabIndex = 0;
            this.tabMap.Text = "Map";
            this.tabMap.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabModel);
            this.tabControl2.Controls.Add(this.tabMovements);
            this.tabControl2.Controls.Add(this.tabObjects);
            this.tabControl2.Location = new System.Drawing.Point(6, 6);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(415, 370);
            this.tabControl2.TabIndex = 0;
            // 
            // tabModel
            // 
            this.tabModel.Location = new System.Drawing.Point(4, 22);
            this.tabModel.Name = "tabModel";
            this.tabModel.Padding = new System.Windows.Forms.Padding(3);
            this.tabModel.Size = new System.Drawing.Size(407, 344);
            this.tabModel.TabIndex = 0;
            this.tabModel.Text = "Model";
            this.tabModel.UseVisualStyleBackColor = true;
            // 
            // tabMovements
            // 
            this.tabMovements.Controls.Add(this.panel2);
            this.tabMovements.Controls.Add(this.panel1);
            this.tabMovements.Location = new System.Drawing.Point(4, 22);
            this.tabMovements.Name = "tabMovements";
            this.tabMovements.Padding = new System.Windows.Forms.Padding(3);
            this.tabMovements.Size = new System.Drawing.Size(407, 344);
            this.tabMovements.TabIndex = 1;
            this.tabMovements.Text = "Movements";
            this.tabMovements.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pMovements);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(401, 292);
            this.panel2.TabIndex = 1;
            // 
            // pMovements
            // 
            this.pMovements.Location = new System.Drawing.Point(0, 0);
            this.pMovements.Name = "pMovements";
            this.pMovements.Size = new System.Drawing.Size(512, 512);
            this.pMovements.TabIndex = 0;
            this.pMovements.TabStop = false;
            this.pMovements.Paint += new System.Windows.Forms.PaintEventHandler(this.pMovements_Paint);
            this.pMovements.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pMovements_MouseDown);
            this.pMovements.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pMovements_MouseMove);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bMoveColors);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cMovePermission);
            this.panel1.Controls.Add(this.rMoveFlags);
            this.panel1.Controls.Add(this.rMovePermissions);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(401, 46);
            this.panel1.TabIndex = 0;
            // 
            // tabObjects
            // 
            this.tabObjects.Location = new System.Drawing.Point(4, 22);
            this.tabObjects.Name = "tabObjects";
            this.tabObjects.Padding = new System.Windows.Forms.Padding(3);
            this.tabObjects.Size = new System.Drawing.Size(407, 344);
            this.tabObjects.TabIndex = 2;
            this.tabObjects.Text = "Objects";
            this.tabObjects.UseVisualStyleBackColor = true;
            // 
            // tabHeader
            // 
            this.tabHeader.Controls.Add(this.groupBox1);
            this.tabHeader.Location = new System.Drawing.Point(4, 22);
            this.tabHeader.Name = "tabHeader";
            this.tabHeader.Padding = new System.Windows.Forms.Padding(3);
            this.tabHeader.Size = new System.Drawing.Size(427, 382);
            this.tabHeader.TabIndex = 1;
            this.tabHeader.Text = "Header";
            this.tabHeader.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtHObjectTextures);
            this.groupBox1.Controls.Add(this.txtHMapTextures);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 71);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Textures";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Objects:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Map:";
            // 
            // rMovePermissions
            // 
            this.rMovePermissions.AutoSize = true;
            this.rMovePermissions.Checked = true;
            this.rMovePermissions.Location = new System.Drawing.Point(3, 3);
            this.rMovePermissions.Name = "rMovePermissions";
            this.rMovePermissions.Size = new System.Drawing.Size(80, 17);
            this.rMovePermissions.TabIndex = 0;
            this.rMovePermissions.TabStop = true;
            this.rMovePermissions.Text = "Permissions";
            this.rMovePermissions.UseVisualStyleBackColor = true;
            this.rMovePermissions.CheckedChanged += new System.EventHandler(this.rMove_CheckChanged);
            // 
            // rMoveFlags
            // 
            this.rMoveFlags.AutoSize = true;
            this.rMoveFlags.Location = new System.Drawing.Point(89, 3);
            this.rMoveFlags.Name = "rMoveFlags";
            this.rMoveFlags.Size = new System.Drawing.Size(50, 17);
            this.rMoveFlags.TabIndex = 1;
            this.rMoveFlags.Text = "Flags";
            this.rMoveFlags.UseVisualStyleBackColor = true;
            this.rMoveFlags.CheckedChanged += new System.EventHandler(this.rMove_CheckChanged);
            // 
            // cMovePermission
            // 
            this.cMovePermission.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cMovePermission.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cMovePermission.FormattingEnabled = true;
            this.cMovePermission.Location = new System.Drawing.Point(2, 22);
            this.cMovePermission.Name = "cMovePermission";
            this.cMovePermission.Size = new System.Drawing.Size(136, 21);
            this.cMovePermission.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Left on - Right off";
            this.label3.Visible = false;
            // 
            // bMoveColors
            // 
            this.bMoveColors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bMoveColors.Location = new System.Drawing.Point(323, 20);
            this.bMoveColors.Name = "bMoveColors";
            this.bMoveColors.Size = new System.Drawing.Size(75, 23);
            this.bMoveColors.TabIndex = 10;
            this.bMoveColors.Text = "Edit Colors";
            this.bMoveColors.UseVisualStyleBackColor = true;
            this.bMoveColors.Click += new System.EventHandler(this.bMoveColors_Click);
            // 
            // txtHObjectTextures
            // 
            this.txtHObjectTextures.Location = new System.Drawing.Point(58, 45);
            this.txtHObjectTextures.MaximumValue = ((uint)(255u));
            this.txtHObjectTextures.MinimumValue = ((uint)(0u));
            this.txtHObjectTextures.Name = "txtHObjectTextures";
            this.txtHObjectTextures.Size = new System.Drawing.Size(123, 20);
            this.txtHObjectTextures.TabIndex = 3;
            this.txtHObjectTextures.Text = "0";
            this.txtHObjectTextures.Value = ((uint)(0u));
            // 
            // txtHMapTextures
            // 
            this.txtHMapTextures.Location = new System.Drawing.Point(58, 19);
            this.txtHMapTextures.MaximumValue = ((uint)(255u));
            this.txtHMapTextures.MinimumValue = ((uint)(0u));
            this.txtHMapTextures.Name = "txtHMapTextures";
            this.txtHMapTextures.Size = new System.Drawing.Size(123, 20);
            this.txtHMapTextures.TabIndex = 2;
            this.txtHMapTextures.Text = "0";
            this.txtHMapTextures.Value = ((uint)(0u));
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 472);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblROM);
            this.Controls.Add(this.pBanner);
            this.Controls.Add(this.treeMaps);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "DS Map";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBanner)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabMap.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabMovements.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pMovements)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabHeader.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem rOMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bLoadROM;
        private System.Windows.Forms.ToolStripButton bBuildROM;
        private System.Windows.Forms.OpenFileDialog openDialog;
        private System.Windows.Forms.TreeView treeMaps;
        private System.Windows.Forms.Label lblROM;
        private System.Windows.Forms.PictureBox pBanner;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabMap;
        private System.Windows.Forms.TabPage tabHeader;
        private System.Windows.Forms.GroupBox groupBox1;
        private NumericTextBox txtHObjectTextures;
        private NumericTextBox txtHMapTextures;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabModel;
        private System.Windows.Forms.TabPage tabMovements;
        private System.Windows.Forms.TabPage tabObjects;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pMovements;
        private System.Windows.Forms.RadioButton rMovePermissions;
        private System.Windows.Forms.RadioButton rMoveFlags;
        private System.Windows.Forms.ComboBox cMovePermission;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bMoveColors;
    }
}

