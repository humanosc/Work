using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LPKFHistoryAssistantClient
{
    public class CsVersionIncrementorStrategy : VersionIncrementorStrategyBase 
    {    
        public CsVersionIncrementorStrategy ()
            : base( "cs" )
        {
        }

        public override VersionIncremention Increment ( string path )
        {
            const string versionPattern = @"(\d{2})[.](\d{3})[.](\d{5})";
            const string assemblyPattern = "AssemblyTitle[\\(\"](.*)[\"\\)]";
 
            ////////////////////////////////////////////////////////
            // read assembly text
            string text = readText( path );
            
            ////////////////////////////////////////////////////////
            // match version 
            var result = Regex.Match( text, versionPattern );
            if ( !result.Success || result.Groups.Count < 4)
            {
                return null;
            }            
              
            ////////////////////////////////////////////////////////
            // build new version string
            string oldVersion = result.Value;
            string newVersion = null;
            for ( int i = 1; i < result.Groups.Count - 1; i++ )
            {
                newVersion += result.Groups[i].Value + ".";
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
            // write new assembly text
            string newText = text.Replace( oldVersion, newVersion );
            writeText( path, newText );

            return new VersionIncremention( oldVersion, newVersion, assemblyName );
        }
    }
}
