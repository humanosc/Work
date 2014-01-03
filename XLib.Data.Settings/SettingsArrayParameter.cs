using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XLib.Data.Settings
{
    public class SettingsArrayParameter<T> : SettingsParameter<T[]>, ISettingsParameterGroup
    {
        public static readonly Func<T> DefaultArrayItemCreationPolicy = SettingsParameter<T>.DefaultCreationPolicy;
        private Func<T> _arrayItemCreationPolicy;

        public SettingsArrayParameter ( string id ) : this(id, DefaultArrayItemCreationPolicy)
        {
        }

        public SettingsArrayParameter ( string id, Func<T> arrayItemCreationPolicy )
            : base( id )
        {
            _arrayItemCreationPolicy = arrayItemCreationPolicy;
        }

        public T this[int index]
        {
            get { return Value[index]; }
            set { Value[index] = value; }
        }

        public void ReadParameter ( ISettingsScope scope )
        {
            Value = new T[scope.Count];
            
            for ( int i = 0; i < Value.Length; i++ )
            {
                SettingsParameter<T> parameter = new SettingsParameter<T>( i.ToString() );
                parameter.Value = _arrayItemCreationPolicy();
                scope.ReadParameter( parameter );
                Value[i] = parameter.Value;
            }
        }

        public void WriteParameter ( ISettingsScope scope )
        {
            for ( int i = 0; i < Value.Length; i++ )
            {
                SettingsParameter<T> parameter = new SettingsParameter<T>( i.ToString() );
                parameter.Value = Value[i];
                scope.WriteParameter( parameter );
            }
        }
    }
}
