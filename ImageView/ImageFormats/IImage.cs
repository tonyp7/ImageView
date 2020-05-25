using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageView.ImageFormats
{
    /// <summary>
    /// Place holder for a generic image type
    /// </summary>
    public interface IImage : IDisposable
    {
        int BaseWidth { get; }
        int BaseHeight { get; }

        Bitmap ToBitmap();

    }
}
