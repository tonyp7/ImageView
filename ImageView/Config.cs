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
    public class Config
    {
        private string configFileLocation;
        private XmlDocument configFileDoc;

        public ConfigHistory History;
        public ConfigDisplay Display;

        public readonly string[] ExtensionFilter = new string[]{ ".jpg", ".jpeg", ".png", ".bmp", ".tiff", ".tif", ".gif" };

        //private Window window;
        //private Slideshow slideshow;

        public Config()
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
                        sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?><ImageViewConfig></ImageViewConfig>");
                        sw.Flush();
                        sw.Close();
                    }
                }            
                configFileDoc.Load(configFileLocation);

                History = new ConfigHistory();
                Display = new ConfigDisplay();

            }
            catch(Exception)
            {
                //Somehow we could not access the config file or create it
                //Program will work, but nothing will be saved
            }

        }
    }


    public class ConfigHistory
    {
        private List<string> history;
        public int MaxSize = 20;

        public ConfigHistory()
        {
            history = new List<string>();
        }

        public void AddFile(string file)
        {
            history.Remove(file); //attemp to delete from history
            history.Insert(0, file); //reinsert as index 0 == last viewed
            
            if(history.Count > MaxSize)
            {
                //delete everything after max size
                history.RemoveRange(MaxSize-1, history.Count - MaxSize);
            }
        }

        public List<string> Get()
        {
            return history;
        }

    }

    public class ConfigDisplay
    {
        

        public int Zoom { get; set; }
        public ImageSizeMode SizeMode { get; set; }

        public ConfigDisplay()
        {
            SizeMode = ImageSizeMode.Autosize;
            Zoom = 100;
        }
    }


    public enum ImageSizeMode
    {
        Autosize,
        Normal,
        Zoom
    }
}
