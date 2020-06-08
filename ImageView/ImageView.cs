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
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Drawing.Imaging;
using System.Runtime.Serialization;
using System.Collections.Generic;
using ImageMagick;
using System.Diagnostics;
using ImageView.Configuration;
using SevenZipExtractor;
using ImageView.ImageEntry;

namespace ImageView
{


 


    ///// <summary>
    ///// Container for everything related to the current data being used by ImageView
    ///// </summary>
    //public class WorkingData
    //{
    //    public Bitmap bitmap = null;
    //    public MagickImage nativeImage = null;
    //    public IEntry activeEntry;
    //    public List<IEntry> entries;
    //    public int directoryIndex;
    //    public float calculatedZoom;
    //    public ArchiveFile archive;

    //    public WorkingData()
    //    {
    //        entries = new List<IEntry>();
    //        reset();
    //    }


    //    public void Dispose()
    //    {
    //        if (bitmap != null)
    //        {
    //            bitmap.Dispose();
    //            bitmap = null;
    //        }
    //        if (nativeImage != null && !nativeImage.IsDisposed)
    //        {
    //            nativeImage.Dispose();
    //            nativeImage = null;
    //        }
    //    }
    //    public void reset()
    //    {
    //        directoryIndex = -1;
    //        activeEntry = null;
    //        entries.Clear();
    //        bitmap = null;
    //        calculatedZoom = -1.0f;
    //        if (archive != null)
    //        {
    //            archive.Dispose();
    //            archive = null;
    //        }
    //    }
    //}

    public partial class FrmMain : Form
    {


        //WorkingData workingData;

