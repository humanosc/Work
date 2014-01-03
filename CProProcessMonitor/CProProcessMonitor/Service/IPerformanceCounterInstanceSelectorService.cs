using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Service
{
    public interface IPerformanceCounterInstanceSelectorService
    {
        string SelectInstance(string[] instances);
    }
}
