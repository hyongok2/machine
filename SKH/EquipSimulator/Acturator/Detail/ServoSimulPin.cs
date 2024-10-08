
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipSimulator.Acturator.Detail
{
    public class ServoSimulPin : ServoSimul2
    {

        public bool PIN_1ST_DOWN_POS_MOVE;
        public bool PIN_2ND_DOWN_POS_MOVE;
        public bool PIN_1ST_UP_POS_MOVE;
        public bool PIN_2ND_UP_POS_MOVE;

        public bool PIN_1ST_DOWN_POS_MOVE_END;
        public bool PIN_2ND_DOWN_POS_MOVE_END;
        public bool PIN_1ST_UP_POS_MOVE_END;
        public bool PIN_2ND_UP_POS_MOVE_END;


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
            PositionWoring(ref stepMove2, ref PIN_2ND_DOWN_POS_MOVE, ref   PIN_2ND_DOWN_POS_MOVE_END, 0, 5, false,"DOWN 2ND");
            PositionWoring(ref stepMove1, ref PIN_1ST_DOWN_POS_MOVE, ref   PIN_1ST_DOWN_POS_MOVE_END, 30, 5, false, "DOWN 1ST");
            PositionWoring(ref stepMove3, ref PIN_1ST_UP_POS_MOVE, ref   PIN_1ST_UP_POS_MOVE_END, 60, 5, false, "UP 1ST");
            PositionWoring(ref stepMove4, ref PIN_2ND_UP_POS_MOVE, ref   PIN_2ND_UP_POS_MOVE_END, 100, 5, false, "UP 2ND");
            base.MoveWoring();
        }
    }
}
