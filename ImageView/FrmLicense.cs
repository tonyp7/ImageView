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
