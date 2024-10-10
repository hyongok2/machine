using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Monitor;
using EquipMainUi.Struct.Detail;
using EquipMainUi.Struct.TransferData;
using EquipMainUi.UserMessageBoxes;
using System.Windows.Forms;

namespace EquipMainUi.Struct.Step
{
    public enum EmMN_NO
    {
        END,
        WAIT,
        USER_CHECK_BEFORE_LOADING,
        USER_INPUT_WAIT,
        LOADING_WAIT,
        LOADING_START,
        LOADING_COMPLETE,

        REINSPECT_START_WAIT,

        TTTM_START,
        TTTM_WAIT,
        TTTM_COMPLETE,

        SCAN_START,
        SCAN_WAIT,
        SCAN_COMPLETE,

        REVIEW_START_DELAY,

        REVIEW_START,
        REVIEW_WAIT,
        REVIEW_COMPLETE,

        UNLOADING_START,
        UNLOADING_WAIT,
        UNLOADING_COMPLETE,

        HOME_START,
        HOME_WAIT,
        HOME_END,
        ROBOT_ARM_CHECK
    }

    public class MainStep : StepBase
    {
        private EmMN_NO _stepNum = EmMN_NO.WAIT;
        private EmMN_NO _stepNumOld = 0;
        public EmMN_NO StepNum { get { return _stepNum; } }
        private bool _isComplete = false;
        private PlcTimerEx _loadingDelay = new PlcTimerEx("검사,리뷰 Loading Signal Delay");
        private PlcTimerEx _reviewStartDelay = new PlcTimerEx("리뷰 Start Delay");
        private Timer _popupTimer = new Timer();
        private FrmRetryUserSelect _frmRetryMsg;

