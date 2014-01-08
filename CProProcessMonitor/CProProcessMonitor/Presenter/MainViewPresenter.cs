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

        private readonly IMainModelSerializerService _moderlSerializerService;
        private readonly IProcessMonitorService _processMonitorService;
        private readonly ILogService _logService;
        private readonly IGnuPlotGeneratorService _plotGeneratorService;
        private readonly List<IntervalEntry> _intervals = new List<IntervalEntry>();
        private readonly IMainModel _model;
        private readonly AboutViewPresenter _aboutViewPresenter;

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
                                 IGnuPlotGeneratorService plotGeneratorService,
                                 AboutViewPresenter aboutViewPresenter ) : base( view )
        {
            _model = model;
            _logService = logService;
            _moderlSerializerService = modelSerializerService;
            _processMonitorService = processMonitorService;
            _plotGeneratorService = plotGeneratorService;
            _aboutViewPresenter = aboutViewPresenter;

            _intervals.Add(new IntervalEntry("Very very high resolution ~ 1 s", 1000));
            _intervals.Add(new IntervalEntry("Very high resolution ~ 5 s", 5000));
            _intervals.Add(new IntervalEntry("High resolution ~ 10 s", 10000));
            _intervals.Add(new IntervalEntry("Standard resolution ~ 30 s", 30000));
            _intervals.Add(new IntervalEntry("Low resolution ~ 1 m", 60000));
            _intervals.Add(new IntervalEntry("Very low resolution ~ 5 m", 300000));
            _intervals.Add(new IntervalEntry("Very very low resolution ~ 10 m", 600000));

            View.UpdateIntervals = _intervals.Select(e => e.ToString()).ToArray();
            View.Title = AssemblyVersion;
         
            _processMonitorService.EvNewData += _processMonitorService_EvNewData;
            _processMonitorService.EvProcessCreated += _processMonitorService_EvProcessCreated;
            _processMonitorService.EvProcessExited += _processMonitorService_EvProcessExited;

            View.StateText = "Waiting for process...";
        }

        void _processMonitorService_EvProcessCreated ( object sender, ProcessMonitorEventArgs e )
        {
            View.Context.Send( () => View.StateText = string.Format( "Attached to {0} ({1})", e.ProcessName, e.ProcessId ));
        }

        private void _processMonitorService_EvProcessExited ( object sender, EventArgs e )
        {
            View.Context.Send( () => 
                                {
                                    View.Reset();
                                    View.StateText = "Waiting for process...";
                                });

        }

        private void _processMonitorService_EvNewData ( object sender, NewProcessMonitorDataEventArgs e )
        {
            View.Context.Send(() =>
                {
                    View.Memory = e.Memory;
                    View.ClrMemory = e.ClrMemory;
                    View.CPU = e.Cpu;
                });            

            _logService.Log( e.Cpu, e.Memory, e.ClrMemory );
        }


        public override void OnClosed()
        {
             _processMonitorService.Stop();
             _processMonitorService.Deinitialize();

            _model.WindowTop = View.Top;
            _model.WindowLeft = View.Left;            
            _moderlSerializerService.Serialize( SETTINGS_PATH, _model );
            
            _logService.Deinitialize();
        }

        public override void OnLoaded()
        {
            _moderlSerializerService.Deserialize( SETTINGS_PATH, _model );

            View.Top = _model.WindowTop;
            View.Left = _model.WindowLeft;
            View.SelectedUpdateIntervalIndex = _model.TimerResolutionId;
           
            _logService.Initialize( LOG_DIR_PATH );

            _processMonitorService.Initialize();
            _processMonitorService.Start();
        }

        public virtual void OnClearLog()
        {
            _logService.ClearLog();
        }

        public virtual void OnOpenLogFile()
        {
            _logService.OpenLog();
        }

        public virtual void OnOpenLogFolder()
        {
            _logService.OpenLogFolder();
        }

        public virtual void OnCleanupLogFolder()
        {
            _logService.CleanupLogFolder();
        }

        public virtual void OnAbout()
        {
            _aboutViewPresenter.ShowDialogView();
        }

        public virtual void OnShow()
        {
            View.Show();
        }

        public virtual void OnHide()
        {
            View.Hide();
        }

        public virtual void OnClose()
        {
            View.Close();
        }        

        public virtual void OnUpdateIntervalChanged()
        {
            _model.TimerResolutionId = View.SelectedUpdateIntervalIndex;
            _processMonitorService.Interval = _intervals[View.SelectedUpdateIntervalIndex].Milliseconds;
        }

        public virtual void OnGenerateMemoryDiagram()
        {
            _plotGeneratorService.Generate(GnuPlotDiagramType.Memory);
        }

        public virtual void OnGenerateDiagram ()
        {
            _plotGeneratorService.Generate(GnuPlotDiagramType.All);
        }

        public virtual void OnGenerateCpuDiagram ()
        {
            _plotGeneratorService.Generate(GnuPlotDiagramType.Cpu);
        }

        public virtual void OnGenerateClrMemoryDiagram ()
        {
            _plotGeneratorService.Generate(GnuPlotDiagramType.ClrMemory);
        }
    }
}
