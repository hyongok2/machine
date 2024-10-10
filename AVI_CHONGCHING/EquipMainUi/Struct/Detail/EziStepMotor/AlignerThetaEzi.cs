using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Detail.EziStep
{
    public class AlignerThetaEzi : StepMotorEzi
    {
        public AlignerThetaEzi(int innnerSlaveNo, string name, int posiCount) : base(innnerSlaveNo, name, posiCount)
        {
            motorType = EM_STEP_MOTOR_TYPE.THETA;

            SoftPlusLimit = 30;
            SoftMinusLimit = -1;

            SoftSpeedLimit = 10;
            SoftJogSpeedLimit = 10;
        }
    }
}
