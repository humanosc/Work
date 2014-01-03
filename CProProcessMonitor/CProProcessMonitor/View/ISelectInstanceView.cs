using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.View
{
    public interface ISelectInstanceView : IView 
    {
        event EventHandler EvOk;
        event EventHandler EvCancel;
        
        string[] Instances { set; }
        int SelectedInstanceIndex { get; }
    }
}