        public MainStep()
        {            
            PioRecv = new PioSignalRecv() { Name = "AVI-Robot-Recv" };
            PioSend = new PioSignalSend() { Name = "AVI-Robot-Send" };
        }
        public void InitUserInferface()
        {
            _frmRetryMsg = new FrmRetryUserSelect();

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
                    _frmRetryMsg.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
            if (equip.IsImmediatStop)
            {
                if (equip.IsNeedRestart == false && equip.IsHeavyAlarm == true && equip.EquipRunMode == EmEquipRunMode.Auto)
                {
                    equip.IsNeedRestart = true;
                }
                return;
            }

            if (_stepNum != _stepNumOld)
            {
                Logger.Log.AppendLine(LogLevel.AllLog, "●[{0,-20}] = {1}", "MAIN_STEP", _stepNum.ToString());
                _stepNumOld = _stepNum;
            }

            if (equip.EquipRunMode == EmEquipRunMode.Manual)
                _stepNum = EmMN_NO.WAIT;
            else
            {                
            }

            if (_stepNum == EmMN_NO.END)
            {
            }
            else if (_stepNum == EmMN_NO.WAIT)
            {
                if (equip.EquipRunMode == EmEquipRunMode.Auto)
                {
                    _stepNum = EmMN_NO.ROBOT_ARM_CHECK;
                }
            }
            else if (_stepNum == EmMN_NO.ROBOT_ARM_CHECK)
            {
                if (equip.RobotArmDefect.IsOn == false)
                {
                    _stepNum = EmMN_NO.HOME_START;
                }
            }
            else if (_stepNum == EmMN_NO.HOME_START)
            {
                if (equip.IsWaferDetect != EmGlassDetect.NOT && equip.IsGlassUnloading == true) // Home 완료 상태에서만 GlassUnloading 옵션을 켤수있음.
                    _stepNum = EmMN_NO.UNLOADING_START;
                else if (equip.IsReadyToLoading == true)
                    _stepNum = EmMN_NO.USER_CHECK_BEFORE_LOADING;
                else
                {
                    if (equip.StHoming.StepStart(equip) == false) return;
                    _stepNum = EmMN_NO.HOME_WAIT;
                }
            }
            else if (_stepNum == EmMN_NO.HOME_WAIT)
            {
                if (equip.StHoming.IsStepComplete())
                {
                    _stepNum = EmMN_NO.HOME_END;
                }
            }
            else if (_stepNum == EmMN_NO.HOME_END)
            {
                _stepNum = EmMN_NO.USER_CHECK_BEFORE_LOADING;
            }

            //LOADING 
            else if (_stepNum == EmMN_NO.USER_CHECK_BEFORE_LOADING)
            {
                if (equip.IsLongTest == false && GG.EfemLongRun == false
                    && equip.IsWaferDetect != EmGlassDetect.NOT && equip.IsForcedComeback == true)
                {
                    //10월 21일 기술5팀 최영훈C 요청으로 삭제
                    //AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0540_AVI_RETRY_INPUT_WAIT);
                    _frmRetryMsg.RequestPopup(GG.boChinaLanguage ? "AVI 检查 重试" : "AVI 검사 재시도", GG.boChinaLanguage ? "重试或原点完毕后，第一次进行检查\n从以下中进行选择" : "재시도 또는 홈 완료 후 첫 검사 진행입니다\n다음 중 선택하세요");
                    _stepNum = EmMN_NO.USER_INPUT_WAIT;                    
                }
                else
                    _stepNum = EmMN_NO.LOADING_START;
            }
            else if (_stepNum == EmMN_NO.USER_INPUT_WAIT)
            {
                if (_frmRetryMsg.PopupFlow == EmPopupFlow.OK)
                {
                    if (_frmRetryMsg.IsRetry)
                    {
                        Logger.Log.AppendLine("검사기 재시도/배출 중 재시도 선택");
                        _stepNum = EmMN_NO.LOADING_START;
                    }
                    else
                    {
                        Logger.Log.AppendLine("검사기 재시도/배출 중 배출 선택");

                        
                        Logger.Log.AppendLine("해당 웨이퍼 검사 진행하지 않고 배출하여 내부 플래그 업데이트");
                        
                        equip.IsSkipWaferLPM = equip.TransferUnit.LowerWaferKey.CstID == equip.Efem.LoadPort1.CstKey.ID ? 1 : 
                                                equip.TransferUnit.LowerWaferKey.CstID == equip.Efem.LoadPort2.CstKey.ID ? 2 : -1;
                        equip.IsSkipWaferSlotNo = equip.TransferUnit.LowerWaferKey.SlotNo;
                        equip.IsGlassUnloading = true;
                        _stepNum = EmMN_NO.UNLOADING_START;
                    }
                }
            }
            else if (_stepNum == EmMN_NO.LOADING_START)
            {
                TactTimeMgr.Instance.AddToHist();
                equip.IsForcedComeback = true;
                if (equip.StLoading.StepStart(equip) == false) return;
                _stepNum = EmMN_NO.LOADING_WAIT;
            }
            else if (_stepNum == EmMN_NO.LOADING_WAIT)
            {
                if (equip.StLoading.IsStepComplete())
                {
                    if (GG.EfemNoWafer)
                        _stepNum = EmMN_NO.UNLOADING_START;
                    else
                        _stepNum = EmMN_NO.LOADING_COMPLETE;
                }
            }
            else if (_stepNum == EmMN_NO.LOADING_COMPLETE)
            {
                if (equip.IsTTTMMode && (equip.processTTTMCount + 1 >= equip.CtrlSetting.Insp.TTTMMeasureCycle))
                {
                    _stepNum = EmMN_NO.TTTM_START;
                }
                else
                {
                    _stepNum = EmMN_NO.SCAN_START;
                }
            }

            //TTTM
            else if (_stepNum == EmMN_NO.TTTM_START)
            {
                if (equip.StTTTM.StepStart(equip) == false) return;
                _stepNum = EmMN_NO.TTTM_WAIT;
            }
            else if (_stepNum == EmMN_NO.TTTM_WAIT)
            {
                if (equip.StTTTM.IsStepComplete())
                {
                    _stepNum = EmMN_NO.TTTM_COMPLETE;
                }
            }
            else if (_stepNum == EmMN_NO.TTTM_COMPLETE)
            {
                _stepNum = EmMN_NO.SCAN_START;
            }

            //REINSPECT
            else if (_stepNum == EmMN_NO.REINSPECT_START_WAIT)
            {
                if (equip.IsReviewManual == EmReviewManual.Request)
                {
                    equip.IsReviewManual = EmReviewManual.ManualOn;
                }
                if (equip.IsReserveReInsp == EmReserveReInsp.START)
                {
                    equip.IsReviewManual = EmReviewManual.None;
                    if (equip.StLoading.ReloadingReinspection(equip) == false)
                        return;
                    _stepNum = EmMN_NO.LOADING_WAIT;
                }
                else if (equip.IsReserveReInsp == EmReserveReInsp.DISABLE)
                {
                    _stepNum = EmMN_NO.UNLOADING_START;
                }

            }
            //SCAN
            else if (_stepNum == EmMN_NO.SCAN_START)
            {
                if (equip.StScanning.StepStart(equip) == false) return;
                _stepNum = EmMN_NO.SCAN_WAIT;
            }
            else if (_stepNum == EmMN_NO.SCAN_WAIT)
            {
                if (equip.StScanning.IsStepComplete())
                {
                    try
                    {
                        _tempWaferInfo = TransferDataMgr.GetWafer(equip.TransferUnit.LowerWaferKey);
                        _tempWaferInfo.IsInspComplete = true;
                        _tempWaferInfo.Update();
                    }
                    catch (Exception ex)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0660_NO_WAFER_INFO);
                    }
                    if (equip.IsReviewSkip == EmReviewSkip.Request)
                        equip.IsReviewSkip = EmReviewSkip.SkipOn;
                    _stepNum = EmMN_NO.SCAN_COMPLETE;
                }
            }
            else if (_stepNum == EmMN_NO.SCAN_COMPLETE)
            {
                if (equip.IsReviewSkip == EmReviewSkip.SkipOn && equip.IsReviewManual != EmReviewManual.Request)
                    _stepNum = EmMN_NO.REVIEW_COMPLETE;
                else
                    _stepNum = EmMN_NO.REVIEW_START_DELAY;
            }

