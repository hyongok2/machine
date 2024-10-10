using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework;
using Dit.Framework.Net.AsyncSocket;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace Dit.FrameworkSingle.Net.ServerClinet
{
    public class TcpServerSession
    {
        private AsyncSocketClient _socketClient; // 클라이언트 접속 서버

        //통신용 메모리 버퍼.
        private byte[] _allReciveDataBuffer = new byte[1024];
        private int _allReciveDataBufferSize = 1024;
        private int _totalBytesSize = 0;

        public event DNetErrorEventHandler OnError;
        public event DNetConnectEventHandler OnConnet;
        public event DNetCloseEventHandler OnClose;
        public event DNetSendEventHandler OnSend;
        public event DNetReceiveEventHandler OnReceive;

        public bool IsConnection
        {
            get
            {
                if (_socketClient.Connection == null)
                {
                    return false;
                }
                else
                {
                    return _socketClient.Connection.Connected;
                }
            }
        }

        public TcpServerSession()
        {
        }
        public TcpServerSession(AsyncSocketClient newClient)
        {
            _socketClient = newClient;
            _socketClient.OnReceive += new AsyncSocketReceiveEventHandler(SocketCllient_OnReceive);
            _socketClient.OnError += new AsyncSocketErrorEventHandler(SocketCllient_OnError);
            _socketClient.OnClose += new AsyncSocketCloseEventHandler(SocketCllient_OnClose);

        }
        public void IntializeSocket()
        {
            _allReciveDataBuffer = new byte[1024];
            _allReciveDataBufferSize = 1024;
            _totalBytesSize = 0;
        }
        public void StartRecv()
        {
            if (_socketClient != null)
            {
                IntializeSocket();
                _socketClient.Receive();
            }
        }

        public void SocketCllient_OnClose(object sender, AsyncSocketConnectionEventArgs e)
        {
            if (OnClose != null)
                OnClose(this, new DNetConnectionEventArgs(0));
        }
        public void SocketCllient_OnError(object sender, AsyncSocketErrorEventArgs e)
        {
            if (OnError != null)
                OnError(this, new DNetErrorEventArgs(0, e.AsyncSocketException));
        }
        private void SocketCllient_OnReceive(object sender, AsyncSocketReceiveEventArgs e)
        {
            lock (this)
            {
                try
                {
                    int onepacketsize = 0;

                    if (_totalBytesSize + e.ReceiveBytes > _allReciveDataBufferSize)
                    {
                        _allReciveDataBufferSize = _totalBytesSize + e.ReceiveBytes + 1024;
                        byte[] tBuff = new byte[_allReciveDataBufferSize];
                        Array.Copy(_allReciveDataBuffer, 0, tBuff, 0, _allReciveDataBuffer.Length);

                        _allReciveDataBuffer = tBuff;
                    }


                    Array.Copy(e.ReceiveData, 0, _allReciveDataBuffer, _totalBytesSize, e.ReceiveBytes);
                    _totalBytesSize = _totalBytesSize + e.ReceiveBytes;

                    if (_totalBytesSize >= 20)
                    {
                        onepacketsize = BitConverter.ToInt32(_allReciveDataBuffer, 1);
                        if (_allReciveDataBuffer[0] != 0x02)
                        {
                            throw new Exception(string.Format("{0} Socket Packet Size Error", this.EqpID));
                        }

                        while (_totalBytesSize >= onepacketsize)
                        {
                            byte[] buff = new byte[onepacketsize];
                            Array.Copy(_allReciveDataBuffer, 0, buff, 0, onepacketsize);
                            Array.Copy(_allReciveDataBuffer, onepacketsize, _allReciveDataBuffer, 0, _allReciveDataBuffer.Length - onepacketsize);
                            _totalBytesSize = _totalBytesSize - onepacketsize;

                            if (buff[0] != 0x02 || buff[buff.Length - 1] != 0x03)
                            {
                                throw new Exception(string.Format("{0} Socket Packet Size Error", this.EqpID));
                            }
                            else
                            {
                                Deliver deliver = new Deliver();
                                deliver.SetDeiver(buff);
                                OnRecivePacket(deliver);
                            }

                            if (_totalBytesSize >= 20)
                                onepacketsize = BitConverter.ToInt32(_allReciveDataBuffer, 1);
                            else
                                break;
                        }
                    }
                }
                catch (Exception)
                {
                    _socketClient.Close();
                }
            }
        }
        private void OnRecivePacket(Deliver deliver)
        {
            try
            {
                Packet pk = Packet.Deserialize(deliver.PacketBuffer);

                if (OnReceive != null)
                    OnReceive(this, new DNetReceiveEventArgs(0, pk));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void SendPacket(Packet pk)
        {
            lock (this)
            {
                if (IsConnection)
                {
                    Deliver deliver = new Deliver();
                    deliver.SetDeiver(pk.PacketID, pk.Serialize());
                    _socketClient.Send(deliver.AllBuffer);
                }
            }
        }
        public Socket Connection
        {
            get { return _socketClient.Connection; }
        }
        public string EqpID { get; set; }

        public IPEndPoint LocalEndPoint
        {
            get
            {
                IPEndPoint local = this.Connection.LocalEndPoint as IPEndPoint;
                return local;
            }
        }

        public IPEndPoint RemoteEndPoint
        {
            get
            {
                IPEndPoint local = this.Connection.RemoteEndPoint as IPEndPoint;
                return local;
            }
        }

    }
}
