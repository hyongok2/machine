using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Sockets;
using System.Diagnostics;

namespace Dit.Framework.PMAC
{
    public class ExcomData
    {
        public byte RequestType;
        public byte Request;
        public short Value;
        public short Index;
        public short Length;
        public byte[] Data = new byte[1492];

        public byte[] ToByte(int dataLen)
        {
            List<byte> buf = new List<byte>();
            buf.Add(RequestType);
            buf.Add(Request);
            buf.AddRange(BitConverter.GetBytes(Value));
            buf.AddRange(BitConverter.GetBytes(Index));
            buf.AddRange(BitConverter.GetBytes(Length));

            if (dataLen > 0)
            {
                byte[] data = new byte[dataLen];
                Array.Copy(this.Data, data, dataLen);
                buf.AddRange(data);
            }

            return buf.ToArray();
        }

        public byte[] ToByte()
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, this);
                    return stream.ToArray();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            return null;
        }

        public bool SetByte(byte[] data)
        {
            if (data.Length < 8)
                return false;

            RequestType = data[0];
            Request = data[1];
            Value = BitConverter.ToInt16(data, 2);
            Index = BitConverter.ToInt16(data, 4);
            Length = BitConverter.ToInt16(data, 6);
            Array.Copy(data, 8, this.Data, 0, 1492);
            return true;
        }
    }

    public enum ExcomRequestType
    {
        Read = 0x10,
        Write = 0x11,
        Nak = 0x15
    }

    /// <summary>
    /// brief   Pmac Excom 통신
    /// date    19.03.29
    /// </summary>
    public class PmacExcom
    {
        private const int MaxDataLen = 1400;
        private const int ArrayMax = 15360;

        private string _ip;
        private int _port;
        private Socket _socket;
        private bool _isConnected;
        //private Thread receiveThread;
        //private byte[] _readMemory;

        public PmacExcom(string ip, int port)
        {
            _ip = ip;
            _port = port;
            //_readMemory = new byte[102400];

            //receiveThread = new Thread(new ThreadStart(Receive));
            //receiveThread.IsBackground = true;
            //receiveThread.Start();
        }

        public bool IsConnected { get { return _socket.Connected && _isConnected == true; } }

        public bool Open()
        {
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                _socket.Connect(_ip, _port);
                _isConnected = true;
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Open(string ip, int port)
        {
            _ip = ip;
            _port = port;
            return Open();
        }

        public bool Close()
        {
            if (_socket != null)
            {
                _isConnected = false;
                _socket.Disconnect(true);
                _socket = null;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startAddr">4byte단위로만 지원 (입력값/4)</param>
        /// <param name="byteLen"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetMemory(int startAddr, int byteLen, byte[] value)
        {
            if (_isConnected == false) return false;

            int bufLen = 0, timeout = 100,
                bufRemain = byteLen, bufIdx = 0;

            lock (this)
            {
                while (bufRemain > 0)
                {
                    if (bufRemain > MaxDataLen)
                        bufLen = MaxDataLen;
                    else
                        bufLen = bufRemain;

                    bufRemain -= bufLen;

                    if (startAddr + bufLen > ArrayMax)
                    {
                        bufRemain = 0;
                        bufLen = ArrayMax - startAddr;

                        if (bufLen <= 0)
                            throw new Exception("Pmac Excom Memory Write Access Violation Error");
                    }

                    ExcomData cmd = new ExcomData();
                    cmd.RequestType = (byte)ExcomRequestType.Write;
                    cmd.Index = (short)(startAddr / 4);
                    cmd.Length = (short)(bufLen / 4);

                    Array.Copy(value, bufIdx, cmd.Data, 0, bufLen);

                    SocketError sendRet;
                    ExcomData recv = new ExcomData();
                    byte[] recvByte = new byte[1500];

                    try
                    {
                        _socket.Send(cmd.ToByte(bufLen), 0, 8 + bufLen, SocketFlags.None, out sendRet);
                        if (sendRet != SocketError.Success)
                            return false;
                        _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeout);
                        if (_socket.Receive(recvByte) <= 0)
                            return false;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                    recv.SetByte(recvByte);
                    if (recv.RequestType == (byte)ExcomRequestType.Nak)
                        return false;

                    startAddr += bufLen;
                    bufIdx += bufLen;
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startAddr">4byte단위로만 지원 (입력값/4)</param>
        /// <param name="byteLen"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool GetMemory(int startAddr, int byteLen, out byte[] value)
        {
            value = new byte[byteLen];
            if (_isConnected == false) return false;

            int bufLen = 0, timeout = 100,
                bufRemain = byteLen, bufIdx = 0;

            lock (this)
            {
                while (bufRemain > 0)
                {
                    if (bufRemain > MaxDataLen)
                        bufLen = MaxDataLen;
                    else
                        bufLen = bufRemain;

                    bufRemain -= bufLen;

                    if (startAddr + bufLen > ArrayMax)
                    {
                        bufRemain = 0;
                        bufLen = ArrayMax - startAddr;
                        if (bufLen <= 0)
                            throw new Exception("Pmac Excom Memory Read Access Violation Error");
                    }

                    ExcomData cmd = new ExcomData();
                    cmd.RequestType = (byte)ExcomRequestType.Read;
                    cmd.Index = (short)(startAddr / 4);
                    cmd.Length = (short)(bufLen / 4);

                    SocketError sendRet;
                    ExcomData recv = new ExcomData();
                    byte[] recvByte = new byte[1500];

                    try
                    {
                        _socket.Send(cmd.ToByte(bufLen), 0, 8 + bufLen, SocketFlags.None, out sendRet);
                        if (sendRet != SocketError.Success)
                            return false;
                        _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeout);
                        if (_socket.Receive(recvByte) <= 0)
                            return false;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                    recv.SetByte(recvByte);
                    if (recv.RequestType == (byte)ExcomRequestType.Nak)
                        return false;

                    Array.Copy(recv.Data, 0, value, bufIdx, bufLen);
                    startAddr += bufLen;
                    bufIdx += bufLen;
                }
            }

            return true;
        }

        private void Receive()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1);
                if (_isConnected == true)
                {
                    //byte[] recevieBuffer = new byte[512];
                    //int length = _socket.Receive(
                    //recevieBuffer, recevieBuffer.Length, SocketFlags.None
                    //);
                }
            }
        }
    }
}
