using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using EquipMainUi.Struct.Detail;

namespace EquipMainUi.Struct.BaseUnit
{
    public class CenteringCylinder : UnitBase
    {
        public int TimeOutInterval = 5;

        public PlcAddr YB_ForwardCmd { get; set; }
        public PlcAddr YB_BackwardCmd { get; set; }

        public PlcAddr XB_ForwardCompleteP1 { get; set; }
        public PlcAddr XB_BackwardCompleteP1 { get; set; }

        public PlcAddr XB_ForwardCompleteP2 { get; set; }
        public PlcAddr XB_BackwardCompleteP2 { get; set; }

        public DateTime ForwardStartTime { get; set; }
        public DateTime BackwardStartTime { get; set; }

        public EM_AL_LST[] AlcdForwardTimeOut = new EM_AL_LST[2];
        public EM_AL_LST[] AlcdBackwardTimeOut = new EM_AL_LST[2];

        public MccActionItem MCC_FWD_ON_OFF { get; set; }
        public MccActionItem MCC_BWD_ON_OFF { get; set; }


        public string ForwardTimeP1 { get; set; }
        public string BackwardTimeP1 { get; set; }

        public string ForwardTimeP2 { get; set; }
        public string BackwardTimeP2 { get; set; }

        private DateTime _errorStartTimeP1 = DateTime.Now;
        private DateTime _errorStartTimeP2 = DateTime.Now;

        private bool _isFirstChkP1 = false;
        private bool _isFirstChkP2 = false;

        public CenteringCylinder()
        {
            AlcdForwardTimeOut[0] = EM_AL_LST.AL_0000_NONE;
            AlcdForwardTimeOut[1] = EM_AL_LST.AL_0000_NONE;

            AlcdBackwardTimeOut[0] = EM_AL_LST.AL_0000_NONE;
            AlcdBackwardTimeOut[1] = EM_AL_LST.AL_0000_NONE;

            MCC_FWD_ON_OFF = MccActionItem.NONE;
            MCC_BWD_ON_OFF = MccActionItem.NONE;
        }

        public virtual bool Forward(Equipment equip)
        {
            ForwardStartTime = DateTime.Now;
            YB_ForwardCmd.vBit = true;
            YB_BackwardCmd.vBit = false;

            equip.MccPc.SetMccAction(MCC_FWD_ON_OFF);

            return true;
        }
        public virtual bool Backward(Equipment equip)
        {
            BackwardStartTime = DateTime.Now;
            YB_ForwardCmd.vBit = false;
            YB_BackwardCmd.vBit = true;

            equip.MccPc.SetMccAction(MCC_BWD_ON_OFF);

            return true;
        }
        public bool IsForward
        {
            get
            {
                return XB_ForwardCompleteP1.vBit == true && XB_ForwardCompleteP2.vBit == true &&
                       XB_BackwardCompleteP1.vBit == false && XB_BackwardCompleteP2.vBit == false;
            }
        }
        public bool IsForwarding
        {
            get
            {
                return IsForward == false && YB_ForwardCmd == true;
            }
        }
        public bool IsBackward
        {
            get
            {
                return XB_ForwardCompleteP1.vBit == false && XB_ForwardCompleteP2.vBit == false &&
                          XB_BackwardCompleteP1.vBit == true && XB_BackwardCompleteP2.vBit == true;
            }
        }
        public bool IsBackwarding
        {
            get
            {
                return IsBackward == false && YB_BackwardCmd == true;
            }
        }

