﻿/*
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
using ImageView.Configuration;

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
                case ImageSizeMode.FitToWidth:
                    cmbOnLoadImageSizeMode.SelectedIndex = 2;
                    break;
                case ImageSizeMode.FitToHeight:
                    cmbOnLoadImageSizeMode.SelectedIndex = 3;
                    break;
                case ImageSizeMode.Zoom:
                    cmbOnLoadImageSizeMode.SelectedIndex = 4;
                    break;
                case ImageSizeMode.Restore:
                    cmbOnLoadImageSizeMode.SelectedIndex = 5;
                    break;
            }

            ////////////////////////
            // VIEWING TAB: AUTO ROTATE
            ////////////////////////
            chkAutoRotate.Checked = configNew.Display.AutoRotate;


            ////////////////////////
            // CHECKERED PATTERN
            ////////////////////////
            chkCheckeredPatternBackground.Checked = configNew.Display.CheckeredPatternBackground;

            ////////////////////////
            // LANGUAGE
            ////////////////////////
            string currentLanguage = configNew.General.Culture.TwoLetterISOLanguageName;
            switch (currentLanguage)
            {
                case "en":
                    cmbLanguage.SelectedIndex = 0;
                    break;
                case "fr":
                    cmbLanguage.SelectedIndex = 1;
                    break;
            }
        }

        public FrmSettings(FrmMain frmMain)
        {
            InitializeComponent();

            cmbOnLoadImageSizeMode.Items.AddRange(new string[] { String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty });
            cmbLanguage.Items.AddRange(new string[] { String.Empty, String.Empty});

            InitalizeComponentsCultureAware(); 
            
            this.frmMain = frmMain;
            this.configNew = (Config)Settings.Get.Clone();
            loadUIElements();

        }

        public void InitalizeComponentsCultureAware()
        {
            var lang = Settings.Get.General;


            this.Text = lang.GetString("Settings");
            this.tabPageGeneral.Text = lang.GetString("SettingsGeneral");
            this.grpHistory.Text = lang.GetString("SettingsHistory");
            this.lblHistoryExplanation.Text = lang.GetString("SettingsHistoryExplanation");  
            this.label5.Text = lang.GetString("SettingsHistoryMaxSize");
            this.chkHistorySaveOnExit.Text = lang.GetString("SettingsHistorySaveOnExit");
            this.grpSlideshow.Text = lang.GetString("Slideshow");
            this.lblTimeToDisplayEachImage.Text = lang.GetString("SettingsSlideshowTimer");
            this.grpDefaultApps.Text = lang.GetString("SettingsDefaultApp"); 
            this.lblDefaultAppsExplanation.Text = lang.GetString("SettingsDefaultAppExplanation");
            this.btnMakeDefault.Text = lang.GetString("SettingsChooseDefaultApps"); 
            this.tabPageView.Text = lang.GetString("SettingsImageViewing");
            this.grpAutoRotate.Text = lang.GetString("SettingsAutoRotate");
            this.chkAutoRotate.Text = lang.GetString("SettingsAutoRotateExplanation");
            this.grpDefaultSizeMode.Text = lang.GetString("SettingsDefaultSizeMode"); 
            this.lblDefaultSizeModeExplanation.Text = lang.GetString("SettingsDefaultSizeModeExplanation"); 

            this.btnApply.Text = lang.GetString("Apply");
            this.btnCancel.Text = lang.GetString("Cancel");
            this.btnOK.Text = lang.GetString("OK");


            
            cmbOnLoadImageSizeMode.Items[0] = lang.GetString("BestFit");
            cmbOnLoadImageSizeMode.Items[1] = lang.GetString("RealSize");
            cmbOnLoadImageSizeMode.Items[2] = lang.GetString("FitToWidth");
            cmbOnLoadImageSizeMode.Items[3] = lang.GetString("FitToHeight");
            cmbOnLoadImageSizeMode.Items[4] = lang.GetString("Zoom");
            cmbOnLoadImageSizeMode.Items[5] = lang.GetString("ImageModeRestore");

            cmbLanguage.Items[0] = lang.GetString("en");
            cmbLanguage.Items[1] = lang.GetString("fr");

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
                txtSlideshowTimer.Text = Settings.Get.Slideshow.Timer.ToString(); //something input by the user is wrong so current config value is restored
            }

            ////////////////////////
            // HISTORY
            ////////////////////////
            configNew.History.SaveOnExit = chkHistorySaveOnExit.Checked;
            if (int.TryParse(txtHistorySize.Text, out value) && value >= 0 && value <= ConfigHistory.MAXIMUM_HISTORY_SIZE)
            {
                configNew.History.MaxSize = value;
                //apply the new history size -- if the new value is less than the old we have some clean up to do
                if(value < Settings.Get.History.MaxSize)
                {
                    frmMain.RefreshHistoryList();
                }

            }
            else
            {
                txtSlideshowTimer.Text = Settings.Get.History.MaxSize.ToString(); //something input by the user is wrong so current config value is restored
            }


            ////////////////////////
            // VIEWING TAB: DEFAULT IMAGE SIZE MODE
            ////////////////////////
            switch (cmbOnLoadImageSizeMode.SelectedIndex)
            {
                case 0:
                    configNew.Display.SizeModeOnImageLoad = ImageSizeMode.BestFit;
                    break;
                case 1:
                    configNew.Display.SizeModeOnImageLoad = ImageSizeMode.RealSize;
                    break;
                case 2:
                    configNew.Display.SizeModeOnImageLoad = ImageSizeMode.FitToWidth;
                    break;
                case 3:
                    configNew.Display.SizeModeOnImageLoad = ImageSizeMode.FitToHeight;
                    break;
                case 4:
                    configNew.Display.SizeModeOnImageLoad = ImageSizeMode.Zoom;
                    break;
                default:
                    configNew.Display.SizeModeOnImageLoad = ImageSizeMode.Restore;
                    break;
            }

            ////////////////////////
            // VIEWING TAB: AUTO ROTATE
            ////////////////////////
            configNew.Display.AutoRotate = chkAutoRotate.Checked;

            ////////////////////////
            // CHECKERED PATTERN
            ////////////////////////
            configNew.Display.CheckeredPatternBackground = chkCheckeredPatternBackground.Checked;

            ////////////////////////
            // LANGUAGE
            ////////////////////////
            switch (cmbLanguage.SelectedIndex)
            {
                case 1:
                    configNew.General.SetCulture("fr");
                    break;
                case 0:
                default:
                    configNew.General.SetCulture("en");
                    break;
            }


            /////FINISH: New Config becomes old -- but first check if the language needs to be reloaded
            bool reloadLanguage = Settings.Get.General.Culture.TwoLetterISOLanguageName != configNew.General.Culture.TwoLetterISOLanguageName;
            Settings.Get = configNew;
            if (reloadLanguage)
            {
                InitalizeComponentsCultureAware();
                frmMain.InitalizeComponentsCultureAware();
            }
            frmMain.setCheckeredPatternBackground(configNew.Display.CheckeredPatternBackground);
            configNew = null;
            configNew = (Config)Settings.Get.Clone();

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
