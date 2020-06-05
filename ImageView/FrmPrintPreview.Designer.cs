namespace ImageView
{
    partial class FrmPrintPreview
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
            this.printPreviewControl = new System.Windows.Forms.PrintPreviewControl();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.toolStripButtonPortrait = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLandscape = new System.Windows.Forms.ToolStripButton();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPageSetup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonClose = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // printPreviewControl
            // 
            this.printPreviewControl.AutoZoom = false;
            this.printPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printPreviewControl.Document = this.printDocument;
            this.printPreviewControl.Location = new System.Drawing.Point(0, 39);
            this.printPreviewControl.Name = "printPreviewControl";
            this.printPreviewControl.Size = new System.Drawing.Size(1008, 690);
            this.printPreviewControl.TabIndex = 0;
            this.printPreviewControl.UseAntiAlias = true;
            this.printPreviewControl.Zoom = 0.60992301112061587D;
            // 
            // printDocument
            // 
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // toolStripButtonPortrait
            // 
            this.toolStripButtonPortrait.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPortrait.Image = global::ImageView.Properties.Resources.portrait32;
            this.toolStripButtonPortrait.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPortrait.Name = "toolStripButtonPortrait";
            this.toolStripButtonPortrait.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonPortrait.Text = "Portrait Mode";
            this.toolStripButtonPortrait.Click += new System.EventHandler(this.toolStripButtonPortrait_Click);
            // 
            // toolStripButtonLandscape
            // 
            this.toolStripButtonLandscape.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLandscape.Image = global::ImageView.Properties.Resources.landscape32;
            this.toolStripButtonLandscape.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLandscape.Name = "toolStripButtonLandscape";
            this.toolStripButtonLandscape.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonLandscape.Text = "Landscape mode";
            this.toolStripButtonLandscape.Click += new System.EventHandler(this.toolStripButtonLandscape_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonPrint,
            this.toolStripSeparator2,
            this.toolStripButtonPageSetup,
            this.toolStripSeparator3,
            this.toolStripButtonPortrait,
            this.toolStripButtonLandscape,
            this.toolStripSeparator1,
            this.toolStripButtonClose});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1008, 39);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolStripButtonPrint
            // 
            this.toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPrint.Image = global::ImageView.Properties.Resources.printer32;
            this.toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrint.Name = "toolStripButtonPrint";
            this.toolStripButtonPrint.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonPrint.Text = "Print";
            this.toolStripButtonPrint.Click += new System.EventHandler(this.toolStripButtonPrint_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButtonPageSetup
            // 
            this.toolStripButtonPageSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPageSetup.Image = global::ImageView.Properties.Resources.pagesetup32;
            this.toolStripButtonPageSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPageSetup.Name = "toolStripButtonPageSetup";
            this.toolStripButtonPageSetup.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonPageSetup.Text = "Page Setup";
            this.toolStripButtonPageSetup.Click += new System.EventHandler(this.toolStripButtonPageSetup_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButtonClose
            // 
            this.toolStripButtonClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonClose.Image = global::ImageView.Properties.Resources.doorout32;
            this.toolStripButtonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClose.Name = "toolStripButtonClose";
            this.toolStripButtonClose.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonClose.Text = "Close Print Preview";
            this.toolStripButtonClose.Click += new System.EventHandler(this.toolStripButtonClose_Click);
            // 
            // FrmPrintPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.printPreviewControl);
            this.Controls.Add(this.toolStrip);
            this.Name = "FrmPrintPreview";
            this.Text = "Print Preview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPrintPreview_FormClosing);
            this.Load += new System.EventHandler(this.FrmPrintPreview_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PrintPreviewControl printPreviewControl;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.ToolStripButton toolStripButtonPortrait;
        private System.Windows.Forms.ToolStripButton toolStripButtonLandscape;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonPageSetup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonClose;
    }
}