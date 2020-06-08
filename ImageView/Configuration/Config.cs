/*
MIT License

Copyright (c) 2020 Tony Pottier

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace ImageView.Configuration
{

    public sealed class Settings
    {

        private static Config _config = new Config();
        public static Config Get
        {
            get { return _config; }
            set { _config = value; }
        }

        private Settings()
        {

        }
        static Settings()
        {

        }
    }

    public class Config : ICloneable, IEquatable<Config>
    {
        private string configFileLocation = null;
        public XmlDocument configFileDoc = null;

        public ConfigGeneral General;
        public ConfigHistory History;
        public ConfigDisplay Display;
        public ConfigWindow Window;
        public ConfigSlideshow Slideshow;
        public ConfigReader Reader;

        public static readonly string[] ExtensionFilter = new string[]{ ".jpg", ".jpeg", ".png", ".bmp", "*.dib", ".tiff", ".tif", ".gif", ".webp", ".cr2", ".dng", ".jp2", ".psd", ".svg", ".tga" };
        public static readonly string[] ArchiveFilter = new string[] { ".cbz", ".cbr", ".cb7", ".cba", ".cbt", ".7z", ".zip", ".rar" };

        public object Clone()
        {
            Config c = new Config();

            c.configFileLocation = (string)this.configFileLocation.Clone();
            c.configFileDoc = (XmlDocument)configFileDoc.Clone();

            c.General = (ConfigGeneral)this.General.Clone();
            c.History = (ConfigHistory)this.History.Clone();
            c.Display = (ConfigDisplay)this.Display.Clone();
            c.Window = (ConfigWindow)this.Window.Clone();
            c.Slideshow = (ConfigSlideshow)this.Slideshow.Clone();
            c.Reader = (ConfigReader)this.Reader.Clone();

            return c;
        }


        public void Save()
        {

            General.Save(configFileDoc);
            History.Save(configFileDoc);
            Display.Save(configFileDoc);
            Window.Save(configFileDoc);
            Slideshow.Save(configFileDoc);
            Reader.Save(configFileDoc);

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

                General = new ConfigGeneral();
                History = new ConfigHistory();
                Display = new ConfigDisplay();
                Window = new ConfigWindow();
                Slideshow = new ConfigSlideshow();
                Reader = new ConfigReader();


                //restore previous config
                General.Load(configFileDoc);
                History.Load(configFileDoc);
                Display.Load(configFileDoc);
                Window.Load(configFileDoc);
                Slideshow.Load(configFileDoc);
                Reader.Load(configFileDoc);


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


    public enum ImageSizeMode : int
    {
        [Description("Best Fit")]
        BestFit = 0,

        [Description("Real Size")]
        RealSize = 3,

        [Description("Zoom")]
        Zoom = 5,

        [Description("FitToWidth")]
        FitToWidth = 1,

        [Description("FitToHeight")]
        FitToHeight = 2,

        [Description("Restore")]
        Restore = -1
    };

    public enum ViewingMode
    {
        Normal,
        Slideshow,
        Reader
    }

}
