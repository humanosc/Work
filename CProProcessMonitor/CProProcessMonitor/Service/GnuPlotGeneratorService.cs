using CProProcessMonitor.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Service
{
    public class GnuPlotGeneratorService : IGnuPlotGeneratorService 
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

        private readonly IMainModel _model;

        public GnuPlotGeneratorService ( IMainModel model )
        {
            _model = model;
        }

        public void Generate ( GnuPlotDiagramType type )
        {
            string settingsTemplate = convertToSettingsTemplate( type );

            generate( settingsTemplate );
        }

        private void generate ( string settingsTemplate )
        {
            string gnuPlotDir = Path.GetFullPath( "gnuplot\\" );

            // load plot settings
            string plotSettingsPath = Path.Combine( gnuPlotDir, "plot_settings.txt" );
            //string title = ( string.IsNullOrEmpty( _processWindowTitle ) ? _logPath : _processWindowTitle ).Replace( "\\", "\\\\" );
            string plotSettings = settingsTemplate.Replace( "[log_path]", _model.LogPath.Replace( '\\', '/' ) )
                .Replace( "[title]", string.IsNullOrEmpty(_model.ProcessWindowTitle) ? "CPro Process Monitor" : _model.ProcessWindowTitle )
                .Replace( "[width]",  _model.DiagramWidth.ToString() )
                .Replace( "[height]", _model.DiagramHeight.ToString() );

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
            string destPngPath = _model.LogPath.Replace( ".log", ".png" );
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