        /// <summary>
        /// Restore the app to default conditions
        /// </summary>
        private void close()
        {
            pictureBox.Bitmap = null;
            //pictureBox.Visible = false;

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





        public void setCheckeredPatternBackground(bool b)
        {
            if(this.pictureBox.UseBackgroundBrush != b)
            {
                this.pictureBox.UseBackgroundBrush = b;
                //this.pictureBox.Invalidate();
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


        //private static string NiceFileSize(long len)
        //{
        //    string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        //    double lend = (double)len;
        //    int order = 0;
        //    while (lend >= 1024 && order < sizes.Length - 1)
        //    {
        //        order++;
        //        lend = lend / 1024;
        //    }

        //    // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
        //    // show a single decimal place, and no space.
        //    string result = String.Format("{0:0.##} {1}", lend, sizes[order]);

        //    return result;
        //}


        /// <summary>
        /// History is made of simple strings that represent enough information to load a file:
        ///  - The picture full name
        ///  - The archive file (if any) from which the file derives
        /// </summary>
        /// <param name="tre"></param>
        //private void loadPicture(TextRepresentationEntry tre)
        //{
        //    if(tre.ArchiveFile == String.Empty)
        //    {
        //        //it's just a regular file, go load it
        //        loadPicture(tre.FullName);
        //    }
        //    else
        //    {
        //        if (File.Exists(tre.ArchiveFile))
        //        {
        //            List<ArchiveEntry> imagesFiles = ArchiveEntry.GetImageFiles(tre.ArchiveFile);

        //            if(imagesFiles.Count > 0)
        //            {
        //                int index = imagesFiles.FindIndex(x => x.InternalArchiveFullName.Equals(tre.FullName));

        //                //check if image still exist inside this archive. Could have been deleted for whatever reason
        //                if(index != -1)
        //                {
        //                    workingData.directoryIndex = index;
        //                    workingData.entries = imagesFiles.ToList<IEntry>();
        //                    loadPicture(workingData.entries[index]);
        //                }
        //            }  
        //        }
        //    }

        //}



        /// <summary>
        /// When loading from a single fullname string, this full name can be many different things which leads to different behaviors
        ///  - An image file: list images in the folder, load the image
        ///  - A folder: list images in the folder, load the first image in the folder
        ///  - An image inside an archive: list images inside the folder archive, load the image
        ///  - An archive: list images in the archive, load the first image in the archive
        /// </summary>
        /// <param name="fullname"></param>
        //private void loadPicture(string fullname)
        //{
        //    if (File.Exists(fullname))
        //    {
        //        //check if its an archive. Here we depend on the file extension. It's not a fullproof method but it's a reasonnable assumption
        //        //and expectation that the file extension is correct
        //        string fullnameL = fullname.ToLower();
        //        if (Config.ArchiveFilter.Any(x => fullnameL.EndsWith(x)))
        //        {
        //            try
        //            {
        //                List<ArchiveEntry> imagesFiles = ArchiveEntry.GetImageFiles(fullname);
        //                if(imagesFiles.Count > 0)
        //                {
        //                    workingData.directoryIndex = 0;
        //                    workingData.entries = imagesFiles.ToList<IEntry>();

        //                    loadPicture(workingData.entries[0]);
        //                }
        //            }
        //            catch(Exception e)
        //            {
        //                throw (e);
        //            }
                   
        //        }
        //        else
        //        {
        //            //attempt to load as a regular image
        //            string path = Path.GetDirectoryName(fullname);
        //            string[] files = Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly).Where(file => Config.ExtensionFilter.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase))).ToArray();
        //            int index = Array.FindIndex(files, x => x.Contains(fullname));

        //            if(index != -1)
        //            {
        //                List<IEntry> l = new List<IEntry>();
        //                foreach (string s in files)
        //                {
        //                    var e = new FileEntry(s);
        //                    l.Add(e);
        //                }
        //                workingData.entries = l;
        //                workingData.directoryIndex = index;
        //                loadPicture(workingData.entries[workingData.directoryIndex]);
        //            }
        //            else
        //            {
        //                //the file is probably not a supported image
        //            }
        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            FileAttributes attr = File.GetAttributes(fullname);
        //            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
        //            {
        //                var files = Directory.EnumerateFiles(fullname, "*.*", SearchOption.TopDirectoryOnly).Where(file => Config.ExtensionFilter.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase))).ToArray();
        //                //workingData.directoryIndex = Array.FindIndex(files, x => x.Contains(fullname));

        //                if (files.Length > 0)
        //                {

        //                    List<IEntry> l = new List<IEntry>();
        //                    foreach (string s in files)
        //                    {
        //                        var e = new FileEntry(s);
        //                        l.Add(e);
        //                    }

        //                    workingData.directoryIndex = 0;
        //                    workingData.entries = l;

        //                    loadPicture(workingData.entries[0]);

        //                }
        //                else
        //                {
        //                    //no image files where founds.
        //                    return;
        //                }
        //            }
        //        }
        //        catch (FileNotFoundException)
        //        {
        //            //it's not a file, but its not a directory either? it could be a file inside an archive
        //            //TODO: support direct files inside archives
        //            pictureBox.Visible = false;
        //        }
        //        catch (DirectoryNotFoundException)
        //        {
        //            MessageBox.Show(  String.Format(Settings.Get.General.GetString("ErrorPathNotFound"),fullname), Settings.Get.General.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            pictureBox.Visible = false;
        //        }
        //    }



        //}

//        public void loadPicture(IEntry entry)
//        {
//            //clean up previously used memory (if any)
//            workingData.Dispose();
//            workingData.activeEntry = entry;

//            //load image
//#if DEBUG
//            Stopwatch stopWatch = new Stopwatch();
//            stopWatch.Start();
//#endif
//            Stream stream = workingData.activeEntry.GetStream();
//            try
//            {
//                workingData.nativeImage = new ImageMagick.MagickImage(stream);
//            }
//            catch(Exception)
//            {
//                workingData.nativeImage = new MagickImage(Properties.Resources.error);
//                MessageBox.Show(String.Format(Settings.Get.General.GetString("ErrorImageLoad"), entry.FullName), Settings.Get.General.GetString("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            stream.Dispose();
//#if DEBUG
//            stopWatch.Stop();
//            System.Diagnostics.Debug.WriteLine(String.Format("Loading image in {0} ms", stopWatch.Elapsed.Milliseconds));
//#endif

//            //convert to bitmap
//            if(Settings.Get.Display.AutoRotate && workingData.nativeImage.Orientation != OrientationType.Undefined)
//            {
//                workingData.nativeImage.AutoOrient();
//            }
//            workingData.bitmap = workingData.nativeImage.ToBitmap();


//            //check if the image mode should be changed because user wants a specific image size mode on load
//            if (viewingMode == ViewingMode.Normal)
//            {
//                if (Settings.Get.Display.SizeModeOnImageLoad != ImageSizeMode.Restore && Settings.Get.Display.SizeModeOnImageLoad != Settings.Get.Display.SizeMode)
//                {
//                    Settings.Get.Display.SizeMode = Settings.Get.Display.SizeModeOnImageLoad;
//                    refreshImageSizeModeUI();
//                }
//            }

//#if DEBUG
//            stopWatch.Start();
//#endif
//            //Assign image to picture box then refresh sizing
//            pictureBox.Bitmap = workingData.bitmap;
//#if DEBUG
//            stopWatch.Stop();
//            System.Diagnostics.Debug.WriteLine(String.Format("Paint done in {0} ms", stopWatch.Elapsed.Milliseconds));
//#endif

//            //Add loaded file to history if necessary
//            TextRepresentationEntry tre = workingData.activeEntry.ToText();
//            Settings.Get.History.AddFile(tre);
//            SetHistoryList(Settings.Get.History.Get());

//            //refresh UI elements
//            toolStripComboBoxNavigation_UpdateText(workingData.activeEntry.FullName);
//            toolStripStatusLabelImageInfo.Text = String.Format("{0} x {1} - {2} {3}", workingData.nativeImage.BaseWidth, workingData.nativeImage.BaseHeight, workingData.nativeImage.ColorSpace, workingData.nativeImage.ColorType);
//            toolStripStatusLabelImageInfo.Visible = true;
//            toolStripStatusLabelFileSize.Text = NiceFileSize(workingData.activeEntry.Length);
//            toolStripStatusLabelFileSize.Visible = true;
//            toolStripStatusLabelImagePosition.Text = String.Format("{0} / {1}", workingData.directoryIndex + 1, workingData.entries.Count);
//            toolStripStatusLabelImagePosition.Visible = true;
//            this.Text = String.Format("{0} - {1}", workingData.activeEntry.FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            
//        }


        internal void SetHistoryList(List<TextRepresentationEntry> list)
        {
            toolStripComboBoxNavigation.Items.Clear();
            toolStripComboBoxNavigation.Items.AddRange(Settings.Get.History.Get().ToArray());
        }

        private void copy()
        {
            if(Program.State.Bitmap != null)
            {
                Clipboard.SetImage(Program.State.Bitmap);
            }
        }

        private void showInformation()
        {
            if(Program.State.ActiveEntry != null)
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
            if(pictureBox.Bitmap != null)
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
            if(viewingMode == ViewingMode.Reader)
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

            if(vm != viewingMode)
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
            Settings.Get.Display.SizeMode = Settings.Get.Slideshow.SizeMode;
            refreshImageSizeModeUI();
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
    }
}