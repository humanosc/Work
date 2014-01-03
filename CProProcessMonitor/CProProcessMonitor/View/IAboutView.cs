using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.View
{
    public interface IAboutView : IView 
    {
        string Title { set; }
        string Version { set; }
        string Description { set; }
        string Product { set; }
        string Copyright { set; }
        string Company { set; }
    }
}
