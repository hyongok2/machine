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
    public partial class FrmServer : Form
    {
        private int _iPos = 0;
        private AsSocketServer _server;
        private List<AsSocketClient> lstClient = new List<AsSocketClient>();
        public FrmServer()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _server = new AsSocketServer(int.Parse(txtPort.Text));
            _server.OnAccept += new AsSocketAcceptEventHandler(Server_OnAccept);
            _server.Listen();
        }

        public void Server_OnAccept(object sender, AsSocketAcceptEventArgs e)
        {
            AddLog("접속 발생");

            AsSocketClient client = new AsSocketClient(_iPos++, e.Worker);

            client.OnReceive += new AsSocketReceiveEventHandler(Client_OnReceive);
            client.OnConnet += new AsSocketConnectEventHandler(Client_OnConnet);
            client.OnError += new AsSocketErrorEventHandler(Client_OnError);
            client.OnClose += new AsSocketCloseEventHandler(Client_OnClose);
            client.Receive();

            lstClient.Add(client);
        }

        public void AddLog(string str)
        {
            txtRecv.AppendText(Environment.NewLine);
            txtRecv.AppendText(DateTime.Now.ToString("HH:mm:ss") + " " + str);
        }
        public void Client_OnClose(object sender, AsSocketConnectionEventArgs e)
        {
            AsSocketClient client = sender as AsSocketClient;
            lstClient.Remove(client);
            AddLog("접속 종료 발생");
        }
        public void Client_OnError(object sender, AsSocketErrorEventArgs e)
        {
            AddLog("ERROR 발생");
            AsSocketClient client = sender as AsSocketClient;
            lstClient.Remove(client);
        }
        public void Client_OnConnet(object sender, AsSocketConnectionEventArgs e)
        {
            AddLog(string.Format("CLINET 접속 {0},{1}", ((AsSocketClient)sender).Connection.AddressFamily.ToString()));
        }
        public void Client_OnReceive(object sender, AsSocketReceiveEventArgs e)
        {
            string recv = Encoding.Unicode.GetString(e.ReceiveData);
            AddLog(string.Format("메시지 수신 = {0}", recv));
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(txtSend.Text);
            lstClient.ForEach(f => f.Send(bytes));
        }
    }
}
