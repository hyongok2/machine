using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using System.Timers;
using DitCim.PLC;
using EquipMainUi.Struct.Detail;

namespace EquipMainUi.Struct
{
    public class TowerLamp : FlickerSwitch
    {
        public LampState State { get; set; }

        public TowerLamp()
        {
            State = LampState.OFF;
        }
        public override void LogicWorking(Equipment equip)
        {

            if (State == LampState.ON)
            {
                OnOff(equip, true);
            }
            else if (State == LampState.OFF)
            {
                OnOff(equip, false);
            }
            else if (State == LampState.BLINKING)
            {
                if (DateTime.Now.Millisecond > 500)
                    OnOff(equip, false);
                else
                    OnOff(equip, true);
            }
            base.LogicWorking(equip);
        }
    }
}
