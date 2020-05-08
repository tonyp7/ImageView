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

        private Config config;
        private DirectoryInfo currentWorkingDir;
        private FileInfo currentFile;
        private string[] currentWorkingDirFiles;
        private int currentWorkingDirIndex;


        private bool fullscreen = false;

        private Point mousePosition = new Point(); 

        private FormWindowState windowStateBeforeEnteringFullscreen = FormWindowState.Normal;


        public FrmMain()
        {
            InitializeComponent();
            this.MouseWheel += FrmMain_MouseWheel;
            close();
            config = new Config();
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
            resizePictureBox();
        }
        private void normalSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            config.Display.SizeMode = ImageSizeMode.Normal;
            resizePictureBox();
        }
        private void toolStripComboBoxZoom_TextChanged(object sender, EventArgs e)
        {
           
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
            int zoom = 100;
            if (int.TryParse(toolStripComboBoxZoom.Text.Trim(' ', '%'), out zoom))
            {
                config.Display.SizeMode = ImageSizeMode.Zoom;
                config.Display.Zoom = zoom;
#if DEBUG
                System.Diagnostics.Debug.WriteLine("Resize from toolStripComboBoxZoom_SelectedIndexChanged");
#endif
                resizePictureBox();
            }

        }

        private void resizePictureBox()
        {
            resizePictureBox(null);
        }
        private void resizePictureBox(Image i)
        {

#if DEBUG
            System.Diagnostics.Debug.WriteLine("Resize " + currentFile.Name);
#endif

            //if not argument is provided attempt to get it from the picture box
            if (i == null)
            {
                i = pictureBox.Image;
            }


            if (i == null) return;


            if (config.Display.SizeMode == ImageSizeMode.Autosize)
            {

                panelMain.AutoScroll = false;

                //zoom calculated for the autosize. default 100 then re-adjusted if the image cant fit
                int zoom = 100;

                if (panelMain.Width >= i.Width && panelMain.Height >= i.Height)
                {
                    //image can fit entirely on the screen so the display is actually normal
                    pictureBox.SizeMode = PictureBoxSizeMode.Normal;
                    pictureBox.Width = i.Width;
                    pictureBox.Height = i.Height;


                    //center image on screen
                    Point xy = new Point(0, 0);
                    if (i != null)
                    {
                        if (panelMain.Width > i.Width)
                        {
                            xy.X = (int)((panelMain.Width - i.Width) / 2.0f);
                        }
                        if (panelMain.Height > i.Height)
                        {
                            xy.Y = (int)((panelMain.Height - i.Height) / 2.0f);
                        }
                    }
                    if (!xy.Equals(pictureBox.Location))
                        pictureBox.Location = xy;



                }
                else
                {
                    pictureBox.Location = Point.Empty;
                    
                    pictureBox.Width = panelMain.Width;
                    pictureBox.Height = panelMain.Height;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                    float aspec = (float)i.Width / (float)i.Height;
                    float windowAspec = (float)panelMain.Width / (float)panelMain.Height;
                    if (aspec > windowAspec)
                    {
                        zoom = (int)((float)panelMain.Width / (float)i.Width * 100.0f);
                    }
                    else
                    {
                        zoom = (int)((float)panelMain.Height / (float)i.Height * 100.0f);
                    }
                    
                }

                toolStripStatusLabelZoom.Visible = true;
                toolStripStatusLabelZoom.Text = String.Format("{0} %", zoom);
                toolStripComboBoxZoom_UpdateText(String.Format("{0}%", zoom));
 

            }
            else if (config.Display.SizeMode == ImageSizeMode.Zoom || config.Display.SizeMode == ImageSizeMode.Normal)
            {

                int newWidth = i.Width;
                int newHeight = i.Height;

                //for zoom we calculate new height and width but the code is otherwise the same than drawing the regular picture
                if(config.Display.SizeMode == ImageSizeMode.Zoom)
                {
                    float zoomf = config.Display.Zoom / 100.0f;
                    newWidth = (int)Math.Round(((float)i.Width * zoomf));
                    newHeight = (int)Math.Round(((float)i.Height * zoomf));
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    pictureBox.SizeMode = PictureBoxSizeMode.Normal;
                }
                
                pictureBox.Width = newWidth;
                pictureBox.Height = newHeight;

                //center image if needed
                panelMain.AutoScroll = false;
                //pictureBox.Location = Point.Empty;
                Point xy = new Point(0, 0);
                if (panelMain.Width > newWidth)
                {
                    xy.X = (int)((panelMain.Width - newWidth) / 2.0f);
                }
                if (panelMain.Height > newHeight)
                {
                    xy.Y = (int)((panelMain.Height - newHeight) / 2.0f);
                }
                pictureBox.Location = xy;
                panelMain.AutoScroll = true;

                //zoom state
                int zoom = config.Display.SizeMode == ImageSizeMode.Normal ? 100 : config.Display.Zoom;
                toolStripStatusLabelZoom.Visible = true;
                toolStripStatusLabelZoom.Text = String.Format("{0} %", zoom);
                toolStripComboBoxZoom_UpdateText(String.Format("{0}%", zoom));
            }
        }


        private void next()
        {
            if(currentWorkingDirIndex != -1)
            {
                currentWorkingDirIndex++;

                //loop
                if(currentWorkingDirIndex >= currentWorkingDirFiles.Length)
                {
                    currentWorkingDirIndex = 0;
                    
                }
                loadPicture(currentWorkingDirFiles[currentWorkingDirIndex], false);
            }
        }
        private void previous()
        {
            if (currentWorkingDirIndex != -1)
            {
                currentWorkingDirIndex--;

                //loop
                if (currentWorkingDirIndex < 0)
                {
                    currentWorkingDirIndex = currentWorkingDirFiles.Length - 1;
                }
                loadPicture(currentWorkingDirFiles[currentWorkingDirIndex], false);
            }

        }


        private static string NiceFileSize(FileInfo fi)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = fi.Length;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            string result = String.Format("{0:0.##} {1}", len, sizes[order]);

            return result;
        }


        /// <summary>
        /// Todo: add a check if the file exists. If it doesn't and there's currently a working dir then refresh folder structure.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="loadFolderStructure"></param>
        private void loadPicture(string filename, bool loadFolderStructure)
        {
            try
            {
                //Image related 
                currentFile = new FileInfo(filename);
                Image i = Image.FromFile(currentFile.FullName);
                
                //Add loaded file to history
                config.History.AddFile(currentFile.FullName);

                //Assign image to picture box then refresh sizing
                pictureBox.Image = i;
                resizePictureBox();


                // fixed visual infos
                toolStripComboBoxNavigation.Text = currentFile.FullName;
                toolStripComboBoxNavigation.Items.Remove(currentFile.FullName);
                toolStripComboBoxNavigation.Items.Insert(0, currentFile.FullName);

                toolStripStatusLabelImageInfo.Text = String.Format("{0} x {1} x {2} BPP", i.Width, i.Height, Image.GetPixelFormatSize(i.PixelFormat));
                toolStripStatusLabelImageInfo.Visible = true;
                toolStripStatusLabelFileSize.Text = NiceFileSize(currentFile);
                toolStripStatusLabelFileSize.Visible = true;
                this.Text = String.Format("{0} - {1}", filename, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);


            }
            catch (OutOfMemoryException)
            {
                // Image.FromFile(String) 
                //The file does not have a valid image format. -or - GDI + does not support the pixel format of the file.
            }
            catch (Exception)
            {

            }



            // Check if we are in the current working dir and pic position
            // This whole check can be completely ignored. This avoids useless computations in case a user click on next/previous image
            // By default it is set to true
            if (loadFolderStructure)
            {
                string path = Path.GetDirectoryName(filename);
                DirectoryInfo di = new DirectoryInfo(path);
                if (currentWorkingDir == null || di.FullName != currentWorkingDir.FullName)
                {
                    currentWorkingDir = di;
                    currentWorkingDirFiles = Directory.EnumerateFiles(di.FullName, "*.*", SearchOption.TopDirectoryOnly).Where(file => config.ExtensionFilter.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase))).ToArray();
                    currentWorkingDirIndex = Array.FindIndex(currentWorkingDirFiles, x => x.Contains(filename));
                }
            }


            toolStripStatusLabelImagePosition.Text = String.Format("{0} / {1}", currentWorkingDirIndex + 1, currentWorkingDirFiles.Length);
            toolStripStatusLabelImagePosition.Visible = true;
        }
        private void loadPicture(string filename)
        {
            loadPicture(filename, true);
        }

        private void panelMain_Resize(object sender, EventArgs e)
        {
            resizePictureBox();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Attempt to save config
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

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Left:
                    previous();
                    break;
                case Keys.Right:
                    next();
                    break;
                case Keys.F:
                    toggleFullScreen();
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
        /// <summary>
        /// Restore the app to default conditions
        /// </summary>
        private void close()
        {
            pictureBox.Image = null;
            currentWorkingDir = null;
            currentWorkingDirFiles = null;
            currentWorkingDirIndex = -1;
            currentFile = null;
            toolStripStatusLabelImageInfo.Text = "Welcome! Open an image file to begin browsing.";
            this.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            toolStripComboBoxNavigation.Text = "";

            toolStripStatusLabelImagePosition.Visible = false;
            toolStripStatusLabelImagePosition.Text = "";
            toolStripStatusLabelZoom.Visible = false;
            toolStripStatusLabelZoom.Text = "";
            toolStripStatusLabelFileSize.Visible = false;
            toolStripStatusLabelFileSize.Text = "";
        }

        /// <summary>
        /// Deletes the file currently being viewed. 
        /// TODO: add a switch based on the configuration to move to recycle bin by default instead of deleting.
        /// TODO: In case of AunauthorizedAccessException prompt user to restart the app in admin mode
        /// </summary>
        private void delete()
        {
            if(currentFile != null)
            {
                if(MessageBox.Show(  String.Format("The file {0} will be permanently deleted.\nAre you sure you want to continue?", currentFile.Name), "Delete file?", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes)
                {
                    //before deleting, try to get access to the previous image, which will be automatically loaded upon file deletion.
                    //if there was only one image in the current working folder, then the app will close all

                    
                    string nextFileToLoad = String.Empty;
                    if(currentWorkingDirFiles.Length > 1)
                    {
                        //will try to move to the file
                        int moveToIndex = currentWorkingDirIndex;
                        moveToIndex--;
                        if (moveToIndex < 0) moveToIndex = currentWorkingDirFiles.Length - 1; //auto loop to the end
                        nextFileToLoad = currentWorkingDirFiles[moveToIndex];
                    }


                    try
                    {
                        File.Delete(currentFile.FullName);
                    }
                    catch (UnauthorizedAccessException uaex)
                    {
                        MessageBox.Show("Error wile deleting file\n.Insufficient user privilege.\nPlease restart the app as an administrator.\n\n" + uaex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show("Error wile deleting file\n\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if(nextFileToLoad == String.Empty)
                    {
                        //only a single file in the folder -- close
                    }
                    else
                    {
                        //load the next image and force a refresh of the folder structure
                        loadPicture(nextFileToLoad, true);
                    }


                }
            }
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
 


        private void exitFullScreen()
        {

            toolStrip.Visible = true;
            statusStrip.Visible = true;
            menuStrip.Visible = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            panelMain.BorderStyle = BorderStyle.Fixed3D;
            this.WindowState = windowStateBeforeEnteringFullscreen; //restore window state

            fullscreen = false;
        }
        private void enterFullScreen()
        {


            windowStateBeforeEnteringFullscreen = this.WindowState; //save window state before entering full screen so that it can be restored when exiting

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            panelMain.BorderStyle = BorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
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


        private void fullscreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toggleFullScreen();
        }


        private void pictureBox_DoubleClick(object sender, EventArgs e)
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


        private void timerSlideShow_Tick(object sender, EventArgs e)
        {
            next();
        }

        private void exitSlideshow()
        {
            exitFullScreen();
            timerSlideShow.Stop();
        }
        private void enterSlideshow()
        {
            enterFullScreen();
            timerSlideShow.Start();
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



        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                mousePosition = e.Location;
                
            }
            
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Cursor = Cursors.Default;
            }
               
            
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                if(this.Cursor == Cursors.Default) this.Cursor = Cursors.SizeAll;

                Point changePoint = new Point(e.Location.X - mousePosition.X,
                                  e.Location.Y - mousePosition.Y);
                panelMain.AutoScrollPosition = new Point(-panelMain.AutoScrollPosition.X - changePoint.X,
                                                      -panelMain.AutoScrollPosition.Y - changePoint.Y);

            }
            

            //if (drag)
            //{
            //    int deltaX = e.X - mousePosition.X;
            //    int deltaY = mousePosition.Y - e.Y;

            //    int scrollX = panelMain.HorizontalScroll.Value + deltaX;
            //    int scrollY = panelMain.VerticalScroll.Value + deltaY;

            //    scrollX = Program.Clamp(scrollX, panelMain.HorizontalScroll.Minimum, panelMain.HorizontalScroll.Maximum);
            //    scrollY = Program.Clamp(scrollY, panelMain.VerticalScroll.Minimum, panelMain.VerticalScroll.Maximum);


            //    panelMain.HorizontalScroll.Value = scrollX;
            //    panelMain.VerticalScroll.Value = scrollY;


            //    mousePosition.X = e.X;
            //    mousePosition.Y = e.Y;
            //}
        }

        private void toolStripDropDownButtonDisplayType_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBoxZoom_Click(object sender, EventArgs e)
        {

        }

 
    }


}
