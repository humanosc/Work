using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CProProcessMonitor.Model;
using XLib.Data.Settings;

namespace CProProcessMonitor.Service
{
    public class MainModelSerializerService : IMainModelSerializerService 
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
        
        public void Serialize ( string path, IMainModel model )
        {
            Settings.Instance.ProcessName.Value = model.ProcessName;
            Settings.Instance.WindowTop.Value = model.WindowTop;
            Settings.Instance.WindowLeft.Value = model.WindowLeft;
            Settings.Instance.TimerResolutionId.Value = model.TimerResolutionId;
            Settings.Instance.DiagramWidth.Value = model.DiagramWidth;
            Settings.Instance.DiagramHeight.Value = model.DiagramHeight;
            Settings.Instance.Save( path );
        }

        public void Deserialize ( string path, IMainModel model )
        {
            Settings.Instance.Load( path );
            model.ProcessName = Settings.Instance.ProcessName.Value;
            model.WindowTop = Settings.Instance.WindowTop.Value;
            model.WindowLeft = Settings.Instance.WindowLeft.Value;
            model.TimerResolutionId = Settings.Instance.TimerResolutionId.Value;
            model.DiagramWidth = Settings.Instance.DiagramWidth.Value;
            model.DiagramHeight = Settings.Instance.DiagramHeight.Value;
        }        
    }
}
