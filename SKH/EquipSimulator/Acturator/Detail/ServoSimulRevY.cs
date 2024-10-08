
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipSimulator.Acturator.Detail
{
    public class ServoSimulRevY : ServoSimul2
    {
        public bool REVIEW_WAIT_POS_MOVE;
        public bool REVIEW_REVIEW_POS_MOVE;  

        public bool REVIEW_WAIT_POS_MOVE_END;
        public bool REVIEW_REVIEW_POS_MOVE_END; 


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
            base.MoveWoring();
            base.PositionWoring(ref stepMove1, ref REVIEW_WAIT_POS_MOVE, ref   REVIEW_WAIT_POS_MOVE_END, 100, 10, false, "REVIEW WAIT");
            base.PositionWoring(ref stepMove2, ref REVIEW_REVIEW_POS_MOVE, ref   REVIEW_REVIEW_POS_MOVE_END, 200, 10, false, "REVIEW POSI");
            
        }
    }
}
