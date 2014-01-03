using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading;
using System.Diagnostics;

namespace LPKFHistoryAssistant
{
    public static class ClipboardEx
    {
        [System.Runtime.InteropServices.DllImport( "user32.dll" )]
        private static extern IntPtr CloseClipboard ();

        [System.Runtime.InteropServices.DllImport( "user32.dll" )]
        private static extern IntPtr GetOpenClipboardWindow ();

        [System.Runtime.InteropServices.DllImport( "user32.dll" )]
        private static extern int GetWindowText ( int hwnd, StringBuilder text, int count );

        [System.Runtime.InteropServices.DllImport( "user32.dll", SetLastError = true )]
        static extern uint GetWindowThreadProcessId ( IntPtr hWnd, out uint lpdwProcessId );

        private static string getOpenClipboardWindowText ()
        {
            IntPtr hwnd = GetOpenClipboardWindow();
            StringBuilder sb = new StringBuilder( 501 );
            GetWindowText( hwnd.ToInt32(), sb, 500 );
            return sb.ToString();
            // example:
            // skype_plugin_core_proxy_window: 02490E80
        }

        private static Process getProcessFromClipboardWindowText ( string clipboardWindowText )
        {
            int index = clipboardWindowText.IndexOf( ':' );
            if ( ( index == -1 ) || ( index + 1 >= clipboardWindowText.Length ) )
            {
                throw new Exception( "Can't find process by window title." );
            }
            string windowHandleHexStr = clipboardWindowText.Substring( index + 1 ).Trim();
            int windowHandle = int.Parse( windowHandleHexStr, System.Globalization.NumberStyles.HexNumber );
            IntPtr handle = new IntPtr( windowHandle );
            uint procId = 0;
            GetWindowThreadProcessId( handle, out procId );
            return Process.GetProcessById( (int)procId );
        }

        public static void Close ()
        {
            CloseClipboard();
        }

        public static void Clear ()
        {
            Clipboard.Clear();
        }

        private static void setDataObject ( string text, int retryCounter, int timeout )
        {
            Exception exception = null;
            for ( int i = 0; i < retryCounter; i++ )
            {
                try
                {
                    CloseClipboard();
                    Clipboard.Clear();
                    Clipboard.SetDataObject( text, true );
                    exception = null;
                    break;
                }
                catch ( Exception ex )
                {
                    exception = ex;
                    Thread.Sleep( timeout );
                }
            }

            if ( exception != null )
            {
                throw exception;
            }
        }

        public static void SetDataObject ( string text, int retryCounter, int timeout )
        {        
            while ( true )
            {
                try
                {
                    setDataObject( text, retryCounter, timeout );
                    return;
                }
                catch ( Exception ex )
                {
                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    // well - we reach this point when an other application is blocking the clipboard
                   
                    // for this reason we are trying to retrieve the window title which is blocking the clipboard
                    string clipBoardWindowText = getOpenClipboardWindowText();
                    string message = string.Format( "{0}\r\n\r\nClipboard locked by window:{1}", ex.Message, clipBoardWindowText );
                  
                    bool throwException = true;
                    // asking the user if the process should be killed?
                    var result = MessageBox.Show( string.Format( "{0}\r\nDo you want to kill this Application?", message ), "Error...", MessageBoxButton.YesNo, MessageBoxImage.Error );
                    if ( result == MessageBoxResult.Yes )
                    {
                        // kill the blocking process 
                        var process = getProcessFromClipboardWindowText( clipBoardWindowText );
                        process.Kill();
                        // reset the trow exception flag to retry setDataObject routine again
                        throwException = false;
                    }
                    
                    if(throwException)
                    {
                        throw new Exception( message );
                    }
                }
            }
        }

        public static void SetDataObjectSTA ( string text, int retryCounter, int timeout )
        {
            ManualResetEvent syncEvent = new ManualResetEvent( false );

            Exception exception = null;
            ThreadStart threadStart = () =>
            {
                try
                {
                    SetDataObject( text, retryCounter, timeout );
                }
                catch ( Exception ex )
                {
                    exception = ex;
                }
                finally
                {
                    syncEvent.Set();
                }
            };

            Thread clipboardThread = new Thread( threadStart );
            clipboardThread.SetApartmentState( ApartmentState.STA );
            clipboardThread.IsBackground = false;
            clipboardThread.Start();
            syncEvent.WaitOne();

            if ( exception != null )
            {
                throw exception;
            }
        }
    }
}
