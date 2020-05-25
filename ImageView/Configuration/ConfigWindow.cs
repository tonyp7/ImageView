using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ImageView.Configuration
{
    public class ConfigWindow : IConfigurationItem, IEquatable<ConfigWindow>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public System.Windows.Forms.FormWindowState State { get; set; }

        private static readonly int DEFAULT_WINDOW_WIDTH = 1024;
        private static readonly int DEFAULT_WINDOW_HEIGHT = 768;
        private static readonly int DEFAULT_WINDOW_X = (Screen.PrimaryScreen.Bounds.Width - DEFAULT_WINDOW_WIDTH) >> 1;
        private static readonly int DEFAULT_WINDOW_Y = (Screen.PrimaryScreen.Bounds.Height - DEFAULT_WINDOW_HEIGHT) >> 1;
        private static readonly System.Windows.Forms.FormWindowState DEFAULT_WINDOW_STATE = System.Windows.Forms.FormWindowState.Normal;


        public ConfigWindow()
        {
            X = DEFAULT_WINDOW_X;
            Y = DEFAULT_WINDOW_Y;
            State = DEFAULT_WINDOW_STATE;
            Width = DEFAULT_WINDOW_WIDTH;
            Height = DEFAULT_WINDOW_HEIGHT;
        }

        public object Clone()
        {
            ConfigWindow cw = new ConfigWindow();
            cw.X = this.X;
            cw.Y = this.Y;
            cw.Width = this.Width;
            cw.Height = this.Height;
            cw.State = this.State;

            return cw;
        }

        public bool Equals(ConfigWindow other)
        {
            return X == other.X && Y == other.Y && Width == other.Width && Height == other.Height && State == other.State;
        }

        public void Load(XmlDocument doc)
        {
            int ivalue;
            XmlNode n;

            n = doc.SelectSingleNode("/Settings/Window/X");
            if (n != null && int.TryParse(n.InnerText, out ivalue) && ivalue >= 0)
            {
                X = ivalue;
            }

            n = doc.SelectSingleNode("/Settings/Window/Y");
            if (n != null && int.TryParse(n.InnerText, out ivalue) && ivalue >= 0)
            {
                Y = ivalue;
            }

            n = doc.SelectSingleNode("/Settings/Window/Width");
            if (n != null && int.TryParse(n.InnerText, out ivalue) && ivalue >= 0)
            {
                Width = ivalue;
            }

            n = doc.SelectSingleNode("/Settings/Window/Height");
            if (n != null && int.TryParse(n.InnerText, out ivalue) && ivalue >= 0)
            {
                Height = ivalue;
            }


            n = doc.SelectSingleNode("/Settings/Window/State");
            if (n != null)
            {
                switch (n.InnerText)
                {
                    case "Maximized":
                        State = System.Windows.Forms.FormWindowState.Maximized;
                        break;
                    case "Minimized":
                        State = System.Windows.Forms.FormWindowState.Minimized;
                        break;
                    case "Normal":
                        State = System.Windows.Forms.FormWindowState.Normal;
                        break;
                    default:
                        State = DEFAULT_WINDOW_STATE;
                        break;

                }
            }
            else
            {
                State = DEFAULT_WINDOW_STATE;
            }

        }

        public void Save(XmlDocument doc)
        {
            Config.SafeNodeSelect(doc, "/Settings/Window/X", X.ToString());
            Config.SafeNodeSelect(doc, "/Settings/Window/Y", Y.ToString());
            Config.SafeNodeSelect(doc, "/Settings/Window/Width", Width.ToString());
            Config.SafeNodeSelect(doc, "/Settings/Window/Height", Height.ToString());
            Config.SafeNodeSelect(doc, "/Settings/Window/State", State.ToString());
        }
    }
}
