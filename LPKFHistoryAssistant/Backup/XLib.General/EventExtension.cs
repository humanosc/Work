using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XLib.General
{
    public static class EventExtension
    {
        public static void RaiseIfValid<T> ( this EventHandler<T> handler, object sender, T args ) where T : EventArgs
        {
            if ( handler != null )
            {
                handler( sender, args );
            }
        }
    }
}
