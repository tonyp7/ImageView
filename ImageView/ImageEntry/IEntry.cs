using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageView.ImageEntry
{
    public interface IEntry
    {
        string Name { get; }
        string FullName { get; }
        Stream GetStream();
        byte[] GetData();
        long Length { get; }
        bool IsArchive { get; }
        bool Exists { get; }
        void Delete();
        string DirectoryName { get; }
        DateTime CreationTime { get; }
        DateTime LastWriteTime { get; }
        TextRepresentationEntry ToText();


    }

 //   public class ImageEntryFactory
 //   {
//
//        private static string[] archiveFilter = new string[] { ".cbz", ".cbr", ".cb7", ".cba", ".cbt"};
//        IEntry GetEntry(string fullName)
//        {
//
//            if (archiveFilter.Any(x => fullName.ToLower().EndsWith(x)))
//            {
//
 //           }
  //      }
  //  }
}
