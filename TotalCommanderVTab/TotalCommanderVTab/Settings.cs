using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XLib.General;
using XLib.Data.Settings;

namespace TotalCommanderVTab
{
    public class Settings : SettingsBase<Settings>
    {
        private const string CURRENT_VERSION = "1.0";

        public SettingsParameter<string> Version = new SettingsParameter<string>( "Version" );
        public SettingsParameter<string> LastSelectedDirectory = new SettingsParameter<string>( "LastSelectedDirectory" );
        public SettingsParameter<bool> OpenDirectoryInNewTab = new SettingsParameter<bool>( "OpenDirectoryInNewTab" );
        public SettingsParameter<bool> OpenDirectoryInSourceTab = new SettingsParameter<bool>( "OpenDirectoryInSourceTab" );
        //public SettingsParameter<double[]> DoubleArray2 = new SettingsParameter<double[]>( "DoubleArray2" );
        //public class ComplexSettings : ISettingsParameterGroup 
        //{
        //    public SettingsParameter<bool> IsValid = new SettingsParameter<bool>( "IsValid" );
        //    public SettingsParameter<double> Precision = new SettingsParameter<double>( "Precision" );

        //    #region ISettingsParameterGroup Members

        //    public void ReadParameter ( ISettingsScope scope )
        //    {
        //        scope.ReadParameter( IsValid );
        //        scope.ReadParameter( Precision );
        //    }

        //    public void WriteParameter ( ISettingsScope scope )
        //    {
        //        scope.WriteParameter( IsValid );
        //        scope.WriteParameter( Precision );
        //    }

        //    #endregion
        //}

        //public SettingsParameter<ComplexSettings> Complex = new SettingsParameter<ComplexSettings>( "ComplexSettings" );
        //public SettingsArrayParameter<ComplexSettings> ComplexArray = new SettingsArrayParameter<ComplexSettings>( "ComplexArray" );
        //public SettingsArrayParameter<Double> DoubleArray = new SettingsArrayParameter<Double>( "DoubleArray" );

        
        public Settings ()
        {
            Version.Value = CURRENT_VERSION;

            ParameterRegister.Register( Version );
            ParameterRegister.Register( LastSelectedDirectory );
            ParameterRegister.Register( OpenDirectoryInNewTab );
            ParameterRegister.Register( OpenDirectoryInSourceTab );
            //parameterRegister.RegisterParameter( Complex );
            //parameterRegister.RegisterParameter( ComplexArray );
            //parameterRegister.RegisterParameter( DoubleArray );
            //parameterRegister.RegisterParameter( DoubleArray2 );

            //DoubleArray.Value = new double[3];
            //DoubleArray.Value[0] = 1.1;
            //DoubleArray.Value[1] = 1.2;
            //DoubleArray.Value[2] = 1.3;

            //DoubleArray2.Value = new double[3];
            //DoubleArray2.Value[0] = 2.1;
            //DoubleArray2.Value[1] = 2.2;
            //DoubleArray2.Value[2] = 2.3;

            //Complex.Value = new ComplexSettings();
            //Complex.Value.IsValid.Value = true;
            //Complex.Value.Precision.Value = 0.5;

            //ComplexArray.Value = new ComplexSettings[3];
            //ComplexArray.Value[0] = new ComplexSettings();
            //ComplexArray.Value[0].IsValid.Value = true;
            //ComplexArray.Value[0].Precision.Value = 0.3;
            //ComplexArray.Value[1] = new ComplexSettings();
            //ComplexArray.Value[1].IsValid.Value = false;
            //ComplexArray.Value[1].Precision.Value = 0.2;
            //ComplexArray.Value[2] = new ComplexSettings();
            //ComplexArray.Value[2].IsValid.Value = true;
            //ComplexArray.Value[2].Precision.Value = 0.1;
        }
    }
}
