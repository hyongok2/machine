using EquipMainUi.Monitor;
using EquipMainUi.Struct.Step;
using EquipMainUi.Struct.TransferData;
using EquipMainUi.Tact;
using EquipMainUi.UserMessageBoxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EquipMainUi.Struct.Detail.EFEM.Step
{
    public enum EmEFEMRobotHomeStep
    {
        H000_END,
        H005_EFEM_AUTO_CHANGE,
        H010_MOVING_CHECK,
        H015_WAIT_ROBOT_STOP,
        H020_WAIT_ROBOT_STOP_COMPLETE,
        H030_RESET_ROBOT,
        H040_WAIT_ROBOT_RESET,
        H050_WAIT_ROBOT_INIT,
        H060_CHECK_ROBOT_STATUS,
        H100_HOME_COMPLETE,        
    }
    public enum EmEFEMRobotSeqStep
    {
        S000_END,        
        S100_CHECK_WAFER_IN_ROBOT,
        S200_NO_WAFER,
        S201_CHECK_AVI,
        S205_CHECK_ALIGNER,
        S210_CHECK_LPM_READY,
        S211_MOVE_WAIT_POS,
        S215_WAIT_LPM_SEND_ABLE,
        S216_MOVE_WAIT_POS,
        S220_CHECK_LIGHT_CURTAIN_OFF,

        S225_MOVE_WAFER_LPM_TO_LOWER,

        S226_SEND_ALIGNER_READY_SIGNAL,
        S227_WAIT_SEND_ALIGNER_READY_COMPLETE,
        S228_WAIT_MOVE_WAFER_LPM_TO_LOWER,
        S229_WAIT_ROBOT_ALARM_RESET,

        S230_WAIT_USER_SELECT_PROGRESS,

        S231_WAIT_LPM_RECV_COMPLETE,
        S232_PROGRESS_STOP,
        S235_WAIT_LPM_RECV_SIGNAL_OFF,
        
        S300_HAVE_TWO_WAFER,        
        
        S500_LOWER_HAVE_WAFER,
        S510_CHECK_ALIGN_COMPLETE,
        S520_CHECK_ALIGNER_STATUS,
        S525_WAIT_ALIGNER_START_SIGNAL,
        S530_WAIT_MOVE_WAFER_ROBOT_TO_ALIGNER,
        S540_WAIT_ALIGNER_RECV_COMPLETE,
        S550_WAIT_ALIGNER_RECV_SIGNAL_CLEAR,
        S560_WAIT_PRE_ALIGN_END,
        S570_WAIT_ALIGNER_SEND_START,
        S580_WAIT_MOVE_WAFER_ALIGNER_TO_ROBOT,
        S590_WAIT_ALIGNER_SEND_COMPLETE,
        S595_WAIT_ALIGNER_SEND_SIGNAL_CLEAR,
        S600_CHECK_AVI_STATUS,
        
        S610_WAIT_ROBOT_WAITPOS_MOVE,        
        S620_WAIT_EQUIPMENT_PROC_END,        
        S630_START_WAFER_MOVE_ROBOT_TO_AVI,
        S640_WAIT_AVI_RECV_ABLE_OFF,
        S645_WAIT_MOVE_WAFER_ROBOT_TO_AVI,
        S650_WAIT_AVI_RECV_COMPLETE,
        S655_WAIT_MOVE_WAFER_AVI_TO_ROBOT,
        S660_WAIT_AVI_RECV_START,
        S670_WAIT_MOVE_WAFER_AVI_TO_ROBOT,
        S680_WAIT_AVI_SEND_COMPLETE,
        S690_WAIT_AVI_SEND_SIGNAL_CLEAR,
        S700_WAIT_DELAY,
        S710_AVI_RECV_WAIT,
        S720_WAIT_AVI_RECV_START,
        S730_WAIT_WAFER_MOVE_ROBOT_TO_AVI,
        S740_WAIT_AVI_RECV_COMPLETE,
        S750_MOVE_WAFER_LOWER_TO_AVI,
        S760_WAIT_AVI_RECV_SIGNAL_CLEAR,

        S800_UPPER_HAVE_WAFER,
        S801_CHACK_AUTOMOVEOUT, // KYH 230912 추가
        S805_WAIT_LPM_RECV_ABLE,
        S810_WAIT_LPM_RECV_START,
        S811_CHECK_LIGHT_CURTAIN_OFF,
        S815_WAIT_MOVE_WAFER_UPPER_TO_LPM,        
        S820_WAIT_LPM1_RECV_COMPLETE,
        S821_WAIT_LPM1_RECV_OFF,
        S825_WAIT_LPM2_RECV_COMPLETE,
        S826_WAIT_LPM2_RECV_OFF,
    }
    public class EFEMRobotUnit : StepBase
    {
        private EFEMUnit _efem;
        
        private EmEFEMRobotHomeStep _homeStepNum = 0;
        private EmEFEMRobotHomeStep _homeStepNumOld = 0;
        public EmEFEMRobotHomeStep HomeStepNum { get { return _homeStepNum; } }

        private EmEFEMRobotSeqStep _seqStepNum = 0;
        private EmEFEMRobotSeqStep _seqStepNumOld = 0;
        public EmEFEMRobotSeqStep SeqStepNum { get { return _seqStepNum; } }

        public List<EmEFEMRobotSeqStep> LstCycleStopStep = new List<EmEFEMRobotSeqStep>();

        public WaferInfoKey UpperWaferKey;
        public WaferInfoKey LastUnloadedWaferKey;

        public EFEMRobotStatus Status { get { return _efem.Proxy.RobotStat; } }

        public PioSignalRecv PioRecvLpm1 { get; set; }
        public PioSignalRecv PioRecvLpm2 { get; set; }
        public PioSignalRecv PioRecvAligner { get; set; }
        public PioSignalRecv PioRecvAVI { get; set; }

        public PioSignalSend PioSendLpm1 { get; set; }
        public PioSignalSend PioSendLpm2 { get; set; }
        public PioSignalSend PioSendAligner { get; set; }
        public PioSignalSend PioSendAVI { get; set; }

        private PlcTimerEx _lpmReadyWait = new PlcTimerEx("LPM READY WAIT", false);
        private PlcTimerEx _waferSkipWait = new PlcTimerEx("WAFER SKIP WAIT", false);

        private PlcTimerEx _SensorCheckWait = new PlcTimerEx("Sensor Check WAIT", false);
        private FrmRetryUserSelect _frmRetryMsg;
        private Timer _popupTimer = new Timer();
        public long LpmWaitTime {  get { return _lpmReadyWait.PassTime/1000; } }

        private bool _isAlignerForcedOut;
        private int _unldPort;

        public int TargetLoadPort { get; private set; }
        public void InitUserInterface(Equipment equip)
        {
            _frmRetryMsg = new FrmRetryUserSelect("다음 장 진행", "메뉴얼 전환");

            _popupTimer.Tick += _popupTimer_Tick;
            _popupTimer.Interval = 1000;
            _popupTimer.Start();
        }

        private void _popupTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_frmRetryMsg.PopupFlow == EmPopupFlow.PopupRequest)
                {
                    _frmRetryMsg.StartPosition = FormStartPosition.CenterScreen;
                    _frmRetryMsg.PopupFlow = EmPopupFlow.UserWait;
                    _frmRetryMsg.Show();
                }
            }
            catch (Exception ex)
            {
                if (AlarmMgr.Instance.IsHappened(GG.Equip, EM_AL_LST.AL_0946_UI_EXCEPTION) == false)
                {
                    Logger.ExceptionLog.AppendLine(LogLevel.Error, string.Format("UI 갱신 예외 발생 : {0}", ex.Message));
                    Logger.ExceptionLog.AppendLine(LogLevel.Error, Log.EquipStatusDump.CallStackLog());
                    AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0946_UI_EXCEPTION);
                }
            }
        }

        public bool IsInitDone
        {
            get
            {
                return Status.IsMoving == false
                    && Status.IsAlarm == false
                    && Status.IsServoOn == true
                    && Status.IsAutoMode == true
                    && Status.IsProgramRunning == true
                    && Status.IsArmUnFold == false
                    ;
            }
        }

        public EFEMRobotUnit(EFEMUnit efem)
        {
            _efem = efem;

            PioRecvLpm1 = new PioSignalRecv() { Name = "Robot-Lpm1-Recv" };
            PioSendLpm1 = new PioSignalSend() { Name = "Robot-Lpm1-Send" };
            PioRecvLpm2 = new PioSignalRecv() { Name = "Robot-Lpm2-Recv" };
            PioSendLpm2 = new PioSignalSend() { Name = "Robot-Lpm2-Send" };
            PioRecvAligner = new PioSignalRecv() { Name = "Robot-Aligner-Recv" };
            PioSendAligner = new PioSignalSend() { Name = "Robot-Aligner-Send" };
            PioRecvAVI = new PioSignalRecv() { Name = "Robot-AVI-Recv" };
            PioSendAVI = new PioSignalSend() { Name = "Robot-AVI-Send" };

            LstCycleStopStep.Add(EmEFEMRobotSeqStep.S000_END);
        }

        public override void LogicWorking(Equipment equip)
        {            
            StatusLogicWorking(equip);            
            SeqLogicWorking(equip);
        }

        Dit.Framework.PeriodChecker _statPeriod = new Dit.Framework.PeriodChecker();
        public override void StatusLogicWorking(Equipment equip)
        {
            if (_statPeriod.IsTimeToCheck(_efem.StatReadTime))
            {
                _statPeriod.Reset = true;

                if (GG.TestMode == true)
                    return;
                if (_efem.Proxy.IsRunning(equip, EmEfemPort.ROBOT, EmEfemCommand.STAT_) == false)
                    _efem.Proxy.StartCommand(equip, EmEfemPort.ROBOT, EmEfemCommand.STAT_);
            }
        }

        public override void HomeLogicWorking(Equipment equip)
        {
            SeqLogging(equip);

            switch (_homeStepNum)
            {
                case EmEFEMRobotHomeStep.H000_END:
                    if (_efem.Proxy.IsComplete(GG.Equip, EmEfemPort.ETC, EmEfemCommand.CHMDA) == true)
                        _efem.Proxy.StartCommand(GG.Equip, EmEfemPort.ETC, EmEfemCommand.CHMDA);
                    _efem.LPMLightCurtain.StopMuting();
                    _homeStepNum = EmEFEMRobotHomeStep.H005_EFEM_AUTO_CHANGE;
                    break;
                case EmEFEMRobotHomeStep.H005_EFEM_AUTO_CHANGE:
                    if (_efem.Proxy.IsComplete(GG.Equip, EmEfemPort.ETC, EmEfemCommand.CHMDA) == true)
                    {
                        _homeStepNum = EmEFEMRobotHomeStep.H010_MOVING_CHECK;
                    }
                    break;
                case EmEFEMRobotHomeStep.H010_MOVING_CHECK:                    
                    if (_efem.Proxy.RobotStat.IsMoving == true)
                        _homeStepNum = EmEFEMRobotHomeStep.H015_WAIT_ROBOT_STOP;
                    else
                    {
                        //if (_efem.Proxy.StartCommand(equip, EmEfemPort.ROBOT, EmEfemCommand.STOP_) == false) return;
                        _homeStepNum = EmEFEMRobotHomeStep.H020_WAIT_ROBOT_STOP_COMPLETE;
                    }
                    break;
                case EmEFEMRobotHomeStep.H015_WAIT_ROBOT_STOP:
                    if (_efem.Proxy.RobotStat.IsMoving == false)
                    {
                        _homeStepNum = EmEFEMRobotHomeStep.H030_RESET_ROBOT;
                    }
                    break;
                case EmEFEMRobotHomeStep.H020_WAIT_ROBOT_STOP_COMPLETE:
                    //if (_efem.Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.STOP_) == true)
                    {
                        _homeStepNum = EmEFEMRobotHomeStep.H030_RESET_ROBOT;
                    }
                    break;
                case EmEFEMRobotHomeStep.H030_RESET_ROBOT:
                    if (_efem.Proxy.StartCommand(equip, EmEfemPort.ROBOT, EmEfemCommand.RESET) == false) return;
                    _homeStepNum = EmEFEMRobotHomeStep.H040_WAIT_ROBOT_RESET;
                    break;
                case EmEFEMRobotHomeStep.H040_WAIT_ROBOT_RESET:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.RESET) == true)
                    {
                        if (_efem.Proxy.StartCommand(equip, EmEfemPort.ROBOT, EmEfemCommand.INIT_) == false) return;
                        _homeStepNum = EmEFEMRobotHomeStep.H050_WAIT_ROBOT_INIT;
                    }
                    break;
                case EmEFEMRobotHomeStep.H050_WAIT_ROBOT_INIT:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.INIT_) == true)
                    {                        
                        _homeStepNum = EmEFEMRobotHomeStep.H060_CHECK_ROBOT_STATUS;
                    }
                    break;
                case EmEFEMRobotHomeStep.H060_CHECK_ROBOT_STATUS:
                    if (GG.TestMode == true || IsInitDone == true)
                    {
                        _homeStepNum = EmEFEMRobotHomeStep.H100_HOME_COMPLETE;
                    }
                    break;
                case EmEFEMRobotHomeStep.H100_HOME_COMPLETE:
                    IsHomeComplete = true;
                    break;
            }
        }
        public override void SeqLogicWorking(Equipment equip)
        {
            if (_efem.RunMode == EmEfemRunMode.Stop)
            {
                Stop();

                _seqStepNum = EmEFEMRobotSeqStep.S000_END;
                _homeStepNum = EmEFEMRobotHomeStep.H000_END;
            }
            else if (_efem.RunMode == EmEfemRunMode.Pause || this.RunMode == EmEfemRunMode.Pause)
            {

            }
            else if (_efem.RunMode == EmEfemRunMode.Home)
            {
                if (this.IsHomeComplete == false)
                {
                    if (this.IsSafeToHome() == true)
                        HomeLogicWorking(equip);
                }
                else if (IsReserveStart == true)
                {
                    SetRunMode(EmEfemRunMode.Start);
                }
            }
            else if (_efem.RunMode == EmEfemRunMode.Start)
            {
                if (_efem.RunMode == EmEfemRunMode.CycleStop && LstCycleStopStep.Contains(SeqStepNum) == true)
                {
                    SetRunMode(EmEfemRunMode.Pause);
                }
                else
                {
                    if (_efem.IsHomeComplete == false) return;
                    if (GG.PassLightCurtain == false && _efem.LPMLightCurtain.Detect.IsOn && GG.TestMode == false) return; //라이트커튼 제거
                    if (GG.Equip.IsAllHomeComplete == false) return;

                    GetLPMWaferLogic(equip);
                    AlignToRobotLogic(equip);
                    AlignEndToAVILogic(equip);
                    AVIToRobotLogic(equip);
                    RobotToLPMLogic(equip);
                }
            }
        }

        private void SeqLogging(Equipment equip)
        {
            SeqStepStr = _seqStepNum.ToString();
            HomeStepStr = HomeStepNum.ToString();

            if (_seqStepNum != _seqStepNumOld)
                _seqStepNumOld = _seqStepNum;

            if (_homeStepNum != _homeStepNumOld)
                _homeStepNumOld = _homeStepNum;

            base.LogicWorking(equip);
        }

        public void GetLPMWaferLogic(Equipment equip)
        {
            SeqLogging(equip);

            PioSignalRecv pioRecv;

            switch (_seqStepNum)
            {
                case EmEFEMRobotSeqStep.S000_END:
                    if (TargetLoadPort != 0)
                    {
                        // 해당 포트 Foup이 없는 경우 초기화
                        if ((TargetLoadPort == 2 && _efem.LoadPort2.Status.IsFoupExist == false)
                            || (TargetLoadPort == 1 && _efem.LoadPort1.Status.IsFoupExist == false))
                        {
                            Logger.Log.AppendLine(LogLevel.Info, "LoadPort{0} Foup 없음, TargetLoadPort 재지정", TargetLoadPort);
                            SetTargetLoadPort(0);
                        }
                           
                        // 설비 내 제품이 없는 경우 초기화
                        if (GG.Equip.IsWaferDetect == EmGlassDetect.NOT
                            && _efem.Robot.Status.IsLowerArmVacOn == false
                            && _efem.Robot.Status.IsUpperArmVacOn == false
                            && _efem.Aligner.Status.IsWaferExist == false
                            )
                        {
                            Logger.Log.AppendLine(LogLevel.Info, "설비 내 Wafer 없음, TargetLoadPort 재지정", TargetLoadPort);
                            SetTargetLoadPort(0);
                        }
                        else
                        {
                            EFEMLPMUnit lpm = TargetLoadPort == 2 ? _efem.LoadPort2 : _efem.LoadPort1;

                            // 제품 중 해당 포트의 아이디와 일치하는게 없는 경우 초기화
                            if ((GG.Equip.IsWaferDetect == EmGlassDetect.NOT ? false : (GG.Equip.TransferUnit.LowerWaferKey != null && GG.Equip.TransferUnit.LowerWaferKey.CstID == lpm.CstKey.ID))
                                || (_efem.Robot.Status.IsLowerArmVacOn == false ? false : (_efem.Robot.LowerWaferKey != null && _efem.Robot.LowerWaferKey.CstID == lpm.CstKey.ID))
                                || (_efem.Robot.Status.IsUpperArmVacOn == false ? false : (_efem.Robot.UpperWaferKey != null && _efem.Robot.UpperWaferKey.CstID == lpm.CstKey.ID))
                                || (_efem.Aligner.Status.IsWaferExist == false  ? false : (_efem.Aligner.LowerWaferKey != null && _efem.Aligner.LowerWaferKey.CstID == lpm.CstKey.ID))
                                )
                            {

                            }
                            else
                            {
                                Logger.Log.AppendLine(LogLevel.Info, "지정 TargetLoadPort에서 Wafer 배출 시작 안됨, TargetLoadPort 재지정", TargetLoadPort);
                                SetTargetLoadPort(0);
                            }
                        }
                    }

                    _seqStepNum = EmEFEMRobotSeqStep.S100_CHECK_WAFER_IN_ROBOT;
                    break;
                case EmEFEMRobotSeqStep.S100_CHECK_WAFER_IN_ROBOT:
                    //case 1 : lower, upper 없음
                    if (Status.IsLowerArmVacOn == false
                        && Status.IsUpperArmVacOn == false
                        )
                    {
                        _seqStepNum = EmEFEMRobotSeqStep.S200_NO_WAFER;
                    }
                    //case 2 : lower, upper 있음
                    else if (Status.IsLowerArmVacOn == true
                        && Status.IsUpperArmVacOn == true)
                    {
                        _seqStepNum = EmEFEMRobotSeqStep.S300_HAVE_TWO_WAFER;
                    }
                    //case 3 : lower만 있음
                    else if (Status.IsLowerArmVacOn == true
                        && Status.IsUpperArmVacOn == false)
                    {
                        _seqStepNum = EmEFEMRobotSeqStep.S500_LOWER_HAVE_WAFER;
                    }
                    //case 4 : upper만 있음
                    else if (Status.IsLowerArmVacOn == false
                        && Status.IsUpperArmVacOn == true)
                    {
                        _seqStepNum = EmEFEMRobotSeqStep.S800_UPPER_HAVE_WAFER;
                    }
                    break;

                case EmEFEMRobotSeqStep.S200_NO_WAFER:
                    _SensorCheckWait.Start(1);
                    _seqStepNum = EmEFEMRobotSeqStep.S201_CHECK_AVI;
                    break;
                case EmEFEMRobotSeqStep.S201_CHECK_AVI:
                    if (_SensorCheckWait)
                    {
                        _SensorCheckWait.Stop();

                        if (_efem.Aligner.Status.IsWaferExist == true)
                            _seqStepNum = EmEFEMRobotSeqStep.S560_WAIT_PRE_ALIGN_END;
                        else if (equip.IsWaferDetect != EmGlassDetect.NOT && _efem.Aligner.Status.IsWaferExist == false
                            && PioRecvLpm2.XSendAble == false && PioRecvLpm1.XSendAble == false)
                        {
                            Logger.Log.AppendLine(LogLevel.NoLog, "Robot 600 스텝 진행 전, IsWaferDetect {0}, LiftPin1 {1}, LiftPin2 {2}, Stage {3}, Aligner.Status.IsWaferExist: {4}, PioRecvLpm2.XSendAble : {5}, PioRecvLpm1.XSendAble : {6}",
                                equip.IsWaferDetect, equip.WaferDetectSensorLiftpin1.IsOn, equip.WaferDetectSensorLiftpin2.IsOn, equip.WaferDetectSensorStage1.IsOn, _efem.Aligner.Status.IsWaferExist, PioRecvLpm2.XSendAble, PioRecvLpm1.XSendAble);
                            _seqStepNum = EmEFEMRobotSeqStep.S600_CHECK_AVI_STATUS;
                        }
                        else
                        {
                            Logger.Log.AppendLine(LogLevel.NoLog, "Robot 220 스텝 진행 전, IsWaferDetect {0}, LiftPin1 {1}, LiftPin2 {2}, Stage {3}, Aligner.Status.IsWaferExist: {4}, PioRecvLpm2.XSendAble : {5}, PioRecvLpm1.XSendAble : {6}",
                               equip.IsWaferDetect, equip.WaferDetectSensorLiftpin1.IsOn, equip.WaferDetectSensorLiftpin2.IsOn, equip.WaferDetectSensorStage1.IsOn, _efem.Aligner.Status.IsWaferExist, PioRecvLpm2.XSendAble, PioRecvLpm1.XSendAble);
                            _seqStepNum = EmEFEMRobotSeqStep.S220_CHECK_LIGHT_CURTAIN_OFF;
                        }
                    }
                    break;
                //case EmEFEMRobotSeqStep.S205_CHECK_ALIGNER:
                //    if (_efem.Aligner.Status.IsWaferExist)
                //        _seqStepNum = EmEFEMRobotSeqStep.S560_WAIT_PRE_ALIGN_END;
                //    else
                //        _seqStepNum = EmEFEMRobotSeqStep.S220_CHECK_LIGHT_CURTAIN_OFF;
                //    break;
                case EmEFEMRobotSeqStep.S220_CHECK_LIGHT_CURTAIN_OFF:
                    if (GG.PassLightCurtain || _efem.LPMLightCurtain.Detect.IsOn == false) //라이트커튼 제거
                    {
                        _seqStepNum = EmEFEMRobotSeqStep.S210_CHECK_LPM_READY;
                    }
                    break;
                case EmEFEMRobotSeqStep.S210_CHECK_LPM_READY:
                    if (TargetLoadPort == 2 && _efem.LoadPort2.Status.IsFoupExist == false)
                    {
                        Logger.Log.AppendLine(LogLevel.NoLog, "TargetPort 재지정");
                        SetTargetLoadPort(0);                        
                    }
                    else if (TargetLoadPort == 1 && _efem.LoadPort1.Status.IsFoupExist == false)
                    {
                        Logger.Log.AppendLine(LogLevel.NoLog, "TargetPort 재지정");
                        SetTargetLoadPort(0);
                    }                    

                    if (TargetLoadPort != 0)
                        _seqStepNum = EmEFEMRobotSeqStep.S215_WAIT_LPM_SEND_ABLE;                            
                    else if (PioRecvLpm2.XSendAble == true)
                    {
                        SetTargetLoadPort(2);
                        PioRecvLpm2.YRecvAble = true;
                        _seqStepNum = EmEFEMRobotSeqStep.S225_MOVE_WAFER_LPM_TO_LOWER;
                    }
                    else if (PioRecvLpm1.XSendAble == true)
                    {
                        SetTargetLoadPort(1);
                        PioRecvLpm1.YRecvAble = true;
                        _seqStepNum = EmEFEMRobotSeqStep.S225_MOVE_WAFER_LPM_TO_LOWER;
                    }
                    else if (_efem.LoadPort2.BeginToOpen == true)
                    {
                        _lpmReadyWait.Stop();
                        SetTargetLoadPort(2);
                        if (_efem.Proxy.StartWaitRobot(equip, new EFEMWAITRDataSet(EmEfemRobotArm.Lower, EmEfemPort.LOADPORT2, 5)) == false) return;
                        _seqStepNum = EmEFEMRobotSeqStep.S211_MOVE_WAIT_POS;
                    }
                    else if (_efem.LoadPort1.BeginToOpen == true)
                    {
                        _lpmReadyWait.Stop();
                        SetTargetLoadPort(1);
                        if (_efem.Proxy.StartWaitRobot(equip, new EFEMWAITRDataSet(EmEfemRobotArm.Lower, EmEfemPort.LOADPORT1, 5)) == false) return;
                        _seqStepNum = EmEFEMRobotSeqStep.S211_MOVE_WAIT_POS;
                    }
                    else
                        SetTargetLoadPort(0);
                    break;
                case EmEFEMRobotSeqStep.S211_MOVE_WAIT_POS:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.WAITR) == true && Status.IsMoving == false)
                    {
                        if (TargetLoadPort == 2 && PioRecvLpm2.XSendAble == true)
                        {
                            _lpmReadyWait.Stop();
                            PioRecvLpm2.YRecvAble = true;
                            _seqStepNum = EmEFEMRobotSeqStep.S225_MOVE_WAFER_LPM_TO_LOWER;
                        }
                        else if (TargetLoadPort == 1 && PioRecvLpm1.XSendAble == true)
                        {
                            _lpmReadyWait.Stop();
                            PioRecvLpm1.YRecvAble = true;
                            _seqStepNum = EmEFEMRobotSeqStep.S225_MOVE_WAFER_LPM_TO_LOWER;
                        }
                        else if (TargetLoadPort == 1 ? _efem.LoadPort1.IsUnldButtonWaitStep : _efem.LoadPort2.IsUnldButtonWaitStep)
                        {
                            SetTargetLoadPort(0);
                            Logger.Log.AppendLine(LogLevel.Error, "TargetLoadPort{0} 배출 스텝 되어 재지정 스텝 이동", TargetLoadPort);
                            _seqStepNum = EmEFEMRobotSeqStep.S210_CHECK_LPM_READY;
                        }
                        else if (_lpmReadyWait.IsStart == false)
                        {
                            if (GG.TestMode == true)
                                _lpmReadyWait.Start(600, 0);
                            else
                                _lpmReadyWait.Start(600, 0); // TargetPort 지정 전 LPM Open상태 보고 해당 LPM에 대기 중
                        }
                        else if (TargetLoadPort != 0
                            && (_efem.LoadPort((EmEfemPort)TargetLoadPort).IsLdFoupWaitStep
                            || _efem.LoadPort((EmEfemPort)TargetLoadPort).IsLdButtonWaitStep
                            || _efem.LoadPort((EmEfemPort)TargetLoadPort).IsUserInterfaceWaitStep)
                            )
                            _lpmReadyWait.Stop();
                        else if (_lpmReadyWait)
                        {
                            _lpmReadyWait.Stop();
                            if (TargetLoadPort == 2)
                                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0668_LPM2_READY_WAIT_OVERTIME);
                            else if (TargetLoadPort == 1)
                                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0667_LPM1_READY_WAIT_OVERTIME);
                            else
                                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0669_TARGETPORT_ABNORMAL);
                        }
                    }
                    break;
                case EmEFEMRobotSeqStep.S215_WAIT_LPM_SEND_ABLE:
                    PioSignalRecv recv = TargetLoadPort == 2 ? PioRecvLpm2 : PioRecvLpm1;
                    PioSignalRecv otherRecv = TargetLoadPort == 2 ? PioRecvLpm1 : PioRecvLpm2;

                    if ((TargetLoadPort == 2 && _efem.LoadPort2.Status.IsFoupExist == false)
                        || (TargetLoadPort == 1 && _efem.LoadPort1.Status.IsFoupExist == false))
                    {
                        Logger.Log.AppendLine(LogLevel.Info, "LoadPort{0} Foup 없음, TargetPort 재지정", TargetLoadPort);
                        SetTargetLoadPort(0);
                        _seqStepNum = EmEFEMRobotSeqStep.S210_CHECK_LPM_READY;
                    }

                    if (recv.XSendAble == true)
                    {
                        recv.YRecvAble = true;
                        _seqStepNum = EmEFEMRobotSeqStep.S225_MOVE_WAFER_LPM_TO_LOWER;
                    }
                    else if (otherRecv.XSendAble == true)
                    {
                        SetTargetLoadPort(TargetLoadPort == 2 ? 1 : 2);
                        otherRecv.YRecvAble = true;
                        _seqStepNum = EmEFEMRobotSeqStep.S225_MOVE_WAFER_LPM_TO_LOWER;
                    }
                    else if (GG.Equip.IsWaferDetect != EmGlassDetect.NOT
                        && TransferDataMgr.IsAllOut(TargetLoadPort == 2 ? _efem.LoadPort2.CstKey : _efem.LoadPort1.CstKey) == true
                        && TransferDataMgr.IsAllComeBack(TargetLoadPort == 2 ? _efem.LoadPort2.CstKey : _efem.LoadPort1.CstKey) == false)
                    {
                        _seqStepNum = EmEFEMRobotSeqStep.S600_CHECK_AVI_STATUS;
                    }
                    else
                    {
                        _lpmReadyWait.Stop();
                        if (_efem.Proxy.StartWaitRobot(equip, new EFEMWAITRDataSet(EmEfemRobotArm.Lower, TargetLoadPort == 2 ? EmEfemPort.LOADPORT2 : EmEfemPort.LOADPORT1, 5)) == false) return;
                        _seqStepNum = EmEFEMRobotSeqStep.S216_MOVE_WAIT_POS;
                    }
                    break;
                case EmEFEMRobotSeqStep.S216_MOVE_WAIT_POS:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.WAITR) == true && Status.IsMoving == false)
                    {
                        recv = TargetLoadPort == 2 ? PioRecvLpm2 : PioRecvLpm1;
                        if (recv.XSendAble == true)
                        {
                            _lpmReadyWait.Stop();
                            recv.YRecvAble = true;
                            _seqStepNum = EmEFEMRobotSeqStep.S225_MOVE_WAFER_LPM_TO_LOWER;
                        }
                        else if (_lpmReadyWait.IsStart == false)
                        {
                            if (GG.TestMode == true)
                                _lpmReadyWait.Start(600, 0);
                            else
                                _lpmReadyWait.Start(600, 0); // TargetPort 지정된 상태에서 LPM 준비 안됐을 때 대기 중
                        }
                        else if (TargetLoadPort != 0
                            && (_efem.LoadPort((EmEfemPort)TargetLoadPort).IsLdFoupWaitStep
                            || _efem.LoadPort((EmEfemPort)TargetLoadPort).IsLdButtonWaitStep
                            || _efem.LoadPort((EmEfemPort)TargetLoadPort).IsUserInterfaceWaitStep)
                            )
                            _lpmReadyWait.Stop();
                        else if (_lpmReadyWait)
                        {
                            _lpmReadyWait.Stop();
                            if (TargetLoadPort == 2)
                                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0668_LPM2_READY_WAIT_OVERTIME);
                            else if (TargetLoadPort == 1)
                                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0667_LPM1_READY_WAIT_OVERTIME);
                            else
                                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0669_TARGETPORT_ABNORMAL);
                        }
                    }
                    break;
                case EmEFEMRobotSeqStep.S225_MOVE_WAFER_LPM_TO_LOWER:
                    CassetteInfo cst;

                    if (TargetLoadPort == 2 && PioRecvLpm2.XSendAble == true && PioRecvLpm2.XSendStart == true)
                    {
                        cst = TransferDataMgr.GetCst(_efem.LoadPort2.CstKey);
                        if (TransferDataMgr.IsAllOut(_efem.LoadPort2.CstKey) == false)
                        {
                            PioRecvLpm2.YRecvAble = true;
                            PioRecvLpm2.YRecvStart = true;

                            _efem.LoadPort2.UpdateNextWaferKey();
                            EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T010_ROBOT_PICK_FROM_LPM_START, _efem.LoadPort2.LowerWaferKey);
                            if (_efem.Proxy.StartTransRobot(equip,
                                new EFEMTRANSDataSet(EmEfemRobotArm.Lower, EmEfemTransfer.PICK, EmEfemPort.LOADPORT2,
                                _efem.LoadPort2.LowerWaferKey.SlotNo)) == false) return;
                            Logger.TacttimeLog.AppendLine(LogLevel.Info, "[EFEM TACT]LPM TO ROBOT PIO START");

                            _tempWaferInfo = TransferDataMgr.GetWafer(_efem.LoadPort2.LowerWaferKey);
                            _tempWaferInfo.StartTime = DateTime.Now;
                            _tempWaferInfo.Update();
                            
                            _seqStepNum = EmEFEMRobotSeqStep.S226_SEND_ALIGNER_READY_SIGNAL;
                        }
                        else
                        {
                            AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0664_LPM2_WAFER_STATUS_ABNORMAL);
                            SetTargetLoadPort(0);
                        }
                    }
                    else if (TargetLoadPort == 1 && PioRecvLpm1.XSendAble == true && PioRecvLpm1.XSendStart == true)
                    {
                        cst = TransferDataMgr.GetCst(_efem.LoadPort1.CstKey);
                        if (TransferDataMgr.IsAllOut(_efem.LoadPort1.CstKey) == false)
                        {
                            PioRecvLpm1.YRecvAble = true;
                            PioRecvLpm1.YRecvStart = true;

                            _efem.LoadPort1.UpdateNextWaferKey();
                            EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T010_ROBOT_PICK_FROM_LPM_START, _efem.LoadPort1.LowerWaferKey);
                            if (_efem.Proxy.StartTransRobot(equip,
                                new EFEMTRANSDataSet(EmEfemRobotArm.Lower, EmEfemTransfer.PICK, EmEfemPort.LOADPORT1,
                                _efem.LoadPort1.LowerWaferKey.SlotNo)) == false) return;
                            Logger.TacttimeLog.AppendLine(LogLevel.Info, "[EFEM TACT]LPM TO ROBOT PIO START");

                            _tempWaferInfo = TransferDataMgr.GetWafer(_efem.LoadPort1.LowerWaferKey);
                            _tempWaferInfo.StartTime = DateTime.Now;
                            _tempWaferInfo.Update();
                            
                            _seqStepNum = EmEFEMRobotSeqStep.S226_SEND_ALIGNER_READY_SIGNAL;
                        }
                        else
                        {
                            AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0663_LPM1_WAFER_STATUS_ABNORMAL);
                            SetTargetLoadPort(0);
                        }
                    }
                    break;
                case EmEFEMRobotSeqStep.S226_SEND_ALIGNER_READY_SIGNAL:
                    if(GG.TestMode == true && GG.IsDitPreAligner == false)
                    {
                        _seqStepNum = EmEFEMRobotSeqStep.S228_WAIT_MOVE_WAFER_LPM_TO_LOWER;
                    }
                    else
                    {
                        if (_efem.Proxy.StartCommand(equip, EmEfemPort.ALIGNER, EmEfemCommand.PARDY) == false) return;
                        _seqStepNum = EmEFEMRobotSeqStep.S227_WAIT_SEND_ALIGNER_READY_COMPLETE;
                    }
                    break;
                case EmEFEMRobotSeqStep.S227_WAIT_SEND_ALIGNER_READY_COMPLETE:
                    if(_efem.Proxy.IsComplete(equip, EmEfemPort.ALIGNER, EmEfemCommand.PARDY) == true)
                    {
                        _seqStepNum = EmEFEMRobotSeqStep.S228_WAIT_MOVE_WAFER_LPM_TO_LOWER;
                    }
                    break;
                case EmEFEMRobotSeqStep.S228_WAIT_MOVE_WAFER_LPM_TO_LOWER:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.TRANS) == true
                        && Status.IsMoving == false && Status.IsAlarm && GG.VacuumFailSkipMode 
                        && AlarmMgr.Instance.IsHappened(equip, EM_AL_LST.AL_0802_WAFER_SKIPPED_BECAUSE_ROBOT_CANNOT_VACUUM_ON))
                    {
                        AlarmMgr.Instance.HappenAlarms[EM_AL_LST.AL_0802_WAFER_SKIPPED_BECAUSE_ROBOT_CANNOT_VACUUM_ON].Happen = false;

                        pioRecv = this.TargetLoadPort == 2 ? PioRecvLpm2 : PioRecvLpm1;

                        pioRecv.YRecvAble = true;
                        pioRecv.YRecvStart = true;
                        pioRecv.YRecvComplete = true;
                        
                        Logger.Log.AppendLine(LogLevel.Info, "[EFEM TACT]LPM TO ROBOT PIO SKIPPED");

                        EFEMLPMUnit LoadPort = equip.Efem.LoadPort((EmEfemPort)TargetLoadPort);
                        try
                        {
                            if(LoadPort.ProgressWay == EmProgressWay.Mapping || LoadPort.ProgressWay == EmProgressWay.User)
                            {
                                _tempWaferInfo = TransferDataMgr.GetWafer(LoadPort.LowerWaferKey);
                                _tempWaferInfo.IsOut = true;
                                _tempWaferInfo.OutputDate = DateTime.Now;
                                _tempWaferInfo.InputDate = DateTime.Now;
                                _tempWaferInfo.IsComeBack = true;
                                _tempWaferInfo.Update();
                            }
                            //only first
                            else if(LoadPort.ProgressWay == EmProgressWay.OnlyFirst)
                            {
                                List<WaferInfo> _list = TransferDataMgr.GetWafers(LoadPort.CstKey.ID);
                                int NewFirstSlotNo = -1;
                                for (int i = 0; i < _list.Count; i++)
                                {
                                    if(_list[i].IsComeBack == false && _list[i].IsOut == false)
                                    {
                                        if(_list.Count > i+1)
                                        {
                                            NewFirstSlotNo = _list[i + 1].SlotNo;
                                            break;
                                        }
                                        else
                                        {
                                            _seqStepNum = EmEFEMRobotSeqStep.S232_PROGRESS_STOP;
                                            break;
                                        }
                                    }
                                }
                                foreach (var input in _list)
                                {
                                    _tempWaferInfo = input;
                                    // 실제 Mapping해서 Wafer있는걸로 확인된 것 중에서만 입력한 데이터 (상위or유저)에 맞게 진행여부 변경
                                    if (_tempWaferInfo.Status == EmEfemMappingInfo.Presence)
                                    {
                                        if (input.SlotNo == NewFirstSlotNo)
                                        {
                                            _tempWaferInfo.IsComeBack = false;
                                            _tempWaferInfo.IsOut = false;
                                            _tempWaferInfo.IsAlignComplete = false;
                                            _tempWaferInfo.IsInspComplete = false;
                                            _tempWaferInfo.IsReviewComplete = false;
                                            _tempWaferInfo.Notch = input.Notch;
                                        }
                                        else
                                        {
                                            _tempWaferInfo.IsComeBack = true;
                                            _tempWaferInfo.IsOut = true;
                                            _tempWaferInfo.Notch = input.Notch;
                                        }
                                    }
                                    _tempWaferInfo.Update();
                                }
                            }
                            //only last
                            else if (LoadPort.ProgressWay == EmProgressWay.OnlyLast)
                            {
                                List<WaferInfo> _list = TransferDataMgr.GetWafers(LoadPort.CstKey.ID);
                                int NewFirstSlotNo = -1;
                                for (int i = _list.Count -1; i >= 1; i--)
                                {
                                    if (_list[i].IsComeBack == false && _list[i].IsOut == false)
                                    {
                                        if (i - 1 >= 0)
                                        {
                                            NewFirstSlotNo = _list[i - 1].SlotNo;
                                            break;
                                        }
                                        else
                                        {
                                            _seqStepNum = EmEFEMRobotSeqStep.S232_PROGRESS_STOP;
                                            break;
                                        }
                                    }
                                }
                                foreach (var input in _list)
                                {
                                    _tempWaferInfo = input;
                                    // 실제 Mapping해서 Wafer있는걸로 확인된 것 중에서만 입력한 데이터 (상위or유저)에 맞게 진행여부 변경
                                    if (_tempWaferInfo.Status == EmEfemMappingInfo.Presence)
                                    {
                                        if (input.SlotNo == NewFirstSlotNo)
                                        {
                                            _tempWaferInfo.IsComeBack = false;
                                            _tempWaferInfo.IsOut = false;
                                            _tempWaferInfo.IsAlignComplete = false;
                                            _tempWaferInfo.IsInspComplete = false;
                                            _tempWaferInfo.IsReviewComplete = false;
                                            _tempWaferInfo.Notch = input.Notch;
                                        }
                                        else
                                        {
                                            _tempWaferInfo.IsComeBack = true;
                                            _tempWaferInfo.IsOut = true;
                                            _tempWaferInfo.Notch = input.Notch;
                                        }
                                    }
                                    _tempWaferInfo.Update();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0660_NO_WAFER_INFO);
                        }
                        ///////////////////////////////////////////////////////////////////////////////////
                        if (_efem.Proxy.StartCommand(equip, EmEfemPort.ROBOT, EmEfemCommand.RESET) == false) return;

                        _waferSkipWait.Start(1);
                        _seqStepNum = EmEFEMRobotSeqStep.S229_WAIT_ROBOT_ALARM_RESET;

                    }
                    else if (_efem.Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.TRANS) == true
                        && Status.IsLowerArmVacOn == true && Status.IsMoving == false)
                    {
                        pioRecv = this.TargetLoadPort == 2 ? PioRecvLpm2 : PioRecvLpm1;

                        pioRecv.YRecvAble = true;
                        pioRecv.YRecvStart = true;
                        pioRecv.YRecvComplete = true;
                        
                        Logger.TacttimeLog.AppendLine(LogLevel.Info, "[EFEM TACT]LPM TO ROBOT PIO COMPLETE");
                        try
                        {
                            _tempWaferInfo = TransferDataMgr.GetWafer(this.LowerWaferKey);
                            _tempWaferInfo.IsOut = true;
                            _tempWaferInfo.OutputDate = DateTime.Now;
                            _tempWaferInfo.Update();
                        }
                        catch (Exception ex)
                        {
                            AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0660_NO_WAFER_INFO);
                        }

                        _seqStepNum = EmEFEMRobotSeqStep.S231_WAIT_LPM_RECV_COMPLETE;
                    }
                    break;
                case EmEFEMRobotSeqStep.S229_WAIT_ROBOT_ALARM_RESET:
                    if(_efem.Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.RESET) && _waferSkipWait)
                    {
                        _waferSkipWait.Stop();
                        Logger.Log.AppendLine(LogLevel.Info, "[Wafer Skip] Because Robot Arm cannot vacuum on");
                        _frmRetryMsg.RequestPopup(GG.boChinaLanguage ? "ROBOT晶片拾取失败" : "ROBOT Wafer Pick 실패", GG.boChinaLanguage ? "请在下列选项中选择" : "다음 중 선택하세요");
                        _seqStepNum = EmEFEMRobotSeqStep.S230_WAIT_USER_SELECT_PROGRESS;
                    }
                    break;
                case EmEFEMRobotSeqStep.S230_WAIT_USER_SELECT_PROGRESS:
                    if (_frmRetryMsg.PopupFlow == EmPopupFlow.OK)
                    {
                        if (_frmRetryMsg.IsRetry)//다음 장 진행
                        {
                            _seqStepNum = EmEFEMRobotSeqStep.S231_WAIT_LPM_RECV_COMPLETE;
                        }
                        else //메뉴얼 전환
                        {
                            _seqStepNum = EmEFEMRobotSeqStep.S232_PROGRESS_STOP;
                        }
                    }
                    break;
                case EmEFEMRobotSeqStep.S232_PROGRESS_STOP:
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0804_VACUUM_ON_FAIL_ALL_WAFER);
                    equip.ChangeRunMode(EmEquipRunMode.Manual);
                    equip.IsInterlock = true;
                    _seqStepNum = EmEFEMRobotSeqStep.S000_END;
                    return;
                case EmEFEMRobotSeqStep.S231_WAIT_LPM_RECV_COMPLETE:
                    PioRecv = this.TargetLoadPort == 2 ? PioRecvLpm2 : PioRecvLpm1;
                    if (PioRecv.XSendAble == true && PioRecv.XSendStart == true && PioRecv.XSendComplete == true)
                    {
                        PioRecv.Initailize();
                        _seqStepNum = EmEFEMRobotSeqStep.S235_WAIT_LPM_RECV_SIGNAL_OFF;
                    }
                    break;
                case EmEFEMRobotSeqStep.S235_WAIT_LPM_RECV_SIGNAL_OFF:
                    PioRecv = this.TargetLoadPort == 2 ? PioRecvLpm2 : PioRecvLpm1;
                    if (PioRecv.XSendAble == false && PioRecv.XSendStart == false && PioRecv.XSendComplete == false)
                    {
                        _seqStepNum = EmEFEMRobotSeqStep.S100_CHECK_WAFER_IN_ROBOT;
                    }
                    break;
            }
        }
        public void AlignToRobotLogic(Equipment equip)
        {
            SeqLogging(equip);

            switch (_seqStepNum)
            {
                case EmEFEMRobotSeqStep.S500_LOWER_HAVE_WAFER:
                    if (_efem.Aligner.Status.IsWaferExist == true)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0622_EFEM_SEQ_ABNORMAL);
                        InterLockMgr.AddInterLock(GG.boChinaLanguage ? "不是正常的 Sequence. 确认Lower Arm, Aligner 当中 是否有错误的Wafer Info, 需排除后进行." : "정상적인 시퀀스가 아닙니다. Lower Arm, Aligner 중 웨이퍼정보가 안맞는 것이 있는지 확인하고 제거 후 진행해야합니다");
                        _efem.ChangeMode(EmEfemRunMode.Pause);
                    }

                    _seqStepNum = EmEFEMRobotSeqStep.S510_CHECK_ALIGN_COMPLETE;
                    break;
                case EmEFEMRobotSeqStep.S510_CHECK_ALIGN_COMPLETE:
                    if (TransferDataMgr.GetWafer(LowerWaferKey).IsAlignComplete == false)
                    {
                        // 2. EFEM 0817 Error 51번 Error 관련 수정 (Auto Run 동작 중 Lower To LPM 진행 시, 알람 발생할 경우)
                        // Lower To LPM에서 재시작 시, 바로 Case500 진행하므로 outputdate Update
                        // Lower to LPM (Lower에 Wafer 들어온 순간) Error 발생 시, 재시작 하였을 경우
                        if (TransferDataMgr.GetWafer(LowerWaferKey.CstID, GG.Equip.Efem.Robot.LowerWaferKey.SlotNo).Status == EmEfemMappingInfo.Presence &&
                            (TransferDataMgr.GetWafer(LowerWaferKey.CstID, GG.Equip.Efem.Robot.LowerWaferKey.SlotNo).OutputDate.Year == 0001 ||
                            TransferDataMgr.GetWafer(LowerWaferKey.CstID, GG.Equip.Efem.Robot.LowerWaferKey.SlotNo).IsOut == false)) // outputdate = 0001년, isout = false, presence
                        {
                            // Lower에 있는 Wafer 정보 Get
                            _tempWaferInfo = TransferDataMgr.GetWafer(LowerWaferKey.CstID, GG.Equip.Efem.Robot.LowerWaferKey.SlotNo);
                            _tempWaferInfo.IsOut = true; // EFEMLPMUnit.cs에 S530에서 확인 시, IsOut false면 YSendAble 신호 변경됨                            
                            _tempWaferInfo.OutputDate = DateTime.Now; // S228에서 Output DateTime Update하는데 Lower To LPM 시, S227에서 대기해서 Update안됨
                            _tempWaferInfo.Update();
                        }
                        _seqStepNum = EmEFEMRobotSeqStep.S520_CHECK_ALIGNER_STATUS;
                    }
                    else
                    {
                        _seqStepNum = EmEFEMRobotSeqStep.S600_CHECK_AVI_STATUS;
                    }
                    break;
                case EmEFEMRobotSeqStep.S520_CHECK_ALIGNER_STATUS:
                    if (PioSendAligner.XRecvAble == true)
                    {
                        PioSendAligner.YSendAble = true;
                        _seqStepNum = EmEFEMRobotSeqStep.S525_WAIT_ALIGNER_START_SIGNAL;
                    }                    
                    break;
                case EmEFEMRobotSeqStep.S525_WAIT_ALIGNER_START_SIGNAL:
                    if (PioSendAligner.XRecvAble == true && PioSendAligner.XRecvStart == true)
                    {
                        PioSendAligner.YSendAble = true;
                        PioSendAligner.YSendStart = true;
                        EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T010_ROBOT_PICK_FROM_LPM_END, EFEM_TACT_VALUE.T020_ROBOT_PLACE_TO_ALIGNER_START, LowerWaferKey);
                        if (_efem.Proxy.StartTransRobot(equip, new EFEMTRANSDataSet(EmEfemRobotArm.Lower, EmEfemTransfer.PLACE, EmEfemPort.ALIGNER, 1)) == false) return;
                        _seqStepNum = EmEFEMRobotSeqStep.S530_WAIT_MOVE_WAFER_ROBOT_TO_ALIGNER;
                    }
                    break;
                case EmEFEMRobotSeqStep.S530_WAIT_MOVE_WAFER_ROBOT_TO_ALIGNER:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.TRANS) == true
                        && _efem.Aligner.Status.IsWaferExist == true && Status.IsLowerArmVacOn == false && Status.IsMoving == false)
                    {
                        PioSendAligner.YSendAble = true;
                        PioSendAligner.YSendStart = true;
                        PioSendAligner.YSendComplete = true;
                        EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T020_ROBOT_PLACE_TO_ALIGNER_END, EFEM_TACT_VALUE.T030_ALIGNER_ALIGNMENT_START ,_efem.Aligner.LowerWaferKey);
                        _seqStepNum = EmEFEMRobotSeqStep.S540_WAIT_ALIGNER_RECV_COMPLETE;
                    }
                    break;
                case EmEFEMRobotSeqStep.S540_WAIT_ALIGNER_RECV_COMPLETE:
                    if (PioSendAligner.XRecvAble == true && PioSendAligner.XRecvStart == true && PioSendAligner.XRecvComplete == true)
                    {
                        PioSendAligner.Initailize();
                        _seqStepNum = EmEFEMRobotSeqStep.S550_WAIT_ALIGNER_RECV_SIGNAL_CLEAR;
                    }
                    break;
                case EmEFEMRobotSeqStep.S550_WAIT_ALIGNER_RECV_SIGNAL_CLEAR:
                    if (PioSendAligner.XRecvAble == false && PioSendAligner.XRecvStart == false && PioSendAligner.XRecvComplete == false)
                    {                        
                        _seqStepNum = EmEFEMRobotSeqStep.S560_WAIT_PRE_ALIGN_END;
                    }
                    break;
                //////////////////////////////////////////
                //////////////////////////////////////////
                ///     ALIGN                          ///
                //////////////////////////////////////////
                //////////////////////////////////////////
                case EmEFEMRobotSeqStep.S560_WAIT_PRE_ALIGN_END:
                    if (PioRecvAligner.XSendAble == true)
                    {
                        PioRecvAligner.YRecvAble = true;
                        _seqStepNum = EmEFEMRobotSeqStep.S570_WAIT_ALIGNER_SEND_START;
                    }
                    break;
                case EmEFEMRobotSeqStep.S570_WAIT_ALIGNER_SEND_START:
                    if (PioRecvAligner.XSendAble == true && PioRecvAligner.XSendStart == true)
                    {
                        PioRecvAligner.YRecvAble = true;
                        PioRecvAligner.YRecvStart = true;

                        EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T030_ALIGNER_ALIGNMENT_END, EFEM_TACT_VALUE.T040_ROBOT_PICK_FROM_ALIGNER_START, _efem.Aligner.LowerWaferKey);
                        _isAlignerForcedOut = _efem.Aligner.IsForcedOut;
                        if (_isAlignerForcedOut)
                        {
                            if (_efem.Proxy.StartTransRobot(equip, new EFEMTRANSDataSet(EmEfemRobotArm.Upper, EmEfemTransfer.PICK, EmEfemPort.ALIGNER, 1)) == false) return;
                        }
                        else
                        {
                            if (_efem.Proxy.StartTransRobot(equip, new EFEMTRANSDataSet(EmEfemRobotArm.Lower, EmEfemTransfer.PICK, EmEfemPort.ALIGNER, 1)) == false) return;
                        }
                        _seqStepNum = EmEFEMRobotSeqStep.S580_WAIT_MOVE_WAFER_ALIGNER_TO_ROBOT;
                    }
                    break;
                case EmEFEMRobotSeqStep.S580_WAIT_MOVE_WAFER_ALIGNER_TO_ROBOT:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.TRANS) == true
                        && _efem.Aligner.Status.IsWaferExist == false && Status.IsMoving == false
                        && (_efem.Aligner.IsForcedOut ? Status.IsUpperArmVacOn == true : Status.IsLowerArmVacOn == true)
                        )
                    {
                        PioRecvAligner.YRecvAble = true;
                        PioRecvAligner.YRecvStart = true;
                        PioRecvAligner.YRecvComplete = true;
                        _seqStepNum = EmEFEMRobotSeqStep.S590_WAIT_ALIGNER_SEND_COMPLETE;
                    }
                    break;
                case EmEFEMRobotSeqStep.S590_WAIT_ALIGNER_SEND_COMPLETE:
                    if (PioRecvAligner.XSendAble == true && PioRecvAligner.XSendStart == true && PioRecvAligner.XSendComplete == true)
                    {
                        PioRecvAligner.Initailize();
                        _seqStepNum = EmEFEMRobotSeqStep.S595_WAIT_ALIGNER_SEND_SIGNAL_CLEAR;
                    }
                    break;
                case EmEFEMRobotSeqStep.S595_WAIT_ALIGNER_SEND_SIGNAL_CLEAR:
                    if (PioRecvAligner.XSendAble == false && PioRecvAligner.XSendStart == false && PioRecvAligner.XSendComplete == false)
                    {
                        EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T040_ROBOT_PICK_FROM_ALIGNER_END, LowerWaferKey);

                        if (_isAlignerForcedOut)
                            _seqStepNum = EmEFEMRobotSeqStep.S800_UPPER_HAVE_WAFER;
                        else
                            _seqStepNum = EmEFEMRobotSeqStep.S600_CHECK_AVI_STATUS;                        
                    }
                    break;
            }
        }
        public void AlignEndToAVILogic(Equipment equip)
        {
            SeqLogging(equip);

            switch (_seqStepNum)
            {
                case EmEFEMRobotSeqStep.S600_CHECK_AVI_STATUS:
                    if (PioSendAVI.XRecvAble == false && PioRecvAVI.XSendAble == false)
                    {
                        if (equip.IsWaferDetect == EmGlassDetect.NOT)
                        {
                            _seqStepNum = EmEFEMRobotSeqStep.S620_WAIT_EQUIPMENT_PROC_END;
                        }
                        else
                        {
                            if (_efem.Proxy.StartWaitRobot(equip, new EFEMWAITRDataSet(EmEfemRobotArm.Upper, EmEfemPort.EQUIPMENT, 1)) == false) return;
                            _seqStepNum = EmEFEMRobotSeqStep.S610_WAIT_ROBOT_WAITPOS_MOVE;
                        }
                    }
                    else
                    {
                        _seqStepNum = EmEFEMRobotSeqStep.S620_WAIT_EQUIPMENT_PROC_END;
                    }
                    break;
                case EmEFEMRobotSeqStep.S610_WAIT_ROBOT_WAITPOS_MOVE:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.WAITR) == true && Status.IsMoving == false)
                    {
                        Logger.TacttimeLog.AppendLine(LogLevel.Info, "[EFEM TACT]ROBOT MOVE TO AVI PIO POSITION COMPLETE");
                        _seqStepNum = EmEFEMRobotSeqStep.S620_WAIT_EQUIPMENT_PROC_END;
                    }
                    break;
                case EmEFEMRobotSeqStep.S620_WAIT_EQUIPMENT_PROC_END:
                    if (equip.IsWaferDetect == EmGlassDetect.NOT && PioSendAVI.XRecvAble == true)
                    {
                        PioSendAVI.YSendAble = true;
                        PioSendAVI.YSendStart = true;
                        //EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T050_PREVIOUS_WAFER_WAIT_END, EFEM_TACT_VALUE.T070_WAIT_PREVIOUS_WAFER_PIO_COMPLETE_START, LowerWaferKey);
                        EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T050_PREVIOUS_WAFER_WAIT_START, LowerWaferKey);                        
                        _seqStepNum = EmEFEMRobotSeqStep.S630_START_WAFER_MOVE_ROBOT_TO_AVI;
                    }
                    else if (equip.IsWaferDetect != EmGlassDetect.NOT && PioRecvAVI.XSendAble == true)
                    {
                        //EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T050_PREVIOUS_WAFER_WAIT_END, EFEM_TACT_VALUE.T070_WAIT_PREVIOUS_WAFER_PIO_COMPLETE_START, LowerWaferKey);
                        EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T050_PREVIOUS_WAFER_WAIT_START, LowerWaferKey);
                        _seqStepNum = EmEFEMRobotSeqStep.S655_WAIT_MOVE_WAFER_AVI_TO_ROBOT;
                    }
                    else if (Status.IsLowerArmVacOn == false)
                    {
                        if (_efem.Aligner.Status.IsWaferExist == true
                            && PioRecvAligner.XSendAble == true)
                        {
                            _seqStepNum = EmEFEMRobotSeqStep.S100_CHECK_WAFER_IN_ROBOT;
                        }
                        else if (TargetLoadPort == 0
                            && (PioRecvLpm1.XSendAble == true || PioRecvLpm2.XSendAble == true))
                        {
                            //Lower Vacuum Off && 현재 카세트 완료(TargetLoadPort 초기화 된 상황) && 카세트 준비된 한정적인 상황에서만 
                            _seqStepNum = EmEFEMRobotSeqStep.S100_CHECK_WAFER_IN_ROBOT;
                        }                        
                    }
                    break;
                case EmEFEMRobotSeqStep.S630_START_WAFER_MOVE_ROBOT_TO_AVI:
                    if (PioSendAVI.XRecvAble == true && PioSendAVI.XRecvStart == true
                        && equip.IsReadyToInputArm.IsSolOnOff == true)
                    {
                        //EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T070_WAIT_PREVIOUS_WAFER_PIO_COMPLETE_END, EFEM_TACT_VALUE.T080_PLACE_TO_AVI_START, LowerWaferKey);
                        TactTimeMgr.Instance.Set(EM_TT_LST.T000_PIO_RECEIVE_WAIT_END, EM_TT_LST.T010_PIO_RECEIVE_ACTUAL_START);
                        if (_efem.Proxy.StartTransRobot(equip, new EFEMTRANSDataSet(EmEfemRobotArm.Lower, EmEfemTransfer.PLACE, EmEfemPort.EQUIPMENT, 1)) == false) return;
                        _seqStepNum = EmEFEMRobotSeqStep.S645_WAIT_MOVE_WAFER_ROBOT_TO_AVI;
                    }
                    break;
                case EmEFEMRobotSeqStep.S645_WAIT_MOVE_WAFER_ROBOT_TO_AVI:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.TRANS) == true
                        && (GG.Equip.IsWaferDetect != EmGlassDetect.NOT) && Status.IsLowerArmVacOn == false && Status.IsMoving == false)
                    {
                        PioSendAVI.YSendAble = true;
                        PioSendAVI.YSendStart = true;
                        PioSendAVI.YSendComplete = true;
                        //EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T080_PLACE_TO_AVI_END, EFEM_TACT_VALUE.T090_INSPECTOR_START, equip.TransferUnit.LowerWaferKey);
                        _seqStepNum = EmEFEMRobotSeqStep.S650_WAIT_AVI_RECV_COMPLETE;
                    }
                    break;
                case EmEFEMRobotSeqStep.S650_WAIT_AVI_RECV_COMPLETE:
                    if (PioSendAVI.XRecvAble == true && PioSendAVI.XRecvStart == true && PioSendAVI.XRecvComplete == true)
                    {
                        PioSendAVI.Initailize();
                        _seqStepNum = EmEFEMRobotSeqStep.S640_WAIT_AVI_RECV_ABLE_OFF;
                    }
                    break;
                case EmEFEMRobotSeqStep.S640_WAIT_AVI_RECV_ABLE_OFF:
                    if (PioSendAVI.XRecvAble == false && PioSendAVI.XRecvStart == false && PioSendAVI.XRecvComplete == false)
                    {
                        //EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T070_WAIT_PREVIOUS_WAFER_PIO_COMPLETE_END, EFEM_TACT_VALUE.T080_PLACE_TO_AVI_START, LowerWaferKey);
                        Logger.TacttimeLog.AppendLine(LogLevel.Info, "[EFEM TACT]ROBOT TO AVI PIO COMPLETE------------------");
                        _seqStepNum = EmEFEMRobotSeqStep.S100_CHECK_WAFER_IN_ROBOT;
                    }
                    break;
            }
        }
        private PlcTimerEx _exchDelay = new PlcTimerEx("", false);
        public void AVIToRobotLogic(Equipment equip)
        {
            SeqLogging(equip);

            switch (_seqStepNum)
            {
                case EmEFEMRobotSeqStep.S655_WAIT_MOVE_WAFER_AVI_TO_ROBOT:
                    if (PioRecvAVI.XSendAble == true)
                    {
                        PioRecvAVI.YRecvAble = true;
                        PioRecvAVI.YRecvStart = true;
                        _seqStepNum = EmEFEMRobotSeqStep.S660_WAIT_AVI_RECV_START;
                    }
                    break;
                case EmEFEMRobotSeqStep.S660_WAIT_AVI_RECV_START:
                    if (PioRecvAVI.XSendAble == true && PioRecvAVI.XSendStart == true)
                    {
                        //EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T095_WAIT_ROBOT_PICK_END, EFEM_TACT_VALUE.T100_PICK_FROM_AVI_START, equip.TransferUnit.LowerWaferKey);
                        //EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T050_PREVIOUS_WAFER_WAIT_END, EFEM_TACT_VALUE.T070_WAIT_PREVIOUS_WAFER_PIO_COMPLETE_START, LowerWaferKey);
                        if (_efem.Proxy.StartTransRobot(equip, new EFEMTRANSDataSet(EmEfemRobotArm.Upper, EmEfemTransfer.PICK, EmEfemPort.EQUIPMENT, 1)) == false) return;
                        Logger.TacttimeLog.AppendLine(LogLevel.Info, "[EFEM TACT]AVI TO ROBOT PIO START");
                        _seqStepNum = EmEFEMRobotSeqStep.S670_WAIT_MOVE_WAFER_AVI_TO_ROBOT;
                    }
                    break;
                case EmEFEMRobotSeqStep.S670_WAIT_MOVE_WAFER_AVI_TO_ROBOT:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.TRANS) == true
                        && Status.IsUpperArmVacOn == true && (equip.IsWaferDetect == EmGlassDetect.NOT) && Status.IsMoving == false)
                    {
                        PioRecvAVI.YRecvAble = true;
                        PioRecvAVI.YRecvStart = true;
                        PioRecvAVI.YRecvComplete = true;

                        //EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T100_PICK_FROM_AVI_END, EFEM_TACT_VALUE.T110_WAIT_NEXT_WAFER_PIO_COMPLETE_START, UpperWaferKey);
                        //EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T070_WAIT_PREVIOUS_WAFER_PIO_COMPLETE_END, EFEM_TACT_VALUE.T080_PLACE_TO_AVI_START, LowerWaferKey);
                        _seqStepNum = EmEFEMRobotSeqStep.S680_WAIT_AVI_SEND_COMPLETE;
                    }
                    break;
                case EmEFEMRobotSeqStep.S680_WAIT_AVI_SEND_COMPLETE:
                    if (PioRecvAVI.XSendAble == true && PioRecvAVI.XSendStart == true && PioRecvAVI.XSendComplete == true)
                    {
                        PioRecvAVI.Initailize();
                        _seqStepNum = EmEFEMRobotSeqStep.S690_WAIT_AVI_SEND_SIGNAL_CLEAR;
                    }
                    break;
                case EmEFEMRobotSeqStep.S690_WAIT_AVI_SEND_SIGNAL_CLEAR:
                    if (PioRecvAVI.XSendAble == false && PioRecvAVI.XSendStart == false && PioRecvAVI.XSendComplete == false)
                    {
                        if (Status.IsLowerArmVacOn == true)
                        {
                            _exchDelay.Start(0, 500);
                            _seqStepNum = EmEFEMRobotSeqStep.S700_WAIT_DELAY;
                        }                        
                        else
                            _seqStepNum = EmEFEMRobotSeqStep.S800_UPPER_HAVE_WAFER;
                    }
                    break;
                case EmEFEMRobotSeqStep.S700_WAIT_DELAY:
                    if (_exchDelay)
                    {
                        _seqStepNum = EmEFEMRobotSeqStep.S710_AVI_RECV_WAIT;
                    }
                    break;
                case EmEFEMRobotSeqStep.S710_AVI_RECV_WAIT:
                    if (PioSendAVI.XRecvAble == true)
                    {
                        PioSendAVI.YSendAble = true;
                        PioSendAVI.YSendStart = true;
                        _seqStepNum = EmEFEMRobotSeqStep.S720_WAIT_AVI_RECV_START;
                    }
                    break;
                case EmEFEMRobotSeqStep.S720_WAIT_AVI_RECV_START:
                    if (PioSendAVI.XRecvAble == true && PioSendAVI.XRecvStart == true)
                    {
                        //EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T070_WAIT_PREVIOUS_WAFER_PIO_COMPLETE_END, EFEM_TACT_VALUE.T080_PLACE_TO_AVI_START, LowerWaferKey);
                        TactTimeMgr.Instance.Set(EM_TT_LST.T000_PIO_RECEIVE_WAIT_END, EM_TT_LST.T010_PIO_RECEIVE_ACTUAL_START);
                        if (_efem.Proxy.StartTransRobot(equip, new EFEMTRANSDataSet(EmEfemRobotArm.Lower, EmEfemTransfer.PLACE, EmEfemPort.EQUIPMENT, 1)) == false) return;                        
                        _seqStepNum = EmEFEMRobotSeqStep.S730_WAIT_WAFER_MOVE_ROBOT_TO_AVI;
                    }
                    break;
                case EmEFEMRobotSeqStep.S730_WAIT_WAFER_MOVE_ROBOT_TO_AVI:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.TRANS) == true
                        && (GG.Equip.IsWaferDetect != EmGlassDetect.NOT) && Status.IsLowerArmVacOn == false && Status.IsMoving == false)
                    {
                        PioSendAVI.YSendAble = true;
                        PioSendAVI.YSendStart = true;
                        PioSendAVI.YSendComplete = true;                        
                        EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T110_WAIT_NEXT_WAFER_PIO_COMPLETE_END, EFEM_TACT_VALUE.T120_ROBOT_PLACE_TO_LPM_START, UpperWaferKey);
                        //EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T080_PLACE_TO_AVI_END ,EFEM_TACT_VALUE.T090_INSPECTOR_START, equip.TransferUnit.LowerWaferKey);
                        _seqStepNum = EmEFEMRobotSeqStep.S740_WAIT_AVI_RECV_COMPLETE;
                    }
                    break;
                case EmEFEMRobotSeqStep.S740_WAIT_AVI_RECV_COMPLETE:
                    if (PioSendAVI.XRecvAble == true && PioSendAVI.XRecvStart == true && PioSendAVI.XRecvComplete == true)
                    {
                        PioSendAVI.Initailize();
                        _seqStepNum = EmEFEMRobotSeqStep.S760_WAIT_AVI_RECV_SIGNAL_CLEAR;
                    }
                    break;
                case EmEFEMRobotSeqStep.S760_WAIT_AVI_RECV_SIGNAL_CLEAR:
                    if (PioSendAVI.XRecvAble == false && PioSendAVI.XRecvStart == false && PioSendAVI.XRecvComplete == false)
                    {
                        _seqStepNum = EmEFEMRobotSeqStep.S800_UPPER_HAVE_WAFER;
                    }
                    break;
            }
        }

        public void RobotToLPMLogic(Equipment equip)
        {
            SeqLogging(equip);

            int slot;
            int loadPort;
            PioSignalSend pioSend;

            switch (_seqStepNum)
            {

                case EmEFEMRobotSeqStep.S300_HAVE_TWO_WAFER:
                case EmEFEMRobotSeqStep.S800_UPPER_HAVE_WAFER:                 
                    _lpmReadyWait.Stop();

                    GG.WaferTransfer_RobotToLPM = false; // KYH 230914
                    if (equip.IsForcedComeback)
                    {
                        _seqStepNum = EmEFEMRobotSeqStep.S805_WAIT_LPM_RECV_ABLE;
                    }
                    else
                    {
                        // 230912 KYH : AutoMoveOut 체크 추가로 시퀀스 변경됨
                        if (GG.Equip.CtrlSetting.ReviewJudgeMode)
                        {
                            LastUnloadedWaferKey = (WaferInfoKey)this.UpperWaferKey.Clone();
                            _seqStepNum = EmEFEMRobotSeqStep.S801_CHACK_AUTOMOVEOUT;
                        }
                        else
                        {
                            _seqStepNum = EmEFEMRobotSeqStep.S805_WAIT_LPM_RECV_ABLE;
                        }
                    }
                    break;

                // 230912 KYH : AutoMoveOut 체크 추가로 시퀀스 추가됨
                case EmEFEMRobotSeqStep.S801_CHACK_AUTOMOVEOUT:
                    if (!GG.AutoMoveOutReceive) break;
                    GG.AutoMoveOutReceive = false; // KYH AutoMoveOutReceive Clear 230913 : AutoMoveOutReceive Reset
                    Logger.Log.AppendLine(LogLevel.Info, string.Format("[S801_CHACK_AUTOMOVEOUT] AutoMoveOutReceive : Pass & Reset"));

                    _seqStepNum = EmEFEMRobotSeqStep.S805_WAIT_LPM_RECV_ABLE;
                    break;

                case EmEFEMRobotSeqStep.S805_WAIT_LPM_RECV_ABLE:
                    CassetteInfo cstinfoOfUpperkey = TransferDataMgr.GetCst(this.UpperWaferKey);
                    if (cstinfoOfUpperkey == null)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0660_NO_WAFER_INFO);
                        equip.IsInterlock = true;
                        return;
                    }
                    loadPort = cstinfoOfUpperkey.LoadPortNo;
                    pioSend = loadPort == 2 ? PioSendLpm2 : PioSendLpm1;

                    if (pioSend.XRecvAble == true)
                    {
                        if (GG.EfemLongRun == false)
                        {
                            if (loadPort == 1)
                            {
                                if (cstinfoOfUpperkey.CstID != equip.Efem.LoadPort1.CstKey.ID)
                                {
                                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0657_LPM1_ID_NOT_MATCH_ROBOT_UPPER);
                                    this.RunMode = EmEfemRunMode.Pause;
                                    equip.IsPause = true;
                                }
                            }
                            else if (loadPort == 2)
                            {
                                if (cstinfoOfUpperkey.CstID != equip.Efem.LoadPort2.CstKey.ID)
                                {
                                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0658_LPM2_ID_NOT_MATCH_ROBOT_UPPER);
                                    this.RunMode = EmEfemRunMode.Pause;
                                    equip.IsPause = true;
                                }
                            }
                        }

                        pioSend.YSendAble = true;
                        EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T110_WAIT_NEXT_WAFER_PIO_COMPLETE_END, EFEM_TACT_VALUE.T120_ROBOT_PLACE_TO_LPM_START, UpperWaferKey);
                        _seqStepNum = EmEFEMRobotSeqStep.S810_WAIT_LPM_RECV_START;
                    }
                    else if (_lpmReadyWait.IsStart == false)
                        _lpmReadyWait.Start(600, 0); // LPM에 Wafer투입하려고 대기 중
                    else if (TargetLoadPort != 0
                        && (_efem.LoadPort((EmEfemPort)TargetLoadPort).IsLdFoupWaitStep
                        || _efem.LoadPort((EmEfemPort)TargetLoadPort).IsLdButtonWaitStep
                        || _efem.LoadPort((EmEfemPort)TargetLoadPort).IsUserInterfaceWaitStep)
                        )
                        _lpmReadyWait.Stop();
                    else if (_lpmReadyWait)
                    {
                        _lpmReadyWait.Stop();
                        if (TargetLoadPort == 2)
                            AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0668_LPM2_READY_WAIT_OVERTIME);
                        else if (TargetLoadPort == 1)
                            AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0667_LPM1_READY_WAIT_OVERTIME);
                        else
                            AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0669_TARGETPORT_ABNORMAL);
                    }
                    break;
                case EmEFEMRobotSeqStep.S810_WAIT_LPM_RECV_START:
                    loadPort = TransferDataMgr.GetCst(this.UpperWaferKey).LoadPortNo;
                    pioSend = loadPort == 2 ? PioSendLpm2 : PioSendLpm1;
                    if (pioSend.XRecvAble == true && pioSend.XRecvStart == true && GG.Equip.Efem.LoadPort((EmEfemPort)loadPort).Status.IsDoorOpen)
                    {
                        pioSend.YSendAble = true;
                        pioSend.YSendStart = true;                        
                        _seqStepNum = EmEFEMRobotSeqStep.S811_CHECK_LIGHT_CURTAIN_OFF;
                    }
                    break;
                case EmEFEMRobotSeqStep.S811_CHECK_LIGHT_CURTAIN_OFF:
                    if (GG.PassLightCurtain || _efem.LPMLightCurtain.Detect.IsOn == false) //라이트커튼 제거
                    {
                        _unldPort = TransferDataMgr.GetCst(this.UpperWaferKey).LoadPortNo;
                        pioSend = _unldPort == 2 ? PioSendLpm2 : PioSendLpm1;
                        slot = TransferDataMgr.GetWafer(this.UpperWaferKey).SlotNo;
                        _tempWaferInfo = TransferDataMgr.GetWafer(this.UpperWaferKey);

                        if (_unldPort == 1)
                        {
                            if(equip.Efem.LoadPort1.Status.IsDoorOpen == false)
                            {
                                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0634_LOADPORT1_DOOR_CLOSE);
                            }
                        }
                        else if (_unldPort == 2)
                        {
                            if (equip.Efem.LoadPort2.Status.IsDoorOpen == false)
                            {
                                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0635_LOADPORT2_DOOR_CLOSE);
                            }
                        }
                        else if (TransferDataMgr.GetWafer(this.UpperWaferKey).IsComeBack == true)
                        {
                            AlarmMgr.Instance.Happen(equip, _unldPort == 1 ? EM_AL_LST.AL_0653_LPM1_SLOT_ABNORMAL : EM_AL_LST.AL_0654_LPM2_SLOT_ABNORMAL);
                            equip.IsInterlock = true;
                        }

                        LastUnloadedWaferKey = (WaferInfoKey)UpperWaferKey.Clone();

                        if (_efem.Proxy.StartTransRobot(equip, new EFEMTRANSDataSet(EmEfemRobotArm.Upper, EmEfemTransfer.PLACE, _unldPort == 2 ? EmEfemPort.LOADPORT2 : EmEfemPort.LOADPORT1, slot)) == false) return;
                        _tempWaferInfo.EndTime = DateTime.Now;
                        _tempWaferInfo.Update();

                        if (equip.HsmsPc.StartCommand(equip, HSMS.EmHsmsPcCommand.WAFER_UNLOAD, UpperWaferKey) == false) return;
                        
                        _seqStepNum = EmEFEMRobotSeqStep.S815_WAIT_MOVE_WAFER_UPPER_TO_LPM;
                    }
                    break;

                case EmEFEMRobotSeqStep.S815_WAIT_MOVE_WAFER_UPPER_TO_LPM:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.TRANS) == true && Status.IsUpperArmVacOn == false && Status.IsMoving == false)
                    {
                        if (_unldPort == 0) throw new Exception("S815_WAIT_MOVE_WAFER_UPPER_TO_LPM WaferInfo ERROR");

                        pioSend = _unldPort == 2 ? PioSendLpm2 : PioSendLpm1;
                        pioSend.YSendAble = true;
                        pioSend.YSendStart = true;
                        pioSend.YSendComplete = true;
                        EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T120_ROBOT_PLACE_TO_LPM_END, UpperWaferKey);

                        if (_unldPort == 2)
                        {
                            if (TransferDataMgr.IsAllComeBack(_efem.LoadPort2.CstKey) == true)
                                SetTargetLoadPort(0);
                            if (TransferDataMgr.NextWaferKey(_efem.LoadPort2.CstKey.ID) == null)
                                SetTargetLoadPort(0);

                            //EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T130_LPM_CLOSE_START, _efem.LoadPort2.CstKey.ID);
                            _seqStepNum = EmEFEMRobotSeqStep.S825_WAIT_LPM2_RECV_COMPLETE;
                        }
                        else if (_unldPort == 1)
                        {
                            if (TransferDataMgr.IsAllComeBack(_efem.LoadPort1.CstKey) == true)
                                SetTargetLoadPort(0);
                            if (TransferDataMgr.NextWaferKey(_efem.LoadPort1.CstKey.ID) == null)
                                SetTargetLoadPort(0);

                            //EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T130_LPM_CLOSE_START, _efem.LoadPort1.CstKey.ID);
                            _seqStepNum = EmEFEMRobotSeqStep.S820_WAIT_LPM1_RECV_COMPLETE;
                        }   
                    }
                    break;
                case EmEFEMRobotSeqStep.S820_WAIT_LPM1_RECV_COMPLETE:
                    if (PioSendLpm1.XRecvAble == true && PioSendLpm1.XRecvStart == true && PioSendLpm1.XRecvComplete == true)
                    {
                        PioSendLpm1.Initailize();
                        _seqStepNum = EmEFEMRobotSeqStep.S821_WAIT_LPM1_RECV_OFF;
                    }
                    break;
                case EmEFEMRobotSeqStep.S821_WAIT_LPM1_RECV_OFF:
                    if (PioSendLpm1.XRecvAble == false && PioSendLpm1.XRecvStart == false && PioSendLpm1.XRecvComplete == false)
                    {
                        GG.WaferTransfer_RobotToLPM = true; // KYH 230914
                        _seqStepNum = EmEFEMRobotSeqStep.S100_CHECK_WAFER_IN_ROBOT;
                    }
                    break;

                case EmEFEMRobotSeqStep.S825_WAIT_LPM2_RECV_COMPLETE:
                    if (PioSendLpm2.XRecvAble == true && PioSendLpm2.XRecvStart == true && PioSendLpm2.XRecvComplete == true)
                    {
                        PioSendLpm2.Initailize();
                        _seqStepNum = EmEFEMRobotSeqStep.S826_WAIT_LPM2_RECV_OFF;
                    }
                    break;
                case EmEFEMRobotSeqStep.S826_WAIT_LPM2_RECV_OFF:
                    if (PioSendLpm2.XRecvAble == false && PioSendLpm2.XRecvStart == false && PioSendLpm2.XRecvComplete == false)
                    {
                        GG.WaferTransfer_RobotToLPM = true; // KYH 230914
                        _seqStepNum = EmEFEMRobotSeqStep.S100_CHECK_WAFER_IN_ROBOT;
                    }
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">0 = none, 1 = LoadPort1, 2 = LoadPort2</param>
        public void SetTargetLoadPort(int target)
        {
            TargetLoadPort = target;
        }
        protected override void Stop()
        {
            PioRecvLpm1.Initailize();
            PioRecvLpm2.Initailize();
            PioRecvAVI.Initailize();
            PioRecvAligner.Initailize();
            PioSendLpm1.Initailize();
            PioSendLpm2.Initailize();
            PioSendAVI.Initailize();
            PioSendAligner.Initailize();
            base.Stop();
        }
        protected override bool IsSafeToHome()
        {
            return true;
        }
    }
}
