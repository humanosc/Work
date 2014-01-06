using CProProcessMonitor.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CProProcessMonitor.Service
{
    public class PerformanceCounterInstanceSelectorService : IPerformanceCounterInstanceSelectorService
    {
        private SelectInstancePresenter _selectInstancePresenter;

        public PerformanceCounterInstanceSelectorService( SelectInstancePresenter selectInstancePresenter )
        {
            _selectInstancePresenter = selectInstancePresenter;
        }

        public int SelectInstance(string[] instances)
        {    
            _selectInstancePresenter.Model.Instances = instances;
            _selectInstancePresenter.ShowDialogView();    
            return _selectInstancePresenter.Model.SelectedInstanceIndex;
        }
    }
}
