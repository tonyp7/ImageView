using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImageView.Configuration
{
    public class ConfigSlideshow : IConfigurationItem, IEquatable<ConfigSlideshow>
    {
        public static readonly int MAXIMUM_SLIDESHOW_TIMER = 999999;
        public static readonly int DEFAULT_SLIDESHOW_TIMER = 5000;

        private static readonly ImageSizeMode DEFAULT_IMAGESIZE_MODE = ImageSizeMode.BestFit;

        public ImageSizeMode SizeMode;


        public int Timer { get; set; }


        public ConfigSlideshow()
        {
            Timer = DEFAULT_SLIDESHOW_TIMER;
            SizeMode = ConfigSlideshow.DEFAULT_IMAGESIZE_MODE;
        }

        public void Load(XmlDocument doc)
        {
            XmlNode n;
            ImageSizeMode sz;
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

            //size mode for slideshow
            n = doc.SelectSingleNode("/Settings/Slideshow/SizeMode");
            if (n != null && Enum.TryParse(n.InnerText, out sz))
            {
                SizeMode = sz;
            }
            else
            {
                SizeMode = ConfigSlideshow.DEFAULT_IMAGESIZE_MODE;
            }

        }

        public void Save(XmlDocument doc)
        {
            Config.SafeNodeSelect(doc, "/Settings/Slideshow/Timer", Timer.ToString());
            Config.SafeNodeSelect(doc, "/Settings/Slideshow/SizeMode", SizeMode.ToString());
        }

        public object Clone()
        {
            ConfigSlideshow c = new ConfigSlideshow();
            c.Timer = this.Timer;
            c.SizeMode = this.SizeMode;
            return c;
        }

        public bool Equals(ConfigSlideshow other)
        {
            throw new NotImplementedException();
        }
    }
}
