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
    public partial class FrmPrintPreview : Form
    {

        private Bitmap bitmap;

        public FrmPrintPreview(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            InitializeComponent();
            this.Icon = Properties.Resources.imageview;
            printDocument.DefaultPageSettings.Margins.Bottom = 50;
            printDocument.DefaultPageSettings.Margins.Top = 50;
            printDocument.DefaultPageSettings.Margins.Left = 50;
            printDocument.DefaultPageSettings.Margins.Right = 50;
            refreshLandscapePortraitUI();
        }


        private void refreshLandscapePortraitUI()
        {
            toolStripButtonLandscape.Checked = printDocument.DefaultPageSettings.Landscape;
            toolStripButtonPortrait.Checked = !printDocument.DefaultPageSettings.Landscape;
        }
        private void setLandscape(bool b)
        {
            if(b != printDocument.DefaultPageSettings.Landscape)
            {
                printDocument.DefaultPageSettings.Landscape = b;
                refreshLandscapePortraitUI();
                printPreviewControl.InvalidatePreview();
            }
        }
        
        private void FrmPrintPreview_Load(object sender, EventArgs e)
        {
            
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {

            RectangleF area = printDocument.DefaultPageSettings.PrintableArea;
            Margins margins = printDocument.DefaultPageSettings.Margins;

            area.X += margins.Left;
            area.Y += margins.Top;
            area.Width = area.Width - margins.Left - margins.Right;
            area.Height = area.Height - margins.Top - margins.Bottom;

            if(area.Width > 0 && area.Height > 0)
            {
                //BestFit sizing and center to image
                RectangleF dstRect = new RectangleF();
                float aspec = (float)bitmap.Width / (float)bitmap.Height;
                float pageAspect = (float)area.Width / (float)area.Height;
                float zoom;

                if (aspec > pageAspect)
                {
                    zoom = (area.Width / (float)bitmap.Width);
                    dstRect.Width = area.Width;
                    dstRect.X = area.X;
                    dstRect.Height = (float)Math.Round((dstRect.Width / aspec));
                    dstRect.Y = (printDocument.DefaultPageSettings.PrintableArea.Height - dstRect.Height) / 2.0f;
                }
                else
                {
                    zoom = ((float)area.Height / (float)bitmap.Height);
                    dstRect.Height = area.Height;
                    dstRect.Y = area.Y;
                    dstRect.Width = (float)Math.Round((dstRect.Height * aspec));
                    dstRect.X = (printDocument.DefaultPageSettings.PrintableArea.Width - dstRect.Width) / 2.0f;
                }

                e.Graphics.DrawImage(bitmap, dstRect);
            }



        }

        private void toolStripButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButtonPortrait_Click(object sender, EventArgs e)
        {
            setLandscape(false);
        }

        private void toolStripButtonLandscape_Click(object sender, EventArgs e)
        {
            setLandscape(true);
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            printDialog.Document = printDocument;
            if(printDialog.ShowDialog() == DialogResult.OK)
            {
                printDialog.Document.Print();
            }
            refreshLandscapePortraitUI();
            printPreviewControl.InvalidatePreview();
        }

        private void toolStripButtonPageSetup_Click(object sender, EventArgs e)
        {
            FrmPageSetup f = new FrmPageSetup(ref printDocument);
            if(f.ShowDialog() == DialogResult.OK)
            {
                printDocument.DefaultPageSettings.Margins = f.Margins;
                printDocument.DefaultPageSettings.PaperSize = f.PaperSize;
                printPreviewControl.InvalidatePreview();
            }
        }

        private void FrmPrintPreview_FormClosing(object sender, FormClosingEventArgs e)
        {
            bitmap.Dispose();
        }
    }
}
