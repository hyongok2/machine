using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Dit.Framework.Net.AsyncSocket
{
    public class AsyncSocketErrorEventArgs : EventArgs
    {
        private readonly Exception exception;
        private readonly int id = 0;
        public AsyncSocketErrorEventArgs(int id, Exception exception)
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

    public class AsyncSocketConnectionEventArgs : EventArgs
    {
        private readonly int id = 0;
        public AsyncSocketConnectionEventArgs(int id)
        {
            this.id = id;
        }
        public int ID
        {
            get { return this.id; }
        }
    }

    public class AsyncSocketSendEventArgs : EventArgs
    {
        private readonly int id = 0;
        private readonly int sendBytes;

        public AsyncSocketSendEventArgs(int id, int sendBytes)
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


    public class AsyncSocketReceiveEventArgs : EventArgs
    {
        private readonly int id = 0;
        private readonly int receiveBytes;
        private readonly byte[] receiveData;

        public AsyncSocketReceiveEventArgs(int id, int receiveBytes, byte[] receiveData)
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


    public class AsyncSocketAcceptEventArgs : EventArgs
    {
        private readonly Socket conn;

        public AsyncSocketAcceptEventArgs(Socket conn)
        {
            this.conn = conn;
        }

        public Socket Worker
        {
            get { return this.conn; }
        }
    }
}
