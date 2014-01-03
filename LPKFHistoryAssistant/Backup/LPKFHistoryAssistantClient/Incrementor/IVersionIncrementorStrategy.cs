using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LPKFHistoryAssistantClient
{
    public interface IVersionIncrementorStrategy
    {
        string FileExtension { get; }

        VersionIncremention Increment ( string path );
    }
}
