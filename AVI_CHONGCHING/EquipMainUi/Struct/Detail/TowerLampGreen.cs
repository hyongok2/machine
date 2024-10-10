using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipComm.PLC;
using System.Timers;
using DitCim.PLC;

namespace EquipMainUi.Struct
{
    public class TowerLampGreen : FlickerSwitch
    {
        public TowerLampGreen()
        {

        }
        public override void LogicWorking(Equipment equip)
        {
            //TowerLamp G 점등
            if (equip.IsHeavyAlarm == false)
            {
                if (equip.IsLightAlarm == true)
                {
                    YB_OnOff.vBit = true;
                }
                else
                {
                    if (CIMAB.OpcalSetCmd.vBit == true)
                    {
                        if (Flicker == false)
                            Flicker = true;
                    }
                    else
                    {
                        if (equip.IsPM == true)
                            YB_OnOff.vBit = true;
                        else if ((equip.EquipRunMode == EmEquipRunMode.Auto && equip.IsGlassCheckOk == EmGlassSense.ALL) ||
                                (equip.IsCycleStop != EmCycleStop.None && equip.IsGlassCheckOk == EmGlassSense.ALL) ||
                                equip.IsPause == true)
                            YB_OnOff.vBit = true;
                        else
                        {
                            YB_OnOff.vBit = false;
                            Flicker = false;
                        }
                    }
                }
            }
            else
            {
                YB_OnOff.vBit = false;
                Flicker = false;
            }
            base.LogicWorking(equip);
        }
    }
}
