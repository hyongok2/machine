using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.Detail
{
    public enum LampState
    {
        ON,
        OFF,
        BLINKING
    }

    public class TowerLampProxy : UnitBase
    {
        private bool _blueOn = false;

        public TowerLamp Red                                                    /**/    = new TowerLamp();
        public TowerLamp Yellow                                                 /**/    = new TowerLamp();
        public TowerLamp Green                                                  /**/    = new TowerLamp();
        public TowerLamp Blue                                                   /**/    = new TowerLamp();

        public override void LogicWorking(Equipment equip)
        {
            Red.LogicWorking(equip);
            Yellow.LogicWorking(equip);
            Green.LogicWorking(equip);
            if (_blueOn == true)
            {
                Blue.LogicWorking(equip);
                //    Blue.State = equip.HsmsPc.CimOnOff == EmCimOnOff.CimOn ? LampState.ON : LampState.OFF;
            }

            if (equip.EquipRunMode == EmEquipRunMode.Auto
                //equip.ProcState == EMProcState.Run
                )
            {
                if (equip.IsLightAlarm == false)
                {
                    Red.State = LampState.OFF;
                    Yellow.State = LampState.OFF;
                    Green.State = LampState.ON;
                }
                else
                {
                    Red.State = LampState.BLINKING;
                    Yellow.State = LampState.OFF;
                    Green.State = LampState.ON;
                }
            }
            //else if (equip.ProcState == EMProcState.Idle)
            //{
            //    if (equip.IsLightAlarm == false)
            //    {
            //        Red.State = LampState.OFF;
            //        Yellow.State = LampState.BLINKING;
            //        Green.State = LampState.OFF;
            //    }
            //    else
            //    {
            //        Red.State = LampState.BLINKING;
            //        Yellow.State = LampState.OFF;
            //        Green.State = LampState.BLINKING;
            //    }
            //}
            //else if (equip.ProcState == EMProcState.BM)
            //{
            //    Red.State = LampState.BLINKING;
            //    Yellow.State = LampState.BLINKING;
            //    Green.State = LampState.OFF;
            //}
            //else if (equip.ProcState == EMProcState.Pause)
            //{
            //    Red.State = LampState.ON;
            //    Yellow.State = LampState.BLINKING;
            //    Green.State = LampState.OFF;
            //}
            //else if (equip.ProcState == EMProcState.PM)
            //{
            //    Red.State = LampState.ON;
            //    Yellow.State = LampState.OFF;
            //    Green.State = LampState.OFF;
            //}

        }
    }
}
