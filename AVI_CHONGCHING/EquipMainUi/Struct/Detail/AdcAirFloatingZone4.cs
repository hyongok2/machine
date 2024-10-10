using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Struct.BaseUnit;

namespace EquipMainUi.Struct.Detail
{
    public class AdcAirFloatingZone4 : ADConverter_AJ65BTCU_64AD
    {
        public AdcAirFloatingZone4(int unitCount)
            : base(unitCount)
        { }

        public float AirFloatingZone25 { get { return ReadDataBuf[0]; } }
        public float AirFloatingZone26 { get { return ReadDataBuf[1]; } }
        public float AirFloatingZone27 { get { return ReadDataBuf[2]; } }
        public float AirFloatingZone28 { get { return ReadDataBuf[3]; } }
        public float AirFloatingZone29 { get { return ReadDataBuf[4]; } }
        public float AirFloatingZone30 { get { return ReadDataBuf[5]; } }
        public float AirFloatingZone31 { get { return ReadDataBuf[6]; } }
        public float AirFloatingZone32 { get { return ReadDataBuf[7]; } }
    }
}
