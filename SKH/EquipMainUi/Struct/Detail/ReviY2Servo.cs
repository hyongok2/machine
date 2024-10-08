using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace EquipMainUi.Struct.Detail
{
    public class ReviY2Servo : ServoMotorUmac
    {
    //    public static string PATH_SETTING = Path.Combine(Application.StartupPath, "Setting", "ReviYServo.ini");
        public static string PATH_SETTING = Path.Combine(Application.StartupPath, "Setting", "ReviY_UnderServo.ini");

        public static int RevY2LoadingPosiNo { get; set; }
        public static int RevY2ReviewWaitPosiNo { get; set; }
        
        public ReviY2Servo(string name) :
            base(name)
        {
            SoftMinusLimit = 0;
            SoftPlusLimit = 1530;
            SoftSpeedLimit = 701;
            SoftJogSpeedLimit = 100;
            EnableGripJogSpeedLimit = 50;

            RevY2LoadingPosiNo = 1;
            RevY2ReviewWaitPosiNo = 2;
            
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
                InterLockMgr.AddInterLock("LIFT PIN 다운(홈) 위치가 아님. REV Y 이동 금지");
                Logger.Log.AppendLine(LogLevel.Warning, "LIFT PIN 다운(홈) 위치가 아님. REV Y 이동 금지");
                equip.IsInterLock = true;
                return true;
            }
            if (equip.Centering.IsCenteringBackward(equip) == false)
            {
                InterLockMgr.AddInterLock("센터링 전진 중. REV Y 이동 금지");
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
