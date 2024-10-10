using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework;
using Dit.Framework.Net.AsSocket;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using System.Timers;
using Dit.Framework.Comm;

namespace Dit.FrameworkSingle.Net.DNet
{    
    public class DNetClinetSession
    {
        //이벤트
        public event DNetErrorEventHandler OnError;
        public event DNetConnectEventHandler OnConnet;
        public event DNetCloseEventHandler OnClose;
        public event DNetSendEventHandler OnSend;
        public event DNetReceiveEventHandler OnReceive;
        public event DNetAcceptEventHandler OnAccept;
                
        private AsSocketClient _socket; // 설비 접속 클라이언트

        public string IpAddress { get; set; }
        public int Port { get; set; }
        
        //통신용 메모리 버퍼.
        private byte[] _allReciveDataBuffer = new byte[819200];
        private int _allReciveDataBufferSize = 819200;
        private int _totalBytesSize = 0;

        //재연결 처리용
        private Timer _tmrConnector = new Timer(5000); 
    
        private bool _isStarted = false;

        public DNetClinetSession()
        {
            _tmrConnector.Elapsed += new ElapsedEventHandler(TmrConnector_Elapsed);
        }

        public bool IsConnection
        {
            get
            {
                if (_socket == null) return false;
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

        public void TmrConnector_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (IsConnection == false && _isStarted)
            {
                IntializeSocket();
                _socket.Connect(IpAddress, Port);
            }
        }
        public void Start()
        {
            _isStarted = true;
            _socket = new AsSocketClient(0);
            _socket.OnConnet += new AsSocketConnectEventHandler(SocketEquip_OnConnet);
            _socket.OnClose += new AsSocketCloseEventHandler(SocketEquip_OnClose);
            _socket.OnError += new AsSocketErrorEventHandler(SocketEquip_OnError);
            _socket.OnReceive += new AsSocketReceiveEventHandler(SocketEquip_OnReceive);


            TmrConnector_Elapsed(null, null);
            _tmrConnector.Start();
        }
        public void Stop()
        {
            _isStarted = false;
            _tmrConnector.Stop();
            _socket.Close();
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
        private void SocketEquip_OnError(object sender, AsSocketErrorEventArgs e)
        {
            IntPtr aa = ((AsSocketClient)sender).Connection.Handle;

            _socket.Connection.Close();

            if (OnError != null)
                OnError(this, new DNetErrorEventArgs(0, e.AsSocketException));
        }
        private void SocketEquip_OnConnet(object sender, AsSocketConnectionEventArgs e)
        {
            if (OnConnet != null)
                OnConnet(0, new DNetConnectionEventArgs(0));
        }
        private void SocketEquip_OnClose(object sender, AsSocketConnectionEventArgs e)
        {
            if (OnClose != null)
                OnClose(0, new DNetConnectionEventArgs(0));
        }

        public void IntializeSocket()
        {
            _allReciveDataBuffer = new byte[819200];
            _allReciveDataBufferSize = 819200;
            _totalBytesSize = 0;
        }
        private void SocketEquip_OnReceive(object sender, AsSocketReceiveEventArgs e)
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

        private ComResult CheckPacketHeader(byte[] allReciveDataBuffer, int _totalBytesSize, ref int onepacketsize)
        {
            int pkHeaderSize = 0;
            DNetPacket headerPacket = new DNetPacket();

            if (headerPacket.ParsingHeaderBuffer(allReciveDataBuffer, ref pkHeaderSize) == ComResult.Fail)
                return ComResult.Fail;

            if (_totalBytesSize >= headerPacket.PacketSize)
            {
                onepacketsize = headerPacket.PacketSize;
                return ComResult.Success;
            }
            else
            {
                return ComResult.Fail;
            }
        }
        private void OnRecivePacket(DNetPacket Packet)
        {             
            if (OnReceive != null)
                OnReceive(this, new DNetReceiveEventArgs(0, Packet));
        }
    }
}
