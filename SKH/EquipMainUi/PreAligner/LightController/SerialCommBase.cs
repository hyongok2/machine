using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.PreAligner
{
    public abstract class SerialCommBase
    {
        protected SerialPort _serialPort;

        private int _readTimeout;
        private int _readInterval;
        private bool _waitForResponse;

        public SerialCommBase(string portName, int baudRate = 9600, int dataBits = 8, 
            StopBits stopBits = StopBits.One, Parity parity = Parity.None, int readTimeout = 5000)
        {
            _waitForResponse = false;
            _serialPort = new SerialPort();

            _serialPort.PortName = portName;
            _serialPort.ReadTimeout = _readTimeout = readTimeout;
            _serialPort.BaudRate = baudRate;
            _serialPort.DataBits = dataBits;
            _serialPort.StopBits = stopBits;
            _serialPort.Parity = parity;

            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
        }

        public SerialCommBase(SerialPort port)
        {
            _serialPort = port;
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            _waitForResponse = false;
            _readTimeout = _serialPort.ReadTimeout;
            try
            {
                var msg = _serialPort.ReadExisting();
                if (msg == null || msg.Length == 0) return;
                ReceivedData(msg);
            }
            catch (Exception ex)
            {
                ParsingReceivedData(ex.Message);
            }
        }

        protected abstract void ReceivedData(string message);
        protected abstract void ParsingReceivedData(string message);
        public string Port { get { return _serialPort.PortName; } }
        public bool IsOpen()
        {
            return _serialPort.IsOpen;
        }
        public bool Open()
        {
            try
            {
                //_serialPort.Open();
            }
            catch (Exception ex)
            {
                return false;
            }

            if (!_serialPort.IsOpen)
            {
                return false;
            }
            return true;
        }
        public bool ReOpen(string port)
        {
            if (IsOpen())
            {
                SerialPortClose();
            }

            _serialPort.PortName = port;

            return Open();
        }
        public bool Stop()
        {
            return SerialPortClose();
        }
        private bool SerialPortClose()
        {
            if (!_serialPort.IsOpen)
                return false;

            _serialPort.Close();
            return true;
        }
        protected string CalculateChecksum(byte[] data)
        {
            int checksum = 0;
            checksum = data[0] ^ data[1];
            for (int i = 2; i < data.Length; i++)
            {
                checksum = checksum ^ data[i];
            }

            return checksum.ToString("X2");
        }
        protected string CalculateChecksum(List<byte> data)
        {
            int checksum = 0;
            checksum = data[0] ^ data[1];
            for (int i = 2; i < data.Count; i++)
            {
                checksum = checksum ^ data[i];
            }

            return checksum.ToString("X2");
        }
        protected string CheckCheckSum(byte[] data)
        {
            byte[] byteToCalculate = new byte[data.Length - 1];
            Array.Copy(data, byteToCalculate, byteToCalculate.Length);

            int checksum = 0;
            checksum = byteToCalculate[0] ^ byteToCalculate[1];
            for (int i = 2; i < byteToCalculate.Length; i++)
            {
                checksum = checksum ^ byteToCalculate[i];
            }

            return checksum.ToString("X2");
        }

        public void ClearSerialBuffer()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.ReadExisting();
            }

        }
    }
}
