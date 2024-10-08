using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Setting;

namespace EquipMainUi.Struct.Detail.Servo
{
    public static class ServoPositionMgr
    {
        public static bool LoadPosition(Equipment equip, ref int errCnt)
        {
            equip.StageX.Setting.Section = equip.StageX.Name;
            equip.StageX.Setting.LstServoPosiInfo = new Setting.ServoPosiInfo[]
            {
                new ServoPosiInfo() {No = 0, Name = "원점 위치", Position = 0, Speed = 0, Accel = 300f, Servo = equip.StageX},
                new ServoPosiInfo() {No = 1, Name = "로딩 위치", Position = 0, Speed = 0, Accel = 300f, Servo = equip.StageX},
                new ServoPosiInfo() {No = 2, Name = "Align 위치", Position = 0, Speed = 0, Accel = 300f, Servo = equip.StageX},
                new ServoPosiInfo() {No = 3, Name = "검사 Ready 위치", Position = 0, Speed = 0, Accel = 300f, Servo = equip.StageX},
                new ServoPosiInfo() {No = 4, Name = "리뷰 Ready 위치", Position = 0, Speed = 0, Accel = 300f, Servo = equip.StageX},
            };
            if (equip.StageX.Setting.Load() == false) errCnt++;

            equip.StageY.Setting.Section = equip.StageY.Name;
            equip.StageY.Setting.LstServoPosiInfo = new Setting.ServoPosiInfo[]
            {
                new ServoPosiInfo() {No = 0, Name = "원점 위치", Position = 0, Speed = 0, Accel = 300f, Servo = equip.StageY},
                new ServoPosiInfo() {No = 1, Name = "로딩 위치", Position = 0, Speed = 0, Accel = 300f, Servo = equip.StageY},
                new ServoPosiInfo() {No = 2, Name = "Align 위치", Position = 0, Speed = 0, Accel = 300f, Servo = equip.StageY},
                new ServoPosiInfo() {No = 3, Name = "검사 Ready 위치", Position = 0, Speed = 0, Accel = 300f, Servo = equip.StageY},
                new ServoPosiInfo() {No = 4, Name = "리뷰 Ready 위치", Position = 0, Speed = 0, Accel = 300f, Servo = equip.StageY},
            };
            if (equip.StageY.Setting.Load() == false) errCnt++;

            equip.Theta.Setting.Section = equip.Theta.Name;
            equip.Theta.Setting.LstServoPosiInfo = new Setting.ServoPosiInfo[]
            {
                new ServoPosiInfo() {No = 0, Name = "원점 위치", Position = 0, Speed = 0, Accel = 300f, Servo = equip.Theta},
                new ServoPosiInfo() {No = 1, Name = "로딩 위치", Position = 0, Speed = 0, Accel = 300f, Servo = equip.Theta},
                new ServoPosiInfo() {No = 2, Name = "Align 위치", Position = 0, Speed = 0, Accel = 300f, Servo = equip.Theta},
                new ServoPosiInfo() {No = 3, Name = "검사 Ready 위치", Position = 0, Speed = 0, Accel = 300f, Servo = equip.Theta},
                new ServoPosiInfo() {No = 4, Name = "리뷰 Ready 위치", Position = 0, Speed = 0, Accel = 300f, Servo = equip.Theta},
            };
            if (equip.Theta.Setting.Load() == false) errCnt++;

            #region Step Motor
            if(GG.IsDitPreAligner)
            {
                equip.AlignerX.Setting.Section = equip.AlignerX.Name;
                equip.AlignerX.Setting.LstServoPosiInfo = new Setting.ServoPosiInfo[]
                {
                    new ServoPosiInfo() {No = 0, Name = "원점 위치", Position = 0, Speed = 0, Accel = 300f, StepMotor = equip.AlignerX},
                    new ServoPosiInfo() {No = 1, Name = "로딩 위치", Position = 0, Speed = 0, Accel = 300f, StepMotor = equip.AlignerX},
                    new ServoPosiInfo() {No = 2, Name = "언로딩 위치", Position = 0, Speed = 0, Accel = 300f, StepMotor = equip.AlignerX},
                };
                if (equip.AlignerX.Setting.Load() == false) errCnt++;

                equip.AlignerY.Setting.Section = equip.AlignerY.Name;
                equip.AlignerY.Setting.LstServoPosiInfo = new Setting.ServoPosiInfo[]
                {
                    new ServoPosiInfo() {No = 0, Name = "원점 위치", Position = 0, Speed = 0, Accel = 300f, StepMotor = equip.AlignerY},
                    new ServoPosiInfo() {No = 1, Name = "로딩 위치", Position = 0, Speed = 0, Accel = 300f, StepMotor = equip.AlignerY},
                    new ServoPosiInfo() {No = 2, Name = "언로딩 위치", Position = 0, Speed = 0, Accel = 300f, StepMotor = equip.AlignerY},
                };
                if (equip.AlignerY.Setting.Load() == false) errCnt++;

                equip.AlignerT.Setting.Section = equip.AlignerT.Name;
                equip.AlignerT.Setting.LstServoPosiInfo = new Setting.ServoPosiInfo[]
                {
                    new ServoPosiInfo() {No = 0, Name = "원점 위치", Position = 0, Speed = 0, Accel = 300f, StepMotor = equip.AlignerT},
                    new ServoPosiInfo() {No = 1, Name = "로딩 위치", Position = 0, Speed = 0, Accel = 300f, StepMotor = equip.AlignerT},
                    new ServoPosiInfo() {No = 2, Name = "언로딩 위치", Position = 0, Speed = 0, Accel = 300f, StepMotor = equip.AlignerT},
                };
                if (equip.AlignerT.Setting.Load() == false) errCnt++;
            }

            #endregion


            return errCnt <= 0;
        }
    }
}
