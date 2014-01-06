using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CProProcessMonitor.Service
{
    public class LogService : ILogService 
    {       
        private StreamWriter _writer;
        private string _logFilePath;
        private string _logDirectoryPath;

        public bool IsInitialized
        {
            get { return _writer != null; }
        }

        private static StreamWriter createLogStream ( string logPath )
        {
            StreamWriter writer = new StreamWriter( new FileStream( logPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, 64 ) );
            writer.WriteLine( "Timestamp\tCPU\tMemory (MB)\tCLR-Memory (MB)" );
            writer.AutoFlush = true;
            return writer;
        }

        public void Initialize ( string logDirectoryPath )
        {
            _logDirectoryPath = logDirectoryPath;

            if ( !Directory.Exists( logDirectoryPath ) )
            {
                Directory.CreateDirectory( logDirectoryPath );
            }

            _logFilePath = Path.Combine( logDirectoryPath, string.Format( "CPro Process Monitor ({0}).log", DateTime.Now.ToString( "ddMMyy-hhmmss" ) ) );
    
            _writer = createLogStream( _logFilePath );
        }

        public void Deinitialize ()
        {
            if ( _writer != null )
            {
                _writer.Close();
                _writer = null;
                _logFilePath = null;
            }
        }

        public void Log ( float cpu, float memory, float clrMemory )
        {
            string cpuStr = cpu.ToString( "0.00", CultureInfo.InvariantCulture );
            string memoryStr = memory.ToString( "0.00", CultureInfo.InvariantCulture );
            string clrmemoryStr = clrMemory.ToString( "0.00", CultureInfo.InvariantCulture );
            
            _writer.WriteLine( string.Format( "{0}\t{1}\t{2}\t{3}", DateTime.Now.ToString( CultureInfo.InvariantCulture ), cpuStr, memoryStr, clrmemoryStr ) );
        }            

        public void ClearLog ()
        {
            if ( IsInitialized )
            {
                _writer.Close();
                _writer = createLogStream( _logFilePath );
            }
        }

        public void ClearLogFolder ()
        {
            string[] filePaths = Directory.GetFiles( _logDirectoryPath );
            foreach ( var path in filePaths )
            {
                try
                {
                    File.Delete( path );
                }
                catch
                {
                }
            }
        }

        public void OpenLog ()
        {
            Process.Start( _logFilePath );
        }
    }
}
