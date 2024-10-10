using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Dit.Framework.Net.AsSocket
{
    public class AsSocketServer : AsSocketClass
    {
        private const int backLog = 100;

        //맴버 변수
        private int port;
        private Socket listener;

        //프로퍼티
        public int Port
        {
            get { return this.port; }
        }

        //메소드 - 생성자
        public AsSocketServer(int port)
        {
            this.port = port;
        }

        //메소드 - 접속 대기 요청 시작/정지
        public void Listen()
        {
            try
            {
                listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(new IPEndPoint(IPAddress.Any, this.port));
                listener.Listen(backLog);

                StartAccept();
            }
            catch (System.Exception e)
            {
                AsSocketErrorEventArgs eev = new AsSocketErrorEventArgs(this.id, e);

                ErrorOccured(eev);
            }
        }
        public void Stop()
        {
            try
            {
                if (listener != null)
                {
                    if (listener.IsBound)
                        listener.Close(100);
                }
            }
            catch (System.Exception e)
            {
                AsSocketErrorEventArgs eev = new AsSocketErrorEventArgs(this.id, e);

                ErrorOccured(eev);
            }
        }
        private void StartAccept()
        {
            try
            {
                listener.BeginAccept(new AsyncCallback(OnListenCallBack), listener);
            }
            catch (System.Exception e)
            {
                AsSocketErrorEventArgs eev = new AsSocketErrorEventArgs(this.id, e);

                ErrorOccured(eev);
            }
        }

        //메소드 - 접속 요청 콜백
        private void OnListenCallBack(IAsyncResult ar)
        {
            try
            {
                Socket listener = (Socket)ar.AsyncState;
                Socket worker = listener.EndAccept(ar);

                // Client를 Accept 했다고 Event를 발생시킨다.
                AsSocketAcceptEventArgs aev = new AsSocketAcceptEventArgs(worker);

                Accepted(aev);

                // 다시 새로운 클라이언트의 접속을 기다린다.
                StartAccept();
            }
            catch (System.Exception e)
            {
                AsSocketErrorEventArgs eev = new AsSocketErrorEventArgs(this.id, e);

                ErrorOccured(eev);
            }
        }
    }  
}
