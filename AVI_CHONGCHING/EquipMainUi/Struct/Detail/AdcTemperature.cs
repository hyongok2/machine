using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Struct.BaseUnit;

namespace EquipMainUi.Struct.Detail
{
    public class AdcTemperature : ADConverter_AJ65BT_64RD3
    {
        public AdcTemperature(int unitCount)
            : base(unitCount)
        { }

        public float CurPanelTemp { get { return ReadDataBuf[0]; } }
        public float CurPCRackTemp { get { return ReadDataBuf[1]; } }
    }
}
