using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Dit.Framework.Net.AsSocket
{
    //UDP 비동기 통신 클래스.    
    public class AsUdpSocketClient : AsSocketClass
    {
        public AsUdpSocketClient(int id)
        {
            this.id = id;
        }
        public AsUdpSocketClient(int id, Socket conn)
        {
            this.id = id;
            this.conn = conn;
        }

        public override bool Connect(string hostAddress, int port)
        {
            try
            {
                IPAddress[] ips = Dns.GetHostAddresses(hostAddress);
                IPEndPoint remoteEP = new IPEndPoint(ips[0], port);


                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

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
        
        public override void Close()
        {
            try
            {
                Socket client = conn;

                client.Shutdown(SocketShutdown.Both);
                client.BeginDisconnect(false, new AsyncCallback(OnCloseCallBack), client);
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
