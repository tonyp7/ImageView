using ImageView.ImageEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageView.Exceptions
{
    public class ImageViewLoadException : Exception
    {
        public new Exception InnerException { get; set; }
        public IEntry Entry { get; set; }

        public ImageViewLoadException(Exception innerException, IEntry entry)
        {
            this.InnerException = innerException;
            this.Entry = entry;
        }
    }
}
