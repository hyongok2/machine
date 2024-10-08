using EquipMainUi.Monitor;
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

namespace EquipMainUi
{
    public delegate void DataEvent(string str);

    public partial class FrmRFIDTest : Form
    {
        private RFIDController _rfid;
        public FrmRFIDTest(RFIDController rfid)
        {
            InitializeComponent();
            _rfid = rfid;
            _rfid.dataReceive += LogAppendLine;
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
            cbPort.Text = _rfid.Port;
        }

        private void LogAppendLine(string str)
        {
            lbLog.Items.Add(str);
        }

        private void UpdateConnBtn()
        {
            btnConnect.Text = _rfid.IsOpen() ? "Disconnect" : "Connect";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (_rfid.IsOpen() == true)
                _rfid.Stop();
            else
            {
                _rfid.ReOpen(cbPort.Text);
            }

            UpdateConnBtn();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            _rfid.ScanTagCmd(RFIDCmd.READER1);
        }
        private void btnScan2_Click(object sender, EventArgs e)
        {
            _rfid.ScanTagCmd(RFIDCmd.READER2);
        }
    }
}
