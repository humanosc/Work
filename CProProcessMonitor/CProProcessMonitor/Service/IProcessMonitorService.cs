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

    public class ProcessMonitorEventArgs : EventArgs
    {
        public readonly string ProcessName;
        public readonly int ProcessId;

        public ProcessMonitorEventArgs ( string processName, int processId )
        {
            ProcessName = processName;
            ProcessId = processId;
        }
    }

    public interface IProcessMonitorService
    {
        event EventHandler<ProcessMonitorEventArgs> EvProcessCreated;
        event EventHandler<ProcessMonitorEventArgs> EvProcessExited;
        event EventHandler<NewProcessMonitorDataEventArgs> EvNewData;
        int Interval { get; set; }
        bool IsInitialized { get; } 
        void Initialize ();
        void Deinitialize ();
        void Start ();
        void Stop ();
    }
}
