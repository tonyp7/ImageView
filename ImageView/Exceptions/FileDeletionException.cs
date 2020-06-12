using ImageView.ImageEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageView.Exceptions
{
    public class FileDeletionException : Exception
    {

        public new Exception InnerException { get; set; }

        public IEntry Entry { get; set; }

        public new string Message { get; set; }

        public FileDeletionException(Exception innerException, IEntry entry)
        {
            this.InnerException = innerException;
            this.Entry = entry;
        }


        public FileDeletionException(IEntry entry)
        {
            this.Entry = entry;
            this.InnerException = null;
        }


    }
}
