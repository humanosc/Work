using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XLib.Data.Settings
{
    public interface ISettingsScope
    {
        int Count { get; }
        void WriteParameter ( ISettingsParameter parameter );
        void ReadParameter ( ISettingsParameter paramter );
    }
}
