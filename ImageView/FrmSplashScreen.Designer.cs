namespace ImageView
{
    partial class FrmSplashScreen
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
            this.grpLanguage = new System.Windows.Forms.GroupBox();
            this.lblSelected = new System.Windows.Forms.Label();
            this.radFr = new System.Windows.Forms.RadioButton();
            this.radEn = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblFirstLaunch = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grpLanguage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLanguage
            // 
            this.grpLanguage.Controls.Add(this.lblSelected);
            this.grpLanguage.Controls.Add(this.radFr);
            this.grpLanguage.Controls.Add(this.radEn);
            this.grpLanguage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLanguage.Location = new System.Drawing.Point(0, 96);
            this.grpLanguage.Name = "grpLanguage";
            this.grpLanguage.Size = new System.Drawing.Size(464, 162);
            this.grpLanguage.TabIndex = 4;
            this.grpLanguage.TabStop = false;
            // 
            // lblSelected
            // 
            this.lblSelected.Location = new System.Drawing.Point(12, 105);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(440, 18);
            this.lblSelected.TabIndex = 6;
            this.lblSelected.Text = "Selected Language: {0}";
            this.lblSelected.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // radFr
            // 
            this.radFr.Appearance = System.Windows.Forms.Appearance.Button;
            this.radFr.AutoSize = true;
            this.radFr.Image = global::ImageView.Flags.fr;
            this.radFr.Location = new System.Drawing.Point(230, 40);
            this.radFr.Name = "radFr";
            this.radFr.Size = new System.Drawing.Size(38, 38);
            this.radFr.TabIndex = 5;
            this.radFr.TabStop = true;
            this.radFr.Tag = "fr";
            this.radFr.UseVisualStyleBackColor = true;
            this.radFr.CheckedChanged += new System.EventHandler(this.radLanguage_CheckedChanged);
            // 
            // radEn
            // 
            this.radEn.Appearance = System.Windows.Forms.Appearance.Button;
            this.radEn.AutoSize = true;
            this.radEn.Image = global::ImageView.Flags.en;
            this.radEn.Location = new System.Drawing.Point(186, 40);
            this.radEn.Name = "radEn";
            this.radEn.Size = new System.Drawing.Size(38, 38);
            this.radEn.TabIndex = 4;
            this.radEn.TabStop = true;
            this.radEn.Tag = "en";
            this.radEn.UseVisualStyleBackColor = true;
            this.radEn.CheckedChanged += new System.EventHandler(this.radLanguage_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 258);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(464, 63);
            this.panel1.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(339, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 53);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lblFirstLaunch
            // 
            this.lblFirstLaunch.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFirstLaunch.Location = new System.Drawing.Point(0, 0);
            this.lblFirstLaunch.Name = "lblFirstLaunch";
            this.lblFirstLaunch.Size = new System.Drawing.Size(464, 96);
            this.lblFirstLaunch.TabIndex = 6;
            this.lblFirstLaunch.Text = "It appears the first time you start ImageView.\r\nPlease select your language.\r\nYou" +
    " can always change it later by going into the application settings.";
            this.lblFirstLaunch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmSplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 321);
            this.Controls.Add(this.grpLanguage);
            this.Controls.Add(this.lblFirstLaunch);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSplashScreen";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Welcome!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSplashScreen_FormClosing);
            this.grpLanguage.ResumeLayout(false);
            this.grpLanguage.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLanguage;
        private System.Windows.Forms.RadioButton radFr;
        private System.Windows.Forms.RadioButton radEn;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblFirstLaunch;
        private System.Windows.Forms.ToolTip toolTip;
    }
}