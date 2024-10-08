using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using EquipMainUi.Struct.Detail;

namespace EquipMainUi.Struct.BaseUnit
{
    /// <summary>
    /// 1 sol에 Sensor 개수는 제한없이 추가 가능
    /// date    20170514
    /// </summary>
    public class CustomCylinder : UnitBase
    {
        protected string Name;
        public int ForwardOverTime = 5000;
        public int BackwardOverTime = 5000;
        protected int _currentDirectionOverTime = 5000;

        public PlcAddr YB_ForwardCmd { get; set; }
        public PlcAddr YB_BackwardCmd { get; set; }

        public PlcAddr[] XB_ForwardComplete { get; set; }
        public PlcAddr[] XB_BackwardComplete { get; set; }

        public DateTime ForwardStartTime { get; set; }
        public DateTime BackwardStartTime { get; set; }

        public string[] ForwardTime { get; set; }
        public string[] BackwardTime { get; set; }

        public EM_AL_LST[] AlcdForwardTimeOut;
        public EM_AL_LST[] AlcdBackwardTimeOut;
        public MccActionItem MCC_FWD_ON_OFF { get; set; }
        public MccActionItem MCC_BWD_ON_OFF { get; set; }
        protected DateTime[] _errorStartTime;
        protected bool[] _isFirstChk;
        protected int _sensorCount;

        public CustomCylinder()
        {

        }
        public CustomCylinder(string partsName, int sensorCount)
        {
            Name = partsName;
            _sensorCount = sensorCount;
            XB_ForwardComplete = new PlcAddr[sensorCount];
            XB_BackwardComplete = new PlcAddr[sensorCount];

            ForwardTime = new string[sensorCount];
            BackwardTime = new string[sensorCount];

            AlcdForwardTimeOut = new EM_AL_LST[sensorCount];
            AlcdBackwardTimeOut = new EM_AL_LST[sensorCount];

            _errorStartTime = new DateTime[sensorCount];
            _isFirstChk = new bool[sensorCount];
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
                return XB_ForwardComplete.FirstOrDefault(f => f == false) == null &&
                       XB_BackwardComplete.FirstOrDefault(f => f == true) == null;
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
                return XB_BackwardComplete.FirstOrDefault(f => f == false) == null &&
                       XB_ForwardComplete.FirstOrDefault(f => f == true) == null;
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

            for (int point = 0; point < _sensorCount; ++point)
                CheckErrorByPoint(ref equip, ref XB_ForwardComplete[point], ref XB_BackwardComplete[point],
                    ref _isFirstChk[point], ref _errorStartTime[point], ref AlcdForwardTimeOut[point], ref AlcdBackwardTimeOut[point]);

            if (YB_ForwardCmd.vBit && YB_BackwardCmd.vBit)
            {
                Logger.Log.AppendLine(LogLevel.Error, "{0} 명령 동시 ON Error", Name);
                //Console.WriteLine("실린더 명령 동시 ON Error");
            }

            if (YB_ForwardCmd.vBit && IsForward)
            {
                if (YB_ForwardCmd.vBit == true)
                    equip.MccPc.SetMccAction(MCC_FWD_ON_OFF, false);

                YB_ForwardCmd.vBit = false;
            }
            else if (YB_BackwardCmd.vBit && IsBackward)
            {
                if (YB_BackwardCmd.vBit == true)
                    equip.MccPc.SetMccAction(MCC_BWD_ON_OFF, false);

                YB_BackwardCmd.vBit = false;
            }
            else
            {
                if (YB_ForwardCmd.vBit == true && IsForward == false)
                {
                    if ((DateTime.Now - ForwardStartTime).TotalMilliseconds > ForwardOverTime)
                    {
                        for (int point = 0; point < _sensorCount; ++point)
                        {
                            if (XB_ForwardComplete[point].vBit == false)
                                AlarmMgr.Instance.Happen(equip, AlcdForwardTimeOut[0]);
                        }

                        YB_ForwardCmd.vBit = false;
                    }
                }
                if (YB_BackwardCmd.vBit == true && IsBackward == false)
                {
                    if ((DateTime.Now - BackwardStartTime).TotalMilliseconds > BackwardOverTime)
                    {
                        for (int point = 0; point < _sensorCount; ++point)
                        {
                            if (XB_BackwardComplete[point].vBit == false)
                                AlarmMgr.Instance.Happen(equip, AlcdBackwardTimeOut[0]);
                        }

                        YB_BackwardCmd.vBit = false;
                    }
                }
            }
        }

