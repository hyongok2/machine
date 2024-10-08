using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using EquipMainUi.Struct.Detail;
using EquipMainUi.Monitor;
using EquipMainUi.Struct.Detail.HSMS;
using DitCim.PLC;
using EquipMainUi.Setting;
using EquipMainUi.Struct.Detail.PC;
using EquipMainUi.Struct.TransferData;

namespace EquipMainUi.Struct.Step
{
    public enum EmLD_NO
    {
        S000_LOADING_WAIT,
        S005_PAUSE_OFF_WAIT,                
        S010_LOADING_START,
        S015_LIFTPIN_DOWN,
        S016_WAIT_LIFTPIN_DOWN,
        S020_SERVO_GO_LD_POSI_START,
        S030_SERVO_GO_LD_POSI_WAIT,
        S035_WAFER_CHECK,
        S040_VACUUM_OFF,
        S045_VACUUM_OFF_WAIT,
        S050_LIFT_PIN_UP_WAIT,

        S060_PIO_START,
        S061_MANUAL_LOADING_WAIT,
        S070_PIO_RECV_ABLE_ON,
        S075_PIO_SEND_ABLE_WAIT,

        S080_THETA_READY_START,
        S081_PIN_DOWN_WAIT,
        S082_THETA_MOVE_WAIT,
        S083_PIN_UP_WAIT, 
        
        S090_PIO_CONTINUE,
        S095_PIO_COMPLETE_WAIT,

        S100_ROBOT_OUT_CHECK,
        S110_START_LOADING,
        S130_LIFT_PIN_DOWN_START,
        S140_VACUUM_ON_WAIT,
        S145_WAFER_DOWN_COMPLETE,
        S150_ALIGN_POSI_MOVE_START,
        S160_INSP_LOADING_ACK_WAIT,
        S170_INSP_LOADING_COMPLETE_WAIT,

        S180_ALIGN_POS_MOVE_COMPLETE_WAIT,
        S185_ALIGN_START,
        S200_ALIGN_COMPLETE_ACK_WAIT,

        S210_CHECK_TTTM_MODE,

        S220_SCAN_READY_POSI_MOVE_START,

        S230_LOADING_COMPLETE_WAIT,
        S240_LOADING_COMPLETE,
        S105_WAFER_MAP_DOWNLOAD_COMPLETE,
    }

    public class LoadingStep : StepBase
    {
        //Loading Step 로직 처리. 
        private PlcTimerEx BlowerDelay = new PlcTimerEx("Blower On Time Delay");
        private PlcTimerEx CenteringForwardTmrDelay = new PlcTimerEx("Centering Foward Delay");        
        private PlcTimerEx _vacuumOnDelay = new PlcTimerEx("Vacuum On Wait Delay");
        private PlcTimerEx _tmrThetaAlignTimeOver = new PlcTimerEx("THETA ALIGN TIMEOVER");
        private PlcTimerEx _InspCompleteEvtTimeover = new PlcTimerEx("Inspection Loading Complete TIME OEVER");
        private int _thetaAlignCurTry = 0;
        private bool _passCheckGlassType;
        private EmLD_NO _stepNum = 0;
        private EmLD_NO _stepNumOld = 0;
        public EmLD_NO StepNum { get { return _stepNum; } }

        private bool _isComplete = false;

        private float _xTargetPos;
        private float _yTargetPos;
        private float _tTargetPos;
        public bool _firstTry = true;

