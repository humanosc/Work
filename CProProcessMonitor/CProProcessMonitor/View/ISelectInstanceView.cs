using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CProProcessMonitor.View
{
    public interface ISelectInstanceView : IView 
    {
        event EventHandler EvOk;
        event EventHandler EvCancel;

        SynchronizationContext Context { get; }
        string[] Instances { set; }
        int SelectedInstanceIndex { get; }
    }
}
