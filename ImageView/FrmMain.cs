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

        private Point mousePosition = new Point();
        private Point mousePositionOnMouseDown = new Point();

        private FullScreenSaveState fullScreenSaveState = new FullScreenSaveState();

        private Tool activeTool = Tool.None;
        private ViewingMode viewingMode = ViewingMode.Normal;

        private ImageBox pictureBox;
        public FrmMain()
        {
            InitializeComponent();

            Settings.Get.Load();

            //Picture box stuff
            // 
            // pictureBox
            // 
            this.pictureBox = new ImageBox();
            this.pictureBox.InitialImage = null;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(556, 309);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            this.pictureBox.DoubleClick += new System.EventHandler(this.pictureBox_DoubleClick);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
            this.pictureBox.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            this.pictureBox.CheckeredPatternBackground = Settings.Get.Display.CheckeredPatternBackground;
            this.panelMain.Controls.Add(this.pictureBox);
            this.MouseWheel += FrmMain_MouseWheel;
            this.openFileDialog.Filter = Properties.Resources.SupportedImageFiles;
            workingData = new WorkingData();

            

            //multilingual settings
            InitalizeComponentsCultureAware();

            close();

            //restore history
            toolStripComboBoxNavigation.Items.AddRange(Settings.Get.History.Get().ToArray() );

            //set check mark on the type of image view
            refreshImageSizeModeUI();

            //restore window size
            if (Settings.Get.Window.Width != 0 && Settings.Get.Window.Height != 0)
            {
                this.panelMain.Resize -= panelMain_Resize;
                this.Location = new Point(Settings.Get.Window.X, Settings.Get.Window.Y);
                this.Width = Settings.Get.Window.Width;
                this.Height = Settings.Get.Window.Height;
                this.WindowState = Settings.Get.Window.State;
                this.panelMain.Resize += panelMain_Resize;
            }
        }

        public void InitalizeComponentsCultureAware()
        {

            this.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.panelMain.SuspendLayout();

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

            toolStripDropDownButtonDisplayType.ToolTipText = Settings.Get.General.GetString("PickImageSizeMode");

            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        private void FrmMain_MouseWheel(object sender, MouseEventArgs e)
        {
            if(e.Delta < 0) //scroll down
            {
                next();
            }
            else
            {
                previous();
            }
        }

        private void toolStripComboBoxNavigation_Click(object sender, EventArgs e)
        {

        }

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
                loadPicture(args[1]);
            }
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
        private void toolStripComboBoxNavigation_SelectedIndexChanged(object sender, EventArgs e)
        {

            TextRepresentationEntry tre = (TextRepresentationEntry)toolStripComboBoxNavigation.SelectedItem;

            this.ActiveControl = null;
            loadPicture(tre);

            //This fixes a weird refresh issue where the button to expand the dropdown stays focused after the selection change
            toolStripComboBoxNavigation.Select(0, 0);
            toolStripComboBoxNavigation.Invalidate();
            toolStripComboBoxNavigation.Select(0, 0);

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
        private void toolStripComboBoxZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelMain.Resize -= panelMain_Resize;
            zoom();
            panelMain.Resize += panelMain_Resize;
        }

        private void toolStripComboBoxZoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                zoom();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Zoom! max zoom is for the moment set to 400 arbitrarily. When zooming on big pictures the program becomes a giant sloth and the pictubox control shows its limit here
        /// until a more robust solution is found, this will stay in place.
        /// </summary>
        private void zoom()
        {
            double zoom = 1.0;
            if (double.TryParse(toolStripComboBoxZoom.Text.Trim(' ', '%'), out zoom))
            {
                zoom /= 100.0;
                zoom = Program.Clamp(zoom, .01, ConfigDisplay.MAX_ZOOM);
                Settings.Get.Display.SizeMode = ImageSizeMode.Zoom;
                Settings.Get.Display.Zoom = zoom;
                refreshImageSizeModeUI();
                resizePictureBox();

                //remove focus from the textbox so that user can navigate with arrow keys etc.
                this.ActiveControl = null;
            }
        }



        private void panelMain_Resize(object sender, EventArgs e)
        {

#if DEBUG
            System.Diagnostics.Debug.WriteLine("panelMain_Resize");
#endif
            resizePictureBox();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Attempt to save config
            Settings.Get.Save();
        }



        private void toolStripButtonNext_Click(object sender, EventArgs e)
        {
            next();
        }

        private void toolStripButtonPrevious_Click(object sender, EventArgs e)
        {
            previous();
        }




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


        private void FrmMain_KeyPress(object sender, KeyPressEventArgs e)
        {

        }


        private void FrmMain_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Menu:
                    if (activeTool == Tool.Zoom)
                    {
                        var coord = pictureBox.PointToClient(Cursor.Position);

                        if (pictureBox.DisplayRectangle.Contains(coord))
                        {
                            Cursor = new Cursor(Properties.Resources.zoomin.Handle);
                        }
                        else
                        {
                            Cursor = Cursors.Default;
                        }


                    }
                    break;
            }
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
                case Keys.Menu:
                    if (activeTool == Tool.Zoom)
                    {
                        var coord = pictureBox.PointToClient(Cursor.Position);
                        if (pictureBox.DisplayRectangle.Contains(coord))
                        {
                            e.Handled = true; //prevent ALT triggering the toolstripmenu
                            Cursor = new Cursor(Properties.Resources.zoomout.Handle);
                        }
                        else
                        {

                        }
                    }
                    break;
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

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            delete();
        }



        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            close();
        }


 

        private void openFile()
        {
            //openFileDialog.FileName = "";
            DialogResult dr = openFileDialog.ShowDialog();
            if(dr == DialogResult.OK)
            {
                //loadPicture(openFileDialog.FileName);
                loadPicture(openFileDialog.FileName);
            }
        }
        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            openFile();
        }
        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            openFile();
        }
 


 


        private void fullscreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toggleFullScreen();
        }


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


        private void timerSlideShow_Tick(object sender, EventArgs e)
        {
            next();
        }


        private void slideshowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //enterSlideshow();
            setViewMode(ViewingMode.Slideshow);
        }

        private void toolStripButtonSlideShow_Click(object sender, EventArgs e)
        {
            //enterSlideshow();
            setViewMode(ViewingMode.Slideshow);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout a = new FrmAbout();
            a.ShowDialog();
        }

        private void resizeNavigationBar()
        {
            toolStripComboBoxNavigation.Width = (int)(0.5f * this.Width);
        }
        private void FrmMain_Resize(object sender, EventArgs e)
        {
            resizeNavigationBar();
        }




        private void toolStripDropDownButtonDisplayType_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBoxZoom_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonSettings_Click(object sender, EventArgs e)
        {
            showSettings();
        }

        private void showSettings()
        {
            FrmSettings f = new FrmSettings(this);
            f.ShowDialog();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSettings();
        }

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

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showInformation();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copy();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void panelMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if(s != null & s.Count() > 0)
            {
                loadPicture(s[0]);
            }

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



        /// <summary>
        /// Saves the mouse position when it is first moved down, in order to handle dragging of the picture correctly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
            {
                mousePosition = e.Location;
                mousePositionOnMouseDown = Control.MousePosition;
            }
        }

        /// <summary>
        /// If the tool is not zoom, cursor is restored to default.
        /// Zoom tool stays active until user manually de-activate it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                if (this.activeTool == Tool.None)
                {
                    this.Cursor = Cursors.Default;
                }
            }
            else if(e.Button == MouseButtons.Left)
            {
                if (this.activeTool == Tool.Zoom)
                {
                    var coord = pictureBox.PointToClient(Cursor.Position);

                    Settings.Get.Display.SizeMode = ImageSizeMode.Zoom;


                    double newZoom = Control.ModifierKeys != Keys.Alt ? workingData.calculatedZoom + Settings.Get.Display.ZoomStep : workingData.calculatedZoom - Settings.Get.Display.ZoomStep;

                    newZoom = Program.Clamp(newZoom, .01, ConfigDisplay.MAX_ZOOM);

                    panelMain.Resize -= panelMain_Resize;
                    resizePictureBox(e.Location, newZoom);
                    panelMain.Resize += panelMain_Resize;
                    Settings.Get.Display.Zoom = newZoom;

                    refreshImageSizeModeUI();
                }
            }
            else if(e.Button == MouseButtons.Right)
            {
#if DEBUG
                Point panelHalfSize = new Point(panelMain.Width >> 1, panelMain.Height >> 1);
                panelMain.AutoScrollPosition = new Point(e.Location.X - panelHalfSize.X, e.Location.Y - panelHalfSize.Y);
#endif

            }
        }

        private void pictureBox_Click(object sender, EventArgs ea)
        {

        }


        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            //refresh pixel coord
            double X, Y;
            X = e.Location.X / workingData.calculatedZoom;
            Y = e.Location.Y / workingData.calculatedZoom;
            toolStripStatusLabelPixelPosition.Text = string.Format("{0:0},{1:0}", X, Y);

            //drag image
            if (e.Button == MouseButtons.Middle)
            {
                if (this.Cursor == Cursors.Default) this.Cursor = new Cursor(Properties.Resources.move.Handle);

                Point changePoint = Point.Empty;
                if (panelMain.VerticalScroll.Visible)
                {
                    changePoint.Y = e.Location.Y - mousePosition.Y;
                }
                if (panelMain.HorizontalScroll.Visible)
                {
                    changePoint.X = e.Location.X - mousePosition.X;
                }
                panelMain.AutoScrollPosition = new Point(-panelMain.AutoScrollPosition.X - changePoint.X,
                                                      -panelMain.AutoScrollPosition.Y - changePoint.Y);

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
            if(activeTool != Tool.Zoom)
            {
                enterZoomTool();
            }
            else
            {
                exitZoomTool();
            }
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.toolStripStatusLabelPixelPosition.Visible = true;
            if (activeTool == Tool.Zoom)
            {
                this.Cursor = new Cursor(Properties.Resources.zoomin.Handle);
            }
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            this.toolStripStatusLabelPixelPosition.Visible = false;
            if (this.Cursor != Cursors.Default)
                this.Cursor = Cursors.Default;
        }

        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmLicense().ShowDialog();
        }


        /// <summary>
        /// Change image size mode, refresh UI elements and resize the picture box
        /// </summary>
        /// <param name="sz">New image size mode to be set</param>
        private void setImageSizeMode(ImageSizeMode sz)
        {
            Settings.Get.Display.SizeMode = sz;
            refreshImageSizeModeUI();

            panelMain.Resize -= panelMain_Resize;
            resizePictureBox();
            panelMain.Resize += panelMain_Resize;
        }

        

        private void enterReader()
        {
            setImageSizeMode(Settings.Get.Reader.SizeMode);
            toolStripReaderMode.BackColor = SystemColors.MenuHighlight;
        }

        private void exitReader()
        {
            if(Settings.Get.Display.SizeModeOnImageLoad != ImageSizeMode.Restore)
            {
                setImageSizeMode(Settings.Get.Display.SizeModeOnImageLoad);
            }
            toolStripReaderMode.BackColor = SystemColors.Control;
        }

        private void BestFitStripMenuItem_Click(object sender, EventArgs e)
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

        private void toolStripReaderMode_Click(object sender, EventArgs e)
        {
            toggleReaderMode();
        }

        private void readerModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toggleReaderMode();
        }


        private void verticalScroll(int direction)
        {
            if (panelMain.VerticalScroll.Visible)
            {
                Point scroll = panelMain.AutoScrollPosition;

                scroll.X *= -1;
                scroll.Y = -scroll.Y + (panelMain.Height / 10)*direction;

                panelMain.AutoScrollPosition = scroll;
            }
        }

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
