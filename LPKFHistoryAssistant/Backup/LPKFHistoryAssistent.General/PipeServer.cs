using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Pipes;
using System.IO;

namespace LPKFHistoryAssistent.General
{
    public class PipeServerEventArgs : EventArgs
    {
        public readonly string Message;
        public Exception Error;

        public PipeServerEventArgs(string message, Exception error)
        {
            Message = message;
            Error = error;
        }
    }

    public class PipeServer
    {
        private ManualResetEvent _shutDownServer = new ManualResetEvent( false );
        private ManualResetEvent _shutDownServerCompleted = new ManualResetEvent( false );
        private volatile bool _isRunning;
        private string _pipeName;
        private Thread _thread;
        public event EventHandler<PipeServerEventArgs> MessageReceived;

        public string Name
        {
            get { return _pipeName; }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
        }

        protected virtual void onMessageReceived ( PipeServerEventArgs e )
        {
            if ( MessageReceived != null )
            {
                MessageReceived( this, e );
            }
        }

        public PipeServer ()
        {
        }

        private void pipeWorker ()
        {
            _isRunning = true;
            
            try
            {
                using ( NamedPipeServerStream pipeServer = new NamedPipeServerStream( _pipeName, PipeDirection.In, 4 ) )
                {                     
                    using ( StreamReader reader = new StreamReader( pipeServer ) )
                    {                   
                        while ( true )
                        {
                            if ( _shutDownServer.WaitOne( 500 ) )
                            {
                                break;
                            }

                            pipeServer.WaitForConnection();

                            onMessageReceived( new PipeServerEventArgs( reader.ReadLine(), null ) );

                            pipeServer.Disconnect();
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                if(!(ex is ThreadAbortException))
                {
                onMessageReceived(new PipeServerEventArgs(null, ex));
                }
            }
            finally
            {
                _isRunning = false;
                _shutDownServerCompleted.Set();
            }
        }


        public void Start (string pipeName)
        {
            if(_isRunning)
            {
                Stop();
            }

            _pipeName = pipeName;

            _shutDownServer.Reset();
            _shutDownServerCompleted.Reset();

            _thread = new Thread( new ThreadStart( pipeWorker ) );
            _thread.IsBackground = true;
            _thread.Start();
        }

        public void Stop ()
        {
            if ( _isRunning )
            {
                _shutDownServer.Set();
                bool completed = _shutDownServerCompleted.WaitOne(1000);
                try
                {
                    if ( !completed )
                        _thread.Abort();
                }
                catch { }
            }
        }
    }
}
