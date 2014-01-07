using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Service
{
    public interface IInstanceSelectorService
    {
        int SelectInstance(string category, string[] instances);
    }
}
