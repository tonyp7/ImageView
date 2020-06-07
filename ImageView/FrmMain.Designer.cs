namespace ImageView
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.rotateLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalFlipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalFlipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullscreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slideshowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readerModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.previousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrintPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSlideShow = new System.Windows.Forms.ToolStripButton();
            this.toolStripReaderMode = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripComboBoxNavigation = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButtonDisplayType = new System.Windows.Forms.ToolStripDropDownButton();
            this.realSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BestFitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fitToWidthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fitToHeightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxZoom = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSettings = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelImageInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelImagePosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelZoom = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelFileSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelPixelPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.timerSlideShow = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.imageToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpen,
            this.closeToolStripMenuItem,
            this.toolStripSeparator8,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.printToolStripMenuItem,
            this.toolStripSeparator11,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // toolStripMenuItemOpen
            // 
            this.toolStripMenuItemOpen.Image = global::ImageView.Properties.Resources.open16;
            this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            this.toolStripMenuItemOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.toolStripMenuItemOpen.Size = new System.Drawing.Size(149, 22);
            this.toolStripMenuItemOpen.Text = "Open";
            this.toolStripMenuItemOpen.Click += new System.EventHandler(this.toolStripMenuItemOpen_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Image = global::ImageView.Properties.Resources.close16;
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(146, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::ImageView.Properties.Resources.delete;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(146, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = global::ImageView.Properties.Resources.printer16;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.printToolStripMenuItem.Text = "Print...";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(146, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informationToolStripMenuItem,
            this.toolStripSeparator9,
            this.rotateLeftToolStripMenuItem,
            this.rotateRightToolStripMenuItem,
            this.verticalFlipToolStripMenuItem,
            this.horizontalFlipToolStripMenuItem});
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.imageToolStripMenuItem.Text = "&Image";
            // 
            // informationToolStripMenuItem
            // 
            this.informationToolStripMenuItem.Image = global::ImageView.Properties.Resources.properties16;
            this.informationToolStripMenuItem.Name = "informationToolStripMenuItem";
            this.informationToolStripMenuItem.ShortcutKeyDisplayString = "I";
            this.informationToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.informationToolStripMenuItem.Text = "Information";
            this.informationToolStripMenuItem.Click += new System.EventHandler(this.informationToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(251, 6);
            // 
            // rotateLeftToolStripMenuItem
            // 
            this.rotateLeftToolStripMenuItem.Image = global::ImageView.Properties.Resources.rotaterleft16;
            this.rotateLeftToolStripMenuItem.Name = "rotateLeftToolStripMenuItem";
            this.rotateLeftToolStripMenuItem.ShortcutKeyDisplayString = "L";
            this.rotateLeftToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.rotateLeftToolStripMenuItem.Text = "Rotate Left (Counter Clockwise)";
            this.rotateLeftToolStripMenuItem.Click += new System.EventHandler(this.rotateLeftToolStripMenuItem_Click);
            // 
            // rotateRightToolStripMenuItem
            // 
            this.rotateRightToolStripMenuItem.Image = global::ImageView.Properties.Resources.rotateright16;
            this.rotateRightToolStripMenuItem.Name = "rotateRightToolStripMenuItem";
            this.rotateRightToolStripMenuItem.ShortcutKeyDisplayString = "R";
            this.rotateRightToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.rotateRightToolStripMenuItem.Text = "Rotate Right (Clockwise)";
            this.rotateRightToolStripMenuItem.Click += new System.EventHandler(this.rotateRightToolStripMenuItem_Click);
            // 
            // verticalFlipToolStripMenuItem
            // 
            this.verticalFlipToolStripMenuItem.Image = global::ImageView.Properties.Resources.verticalflip16;
            this.verticalFlipToolStripMenuItem.Name = "verticalFlipToolStripMenuItem";
            this.verticalFlipToolStripMenuItem.ShortcutKeyDisplayString = "V";
            this.verticalFlipToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.verticalFlipToolStripMenuItem.Text = "Vertical Flip";
            this.verticalFlipToolStripMenuItem.Click += new System.EventHandler(this.verticalFlipToolStripMenuItem_Click);
            // 
            // horizontalFlipToolStripMenuItem
            // 
            this.horizontalFlipToolStripMenuItem.Image = global::ImageView.Properties.Resources.horizontalflip16;
            this.horizontalFlipToolStripMenuItem.Name = "horizontalFlipToolStripMenuItem";
            this.horizontalFlipToolStripMenuItem.ShortcutKeyDisplayString = "H";
            this.horizontalFlipToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.horizontalFlipToolStripMenuItem.Text = "Horizontal Flip";
            this.horizontalFlipToolStripMenuItem.Click += new System.EventHandler(this.horizontalFlipToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullscreenToolStripMenuItem,
            this.slideshowToolStripMenuItem,
            this.readerModeToolStripMenuItem,
            this.toolStripSeparator5,
            this.previousToolStripMenuItem,
            this.nextToolStripMenuItem,
            this.toolStripSeparator10,
            this.zoomToolToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // fullscreenToolStripMenuItem
            // 
            this.fullscreenToolStripMenuItem.Name = "fullscreenToolStripMenuItem";
            this.fullscreenToolStripMenuItem.ShortcutKeyDisplayString = "F";
            this.fullscreenToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.fullscreenToolStripMenuItem.Text = "Fullscreen";
            this.fullscreenToolStripMenuItem.Click += new System.EventHandler(this.fullscreenToolStripMenuItem_Click);
            // 
            // slideshowToolStripMenuItem
            // 
            this.slideshowToolStripMenuItem.Image = global::ImageView.Properties.Resources.slideshow32;
            this.slideshowToolStripMenuItem.Name = "slideshowToolStripMenuItem";
            this.slideshowToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.slideshowToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.slideshowToolStripMenuItem.Text = "Slideshow";
            this.slideshowToolStripMenuItem.Click += new System.EventHandler(this.slideshowToolStripMenuItem_Click);
            // 
            // readerModeToolStripMenuItem
            // 
            this.readerModeToolStripMenuItem.Image = global::ImageView.Properties.Resources.openbook16;
            this.readerModeToolStripMenuItem.Name = "readerModeToolStripMenuItem";
            this.readerModeToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.readerModeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.readerModeToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.readerModeToolStripMenuItem.Text = "Reader Mode";
            this.readerModeToolStripMenuItem.Click += new System.EventHandler(this.readerModeToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(160, 6);
            // 
            // previousToolStripMenuItem
            // 
            this.previousToolStripMenuItem.Image = global::ImageView.Properties.Resources.left;
            this.previousToolStripMenuItem.Name = "previousToolStripMenuItem";
            this.previousToolStripMenuItem.ShortcutKeyDisplayString = "Left";
            this.previousToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.previousToolStripMenuItem.Text = "Previous";
            // 
            // nextToolStripMenuItem
            // 
            this.nextToolStripMenuItem.Image = global::ImageView.Properties.Resources.right;
            this.nextToolStripMenuItem.Name = "nextToolStripMenuItem";
            this.nextToolStripMenuItem.ShortcutKeyDisplayString = "Right";
            this.nextToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.nextToolStripMenuItem.Text = "Next";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(160, 6);
            // 
            // zoomToolToolStripMenuItem
            // 
            this.zoomToolToolStripMenuItem.Image = global::ImageView.Properties.Resources.zoom16;
            this.zoomToolToolStripMenuItem.Name = "zoomToolToolStripMenuItem";
            this.zoomToolToolStripMenuItem.ShortcutKeyDisplayString = "Z";
            this.zoomToolToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.zoomToolToolStripMenuItem.Text = "Zoom Tool";
            this.zoomToolToolStripMenuItem.Click += new System.EventHandler(this.zoomToolToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.ShortcutKeyDisplayString = "P";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesToolStripMenuItem,
            this.licenseToolStripMenuItem,
            this.toolStripSeparator7,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates";
            this.checkForUpdatesToolStripMenuItem.Visible = false;
            // 
            // licenseToolStripMenuItem
            // 
            this.licenseToolStripMenuItem.Name = "licenseToolStripMenuItem";
            this.licenseToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.licenseToolStripMenuItem.Text = "License";
            this.licenseToolStripMenuItem.Click += new System.EventHandler(this.licenseToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(168, 6);
            this.toolStripSeparator7.Visible = false;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::ImageView.Properties.Resources.help16;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpen,
            this.toolStripButtonPrintPreview,
            this.toolStripButtonSaveAs,
            this.toolStripButtonDelete,
            this.toolStripSeparator2,
            this.toolStripButtonSlideShow,
            this.toolStripReaderMode,
            this.toolStripButtonPrevious,
            this.toolStripButtonNext,
            this.toolStripSeparator3,
            this.toolStripComboBoxNavigation,
            this.toolStripSeparator4,
            this.toolStripDropDownButtonDisplayType,
            this.toolStripComboBoxZoom,
            this.toolStripSeparator6,
            this.toolStripButtonSettings});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1008, 42);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpen.Image = global::ImageView.Properties.Resources.open32;
            this.toolStripButtonOpen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(36, 39);
            this.toolStripButtonOpen.Text = "Open Image File";
            this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
            // 
            // toolStripButtonPrintPreview
            // 
            this.toolStripButtonPrintPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrintPreview.Name = "toolStripButtonPrintPreview";
            this.toolStripButtonPrintPreview.Size = new System.Drawing.Size(23, 39);
            this.toolStripButtonPrintPreview.Text = "Print";
            this.toolStripButtonPrintPreview.Visible = false;
            this.toolStripButtonPrintPreview.Click += new System.EventHandler(this.toolStripButtonPrintPreview_Click);
            // 
            // toolStripButtonSaveAs
            // 
            this.toolStripButtonSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveAs.Name = "toolStripButtonSaveAs";
            this.toolStripButtonSaveAs.Size = new System.Drawing.Size(23, 39);
            this.toolStripButtonSaveAs.Text = "Save Image Copy As...";
            this.toolStripButtonSaveAs.Visible = false;
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDelete.Image = global::ImageView.Properties.Resources.delete;
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(36, 39);
            this.toolStripButtonDelete.Text = "Delete Image";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 42);
            // 
            // toolStripButtonSlideShow
            // 
            this.toolStripButtonSlideShow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSlideShow.Image = global::ImageView.Properties.Resources.slideshow32;
            this.toolStripButtonSlideShow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonSlideShow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSlideShow.Name = "toolStripButtonSlideShow";
            this.toolStripButtonSlideShow.Size = new System.Drawing.Size(36, 39);
            this.toolStripButtonSlideShow.Text = "Slideshow";
            this.toolStripButtonSlideShow.Click += new System.EventHandler(this.toolStripButtonSlideShow_Click);
            // 
            // toolStripReaderMode
            // 
            this.toolStripReaderMode.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripReaderMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripReaderMode.Image = global::ImageView.Properties.Resources.openbook32;
            this.toolStripReaderMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripReaderMode.Name = "toolStripReaderMode";
            this.toolStripReaderMode.Size = new System.Drawing.Size(36, 39);
            this.toolStripReaderMode.Text = "Read Mode";
            this.toolStripReaderMode.Click += new System.EventHandler(this.toolStripReaderMode_Click);
            // 
            // toolStripButtonPrevious
            // 
            this.toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrevious.Image = global::ImageView.Properties.Resources.left;
            this.toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrevious.Name = "toolStripButtonPrevious";
            this.toolStripButtonPrevious.Size = new System.Drawing.Size(36, 39);
            this.toolStripButtonPrevious.Text = "Previous Image";
            this.toolStripButtonPrevious.Click += new System.EventHandler(this.toolStripButtonPrevious_Click);
            // 
            // toolStripButtonNext
            // 
            this.toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNext.Image = global::ImageView.Properties.Resources.right;
            this.toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNext.Name = "toolStripButtonNext";
            this.toolStripButtonNext.Size = new System.Drawing.Size(36, 39);
            this.toolStripButtonNext.Text = "Next Image";
            this.toolStripButtonNext.Click += new System.EventHandler(this.toolStripButtonNext_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 42);
            // 
            // toolStripComboBoxNavigation
            // 
            this.toolStripComboBoxNavigation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.toolStripComboBoxNavigation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.toolStripComboBoxNavigation.AutoSize = false;
            this.toolStripComboBoxNavigation.DropDownWidth = 500;
            this.toolStripComboBoxNavigation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toolStripComboBoxNavigation.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripComboBoxNavigation.ForeColor = System.Drawing.SystemColors.GrayText;
            this.toolStripComboBoxNavigation.MaxDropDownItems = 10;
            this.toolStripComboBoxNavigation.Name = "toolStripComboBoxNavigation";
            this.toolStripComboBoxNavigation.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripComboBoxNavigation.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripComboBoxNavigation.Size = new System.Drawing.Size(500, 38);
            this.toolStripComboBoxNavigation.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxNavigation_SelectedIndexChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 42);
            // 
            // toolStripDropDownButtonDisplayType
            // 
            this.toolStripDropDownButtonDisplayType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.realSizeToolStripMenuItem,
            this.BestFitToolStripMenuItem,
            this.fitToWidthToolStripMenuItem,
            this.fitToHeightToolStripMenuItem});
            this.toolStripDropDownButtonDisplayType.Image = global::ImageView.Properties.Resources.image;
            this.toolStripDropDownButtonDisplayType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonDisplayType.Name = "toolStripDropDownButtonDisplayType";
            this.toolStripDropDownButtonDisplayType.Size = new System.Drawing.Size(45, 39);
            this.toolStripDropDownButtonDisplayType.ToolTipText = "Pick Image Sizing";
            this.toolStripDropDownButtonDisplayType.Click += new System.EventHandler(this.toolStripDropDownButtonDisplayType_Click);
            // 
            // realSizeToolStripMenuItem
            // 
            this.realSizeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.realSizeToolStripMenuItem.Name = "realSizeToolStripMenuItem";
            this.realSizeToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.realSizeToolStripMenuItem.Text = "Real Size";
            this.realSizeToolStripMenuItem.Click += new System.EventHandler(this.realSizeToolStripMenuItem_Click);
            // 
            // BestFitToolStripMenuItem
            // 
            this.BestFitToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BestFitToolStripMenuItem.Name = "BestFitToolStripMenuItem";
            this.BestFitToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.BestFitToolStripMenuItem.Text = "Best Fit";
            this.BestFitToolStripMenuItem.Click += new System.EventHandler(this.BestFitStripMenuItem_Click);
            // 
            // fitToWidthToolStripMenuItem
            // 
            this.fitToWidthToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.fitToWidthToolStripMenuItem.Name = "fitToWidthToolStripMenuItem";
            this.fitToWidthToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.fitToWidthToolStripMenuItem.Text = "Fit to Width";
            this.fitToWidthToolStripMenuItem.Click += new System.EventHandler(this.fitToWidthToolStripMenuItem_Click);
            // 
            // fitToHeightToolStripMenuItem
            // 
            this.fitToHeightToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.fitToHeightToolStripMenuItem.Name = "fitToHeightToolStripMenuItem";
            this.fitToHeightToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.fitToHeightToolStripMenuItem.Text = "Fit to Height";
            this.fitToHeightToolStripMenuItem.Click += new System.EventHandler(this.fitToHeightToolStripMenuItem_Click);
            // 
            // toolStripComboBoxZoom
            // 
            this.toolStripComboBoxZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toolStripComboBoxZoom.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.toolStripComboBoxZoom.Name = "toolStripComboBoxZoom";
            this.toolStripComboBoxZoom.Size = new System.Drawing.Size(80, 42);
            this.toolStripComboBoxZoom.Text = "100%";
            this.toolStripComboBoxZoom.ToolTipText = "Zoom";
            this.toolStripComboBoxZoom.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxZoom_SelectedIndexChanged);
            this.toolStripComboBoxZoom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripComboBoxZoom_KeyPress);
            this.toolStripComboBoxZoom.Click += new System.EventHandler(this.toolStripComboBoxZoom_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 42);
            // 
            // toolStripButtonSettings
            // 
            this.toolStripButtonSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSettings.Image = global::ImageView.Properties.Resources.settings32;
            this.toolStripButtonSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSettings.Name = "toolStripButtonSettings";
            this.toolStripButtonSettings.Size = new System.Drawing.Size(36, 39);
            this.toolStripButtonSettings.Text = "Settings";
            this.toolStripButtonSettings.Click += new System.EventHandler(this.toolStripButtonSettings_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelImageInfo,
            this.toolStripStatusLabelImagePosition,
            this.toolStripStatusLabelZoom,
            this.toolStripStatusLabelFileSize,
            this.toolStripStatusLabelSpring,
            this.toolStripStatusLabelPixelPosition});
            this.statusStrip.Location = new System.Drawing.Point(0, 705);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1008, 24);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabelImageInfo
            // 
            this.toolStripStatusLabelImageInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelImageInfo.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabelImageInfo.Name = "toolStripStatusLabelImageInfo";
            this.toolStripStatusLabelImageInfo.Size = new System.Drawing.Size(269, 19);
            this.toolStripStatusLabelImageInfo.Text = "Welcome! Open an image file to begin browsing.";
            // 
            // toolStripStatusLabelImagePosition
            // 
            this.toolStripStatusLabelImagePosition.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelImagePosition.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabelImagePosition.Name = "toolStripStatusLabelImagePosition";
            this.toolStripStatusLabelImagePosition.Size = new System.Drawing.Size(87, 19);
            this.toolStripStatusLabelImagePosition.Text = "ImagePosition";
            // 
            // toolStripStatusLabelZoom
            // 
            this.toolStripStatusLabelZoom.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelZoom.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabelZoom.Name = "toolStripStatusLabelZoom";
            this.toolStripStatusLabelZoom.Size = new System.Drawing.Size(43, 19);
            this.toolStripStatusLabelZoom.Text = "Zoom";
            // 
            // toolStripStatusLabelFileSize
            // 
            this.toolStripStatusLabelFileSize.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelFileSize.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabelFileSize.Name = "toolStripStatusLabelFileSize";
            this.toolStripStatusLabelFileSize.Size = new System.Drawing.Size(49, 19);
            this.toolStripStatusLabelFileSize.Text = "FileSize";
            // 
            // toolStripStatusLabelSpring
            // 
            this.toolStripStatusLabelSpring.Name = "toolStripStatusLabelSpring";
            this.toolStripStatusLabelSpring.Size = new System.Drawing.Size(470, 19);
            this.toolStripStatusLabelSpring.Spring = true;
            // 
            // toolStripStatusLabelPixelPosition
            // 
            this.toolStripStatusLabelPixelPosition.Name = "toolStripStatusLabelPixelPosition";
            this.toolStripStatusLabelPixelPosition.Size = new System.Drawing.Size(75, 19);
            this.toolStripStatusLabelPixelPosition.Text = "PixelPosition";
            this.toolStripStatusLabelPixelPosition.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // panelMain
            // 
            this.panelMain.AllowDrop = true;
            this.panelMain.AutoScroll = true;
            this.panelMain.BackColor = System.Drawing.Color.Black;
            this.panelMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 66);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1008, 639);
            this.panelMain.TabIndex = 4;
            this.panelMain.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panelMain_Scroll);
            this.panelMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelMain_DragDrop);
            this.panelMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.panelMain_DragEnter);
            this.panelMain.DoubleClick += new System.EventHandler(this.pictureBox_DoubleClick);
            this.panelMain.Resize += new System.EventHandler(this.panelMain_Resize);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Open Image";
            // 
            // timerSlideShow
            // 
            this.timerSlideShow.Interval = 5000;
            this.timerSlideShow.Tick += new System.EventHandler(this.timerSlideShow_Tick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyUp);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveAs;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonSlideShow;
        private System.Windows.Forms.ToolStripButton toolStripButtonPrevious;
        private System.Windows.Forms.ToolStripButton toolStripButtonNext;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxNavigation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonDisplayType;
        //private System.Windows.Forms.PictureBox pictureBox;
        //private ImageView.PictureBox pictureBox;
        private System.Windows.Forms.ToolStripMenuItem BestFitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem realSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelImageInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelImagePosition;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelZoom;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFileSize;
        private System.Windows.Forms.ToolStripButton toolStripButtonSettings;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previousToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem slideshowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem informationToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxZoom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullscreenToolStripMenuItem;
        private System.Windows.Forms.Timer timerSlideShow;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem rotateLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalFlipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontalFlipToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem zoomToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPixelPosition;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSpring;
        private System.Windows.Forms.ToolStripMenuItem licenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fitToWidthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fitToHeightToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripReaderMode;
        private System.Windows.Forms.ToolStripMenuItem readerModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonPrintPreview;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
    }
}

