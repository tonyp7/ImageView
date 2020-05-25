using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImageView.Configuration
{
    public class ConfigReader : IConfigurationItem, IEquatable<object>
    {

        private static readonly ImageSizeMode DEFAULT_IMAGESIZE_MODE = ImageSizeMode.FitToWidth;

        public ImageSizeMode SizeMode;

        public ConfigReader()
        {
            SizeMode = DEFAULT_IMAGESIZE_MODE;
        }

        public object Clone()
        {
            ConfigReader c = new ConfigReader();
            c.SizeMode = this.SizeMode;
            return c;
        }

        public void Load(XmlDocument doc)
        {
            XmlNode n;
            ImageSizeMode sz;

            n = doc.SelectSingleNode("/Settings/Reader/SizeMode");
            if (n != null && Enum.TryParse(n.InnerText, out sz))
            {
                SizeMode = sz;
            }
            else
            {
                SizeMode = DEFAULT_IMAGESIZE_MODE;
            }
        }

        public void Save(XmlDocument doc)
        {
            Config.SafeNodeSelect(doc, "/Settings/Reader/SizeMode", SizeMode.ToString());
        }
    }
}
