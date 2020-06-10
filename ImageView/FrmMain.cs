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
using ImageMagick;
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
using ImageView.Configuration;
using ImageView.ImageEntry;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Resources;
using ImageView.Components;

namespace ImageView
{
    public partial class FrmMain : Form
    {

        class FullScreenSaveState
        {
            public FormWindowState WindowState;
            public Point Location;
            public Size Size;

            public FullScreenSaveState()
            {
                WindowState = FormWindowState.Normal;
                Location = new Point();
                Size = new Size();
            }
        }



        public enum Tool
        {
            None,
            Zoom
        }


        private bool fullscreen = false;
        private FullScreenSaveState fullScreenSaveState = new FullScreenSaveState();
        private Tool activeTool = Tool.None;
        private ViewingMode viewingMode = ViewingMode.Normal;


        public FrmMain()
        {
            InitializeComponent();

            Settings.Get.Load();

            // The custom picture box
            // 
            // pictureBox
            // 
            //this.pictureBox.CheckeredPatternBackground = Settings.Get.Display.CheckeredPatternBackground;
            this.pictureBox.DragCursor = Properties.Resources.move;
            this.pictureBox.ZoomInCursor = Properties.Resources.zoomin;
            this.pictureBox.ZoomOutCursor = Properties.Resources.zoomout;


            //Zoom list
            foreach (float f in ConfigDisplay.ZOOM_STEPS)
            {
                this.toolStripComboBoxZoom.Items.Add(String.Format("{0}%", f*100.0f));
            }

            this.openFileDialog.Filter = Properties.Resources.SupportedImageFiles;


            //multilingual settings
            InitalizeComponentsCultureAware();

            close();

            //restore history
            toolStripComboBoxNavigation.SelectedIndexChanged -= toolStripComboBoxNavigation_SelectedIndexChanged;
            toolStripComboBoxNavigation.ComboBox.DataSource = Settings.Get.History.Get();
            toolStripComboBoxNavigation.SelectedIndexChanged += toolStripComboBoxNavigation_SelectedIndexChanged;

            //set check mark on the type of image view
            refreshImageSizeModeUI();

            //restore window size
            if (Settings.Get.Window.Width != 0 && Settings.Get.Window.Height != 0)
            {
                this.Location = new Point(Settings.Get.Window.X, Settings.Get.Window.Y);
                this.Width = Settings.Get.Window.Width;
                this.Height = Settings.Get.Window.Height;
                this.WindowState = Settings.Get.Window.State;
            }
        }



