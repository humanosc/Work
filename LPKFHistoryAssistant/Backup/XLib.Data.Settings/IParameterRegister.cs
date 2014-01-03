using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XLib.Data.Settings
{
    public interface IParameterRegister
    {
        void Register ( ISettingsParameter parameter );
        void Unregister ( ISettingsParameter parameter );
    }
}