        public override void LogicWorking(Equipment equip)
        {
            UpdateTime();

            if (XB_ForwardCompleteP1.vBit == false && XB_BackwardCompleteP1.vBit == false && _isFirstChkP1 == true)
            {
                _errorStartTimeP1 = DateTime.Now;
                _isFirstChkP1 = false;
            }
            else if (XB_ForwardCompleteP1.vBit != XB_BackwardCompleteP1.vBit)
            {
                _errorStartTimeP1 = DateTime.Now;
                _isFirstChkP1 = true;
            }

            if ((DateTime.Now - _errorStartTimeP1).TotalSeconds > TimeOutInterval)
            {
                if (YB_ForwardCmd.vBit == true)
                {
                    AlarmMgr.Instance.Happen(equip, AlcdForwardTimeOut[0]);
                }
                else if (YB_BackwardCmd.vBit == true)
                {
                    AlarmMgr.Instance.Happen(equip, AlcdBackwardTimeOut[0]);
                }
                else
                {
                    AlarmMgr.Instance.Happen(equip, AlcdForwardTimeOut[0]);
                    AlarmMgr.Instance.Happen(equip, AlcdBackwardTimeOut[0]);
                }
            }

            if (XB_ForwardCompleteP2.vBit == false && XB_BackwardCompleteP2.vBit == false && _isFirstChkP2 == true)
            {
                _errorStartTimeP2 = DateTime.Now;
                _isFirstChkP2 = false;
            }
            else if (XB_ForwardCompleteP2.vBit != XB_BackwardCompleteP2.vBit)
            {
                _errorStartTimeP2 = DateTime.Now;
                _isFirstChkP2 = true;
            }

            if ((DateTime.Now - _errorStartTimeP2).TotalSeconds > TimeOutInterval)
            {
                if (YB_ForwardCmd.vBit == true)
                {
                    AlarmMgr.Instance.Happen(equip, AlcdForwardTimeOut[1]);
                }
                else if (YB_BackwardCmd.vBit == true)
                {
                    AlarmMgr.Instance.Happen(equip, AlcdBackwardTimeOut[1]);
                }
                else
                {
                    AlarmMgr.Instance.Happen(equip, AlcdForwardTimeOut[1]);
                    AlarmMgr.Instance.Happen(equip, AlcdBackwardTimeOut[1]);
                }
            }

            if (XB_ForwardCompleteP1.vBit && XB_BackwardCompleteP1.vBit)
            {
                Logger.Log.AppendLine(LogLevel.Error, "실린더 완료 동시 ON Error");
                //Console.WriteLine("실린더 완료 동시 ON Error");
            }

            if (XB_ForwardCompleteP2.vBit && XB_BackwardCompleteP2.vBit)
            {
                Logger.Log.AppendLine(LogLevel.Error, "실린더 완료 동시 ON Error");
                //Console.WriteLine("실린더 완료 동시 ON Error");
            }
            if (YB_ForwardCmd.vBit && YB_BackwardCmd.vBit)
            {
                Logger.Log.AppendLine(LogLevel.Error, "실린더 명령 동시 ON Error");
                //Console.WriteLine("실린더 명령 동시 ON Error");
            }

            if (YB_ForwardCmd.vBit && XB_ForwardCompleteP1.vBit && XB_ForwardCompleteP2.vBit)
            {
                if (YB_ForwardCmd.vBit == true)
                    equip.MccPc.SetMccAction(MCC_FWD_ON_OFF, false);

                YB_ForwardCmd.vBit = false;
            }
            else if (YB_BackwardCmd.vBit && XB_BackwardCompleteP1.vBit && XB_BackwardCompleteP2.vBit)
            {
                if (YB_BackwardCmd.vBit == true)
                    equip.MccPc.SetMccAction(MCC_BWD_ON_OFF, false);

                YB_BackwardCmd.vBit = false;
            }
            else
            {
                if (YB_ForwardCmd.vBit == true && (XB_ForwardCompleteP1.vBit == false || XB_ForwardCompleteP2.vBit == false))
                {
                    if ((DateTime.Now - ForwardStartTime).TotalSeconds > TimeOutInterval)
                    {
                        if (XB_ForwardCompleteP1.vBit == false)
                            AlarmMgr.Instance.Happen(equip, AlcdForwardTimeOut[0]);
                        if (XB_ForwardCompleteP2.vBit == false)
                            AlarmMgr.Instance.Happen(equip, AlcdForwardTimeOut[1]);

                        YB_ForwardCmd.vBit = false;
                    }
                }
                if (YB_BackwardCmd.vBit == true && (XB_BackwardCompleteP1.vBit == false || XB_BackwardCompleteP2.vBit == false))
                {
                    if ((DateTime.Now - BackwardStartTime).TotalSeconds > TimeOutInterval)
                    {
                        if (XB_BackwardCompleteP1.vBit == false)
                            AlarmMgr.Instance.Happen(equip, AlcdBackwardTimeOut[0]);
                        if (XB_BackwardCompleteP2.vBit == false)
                            AlarmMgr.Instance.Happen(equip, AlcdBackwardTimeOut[1]);

                        YB_BackwardCmd.vBit = false;
                    }
                }
            }
        }

