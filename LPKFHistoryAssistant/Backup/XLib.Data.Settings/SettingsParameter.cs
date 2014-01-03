using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XLib.General;

namespace XLib.Data.Settings
{
    public class SettingsParameter<T> : ISettingsParameter
    {
        public static readonly Func<T> DefaultCreationPolicy = () => default( T );
        private Func<T> _creationPolicy;

        public SettingsParameter ( string id, Func<T> creationPolicy )
        {
            _creationPolicy = creationPolicy;
            Id = id;
            Value = _creationPolicy();
        }

        public SettingsParameter ( string id )  : this(id, DefaultCreationPolicy)
        {
        }

        public object CreateDefaultObject ()
        {
            return _creationPolicy();
        }

        public string Id { get; set; }
        public T Value { get; set; }
        public object ObjectValue
        {
            get { return Value; }
            set { Value = (T)value; }
        }
        public Type ObjectType
        {
            get { return typeof( T ); }
        }
    }
}
