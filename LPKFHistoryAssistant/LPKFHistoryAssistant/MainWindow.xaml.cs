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
//using System.Windows.Shapes;


namespace LPKFHistoryAssistant
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _initialWindowTitle;
        private BugtrackForm _bugTrackForm;
        private SettingsForm _settingsForm;
        private HistoryForm _historyForm;

        private string WindowTitle
        {
            set { base.Title = string.Format( "{0} [{1}]", _initialWindowTitle, value ); }
            get { return base.Title; }
        }

        public static MainWindow Instance
        {
            get { return Application.Current.MainWindow as MainWindow; }
        }

        public enum ViewType
        {
            BugTrackForm,
            SettingsForm,
            HistoryForm
        }

        public void SwitchView (ViewType viewType)
        {
            Page page = null;
            switch ( viewType )
            {
                case ViewType.SettingsForm:
                    page = _settingsForm;
                    break;
                case ViewType.HistoryForm:
                    page = _historyForm;
                    break;
                default:
                    page = _bugTrackForm;
                    break;
            }
            pageTransitionControl.ShowPage( page );
        }

        private static void HideBoundingBox ( object root )
        {

            Control control = root as Control;

            if ( control != null )
                control.FocusVisualStyle = null;


            if ( root is DependencyObject )
            {

                foreach ( object child in LogicalTreeHelper.GetChildren( (DependencyObject)root ) )
                {

                    HideBoundingBox( child );

                }

            }

        }

        public MainWindow ()
        {
            InitializeComponent();

            HideBoundingBox( pageTransitionControl );

            _initialWindowTitle = Title;

            WindowTitle = App.Instance.HistoryPath;

            _bugTrackForm = new BugtrackForm();
            _settingsForm = new SettingsForm();
            _historyForm = new HistoryForm();

            SwitchView( ViewType.BugTrackForm );           
        } 
    }
}
