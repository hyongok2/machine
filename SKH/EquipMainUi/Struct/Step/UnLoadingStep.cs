using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using EquipMainUi.Struct.Detail;
using EquipMainUi.Monitor;
using DitCim.PLC;
using EquipMainUi.Struct.Detail.HSMS;
using EquipMainUi.Struct.Detail.PC;
using EquipMainUi.Struct.TransferData;
using EquipMainUi.Tact;

namespace EquipMainUi.Struct.Step
{
    public enum EmUD_NO
    {
        S000_UNLOADING_WAIT,

        S010_UNLOADING_START,
        S011_CHECK_LIFT_PIN_DOWN,
        S012_CHECK_VACUUM_ON,
        S020_SERVO_UNLOADING_POS_START,
        S030_SERVO_UNLOADING_POS_WAIT,
        S040_UNLOADING_ACK_WAIT,
        S060_VACUUM_OFF_START,
        S070_VACUUM_OFF_WAIT,
        S080_LIFT_PIN_UP_WAIT,
        S090_CENTERING_FORWARD_WAIT,
        S100_CENTERING_BACKWARD_WAIT,
        S105_UNLOADING_COMPLETE_WAIT,
        S110_UNLOADING_LONGRUN_CHECK,
        S111_PIO_START,
        S115_MANUAL_UNLOADING_WAIT,
        S120_START_SEND_PIO,
        S130_PIO_RECEIVEABLE_WAIT,
        S140_PIO_COMPLETE_WAIT,
        S145_ROBOT_OUT_CHECK,
        S150_UNLOADING_TMR_WAIT,
        S160_UNLOADING_COMPLETE,
    }

    public class UnLoadingStep : StepBase
    {
        //Loading Step 로직 처리.
        private bool _isComplete = false;

        private EmUD_NO _stepNum = 0;
        private EmUD_NO _stepNumOld = 0;
        public EmUD_NO StepNum { get { return _stepNum; } }
        public bool IsStepUnloading = false;
        public PlcTimerEx TmrLiftPinQualPosiWait = new PlcTimerEx("Liftpin Qual Position Time");
        public PlcTimerEx TmrUnloadBlower = new PlcTimerEx("Unloading Blower Time");
        public PlcTimerEx TmrUnLoadLower = new PlcTimerEx("UnLoading Lower Time");
        public PlcTimerEx TmrThetaWait = new PlcTimerEx("Theta Wait Time");
        private PlcTimerEx _tmrUnloadingCompleteTimeOver = new PlcTimerEx("Unloading Complete Time Over");
        private PlcTimerEx CenteringForwardTmrDelay = new PlcTimerEx("Centering Foward Wait Delay");
        private PlcTimerEx _tmrVacOffToLiftpinUpDelay = new PlcTimerEx("_tmrVacOffToLiftpinUpDelay");


        public UnLoadingStep()
        {
        }
        public override void LogicWorking(Equipment equip)
        {
            if (equip.IsPause)
            {
                StepStartTime = DateTime.Now;
            }
            if (equip.IsImmediatStop) return;
            if(_stepNum == EmUD_NO.S111_PIO_START)
            {
                bool pinDetect1 = equip.WaferDetectSensorLiftpin1.IsOn;
                bool pinDetect2 = equip.WaferDetectSensorLiftpin2.IsOn;
                bool stageDetect = equip.WaferDetectSensorStage1.IsOn;
                Logger.Log.AppendLine("S111_PIO_START Step 웨이퍼 감지 센서 감지 여부\n LiftPin1 Sensor : {0}\nLiftPin2 Sensor : {1}\nDetect Sensor : {2}",
                    pinDetect1 == true ? "감지" : "미감지",
                    pinDetect2 == true ? "감지" : "미감지",
                    stageDetect == true ? "감지" : "미감지");
            }
            if (_stepNum != _stepNumOld)
            {
                Logger.Log.AppendLine(LogLevel.AllLog, "★[UNLOADING STEP] = {1} -> {0} Step 소요시간 {2}s",
                    _stepNum.ToString(), _stepNumOld.ToString(), (DateTime.Now - StepStartTime).TotalSeconds);

                StepStartTime = DateTime.Now;
                _stepNumOld = _stepNum;
            }
            else if (GG.TestMode == false)
            {
                if (_stepNum > 0 &&
                    (DateTime.Now - StepStartTime).TotalMilliseconds > equip.CtrlSetting.Ctrl.AutoStepTimeout)
                {
                    string err = string.Format("[UNLOADING STEP] = {0} 중 AutoStep Timeover 발생", _stepNum.ToString());
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0289_AUTO_STEP_OVERTIME);
                    InterLockMgr.AddInterLock("AutoStep Timeover\n" + err);
                    Logger.Log.AppendLine(LogLevel.Error, "{0} ({1}s)", err, (DateTime.Now - StepStartTime).TotalSeconds);
                    equip.IsInterlock = true;
                }
            }

