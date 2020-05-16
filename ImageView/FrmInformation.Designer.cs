namespace ImageView
{
    partial class FrmInformation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInformation));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageFile = new System.Windows.Forms.TabPage();
            this.dgvFile = new System.Windows.Forms.DataGridView();
            this.colProperty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageEXIF = new System.Windows.Forms.TabPage();
            this.dgvExif = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPageFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).BeginInit();
            this.tabPageEXIF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExif)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageFile);
            this.tabControl.Controls.Add(this.tabPageEXIF);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(464, 378);
            this.tabControl.TabIndex = 1;
            // 
            // tabPageFile
            // 
            this.tabPageFile.Controls.Add(this.dgvFile);
            this.tabPageFile.Location = new System.Drawing.Point(4, 22);
            this.tabPageFile.Name = "tabPageFile";
            this.tabPageFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFile.Size = new System.Drawing.Size(456, 352);
            this.tabPageFile.TabIndex = 0;
            this.tabPageFile.Text = "File";
            // 
            // dgvFile
            // 
            this.dgvFile.AllowUserToAddRows = false;
            this.dgvFile.AllowUserToDeleteRows = false;
            this.dgvFile.AllowUserToOrderColumns = true;
            this.dgvFile.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFile.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProperty,
            this.colValue});
            this.dgvFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFile.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvFile.Location = new System.Drawing.Point(3, 3);
            this.dgvFile.Name = "dgvFile";
            this.dgvFile.ReadOnly = true;
            this.dgvFile.RowHeadersVisible = false;
            this.dgvFile.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFile.Size = new System.Drawing.Size(450, 346);
            this.dgvFile.TabIndex = 1;
            // 
            // colProperty
            // 
            this.colProperty.HeaderText = "Property";
            this.colProperty.Name = "colProperty";
            this.colProperty.ReadOnly = true;
            this.colProperty.Width = 119;
            // 
            // colValue
            // 
            this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            this.colValue.ReadOnly = true;
            // 
            // tabPageEXIF
            // 
            this.tabPageEXIF.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageEXIF.Controls.Add(this.dgvExif);
            this.tabPageEXIF.Location = new System.Drawing.Point(4, 22);
            this.tabPageEXIF.Name = "tabPageEXIF";
            this.tabPageEXIF.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEXIF.Size = new System.Drawing.Size(456, 352);
            this.tabPageEXIF.TabIndex = 1;
            this.tabPageEXIF.Text = "EXIF";
            // 
            // dgvExif
            // 
            this.dgvExif.AllowUserToAddRows = false;
            this.dgvExif.AllowUserToDeleteRows = false;
            this.dgvExif.AllowUserToOrderColumns = true;
            this.dgvExif.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvExif.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvExif.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExif.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgvExif.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExif.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvExif.Location = new System.Drawing.Point(3, 3);
            this.dgvExif.Name = "dgvExif";
            this.dgvExif.ReadOnly = true;
            this.dgvExif.RowHeadersVisible = false;
            this.dgvExif.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvExif.Size = new System.Drawing.Size(450, 346);
            this.dgvExif.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Property";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 119;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 378);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(464, 63);
            this.panel1.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(339, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 53);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FrmInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 441);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmInformation";
            this.Load += new System.EventHandler(this.FrmInformation_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageFile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).EndInit();
            this.tabPageEXIF.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExif)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageFile;
        private System.Windows.Forms.DataGridView dgvFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProperty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabPage tabPageEXIF;
        private System.Windows.Forms.DataGridView dgvExif;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}