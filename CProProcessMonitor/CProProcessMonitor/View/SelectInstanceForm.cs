using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XLib.General;

namespace CProProcessMonitor.View
{
    public partial class SelectInstanceForm : Form, ISelectInstanceView 
    {
        public event EventHandler EvLoad;
        public event EventHandler EvShow;
        public event EventHandler EvHide;
        public event EventHandler EvClose;
        public event EventHandler EvOk;
        public event EventHandler EvCancel;
        
        public SelectInstanceForm ()
        {
            InitializeComponent();

            Load += (o, e) => EvLoad.RaiseIfValid(this);
            Shown += (o, e) => EvShow.RaiseIfValid(this);
            FormClosed += (o, e) => EvClose.RaiseIfValid(this);
            VisibleChanged += (o, e) => {
                if (!Visible)
                    EvHide.RaiseIfValid(this);
            };
            bt_Ok.Click += (o, e) => EvOk.RaiseIfValid(this);
            bt_Cancel.Click += (o, e) => EvCancel.RaiseIfValid(this);
        }    
        
        public string[] Instances
        {
            set
            {
                lb_Instances.Items.Clear();
                lb_Instances.Items.AddRange(value);
            }
        }

        public int SelectedInstanceIndex
        {
            get { return lb_Instances.SelectedIndex; }
        }       
    }
}
