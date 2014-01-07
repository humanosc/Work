using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Service
{   
    public class PerformanceCounterInstanceInfoService : IPerformanceCounterInstanceInfoService 
    {
        public PerformanceCounterInstanceInfoService ()
        {
        }

        private static string[] GetProcessInstanceNames ( PerformanceCounterCategoryType categoryType, int pid )
        {
            PerformanceCounterCategory cat = new PerformanceCounterCategory( categoryType.GetName() );
            List<string> names = new List<string>();
            string[] instances = cat.GetInstanceNames();
            foreach ( string instance in instances )
            {

                using ( PerformanceCounter cnt = new PerformanceCounter( categoryType.GetName(),
                     "ID Process", instance, true ) )
                {
                    int val = (int)cnt.RawValue;
                    if ( val == pid )
                    {
                        names.Add( instance );
                    }
                }
            }
            return names.ToArray();
        }

        public string[] GetCategoryInstanceNames ( PerformanceCounterCategoryType categoryType, Process process )
        {
            var category = new PerformanceCounterCategory( categoryType.GetName() );
            return GetProcessInstanceNames( categoryType, process.Id ); // category.GetInstanceNames().Where( s => s == process.ProcessName ).ToArray();
        }
    }
}
