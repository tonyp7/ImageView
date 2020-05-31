using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImageView.Configuration
{
    public class ConfigDisplay : IConfigurationItem, IEquatable<ConfigDisplay>
    {


        public double Zoom { get; set; }
        public ImageSizeMode SizeMode { get; set; }
        public double ZoomStep { get; set; }
        public bool AutoRotate { get; set; }
        public bool CheckeredPatternBackground { get; set; }


        public ImageSizeMode SizeModeOnImageLoad { get; set; }

        private static readonly double DEFAULT_ZOOM = 1.0;
        private static readonly double DEFAULT_ZOOM_STEP = 0.25;
        public static readonly double MAX_ZOOM = 4.0;
        private static readonly bool DEFAULT_AUTO_ROTATE = true;
        private static readonly bool DEFAULT_CHECKERED_PATTERN_BACKGROUND = true;
        private static readonly ImageSizeMode DEFAULT_IMAGESIZEMODE = ImageSizeMode.BestFit;

        public ConfigDisplay()
        {
            SizeMode = DEFAULT_IMAGESIZEMODE;
            Zoom = DEFAULT_ZOOM;
            ZoomStep = DEFAULT_ZOOM_STEP;
            AutoRotate = DEFAULT_AUTO_ROTATE;
            CheckeredPatternBackground = DEFAULT_CHECKERED_PATTERN_BACKGROUND;
        }

        public void Load(XmlDocument doc)
        {
            XmlNode n;
            bool bvalue;
            //int ivalue;
            double dvalue;


            //Auto rotate
            n = doc.SelectSingleNode("/Settings/Display/AutoRotate");
            if (n != null && bool.TryParse(n.InnerText, out bvalue))
            {
                AutoRotate = bvalue;
            }
            else
            {
                AutoRotate = DEFAULT_AUTO_ROTATE;
            }

            //CheckeredPatternBackground
            n = doc.SelectSingleNode("/Settings/Display/CheckeredPatternBackground");
            if (n != null && bool.TryParse(n.InnerText, out bvalue))
            {
                CheckeredPatternBackground = bvalue;
            }
            else
            {
                CheckeredPatternBackground = DEFAULT_CHECKERED_PATTERN_BACKGROUND;
            }

            //zoom
            n = doc.SelectSingleNode("/Settings/Display/Zoom");
            if (n != null && double.TryParse(n.InnerText, out dvalue) && dvalue > 0.0)
            {
                Zoom = dvalue;
            }
            else
            {
                Zoom = DEFAULT_ZOOM;
            }


            //zoom step
            n = doc.SelectSingleNode("/Settings/Display/ZoomStep");
            if (n != null && double.TryParse(n.InnerText, out dvalue) && dvalue > 0.0)
            {
                ZoomStep = dvalue;
            }
            else
            {
                ZoomStep = DEFAULT_ZOOM_STEP;
            }


            //image size mode
            ImageSizeMode sz;
            n = doc.SelectSingleNode("/Settings/Display/SizeMode");
            if (n != null && Enum.TryParse(n.InnerText, out sz))
            {
                SizeMode = sz;
            }
            else
            {
                SizeMode = DEFAULT_IMAGESIZEMODE;
            }

            //SizeModeOnNextImage
            n = doc.SelectSingleNode("/Settings/Display/SizeModeOnImageLoad");
            if (n != null && Enum.TryParse(n.InnerText, out sz))
            {
                SizeModeOnImageLoad = sz;
            }
            else
            {
                SizeModeOnImageLoad = DEFAULT_IMAGESIZEMODE;
            }
        }

        public void Save(XmlDocument doc)
        {
            Config.SafeNodeSelect(doc, "/Settings/Display/AutoRotate", AutoRotate.ToString());
            Config.SafeNodeSelect(doc, "/Settings/Display/CheckeredPatternBackground", CheckeredPatternBackground.ToString());
            Config.SafeNodeSelect(doc, "/Settings/Display/Zoom", Zoom.ToString());
            Config.SafeNodeSelect(doc, "/Settings/Display/ZoomStep", SizeMode.ToString());
            Config.SafeNodeSelect(doc, "/Settings/Display/SizeMode", SizeMode.ToString());
            Config.SafeNodeSelect(doc, "/Settings/Display/SizeModeOnImageLoad", SizeModeOnImageLoad.ToString());

        }

        public object Clone()
        {
            ConfigDisplay cd = new ConfigDisplay();

            cd.Zoom = this.Zoom;
            cd.SizeMode = this.SizeMode;
            cd.ZoomStep = this.ZoomStep;
            cd.SizeModeOnImageLoad = this.SizeModeOnImageLoad;
            cd.AutoRotate = this.AutoRotate;
            cd.CheckeredPatternBackground = this.CheckeredPatternBackground;

            return cd;
        }

        public bool Equals(ConfigDisplay other)
        {
            return Zoom == other.Zoom
                    && SizeMode == other.SizeMode
                    && ZoomStep == other.ZoomStep
                    && SizeModeOnImageLoad == other.SizeModeOnImageLoad
                    && AutoRotate == other.AutoRotate
                    && CheckeredPatternBackground == other.CheckeredPatternBackground
                    ;
        }


    }
}
