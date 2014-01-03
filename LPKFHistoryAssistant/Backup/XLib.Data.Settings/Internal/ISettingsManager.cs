using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XLib.Data.Settings.Internal
{
    internal interface ISettingsManager : IParameterRegister
    {
        void Load ( string path );
        void Save ( string path );
        void Reset ();
    }
}
