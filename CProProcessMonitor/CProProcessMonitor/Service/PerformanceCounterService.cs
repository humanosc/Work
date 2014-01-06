using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using XLib.General;

namespace CProProcessMonitor.Service
{
    public class PerformanceCounterService : IPerformanceCounterService
    {
        private static int _processorCount = Environment.ProcessorCount;
        
        public event EventHandler<PerformanceCounterCategoryNotSupportedEventArgs> PerformanceCounterNotSupported;
        public event EventHandler<PerformanceCounterBrokenEventArgs> PerformanceCounterBroken;

        private PerformanceCounter _processCpu;
        private PerformanceCounter _processPrivateBytes;
        private PerformanceCounter _processClrBytes;
        private bool _isInitialized;

        public bool IsInitialized { get { return _isInitialized; } }

        public float GetNextCpuValue()
        {
            return _processCpu.NextValue() / _processorCount;
        }

        public float GetNextPrivateBytesValue()
        {
            return _processPrivateBytes.NextValue() / 1024 / 1024;
        }

        public float GetNextClrBytesValue()
        {
            if (_processClrBytes != null)
            {
                double value = _processClrBytes.NextValue();
                if ((int)value == 0)
                {
                    PerformanceCounterBroken.RaiseIfValid(this, new PerformanceCounterBrokenEventArgs(_processClrBytes.CategoryName));
                }
            }

            return _processClrBytes != null ? _processClrBytes.NextValue() / 1024 / 1024 : 0.0f;
        }

        public void Initialize(string processPerformanceCounterInstanceName, string clrMemoryPerformanceCounterInstanceName)
        {
            _processCpu = new PerformanceCounter(PerformanceCounterCategoryNames.Process, "% Processor Time", processPerformanceCounterInstanceName);
            _processPrivateBytes = new PerformanceCounter(PerformanceCounterCategoryNames.Process, "Private Bytes", processPerformanceCounterInstanceName);


            if (PerformanceCounterCategory.InstanceExists(clrMemoryPerformanceCounterInstanceName, PerformanceCounterCategoryNames.ClrMemory))
            {
                _processClrBytes = new PerformanceCounter(PerformanceCounterCategoryNames.ClrMemory, "# Bytes in all heaps", clrMemoryPerformanceCounterInstanceName);
            }
            else
            {

                PerformanceCounterNotSupported.RaiseIfValid(this, new PerformanceCounterCategoryNotSupportedEventArgs(PerformanceCounterCategoryNames.ClrMemory));
            }

            _isInitialized = true;

        }

        public void Deinitialize()
        {
            if (_processCpu != null)
            {
                _processCpu.Close();
                _processCpu = null;
            }

            if (_processPrivateBytes != null)
            {
                _processPrivateBytes.Close();
                _processPrivateBytes = null;
            }

            if (_processClrBytes != null)
            {
                _processClrBytes.Close();
                _processClrBytes = null;
            }

            _isInitialized = false;
        }
    }
}
