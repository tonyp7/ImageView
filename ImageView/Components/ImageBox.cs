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

        public Rectangle SourceRectange { get; set; }
        public Rectangle TargetRectange { get; set; }
        public bool CheckeredPatternBackground { get; set; }

        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            if (Image != null) {

                paintEventArgs.Graphics.PixelOffsetMode = PixelOffsetMode.None;

                if (CheckeredPatternBackground && Image.IsAlphaPixelFormat(Image.PixelFormat))
                {
                    paintEventArgs.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                    paintEventArgs.Graphics.FillRectangle(textureBrush, 0, 0, this.Width, this.Height);
                }

                paintEventArgs.Graphics.InterpolationMode = InterpolationMode.Default;
                paintEventArgs.Graphics.DrawImage(Image, 0, 0, this.Width, this.Height);
            }
        }
    }
}
