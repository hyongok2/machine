using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace EquipMainUi.Struct.Detail
{
    public class InspYServo : ServoMotorUmac
    {
        public static string PATH_SETTING = Path.Combine(Application.StartupPath, "Setting", "InspYServo.ini");
        public static int InspY1ScanPosiNo { get; set; }
        public static int InspY2ScanPosiNo { get; set; }
        public static int InspY3ScanPosiNo { get; set; }
        public static int InspY4ScanPosiNo { get; set; }
        public static int InspY5ScanPosiNo { get; set; }
        public static int InspY6ScanPosiNo { get; set; }
        public static int InspY7ScanPosiNo { get; set; }
        public static int InspY8ScanPosiNo { get; set; }
        public static int InspY9ScanPosiNo { get; set; }
        public static int InspYLoadingPosiNo { get; set; }
            

        public InspYServo(string name) :
            base(name)
        {
            SoftMinusLimit = 0;
            SoftPlusLimit = 450;
            SoftSpeedLimit = 701;
            SoftJogSpeedLimit = 100;
            EnableGripJogSpeedLimit = 50;

            InspYLoadingPosiNo = 1;
            InspY1ScanPosiNo = 2;
            InspY2ScanPosiNo = 3;
            InspY3ScanPosiNo = 4;            
            InspY4ScanPosiNo = 5;
            InspY5ScanPosiNo = 6;
            InspY6ScanPosiNo = 7;
            InspY7ScanPosiNo = 8;
            InspY8ScanPosiNo = 9;
            InspY9ScanPosiNo = 10;
            
            
            base.MoveActionName[0] = "원점 위치";
            base.MoveActionName[1] = "LOADING 위치";
            base.MoveActionName[2] = "SCAN 1 위치";
            base.MoveActionName[3] = "SCAN 2 위치";
            base.MoveActionName[4] = "SCAN 3 위치";
            base.MoveActionName[5] = "SCAN 4 위치";
            base.MoveActionName[6] = "SCAN 5 위치";
            base.MoveActionName[7] = "SCAN 6 위치";
            base.MoveActionName[8] = "SCAN 7 위치";
            base.MoveActionName[9] = "SCAN 8 위치";
            base.MoveActionName[10] = "SCAN 9 위치";
        }
        protected override bool CheckStartMoveInterLock(Equipment equip, MoveCommand cmd)
        {
            if (equip.IsUseInterLockOff) return false;

            if (equip.LiftPin.IsMoveOnPosition(LiftPinServo.LiftPinHomeLowSpeed) == false &&
                equip.LiftPin.IsMoveOnPosition(LiftPinServo.LiftPinDownHiSpeed) == false &&
                equip.LiftPin.IsMoveOnPosition(LiftPinServo.LiftPinDownLowSpeed) == false)
            {
                InterLockMgr.AddInterLock("인터락<LIFT PIN>\n(INSPECTION Y축 이동은 LIFT PIN 다운(홈) 위치에서만 가능합니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "LIFT PIN 다운(홈) 위치가 아님. INSP Y 이동 금지");
                equip.IsInterLock = true;
                return true;
            }

            if (equip.Centering.IsCenteringBackward(equip) == false)
            {
                InterLockMgr.AddInterLock("인터락<CENTERING>\n(INSPECTION Y축 이동은 센터링이 후진인 경우만 가능합니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "센터링 전진 중. INSP Y 이동 금지");
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
