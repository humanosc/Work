using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Service
{
    public interface IPerformanceCounterInstanceInfoService
    {
        string[] GetProcessCategoryInstanceNames ( string processName );
        string[] GetClrMemoryCategoryInstanceNames ( string processName );
    }
}
