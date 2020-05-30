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
using ImageView.Configuration;
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
            InitalizeComponentsCultureAware();
        }

        public void InitalizeComponentsCultureAware()
        {

            var lang = Settings.Get.General;
            var v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            //title
            this.Text = String.Format(lang.GetString("AboutTitle"), System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);

            //dynamic stuff
            labelName.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            labelVersion.Text = String.Format(lang.GetString("AboutVersion"), v.Major, v.Minor, v.Build);

            //fixed stuff
            this.lblFreeAndOpenSource.Text = lang.GetString("AboutFreeAndOpenSource");
            this.labelCopyright.Text = lang.GetString("AboutLicense");
            this.lblSourceCodeRepo.Text = lang.GetString("AboutSourceCodeRepo");
            this.lblGetLatest.Text = lang.GetString("AboutGetLatest");
            this.lblPleaseDonate2.Text = lang.GetString("AboutPleaseDonate2");
            this.lblPleaseDonate.Text = lang.GetString("AboutPleaseDonate");
            this.lblCredits.Text = lang.GetString("AboutDependencies");
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
