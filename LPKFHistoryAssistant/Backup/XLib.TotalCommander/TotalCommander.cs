using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace XLib.TotalCommander
{
    public static class TotalCommander
    {
        public enum TabWindow
        {
            Source,
            Destination
        }

        public enum TabCreation
        {
            OpenCurrent,
            OpenNew
        }

        private static string convertToArgument ( TabCreation tabCreation )
        {
            switch ( tabCreation )
            {
                case TabCreation.OpenCurrent: return string.Empty;
                case TabCreation.OpenNew: return "/T";
            }
            return string.Empty;
        }

        private static string convertToArgument ( TabWindow tabWindow )
        {
            switch ( tabWindow )
            {
                case TabWindow.Source: return "/L";
                case TabWindow.Destination: return "/R";
            }
            return string.Empty;
        }

        public static void Open ( string totalCommanderDirectory, string tabPath, TabWindow tabWindow, TabCreation tabCreation )
        {
            string tabWindowArg = convertToArgument( tabWindow );
            string newTabArg = convertToArgument( tabCreation );
            string paramters = String.Format( "/O {0} /S {1}=\"{2}\"", newTabArg, tabWindowArg, tabPath );

            var process = new Process();
            process.StartInfo.FileName = Path.Combine( totalCommanderDirectory, "TOTALCMD.EXE" );
            process.StartInfo.Arguments = paramters;
            process.Start();
        }
    }
}
