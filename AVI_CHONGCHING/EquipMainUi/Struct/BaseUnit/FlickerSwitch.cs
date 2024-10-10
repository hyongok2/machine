using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using System.Timers;

namespace EquipMainUi.Struct
{
    public class FlickerSwitch : Switch
    {
        bool _isFlicker = false;
        public int Interval { get; set; }

        public FlickerSwitch(bool isAContactSol = true, bool isAContactSensor = true)
            : base(isAContactSol, isAContactSensor)
        {
            Interval = 800;
            TimeOutInterval = 3;
            ON_TIME_OUT_ERROR = EM_AL_LST.AL_0000_NONE;
            OFF_TIME_OUT_ERROR = EM_AL_LST.AL_0000_NONE;
            MccOnOff = MccActionItem.NONE;
        }        
        public bool Flicker
        {
            get { return _isFlicker; }
            set
            {
                _isFlicker = value;
                if(value == false)
                    YB_OnOff.vBit = !(_isAContactSol ^ false);

                OnOffStartTime = DateTime.Now;
            }
        }
        public override void LogicWorking(Equipment equip)
        {
            if (Flicker)
            {
                if ((DateTime.Now - OnOffStartTime).TotalMilliseconds >= Interval)
                {
                    YB_OnOff.vBit = !YB_OnOff.vBit;
                    OnOffStartTime = DateTime.Now;
                }                
            }
        }
    }
}
