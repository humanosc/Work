using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LPKFHistoryAssistantClient
{
    public abstract class VersionIncrementorStrategyBase : IVersionIncrementorStrategy 
    {
        private readonly string _extension;

        protected VersionIncrementorStrategyBase ( string extension )
        {
            _extension = extension;
        }

        public string FileExtension
        {
            get { return _extension; }
        }

        // used for shims
        internal string ReadText ( string path )
        {
            return File.ReadAllText( path, Encoding.Default );
        }
        // used for shims
        internal void WriteText ( string path, string text )
        {
            File.WriteAllText( path, text, Encoding.Default );
        }

        protected string createNewBuildVersion ( string oldBuildVersion )
        {
            int oldBuildMinor = int.Parse( oldBuildVersion );
            int newBuildMinor = oldBuildMinor + 1;
            return newBuildMinor.ToString( "00000" );
        }

        public abstract VersionIncremention Increment ( string path );
    }
}
