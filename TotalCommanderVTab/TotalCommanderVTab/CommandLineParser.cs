using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XLib.General;

namespace TotalCommanderVTab
{
    internal static class InternalCommandLineParser 
    {
        public static string GetPathArg ( string[] args )
        {
            var arg = CommandLineParser.GetParameters( args, "path" );
            return arg.Length == 0 ? null : arg[0];
        }

        public static string[] GetRegexArgs ( string[] args )
        {
            return CommandLineParser.GetParameters( args, "regex" );
        }

        public static bool GetDebugArg ( string[] args )
        {
            return CommandLineParser.GetParameters( args, "debug" ).Length > 0;
        }

        public static bool GetOpenFileArg ( string[] args )
        {
            return CommandLineParser.GetParameters( args, "openfile" ).Length > 0;
        }
    }
}
