using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DitSharedMemoryPacket;

namespace DitSharedMemoryTester
{
    public partial class FrmMain : Form
    {
        private ShardMem _shardMem = new ShardMem();
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            _shardMem.Name = txtShName.Text;
            _shardMem.Size = (int)nudShSize.Value;
            _shardMem.Open();
            btnCreate.Enabled = false;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            int value = 0;
            int addr = (int)nudShAddress.Value;
            value = _shardMem.ReadInt32(addr);
            txtRead.Text = value.ToString();
        }
        private void btnWrite_Click(object sender, EventArgs e)
        {
            int value = 0;
            int addr = (int)nudShAddress.Value;

            if (int.TryParse(txtWrite.Text, out value))
            {
                _shardMem.Write(addr, value);
            }
        }

        private void tmrShow_Tick(object sender, EventArgs e)
        {

        }
    }
}
