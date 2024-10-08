using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;

namespace EquipMainUi.Struct
{
    /// <summary>
    /// Blower Interlock, InterlockFunc 활용 => Equipment.InterlockSetting() 참고
    /// date 17.06.08
    /// </summary>
    public class SwitchOneWay : UnitBase
    {
        public Func<Equipment, bool, bool> InterLockFunc { get; set; }
        public PlcAddr YB_OnOff { get; set; }

        public MccActionItem MccOnOff { get; set; }

        public SwitchOneWay()
        {
            MccOnOff = MccActionItem.NONE;
        }

        public bool OnOff(Equipment equip, bool value)
        {
            if (InterLockFunc != null)
                if (InterLockFunc(equip, value) == true)
                {
                    return false;
                }
            YB_OnOff.vBit = value;
            return true;
        }

        public bool IsOnOff { get { return YB_OnOff.vBit; } }
    }
}
