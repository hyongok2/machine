using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Struct.BaseUnit;

namespace EquipMainUi.Struct.Detail
{
    public class AdcAirFloatingVacuum : ADConverter_AJ65BTCU_64AD
    {
        public AdcAirFloatingVacuum(int unitCount)
            : base(unitCount)
        { }

        public float AirFloatingVacuum1 { get { return ReadDataBuf[0]; } }
        public float AirFloatingVacuum2 { get { return ReadDataBuf[1]; } }
        public float AirFloatingVacuum3 { get { return ReadDataBuf[2]; } }
        public float AirFloatingVacuum4 { get { return ReadDataBuf[3]; } }
        public float AirFloatingVacuum5 { get { return ReadDataBuf[4]; } }
        public float MainGrabber1 { get { return ReadDataBuf[5]; } }
        public float MainGrabber2 { get { return ReadDataBuf[6]; } }
    }
}
