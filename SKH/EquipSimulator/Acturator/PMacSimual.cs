using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;

namespace EquipSimulator.Acturator
{
    public class PMacSimul
    {
        public PlcAddr YB_EquipMode { get; set; }
        public PlcAddr YB_CheckAlarmStatus { get; set; }
        public PlcAddr YB_UpperInterfaceWorking { get; set; }
        public PlcAddr YB_LowerInterfaceWorking { get; set; }
        public PlcAddr YB_EquipStatusOriginGlassInspecting { get; set; }
        public PlcAddr YB_EquipStatusRearGlassInspecting { get; set; }
        public PlcAddr YB_EquipStatusFrontGlassInspecting { get; set; }

        public PlcAddr XB_PmacReady { get; set; }
        public PlcAddr XB_PmacAlive { get; set; }
        public PlcAddr XB_PmacReviewGantryCrashCaution { get; set; }

        public PlcAddr YB_EquipStatusMotorInterlock { get; set; }
        public PlcAddr XB_EquipStatusMotorInterlockAck { get; set; }

        public PlcAddr YB_ReviewTimerOverCmd { get; set; }
        public PlcAddr XB_ReviewTimerOverCmdAck { get; set; }

        public PlcAddr YB_PmacResetCmd { get; set; }
        public PlcAddr XB_PmacResetCmdAck { get; set; }

        public PlcAddr YF_OriginGlsTriggerStartPosi { get; set; }
        public PlcAddr XF_OriginGlsTriggerStartPosiAck { get; set; }
        public PlcAddr YF_OriginGlsTriggerEndPosi { get; set; }
        public PlcAddr XF_OriginGlsTriggerEndPosiAck { get; set; }
        public PlcAddr YF_RearGlsTriggerStartPosi { get; set; }
        public PlcAddr XF_RearGlsTriggerStartPosiAck { get; set; }
        public PlcAddr YF_RearGlsTriggerEndPosi { get; set; }
        public PlcAddr XF_RearGlsTriggerEndPosiAck { get; set; }
        public PlcAddr YF_FrontGlsTriggerStartPosi { get; set; }
        public PlcAddr XF_FrontGlsTriggerStartPosiAck { get; set; }
        public PlcAddr YF_FrontGlsTriggerEndPosi { get; set; }
        public PlcAddr XF_FrontGlsTriggerEndPosiAck { get; set; }



        public void LogicWorking()
        {
            XB_PmacResetCmdAck.vBit = YB_PmacResetCmd.vBit;
            XB_EquipStatusMotorInterlockAck.vBit = YB_EquipStatusMotorInterlock.vBit;

            //XF_OriginGlsTriggerStartPosiAck.vFloat = YF_OriginGlsTriggerStartPosi.vFloat;
            //XF_OriginGlsTriggerEndPosiAck.vFloat = YF_OriginGlsTriggerEndPosi.vFloat;
            //XF_RearGlsTriggerStartPosiAck.vFloat = YF_RearGlsTriggerStartPosi.vFloat;
            //XF_RearGlsTriggerEndPosiAck.vFloat = YF_RearGlsTriggerEndPosi.vFloat;
            //XF_FrontGlsTriggerStartPosiAck.vFloat = YF_FrontGlsTriggerStartPosi.vFloat;
            //XF_FrontGlsTriggerEndPosiAck.vFloat = YF_FrontGlsTriggerEndPosi.vFloat;

        }

    }
}
