using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using XLib.General;
using CProProcessMonitor.Model;
using System.Globalization;

namespace CProProcessMonitor.Service
{
    public class ProcessMonitorService : IProcessMonitorService 
    {
        public event EventHandler EvProcessExited;
        public event EventHandler<NewProcessMonitorDataEventArgs> EvNewData;

        private readonly IPerformanceCounterService _performanceCounterService;
        private readonly IModel _model;

        public int Interval
        {
            get;
            set;
        }

        public ProcessMonitorService ( IPerformanceCounterService performanceCounterService, IModel model )
        {
            _performanceCounterService = performanceCounterService;
            _model = model;
        }

        private void update ()
        {
            var processes = Process.GetProcesses();
            var process = processes.FirstOrDefault( p => p.ProcessName.StartsWith(_model.ProcessName ) );
            if ( process == null )
            {
                _performanceCounterService.Deinitialize();
                EvProcessExited.RaiseIfValid( this );
                return;
            }

            _model.ProcessWindowTitle = process.MainWindowTitle;

            try
            {
                if ( !_performanceCounterService.IsInitialized )
                {
                 //   _performanceCounterService.Initialize( process.ProcessName );
                }

                float cpu = _performanceCounterService.GetNextCpuValue();
                float memory = _performanceCounterService.GetNextPrivateBytesValue();
                float clrMemory = _performanceCounterService.GetNextClrBytesValue();

                EvNewData.RaiseIfValid( this, new NewProcessMonitorDataEventArgs( cpu, memory, clrMemory));
            }
            catch
            {
                _performanceCounterService.Deinitialize();
                EvProcessExited.RaiseIfValid( this );
            }            
        }

      
        public void Initialize ()
        {

        }

        public void Deinitialize ()
        {
  
        }

        public void Start ()
        {

        }

        public void Stop ()
        {
 
        }    
    }
}
