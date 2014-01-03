using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Xml.Serialization;
using XLib.TotalCommander;
using XLib.General;

namespace TotalCommanderVTab
{
    public partial class MainForm : Form
    {
        private class InternalDataItem
        {
            public readonly string FullPath;
            public readonly string[] SubDirectories;

            public InternalDataItem ( string fullPath, string[] subDirectories )
            {
                FullPath = fullPath;
                SubDirectories = subDirectories;
            }

            public override string ToString ()
            {
                return SubDirectories[SubDirectories.Length - 1];
            }
        }

        private const int MAX_KEY_TIMEOUT = 1000;
        private string _settingsPath;
        private string _sourcePath;
        private string _subPath;
        private List<string> _subDirectories = new List<string>();
        private List<List<Regex>> _regExpressions = new List<List<Regex>>();
        private Stopwatch _stopWatch = new Stopwatch();
        private string _keyBuffer;

        public MainForm ()
        {
            InitializeComponent();
            _stopWatch.Start();
        }

        private void openNewTab ()
        {
            var items = lv_Directories.SelectedItems;
            if (( items == null ) || (items.Count == 0))
            {
                return;
            }

            var item = items[0].Tag as InternalDataItem;

            string subDestDir = "\\";
            string subSrcDir = "\\";

            Array.ForEach(item.SubDirectories, (dir) => subDestDir += dir + "\\");
	        _subDirectories.ForEach((dir) => subSrcDir += dir + "\\");

            string newTabPath = _sourcePath.Replace( subSrcDir, subDestDir );
        
            TotalCommander.TabCreation tabCreation = cb_OpenPathInNewTab.Checked ? TotalCommander.TabCreation.OpenNew : TotalCommander.TabCreation.OpenCurrent;
            TotalCommander.TabWindow tabWindow = cb_OpenPathInSourceTab.Checked ? TotalCommander.TabWindow.Source : TotalCommander.TabWindow.Destination;
            TotalCommander.Open( string.Empty, newTabPath, tabWindow, tabCreation );  
        }

        private void initialize ()
        {
            string[] args = CommandLineParser.GetCommandLineArgs( Environment.CommandLine );
            string pathArg = InternalCommandLineParser.GetPathArg( args );
            string[] regexArgs = InternalCommandLineParser.GetRegexArgs( args );
            bool debugArg = InternalCommandLineParser.GetDebugArg( args );
           
            if ( debugArg )
            {
                string tmpRegexArgs = String.Empty;
                Array.ForEach(regexArgs, (arg) => tmpRegexArgs += arg + " "); 
                MessageBox.Show( string.Format("Path:{0}\nRegex:{1}", pathArg,  tmpRegexArgs));
            }

            if ( args.Length < 2 ) // check parameter count
            {
                throw new Exception( "Invalid commandline parameter..." );
            }

            if ( pathArg == null )
            {
                throw new Exception( "Invalid commandline path parameter..." );
            }

            if ( regexArgs.Length == 0 )
            {
                throw new Exception( "Invalid commandline regex parameter..." );
            }

            _sourcePath = pathArg;
          

            // create regular expressions
            foreach(var regexArg in regexArgs)
            {
                List<Regex> tmpRegex = new List<Regex>();
                var regexPatterns = regexArg.Split( '|' );
                foreach ( var pattern in regexPatterns )
                {
                    tmpRegex.Add( new Regex( pattern.Trim('\"') ) );
                }
                _regExpressions.Add( tmpRegex );
            }

            // match regex patterns
            bool foundPattern = false;
            int lowIndex = int.MaxValue;
            foreach ( var regexList in _regExpressions )
            {
                foreach ( var regex in regexList )
                {
                    // match pattern
                    var tmpMatch = regex.Match( _sourcePath );
                    if ( ( tmpMatch != null ) && ( tmpMatch.Success ) )
                    {
                        // add matched sub directory to list                   
                        string tmpDir = tmpMatch.Value.Trim( '\\' );
                        _subDirectories.Add( tmpDir ); 

                        if ( tmpMatch.Index < lowIndex )
                        {
                            // update list
                            lowIndex = tmpMatch.Index;
                        }
          
                        foundPattern = true;
         
                    }                
                }

                if ( foundPattern ) // check if already one pattern was found
                {
                    break;
                }
            }

            if ( !foundPattern )
            {
                // all patterns failed
                throw new Exception( "Can't found regex pattern..." );
            }

            _subPath = _sourcePath.Substring( 0, lowIndex );

            string appPath = args[0].Trim( '"' );
            string appDir = Path.GetDirectoryName( appPath );
            _settingsPath = appDir + "\\vtab_settings.xml";
            
            Settings.Instance.Load( _settingsPath );
        }

