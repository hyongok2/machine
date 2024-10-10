using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EquipSimulator
{
    public class CimColor
    {
        // Color 상수
        public static Color CoDEFAULT;
        public static Color CoGREEN;
        public static Color CoYELLOW;
        public static Color CoRED;
        public static Color CoSKY;
        public static Color CoBLUE;
        public static Color CoVIOLET;
        public static Color CoWHITE;
        public static Color CoBLACK;
        public static Color CoGRAY;
        public static Color CoDGRAY;
        public static Color CoWATER;
        public static Color CoCONTROL;

        public static Color CoCon;   // Connection
        public static Color CoDis; // Disconnection

        // Remote / Local / Offline
        public static Color CoMCMD_REMOTE_B;
        public static Color CoMCMD_REMOTE_F;
        public static Color CoMCMD_LOCAL_B;
        public static Color CoMCMD_LOCAL_F;
        public static Color CoMCMD_OFFLINE_B;
        public static Color CoMCMD_OFFLINE_F;
        public static Color CoMCMD_DEFAULT_B;
        public static Color CoMCMD_DEFAULT_F;

        // EQUIPST ID (Equipment State)
        public static Color CoEQUIPST_NORMAL_B;    // 1
        public static Color CoEQUIPST_NORMAL_F;// 1
        public static Color CoEQUIPST_FAULT_B;// 2
        public static Color CoEQUIPST_FAULT_F; // 2
        public static Color CoEQUIPST_PM_B;// 3
        public static Color CoEQUIPST_PM_F;// 3
        public static Color CoEQUIPST_DEFAULT_B;
        public static Color CoEQUIPST_DEFAULT_F;

        //PROCST ID (Equipment Process State)
        public static Color CoPROCST_INIT_B;  // 1              // Initial
        public static Color CoPROCST_INIT_F;  // 1              // Initial
        public static Color CoPROCST_IDLE_B; // 2              // Idle
        public static Color CoPROCST_IDLE_F; // 2              // Idle
        public static Color CoPROCST_SETUP_B; // 3              // Setup
        public static Color CoPROCST_SETUP_F; // 3              // Setup
        public static Color CoPROCST_READY_B; // 4              // Ready
        public static Color CoPROCST_READY_F; // 4              // Ready
        public static Color CoPROCST_EXECUTE_B; // 5              // Executing
        public static Color CoPROCST_EXECUTE_F; // 5              // Executing
        public static Color CoPROCST_PAUSE_B; // 6              // Pause
        public static Color CoPROCST_PAUSE_F; // 6              // Pause
        public static Color CoPROCST_DEFAULT_B;
        public static Color CoPROCST_DEFAULT_F;

        // Port Status Color
        public static Color CoPORTST_EMPTY_B;    // 0
        public static Color CoPORTST_EMPTY_F;    // 0
        public static Color CoPORTST_IDLE_B;    // 1
        public static Color CoPORTST_IDLE_F;    // 1
        public static Color CoPORTST_WAIT_B;    // 2
        public static Color CoPORTST_WAIT_F;    // 2
        public static Color CoPORTST_RESERVE_B;    // 3
        public static Color CoPORTST_RESERVE_F;    // 3
        public static Color CoPORTST_BUSY_B;    // 4
        public static Color CoPORTST_BUSY_F;    // 4
        public static Color CoPORTST_COMPLETE_B;    // 5
        public static Color CoPORTST_COMPLETE_F;    // 5
        public static Color CoPORTST_ABORT_B;    // 6
        public static Color CoPORTST_ABORT_F;    // 6
        public static Color CoPORTST_CANCEL_B;    // 7
        public static Color CoPORTST_CANCEL_F;    // 7
        public static Color CoPORTST_PAUSE_B;    // 8
        public static Color CoPORTST_PAUSE_F;    // 8
        public static Color CoPORTST_DISABLE_B;    // 9
        public static Color CoPORTST_DISABLE_F;    // 9

        // Panel Status Color
        public static Color CoPNLST_EMPTY_B;    // 0
        public static Color CoPNLST_EMPTY_F;    // 0
        public static Color CoPNLST_IDLE_B;    // 1
        public static Color CoPNLST_IDLE_F;    // 1
        public static Color CoPNLST_STP_B;    // 2
        public static Color CoPNLST_STP_F;    // 2
        public static Color CoPNLST_PROCESSING_B;    // 3
        public static Color CoPNLST_PROCESSING_F;    // 3
        public static Color CoPNLST_DONE_B;    // 4
        public static Color CoPNLST_DONE_F;    // 4
        public static Color CoPNLST_ABORTING_B;    // 5
        public static Color CoPNLST_ABORTING_F;    // 5
        public static Color CoPNLST_ABORTED_B;    // 6
        public static Color CoPNLST_ABORTED_F;    // 6
        public static Color CoPNLST_CANCELED_B;    // 7
        public static Color CoPNLST_CANCELED_F;    // 7

        // On / Off Color
        public static Color CoON_B;
        public static Color CoON_F;
        public static Color CoOFF_B;
        public static Color CoOFF_F;

         
       
    }
}
