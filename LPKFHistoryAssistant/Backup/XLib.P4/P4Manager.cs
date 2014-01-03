using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XLib.Data.Settings;
using XLib.General;

namespace XLib.P4
{
    public class P4Manager : Singleton<P4Manager> 
    {
        class P4Settings : SettingsBase<P4Settings>
        {
            public class Entry : ISettingsParameterGroup
            {
                public readonly SettingsParameter<string> Directory = new SettingsParameter<string>( "Directory" );
                public readonly SettingsParameter<string> Client = new SettingsParameter<string>( "Client" );

                public Entry ()
                {
                }

                public void ReadParameter ( ISettingsScope scope )
                {
                    scope.ReadParameter( Directory );
                    scope.ReadParameter( Client );
                }

                public void WriteParameter ( ISettingsScope scope )
                {
                    scope.WriteParameter( Directory );
                    scope.WriteParameter( Client );
                }
            }

            public readonly SettingsArrayParameter<Entry> ClientMap = new SettingsArrayParameter<Entry>( "ClientMap", () => new Entry() );

            public P4Settings ()
            {
                ParameterRegister.Register( ClientMap );
            }
        }
     
        private P4Settings.Entry[] _sortedEntries;

        public void Initialize ( string path )
        {
            Comparison<P4Settings.Entry> comparer = (a, b) => b.Directory.Value.CompareTo(a.Directory.Value);
            P4Settings.Instance.Load( path );
            if ( P4Settings.Instance.ClientMap.Value != null )
            {
                _sortedEntries = new P4Settings.Entry[P4Settings.Instance.ClientMap.Value.Length];
                Array.Copy( P4Settings.Instance.ClientMap.Value, _sortedEntries, _sortedEntries.Length );
                Array.Sort( _sortedEntries, comparer );
            }
        }

        private string findClient ( string path )
        {
            if ( _sortedEntries != null )
            {
                foreach ( var entry in _sortedEntries )
                {
                    if ( path.Contains( entry.Directory.Value ) )
                    {
                        return entry.Client.Value;
                    }
                }
            }
            return null;
        }

        public void Edit ( string path )
        {
            string client = findClient( path );
            if ( client == null )
            {
                P4.Edit( path );
            }
            else
            {
                P4.Edit( path, client );
            }
        }        
     
        public void Sync ( string path )
        {
            string client = findClient( path );
            if ( client == null )
            {
                P4.Sync( path );
            }
            else
            {
                P4.Sync( path, client );
            }
        }

        public void Revert ( string path )
        {
            string client = findClient( path );
            if ( client == null )
            {
                P4.Revert( path );
            }
            else
            {
                P4.Revert( path, client );
            }
        }
     
        public string Add ( string path )
        {
            string client = findClient( path );
            if ( client == null )
            {
                return P4.Add( path );
            }
            else
            {
                return P4.Add( path, client );
            }
        }

        public string Delete ( string path )
        {
            string client = findClient( path );
            if ( client == null )
            {
                return P4.Delete( path );
            }
            else
            {
                return P4.Delete( path, client );
            }
        }
    }
}
