using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using XLib.General;

namespace CProProcessMonitor.View
{
    public abstract class FormViewBase : Form, IView 
    {
        public event EventHandler EvLoaded;
        public event EventHandler EvShown;
        public event EventHandler EvHidden;
        public event EventHandler EvClosed;

        private SynchronizationContext _context;

        public SynchronizationContext Context
        {
            get { return _context; }
        }
             
        public new bool ShowDialog ()
        {
            return base.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        protected override void OnLoad ( EventArgs e )
        {
            EvLoaded.RaiseIfValid( this );    
            base.OnLoad( e );
        }
        protected override void OnVisibleChanged ( EventArgs e )
        {
            if ( Visible )
            {
                EvShown.RaiseIfValid( this );
            }
            else
            {
                EvHidden.RaiseIfValid( this );
            }

            base.OnVisibleChanged( e );
        }
        protected override void OnClosed ( EventArgs e )
        {
            EvClosed.RaiseIfValid( this );
            base.OnClosed( e );
        }

        public FormViewBase ()
        {
            _context = SynchronizationContext.Current;
        }      
    }
}
