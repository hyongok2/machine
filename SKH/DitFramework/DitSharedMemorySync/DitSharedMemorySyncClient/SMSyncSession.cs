using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.FrameworkSingle.Net.ServerClinet;
using DitSharedMemoryPacket;
using System.Threading;
using DitSharedMemoryClient.Log;
using Dit.Framework.Log;
using Dit.Framework.Util;

namespace DitSharedMemoryClient
{
    public class SyncResult
    {
        public DateTime ReqTime { get; set; }
        public DateTime RspTime { get; set; }
    }

    public class SMSyncSession
    {
        public ShardMem _shardMem;
        public string IP { get; set; }
        public int Port { get; set; }
        public string SMName { get; set; }
        public int SMSize { get; set; }
        public int SyncStart { get; set; }
        public int SyncSize { get; set; }
        public int SyncTime { get; set; }
        public int SyncCommand { get; set; }
        public bool _running { get; set; }
        public DateTime ConnectDateTime { get; set; }
        public bool IsConnected { get { return _session.IsConnection; } }
        private Thread _worker = null;
        private TcpClinetSession _session = new TcpClinetSession();
        public List<SyncResult> LstSyncResult { get; set; }
        public int SyncSec { get; set; }
        public SMSyncSession(string ip, int port)
        {

            IP = ip;
            Port = port;
            LstSyncResult = new List<SyncResult>();
            _session.OnConnet += new DNetConnectEventHandler(Session_OnConnet);
            _session.OnClose += new DNetCloseEventHandler(Session_OnClose);
            _session.OnReceive += new DNetReceiveEventHandler(Session_OnReceive);
            _session.OnSend += new DNetSendEventHandler(Session_OnSend);
            _session.OnError += new DNetErrorEventHandler(Session_OnError);
        }

        private void Session_OnError(object sender, DNetErrorEventArgs e)
        {

        }

        private void Session_OnSend(object sender, DNetSendEventArgs e)
        {

        }
        int _reqPackCnt = 0;
        private void Session_OnReceive(object sender, DNetReceiveEventArgs e)
        {
            if (e.RecvPacket is PkSyncReadRsp)
            {
                lock (this) _reqPackCnt--;
                OnPkSyncReadRsp((PkSyncReadRsp)e.RecvPacket);

            }
            else if (e.RecvPacket is PkSyncWriteRsp)
            {
                lock (this) _reqPackCnt--;
                OnPkSyncWriteRsp((PkSyncWriteRsp)e.RecvPacket);
            }
            else if (e.RecvPacket is PkSyncTest)
            {
            }
        }
        int _PkSyncReadRspCnt = 0;
        private void OnPkSyncWriteRsp(PkSyncWriteRsp pk)
        {
            if (pk.Result == 0)
            {
                lock (LstSyncResult)
                    LstSyncResult.Add(new SyncResult() { ReqTime = pk.ReqTime, RspTime = DateTime.Now });

                if (_PkSyncReadRspCnt++ % 1000 == 0)
                {
                    Logger.FileLogger.AppendLine(LogLevel.Info, "RECV Sync Read Rsp =[{0}], START=[{1:00000#}], LENGTH=[{2:00000#}], {3}", pk.Name, pk.Start, pk.Length, _PkSyncReadRspCnt);
                }
            }
            else
            {
                Logger.FileLogger.AppendLine(LogLevel.Warning, "WRITE RSP NAME =[{0}], START=[{1:00000#}], LENGTH=[{2:00000#}]", pk.Name, pk.Start, pk.Length);
            }
        }
        int _PkSyncWriteRspCnt = 0;
        private void OnPkSyncReadRsp(PkSyncReadRsp pk)
        {
            if (pk.Result == 0)
            {
                _shardMem.WriteBytes(pk.Start, pk.Length, pk.ReadBytes);

                lock (LstSyncResult)
                    LstSyncResult.Add(new SyncResult() { ReqTime = pk.ReqTime, RspTime = DateTime.Now });

                if (_PkSyncWriteRspCnt++ % 1000 == 0)
                {
                    Logger.FileLogger.AppendLine(LogLevel.Info, "RECV Sync Write Rsp =[{0}], START=[{1:00000#}], LENGTH=[{2:00000#}], {3}", pk.Name, pk.Start, pk.Length, _PkSyncWriteRspCnt);
                }
            }
            else
            {
                Logger.FileLogger.AppendLine(LogLevel.Warning, "READ RSP NAME =[{0}], START=[{1:00000#}], LENGTH=[{2:00000#}]", pk.Name, pk.Start, pk.Length);
            }
        }
        private void Session_OnClose(object sender, DNetConnectionEventArgs e)
        {
            _reqPackCnt = 0;
        }
        private void Session_OnConnet(object sender, DNetConnectionEventArgs e)
        {
            _reqPackCnt = 0;
            ConnectDateTime = PcDateTime.Now;
        }
        public void Start()
        {
            _shardMem = new ShardMem();
            _shardMem.Name = SMName;
            _shardMem.Size = SMSize;
            _shardMem.Open();

            _session.IpAddress = IP;
            _session.Port = Port;
            _session.Start();

            _running = true;
            _worker = new Thread(Working);
            _worker.Start();
        }

        public DateTime _sendTesterPacket = PcDateTime.Now;
        int _workProcess = 0;
        public void Working()
        {
            try
            {
                while (_running)
                {
                    if (_session.IsConnection)
                    {
                        SyncSec = DateTime.Now.Second;

                        if ((PcDateTime.Now - _sendTesterPacket).TotalSeconds > 5)
                        {
                            SendPacket(new PkSyncTest());
                            _sendTesterPacket = PcDateTime.Now;
                        }

                        if (_reqPackCnt > 1)
                        {
                            Thread.Sleep(10);
                            continue;
                        }
                        
                        if (_workProcess++ % 1000 == 0)
                        {
                            Logger.FileLogger.AppendLine(LogLevel.Info, "Work SyncCommand=[{0}], NAME ={1}, START=[{2:00000#}], LENGTH=[{3:00000#}], {4}", SyncCommand, SMName, SyncStart, SyncSize, _workProcess);                        
                        }
                        if (SyncCommand == 0)
                        {
                            PkSyncReadReq pk = new PkSyncReadReq()
                            {
                                ReqTime = DateTime.Now,
                                Name = SMName,
                                Start = SyncStart,
                                Length = SyncSize,
                            };
                            SendPacket(pk);

                            lock (this) _reqPackCnt++;
                        }
                        else if (SyncCommand == 1)
                        {
                            byte[] read = new byte[SyncSize];
                            _shardMem.ReadBytes(SyncStart, SyncSize, out read);

                            PkSyncWriteReq pk = new PkSyncWriteReq()
                            {
                                ReqTime = DateTime.Now,
                                Name = SMName,
                                Start = SyncStart,
                                Length = SyncSize,
                                WriteBytes = read,
                            };

                            SendPacket(pk);
                            lock (this) _reqPackCnt++;
                        }
                    }

                    Thread.Sleep(SyncTime);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SendPacket(Packet pk)
        {
            _session.SendPacket(pk);
        }
        public void Stop()
        {
            _running = false;
            _worker.Join();
            _session.Stop();
            _shardMem.Close();
        }
        public void Disconnect()
        {
            _session.Disconnect();
        }
    }
}
