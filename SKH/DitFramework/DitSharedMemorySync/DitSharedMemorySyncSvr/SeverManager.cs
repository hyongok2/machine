using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Net.AsyncSocket;
using System.Timers;
using Dit.FrameworkSingle.Net.ServerClinet;

namespace DitSharedMemorySvr
{
    public class SeverManager
    {
        //내부 변수
        private AsyncSocketServer _accepter;
        private Timer _tmrAliveClient = new Timer(1000);

        public int Port { get; set; }
        public List<TcpServerSession> Sessions = new List<TcpServerSession>();


        public event EventHandler OnAccepted;
        public event EventHandler OnClosed;

        //메소드 - 컨트롤 변수
        public void Start()
        {
            if (_accepter != null)
                Stop();

            _accepter = new AsyncSocketServer(Port);
            _accepter.OnAccept += new AsyncSocketAcceptEventHandler(Accepter_OnAccept);
            _accepter.Listen();

            _tmrAliveClient.Interval = 1000;
            _tmrAliveClient.Start();
        }
        public void Stop()
        {
            _accepter.Stop();
            _tmrAliveClient.Stop();
        }

        //메소드 - 연결 확인용
        private void TmrAliveClient_Elapsed(object sender, ElapsedEventArgs e)
        {

        }
        private void Accepter_OnAccept(object sender, AsyncSocketAcceptEventArgs e)
        {
            AsyncSocketClient newClinet = new AsyncSocketClient(e.Worker.GetHashCode(), e.Worker);
            SMSvrSession session = new SMSvrSession(newClinet, this);
            lock (Sessions) { Sessions.Add(session); }
            session.StartRecv();

            if (OnAccepted != null)
            {
                OnAccepted(session, new EventArgs());
            }
        }
        public void CloseSession(SMSvrSession session)
        {
            lock (Sessions) { Sessions.Remove(session); }
            
            if (OnClosed!= null)
            {
                OnClosed(session, new EventArgs());
            }
        }
    }
}
