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
