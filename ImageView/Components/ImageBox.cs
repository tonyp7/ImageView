/*
MIT License

Copyright (c) 2020 Tony Pottier

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

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
    /// A higher performance picturebox
    /// </summary>
    public class ImageBox : System.Windows.Forms.PictureBox
    {

        public ImageBox()
        {
            textureBrush = new TextureBrush(Properties.Resources.transparent16);
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
