using EquipMainUi.Monitor;
using EquipMainUi.RecipeManagement;
using EquipMainUi.Setting.TransferData;
using EquipMainUi.Struct.Detail.HSMS.ReportStruct;
using EquipMainUi.Struct.Detail.OHT;
using EquipMainUi.Struct.Step;
using EquipMainUi.Struct.TransferData;
using EquipMainUi.Tact;
using EquipMainUi.UserMessageBoxes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EquipMainUi.Struct.Detail.EFEM.Step
{
    public enum EmEFEMLPMHomeStep
    {
        H000_END,
        H010_BUSY_CHECK,
        H015_WAIT_LPM_TO_NOT_BUSY,
        H020_WAIT_LPM_STOP_COMPLETE,
        H030_INIT_LPM,
        H040_WAIT_LPM_RESET,
        H050_WAIT_LPM_INIT,
        H060_CHECK_LPM_STATUS,
        H100_HOME_COMPLETE,
    }
    public enum EmEFEMLPMSeqStep
    {
        S000_END,
        S010_CHECK_EXIST,
        S100_RECV_FOUP,
        S110_WAIT_LOAD_BUTTON_PUSH,
        S120_Muting_ON_WAIT,
        S130_WAIT_LOAD_BUTTON_PUSH,
        S150_CHECK_OTHER_LOADPORT_PROGRESSING,

        S200_START_OHT_COMMUNICATION,
        S210_WAIT_CST_OHT_LOAD,

        S300_LOAD_FOUP,
        S310_READ_RF_DELAY,
        S315_READ_RF_ID,
        S320_WAIT_READ_RF_ID,
        S330_RFID_USER_CHECK,
        S340_FIRST_CST_INFO_CHECK,
        S350_RETRY_CST_INFO_CHECK,
        S355_RETRY_CST_PROGRESS,
        S360_WAIT_CST_INFO_INPUT,
        S370_BEFORE_OPEN_READY,

        S380_OPEN_READY,

        S390_CST_LOAD_REPORT,
        S400_CST_LOAD_CONFIRM,
        S410_CST_LOAD_CONFIRM_WAIT,
        S420_CST_START_CMD_WAIT,
        S430_PP_SELECT_WAIT_VERIOITY_OK_WAIT,

        S440_START_OPEN,
        S450_WAIT_LPM_LOAD,
        S460_WAIT_LPM_MAPPING,
        S470_VERIFY_LPM_MAPPING,
        S480_HOST_DATA_COMPARE,
        S490_APPLY_USER_MAP_TO_OLD_CST,
        S500_PROCESSING,

        S510_CST_LOT_START,
        S520_LOT_START_CONFIRM_WAIT,

        S605_USER_CONFIRM_NEXT_WAFER,

        S530_SEND_RECV_ABLE_ON,
        S535_REVIEW_JUDGE_ON,
        S540_WAIT_ROBOT_SEND_RECV_ABLE,
        S550_WAIT_ROBOT_SEND_COMPLETE,
        S560_WAIT_RECV_SIGNAL_CLEAR,
        S570_WAIT_ROBOT_RECV_COMPLETE,
        S580_WAIT_SEND_SIGNAL_CLEAR,
        S590_WAFER_IN_OUT_DELAY,
        S600_DELAY_WAIT,

        S610_UNLD_FOUP,
        S620_WAIT_LPM_CLOSE,
        S630_CHECK_CST_ID,
        S640_CASSETTE_UNLOAD_REPORT,

        S645_CASSETTE_UNLOAD_REPORT_COMPLETE_WAIT,
        S650_WAIT_UNLD_BUTTON_PUSH,
        S660_NOTIFY_USER,
        S670_WAIT_UNLD_BUTTON_PUSH,
        S680_WAIT_UNLD_FOUP,

        S690_OHT_ULD_COMMUNICATION_START,
        S700_OHT_ULD_COMPLETE_WAIT,
    }
    public class EFEMLPMUnit : StepBase
    {
        private EFEMUnit _efem;

        private EmEfemPort _port;
        public EmEfemPort Port { get { return _port; } }

        private EmEFEMLPMHomeStep _homeStepNum = 0;
        private EmEFEMLPMHomeStep _homeStepNumOld = 0;
        public EmEFEMLPMHomeStep HomeStepNum { get { return _homeStepNum; } }

        private EmEFEMLPMSeqStep _seqStepNum = 0;
        private EmEFEMLPMSeqStep _seqStepNumOld = 0;
        public EmEFEMLPMSeqStep SeqStepNum { get { return _seqStepNum; } }

        public bool IsProcessingStep { get { return EmEFEMLPMSeqStep.S500_PROCESSING <= _seqStepNum && _seqStepNum <= EmEFEMLPMSeqStep.S600_DELAY_WAIT; } }
        public bool IsCstUnloadReportComplete { get {return _seqStepNum < EmEFEMLPMSeqStep.S390_CST_LOAD_REPORT || _seqStepNum >= EmEFEMLPMSeqStep.S690_OHT_ULD_COMMUNICATION_START; } }
        public bool IsStandByRunState
        {
            get
            {
                if (this.LoadType == EmLoadType.Manual)
                    return EmEFEMLPMSeqStep.S110_WAIT_LOAD_BUTTON_PUSH == _seqStepNum;
                else
                    return EmEFEMLPMSeqStep.S210_WAIT_CST_OHT_LOAD == _seqStepNum || EmEFEMLPMSeqStep.S200_START_OHT_COMMUNICATION == _seqStepNum;
            }
        }
        public bool IsLdButtonWaitStep { get { return _seqStepNum == EmEFEMLPMSeqStep.S110_WAIT_LOAD_BUTTON_PUSH; } }
        public bool IsLdFoupWaitStep { get { return _seqStepNum == EmEFEMLPMSeqStep.S130_WAIT_LOAD_BUTTON_PUSH; } }
        public bool IsUnldButtonWaitStep { get { return _seqStepNum == EmEFEMLPMSeqStep.S650_WAIT_UNLD_BUTTON_PUSH; } }
        public bool IsRemoveFoupWaitStep { get { return _seqStepNum == EmEFEMLPMSeqStep.S670_WAIT_UNLD_BUTTON_PUSH || _seqStepNum == EmEFEMLPMSeqStep.S680_WAIT_UNLD_FOUP; } }
        public bool IsWaitRobotStep { get { return _seqStepNum == EmEFEMLPMSeqStep.S540_WAIT_ROBOT_SEND_RECV_ABLE; } }
        public bool IsUserInterfaceWaitStep
        {
            get
            {
                return _seqStepNum == EmEFEMLPMSeqStep.S330_RFID_USER_CHECK
                    || _seqStepNum == EmEFEMLPMSeqStep.S360_WAIT_CST_INFO_INPUT
                    ;
            }
        }
        public bool IsOpenReadyStep { get { return _seqStepNum == EmEFEMLPMSeqStep.S380_OPEN_READY; } }
        public bool IsOpenningStep
        {
            get
            {
                return _seqStepNum == EmEFEMLPMSeqStep.S440_START_OPEN
                    || _seqStepNum == EmEFEMLPMSeqStep.S450_WAIT_LPM_LOAD
                    || _seqStepNum == EmEFEMLPMSeqStep.S460_WAIT_LPM_MAPPING;
            }
        }

        public bool NextWaferSlot = false;
        public int LastSlotNo = -1;
        public string DeepLearningReviewJudge = "";
        public string CSTID_Clone = "";

        public List<EmEFEMLPMSeqStep> LstCycleStopStep = new List<EmEFEMLPMSeqStep>();

        public CassetteInfoKey CstKey { get; set; }

        public EmLoadType LoadType { get; set; }
        public EmProgressWay ProgressWay { get; set; }

        // KYH 230913-01 : ReviewFailCount, AutoMoveOutCount는 포트별로 가져야 한다.
        public int ReviewFailCount = 0;
        public int AutoMoveOutCount = 0;

        private bool _beginToOpen;
        public int _InspCount;
        public bool BeginToOpen { get { return _beginToOpen; } }
        private bool _isZeroCst;
        private bool _isPassMutingCheckAtLoad;
        private bool _goReadyCst = false;
        public void GoReadyCst()
        {
            _goReadyCst = true;
        }

        private PlcTimerEx _unldDelay = new PlcTimerEx("Unld Delay", false);
        private PlcTimerEx _ldDelay = new PlcTimerEx("Ld Delay", false);

        private PlcTimerEx _LoadConfirmDelay = new PlcTimerEx("Load Confirm Delay", false);
        private PlcTimerEx _PPSelcetDelay = new PlcTimerEx("PP Select Delay", false);
        private PlcTimerEx _LoadStartCmdDelay = new PlcTimerEx("Load Start Cmd Delay", false);
        private PlcTimerEx _LotStartCmdDelay = new PlcTimerEx("Lot Start Confirm Delay", false);

        private PlcTimerEx _UnloadReportDelay = new PlcTimerEx("Unload  Request Delay");

        private PlcTimerEx _OHTRequestDely = new PlcTimerEx("Load/Unload Request Delay");

        private PlcTimerEx _DeepLearningReviewCompleteDelay = new PlcTimerEx("Deep Learning Review Complete Delay");
        private PlcTimerEx _rfReadDelay = new PlcTimerEx("_rfReadDelay");

        public string _rfReadCstId { get; private set; }
        private Timer _popupTimer = new Timer();
        private FrmUserSelectReadData _frmRfUserMsgBox;
        private FrmCstDataUserInput _frmCstDataInput;
        private FrmRetryUserSelect _frmRetryMsg;
        private FrmVerifyMapping _frmVerifyMapping;
        private List<WaferInfo> _inputWaferInfo = new List<WaferInfo>();
        public Monitor.RFIDController RFR;

        private bool IsAlreadyOpen = false;

        public void InitUserInterface(Equipment equip)
        {
            _frmRfUserMsgBox = new FrmUserSelectReadData(equip, Port, RFR);
            _frmVerifyMapping = new FrmVerifyMapping(Port.ToString());
            _frmCstDataInput = new FrmCstDataUserInput(equip, Port, RFR);
            _frmRetryMsg = new FrmRetryUserSelect("다음 장 진행", "카세트 배출");

            _popupTimer.Tick += _popupTimer_Tick;
            _popupTimer.Interval = 1000;
            _popupTimer.Start();
        }

        private void _popupTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_frmRfUserMsgBox.PopupFlow == EmPopupFlow.PopupRequest)
                {
                    _frmRfUserMsgBox.StartPosition = FormStartPosition.CenterScreen;
                    _frmRfUserMsgBox.PopupFlow = EmPopupFlow.UserWait;
                    _frmRfUserMsgBox.Show();
                }

                if (_frmVerifyMapping.PopupFlow == EmPopupFlow.PopupRequest)
                {
                    _frmVerifyMapping.StartPosition = FormStartPosition.CenterScreen;
                    _frmVerifyMapping.PopupFlow = EmPopupFlow.UserWait;
                    _frmVerifyMapping.Show();
                }
                if (_frmRetryMsg.PopupFlow == EmPopupFlow.PopupRequest)
                {
                    _frmRetryMsg.StartPosition = FormStartPosition.CenterScreen;
                    _frmRetryMsg.PopupFlow = EmPopupFlow.UserWait;
                    _frmRetryMsg.Show();
                }
                if (_frmCstDataInput.PopupFlow == EmPopupFlow.PopupRequest)
                {
                    _frmCstDataInput.StartPosition = FormStartPosition.CenterScreen;
                    _frmCstDataInput.PopupFlow = EmPopupFlow.UserWait;
                    try
                    {
                        _frmCstDataInput.Show();
                    }
                    catch (Exception ex)
                    {
                        Logger.ExceptionLog.AppendLine(LogLevel.NoLog, "FRM CST SHOW EXCEPTION");
                    }
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

        public List<WaferInfo> wafers
        {
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    if (i == 13)
                        break;
                    Status.MappingData[i] = value[i].Status;
                }

            }
        }

        public WaferInfoKey UpdateNextWaferKey()
        {
            if (CstKey == null)
                return null;
            LowerWaferKey = TransferDataMgr.NextWaferKey(CstKey.ID);
            return LowerWaferKey;
        }
        public WaferInfoKey UpdateNextWaferKey(int slotNo)
        {
            if (CstKey == null)
                return new WaferInfoKey() { CstID = "", SlotNo = slotNo };
            LowerWaferKey = new WaferInfoKey() { CstID = CstKey.ID, SlotNo = slotNo };
            return LowerWaferKey;
        }
        public List<WaferInfo> CurrentWafers()
        {
            if (CstKey == null)
                return null;
            return TransferDataMgr.CurrentProgressingWafer(CstKey.ID);
        }

        public EFEMLoadPortStatus Status { get { return _port == EmEfemPort.LOADPORT2 ? _efem.Proxy.LoadPort2Stat : _efem.Proxy.LoadPort1Stat; } }
        public PioOHTStep OHTpio { get { return _port == EmEfemPort.LOADPORT1 ? GG.Equip.PioLPM1 : GG.Equip.PioLPM2; } }
        private Dictionary<EmEfemLampType, LampCheck> _changeLampDone;
        private Queue<EFEMLPLEDDataSet> _lpledQue;
        private PlcTimerEx _waferLdWait = new PlcTimerEx("WaferLdTimer");
        private PlcTimerEx _waferUnldWait = new PlcTimerEx("WaferUnldTimer");
        private PlcTimerEx _RFIDReadWait = new PlcTimerEx("RFIDReadTimer");

        public bool IsFoupLoadComplete
        {
            get
            {
                return Status.IsDoorOpen == true
                    && Status.IsFoupDetected == true
                    && Status.IsFoupExist == true
                    && Status.IsWaferStickOut == false
                    ;
            }
        }

        public bool IsInitDone
        {
            get
            {
                return Status.IsError == false
                    //&& Status.IsBusy == false
                    && Status.IsAuto == true
                    //&& Status.IsHomeComplete == true
                    && Status.IsMappingSensorUse == true
                    ;
            }
        }

        public EFEMLPMUnit(EFEMUnit efem, EmEfemPort port)
        {
            if (port != EmEfemPort.LOADPORT1
                && port != EmEfemPort.LOADPORT2)
                throw new Exception("[CODE] PORT ERROR");

            _efem = efem;
            _port = port;

            PioRecv = new PioSignalRecv() { Name = string.Format("{0}-Robot-Recv", port.ToString()) };
            PioSend = new PioSignalSend() { Name = string.Format("{0}-Robot-Send", port.ToString()) };

            _changeLampDone = new Dictionary<EmEfemLampType, LampCheck>();
            _changeLampDone[EmEfemLampType.LOAD_LAMP] = new LampCheck() { Type = EmEfemLampType.LOAD_LAMP };
            _changeLampDone[EmEfemLampType.RESERVE_LAMP] = new LampCheck() { Type = EmEfemLampType.RESERVE_LAMP };
            _changeLampDone[EmEfemLampType.UNLOAD_LAMP] = new LampCheck() { Type = EmEfemLampType.UNLOAD_LAMP };
            _lpledQue = new Queue<EFEMLPLEDDataSet>();

            LstCycleStopStep.Add(EmEFEMLPMSeqStep.S000_END);
        }

        public override void LogicWorking(Equipment equip)
        {
            StatusLogicWorking(equip);
            SeqLogicWorking(equip);
        }

        public override void StatusLogicWorking(Equipment equip)
        {
            GetStat(equip);
            FrontLampLogic(equip);
            if (IsProcessingStep == true && Status.IsFoupExist == false)
            {
                AlarmMgr.Instance.Happen(equip, Port == EmEfemPort.LOADPORT1 ? EM_AL_LST.AL_0871_LPM1_CASSETTE_REMOVED : EM_AL_LST.AL_0872_LPM2_CASSETTE_REMOVED);
                equip.IsInterlock = true;
            }
        }
        private PlcTimerEx _lpledDelay2 = new PlcTimerEx("", false);

        private void FrontLampLogic(Equipment equip)
        {
            //if (this.RunMode == EmEfemRunMode.Start)
            //    ChangeLed(EmEfemLampType.AUTO_LAMP, EmEfemLampBuzzerState.ON);
            //else if (this.RunMode == EmEfemRunMode.Home || this.RunMode == EmEfemRunMode.CycleStop)
            //    ChangeLed(EmEfemLampType.AUTO_LAMP, EmEfemLampBuzzerState.BLINK);
            //else
            //    ChangeLed(EmEfemLampType.AUTO_LAMP, EmEfemLampBuzzerState.OFF);

            if (IsOpenReadyStep)
            {
                if(_lpledDelay2.IsStart == false)
                {
                    _lpledDelay2.Start(0, 1000);
                }
                else
                {
                    if(_lpledDelay2)
                    {
                        ChangeLed(EmEfemLampType.RESERVE_LAMP, EmEfemLampBuzzerState.BLINK);
                    }
                }
            }
                
            else
            {
                ChangeLed(EmEfemLampType.RESERVE_LAMP, EmEfemLampBuzzerState.OFF);
                _lpledDelay2.Stop();
            }
                

            if (IsLdButtonWaitStep || IsLdFoupWaitStep)
            {
                ChangeLed(EmEfemLampType.LOAD_LAMP, EmEfemLampBuzzerState.BLINK);
                ChangeLed(EmEfemLampType.UNLOAD_LAMP, EmEfemLampBuzzerState.OFF);
            }

            if (IsUnldButtonWaitStep || IsRemoveFoupWaitStep)
            {
                ChangeLed(EmEfemLampType.UNLOAD_LAMP, EmEfemLampBuzzerState.BLINK);
                ChangeLed(EmEfemLampType.LOAD_LAMP, EmEfemLampBuzzerState.OFF);
            }

            if (IsHomeComplete == true)
                ChangeLampLogic(equip);

            _changeLampDone[EmEfemLampType.LOAD_LAMP].StateCheckLogic(this);
            _changeLampDone[EmEfemLampType.UNLOAD_LAMP].StateCheckLogic(this);
            _changeLampDone[EmEfemLampType.RESERVE_LAMP].StateCheckLogic(this);
        }

        Dit.Framework.PeriodChecker _statPeriod = new Dit.Framework.PeriodChecker();
        private void GetStat(Equipment equip)
        {
            if (_statPeriod.IsTimeToCheck(_efem.StatReadTime))
            {
                _statPeriod.Reset = true;

                if (GG.TestMode == true)
                    return;

                if (_efem.Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.TRANS) == false)
                {
                    if ((_efem.IsPickingSlot1LPM1 && Port == EmEfemPort.LOADPORT1)
                        || (_efem.IsPickingSlot1LPM2 && Port == EmEfemPort.LOADPORT2))
                        return;
                }

                if (_efem.Proxy.IsRunning(equip, Port, EmEfemCommand.STAT_) == false)
                    _efem.Proxy.StartCommand(equip, Port, EmEfemCommand.STAT_);
            }
        }

        public void ChangeLed(EmEfemLampType type, EmEfemLampBuzzerState state)
        {
            if (_changeLampDone.ContainsKey(type) == false)
                return;

            if (_changeLampDone[type].State == state
                || _changeLampDone[type].IsRunning == true)
                return;

            //20년 11월 4일 로보스타 요청으로 1번슬롯에 대한 Trans 명령 중에는 LPLED 명령 진행하지 않도록 요청
            if(GG.Equip.Efem.Proxy.HS[EmEfemPort.ROBOT].IsFirstSlot)
            {
                return;
            }

            _changeLampDone[type].Start();

            if (_lpledQue.Count < 10)
                _lpledQue.Enqueue(new EFEMLPLEDDataSet(type, state));
        }
        private int _changeLampStep = 0;
        private PlcTimerEx _lpledDelay = new PlcTimerEx("", false);
        private EFEMLPLEDDataSet _curLPLEDData;
        public void ChangeLampLogic(Equipment equip)
        {
            switch (_changeLampStep)
            {

                case 0:
                    if (_lpledQue.Count != 0)
                    {
                        _curLPLEDData = _lpledQue.Dequeue();
                        _lpledDelay.Start(0, 250);
                        _changeLampStep = 10;
                    }
                    break;
                case 10:
                    if (this.Status.IsBusy == false && _lpledDelay
                        && _efem.Proxy.IsRunning(equip, this.Port, EmEfemCommand.OPEN_) == false
                        && _efem.Proxy.IsRunning(equip, this.Port, EmEfemCommand.CLOSE) == false
                        && _efem.Proxy.IsRunning(equip, EmEfemPort.ROBOT, EmEfemCommand.TRANS) == false
                        )
                    {
                        if (_efem.Proxy.StartLPMLedChange(equip, this.Port, _curLPLEDData) == false) return;
                        _lpledDelay.Stop();
                        _changeLampStep = 20;
                    }
                    break;
                case 20:
                    if (_efem.Proxy.IsComplete(equip, this.Port, EmEfemCommand.LPLED) == true)
                    {
                        _lpledDelay.Start(0, _curLPLEDData.LampState == EmEfemLampBuzzerState.BLINK ? 2000 : 1000);
                        _changeLampStep = 30;
                    }
                    break;
                case 30:
                    if (_changeLampDone[_curLPLEDData.LampType].State == _curLPLEDData.LampState)
                    {
                        _changeLampStep = 0;
                        _changeLampDone[_curLPLEDData.LampType].IsRunning = false;
                    }
                    else if (_lpledDelay)
                    {
                        _lpledDelay.Stop();
                        _changeLampStep = 0;
                        _changeLampDone[_curLPLEDData.LampType].IsRunning = false;
                    }
                    break;
            }
        }

        private PlcTimerEx _homeCompelteDelay = new PlcTimerEx("NO MSG", false);
        public override void HomeLogicWorking(Equipment equip)
        {
            SeqLogging(equip);

            switch (_homeStepNum)
            {
                case EmEFEMLPMHomeStep.H000_END:
                    _homeStepNum = EmEFEMLPMHomeStep.H010_BUSY_CHECK;
                    break;
                case EmEFEMLPMHomeStep.H010_BUSY_CHECK:
                    if (Status.IsBusy == true)
                        _homeStepNum = EmEFEMLPMHomeStep.H015_WAIT_LPM_TO_NOT_BUSY;
                    else
                    {
                        //                        if (_efem.Proxy.StartCommand(equip, Port, EmEfemCommand.STOP_) == false) return;
                        _homeStepNum = EmEFEMLPMHomeStep.H020_WAIT_LPM_STOP_COMPLETE;
                    }
                    break;
                case EmEFEMLPMHomeStep.H015_WAIT_LPM_TO_NOT_BUSY:
                    if (Status.IsBusy == false)
                    {
                        _homeStepNum = EmEFEMLPMHomeStep.H030_INIT_LPM;
                    }
                    break;
                case EmEFEMLPMHomeStep.H020_WAIT_LPM_STOP_COMPLETE:
                    //if (_efem.Proxy.IsComplete(equip, Port, EmEfemCommand.STOP_) == true)
                    {
                        _homeStepNum = EmEFEMLPMHomeStep.H030_INIT_LPM;
                    }
                    break;
                case EmEFEMLPMHomeStep.H030_INIT_LPM:
                    if (_efem.Proxy.StartCommand(equip, Port, EmEfemCommand.RESET) == false) return;
                    _homeStepNum = EmEFEMLPMHomeStep.H040_WAIT_LPM_RESET;
                    break;
                case EmEFEMLPMHomeStep.H040_WAIT_LPM_RESET:
                    if (_efem.Proxy.IsComplete(equip, Port, EmEfemCommand.RESET) == true)
                    {
                        if (_efem.Proxy.StartCommand(equip, Port, EmEfemCommand.INIT_) == false) return;
                        _homeStepNum = EmEFEMLPMHomeStep.H050_WAIT_LPM_INIT;
                    }
                    break;
                case EmEFEMLPMHomeStep.H050_WAIT_LPM_INIT:
                    if (_efem.Proxy.IsComplete(equip, Port, EmEfemCommand.INIT_) == true)
                    {
                        if (GG.TestMode)
                        {
                            this.Status.IsDoorOpen = false;
                            this.Status.IsDoorClose = true;
                        }
                        _homeStepNum = EmEFEMLPMHomeStep.H060_CHECK_LPM_STATUS;
                    }
                    break;
                case EmEFEMLPMHomeStep.H060_CHECK_LPM_STATUS:
                    if (GG.TestMode == true || IsInitDone == true)
                    {
                        _homeCompelteDelay.Start(0, 2000);
                        _homeStepNum = EmEFEMLPMHomeStep.H100_HOME_COMPLETE;
                    }
                    break;
                case EmEFEMLPMHomeStep.H100_HOME_COMPLETE:
                    if (_homeCompelteDelay)
                    {
                        _homeCompelteDelay.Stop();
                        IsHomeComplete = true;
                    }
                    break;
            }
        }

        public override void SeqLogicWorking(Equipment equip)
        {
            if (_efem.RunMode == EmEfemRunMode.Stop)
            {
                Stop();
                _beginToOpen = false;
                _seqStepNum = EmEFEMLPMSeqStep.S000_END;
                _homeStepNum = EmEFEMLPMHomeStep.H000_END;
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

                    FoupLdLogic(equip);
                    WaferInOutLogic(equip);
                    FoupUnldLogic(equip);
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

        private int _readyStepCounter = 0;
        private int _rfReadCount = 0;
        private bool _isCsUldtReportEnd;

        public void FoupLdLogic(Equipment equip)
        {
            SeqLogging(equip);

            switch (_seqStepNum)
            {
                case EmEFEMLPMSeqStep.S000_END:
                    _isZeroCst = false;
                    equip.IsOhtLoadRunning = false;
                    equip.IsOhtUnloadRunning = false;
                    _seqStepNum = EmEFEMLPMSeqStep.S010_CHECK_EXIST;
                    break;
                case EmEFEMLPMSeqStep.S010_CHECK_EXIST:
                    if (Status.IsFoupExist == true)
                    {
                        //if (Status.IsFoupDetected == false)
                        //{
                        //    AlarmMgr.Instance.Happen(equip, this.Port == EmEfemPort.LOADPORT1 ? EM_AL_LST.AL_0650_EFEM_LPM1_CST_POS_ERROR : EM_AL_LST.AL_0651_EFEM_LPM2_CST_POS_ERROR);
                        //    return;
                        //}
                        //비정상 상황
                        if (this.CstKey == null)
                        {
                            AlarmMgr.Instance.Happen(equip, this.Port == EmEfemPort.LOADPORT1 ? EM_AL_LST.AL_0655_EFEM_LPM1_NO_DATA : EM_AL_LST.AL_0656_EFEM_LPM2_NO_DATA);
                            InterLockMgr.AddInterLock(GG.boChinaLanguage ? "无CST 情报. 可以手动生成进行." : "카세트 정보가 없습니다. 수동 생성하여 진행가능합니다.");
                            return;
                        }
                        //모든 웨이퍼가 복귀 했다고 판단 시 && 검사와 리뷰가 정상적으로 진행된 웨이퍼가 한장 이상 있을 시
                        else if (this.CstKey.ID != "" && TransferDataMgr.IsAllComeBack(this.CstKey) == true && TransferDataMgr.IsExistPerfectCompleteWafer(this.CstKey.ID))
                        {
                            //한장만 진행 모드 일때에는 정상적으로 검사한 웨이퍼가 한장 이상 있을 때 언로드 시퀀스로 빠지도록
                            if(this.ProgressWay == EmProgressWay.OnlyFirst || this.ProgressWay == EmProgressWay.OnlyLast)
                            {
                                if (GG.Equip.CtrlSetting.ReviewJudgeMode)
                                {
                                    if ((GG.Equip.CtrlSetting.NextInspCount == (ReviewFailCount + AutoMoveOutCount)) || GG.NextInspAutoMoveOut) // KYH 230913-01
                                    {
                                        GG.NextInspAutoMoveOut = false;
                                        _seqStepNum = EmEFEMLPMSeqStep.S610_UNLD_FOUP;
                                    }
                                    else
                                    {
                                        //Review 판정 대기중 알람 발생 시, 여기서 부터 다시 시작
                                        _seqStepNum = EmEFEMLPMSeqStep.S440_START_OPEN; // 이전 스텝부터 진행 시 Cassete Load 다시 하게 됨
                                    }
                                }
                                else
                                {
                                    _seqStepNum = EmEFEMLPMSeqStep.S610_UNLD_FOUP;
                                }
                            }
                            //나머지 진행 모드일 때에는
                            else
                            {
                                _isPassMutingCheckAtLoad = false;
                                _seqStepNum = EmEFEMLPMSeqStep.S110_WAIT_LOAD_BUTTON_PUSH;
                            }
                        }
                        else
                        {
                            if (GG.Equip.CtrlSetting.ReviewJudgeMode)
                            {
                                //진행 중이던 CST (바로 Open)
                                if (GG.MEM_DIT.VirGetAsciiTrim(DitCim.PLC.CIMAW.XW_CST_MAP_CST_ID) == _rfReadCstId)
                                {
                                    if((int)this.Port == 1)
                                    {
                                        GG.Equip.Efem.Robot.SetTargetLoadPort(1);
                                    }
                                    else
                                    {
                                        GG.Equip.Efem.Robot.SetTargetLoadPort(2);
                                    }
                                    _seqStepNum = EmEFEMLPMSeqStep.S440_START_OPEN;
                                }
                                else //아직 Load 안된 CST (CST Load후 Open 대기)
                                {
                                    if (Status.IsFoupExist) // CST 있는지 Double 체크
                                    {
                                        //CST Load 시키기
                                        _seqStepNum = EmEFEMLPMSeqStep.S110_WAIT_LOAD_BUTTON_PUSH;
                                    }
                                    else
                                    {
                                        //CST Load Request 보내서 CST 받기
                                        //정상 시퀀스라 하면 여기 안탐
                                        _seqStepNum = EmEFEMLPMSeqStep.S200_START_OHT_COMMUNICATION;
                                    }
                                }
                            }
                            else
                            { 
                                //진행 중이던 CST (바로 Open)
                                if (GG.MEM_DIT.VirGetAsciiTrim(DitCim.PLC.CIMAW.XW_CST_MAP_CST_ID) == _rfReadCstId)
                                {
                                    if ((int)this.Port == 1)
                                    {
                                        GG.Equip.Efem.Robot.SetTargetLoadPort(1);
                                    }
                                    else
                                    {
                                        GG.Equip.Efem.Robot.SetTargetLoadPort(2);
                                    }
                                    _seqStepNum = EmEFEMLPMSeqStep.S440_START_OPEN;
                                }
                                else
                                {
                                    _isPassMutingCheckAtLoad = false;
                                    _seqStepNum = EmEFEMLPMSeqStep.S110_WAIT_LOAD_BUTTON_PUSH;
                                }
                            }
                        }
                    }
                    else//실물 카세트 없지만 정보 남아 있는거 삭제
                    {
                        TransferDataMgr.DeleteCst(this.CstKey.ID);
                        TransferDataMgr.DeleteWafer(this.CstKey.ID);
                        _isPassMutingCheckAtLoad = false;
                        _seqStepNum = EmEFEMLPMSeqStep.S100_RECV_FOUP;
                    }
                    break;

                case EmEFEMLPMSeqStep.S100_RECV_FOUP:
                    if (this.LoadType == EmLoadType.Manual)
                    {
                        Status.IsLoadButtonPushed = false;
                        _seqStepNum = EmEFEMLPMSeqStep.S110_WAIT_LOAD_BUTTON_PUSH;
                    }
                    else if (this.LoadType == EmLoadType.OHT)
                    {
                        _seqStepNum = EmEFEMLPMSeqStep.S200_START_OHT_COMMUNICATION;
                    }
                    break;
                case EmEFEMLPMSeqStep.S110_WAIT_LOAD_BUTTON_PUSH:
                    if (Status.IsFoupExist == true && (_isPassMutingCheckAtLoad == true))
                        _seqStepNum = EmEFEMLPMSeqStep.S150_CHECK_OTHER_LOADPORT_PROGRESSING;
                    else if (Status.IsLoadButtonPushed == true || GG.EfemLongRun == true) // || GG.Equip.IsCanProgress((int)this.Port)) 진행중 이던 LPM정보로 새로 LPM 투입 시 EFEM Trans Cmd 에러남
                    {
                        Logger.Log.AppendLine(LogLevel.Info, "Port : {0}, Status.IsLoadButtonPushed : {1} ", (int)this.Port, Status.IsLoadButtonPushed);
                        Status.IsLoadButtonPushed = false;
                        _efem.LPMLightCurtain.StartMuting();
                        _seqStepNum = EmEFEMLPMSeqStep.S120_Muting_ON_WAIT;
                    }
                    //OHT 도중 abnormal case로 메뉴얼 전환후 다시 시작했을 때 이 스텝으로 오게됨
                    else if(this.LoadType == EmLoadType.OHT)
                    {
                        //if(OHTpio.IsLoad)
                        _seqStepNum = EmEFEMLPMSeqStep.S150_CHECK_OTHER_LOADPORT_PROGRESSING;
                    }
                    break;
                case EmEFEMLPMSeqStep.S120_Muting_ON_WAIT:
                    if (Status.IsFoupExist) // muing on 되기전에 cst 감지된 경우
                    {
                        Status.IsLoadButtonPushed = false;
                        if (GG.TestMode == true)
                            Status.IsFoupExist = true;
                        _efem.LPMLightCurtain.StopMuting();
                        _seqStepNum = EmEFEMLPMSeqStep.S150_CHECK_OTHER_LOADPORT_PROGRESSING;
                    }
                    else if (GG.PassLightCurtain || _efem.LPMLightCurtain.IsMuting == true || GG.EfemLongRun == true)
                    {
                        Status.IsLoadButtonPushed = false;
                        _seqStepNum = EmEFEMLPMSeqStep.S130_WAIT_LOAD_BUTTON_PUSH;
                    }
                    break;
                case EmEFEMLPMSeqStep.S130_WAIT_LOAD_BUTTON_PUSH:
                    if (Status.IsLoadButtonPushed == true || GG.EfemLongRun == true)
                    {
                        Status.IsLoadButtonPushed = false;
                        if (GG.TestMode == true)
                            Status.IsFoupExist = true;
                        _efem.LPMLightCurtain.StopMuting();
                        _seqStepNum = EmEFEMLPMSeqStep.S150_CHECK_OTHER_LOADPORT_PROGRESSING;
                    }
                    break;
                case EmEFEMLPMSeqStep.S150_CHECK_OTHER_LOADPORT_PROGRESSING:
                    _seqStepNum = EmEFEMLPMSeqStep.S300_LOAD_FOUP;
                    break;
                case EmEFEMLPMSeqStep.S200_START_OHT_COMMUNICATION:
                    //OHT UnLoad Start
                    if (OHTpio.OHTStart(EmOHTtype.LOAD))
                    {
                        equip.IsOhtLoadRunning = true;
                        HsmsPortInfo info = new HsmsPortInfo();
                        info.CstID = "";
                        info.IsCstExist = false;
                        info.LoadportNo = (int)Port;
                        info.PortMode = PortMode.LOAD_REQUEST;

                        if (equip.HsmsPc.StartCommand(equip, HSMS.EmHsmsPcCommand.PORT_MODE_CHANGE, info) == false) return;

                        _OHTRequestDely.Start(30, 0);

                        _seqStepNum = EmEFEMLPMSeqStep.S210_WAIT_CST_OHT_LOAD;
                    }
                    break;
                case EmEFEMLPMSeqStep.S210_WAIT_CST_OHT_LOAD:
                    if (OHTpio.IsComplete(EmOHTtype.LOAD))
                    {
                        equip.IsOhtLoadRunning = false;

                        _OHTRequestDely.Stop();

                        HsmsPortInfo info = new HsmsPortInfo();
                        info.CstID = "";
                        info.IsCstExist = true;
                        info.LoadportNo = (int)Port;
                        info.PortMode = PortMode.LOAD_COMPLETE;

                        if (equip.HsmsPc.StartCommand(equip, HSMS.EmHsmsPcCommand.PORT_MODE_CHANGE, info) == false) return;

                        _seqStepNum = EmEFEMLPMSeqStep.S300_LOAD_FOUP;
                    }
                    else if (_OHTRequestDely && OHTpio.IsRunning == false)
                    {
                        _OHTRequestDely.Stop();

                        _seqStepNum = EmEFEMLPMSeqStep.S200_START_OHT_COMMUNICATION;
                    }
                    break;
                case EmEFEMLPMSeqStep.S300_LOAD_FOUP:
                    if (Status.IsFoupExist == false)
                    {
                        if (GG.EfemLongRun == false)
                        {
                            AlarmMgr.Instance.Happen(equip, this.Port == EmEfemPort.LOADPORT1 ? EM_AL_LST.AL_0650_EFEM_LPM1_CST_POS_ERROR : EM_AL_LST.AL_0651_EFEM_LPM2_CST_POS_ERROR);
                            _seqStepNum = EmEFEMLPMSeqStep.S130_WAIT_LOAD_BUTTON_PUSH;
                        }
                        return;
                    }
                    _rfReadCount = 0;
                    _seqStepNum = EmEFEMLPMSeqStep.S310_READ_RF_DELAY;
                    
                    break;
                case EmEFEMLPMSeqStep.S310_READ_RF_DELAY:
                    if (_rfReadDelay.IsStart == false)
                    {
                        _rfReadDelay.Start(0, GG.Equip.CtrlSetting.Ctrl.RfReadDelayTime);
                    }
                    else
                    {
                        if (_rfReadDelay)
                        {
                            _rfReadDelay.Stop();
                            _seqStepNum = EmEFEMLPMSeqStep.S315_READ_RF_ID;
                        }
                    }
                    break;
                case EmEFEMLPMSeqStep.S315_READ_RF_ID:
                    _rfReadCount++;
                    _rfReadCstId = string.Empty;
                    if ((_rfReadCount % 2 == 1))
                        RFR.ScanTagCmd(Monitor.RFIDCmd.READER1); //RFID제거(2->1)
                    else
                        RFR.ScanTagCmd(Monitor.RFIDCmd.READER1);
                    _RFIDReadWait.Start(1);
                    _seqStepNum = EmEFEMLPMSeqStep.S320_WAIT_READ_RF_ID;
                    break;
                case EmEFEMLPMSeqStep.S320_WAIT_READ_RF_ID:
                    string read1/*, read2*/, rand;
                    read1 = /*read2 =*/ rand = string.Empty;
                    rand = this.Port == EmEfemPort.LOADPORT1 ? "LongRun_Lpm1" : "LongRun_Lpm1";//jys:: id랜덤생성 > string.Format("r{0}_{1}", (int)Port, DateTime.Now.ToString("yyMMddHHmmssfff"));

                    if (RFR.IsReader1Success() /*|| RFR.IsReader2Success()/*rf check*/) //RFID제거
                    {
                        if (RFR.IsReader1Success())
                        {
                            read1 = RFR.GetReaderReadID[0];
                        }
                        //else //RFID제거
                        //{
                        //    read2 = RFR.GetReaderReadID[1];
                        //}
                        if (GG.EfemLongRun == true)
                        {
                            _rfReadCstId = rand;
                            _seqStepNum = EmEFEMLPMSeqStep.S340_FIRST_CST_INFO_CHECK;
                        }
                        else 
                        {
                            _rfReadCstId = RFR.IsReader1Success() ? read1 : read1; //RFID제거
                            _seqStepNum = EmEFEMLPMSeqStep.S340_FIRST_CST_INFO_CHECK;
                        }

                        if (GG.EfemLongRun == false
                            && TransferDataMgr.IsExistCst(_rfReadCstId) == true && GG.Equip.CtrlSetting.IsRFKeyIn)
                        {
                            _frmRfUserMsgBox.RequestPopup(read1/*, read2*/); //RFID제거
                            _seqStepNum = EmEFEMLPMSeqStep.S330_RFID_USER_CHECK;
                        }
                    }
                    else if (_RFIDReadWait)
                    {
                        _RFIDReadWait.Stop();
                        RFR.ClearSerialBuffer();
                        if (_rfReadCount < GG.Equip.CtrlSetting.Ctrl.RFReadTryTimes && GG.TestMode == false)
                        {
                            _seqStepNum = EmEFEMLPMSeqStep.S315_READ_RF_ID;
                        }
                        else
                        {
                            AlarmMgr.Instance.Happen(equip, Port == EmEfemPort.LOADPORT1 ? EM_AL_LST.AL_0632_LOADPORT1_RF_READ_ERROR : EM_AL_LST.AL_0633_LOADPORT2_RF_READ_ERROR);

                            if (GG.EfemLongRun == true)
                            {
                                _rfReadCstId = rand;
                                _seqStepNum = EmEFEMLPMSeqStep.S340_FIRST_CST_INFO_CHECK;
                            }
                            else if (GG.TestMode)
                            {
                                if (Port == EmEfemPort.LOADPORT1)
                                {
                                    _rfReadCstId = "LPM1_CSTID";
                                }
                                else
                                {
                                    _rfReadCstId = "LPM2_CSTID";
                                }
                                _seqStepNum = EmEFEMLPMSeqStep.S340_FIRST_CST_INFO_CHECK;
                            }
                            else if (GG.Equip.CtrlSetting.IsRFKeyIn == true || GG.TestMode)
                            {
                                _frmRfUserMsgBox.RequestPopup("Timeover", "Timeover");
                                _seqStepNum = EmEFEMLPMSeqStep.S330_RFID_USER_CHECK;
                            }
                            else
                                equip.IsInterlock = true;
                        }
                    }
                    break;
                case EmEFEMLPMSeqStep.S330_RFID_USER_CHECK:
                    if (_frmRfUserMsgBox.PopupFlow == EmPopupFlow.OK)
                    {
                        _rfReadCstId = _frmRfUserMsgBox.ReturnValue;
                        _seqStepNum = EmEFEMLPMSeqStep.S340_FIRST_CST_INFO_CHECK;
                    }
                    break;
                case EmEFEMLPMSeqStep.S340_FIRST_CST_INFO_CHECK:
                    _frmCstDataInput.InitWafers(ref _inputWaferInfo);
                    _seqStepNum = EmEFEMLPMSeqStep.S350_RETRY_CST_INFO_CHECK;
                    break;
                case EmEFEMLPMSeqStep.S350_RETRY_CST_INFO_CHECK:
                    string oldWaferCstId = string.Empty;

                    if (GG.EfemLongRun == true)
                    {
                        // 설비 내 웨이퍼가 있고 배출에 제일 가까운 웨이퍼의 CST ID가 RF와 다른 경우 CST ID NOT MATCH로 진행 불가.
                        if (GG.Equip.Efem.Robot.UpperWaferKey.CstID != "") oldWaferCstId = GG.Equip.Efem.Robot.UpperWaferKey.CstID;
                        else if (GG.Equip.TransferUnit.LowerWaferKey.CstID != "") oldWaferCstId = GG.Equip.TransferUnit.LowerWaferKey.CstID;
                        else if (GG.Equip.Efem.Aligner.LowerWaferKey.CstID != "") oldWaferCstId = GG.Equip.Efem.Aligner.LowerWaferKey.CstID;
                        else if (GG.Equip.Efem.Robot.LowerWaferKey.CstID != "") oldWaferCstId = GG.Equip.Efem.Robot.LowerWaferKey.CstID;

                        if (oldWaferCstId != string.Empty
                            && oldWaferCstId != _rfReadCstId
                            && GG.Equip.Efem.OtherLoadPort(this.Port).Status.IsDoorOpen == false)
                            return;
                    }
                    else if (this.IsOpenReadyStep == false && this.IsOpenningStep == false && this.Status.IsDoorOpen == false
                        && GG.Equip.Efem.OtherLoadPort(this.Port).IsOpenReadyStep == false
                        && GG.Equip.Efem.OtherLoadPort(this.Port).IsOpenningStep == false
                        && GG.Equip.Efem.OtherLoadPort(this.Port).Status.IsDoorOpen == false)
                    //처음 시작하는 카세트인 경우.
                    {
                        // 설비 내 웨이퍼가 있고 배출에 제일 가까운 웨이퍼의 CST ID가 RF와 다른 경우 CST ID NOT MATCH로 진행 불가.
                        if (GG.Equip.Efem.Robot.UpperWaferKey.CstID != "") oldWaferCstId = GG.Equip.Efem.Robot.UpperWaferKey.CstID;
                        else if (GG.Equip.TransferUnit.LowerWaferKey.CstID != "") oldWaferCstId = GG.Equip.TransferUnit.LowerWaferKey.CstID;
                        else if (GG.Equip.Efem.Aligner.LowerWaferKey.CstID != "") oldWaferCstId = GG.Equip.Efem.Aligner.LowerWaferKey.CstID;
                        else if (GG.Equip.Efem.Robot.LowerWaferKey.CstID != "") oldWaferCstId = GG.Equip.Efem.Robot.LowerWaferKey.CstID;

                        //20 11 11 kkt::930번째줄 조건  oldWaferCstId != GG.Equip.Efem.OtherLoadPort(this.Port)._rfReadCstId  때문에 인터락이 발생하여
                        // 하기 조건 추가, 다른 쪽 로드포트 RFID 다 읽을 때까지 대기
                        if (GG.Equip.Efem.OtherLoadPort(this.Port)._rfReadCstId == string.Empty)
                        {
                            Logger.Log.AppendLine("다른 로드포트 RFID Read 완료 되지 않아 S350_RETRY_CST_INFO_CHECK 스텝에서 대기");
                            break;
                        }
                        if (oldWaferCstId != string.Empty
                            && oldWaferCstId != _rfReadCstId
                            && oldWaferCstId != GG.Equip.Efem.OtherLoadPort(this.Port)._rfReadCstId)
                        {
                            string msg = "설비 내 웨이퍼의 CST ID와 진행하려는 포트의 CST ID가 다릅니다. CST ID가 맞는 카세트를 먼저 진행해야합니다";
                            InterLockMgr.AddInterLock(GG.boChinaLanguage ? "设备内 Wafer的 CST ID和要进行的  Port的 CST ID不同. 需先进行CST ID正确的 CST" : msg, GG.boChinaLanguage ? "设备内 Wafer ID\r\n现在 CST ID :\t{0}\r\nRobot-Upper:\t{1}\r\nAVI:\t{2}\r\nAligner:\t{3}\r\nRobot-Lower:\t{4}" :
                                 "설비 내 웨이퍼 ID\r\n현재 CST ID:\t{0}\r\nRobot-Upper:\t{1}\r\nAVI:\t{2}\r\nAligner:\t{3}\r\nRobot-Lower:\t{4}",
                                 _rfReadCstId, GG.Equip.Efem.Robot.UpperWaferKey.CstID, GG.Equip.TransferUnit.LowerWaferKey.CstID,
                                GG.Equip.Efem.Aligner.LowerWaferKey.CstID, GG.Equip.Efem.Robot.LowerWaferKey.CstID);
                            _seqStepNum = EmEFEMLPMSeqStep.S110_WAIT_LOAD_BUTTON_PUSH;
                            return;
                        }
                    }

                    if (TransferDataMgr.IsInEquip(_rfReadCstId) == true)
                    {
                        Logger.Log.AppendLine(LogLevel.Info, "진행중이던 Cassette 정보입력없이 바로 진행 (ID:{0})", _rfReadCstId);
                        _seqStepNum = EmEFEMLPMSeqStep.S370_BEFORE_OPEN_READY;
                    }
                    else if (GG.EfemLongRun == false
                        && (this.ProgressWay == EmProgressWay.OnlyFirst || this.ProgressWay == EmProgressWay.OnlyLast))
                    {
                        //중간에 재시작 했을 때, 해당 카세트에서 검사, 리뷰 까지 모두 마친 웨이퍼가 1장 이상 있을 떄 이 조건으로 이동 됨.
                        if(TransferDataMgr.IsExistPerfectCompleteWafer(_rfReadCstId))
                        {
                            _frmRetryMsg.RequestPopup(GG.boChinaLanguage ? "检查时停止并重新启动" : "검사 도중 정지 후 재시작", GG.boChinaLanguage ? "重试，请在排出中选择。" : "재시도, 배출 중 선택 해주세요");

                            _seqStepNum = EmEFEMLPMSeqStep.S355_RETRY_CST_PROGRESS;
                        }
                        //아닐 경우는 시작하면 됨
                        else
                        {
                            _seqStepNum = EmEFEMLPMSeqStep.S370_BEFORE_OPEN_READY;
                        }
                    }
                    else if (GG.EfemLongRun == false
                        && (this.ProgressWay == EmProgressWay.User || TransferDataMgr.IsExistCst(_rfReadCstId) == true))
                    {
                        TransferDataMgr.DeleteCst(_rfReadCstId);
                        TransferDataMgr.DeleteWafer(_rfReadCstId);

                        _frmCstDataInput.RequestPopup(_rfReadCstId, ref _inputWaferInfo);
                        _seqStepNum = EmEFEMLPMSeqStep.S360_WAIT_CST_INFO_INPUT;
                    }
                    else
                        _seqStepNum = EmEFEMLPMSeqStep.S370_BEFORE_OPEN_READY;
                    break;
                case EmEFEMLPMSeqStep.S355_RETRY_CST_PROGRESS:
                    if (_frmRetryMsg.PopupFlow == EmPopupFlow.OK)
                    {
                        if (_frmRetryMsg.IsRetry)
                        {
                            _seqStepNum = EmEFEMLPMSeqStep.S370_BEFORE_OPEN_READY;
                        }
                        else
                        {
                            _seqStepNum = EmEFEMLPMSeqStep.S610_UNLD_FOUP;
                        }
                    }
                    break;
                case EmEFEMLPMSeqStep.S360_WAIT_CST_INFO_INPUT:
                    if (_frmCstDataInput.PopupFlow == EmPopupFlow.OK)
                    {
                        _rfReadCstId = _frmCstDataInput.ReturnValue;
                        LastSlotNo = _frmCstDataInput.LastSlotNo;
                        _seqStepNum = EmEFEMLPMSeqStep.S370_BEFORE_OPEN_READY;
                    }
                    break;
                case EmEFEMLPMSeqStep.S370_BEFORE_OPEN_READY:
                    _goReadyCst = false;
                    _readyStepCounter = 0;

                    _tempCstInfo = null;
                    _tempCstInfo = TransferDataMgr.GetCst(_rfReadCstId);

                    if (equip.CheckIsExistWaferInfo(_rfReadCstId))
                    {
                        Logger.Log.AppendLine(LogLevel.Info, "DB에 존재하는 카세트 시작");
                        //로드포트 1과 2 동시 진행을 막기 위한 딜레이
                        //if (this.Port == EmEfemPort.LOADPORT1)
                        //{
                            _ldDelay.Start(0, 10);

                        //}
                        //else
                        //{
                        //    _ldDelay.Start(0, 2000);
                        //}
                        _seqStepNum = EmEFEMLPMSeqStep.S380_OPEN_READY;
                    }
                    else
                    {
                        //로드포트 1과 2 동시 진행을 막기 위한 딜레이
                        if (this.Port == EmEfemPort.LOADPORT1)
                        {
                            _ldDelay.Start(0, 1000);

                        }
                        else
                        {
                            _ldDelay.Start(0, 2000);
                        }
                        _seqStepNum = EmEFEMLPMSeqStep.S380_OPEN_READY;
                    }
                    break;
                case EmEFEMLPMSeqStep.S380_OPEN_READY:
                    if (_ldDelay)
                    {
                        //다른쪽 로드포트가 존재하지 않으면 바로 진행
                        //또는 다른쪽 로드포트의 문이 닫혀있으면 바로 진행
                        //하지만 다른 로드포트의 스텝이 S390 ~ S460 즉, 문이 열리기전에 하는 보고되는 항목이 진행 할 때에는 대기
                        if ((_efem.OtherLoadPort(this.Port).Status.IsFoupExist == false || _efem.OtherLoadPort(this.Port).Status.IsDoorClose == true)
                            && !(_efem.OtherLoadPort(this.Port).SeqStepNum >= EmEFEMLPMSeqStep.S390_CST_LOAD_REPORT && _efem.OtherLoadPort(this.Port).SeqStepNum <= EmEFEMLPMSeqStep.S650_WAIT_UNLD_BUTTON_PUSH)
                            )
                        {
                            GoReadyCst(); // 다른 로드포트에 카세트 없거나, Close상태면 대기할 필요 없으므로.
                        }
                        //다른쪽 포트가 있으면
                        else
                        {
                            //20년 10월 7일    RFID로 읽은 CST ID로 DB검색 했는데 존재 할때
                            //                [경우 1] : Insert Cst하였지만 문제생겨서 재시작, [경우 2] : 새로운 들어온 카세트이지만 동일한ID로 투입되서 DB에서 존재하는 카세트라고 판단)
                            if(_tempCstInfo != null)
                            {
                                //이 카세트가 진행중이던 카세트라 판단 되면, 그리고 [경우 1]일 경우 이미 IsProgressing 변수가 true일 테니까(S470 ProcessNewCstIn 함수통해서)
                                //만약 IsProgressing 을 true로 바꿔주는 함수 전에 재시작 됐다면..? 그럴일 없음 InserCst 후 IsProgressing 변수 업데이트 함, 
                                if(GG.Equip.CheckIsExistWaferInfo(GG.Equip.Efem.OtherLoadPort(this.Port)._rfReadCstId) == false)
                                {
                                    GoReadyCst(); //진행 중이던 카세트가 있으면 시작
                                }
                                //진행중이지 않은 카세트라면(IsProgressing 변수가 true로 업데이트가 안됐다면)
                                //그리고 [경우 2]일 경우에도 이쪽으로 진행
                                else
                                {

                                }
                            }
                            //처음 투입된 카세트일 경우, DB검색결과가 없을 경우
                            else
                            {
                                //다른 카세트 완료될 때까지 대기
                            }
                        }

                        if (/*(GG.TestMode && Status.IsLoadButtonPushed) ||*/ _goReadyCst) // robotunit에서 lpm to lower 진행 완료 후 on하게 돼있음
                        {
                            if (GG.TestMode)
                                Status.IsLoadButtonPushed = false;
                            _ldDelay.Stop();
                            _seqStepNum = EmEFEMLPMSeqStep.S390_CST_LOAD_REPORT;
                        }
                    }
                    break;
                //===============================
                case EmEFEMLPMSeqStep.S390_CST_LOAD_REPORT:
                    if(equip.HsmsPc.IsCommandAck(HSMS.EmHsmsPcCommand.CASSETTE_UNLOAD))
                    {
                        HsmsCstLoadInfo reportInfo = new HsmsCstLoadInfo();
                        reportInfo.CstID = _rfReadCstId;
                        reportInfo.CurrentRecipe = (int)this.Port == 1 ? RecipeDataMgr.GetCurRecipeName(0) : RecipeDataMgr.GetCurRecipeName(1);
                        reportInfo.PortNo = (int)this.Port;

                        if (GG.Equip.HsmsPc.StartCommand(GG.Equip, HSMS.EmHsmsPcCommand.CASSETTE_LOAD, reportInfo) == false) return;
                        Logger.CSTHistoryLog.AppendLine(LogLevel.Info, string.Format("[카세트 로드] 보고 : {0}", _rfReadCstId));
                        _isCsUldtReportEnd = false;
                        _LoadConfirmDelay.Start(equip.CtrlSetting.Hsms.CstLoadConfirmTimeover, 0);
                        _seqStepNum = EmEFEMLPMSeqStep.S400_CST_LOAD_CONFIRM;
                    }
                    break;
                case EmEFEMLPMSeqStep.S400_CST_LOAD_CONFIRM:
                    //* CST Load Reply 와 PP Select 의 이벤트 순서는 바뀔 수 있음
                    if (_LoadConfirmDelay)
                    {
                        _LoadConfirmDelay.Stop();
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0570_CASSETTE_LOAD_CONFIRM_TIMEOVER);

                    }
                    else if (GG.Equip.HsmsPc.IsEventComplete(HSMS.EmHsmsPcEvent.CST_MAP) && (GG.Equip.HsmsPc.IsCstLoadIFComplete || GG.CimTestMode == true))
                    {
                        _LoadConfirmDelay.Stop();
                        GG.Equip.HsmsPc.IsCstLoadIFComplete = false;
                        if (GG.Equip.HsmsPc.IsCstLoadConfirmOK == true || GG.CimTestMode == true)
                        {
                            if(GG.TestMode == true)
                            {
                                GG.MEM_DIT.VirSetAscii(DitCim.PLC.CIMAW.XW_CST_MAP_CST_ID, this.Port == EmEfemPort.LOADPORT1 ? "LPM1_CSTID" : "LPM2_CSTID");
                            }
                            if(GG.MEM_DIT.VirGetAsciiTrim(DitCim.PLC.CIMAW.XW_CST_MAP_CST_ID) != _rfReadCstId && GG.Equip.CimMode == EmCimMode.Remote)
                            {
                                AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0562_CST_CONFIRM_OK_BUT_ID_NOT_MATCH_RFID_READ);
                                GG.Equip.IsInterlock = true;
                                return;
                            }
                            else
                            {
                                _PPSelcetDelay.Start(equip.CtrlSetting.Hsms.PPSelectTimeover, 0);
                                _LoadStartCmdDelay.Start(equip.CtrlSetting.Hsms.CstLoadStartCmdTimeover, 0);
                                _seqStepNum = EmEFEMLPMSeqStep.S420_CST_START_CMD_WAIT;
                            }
                        }
                        else
                        {
                            AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0555_HOST_CST_LOAD_CONFIRM_NG);
                            _seqStepNum = EmEFEMLPMSeqStep.S420_CST_START_CMD_WAIT;
                        }
                    }
                    break;
                case EmEFEMLPMSeqStep.S420_CST_START_CMD_WAIT:
                    //  -인터락 체크 결과 OK이면서 장비의 Recipe 를 변경하지 않는 경우
                    //   : CST Load > Reply(True) > Start Command > Lot Start
                    //- 인터락 체크 결과 OK이면서 장비의 Recipe 를 변경하는 경우
                    //    : CST Load > Reply(True) > PP Select > Recipe Select > Recipe Validation OK 시 Start Command > Lot Start
                    if (GG.Equip.HsmsPc.IsCstStartCommand || GG.CimTestMode == true)
                    {
                        GG.Equip.HsmsPc.IsCstStartCommand = false;
                        _LoadStartCmdDelay.Stop();

                        _seqStepNum = EmEFEMLPMSeqStep.S440_START_OPEN;
                    }
                    else if (this.LoadType == EmLoadType.Manual)
                    {
                        _LoadStartCmdDelay.Stop();


                        _seqStepNum = EmEFEMLPMSeqStep.S430_PP_SELECT_WAIT_VERIOITY_OK_WAIT;
                    }
                    else if (_LoadStartCmdDelay)
                    {
                        _LoadStartCmdDelay.Stop();

                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0572_CASSETTE_LOAD_START_CMD_TIMEOVER);
                    }
                    break;
                case EmEFEMLPMSeqStep.S430_PP_SELECT_WAIT_VERIOITY_OK_WAIT:
                    if (equip.HsmsPc.IsPPSelectOK)
                    {
                        equip.HsmsPc.IsPPSelectOK = false;

                        _seqStepNum = EmEFEMLPMSeqStep.S440_START_OPEN;
                    }
                    break;
                //=====================
                case EmEFEMLPMSeqStep.S440_START_OPEN:
                    //추후 상위 정보 비교 등 처리.        

                    _beginToOpen = true;
                    _isZeroCst = false;
                    EFEMTactMgr.Instance.InsertWafer((int)Port);
                    Logger.TacttimeLog.AppendLine(LogLevel.NoLog, "[EFEM TACT] {0} OPEN START============================================", Port);
                    if (Status.IsDoorOpen == false)
                    {
                        if (_efem.Proxy.StartCommand(equip, Port, EmEfemCommand.OPEN_) == false) return;
                    }
                    _seqStepNum = EmEFEMLPMSeqStep.S450_WAIT_LPM_LOAD;
                    break;
                case EmEFEMLPMSeqStep.S450_WAIT_LPM_LOAD:
                    if (_efem.Proxy.IsComplete(equip, Port, EmEfemCommand.OPEN_) == true)
                    {
                        if (GG.TestMode)
                        {
                            this.Status.IsDoorOpen = true;
                            this.Status.IsDoorClose = false;
                        }

                        _beginToOpen = false;
                        if (_efem.Proxy.StartCommand(equip, Port, EmEfemCommand.MAPP_) == false) return;
                        _seqStepNum = EmEFEMLPMSeqStep.S460_WAIT_LPM_MAPPING;
                    }
                    break;
                case EmEFEMLPMSeqStep.S460_WAIT_LPM_MAPPING:
                    if (_efem.Proxy.IsComplete(equip, Port, EmEfemCommand.MAPP_) == true)
                    {
                        if (this.ProgressWay == EmProgressWay.Mapping && TransferDataMgr.IsExistCst(_rfReadCstId) == false)
                            MappingToUserMap(ref _inputWaferInfo);

                        if (GG.EfemLongRun == false
                            && TransferDataMgr.IsInEquip(_rfReadCstId) == false
                            && CompareUserMap(ref _inputWaferInfo) == false
                            && (this.ProgressWay == EmProgressWay.Mapping || this.ProgressWay == EmProgressWay.User))
                        {
                            AlarmMgr.Instance.Happen(equip, Port == EmEfemPort.LOADPORT1 ? EM_AL_LST.AL_0860_LPM1_USER_INPUT_MAP_ABRNOMAL : EM_AL_LST.AL_0861_LPM2_USER_INPUT_MAP_ABRNOMAL);
                            MappingToUserMap(ref _inputWaferInfo);
                            _seqStepNum = EmEFEMLPMSeqStep.S350_RETRY_CST_INFO_CHECK;
                            return;
                        }

                        _seqStepNum = EmEFEMLPMSeqStep.S470_VERIFY_LPM_MAPPING;
                    }
                    break;
                case EmEFEMLPMSeqStep.S470_VERIFY_LPM_MAPPING:
                    {
                        string mapRet = string.Empty;

                        //기존에 등록된 cst인지 확인
                        // INIT 후 재스캔하는 경우 나간 Wafer와 카세트 내 웨이퍼의 정보가 일치하는지 확인해야함.                    
                        if (TransferDataMgr.IsExistCst(_rfReadCstId) == false)
                        {
                            int waferCount = Status.MappingData.Count(c => c == EmEfemMappingInfo.Presence);
                            ProcessNewCstIn(_rfReadCstId, waferCount);

                            if (waferCount == 0)
                            {
                                _isZeroCst = true;
                                Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "{0} Mapping Data No Wafer", Port);
                                AlarmMgr.Instance.Happen(equip, this.Port == EmEfemPort.LOADPORT1 ? EM_AL_LST.AL_0677_LPM1_NO_WAFER_IN_CST : EM_AL_LST.AL_0678_LPM2_NO_WAFER_IN_CST);
                                _seqStepNum = EmEFEMLPMSeqStep.S610_UNLD_FOUP;
                                return;
                            }
                            else if (ProcessMappingData(equip, CstKey.ID, out mapRet, false) == false)
                            {
                                AlarmMgr.Instance.Happen(equip, this.Port == EmEfemPort.LOADPORT1 ? EM_AL_LST.AL_0663_LPM1_WAFER_STATUS_ABNORMAL : EM_AL_LST.AL_0664_LPM2_WAFER_STATUS_ABNORMAL);
                                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Wafer Status Error (生成新的Wafer)" : "Wafer상태 이상 (새로운 웨이퍼 생성)", "{0}\r\n{1}", Port.ToString(), mapRet);
                                equip.IsInterlock = true;
                            }
                            UpdateNextWaferKey();
                            _seqStepNum = EmEFEMLPMSeqStep.S480_HOST_DATA_COMPARE;
                            //_seqStepNum = EmEFEMLPMSeqStep.S350_CST_LOAD_REPORT;
                        }
                        //[경우1] 재시작
                        //[경우2] 새로운 카세트지만 중복 CST ID 다시 로드 됨
                        else //등록된 카세트면 데이터를 합칠지 확인 (사용자 확인), 웨이퍼 위치 중복없으면 그냥 넘어가야함
                        {
                            if (IsCstMapDuplication(_rfReadCstId) == true)
                            {
                                AlarmMgr.Instance.Happen(equip, this.Port == EmEfemPort.LOADPORT1 ? EM_AL_LST.AL_0665_LPM1_WAFER_DUPLICATION_ERROR : EM_AL_LST.AL_0666_LPM2_WAFER_DUPLICATION_ERROR);
                                equip.IsInterlock = true;
                                return;
                            }
                            else
                            {
                                Logger.TacttimeLog.AppendLine(LogLevel.NoLog, "[EFEM TACT] {0} OPEN/MAP COMPLETE", Port);
                                ProcessExistCstIn(_rfReadCstId, (int)this.Port);
                                if (GG.EfemLongRun)
                                {
                                    try
                                    {
                                        CassetteInfo tempCst = TransferDataMgr.GetCst(this.CstKey);
                                        tempCst.InputDate = DateTime.Now;
                                        
                                        tempCst.Update();
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.Log.AppendLine(LogLevel.NoLog, "LONG RUN 중 카세트 시작 시간 초기화 실패");
                                    }
                                }
                                _seqStepNum = EmEFEMLPMSeqStep.S480_HOST_DATA_COMPARE;
                            }

                            if (TransferDataMgr.IsInEquip(_rfReadCstId) == false)
                            {
                                if (ProcessMappingData(equip, CstKey.ID, out mapRet, true) == false)
                                {
                                    AlarmMgr.Instance.Happen(equip, this.Port == EmEfemPort.LOADPORT1 ? EM_AL_LST.AL_0663_LPM1_WAFER_STATUS_ABNORMAL : EM_AL_LST.AL_0664_LPM2_WAFER_STATUS_ABNORMAL);
                                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Wafer Status Error (再生成Wafer)" : "Wafer상태 이상 (웨이퍼 재생성)", "{0}\r\n{1}", Port.ToString(), mapRet);
                                    equip.IsInterlock = true;
                                }
                            }

                            StringBuilder sb = new StringBuilder();
                            sb.AppendFormat("등록된 카세트 투입 새 맵 : ");
                            for (int i = 0; i < Status.MappingData.Length; ++i)
                                sb.AppendFormat("{0}:{1},", i + 1, Status.MappingData[i]);
                            sb.AppendFormat(" | Aligner:{0},{1}", _efem.Aligner.LowerWaferKey.CstID, _efem.Robot.LowerWaferKey.SlotNo);
                            sb.AppendFormat(" | Lower:{0},{1}", _efem.Robot.LowerWaferKey.CstID, _efem.Robot.LowerWaferKey.SlotNo);
                            sb.AppendFormat(" | Upper:{0},{1}", _efem.Robot.UpperWaferKey.CstID, _efem.Robot.UpperWaferKey.SlotNo);
                            sb.AppendFormat(" | AVI:{0},{1}", GG.Equip.TransferUnit.LowerWaferKey.CstID, GG.Equip.TransferUnit.LowerWaferKey.SlotNo);
                            Logger.TransferDataLog.AppendLine(LogLevel.Info, sb.ToString());
                        }
                    }
                    break;
                case EmEFEMLPMSeqStep.S480_HOST_DATA_COMPARE:
                    if (GG.Equip.CheckCstMapData(Status.MappingData, _rfReadCstId, Port) == true)
                    {
                        _seqStepNum = EmEFEMLPMSeqStep.S490_APPLY_USER_MAP_TO_OLD_CST;
                    }
                    else
                    {
                        AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0554_HOST_MAPPING_DATA_NOT_MATCH);
                    }
                    break;
                case EmEFEMLPMSeqStep.S490_APPLY_USER_MAP_TO_OLD_CST: // 진행여부만 업데이트
                    if (GG.DeepReviewWaitAlarm)
                    {
                        GG.DeepReviewWaitAlarm = false;
                    }
                    else
                    {
                        if (this.ProgressWay == EmProgressWay.OnlyLast)
                        {
                            if (equip.CheckIsExistWaferInfo(CstKey.ID) == false)
                            {
                                int LastSlotNo = -1;
                                for (int i = 12; i >= 0; i--)
                                {
                                    if (Status.MappingData[i] == EmEfemMappingInfo.Presence)
                                    {
                                        LastSlotNo = i + 1;
                                        break;
                                    }
                                }
                                //한장 들어있는 카세트 들어 왔을 때 해당 웨이퍼가 진행 중이면 카세트안에는 웨이퍼가 없으므로 재시작 관련 처리가 되어 있지 않았음
                                if (LastSlotNo != -1)
                                {
                                    foreach (var input in _inputWaferInfo)
                                    {
                                        _tempWaferInfo = TransferDataMgr.GetWafer(CstKey.ID, input.SlotNo);
                                        // 실제 Mapping해서 Wafer있는걸로 확인된 것 중에서만 입력한 데이터 (상위or유저)에 맞게 진행여부 변경
                                        if (_tempWaferInfo.Status == EmEfemMappingInfo.Presence)
                                        {
                                            if (input.SlotNo == LastSlotNo)
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
                            else
                            {
                                _seqStepNum = EmEFEMLPMSeqStep.S500_PROCESSING;
                                break;
                            }

                        }
                        else if (this.ProgressWay == EmProgressWay.OnlyFirst)
                        {
                            if (equip.CheckIsExistWaferInfo(CstKey.ID) == false)
                            {
                                int FirstSlotNo = -1;
                                for (int i = 0; i < 13; i++)
                                {
                                    if (Status.MappingData[i] == EmEfemMappingInfo.Presence)
                                    {
                                        FirstSlotNo = i + 1;
                                        break;
                                    }
                                }
                                //한장 들어있는 카세트 들어 왔을 때 해당 웨이퍼가 진행 중이면 카세트안에는 웨이퍼가 없으므로 재시작 관련 처리가 되어 있지 않았음
                                if (FirstSlotNo != -1)
                                {
                                    foreach (var input in _inputWaferInfo)
                                    {
                                        _tempWaferInfo = TransferDataMgr.GetWafer(CstKey.ID, input.SlotNo);
                                        // 실제 Mapping해서 Wafer있는걸로 확인된 것 중에서만 입력한 데이터 (상위or유저)에 맞게 진행여부 변경
                                        if (_tempWaferInfo.Status == EmEfemMappingInfo.Presence)
                                        {
                                            if (input.SlotNo == FirstSlotNo)
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
                            else
                            {
                                _seqStepNum = EmEFEMLPMSeqStep.S500_PROCESSING;
                                break;
                            }
                        }
                        else if (GG.EfemLongRun == false
                            && TransferDataMgr.IsInEquip(_rfReadCstId) == false)
                        {

                            foreach (var input in _inputWaferInfo)
                            {
                                _tempWaferInfo = TransferDataMgr.GetWafer(CstKey.ID, input.SlotNo);
                                // 실제 Mapping해서 Wafer있는걸로 확인된 것 중에서만 입력한 데이터 (상위or유저)에 맞게 진행여부 변경
                                if (_tempWaferInfo.Status == EmEfemMappingInfo.Presence)
                                {
                                    if (input.IsComeBack)
                                    {
                                        _tempWaferInfo.IsComeBack = true;
                                        _tempWaferInfo.IsOut = true;
                                        _tempWaferInfo.Notch = input.Notch;
                                    }
                                    else
                                    {
                                        _tempWaferInfo.IsComeBack = false;
                                        _tempWaferInfo.IsOut = false;
                                        _tempWaferInfo.IsAlignComplete = false;
                                        _tempWaferInfo.IsInspComplete = false;
                                        _tempWaferInfo.IsReviewComplete = false;
                                        _tempWaferInfo.Notch = input.Notch;
                                    }
                                }
                                _tempWaferInfo.Update();
                            }
                        }
                    }

                    CassetteInfo cst = TransferDataMgr.GetCst(CstKey.ID);
                    if (cst.LoadPortNo == 1)
                        cst.RecipeName = RecipeDataMgr.GetCurRecipeName(0);
                    else
                        cst.RecipeName = RecipeDataMgr.GetCurRecipeName(1);
                    cst.LotID = GG.MEM_DIT.VirGetAsciiTrim(DitCim.PLC.CIMAW.XW_CST_MAP_LOT_ID);
                    int _tempWaferCount = 0;
                    foreach (var mappingData in Status.MappingData)
                    {
                        if (mappingData == EmEfemMappingInfo.Presence)
                            _tempWaferCount++;
                    }
                    if(cst.IsLotStartOK == false)
                        cst.SlotCount = _tempWaferCount;
                    cst.LoadPortNo = (int)this.Port;
                    cst.Update();

                    _seqStepNum = EmEFEMLPMSeqStep.S510_CST_LOT_START;
                    break;

                case EmEFEMLPMSeqStep.S510_CST_LOT_START:
                    if (equip.HsmsPc.StartCommand(equip, HSMS.EmHsmsPcCommand.LOT_START, TransferDataMgr.GetCst(this.CstKey)))
                    {
                        _seqStepNum = EmEFEMLPMSeqStep.S520_LOT_START_CONFIRM_WAIT;
                    }
                    break;
                case EmEFEMLPMSeqStep.S520_LOT_START_CONFIRM_WAIT:
                    if (equip.HsmsPc.IsEventComplete(HSMS.EmHsmsPcEvent.LOT_START) && (equip.HsmsPc.IsLotStartIFComplete || GG.CimTestMode == true))
                    {
                        if (equip.HsmsPc.IsLotStartConfirmOK || GG.CimTestMode == true)
                        {
                            Logger.CIMLog.AppendLine(LogLevel.Info, "Lot Start Confirm OK");
                            _tempCstInfo = TransferDataMgr.GetCst(this.CstKey);
                            _tempCstInfo.IsLotStartOK = true;
                            _tempCstInfo.Update();
                        }
                        else
                        {
                            Logger.CIMLog.AppendLine(LogLevel.Info, "Lot Start Confirm NG");
                            _tempCstInfo = TransferDataMgr.GetCst(this.CstKey);
                            _tempCstInfo.IsLotStartOK = false;
                            _tempCstInfo.Update();
                            AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0550_LOT_START_CONFIRM_NG);
                        }
                        equip.HsmsPc.IsLotStartConfirmOK = false;
                        equip.HsmsPc.IsLotStartIFComplete = false;
                        _seqStepNum = EmEFEMLPMSeqStep.S500_PROCESSING;
                    }
                    break;

            }
        }

        private void MappingToUserMap(ref List<WaferInfo> inputWaferInfo)
        {
            for (int i = 0; i < inputWaferInfo.Count; ++i)
            {
                inputWaferInfo[i].Status = Status.MappingData[i] != EmEfemMappingInfo.Absence ? EmEfemMappingInfo.Presence : EmEfemMappingInfo.Absence;
            }
        }

        private bool CompareUserMap(ref List<WaferInfo> inputWaferInfo)
        {
            foreach (var input in inputWaferInfo)
            {
                if (input.Status != Status.MappingData[inputWaferInfo.IndexOf(input)])
                    return false;
            }
            return true;
        }

        private bool IsCstMapNotMatched(EmEfemMappingInfo[] curMap, string bakCstId)
        {
            EmEfemMappingInfo bk;
            for (int i = 0; i < curMap.Length; ++i)
            {
                try
                {
                    bk = TransferDataMgr.GetWafer(bakCstId, i + 1).Status;
                    if (curMap[i] != bk)
                        return true;
                }
                catch (Exception ex)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsCstMapDuplication(string cstId)
        {
            return _efem.Aligner.LowerWaferKey.CstID == cstId && Status.MappingData[_efem.Aligner.LowerWaferKey.SlotNo - 1] != EmEfemMappingInfo.Absence
                || GG.Equip.TransferUnit.LowerWaferKey.CstID == cstId && Status.MappingData[GG.Equip.TransferUnit.LowerWaferKey.SlotNo - 1] != EmEfemMappingInfo.Absence
                || _efem.Robot.LowerWaferKey.CstID == cstId && Status.MappingData[_efem.Robot.LowerWaferKey.SlotNo - 1] != EmEfemMappingInfo.Absence
                || _efem.Robot.UpperWaferKey.CstID == cstId && Status.MappingData[_efem.Robot.UpperWaferKey.SlotNo - 1] != EmEfemMappingInfo.Absence
                ;
        }
        private bool ProcessMappingData(Equipment equip, string cstId, out string errRet, bool isClearAndInsert)
        {
            bool errWafer = false;
            errRet = string.Empty;
            int waferCount = Status.MappingData.Count(c => c == EmEfemMappingInfo.Presence);
            WaferInfo wafer;

            #region Test ONLY (No Wafer)
            if (GG.EfemNoWafer)
            {
                if (waferCount == 0)
                {
                    Random r = new Random();
                    Status.MappingData[0] = EmEfemMappingInfo.Presence;
                    for (int i = 1; i < 13; ++i)
                        Status.MappingData[i] = (r.Next() % 2) == 0 ? EmEfemMappingInfo.Presence : EmEfemMappingInfo.Absence;
                }
                else
                {
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "是No Wafer Mode但感应到了 Wafer" : "No Wafer Mode인데 웨이퍼 감지 됩니다");
                    this.SetRunMode(EmEfemRunMode.Stop);
                    equip.IsInterlock = true;
                }
            }
            #endregion Test ONLY

            StringBuilder sb = new StringBuilder(), map = new StringBuilder();

            // 1. Wafer Recovery Error 관련 수정
            List<WaferInfo> CurrentMappingWafer = new List<WaferInfo>();
            CurrentMappingWafer = TransferDataMgr.GetWafersIn(this.CstKey.ID);
            // 0이 아니면(정상이 아님 = Data중복) Insert Wafer 시, Count 증가
            if (CurrentMappingWafer.Count == 0) // 정상이면
            {
                for (int i = 0; i < Status.MappingData.Length; ++i)
                {
                    if (errWafer == false && Status.MappingData[i] != EmEfemMappingInfo.Presence && Status.MappingData[i] != EmEfemMappingInfo.Absence)
                        errWafer = true;
                    map.AppendFormat("{0} ", Status.MappingData[i]);

                    if (Status.MappingData[i] != EmEfemMappingInfo.Presence)
                        sb.AppendFormat("SlotNo:{0} - ", i + 1).AppendLine(Status.MappingData[i].ToString());

                    if (isClearAndInsert == true)
                        TransferDataMgr.InitExistWafer(cstId, i + 1, Status.MappingData[i]);
                    else
                        TransferDataMgr.InsertWafer(cstId, i + 1, Status.MappingData[i]);
                }
                Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "{0}.{1} SENSOR MAPPING : {2}", Port.ToString(), cstId, map.ToString());
            }
            else // 비정상 Data 중복
            {
                Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "LPM{0}.CST ID: {1} InsertWafer Data 중복 발생하여 예외처리 진행", Port.ToString(), cstId);
            }
            errRet = sb.ToString();
            return !errWafer;
        }
        private string ProcessNewCstIn(string cstId, int waferCount)
        {
            TransferDataMgr.InsertCst(cstId, Port, waferCount);
            
            _tempCstInfo = null;
            _tempCstInfo = TransferDataMgr.GetCst(cstId);
            if(_tempCstInfo != null)
            {
                CstKey = _tempCstInfo.ToKey();

                bool before = _tempCstInfo.IsProgressing;
                _tempCstInfo.IsProgressing = true;
                _tempCstInfo.Update();

                Logger.TransferDataLog.AppendLine(LogLevel.Info, "[IsProgressing 변수 변경 {0} -> {1}]", before.ToString(), _tempCstInfo.IsProgressing.ToString());
            }
            else
            {
                //_tempCstInfo 가 null이면 문제 있는거임
                AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0659_NO_CST_INFO);
            }

            TransferDataMgr.UpdateBak(DBPort, CstKey);
            Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "{0} New Cst In : {1}, Slot Count : {2}", this.Port, cstId, waferCount);

            return cstId;
        }
        private string ProcessExistCstIn(string cstId, int loadPortNo)
        {
            CassetteInfo cst = TransferDataMgr.GetCst(cstId);
            cst.LoadPortNo = loadPortNo;
            cst.Update();
            CstKey = cst.ToKey();
            TransferDataMgr.UpdateBak(DBPort, CstKey);
            Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "{0} Exist Cst In : {1}, Slot Count : {2}", this.Port, cstId, cst.SlotCount);
            return cstId;
        }

        public void ProcessCstOut()
        {
            _tempCstInfo = TransferDataMgr.GetCst(CstKey);
            if (_tempCstInfo != null)
            {
                _tempCstInfo.OutputDate = DateTime.Now;
                _tempCstInfo.Update();

                TransferDataMgr.DeleteCst(_tempCstInfo.CstID);
                TransferDataMgr.DeleteWafer(_tempCstInfo.CstID);
            }
            Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "{0} Cst Out : {1}", this.Port, CstKey.ID);
            CstKey.Clear();
            TransferDataMgr.ClearCstBak(DBPort);
        }

        public void ReviewJudgeFailMode(Equipment equip)
        {
            // Joo // 마지막으로 진행한 Upper Wafer Slot Number
            int unloadedSlot = GG.Equip.Efem.Robot.LastUnloadedWaferKey.SlotNo; // Slot = 1 ~ 13
                                                                                // Joo // 검색할 슬롯, for문에 들어갈거                                                                    
            int skipIdx = unloadedSlot - 1;
            // Joo // 다음 진행 할 Wafer Slot
            int NextIdx = -1;

            // KYH 230913 : 다음장 검사모드인데, 전체검사 모드로 변경된 경우 
            if (this.ProgressWay == EmProgressWay.Mapping)
            {
                Logger.Log.AppendLine(LogLevel.Info, "ReviewJudgeFailMode : 전체검사 모드라서 Return"); // KYH 230912
                return;
            }

            if (this.ProgressWay == EmProgressWay.OnlyLast)
            {   // Joo // 진행 모드 확인 (맨 윗장 하나만)
                for (int i = skipIdx; i >= 0; i--)
                {   // Joo // 마지막 진행한 Wafer Slot이 13이면 skipIdx = 12..11..10.. 
                    if (i == 0)
                    {   // Joo // MappingData 배열 Error문제 떄문에
                        continue;
                    }
                    // Joo // CST에 존재하는 Wafer Slot 확인, 가장 위에서 시작했으니 아래순으로 확인
                    if (Status.MappingData[i - 1] == EmEfemMappingInfo.Presence)
                    {
                        // Joo // CST에 Wafer가 존재하면 해당 Slot을 NextIdx에 저장 후, 탈출
                        NextIdx = i - 1;
                        break;
                    }
                }
            }
            else if (this.ProgressWay == EmProgressWay.OnlyFirst)
            {       // Joo // 진행 모드 확인 (맨 아랫장 하나만)
                    // Joo // MappingData.Length = 13
                for (int i = skipIdx; i < Status.MappingData.Length; i++)
                {
                    if (i == 12)
                    {
                        continue;
                    }
                    if (Status.MappingData[i + 1] == EmEfemMappingInfo.Presence)
                    {
                        NextIdx = i + 1;
                        break;
                    }
                }
            }
            else if (this.ProgressWay == EmProgressWay.User)
            {       // Joo // 진행 모드 확인 (User 랜덤 선택)
                for (int i = 0; i < Status.MappingData.Length; i++)
                {
                    if (i == 13)
                    {
                        continue;
                    }
                    // Joo // Wafer 1번 Slot 부터 확인, 제일 아랫장 부터 오름차순 진행
                    if (Status.MappingData[i] == EmEfemMappingInfo.Presence)
                    {
                        _tempWaferInfo = TransferDataMgr.GetWafer(this.CstKey.ID, i + 1);
                        // Joo // Wafer 1번 Slot이 있는데 검사진행을 안한 Wafer일 경우, 검사 진행
                        if (_tempWaferInfo.IsAlignComplete == false && _tempWaferInfo.IsInspComplete == false && _tempWaferInfo.IsReviewComplete == false)
                        {
                            // Joo // 결국에 Continue 해도, NextIdx = -1로 초기화해서 문제 없음
                            NextIdx = i;
                            break;
                        }
                    }
                }
            }

            //1. 갱신 결과 진행할 웨이퍼가 없을 때
            if (NextIdx == -1 || NextIdx == 13)
            {   // Joo // 첫장이나 마지막장이면 자동으로 여기로 들어오게됨
                // 마지막 웨이퍼이므로 오류 메시지 출력 후 배출
                //CheckMgr.AddCheckMsg(true, "진행할 웨이퍼가 없어 자동 배출 됩니다");
                Logger.Log.AppendLine(LogLevel.Info, "딥러닝 컴플리트 미수신 하였지만 진행할 웨이퍼가 없어 해당 카세트 배출 => X"); // KYH 230912
                GG.Equip.Efem.Robot.LastUnloadedWaferKey.Clear();

                //시퀀스 변경으로 놓칠수 있는 항목들
                _DeepLearningReviewCompleteDelay.Stop();
                GG.ReviewJudgeWait = false;
                // DeepLearningReviewJudge = ""; // KYH 230913
                equip.HsmsPc._IsAutoMoveOutIFComplete = false;

                // Joo // CST Unload (배출)준비
                //_seqStepNum = EmEFEMLPMSeqStep.S610_UNLD_FOUP;  // KYH 230912
                return;
            }


            //2. 웨이퍼 out, comback 갱신
            if (this.ProgressWay == EmProgressWay.OnlyLast)
            {
                for (int i = skipIdx; i >= 0; i--)
                {
                    _tempWaferInfo = TransferDataMgr.GetWafer(this.CstKey.ID, i + 1);
                    if (i == NextIdx)
                    {   // Joo // 진행 모드 확인에서 다음 진행 할 Wafer Slot = NextIdx
                        // Joo // 결국엔 무조건 여기 들어감 (바로 다음 장 진행)
                        _tempWaferInfo.IsComeBack = false;
                        _tempWaferInfo.IsOut = false;
                        _tempWaferInfo.IsAlignComplete = false;
                        _tempWaferInfo.IsInspComplete = false;
                        _tempWaferInfo.IsReviewComplete = false;
                        _tempWaferInfo.Update();
                        break;
                    }
                    else
                    {
                        _tempWaferInfo.IsComeBack = true;
                        _tempWaferInfo.IsOut = true;
                        _tempWaferInfo.Update();
                    }
                }
            }
            else if (this.ProgressWay == EmProgressWay.OnlyFirst)
            {
                for (int i = skipIdx; i < Status.MappingData.Length; i++)
                {
                    _tempWaferInfo = TransferDataMgr.GetWafer(this.CstKey.ID, i + 1);
                    if (i == NextIdx)
                    {
                        _tempWaferInfo.IsComeBack = false;
                        _tempWaferInfo.IsOut = false;
                        _tempWaferInfo.IsAlignComplete = false;
                        _tempWaferInfo.IsInspComplete = false;
                        _tempWaferInfo.IsReviewComplete = false;
                        _tempWaferInfo.Update();
                        break;
                    }
                    else
                    {
                        _tempWaferInfo.IsComeBack = true;
                        _tempWaferInfo.IsOut = true;
                        _tempWaferInfo.Update();
                    }

                }
            }
            else if (this.ProgressWay == EmProgressWay.User)
            {
                // Joo // 다음장 검사 진행용 Wafer 확인
                NextWaferSlot = true;
                for (int i = 0; i < Status.MappingData.Length; i++)
                {
                    _tempWaferInfo = TransferDataMgr.GetWafer(this.CstKey.ID, i + 1);
                    if (i == NextIdx)
                    {
                        _tempWaferInfo.IsComeBack = false;
                        _tempWaferInfo.IsOut = false;
                        _tempWaferInfo.IsAlignComplete = false;
                        _tempWaferInfo.IsInspComplete = false;
                        _tempWaferInfo.IsReviewComplete = false;
                        _tempWaferInfo.Update();
                        break;
                    }
                    else
                    {
                        _tempWaferInfo.IsComeBack = true;
                        _tempWaferInfo.IsOut = true;
                        _tempWaferInfo.Update();
                    }
                }
            }
            else
            {
                // Joo // 진행 모드 Only First, Last, User 가 아닐 경우
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0947_EXCEPTION_OCCURE);
                Logger.Log.AppendLine(LogLevel.Warning, string.Format("[Auto MoveOut] 웨이퍼 진행 모드 Error 발생 - {0}", this.ProgressWay.ToString()));
            }
        }

        int slotProcessCount = 0;
        public void WaferInOutLogic(Equipment equip)
        {
            SeqLogging(equip);

            switch (_seqStepNum)
            {
                case EmEFEMLPMSeqStep.S500_PROCESSING:
                    _beginToOpen = false;

                    // 230621 S535 무한 대기 관련 비트 삭제
                    // equip.HsmsPc._IsAutoMoveOutIFComplete = false;
                    _seqStepNum = EmEFEMLPMSeqStep.S530_SEND_RECV_ABLE_ON;
                    break;
                case EmEFEMLPMSeqStep.S530_SEND_RECV_ABLE_ON:
                    if (TransferDataMgr.IsAllComeBack(this.CstKey) == true && TransferDataMgr.IsExistPerfectCompleteWafer(this.CstKey.ID))
                    {
                        if(GG.Equip.Efem.LoadPort1.ProgressWay == EmProgressWay.Mapping || GG.Equip.Efem.LoadPort2.ProgressWay == EmProgressWay.Mapping)
                        {
                            _seqStepNum = EmEFEMLPMSeqStep.S610_UNLD_FOUP;
                        }
                        else if (GG.Equip.CtrlSetting.ReviewJudgeMode)
                        {
                            _seqStepNum = EmEFEMLPMSeqStep.S535_REVIEW_JUDGE_ON;
                        }
                        else
                        {
                            _seqStepNum = EmEFEMLPMSeqStep.S610_UNLD_FOUP;
                        }
                    }
                    else
                    {
                        // Joo // 2번 빠짐 = Wafer Load 하기전, PreAligner에 넣기 전
                        PioRecv.YRecvAble = true;

                        // 새로 바꾼 방식 

                        // 전체 검사 모드
                        if(this.ProgressWay == EmProgressWay.Mapping)   
                        {
                            CassetteInfo cstInfo = TransferDataMgr.GetCst(CstKey.ID);

                            if(cstInfo.SlotCount == 1)
                            {
                                Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "Slot Count 1개 전체검사 투입");

                                if (GG.Equip.CtrlSetting.slotProcessCount == 0)
                                {
                                    Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "Slot Count 1개 전체검사 시작 Port {0}, CstKey.ID : {1} Slot Count : {2}, Review Fail : {3}, AutoMoveOut : {4}", Port, CstKey.ID, cstInfo.SlotCount, ReviewFailCount, AutoMoveOutCount);  // KYH 230913-01
                                    PioSend.YSendAble = true;
                                }
                                else if(GG.Equip.CtrlSetting.slotProcessCount == 1)
                                {
                                    Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "Slot Count 1개 전체검사 완료 Port {0}, CstKey.ID : {1} Slot Count : {2}, Review Fail : {3}, AutoMoveOut : {4}", Port, CstKey.ID, cstInfo.SlotCount, ReviewFailCount, AutoMoveOutCount);  // KYH 230913-01
                                }
                                else
                                {
                                    Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "Slot Process Count 이상 Port {0}, CstKey.ID : {1} Slot Count : {2}, Review Fail : {3}, AutoMoveOut : {4}", Port, CstKey.ID, cstInfo.SlotCount, ReviewFailCount, AutoMoveOutCount);  // KYH 230913-01

                                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0679_SLOT_PROCESS_COUNT_ABNORMAL);
                                }
                            }
                            else
                            {
                                if ((cstInfo.SlotCount == GG.Equip.CtrlSetting.slotProcessCount))
                                {
                                    //GG.Equip.CtrlSetting.slotProcessCount = 0;
                                    //마지막 장임으로 SendAble 미송신
                                    Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "전체검사 마지막장 Port {0}, CstKey.ID : {1} Slot Count : {2}, slotProcessCount : {3}", Port, CstKey.ID, cstInfo.SlotCount, GG.Equip.CtrlSetting.slotProcessCount);
                                }
                                else
                                {
                                    Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "전체검사 검사중 Port {0}, CstKey.ID : {1} Slot Count : {2}, slotProcessCount : {3}", Port, CstKey.ID, cstInfo.SlotCount, GG.Equip.CtrlSetting.slotProcessCount);
                                    PioSend.YSendAble = true; //전체 검사 모드일 때는 바로 LPM->ROBOT Send Able 시작
                                }
                            }
                        }
                        else // 한 장 검사, 직접 선택 모드
                        {
                            // 설비 내에 Wafer가 없을 경우 LPM에서 Send (한 장 빠졌을 경우 대기하기 위함)
                            if (equip.IsWaferDetect == EmGlassDetect.NOT && _efem.Aligner.Status.IsWaferExist == false && _efem.Robot.Status.IsLowerArmVacOn == false && _efem.Robot.Status.IsUpperArmVacOn == false)
                            {
                                Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "{0}_Step, Port : {1}, Wafer 위치 => AVI : {2}, Aligner : {3}, Lower Arm : {4}, Upper Arm : {5}",
                                _seqStepNum, Port, equip.IsWaferDetect, _efem.Aligner.Status.IsWaferExist, _efem.Robot.Status.IsLowerArmVacOn, _efem.Robot.Status.IsUpperArmVacOn);

                                PioSend.YSendAble = true; 
                            }
                            else // 설비 내에 Wafer가 하나라도 있는 경우 Recv로 대기
                            {
                                Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "{0}_Step, Port : {1}, Wafer 위치 => AVI : {2}, Aligner : {3}, Lower Arm : {4}, Upper Arm : {5}",
                                _seqStepNum, Port, equip.IsWaferDetect, _efem.Aligner.Status.IsWaferExist, _efem.Robot.Status.IsLowerArmVacOn, _efem.Robot.Status.IsUpperArmVacOn);

                                Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "{0}_Step {1} Wait Robot(Wafer) Back", _seqStepNum, Port);
                            }
                        }

                        //기존에 쓰던 방식
                        //if (TransferDataMgr.IsAllOut(this.CstKey) == false) //&& !(this.ProgressWay == EmProgressWay.OnlyLast || this.ProgressWay == EmProgressWay.OnlyFirst))
                        //{
                        //    Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "{0}_Step {1} All Out SendAble Off", _seqStepNum, Port);
                        //    PioSend.YSendAble = true;
                        //}

                        bool check = false;
                        EmEfemMappingInfo[] mappinginfo = Status.MappingData;
                        foreach (var item in mappinginfo)
                        {
                            if (item == EmEfemMappingInfo.Presence)
                            {
                                check = true;
                                break;
                            }
                        }
                        if (check == true && ((Port == EmEfemPort.LOADPORT1 && EFEMTactMgr.Instance.IsInsertWafer1 == true) || (Port == EmEfemPort.LOADPORT2 && EFEMTactMgr.Instance.IsInsertWafer2 == true)))
                        {
                            EFEMTactMgr.Instance.FilterData(Status.MappingData, _rfReadCstId, (int)Port);
                        }
                        Status.IsLoadButtonPushed = Status.IsUnloadButtonPushed = false;
                        _seqStepNum = EmEFEMLPMSeqStep.S540_WAIT_ROBOT_SEND_RECV_ABLE;
                    }
                    break;

                case EmEFEMLPMSeqStep.S535_REVIEW_JUDGE_ON:
                    // Joo // User 선택인 경우, 선택했던 모든 Wafer 검사 후 CST안에 들어가면(모든 Wafer 검사가 끝나면), 여기 시퀀스 진행
                    // Joo // 만약 재시도 했던 경우 => 결국 완료가 다 안된경우라 여기 들어올 수가 없음
                    
                    if (_DeepLearningReviewCompleteDelay.IsStart == false)
                    {
                        GG.ReviewJudgeWait = true;
                        _DeepLearningReviewCompleteDelay.Start(0, GG.Equip.CtrlSetting.Ctrl.DeepLearningDelayTime);
                    }

                    if (!GG.WaferTransfer_RobotToLPM) break; // KYH 230914 : WaferTransfer_RobotToLPM Flag Check
                    GG.WaferTransfer_RobotToLPM = false;
                    Logger.Log.AppendLine(LogLevel.Info, string.Format("[S535_REVIEW_JUDGE_ON] WaferTransfer_RobotToLPM : Pass & Reset"));

                    if (GG.Equip.CtrlSetting.NextInspCount == (ReviewFailCount + AutoMoveOutCount))  // KYH 230913-01
                    {
                        if (equip.HsmsPc.IsEventComplete(HSMS.EmHsmsPcEvent.AUTO_MOVE_OUT) && (equip.HsmsPc._IsAutoMoveOutIFComplete || GG.CimTestMode == true))
                        {
                            equip.HsmsPc._IsAutoMoveOutIFComplete = false;

                            //All Review Fail 발생 시
                            Logger.Log.AppendLine(LogLevel.Info, string.Format("[S535_REVIEW_JUDGE_ON - 1] 설정한 검사 횟수 : {0}, Review Fail 횟수 {1}, Auto Move Out 횟수 {2} => 검사횟수가 설정한 값과 동일함으로 Auto MoveOut 진행", GG.Equip.CtrlSetting.NextInspCount, ReviewFailCount, AutoMoveOutCount));  // KYH 230913-01
                            Logger.Log.AppendLine(LogLevel.Info, "Is Auto Move Out If Complete = false");

                            ReviewFailCount = 0;  // KYH 230913-01
                            AutoMoveOutCount = 0;  // KYH 230913-01
                            switch(Port)
                            {
                                case EmEfemPort.LOADPORT1:
                                    GG.Equip.CtrlSetting.ReviewFailCount1 = 0;
                                    GG.Equip.CtrlSetting.AutoMoveOutCount1 = 0;
                                    break;

                                case EmEfemPort.LOADPORT2:
                                    GG.Equip.CtrlSetting.ReviewFailCount2 = 0;
                                    GG.Equip.CtrlSetting.AutoMoveOutCount2 = 0;
                                    break;
                            }
                            GG.Equip.CtrlSetting.Save();

                            _seqStepNum = EmEFEMLPMSeqStep.S610_UNLD_FOUP;
                            DeepLearningReviewJudge = "";  // KYH 230913 : 최종 Clear
                        }
                    }
                    else
                    {
                        if (equip.HsmsPc.IsEventComplete(HSMS.EmHsmsPcEvent.AUTO_MOVE_OUT) && (equip.HsmsPc._IsAutoMoveOutIFComplete || GG.CimTestMode == true))
                        {
                            Logger.Log.AppendLine(LogLevel.Info, "Review Judge : {0}", DeepLearningReviewJudge);
                            equip.HsmsPc._IsAutoMoveOutIFComplete = false;
                            Logger.Log.AppendLine(LogLevel.Info, "Is Auto Move Out If Complete = false");

                            if (DeepLearningReviewJudge.Equals("R")) // Review
                            {
                                _DeepLearningReviewCompleteDelay.Stop();
                                GG.ReviewJudgeWait = false;

                                //진행 웨이퍼 갱신 후 S500_PROCESSING 스탭으로
                                _seqStepNum = EmEFEMLPMSeqStep.S500_PROCESSING;
                                DeepLearningReviewJudge = "";  // KYH 230913 : 최종 Clear
                            }
                            else if (DeepLearningReviewJudge.Equals("A")) // Auto MoveOut
                            {
                                _DeepLearningReviewCompleteDelay.Stop();
                                GG.ReviewJudgeWait = false;

                                // 딥러닝 컴플리트 신호 수신 했을 때, Auto Move Out
                                _seqStepNum = EmEFEMLPMSeqStep.S610_UNLD_FOUP;
                                DeepLearningReviewJudge = "";  // KYH 230913 : 최종 Clear
                            }
                            else
                            {
                                // Joo // 60초 안에 판정 못받으면 알람 발생
                                if (_DeepLearningReviewCompleteDelay)
                                {
                                    _DeepLearningReviewCompleteDelay.Stop();
                                    GG.ReviewJudgeWait = false;

                                    // Joo // 딥러닝 리뷰 판정 Auto MoveOut, Review 둘다 아닐 경우
                                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0945_DEEP_LEARNING_COMPLETE_OVERTIME);
                                    Logger.Log.AppendLine(LogLevel.Warning, string.Format("[Auto MoveOut] 딥러닝 리뷰 판정 Error 발생 - Auto MoveOut or Review 가 아닙니다. 판정 Data: {0} , Port: {1}, CST ID: {2}", DeepLearningReviewJudge, Port, this.CstKey.ID));
                                    _seqStepNum = EmEFEMLPMSeqStep.S500_PROCESSING;
                                    DeepLearningReviewJudge = "";  // KYH 230913 : 최종 Clear
                                    GG.AutoMoveOutReceive = false; // KYH AutoMoveOutReceive Clear 230913 : AutoMoveOutReceive Reset
                                }
                            }
                        }

                        // Joo // 60초 안에 판정 못받으면 알람 발생
                        if ((GG.Equip.CimMode == EmCimMode.OffLine || GG.Equip.CimMode == EmCimMode.Local) && _DeepLearningReviewCompleteDelay)
                        {
                            _DeepLearningReviewCompleteDelay.Stop();
                            // DeepLearningReviewJudge = "";  // KYH 230913
                            GG.ReviewJudgeWait = false;
                            //GG.AutoMoveOutReceive = false; // 230912 KYH : WaferUnload 이후 리셋

                            // Joo // 딥러닝 리뷰 판정 Auto MoveOut, Review 둘다 아닐 경우
                            AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0945_DEEP_LEARNING_COMPLETE_OVERTIME);
                            Logger.Log.AppendLine(LogLevel.Warning, string.Format("[Auto MoveOut] 딥러닝 리뷰 판정 Error 발생 - Auto MoveOut or Review 가 아닙니다. 판정 Data: {0} , Port: {1}, CST ID: {2}", DeepLearningReviewJudge, Port, this.CstKey.ID));
                            _seqStepNum = EmEFEMLPMSeqStep.S610_UNLD_FOUP;
                            DeepLearningReviewJudge = "";  // KYH 230913 : 최종 Clear
                            GG.AutoMoveOutReceive = false; // KYH AutoMoveOutReceive Clear 230913 : AutoMoveOutReceive Reset
                        }
                    }
                    break;
                case EmEFEMLPMSeqStep.S540_WAIT_ROBOT_SEND_RECV_ABLE:
                    if (PioRecv.XSendAble == true)
                    {
                        PioRecv.YRecvAble = true;
                        PioRecv.YRecvStart = true;
                        PioRecv.YRecvComplete = false;
                        _seqStepNum = EmEFEMLPMSeqStep.S570_WAIT_ROBOT_RECV_COMPLETE;
                    }
                    else if (PioSend.XRecvAble == true)
                    {
                        GG.Equip.CtrlSetting.slotProcessCount++;
                        GG.Equip.CtrlSetting.Save();
                        PioSend.YSendAble = true;
                        PioSend.YSendStart = true;
                        PioSend.YSendComplete = false;
                        _seqStepNum = EmEFEMLPMSeqStep.S550_WAIT_ROBOT_SEND_COMPLETE;
                    }
                    else if (Status.IsLoadButtonPushed || Status.IsUnloadButtonPushed)
                    {
                        Status.IsLoadButtonPushed = Status.IsUnloadButtonPushed = false;
                        if (_efem.Robot.UpperWaferKey.CstID == CstKey.ID
                            || _efem.Robot.LowerWaferKey.CstID == CstKey.ID
                            || _efem.Aligner.LowerWaferKey.CstID == CstKey.ID
                            || GG.Equip.TransferUnit.LowerWaferKey.CstID == CstKey.ID)
                            InterLockMgr.AddInterLock(GG.boChinaLanguage ? "进行中的 CST无法排出." : "진행 중인 카세트는 배출할 수 없습니다.");
                        else
                        {
                            PioSend.Initailize();
                            if (_efem.Robot.TargetLoadPort == (int)this.Port)
                                _efem.Robot.SetTargetLoadPort(0);
                            _seqStepNum = EmEFEMLPMSeqStep.S610_UNLD_FOUP;
                        }
                    }
                    break;
                #region Wafer In
                case EmEFEMLPMSeqStep.S550_WAIT_ROBOT_SEND_COMPLETE:
                    if (PioSend.XRecvAble == true && PioSend.XRecvStart == true && PioSend.XRecvComplete == true)
                    {
                        PioSend.YSendAble = true;
                        PioSend.YSendStart = true;
                        PioSend.YSendComplete = true;
                        _seqStepNum = EmEFEMLPMSeqStep.S560_WAIT_RECV_SIGNAL_CLEAR;
                    }
                    break;
                case EmEFEMLPMSeqStep.S560_WAIT_RECV_SIGNAL_CLEAR:
                    if (PioSend.XRecvAble == false && PioSend.XRecvStart == false && PioSend.XRecvComplete == false)
                    {
                        PioSend.Initailize();
                        _seqStepNum = EmEFEMLPMSeqStep.S590_WAFER_IN_OUT_DELAY;
                    }
                    break;
                #endregion Wafer IN
                #region Wafer Out
                case EmEFEMLPMSeqStep.S570_WAIT_ROBOT_RECV_COMPLETE:
                    if (PioRecv.XSendAble == true && PioRecv.XSendStart == true && PioRecv.XSendComplete == true)
                    {
                        PioRecv.YRecvAble = true;
                        PioRecv.YRecvStart = true;
                        PioRecv.YRecvComplete = true;
                        _seqStepNum = EmEFEMLPMSeqStep.S580_WAIT_SEND_SIGNAL_CLEAR;
                    }
                    break;
                case EmEFEMLPMSeqStep.S580_WAIT_SEND_SIGNAL_CLEAR:
                    if (PioRecv.XSendAble == false && PioRecv.XSendStart == false && PioRecv.XSendComplete == false)
                    {
                        PioRecv.Initailize();
                        //EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T000_LPM_OPEN_END, ((int)Port).ToString());
                        _seqStepNum = EmEFEMLPMSeqStep.S590_WAFER_IN_OUT_DELAY;
                    }
                    break;
                #endregion Wafer Out
                case EmEFEMLPMSeqStep.S590_WAFER_IN_OUT_DELAY:
                    UpdateNextWaferKey();
                    _waferUnldWait.Start(0, 1000);
                    _seqStepNum = EmEFEMLPMSeqStep.S600_DELAY_WAIT;
                    break;
                case EmEFEMLPMSeqStep.S600_DELAY_WAIT:
                    if (_waferUnldWait)
                    {
                        if (((equip.IsSkipWaferLPM == 1 && this.Port == EmEfemPort.LOADPORT1) || (equip.IsSkipWaferLPM == 2 && this.Port == EmEfemPort.LOADPORT2)) && this.ProgressWay != EmProgressWay.User)
                        {
                            _frmRetryMsg.RequestPopup(GG.boChinaLanguage ? "用户选择" : "사용자 선택", GG.boChinaLanguage ? "晶片未进行检查，通过'排出'按钮返回。ㅣ\n 请在'进行下一个晶片'或'排出盒'中选择。" : "웨이퍼가 검사를 진행하지 않고 '배출' 버튼을 통해 복귀하였습니다ㅣ\n'다음 장 웨이퍼 진행' 또는 '카세트 배출' 중 선택 해주세요");
                            _seqStepNum = EmEFEMLPMSeqStep.S605_USER_CONFIRM_NEXT_WAFER;
                        }
                        else
                        {
                            _seqStepNum = EmEFEMLPMSeqStep.S500_PROCESSING;
                        }
                    }
                    break;
                case EmEFEMLPMSeqStep.S605_USER_CONFIRM_NEXT_WAFER:
                    if (_frmRetryMsg.PopupFlow == EmPopupFlow.OK)
                    {
                        if (_frmRetryMsg.IsRetry)//'다음장 웨이퍼 진행' 클릭
                        {
                            Logger.Log.AppendLine(LogLevel.Info, "웨이퍼 검사 진행하지 않고 배출 선택 후 다음장 자동 진행");
                            //1. 다음에 진행할 웨이퍼 있는지 확인
                            int skipIdx = equip.IsSkipWaferSlotNo - 1;
                            int NextIdx = -1;
                            if (this.ProgressWay == EmProgressWay.OnlyLast)
                            {
                                for (int i = skipIdx; i >= 0; i--)
                                {
                                    if(i == 0)
                                    {
                                        continue;
                                    }
                                    if (Status.MappingData[i - 1] == EmEfemMappingInfo.Presence)
                                    {
                                        NextIdx = i - 1;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                for (int i = skipIdx; i < Status.MappingData.Length; i++)
                                {
                                    if (i == 12)
                                    {
                                        continue;
                                    }
                                    if (Status.MappingData[i + 1] == EmEfemMappingInfo.Presence)
                                    {
                                        NextIdx = i + 1;
                                        break;
                                    }
                                }
                            }

                            if (NextIdx == -1 || NextIdx == 13)
                            {
                                //마지막 웨이퍼이므로 오류 메시지 출력 후 배출
                                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "没有要进行的 Wafer会自动排出" : "진행할 웨이퍼가 없어 자동 배출 됩니다");
                                equip.IsSkipWaferLPM = -1;
                                equip.IsSkipWaferSlotNo = -1;
                                Logger.Log.AppendLine(LogLevel.Info, "웨이퍼 검사 진행하지 않고 배출 선택 후 해당 카세트 배출");
                                _seqStepNum = EmEFEMLPMSeqStep.S610_UNLD_FOUP;
                                break;
                            }
                            //2. 웨이퍼 out, comback 갱신
                            if (this.ProgressWay == EmProgressWay.OnlyLast)
                            {
                                for (int i = skipIdx; i >= 0; i--)
                                {
                                    _tempWaferInfo = TransferDataMgr.GetWafer(this.CstKey.ID, i+1);
                                    if (i == NextIdx)
                                    {
                                        _tempWaferInfo.IsComeBack = false;
                                        _tempWaferInfo.IsOut = false;
                                        _tempWaferInfo.IsAlignComplete = false;
                                        _tempWaferInfo.IsInspComplete = false;
                                        _tempWaferInfo.IsReviewComplete = false;
                                        _tempWaferInfo.Update();
                                        break;
                                    }
                                    else
                                    {
                                        _tempWaferInfo.IsComeBack = true;
                                        _tempWaferInfo.IsOut = true;
                                        _tempWaferInfo.Update();
                                    }
                                }
                            }
                            //2.5 만약 전체mapping 검사여서 로봇이 이미 다음장 하부암으로 떠있을경우? 
                            else if(this.ProgressWay == EmProgressWay.Mapping)
                            {
                                // => 그냥 진행하면 됨
                            }
                            else //현재 21년 5월 5일 기준으로 OnlyFirst 경우일 경우에만 제대로 동작함, 특정웨이퍼 직접 진행일 떄에는 시퀀스가 꼬임
                            {
                                for (int i = skipIdx; i < Status.MappingData.Length; i++)
                                {
                                    _tempWaferInfo = TransferDataMgr.GetWafer(this.CstKey.ID, i+1);
                                    if (i == NextIdx)
                                    {
                                        _tempWaferInfo.IsComeBack = false;
                                        _tempWaferInfo.IsOut = false;
                                        _tempWaferInfo.IsAlignComplete = false;
                                        _tempWaferInfo.IsInspComplete = false;
                                        _tempWaferInfo.IsReviewComplete = false;
                                        _tempWaferInfo.Update();
                                        break;
                                    }
                                    else
                                    {
                                        _tempWaferInfo.IsComeBack = true;
                                        _tempWaferInfo.IsOut = true;
                                        _tempWaferInfo.Update();
                                    }
                                    
                                }
                            }

                            equip.IsSkipWaferLPM = -1;
                            equip.IsSkipWaferSlotNo = -1;
                            _seqStepNum = EmEFEMLPMSeqStep.S500_PROCESSING;
                        }
                        else
                        {
                            equip.IsSkipWaferLPM = -1;
                            equip.IsSkipWaferSlotNo = -1;
                            Logger.Log.AppendLine(LogLevel.Info, "웨이퍼 검사 진행하지 않고 배출 선택 후 해당 카세트 배출");
                            _seqStepNum = EmEFEMLPMSeqStep.S610_UNLD_FOUP;
                        }
                    }
                    break;
            }
        }
        private void FoupUnldLogic(Equipment equip)
        {
            SeqLogging(equip);

            switch (_seqStepNum)
            {
                case EmEFEMLPMSeqStep.S610_UNLD_FOUP:
                    EFEMTactMgr.Instance.CloseStart(_rfReadCstId);
                    if (_efem.Proxy.StartCommand(equip, Port, EmEfemCommand.CLOSE) == false) return;
                    ReviewFailCount = 0;  // KYH 230913-01
                    AutoMoveOutCount = 0; // KYH 230913-01
                    switch (Port)
                    {
                        case EmEfemPort.LOADPORT1:
                            GG.Equip.CtrlSetting.ReviewFailCount1 = 0;
                            GG.Equip.CtrlSetting.AutoMoveOutCount1 = 0;
                            break;

                        case EmEfemPort.LOADPORT2:
                            GG.Equip.CtrlSetting.ReviewFailCount2 = 0;
                            GG.Equip.CtrlSetting.AutoMoveOutCount2 = 0;
                            break;
                    }

                    GG.Equip.CtrlSetting.slotProcessCount = 0;
                    GG.Equip.CtrlSetting.Save();
                    _seqStepNum = EmEFEMLPMSeqStep.S620_WAIT_LPM_CLOSE;
                    break;
                case EmEFEMLPMSeqStep.S620_WAIT_LPM_CLOSE:
                    if (_efem.Proxy.IsComplete(equip, Port, EmEfemCommand.CLOSE) == true)
                    {
                        if (GG.TestMode)
                        {
                            this.Status.IsDoorOpen = false;
                            this.Status.IsDoorClose = true;
                        }
                        _tempCstInfo = TransferDataMgr.GetCst(this.CstKey);
                        _tempCstInfo.IsProgressing = false;
                        _tempCstInfo.Update();

                        _seqStepNum = EmEFEMLPMSeqStep.S630_CHECK_CST_ID;
                    }
                    break;
                //재시작 시 카세트가 감지되어 있어도 이 스텝으로 옴, RFID시작 전 이나 올려놓고 시작하면 카세트키가 없음
                //따라서 단지, 카세트 감지되어 있을 때 재시작 되어 온건지, 실제로 카세트 언로드 되는건지 구분 해야함.
                case EmEFEMLPMSeqStep.S630_CHECK_CST_ID:
                    //_seqStepNum = EmEFEMLPMSeqStep.S690_OHT_ULD_COMMUNICATION_START;
                    //break;
                    _tempCstInfo = TransferDataMgr.GetCst(CstKey);
                    //카세트 키 체크
                    if (_tempCstInfo == null || _tempCstInfo.CstID =="")
                    {
                        //OHT 모드라면 이 스텝은 타지 않아야 정상이지만 비정상적으로 물류 정보가 없어지거나 했다면 알람 발생 시키고 조치해야함
                        //0709 투입 후 카세트 등록 전에 문제 발생하면 아직 재시작시 Cst ID가 null일수 있으므로 일단은..
                        if (this.LoadType == EmLoadType.OHT)
                        {
                            _seqStepNum = EmEFEMLPMSeqStep.S300_LOAD_FOUP;
                            //InterLockMgr.AddInterLock("OHT를 통해 투입된 카세트가 아닙니다. 메뉴얼 로딩/언로딩 모드로 바꾼 후 진행 해주세요");
                            //AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0647_EFEM_LPM1_CST_EXIST_BUT_NO_CST_DATA);
                        }
                        //아닌 경우는 자동으로 시작 하면 됨(카세트가 없으므로 정상 시퀀스 이어서 진행)
                        else
                        {
                            _seqStepNum = EmEFEMLPMSeqStep.S100_RECV_FOUP;
                        }
                    }
                    //1. 정상 물류 진행 시, 2. 재시작 하였는데 카세트가 존재하고 카세트키가 있을 때
                    else
                    {
                        EFEMTactMgr.Instance.CloseEnd(_rfReadCstId);
                        IsAlreadyOpen = false;
                        Logger.TacttimeLog.AppendLine(LogLevel.NoLog, "[EFEM TACT] {0} CLOSE COMPLETE==============================================", Port);
                        if(this.LoadType == EmLoadType.Manual)
                        {
                            equip.BuzzerK4.OnOff(equip, true);
                        }
                        
                        Status.IsUnloadButtonPushed = false;
                        Status.IsLoadButtonPushed = false;

                        _seqStepNum = EmEFEMLPMSeqStep.S640_CASSETTE_UNLOAD_REPORT;
                    }
                    break;
                case EmEFEMLPMSeqStep.S640_CASSETTE_UNLOAD_REPORT:
                    if (this.LoadType == EmLoadType.OHT) //물류
                    {
                        if(_UnloadReportDelay.IsStart == false)
                        {
                            if (equip.HsmsPc.StartCommand(equip, HSMS.EmHsmsPcCommand.CASSETTE_UNLOAD, CstKey) == false) return;
                            Logger.CSTHistoryLog.AppendLine(LogLevel.Info, string.Format("[카세트 언로드] 보고 : {0}", _rfReadCstId));
                            _isCsUldtReportEnd = true;

                            _UnloadReportDelay.Start(0, 1000);
                        }
                        else
                        {
                            if (equip.HsmsPc.IsCommandAck(HSMS.EmHsmsPcCommand.PORT_MODE_CHANGE) && _UnloadReportDelay)
                            {
                                _UnloadReportDelay.Stop();

                                HsmsPortInfo info = new HsmsPortInfo();
                                info.CstID = _tempCstInfo.CstID;
                                info.IsCstExist = true;
                                info.LoadportNo = (int)Port;
                                info.PortMode = PortMode.UNLOAD_REQUEST;

                                if (equip.HsmsPc.StartCommand(equip, HSMS.EmHsmsPcCommand.PORT_MODE_CHANGE, info) == false) return;
                                _seqStepNum = EmEFEMLPMSeqStep.S690_OHT_ULD_COMMUNICATION_START;
                            }
                        }
                    }
                    else//비물류
                    {
                        if (equip.HsmsPc.StartCommand(equip, HSMS.EmHsmsPcCommand.CASSETTE_UNLOAD, CstKey) == false) return;
                        Logger.CSTHistoryLog.AppendLine(LogLevel.Info, string.Format("[카세트 언로드] 보고 : {0}", _rfReadCstId));
                        _isCsUldtReportEnd = true;

                        //완료 알람 팝업
                        CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? string.Format("{0} CST 全体检查完毕", this.Port.ToString()) : string.Format("{0} 카세트 전체 검사 완료", this.Port.ToString()));
                        Logger.Log.AppendLine("비물류 모드 / {0} 카세트 전체 완료 / Cst ID : {1}", this.Port.ToString(), _tempCstInfo.CstID);

                        _seqStepNum = EmEFEMLPMSeqStep.S645_CASSETTE_UNLOAD_REPORT_COMPLETE_WAIT;
                    }
                    break;
                #region ManualULD
                case EmEFEMLPMSeqStep.S645_CASSETTE_UNLOAD_REPORT_COMPLETE_WAIT:
                    if (equip.HsmsPc.IsCommandAck(HSMS.EmHsmsPcCommand.CASSETTE_UNLOAD))
                    {
                        TransferDataMgr.DeleteCst(_tempCstInfo.CstID);
                        TransferDataMgr.DeleteWafer(_tempCstInfo.CstID);

                        _seqStepNum = EmEFEMLPMSeqStep.S650_WAIT_UNLD_BUTTON_PUSH;
                    }
                    break;
                case EmEFEMLPMSeqStep.S650_WAIT_UNLD_BUTTON_PUSH:
                    
                    if (Status.IsUnloadButtonPushed || Status.IsLoadButtonPushed || (GG.EfemLongRun && _isZeroCst == false))
                    {
                        _efem.LPMLightCurtain.StartMuting();
                        _seqStepNum = EmEFEMLPMSeqStep.S660_NOTIFY_USER;
                    }
                    if (GG.EfemLongRun == true)
                        equip.BuzzerK4.OnOff(equip, false);
                    break;
                case EmEFEMLPMSeqStep.S660_NOTIFY_USER:
                    if (GG.PassLightCurtain || _efem.LPMLightCurtain.IsMuting == true)
                    {
                        Status.IsUnloadButtonPushed = false;
                        Status.IsLoadButtonPushed = false;
                        equip.BuzzerK4.OnOff(equip, false);
                        _seqStepNum = EmEFEMLPMSeqStep.S670_WAIT_UNLD_BUTTON_PUSH;
                    }
                    break;
                case EmEFEMLPMSeqStep.S670_WAIT_UNLD_BUTTON_PUSH:
                    if (Status.IsUnloadButtonPushed || Status.IsLoadButtonPushed || GG.EfemLongRun)
                    {
                        Status.IsUnloadButtonPushed = false;
                        Status.IsLoadButtonPushed = false;
                        if (GG.TestMode == true)
                            Status.IsFoupExist = false;

                        ProcessCstOut();

                        _unldDelay.Start(0, 3000);
                        _seqStepNum = EmEFEMLPMSeqStep.S680_WAIT_UNLD_FOUP;
                    }
                    break;
                case EmEFEMLPMSeqStep.S680_WAIT_UNLD_FOUP:
                    if (_unldDelay == true)
                    {
                        _isPassMutingCheckAtLoad = Status.IsFoupExist;
                        _efem.LPMLightCurtain.StopMuting();
                        _unldDelay.Stop();
                        _seqStepNum = EmEFEMLPMSeqStep.S100_RECV_FOUP;
                    }
                    break;
                #endregion
                #region OHT
                case EmEFEMLPMSeqStep.S690_OHT_ULD_COMMUNICATION_START:
                    // OHT ULD Start
                    if(OHTpio.OHTStart(EmOHTtype.UNLOAD))
                    {
                        equip.IsOhtUnloadRunning = true;
                        _seqStepNum = EmEFEMLPMSeqStep.S700_OHT_ULD_COMPLETE_WAIT;
                    }
                    break;
                case EmEFEMLPMSeqStep.S700_OHT_ULD_COMPLETE_WAIT:
                    if (/*Status.IsFoupExist == false &&*/ OHTpio.IsComplete(EmOHTtype.UNLOAD))
                    {
                        equip.IsOhtUnloadRunning = false;

                        if (Status.IsFoupExist)
                        {
                            if(Port == EmEfemPort.LOADPORT1)
                                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0873_LPM1_OHT_UNLOAD_COMPLETE_BUT_CST_EXIST);
                            else
                                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0874_LPM2_OHT_UNLOAD_COMPLETE_BUT_CST_EXIST);
                        }
                        else
                        {
                            HsmsPortInfo info = new HsmsPortInfo();
                            info.CstID = "";
                            info.IsCstExist = false;
                            info.LoadportNo = (int)Port;
                            info.PortMode = PortMode.UNLOAD_COMPLETE;

                            if (equip.HsmsPc.StartCommand(equip, HSMS.EmHsmsPcCommand.PORT_MODE_CHANGE, info) == false) return;

                            ProcessCstOut();

                            _seqStepNum = EmEFEMLPMSeqStep.S100_RECV_FOUP;
                        }
                    }
                    break;
                #endregion
            }
        }
        
        protected override bool IsSafeToHome()
        {
            return _efem.Robot.IsHomeComplete == true
                && _efem.Robot.Status.IsArmUnFold == false
                ;
        }

        public object[] GetData()
        {
            try
            {
                object[] result = new object[3];
                //Mapping Data

                var mappingData = new int[Status.MappingData.Length];
                if ((Status.IsFoupExist && this.CstKey.ID != "") && (!IsOpenReadyStep || !IsOpenningStep))
                {
                    Array.Copy(Status.MappingData, mappingData, mappingData.Length);
                }
                result[0] = mappingData;

                //Progressing Wafer
                var listWafers = CurrentWafers();
                var ProgressingData = new int[listWafers.Count];
                if (listWafers.Count > 0 && (!IsOpenReadyStep || !IsOpenningStep))
                {
                    for (int i = 0; i < listWafers.Count; i++)
                    {
                        ProgressingData[i] = listWafers[i].SlotNo;
                    }
                }
                result[1] = ProgressingData;

                //Inspection Complete Wafer
                listWafers = TransferDataMgr.GetWafersIn(CstKey.ID);
                var isComplete = new bool[3, 13];
                if (Status.IsFoupExist == true)
                {
                    int count = listWafers.Count > 13 ? 13 : listWafers.Count;
                    for (int i = 0; i < count; i++)
                    {
                        isComplete[0, i] = listWafers[i].IsAlignComplete;
                    }
                    for (int i = 0; i < count; i++)
                    {
                        isComplete[1, i] = listWafers[i].IsInspComplete;
                    }
                    for (int i = 0; i < count; i++)
                    {
                        isComplete[2, i] = listWafers[i].IsComeBack && listWafers[i].OutputDate.Year != 1;
                    }
                }

                result[2] = isComplete;

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
    public class LampCheck
    {
        public bool IsRunning;
        public EmEfemLampType Type;
        public EmEfemLampBuzzerState State;

        public void Start()
        {
            IsRunning = true;
        }

        private bool _oldState;
        private Stopwatch _blinkSw = new Stopwatch();
        public void StateCheckLogic(EFEMLPMUnit lpm)
        {
            if (_oldState != lpm.Status.IsLampOn(Type))
            {
                _blinkSw.Restart();
            }
            else if (_blinkSw.ElapsedMilliseconds < 500)
            {
                if (_blinkSw.IsRunning == false)
                    _blinkSw.Restart();
                State = EmEfemLampBuzzerState.BLINK;
            }
            else if (_blinkSw.ElapsedMilliseconds > 1500)
            {
                State = (EmEfemLampBuzzerState)(lpm.Status.IsLampOn(Type) ? 1 : 0);
            }

            _oldState = lpm.Status.IsLampOn(Type);
        }
    }
}
