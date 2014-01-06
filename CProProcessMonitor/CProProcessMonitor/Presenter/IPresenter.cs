using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CProProcessMonitor.Presenter
{
    public interface IPresenter
    {
        SynchronizationContext ViewContext { get; }       
        void ShowView ();
        bool ShowDialogView();

        void OnLoaded();
        void OnShown();
        void OnHidden();
        void OnClosed();
    }
}
