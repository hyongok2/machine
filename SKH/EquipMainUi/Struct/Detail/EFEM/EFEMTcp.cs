using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EquipMainUi.Struct.Detail.EFEM
{
    public class EFEMHSPort
    {
        public Dictionary<EmEfemCommand, EFEMHandShake> Cmds = new Dictionary<EmEfemCommand, EFEMHandShake>();

        public EFEMHandShake this[EmEfemCommand cmd]
        {
            get
            {
                return Cmds[cmd];
            }
            set
            {
                Cmds[cmd] = value;
            }
        }
    }
    public class EFEMHandShake
    {
        public string Name { get; set; }
        public bool IsCmdOn { get; set; }
        public bool IsAckRecved { get; set; }
        public bool IsCompleteRecved { get; set; }
        public bool IsSuccess { get; set; }
        public string ReturnData { get; set; }
        public bool IsDataProcessDone { get; set; }
        
        public EmEfemErrorModule ErrorModule { get; set; }
        public int ErrorCode { get; set; }

        public EFEMHandShake()
        {
            IsAckRecved = true;
            IsCompleteRecved = true;
            IsSuccess = true;
        }

        public string GetErrorDesc()
        {
            return EFEMError.GetErrorDesc(ErrorModule, ErrorCode);
        }
        //public void AlarmHappen(EmEfemPort port, EmEfemCommand cmd)
        //{
        //    switch (port)
        //    {
        //        case EmEfemPort.ROBOT:
        //            AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0640_ROBOT_MOVING_COMMAND_FAIL);
        //            break;
        //        case EmEfemPort.LOADPORT1:
        //            AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0641_LOADPORT1_MOVING_COMMAND_FAIL);
        //            break;
        //        case EmEfemPort.LOADPORT2:
        //            AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0642_LOADPORT2_MOVING_COMMAND_FAIL);
        //            break;
        //    }
        //}

        public void CmdOn()
        {
            IsCmdOn = true;
            IsAckRecved = false;
            IsCompleteRecved = false;
            IsSuccess = false;
            IsDataProcessDone = false;
            ReturnData = string.Empty;
            ErrorModule = EmEfemErrorModule.E0;
            ErrorCode = -1;            
        }

        public void ClearComplete()
        {
            this.IsCompleteRecved = false;
        }
    }

    public class AsyncObject
    {
        public Byte[] Buffer;
        public Socket WorkingSocket;
        public AsyncObject(Int32 bufferSize)
        {
            this.Buffer = new Byte[bufferSize];
        }
    }

    public static class EFEMTcp
    {
        public static Equipment equip;

        public static Dictionary<EmEfemPort, EFEMHSPort> HS = new Dictionary<EmEfemPort, EFEMHSPort>();

        public static EFEMStatus EfemStat = new EFEMStatus();
        
        public static EFEMRobotStatus RobotStat = new EFEMRobotStatus();
        public static EFEMLoadPortStatus LoadPortStat1 = new EFEMLoadPortStatus();
        public static EFEMLoadPortStatus LoadPortStat2 = new EFEMLoadPortStatus();
        public static EFEMAlignerStatus AlignerStat = new EFEMAlignerStatus();

        //TCP        
        private static Socket m_ClientSocket = null;
        private static AsyncCallback m_fnReceiveHandler;
        private static AsyncCallback m_fnSendHandler;

        private static bool _isConnected;
        public static bool IsConnected { get { return _isConnected; } }
        private static List<byte> _lstRecvBytes = new List<byte>();
        //EFEM 통신 확인을 위해 임시 추가
        public static event AppendDataEvent appendDataEvent = null;
        

        static EFEMTcp()
        {
            InitializeHandShake();
        }

        #region TCP Conn
        public static bool Connect(string efemIp, int efemPort)
        {
            // 비동기 작업에 사용될 대리자를 초기화합니다.
            m_fnReceiveHandler = new AsyncCallback(handleDataReceive);
            m_fnSendHandler = new AsyncCallback(handleDataSend);
            
            // TCP 통신을 위한 소켓을 생성합니다.
            m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            Boolean isConnected = false;
            try
            {
                var remoteEP = new IPEndPoint(IPAddress.Parse(efemIp), efemPort);
                var result = m_ClientSocket.BeginConnect(remoteEP, null, null);

                bool success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(10), false);


                //// 연결 시도
                //m_ClientSocket.Connect(efemIp, efemPort);

                //m_ClientSocket.EndConnect(result);

                // 연결 성공
                isConnected = _isConnected = true;
            }
            catch (Exception ex)
            {
                // 연결 실패 (연결 도중 오류가 발생함)
                isConnected = false;
            }            
            if (isConnected)
            {
                // 4096 바이트의 크기를 갖는 바이트 배열을 가진 AsyncObject 클래스 생성
                AsyncObject ao = new AsyncObject(4096);
                // 작업 중인 소켓을 저장하기 위해 sockClient 할당
                ao.WorkingSocket = m_ClientSocket;

                // 비동기적으로 들어오는 자료를 수신하기 위해 BeginReceive 메서드 사용!
                m_ClientSocket.BeginReceive(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, m_fnReceiveHandler, ao);
                
                _isConnected = isConnected;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void StopClient()
        {
            // 가차없이 클라이언트 소켓을 닫습니다.
            _isConnected = false;
            m_ClientSocket.Close();
        }

        private static void handleDataSend(IAsyncResult ar)
        {
            AsyncObject ao = (AsyncObject)ar.AsyncState;
            
            int sentBytes;

            try
            {
                sentBytes = ao.WorkingSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                _isConnected = false;
                Console.WriteLine("자료 송신 도중 오류 발생! 메세지: {0}", ex.Message);
                return;
            }

            if (sentBytes > 0)
            {
                byte[] msgByte = new byte[sentBytes];
                Array.Copy(ao.Buffer, msgByte, sentBytes);

                if (msgByte.Length > 5
                    && msgByte[1] == 'S'
                    && msgByte[2] == 'T'
                    && msgByte[3] == 'A'
                    && msgByte[4] == 'T'
                    )
                {
                }
                else
                    appendDataEvent(EmEfemPort.NONE, string.Format("EQ->EFEM[송신]:{0}", Encoding.UTF8.GetString(msgByte)));                
            }
        }
        public static void SendMessage(string message)
        {
            AsyncObject ao = new AsyncObject(1);
            
            ao.Buffer = Encoding.UTF8.GetBytes(message);

            ao.WorkingSocket = m_ClientSocket;
            
            try
            {
                m_ClientSocket.BeginSend(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, m_fnSendHandler, ao);
            }
            catch (Exception ex)
            {
                _isConnected = false;
                Console.WriteLine("전송 중 오류 발생!\n메세지: {0}", ex.Message);
            }
        }

        private static void handleDataReceive(IAsyncResult ar)
        {
            AsyncObject ao = (AsyncObject)ar.AsyncState;
            
            int recvBytes;

            try
            {
                recvBytes = ao.WorkingSocket.EndReceive(ar);
            }
            catch
            {
                return;
            }
            
            if (recvBytes > 0)
            {
                byte[] msgByte = new byte[recvBytes];
                Array.Copy(ao.Buffer, msgByte, recvBytes);

                //Console.WriteLine("메세지 받음: {0}", Encoding.Unicode.GetString(msgByte));
                DataReceived(msgByte);
            }

            try
            {
                ao.WorkingSocket.BeginReceive(ao.Buffer, 0, ao.Buffer.Length, SocketFlags.None, m_fnReceiveHandler, ao);
            }
            catch (Exception ex)
            {
                _isConnected = false;
                Console.WriteLine("자료 수신 대기 도중 오류 발생! 메세지: {0}", ex.Message);
                return;
            }
        }
        private static void RecvData(string recvData)
        {
            EFEMData data = new EFEMData(recvData);
                        

            if (recvData[0].Equals(((char)EmEfEmByte.BOCofAck)))
            {
                EmEfemPort port = StringToEmEfemPort(data.Port);
                EmEfemCommand emcommand = StringToEmEfemCommand(data.Command);

                HS[port][emcommand].IsAckRecved = true;

                if (appendDataEvent != null && emcommand != EmEfemCommand.STAT_)
                    appendDataEvent(StringToEmEfemPort(data.Port), string.Format("EQ<-EFEM[수신]:{0}", recvData));
            }
            else if (recvData[0].Equals((char)EmEfEmByte.BOCofComplete))
            {
                bool dataEvent = true;
                switch (data.Command)
                {
                    case "STAT~":   OnCompleteState(data); dataEvent = false; break;
                    case "MAPP~":   OnCompleteMapping(data); break;
                    case "ALIGN":   OnCompleteAlign(data); break;
                    default:        OnCompleteSometing(data); break;
                }

                if (appendDataEvent != null && dataEvent == true)
                    appendDataEvent(StringToEmEfemPort(data.Port), string.Format("EQ<-EFEM[수신]:{0}", recvData));
            }
            else if (recvData[0].Equals((char)EmEfEmByte.BOCofLPMSG))
            {
                string lpmsg = data.Data.Substring(1, data.Data.Length - 2);
                if (data.Port == "1")
                {
                    LoadPortStat1.SetLPMSG(lpmsg, EmEfemPort.LOADPORT1);
                    HS[EmEfemPort.LOADPORT1][EmEfemCommand.LPMSG].IsCompleteRecved = true;
                }
                else if (data.Port == "2")
                {
                    LoadPortStat2.SetLPMSG(lpmsg, EmEfemPort.LOADPORT2);
                    HS[EmEfemPort.LOADPORT2][EmEfemCommand.LPMSG].IsCompleteRecved = true;
                }

                if (appendDataEvent != null)
                    appendDataEvent(StringToEmEfemPort(data.Port), string.Format("EQ<-EFEM[수신]:{0}", recvData));
            }
        }
        private static void MsgSpliter(string msg)
        {
            if (msg.Count(c => c == (char)EmEfEmByte.EOC) > 1)
            {
                var spl = msg.Split(new char[] { (char)EmEfEmByte.EOC });
                foreach (string s in spl)
                {
                    if (s.Length == 0)
                        continue;
                    RecvData(s+'\r');
                }
            }
            else
                RecvData(msg);
        }
        private static void DataReceived(byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0) return;

            //전체 packet 수신
            if ((buffer[0] == (byte)EmEfEmByte.BOCofAck || buffer[0] == (byte)EmEfEmByte.BOCofComplete || buffer[0] == (byte)EmEfEmByte.BOCofLPMSG)
                && buffer[buffer.Length - 1] == (byte)EmEfEmByte.EOC)
            {
                MsgSpliter(Encoding.UTF8.GetString(buffer));
            }
            //일부 packet 수신 - 시작
            else if ((buffer[0] == (byte)EmEfEmByte.BOCofAck || buffer[0] == (byte)EmEfEmByte.BOCofComplete || buffer[0] == (byte)EmEfEmByte.BOCofLPMSG)
                && buffer[buffer.Length - 1] != (byte)EmEfEmByte.EOC)
            {
                _lstRecvBytes.Clear();
                _lstRecvBytes.AddRange(buffer);
            }
            //일부 packet 수신 - 중간
            else if ((buffer[0] != (byte)EmEfEmByte.BOCofAck && buffer[0] != (byte)EmEfEmByte.BOCofComplete && buffer[0] != (byte)EmEfEmByte.BOCofLPMSG )
                && buffer[buffer.Length - 1] == (byte)EmEfEmByte.EOC)
            {
                _lstRecvBytes.AddRange(buffer);
            }
            //일부 packet 수신 - 끝
            else if ((buffer[0] != (byte)EmEfEmByte.BOCofAck && buffer[0] != (byte)EmEfEmByte.BOCofComplete && buffer[0] != (byte)EmEfEmByte.BOCofLPMSG)
                && buffer[buffer.Length - 1] == (byte)EmEfEmByte.EOC)
            {
                _lstRecvBytes.AddRange(buffer);
                MsgSpliter(Encoding.UTF8.GetString(_lstRecvBytes.ToArray()));
            }
        }

        #endregion TCP Conn

        private static void InitializeHandShake()
        {
            HS[EmEfemPort.ROBOT] = new EFEMHSPort();
            HS[EmEfemPort.LOADPORT1] = new EFEMHSPort();
            HS[EmEfemPort.LOADPORT2] = new EFEMHSPort();
            HS[EmEfemPort.ETC] = new EFEMHSPort();
            HS[EmEfemPort.ALIGNER] = new EFEMHSPort();

            HS[EmEfemPort.ROBOT][EmEfemCommand.INIT_] = new EFEMHandShake() { Name = "Initialization Robot" };
            HS[EmEfemPort.LOADPORT1][EmEfemCommand.INIT_] = new EFEMHandShake() { Name = "Init Command To Load Port1" };
            HS[EmEfemPort.LOADPORT2][EmEfemCommand.INIT_] = new EFEMHandShake() { Name = "Init Command To Load Port2" };
            HS[EmEfemPort.ALIGNER][EmEfemCommand.INIT_] = new EFEMHandShake() { Name = "Init Command To Alinger" };

            HS[EmEfemPort.ROBOT][EmEfemCommand.RESET] = new EFEMHandShake() { Name = "Reset Robot" };
            HS[EmEfemPort.LOADPORT1][EmEfemCommand.RESET] = new EFEMHandShake() { Name = "Reset Load Port1" };
            HS[EmEfemPort.LOADPORT2][EmEfemCommand.RESET] = new EFEMHandShake() { Name = "Reset Load Port2" };
            HS[EmEfemPort.ALIGNER][EmEfemCommand.RESET] = new EFEMHandShake() { Name = "Reset Aligner" };
            HS[EmEfemPort.ETC][EmEfemCommand.RESET] = new EFEMHandShake() { Name = "Reset Safty PLC" };

            HS[EmEfemPort.ROBOT][EmEfemCommand.TRANS] = new EFEMHandShake() { Name = "Command Transfer To Robot" };

            HS[EmEfemPort.ROBOT][EmEfemCommand.WAITR] = new EFEMHandShake() { Name = "Move Wating Postion to Robot" };

            HS[EmEfemPort.ROBOT][EmEfemCommand.STAT_] = new EFEMHandShake() { Name = "Check Robot State" };
            HS[EmEfemPort.LOADPORT1][EmEfemCommand.STAT_] = new EFEMHandShake() { Name = "Check Load Port1 State" };
            HS[EmEfemPort.LOADPORT2][EmEfemCommand.STAT_] = new EFEMHandShake() { Name = "Check Load Port2 State" };
            HS[EmEfemPort.ALIGNER][EmEfemCommand.STAT_] = new EFEMHandShake() { Name = "Check Aligner State" };
            HS[EmEfemPort.ETC][EmEfemCommand.STAT_] = new EFEMHandShake() { Name = "Check EFEM State" };

            HS[EmEfemPort.ROBOT][EmEfemCommand.PAUSE] = new EFEMHandShake() { Name = "Pause Robot" };
            HS[EmEfemPort.ROBOT][EmEfemCommand.RESUM] = new EFEMHandShake() { Name = "Resume Robot" };

            //HS[EmEfemPort.ROBOT][EmEfemCommand.EXTND] = new EFEMHandShake() { Name = "Extend Robot" };
            //HS[EmEfemPort.ROBOT][EmEfemCommand.ARMFD] = new EFEMHandShake() { Name = "Arm Fold Robot" };

            HS[EmEfemPort.ROBOT][EmEfemCommand.STOP_] = new EFEMHandShake() { Name = "Stop Moving Robot" };            

            HS[EmEfemPort.LOADPORT1][EmEfemCommand.STOP_] = new EFEMHandShake() { Name = "Stop Moving Load Port1" };
            HS[EmEfemPort.LOADPORT2][EmEfemCommand.STOP_] = new EFEMHandShake() { Name = "Stop Moving Load Port2" };

            HS[EmEfemPort.ALIGNER][EmEfemCommand.PARDY] = new EFEMHandShake() { Name = "Send Align Receive Ready Signal" };
            HS[EmEfemPort.ALIGNER][EmEfemCommand.PATRR] = new EFEMHandShake() { Name = "Send Align Rotation Signal" };
            HS[EmEfemPort.ALIGNER][EmEfemCommand.PASRD] = new EFEMHandShake() { Name = "Send Align Send Ready Signal" };
            HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN] = new EFEMHandShake() { Name = "Do Alignment" };
            
            //HS[EmEfemPort.LOADPORT1][EmEfemCommand.CLAMP] = new EFEMHandShake() { Name = "Clamp FOUP Of Load Port1" };
            //HS[EmEfemPort.LOADPORT2][EmEfemCommand.CLAMP] = new EFEMHandShake() { Name = "Clamp FOUP Of Load Port2" };

            //HS[EmEfemPort.LOADPORT1][EmEfemCommand.UCLAM] = new EFEMHandShake() { Name = "UnClamp FOUP Of Load Port1" };
            //HS[EmEfemPort.LOADPORT2][EmEfemCommand.UCLAM] = new EFEMHandShake() { Name = "UnClamp FOUP Of Load Port2" };

            //HS[EmEfemPort.LOADPORT1][EmEfemCommand.DOCK_] = new EFEMHandShake() { Name = "Docking FOUP Of Load Port1" };
            //HS[EmEfemPort.LOADPORT2][EmEfemCommand.DOCK_] = new EFEMHandShake() { Name = "Docking FOUP Of Load Port2" };

            //HS[EmEfemPort.LOADPORT1][EmEfemCommand.UDOCK] = new EFEMHandShake() { Name = "UnDocking FOUP Of Load Port1" };
            //HS[EmEfemPort.LOADPORT2][EmEfemCommand.UDOCK] = new EFEMHandShake() { Name = "UnDocking FOUP Of Load Port2" };

            HS[EmEfemPort.LOADPORT1][EmEfemCommand.OPEN_] = new EFEMHandShake() { Name = "Open FOUP Of Load Port1 And Mapping" };
            HS[EmEfemPort.LOADPORT2][EmEfemCommand.OPEN_] = new EFEMHandShake() { Name = "Open FOUP Of Load Port2 And Mapping" };

            HS[EmEfemPort.LOADPORT1][EmEfemCommand.CLOSE] = new EFEMHandShake() { Name = "Close FOUP Of Load Port1" };
            HS[EmEfemPort.LOADPORT2][EmEfemCommand.CLOSE] = new EFEMHandShake() { Name = "Close FOUP Of Load Port2" };

            //HS[EmEfemPort.LOADPORT1][EmEfemCommand.LOAD_] = new EFEMHandShake() { Name = "Load FOUP Of Load Port1" };
            //HS[EmEfemPort.LOADPORT2][EmEfemCommand.LOAD_] = new EFEMHandShake() { Name = "Load FOUP Of Load Port2" };

            //HS[EmEfemPort.LOADPORT1][EmEfemCommand.ULOAD] = new EFEMHandShake() { Name = "UnLoad FOUP Of Load Port1" };
            //HS[EmEfemPort.LOADPORT2][EmEfemCommand.ULOAD] = new EFEMHandShake() { Name = "UnLoad FOUP Of Load Port2" };            

            HS[EmEfemPort.LOADPORT1][EmEfemCommand.MAPP_] = new EFEMHandShake() { Name = "Ask Mapping Data Of Load Port1" };
            HS[EmEfemPort.LOADPORT2][EmEfemCommand.MAPP_] = new EFEMHandShake() { Name = "Ask Mapping Data Of Load Port2" };

            HS[EmEfemPort.LOADPORT1][EmEfemCommand.LPLED] = new EFEMHandShake() { Name = "Control Led Lamp Of Load Port1" };
            HS[EmEfemPort.LOADPORT2][EmEfemCommand.LPLED] = new EFEMHandShake() { Name = "Control Led Lamp Of Load Port2" };

            HS[EmEfemPort.LOADPORT1][EmEfemCommand.LPMSG] = new EFEMHandShake() { Name = "LOADPORT1 LPMSG Event" };
            HS[EmEfemPort.LOADPORT2][EmEfemCommand.LPMSG] = new EFEMHandShake() { Name = "LOADPORT2 LPMSG Event" };

            HS[EmEfemPort.ETC][EmEfemCommand.CHMDA] = new EFEMHandShake() { Name = "Change Auto Mode Of EFEM" };

            HS[EmEfemPort.ETC][EmEfemCommand.CHMDM] = new EFEMHandShake() { Name = "Change Manual Mode Of EFEM" };

            HS[EmEfemPort.ETC][EmEfemCommand.SIGLM] = new EFEMHandShake() { Name = "Setting Lamp & Buzzer Of Signal Tower" };
        }
        
        private static EmEfemCommand StringToEmEfemCommand(string command)
        {
            EmEfemCommand emCommand = EmEfemCommand.NONE_;

            switch (command)
            {
                case "INIT~": emCommand = EmEfemCommand.INIT_; break;
                case "RESET": emCommand = EmEfemCommand.RESET; break;
                case "TRANS": emCommand = EmEfemCommand.TRANS; break;
                case "WAITR": emCommand = EmEfemCommand.WAITR; break;
                case "STAT~": emCommand = EmEfemCommand.STAT_; break;
                case "PAUSE": emCommand = EmEfemCommand.PAUSE; break;
                case "RESUM": emCommand = EmEfemCommand.RESUM; break;
                case "STOP~": emCommand = EmEfemCommand.STOP_; break;
                case "PARDY": emCommand = EmEfemCommand.PARDY; break;
                case "PATRR": emCommand = EmEfemCommand.PATRR; break;
                case "PASRD": emCommand = EmEfemCommand.PASRD; break;
                case "ALIGN": emCommand = EmEfemCommand.ALIGN; break;
                //case "CLAMP": emCommand = EmEfemCommand.CLAMP; break;
                //case "UCLAM": emCommand = EmEfemCommand.UCLAM; break;
                //case "DOCK~": emCommand = EmEfemCommand.DOCK_; break;
                //case "UDOCK": emCommand = EmEfemCommand.UDOCK; break;
                case "OPEN~": emCommand = EmEfemCommand.OPEN_; break;
                case "CLOSE": emCommand = EmEfemCommand.CLOSE; break;
                //case "LOAD~": emCommand = EmEfemCommand.LOAD_; break;
                //case "ULOAD": emCommand = EmEfemCommand.ULOAD; break;
                case "MAPP~": emCommand = EmEfemCommand.MAPP_; break;
                case "LPLED": emCommand = EmEfemCommand.LPLED; break;
                case "CHMDA": emCommand = EmEfemCommand.CHMDA; break;
                case "CHMDM": emCommand = EmEfemCommand.CHMDM; break;
                case "SIGLM": emCommand = EmEfemCommand.SIGLM; break;                
            }
            return emCommand;
        }
        private static EmEfemPort StringToEmEfemPort(string port)
        {
            EmEfemPort emPort = EmEfemPort.NONE;

            emPort =    port.Equals("0") ? EmEfemPort.ROBOT :
                        port.Equals("1") ? EmEfemPort.LOADPORT1 :
                        port.Equals("2") ? EmEfemPort.LOADPORT2 :
                        port.Equals("9") ? EmEfemPort.ALIGNER :
                        port.Equals("F") ? EmEfemPort.ETC : EmEfemPort.NONE;

            return emPort;
        }

        #region Complete

        private static void OnCompleteSometing(EFEMData recvPacket)
        {
            EmEfemPort port = StringToEmEfemPort(recvPacket.Port);
            EmEfemCommand command = StringToEmEfemCommand(recvPacket.Command);

            if (command == EmEfemCommand.NONE_)
            {
                InterLockMgr.AddInterLock("EXCEPTION", GG.boChinaLanguage ? "Error Complete 信号发生, PORT:{0},RECV MSG:{1}" : "이상 Complete 신호 발생, PORT:{0},RECV MSG:{1}", port.ToString(), recvPacket.ToString());
                return;
            }
            else
                ParsingPacket(port, command, recvPacket.Data);
            HS[port][command].IsDataProcessDone = true;
        }

        private static void OnCompleteState(EFEMData recvPacket)
        {
            EmEfemPort port = StringToEmEfemPort(recvPacket.Port);
            EmEfemCommand command = EmEfemCommand.STAT_;
            if (ParsingPacket(port, command, recvPacket.Data) == false)
                return;

            string statusData = string.Empty;

            statusData = HS[port][command].ReturnData;

            if (port == EmEfemPort.ETC)
                EfemStat.Set(statusData);
            else if (port == EmEfemPort.ROBOT)
            {
                RobotStat.Set(statusData);
            }
            else if (port == EmEfemPort.ALIGNER)
            {
                AlignerStat.Set(statusData);
            }
            else if (port == EmEfemPort.LOADPORT1)
            {
                LoadPortStat1.Set(statusData);               
            }
            else if (port == EmEfemPort.LOADPORT2)
            {
                LoadPortStat2.Set(statusData);
            }

            HS[port][command].IsDataProcessDone = true;
        }
        
        private static void OnCompleteMapping(EFEMData recvPacket)
        {
            EmEfemPort port = StringToEmEfemPort(recvPacket.Port);
            EmEfemCommand command = EmEfemCommand.MAPP_;

            ParsingPacket(port, command, recvPacket.Data);

            string mappingData = string.Empty;

            mappingData = HS[port][command].ReturnData;

            if (port == EmEfemPort.LOADPORT1)
            {
                LoadPortStat1.MappingData = new EmEfemMappingInfo[mappingData.Length];
                for (int i = 0; i < mappingData.Length; i++)
                {
                    LoadPortStat1.MappingData[i] = (EmEfemMappingInfo)int.Parse(mappingData[i].ToString());
                }
            }
            else if (port == EmEfemPort.LOADPORT2)
            {
                LoadPortStat2.MappingData = new EmEfemMappingInfo[mappingData.Length];
                for (int i = 0; i < mappingData.Length; i++)
                {
                    LoadPortStat2.MappingData[i] = (EmEfemMappingInfo)int.Parse(mappingData[i].ToString());
                }
            }

            HS[port][command].IsDataProcessDone = true;
        }

        private static void OnCompleteAlign(EFEMData recvPacket)
        {
            EmEfemPort port = StringToEmEfemPort(recvPacket.Port);
            EmEfemCommand command = EmEfemCommand.ALIGN;

            ParsingPacket(port, command, recvPacket.Data);

            string alignData = string.Empty;

            alignData = HS[port][command].ReturnData;

            if (alignData == string.Empty && HS[port][command].ErrorCode != -1)
            {

            }
            else if (AlignerStat.Parsing(alignData) == false)
            {
                AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0920_ALIGNER_DATA_READ_FAIL);
            }
            HS[port][command].IsDataProcessDone = true;
        }

        private static bool ParsingPacket(EmEfemPort port, EmEfemCommand command, string data)
        {
            if (data[0].Equals('1'))
            {
                HS[port][command].IsSuccess = false;
                string errorCode = data.Substring(1, data.Length - 1);

                EmEfemErrorModule e = EmEfemErrorModule.E0;
                int c = 0;

                if (Enum.TryParse(errorCode.Substring(0, 2), out e) == true
                    && int.TryParse(errorCode.Substring(2, 4), out c) == true
                    )
                {                    
                    HS[port][command].ErrorModule = e;
                    HS[port][command].ErrorCode = c;
                }
                else
                {
                    HS[port][command].ErrorModule = EmEfemErrorModule.E0;
                    HS[port][command].ErrorCode = -1;
                }
                HS[port][command].IsCompleteRecved = true;
                return false;
            }
            else
            {
                HS[port][command].IsSuccess = true;
                HS[port][command].ReturnData = data.Contains('[') == true ? data.Split('[')[1].Split(']')[0] : string.Empty;
                HS[port][command].IsCompleteRecved = true;
                return true;
            }
        }
        #endregion

        #region Command
        private static string MakePacket(EmEfemCommand em, string port = "", string data = "")
        {
            EFEMData packet = new EFEMData(em.ToString(), port, data);

            return packet.ToString();
        }

        /// <summary>
        /// 지정한 Module(Port)을 초기화 시킨다. Stop 명령 이후 Init 명령 실행 후에 다른 명령의 실행이 가능하다.
        /// </summary>
        /// <param name="port">ROBOT, LOADPORT1, LOADPORT2, ALIGNER 사용 가능</param>
        public static void CmdInit(EmEfemPort targetPort)
        {
            if (targetPort != EmEfemPort.ROBOT
                && targetPort != EmEfemPort.LOADPORT1
                && targetPort != EmEfemPort.LOADPORT2
                && targetPort != EmEfemPort.ALIGNER
                )
            {
                return;
            }

            string packet = MakePacket(EmEfemCommand.INIT_, ((int)targetPort).ToString());
            HS[targetPort][EmEfemCommand.INIT_].CmdOn();
            SendMessage(packet);
        }

        /// <summary>
        /// 지정한 Module(Port)의 Error를 초기화 시킨다.
        /// </summary>
        /// <param name="targetPort">사용 가능 포트 : ROBOT, LOADPORT1, LOADPORT2, ALIGNER, ETC</param>
        public static void CmdReset(EmEfemPort targetPort)
        {
            if (targetPort != EmEfemPort.ROBOT 
                && targetPort != EmEfemPort.LOADPORT1 
                && targetPort != EmEfemPort.LOADPORT2 
                && targetPort != EmEfemPort.ETC
                && targetPort != EmEfemPort.ALIGNER
                )
            {
                return;
            }

            string packet = string.Empty;
            if (targetPort == EmEfemPort.ETC)
            {
                packet = MakePacket(EmEfemCommand.RESET, "F");
                HS[targetPort][EmEfemCommand.RESET].CmdOn();
                SendMessage(packet);
            }
            else
            {
                packet = MakePacket(EmEfemCommand.RESET, ((int)targetPort).ToString());
                HS[targetPort][EmEfemCommand.RESET].CmdOn();
                SendMessage(packet);
            }
        }
        //public static void CmdArmFold()
        //{
        //    string packet = MakePacket(EmEfemCommand.ARMFD, "0");
        //    HS[EmEfemPort.ROBOT][EmEfemCommand.ARMFD].CmdOn();
        //    SendMessage(packet);
        //}
        ///// <summary>
        ///// 시작위치와 반송위치를 지정하여 First Arm으로 Wafer를 Pick/Place 시킨다.
        ///// </summary>
        ///// <param name="arm">TRANS 명령을 수행 할 Robot Arm 선택 : FIRST_ARM, SECOND_ARM</param>
        ///// <param name="transfer">TRANS 명령 수행시 PICK 또는 PLACE 할지 결정</param>
        ///// <param name="targetPort">target 위치 Port(LOAD</param>
        ///// <param name="targetSlot">target 위치 Slot number (LoadPort 외 1 고정)</param>        
        //public static void CmdExtendArm(EmEfemRobotArm arm, EmEfemTransfer transfer, EmEfemPort targetPort, int targetSlot = 1)
        //{
        //    CmdRobotMoveBase(EmEfemCommand.EXTND, arm, transfer, targetPort, targetSlot);
        //}
        /// <summary>
        /// 시작위치와 반송위치를 지정하여 First Arm으로 Wafer를 Pick/Place 시킨다.
        /// </summary>
        /// <param name="arm">TRANS 명령을 수행 할 Robot Arm 선택 : FIRST_ARM, SECOND_ARM</param>
        /// <param name="transfer">TRANS 명령 수행시 PICK 또는 PLACE 할지 결정</param>
        /// <param name="targetPort">target 위치 Port(LOAD</param>
        /// <param name="targetSlot">target 위치 Slot number (LoadPort 외 1 고정)</param>
        public static void CmdTransferWafer(EmEfemRobotArm arm, EmEfemTransfer transfer, EmEfemPort targetPort, int targetSlot = 1)
        {
            CmdRobotMoveBase(EmEfemCommand.TRANS, arm, transfer, targetPort, targetSlot);
        }
        private static void CmdRobotMoveBase(EmEfemCommand cmd, EmEfemRobotArm arm, EmEfemTransfer transfer, EmEfemPort targetPort, int targetSlot = 1)
        {
            string data = string.Empty;
            //“@TRANS0[101#########]   LoadPort1 Slot 1에서 Robot의 First Arm(Upper)으로 Wafer Pick Action 을 수행하라

            data += (int)targetPort;
            data += targetSlot.ToString("00");

            if (transfer == EmEfemTransfer.PICK)
            {
                data += "###";
            }
            else if (transfer == EmEfemTransfer.PLACE)
            {
                data = "###" + data;
            }


            if (arm == EmEfemRobotArm.Upper)
            {
                data += "######";
            }
            else
            {
                data = "######" + data;
            }

            string packet = MakePacket(cmd, "0", data);
            HS[EmEfemPort.ROBOT][cmd].CmdOn();
            SendMessage(packet);
        }

        /// <summary>
        /// Robot을 지정된 Module(Port) 및 지정된 Slot 위치의 명령수행 대기 위치로 이동시킨다.
        /// </summary>
        /// <param name="arm">대기 할 Robot Arm</param>
        /// <param name="targetPort">대기 할 Target Port</param>
        /// <param name="slot">대기 할 Target Slot (LoadPort 외 1 고정)</param>
        public static void CmdWaitTransfer(EmEfemRobotArm arm, EmEfemPort targetPort, int slot = 1)
        {
            string data = string.Empty;

            if (targetPort != EmEfemPort.LOADPORT1
                && targetPort != EmEfemPort.LOADPORT2)
                slot = 1;

            data += (int)targetPort;
            data += slot.ToString("00");

            if (arm == EmEfemRobotArm.Upper)
            {
                data += "###";
            }
            else
            {
                data = "###" + data;
            }

            string packet = MakePacket(EmEfemCommand.WAITR, "0", data);
            HS[EmEfemPort.ROBOT][EmEfemCommand.WAITR].CmdOn();
            SendMessage(packet);
        }

        /// <summary>
        /// 지정된 Module의 상태를 점검한다.
        /// </summary>
        /// <param name="targetPort">사용 가능 포트 : ROBOT, LOADPORT1, LOADPORT2, ETC</param>
        public static void CmdCheckState(EmEfemPort targetPort)
        {
            if (targetPort != EmEfemPort.ROBOT
                && targetPort != EmEfemPort.LOADPORT1 
                && targetPort != EmEfemPort.LOADPORT2
                && targetPort != EmEfemPort.ALIGNER
                && targetPort != EmEfemPort.ETC)
            {
                return;
            }

            string _targetPort = string.Empty;
            if (targetPort == EmEfemPort.ETC)
            {
                _targetPort = "F";
            }
            else
            {
                _targetPort = ((int)targetPort).ToString();
            }

            string packet = MakePacket(EmEfemCommand.STAT_, _targetPort);
            HS[targetPort][EmEfemCommand.STAT_].CmdOn();
            SendMessage(packet);
        }

        /// <summary>
        /// 지정된 Robot을 일시 정지 시킨다. Pause명령은 Robot이 Trans명령을 수행하고 있을 경우에만 동작한다.
        /// </summary>
        public static void CmdPause()
        {
            string packet = MakePacket(EmEfemCommand.PAUSE, "0");
            HS[EmEfemPort.ROBOT][EmEfemCommand.PAUSE].CmdOn();
            SendMessage(packet);
        }

        /// <summary>
        /// 지정된 Robot이 Pause 상태에 있을 경우 Pause상태를 해제한다. Pause 상태를 해제하고 Pause상태 이전에 수행되고 있던 Job을 계속 진행한다.
        /// </summary>
        public static void CmdResume()
        {
            string packet = MakePacket(EmEfemCommand.RESUM, "0");
            HS[EmEfemPort.ROBOT][EmEfemCommand.RESUM].CmdOn();
            SendMessage(packet);
        }

        /// <summary>
        /// 동작 중인 Robot 또는 LPM을 정지시킨다.
        /// </summary>
        /// <param name="targetPort">사용 가능 포트 : ROBOT, LOADPORT1, LOADPORT2</param>
        public static void CmdStop(EmEfemPort targetPort)
        {
            if (targetPort != EmEfemPort.ROBOT && targetPort != EmEfemPort.LOADPORT1 && targetPort != EmEfemPort.LOADPORT2)
            {
                return;
            }

            string packet = MakePacket(EmEfemCommand.STOP_, ((int)targetPort).ToString());
            HS[targetPort][EmEfemCommand.STOP_].CmdOn();
            SendMessage(packet);
        }

        /// <summary>
        /// Aligner위의 Wafer에 대해 지정한 각도에서 웨이퍼 노치를 찾는다. 지정한 각도 근처에 없으면 알람
        /// </summary>        
        /// <param name="degree">0 ~ 359</param>
        /// <param name="ocrCylinderFowardAfterAlign">Align 완료 후 실린더 전진</param>
        /// <param name="waferID">wafer id 앞 3자리를 잘라서 recipe명으로 보냄</param>
        public static void CmdAlignment(int iDegree, bool ocrCylinderFowardAfterAlign, string waferID)
        {
            if (iDegree < 0 || iDegree >= 360)
            {
                return;
            }
            //if (waferID == null || waferID == string.Empty
            //    || waferID.Length < 3)
            //    return;

            //if (waferID.Length > 3)
            //    waferID = waferID.Remove(3);

            string data = string.Empty;
            data = string.Format("{0}/{1}/{2}", iDegree.ToString("D3"), ocrCylinderFowardAfterAlign ? 1 : 0, waferID);

            string packet = MakePacket(EmEfemCommand.ALIGN, "9", data);
            HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].CmdOn();
            SendMessage(packet);
        }

        ///// <summary>
        ///// 지정된 LoadPort 위에 적재된 FOUP을 Clamp 시킨다.
        ///// </summary>
        ///// <param name="targetPort">사용 가능 포트 : LOADPORT1, LOADPORT2</param>
        //public static void CmdClamp(EmEfemPort targetPort)
        //{
        //    if (targetPort != EmEfemPort.LOADPORT1 && targetPort != EmEfemPort.LOADPORT2)
        //    {
        //        return;
        //    }

        //    string packet = MakePacket(EmEfemCommand.CLAMP, ((int)targetPort).ToString());
        //    HS[targetPort][EmEfemCommand.CLAMP].CmdOn();
        //    SendMessage(packet);
        //}

        ///// <summary>
        ///// 지정된 LoadPort 위에 적재된 FOUP을 UnClamp 시킨다.
        ///// </summary>
        ///// <param name="targetPort">사용 가능 포트 : LOADPORT1, LOADPORT2</param>
        //public static void CmdUnClamp(EmEfemPort targetPort)
        //{
        //    if (targetPort != EmEfemPort.LOADPORT1 && targetPort != EmEfemPort.LOADPORT2)
        //    {
        //        return;
        //    }

        //    string packet = MakePacket(EmEfemCommand.UCLAM, ((int)targetPort).ToString());
        //    HS[targetPort][EmEfemCommand.UCLAM].CmdOn();
        //    SendMessage(packet);
        //}

        ///// <summary>
        ///// 지정된 LoadPort 위에 적재된 FOUP을 Docking 시킨다.
        ///// </summary>
        ///// <param name="targetPort">사용 가능 포트 : LOADPORT1, LOADPORT2</param>
        //public static void CmdDocking(EmEfemPort targetPort)
        //{
        //    if(targetPort != EmEfemPort.LOADPORT1 && targetPort != EmEfemPort.LOADPORT2)
        //    {
        //        return;
        //    }

        //    string packet = MakePacket(EmEfemCommand.DOCK_, ((int)targetPort).ToString());
        //    HS[targetPort][EmEfemCommand.DOCK_].CmdOn();
        //    SendMessage(packet);
        //}

        ///// <summary>
        ///// 지정된 LoadPort 위에 적재된 FOUP을 UnDocking 시킨다.
        ///// </summary>
        ///// <param name="targetPort">사용 가능 포트 : LOADPORT1, LOADPORT2</param>
        //public static void CmdUnDocking(EmEfemPort targetPort)
        //{
        //    if (targetPort != EmEfemPort.LOADPORT1 && targetPort != EmEfemPort.LOADPORT2)
        //    {
        //        return;
        //    }

        //    string packet = MakePacket(EmEfemCommand.UDOCK, ((int)targetPort).ToString());
        //    HS[targetPort][EmEfemCommand.UDOCK].CmdOn();
        //    SendMessage(packet);
        //}

        /// <summary>
        /// 지정된 LoadPort 위에 적재된 FOUP을 열고 Mapping 기능을 수행한다.
        /// </summary>
        /// <param name="targetPort">사용 가능 포트 : LOADPORT1, LOADPORT2</param>
        public static void CmdOpenFoup(EmEfemPort targetPort)
        {
            if (targetPort != EmEfemPort.LOADPORT1 && targetPort != EmEfemPort.LOADPORT2)
            {
                return;
            }

            string packet = MakePacket(EmEfemCommand.OPEN_, ((int)targetPort).ToString());
            HS[targetPort][EmEfemCommand.OPEN_].CmdOn();
            SendMessage(packet);
        }

        /// <summary>
        /// 지정된 LoadPort 위에 적재된 FOUP을 닫는 기능을 수행한다.
        /// </summary>
        /// <param name="targetPort">사용 가능 포트 : LOADPORT1, LOADPORT2</param>
        public static void CmdCloseFoup(EmEfemPort targetPort)
        {
            if (targetPort != EmEfemPort.LOADPORT1 && targetPort != EmEfemPort.LOADPORT2)
            {
                return;
            }

            string packet = MakePacket(EmEfemCommand.CLOSE, ((int)targetPort).ToString());
            HS[targetPort][EmEfemCommand.CLOSE].CmdOn();
            SendMessage(packet);
        }

        ///// <summary>
        ///// 지정된 LoadPort 위에 적재된 FOUP을 Open 시킨다.
        ///// </summary>
        ///// <param name="targetPort">사용 가능 포트 : LOADPORT1, LOADPORT2</param>
        //public static void CmdLoadFoup(EmEfemPort targetPort)
        //{
        //    if (targetPort != EmEfemPort.LOADPORT1 && targetPort != EmEfemPort.LOADPORT2)
        //    {
        //        return;
        //    }

        //    string packet = MakePacket(EmEfemCommand.LOAD_, ((int)targetPort).ToString());
        //    HS[targetPort][EmEfemCommand.LOAD_].CmdOn();
        //    SendMessage(packet);
        //}

        ///// <summary>
        ///// 지정된 LoadPort 위에 적재된 FOUP을 Close 시킨다.
        ///// </summary>
        ///// <param name="targetPort">사용 가능 포트 : LOADPORT1, LOADPORT2</param>
        //public static void CmdUnLoadFoup(EmEfemPort targetPort)
        //{
        //    if(targetPort != EmEfemPort.LOADPORT1 && targetPort != EmEfemPort.LOADPORT2)
        //    {
        //        return;
        //    }

        //    string packet = MakePacket(EmEfemCommand.ULOAD, ((int)targetPort).ToString());
        //    HS[targetPort][EmEfemCommand.ULOAD].CmdOn();
        //    SendMessage(packet);
        //}

        /// <summary>
        /// 지정된 load Port의 Mapping Data를 요구한다.
        /// </summary>
        /// <param name="targetPort">사용 가능 포트 : LOADPORT1, LOADPORT2</param>
        public static void CmdAskMappingData(EmEfemPort targetPort)
        {
            if (targetPort != EmEfemPort.LOADPORT1 && targetPort != EmEfemPort.LOADPORT2)
            {
                return;
            }

            string packet = MakePacket(EmEfemCommand.MAPP_, ((int)targetPort).ToString());
            HS[targetPort][EmEfemCommand.MAPP_].CmdOn();
            SendMessage(packet);
        }

        /// <summary>
        /// LPM의 Led Lamp르 컨트롤 한다.
        /// </summary>
        /// <param name="targetPort">사용 가능 포트 : LOADPORT1, LOADPORT2</param>
        /// <param name="ledType"> LED 램프 종류 선택 : CONTROL_MODE / LOAD_LAMP, UNLOAD_LAMP, AUTO_LAMP, MANUAL_LAMP, RESERVE_LAMP</param>
        /// <param name="ledStateType">LED 램프 컨트롤 타입 선택 : DISABLE, ENABLE / OFF, ON, BLINK</param>
        public static void CmdChangeLedLamp(EmEfemPort targetPort, EmEfemLampType ledType, EmEfemLampBuzzerState ledStateType)
        {
            if (targetPort != EmEfemPort.LOADPORT1 && targetPort != EmEfemPort.LOADPORT2)
            {
                return;
            }

            string data = string.Empty;

            data += (int)ledType;
            data += (int)ledStateType;

            string packet = MakePacket(EmEfemCommand.LPLED, ((int)targetPort).ToString(), data);
            HS[targetPort][EmEfemCommand.LPLED].CmdOn();
            SendMessage(packet);
        }

        /// <summary>
        /// EFEM의 Mode를 Auto 상태로 변경한다.
        /// </summary>
        public static void CmdChangeModeAuto()
        {
            string packet = MakePacket(EmEfemCommand.CHMDA, "F");
            HS[EmEfemPort.ETC][EmEfemCommand.CHMDA].CmdOn();
            SendMessage(packet);
        }

        /// <summary>
        /// EFEM의 Mode를 Manual 상태로 변경한다.
        /// </summary>
        public static void CmdChangeModeManual()
        {
            string packet = MakePacket(EmEfemCommand.CHMDM, "F");
            HS[EmEfemPort.ETC][EmEfemCommand.CHMDM].CmdOn();
            SendMessage(packet);
        }

        /// <summary>
        /// Signal Tower의 Lamp 및 Buzzer를 설정한다.
        /// </summary>
        /// <param name="targetPort"></param>
        /// <param name="redLamp"></param>
        /// <param name="yellowLamp"></param>
        /// <param name="greenLamp"></param>
        /// <param name="blueLamp"></param>
        /// <param name="buzzer1"></param>
        public static void CmdSetLampAndBuzzer(
            EmEfemLampBuzzerState redLamp,
            EmEfemLampBuzzerState yellowLamp,
            EmEfemLampBuzzerState greenLamp,
            EmEfemLampBuzzerState blueLamp,
            EmEfemLampBuzzerState buzzer1
            )
        {
            if (buzzer1 == EmEfemLampBuzzerState.BLINK)
                buzzer1 = EmEfemLampBuzzerState.ON;

            string data = string.Empty;

            data += (int)redLamp;
            data += (int)yellowLamp;
            data += (int)greenLamp;
            data += (int)blueLamp;
            data += (int)buzzer1;
            data += (int)0;

            string packet = MakePacket(EmEfemCommand.SIGLM, "F", data);
            HS[EmEfemPort.ETC][EmEfemCommand.SIGLM].CmdOn();
            SendMessage(packet);
        }
        /// <summary>
        /// Aligner 에 Align Ready 신호를 보낸다.
        /// </summary>
        public static void CmdPreAlignerReady()
        {
            string data = string.Empty;

            string packet = MakePacket(EmEfemCommand.PARDY, "9");
            HS[EmEfemPort.ALIGNER][EmEfemCommand.PARDY].CmdOn();
            SendMessage(packet);
        }
        /// <summary>     
        /// Aligner 에 Align Rotation 신호를 보낸다.     
        /// </summary>
        /// <param name="degree">0~359</param>
        /// <param name="ocrCylinderFowardAfterAlign">회전 후 OCR 실린더 전진</param>
        public static void CmdPreAlignerRotation(int degree, bool ocrCylinderFowardAfterAlign)
        {
            if (degree < 0 || degree >= 360)
            {
                return;
            }

            string data = string.Empty;
            data = string.Format("{0}/{1}", degree.ToString("D3"), ocrCylinderFowardAfterAlign ? 1 : 0);

            string packet = MakePacket(EmEfemCommand.PATRR, "9", data);
            HS[EmEfemPort.ALIGNER][EmEfemCommand.PATRR].CmdOn();
            SendMessage(packet);
        }
        /// <summary>
        /// Aligner 에 Align Send Ready 신호를 보낸다.
        /// </summary>
        public static void CmdPreAlignerSendReady()
        {
            string data = string.Empty;

            string packet = MakePacket(EmEfemCommand.PASRD, "9");
            HS[EmEfemPort.ALIGNER][EmEfemCommand.PASRD].CmdOn();
            SendMessage(packet);
        }
        #endregion
    }
}
