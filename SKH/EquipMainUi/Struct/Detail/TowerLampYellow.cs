using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipComm.PLC;
using System.Timers;
using DitCim.PLC;

namespace EquipMainUi.Struct
{
    public class TowerLampYellow : FlickerSwitch
    {
        public TowerLampYellow()
        {
            
        }      
        public override void LogicWorking(Equipment equip)
        {
            if (equip.IsHeavyAlarm == false && CIMAB.OpcalSetCmd.vBit == false && equip.IsLightAlarm == false)
            {
                if (equip.IsPM == true)
                    YB_OnOff.vBit = true;
                else if (equip.IsGlassCheckOk == EmGlassSense.ALL || equip.EquipRunMode != EmEquipRunMode.Auto
                         || equip.IsPause == true)
                    YB_OnOff.vBit = true;
                else if (Flicker == false)
                    YB_OnOff.vBit = false;
            }
            else if (CIMAB.OpcalSetCmd.vBit == true && equip.IsHeavyAlarm == false && equip.IsLightAlarm == false)
            {
                if (Flicker == false)
                    Flicker = true;
            }
            else
            {
                Flicker = false;
                YB_OnOff.vBit = false;
            }
               
            base.LogicWorking(equip);           
        }
    }
}
