using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LPKFHistoryAssistantClient
{
    public class VersionIncremention
    {
        public readonly string OldVersion;
        public readonly string NewVersion;
        public readonly string AssemblyName;

        public VersionIncremention ( string oldVersion, string newVersion, string assemblyName )
        {
            OldVersion = oldVersion;
            NewVersion = newVersion;
            AssemblyName = assemblyName;
        }
    }
}
