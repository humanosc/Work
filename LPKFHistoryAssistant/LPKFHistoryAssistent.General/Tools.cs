using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using XLib.General;

namespace LPKFHistoryAssistent.General
{
    public static class Tools
    {
        public static string GetPathArg ( string[] args )
        {
            var arg = CommandLineParser.GetParameters( args, "path" );
            return arg.Length == 0 ? null : arg[0];
        }

        public static string BuildSettingsPath ( string appDir )
        {
            return appDir + "\\lpkfha_settings.xml";
        }

        public static string BuildClientMapSettingsPath ( string appDir )
        {
            return appDir + "\\p4_client_map.xml";
        }

        public static string BuildInstanceKey ( string path )
        {
            return "lpkfha_" + Uri.EscapeDataString( Path.GetFullPath( path ).ToLower() );
        }

        public static string BuildInstanceSyncKey ( string path )
        {
            return BuildInstanceKey( path ) + "_sync";
        }

        public static string FindHistoryPath ( string path )
        {
            string parentDirectory = path;
            string historyPath = null;

            while ( ( parentDirectory = Path.GetDirectoryName( parentDirectory ) ) != null )
            {
                string currentPath = Path.Combine( parentDirectory, "# History.txt" );
                if ( File.Exists( currentPath ) )
                {
                    historyPath = currentPath;
                    break;
                }
            }

            return historyPath;
        }
    }
}
