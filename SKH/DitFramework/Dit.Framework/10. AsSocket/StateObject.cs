using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Dit.Framework.Net.AsSocket
{
    public class StateObject
    {
        private const int BUFFER_SIZE = 8192;
        private Socket worker;
        private byte[] buffer;

        public StateObject(Socket worker)
        {
            this.worker = worker;
            this.buffer = new byte[BUFFER_SIZE];
        }
        public Socket Worker
        {
            get { return this.worker; }
            set { this.worker = value; }
        }
        public byte[] Buffer
        {
            get { return this.buffer; }
            set { this.buffer = value; }
        }
        public int BufferSize
        {
            get { return BUFFER_SIZE; }
        }
    }
    
}
