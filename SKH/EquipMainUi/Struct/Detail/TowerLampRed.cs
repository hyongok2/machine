using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipComm.PLC;
using System.Timers;
using DitCim.PLC;

namespace EquipMainUi.Struct
{
    public class TowerLampRed : FlickerSwitch
    {
        public TowerLampRed()
        {
            
        }      
        public override void LogicWorking(Equipment equip)
        {
            //TowerLamp R 점등
            if (equip.IsHeavyAlarm == true || (equip.IsPM == true && equip.IsLightAlarm == false
                        && CIMAB.OpcalSetCmd.vBit == false && equip.IsHeavyAlarm == false))
                YB_OnOff.vBit = true;
            else
            {
                YB_OnOff.vBit = false;

                //TowerLamp R 점멸
                if (equip.IsHeavyAlarm == false && (CIMAB.OpcalSetCmd.vBit == true || equip.IsLightAlarm == true))
                {
                    if (Flicker == false)
                        Flicker = true;
                }
                else
                    Flicker = false;
            }
            base.LogicWorking(equip);           
        }
    }
}
