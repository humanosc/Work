using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XLib.General;
using CProProcessMonitor.View;
using CProProcessMonitor.Model;

namespace CProProcessMonitor.Presenter
{
    public class SelectInstancePresenter
    {
        public event EventHandler ViewClosed;

        private ISelectInstanceView _view;
        private IPerformanceCounterInstanceModel _model;

        public IPerformanceCounterInstanceModel Model
        {
            get { return _model; }
        }

        public SelectInstancePresenter ( ISelectInstanceView view, IPerformanceCounterInstanceModel model )
        {
            _view = view;
            _model = model;
            _model.EvChanged += _model_EvChanged;
            _view.EvOk += _view_EvOk;
            _view.EvClose += _view_EvClose;
            _view.EvCancel += _view_EvCancel;
        }

        private void _view_EvCancel(object sender, EventArgs e)
        {
            _model.SelectedInstanceIndex = -1;
            _view.Close();
        }

        private void _view_EvClose(object sender, EventArgs e)
        {
            ViewClosed.RaiseIfValid(this);
        }

        private void _view_EvOk(object sender, EventArgs e)
        {
            _model.SelectedInstanceIndex = _view.SelectedInstanceIndex;
            _view.Close();
        }

        private void _model_EvChanged(object sender, EventArgs e)
        {
            _view.Instances = _model.Instances;
        }

        public void ShowView()
        {
            _view.Show();
        }
    }
}
