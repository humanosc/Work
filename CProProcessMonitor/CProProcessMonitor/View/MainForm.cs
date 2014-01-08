using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using XLib.Data.Settings;
using XLib.General;
using CProProcessMonitor.Presenter;

namespace CProProcessMonitor.View
{
    public partial class MainForm : GenericFormViewBase<MainViewPresenter>, IMainView 
    {      
        public string[] UpdateIntervals
        {
            set 
            {
                tscb_TimerInterval.Items.Clear();
                tscb_TimerInterval.Items.AddRange(value);
            }
        }

        public string StateText
        {
            set
            {
                tssl_State.Text = value;               
            }
        }

        public string Title
        {
            set { Text = value; }
        }

        public int SelectedUpdateIntervalIndex
        {
            set { tscb_TimerInterval.SelectedIndex = value; }
            get { return tscb_TimerInterval.SelectedIndex; }
        }

        public double CPU
        {
            set
            {
                string cpuStr = value.ToString("0.00", CultureInfo.InvariantCulture );           
                lbl_CPU.Text = cpuStr + " %";
            }
        }

        public double Memory
        {
            set
            {
                string memoryStr = value.ToString( "0.00", CultureInfo.InvariantCulture );           
                lbl_Memory.Text = memoryStr + " MB";
            }
        }

        public double ClrMemory
        {
            set 
            {
                string clrmemoryStr = value.ToString( "0.00", CultureInfo.InvariantCulture );
                lbl_CLRMemory.Text = clrmemoryStr + " MB";    
            }
        }

        public MainForm ()
        {
            InitializeComponent();

            ni_MainForm.MouseClick += (o, e) =>
                  {
                      if (e.Button == System.Windows.Forms.MouseButtons.Left)
                      {
                          if (Visible)
                          {
                              Presenter.OnHide();
                          }
                          else
                          {
                              Presenter.OnShow();
                          }
                      }
                  };

            tsm_ShowMonitor.Click += (o, e) => Presenter.OnShow();
            tsm_HideMonitor.Click += (o, e) => Presenter.OnHide();
            tsm_CloseMonitor.Click += (o, e) => Presenter.OnClose();
            tsm_GenerateDiagram.Click += (o, e) => Presenter.OnGenerateDiagram();
            tsm_GenerateCpuDiagram.Click += (o, e) => Presenter.OnGenerateCpuDiagram();
            tsm_GenerateMemoryDiagram.Click += (o, e) => Presenter.OnGenerateMemoryDiagram();
            tsm_GenerateClrMemoryDiagram.Click += (o, e) => Presenter.OnGenerateClrMemoryDiagram();
            tsm_ShowLog.Click += (o, e) => Presenter.OnOpenLogFile();
            tsm_OpenLogfolder.Click += (o, e) => Presenter.OnOpenLogFolder();
            tsm_CleanupLogfolder.Click += (o, e) => Presenter.OnCleanupLogFolder();
            tsm_ClearLog.Click += (o, e) => Presenter.OnClearLog();
            tsm_About.Click += (o, e) => Presenter.OnAbout();
            tscb_TimerInterval.SelectedIndexChanged += (o, e) =>
                {
                    Presenter.OnUpdateIntervalChanged();
                    cms_MainForm.Close();
                };

           
        }

        public void Reset ()
        {
            CPU = 0.0;
            Memory = 0.0;
            ClrMemory = 0.0;
            StateText = string.Empty;
        }     
    }
}