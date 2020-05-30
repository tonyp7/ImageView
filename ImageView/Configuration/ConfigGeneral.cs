using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImageView.Configuration
{
    public class ConfigGeneral : IConfigurationItem, IEquatable<ConfigGeneral>
    {


        public ResourceManager Language { get { return language; } }

        public bool FirstLaunch { get { return firstLaunch; } }


        public static string[] SupportedLanguages = { "en", "fr" };


        private bool firstLaunch = true;
        private ResourceManager language;
        private CultureInfo culture;

        public CultureInfo Culture
        {
            get { return culture; }
        }
        public void SetCulture(string culture)
        {
            this.culture = CultureInfo.CreateSpecificCulture(culture);
        }

        public ConfigGeneral()
        {
            language = new ResourceManager("ImageView.Resources.strings", typeof(Program).Assembly);
        }

        public string GetString(string value)
        {
            return language.GetString(value, culture);
        }

        public object Clone()
        {
            ConfigGeneral cg = new ConfigGeneral();
            cg.SetCulture(this.Culture.TwoLetterISOLanguageName);
            return cg;
        }

        public bool Equals(ConfigGeneral other)
        {
            return this.Culture.TwoLetterISOLanguageName.Equals(other.Culture.TwoLetterISOLanguageName);
        }

        public void Load(XmlDocument doc)
        {
            XmlNode n;

            //language
            n = doc.SelectSingleNode("/Settings/General/Language");
            if (n != null && SupportedLanguages.Contains(n.InnerText))
            {
                firstLaunch = false;
                SetCulture(n.InnerText);
            }
            else
            {
                SetCulture(SupportedLanguages[0]);
            }
        }

        public void Save(XmlDocument doc)
        {
            Config.SafeNodeSelect(doc, "/Settings/General/Language", culture.TwoLetterISOLanguageName);
        }
    }
}
