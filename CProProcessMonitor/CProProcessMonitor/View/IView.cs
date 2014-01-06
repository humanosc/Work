using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CProProcessMonitor.View
{
    public interface IView
    {
        event EventHandler EvLoaded;
        event EventHandler EvShown;
        event EventHandler EvHidden;
        event EventHandler EvClosed;

        SynchronizationContext Context { get; }
        void Show();
        bool ShowDialog ();
        void Hide();
        void Close();
    }
}
