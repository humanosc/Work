using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace XLib.Data.Settings.Internal
{  
    internal sealed class SettingsManager : ISettingsManager
    {
        private SettingsScope _rootScope = new SettingsScope();

        private Dictionary<string, ISettingsParameter> _parameterDictionary = new Dictionary<string, ISettingsParameter>();

        public SettingsManager ()
        {
        }

        public void Load ( string path )
        {
            _rootScope.Reset();

            if ( !File.Exists( path ) )
            {
                return;
            }

            SettingsList tempList = null;
            using ( Stream stream = File.OpenRead( path ) )
            {
                XmlSerializer serializer = new XmlSerializer( typeof( SettingsList ) );
                tempList = serializer.Deserialize( stream ) as SettingsList;
            }

            _rootScope.Initialize( tempList.ConvertToDictionary() );

            foreach ( var parameter in _parameterDictionary.Values )
            {
                _rootScope.ReadParameter( parameter );
            }
        }

        public void Save ( string path )
        {
            _rootScope.Reset();

            foreach ( var parameter in _parameterDictionary.Values )
            {
                _rootScope.WriteParameter( parameter );
            }

            var tempList = _rootScope.ConvertToList();

            using ( Stream stream = File.Create( path ) )
            {
                XmlSerializer serializer = new XmlSerializer( typeof( SettingsList ) );
                serializer.Serialize( stream, tempList );
            }
        }

        public void Register ( ISettingsParameter parameter )
        {
            _parameterDictionary.Add( parameter.Id, parameter );
        }

        public void Unregister ( ISettingsParameter parameter )
        {
            _parameterDictionary.Remove( parameter.Id );
        }

        public void Reset ()
        {
            _parameterDictionary.Clear();
            _rootScope.Reset();
        }
    }
}
