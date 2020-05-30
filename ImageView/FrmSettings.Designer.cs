namespace ImageView
{
    partial class FrmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.grpHistory = new System.Windows.Forms.GroupBox();
            this.lblHistoryExplanation = new System.Windows.Forms.Label();
            this.txtHistorySize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkHistorySaveOnExit = new System.Windows.Forms.CheckBox();
            this.grpSlideshow = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSlideshowTimer = new System.Windows.Forms.TextBox();
            this.lblTimeToDisplayEachImage = new System.Windows.Forms.Label();
            this.grpDefaultApps = new System.Windows.Forms.GroupBox();
            this.lblDefaultAppsExplanation = new System.Windows.Forms.Label();
            this.btnMakeDefault = new System.Windows.Forms.Button();
            this.tabPageView = new System.Windows.Forms.TabPage();
            this.grpAutoRotate = new System.Windows.Forms.GroupBox();
            this.chkAutoRotate = new System.Windows.Forms.CheckBox();
            this.grpDefaultSizeMode = new System.Windows.Forms.GroupBox();
            this.cmbOnLoadImageSizeMode = new System.Windows.Forms.ComboBox();
            this.lblDefaultSizeModeExplanation = new System.Windows.Forms.Label();
            this.tabPageLanguage = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grpLanguage = new System.Windows.Forms.GroupBox();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.lblLanguageExplanation = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.grpHistory.SuspendLayout();
            this.grpSlideshow.SuspendLayout();
            this.grpDefaultApps.SuspendLayout();
            this.tabPageView.SuspendLayout();
            this.grpAutoRotate.SuspendLayout();
            this.grpDefaultSizeMode.SuspendLayout();
            this.tabPageLanguage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpLanguage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageGeneral);
            this.tabControl.Controls.Add(this.tabPageView);
            this.tabControl.Controls.Add(this.tabPageLanguage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(624, 378);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.grpHistory);
            this.tabPageGeneral.Controls.Add(this.grpSlideshow);
            this.tabPageGeneral.Controls.Add(this.grpDefaultApps);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(616, 352);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            // 
            // grpHistory
            // 
            this.grpHistory.Controls.Add(this.lblHistoryExplanation);
            this.grpHistory.Controls.Add(this.txtHistorySize);
            this.grpHistory.Controls.Add(this.label5);
            this.grpHistory.Controls.Add(this.chkHistorySaveOnExit);
            this.grpHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistory.Location = new System.Drawing.Point(3, 225);
            this.grpHistory.Name = "grpHistory";
            this.grpHistory.Size = new System.Drawing.Size(610, 124);
            this.grpHistory.TabIndex = 3;
            this.grpHistory.TabStop = false;
            this.grpHistory.Text = "History";
            // 
            // lblHistoryExplanation
            // 
            this.lblHistoryExplanation.AutoSize = true;
            this.lblHistoryExplanation.Location = new System.Drawing.Point(6, 85);
            this.lblHistoryExplanation.Name = "lblHistoryExplanation";
            this.lblHistoryExplanation.Size = new System.Drawing.Size(278, 13);
            this.lblHistoryExplanation.TabIndex = 4;
            this.lblHistoryExplanation.Text = "A value of 0 will disable history. The maximum value is 99.";
            // 
            // txtHistorySize
            // 
            this.txtHistorySize.Location = new System.Drawing.Point(239, 49);
            this.txtHistorySize.MaxLength = 3;
            this.txtHistorySize.Name = "txtHistorySize";
            this.txtHistorySize.Size = new System.Drawing.Size(45, 20);
            this.txtHistorySize.TabIndex = 5;
            this.toolTip.SetToolTip(this.txtHistorySize, "Values are in milliseconds. The default of 5000 means 5 seconds.");
            this.txtHistorySize.TextChanged += new System.EventHandler(this.txtHistoryMaxSize_TextChanged);
            this.txtHistorySize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeric_KeyPress);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(233, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Number of items to keep in history:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkHistorySaveOnExit
            // 
            this.chkHistorySaveOnExit.AutoSize = true;
            this.chkHistorySaveOnExit.Location = new System.Drawing.Point(6, 28);
            this.chkHistorySaveOnExit.Name = "chkHistorySaveOnExit";
            this.chkHistorySaveOnExit.Size = new System.Drawing.Size(194, 17);
            this.chkHistorySaveOnExit.TabIndex = 1;
            this.chkHistorySaveOnExit.Text = "Save History When Exiting Program";
            this.chkHistorySaveOnExit.UseVisualStyleBackColor = true;
            // 
            // grpSlideshow
            // 
            this.grpSlideshow.Controls.Add(this.label4);
            this.grpSlideshow.Controls.Add(this.txtSlideshowTimer);
            this.grpSlideshow.Controls.Add(this.lblTimeToDisplayEachImage);
            this.grpSlideshow.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSlideshow.Location = new System.Drawing.Point(3, 152);
            this.grpSlideshow.Name = "grpSlideshow";
            this.grpSlideshow.Size = new System.Drawing.Size(610, 73);
            this.grpSlideshow.TabIndex = 2;
            this.grpSlideshow.TabStop = false;
            this.grpSlideshow.Text = "Slideshow";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(291, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "ms";
            // 
            // txtSlideshowTimer
            // 
            this.txtSlideshowTimer.Location = new System.Drawing.Point(239, 24);
            this.txtSlideshowTimer.MaxLength = 7;
            this.txtSlideshowTimer.Name = "txtSlideshowTimer";
            this.txtSlideshowTimer.Size = new System.Drawing.Size(45, 20);
            this.txtSlideshowTimer.TabIndex = 3;
            this.toolTip.SetToolTip(this.txtSlideshowTimer, "Values are in milliseconds. The default of 5000 means 5 seconds.");
            this.txtSlideshowTimer.TextChanged += new System.EventHandler(this.txtSlideshowTimer_TextChanged);
            this.txtSlideshowTimer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeric_KeyPress);
            // 
            // lblTimeToDisplayEachImage
            // 
            this.lblTimeToDisplayEachImage.Location = new System.Drawing.Point(9, 27);
            this.lblTimeToDisplayEachImage.Name = "lblTimeToDisplayEachImage";
            this.lblTimeToDisplayEachImage.Size = new System.Drawing.Size(224, 13);
            this.lblTimeToDisplayEachImage.TabIndex = 1;
            this.lblTimeToDisplayEachImage.Text = "Time to display each image:";
            this.lblTimeToDisplayEachImage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // grpDefaultApps
            // 
            this.grpDefaultApps.Controls.Add(this.lblDefaultAppsExplanation);
            this.grpDefaultApps.Controls.Add(this.btnMakeDefault);
            this.grpDefaultApps.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpDefaultApps.Location = new System.Drawing.Point(3, 3);
            this.grpDefaultApps.Name = "grpDefaultApps";
            this.grpDefaultApps.Size = new System.Drawing.Size(610, 149);
            this.grpDefaultApps.TabIndex = 1;
            this.grpDefaultApps.TabStop = false;
            this.grpDefaultApps.Text = "Default Application";
            // 
            // lblDefaultAppsExplanation
            // 
            this.lblDefaultAppsExplanation.Location = new System.Drawing.Point(6, 25);
            this.lblDefaultAppsExplanation.Name = "lblDefaultAppsExplanation";
            this.lblDefaultAppsExplanation.Size = new System.Drawing.Size(598, 61);
            this.lblDefaultAppsExplanation.TabIndex = 2;
            this.lblDefaultAppsExplanation.Text = resources.GetString("lblDefaultAppsExplanation.Text");
            this.lblDefaultAppsExplanation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMakeDefault
            // 
            this.btnMakeDefault.Location = new System.Drawing.Point(202, 89);
            this.btnMakeDefault.Name = "btnMakeDefault";
            this.btnMakeDefault.Size = new System.Drawing.Size(200, 40);
            this.btnMakeDefault.TabIndex = 1;
            this.btnMakeDefault.Text = "Choose Default Apps";
            this.btnMakeDefault.UseVisualStyleBackColor = true;
            this.btnMakeDefault.Click += new System.EventHandler(this.btnMakeDefault_Click);
            // 
            // tabPageView
            // 
            this.tabPageView.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageView.Controls.Add(this.grpAutoRotate);
            this.tabPageView.Controls.Add(this.grpDefaultSizeMode);
            this.tabPageView.Location = new System.Drawing.Point(4, 22);
            this.tabPageView.Name = "tabPageView";
            this.tabPageView.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageView.Size = new System.Drawing.Size(616, 352);
            this.tabPageView.TabIndex = 1;
            this.tabPageView.Text = "Image Viewing";
            // 
            // grpAutoRotate
            // 
            this.grpAutoRotate.Controls.Add(this.chkAutoRotate);
            this.grpAutoRotate.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpAutoRotate.Location = new System.Drawing.Point(3, 72);
            this.grpAutoRotate.Name = "grpAutoRotate";
            this.grpAutoRotate.Size = new System.Drawing.Size(610, 69);
            this.grpAutoRotate.TabIndex = 4;
            this.grpAutoRotate.TabStop = false;
            this.grpAutoRotate.Text = "Auto-rotate";
            // 
            // chkAutoRotate
            // 
            this.chkAutoRotate.AutoSize = true;
            this.chkAutoRotate.Location = new System.Drawing.Point(23, 33);
            this.chkAutoRotate.Name = "chkAutoRotate";
            this.chkAutoRotate.Size = new System.Drawing.Size(341, 17);
            this.chkAutoRotate.TabIndex = 0;
            this.chkAutoRotate.Text = "Automatically rotate the image according to its orientation metadata";
            this.chkAutoRotate.UseVisualStyleBackColor = true;
            // 
            // grpDefaultSizeMode
            // 
            this.grpDefaultSizeMode.Controls.Add(this.cmbOnLoadImageSizeMode);
            this.grpDefaultSizeMode.Controls.Add(this.lblDefaultSizeModeExplanation);
            this.grpDefaultSizeMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpDefaultSizeMode.Location = new System.Drawing.Point(3, 3);
            this.grpDefaultSizeMode.Name = "grpDefaultSizeMode";
            this.grpDefaultSizeMode.Size = new System.Drawing.Size(610, 69);
            this.grpDefaultSizeMode.TabIndex = 3;
            this.grpDefaultSizeMode.TabStop = false;
            this.grpDefaultSizeMode.Text = "Default Image Size Mode";
            // 
            // cmbOnLoadImageSizeMode
            // 
            this.cmbOnLoadImageSizeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOnLoadImageSizeMode.FormattingEnabled = true;
            this.cmbOnLoadImageSizeMode.Location = new System.Drawing.Point(322, 28);
            this.cmbOnLoadImageSizeMode.Name = "cmbOnLoadImageSizeMode";
            this.cmbOnLoadImageSizeMode.Size = new System.Drawing.Size(201, 21);
            this.cmbOnLoadImageSizeMode.TabIndex = 4;
            // 
            // lblDefaultSizeModeExplanation
            // 
            this.lblDefaultSizeModeExplanation.Location = new System.Drawing.Point(23, 31);
            this.lblDefaultSizeModeExplanation.Name = "lblDefaultSizeModeExplanation";
            this.lblDefaultSizeModeExplanation.Size = new System.Drawing.Size(293, 21);
            this.lblDefaultSizeModeExplanation.TabIndex = 3;
            this.lblDefaultSizeModeExplanation.Text = "When loading an image, set image size mode to:";
            this.lblDefaultSizeModeExplanation.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tabPageLanguage
            // 
            this.tabPageLanguage.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageLanguage.Controls.Add(this.grpLanguage);
            this.tabPageLanguage.Location = new System.Drawing.Point(4, 22);
            this.tabPageLanguage.Name = "tabPageLanguage";
            this.tabPageLanguage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLanguage.Size = new System.Drawing.Size(616, 352);
            this.tabPageLanguage.TabIndex = 2;
            this.tabPageLanguage.Text = "Language";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 378);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(624, 63);
            this.panel1.TabIndex = 4;
            // 
            // btnApply
            // 
            this.btnApply.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnApply.Location = new System.Drawing.Point(369, 5);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(120, 53);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCancel.Location = new System.Drawing.Point(5, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 53);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(489, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(10, 53);
            this.panel2.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(499, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 53);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // grpLanguage
            // 
            this.grpLanguage.Controls.Add(this.cmbLanguage);
            this.grpLanguage.Controls.Add(this.lblLanguageExplanation);
            this.grpLanguage.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpLanguage.Location = new System.Drawing.Point(3, 3);
            this.grpLanguage.Name = "grpLanguage";
            this.grpLanguage.Size = new System.Drawing.Size(610, 69);
            this.grpLanguage.TabIndex = 4;
            this.grpLanguage.TabStop = false;
            this.grpLanguage.Text = "Language";
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(322, 28);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(201, 21);
            this.cmbLanguage.TabIndex = 4;
            // 
            // lblLanguageExplanation
            // 
            this.lblLanguageExplanation.Location = new System.Drawing.Point(23, 31);
            this.lblLanguageExplanation.Name = "lblLanguageExplanation";
            this.lblLanguageExplanation.Size = new System.Drawing.Size(293, 21);
            this.lblLanguageExplanation.TabIndex = 3;
            this.lblLanguageExplanation.Text = "Choose the application\'s language:";
            this.lblLanguageExplanation.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.grpHistory.ResumeLayout(false);
            this.grpHistory.PerformLayout();
            this.grpSlideshow.ResumeLayout(false);
            this.grpSlideshow.PerformLayout();
            this.grpDefaultApps.ResumeLayout(false);
            this.tabPageView.ResumeLayout(false);
            this.grpAutoRotate.ResumeLayout(false);
            this.grpAutoRotate.PerformLayout();
            this.grpDefaultSizeMode.ResumeLayout(false);
            this.tabPageLanguage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.grpLanguage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grpDefaultApps;
        private System.Windows.Forms.Label lblDefaultAppsExplanation;
        private System.Windows.Forms.Button btnMakeDefault;
        private System.Windows.Forms.GroupBox grpSlideshow;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSlideshowTimer;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label lblTimeToDisplayEachImage;
        private System.Windows.Forms.GroupBox grpHistory;
        private System.Windows.Forms.TextBox txtHistorySize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkHistorySaveOnExit;
        private System.Windows.Forms.Label lblHistoryExplanation;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabPage tabPageView;
        private System.Windows.Forms.GroupBox grpDefaultSizeMode;
        private System.Windows.Forms.ComboBox cmbOnLoadImageSizeMode;
        private System.Windows.Forms.Label lblDefaultSizeModeExplanation;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox grpAutoRotate;
        private System.Windows.Forms.CheckBox chkAutoRotate;
        private System.Windows.Forms.TabPage tabPageLanguage;
        private System.Windows.Forms.GroupBox grpLanguage;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.Label lblLanguageExplanation;
    }
}