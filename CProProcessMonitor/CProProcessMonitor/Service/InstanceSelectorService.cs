using CProProcessMonitor.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CProProcessMonitor.View;

namespace CProProcessMonitor.Service
{
    public class InstanceSelectorService : IInstanceSelectorService
    {
        private SelectInstancePresenter _selectInstancePresenter;

        public InstanceSelectorService( SelectInstancePresenter selectInstancePresenter )
        {
            _selectInstancePresenter = selectInstancePresenter;
        }

        public int SelectInstance(string category, string[] instances)
        {
            _selectInstancePresenter.ViewContext.Send( () =>
                {
                    _selectInstancePresenter.Model.Category = category;
                    _selectInstancePresenter.Model.Instances = instances;
                    _selectInstancePresenter.ShowDialogView();
                } );
            return _selectInstancePresenter.Model.SelectedInstanceIndex;
        }
    }
}