        public LoadingStep()
        {
        }
        //메소드 - 스템 시퀀스...
        public override void LogicWorking(Equipment equip)
        {
            if(equip.IsPause)
            {
                StepStartTime = DateTime.Now;
            }
            if (equip.IsImmediatStop) return;

            if (_stepNum != _stepNumOld)
            {
                Logger.Log.AppendLine(LogLevel.AllLog, "★[LOADING STEP] = {0} Step 소요시간 {1}s",
                    _stepNum.ToString(), (DateTime.Now - StepStartTime).TotalSeconds);

                StepStartTime = DateTime.Now;
                _stepNumOld = _stepNum;
            }
            else if (GG.TestMode == false)
            {
                if (_stepNum > 0 &&
                    (DateTime.Now - StepStartTime).TotalMilliseconds > equip.CtrlSetting.Ctrl.AutoStepTimeout)
                {
                    string err = string.Format("[LOADING STEP] = {0} 중 AutoStep Timeover 발생", _stepNum.ToString());
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0289_AUTO_STEP_OVERTIME);
                    InterLockMgr.AddInterLock("AutoStep Timeover\n" + err);
                    Logger.Log.AppendLine(LogLevel.Error, "{0} ({1}s)", err, (DateTime.Now - StepStartTime).TotalSeconds);
                    Logger.Log.AppendLine(LogLevel.Info, "스테이지 버큠 현재상태 {0} / {1}kPa  링프레임 버큠 현재 상태 {2} / {3}kPa",
                        GG.Equip.Vacuum.Stage1.IsOnOff ? "ON":"OFF", GG.Equip.ADC.CheckVacuum1, GG.Equip.Vacuum.Stage2.IsOnOff?"ON":"OFF", GG.Equip.ADC.CheckVacuum2);
                    equip.IsInterlock = true;
                }
            }

            if (equip.EquipRunMode != EmEquipRunMode.Auto && _stepNum != EmLD_NO.S000_LOADING_WAIT)
            {
                Logger.Log.AppendLine(LogLevel.Warning, "MANUAL중 LOADING STEP = {0}이 들어옴.");
                _stepNum = EmLD_NO.S000_LOADING_WAIT;
            }

