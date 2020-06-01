using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ImageView.Components
{
    /// <summary>
    /// Place holder for a high performing blitter
    /// </summary>
    public class ImageBox : System.Windows.Forms.PictureBox
    {

        public ImageBox()
        {
            textureBrush = new TextureBrush(ImageView.Properties.Resources.transparent16);
            textureBrush.WrapMode = WrapMode.Tile;
            this.DoubleBuffered = true;
        }


        private TextureBrush textureBrush; 
        public InterpolationMode InterpolationMode { get; set; }

        private RectangleF sourceRectangle = RectangleF.Empty;
        private RectangleF targetRectangle = RectangleF.Empty;
        public RectangleF SourceRectangle 
        { 
            get { return sourceRectangle; } 
            set { sourceRectangle = value; }
        }
        public RectangleF TargetRectange
        {
            get { return targetRectangle; }
            set { targetRectangle = value; }
        }
        public bool CheckeredPatternBackground { get; set; }

        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            if (Image != null) {

                Graphics g = paintEventArgs.Graphics;
                g.PixelOffsetMode = PixelOffsetMode.None;

                if (CheckeredPatternBackground && Image.IsAlphaPixelFormat(Image.PixelFormat))
                {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.FillRectangle(textureBrush, 0, 0, this.Width, this.Height);
                }

                

                if(sourceRectangle != RectangleF.Empty)
                {
                    g.InterpolationMode = InterpolationMode.Default;
                    g.DrawImage(Image, targetRectangle, sourceRectangle, GraphicsUnit.Pixel);

                }
                else
                {
                    g.InterpolationMode = InterpolationMode.Default;
                    g.DrawImage(Image, 0, 0, this.Width, this.Height);
                }

                
            }
        }
    }
}
