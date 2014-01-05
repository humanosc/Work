using CProProcessMonitor.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CProProcessMonitor.Service;
using CProProcessMonitor.Model;
using System.Reflection;

namespace CProProcessMonitor.Presenter
{
    public class MainViewPresenter
    {
        struct IntervalEntry
        {
            public readonly string Text;
            public int Milliseconds;

            public IntervalEntry(string text, int milliseconds)
            {

                Text = text;
                Milliseconds = milliseconds;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private readonly IMainView _view;
        private readonly IProcessMonitorService _processMonitorService;
        private readonly IGnuPlotGeneratorService _plotGeneratorService;
        private readonly List<IntervalEntry> _intervals = new List<IntervalEntry>();
        private readonly IMainModel _model;

        public string AssemblyVersion
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return string.Format("CPro Process Monitor v{0}.{1}", version.Major, version.Minor);
            }
        }

        public MainViewPresenter(IMainView view, IMainModel model, IProcessMonitorService processMonitorService, IGnuPlotGeneratorService plotGeneratorService )
        {
            _view = view;
            _model = model;
            _processMonitorService = processMonitorService;
            _plotGeneratorService = plotGeneratorService;

            _intervals.Add(new IntervalEntry("Very very high resolution ~ 1 s", 1000));
            _intervals.Add(new IntervalEntry("Very high resolution ~ 5 s", 5000));
            _intervals.Add(new IntervalEntry("High resolution ~ 10 s", 10000));
            _intervals.Add(new IntervalEntry("Standard resolution ~ 30 s", 30000));
            _intervals.Add(new IntervalEntry("Low resolution ~ 1 m", 60000));
            _intervals.Add(new IntervalEntry("Very low resolution ~ 5 m", 300000));
            _intervals.Add(new IntervalEntry("Very very low resolution ~ 10 m", 600000));

            _view.UpdateIntervals = _intervals.Select(e => e.ToString()).ToArray();
            _view.SelectedUpdateIntervalIndex = _model.TimerResolutionId;
            _view.Title = AssemblyVersion;

            _view.EvGenerateClrMemoryDiagram += _view_EvGenerateClrMemoryDiagram;
            _view.EvGenerateCpuDiagram += _view_EvGenerateCpuDiagram;
            _view.EvGenerateDiagram += _view_EvGenerateDiagram;
            _view.EvGenerateMemoryDiagram += _view_EvGenerateMemoryDiagram;
            _view.EvUpdateIntervalChanged += _view_EvUpdateIntervalChanged;
        }

        private void _view_EvUpdateIntervalChanged(object sender, EventArgs e)
        {
            _model.TimerResolutionId = _view.SelectedUpdateIntervalIndex;
        }

        public void ShowView()
        {
            _view.Show();
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