        private bool isMatch (string path)
        {
            foreach ( var regexList in _regExpressions )
            {
                foreach ( var regex in regexList )
                {
                    string dirName = "\\" + Path.GetFileName( path );
                    if ( regex.IsMatch( dirName ) )
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool createListBoxItems ( string path, Stack<string> subDirectories )
        {
            if(!isMatch(path))
            {
                return false;
            }
            
            subDirectories.Push( Path.GetFileName( path ));

            var subPaths = Directory.GetDirectories( path );
            foreach ( var subPath in subPaths )
            {
                if ( !createListBoxItems( subPath, subDirectories ) )
                {
                    var internalData = new InternalDataItem( path, subDirectories.Reverse().ToArray() );
                    var lvItem = new ListViewItem();
                    lvItem.Text = internalData.ToString();
                    lvItem.Tag = internalData;
                    lv_Directories.Items.Add( lvItem );
                    subDirectories.Pop();
                    break;
                }
            }

            return true;
        }

        private void updateView ( )
        {
            lv_Directories.Items.Clear();
            lv_Directories.Columns.Clear();

            lv_Directories.Columns.Add( "Directories", lv_Directories.Width - 8 );

            // find subdirectories and fill listbox
            var directories = Directory.GetDirectories( _subPath );
            
            foreach ( var path in directories )
            {
                Stack<string> tmpSubDirectories = new Stack<string>();
                createListBoxItems( path, tmpSubDirectories );
            }

            bool foundItem = false;
            foreach ( ListViewItem item in lv_Directories.Items)
            {
                if ( item.Text == Settings.Instance.LastSelectedDirectory.Value )
                {
                    lv_Directories.FocusedItem = item;
                    item.Selected = true;
                    foundItem = true;
                    break;
                }
            }

            if ( !foundItem )
            {
                if ( lv_Directories.Items.Count > 0 )
                {
                    lv_Directories.Items[0].Selected = true;
                }
            }

            lv_Directories.Select();

            cb_OpenPathInNewTab.Checked = Settings.Instance.OpenDirectoryInNewTab.Value;
            cb_OpenPathInSourceTab.Checked = Settings.Instance.OpenDirectoryInSourceTab.Value;
        }

        private void updateSelection (char c)
        {
            if ( c == '.' )
            {
                return;
            }

            if ( _stopWatch.ElapsedMilliseconds > MAX_KEY_TIMEOUT )
            {
                _keyBuffer = String.Empty;
                _stopWatch.Reset();
                _stopWatch.Start();
            }

            _keyBuffer += c;

            for ( int i = 0; i < lv_Directories.Items.Count; i++ )
            {
                var item = lv_Directories.Items[i];
                string name = item.Text.Replace( ".", String.Empty ).ToLower();
                if ( name.StartsWith( _keyBuffer ) )
                {
                    item.Selected = true;
                    break;
                }
            }

            lv_Directories.Select();
        }

        private void MainForm_Load ( object sender, EventArgs e )
        {
            try
            {
                initialize();
                updateView();
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                Application.Exit();
            }
        }

        private void bt_Abort_Click ( object sender, EventArgs e )
        {
            Close();
        }

        private void bt_Ok_Click ( object sender, EventArgs e )
        {
            try
            {
                openNewTab();
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
            finally
            {
                Close();
            }
        }

        private void MainForm_KeyPress ( object sender, KeyPressEventArgs e )
        {
            e.Handled = false;
            updateSelection( e.KeyChar );   
        }

        private void MainForm_FormClosed ( object sender, FormClosedEventArgs e )
        {
            if ( _settingsPath != null )
            {
                Settings.Instance.Save( _settingsPath );
            }
        }

        private void cb_OpenPathInNewTab_CheckedChanged ( object sender, EventArgs e )
        {
            Settings.Instance.OpenDirectoryInNewTab.Value = cb_OpenPathInNewTab.Checked;
        }

        private void lv_Directories_SelectedIndexChanged ( object sender, EventArgs e )
        {
            if(lv_Directories.SelectedItems != null && lv_Directories.SelectedItems.Count > 0)
                Settings.Instance.LastSelectedDirectory.Value = lv_Directories.SelectedItems[0].Text;
        }

        private void cb_OpenPathInSourceTab_CheckedChanged ( object sender, EventArgs e )
        {
            Settings.Instance.OpenDirectoryInSourceTab.Value = cb_OpenPathInSourceTab.Checked;
        }
    }
}
