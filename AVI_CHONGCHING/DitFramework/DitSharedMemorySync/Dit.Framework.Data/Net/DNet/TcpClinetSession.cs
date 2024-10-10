using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework;
using Dit.Framework.Net.AsyncSocket;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using System.Timers;
using Dit.Framework.Log;

namespace Dit.FrameworkSingle.Net.ServerClinet
{
    [Serializable]
    public class TcpClinetSession
    {
        // Event Handler
        public event DNetErrorEventHandler OnError;
        public event DNetConnectEventHandler OnConnet;
        public event DNetCloseEventHandler OnClose;
        public event DNetSendEventHandler OnSend;
        public event DNetReceiveEventHandler OnReceive;
        public event DNetAcceptEventHandler OnAccept;

        [XmlIgnore]
        private AsyncSocketClient _socketEquip; // 설비 접속 클라이언트

        public string IpAddress { get; set; }
        public int Port { get; set; }
        public object Tag { get; set; }
        public ILogger Logger { get; set; }
        public string Version { get; set; }

        public int ScreenInterval { get; set; }
        public bool ScreenModeEnalbe { get; set; }
        public int ScreenRate { get; set; }

        //통신용 메모리 버퍼.
        private byte[] _allReciveDataBuffer = new byte[819200];
        private int _allReciveDataBufferSize = 819200;
        private int _totalBytesSize = 0;

        //재연결 처리용
        private Timer _tmrConnector = new Timer(5000); //60000);  //10분        

        private bool _isStarted = false;

        // 이벤트 정의
        public int MaxClientCount { get; set; }
        public TcpClinetSession()
        {
            Logger = new Dit.Framework.Log.ILogger();

            _tmrConnector.Elapsed += new ElapsedEventHandler(TmrConnector_Elapsed);

            ScreenInterval = 1;
            ScreenModeEnalbe = true;
            ScreenRate = 1;
        }

        public bool IsConnection
        {
            get
            {
                if (_socketEquip == null) return false;
                if (_socketEquip.Connection == null)
                {
                    return false;
                }
                else
                {
                    return _socketEquip.Connection.Connected;
                }
            }
        }

        public void TmrConnector_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (IsConnection == false && _isStarted)
            {
                IntializeSocket();
                _socketEquip.Connect(IpAddress, Port);
                Logger.AppendLine(string.Format("Try Connecting ip {0} port {1}", IpAddress, Port));
            }
        }
        public void Start()
        {
            _isStarted = true;
            _socketEquip = new AsyncSocketClient(0);
            _socketEquip.OnConnet += new AsyncSocketConnectEventHandler(SocketEquip_OnConnet);
            _socketEquip.OnClose += new AsyncSocketCloseEventHandler(SocketEquip_OnClose);
            _socketEquip.OnError += new AsyncSocketErrorEventHandler(SocketEquip_OnError);
            _socketEquip.OnReceive += new AsyncSocketReceiveEventHandler(SocketEquip_OnReceive);

            Logger.AppendLine(string.Format("Start Equip Monitor ip {0} port {1}", IpAddress, Port));

            TmrConnector_Elapsed(null, null);
            _tmrConnector.Start();
        }
        public void Stop()
        {
            _isStarted = false;
            _tmrConnector.Stop();
            _socketEquip.Close();
            Logger.AppendLine(string.Format("Stop Equip Monitor ip {0} port {1}", IpAddress, Port));
        }

        public void SendPacket(Packet pk)
        {
            lock (this)
            {
                if (IsConnection)
                {
                    Deliver deliver = new Deliver();
                    deliver.SetDeiver(pk.PacketID, pk.Serialize());
                    _socketEquip.Send(deliver.AllBuffer);
                }
            }
        }
        private void SocketEquip_OnError(object sender, AsyncSocketErrorEventArgs e)
        {
            Logger.AppendLine(string.Format("Error Equip Socket ip {0} port {1}", IpAddress, Port));
            if (((AsyncSocketClient)sender).Connection == null) return;
            IntPtr aa = ((AsyncSocketClient)sender).Connection.Handle;

            _socketEquip.Connection.Close();

            if (OnError != null)
                OnError(this, new DNetErrorEventArgs(0, e.AsyncSocketException));
        }
        private void SocketEquip_OnConnet(object sender, AsyncSocketConnectionEventArgs e)
        {
            Logger.AppendLine(string.Format("Connected ip {0} port {1}", IpAddress, Port));
            if (OnConnet != null)
                OnConnet(0, new DNetConnectionEventArgs(0));
        }
        private void SocketEquip_OnClose(object sender, AsyncSocketConnectionEventArgs e)
        {
            Logger.AppendLine(string.Format("Disonnected Equip ip {0} port {1}", IpAddress, Port));

            if (OnClose != null)
                OnClose(0, new DNetConnectionEventArgs(0));
        }

        public void IntializeSocket()
        {
            _allReciveDataBuffer = new byte[819200];
            _allReciveDataBufferSize = 819200;
            _totalBytesSize = 0;
        }
        private void SocketEquip_OnReceive(object sender, AsyncSocketReceiveEventArgs e)
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
                            throw new Exception(string.Format("Socket Packet Size Error ip {0} port {1}", IpAddress, Port));
                        }

                        while (_totalBytesSize >= onepacketsize)
                        {
                            byte[] buff = new byte[onepacketsize];
                            Array.Copy(_allReciveDataBuffer, 0, buff, 0, onepacketsize);
                            Array.Copy(_allReciveDataBuffer, onepacketsize, _allReciveDataBuffer, 0, _allReciveDataBuffer.Length - onepacketsize);
                            _totalBytesSize = _totalBytesSize - onepacketsize;

                            if (buff[0] != 0x02 || buff[buff.Length - 1] != 0x03)
                            {
                                throw new Exception(string.Format("Socket Packet Size Error ip {0} port {1}", IpAddress, Port));
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
                catch (Exception ex)
                {
                    Logger.AppendLine(string.Format("Socket Packet Size Error {1} ip {0} port {1}", IpAddress, Port));
                    _socketEquip.Close();
                }
            }
        }
        private void OnRecivePacket(Deliver deliver)
        {
            Packet pk = Packet.Deserialize(deliver.PacketBuffer);
            if (OnReceive != null)
                OnReceive(this, new DNetReceiveEventArgs(0, pk));
        }

        public void Disconnect()
        {
            if (_socketEquip != null)
                _socketEquip.Close();
        }
    }
}
