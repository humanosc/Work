using CProProcessMonitor.Presenter;
using CProProcessMonitor.View;
using CProProcessMonitor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CProProcessMonitor.Service;

namespace CProProcessMonitor
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main ()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );

            MainForm mainForm = new MainForm(); 
            MainModel mainModel = new MainModel();
            SelectInstancePresenter selectInstancePresenter = new SelectInstancePresenter( new SelectInstanceForm(), new PerformanceCounterInstanceModel());
            IPerformanceCounterService performanceCounterService = new PerformanceCounterService();
            IPerformanceCounterInstanceInfoService performanceCounterInstanceInfoService = new PerformanceCounterInstanceInfoService();
            IPerformanceCounterInstanceSelectorService performanceCounterInstanceSelectorService = new PerformanceCounterInstanceSelectorService( selectInstancePresenter );
            ProcessMonitorService processMonitorService = new ProcessMonitorService( performanceCounterService, performanceCounterInstanceInfoService, performanceCounterInstanceSelectorService, mainModel);
            GnuPlotGeneratorService gnuPlotGenerator = new GnuPlotGeneratorService( mainModel );

            MainViewPresenter mainViewPresenter = new MainViewPresenter(mainForm, mainModel, processMonitorService, gnuPlotGenerator);

            Application.Run( );
        }
    }
}
