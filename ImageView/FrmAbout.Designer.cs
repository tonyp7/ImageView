namespace ImageView
{
    partial class FrmAbout
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
            this.btnOK = new System.Windows.Forms.Button();
            this.pictureBoxDonate = new System.Windows.Forms.PictureBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.lblFreeAndOpenSource = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.lblSourceCodeRepo = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lblGetLatest = new System.Windows.Forms.Label();
            this.linkLabelGetImageView = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPleaseDonate2 = new System.Windows.Forms.Label();
            this.lblPleaseDonate = new System.Windows.Forms.Label();
            this.lblCredits = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDonate)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(278, 290);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(107, 34);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // pictureBoxDonate
            // 
            this.pictureBoxDonate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxDonate.Image = global::ImageView.Properties.Resources.paypal_donate_button;
            this.pictureBoxDonate.Location = new System.Drawing.Point(103, 46);
            this.pictureBoxDonate.Name = "pictureBoxDonate";
            this.pictureBoxDonate.Size = new System.Drawing.Size(157, 46);
            this.pictureBoxDonate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxDonate.TabIndex = 1;
            this.pictureBoxDonate.TabStop = false;
            this.pictureBoxDonate.Click += new System.EventHandler(this.pictureBoxDonate_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 9);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(35, 13);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "label1";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(12, 22);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(35, 13);
            this.labelVersion.TabIndex = 3;
            this.labelVersion.Text = "label1";
            // 
            // lblFreeAndOpenSource
            // 
            this.lblFreeAndOpenSource.AutoSize = true;
            this.lblFreeAndOpenSource.Location = new System.Drawing.Point(12, 35);
            this.lblFreeAndOpenSource.Name = "lblFreeAndOpenSource";
            this.lblFreeAndOpenSource.Size = new System.Drawing.Size(228, 13);
            this.lblFreeAndOpenSource.TabIndex = 4;
            this.lblFreeAndOpenSource.Text = "This program is free and open source software.";
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.Location = new System.Drawing.Point(12, 48);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(312, 13);
            this.labelCopyright.TabIndex = 7;
            this.labelCopyright.Text = "Licensed under the MIT License. Copyright © 2020, Tony Pottier";
            // 
            // lblSourceCodeRepo
            // 
            this.lblSourceCodeRepo.Location = new System.Drawing.Point(5, 77);
            this.lblSourceCodeRepo.Name = "lblSourceCodeRepo";
            this.lblSourceCodeRepo.Size = new System.Drawing.Size(156, 13);
            this.lblSourceCodeRepo.TabIndex = 8;
            this.lblSourceCodeRepo.Text = "Source Code Repository:";
            this.lblSourceCodeRepo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(167, 77);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(189, 13);
            this.linkLabel1.TabIndex = 9;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://github.com/tonyp7/ImageView";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // lblGetLatest
            // 
            this.lblGetLatest.Location = new System.Drawing.Point(-1, 90);
            this.lblGetLatest.Name = "lblGetLatest";
            this.lblGetLatest.Size = new System.Drawing.Size(162, 13);
            this.lblGetLatest.TabIndex = 10;
            this.lblGetLatest.Text = "Get Latest Version:";
            this.lblGetLatest.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // linkLabelGetImageView
            // 
            this.linkLabelGetImageView.AutoSize = true;
            this.linkLabelGetImageView.Location = new System.Drawing.Point(167, 90);
            this.linkLabelGetImageView.Name = "linkLabelGetImageView";
            this.linkLabelGetImageView.Size = new System.Drawing.Size(126, 13);
            this.linkLabelGetImageView.TabIndex = 11;
            this.linkLabelGetImageView.TabStop = true;
            this.linkLabelGetImageView.Text = "https://getimageview.net";
            this.linkLabelGetImageView.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPleaseDonate2);
            this.groupBox1.Controls.Add(this.lblPleaseDonate);
            this.groupBox1.Controls.Add(this.pictureBoxDonate);
            this.groupBox1.Location = new System.Drawing.Point(12, 157);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 120);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // lblPleaseDonate2
            // 
            this.lblPleaseDonate2.Location = new System.Drawing.Point(6, 95);
            this.lblPleaseDonate2.Name = "lblPleaseDonate2";
            this.lblPleaseDonate2.Size = new System.Drawing.Size(361, 13);
            this.lblPleaseDonate2.TabIndex = 15;
            this.lblPleaseDonate2.Text = "Every donation is greatly appreciated!";
            this.lblPleaseDonate2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblPleaseDonate
            // 
            this.lblPleaseDonate.Location = new System.Drawing.Point(6, 16);
            this.lblPleaseDonate.Name = "lblPleaseDonate";
            this.lblPleaseDonate.Size = new System.Drawing.Size(358, 27);
            this.lblPleaseDonate.TabIndex = 14;
            this.lblPleaseDonate.Text = "If you wish to support this program, please consider donating using the donate bu" +
    "tton below:";
            this.lblPleaseDonate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCredits
            // 
            this.lblCredits.Location = new System.Drawing.Point(12, 125);
            this.lblCredits.Name = "lblCredits";
            this.lblCredits.Size = new System.Drawing.Size(367, 29);
            this.lblCredits.TabIndex = 13;
            this.lblCredits.Text = "This program depends on other work (libraries or art files). Please check out Abo" +
    "ut/License for a complete information.";
            // 
            // FrmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 336);
            this.Controls.Add(this.lblCredits);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.linkLabelGetImageView);
            this.Controls.Add(this.lblGetLatest);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.lblSourceCodeRepo);
            this.Controls.Add(this.labelCopyright);
            this.Controls.Add(this.lblFreeAndOpenSource);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmAbout";
            this.Load += new System.EventHandler(this.FrmAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDonate)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox pictureBoxDonate;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label lblFreeAndOpenSource;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label lblSourceCodeRepo;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label lblGetLatest;
        private System.Windows.Forms.LinkLabel linkLabelGetImageView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPleaseDonate2;
        private System.Windows.Forms.Label lblPleaseDonate;
        private System.Windows.Forms.Label lblCredits;
    }
}