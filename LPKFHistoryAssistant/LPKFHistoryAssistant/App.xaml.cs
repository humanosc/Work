//#define SHOW_ARGUMENTS

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Threading;
using System.IO;
using XLib.General;
using LPKFHistoryAssistent.General;



namespace LPKFHistoryAssistant
{
    public enum InputDataType
    {
        Ids,
        Entry
    }

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Private Member Variables
        private Mutex _mutex;
        #endregion

        #region Properties
        
        public InputDataType InputDataType
        {
            get;
            set;
        }

        public string HistoryEntryTitle 
        { 
            get; 
            set; 
        }
        public string BugtrackEntryTitle 
        { 
            get; 
            set; 
        }
        public string[] BugtrackIds 
        { 
            get; 
            set; 
        }
        public string HistoryPath
        {
            get; 
            private set;
        }
        public string PersonalHistoryPath
        {
            private set;
            get;
        }
        public string PersonalHistoryGroupedByTitlePath
        {
            private set;
            get;
        }
        public string SettingsPath
        {
            private set;
            get;
        }
        public string ClientMapSettingsPath
        {
            private set;
            get;
        }
        public string InstanceKey
        {
            private set;
            get;
        }
        public EventWaitHandle SyncEvent
        {
            private set;
            get;
        }
        #endregion

        #region Intialization / Deinitialization
        private void initialize ()
        {
            var args = CommandLineParser.GetCommandLineArgs( Environment.CommandLine );

    #if SHOW_ARGUMENTS
            foreach ( var item in args )
            {
                MessageBox.Show( item );
            }
    #endif
            string appPath = args[0].Trim( '"' );
            string appDir = Path.GetDirectoryName( appPath );
            SettingsPath = Tools.BuildSettingsPath( appDir );
            ClientMapSettingsPath = Tools.BuildClientMapSettingsPath( appDir );
            PersonalHistoryPath = appDir + "\\Personal History.txt";
            PersonalHistoryGroupedByTitlePath = appDir + "\\Personal History (grouped by title).txt";

            Settings.Instance.Load( SettingsPath );

            string path = Tools.GetPathArg( args );
            if ( !File.Exists( path ) )
            {
                throw new Exception( string.Format( "Invalid path argument ({0}).", path ) );
            }
            
    #if SHOW_ARGUMENTS
            MessageBox.Show( path );
    #endif
            string historyPath = Tools.FindHistoryPath( path );
            if ( historyPath == null )
            {
                throw new Exception( "Can't find history." );
            }

    #if SHOW_ARGUMENTS
             MessageBox.Show( historyPath );
    #endif

            string mutexName = Tools.BuildInstanceKey( historyPath );

            bool createdNew;
            Mutex mutex = new Mutex( true, mutexName, out createdNew );
            if ( !createdNew )
            {
                Application.Current.Shutdown();
                return;
            }

            string syncEventName = Tools.BuildInstanceSyncKey( historyPath );
            EventWaitHandle syncEvent = new EventWaitHandle( false, EventResetMode.ManualReset, syncEventName );

            _mutex = mutex;
            SyncEvent = syncEvent;
            InstanceKey = mutexName;
            HistoryPath = historyPath;
        }

        private void deinitialize ()
        {
            if ( _mutex != null )
            {
                _mutex.Close();
            }

            if ( SyncEvent != null )
            {
                SyncEvent.Reset();
                SyncEvent.Close();
            }
        }

        private void Application_Exit ( object sender, ExitEventArgs e )
        {
            try
            {
                deinitialize();
            }
            catch
            {
            }
        }

        private void Application_LoadCompleted ( object sender, System.Windows.Navigation.NavigationEventArgs e )
        {

        }

        #endregion

        public void ShowErrorMessage ( string message )
        {
            MessageBox.Show( message, "Error...", MessageBoxButton.OK, MessageBoxImage.Error );
        }

        public void HandleException ( Exception ex )
        {
            ShowErrorMessage( ex.Message );
            Dispatcher.Invoke( new Action( () => Application.Current.Shutdown() ) );    
        }

        private void Application_Startup ( object sender, StartupEventArgs e )
        {
            try
            {
                initialize();
            }
            catch ( Exception ex )
            {
                HandleException( ex );
            }
        }

        public static App Instance
        {
            get { return Application.Current as App; }
        }
    }
}
