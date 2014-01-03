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
using System.Windows.Shapes;
using LPKFHistoryAssistent.General;

namespace LPKFHistoryAssistant
{
    /// <summary>
    /// Interaction logic for SettingsForm.xaml
    /// </summary>
    public partial class SettingsForm : Page
    {
        public SettingsForm ()
        {
            InitializeComponent();
        }
           
        private void _btOk_Click ( object sender, RoutedEventArgs e )
        {
            Settings.Instance.Nickname.Value = _txbNickname.Text;
            Settings.Instance.RssUrl.Value = _txbRssUrl.Text;
            Settings.Instance.UsePerforceIntegration.Value = _cbPerforceIntegration.IsChecked.GetValueOrDefault();
            Settings.Instance.Save( App.Instance.SettingsPath );

            MainWindow.Instance.SwitchView( MainWindow.ViewType.BugTrackForm );
        }

        private void Page_Loaded ( object sender, RoutedEventArgs e )
        {
            _txbNickname.Text = Settings.Instance.Nickname.Value;
            _txbRssUrl.Text = Settings.Instance.RssUrl.Value;
            _cbPerforceIntegration.IsChecked = Settings.Instance.UsePerforceIntegration.Value;

            _txbNickname.Focus();
        }
    }
}
