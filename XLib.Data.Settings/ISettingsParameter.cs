using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XLib.Data.Settings
{
    public interface ISettingsParameter
    {
        string Id { get; set; }
        object ObjectValue { get; set; }
        Type ObjectType { get; }
        object CreateDefaultObject ();
    }
}