            //SEQUENCE////////////////////////////////////////////////////////////////////////
            if (_stepNum == EmUD_NO.S000_UNLOADING_WAIT)
            {
            }
            else if (_stepNum == EmUD_NO.S010_UNLOADING_START)
            {
                if (equip.IsGlassUnloading == true || GG.EfemNoWafer == true)
                    _stepNum = EmUD_NO.S011_CHECK_LIFT_PIN_DOWN;
                else
                    _stepNum = EmUD_NO.S020_SERVO_UNLOADING_POS_START;
            }
            #region 강제 Unloading 배출 시퀀스 시
            else if (_stepNum == EmUD_NO.S011_CHECK_LIFT_PIN_DOWN)
            {
                if (equip.Vacuum.AllVacuumOn() == false) return;
                if (equip.LiftPin.Backward(equip) == false) return;
                _stepNum = EmUD_NO.S012_CHECK_VACUUM_ON;
            }
            else if (_stepNum == EmUD_NO.S012_CHECK_VACUUM_ON)
            {
                if (equip.LiftPin.IsBackward && (equip.Vacuum.IsVacuumOn || equip.IsNoGlassMode))
                {
                    _stepNum = EmUD_NO.S020_SERVO_UNLOADING_POS_START;
                }
            }
            #endregion

            else if (_stepNum == EmUD_NO.S020_SERVO_UNLOADING_POS_START)
            {
                double degree = 0;
                try
                {
                    _tempWaferInfo = TransferDataMgr.GetWafer(equip.TransferUnit.LowerWaferKey);
                    degree = _tempWaferInfo.NotchToDegreeStdAVI() * -1; // 노치위치 반대로 움직여야 하므로
                }
                catch (Exception ex)
                {
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0660_NO_WAFER_INFO);
                    equip.IsInterlock = true;
                }

                //센터링 사용 시
                //if (equip.Theta.MoveLoadingEx(equip, 0) == false) return;
                if (equip.StageX.MoveLoadingEx(equip, 0) == false) return;
                if (equip.StageY.MoveLoadingEx(equip, 0) == false) return;

                //보정사용시
                //double xUnldOffset, yUnldOffset, tUnldOffset;
                //GetPreAlignRestorePos(equip, out xUnldOffset, out yUnldOffset, out tUnldOffset);                
                if (equip.Theta.MoveCalLoadingPos(equip, degree) == false) return;
                //if (equip.StageX.MoveLoadingEx(equip, (float)xUnldOffset) == false) return;
                //if (equip.StageY.MoveLoadingEx(equip, (float)yUnldOffset) == false) return;

                if (equip.IsGlassUnloading == false)
                    if (equip.InspPc.StartCommand(equip, EmInspPcCommand.UNLOADING, 0) == false) return;

