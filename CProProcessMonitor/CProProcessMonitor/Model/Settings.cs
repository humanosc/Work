using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XLib.Data.Settings;

namespace CProProcessMonitor.Model
{
    public class Settings : SettingsBase<Settings>
    {
        private const string CURRENT_VERSION = "1.0";

        public SettingsParameter<string> Version = new SettingsParameter<string>( "Version" );
        public SettingsParameter<string> ProcessName = new SettingsParameter<string>( "ProcessName" );
        public SettingsParameter<int> WindowTop = new SettingsParameter<int>( "Top" );
        public SettingsParameter<int> WindowLeft = new SettingsParameter<int>( "Left" );
        public SettingsParameter<int> TimerResolutionId = new SettingsParameter<int>( "TimerResolutionId" );
        public SettingsParameter<int> DiagramWidth = new SettingsParameter<int>( "DiagramWidth" );
        public SettingsParameter<int> DiagramHeight = new SettingsParameter<int>( "DiagramHeight" );

        public Settings ()
        {
            Version.Value = CURRENT_VERSION;
#if DEBUG
                ProcessName.Value = "explorer"; 
#else
            ProcessName.Value = "CircuitPro";
#endif

            TimerResolutionId.Value = 0;
            DiagramWidth.Value = 1024;
            DiagramHeight.Value = 768;

            ParameterRegister.Register( Version );
            ParameterRegister.Register( ProcessName );
            ParameterRegister.Register( WindowTop );
            ParameterRegister.Register( WindowLeft );
            ParameterRegister.Register( TimerResolutionId );
            ParameterRegister.Register( DiagramWidth );
            ParameterRegister.Register( DiagramHeight );
        }
    }
}
