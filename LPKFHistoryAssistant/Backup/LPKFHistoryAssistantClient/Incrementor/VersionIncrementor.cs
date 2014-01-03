using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LPKFHistoryAssistantClient
{
    public class VersionIncrementor
    {
        private IVersionIncrementorStrategy _strategy;
        private string _path;

        public string FileExtension 
        { 
            get { return _strategy.FileExtension; } 
        }

        public VersionIncrementor ( string path, IVersionIncrementorStrategy strategy )
        {
            _path = path;
            _strategy = strategy;
        }

        public VersionIncremention Increment ()
        {
            return _strategy.Increment( _path );
        }
    }
}
