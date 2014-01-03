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
        private readonly IPerformanceCounterInstanceSelectorService _performanceCounterInstanceSelectorService;
        private readonly IModel _model;
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
                                       IPerformanceCounterInstanceSelectorService performanceCounterInstanceSelectorService, 
                                       IModel model )
        {
            _performanceCounterService = performanceCounterService;
            _performanceCounterInstanceInfoService = performanceCounterInstanceInfoService;
            _performanceCounterInstanceSelectorService = performanceCounterInstanceSelectorService;
            _model = model;
        }

        private void update ()
        {
            while (_isRunning)
            {
                _startEvent.WaitOne();

                var processes = Process.GetProcesses();
                var process = processes.FirstOrDefault(p => p.ProcessName.StartsWith(_model.ProcessName));
                if (process == null)
                {
                    _performanceCounterService.Deinitialize();
                    EvProcessExited.RaiseIfValid(this);
                    return;
                }

                _model.ProcessWindowTitle = process.MainWindowTitle;

                try
                {
                    if (!_performanceCounterService.IsInitialized)
                    {
                        string[] processInstances = _performanceCounterInstanceInfoService.GetProcessCategoryInstanceNames(process.ProcessName);
                        string[] clrMemoryInstances = _performanceCounterInstanceInfoService.GetClrMemoryCategoryInstanceNames(process.ProcessName);

                        string processInstance = processInstances.Length > 1 ? _performanceCounterInstanceSelectorService.SelectInstance(processInstances) : processInstances[0];
                        string clrMemoryInstance = clrMemoryInstances.Length > 1 ? _performanceCounterInstanceSelectorService.SelectInstance(clrMemoryInstances) : clrMemoryInstances[0];

                        _performanceCounterService.Initialize(processInstance, clrMemoryInstance);
                    }

                    float cpu = _performanceCounterService.GetNextCpuValue();
                    float memory = _performanceCounterService.GetNextPrivateBytesValue();
                    float clrMemory = _performanceCounterService.GetNextClrBytesValue();

                    EvNewData.RaiseIfValid(this, new NewProcessMonitorDataEventArgs(cpu, memory, clrMemory));
                }
                catch
                {
                    _performanceCounterService.Deinitialize();
                    EvProcessExited.RaiseIfValid(this);
                }

                Thread.Sleep(Interval);
            }
        }

      
        public void Initialize ()
        {
            Deinitialize();
            _thread = new Thread(new ThreadStart(update));
            _thread.IsBackground = true;
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
