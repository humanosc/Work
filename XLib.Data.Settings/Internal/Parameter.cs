using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace XLib.Data.Settings.Internal
{
    [Serializable]
    public class Parameter
    {
        [XmlAttribute( AttributeName = "Id" )]
        public string Id;
        [XmlAttribute( AttributeName = "Type" )]
        public string Type;
        [XmlElement( ElementName = "Value" )]
        public string Value;
        public SettingsList Childs;

        public Parameter ()
        {
        }

        public Parameter ( string id, Type type )
        {
            this.Id = id;
            this.Type = type.ToString();
        }

        public Parameter ( string id, string value, Type type )
        {
            this.Id = id;
            this.Type = type.ToString();
            this.Value = value;
        }
    }
}
