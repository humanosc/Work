using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XLib.Data.Settings.Internal
{
    public class SettingsDictionary : Dictionary<string, Parameter>
    {
        public static SettingsDictionary Create ( SettingsList settingsList )
        {
            SettingsDictionary dictionary = new SettingsDictionary();

            foreach ( var item in settingsList )
            {
                dictionary.Add( item.Id, item );
            }

            return dictionary;
        }

        public SettingsList ConvertToList ()
        {
            return SettingsList.Create( this );
        }
    }
}
