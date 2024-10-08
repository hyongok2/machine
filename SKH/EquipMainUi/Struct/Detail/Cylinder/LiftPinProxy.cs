using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using Dit.Framework.Comm;
using EquipMainUi.Struct.BaseUnit;

namespace EquipMainUi.Struct.Detail.Cylinder
{
    public class LiftPinProxy : CustomCylinder
    {
        public LiftPinProxy(int sensorCount) :
           base("LiftPin", sensorCount)
        {

        }
                
        public override bool IsInterlock(Equipment equip, bool isBackward)
        {
            if (equip.IsUseInterLockOff) return false;

            if (true
                && GG.TestMode == false
                && equip.IsHeavyAlarm == true
                )
            {
                InterLockMgr.AddInterLock(string.Format("인터락<HEAVY ALARM>\n(HEAVY ALARM 상태입니다. {0} 동작이 불가능 합니다.)", Name));
                return true;
            }

            if (true
                && GG.TestMode == false
                && equip.IsEnableGripMiddleOn == false
                && equip.IsDoorOpen
                )
            {
                InterLockMgr.AddInterLock(string.Format("인터락<DOOR>\n(설비 상태가 DOOR OPEN 상태에서 {0} 이동이 금지됩니다.)", Name));
                Logger.Log.AppendLine(LogLevel.Warning, "설비 상태가 Door Open 상태에서 {0} 이동 금지됨", Name);
                return true;
            }

            if (isBackward == false)
            {
                if (equip.RobotArmDefect.IsOn == true || equip.IsEFEMInputArm.IsOn == true)
                {
                    string str = string.Format("{0} {1} 상태, 리프트핀 상승 불가", equip.RobotArmDefect.IsOn ? "로봇 암 감지" : "", equip.IsEFEMInputArm.IsOn ? "EFEM 로봇투입 신호 ON" : "");
                    if (equip.IsHomePositioning || equip.EquipRunMode == EmEquipRunMode.Auto)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0290_ROBOT_ARM_DETECT_ERROR);
                    }
                    else
                        InterLockMgr.AddInterLock("인터락<LIFT PIN>\n" + str);

                    equip.IsInterlock = true;
                    Logger.Log.AppendLine(LogLevel.Warning, str);
                    return true;
                }

                if (equip.Vacuum.IsVacuumOn == true)
                {
                    string str = "Vacuum ON 상태, 리프트핀 상승 불가";
                    if (equip.IsHomePositioning || equip.EquipRunMode == EmEquipRunMode.Auto)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0110_VACUUM_ON_LIFTPIN_CANT_UP);
                    }
                    else
                        InterLockMgr.AddInterLock("인터락<LIFT PIN>\n" + str);

