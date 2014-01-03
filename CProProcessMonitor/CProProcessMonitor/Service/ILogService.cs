using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Service
{
    public interface ILogService
    {
        bool IsInitialized { get; }

        void Initialize ( string path );
        void Deinitialize ();
        void Clear ();

        void Log ( float cpu, float memory, float clrMemory );
    }
}
