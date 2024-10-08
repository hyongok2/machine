using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Dit.Framework.HWControl.CharReader
{
    /// <summary>
    /// Congex 사 BCR DM150 제품
    /// RS232 Serial 사용
    /// Custom Command - Serial Trigger Enable 설정 필요
    /// since 19.06.07
    /// </summary>
    public class BCR_DM150
    {
        private SerialPort _sp;
        private int _readTimeout;
        private bool _waitForResponse;
        private string _readValue;
        private List<byte> _lstRecvBytes = new List<byte>();
        private string _triggerCmd;

        public string ReadValue { get { return _readValue; } }
        public bool IsReadValueError
        {
            get
            {
                return _readValue.Length == 0 || _readValue[0] == 'E';
            }
        }

        public BCR_DM150(string portName, string triggerCmd = "+", int readTimeout = 5000)
        {
            _waitForResponse = false;

            _triggerCmd = triggerCmd;

            _sp = new SerialPort();
            _sp.PortName = portName;
            _sp.ReadTimeout = _readTimeout = readTimeout;
            _sp.BaudRate = (int)115200;
            //아래 Default값.
            //_ffuSP.DataBits = (int)8;
            //_ffuSP.Parity = Parity.None;
            //_ffuSP.StopBits = StopBits.One;
            //_ffuSP.WriteTimeout = (int)100;
            _sp.DataReceived += new SerialDataReceivedEventHandler(_sp_DataReceived);
        }

        private void _sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            _waitForResponse = false;
            _readTimeout = _sp.ReadTimeout;

            try
            {
                string str = _sp.ReadLine();
                _readValue = str;
            }
            catch (Exception ex)
            {
                _readValue = "E-Read Exception" + ex.Message;
            }
        }

        public bool Open()
        {
            try
            {
                _sp.Open();
            }
            catch (System.IO.IOException SerialPortNullEx)
            {
                _readValue = "E-Open Exception1" + SerialPortNullEx.Message;
                return false;
            }
            catch (Exception ex)
            {
                _readValue = "E-" + ex.Message;
                return false;
            }

            if (!_sp.IsOpen)
            {
                _readValue = "E-Open Exception2";
                return false;
            }
            return true;
        }
        public void Stop()
        {
            SerialPortClose();
        }

        private bool SerialPortClose()
        {
            if (!_sp.IsOpen)
                return false;

            _sp.Close();
            return true;
        }
        public void GetBarcode()
        {
            try
            {
                _readValue = string.Empty;
                _sp.Write(_triggerCmd);
            }
            catch (Exception ex)
            {
                _readValue = "E-Write Exception" + ex.Message;
            }
        }
    }
}
