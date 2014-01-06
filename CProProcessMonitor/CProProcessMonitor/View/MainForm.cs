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

namespace CProProcessMonitor.View
{  
    public partial class MainForm : FormViewBase, IMainView 
    {
        public event EventHandler EvGenerateDiagram;
        public event EventHandler EvGenerateCpuDiagram;
        public event EventHandler EvGenerateMemoryDiagram;
        public event EventHandler EvGenerateClrMemoryDiagram;
        public event EventHandler EvOpenLogFile;
        public event EventHandler EvOpenLogFolder;
        public event EventHandler EvCleanupLogFolder;
        public event EventHandler EvAbout;
        public event EventHandler EvUpdateIntervalChanged;

        public string[] UpdateIntervals
        {
            set 
            {
                tscb_TimerInterval.Items.Clear();
                tscb_TimerInterval.Items.AddRange(value);
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

            tsm_GenerateDiagram.Click += (o, e) => EvGenerateDiagram.RaiseIfValid(this);
            tsm_GenerateCpuDiagram.Click += (o, e) => EvGenerateCpuDiagram.RaiseIfValid(this);
            tsm_GenerateMemoryDiagram.Click += (o, e) => EvGenerateMemoryDiagram.RaiseIfValid(this);
            tsm_GenerateClrMemoryDiagram.Click += (o, e) => EvGenerateClrMemoryDiagram.RaiseIfValid(this);
            tsm_ShowLog.Click += (o, e) => EvOpenLogFile.RaiseIfValid(this);
            tsm_OpenLogfolder.Click += (o, e) => EvOpenLogFolder.RaiseIfValid(this);
            tsm_CleanupLogfolder.Click += (o, e) => EvCleanupLogFolder.RaiseIfValid(this);
            tsm_About.Click += (o, e) => EvAbout.RaiseIfValid(this);
            tscb_TimerInterval.SelectedIndexChanged += (o, e) => EvUpdateIntervalChanged.RaiseIfValid(this);
        }

        public void Reset ()
        {
            CPU = 0.0;
            Memory = 0.0;
            ClrMemory = 0.0;
        }
       
      

   

        //private void ni_MainForm_MouseClick ( object sender, MouseEventArgs e )
        //{
        //    Show();
        //}

        //private void tsm_ShowMonitor_Click ( object sender, EventArgs e )
        //{
        //    Show();
        //}

        //private void tsm_HideMonitor_Click ( object sender, EventArgs e )
        //{
        //    Hide();
        //}

        //private void tsm_CloseMonitor_Click ( object sender, EventArgs e )
        //{
        //    Close();
        //}

     
        //private void ni_MainForm_MouseClick_1(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == System.Windows.Forms.MouseButtons.Left)
        //    {
        //        Visible = !Visible;
        //    }
        //}


        

        //private void tsm_About_Click(object sender, EventArgs e)
        //{
        //    AboutBox about = new AboutBox();
        //    about.StartPosition = FormStartPosition.CenterScreen;
        //    about.ShowDialog();
        //}

        //private void tscb_TimerInterval_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Settings.Instance.TimerResolutionId.Value = tscb_TimerInterval.SelectedIndex;
        //    tm_ProcessUpdate.Interval = ((IntervalEntry)tscb_TimerInterval.SelectedItem).Milliseconds;
        //    cms_MainForm.Close();
        //}
    }
}