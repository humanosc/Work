using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using XLib.General;
using System.Threading;
using System.Text.RegularExpressions;

namespace LPKFHistoryAssistant
{
    /// <summary>
    /// Interaction logic for BugtrackForm.xaml
    /// </summary>
    public partial class BugtrackForm : Page
    {
        public BugtrackForm ()
        {
            InitializeComponent();
        }

        private void Window_Loaded ( object sender, RoutedEventArgs e )
        {
        }

        private void _btSettings_Click ( object sender, RoutedEventArgs e )
        {
            MainWindow.Instance.SwitchView( MainWindow.ViewType.SettingsForm );
        }   

        private void _btOk_Click ( object sender, RoutedEventArgs e )
        {
            if ( _txbTitle.Text.Length > 0 )
            {
                string title = _txbTitle.Text.TrimEnd( '\r', '\n' ); 
                if ( title.Length == 0 )
                {
                    App.Instance.ShowErrorMessage( "Please enter an valid Bugtrack Id or Entry Title..." );
                    _txbTitle.Focus();
                    _txbTitle.SelectAll();
                    return;
                }
                App.Instance.BugtrackEntryTitle = title;
                App.Instance.InputDataType = InputDataType.Entry;
            }
            else
            {
                string[] bugtrackIds = _txbIds.Text.Split( new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries );
                for ( int i = 0; i < bugtrackIds.Length; i++ )
                {
                    bugtrackIds[i] = bugtrackIds[i].Trim();
                }

                foreach ( var bugtrackId in bugtrackIds )
                {
                    var match = Regex.Match( bugtrackId, "^\\d+$" );
                    if ( !match.Success )
                    {
                        App.Instance.ShowErrorMessage( "Please enter an valid Bugtrack Id or Entry Title..." );
                        _txbIds.Focus();
                        _txbIds.SelectAll();
                        return;
                    }    
                }                

                App.Instance.BugtrackIds = bugtrackIds;
                App.Instance.InputDataType = InputDataType.Ids;
            }
            
            MainWindow.Instance.SwitchView( MainWindow.ViewType.HistoryForm );
        }

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            _txbIds.Focus();
            _txbIds.SelectAll();      
        }

        private void _txbId_TextChanged ( object sender, TextChangedEventArgs e )
        {
            _txbTitle.IsEnabled = ( _txbIds.Text.Length == 0 );              
        }

        private void _txbTitle_TextChanged ( object sender, TextChangedEventArgs e )
        {
            _txbIds.IsEnabled = ( _txbTitle.Text.Length == 0 );              
        }      
    }
}
