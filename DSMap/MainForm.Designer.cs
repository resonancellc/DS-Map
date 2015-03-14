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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMap = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabModel = new System.Windows.Forms.TabPage();
            this.glMapModel = new OpenTK.GLControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bModelImport = new System.Windows.Forms.Button();
            this.bModelExport = new System.Windows.Forms.Button();
            this.tabMovements = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pMovements = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bMoveColors = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cMovePermission = new System.Windows.Forms.ComboBox();
            this.rMoveFlags = new System.Windows.Forms.RadioButton();
            this.rMoveBehaviours = new System.Windows.Forms.RadioButton();
            this.tabObjects = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.bObjRemove = new System.Windows.Forms.Button();
            this.bObjAdd = new System.Windows.Forms.Button();
            this.listObjects = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pObjMap = new System.Windows.Forms.PictureBox();
            this.tabHeader = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pBanner = new System.Windows.Forms.PictureBox();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.txtMapModelName = new System.Windows.Forms.TextBox();
            this.txtObjHeight = new DSMap.NumericTextBox();
            this.txtObjLength = new DSMap.NumericTextBox();
            this.txtObjWidth = new DSMap.NumericTextBox();
            this.txtObjZ = new DSMap.SignedNumericTextBox();
            this.txtObjZFlag = new DSMap.NumericTextBox();
            this.txtObjY = new DSMap.SignedNumericTextBox();
            this.txtObjYFlag = new DSMap.NumericTextBox();
            this.txtObjX = new DSMap.SignedNumericTextBox();
            this.txtObjXFlag = new DSMap.NumericTextBox();
            this.txtObjModel = new DSMap.NumericTextBox();
            this.txtHObjectTextures = new DSMap.NumericTextBox();
            this.txtHMapTextures = new DSMap.NumericTextBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabMap.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabModel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabMovements.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pMovements)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabObjects.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pObjMap)).BeginInit();
            this.tabHeader.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBanner)).BeginInit();
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
            this.menuStrip1.Size = new System.Drawing.Size(792, 24);
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
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
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
            this.toolStrip1.Size = new System.Drawing.Size(792, 25);
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
            this.treeMaps.Size = new System.Drawing.Size(164, 528);
            this.treeMaps.TabIndex = 6;
            this.treeMaps.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeMaps_NodeMouseDoubleClick);
            // 
            // lblROM
            // 
            this.lblROM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblROM.AutoSize = true;
            this.lblROM.Location = new System.Drawing.Point(50, 583);
            this.lblROM.Name = "lblROM";
            this.lblROM.Size = new System.Drawing.Size(77, 13);
            this.lblROM.TabIndex = 8;
            this.lblROM.Text = "Load a ROM...";
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
            this.tabControl1.Size = new System.Drawing.Size(598, 566);
            this.tabControl1.TabIndex = 9;
            // 
            // tabMap
            // 
            this.tabMap.Controls.Add(this.tabControl2);
            this.tabMap.Location = new System.Drawing.Point(4, 22);
            this.tabMap.Name = "tabMap";
            this.tabMap.Padding = new System.Windows.Forms.Padding(3);
            this.tabMap.Size = new System.Drawing.Size(590, 540);
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
            this.tabControl2.Size = new System.Drawing.Size(578, 528);
            this.tabControl2.TabIndex = 0;
            // 
            // tabModel
            // 
            this.tabModel.AutoScroll = true;
            this.tabModel.Controls.Add(this.glMapModel);
            this.tabModel.Controls.Add(this.panel3);
            this.tabModel.Location = new System.Drawing.Point(4, 22);
            this.tabModel.Name = "tabModel";
            this.tabModel.Padding = new System.Windows.Forms.Padding(3);
            this.tabModel.Size = new System.Drawing.Size(570, 502);
            this.tabModel.TabIndex = 0;
            this.tabModel.Text = "Model";
            this.tabModel.UseVisualStyleBackColor = true;
            // 
            // glMapModel
            // 
            this.glMapModel.BackColor = System.Drawing.Color.Black;
            this.glMapModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glMapModel.Location = new System.Drawing.Point(3, 35);
            this.glMapModel.Name = "glMapModel";
            this.glMapModel.Size = new System.Drawing.Size(564, 464);
            this.glMapModel.TabIndex = 3;
            this.glMapModel.VSync = false;
            this.glMapModel.Paint += new System.Windows.Forms.PaintEventHandler(this.glMapModel_Paint);
            this.glMapModel.Resize += new System.EventHandler(this.glMapModel_Resize);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtMapModelName);
            this.panel3.Controls.Add(this.bModelImport);
            this.panel3.Controls.Add(this.bModelExport);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(564, 32);
            this.panel3.TabIndex = 4;
            // 
            // bModelImport
            // 
            this.bModelImport.Location = new System.Drawing.Point(486, 3);
            this.bModelImport.Name = "bModelImport";
            this.bModelImport.Size = new System.Drawing.Size(75, 23);
            this.bModelImport.TabIndex = 1;
            this.bModelImport.Text = "Import";
            this.bModelImport.UseVisualStyleBackColor = true;
            this.bModelImport.Click += new System.EventHandler(this.bModelImport_Click);
            // 
            // bModelExport
            // 
            this.bModelExport.Location = new System.Drawing.Point(405, 3);
            this.bModelExport.Name = "bModelExport";
            this.bModelExport.Size = new System.Drawing.Size(75, 23);
            this.bModelExport.TabIndex = 0;
            this.bModelExport.Text = "Export";
            this.bModelExport.UseVisualStyleBackColor = true;
            this.bModelExport.Click += new System.EventHandler(this.bModelExport_Click);
            // 
            // tabMovements
            // 
            this.tabMovements.Controls.Add(this.panel2);
            this.tabMovements.Controls.Add(this.panel1);
            this.tabMovements.Location = new System.Drawing.Point(4, 22);
            this.tabMovements.Name = "tabMovements";
            this.tabMovements.Padding = new System.Windows.Forms.Padding(3);
            this.tabMovements.Size = new System.Drawing.Size(570, 502);
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
            this.panel2.Size = new System.Drawing.Size(564, 450);
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
            this.panel1.Controls.Add(this.rMoveBehaviours);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(564, 46);
            this.panel1.TabIndex = 0;
            // 
            // bMoveColors
            // 
            this.bMoveColors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bMoveColors.Location = new System.Drawing.Point(486, 20);
            this.bMoveColors.Name = "bMoveColors";
            this.bMoveColors.Size = new System.Drawing.Size(75, 23);
            this.bMoveColors.TabIndex = 10;
            this.bMoveColors.Text = "Edit Colors";
            this.bMoveColors.UseVisualStyleBackColor = true;
            this.bMoveColors.Click += new System.EventHandler(this.bMoveColors_Click);
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
            // rMoveBehaviours
            // 
            this.rMoveBehaviours.AutoSize = true;
            this.rMoveBehaviours.Checked = true;
            this.rMoveBehaviours.Location = new System.Drawing.Point(3, 3);
            this.rMoveBehaviours.Name = "rMoveBehaviours";
            this.rMoveBehaviours.Size = new System.Drawing.Size(78, 17);
            this.rMoveBehaviours.TabIndex = 0;
            this.rMoveBehaviours.TabStop = true;
            this.rMoveBehaviours.Text = "Behaviours";
            this.rMoveBehaviours.UseVisualStyleBackColor = true;
            this.rMoveBehaviours.CheckedChanged += new System.EventHandler(this.rMove_CheckChanged);
            // 
            // tabObjects
            // 
            this.tabObjects.AutoScroll = true;
            this.tabObjects.Controls.Add(this.groupBox4);
            this.tabObjects.Controls.Add(this.groupBox3);
            this.tabObjects.Controls.Add(this.groupBox2);
            this.tabObjects.Location = new System.Drawing.Point(4, 22);
            this.tabObjects.Name = "tabObjects";
            this.tabObjects.Padding = new System.Windows.Forms.Padding(3);
            this.tabObjects.Size = new System.Drawing.Size(570, 502);
            this.tabObjects.TabIndex = 2;
            this.tabObjects.Text = "Objects";
            this.tabObjects.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.bObjRemove);
            this.groupBox4.Controls.Add(this.bObjAdd);
            this.groupBox4.Controls.Add(this.listObjects);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(152, 281);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Objects";
            // 
            // bObjRemove
            // 
            this.bObjRemove.Image = global::DSMap.Properties.Resources.minus;
            this.bObjRemove.Location = new System.Drawing.Point(114, 252);
            this.bObjRemove.Name = "bObjRemove";
            this.bObjRemove.Size = new System.Drawing.Size(32, 23);
            this.bObjRemove.TabIndex = 5;
            this.bObjRemove.UseVisualStyleBackColor = true;
            // 
            // bObjAdd
            // 
            this.bObjAdd.Image = global::DSMap.Properties.Resources.plus;
            this.bObjAdd.Location = new System.Drawing.Point(76, 252);
            this.bObjAdd.Name = "bObjAdd";
            this.bObjAdd.Size = new System.Drawing.Size(32, 23);
            this.bObjAdd.TabIndex = 4;
            this.bObjAdd.UseVisualStyleBackColor = true;
            // 
            // listObjects
            // 
            this.listObjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listObjects.FullRowSelect = true;
            this.listObjects.GridLines = true;
            this.listObjects.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listObjects.HideSelection = false;
            this.listObjects.Location = new System.Drawing.Point(6, 16);
            this.listObjects.Name = "listObjects";
            this.listObjects.Size = new System.Drawing.Size(140, 227);
            this.listObjects.TabIndex = 3;
            this.listObjects.UseCompatibleStateImageBehavior = false;
            this.listObjects.View = System.Windows.Forms.View.Details;
            this.listObjects.SelectedIndexChanged += new System.EventHandler(this.listObjects_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "#";
            this.columnHeader1.Width = 32;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Model";
            this.columnHeader2.Width = 76;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtObjHeight);
            this.groupBox3.Controls.Add(this.txtObjLength);
            this.groupBox3.Controls.Add(this.txtObjWidth);
            this.groupBox3.Controls.Add(this.txtObjZ);
            this.groupBox3.Controls.Add(this.txtObjZFlag);
            this.groupBox3.Controls.Add(this.txtObjY);
            this.groupBox3.Controls.Add(this.txtObjYFlag);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtObjX);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtObjXFlag);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtObjModel);
            this.groupBox3.Location = new System.Drawing.Point(438, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(126, 266);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Object";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 224);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Height:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 185);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Length:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Width:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Flags:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "X, Y, Z:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Model:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pObjMap);
            this.groupBox2.Location = new System.Drawing.Point(164, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 281);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Position";
            // 
            // pObjMap
            // 
            this.pObjMap.Location = new System.Drawing.Point(6, 19);
            this.pObjMap.Name = "pObjMap";
            this.pObjMap.Size = new System.Drawing.Size(256, 256);
            this.pObjMap.TabIndex = 66;
            this.pObjMap.TabStop = false;
            this.pObjMap.Paint += new System.Windows.Forms.PaintEventHandler(this.pObjMap_Paint);
            // 
            // tabHeader
            // 
            this.tabHeader.Controls.Add(this.groupBox1);
            this.tabHeader.Location = new System.Drawing.Point(4, 22);
            this.tabHeader.Name = "tabHeader";
            this.tabHeader.Padding = new System.Windows.Forms.Padding(3);
            this.tabHeader.Size = new System.Drawing.Size(590, 540);
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
            // pBanner
            // 
            this.pBanner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pBanner.Location = new System.Drawing.Point(12, 586);
            this.pBanner.Name = "pBanner";
            this.pBanner.Size = new System.Drawing.Size(32, 32);
            this.pBanner.TabIndex = 7;
            this.pBanner.TabStop = false;
            // 
            // txtMapModelName
            // 
            this.txtMapModelName.Location = new System.Drawing.Point(3, 5);
            this.txtMapModelName.MaxLength = 16;
            this.txtMapModelName.Name = "txtMapModelName";
            this.txtMapModelName.Size = new System.Drawing.Size(120, 20);
            this.txtMapModelName.TabIndex = 2;
            this.txtMapModelName.TextChanged += new System.EventHandler(this.txtMapModelName_TextChanged);
            // 
            // txtObjHeight
            // 
            this.txtObjHeight.Location = new System.Drawing.Point(6, 240);
            this.txtObjHeight.MaximumValue = ((uint)(4294967294u));
            this.txtObjHeight.MinimumValue = ((uint)(0u));
            this.txtObjHeight.Name = "txtObjHeight";
            this.txtObjHeight.Size = new System.Drawing.Size(114, 20);
            this.txtObjHeight.TabIndex = 12;
            this.txtObjHeight.Text = "0";
            this.txtObjHeight.Value = ((uint)(0u));
            this.txtObjHeight.TextChanged += new System.EventHandler(this.txtObjWHL_TextChanged);
            // 
            // txtObjLength
            // 
            this.txtObjLength.Location = new System.Drawing.Point(6, 201);
            this.txtObjLength.MaximumValue = ((uint)(4294967294u));
            this.txtObjLength.MinimumValue = ((uint)(0u));
            this.txtObjLength.Name = "txtObjLength";
            this.txtObjLength.Size = new System.Drawing.Size(114, 20);
            this.txtObjLength.TabIndex = 11;
            this.txtObjLength.Text = "0";
            this.txtObjLength.Value = ((uint)(0u));
            this.txtObjLength.TextChanged += new System.EventHandler(this.txtObjWHL_TextChanged);
            // 
            // txtObjWidth
            // 
            this.txtObjWidth.Location = new System.Drawing.Point(6, 162);
            this.txtObjWidth.MaximumValue = ((uint)(4294967294u));
            this.txtObjWidth.MinimumValue = ((uint)(0u));
            this.txtObjWidth.Name = "txtObjWidth";
            this.txtObjWidth.Size = new System.Drawing.Size(114, 20);
            this.txtObjWidth.TabIndex = 10;
            this.txtObjWidth.Text = "0";
            this.txtObjWidth.Value = ((uint)(0u));
            this.txtObjWidth.TextChanged += new System.EventHandler(this.txtObjWHL_TextChanged);
            // 
            // txtObjZ
            // 
            this.txtObjZ.Location = new System.Drawing.Point(6, 123);
            this.txtObjZ.Name = "txtObjZ";
            this.txtObjZ.Size = new System.Drawing.Size(54, 20);
            this.txtObjZ.TabIndex = 9;
            this.txtObjZ.Text = "0";
            this.txtObjZ.Value = 0;
            this.txtObjZ.TextChanged += new System.EventHandler(this.txtObjXYZ_TextChanged);
            // 
            // txtObjZFlag
            // 
            this.txtObjZFlag.Location = new System.Drawing.Point(66, 123);
            this.txtObjZFlag.MaximumValue = ((uint)(4294967294u));
            this.txtObjZFlag.MinimumValue = ((uint)(0u));
            this.txtObjZFlag.Name = "txtObjZFlag";
            this.txtObjZFlag.NumberStyle = DSMap.NumericTextBox.NumberStyles.Hexadecimal;
            this.txtObjZFlag.Size = new System.Drawing.Size(54, 20);
            this.txtObjZFlag.TabIndex = 8;
            this.txtObjZFlag.Text = "0x0";
            this.txtObjZFlag.Value = ((uint)(0u));
            this.txtObjZFlag.TextChanged += new System.EventHandler(this.txtObjXYZFlags_TextChanged);
            // 
            // txtObjY
            // 
            this.txtObjY.Location = new System.Drawing.Point(6, 97);
            this.txtObjY.Name = "txtObjY";
            this.txtObjY.Size = new System.Drawing.Size(54, 20);
            this.txtObjY.TabIndex = 7;
            this.txtObjY.Text = "0";
            this.txtObjY.Value = 0;
            this.txtObjY.TextChanged += new System.EventHandler(this.txtObjXYZ_TextChanged);
            // 
            // txtObjYFlag
            // 
            this.txtObjYFlag.Location = new System.Drawing.Point(66, 97);
            this.txtObjYFlag.MaximumValue = ((uint)(4294967294u));
            this.txtObjYFlag.MinimumValue = ((uint)(0u));
            this.txtObjYFlag.Name = "txtObjYFlag";
            this.txtObjYFlag.NumberStyle = DSMap.NumericTextBox.NumberStyles.Hexadecimal;
            this.txtObjYFlag.Size = new System.Drawing.Size(54, 20);
            this.txtObjYFlag.TabIndex = 6;
            this.txtObjYFlag.Text = "0x0";
            this.txtObjYFlag.Value = ((uint)(0u));
            this.txtObjYFlag.TextChanged += new System.EventHandler(this.txtObjXYZFlags_TextChanged);
            // 
            // txtObjX
            // 
            this.txtObjX.Location = new System.Drawing.Point(6, 71);
            this.txtObjX.Name = "txtObjX";
            this.txtObjX.Size = new System.Drawing.Size(54, 20);
            this.txtObjX.TabIndex = 4;
            this.txtObjX.Text = "0";
            this.txtObjX.Value = 0;
            this.txtObjX.TextChanged += new System.EventHandler(this.txtObjXYZ_TextChanged);
            // 
            // txtObjXFlag
            // 
            this.txtObjXFlag.Location = new System.Drawing.Point(66, 71);
            this.txtObjXFlag.MaximumValue = ((uint)(4294967294u));
            this.txtObjXFlag.MinimumValue = ((uint)(0u));
            this.txtObjXFlag.Name = "txtObjXFlag";
            this.txtObjXFlag.NumberStyle = DSMap.NumericTextBox.NumberStyles.Hexadecimal;
            this.txtObjXFlag.Size = new System.Drawing.Size(54, 20);
            this.txtObjXFlag.TabIndex = 2;
            this.txtObjXFlag.Text = "0x0";
            this.txtObjXFlag.Value = ((uint)(0u));
            this.txtObjXFlag.TextChanged += new System.EventHandler(this.txtObjXYZFlags_TextChanged);
            // 
            // txtObjModel
            // 
            this.txtObjModel.Location = new System.Drawing.Point(6, 32);
            this.txtObjModel.MaximumValue = ((uint)(4294967294u));
            this.txtObjModel.MinimumValue = ((uint)(0u));
            this.txtObjModel.Name = "txtObjModel";
            this.txtObjModel.Size = new System.Drawing.Size(114, 20);
            this.txtObjModel.TabIndex = 0;
            this.txtObjModel.Text = "0";
            this.txtObjModel.Value = ((uint)(0u));
            this.txtObjModel.TextChanged += new System.EventHandler(this.txtObjModel_TextChanged);
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
            this.ClientSize = new System.Drawing.Size(792, 630);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblROM);
            this.Controls.Add(this.pBanner);
            this.Controls.Add(this.treeMaps);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DS Map";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabMap.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabModel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabMovements.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pMovements)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabObjects.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pObjMap)).EndInit();
            this.tabHeader.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBanner)).EndInit();
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
        private System.Windows.Forms.RadioButton rMoveBehaviours;
        private System.Windows.Forms.RadioButton rMoveFlags;
        private System.Windows.Forms.ComboBox cMovePermission;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bMoveColors;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pObjMap;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private NumericTextBox txtObjModel;
        private NumericTextBox txtObjXFlag;
        private System.Windows.Forms.Label label6;
        private SignedNumericTextBox txtObjX;
        private System.Windows.Forms.Label label5;
        private SignedNumericTextBox txtObjZ;
        private NumericTextBox txtObjZFlag;
        private SignedNumericTextBox txtObjY;
        private NumericTextBox txtObjYFlag;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private NumericTextBox txtObjHeight;
        private NumericTextBox txtObjLength;
        private NumericTextBox txtObjWidth;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button bObjRemove;
        private System.Windows.Forms.Button bObjAdd;
        private System.Windows.Forms.ListView listObjects;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private OpenTK.GLControl glMapModel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button bModelImport;
        private System.Windows.Forms.Button bModelExport;
        private System.Windows.Forms.SaveFileDialog saveDialog;
        private System.Windows.Forms.TextBox txtMapModelName;
    }
}

