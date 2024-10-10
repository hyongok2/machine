using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Struct.BaseUnit;

namespace EquipMainUi.Struct.Detail
{
    public class AdcAirFloatingZone3 : ADConverter_AJ65BTCU_64AD
    {
        public AdcAirFloatingZone3(int unitCount)
            : base(unitCount)
        { }

        public float AirFloatingZone17 { get { return ReadDataBuf[0]; } }
        public float AirFloatingZone18 { get { return ReadDataBuf[1]; } }
        public float AirFloatingZone19 { get { return ReadDataBuf[2]; } }
        public float AirFloatingZone20 { get { return ReadDataBuf[3]; } }
        public float AirFloatingZone21 { get { return ReadDataBuf[4]; } }
        public float AirFloatingZone22 { get { return ReadDataBuf[5]; } }
        public float AirFloatingZone23 { get { return ReadDataBuf[6]; } }
        public float AirFloatingZone24 { get { return ReadDataBuf[7]; } }
    }
}
