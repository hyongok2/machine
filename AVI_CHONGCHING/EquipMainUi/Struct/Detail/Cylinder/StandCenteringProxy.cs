using EquipMainUi.Struct.BaseUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.Detail.Cylinder
{
    public class StandCenteringProxy : CustomCylinder
    {
        public StandCenteringProxy(int sensorCount) :
            base("Stand Centering", sensorCount)
        {

        }
        public override bool IsInterlock(Equipment equip, bool isBackward)
        {
            if (base.IsInterlock(equip, isBackward) == true) return true;

            if (isBackward)
            {

            }
            else
            {
                if (GG.TestMode == false
                    && equip.IsReadyToInputArm.IsSolOnOff == true
                    && (equip.IsRobotArmDetect == true || equip.IsEFEMInputArm.IsOn == true)
                    )
                {
                    string str = GG.boChinaLanguage ? string.Format("PIO 准备状态及 {0} {1} 状态, Centering 不可以前进", equip.RobotArmDefect.IsOn ? "Robot Arm 感应" : "", equip.IsEFEMInputArm.IsOn ? "EFEM Robot 投入信号 ON" : "") : string.Format("PIO 준비 상태 및 {0} {1} 상태, 센터링 전진 불가", equip.RobotArmDefect.IsOn ? "로봇 암 감지" : "", equip.IsEFEMInputArm.IsOn ? "EFEM 로봇투입 신호 ON" : "");
                    if (equip.IsHomePositioning || equip.EquipRunMode == EmEquipRunMode.Auto)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0290_ROBOT_ARM_DETECT_ERROR);
                    }
                    else
                        InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<Centering>\n" : "인터락<CENTERING>\n" + str);

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
    
            base.Forward(equip);
            return true;
        }
        public override bool Backward(Equipment equip)
        {
            if (IsInterlock(equip, true)) return false;           

            base.Backward(equip);
            return true;
        }
    }
}
