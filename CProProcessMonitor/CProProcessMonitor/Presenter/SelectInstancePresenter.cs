using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CProProcessMonitor.View;

namespace CProProcessMonitor.Presenter
{
    public class SelectInstancePresenter
    {
        private ISelectInstanceView _view;

        public SelectInstancePresenter ( ISelectInstanceView view )
        {
            _view = view;
        }
    }
}
