using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageMagick;

namespace ImageViewPreviewPanelDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnIco_Click(object sender, EventArgs e)
        {
            MagickImageCollection collection = new MagickImageCollection("imageview.ico");

            foreach(var image in collection)
            {

            }

        }
    }
}
