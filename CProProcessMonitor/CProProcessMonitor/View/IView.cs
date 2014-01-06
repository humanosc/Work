using CProProcessMonitor.Presenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CProProcessMonitor.View
{
    public static class SynchronizationContextExt
    {
        public static void Send( this SynchronizationContext instance, Action action )
        {
            instance.Send(o => action(), null);
        }
    }

    public interface IView
    {
        SynchronizationContext Context { get; }
            
        void AttachPresenter<TPresenter>(TPresenter presenter) where TPresenter : class, IPresenter;
        void DetachPresenter();
                
        void Show();
        bool ShowDialog ();
        void Hide();
        void Close();
    }
}
