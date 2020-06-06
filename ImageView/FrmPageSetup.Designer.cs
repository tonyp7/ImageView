﻿namespace ImageView
{
    partial class FrmPageSetup
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbPaperSize = new System.Windows.Forms.ComboBox();
            this.lblPaperSize = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioCm = new System.Windows.Forms.RadioButton();
            this.radioInches = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtMarginBottom = new System.Windows.Forms.TextBox();
            this.txtMarginRight = new System.Windows.Forms.TextBox();
            this.txtMarginLeft = new System.Windows.Forms.TextBox();
            this.txtMarginTop = new System.Windows.Forms.TextBox();
            this.lblBottom = new System.Windows.Forms.Label();
            this.lblTop = new System.Windows.Forms.Label();
            this.lblRight = new System.Windows.Forms.Label();
            this.lblLeft = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbPaperSize);
            this.groupBox1.Controls.Add(this.lblPaperSize);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(384, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Paper Size";
            // 
            // cmbPaperSize
            // 
            this.cmbPaperSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaperSize.FormattingEnabled = true;
            this.cmbPaperSize.Location = new System.Drawing.Point(138, 37);
            this.cmbPaperSize.Name = "cmbPaperSize";
            this.cmbPaperSize.Size = new System.Drawing.Size(121, 21);
            this.cmbPaperSize.TabIndex = 1;
            this.cmbPaperSize.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.cmbPaperSize_Format);
            // 
            // lblPaperSize
            // 
            this.lblPaperSize.Location = new System.Drawing.Point(32, 40);
            this.lblPaperSize.Name = "lblPaperSize";
            this.lblPaperSize.Size = new System.Drawing.Size(100, 23);
            this.lblPaperSize.TabIndex = 0;
            this.lblPaperSize.Text = "Paper Size";
            this.lblPaperSize.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioCm);
            this.groupBox2.Controls.Add(this.radioInches);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.txtMarginBottom);
            this.groupBox2.Controls.Add(this.txtMarginRight);
            this.groupBox2.Controls.Add(this.txtMarginLeft);
            this.groupBox2.Controls.Add(this.txtMarginTop);
            this.groupBox2.Controls.Add(this.lblBottom);
            this.groupBox2.Controls.Add(this.lblTop);
            this.groupBox2.Controls.Add(this.lblRight);
            this.groupBox2.Controls.Add(this.lblLeft);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(384, 193);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Margins";
            // 
            // radioCm
            // 
            this.radioCm.AutoSize = true;
            this.radioCm.Location = new System.Drawing.Point(297, 90);
            this.radioCm.Name = "radioCm";
            this.radioCm.Size = new System.Drawing.Size(80, 17);
            this.radioCm.TabIndex = 10;
            this.radioCm.TabStop = true;
            this.radioCm.Text = "Centimeters";
            this.radioCm.UseVisualStyleBackColor = true;
            // 
            // radioInches
            // 
            this.radioInches.AutoSize = true;
            this.radioInches.Checked = true;
            this.radioInches.Location = new System.Drawing.Point(297, 67);
            this.radioInches.Name = "radioInches";
            this.radioInches.Size = new System.Drawing.Size(57, 17);
            this.radioInches.TabIndex = 9;
            this.radioInches.TabStop = true;
            this.radioInches.Text = "Inches";
            this.radioInches.UseVisualStyleBackColor = true;
            this.radioInches.CheckedChanged += new System.EventHandler(this.radioInches_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ImageView.Properties.Resources.margins32;
            this.pictureBox1.Location = new System.Drawing.Point(122, 82);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // txtMarginBottom
            // 
            this.txtMarginBottom.Location = new System.Drawing.Point(118, 137);
            this.txtMarginBottom.Name = "txtMarginBottom";
            this.txtMarginBottom.Size = new System.Drawing.Size(40, 20);
            this.txtMarginBottom.TabIndex = 7;
            this.txtMarginBottom.Text = "10.0";
            // 
            // txtMarginRight
            // 
            this.txtMarginRight.Location = new System.Drawing.Point(217, 89);
            this.txtMarginRight.Name = "txtMarginRight";
            this.txtMarginRight.Size = new System.Drawing.Size(40, 20);
            this.txtMarginRight.TabIndex = 6;
            this.txtMarginRight.Text = "10.0";
            // 
            // txtMarginLeft
            // 
            this.txtMarginLeft.Location = new System.Drawing.Point(49, 89);
            this.txtMarginLeft.Name = "txtMarginLeft";
            this.txtMarginLeft.Size = new System.Drawing.Size(40, 20);
            this.txtMarginLeft.TabIndex = 5;
            this.txtMarginLeft.Text = "10.0";
            // 
            // txtMarginTop
            // 
            this.txtMarginTop.Location = new System.Drawing.Point(118, 48);
            this.txtMarginTop.Name = "txtMarginTop";
            this.txtMarginTop.Size = new System.Drawing.Size(40, 20);
            this.txtMarginTop.TabIndex = 4;
            this.txtMarginTop.Text = "10.0";
            // 
            // lblBottom
            // 
            this.lblBottom.Location = new System.Drawing.Point(-42, 140);
            this.lblBottom.Name = "lblBottom";
            this.lblBottom.Size = new System.Drawing.Size(154, 13);
            this.lblBottom.TabIndex = 3;
            this.lblBottom.Text = "Bottom";
            this.lblBottom.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTop
            // 
            this.lblTop.Location = new System.Drawing.Point(39, 51);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(73, 13);
            this.lblTop.TabIndex = 2;
            this.lblTop.Text = "Top";
            this.lblTop.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblRight
            // 
            this.lblRight.Location = new System.Drawing.Point(115, 92);
            this.lblRight.Name = "lblRight";
            this.lblRight.Size = new System.Drawing.Size(96, 13);
            this.lblRight.TabIndex = 1;
            this.lblRight.Text = "Right";
            this.lblRight.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblLeft
            // 
            this.lblLeft.Location = new System.Drawing.Point(-57, 92);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(100, 19);
            this.lblLeft.TabIndex = 0;
            this.lblLeft.Text = "Left";
            this.lblLeft.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 278);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(384, 63);
            this.panel1.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCancel.Location = new System.Drawing.Point(5, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 53);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(249, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(10, 53);
            this.panel2.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(259, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 53);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FrmPageSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 341);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPageSetup";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Page Setup";
            this.Load += new System.EventHandler(this.FrmPageSetup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtMarginBottom;
        private System.Windows.Forms.TextBox txtMarginRight;
        private System.Windows.Forms.TextBox txtMarginLeft;
        private System.Windows.Forms.TextBox txtMarginTop;
        private System.Windows.Forms.Label lblBottom;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.Label lblRight;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cmbPaperSize;
        private System.Windows.Forms.Label lblPaperSize;
        private System.Windows.Forms.RadioButton radioCm;
        private System.Windows.Forms.RadioButton radioInches;
    }
}