                    equip.IsInterlock = true;
                    Logger.Log.AppendLine(LogLevel.Warning, str);
                    return true;
                }

                if ((equip.StageX.IsMoveOnPosition(StageXServo.HomePosition) == false && equip.StageX.IsMoveOnPosition(StageXServo.LoadingPos) == false))
                {
                    Logger.Log.AppendLine(LogLevel.Error, string.Format("INSP X축 확인-{0}, {1}, {2}",
                        equip.StageX.IsLoadingOnPosition == true ? "TRUE" : "FALSE",
                        equip.StageX.IsMoving == false ? "TRUE" : "FALSE",
                        equip.StageX.IsStepEnd == true ? "TRUE" : "FALSE"));

                    if (equip.IsHomePositioning || equip.EquipRunMode == EmEquipRunMode.Auto)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0497_STAGE_X_LOADING_POSITION_ERROR);
                        equip.IsInterlock = true;
                    }
                    else
                    {
                        if (equip.IsCenteringStepping)
                            equip.IsInterlock = true;
                        InterLockMgr.AddInterLock(string.Format("인터락<INSPECTION X축>\n {0} 이동은 INSPECTION X축 로딩 위치에서만 가능합니다.)", this.Name));
                    }

                    Logger.Log.AppendLine(LogLevel.Warning, string.Format("INSP X축 로딩 위치에서만 {0} 조작 가능", this.Name));
                    return true;
                }
                if ((equip.StageY.IsMoveOnPosition(StageXServo.HomePosition) == false && equip.StageY.IsMoveOnPosition(StageXServo.LoadingPos) == false))
                {
                    Logger.Log.AppendLine(LogLevel.Error, string.Format("INSP Y축 확인-{0}, {1}, {2}",
                        equip.StageY.XF_CurrMotorPosition.vFloat,
                        equip.StageY.IsMoving == false ? "TRUE" : "FALSE",
                        equip.StageY.IsStepEnd == true ? "TRUE" : "FALSE"));

                    if (equip.IsHomePositioning || equip.EquipRunMode == EmEquipRunMode.Auto)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0515_STAGE_Y_LOADING_POSITION_ERROR);
                        equip.IsInterlock = true;
                    }
                    else
                    {
                        if (equip.IsCenteringStepping)
                            equip.IsInterlock = true;
                        InterLockMgr.AddInterLock(string.Format("인터락<INSPECTION Y축>\n {0} 이동은 INSPECTION Y축 로딩 위치에서만 가능합니다.)", this.Name));
                    }

                    Logger.Log.AppendLine(LogLevel.Warning, string.Format("INSP Y축 로딩 위치에서만 {0} 조작 가능", this.Name));
                    return true;
                }
                if (equip.Theta.IsMoveOnPosition(ThetaServo.LoadingPos) == false)
                {
                    Logger.Log.AppendLine(LogLevel.Error, string.Format("Theta축 확인-{0}, {1}, {2}",
                        equip.Theta.XF_CurrMotorPosition.vFloat,
                        equip.Theta.IsMoving == false ? "TRUE" : "FALSE",
                        equip.Theta.IsStepEnd == true ? "TRUE" : "FALSE"));

                    if (equip.IsHomePositioning || equip.EquipRunMode == EmEquipRunMode.Auto)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0530_THETA_LOADING_POSITION_ERROR);
                        equip.IsInterlock = true;
                    }
                    else
                    {
                        if (equip.IsCenteringStepping)
                            equip.IsInterlock = true;
                        InterLockMgr.AddInterLock(string.Format("인터락<Theta축>\n {0} 이동은 Theta축 로딩 위치에서만 가능합니다.)", this.Name));
                    }

                    Logger.Log.AppendLine(LogLevel.Warning, string.Format("Theta축 로딩 위치에서만 {0} 조작 가능", this.Name));
                    return true;
                }
            }
            else
            {
                if (equip.RobotArmDefect.IsOn == true || equip.IsEFEMInputArm.IsOn == true)
                {
                    string str = string.Format("{0} {1} 상태, 리프트핀 하강 불가", equip.RobotArmDefect.IsOn ? "로봇 암 감지" : "", equip.IsEFEMInputArm.IsOn ? "EFEM 로봇투입 신호 ON" : "");
                    if (equip.IsHomePositioning || equip.EquipRunMode == EmEquipRunMode.Auto)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0290_ROBOT_ARM_DETECT_ERROR);
                    }
                    else
                        InterLockMgr.AddInterLock("인터락<LIFT PIN>\n" + str);

                    equip.IsInterlock = true;
                    Logger.Log.AppendLine(LogLevel.Warning, str);
                    return true;
                }
            }
            return false;
        }

        public override bool Forward(Equipment equip)
        {
            if (IsInterlock(equip, false)) return false;

            if (equip.IsUseInterLockOff == false)
            {

            }
            Logger.Log.AppendLine(LogLevel.Info, "리프트핀 상승 동작 시작");
            base.Forward(equip);
            return true;
        }
        public override bool Backward(Equipment equip)
        {
            if (IsInterlock(equip, true)) return false;

            if (equip.IsUseInterLockOff == false)
            {

            }
            Logger.Log.AppendLine(LogLevel.Info, "리프트핀 하강 동작 시작");
            base.Backward(equip);
            return true;
        }
    }
}