            //SEQUENCE////////////////////////////////////////////////////////////////////////
            if (_stepNum == EmLD_NO.S000_LOADING_WAIT)
            {
            }
            else if (_stepNum == EmLD_NO.S005_PAUSE_OFF_WAIT)
            {
                _stepNum = EmLD_NO.S010_LOADING_START;
            }
            else if (_stepNum == EmLD_NO.S010_LOADING_START)
            {
                if (GG.Equip.CtrlSetting.InspRepeatMode && _firstTry == false)
                {
                    _stepNum = EmLD_NO.S110_START_LOADING;
                    return;
                }

                if (equip.LiftPin.IsBackward == true)
                    _stepNum = EmLD_NO.S020_SERVO_GO_LD_POSI_START;
                else if (equip.LiftPin.IsForward == true)
                    _stepNum = EmLD_NO.S040_VACUUM_OFF; //S040_VACUUM_OFF,  S015_LIFTPIN_DOWN
                else
                {
                    equip.IsPause = true;
                    InterLockMgr.AddInterLock("리프트핀 센서 이상");
                }
            }
            else if (_stepNum == EmLD_NO.S015_LIFTPIN_DOWN)
            {
                if (equip.LiftPin.Backward(equip) == false) return;
                _stepNum = EmLD_NO.S016_WAIT_LIFTPIN_DOWN;
            }
            else if (_stepNum == EmLD_NO.S016_WAIT_LIFTPIN_DOWN)
            {
                if (equip.LiftPin.IsBackward)
                {
                    _stepNum = EmLD_NO.S020_SERVO_GO_LD_POSI_START;
                }
            }
            else if (_stepNum == EmLD_NO.S020_SERVO_GO_LD_POSI_START)
            {
                if (equip.StageX.MoveLoadingEx(equip) == false) return;
                if (equip.StageY.MoveLoadingEx(equip) == false) return;
                if (equip.Theta.MoveLoadingEx(equip) == false) return;
                _stepNum = EmLD_NO.S030_SERVO_GO_LD_POSI_WAIT;
            }
            else if (_stepNum == EmLD_NO.S030_SERVO_GO_LD_POSI_WAIT)
            {
                if (equip.StageX.IsMoveOnPosition(StageXServo.LoadingPos) == true
                    && equip.StageY.IsMoveOnPosition(StageYServo.LoadingPos) == true
                    && equip.Theta.IsMoveOnPosition(ThetaServo.LoadingPos) == true
                    )
                {
                    _stepNum = EmLD_NO.S035_WAFER_CHECK;
                }
            }         
            else if (_stepNum == EmLD_NO.S035_WAFER_CHECK)
            {
                if (equip.IsWaferDetect == EmGlassDetect.NOT)
                {
                    _stepNum = EmLD_NO.S040_VACUUM_OFF;
                }
                else
                {
                    _stepNum = EmLD_NO.S100_ROBOT_OUT_CHECK;
                }
            }
            else if (_stepNum == EmLD_NO.S040_VACUUM_OFF)
            {
                equip.Vacuum.StartOffStep();
                _stepNum = EmLD_NO.S045_VACUUM_OFF_WAIT;
            }
            else if (_stepNum == EmLD_NO.S045_VACUUM_OFF_WAIT)
            {
                if (equip.Vacuum.IsVacuumOff == true)
                {
                    if (equip.LiftPin.Forward(equip) == false) return;
                    _stepNum = EmLD_NO.S050_LIFT_PIN_UP_WAIT;
                }
            }
            else if (_stepNum == EmLD_NO.S050_LIFT_PIN_UP_WAIT)
            {
                if (equip.IsWaferDetect == EmGlassDetect.NOT)
                {
                    _stepNum = EmLD_NO.S060_PIO_START;
                }
                else
                {
                    _stepNum = EmLD_NO.S100_ROBOT_OUT_CHECK;
                }
            }
            #region PIO            
            else if (_stepNum == EmLD_NO.S060_PIO_START)
            {
                if (equip.LiftPin.IsForward == true)
                {
                    if (GG.ManualLdUld == true)
                    {
                        equip.IsPause = true;
                        _stepNum = EmLD_NO.S061_MANUAL_LOADING_WAIT;
                    }
                    else
                    {                        
                        TactTimeMgr.Instance.Set(EM_TT_LST.T000_PIO_RECEIVE_WAIT_START);
                        _stepNum = EmLD_NO.S070_PIO_RECV_ABLE_ON;
                    }
                }
            }
            else if (_stepNum == EmLD_NO.S061_MANUAL_LOADING_WAIT)
            {
                if (equip.IsWaferDetect == EmGlassDetect.ALL)
                {
                    _stepNum = EmLD_NO.S100_ROBOT_OUT_CHECK;
                }
            }
            else if (_stepNum == EmLD_NO.S070_PIO_RECV_ABLE_ON)
            {                
                equip.PioRecv.YRecvAble = true;
                _stepNum = EmLD_NO.S075_PIO_SEND_ABLE_WAIT;
            }
            else if (_stepNum == EmLD_NO.S075_PIO_SEND_ABLE_WAIT)
            {
                PassStepTimeoverCheck();
                if (equip.PioRecv.XSendAble == true)
                {
                    //if (GG.EfemNoUse == false)
                    //{
                    //    double alignerRotation, rotaionAtUnld;
                    //    try
                    //    {
                    //        WaferInfo w = TransferDataMgr.GetWafer(equip.Efem.Robot.LowerWaferKey);
                    //        double? t = w.OffsetT - w.SettingT;
                    //        alignerRotation = (double)t; // Aligner에서 t만큼 돌렸다

                    //        Logger.Log.AppendLine(LogLevel.Info, "LoadingStep Theta Ready : Aligner 전:{0} 후:{1} 배출 시 회전Offset {2}, 현재위치 {3}", w.SettingT, w.OffsetT, -alignerRotation, equip.Theta.XF_CurrMotorPosition.vFloat);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        alignerRotation = -9999;
                    //        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0660_NO_WAFER_INFO);
                    //    }
                    //    rotaionAtUnld = -alignerRotation;
                    //    if (alignerRotation > -9000
                    //        &&
                    //        (equip.Theta.IsCantMoveLdOffsetPos(rotaionAtUnld) == true // 배출 시 위치가 이동 불가 위치인지 확인하여 ThetaServo.UnldOffset 만큼 이동하기 위한 스텝으로.
                    //        ))
                    //    {
                    //        _stepNum = EmLD_NO.S080_THETA_READY_START;
                    //    }
                    //    else
                    //        _stepNum = EmLD_NO.S090_PIO_CONTINUE;
                    //}
                    //else
                    //    _stepNum = EmLD_NO.S090_PIO_CONTINUE;

                    //TactTimeMgr.Instance.Set(EM_TT_LST.T000_PIO_RECEIVE_WAIT_END, EM_TT_LST.T010_PIO_RECEIVE_ACTUAL_START);
                    equip.PinIonizer.IonizerOn();
                    _stepNum = EmLD_NO.S090_PIO_CONTINUE;
                }
            }
            else if (_stepNum == EmLD_NO.S080_THETA_READY_START)
            {
                if (equip.LiftPin.Backward(equip) == false) return;
                _stepNum = EmLD_NO.S081_PIN_DOWN_WAIT;
            }
            else if (_stepNum == EmLD_NO.S081_PIN_DOWN_WAIT)
            {
                if (equip.LiftPin.IsBackward == true)
                {
                    if (equip.Theta.MoveLoadingEx(equip, (float)ThetaServo.UnldOffset) == false) return;
                    _stepNum = EmLD_NO.S082_THETA_MOVE_WAIT;
                }
            }
            else if (_stepNum == EmLD_NO.S082_THETA_MOVE_WAIT)
            {
                if (equip.Theta.IsMoveOnPosition(ThetaServo.LoadingPos) == true)
                {
                    if (equip.LiftPin.Forward(equip) == false) return;
                    _stepNum = EmLD_NO.S083_PIN_UP_WAIT;
                }
            }
            else if (_stepNum == EmLD_NO.S083_PIN_UP_WAIT)
            {
                if (equip.LiftPin.IsForward == true)
                {
                    _stepNum = EmLD_NO.S090_PIO_CONTINUE;
                }
            }
            else if (_stepNum == EmLD_NO.S090_PIO_CONTINUE)
            {
                if (equip.PioI2ARecv.StartPioRecv(equip) == false) return;
                _stepNum = EmLD_NO.S095_PIO_COMPLETE_WAIT;
            }
            else if (_stepNum == EmLD_NO.S095_PIO_COMPLETE_WAIT)
            {
                PassStepTimeoverCheck();
                if (equip.PioI2ARecv.IsPioReciveComplete())
                {
                    if (equip.IsWaferDetect == EmGlassDetect.NOT && equip.IsNoGlassMode == false)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0620_LOADING_PIO_AVI_WAFER_NO_DETECT);
                        equip.IsInterlock = true;
                        return;
                    }