        private void CheckErrorByPoint(ref Equipment equip, ref PlcAddr forwardComplete, ref PlcAddr backwardComplete,
            ref bool firstCheck, ref DateTime errorStartTime, ref EM_AL_LST forwardTimeout, ref EM_AL_LST backwardTimeout)
        {
            if (forwardComplete.vBit == false && backwardComplete.vBit == false && firstCheck == true)
            {
                errorStartTime = DateTime.Now;
                firstCheck = false;
            }
            else if (forwardComplete.vBit != backwardComplete.vBit)
            {
                errorStartTime = DateTime.Now;
                firstCheck = true;
            }

            _currentDirectionOverTime = YB_ForwardCmd.vBit ? ForwardOverTime : BackwardOverTime;
            if ((DateTime.Now - errorStartTime).TotalMilliseconds > _currentDirectionOverTime)
            {
                if (YB_ForwardCmd.vBit == true)
                {
                    AlarmMgr.Instance.Happen(equip, forwardTimeout);
                }
                else if (YB_BackwardCmd.vBit == true)
                {
                    AlarmMgr.Instance.Happen(equip, backwardTimeout);
                }
                else
                {
                    AlarmMgr.Instance.Happen(equip, forwardTimeout);
                    AlarmMgr.Instance.Happen(equip, backwardTimeout);
                }
            }

            if (forwardComplete.vBit && backwardComplete.vBit)
            {
                Logger.Log.AppendLine(LogLevel.Error, "실린더 완료 동시 ON Error ({0}, {1})",
                    forwardComplete.Desc, backwardComplete.Desc);
            }
        }

        protected virtual void UpdateTime()
        {
            for (int point = 0; point < _sensorCount; ++point)
            {
                if (YB_ForwardCmd.vBit == true && XB_ForwardComplete[point].vBit == false)
                    ForwardTime[point] = string.Format("{0:0.#}", (DateTime.Now - ForwardStartTime).TotalMilliseconds / 1000);
                if (YB_BackwardCmd.vBit == true && XB_BackwardComplete[point].vBit == false)
                    BackwardTime[point] = string.Format("{0:0.#}", (DateTime.Now - BackwardStartTime).TotalMilliseconds / 1000);
            }
        }

        //메소드 - 공통 인터락 확인 사항 
        public virtual bool IsInterlock(Equipment equip, bool isBackward)
        {
            if (equip.IsUseInterLockOff) return false;

            if (true
                && GG.TestMode == false
                && equip.IsHeavyAlarm == true
                )
            {
                InterLockMgr.AddInterLock(string.Format("인터락<HEAVY ALARM>\n(HEAVY ALARM 상태입니다. {0} 동작이 불가능 합니다.)", Name));
                return true;
            }

            if (true
                && GG.TestMode == false
                && equip.IsEnableGripMiddleOn == false
                && equip.IsDoorOpen
                )
            {
                InterLockMgr.AddInterLock(string.Format("인터락<DOOR>\n(설비 상태가 DOOR OPEN 상태에서 {0} 이동이 금지됩니다.)", Name));
                Logger.Log.AppendLine(LogLevel.Warning, "설비 상태가 Door Open 상태에서 {0} 이동 금지됨", Name);
                return true;
            }

            // 전진
            if (isBackward == false)
            {
                if (equip.IsUseLiftpinVacuumCenteringExMode == false)
                {
                    if (equip.LiftPin.IsForward == false)
                    {
                        if (equip.IsHomePositioning || equip.EquipRunMode == EmEquipRunMode.Auto || equip.IsCenteringStepping)
                        {
                            AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0201_LIFT_PIN_IS_NOT_UP_POSITION_STATE_ERROR);
                        }
                        else
                            InterLockMgr.AddInterLock("인터락<LIFT PIN>\n(센터링 이동은 LIFT PIN 상승 위치에서만 가능합니다.)");

                        equip.IsInterlock = true;
                        Logger.Log.AppendLine(LogLevel.Warning, "Lift Pin 상승 위치에서만 센터링 가능");
                        return true;
                    }
                }

                if (equip.StageX.IsMoveOnPosition(StageXServo.HomePosition) == false && equip.StageX.IsMoveOnPosition(StageXServo.LoadingPos)==false)
                {
                    Logger.Log.AppendLine(LogLevel.Error, string.Format("INSP X축 확인-{0}, {1}, {2}",
                        equip.StageX.IsLoadingOnPosition == true ? "TRUE" : "FALSE",
                        equip.StageX.IsMoving == false ? "TRUE" : "FALSE",
                        equip.StageX.IsStepEnd == true ? "TRUE" : "FALSE"));

                    if (equip.IsHomePositioning || equip.EquipRunMode == EmEquipRunMode.Auto)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0497_STAGE_X_LOADING_POSITION_ERROR);
                        equip.IsInterlock = true;
                    }
                    else
                    {
                        if (equip.IsCenteringStepping)
                            equip.IsInterlock = true;
                        InterLockMgr.AddInterLock(string.Format("인터락<INSPECTION X축>\n {0} 이동은 INSPECTION X축 로딩 위치에서만 가능합니다.)", this.Name));
                    }

