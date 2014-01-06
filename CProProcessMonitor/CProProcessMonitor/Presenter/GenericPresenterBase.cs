using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CProProcessMonitor.View;

namespace CProProcessMonitor.Presenter
{
    public abstract class GenericPresenterBase<TView> : IPresenter where TView : IView
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

            View.AttachPresenter(this);
        }
               
        public void ShowView ()
        {
            View.Show();
        }

        public bool ShowDialogView ()
        {
            return View.ShowDialog();
        }

        public virtual void OnLoaded()
        {
        }

        public virtual void OnShown()
        {
        }

        public virtual void OnHidden()
        {
        }

        public virtual void OnClosed()
        {
        }
    }
}
