using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XLib.General;
using XLib.Data.Settings.Internal;

namespace XLib.Data.Settings
{
    public abstract class SettingsBase<T> : Singleton<T> where T : SettingsBase<T>, new()
    {
        private ISettingsManager _manager = new SettingsManager();
        public event EventHandler<EventArgs> Loaded;
        public event EventHandler<EventArgs> Saved;

        protected virtual void onLoaded ( EventArgs e )
        {
            Loaded.RaiseIfValid( this, e );
        }

        protected virtual void onSaved ( EventArgs e )
        {
            Saved.RaiseIfValid( this, e );
        }

        protected IParameterRegister ParameterRegister 
        {
            get { return _manager; }  
        }

        public SettingsBase ()
        {
        }

        public void Load ( string path )
        {
            _manager.Load( path );
            onLoaded( EventArgs.Empty );
        }

        public void Save ( string path )
        {
            _manager.Save( path );
            onSaved( EventArgs.Empty );
        }
    }
}
