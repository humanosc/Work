using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XLib.General;

namespace CProProcessMonitor.Model
{
    public class PerformanceCounterInstanceModel : IPerformanceCounterInstanceModel 
    {
        public event EventHandler EvChanged;

        private string[] _instances;
        public string[] Instances
        {
            get { return _instances; }
            set 
            { 
                _instances = value;
                EvChanged.RaiseIfValid(this);
            }
        }

        public int SelectedInstanceIndex
        {
            get;
            set;
        }

        public PerformanceCounterInstanceModel()
        {
        }

    }
}
