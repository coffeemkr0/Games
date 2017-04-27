using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LegendsOfKesmaiSurvival.WinFormsClient.Dialogs
{
    public partial class OptionsDialog : Form
    {
        public Utility.Config Config
        {
            get { return configBindingSource.DataSource as Utility.Config; }
            set { configBindingSource.DataSource = value; }
        }

        public OptionsDialog()
        {
            InitializeComponent();
        }
    }
}
