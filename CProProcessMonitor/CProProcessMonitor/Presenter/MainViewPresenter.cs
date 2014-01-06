using CProProcessMonitor.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CProProcessMonitor.Service;
using CProProcessMonitor.Model;
using System.Reflection;
using System.IO;
using System.Threading;

namespace CProProcessMonitor.Presenter
{
    public class MainViewPresenter : GenericPresenterBase<IMainView>
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

        const string SETTINGS_PATH = "Settings.xml";
        const string LOG_DIR_PATH = "Log";

        private readonly SynchronizationContext _syncContext;
        private readonly IMainModelSerializerService _moderlSerializerService;
        private readonly IProcessMonitorService _processMonitorService;
        private readonly ILogService _logService;
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

        public MainViewPresenter(IMainView view, 
                                 IMainModel model, 
                                 IMainModelSerializerService modelSerializerService,
                                 IProcessMonitorService processMonitorService, 
                                 ILogService logService,
                                 IGnuPlotGeneratorService plotGeneratorService ) : base( view )
        {
            _model = model;
            _logService = logService;
            _moderlSerializerService = modelSerializerService;
            _processMonitorService = processMonitorService;
            _plotGeneratorService = plotGeneratorService;

            _intervals.Add(new IntervalEntry("Very very high resolution ~ 1 s", 1000));
            _intervals.Add(new IntervalEntry("Very high resolution ~ 5 s", 5000));
            _intervals.Add(new IntervalEntry("High resolution ~ 10 s", 10000));
            _intervals.Add(new IntervalEntry("Standard resolution ~ 30 s", 30000));
            _intervals.Add(new IntervalEntry("Low resolution ~ 1 m", 60000));
            _intervals.Add(new IntervalEntry("Very low resolution ~ 5 m", 300000));
            _intervals.Add(new IntervalEntry("Very very low resolution ~ 10 m", 600000));

            View.UpdateIntervals = _intervals.Select(e => e.ToString()).ToArray();
            View.Title = AssemblyVersion;
            View.EvLoaded += View_EvLoaded;
            View.EvClosed += View_EvClosed;         
            View.EvGenerateClrMemoryDiagram += View_EvGenerateClrMemoryDiagram;
            View.EvGenerateCpuDiagram += View_EvGenerateCpuDiagram;
            View.EvGenerateDiagram += View_EvGenerateDiagram;
            View.EvGenerateMemoryDiagram += View_EvGenerateMemoryDiagram;
            View.EvUpdateIntervalChanged += View_EvUpdateIntervalChanged;

            _processMonitorService.EvNewData += _processMonitorService_EvNewData;
            _processMonitorService.EvProcessExited += _processMonitorService_EvProcessExited;
        }

        private void _processMonitorService_EvProcessExited ( object sender, EventArgs e )
        {
            _syncContext.Send( o => View.Reset(), null );
        }

        private void _processMonitorService_EvNewData ( object sender, NewProcessMonitorDataEventArgs e )
        {
            _logService.Log( e.Cpu, e.Memory, e.ClrMemory );
        }

        private void View_EvClosed ( object sender, EventArgs e )
        {
            _processMonitorService.Stop();

            _model.WindowTop = View.Top;
            _model.WindowLeft = View.Left;            
            _moderlSerializerService.Serialize( SETTINGS_PATH, _model );
            
            _logService.Deinitialize();
        }

        private void View_EvLoaded ( object sender, EventArgs e )
        {
            _moderlSerializerService.Deserialize( SETTINGS_PATH, _model );

            View.Top = _model.WindowTop;
            View.Left = _model.WindowLeft;
            View.SelectedUpdateIntervalIndex = _model.TimerResolutionId;
           
            _logService.Initialize( LOG_DIR_PATH );

            _processMonitorService.Start();
        }

        private void View_EvUpdateIntervalChanged(object sender, EventArgs e)
        {
            _model.TimerResolutionId = View.SelectedUpdateIntervalIndex;
        }

        private void View_EvGenerateMemoryDiagram ( object sender, EventArgs e )
        {

        }

        private void View_EvGenerateDiagram ( object sender, EventArgs e )
        {

        }

        private void View_EvGenerateCpuDiagram ( object sender, EventArgs e )
        {

        }

        private void View_EvGenerateClrMemoryDiagram ( object sender, EventArgs e )
        {

        }
    }
}
