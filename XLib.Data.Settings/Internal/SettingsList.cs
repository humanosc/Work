using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace XLib.Data.Settings.Internal
{
    [Serializable]
    [XmlRoot( ElementName = "Settings" )]
    public class SettingsList : List<Parameter>
    {
        public static SettingsList Create ( SettingsDictionary settingsDictionary )
        {
            SettingsList list = new SettingsList();

            foreach ( var item in settingsDictionary.Values )
            {
                list.Add( item );
            }

            return list;
        }

        public SettingsDictionary ConvertToDictionary ()
        {
            return SettingsDictionary.Create( this );
        }
    }
}
