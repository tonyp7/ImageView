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


    /// <summary>
    /// Container for everything related to the current data being used by ImageView
    /// </summary>
    public class WorkingData
    {
        public Bitmap bitmap = null;
        public MagickImage nativeImage = null;
        public IEntry activeEntry;
        public IEntry[] entries;
        public int directoryIndex;
        public float calculatedZoom;
        public ArchiveFile archive;

        public WorkingData()
        {
            reset();
        }


        public void Dispose()
        {
            if (bitmap != null)
            {
                bitmap.Dispose();
                bitmap = null;
            }
            if (nativeImage != null && !nativeImage.IsDisposed)
            {
                nativeImage.Dispose();
                nativeImage = null;
            }
        }
        public void reset()
        {
            directoryIndex = -1;
            activeEntry = null;
            entries = null;
            bitmap = null;
            calculatedZoom = -1.0f;
            if (archive != null)
            {
                archive.Dispose();
                archive = null;
            }
        }
    }

    public partial class FrmMain : Form
    {


        WorkingData workingData;

        /// <summary>
        /// Restore the app to default conditions
        /// </summary>
        private void close()
        {
            pictureBox.Image = null;

            workingData.Dispose();
            workingData.reset();

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
                    BestFitToolStripMenuItem.Image = ImageView.Properties.Resources.expand_arrows_tick16;
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
            if(this.pictureBox.CheckeredPatternBackground != b)
            {
                this.pictureBox.CheckeredPatternBackground = b;
                this.pictureBox.Invalidate();
            }
        }


        /// <summary>
        /// Calculate the display rectangle given an image i that is stretched in width to the size of the display panel
        /// </summary>
        /// <param name="i"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        private Rectangle calculateFitToWidth(Image i, ref float zoom, ref RectangleF srcRect, ref RectangleF dstRect)
        {
            Rectangle rect = Rectangle.Empty;
            Size clientSize = panelMain.ClientSize;

            //the width is locked to be window width
            rect.X = 0;
            rect.Width = clientSize.Width;

            //height is simply a function of the zoom
            zoom = rect.Width / (float)i.Width;
            rect.Height = (int)Math.Round(zoom * (float)i.Height);



            //if a vertical scrollbar is going to appear, we need to recalculate the size to take into account the width lost by the scrollbar's thickness
            //weirdly enough the behavior of winforms is strange here. If a scrollbar is visible then the client size area is reflected. But we need to account
            //for when this is not the case and so there's a test to check if the scroll bar is visible. If it is then we do not need to subsctract the thickness
            //of the bar since it's already accounted for in the panel ClientSize.
            if (rect.Height > panelMain.ClientSize.Height)
            {
                rect.Width = clientSize.Width -  (panelMain.VerticalScroll.Visible?0:System.Windows.Forms.SystemInformation.VerticalScrollBarWidth);
                zoom = rect.Width / (float)i.Width;
                rect.Height = (int)Math.Round(zoom * (float)i.Height);
            }


            //center y
            if(rect.Height < panelMain.ClientSize.Height)
            {
                rect.Y = (panelMain.ClientSize.Height - rect.Height) >> 1;
            }
            else
            {
                rect.Y = 0;
            }


            //source portion of the image is locked to width of the image
            srcRect.X = 0;
            srcRect.Y = 0;
            srcRect.Width = i.Width;
            srcRect.Height = (float)clientSize.Height / zoom;

            //target
            dstRect.X = 0;
            dstRect.Y = 0;
            dstRect.Width = rect.Width;
            dstRect.Height = clientSize.Height;

            return rect;
        }


        private void verticalScroll(int direction)
        {
            if (panelMain.VerticalScroll.Visible)
            {
                Point scroll = panelMain.AutoScrollPosition;

                scroll.X *= -1;
                scroll.Y = -scroll.Y + (panelMain.Height / 10) * direction;

                panelMain.AutoScrollPosition = scroll;

                //REFRESH DRAWING PORTION
                float zoom = workingData.calculatedZoom;
                Size clientSize = panelMain.Size;
                RectangleF srcRect = new Rectangle();
                RectangleF dstRect = new Rectangle();
                dstRect.X = scroll.X;
                dstRect.Y = scroll.Y;
                dstRect.Width = clientSize.Width;
                dstRect.Height = clientSize.Height;
                srcRect.X = (((float)scroll.X / zoom));
                srcRect.Y = (((float)scroll.Y / zoom));
                srcRect.Width = (((float)clientSize.Width / zoom));
                srcRect.Height = (((float)clientSize.Height / zoom));
                pictureBox.SourceRectangle = srcRect;
                pictureBox.TargetRectange = dstRect;
            }

 
        }



        /// <summary>
        /// Calculate the display rectangle given an image i that is stretched in height to the size of the display panel
        /// </summary>
        /// <param name="i"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        private Rectangle calculateFitToHeight(Image i, ref float zoom, ref RectangleF srcRect, ref RectangleF dstRect)
        {
            Rectangle rect = Rectangle.Empty;
            Size clientSize = panelMain.ClientSize;

            //the height is locked to be window height
            rect.Y = 0;
            rect.Height = clientSize.Height;

            //width is simply a function of the zoom
            zoom = rect.Height / (float)i.Height;
            rect.Width = (int)Math.Round(zoom * (float)i.Width);

            //if an horizontal scrollbar is going to appear, we need to recalculate the size to take into account the height lost by the scrollbar's thickness
            if (rect.Width > clientSize.Width)
            {
                rect.Height = clientSize.Height - (panelMain.HorizontalScroll.Visible?0:System.Windows.Forms.SystemInformation.HorizontalScrollBarHeight);
                zoom = rect.Height / (float)i.Height;
                rect.Width = (int)Math.Round(zoom * (float)i.Width);

            }

            //center x
            if (rect.Width < clientSize.Width)
            {
                rect.X = (clientSize.Width - rect.Width) >> 1;
            }
            else
            {
                rect.X = 0;
            }


            //calculate rects for drawing
            dstRect.X = 0.0f;
            dstRect.Y = 0.0f;
            dstRect.Width = rect.Width;
            dstRect.Height = rect.Height;
            srcRect.X = 0.0f;
            srcRect.Y = 0.0f;
            srcRect.Width = dstRect.Width / zoom;
            srcRect.Height = dstRect.Height / zoom; // = image height


            return rect;
        }

        /// <summary>
        /// Given an image i, this function calculate the position and size of the image that should fit inside the display window
        /// If it fits entirely, no problem, it's just centered.
        /// If it does not, then the width or height of the display window becomes a limiting factor in respect to the image aspect ratio
        /// in that case, the display size width or height becomes the display window size, and the other size member (width or height) is
        /// calculated by applying the aspect ratio.
        /// This way ensure one of the two sizes will always pixel-perfect match the window and avoids weird display artifacts.
        /// </summary>
        /// <param name="i">The image</param>
        /// <param name="zoom">Returns calculated zoom ratio</param>
        /// <returns>Rectangle containing position and size of the image to display</returns>
        private Rectangle calculateAutoSize(Image i, ref float zoom, ref RectangleF srcRect, ref RectangleF dstRect)
        {
            Rectangle rect = Rectangle.Empty;
            Size clientSize = panelMain.ClientSize;

            //image can fit entirely on the screen so the display is actually normal
            if (clientSize.Width >= i.Width && clientSize.Height >= i.Height)
            {
                zoom = 1.0f;

                rect.Width = i.Width;
                rect.Height = i.Height;

                //center image on screen
                if (i != null)
                {
                    if (clientSize.Width > i.Width)
                    {
                        rect.X = (clientSize.Width - i.Width) >> 1;
                    }
                    if (clientSize.Height > i.Height)
                    {
                        rect.Y = (clientSize.Height - i.Height) >> 1;
                    }
                }
            }
            else
            {
                //either the width or height becomes the limiting factor, 
                //this is determined by the aspect ratio of the image and the aspect ratio of the panel containing said image
                float aspec = (float)i.Width / (float)i.Height;
                float windowAspec = (float)clientSize.Width / (float)clientSize.Height;
                if (aspec > windowAspec)
                {
                    zoom = ((float)clientSize.Width / (float)i.Width);
                    rect.Width = clientSize.Width;
                    rect.Height = (int)Math.Round((rect.Width / aspec));

                    rect.Y = (clientSize.Height - rect.Height) >> 1;
                }
                else
                {
                    zoom = ((float)clientSize.Height / (float)i.Height);
                    rect.Height = clientSize.Height;
                    rect.Width = (int)Math.Round((rect.Height * aspec));

                    rect.X = (clientSize.Width - rect.Width) >> 1;
                }
            }


            //calculate rects for drawing
            dstRect.X = 0.0f;
            dstRect.Y = 0.0f;
            dstRect.Width = rect.Width;
            dstRect.Height = rect.Height;
            srcRect.X = 0.0f;
            srcRect.Y = 0.0f;
            srcRect.Width = dstRect.Width / zoom;
            srcRect.Height = dstRect.Height / zoom;

            return rect;
        }

        private void resizePictureBox(Point mouseCoord, float newZoom)
        {
            resizePictureBox(null, mouseCoord, newZoom);
        }
        private void resizePictureBox()
        {
            resizePictureBox(null, Point.Empty, -1);
        }

        private void resizePictureBox(Image i, Point mouseCoord, float newZoom)
        {

            this.SuspendLayout();
#if DEBUG
            if (workingData.activeEntry != null)
                System.Diagnostics.Debug.WriteLine("resizePictureBox " + workingData.activeEntry.Name);
            else
                System.Diagnostics.Debug.WriteLine("resizePictureBox");
#endif

            //if not argument is provided attempt to get it from the picture box
            if (i == null)
            {
                i = pictureBox.Image;
            }


            if (i == null) return;


            if (Settings.Get.Display.SizeMode == ImageSizeMode.BestFit)
            {
                panelMain.AutoScroll = false;
                RectangleF srcRect = new Rectangle();
                RectangleF dstRect = new Rectangle();
                Rectangle rect = calculateAutoSize(i, ref workingData.calculatedZoom, ref srcRect, ref dstRect);
                pictureBox.Size = rect.Size;
                pictureBox.Location = rect.Location;
                pictureBox.SourceRectangle = srcRect;
                pictureBox.TargetRectange = dstRect;
                toolStripStatusLabelZoom.Visible = true;

                toolStripStatusLabelZoom.Text = String.Format("{0} %", (int)(workingData.calculatedZoom * 100.0f));
                toolStripComboBoxZoom_UpdateText(String.Format("{0}%", (int)(workingData.calculatedZoom * 100.0f)));
            }
            else if(Settings.Get.Display.SizeMode == ImageSizeMode.FitToWidth)
            {
                panelMain.AutoScroll = false;
                RectangleF srcRect = new Rectangle();
                RectangleF dstRect = new Rectangle();
                Rectangle rect = calculateFitToWidth(i, ref workingData.calculatedZoom, ref srcRect, ref dstRect);
                pictureBox.Size = rect.Size;
                pictureBox.Location = rect.Location;
                pictureBox.SourceRectangle = srcRect;
                pictureBox.TargetRectange = dstRect;
                panelMain.AutoScroll = true;

                toolStripStatusLabelZoom.Visible = true;
                toolStripStatusLabelZoom.Text = String.Format("{0} %", (int)(workingData.calculatedZoom * 100.0f));
                toolStripComboBoxZoom_UpdateText(String.Format("{0}%", (int)(workingData.calculatedZoom * 100.0f)));

            }
            else if (Settings.Get.Display.SizeMode == ImageSizeMode.FitToHeight)
            {
                panelMain.AutoScroll = false;
                RectangleF srcRect = new Rectangle();
                RectangleF dstRect = new Rectangle();
                Rectangle rect = calculateFitToHeight(i, ref workingData.calculatedZoom, ref srcRect, ref dstRect);
                pictureBox.Size = rect.Size;
                pictureBox.Location = rect.Location;
                pictureBox.SourceRectangle = srcRect;
                pictureBox.TargetRectange = dstRect;
                panelMain.AutoScroll = true;

                toolStripStatusLabelZoom.Visible = true;
                toolStripStatusLabelZoom.Text = String.Format("{0} %", (int)(workingData.calculatedZoom * 100.0f));
                toolStripComboBoxZoom_UpdateText(String.Format("{0}%", (int)(workingData.calculatedZoom * 100.0f)));

            }
            else if (Settings.Get.Display.SizeMode == ImageSizeMode.Zoom || Settings.Get.Display.SizeMode == ImageSizeMode.RealSize)
            {

                int previousWidth = i.Width;
                int previousHeight = i.Height;
                int newWidth = i.Width;
                int newHeight = i.Height;

                //for zoom we calculate new height and width but the code is otherwise the same than drawing the regular picture
                if (Settings.Get.Display.SizeMode == ImageSizeMode.Zoom)
                {
                    //calculate the previous height and width
                    previousHeight = (int)Math.Round(((float)i.Width * workingData.calculatedZoom ));
                    previousHeight = (int)Math.Round(((float)i.Height * workingData.calculatedZoom ));

                    //calculate new zoom
                    float zoomf = newZoom == -1 ? Settings.Get.Display.Zoom  : newZoom ;
                    newWidth = (int)Math.Round(((float)i.Width * zoomf));
                    newHeight = (int)Math.Round(((float)i.Height * zoomf));

                }

                System.Diagnostics.Debug.WriteLine(String.Format("Previous: {0}x{1} // New: {2}x{3}", pictureBox.Width, pictureBox.Height, newWidth, newHeight));


                

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

                //save the new zoom state
                float zoom;
                if(newZoom == -1.0)
                {
                    zoom = Settings.Get.Display.SizeMode == ImageSizeMode.RealSize ? 1.0f : Settings.Get.Display.Zoom;
                }
                else
                {
                    zoom = newZoom; 
                }

                //if a click point was provided, we center the image on this reference point
                if (mouseCoord != Point.Empty)
                {
                    //translate mouse position to pixel coords
                    float Xpx, Ypx;
                    Xpx = mouseCoord.X / workingData.calculatedZoom ;
                    Ypx = mouseCoord.Y / workingData.calculatedZoom ;

                    //translate these pixel coords to new coords
                    Xpx = Xpx * zoom ;
                    Ypx = Ypx * zoom ;

                    //center clicked ppoint
                    Point panelHalfSize = new Point(panelMain.Width >> 1, panelMain.Height >> 1);
                    panelMain.AutoScrollPosition = new Point((int)Xpx - panelHalfSize.X, (int)Ypx - panelHalfSize.Y);
                }

                //The current zoom now becomes what was calculated
                workingData.calculatedZoom = zoom;


                //DRAW
                refreshDrawingSurface();

                //fixes an issue when the picturebox doesn't completely redraw when a new zoom is applied
                if (newZoom != -1.0)
                {
                    this.pictureBox.Invalidate();
                }

                toolStripStatusLabelZoom.Visible = true;
                toolStripStatusLabelZoom.Text = String.Format("{0} %", (int)(zoom*100.0f));
                toolStripComboBoxZoom_UpdateText(String.Format("{0}%", (int)(zoom*100.0f)));
            }

            this.ResumeLayout();
        }


        private void refreshDrawingSurface()
        {
            float zoom = workingData.calculatedZoom;
            Size clientSize = panelMain.Size;
            RectangleF srcRect = new RectangleF();
            RectangleF dstRect = new RectangleF();
            dstRect.X = -panelMain.AutoScrollPosition.X;
            dstRect.Y = -panelMain.AutoScrollPosition.Y;
            dstRect.Width = clientSize.Width;
            dstRect.Height = clientSize.Height;
            srcRect.X = dstRect.X / zoom;
            srcRect.Y = dstRect.Y / zoom;
            srcRect.Width = dstRect.Width / zoom;
            srcRect.Height = dstRect.Height / zoom;
            pictureBox.SourceRectangle = srcRect;
            pictureBox.TargetRectange = dstRect;
        }


        /// <summary>
        /// TODO: find the next existing file. If a file doesnt exist force a reload of the folder structure. apply the same to previous
        /// </summary>
        private void next()
        {
            if (workingData.directoryIndex != -1)
            {
                workingData.directoryIndex++;

                //loop
                if (workingData.directoryIndex >= workingData.entries.Length)
                {
                    workingData.directoryIndex = 0;
                }

                IEntry entry = workingData.entries[workingData.directoryIndex];
                loadPicture(entry);
            }
        }
        private void previous()
        {
            if (workingData.directoryIndex != -1)
            {
                workingData.directoryIndex--;

                //loop
                if (workingData.directoryIndex < 0)
                {
                    workingData.directoryIndex = workingData.entries.Length - 1;
                }

                IEntry entry = workingData.entries[workingData.directoryIndex];
                loadPicture(entry);
            }

        }


        private static string NiceFileSize(long len)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double lend = (double)len;
            int order = 0;
            while (lend >= 1024 && order < sizes.Length - 1)
            {
                order++;
                lend = lend / 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            string result = String.Format("{0:0.##} {1}", lend, sizes[order]);

            return result;
        }


        /// <summary>
        /// History is made of simple strings that represent enough information to load a file:
        ///  - The picture full name
        ///  - The archive file (if any) from which the file derives
        /// </summary>
        /// <param name="tre"></param>
        private void loadPicture(TextRepresentationEntry tre)
        {
            if(tre.ArchiveFile == String.Empty)
            {
                //it's just a regular file, go load it
                loadPicture(tre.FullName);
            }
            else
            {
                if (File.Exists(tre.ArchiveFile))
                {
                    List<ArchiveEntry> imagesFiles = ArchiveEntry.GetImageFiles(tre.ArchiveFile);

                    if(imagesFiles.Count > 0)
                    {
                        int index = imagesFiles.FindIndex(x => x.InternalArchiveFullName.Equals(tre.FullName));

                        //check if image still exist inside this archive. Could have been deleted for whatever reason
                        if(index != -1)
                        {
                            workingData.directoryIndex = index;
                            workingData.entries = imagesFiles.ToArray();
                            loadPicture(workingData.entries[index]);
                        }
                    }  
                }
            }

        }



        /// <summary>
        /// When loading from a single fullname string, this full name can be many different things which leads to different behaviors
        ///  - An image file: list images in the folder, load the image
        ///  - A folder: list images in the folder, load the first image in the folder
        ///  - An image inside an archive: list images inside the folder archive, load the image
        ///  - An archive: list images in the archive, load the first image in the archive
        /// </summary>
        /// <param name="fullname"></param>
        private void loadPicture(string fullname)
        {
            if (File.Exists(fullname))
            {
                //check if its an archive. Here we depend on the file extension. It's not a fullproof method but it's a reasonnable assumption
                //and expectation that the file extension is correct
                string fullnameL = fullname.ToLower();
                if (Config.ArchiveFilter.Any(x => fullnameL.EndsWith(x)))
                {
                    try
                    {
                        List<ArchiveEntry> imagesFiles = ArchiveEntry.GetImageFiles(fullname);
                        if(imagesFiles.Count > 0)
                        {
                            workingData.directoryIndex = 0;
                            workingData.entries = imagesFiles.ToArray();

                            loadPicture(workingData.entries[0]);
                        }
                    }
                    catch(Exception e)
                    {
                        throw (e);
                    }
                   
                }
                else
                {
                    //attempt to load as a regular image
                    string path = Path.GetDirectoryName(fullname);
                    string[] files = Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly).Where(file => Config.ExtensionFilter.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase))).ToArray();
                    int index = Array.FindIndex(files, x => x.Contains(fullname));

                    if(index != -1)
                    {
                        List<IEntry> l = new List<IEntry>();
                        foreach (string s in files)
                        {
                            var e = new FileEntry(s);
                            l.Add(e);
                        }
                        workingData.entries = l.ToArray();
                        workingData.directoryIndex = index;
                        loadPicture(workingData.entries[workingData.directoryIndex]);
                    }
                    else
                    {
                        //the file is probably not a supported image
                    }
                }
            }
            else
            {
                try
                {
                    FileAttributes attr = File.GetAttributes(fullname);
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        var files = Directory.EnumerateFiles(fullname, "*.*", SearchOption.TopDirectoryOnly).Where(file => Config.ExtensionFilter.Any(x => file.EndsWith(x, StringComparison.OrdinalIgnoreCase))).ToArray();
                        //workingData.directoryIndex = Array.FindIndex(files, x => x.Contains(fullname));

                        if (files.Length > 0)
                        {

                            List<IEntry> l = new List<IEntry>();
                            foreach (string s in files)
                            {
                                var e = new FileEntry(s);
                                l.Add(e);
                            }

                            workingData.directoryIndex = 0;
                            workingData.entries = l.ToArray();

                            loadPicture(workingData.entries[0]);

                        }
                        else
                        {
                            //no image files where founds.
                            return;
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    //it's not a file, but its not a directory either? it could be a file inside an archive
                    //TODO: support direct files inside archives
                }
            }



        }

        public void loadPicture(IEntry entry)
        {
            //clean up previously used memory (if any)
            workingData.Dispose();
            workingData.activeEntry = entry;

            //load image
#if DEBUG
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
#endif
            Stream stream = workingData.activeEntry.GetStream();
            try
            {
                workingData.nativeImage = new ImageMagick.MagickImage(stream);
            }
            catch(Exception e)
            {
                close();
                return;
            }
            finally
            {
                stream.Dispose();
            }
#if DEBUG
            stopWatch.Stop();
            System.Diagnostics.Debug.WriteLine(String.Format("Loading image in {0} ms", stopWatch.Elapsed.Milliseconds));
#endif

            //convert to bitmap
            if(Settings.Get.Display.AutoRotate && workingData.nativeImage.Orientation != OrientationType.Undefined)
            {
                workingData.nativeImage.AutoOrient();
            }
            workingData.bitmap = workingData.nativeImage.ToBitmap();


            //check if the image mode should be changed because user wants a specific image size mode on load
            if (viewingMode == ViewingMode.Normal)
            {
                if (Settings.Get.Display.SizeModeOnImageLoad != ImageSizeMode.Restore && Settings.Get.Display.SizeModeOnImageLoad != Settings.Get.Display.SizeMode)
                {
                    Settings.Get.Display.SizeMode = Settings.Get.Display.SizeModeOnImageLoad;
                    refreshImageSizeModeUI();
                }
            }

#if DEBUG
            stopWatch.Start();
#endif
            //Assign image to picture box then refresh sizing
            pictureBox.Image = workingData.bitmap;
            panelMain.Resize -= panelMain_Resize;
            resizePictureBox();
            panelMain.Resize += panelMain_Resize;
#if DEBUG
            stopWatch.Stop();
            System.Diagnostics.Debug.WriteLine(String.Format("Paint done in {0} ms", stopWatch.Elapsed.Milliseconds));
#endif

            //Add loaded file to history if necessary
            TextRepresentationEntry tre = workingData.activeEntry.ToText();
            Settings.Get.History.AddFile(tre);
            SetHistoryList(Settings.Get.History.Get());

            //refresh UI elements
            toolStripComboBoxNavigation_UpdateText(workingData.activeEntry.FullName);
            toolStripStatusLabelImageInfo.Text = String.Format("{0} x {1} - {2} {3}", workingData.nativeImage.BaseWidth, workingData.nativeImage.BaseHeight, workingData.nativeImage.ColorSpace, workingData.nativeImage.ColorType);
            toolStripStatusLabelImageInfo.Visible = true;
            toolStripStatusLabelFileSize.Text = NiceFileSize(workingData.activeEntry.Length);
            toolStripStatusLabelFileSize.Visible = true;
            toolStripStatusLabelImagePosition.Text = String.Format("{0} / {1}", workingData.directoryIndex + 1, workingData.entries.Length);
            toolStripStatusLabelImagePosition.Visible = true;
            this.Text = String.Format("{0} - {1}", workingData.activeEntry.FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
            
        }


        internal void SetHistoryList(List<TextRepresentationEntry> list)
        {
            toolStripComboBoxNavigation.Items.Clear();
            toolStripComboBoxNavigation.Items.AddRange(Settings.Get.History.Get().ToArray());
        }

        private void copy()
        {
            if(workingData.bitmap != null)
            {
                Clipboard.SetImage(workingData.bitmap);
            }
        }

        private void showInformation()
        {
            if(workingData.activeEntry != null)
            {
                FrmInformation f = new FrmInformation(workingData);
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
            panelMain.BorderStyle = BorderStyle.Fixed3D;

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
            panelMain.BorderStyle = BorderStyle.None;
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
            if (workingData.bitmap != null)
            {
                workingData.bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                pictureBox.Image = workingData.bitmap;
            }
        }

        private void verticalFlip()
        {
            if (workingData.bitmap != null)
            {
                workingData.bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBox.Image = workingData.bitmap;
            }
        }

        private void rotateRight()
        {
            if (workingData.bitmap != null)
            {
                workingData.bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox.Image = workingData.bitmap;
                resizePictureBox(); //see rotate left
            }
        }

        private void rotateLeft()
        {
            if (workingData.bitmap != null)
            {
                workingData.bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                pictureBox.Image = workingData.bitmap;

                //this will recenter the image properly in the panel 
                //in case the image width/height are different, if you rotate in normal (not autosize) mode, the 
                //image position X and Y needs to be updated
                resizePictureBox(); 
            }
        }


        /// <summary>
        /// Deletes the file currently being viewed. 
        /// TODO: add a switch based on the configuration to move to recycle bin by default instead of deleting.
        /// TODO: In case of AunauthorizedAccessException prompt user to restart the app in admin mode
        /// </summary>
        private void delete()
        {
            if (workingData.activeEntry != null)
            {
                if (workingData.activeEntry.IsArchive)
                {
                    //TODO: add support for deletion inside an archive
                    MessageBox.Show("This image file is contained inside an archive file.\nIt cannot be deleted.", "Cannot delete file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (MessageBox.Show(String.Format("The file {0} will be permanently deleted.\nAre you sure you want to continue?", workingData.activeEntry.Name), "Delete file?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //before deleting, try to get access to the previous image, which will be automatically loaded upon file deletion.
                    //if there was only one image in the current working folder, then the app will close all

                    string nextFileToLoad = String.Empty;
                    if (workingData.entries.Length > 1)
                    {
                        //will try to move to the file
                        int moveToIndex = workingData.directoryIndex;
                        moveToIndex--;
                        if (moveToIndex < 0) moveToIndex = workingData.entries.Length - 1; //auto loop to the end
                        IEntry entry = workingData.entries[moveToIndex];
                        nextFileToLoad = entry.FullName;
                    }


                    try
                    {
                        workingData.activeEntry.Delete();
                    }
                    catch (UnauthorizedAccessException uaex)
                    {
                        MessageBox.Show("Error wile deleting file\n.Insufficient user privilege.\nPlease restart the app as an administrator.\n\n" + uaex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error wile deleting file\n\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (nextFileToLoad == String.Empty)
                    {
                        //only a single file in the folder -- close
                        close();
                    }
                    else
                    {
                        //load the next image and force a refresh of the folder structure
                        loadPicture(nextFileToLoad);
                    }


                }
            }
        }
    }
}