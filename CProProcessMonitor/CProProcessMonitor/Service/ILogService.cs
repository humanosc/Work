using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Service
{
    public interface ILogService
    {
        bool IsInitialized { get; }

        void Initialize ( string rootDir );
        void Deinitialize ();
        void ClearLog ();
        void OpenLog ();
        void CleanupLogFolder();
        void OpenLogFolder();

        void Log ( float cpu, float memory, float clrMemory );
    }
}
