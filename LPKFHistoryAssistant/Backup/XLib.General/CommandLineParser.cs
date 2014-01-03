using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XLib.General
{
    public static class CommandLineParser
    {
        public static string[] GetCommandLineArgs ( string commandLine )
        {
            int startIndex = 0;
            int endIndex = 0;
            List<string> commandLineArgs = new List<string>();
            while ( startIndex < commandLine.Length )
            {
                ///////////////////////////////////////////////////////////////////////////////////////////////////
                // Some Value Parsing here               
                bool hasFoundStartQoute = false;
                bool hasFoundChar = false;
                for ( endIndex = startIndex; endIndex < commandLine.Length; endIndex++ )
                {
                    char curChar = commandLine[endIndex];
                    if ( curChar == '"' )
                    {
                        if ( hasFoundStartQoute && hasFoundChar )
                        {
                            // found end qoute - exit value parsing
                            break;
                        }

                        hasFoundStartQoute = true;
                    }
                    else if ( curChar == ' ' && hasFoundChar && !hasFoundStartQoute )
                    {
                        // found another seperator char outside of a qoute
                        break;
                    }
                    else
                    {
                        hasFoundChar = true;
                    }
                }

                string commandLineArg = commandLine.Substring( startIndex, endIndex - startIndex ).Trim();
                commandLineArgs.Add( commandLineArg );

                startIndex = endIndex + 1;
            }

            return commandLineArgs.ToArray();
        }

        public static string[] GetParameters ( string[] args, string paramName)
        {
            string pattern = String.Format( "-{0}=", paramName );
            var paramteres = new List<string>();
            foreach ( var arg in args )
            {
                if ( arg.StartsWith( pattern ) )
                {                 
                    paramteres.Add( arg.Replace( pattern, String.Empty ).Trim('"') );
                }
            }
            return paramteres.ToArray();
        }
    }

}
