using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows;
using System.Timers;

namespace EquipMainUi.Monitor
{
    //Fan Filter Unit
    public class EFU
    {
        public EFU(int id)
        {
            //this.id = (byte)id;
            ID = (byte)id;
            CurRPM = 0;
            SettingRPM = 0;
            AlarmCode = 0;
            OldAlarmCode = 1;
        }

        public byte ID { get; set; }
        public byte CurRPM { get; set; }
        public byte SettingRPM { get; set; }
        public byte AlarmCode { get; set; }
        public byte OldAlarmCode { get; set; }

    }
    public class EFUCommand
    {
        public enum Cmd : byte
        {
            STX = 0x02,
            ETX = 0x03,
            REQ_1 = 0x8A,
            REQ_2 = 0x87,
            SETSPEED_1 = 0x89,
            SETSPEED_2 = 0x84,
            SETSPEED_OK = 0xB9
        }
        private byte GetCheckSum(byte[] data, int start, int end)
        {
            int checksum = 0;

            for (int iter = start; iter <= end; ++iter)
                checksum += data[iter];

            checksum &= 0xFF;
            return (byte)checksum;
        }
        public void SetCommand(byte mode1, byte mode2, byte controllerId, byte dpuId, int value = 0)
        {
            this.Mode1 = mode1;
            this.Mode2 = mode2;
            this.ControllerID = controllerId;
            this.DpuID = dpuId;
            this.Value = value;
        }
        public void SetSpeedCmd(byte Lv32_ID, int vSpeed, uint Bl500_StartID, uint Bl500_EndID)
        {
            SetCommand((byte)Cmd.SETSPEED_1, (byte)Cmd.SETSPEED_2, Lv32_ID, 0x9F, vSpeed);

            byte[] TempReq = {
                                 (byte)Cmd.STX, Mode1, Mode2, 
                                 (byte)(Lv32_ID | 0x80), DpuID, 
                                 (byte)(Bl500_StartID+1 | 0x80), (byte)(Bl500_EndID+1 | 0x80),
                                 (byte)(vSpeed / 10), 0,
                                 (byte)Cmd.ETX,
                     };

            TempReq[TempReq.Length - 2] = GetCheckSum(TempReq, 1, TempReq.Length - 3);
            CmdReq = TempReq;
        }
        public void SetValueCmd(byte Lv32_ID, uint Bl500_StartID, uint Bl500_EndID)
        {
            SetCommand((byte)Cmd.REQ_1, (byte)Cmd.REQ_2, Lv32_ID, 0x9F);

            byte[] TempReq = {
                                 (byte)Cmd.STX, Mode1, Mode2, 
                                 (byte)(Lv32_ID | 0x80), DpuID, 
                                 (byte)(Bl500_StartID | 0x80), (byte)(Bl500_EndID | 0x80), 0,
                                 (byte)Cmd.ETX
                     };

            TempReq[TempReq.Length - 2] = GetCheckSum(TempReq, 1, TempReq.Length - 3);
            CmdReq = TempReq;
        }
        public byte[] GetCmd()
        {
            return CmdReq;
        }
        public byte Mode1 { get; set; }
        public byte Mode2 { get; set; }
        public byte ControllerID { get; set; }
        public byte DpuID { get; set; }
        public int Value { get; set; }

        private byte[] CmdReq { get; set; }
    }
    public class EFUController
    {
        public enum Error
        {
            OK, EFUIndexErr, RPMRangeErr, Timeout, SettingErr
        }
        private Dictionary<byte, string> _alarmCode = new Dictionary<byte, string>()
        {
            { 0x80, "정상"      },
            { 0x84, "전원불량"  },
            { 0xA0, "과전류"    },
            { 0xC0, "Motor이상" },            
            { 0x00, "통신이상"  },
            { 0x01, "최초실행"  }
        };
        private int _numFFU = 0;
        public const int MAX_SPEED = 1400;
        private SerialPort _efuSP;
        private List<EFU> _efuList;
        private EFUCommand _curCMD;
        private Timer _efuUpdateTimer;
        private byte _controllerID;
        private int _readTimeout;
        private int _readInterval;
        private bool _waitForResponse;
        private List<byte> _lstRecvBytes = new List<byte>();

