using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CProProcessMonitor
{
    public enum GnuPlotDiagramType
    {
        All,
        Cpu,
        Memory,
        ClrMemory
    }

    public static class GnuPlotGenerator
    {
        private static readonly string _plotsettingsTemplate = global::CProProcessMonitor.Properties.Resources.plotsettings_template;
        private static readonly string _plotsettingsTemplateCpu = global::CProProcessMonitor.Properties.Resources.plotsettings_cpu_template;
        private static readonly string _plotsettingsTemplateMemory = global::CProProcessMonitor.Properties.Resources.plotsettings_memory_template;
        private static readonly string _plotsettingsTemplateClrMemory = global::CProProcessMonitor.Properties.Resources.plotsettings_clrmemory_template;

        private static string convertToSettingsTemplate ( GnuPlotDiagramType type )
        {
            switch ( type )
            {
                case GnuPlotDiagramType.All:
                    return _plotsettingsTemplate;
                case GnuPlotDiagramType.Cpu:
                    return _plotsettingsTemplateCpu;
                case GnuPlotDiagramType.Memory:
                    return _plotsettingsTemplateMemory;
                case GnuPlotDiagramType.ClrMemory:
                    return _plotsettingsTemplateClrMemory;
            }

            return _plotsettingsTemplate;            
        }

        public static void Generate ( GnuPlotDiagramType type, string title, string logPath )
        {
            string settingsTemplate = convertToSettingsTemplate( type );

            generate( settingsTemplate, title, logPath );
        }

        private static void generate ( string settingsTemplate, string title, string logPath )
        {
            string gnuPlotDir = Path.GetFullPath( "gnuplot\\" );

            // load plot settings
            string plotSettingsPath = Path.Combine( gnuPlotDir, "plot_settings.txt" );
            //string title = ( string.IsNullOrEmpty( _processWindowTitle ) ? _logPath : _processWindowTitle ).Replace( "\\", "\\\\" );
            string plotSettings = settingsTemplate.Replace( "[log_path]", logPath.Replace( '\\', '/' ) )
                .Replace( "[title]", title )
                .Replace( "[width]", Settings.Instance.DiagramWidth.Value.ToString() )
                .Replace( "[height]", Settings.Instance.DiagramHeight.Value.ToString() );

            // write modified plot settings
            File.WriteAllText( plotSettingsPath, plotSettings );

            // launch gnu plot batch

            string gnuBatchPath = Path.Combine( gnuPlotDir, "plot.bat" );
            Process process = new Process();
            process.StartInfo.FileName = gnuBatchPath;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName( gnuBatchPath );
            process.Start();
            process.WaitForExit();

            // copy plot result to working directory
            string sourcePngPath = Path.Combine( gnuPlotDir, "plot.png" );
            string destPngPath = logPath.Replace( ".log", ".png" );
            if ( File.Exists( destPngPath ) )
            {
                File.Delete( destPngPath );
            }
            File.Copy( sourcePngPath, destPngPath );

            // show plot 
            Process.Start( destPngPath );
        }
    }
}
