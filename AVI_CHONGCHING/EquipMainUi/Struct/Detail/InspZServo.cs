using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace EquipMainUi.Struct.Detail
{
    public class InspZServo : ServoMotorUmac
    {
        public static string PATH_SETTING_Z1 = Path.Combine(Application.StartupPath, "Setting", "InspZ1Servo.ini");
        public static string PATH_SETTING_Z2 = Path.Combine(Application.StartupPath, "Setting", "InspZ2Servo.ini");
        public static string PATH_SETTING_Z3 = Path.Combine(Application.StartupPath, "Setting", "InspZ3Servo.ini");
        public static int InspZLoadingPosiNo { get; set; }
        public static int InspZPosi1No { get; set; }
        public static int InspZPosi2No { get; set; }
        public static int InspZPosi3No { get; set; }
     
        public InspZServo(string name) : 
            base(name)
        {
            SoftMinusLimit = 0;
            SoftPlusLimit = 17;
            SoftSpeedLimit = 7;
            SoftJogSpeedLimit = 5;

            InspZLoadingPosiNo = 1;
            InspZPosi1No = 2;
            InspZPosi2No = 3;
            InspZPosi3No = 4;

            base.MoveActionName[0] = "원점 위치";
            base.MoveActionName[1] = "LOADING 위치";
            base.MoveActionName[2] = "Scan1 위치";
            base.MoveActionName[3] = "Scan2 위치";
            base.MoveActionName[4] = "Scan3 위치";
        }
        protected override bool CheckStartMoveInterLock(Equipment equip, MoveCommand cmd)
        {
            if (equip.IsUseInterLockOff) return false; 
            return false;
        }
        protected override bool CheckMovingInterLock(Equipment equip, MoveCommand cmd, ref int stepMove)
        {
            return false;
        }
    }
}
