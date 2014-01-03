using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Pipes;
using System.IO;
using System.Threading;

namespace LPKFHistoryAssistent.General
{
    public class PipeClient
    {
        private Thread _thread;
        private ManualResetEvent _sendingFinished = new ManualResetEvent( true );
        private string _pipeName;
        private NamedPipeClientStream _pipeStream;

        public string Name 
        { 
            get{ return _pipeName; }
        }
        
        public PipeClient()
        {
        }

        private void pipeWorker (object args)
        {
            try
            {
                string value = args.ToString();
                _pipeStream = new NamedPipeClientStream( ".", _pipeName, PipeDirection.Out );
                _pipeStream.Connect();
                byte[] messageBytes = Encoding.UTF8.GetBytes( value );
                _pipeStream.Write( messageBytes, 0, messageBytes.Length );
                _pipeStream.WaitForPipeDrain();
                _pipeStream.Close();
                Thread.Sleep( 5000 );
                _sendingFinished.Set();
            }
            catch
            {
            }
        }

        public bool IsConnected
        {
            get { return _pipeName != null; }
        }

        public void Connect(string pipeName)
        {
            if ( IsConnected )
            {
                Disconnect();
            }
            
            _pipeName = pipeName;
        }       

        public void Disconnect()
        {
            if ( !_sendingFinished.WaitOne( 500 ) )
            {
                _thread.Abort();
            }
            
            _pipeName = null;
        }

        public void Send(string value)
        {
            if ( _sendingFinished.WaitOne() )
            {
                _sendingFinished.Reset();
                _thread = new Thread( new ParameterizedThreadStart( pipeWorker ) );
                _thread.IsBackground = true;
                _thread.Start( value );
            }
        }

        public void SendLine(string value)
        {
            Send( value + "\n" );   
        }
    }
}
