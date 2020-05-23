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
                MessageBox.Show("There was an unexpected error while loading the license files.\nPlease refer to https://getimageview.net", "Error loading license files", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
