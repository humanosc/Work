using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XLib.General;
using CProProcessMonitor.View;
using CProProcessMonitor.Model;
using System.Threading;

namespace CProProcessMonitor.Presenter
{
    public class SelectInstancePresenter : GenericPresenterBase<ISelectInstanceView>
    {
        private readonly IPerformanceCounterInstanceModel _model;

        public IPerformanceCounterInstanceModel Model
        {
            get { return _model; }
        }

        public SelectInstancePresenter ( ISelectInstanceView view, IPerformanceCounterInstanceModel model ) : base( view )
        {
            _model = model;
            _model.EvChanged += _model_EvChanged;
        }

        public virtual void OnCancel()
        {
            _model.SelectedInstanceIndex = -1;
            View.Close();
        }

        public virtual void OnOk()
        {
            _model.SelectedInstanceIndex = View.SelectedInstanceIndex;
            View.Close();
        }

        private void _model_EvChanged(object sender, EventArgs e)
        {
            View.Instances = _model.Instances;
        }
    }
}
