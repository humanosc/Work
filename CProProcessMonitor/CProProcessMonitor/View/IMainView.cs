using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.View
{
    public interface IMainView : IView 
    {
        string StateText { set; }
        string Title { set; }
        string[] UpdateIntervals { set; }
        int SelectedUpdateIntervalIndex { get; set; }
        int Top { set; get; }
        int Left { set; get; }
        double CPU { set; }
        double Memory { set; }
        double ClrMemory { set; }

        void Reset();
    }
}
