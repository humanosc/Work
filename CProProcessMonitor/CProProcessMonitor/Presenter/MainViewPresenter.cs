using CProProcessMonitor.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Presenter
{
    public class MainViewPresenter
    {
        private IMainView _view;

        public MainViewPresenter(IMainView view)
        {
            _view = view;
            
        }
    }
}
