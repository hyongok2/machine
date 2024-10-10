using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using EquipMainUi.Struct.Detail.HSMS;
using DitCim.PLC;
using DitCim.Lib;
using DitCim.Common;
using EquipMainUi.Struct.Detail;
using EquipMainUi.Struct.Detail.MCC;
using System.Threading.Tasks;

namespace EquipMainUi.Struct
{
    public enum MccActionCategory
    {
        INITIAL,
        LOADING,
        SCAN1,
        SCAN2,
        SCAN3,
        SCAN4,
        REVIEW,
        UNLOADING,
        NONE
    }
    public enum MccActionItem
    {
        NONE,

        //센터링 항목..
        CENTERING_ORIGN,

        CENTERING_FWD_PROCESS,
        CENTERING_BACK_PROCESS,



        FRONT_CENTERING_FWD_SOL,
        FRONT_CENTERING_BWD_SOL,
        SEP_CENTERING_UP_SOL,
        SEP_CENTERING_DOWN_SOL,
        SEP_LEFT_CENTERING_FWD_SOL,
        SEP_LEFT_CENTERING_BWD_SOL,
        SEP_RIGHT_CENTERING_FWD_SOL,
        SEP_RIGHT_CENTERING_BWD_SOL,
        REAR_CENTERING_FWD_SOL,
        REAR_CENTERING_BWD_SOL,
        SEP_REAR_CENTERING_FWD_SOL,
        SEP_REAR_CENTERING_BWD_SOL,
        STD_CENTERING_FWD_SOL,
        STD_CENTERING_BWD_SOL,
        SEP_STD_CENTERING_FWD_SOL,
        SEP_STD_CENTERING_BWD_SOL,
        ASSIST_CENTERING_FWD_SOL,
        ASSIST_CENTERING_BWD_SOL,
        SEP_ASSIST_CENTERING_FWD_SOL,
        SEP_ASSIST_CENTERING_BWD_SOL,

        //진공 항목
        VAC_ON,
        VAC_OFF,
        STD_VAC_1_SOL_ON,
        STD_VAC_2_SOL_ON,
        ASSIST_VAC_1_SOL_ON,
        ASSIST_VAC_2_SOL_ON,

        //BLOW 
        BLOW_ON,
        STD_BLOW_1_SOL_ON,
        STD_BLOW_2_SOL_ON,
        ASSIST_BLOW_1_SOL_ON,
        ASSIST_BLOW_2_SOL_ON,

        STAGE_X1_ORIGIN,
        STAGE_X1_P1_ORIGIN_1,
        STAGE_X1_P2_LOADING,
        STAGE_X1_P3_SCAN_START,
        STAGE_X1_P4_FWD_SCAN_END,
        STAGE_X1_P5_BWD_SCAN_END,
        STAGE_X1_P6_FWD_SCAN_END,
        STAGE_X1_P7_REVIEW_WAIT,
        STAGE_X1_P8_REVIEW_ALIGN,

        INSP_Y1_ORIGIN,
        INSP_Y1_P1_ORIGIN_1,
        INSP_Y1_P2_LOADING,
        INSP_Y1_P3_SCAN1,
        INSP_Y1_P4_SCAN2,
        INSP_Y1_P5_SCAN3,
        INSP_Y1_P6_SCAN4,

        REVI_X1_ORIGIN,
        REVI_X1_P1_ORIGIN_1,
        REVI_X1_P2_LOADING,
        REVI_X1_P3_REVIEW_ALIGN,
        REVI_X1_P4_REVIEW_WAIT,
        REVI_X1_POSITION5,
        REVI_X1_POSITION6,
        REVI_X1_POSITION7,

        REVI_X2_ORIGIN,
        REVI_X2_P1_ORIGIN_1,
        REVI_X2_P2_LOADING,
        REVI_X2_P3_REVIEW_ALIGN,
        REVI_X2_P4_REVIEW_WAIT,
        REVI_X2_POSITION5,
        REVI_X2_POSITION6,
        REVI_X2_POSITION7,

        REVI_Y1_ORIGIN,
        REVI_Y1_P1_ORIGIN_1,
        REVI_Y1_P2_LOADING,
        REVI_Y1_P3_REVIEW_ALIGN,
        REVI_Y1_P4_REVIEW_WAIT,
        REVI_Y1_POSITION5,
        REVI_Y1_POSITION6,
        REVI_Y1_POSITION7,

        REVI_Y2_ORIGIN,
        REVI_Y2_P1_ORIGIN_1,
        REVI_Y2_P2_LOADING,
        REVI_Y2_P3_REVIEW_ALIGN,
        REVI_Y2_P4_REVIEW_WAIT,
        REVI_Y2_POSITION5,
        REVI_Y2_POSITION6,
        REVI_Y2_POSITION7,

        INSP_Z_ALL_SCAN1,

        INSP_Z1_ORIGIN,
        INSP_Z1_P1_ORIGIN_1,
        INSP_Z1_P2_LOADING,
        INSP_Z1_P3_SCAN1,
        INSP_Z1_P4_SCAN2,
        INSP_Z1_P5_SCAN3,
        INSP_Z1_P6_SCAN4,

        INSP_Z2_ORIGIN,
        INSP_Z2_P1_ORIGIN_1,
        INSP_Z2_P2_LOADING,
        INSP_Z2_P3_SCAN1,
        INSP_Z2_P4_SCAN2,
        INSP_Z2_P5_SCAN3,
        INSP_Z2_P6_SCAN4,

        INSP_Z3_ORIGIN,
        INSP_Z3_P1_ORIGIN_1,
        INSP_Z3_P2_LOADING,
        INSP_Z3_P3_SCAN1,
        INSP_Z3_P4_SCAN2,
        INSP_Z3_P5_SCAN3,
        INSP_Z3_P6_SCAN4,

        INSP_Z4_ORIGIN,
        INSP_Z4_P1_ORIGIN_1,
        INSP_Z4_P2_LOADING,
        INSP_Z4_P3_SCAN1,
        INSP_Z4_P4_SCAN2,
        INSP_Z4_P5_SCAN3,
        INSP_Z4_P6_SCAN4,

        INSP_Z5_ORIGIN,
        INSP_Z5_P1_ORIGIN_1,
        INSP_Z5_P2_LOADING,
        INSP_Z5_P3_SCAN1,
        INSP_Z5_P4_SCAN2,
        INSP_Z5_P5_SCAN3,
        INSP_Z5_P6_SCAN4,

        INSP_Z6_ORIGIN,
        INSP_Z6_P1_ORIGIN_1,
        INSP_Z6_P2_LOADING,
        INSP_Z6_P3_SCAN1,
        INSP_Z6_P4_SCAN2,
        INSP_Z6_P5_SCAN3,
        INSP_Z6_P6_SCAN4,

        INSP_Z7_ORIGIN,
        INSP_Z7_P1_ORIGIN_1,
        INSP_Z7_P2_LOADING,
        INSP_Z7_P3_SCAN1,
        INSP_Z7_P4_SCAN2,
        INSP_Z7_P5_SCAN3,
        INSP_Z7_P6_SCAN4,

        INSP_Z8_ORIGIN,
        INSP_Z8_P1_ORIGIN_1,
        INSP_Z8_P2_LOADING,
        INSP_Z8_P3_SCAN1,
        INSP_Z8_P4_SCAN2,
        INSP_Z8_P5_SCAN3,
        INSP_Z8_P6_SCAN4,

        INSP_Z9_ORIGIN,
        INSP_Z9_P1_ORIGIN_1,
        INSP_Z9_P2_LOADING,
        INSP_Z9_P3_SCAN1,
        INSP_Z9_P4_SCAN2,
        INSP_Z9_P5_SCAN3,
        INSP_Z9_P6_SCAN4,

        INSP_Z10_ORIGIN,
        INSP_Z10_P1_ORIGIN_1,
        INSP_Z10_P2_LOADING,
        INSP_Z10_P3_SCAN1,
        INSP_Z10_P4_SCAN2,
        INSP_Z10_P5_SCAN3,
        INSP_Z10_P6_SCAN4,

        INSP_Z11_ORIGIN,
        INSP_Z11_P1_ORIGIN_1,
        INSP_Z11_P2_LOADING,
        INSP_Z11_P3_SCAN1,
        INSP_Z11_P4_SCAN2,
        INSP_Z11_P5_SCAN3,
        INSP_Z11_P6_SCAN4,

        LIFTPIN_ORIGIN,
        LIFTPIN_P1_ORIGIN_1,
        LIFTPIN_P2_DOWN_HI,
        LIFTPIN_P3_MID_LOW,
        LIFTPIN_P4_UP_HI,
        LIFTPIN_P5_MID_HI,
        LIFTPIN_P6_DOWN_LOW,
        LIFTPIN_P7_UP_LOW,

