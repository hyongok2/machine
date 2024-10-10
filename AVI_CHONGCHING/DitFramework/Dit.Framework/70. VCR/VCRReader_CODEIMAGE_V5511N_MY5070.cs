using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Timers;
using System.Globalization;
using System.Diagnostics;
using System.Windows.Forms;
using Dit.Framework.Comm;

namespace Dit.Framework.VCR
{
    public class VCRReader_CODEIMAGE_V5511N_MY5070
    {        
        public const byte CR = 0x0D;

        private SerialPort _serial;
        private int _readTimeout;
        private int _readInterval;
        private List<byte> _lstRecvBytes = new List<byte>();
        private byte[] _totalBytes = new byte[8192];
        private int _totalSize = 0;
        protected string _vcrReadData = string.Empty;

        public VCRReader_CODEIMAGE_V5511N_MY5070(string portName, int readTimeout = 5000)
        {
            _readInterval = 500;
            _serial = new SerialPort();

            _serial.PortName = portName;
            _serial.ReadTimeout = _readTimeout = readTimeout;
            _serial.BaudRate = (int)9600;
            _serial.DataReceived += new SerialDataReceivedEventHandler(VCRDataReceived);
            Initialize();
        }
        public void VCRDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int recvSize = _serial.BytesToRead;

            if (recvSize != 0)
            {
                //Console.WriteLine(string.Format(" Recv Size {0} {1}", DateTime.Now.ToString("hh:MM:ss.fff"), recvSize));
                byte[] recvBytes = new byte[recvSize];
                _serial.Read(recvBytes, 0, recvSize);

                Array.Copy(recvBytes, 0, _totalBytes, _totalSize, recvSize);
                _totalSize = _totalSize + recvSize;

                //패킷 파싱   
                while (true)
                {
                    int eofInx = _totalBytes.IndexOfAny(CR);

                    if (eofInx > 0 && eofInx + 1 <= _totalSize)
                    {
                        int msgSize = eofInx + 1;
                        byte[] msgBytes = new byte[msgSize];
                        Array.Copy(_totalBytes, 0, msgBytes, 0, msgSize);

                        Array.Copy(_totalBytes, msgSize, _totalBytes, 0, _totalSize - msgSize);
                        _totalSize = _totalSize - msgSize;
                        _serial.DiscardInBuffer();
                        Receive(msgBytes);
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        public virtual bool Initialize()
        {
            //_O2UpdateTimer.Interval = _readInterval;
            //_O2UpdateTimer.Elapsed += new ElapsedEventHandler(UpdateO2Status);
            return true;
        }
        public virtual bool Open()
        {
            try
            {
                _serial.Open();
            }
            catch (System.Exception ex)
            {
                return false;
            }

            if (!_serial.IsOpen)
            {
                return false;
            }
            return true;
        }
        public virtual bool Close()
        {
            if (!_serial.IsOpen)
                return false;

            _serial.Close();
            return true;
        }
        public virtual void Decode()
        {
            _serial.DiscardInBuffer();
            _serial.DiscardOutBuffer();
            byte[] CmdReadVCR = new byte[5];

            CmdReadVCR[0] = 0x1B;
            CmdReadVCR[1] = 0x50;
            CmdReadVCR[2] = 0x00;
            CmdReadVCR[3] = 0xFF;
            CmdReadVCR[4] = CR;

            _serial.Write(CmdReadVCR, 0, CmdReadVCR.Length);
        }
        public virtual bool Receive(byte[] bytes)
        {
            string suffix = bytes.GetString(0, 1);

            if (suffix.CompareTo("V") == 0)
            {
                int length = bytes.GetDigit(1, 2);
                _vcrReadData = bytes.GetString(3, length);
                return true;
            }
            else if (suffix.CompareTo("R") == 0)
            {
                return false;
            }
            else
            {
                _vcrReadData = bytes.GetString(0, bytes.Length - 1);
                return true;
            }
        }
        //TEST CODE
        public static string ShowAvailablePort()
        {
            string ports = "";
            foreach (string portName in SerialPort.GetPortNames())
                ports += portName + "\r\n";

            return ports;
        }
        public bool IsOpen { get { return _serial.IsOpen; } }

        public bool ReConnect(string portName)
        {
            Close();
            _serial.PortName = portName;
            return Open();
        }
        public string CurrentPort
        {
            get
            {
                return _serial.PortName;
            }
            set
            {
                _serial.PortName = value;
            }
        }
    }
}