using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Struct.Detail;
using Dit.Framework.PLC;
using EquipMainUi.Monitor;
using EquipMainUi.Struct.Detail.PC;
using EquipMainUi.Setting;

namespace EquipMainUi.Struct.Step
{
    public enum EmRV_NO
    {
        S000_REVIEW_WAIT,

        S010_REVIEW_START,
        S020_REVIEW_READY_POSI_WAIT,
        S025_REVIEW_READY,
        S030_REVIEW_START_ACK_WAIT,
        S040_REVIEW_COMPLETE_WAIT,

        S041_REVIEW_TIME_OVER_START,
        S042_REVIEW_TIME_OVER_WAIT,

        S050_PMAC_REVIEW_STOP_WAIT,
        S060_CHECK_MANUAL_REVIEW,
        S070_REVIEW_COMPLETE,
        S055_MOTOR_STOP_DELAY_WAIT,
    }

    public class ReviewStep : StepBase
    {
        //Loading Step 로직 처리. 
        private bool _isComplete = false;
        private EmRV_NO _stepNum = 0;
        private EmRV_NO _stepNumOld = 0;
        public EmRV_NO StepNum { get { return _stepNum; } }
        private PlcTimerEx _tmrReviTimeOver = new PlcTimerEx("REVIEW TIME CHECKER");
        public long ReviewPassTime { get { return _tmrReviTimeOver.PassTime; } }

        public bool IsReviewingStep { get { return _stepNum == EmRV_NO.S040_REVIEW_COMPLETE_WAIT || _stepNum == EmRV_NO.S060_CHECK_MANUAL_REVIEW; } }

        private PlcTimerEx ReviStartDelay = new PlcTimerEx("REVIEW START DELAY");        
        private PlcTimerEx TmrJudgeCompleteTimeOver = new PlcTimerEx("Judge Complete Time Over");
        private PlcTimerEx _tmrMotorStopWait = new PlcTimerEx("Motor Stop Wait");

        private float _xTargetPos;
        private float _yTargetPos;

        public ReviewStep()
        {
        }

        public override void LogicWorking(Equipment equip)
        {
            if (equip.IsPause)
            {
                StepStartTime = DateTime.Now;
            }
            if (equip.IsImmediatStop) return;

            if (_stepNum != _stepNumOld)
            {
                Logger.Log.AppendLine(LogLevel.AllLog, "★[REVIEW STEP] = {0} Step 소요시간 {1}s",
                    _stepNum.ToString(), (DateTime.Now - StepStartTime).TotalSeconds);

                StepStartTime = DateTime.Now;
                _stepNumOld = _stepNum;
            }
            else if (GG.TestMode == false)
            {
                if (_stepNum > 0 &&
                    (DateTime.Now - StepStartTime).TotalMilliseconds > equip.CtrlSetting.Ctrl.AutoStepTimeout)
                {
                    string err = string.Format("[REVIEW STEP] = {0} 중 AutoStep Timeover 발생", _stepNum.ToString());
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0289_AUTO_STEP_OVERTIME);
                    InterLockMgr.AddInterLock("AutoStep Timeover\n" + err);
                    Logger.Log.AppendLine(LogLevel.Error, "{0} ({1}s)", err, (DateTime.Now - StepStartTime).TotalSeconds);
                    equip.IsInterlock = true;
                }
            }




