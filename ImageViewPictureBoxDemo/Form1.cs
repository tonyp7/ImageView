using ImageViewPictureBoxDemo.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageViewPictureBoxDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //CC0 Image by Myriam Zilles 
            this.pictureBox1.Bitmap = Resources.halloween_2893710_1920;

            this.pictureBox1.DragCursor = Properties.Resources.move;
            this.pictureBox1.ZoomInCursor = Properties.Resources.zoomin;
            this.pictureBox1.ZoomOutCursor = Properties.Resources.zoomout;


        }

        private void pictureBox1_ZoomChanged(object sender, ImageView.PictureBox.ZoomEventArgs e)
        {
            toolStripLabelZoom.Text = string.Format("Current Zoom: {0}%", (int)(e.Zoom * 100.0f));
        }

        private void pictureBox1_PixelCoordinatesChanged(object sender, ImageView.PictureBox.CoordinatesEventArgs e)
        {
            toolStripLabelCoord.Text = string.Format("Coordinates: {0:0},{1:0}", e.PixelCoordinates.X, e.PixelCoordinates.Y);
        }
    }
}
