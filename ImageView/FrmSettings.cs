using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageView
{
    public partial class FrmSettings : Form
    {
        private Config configOriginal;
        private Config configNew;



        private void loadUIElements()
        {
            txtSlideshowTimer.Text = configNew.Slideshow.Timer.ToString();
            chkHistorySaveOnExit.Checked = configNew.History.SaveOnExit;
            txtHistoryMaxSize.Text = configNew.History.Size.ToString();
        }

        public FrmSettings(Config config)
        {
            InitializeComponent();
            this.configOriginal = config;
            this.configNew = (Config)config.Clone();
            loadUIElements();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMakeDefault_Click(object sender, EventArgs e)
        {
            Program.LaunchURL("ms-settings:defaultapps");
            //bool result = await System.Launcher.LaunchUriAsync(new Uri("ms-settings:defaultapps"));

        }



        private void validateNumericTextBox(object sender, EventArgs e, int defaultValue, int maxValue)
        {
            TextBox t = (TextBox)sender;
            t.Text = Regex.Replace(t.Text, "[^0-9]", "");

            uint i = 0;
            if (uint.TryParse(t.Text, out i))
            {
                if (i > maxValue)
                {
                    t.Text = maxValue.ToString();
                }
            }
            else
            {
                t.Text = defaultValue.ToString();
            }
        }


        private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtHistoryMaxSize_TextChanged(object sender, EventArgs e)
        {
            validateNumericTextBox(sender, e, ConfigHistory.DEFAULT_HISTORY_SIZE, ConfigHistory.MAXIMUM_HISTORY_SIZE);
        }

        private void txtSlideshowTimer_TextChanged(object sender, EventArgs e)
        {
            validateNumericTextBox(sender, e, ConfigSlideshow.DEFAULT_SLIDESHOW_TIMER, ConfigSlideshow.MAXIMUM_SLIDESHOW_TIMER);
        }
    }
}
