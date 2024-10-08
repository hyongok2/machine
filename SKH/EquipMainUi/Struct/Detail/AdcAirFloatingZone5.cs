using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Struct.BaseUnit;

namespace EquipMainUi.Struct.Detail
{
    public class AdcAirFloatingZone5 : ADConverter_AJ65BTCU_64AD
    {
        public AdcAirFloatingZone5(int unitCount)
            : base(unitCount)
        { }

        public float AirFloatingZone33 { get { return ReadDataBuf[0]; } }
        public float AirFloatingZone34 { get { return ReadDataBuf[1]; } }
        public float AirFloatingZone35 { get { return ReadDataBuf[2]; } }
    }
}
