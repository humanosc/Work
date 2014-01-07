using CProProcessMonitor.Presenter;
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
    partial class AboutBox : GenericFormViewBase<AboutViewPresenter>, IAboutView
    {
        public string Title
        {
            set { Text = value; }
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

            StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
