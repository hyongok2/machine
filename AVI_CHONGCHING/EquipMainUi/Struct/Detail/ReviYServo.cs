using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace EquipMainUi.Struct.Detail
{
    public class ReviYServo : ServoMotorUmac
    {
        public static string PATH_SETTING = Path.Combine(Application.StartupPath, "Setting", "ReviYServo.ini");
   //     public static string PATH_SETTING_2 = Path.Combine(Application.StartupPath, "Setting", "ReviY_UnderServo.ini");

        public static int RevYLoadingPosiNo { get; set; }
        public static int RevYReviewWaitPosiNo { get; set; }
        
        public ReviYServo(string name) :
            base(name)
        {
            SoftMinusLimit = 0;
            SoftPlusLimit = 1530;
            SoftSpeedLimit = 701;
            SoftJogSpeedLimit = 100;
            EnableGripJogSpeedLimit = 50;

            RevYLoadingPosiNo = 1;
            RevYReviewWaitPosiNo = 2;

            base.MoveActionName[0] = "원점 위치";
            base.MoveActionName[1] = "LOADING 위치";
            base.MoveActionName[2] = "REVIEW 대기 위치";
        }
        protected override bool CheckStartMoveInterLock(Equipment equip, MoveCommand cmd)
        {
            if (equip.IsUseInterLockOff) return false;
            if (equip.LiftPin.IsMoveOnPosition(LiftPinServo.LiftPinHomeLowSpeed) == false &&
              equip.LiftPin.IsMoveOnPosition(LiftPinServo.LiftPinDownHiSpeed) == false &&
              equip.LiftPin.IsMoveOnPosition(LiftPinServo.LiftPinDownLowSpeed) == false)
            {
                InterLockMgr.AddInterLock("인터락<LIFT PIN>\n(REVIEW Y축 이동은 LIFT PIN 다운(홈) 위치에서만 가능합니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "LIFT PIN 다운(홈) 위치가 아님. REV Y 이동 금지");
                equip.IsInterLock = true;
                return true;
            }
            if (equip.Centering.IsCenteringBackward(equip) == false)
            {
                InterLockMgr.AddInterLock("인터락<CENTERING>\n(REVIEW Y축 이동은 센터링 후진 위치에서만 가능합니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "센터링 전진 중. REV Y 이동 금지");
                equip.IsInterLock = true;
                return true;
            }

            return false;
        }
        protected override bool CheckMovingInterLock(Equipment equip, MoveCommand cmd, ref int stepMove)
        {
            return false;
        }
    }
}
