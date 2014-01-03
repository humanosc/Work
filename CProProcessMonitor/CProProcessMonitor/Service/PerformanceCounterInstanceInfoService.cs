using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Service
{
    public class PerformanceCounterInstanceInfoService : IPerformanceCounterInstanceInfoService 
    {
        public PerformanceCounterInstanceInfoService ()
        {
        }

        public string[] GetProcessCategoryInstanceNames ( string processName )
        {
            var category = new PerformanceCounterCategory( PerformanceCounterCategoryNames.Process );
            return category.GetInstanceNames().Where( s => s.StartsWith( processName ) ).ToArray();
        }

        public string[] GetClrMemoryCategoryInstanceNames ( string processName )
        {
            var category = new PerformanceCounterCategory( PerformanceCounterCategoryNames.ClrMemory );
            return category.GetInstanceNames().Where( s => s.StartsWith( processName ) ).ToArray();
        }
    }
}
