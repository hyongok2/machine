using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.Detail
{
    public class WorkingLightProxy
    {
        public bool IsAutoMode = true;

        public Switch CpBox          /**/ = new Switch();
        public Switch PcRack          /**/ = new Switch();
        public Switch Stage          /**/ = new Switch();

        public Equipment Equip { get; set; }

        public void LogicWorking()
        {
            AutoLogic(Equip);
        }
        public void AllOnOff(bool isOn)
        {
            PcRack.OnOff(Equip, isOn);
            Stage.OnOff(Equip, isOn);
        }
        public void Toggle()
        {
            bool isOn = !Stage.IsSolOnOff;
            AllOnOff(isOn);
        }

        public void AutoLogic(Equipment Equip)
        {
            if (IsAutoMode == false) return;

            if (Equip.EquipRunMode == EmEquipRunMode.Auto)
            {
                if (Stage.IsSolOnOff == true)
                    Stage.OnOff(Equip, false);
            }
            else 
            {
                if (Stage.IsSolOnOff == false)
                    Stage.OnOff(Equip, true);
            }
        }
    }
}
