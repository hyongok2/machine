using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipSimulator.Acturator.Detail
{
    public class ServoSimulInspX : ServoSimul2
    {
        public bool SCAN_X1_AXIS_LOADING_POS_MOVE;
        public bool SCAN_X1_AXIS_BACK_POS_MOVE;
        public bool SCAN_X1_AXIS_FORWARD_POS_MOVE;
        public bool SCAN_X1_AXIS_VERIPANEL_START_POS_MOVE;
        public bool SCAN_X1_AXIS_VERIPANEL_END_POS_MOVE;

        public bool SCAN_X1_AXIS_LOADING_POS_MOVE_END;
        public bool SCAN_X1_AXIS_BACK_POS_MOVE_END;
        public bool SCAN_X1_AXIS_FORWARD_POS_MOVE_END;
        public bool SCAN_X1_AXIS_VERIPANEL_START_POS_MOVE_END;
        public bool SCAN_X1_AXIS_VERIPANEL_END_POS_MOVE_END;

        public int Cmd = 0;
        int stepMove1 = 0;
        int stepMove2 = 0;
        int stepMove3 = 0;
        int stepMove4 = 0;
        int stepMove5 = 0;

        protected override void MoveWoring()
        {
            PositionWoring(ref stepMove1, ref SCAN_X1_AXIS_LOADING_POS_MOVE, ref SCAN_X1_AXIS_LOADING_POS_MOVE_END, 100, 10, false, "로딩 위치");
            PositionWoring(ref stepMove2, ref SCAN_X1_AXIS_BACK_POS_MOVE, ref SCAN_X1_AXIS_BACK_POS_MOVE_END, 800, 10, false, "BACK 위치");
            PositionWoring(ref stepMove3, ref SCAN_X1_AXIS_FORWARD_POS_MOVE, ref SCAN_X1_AXIS_FORWARD_POS_MOVE_END, 300, 10, false, "FORWARD 위치");
            PositionWoring(ref stepMove4, ref SCAN_X1_AXIS_VERIPANEL_START_POS_MOVE, ref SCAN_X1_AXIS_VERIPANEL_START_POS_MOVE_END, 500, 10, false, "VERIPANEL 시작 위치");
            PositionWoring(ref stepMove5, ref SCAN_X1_AXIS_VERIPANEL_END_POS_MOVE, ref SCAN_X1_AXIS_VERIPANEL_END_POS_MOVE_END, 500, 10, false, "VERIPANEL 종료 위치");
            base.MoveWoring();
        }

    }
}
