﻿using CProProcessMonitor.Presenter;
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
            SelectInstancePresenter selectInstancePresenter = new SelectInstancePresenter( new SelectInstanceForm(), new InstanceModel());
            
            IPerformanceCounterService performanceCounterService = new PerformanceCounterService();
            IPerformanceCounterInstanceInfoService performanceCounterInstanceInfoService = new PerformanceCounterInstanceInfoService();
            IInstanceSelectorService instanceSelectorService = new InstanceSelectorService( selectInstancePresenter );
            IProcessMonitorService processMonitorService = new ProcessMonitorService( performanceCounterService, performanceCounterInstanceInfoService, instanceSelectorService, mainModel);
            IGnuPlotGeneratorService gnuPlotGenerator = new GnuPlotGeneratorService( mainModel );
            IMainModelSerializerService mainModelSerializerService = new MainModelSerializerService();
            ILogService logService = new LogService( mainModel );

            AboutViewPresenter aboutPresenter = new AboutViewPresenter(new AboutBox());

            MainViewPresenter mainViewPresenter = new MainViewPresenter(mainForm, mainModel, mainModelSerializerService, processMonitorService, logService, gnuPlotGenerator, aboutPresenter);

            Application.Run( mainForm );
        }
    }
}
