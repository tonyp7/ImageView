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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public Config config;




        private bool fullscreen = false;

        private Point mousePosition = new Point();
        private Point mousePositionOnMouseDown = new Point();

        private FullScreenSaveState fullScreenSaveState = new FullScreenSaveState();

        private Tool activeTool = Tool.None;

        public FrmMain()
        {
            InitializeComponent();
            this.MouseWheel += FrmMain_MouseWheel;
            workingData = new WorkingData();
            config = new Config();
            config.Load();

            close();

            //restore history
            toolStripComboBoxNavigation.Items.AddRange( config.History.Get().ToArray() );

            //set check mark on the type of image view
            refreshImageSizeModeUI();

            //restore window size
            if (config.Window.Width != 0 && config.Window.Height != 0)
            {
                this.panelMain.Resize -= panelMain_Resize;
                this.Location = new Point(config.Window.X, config.Window.Y);
                this.Width = config.Window.Width;
                this.Height = config.Window.Height;
                this.WindowState = config.Window.State;
                this.panelMain.Resize += panelMain_Resize;
            }

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


            resizeNavigationBar();
            string[] args = Environment.GetCommandLineArgs();

            if (args != null && args.Length >= 2)
            {
                loadPicture(args[1]);
            }
        }


        private void AutosizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            config.Display.SizeMode = ImageSizeMode.Autosize;
            refreshImageSizeModeUI();

            panelMain.Resize -= panelMain_Resize;
            resizePictureBox();
            panelMain.Resize += panelMain_Resize;
        }
        private void normalSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            config.Display.SizeMode = ImageSizeMode.Normal;
            refreshImageSizeModeUI();

            panelMain.Resize -= panelMain_Resize;
            resizePictureBox();
            panelMain.Resize += panelMain_Resize;
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
            string value = (string)toolStripComboBoxNavigation.SelectedItem;
            this.ActiveControl = null;
            loadPicture(value);

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
            int zoom = 100;
            if (int.TryParse(toolStripComboBoxZoom.Text.Trim(' ', '%'), out zoom))
            {
                zoom = Program.Clamp(zoom, 1, ConfigDisplay.MAX_ZOOM);
                config.Display.SizeMode = ImageSizeMode.Zoom;
                config.Display.Zoom = zoom;
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
            config.Save();
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
                    previous();
                    break;
                case Keys.Right:
                    next();
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
                    if (fullscreen)
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
                    if (timerSlideShow.Enabled)
                    {
                        //a double click while a slideshow is playing automatically stops it
                        exitSlideshow();
                    }
                    else
                    {
                        toggleFullScreen();
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
            enterSlideshow();
        }

        private void toolStripButtonSlideShow_Click(object sender, EventArgs e)
        {
            enterSlideshow();
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
                config.Window.X = this.RestoreBounds.X;
                config.Window.Y = this.RestoreBounds.Y;
                config.Window.Width = this.RestoreBounds.Width;
                config.Window.Height = this.RestoreBounds.Height;
                config.Window.State = this.WindowState;
            }
            else
            {
                config.Window.X = this.Location.X;
                config.Window.Y = this.Location.Y;
                config.Window.Width = this.Width;
                config.Window.Height = this.Height;
                config.Window.State = this.WindowState;
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

                    config.Display.SizeMode = ImageSizeMode.Zoom;


                    int newZoom = Control.ModifierKeys != Keys.Alt ? (int)workingData.calculatedZoom + config.Display.ZoomStep : (int)workingData.calculatedZoom - config.Display.ZoomStep;

                    newZoom = Program.Clamp(newZoom, 1, ConfigDisplay.MAX_ZOOM);

                    panelMain.Resize -= panelMain_Resize;
                    resizePictureBox(e.Location, newZoom);
                    panelMain.Resize += panelMain_Resize;
                    config.Display.Zoom = newZoom;

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
            X = e.Location.X / workingData.calculatedZoom * 100.0;
            Y = e.Location.Y / workingData.calculatedZoom * 100.0;
            toolStripStatusLabelPixelPosition.Text = string.Format("{0:#},{1:#}", X, Y);

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

        private void toggleZoomTool()
        {
            if(activeTool != Tool.Zoom)
            {
                activeTool = Tool.Zoom;

                var coord = pictureBox.PointToClient(Cursor.Position);
                if (pictureBox.DisplayRectangle.Contains(coord)){
                    this.Cursor = new Cursor(Properties.Resources.zoomin.Handle);
                }
            }
            else
            {
                activeTool = Tool.None;
                if (this.Cursor != Cursors.Default)
                    this.Cursor = Cursors.Default;
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


    }


}
