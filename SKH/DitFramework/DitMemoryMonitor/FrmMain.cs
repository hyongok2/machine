using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dit.Framework.PLC;

namespace DitMemoryMonitor
{
    public partial class FrmMain : Form
    {
        VirtualShare _plc = null;
        public FrmMain()
        {
            InitializeComponent();
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            _plc = new VirtualShare("DIT.PLC.HSMS_MEM.S", 102400);
            _plc.Open();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            PlcAddr addr = PlcAddr.Parsing(txtAddr.Text);
            txtResult.Text = _plc.GetBit(addr).ToString();
        }
    }
}
