using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace XLib.P4
{
    public class P4Exception : Exception 
    {
        public P4Exception ()
        {

        }
        public P4Exception (string message) : base(message)
        {

        }
    }

    public static class P4
    {
        private static string formatPath(string path)
        {           
            path = path.Replace("%", "%25");
            path = path.Replace("#", "%23");
            path = path.Replace("@", "%40");
            path = path.Replace( "*", "%2A" );
            return path;
        }

        private static void execute (string cmd, string path, out string stdOutput, out string errorOutput)
        {
            var process = new Process();
            process.StartInfo.FileName = "p4";
            process.StartInfo.Arguments = string.Format( "{0} \"{1}\"", cmd, formatPath( path ) );
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.EnableRaisingEvents = false;
            process.Start();
            process.WaitForExit();
            stdOutput = process.StandardOutput.ReadToEnd();
            errorOutput = process.StandardError.ReadToEnd();
        }

        private static string executeAndValidateOutput ( string cmd, string path, Func<string, string, bool> validationHandler )
        {
            string stdOut, stdError;
            execute( cmd, path, out stdOut, out stdError );
            if ( validationHandler == null )
            {
                if ( !string.IsNullOrEmpty( stdError ) )
                {
                    throw new P4Exception( stdError );
                }
            }
            else
            {
                if ( !validationHandler( stdOut, stdError ) )
                {
                    throw new P4Exception( stdError );
                }
            }
            return stdOut;
        }

        public static void Edit(string path)
        {
            executeAndValidateOutput( "edit", path, null );
        }

        public static void Edit ( string path, string client )
        {
            executeAndValidateOutput( string.Format( "-c {0}", client ) + " edit", path, null );
        }

        public static void Sync ( string path )
        {
            Func<string, string, bool> syncValidationHandler = (stdOut, stdError) => !stdError.ToLower().Contains( "no such file" );
            executeAndValidateOutput( "sync", path, syncValidationHandler);
        }

        public static void Sync ( string path, string client )
        {
            Func<string, string, bool> syncValidationHandler = ( stdOut, stdError ) => !stdError.ToLower().Contains( "no such file" );
            executeAndValidateOutput( string.Format( "-c {0}", client ) + " sync", path, syncValidationHandler );
        }

        public static void Revert ( string path )
        {
            executeAndValidateOutput( "revert", path, null );
        }

        public static void Revert ( string path, string client )
        {
            executeAndValidateOutput( string.Format("-c {0}", client) + " revert", path, null );
        }

        public static string Add ( string path )
        {
            return executeAndValidateOutput( "add", path, null);
        }

        public static string Add ( string path, string client )
        {
            return executeAndValidateOutput( string.Format( "-c {0}", client ) + " add", path, null );
        }

        public static string Delete ( string path )
        {
            return executeAndValidateOutput( "delete", path, null );
        }

        public static string Delete ( string path, string client )
        {
            return executeAndValidateOutput( string.Format( "-c {0}", client ) + " delete", path, null );
        }
    }
}
