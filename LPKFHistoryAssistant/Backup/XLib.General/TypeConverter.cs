using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XLib.General
{
    public static class TypeConverter
    {
        private static Dictionary<string, Func<object, object>> _converters = new Dictionary<string, Func<object, object>>();
        private static StringBuilder _builder = new StringBuilder( 1024 );
        private const char _sepChar = ';';

        private static string GenericArrayToStringConverter<F> ( F[] fromValues )
        {
            _builder.Length = 0;
            for ( int i = 0; i < fromValues.Length; i++ )
            {
                string tempValue = ConvertTo<F, string>( fromValues[i] );
                _builder.Append( Uri.EscapeDataString( tempValue ) );
                if ( i < fromValues.Length - 1 )
                {
                    _builder.Append( _sepChar );
                }
            }
            return _builder.ToString();
        }
        private static T[] GenericStringToArrayConverter<T> ( string fromValue )
        {
            var fromValues = fromValue.Split( new[] { _sepChar }, StringSplitOptions.RemoveEmptyEntries );
            var toValues = new T[fromValues.Length];
            for ( int i = 0; i < fromValues.Length; i++ )
            {
                string tempValue = Uri.UnescapeDataString( fromValues[i] );
                toValues[i] = ConvertTo<string, T>( tempValue );
            }
            return toValues;
        }
        private static void RegisterGenericArrayToStringConverter<F> ()
        {
            RegisterConverter( typeof( F[] ), typeof( string ), ( o ) => GenericArrayToStringConverter<F>( (F[])o ) );
        }
        private static void RegisterGenericStringToArrayConverter<T> ()
        {
            RegisterConverter( typeof( string ), typeof( T[] ), ( o ) => GenericStringToArrayConverter<T>( (string)o ) );
        }

        static TypeConverter ()
        {
            RegisterConverter( typeof( string ), typeof( string ), ( o ) => o );
            RegisterConverter( typeof( string ), typeof( bool ), ( o ) => bool.Parse( o.ToString() ) );
            RegisterConverter( typeof( string ), typeof( byte ), ( o ) => byte.Parse( o.ToString() ) );
            RegisterConverter( typeof( string ), typeof( sbyte ), ( o ) => sbyte.Parse( o.ToString() ) );
            RegisterConverter( typeof( string ), typeof( short ), ( o ) => short.Parse( o.ToString() ) );
            RegisterConverter( typeof( string ), typeof( int ), ( o ) => int.Parse( o.ToString() ) );
            RegisterConverter( typeof( string ), typeof( long ), ( o ) => long.Parse( o.ToString() ) );
            RegisterConverter( typeof( string ), typeof( ushort ), ( o ) => ushort.Parse( o.ToString() ) );
            RegisterConverter( typeof( string ), typeof( uint ), ( o ) => uint.Parse( o.ToString() ) );
            RegisterConverter( typeof( string ), typeof( ulong ), ( o ) => ulong.Parse( o.ToString() ) );
            RegisterConverter( typeof( string ), typeof( float ), ( o ) => float.Parse( o.ToString() ) );
            RegisterConverter( typeof( string ), typeof( double ), ( o ) => double.Parse( o.ToString() ) );
            RegisterConverter( typeof( string ), typeof( decimal ), ( o ) => decimal.Parse( o.ToString() ) );
            RegisterConverter( typeof( string ), typeof( DateTime ), ( o ) => DateTime.Parse( o.ToString() ) );
            RegisterConverter( typeof( string ), typeof( TimeSpan ), ( o ) => TimeSpan.Parse( o.ToString() ) );

            RegisterConverter( typeof( bool ), typeof( string ), ( o ) => o.ToString() );
            RegisterConverter( typeof( byte ), typeof( string ), ( o ) => o.ToString() );
            RegisterConverter( typeof( sbyte ), typeof( string ), ( o ) => o.ToString() );
            RegisterConverter( typeof( short ), typeof( string ), ( o ) => o.ToString() );
            RegisterConverter( typeof( int ), typeof( string ), ( o ) => o.ToString() );
            RegisterConverter( typeof( long ), typeof( string ), ( o ) => o.ToString() );
            RegisterConverter( typeof( ushort ), typeof( string ), ( o ) => o.ToString() );
            RegisterConverter( typeof( uint ), typeof( string ), ( o ) => o.ToString() );
            RegisterConverter( typeof( ulong ), typeof( string ), ( o ) => o.ToString() );
            RegisterConverter( typeof( float ), typeof( string ), ( o ) => o.ToString() );
            RegisterConverter( typeof( double ), typeof( string ), ( o ) => o.ToString() );
            RegisterConverter( typeof( decimal ), typeof( string ), ( o ) => o.ToString() );
            RegisterConverter( typeof( DateTime ), typeof( string ), ( o ) => o.ToString() );
            RegisterConverter( typeof( TimeSpan ), typeof( string ), ( o ) => o.ToString() );

            RegisterConverter( typeof( byte[] ), typeof( string ), ( o ) => Convert.ToBase64String( (byte[])o ) );
            RegisterConverter( typeof( string ), typeof( byte[] ), ( o ) => Convert.FromBase64String( o.ToString() ) );

            RegisterGenericArrayToStringConverter<string>();
            RegisterGenericArrayToStringConverter<bool>();
            RegisterGenericArrayToStringConverter<sbyte>();
            RegisterGenericArrayToStringConverter<short>();
            RegisterGenericArrayToStringConverter<int>();
            RegisterGenericArrayToStringConverter<long>();
            RegisterGenericArrayToStringConverter<ushort>();
            RegisterGenericArrayToStringConverter<uint>();
            RegisterGenericArrayToStringConverter<ulong>();
            RegisterGenericArrayToStringConverter<float>();
            RegisterGenericArrayToStringConverter<double>();
            RegisterGenericArrayToStringConverter<decimal>();
            RegisterGenericArrayToStringConverter<DateTime>();
            RegisterGenericArrayToStringConverter<TimeSpan>();

            RegisterGenericStringToArrayConverter<string>();
            RegisterGenericStringToArrayConverter<bool>();
            RegisterGenericStringToArrayConverter<sbyte>();
            RegisterGenericStringToArrayConverter<short>();
            RegisterGenericStringToArrayConverter<int>();
            RegisterGenericStringToArrayConverter<long>();
            RegisterGenericStringToArrayConverter<ushort>();
            RegisterGenericStringToArrayConverter<uint>();
            RegisterGenericStringToArrayConverter<ulong>();
            RegisterGenericStringToArrayConverter<float>();
            RegisterGenericStringToArrayConverter<double>();
            RegisterGenericStringToArrayConverter<decimal>();
            RegisterGenericStringToArrayConverter<DateTime>();
            RegisterGenericStringToArrayConverter<TimeSpan>();
        }
        private static string BuildConverterKey ( Type fromType, Type toType )
        {
            return fromType.ToString() + "-" + toType.ToString();
        }
        public static void RegisterConverter ( Type fromType, Type toType, Func<object, object> converter )
        {
            _converters.Add( BuildConverterKey( fromType, toType ), converter );
        }
        public static void UnregisterConverter ( Type fromType, Type toType )
        {
            _converters.Remove( BuildConverterKey( fromType, toType ) );
        }
        public static T ConvertTo<F, T> ( F fromValue )
        {
            T toTalue = default( T );
            Func<object, object> converter = null;
            _converters.TryGetValue( BuildConverterKey( typeof( F ), typeof( T ) ), out converter );
            if ( converter != null )
            {
                toTalue = (T)converter( fromValue );
            }
            return toTalue;
        }
        public static object ConvertTo ( object fromObj, Type toType )
        {
            if ( fromObj == null )
            {
                return null;
            }

            object toObj = null;
            Func<object, object> converter = null;
            _converters.TryGetValue( BuildConverterKey( fromObj.GetType(), toType ), out converter );
            if ( converter != null )
            {
                toObj = converter( fromObj );
            }
            return toObj;
        }
    }

}
