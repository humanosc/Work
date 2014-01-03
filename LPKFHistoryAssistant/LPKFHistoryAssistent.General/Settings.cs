using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XLib.Data.Settings;

namespace LPKFHistoryAssistent.General
{
    public class Settings : SettingsBase<Settings>
    {
        private const string CURRENT_VERSION = "1.0";
        public SettingsParameter<string> Version = new SettingsParameter<string>( "Version" );
        public SettingsParameter<string> Nickname = new SettingsParameter<string>( "Nick" );
        public SettingsParameter<string> RssUrl = new SettingsParameter<string>( "RssUrl" );
        public SettingsParameter<bool> UsePerforceIntegration = new SettingsParameter<bool>( "UsePerforceIntegration" );
     
        public Settings ()
        {
            Version.Value = CURRENT_VERSION;

            ParameterRegister.Register( Version );
            ParameterRegister.Register( Nickname );
            ParameterRegister.Register( RssUrl );
            ParameterRegister.Register( UsePerforceIntegration );
        }
    }
}
