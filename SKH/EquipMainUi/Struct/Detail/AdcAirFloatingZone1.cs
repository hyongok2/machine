using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Struct.BaseUnit;

namespace EquipMainUi.Struct.Detail
{
    public class AdcAirFloatingZone1 : ADConverter_AJ65BTCU_64AD
    {
        public AdcAirFloatingZone1(int unitCount)
            : base(unitCount)
        { }

        public float AirFloatingZone1 { get { return ReadDataBuf[0]; } }
        public float AirFloatingZone2 { get { return ReadDataBuf[1]; } }
        public float AirFloatingZone3 { get { return ReadDataBuf[2]; } }
        public float AirFloatingZone4 { get { return ReadDataBuf[3]; } }
        public float AirFloatingZone5 { get { return ReadDataBuf[4]; } }
        public float AirFloatingZone6 { get { return ReadDataBuf[5]; } }
        public float AirFloatingZone7 { get { return ReadDataBuf[6]; } }
        public float AirFloatingZone8 { get { return ReadDataBuf[7]; } }
    }
}
