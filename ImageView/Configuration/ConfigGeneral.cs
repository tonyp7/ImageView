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
