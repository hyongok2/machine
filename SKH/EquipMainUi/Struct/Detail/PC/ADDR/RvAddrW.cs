using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;


namespace EquipMainUi.Struct.Detail.PC
{
    public class RvAddrW
    {
        //CTRL -> REVIEW
        public static PlcAddr YW_GlassData      				/*   */ = new PlcAddr(PlcMemType.S, 10100, 0, 4, PlcValueType.SHORT);

        public static PlcAddr YW_Year                           /*   */ = new PlcAddr(PlcMemType.S, 11100, 0, 2, PlcValueType.SHORT);
        public static PlcAddr YW_Month                          /*   */ = new PlcAddr(PlcMemType.S, 11102, 0, 2, PlcValueType.SHORT);
        public static PlcAddr YW_Day                            /*   */ = new PlcAddr(PlcMemType.S, 11104, 0, 2, PlcValueType.SHORT);
        public static PlcAddr YW_Hour                           /*   */ = new PlcAddr(PlcMemType.S, 11106, 0, 2, PlcValueType.SHORT);
        public static PlcAddr YW_Minuts                         /*   */ = new PlcAddr(PlcMemType.S, 11108, 0, 2, PlcValueType.SHORT);
        public static PlcAddr YW_Second                         /*   */ = new PlcAddr(PlcMemType.S, 11110, 0, 2, PlcValueType.SHORT);

        public static PlcAddr YF_ReviewPositionX				/*   */ = new PlcAddr(PlcMemType.S, 11114, 0, 4, PlcValueType.INT32);
        public static PlcAddr YF_ReviewPositionY				/*   */ = new PlcAddr(PlcMemType.S, 11118, 0, 4, PlcValueType.INT32);

        //REVIEW -> CTRL
        public static PlcAddr XI_DVItem     				    /*   */ = new PlcAddr(PlcMemType.S, 15100, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_SVItem     				    /*   */ = new PlcAddr(PlcMemType.S, 16100, 0, 4, PlcValueType.INT32);
        public static PlcAddr XW_LoadingResult     				/*   */ = new PlcAddr(PlcMemType.S, 17100, 0, 2, PlcValueType.ASCII);
        public static PlcAddr XW_AlignResult     				/*   */ = new PlcAddr(PlcMemType.S, 17102, 0, 2, PlcValueType.ASCII);
        public static PlcAddr XW_ReviewResult     				/*   */ = new PlcAddr(PlcMemType.S, 17104, 0, 2, PlcValueType.ASCII);
        public static PlcAddr XI_AlarmCode                      /*   */ = new PlcAddr(PlcMemType.S, 17106, 0, 4, PlcValueType.INT32);

        static RvAddrW()
        {
        }

        public static void Initialize(IVirtualMem plc)
        {
            YW_GlassData.PLC = plc;
            YW_Year.PLC = plc;
            YW_Month.PLC = plc;
            YW_Day.PLC = plc;
            YW_Hour.PLC = plc;
            YW_Minuts.PLC = plc;
            YW_Second.PLC = plc;
            YF_ReviewPositionX.PLC = plc;
            YF_ReviewPositionY.PLC = plc;
            XI_DVItem.PLC = plc;
            XI_SVItem.PLC = plc;
            XW_LoadingResult.PLC = plc;
            XW_AlignResult.PLC = plc;
            XW_ReviewResult.PLC = plc;
            XI_AlarmCode.PLC = plc;
        }
    }
}