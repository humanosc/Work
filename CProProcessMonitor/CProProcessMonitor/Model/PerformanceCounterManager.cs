using System;
using XLib.General;
using System.Linq;
using System.Diagnostics;

namespace CProProcessMonitor
{
    public abstract class PerformanceCounterEventArgsBase : EventArgs
    {
        public readonly string Name;

        public PerformanceCounterEventArgsBase ( string performanceCounterName )
	    {
            Name = performanceCounterName;
	    }
    }

    public class PerformanceCounterCategoryNotSupportedEventArgs : PerformanceCounterEventArgsBase   
    {
        public PerformanceCounterCategoryNotSupportedEventArgs ( string categoryName ) : base( categoryName ) {}
    }

    public class PerformanceCounterBrokenEventArgs : PerformanceCounterEventArgsBase
    {
        public PerformanceCounterBrokenEventArgs ( string performanceCounterName ) : base ( performanceCounterName ) { }
    }

    public class PerformanceCounterManager
    {
        public event EventHandler<PerformanceCounterCategoryNotSupportedEventArgs> PerformanceCounterNotSupported;
        public event EventHandler<PerformanceCounterBrokenEventArgs> PerformanceCounterBroken;

        private static readonly string _processCategory = "Process";
        private static readonly string _clrMemoryCategory = ".NET CLR Memory";

        private PerformanceCounter _processCpu;
        private PerformanceCounter _processPrivateBytes;
        private PerformanceCounter _processClrBytes;
        private bool _isInitialized;
    
        public bool IsInitialized { get { return _isInitialized; } }
        public PerformanceCounter CPU { get { return _processCpu; } }
        public PerformanceCounter PrivateBytes { get { return _processPrivateBytes; } }
        public PerformanceCounter ClrBytes { get { return _processClrBytes; } }

        public float GetNextCpuValue ()
        {
            return _processCpu.NextValue();
        }

        public float GetNextPrivateBytesValue ()
        {
            return _processPrivateBytes.NextValue();
        }

        public static string[] GetProcessCategoryInstanceNames ( string processName )
        {
            var category = new PerformanceCounterCategory( _processCategory );
            return category.GetInstanceNames().Where( s => s.StartsWith( processName ) ).ToArray();
        }

        public static string[] GetClrMemoryCategoryInstanceNames ( string processName )
        {
            var category = new PerformanceCounterCategory( _clrMemoryCategory );
            return category.GetInstanceNames().Where( s => s.StartsWith( processName ) ).ToArray();
        }

        public float GetNextClrBytesValue ()
        {
                if ( _processClrBytes != null )
                {
                    double value = _processClrBytes.NextValue();
                    if ( (int)value == 0 )
                    {
                        PerformanceCounterBroken.RaiseIfValid( this, new PerformanceCounterBrokenEventArgs( _processClrBytes.CategoryName ) );
                    }
                }

                return _processClrBytes != null ? _processClrBytes.NextValue() : 0.0f;
        }

        public void Initialize ( string processPerformanceCounterInstanceName, string clrMemoryPerformanceCounterInstanceName )
        {
            _processCpu = new PerformanceCounter( _processCategory, "% Processor Time", processPerformanceCounterInstanceName );
            _processPrivateBytes = new PerformanceCounter( _processCategory, "Private Bytes", processPerformanceCounterInstanceName );


            if ( PerformanceCounterCategory.InstanceExists( clrMemoryPerformanceCounterInstanceName, _clrMemoryCategory ) )
            {
                _processClrBytes = new PerformanceCounter( _clrMemoryCategory, "# Bytes in all heaps", clrMemoryPerformanceCounterInstanceName );
            }
            else
            {

                PerformanceCounterNotSupported.RaiseIfValid( this, new PerformanceCounterCategoryNotSupportedEventArgs( _clrMemoryCategory ) );

//                MessageBox.Show( "The .NET CLR Memory Perfomance Counter is not supported", "Info...", MessageBoxButtons.OK, MessageBoxIcon.Information );
            }

            _isInitialized = true;

        }

        public void Destroy ()
        {
            if ( _processCpu != null )
            {
                _processCpu.Close();
                _processCpu = null;
            }

            if ( _processPrivateBytes != null )
            {
                _processPrivateBytes.Close();
                _processPrivateBytes = null;
            }

            if ( _processClrBytes != null )
            {
                _processClrBytes.Close();
                _processClrBytes = null;
            }

            _isInitialized = false;
        }
    }
}