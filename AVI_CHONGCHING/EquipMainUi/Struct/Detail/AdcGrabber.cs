using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Struct.BaseUnit;

namespace EquipMainUi.Struct.Detail
{
    public class AdcGrabber : ADConverter_AJ65BTCU_64AD
    {
        public AdcGrabber(int unitCount)
            : base(unitCount)
        { }

        public float MainGrabber3 { get { return ReadDataBuf[0]; } }
        public float MainGrabber4 { get { return ReadDataBuf[1]; } }
        public float MainGrabber5 { get { return ReadDataBuf[2]; } }
        public float MainGrabber6 { get { return ReadDataBuf[3]; } }
        public float ULDAir1 { get { return ReadDataBuf[4]; } }
        public float ULDAir2 { get { return ReadDataBuf[5]; } }
        public float ScanAir { get { return ReadDataBuf[6]; } }
    }
}