        INSP_THETA_ORIGIN,
        INSP_THETA_P1_ORIGIN_1,
        INSP_THETA_P2_LOADING,
        INSP_THETA_P3_SCAN1,
        INSP_THETA_P4_SCAN2,
        INSP_THETA_P5_SCAN3,
        INSP_THETA_P6_SCAN4,

        //
        UNLOADING_STEP_PROCESS,
        REVIEW_RUN,
        LOADING_STEP_PROCESS,
        INIT_STEP_PROCESS,
        READY_CYCLE,
        AUTO_CYCLE,
        GLASS_CHECK,
        REVIEW_ALIGN,

        //GLASS IN OUT 
        COMPONENT_IN,
        COMPONENT_OUT,

        SCAN_STEP_PROCESS,
        REVIEW_STEP_PROCESS,
        REVIEW_START,
        REVIEW_END,


    }
    public class MccIntervalOnOff
    {
        public DateTime OffTime;
        public PlcAddr Address;

        public bool Complete { get; set; }
    }

    public class MccPcProxy
    {
        private List<MccIntervalOnOff> _lstIntervalOnOff = new List<MccIntervalOnOff>(1000);

        public SortedList<MccActionCategory, SortedList<MccActionItem, int>> SlstMccItem = new SortedList<MccActionCategory, SortedList<MccActionItem, int>>();
        private MccActionCategory _currMccCategory = MccActionCategory.INITIAL;
        private MccActionCategory _prevMccCategory = MccActionCategory.INITIAL;

        //GLASS INFO



        public static PlcAddr GLASSID_POS_R   /*  */ = new PlcAddr(PlcMemType.S, 20250, 0x0, 30); 
        public static PlcAddr GLASSID_POS_F   /*  */ = new PlcAddr(PlcMemType.S, 20100, 0x0, 30);
        public static PlcAddr LOTID_POS     /*  */ = new PlcAddr(PlcMemType.S, 20160, 0x0, 30);
        public static PlcAddr PPID_POS      /*  */ = new PlcAddr(PlcMemType.S, 20190, 0x0, 30);
        public static PlcAddr STEPID_POS    /*  */ = new PlcAddr(PlcMemType.S, 20220, 0x0, 30);

        


        public static PlcAddr EQP_STATE         /*  */ = new PlcAddr(PlcMemType.S, 20300, 0x0, 2);
        public static PlcAddr OLD_EQP_STATE     /*  */ = new PlcAddr(PlcMemType.S, 20302, 0x0, 2);

        public static PlcAddr PROC_STATE        /*  */ = new PlcAddr(PlcMemType.S, 20304, 0x0, 2);
        public static PlcAddr OLD_PROC_STATE    /*  */ = new PlcAddr(PlcMemType.S, 20306, 0x0, 2);

        public static PlcAddr NEW_PPID      /*  */ = new PlcAddr(PlcMemType.S, 20310, 0x0, 50);
        public static PlcAddr OLD_PPID      /*  */ = new PlcAddr(PlcMemType.S, 20360, 0x0, 50);



        //ACTION ITEM START BIT
        public static PlcAddr ACTION_BIT_START  /*  */ = new PlcAddr(PlcMemType.S, 21000, 0x0);
                                                                                   
        //SIGNAL ITEM START BIT                                                    
        public static PlcAddr SIGNAL_BIT_START  /*  */ = new PlcAddr(PlcMemType.S, 21500, 0x0);
                                                                                   
        //EVENT ITEM START BIT                                                     
        public static PlcAddr EVENT_BIT_START   /*  */ = new PlcAddr(PlcMemType.S, 22000, 0x0);
                                                                                   
        //ALARM ITEM START BIT                                                     
        public static PlcAddr ALARM_BIT_START   /*  */ = new PlcAddr(PlcMemType.S, 22500, 0x0);
                                                                                   
        //INFORMATION ITEM START BIT                                               
        public static PlcAddr INFO_BIT_START    /*  */ = new PlcAddr(PlcMemType.S, 23000, 0x0);

        public MccPcProxy()
        {
            for (int iPos = 0; iPos < _lstIntervalOnOff.Capacity; iPos++)
                _lstIntervalOnOff.Add(new MccIntervalOnOff() { Address = null, Complete = true, OffTime = DateTime.Now });
        }


