using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Dit.FrameworkSingle.Net.ServerClinet
{
    public delegate void DNetErrorEventHandler(object sender, DNetErrorEventArgs e);
    public delegate void DNetConnectEventHandler(object sender, DNetConnectionEventArgs e);
    public delegate void DNetCloseEventHandler(object sender, DNetConnectionEventArgs e);
    public delegate void DNetSendEventHandler(object sender, DNetSendEventArgs e);
    public delegate void DNetReceiveEventHandler(object sender, DNetReceiveEventArgs e);
    public delegate void DNetAcceptEventHandler(object sender, DNetAcceptEventArgs e);

    public class DNetErrorEventArgs : EventArgs
    {
        private readonly Exception exception;
        private readonly int id = 0;
        public DNetErrorEventArgs(int id, Exception exception)
        {
            this.id = id;
            this.exception = exception;
        }
        public Exception AsyncSocketException
        {
            get { return this.exception; }
        }
        public int ID
        {
            get { return this.id; }
        }
    }

    public class DNetConnectionEventArgs : EventArgs
    {
        private readonly int id = 0;
        public DNetConnectionEventArgs(int id)
        {
            this.id = id;
        }
        public int ID
        {
            get { return this.id; }
        }
    }

    public class DNetSendEventArgs : EventArgs
    {
        private readonly int id = 0;
        private readonly int sendBytes;

        public DNetSendEventArgs(int id, int sendBytes)
        {
            this.id = id;
            this.sendBytes = sendBytes;
        }
        public int SendBytes
        {
            get { return this.sendBytes; }
        }
        public int ID
        {
            get { return this.id; }
        }
    }

    public class DNetReceiveEventArgs : EventArgs
    {
        private readonly int id = 0;
        public Packet RecvPacket
        {
            get;
            set;
        }
        public DNetReceiveEventArgs(int id, Packet packet )
        {
            this.id = id;
            this.RecvPacket = packet;
        }      
        public int ID
        {
            get { return this.id; }
        }
    }

    public class DNetAcceptEventArgs : EventArgs
    {   
        public DNetAcceptEventArgs(TcpServerSession conn)
        {
            this.Worker = conn;
        }
        public TcpServerSession Worker
        {
            get;
            set;
        }
    }
}
