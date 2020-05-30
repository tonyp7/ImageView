using ImageView.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageView
{
    public partial class FrmLicense : Form
    {
        public FrmLicense()
        {
            InitializeComponent();
            InitalizeComponentsCultureAware();
        }

        public void InitalizeComponentsCultureAware()
        {
            var lang = Settings.Get.General;

            this.Text = lang.GetString("License");
            lblLicense.Text = lang.GetString("LicenseMain");
            lblAdditional.Text = lang.GetString("LicenseAdditional");
        }

        private void FrmLicense_Load(object sender, EventArgs e)
        {
            try
            {
                string exeLocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string license = File.ReadAllText(Path.Combine(exeLocation, "LICENSE"));
                string additional = File.ReadAllText(Path.Combine(exeLocation, "DEPENDENCIES.md"));

                txtLicense.Text = license;
                txtAdditional.Text = additional;

                this.ActiveControl = btnOK;
            }
            catch (Exception)
            {
                var lang = Settings.Get.General;
                MessageBox.Show(lang.GetString("LicenseErrorMessage"), lang.GetString("LicenseErrorTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
