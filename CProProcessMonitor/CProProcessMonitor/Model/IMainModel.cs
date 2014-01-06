using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Model
{
    public interface IMainModel
    {
        string ProcessWindowTitle { set; get; }
        string LogPath { set; get; }

        string ProcessName { set; get; }
        int WindowTop { set; get; }
        int WindowLeft { set; get; }
        int TimerResolutionId { set; get; }
        int DiagramWidth { set; get; }
        int DiagramHeight { set; get; }
    }
}
