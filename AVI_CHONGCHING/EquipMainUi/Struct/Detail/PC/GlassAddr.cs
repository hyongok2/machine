using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;

namespace EquipMainUi.Struct.Detail.PC
{
    class GlassAddr
    {
        public static PlcAddr H_GLASSID_POS 			        /*  */= new PlcAddr(PlcMemType.S, 0, 0, 16, PlcValueType.NONO);
        public static PlcAddr E_GLASSID_POS 			        /*  */= new PlcAddr(PlcMemType.S, 16, 0, 16, PlcValueType.NONO);
        public static PlcAddr LOTID_POS 			            /*  */= new PlcAddr(PlcMemType.S, 32, 0, 16, PlcValueType.NONO);
        public static PlcAddr BATCHID_POS 			            /*  */= new PlcAddr(PlcMemType.S, 48, 0, 16, PlcValueType.NONO);
        public static PlcAddr JOBID_POS 			            /*  */= new PlcAddr(PlcMemType.S, 64, 0, 16, PlcValueType.NONO);
        public static PlcAddr PORTID_POS 			            /*  */= new PlcAddr(PlcMemType.S, 80, 0, 4, PlcValueType.NONO);
        public static PlcAddr SLOTNO_POS 			            /*  */= new PlcAddr(PlcMemType.S, 84, 0, 2, PlcValueType.NONO);
        public static PlcAddr PRODUCT_TYPE_POS 			        /*  */= new PlcAddr(PlcMemType.S, 86, 0, 4, PlcValueType.NONO);
        public static PlcAddr PRODUCT_KIND_POS 			        /*  */= new PlcAddr(PlcMemType.S, 90, 0, 4, PlcValueType.NONO);
        public static PlcAddr PRODUCTID_POS 			        /*  */= new PlcAddr(PlcMemType.S, 94, 0, 16, PlcValueType.NONO);
        public static PlcAddr RUNSPECID_POS 			        /*  */= new PlcAddr(PlcMemType.S, 110, 0, 16, PlcValueType.NONO);
        public static PlcAddr LAYERID_POS 			            /*  */= new PlcAddr(PlcMemType.S, 126, 0, 8, PlcValueType.NONO);
        public static PlcAddr STEPID_POS 			            /*  */= new PlcAddr(PlcMemType.S, 134, 0, 8, PlcValueType.NONO);
        public static PlcAddr PPID_POS 			                /*  */= new PlcAddr(PlcMemType.S, 142, 0, 20, PlcValueType.NONO);
        public static PlcAddr FLOWID_POS 			            /*  */= new PlcAddr(PlcMemType.S, 162, 0, 20, PlcValueType.NONO);
        public static PlcAddr GLASS_SIZE_POS 			        /*  */= new PlcAddr(PlcMemType.S, 182, 0, 4, PlcValueType.NONO);
        public static PlcAddr GLASS_THICKNESS_POS 		        /*  */= new PlcAddr(PlcMemType.S, 186, 0, 2, PlcValueType.NONO);
        public static PlcAddr GLASS_STATE_POS 			        /*  */= new PlcAddr(PlcMemType.S, 188, 0, 2, PlcValueType.NONO);
        public static PlcAddr GLASS_ORDER_POS 			        /*  */= new PlcAddr(PlcMemType.S, 190, 0, 4, PlcValueType.NONO);
        public static PlcAddr COMMENT_POS 			            /*  */= new PlcAddr(PlcMemType.S, 194, 0, 16, PlcValueType.NONO);
        public static PlcAddr USE_COUNT_POS 			        /*  */= new PlcAddr(PlcMemType.S, 210, 0, 4, PlcValueType.NONO);
        public static PlcAddr JUDGEMENT_POS 			        /*  */= new PlcAddr(PlcMemType.S, 214, 0, 4, PlcValueType.NONO);
        public static PlcAddr REASON_CODE_POS 			        /*  */= new PlcAddr(PlcMemType.S, 218, 0, 4, PlcValueType.NONO);
        public static PlcAddr INS_FLAG_POS 			            /*  */= new PlcAddr(PlcMemType.S, 222, 0, 2, PlcValueType.NONO);
        public static PlcAddr ENC_FLAG_POS 			            /*  */= new PlcAddr(PlcMemType.S, 224, 0, 2, PlcValueType.NONO);
        public static PlcAddr PRERUN_FLAG_POS 			        /*  */= new PlcAddr(PlcMemType.S, 226, 0, 2, PlcValueType.NONO);
        public static PlcAddr TURN_DIR_POS 			            /*  */= new PlcAddr(PlcMemType.S, 228, 0, 2, PlcValueType.NONO);
        public static PlcAddr FLIP_STATE_POS 			        /*  */= new PlcAddr(PlcMemType.S, 230, 0, 2, PlcValueType.NONO);
        public static PlcAddr WORK_STATE_POS 			        /*  */= new PlcAddr(PlcMemType.S, 232, 0, 4, PlcValueType.NONO);
        public static PlcAddr MULTI_USE_POS 			        /*  */= new PlcAddr(PlcMemType.S, 236, 0, 16, PlcValueType.NONO);
        public static PlcAddr PAIR_GLASSID_POS 			        /*  */= new PlcAddr(PlcMemType.S, 252, 0, 16, PlcValueType.NONO);
        public static PlcAddr PAIR_PPID_POS 			        /*  */= new PlcAddr(PlcMemType.S, 268, 0, 20, PlcValueType.NONO);
        public static PlcAddr OPTIONNAME1_POS 			        /*  */= new PlcAddr(PlcMemType.S, 288, 0, 40, PlcValueType.NONO);
        public static PlcAddr OPTIONVALUE1_POS 			        /*  */= new PlcAddr(PlcMemType.S, 328, 0, 40, PlcValueType.NONO);
        public static PlcAddr OPTIONNAME2_POS 			        /*  */= new PlcAddr(PlcMemType.S, 368, 0, 40, PlcValueType.NONO);
        public static PlcAddr OPTIONVALUE2_POS 			        /*  */= new PlcAddr(PlcMemType.S, 408, 0, 40, PlcValueType.NONO);
        public static PlcAddr OPTIONNAME3_POS 			        /*  */= new PlcAddr(PlcMemType.S, 448, 0, 40, PlcValueType.NONO);
        public static PlcAddr OPTIONVALUE3_POS 			        /*  */= new PlcAddr(PlcMemType.S, 488, 0, 40, PlcValueType.NONO);
        public static PlcAddr OPTIONNAME4_POS 			        /*  */= new PlcAddr(PlcMemType.S, 528, 0, 40, PlcValueType.NONO);
        public static PlcAddr OPTIONVALUE4_POS 			        /*  */= new PlcAddr(PlcMemType.S, 568, 0, 40, PlcValueType.NONO);
        public static PlcAddr OPTIONNAME5_POS 			        /*  */= new PlcAddr(PlcMemType.S, 608, 0, 40, PlcValueType.NONO);
        public static PlcAddr OPTIONVALUE5_POS 			        /*  */= new PlcAddr(PlcMemType.S, 648, 0, 40, PlcValueType.NONO);
        public static PlcAddr CSIF_POS 			                /*  */= new PlcAddr(PlcMemType.S, 688, 0, 4, PlcValueType.NONO);
        public static PlcAddr AS_POS 			                /*  */= new PlcAddr(PlcMemType.S, 692, 0, 4, PlcValueType.NONO);
        public static PlcAddr APS_POS 			                /*  */= new PlcAddr(PlcMemType.S, 696, 0, 4, PlcValueType.NONO);
        public static PlcAddr UNIQUEID_POS 		                /*  */= new PlcAddr(PlcMemType.S, 700, 0, 4, PlcValueType.NONO);
        public static PlcAddr BITSIGNAL_POS 	                /*  */= new PlcAddr(PlcMemType.S, 704, 0, 4, PlcValueType.NONO);
        public static PlcAddr MAIN_LOTID 			            /*  */= new PlcAddr(PlcMemType.S, 708, 0, 16, PlcValueType.NONO);
        public static PlcAddr LOT_FLAG 			                /*  */= new PlcAddr(PlcMemType.S, 724, 0, 2, PlcValueType.NONO);
        public static PlcAddr CSTID 			                /*  */= new PlcAddr(PlcMemType.S, 726, 0, 12, PlcValueType.NONO);
    }
}
