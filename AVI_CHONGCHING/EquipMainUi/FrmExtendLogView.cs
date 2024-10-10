using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EquipMainUi
{
    public partial class FrmExtendLogView : Form
    {
        public FrmExtendLogView()
        {
            InitializeComponent();
            Logger.Log.AddLogView(listView1);
        }

        private void FrmExtendLogView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Logger.Log.RemoveLogView(listView1);
        }
    }
}
