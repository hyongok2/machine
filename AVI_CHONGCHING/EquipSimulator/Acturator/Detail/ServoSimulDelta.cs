
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipSimulator.Acturator.Detail
{
    public class ServoSimulDelta : ServoSimul2
    {
        public bool THETA_WAIT_POS_MOVE;
        public bool THETA_WAIT_POS_MOVE_END;

        public int Cmd = 0;
        int stepMove1 = 0; 

        protected override void MoveWoring()
        {
            PositionWoring(ref stepMove1, ref THETA_WAIT_POS_MOVE, ref   THETA_WAIT_POS_MOVE_END, 100, 10, false, "THETA 조정");
            base.MoveWoring();
        }
    }
}
