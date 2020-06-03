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
    public partial class FrmSplashScreen : Form
    {
        private FrmMain frmMain;
        public FrmSplashScreen(FrmMain frmMain)
        {
            InitializeComponent();
            InitalizeComponentsCultureAware(Settings.Get.General.Culture.ThreeLetterISOLanguageName);
            this.frmMain = frmMain;
        }

        private void radLanguage_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rad = (RadioButton)sender;
            string language = (string)rad.Tag;

            Settings.Get.General.SetCulture(language);
            InitalizeComponentsCultureAware(language);
        }

        private void InitalizeComponentsCultureAware(string language)
        {
            var lang = Settings.Get.General;

            this.Text = lang.GetString("SplashTitle");
            lblFirstLaunch.Text = lang.GetString("SplashExplanation");
            lblSelected.Text = String.Format(lang.GetString("SplashSelected"), lang.GetString(language));
            btnOK.Text = lang.GetString("OK");

            toolTip.SetToolTip(radEn, lang.GetString("en"));
            toolTip.SetToolTip(radFr, lang.GetString("fr"));


        }

        private void FrmSplashScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMain.InitalizeComponentsCultureAware();
        }
    }
}
