using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;


namespace EquipMainUi.Struct.Detail.PC
{
    public class RvAddrB
    {
        //CTRL -> REVIEW
        //STATE
        public static PlcAddr YB_ControlAlive				    /*   */ = new PlcAddr(PlcMemType.S, 10000, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_GlassIn				        /*   */ = new PlcAddr(PlcMemType.S, 10000, 1, 1, PlcValueType.BIT);
        
        public static PlcAddr YB_BypassMode				        /*   */ = new PlcAddr(PlcMemType.S, 10002, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_AutoMode				        /*   */ = new PlcAddr(PlcMemType.S, 10002, 1, 1, PlcValueType.BIT);
        public static PlcAddr YB_ManualMode				        /*   */ = new PlcAddr(PlcMemType.S, 10002, 2, 1, PlcValueType.BIT);
        public static PlcAddr YB_ReviewManualMode				/*   */ = new PlcAddr(PlcMemType.S, 10002, 3, 1, PlcValueType.BIT);

        public static PlcAddr YB_AfmHomeState				    /*   */ = new PlcAddr(PlcMemType.S, 10002, 5, 1, PlcValueType.BIT);
        public static PlcAddr YB_AfmPlusLimitState				/*   */ = new PlcAddr(PlcMemType.S, 10002, 6, 1, PlcValueType.BIT);

        //REQ                                                                                           
        public static PlcAddr YB_Loading				        /*   */ = new PlcAddr(PlcMemType.S, 10020, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_AlignStart				        /*   */ = new PlcAddr(PlcMemType.S, 10020, 1, 1, PlcValueType.BIT);
        public static PlcAddr YB_ResultFileReadReq  		    /*   */ = new PlcAddr(PlcMemType.S, 10020, 2, 1, PlcValueType.BIT);
        public static PlcAddr YB_ReviewStart    		        /*   */ = new PlcAddr(PlcMemType.S, 10020, 3, 1, PlcValueType.BIT);
        public static PlcAddr YB_ReviewEnd			            /*   */ = new PlcAddr(PlcMemType.S, 10020, 4, 1, PlcValueType.BIT);
        public static PlcAddr YB_AfmHomeMove			        /*   */ = new PlcAddr(PlcMemType.S, 10020, 5, 1, PlcValueType.BIT);
        public static PlcAddr YB_WsiHomeMove			        /*   */ = new PlcAddr(PlcMemType.S, 10020, 6, 1, PlcValueType.BIT);
        public static PlcAddr YB_GlassUnloading			        /*   */ = new PlcAddr(PlcMemType.S, 10020, 7, 1, PlcValueType.BIT);

        public static PlcAddr YB_DateTimeSet                    /*   */ = new PlcAddr(PlcMemType.S, 10021, 0, 1, PlcValueType.BIT);

        //ACK
        public static PlcAddr YB_LoadingCompleteAck             /*   */ = new PlcAddr(PlcMemType.S, 10040, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_AlignCompleteAck				/*   */ = new PlcAddr(PlcMemType.S, 10040, 1, 1, PlcValueType.BIT);
        public static PlcAddr YB_ReviewReadyAck		            /*   */ = new PlcAddr(PlcMemType.S, 10040, 2, 1, PlcValueType.BIT);
        public static PlcAddr YB_ReviewCompleteAck		        /*   */ = new PlcAddr(PlcMemType.S, 10040, 3, 1, PlcValueType.BIT);
        public static PlcAddr YB_ResultFileCreateCompleteAck	/*   */ = new PlcAddr(PlcMemType.S, 10040, 4, 1, PlcValueType.BIT);

        public static PlcAddr YB_AlarmOccurAck		            /*   */ = new PlcAddr(PlcMemType.S, 10041, 0, 1, PlcValueType.BIT);

        //REVIEW -> CTRL
        //STATE
        public static PlcAddr XB_ReviewAlive    				/*   */ = new PlcAddr(PlcMemType.S, 15000, 0, 1, PlcValueType.BIT);

        //ACK
        public static PlcAddr XB_LoadingAck				        /*   */ = new PlcAddr(PlcMemType.S, 15020, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_AlignStartAck				    /*   */ = new PlcAddr(PlcMemType.S, 15020, 1, 1, PlcValueType.BIT);
        public static PlcAddr XB_ResultFileReadReqAck			/*   */ = new PlcAddr(PlcMemType.S, 15020, 2, 1, PlcValueType.BIT);
        public static PlcAddr XB_ReviewStartAck				    /*   */ = new PlcAddr(PlcMemType.S, 15020, 3, 1, PlcValueType.BIT);
        public static PlcAddr XB_ReviewEndAck			        /*   */ = new PlcAddr(PlcMemType.S, 15020, 4, 1, PlcValueType.BIT);
        public static PlcAddr XB_AfmHomeMoveAck			        /*   */ = new PlcAddr(PlcMemType.S, 15020, 5, 1, PlcValueType.BIT);
        public static PlcAddr XB_WsiHomeMoveAck			        /*   */ = new PlcAddr(PlcMemType.S, 15020, 6, 1, PlcValueType.BIT);
        public static PlcAddr XB_GlassUnloadingAck			    /*   */ = new PlcAddr(PlcMemType.S, 15020, 7, 1, PlcValueType.BIT);

        public static PlcAddr XB_DateTimeSetAck				    /*   */ = new PlcAddr(PlcMemType.S, 15021, 0, 1, PlcValueType.BIT);

        //REQ
        public static PlcAddr XB_LoadingComplete				/*   */ = new PlcAddr(PlcMemType.S, 15040, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_AlignComplete				    /*   */ = new PlcAddr(PlcMemType.S, 15040, 1, 1, PlcValueType.BIT);
        public static PlcAddr XB_ResultFileReadComplete			/*   */ = new PlcAddr(PlcMemType.S, 15040, 2, 1, PlcValueType.BIT);
        public static PlcAddr XB_ReviewComplete				    /*   */ = new PlcAddr(PlcMemType.S, 15040, 3, 1, PlcValueType.BIT);
        public static PlcAddr XB_ResultFileCreateComplete		/*   */ = new PlcAddr(PlcMemType.S, 15040, 4, 1, PlcValueType.BIT);

        public static PlcAddr XB_AlarmOccur		                /*   */ = new PlcAddr(PlcMemType.S, 15041, 0, 1, PlcValueType.BIT);

        static RvAddrB()
        {
        }

        public static void Initialize(IVirtualMem plc)
        {
            YB_ControlAlive.PLC = plc;
            YB_GlassIn.PLC = plc;
            YB_BypassMode.PLC = plc;
            YB_AutoMode.PLC = plc;
            YB_ManualMode.PLC = plc;
            YB_ReviewManualMode.PLC = plc;
            YB_AfmHomeState.PLC = plc;
            YB_AfmPlusLimitState.PLC = plc;
            
            YB_Loading.PLC = plc;
            YB_AlignStart.PLC = plc;
            YB_ResultFileReadReq.PLC = plc;
            YB_ReviewStart.PLC = plc;
            YB_ReviewEnd.PLC = plc;
            YB_AfmHomeMove.PLC = plc;
            YB_WsiHomeMove.PLC = plc;
            YB_GlassUnloading.PLC = plc;

            YB_DateTimeSet.PLC = plc;

            YB_LoadingCompleteAck.PLC = plc;
            YB_AlignCompleteAck.PLC = plc;
            YB_ReviewReadyAck.PLC = plc;
            YB_ReviewCompleteAck.PLC = plc;
            YB_ResultFileCreateCompleteAck.PLC = plc;

            YB_AlarmOccurAck.PLC = plc;

            XB_ReviewAlive.PLC = plc;

            XB_LoadingAck.PLC = plc;
            XB_AlignStartAck.PLC = plc;
            XB_ResultFileReadReqAck.PLC = plc;
            XB_ReviewStartAck.PLC = plc;
            XB_ReviewEndAck.PLC = plc;
            XB_AfmHomeMoveAck.PLC = plc;
            XB_WsiHomeMoveAck.PLC = plc;
            XB_GlassUnloadingAck.PLC = plc;

            XB_DateTimeSetAck.PLC = plc;

            XB_LoadingComplete.PLC = plc;
            XB_AlignComplete.PLC = plc;
            XB_ResultFileReadComplete.PLC = plc;
            XB_ReviewComplete.PLC = plc;
            XB_ResultFileCreateComplete.PLC = plc;

            XB_AlarmOccur.PLC = plc;
        }
    }
}