using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Service
{
    public enum PerformanceCounterCategoryType
    {
        Process,
        ClrMemory
    }

    public static class PerformanceCounterCategoryExt
    {
        public static string GetName ( this PerformanceCounterCategoryType instance )
        {
            switch ( instance )
            {
                case PerformanceCounterCategoryType.Process: return "Process";
                case PerformanceCounterCategoryType.ClrMemory: return ".NET CLR Memory";
            }

            return null;
        }

        public static string GetProcessIdCounterName(this PerformanceCounterCategoryType instance)
        {
            switch (instance)
            {
                case PerformanceCounterCategoryType.Process: return "ID Process";
                case PerformanceCounterCategoryType.ClrMemory: return "Process ID";
            }

            return null;
        }
    }

    public interface IPerformanceCounterInstanceInfoService
    {
        string[] GetCategoryInstanceNames ( PerformanceCounterCategoryType categoryType, Process process );
    }
}
