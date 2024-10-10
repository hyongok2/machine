using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.BaseUnit
{
    public class SwitchAirKnife : SwitchOneWay
    {
        public new bool OnOff(Equipment equip, bool value)
        {
            if (GG.Equip.CtrlSetting.AirKnifeUse)
                return base.OnOff(equip, value);
            else
            {
                return false;
            }
        }
    }
}
