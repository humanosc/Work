using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CProProcessMonitor.Model
{
    public class MainModel : IMainModel
    {
        public string ProcessWindowTitle
        {
            get;
            set;
        }

        public string LogDirPath
        {
            get;
            set;
        }

        public string LogPath
        {
            get;
            set;
        }        

        public string ProcessName
        {
            get;
            set;
        }

        public int WindowTop
        {
            get;
            set;
        }

        public int WindowLeft
        {
            get;
            set;
        }

        public int TimerResolutionId
        {
            get;
            set;
        }

        public int DiagramWidth
        {
            get;
            set;
        }

        public int DiagramHeight
        {
            get;
            set;
        }       
    }
}