                    Logger.Log.AppendLine(LogLevel.Warning, string.Format("INSP X축 로딩 위치에서만 {0} 조작 가능", this.Name));
                    return true;
                }
                if ((equip.StageY.IsMoveOnPosition(StageXServo.HomePosition) == false && equip.StageY.IsMoveOnPosition(StageXServo.LoadingPos) == false))
                {
                    Logger.Log.AppendLine(LogLevel.Error, string.Format("INSP Y축 확인-{0}, {1}, {2}",
                        equip.StageY.XF_CurrMotorPosition.vFloat,
                        equip.StageY.IsMoving == false ? "TRUE" : "FALSE",
                        equip.StageY.IsStepEnd == true ? "TRUE" : "FALSE"));

                    if (equip.IsHomePositioning || equip.EquipRunMode == EmEquipRunMode.Auto)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0515_STAGE_Y_LOADING_POSITION_ERROR);
                        equip.IsInterlock = true;
                    }
                    else
                    {
                        if (equip.IsCenteringStepping)
                            equip.IsInterlock = true;
                        InterLockMgr.AddInterLock(string.Format("인터락<INSPECTION Y축>\n {0} 이동은 INSPECTION Y축 로딩 위치에서만 가능합니다.)", this.Name));
                    }

                    Logger.Log.AppendLine(LogLevel.Warning, string.Format("INSP Y축 로딩 위치에서만 {0} 조작 가능", this.Name));
                    return true;
                }
                if (equip.Theta.IsMoveOnPosition(ThetaServo.LoadingPos) == false)
                {
                    Logger.Log.AppendLine(LogLevel.Error, string.Format("Theta축 확인-{0}, {1}, {2}",
                        equip.Theta.XF_CurrMotorPosition.vFloat,
                        equip.Theta.IsMoving == false ? "TRUE" : "FALSE",
                        equip.Theta.IsStepEnd == true ? "TRUE" : "FALSE"));

                    if (equip.IsHomePositioning || equip.EquipRunMode == EmEquipRunMode.Auto)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0530_THETA_LOADING_POSITION_ERROR);
                        equip.IsInterlock = true;
                    }
                    else
                    {
                        if (equip.IsCenteringStepping)
                            equip.IsInterlock = true;
                        InterLockMgr.AddInterLock(string.Format("인터락<Theta축>\n {0} 이동은 Theta축 로딩 위치에서만 가능합니다.)", this.Name));
                    }

                    Logger.Log.AppendLine(LogLevel.Warning, string.Format("Theta축 로딩 위치에서만 {0} 조작 가능", this.Name));
                    return true;
                }
            }
                //후진
            else
            {

            }
            return false;
        }
    }
}
