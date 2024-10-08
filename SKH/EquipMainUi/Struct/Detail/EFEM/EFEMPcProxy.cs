using EquipMainUi.Common.Convenience;
using EquipMainUi.PreAligner;
using EquipMainUi.PreAligner.Recipe;
using EquipMainUi.Struct.Detail.EziStep;
using EquipMainUi.Struct.TransferData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace EquipMainUi.Struct.Detail.EFEM
{
    public class EFEMPcCmd
    {
        public string Name { get; set; }
        public EmEfemPort Port { get; set; }
        public EmEfemCommand Cmd { get; set; }

        private int SignalTimeOut = 20; 
        public EM_AL_LST SignalTimeover { get; set; }
        private int CompleteTimeOut = 30; 
        public EM_AL_LST CompleteTimeover { get; set; }

        public EmEfemErrorModule ErrorModule { get; set; }
        public int ErrorCode { get; set; }
        public EM_AL_LST CommandFailAlarm { get; set; }


        public object Tag { get; set; }

        public int Step = 0;
        private DateTime _stepTime = DateTime.Now;
        public bool IsStepRunning { get { return Step != 0; } }
        public bool UseHandShakeLog { get; set; }

        public Action<Equipment, EFEMPcCmd> SendCmd { get; set; }        
        public Func<Equipment, EFEMPcCmd, bool> RecvAck { get; set; }
        public Func<Equipment, EFEMPcCmd, bool> RecvComplete { get; set; }
        public Action<Equipment, EFEMPcCmd> ComepleteSuccess { get; set; }

        public void LogicWorking(Equipment equip)
        {
            if(this.Port == EmEfemPort.ALIGNER && GG.IsDitPreAligner)
            {

            }
            else if (GG.EfemNoUse)
            {
                Step = 0;
                equip.Efem.Proxy.HS[Port].SetMoving(Cmd, false);
                return;
            }

            if (Step == 20 && (DateTime.Now - _stepTime).TotalSeconds > SignalTimeOut)
            {
                Logger.Log.AppendLine(LogLevel.Error, "제어↔EFEM PC {0} 시그널 TIME OVER", Name);
                AlarmMgr.Instance.Happen(equip, SignalTimeover);
                Step = 0;
                equip.Efem.Proxy.HS[Port].SetMoving(Cmd, false);
                return;
            }
            if (Step == 30 && (DateTime.Now - _stepTime).TotalSeconds > CompleteTimeOut)
            {
                Logger.Log.AppendLine(LogLevel.Error, "제어↔EFEM PC {0} Complete TIME OVER", Name);
                AlarmMgr.Instance.Happen(equip, CompleteTimeover);
                Step = 0;
                equip.Efem.Proxy.HS[Port].SetMoving(Cmd, false);
                return;
            }
            if (Step == 40 && (DateTime.Now - _stepTime).TotalSeconds > CompleteTimeOut)
            {
                AlarmMgr.Instance.Happen(equip, CommandFailAlarm);
                Logger.Log.AppendLine(LogLevel.Info, "EFEM→제어PC({0}) Complete 에러 (NoErrorCode)", Name);
                Logger.EFEMCommLog.AppendLine(LogLevel.Error, "EFEM→제어PC({0}) Complete 에러 (NoErrorCode)", Name);
                equip.Efem.Proxy.HS[Port].SetMoving(Cmd, false);
                Step = 0;
                return;
            }

            if (Step == 0)
            {
            }
            else if (Step == 10)
            {
                _stepTime = DateTime.Now;
                ErrorModule = EmEfemErrorModule.E0;
                ErrorCode = -1;
                SendCmd(equip, this);
                if (UseHandShakeLog)
                    Logger.Log.AppendLine(LogLevel.Info, "제어→EFEM PC {0} 시그널 시작", Name);
                Step = 20;
            }
            else if (Step == 20)
            {
                if (RecvAck(equip, this) == true)
                {
                    if (UseHandShakeLog)
                        Logger.Log.AppendLine(LogLevel.Info, "EFEM→제어 PC {0} 시그널 응답 확인", Name);
                    Step = 30;
                }
            }
            else if (Step == 30)
            {
                if (RecvComplete(equip, this) == true)
                {
                    equip.Efem.Proxy.HS[Port].SetMoving(Cmd, false);
                    if (UseHandShakeLog)
                        Logger.Log.AppendLine(LogLevel.Info, "EFEM→제어 PC {0} 시그널 Complete 확인 (성공?:{1})", Name, EFEMTcp.HS[this.Port][this.Cmd].IsSuccess);
                    if (EFEMTcp.HS[this.Port][this.Cmd].IsSuccess == false)
                    {
                        Step = 40;
                    }
                    else
                    {
                        ComepleteSuccess?.Invoke(equip, this);
                        Step = 00;
                    }
                }
            }
            else if (Step == 40)
            {
                if (EFEMTcp.HS[this.Port][this.Cmd].ErrorCode != -1)
                {
                    ErrorModule = EFEMTcp.HS[this.Port][this.Cmd].ErrorModule;
                    ErrorCode = EFEMTcp.HS[this.Port][this.Cmd].ErrorCode;
                    string errMsg = EFEMError.GetErrorDesc(ErrorModule, ErrorCode);

                    if (ErrorCode != 75 && ErrorCode > 0)
                    {
                        if (EFEMError.IsCriticalAlarm(ErrorModule, ErrorCode) == true && 
                            ((this.Cmd == EmEfemCommand.TRANS && GG.Equip.Efem.LastRobotPort != EmEfemPort.ALIGNER) || this.Cmd == EmEfemCommand.ALIGN))
                            AlarmMgr.Instance.Happen(equip, EFEMError.GetAlarmCode(ErrorModule, ErrorCode));
                        else
                            AlarmMgr.Instance.Happen(equip, CommandFailAlarm);
                    }

                    Logger.Log.AppendLine(LogLevel.Error, "EFEM→제어PC({0}) Complete 에러 ({3}:{2}:{1})", Name, errMsg, ErrorCode, ErrorModule);
                    Logger.EFEMCommLog.AppendLine(LogLevel.Error, "EFEM→제어PC({0}) Complete 에러 ({3}:{2}:{1})", Name, errMsg, ErrorCode, ErrorModule);
                    Step = 00;
                }
            }
        }

        public void ResetStep()
        {
            Step = 0;
        }

        internal void SetCompleteTimeout(int v)
        {
            CompleteTimeOut = v;
        }
    }
    public class EFEMPcEvent
    {        
        public string Name { get; set; }
        public EmEfemPort Port { get; set; }
        public EmEfemCommand Cmd { get; set; }

        private int SignalTimeOut = 15;
        public EM_AL_LST TimeOver { get; set; }
        public object Tag { get; set; }

        private bool _isComplete { get; set; }
        public bool IsComplete { get { return _isComplete; } }

        public int Step = 0;
        private DateTime _stepTime = DateTime.Now;
        public bool IsStepRunning { get { return Step != 0; } }

        public Func<Equipment, EFEMPcEvent, bool> RecvComplete { get; set; }


        public void LogicWorking(Equipment equip)
        {
            if (Step == 0)
            {
                Step = 10;
            }
            else if (Step == 10)
            {
                if (RecvComplete(equip, this) == true)
                {
                    _isComplete = true;
                    Step = 40;
                }
            }
            else if (Step == 40)
            {
                Logger.Log.AppendLine(LogLevel.Info, "EFEM→제어 PC {0} 이벤트 완료", Name);
                Step = 0;
            }
        }
        public void ResetStep()
        {
            Step = 0;
        }
    }
    public class EFEMPortProxy // 기존의 hsmspcproxy처럼 동작할 수 있게.
    {
        public EmEfemPort Port;
        public EmEfemCommand LastActionCmd;
        public EmEfemCommand LastStatusCmd;
        public bool IsProcessingMovingCmd;
        public bool IsFirstSlot;

        public Dictionary<EmEfemCommand, EFEMPcCmd> LstCmd = new Dictionary<EmEfemCommand, EFEMPcCmd>();
        public Dictionary<EmEfemCommand, EFEMPcEvent> LstEvent = new Dictionary<EmEfemCommand, EFEMPcEvent>();        

        public bool HasErrorCode()
        {
            return LstCmd.ContainsKey(LastActionCmd) ? LstCmd[LastActionCmd].ErrorCode > 0 : false;
        }
        public int GetErrorCode()
        {
            return LstCmd.ContainsKey(LastActionCmd) ? LstCmd[LastActionCmd].ErrorCode : -1;
        }
        /// <summary>
        /// E0.000 Format
        /// </summary>
        /// <returns></returns>
        public string GetErrorCodeStr()
        {
            return LstCmd.ContainsKey(LastActionCmd) ? string.Format("{0}.{1}", LstCmd[LastActionCmd].ErrorModule, LstCmd[LastActionCmd].ErrorCode.ToString("000")) : "";
        }
        public string GetErrorDesc()
        {
            return LstCmd.ContainsKey(LastActionCmd) ? EFEMTcp.HS[this.Port][this.LastActionCmd].GetErrorDesc() : "";
        }

        public EFEMPortProxy(EmEfemPort port)
        {
            Port = port;
        }

        public bool HasCmd(EmEfemCommand cmd)
        {
            return LstCmd.ContainsKey(cmd);
        }

        public bool IsStepRunning(EmEfemCommand curCmd)
        {
            return LstCmd[curCmd].IsStepRunning;
        }
        public bool IsMovingCommand(EmEfemCommand curCmd)
        {
            return curCmd == EmEfemCommand.TRANS
                || curCmd == EmEfemCommand.WAITR
                || curCmd == EmEfemCommand.OPEN_
                || curCmd == EmEfemCommand.CLOSE
                || curCmd == EmEfemCommand.INIT_
                || curCmd == EmEfemCommand.RESET
                || curCmd == EmEfemCommand.ALIGN
                ;
        }
        public bool IsActionCommand(EmEfemPort port, EmEfemCommand curCmd)
        {
            return IsMovingCommand(curCmd)                               
                || curCmd == EmEfemCommand.STOP_
                || curCmd == EmEfemCommand.PAUSE
                || curCmd == EmEfemCommand.RESUM
                ;
        }
        public void SetMoving(EmEfemCommand curCmd, bool isMoving)
        {
            if (IsMovingCommand(curCmd))
                this.IsProcessingMovingCmd = isMoving;
            else if (curCmd == EmEfemCommand.STOP_ && isMoving == false)
                this.IsProcessingMovingCmd = isMoving;
        }
    }
    public class EFEMPcProxy : UnitBase
    {
        public Dictionary<EmEfemPort, EFEMPortProxy> HS = new Dictionary<EmEfemPort, EFEMPortProxy>();      
        
        public EFEMStatus EfemStat = new EFEMStatus();
        public EFEMRobotStatus RobotStat = new EFEMRobotStatus();
        public EFEMLoadPortStatus LoadPort1Stat = new EFEMLoadPortStatus();
        public EFEMLoadPortStatus LoadPort2Stat = new EFEMLoadPortStatus();
        public EFEMAlignerStatus AlignerStat = new EFEMAlignerStatus();
        
        public bool IsInitDone {
            get
            {
                return EfemStat.IsDoorClose == true
                    && EfemStat.IsEMO == false
                    && EfemStat.IsError == false
                    && EfemStat.IsInitComplete == true
                    && EfemStat.IsLPMCDAError == false
                    && EfemStat.IsLPMVacError == false
                    && EfemStat.IsMainIonizerError == false
                    && RobotStat.IsAlarm == false
                    && RobotStat.IsArmUnFold == false
                    && RobotStat.IsMoving == false
                    && RobotStat.IsProgramRunning == false
                    && RobotStat.IsServoOn == true
                    && LoadPort1Stat.IsBusy == false
                    && LoadPort1Stat.IsError == false
                    && LoadPort1Stat.IsHomeComplete == true
                    && LoadPort1Stat.IsWaferStickOut == false
                    && LoadPort2Stat.IsBusy == false
                    && LoadPort2Stat.IsError == false
                    && LoadPort2Stat.IsHomeComplete == true
                    && LoadPort2Stat.IsWaferStickOut == false
                    && AlignerStat.IsStatus == EmEfemAlignerStatus.READY                    
                    ;
            }
        }
        public bool IsNoError
        {
            get
            {
                return EfemStat.IsEMO == false
                    && EfemStat.IsError == false
                    && EfemStat.IsLPMCDAError == false
                    && EfemStat.IsLPMVacError == false
                    && EfemStat.IsMainIonizerError == false
                    && RobotStat.IsAlarm == false
                    && RobotStat.IsServoOn == true
                    && LoadPort1Stat.IsError == false
                    && LoadPort1Stat.IsHomeComplete == true
                    && LoadPort1Stat.IsWaferStickOut == false
                    && LoadPort2Stat.IsError == false
                    && LoadPort2Stat.IsHomeComplete == true
                    && LoadPort2Stat.IsWaferStickOut == false
                    && AlignerStat.IsStatus != EmEfemAlignerStatus.ERROR;
                ;
            }
        }
        
        private EmEfemCommand _lastCmd;
        public EmEfemCommand LastCmd { get { return _lastCmd; } }

        public bool IsConnected { get { return EFEMTcp.IsConnected; } }       

        public bool Connect(Equipment equip)
        {
            if (GG.TestMode)
                return EFEMTcp.Connect(IPCtrl.GetMyIp(), equip.InitSetting.EfemPort);
            else
                return EFEMTcp.Connect(equip.InitSetting.EfemIP, equip.InitSetting.EfemPort);
        }
        public void Connect()
        {
            if (GG.TestMode)
                EFEMTcp.Connect(IPCtrl.GetMyIp(), GG.Equip.InitSetting.EfemPort);
            else
                EFEMTcp.Connect(GG.Equip.InitSetting.EfemIP, GG.Equip.InitSetting.EfemPort);
        }
        public bool Initailize(Equipment equip)
        {            
            #region Cmd
            HS[EmEfemPort.ETC] = new EFEMPortProxy(EmEfemPort.ETC);
            HS[EmEfemPort.ETC].LstCmd[EmEfemCommand.STAT_] = new EFEMPcCmd()        { Port = EmEfemPort.ETC,        Cmd = EmEfemCommand.STAT_,  Name = "EFEM_STAT_",      SendCmd = OnSTAT_Cmd, RecvAck = OnAck, RecvComplete = OnSTAT_Complete, CommandFailAlarm = EM_AL_LST.AL_0790_EFEM_SYSTEM_STATUS_FAIL,           SignalTimeover = EM_AL_LST.AL_0717_ETC_STATUS_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0768_ETC_STATUS_COMPLETE_TIMEOVER, UseHandShakeLog = false, };            
            HS[EmEfemPort.ETC].LstCmd[EmEfemCommand.SIGLM] = new EFEMPcCmd()        { Port = EmEfemPort.ETC,        Cmd = EmEfemCommand.SIGLM,  Name = "EFEM_SIGLM",      SendCmd = OnSIGLMCmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0791_EFEM_SYSTEM_SIGLM_FAIL,           SignalTimeover = EM_AL_LST.AL_0718_ETC_SIGLM_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0769_ETC_SIGLM_COMPLETE_TIMEOVER};
            HS[EmEfemPort.ETC].LstCmd[EmEfemCommand.RESET] = new EFEMPcCmd()        { Port = EmEfemPort.ETC,        Cmd = EmEfemCommand.RESET,  Name = "EFEM_RESET",      SendCmd = OnRESETCmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0792_EFEM_SYSTEM_RESET_FAIL,           SignalTimeover = EM_AL_LST.AL_0719_ETC_RESET_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0770_ETC_RESET_COMPLETE_TIMEOVER};
            HS[EmEfemPort.ETC].LstCmd[EmEfemCommand.CHMDA] = new EFEMPcCmd()        { Port = EmEfemPort.ETC,        Cmd = EmEfemCommand.CHMDA,  Name = "EFEM_CHMDA",      SendCmd = OnCHMDACmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0793_EFEM_SYSTEM_CHMDA_FAIL,           SignalTimeover = EM_AL_LST.AL_0720_ETC_CHMDA_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0771_ETC_CHMDA_COMPLETE_TIMEOVER };
            HS[EmEfemPort.ETC].LstCmd[EmEfemCommand.CHMDM] = new EFEMPcCmd()        { Port = EmEfemPort.ETC,        Cmd = EmEfemCommand.CHMDM,  Name = "EFEM_CHMDM",      SendCmd = OnCHMDMCmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0794_EFEM_SYSTEM_CHMDM_FAIL,           SignalTimeover = EM_AL_LST.AL_0721_ETC_CHMDM_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0772_ETC_CHMDM_COMPLETE_TIMEOVER};
                                                                                                                                                                                                                                                                                                                     
            HS[EmEfemPort.ROBOT] = new EFEMPortProxy(EmEfemPort.ROBOT);                                                                                                                                                                                                                                              
            HS[EmEfemPort.ROBOT].LstCmd[EmEfemCommand.STAT_] = new EFEMPcCmd()      { Port = EmEfemPort.ROBOT,      Cmd = EmEfemCommand.STAT_,  Name = "ROBOT_STAT_",     SendCmd = OnSTAT_Cmd, RecvAck = OnAck, RecvComplete = OnSTAT_Complete, CommandFailAlarm = EM_AL_LST.AL_0810_EFEM_ROBOT_STATUS_CMD_FAIL,         SignalTimeover = EM_AL_LST.AL_0680_ROBOT_STATUS_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0731_ROBOT_STATUS_COMPLETE_TIMEOVER ,UseHandShakeLog = false };
            HS[EmEfemPort.ROBOT].LstCmd[EmEfemCommand.INIT_] = new EFEMPcCmd()      { Port = EmEfemPort.ROBOT,      Cmd = EmEfemCommand.INIT_,  Name = "ROBOT_INIT_",     SendCmd = OnINIT_Cmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0811_EFEM_ROBOT_INITIAL_CMD_FAIL,     SignalTimeover = EM_AL_LST.AL_0681_ROBOT_INIT_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0732_ROBOT_INIT_COMPLETE_TIMEOVER };
            HS[EmEfemPort.ROBOT].LstCmd[EmEfemCommand.RESET] = new EFEMPcCmd()      { Port = EmEfemPort.ROBOT,      Cmd = EmEfemCommand.RESET,  Name = "ROBOT_RESET",     SendCmd = OnRESETCmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0812_EFEM_ROBOT_RESET_CMD_FAIL,     SignalTimeover = EM_AL_LST.AL_0682_ROBOT_RESET_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0733_ROBOT_RESET_COMPLETE_TIMEOVER};
            HS[EmEfemPort.ROBOT].LstCmd[EmEfemCommand.PAUSE] = new EFEMPcCmd()      { Port = EmEfemPort.ROBOT,      Cmd = EmEfemCommand.PAUSE,  Name = "ROBOT_PAUSE",     SendCmd = OnPAUSECmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0813_EFEM_ROBOT_PAUSE_CMD_FAIL,     SignalTimeover = EM_AL_LST.AL_0683_ROBOT_RESUME_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0734_ROBOT_RESUME_COMPLETE_TIMEOVER };
            HS[EmEfemPort.ROBOT].LstCmd[EmEfemCommand.RESUM] = new EFEMPcCmd()      { Port = EmEfemPort.ROBOT,      Cmd = EmEfemCommand.RESUM,  Name = "ROBOT_RESUM",     SendCmd = OnRESUMCmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0814_EFEM_ROBOT_RESUME_CMD_FAIL,     SignalTimeover = EM_AL_LST.AL_0684_ROBOT_PAUSE_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0735_ROBOT_PAUSE_COMPLETE_TIMEOVER };
            HS[EmEfemPort.ROBOT].LstCmd[EmEfemCommand.STOP_] = new EFEMPcCmd()      { Port = EmEfemPort.ROBOT,      Cmd = EmEfemCommand.STOP_,  Name = "ROBOT_STOP_",     SendCmd = OnSTOP_Cmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0815_EFEM_ROBOT_STOP_CMD_FAIL,     SignalTimeover = EM_AL_LST.AL_0685_ROBOT_STOP_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0736_ROBOT_STOP_COMPLETE_TIMEOVER };
            HS[EmEfemPort.ROBOT].LstCmd[EmEfemCommand.WAITR] = new EFEMPcCmd()      { Port = EmEfemPort.ROBOT,      Cmd = EmEfemCommand.WAITR,  Name = "ROBOT_WAITR",     SendCmd = OnWAITRCmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0816_EFEM_ROBOT_WAITR_CMD_FAIL,     SignalTimeover = EM_AL_LST.AL_0686_ROBOT_WAIT_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0737_ROBOT_WAIT_COMPLETE_TIMEOVER };
            HS[EmEfemPort.ROBOT].LstCmd[EmEfemCommand.TRANS] = new EFEMPcCmd()      { Port = EmEfemPort.ROBOT,      Cmd = EmEfemCommand.TRANS,  Name = "ROBOT_TRANS",     SendCmd = OnTRANSCmd, RecvAck = OnAck, RecvComplete = OnTransComplete, ComepleteSuccess = OnTransCompleteSuccess, CommandFailAlarm = EM_AL_LST.AL_0817_EFEM_ROBOT_TRANS_CMD_FAIL,     SignalTimeover = EM_AL_LST.AL_0687_ROBOT_TRANS_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0738_ROBOT_TRANS_COMPLETE_TIMEOVER };
            //HS[EmEfemPort.ROBOT].LstCmd[EmEfemCommand.EXTND]/*미사용*/ = new EFEMPcCmd() { Port = EmEfemPort.ROBOT, Cmd = EmEfemCommand.TRANS, Name = "ROBOT_EXTND", SendCmd = OnEXTNDCmd, RecvAck = OnAck, RecvComplete = OnComplete };                                                                            
            //HS[EmEfemPort.ROBOT].LstCmd[EmEfemCommand.ARMFD]/*미사용*/ = new EFEMPcCmd() { Port = EmEfemPort.ROBOT, Cmd = EmEfemCommand.TRANS, Name = "ROBOT_ARMFD", SendCmd = OnARMFDCmd, RecvAck = OnAck, RecvComplete = OnComplete };                                                                            

            HS[EmEfemPort.LOADPORT1] = new EFEMPortProxy(EmEfemPort.LOADPORT1);                                                                                                                                                                                                                                      
            HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.STAT_] = new EFEMPcCmd()  { Port = EmEfemPort.LOADPORT1,  Cmd = EmEfemCommand.STAT_,  Name = "LoadPort1_STAT_", SendCmd = OnSTAT_Cmd, RecvAck = OnAck, RecvComplete = OnSTAT_Complete, CommandFailAlarm = EM_AL_LST.AL_0880_EFEM_LPM1_STATUS_CMD_FAIL,     SignalTimeover = EM_AL_LST.AL_0690_LPM1_STATUS_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0741_LPM1_STATUS_COMPLETE_TIMEOVER  ,UseHandShakeLog = false };
            HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.INIT_] = new EFEMPcCmd()  { Port = EmEfemPort.LOADPORT1,  Cmd = EmEfemCommand.INIT_,  Name = "LoadPort1_INIT_", SendCmd = OnINIT_Cmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0881_EFEM_LPM1_INITIAL_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0691_LPM1_INIT_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0742_LPM1_INIT_COMPLETE_TIMEOVER };
            HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.STOP_] = new EFEMPcCmd()  { Port = EmEfemPort.LOADPORT1,  Cmd = EmEfemCommand.STOP_,  Name = "LoadPort1_STOP_", SendCmd = OnSTOP_Cmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0882_EFEM_LPM1_STOP_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0693_LPM1_STOP_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0744_LPM1_STOP_COMPLETE_TIMEOVER};
            HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.RESET] = new EFEMPcCmd()  { Port = EmEfemPort.LOADPORT1,  Cmd = EmEfemCommand.RESET,  Name = "LoadPort1_RESET", SendCmd = OnRESETCmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0883_EFEM_LPM1_RESET_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0692_LPM1_RESET_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0743_LPM1_RESET_COMPLETE_TIMEOVER};
            //HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.CLAMP]/*미사용*/ = new EFEMPcCmd() { Port = EmEfemPort.LOADPORT1, Cmd = EmEfemCommand.CLAMP, Name = "LoadPort1_CLAMP", SendCmd = OnCLAMPCmd, RecvAck = OnAck, RecvComplete = OnComplete };
            //HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.UCLAM]/*미사용*/ = new EFEMPcCmd() { Port = EmEfemPort.LOADPORT1, Cmd = EmEfemCommand.UCLAM, Name = "LoadPort1_UCLAM", SendCmd = OnUCLAMCmd, RecvAck = OnAck, RecvComplete = OnComplete };
            //HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.DOCK_]/*미사용*/ = new EFEMPcCmd() { Port = EmEfemPort.LOADPORT1, Cmd = EmEfemCommand.DOCK_, Name = "LoadPort1_DOCK_", SendCmd = OnDOCK_Cmd, RecvAck = OnAck, RecvComplete = OnComplete };
            //HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.UDOCK]/*미사용*/ = new EFEMPcCmd() { Port = EmEfemPort.LOADPORT1, Cmd = EmEfemCommand.UDOCK, Name = "LoadPort1_UDOCK", SendCmd = OnUDOCKCmd, RecvAck = OnAck, RecvComplete = OnComplete };
            HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.OPEN_] = new EFEMPcCmd()  { Port = EmEfemPort.LOADPORT1,  Cmd = EmEfemCommand.OPEN_,  Name = "LoadPort1_OPEN_", SendCmd = OnOPEN_Cmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0884_EFEM_LPM1_OPEN_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0694_LPM1_OPEN_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0745_LPM1_OPEN_COMPLETE_TIMEOVER };
            HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.CLOSE] = new EFEMPcCmd()  { Port = EmEfemPort.LOADPORT1,  Cmd = EmEfemCommand.CLOSE,  Name = "LoadPort1_CLOSE", SendCmd = OnCLOSECmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0885_EFEM_LPM1_CLOSE_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0695_LPM1_CLOSE_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0746_LPM1_CLOSE_COMPLETE_TIMEOVER };
            //HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.LOAD_]/*미사용*/ = new EFEMPcCmd() { Port = EmEfemPort.LOADPORT1, Cmd = EmEfemCommand.LOAD_, Name = "LoadPort1_LOAD_", SendCmd = OnLOAD_Cmd, RecvAck = OnAck, RecvComplete = OnComplete };
            //HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.ULOAD]/*미사용*/ = new EFEMPcCmd() { Port = EmEfemPort.LOADPORT1, Cmd = EmEfemCommand.ULOAD, Name = "LoadPort1_ULOAD", SendCmd = OnULOADCmd, RecvAck = OnAck, RecvComplete = OnComplete };
            HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.MAPP_] = new EFEMPcCmd()  { Port = EmEfemPort.LOADPORT1,  Cmd = EmEfemCommand.MAPP_,  Name = "LoadPort1_MAPP_", SendCmd = OnMAPP_Cmd, RecvAck = OnAck, RecvComplete = OnMAPP_Complete, CommandFailAlarm = EM_AL_LST.AL_0886_EFEM_LPM1_MAPPING_CMD_FAIL,   SignalTimeover = EM_AL_LST.AL_0696_LPM1_MAPPING_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0747_LPM1_MAPPING_COMPLETE_TIMEOVER };
            HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.LPLED] = new EFEMPcCmd()  { Port = EmEfemPort.LOADPORT1,  Cmd = EmEfemCommand.LPLED,  Name = "LoadPort1_LPLED", SendCmd = OnLPLEDCmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0887_EFEM_LPM1_LPLED_CMD_FAIL,     SignalTimeover = EM_AL_LST.AL_0697_LPM1_LPLED_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0748_LPM1_LPLED_COMPLETE_TIMEOVER };

            HS[EmEfemPort.LOADPORT2] = new EFEMPortProxy(EmEfemPort.LOADPORT2);
            HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.STAT_] = new EFEMPcCmd()  { Port = EmEfemPort.LOADPORT2,  Cmd = EmEfemCommand.STAT_,  Name = "LoadPort2_STAT_", SendCmd = OnSTAT_Cmd, RecvAck = OnAck, RecvComplete = OnSTAT_Complete, CommandFailAlarm = EM_AL_LST.AL_0890_EFEM_LPM2_STATUS_CMD_FAIL,     SignalTimeover = EM_AL_LST.AL_0700_LPM2_STATUS_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0750_LPM2_STATUS_COMPLETE_TIMEOVER   ,UseHandShakeLog = false };
            HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.INIT_] = new EFEMPcCmd()  { Port = EmEfemPort.LOADPORT2,  Cmd = EmEfemCommand.INIT_,  Name = "LoadPort2_INIT_", SendCmd = OnINIT_Cmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0891_EFEM_LPM2_INITIAL_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0701_LPM2_INIT_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST .AL_0751_LPM2_INIT_COMPLETE_TIMEOVER};
            HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.STOP_] = new EFEMPcCmd()  { Port = EmEfemPort.LOADPORT2,  Cmd = EmEfemCommand.STOP_,  Name = "LoadPort2_STOP_", SendCmd = OnSTOP_Cmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0892_EFEM_LPM2_STOP_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0703_LPM2_STOP_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0753_LPM2_STOP_COMPLETE_TIMEOVER};
            HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.RESET] = new EFEMPcCmd()  { Port = EmEfemPort.LOADPORT2,  Cmd = EmEfemCommand.RESET,  Name = "LoadPort2_RESET", SendCmd = OnRESETCmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0893_EFEM_LPM2_RESET_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0702_LPM2_RESET_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST .AL_0752_LPM2_RESET_COMPLETE_TIMEOVER};
            //HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.CLAMP]/*미사용*/  = new EFEMPcCmd() { Port = EmEfemPort.LOADPORT2, Cmd = EmEfemCommand.CLAMP, Name = "LoadPort2_CLAMP", SendCmd = OnCLAMPCmd, RecvAck = OnAck, RecvComplete = OnComplete };
            //HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.UCLAM]/*미사용*/  = new EFEMPcCmd() { Port = EmEfemPort.LOADPORT2, Cmd = EmEfemCommand.UCLAM, Name = "LoadPort2_UCLAM", SendCmd = OnUCLAMCmd, RecvAck = OnAck, RecvComplete = OnComplete };
            //HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.DOCK_]/*미사용*/  = new EFEMPcCmd() { Port = EmEfemPort.LOADPORT2, Cmd = EmEfemCommand.DOCK_, Name = "LoadPort2_DOCK_", SendCmd = OnDOCK_Cmd, RecvAck = OnAck, RecvComplete = OnComplete };
            //HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.UDOCK]/*미사용*/  = new EFEMPcCmd() { Port = EmEfemPort.LOADPORT2, Cmd = EmEfemCommand.UDOCK, Name = "LoadPort2_UDOCK", SendCmd = OnUDOCKCmd, RecvAck = OnAck, RecvComplete = OnComplete };
            HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.OPEN_] = new EFEMPcCmd()  { Port = EmEfemPort.LOADPORT2,  Cmd = EmEfemCommand.OPEN_,  Name = "LoadPort2_OPEN_", SendCmd = OnOPEN_Cmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0894_EFEM_LPM2_OPEN_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0704_LPM2_OPEN_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0754_LPM2_OPEN_COMPLETE_TIMEOVER };
            HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.CLOSE] = new EFEMPcCmd()  { Port = EmEfemPort.LOADPORT2,  Cmd = EmEfemCommand.CLOSE,  Name = "LoadPort2_CLOSE", SendCmd = OnCLOSECmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0895_EFEM_LPM2_CLOSE_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0705_LPM2_CLOSE_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0755_LPM2_CLOSE_COMPLETE_TIMEOVER };
            //HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.LOAD_]/*미사용*/  = new EFEMPcCmd() { Port = EmEfemPort.LOADPORT2, Cmd = EmEfemCommand.LOAD_, Name = "LoadPort2_LOAD_", SendCmd = OnLOAD_Cmd, RecvAck = OnAck, RecvComplete = OnComplete };                                                              
            //HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.ULOAD]/*미사용*/  = new EFEMPcCmd() { Port = EmEfemPort.LOADPORT2, Cmd = EmEfemCommand.ULOAD, Name = "LoadPort2_ULOAD", SendCmd = OnULOADCmd, RecvAck = OnAck, RecvComplete = OnComplete };                                                                   
            HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.MAPP_] = new EFEMPcCmd()  { Port = EmEfemPort.LOADPORT2,  Cmd = EmEfemCommand.MAPP_,  Name = "LoadPort2_MAPP_", SendCmd = OnMAPP_Cmd, RecvAck = OnAck, RecvComplete = OnMAPP_Complete, CommandFailAlarm = EM_AL_LST.AL_0896_EFEM_LPM2_MAPPING_CMD_FAIL,   SignalTimeover = EM_AL_LST.AL_0706_LPM2_MAPPING_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0756_LPM2_MAPPING_COMPLETE_TIMEOVER};
            HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.LPLED] = new EFEMPcCmd()  { Port = EmEfemPort.LOADPORT2,  Cmd = EmEfemCommand.LPLED,  Name = "LoadPort2_LPLED", SendCmd = OnLPLEDCmd, RecvAck = OnAck, RecvComplete = OnComplete,      CommandFailAlarm = EM_AL_LST.AL_0897_EFEM_LPM2_LPLED_CMD_FAIL,     SignalTimeover = EM_AL_LST.AL_0707_LPM2_LPLED_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0757_LPM2_LPLED_COMPLETE_TIMEOVER };
                                                                                                                    
            HS[EmEfemPort.ALIGNER] = new EFEMPortProxy(EmEfemPort.ALIGNER);                                         
            if(GG.IsDitPreAligner)
            {
                HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.STAT_] = new EFEMPcCmd() { Port = EmEfemPort.ALIGNER, Cmd = EmEfemCommand.STAT_, Name = "Aligner_STAT_",        SendCmd = OnSTAT_Cmd_Aligner, RecvAck = OnAck_Aligner, RecvComplete = OnSTAT_Complete_Aligner, CommandFailAlarm = EM_AL_LST.AL_0930_ALIGNER_STATUS_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0710_ALIGNER_STATUS_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0760_ALIGNER_STATUS_COMPLETE_TIMEOVER, UseHandShakeLog = false };
                HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.INIT_] = new EFEMPcCmd() { Port = EmEfemPort.ALIGNER, Cmd = EmEfemCommand.INIT_, Name = "Aligner_INIT_",        SendCmd = OnINIT_Cmd_Aligner, RecvAck = OnAck_Aligner, RecvComplete = OnINIT_Complete_Aligner, CommandFailAlarm = EM_AL_LST.AL_0932_ALIGNER_INITAIL_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0711_ALIGNER_INIT_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0761_ALIGNER_INIT_COMPLETE_TIMEOVER };
                HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.ALIGN] = new EFEMPcCmd() { Port = EmEfemPort.ALIGNER, Cmd = EmEfemCommand.ALIGN, Name = "Aligner_ALIGN",        SendCmd = OnALIGNCmd_Aligner, RecvAck = OnAck_Aligner, RecvComplete = OnALIGNComplete_Aligner, CommandFailAlarm = EM_AL_LST.AL_0931_ALIGNER_ALIGN_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0713_ALIGNER_ALIGN_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0763_ALIGNER_ALIGN_COMPLETE_TIMEOVER };
                HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.RESET] = new EFEMPcCmd() { Port = EmEfemPort.ALIGNER, Cmd = EmEfemCommand.RESET, Name = "Aligner_RESET",        SendCmd = OnRESETCmd_Aligner, RecvAck = OnAck_Aligner, RecvComplete = OnRESETComplete_Aligner, CommandFailAlarm = EM_AL_LST.AL_0933_ALIGNER_RESET_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0712_ALIGNER_RESET_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0762_ALIGNER_RESET_COMPLETE_TIMEOVER };
                HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.PARDY] = new EFEMPcCmd() { Port = EmEfemPort.ALIGNER, Cmd = EmEfemCommand.PARDY, Name = "Aligner_Recv_READY",   SendCmd = OnPARDYCmd_Aligner, RecvAck = OnAck_Aligner, RecvComplete = OnPARDYComplete_Aligner, CommandFailAlarm = EM_AL_LST.AL_0934_ALIGNER_PARDY_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0714_ALIGNER_RECV_READY_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0764_ALIGNER_RECV_READY_COMPLETE_TIMEOVER };
                HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.PATRR] = new EFEMPcCmd() { Port = EmEfemPort.ALIGNER, Cmd = EmEfemCommand.PATRR, Name = "Aligner_Rotation",     SendCmd = OnPATRRCmd_Aligner, RecvAck = OnAck_Aligner, RecvComplete = OnPATRRComplete_Aligner, CommandFailAlarm = EM_AL_LST.AL_0935_ALIGNER_PATRR_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0715_ALIGNER_ROTATION_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0765_ALIGNER_ROTATION_COMPLETE_TIMEOVER };
                HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.PASRD] = new EFEMPcCmd() { Port = EmEfemPort.ALIGNER, Cmd = EmEfemCommand.PASRD, Name = "Aligner_Send_READY",   SendCmd = OnPASRDCmd_Aligner, RecvAck = OnAck_Aligner, RecvComplete = OnPASRDComplete_Aligner, CommandFailAlarm = EM_AL_LST.AL_0936_ALIGNER_PASRD_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0716_ALIGNER_SEND_READY_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0766_ALIGNER_SEND_READY_COMPLETE_TIMEOVER };
            }
            else
            {
                HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.STAT_] = new EFEMPcCmd() { Port = EmEfemPort.ALIGNER, Cmd = EmEfemCommand.STAT_, Name = "Aligner_STAT_",        SendCmd = OnSTAT_Cmd, RecvAck = OnAck, RecvComplete = OnSTAT_Complete, CommandFailAlarm = EM_AL_LST.AL_0930_ALIGNER_STATUS_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0710_ALIGNER_STATUS_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0760_ALIGNER_STATUS_COMPLETE_TIMEOVER, UseHandShakeLog = false };
                HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.INIT_] = new EFEMPcCmd() { Port = EmEfemPort.ALIGNER, Cmd = EmEfemCommand.INIT_, Name = "Aligner_INIT_",        SendCmd = OnINIT_Cmd, RecvAck = OnAck, RecvComplete = OnComplete, CommandFailAlarm = EM_AL_LST.AL_0932_ALIGNER_INITAIL_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0711_ALIGNER_INIT_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0761_ALIGNER_INIT_COMPLETE_TIMEOVER };
                HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.ALIGN] = new EFEMPcCmd() { Port = EmEfemPort.ALIGNER, Cmd = EmEfemCommand.ALIGN, Name = "Aligner_ALIGN",        SendCmd = OnALIGNCmd, RecvAck = OnAck, RecvComplete = OnALIGNComplete, CommandFailAlarm = EM_AL_LST.AL_0931_ALIGNER_ALIGN_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0713_ALIGNER_ALIGN_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0763_ALIGNER_ALIGN_COMPLETE_TIMEOVER};
                HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.RESET] = new EFEMPcCmd() { Port = EmEfemPort.ALIGNER, Cmd = EmEfemCommand.RESET, Name = "Aligner_RESET",        SendCmd = OnRESETCmd, RecvAck = OnAck, RecvComplete = OnComplete, CommandFailAlarm = EM_AL_LST.AL_0933_ALIGNER_RESET_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0712_ALIGNER_RESET_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0762_ALIGNER_RESET_COMPLETE_TIMEOVER };
                HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.PARDY] = new EFEMPcCmd() { Port = EmEfemPort.ALIGNER, Cmd = EmEfemCommand.PARDY, Name = "Aligner_Recv_READY",   SendCmd = OnPARDYCmd, RecvAck = OnAck, RecvComplete = OnComplete, CommandFailAlarm = EM_AL_LST.AL_0934_ALIGNER_PARDY_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0714_ALIGNER_RECV_READY_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0764_ALIGNER_RECV_READY_COMPLETE_TIMEOVER };
                HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.PATRR] = new EFEMPcCmd() { Port = EmEfemPort.ALIGNER, Cmd = EmEfemCommand.PATRR, Name = "Aligner_Rotation",     SendCmd = OnPATRRCmd, RecvAck = OnAck, RecvComplete = OnComplete, CommandFailAlarm = EM_AL_LST.AL_0935_ALIGNER_PATRR_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0715_ALIGNER_ROTATION_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0765_ALIGNER_ROTATION_COMPLETE_TIMEOVER };
                HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.PASRD] = new EFEMPcCmd() { Port = EmEfemPort.ALIGNER, Cmd = EmEfemCommand.PASRD, Name = "Aligner_Send_READY",   SendCmd = OnPASRDCmd, RecvAck = OnAck, RecvComplete = OnComplete, CommandFailAlarm = EM_AL_LST.AL_0936_ALIGNER_PASRD_CMD_FAIL, SignalTimeover = EM_AL_LST.AL_0716_ALIGNER_SEND_READY_COMMAND_TIMEOVER, CompleteTimeover = EM_AL_LST.AL_0766_ALIGNER_SEND_READY_COMPLETE_TIMEOVER };
            }
            #endregion

            #region event
            HS[EmEfemPort.LOADPORT1].LstEvent[EmEfemCommand.LPMSG] = new EFEMPcEvent() { Port = EmEfemPort.LOADPORT1, Cmd = EmEfemCommand.LPMSG, Name = "LOADPORT1_LPMSG_COMP", RecvComplete = OnLPMSGComplete };
            HS[EmEfemPort.LOADPORT2].LstEvent[EmEfemCommand.LPMSG] = new EFEMPcEvent() { Port = EmEfemPort.LOADPORT2, Cmd = EmEfemCommand.LPMSG, Name = "LOADPORT2_LPMSG_COMP", RecvComplete = OnLPMSGComplete };
            #endregion
            return true;
        }
        private PlcTimerEx _tcpReconnectDelay = new PlcTimerEx("EFEM TCP COMM Reconnect Delay");
        public override void LogicWorking(Equipment equip)
        {
            if (IsConnected == true)
                HSLogicWorking(equip);
            else if (GG.IsDitPreAligner && equip.IsAutoOnlyAligner)
                HSLogicWorking(equip);
            else
            {
                ResetHS();
                if (GG.EfemNoUse == false)
                {
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0785_EFEM_PC_ALIVE_ERROR);
                    if (_tcpReconnectDelay.IsStart == false)
                    {
                        GG.Equip.EFEMTcpReconnect();
                        _tcpReconnectDelay.Start(GG.Equip.CtrlSetting.Ctrl.EFEMReconnectCycleTime);
                    }
                    else if (_tcpReconnectDelay)
                    {
                        _tcpReconnectDelay.Stop();
                    }
                }
            }
        }

        public void ResetHS()
        {
            var ports = new List<EmEfemPort>(HS.Keys);
            foreach (EmEfemPort port in ports)
            {
                var cmds = new List<EmEfemCommand>(HS[port].LstCmd.Keys);
                foreach (EmEfemCommand cmd in cmds)
                {
                    HS[port].LstCmd[cmd].ResetStep();
                    HS[port].SetMoving(cmd, false);
                }

                var evts = new List<EmEfemCommand>(HS[port].LstEvent.Keys);
                foreach (EmEfemCommand evt in evts)
                {
                    HS[port].LstEvent[evt].ResetStep();
                }
            }
        }

        private void HSLogicWorking(Equipment equip)
        {
            var ports = new List<EmEfemPort>(HS.Keys);
            foreach(EmEfemPort port in ports)
            {
                if(GG.IsDitPreAligner == true && port == EmEfemPort.ALIGNER)
                {

                }
                else if (GG.EfemNoUse == true && port != EmEfemPort.ROBOT)
                    continue;

                var cmds = new List<EmEfemCommand>(HS[port].LstCmd.Keys);
                foreach (EmEfemCommand cmd in cmds)
                {
                    if (GG.IsDitPreAligner == true && port == EmEfemPort.ALIGNER)
                    {

                    }
                    else if (GG.EfemNoUse == true && cmd != EmEfemCommand.INIT_)
                        continue;                                              

                    HS[port].LstCmd[cmd].LogicWorking(equip);
                }

                var evts = new List<EmEfemCommand>(HS[port].LstEvent.Keys);
                foreach (EmEfemCommand evt in evts)
                {
                    HS[port].LstEvent[evt].LogicWorking(equip);
                }
            }
        }
        #region HS Command/Ack/Complete
        public bool OnAck(Equipment equip, EFEMPcCmd cmd)
        {
            return EFEMTcp.HS[cmd.Port][cmd.Cmd].IsAckRecved;
        }
        #region OnCmds        
        public void OnSTAT_Cmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMTcp.CmdCheckState(cmd.Port);
        }
        public void OnSIGLMCmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMSIGLMDataSet data = cmd.Tag as EFEMSIGLMDataSet;
            EFEMTcp.CmdSetLampAndBuzzer(data.Red, data.Yellow, data.Green, data.Blue, data.Buzzer1);
        }
        public void OnRESETCmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMTcp.CmdReset(cmd.Port);
        }
        public void OnCHMDACmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMTcp.CmdChangeModeAuto();
        }
        public void OnCHMDMCmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMTcp.CmdChangeModeManual();
        }
        public void OnINIT_Cmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMTcp.CmdInit(cmd.Port);
            if (cmd.Port == EmEfemPort.ROBOT)
            {
                equip.Efem.LastRobotPort = EmEfemPort.ROBOT;
            }
        }
        public void OnPAUSECmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMTcp.CmdPause();
        }
        public void OnRESUMCmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMTcp.CmdResume();
        }
        public void OnSTOP_Cmd(Equipment equip, EFEMPcCmd cmd)
        {            
            EFEMTcp.CmdStop(cmd.Port);
        }
        public void OnWAITRCmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMWAITRDataSet data = cmd.Tag as EFEMWAITRDataSet;
            EFEMTcp.CmdWaitTransfer(data.Arm, data.TargetPort, data.Slot);
            equip.Efem.LastRobotPort = data.TargetPort;
            equip.Efem.LastRobotForward = false;
        }
        public void OnTRANSCmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMTRANSDataSet data = cmd.Tag as EFEMTRANSDataSet;
            EFEMTcp.CmdTransferWafer(data.Arm, data.Transfer, data.TargetPort, data.Slot);
            equip.Efem.LastRobotPort = data.TargetPort;
            equip.Efem.LastRobotForward = true;
            
            equip.MoveRobotArm(data.Arm, data.TargetPort);

            equip.Efem.IsPickingSlot1LPM1 = false;
            equip.Efem.IsPickingSlot1LPM2 = false;
            if (data.Transfer == EmEfemTransfer.PICK && data.Slot == 1)
            {
                if (data.TargetPort == EmEfemPort.LOADPORT1)
                {
                    equip.Efem.IsPickingSlot1LPM1 = true;
                    equip.Efem.IsPickingSlot1LPM2 = false;
                }
                else if (data.TargetPort == EmEfemPort.LOADPORT2)
                {
                    equip.Efem.IsPickingSlot1LPM1 = false;
                    equip.Efem.IsPickingSlot1LPM2 = true;
                }
            }                
        }
        public void OnEXTNDCmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMEXTNDDataSet data = cmd.Tag as EFEMEXTNDDataSet;
            //EFEMTcp.CmdExtendArm(data.Arm, data.Transfer, data.TargetPort, data.Slot);
            equip.Efem.LastRobotPort = data.TargetPort;
            equip.Efem.LastRobotForward = true;
        }
        public void OnARMFDCmd(Equipment equip, EFEMPcCmd cmd)
        {
            //EFEMTRANSDataSet data = cmd.Tag as EFEMTRANSDataSet;
            //EFEMTcp.CmdArmFold();
        }
        public void OnCLAMPCmd(Equipment equip, EFEMPcCmd cmd)
        {
            //EFEMTcp.CmdClamp(cmd.Port);
        }
        public void OnUCLAMCmd(Equipment equip, EFEMPcCmd cmd)
        {
            //EFEMTcp.CmdUnClamp(cmd.Port);
        }
        public void OnDOCK_Cmd(Equipment equip, EFEMPcCmd cmd)
        {
            //EFEMTcp.CmdDocking(cmd.Port);
        }
        public void OnUDOCKCmd(Equipment equip, EFEMPcCmd cmd)
        {
            //EFEMTcp.CmdUnDocking(cmd.Port);
        }
        public void OnOPEN_Cmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMTcp.CmdOpenFoup(cmd.Port);
        }
        public void OnCLOSECmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMTcp.CmdCloseFoup(cmd.Port);
        }
        public void OnLOAD_Cmd(Equipment equip, EFEMPcCmd cmd)
        {
            //EFEMTcp.CmdLoadFoup(cmd.Port);
        }
        public void OnULOADCmd(Equipment equip, EFEMPcCmd cmd)
        {
            //EFEMTcp.CmdUnLoadFoup(cmd.Port);
        }
        public void OnMAPP_Cmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMTcp.CmdAskMappingData(cmd.Port);
        }
        public void OnLPLEDCmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMLPLEDDataSet data = cmd.Tag as EFEMLPLEDDataSet;
            EFEMTcp.CmdChangeLedLamp(cmd.Port, data.LampType, data.LampState);
        }
        public void OnALIGNCmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMAlignDataSet data = cmd.Tag as EFEMAlignDataSet;
            EFEMTcp.CmdAlignment(data.NotchDegree, data.OcrForward, data.WaferID);
        }
        public void OnPARDYCmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMTcp.CmdPreAlignerReady();
        }
        public void OnPASRDCmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMTcp.CmdPreAlignerSendReady();
        }
        public void OnPATRRCmd(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMAlignDataSet data = cmd.Tag as EFEMAlignDataSet;
            EFEMTcp.CmdPreAlignerRotation(data.NotchDegree, data.OcrForward);
        }
        #endregion OnCmds
        #region OnCompletes
        public bool OnTransComplete(Equipment equip, EFEMPcCmd cmd)
        {
            if (OnComplete(equip, cmd) == true)
            {
                if (cmd.Port == EmEfemPort.ROBOT && cmd.Cmd == EmEfemCommand.TRANS)
                {
                    equip.Efem.LastRobotForward = false;
                }

                if (GG.TestMode == false)
                {
                    if (GG.Equip.Efem.Proxy.IsRunning(equip, EmEfemPort.ROBOT, EmEfemCommand.STAT_) == false)
                        GG.Equip.Efem.Proxy.StartCommand(equip, EmEfemPort.ROBOT, EmEfemCommand.STAT_);
                }
                #region testonly
                if (GG.TestMode == true || GG.EfemNoWafer == true)
                {
                    EFEMTRANSDataSet data = cmd.Tag as EFEMTRANSDataSet;
                    if (data.Transfer == EmEfemTransfer.PICK)
                    {
                        if (data.Arm == EmEfemRobotArm.Lower)
                            equip.Efem.Robot.Status.IsLowerArmVacOn = true;
                        else
                            equip.Efem.Robot.Status.IsUpperArmVacOn = true;

                        if (data.TargetPort == EmEfemPort.EQUIPMENT)
                        {
                            equip.WaferDetectSensorLiftpin1.XB_OnOff.vBit = false;
                            equip.WaferDetectSensorLiftpin2.XB_OnOff.vBit = false;
                            equip.WaferDetectSensorStage1.XB_OnOff.vBit = false;
                            equip.IsWaferDetect = EmGlassDetect.NOT;
                        }
                        if (data.TargetPort == EmEfemPort.ALIGNER)
                            equip.Efem.Aligner.Status.IsWaferExist = false;
                    }
                    else if (data.Transfer == EmEfemTransfer.PLACE)
                    {
                        if (data.Arm == EmEfemRobotArm.Lower)
                            equip.Efem.Robot.Status.IsLowerArmVacOn = false;
                        else
                            equip.Efem.Robot.Status.IsUpperArmVacOn = false;

                        if (data.TargetPort == EmEfemPort.EQUIPMENT)
                        {
                            equip.IsWaferDetect = EmGlassDetect.SOME;
                            equip.WaferDetectSensorLiftpin1.XB_OnOff.vBit = true;
                            equip.WaferDetectSensorLiftpin2.XB_OnOff.vBit = true;
                            equip.WaferDetectSensorStage1.XB_OnOff.vBit = true;
                        }
                        if (data.TargetPort == EmEfemPort.ALIGNER)
                            equip.Efem.Aligner.Status.IsWaferExist = true;
                    }
                }
                #endregion
                return true;
            }
            else
                return false;
        }
        public void OnTransCompleteSuccess(Equipment equip, EFEMPcCmd cmd)
        {
            EFEMTRANSDataSet data = cmd.Tag as EFEMTRANSDataSet;
            if (data.Transfer == EmEfemTransfer.PICK)
            {
                TransferDataMgr.CopyWaferPickToRobot(equip, data, data.Arm);
            }
            else
            {
                TransferDataMgr.CopyWaferPlaceToPort(equip, data, data.Arm);
            }
        }   

        public bool OnComplete(Equipment equip, EFEMPcCmd cmd)
        {
            return EFEMTcp.HS[cmd.Port][cmd.Cmd].IsCompleteRecved;
        }
        public bool OnSTAT_Complete(Equipment equip, EFEMPcCmd cmd)
        {
            if (EFEMTcp.HS[cmd.Port][cmd.Cmd].IsCompleteRecved == true)
            {
                if (EFEMTcp.HS[cmd.Port][cmd.Cmd].IsSuccess == true && EFEMTcp.HS[cmd.Port][cmd.Cmd].IsDataProcessDone == true)
                {
                    switch (cmd.Port)
                    {
                        case EmEfemPort.ETC: EFEMTcp.EfemStat.CopyTo(ref this.EfemStat); break;
                        case EmEfemPort.ROBOT: EFEMTcp.RobotStat.CopyTo(ref this.RobotStat); break;
                        case EmEfemPort.LOADPORT1: EFEMTcp.LoadPortStat1.CopyTo(ref this.LoadPort1Stat); break;
                        case EmEfemPort.LOADPORT2: EFEMTcp.LoadPortStat2.CopyTo(ref this.LoadPort2Stat); break;
                        case EmEfemPort.ALIGNER: EFEMTcp.AlignerStat.CopyTo(ref this.AlignerStat); break;
                    }
                }

                return true;
            }
            return false;
        }
        public bool OnMAPP_Complete(Equipment equip, EFEMPcCmd cmd)
        {
            if (EFEMTcp.HS[cmd.Port][cmd.Cmd].IsCompleteRecved == true)
            {                
                if (EFEMTcp.HS[cmd.Port][cmd.Cmd].IsSuccess == true && EFEMTcp.HS[cmd.Port][cmd.Cmd].IsDataProcessDone == true)
                {
                    switch (cmd.Port)
                    {
                        case EmEfemPort.LOADPORT1: EFEMTcp.LoadPortStat1.MappingCopyTo(ref this.LoadPort1Stat); break;
                        case EmEfemPort.LOADPORT2: EFEMTcp.LoadPortStat2.MappingCopyTo(ref this.LoadPort2Stat); break;
                    }
                    return true;
                }                
            }
            return false;
        }
        PlcTimerEx AlignCompleteDelay = new PlcTimerEx("Align Delay", false);
        public bool OnALIGNComplete(Equipment equip, EFEMPcCmd cmd)
        {
            if (EFEMTcp.HS[cmd.Port][cmd.Cmd].IsCompleteRecved == true)
            {
                if(EFEMTcp.HS[cmd.Port][cmd.Cmd].IsSuccess == true && EFEMTcp.HS[cmd.Port][cmd.Cmd].IsDataProcessDone == true)
                {
                    EFEMTcp.AlignerStat.AlignDataCopyTo(ref this.AlignerStat);
                    return true;
                }
                else
                {
                    if(AlignCompleteDelay)
                    {
                        Logger.Log.AppendLine("EFEN Align CompleteRecved TimeOver , IsSucess : {0}, IsDataPrecessDone : {1}",
                            EFEMTcp.HS[cmd.Port][cmd.Cmd].IsSuccess, EFEMTcp.HS[cmd.Port][cmd.Cmd].IsDataProcessDone);
                        return true;
                    }
                    else if(AlignCompleteDelay.IsStart == false)
                    {
                        AlignCompleteDelay.Start(3);
                    }
                }

                //else if(EFEMTcp.HS[cmd.Port][cmd.Cmd].ErrorCode == 7)
                //Logger.Log.AppendLine(LogLevel.Error, "Align Data Read FAIL");
            }
            return false;
        }
        public bool OnLPMSGComplete(Equipment equip, EFEMPcEvent evt)
        {
            if (EFEMTcp.HS[evt.Port][EmEfemCommand.LPMSG].IsCompleteRecved == true)
            {
                if (evt.Port == EmEfemPort.LOADPORT1)
                    EFEMTcp.LoadPortStat1.CopyEventDataTo(ref LoadPort1Stat);
                else if (evt.Port == EmEfemPort.LOADPORT2)
                    EFEMTcp.LoadPortStat2.CopyEventDataTo(ref LoadPort2Stat);
                EFEMTcp.HS[evt.Port][EmEfemCommand.LPMSG].ClearComplete();
                return true;
            }
            return false;
        }
        #endregion OnCompletes        
        #endregion HS Command/Ack/Complete

        #region 2호기 Aligner Command/Ack/Complete
        public void OnSTAT_Cmd_Aligner(Equipment equip, EFEMPcCmd cmd)
        {
           
        }
        public void OnALIGNCmd_Aligner(Equipment equip, EFEMPcCmd cmd)
        {
            equip.PreAlignerSeq.AlignStart(equip, cmd);
            //EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorModule = EmEfemErrorModule.E3;
            //EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode = 0;

            //if (GG.TestMode)
            //{
            //    equip.PreAligner.ImageProcessingUpdate();
            //    return;
            //}

            //if (equip.PreAligner.IsCamReady == false)
            //    EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode = 83;
            //if (equip.PreAligner.IsLightReady == false)
            //    EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode = 81;

            //if (EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode > 0)
            //    return;

            //EFEMAlignDataSet data = cmd.Tag as EFEMAlignDataSet;
            //PreAlignerRecipe recp = null;
            //if (equip.UseFixedDitAlignerRecipe)
            //    recp = PreAlignerRecipeDataMgr.GetRecipe(equip.FixedDitAlignerRecipeName);
            //else
            //    recp = PreAlignerRecipeDataMgr.GetRecipe(data.WaferID);

            //if (recp == null || recp.Name == string.Empty)
            //{
            //    EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode = 7;
            //}
            //else
            //{
            //    _isPreAlignProcessingDone = false;
            //    equip.PreAligner.SetCurRecipe(recp);
            //    equip.PreAligner.Start(data.WaferID);
            //}
        }
        public void OnINIT_Cmd_Aligner(Equipment equip, EFEMPcCmd cmd)
        {
            cmd.SetCompleteTimeout(600);
            equip.PreAlignerSeq.InitStart(equip);
        }
        public void OnRESETCmd_Aligner(Equipment equip, EFEMPcCmd cmd)
        {
            //error 발생되어 있으면 reset     

            equip.PreAligner.Stop();
            equip.PreAligner.StopLive();
            equip.PreAligner.LightController.Off();
        }
        public void OnPARDYCmd_Aligner(Equipment equip, EFEMPcCmd cmd)
        {
            //ocr 실린더 상승 명령, vacuun off 명령, 
            equip.AlignerOcrCylinder.Up();
            equip.AlignerVac.StartOffStep();

            //모든 축 로딩위치(로봇 받을 준비)로 이동
            equip.AlignerX.MovePosition(AlignerXEzi.LoadingPos);
            equip.AlignerY.MovePosition(AlignerYEzi.LoadingPos);
            equip.AlignerT.MovePosition(AlignerThetaEzi.LoadingPos);
        }
        public void OnPATRRCmd_Aligner(Equipment equip, EFEMPcCmd cmd)
        {
            //1호기는 여기서 Use Ocr을 통해 실린더 하강 여부 결정 의외 처리 x
            EFEMAlignDataSet data = (EFEMAlignDataSet)cmd.Tag;

            if (data.OcrForward)
                equip.AlignerOcrCylinder.Down();
            equip.AlignerVac.VacuumOn();

            equip.AlignerX.MovePosition(AlignerXEzi.LoadingPos);
            equip.AlignerY.MovePosition(AlignerYEzi.LoadingPos);
            equip.AlignerT.MovePosition(AlignerThetaEzi.LoadingPos);
        }
        public void OnPASRDCmd_Aligner(Equipment equip, EFEMPcCmd cmd)
        {
            equip.AlignerVac.StartOffStep();
            equip.AlignerOcrCylinder.Up();
            //얼라이너 배출 위치로 모터 이동
            equip.AlignerX.MovePosition(AlignerXEzi.UnloadingPos);
            equip.AlignerY.MovePosition(AlignerYEzi.UnloadingPos);
            equip.AlignerT.MovePosition(AlignerThetaEzi.UnloadingPos);
        }
        public bool OnAck_Aligner(Equipment equip, EFEMPcCmd cmd)
        {
            //Ack 처리는 항상 true(기존 EFEM Proxy 구조는 Command, Ack수신, Complete 수신이므로)
            return true;
        }
        public bool OnSTAT_Complete_Aligner(Equipment equip, EFEMPcCmd cmd)
        {
            
            return true;
        }
        private bool _isPreAlignProcessingDone = false;
        public bool OnALIGNComplete_Aligner(Equipment equip, EFEMPcCmd cmd)
        {
            if(equip.PreAlignerSeq.IsAlignComplete)
            {
                return true;
            }
            else
            {
                return false;
            }
            //if (_isPreAlignProcessingDone == true)
            //{
            //    if (equip.AlignerVac.OffStepSync())
            //        return true;
            //    return false;
            //}

            //EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorModule = EmEfemErrorModule.E3;
            //EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].IsSuccess = false;
            //if (EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode > 0)
            //{
            //    Logger.Log.AppendLine(LogLevel.Error, "PreAligner 알람발생 : {0}",
            //        EFEMError.GetErrorDesc(EmEfemErrorModule.E3, EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode));
            //    _isPreAlignProcessingDone = true;
            //}
            //else
            //{
            //    if (equip.PreAligner.IsProcessingDone)
            //    {
            //        if (equip.PreAligner.IsProcessingOvertime)
            //        {
            //            if (equip.PreAligner.IsLightControllDone == false)
            //                EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode = 84;
            //            else
            //                EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode = 83;
            //            Logger.Log.AppendLine(LogLevel.Error, "PreAligner 알람발생 : {0}",
            //                EFEMError.GetErrorDesc(EmEfemErrorModule.E3, EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode));
            //            _isPreAlignProcessingDone = true;
            //        }
            //        else
            //        {
            //            if (equip.PreAligner.ProcessingResult == WaferPreAlignerResultCode.WaferPreAlignerResult_Success)
            //            {
            //                AlignerStat.SetDitAlignResult(equip.PreAligner.AlignParamResult);
            //                EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].IsSuccess = true;
            //                Logger.Log.AppendLine(LogLevel.Info, "PreAligner 처리 성공 (미가공Data) |02~11| {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}",
            //                    equip.PreAligner.AlignParamResult.DX,
            //                    equip.PreAligner.AlignParamResult.DY,
            //                    equip.PreAligner.AlignParamResult.DTheta,
            //                    equip.PreAligner.AlignParamResult.CenterX,
            //                    equip.PreAligner.AlignParamResult.CenterY,
            //                    equip.PreAligner.AlignParamResult.MajorLength,
            //                    equip.PreAligner.AlignParamResult.MinorLength,
            //                    equip.PreAligner.AlignParamResult.NotchX,
            //                    equip.PreAligner.AlignParamResult.NotchY,
            //                    equip.PreAligner.AlignParamResult.NotchDegree
            //                    );
            //                Logger.Log.AppendLine(LogLevel.Info, "PreAligner 처리 성공 (가공Data) |02~11| {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}",
            //                    AlignerStat.OffsetX,
            //                    AlignerStat.OffsetY,
            //                    AlignerStat.OffsetT,
            //                    AlignerStat.EllipseX,
            //                    AlignerStat.EllipseY,
            //                    AlignerStat.MajorLength,
            //                    AlignerStat.MinorLength,
            //                    AlignerStat.WaferNotchPosX,
            //                    AlignerStat.WaferNotchPosY,
            //                    AlignerStat.WaferNotchPosT
            //                    );
            //            }
            //            else
            //            {
            //                EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode = 208 + (int)equip.PreAligner.ProcessingResult;
            //                Logger.Log.AppendLine(LogLevel.Error, "PreAligner 처리 실패 : {0}",
            //                    EFEMError.GetErrorDesc(EmEfemErrorModule.E3, EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode));
            //            }

            //            if (equip.PreAligner.CurRecipe.SaveResult)
            //            {
            //                equip.PreAligner.SaveResultImage(
            //                    equip.PreAligner.ProcessingResult == WaferPreAlignerResultCode.WaferPreAlignerResult_Success,
            //                    equip.PreAligner.WaferID);
            //            }
            //            _isPreAlignProcessingDone = true;
            //        }
            //    }
            //    else
            //        _isPreAlignProcessingDone = false;
            //}
            //return false;
        }


        public bool OnINIT_Complete_Aligner(Equipment equip, EFEMPcCmd cmd)
        {
            if(equip.PreAlignerSeq.IsInitComplete)
            {
                EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.INIT_].IsSuccess = true;
                return true;
            }
            else
            {
                EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.INIT_].IsSuccess = false;
                return false;
            }
            ////모든 축 서보온, home complete, 모터 Stop, 원점위치, error off 확인 후 return true
            //bool result = true;
            //foreach (var motor in equip.StepMotors)
            //{
            //    result &= motor.IsServoOnBit && motor.IsHomeCompleteBit && motor.IsHomming == false && motor.IsMoving == false;
            //}
            //result &= equip.AlignerX.IsMoveOnPosition(AlignerXEzi.LoadingPos);
            //result &= equip.AlignerY.IsMoveOnPosition(AlignerYEzi.LoadingPos);
            //result &= equip.AlignerT.IsMoveOnPosition(AlignerThetaEzi.LoadingPos);

            //result &= equip.AlignerOcrCylinder.IsUp;

            //if(result)
            //{
            //    EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.INIT_].IsSuccess = true;
            //    return true;
            //}
            //else
            //{
            //    EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.INIT_].IsSuccess = false;
            //    return false;
            //}
        }
        public bool OnRESETComplete_Aligner(Equipment equip, EFEMPcCmd cmd)
        {
            //Reset은 따로 해줄 처리가 없으니 바로 true 반환
            EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.RESET].IsSuccess = true;
            return true;
        }
        public bool OnPARDYComplete_Aligner(Equipment equip, EFEMPcCmd cmd)
        {
            //ocr 실린더 상승, vacuum off, 모든 축 ld위치 확인 후 true반환
            if (equip.AlignerX.IsMoveOnPosition(AlignerXEzi.LoadingPos) 
                && equip.AlignerY.IsMoveOnPosition(AlignerYEzi.LoadingPos) 
                && equip.AlignerT.IsMoveOnPosition(AlignerThetaEzi.LoadingPos)
                && equip.AlignerOcrCylinder.IsUp && equip.AlignerVac.IsVacuumOff)
            {
                EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.PARDY].IsSuccess = true;
                return true;
            }
            else
            {
                EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.PARDY].IsSuccess = false;
                return false;
            }
        }
        public bool OnPATRRComplete_Aligner(Equipment equip, EFEMPcCmd cmd)
        {
            if (equip.AlignerX.IsMoveOnPosition(AlignerXEzi.LoadingPos)
                && equip.AlignerY.IsMoveOnPosition(AlignerYEzi.LoadingPos)
                && equip.AlignerT.IsMoveOnPosition(AlignerThetaEzi.LoadingPos)
                && (GG.Equip.CtrlSetting.UseOCR ? equip.AlignerOcrCylinder.IsDown : true )&& equip.AlignerVac.IsVacuumOn)
            {
                EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.PATRR].IsSuccess = true;
                return true;
            }
            else
            {
                EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.PATRR].IsSuccess = false;
                return false;
            }
        }
        public bool OnPASRDComplete_Aligner(Equipment equip, EFEMPcCmd cmd)
        {
            //로봇 배출 위치 이동 완료 확인
            if (equip.AlignerX.IsMoveOnPosition(AlignerXEzi.UnloadingPos) && 
                equip.AlignerY.IsMoveOnPosition(AlignerYEzi.UnloadingPos) && 
                equip.AlignerT.IsMoveOnPosition(AlignerThetaEzi.UnloadingPos) &&
                equip.AlignerVac.IsVacuumOff && equip.AlignerOcrCylinder.IsUp)
            {
                EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.PASRD].IsSuccess = true;
                return true;
            }
            else
            {
                EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.PASRD].IsSuccess = false;
                return false;
            }
        }

        #endregion
        public bool StartLampBuzzerChange(Equipment equip, EFEMSIGLMDataSet data)
        {
            if(GG.TestMode == true)
            {
                return true;
            }
            return StartCmd(equip, EmEfemPort.ETC, EmEfemCommand.SIGLM, data);
        }
        public bool StartWaitRobot(Equipment equip, EFEMWAITRDataSet data)
        {
            if (IsEquipInterlock("ROBOT", EmEfemCommand.WAITR) == false)
                return false;

            return StartCmd(equip, EmEfemPort.ROBOT, EmEfemCommand.WAITR, data);
        }
        ///// <summary>
        ///// 미사용
        ///// </summary>
        ///// <param name="equip"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public bool StartExtndRobot(Equipment equip, EFEMEXTNDDataSet data)
        //{
        //    return false;//미사용 StartCmd(equip, EmEfemPort.ROBOT, EmEfemCommand.EXTND, data);
        //}
        public bool StartTransRobot(Equipment equip, EFEMTRANSDataSet data)
        {
            if (IsOkStartTransRobotInterlock(equip, data) == false)
                return false;

            if (data.TargetPort == EmEfemPort.ALIGNER
                || data.TargetPort == EmEfemPort.EQUIPMENT)
                data.Slot = 1;

            if(data.TargetPort == EmEfemPort.LOADPORT1 || data.TargetPort == EmEfemPort.LOADPORT2
                && data.Slot == 1)
            {
                HS[EmEfemPort.ROBOT].IsFirstSlot = true;
            }
            else
                HS[EmEfemPort.ROBOT].IsFirstSlot = false;


            return StartCmd(equip, EmEfemPort.ROBOT, EmEfemCommand.TRANS, data);
        }
        public bool IsOkStartTransRobotInterlock(Equipment equip, EFEMTRANSDataSet data)
        {
            if (equip.Efem.Robot.Status.IsMoving == true)
            {
                InterLockMgr.AddInterLock("Robot Moving 상태 입니다 이동불가");
                equip.IsInterlock = true;
                return false;
            }
            else if (data.Transfer == EmEfemTransfer.PICK)
            {
                // 공통
                if (data.Arm == EmEfemRobotArm.Upper ? equip.Efem.Robot.Status.IsUpperArmVacOn : equip.Efem.Robot.Status.IsLowerArmVacOn)
                {
                    InterLockMgr.AddInterLock("Robot에 Wafer가 존재할 때 PICK 명령 금지");
                    equip.IsInterlock = true;
                    return false;
                }

                if (data.TargetPort == EmEfemPort.ALIGNER)
                {
                    if (equip.Efem.Aligner.Status.IsWaferExist == false)
                    {
                        InterLockMgr.AddInterLock("Aligner에 Wafer가 존재하지 않을 때 PICK 명령 금지");
                        equip.IsInterlock = true;
                        return false;
                    }
                    if(equip.Efem.Aligner.IsInitDone == false)
                    {
                        InterLockMgr.AddInterLock("Aligner가 이니셜이 완료되지 않았을 때 PICK 명령 금지, 프리얼라이너 이니셜 먼저 진행");
                        equip.IsInterlock = true;
                        return false;
                    }
                }
                else if (data.TargetPort == EmEfemPort.EQUIPMENT)
                {
                    if (equip.IsWaferDetect == EmGlassDetect.NOT)
                    {
                        InterLockMgr.AddInterLock("AVI에 Wafer가 존재 하지 않을 때 PICK 명령 금지");
                        equip.IsInterlock = true;
                        return false;
                    }
                }
            }
            else if (data.Transfer == EmEfemTransfer.PLACE)
            {
                // 공통
                if (data.Arm == EmEfemRobotArm.Upper ? equip.Efem.Robot.Status.IsUpperArmVacOn == false : equip.Efem.Robot.Status.IsLowerArmVacOn == false)
                {
                    InterLockMgr.AddInterLock("Robot에 Wafer가 없을 때 PLACE 명령 금지");
                    equip.IsInterlock = true;
                    return false;
                }

                if (data.TargetPort == EmEfemPort.ALIGNER && equip.Efem.Aligner.Status.IsWaferExist == true)
                {
                    InterLockMgr.AddInterLock("Aligner에 Wafer가 존재할 때 PLACE 명령 금지");
                    equip.IsInterlock = true;
                    return false;
                }
                else if (data.TargetPort == EmEfemPort.EQUIPMENT && equip.IsWaferDetect != EmGlassDetect.NOT)
                {
                    InterLockMgr.AddInterLock("AVI에 Wafer가 존재할 때 PLACE 명령 금지");
                    equip.IsInterlock = true;
                    return false;
                }

                if(data.TargetPort == EmEfemPort.ALIGNER && equip.Efem.Aligner.IsInitDone == false)
                {
                    if (equip.Efem.Aligner.IsInitDone == false)
                    {
                        InterLockMgr.AddInterLock("Aligner가 이니셜이 완료되지 않았을 때 PLACE 명령 금지, 프리얼라이너 이니셜 먼저 진행");
                        equip.IsInterlock = true;
                        return false;
                    }
                }
            }
            else if (data.TargetPort == EmEfemPort.LOADPORT1 && GG.Equip.Efem.LoadPort1.Status.IsDoorClose == true)
            {
                InterLockMgr.AddInterLock(string.Format("LOADPORT1 CLOSE 상태에서 {0} 불가", data.Transfer));
                equip.IsInterlock = true;
                return false;
            }
            else if (data.TargetPort == EmEfemPort.LOADPORT2 && GG.Equip.Efem.LoadPort2.Status.IsDoorClose == true)
            {
                InterLockMgr.AddInterLock(string.Format("LOADPORT2 CLOSE 상태에서 {0} 불가", data.Transfer));
                equip.IsInterlock = true;
                return false;
            }

            if (IsEquipInterlock("ROBOT", EmEfemCommand.TRANS) == false)
                return false;

            return true;
        }
        public bool StartLPMLedChange(Equipment equip, EmEfemPort port, EFEMLPLEDDataSet data)
        {
            return StartCmd(equip, port, EmEfemCommand.LPLED, data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="equip"></param>
        /// <param name="notchDegree">상위정보상에 Notch방향 설정 (해당 방향 근처에 없으면 알람)</param>
        /// <param name="ocrForward">ALIGN 완료 후 OCR 실린더를 전진할지 여부</param>
        /// <returns></returns>
        public bool StartAlign(Equipment equip, int notchDegree, bool ocrForward, string waferID)
        {
            return StartCmd(equip, EmEfemPort.ALIGNER, EmEfemCommand.ALIGN, new EFEMAlignDataSet() { WaferID = waferID, NotchDegree = notchDegree, OcrForward = ocrForward });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="equip"></param>
        /// <param name="degree">상위정보상에 Notch방향 설정 (해당 방향 근처에 없으면 알람)</param>
        /// <param name="ocrForward">ALIGN 완료 후 OCR 실린더를 전진할지 여부</param>
        /// <returns></returns>
        public bool StartAlignerRotation(Equipment equip, int degree, bool ocrForward)
        {
            if (degree < 0)
                degree += 360;

            if (0 > degree || 360 <= degree)
            {
                InterLockMgr.AddInterLock("PreAligner 회전은 0~359도만 가능합니다");
                return false;
            }

            return StartCmd(equip, EmEfemPort.ALIGNER, EmEfemCommand.PATRR, new EFEMAlignDataSet() { NotchDegree = degree, OcrForward = ocrForward });
        }
        public bool StartCommand(Equipment equip, EmEfemPort port, EmEfemCommand cmd)
        {
            return StartCmd(equip, port, cmd, null);
        }
        private bool StartCmd(Equipment equip, EmEfemPort port, EmEfemCommand cmd, object param)
        {
            if (IsConnected == false && equip.IsAutoOnlyAligner == false)
            {
                InterLockMgr.AddInterLock("EFEM 연결 상태가 아닌데 명령이 들어왔습니다.","({0}:{1})", port, cmd);
                return false;
            }
            if (GG.EfemNoUse == true && (port == EmEfemPort.ROBOT && cmd == EmEfemCommand.INIT_) == false && equip.IsAutoOnlyAligner == false)
            {
                InterLockMgr.AddInterLock("EFEM 미사용 모드 상태입니다, 로봇 Initialize만 가능합니다");
                return false;
            }
            if (HS.ContainsKey(port) == false)
            {
                InterLockMgr.AddInterLock("없는 Port입니다", "({0}:{1})", port, cmd);
                return false;
            }
            if (HS[port].HasCmd(cmd) == false)
            {
                InterLockMgr.AddInterLock("해당 Port에 지원하지 않는 명령입니다.", "({0}:{1})", port, cmd);
                return false;
            }
            if (HS[port].IsStepRunning(cmd) == true)
            {
                InterLockMgr.AddInterLock("해당 명령이 진행 중입니다.", "({0}:{1})", port, cmd);
                return false;
            }
            if (HS[port].IsMovingCommand(cmd) == true
                && HS[port].IsProcessingMovingCmd == true && HS[port].LastStatusCmd != EmEfemCommand.STAT_)
            {
                InterLockMgr.AddInterLock("해당 Port가 동작중입니다.", "({0}: 마지막 동작Cmd:{1}, 마지막 기타Cmd:{2})", port, HS[port].LastActionCmd, HS[port].LastStatusCmd);
                return false;
            }
            StartCmdLogging(port, cmd, param);
            _lastCmd = cmd;
            HS[port].SetMoving(cmd, true);
            if (HS[port].IsActionCommand(port, cmd) == true)
                HS[port].LastActionCmd = cmd;
            else
                HS[port].LastStatusCmd = cmd;
            HS[port].LstCmd[cmd].Tag = param;
            HS[port].LstCmd[cmd].Step = 10;
            return true;
        }

        private void StartCmdLogging(EmEfemPort port, EmEfemCommand cmd, object param)
        {
            if (port == EmEfemPort.ROBOT)
            {
                if (cmd == EmEfemCommand.TRANS)
                {
                    EFEMTRANSDataSet trData = param as EFEMTRANSDataSet;
                    Logger.RobotLog.AppendLine(LogLevel.NoLog, string.Format("TRANS : {0} -> {1}.{2} ({3})", trData.Arm.ToString(), trData.TargetPort.ToString(), trData.Slot, trData.Transfer.ToString()));                    
                }
                else if (cmd == EmEfemCommand.WAITR)
                {
                    EFEMWAITRDataSet wtData = param as EFEMWAITRDataSet;
                    Logger.RobotLog.AppendLine(LogLevel.NoLog, string.Format("WAITR : {0}.{1}", wtData.Arm.ToString(), wtData.TargetPort.ToString()));
                }
                else if (cmd != EmEfemCommand.STAT_)
                    Logger.RobotLog.AppendLine(LogLevel.NoLog, string.Format("{0}", cmd));
            }
        }

        public bool IsRunning(Equipment equip, EmEfemPort port, EmEfemCommand cmd)
        {
            return HS[port].IsStepRunning(cmd) == true;
        }
        public bool IsComplete(Equipment equip, EmEfemPort port, EmEfemCommand cmd)
        {
            return HS[port].IsStepRunning(cmd) == false && EFEMTcp.HS[port][cmd].IsCompleteRecved;
        }
        public bool IsSuccess(Equipment equip, EmEfemPort port, EmEfemCommand cmd)
        {
            if (GG.PreAlignerType == PreAlignerTypes.DIT
                && port == EmEfemPort.ALIGNER)
            {
                if (cmd == EmEfemCommand.ALIGN)
                {
                    return equip.PreAligner.IsProcessingSuccess;
                }
            }

            return EFEMTcp.HS[port][cmd].IsSuccess;
        }
        public bool IsDataProcessDone(Equipment equip, EmEfemPort port, EmEfemCommand cmd)
        {
            if (GG.PreAlignerType == PreAlignerTypes.DIT
              && port == EmEfemPort.ALIGNER)
            {
                if (cmd == EmEfemCommand.ALIGN)
                {
                    return equip.PreAligner.IsProcessingDone;
                }
            }

            return EFEMTcp.HS[port][cmd].IsDataProcessDone;
        }

        public bool IsEquipInterlock(string name, EmEfemCommand action)
        {
            if (GG.Equip.IsDoorOpen && GG.Equip.IsUseInterLockOff == false && GG.Equip.IsUseDoorInterLockOff == false /*&& GG.Equip.IsEnableGripSwOn == EmGrapSw.MIDDLE_ON*/)
            {
                InterLockMgr.AddInterLock(string.Format("인터락<DOOR>\n(설비 상태가 DOOR OPEN 상태에서 {0}-{1} 금지됩니다.)", name, action));
                return false;
            }

            if (GG.Equip.IsImmediatStop)
            {
                InterLockMgr.AddInterLock(string.Format("인터락<EMERGENCY>\n(설비 상태가 PAUSE or EMERGENCY 상태에서 {0}-{1} 이동이 금지됩니다.)", name, action));
                return false;
            }
            return true;
        }
    }
    #region DataSet
    public class EFEMSIGLMDataSet
    {
        public EmEfemLampBuzzerState Red { get; set; }
        public EmEfemLampBuzzerState Yellow { get; set; }
        public EmEfemLampBuzzerState Green { get; set; }
        public EmEfemLampBuzzerState Blue { get; set; }
        public EmEfemLampBuzzerState Buzzer1 { get; set; }
        private EmEfemLampBuzzerState Buzzer2{ get; set; }

        public EFEMSIGLMDataSet(
            EmEfemLampBuzzerState red,
            EmEfemLampBuzzerState yellow,
            EmEfemLampBuzzerState green,
            EmEfemLampBuzzerState blue,
            EmEfemLampBuzzerState buzzer1
            )
        {
            Red     = red;
            Yellow  = yellow;
            Green   = green;
            Blue    = blue;
            Buzzer1 = buzzer1;
            Buzzer2 = EmEfemLampBuzzerState.DISABLE;
        }
    }
    public class EFEMWAITRDataSet
    {
        public EmEfemRobotArm Arm { get; set; }
        public EmEfemPort TargetPort { get; set; }
        public int Slot { get; set; }

        public EFEMWAITRDataSet(
            EmEfemRobotArm arm,
            EmEfemPort targetPort,
            int slot
            )
        {
            Arm = arm;
            TargetPort = targetPort;
            Slot = slot;
        }
    }
    public class EFEMEXTNDDataSet : EFEMTRANSDataSet
    {
        public EFEMEXTNDDataSet(
          EmEfemRobotArm arm,
          EmEfemTransfer transfer,
          EmEfemPort targetPort
          ) : base(arm, transfer, targetPort, 1)
        {
        }
    }
    public class EFEMTRANSDataSet
    {        
        public EmEfemRobotArm Arm { get; set; }
        public EmEfemTransfer Transfer { get; set; }
        public EmEfemPort TargetPort { get; set; }
        public int Slot { get; set; }

        public EFEMTRANSDataSet(
            EmEfemRobotArm arm,
            EmEfemTransfer transfer,
            EmEfemPort targetPort,
            int slot
            )
        {
            Arm        = arm;
            Transfer   = transfer;
            TargetPort = targetPort;
            Slot       = slot;
        }

        public string ToString()
        {
            return string.Format("{0}.{1}.{2}.{3}", Transfer.ToString(), Arm.ToString(), TargetPort.ToString(), Slot);
        }
    }
    public class EFEMLPLEDDataSet
    {
        public EmEfemLampType LampType { get; set; }
        public EmEfemLampBuzzerState LampState { get; set; }

        public EFEMLPLEDDataSet(
            EmEfemLampType lampType,
            EmEfemLampBuzzerState lampState
            )
        {
            LampType = lampType;
            LampState = lampState;
        }
    }
    public class EFEMAlignDataSet
    {
        public string WaferID { get; set; }
        public int NotchDegree { get; set; }
        public bool OcrForward { get; set; }
    }
    #endregion DataSet
}
