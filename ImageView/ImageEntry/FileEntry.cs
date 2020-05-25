using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageView.ImageEntry
{
    class FileEntry : IEntry
    {
        public string Name { get { populateFileInfo(); return _fileInfo.Name; } }
        public string FullName { get { return _fullname; } }
        public bool IsArchive { get { return false; } }
        public long Length { get { populateFileInfo();  return _fileInfo.Length;} }
        public bool Exists { get { populateFileInfo(); return _fileInfo.Exists; } }
        public string DirectoryName { get { populateFileInfo(); return _fileInfo.DirectoryName; } }
        public DateTime CreationTime { get { populateFileInfo(); return _fileInfo.CreationTime; } }
        public DateTime LastWriteTime { get { populateFileInfo(); return _fileInfo.LastWriteTime; } }

        private string _fullname;
        private FileInfo _fileInfo;

        public Stream GetStream()
        {
            return new FileStream(_fullname, FileMode.Open, FileAccess.Read);
        }

        public byte[] GetData()
        {
            Stream s = GetStream();
            MemoryStream ms = new MemoryStream();
            s.CopyTo(ms);
            byte[] b = ms.ToArray();

            s.Dispose();
            ms.Dispose();

            return b;

        }


        public void Delete()
        {
            populateFileInfo();
            _fileInfo.Delete();
        }

        public FileEntry(string fullName)
        {
            _fileInfo = null;
            _fullname = fullName;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void populateFileInfo()
        {
            if (_fileInfo == null)
            {
                _fileInfo = new FileInfo(_fullname);
            }
        }

        public TextRepresentationEntry ToText()
        {
            return new TextRepresentationEntry(_fullname);
        }
    }
}
