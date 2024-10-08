using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Struct.BaseUnit;

namespace EquipMainUi.Struct.Detail
{
    public class AdcVacuum : ADConverter_AJ65BTCU_64AD
    {
        public AdcVacuum(int unitCount)
            : base(unitCount)
        { }

        public float MainVaccum { get { return ReadDataBuf[0]; } }
        public float MainStandVaccum { get { return ReadDataBuf[1]; } }
        public float MainSubVaccum { get { return ReadDataBuf[2]; } }
        public float MainIonizerAir { get { return ReadDataBuf[3]; } }
        public float Vacuum1 { get { return ReadDataBuf[4]; } }
        public float Vacuum2 { get { return ReadDataBuf[5]; } }
        public float Vacuum3 { get { return ReadDataBuf[6]; } }
        public float Vacuum4 { get { return ReadDataBuf[7]; } }
    }
}
