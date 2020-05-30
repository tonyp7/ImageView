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
    public partial class PictureBox : UserControl
    {

        public PictureBox()
        {
            InitializeComponent();
        }

        public Bitmap Image { get; set; }
        public InterpolationMode InterpolationMode { get; set; }

        public Rectangle SourceRectangle { get; set; }
        public Rectangle DestinationRectangle { get; set; }

        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            
            if (Image != null)
            {
                Rectangle rect = new Rectangle(0, 0, Image.Width, Image.Height);
                this.Width = rect.Width;
                this.Height = rect.Height;
                paintEventArgs.Graphics.DrawImageUnscaledAndClipped(Image, rect);
            }

        }
    }
}
