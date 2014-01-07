using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Service
{
    public abstract class PerformanceCounterEventArgsBase : EventArgs
    {
        public readonly string Name;

        public PerformanceCounterEventArgsBase ( string performanceCounterName )
        {
            Name = performanceCounterName;
        }
    }

    public class PerformanceCounterCategoryNotSupportedEventArgs : PerformanceCounterEventArgsBase
    {
        public PerformanceCounterCategoryNotSupportedEventArgs ( string categoryName ) : base( categoryName ) { }
    }

    public class PerformanceCounterBrokenEventArgs : PerformanceCounterEventArgsBase
    {
        public PerformanceCounterBrokenEventArgs ( string performanceCounterName ) : base( performanceCounterName ) { }
    }

    public interface IPerformanceCounterService
    {
        event EventHandler<PerformanceCounterCategoryNotSupportedEventArgs> PerformanceCounterNotSupported;
        event EventHandler<PerformanceCounterBrokenEventArgs> PerformanceCounterBroken;
        
        bool IsInitialized { get; }

        float GetNextCpuValue ();        
        float GetNextPrivateBytesValue ();              
        float GetNextClrBytesValue ();
        
        void Initialize ( string processPerformanceCounterInstanceName, string clrMemoryPerformanceCounterInstanceName );
        void Deinitialize ();
    }
}
