using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dit.Framework.Net.AsSocket;

namespace AsNetTester
{
    public partial class FrmClinet : Form
    {
        private AsSocketClient _client;
        public FrmClinet()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _client = new AsSocketClient(0);
            _client.OnConnet += new AsSocketConnectEventHandler(Client_OnConnet);
            _client.OnClose += new AsSocketCloseEventHandler(Client_OnClose);
            _client.OnError += new AsSocketErrorEventHandler(Client_OnError);
            _client.OnReceive += new AsSocketReceiveEventHandler(Client_OnReceive);

            if (_client.Connect(txtIp.Text, int.Parse(txtPort.Text)) == true)
            { 
            }
            else
            {
                AddLog("접속 실패");
            }
        }
        public void AddLog(string str)
        {
            txtRecv.AppendText(Environment.NewLine);
            txtRecv.AppendText(DateTime.Now.ToString("HH:mm:ss") + " " + str);
        }
        public void Client_OnReceive(object sender, AsSocketReceiveEventArgs e)
        {
            string recv = Encoding.Unicode.GetString(e.ReceiveData);
            AddLog(string.Format("메시지 수신 = {0}", recv));
        }
        public void Client_OnError(object sender, AsSocketErrorEventArgs e)
        {   
            AddLog("ERROR 발생");
        }
        public void Client_OnClose(object sender, AsSocketConnectionEventArgs e)
        {
            AddLog("종료 발생");
        }
        public void Client_OnConnet(object sender, AsSocketConnectionEventArgs e)
        {
            AddLog("접속 발생");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(txtSend.Text);
            _client.Send(bytes);
        }
    }
}
