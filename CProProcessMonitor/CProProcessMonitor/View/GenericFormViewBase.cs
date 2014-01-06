using CProProcessMonitor.Presenter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using XLib.General;

namespace CProProcessMonitor.View
{
    public abstract class GenericFormViewBase<TPresenter> : Form, IView where TPresenter : class, IPresenter
    {
        private SynchronizationContext _context;

        public SynchronizationContext Context
        {
            get { return _context; }
        }

        protected TPresenter Presenter
        {
            private set;
            get;
        }

        public new bool ShowDialog ()
        {
            return base.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        protected override void OnLoad ( EventArgs e )
        {
            Presenter.OnLoaded();  
            base.OnLoad( e );
        }

        protected override void OnVisibleChanged ( EventArgs e )
        {
            if ( Visible )
            {
                Presenter.OnShown();
            }
            else
            {
                Presenter.OnHidden();
            }

            base.OnVisibleChanged( e );
        }
        
        protected override void OnClosed ( EventArgs e )
        {
            Presenter.OnClosed();
            base.OnClosed( e );
        }

        public GenericFormViewBase ()
        {
            _context = SynchronizationContext.Current;
        }

        public void AttachPresenter<T>(T presenter) where T : class, IPresenter 
        {
            Debug.Assert(presenter != null, "A presenter is already attached");

            Presenter = presenter as TPresenter;
        }

        public void DetachPresenter()
        {
            Presenter = null;
        }
    }
}
