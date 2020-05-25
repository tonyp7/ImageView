using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImageView.ImageEntry
{
    /// <summary>
    /// This is a text representation of an image entry that is used for saving in the history configuration file and in the history drop down on the main UI.
    /// </summary>
    public class TextRepresentationEntry : ICloneable, IEquatable<TextRepresentationEntry>
    {
        /// <summary>
        /// Represents the full name of the file. If it's an archive then it's the full name inside the archive
        /// </summary>
        public string FullName;

        /// <summary>
        /// Represents the archive file full name if there is one
        /// </summary>
        public string ArchiveFile;

        public TextRepresentationEntry()
        {
            ArchiveFile = String.Empty;
            FullName = String.Empty;
        }

        public TextRepresentationEntry(string fullName)
        {
            ArchiveFile = String.Empty;
            FullName = fullName;
        }

        public TextRepresentationEntry(string fullName, string archiveFile)
        {
            ArchiveFile = archiveFile;
            FullName = fullName;
        }

        public object Clone()
        {
            return new TextRepresentationEntry(FullName, ArchiveFile);
        }

        public bool Equals(TextRepresentationEntry other)
        {
            return ArchiveFile == other.ArchiveFile && FullName == other.FullName;
        }

        public override string ToString()
        {
            if (ArchiveFile != String.Empty)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append( ArchiveFile );
                sb.Append(Path.DirectorySeparatorChar);
                sb.Append(FullName);
                return sb.ToString();
            }
            else
            {
                return FullName;
            }
        }
    }
}
