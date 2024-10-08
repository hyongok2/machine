using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.FrameworkSingle.Net.ServerClinet;
using Dit.Framework.Net.AsyncSocket;
using DitSharedMemoryPacket;
using DitSharedMemorySvr.Log;
using Dit.Framework.Log;
using System.Threading;

namespace DitSharedMemorySvr
{
    public class SMSvrSession : TcpServerSession
    {
        private SeverManager _severManager = null;
        private Timer _connectionChk;
        public SMSvrSession(AsyncSocketClient newClient, SeverManager severManager)
            : base(newClient)
        {
            _severManager = severManager;
            _connectionChk = new Timer(CheckConnection, null, 5000, 5000);
            this.OnClose += new DNetCloseEventHandler(SyncSession_OnClose);
            this.OnReceive += new DNetReceiveEventHandler(SyncSession_OnReceive);
            this.OnConnet += new DNetConnectEventHandler(SyncSession_OnConnet);
            this.OnSend += new DNetSendEventHandler(SyncSession_OnSend);
            this.OnError += new DNetErrorEventHandler(SMSvrSession_OnError);
        }

        private void SMSvrSession_OnError(object sender, DNetErrorEventArgs e)
        {
            _severManager.CloseSession(this);
        }
        private void SyncSession_OnSend(object sender, DNetSendEventArgs e)
        {

        }
        private void SyncSession_OnConnet(object sender, DNetConnectionEventArgs e)
        {
        }

        public void CheckConnection(object state)
        {
            this.SendPacket(new PkSyncTest());
        }
        private void SyncSession_OnReceive(object sender, DNetReceiveEventArgs e)
        {
            if (e.RecvPacket is PkSyncReadReq)
            {
                PkSyncReadReq pk = e.RecvPacket as PkSyncReadReq;
                OnPkSyncReadReq(pk);
            }
            else if (e.RecvPacket is PkSyncWriteReq)
            {
                PkSyncWriteReq pk = e.RecvPacket as PkSyncWriteReq;
                OnPkSyncWriteReq(pk);
            }
        }


        private int _recvWriteReq = 0;
        private void OnPkSyncWriteReq(PkSyncWriteReq pk)
        {

            if (GG.SMMgr.LstShardMemory.ContainsKey(pk.Name))
            {
                GG.SMMgr.LstShardMemory[pk.Name].WriteBytes(pk.Start, pk.Length, pk.WriteBytes);
                PkSyncWriteRsp pkRsp = new PkSyncWriteRsp()
                {
                    ReqTime = pk.ReqTime,
                    Name = pk.Name,
                    Start = pk.Start,
                    Length = pk.Length,
                    Result = 0
                };

                this.SendPacket(pkRsp);


                if (_recvWriteReq++ % 1000 == 0)
                    Logger.FileLogger.AppendLine(LogLevel.Info, "WRITE RSP NAME =[{0}], START=[{1}], LENGTH=[{2}]", pk.Name, pk.Start, pk.Length);
            }
            else
            {
                this.SendPacket(new PkSyncWriteRsp()
                {
                    ReqTime = pk.ReqTime,
                    Name = pk.Name,
                    Start = pk.Start,
                    Length = pk.Length,
                    Result = 1,

                });
                Logger.FileLogger.AppendLine(LogLevel.Warning, "READ RSP NAME =[{0}], START=[{1}], LENGTH=[{2}]", pk.Name, pk.Start, pk.Length);
            }
        }

        private int _recvReadReq = 0;
        private void OnPkSyncReadReq(PkSyncReadReq pk)
        {
            //Logger.FileLogger.AppendLine(LogLevel.Info, "WRITE REQ NAME =[{0}], START=[{1}], LENGTH=[{2}]", pk.Name, pk.Start, pk.Length);

            byte[] readBytes = new byte[pk.Length];
            if (GG.SMMgr.LstShardMemory.ContainsKey(pk.Name))
            {
                GG.SMMgr.LstShardMemory[pk.Name].ReadBytes(pk.Start, pk.Length, out readBytes);

                PkSyncReadRsp pkRsp = new PkSyncReadRsp()
                {
                    ReqTime = pk.ReqTime,
                    Name = pk.Name,
                    Start = pk.Start,
                    Length = pk.Length,
                    ReadBytes = readBytes,
                    Result = 0
                };

                this.SendPacket(pkRsp);

                if (_recvWriteReq++ % 1000 == 0)
                    Logger.FileLogger.AppendLine(LogLevel.Info, "READ RSP NAME =[{0}], START=[{1}], LENGTH=[{2}]", pk.Name, pk.Start, pk.Length);
            }
            else
            {
                this.SendPacket(new PkSyncReadRsp()
                {
                    ReqTime = pk.ReqTime,
                    Name = pk.Name,
                    Start = pk.Start,
                    Length = pk.Length,
                    ReadBytes = readBytes,
                    Result = 1,
                });
                //Logger.FileLogger.AppendLine(LogLevel.Warning, "WRITE RSP NAME =[{0}], START=[{1}], LENGTH=[{2}]", pk.Name, pk.Start, pk.Length);
            }
        }
        private void SyncSession_OnClose(object sender, DNetConnectionEventArgs e)
        {
            _severManager.CloseSession(this);
        }
    }
}
