using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XLib.P4;
using LPKFHistoryAssistent.General;
using System.Diagnostics;
using System.IO;
using System.Threading;
using XLib.General;

namespace LPKFHistoryAssistantClient
{
    class Program
    {
        private const string VERSION = "1.0";

        private static void writeCenteredLine ( string line, char borderChar )
        {
            Console.Write( borderChar );
            int centerLeft = (Console.BufferWidth - line.Length) / 2;
            Console.SetCursorPosition( centerLeft , Console.CursorTop );
            Console.Write( line );
            Console.SetCursorPosition( Console.BufferWidth - 1, Console.CursorTop );
            Console.Write( borderChar );
        }

        private static void writeBanner ()
        {
            char[] buffer = new char[Console.BufferWidth];
            for ( int i = 0; i < buffer.Length; i++ )
            {
                buffer[i] = '#';
            }

            Console.Write( buffer );
            writeCenteredLine( string.Empty, '#' );
            writeCenteredLine( string.Format( "LPKF History Assistent Client v{0}", VERSION ), '#' );
            writeCenteredLine( "Coded by Daniel Brosche", '#' );
            writeCenteredLine( string.Empty, '#' );
            Console.Write( buffer );
            Console.Write("\n\n\n");
        }

        private static void startLPKFHistoryAssistent ( string path )
        {
            // start history assistent server
            string arg = string.Format( "-path=\"{0}\"", path );
            // start history assistant server
            var process = new Process();
            process.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LPKF History Assistant.exe");
            process.StartInfo.Arguments = arg;
            process.StartInfo.UseShellExecute = true;
            process.Start();
            Console.WriteLine( "LPKF History Assistent Application started" );
        }

        private static void waitForSyncEvent ( string instanceSyncKey )
        {
            int dotCount = 0;
            int maxDotCount = 3;
            string suffix = null;
            char[] empyBuffer = new char[Console.BufferWidth];
            for ( int i = 0; i < empyBuffer.Length; i++ )
            {
                empyBuffer[i] = ' ';
            }
            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;
            
            while ( true )
            {
                try
                {
                    // try to open existing wait handle
                    EventWaitHandle syncEvent = EventWaitHandle.OpenExisting( instanceSyncKey, System.Security.AccessControl.EventWaitHandleRights.Synchronize );
                    if ( syncEvent.WaitOne( 1000 ) )
                    {
                        syncEvent.Close();
                        break;
                    }
                }
                catch
                {
                }

                dotCount++;
                suffix += '.';

                Console.SetCursorPosition( cursorLeft, cursorTop );
                Console.WriteLine( empyBuffer );
                Console.SetCursorPosition( cursorLeft, cursorTop );

                Console.WriteLine( "Waiting for LPKF History Assistent" + suffix );
                if ( dotCount >= maxDotCount )
                {
                    dotCount = 0;
                    suffix = null;
                }
                Thread.Sleep( 1000 );
            }
        }

        static void Main ()
        {            
            try
            {
                writeBanner();

                ///////////////////////////////////////////////////////////////////////////////////////////////////
                // check arguments
                var args = Environment.GetCommandLineArgs();
                if ( args.Length < 2 )
                {
                    throw new Exception( "Invalid path argument count." );
                }

                for ( int i = 1; i < args.Length; i++ )
                {
                    if ( !File.Exists( args[i] ) )
                    {
                        throw new Exception( string.Format("Invalid path argument ({0}).", args[i]) );
                    }
                }

                string appPath = args[0];
                string appDir = Path.GetDirectoryName( appPath );
                string settingsPath = Tools.BuildSettingsPath( appDir );

                Settings.Instance.Load( settingsPath );

            #if DISABLE_PERFORCE
                bool usePerfoceIntegration = false;
            #else
                bool usePerfoceIntegration = Settings.Instance.UsePerforceIntegration.Value;
            #endif

                for ( int i = 1; i < args.Length; i++ )
                {
                    string path = args[i];

                    Console.WriteLine( string.Format( "Opening File: {0}", path ) );

                    // create version incrementor
                    var versionIncrementor = VersionIncrementorFactory.Instance.Create( path );
                    if ( versionIncrementor == null )
                    {
                        throw new Exception( "File format isn't supported." );
                    }

                    // find history path
                    string historyPath = Tools.FindHistoryPath( path );
                    if ( historyPath == null )
                    {
                        throw new Exception( "Can't find History." );
                    }

                    // build instance key
                    string instanceKey = Tools.BuildInstanceKey( historyPath );

                    // build instance sync key
                    string instanceSyncKey = Tools.BuildInstanceSyncKey( historyPath );

                    // start history assistent
                    startLPKFHistoryAssistent( path );

                    // wait until server is ready
                    waitForSyncEvent( instanceSyncKey );

                    // create pipe client
                    var pipeClient = new PipeClient();

                    // connect to pipe server
                    pipeClient.Connect( instanceKey );

                    if ( usePerfoceIntegration )
                    {
                        P4Manager.Instance.Initialize( Tools.BuildClientMapSettingsPath( appDir ) );

                        // try to sync and checkout assembly info
                        P4Manager.Instance.Sync( path );
                        P4Manager.Instance.Edit( path );
                    }                    

                    // increment version number
                    VersionIncremention incremention = versionIncrementor.Increment();

                    string incrementionLog = string.Format( "{0} => {1}", incremention.AssemblyName, incremention.NewVersion );

                    // notify assistent only when file extentsion is not "rc" 
                    if ( versionIncrementor.FileExtension != "rc" )
                    {
                        // send incremention result
                        pipeClient.SendLine( incrementionLog );
                    }

                    // disconnect client
                    pipeClient.Disconnect();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine( "Incremention succeeded: " + incrementionLog );
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                }
                
                Thread.Sleep( 1000 );
            }
            catch ( Exception ex )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine( string.Format( "Error: {0}", ex.ToString() ) );
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();
                System.Diagnostics.Process pauseProc = System.Diagnostics.Process.Start( new System.Diagnostics.ProcessStartInfo() { FileName = "cmd", Arguments = "/C pause", UseShellExecute = false } );
                pauseProc.WaitForExit();
            }              
        }
    }
}
