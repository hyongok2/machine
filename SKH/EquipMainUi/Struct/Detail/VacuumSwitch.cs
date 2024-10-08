using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Log;

namespace EquipMainUi.Struct.Detail
{
    public class VacuumSwitch : Switch
    {
        public VacuumSwitch(bool isAContactSol = true, bool isAContactSensor = true)
            : base(isAContactSol, isAContactSensor)
        {
            TimeOutInterval = 10000; // milisecons.
            ON_TIME_OUT_ERROR = EM_AL_LST.AL_0000_NONE;
            OFF_TIME_OUT_ERROR = EM_AL_LST.AL_0000_NONE;
            MccOnOff = MccActionItem.NONE;
        }
        public override void OnOff(Equipment equip, bool isON)
        {
            if (isON == true &&
                equip.IsHomePositioning == false && equip.EquipRunMode == EmEquipRunMode.Manual && equip.IsUseInterLockOff == false)
            {
                if (equip.LiftPin.IsBackward == false && equip.LiftPin.IsBackwarding == false)
                {
                    InterLockMgr.AddInterLock("인터락<LIFTPIN>\nLiftPin 하강 위치 일 경우만 공압이 가능합니다.");
                    return;
                }
            }
            if(isON == false && equip.IsUseInterLockOff == false)
            {
                if (equip.StageX.IsMoving == true)
                {
                    InterLockMgr.AddInterLock("인터락<Stage X>\nStage X 축이 움직 일 때 Vacuum Off 불가능합니다.");
                    return;
                }
                if (equip.StageY.IsMoving == true)
                {
                    InterLockMgr.AddInterLock("인터락<Stage Y>\nStage Y 축이 움직 일 때 Vacuum Off 불가능합니다.");
                    return;
                }
                if (equip.Theta.IsMoving == true)
                {
                    InterLockMgr.AddInterLock("인터락<Theta>\nTheta 축이 움직 일 때 Vacuum Off 불가능합니다.");
                    return;
                }
            }
            base.OnOff(equip, isON);
        }
        public override void LogicWorking(Equipment equip)
        {
            UpdateTime();

            if (IsSolOnOff == true && IsOnOff == false)
            {
                if ((DateTime.Now - OnOffStartTime).TotalMilliseconds > TimeOutInterval)
                {
                    AlarmMgr.Instance.Happen(equip, ON_TIME_OUT_ERROR);
                    EquipStatusDump.LogVaccumStatus(equip);
                }
            }

            if (IsSolOnOff == false && IsOnOff == true)
            {
                if ((DateTime.Now - OnOffStartTime).TotalMilliseconds > TimeOutInterval)
                {
                    AlarmMgr.Instance.Happen(equip, OFF_TIME_OUT_ERROR);
                    EquipStatusDump.LogVaccumStatus(equip);
                }
            }
        }
    }
}
