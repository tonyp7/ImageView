using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.VisualBasic;

namespace ImageView
{
    public class Config : ICloneable
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


    }


    public class ConfigHistory : ICloneable
    {
        public static readonly int MAXIMUM_HISTORY_SIZE = 99;
        public static readonly int DEFAULT_HISTORY_SIZE = 20;

        private List<string> history;
        public int Size = DEFAULT_HISTORY_SIZE;
        public bool SaveOnExit = true;

        


        public ConfigHistory()
        {
            history = new List<string>();
        }

        public void AddFile(string file)
        {
            history.Remove(file); //attemp to delete from history
            history.Insert(0, file); //reinsert as index 0 == last viewed
            
            if(history.Count > Size)
            {
                //delete everything after max size
                history.RemoveRange(Size-1, history.Count - Size);
            }
        }

        public List<string> Get()
        {
            return history;
        }


        public void Save(XmlDocument doc)
        {
            Config.SafeNodeSelect(doc, "/Settings/History/MaxSize", Size.ToString());
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
            ch.Size = this.Size;
            ch.SaveOnExit = this.SaveOnExit;

            return ch;
        }
    }

    

    public class ConfigWindow : ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public System.Windows.Forms.FormWindowState State { get; set; }


        public object Clone()
        {
            ConfigWindow cw = new ConfigWindow();
            cw.X = this.X;
            cw.Y = this.Y;
            cw.Width = this.Width;
            cw.Height = this.Height;

            return cw;
        }

        public void Load(XmlDocument doc)
        {

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

    public class ConfigSlideshow : ICloneable
    {
        public static readonly int MAXIMUM_SLIDESHOW_TIMER = 999999;
        public static readonly int DEFAULT_SLIDESHOW_TIMER = 5000;


        public int Timer { get; set; }


        public ConfigSlideshow()
        {
            Timer = DEFAULT_SLIDESHOW_TIMER;
        }

        public void Save(XmlDocument doc)
        {
            Config.SafeNodeSelect(doc, "/Settings/Slideshow/Zoom", Timer.ToString());
        }

        public object Clone()
        {
            ConfigSlideshow c = new ConfigSlideshow();
            c.Timer = this.Timer;
            return c;
        }
    }

    public class ConfigDisplay : ICloneable
    {
        

        public int Zoom { get; set; }
        public ImageSizeMode SizeMode { get; set; }

        public ConfigDisplay()
        {
            SizeMode = ImageSizeMode.Autosize;
            Zoom = 100;
        }

        public void Save(XmlDocument doc)
        {
            Config.SafeNodeSelect(doc, "/Settings/Display/Zoom", Zoom.ToString());
            Config.SafeNodeSelect(doc, "/Settings/Display/SizeMode", SizeMode.ToString());
        }

        public object Clone()
        {
            ConfigDisplay cd = new ConfigDisplay();

            cd.Zoom = this.Zoom;
            cd.SizeMode = this.SizeMode;

            return cd;
        }
    }


    public enum ImageSizeMode
    {
        Autosize,
        Normal,
        Zoom
    }
}
