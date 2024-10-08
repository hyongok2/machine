using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Modbus;
using System.Threading;
using System.Net.Sockets;

namespace Dit.Framework.HWControl.DisplacementSensor
{
    /// <summary>
    /// ZW7000T 변위센서 TCP통신 인터페이스
    /// date 181207
    /// since 181207
    /// </summary>
    public class ZW7000T
    {
        public enum EmZW7000TCmd
        {
            GetTask1,        
            GetTask2,
            GetTask3,
            GetTask4,
        } 

        protected TcpClient _comm = null;
        protected Thread _worker = null;
        protected bool _isRunning = false;
        protected bool _isGetting = false;
        protected bool _isSuccess = false;
        public bool IsSuccess { get { return _isSuccess; } }
        public bool IsReadComplete { get { return !_isGetting; } }

        private int _maxReadCount = 10;

        protected EmZW7000TCmd _currentCmd = EmZW7000TCmd.GetTask1;
        protected double _returnValue;
        public double MeasurementValue { get { return _returnValue; } }

        public ZW7000T()
        {

        }

        public void Open(string ip, int port)
        {
            _comm = new TcpClient(ip, port);
            _worker = new Thread(new ThreadStart(GetDataWorker));
            _isRunning = true;
            _worker.Start();
        }

        public void StartGetData(EmZW7000TCmd cmd)
        {
            _returnValue = -9999;
            _curCount = 0;
            _currentCmd = cmd;
            _isSuccess = false;
            _isGetting = true;
            _readBuf = new byte[100];            
        }

        
        private int _curCount;
        private double _ret;
        private void GetDataWorker()
        {
            while (_isRunning)
            {
                System.Threading.Thread.Sleep(1);
                if (_isGetting)
                {
                    if (GetData(_currentCmd, out _ret) == true)
                    {
                        _returnValue = _ret;
                        _isSuccess = true;
                        _isGetting = false;
                    }
                    else
                    {
                        _curCount++;

                        if (_curCount > _maxReadCount)
                        {
                            _isSuccess = false;
                            _isGetting = false;
                        }
                    }
                }
            }
        }

        private byte[] _readBuf = null;
        private bool GetData(EmZW7000TCmd cmd, out double ret)
        {
            ret = -9999;
            if (_comm.GetStream().CanWrite)
            {
                byte[] cmdByte = GetCmd(cmd);
                _comm.GetStream().Write(cmdByte, 0, cmdByte.Length);
                if (_comm.GetStream().CanRead)
                {
                    _comm.GetStream().Read(_readBuf, 0, 100);

                    int start = 0;
                    for (int i = 0; i < _readBuf.Length; ++i)
                    {
                        if (_readBuf[i] == (byte)'\r')
                            start = i - 11;
                    }

                    if (start >= 0)
                    {
                        List<byte> data = new List<byte>();
                        for (int i = 0; i < 11; ++i)
                        {
                            if (_readBuf[start + i] != (byte)' ')
                            {
                                data.Add(_readBuf[start + i]);
                            }
                        }
                        string str = string.Empty;
                        str = Encoding.Default.GetString(data.ToArray());
                        if (double.TryParse(str, out ret))
                        {
                            ret /= 1000000;
                            return true;
                        }
                        ret = -9999;
                        return false;
                    }
                    return false;
                }
            }
            return false;
        }

        public byte[] GetCmd(EmZW7000TCmd cmd)
        {
            switch(cmd)
            {
                case EmZW7000TCmd.GetTask1:
                    return new byte[] { (byte)'M', (byte)'S', (byte)' ', (byte)'0', (byte)'\r' };                    
                case EmZW7000TCmd.GetTask2:
                    return new byte[] { (byte)'M', (byte)'S', (byte)' ', (byte)'1', (byte)'\r' };
                case EmZW7000TCmd.GetTask3:
                    return new byte[] { (byte)'M', (byte)'S', (byte)' ', (byte)'2', (byte)'\r' };
                case EmZW7000TCmd.GetTask4:
                    return new byte[] { (byte)'M', (byte)'S', (byte)' ', (byte)'3', (byte)'\r' };
                default:
                    return null;
            }
        }
    }
}