        public void InitalizeComponentsCultureAware()
        {

            this.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();

            this.menuStrip.Text = String.Empty;
            this.fileToolStripMenuItem.Text = Settings.Get.General.GetString("MenuFile");
            this.toolStripMenuItemOpen.Text = Settings.Get.General.GetString("Open");
            this.closeToolStripMenuItem.Text = Settings.Get.General.GetString("Close");
            this.deleteToolStripMenuItem.Text = Settings.Get.General.GetString("Delete");
            this.exitToolStripMenuItem.Text = Settings.Get.General.GetString("Exit");
            this.editToolStripMenuItem.Text = Settings.Get.General.GetString("Edit");
            this.copyToolStripMenuItem.Text = Settings.Get.General.GetString("Copy");
            this.imageToolStripMenuItem.Text = Settings.Get.General.GetString("Image");
            this.informationToolStripMenuItem.Text = Settings.Get.General.GetString("Information");
            this.rotateLeftToolStripMenuItem.Text = Settings.Get.General.GetString("RotateLeft");
            this.rotateRightToolStripMenuItem.Text = Settings.Get.General.GetString("RotateRight");
            this.verticalFlipToolStripMenuItem.Text = Settings.Get.General.GetString("VerticalFlip");
            this.horizontalFlipToolStripMenuItem.Text = Settings.Get.General.GetString("HorizontalFlip");
            this.viewToolStripMenuItem.Text = Settings.Get.General.GetString("View");
            this.fullscreenToolStripMenuItem.Text = Settings.Get.General.GetString("Fullscreen");
            this.slideshowToolStripMenuItem.Text = Settings.Get.General.GetString("Slideshow");
            this.readerModeToolStripMenuItem.Text = Settings.Get.General.GetString("ReaderMode");
            this.previousToolStripMenuItem.Text = Settings.Get.General.GetString("Previous");
            this.nextToolStripMenuItem.Text = Settings.Get.General.GetString("Next");
            this.zoomToolToolStripMenuItem.Text = Settings.Get.General.GetString("ZoomTool");
            this.optionsToolStripMenuItem.Text = Settings.Get.General.GetString("Options");
            this.settingsToolStripMenuItem.Text = Settings.Get.General.GetString("Settings");
            this.helpToolStripMenuItem.Text = Settings.Get.General.GetString("Help");
            this.checkForUpdatesToolStripMenuItem.Text = Settings.Get.General.GetString("CheckForUpdates");
            this.licenseToolStripMenuItem.Text = Settings.Get.General.GetString("License");
            this.aboutToolStripMenuItem.Text = Settings.Get.General.GetString("About");
            this.toolStrip.Text = String.Empty;
            this.toolStripButtonOpen.Text = Settings.Get.General.GetString("OpenImageFile");
            this.toolStripButtonSaveAs.Text = Settings.Get.General.GetString("SaveImageCopyAs");
            this.toolStripButtonDelete.Text = Settings.Get.General.GetString("DeleteImage");
            this.toolStripButtonSlideShow.Text = Settings.Get.General.GetString("Slideshow");
            this.toolStripReaderMode.Text = Settings.Get.General.GetString("ReaderMode");
            this.toolStripButtonPrevious.Text = Settings.Get.General.GetString("PreviousImage");
            this.toolStripButtonNext.Text = Settings.Get.General.GetString("NextImage");
            this.realSizeToolStripMenuItem.Text = Settings.Get.General.GetString("RealSize");
            this.BestFitToolStripMenuItem.Text = Settings.Get.General.GetString("BestFit");
            this.fitToWidthToolStripMenuItem.Text = Settings.Get.General.GetString("FitToWidth");
            this.fitToHeightToolStripMenuItem.Text = Settings.Get.General.GetString("FitToHeight");
            this.toolStripComboBoxZoom.Text = String.Empty;
            this.toolStripButtonSettings.Text = Settings.Get.General.GetString("Settings");
            this.statusStrip.Text = String.Empty;
            this.toolStripStatusLabelImageInfo.Text = Settings.Get.General.GetString("WelcomeStatus");
            this.toolStripStatusLabelImagePosition.Text = String.Empty;
            this.toolStripStatusLabelZoom.Text = String.Empty;
            this.toolStripStatusLabelFileSize.Text = String.Empty;
            this.toolStripStatusLabelPixelPosition.Text = String.Empty;
            this.printToolStripMenuItem.Text = Settings.Get.General.GetString("PrintPreviewAlternate");

            toolStripDropDownButtonDisplayType.ToolTipText = Settings.Get.General.GetString("PickImageSizeMode");

            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #region Events - Mouse

        private void FrmMain_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0) //scroll down
            {
                next();
            }
            else
            {
                previous();
            }
        }

        //TODO: FIXME with new picture box
        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;

