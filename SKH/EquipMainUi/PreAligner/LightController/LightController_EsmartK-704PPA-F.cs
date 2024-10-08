using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.PreAligner
{
    public enum EmCommand
    {
        SetBright,
        GetBright,
        On,
        Off,
        GetOnOff,
        SetRemoteMode,
        GetRemoteMode,
        GetErrStatus
    }
    public class CommandSet
    {
        public bool IsReturned;
        public bool IsSuccess;
        public string Cmd;
        public Func<string[], bool> Parsing;

        public void CmdClear()
        {
            IsReturned = IsSuccess = false;
        }
    }
    public class LightStatus
    {
        public int Bright;
        public bool IsOn;
    }
    public class LightController_EsmartK_704PPA_F : SerialCommBase
    {
        private Dictionary<EmCommand, CommandSet> _cmds;

        public LightStatus Ch1 = new LightStatus();
        public LightStatus Ch2 = new LightStatus();
        public bool IsRemote { get; private set; }
        private int _errorCode;
        public string ErrorMsg { get { return GetErrorMsg(_errorCode); } }

        private string GetErrorMsg(int errorCode)
        {
            switch (errorCode)
            {
                case 0xFF: return "0xFF : 제어기 시스템 이상 제어기 기술문의 바랍니다";
                case 0x01: return "0x01 : 과전압 보호 상태 조명 연결단자의 커넥터 접촉 상태 불량 오픈";
                case 0x02: return "0x02 : 과온도 보호 상태 조명의 온도 방열 확인 과열";
                case 0x04: return "0x04 : 제어기간 링크불량 제어기간의 연결 케이블 확인";
                case 0x10: return "0x10 : 채널 0~3 번 과전류 상태 조명 연결단자의 커넥터 접촉 상태 불량 쇼트";
                case 0x20: return "0x20 : 채널 4~7 번 과전류 상태";
                case 0x40: return "0x40 : 채널 8~11 번 과전류 상태";
                case 0x80: return "0x80 : 채널 12~15 번 과전류 상태";
            }
            return string.Empty;
        }

        public LightController_EsmartK_704PPA_F(SerialPort port) : base(port)
        {
            InitCmd();
        }
        public LightController_EsmartK_704PPA_F(string portName, int baudRate = 9600, int dataBits = 8, 
            StopBits stopBits = StopBits.One, Parity parity = Parity.None, int readTimeout = 5000) 
            : base(portName, 38400, dataBits, stopBits, parity, readTimeout)
        {
            InitCmd();
        }

        private void InitCmd()
        {
            _cmds = new Dictionary<EmCommand, CommandSet>();
            _cmds[EmCommand.SetBright]     = new CommandSet() { Cmd = "#"           , Parsing = null };
            _cmds[EmCommand.GetBright]     = new CommandSet() { Cmd = "$$"          , Parsing = OnGetBright };
            _cmds[EmCommand.On]            = new CommandSet() { Cmd = "setonoffex"  , Parsing = null };
            _cmds[EmCommand.Off]           = new CommandSet() { Cmd = "setonoffex"  , Parsing = null };
            _cmds[EmCommand.GetOnOff]      = new CommandSet() { Cmd = "getonoffex"  , Parsing = OnGetOnOff };
            _cmds[EmCommand.SetRemoteMode] = new CommandSet() { Cmd = "setmode 1"   , Parsing = null };
            _cmds[EmCommand.GetRemoteMode] = new CommandSet() { Cmd = "getmode"     , Parsing = OnGetRemoteMode };
            _cmds[EmCommand.GetErrStatus]  = new CommandSet() { Cmd = "geterrstatus", Parsing = OnGetErrStatus };
        }

        public bool IsCmdReturned(EmCommand cmd)
        {
            return _cmds[cmd].IsReturned;
        }

        public bool IsCmdSuccess(EmCommand cmd)
        {
            return _cmds[cmd].IsSuccess;
        }

        private bool OnGetErrStatus(string[] split)
        {
            if (split.Length != 3) return false;
            int isError, errorCode;
            if (int.TryParse(split[1], out isError) == false) return false;
            if (int.TryParse(split[2], out errorCode) == false) return false;

            LightStatus status;
            if (isError == 0)
                status = Ch1;
            else if (isError == 1)
                status = Ch2;
            else
                return false;

            if (isError == 1)
                _errorCode = errorCode;
            else
                _errorCode = 0;

            return true;
        }

        private bool OnGetRemoteMode(string[] split)
        {
            if (split.Length != 2) return false;
            int isRemote;
            if (int.TryParse(split[1], out isRemote) == false) return false;            

            LightStatus status;
            if (isRemote == 0)
                status = Ch1;
            else if (isRemote == 1)
                status = Ch2;
            else
                return false;

            IsRemote = isRemote == 1 ? true : false;

            return true;
        }

        private bool OnGetOnOff(string[] split)
        {
            if (split.Length != 3) return false;
            int ch, onOff;
            try
            {
                ch = Convert.ToInt32(split[1], 16);
            }
            catch
            {
                return false;
            }
            if (int.TryParse(split[2], out onOff) == false) return false;

            if (onOff == 0)
                Ch1.IsOn = Ch2.IsOn = false;
            else if (onOff == 3)
                Ch1.IsOn = Ch2.IsOn = true;
            else
            {
                Ch1.IsOn = onOff == 1 ? true : false;
                Ch2.IsOn = onOff == 2 ? true : false;
            }

            return true;
        }

        private bool OnGetBright(string[] split)
        {
            if (split.Length != 3) return false;
            int ch, bright;
            if (int.TryParse(split[1], out ch) == false) return false;
            if (int.TryParse(split[2], out bright) == false) return false;

            LightStatus status;
            if (ch == 0)
                status = Ch1;
            else if (ch == 1)
                status = Ch2;
            else
                return false;

            status.Bright = bright;
            
            return true;
        }

        protected override void ParsingReceivedData(string message)
        {
            var split = message.Split(' ');
            if (split.Length < 1) return;
            var getCmd = _cmds.FirstOrDefault(c => c.Value.Cmd == split[0]);
            if (getCmd.Equals(default(KeyValuePair<EmCommand, CommandSet>)) == true) return;
            
            switch(getCmd.Key)
            {
                case EmCommand.GetBright:
                    getCmd.Value.IsSuccess = OnGetBright(split);
                    break;                
                case EmCommand.GetOnOff:
                    getCmd.Value.IsSuccess = OnGetOnOff(split);
                    break;
                case EmCommand.GetRemoteMode:
                    getCmd.Value.IsSuccess = OnGetRemoteMode(split);
                    break;
                case EmCommand.GetErrStatus:
                    getCmd.Value.IsSuccess = OnGetErrStatus(split);
                    break;
            }
            getCmd.Value.IsReturned = true;
        }

        private int _curRecvCount;
        private const int _maxRecvCount = 3;
        private string recvData;
        protected override void ReceivedData(string msg)
        {
            var split = msg.Split('\n');
            if (split.Length > 1)
            {
                recvData += split[0];
                ParsingReceivedData(recvData);
                _curRecvCount = 0;
                recvData = string.Empty;
            }
            else if (_curRecvCount >= _maxRecvCount)
            {
                _curRecvCount = 0;
                recvData = string.Empty;
            }
            else
            {
                recvData += split[0];
                _curRecvCount++;
            }
        }

        private void WriteCmd(string v)
        {
            var b = Encoding.ASCII.GetBytes(v);
            _serialPort.Write(b, 0, b.Length);
        }

        public int SetBright(int ch, int bright)
        {
            if (ch < 0) ch = 0;
            if (ch > 1) ch = 1;
            if (bright < 0) bright = 0;
            if (bright > 1024) bright = 1024;

            string cmd = _cmds[EmCommand.SetBright].Cmd;
            _cmds[EmCommand.SetBright].CmdClear();
            WriteCmd(string.Format("{0} {1} {2}\n", cmd, ch, bright));
            return bright;
            
        }
        
        public void GetBright(int ch)
        {
            if (ch < 0) ch = 0;
            if (ch > 1) ch = 1;

            string cmd = _cmds[EmCommand.GetBright].Cmd;
            _cmds[EmCommand.GetBright].CmdClear();
            WriteCmd(string.Format("{0} {1}\n", cmd, ch));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ch">3 = 0,1 on</param>
        public void On(int ch = 3)
        {
            if (ch < 0) ch = 0;
            if (ch == 2) ch = 1;
            if (ch > 3) ch = 3;

            string cmd = _cmds[EmCommand.On].Cmd;
            _cmds[EmCommand.On].CmdClear();
            WriteCmd(string.Format("{0} {1}\n", cmd, ch));
        }
        
        public void Off()
        {
            string cmd = _cmds[EmCommand.Off].Cmd;
            _cmds[EmCommand.Off].CmdClear();
            WriteCmd(string.Format("{0} {1}\n", cmd, 0));
        }

        public void GetOnOff()
        {
            string cmd = _cmds[EmCommand.GetOnOff].Cmd;
            _cmds[EmCommand.GetOnOff].CmdClear();
            WriteCmd(string.Format("{0}\n", cmd));
        }

        public void SetRemoteMode()
        {
            string cmd = _cmds[EmCommand.SetRemoteMode].Cmd;
            _cmds[EmCommand.SetRemoteMode].CmdClear();
            WriteCmd(string.Format("{0}\n", cmd));
        }

        public void GetRemoteMode()
        {
            string cmd = _cmds[EmCommand.GetRemoteMode].Cmd;
            _cmds[EmCommand.GetRemoteMode].CmdClear();
            WriteCmd(string.Format("{0}\n", cmd));
        }

        public void GetErrStatus()
        {
            string cmd = _cmds[EmCommand.GetErrStatus].Cmd;
            _cmds[EmCommand.GetErrStatus].CmdClear();
            WriteCmd(string.Format("{0}\n", cmd));
        }
    }
}
