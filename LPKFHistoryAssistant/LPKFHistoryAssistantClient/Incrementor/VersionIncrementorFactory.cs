using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using XLib.General;

namespace LPKFHistoryAssistantClient
{
    public class VersionIncrementorFactory : Singleton<VersionIncrementorFactory>
    {
        private List<IVersionIncrementorStrategy> _incrementorStrategies = new List<IVersionIncrementorStrategy>();

        public VersionIncrementorFactory ()
        {
            _incrementorStrategies.Add( new CsVersionIncrementorStrategy() );
            _incrementorStrategies.Add( new CppVersionIncrementorStrategy() );
            _incrementorStrategies.Add( new RcVersionIncrementorStrategy() );
        }

        public VersionIncrementor Create ( string path )
        {
            string extension = Path.GetExtension( path ).TrimStart('.');
            var incrementorStrategy = _incrementorStrategies.Find( ( item ) => item.FileExtension == extension );
            return incrementorStrategy != null ? new VersionIncrementor( path, incrementorStrategy ) : null; 
        }
    }
}
