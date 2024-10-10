using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Detail.EziStep
{
    public class AlignerYEzi : StepMotorEzi
    {
        public AlignerYEzi(int innnerSlaveNo, string name, int posiCount) : base(innnerSlaveNo, name, posiCount)
        {
            SoftPlusLimit = 15;
            SoftMinusLimit = -1;

            SoftSpeedLimit = 4;
            SoftJogSpeedLimit = 4;
        }
    }
}
