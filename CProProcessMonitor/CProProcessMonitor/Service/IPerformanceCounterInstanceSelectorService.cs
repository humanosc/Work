using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Service
{
    public interface IPerformanceCounterInstanceSelectorService
    {
        int SelectInstance(string[] instances);
    }
}