        public EFUController(int numFFU, string portName, int readTimeout = 5000)
        {
            _numFFU = numFFU;
            _controllerID = 1;
            _readInterval = 500;
            _waitForResponse = false;
            _efuSP = new SerialPort();
            _efuList = new List<EFU>();
            _efuUpdateTimer = new System.Timers.Timer();
            _curCMD = new EFUCommand();

            _efuSP.PortName = portName;
            _efuSP.ReadTimeout = _readTimeout = readTimeout;
            _efuSP.BaudRate = (int)9600;
            //아래 Default값.
            //_ffuSP.DataBits = (int)8;
            //_ffuSP.Parity = Parity.None;
            //_ffuSP.StopBits = StopBits.One;
            //_ffuSP.WriteTimeout = (int)100;
            _efuSP.DataReceived += new SerialDataReceivedEventHandler(EFUDataReceived);
            Init();
        }
        // speed : 0 ~ 1400
        public Error SetSpeed(uint efuIdxStart, uint efuIdxEnd, int speed)
        {
            if (efuIdxStart >= _efuList.Count ||
                efuIdxEnd >= _efuList.Count)
                return Error.EFUIndexErr;

            if (!(0 < speed && speed <= MAX_SPEED))
                return Error.RPMRangeErr;

            _waitForResponse = true;

            try
            {
                _curCMD.SetSpeedCmd(_controllerID, speed, efuIdxStart, efuIdxEnd);
                byte[] req = _curCMD.GetCmd();

                try
                {
                    _efuSP.Write(req, 0, req.Length);
                }
                catch (System.Exception ex)
                {
                    Logger.Log.AppendLine(LogLevel.Error, string.Format("EFU {0} : 설정 저장 오류", efuIdxStart));
                }

            }
            catch (TimeoutException timeoutE)
            {
                Logger.Log.AppendLine(LogLevel.Error, "EFU : Set Values Timeout");
                return Error.Timeout;
            }

            return Error.OK;
        }
        public string GetAlarmMsg(byte code)
        {
            if (_alarmCode.ContainsKey(code) == false)
            {
                Logger.Log.AppendLine(LogLevel.Error, string.Format("EFU : 정의 되지 않은 알람 메시지 : {0}", code));
                return "Error";
            }
            return _alarmCode[code];
        }
        public void StatusCheck()
        {
            List<EFU> efu = GetEFU();
            string ffuStatusLog = "EFU Status : ";
            foreach (EFU f in efu)
            {
                string oldStatus = GetAlarmMsg(f.OldAlarmCode);
                string newStatus = GetAlarmMsg(f.AlarmCode);

                if (oldStatus != newStatus)
                    ffuStatusLog += string.Format("ID:{0} 상태 변경 됨 ([{1}]->[{2}])/", f.ID, oldStatus, newStatus);

                if (f.OldAlarmCode == _alarmCode.FirstOrDefault(s => s.Value == "최초실행").Key)
                    f.OldAlarmCode = f.AlarmCode;
            }
            if (ffuStatusLog != "EFU Status : ")
                Logger.Log.AppendLine(LogLevel.NoLog, ffuStatusLog);
        }
        public List<EFU> GetEFU()
        {
            return _efuList;
        }
        private bool Init()
        {
            _efuList.Clear();
            for (int iter = 0; iter < _numFFU; ++iter)
                _efuList.Add(new EFU(iter + 1));

            _efuUpdateTimer.Interval = _readInterval;
            _efuUpdateTimer.Elapsed += new ElapsedEventHandler(UpdateEFU);
            return true;
        }
        public bool Open()
        {
            try
            {
                _efuSP.Open();
                Logger.Log.AppendLine(LogLevel.Error, "EFU : Serial Port Open Success");
            }
            catch (System.IO.IOException SerialPortNullEx)
            {
                Logger.Log.AppendLine(LogLevel.Error, "EFU : Serial Port Open Error");

                return false;
            }

            if (!_efuSP.IsOpen)
            {
                return false;
            }
            return true;
        }
        private bool SerialPortClose()
        {
            if (!_efuSP.IsOpen)
                return false;

            _efuSP.Close();
            return true;
        }
        private void UpdateEFU(object sender, ElapsedEventArgs e)
        {
            if (false == _waitForResponse)
                RequestEFUValues();
            else
            {
                _readTimeout -= _readInterval;

                if (0 > _readTimeout)
                {
                    _waitForResponse = false;
                    _readTimeout = _efuSP.ReadTimeout;

                    foreach (EFU f in _efuList)
                        f.AlarmCode = 0x00;

                    if ((byte)EFUCommand.Cmd.SETSPEED_1 == _curCMD.Mode1 && (byte)EFUCommand.Cmd.SETSPEED_2 == _curCMD.Mode2)
                        Logger.Log.AppendLine(LogLevel.Warning, "EFU : Speed Setting Error");
                }
            }
        }
        private Error RequestEFUValues()
        {
            _waitForResponse = true;

            try
            {
                _curCMD.SetValueCmd(_controllerID, 1, _efuList[_efuList.Count - 1].ID);
                byte[] req = _curCMD.GetCmd();
                _efuSP.Write(req, 0, req.Length);
            }
            catch (TimeoutException timeoutE)
            {
                Logger.Log.AppendLine(LogLevel.Exception, "EFU : Time Out");
                return Error.Timeout;
            }

            return Error.OK;
        }
        private void EFUDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            _waitForResponse = false;
            _readTimeout = _efuSP.ReadTimeout;

