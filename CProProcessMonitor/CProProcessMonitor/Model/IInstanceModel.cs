using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Model
{
    public interface IInstanceModel
    {
        event EventHandler EvChanged;

        string Category { get; set; }
        string[] Instances { get; set; }
        int SelectedInstanceIndex { get; set; }
    }
}
