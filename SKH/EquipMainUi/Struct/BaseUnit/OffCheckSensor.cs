using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.BaseUnit
{
    public class OffCheckSwitch : Switch
    {
        private int _offTimeLimitSecond = 1;

        public OffCheckSwitch(bool isAContactSol = true, bool isAContactSensor = true)
            : base(isAContactSol, isAContactSensor)
        {
            
        }
        public override void LogicWorking(Equipment equip)
        {
            if (IsSolOnOff == false && IsOnOff == true)
            {
                if ((DateTime.Now - OnOffStartTime).TotalSeconds > _offTimeLimitSecond)
                {
                    AlarmMgr.Instance.Happen(equip, OFF_TIME_OUT_ERROR);
                }
            }
        }
    }
}
