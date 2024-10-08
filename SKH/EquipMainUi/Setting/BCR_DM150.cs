using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace EquipMainUi.Setting
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
        private string _errorStr;

        public string ReadValue { get { return _readValue; } }
        private Regex _readValVerify = new Regex("^*$");
        private Regex _readValVerify2 = new Regex("^*$");
        public bool IsReadComplete { get { return _readValue.Length != 0; } }
        public bool IsReadValueError
        {
            get
            {
                Logger.OCRBCRLog.AppendLine(LogLevel.Error, "IsReadValueError : {0}", _readValue);
                Logger.OCRBCRLog.AppendLine(LogLevel.Error, "{0},{1},{2},{3},{4},{5},{6}", _readValue == null, _readValue == string.Empty, _readValue.Length == 0, _readValue == _errorStr, _readValue.Length < 8, _readValVerify.IsMatch(_readValue) == false, _readValVerify2.IsMatch(_readValue) == false);
                return _readValue == null
                    || _readValue == string.Empty
                    || _readValue.Length == 0
                    || _readValue == _errorStr
                    || _readValue.Length < 8
                    || (_readValVerify.IsMatch(_readValue) == false && _readValVerify2.IsMatch(_readValue) == false)
                    ;
            }
        }

        public string Port { get { return _sp.PortName; } }

        public BCR_DM150(string portName, string triggerCmd = "+", string errorStr = "nodata", int readTimeout = 5000)
        {
            _waitForResponse = false;

            _triggerCmd = triggerCmd;
            _errorStr = errorStr;

            _sp = new SerialPort();
            _sp.PortName = portName;
            _sp.ReadTimeout = _readTimeout = readTimeout;
            _sp.BaudRate = (int)115200;
            _readValue = string.Empty;
            //아래 Default값.
            //_ffuSP.DataBits = (int)8;
            //_ffuSP.Parity = Parity.None;
            //_ffuSP.StopBits = StopBits.One;
            //_ffuSP.WriteTimeout = (int)100;
            _sp.DataReceived += new SerialDataReceivedEventHandler(_sp_DataReceived);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reqularExcpress"></param>
        /// <returns>정규식 생성 실패 시 false</returns>
        public bool SetReadValueRegex(string reqularExcpress, string reqularExcpress2)
        {
            try
            {
                _readValVerify = new Regex(reqularExcpress);
                _readValVerify2 = new Regex(reqularExcpress2);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void _sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            _waitForResponse = false;
            _readTimeout = _sp.ReadTimeout;

            try
            {
                string str = _sp.ReadLine();
                Logger.OCRBCRLog.AppendLine(LogLevel.Info, string.Format("BCR Read Data(serial read line) : {0}", str));

                if (str != string.Empty || str.Length > 1)
                    str = str.Replace("\r", "");
                if (str.Contains(_errorStr) && str.Length > 6)
                    str = str.Replace(_errorStr, "");

                _readValue = str;
                Logger.OCRBCRLog.AppendLine(LogLevel.Info, string.Format("BCR Read Data(read value) : {0}", _readValue));
            }
            catch (Exception ex)
            {
                _readValue = "?-Read Exception" + ex.Message;
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
                _readValue = "?-Open Exception1" + SerialPortNullEx.Message;
                return false;
            }
            catch (Exception ex)
            {
                _readValue = "?-" + ex.Message;
                return false;
            }

            if (!_sp.IsOpen)
            {
                _readValue = "?-Open Exception2";
                return false;
            }
            return true;
        }
        public bool ReOpen(string port)
        {
            if (IsOpen())
                return false;

            _sp.PortName = port;

            return Open();
        }
        public bool IsOpen()
        {
            return _sp.IsOpen;
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
                _readValue = "?-Write Exception" + ex.Message;
            }
        }
    }
}
