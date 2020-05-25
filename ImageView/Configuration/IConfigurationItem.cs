using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ImageView.Configuration
{
    interface IConfigurationItem : ICloneable
    {
        void Load(XmlDocument doc);
        void Save(XmlDocument doc);
    }
}
