using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipSimulator.Acturator.Detail
{
    public class ServoSimulInspY : ServoSimul2
    {
        public bool INSPECTION_SHIFT1_POS_MOVE;
        public bool INSPECTION_SHIFT2_POS_MOVE;
        public bool INSPECTION_SHIFT3_POS_MOVE;
        public bool INSPECTION_SHIFT4_POS_MOVE;
        public bool INSPECTION_SHIFT5_POS_MOVE;
        public bool INSPECTION_SHIFT6_POS_MOVE;

        public bool INSPECTION_SHIFT1_POS_MOVE_END;
        public bool INSPECTION_SHIFT2_POS_MOVE_END;
        public bool INSPECTION_SHIFT3_POS_MOVE_END;
        public bool INSPECTION_SHIFT4_POS_MOVE_END;
        public bool INSPECTION_SHIFT5_POS_MOVE_END;
        public bool INSPECTION_SHIFT6_POS_MOVE_END;

        public int Cmd = 0;
        int stepMove1 = 0;
        int stepMove2 = 0;
        int stepMove3 = 0;
        int stepMove4 = 0;
        int stepMove5 = 0;
        int stepMove6 = 0;
        int stepMove7 = 0;

        protected override void MoveWoring()
        {
            PositionWoring(ref stepMove1, ref INSPECTION_SHIFT1_POS_MOVE, ref   INSPECTION_SHIFT1_POS_MOVE_END, 100, 10, false, "SCAN 1 위치");
            PositionWoring(ref stepMove2, ref INSPECTION_SHIFT2_POS_MOVE, ref   INSPECTION_SHIFT2_POS_MOVE_END, 200, 10, false, "SCAN 2 위치");
            PositionWoring(ref stepMove3, ref INSPECTION_SHIFT3_POS_MOVE, ref   INSPECTION_SHIFT3_POS_MOVE_END, 300, 10, false, "SCAN 3 위치");
            PositionWoring(ref stepMove4, ref INSPECTION_SHIFT4_POS_MOVE, ref   INSPECTION_SHIFT4_POS_MOVE_END, 400, 10, false, "SCAN 4 위치");
            PositionWoring(ref stepMove5, ref INSPECTION_SHIFT5_POS_MOVE, ref   INSPECTION_SHIFT5_POS_MOVE_END, 500, 10, false, "SCAN 5 위치");
            PositionWoring(ref stepMove6, ref INSPECTION_SHIFT6_POS_MOVE, ref   INSPECTION_SHIFT6_POS_MOVE_END, 600, 10, false, "SCAN 6 위치");
            base.MoveWoring();
        }
    }
}
