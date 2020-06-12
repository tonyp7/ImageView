using ImageMagick;
using ImageView.Configuration;
using ImageView.Exceptions;
using ImageView.ImageEntry;
using SevenZipExtractor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageView
{
    public class State : IDisposable
    {

        private Bitmap bitmap;
        private IEntry activeEntry;
        private List<IEntry> entries;
        private MagickImage nativeImage;


        public Bitmap Bitmap 
        { 
            get { return bitmap; }
            set { bitmap = value; } 
        }
        public IEntry ActiveEntry 
        { 
            get { return activeEntry; }
        }
        public List<IEntry> Entries 
        { 
            get { return entries; }
        }

        public int ActiveEntryIndex
        {
            get
            {
                return directoryIndex;
            }
        }

        public MagickImage NativeImage
        {
            get
            {
                return nativeImage;
            }
        }

        private int directoryIndex;

        public bool IsBrowsingArchive 
        { 
            get
            {
                if(activeEntry != null)
                {
                    return activeEntry.IsArchive;
                }
                else
                {
                    return false;
                }
            }
        }
        public ArchiveFile Archive { get; set; }

        public State()
        {
            entries = new List<IEntry>();
            directoryIndex = -1;
            activeEntry = null;
            bitmap = null;
            nativeImage = null;
        }

        public void Reset()
        {
            entries.Clear();
            directoryIndex = -1;
            activeEntry = null;
            bitmap = null;
        }


        public void Close()
        {
            Dispose();
            Reset();
        }


        public bool Next()
        {
            if (directoryIndex != -1)
            {
                directoryIndex++;

                //loop back to 0 if reached the end
                if (directoryIndex >= entries.Count)
                {
                    directoryIndex = 0;
                }

                IEntry entry = entries[directoryIndex];
                return loadPicture(entry);
            }
            else
            {
                return false;
            }
        }

        public bool Previous()
        {
            if (directoryIndex != -1)
            {
                directoryIndex--;

                //loop
                if (directoryIndex < 0)
                {
                    directoryIndex = entries.Count - 1;
                }

                IEntry entry = entries[directoryIndex];
                return loadPicture(entry);
            }
            else
            {
                return false;
            }
        }


        public bool HorizontalFlip()
        {
            if (bitmap != null)
            {
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool VerticalFlip()
        {
            if (bitmap != null)
            {
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RotateRight()
        {
            if (bitmap != null)
            {
                bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RotateLeft()
        {
            if (bitmap != null)
            {
                bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                return true;
            }
            else
            {
                return false;
            }
        }


        private bool loadPicture(IEntry entry)
        {

            this.Dispose();
            this.activeEntry = entry;

            Exception exception = null;

            //load image
#if DEBUG
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
#endif
            Stream stream = entry.GetStream();
            try
            {
                this.nativeImage = new ImageMagick.MagickImage(stream);
            }
            catch (Exception e)
            {
                this.nativeImage = new MagickImage(Properties.Resources.error);
                exception = e;
            }
            stream.Dispose();
#if DEBUG
            stopWatch.Stop();
            System.Diagnostics.Debug.WriteLine(String.Format("Loading image in {0} ms", stopWatch.Elapsed.Milliseconds));
#endif

            //convert to bitmap
            if (Settings.Get.Display.AutoRotate && nativeImage.Orientation != OrientationType.Undefined)
            {
                nativeImage.AutoOrient();
            }
            bitmap = nativeImage.ToBitmap();


            //Add loaded file to history if necessary
            TextRepresentationEntry tre = activeEntry.ToText();
            Settings.Get.History.AddFile(tre);

            //if there was an exception throw it
            if(exception != null)
            {
                throw new ImageViewLoadException(exception, entry);
            }

            return true;
        }

        /// <summary>
        /// History is made of simple strings that represent enough information to load a file:
        ///  - The picture full name
        ///  - The archive file (if any) from which the file derives
        /// </summary>
        /// <param name="tre"></param>
        public bool LoadPicture(TextRepresentationEntry tre)
        {
            if (tre.ArchiveFile == String.Empty)
            {
                //it's just a regular file, go load it
                return LoadPicture(tre.FullName);
            }
            else
            {
                if (File.Exists(tre.ArchiveFile))
                {
                    List<ArchiveEntry> imagesFiles = ArchiveEntry.GetImageFiles(tre.ArchiveFile);

                    if (imagesFiles.Count > 0)
                    {
                        int index = imagesFiles.FindIndex(x => x.InternalArchiveFullName.Equals(tre.FullName));

                        //check if image still exist inside this archive. Could have been deleted for whatever reason
                        if (index != -1)
                        {
                            directoryIndex = index;
                            entries = imagesFiles.ToList<IEntry>();
                            return loadPicture(entries[index]);
                        }
                    }
                }
            }

            return false;
        }


        /// <summary>
        /// Deletes the file currently being viewed. 
        /// TODO: add a switch based on the configuration to move to recycle bin by default instead of deleting.
        /// TODO: In case of AunauthorizedAccessException prompt user to restart the app in admin mode
        /// </summary>
        public void Delete()
        {
            if (activeEntry != null)
            {
                if (activeEntry.IsArchive)
                {
                    throw new ArchiveFileDeletionException();
                }
                else
                {
                    //before deleting, try to get access to the previous image, which will be automatically loaded upon file deletion.
                    //if there was only one image in the current working folder, then the app will close all

                    string nextFileToLoad = String.Empty;
                    if (entries.Count > 1)
                    {
                        //will try to move to the file
                        int moveToIndex = directoryIndex;
                        moveToIndex--;
                        if (moveToIndex < 0) moveToIndex = entries.Count - 1; //auto loop to the end
                        IEntry entry = entries[moveToIndex];
                        nextFileToLoad = entry.FullName;
                    }


                    Exception exception = null;

                    try
                    {
                        activeEntry.Delete();
                    }
                    catch (UnauthorizedAccessException uaex)
                    {
                        exception = uaex;
                    }
                    catch (Exception e)
                    {
                        exception = e;
                    }
                    finally
                    {
                        if (nextFileToLoad == String.Empty)
                        {
                            //only a single file in the folder -- close
                            //Close();
                            exception = new NoFileToLoadException();
                        }
                        else
                        {
                            //load the next image and force a refresh of the folder structure
                            LoadPicture(nextFileToLoad);
                        }
                    }

                    if(exception != null)
                    {
                        throw (exception);
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
        public bool LoadPicture(string fullname)
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
                        if (imagesFiles.Count > 0)
                        {
                            directoryIndex = 0;
                            entries = imagesFiles.ToList<IEntry>();

                            return loadPicture(entries[0]);
                        }
                    }
                    catch (Exception e)
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

                    if (index != -1)
                    {
                        List<IEntry> l = new List<IEntry>();
                        foreach (string s in files)
                        {
                            var e = new FileEntry(s);
                            l.Add(e);
                        }
                        entries = l;
                        directoryIndex = index;
                        return loadPicture(entries[directoryIndex]);
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

                            directoryIndex = 0;
                            entries = l;

                            return loadPicture(entries[0]);

                        }
                        else
                        {
                            //no image files where founds.
                            return false;
                        }
                    }
                }
                catch (FileNotFoundException notfounde)
                {
                    //it's not a file, but its not a directory either? it could be a file inside an archive
                    //TODO: support direct files inside archives
                    throw (notfounde);
                }
                catch (DirectoryNotFoundException notfounde)
                {
                    notfounde.Data.Add("fullname", fullname);
                    throw (notfounde);
                    
                }
            }

            return false;
        }

        public void Dispose()
        {
            if (Bitmap != null)
            {
                Bitmap.Dispose();
                Bitmap = null;
            }

            if (nativeImage != null && !nativeImage.IsDisposed)
            {
                nativeImage.Dispose();
                nativeImage = null;
            }

            if (Archive != null)
            {
                Archive.Dispose();
                Archive = null;
            }
        }
    }
}