            //prevents a fullscreen trigger when trying to zoom on the picture
            if (activeTool != Tool.Zoom)
            {
                if (me.Button == MouseButtons.Left)
                {
                    if (fullscreen)
                    {
                        exitFullScreen();
                    }
                    else
                    {
                        enterFullScreen();
                    }
                }
            }
        }
        #endregion

        #region Events - Form
        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (Settings.Get.General.FirstLaunch)
            {
                new FrmSplashScreen(this).ShowDialog();
            }

            resizeNavigationBar();

            string[] args = Environment.GetCommandLineArgs();
            if (args != null && args.Length >= 2)
            {
                if (Program.State.LoadPicture(args[1]))
                {
                    loadPictureUI();
                }
            }
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            resizeNavigationBar();
            pictureBox.Refresh();
        }


        /// <summary>
        /// Saves the current window state to be able to restore it on next launch
        /// <seealso cref="FrmMain_FormClosed(object, FormClosedEventArgs)"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Save last window size and position
            if (this.WindowState == FormWindowState.Maximized)
            {
                Settings.Get.Window.X = this.RestoreBounds.X;
                Settings.Get.Window.Y = this.RestoreBounds.Y;
                Settings.Get.Window.Width = this.RestoreBounds.Width;
                Settings.Get.Window.Height = this.RestoreBounds.Height;
                Settings.Get.Window.State = this.WindowState;
            }
            else
            {
                Settings.Get.Window.X = this.Location.X;
                Settings.Get.Window.Y = this.Location.Y;
                Settings.Get.Window.Width = this.Width;
                Settings.Get.Window.Height = this.Height;
                Settings.Get.Window.State = this.WindowState;
            }

        }


        /// <summary>
        /// At form closure there's a silent attempt to save the current configuration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Get.Save();
        }


        /// <summary>
        /// Implements a lot of shortcut keys when Windows Forms does not natively support the shortcuts. For instance, single key shortcuts are invalid for winforms
        /// so this method contains many single key shortcuts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.PageUp:
                    previous();
                    break;
                case Keys.Right:
                case Keys.PageDown:
                    next();
                    break;
                case Keys.Down:
                    scrollDown();
                    break;
                case Keys.Up:
                    scrollUp();
                    break;
                case Keys.P:
                    showSettings();
                    break;
                case Keys.I:
                    showInformation();
                    break;
                case Keys.F:
                    toggleFullScreen();
                    break;
                case Keys.L:
                    rotateLeft();
                    break;
                case Keys.R:
                    rotateRight();
                    break;
                case Keys.V:
                    verticalFlip();
                    break;
                case Keys.H:
                    horizontalFlip();
                    break;
                case Keys.Z:
                    toggleZoomTool();
                    break;
                case Keys.Escape:
                    if (activeTool == Tool.Zoom)
                    {
                        exitZoomTool();
                    }
                    else if (fullscreen)
                    {
                        exitFullScreen();
                        timerSlideShow.Stop();
                    }
                    break;
            }
        }


        #endregion

        #region Events - Menu

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreview();
        }

        private void readerModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toggleReaderMode();
        }

        private void bestFitStripMenuItem_Click(object sender, EventArgs e)
        {
            setImageSizeMode(ImageSizeMode.BestFit);
        }
        private void realSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setImageSizeMode(ImageSizeMode.RealSize);
        }

        private void fitToWidthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setImageSizeMode(ImageSizeMode.FitToWidth);
        }

        private void fitToHeightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setImageSizeMode(ImageSizeMode.FitToHeight);
        }
        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmLicense().ShowDialog();
        }
        private void rotateLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rotateLeft();
        }

        private void rotateRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rotateRight();
        }


        private void verticalFlipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            verticalFlip();
        }

        private void horizontalFlipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            horizontalFlip();
        }

        private void zoomToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toggleZoomTool();
        }
        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showInformation();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copy();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSettings();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout a = new FrmAbout();
            a.ShowDialog();
        }

        private void slideshowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //enterSlideshow();
            setViewMode(ViewingMode.Slideshow);
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            close();
        }


        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void fullscreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toggleFullScreen();
        }


        #endregion

        #region Events - Toolbar
        private void toolStripButtonPrintPreview_Click(object sender, EventArgs e)
        {
            printPreview();
        }


        private void toolStripReaderMode_Click(object sender, EventArgs e)
        {
            toggleReaderMode();
        }
        private void toolStripButtonSettings_Click(object sender, EventArgs e)
        {
            showSettings();
        }

        private void toolStripButtonSlideShow_Click(object sender, EventArgs e)
        {
            //enterSlideshow();
            setViewMode(ViewingMode.Slideshow);
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            openFile();
        }
        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void toolStripButtonNext_Click(object sender, EventArgs e)
        {
            next();
        }

        private void toolStripButtonPrevious_Click(object sender, EventArgs e)
        {
            previous();
        }
        private void toolStripComboBoxNavigation_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBoxNavigation_SelectedIndexChanged(object sender, EventArgs e)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("toolStripComboBoxNavigation_SelectedIndexChanged" + sender.ToString());
