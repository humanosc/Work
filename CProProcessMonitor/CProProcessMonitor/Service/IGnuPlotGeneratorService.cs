using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Service
{
    public enum GnuPlotDiagramType
    {
        All,
        Cpu,
        Memory,
        ClrMemory
    }

    public interface IGnuPlotGeneratorService
    {
        void Generate ( GnuPlotDiagramType type, string title, string logPath );
    }
}
