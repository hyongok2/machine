using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Monitor
{
    public enum RFIDCmd : byte
    {
        SEND_CMD = 0x44,   //'D'
        SEND_PARAM = 0x30, //'0'
        SP = 0x20, //'SP'
        READER1 = 0x31, //'1'
        //READER2 = 0x32, //'2' //RFID제거

        RECV_CMD = 0x41, ///A'
        RECV_CMD_ERROR = 0x45, //'E'
    }
    public class RFIDController
    {
        private SerialPort _rfidSP;

        private int _readTimeout;
        private int _readInterval;
        private bool _waitForResponse;
        
        public event DataEvent dataReceive = null;
        public event DataEvent dataSend = null;

        string commandReader1 = "D0                1E";  //command + sp * 16 + Check sum
        //string commandReader2 = "D0                2F"; //RFID제거

        private string _reader1ReadValue;
        //private string _reader2ReadValue; //RFID제거

        public RFIDController(string portName, int readTimeout = 5000)
        {
            _waitForResponse = false;
            _rfidSP = new SerialPort();

            _rfidSP.PortName = portName;
            _rfidSP.ReadTimeout = _readTimeout = readTimeout;
            _rfidSP.BaudRate = (int)9600;

            _reader1ReadValue = string.Empty;
            //_reader2ReadValue = string.Empty; //RFID제거

            _rfidSP.DataReceived += new SerialDataReceivedEventHandler(RFIDDataReceived);
        }

        public string Port { get { return _rfidSP.PortName; } }

        public bool Open()
        {
            try
            {
                _rfidSP.Open();
                Logger.Log.AppendLine(LogLevel.Error, "RFID : Serial Port Open Success");
            }
            catch (Exception)
            {
                Logger.Log.AppendLine(LogLevel.Error, "RFID : Serial Port Open Error");

                return false;
            }

            if (!_rfidSP.IsOpen)
            {
                return false;
            }
            return true;
        }

        public bool ReOpen(string port)
        {
            if (IsOpen())
                return false;

            _rfidSP.PortName = port;

            return Open();
        }

        public bool IsOpen()
        {
            return _rfidSP.IsOpen;
        }

        public bool Stop()
        {
            return SerialPortClose();
        }
        private bool SerialPortClose()
        {
            if (!_rfidSP.IsOpen)
                return false;

            _rfidSP.Close();
            return true;
        }
        string recvStr = string.Empty;
        private void RFIDDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            _waitForResponse = false;
            _readTimeout = _rfidSP.ReadTimeout;
            try
            {
                recvStr += _rfidSP.ReadExisting();
                Logger.Log.AppendLine(LogLevel.Info, "RFID SERIAL READ BUFFER ({1}) : {0}", recvStr, _rfidSP.PortName);
                //_rfidSP.Read(temp, 0, 20);
                //if(recvStr[0].Equals("A") == false)
                //{
                //    recvStr = string.Empty;
                //}
                if(recvStr[0].Equals('A') == false)
                {
                    recvStr = string.Empty;
                }
                else if((recvStr.Length < 20))
                {
                    return;
                }
                else if(recvStr.Length ==20)
                {
                    RecvData(recvStr);
                }
                else if(recvStr.Length > 20)
                {
                    _rfidSP.ReadExisting();//남은 버퍼 처리
                    recvStr = string.Empty;
                }
            }
            catch (Exception ex)
            {
                RecvData(ex.Message);
            }
        }
        private string _rfid1ID /*,_rfid2ID*/; //RFID제거
        private bool _reader1Success /*,_reader2Success*/; //RFID제거
        public string [] GetReaderReadID
        {
            get
            {
                return new string[1] { _rfid1ID /*,_rfid2ID */}; //RFID제거
            }
        }
        public bool IsReader1Success()
        {
            return _reader1Success;
        }
        //public bool IsReader2Success() //RFID제거
        //{
        //    return _reader2Success;
        //}
        private void RecvData(string str)
        {
            //정상 수신
            //A2IC302060        1O
            //A2IC302060        2L
            if (str[0] == 'A' && str[1] == '2')
            {
                if(str[18] == '1')
                {
                    _rfid1ID = str.Substring(2, 16);
                    _rfid1ID = _rfid1ID.Trim();
                    _reader1Success = true;
                    dataReceive?.Invoke("Reader 1 Read : " + _rfid1ID);
                    //_rfid2ID = string.Empty;
                }
                //else if(str[18] == '2') //RFID제거
                //{
                //    _rfid2ID = str.Substring(2, 16);
                //    _rfid2ID = _rfid2ID.Trim();
                //    _reader2Success = true;
                //    dataReceive?.Invoke("Reader 2 Read : " + _rfid2ID);
                //    _rfid1ID = string.Empty;
                //}
            }
            else if(str[0] == 'E' && str[1] == '2')
            {
                if(str[18] == '1')
                {
                    _rfid1ID = "CAN NOT READ RFID READR1";
                    dataReceive?.Invoke(_rfid1ID);
                }
                //else if(str[18] == '2') //RFID제거
                //{
                //    _rfid2ID = "CAN NOT READ RFID READR2";
                //    dataReceive?.Invoke(_rfid2ID);
                //}
            }
            else
            {
                _rfidSP.ReadExisting();
            }
            recvStr = string.Empty;

        }

        public void ScanTagCmd(RFIDCmd reader)
        {
            byte[] cmd = (reader == RFIDCmd.READER1) ? Encoding.UTF8.GetBytes(commandReader1) : Encoding.UTF8.GetBytes(commandReader1);

            if (reader == RFIDCmd.READER1)
                _reader1Success = false;
            //else if (reader == RFIDCmd.READER2)
            //    _reader2Success = false;
            _reader1Success = false;
            //_reader2Success = false; //RFID제거

            try
            {
                if (_rfidSP.IsOpen == true)
                    _rfidSP.Write(cmd, 0, cmd.Length);
            }
            catch (System.Exception e)
            {
                Logger.Log.AppendLine(LogLevel.Error, string.Format("RFID Tag Scan 오류"));
            }
        }
        public void ScanTagCmd(int readCount = 1)
        {
            _reader1Success = false;
            //_reader2Success = false; //RFID제거

            byte[] cmd1 = Encoding.UTF8.GetBytes(commandReader1);
            //byte[] cmd2 = Encoding.UTF8.GetBytes(commandReader2); //RFID제거

            PlcTimerEx rfidReadTimeover = new PlcTimerEx("RFID Read Delay");
            int step = 10;
            for (int i = 0; i < readCount; i++)
            {
                try
                {
                    if (step == 10)
                    {
                        if ((i % 2) == 0)
                        {
                            _rfidSP.Write(cmd1, 0, cmd1.Length);
                        }
                        //else //RFID제거
                        //{
                        //    _rfidSP.Write(cmd2, 0, cmd2.Length);
                        //}
                        rfidReadTimeover.Start(0, 500);

                        step = 20;
                    }
                    else if (step == 20)
                    {
                        if (_reader1Success /*|| _reader2Success*/)//성공했으면 //RFID제거
                        {
                            rfidReadTimeover.Stop();
                            break;
                        }
                        else if (rfidReadTimeover)
                        {
                            rfidReadTimeover.Stop();
                            step = 10;
                        }

                    }
                }
                catch (System.Exception e)
                {
                    Logger.Log.AppendLine(LogLevel.Error, string.Format("RFID Tag Scan Exception"));
                }
            }
        }

        public string CalculateChecksum(byte[] data)
        {
            int checksum = 0;
            checksum = data[0] ^ data[1];
            for (int i = 2; i < data.Length; i++)
            {
                checksum = checksum ^ data[i];
            }

            return checksum.ToString("X2");
        }
        public string CalculateChecksum(List<byte> data)
        {
            int checksum = 0;
            checksum = data[0] ^ data[1];
            for (int i = 2; i < data.Count; i++)
            {
                checksum = checksum ^ data[i];
            }

            return checksum.ToString("X2");
        }
        public string CheckCheckSum(byte[] data)
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
            if(_rfidSP.IsOpen)
            {
                _rfidSP.ReadExisting();
            }
            
        }
    }
}
