using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Ini;
using System.IO;
using System.Windows.Forms;
using EquipComm.PLC;

namespace EquipMainUi.Struct.Detail
{
    public class InspXServo : ServoMotorUmac
    {
        public static string PATH_SETTING = Path.Combine(Application.StartupPath, "Setting", "InspXServo.ini");

        public static int InspXForwardScanStartPosiNo { get; set; }
        public static int InspXForwardScanEndPosiNo { get; set; }
        public static int InspXBackScanStartPosiNo { get; set; }
        public static int InspXBackScanEndPosiNo { get; set; }
        public static int InspXLoadingPosiNo { get; set; }
        public static int InspXReviewWaitPosiNo { get; set; }

      
        public InspXServo(string name) :
            base(name)
        {

            SoftMinusLimit = 0;
            SoftPlusLimit = 2930;
            SoftSpeedLimit = 701;
            SoftJogSpeedLimit = 100;
            EnableGripJogSpeedLimit = 50;

            InspXLoadingPosiNo = 1;
            InspXBackScanStartPosiNo = 2;
            InspXBackScanEndPosiNo = 3;
            InspXForwardScanStartPosiNo = 4;
            InspXForwardScanEndPosiNo = 5;
            InspXReviewWaitPosiNo = 6;

            base.MoveActionName[0] = "원점 위치";
            base.MoveActionName[1] = "LOADING 위치";
            base.MoveActionName[2] = "BACK SCAN START 위치";
            base.MoveActionName[3] = "BACK SCAN END 위치";
            base.MoveActionName[4] = "FORWARD SCAN START 위치";
            base.MoveActionName[5] = "FORWARD SCAN END 위치";
            base.MoveActionName[6] = "REVIEW 대기 위치";

        }
        protected override bool CheckStartMoveInterLock(Equipment equip, MoveCommand cmd)
        {
            if (equip.IsUseInterLockOff) return false;
            
            if (equip.LiftPin.IsMoveOnPosition(LiftPinServo.LiftPinHomeLowSpeed) == false &&                
                equip.LiftPin.IsMoveOnPosition(LiftPinServo.LiftPinDownHiSpeed) == false &&
                equip.LiftPin.IsMoveOnPosition(LiftPinServo.LiftPinDownLowSpeed) == false )
            {
                InterLockMgr.AddInterLock("인터락<LIFT PIN>\n(INSPECTION X축 이동은 LIFT PIN 다운(홈) 위치에서만 가능합니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "LIFT PIN 다운(홈) 위치가 아님. INSP X 이동 금지");
                equip.IsInterLock = true;
                return true;
            }

            if (equip.Vacuum.IsVacuumOn == false && (equip.IsGlassCheckOk == EmGlassSense.SOME || equip.IsGlassCheckOk == EmGlassSense.ALL))
            {
                InterLockMgr.AddInterLock("인터락<VACUUM>\n(INSPECTION X축 이동은 VACUUM ON인 경우만 가능합니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "VACUUM OFF 일때.INSP X 이동 금지");
                equip.IsInterLock = true;
                return true;
            }

            if (equip.Centering.IsCenteringBackward(equip) == false)
            {
                InterLockMgr.AddInterLock("인터락<CENTERING>\n(INSPECTION X축 이동은 센터링이 후진인 경우만 가능합니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "센터링 전진 중. INSP X 이동 금지");
                equip.IsInterLock = true;
                return true;
            }

            if (equip.PMac.XB_PinUpMotorInterlockDiableAck.vInt == 0)
            {
                InterLockMgr.AddInterLock("인터락<PMAC>\n(PMAC에서 LIFT PIN INTERLOCK 상태로 확인됩니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "PMAC에서 LIFT PIN INTERLOCK 상태임");
                equip.IsInterLock = true;
                return true;
            }

            return false;
        }
        protected override bool CheckMovingInterLock(Equipment equip, MoveCommand cmd, ref int stepMove)
        {
            return false;
        }
        
        protected override void OnSettingEx(Equipment equip)
        {
            YF_TriggerStartPosi.vFloat = Convert.ToSingle(Setting.TRIGER_START );
            YF_TriggerEndPosi.vFloat = Convert.ToSingle(Setting.TRIGER_END);
        }
    }
}
