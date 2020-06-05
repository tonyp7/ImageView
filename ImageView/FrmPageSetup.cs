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



        public FrmPageSetup(ref PrintDocument printDocument)
        {
            InitializeComponent();
            this.printDocument = printDocument;

            this.Margins = (Margins)this.printDocument.DefaultPageSettings.Margins;
            this.PaperSize = (PaperSize)this.printDocument.DefaultPageSettings.PaperSize;

            //margins
            txtMarginBottom.Text = (Margins.Bottom / 100.0f).ToString();
            txtMarginBottom.Tag = Margins.Bottom;

            txtMarginLeft.Text = (Margins.Left / 100.0f).ToString();
            txtMarginLeft.Tag = Margins.Left;

            txtMarginRight.Text = (Margins.Right / 100.0f).ToString();
            txtMarginRight.Tag = Margins.Right;

            txtMarginTop.Text = (Margins.Top / 100.0f).ToString();
            txtMarginTop.Tag = Margins.Top;

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
            Margins.Left = (int)txtMarginLeft.Tag;
            Margins.Bottom = (int)txtMarginBottom.Tag;
            Margins.Right = (int)txtMarginRight.Tag;
            Margins.Top = (int)txtMarginTop.Tag;
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
                convertToInches(txtMarginBottom);
                convertToInches(txtMarginLeft);
                convertToInches(txtMarginRight);
                convertToInches(txtMarginTop);
            }
            else
            {
                convertToCm(txtMarginBottom);
                convertToCm(txtMarginLeft);
                convertToCm(txtMarginRight);
                convertToCm(txtMarginTop);
            }
        }
    }
}
