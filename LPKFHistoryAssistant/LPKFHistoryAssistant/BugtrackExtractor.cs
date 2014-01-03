using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Xml.XPath;
using System.IO;

namespace LPKFHistoryAssistant
{
    class RefreshCompletedEventArgs : EventArgs
    {
        public readonly bool Cancelled;
        public Exception Error;
        public RefreshCompletedEventArgs ( bool cancelled, Exception error )
        {
            Cancelled = cancelled;
            Error = error;
        }
    }

    class BugtrackExtractor
    {
        private WebClient _webClient;
        private XPathNavigator _navigator = null;
        private ManualResetEvent _refreshCompleted = new ManualResetEvent( true );
        private object _syncObject = new object();
        private void executeSynchronized ( Action action )
        {
            lock ( _syncObject )
            {
                action();
            }
        }

        public event EventHandler<RefreshCompletedEventArgs> RefreshCompleted;
        protected virtual void onRefreshCompletetd ( RefreshCompletedEventArgs e )
        {
            if ( RefreshCompleted != null )
            {
                RefreshCompleted( this, e );
            }
        }

        public string User { get; set; }
        public string Password { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string Url { get; set; }
 
        public BugtrackExtractor () : this (string.Empty, string.Empty, string.Empty, false)
        {
        }

        public BugtrackExtractor (string url, string user, string password, bool useDefaultCredentials)
        {
            _webClient = new WebClient();
            _webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler( _webClient_DownloadStringCompleted );
            User = user;
            Password = password;
            UseDefaultCredentials = useDefaultCredentials;
            Url = url;
        }

        private void updateNavigator ( string buffer )
        {
            using ( StringReader reader = new StringReader( buffer ) )
            {
                var doc = new XPathDocument( reader );
                _navigator = doc.CreateNavigator();
            }
        }

        void _webClient_DownloadStringCompleted ( object sender, DownloadStringCompletedEventArgs e )
        {
            if ( e.Error == null )
            {
                executeSynchronized( () => updateNavigator( e.Result ) );
            }
            
            _refreshCompleted.Set();

            onRefreshCompletetd( new RefreshCompletedEventArgs( e.Cancelled, e.Error ) );
        }

        public void Refresh ()
        {
            _webClient.UseDefaultCredentials = UseDefaultCredentials;
            _webClient.Credentials = new NetworkCredential( User, Password );
            _webClient.Encoding = Encoding.UTF8;
            string buffer = _webClient.DownloadString( Url );
            executeSynchronized( () => updateNavigator( buffer ) );
        }

        public void RefreshAsync ()
        {
            _refreshCompleted.WaitOne();
            _webClient.UseDefaultCredentials = UseDefaultCredentials;
            _webClient.Credentials = new NetworkCredential( User, Password );
            _webClient.Encoding = Encoding.UTF8;
            _refreshCompleted.Reset();
            _webClient.DownloadStringAsync( new Uri(Url) );                        
        }

        public string ExtractTitle ( string id )
        {
            string title = null;

            Action action = () =>
            {
                if ( _navigator != null )
                {
                    string query = string.Format( "/rss/channel/item[guid={0}]/title", id );
                    var node = _navigator.SelectSingleNode( query );
                    if ( node != null )
                    {
                        title = node.Value;
                    }
                }
            };

            executeSynchronized( action );
            
            //title = node.Value;
            return title;

        }
    }
}