#endif
            TextRepresentationEntry tre = (TextRepresentationEntry)toolStripComboBoxNavigation.SelectedItem;
            this.ActiveControl = null;
            if (Program.State.LoadPicture(tre))
            {
                loadPictureUI();
            }

            //This fixes a weird refresh issue where the button to expand the dropdown stays focused after the selection change
            toolStripComboBoxNavigation.Select(0, 0);
            toolStripComboBoxNavigation.Invalidate();
            toolStripComboBoxNavigation.Select(0, 0);

        }

        private void toolStripComboBoxZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            zoom();
        }

        private void toolStripComboBoxZoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                zoom();
                e.Handled = true;
            }
        }

        #endregion

        #region Events - Others

        private void timerSlideShow_Tick(object sender, EventArgs e)
        {
            next();
        }

        #endregion

        #region Events - Picturebox

        private void pictureBox_ZoomChanged(object sender, PictureBox.ZoomEventArgs e)
        {
            toolStripStatusLabelZoom.Text = String.Format("{0} %", (int)(pictureBox.Zoom * 100.0f));
            toolStripComboBoxZoom_UpdateText(String.Format("{0}%", (int)(pictureBox.Zoom * 100.0f)));
        }

        private void pictureBox_PixelCoordinatesChanged(object sender, PictureBox.CoordinatesEventArgs e)
        {
            if (e.OutOfBounds)
            {
                toolStripStatusLabelPixelPosition.Visible = false;
                toolStripStatusLabelPixelPosition.Text = String.Empty;
            }
            else
            {
                toolStripStatusLabelPixelPosition.Visible = true;
                toolStripStatusLabelPixelPosition.Text = string.Format("{0:0},{1:0}", e.PixelCoordinates.X, e.PixelCoordinates.Y);
            }
        }

        private void pictureBox_KeyDown(object sender, KeyEventArgs e)
        {
            FrmMain_KeyDown(sender, e);
        }

        private void pictureBox_DragEnter(object sender, DragEventArgs e)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("pictureBox_DragEnter");
