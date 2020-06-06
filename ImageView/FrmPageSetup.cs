using ImageView.Components;
using ImageView.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageView
{
    public partial class FrmPageSetup : Form
    {
        private PrintDocument printDocument;
        
        public Margins Margins { get; set; }

        public PaperSize PaperSize { get; set; }


        private NumericTextBox txtMarginLeftNumeric;
        private NumericTextBox txtMarginRightNumeric;
        private NumericTextBox txtMarginTopNumeric;
        private NumericTextBox txtMarginBottomNumeric;

        public FrmPageSetup(ref PrintDocument printDocument)
        {
            InitializeComponent();
            InitalizeComponentsCultureAware();

            this.printDocument = printDocument;

            //Numerics textbox because Designer does not support controls in 64 bits so
            //this is a copout to keep designers with normal textboxes that get replaced
            //by numeric textboxes
            txtMarginLeftNumeric = new NumericTextBox(false, true);
            txtMarginLeftNumeric.Location = txtMarginLeft.Location;
            txtMarginLeftNumeric.Name = "txtMarginsLeftNumeric";
            txtMarginLeftNumeric.Size = txtMarginLeft.Size;
            grpMargins.Controls.Remove(txtMarginLeft);
            grpMargins.Controls.Add(txtMarginLeftNumeric);

            txtMarginRightNumeric = new NumericTextBox(false, true);
            txtMarginRightNumeric.Location = txtMarginRight.Location;
            txtMarginRightNumeric.Name = "txtMarginsRightNumeric";
            txtMarginRightNumeric.Size = txtMarginRight.Size;
            grpMargins.Controls.Remove(txtMarginRight);
            grpMargins.Controls.Add(txtMarginRightNumeric);

            txtMarginTopNumeric = new NumericTextBox(false, true);
            txtMarginTopNumeric.Location = txtMarginTop.Location;
            txtMarginTopNumeric.Name = "txtMarginsTopNumeric";
            txtMarginTopNumeric.Size = txtMarginRight.Size;
            grpMargins.Controls.Remove(txtMarginTop);
            grpMargins.Controls.Add(txtMarginTopNumeric);

            txtMarginBottomNumeric = new NumericTextBox(false, true);
            txtMarginBottomNumeric.Location = txtMarginBottom.Location;
            txtMarginBottomNumeric.Name = "txtMarginsBottomNumeric";
            txtMarginBottomNumeric.Size = txtMarginBottom.Size;
            grpMargins.Controls.Remove(txtMarginBottom);
            grpMargins.Controls.Add(txtMarginBottomNumeric);

            txtMarginLeftNumeric.TextChanged += txtMargin_TextChanged;
            txtMarginRightNumeric.TextChanged += txtMargin_TextChanged;
            txtMarginTopNumeric.TextChanged += txtMargin_TextChanged;
            txtMarginBottomNumeric.TextChanged += txtMargin_TextChanged;


            this.Margins = (Margins)this.printDocument.DefaultPageSettings.Margins;
            this.PaperSize = (PaperSize)this.printDocument.DefaultPageSettings.PaperSize;

            //margins
            txtMarginBottomNumeric.Text = (Margins.Bottom / 100.0f).ToString();
            txtMarginBottomNumeric.Tag = Margins.Bottom;

            txtMarginLeftNumeric.Text = (Margins.Left / 100.0f).ToString();
            txtMarginLeftNumeric.Tag = Margins.Left;

            txtMarginRightNumeric.Text = (Margins.Right / 100.0f).ToString();
            txtMarginRightNumeric.Tag = Margins.Right;

            txtMarginTopNumeric.Text = (Margins.Top / 100.0f).ToString();
            txtMarginTopNumeric.Tag = Margins.Top;

            //paper size
            var paperSizes = printDocument.PrinterSettings.PaperSizes.Cast<PaperSize>().ToList();
            cmbPaperSize.DataSource = paperSizes;
            foreach(PaperSize p in cmbPaperSize.Items)
            {
                //for some reason p.Equals(PaperSize) does not work
                //even if the paper is the same
                //seems like a lazy Equals() implementation from MSFT
                if (p.PaperName == PaperSize.PaperName && 
                    p.Width == PaperSize.Width &&
                    p.Height == PaperSize.Height)
                {
                    cmbPaperSize.SelectedItem = p;
                }
            }
        }


        public void InitalizeComponentsCultureAware()
        {
            var lang = Settings.Get.General;

            this.Text = lang.GetString("PrintPageSetup");

            grpPaperSize.Text = lang.GetString("PrintPaperSize");
            lblPaperSize.Text = lang.GetString("PrintPaperSize");

            grpMargins.Text = lang.GetString("PrintMargins");
            lblTop.Text = lang.GetString("MarginTop");
            lblBottom.Text = lang.GetString("MarginBottom");
            lblLeft.Text = lang.GetString("MarginLeft");
            lblRight.Text = lang.GetString("MarginRight");

            radioInches.Text = lang.GetString("Inches");
            radioCm.Text = lang.GetString("Centimeters");

            btnCancel.Text = lang.GetString("Cancel");
            btnOK.Text = lang.GetString("OK");
        }

        private void txtMargin_TextChanged(object sender, EventArgs e)
        {
            NumericTextBox txt = (NumericTextBox)sender;
            decimal d = txt.Value;

            if (radioInches.Checked)
            {
                txt.Tag = (int)(d * 100m);
            }
            else
            {
                txt.Tag = (int)(d * 100m / 2.54m);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmPageSetup_Load(object sender, EventArgs e)
        {

        }

        private void cmbPaperSize_Format(object sender, ListControlConvertEventArgs e)
        {
            PaperSize p = (PaperSize)e.ListItem;
            e.Value = p.PaperName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //margins
            Margins.Left = (int)txtMarginLeftNumeric.Tag;
            Margins.Bottom = (int)txtMarginBottomNumeric.Tag;
            Margins.Right = (int)txtMarginRightNumeric.Tag;
            Margins.Top = (int)txtMarginTopNumeric.Tag;
            //paper size
            this.PaperSize = (PaperSize)cmbPaperSize.SelectedItem;
        }


        private void convertToInches(TextBox txt)
        {
            float fvalue = (float)((int)txt.Tag);
            txt.Text = (fvalue / 100.0f).ToString();
        }

        private void convertToCm(TextBox txt)
        {
            float fvalue = (float)((int)txt.Tag);
            txt.Text = (fvalue / 100.0f * 2.54f).ToString();
        }

        private void radioInches_CheckedChanged(object sender, EventArgs e)
        {
            if (radioInches.Checked)
            {
                convertToInches(txtMarginBottomNumeric);
                convertToInches(txtMarginLeftNumeric);
                convertToInches(txtMarginRightNumeric);
                convertToInches(txtMarginTopNumeric);
            }
            else
            {
                convertToCm(txtMarginBottomNumeric);
                convertToCm(txtMarginLeftNumeric);
                convertToCm(txtMarginRightNumeric);
                convertToCm(txtMarginTopNumeric);
            }
        }
    }
}
