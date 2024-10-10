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
        public static PlcAddr YW_GlassData                      /*  */= new PlcAddr(PlcMemType.S, 100, 0, 2, PlcValueType.SHORT);
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

        public static PlcAddr YW_VcrReadGlassID                 /*   */ = new PlcAddr(PlcMemType.S, 1114, 0, 40, PlcValueType.ASCII);
        public static PlcAddr YW_VcrReadResult                  /*   */ = new PlcAddr(PlcMemType.S, 1154, 0, 2, PlcValueType.ASCII);
        public static PlcAddr YW_VcrKeyInGlassID                /*   */ = new PlcAddr(PlcMemType.S, 1156, 0, 40, PlcValueType.ASCII);
        public static PlcAddr YW_ScanCount                      /*   */ = new PlcAddr(PlcMemType.S, 1196, 0, 2, PlcValueType.SHORT);
        public static PlcAddr YW_ScanIndex                      /*   */ = new PlcAddr(PlcMemType.S, 1198, 0, 2, PlcValueType.SHORT);

        public static PlcAddr YW_UserID                         /*   */ = new PlcAddr(PlcMemType.S, 1300, 0, 20, PlcValueType.ASCII);
        public static PlcAddr YW_LoginResult                    /*   */ = new PlcAddr(PlcMemType.S, 1320, 0, 2, PlcValueType.SHORT);

        public static PlcAddr YF_NotchX = new PlcAddr(PlcMemType.S, 1500, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_NotchY = new PlcAddr(PlcMemType.S, 1504, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_NotchT = new PlcAddr(PlcMemType.S, 1508, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_OffsetX = new PlcAddr(PlcMemType.S, 1512, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_OffsetY = new PlcAddr(PlcMemType.S, 1516, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_OffsetT = new PlcAddr(PlcMemType.S, 1520, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseX = new PlcAddr(PlcMemType.S, 1524, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseY = new PlcAddr(PlcMemType.S, 1528, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_MajorLength = new PlcAddr(PlcMemType.S, 1532, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_MinorLength = new PlcAddr(PlcMemType.S, 1536, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseT = new PlcAddr(PlcMemType.S, 1540, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseMajorX1 = new PlcAddr(PlcMemType.S, 1544, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseMajorY1 = new PlcAddr(PlcMemType.S, 1548, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseMajorX2 = new PlcAddr(PlcMemType.S, 1552, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseMajorY2 = new PlcAddr(PlcMemType.S, 1556, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseMinorX1 = new PlcAddr(PlcMemType.S, 1560, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseMinorY1 = new PlcAddr(PlcMemType.S, 1564, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseMinorX2 = new PlcAddr(PlcMemType.S, 1568, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr YF_EllipseMinorY2 = new PlcAddr(PlcMemType.S, 1572, 0, 4, PlcValueType.FLOAT);
        #endregion
        #region INSP -> CTRL
        public static PlcAddr XI_DVItem                         /*   */ = new PlcAddr(PlcMemType.S, 5100, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_SVItem                         /*   */ = new PlcAddr(PlcMemType.S, 6100, 0, 4, PlcValueType.INT32);
        public static PlcAddr XW_JudgeResult                    /*   */ = new PlcAddr(PlcMemType.S, 7100, 0, 2, PlcValueType.ASCII);
        public static PlcAddr XW_JudgeCode                      /*   */ = new PlcAddr(PlcMemType.S, 7102, 0, 2, PlcValueType.ASCII);
        public static PlcAddr XW_GradeCode                      /*   */ = new PlcAddr(PlcMemType.S, 7104, 0, 2, PlcValueType.ASCII);
        /// <summary>
        /// 1 : Create, 2 : Delete, 3 : Modify, 4 : Current Recipe Change
        /// </summary>
        public static PlcAddr XI_RecipeChangeCode               /*   */ = new PlcAddr(PlcMemType.S, 7106, 0, 2, PlcValueType.SHORT);
        public static PlcAddr XW_VcrReadGlassID                 /*   */ = new PlcAddr(PlcMemType.S, 7146, 0, 40, PlcValueType.ASCII);
        public static PlcAddr XW_VcrReadResult                  /*   */ = new PlcAddr(PlcMemType.S, 7186, 0, 2, PlcValueType.ASCII);

        public static PlcAddr XI_InspZ0_ScanPosition            /*   */ = new PlcAddr(PlcMemType.S, 7192, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ1_ScanPosition            /*   */ = new PlcAddr(PlcMemType.S, 7240, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ2_ScanPosition            /*   */ = new PlcAddr(PlcMemType.S, 7288, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ3_ScanPosition            /*   */ = new PlcAddr(PlcMemType.S, 7336, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ4_ScanPosition            /*   */ = new PlcAddr(PlcMemType.S, 7384, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ5_ScanPosition            /*   */ = new PlcAddr(PlcMemType.S, 7432, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ6_ScanPosition            /*   */ = new PlcAddr(PlcMemType.S, 7480, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ7_ScanPosition            /*   */ = new PlcAddr(PlcMemType.S, 7528, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ8_ScanPosition            /*   */ = new PlcAddr(PlcMemType.S, 7576, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ9_ScanPosition            /*   */ = new PlcAddr(PlcMemType.S, 7624, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ10_ScanPosition           /*   */ = new PlcAddr(PlcMemType.S, 7672, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ11_ScanPosition           /*   */ = new PlcAddr(PlcMemType.S, 7720, 0, 4, PlcValueType.INT32);
        public static PlcAddr XI_InspZ12_ScanPosition           /*   */ = new PlcAddr(PlcMemType.S, 7768, 0, 4, PlcValueType.INT32);

        public static PlcAddr XF_AlignX1Position                /*   */ = new PlcAddr(PlcMemType.S, 6900, 0, 4, PlcValueType.INT32);
        public static PlcAddr XF_AlignX2Position                /*   */ = new PlcAddr(PlcMemType.S, 6904, 0, 4, PlcValueType.INT32);

        public static PlcAddr XW_UserID                         /*   */ = new PlcAddr(PlcMemType.S, 7900, 0, 20, PlcValueType.ASCII);
        public static PlcAddr XW_Password                       /*   */ = new PlcAddr(PlcMemType.S, 7920, 0, 20, PlcValueType.ASCII);
        public static PlcAddr XI_CurrentRecipe                  /*   */ = new PlcAddr(PlcMemType.S, 7940, 0, 2, PlcValueType.SHORT);

        public static PlcAddr XF_AlignStartX = new PlcAddr(PlcMemType.S, 7950, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_AlignStartY = new PlcAddr(PlcMemType.S, 7954, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_InspectionStartX = new PlcAddr(PlcMemType.S, 7958, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_InspectionStartY = new PlcAddr(PlcMemType.S, 7962, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_ReviewStartX = new PlcAddr(PlcMemType.S, 7966, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_ReviewStartY = new PlcAddr(PlcMemType.S, 7970, 0, 4, PlcValueType.FLOAT);

        /// <summary>
        /// AlignPos 최대 10개 포지션
        /// </summary>

        public static PlcAddr XI_AlignCamX1Pos                  /*   */ = new PlcAddr(PlcMemType.S, 7950, 0, 40, PlcValueType.INT32);
        public static PlcAddr XI_AlignCamY1Pos                  /*   */ = new PlcAddr(PlcMemType.S, 7990, 0, 40, PlcValueType.INT32);
        public static PlcAddr XI_AlignCamX2Pos                  /*   */ = new PlcAddr(PlcMemType.S, 8030, 0, 40, PlcValueType.INT32);
        public static PlcAddr XI_AlignCamY2Pos                  /*   */ = new PlcAddr(PlcMemType.S, 8070, 0, 40, PlcValueType.INT32);

        public static PlcAddr XF_KerfData_Right_Col_1 = new PlcAddr(PlcMemType.S, 8100, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_1 = new PlcAddr(PlcMemType.S, 8104, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_1 = new PlcAddr(PlcMemType.S, 8108, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_2 = new PlcAddr(PlcMemType.S, 8112, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_2 = new PlcAddr(PlcMemType.S, 8116, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_2 = new PlcAddr(PlcMemType.S, 8120, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_3 = new PlcAddr(PlcMemType.S, 8124, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_3 = new PlcAddr(PlcMemType.S, 8128, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_3 = new PlcAddr(PlcMemType.S, 8132, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_4 = new PlcAddr(PlcMemType.S, 8136, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_4 = new PlcAddr(PlcMemType.S, 8140, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_4 = new PlcAddr(PlcMemType.S, 8144, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_5 = new PlcAddr(PlcMemType.S, 8148, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_5 = new PlcAddr(PlcMemType.S, 8152, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_5 = new PlcAddr(PlcMemType.S, 8156, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_6 = new PlcAddr(PlcMemType.S, 8160, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_6 = new PlcAddr(PlcMemType.S, 8164, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_6 = new PlcAddr(PlcMemType.S, 8168, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_7 = new PlcAddr(PlcMemType.S, 8172, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_7 = new PlcAddr(PlcMemType.S, 8176, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_7 = new PlcAddr(PlcMemType.S, 8180, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_8 = new PlcAddr(PlcMemType.S, 8184, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_8 = new PlcAddr(PlcMemType.S, 8188, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_8 = new PlcAddr(PlcMemType.S, 8192, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_9 = new PlcAddr(PlcMemType.S, 8196, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_9 = new PlcAddr(PlcMemType.S, 8200, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_9 = new PlcAddr(PlcMemType.S, 8204, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_10 = new PlcAddr(PlcMemType.S, 8208, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_10 = new PlcAddr(PlcMemType.S, 8212, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_10 = new PlcAddr(PlcMemType.S, 8216, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_11 = new PlcAddr(PlcMemType.S, 8220, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_11 = new PlcAddr(PlcMemType.S, 8224, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_11 = new PlcAddr(PlcMemType.S, 8228, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_12 = new PlcAddr(PlcMemType.S, 8232, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_12 = new PlcAddr(PlcMemType.S, 8236, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_12 = new PlcAddr(PlcMemType.S, 8240, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_13 = new PlcAddr(PlcMemType.S, 8244, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_13 = new PlcAddr(PlcMemType.S, 8248, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_13 = new PlcAddr(PlcMemType.S, 8252, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_14 = new PlcAddr(PlcMemType.S, 8256, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_14 = new PlcAddr(PlcMemType.S, 8260, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_14 = new PlcAddr(PlcMemType.S, 8264, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_15 = new PlcAddr(PlcMemType.S, 8268, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_15 = new PlcAddr(PlcMemType.S, 8272, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_15 = new PlcAddr(PlcMemType.S, 8276, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_16 = new PlcAddr(PlcMemType.S, 8280, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_16 = new PlcAddr(PlcMemType.S, 8284, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_16 = new PlcAddr(PlcMemType.S, 8288, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_17 = new PlcAddr(PlcMemType.S, 8292, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_17 = new PlcAddr(PlcMemType.S, 8296, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_17 = new PlcAddr(PlcMemType.S, 8300, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_18 = new PlcAddr(PlcMemType.S, 8304, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_18 = new PlcAddr(PlcMemType.S, 8308, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_18 = new PlcAddr(PlcMemType.S, 8312, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_19 = new PlcAddr(PlcMemType.S, 8316, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_19 = new PlcAddr(PlcMemType.S, 8320, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_19 = new PlcAddr(PlcMemType.S, 8324, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Col_20 = new PlcAddr(PlcMemType.S, 8328, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_Row_20 = new PlcAddr(PlcMemType.S, 8332, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Right_20 = new PlcAddr(PlcMemType.S, 8336, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KerfData_Bottom_Col_1 = new PlcAddr(PlcMemType.S, 8340, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_1 = new PlcAddr(PlcMemType.S, 8344, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_1 = new PlcAddr(PlcMemType.S, 8348, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_2 = new PlcAddr(PlcMemType.S, 8352, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_2 = new PlcAddr(PlcMemType.S, 8356, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_2 = new PlcAddr(PlcMemType.S, 8360, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_3 = new PlcAddr(PlcMemType.S, 8364, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_3 = new PlcAddr(PlcMemType.S, 8368, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_3 = new PlcAddr(PlcMemType.S, 8372, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_4 = new PlcAddr(PlcMemType.S, 8376, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_4 = new PlcAddr(PlcMemType.S, 8380, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_4 = new PlcAddr(PlcMemType.S, 8384, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_5 = new PlcAddr(PlcMemType.S, 8388, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_5 = new PlcAddr(PlcMemType.S, 8392, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_5 = new PlcAddr(PlcMemType.S, 8396, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_6 = new PlcAddr(PlcMemType.S, 8400, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_6 = new PlcAddr(PlcMemType.S, 8404, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_6 = new PlcAddr(PlcMemType.S, 8408, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_7 = new PlcAddr(PlcMemType.S, 8412, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_7 = new PlcAddr(PlcMemType.S, 8416, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_7 = new PlcAddr(PlcMemType.S, 8420, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_8 = new PlcAddr(PlcMemType.S, 8424, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_8 = new PlcAddr(PlcMemType.S, 8428, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_8 = new PlcAddr(PlcMemType.S, 8432, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_9 = new PlcAddr(PlcMemType.S, 8436, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_9 = new PlcAddr(PlcMemType.S, 8440, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_9 = new PlcAddr(PlcMemType.S, 8444, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_10 = new PlcAddr(PlcMemType.S, 8448, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_10 = new PlcAddr(PlcMemType.S, 8452, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_10 = new PlcAddr(PlcMemType.S, 8456, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_11 = new PlcAddr(PlcMemType.S, 8460, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_11 = new PlcAddr(PlcMemType.S, 8464, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_11 = new PlcAddr(PlcMemType.S, 8468, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_12 = new PlcAddr(PlcMemType.S, 8472, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_12 = new PlcAddr(PlcMemType.S, 8476, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_12 = new PlcAddr(PlcMemType.S, 8480, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_13 = new PlcAddr(PlcMemType.S, 8484, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_13 = new PlcAddr(PlcMemType.S, 8488, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_13 = new PlcAddr(PlcMemType.S, 8492, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_14 = new PlcAddr(PlcMemType.S, 8496, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_14 = new PlcAddr(PlcMemType.S, 8500, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_14 = new PlcAddr(PlcMemType.S, 8504, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_15 = new PlcAddr(PlcMemType.S, 8508, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_15 = new PlcAddr(PlcMemType.S, 8512, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_15 = new PlcAddr(PlcMemType.S, 8516, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_16 = new PlcAddr(PlcMemType.S, 8520, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_16 = new PlcAddr(PlcMemType.S, 8524, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_16 = new PlcAddr(PlcMemType.S, 8528, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_17 = new PlcAddr(PlcMemType.S, 8532, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_17 = new PlcAddr(PlcMemType.S, 8536, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_17 = new PlcAddr(PlcMemType.S, 8540, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_18 = new PlcAddr(PlcMemType.S, 8544, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_18 = new PlcAddr(PlcMemType.S, 8548, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_18 = new PlcAddr(PlcMemType.S, 8552, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_19 = new PlcAddr(PlcMemType.S, 8556, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_19 = new PlcAddr(PlcMemType.S, 8560, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_19 = new PlcAddr(PlcMemType.S, 8564, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Col_20 = new PlcAddr(PlcMemType.S, 8568, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_Row_20 = new PlcAddr(PlcMemType.S, 8572, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KerfData_Bottom_20 = new PlcAddr(PlcMemType.S, 8576, 0, 4, PlcValueType.FLOAT);


        //public static PlcAddr XF_KerfDataCH1_1                  /*   */ = new PlcAddr(PlcMemType.S, 9000, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH1_2                  /*   */ = new PlcAddr(PlcMemType.S, 9004, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH1_3                  /*   */ = new PlcAddr(PlcMemType.S, 9008, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH1_4                  /*   */ = new PlcAddr(PlcMemType.S, 9012, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH1_5                  /*   */ = new PlcAddr(PlcMemType.S, 9016, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH1_6                  /*   */ = new PlcAddr(PlcMemType.S, 9020, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH1_7                  /*   */ = new PlcAddr(PlcMemType.S, 9024, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH1_8                  /*   */ = new PlcAddr(PlcMemType.S, 9028, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH1_9                  /*   */ = new PlcAddr(PlcMemType.S, 9032, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH1_10                 /*   */ = new PlcAddr(PlcMemType.S, 9036, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH1_11                 /*   */ = new PlcAddr(PlcMemType.S, 9040, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH1_12                 /*   */ = new PlcAddr(PlcMemType.S, 9044, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH1_13                 /*   */ = new PlcAddr(PlcMemType.S, 9048, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH1_14                 /*   */ = new PlcAddr(PlcMemType.S, 9052, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH1_15                 /*   */ = new PlcAddr(PlcMemType.S, 9056, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH2_1                  /*   */ = new PlcAddr(PlcMemType.S, 9060, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH2_2                  /*   */ = new PlcAddr(PlcMemType.S, 9064, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH2_3                  /*   */ = new PlcAddr(PlcMemType.S, 9068, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH2_4                  /*   */ = new PlcAddr(PlcMemType.S, 9072, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH2_5                  /*   */ = new PlcAddr(PlcMemType.S, 9076, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH2_6                  /*   */ = new PlcAddr(PlcMemType.S, 9080, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH2_7                  /*   */ = new PlcAddr(PlcMemType.S, 9084, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH2_8                  /*   */ = new PlcAddr(PlcMemType.S, 9088, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH2_9                  /*   */ = new PlcAddr(PlcMemType.S, 9092, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH2_10                 /*   */ = new PlcAddr(PlcMemType.S, 9096, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH2_11                 /*   */ = new PlcAddr(PlcMemType.S, 9100, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH2_12                 /*   */ = new PlcAddr(PlcMemType.S, 9104, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH2_13                 /*   */ = new PlcAddr(PlcMemType.S, 9108, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH2_14                 /*   */ = new PlcAddr(PlcMemType.S, 9112, 0, 4, PlcValueType.INT32);
        //public static PlcAddr XF_KerfDataCH2_15                 /*   */ = new PlcAddr(PlcMemType.S, 9116, 0, 4, PlcValueType.INT32);
        #endregion

        #region KerfData Add // 231129-01 20000 KerfData INSP->Ctrl
        public static PlcAddr XF_KERF_DATA_01_DIE_COL = new PlcAddr(PlcMemType.S, 9200, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_01_DIE_ROW = new PlcAddr(PlcMemType.S, 9204, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_01_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9208, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_01_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9212, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_01_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9216, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_01_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9220, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_02_DIE_COL = new PlcAddr(PlcMemType.S, 9224, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_02_DIE_ROW = new PlcAddr(PlcMemType.S, 9228, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_02_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9232, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_02_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9236, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_02_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9240, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_02_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9244, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_03_DIE_COL = new PlcAddr(PlcMemType.S, 9248, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_03_DIE_ROW = new PlcAddr(PlcMemType.S, 9252, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_03_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9256, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_03_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9260, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_03_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9264, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_03_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9268, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_04_DIE_COL = new PlcAddr(PlcMemType.S, 9272, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_04_DIE_ROW = new PlcAddr(PlcMemType.S, 9276, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_04_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9280, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_04_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9284, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_04_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9288, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_04_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9292, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_05_DIE_COL = new PlcAddr(PlcMemType.S, 9296, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_05_DIE_ROW = new PlcAddr(PlcMemType.S, 9300, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_05_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9304, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_05_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9308, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_05_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9312, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_05_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9316, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_06_DIE_COL = new PlcAddr(PlcMemType.S, 9320, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_06_DIE_ROW = new PlcAddr(PlcMemType.S, 9324, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_06_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9328, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_06_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9332, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_06_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9336, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_06_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9340, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_07_DIE_COL = new PlcAddr(PlcMemType.S, 9344, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_07_DIE_ROW = new PlcAddr(PlcMemType.S, 9348, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_07_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9352, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_07_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9356, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_07_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9360, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_07_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9364, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_08_DIE_COL = new PlcAddr(PlcMemType.S, 9368, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_08_DIE_ROW = new PlcAddr(PlcMemType.S, 9372, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_08_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9376, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_08_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9380, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_08_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9384, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_08_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9388, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_09_DIE_COL = new PlcAddr(PlcMemType.S, 9392, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_09_DIE_ROW = new PlcAddr(PlcMemType.S, 9396, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_09_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9400, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_09_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9404, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_09_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9408, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_09_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9412, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_10_DIE_COL = new PlcAddr(PlcMemType.S, 9416, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_10_DIE_ROW = new PlcAddr(PlcMemType.S, 9420, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_10_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9424, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_10_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9428, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_10_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9432, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_10_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9436, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_11_DIE_COL = new PlcAddr(PlcMemType.S, 9440, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_11_DIE_ROW = new PlcAddr(PlcMemType.S, 9444, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_11_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9448, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_11_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9452, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_11_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9456, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_11_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9460, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_12_DIE_COL = new PlcAddr(PlcMemType.S, 9464, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_12_DIE_ROW = new PlcAddr(PlcMemType.S, 9468, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_12_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9472, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_12_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9476, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_12_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9480, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_12_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9484, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_13_DIE_COL = new PlcAddr(PlcMemType.S, 9488, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_13_DIE_ROW = new PlcAddr(PlcMemType.S, 9492, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_13_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9496, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_13_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9500, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_13_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9504, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_13_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9508, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_14_DIE_COL = new PlcAddr(PlcMemType.S, 9512, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_14_DIE_ROW = new PlcAddr(PlcMemType.S, 9516, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_14_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9520, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_14_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9524, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_14_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9528, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_14_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9532, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_15_DIE_COL = new PlcAddr(PlcMemType.S, 9536, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_15_DIE_ROW = new PlcAddr(PlcMemType.S, 9540, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_15_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9544, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_15_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9548, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_15_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9552, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_15_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9556, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_16_DIE_COL = new PlcAddr(PlcMemType.S, 9560, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_16_DIE_ROW = new PlcAddr(PlcMemType.S, 9564, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_16_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9568, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_16_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9572, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_16_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9576, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_16_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9580, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_17_DIE_COL = new PlcAddr(PlcMemType.S, 9584, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_17_DIE_ROW = new PlcAddr(PlcMemType.S, 9588, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_17_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9592, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_17_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9596, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_17_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9600, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_17_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9604, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_18_DIE_COL = new PlcAddr(PlcMemType.S, 9608, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_18_DIE_ROW = new PlcAddr(PlcMemType.S, 9612, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_18_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9616, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_18_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9620, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_18_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9624, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_18_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9628, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_19_DIE_COL = new PlcAddr(PlcMemType.S, 9632, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_19_DIE_ROW = new PlcAddr(PlcMemType.S, 9636, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_19_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9640, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_19_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9644, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_19_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9648, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_19_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9652, 0, 4, PlcValueType.FLOAT);

        public static PlcAddr XF_KERF_DATA_20_DIE_COL = new PlcAddr(PlcMemType.S, 9656, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_20_DIE_ROW = new PlcAddr(PlcMemType.S, 9660, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_20_WIDTH_TOP = new PlcAddr(PlcMemType.S, 9664, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_20_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 9668, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_20_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 9672, 0, 4, PlcValueType.FLOAT);
        public static PlcAddr XF_KERF_DATA_20_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 9676, 0, 4, PlcValueType.FLOAT);
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

            XF_KerfData_Right_Col_1.PLC = plc;
            XF_KerfData_Right_Row_1.PLC = plc;
            XF_KerfData_Right_1.PLC = plc;
            XF_KerfData_Right_Col_2.PLC = plc;
            XF_KerfData_Right_Row_2.PLC = plc;
            XF_KerfData_Right_2.PLC = plc;
            XF_KerfData_Right_Col_3.PLC = plc;
            XF_KerfData_Right_Row_3.PLC = plc;
            XF_KerfData_Right_3.PLC = plc;
            XF_KerfData_Right_Col_4.PLC = plc;
            XF_KerfData_Right_Row_4.PLC = plc;
            XF_KerfData_Right_4.PLC = plc;
            XF_KerfData_Right_Col_5.PLC = plc;
            XF_KerfData_Right_Row_5.PLC = plc;
            XF_KerfData_Right_5.PLC = plc;
            XF_KerfData_Right_Col_6.PLC = plc;
            XF_KerfData_Right_Row_6.PLC = plc;
            XF_KerfData_Right_6.PLC = plc;
            XF_KerfData_Right_Col_7.PLC = plc;
            XF_KerfData_Right_Row_7.PLC = plc;
            XF_KerfData_Right_7.PLC = plc;
            XF_KerfData_Right_Col_8.PLC = plc;
            XF_KerfData_Right_Row_8.PLC = plc;
            XF_KerfData_Right_8.PLC = plc;
            XF_KerfData_Right_Col_9.PLC = plc;
            XF_KerfData_Right_Row_9.PLC = plc;
            XF_KerfData_Right_9.PLC = plc;
            XF_KerfData_Right_Col_10.PLC = plc;
            XF_KerfData_Right_Row_10.PLC = plc;
            XF_KerfData_Right_10.PLC = plc;
            XF_KerfData_Right_Col_11.PLC = plc;
            XF_KerfData_Right_Row_11.PLC = plc;
            XF_KerfData_Right_11.PLC = plc;
            XF_KerfData_Right_Col_12.PLC = plc;
            XF_KerfData_Right_Row_12.PLC = plc;
            XF_KerfData_Right_12.PLC = plc;
            XF_KerfData_Right_Col_13.PLC = plc;
            XF_KerfData_Right_Row_13.PLC = plc;
            XF_KerfData_Right_13.PLC = plc;
            XF_KerfData_Right_Col_14.PLC = plc;
            XF_KerfData_Right_Row_14.PLC = plc;
            XF_KerfData_Right_14.PLC = plc;
            XF_KerfData_Right_Col_15.PLC = plc;
            XF_KerfData_Right_Row_15.PLC = plc;
            XF_KerfData_Right_15.PLC = plc;
            XF_KerfData_Right_Col_16.PLC = plc;
            XF_KerfData_Right_Row_16.PLC = plc;
            XF_KerfData_Right_16.PLC = plc;
            XF_KerfData_Right_Col_17.PLC = plc;
            XF_KerfData_Right_Row_17.PLC = plc;
            XF_KerfData_Right_17.PLC = plc;
            XF_KerfData_Right_Col_18.PLC = plc;
            XF_KerfData_Right_Row_18.PLC = plc;
            XF_KerfData_Right_18.PLC = plc;
            XF_KerfData_Right_Col_19.PLC = plc;
            XF_KerfData_Right_Row_19.PLC = plc;
            XF_KerfData_Right_19.PLC = plc;
            XF_KerfData_Right_Col_20.PLC = plc;
            XF_KerfData_Right_Row_20.PLC = plc;
            XF_KerfData_Right_20.PLC = plc;
            XF_KerfData_Bottom_Col_1.PLC = plc;
            XF_KerfData_Bottom_Row_1.PLC = plc;
            XF_KerfData_Bottom_1.PLC = plc;
            XF_KerfData_Bottom_Col_2.PLC = plc;
            XF_KerfData_Bottom_Row_2.PLC = plc;
            XF_KerfData_Bottom_2.PLC = plc;
            XF_KerfData_Bottom_Col_3.PLC = plc;
            XF_KerfData_Bottom_Row_3.PLC = plc;
            XF_KerfData_Bottom_3.PLC = plc;
            XF_KerfData_Bottom_Col_4.PLC = plc;
            XF_KerfData_Bottom_Row_4.PLC = plc;
            XF_KerfData_Bottom_4.PLC = plc;
            XF_KerfData_Bottom_Col_5.PLC = plc;
            XF_KerfData_Bottom_Row_5.PLC = plc;
            XF_KerfData_Bottom_5.PLC = plc;
            XF_KerfData_Bottom_Col_6.PLC = plc;
            XF_KerfData_Bottom_Row_6.PLC = plc;
            XF_KerfData_Bottom_6.PLC = plc;
            XF_KerfData_Bottom_Col_7.PLC = plc;
            XF_KerfData_Bottom_Row_7.PLC = plc;
            XF_KerfData_Bottom_7.PLC = plc;
            XF_KerfData_Bottom_Col_8.PLC = plc;
            XF_KerfData_Bottom_Row_8.PLC = plc;
            XF_KerfData_Bottom_8.PLC = plc;
            XF_KerfData_Bottom_Col_9.PLC = plc;
            XF_KerfData_Bottom_Row_9.PLC = plc;
            XF_KerfData_Bottom_9.PLC = plc;
            XF_KerfData_Bottom_Col_10.PLC = plc;
            XF_KerfData_Bottom_Row_10.PLC = plc;
            XF_KerfData_Bottom_10.PLC = plc;
            XF_KerfData_Bottom_Col_11.PLC = plc;
            XF_KerfData_Bottom_Row_11.PLC = plc;
            XF_KerfData_Bottom_11.PLC = plc;
            XF_KerfData_Bottom_Col_12.PLC = plc;
            XF_KerfData_Bottom_Row_12.PLC = plc;
            XF_KerfData_Bottom_12.PLC = plc;
            XF_KerfData_Bottom_Col_13.PLC = plc;
            XF_KerfData_Bottom_Row_13.PLC = plc;
            XF_KerfData_Bottom_13.PLC = plc;
            XF_KerfData_Bottom_Col_14.PLC = plc;
            XF_KerfData_Bottom_Row_14.PLC = plc;
            XF_KerfData_Bottom_14.PLC = plc;
            XF_KerfData_Bottom_Col_15.PLC = plc;
            XF_KerfData_Bottom_Row_15.PLC = plc;
            XF_KerfData_Bottom_15.PLC = plc;
            XF_KerfData_Bottom_Col_16.PLC = plc;
            XF_KerfData_Bottom_Row_16.PLC = plc;
            XF_KerfData_Bottom_16.PLC = plc;
            XF_KerfData_Bottom_Col_17.PLC = plc;
            XF_KerfData_Bottom_Row_17.PLC = plc;
            XF_KerfData_Bottom_17.PLC = plc;
            XF_KerfData_Bottom_Col_18.PLC = plc;
            XF_KerfData_Bottom_Row_18.PLC = plc;
            XF_KerfData_Bottom_18.PLC = plc;
            XF_KerfData_Bottom_Col_19.PLC = plc;
            XF_KerfData_Bottom_Row_19.PLC = plc;
            XF_KerfData_Bottom_19.PLC = plc;
            XF_KerfData_Bottom_Col_20.PLC = plc;
            XF_KerfData_Bottom_Row_20.PLC = plc;
            XF_KerfData_Bottom_20.PLC = plc;

            #region KerfData Add // 231129-01 20000 KerfData I
            XF_KERF_DATA_01_DIE_COL.PLC = plc;
            XF_KERF_DATA_01_DIE_ROW.PLC = plc;
            XF_KERF_DATA_01_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_01_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_01_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_01_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_02_DIE_COL.PLC = plc;
            XF_KERF_DATA_02_DIE_ROW.PLC = plc;
            XF_KERF_DATA_02_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_02_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_02_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_02_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_03_DIE_COL.PLC = plc;
            XF_KERF_DATA_03_DIE_ROW.PLC = plc;
            XF_KERF_DATA_03_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_03_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_03_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_03_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_04_DIE_COL.PLC = plc;
            XF_KERF_DATA_04_DIE_ROW.PLC = plc;
            XF_KERF_DATA_04_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_04_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_04_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_04_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_05_DIE_COL.PLC = plc;
            XF_KERF_DATA_05_DIE_ROW.PLC = plc;
            XF_KERF_DATA_05_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_05_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_05_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_05_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_06_DIE_COL.PLC = plc;
            XF_KERF_DATA_06_DIE_ROW.PLC = plc;
            XF_KERF_DATA_06_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_06_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_06_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_06_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_07_DIE_COL.PLC = plc;
            XF_KERF_DATA_07_DIE_ROW.PLC = plc;
            XF_KERF_DATA_07_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_07_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_07_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_07_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_08_DIE_COL.PLC = plc;
            XF_KERF_DATA_08_DIE_ROW.PLC = plc;
            XF_KERF_DATA_08_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_08_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_08_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_08_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_09_DIE_COL.PLC = plc;
            XF_KERF_DATA_09_DIE_ROW.PLC = plc;
            XF_KERF_DATA_09_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_09_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_09_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_09_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_10_DIE_COL.PLC = plc;
            XF_KERF_DATA_10_DIE_ROW.PLC = plc;
            XF_KERF_DATA_10_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_10_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_10_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_10_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_11_DIE_COL.PLC = plc;
            XF_KERF_DATA_11_DIE_ROW.PLC = plc;
            XF_KERF_DATA_11_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_11_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_11_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_11_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_12_DIE_COL.PLC = plc;
            XF_KERF_DATA_12_DIE_ROW.PLC = plc;
            XF_KERF_DATA_12_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_12_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_12_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_12_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_13_DIE_COL.PLC = plc;
            XF_KERF_DATA_13_DIE_ROW.PLC = plc;
            XF_KERF_DATA_13_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_13_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_13_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_13_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_14_DIE_COL.PLC = plc;
            XF_KERF_DATA_14_DIE_ROW.PLC = plc;
            XF_KERF_DATA_14_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_14_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_14_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_14_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_15_DIE_COL.PLC = plc;
            XF_KERF_DATA_15_DIE_ROW.PLC = plc;
            XF_KERF_DATA_15_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_15_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_15_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_15_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_16_DIE_COL.PLC = plc;
            XF_KERF_DATA_16_DIE_ROW.PLC = plc;
            XF_KERF_DATA_16_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_16_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_16_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_16_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_17_DIE_COL.PLC = plc;
            XF_KERF_DATA_17_DIE_ROW.PLC = plc;
            XF_KERF_DATA_17_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_17_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_17_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_17_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_18_DIE_COL.PLC = plc;
            XF_KERF_DATA_18_DIE_ROW.PLC = plc;
            XF_KERF_DATA_18_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_18_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_18_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_18_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_19_DIE_COL.PLC = plc;
            XF_KERF_DATA_19_DIE_ROW.PLC = plc;
            XF_KERF_DATA_19_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_19_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_19_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_19_WIDTH_LEFT.PLC = plc;

            XF_KERF_DATA_20_DIE_COL.PLC = plc;
            XF_KERF_DATA_20_DIE_ROW.PLC = plc;
            XF_KERF_DATA_20_WIDTH_TOP.PLC = plc;
            XF_KERF_DATA_20_WIDTH_RIGHT.PLC = plc;
            XF_KERF_DATA_20_WIDTH_BOTTOM.PLC = plc;
            XF_KERF_DATA_20_WIDTH_LEFT.PLC = plc;
            #endregion
        }
    }
}