using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Net.AsyncSocket;
using System.Timers;

namespace Dit.FrameworkSingle.Net.ServerClinet
{
    public class TcpServerSessionManager
    {
        //내부 변수
        private AsyncSocketServer _accepter;

        private Timer _tmrAliveClient = new Timer(1000);

        //이벤트
        public event DNetErrorEventHandler OnError;
        public event DNetCloseEventHandler OnClose;
        public event DNetReceiveEventHandler OnReceive;
        public event DNetAcceptEventHandler OnAccept;

        //프로퍼티
        public int Port { get; set; }
        public List<TcpServerSession> Sessions { get; set; }
        //메소드 - 생성자
        public TcpServerSessionManager()
        {
            Port = 5200;
            Sessions = new List<TcpServerSession>();
            _tmrAliveClient.Elapsed += new ElapsedEventHandler(TmrAliveClient_Elapsed);
        }

        //메소드 - 연결 확인용
        private void TmrAliveClient_Elapsed(object sender, ElapsedEventArgs e)
        {

        }

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
        public void SendToAllClientPacket(Packet pk)
        {
            lock (Sessions)
            {
                foreach (TcpServerSession sesion in Sessions)
                    sesion.SendPacket(pk);
            }
        }

        private void Accepter_OnAccept(object sender, AsyncSocketAcceptEventArgs e)
        {
            AsyncSocketClient newClinet = new AsyncSocketClient(e.Worker.GetHashCode(), e.Worker);

            TcpServerSession session = new TcpServerSession(newClinet);
            session.OnClose += new DNetCloseEventHandler(Session_OnClose);
            session.OnReceive += new DNetReceiveEventHandler(Session_OnReceive);
            session.OnError += new DNetErrorEventHandler(Session_OnError);

            lock (Sessions) { Sessions.Add(session); }

            if (OnAccept != null)
                OnAccept(this, new DNetAcceptEventArgs(session));

            session.StartRecv();
        }
        private void Session_OnError(object sender, DNetErrorEventArgs e)
        {
            if (OnError != null)
                OnError(sender, e);

            lock (Sessions)
            {
                TcpServerSession session = sender as TcpServerSession;
                if (Sessions.Contains(session))
                    if (session != null)
                        Sessions.Remove(session);
            }
        }
        private void Session_OnReceive(object sender, DNetReceiveEventArgs e)
        {
            if (OnReceive != null)
                OnReceive(sender, e);
        }
        private void Session_OnClose(object sender, DNetConnectionEventArgs e)
        {
            if (OnClose != null)
                OnClose(sender, e);

            lock (Sessions)
            {
                TcpServerSession session = sender as TcpServerSession;
                if (Sessions.Contains(session))
                    if (session != null)
                        Sessions.Remove(session);
            }
        }
    }
}
