//#define SHOW_HISTORY_ENTRY

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.IO;
using XLib.P4;
using LPKFHistoryAssistent.General;
using System.Collections.Specialized;

namespace LPKFHistoryAssistant
{
    /// <summary>
    /// Interaction logic for HistoryForm.xaml
    /// </summary>
    public partial class HistoryForm : Page
    {
        private PipeServer _pipeServer = new PipeServer();
        private BugtrackExtractor _bugTrackExtractor = new BugtrackExtractor();

        #region initialization
        private void initializeNamedPipe ()
        {
            _pipeServer.Start( App.Instance.InstanceKey );
            // set sync event
            App.Instance.SyncEvent.Set();
        }

        private void initialize ()
        {
            if ( App.Instance.InputDataType == InputDataType.Ids ) // check if a bugtrack id has been specified
            {
                // setup bugtrack extractor and extract bugtrack entry
                _bugTrackExtractor.Url = Settings.Instance.RssUrl.Value;

                Action action = () =>
                {
                    Thread.Sleep( 1000 );
                    _bugTrackExtractor.RefreshAsync();
                };

                action.BeginInvoke( null, null );
            }
            else // no bugtrack ids have benn specified - in this case we don't extract 
            {    // the title because the bugtrack title was entered by user
                try
                {
                    updateBugtrackTitle();

                    initializeNamedPipe();
                }
                catch ( Exception ex )
                {
                    App.Instance.HandleException( ex );
                }
            }
        }
        #endregion

        private void updateBugtrackTitle ()
        {
            if ( !string.IsNullOrEmpty( App.Instance.BugtrackEntryTitle ) )
            {
                string nick = Settings.Instance.Nickname.Value;
                string historyTitle = String.Format( "{0} ({1})", DateTime.Now.ToShortDateString(), nick);
                string[] lines = App.Instance.BugtrackEntryTitle.Split( new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries );

                foreach ( var line in lines )
                {
                    historyTitle += string.Format( "\r\n- {0}", line.Trim( ' ', '-' ) );
                }

                App.Instance.HistoryEntryTitle = historyTitle;
                Dispatcher.Invoke( new Action( () =>
                {
                    _txbHead.Text = historyTitle;
                    _btAddRecord.IsEnabled = true;
                } ) );
            }
            else
            {
                Dispatcher.Invoke( new Action( () =>
                {
                    _btAddRecord.IsEnabled = false;
                } ) );
            }
        }

        /// <summary>
        /// default constructor
        /// </summary>
        public HistoryForm ()
        {
            InitializeComponent();

            _bugTrackExtractor.RefreshCompleted += new EventHandler<RefreshCompletedEventArgs>( _bugTrackExtractor_RefreshCompleted );

            _pipeServer.MessageReceived += new EventHandler<PipeServerEventArgs>( _pipeServer_MessageReceived );

        }

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            _btAddRecord.IsEnabled = false;
            _btAddRecord.Focus();

            initialize();
        }

        void _bugTrackExtractor_RefreshCompleted ( object sender, RefreshCompletedEventArgs e )
        {
            try
            {
                if ( e.Error != null )
                {
                    throw e.Error;
                }

                foreach ( var bugtrackId in App.Instance.BugtrackIds )
                {
                    string btTitle = _bugTrackExtractor.ExtractTitle( bugtrackId );
                    if ( btTitle == null )
                    {
                        App.Instance.ShowErrorMessage( string.Format( "Can't find Bugtrack Record. Please check Bugtrack Id: {0}...", bugtrackId ) );
                        Dispatcher.Invoke( new Action( () => MainWindow.Instance.SwitchView( MainWindow.ViewType.BugTrackForm ) ) );
                        return;
                    }

                    // append bugtrack title
                    App.Instance.BugtrackEntryTitle += string.Format( "{0}\r\n", btTitle );    
                }
                
                updateBugtrackTitle();

                initializeNamedPipe();
            }
            catch ( Exception ex)
            {
                App.Instance.HandleException( ex ); 
            }
        }