            //SEQUENCE////////////////////////////////////////////////////////////////////////
            if (_stepNum == EmRV_NO.S000_REVIEW_WAIT)
            {
            }
            else if (_stepNum == EmRV_NO.S010_REVIEW_START)
            {
                equip.MccPc.SetMccCategory(MccActionCategory.REVIEW);

                if (IsptAddrW.XF_ReviewStartX.vFloat < 0 || GG.TestMode == true || GG.InspTestMode)
                {
                    _xTargetPos = equip.StageX.Setting.LstServoPosiInfo[StageXServo.ReviewReadyPos].Position;
                    Logger.Log.AppendLine(LogLevel.Warning, "StageX ReviewReady위치 제어지정위치 이동 {0}", _xTargetPos);
                }
                else
                    _xTargetPos = IsptAddrW.XF_ReviewStartX.vFloat;

                if (IsptAddrW.XF_ReviewStartY.vFloat < 0 || GG.TestMode == true || GG.InspTestMode)
                {
                    _yTargetPos = equip.StageY.Setting.LstServoPosiInfo[StageYServo.ReviewReadyPos].Position;
                    Logger.Log.AppendLine(LogLevel.Warning, "StageY ReviewReady위치 제어지정위치 이동 {0}", _yTargetPos);
                }
                else
                    _yTargetPos = IsptAddrW.XF_ReviewStartY.vFloat;
                
                if (equip.StageX.MovePosition(equip, StageXServo.ReviewReadyPos, _xTargetPos) == false) return;
                if (equip.StageY.MovePosition(equip, StageYServo.ReviewReadyPos, _yTargetPos) == false) return;
                //TactTimeMgr.Instance.Set(EM_TT_LST.T080_REVIEW_WAIT_POS_START);

                _stepNum = EmRV_NO.S020_REVIEW_READY_POSI_WAIT;
            }
            else if (_stepNum == EmRV_NO.S020_REVIEW_READY_POSI_WAIT)
            {
                if (equip.StageX.IsMoveOnPosition(_xTargetPos) == true
                    && equip.StageY.IsMoveOnPosition(_yTargetPos) == true
                    )
                {
                    _stepNum = EmRV_NO.S025_REVIEW_READY;                 
                }
            }
            else if (_stepNum == EmRV_NO.S025_REVIEW_READY)
            {
                if (IsptAddrB.YB_MotorInterlockOffState.vBit == true || GG.TestMode == true)
                {
                    TactTimeMgr.Instance.Set(EM_TT_LST.T080_REVIEW_WAIT_POS_END, EM_TT_LST.T090_REVIEW_RUN_START);
                    if (equip.IsReviewSkip == EmReviewSkip.SkipOn)
                    {
                        _stepNum = EmRV_NO.S050_PMAC_REVIEW_STOP_WAIT;
                    }
                    else
                    {
                        if (equip.InspPc.StartCommand(equip, EmInspPcCommand.REVIEW_START, 0) == false) return;
                        _tmrReviTimeOver.Start(0, equip.CtrlSetting.Insp.ReviewOvertime);
                        _stepNum = EmRV_NO.S030_REVIEW_START_ACK_WAIT;
                    }
                }
            }
            else if (_stepNum == EmRV_NO.S030_REVIEW_START_ACK_WAIT)
            {
                if (equip.InspPc.IsCommandAck(equip, EmInspPcCommand.REVIEW_START) == true)
                {
                    _stepNum = EmRV_NO.S040_REVIEW_COMPLETE_WAIT;
                }
            }
            else if (_stepNum == EmRV_NO.S040_REVIEW_COMPLETE_WAIT)
            {
                PassStepTimeoverCheck();
                if (equip.InspPc.IsEventComplete(EmInspPcEvent.REVIEW_COMPLETE) == true)
                {
                    _tmrReviTimeOver.Stop();
                    Logger.Log.AppendLine(LogLevel.Warning, "리뷰 완료");
                    _stepNum = EmRV_NO.S050_PMAC_REVIEW_STOP_WAIT;
                }
                else if (_tmrReviTimeOver == true)
                {
                    _tmrReviTimeOver.Stop();
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0333_REVIEW_TIMEOVER);
                    Logger.Log.AppendLine(LogLevel.Warning, "리뷰 TIME OVER");
                    _stepNum = EmRV_NO.S041_REVIEW_TIME_OVER_START;
                }
            }
            #region Timeover처리            
            else if (_stepNum == EmRV_NO.S041_REVIEW_TIME_OVER_START)
            {
                //jys:: 190727 신경철J 검사에서 명령 안주면 됨
                // if (equip.PMac.StartCommand(equip, EmPMacmd.REVIEW_STOP, 0) == false) return;
                //190727 김태현2K : 인터락신호만 켜주면 다음 동작 안함 (-> equip.IsUseMotorInspSever() 에 스텝에 따라 바꾸게 돼있음)
                //  if (equip.InspPc.StartCommand(equip, EmInspPcCommand.REVIEW_END, 0) == false) return;
                Logger.IsptLog.AppendLine(LogLevel.Warning, "REVI TIME OVER");
                _stepNum = EmRV_NO.S042_REVIEW_TIME_OVER_WAIT;
            }
            else if (_stepNum == EmRV_NO.S042_REVIEW_TIME_OVER_WAIT)
            {
                if (true
                    // && equip.InspPc.IsCommandAck(equip, EmInspPcCommand.REVIEW_END) == true
                    //&& equip.PMac.IsCommandAck(equip, EmPMacmd.REVIEW_STOP) == true
                    )
                {
                    _stepNum = EmRV_NO.S050_PMAC_REVIEW_STOP_WAIT;
                }
            }
            #endregion
            else if (_stepNum == EmRV_NO.S050_PMAC_REVIEW_STOP_WAIT)
            {
                if (equip.PMac.XB_ReviewRunning.vBit == true) return;

                if (equip.StageX.IsMoving == false
                    && equip.StageY.IsMoving == false
                    )
                {
                    _tmrMotorStopWait.Start(0, 2000);
                    _stepNum = EmRV_NO.S055_MOTOR_STOP_DELAY_WAIT;
                }
            }
            else if (_stepNum == EmRV_NO.S055_MOTOR_STOP_DELAY_WAIT)
            {
                if (_tmrMotorStopWait)
                {
                    _tmrMotorStopWait.Stop();
                    TactTimeMgr.Instance.Set(EM_TT_LST.T090_REVIEW_RUN_END, EM_TT_LST.T100_MANUAL_REVIEW_START);
                    if (equip.IsReviewManual == EmReviewManual.Request)
                        equip.IsReviewManual = EmReviewManual.ManualOn;
                    _stepNum = EmRV_NO.S060_CHECK_MANUAL_REVIEW;
                }
            }
            /* 수동리뷰 */
            else if (_stepNum == EmRV_NO.S060_CHECK_MANUAL_REVIEW)
            {
                PassStepTimeoverCheck();
                if (equip.IsReviewManual != EmReviewManual.ManualOn)
                {
                    if (equip.StageX.IsMoving == false
                        && equip.StageY.IsMoving == false)
                    {
                        _tmrMotorStopWait.Start(0, 2000);
                        _stepNum = EmRV_NO.S070_REVIEW_COMPLETE;
                    }
                }
            }
            /* REVIEW COMPLETE */
            else if (_stepNum == EmRV_NO.S070_REVIEW_COMPLETE)
            {
                if (_tmrMotorStopWait)
                {
                    _tmrMotorStopWait.Stop();
                    TactTimeMgr.Instance.Set(EM_TT_LST.T100_MANUAL_REVIEW_END);
                    _isComplete = true;
                    _stepNum = EmRV_NO.S000_REVIEW_WAIT;
                }
            }
        }

        public bool StepStart(Equipment equip)
        {
            if (_stepNum != EmRV_NO.S000_REVIEW_WAIT)
            {
                InterLockMgr.AddInterLock("인터락<실행중>\n(REVIEW STEP 진행중 시작 명령이 들어왔습니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "REVIEW STEP 진행중 시작 명령이 들어옴.");
                equip.IsInterlock = true;
                return false;
            }


            _isComplete = false;
            _stepNum = EmRV_NO.S010_REVIEW_START;
            return true;
        }
        public bool StepStop(Equipment equip)
        {
            _isComplete = false;

            //if (EmRV_NO.S010_REVIEW_START <= _stepNum && _stepNum <= EmRV_NO.S040_REVIEW_COMPLETE_WAIT)
            //jys::todo reviwe end ... equip.InspPc.StartCommand(equip, EmInspPcCommand.r, 0);

            bool isReviewUsing = equip.PMac.XB_ReviewRunning.vBit;
                //|| RvAddrB.YB_ReviewManualMode.vBit
                ;                
            if(isReviewUsing)
                equip.PMac.StartCommand(equip, EmPMacmd.REVIEW_STOP, 0);

            _stepNum = EmRV_NO.S000_REVIEW_WAIT;
            return true;
        }

        public bool IsStepComplete()
        {
            return _isComplete && _stepNum == EmRV_NO.S000_REVIEW_WAIT;
        }
    }
}
