using ImageView.Configuration;
using SevenZipExtractor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageView.ImageEntry
{
    class ArchiveEntry : IEntry
    {

        /// <summary>
        /// Just the filename, e.g: myimage.jpg
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// Represents the complete path to the file, including the archive, e.g; c:\mybook.cbz\path\to\myimage.jpg
        /// </summary>
        public string FullName { get { return _fullName; } }
        public bool IsArchive { get { return true; } }
        public long Length { get { return _size; } }
        public bool Exists { get { return true; } }

        public string DirectoryName { get { return _archiveFullname; } }
        public DateTime CreationTime { get { return _creationTime; } }
        public DateTime LastWriteTime { get { return _lastWriteTime; } }

        public string InternalArchiveFullName { get { return _internalArchiveFullName; } }

        private long _size;
        private string _name;
        private string _fullName;
        private string _archiveFullname;
        private DateTime _creationTime;
        private DateTime _lastWriteTime;

        private string _internalArchiveFullName;

        public Stream GetStream()
        {
            
            ArchiveFile archiveFile = new ArchiveFile(_archiveFullname);
            var filtered = archiveFile.Entries.Where(entry => entry.FileName.Equals(Name)).ToList();

            if(filtered.Count > 0)
            {
                MemoryStream memoryStream = new MemoryStream();
                filtered[0].Extract(memoryStream);
                memoryStream.Position = 0;
                archiveFile.Dispose();
                return memoryStream;
            }
            else
            {
                archiveFile.Dispose();
                return null;
            }
        }

        public byte[] GetData()
        {
            return ((MemoryStream)GetStream()).ToArray();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public ArchiveEntry(Entry entry, string archiveFullname)
        {

            _creationTime = entry.CreationTime;
            _lastWriteTime = entry.LastWriteTime;
            _internalArchiveFullName = entry.FileName;
            _size = (long)entry.Size;

            //get the name from the internal full name path
            _name = Path.GetFileName(entry.FileName);


            _archiveFullname = archiveFullname;
            _fullName = _archiveFullname + Path.DirectorySeparatorChar + _internalArchiveFullName;

        }

        public TextRepresentationEntry ToText()
        {
            return new TextRepresentationEntry(_internalArchiveFullName, _archiveFullname);
        }


        /// <summary>
        /// Returns all images found in an archive file
        /// </summary>
        /// <param name="archiveFileFullName">Path to the archive file</param>
        /// <returns>A list of ArchiveEntry objects containing all suspected image files</returns>
        public static List<ArchiveEntry> GetImageFiles(string archiveFileFullName)
        {
            ArchiveFile arc = new ArchiveFile(archiveFileFullName);
            List<ArchiveEntry> imagesFiles = new List<ArchiveEntry>();

            var files = arc.Entries.Where(file => Config.ExtensionFilter.Any(x => file.FileName.EndsWith(x, StringComparison.OrdinalIgnoreCase))).ToList();
            if (files.Count > 0)
            {
                foreach (Entry archiveEntry in files)
                {
                    //check that we are not adding a folder named HELLO.JPG
                    if (!archiveEntry.IsFolder)
                    {
                        var e = new ArchiveEntry(archiveEntry, archiveFileFullName);
                        imagesFiles.Add(e);
                    }
                }
            }

            arc.Dispose();

            return imagesFiles;
        }


    }
}
