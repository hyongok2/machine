using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Dit.Framework.Net.AsSocket
{
    public class AsSocketClient : AsSocketClass
    {
        //메소드 - 생성자
        public AsSocketClient(int id)
        {
            this.id = id;
        }
        public AsSocketClient(int id, Socket conn)
        {
            this.id = id;
            this.conn = conn;
        }

        //메소드 - 접속 요청 함수
        public override bool Connect(string hostAddress, int port)
        {
            try
            {
                IPAddress[] ips = Dns.GetHostAddresses(hostAddress);
                IPEndPoint remoteEP = new IPEndPoint(ips[0], port);
                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.BeginConnect(remoteEP, new AsyncCallback(OnConnectCallback), client);
            }
            catch (System.Exception e)
            {
                AsSocketErrorEventArgs eev = new AsSocketErrorEventArgs(this.id, e);

                ErrorOccured(eev);
                return false;
            }

            return true;

        }

        //메소드 - 수신 대기 요청
        public void Receive()
        {
            try
            {
                StateObject so = new StateObject(conn);
                so.Worker.BeginReceive(so.Buffer, 0, so.BufferSize, 0, new AsyncCallback(OnReceiveCallBack), so);
            }
            catch (System.Exception e)
            {
                AsSocketErrorEventArgs eev = new AsSocketErrorEventArgs(this.id, e);
                ErrorOccured(eev);
            }
        }

        //메소드 - 전송 대기 요청 
        public override bool Send(byte[] buffer)
        {
            try
            {
                Socket client = conn;
                client.BeginSend(buffer, 0, buffer.Length, 0, new AsyncCallback(OnSendCallBack), client);
            }
            catch (System.Exception e)
            {
                AsSocketErrorEventArgs eev = new AsSocketErrorEventArgs(this.id, e);
                ErrorOccured(eev);
                return false;
            }

            return true;
        }

        //메소드 - 통신 종료 요청 
        public bool Close()
        {
            try
            {
                Socket client = conn;
                client.Shutdown(SocketShutdown.Both);
                client.BeginDisconnect(false, new AsyncCallback(OnCloseCallBack), client);
                return true;
            }
            catch (System.Exception e)
            {
                AsSocketErrorEventArgs eev = new AsSocketErrorEventArgs(this.id, e);
                ErrorOccured(eev);
                return false;
            }
        }

        //메소드 -  콜백 함수 
        private void OnConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;

                // 보류 중인 연결을 완성한다.
                client.EndConnect(ar);
                conn = client;
                // 연결에 성공하였다면, 데이터 수신을 대기한다.
                Receive();

                // 연결 성공 이벤트를 날린다.
                AsSocketConnectionEventArgs cev = new AsSocketConnectionEventArgs(this.id);

                Connected(cev);
            }
            catch (System.Exception e)
            {
                AsSocketErrorEventArgs eev = new AsSocketErrorEventArgs(this.id, e);

                ErrorOccured(eev);
            }
        }
        private void OnReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                StateObject so = (StateObject)ar.AsyncState;
                int bytesRead = so.Worker.EndReceive(ar);
                AsSocketReceiveEventArgs rev = new AsSocketReceiveEventArgs(this.id, bytesRead, so.Buffer);

                // 데이터 수신 이벤트를 처리한다.
                if (bytesRead > 0)
                    Received(rev);

                // 다음 읽을 데이터를 처리한다.
                Receive();
            }
            catch (System.Exception e)
            {
                AsSocketErrorEventArgs eev = new AsSocketErrorEventArgs(this.id, e);

                ErrorOccured(eev);
            }
        }
        private void OnSendCallBack(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;

                int bytesWritten = client.EndSend(ar);

                AsSocketSendEventArgs sev = new AsSocketSendEventArgs(this.id, bytesWritten);

                Send(sev);
            }
            catch (System.Exception e)
            {
                AsSocketErrorEventArgs eev = new AsSocketErrorEventArgs(this.id, e);

                ErrorOccured(eev);
            }
        }
        private void OnCloseCallBack(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;

                client.EndDisconnect(ar);
                client.Close();

                AsSocketConnectionEventArgs cev = new AsSocketConnectionEventArgs(this.id);

                Closed(cev);
            }
            catch (System.Exception e)
            {
                AsSocketErrorEventArgs eev = new AsSocketErrorEventArgs(this.id, e);

                ErrorOccured(eev);
            }
        }
    }

}
