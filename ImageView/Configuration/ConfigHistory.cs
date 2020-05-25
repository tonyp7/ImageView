using ImageView.ImageEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImageView.Configuration
{
    public class ConfigHistory : IConfigurationItem, IEquatable<ConfigHistory>
    {
        public static readonly int MAXIMUM_HISTORY_SIZE = 99;
        public static readonly int DEFAULT_HISTORY_SIZE = 20;

        private List<TextRepresentationEntry> history;

        private int size;
        public int MaxSize
        {
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
            history = new List<TextRepresentationEntry>();
            size = DEFAULT_HISTORY_SIZE;
        }

        public void AddFile(TextRepresentationEntry file)
        {
            //attemp to delete from history
            history.Remove(file); 
            history.Insert(0, file); //reinsert as index 0 == last viewed

            if (history.Count > MaxSize)
            {
                //delete everything after max size
                history.RemoveRange(MaxSize, history.Count - MaxSize);
            }
        }

        public List<TextRepresentationEntry> Get()
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
            if (n != null && int.TryParse(n.InnerText, out ivalue) && ivalue >= 0 && ivalue <= MAXIMUM_HISTORY_SIZE)
            {
                size = ivalue;
            }
            else
            {
                size = DEFAULT_HISTORY_SIZE;
            }

            //save on exit
            n = doc.SelectSingleNode("/Settings/History/SaveOnExit");
            if (n != null && bool.TryParse(n.InnerText, out bvalue))
            {
                SaveOnExit = bvalue;
            }

            //history
            XmlNodeList nlist = doc.SelectNodes("/Settings/History/Files/File");
            if (nlist != null)
            {
                int i = 1;
                foreach (XmlNode nd in nlist)
                {
                    TextRepresentationEntry tre = new TextRepresentationEntry(nd.InnerText);

                    //if there's an archive attribute get that too
                    var attrib = nd.SelectSingleNode("@archive");
                    if(attrib != null && attrib.Value != String.Empty)
                    {
                        tre.ArchiveFile = attrib.Value;
                    }

                    history.Add(tre);
                    if (i > size)
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
                foreach (TextRepresentationEntry tre in history)
                {
                    XmlElement e = doc.CreateElement("File");
                    e.InnerText = tre.FullName;

                    if(tre.ArchiveFile != String.Empty)
                    {
                        XmlAttribute attrib = doc.CreateAttribute("archive");
                        attrib.Value = tre.ArchiveFile;
                        e.Attributes.Append(attrib);
                    }

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
}
