using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Struct.BaseUnit;

namespace EquipMainUi.Struct.Detail
{
    public class AdcAirFloatingZone2 : ADConverter_AJ65BTCU_64AD
    {
        public AdcAirFloatingZone2(int unitCount)
            : base(unitCount)
        { }

        public float AirFloatingZone9 { get { return ReadDataBuf[0]; } }
        public float AirFloatingZone10 { get { return ReadDataBuf[1]; } }
        public float AirFloatingZone11 { get { return ReadDataBuf[2]; } }
        public float AirFloatingZone12 { get { return ReadDataBuf[3]; } }
        public float AirFloatingZone13 { get { return ReadDataBuf[4]; } }
        public float AirFloatingZone14 { get { return ReadDataBuf[5]; } }
        public float AirFloatingZone15 { get { return ReadDataBuf[6]; } }
        public float AirFloatingZone16 { get { return ReadDataBuf[7]; } }
    }
}
