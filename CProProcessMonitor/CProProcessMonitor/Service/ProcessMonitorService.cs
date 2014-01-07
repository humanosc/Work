using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using XLib.General;
using CProProcessMonitor.Model;
using System.Globalization;
using System.Threading;

namespace CProProcessMonitor.Service
{
    public class ProcessMonitorService : IProcessMonitorService 
    {
        public event EventHandler EvProcessCreated;
        public event EventHandler EvProcessExited;
        public event EventHandler<NewProcessMonitorDataEventArgs> EvNewData;

        private readonly IPerformanceCounterService _performanceCounterService;
        private readonly IPerformanceCounterInstanceInfoService _performanceCounterInstanceInfoService;
        private readonly IMainModel _model;
        private bool _isInitialized;
        private bool _isProcessObserverRunning;
        private bool _isProcessMonitorRunning;
        private readonly ManualResetEvent _startEvent = new ManualResetEvent(false);
      
        public int Interval
        {
            get;
            set;
        }

        public bool IsInitialized
        {
            get { return _isInitialized != null; }
        }

        public ProcessMonitorService ( IPerformanceCounterService performanceCounterService,
                                       IPerformanceCounterInstanceInfoService performanceCounterInstanceInfoService,
                                       IMainModel model )
        {
            _performanceCounterService = performanceCounterService;
            _performanceCounterInstanceInfoService = performanceCounterInstanceInfoService;
            _model = model;
        }

        private void initializePerformanceCounterService ( string processPerformanceCounterInstanceName, string clrMemoryPerformanceCounterInstanceName )
        {
            _performanceCounterService.PerformanceCounterBroken += _performanceCounterService_PerformanceCounterBroken;
            _performanceCounterService.Initialize( processPerformanceCounterInstanceName, clrMemoryPerformanceCounterInstanceName );              
        }

        private void _performanceCounterService_PerformanceCounterBroken ( object sender, PerformanceCounterBrokenEventArgs e )
        {
            deinitializePerformanceCounterService();
        }

        private void deinitializePerformanceCounterService ()
        {
            _performanceCounterService.PerformanceCounterBroken -= _performanceCounterService_PerformanceCounterBroken;
            _performanceCounterService.Deinitialize();
            EvProcessExited.RaiseIfValid( this );
        }

        private void update ()
        {
            Process process = null;

            while (_isProcessObserverRunning)
            {
                while (!_startEvent.WaitOne(100))
                {
                    if (!_isProcessObserverRunning)
                    {
                        return;
                    }
                }

                if ( process != null && process.HasExited )
                {
                    deinitializePerformanceCounterService();
                    EvProcessExited.RaiseIfValid( this );
                }

                try
                {
                    if ( !_performanceCounterService.IsInitialized )
                    {
                        var processes = Process.GetProcesses();
                        var filteredProcesses = processes.Where( p => p.ProcessName == _model.ProcessName ).ToArray();
                        if ( filteredProcesses.Length > 0 )
                        {
                            EvProcessCreated.RaiseIfValid(this);

                            process = filteredProcesses.Min( p => p.TotalProcessorTime.TotalSeconds );

                            _model.ProcessWindowTitle = process.MainWindowTitle;

                            string[] processInstances = _performanceCounterInstanceInfoService.GetCategoryInstanceNames( PerformanceCounterCategoryType.Process, process );
                            string[] clrMemoryInstances = _performanceCounterInstanceInfoService.GetCategoryInstanceNames( PerformanceCounterCategoryType.ClrMemory, process );

                            string processInstance = processInstances.Length > 0 ? processInstances.Last() : null;
                            string clrMemoryInstance = clrMemoryInstances.Length > 0 ? clrMemoryInstances.Last(): null;

                            if (processInstance != null) 
                            {
                                initializePerformanceCounterService( processInstance, clrMemoryInstance );
                            }
                        }                       
                    }

                    if( _performanceCounterService.IsInitialized )
                    {
                        float cpu = _performanceCounterService.GetNextCpuValue();
                        float memory = _performanceCounterService.GetNextPrivateBytesValue();
                        float clrMemory = _performanceCounterService.GetNextClrBytesValue();

                        EvNewData.RaiseIfValid( this, new NewProcessMonitorDataEventArgs( cpu, memory, clrMemory ) );
                    }
                }
                catch
                {
                    deinitializePerformanceCounterService();
                }

                Thread.Sleep(Interval);
            }
        }

      
        public void Initialize ()
        {
            Deinitialize();
            ThreadPool.QueueUserWorkItem( o => update() );
            _isProcessObserverRunning = true;            
        }

        public void Deinitialize ()
        {
            if (IsInitialized)
            {
                _isProcessObserverRunning = false;
                _thread.Join();
                _thread = null;
            }
        }

        public void Start ()
        {
            _startEvent.Set();
        }

        public void Stop ()
        {
            _startEvent.Reset();
        }    
    }
}
