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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageView
{
    public partial class FrmSettings : Form
    {
        private FrmMain frmMain;
        private Config configNew;



        private void loadUIElements()
        {
            txtSlideshowTimer.Text = configNew.Slideshow.Timer.ToString();
            chkHistorySaveOnExit.Checked = configNew.History.SaveOnExit;
            txtHistorySize.Text = configNew.History.MaxSize.ToString();

            
            switch (configNew.Display.SizeModeOnImageLoad)
            {
                case ImageSizeMode.BestFit:
                    cmbOnLoadImageSizeMode.SelectedIndex = 0;
                    break;
                case ImageSizeMode.RealSize:
                    cmbOnLoadImageSizeMode.SelectedIndex = 1;
                    break;
                case ImageSizeMode.Zoom:
                    cmbOnLoadImageSizeMode.SelectedIndex = 2;
                    break;
                case ImageSizeMode.Restore:
                    cmbOnLoadImageSizeMode.SelectedIndex = 3;
                    break;
            }
        }

        public FrmSettings(FrmMain frmMain)
        {
            InitializeComponent();
            cmbOnLoadImageSizeMode.Items.AddRange(new string[] { "Best Fit", "Real Size", "Zoom", "Same as last viewed image" });

            this.frmMain = frmMain;
            this.configNew = (Config)frmMain.config.Clone();
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

        private void btnApply_Click(object sender, EventArgs e)
        {
            apply();
        }



        private void apply()
        {
            

            int value;

            ////////////////////////
            // SLIDESHOW
            ////////////////////////
            if(int.TryParse(txtSlideshowTimer.Text, out value) && value > 0 && value <= ConfigSlideshow.MAXIMUM_SLIDESHOW_TIMER)
            {
                configNew.Slideshow.Timer = value;
            }
            else
            {
                txtSlideshowTimer.Text = frmMain.config.Slideshow.Timer.ToString(); //something input by the user is wrong so current config value is restored
            }

            ////////////////////////
            // HISTORY
            ////////////////////////
            configNew.History.SaveOnExit = chkHistorySaveOnExit.Checked;
            if (int.TryParse(txtHistorySize.Text, out value) && value >= 0 && value <= ConfigHistory.MAXIMUM_HISTORY_SIZE)
            {
                configNew.History.MaxSize = value;
                //apply the new history size -- if the new value is less than the old we have some clean up to do
                if(value < frmMain.config.History.MaxSize)
                {
                    frmMain.removeExcessHistoryItems(value);
                }

            }
            else
            {
                txtSlideshowTimer.Text = frmMain.config.History.MaxSize.ToString(); //something input by the user is wrong so current config value is restored
            }


            ////////////////////////
            // VIEW
            ////////////////////////
            string onImageLoadSizeMode = (string)cmbOnLoadImageSizeMode.SelectedItem;
            switch (onImageLoadSizeMode)
            {
                case "Best Fit":
                    configNew.Display.SizeModeOnImageLoad = ImageSizeMode.BestFit;
                    break;
                case "Real Size":
                    configNew.Display.SizeModeOnImageLoad = ImageSizeMode.RealSize;
                    break;
                case "Zoom":
                    configNew.Display.SizeModeOnImageLoad = ImageSizeMode.Zoom;
                    break;
                default:
                    configNew.Display.SizeModeOnImageLoad = ImageSizeMode.Restore;
                    break;
            }


            /////FINISH: New Config becomes old
            frmMain.config = configNew;
            configNew = null;
            configNew = (Config)frmMain.config.Clone();

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            apply();
            this.Close();
        }

        private void cmbNextImageSizeMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
