using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Dit.Framework.Net.AsyncSocket
{
    public delegate void AsyncSocketErrorEventHandler(object sender, AsyncSocketErrorEventArgs e);
    public delegate void AsyncSocketConnectEventHandler(object sender, AsyncSocketConnectionEventArgs e);
    public delegate void AsyncSocketCloseEventHandler(object sender, AsyncSocketConnectionEventArgs e);
    public delegate void AsyncSocketSendEventHandler(object sender, AsyncSocketSendEventArgs e);
    public delegate void AsyncSocketReceiveEventHandler(object sender, AsyncSocketReceiveEventArgs e);
    public delegate void AsyncSocketAcceptEventHandler(object sender, AsyncSocketAcceptEventArgs e);

    public class AsyncSocketClass
    {
        protected int id;
        protected Socket conn = null;

        // Event Handler
        public event AsyncSocketErrorEventHandler OnError;
        public event AsyncSocketConnectEventHandler OnConnet;
        public event AsyncSocketCloseEventHandler OnClose;
        public event AsyncSocketSendEventHandler OnSend;
        public event AsyncSocketReceiveEventHandler OnReceive;
        public event AsyncSocketAcceptEventHandler OnAccept;
         
        public AsyncSocketClass()
        {
            this.id = -1;
        }

        public AsyncSocketClass(int id)
        {
            this.id = id;
        }

        public int ID
        {
            get { return this.id; }
        }
        public Socket Connection
        {
            get { return this.conn; }
            set { this.conn = value; }
        }

        protected virtual void ErrorOccured(AsyncSocketErrorEventArgs e)
        {
            AsyncSocketErrorEventHandler handler = OnError;

            if (handler != null)
                handler(this, e);
        }
        protected virtual void Connected(AsyncSocketConnectionEventArgs e)
        {
            AsyncSocketConnectEventHandler handler = OnConnet;

            if (handler != null)
                handler(this, e);
        }
        protected virtual void Closed(AsyncSocketConnectionEventArgs e)
        {
            AsyncSocketCloseEventHandler handler = OnClose;

            if (handler != null)
                handler(this, e);
        }
        protected virtual void Sent(AsyncSocketSendEventArgs e)
        {
            AsyncSocketSendEventHandler handler = OnSend;

            if (handler != null)
                handler(this, e);
        }
        protected virtual void Received(AsyncSocketReceiveEventArgs e)
        {
            AsyncSocketReceiveEventHandler handler = OnReceive;

            if (handler != null)
                handler(this, e);
        }
        protected virtual void Accepted(AsyncSocketAcceptEventArgs e)
        {
            AsyncSocketAcceptEventHandler handler = OnAccept;

            if (handler != null)
                handler(this, e);
        }

        public virtual bool Send(byte[] buffer)
        {
            return false;   
        }

        public virtual bool Connect(string hostAddress, int port)
        {
            return false;  
        }
        public virtual void Close()
        {}        
    } // end of class AsyncSocketClass

}
