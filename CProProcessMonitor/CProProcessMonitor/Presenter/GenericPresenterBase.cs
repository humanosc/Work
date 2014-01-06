using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CProProcessMonitor.View;

namespace CProProcessMonitor.Presenter
{
    public abstract class GenericPresenterBase<TView> where TView : IView
    {
        protected TView View
        {
            private set;
            get;
        }

        public SynchronizationContext ViewContext
        {
            get { return View.Context; }
        }

        public GenericPresenterBase ( TView view )
        {
            View = view;
        }
               
        public void ShowView ()
        {
            View.Show();
        }

        public bool ShowDialogView ()
        {
            return View.ShowDialog();
        }
    }
}
