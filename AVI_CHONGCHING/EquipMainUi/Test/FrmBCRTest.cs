using EquipMainUi.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMainUi.Test
{
    public partial class FrmBCRTest : Form
    {
        private BCR_DM150 _bcr;
        public FrmBCRTest(BCR_DM150 bcr)
        {
            InitializeComponent();
            _bcr = bcr;
            UpdateConnBtn();

            cbPort.Items.Clear();
            foreach (var portName in SerialPort.GetPortNames())
            {
                try
                {
                    cbPort.Items.Add(portName);
                }
                catch (Exception)
                {

                }
            }
            cbPort.Text = _bcr.Port;
        }

        private void UpdateConnBtn()
        {
            btnConnect.Text = _bcr.IsOpen() ? "Disconnect" : "Connect";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (_bcr.IsOpen() == true)
                _bcr.Stop();
            else
            {
                _bcr.ReOpen(cbPort.Text);
            }

            UpdateConnBtn();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            _bcr.GetBarcode();

            for (int i = 0; i < 10; ++i)
            {
                Thread.Sleep(100);
                if (_bcr.IsReadValueError == false)
                    break;
            }
            lbLog.Items.Add(_bcr.IsReadValueError ? "Error:" + _bcr.ReadValue : "Read:" + _bcr.ReadValue);
        }
    }
}