#endif
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                var settings = Settings.Get;
                if (s != null & s.Count() > 0)
                {
                    if (Config.ExtensionFilter.Any(x => s[0].ToLower().EndsWith(x)))
                    {
                        e.Effect = DragDropEffects.All;
                        return;
                    }
                }
            }

            e.Effect = DragDropEffects.None;
        }

        private void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (s != null & s.Count() > 0)
            {
                if (Program.State.LoadPicture(s[0]))
                {
                    loadPictureUI();
                }
            }

        }

        #endregion

        #region UI Updates


        /// <summary>
        /// This is a key function that actually display the loaded image on screen
        /// </summary>
        private void loadPictureUI()
        {
            var state = Program.State;
            var settings = Settings.Get;

            pictureBox.Bitmap = null;

            //set proper image size mode before loading the bitmap.
            if (settings.Display.SizeModeOnImageLoad != ImageSizeMode.Restore &&  (int)settings.Display.SizeModeOnImageLoad != (int)pictureBox.SizeMode)
            {
                setImageSizeMode(settings.Display.SizeModeOnImageLoad);
            }

            //bitmap to picturebox!
            pictureBox.Bitmap = state.Bitmap;

            //peripheral UI elements


            //force refresh history. If this isnt done the new history order doesnt get refreshed
            RefreshHistoryList();

            toolStripComboBoxNavigation_UpdateText(state.ActiveEntry.FullName);

            toolStripStatusLabelImageInfo.Text = String.Format("{0} x {1} - {2} {3}", state.NativeImage.BaseWidth, state.NativeImage.BaseHeight, state.NativeImage.ColorSpace, state.NativeImage.ColorType);
            toolStripStatusLabelImageInfo.Visible = true;
            toolStripStatusLabelFileSize.Text = Program.NiceFileSize(state.ActiveEntry.Length);
            toolStripStatusLabelFileSize.Visible = true;
            toolStripStatusLabelImagePosition.Text = String.Format("{0} / {1}", state.ActiveEntryIndex + 1, state.Entries.Count);
            toolStripStatusLabelImagePosition.Visible = true;
            this.Text = String.Format("{0} - {1}", state.ActiveEntry.FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// Prevents a useless picture reload if the image is already in history. See also: toolStripComboBoxZoom_UpdateText
        /// </summary>
        /// <param name="value"></param>
        private void toolStripComboBoxNavigation_UpdateText(string value)
        {
            toolStripComboBoxNavigation.SelectedIndexChanged -= toolStripComboBoxNavigation_SelectedIndexChanged;
            toolStripComboBoxNavigation.Text = value;
            toolStripComboBoxNavigation.SelectedIndexChanged += toolStripComboBoxNavigation_SelectedIndexChanged;
        }

        /// <summary>
        /// In case the calculated zoom level matches exactly a zoom preset (like 50%), the event SelectedIndexChanged is fired.
        /// In order to programmatically change the combo box without firing the event, we:
        ///     - Unbind the SelectedIndexChanged
        ///     - Update the text
        ///     - Rebind the SelectedIndexChanged event
        /// An alternative is to check for the control focus on the SelectedIndexChanged event. as in:
        ///     ToolStripComboBox cb = (ToolStripComboBox)sender;
        ///     if (!cb.Focused) return;
        /// Both methods have merits, but it is preferred here not to mess with focuses
        /// </summary>
        /// <param name="value">New value to assign the combo box .Text property</param>
        private void toolStripComboBoxZoom_UpdateText(string value)
        {
            toolStripComboBoxZoom.SelectedIndexChanged -= toolStripComboBoxZoom_SelectedIndexChanged;
            toolStripComboBoxZoom.Text = value;
            toolStripComboBoxZoom.SelectedIndexChanged += toolStripComboBoxZoom_SelectedIndexChanged;
        }

        #endregion

        #region private methods

        /// <summary>
        /// Change image size mode, refresh UI elements and resize the picture box
        /// </summary>
        /// <param name="sz">New image size mode to be set</param>
        private void setImageSizeMode(ImageSizeMode sz)
        {
            Settings.Get.Display.SizeMode = sz;
            refreshImageSizeModeUI();
            pictureBox.SizeMode = (ImageView.SizeMode)sz;
        }



        private void enterReader()
        {
            setImageSizeMode(Settings.Get.Reader.SizeMode);
            toolStripReaderMode.BackColor = SystemColors.MenuHighlight;
        }

        private void exitReader()
        {
            if (Settings.Get.Display.SizeModeOnImageLoad != ImageSizeMode.Restore)
            {
                setImageSizeMode(Settings.Get.Display.SizeModeOnImageLoad);
            }
            toolStripReaderMode.BackColor = SystemColors.Control;
        }

        private void refreshImageSizeModeUI()
        {
            switch (Settings.Get.Display.SizeMode)
            {
                case ImageSizeMode.BestFit:
                    BestFitToolStripMenuItem.Image = Properties.Resources.expand_arrows_tick16;
                    realSizeToolStripMenuItem.Image = ImageView.Properties.Resources.expand_solid16;
                    fitToWidthToolStripMenuItem.Image = ImageView.Properties.Resources.fith16;
                    fitToHeightToolStripMenuItem.Image = ImageView.Properties.Resources.fitv16;
                    break;
                case ImageSizeMode.RealSize:
                    BestFitToolStripMenuItem.Image = ImageView.Properties.Resources.expand_arrows16;
                    realSizeToolStripMenuItem.Image = ImageView.Properties.Resources.expand_solid_tick16;
                    fitToWidthToolStripMenuItem.Image = ImageView.Properties.Resources.fith16;
                    fitToHeightToolStripMenuItem.Image = ImageView.Properties.Resources.fitv16;
                    break;
                case ImageSizeMode.FitToWidth:
                    BestFitToolStripMenuItem.Image = ImageView.Properties.Resources.expand_arrows16;
                    realSizeToolStripMenuItem.Image = ImageView.Properties.Resources.expand_solid16;
                    fitToWidthToolStripMenuItem.Image = ImageView.Properties.Resources.fith_tick16;
                    fitToHeightToolStripMenuItem.Image = ImageView.Properties.Resources.fitv16;
                    break;
                case ImageSizeMode.FitToHeight:
                    BestFitToolStripMenuItem.Image = ImageView.Properties.Resources.expand_arrows16;
                    realSizeToolStripMenuItem.Image = ImageView.Properties.Resources.expand_solid16;
                    fitToWidthToolStripMenuItem.Image = ImageView.Properties.Resources.fith16;
                    fitToHeightToolStripMenuItem.Image = ImageView.Properties.Resources.fitv_tick16;
                    break;
                default:
                    BestFitToolStripMenuItem.Image = ImageView.Properties.Resources.expand_arrows16;
                    realSizeToolStripMenuItem.Image = ImageView.Properties.Resources.expand_solid16;
                    fitToWidthToolStripMenuItem.Image = ImageView.Properties.Resources.fith16;
                    fitToHeightToolStripMenuItem.Image = ImageView.Properties.Resources.fitv16;
                    break;
            }

        }

        private void enterZoomTool()
        {
            activeTool = Tool.Zoom;

            var coord = pictureBox.PointToClient(Cursor.Position);
            if (pictureBox.DisplayRectangle.Contains(coord))
            {
                this.Cursor = new Cursor(Properties.Resources.zoomin.Handle);
            }
        }

        private void exitZoomTool()
        {
            activeTool = Tool.None;
            if (this.Cursor != Cursors.Default)
                this.Cursor = Cursors.Default;
        }

        private void toggleZoomTool()
        {
            if (activeTool != Tool.Zoom)
            {
                enterZoomTool();
            }
            else
            {
                exitZoomTool();
            }
        }
        private void showSettings()
        {
            FrmSettings f = new FrmSettings(this);
            f.ShowDialog();
        }
        private void resizeNavigationBar()
        {
            toolStripComboBoxNavigation.Width = (int)(0.5f * this.Width);
        }
        private void printPreview()
        {
            var state = Program.State;
            if (state.Bitmap != null)
            {
                (new FrmPrintPreview((Bitmap)state.Bitmap.Clone())).Show();
            }
        }

        /// <summary>
        /// This function parses the value of the zoom box and assign the zoom to the picture
        /// </summary>
        private void zoom()
        {
            float zoom = 1.0f;
            if (float.TryParse(toolStripComboBoxZoom.Text.Trim(' ', '%'), out zoom))
            {
                zoom /= 100.0f;
                zoom = Program.Clamp(zoom, .01f, ConfigDisplay.MAX_ZOOM);
                Settings.Get.Display.Zoom = zoom;
                pictureBox.Zoom = zoom;

                setImageSizeMode(Settings.Get.Display.SizeMode = ImageSizeMode.Zoom);
                
                //remove focus from the textbox so that user can navigate with arrow keys etc.
                this.ActiveControl = null;
            }
        }

        private void openFile()
        {
            DialogResult dr = openFileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (Program.State.LoadPicture(openFileDialog.FileName))
                {
                    loadPictureUI();
                }
            }
        }



        private void verticalScroll(int direction)
        {
            //TODO: REDO
            //if (panelMain.VerticalScroll.Visible)
            //{
            //    Point scroll = panelMain.AutoScrollPosition;

            //    scroll.X *= -1;
            //    scroll.Y = -scroll.Y + (panelMain.Height / 10) * direction;

            //    panelMain.AutoScrollPosition = scroll;

            //    //REFRESH DRAWING PORTION
            //    float zoom = workingData.calculatedZoom;
            //    Size clientSize = panelMain.Size;
            //    RectangleF srcRect = new RectangleF();
            //    RectangleF dstRect = new RectangleF();
            //    dstRect.X = scroll.X;
            //    dstRect.Y = scroll.Y;
            //    dstRect.Width = clientSize.Width;
            //    dstRect.Height = clientSize.Height;
            //    srcRect.X = (((float)scroll.X / zoom));
            //    srcRect.Y = (((float)scroll.Y / zoom));
            //    srcRect.Width = (((float)clientSize.Width / zoom));
            //    srcRect.Height = (((float)clientSize.Height / zoom));
            //    pictureBox.SourceRectangle = srcRect;
            //    pictureBox.TargetRectange = dstRect;
            //}


        }



        /// <summary>
        /// TODO: find the next existing file. If a file doesnt exist force a reload of the folder structure. apply the same to previous
        /// </summary>
        private void next()
        {
            if (Program.State.Next())
            {

                loadPictureUI();

            }
            //    if (workingData.directoryIndex != -1)
            //    {
            //        workingData.directoryIndex++;

            //        //loop
            //        if (workingData.directoryIndex >= workingData.entries.Count)
            //        {
            //            workingData.directoryIndex = 0;
            //        }

            //        IEntry entry = workingData.entries[workingData.directoryIndex];
            //        loadPicture(entry);
            //    }
        }
        private void previous()
        {

            if (Program.State.Previous())
            {
                loadPictureUI();
            }

            //if (workingData.directoryIndex != -1)
            //{
            //    workingData.directoryIndex--;

            //    //loop
            //    if (workingData.directoryIndex < 0)
            //    {
            //        workingData.directoryIndex = workingData.entries.Count - 1;
            //    }

            //    IEntry entry = workingData.entries[workingData.directoryIndex];
            //    loadPicture(entry);
            //}

        }


        private void copy()
        {
            if (Program.State.Bitmap != null)
            {
                Clipboard.SetImage(Program.State.Bitmap);
            }
        }

        private void showInformation()
        {
            if (Program.State.ActiveEntry != null)
            {
                FrmInformation f = new FrmInformation();
                f.ShowDialog();
            }
        }


        private void exitFullScreen()
        {

            WinTaskbar.Show();
            toolStrip.Visible = true;
            statusStrip.Visible = true;
            menuStrip.Visible = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.TopMost = false;
            pictureBox.BorderStyle = BorderStyle.Fixed3D;

            //restore window state
            this.WindowState = fullScreenSaveState.WindowState;
            this.Location = fullScreenSaveState.Location;
            this.Size = fullScreenSaveState.Size;

            //if slideshow was on we disable it
            if (timerSlideShow.Enabled) timerSlideShow.Stop();

            fullscreen = false;
        }
        private void enterFullScreen()
        {


            //save window state before entering full screen so that it can be restored when exiting
            fullScreenSaveState.WindowState = this.WindowState;
            if (this.WindowState == FormWindowState.Maximized)
            {
                fullScreenSaveState.Location = this.RestoreBounds.Location;
                fullScreenSaveState.Size = this.RestoreBounds.Size;
            }
            else
            {
                fullScreenSaveState.Location = this.Location;
                fullScreenSaveState.Size = this.Size;
            }


            WinTaskbar.Hide();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            //if the panel does not contain anything, setting the border to none completely breaks the layout for
            //some odd reason. Seems to be a WinForms bug and this is the workaround
            if (pictureBox.Bitmap != null)
                pictureBox.BorderStyle = BorderStyle.None;

            this.TopMost = true;

            this.WindowState = FormWindowState.Normal;
            this.Location = new Point(0, 0);
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Size.Width, Screen.PrimaryScreen.Bounds.Size.Height);

            toolStrip.Visible = false;
            statusStrip.Visible = false;
            menuStrip.Visible = false;
            fullscreen = true;
        }


        private void toggleFullScreen()
        {
            if (fullscreen)
            {
                exitFullScreen();
            }
            else
            {
                enterFullScreen();
            }
        }


        private void toggleReaderMode()
        {
            if (viewingMode == ViewingMode.Reader)
            {
                setViewMode(ViewingMode.Normal);
            }
            else
            {
                setViewMode(ViewingMode.Reader);
            }
        }

        private void setViewMode(ViewingMode vm)
        {

            if (vm != viewingMode)
            {
                switch (viewingMode)
                {
                    case ViewingMode.Slideshow:
                        exitSlideshow();
                        break;
                    case ViewingMode.Reader:
                        exitReader();
                        break;
                }
            }

            switch (vm)
            {
                case ViewingMode.Slideshow:
                    enterSlideshow();
                    break;
                case ViewingMode.Reader:
                    if (timerSlideShow.Enabled) timerSlideShow.Stop();
                    enterReader();
                    break;
                case ViewingMode.Normal:
                    if (timerSlideShow.Enabled) timerSlideShow.Stop();
                    break;
            }

            viewingMode = vm;
        }



        private void exitSlideshow()
        {
            exitFullScreen();
        }
        private void enterSlideshow()
        {

            setImageSizeMode(Settings.Get.Slideshow.SizeMode);

            enterFullScreen();
            timerSlideShow.Interval = Settings.Get.Slideshow.Timer;
            timerSlideShow.Start();
        }


        private void horizontalFlip()
        {
            if (Program.State.HorizontalFlip())
            {
                pictureBox.Bitmap = Program.State.Bitmap;
            }
            //if (workingData.bitmap != null)
            //{
            //    workingData.bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            //    pictureBox.Bitmap = workingData.bitmap;
            //}
        }

        private void verticalFlip()
        {
            if (Program.State.VerticalFlip())
            {
                pictureBox.Bitmap = Program.State.Bitmap;
            }
            //if (workingData.bitmap != null)
            //{
            //    workingData.bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
            //    pictureBox.Bitmap = workingData.bitmap;
            //}
        }

        private void rotateRight()
        {
            if (Program.State.RotateRight())
            {
                pictureBox.Bitmap = Program.State.Bitmap;
            }
            //if (workingData.bitmap != null)
            //{
            //    workingData.bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            //    pictureBox.Bitmap = workingData.bitmap;
            //}
        }

        private void rotateLeft()
        {
            if (Program.State.RotateLeft())
            {
                pictureBox.Bitmap = Program.State.Bitmap;
            }
            //if (workingData.bitmap != null)
            //{
            //    workingData.bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
            //    pictureBox.Bitmap = workingData.bitmap;
            //}
        }


        /// <summary>
        /// Deletes the file currently being viewed. 
        /// TODO: add a switch based on the configuration to move to recycle bin by default instead of deleting.
        /// TODO: In case of AunauthorizedAccessException prompt user to restart the app in admin mode
        /// </summary>
        private void delete()
        {
            //TODO: rework to separate state from UI

            //if (workingData.activeEntry != null)
            //{
            //    if (workingData.activeEntry.IsArchive)
            //    {
            //        //TODO: add support for deletion inside an archive
            //        MessageBox.Show("This image file is contained inside an archive file.\nIt cannot be deleted.", "Cannot delete file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }
            //    else if (MessageBox.Show(String.Format("The file {0} will be permanently deleted.\nAre you sure you want to continue?", workingData.activeEntry.Name), "Delete file?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        //before deleting, try to get access to the previous image, which will be automatically loaded upon file deletion.
            //        //if there was only one image in the current working folder, then the app will close all

            //        string nextFileToLoad = String.Empty;
            //        if (workingData.entries.Count > 1)
            //        {
            //            //will try to move to the file
            //            int moveToIndex = workingData.directoryIndex;
            //            moveToIndex--;
            //            if (moveToIndex < 0) moveToIndex = workingData.entries.Count - 1; //auto loop to the end
            //            IEntry entry = workingData.entries[moveToIndex];
            //            nextFileToLoad = entry.FullName;
            //        }


            //        try
            //        {
            //            workingData.activeEntry.Delete();
            //        }
            //        catch (UnauthorizedAccessException uaex)
            //        {
            //            MessageBox.Show("Error wile deleting file\n.Insufficient user privilege.\nPlease restart the app as an administrator.\n\n" + uaex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //        catch (Exception e)
            //        {
            //            MessageBox.Show("Error wile deleting file\n\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }

            //        if (nextFileToLoad == String.Empty)
            //        {
            //            //only a single file in the folder -- close
            //            close();
            //        }
            //        else
            //        {
            //            //load the next image and force a refresh of the folder structure
            //            loadPicture(nextFileToLoad);
            //        }


            //    }
            //}
        }

        /// <summary>
        /// Restore the app to default conditions
        /// </summary>
        private void close()
        {
            pictureBox.Bitmap = null;

            Program.State.Dispose();
            Program.State.Reset();

            toolStripStatusLabelImageInfo.Text = "Welcome! Open an image file to begin browsing.";
            this.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            toolStripComboBoxNavigation_UpdateText(String.Empty);
            toolStripStatusLabelImagePosition.Visible = false;
            toolStripStatusLabelImagePosition.Text = String.Empty;
            toolStripStatusLabelZoom.Visible = false;
            toolStripStatusLabelZoom.Text = String.Empty;
            toolStripStatusLabelFileSize.Visible = false;
            toolStripStatusLabelFileSize.Text = String.Empty;
            toolStripStatusLabelPixelPosition.Visible = false;
            toolStripStatusLabelPixelPosition.Text = String.Empty;
        }

        #endregion

        #region override
        /// <summary>
        /// Allows catching arrows key as part of the WM_KEYDOWN message
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Menu:
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    if (e.Shift)
                    {

                    }
                    else
                    {
                    }
                    break;
            }
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Forces a refresh of the history box
        /// </summary>
        public void RefreshHistoryList()
        {
            toolStripComboBoxNavigation.SelectedIndexChanged -= toolStripComboBoxNavigation_SelectedIndexChanged;
            toolStripComboBoxNavigation.ComboBox.DataSource = null;
            toolStripComboBoxNavigation.Items.Clear();
            toolStripComboBoxNavigation.ComboBox.DataSource = Settings.Get.History.Get();
            toolStripComboBoxNavigation.SelectedIndexChanged += toolStripComboBoxNavigation_SelectedIndexChanged;
        }


        public void setCheckeredPatternBackground(bool b)
        {
            if (this.pictureBox.UseBackgroundBrush != b)
            {
                this.pictureBox.UseBackgroundBrush = b;
            }
        }

        #endregion


        private void scrollDown()
        {
            verticalScroll(1);
        }

        private void scrollUp()
        {
            verticalScroll(-1);
        }




    }


}