            //implement
            try
            {
                int num = _efuSP.BytesToRead;
                byte[] recvBytes = new byte[num];
                _efuSP.Read(recvBytes, 0, num);

                if (recvBytes == null || recvBytes.Length == 0) return;

                //전체 명령어 수신
                if (recvBytes[0] == (byte)EFUCommand.Cmd.STX && recvBytes[recvBytes.Length - 1] == (byte)EFUCommand.Cmd.ETX)
                {
                    RecvData(recvBytes);
                }
                //일부 명령어 수신 - 시작
                else if (recvBytes[0] == (byte)EFUCommand.Cmd.STX && recvBytes[recvBytes.Length - 1] != (byte)EFUCommand.Cmd.ETX)
                {
                    _lstRecvBytes.Clear();
                    _lstRecvBytes.AddRange(recvBytes);
                }
                //일부 명령어 수신 - 중간
                else if (recvBytes[0] != (byte)EFUCommand.Cmd.STX && recvBytes[recvBytes.Length - 1] != (byte)EFUCommand.Cmd.ETX)
                {
                    _lstRecvBytes.AddRange(recvBytes);
                }
                //일부 명령어 수신 - 끝
                else if (recvBytes[0] != (byte)EFUCommand.Cmd.STX && recvBytes[recvBytes.Length - 1] == (byte)EFUCommand.Cmd.ETX)
                {
                    _lstRecvBytes.AddRange(recvBytes);
                    RecvData(_lstRecvBytes.ToArray());
                }
            }
            catch (TimeoutException timeoutE)
            {
                Logger.Log.AppendLine(LogLevel.Exception, "EFU : Read Timeout");
            }
        }
        public delegate void EFUrpmChange();
        public event EFUrpmChange ecidEvent;
        private void RecvData(byte[] recvBytes)
        {
            if (((byte)EFUCommand.Cmd.REQ_1 == recvBytes[1]) && ((byte)EFUCommand.Cmd.REQ_2 == recvBytes[2]))
            {
                int ffuIdx = 0;
                int std = 5;

                while (std + 4 < recvBytes.Length)
                {
                    if (ffuIdx >= _numFFU) return;
                    _efuList[ffuIdx].OldAlarmCode = _efuList[ffuIdx].AlarmCode;
                    _efuList[ffuIdx].CurRPM = recvBytes[std + 1];
                    _efuList[ffuIdx].AlarmCode = recvBytes[std + 2];
                    _efuList[ffuIdx].SettingRPM = recvBytes[std + 3];
                    std += 4;
                    ffuIdx++;
                }
            }
            else if (((byte)EFUCommand.Cmd.SETSPEED_1 == recvBytes[1]) && ((byte)EFUCommand.Cmd.SETSPEED_2 == recvBytes[2]))
            {
                if ((byte)EFUCommand.Cmd.SETSPEED_OK == recvBytes[7])
                {
                    for (byte iter = recvBytes[5]; iter <= recvBytes[6]; ++iter)
                    {
                        _efuList[iter - 1 ^ 0x80].SettingRPM = (byte)(_curCMD.Value / 10);
                    }
                    ecidEvent?.Invoke();
                }
                else
                {
                    Logger.Log.AppendLine(LogLevel.Warning, "EFU : Speed Setting Error");
                }
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
        public bool Start()
        {
            Open();

            if (!_efuSP.IsOpen)
                return true;

            try
            {
                _efuUpdateTimer.Start();
                return true;
            }
            catch (NullReferenceException efuUpdateTimerNuller_E)
            {
                Logger.Log.AppendLine(LogLevel.Exception, "EFU : Update Timer Null Ex");
                return false;
            }
        }
        public void Stop()
        {
            if (_efuUpdateTimer == null) return;

            _efuUpdateTimer.Stop();
            SerialPortClose();
        }
        public string CurrentPort
        {
            get
            {
                return _efuSP.PortName;
            }
            set
            {
                _efuSP.PortName = value;
            }
        }
        public bool IsOpen()
        {
            return _efuSP.IsOpen;
        }
    }
}