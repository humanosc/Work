using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CProProcessMonitor.Model;

namespace CProProcessMonitor.Service
{
    public interface IMainModelSerializerService
    {
        void Serialize ( string path, IMainModel model );
        void Deserialize ( string path, IMainModel model );
    }
}
