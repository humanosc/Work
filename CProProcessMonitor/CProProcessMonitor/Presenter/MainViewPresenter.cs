using CProProcessMonitor.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CProProcessMonitor.Service;

namespace CProProcessMonitor.Presenter
{
    public class MainViewPresenter
    {
        private readonly IMainView _view;
        private readonly IPerformanceCounterService _performanceCounterService;
        private readonly IGnuPlotGenerator _plotGeneratorService;

        public MainViewPresenter(IMainView view, IPerformanceCounterService performanceCounterService, IGnuPlotGenerator plotGeneratorService )
        {
            _view = view;
            _performanceCounterService = performanceCounterService;
            _plotGeneratorService = plotGeneratorService;
           

            _view.EvGenerateClrMemoryDiagram += _view_EvGenerateClrMemoryDiagram;
            _view.EvGenerateCpuDiagram += _view_EvGenerateCpuDiagram;
            _view.EvGenerateDiagram += _view_EvGenerateDiagram;
            _view.EvGenerateMemoryDiagram += _view_EvGenerateMemoryDiagram;
        }

        private void _view_EvGenerateMemoryDiagram ( object sender, EventArgs e )
        {

        }

        private void _view_EvGenerateDiagram ( object sender, EventArgs e )
        {
        }

        private void _view_EvGenerateCpuDiagram ( object sender, EventArgs e )
        {
        }

        private void _view_EvGenerateClrMemoryDiagram ( object sender, EventArgs e )
        {
        }
    }
}
