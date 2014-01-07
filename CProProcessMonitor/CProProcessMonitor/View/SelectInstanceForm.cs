using CProProcessMonitor.Presenter;
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
    public partial class SelectInstanceForm : GenericFormViewBase<SelectInstancePresenter>, ISelectInstanceView
    {
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

        public SelectInstanceForm()
        {
            InitializeComponent();

            bt_Ok.Click += (o, e) => Presenter.OnOk();
            bt_Cancel.Click += (o, e) => Presenter.OnCancel();

            StartPosition = FormStartPosition.CenterScreen;
        }

        public string Category
        {
            set { Text = string.Format("Please select {0} instance...", value); }
        }
    }
}
