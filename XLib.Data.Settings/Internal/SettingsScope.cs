using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XLib.General;

namespace XLib.Data.Settings.Internal
{
    internal sealed class SettingsScope : ISettingsScope
    {
        private SettingsDictionary _settings = new SettingsDictionary();

        public SettingsScope ()
        {
        }

        public SettingsScope ( SettingsDictionary dictionary )
        {
            _settings = dictionary;
        }

        public void Initialize ( SettingsDictionary dictionary )
        {
            _settings = dictionary;
        }

        public SettingsList ConvertToList ()
        {
            return _settings.ConvertToList();
        }

        public int Count
        {
            get { return _settings.Count; }
        }

        public void WriteParameter ( ISettingsParameter parameter )
        {
            var rootItem = new Parameter( parameter.Id, parameter.ObjectType );

            if ( parameter is ISettingsParameterGroup )
            {
                SettingsScope innerScope = new SettingsScope();
                ( (ISettingsParameterGroup)parameter ).WriteParameter( innerScope );
                rootItem.Childs = innerScope.ConvertToList();
            }
            else if ( parameter.ObjectValue is ISettingsParameterGroup )
            {
                SettingsScope innerScope = new SettingsScope();
                ( (ISettingsParameterGroup)parameter.ObjectValue ).WriteParameter( innerScope );
                rootItem.Childs = innerScope.ConvertToList();
            }
            else
            {
                rootItem.Value = (string)TypeConverter.ConvertTo( parameter.ObjectValue, typeof( string ) );
            }

            _settings[parameter.Id] = rootItem;
        }

        public void ReadParameter ( ISettingsParameter parameter )
        {
            var item = this[parameter.Id];
            if ( item != null )
            {
                if ( ( parameter is ISettingsParameterGroup ) && ( item.Childs != null ) )
                {
                    SettingsScope innerScope = new SettingsScope( item.Childs.ConvertToDictionary() );
                    ( (ISettingsParameterGroup)parameter ).ReadParameter( innerScope );
                }
                else if ( ( parameter.ObjectValue is ISettingsParameterGroup ) && ( item.Childs != null ) )
                {
                    SettingsScope innerScope = new SettingsScope( item.Childs.ConvertToDictionary() );
                    ( (ISettingsParameterGroup)parameter.ObjectValue ).ReadParameter( innerScope );
                }
                else
                {
                    parameter.ObjectValue = TypeConverter.ConvertTo( item.Value, parameter.ObjectType );
                }
            }
        }

        public void Reset ()
        {
            _settings.Clear();
        }

        public Parameter this[string id]
        {
            get
            {
                Parameter parameter = null;
                _settings.TryGetValue( id, out parameter );
                return parameter;
            }
            set { _settings[id] = value; }
        }
    }
}
