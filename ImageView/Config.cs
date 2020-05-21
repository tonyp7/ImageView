using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace ImageView
{
    public class Config : ICloneable, IEquatable<Config>
    {
        private string configFileLocation = null;
        public XmlDocument configFileDoc = null;

        public ConfigHistory History;
        public ConfigDisplay Display;
        public ConfigWindow Window;
        public ConfigSlideshow Slideshow;

        public readonly string[] ExtensionFilter = new string[]{ ".jpg", ".jpeg", ".png", ".bmp", ".tiff", ".tif", ".gif" };

        //private Window window;
        //private Slideshow slideshow;


        public object Clone()
        {
            Config c = new Config();

            c.configFileLocation = (string)this.configFileLocation.Clone();
            c.configFileDoc = (XmlDocument)configFileDoc.Clone();

            c.History = (ConfigHistory)this.History.Clone();
            c.Display = (ConfigDisplay)this.Display.Clone();
            c.Window = (ConfigWindow)this.Window.Clone();
            c.Slideshow = (ConfigSlideshow)this.Slideshow.Clone();

            return c;
        }


        public void Save()
        {

            History.Save(configFileDoc);
            Display.Save(configFileDoc);
            Window.Save(configFileDoc);
            Slideshow.Save(configFileDoc);

            try
            {
                configFileDoc.Save(configFileLocation);
            }
            catch (Exception)
            {
                //TODO: decide if this error has to stay silent or alert the user with a messagebox
            }
            
        }

        
        public void Load()
        {
            try
            {
                configFileDoc = new XmlDocument();
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), System.Reflection.Assembly.GetEntryAssembly().GetName().Name);
                // Directory does not exist could indicate first time launch, or directory deleted by mistake
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                configFileLocation = Path.Combine(path, "config.xml");

                // Similarly for the config file itself
                if (!File.Exists(configFileLocation))
                {
                    using (var sw = File.CreateText(configFileLocation))
                    {
                        sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?><Settings></Settings>");
                        sw.Flush();
                        sw.Close();
                    }
                }
                configFileDoc.Load(configFileLocation);

                History = new ConfigHistory();
                Display = new ConfigDisplay();
                Window = new ConfigWindow();
                Slideshow = new ConfigSlideshow();


                //restore previous config
                History.Load(configFileDoc);
                Display.Load(configFileDoc);
                Window.Load(configFileDoc);
                Slideshow.Load(configFileDoc);


            }
            catch (Exception)
            {
                //Somehow we could not access the config file or create it
                //Program will work, but nothing will be saved
            }
        }


        public Config()
        {

        }



        /// <summary>
        /// Utility function to create missing nodes through a given xpath in case they don't exist
        /// </summary>
        /// <param name="doc">XML document to update</param>
        /// <param name="nodePath">XPath of the node</param>
        /// <param name="value">Optional InnerText value to assign to the node</param>
        public static XmlNode SafeNodeSelect(XmlDocument doc, string nodePath, string value = null)
        {
            //split path
            string[] p = nodePath.Split('/');

            //select beginning node
            XmlNode currentNode = doc.SelectSingleNode("/");

            string currentPath = "/";
            for(int i=0; i < p.Length; i++)
            {
                currentPath += p[i];

                //if the node exists, we continue to iterate through the path. 
                //If not, we create it and move through the path
                XmlNode n = doc.SelectSingleNode(currentPath);
                if(n == null)
                {
                    XmlElement e = doc.CreateElement(p[i]);
                    currentNode.AppendChild(e);
                    currentNode = e;
                }
                else
                {
                    currentNode = n;
                }

                currentPath += "/";
            }

            //at the end of the process we've reached the node so we can finally assign its value
            if(value != null)
            {
                currentNode.InnerText = value;
            }

            return currentNode;


        }

        public bool Equals(Config other)
        {
            return Display.Equals(other) && History.Equals(other) && Slideshow.Equals(other) && Window.Equals(other);
        }
    }


    public class ConfigHistory : ICloneable, IEquatable<ConfigHistory>
    {
        public static readonly int MAXIMUM_HISTORY_SIZE = 99;
        public static readonly int DEFAULT_HISTORY_SIZE = 20;

        private List<string> history;

        private int size;
        public int MaxSize {
            get
            {
                return size;
            }
            set
            {
                size = value;

                //clean up history in case there are more items in history than its max capacity
                if (history.Count > size)
                {
                    history.RemoveRange(size, history.Count - size);
                }
            }
        }
        public bool SaveOnExit = true;

        

        public int CurrentSize
        {
            get
            {
                return history.Count;
            }
        }

        public ConfigHistory()
        {
            history = new List<string>();
            size = DEFAULT_HISTORY_SIZE;
        }

        public void AddFile(string file)
        {
            history.Remove(file); //attemp to delete from history
            history.Insert(0, file); //reinsert as index 0 == last viewed
            
            if(history.Count > MaxSize)
            {
                //delete everything after max size
                history.RemoveRange(MaxSize, history.Count - MaxSize);
            }
        }

        public List<string> Get()
        {
            return history;
        }

 
        public void Load(XmlDocument doc)
        {
            XmlNode n;

            int ivalue;
            bool bvalue;


            //max size
            n = doc.SelectSingleNode("/Settings/History/MaxSize");
            if(n != null && int.TryParse(n.InnerText, out ivalue) && ivalue >= 0 && ivalue <= MAXIMUM_HISTORY_SIZE)
            {
                size = ivalue;
            }
            else
            {
                size = DEFAULT_HISTORY_SIZE;
            }

            //save on exit
            n = doc.SelectSingleNode("/Settings/History/SaveOnExit");
            if(n!=null && bool.TryParse(n.InnerText, out bvalue))
            {
                SaveOnExit = bvalue;
            }

            //history
            XmlNodeList nlist = doc.SelectNodes("/Settings/History/Files/File");
            if(nlist != null)
            {
                int i = 1;
                foreach(XmlNode nd in nlist)
                {
                    history.Add(nd.InnerText);
                    if(i > size)
                    {
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
            }

        }
        public void Save(XmlDocument doc)
        {
            Config.SafeNodeSelect(doc, "/Settings/History/MaxSize", MaxSize.ToString());
            Config.SafeNodeSelect(doc, "/Settings/History/SaveOnExit", SaveOnExit.ToString());

            XmlNode n = Config.SafeNodeSelect(doc, "/Settings/History/Files");

            n.RemoveAll(); //delete all history
            if (SaveOnExit)
            {
                foreach(string s in history)
                {
                    XmlElement e = doc.CreateElement("File");
                    e.InnerText = s;
                    n.AppendChild(e);
                }
            }

        }

        public object Clone()
        {
            ConfigHistory ch = new ConfigHistory();
            ch.history.AddRange(this.history);
            ch.MaxSize = this.MaxSize;
            ch.SaveOnExit = this.SaveOnExit;

            return ch;
        }

        public bool Equals(ConfigHistory other)
        {
            return SaveOnExit == other.SaveOnExit && MaxSize == other.MaxSize;
        }
    }

    

    public class ConfigWindow : ICloneable, IEquatable<ConfigWindow>
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

    public class ConfigSlideshow : ICloneable, IEquatable<ConfigSlideshow>
    {
        public static readonly int MAXIMUM_SLIDESHOW_TIMER = 999999;
        public static readonly int DEFAULT_SLIDESHOW_TIMER = 5000;


        public int Timer { get; set; }


        public ConfigSlideshow()
        {
            Timer = DEFAULT_SLIDESHOW_TIMER;
        }

        public void Load(XmlDocument doc)
        {
            XmlNode n;

            int ivalue;

            //timer
            n = doc.SelectSingleNode("/Settings/Slideshow/Timer");
            if (n != null && int.TryParse(n.InnerText, out ivalue) && ivalue >= 0 && ivalue <= MAXIMUM_SLIDESHOW_TIMER)
            {
                Timer = ivalue;
            }
            else
            {
                Timer = DEFAULT_SLIDESHOW_TIMER;
            }
        }

        public void Save(XmlDocument doc)
        {
            Config.SafeNodeSelect(doc, "/Settings/Slideshow/Timer", Timer.ToString());
        }

        public object Clone()
        {
            ConfigSlideshow c = new ConfigSlideshow();
            c.Timer = this.Timer;
            return c;
        }

        public bool Equals(ConfigSlideshow other)
        {
            throw new NotImplementedException();
        }
    }

    public class ConfigDisplay : ICloneable, IEquatable<ConfigDisplay>
    {
        

        public int Zoom { get; set; }
        public ImageSizeMode SizeMode { get; set; }
        public int ZoomStep { get; set; }

        private static readonly int DEFAULT_ZOOM = 100;
        private static readonly int DEFAULT_ZOOM_STEP = 25;
        public static readonly int MAX_ZOOM = 400;
        private static readonly ImageSizeMode DEFAULT_IMAGESIZEMODE = ImageSizeMode.Autosize;

        public ConfigDisplay()
        {
            SizeMode = DEFAULT_IMAGESIZEMODE;
            Zoom = DEFAULT_ZOOM;
            ZoomStep = DEFAULT_ZOOM_STEP;
        }

        public void Load(XmlDocument doc)
        {
            XmlNode n;
            int ivalue;

            //zoom
            n = doc.SelectSingleNode("/Settings/Display/Zoom");
            if (n != null && int.TryParse(n.InnerText, out ivalue) && ivalue > 0)
            {
                Zoom = ivalue;
            }
            else
            {
                Zoom = DEFAULT_ZOOM;
            }


            //zoom step
            n = doc.SelectSingleNode("/Settings/Display/ZoomStep");
            if (n != null && int.TryParse(n.InnerText, out ivalue) && ivalue > 0)
            {
                ZoomStep = ivalue;
            }
            else
            {
                ZoomStep = DEFAULT_ZOOM_STEP;
            }

            //image size mode
            n = doc.SelectSingleNode("/Settings/Display/SizeMode");
            if(n != null)
            {
                switch (n.InnerText)
                {
                    case "Autosize":
                        SizeMode = ImageSizeMode.Autosize;
                        break;
                    case "Normal":
                        SizeMode = ImageSizeMode.Normal;
                        break;
                    case "Zoom":
                        SizeMode = ImageSizeMode.Zoom;
                        break;
                    default:
                        SizeMode = DEFAULT_IMAGESIZEMODE;
                        break;

                }
            }
            else
            {
                SizeMode = DEFAULT_IMAGESIZEMODE;
            }
        }

        public void Save(XmlDocument doc)
        {
            Config.SafeNodeSelect(doc, "/Settings/Display/Zoom", Zoom.ToString());
            Config.SafeNodeSelect(doc, "/Settings/Display/ZoomStep", SizeMode.ToString());
            Config.SafeNodeSelect(doc, "/Settings/Display/SizeMode", SizeMode.ToString());
            
        }

        public object Clone()
        {
            ConfigDisplay cd = new ConfigDisplay();

            cd.Zoom = this.Zoom;
            cd.SizeMode = this.SizeMode;
            cd.ZoomStep = this.ZoomStep;

            return cd;
        }

        public bool Equals(ConfigDisplay other)
        {
            return Zoom == other.Zoom && SizeMode == other.SizeMode && ZoomStep == other.ZoomStep;
        }
    }


    public enum ImageSizeMode
    {
        Autosize,
        Normal,
        Zoom
    }
}