                TactTimeMgr.Instance.Set(EM_TT_LST.T110_POS_UNLOADING_START);
                _stepNum = EmUD_NO.S030_SERVO_UNLOADING_POS_WAIT;

            }
            else if (_stepNum == EmUD_NO.S030_SERVO_UNLOADING_POS_WAIT)
            {
                if (true
                    && equip.StageX.IsMoveOnPosition(StageXServo.LoadingPos)
                    && equip.StageY.IsMoveOnPosition(StageYServo.LoadingPos)
                    && equip.Theta.IsMoveOnPosition(ThetaServo.LoadingPos)
                    )
                {
                    _stepNum = EmUD_NO.S040_UNLOADING_ACK_WAIT;                    
                }
            }
            else if (_stepNum == EmUD_NO.S040_UNLOADING_ACK_WAIT)
            {
                if (equip.IsGlassUnloading == true 
                    || GG.EfemNoWafer == true
                    || equip.InspPc.IsCommandAck(equip, EmInspPcCommand.UNLOADING) == true)
                {
                    _stepNum = EmUD_NO.S060_VACUUM_OFF_START;
                }
            }
            else if (_stepNum == EmUD_NO.S060_VACUUM_OFF_START)
            {
                TactTimeMgr.Instance.Set(EM_TT_LST.T110_POS_UNLOADING_END, EM_TT_LST.T120_VACUUM_OFF_START);
                equip.Vacuum.StartOffStep();
                _tmrVacOffToLiftpinUpDelay.Start(0, 200);
                _stepNum = EmUD_NO.S070_VACUUM_OFF_WAIT;
            }
            else if (_stepNum == EmUD_NO.S070_VACUUM_OFF_WAIT)
            {
                if (equip.Vacuum.IsVacuumOff == true && _tmrVacOffToLiftpinUpDelay == true)
                {
                    TactTimeMgr.Instance.Set(EM_TT_LST.T120_VACUUM_OFF_END, EM_TT_LST.T130_LIFT_PIN_UP_START);
                    if (equip.LiftPin.Forward(equip) == false) return;

                    _tmrVacOffToLiftpinUpDelay.Stop();
                    _stepNum = EmUD_NO.S080_LIFT_PIN_UP_WAIT;
                }
            }
            else if (_stepNum == EmUD_NO.S080_LIFT_PIN_UP_WAIT)
            {
                if (equip.LiftPin.IsForward == true)
                {
                    TactTimeMgr.Instance.Set(EM_TT_LST.T130_LIFT_PIN_UP_END, EM_TT_LST.T140_ALL_CENTERING_START);
                    if (equip.Centering.CenteringFoward() == false) return;

                    _stepNum = EmUD_NO.S090_CENTERING_FORWARD_WAIT;
                }
            }

