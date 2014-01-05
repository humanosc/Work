using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Model
{
    public interface IPerformanceCounterInstanceModel
    {
        event EventHandler EvChanged;

        string[] Instances { get; set; }
        int SelectedInstanceIndex { get; set; }
    }
}
