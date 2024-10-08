using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Dit.Framework.PLC;

namespace EquipMainUi.Struct
{
    /// <summary>
    /// 170714 : Sol, Sensor 접정 설정 기능 추가    
    /// </summary>
    public class Switch : UnitBase
    {
        public Func<bool> InterLockFunc { get; set; }
        public VirtualMem PLC;
        protected bool _isAContactSol = true;
        protected bool _isAContactSensor = true;
        public PlcAddr YB_OnOff { get; set; }
        public PlcAddr XB_OnOff { get; set; }
        public DateTime OnOffStartTime = DateTime.Now;

        public int TimeOutInterval { get; set; }
        public EM_AL_LST ON_TIME_OUT_ERROR { get; set; }
        public EM_AL_LST OFF_TIME_OUT_ERROR { get; set; }
        public string OnOffTime { get; set; }
        public string OffOnTime { get; set; }

        public MccActionItem MccOnOff { get; set; }


        public Switch(bool isAContactSol = true, bool isAContactSensor = true)
        {
            _isAContactSol = isAContactSol;
            _isAContactSensor = isAContactSensor;
            TimeOutInterval = 3;
            ON_TIME_OUT_ERROR = EM_AL_LST.AL_0000_NONE;
            OFF_TIME_OUT_ERROR = EM_AL_LST.AL_0000_NONE;
            MccOnOff = MccActionItem.NONE;
        }
        public virtual void OnOff(Equipment equip, bool value)
        {
            if (InterLockFunc != null)
                if (InterLockFunc() == true)
                {
                    return;
                }
            
            equip.MccPc.SetMccAction(MccOnOff, value);
            
            OnOffStartTime = DateTime.Now;
            YB_OnOff.vBit = !(_isAContactSol ^ value);
        }
        public bool IsSolOnOff { get { return !(_isAContactSol ^ YB_OnOff.vBit); } }
        public bool IsOnOff { get { return !(_isAContactSensor ^ XB_OnOff.vBit); } }
        public override void LogicWorking(Equipment equip)
        {
            UpdateTime();

            if (IsSolOnOff == true && IsOnOff == false)
            {
                if ((DateTime.Now - OnOffStartTime).TotalSeconds > TimeOutInterval)
                {
                    AlarmMgr.Instance.Happen(equip, ON_TIME_OUT_ERROR);
                }
            }

            if (IsSolOnOff == false && IsOnOff == true)
            {
                if ((DateTime.Now - OnOffStartTime).TotalSeconds > TimeOutInterval)
                {
                    AlarmMgr.Instance.Happen(equip, OFF_TIME_OUT_ERROR);
                }
            }
        }
        protected void UpdateTime()
        {
            if (IsOnOff != IsSolOnOff && (IsSolOnOff == true))
                OffOnTime = ((DateTime.Now - OnOffStartTime).TotalMilliseconds / 1000).ToString("00.#");

            if (IsOnOff != IsSolOnOff && (IsSolOnOff == false))
                OnOffTime = ((DateTime.Now - OnOffStartTime).TotalMilliseconds / 1000).ToString("00.#");
        }       
    }
}
