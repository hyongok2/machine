using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Dit.Framework.Net.AsSocket
{
    public delegate void AsSocketErrorEventHandler(object sender, AsSocketErrorEventArgs e);
    public delegate void AsSocketConnectEventHandler(object sender, AsSocketConnectionEventArgs e);
    public delegate void AsSocketCloseEventHandler(object sender, AsSocketConnectionEventArgs e);
    public delegate void AsSocketSendEventHandler(object sender, AsSocketSendEventArgs e);
    public delegate void AsSocketReceiveEventHandler(object sender, AsSocketReceiveEventArgs e);
    public delegate void AsSocketAcceptEventHandler(object sender, AsSocketAcceptEventArgs e);

    //비동기 통신 라이브러리(고성능은 아님. 고성능이 필요할 경우. 별도 개발 요함)     
    public class AsSocketClass
    {
        protected int id;
        protected Socket conn = null;

        //이벤트
        public event AsSocketErrorEventHandler OnError;
        public event AsSocketConnectEventHandler OnConnet;
        public event AsSocketCloseEventHandler OnClose;
        public event AsSocketSendEventHandler OnSend;
        public event AsSocketReceiveEventHandler OnReceive;
        public event AsSocketAcceptEventHandler OnAccept;

        //프로퍼티
        public int ID
        {
            get { return this.id; }
        }
        public Socket Connection
        {
            get { return this.conn; }
            set { this.conn = value; }
        }

        //메소드 - 생성자
        public AsSocketClass()
        {
            this.id = -1;
        }
        public AsSocketClass(int id)
        {
            this.id = id;
        }

        //메소드 - 통신 함수 재정의 필요
        protected virtual void ErrorOccured(AsSocketErrorEventArgs e)
        {
            AsSocketErrorEventHandler handler = OnError;

            if (handler != null)
                handler(this, e);
        }
        protected virtual void Connected(AsSocketConnectionEventArgs e)
        {
            AsSocketConnectEventHandler handler = OnConnet;

            if (handler != null)
                handler(this, e);
        }
        protected virtual void Closed(AsSocketConnectionEventArgs e)
        {
            AsSocketCloseEventHandler handler = OnClose;

            if (handler != null)
                handler(this, e);
        }
        protected virtual void Send(AsSocketSendEventArgs e)
        {
            AsSocketSendEventHandler handler = OnSend;

            if (handler != null)
                handler(this, e);
        }
        protected virtual void Received(AsSocketReceiveEventArgs e)
        {
            AsSocketReceiveEventHandler handler = OnReceive;

            if (handler != null)
                handler(this, e);
        }
        protected virtual void Accepted(AsSocketAcceptEventArgs e)
        {
            AsSocketAcceptEventHandler handler = OnAccept;

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
        {
        }        
    } 
}
