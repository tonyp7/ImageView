using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageView
{
    public partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();


            this.Text = String.Format("About " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            labelName.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            var v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            labelVersion.Text = String.Format("Version {0}.{1}.{2}", v.Major, v.Minor, v.Build);







        }

        private void pictureBoxDonate_Click(object sender, EventArgs e)
        {
            Program.LaunchURL("https://www.paypal.me/tonypottier");
        }

        private void FrmAbout_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lbl = (LinkLabel)sender;
            Program.LaunchURL(lbl.Text);
        }
    }
}
