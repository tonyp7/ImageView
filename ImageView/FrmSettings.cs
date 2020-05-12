﻿using System;
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
        }

        public FrmSettings(FrmMain frmMain)
        {
            InitializeComponent();
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
    }
}
