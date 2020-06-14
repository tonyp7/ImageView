using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ImageView
{
    public partial class PreviewPanel : UserControl
    {
        public PreviewPanel()
        {
            InitializeComponent();

            //disable horizontal scroll
            //panel.HorizontalScroll.Maximum = 0;
            //panel.AutoScroll = false;
            //panel.VerticalScroll.Visible = false;
            //panel.AutoScroll = true;

            //ShowScrollBar(panel.Handle, (int)ScrollBarDirection.SB_HORZ, false);
        }



        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        private enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
        }

    }



}
