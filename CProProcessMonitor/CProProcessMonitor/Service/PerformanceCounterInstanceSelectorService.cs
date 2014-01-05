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
            ManualResetEvent ev = new ManualResetEvent(false);

            _selectInstancePresenter.Model.Instances = instances;
            _selectInstancePresenter.ShowView();
            _selectInstancePresenter.ViewClosed += (o, e) => ev.Set();            
            ev.WaitOne();

            return _selectInstancePresenter.Model.SelectedInstanceIndex;
        }
    }
}
