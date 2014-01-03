using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using XLib.General;

namespace CProProcessMonitor.View
{
    partial class AboutBox : Form, IAboutView
    {
        public event EventHandler EvLoad;
        public event EventHandler EvShow;
        public event EventHandler EvHide;
        public event EventHandler EvClose;

        public string Title
        {
            set { throw new NotImplementedException(); }
        }

        public string Version
        {
            set { this.labelVersion.Text = value; }
        }

        public string Description
        {
            set { this.textBoxDescription.Text = value; }
        }

        public string Product
        {
            set { this.labelProductName.Text = value; }
        }

        public string Copyright
        {
            set { this.labelCopyright.Text = value; }
        }

        public string Company
        {
            set { this.labelCompanyName.Text = value; }
        }

        public AboutBox()
        {
            InitializeComponent();
            Load += (o, e) => EvLoad.RaiseIfValid(this);
            Shown += (o, e) => EvShow.RaiseIfValid(this);
            FormClosed += (o, e) => EvClose.RaiseIfValid(this);
            VisibleChanged += (o, e) =>
            {
                if (!Visible)
                    EvHide.RaiseIfValid(this);
            };
        }
    }
}
