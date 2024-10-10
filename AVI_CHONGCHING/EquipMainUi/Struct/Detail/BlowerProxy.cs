using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.Detail
{
    public class BlowerProxy
    {
        public Equipment Equip { get; set; }

        public SwitchOneWay Stage1 = new SwitchOneWay();
        public SwitchOneWay Stage2 = new SwitchOneWay();

        public bool IsSolOn
        {
            get
            {
                return Stage1.IsOnOff == true
                    || Stage2.IsOnOff == true
                    ;

            }
        }

        //기준명 센터링. 

        public bool IsInterlock()
        {
            return false;
        }
        public bool BlowerOn()
        {
            if (IsInterlock()) return false;
            // 정상 동작

            Stage1.OnOff(Equip, true);
            Stage2.OnOff(Equip, true);

            return true;
        }
        public bool BlowerOff()
        {
            if (IsInterlock()) return false;
            // 정상 동작

            Stage1.OnOff(Equip, false);
            Stage2.OnOff(Equip, false);
            return true;
        }
    }
}