        private void UpdateTime()
        {
            if (YB_ForwardCmd.vBit == true && XB_ForwardCompleteP1.vBit == false)
                ForwardTimeP1 = string.Format("{0:0.#}", (DateTime.Now - ForwardStartTime).TotalMilliseconds / 1000);
            if (YB_BackwardCmd.vBit == true && XB_BackwardCompleteP1.vBit == false)
                BackwardTimeP1 = string.Format("{0:0.#}", (DateTime.Now - BackwardStartTime).TotalMilliseconds / 1000);
            if (YB_ForwardCmd.vBit == true && XB_ForwardCompleteP2.vBit == false)
                ForwardTimeP2 = string.Format("{0:0.#}", (DateTime.Now - ForwardStartTime).TotalMilliseconds / 1000);
            if (YB_BackwardCmd.vBit == true && XB_BackwardCompleteP2.vBit == false)
                BackwardTimeP2 = string.Format("{0:0.#}", (DateTime.Now - BackwardStartTime).TotalMilliseconds / 1000);
        }

        //메소드 - 공통 인터락 확인 사항 
        public virtual bool IsInterlock(Equipment equip, bool isBackward)
        {
            if (equip.IsUseInterLockOff) return false;

            if (equip.LiftPin.IsForward)
            {
                if (equip.IsHomePositioning || equip.EquipRunMode == EmEquipRunMode.Auto || equip.IsCenteringStepping)
                {
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0201_LIFT_PIN_IS_NOT_UP_POSITION_STATE_ERROR);
                }
                else
                    InterLockMgr.AddInterLock("인터락<LIFT PIN>\n(센터링 이동은 LIFT PIN 하강 위치에서만 가능합니다.)");

                equip.IsInterlock = true;
                Logger.Log.AppendLine(LogLevel.Warning, "Lift Pin 하강 위치에서만 센터링 가능");
                return true;
            }

            if (equip.StageX.IsMoveOnPosition(0) == false && isBackward != true)
            {
                if (equip.IsHomePositioning || equip.EquipRunMode == EmEquipRunMode.Auto)
                {
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0497_STAGE_X_LOADING_POSITION_ERROR);
                    equip.IsInterlock = true;
                }
                else
                {
                    if (equip.IsCenteringStepping)
                        equip.IsInterlock = true;
                    InterLockMgr.AddInterLock("인터락<INSPECTION X축>\n(센터링 이동은 INSPECTION X축 로딩 위치에서만 가능합니다.)");
                }

                Logger.Log.AppendLine(LogLevel.Warning, "INSP X축 로딩  위치에서만 센터링 가능");
                return true;
            }
//             if (equip.InspY.IsMoveOnPosition(InspYServo.InspYLoadingPosiNo) == false && isBackward != true)
//             {
//                 if (equip.IsHomePositioning || equip.EquipRunMode == EmEquipRunMode.Auto)
//                 {
//                     AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0642_INSP_Y_LOADING_POSITION_ERROR);
//                     equip.IsInterlock = true;
//                 }
//                 else
//                 {
//                     if (equip.IsCenteringStepping)
//                         equip.IsInterlock = true;
//                     InterLockMgr.AddInterLock("인터락<INSPECTION Y축>\n(센터링 이동은 INSPECTION Y축 로딩 위치에서만 가능합니다.)");
//                 }
// 
//                 Logger.Log.AppendLine(LogLevel.Warning, "INSP Y축 로딩  위치에서만 센터링 가능");
//                 return true;
//             }
            

            return false;
        }
    }
}
