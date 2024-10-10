using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;


namespace EquipMainUi.Struct.Detail.PC
{
    public class IsptAddrW
    {
        #region Ctrl -> INSP
        public static PlcAddr YW_GlassData				        /*  */= new PlcAddr(PlcMemType.S, 100, 0, 2, PlcValueType.SHORT);
        public static PlcAddr CST_ID_POS                        /*  */= new PlcAddr(PlcMemType.S, 000, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr RF_READ_CST_ID_POS              /*  */= new PlcAddr(PlcMemType.S, 020, 0, 20, PlcValueType.ASCII);
        public static PlcAddr WAFER_ID_POS                      /*  */= new PlcAddr(PlcMemType.S, 040, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr BARCODE_READ_ID_POS             /*  */= new PlcAddr(PlcMemType.S, 060, 0, 20, PlcValueType.ASCII);
        public static PlcAddr RECIPE_ID_POS                     /*  */= new PlcAddr(PlcMemType.S, 080, 0, 30, PlcValueType.ASCII);
        public static PlcAddr LOT_ID_POS                        /*  */= new PlcAddr(PlcMemType.S, 110, 0, 20, PlcValueType.ASCII);
        public static PlcAddr ID_TYPE_POS                       /*  */= new PlcAddr(PlcMemType.S, 130, 0, 20, PlcValueType.ASCII);
        public static PlcAddr PROCESS_POS                       /*  */= new PlcAddr(PlcMemType.S, 140, 0, 20, PlcValueType.ASCII);
        public static PlcAddr COLUMNS_POS                       /*  */= new PlcAddr(PlcMemType.S, 150, 0, 2, PlcValueType.SHORT);
        public static PlcAddr ROWS_POS                          /*  */= new PlcAddr(PlcMemType.S, 152, 0, 2, PlcValueType.SHORT);
        public static PlcAddr FLAT_ZONE_POS                     /*  */= new PlcAddr(PlcMemType.S, 154, 0, 10, PlcValueType.ASCII);
        public static PlcAddr OPER_ID_POS                       /*  */= new PlcAddr(PlcMemType.S, 164, 0, 10, PlcValueType.ASCII);

        //yyyymmddhhMMss
        public static PlcAddr YW_TimeSync                       /*   */ = new PlcAddr(PlcMemType.S, 1100, 0, 14, PlcValueType.ASCII);

        public static PlcAddr YW_VcrReadGlassID    				/*   */ = new PlcAddr(PlcMemType.S, 1114, 0, 40, PlcValueType.ASCII);
        public static PlcAddr YW_VcrReadResult    				/*   */ = new PlcAddr(PlcMemType.S, 1154, 0, 2, PlcValueType.ASCII);
        public static PlcAddr YW_VcrKeyInGlassID                /*   */ = new PlcAddr(PlcMemType.S, 1156, 0, 40, PlcValueType.ASCII);
        public static PlcAddr YW_ScanCount                      /*   */ = new PlcAddr(PlcMemType.S, 1196, 0, 2, PlcValueType.SHORT);
        public static PlcAddr YW_ScanIndex                      /*   */ = new PlcAddr(PlcMemType.S, 1198, 0, 2, PlcValueType.SHORT);

        public static PlcAddr YW_UserID                         /*   */ = new PlcAddr(PlcMemType.S, 1300, 0, 20, PlcValueType.ASCII);
        public static PlcAddr YW_LoginResult                    /*   */ = new PlcAddr(PlcMemType.S, 1320, 0, 2, PlcValueType.SHORT);

        public static PlcAddr YF_NotchX           = new PlcAddr(PlcMemType.S, 1500, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_NotchY           = new PlcAddr(PlcMemType.S, 1504, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_NotchT           = new PlcAddr(PlcMemType.S, 1508, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_OffsetX            = new PlcAddr(PlcMemType.S, 1512, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_OffsetY            = new PlcAddr(PlcMemType.S, 1516, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_OffsetT            = new PlcAddr(PlcMemType.S, 1520, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseX         = new PlcAddr(PlcMemType.S, 1524, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseY         = new PlcAddr(PlcMemType.S, 1528, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_MajorLength      = new PlcAddr(PlcMemType.S, 1532, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_MinorLength      = new PlcAddr(PlcMemType.S, 1536, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseT         = new PlcAddr(PlcMemType.S, 1540, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseMajorX1   = new PlcAddr(PlcMemType.S, 1544, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseMajorY1   = new PlcAddr(PlcMemType.S, 1548, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseMajorX2   = new PlcAddr(PlcMemType.S, 1552, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseMajorY2   = new PlcAddr(PlcMemType.S, 1556, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseMinorX1   = new PlcAddr(PlcMemType.S, 1560, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseMinorY1   = new PlcAddr(PlcMemType.S, 1564, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseMinorX2   = new PlcAddr(PlcMemType.S, 1568, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseMinorY2   = new PlcAddr(PlcMemType.S, 1572, 0, 4, PlcValueType.FLOAT);
        #endregion
        #region INSP -> CTRL
        public static PlcAddr XI_DVItem     				    /*   */ = new PlcAddr(PlcMemType.S, 5100, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_SVItem     				    /*   */ = new PlcAddr(PlcMemType.S, 6100, 0, 4, PlcValueType.INT32);
        public static PlcAddr XW_JudgeResult    				/*   */ = new PlcAddr(PlcMemType.S, 7100, 0, 2, PlcValueType.ASCII);
        public static PlcAddr XW_JudgeCode      				/*   */ = new PlcAddr(PlcMemType.S, 7102, 0, 2, PlcValueType.ASCII);
        public static PlcAddr XW_GradeCode      				/*   */ = new PlcAddr(PlcMemType.S, 7104, 0, 2, PlcValueType.ASCII);
        /// <summary>
        /// 1 : Create, 2 : Delete, 3 : Modify, 4 : Current Recipe Change
        /// </summary>
        public static PlcAddr XI_RecipeChangeCode               /*   */ = new PlcAddr(PlcMemType.S, 7106, 0, 2, PlcValueType.SHORT);
        public static PlcAddr XW_VcrReadGlassID    				/*   */ = new PlcAddr(PlcMemType.S, 7146, 0, 40, PlcValueType.ASCII);
        public static PlcAddr XW_VcrReadResult    				/*   */ = new PlcAddr(PlcMemType.S, 7186, 0, 2, PlcValueType.ASCII);

        public static PlcAddr XI_InspZ0_ScanPosition			/*   */ = new PlcAddr(PlcMemType.S, 7192, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ1_ScanPosition			/*   */ = new PlcAddr(PlcMemType.S, 7240, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ2_ScanPosition			/*   */ = new PlcAddr(PlcMemType.S, 7288, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ3_ScanPosition			/*   */ = new PlcAddr(PlcMemType.S, 7336, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ4_ScanPosition			/*   */ = new PlcAddr(PlcMemType.S, 7384, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ5_ScanPosition			/*   */ = new PlcAddr(PlcMemType.S, 7432, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ6_ScanPosition			/*   */ = new PlcAddr(PlcMemType.S, 7480, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ7_ScanPosition			/*   */ = new PlcAddr(PlcMemType.S, 7528, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ8_ScanPosition			/*   */ = new PlcAddr(PlcMemType.S, 7576, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ9_ScanPosition			/*   */ = new PlcAddr(PlcMemType.S, 7624, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ10_ScanPosition			/*   */ = new PlcAddr(PlcMemType.S, 7672, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ11_ScanPosition			/*   */ = new PlcAddr(PlcMemType.S, 7720, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ12_ScanPosition			/*   */ = new PlcAddr(PlcMemType.S, 7768, 0, 4, PlcValueType.INT32);

        public static PlcAddr XF_AlignX1Position				/*   */ = new PlcAddr(PlcMemType.S, 6900, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_AlignX2Position				/*   */ = new PlcAddr(PlcMemType.S, 6904, 0, 4, PlcValueType.INT32);

        public static PlcAddr XW_UserID                         /*   */ = new PlcAddr(PlcMemType.S, 7900, 0, 20, PlcValueType.ASCII);
        public static PlcAddr XW_Password                       /*   */ = new PlcAddr(PlcMemType.S, 7920, 0, 20, PlcValueType.ASCII);
        public static PlcAddr XI_CurrentRecipe                  /*   */ = new PlcAddr(PlcMemType.S, 7940, 0, 2, PlcValueType.SHORT);

        public static PlcAddr XF_AlignStartX      = new PlcAddr(PlcMemType.S, 7950, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_AlignStartY      = new PlcAddr(PlcMemType.S, 7954, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_InspectionStartX = new PlcAddr(PlcMemType.S, 7958, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_InspectionStartY = new PlcAddr(PlcMemType.S, 7962, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_ReviewStartX     = new PlcAddr(PlcMemType.S, 7966, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_ReviewStartY     = new PlcAddr(PlcMemType.S, 7970, 0, 4, PlcValueType.FLOAT);

        /// <summary>
        /// AlignPos 최대 10개 포지션
        /// </summary>

        public static PlcAddr XI_AlignCamX1Pos                  /*   */ = new PlcAddr(PlcMemType.S, 7950, 0, 40, PlcValueType.INT32);
        public static PlcAddr XI_AlignCamY1Pos                  /*   */ = new PlcAddr(PlcMemType.S, 7990, 0, 40, PlcValueType.INT32);
        public static PlcAddr XI_AlignCamX2Pos                  /*   */ = new PlcAddr(PlcMemType.S, 8030, 0, 40, PlcValueType.INT32);
        public static PlcAddr XI_AlignCamY2Pos                  /*   */ = new PlcAddr(PlcMemType.S, 8070, 0, 40, PlcValueType.INT32);

        public static PlcAddr XF_KerfDataCH1_1                  /*   */ = new PlcAddr(PlcMemType.S, 9000, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH1_2                  /*   */ = new PlcAddr(PlcMemType.S, 9004, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH1_3                  /*   */ = new PlcAddr(PlcMemType.S, 9008, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH1_4                  /*   */ = new PlcAddr(PlcMemType.S, 9012, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH1_5                  /*   */ = new PlcAddr(PlcMemType.S, 9016, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH1_6                  /*   */ = new PlcAddr(PlcMemType.S, 9020, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH1_7                  /*   */ = new PlcAddr(PlcMemType.S, 9024, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH1_8                  /*   */ = new PlcAddr(PlcMemType.S, 9028, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH1_9                  /*   */ = new PlcAddr(PlcMemType.S, 9032, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH1_10                 /*   */ = new PlcAddr(PlcMemType.S, 9036, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH1_11                 /*   */ = new PlcAddr(PlcMemType.S, 9040, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH1_12                 /*   */ = new PlcAddr(PlcMemType.S, 9044, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH1_13                 /*   */ = new PlcAddr(PlcMemType.S, 9048, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH1_14                 /*   */ = new PlcAddr(PlcMemType.S, 9052, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH1_15                 /*   */ = new PlcAddr(PlcMemType.S, 9056, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH2_1                  /*   */ = new PlcAddr(PlcMemType.S, 9060, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH2_2                  /*   */ = new PlcAddr(PlcMemType.S, 9064, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH2_3                  /*   */ = new PlcAddr(PlcMemType.S, 9068, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH2_4                  /*   */ = new PlcAddr(PlcMemType.S, 9072, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH2_5                  /*   */ = new PlcAddr(PlcMemType.S, 9076, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH2_6                  /*   */ = new PlcAddr(PlcMemType.S, 9080, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH2_7                  /*   */ = new PlcAddr(PlcMemType.S, 9084, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH2_8                  /*   */ = new PlcAddr(PlcMemType.S, 9088, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH2_9                  /*   */ = new PlcAddr(PlcMemType.S, 9092, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH2_10                 /*   */ = new PlcAddr(PlcMemType.S, 9096, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH2_11                 /*   */ = new PlcAddr(PlcMemType.S, 9100, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH2_12                 /*   */ = new PlcAddr(PlcMemType.S, 9104, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH2_13                 /*   */ = new PlcAddr(PlcMemType.S, 9108, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH2_14                 /*   */ = new PlcAddr(PlcMemType.S, 9112, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_KerfDataCH2_15                 /*   */ = new PlcAddr(PlcMemType.S, 9116, 0, 4, PlcValueType.INT32);
        #endregion

        static IsptAddrW()
        {
        }

        public static void Initialize(IVirtualMem plc)
        {
            YW_GlassData.PLC = plc;
            YW_TimeSync.PLC = plc;
            YW_VcrReadGlassID.PLC = plc;
            YW_VcrReadResult.PLC = plc;
            YW_VcrKeyInGlassID.PLC = plc;
            YW_ScanCount.PLC = plc;
            YW_ScanIndex.PLC = plc;
            YW_UserID.PLC = plc;
            YW_LoginResult.PLC = plc;
            XI_DVItem.PLC = plc;
            XI_SVItem.PLC = plc;
            XW_JudgeResult.PLC = plc;
            XW_JudgeCode.PLC = plc;
            XW_GradeCode.PLC = plc;
            XI_RecipeChangeCode.PLC = plc;
            XW_VcrReadGlassID.PLC = plc;
            XW_VcrReadResult.PLC = plc;
            XI_InspZ0_ScanPosition.PLC = plc;
            XI_InspZ1_ScanPosition.PLC = plc;
            XI_InspZ2_ScanPosition.PLC = plc;
            XI_InspZ3_ScanPosition.PLC = plc;
            XI_InspZ4_ScanPosition.PLC = plc;
            XI_InspZ5_ScanPosition.PLC = plc;
            XI_InspZ6_ScanPosition.PLC = plc;
            XI_InspZ7_ScanPosition.PLC = plc;
            XI_InspZ8_ScanPosition.PLC = plc;
            XI_InspZ9_ScanPosition.PLC = plc;
            XI_InspZ10_ScanPosition.PLC = plc;
            XI_InspZ11_ScanPosition.PLC = plc;
            XI_InspZ12_ScanPosition.PLC = plc;
            XF_AlignX1Position.PLC = plc;
            XF_AlignX2Position.PLC = plc;
            XW_UserID.PLC = plc;
            XW_Password.PLC = plc;
            XI_CurrentRecipe.PLC = plc;
            XI_AlignCamX1Pos.PLC = plc;
            XI_AlignCamY1Pos.PLC = plc;
            XI_AlignCamX2Pos.PLC = plc;
            XI_AlignCamY2Pos.PLC = plc;
            XF_AlignStartX.PLC = plc;
            XF_AlignStartY.PLC = plc;
            XF_InspectionStartX.PLC = plc;
            XF_InspectionStartY.PLC = plc;
            XF_ReviewStartX.PLC = plc;
            XF_ReviewStartY.PLC = plc;

            YF_NotchX.PLC = plc;
            YF_NotchY.PLC = plc;
            YF_NotchT.PLC = plc;
            YF_OffsetX.PLC = plc;
            YF_OffsetY.PLC = plc;
            YF_OffsetT.PLC = plc;
            YF_EllipseX.PLC = plc;
            YF_EllipseY.PLC = plc;
            YF_MajorLength.PLC = plc;
            YF_MinorLength.PLC = plc;
            YF_EllipseT.PLC = plc;
            YF_EllipseMajorX1.PLC = plc;
            YF_EllipseMajorY1.PLC = plc;
            YF_EllipseMajorX2.PLC = plc;
            YF_EllipseMajorY2.PLC = plc;
            YF_EllipseMinorX1.PLC = plc;
            YF_EllipseMinorY1.PLC = plc;
            YF_EllipseMinorX2.PLC = plc;
            YF_EllipseMinorY2.PLC = plc;

            XF_KerfDataCH1_1.PLC = plc;
            XF_KerfDataCH1_2.PLC = plc;
            XF_KerfDataCH1_3.PLC = plc;
            XF_KerfDataCH1_4.PLC = plc;
            XF_KerfDataCH1_5.PLC = plc;
            XF_KerfDataCH1_6.PLC = plc;
            XF_KerfDataCH1_7.PLC = plc;
            XF_KerfDataCH1_8.PLC = plc;
            XF_KerfDataCH1_9.PLC = plc;
            XF_KerfDataCH1_10.PLC = plc;
            XF_KerfDataCH1_11.PLC = plc;
            XF_KerfDataCH1_12.PLC = plc;
            XF_KerfDataCH1_13.PLC = plc;
            XF_KerfDataCH1_14.PLC = plc;
            XF_KerfDataCH1_15.PLC = plc;
            XF_KerfDataCH2_1.PLC = plc;
            XF_KerfDataCH2_2.PLC = plc;
            XF_KerfDataCH2_3.PLC = plc;
            XF_KerfDataCH2_4.PLC = plc;
            XF_KerfDataCH2_5.PLC = plc;
            XF_KerfDataCH2_6.PLC = plc;
            XF_KerfDataCH2_7.PLC = plc;
            XF_KerfDataCH2_8.PLC = plc;
            XF_KerfDataCH2_9.PLC = plc;
            XF_KerfDataCH2_10.PLC = plc;
            XF_KerfDataCH2_11.PLC = plc;
            XF_KerfDataCH2_12.PLC = plc;
            XF_KerfDataCH2_13.PLC = plc;
            XF_KerfDataCH2_14.PLC = plc;
            XF_KerfDataCH2_15.PLC = plc;
        }
    }
}