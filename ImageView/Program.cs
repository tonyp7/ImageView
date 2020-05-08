using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

/// <summary>
/// Icon GPL Nick Roach
/// https://www.iconfinder.com/icons/1055042/image_photo_photography_picture_icon
/// https://www.iconfinder.com/iconsets/circle-icons-1
/// </summary>

namespace ImageView
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmMain f = new FrmMain();
            Application.Run(f);
        }

        public static void LaunchURL(string url)
        {

            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch { }
        }

        /// <summary>
        /// For some reason Math.Clamp seems to be missing from my framework so here's a crude implementation
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(int value, int min, int max)
        {
            return value < min ? min : (value > max ? max : value);
        }
    }
}
