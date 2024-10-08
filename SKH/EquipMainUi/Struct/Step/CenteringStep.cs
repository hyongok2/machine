using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using EquipMainUi.Struct.BaseUnit;
using EquipMainUi.Monitor;
using EquipMainUi.Log;

namespace EquipMainUi.Struct.Detail
{
    //인터락이 필요하여 Cylinder 글래스를 변경..
    public class CenteringStep
    {
        public Equipment Equip { get; set; }

        private DateTime ForwardStartTime = new DateTime(0);
        private DateTime BackwardStartTime = new DateTime(0);
        public PlcTimerEx AssiCenteringDelay = new PlcTimerEx("AssistCentering Delay");
        public int ForwardTime { get; set; }
        public int BackwardTime { get; set; }

        ////기준명 센터링. 

        //센터링 시퀀스 전체 진행...
        public bool CenteringFoward()
        {
            /// 인터락 추가 필요            
            Logger.Log.AppendLine(LogLevel.Info, "센터링 전진");
            StepCenteringForward = 10;
            return true;
        }
        public bool CenteringBackward()
        {
            /// 인터락 추가 필요 
            Logger.Log.AppendLine(LogLevel.Info, "센터링 후진");
            StepCenteringBackward = 10;
            return true;
        }
        public bool CenteringFowardStop(Equipment equip)
        {
            /// 인터락 추가 필요             
            StepCenteringForward = 0;
            return true;
        }
        public bool CenteringBackwardStop(Equipment equip)
        {
            /// 인터락 추가 필요 
            StepCenteringBackward = 0;
            return true;
        }
        public bool IsCenteringBackward(Equipment equip)
        {
            return equip.StandCentering.IsBackward;
        }
        public bool IsCenteringForward(Equipment equip)
        {
            return equip.StandCentering.IsForward;
        }
        public void LogicWorking(Equipment equip)
        {
            equip.StandCentering.LogicWorking(equip);
            LogCenteringBackward(equip);
            LogCenteringForward(equip);
        }

        public int StepCenteringForward = 0;
        private PlcTimer TmrCenteringForwarding = new PlcTimer();
        public void LogCenteringForward(Equipment equip)
        {
            if (equip.IsImmediatStop)
            {
                StepCenteringForward = 0;
                equip.IsCenteringStepping = false;
            };

            if (StepCenteringForward == 0)
            {

            }
            else if (StepCenteringForward == 10)
            {
                ForwardStartTime = DateTime.Now;

                if (equip.StandCentering.Forward(equip) == false) return;

                StepCenteringForward = 20;
            }
            else if (StepCenteringForward == 20)
            {
                StepCenteringForward = 30;
            }
            else if (StepCenteringForward == 30)
            {
                if (IsCenteringForward(equip) == true)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "센터링 전진 종료");
                    StepCenteringForward = 0;
                    equip.IsCenteringStepping = false;
                }
            }
        }

        public int StepCenteringBackward = 0;
        private PlcTimer TmrCenteringBackwarding = new PlcTimer();
        public void LogCenteringBackward(Equipment equip)
        {
            if (equip.IsImmediatStop)
            {
                if (equip.IsHomePositioning == false && equip.EquipRunMode != EmEquipRunMode.Auto)
                {
                    StepCenteringBackward = 0;
                    equip.IsCenteringStepping = false;
                }
                return;
            }
            if (StepCenteringBackward == 0)
            {

            }
            else if (StepCenteringBackward == 10)
            {
                ForwardStartTime = DateTime.Now;
                StepCenteringBackward = 20;
            }
            else if (StepCenteringBackward == 20)
            {
                if (equip.StandCentering.Backward(equip) == false) return;
                StepCenteringBackward = 30;
            }
            else if (StepCenteringBackward == 30)
            {
                if (IsCenteringBackward(equip) == true)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "센터링 후진 종료");
                    StepCenteringBackward = 0;
                    equip.IsCenteringStepping = false;
                }
            }
        }
    }
}
