using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using XLib.P4;
using XLib.General;


namespace P4Assistent
{
    class Program
    {
        private static string GetCommandArg ( string[] args )
        {
            var tmp = CommandLineParser.GetParameters( args, "command" );
            return tmp.Length > 0 ? tmp[0] : null;
        }

        private static string GetPathArg ( string[] args )
        {
            var tmp = CommandLineParser.GetParameters( args, "path" );
            return tmp.Length > 0 ? tmp[0] : null;
        }

      

        static void Main ()
        {
            try
            {           
                var args = CommandLineParser.GetCommandLineArgs( Environment.CommandLine );
                //foreach ( var item in args )
                //{
                //    Console.WriteLine(item);
                //}

                string appPath = args[0].Trim('"');
                string appDir = Path.GetDirectoryName( appPath );
                P4Manager.Instance.Initialize( Path.Combine( appDir, "p4_client_map.xml" ) );


                if ( args.Length < 2 )
                {
                    throw new Exception( "Invalid commandline parameter" );
                }

                string command = GetCommandArg( args );
                string path = GetPathArg( args );

                if ( command == null || path == null )
                {
                    throw new Exception( "Invalid commandline parameter" );
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
               
                switch ( command.ToLower() )
                {
                    case "edit":
                        Console.WriteLine( string.Format( "edit: {0}", path ) );
                        P4Manager.Instance.Edit( path );
                        break;
                    case "sync":
                        Console.WriteLine( string.Format( "sync: {0}", path ) );
                        P4Manager.Instance.Sync( path );
                        break;
                    case "revert":
                        Console.WriteLine( string.Format( "revert: {0}", path ) );
                        P4Manager.Instance.Revert( path );
                        break;
                    case "add":
                        Console.WriteLine( string.Format( "add: {0}", path ) );
                        Console.WriteLine( P4Manager.Instance.Add( path ) );
                        break;
                    case "delete":
                        Console.WriteLine( string.Format( "delete: {0}", path ) );
                        Console.WriteLine( P4Manager.Instance.Delete( path ) );
                        break;

                    default: throw new Exception( "Invalid or not supported P4 command" );
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Operation successfully completed...");       
            }
            catch ( Exception ex )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine( ex.Message );
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.White;
                System.Diagnostics.Process pauseProc = System.Diagnostics.Process.Start( new System.Diagnostics.ProcessStartInfo() { FileName = "cmd", Arguments = "/C pause", UseShellExecute = false } );
                pauseProc.WaitForExit();
            }

        }
    }
}
