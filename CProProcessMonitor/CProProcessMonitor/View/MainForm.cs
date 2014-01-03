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
    public partial class MainForm : Form, IMainView 
    {
        public event EventHandler EvLoad;
        public event EventHandler EvShow;
        public event EventHandler EvHide;
        public event EventHandler EvClose;
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

        public int SelectedUpdateIntervalIndex
        {
            get { return tscb_TimerInterval.SelectedIndex; }
        }

        public double CPU
        {
            set
            {
                string cpuStr = value.ToString("0.00", _cultureInfo);           
                lbl_CPU.Text = cpuStr + " %";
            }
        }

        public double Memory
        {
            set
            {
                string memoryStr = value.ToString("0.00", _cultureInfo);           
                lbl_Memory.Text = memoryStr + " MB";
            }
        }

        public double ClrMemory
        {
            set 
            {
                string clrmemoryStr = value.ToString("0.00", _cultureInfo);
                lbl_CLRMemory.Text = clrmemoryStr + " MB";    
            }
        }

        public void Reset()
        {
            CPU = 0.0;
            Memory = 0.0;
            ClrMemory = 0.0;
        }

        
        private static readonly string _settingsPath = "Settings.xml";
   
        private static int _processorCount = Environment.ProcessorCount;
        private static readonly CultureInfo _cultureInfo = CultureInfo.InvariantCulture; 
        private readonly PerformanceCounterManager _perfCounters = new PerformanceCounterManager();
        private StreamWriter _writer;
        private string _logPath;
        private string _logDirPath;
        private string _processWindowTitle;


        struct IntervalEntry
        {
            public readonly string Text;
            public int Milliseconds;
            
            public IntervalEntry( string text, int milliseconds )
            {
                
                Text = text;
                Milliseconds = milliseconds;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private static StreamWriter createLogStream ( string logPath )
        {
            StreamWriter writer = new StreamWriter( new FileStream( logPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, 64 ) );
            writer.WriteLine( "Timestamp\tCPU\tMemory (MB)\tCLR-Memory (MB)" );
            writer.AutoFlush = true;
            return writer;
        }

        public MainForm ()
        {
            InitializeComponent();

            Load += (o, e) => EvLoad.RaiseIfValid(this);
            Shown += (o, e) => EvShow.RaiseIfValid(this);
            FormClosed += (o, e) => EvClose.RaiseIfValid(this);
            VisibleChanged += (o, e) =>
            {
                if (!Visible)
                    EvHide.RaiseIfValid(this);
            };

            tsm_GenerateDiagram.Click += (o, e) => EvGenerateDiagram.RaiseIfValid(this);
            tsm_GenerateCpuDiagram.Click += (o, e) => EvGenerateCpuDiagram.RaiseIfValid(this);
            tsm_GenerateMemoryDiagram.Click += (o, e) => EvGenerateMemoryDiagram.RaiseIfValid(this);
            tsm_GenerateClrMemoryDiagram.Click += (o, e) => EvGenerateClrMemoryDiagram.RaiseIfValid(this);
            tsm_ShowLog.Click += (o, e) => EvOpenLogFile.RaiseIfValid(this);
            tsm_OpenLogfolder.Click += (o, e) => EvOpenLogFolder.RaiseIfValid(this);
            tsm_CleanupLogfolder.Click += (o, e) => EvCleanupLogFolder.RaiseIfValid(this);
            tsm_About.Click += (o, e) => EvAbout.RaiseIfValid(this);
            tscb_TimerInterval.SelectedIndexChanged += (o, e) => EvUpdateIntervalChanged.RaiseIfValid(this);

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            Text = string.Format("CPro Process Monitor v{0}.{1}", version.Major, version.Minor);

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // initialize logging
            _logDirPath = Path.GetFullPath("Log");
            if (!Directory.Exists( _logDirPath ))
            {
                Directory.CreateDirectory( _logDirPath );
            }

            _logPath = Path.Combine(_logDirPath, string.Format("CPro Process Monitor ({0}).log", DateTime.Now.ToString("ddMMyy-hhmmss")));
            _writer = createLogStream( _logPath );

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // load settings and setup databinding
            Settings.Instance.Load( _settingsPath );
            DataBindings.Add( new Binding( "Top", Settings.Instance.WindowTop, "Value" ) );
            DataBindings.Add( new Binding( "Left", Settings.Instance.WindowLeft, "Value" ) );

            tscb_TimerInterval.Items.Add( new IntervalEntry("Very very high resolution ~ 1 s", 1000));
            tscb_TimerInterval.Items.Add( new IntervalEntry("Very high resolution ~ 5 s", 5000));
            tscb_TimerInterval.Items.Add( new IntervalEntry("High resolution ~ 10 s", 10000));
            tscb_TimerInterval.Items.Add( new IntervalEntry("Standard resolution ~ 30 s", 30000));
            tscb_TimerInterval.Items.Add( new IntervalEntry("Low resolution ~ 1 m", 60000));
            tscb_TimerInterval.Items.Add( new IntervalEntry( "Very low resolution ~ 5 m", 300000 ) );
            tscb_TimerInterval.Items.Add( new IntervalEntry( "Very very low resolution ~ 10 m", 600000 ) );
            tscb_TimerInterval.SelectedIndex = Settings.Instance.TimerResolutionId.Value;

            tm_ProcessUpdate.Start();
        }

        protected override void OnClosed ( EventArgs e )
        {
            _writer.Close();

            foreach ( Binding b in DataBindings )
            {
                b.WriteValue();
            }

            Settings.Instance.Save( _settingsPath );

            base.OnClosed( e );
        }
       
      

        private void tm_ProcessUpdate_Tick ( object sender, EventArgs e )
        {
            var processes = Process.GetProcesses();
            var process = processes.FirstOrDefault( p => p.ProcessName.StartsWith( Settings.Instance.ProcessName.Value ) );
            if ( process == null )
            {
                _perfCounters.Destroy();
                Reset();
                return;
            }

            _processWindowTitle = process.MainWindowTitle;

            try
            {
                if (!_perfCounters.IsInitialized)
                {
                    _perfCounters.Initialize( process.ProcessName );
                }                

                float cpu = _perfCounters.GetNextCpuValue() / _processorCount;
                float memory = _perfCounters.GetNextPrivateBytesValue() / 1024 / 1024;
                float clrmemory = _perfCounters.GetNextClrBytesValue() / 1024 / 1024;

                string cpuStr = cpu.ToString( "0.00", _cultureInfo );
                string memoryStr = memory.ToString( "0.00", _cultureInfo );
                string clrmemoryStr = clrmemory.ToString( "0.00", _cultureInfo );

                lbl_CPU.Text = cpuStr + " %";
                lbl_Memory.Text = memoryStr + " MB";
                lbl_CLRMemory.Text = clrmemoryStr + " MB";

                _writer.WriteLine( string.Format( "{0}\t{1}\t{2}\t{3}", DateTime.Now.ToString( _cultureInfo ), cpuStr, memoryStr, clrmemoryStr ) );
            }
            catch
            {
                _perfCounters.Destroy();
                resetView();
            }            
        }

        private void ni_MainForm_MouseClick ( object sender, MouseEventArgs e )
        {
            Show();
        }

        private void tsm_ShowMonitor_Click ( object sender, EventArgs e )
        {
            Show();
        }

        private void tsm_HideMonitor_Click ( object sender, EventArgs e )
        {
            Hide();
        }

        private void tsm_CloseMonitor_Click ( object sender, EventArgs e )
        {
            Close();
        }

     

        private void tsm_GenerateDiagram_Click(object sender, System.EventArgs e)
        {
            generateDiagram(_plotsettingsTemplate);
        }

        private void ni_MainForm_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Visible = !Visible;
            }
        }

        private void tsm_ShowLog_Click(object sender, EventArgs e)
        {
            Process.Start( _logPath );
        }

        private void tsm_OpenLogfolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", _logDirPath);
        }

        private void tsm_CleanupLogfolder_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do really want to delete all unused files in the Log directory?", "Question...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string[] filePaths = Directory.GetFiles(_logDirPath);
                foreach (var path in filePaths)
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch
                    {
                    }
                }            
            }
        }

        private void tsm_GenerateCpuDiagram_Click(object sender, EventArgs e)
        {
            generateDiagram(_plotsettingsTemplateCpu);
        }

        private void tsm_GenerateMemoryDiagram_Click(object sender, EventArgs e)
        {
            generateDiagram(_plotsettingsTemplateMemory);
        }

        private void tsm_GenerateClrMemoryDiagram_Click(object sender, EventArgs e)
        {
            generateDiagram(_plotsettingsTemplateClrMemory);
        }

        private void tsm_About_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.StartPosition = FormStartPosition.CenterScreen;
            about.ShowDialog();
        }

        private void tscb_TimerInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Instance.TimerResolutionId.Value = tscb_TimerInterval.SelectedIndex;
            tm_ProcessUpdate.Interval = ((IntervalEntry)tscb_TimerInterval.SelectedItem).Milliseconds;
            cms_MainForm.Close();
        }

        private void tsm_ClearLog_Click ( object sender, EventArgs e )
        {
            // close current log stream
            _writer.Close();
            // create new log stream ( empty file )
            _writer = createLogStream( _logPath );
        }




       
    }
}