                    TactTimeMgr.Instance.Set(EM_TT_LST.T010_PIO_RECEIVE_ACTUAL_END);

                    //equip.FrontLoadingGlassInfo = equip.HsmsPc.ReadGlassInfoFromHsms(CIMAW.GLASS_INFO_CIM2AOI_F);
                    //equip.FrontLoadingGlassBak.Backup(equip.FrontLoadingGlassInfo);
                    //equip.FrontResultJudgeGlassInfo.CopyData(equip.FrontLoadingGlassInfo);
                    //equip.FrontResultJudgeGlassBak.Backup(equip.FrontLoadingGlassInfo);
                    //equip.HsmsPc.StartCommand(equip, EmHsmsPcCommand.GLASS_MODULE_IN, null);                    

                    Logger.Log.AppendLine(LogLevel.Info, "COMPONENT IN");

                    _stepNum = EmLD_NO.S100_ROBOT_OUT_CHECK;
                }
            }
            #endregion
            else if (_stepNum == EmLD_NO.S100_ROBOT_OUT_CHECK)
            {
                if (equip.IsEFEMInputArm.IsOn == false && equip.RobotArmDefect.IsOn == false)
                {
                    _stepNum = EmLD_NO.S105_WAFER_MAP_DOWNLOAD_COMPLETE;
                }
            }
            else if(_stepNum == EmLD_NO.S105_WAFER_MAP_DOWNLOAD_COMPLETE)
            {
                if(GG.CimTestMode == true)
                {
                    _stepNum = EmLD_NO.S110_START_LOADING;
                }
                else
                {
                    if(equip.HsmsPc.IsEventComplete(EmHsmsPcEvent.MAP_FILE_CREATE))
                    {
                        _stepNum = EmLD_NO.S110_START_LOADING;
                    }
                }
            }
            else if (_stepNum == EmLD_NO.S110_START_LOADING)
            {
                //여기서 부터 검사 반복성 시작(Insp Loading ~ Review End)
                if (equip.IsNoGlassMode == false)
                {
                    if (equip.Vacuum.AllVacuumOn() == false) return;
                }

                CassetteInfo cst = TransferDataMgr.GetCst(GG.Equip.TransferUnit.LowerWaferKey.CstID);
                if (cst != null)
                {
                    if (cst.LoadPortNo == 1)
                        GG.Equip.LPM1Recipe = cst.RecipeName;
                    else
                        GG.Equip.LPM2Recipe = cst.RecipeName;
                }
                IsptAddrB.YB_TTTMMode.vBit = equip.IsTTTMMode && equip.processTTTMCount +1 >= equip.CtrlSetting.Insp.TTTMMeasureCycle;
                if (equip.InspPc.StartCommand(equip, EmInspPcCommand.LOADING, GG.Equip.TransferUnit.LowerWaferKey) == false) return;

                WaferInfo wafer = TransferDataMgr.GetWafer(equip.TransferUnit.LowerWaferKey);
                TactTimeMgr.Instance.SetGlassInfo(wafer == null ? "NULL" : wafer.WaferID);
                TactTimeMgr.Instance.Set(EM_TT_LST.T020_LIFT_PIN_DOWN_START);
                _stepNum = EmLD_NO.S130_LIFT_PIN_DOWN_START;
            }
            else if (_stepNum == EmLD_NO.S130_LIFT_PIN_DOWN_START)
            {
                if (equip.LiftPin.Backward(equip) == false) return;
                _stepNum = EmLD_NO.S140_VACUUM_ON_WAIT;
            }
            else if (_stepNum == EmLD_NO.S140_VACUUM_ON_WAIT)
            {
                if (equip.LiftPin.IsBackward == true)
                {
                    if (_vacuumOnDelay)
                    {
                        if (equip.Vacuum.IsVacuumOn
                            || equip.IsNoGlassMode == true
                            )
                        {
                            _vacuumOnDelay.Stop();
                            TactTimeMgr.Instance.Set(EM_TT_LST.T030_VACUUM_ON_END, EM_TT_LST.T035_WAIT_LD_SIGNAL_START);
                            _stepNum = EmLD_NO.S145_WAFER_DOWN_COMPLETE;
                        }
                    }
                    else if (_vacuumOnDelay.IsStart == false)
                    {
                        TactTimeMgr.Instance.Set(EM_TT_LST.T020_LIFT_PIN_DOWN_END, EM_TT_LST.T030_VACUUM_ON_START);
                        if (equip.IsNoGlassMode == true || equip.IsWaferDetect == EmGlassDetect.NOT)
                            _vacuumOnDelay.Start(0, 10);
                        else
                            _vacuumOnDelay.Start(0, equip.CtrlSetting.Ctrl.VacuumOnWaitTime);
                    }
                }
            }
            else if (_stepNum == EmLD_NO.S145_WAFER_DOWN_COMPLETE)
            {
                _stepNum = EmLD_NO.S150_ALIGN_POSI_MOVE_START;
            }
            else if (_stepNum == EmLD_NO.S150_ALIGN_POSI_MOVE_START)
            {
                _stepNum = EmLD_NO.S160_INSP_LOADING_ACK_WAIT;
            }
            else if (_stepNum == EmLD_NO.S160_INSP_LOADING_ACK_WAIT)
            {
                if (equip.InspPc.IsCommandAck(equip, EmInspPcCommand.LOADING) == true)
                {
                    _InspCompleteEvtTimeover.Start(0, equip.CtrlSetting.Insp.InspectionCompleteEventTimeout);
                    _stepNum = EmLD_NO.S170_INSP_LOADING_COMPLETE_WAIT;
                }
            }
            else if (_stepNum == EmLD_NO.S170_INSP_LOADING_COMPLETE_WAIT)
            {
                if(_InspCompleteEvtTimeover)
                {
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0360_INSP_PC_LOADING_COMPLETE_HS_TIMEOVER);
                }
                else if (equip.InspPc.IsEventComplete(EmInspPcEvent.LOADING_COMPLETE) == true)
                {
                    _InspCompleteEvtTimeover.Stop();
                    if (IsptAddrB.XB_NoRecipe.vBit)
                    {
                        equip.IsPause = true;
                        equip.Efem.ChangeMode(EmEfemRunMode.Pause);
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0318_INSPECTION_ALARM_SIGNAL_NO_RECIPE);
                        CheckMgr.AddCheckMsg(true, "검사기에 레시피가 없습니다. 생성 후 Pause해제하세요");
                        _stepNum = EmLD_NO.S005_PAUSE_OFF_WAIT;
                        return;
                    }
                    CassetteInfo cst = TransferDataMgr.GetCst(GG.Equip.TransferUnit.LowerWaferKey.CstID);                    
                    Logger.Log.AppendLine(LogLevel.Warning, "로딩 Complete 신호 확인 (레시피:{0})", cst == null ? "NULL" : cst.RecipeName);
                    TactTimeMgr.Instance.Set(EM_TT_LST.T035_WAIT_LD_SIGNAL_END, EM_TT_LST.T040_ALIGN_POS_MOVE_START);

                    if (IsptAddrW.XF_AlignStartX.vFloat < 0 || GG.TestMode == true)
                    {
                        _xTargetPos = equip.StageX.Setting.LstServoPosiInfo[StageXServo.AlignPos].Position;
                        Logger.Log.AppendLine(LogLevel.Warning, "StageX Align위치 제어지정위치 이동 {0}", _xTargetPos);
                    }
                    else
                        _xTargetPos = IsptAddrW.XF_AlignStartX.vFloat;

                    if (IsptAddrW.XF_AlignStartY.vFloat < 0 || GG.TestMode == true)
                    {
                        _yTargetPos = equip.StageY.Setting.LstServoPosiInfo[StageYServo.AlignPos].Position;
                        Logger.Log.AppendLine(LogLevel.Warning, "StageY Align위치 제어지정위치 이동 {0}", _yTargetPos);
                    }
                    else
                        _yTargetPos = IsptAddrW.XF_AlignStartY.vFloat;

                    Logger.Log.AppendLine(LogLevel.NoLog, "검사 - 제어 Align Start위치 xy {0},{1}", _xTargetPos, _yTargetPos);

                    // PreAligner 백보정 시
                    //if (equip.StageX.MovePosition(equip, StageXServo.AlignPos, _xTargetPos) == false) return;
                    //if (equip.StageY.MovePosition(equip, StageYServo.AlignPos, _yTargetPos) == false) return;
                    //if (equip.Theta.MovePosition(equip, ThetaServo.AlignPos) == false) return;

                    // PreAlign 보정값만 넘길 떄 사용
                    _tTargetPos = 0;
                    try
                    {
                        WaferInfo w = TransferDataMgr.GetWafer(GG.Equip.TransferUnit.LowerWaferKey);
                        //_xTargetPos += -(float)w.CaliX / 1000; jsy::190729협의로 xy는 검사에서 계산해서 보내주는 것으로.
                        //_yTargetPos += -(float)w.CaliY / 1000;
                        _tTargetPos += (float)w.OffsetT; //-(float)w.OffsetT;
                        Logger.Log.AppendLine(LogLevel.NoLog, "ALIGNER 노치 차이 값 xyt {0},{1},{2}", w.OffsetX, w.OffsetY, w.OffsetT);
                    }
                    catch (Exception ex)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0660_NO_WAFER_INFO);
                        Logger.Log.AppendLine(LogLevel.NoLog, "ALIGNER 노치 차이 값 xyt ERROR");
                    }
                    Logger.Log.AppendLine(LogLevel.NoLog, "ALIGNER 노치 차이 값 적용 최종 이동 위치 xyt {0},{1},{2}",
                        _xTargetPos, _yTargetPos, equip.Theta.Setting.LstServoPosiInfo[ThetaServo.AlignPos].Position + _tTargetPos);

                    if (equip.StageX.MovePosition(equip, StageXServo.AlignPos, _xTargetPos) == false) return;
                    if (equip.StageY.MovePosition(equip, StageYServo.AlignPos, _yTargetPos) == false) return;
                    if (equip.Theta.MoveDefinedPositionOffset(equip, ThetaServo.AlignPos, _tTargetPos) == false) return;

                    _stepNum = EmLD_NO.S180_ALIGN_POS_MOVE_COMPLETE_WAIT;
                }
            }
            else if (_stepNum == EmLD_NO.S180_ALIGN_POS_MOVE_COMPLETE_WAIT)
            {
                if (equip.StageX.IsMoveOnPosition(_xTargetPos) == true
                    && equip.StageY.IsMoveOnPosition(_yTargetPos) == true
                    //백보정시 사용&& equip.Theta.IsMoveOnPosition(ThetaServo.AlignPos) == true
                    /* PreAlign 보정값만 넘길 떄 사용*/&& equip.Theta.IsMoveOnPosition(equip.Theta.Setting.LstServoPosiInfo[ThetaServo.AlignPos].Position + _tTargetPos) == true
                    )
                {
                    TactTimeMgr.Instance.Set(EM_TT_LST.T040_ALIGN_POS_MOVE_END, EM_TT_LST.T050_ALIGN_START);
                    _stepNum = EmLD_NO.S185_ALIGN_START;
                }
            }
            else if (_stepNum == EmLD_NO.S185_ALIGN_START)
            {                
                if (equip.InspPc.StartCommand(equip, EmInspPcCommand.ALIGN_START, 0) == false) return;
                _tmrThetaAlignTimeOver.Start(0, equip.CtrlSetting.Insp.AlignOvertime);
                _stepNum = EmLD_NO.S200_ALIGN_COMPLETE_ACK_WAIT;
            }
            else if (_stepNum == EmLD_NO.S200_ALIGN_COMPLETE_ACK_WAIT)
            {
                if (equip.InspPc.IsCommandAck(equip, EmInspPcCommand.ALIGN_START) == true
                    && equip.InspPc.IsEventComplete(EmInspPcEvent.ALIGN_COMPLETE) == true)
                {
                    TactTimeMgr.Instance.Set(EM_TT_LST.T050_ALIGN_END, EM_TT_LST.T055_TTTM_START);
                    _stepNum = EmLD_NO.S210_CHECK_TTTM_MODE;
                }
                else if (_tmrThetaAlignTimeOver == true)
                {                    
                    if (_thetaAlignCurTry < equip.CtrlSetting.Insp.AlignTryCount)
                    {
                        Logger.Log.AppendLine(LogLevel.Info, "AVI ALIGN 재시도 {0}/{1}", _thetaAlignCurTry + 1, equip.CtrlSetting.Insp.AlignTryCount);
                        _stepNum = EmLD_NO.S185_ALIGN_START;
                    }
                    else
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0331_INSP_ALIGN_TIMEOVER);
                        _stepNum = EmLD_NO.S210_CHECK_TTTM_MODE;
                    }
                    _thetaAlignCurTry++;
                }
            }
            else if (_stepNum == EmLD_NO.S210_CHECK_TTTM_MODE)
            {
                if (equip.IsTTTMMode && (equip.processTTTMCount + 1 >= equip.CtrlSetting.Insp.TTTMMeasureCycle))
                {
                    Logger.Log.AppendLine(LogLevel.AllLog, "로딩 완료, TTTM 측정 진행");

                    _isComplete = true;
                    _stepNum = EmLD_NO.S000_LOADING_WAIT;
                }
                else
                {
                    TactTimeMgr.Instance.Set(EM_TT_LST.T055_TTTM_END, EM_TT_LST.T060_SCAN_START_POS_START);
                    _stepNum = EmLD_NO.S220_SCAN_READY_POSI_MOVE_START;
                }
            }
            else if (_stepNum == EmLD_NO.S220_SCAN_READY_POSI_MOVE_START)
            {
                if (IsptAddrW.XF_InspectionStartX.vFloat < 0 || GG.TestMode == true || GG.InspTestMode)
                {
                    _xTargetPos = equip.StageX.Setting.LstServoPosiInfo[StageXServo.InspReadyPos].Position;
                    Logger.Log.AppendLine(LogLevel.Warning, "StageX InspReady위치 제어지정위치 이동 {0}", _xTargetPos);
                }
                else
                    _xTargetPos = IsptAddrW.XF_InspectionStartX.vFloat;

                if (IsptAddrW.XF_InspectionStartY.vFloat < 0 || GG.TestMode == true || GG.InspTestMode)
                {
                    _yTargetPos = equip.StageY.Setting.LstServoPosiInfo[StageYServo.InspReadyPos].Position;
                    Logger.Log.AppendLine(LogLevel.Warning, "StageY InspReady위치 제어지정위치 이동 {0}", _yTargetPos);
                }
                else
                    _yTargetPos = IsptAddrW.XF_InspectionStartY.vFloat;

                if (equip.StageX.MovePosition(equip, StageXServo.InspReadyPos, _xTargetPos) == false) return;
                if (equip.StageY.MovePosition(equip, StageYServo.InspReadyPos, _yTargetPos) == false) return;
                _stepNum = EmLD_NO.S230_LOADING_COMPLETE_WAIT;
            }
            else if (_stepNum == EmLD_NO.S230_LOADING_COMPLETE_WAIT)
            {
                if (equip.StageX.IsMoveOnPosition(_xTargetPos) == true
                     && equip.StageY.IsMoveOnPosition(_yTargetPos) == true
                     )
                {
                    _stepNum = EmLD_NO.S240_LOADING_COMPLETE;
                }
            }
            else if (_stepNum == EmLD_NO.S240_LOADING_COMPLETE)
            {
                TactTimeMgr.Instance.Set(EM_TT_LST.T060_SCAN_START_POS_END);
                Logger.Log.AppendLine(LogLevel.AllLog, "{0} 로딩 완료", equip.CurInspGlass);

                _isComplete = true;
                _stepNum = EmLD_NO.S000_LOADING_WAIT;
            }
        }
        //메소드 - 프로세스 시작 / 즉시 정지
        public bool StepStart(Equipment equip)
        {
            if (_stepNum != EmLD_NO.S000_LOADING_WAIT)
            {
                InterLockMgr.AddInterLock("인터락<실행중>\n(LOADING STEP 진행중 시작 명령이 들어왔습니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "LOADING STEP 진행중 시작 명령이 들어옴.");
                equip.IsInterlock = true;

                return false;
            }
            _passCheckGlassType = false;
            _thetaAlignCurTry = 0;
            equip.OverDefectReinspCurrentCount = 0;
            _isComplete = false;
            _stepNum = EmLD_NO.S010_LOADING_START;

            return true;
        }

        public bool ReloadingReinspection(Equipment equip)
        {
            if (_stepNum != EmLD_NO.S000_LOADING_WAIT)
            {
                InterLockMgr.AddInterLock("LOADING STEP 진행중 재검사 시작 명령이 들어옴.");
                Logger.Log.AppendLine(LogLevel.Warning, "LOADING STEP 진행중 재검사 시작 명령이 들어옴.");
                equip.IsInterlock = true;

                return false;
            }
            if (equip.IsReserveReInsp != EmReserveReInsp.START)
            {
                InterLockMgr.AddInterLock("시퀀스 이상! 재검사 시작 전 LOADING STEP 진행");
                Logger.Log.AppendLine(LogLevel.Warning, "시퀀스 이상! 재검사 시작 전 LOADING STEP 진행");
                equip.IsInterlock = true;
            }
            equip.IsReserveReInsp = EmReserveReInsp.RESERVE;
            _thetaAlignCurTry = 0;
            equip.OverDefectReinspCurrentCount = 0;
            _isComplete = false;
            _stepNum = EmLD_NO.S020_SERVO_GO_LD_POSI_START;

            return true;
        }

        public bool StepStop(Equipment equip)
        {
            _isComplete = false;
            _stepNum = EmLD_NO.S000_LOADING_WAIT;

            return true;
        }

        //메소드 - 완료 확인 
        public bool IsStepComplete()
        {
            return _isComplete && _stepNum == 0;
        }

        public bool IsManualLoadingWait {  get { return _stepNum == EmLD_NO.S061_MANUAL_LOADING_WAIT; } }
    }
}