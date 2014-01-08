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
        public event EventHandler<ProcessMonitorEventArgs> EvProcessCreated;
        public event EventHandler<ProcessMonitorEventArgs> EvProcessExited;
        public event EventHandler<NewProcessMonitorDataEventArgs> EvNewData;

        private readonly IPerformanceCounterService _performanceCounterService;
        private readonly IPerformanceCounterInstanceInfoService _performanceCounterInstanceInfoService;
        private readonly IMainModel _model;
        private bool _isRunning;
        private readonly ManualResetEvent _startEvent = new ManualResetEvent(false);
        private readonly ManualResetEvent _stoppedEvent = new ManualResetEvent( false );
       
        public int Interval
        {
            get;
            set;
        }

        public bool IsInitialized
        {
            get { return _isRunning; }
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
        }

        private void update ()
        {
            Process process = null;

            while (_isRunning)
            {
                if(!_startEvent.WaitOne(100))
                {
                    continue;
                }                

                if ( process != null && process.HasExited )
                {
                    deinitializePerformanceCounterService();
                    EvProcessExited.RaiseIfValid( this, new ProcessMonitorEventArgs( process.ProcessName, process.Id ) );
                }

                try
                {
                    if ( !_performanceCounterService.IsInitialized )
                    {
                        var processes = Process.GetProcesses();
                        var filteredProcesses = processes.Where( p => p.ProcessName == _model.ProcessName ).ToArray();
                        if ( filteredProcesses.Length > 0 )
                        {
                            process = filteredProcesses.OrderBy( p => p.TotalProcessorTime ).Last();

                            _model.ProcessWindowTitle = process.MainWindowTitle;

                            string[] processInstances = _performanceCounterInstanceInfoService.GetCategoryInstanceNames( PerformanceCounterCategoryType.Process, process );
                            string[] clrMemoryInstances = _performanceCounterInstanceInfoService.GetCategoryInstanceNames( PerformanceCounterCategoryType.ClrMemory, process );

                            string processInstance = processInstances.Length > 0 ? processInstances.Last() : null;
                            string clrMemoryInstance = clrMemoryInstances.Length > 0 ? clrMemoryInstances.Last(): null;

                            if (processInstance != null) 
                            {
                                initializePerformanceCounterService( processInstance, clrMemoryInstance );
                                EvProcessCreated.RaiseIfValid( this, new ProcessMonitorEventArgs( process.ProcessName, process.Id ) );
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

            _stoppedEvent.Set();
        }

      
        public void Initialize ()
        {
            Deinitialize();

            ThreadPool.QueueUserWorkItem( o => update() );
          
            _stoppedEvent.Reset();
            _isRunning = true;            
        }

        public void Deinitialize ()
        {
            if ( IsInitialized )
            {
                _isRunning = false;
                _stoppedEvent.WaitOne();
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
