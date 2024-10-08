using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework;
using Dit.Framework.Net.AsSocket;
using System.IO;
using System.Net.Sockets;
using System.Net;
using Dit.Framework.Comm;

namespace Dit.FrameworkSingle.Net.DNet
{
    public class DNetServerSession
    {
        //통신용 비동기 소켓
        private AsSocketClient _socket; // 클라이언트 접속 서버

        //통신용 메모리 버퍼.
        private byte[] _allReciveDataBuffer = new byte[1024];
        private int _allReciveDataBufferSize = 1024;
        private int _totalBytesSize = 0;

        //이벤트 
        public event DNetErrorEventHandler OnError;
        public event DNetConnectEventHandler OnConnet;
        public event DNetCloseEventHandler OnClose;
        public event DNetSendEventHandler OnSend;
        public event DNetReceiveEventHandler OnReceive;

        //프로퍼티
        public bool IsConnection
        {
            get
            {
                if (_socket.Connection == null)
                {
                    return false;
                }
                else
                {
                    return _socket.Connection.Connected;
                }
            }
        }
        public Socket Connection
        {
            get { return _socket.Connection; }
        }
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

        //메소드 - 생성자
        public DNetServerSession()
        {
        }
        public DNetServerSession(AsSocketClient newClient)
        {
            _socket = newClient;
            _socket.OnReceive += new AsSocketReceiveEventHandler(SocketCllient_OnReceive);
            _socket.OnError += new AsSocketErrorEventHandler(SocketCllient_OnError);
            _socket.OnClose += new AsSocketCloseEventHandler(SocketCllient_OnClose);

        }
        public void IntializeSocket()
        {
            _allReciveDataBuffer = new byte[1024];
            _allReciveDataBufferSize = 1024;
            _totalBytesSize = 0;
        }
        public void StartRecv()
        {
            if (_socket != null)
            {
                IntializeSocket();
                _socket.Receive();
            }
        }

        //메소드 - 소켓 이벤트 처리
        public void SocketCllient_OnClose(object sender, AsSocketConnectionEventArgs e)
        {
            if (OnClose != null)
                OnClose(this, new DNetConnectionEventArgs(0));
        }
        public void SocketCllient_OnError(object sender, AsSocketErrorEventArgs e)
        {
            if (OnError != null)
                OnError(this, new DNetErrorEventArgs(0, e.AsSocketException));
        }
        private void SocketCllient_OnReceive(object sender, AsSocketReceiveEventArgs e)
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

                    while (true)
                    {
                        if (_totalBytesSize >= DNetPacket.PACKET_HEADERSIZE)
                        {
                            DNetPacket recvPk = new DNetPacket();
                            if (CheckPacketHeader(_allReciveDataBuffer, _totalBytesSize, ref onepacketsize) == ComResult.Success)
                            {
                                byte[] buff = new byte[onepacketsize];
                                Array.Copy(_allReciveDataBuffer, 0, buff, 0, onepacketsize);

                                Array.Copy(_allReciveDataBuffer, onepacketsize, _allReciveDataBuffer, 0, _allReciveDataBuffer.Length - onepacketsize);
                                _totalBytesSize = _totalBytesSize - onepacketsize;

                                recvPk = new DNetPacket(buff);
                                OnRecivePacket(recvPk);
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                    _socket.Close();
                }
            }
        }
                
        //메소드 - 리시브 패킷 처리..
        private void OnRecivePacket(DNetPacket packet)
        {
            if (OnReceive != null)
                OnReceive(this, new DNetReceiveEventArgs(0, packet));
        }
        public void SendPacket(DNetPacket pk)
        {
            lock (this)
            {
                if (IsConnection)
                {
                    _socket.Send(pk.GetBuffer());
                }
            }
        }
        
        private ComResult CheckPacketHeader(byte[] allReciveDataBuffer, int _totalBytesSize, ref int onepacketsize)
        {
            try
            {
                int pkHeaderSize = 0;
                DNetPacket headerPacket = new DNetPacket();

                if (headerPacket.ParsingHeaderBuffer(allReciveDataBuffer, ref pkHeaderSize) == ComResult.Fail)
                    return ComResult.Fail;

                if (_totalBytesSize >= headerPacket.PacketSize)
                {
                    onepacketsize = headerPacket.PacketSize;

                    if (onepacketsize > DNetPacket.PACKET_HEADERSIZE)
                        return ComResult.Success;
                    else
                        return ComResult.Fail;
                }
                else
                {
                    return ComResult.Fail;
                }
            }
            catch
            {
                return ComResult.Fail;
            }
        }
    }
}