            // REVIEW START
            else if (_stepNum == EmMN_NO.REVIEW_START_DELAY)
            {
                TactTimeMgr.Instance.Set(EM_TT_LST.T080_REVIEW_WAIT_POS_START);
                _reviewStartDelay.Start(0, equip.CtrlSetting.Insp.ReviewStartDelay);
                _stepNum = EmMN_NO.REVIEW_START;
            }
            else if (_stepNum == EmMN_NO.REVIEW_START)
            {
                if (_reviewStartDelay)
                {
                    _reviewStartDelay.Stop();
                    if (equip.StReviewing.StepStart(equip) == false) return;
                    _stepNum = EmMN_NO.REVIEW_WAIT;
                }
            }
            else if (_stepNum == EmMN_NO.REVIEW_WAIT)
            {
                if (equip.StReviewing.IsStepComplete())
                {
                    try
                    {
                        _tempWaferInfo = TransferDataMgr.GetWafer(equip.TransferUnit.LowerWaferKey);
                        _tempWaferInfo.IsReviewComplete = true;
                        _tempWaferInfo.Update();
                    }
                    catch (Exception ex)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0660_NO_WAFER_INFO);
                    }

                    _stepNum = EmMN_NO.REVIEW_COMPLETE;
                }
            }
            else if (_stepNum == EmMN_NO.REVIEW_COMPLETE)
            {
                if (equip.IsReserveReInsp == EmReserveReInsp.RESERVE)
                {
                    equip.IsReserveReInsp = EmReserveReInsp.READY;
                    _stepNum = EmMN_NO.REINSPECT_START_WAIT;
                }
                else
                {
                    _stepNum = EmMN_NO.UNLOADING_START;
                }
            }

            //UNLOADING
            else if (_stepNum == EmMN_NO.UNLOADING_START)
            {
                if (equip.IsReserveReInsp == EmReserveReInsp.RESERVE)
                {
                    equip.IsReserveReInsp = EmReserveReInsp.READY;
                    _stepNum = EmMN_NO.REINSPECT_START_WAIT;
                }
                if (equip.StUnloading.StepStart(equip) == false) return;
                _stepNum = EmMN_NO.UNLOADING_WAIT;
            }
            else if (_stepNum == EmMN_NO.UNLOADING_WAIT)
            {
                if (equip.StUnloading.IsStepComplete())
                {
                    equip.IsForcedComeback = false;
                    _stepNum = EmMN_NO.UNLOADING_COMPLETE;
                }
            }
            else if (_stepNum == EmMN_NO.UNLOADING_COMPLETE)
            {
                if (equip.IsGlassUnloading == true)
                    equip.IsGlassUnloading = false;

                if (equip.IsCycleStop == EmCycleStop.Request)
                {
                    _stepNum = EmMN_NO.HOME_WAIT;
                }
                else if (equip.IsLongTest == true)
                {
                    _stepNum = EmMN_NO.WAIT;
                }
                else
                {
                    _stepNum = EmMN_NO.WAIT;
                }
                equip.LongRunCount++;
                equip.UsingTimeSetting.AccLongRunCount++;
                equip.UsingTimeSetting.Save();
                Logger.Log.AppendLine(LogLevel.NoLog, "Long Run 횟수 {0} 회, 누적 Long Run 횟수 {1} 회",
                    equip.LongRunCount, equip.UsingTimeSetting.AccLongRunCount);
                Logger.Log.AppendLine(LogLevel.NoLog, "런 정보 : GlassID {0}.{3}, PC응답무시 {1}, NoGlass {2}",
                    equip.TransferUnit.LowerWaferKey.CstID, GG.InspTestMode, equip.IsNoGlassMode,
                    equip.TransferUnit.LowerWaferKey.SlotNo);
            }
            else if (_stepNum == EmMN_NO.HOME_WAIT)
            {
                if (equip.IsCycleStop == EmCycleStop.Request)
                {
                    equip.IsCycleStop = EmCycleStop.Complete;

                    equip.IsPause = true;
                    _stepNum = EmMN_NO.WAIT;
                }
            }

        }

        public void StepStart()
        {
            _isComplete = false;
            _stepNum = EmMN_NO.USER_CHECK_BEFORE_LOADING;
        }

        public bool StepStop(Equipment equip)
        {
            _isComplete = false;
            _stepNum = EmMN_NO.WAIT;

            return true;
        }
        public bool IsStepComplete()
        {
            return _isComplete && _stepNum == 0;
        }
    }
}
