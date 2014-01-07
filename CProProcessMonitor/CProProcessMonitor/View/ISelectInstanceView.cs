using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CProProcessMonitor.View
{
    public interface ISelectInstanceView : IView 
    {
        string Category { set; }
        string[] Instances { set; }
        int SelectedInstanceIndex { get; }
    }
}
