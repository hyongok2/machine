using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.Detail
{
    public class ModeSelectKeyBox
    {
        public Equipment Equip;
        public SwitchOneWay Sol;
        public Sensor AutoSensor;

        public ModeSelectKeyBox()
        {
            Sol = new SwitchOneWay();
            AutoSensor = new Sensor();
        }

        public bool IsAuto
        {
            get
            {
                return AutoSensor.IsOn;                
            }
        }

        public void SolOn(bool auto)
        {
            Sol.OnOff(Equip, auto);
        }
    }
}
