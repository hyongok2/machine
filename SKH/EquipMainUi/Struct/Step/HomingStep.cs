using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Struct.Detail;
using Dit.Framework.PLC;

namespace EquipMainUi.Struct.Step
{
    public enum EmHM_NO
    {
        S000_STEP_WAIT,
        S010_HOME_START,
        S015_CHECK_CENTERING_BACKWARD,
        S030_CHECK_GLASS,
        S020_CENTERING_BACKWARD_WAIT,
        S040_EXIT_GLASS,
        S050_VACUUM_ON_WAIT,
        S060_SERVO_HOME_START,
        S070_SERVO_HOME_WAIT,        
        S100_STAGE_Y1_Y2_HOME_START,
        S110_STAGE_Y1_Y2_HOME_WAIT,
        S080_HOME_COMPLETE,
        S011_ROBOT_ARM_OFF_CHECK,
        S065_Y_HOME_WAIT,
    }
    public class HomingStep : StepBase
    {
        public EmHM_NO StepNum { get { return _stepNum; } }
        //Homing Step 로직 처리. 
        private EmHM_NO _stepNum = EmHM_NO.S000_STEP_WAIT;
        private EmHM_NO _stepNumOld = EmHM_NO.S010_HOME_START;
        private bool _isComplete;
        private PlcTimerEx _vacuumOnDelay = new PlcTimerEx("Vacuum On Wait Delay");

