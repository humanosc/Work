using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XLib.Data.Settings
{
    public interface ISettingsParameterGroup
    {
        void ReadParameter ( ISettingsScope scope );
        void WriteParameter ( ISettingsScope scope );
    }
}
