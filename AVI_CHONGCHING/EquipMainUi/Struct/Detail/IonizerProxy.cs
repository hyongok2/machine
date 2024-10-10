using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.Detail
{
    public class IonizerProxy
    {
        public Equipment Equip { get; set; }

        public bool IonizerOn(Equipment equip)
        {
            Equip.Ionizer1AirOn.OnOff(equip, true);
            Equip.Ionizer1PowerOn.OnOff(equip, true);
            Equip.Ionizer2PowerOn.OnOff(equip, true);
            Equip.Ionizer3PowerOn.OnOff(equip, true);
            Equip.Ionizer4PowerOn.OnOff(equip, true);

            Logger.Log.AppendLine(LogLevel.Info, "이오나이저 ON");
            return true;
        }
        public bool IonizerOff(Equipment equip)
        {
            Equip.Ionizer1AirOn.OnOff(equip, false);
            Equip.Ionizer1PowerOn.OnOff(equip, false);
            Equip.Ionizer2PowerOn.OnOff(equip, false);
            Equip.Ionizer3PowerOn.OnOff(equip, false);
            Equip.Ionizer4PowerOn.OnOff(equip, false);

            Logger.Log.AppendLine(LogLevel.Info, "이오나이저 OFF");
            return true;
        }
        public bool IsIonizerOn
        {
            get
            {
                return Equip.Ionizer1AirOn.IsOnOff &&
                       Equip.Ionizer1PowerOn.IsOnOff &&
                       Equip.Ionizer2PowerOn.IsOnOff &&
                       Equip.Ionizer3PowerOn.IsOnOff &&
                       Equip.Ionizer4PowerOn.IsOnOff;
            }
        }
        public bool IsIonizerPowerOn
        {
            get
            {
                return Equip.Ionizer1PowerOn.IsOnOff &&
                       Equip.Ionizer2PowerOn.IsOnOff &&
                       Equip.Ionizer3PowerOn.IsOnOff &&
                       Equip.Ionizer4PowerOn.IsOnOff;
            }
        }
        public bool IsIonizerOff
        {
            get
            {
                return Equip.Ionizer1AirOn.IsOnOff == false &&
                       Equip.Ionizer1PowerOn.IsOnOff == false &&
                       Equip.Ionizer2PowerOn.IsOnOff == false &&
                       Equip.Ionizer3PowerOn.IsOnOff == false &&
                       Equip.Ionizer4PowerOn.IsOnOff == false;
            }
        }
        public bool IsIonizerPowerOff
        {
            get
            {
                return Equip.Ionizer1PowerOn.IsOnOff == false &&
                       Equip.Ionizer2PowerOn.IsOnOff == false &&
                       Equip.Ionizer3PowerOn.IsOnOff == false &&
                       Equip.Ionizer4PowerOn.IsOnOff == false;
            }
        }
    }
}