        //메소드 - 스템 시퀀스...
        public override void LogicWorking(Equipment equip)
        {
            if (equip.IsImmediatStop && equip.IsHomePositioning == true)
            {
                equip.IsHomePositioning = false;                
                _stepNum = EmHM_NO.S000_STEP_WAIT;
                return;
            }

            if (_stepNum != _stepNumOld)
            {
                Logger.Log.AppendLine(LogLevel.AllLog, "★[{0,-20}] = {1}", "HOME_STEP", _stepNum.ToString());
                _stepNumOld = _stepNum;
            }

            if (_stepNum == EmHM_NO.S000_STEP_WAIT)
            {
                if (equip.IsHomePositioning == true)
                    equip.IsHomePositioning = false;
            }
            else if (_stepNum == EmHM_NO.S010_HOME_START)
            {
                _stepNum = EmHM_NO.S011_ROBOT_ARM_OFF_CHECK;
            }
            else if (_stepNum == EmHM_NO.S011_ROBOT_ARM_OFF_CHECK)
            {
                if (equip.RobotArmDefect.IsOn == false)
                {
                    _stepNum = EmHM_NO.S015_CHECK_CENTERING_BACKWARD;
                }
            }
            else if (_stepNum == EmHM_NO.S015_CHECK_CENTERING_BACKWARD)
            {
                Logger.Log.AppendLine(LogLevel.Info, "Home Position 이동 시작");
                equip.IsHomePositioning = true;

                if (equip.Centering.CenteringBackward() == false) return;

                _stepNum = EmHM_NO.S020_CENTERING_BACKWARD_WAIT;
            }
            else if (_stepNum == EmHM_NO.S020_CENTERING_BACKWARD_WAIT)
            {
                if (equip.Centering.IsCenteringBackward(equip) == true)
                {
                    if (equip.LiftPin.Backward(equip) == false) return;
                    _stepNum = EmHM_NO.S030_CHECK_GLASS;
                }
            }
            else if (_stepNum == EmHM_NO.S030_CHECK_GLASS)
            {
                if (equip.IsWaferDetect != EmGlassDetect.NOT)
                    Logger.Log.AppendLine(LogLevel.Info, "WAFER 감지됨");
                else
                    Logger.Log.AppendLine(LogLevel.Info, "WAFER 감지 안됨");
                _stepNum = EmHM_NO.S040_EXIT_GLASS;
            }
            else if (_stepNum == EmHM_NO.S040_EXIT_GLASS)
            {
                if (equip.LiftPin.IsForward != true)
                {
                    if (equip.IsNoGlassMode == true || equip.IsWaferDetect == EmGlassDetect.NOT)
                    {

                    }
                    else
                    {
                        if (equip.Vacuum.AllVacuumOn() == false) return;
                    }
                    _stepNum = EmHM_NO.S050_VACUUM_ON_WAIT;
                }
            }
            else if (_stepNum == EmHM_NO.S050_VACUUM_ON_WAIT)
            {
                if (equip.LiftPin.IsBackward == true)
                {
                    if (_vacuumOnDelay)
                    {
                        if ((equip.IsWaferDetect != EmGlassDetect.NOT ? equip.Vacuum.IsVacuumOn : true)
                            || equip.IsNoGlassMode == true
                            )
                        {
                            _vacuumOnDelay.Stop();
                            _stepNum = EmHM_NO.S060_SERVO_HOME_START;
                        }
                    }
                    else if (_vacuumOnDelay.IsStart == false)
                    {
                        if (equip.IsNoGlassMode == true || equip.IsWaferDetect == EmGlassDetect.NOT)
                            _vacuumOnDelay.Start(0, 10);
                        else
                            _vacuumOnDelay.Start(0, equip.CtrlSetting.Ctrl.VacuumOnWaitTime);
                    }
                }
            }
            else if (_stepNum == EmHM_NO.S060_SERVO_HOME_START)
            {
                if (equip.StageY.GoHomeOrPositionOne(equip) == false) return;
                _stepNum = EmHM_NO.S065_Y_HOME_WAIT;
            }
            else if (_stepNum == EmHM_NO.S065_Y_HOME_WAIT)
            {
                if (equip.StageY.IsHomeOnPosition() && equip.StageY.IsMovingStep() == false) // robot detect 간섭때문에 추가. 추후 이부분만 주석처리하면 됨
                {
                    if (equip.StageX.GoHomeOrPositionOne(equip) == false) return;
                    if (equip.Theta.GoHomeOrPositionOne(equip) == false) return;
                    _stepNum = EmHM_NO.S070_SERVO_HOME_WAIT;
                }
            }
            else if (_stepNum == EmHM_NO.S070_SERVO_HOME_WAIT)
            {
                if (equip.StageX.IsHomeOnPosition() && equip.StageX.IsMovingStep() == false &&
                    equip.StageY.IsHomeOnPosition() && equip.StageY.IsMovingStep() == false &&
                    equip.Theta.IsHomeOnPosition() && equip.Theta.IsMovingStep() == false
                    )
                {
                    _stepNum = EmHM_NO.S080_HOME_COMPLETE;
                }
            }
            else if (_stepNum == EmHM_NO.S080_HOME_COMPLETE)
            {
                if (equip.IsHomePosition == false)
                {
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "<Interlock> 未能满足HOME 条件" : "<Interlock> HOME 조건 미충족",
                      GG.boChinaLanguage ? "每个 Motor Home Position, Lift Pin Down Position, Vacuum On(有Glass的情况),需确认 Centering 后退(後) Position是否正确" : string.Format("각 모터 홈위치, 리프트핀 하강위치, Vacuum On(글라스있는경우), Centering 후진 위치가 맞는지 확인 필요"));
                }
                else
                    Logger.Log.AppendLine(LogLevel.Info, "Home Position 이동 완료");
                equip.PMac.StartReset(equip);
                equip.IsHomePositioning = false;
                _isComplete = true;
                if (equip.IsWaferDetect != EmGlassDetect.NOT)
                    equip.IsForcedComeback = true;
                _stepNum = EmHM_NO.S000_STEP_WAIT;
            }
        }

        //메소드 - 프로세스 시작 / 즉시 정지
        public bool StepStart(Equipment equip)
        {
            if (_stepNum != EmHM_NO.S000_STEP_WAIT)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<执行中>\n(HOME STEP 进行中收到开始命令.)" : ",인터락<실행중>\n(HOME STEP 진행중 시작 명령이 들어왔습니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "HOME STEP 진행중 시작 명령이 들어옴.");
                equip.IsInterlock = true;
                return false;
            }
            _isComplete = false;
            _stepNum = EmHM_NO.S010_HOME_START;

            return true;
        }
        public bool StepStop(Equipment equip)
        {
            _isComplete = false;
            _stepNum = EmHM_NO.S000_STEP_WAIT;

            return true;
        }

        //메소드 - 시작 확인 
        public bool IsStepComplete()
        {
            return _isComplete && _stepNum == 0;
        }

    }
}
