namespace ImageViewPreviewPanelDemo
{
    partial class Form1
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
            this.previewPanel = new ImageView.PreviewPanel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.btnIco = new System.Windows.Forms.Button();
            this.btnGif = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // previewPanel
            // 
            this.previewPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.previewPanel.Location = new System.Drawing.Point(0, 0);
            this.previewPanel.Name = "previewPanel";
            this.previewPanel.Size = new System.Drawing.Size(302, 729);
            this.previewPanel.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(302, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 729);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // btnIco
            // 
            this.btnIco.Location = new System.Drawing.Point(499, 229);
            this.btnIco.Name = "btnIco";
            this.btnIco.Size = new System.Drawing.Size(236, 67);
            this.btnIco.TabIndex = 2;
            this.btnIco.Text = "Load Sample ICO file";
            this.btnIco.UseVisualStyleBackColor = true;
            this.btnIco.Click += new System.EventHandler(this.btnIco_Click);
            // 
            // btnGif
            // 
            this.btnGif.Location = new System.Drawing.Point(499, 302);
            this.btnGif.Name = "btnGif";
            this.btnGif.Size = new System.Drawing.Size(236, 67);
            this.btnGif.TabIndex = 3;
            this.btnGif.Text = "Load Sample GIF file";
            this.btnGif.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.btnGif);
            this.Controls.Add(this.btnIco);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.previewPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ImageView.PreviewPanel previewPanel;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button btnIco;
        private System.Windows.Forms.Button btnGif;
    }
}