            else if (_stepNum == EmUD_NO.S090_CENTERING_FORWARD_WAIT)
            {
                if (equip.Centering.IsCenteringForward(equip))
                {
                    if (CenteringForwardTmrDelay.IsStart == false)
                        CenteringForwardTmrDelay.Start(0, equip.CtrlSetting.Ctrl.CenteringForwardWait);
                    else if (CenteringForwardTmrDelay)
                    {
                        CenteringForwardTmrDelay.Stop();
                        TactTimeMgr.Instance.Set(EM_TT_LST.T140_ALL_CENTERING_END, EM_TT_LST.T150_ALL_UNCENTERING_START);
                        if (equip.Centering.CenteringBackward() == false) return;
                        _stepNum = EmUD_NO.S100_CENTERING_BACKWARD_WAIT;
                    }
                }
            }
            else if (_stepNum == EmUD_NO.S100_CENTERING_BACKWARD_WAIT)
            {
                if (equip.Centering.IsCenteringBackward(equip) == true)
                {
                    TactTimeMgr.Instance.Set(EM_TT_LST.T150_ALL_UNCENTERING_END, EM_TT_LST.T155_INSP_UNLOADING_SIGNAL_COMPLETE_START);
                    _tmrUnloadingCompleteTimeOver.Start(0, equip.CtrlSetting.Insp.JudgeCompleteTimeOver);
                    _stepNum = EmUD_NO.S105_UNLOADING_COMPLETE_WAIT;
                }
            }
            else if (_stepNum == EmUD_NO.S105_UNLOADING_COMPLETE_WAIT)
            {
                if (equip.IsGlassUnloading == true 
                    || equip.InspPc.IsEventComplete(EmInspPcEvent.UNLOADING_COMPLETE) == true)
                {
                    _tmrUnloadingCompleteTimeOver.Stop();
                    _stepNum = EmUD_NO.S110_UNLOADING_LONGRUN_CHECK;
                }
                else if (_tmrUnloadingCompleteTimeOver == true)
                {
                    _tmrUnloadingCompleteTimeOver.Stop();
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0328_INSPECTION_UNLOADING_COMPLETE_TIMEOVER_ERROR);
                    _stepNum = EmUD_NO.S110_UNLOADING_LONGRUN_CHECK;
                }                
            }
            else if (_stepNum == EmUD_NO.S110_UNLOADING_LONGRUN_CHECK)
            {
                TactTimeMgr.Instance.Set(EM_TT_LST.T155_INSP_UNLOADING_SIGNAL_COMPLETE_END);
                TactTimeMgr.Instance.LogTactTime(equip);

                equip.IsReviewSkip = EmReviewSkip.None;
                equip.IsReviewManual = EmReviewManual.None;

                if (equip.IsTTTMMode)
                    equip.processTTTMCount++;

                if (GG.Equip.CtrlSetting.InspRepeatMode)
                {
                    if((GG.Equip.CtrlSetting.InspRepeatCount + 1) == equip.processStatsToolCount)
                    {
                        if (equip.InspPc.StartCommand(equip, EmInspPcCommand.STATS_TOOL_START, GG.Equip.TransferUnit.LowerWaferKey) == false) return;

                        equip.processStatsToolCount = 1;

                        GG.Equip.StLoading._firstTry = true;
                        _stepNum = EmUD_NO.S111_PIO_START;
                        return;
                    }
                    else
                    {
                        GG.Equip.StLoading._firstTry = false;
                        equip.processStatsToolCount++;
                        _stepNum = EmUD_NO.S160_UNLOADING_COMPLETE;
                        return;
                    }
                }

                if (equip.IsLongTest || (equip.IsNoGlassMode && GG.EfemNoWafer == false))
                {
                    _stepNum = EmUD_NO.S160_UNLOADING_COMPLETE;
                }
                else
                {
                    _stepNum = EmUD_NO.S111_PIO_START;
                }
            }
            #region PIO
            else if (_stepNum == EmUD_NO.S111_PIO_START)
            {
                bool pinDetect1 = equip.WaferDetectSensorLiftpin1.IsOn;
                bool pinDetect2 = equip.WaferDetectSensorLiftpin2.IsOn;
                bool stageDetect = equip.WaferDetectSensorStage1.IsOn;
                if (equip.IsWaferDetect != EmGlassDetect.ALL)
                {
                    InterLockMgr.AddInterLock("웨이퍼 배출 시 웨이퍼 감지 센서가 모두 감지되어야 하는데 일부가 감지되지 않습니다", "LiftPin1 Sensor : {0}\nLiftPin2 Sensor : {1}\nDetect Sensor : {2}",
                        pinDetect1 == true ? "감지" : "미감지",
                        pinDetect2 == true ? "감지" : "미감지",
                        stageDetect == true ? "감지" : "미감지");
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0621_UNLD_PIO_AVI_WAFER_DETECT_ABNORMAL);
                    equip.IsInterlock = true;
                }
                if (TransferDataMgr.IsExistWafer(equip.TransferUnit.LowerWaferKey) == false)
                {
                    InterLockMgr.AddInterLock(string.Format("웨이퍼 정보가 없어 배출각도를 알 수 없습니다[{0} / {1] Slot]", equip.TransferUnit.LowerWaferKey.CstID, equip.TransferUnit.LowerWaferKey.SlotNo));
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0660_NO_WAFER_INFO);
                    equip.IsInterlock = true;
                }

                if (GG.ManualLdUld == true)
                {
                    equip.IsPause = true;
                    _stepNum = EmUD_NO.S115_MANUAL_UNLOADING_WAIT;
                }
                else
                {
                    _stepNum = EmUD_NO.S120_START_SEND_PIO;
                }
            }
            else if (_stepNum == EmUD_NO.S115_MANUAL_UNLOADING_WAIT)
            {
                _stepNum = EmUD_NO.S160_UNLOADING_COMPLETE;
            }
            else if (_stepNum == EmUD_NO.S120_START_SEND_PIO)
            {
                if ((equip.Efem.LoadPort1.CstKey == null ? true : equip.TransferUnit.LowerWaferKey.CstID != equip.Efem.LoadPort1.CstKey.ID)
                    && (equip.Efem.LoadPort2.CstKey == null ? true : equip.TransferUnit.LowerWaferKey.CstID != equip.Efem.LoadPort2.CstKey.ID))
                {
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0661_AVI_WAFER_CST_ID_NOT_MATCH);
                }
                Logger.TacttimeLog.AppendLine(LogLevel.Info, "[EFEM TACT]AVI UNLOAD READY------------------");
                equip.MccPc.SetMccAction(MccActionItem.COMPONENT_OUT, true);
                if (equip.PioA2ISend.StartSendPio(equip) == false) return;
                TactTimeMgr.Instance.Set(EM_TT_LST.T160_PIO_SEND_WAIT_START);
                //EFEMTactMgr.Instance.Set(EFEM_TACT_VALUE.T090_INSPECTOR_END, EFEM_TACT_VALUE.T095_WAIT_ROBOT_PICK_START, equip.TransferUnit.LowerWaferKey);
                _stepNum = EmUD_NO.S130_PIO_RECEIVEABLE_WAIT;
            }
            else if (_stepNum == EmUD_NO.S130_PIO_RECEIVEABLE_WAIT)
            {
                PassStepTimeoverCheck();
                if (equip.PioSend.XRecvAble == true)
                {
                    TactTimeMgr.Instance.Set(EM_TT_LST.T160_PIO_SEND_WAIT_END, EM_TT_LST.T170_PIO_SEND_ACTUAL_START);
                    _stepNum = EmUD_NO.S140_PIO_COMPLETE_WAIT;
                }
            }
            else if (_stepNum == EmUD_NO.S140_PIO_COMPLETE_WAIT)
            {
                PassStepTimeoverCheck();
                if (equip.PioA2ISend.IsSendComplete())
                {
                    if (equip.IsWaferDetect != EmGlassDetect.NOT)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0620_LOADING_PIO_AVI_WAFER_NO_DETECT);
                        equip.IsInterlock = true;
                        return;
                    }

                    //TactTimeMgr.Instance.Set(EM_TT_LST.T170_PIO_SEND_ACTUAL_END);
                    equip.IsUseSelectInspOrder = false;

                    Logger.Log.AppendLine(LogLevel.Info, "COMPONENT OUT {0}.{1}", equip.TransferUnit.LowerWaferKey.CstID, equip.TransferUnit.LowerWaferKey.SlotNo);                    

                    equip.MccPc.SetMccAction(MccActionItem.COMPONENT_OUT, false);
                    //equip.MccPc.SetMccEventGlassOut(equip.FrontUnloadingGlassInfo, equip.UnloadingWaferInfo);
                    TmrUnLoadLower.Start(1);
                    equip.PinIonizer.IonizerOff();
                    _stepNum = EmUD_NO.S145_ROBOT_OUT_CHECK;
                }
            }
            else if (_stepNum == EmUD_NO.S145_ROBOT_OUT_CHECK)
            {
                if (equip.IsEFEMInputArm.IsOn == false && equip.RobotArmDefect.IsOn == false)
                {
                    _stepNum = EmUD_NO.S150_UNLOADING_TMR_WAIT;
                }
            }
            #endregion
            else if (_stepNum == EmUD_NO.S150_UNLOADING_TMR_WAIT)
            {
                if (TmrUnLoadLower)
                {
                    _stepNum = EmUD_NO.S160_UNLOADING_COMPLETE;
                    TmrUnLoadLower.Stop();
                }
            }
            else if (_stepNum == EmUD_NO.S160_UNLOADING_COMPLETE)
            {
                _isComplete = true;
                TactTimeMgr.Instance.Set(EM_TT_LST.T170_PIO_SEND_ACTUAL_END);
                _stepNum = EmUD_NO.S000_UNLOADING_WAIT;
                equip.MccPc.SetMccAction(MccActionItem.AUTO_CYCLE, false, MccActionCategory.LOADING);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="equip"></param>
        /// <param name="xUnldOffset">PreAlign 이동 대비 원복해야되는 Offset 리턴 (언로딩 = 로딩 + 리턴값)</param>
        /// <param name="yUnldOffset">PreAlign 이동 대비 원복해야되는 Offset 리턴 (언로딩 = 로딩 + 리턴값)</param>
        /// <param name="tUnldOffset">PreAlign 이동 대비 이동해야되는 값 리턴 (언로딩 = 리턴값)</param>
        private void GetPreAlignRestorePos(Equipment equip, out double xUnldOffset, out double yUnldOffset, out double tUnldOffset)
        {
            tUnldOffset = 0;
            yUnldOffset = 0;
            xUnldOffset = 0;
            //나중에 사용 시 190805 기준 프리얼라이너 제공값 반경 필요
            //double alignerT, alignerX, alignerY;
            //xUnldOffset = 0;
            //yUnldOffset = 0;
            //tUnldOffset = 0;

            //try
            //{
            //    WaferInfo w = TransferDataMgr.GetWafer(equip.LowerWaferKey);
            //    double? x = w.OffsetX - w.SettingX;
            //    double? y = w.OffsetY - w.SettingY;
            //    double? t = w.OffsetT - w.SettingT;
            //    alignerX = (double)x;
            //    alignerY = (double)y;
            //    alignerT = (double)t; // Aligner에서 t만큼 돌렸다
            //    if (Math.Abs(alignerX) > StageXServo.AllowUnldRange) AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0673_PREALIGN_X_LIMIT_OVER);
            //    if (Math.Abs(alignerY) > StageYServo.AllowUnldRange) AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0674_PREALIGN_Y_LIMIT_OVER);
            //    if (equip.Theta.IsCanLoadingPos(alignerT) == false) AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0675_PREALIGN_T_LIMIT_OVER);

            //    Logger.Log.AppendLine(LogLevel.Info, "PreAlignRestore X Ready : Aligner 전:{0}um 후:{1}um");
            //    Logger.Log.AppendLine(LogLevel.Info, "PreAlignRestore Y Ready : Aligner 전:{0}um 후:{1}um");
            //    Logger.Log.AppendLine(LogLevel.Info, "PreAlignRestore T Ready : Aligner 전:{0} 후:{1}");
            //}
            //catch (Exception ex)
            //{
            //    alignerX = -9999;
            //    alignerY = -9999;
            //    alignerT = -9999;
            //    Logger.Log.AppendLine(LogLevel.Info, "Pre Align 값 저장안됨");
            //    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0660_NO_WAFER_INFO);
            //}

            //if (alignerT > -270)
            //{                
            //    tUnldOffset = -alignerT; // Aligner에서 돌린 값의 반대로 돌려야함
            //    tUnldOffset = equip.Theta.IsCantMoveLdOffsetPos(tUnldOffset) ? ThetaServo.UnldOffset + tUnldOffset : tUnldOffset;
            //}
            //if (alignerX > -100)
            //{
            //    xUnldOffset = -alignerX;
            //}
            //if (alignerY > -100)
            //{
            //    yUnldOffset = -alignerY;
            //}
            //Logger.Log.AppendLine(LogLevel.Info, "PreAlignRestore X Ready : 배출 시 이동Offset {0}mm, 현재위치 {1}mm", xUnldOffset, equip.StageX.XF_CurrMotorPosition.vFloat);
            //Logger.Log.AppendLine(LogLevel.Info, "PreAlignRestore Y Ready : 배출 시 이동Offset {0}mm, 현재위치 {1}mm", yUnldOffset, equip.StageY.XF_CurrMotorPosition.vFloat);
            //Logger.Log.AppendLine(LogLevel.Info, "PreAlignRestore T Ready : 배출 시 회전Offset {0}, 현재위치 {1}", tUnldOffset, equip.Theta.XF_CurrMotorPosition.vFloat);
        }

        public bool StepStart(Equipment equip)
        {
            if (_stepNum != EmUD_NO.S000_UNLOADING_WAIT)
            {
                InterLockMgr.AddInterLock("인터락<실행중>\n(UNLOADING STEP 진행중 시작 명령이 들어왔습니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "UNLOADING STEP 진행중 시작 명령이 들어옴.");

                equip.IsInterlock = true;
                return false;
            }

            _isComplete = false;
            _stepNum = EmUD_NO.S010_UNLOADING_START;

            return true;
        }
        public bool StepStop(Equipment equip)
        {
            _isComplete = false;
            _stepNum = EmUD_NO.S000_UNLOADING_WAIT;

            return true;
        }
        public bool IsStepComplete()
        {
            return _isComplete && _stepNum == EmUD_NO.S000_UNLOADING_WAIT;
        }
    }
}