        void _pipeServer_MessageReceived ( object sender, PipeServerEventArgs e )
        {
            if ( e.Error != null )
            {
                App.Instance.HandleException( e.Error );
            }
            else
            {
                Action action = new Action( () => _txbBody.Text += string.Format( "\t{0}\r\n", e.Message ) );
                Dispatcher.Invoke( action );
            }
        }

        #region File Helper
        private string readAllText ( string path )
        {
            string text = string.Empty;
            if ( File.Exists( path ) )
            {
                text = File.ReadAllText( path, Encoding.Default );
            }
            return text;
        }

        private void writeText ( string path, string text )
        {
            using ( StreamWriter stream = new StreamWriter( path, false, Encoding.Default ) )
            {
                stream.WriteLine( text );
            }
        }

        private void writeAllTextLines ( string path, params string[] textLines )
        {
            using ( StreamWriter stream = new StreamWriter( path, false, Encoding.Default ) )
            {
                foreach ( string textLine in textLines )
                {
                    stream.WriteLine( textLine );         
                }
            }
        }

        private void prependText ( string path, string header, string body )
        {
            string oldText = readAllText( path );
            // prepend new text
            writeAllTextLines( path, header, body, oldText );
        }

        private void syncFileOperation ( string path, Action action )
        {
            string syncMutexName = Tools.BuildInstanceSyncKey( path );
            Mutex syncMutex = new Mutex( false, syncMutexName );
            syncMutex.WaitOne();
            action();
            syncMutex.ReleaseMutex();
            syncMutex.Close();
        }

        private void prependTextSync ( string path, string header, string body )
        {
            syncFileOperation( path, () => prependText( path, header, body ) );
        }

        private void prependGroupedText ( string path, string header, string body )
        {
            string oldText = readAllText( path );
            string headerNL = header + "\r\n";
            int headerIndex = oldText.IndexOf( headerNL );
            if ( headerIndex == -1 )
            {
                writeAllTextLines( path, header, body, oldText );
            }
            else
            {
                int insertIndex = headerIndex + headerNL.Length;             
                string newText = oldText.Insert( insertIndex, body );
                writeText( path, newText );
            }
        }

        private void prependGroupedTextSync ( string path, string header, string body )
        {
            syncFileOperation( path, () => prependGroupedText( path, header, body ) );
        }
        #endregion

        

        private void _btAddRecord_Click ( object sender, RoutedEventArgs e )
        {
            if ( _txbBody.Text.Length == 0 )
            {
                App.Instance.ShowErrorMessage( "Empty history entries are prohibit..." );
                return;
            }

            _pipeServer.Stop();

            // get formated header
            string header = App.Instance.HistoryEntryTitle;
            // get formated body
            string body = _txbBody.Text.TrimEnd( '\r', '\n' ) + "\r\n";
           // string[] bodyLines = body.Split( new[] { "\r\n" } );
            

    #if SHOW_HISTORY_ENTRY
            MessageBox.Show( historyEntry );
    #endif
    
            try
            {
    #if DISABLE_PERFORCE
                bool usePerforceIntegration = false;
    #else
                bool usePerforceIntegration = Settings.Instance.UsePerforceIntegration.Value;
    #endif
                P4Manager.Instance.Initialize( App.Instance.ClientMapSettingsPath );

                if ( usePerforceIntegration )
                {
                    // try to sync and checkout history
                    P4Manager.Instance.Sync( App.Instance.HistoryPath );
                    P4Manager.Instance.Edit( App.Instance.HistoryPath );
                }

                // safe entry into specific history
                prependText( App.Instance.HistoryPath, header, body );
                // safe entry into personal history
                prependTextSync( App.Instance.PersonalHistoryPath, header, body );
                // safe entry into personal grouped history
                prependGroupedTextSync( App.Instance.PersonalHistoryGroupedByTitlePath, header, body );

                ClipboardEx.SetDataObjectSTA( App.Instance.BugtrackEntryTitle, 100, 50 );
                
                // shut down application
                Application.Current.Shutdown();
            }
            catch ( Exception ex )
            {
                App.Instance.HandleException( ex );
            }
        }

  
     
    }
}
