using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Dit.Framework.Net.AsyncSocket
{
    public class AsyncSocketServer : AsyncSocketClass
    {
        private const int backLog = 100;

        private int port;
        private Socket listener;

        public AsyncSocketServer(int port)
        {
            this.port = port;
        }
        public int Port
        {
            get { return this.port; }
        }
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
                AsyncSocketErrorEventArgs eev = new AsyncSocketErrorEventArgs(this.id, e);

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
                AsyncSocketErrorEventArgs eev = new AsyncSocketErrorEventArgs(this.id, e);

                ErrorOccured(eev);
            }
        }
        private void OnListenCallBack(IAsyncResult ar)
        {
            try
            {
                Socket listener = (Socket)ar.AsyncState;
                Socket worker = listener.EndAccept(ar);

                // Client를 Accept 했다고 Event를 발생시킨다.
                AsyncSocketAcceptEventArgs aev = new AsyncSocketAcceptEventArgs(worker);

                Accepted(aev);

                // 다시 새로운 클라이언트의 접속을 기다린다.
                StartAccept();
            }
            catch (System.Exception e)
            {
                AsyncSocketErrorEventArgs eev = new AsyncSocketErrorEventArgs(this.id, e);

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
                AsyncSocketErrorEventArgs eev = new AsyncSocketErrorEventArgs(this.id, e);

                ErrorOccured(eev);
            }
        }

    }  
}
