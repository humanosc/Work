using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XLib.General
{
    public class Singleton<T> where T : Singleton<T>, new() 
    {
        private static class Lazy
        {
            internal static readonly T Instance = new T();

            static Lazy ()
            {
            }
        }

        public static T Instance
        {
            get
            {
                return Lazy.Instance;
            }
        }
    }
}
