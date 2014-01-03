using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LPKFHistoryAssistantClient
{
    public class RcVersionIncrementorStrategy : VersionIncrementorStrategyBase 
    {
        public RcVersionIncrementorStrategy ()
            : base( "rc" )
        {
        }

        public override VersionIncremention Increment ( string path )
        {
            const string versionPattern = @"(\d{1,2})[,](\d{1,3})[,](\d{1,5})";
            const string assemblyPattern = "OriginalFilename\", \"(.*)\"";

            ////////////////////////////////////////////////////////
            // read assembly text
            string text = ReadText( path );

            ////////////////////////////////////////////////////////
            // match version 
            var result = Regex.Match( text, versionPattern );
            if ( !result.Success || result.Groups.Count < 4 )
            {
                return null;
            }

            ////////////////////////////////////////////////////////
            // build new version string
            string oldVersion = result.Value;
            string newVersion = null;
            for ( int i = 1; i < result.Groups.Count - 1; i++ )
            {
                newVersion += result.Groups[i].Value + ",";
            }

            // build new revision number
            newVersion += createNewBuildVersion( result.Groups[result.Groups.Count - 1].Value );

            ////////////////////////////////////////////////////////
            // match assemblyname
            result = Regex.Match( text, assemblyPattern );
            if ( !result.Success || result.Groups.Count < 2 )
            {
                return null;
            }
            string assemblyName = result.Groups[1].Value.Trim( ' ', '\"' );

            ////////////////////////////////////////////////////////
            // write new resource text

            // replace FILEVERSION & PRODUCTVERSION
            string newText = text.Replace( oldVersion, newVersion );

            // build version strings with whitespaces to replace "StringFileInfo Fields"
            string oldVersionWithWs = oldVersion.Replace( ",", ", " );
            string newVersionWithWs = newVersion.Replace( ",", ", " );
            newText = newText.Replace( oldVersionWithWs, newVersionWithWs );

            WriteText( path, newText );

            return new VersionIncremention( oldVersion, newVersion, assemblyName );
        }   
    }
}
