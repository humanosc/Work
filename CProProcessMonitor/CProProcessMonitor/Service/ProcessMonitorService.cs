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
        public event EventHandler EvProcessExited;
        public event EventHandler<NewProcessMonitorDataEventArgs> EvNewData;

        private readonly IPerformanceCounterService _performanceCounterService;
        private readonly IPerformanceCounterInstanceInfoService _performanceCounterInstanceInfoService;
        private readonly IInstanceSelectorService _instanceSelectorService;
        private readonly IMainModel _model;
        private Thread _thread;
        private bool _isRunning = false;
        private readonly ManualResetEvent _startEvent = new ManualResetEvent(false);
      
        public int Interval
        {
            get;
            set;
        }

        public bool IsInitialized
        {
            get { return _thread != null; }
        }

        public ProcessMonitorService ( IPerformanceCounterService performanceCounterService,
                                       IPerformanceCounterInstanceInfoService performanceCounterInstanceInfoService,
                                       IInstanceSelectorService instanceSelectorService, 
                                       IMainModel model )
        {
            _performanceCounterService = performanceCounterService;
            _performanceCounterInstanceInfoService = performanceCounterInstanceInfoService;
            _instanceSelectorService = instanceSelectorService;
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

            while (_isRunning)
            {
                while (!_startEvent.WaitOne(100))
                {
                    if (!_isRunning)
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
                            int processIndex = filteredProcesses.Length > 1 ? _instanceSelectorService.SelectInstance( "Process", filteredProcesses.Select( p => string.Format( "Name:{0} PID:{1}", p.ProcessName, p.Id ) ).ToArray() ) : 0;
                            process = filteredProcesses[processIndex];

                            _model.ProcessWindowTitle = process.MainWindowTitle;

                            string[] processInstances = _performanceCounterInstanceInfoService.GetCategoryInstanceNames( PerformanceCounterCategoryType.Process, process );
                            string[] clrMemoryInstances = _performanceCounterInstanceInfoService.GetCategoryInstanceNames( PerformanceCounterCategoryType.ClrMemory, process );

                            int processInstanceIndex = processInstances.Length > 1 ? _instanceSelectorService.SelectInstance( PerformanceCounterCategoryType.Process.GetName(), processInstances ) : 0;
                            int clrMemoryInstanceIndex = clrMemoryInstances.Length > 1 ? _instanceSelectorService.SelectInstance( PerformanceCounterCategoryType.ClrMemory.GetName(), clrMemoryInstances ) : 0;

                            string processInstance = processInstances.Length > 0 ? processInstances[processInstanceIndex] : null;
                            string clrMemoryInstance = clrMemoryInstances.Length > 0 ? clrMemoryInstances[clrMemoryInstanceIndex] : null;

                            if ( processInstanceIndex != -1 && clrMemoryInstanceIndex != -1 )
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
            _thread = new Thread(new ThreadStart(update));
            _thread.IsBackground = true;
            _isRunning = true;
            _thread.Start();
        }

        public void Deinitialize ()
        {
            if (IsInitialized)
            {
                _isRunning = false;
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
