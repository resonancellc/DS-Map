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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openScriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveScriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createPatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyPatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bLoadROM = new System.Windows.Forms.ToolStripButton();
            this.bBuildROM = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bSave = new System.Windows.Forms.ToolStripButton();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.treeMaps = new System.Windows.Forms.TreeView();
            this.imageListMaps = new System.Windows.Forms.ImageList(this.components);
            this.lblROM = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMap = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabModel = new System.Windows.Forms.TabPage();
            this.glMapModel = new OpenTK.GLControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtMapModelName = new System.Windows.Forms.TextBox();
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
            this.tabScripts = new System.Windows.Forms.TabPage();
            this.tabControlScripts = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtScripts = new ScintillaNET.Scintilla();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txtFunctions = new ScintillaNET.Scintilla();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.txtMovements = new ScintillaNET.Scintilla();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.bCompile = new System.Windows.Forms.Button();
            this.bTokenize = new System.Windows.Forms.Button();
            this.txtTokens = new System.Windows.Forms.RichTextBox();
            this.tabText = new System.Windows.Forms.TabPage();
            this.txtText = new ScintillaNET.Scintilla();
            this.tabWilds = new System.Windows.Forms.TabPage();
            this.tabControlWilds = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.cWildsRadar3 = new System.Windows.Forms.ComboBox();
            this.cWildsRadar1 = new System.Windows.Forms.ComboBox();
            this.cWildsRadar2 = new System.Windows.Forms.ComboBox();
            this.cWildsRadar0 = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.cWildsEm1 = new System.Windows.Forms.ComboBox();
            this.cWildsEm0 = new System.Windows.Forms.ComboBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.cWildsLeaf1 = new System.Windows.Forms.ComboBox();
            this.cWildsLeaf0 = new System.Windows.Forms.ComboBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.cWildsFire1 = new System.Windows.Forms.ComboBox();
            this.cWildsFire0 = new System.Windows.Forms.ComboBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.cWildsSapp1 = new System.Windows.Forms.ComboBox();
            this.cWildsSapp0 = new System.Windows.Forms.ComboBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.cWildsRuby1 = new System.Windows.Forms.ComboBox();
            this.cWildsRuby0 = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.cWildsNight1 = new System.Windows.Forms.ComboBox();
            this.cWildsNight0 = new System.Windows.Forms.ComboBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.cWildsDay1 = new System.Windows.Forms.ComboBox();
            this.cWildsDay0 = new System.Windows.Forms.ComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.cWildsMorn1 = new System.Windows.Forms.ComboBox();
            this.cWildsMorn0 = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label25 = new System.Windows.Forms.Label();
            this.cWildsWalking11 = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.cWildsWalking10 = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cWildsWalking9 = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.cWildsWalking8 = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cWildsWalking7 = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cWildsWalking6 = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cWildsWalking5 = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cWildsWalking4 = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cWildsWalking3 = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cWildsWalking2 = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cWildsWalking1 = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cWildsWalking0 = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblWildsWalkingRate = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.label56 = new System.Windows.Forms.Label();
            this.cWildsSR4 = new System.Windows.Forms.ComboBox();
            this.label57 = new System.Windows.Forms.Label();
            this.cWildsSR3 = new System.Windows.Forms.ComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.cWildsSR2 = new System.Windows.Forms.ComboBox();
            this.label59 = new System.Windows.Forms.Label();
            this.cWildsSR1 = new System.Windows.Forms.ComboBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label60 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.cWildsSR0 = new System.Windows.Forms.ComboBox();
            this.label62 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.lblWildsSRRate = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.label46 = new System.Windows.Forms.Label();
            this.cWildsGR4 = new System.Windows.Forms.ComboBox();
            this.label47 = new System.Windows.Forms.Label();
            this.cWildsGR3 = new System.Windows.Forms.ComboBox();
            this.label48 = new System.Windows.Forms.Label();
            this.cWildsGR2 = new System.Windows.Forms.ComboBox();
            this.label49 = new System.Windows.Forms.Label();
            this.cWildsGR1 = new System.Windows.Forms.ComboBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label50 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.cWildsGR0 = new System.Windows.Forms.ComboBox();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.lblWildsGRRate = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cWildsOR4 = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.cWildsOR3 = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.cWildsOR2 = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cWildsOR1 = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.cWildsOR0 = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.lblWildsORRate = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.label33 = new System.Windows.Forms.Label();
            this.cWildsSurfing4 = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.cWildsSurfing3 = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.cWildsSurfing2 = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.cWildsSurfing1 = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.cWildsSurfing0 = new System.Windows.Forms.ComboBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.lblWildsSurfingRate = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.lblNoWilds = new System.Windows.Forms.Label();
            this.tabHeader = new System.Windows.Forms.TabPage();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.label73 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.label74 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.bHeaderName = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.txtHeaderName = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.cHeaderName = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bHeaderTex = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.saveDialog = new System.Windows.Forms.SaveFileDialog();
            this.pBanner = new System.Windows.Forms.PictureBox();
            this.scriptsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.functionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.movementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scriptsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.functionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.movementsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.txtWildsWalking11 = new DSMap.SignedNumericTextBox();
            this.txtWildsWalking10 = new DSMap.SignedNumericTextBox();
            this.txtWildsWalking9 = new DSMap.SignedNumericTextBox();
            this.txtWildsWalking8 = new DSMap.SignedNumericTextBox();
            this.txtWildsWalking7 = new DSMap.SignedNumericTextBox();
            this.txtWildsWalking6 = new DSMap.SignedNumericTextBox();
            this.txtWildsWalking5 = new DSMap.SignedNumericTextBox();
            this.txtWildsWalking4 = new DSMap.SignedNumericTextBox();
            this.txtWildsWalking3 = new DSMap.SignedNumericTextBox();
            this.txtWildsWalking2 = new DSMap.SignedNumericTextBox();
            this.txtWildsWalking1 = new DSMap.SignedNumericTextBox();
            this.txtWildsWalking0 = new DSMap.SignedNumericTextBox();
            this.txtWildsWalkingRate = new DSMap.SignedNumericTextBox();
            this.txtWildsSRMax4 = new DSMap.SignedNumericTextBox();
            this.txtWildsSRMax3 = new DSMap.SignedNumericTextBox();
            this.txtWildsSRMax2 = new DSMap.SignedNumericTextBox();
            this.txtWildsSRMax1 = new DSMap.SignedNumericTextBox();
            this.txtWildsSRMax0 = new DSMap.SignedNumericTextBox();
            this.txtWildsSRMin4 = new DSMap.SignedNumericTextBox();
            this.txtWildsSRMin3 = new DSMap.SignedNumericTextBox();
            this.txtWildsSRMin2 = new DSMap.SignedNumericTextBox();
            this.txtWildsSRMin1 = new DSMap.SignedNumericTextBox();
            this.txtWildsSRMin0 = new DSMap.SignedNumericTextBox();
            this.txtWildsSRRate = new DSMap.SignedNumericTextBox();
            this.txtWildsGRMax4 = new DSMap.SignedNumericTextBox();
            this.txtWildsGRMax3 = new DSMap.SignedNumericTextBox();
            this.txtWildsGRMax2 = new DSMap.SignedNumericTextBox();
            this.txtWildsGRMax1 = new DSMap.SignedNumericTextBox();
            this.txtWildsGRMax0 = new DSMap.SignedNumericTextBox();
            this.txtWildsGRMin4 = new DSMap.SignedNumericTextBox();
            this.txtWildsGRMin3 = new DSMap.SignedNumericTextBox();
            this.txtWildsGRMin2 = new DSMap.SignedNumericTextBox();
            this.txtWildsGRMin1 = new DSMap.SignedNumericTextBox();
            this.txtWildsGRMin0 = new DSMap.SignedNumericTextBox();
            this.txtWildsGRRate = new DSMap.SignedNumericTextBox();
            this.txtWildsORMax4 = new DSMap.SignedNumericTextBox();
            this.txtWildsORMax3 = new DSMap.SignedNumericTextBox();
            this.txtWildsORMax2 = new DSMap.SignedNumericTextBox();
            this.txtWildsORMax1 = new DSMap.SignedNumericTextBox();
            this.txtWildsORMax0 = new DSMap.SignedNumericTextBox();
            this.txtWildsORMin4 = new DSMap.SignedNumericTextBox();
            this.txtWildsORMin3 = new DSMap.SignedNumericTextBox();
            this.txtWildsORMin2 = new DSMap.SignedNumericTextBox();
            this.txtWildsORMin1 = new DSMap.SignedNumericTextBox();
            this.txtWildsORMin0 = new DSMap.SignedNumericTextBox();
            this.txtWildsORRate = new DSMap.SignedNumericTextBox();
            this.txtWildsSurfingMax4 = new DSMap.SignedNumericTextBox();
            this.txtWildsSurfingMax3 = new DSMap.SignedNumericTextBox();
            this.txtWildsSurfingMax2 = new DSMap.SignedNumericTextBox();
            this.txtWildsSurfingMax1 = new DSMap.SignedNumericTextBox();
            this.txtWildsSurfingMax0 = new DSMap.SignedNumericTextBox();
            this.txtWildsSurfingMin4 = new DSMap.SignedNumericTextBox();
            this.txtWildsSurfingMin3 = new DSMap.SignedNumericTextBox();
            this.txtWildsSurfingMin2 = new DSMap.SignedNumericTextBox();
            this.txtWildsSurfingMin1 = new DSMap.SignedNumericTextBox();
            this.txtWildsSurfingMin0 = new DSMap.SignedNumericTextBox();
            this.txtWildsSurfingRate = new DSMap.SignedNumericTextBox();
            this.txtHeaderLvlScripts = new DSMap.NumericTextBox();
            this.txtHeaderWildPokemon = new DSMap.NumericTextBox();
            this.txtHeaderMatrix = new DSMap.NumericTextBox();
            this.txtHeaderText = new DSMap.NumericTextBox();
            this.txtHeaderScripts = new DSMap.NumericTextBox();
            this.txtHeaderEvents = new DSMap.NumericTextBox();
            this.txtHeaderFlags = new DSMap.NumericTextBox();
            this.txtHeaderCamera = new DSMap.NumericTextBox();
            this.txtHeaderWeather = new DSMap.NumericTextBox();
            this.txtHeaderMusicNight = new DSMap.NumericTextBox();
            this.txtHeaderMusicDay = new DSMap.NumericTextBox();
            this.txtHeaderNameFrame = new DSMap.NumericTextBox();
            this.txtHeaderNameStyle = new DSMap.NumericTextBox();
            this.txtHeaderObjectTextures = new DSMap.NumericTextBox();
            this.txtHeaderMapTextures = new DSMap.NumericTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tabScripts.SuspendLayout();
            this.tabControlScripts.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabText.SuspendLayout();
            this.tabWilds.SuspendLayout();
            this.tabControlWilds.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.tabHeader.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.scriptsToolStripMenuItem,
            this.patchingToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(792, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadROMToolStripMenuItem,
            this.buildROMToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveAllToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadROMToolStripMenuItem
            // 
            this.loadROMToolStripMenuItem.Image = global::DSMap.Properties.Resources.folder_open;
            this.loadROMToolStripMenuItem.Name = "loadROMToolStripMenuItem";
            this.loadROMToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadROMToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.loadROMToolStripMenuItem.Text = "Load ROM";
            this.loadROMToolStripMenuItem.Click += new System.EventHandler(this.loadROMToolStripMenuItem_Click);
            // 
            // buildROMToolStripMenuItem
            // 
            this.buildROMToolStripMenuItem.Image = global::DSMap.Properties.Resources.compile;
            this.buildROMToolStripMenuItem.Name = "buildROMToolStripMenuItem";
            this.buildROMToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.buildROMToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.buildROMToolStripMenuItem.Text = "Build ROM";
            this.buildROMToolStripMenuItem.Click += new System.EventHandler(this.buildROMToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(170, 6);
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Image = global::DSMap.Properties.Resources.disk_black;
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.saveAllToolStripMenuItem.Text = "Save All";
            this.saveAllToolStripMenuItem.Click += new System.EventHandler(this.saveAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(170, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::DSMap.Properties.Resources.cross;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // scriptsToolStripMenuItem
            // 
            this.scriptsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openScriptsToolStripMenuItem,
            this.saveScriptsToolStripMenuItem,
            this.toolStripSeparator4,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.scriptsToolStripMenuItem.Name = "scriptsToolStripMenuItem";
            this.scriptsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.scriptsToolStripMenuItem.Text = "Scripts";
            // 
            // openScriptsToolStripMenuItem
            // 
            this.openScriptsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scriptsToolStripMenuItem1,
            this.functionsToolStripMenuItem,
            this.movementsToolStripMenuItem});
            this.openScriptsToolStripMenuItem.Name = "openScriptsToolStripMenuItem";
            this.openScriptsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openScriptsToolStripMenuItem.Text = "Open...";
            // 
            // saveScriptsToolStripMenuItem
            // 
            this.saveScriptsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scriptsToolStripMenuItem2,
            this.functionsToolStripMenuItem1,
            this.movementsToolStripMenuItem1});
            this.saveScriptsToolStripMenuItem.Name = "saveScriptsToolStripMenuItem";
            this.saveScriptsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveScriptsToolStripMenuItem.Text = "Save...";
            // 
            // patchingToolStripMenuItem
            // 
            this.patchingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createPatchToolStripMenuItem,
            this.applyPatchToolStripMenuItem});
            this.patchingToolStripMenuItem.Name = "patchingToolStripMenuItem";
            this.patchingToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.patchingToolStripMenuItem.Text = "Patching";
            // 
            // createPatchToolStripMenuItem
            // 
            this.createPatchToolStripMenuItem.Name = "createPatchToolStripMenuItem";
            this.createPatchToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.createPatchToolStripMenuItem.Text = "Create Patch";
            this.createPatchToolStripMenuItem.Click += new System.EventHandler(this.createPatchToolStripMenuItem_Click);
            // 
            // applyPatchToolStripMenuItem
            // 
            this.applyPatchToolStripMenuItem.Name = "applyPatchToolStripMenuItem";
            this.applyPatchToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.applyPatchToolStripMenuItem.Text = "Apply Patch";
            this.applyPatchToolStripMenuItem.Click += new System.EventHandler(this.applyPatchToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::DSMap.Properties.Resources.information;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bLoadROM,
            this.bBuildROM,
            this.toolStripSeparator1,
            this.bSave});
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
            this.bLoadROM.Click += new System.EventHandler(this.loadROMToolStripMenuItem_Click);
            // 
            // bBuildROM
            // 
            this.bBuildROM.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bBuildROM.Image = global::DSMap.Properties.Resources.compile;
            this.bBuildROM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bBuildROM.Name = "bBuildROM";
            this.bBuildROM.Size = new System.Drawing.Size(23, 22);
            this.bBuildROM.Text = "Build ROM";
            this.bBuildROM.Click += new System.EventHandler(this.buildROMToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bSave
            // 
            this.bSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bSave.Image = global::DSMap.Properties.Resources.disk_black;
            this.bSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(23, 22);
            this.bSave.Text = "Save Map";
            this.bSave.Click += new System.EventHandler(this.saveAllToolStripMenuItem_Click);
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
            this.treeMaps.ImageIndex = 0;
            this.treeMaps.ImageList = this.imageListMaps;
            this.treeMaps.Location = new System.Drawing.Point(12, 52);
            this.treeMaps.Name = "treeMaps";
            this.treeMaps.SelectedImageIndex = 0;
            this.treeMaps.Size = new System.Drawing.Size(164, 559);
            this.treeMaps.TabIndex = 6;
            this.treeMaps.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeMaps_NodeMouseDoubleClick);
            // 
            // imageListMaps
            // 
            this.imageListMaps.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMaps.ImageStream")));
            this.imageListMaps.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListMaps.Images.SetKeyName(0, "sitemap.png");
            this.imageListMaps.Images.SetKeyName(1, "map.png");
            // 
            // lblROM
            // 
            this.lblROM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblROM.AutoSize = true;
            this.lblROM.Location = new System.Drawing.Point(50, 614);
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
            this.tabControl1.Controls.Add(this.tabScripts);
            this.tabControl1.Controls.Add(this.tabText);
            this.tabControl1.Controls.Add(this.tabWilds);
            this.tabControl1.Controls.Add(this.tabHeader);
            this.tabControl1.Location = new System.Drawing.Point(182, 52);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(598, 597);
            this.tabControl1.TabIndex = 9;
            // 
            // tabMap
            // 
            this.tabMap.Controls.Add(this.tabControl2);
            this.tabMap.Location = new System.Drawing.Point(4, 22);
            this.tabMap.Name = "tabMap";
            this.tabMap.Padding = new System.Windows.Forms.Padding(3);
            this.tabMap.Size = new System.Drawing.Size(590, 571);
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
            this.tabControl2.Size = new System.Drawing.Size(578, 559);
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
            this.tabModel.Size = new System.Drawing.Size(570, 533);
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
            this.glMapModel.Size = new System.Drawing.Size(564, 495);
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
            // txtMapModelName
            // 
            this.txtMapModelName.Location = new System.Drawing.Point(3, 5);
            this.txtMapModelName.MaxLength = 16;
            this.txtMapModelName.Name = "txtMapModelName";
            this.txtMapModelName.Size = new System.Drawing.Size(120, 20);
            this.txtMapModelName.TabIndex = 2;
            this.txtMapModelName.TextChanged += new System.EventHandler(this.txtMapModelName_TextChanged);
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
            this.tabMovements.Size = new System.Drawing.Size(570, 533);
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
            this.panel2.Size = new System.Drawing.Size(564, 481);
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
            this.tabObjects.Size = new System.Drawing.Size(570, 533);
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
            // tabScripts
            // 
            this.tabScripts.Controls.Add(this.tabControlScripts);
            this.tabScripts.Location = new System.Drawing.Point(4, 22);
            this.tabScripts.Name = "tabScripts";
            this.tabScripts.Size = new System.Drawing.Size(590, 571);
            this.tabScripts.TabIndex = 3;
            this.tabScripts.Text = "Scripts";
            this.tabScripts.UseVisualStyleBackColor = true;
            // 
            // tabControlScripts
            // 
            this.tabControlScripts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlScripts.Controls.Add(this.tabPage1);
            this.tabControlScripts.Controls.Add(this.tabPage4);
            this.tabControlScripts.Controls.Add(this.tabPage6);
            this.tabControlScripts.Controls.Add(this.tabPage5);
            this.tabControlScripts.Location = new System.Drawing.Point(3, 3);
            this.tabControlScripts.Name = "tabControlScripts";
            this.tabControlScripts.SelectedIndex = 0;
            this.tabControlScripts.Size = new System.Drawing.Size(584, 565);
            this.tabControlScripts.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtScripts);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(576, 539);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Scripts";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtScripts
            // 
            this.txtScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScripts.HighlightGuide = 0;
            this.txtScripts.Lexer = ScintillaNET.Lexer.Cpp;
            this.txtScripts.Location = new System.Drawing.Point(3, 3);
            this.txtScripts.Name = "txtScripts";
            this.txtScripts.RectangularSelectionAnchor = 0;
            this.txtScripts.RectangularSelectionAnchorVirtualSpace = 0;
            this.txtScripts.RectangularSelectionCaret = 0;
            this.txtScripts.RectangularSelectionCaretVirtualSpace = 0;
            this.txtScripts.Size = new System.Drawing.Size(570, 533);
            this.txtScripts.TabIndex = 0;
            this.txtScripts.Text = "// Load a Map...";
            this.txtScripts.TextChanged += new System.EventHandler(this.txtScripts_TextChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.txtFunctions);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(576, 539);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Functions";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // txtFunctions
            // 
            this.txtFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFunctions.HighlightGuide = 0;
            this.txtFunctions.Lexer = ScintillaNET.Lexer.Cpp;
            this.txtFunctions.Location = new System.Drawing.Point(3, 3);
            this.txtFunctions.Name = "txtFunctions";
            this.txtFunctions.RectangularSelectionAnchor = 0;
            this.txtFunctions.RectangularSelectionAnchorVirtualSpace = 0;
            this.txtFunctions.RectangularSelectionCaret = 0;
            this.txtFunctions.RectangularSelectionCaretVirtualSpace = 0;
            this.txtFunctions.Size = new System.Drawing.Size(570, 533);
            this.txtFunctions.TabIndex = 1;
            this.txtFunctions.Text = "// Load a Map...";
            this.txtFunctions.TextChanged += new System.EventHandler(this.txtFunctions_TextChanged);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.txtMovements);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(576, 539);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "Movements";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // txtMovements
            // 
            this.txtMovements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMovements.HighlightGuide = 0;
            this.txtMovements.Lexer = ScintillaNET.Lexer.Cpp;
            this.txtMovements.Location = new System.Drawing.Point(3, 3);
            this.txtMovements.Name = "txtMovements";
            this.txtMovements.RectangularSelectionAnchor = 0;
            this.txtMovements.RectangularSelectionAnchorVirtualSpace = 0;
            this.txtMovements.RectangularSelectionCaret = 0;
            this.txtMovements.RectangularSelectionCaretVirtualSpace = 0;
            this.txtMovements.Size = new System.Drawing.Size(570, 533);
            this.txtMovements.TabIndex = 0;
            this.txtMovements.Text = "// Load a Map...";
            this.txtMovements.TextChanged += new System.EventHandler(this.txtMovements_TextChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.bCompile);
            this.tabPage5.Controls.Add(this.bTokenize);
            this.tabPage5.Controls.Add(this.txtTokens);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(576, 539);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Compiler Output";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // bCompile
            // 
            this.bCompile.Location = new System.Drawing.Point(345, 35);
            this.bCompile.Name = "bCompile";
            this.bCompile.Size = new System.Drawing.Size(75, 23);
            this.bCompile.TabIndex = 4;
            this.bCompile.Text = "Compile";
            this.bCompile.UseVisualStyleBackColor = true;
            this.bCompile.Click += new System.EventHandler(this.bCompile_Click);
            // 
            // bTokenize
            // 
            this.bTokenize.Location = new System.Drawing.Point(345, 6);
            this.bTokenize.Name = "bTokenize";
            this.bTokenize.Size = new System.Drawing.Size(75, 23);
            this.bTokenize.TabIndex = 3;
            this.bTokenize.Text = "Tokenize";
            this.bTokenize.UseVisualStyleBackColor = true;
            this.bTokenize.Click += new System.EventHandler(this.bTokenize_Click);
            // 
            // txtTokens
            // 
            this.txtTokens.AcceptsTab = true;
            this.txtTokens.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtTokens.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTokens.ForeColor = System.Drawing.SystemColors.Window;
            this.txtTokens.Location = new System.Drawing.Point(6, 6);
            this.txtTokens.Name = "txtTokens";
            this.txtTokens.Size = new System.Drawing.Size(333, 325);
            this.txtTokens.TabIndex = 2;
            this.txtTokens.Text = "";
            this.txtTokens.WordWrap = false;
            // 
            // tabText
            // 
            this.tabText.Controls.Add(this.txtText);
            this.tabText.Location = new System.Drawing.Point(4, 22);
            this.tabText.Name = "tabText";
            this.tabText.Padding = new System.Windows.Forms.Padding(3);
            this.tabText.Size = new System.Drawing.Size(590, 571);
            this.tabText.TabIndex = 4;
            this.tabText.Text = "Text";
            this.tabText.UseVisualStyleBackColor = true;
            // 
            // txtText
            // 
            this.txtText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtText.HighlightGuide = 0;
            this.txtText.Location = new System.Drawing.Point(3, 3);
            this.txtText.Name = "txtText";
            this.txtText.RectangularSelectionAnchor = 0;
            this.txtText.RectangularSelectionAnchorVirtualSpace = 0;
            this.txtText.RectangularSelectionCaret = 0;
            this.txtText.RectangularSelectionCaretVirtualSpace = 0;
            this.txtText.Size = new System.Drawing.Size(584, 565);
            this.txtText.TabIndex = 0;
            this.txtText.Text = "text0 = Nothing to see here...\\nMove along.";
            this.txtText.TextChanged += new System.EventHandler(this.txtText_TextChanged);
            // 
            // tabWilds
            // 
            this.tabWilds.Controls.Add(this.tabControlWilds);
            this.tabWilds.Controls.Add(this.lblNoWilds);
            this.tabWilds.Location = new System.Drawing.Point(4, 22);
            this.tabWilds.Name = "tabWilds";
            this.tabWilds.Size = new System.Drawing.Size(590, 571);
            this.tabWilds.TabIndex = 2;
            this.tabWilds.Text = "Wild Pokémon";
            this.tabWilds.UseVisualStyleBackColor = true;
            // 
            // tabControlWilds
            // 
            this.tabControlWilds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlWilds.Controls.Add(this.tabPage2);
            this.tabControlWilds.Controls.Add(this.tabPage3);
            this.tabControlWilds.Location = new System.Drawing.Point(6, 3);
            this.tabControlWilds.Name = "tabControlWilds";
            this.tabControlWilds.SelectedIndex = 0;
            this.tabControlWilds.Size = new System.Drawing.Size(581, 565);
            this.tabControlWilds.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.groupBox16);
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(573, 539);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Tall Grass/Cave";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.cWildsRadar3);
            this.groupBox16.Controls.Add(this.cWildsRadar1);
            this.groupBox16.Controls.Add(this.cWildsRadar2);
            this.groupBox16.Controls.Add(this.cWildsRadar0);
            this.groupBox16.Location = new System.Drawing.Point(39, 401);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(133, 127);
            this.groupBox16.TabIndex = 3;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Poké Radar";
            // 
            // cWildsRadar3
            // 
            this.cWildsRadar3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsRadar3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsRadar3.FormattingEnabled = true;
            this.cWildsRadar3.Location = new System.Drawing.Point(6, 100);
            this.cWildsRadar3.Name = "cWildsRadar3";
            this.cWildsRadar3.Size = new System.Drawing.Size(121, 21);
            this.cWildsRadar3.TabIndex = 10;
            this.cWildsRadar3.SelectedIndexChanged += new System.EventHandler(this.cWildsRadar_SelectedIndexChanged);
            // 
            // cWildsRadar1
            // 
            this.cWildsRadar1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsRadar1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsRadar1.FormattingEnabled = true;
            this.cWildsRadar1.Location = new System.Drawing.Point(6, 46);
            this.cWildsRadar1.Name = "cWildsRadar1";
            this.cWildsRadar1.Size = new System.Drawing.Size(121, 21);
            this.cWildsRadar1.TabIndex = 9;
            this.cWildsRadar1.SelectedIndexChanged += new System.EventHandler(this.cWildsRadar_SelectedIndexChanged);
            // 
            // cWildsRadar2
            // 
            this.cWildsRadar2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsRadar2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsRadar2.FormattingEnabled = true;
            this.cWildsRadar2.Location = new System.Drawing.Point(6, 73);
            this.cWildsRadar2.Name = "cWildsRadar2";
            this.cWildsRadar2.Size = new System.Drawing.Size(121, 21);
            this.cWildsRadar2.TabIndex = 8;
            this.cWildsRadar2.SelectedIndexChanged += new System.EventHandler(this.cWildsRadar_SelectedIndexChanged);
            // 
            // cWildsRadar0
            // 
            this.cWildsRadar0.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsRadar0.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsRadar0.FormattingEnabled = true;
            this.cWildsRadar0.Location = new System.Drawing.Point(6, 19);
            this.cWildsRadar0.Name = "cWildsRadar0";
            this.cWildsRadar0.Size = new System.Drawing.Size(121, 21);
            this.cWildsRadar0.TabIndex = 7;
            this.cWildsRadar0.SelectedIndexChanged += new System.EventHandler(this.cWildsRadar_SelectedIndexChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox15);
            this.groupBox7.Controls.Add(this.groupBox14);
            this.groupBox7.Controls.Add(this.groupBox13);
            this.groupBox7.Controls.Add(this.groupBox12);
            this.groupBox7.Controls.Add(this.groupBox11);
            this.groupBox7.Location = new System.Drawing.Point(219, 189);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(284, 256);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Dual-Slot";
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.cWildsEm1);
            this.groupBox15.Controls.Add(this.cWildsEm0);
            this.groupBox15.Location = new System.Drawing.Point(76, 177);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(133, 73);
            this.groupBox15.TabIndex = 5;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Emerald";
            // 
            // cWildsEm1
            // 
            this.cWildsEm1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsEm1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsEm1.FormattingEnabled = true;
            this.cWildsEm1.Location = new System.Drawing.Point(6, 46);
            this.cWildsEm1.Name = "cWildsEm1";
            this.cWildsEm1.Size = new System.Drawing.Size(121, 21);
            this.cWildsEm1.TabIndex = 8;
            this.cWildsEm1.SelectedIndexChanged += new System.EventHandler(this.cWildsDual_SelectedIndexChanged);
            // 
            // cWildsEm0
            // 
            this.cWildsEm0.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsEm0.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsEm0.FormattingEnabled = true;
            this.cWildsEm0.Location = new System.Drawing.Point(6, 19);
            this.cWildsEm0.Name = "cWildsEm0";
            this.cWildsEm0.Size = new System.Drawing.Size(121, 21);
            this.cWildsEm0.TabIndex = 7;
            this.cWildsEm0.SelectedIndexChanged += new System.EventHandler(this.cWildsDual_SelectedIndexChanged);
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.cWildsLeaf1);
            this.groupBox14.Controls.Add(this.cWildsLeaf0);
            this.groupBox14.Location = new System.Drawing.Point(145, 98);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(133, 73);
            this.groupBox14.TabIndex = 4;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "LeafGreen";
            // 
            // cWildsLeaf1
            // 
            this.cWildsLeaf1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsLeaf1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsLeaf1.FormattingEnabled = true;
            this.cWildsLeaf1.Location = new System.Drawing.Point(6, 46);
            this.cWildsLeaf1.Name = "cWildsLeaf1";
            this.cWildsLeaf1.Size = new System.Drawing.Size(121, 21);
            this.cWildsLeaf1.TabIndex = 8;
            this.cWildsLeaf1.SelectedIndexChanged += new System.EventHandler(this.cWildsDual_SelectedIndexChanged);
            // 
            // cWildsLeaf0
            // 
            this.cWildsLeaf0.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsLeaf0.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsLeaf0.FormattingEnabled = true;
            this.cWildsLeaf0.Location = new System.Drawing.Point(6, 19);
            this.cWildsLeaf0.Name = "cWildsLeaf0";
            this.cWildsLeaf0.Size = new System.Drawing.Size(121, 21);
            this.cWildsLeaf0.TabIndex = 7;
            this.cWildsLeaf0.SelectedIndexChanged += new System.EventHandler(this.cWildsDual_SelectedIndexChanged);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.cWildsFire1);
            this.groupBox13.Controls.Add(this.cWildsFire0);
            this.groupBox13.Location = new System.Drawing.Point(6, 98);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(133, 73);
            this.groupBox13.TabIndex = 3;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "FireRed";
            // 
            // cWildsFire1
            // 
            this.cWildsFire1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsFire1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsFire1.FormattingEnabled = true;
            this.cWildsFire1.Location = new System.Drawing.Point(6, 46);
            this.cWildsFire1.Name = "cWildsFire1";
            this.cWildsFire1.Size = new System.Drawing.Size(121, 21);
            this.cWildsFire1.TabIndex = 8;
            this.cWildsFire1.SelectedIndexChanged += new System.EventHandler(this.cWildsDual_SelectedIndexChanged);
            // 
            // cWildsFire0
            // 
            this.cWildsFire0.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsFire0.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsFire0.FormattingEnabled = true;
            this.cWildsFire0.Location = new System.Drawing.Point(6, 19);
            this.cWildsFire0.Name = "cWildsFire0";
            this.cWildsFire0.Size = new System.Drawing.Size(121, 21);
            this.cWildsFire0.TabIndex = 7;
            this.cWildsFire0.SelectedIndexChanged += new System.EventHandler(this.cWildsDual_SelectedIndexChanged);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.cWildsSapp1);
            this.groupBox12.Controls.Add(this.cWildsSapp0);
            this.groupBox12.Location = new System.Drawing.Point(145, 19);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(133, 73);
            this.groupBox12.TabIndex = 2;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Sapphire";
            // 
            // cWildsSapp1
            // 
            this.cWildsSapp1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsSapp1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsSapp1.FormattingEnabled = true;
            this.cWildsSapp1.Location = new System.Drawing.Point(6, 46);
            this.cWildsSapp1.Name = "cWildsSapp1";
            this.cWildsSapp1.Size = new System.Drawing.Size(121, 21);
            this.cWildsSapp1.TabIndex = 8;
            this.cWildsSapp1.SelectedIndexChanged += new System.EventHandler(this.cWildsDual_SelectedIndexChanged);
            // 
            // cWildsSapp0
            // 
            this.cWildsSapp0.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsSapp0.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsSapp0.FormattingEnabled = true;
            this.cWildsSapp0.Location = new System.Drawing.Point(6, 19);
            this.cWildsSapp0.Name = "cWildsSapp0";
            this.cWildsSapp0.Size = new System.Drawing.Size(121, 21);
            this.cWildsSapp0.TabIndex = 7;
            this.cWildsSapp0.SelectedIndexChanged += new System.EventHandler(this.cWildsDual_SelectedIndexChanged);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.cWildsRuby1);
            this.groupBox11.Controls.Add(this.cWildsRuby0);
            this.groupBox11.Location = new System.Drawing.Point(6, 19);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(133, 73);
            this.groupBox11.TabIndex = 1;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Ruby";
            // 
            // cWildsRuby1
            // 
            this.cWildsRuby1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsRuby1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsRuby1.FormattingEnabled = true;
            this.cWildsRuby1.Location = new System.Drawing.Point(6, 46);
            this.cWildsRuby1.Name = "cWildsRuby1";
            this.cWildsRuby1.Size = new System.Drawing.Size(121, 21);
            this.cWildsRuby1.TabIndex = 8;
            this.cWildsRuby1.SelectedIndexChanged += new System.EventHandler(this.cWildsDual_SelectedIndexChanged);
            // 
            // cWildsRuby0
            // 
            this.cWildsRuby0.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsRuby0.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsRuby0.FormattingEnabled = true;
            this.cWildsRuby0.Location = new System.Drawing.Point(6, 19);
            this.cWildsRuby0.Name = "cWildsRuby0";
            this.cWildsRuby0.Size = new System.Drawing.Size(121, 21);
            this.cWildsRuby0.TabIndex = 7;
            this.cWildsRuby0.SelectedIndexChanged += new System.EventHandler(this.cWildsDual_SelectedIndexChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.groupBox10);
            this.groupBox6.Controls.Add(this.groupBox9);
            this.groupBox6.Controls.Add(this.groupBox8);
            this.groupBox6.Location = new System.Drawing.Point(219, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(284, 177);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Time of Day";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.cWildsNight1);
            this.groupBox10.Controls.Add(this.cWildsNight0);
            this.groupBox10.Location = new System.Drawing.Point(76, 98);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(133, 73);
            this.groupBox10.TabIndex = 2;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Night";
            // 
            // cWildsNight1
            // 
            this.cWildsNight1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsNight1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsNight1.FormattingEnabled = true;
            this.cWildsNight1.Location = new System.Drawing.Point(6, 46);
            this.cWildsNight1.Name = "cWildsNight1";
            this.cWildsNight1.Size = new System.Drawing.Size(121, 21);
            this.cWildsNight1.TabIndex = 8;
            this.cWildsNight1.SelectedIndexChanged += new System.EventHandler(this.cWildsTime_SelectedIndexChanged);
            // 
            // cWildsNight0
            // 
            this.cWildsNight0.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsNight0.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsNight0.FormattingEnabled = true;
            this.cWildsNight0.Location = new System.Drawing.Point(6, 19);
            this.cWildsNight0.Name = "cWildsNight0";
            this.cWildsNight0.Size = new System.Drawing.Size(121, 21);
            this.cWildsNight0.TabIndex = 7;
            this.cWildsNight0.SelectedIndexChanged += new System.EventHandler(this.cWildsTime_SelectedIndexChanged);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.cWildsDay1);
            this.groupBox9.Controls.Add(this.cWildsDay0);
            this.groupBox9.Location = new System.Drawing.Point(145, 19);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(133, 73);
            this.groupBox9.TabIndex = 1;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Day";
            // 
            // cWildsDay1
            // 
            this.cWildsDay1.FormattingEnabled = true;
            this.cWildsDay1.Location = new System.Drawing.Point(6, 46);
            this.cWildsDay1.Name = "cWildsDay1";
            this.cWildsDay1.Size = new System.Drawing.Size(121, 21);
            this.cWildsDay1.TabIndex = 8;
            this.cWildsDay1.SelectedIndexChanged += new System.EventHandler(this.cWildsTime_SelectedIndexChanged);
            // 
            // cWildsDay0
            // 
            this.cWildsDay0.FormattingEnabled = true;
            this.cWildsDay0.Location = new System.Drawing.Point(6, 19);
            this.cWildsDay0.Name = "cWildsDay0";
            this.cWildsDay0.Size = new System.Drawing.Size(121, 21);
            this.cWildsDay0.TabIndex = 7;
            this.cWildsDay0.SelectedIndexChanged += new System.EventHandler(this.cWildsTime_SelectedIndexChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.cWildsMorn1);
            this.groupBox8.Controls.Add(this.cWildsMorn0);
            this.groupBox8.Location = new System.Drawing.Point(6, 19);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(133, 73);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Morning";
            // 
            // cWildsMorn1
            // 
            this.cWildsMorn1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsMorn1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsMorn1.FormattingEnabled = true;
            this.cWildsMorn1.Location = new System.Drawing.Point(6, 46);
            this.cWildsMorn1.Name = "cWildsMorn1";
            this.cWildsMorn1.Size = new System.Drawing.Size(121, 21);
            this.cWildsMorn1.TabIndex = 8;
            this.cWildsMorn1.SelectedIndexChanged += new System.EventHandler(this.cWildsTime_SelectedIndexChanged);
            // 
            // cWildsMorn0
            // 
            this.cWildsMorn0.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsMorn0.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsMorn0.FormattingEnabled = true;
            this.cWildsMorn0.Location = new System.Drawing.Point(6, 19);
            this.cWildsMorn0.Name = "cWildsMorn0";
            this.cWildsMorn0.Size = new System.Drawing.Size(121, 21);
            this.cWildsMorn0.TabIndex = 7;
            this.cWildsMorn0.SelectedIndexChanged += new System.EventHandler(this.cWildsTime_SelectedIndexChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label25);
            this.groupBox5.Controls.Add(this.txtWildsWalking11);
            this.groupBox5.Controls.Add(this.cWildsWalking11);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Controls.Add(this.txtWildsWalking10);
            this.groupBox5.Controls.Add(this.cWildsWalking10);
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.txtWildsWalking9);
            this.groupBox5.Controls.Add(this.cWildsWalking9);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.txtWildsWalking8);
            this.groupBox5.Controls.Add(this.cWildsWalking8);
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.txtWildsWalking7);
            this.groupBox5.Controls.Add(this.cWildsWalking7);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.txtWildsWalking6);
            this.groupBox5.Controls.Add(this.cWildsWalking6);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.txtWildsWalking5);
            this.groupBox5.Controls.Add(this.cWildsWalking5);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.txtWildsWalking4);
            this.groupBox5.Controls.Add(this.cWildsWalking4);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.txtWildsWalking3);
            this.groupBox5.Controls.Add(this.cWildsWalking3);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.txtWildsWalking2);
            this.groupBox5.Controls.Add(this.cWildsWalking2);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.txtWildsWalking1);
            this.groupBox5.Controls.Add(this.cWildsWalking1);
            this.groupBox5.Controls.Add(this.panel4);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.txtWildsWalking0);
            this.groupBox5.Controls.Add(this.cWildsWalking0);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.txtWildsWalkingRate);
            this.groupBox5.Controls.Add(this.lblWildsWalkingRate);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(207, 389);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Walking";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 365);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(21, 13);
            this.label25.TabIndex = 43;
            this.label25.Text = "1%";
            // 
            // cWildsWalking11
            // 
            this.cWildsWalking11.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsWalking11.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsWalking11.FormattingEnabled = true;
            this.cWildsWalking11.Location = new System.Drawing.Point(39, 362);
            this.cWildsWalking11.Name = "cWildsWalking11";
            this.cWildsWalking11.Size = new System.Drawing.Size(121, 21);
            this.cWildsWalking11.TabIndex = 41;
            this.cWildsWalking11.SelectedIndexChanged += new System.EventHandler(this.cWildsWalking_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 338);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(21, 13);
            this.label24.TabIndex = 40;
            this.label24.Text = "1%";
            // 
            // cWildsWalking10
            // 
            this.cWildsWalking10.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsWalking10.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsWalking10.FormattingEnabled = true;
            this.cWildsWalking10.Location = new System.Drawing.Point(39, 335);
            this.cWildsWalking10.Name = "cWildsWalking10";
            this.cWildsWalking10.Size = new System.Drawing.Size(121, 21);
            this.cWildsWalking10.TabIndex = 38;
            this.cWildsWalking10.SelectedIndexChanged += new System.EventHandler(this.cWildsWalking_SelectedIndexChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 311);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(21, 13);
            this.label23.TabIndex = 37;
            this.label23.Text = "4%";
            // 
            // cWildsWalking9
            // 
            this.cWildsWalking9.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsWalking9.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsWalking9.FormattingEnabled = true;
            this.cWildsWalking9.Location = new System.Drawing.Point(39, 308);
            this.cWildsWalking9.Name = "cWildsWalking9";
            this.cWildsWalking9.Size = new System.Drawing.Size(121, 21);
            this.cWildsWalking9.TabIndex = 35;
            this.cWildsWalking9.SelectedIndexChanged += new System.EventHandler(this.cWildsWalking_SelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 284);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(21, 13);
            this.label22.TabIndex = 34;
            this.label22.Text = "4%";
            // 
            // cWildsWalking8
            // 
            this.cWildsWalking8.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsWalking8.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsWalking8.FormattingEnabled = true;
            this.cWildsWalking8.Location = new System.Drawing.Point(39, 281);
            this.cWildsWalking8.Name = "cWildsWalking8";
            this.cWildsWalking8.Size = new System.Drawing.Size(121, 21);
            this.cWildsWalking8.TabIndex = 32;
            this.cWildsWalking8.SelectedIndexChanged += new System.EventHandler(this.cWildsWalking_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 257);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(21, 13);
            this.label21.TabIndex = 31;
            this.label21.Text = "5%";
            // 
            // cWildsWalking7
            // 
            this.cWildsWalking7.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsWalking7.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsWalking7.FormattingEnabled = true;
            this.cWildsWalking7.Location = new System.Drawing.Point(39, 254);
            this.cWildsWalking7.Name = "cWildsWalking7";
            this.cWildsWalking7.Size = new System.Drawing.Size(121, 21);
            this.cWildsWalking7.TabIndex = 29;
            this.cWildsWalking7.SelectedIndexChanged += new System.EventHandler(this.cWildsWalking_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 230);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(21, 13);
            this.label20.TabIndex = 28;
            this.label20.Text = "5%";
            // 
            // cWildsWalking6
            // 
            this.cWildsWalking6.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsWalking6.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsWalking6.FormattingEnabled = true;
            this.cWildsWalking6.Location = new System.Drawing.Point(39, 227);
            this.cWildsWalking6.Name = "cWildsWalking6";
            this.cWildsWalking6.Size = new System.Drawing.Size(121, 21);
            this.cWildsWalking6.TabIndex = 26;
            this.cWildsWalking6.SelectedIndexChanged += new System.EventHandler(this.cWildsWalking_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 203);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(27, 13);
            this.label19.TabIndex = 25;
            this.label19.Text = "10%";
            // 
            // cWildsWalking5
            // 
            this.cWildsWalking5.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsWalking5.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsWalking5.FormattingEnabled = true;
            this.cWildsWalking5.Location = new System.Drawing.Point(39, 200);
            this.cWildsWalking5.Name = "cWildsWalking5";
            this.cWildsWalking5.Size = new System.Drawing.Size(121, 21);
            this.cWildsWalking5.TabIndex = 23;
            this.cWildsWalking5.SelectedIndexChanged += new System.EventHandler(this.cWildsWalking_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 176);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(27, 13);
            this.label18.TabIndex = 22;
            this.label18.Text = "10%";
            // 
            // cWildsWalking4
            // 
            this.cWildsWalking4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsWalking4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsWalking4.FormattingEnabled = true;
            this.cWildsWalking4.Location = new System.Drawing.Point(39, 173);
            this.cWildsWalking4.Name = "cWildsWalking4";
            this.cWildsWalking4.Size = new System.Drawing.Size(121, 21);
            this.cWildsWalking4.TabIndex = 20;
            this.cWildsWalking4.SelectedIndexChanged += new System.EventHandler(this.cWildsWalking_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 149);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(27, 13);
            this.label17.TabIndex = 19;
            this.label17.Text = "10%";
            // 
            // cWildsWalking3
            // 
            this.cWildsWalking3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsWalking3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsWalking3.FormattingEnabled = true;
            this.cWildsWalking3.Location = new System.Drawing.Point(39, 146);
            this.cWildsWalking3.Name = "cWildsWalking3";
            this.cWildsWalking3.Size = new System.Drawing.Size(121, 21);
            this.cWildsWalking3.TabIndex = 17;
            this.cWildsWalking3.SelectedIndexChanged += new System.EventHandler(this.cWildsWalking_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 122);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(27, 13);
            this.label16.TabIndex = 16;
            this.label16.Text = "10%";
            // 
            // cWildsWalking2
            // 
            this.cWildsWalking2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsWalking2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsWalking2.FormattingEnabled = true;
            this.cWildsWalking2.Location = new System.Drawing.Point(39, 119);
            this.cWildsWalking2.Name = "cWildsWalking2";
            this.cWildsWalking2.Size = new System.Drawing.Size(121, 21);
            this.cWildsWalking2.TabIndex = 14;
            this.cWildsWalking2.SelectedIndexChanged += new System.EventHandler(this.cWildsWalking_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 95);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 13);
            this.label15.TabIndex = 13;
            this.label15.Text = "20%";
            // 
            // cWildsWalking1
            // 
            this.cWildsWalking1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsWalking1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsWalking1.FormattingEnabled = true;
            this.cWildsWalking1.Location = new System.Drawing.Point(39, 92);
            this.cWildsWalking1.Name = "cWildsWalking1";
            this.cWildsWalking1.Size = new System.Drawing.Size(121, 21);
            this.cWildsWalking1.TabIndex = 11;
            this.cWildsWalking1.SelectedIndexChanged += new System.EventHandler(this.cWildsWalking_SelectedIndexChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel4.Location = new System.Drawing.Point(6, 45);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(195, 1);
            this.panel4.TabIndex = 10;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 49);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(18, 13);
            this.label14.TabIndex = 9;
            this.label14.Text = "%:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 68);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "20%";
            // 
            // cWildsWalking0
            // 
            this.cWildsWalking0.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsWalking0.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsWalking0.FormattingEnabled = true;
            this.cWildsWalking0.Location = new System.Drawing.Point(39, 65);
            this.cWildsWalking0.Name = "cWildsWalking0";
            this.cWildsWalking0.Size = new System.Drawing.Size(121, 21);
            this.cWildsWalking0.TabIndex = 6;
            this.cWildsWalking0.SelectedIndexChanged += new System.EventHandler(this.cWildsWalking_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(163, 49);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Level:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(36, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Pokémon:";
            // 
            // lblWildsWalkingRate
            // 
            this.lblWildsWalkingRate.AutoSize = true;
            this.lblWildsWalkingRate.Location = new System.Drawing.Point(168, 22);
            this.lblWildsWalkingRate.Name = "lblWildsWalkingRate";
            this.lblWildsWalkingRate.Size = new System.Drawing.Size(33, 13);
            this.lblWildsWalkingRate.TabIndex = 2;
            this.lblWildsWalkingRate.Text = "100%";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Encounter Rate:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox20);
            this.tabPage3.Controls.Add(this.groupBox19);
            this.tabPage3.Controls.Add(this.groupBox18);
            this.tabPage3.Controls.Add(this.groupBox17);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(573, 539);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "Water";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.txtWildsSRMax4);
            this.groupBox20.Controls.Add(this.txtWildsSRMax3);
            this.groupBox20.Controls.Add(this.txtWildsSRMax2);
            this.groupBox20.Controls.Add(this.txtWildsSRMax1);
            this.groupBox20.Controls.Add(this.txtWildsSRMax0);
            this.groupBox20.Controls.Add(this.label56);
            this.groupBox20.Controls.Add(this.txtWildsSRMin4);
            this.groupBox20.Controls.Add(this.cWildsSR4);
            this.groupBox20.Controls.Add(this.label57);
            this.groupBox20.Controls.Add(this.txtWildsSRMin3);
            this.groupBox20.Controls.Add(this.cWildsSR3);
            this.groupBox20.Controls.Add(this.label58);
            this.groupBox20.Controls.Add(this.txtWildsSRMin2);
            this.groupBox20.Controls.Add(this.cWildsSR2);
            this.groupBox20.Controls.Add(this.label59);
            this.groupBox20.Controls.Add(this.txtWildsSRMin1);
            this.groupBox20.Controls.Add(this.cWildsSR1);
            this.groupBox20.Controls.Add(this.panel8);
            this.groupBox20.Controls.Add(this.label60);
            this.groupBox20.Controls.Add(this.label61);
            this.groupBox20.Controls.Add(this.txtWildsSRMin0);
            this.groupBox20.Controls.Add(this.cWildsSR0);
            this.groupBox20.Controls.Add(this.label62);
            this.groupBox20.Controls.Add(this.label63);
            this.groupBox20.Controls.Add(this.txtWildsSRRate);
            this.groupBox20.Controls.Add(this.lblWildsSRRate);
            this.groupBox20.Controls.Add(this.label65);
            this.groupBox20.Location = new System.Drawing.Point(260, 212);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(248, 200);
            this.groupBox20.TabIndex = 4;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Super Rod";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(6, 176);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(21, 13);
            this.label56.TabIndex = 22;
            this.label56.Text = "1%";
            // 
            // cWildsSR4
            // 
            this.cWildsSR4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsSR4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsSR4.FormattingEnabled = true;
            this.cWildsSR4.Location = new System.Drawing.Point(39, 173);
            this.cWildsSR4.Name = "cWildsSR4";
            this.cWildsSR4.Size = new System.Drawing.Size(121, 21);
            this.cWildsSR4.TabIndex = 20;
            this.cWildsSR4.SelectedIndexChanged += new System.EventHandler(this.cWildsSR_SelectedIndexChanged);
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(6, 149);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(21, 13);
            this.label57.TabIndex = 19;
            this.label57.Text = "4%";
            // 
            // cWildsSR3
            // 
            this.cWildsSR3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsSR3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsSR3.FormattingEnabled = true;
            this.cWildsSR3.Location = new System.Drawing.Point(39, 146);
            this.cWildsSR3.Name = "cWildsSR3";
            this.cWildsSR3.Size = new System.Drawing.Size(121, 21);
            this.cWildsSR3.TabIndex = 17;
            this.cWildsSR3.SelectedIndexChanged += new System.EventHandler(this.cWildsSR_SelectedIndexChanged);
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(6, 122);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(21, 13);
            this.label58.TabIndex = 16;
            this.label58.Text = "5%";
            // 
            // cWildsSR2
            // 
            this.cWildsSR2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsSR2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsSR2.FormattingEnabled = true;
            this.cWildsSR2.Location = new System.Drawing.Point(39, 119);
            this.cWildsSR2.Name = "cWildsSR2";
            this.cWildsSR2.Size = new System.Drawing.Size(121, 21);
            this.cWildsSR2.TabIndex = 14;
            this.cWildsSR2.SelectedIndexChanged += new System.EventHandler(this.cWildsSR_SelectedIndexChanged);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(6, 95);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(27, 13);
            this.label59.TabIndex = 13;
            this.label59.Text = "30%";
            // 
            // cWildsSR1
            // 
            this.cWildsSR1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsSR1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsSR1.FormattingEnabled = true;
            this.cWildsSR1.Location = new System.Drawing.Point(39, 92);
            this.cWildsSR1.Name = "cWildsSR1";
            this.cWildsSR1.Size = new System.Drawing.Size(121, 21);
            this.cWildsSR1.TabIndex = 11;
            this.cWildsSR1.SelectedIndexChanged += new System.EventHandler(this.cWildsSR_SelectedIndexChanged);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel8.Location = new System.Drawing.Point(6, 45);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(236, 1);
            this.panel8.TabIndex = 10;
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(6, 49);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(18, 13);
            this.label60.TabIndex = 9;
            this.label60.Text = "%:";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(6, 68);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(27, 13);
            this.label61.TabIndex = 8;
            this.label61.Text = "60%";
            // 
            // cWildsSR0
            // 
            this.cWildsSR0.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsSR0.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsSR0.FormattingEnabled = true;
            this.cWildsSR0.Location = new System.Drawing.Point(39, 65);
            this.cWildsSR0.Name = "cWildsSR0";
            this.cWildsSR0.Size = new System.Drawing.Size(121, 21);
            this.cWildsSR0.TabIndex = 6;
            this.cWildsSR0.SelectedIndexChanged += new System.EventHandler(this.cWildsSR_SelectedIndexChanged);
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(163, 49);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(36, 13);
            this.label62.TabIndex = 5;
            this.label62.Text = "Level:";
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(36, 49);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(55, 13);
            this.label63.TabIndex = 4;
            this.label63.Text = "Pokémon:";
            // 
            // lblWildsSRRate
            // 
            this.lblWildsSRRate.AutoSize = true;
            this.lblWildsSRRate.Location = new System.Drawing.Point(204, 22);
            this.lblWildsSRRate.Name = "lblWildsSRRate";
            this.lblWildsSRRate.Size = new System.Drawing.Size(33, 13);
            this.lblWildsSRRate.TabIndex = 2;
            this.lblWildsSRRate.Text = "100%";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(6, 22);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(85, 13);
            this.label65.TabIndex = 1;
            this.label65.Text = "Encounter Rate:";
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.txtWildsGRMax4);
            this.groupBox19.Controls.Add(this.txtWildsGRMax3);
            this.groupBox19.Controls.Add(this.txtWildsGRMax2);
            this.groupBox19.Controls.Add(this.txtWildsGRMax1);
            this.groupBox19.Controls.Add(this.txtWildsGRMax0);
            this.groupBox19.Controls.Add(this.label46);
            this.groupBox19.Controls.Add(this.txtWildsGRMin4);
            this.groupBox19.Controls.Add(this.cWildsGR4);
            this.groupBox19.Controls.Add(this.label47);
            this.groupBox19.Controls.Add(this.txtWildsGRMin3);
            this.groupBox19.Controls.Add(this.cWildsGR3);
            this.groupBox19.Controls.Add(this.label48);
            this.groupBox19.Controls.Add(this.txtWildsGRMin2);
            this.groupBox19.Controls.Add(this.cWildsGR2);
            this.groupBox19.Controls.Add(this.label49);
            this.groupBox19.Controls.Add(this.txtWildsGRMin1);
            this.groupBox19.Controls.Add(this.cWildsGR1);
            this.groupBox19.Controls.Add(this.panel7);
            this.groupBox19.Controls.Add(this.label50);
            this.groupBox19.Controls.Add(this.label51);
            this.groupBox19.Controls.Add(this.txtWildsGRMin0);
            this.groupBox19.Controls.Add(this.cWildsGR0);
            this.groupBox19.Controls.Add(this.label52);
            this.groupBox19.Controls.Add(this.label53);
            this.groupBox19.Controls.Add(this.txtWildsGRRate);
            this.groupBox19.Controls.Add(this.lblWildsGRRate);
            this.groupBox19.Controls.Add(this.label55);
            this.groupBox19.Location = new System.Drawing.Point(6, 212);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(248, 200);
            this.groupBox19.TabIndex = 3;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Good Rod";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(6, 176);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(21, 13);
            this.label46.TabIndex = 22;
            this.label46.Text = "1%";
            // 
            // cWildsGR4
            // 
            this.cWildsGR4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsGR4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsGR4.FormattingEnabled = true;
            this.cWildsGR4.Location = new System.Drawing.Point(39, 173);
            this.cWildsGR4.Name = "cWildsGR4";
            this.cWildsGR4.Size = new System.Drawing.Size(121, 21);
            this.cWildsGR4.TabIndex = 20;
            this.cWildsGR4.SelectedIndexChanged += new System.EventHandler(this.cWildsGR_SelectedIndexChanged);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(6, 149);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(21, 13);
            this.label47.TabIndex = 19;
            this.label47.Text = "4%";
            // 
            // cWildsGR3
            // 
            this.cWildsGR3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsGR3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsGR3.FormattingEnabled = true;
            this.cWildsGR3.Location = new System.Drawing.Point(39, 146);
            this.cWildsGR3.Name = "cWildsGR3";
            this.cWildsGR3.Size = new System.Drawing.Size(121, 21);
            this.cWildsGR3.TabIndex = 17;
            this.cWildsGR3.SelectedIndexChanged += new System.EventHandler(this.cWildsGR_SelectedIndexChanged);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(6, 122);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(21, 13);
            this.label48.TabIndex = 16;
            this.label48.Text = "5%";
            // 
            // cWildsGR2
            // 
            this.cWildsGR2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsGR2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsGR2.FormattingEnabled = true;
            this.cWildsGR2.Location = new System.Drawing.Point(39, 119);
            this.cWildsGR2.Name = "cWildsGR2";
            this.cWildsGR2.Size = new System.Drawing.Size(121, 21);
            this.cWildsGR2.TabIndex = 14;
            this.cWildsGR2.SelectedIndexChanged += new System.EventHandler(this.cWildsGR_SelectedIndexChanged);
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(6, 95);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(27, 13);
            this.label49.TabIndex = 13;
            this.label49.Text = "30%";
            // 
            // cWildsGR1
            // 
            this.cWildsGR1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsGR1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsGR1.FormattingEnabled = true;
            this.cWildsGR1.Location = new System.Drawing.Point(39, 92);
            this.cWildsGR1.Name = "cWildsGR1";
            this.cWildsGR1.Size = new System.Drawing.Size(121, 21);
            this.cWildsGR1.TabIndex = 11;
            this.cWildsGR1.SelectedIndexChanged += new System.EventHandler(this.cWildsGR_SelectedIndexChanged);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel7.Location = new System.Drawing.Point(6, 45);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(236, 1);
            this.panel7.TabIndex = 10;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(6, 49);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(18, 13);
            this.label50.TabIndex = 9;
            this.label50.Text = "%:";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(6, 68);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(27, 13);
            this.label51.TabIndex = 8;
            this.label51.Text = "60%";
            // 
            // cWildsGR0
            // 
            this.cWildsGR0.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsGR0.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsGR0.FormattingEnabled = true;
            this.cWildsGR0.Location = new System.Drawing.Point(39, 65);
            this.cWildsGR0.Name = "cWildsGR0";
            this.cWildsGR0.Size = new System.Drawing.Size(121, 21);
            this.cWildsGR0.TabIndex = 6;
            this.cWildsGR0.SelectedIndexChanged += new System.EventHandler(this.cWildsGR_SelectedIndexChanged);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(163, 49);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(36, 13);
            this.label52.TabIndex = 5;
            this.label52.Text = "Level:";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(36, 49);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(55, 13);
            this.label53.TabIndex = 4;
            this.label53.Text = "Pokémon:";
            // 
            // lblWildsGRRate
            // 
            this.lblWildsGRRate.AutoSize = true;
            this.lblWildsGRRate.Location = new System.Drawing.Point(204, 22);
            this.lblWildsGRRate.Name = "lblWildsGRRate";
            this.lblWildsGRRate.Size = new System.Drawing.Size(33, 13);
            this.lblWildsGRRate.TabIndex = 2;
            this.lblWildsGRRate.Text = "100%";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(6, 22);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(85, 13);
            this.label55.TabIndex = 1;
            this.label55.Text = "Encounter Rate:";
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.txtWildsORMax4);
            this.groupBox18.Controls.Add(this.txtWildsORMax3);
            this.groupBox18.Controls.Add(this.txtWildsORMax2);
            this.groupBox18.Controls.Add(this.txtWildsORMax1);
            this.groupBox18.Controls.Add(this.txtWildsORMax0);
            this.groupBox18.Controls.Add(this.label26);
            this.groupBox18.Controls.Add(this.txtWildsORMin4);
            this.groupBox18.Controls.Add(this.cWildsOR4);
            this.groupBox18.Controls.Add(this.label27);
            this.groupBox18.Controls.Add(this.txtWildsORMin3);
            this.groupBox18.Controls.Add(this.cWildsOR3);
            this.groupBox18.Controls.Add(this.label28);
            this.groupBox18.Controls.Add(this.txtWildsORMin2);
            this.groupBox18.Controls.Add(this.cWildsOR2);
            this.groupBox18.Controls.Add(this.label29);
            this.groupBox18.Controls.Add(this.txtWildsORMin1);
            this.groupBox18.Controls.Add(this.cWildsOR1);
            this.groupBox18.Controls.Add(this.panel6);
            this.groupBox18.Controls.Add(this.label30);
            this.groupBox18.Controls.Add(this.label31);
            this.groupBox18.Controls.Add(this.txtWildsORMin0);
            this.groupBox18.Controls.Add(this.cWildsOR0);
            this.groupBox18.Controls.Add(this.label32);
            this.groupBox18.Controls.Add(this.label43);
            this.groupBox18.Controls.Add(this.txtWildsORRate);
            this.groupBox18.Controls.Add(this.lblWildsORRate);
            this.groupBox18.Controls.Add(this.label45);
            this.groupBox18.Location = new System.Drawing.Point(260, 6);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(248, 200);
            this.groupBox18.TabIndex = 2;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Old Rod";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 176);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(21, 13);
            this.label26.TabIndex = 22;
            this.label26.Text = "1%";
            // 
            // cWildsOR4
            // 
            this.cWildsOR4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsOR4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsOR4.FormattingEnabled = true;
            this.cWildsOR4.Location = new System.Drawing.Point(39, 173);
            this.cWildsOR4.Name = "cWildsOR4";
            this.cWildsOR4.Size = new System.Drawing.Size(121, 21);
            this.cWildsOR4.TabIndex = 20;
            this.cWildsOR4.SelectedIndexChanged += new System.EventHandler(this.cWildsOR_SelectedIndexChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 149);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(21, 13);
            this.label27.TabIndex = 19;
            this.label27.Text = "4%";
            // 
            // cWildsOR3
            // 
            this.cWildsOR3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsOR3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsOR3.FormattingEnabled = true;
            this.cWildsOR3.Location = new System.Drawing.Point(39, 146);
            this.cWildsOR3.Name = "cWildsOR3";
            this.cWildsOR3.Size = new System.Drawing.Size(121, 21);
            this.cWildsOR3.TabIndex = 17;
            this.cWildsOR3.SelectedIndexChanged += new System.EventHandler(this.cWildsOR_SelectedIndexChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(6, 122);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(21, 13);
            this.label28.TabIndex = 16;
            this.label28.Text = "5%";
            // 
            // cWildsOR2
            // 
            this.cWildsOR2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsOR2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsOR2.FormattingEnabled = true;
            this.cWildsOR2.Location = new System.Drawing.Point(39, 119);
            this.cWildsOR2.Name = "cWildsOR2";
            this.cWildsOR2.Size = new System.Drawing.Size(121, 21);
            this.cWildsOR2.TabIndex = 14;
            this.cWildsOR2.SelectedIndexChanged += new System.EventHandler(this.cWildsOR_SelectedIndexChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 95);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(27, 13);
            this.label29.TabIndex = 13;
            this.label29.Text = "30%";
            // 
            // cWildsOR1
            // 
            this.cWildsOR1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsOR1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsOR1.FormattingEnabled = true;
            this.cWildsOR1.Location = new System.Drawing.Point(39, 92);
            this.cWildsOR1.Name = "cWildsOR1";
            this.cWildsOR1.Size = new System.Drawing.Size(121, 21);
            this.cWildsOR1.TabIndex = 11;
            this.cWildsOR1.SelectedIndexChanged += new System.EventHandler(this.cWildsOR_SelectedIndexChanged);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel6.Location = new System.Drawing.Point(6, 45);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(236, 1);
            this.panel6.TabIndex = 10;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 49);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(18, 13);
            this.label30.TabIndex = 9;
            this.label30.Text = "%:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(6, 68);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(27, 13);
            this.label31.TabIndex = 8;
            this.label31.Text = "60%";
            // 
            // cWildsOR0
            // 
            this.cWildsOR0.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsOR0.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsOR0.FormattingEnabled = true;
            this.cWildsOR0.Location = new System.Drawing.Point(39, 65);
            this.cWildsOR0.Name = "cWildsOR0";
            this.cWildsOR0.Size = new System.Drawing.Size(121, 21);
            this.cWildsOR0.TabIndex = 6;
            this.cWildsOR0.SelectedIndexChanged += new System.EventHandler(this.cWildsOR_SelectedIndexChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(163, 49);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(36, 13);
            this.label32.TabIndex = 5;
            this.label32.Text = "Level:";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(36, 49);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(55, 13);
            this.label43.TabIndex = 4;
            this.label43.Text = "Pokémon:";
            // 
            // lblWildsORRate
            // 
            this.lblWildsORRate.AutoSize = true;
            this.lblWildsORRate.Location = new System.Drawing.Point(204, 22);
            this.lblWildsORRate.Name = "lblWildsORRate";
            this.lblWildsORRate.Size = new System.Drawing.Size(33, 13);
            this.lblWildsORRate.TabIndex = 2;
            this.lblWildsORRate.Text = "100%";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(6, 22);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(85, 13);
            this.label45.TabIndex = 1;
            this.label45.Text = "Encounter Rate:";
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.txtWildsSurfingMax4);
            this.groupBox17.Controls.Add(this.txtWildsSurfingMax3);
            this.groupBox17.Controls.Add(this.txtWildsSurfingMax2);
            this.groupBox17.Controls.Add(this.txtWildsSurfingMax1);
            this.groupBox17.Controls.Add(this.txtWildsSurfingMax0);
            this.groupBox17.Controls.Add(this.label33);
            this.groupBox17.Controls.Add(this.txtWildsSurfingMin4);
            this.groupBox17.Controls.Add(this.cWildsSurfing4);
            this.groupBox17.Controls.Add(this.label34);
            this.groupBox17.Controls.Add(this.txtWildsSurfingMin3);
            this.groupBox17.Controls.Add(this.cWildsSurfing3);
            this.groupBox17.Controls.Add(this.label35);
            this.groupBox17.Controls.Add(this.txtWildsSurfingMin2);
            this.groupBox17.Controls.Add(this.cWildsSurfing2);
            this.groupBox17.Controls.Add(this.label36);
            this.groupBox17.Controls.Add(this.txtWildsSurfingMin1);
            this.groupBox17.Controls.Add(this.cWildsSurfing1);
            this.groupBox17.Controls.Add(this.panel5);
            this.groupBox17.Controls.Add(this.label37);
            this.groupBox17.Controls.Add(this.label38);
            this.groupBox17.Controls.Add(this.txtWildsSurfingMin0);
            this.groupBox17.Controls.Add(this.cWildsSurfing0);
            this.groupBox17.Controls.Add(this.label39);
            this.groupBox17.Controls.Add(this.label40);
            this.groupBox17.Controls.Add(this.txtWildsSurfingRate);
            this.groupBox17.Controls.Add(this.lblWildsSurfingRate);
            this.groupBox17.Controls.Add(this.label42);
            this.groupBox17.Location = new System.Drawing.Point(6, 6);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(248, 200);
            this.groupBox17.TabIndex = 1;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Surfing";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 176);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(21, 13);
            this.label33.TabIndex = 22;
            this.label33.Text = "1%";
            // 
            // cWildsSurfing4
            // 
            this.cWildsSurfing4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsSurfing4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsSurfing4.FormattingEnabled = true;
            this.cWildsSurfing4.Location = new System.Drawing.Point(39, 173);
            this.cWildsSurfing4.Name = "cWildsSurfing4";
            this.cWildsSurfing4.Size = new System.Drawing.Size(121, 21);
            this.cWildsSurfing4.TabIndex = 20;
            this.cWildsSurfing4.SelectedIndexChanged += new System.EventHandler(this.cWildsSurfing_SelectedIndexChanged);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(6, 149);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(21, 13);
            this.label34.TabIndex = 19;
            this.label34.Text = "4%";
            // 
            // cWildsSurfing3
            // 
            this.cWildsSurfing3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsSurfing3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsSurfing3.FormattingEnabled = true;
            this.cWildsSurfing3.Location = new System.Drawing.Point(39, 146);
            this.cWildsSurfing3.Name = "cWildsSurfing3";
            this.cWildsSurfing3.Size = new System.Drawing.Size(121, 21);
            this.cWildsSurfing3.TabIndex = 17;
            this.cWildsSurfing3.SelectedIndexChanged += new System.EventHandler(this.cWildsSurfing_SelectedIndexChanged);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(6, 122);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(21, 13);
            this.label35.TabIndex = 16;
            this.label35.Text = "5%";
            // 
            // cWildsSurfing2
            // 
            this.cWildsSurfing2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsSurfing2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsSurfing2.FormattingEnabled = true;
            this.cWildsSurfing2.Location = new System.Drawing.Point(39, 119);
            this.cWildsSurfing2.Name = "cWildsSurfing2";
            this.cWildsSurfing2.Size = new System.Drawing.Size(121, 21);
            this.cWildsSurfing2.TabIndex = 14;
            this.cWildsSurfing2.SelectedIndexChanged += new System.EventHandler(this.cWildsSurfing_SelectedIndexChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(6, 95);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(27, 13);
            this.label36.TabIndex = 13;
            this.label36.Text = "30%";
            // 
            // cWildsSurfing1
            // 
            this.cWildsSurfing1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsSurfing1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsSurfing1.FormattingEnabled = true;
            this.cWildsSurfing1.Location = new System.Drawing.Point(39, 92);
            this.cWildsSurfing1.Name = "cWildsSurfing1";
            this.cWildsSurfing1.Size = new System.Drawing.Size(121, 21);
            this.cWildsSurfing1.TabIndex = 11;
            this.cWildsSurfing1.SelectedIndexChanged += new System.EventHandler(this.cWildsSurfing_SelectedIndexChanged);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel5.Location = new System.Drawing.Point(6, 45);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(236, 1);
            this.panel5.TabIndex = 10;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(6, 49);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(18, 13);
            this.label37.TabIndex = 9;
            this.label37.Text = "%:";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(6, 68);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(27, 13);
            this.label38.TabIndex = 8;
            this.label38.Text = "60%";
            // 
            // cWildsSurfing0
            // 
            this.cWildsSurfing0.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cWildsSurfing0.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cWildsSurfing0.FormattingEnabled = true;
            this.cWildsSurfing0.Location = new System.Drawing.Point(39, 65);
            this.cWildsSurfing0.Name = "cWildsSurfing0";
            this.cWildsSurfing0.Size = new System.Drawing.Size(121, 21);
            this.cWildsSurfing0.TabIndex = 6;
            this.cWildsSurfing0.SelectedIndexChanged += new System.EventHandler(this.cWildsSurfing_SelectedIndexChanged);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(163, 49);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(36, 13);
            this.label39.TabIndex = 5;
            this.label39.Text = "Level:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(36, 49);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(55, 13);
            this.label40.TabIndex = 4;
            this.label40.Text = "Pokémon:";
            // 
            // lblWildsSurfingRate
            // 
            this.lblWildsSurfingRate.AutoSize = true;
            this.lblWildsSurfingRate.Location = new System.Drawing.Point(204, 22);
            this.lblWildsSurfingRate.Name = "lblWildsSurfingRate";
            this.lblWildsSurfingRate.Size = new System.Drawing.Size(33, 13);
            this.lblWildsSurfingRate.TabIndex = 2;
            this.lblWildsSurfingRate.Text = "100%";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(6, 22);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(85, 13);
            this.label42.TabIndex = 1;
            this.label42.Text = "Encounter Rate:";
            // 
            // lblNoWilds
            // 
            this.lblNoWilds.AutoSize = true;
            this.lblNoWilds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoWilds.Location = new System.Drawing.Point(3, 0);
            this.lblNoWilds.Name = "lblNoWilds";
            this.lblNoWilds.Size = new System.Drawing.Size(309, 39);
            this.lblNoWilds.TabIndex = 0;
            this.lblNoWilds.Text = "There are no Wild Pokémon on this map!\r\n\r\nNote: Expect being able to add them in " +
    "the future! :D";
            // 
            // tabHeader
            // 
            this.tabHeader.AutoScroll = true;
            this.tabHeader.Controls.Add(this.groupBox23);
            this.tabHeader.Controls.Add(this.groupBox22);
            this.tabHeader.Controls.Add(this.groupBox21);
            this.tabHeader.Controls.Add(this.groupBox1);
            this.tabHeader.Location = new System.Drawing.Point(4, 22);
            this.tabHeader.Name = "tabHeader";
            this.tabHeader.Padding = new System.Windows.Forms.Padding(3);
            this.tabHeader.Size = new System.Drawing.Size(590, 571);
            this.tabHeader.TabIndex = 1;
            this.tabHeader.Text = "Header";
            this.tabHeader.UseVisualStyleBackColor = true;
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.txtHeaderLvlScripts);
            this.groupBox23.Controls.Add(this.label73);
            this.groupBox23.Controls.Add(this.txtHeaderWildPokemon);
            this.groupBox23.Controls.Add(this.label72);
            this.groupBox23.Controls.Add(this.txtHeaderMatrix);
            this.groupBox23.Controls.Add(this.label71);
            this.groupBox23.Controls.Add(this.txtHeaderText);
            this.groupBox23.Controls.Add(this.label70);
            this.groupBox23.Controls.Add(this.txtHeaderScripts);
            this.groupBox23.Controls.Add(this.label69);
            this.groupBox23.Controls.Add(this.txtHeaderEvents);
            this.groupBox23.Controls.Add(this.label68);
            this.groupBox23.Location = new System.Drawing.Point(6, 278);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(401, 175);
            this.groupBox23.TabIndex = 3;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Files";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(3, 94);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(71, 13);
            this.label73.TabIndex = 17;
            this.label73.Text = "Level Scripts:";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(204, 55);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(79, 13);
            this.label72.TabIndex = 15;
            this.label72.Text = "Wild Pokémon:";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(204, 16);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(38, 13);
            this.label71.TabIndex = 13;
            this.label71.Text = "Matrix:";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(3, 133);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(31, 13);
            this.label70.TabIndex = 11;
            this.label70.Text = "Text:";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(3, 55);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(39, 13);
            this.label69.TabIndex = 9;
            this.label69.Text = "Scripts";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Location = new System.Drawing.Point(3, 16);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(43, 13);
            this.label68.TabIndex = 7;
            this.label68.Text = "Events:";
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.txtHeaderFlags);
            this.groupBox22.Controls.Add(this.label74);
            this.groupBox22.Controls.Add(this.txtHeaderCamera);
            this.groupBox22.Controls.Add(this.label67);
            this.groupBox22.Controls.Add(this.txtHeaderWeather);
            this.groupBox22.Controls.Add(this.label66);
            this.groupBox22.Controls.Add(this.txtHeaderMusicNight);
            this.groupBox22.Controls.Add(this.label64);
            this.groupBox22.Controls.Add(this.txtHeaderMusicDay);
            this.groupBox22.Controls.Add(this.label54);
            this.groupBox22.Location = new System.Drawing.Point(6, 136);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(401, 136);
            this.groupBox22.TabIndex = 2;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "Options";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(3, 94);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(35, 13);
            this.label74.TabIndex = 15;
            this.label74.Text = "Flags:";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(3, 55);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(46, 13);
            this.label67.TabIndex = 11;
            this.label67.Text = "Camera:";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(204, 55);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(51, 13);
            this.label66.TabIndex = 9;
            this.label66.Text = "Weather:";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(204, 16);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(72, 13);
            this.label64.TabIndex = 7;
            this.label64.Text = "Music (Night):";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(3, 16);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(66, 13);
            this.label54.TabIndex = 5;
            this.label54.Text = "Music (Day):";
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.bHeaderName);
            this.groupBox21.Controls.Add(this.panel9);
            this.groupBox21.Controls.Add(this.txtHeaderName);
            this.groupBox21.Controls.Add(this.txtHeaderNameFrame);
            this.groupBox21.Controls.Add(this.label44);
            this.groupBox21.Controls.Add(this.txtHeaderNameStyle);
            this.groupBox21.Controls.Add(this.label41);
            this.groupBox21.Controls.Add(this.cHeaderName);
            this.groupBox21.Location = new System.Drawing.Point(6, 6);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(401, 124);
            this.groupBox21.TabIndex = 1;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Name";
            // 
            // bHeaderName
            // 
            this.bHeaderName.Location = new System.Drawing.Point(207, 45);
            this.bHeaderName.Name = "bHeaderName";
            this.bHeaderName.Size = new System.Drawing.Size(188, 23);
            this.bHeaderName.TabIndex = 7;
            this.bHeaderName.Text = "Modify Name";
            this.bHeaderName.UseVisualStyleBackColor = true;
            this.bHeaderName.Click += new System.EventHandler(this.bHeaderName_Click);
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel9.Location = new System.Drawing.Point(200, 19);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1, 99);
            this.panel9.TabIndex = 6;
            // 
            // txtHeaderName
            // 
            this.txtHeaderName.Location = new System.Drawing.Point(207, 19);
            this.txtHeaderName.Name = "txtHeaderName";
            this.txtHeaderName.Size = new System.Drawing.Size(188, 20);
            this.txtHeaderName.TabIndex = 5;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(3, 82);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(39, 13);
            this.label44.TabIndex = 3;
            this.label44.Text = "Frame:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(3, 43);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(33, 13);
            this.label41.TabIndex = 1;
            this.label41.Text = "Style:";
            // 
            // cHeaderName
            // 
            this.cHeaderName.FormattingEnabled = true;
            this.cHeaderName.Location = new System.Drawing.Point(6, 19);
            this.cHeaderName.Name = "cHeaderName";
            this.cHeaderName.Size = new System.Drawing.Size(188, 21);
            this.cHeaderName.TabIndex = 0;
            this.cHeaderName.SelectedIndexChanged += new System.EventHandler(this.cHeaderName_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bHeaderTex);
            this.groupBox1.Controls.Add(this.txtHeaderObjectTextures);
            this.groupBox1.Controls.Add(this.txtHeaderMapTextures);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 459);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(401, 87);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Textures";
            // 
            // bHeaderTex
            // 
            this.bHeaderTex.Location = new System.Drawing.Point(6, 58);
            this.bHeaderTex.Name = "bHeaderTex";
            this.bHeaderTex.Size = new System.Drawing.Size(188, 23);
            this.bHeaderTex.TabIndex = 8;
            this.bHeaderTex.Text = "Modify and Reload Map";
            this.bHeaderTex.UseVisualStyleBackColor = true;
            this.bHeaderTex.Click += new System.EventHandler(this.bHeaderTex_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Objects:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Map:";
            // 
            // pBanner
            // 
            this.pBanner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pBanner.Location = new System.Drawing.Point(12, 617);
            this.pBanner.Name = "pBanner";
            this.pBanner.Size = new System.Drawing.Size(32, 32);
            this.pBanner.TabIndex = 7;
            this.pBanner.TabStop = false;
            // 
            // scriptsToolStripMenuItem1
            // 
            this.scriptsToolStripMenuItem1.Name = "scriptsToolStripMenuItem1";
            this.scriptsToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.scriptsToolStripMenuItem1.Text = "Scripts";
            this.scriptsToolStripMenuItem1.Click += new System.EventHandler(this.scriptsToolStripMenuItem1_Click);
            // 
            // functionsToolStripMenuItem
            // 
            this.functionsToolStripMenuItem.Name = "functionsToolStripMenuItem";
            this.functionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.functionsToolStripMenuItem.Text = "Functions";
            this.functionsToolStripMenuItem.Click += new System.EventHandler(this.functionsToolStripMenuItem_Click);
            // 
            // movementsToolStripMenuItem
            // 
            this.movementsToolStripMenuItem.Name = "movementsToolStripMenuItem";
            this.movementsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.movementsToolStripMenuItem.Text = "Movements";
            this.movementsToolStripMenuItem.Click += new System.EventHandler(this.movementsToolStripMenuItem_Click);
            // 
            // scriptsToolStripMenuItem2
            // 
            this.scriptsToolStripMenuItem2.Name = "scriptsToolStripMenuItem2";
            this.scriptsToolStripMenuItem2.Size = new System.Drawing.Size(137, 22);
            this.scriptsToolStripMenuItem2.Text = "Scripts";
            this.scriptsToolStripMenuItem2.Click += new System.EventHandler(this.scriptsToolStripMenuItem2_Click);
            // 
            // functionsToolStripMenuItem1
            // 
            this.functionsToolStripMenuItem1.Name = "functionsToolStripMenuItem1";
            this.functionsToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.functionsToolStripMenuItem1.Text = "Functions";
            this.functionsToolStripMenuItem1.Click += new System.EventHandler(this.functionsToolStripMenuItem1_Click);
            // 
            // movementsToolStripMenuItem1
            // 
            this.movementsToolStripMenuItem1.Name = "movementsToolStripMenuItem1";
            this.movementsToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.movementsToolStripMenuItem1.Text = "Movements";
            this.movementsToolStripMenuItem1.Click += new System.EventHandler(this.movementsToolStripMenuItem1_Click);
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
            // txtWildsWalking11
            // 
            this.txtWildsWalking11.Location = new System.Drawing.Point(166, 362);
            this.txtWildsWalking11.MaxValue = 255;
            this.txtWildsWalking11.MinValue = 0;
            this.txtWildsWalking11.Name = "txtWildsWalking11";
            this.txtWildsWalking11.Size = new System.Drawing.Size(35, 20);
            this.txtWildsWalking11.TabIndex = 42;
            this.txtWildsWalking11.Text = "0";
            this.txtWildsWalking11.Value = 0;
            this.txtWildsWalking11.TextChanged += new System.EventHandler(this.txtWildsWalking_TextChanged);
            // 
            // txtWildsWalking10
            // 
            this.txtWildsWalking10.Location = new System.Drawing.Point(166, 335);
            this.txtWildsWalking10.MaxValue = 255;
            this.txtWildsWalking10.MinValue = 0;
            this.txtWildsWalking10.Name = "txtWildsWalking10";
            this.txtWildsWalking10.Size = new System.Drawing.Size(35, 20);
            this.txtWildsWalking10.TabIndex = 39;
            this.txtWildsWalking10.Text = "0";
            this.txtWildsWalking10.Value = 0;
            this.txtWildsWalking10.TextChanged += new System.EventHandler(this.txtWildsWalking_TextChanged);
            // 
            // txtWildsWalking9
            // 
            this.txtWildsWalking9.Location = new System.Drawing.Point(166, 308);
            this.txtWildsWalking9.MaxValue = 255;
            this.txtWildsWalking9.MinValue = 0;
            this.txtWildsWalking9.Name = "txtWildsWalking9";
            this.txtWildsWalking9.Size = new System.Drawing.Size(35, 20);
            this.txtWildsWalking9.TabIndex = 36;
            this.txtWildsWalking9.Text = "0";
            this.txtWildsWalking9.Value = 0;
            this.txtWildsWalking9.TextChanged += new System.EventHandler(this.txtWildsWalking_TextChanged);
            // 
            // txtWildsWalking8
            // 
            this.txtWildsWalking8.Location = new System.Drawing.Point(166, 281);
            this.txtWildsWalking8.MaxValue = 255;
            this.txtWildsWalking8.MinValue = 0;
            this.txtWildsWalking8.Name = "txtWildsWalking8";
            this.txtWildsWalking8.Size = new System.Drawing.Size(35, 20);
            this.txtWildsWalking8.TabIndex = 33;
            this.txtWildsWalking8.Text = "0";
            this.txtWildsWalking8.Value = 0;
            this.txtWildsWalking8.TextChanged += new System.EventHandler(this.txtWildsWalking_TextChanged);
            // 
            // txtWildsWalking7
            // 
            this.txtWildsWalking7.Location = new System.Drawing.Point(166, 254);
            this.txtWildsWalking7.MaxValue = 255;
            this.txtWildsWalking7.MinValue = 0;
            this.txtWildsWalking7.Name = "txtWildsWalking7";
            this.txtWildsWalking7.Size = new System.Drawing.Size(35, 20);
            this.txtWildsWalking7.TabIndex = 30;
            this.txtWildsWalking7.Text = "0";
            this.txtWildsWalking7.Value = 0;
            this.txtWildsWalking7.TextChanged += new System.EventHandler(this.txtWildsWalking_TextChanged);
            // 
            // txtWildsWalking6
            // 
            this.txtWildsWalking6.Location = new System.Drawing.Point(166, 227);
            this.txtWildsWalking6.MaxValue = 255;
            this.txtWildsWalking6.MinValue = 0;
            this.txtWildsWalking6.Name = "txtWildsWalking6";
            this.txtWildsWalking6.Size = new System.Drawing.Size(35, 20);
            this.txtWildsWalking6.TabIndex = 27;
            this.txtWildsWalking6.Text = "0";
            this.txtWildsWalking6.Value = 0;
            this.txtWildsWalking6.TextChanged += new System.EventHandler(this.txtWildsWalking_TextChanged);
            // 
            // txtWildsWalking5
            // 
            this.txtWildsWalking5.Location = new System.Drawing.Point(166, 200);
            this.txtWildsWalking5.MaxValue = 255;
            this.txtWildsWalking5.MinValue = 0;
            this.txtWildsWalking5.Name = "txtWildsWalking5";
            this.txtWildsWalking5.Size = new System.Drawing.Size(35, 20);
            this.txtWildsWalking5.TabIndex = 24;
            this.txtWildsWalking5.Text = "0";
            this.txtWildsWalking5.Value = 0;
            this.txtWildsWalking5.TextChanged += new System.EventHandler(this.txtWildsWalking_TextChanged);
            // 
            // txtWildsWalking4
            // 
            this.txtWildsWalking4.Location = new System.Drawing.Point(166, 173);
            this.txtWildsWalking4.MaxValue = 255;
            this.txtWildsWalking4.MinValue = 0;
            this.txtWildsWalking4.Name = "txtWildsWalking4";
            this.txtWildsWalking4.Size = new System.Drawing.Size(35, 20);
            this.txtWildsWalking4.TabIndex = 21;
            this.txtWildsWalking4.Text = "0";
            this.txtWildsWalking4.Value = 0;
            this.txtWildsWalking4.TextChanged += new System.EventHandler(this.txtWildsWalking_TextChanged);
            // 
            // txtWildsWalking3
            // 
            this.txtWildsWalking3.Location = new System.Drawing.Point(166, 146);
            this.txtWildsWalking3.MaxValue = 255;
            this.txtWildsWalking3.MinValue = 0;
            this.txtWildsWalking3.Name = "txtWildsWalking3";
            this.txtWildsWalking3.Size = new System.Drawing.Size(35, 20);
            this.txtWildsWalking3.TabIndex = 18;
            this.txtWildsWalking3.Text = "0";
            this.txtWildsWalking3.Value = 0;
            this.txtWildsWalking3.TextChanged += new System.EventHandler(this.txtWildsWalking_TextChanged);
            // 
            // txtWildsWalking2
            // 
            this.txtWildsWalking2.Location = new System.Drawing.Point(166, 119);
            this.txtWildsWalking2.MaxValue = 255;
            this.txtWildsWalking2.MinValue = 0;
            this.txtWildsWalking2.Name = "txtWildsWalking2";
            this.txtWildsWalking2.Size = new System.Drawing.Size(35, 20);
            this.txtWildsWalking2.TabIndex = 15;
            this.txtWildsWalking2.Text = "0";
            this.txtWildsWalking2.Value = 0;
            this.txtWildsWalking2.TextChanged += new System.EventHandler(this.txtWildsWalking_TextChanged);
            // 
            // txtWildsWalking1
            // 
            this.txtWildsWalking1.Location = new System.Drawing.Point(166, 92);
            this.txtWildsWalking1.MaxValue = 255;
            this.txtWildsWalking1.MinValue = 0;
            this.txtWildsWalking1.Name = "txtWildsWalking1";
            this.txtWildsWalking1.Size = new System.Drawing.Size(35, 20);
            this.txtWildsWalking1.TabIndex = 12;
            this.txtWildsWalking1.Text = "0";
            this.txtWildsWalking1.Value = 0;
            this.txtWildsWalking1.TextChanged += new System.EventHandler(this.txtWildsWalking_TextChanged);
            // 
            // txtWildsWalking0
            // 
            this.txtWildsWalking0.Location = new System.Drawing.Point(166, 65);
            this.txtWildsWalking0.MaxValue = 255;
            this.txtWildsWalking0.MinValue = 0;
            this.txtWildsWalking0.Name = "txtWildsWalking0";
            this.txtWildsWalking0.Size = new System.Drawing.Size(35, 20);
            this.txtWildsWalking0.TabIndex = 7;
            this.txtWildsWalking0.Text = "0";
            this.txtWildsWalking0.Value = 0;
            this.txtWildsWalking0.TextChanged += new System.EventHandler(this.txtWildsWalking_TextChanged);
            // 
            // txtWildsWalkingRate
            // 
            this.txtWildsWalkingRate.Location = new System.Drawing.Point(97, 19);
            this.txtWildsWalkingRate.MaxValue = 255;
            this.txtWildsWalkingRate.MinValue = 0;
            this.txtWildsWalkingRate.Name = "txtWildsWalkingRate";
            this.txtWildsWalkingRate.Size = new System.Drawing.Size(65, 20);
            this.txtWildsWalkingRate.TabIndex = 3;
            this.txtWildsWalkingRate.Text = "0";
            this.txtWildsWalkingRate.Value = 0;
            this.txtWildsWalkingRate.TextChanged += new System.EventHandler(this.txtWildsWalkingRate_TextChanged);
            // 
            // txtWildsSRMax4
            // 
            this.txtWildsSRMax4.Location = new System.Drawing.Point(207, 173);
            this.txtWildsSRMax4.MaxValue = 255;
            this.txtWildsSRMax4.MinValue = 0;
            this.txtWildsSRMax4.Name = "txtWildsSRMax4";
            this.txtWildsSRMax4.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSRMax4.TabIndex = 27;
            this.txtWildsSRMax4.Text = "0";
            this.txtWildsSRMax4.Value = 0;
            this.txtWildsSRMax4.TextChanged += new System.EventHandler(this.txtWildsSR_TextChanged);
            // 
            // txtWildsSRMax3
            // 
            this.txtWildsSRMax3.Location = new System.Drawing.Point(207, 146);
            this.txtWildsSRMax3.MaxValue = 255;
            this.txtWildsSRMax3.MinValue = 0;
            this.txtWildsSRMax3.Name = "txtWildsSRMax3";
            this.txtWildsSRMax3.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSRMax3.TabIndex = 26;
            this.txtWildsSRMax3.Text = "0";
            this.txtWildsSRMax3.Value = 0;
            this.txtWildsSRMax3.TextChanged += new System.EventHandler(this.txtWildsSR_TextChanged);
            // 
            // txtWildsSRMax2
            // 
            this.txtWildsSRMax2.Location = new System.Drawing.Point(207, 119);
            this.txtWildsSRMax2.MaxValue = 255;
            this.txtWildsSRMax2.MinValue = 0;
            this.txtWildsSRMax2.Name = "txtWildsSRMax2";
            this.txtWildsSRMax2.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSRMax2.TabIndex = 25;
            this.txtWildsSRMax2.Text = "0";
            this.txtWildsSRMax2.Value = 0;
            this.txtWildsSRMax2.TextChanged += new System.EventHandler(this.txtWildsSR_TextChanged);
            // 
            // txtWildsSRMax1
            // 
            this.txtWildsSRMax1.Location = new System.Drawing.Point(207, 92);
            this.txtWildsSRMax1.MaxValue = 255;
            this.txtWildsSRMax1.MinValue = 0;
            this.txtWildsSRMax1.Name = "txtWildsSRMax1";
            this.txtWildsSRMax1.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSRMax1.TabIndex = 24;
            this.txtWildsSRMax1.Text = "0";
            this.txtWildsSRMax1.Value = 0;
            this.txtWildsSRMax1.TextChanged += new System.EventHandler(this.txtWildsSR_TextChanged);
            // 
            // txtWildsSRMax0
            // 
            this.txtWildsSRMax0.Location = new System.Drawing.Point(207, 65);
            this.txtWildsSRMax0.MaxValue = 255;
            this.txtWildsSRMax0.MinValue = 0;
            this.txtWildsSRMax0.Name = "txtWildsSRMax0";
            this.txtWildsSRMax0.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSRMax0.TabIndex = 23;
            this.txtWildsSRMax0.Text = "0";
            this.txtWildsSRMax0.Value = 0;
            this.txtWildsSRMax0.TextChanged += new System.EventHandler(this.txtWildsSR_TextChanged);
            // 
            // txtWildsSRMin4
            // 
            this.txtWildsSRMin4.Location = new System.Drawing.Point(166, 173);
            this.txtWildsSRMin4.MaxValue = 255;
            this.txtWildsSRMin4.MinValue = 0;
            this.txtWildsSRMin4.Name = "txtWildsSRMin4";
            this.txtWildsSRMin4.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSRMin4.TabIndex = 21;
            this.txtWildsSRMin4.Text = "0";
            this.txtWildsSRMin4.Value = 0;
            this.txtWildsSRMin4.TextChanged += new System.EventHandler(this.txtWildsSR_TextChanged);
            // 
            // txtWildsSRMin3
            // 
            this.txtWildsSRMin3.Location = new System.Drawing.Point(166, 146);
            this.txtWildsSRMin3.MaxValue = 255;
            this.txtWildsSRMin3.MinValue = 0;
            this.txtWildsSRMin3.Name = "txtWildsSRMin3";
            this.txtWildsSRMin3.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSRMin3.TabIndex = 18;
            this.txtWildsSRMin3.Text = "0";
            this.txtWildsSRMin3.Value = 0;
            this.txtWildsSRMin3.TextChanged += new System.EventHandler(this.txtWildsSR_TextChanged);
            // 
            // txtWildsSRMin2
            // 
            this.txtWildsSRMin2.Location = new System.Drawing.Point(166, 119);
            this.txtWildsSRMin2.MaxValue = 255;
            this.txtWildsSRMin2.MinValue = 0;
            this.txtWildsSRMin2.Name = "txtWildsSRMin2";
            this.txtWildsSRMin2.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSRMin2.TabIndex = 15;
            this.txtWildsSRMin2.Text = "0";
            this.txtWildsSRMin2.Value = 0;
            this.txtWildsSRMin2.TextChanged += new System.EventHandler(this.txtWildsSR_TextChanged);
            // 
            // txtWildsSRMin1
            // 
            this.txtWildsSRMin1.Location = new System.Drawing.Point(166, 92);
            this.txtWildsSRMin1.MaxValue = 255;
            this.txtWildsSRMin1.MinValue = 0;
            this.txtWildsSRMin1.Name = "txtWildsSRMin1";
            this.txtWildsSRMin1.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSRMin1.TabIndex = 12;
            this.txtWildsSRMin1.Text = "0";
            this.txtWildsSRMin1.Value = 0;
            this.txtWildsSRMin1.TextChanged += new System.EventHandler(this.txtWildsSR_TextChanged);
            // 
            // txtWildsSRMin0
            // 
            this.txtWildsSRMin0.Location = new System.Drawing.Point(166, 65);
            this.txtWildsSRMin0.MaxValue = 255;
            this.txtWildsSRMin0.MinValue = 0;
            this.txtWildsSRMin0.Name = "txtWildsSRMin0";
            this.txtWildsSRMin0.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSRMin0.TabIndex = 7;
            this.txtWildsSRMin0.Text = "0";
            this.txtWildsSRMin0.Value = 0;
            this.txtWildsSRMin0.TextChanged += new System.EventHandler(this.txtWildsSR_TextChanged);
            // 
            // txtWildsSRRate
            // 
            this.txtWildsSRRate.Location = new System.Drawing.Point(97, 19);
            this.txtWildsSRRate.MaxValue = 255;
            this.txtWildsSRRate.MinValue = 0;
            this.txtWildsSRRate.Name = "txtWildsSRRate";
            this.txtWildsSRRate.Size = new System.Drawing.Size(104, 20);
            this.txtWildsSRRate.TabIndex = 3;
            this.txtWildsSRRate.Text = "0";
            this.txtWildsSRRate.Value = 0;
            this.txtWildsSRRate.TextChanged += new System.EventHandler(this.txtWildsSRRate_TextChanged);
            // 
            // txtWildsGRMax4
            // 
            this.txtWildsGRMax4.Location = new System.Drawing.Point(207, 173);
            this.txtWildsGRMax4.MaxValue = 255;
            this.txtWildsGRMax4.MinValue = 0;
            this.txtWildsGRMax4.Name = "txtWildsGRMax4";
            this.txtWildsGRMax4.Size = new System.Drawing.Size(35, 20);
            this.txtWildsGRMax4.TabIndex = 27;
            this.txtWildsGRMax4.Text = "0";
            this.txtWildsGRMax4.Value = 0;
            this.txtWildsGRMax4.TextChanged += new System.EventHandler(this.txtWildsGR_TextChanged);
            // 
            // txtWildsGRMax3
            // 
            this.txtWildsGRMax3.Location = new System.Drawing.Point(207, 146);
            this.txtWildsGRMax3.MaxValue = 255;
            this.txtWildsGRMax3.MinValue = 0;
            this.txtWildsGRMax3.Name = "txtWildsGRMax3";
            this.txtWildsGRMax3.Size = new System.Drawing.Size(35, 20);
            this.txtWildsGRMax3.TabIndex = 26;
            this.txtWildsGRMax3.Text = "0";
            this.txtWildsGRMax3.Value = 0;
            this.txtWildsGRMax3.TextChanged += new System.EventHandler(this.txtWildsGR_TextChanged);
            // 
            // txtWildsGRMax2
            // 
            this.txtWildsGRMax2.Location = new System.Drawing.Point(207, 119);
            this.txtWildsGRMax2.MaxValue = 255;
            this.txtWildsGRMax2.MinValue = 0;
            this.txtWildsGRMax2.Name = "txtWildsGRMax2";
            this.txtWildsGRMax2.Size = new System.Drawing.Size(35, 20);
            this.txtWildsGRMax2.TabIndex = 25;
            this.txtWildsGRMax2.Text = "0";
            this.txtWildsGRMax2.Value = 0;
            this.txtWildsGRMax2.TextChanged += new System.EventHandler(this.txtWildsGR_TextChanged);
            // 
            // txtWildsGRMax1
            // 
            this.txtWildsGRMax1.Location = new System.Drawing.Point(207, 92);
            this.txtWildsGRMax1.MaxValue = 255;
            this.txtWildsGRMax1.MinValue = 0;
            this.txtWildsGRMax1.Name = "txtWildsGRMax1";
            this.txtWildsGRMax1.Size = new System.Drawing.Size(35, 20);
            this.txtWildsGRMax1.TabIndex = 24;
            this.txtWildsGRMax1.Text = "0";
            this.txtWildsGRMax1.Value = 0;
            this.txtWildsGRMax1.TextChanged += new System.EventHandler(this.txtWildsGR_TextChanged);
            // 
            // txtWildsGRMax0
            // 
            this.txtWildsGRMax0.Location = new System.Drawing.Point(207, 65);
            this.txtWildsGRMax0.MaxValue = 255;
            this.txtWildsGRMax0.MinValue = 0;
            this.txtWildsGRMax0.Name = "txtWildsGRMax0";
            this.txtWildsGRMax0.Size = new System.Drawing.Size(35, 20);
            this.txtWildsGRMax0.TabIndex = 23;
            this.txtWildsGRMax0.Text = "0";
            this.txtWildsGRMax0.Value = 0;
            this.txtWildsGRMax0.TextChanged += new System.EventHandler(this.txtWildsGR_TextChanged);
            // 
            // txtWildsGRMin4
            // 
            this.txtWildsGRMin4.Location = new System.Drawing.Point(166, 173);
            this.txtWildsGRMin4.MaxValue = 255;
            this.txtWildsGRMin4.MinValue = 0;
            this.txtWildsGRMin4.Name = "txtWildsGRMin4";
            this.txtWildsGRMin4.Size = new System.Drawing.Size(35, 20);
            this.txtWildsGRMin4.TabIndex = 21;
            this.txtWildsGRMin4.Text = "0";
            this.txtWildsGRMin4.Value = 0;
            this.txtWildsGRMin4.TextChanged += new System.EventHandler(this.txtWildsGR_TextChanged);
            // 
            // txtWildsGRMin3
            // 
            this.txtWildsGRMin3.Location = new System.Drawing.Point(166, 146);
            this.txtWildsGRMin3.MaxValue = 255;
            this.txtWildsGRMin3.MinValue = 0;
            this.txtWildsGRMin3.Name = "txtWildsGRMin3";
            this.txtWildsGRMin3.Size = new System.Drawing.Size(35, 20);
            this.txtWildsGRMin3.TabIndex = 18;
            this.txtWildsGRMin3.Text = "0";
            this.txtWildsGRMin3.Value = 0;
            this.txtWildsGRMin3.TextChanged += new System.EventHandler(this.txtWildsGR_TextChanged);
            // 
            // txtWildsGRMin2
            // 
            this.txtWildsGRMin2.Location = new System.Drawing.Point(166, 119);
            this.txtWildsGRMin2.MaxValue = 255;
            this.txtWildsGRMin2.MinValue = 0;
            this.txtWildsGRMin2.Name = "txtWildsGRMin2";
            this.txtWildsGRMin2.Size = new System.Drawing.Size(35, 20);
            this.txtWildsGRMin2.TabIndex = 15;
            this.txtWildsGRMin2.Text = "0";
            this.txtWildsGRMin2.Value = 0;
            this.txtWildsGRMin2.TextChanged += new System.EventHandler(this.txtWildsGR_TextChanged);
            // 
            // txtWildsGRMin1
            // 
            this.txtWildsGRMin1.Location = new System.Drawing.Point(166, 92);
            this.txtWildsGRMin1.MaxValue = 255;
            this.txtWildsGRMin1.MinValue = 0;
            this.txtWildsGRMin1.Name = "txtWildsGRMin1";
            this.txtWildsGRMin1.Size = new System.Drawing.Size(35, 20);
            this.txtWildsGRMin1.TabIndex = 12;
            this.txtWildsGRMin1.Text = "0";
            this.txtWildsGRMin1.Value = 0;
            this.txtWildsGRMin1.TextChanged += new System.EventHandler(this.txtWildsGR_TextChanged);
            // 
            // txtWildsGRMin0
            // 
            this.txtWildsGRMin0.Location = new System.Drawing.Point(166, 65);
            this.txtWildsGRMin0.MaxValue = 255;
            this.txtWildsGRMin0.MinValue = 0;
            this.txtWildsGRMin0.Name = "txtWildsGRMin0";
            this.txtWildsGRMin0.Size = new System.Drawing.Size(35, 20);
            this.txtWildsGRMin0.TabIndex = 7;
            this.txtWildsGRMin0.Text = "0";
            this.txtWildsGRMin0.Value = 0;
            this.txtWildsGRMin0.TextChanged += new System.EventHandler(this.txtWildsGR_TextChanged);
            // 
            // txtWildsGRRate
            // 
            this.txtWildsGRRate.Location = new System.Drawing.Point(97, 19);
            this.txtWildsGRRate.MaxValue = 255;
            this.txtWildsGRRate.MinValue = 0;
            this.txtWildsGRRate.Name = "txtWildsGRRate";
            this.txtWildsGRRate.Size = new System.Drawing.Size(104, 20);
            this.txtWildsGRRate.TabIndex = 3;
            this.txtWildsGRRate.Text = "0";
            this.txtWildsGRRate.Value = 0;
            this.txtWildsGRRate.TextChanged += new System.EventHandler(this.txtWildsGRRate_TextChanged);
            // 
            // txtWildsORMax4
            // 
            this.txtWildsORMax4.Location = new System.Drawing.Point(207, 173);
            this.txtWildsORMax4.MaxValue = 255;
            this.txtWildsORMax4.MinValue = 0;
            this.txtWildsORMax4.Name = "txtWildsORMax4";
            this.txtWildsORMax4.Size = new System.Drawing.Size(35, 20);
            this.txtWildsORMax4.TabIndex = 27;
            this.txtWildsORMax4.Text = "0";
            this.txtWildsORMax4.Value = 0;
            this.txtWildsORMax4.TextChanged += new System.EventHandler(this.txtWildsOR_TextChanged);
            // 
            // txtWildsORMax3
            // 
            this.txtWildsORMax3.Location = new System.Drawing.Point(207, 146);
            this.txtWildsORMax3.MaxValue = 255;
            this.txtWildsORMax3.MinValue = 0;
            this.txtWildsORMax3.Name = "txtWildsORMax3";
            this.txtWildsORMax3.Size = new System.Drawing.Size(35, 20);
            this.txtWildsORMax3.TabIndex = 26;
            this.txtWildsORMax3.Text = "0";
            this.txtWildsORMax3.Value = 0;
            this.txtWildsORMax3.TextChanged += new System.EventHandler(this.txtWildsOR_TextChanged);
            // 
            // txtWildsORMax2
            // 
            this.txtWildsORMax2.Location = new System.Drawing.Point(207, 119);
            this.txtWildsORMax2.MaxValue = 255;
            this.txtWildsORMax2.MinValue = 0;
            this.txtWildsORMax2.Name = "txtWildsORMax2";
            this.txtWildsORMax2.Size = new System.Drawing.Size(35, 20);
            this.txtWildsORMax2.TabIndex = 25;
            this.txtWildsORMax2.Text = "0";
            this.txtWildsORMax2.Value = 0;
            this.txtWildsORMax2.TextChanged += new System.EventHandler(this.txtWildsOR_TextChanged);
            // 
            // txtWildsORMax1
            // 
            this.txtWildsORMax1.Location = new System.Drawing.Point(207, 92);
            this.txtWildsORMax1.MaxValue = 255;
            this.txtWildsORMax1.MinValue = 0;
            this.txtWildsORMax1.Name = "txtWildsORMax1";
            this.txtWildsORMax1.Size = new System.Drawing.Size(35, 20);
            this.txtWildsORMax1.TabIndex = 24;
            this.txtWildsORMax1.Text = "0";
            this.txtWildsORMax1.Value = 0;
            this.txtWildsORMax1.TextChanged += new System.EventHandler(this.txtWildsOR_TextChanged);
            // 
            // txtWildsORMax0
            // 
            this.txtWildsORMax0.Location = new System.Drawing.Point(207, 65);
            this.txtWildsORMax0.MaxValue = 255;
            this.txtWildsORMax0.MinValue = 0;
            this.txtWildsORMax0.Name = "txtWildsORMax0";
            this.txtWildsORMax0.Size = new System.Drawing.Size(35, 20);
            this.txtWildsORMax0.TabIndex = 23;
            this.txtWildsORMax0.Text = "0";
            this.txtWildsORMax0.Value = 0;
            this.txtWildsORMax0.TextChanged += new System.EventHandler(this.txtWildsOR_TextChanged);
            // 
            // txtWildsORMin4
            // 
            this.txtWildsORMin4.Location = new System.Drawing.Point(166, 173);
            this.txtWildsORMin4.MaxValue = 255;
            this.txtWildsORMin4.MinValue = 0;
            this.txtWildsORMin4.Name = "txtWildsORMin4";
            this.txtWildsORMin4.Size = new System.Drawing.Size(35, 20);
            this.txtWildsORMin4.TabIndex = 21;
            this.txtWildsORMin4.Text = "0";
            this.txtWildsORMin4.Value = 0;
            this.txtWildsORMin4.TextChanged += new System.EventHandler(this.txtWildsOR_TextChanged);
            // 
            // txtWildsORMin3
            // 
            this.txtWildsORMin3.Location = new System.Drawing.Point(166, 146);
            this.txtWildsORMin3.MaxValue = 255;
            this.txtWildsORMin3.MinValue = 0;
            this.txtWildsORMin3.Name = "txtWildsORMin3";
            this.txtWildsORMin3.Size = new System.Drawing.Size(35, 20);
            this.txtWildsORMin3.TabIndex = 18;
            this.txtWildsORMin3.Text = "0";
            this.txtWildsORMin3.Value = 0;
            this.txtWildsORMin3.TextChanged += new System.EventHandler(this.txtWildsOR_TextChanged);
            // 
            // txtWildsORMin2
            // 
            this.txtWildsORMin2.Location = new System.Drawing.Point(166, 119);
            this.txtWildsORMin2.MaxValue = 255;
            this.txtWildsORMin2.MinValue = 0;
            this.txtWildsORMin2.Name = "txtWildsORMin2";
            this.txtWildsORMin2.Size = new System.Drawing.Size(35, 20);
            this.txtWildsORMin2.TabIndex = 15;
            this.txtWildsORMin2.Text = "0";
            this.txtWildsORMin2.Value = 0;
            this.txtWildsORMin2.TextChanged += new System.EventHandler(this.txtWildsOR_TextChanged);
            // 
            // txtWildsORMin1
            // 
            this.txtWildsORMin1.Location = new System.Drawing.Point(166, 92);
            this.txtWildsORMin1.MaxValue = 255;
            this.txtWildsORMin1.MinValue = 0;
            this.txtWildsORMin1.Name = "txtWildsORMin1";
            this.txtWildsORMin1.Size = new System.Drawing.Size(35, 20);
            this.txtWildsORMin1.TabIndex = 12;
            this.txtWildsORMin1.Text = "0";
            this.txtWildsORMin1.Value = 0;
            this.txtWildsORMin1.TextChanged += new System.EventHandler(this.txtWildsOR_TextChanged);
            // 
            // txtWildsORMin0
            // 
            this.txtWildsORMin0.Location = new System.Drawing.Point(166, 65);
            this.txtWildsORMin0.MaxValue = 255;
            this.txtWildsORMin0.MinValue = 0;
            this.txtWildsORMin0.Name = "txtWildsORMin0";
            this.txtWildsORMin0.Size = new System.Drawing.Size(35, 20);
            this.txtWildsORMin0.TabIndex = 7;
            this.txtWildsORMin0.Text = "0";
            this.txtWildsORMin0.Value = 0;
            this.txtWildsORMin0.TextChanged += new System.EventHandler(this.txtWildsOR_TextChanged);
            // 
            // txtWildsORRate
            // 
            this.txtWildsORRate.Location = new System.Drawing.Point(97, 19);
            this.txtWildsORRate.MaxValue = 255;
            this.txtWildsORRate.MinValue = 0;
            this.txtWildsORRate.Name = "txtWildsORRate";
            this.txtWildsORRate.Size = new System.Drawing.Size(104, 20);
            this.txtWildsORRate.TabIndex = 3;
            this.txtWildsORRate.Text = "0";
            this.txtWildsORRate.Value = 0;
            this.txtWildsORRate.TextChanged += new System.EventHandler(this.txtWildsORRate_TextChanged);
            // 
            // txtWildsSurfingMax4
            // 
            this.txtWildsSurfingMax4.Location = new System.Drawing.Point(207, 173);
            this.txtWildsSurfingMax4.MaxValue = 255;
            this.txtWildsSurfingMax4.MinValue = 0;
            this.txtWildsSurfingMax4.Name = "txtWildsSurfingMax4";
            this.txtWildsSurfingMax4.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSurfingMax4.TabIndex = 27;
            this.txtWildsSurfingMax4.Text = "0";
            this.txtWildsSurfingMax4.Value = 0;
            this.txtWildsSurfingMax4.TextChanged += new System.EventHandler(this.txtWildsSurfing_TextChanged);
            // 
            // txtWildsSurfingMax3
            // 
            this.txtWildsSurfingMax3.Location = new System.Drawing.Point(207, 146);
            this.txtWildsSurfingMax3.MaxValue = 255;
            this.txtWildsSurfingMax3.MinValue = 0;
            this.txtWildsSurfingMax3.Name = "txtWildsSurfingMax3";
            this.txtWildsSurfingMax3.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSurfingMax3.TabIndex = 26;
            this.txtWildsSurfingMax3.Text = "0";
            this.txtWildsSurfingMax3.Value = 0;
            this.txtWildsSurfingMax3.TextChanged += new System.EventHandler(this.txtWildsSurfing_TextChanged);
            // 
            // txtWildsSurfingMax2
            // 
            this.txtWildsSurfingMax2.Location = new System.Drawing.Point(207, 119);
            this.txtWildsSurfingMax2.MaxValue = 255;
            this.txtWildsSurfingMax2.MinValue = 0;
            this.txtWildsSurfingMax2.Name = "txtWildsSurfingMax2";
            this.txtWildsSurfingMax2.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSurfingMax2.TabIndex = 25;
            this.txtWildsSurfingMax2.Text = "0";
            this.txtWildsSurfingMax2.Value = 0;
            this.txtWildsSurfingMax2.TextChanged += new System.EventHandler(this.txtWildsSurfing_TextChanged);
            // 
            // txtWildsSurfingMax1
            // 
            this.txtWildsSurfingMax1.Location = new System.Drawing.Point(207, 92);
            this.txtWildsSurfingMax1.MaxValue = 255;
            this.txtWildsSurfingMax1.MinValue = 0;
            this.txtWildsSurfingMax1.Name = "txtWildsSurfingMax1";
            this.txtWildsSurfingMax1.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSurfingMax1.TabIndex = 24;
            this.txtWildsSurfingMax1.Text = "0";
            this.txtWildsSurfingMax1.Value = 0;
            this.txtWildsSurfingMax1.TextChanged += new System.EventHandler(this.txtWildsSurfing_TextChanged);
            // 
            // txtWildsSurfingMax0
            // 
            this.txtWildsSurfingMax0.Location = new System.Drawing.Point(207, 65);
            this.txtWildsSurfingMax0.MaxValue = 255;
            this.txtWildsSurfingMax0.MinValue = 0;
            this.txtWildsSurfingMax0.Name = "txtWildsSurfingMax0";
            this.txtWildsSurfingMax0.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSurfingMax0.TabIndex = 23;
            this.txtWildsSurfingMax0.Text = "0";
            this.txtWildsSurfingMax0.Value = 0;
            this.txtWildsSurfingMax0.TextChanged += new System.EventHandler(this.txtWildsSurfing_TextChanged);
            // 
            // txtWildsSurfingMin4
            // 
            this.txtWildsSurfingMin4.Location = new System.Drawing.Point(166, 173);
            this.txtWildsSurfingMin4.MaxValue = 255;
            this.txtWildsSurfingMin4.MinValue = 0;
            this.txtWildsSurfingMin4.Name = "txtWildsSurfingMin4";
            this.txtWildsSurfingMin4.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSurfingMin4.TabIndex = 21;
            this.txtWildsSurfingMin4.Text = "0";
            this.txtWildsSurfingMin4.Value = 0;
            this.txtWildsSurfingMin4.TextChanged += new System.EventHandler(this.txtWildsSurfing_TextChanged);
            // 
            // txtWildsSurfingMin3
            // 
            this.txtWildsSurfingMin3.Location = new System.Drawing.Point(166, 146);
            this.txtWildsSurfingMin3.MaxValue = 255;
            this.txtWildsSurfingMin3.MinValue = 0;
            this.txtWildsSurfingMin3.Name = "txtWildsSurfingMin3";
            this.txtWildsSurfingMin3.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSurfingMin3.TabIndex = 18;
            this.txtWildsSurfingMin3.Text = "0";
            this.txtWildsSurfingMin3.Value = 0;
            this.txtWildsSurfingMin3.TextChanged += new System.EventHandler(this.txtWildsSurfing_TextChanged);
            // 
            // txtWildsSurfingMin2
            // 
            this.txtWildsSurfingMin2.Location = new System.Drawing.Point(166, 119);
            this.txtWildsSurfingMin2.MaxValue = 255;
            this.txtWildsSurfingMin2.MinValue = 0;
            this.txtWildsSurfingMin2.Name = "txtWildsSurfingMin2";
            this.txtWildsSurfingMin2.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSurfingMin2.TabIndex = 15;
            this.txtWildsSurfingMin2.Text = "0";
            this.txtWildsSurfingMin2.Value = 0;
            this.txtWildsSurfingMin2.TextChanged += new System.EventHandler(this.txtWildsSurfing_TextChanged);
            // 
            // txtWildsSurfingMin1
            // 
            this.txtWildsSurfingMin1.Location = new System.Drawing.Point(166, 92);
            this.txtWildsSurfingMin1.MaxValue = 255;
            this.txtWildsSurfingMin1.MinValue = 0;
            this.txtWildsSurfingMin1.Name = "txtWildsSurfingMin1";
            this.txtWildsSurfingMin1.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSurfingMin1.TabIndex = 12;
            this.txtWildsSurfingMin1.Text = "0";
            this.txtWildsSurfingMin1.Value = 0;
            this.txtWildsSurfingMin1.TextChanged += new System.EventHandler(this.txtWildsSurfing_TextChanged);
            // 
            // txtWildsSurfingMin0
            // 
            this.txtWildsSurfingMin0.Location = new System.Drawing.Point(166, 65);
            this.txtWildsSurfingMin0.MaxValue = 255;
            this.txtWildsSurfingMin0.MinValue = 0;
            this.txtWildsSurfingMin0.Name = "txtWildsSurfingMin0";
            this.txtWildsSurfingMin0.Size = new System.Drawing.Size(35, 20);
            this.txtWildsSurfingMin0.TabIndex = 7;
            this.txtWildsSurfingMin0.Text = "0";
            this.txtWildsSurfingMin0.Value = 0;
            this.txtWildsSurfingMin0.TextChanged += new System.EventHandler(this.txtWildsSurfing_TextChanged);
            // 
            // txtWildsSurfingRate
            // 
            this.txtWildsSurfingRate.Location = new System.Drawing.Point(97, 19);
            this.txtWildsSurfingRate.MaxValue = 255;
            this.txtWildsSurfingRate.MinValue = 0;
            this.txtWildsSurfingRate.Name = "txtWildsSurfingRate";
            this.txtWildsSurfingRate.Size = new System.Drawing.Size(104, 20);
            this.txtWildsSurfingRate.TabIndex = 3;
            this.txtWildsSurfingRate.Text = "0";
            this.txtWildsSurfingRate.Value = 0;
            this.txtWildsSurfingRate.TextChanged += new System.EventHandler(this.txtWildsSurfingRate_TextChanged);
            // 
            // txtHeaderLvlScripts
            // 
            this.txtHeaderLvlScripts.Location = new System.Drawing.Point(6, 110);
            this.txtHeaderLvlScripts.MaximumValue = ((uint)(65535u));
            this.txtHeaderLvlScripts.MinimumValue = ((uint)(0u));
            this.txtHeaderLvlScripts.Name = "txtHeaderLvlScripts";
            this.txtHeaderLvlScripts.Size = new System.Drawing.Size(188, 20);
            this.txtHeaderLvlScripts.TabIndex = 18;
            this.txtHeaderLvlScripts.Text = "0";
            this.txtHeaderLvlScripts.Value = ((uint)(0u));
            this.txtHeaderLvlScripts.TextChanged += new System.EventHandler(this.txtHeaderFiles_TextChanged);
            // 
            // txtHeaderWildPokemon
            // 
            this.txtHeaderWildPokemon.Location = new System.Drawing.Point(207, 71);
            this.txtHeaderWildPokemon.MaximumValue = ((uint)(65535u));
            this.txtHeaderWildPokemon.MinimumValue = ((uint)(0u));
            this.txtHeaderWildPokemon.Name = "txtHeaderWildPokemon";
            this.txtHeaderWildPokemon.ReadOnly = true;
            this.txtHeaderWildPokemon.Size = new System.Drawing.Size(188, 20);
            this.txtHeaderWildPokemon.TabIndex = 16;
            this.txtHeaderWildPokemon.Text = "0";
            this.txtHeaderWildPokemon.Value = ((uint)(0u));
            // 
            // txtHeaderMatrix
            // 
            this.txtHeaderMatrix.Location = new System.Drawing.Point(207, 32);
            this.txtHeaderMatrix.MaximumValue = ((uint)(65535u));
            this.txtHeaderMatrix.MinimumValue = ((uint)(0u));
            this.txtHeaderMatrix.Name = "txtHeaderMatrix";
            this.txtHeaderMatrix.ReadOnly = true;
            this.txtHeaderMatrix.Size = new System.Drawing.Size(188, 20);
            this.txtHeaderMatrix.TabIndex = 14;
            this.txtHeaderMatrix.Text = "0";
            this.txtHeaderMatrix.Value = ((uint)(0u));
            // 
            // txtHeaderText
            // 
            this.txtHeaderText.Location = new System.Drawing.Point(6, 149);
            this.txtHeaderText.MaximumValue = ((uint)(65535u));
            this.txtHeaderText.MinimumValue = ((uint)(0u));
            this.txtHeaderText.Name = "txtHeaderText";
            this.txtHeaderText.ReadOnly = true;
            this.txtHeaderText.Size = new System.Drawing.Size(188, 20);
            this.txtHeaderText.TabIndex = 12;
            this.txtHeaderText.Text = "0";
            this.txtHeaderText.Value = ((uint)(0u));
            this.txtHeaderText.TextChanged += new System.EventHandler(this.txtHeaderFiles_TextChanged);
            // 
            // txtHeaderScripts
            // 
            this.txtHeaderScripts.Location = new System.Drawing.Point(6, 71);
            this.txtHeaderScripts.MaximumValue = ((uint)(65535u));
            this.txtHeaderScripts.MinimumValue = ((uint)(0u));
            this.txtHeaderScripts.Name = "txtHeaderScripts";
            this.txtHeaderScripts.ReadOnly = true;
            this.txtHeaderScripts.Size = new System.Drawing.Size(188, 20);
            this.txtHeaderScripts.TabIndex = 10;
            this.txtHeaderScripts.Text = "0";
            this.txtHeaderScripts.Value = ((uint)(0u));
            this.txtHeaderScripts.TextChanged += new System.EventHandler(this.txtHeaderFiles_TextChanged);
            // 
            // txtHeaderEvents
            // 
            this.txtHeaderEvents.Location = new System.Drawing.Point(6, 32);
            this.txtHeaderEvents.MaximumValue = ((uint)(65535u));
            this.txtHeaderEvents.MinimumValue = ((uint)(0u));
            this.txtHeaderEvents.Name = "txtHeaderEvents";
            this.txtHeaderEvents.Size = new System.Drawing.Size(188, 20);
            this.txtHeaderEvents.TabIndex = 8;
            this.txtHeaderEvents.Text = "0";
            this.txtHeaderEvents.Value = ((uint)(0u));
            this.txtHeaderEvents.TextChanged += new System.EventHandler(this.txtHeaderFiles_TextChanged);
            // 
            // txtHeaderFlags
            // 
            this.txtHeaderFlags.Location = new System.Drawing.Point(6, 110);
            this.txtHeaderFlags.MaximumValue = ((uint)(255u));
            this.txtHeaderFlags.MinimumValue = ((uint)(0u));
            this.txtHeaderFlags.Name = "txtHeaderFlags";
            this.txtHeaderFlags.Size = new System.Drawing.Size(188, 20);
            this.txtHeaderFlags.TabIndex = 16;
            this.txtHeaderFlags.Text = "0";
            this.txtHeaderFlags.Value = ((uint)(0u));
            this.txtHeaderFlags.TextChanged += new System.EventHandler(this.txtHeaderOptions_TextChanged);
            // 
            // txtHeaderCamera
            // 
            this.txtHeaderCamera.Location = new System.Drawing.Point(6, 71);
            this.txtHeaderCamera.MaximumValue = ((uint)(255u));
            this.txtHeaderCamera.MinimumValue = ((uint)(0u));
            this.txtHeaderCamera.Name = "txtHeaderCamera";
            this.txtHeaderCamera.NumberStyle = DSMap.NumericTextBox.NumberStyles.Hexadecimal;
            this.txtHeaderCamera.Size = new System.Drawing.Size(188, 20);
            this.txtHeaderCamera.TabIndex = 12;
            this.txtHeaderCamera.Text = "0x0";
            this.txtHeaderCamera.Value = ((uint)(0u));
            this.txtHeaderCamera.TextChanged += new System.EventHandler(this.txtHeaderOptions_TextChanged);
            // 
            // txtHeaderWeather
            // 
            this.txtHeaderWeather.Location = new System.Drawing.Point(207, 71);
            this.txtHeaderWeather.MaximumValue = ((uint)(255u));
            this.txtHeaderWeather.MinimumValue = ((uint)(0u));
            this.txtHeaderWeather.Name = "txtHeaderWeather";
            this.txtHeaderWeather.Size = new System.Drawing.Size(188, 20);
            this.txtHeaderWeather.TabIndex = 10;
            this.txtHeaderWeather.Text = "0";
            this.txtHeaderWeather.Value = ((uint)(0u));
            this.txtHeaderWeather.TextChanged += new System.EventHandler(this.txtHeaderOptions_TextChanged);
            // 
            // txtHeaderMusicNight
            // 
            this.txtHeaderMusicNight.Location = new System.Drawing.Point(207, 32);
            this.txtHeaderMusicNight.MaximumValue = ((uint)(65535u));
            this.txtHeaderMusicNight.MinimumValue = ((uint)(0u));
            this.txtHeaderMusicNight.Name = "txtHeaderMusicNight";
            this.txtHeaderMusicNight.Size = new System.Drawing.Size(188, 20);
            this.txtHeaderMusicNight.TabIndex = 8;
            this.txtHeaderMusicNight.Text = "0";
            this.txtHeaderMusicNight.Value = ((uint)(0u));
            this.txtHeaderMusicNight.TextChanged += new System.EventHandler(this.txtHeaderOptions_TextChanged);
            // 
            // txtHeaderMusicDay
            // 
            this.txtHeaderMusicDay.Location = new System.Drawing.Point(6, 32);
            this.txtHeaderMusicDay.MaximumValue = ((uint)(65535u));
            this.txtHeaderMusicDay.MinimumValue = ((uint)(0u));
            this.txtHeaderMusicDay.Name = "txtHeaderMusicDay";
            this.txtHeaderMusicDay.Size = new System.Drawing.Size(188, 20);
            this.txtHeaderMusicDay.TabIndex = 6;
            this.txtHeaderMusicDay.Text = "0";
            this.txtHeaderMusicDay.Value = ((uint)(0u));
            this.txtHeaderMusicDay.TextChanged += new System.EventHandler(this.txtHeaderOptions_TextChanged);
            // 
            // txtHeaderNameFrame
            // 
            this.txtHeaderNameFrame.Location = new System.Drawing.Point(6, 98);
            this.txtHeaderNameFrame.MaximumValue = ((uint)(255u));
            this.txtHeaderNameFrame.MinimumValue = ((uint)(0u));
            this.txtHeaderNameFrame.Name = "txtHeaderNameFrame";
            this.txtHeaderNameFrame.Size = new System.Drawing.Size(188, 20);
            this.txtHeaderNameFrame.TabIndex = 4;
            this.txtHeaderNameFrame.Text = "0";
            this.txtHeaderNameFrame.Value = ((uint)(0u));
            this.txtHeaderNameFrame.TextChanged += new System.EventHandler(this.txtHeaderNameFrame_TextChanged);
            // 
            // txtHeaderNameStyle
            // 
            this.txtHeaderNameStyle.Location = new System.Drawing.Point(6, 59);
            this.txtHeaderNameStyle.MaximumValue = ((uint)(255u));
            this.txtHeaderNameStyle.MinimumValue = ((uint)(0u));
            this.txtHeaderNameStyle.Name = "txtHeaderNameStyle";
            this.txtHeaderNameStyle.Size = new System.Drawing.Size(188, 20);
            this.txtHeaderNameStyle.TabIndex = 2;
            this.txtHeaderNameStyle.Text = "0";
            this.txtHeaderNameStyle.Value = ((uint)(0u));
            this.txtHeaderNameStyle.TextChanged += new System.EventHandler(this.txtHeaderNameStyle_TextChanged);
            // 
            // txtHeaderObjectTextures
            // 
            this.txtHeaderObjectTextures.Location = new System.Drawing.Point(207, 32);
            this.txtHeaderObjectTextures.MaximumValue = ((uint)(255u));
            this.txtHeaderObjectTextures.MinimumValue = ((uint)(0u));
            this.txtHeaderObjectTextures.Name = "txtHeaderObjectTextures";
            this.txtHeaderObjectTextures.Size = new System.Drawing.Size(188, 20);
            this.txtHeaderObjectTextures.TabIndex = 3;
            this.txtHeaderObjectTextures.Text = "0";
            this.txtHeaderObjectTextures.Value = ((uint)(0u));
            // 
            // txtHeaderMapTextures
            // 
            this.txtHeaderMapTextures.Location = new System.Drawing.Point(6, 32);
            this.txtHeaderMapTextures.MaximumValue = ((uint)(255u));
            this.txtHeaderMapTextures.MinimumValue = ((uint)(0u));
            this.txtHeaderMapTextures.Name = "txtHeaderMapTextures";
            this.txtHeaderMapTextures.Size = new System.Drawing.Size(188, 20);
            this.txtHeaderMapTextures.TabIndex = 2;
            this.txtHeaderMapTextures.Text = "0";
            this.txtHeaderMapTextures.Value = ((uint)(0u));
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 661);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblROM);
            this.Controls.Add(this.pBanner);
            this.Controls.Add(this.treeMaps);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DS Map [Alpha]";
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
            this.tabScripts.ResumeLayout(false);
            this.tabControlScripts.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabText.ResumeLayout(false);
            this.tabWilds.ResumeLayout(false);
            this.tabWilds.PerformLayout();
            this.tabControlWilds.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.tabHeader.ResumeLayout(false);
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBanner)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
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
        private NumericTextBox txtHeaderObjectTextures;
        private NumericTextBox txtHeaderMapTextures;
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
        private System.Windows.Forms.TabPage tabWilds;
        private System.Windows.Forms.ToolStripMenuItem patchingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createPatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applyPatchToolStripMenuItem;
        private System.Windows.Forms.Label lblNoWilds;
        private System.Windows.Forms.TabControl tabControlWilds;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblWildsWalkingRate;
        private System.Windows.Forms.Label label10;
        private SignedNumericTextBox txtWildsWalkingRate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private SignedNumericTextBox txtWildsWalking0;
        private System.Windows.Forms.ComboBox cWildsWalking0;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label16;
        private SignedNumericTextBox txtWildsWalking2;
        private System.Windows.Forms.ComboBox cWildsWalking2;
        private System.Windows.Forms.Label label15;
        private SignedNumericTextBox txtWildsWalking1;
        private System.Windows.Forms.ComboBox cWildsWalking1;
        private System.Windows.Forms.Label label19;
        private SignedNumericTextBox txtWildsWalking5;
        private System.Windows.Forms.ComboBox cWildsWalking5;
        private System.Windows.Forms.Label label18;
        private SignedNumericTextBox txtWildsWalking4;
        private System.Windows.Forms.ComboBox cWildsWalking4;
        private System.Windows.Forms.Label label17;
        private SignedNumericTextBox txtWildsWalking3;
        private System.Windows.Forms.ComboBox cWildsWalking3;
        private System.Windows.Forms.Label label25;
        private SignedNumericTextBox txtWildsWalking11;
        private System.Windows.Forms.ComboBox cWildsWalking11;
        private System.Windows.Forms.Label label24;
        private SignedNumericTextBox txtWildsWalking10;
        private System.Windows.Forms.ComboBox cWildsWalking10;
        private System.Windows.Forms.Label label23;
        private SignedNumericTextBox txtWildsWalking9;
        private System.Windows.Forms.ComboBox cWildsWalking9;
        private System.Windows.Forms.Label label22;
        private SignedNumericTextBox txtWildsWalking8;
        private System.Windows.Forms.ComboBox cWildsWalking8;
        private System.Windows.Forms.Label label21;
        private SignedNumericTextBox txtWildsWalking7;
        private System.Windows.Forms.ComboBox cWildsWalking7;
        private System.Windows.Forms.Label label20;
        private SignedNumericTextBox txtWildsWalking6;
        private System.Windows.Forms.ComboBox cWildsWalking6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.ComboBox cWildsEm1;
        private System.Windows.Forms.ComboBox cWildsEm0;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.ComboBox cWildsLeaf1;
        private System.Windows.Forms.ComboBox cWildsLeaf0;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.ComboBox cWildsFire1;
        private System.Windows.Forms.ComboBox cWildsFire0;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.ComboBox cWildsSapp1;
        private System.Windows.Forms.ComboBox cWildsSapp0;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.ComboBox cWildsRuby1;
        private System.Windows.Forms.ComboBox cWildsRuby0;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.ComboBox cWildsNight1;
        private System.Windows.Forms.ComboBox cWildsNight0;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.ComboBox cWildsDay1;
        private System.Windows.Forms.ComboBox cWildsDay0;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox cWildsMorn1;
        private System.Windows.Forms.ComboBox cWildsMorn0;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.ComboBox cWildsRadar3;
        private System.Windows.Forms.ComboBox cWildsRadar1;
        private System.Windows.Forms.ComboBox cWildsRadar2;
        private System.Windows.Forms.ComboBox cWildsRadar0;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.Label label33;
        private SignedNumericTextBox txtWildsSurfingMin4;
        private System.Windows.Forms.ComboBox cWildsSurfing4;
        private System.Windows.Forms.Label label34;
        private SignedNumericTextBox txtWildsSurfingMin3;
        private System.Windows.Forms.ComboBox cWildsSurfing3;
        private System.Windows.Forms.Label label35;
        private SignedNumericTextBox txtWildsSurfingMin2;
        private System.Windows.Forms.ComboBox cWildsSurfing2;
        private System.Windows.Forms.Label label36;
        private SignedNumericTextBox txtWildsSurfingMin1;
        private System.Windows.Forms.ComboBox cWildsSurfing1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private SignedNumericTextBox txtWildsSurfingMin0;
        private System.Windows.Forms.ComboBox cWildsSurfing0;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private SignedNumericTextBox txtWildsSurfingRate;
        private System.Windows.Forms.Label lblWildsSurfingRate;
        private System.Windows.Forms.Label label42;
        private SignedNumericTextBox txtWildsSurfingMax4;
        private SignedNumericTextBox txtWildsSurfingMax3;
        private SignedNumericTextBox txtWildsSurfingMax2;
        private SignedNumericTextBox txtWildsSurfingMax1;
        private SignedNumericTextBox txtWildsSurfingMax0;
        private System.Windows.Forms.GroupBox groupBox20;
        private SignedNumericTextBox txtWildsSRMax4;
        private SignedNumericTextBox txtWildsSRMax3;
        private SignedNumericTextBox txtWildsSRMax2;
        private SignedNumericTextBox txtWildsSRMax1;
        private SignedNumericTextBox txtWildsSRMax0;
        private System.Windows.Forms.Label label56;
        private SignedNumericTextBox txtWildsSRMin4;
        private System.Windows.Forms.ComboBox cWildsSR4;
        private System.Windows.Forms.Label label57;
        private SignedNumericTextBox txtWildsSRMin3;
        private System.Windows.Forms.ComboBox cWildsSR3;
        private System.Windows.Forms.Label label58;
        private SignedNumericTextBox txtWildsSRMin2;
        private System.Windows.Forms.ComboBox cWildsSR2;
        private System.Windows.Forms.Label label59;
        private SignedNumericTextBox txtWildsSRMin1;
        private System.Windows.Forms.ComboBox cWildsSR1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label61;
        private SignedNumericTextBox txtWildsSRMin0;
        private System.Windows.Forms.ComboBox cWildsSR0;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private SignedNumericTextBox txtWildsSRRate;
        private System.Windows.Forms.Label lblWildsSRRate;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.GroupBox groupBox19;
        private SignedNumericTextBox txtWildsGRMax4;
        private SignedNumericTextBox txtWildsGRMax3;
        private SignedNumericTextBox txtWildsGRMax2;
        private SignedNumericTextBox txtWildsGRMax1;
        private SignedNumericTextBox txtWildsGRMax0;
        private System.Windows.Forms.Label label46;
        private SignedNumericTextBox txtWildsGRMin4;
        private System.Windows.Forms.ComboBox cWildsGR4;
        private System.Windows.Forms.Label label47;
        private SignedNumericTextBox txtWildsGRMin3;
        private System.Windows.Forms.ComboBox cWildsGR3;
        private System.Windows.Forms.Label label48;
        private SignedNumericTextBox txtWildsGRMin2;
        private System.Windows.Forms.ComboBox cWildsGR2;
        private System.Windows.Forms.Label label49;
        private SignedNumericTextBox txtWildsGRMin1;
        private System.Windows.Forms.ComboBox cWildsGR1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        private SignedNumericTextBox txtWildsGRMin0;
        private System.Windows.Forms.ComboBox cWildsGR0;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private SignedNumericTextBox txtWildsGRRate;
        private System.Windows.Forms.Label lblWildsGRRate;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.GroupBox groupBox18;
        private SignedNumericTextBox txtWildsORMax4;
        private SignedNumericTextBox txtWildsORMax3;
        private SignedNumericTextBox txtWildsORMax2;
        private SignedNumericTextBox txtWildsORMax1;
        private SignedNumericTextBox txtWildsORMax0;
        private System.Windows.Forms.Label label26;
        private SignedNumericTextBox txtWildsORMin4;
        private System.Windows.Forms.ComboBox cWildsOR4;
        private System.Windows.Forms.Label label27;
        private SignedNumericTextBox txtWildsORMin3;
        private System.Windows.Forms.ComboBox cWildsOR3;
        private System.Windows.Forms.Label label28;
        private SignedNumericTextBox txtWildsORMin2;
        private System.Windows.Forms.ComboBox cWildsOR2;
        private System.Windows.Forms.Label label29;
        private SignedNumericTextBox txtWildsORMin1;
        private System.Windows.Forms.ComboBox cWildsOR1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private SignedNumericTextBox txtWildsORMin0;
        private System.Windows.Forms.ComboBox cWildsOR0;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label43;
        private SignedNumericTextBox txtWildsORRate;
        private System.Windows.Forms.Label lblWildsORRate;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton bSave;
        private System.Windows.Forms.ImageList imageListMaps;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.ComboBox cHeaderName;
        private System.Windows.Forms.Label label41;
        private NumericTextBox txtHeaderNameStyle;
        private NumericTextBox txtHeaderNameFrame;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Button bHeaderName;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.TextBox txtHeaderName;
        private System.Windows.Forms.GroupBox groupBox23;
        private System.Windows.Forms.GroupBox groupBox22;
        private NumericTextBox txtHeaderMusicDay;
        private System.Windows.Forms.Label label54;
        private NumericTextBox txtHeaderMusicNight;
        private System.Windows.Forms.Label label64;
        private NumericTextBox txtHeaderCamera;
        private System.Windows.Forms.Label label67;
        private NumericTextBox txtHeaderWeather;
        private System.Windows.Forms.Label label66;
        private NumericTextBox txtHeaderEvents;
        private System.Windows.Forms.Label label68;
        private NumericTextBox txtHeaderText;
        private System.Windows.Forms.Label label70;
        private NumericTextBox txtHeaderScripts;
        private System.Windows.Forms.Label label69;
        private NumericTextBox txtHeaderMatrix;
        private System.Windows.Forms.Label label71;
        private NumericTextBox txtHeaderWildPokemon;
        private System.Windows.Forms.Label label72;
        private NumericTextBox txtHeaderFlags;
        private System.Windows.Forms.Label label74;
        private NumericTextBox txtHeaderLvlScripts;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Button bHeaderTex;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TabPage tabScripts;
        private System.Windows.Forms.TabControl tabControlScripts;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.RichTextBox txtTokens;
        private System.Windows.Forms.Button bTokenize;
        private System.Windows.Forms.TabPage tabPage6;
        private ScintillaNET.Scintilla txtMovements;
        private ScintillaNET.Scintilla txtScripts;
        private ScintillaNET.Scintilla txtFunctions;
        private System.Windows.Forms.TabPage tabText;
        private ScintillaNET.Scintilla txtText;
        private System.Windows.Forms.Button bCompile;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadROMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildROMToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openScriptsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveScriptsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem functionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem movementsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scriptsToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem functionsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem movementsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
    }
}

