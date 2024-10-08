using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EquipSimulator
{
    public partial class FrmPcMonitor : Form
    {
        public FrmPcMonitor()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {   
            this.Hide();

            e.Cancel = true;
            base.OnClosing(e);
        }
    }
}