        public bool Initailize(IVirtualMem plc)
        {
            GLASSID_POS_F.PLC = plc;
            GLASSID_POS_R.PLC = plc;
            STEPID_POS.PLC = plc;
            LOTID_POS.PLC = plc;
            PPID_POS.PLC = plc;

            EQP_STATE.PLC = plc;
            OLD_EQP_STATE.PLC = plc;
            PROC_STATE.PLC = plc;
            OLD_PROC_STATE.PLC = plc;
            NEW_PPID.PLC = plc;
            OLD_PPID.PLC = plc;

            ACTION_BIT_START.PLC = plc;
            SIGNAL_BIT_START.PLC = plc;
            ALARM_BIT_START.PLC = plc;
            ACTION_BIT_START.PLC = plc;
            INFO_BIT_START.PLC = plc;

            int iPos = 0;

            SlstMccItem.Add(MccActionCategory.INITIAL, new SortedList<MccActionItem, int>());
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.INIT_STEP_PROCESS                /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.CENTERING_ORIGN                /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.REAR_CENTERING_BWD_SOL         /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.SEP_REAR_CENTERING_BWD_SOL         /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.ASSIST_CENTERING_BWD_SOL       /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.SEP_ASSIST_CENTERING_BWD_SOL       /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.FRONT_CENTERING_BWD_SOL        /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.SEP_LEFT_CENTERING_BWD_SOL    /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.SEP_RIGHT_CENTERING_BWD_SOL    /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.STD_CENTERING_BWD_SOL          /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.SEP_STD_CENTERING_BWD_SOL          /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.SEP_CENTERING_DOWN_SOL       /**/, iPos++);

            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.LIFTPIN_ORIGIN                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.GLASS_CHECK                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.INSP_Z1_ORIGIN                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.INSP_Z2_ORIGIN                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.INSP_Z3_ORIGIN                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.INSP_Z4_ORIGIN                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.INSP_Z5_ORIGIN                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.INSP_Z6_ORIGIN                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.INSP_Z7_ORIGIN                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.INSP_Z8_ORIGIN                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.INSP_Z9_ORIGIN                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.INSP_Z10_ORIGIN                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.INSP_Z11_ORIGIN                 /**/, iPos++);

            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.REVI_X1_ORIGIN                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.REVI_X2_ORIGIN                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.INSP_THETA_ORIGIN                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.STAGE_X1_ORIGIN                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.INSP_Y1_ORIGIN                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.REVI_Y1_ORIGIN                 /**/, iPos++);
            SlstMccItem[MccActionCategory.INITIAL].Add(MccActionItem.REVI_Y2_ORIGIN                 /**/, iPos++);

            // MCC ITEM OK

            SlstMccItem.Add(MccActionCategory.LOADING, new SortedList<MccActionItem, int>());
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.READY_CYCLE                    /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.LIFTPIN_P4_UP_HI              /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.INSP_Z1_P2_LOADING             /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.INSP_Z2_P2_LOADING             /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.INSP_Z3_P2_LOADING             /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.INSP_Z4_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.INSP_Z5_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.INSP_Z6_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.INSP_Z7_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.INSP_Z8_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.INSP_Z9_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.INSP_Z10_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.INSP_Z11_P2_LOADING              /**/, iPos++);

            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.REVI_X1_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.REVI_X2_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.INSP_THETA_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.STAGE_X1_P2_LOADING             /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.INSP_Y1_P2_LOADING             /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.REVI_Y1_P2_LOADING             /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.REVI_Y2_P2_LOADING             /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.GLASS_CHECK                    /**/, iPos++); // End of Ready Cycle.
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.LIFTPIN_P3_MID_LOW                    /**/, iPos++); // End of Ready Cycle.
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.COMPONENT_IN                   /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.AUTO_CYCLE                     /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.LIFTPIN_P2_DOWN_HI              /**/, iPos++); // centering pos
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.CENTERING_FWD_PROCESS          /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.SEP_CENTERING_UP_SOL               /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.SEP_LEFT_CENTERING_FWD_SOL         /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.SEP_RIGHT_CENTERING_FWD_SOL        /**/, iPos++);

            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.FRONT_CENTERING_FWD_SOL        /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.STD_CENTERING_FWD_SOL          /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.SEP_STD_CENTERING_FWD_SOL          /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.REAR_CENTERING_FWD_SOL         /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.SEP_REAR_CENTERING_FWD_SOL         /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.ASSIST_CENTERING_FWD_SOL       /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.SEP_ASSIST_CENTERING_FWD_SOL       /**/, iPos++);

            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.CENTERING_BACK_PROCESS         /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.SEP_CENTERING_DOWN_SOL             /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.SEP_LEFT_CENTERING_BWD_SOL         /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.SEP_RIGHT_CENTERING_BWD_SOL        /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.FRONT_CENTERING_BWD_SOL            /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.STD_CENTERING_BWD_SOL              /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.SEP_STD_CENTERING_BWD_SOL          /**/, iPos++);

            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.REAR_CENTERING_BWD_SOL         /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.SEP_REAR_CENTERING_BWD_SOL         /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.ASSIST_CENTERING_BWD_SOL       /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.SEP_ASSIST_CENTERING_BWD_SOL       /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.LIFTPIN_P6_DOWN_LOW                /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.VAC_ON                             /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.REVIEW_ALIGN                       /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.REVI_X1_P3_REVIEW_ALIGN            /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.REVI_X2_P3_REVIEW_ALIGN            /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.REVI_Y1_P3_REVIEW_ALIGN            /**/, iPos++);
            SlstMccItem[MccActionCategory.LOADING].Add(MccActionItem.REVI_Y2_P3_REVIEW_ALIGN            /**/, iPos++);                        

            SlstMccItem.Add(MccActionCategory.SCAN1, new SortedList<MccActionItem, int>());
            SlstMccItem[MccActionCategory.SCAN1].Add(MccActionItem.SCAN_STEP_PROCESS                /**/, iPos++);
            //SCAN1
            SlstMccItem[MccActionCategory.SCAN1].Add(MccActionItem.INSP_Z_ALL_SCAN1        /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN1].Add(MccActionItem.INSP_Z1_P3_SCAN1                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN1].Add(MccActionItem.INSP_Z2_P3_SCAN1                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN1].Add(MccActionItem.INSP_Z3_P3_SCAN1                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN1].Add(MccActionItem.INSP_Z4_P3_SCAN1                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN1].Add(MccActionItem.INSP_Z5_P3_SCAN1                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN1].Add(MccActionItem.INSP_Z6_P3_SCAN1                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN1].Add(MccActionItem.INSP_Z7_P3_SCAN1                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN1].Add(MccActionItem.INSP_Z8_P3_SCAN1                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN1].Add(MccActionItem.INSP_Z9_P3_SCAN1                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN1].Add(MccActionItem.INSP_Z10_P3_SCAN1                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN1].Add(MccActionItem.INSP_Z11_P3_SCAN1                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN1].Add(MccActionItem.INSP_Y1_P3_SCAN1                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN1].Add(MccActionItem.STAGE_X1_P3_SCAN_START          /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN1].Add(MccActionItem.STAGE_X1_P4_FWD_SCAN_END          /**/, iPos++);

            //SCAN2
            SlstMccItem.Add(MccActionCategory.SCAN2, new SortedList<MccActionItem, int>());
            SlstMccItem[MccActionCategory.SCAN2].Add(MccActionItem.INSP_Z1_P4_SCAN2                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN2].Add(MccActionItem.INSP_Z2_P4_SCAN2                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN2].Add(MccActionItem.INSP_Z3_P4_SCAN2                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN2].Add(MccActionItem.INSP_Z4_P4_SCAN2                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN2].Add(MccActionItem.INSP_Z5_P4_SCAN2                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN2].Add(MccActionItem.INSP_Z6_P4_SCAN2                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN2].Add(MccActionItem.INSP_Z7_P4_SCAN2                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN2].Add(MccActionItem.INSP_Z8_P4_SCAN2                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN2].Add(MccActionItem.INSP_Z9_P4_SCAN2                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN2].Add(MccActionItem.INSP_Z10_P4_SCAN2                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN2].Add(MccActionItem.INSP_Z11_P4_SCAN2                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN2].Add(MccActionItem.INSP_Y1_P4_SCAN2                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN2].Add(MccActionItem.STAGE_X1_P5_BWD_SCAN_END          /**/, iPos++);

            //SCAN3
            SlstMccItem.Add(MccActionCategory.SCAN3, new SortedList<MccActionItem, int>());
            SlstMccItem[MccActionCategory.SCAN3].Add(MccActionItem.INSP_Z1_P5_SCAN3                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN3].Add(MccActionItem.INSP_Z2_P5_SCAN3                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN3].Add(MccActionItem.INSP_Z3_P5_SCAN3                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN3].Add(MccActionItem.INSP_Z4_P5_SCAN3                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN3].Add(MccActionItem.INSP_Z5_P5_SCAN3                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN3].Add(MccActionItem.INSP_Z6_P5_SCAN3                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN3].Add(MccActionItem.INSP_Z7_P5_SCAN3                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN3].Add(MccActionItem.INSP_Z8_P5_SCAN3                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN3].Add(MccActionItem.INSP_Z9_P5_SCAN3                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN3].Add(MccActionItem.INSP_Z10_P5_SCAN3                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN3].Add(MccActionItem.INSP_Z11_P5_SCAN3                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN3].Add(MccActionItem.INSP_Y1_P5_SCAN3                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN3].Add(MccActionItem.STAGE_X1_P4_FWD_SCAN_END          /**/, iPos++);

            //SCAN4
            SlstMccItem.Add(MccActionCategory.SCAN4, new SortedList<MccActionItem, int>());
            SlstMccItem[MccActionCategory.SCAN4].Add(MccActionItem.INSP_Z1_P6_SCAN4                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN4].Add(MccActionItem.INSP_Z2_P6_SCAN4                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN4].Add(MccActionItem.INSP_Z3_P6_SCAN4                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN4].Add(MccActionItem.INSP_Z4_P6_SCAN4                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN4].Add(MccActionItem.INSP_Z5_P6_SCAN4                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN4].Add(MccActionItem.INSP_Z6_P6_SCAN4                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN4].Add(MccActionItem.INSP_Z7_P6_SCAN4                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN4].Add(MccActionItem.INSP_Z8_P6_SCAN4                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN4].Add(MccActionItem.INSP_Z9_P6_SCAN4                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN4].Add(MccActionItem.INSP_Z10_P6_SCAN4                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN4].Add(MccActionItem.INSP_Z11_P6_SCAN4                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN4].Add(MccActionItem.INSP_Y1_P6_SCAN4                 /**/, iPos++);
            SlstMccItem[MccActionCategory.SCAN4].Add(MccActionItem.STAGE_X1_P5_BWD_SCAN_END        /**/, iPos++);

            SlstMccItem.Add(MccActionCategory.REVIEW, new SortedList<MccActionItem, int>());
            SlstMccItem[MccActionCategory.REVIEW].Add(MccActionItem.INSP_Y1_P2_LOADING             /**/, iPos++);
            SlstMccItem[MccActionCategory.REVIEW].Add(MccActionItem.STAGE_X1_P7_REVIEW_WAIT             /**/, iPos++);
            SlstMccItem[MccActionCategory.REVIEW].Add(MccActionItem.REVI_X1_P3_REVIEW_ALIGN             /**/, iPos++);
            SlstMccItem[MccActionCategory.REVIEW].Add(MccActionItem.REVI_X2_P3_REVIEW_ALIGN             /**/, iPos++);
            SlstMccItem[MccActionCategory.REVIEW].Add(MccActionItem.REVI_Y1_P3_REVIEW_ALIGN             /**/, iPos++);            
            SlstMccItem[MccActionCategory.REVIEW].Add(MccActionItem.REVI_Y2_P3_REVIEW_ALIGN             /**/, iPos++);
            SlstMccItem[MccActionCategory.REVIEW].Add(MccActionItem.REVIEW_STEP_PROCESS             /**/, iPos++);
            SlstMccItem[MccActionCategory.REVIEW].Add(MccActionItem.REVIEW_RUN                      /**/, iPos++);

            SlstMccItem.Add(MccActionCategory.UNLOADING, new SortedList<MccActionItem, int>());
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.LIFTPIN_P6_DOWN_LOW             /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.INSP_Z1_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.INSP_Z2_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.INSP_Z3_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.INSP_Z4_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.INSP_Z5_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.INSP_Z6_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.INSP_Z7_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.INSP_Z8_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.INSP_Z9_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.INSP_Z10_P2_LOADING             /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.INSP_Z11_P2_LOADING             /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.REVI_X1_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.REVI_X2_P2_LOADING              /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.INSP_THETA_P2_LOADING           /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.STAGE_X1_P2_LOADING             /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.INSP_Y1_P2_LOADING             /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.REVI_Y1_P2_LOADING             /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.REVI_Y2_P2_LOADING             /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.VAC_OFF                        /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.LIFTPIN_P3_MID_LOW             /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.LIFTPIN_P4_UP_HI               /**/, iPos++);
            SlstMccItem[MccActionCategory.UNLOADING].Add(MccActionItem.COMPONENT_OUT             /**/, iPos++);
            return true;
        }

        public void LogicWorking(Equipment equip)
        {
            CheckIntervalOn();

            if (equip.IsUseInternalTestMode)
                return;

            //LogicWorkingSignal(equip);
            //LogicWorkingInfomation(equip);
            LogicWorkingAlarm(equip);
        }

        //private void LogicWorkingSignal(Equipment equip)
        //{
        //    int iPos = 0;
        //    SetMccSignal(iPos++, equip.EmsOutside1.IsOn);
        //    SetMccSignal(iPos++, equip.EmsOutside2.IsOn);
        //    SetMccSignal(iPos++, equip.EmsOutside3.IsOn);
        //    SetMccSignal(iPos++, equip.EmsOutside4.IsOn);
        //    SetMccSignal(iPos++, equip.EmsInside1.IsOn);
        //    SetMccSignal(iPos++, equip.KeyAutoTeachInner.IsOn);
        //    SetMccSignal(iPos++, equip.ModeSelectKey.IsAuto);
        //    SetMccSignal(iPos++, equip.SafeyPlcError.IsOn);
        //    SetMccSignal(iPos++, equip.EnableGripSwOn1.IsOn);
        //    SetMccSignal(iPos++, equip.EnableGripSwOn2.IsOn);
        //    SetMccSignal(iPos++, equip.EnableGripSwOn3.IsOn);
        //    SetMccSignal(iPos++, equip.TopDoor01.IsOnOff);
        //    SetMccSignal(iPos++, equip.TopDoor02.IsOnOff);
        //    SetMccSignal(iPos++, equip.TopDoor03.IsOnOff);
        //    SetMccSignal(iPos++, equip.BotDoor01.IsOnOff);
        //    SetMccSignal(iPos++, equip.BotDoor02.IsOnOff);
        //    SetMccSignal(iPos++, equip.BotDoor03.IsOnOff);
        //    SetMccSignal(iPos++, equip.BotDoor04.IsOnOff);
        //    SetMccSignal(iPos++, equip.BotDoor05.IsOnOff);
        //    SetMccSignal(iPos++, equip.BotDoor07.IsOnOff);
        //    SetMccSignal(iPos++, equip.BotDoor08.IsOnOff);
        //    SetMccSignal(iPos++, equip.GlassCrackDefectSensor1.IsOn);
        //    SetMccSignal(iPos++, equip.GlassCrackDefectSensor2.IsOn);
        //    SetMccSignal(iPos++, equip.GlassCrackDefectSensor3.IsOn);
        //    SetMccSignal(iPos++, equip.GlassCrackDefectSensor4.IsOn);
        //    SetMccSignal(iPos++, equip.GlassCrackDefectSensor5.IsOn);
        //    SetMccSignal(iPos++, equip.GlassCrackDefectSensor6.IsOn);
        //    SetMccSignal(iPos++, equip.GlassCrackDefectSensor7.IsOn);
        //    SetMccSignal(iPos++, equip.GlassCrackDefectSensor8.IsOn);
        //    SetMccSignal(iPos++, equip.GlassCrackDefectSensor9.IsOn);
        //    SetMccSignal(iPos++, equip.GlassCrackDefectSensor10.IsOn);
        //    SetMccSignal(iPos++, equip.GlassCheckSensor1.IsOn);
        //    SetMccSignal(iPos++, equip.GlassCheckSensor2.IsOn);
        //    SetMccSignal(iPos++, equip.GlassCheckSensor3.IsOn);
        //    SetMccSignal(iPos++, equip.RobotArmDefect.IsOn);
        //    SetMccSignal(iPos++, equip.IsolatorCheck1.IsOn);
        //    SetMccSignal(iPos++, equip.IsolatorCheck2.IsOn);
        //    SetMccSignal(iPos++, equip.IsolatorCheck3.IsOn);
        //    SetMccSignal(iPos++, equip.IsolatorCheck4.IsOn);
        //    SetMccSignal(iPos++, equip.IsolatorCheck5.IsOn);
        //    SetMccSignal(iPos++, equip.IsolatorCheck6.IsOn);
        //    SetMccSignal(iPos++, equip.FrontCentering.XB_ForwardComplete[0]);
        //    SetMccSignal(iPos++, equip.FrontCentering.XB_BackwardComplete[0]);
        //    SetMccSignal(iPos++, equip.FrontCentering.XB_ForwardComplete[1]);
        //    SetMccSignal(iPos++, equip.FrontCentering.XB_BackwardComplete[1]);
        //    SetMccSignal(iPos++, equip.RearCentering.XB_ForwardComplete[0]);
        //    SetMccSignal(iPos++, equip.RearCentering.XB_BackwardComplete[0]);
        //    SetMccSignal(iPos++, equip.RearCentering.XB_ForwardComplete[1]);
        //    SetMccSignal(iPos++, equip.RearCentering.XB_BackwardComplete[1]);
        //    SetMccSignal(iPos++, equip.SepRearCentering.XB_ForwardComplete[0]);
        //    SetMccSignal(iPos++, equip.SepRearCentering.XB_BackwardComplete[0]);
        //    SetMccSignal(iPos++, equip.SepRearCentering.XB_ForwardComplete[1]);
        //    SetMccSignal(iPos++, equip.SepRearCentering.XB_BackwardComplete[1]);
        //    SetMccSignal(iPos++, equip.StandCentering.XB_ForwardComplete[0]);
        //    SetMccSignal(iPos++, equip.StandCentering.XB_BackwardComplete[0]);
        //    SetMccSignal(iPos++, equip.StandCentering.XB_ForwardComplete[1]);
        //    SetMccSignal(iPos++, equip.StandCentering.XB_BackwardComplete[1]);
        //    SetMccSignal(iPos++, equip.SepStandCentering.XB_ForwardComplete[0]);
        //    SetMccSignal(iPos++, equip.SepStandCentering.XB_BackwardComplete[0]);
        //    SetMccSignal(iPos++, equip.SepStandCentering.XB_ForwardComplete[1]);
        //    SetMccSignal(iPos++, equip.SepStandCentering.XB_BackwardComplete[1]);
        //    SetMccSignal(iPos++, equip.AssistCentering.XB_ForwardComplete[0]);
        //    SetMccSignal(iPos++, equip.AssistCentering.XB_BackwardComplete[0]);
        //    SetMccSignal(iPos++, equip.AssistCentering.XB_ForwardComplete[1]);
        //    SetMccSignal(iPos++, equip.AssistCentering.XB_BackwardComplete[1]);
        //    SetMccSignal(iPos++, equip.SepAssistCentering.XB_ForwardComplete[0]);
        //    SetMccSignal(iPos++, equip.SepAssistCentering.XB_BackwardComplete[0]);
        //    SetMccSignal(iPos++, equip.SepAssistCentering.XB_ForwardComplete[1]);
        //    SetMccSignal(iPos++, equip.SepAssistCentering.XB_BackwardComplete[1]);
        //    SetMccSignal(iPos++, equip.SepCenteringUd.XB_ForwardComplete[0]);
        //    SetMccSignal(iPos++, equip.SepCenteringUd.XB_BackwardComplete[0]);
        //    SetMccSignal(iPos++, equip.SepCenteringUd.XB_ForwardComplete[1]);
        //    SetMccSignal(iPos++, equip.SepCenteringUd.XB_BackwardComplete[1]);
        //    SetMccSignal(iPos++, equip.SepRightCentering.XB_ForwardComplete[0]);
        //    SetMccSignal(iPos++, equip.SepRightCentering.XB_BackwardComplete[0]);
        //    SetMccSignal(iPos++, equip.SepRightCentering.XB_ForwardComplete[1]);
        //    SetMccSignal(iPos++, equip.SepRightCentering.XB_BackwardComplete[1]);
        //    SetMccSignal(iPos++, equip.SepLeftCentering.XB_ForwardComplete[0]);
        //    SetMccSignal(iPos++, equip.SepLeftCentering.XB_BackwardComplete[0]);
        //    SetMccSignal(iPos++, equip.SepLeftCentering.XB_ForwardComplete[1]);
        //    SetMccSignal(iPos++, equip.SepLeftCentering.XB_BackwardComplete[1]);
        //    SetMccSignal(iPos++, equip.StageVacuumCtrl1.IsOnOff);
        //    SetMccSignal(iPos++, equip.StageVacuumCtrl2.IsOnOff);
        //    SetMccSignal(iPos++, equip.StageVacuumCtrl3.IsOnOff);
        //    SetMccSignal(iPos++, equip.StageVacuumCtrl4.IsOnOff);
        //    SetMccSignal(iPos++, equip.StageVacuumCtrl5.IsOnOff);
        //    SetMccSignal(iPos++, equip.MainAir1.IsOn);
        //    SetMccSignal(iPos++, equip.MainAir2.IsOn);
        //    SetMccSignal(iPos++, equip.MainAir3.IsOn);
        //    SetMccSignal(iPos++, equip.MainVacuum1.IsOn);
        //    SetMccSignal(iPos++, equip.MainVacuum2.IsOn);
        //    SetMccSignal(iPos++, equip.SafetyCircuitReset.IsSolOnOff);
        //    SetMccSignal(iPos++, equip.TopDoor01.IsSolOnOff);
        //    SetMccSignal(iPos++, equip.TopDoor02.IsSolOnOff);
        //    SetMccSignal(iPos++, equip.TopDoor03.IsSolOnOff);
        //    SetMccSignal(iPos++, equip.BotDoor01.IsSolOnOff);
        //    SetMccSignal(iPos++, equip.BotDoor02.IsSolOnOff);
        //    SetMccSignal(iPos++, equip.BotDoor03.IsSolOnOff);
        //    SetMccSignal(iPos++, equip.BotDoor04.IsSolOnOff);
        //    SetMccSignal(iPos++, equip.BotDoor05.IsSolOnOff);            
        //    SetMccSignal(iPos++, equip.BotDoor07.IsSolOnOff);
        //    SetMccSignal(iPos++, equip.BotDoor08.IsSolOnOff);
        //    SetMccSignal(iPos++, equip.FrontCentering.YB_ForwardCmd);
        //    SetMccSignal(iPos++, equip.FrontCentering.YB_BackwardCmd);
        //    SetMccSignal(iPos++, equip.RearCentering.YB_ForwardCmd);
        //    SetMccSignal(iPos++, equip.RearCentering.YB_BackwardCmd);
        //    SetMccSignal(iPos++, equip.SepRearCentering.YB_ForwardCmd);
        //    SetMccSignal(iPos++, equip.SepRearCentering.YB_BackwardCmd);
        //    SetMccSignal(iPos++, equip.StandCentering.YB_ForwardCmd);
        //    SetMccSignal(iPos++, equip.StandCentering.YB_BackwardCmd);
        //    SetMccSignal(iPos++, equip.SepStandCentering.YB_ForwardCmd);
        //    SetMccSignal(iPos++, equip.SepStandCentering.YB_BackwardCmd);
        //    SetMccSignal(iPos++, equip.AssistCentering.YB_ForwardCmd);
        //    SetMccSignal(iPos++, equip.AssistCentering.YB_BackwardCmd);
        //    SetMccSignal(iPos++, equip.SepAssistCentering.YB_ForwardCmd);
        //    SetMccSignal(iPos++, equip.SepAssistCentering.YB_BackwardCmd);
        //    SetMccSignal(iPos++, equip.PinIonizer.YB_RemoteOn[0]);
        //    SetMccSignal(iPos++, equip.PinIonizer.YB_RemoteOn[1]);
        //    SetMccSignal(iPos++, equip.PinIonizer.YB_RemoteOn[2]);
        //    SetMccSignal(iPos++, equip.PinIonizer.YB_RemoteOn[3]);
        //    SetMccSignal(iPos++, equip.PinIonizer.YB_AirOn[0]);
        //    SetMccSignal(iPos++, equip.PinIonizer.YB_AirOn[1]);
        //    SetMccSignal(iPos++, equip.SepCenteringUd.YB_ForwardCmd);
        //    SetMccSignal(iPos++, equip.SepCenteringUd.YB_BackwardCmd);
        //    SetMccSignal(iPos++, equip.SepRightCentering.YB_ForwardCmd);
        //    SetMccSignal(iPos++, equip.SepRightCentering.YB_BackwardCmd);
        //    SetMccSignal(iPos++, equip.SepLeftCentering.YB_ForwardCmd);
        //    SetMccSignal(iPos++, equip.SepLeftCentering.YB_BackwardCmd);
        //    SetMccSignal(iPos++, equip.StageVacuumCtrl1.IsSolOnOff);
        //    SetMccSignal(iPos++, equip.StageVacuumCtrl2.IsSolOnOff);
        //    SetMccSignal(iPos++, equip.StageVacuumCtrl3.IsSolOnOff);
        //    SetMccSignal(iPos++, equip.StageVacuumCtrl4.IsSolOnOff);
        //    SetMccSignal(iPos++, equip.StageVacuumCtrl5.IsSolOnOff);
        //    SetMccSignal(iPos++, equip.StageBlowerSol1.YB_OnOff);
        //    SetMccSignal(iPos++, equip.StageBlowerSol2.YB_OnOff);
        //    SetMccSignal(iPos++, equip.StageBlowerSol3.YB_OnOff);
        //    SetMccSignal(iPos++, equip.StageBlowerSol4.YB_OnOff);
        //    SetMccSignal(iPos++, equip.StageBlowerSol5.YB_OnOff);
        //    SetMccSignal(iPos++, CIMAB.LOSendAble.vBit);        //X_SEND_ABLE
        //    SetMccSignal(iPos++, CIMAB.LOSendStart.vBit);       //X_SEND_START
        //    SetMccSignal(iPos++, CIMAB.LOSendComplete.vBit);    //X_SEND_COMPLETE
        //    SetMccSignal(iPos++, CIMAB.UPReceiveAble.vBit);     //X_RCV_ABLE
        //    SetMccSignal(iPos++, CIMAB.UPReceiveStart.vBit);    //X_RCV_START
        //    SetMccSignal(iPos++, CIMAB.UPReceiveComplete.vBit); //X_RCV_COMPLETE
        //    SetMccSignal(iPos++, AOIB.LOSendAble.vBit);         //Y_RCV_ABLE
        //    SetMccSignal(iPos++, AOIB.LOSendStart.vBit);        //Y_RCV_START
        //    SetMccSignal(iPos++, AOIB.LOSendComplete.vBit);     //Y_RCV_COMPLETE
        //    SetMccSignal(iPos++, AOIB.UPReceiveAble.vBit);      //Y_SEND_ABLE
        //    SetMccSignal(iPos++, AOIB.UPReceiveStart.vBit);     //Y_SEND_START
        //    SetMccSignal(iPos++, AOIB.UPReceiveComplete.vBit);  //Y_SEND_COMPLETE

        //}
        //private void LogicWorkingInfomation(Equipment equip)
        //{
        //    int iPos = 0;
        //    SetMccInfomation(iPos++, (int)(equip.StageX.Setting.POSITION4_SPEED * 1000));     //SET_INSPECT_X1_SPD
        //    SetMccInfomation(iPos++, (int)(equip.StageX.XF_CurrMotorSpeed.vFloat * 1000));     //CUR_INSPECT_X1_SPD
        //    SetMccInfomation(iPos++, (int)(equip.StageX.XF_CurrMotorPosition.vFloat * 1000));  //CUR_INSPECT_X1_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageX.Setting.POSITION1 * 1000));            //SET_INSPECT_X1_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageX.Setting.POSITION2 * 1000));            //SET_INSPECT_X1_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageX.Setting.POSITION3 * 1000));            //SET_INSPECT_X1_SCAN_START_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageX.Setting.POSITION4 * 1000));            //SET_INSPECT_X1_FWD_SCAN_END_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageX.Setting.POSITION5 * 1000));            //SET_INSPECT_X1_BACK_SCAN_END_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageX.Setting.POSITION9 * 1000));            //SET_INSPECT_X1_RIGHT_GLASS_SCAN_START_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageX.Setting.POSITION10 * 1000));            //SET_INSPECT_X1_RIGHT_GLASS_FWD_SCAN_END_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageX.Setting.POSITION11 * 1000));             //SET_INSPECT_X1_RIGHT_GLASS_BACK_SCAN_END_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageX.Setting.POSITION6 * 1000));             //SET_INSPECT_X1_LEFT_GLASS_SCAN_START_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageX.Setting.POSITION7 * 1000));             //SET_INSPECT_X1_LEFT_GLASS_FWD_SCAN_END_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageX.Setting.POSITION8 * 1000));             //SET_INSPECT_X1_LEFT_GLASS_BACK_SCAN_END_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageX.Setting.POSITION14 * 1000));             //SET_INSPECT_X1_REVIEW_WAIT_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageX.XF_CurrMotorStress.vInt * 1000));       //MAX_LOAD_RATE_INSPECT_X1
        //    SetMccInfomation(iPos++, (int)(equip.StageY.Setting.POSITION4_SPEED * 1000));        //SET_INSPECT_Y_SPD
        //    SetMccInfomation(iPos++, (int)(equip.StageY.XF_CurrMotorSpeed.vFloat * 1000));       //CUR_INSPECT_Y_SPD
        //    SetMccInfomation(iPos++, (int)(equip.StageY.XF_CurrMotorPosition.vFloat * 1000));    //CUR_INSPECT_Y_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageY.Setting.POSITION1 * 1000));              //SET_INSPECT_Y_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageY.Setting.POSITION2 * 1000));              //SET_INSPECT_Y_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageY.Setting.POSITION3 * 1000));              //SET_INSPECT_Y_SCAN1_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageY.Setting.POSITION4 * 1000));              //SET_INSPECT_Y_SCAN2_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageY.Setting.POSITION5 * 1000));              //SET_INSPECT_Y_SCAN3_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageY.Setting.POSITION6 * 1000));              //SET_INSPECT_Y_SCAN4_POS
        //    SetMccInfomation(iPos++, (int)(equip.StageY.XF_CurrMotorStress.vFloat * 1000));              //MAX_LOAD_RATE_INSPECT_Y
        //    SetMccInfomation(iPos++, (int)(equip.ReviX1.XF_CurrMotorSpeed.vFloat * 1000));      //CUR_REVIEW_X1_SPD
        //    SetMccInfomation(iPos++, (int)(equip.ReviX1.XF_CurrMotorPosition.vFloat * 1000));   //CUR_REVIEW_X1_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviX1.Setting.POSITION1 * 1000));             //SET_REVIEW_X1_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviX1.Setting.POSITION2 * 1000));             //SET_REVIEW_X1_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviX1.Setting.POSITION6 * 1000));             //SET_REVIEW_X1_WAIT_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviX1.Setting.POSITION3 * 1000));             //SET_REVIEW_X1_ORIGIN_GLASS_ALIGN_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviX1.Setting.POSITION5 * 1000));             //SET_REVIEW_X1_RIGHT_GLASS_ALIGN_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviX1.Setting.POSITION4 * 1000));             //SET_REVIEW_X1_LEFT_GLASS_ALIGN_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviX1.XF_CurrMotorStress.vFloat * 1000));     //MAX_LOAD_RATE_REVIEW_X1
        //    SetMccInfomation(iPos++, (int)(equip.ReviX2.XF_CurrMotorSpeed.vFloat * 1000));      //CUR_REVIEW_X2_SPD
        //    SetMccInfomation(iPos++, (int)(equip.ReviX2.XF_CurrMotorPosition.vFloat * 1000));   //CUR_REVIEW_X2_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviX2.Setting.POSITION1 * 1000));             //SET_REVIEW_X2_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviX2.Setting.POSITION2 * 1000));             //SET_REVIEW_X2_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviX2.Setting.POSITION6 * 1000));             //SET_REVIEW_X2_WAIT_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviX2.Setting.POSITION3 * 1000));             //SET_REVIEW_X2_ORIGIN_GLASS_ALIGN_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviX2.Setting.POSITION5 * 1000));             //SET_REVIEW_X2_RIGHT_GLASS_ALIGN_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviX2.Setting.POSITION4 * 1000));             //SET_REVIEW_X2_LEFT_GLASS_ALIGN_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviX2.XF_CurrMotorStress.vFloat * 1000));     //MAX_LOAD_RATE_REVIEW_X2

        //    SetMccInfomation(iPos++, (int)(equip.Y2.XF_CurrMotorSpeed.vFloat * 1000));      //CUR_REVIEW_Y1_SPD
        //    SetMccInfomation(iPos++, (int)(equip.Y2.XF_CurrMotorPosition.vFloat * 1000));   //CUR_REVIEW_Y1_POS
        //    SetMccInfomation(iPos++, (int)(equip.Y2.Setting.POSITION1 * 1000));             //SET_REVIEW_Y1_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.Y2.Setting.POSITION2 * 1000));             //SET_REVIEW_Y1_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.Y2.Setting.POSITION6 * 1000));             //SET_REVIEW_Y1_WAIT_POS
        //    SetMccInfomation(iPos++, (int)(equip.Y2.Setting.POSITION3 * 1000));             //SET_REVIEW_Y1_ORIGIN_GLASS_ALIGN_POS
        //    SetMccInfomation(iPos++, (int)(equip.Y2.Setting.POSITION5 * 1000));             //SET_REVIEW_Y1_RIGHT_GLASS_ALIGN_POS
        //    SetMccInfomation(iPos++, (int)(equip.Y2.Setting.POSITION4 * 1000));             //SET_REVIEW_Y1_LEFT_GLASS_ALIGN_POS
        //    SetMccInfomation(iPos++, (int)(equip.Y2.XF_CurrMotorStress.vFloat * 1000));     //MAX_LOAD_RATE_REVIEW_Y1
        //    SetMccInfomation(iPos++, (int)(equip.ReviY2.XF_CurrMotorSpeed.vFloat * 1000));      //CUR_REVIEW_Y2_SPD
        //    SetMccInfomation(iPos++, (int)(equip.ReviY2.XF_CurrMotorPosition.vFloat * 1000));   //CUR_REVIEW_Y2_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviY2.Setting.POSITION1 * 1000));             //SET_REVIEW_Y2_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviY2.Setting.POSITION2 * 1000));             //SET_REVIEW_Y2_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviY2.Setting.POSITION6 * 1000));             //SET_REVIEW_Y2_WAIT_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviY2.Setting.POSITION3 * 1000));             //SET_REVIEW_Y2_ORIGIN_GLASS_ALIGN_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviY2.Setting.POSITION5 * 1000));             //SET_REVIEW_Y2_RIGHT_GLASS_ALIGN_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviY2.Setting.POSITION4 * 1000));             //SET_REVIEW_Y2_LEFT_GLASS_ALIGN_POS
        //    SetMccInfomation(iPos++, (int)(equip.ReviY2.XF_CurrMotorStress.vFloat * 1000));     //MAX_LOAD_RATE_REVIEW_Y2
        //    SetMccInfomation(iPos++, (int)(equip.AlignTheta.XF_CurrMotorSpeed.vFloat * 1000));       //CUR_THETA_SPD
        //    SetMccInfomation(iPos++, (int)(equip.AlignTheta.XF_CurrMotorPosition.vFloat * 1000));    //CUR_THETA_POS
        //    SetMccInfomation(iPos++, (int)(equip.AlignTheta.Setting.POSITION1 * 1000));              //SET_THETA_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.AlignTheta.Setting.POSITION2 * 1000));              //SET_THETA_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.AlignTheta.Setting.POSITION3 * 1000));              //SET_THETA_WAIT_POS
        //    SetMccInfomation(iPos++, (int)(equip.AlignTheta.XF_CurrMotorStress.vFloat * 1000));      //MAX_LOAD_RATE_THETA

        //    SetMccInfomation(iPos++, (int)(equip.WsiZ.Setting.POSITION2_SPEED * 1000));       //SET_INSPECT_Z1_SPD
        //    SetMccInfomation(iPos++, (int)(equip.WsiZ.XF_CurrMotorSpeed.vFloat * 1000));      //CUR_INSPECT_Z1_SPD
        //    SetMccInfomation(iPos++, (int)(equip.WsiZ.XF_CurrMotorPosition.vFloat * 1000));   //CUR_INSPECT_Z1_POS
        //    SetMccInfomation(iPos++, (int)(equip.WsiZ.Setting.POSITION1 * 1000));             //SET_INSPECT_Z1_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.WsiZ.Setting.POSITION2 * 1000));             //SET_INSPECT_Z1_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.WsiZ.Setting.POSITION3 * 1000));             //SET_INSPECT_Z1_SCAN1_POS
        //    SetMccInfomation(iPos++, (int)(equip.WsiZ.Setting.POSITION4 * 1000));             //SET_INSPECT_Z1_SCAN2_POS
        //    SetMccInfomation(iPos++, (int)(equip.WsiZ.Setting.POSITION5 * 1000));             //SET_INSPECT_Z1_SCAN3_POS
        //    SetMccInfomation(iPos++, (int)(equip.WsiZ.Setting.POSITION6 * 1000));             //SET_INSPECT_Z1_SCAN4_POS
        //    SetMccInfomation(iPos++, (int)(equip.WsiZ.XF_CurrMotorStress.vFloat * 1000));     //MAX_LOAD_RATE_INSPECT_Z1

        //    SetMccInfomation(iPos++, (int)(equip.InspZ2.Setting.POSITION2_SPEED * 1000));       //SET_INSPECT_Z2_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ2.XF_CurrMotorSpeed.vFloat * 1000));      //CUR_INSPECT_Z2_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ2.XF_CurrMotorPosition.vFloat * 1000));   //CUR_INSPECT_Z2_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ2.Setting.POSITION1 * 1000));             //SET_INSPECT_Z2_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ2.Setting.POSITION2 * 1000));             //SET_INSPECT_Z2_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ2.Setting.POSITION3 * 1000));             //SET_INSPECT_Z2_SCAN1_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ2.Setting.POSITION4 * 1000));             //SET_INSPECT_Z2_SCAN2_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ2.Setting.POSITION5 * 1000));             //SET_INSPECT_Z2_SCAN3_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ2.Setting.POSITION6 * 1000));             //SET_INSPECT_Z2_SCAN4_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ2.XF_CurrMotorStress.vFloat * 1000));     //MAX_LOAD_RATE_INSPECT_Z2

        //    SetMccInfomation(iPos++, (int)(equip.InspZ3.Setting.POSITION2_SPEED * 1000));       //SET_INSPECT_Z3_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ3.XF_CurrMotorSpeed.vFloat * 1000));      //CUR_INSPECT_Z3_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ3.XF_CurrMotorPosition.vFloat * 1000));   //CUR_INSPECT_Z3_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ3.Setting.POSITION1 * 1000));             //SET_INSPECT_Z3_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ3.Setting.POSITION2 * 1000));             //SET_INSPECT_Z3_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ3.Setting.POSITION3 * 1000));             //SET_INSPECT_Z3_SCAN1_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ3.Setting.POSITION4 * 1000));             //SET_INSPECT_Z3_SCAN2_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ3.Setting.POSITION5 * 1000));             //SET_INSPECT_Z3_SCAN3_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ3.Setting.POSITION6 * 1000));             //SET_INSPECT_Z3_SCAN4_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ3.XF_CurrMotorStress.vFloat * 1000));     //MAX_LOAD_RATE_INSPECT_Z3

        //    SetMccInfomation(iPos++, (int)(equip.InspZ4.Setting.POSITION2_SPEED * 1000));       //SET_INSPECT_Z4_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ4.XF_CurrMotorSpeed.vFloat * 1000));      //CUR_INSPECT_Z4_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ4.XF_CurrMotorPosition.vFloat * 1000));   //CUR_INSPECT_Z4_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ4.Setting.POSITION1 * 1000));             //SET_INSPECT_Z4_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ4.Setting.POSITION2 * 1000));             //SET_INSPECT_Z4_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ4.Setting.POSITION3 * 1000));             //SET_INSPECT_Z4_SCAN1_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ4.Setting.POSITION4 * 1000));             //SET_INSPECT_Z4_SCAN2_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ4.Setting.POSITION5 * 1000));             //SET_INSPECT_Z4_SCAN3_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ4.Setting.POSITION6 * 1000));             //SET_INSPECT_Z4_SCAN4_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ4.XF_CurrMotorStress.vFloat * 1000));     //MAX_LOAD_RATE_INSPECT_Z4

        //    SetMccInfomation(iPos++, (int)(equip.InspZ5.Setting.POSITION2_SPEED * 1000));       //SET_INSPECT_Z5_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ5.XF_CurrMotorSpeed.vFloat * 1000));      //CUR_INSPECT_Z5_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ5.XF_CurrMotorPosition.vFloat * 1000));   //CUR_INSPECT_Z5_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ5.Setting.POSITION1 * 1000));             //SET_INSPECT_Z5_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ5.Setting.POSITION2 * 1000));             //SET_INSPECT_Z5_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ5.Setting.POSITION3 * 1000));             //SET_INSPECT_Z5_SCAN1_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ5.Setting.POSITION4 * 1000));             //SET_INSPECT_Z5_SCAN2_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ5.Setting.POSITION5 * 1000));             //SET_INSPECT_Z5_SCAN3_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ5.Setting.POSITION6 * 1000));             //SET_INSPECT_Z5_SCAN4_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ5.XF_CurrMotorStress.vFloat * 1000));     //MAX_LOAD_RATE_INSPECT_Z5

        //    SetMccInfomation(iPos++, (int)(equip.InspZ6.Setting.POSITION2_SPEED * 1000));       //SET_INSPECT_Z6_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ6.XF_CurrMotorSpeed.vFloat * 1000));      //CUR_INSPECT_Z6_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ6.XF_CurrMotorPosition.vFloat * 1000));   //CUR_INSPECT_Z6_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ6.Setting.POSITION1 * 1000));             //SET_INSPECT_Z6_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ6.Setting.POSITION2 * 1000));             //SET_INSPECT_Z6_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ6.Setting.POSITION3 * 1000));             //SET_INSPECT_Z6_SCAN1_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ6.Setting.POSITION4 * 1000));             //SET_INSPECT_Z6_SCAN2_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ6.Setting.POSITION5 * 1000));             //SET_INSPECT_Z6_SCAN3_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ6.Setting.POSITION6 * 1000));             //SET_INSPECT_Z6_SCAN4_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ6.XF_CurrMotorStress.vFloat * 1000));     //MAX_LOAD_RATE_INSPECT_Z6

        //    SetMccInfomation(iPos++, (int)(equip.InspZ7.Setting.POSITION2_SPEED * 1000));       //SET_INSPECT_Z7_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ7.XF_CurrMotorSpeed.vFloat * 1000));      //CUR_INSPECT_Z7_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ7.XF_CurrMotorPosition.vFloat * 1000));   //CUR_INSPECT_Z7_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ7.Setting.POSITION1 * 1000));             //SET_INSPECT_Z7_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ7.Setting.POSITION2 * 1000));             //SET_INSPECT_Z7_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ7.Setting.POSITION3 * 1000));             //SET_INSPECT_Z7_SCAN1_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ7.Setting.POSITION4 * 1000));             //SET_INSPECT_Z7_SCAN2_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ7.Setting.POSITION5 * 1000));             //SET_INSPECT_Z7_SCAN3_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ7.Setting.POSITION6 * 1000));             //SET_INSPECT_Z7_SCAN4_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ7.XF_CurrMotorStress.vFloat * 1000));     //MAX_LOAD_RATE_INSPECT_Z7

        //    SetMccInfomation(iPos++, (int)(equip.InspZ8.Setting.POSITION2_SPEED * 1000));       //SET_INSPECT_Z8_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ8.XF_CurrMotorSpeed.vFloat * 1000));      //CUR_INSPECT_Z8_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ8.XF_CurrMotorPosition.vFloat * 1000));   //CUR_INSPECT_Z8_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ8.Setting.POSITION1 * 1000));             //SET_INSPECT_Z8_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ8.Setting.POSITION2 * 1000));             //SET_INSPECT_Z8_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ8.Setting.POSITION3 * 1000));             //SET_INSPECT_Z8_SCAN1_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ8.Setting.POSITION4 * 1000));             //SET_INSPECT_Z8_SCAN2_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ8.Setting.POSITION5 * 1000));             //SET_INSPECT_Z8_SCAN3_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ8.Setting.POSITION6 * 1000));             //SET_INSPECT_Z8_SCAN4_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ8.XF_CurrMotorStress.vFloat * 1000));     //MAX_LOAD_RATE_INSPECT_Z8

        //    SetMccInfomation(iPos++, (int)(equip.InspZ9.Setting.POSITION2_SPEED * 1000));       //SET_INSPECT_Z9_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ9.XF_CurrMotorSpeed.vFloat * 1000));      //CUR_INSPECT_Z9_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ9.XF_CurrMotorPosition.vFloat * 1000));   //CUR_INSPECT_Z9_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ9.Setting.POSITION1 * 1000));             //SET_INSPECT_Z9_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ9.Setting.POSITION2 * 1000));             //SET_INSPECT_Z9_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ9.Setting.POSITION3 * 1000));             //SET_INSPECT_Z9_SCAN1_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ9.Setting.POSITION4 * 1000));             //SET_INSPECT_Z9_SCAN2_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ9.Setting.POSITION5 * 1000));             //SET_INSPECT_Z9_SCAN3_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ9.Setting.POSITION6 * 1000));             //SET_INSPECT_Z9_SCAN4_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ9.XF_CurrMotorStress.vFloat * 1000));     //MAX_LOAD_RATE_INSPECT_Z9

        //    SetMccInfomation(iPos++, (int)(equip.InspZ10.Setting.POSITION2_SPEED * 1000));       //SET_INSPECT_Z10_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ10.XF_CurrMotorSpeed.vFloat * 1000));      //CUR_INSPECT_Z10_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ10.XF_CurrMotorPosition.vFloat * 1000));   //CUR_INSPECT_Z10_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ10.Setting.POSITION1 * 1000));             //SET_INSPECT_Z10_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ10.Setting.POSITION2 * 1000));             //SET_INSPECT_Z10_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ10.Setting.POSITION3 * 1000));             //SET_INSPECT_Z10_SCAN1_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ10.Setting.POSITION4 * 1000));             //SET_INSPECT_Z10_SCAN2_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ10.Setting.POSITION5 * 1000));             //SET_INSPECT_Z10_SCAN3_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ10.Setting.POSITION6 * 1000));             //SET_INSPECT_Z10_SCAN4_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ10.XF_CurrMotorStress.vFloat * 1000));     //MAX_LOAD_RATE_INSPECT_Z10

        //    SetMccInfomation(iPos++, (int)(equip.InspZ11.Setting.POSITION2_SPEED * 1000));       //SET_INSPECT_Z11_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ11.XF_CurrMotorSpeed.vFloat * 1000));      //CUR_INSPECT_Z11_SPD
        //    SetMccInfomation(iPos++, (int)(equip.InspZ11.XF_CurrMotorPosition.vFloat * 1000));   //CUR_INSPECT_Z11_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ11.Setting.POSITION1 * 1000));             //SET_INSPECT_Z11_HOME_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ11.Setting.POSITION2 * 1000));             //SET_INSPECT_Z11_LOAD_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ11.Setting.POSITION3 * 1000));             //SET_INSPECT_Z11_SCAN1_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ11.Setting.POSITION4 * 1000));             //SET_INSPECT_Z11_SCAN2_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ11.Setting.POSITION5 * 1000));             //SET_INSPECT_Z11_SCAN3_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ11.Setting.POSITION6 * 1000));             //SET_INSPECT_Z11_SCAN4_POS
        //    SetMccInfomation(iPos++, (int)(equip.InspZ11.XF_CurrMotorStress.vFloat * 1000));     //MAX_LOAD_RATE_INSPECT_Z11

        //    SetMccInfomation(iPos++, (int)(equip.ADC.MainAir1));                                //CUR_MAIN_AIR_1
        //    SetMccInfomation(iPos++, (int)(equip.ADC.MainAir2));                                //CUR_MAIN_AIR_2
        //    SetMccInfomation(iPos++, (int)(equip.ADC.MainAir3));                                //CUR_MAIN_AIR_3
        //    SetMccInfomation(iPos++, (int)(equip.ADC.MainVacuum1));                             //CUR_MAIN_VAC_1
        //    SetMccInfomation(iPos++, (int)(equip.ADC.MainVacuum2));                             //CUR_MAIN_VAC_2
        //    SetMccInfomation(iPos++, (int)(equip.ADC.Vacuum1));                                 //CUR_2ST_VAC_1
        //    SetMccInfomation(iPos++, (int)(equip.ADC.Vacuum2));                                 //CUR_2ST_VAC_2
        //    SetMccInfomation(iPos++, (int)(equip.ADC.Vacuum3));                                 //CUR_2ST_VAC_3
        //    SetMccInfomation(iPos++, (int)(equip.ADC.Vacuum4));                                 //CUR_2ST_VAC_4
        //    SetMccInfomation(iPos++, (int)(equip.ADC.Vacuum5));                                 //CUR_2ST_VAC_5

        //    SetMccInfomation(iPos++, (int)(equip.ADC.CurPCRackTemp));                           //CUR_PC_RACK_TEMPRATUR
        //    SetMccInfomation(iPos++, (int)(equip.ADC.CurCPBoxTemp));                            //CUR_PANEL_TEMPRATUR

        //    SetMccInfomation(iPos++, (int)(equip.AnalogSetting.Pc_Rack_Temp));                    //SET_PC_RACK_LIMIT_TEMPRATUR
        //    SetMccInfomation(iPos++, (int)(equip.AnalogSetting.Panel_Temp));                    //SET_PANEL_LIMIT_TEMPRATUR
        //    SetMccInfomation(iPos++, (int)(0 * 1000));                    //SET_TIME_OVER_DELAY_HOME_POS
        //    SetMccInfomation(iPos++, (int)(0 * 1000));                    //SET_CENTERING_FWD_DELAY
        //    SetMccInfomation(iPos++, (int)(0 * 1000));                    //SET_CENTERING_BWD_DELAY
        //    SetMccInfomation(iPos++, (int)(0 * 1000));                    //SET_UNLOAD_BLOW_TIME_DELAY
        //    SetMccInfomation(iPos++, (int)(0 * 1000));                    //SET_VAC_TIME_WAIT_DELAY
        //    SetMccInfomation(iPos++, (int)(0 * 1000));                    //SET_SERVER_JUDGECOMPLETE_TIMEOVER
        //    SetMccInfomation(iPos++, (int)(0 * 1000));                    //SET_SERVER_COMMAND_TIMEOVER
        //    SetMccInfomation(iPos++, (int)(0 * 1000));                    //SET_SERVER_EVENT_TIMEOVER
        //    SetMccInfomation(iPos++, (int)(0 * 1000));                    //SET_REVIEW_TIMEOVER
        //    SetMccInfomation(iPos++, (int)(0 * 1000));                    //SET_REVIEW_ALIGN_TIMEOVER
        //    SetMccInfomation(iPos++, (int)(0 * 1000));                    //SET_REVIEW_ALIGN_TRY_COUNT
        //}

        private void LogicWorkingAlarm(Equipment equip)
        {
            foreach (EM_AL_LST emAl in Enum.GetValues(typeof(EM_AL_LST)))
                SetMccAlarm((int)emAl, AlarmMgr.Instance.HappenAlarms[emAl].Happen);
        }
        public void SetMccCategory(MccActionCategory category)
        {
            _prevMccCategory = _currMccCategory;
            _currMccCategory = category;
        }
        public void SetMccSignal(int seq, bool value)
        {
            PlcAddr addr = SIGNAL_BIT_START.AddBit(seq, 8);
            addr.vBit = value;
        }
        public void SetMccInfomation(int seq, int value)
        {
            PlcAddr addr = INFO_BIT_START.AddWord(seq * 4);
            addr.Length = 4;
            addr.vInt = value;
        }
        public void SetMccAlarm(int seq, bool value)
        {
            PlcAddr addr = ALARM_BIT_START.AddBit(seq, 8);
            addr.vBit = value;
        }
        public void SetMccEventGlassIn(GlassInfo frontInfo, GlassInfo orgRearInfo)
        {
            GLASSID_POS_R.vAscii = orgRearInfo.HGlassID;
            GLASSID_POS_F.vAscii = frontInfo.HGlassID;
            STEPID_POS.vAscii = orgRearInfo.StepID;
            LOTID_POS.vAscii = orgRearInfo.LotID;
            PPID_POS.vAscii = orgRearInfo.PPID;

        }
        public void SetMccEventGlassOut(GlassInfo frontInfo, GlassInfo orgRearInfo)
        {
            GLASSID_POS_R.vAscii = orgRearInfo.HGlassID;
            GLASSID_POS_F.vAscii = frontInfo.HGlassID;
            STEPID_POS.vAscii = orgRearInfo.StepID;
            LOTID_POS.vAscii = orgRearInfo.LotID;
            PPID_POS.vAscii = orgRearInfo.PPID;
        }
        public void SetMccEventEquipStateChange(EMEquipState newState, EMEquipState oldState)
        {
            EQP_STATE.vShort = (short)newState;
            OLD_EQP_STATE.vShort = (short)oldState;
        }
        public void SetMccEventProcStateChange(EMProcState newState, EMProcState oldState)
        {
            PROC_STATE.vShort = (short)newState;
            OLD_PROC_STATE.vShort = (short)oldState;
        }
        public void SetMccEventCurrentPpidChage(string newPpid, string oldPpid)
        {
            NEW_PPID.vAscii = newPpid;
            OLD_PPID.vAscii = oldPpid;
        }
        private PlcAddr GetMccSeq(MccActionCategory category, MccActionItem action)
        {
            int seq = 0;
            if (SlstMccItem.ContainsKey(category))
                if (SlstMccItem[category].ContainsKey(action))
                    seq = SlstMccItem[category][action];

            return ACTION_BIT_START.AddBit(seq, 8);
        }
        public void SetMccAction(PlcAddr addr, bool value, int interval)
        {
            addr.vBit = value;
            for (int iPos = 0; iPos < _lstIntervalOnOff.Count; iPos++)
            {
                if (_lstIntervalOnOff[iPos].Complete == true)
                {
                    _lstIntervalOnOff[iPos].OffTime = DateTime.Now.AddMilliseconds(interval);
                    _lstIntervalOnOff[iPos].Address = addr;
                    _lstIntervalOnOff[iPos].Complete = false;
                }
            }
        }
        public void SetMccAction(MccActionItem action, bool value = true, MccActionCategory category = MccActionCategory.NONE)
        {
            if (action == MccActionItem.NONE) return;

            PlcAddr addr;

            if (category != MccActionCategory.NONE)
                addr = GetMccSeq(category, category, action);
            else
                addr = GetMccSeq(_currMccCategory, _prevMccCategory, action);

            addr.vBit = value;
            Logger.Log.AppendLine(LogLevel.NoLog, "{0}의 {1}이 {2}로 변경됨", _currMccCategory.ToString(), action.ToString(), value.ToString());
        }
        public void SetMccAction(MccActionItem action, bool value, int interval, MccActionCategory category = MccActionCategory.NONE)
        {
            PlcAddr addr;

            if (category == MccActionCategory.NONE)
                addr = GetMccSeq(_currMccCategory, _prevMccCategory, action);
            else
                addr = GetMccSeq(category, category, action);

            addr.vBit = value;

            SetMccAction(addr, value, interval);
        }

        private PlcAddr GetMccSeq(MccActionCategory category, MccActionCategory prevCategory, MccActionItem action)
        {
            int seq = 0;

            if (SlstMccItem.ContainsKey(category))
                if (SlstMccItem[category].ContainsKey(action))
                {
                    seq = SlstMccItem[category][action];
                }
                else
                {
                    if (SlstMccItem.ContainsKey(prevCategory))
                        if (SlstMccItem[prevCategory].ContainsKey(action))
                        {
                            seq = SlstMccItem[prevCategory][action];
                        }
                }

            return ACTION_BIT_START.AddBit(seq, 8);
        }
        public void CheckIntervalOn()
        {
            for (int iPos = 0; iPos < _lstIntervalOnOff.Count; iPos++)
            {
                if (_lstIntervalOnOff[iPos].Complete == false)
                {
                    if (_lstIntervalOnOff[iPos].OffTime < DateTime.Now)
                    {
                        _lstIntervalOnOff[iPos].Address.vBit = false;
                        _lstIntervalOnOff[iPos].Complete = true;
                    }
                }
            }
        }
        public void MccReset()
        {
            foreach (var category in SlstMccItem)
            {
                foreach (var action in SlstMccItem[category.Key])
                {
                    SetMccAction(action.Key, false, category.Key);
                }
            }
        }
    }
}

