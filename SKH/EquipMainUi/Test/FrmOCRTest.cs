using EquipMainUi.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMainUi.Test
{
    public partial class FrmOCRTest : Form
    {
        private OCR_IS1741 _ocr;
        public FrmOCRTest(OCR_IS1741 ocr, string ip, int port)
        {
            InitializeComponent();
            _ocr = ocr;
            UpdateConnBtn();
            txtIP.Text = ip;
            txtPort.Text = port.ToString();
        }
        private void UpdateConnBtn()
        {
            btnConnect.Text = _ocr.IsConnected ? "Disconnect" : "Connect";
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (_ocr.IsConnected == true)
                _ocr.Close();
            else
            {
                IPAddress ip;
                int port;

                if (IPAddress.TryParse(txtIP.Text, out ip) == false
                    || int.TryParse(txtPort.Text, out port) == false)
                {
                    MessageBox.Show("입력데이터 이상");
                    txtIP.Text = "";
                    txtPort.Text = "";
                    return;
                }

                _ocr.Open(ip.ToString(), port);
            }

            UpdateConnBtn();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            _ocr.Read();
            for (int i = 0; i < 10; ++i)
            {
                Thread.Sleep(100);
                if (_ocr.IsReadComplete == true)
                    break;
            }
            lbLog.Items.Add(!_ocr.IsReadComplete ? "Error:" + _ocr.Readvalue : "Read:" + _ocr.Readvalue);
        }
    }
}
