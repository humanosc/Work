using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.View
{
    public interface IView
    {
        event EventHandler EvLoad;
        event EventHandler EvShow;
        event EventHandler EvHide;
        event EventHandler EvClose;

        void Show();
        void Hide();
        void Close();
    }
}
