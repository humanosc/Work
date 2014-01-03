using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Service
{
    public class NewProcessMonitorDataEventArgs : EventArgs  
    {
        public readonly float Cpu;
        public readonly float Memory;
        public readonly float ClrMemory;

        public NewProcessMonitorDataEventArgs ( float cpu, float memory, float clrMemory )
        {
            Cpu = cpu;
            Memory = memory;
            ClrMemory = clrMemory;
        }
    }

    public interface IProcessMonitorService
    {
        event EventHandler EvProcessExited;
        event EventHandler<NewProcessMonitorDataEventArgs> EvNewData;
        int Interval { get; set; }
        bool IsInitialized { get; } 
        void Initialize ();
        void Deinitialize ();
        void Start ();
        void Stop ();
    }
}
