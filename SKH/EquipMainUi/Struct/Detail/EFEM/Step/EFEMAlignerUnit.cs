using EquipMainUi.Log;
using EquipMainUi.RecipeManagement;
using EquipMainUi.Struct.Detail.HSMS;
using EquipMainUi.Struct.Step;
using EquipMainUi.Struct.TransferData;
using EquipMainUi.UserMessageBoxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EquipMainUi.Struct.Detail.EFEM.Step
{
    public enum EmEFEMAlignerHomeStep
    {
        H000_END,
        H010_ALIGNER_RESET,
        H020_WAIT_ALIGNER_RESET,
        H030_WAIT_ALIGNER_INIT,
        H040_CHECK_ALIGNER_STATUS,
        H100_HOME_COMPLETE,
    }
    public enum EmEFEMAlignerSeqStep
    {
        S000_END,
        S100_CHECK_ALIGNER,
        S200_LD_WAFER_START,
        S210_WAIT_ROBOT_SEND,
        S220_WAIT_ROBOT_SEND_COMPLETE,
        S230_WAIT_ROBOT_SEND_SIGNAL_CLEAR,
        
        S240_ALIGN_START,
        S250_INIT_WAIT_BEFORE_ALIGN,
        S260_USER_INPUT_WAIT,
        S270_GO_READY_POS,
        S280_WAIT_READY_POS,



        S290_ROTATION_COMMAND,
        S300_WAIT_ROTATION_COMPLETE,
        S310_OCR_READ_START,
        S320_OCR_READ_COMMAND,
        S330_WAIT_READ_OCR,
        S340_OCR_USER_CHECK,
        S350_OCR_ID_CHECK,
        S360_START_READ_BCR,
        S370_BCR_READ_COMMAND,
        S380_WAIT_READ_BCR,
        S390_BCR_USER_CHECK,
        S400_BCR_ID_CHECK,

        S410_WAFER_LOAD_REPORT,
        S420_WAFER_LOAD_CONFIRM_WAIT,
        S430_WAFER_MAP_REQUEST_REPORT,
        S440_WAFER_MAP_DOWNLOAD_WAIT,

        S450_START_ALIGN,
        S460_ALIGN_WAIT,

        S470_UNLOAD_WAFER_START,
        S480_SEND_READY_WAIT,
        S490_SEND_ABLE_ON,
        S500_ROBOT_RECV_ABLE_WAIT,
        S510_WAIT_ROBOT_RECV_COMPLETE,
        S520_WAIT_ROBOT_RECV_SIGNAL_CLEAR,
    }
    public class EFEMAlignerUnit : StepBase
    {
        private EFEMUnit _efem;

        private EmEFEMAlignerHomeStep _homeStepNum = 0;
        private EmEFEMAlignerHomeStep _homeStepNumOld = 0;
        public EmEFEMAlignerHomeStep HomeStepNum { get { return _homeStepNum; } }

        private EmEFEMAlignerSeqStep _seqStepNum = 0;
        private EmEFEMAlignerSeqStep _seqStepNumOld = 0;
        public EmEFEMAlignerSeqStep SeqStepNum { get { return _seqStepNum; } }

        public List<EmEFEMAlignerSeqStep> LstCycleStopStep = new List<EmEFEMAlignerSeqStep>();
        public EFEMAlignerStatus Status { get { return _efem.Proxy.AlignerStat; } }
        public PlcTimerEx MapDownloadWaitTimer = new PlcTimerEx("Wafer Map Request Wait Timer");
        public PlcTimerEx _WaferLoadCinfirmDelay = new PlcTimerEx("Wafer Load Confirm Wait Timer");
        public EmHsmsWaferMapNG WaferMapNgWay { get; set; }
        public bool IsInitDone
        {
            get
            {
                return
                    Status.IsStatus == EmEfemAlignerStatus.READY
                    ;
            }
        }

        private bool _checkForcedComeback;
        private bool _isForcedComback;
        public bool IsForcedOut { get { return _isForcedComback; } }
        public void SetSeq(EmEFEMAlignerSeqStep step) { _seqStepNum = step; }

        private Timer _popupTimer = new Timer();
        private FrmUserSelectReadData _frmOcrUserMsgBox;
        private FrmUserSelectReadData _frmBcrUserMsgBox;
        private FrmRetryUserSelect _frmRetryMsg;
        private PlcTimerEx _ocrTimeover = new PlcTimerEx("OCR TIMEOVER", false);
        private PlcTimerEx _bcrTimeover = new PlcTimerEx("BCR TIMEOVER", false);


        public EFEMAlignerUnit(EFEMUnit efem)
        {
            _efem = efem;

            PioRecv = new PioSignalRecv() { Name = "Aligner-Robot-Recv" };
            PioSend = new PioSignalSend() { Name = "Aligner-Robot-Send" };

            LstCycleStopStep.Add(EmEFEMAlignerSeqStep.S000_END);
        }

        public void InitUserInterface(Equipment equip)
        {
            _frmOcrUserMsgBox = new FrmUserSelectReadData(equip, EmEfemPort.ALIGNER, equip.OCR);
            _frmBcrUserMsgBox = new FrmUserSelectReadData(equip, EmEfemPort.ALIGNER, equip.BCR1, equip.BCR2);
            _frmRetryMsg = new FrmRetryUserSelect();

            _popupTimer.Tick += _popupTimer_Tick;
            _popupTimer.Interval = 1000;
            _popupTimer.Start();
        }
        private void _popupTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_frmOcrUserMsgBox.PopupFlow == EmPopupFlow.PopupRequest)
                {
                    _frmOcrUserMsgBox.StartPosition = FormStartPosition.CenterScreen;
                    _frmOcrUserMsgBox.PopupFlow = EmPopupFlow.UserWait;
                    _frmOcrUserMsgBox.Show();
                }

                if (_frmBcrUserMsgBox.PopupFlow == EmPopupFlow.PopupRequest)
                {
                    _frmBcrUserMsgBox.StartPosition = FormStartPosition.CenterScreen;
                    _frmBcrUserMsgBox.PopupFlow = EmPopupFlow.UserWait;
                    _frmBcrUserMsgBox.Show();
                }

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
        public override void LogicWorking(Equipment equip)
        {
            StatusLogicWorking(equip);
            SeqLogicWorking(equip);
        }
        Dit.Framework.PeriodChecker _statPeriod = new Dit.Framework.PeriodChecker();
        public override void StatusLogicWorking(Equipment equip)
        {
            if(GG.IsDitPreAligner)
            {
                bool ctrlRunning, ctrlReady, visionRunning, visionReady;

                //vacuum에 대한 처리..?
                ctrlRunning = false;
                ctrlReady =
                    equip.AlignerX.IsHomeCompleteBit == true
                    && equip.AlignerY.IsHomeCompleteBit == true
                    && equip.AlignerT.IsHomeCompleteBit == true;
                if(GG.TestMode)
                {
                    ctrlReady = true;
                }

                visionRunning = false;
                visionReady = equip.PreAligner.IsCamReady && equip.PreAligner.IsLightReady;
                if (GG.TestMode)
                {
                    visionReady = true;
                }

                if (ctrlRunning || visionRunning)
                {
                    equip.Efem.Aligner.Status.IsStatus = EmEfemAlignerStatus.RUNNING;
                }
                else if (ctrlReady && visionReady)
                {
                    equip.Efem.Aligner.Status.IsStatus = EmEfemAlignerStatus.READY;
                }
                else
                {
                    equip.Efem.Aligner.Status.IsStatus = EmEfemAlignerStatus.ERROR;
                }

                Status.Mode = EmEfemAlignerMode.Auto;
                Status.IsWaferExist = equip.AlignerWaferDetect.IsOn;
            }
            else
            {
                if (_statPeriod.IsTimeToCheck(_efem.StatReadTime))
                {
                    _statPeriod.Reset = true;

                    if (GG.TestMode == true)
                        return;

                    if (_efem.Proxy.IsRunning(equip, EmEfemPort.ALIGNER, EmEfemCommand.STAT_) == false)
                        _efem.Proxy.StartCommand(equip, EmEfemPort.ALIGNER, EmEfemCommand.STAT_);
                }
            }
        }
        public override void SeqLogicWorking(Equipment equip)
        {
            SeqStepStr = _seqStepNum.ToString();
            HomeStepStr = HomeStepNum.ToString();

            if (_seqStepNum != _seqStepNumOld)
                _seqStepNumOld = _seqStepNum;

            if (_homeStepNum != _homeStepNumOld)
                _homeStepNumOld = _homeStepNum;

            base.LogicWorking(equip);

            if (_efem.RunMode == EmEfemRunMode.Stop)
            {
                Stop();

                _seqStepNum = EmEFEMAlignerSeqStep.S000_END;
                _homeStepNum = EmEFEMAlignerHomeStep.H000_END;
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

                    WaferLdLogic(equip);
                    AlignLogic(equip);
                    WaferUnldLogic(equip);
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

        private void WaferLdLogic(Equipment equip)
        {
            SeqLogging(equip);

            switch (_seqStepNum)
            {
                case EmEFEMAlignerSeqStep.S000_END:
                    _seqStepNum = EmEFEMAlignerSeqStep.S100_CHECK_ALIGNER;
                    break;
                case EmEFEMAlignerSeqStep.S100_CHECK_ALIGNER:
                    if (Status.IsWaferExist == false)
                    {
                        _seqStepNum = EmEFEMAlignerSeqStep.S200_LD_WAFER_START;
                    }
                    else if (Status.IsWaferExist == true)
                    {
                        if (TransferDataMgr.GetWafer(this.LowerWaferKey).IsAlignComplete == false)
                            _seqStepNum = EmEFEMAlignerSeqStep.S240_ALIGN_START;
                        else
                            _seqStepNum = EmEFEMAlignerSeqStep.S470_UNLOAD_WAFER_START;
                    }
                    break;

                case EmEFEMAlignerSeqStep.S200_LD_WAFER_START:
                    PioRecv.YRecvAble = true;
                    _seqStepNum = EmEFEMAlignerSeqStep.S210_WAIT_ROBOT_SEND;
                    break;
                case EmEFEMAlignerSeqStep.S210_WAIT_ROBOT_SEND:
                    if (PioRecv.XSendAble == true)
                    {
                        PioRecv.YRecvAble = true;
                        PioRecv.YRecvStart = true;
                        _seqStepNum = EmEFEMAlignerSeqStep.S220_WAIT_ROBOT_SEND_COMPLETE;
                    }
                    break;
                case EmEFEMAlignerSeqStep.S220_WAIT_ROBOT_SEND_COMPLETE:
                    if (PioRecv.XSendAble == true && PioRecv.XSendStart == true && PioRecv.XSendComplete == true)
                    {
                        PioRecv.YRecvAble = true;
                        PioRecv.YRecvStart = true;
                        PioRecv.YRecvComplete = true;
                        if (GG.TestMode)
                            equip.AlignerWaferDetect.XB_OnOff.vBit = false;
                        _seqStepNum = EmEFEMAlignerSeqStep.S230_WAIT_ROBOT_SEND_SIGNAL_CLEAR;
                    }
                    break;
                case EmEFEMAlignerSeqStep.S230_WAIT_ROBOT_SEND_SIGNAL_CLEAR:
                    if (PioRecv.XSendAble == false && PioRecv.XSendStart == false && PioRecv.XSendComplete == false)
                    {
                        _checkForcedComeback = false;
                        PioRecv.Initailize();
                        _seqStepNum = EmEFEMAlignerSeqStep.S240_ALIGN_START;
                        //_seqStepNum = EmEFEMAlignerSeqStep.S500_UNLOAD_WAFER_START; //jys::test
                    }
                    break;
            }
        }
        private bool _readOcr = false;
        private bool _readBcr = false;
        private string _readVal1 = string.Empty;
        private string _readVal2 = string.Empty;
        private bool _ocrReadOk = false;
        private bool _bcrReadOk = false;
        private string _waferIDFromRead;
        private int _readOcrCount;
        private int _readBcrCount;
        private PlcTimerEx _readOcrTimeover = new PlcTimerEx("OCR Timeover", false);

        private int _alignRetryCount = 0;
        private bool _isFirst = true;
        private double _beforeAlignerX;
        private double _beforeAlignerY;
        private double _beforeAlignerT;

        private void AlignLogic(Equipment equip)
        {
            SeqLogging(equip);

            switch (_seqStepNum)
            {
                case EmEFEMAlignerSeqStep.S240_ALIGN_START:
                    if (Status.IsStatus == EmEfemAlignerStatus.ERROR)
                    {
                        if (_efem.Proxy.StartCommand(equip, EmEfemPort.ALIGNER, EmEfemCommand.INIT_) == false) return;
                    }
                    _seqStepNum = EmEFEMAlignerSeqStep.S250_INIT_WAIT_BEFORE_ALIGN;
                    break;
                case EmEFEMAlignerSeqStep.S250_INIT_WAIT_BEFORE_ALIGN:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ALIGNER, EmEfemCommand.INIT_) == true)
                    {
                        AlarmMgr.Instance.HappenAlarms[EM_AL_LST.AL_0901_PRE_ALIGNER_NO_DEFAULT_RECIPE].Happen = false;

                        if (GG.Equip.IsAutoOnlyAligner)
                        {
                            _seqStepNum = EmEFEMAlignerSeqStep.S270_GO_READY_POS;
                        }
                        else if (GG.EfemLongRun == false
                            && _checkForcedComeback)
                        {
                            //AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0921_ALIGNER_RETRY_INPUT_WAIT);
                            _frmRetryMsg.RequestPopup("PreAligner 재시도", "재시도 또는 홈 완료 후 첫 PRE ALIGN 진행입니다 다음 중 선택하세요");
                            _seqStepNum = EmEFEMAlignerSeqStep.S260_USER_INPUT_WAIT;
                        }
                        else
                        {
                            _seqStepNum = EmEFEMAlignerSeqStep.S290_ROTATION_COMMAND;
                        }
                    }
                    break;
                case EmEFEMAlignerSeqStep.S260_USER_INPUT_WAIT:
                    if (_frmRetryMsg.PopupFlow == EmPopupFlow.OK)
                    {
                        if (_frmRetryMsg.IsRetry)
                        {
                            _seqStepNum = EmEFEMAlignerSeqStep.S270_GO_READY_POS;
                        }
                        else
                        {
                            _isForcedComback = true;
                            _seqStepNum = EmEFEMAlignerSeqStep.S470_UNLOAD_WAFER_START;
                        }
                    }
                    break;
                case EmEFEMAlignerSeqStep.S270_GO_READY_POS:
                    if (GG.TestMode)
                    {
                        _seqStepNum = EmEFEMAlignerSeqStep.S290_ROTATION_COMMAND;
                        return;
                    }
                    if (_efem.Proxy.StartCommand(equip, EmEfemPort.ALIGNER, EmEfemCommand.PASRD) == false) return;
                    _seqStepNum = EmEFEMAlignerSeqStep.S280_WAIT_READY_POS;
                    break;
                case EmEFEMAlignerSeqStep.S280_WAIT_READY_POS:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ALIGNER, EmEfemCommand.PASRD) == true)
                    {
                        _seqStepNum = EmEFEMAlignerSeqStep.S290_ROTATION_COMMAND;
                    }
                    break;
                case EmEFEMAlignerSeqStep.S290_ROTATION_COMMAND:
                    _readOcr = GG.Equip.CtrlSetting.UseOCR;
                    _waferIDFromRead = string.Empty;
                    //if (GG.EfemLongRun) _readOcr = false;
                    _isForcedComback = false;
                    _checkForcedComeback = true;
                    int degree = 0;
                    try
                    {
                        _tempWaferInfo = TransferDataMgr.GetWafer(this.LowerWaferKey);
                        degree = (int)_tempWaferInfo.NotchToDegreeStdAVI();
                    }
                    catch (Exception ex)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0660_NO_WAFER_INFO);
                        equip.IsInterlock = true;
                    }
                    if (GG.TestMode == true && GG.IsDitPreAligner == false)
                    {
                        _seqStepNum = EmEFEMAlignerSeqStep.S300_WAIT_ROTATION_COMPLETE;
                    }
                    else
                    {
                        if (_efem.Proxy.StartAlignerRotation(equip, degree, _readOcr) == false) return;
                        _seqStepNum = EmEFEMAlignerSeqStep.S300_WAIT_ROTATION_COMPLETE;
                    }
                    break;
                case EmEFEMAlignerSeqStep.S300_WAIT_ROTATION_COMPLETE:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ALIGNER, EmEfemCommand.PATRR) == true || (GG.TestMode == true && GG.IsDitPreAligner == false))
                    {
                        _seqStepNum = EmEFEMAlignerSeqStep.S310_OCR_READ_START;
                    }
                    break;
                case EmEFEMAlignerSeqStep.S310_OCR_READ_START:
                    _readOcrCount = 0;
                    _ocrReadOk = false;
                    if (GG.TestMode == false && _readOcr)
                        _seqStepNum = EmEFEMAlignerSeqStep.S320_OCR_READ_COMMAND;
                    else
                    {
                        _readVal1 = "PASS";
                        _seqStepNum = EmEFEMAlignerSeqStep.S350_OCR_ID_CHECK;
                    }
                    break;
                #region OCR
                case EmEFEMAlignerSeqStep.S320_OCR_READ_COMMAND:
                    equip.OCR.Read();
                    _readOcrTimeover.Start(0, 1000);
                    _readOcrCount++;
                    _seqStepNum = EmEFEMAlignerSeqStep.S330_WAIT_READ_OCR;
                    break;
                case EmEFEMAlignerSeqStep.S330_WAIT_READ_OCR:
                    if (equip.OCR.IsReadComplete == true || _readOcrTimeover)
                    {
                        _readVal1 = equip.OCR.Readvalue;

                        bool ocrReadError = true;

                        if (_readOcr)
                        {
                            try
                            {
                                ocrReadError = equip.OCR.IsReadValueError;
                            }
                            catch (Exception ex)
                            {
                                ocrReadError = true;
                                Logger.Log.AppendLine(LogLevel.Error, "OCR VERIFY EXEPTION {0}", ex.Message);
                            }
                        }

                        if (_readOcrTimeover || ocrReadError == true)
                        {
                            _readOcrTimeover.Stop();
                            Logger.Log.AppendLine(LogLevel.Error, "OCR Read Error :{0} ({1}/{2})", equip.OCR.Readvalue, _readOcrCount, GG.Equip.CtrlSetting.Ctrl.OCRReadTryTimes);

                            if (_readOcrCount < GG.Equip.CtrlSetting.Ctrl.OCRReadTryTimes)
                            {
                                _seqStepNum = EmEFEMAlignerSeqStep.S320_OCR_READ_COMMAND;
                            }
                            else if (GG.Equip.CtrlSetting.UseBCR == false)
                            {
                                _frmOcrUserMsgBox.RequestPopup(_readVal1);
                                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0631_OCR_READ_ERROR);
                                _readVal1 = "OCR-READERROR";
                                _seqStepNum = EmEFEMAlignerSeqStep.S340_OCR_USER_CHECK;
                            }
                            else
                            {
                                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0631_OCR_READ_ERROR);
                                _readVal1 = "OCR-READERROR";
                                _seqStepNum = EmEFEMAlignerSeqStep.S350_OCR_ID_CHECK;
                            }
                        }
                        else
                        {
                            _ocrReadOk = true;
                            Logger.Log.AppendLine(LogLevel.NoLog, "WaferID, OCR:{0}", _readVal1);
                            _seqStepNum = EmEFEMAlignerSeqStep.S350_OCR_ID_CHECK;
                        }
                    }
                    break;
                case EmEFEMAlignerSeqStep.S340_OCR_USER_CHECK:
                    if (_frmOcrUserMsgBox.PopupFlow == EmPopupFlow.OK)
                    {
                        _readVal1 = _frmOcrUserMsgBox.ReturnValue;
                        _ocrReadOk = true;
                        Logger.Log.AppendLine(LogLevel.NoLog, "WaferID, UserInput OCR:{0}", _readVal1);
                        _seqStepNum = EmEFEMAlignerSeqStep.S350_OCR_ID_CHECK;
                    }
                    break;
                #endregion OCR
                case EmEFEMAlignerSeqStep.S350_OCR_ID_CHECK:
                    try
                    {
                        if (_ocrReadOk)
                            _waferIDFromRead = _readVal1;

                        _tempWaferInfo = TransferDataMgr.GetWafer(this.LowerWaferKey);
                        _tempWaferInfo.OCRID = _readVal1;
                        if (_ocrReadOk)
                            _tempWaferInfo.WaferID = _waferIDFromRead = _readVal1;
                        _tempWaferInfo.Update();
                    }
                    catch (Exception ex)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0660_NO_WAFER_INFO);
                    }
                    _seqStepNum = EmEFEMAlignerSeqStep.S360_START_READ_BCR;
                    break;
                case EmEFEMAlignerSeqStep.S360_START_READ_BCR:
                    if (GG.IsDitPreAligner)
                        GG.Equip.AlignerOcrCylinder.Up();
                    _readBcr = GG.Equip.CtrlSetting.UseBCR;
                    //if (GG.EfemLongRun) _readBcr = false;
                    _readBcrCount = 0;
                    _bcrReadOk = false;
                    _waferIDFromRead = string.Empty;

                    if (GG.Equip.CtrlSetting.UseOnlyReadOCR == true)
                        _ocrReadOk = false;

                    if (GG.TestMode == false && _readBcr && _ocrReadOk == false)
                        _seqStepNum = EmEFEMAlignerSeqStep.S370_BCR_READ_COMMAND;
                    else
                    {
                        _readVal1 = "PASS";
                        _readVal2 = "PASS";
                        _seqStepNum = EmEFEMAlignerSeqStep.S400_BCR_ID_CHECK;
                    }
                    break;
                #region BCR
                case EmEFEMAlignerSeqStep.S370_BCR_READ_COMMAND:
                    _readBcrCount++;
                    equip.BCR1.GetBarcode();
                    equip.BCR2.GetBarcode();
                    _bcrTimeover.Start(0, 2000);
                    _seqStepNum = EmEFEMAlignerSeqStep.S380_WAIT_READ_BCR;
                    break;
                case EmEFEMAlignerSeqStep.S380_WAIT_READ_BCR:
                    // bcr cam 1,2중 정상적으로 읽은 것으로 처리, 둘다 정상적으로 읽었으면 1번 기준
                    // 바코드 -> 파란필름에 있음.
                    _readVal1 = string.Empty;
                    _readVal2 = string.Empty;
                    if (_bcrTimeover
                        || (equip.BCR1.IsReadComplete == true || equip.BCR2.IsReadComplete == true))
                    {
                        _readVal2 = equip.BCR2.ReadValue;
                        _readVal1 = equip.BCR1.ReadValue;

                        if (_bcrTimeover
                            || (equip.BCR1.IsReadValueError && equip.BCR2.IsReadValueError)
                            )
                        {
                            _bcrTimeover.Stop();
                            if (_readBcrCount < GG.Equip.CtrlSetting.Ctrl.BCRReadTryTimes)
                            {
                                _seqStepNum = EmEFEMAlignerSeqStep.S370_BCR_READ_COMMAND;
                            }
                            else
                            {
                                if (GG.Equip.IsAutoOnlyAligner)
                                {
                                    _readVal1 = "READERROR" + DateTime.Now.ToString("ssfff");
                                    _waferIDFromRead = _readVal1;
                                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0630_BCR_READ_ERROR);
                                    Logger.Log.AppendLine(LogLevel.Error, "BCR Read Error (OnlyAlignerMode) 1:{0}, 2{1}", equip.BCR1.ReadValue, equip.BCR2.ReadValue);
                                    _bcrReadOk = true;
                                    _seqStepNum = EmEFEMAlignerSeqStep.S400_BCR_ID_CHECK;
                                }
                                else
                                {
                                    _frmBcrUserMsgBox.RequestPopup(equip.BCR1.ReadValue, equip.BCR1.ReadValue, true);
                                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0630_BCR_READ_ERROR);
                                    _readVal1 = "READERROR" + DateTime.Now.ToString("ssfff");
                                    _readVal2 = "READERROR" + DateTime.Now.ToString("ssfff");
                                    Logger.Log.AppendLine(LogLevel.Error, "BCR Read Error 1:{0}, 2:{1}", equip.BCR1.ReadValue, equip.BCR2.ReadValue);
                                    _seqStepNum = EmEFEMAlignerSeqStep.S390_BCR_USER_CHECK;
                                }
                            }
                        }
                        else
                        {
                            if (equip.BCR1.IsReadValueError == false)
                                _waferIDFromRead = _readVal1.Substring(0, 10); //"TC5XX-NNNN ", DSQ76H3-01, TAGHE27-11H1
                            else if (equip.BCR2.IsReadValueError == false)
                                _waferIDFromRead = _readVal2.Substring(0, 10);
                            _bcrReadOk = true;
                            Logger.Log.AppendLine(LogLevel.NoLog, "WaferID, BCR:{0}/{1}", _readVal1, _readVal2);
                            _seqStepNum = EmEFEMAlignerSeqStep.S400_BCR_ID_CHECK;
                        }
                    }
                    break;
                case EmEFEMAlignerSeqStep.S390_BCR_USER_CHECK:
                    if (_frmBcrUserMsgBox.PopupFlow == EmPopupFlow.OK)
                    {
                        _bcrReadOk = true;
                        _readVal1 = _frmBcrUserMsgBox.ReturnValue;
                        _readVal2 = _frmBcrUserMsgBox.ReturnValue;
                        _waferIDFromRead = _readVal1;
                        Logger.Log.AppendLine(LogLevel.NoLog, "WaferID, UserInput BCR:{0}", _waferIDFromRead);
                        _seqStepNum = EmEFEMAlignerSeqStep.S400_BCR_ID_CHECK;
                    }
                    break;
                #endregion BCR
                case EmEFEMAlignerSeqStep.S400_BCR_ID_CHECK:
                    try
                    {
                        _tempWaferInfo = TransferDataMgr.GetWafer(this.LowerWaferKey);
                        _tempWaferInfo.BCRID1 = _readVal1;
                        _tempWaferInfo.BCRID2 = _readVal2;
                        if (GG.TestMode)
                        {
                            _waferIDFromRead = _tempWaferInfo.WaferID = "WAFERID-13";
                            _bcrReadOk = true;
                        }
                        if (_bcrReadOk)
                        {
                            if (GG.TestMode == false)
                            {
                                try
                                {
                                    if (_waferIDFromRead.Length == 10)  //"TC5XXNN-NN(NN)"
                                    {
                                        _tempWaferInfo.WaferID = _waferIDFromRead;  //.Substring(0, 10);  위에서 정합성 확인함, 사용자가 10자리 이하의 스트리으로 직접입력시 바로 대입하도록 수정
                                        Logger.Log.AppendLine("체크섬 제외 WaferID : {0}", _tempWaferInfo.WaferID);
                                    }
                                    else if (_waferIDFromRead.Length > 10)
                                    {
                                        string originID = _waferIDFromRead;
                                        _waferIDFromRead = _waferIDFromRead.Substring(0, 10);
                                        _tempWaferInfo.WaferID = _waferIDFromRead;
                                        Logger.Log.AppendLine("BCR 수기입력 시 10자리 초과 입력으로 인하여 10자리 까지 자름. 기존 WaferID : {0} 새로 처리한 ID {1}", originID, _tempWaferInfo.WaferID);
                                    }
                                    else
                                    {
                                        string originID = _waferIDFromRead;
                                        _waferIDFromRead = _waferIDFromRead.Substring(0, 3) + DateTime.Now.ToString("HHmm-ss");
                                        _tempWaferInfo.WaferID = _waferIDFromRead;
                                        Logger.Log.AppendLine("BCR 수기입력 시 10자리 미만 입력으로 인하여 나머지 자릿수 자동으로 채움. 기존 WaferID : {0} 새로 처리한 ID {1}", originID, _tempWaferInfo.WaferID);
                                    }
                                }
                                catch (Exception)
                                {
                                    InterLockMgr.AddInterLock("BCR Read 또는 기입한 값 오류", "BCR : {0}", _tempWaferInfo.WaferID);
                                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0630_BCR_READ_ERROR);
                                }
                            }
                                

                            _tempCstInfo = TransferDataMgr.GetCst(this.LowerWaferKey);
                            Recipe recipe = RecipeDataMgr.GetRecipe(_tempCstInfo.LoadPortNo == 1 ? equip.LPM1Recipe : equip.LPM2Recipe);

                            if (recipe.Model == null || recipe.Model == string.Empty)
                            {
                                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0641_RECIPE_MODEL_DATA_IS_ABNORMAL);

                            }
                            else if (_waferIDFromRead.Substring(1, 2) != recipe.Model.Substring(1, 2))
                            {
                                //AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0640_WAFER_ID_IS_NOT_MATCH_WITH_RECIPE);
                                //InterLockMgr.AddInterLock("InterLock<Recipe Model>", "웨이퍼 모델과 레시피가 동일하지 않습니다 웨이퍼 복귀 후 레시피 재설정하여 진행해주세요");
                                //return;
                            }

                            if (equip.CimMode == EmCimMode.Remote)
                            {
                                _tempCstInfo = TransferDataMgr.GetCst(this.LowerWaferKey);
                                string[] waferList = _tempCstInfo.HWaferIDList.Split(',');
                                Logger.Log.AppendLine("CIM 통해서 DB에 저장된 WaferList : {0}", _tempCstInfo.HWaferIDList);

                                bool result = false;
                                foreach (var waferID in waferList)
                                {
                                    //if(_tempWaferInfo.WaferID == waferID)
                                    //{
                                    //    result = true;
                                    //    break;
                                    //}
                                    if (_tempWaferInfo.WaferID == waferID)
                                    {
                                        result = true;
                                        break;
                                    }
                                }
                                if (result == false)
                                {
                                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0559_BEFORE_WAFER_LOAD_ID_HOST_DATA_NOT_MATCH);
                                }
                            }
                        }
                        _tempWaferInfo.Update();

                        if (_readOcr && _readBcr && GG.Equip.CtrlSetting.UseOnlyReadOCR)
                            Logger.OCRBCRLog.AppendLine(LogLevel.NoLog, "OCR:{0}\tBCR1:{1}\tBCR2:{2}\tMatch:{3}", _tempWaferInfo.OCRID, _readVal1, _readVal2, _tempWaferInfo.OCRID == _waferIDFromRead ? "OK" : "NO");


                        _seqStepNum = EmEFEMAlignerSeqStep.S410_WAFER_LOAD_REPORT;
                    }
                    catch (Exception ex)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0947_EXCEPTION_OCCURE);
                        Logger.ExceptionLog.AppendLine("S400_BCR_ID_CHECK_ERROR");
                        Logger.ExceptionLog.AppendLine(ex.Message + "\n{0}", EquipStatusDump.CallStackLog());
                    }
                    break;
                #region Cim Event
                case EmEFEMAlignerSeqStep.S410_WAFER_LOAD_REPORT:
                    //10월 23일 김기태 추가(웨이퍼 로드 시 해당 변수 false로 초기화 시켜주지만 로직이 한 쓰레드에서 처리하다보니 false로 처리되기전에 다음 스텝이 진행되면서 꼬임)
                    equip.HsmsPc.IsWaferStartIFComplete = false;
                    equip.HsmsPc.IsWaferStartConfirmOK = false;
                    equip.HsmsPc.IsWaferMapRequestIFComplete = false;

                    if (equip.HsmsPc.StartCommand(equip, HSMS.EmHsmsPcCommand.WAFER_LOAD, this.LowerWaferKey) == false) return;
                    _WaferLoadCinfirmDelay.Start(equip.CtrlSetting.Hsms.WaferLoadConfirmWait, 0);
                    _seqStepNum = EmEFEMAlignerSeqStep.S420_WAFER_LOAD_CONFIRM_WAIT;
                    break;
                case EmEFEMAlignerSeqStep.S420_WAFER_LOAD_CONFIRM_WAIT:
                    if (equip.HsmsPc.IsEventComplete(HSMS.EmHsmsPcEvent.WAFER_START) && (equip.HsmsPc.IsWaferStartIFComplete == true || GG.CimTestMode == true))
                    {
                        _WaferLoadCinfirmDelay.Stop();
                        if (equip.HsmsPc.IsWaferStartConfirmOK)
                        {
                            _seqStepNum = EmEFEMAlignerSeqStep.S430_WAFER_MAP_REQUEST_REPORT;
                        }
                        else
                        {
                            Logger.CIMLog.AppendLine(LogLevel.Info, "Wafer Start Confirm NG");
                            AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0551_WAFER_START_CONFIRM_NG);
                        }
                    }
                    else if(_WaferLoadCinfirmDelay)
                    {
                        _WaferLoadCinfirmDelay.Stop();
                        Logger.CIMLog.AppendLine(LogLevel.Info, "Wafer Start Confirm Timeover");
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0574_WAFER_LOAD_CONFIRM_TIMEOVER);
                    }
                    break;
                case EmEFEMAlignerSeqStep.S430_WAFER_MAP_REQUEST_REPORT:
                    if (equip.CimMode == EmCimMode.Remote)
                    {
                        if (equip.HsmsPc.StartCommand(equip, HSMS.EmHsmsPcCommand.WAFER_MAP_REQUEST, this.LowerWaferKey) == false) return;
                        Logger.CIMLog.AppendLine(LogLevel.Info, "Wafer Start Confirm OK");
                        MapDownloadWaitTimer.Start(equip.CtrlSetting.Hsms.WaferMapRequestWait, 0);
                        _seqStepNum = EmEFEMAlignerSeqStep.S440_WAFER_MAP_DOWNLOAD_WAIT;
                    }
                    else
                    {
                        Logger.CIMLog.AppendLine(LogLevel.Info, "Wafer Map Request Skip");
                        _seqStepNum = EmEFEMAlignerSeqStep.S450_START_ALIGN;
                    }
                    break;
                case EmEFEMAlignerSeqStep.S440_WAFER_MAP_DOWNLOAD_WAIT:
                    if (equip.HsmsPc.IsEventComplete(HSMS.EmHsmsPcEvent.MAP_FILE_CREATE) && equip.HsmsPc.IsWaferMapRequestIFComplete)
                    {
                        MapDownloadWaitTimer.Stop();

                        Logger.CIMLog.AppendLine(LogLevel.Info, "Wafer Map Request OK");

                        _seqStepNum = EmEFEMAlignerSeqStep.S450_START_ALIGN;
                    }
                    //Timer Over
                    else if (MapDownloadWaitTimer)
                    {
                        MapDownloadWaitTimer.Stop();

                        //1. Countinue, Retry, Auto Stop 중 하나 진행
                        if (WaferMapNgWay == EmHsmsWaferMapNG.AutoStop)
                        {
                            AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0552_WAFER_MAP_DOWNLOAD_FAIL);
                            Logger.CIMLog.AppendLine(LogLevel.Info, "Wafer Map Request NG : Auto Stop");
                            _seqStepNum = EmEFEMAlignerSeqStep.S450_START_ALIGN;
                        }
                        else if (WaferMapNgWay == EmHsmsWaferMapNG.Continue)
                        {
                            AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0552_WAFER_MAP_DOWNLOAD_FAIL);
                            Logger.CIMLog.AppendLine(LogLevel.Info, "Wafer Map Request NG : Continue");
                            _seqStepNum = EmEFEMAlignerSeqStep.S450_START_ALIGN;
                        }
                        else if (WaferMapNgWay == EmHsmsWaferMapNG.Retry)
                        {
                            AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0557_WAFER_MAP_DOWNLOAD_RETRY);
                            Logger.CIMLog.AppendLine(LogLevel.Info, "Wafer Map Request NG : Retry");
                            _seqStepNum = EmEFEMAlignerSeqStep.S430_WAFER_MAP_REQUEST_REPORT;
                        }
                    }
                    break;
                #endregion 
                case EmEFEMAlignerSeqStep.S450_START_ALIGN:
                    if (GG.TestMode == true && GG.IsDitPreAligner == false)
                    {
                        _seqStepNum = EmEFEMAlignerSeqStep.S460_ALIGN_WAIT;
                    }
                    else
                    {
                        //if (_efem.Proxy.StartAlign(equip, 0, false, _waferIDFromRead) == false) return;                        
                        CassetteInfo cst = TransferDataMgr.GetCst(this.LowerWaferKey.CstID);
                        if(equip.IsAutoOnlyAligner)
                        {
                            cst = new CassetteInfo();
                            cst.RecipeName = "TC5";
                        }
                        if (_efem.Proxy.StartAlign(equip, 0, false, cst == null ? "AGN_RCP_NULL" : cst.RecipeName) == false) return;
                        _readOcrTimeover.Start(0, 5000);
                        _seqStepNum = EmEFEMAlignerSeqStep.S460_ALIGN_WAIT;
                    }
                    break;
                case EmEFEMAlignerSeqStep.S460_ALIGN_WAIT:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ALIGNER, EmEfemCommand.ALIGN) == true || (GG.TestMode == true && GG.IsDitPreAligner ==false))
                    {
                        if (AlarmMgr.Instance.IsHappened(GG.Equip, EM_AL_LST.AL_0901_PRE_ALIGNER_NO_DEFAULT_RECIPE))
                        {
                            this.SetRunMode(EmEfemRunMode.Pause);
                            equip.IsPause = true;
                            CheckMgr.AddCheckMsg(true, "PreAligner에 레시피가 없습니다. 생성 후 Pause해제하세요");
                            _seqStepNum = EmEFEMAlignerSeqStep.S240_ALIGN_START;
                        }
                        else if ((_efem.Proxy.IsDataProcessDone(equip, EmEfemPort.ALIGNER, EmEfemCommand.ALIGN) == true
                            && _efem.Proxy.IsSuccess(equip, EmEfemPort.ALIGNER, EmEfemCommand.ALIGN))
                            || GG.TestMode == true)
                        {
                            try
                            {
                                _tempWaferInfo = TransferDataMgr.GetWafer(this.LowerWaferKey);
                                _tempWaferInfo.IsAlignComplete = true;
                                Status.AlignDataCopyTo(ref _tempWaferInfo);
                                _tempWaferInfo.Update();
                                double alignerX = Status.OffsetX;
                                double alignerY = Status.OffsetY;
                                double alignerT = Status.OffsetT;
                                if (Math.Abs(alignerX) > GG.Equip.CtrlSetting.Ctrl.PreAlignerXLimit) { AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0673_PREALIGN_X_LIMIT_OVER); equip.IsInterlock = true; }
                                if (Math.Abs(alignerY) > GG.Equip.CtrlSetting.Ctrl.PreAlignerYLimit) { AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0674_PREALIGN_Y_LIMIT_OVER); equip.IsInterlock = true; }
                                if (Math.Abs(alignerT) > GG.Equip.CtrlSetting.Ctrl.PreAlignerTLimit) { AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0675_PREALIGN_T_LIMIT_OVER); equip.IsInterlock = true; }
                                Logger.Log.AppendLine(LogLevel.Info, string.Format("Pre Aligner Offset 측정값  [X : {0} Y : {1} T : {2}]", alignerX.ToString(), alignerY.ToString(), alignerT.ToString()));


                                if(IsOverLapOffsetValue(alignerX, alignerY, alignerT) && GG.TestMode == false)
                                {
                                    if(_alignRetryCount < GG.Equip.CtrlSetting.Ctrl.PreAlignTryTimes)
                                    {
                                        _alignRetryCount++;
                                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0921_ALIGN_RETRY); //경알람
                                        Logger.Log.AppendLine(LogLevel.Info, "이전 데이터와 보정값 동일하여 얼라인 재시도 [재시도 횟수 : {0}", _alignRetryCount.ToString());

                                        _checkForcedComeback = false;

                                        _seqStepNum = EmEFEMAlignerSeqStep.S240_ALIGN_START;
                                    }
                                    else
                                    {
                                        //얼라인 보정값 똑같고 리트라이 횟수 초과 되면 여기로
                                        //알람 발생시키고 "웨이퍼리턴 하고 재시도" 하라고 안내 팝업창 띄우기
                                        _alignRetryCount = 0;
                                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0922_ALIGN_RETRY_TIMES_OVER);
                                        InterLockMgr.AddInterLock("프리 얼라인 실패\nALIGNER에 위치한 웨이퍼를 카세트로 복귀(리커버리) 후 재시작 해주세요");
                                        return;
                                    }
                                }
                                else
                                {
                                    _alignRetryCount = 0;
                                    _seqStepNum = EmEFEMAlignerSeqStep.S470_UNLOAD_WAFER_START;
                                }
                            }
                            catch (Exception ex)
                            {
                                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0660_NO_WAFER_INFO);
                                _checkForcedComeback = true;
                                _seqStepNum = EmEFEMAlignerSeqStep.S240_ALIGN_START;
                            }
                        }
                        else if (_readOcrTimeover)
                        {
                            _readOcrTimeover.Stop();
                            _checkForcedComeback = true;
                            _seqStepNum = EmEFEMAlignerSeqStep.S240_ALIGN_START;
                        }

                    }
                    break;
            }
        }

        private bool IsOverLapOffsetValue(double alignerX, double alignerY, double alignerT)
        {
            if (_isFirst)
            {
                _isFirst = false;

                _beforeAlignerX = alignerX;
                _beforeAlignerY = alignerY;
                _beforeAlignerT = alignerT;

                return false;
            }
            else
            {
                //직전에 측정한 얼라인 Offset 값이 x, y, theta 모두 동일하면
                if (_beforeAlignerX == alignerX && _beforeAlignerY == alignerY && _beforeAlignerT == alignerT)
                {
                    return true;
                }
                else
                {
                    _beforeAlignerX = alignerX;
                    _beforeAlignerY = alignerY;
                    _beforeAlignerT = alignerT;

                    return false;
                }
            }
        }

        private void WaferUnldLogic(Equipment equip)
        {
            SeqLogging(equip);

            switch (_seqStepNum)
            {
                case EmEFEMAlignerSeqStep.S470_UNLOAD_WAFER_START:
                    if (GG.TestMode == false
                        && GG.EfemLongRun == false
                        && _isForcedComback)
                    {
                        if (_efem.Proxy.StartCommand(equip, EmEfemPort.ALIGNER, EmEfemCommand.PASRD) == false) return;
                        _seqStepNum = EmEFEMAlignerSeqStep.S480_SEND_READY_WAIT;
                    }
                    else
                    {
                        _seqStepNum = EmEFEMAlignerSeqStep.S490_SEND_ABLE_ON;
                    }
                    break;
                case EmEFEMAlignerSeqStep.S480_SEND_READY_WAIT:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ALIGNER, EmEfemCommand.PASRD) == true)
                    {
                        _seqStepNum = EmEFEMAlignerSeqStep.S490_SEND_ABLE_ON;
                    }
                    break;
                case EmEFEMAlignerSeqStep.S490_SEND_ABLE_ON:
                    PioSend.YSendAble = true;
                    _seqStepNum = EmEFEMAlignerSeqStep.S500_ROBOT_RECV_ABLE_WAIT;
                    break;
                case EmEFEMAlignerSeqStep.S500_ROBOT_RECV_ABLE_WAIT:
                    if (PioSend.XRecvAble == true)
                    {
                        PioSend.YSendAble = true;
                        PioSend.YSendStart = true;
                        _seqStepNum = EmEFEMAlignerSeqStep.S510_WAIT_ROBOT_RECV_COMPLETE;
                    }
                    break;
                case EmEFEMAlignerSeqStep.S510_WAIT_ROBOT_RECV_COMPLETE:
                    if (PioSend.XRecvAble == true && PioSend.XRecvStart == true && PioSend.XRecvComplete == true)
                    {
                        PioSend.YSendAble = true;
                        PioSend.YSendStart = true;
                        PioSend.YSendComplete = true;
                        if (GG.TestMode)
                            equip.AlignerWaferDetect.XB_OnOff.vBit = true;
                        _seqStepNum = EmEFEMAlignerSeqStep.S520_WAIT_ROBOT_RECV_SIGNAL_CLEAR;
                    }
                    break;
                case EmEFEMAlignerSeqStep.S520_WAIT_ROBOT_RECV_SIGNAL_CLEAR:
                    if (PioSend.XRecvAble == false && PioSend.XRecvStart == false && PioSend.XRecvComplete == false)
                    {
                        _checkForcedComeback = false;
                        _isForcedComback = false;
                        PioSend.Initailize();
                        _seqStepNum = EmEFEMAlignerSeqStep.S100_CHECK_ALIGNER;
                    }
                    break;
            }
        }

        public override void HomeLogicWorking(Equipment equip)
        {
            SeqLogging(equip);

            switch (_homeStepNum)
            {
                case EmEFEMAlignerHomeStep.H000_END:
                    _homeStepNum = EmEFEMAlignerHomeStep.H010_ALIGNER_RESET;
                    break;
                case EmEFEMAlignerHomeStep.H010_ALIGNER_RESET:
                    if (_efem.Proxy.StartCommand(equip, EmEfemPort.ALIGNER, EmEfemCommand.RESET) == false) return;
                    _homeStepNum = EmEFEMAlignerHomeStep.H020_WAIT_ALIGNER_RESET;
                    break;
                case EmEFEMAlignerHomeStep.H020_WAIT_ALIGNER_RESET:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ALIGNER, EmEfemCommand.RESET) == true)
                    {
                        if (_efem.Proxy.StartCommand(equip, EmEfemPort.ALIGNER, EmEfemCommand.INIT_) == false) return;
                        _homeStepNum = EmEFEMAlignerHomeStep.H030_WAIT_ALIGNER_INIT;
                    }
                    break;
                case EmEFEMAlignerHomeStep.H030_WAIT_ALIGNER_INIT:
                    if (_efem.Proxy.IsComplete(equip, EmEfemPort.ALIGNER, EmEfemCommand.INIT_) == true)
                    {
                        _homeStepNum = EmEFEMAlignerHomeStep.H040_CHECK_ALIGNER_STATUS;
                    }
                    break;
                case EmEFEMAlignerHomeStep.H040_CHECK_ALIGNER_STATUS:
                    if (IsInitDone == true)
                    {
                        if (Status.IsWaferExist)
                            _checkForcedComeback = true;
                        else
                            _checkForcedComeback = false;

                        _homeStepNum = EmEFEMAlignerHomeStep.H100_HOME_COMPLETE;
                    }
                    break;
                case EmEFEMAlignerHomeStep.H100_HOME_COMPLETE:
                    IsHomeComplete = true;
                    break;
            }
        }
        protected override bool IsSafeToHome()
        {
            if (GG.Equip.IsAutoOnlyAligner)
                return true;
            else
            {
                return _efem.Robot.IsHomeComplete == true
                && _efem.Robot.Status.IsArmUnFold == false
                ;
            }
        }
    }
}
