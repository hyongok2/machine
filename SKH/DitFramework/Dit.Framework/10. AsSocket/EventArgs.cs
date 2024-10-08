using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Dit.Framework.Net.AsSocket
{
    public class AsSocketErrorEventArgs : EventArgs
    {
        private readonly Exception exception;
        private readonly int id = 0;
        public AsSocketErrorEventArgs(int id, Exception exception)
        {
            this.id = id;
            this.exception = exception;
        }
        public Exception AsSocketException
        {
            get { return this.exception; }
        }
        public int ID
        {
            get { return this.id; }
        }
    }

    public class AsSocketConnectionEventArgs : EventArgs
    {
        private readonly int id = 0;
        public AsSocketConnectionEventArgs(int id)
        {
            this.id = id;
        }
        public int ID
        {
            get { return this.id; }
        }
    }

    public class AsSocketSendEventArgs : EventArgs
    {
        private readonly int id = 0;
        private readonly int sendBytes;

        public AsSocketSendEventArgs(int id, int sendBytes)
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

    public class AsSocketReceiveEventArgs : EventArgs
    {
        private readonly int id = 0;
        private readonly int receiveBytes;
        private readonly byte[] receiveData;

        public AsSocketReceiveEventArgs(int id, int receiveBytes, byte[] receiveData)
        {
            this.id = id;
            this.receiveBytes = receiveBytes;
            this.receiveData = receiveData;
        }
        public int ReceiveBytes
        {
            get { return this.receiveBytes; }
        }
        public byte[] ReceiveData
        {
            get { return this.receiveData; }
        }
        public int ID
        {
            get { return this.id; }
        }
    }

    public class AsSocketAcceptEventArgs : EventArgs
    {
        private readonly Socket conn;
        public AsSocketAcceptEventArgs(Socket conn)
        {
            this.conn = conn;
        }
        public Socket Worker
        {
            get { return this.conn; }
        }
    }
}
