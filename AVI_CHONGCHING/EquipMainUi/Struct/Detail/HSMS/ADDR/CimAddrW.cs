using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;

namespace DitCim.PLC
{
    public class CIMAW
    {
        #region Ctrl->Cim       
        public static PlcAddr YW_CST_LOAD_REQUEST_CST_ID = new PlcAddr(PlcMemType.S, 2000, 0, 30, PlcValueType.ASCII);
        public static PlcAddr YW_CST_LOAD_REQUEST_PORT_ID = new PlcAddr(PlcMemType.S, 2030, 0, 1, PlcValueType.ASCII);
        public static PlcAddr YW_CST_LOAD_REQUEST_CUR_RECIPE = new PlcAddr(PlcMemType.S, 2031, 0, 50, PlcValueType.ASCII);

        public static PlcAddr YW_LOT_START_CST_ID = new PlcAddr(PlcMemType.S, 2100, 0, 30, PlcValueType.ASCII);
        public static PlcAddr YW_LOT_START_PORT_ID = new PlcAddr(PlcMemType.S, 2130, 0, 30, PlcValueType.ASCII);
        public static PlcAddr YW_LOT_START_PORT_RECIPE = new PlcAddr(PlcMemType.S, 2131, 0, 50, PlcValueType.ASCII);
        public static PlcAddr YW_LOT_START_WAFER_COUNT = new PlcAddr(PlcMemType.S, 2181, 0, 1, PlcValueType.SHORT);

        public static PlcAddr YW_WAFER_LOAD_REQUEST_WAFER_ID = new PlcAddr(PlcMemType.S, 2260, 0, 30, PlcValueType.ASCII);
        public static PlcAddr YW_WAFER_LOAD_REQUEST_CST_ID = new PlcAddr(PlcMemType.S, 2290, 0, 30, PlcValueType.ASCII);
        public static PlcAddr YW_WAFER_LOAD_REQUEST_PORT_ID = new PlcAddr(PlcMemType.S, 2320, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_WAFER_LOAD_REQUEST_SLOT_NO = new PlcAddr(PlcMemType.S, 2321, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_WAFER_LOAD_REQUEST_CURRENT_RECIPE = new PlcAddr(PlcMemType.S, 2322, 0, 50, PlcValueType.ASCII);

        public static PlcAddr YW_WAFER_MAP_REQUEST_WAFER_ID = new PlcAddr(PlcMemType.S, 2380, 0, 30, PlcValueType.ASCII);
        public static PlcAddr YW_WAFER_MAP_REQUEST_ID_TYPE = new PlcAddr(PlcMemType.S, 2410, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_WAFER_MAP_REQUEST_MAP_FORMAT = new PlcAddr(PlcMemType.S, 2411, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_WAFER_MAP_REQUEST_BIN_CODE = new PlcAddr(PlcMemType.S, 2412, 0, 20, PlcValueType.ASCII);
        public static PlcAddr YW_WAFER_MAP_REQUEST_NULL_BIN_CODE = new PlcAddr(PlcMemType.S, 2432, 0, 20, PlcValueType.ASCII);

        public static PlcAddr YW_WAFER_UNLOAD_WAFER_ID = new PlcAddr(PlcMemType.S, 2480, 0, 30, PlcValueType.ASCII);
        public static PlcAddr YW_WAFER_UNLOAD_CST_ID = new PlcAddr(PlcMemType.S, 2510, 0, 30, PlcValueType.ASCII);
        public static PlcAddr YW_WAFER_UNLOAD_PORT_ID = new PlcAddr(PlcMemType.S, 2540, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_WAFER_UNLOAD_SLOT_NO = new PlcAddr(PlcMemType.S, 2541, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_WAFER_UNLOAD_START_TIME = new PlcAddr(PlcMemType.S, 2542, 0, 14, PlcValueType.ASCII);
        public static PlcAddr YW_WAFER_UNLOAD_END_TIME = new PlcAddr(PlcMemType.S, 2556, 0, 14, PlcValueType.ASCII);

        public static PlcAddr YW_WAFER_UNLOAD_IS_LAST = new PlcAddr(PlcMemType.S, 2570, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_CST_UNLOAD_CST_ID = new PlcAddr(PlcMemType.S, 2580, 0, 30, PlcValueType.ASCII);
        public static PlcAddr YW_CST_UNLOAD_PORT_ID = new PlcAddr(PlcMemType.S, 2610, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_CST_UNLOAD_CUR_RECIPE = new PlcAddr(PlcMemType.S, 2611, 0, 50, PlcValueType.ASCII);
        public static PlcAddr YW_CST_UNLOAD_TOTAL_WAFER_COUNT = new PlcAddr(PlcMemType.S, 2661, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_CST_UNLOAD_INSPECTION_WAFER_COUNT = new PlcAddr(PlcMemType.S, 2662, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_CST_UNLOAD_SLOT_INFO_COUNT = new PlcAddr(PlcMemType.S, 2663, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_CST_UNLOAD_SLOT_INFO1 = new PlcAddr(PlcMemType.S, 2664, 0, 40, PlcValueType.ASCII);
        public static PlcAddr YW_CST_UNLOAD_SLOT_INFO2 = new PlcAddr(PlcMemType.S, 2704, 0, 40, PlcValueType.ASCII);
        public static PlcAddr YW_CST_UNLOAD_SLOT_INFO3 = new PlcAddr(PlcMemType.S, 2744, 0, 40, PlcValueType.ASCII);
        public static PlcAddr YW_CST_UNLOAD_SLOT_INFO4 = new PlcAddr(PlcMemType.S, 2784, 0, 40, PlcValueType.ASCII);
        public static PlcAddr YW_CST_UNLOAD_SLOT_INFO5 = new PlcAddr(PlcMemType.S, 2824, 0, 40, PlcValueType.ASCII);
        public static PlcAddr YW_CST_UNLOAD_SLOT_INFO6 = new PlcAddr(PlcMemType.S, 2864, 0, 40, PlcValueType.ASCII);
        public static PlcAddr YW_CST_UNLOAD_SLOT_INFO7 = new PlcAddr(PlcMemType.S, 2904, 0, 40, PlcValueType.ASCII);
        public static PlcAddr YW_CST_UNLOAD_SLOT_INFO8 = new PlcAddr(PlcMemType.S, 2944, 0, 40, PlcValueType.ASCII);
        public static PlcAddr YW_CST_UNLOAD_SLOT_INFO9 = new PlcAddr(PlcMemType.S, 2984, 0, 40, PlcValueType.ASCII);
        public static PlcAddr YW_CST_UNLOAD_SLOT_INFO10 = new PlcAddr(PlcMemType.S, 3024, 0, 40, PlcValueType.ASCII);
        public static PlcAddr YW_CST_UNLOAD_SLOT_INFO11 = new PlcAddr(PlcMemType.S, 3064, 0, 40, PlcValueType.ASCII);
        public static PlcAddr YW_CST_UNLOAD_SLOT_INFO12 = new PlcAddr(PlcMemType.S, 3104, 0, 40, PlcValueType.ASCII);
        public static PlcAddr YW_CST_UNLOAD_SLOT_INFO13 = new PlcAddr(PlcMemType.S, 3144, 0, 40, PlcValueType.ASCII);
        public static PlcAddr YW_CST_UNLOAD_LOT_ID = new PlcAddr(PlcMemType.S, 3264, 0, 30, PlcValueType.ASCII);

        public static PlcAddr YW_ALARM_FLAG = new PlcAddr(PlcMemType.S, 3350, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_ALARM_ID = new PlcAddr(PlcMemType.S, 3351, 0, 2, PlcValueType.SHORT);
        public static PlcAddr YW_ALARM_TEXT = new PlcAddr(PlcMemType.S, 3353, 0, 80, PlcValueType.ASCII);

        public static PlcAddr YW_RECIPE_MODE = new PlcAddr(PlcMemType.S, 3450, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_RECIPE_ID = new PlcAddr(PlcMemType.S, 3451, 0, 50, PlcValueType.ASCII);

        public static PlcAddr YW_EQP_STATUS = new PlcAddr(PlcMemType.S, 3520, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_EQP_STATUS_CURRENT_RECIPE = new PlcAddr(PlcMemType.S, 3521, 0, 50, PlcValueType.ASCII);
        public static PlcAddr YW_EQP_STATUS_CST_ID = new PlcAddr(PlcMemType.S, 3571, 0, 30, PlcValueType.ASCII);

        public static PlcAddr YW_OHT_MODE_AUTO_MODE = new PlcAddr(PlcMemType.S, 3620, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_OHT_MODE_LOAD1_STATUS = new PlcAddr(PlcMemType.S, 3621, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_OHT_MODE_UNLOAD1_STATUS = new PlcAddr(PlcMemType.S, 3622, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_OHT_MODE_LOAD2_STATUS = new PlcAddr(PlcMemType.S, 3623, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_OHT_MODE_UNLOAD2_STATUS = new PlcAddr(PlcMemType.S, 3624, 0, 1, PlcValueType.SHORT);

        public static PlcAddr YW_PORT_REQEST_NO = new PlcAddr(PlcMemType.S, 3670, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_PORT_REQEST_MODE = new PlcAddr(PlcMemType.S, 3671, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_PORT_REQEST_STATUS = new PlcAddr(PlcMemType.S, 3672, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_PORT_REQEST_CST_ID = new PlcAddr(PlcMemType.S, 3673, 0, 30, PlcValueType.ASCII);

        public static PlcAddr YW_EQP_MODE_CHANGE = new PlcAddr(PlcMemType.S, 3710, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_EQP_MODE_CHANGE_CTRL_MODE = new PlcAddr(PlcMemType.S, 3711, 0, 1, PlcValueType.SHORT);

        public static PlcAddr YW_ACK_RECIPE_MODE = new PlcAddr(PlcMemType.S, 3720, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_ACK_RECIPE_ACK = new PlcAddr(PlcMemType.S, 3721, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_ACK_PORT_RECIPE_SELECT = new PlcAddr(PlcMemType.S, 3722, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_ACK_ECV_CHANGE_ACK = new PlcAddr(PlcMemType.S, 3724, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_ACK_PORT_NO = new PlcAddr(PlcMemType.S, 3725, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_ACK_OHT_CHANGE_ACK = new PlcAddr(PlcMemType.S, 3726, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_ACK_RCMD_MODE = new PlcAddr(PlcMemType.S, 3727, 0, 1, PlcValueType.SHORT);
        public static PlcAddr YW_ACK_RCMD_ACK = new PlcAddr(PlcMemType.S, 3728, 0, 1, PlcValueType.SHORT);

        public static PlcAddr YW_OS_VERSION = new PlcAddr(PlcMemType.S, 3740, 0, 20, PlcValueType.ASCII);

        #region Kerf Width Data
        public static PlcAddr YW_KERF_DATA_01_DIE_COL = new PlcAddr(PlcMemType.S, 4070, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_01_DIE_ROW = new PlcAddr(PlcMemType.S, 4077, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_01_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4084, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_01_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4091, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_01_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4098, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_01_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4105, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_02_DIE_COL = new PlcAddr(PlcMemType.S, 4112, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_02_DIE_ROW = new PlcAddr(PlcMemType.S, 4119, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_02_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4126, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_02_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4133, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_02_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4140, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_02_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4147, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_03_DIE_COL = new PlcAddr(PlcMemType.S, 4154, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_03_DIE_ROW = new PlcAddr(PlcMemType.S, 4161, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_03_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4168, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_03_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4175, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_03_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4182, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_03_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4189, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_04_DIE_COL = new PlcAddr(PlcMemType.S, 4196, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_04_DIE_ROW = new PlcAddr(PlcMemType.S, 4203, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_04_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4210, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_04_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4217, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_04_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4224, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_04_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4231, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_05_DIE_COL = new PlcAddr(PlcMemType.S, 4238, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_05_DIE_ROW = new PlcAddr(PlcMemType.S, 4245, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_05_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4252, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_05_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4259, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_05_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4266, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_05_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4273, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_06_DIE_COL = new PlcAddr(PlcMemType.S, 4280, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_06_DIE_ROW = new PlcAddr(PlcMemType.S, 4287, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_06_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4294, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_06_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4301, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_06_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4308, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_06_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4315, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_07_DIE_COL = new PlcAddr(PlcMemType.S, 4322, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_07_DIE_ROW = new PlcAddr(PlcMemType.S, 4329, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_07_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4336, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_07_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4343, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_07_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4350, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_07_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4357, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_08_DIE_COL = new PlcAddr(PlcMemType.S, 4364, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_08_DIE_ROW = new PlcAddr(PlcMemType.S, 4371, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_08_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4378, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_08_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4385, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_08_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4392, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_08_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4399, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_09_DIE_COL = new PlcAddr(PlcMemType.S, 4406, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_09_DIE_ROW = new PlcAddr(PlcMemType.S, 4413, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_09_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4420, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_09_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4427, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_09_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4434, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_09_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4441, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_10_DIE_COL = new PlcAddr(PlcMemType.S, 4448, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_10_DIE_ROW = new PlcAddr(PlcMemType.S, 4455, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_10_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4462, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_10_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4469, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_10_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4476, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_10_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4483, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_11_DIE_COL = new PlcAddr(PlcMemType.S, 4490, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_11_DIE_ROW = new PlcAddr(PlcMemType.S, 4497, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_11_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4504, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_11_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4511, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_11_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4518, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_11_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4525, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_12_DIE_COL = new PlcAddr(PlcMemType.S, 4532, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_12_DIE_ROW = new PlcAddr(PlcMemType.S, 4539, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_12_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4546, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_12_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4553, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_12_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4560, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_12_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4567, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_13_DIE_COL = new PlcAddr(PlcMemType.S, 4574, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_13_DIE_ROW = new PlcAddr(PlcMemType.S, 4581, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_13_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4588, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_13_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4595, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_13_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4602, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_13_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4609, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_14_DIE_COL = new PlcAddr(PlcMemType.S, 4616, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_14_DIE_ROW = new PlcAddr(PlcMemType.S, 4623, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_14_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4630, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_14_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4637, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_14_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4644, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_14_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4651, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_15_DIE_COL = new PlcAddr(PlcMemType.S, 4658, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_15_DIE_ROW = new PlcAddr(PlcMemType.S, 4665, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_15_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4672, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_15_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4679, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_15_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4686, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_15_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4693, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_16_DIE_COL = new PlcAddr(PlcMemType.S, 4700, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_16_DIE_ROW = new PlcAddr(PlcMemType.S, 4707, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_16_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4714, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_16_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4721, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_16_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4728, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_16_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4735, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_17_DIE_COL = new PlcAddr(PlcMemType.S, 4742, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_17_DIE_ROW = new PlcAddr(PlcMemType.S, 4749, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_17_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4756, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_17_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4763, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_17_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4770, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_17_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4777, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_18_DIE_COL = new PlcAddr(PlcMemType.S, 4784, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_18_DIE_ROW = new PlcAddr(PlcMemType.S, 4791, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_18_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4798, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_18_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4805, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_18_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4812, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_18_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4819, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_19_DIE_COL = new PlcAddr(PlcMemType.S, 4826, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_19_DIE_ROW = new PlcAddr(PlcMemType.S, 4833, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_19_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4840, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_19_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4847, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_19_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4854, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_19_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4861, 0, 7, PlcValueType.ASCII);

        public static PlcAddr YW_KERF_DATA_20_DIE_COL = new PlcAddr(PlcMemType.S, 4868, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_20_DIE_ROW = new PlcAddr(PlcMemType.S, 4875, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_20_WIDTH_TOP = new PlcAddr(PlcMemType.S, 4882, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_20_WIDTH_RIGHT = new PlcAddr(PlcMemType.S, 4889, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_20_WIDTH_BOTTOM = new PlcAddr(PlcMemType.S, 4896, 0, 7, PlcValueType.ASCII);
        public static PlcAddr YW_KERF_DATA_20_WIDTH_LEFT = new PlcAddr(PlcMemType.S, 4903, 0, 7, PlcValueType.ASCII);
        #endregion

        #region Kerf Shift Data
        public static PlcAddr YW_KerfData_Right_Col_1 = new PlcAddr(PlcMemType.S, 20000, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_1 = new PlcAddr(PlcMemType.S, 20010, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_1 = new PlcAddr(PlcMemType.S, 20020, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_2 = new PlcAddr(PlcMemType.S, 20030, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_2 = new PlcAddr(PlcMemType.S, 20040, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_2 = new PlcAddr(PlcMemType.S, 20050, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_3 = new PlcAddr(PlcMemType.S, 20060, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_3 = new PlcAddr(PlcMemType.S, 20070, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_3 = new PlcAddr(PlcMemType.S, 20080, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_4 = new PlcAddr(PlcMemType.S, 20090, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_4 = new PlcAddr(PlcMemType.S, 20100, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_4 = new PlcAddr(PlcMemType.S, 20110, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_5 = new PlcAddr(PlcMemType.S, 20120, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_5 = new PlcAddr(PlcMemType.S, 20130, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_5 = new PlcAddr(PlcMemType.S, 20140, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_6 = new PlcAddr(PlcMemType.S, 20150, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_6 = new PlcAddr(PlcMemType.S, 20160, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_6 = new PlcAddr(PlcMemType.S, 20170, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_7 = new PlcAddr(PlcMemType.S, 20180, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_7 = new PlcAddr(PlcMemType.S, 20190, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_7 = new PlcAddr(PlcMemType.S, 20200, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_8 = new PlcAddr(PlcMemType.S, 20210, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_8 = new PlcAddr(PlcMemType.S, 20220, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_8 = new PlcAddr(PlcMemType.S, 20230, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_9 = new PlcAddr(PlcMemType.S, 20240, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_9 = new PlcAddr(PlcMemType.S, 20250, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_9 = new PlcAddr(PlcMemType.S, 20260, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_10 = new PlcAddr(PlcMemType.S, 20270, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_10 = new PlcAddr(PlcMemType.S, 20280, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_10 = new PlcAddr(PlcMemType.S, 20290, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_11 = new PlcAddr(PlcMemType.S, 20300, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_11 = new PlcAddr(PlcMemType.S, 20310, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_11 = new PlcAddr(PlcMemType.S, 20320, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_12 = new PlcAddr(PlcMemType.S, 20330, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_12 = new PlcAddr(PlcMemType.S, 20340, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_12 = new PlcAddr(PlcMemType.S, 20350, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_13 = new PlcAddr(PlcMemType.S, 20360, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_13 = new PlcAddr(PlcMemType.S, 20370, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_13 = new PlcAddr(PlcMemType.S, 20380, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_14 = new PlcAddr(PlcMemType.S, 20390, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_14 = new PlcAddr(PlcMemType.S, 20400, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_14 = new PlcAddr(PlcMemType.S, 20410, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_15 = new PlcAddr(PlcMemType.S, 20420, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_15 = new PlcAddr(PlcMemType.S, 20430, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_15 = new PlcAddr(PlcMemType.S, 20440, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_16 = new PlcAddr(PlcMemType.S, 20450, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_16 = new PlcAddr(PlcMemType.S, 20460, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_16 = new PlcAddr(PlcMemType.S, 20470, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_17 = new PlcAddr(PlcMemType.S, 20480, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_17 = new PlcAddr(PlcMemType.S, 20490, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_17 = new PlcAddr(PlcMemType.S, 20500, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_18 = new PlcAddr(PlcMemType.S, 20510, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_18 = new PlcAddr(PlcMemType.S, 20520, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_18 = new PlcAddr(PlcMemType.S, 20530, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_19 = new PlcAddr(PlcMemType.S, 20540, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_19 = new PlcAddr(PlcMemType.S, 20550, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_19 = new PlcAddr(PlcMemType.S, 20560, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Col_20 = new PlcAddr(PlcMemType.S, 20570, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_Row_20 = new PlcAddr(PlcMemType.S, 20580, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Right_20 = new PlcAddr(PlcMemType.S, 20590, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_1 = new PlcAddr(PlcMemType.S, 20600, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_1 = new PlcAddr(PlcMemType.S, 20610, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_1 = new PlcAddr(PlcMemType.S, 20620, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_2 = new PlcAddr(PlcMemType.S, 20630, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_2 = new PlcAddr(PlcMemType.S, 20640, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_2 = new PlcAddr(PlcMemType.S, 20650, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_3 = new PlcAddr(PlcMemType.S, 20660, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_3 = new PlcAddr(PlcMemType.S, 20670, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_3 = new PlcAddr(PlcMemType.S, 20680, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_4 = new PlcAddr(PlcMemType.S, 20690, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_4 = new PlcAddr(PlcMemType.S, 20700, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_4 = new PlcAddr(PlcMemType.S, 20710, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_5 = new PlcAddr(PlcMemType.S, 20720, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_5 = new PlcAddr(PlcMemType.S, 20730, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_5 = new PlcAddr(PlcMemType.S, 20740, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_6 = new PlcAddr(PlcMemType.S, 20750, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_6 = new PlcAddr(PlcMemType.S, 20760, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_6 = new PlcAddr(PlcMemType.S, 20770, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_7 = new PlcAddr(PlcMemType.S, 20780, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_7 = new PlcAddr(PlcMemType.S, 20790, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_7 = new PlcAddr(PlcMemType.S, 20800, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_8 = new PlcAddr(PlcMemType.S, 20810, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_8 = new PlcAddr(PlcMemType.S, 20820, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_8 = new PlcAddr(PlcMemType.S, 20830, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_9 = new PlcAddr(PlcMemType.S, 20840, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_9 = new PlcAddr(PlcMemType.S, 20850, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_9 = new PlcAddr(PlcMemType.S, 20860, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_10 = new PlcAddr(PlcMemType.S, 20870, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_10 = new PlcAddr(PlcMemType.S, 20880, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_10 = new PlcAddr(PlcMemType.S, 20890, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_11 = new PlcAddr(PlcMemType.S, 20900, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_11 = new PlcAddr(PlcMemType.S, 20910, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_11 = new PlcAddr(PlcMemType.S, 20920, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_12 = new PlcAddr(PlcMemType.S, 20930, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_12 = new PlcAddr(PlcMemType.S, 20940, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_12 = new PlcAddr(PlcMemType.S, 20950, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_13 = new PlcAddr(PlcMemType.S, 20960, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_13 = new PlcAddr(PlcMemType.S, 20970, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_13 = new PlcAddr(PlcMemType.S, 20980, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_14 = new PlcAddr(PlcMemType.S, 20990, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_14 = new PlcAddr(PlcMemType.S, 21000, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_14 = new PlcAddr(PlcMemType.S, 21010, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_15 = new PlcAddr(PlcMemType.S, 21020, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_15 = new PlcAddr(PlcMemType.S, 21030, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_15 = new PlcAddr(PlcMemType.S, 21040, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_16 = new PlcAddr(PlcMemType.S, 21050, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_16 = new PlcAddr(PlcMemType.S, 21060, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_16 = new PlcAddr(PlcMemType.S, 21070, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_17 = new PlcAddr(PlcMemType.S, 21080, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_17 = new PlcAddr(PlcMemType.S, 21090, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_17 = new PlcAddr(PlcMemType.S, 21100, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_18 = new PlcAddr(PlcMemType.S, 21110, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_18 = new PlcAddr(PlcMemType.S, 21120, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_18 = new PlcAddr(PlcMemType.S, 21130, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_19 = new PlcAddr(PlcMemType.S, 21140, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_19 = new PlcAddr(PlcMemType.S, 21150, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_19 = new PlcAddr(PlcMemType.S, 21160, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Col_20 = new PlcAddr(PlcMemType.S, 21170, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_Row_20 = new PlcAddr(PlcMemType.S, 21180, 0, 10, PlcValueType.ASCII);
        public static PlcAddr YW_KerfData_Bottom_20 = new PlcAddr(PlcMemType.S, 21190, 0, 10, PlcValueType.ASCII);

        #endregion

        #region 이전
        //public static PlcAddr YW_KERF_DATA_CH1_1                    /**/= new PlcAddr(PlcMemType.S, 3770, 0, 10, PlcValueType.ASCII); //left kerf shift data
        //public static PlcAddr YW_KERF_DATA_CH1_2                    /**/= new PlcAddr(PlcMemType.S, 3780, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH1_3                    /**/= new PlcAddr(PlcMemType.S, 3790, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH1_4                    /**/= new PlcAddr(PlcMemType.S, 3800, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH1_5                    /**/= new PlcAddr(PlcMemType.S, 3810, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH1_6                    /**/= new PlcAddr(PlcMemType.S, 3820, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH1_7                    /**/= new PlcAddr(PlcMemType.S, 3830, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH1_8                    /**/= new PlcAddr(PlcMemType.S, 3840, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH1_9                    /**/= new PlcAddr(PlcMemType.S, 3850, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH1_10                   /**/= new PlcAddr(PlcMemType.S, 3860, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH1_11                   /**/= new PlcAddr(PlcMemType.S, 3870, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH1_12                   /**/= new PlcAddr(PlcMemType.S, 3880, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH1_13                   /**/= new PlcAddr(PlcMemType.S, 3890, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH1_14                   /**/= new PlcAddr(PlcMemType.S, 3900, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH1_15                   /**/= new PlcAddr(PlcMemType.S, 3910, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH2_1                    /**/= new PlcAddr(PlcMemType.S, 3920, 0, 10, PlcValueType.ASCII); //left kerf shift data
        //public static PlcAddr YW_KERF_DATA_CH2_2                    /**/= new PlcAddr(PlcMemType.S, 3930, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH2_3                    /**/= new PlcAddr(PlcMemType.S, 3940, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH2_4                    /**/= new PlcAddr(PlcMemType.S, 3950, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH2_5                    /**/= new PlcAddr(PlcMemType.S, 3960, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH2_6                    /**/= new PlcAddr(PlcMemType.S, 3970, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH2_7                    /**/= new PlcAddr(PlcMemType.S, 3980, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH2_8                    /**/= new PlcAddr(PlcMemType.S, 3990, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH2_9                    /**/= new PlcAddr(PlcMemType.S, 4000, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH2_10                   /**/= new PlcAddr(PlcMemType.S, 4010, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH2_11                   /**/= new PlcAddr(PlcMemType.S, 4020, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH2_12                   /**/= new PlcAddr(PlcMemType.S, 4030, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH2_13                   /**/= new PlcAddr(PlcMemType.S, 4040, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH2_14                   /**/= new PlcAddr(PlcMemType.S, 4050, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr YW_KERF_DATA_CH2_15                   /**/= new PlcAddr(PlcMemType.S, 4060, 0, 10, PlcValueType.ASCII);

        //public static PlcAddr YW_AlarmCode                    = new PlcAddr(PlcMemType.S, 5301, 0, 1, PlcValueType.SHORT);
        //public static PlcAddr YW_AlarmID                      = new PlcAddr(PlcMemType.S, 5302, 0, 1, PlcValueType.SHORT);
        //public static PlcAddr YW_AlarmText                    = new PlcAddr(PlcMemType.S, 5304, 0, 50, PlcValueType.ASCII); // 특문(_만 허용), 공백 x

        //public static PlcAddr YW_RecipeMode                   = new PlcAddr(PlcMemType.S, 5360, 0, 1, PlcValueType.SHORT); // 1:Create, 2:Delete, 3:Modify
        //public static PlcAddr YW_RecipeName                   = new PlcAddr(PlcMemType.S, 5361, 0, 30, PlcValueType.ASCII); // 특문, 공백x, 확장자 제외 파일명

        //public static PlcAddr YW_RecipeCmdAck                 = new PlcAddr(PlcMemType.S, 5400, 0, 1, PlcValueType.SHORT); // 0 : OK 1 : NACK 6 : 그외 오류
        //public static PlcAddr YW_PauseAck                     = new PlcAddr(PlcMemType.S, 5401, 0, 1, PlcValueType.SHORT); // 0 : OK 1 : NACK 2 : 중복
        //public static PlcAddr YW_ResumeAck                    = new PlcAddr(PlcMemType.S, 5402, 0, 1, PlcValueType.SHORT); // 0 : OK 1 : NACK 2 : 중복
        //public static PlcAddr YW_NormalAck                    = new PlcAddr(PlcMemType.S, 5403, 0, 1, PlcValueType.SHORT); // 0 : OK 1 : NACK 2 : 중복
        //public static PlcAddr YW_PMAck                        = new PlcAddr(PlcMemType.S, 5404, 0, 1, PlcValueType.SHORT); // 0 : OK 1 : NACK 2 : 중복

        //public static PlcAddr YW_EquipState                   = new PlcAddr(PlcMemType.S, 5700, 0, 1, PlcValueType.SHORT); // State ex ) 1: Normal 2: Falut 3: PM
        //public static PlcAddr YW_EquipProc                    = new PlcAddr(PlcMemType.S, 5701, 0, 1, PlcValueType.SHORT); // Proc State ex ) 1: Idle 2: Excute 3: Pause

        //public static PlcAddr YW_ProcessJobID                 = new PlcAddr(PlcMemType.S, 5800, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr YW_ProcessJobStep               = new PlcAddr(PlcMemType.S, 5820, 0, 1, PlcValueType.SHORT); // 대분자 및 _만.

        //public static PlcAddr YW_LoadCstID                    = new PlcAddr(PlcMemType.S, 5900, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr YW_LoadCstPortNo                = new PlcAddr(PlcMemType.S, 5920, 0, 1, PlcValueType.SHORT);
        //public static PlcAddr YW_LoadCstRcp                   = new PlcAddr(PlcMemType.S, 5921, 0, 30, PlcValueType.ASCII);

        //public static PlcAddr YW_MapCstID                     = new PlcAddr(PlcMemType.S, 6000, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr YW_MapCstPortNo                 = new PlcAddr(PlcMemType.S, 6020, 0, 1, PlcValueType.SHORT); // 1 : 1 Port, 2 : 2 Port
        //public static PlcAddr YW_MapCstSlot                   = new PlcAddr(PlcMemType.S, 6021, 0, 40, PlcValueType.ASCII); // "ex) 25,24,1111111111111111111111110 //Carrier Wafer 최대 매수, Sensor On Wafer 매수, Sensor On : 1, Sensor Off : 0"

        //public static PlcAddr YW_WaferDataInStartAddr         = new PlcAddr(PlcMemType.S, 6100, 0, 199, PlcValueType.SHORT);
        //public static PlcAddr YW_WaferDataOutStartAddr        = new PlcAddr(PlcMemType.S, 6300, 0, 199, PlcValueType.SHORT);
        //public static PlcAddr YW_WaferDataScrapStartAddr      = new PlcAddr(PlcMemType.S, 6500, 0, 199, PlcValueType.SHORT);

        //public static PlcAddr YW_WaferDataOffset_Position     = new PlcAddr(PlcMemType.S, 0, 0, 1, PlcValueType.SHORT); // 1 : Carrier Port 1, 2 : Carrier Port 2, 3: EFEM, 4: Aligner 5: PMC, 6 : Buffer
        //public static PlcAddr YW_WaferDataOffset_PositionSlot = new PlcAddr(PlcMemType.S, 1, 0, 1, PlcValueType.SHORT); // Buffer : 1~5, EFEM Upper 1, Lower 2, PMC 1고정, Carrier 1~25        public static PlcAddr YW_WaferDataOffset_CarrierID    = new PlcAddr(PlcMemType.S, 6100, 0, 1, PlcValueType.SHORT); 
        //public static PlcAddr YW_WaferDataOffset_CstID        = new PlcAddr(PlcMemType.S, 2, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr YW_WaferDataOffset_PortNo       = new PlcAddr(PlcMemType.S, 22, 0, 1, PlcValueType.SHORT);        
        //public static PlcAddr YW_WaferDataOffset_LotID        = new PlcAddr(PlcMemType.S, 23, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr YW_WaferDataOffset_SlotNo       = new PlcAddr(PlcMemType.S, 43, 0, 1, PlcValueType.SHORT);
        //public static PlcAddr YW_WaferDataOffset_RecipeID     = new PlcAddr(PlcMemType.S, 44, 0, 30, PlcValueType.ASCII);
        //public static PlcAddr YW_WaferDataOffset_WaferID      = new PlcAddr(PlcMemType.S, 74, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr YW_WaferDataOffset_ControlJobID = new PlcAddr(PlcMemType.S, 94, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr YW_WaferDataOffset_ProcessJobID = new PlcAddr(PlcMemType.S, 114, 0, 20, PlcValueType.ASCII);

        //public static PlcAddr YW_WaferDcollStartAddr          = new PlcAddr(PlcMemType.S, 6700, 0, 459, PlcValueType.ASCII);
        //public static PlcAddr YW_WaferDcollOffset_CstID             = new PlcAddr(PlcMemType.S, 00, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr YW_WaferDcollOffset_PortNo            = new PlcAddr(PlcMemType.S, 20, 0, 1, PlcValueType.SHORT);
        //public static PlcAddr YW_WaferDcollOffset_LotID             = new PlcAddr(PlcMemType.S, 21, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr YW_WaferDcollOffset_SlotNo            = new PlcAddr(PlcMemType.S, 41, 0, 1, PlcValueType.SHORT);
        //public static PlcAddr YW_WaferDcollOffset_RecipeID          = new PlcAddr(PlcMemType.S, 42, 0, 30, PlcValueType.ASCII);
        //public static PlcAddr YW_WaferDcollOffset_WaferID           = new PlcAddr(PlcMemType.S, 72, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr YW_WaferDcollOffset_ControlJobID      = new PlcAddr(PlcMemType.S, 92, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr YW_WaferDcollOffset_ProcessJobID      = new PlcAddr(PlcMemType.S, 112, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr YW_WaferDcollOffset_DcollData_1       = new PlcAddr(PlcMemType.S, 132, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr YW_WaferDcollOffset_DcollData_2       = new PlcAddr(PlcMemType.S, 152, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr YW_WaferDcollOffset_DcollData_3       = new PlcAddr(PlcMemType.S, 172, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr YW_WaferDcollOffset_DcollData_4       = new PlcAddr(PlcMemType.S, 192, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr YW_WaferDcollOffset_DcollData_5       = new PlcAddr(PlcMemType.S, 212, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr YW_WaferDcollOffset_DcollData_6       = new PlcAddr(PlcMemType.S, 232, 0, 20, PlcValueType.ASCII);

        //public static PlcAddr YW_PortNo                       = new PlcAddr(PlcMemType.S, 7160, 0, 1, PlcValueType.SHORT);
        ///// <summary>
        ///// "1 : Read waiting For Host,  2 :Clamped , 3 : ID Read, 4 : ID Verify OK, 5 : Id Verify Fail, 6 : Open
        //// 7 : Slot Map Read, 8 : Slotmap Verify Ok, 9 : SlotMap Verify Fail, 10 : Unload, 11 : Closed
        //// 12 : UnClamped, 13 : Ready to Load, 14: arrived, 15 : Removed 16 : Ready to Unload"
        ///// </summary>
        //public static PlcAddr YW_PortMode                     = new PlcAddr(PlcMemType.S, 7161, 0, 1, PlcValueType.SHORT);
        //public static PlcAddr YW_PortModeCstID                = new PlcAddr(PlcMemType.S, 7162, 0, 20, PlcValueType.ASCII);

        //public static PlcAddr YW_SelectRecipe                 = new PlcAddr(PlcMemType.S, 7210, 0, 30, PlcValueType.ASCII);


        //public static PlcAddr YW_SVIDStartAddr                = new PlcAddr(PlcMemType.S, 9000, 0, 998, PlcValueType.SHORT); // *10000값 전달할 것 (int32)
        //public static PlcAddr YW_MCCStartAddr                 = new PlcAddr(PlcMemType.S, 10300, 0, 998, PlcValueType.SHORT);
        #endregion
        #endregion Ctrl->Cim
        #region Cim->Ctrl
        public static PlcAddr XW_DATE_TIME_SET_CHANGE_BLOCK = new PlcAddr(PlcMemType.S, 10100, 0, 16, PlcValueType.ASCII);

        public static PlcAddr XW_CST_MAP_CST_ID = new PlcAddr(PlcMemType.S, 10120, 0, 30, PlcValueType.ASCII);
        public static PlcAddr XW_CST_MAP_CONFIRM_FLAG = new PlcAddr(PlcMemType.S, 10150, 0, 1, PlcValueType.ASCII);
        public static PlcAddr XW_CST_MAP_LOT_ID = new PlcAddr(PlcMemType.S, 10151, 0, 30, PlcValueType.ASCII);
        public static PlcAddr XW_CST_MAP_OPER = new PlcAddr(PlcMemType.S, 10181, 0, 10, PlcValueType.ASCII);
        public static PlcAddr XW_CST_MAP_EQP_ID = new PlcAddr(PlcMemType.S, 10191, 0, 10, PlcValueType.ASCII);
        public static PlcAddr XW_CST_MAP_PRODUCT_CODE = new PlcAddr(PlcMemType.S, 10201, 0, 30, PlcValueType.ASCII);
        public static PlcAddr XW_CST_MAP_MCP_SALE_CD = new PlcAddr(PlcMemType.S, 10231, 0, 10, PlcValueType.ASCII);
        public static PlcAddr XW_CST_MAP_DEN_TYP = new PlcAddr(PlcMemType.S, 10241, 0, 10, PlcValueType.ASCII);
        public static PlcAddr XW_CST_MAP_TECH_NM = new PlcAddr(PlcMemType.S, 10251, 0, 10, PlcValueType.ASCII);
        public static PlcAddr XW_CST_MAP_PKG_TYP = new PlcAddr(PlcMemType.S, 10261, 0, 10, PlcValueType.ASCII);
        public static PlcAddr XW_CST_MAP_PKG_TYP2 = new PlcAddr(PlcMemType.S, 10271, 0, 10, PlcValueType.ASCII);
        public static PlcAddr XW_CST_MAP_LEAD_CNT = new PlcAddr(PlcMemType.S, 10281, 0, 10, PlcValueType.ASCII);
        //public static PlcAddr XW_CST_MAP_SLOT_LIST                  = new PlcAddr(PlcMemType.S, 10291, 0, 50, PlcValueType.ASCII);
        public static PlcAddr XW_CST_MAP_WAFER_ID_LIST = new PlcAddr(PlcMemType.S, 10341, 0, 500, PlcValueType.ASCII);
        public static PlcAddr XW_CST_MAP_INS_MODE = new PlcAddr(PlcMemType.S, 10841, 0, 10, PlcValueType.ASCII);

        public static PlcAddr XW_LOT_START_CONFIRM_FLAG = new PlcAddr(PlcMemType.S, 11000, 0, 1, PlcValueType.ASCII);

        public static PlcAddr XW_WAFER_START_WAFER_ID = new PlcAddr(PlcMemType.S, 11010, 0, 30, PlcValueType.ASCII);
        public static PlcAddr XW_WAFER_START_CONFIRM_FLAG = new PlcAddr(PlcMemType.S, 11040, 0, 1, PlcValueType.ASCII);



        //public static PlcAddr XW_RECIPE_CMD_MODE                    = new PlcAddr(PlcMemType.S, 300, 0, 16, PlcValueType.ASCII);
        //public static PlcAddr XW_RECIPE_CMD_RECIPE_ID               = new PlcAddr(PlcMemType.S, 300, 0, 16, PlcValueType.ASCII);

        public static PlcAddr XW_OHT_MODE_CHANGE_MODE = new PlcAddr(PlcMemType.S, 11310, 0, 1, PlcValueType.SHORT);
        public static PlcAddr XW_OHT_MODE_CHANGE_PORT_NO = new PlcAddr(PlcMemType.S, 11311, 0, 1, PlcValueType.SHORT);

        public static PlcAddr XW_PORT_RECIPE_SELECT_PORT_NO = new PlcAddr(PlcMemType.S, 11320, 0, 1, PlcValueType.SHORT);
        public static PlcAddr XW_PORT_RECIPE_SELECT_RECIPE_NAME = new PlcAddr(PlcMemType.S, 11321, 0, 50, PlcValueType.ASCII);
        public static PlcAddr XW_PORT_RECIPE_SELECT_RECIPE_CONFIRM = new PlcAddr(PlcMemType.S, 11371, 0, 1, PlcValueType.ASCII);

        public static PlcAddr XW_RCMD_RCMD = new PlcAddr(PlcMemType.S, 11400, 0, 1, PlcValueType.SHORT);

        public static PlcAddr XW_CTRL_MODE_CHANGE_STATE = new PlcAddr(PlcMemType.S, 11410, 0, 1, PlcValueType.SHORT);

        public static PlcAddr XW_WAFER_MAP_WAFER_ID = new PlcAddr(PlcMemType.S, 11420, 0, 30, PlcValueType.ASCII);
        public static PlcAddr XW_WAFER_MAP_LOT_ID = new PlcAddr(PlcMemType.S, 11450, 0, 30, PlcValueType.ASCII);
        public static PlcAddr XW_WAFER_MAP_ID_TYPE = new PlcAddr(PlcMemType.S, 11480, 0, 1, PlcValueType.SHORT);
        public static PlcAddr XW_WAFER_MAP_PRODUCT_CODE = new PlcAddr(PlcMemType.S, 11481, 0, 30, PlcValueType.ASCII);
        public static PlcAddr XW_WAFER_MAP_COLUMNS = new PlcAddr(PlcMemType.S, 11511, 0, 1, PlcValueType.SHORT);
        public static PlcAddr XW_WAFER_MAP_ROWS = new PlcAddr(PlcMemType.S, 11512, 0, 1, PlcValueType.SHORT);
        public static PlcAddr XW_WAFER_MAP_BIN_GOOD_QTY = new PlcAddr(PlcMemType.S, 11513, 0, 30, PlcValueType.ASCII);
        public static PlcAddr XW_WAFER_MAP_FLAT_ZONE = new PlcAddr(PlcMemType.S, 11543, 0, 10, PlcValueType.ASCII);
        public static PlcAddr XW_DEEP_LEARNING_REVIEW_CST_ID = new PlcAddr(PlcMemType.S, 11553, 0, 30, PlcValueType.ASCII);
        public static PlcAddr XW_DEEP_LEARNING_REVIEW_SLOT = new PlcAddr(PlcMemType.S, 11583, 0, 1, PlcValueType.SHORT);

        public static PlcAddr XW_MOVE_OUT_FLAG_LOT_ID = new PlcAddr(PlcMemType.S, 13560, 0, 30, PlcValueType.ASCII);
        public static PlcAddr XW_MOVE_OUT_FLAG_CONFIRM_FLAG = new PlcAddr(PlcMemType.S, 13590, 0, 1, PlcValueType.ASCII);
        public static PlcAddr XW_ACK = new PlcAddr(PlcMemType.S, 14000, 0, 1, PlcValueType.SHORT);
        public static PlcAddr XW_WAFER_AUTO_MOVE_OUT = new PlcAddr(PlcMemType.S, 14001, 0, 30, PlcValueType.ASCII);
        public static PlcAddr XW_WAFER_AUTO_MOVE_OUT_CST_ID = new PlcAddr(PlcMemType.S, 14002, 0, 30, PlcValueType.ASCII);

        public static PlcAddr XW_TerminalMsgCount = new PlcAddr(PlcMemType.S, 14002, 0, 002, PlcValueType.SHORT);
        public static PlcAddr XW_TerminalMsg1 = new PlcAddr(PlcMemType.S, 14004, 0, 100, PlcValueType.ASCII);
        public static PlcAddr XW_TerminalMsg2 = new PlcAddr(PlcMemType.S, 14104, 0, 100, PlcValueType.ASCII);
        public static PlcAddr XW_TerminalMsg3 = new PlcAddr(PlcMemType.S, 14204, 0, 100, PlcValueType.ASCII);
        public static PlcAddr XW_TerminalMsg4 = new PlcAddr(PlcMemType.S, 14304, 0, 100, PlcValueType.ASCII);
        public static PlcAddr XW_TerminalMsg5 = new PlcAddr(PlcMemType.S, 14404, 0, 100, PlcValueType.ASCII);
        public static PlcAddr XW_TerminalMsg6 = new PlcAddr(PlcMemType.S, 14504, 0, 100, PlcValueType.ASCII);
        public static PlcAddr XW_TerminalMsg7 = new PlcAddr(PlcMemType.S, 14604, 0, 100, PlcValueType.ASCII);
        public static PlcAddr XW_TerminalMsg8 = new PlcAddr(PlcMemType.S, 14704, 0, 100, PlcValueType.ASCII);
        public static PlcAddr XW_TerminalMsg9 = new PlcAddr(PlcMemType.S, 14804, 0, 100, PlcValueType.ASCII);
        public static PlcAddr XW_TerminalMsg10 = new PlcAddr(PlcMemType.S, 14904, 0, 096, PlcValueType.ASCII);

        #region 이전
        //public static PlcAddr XW_TimeSet                      = new PlcAddr(PlcMemType.S, 300, 0, 16, PlcValueType.ASCII);
        //public static PlcAddr XW_CurPPIDName                  = new PlcAddr(PlcMemType.S, 320, 0, 50, PlcValueType.ASCII);
        //public static PlcAddr XW_HostRcpMode                  = new PlcAddr(PlcMemType.S, 370, 0, 1, PlcValueType.SHORT);
        //public static PlcAddr XW_RecipeName                   = new PlcAddr(PlcMemType.S, 371, 0, 30, PlcValueType.ASCII);

        //public static PlcAddr XW_CstID                        = new PlcAddr(PlcMemType.S, 500, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_CstPort                      = new PlcAddr(PlcMemType.S, 520, 0, 1, PlcValueType.SHORT);

        //public static PlcAddr XW_LotID01                      = new PlcAddr(PlcMemType.S, 521, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_WaferID01                    = new PlcAddr(PlcMemType.S, 541, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_LotID02                      = new PlcAddr(PlcMemType.S, 561, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_WaferID02                    = new PlcAddr(PlcMemType.S, 581, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_LotID03                      = new PlcAddr(PlcMemType.S, 601, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_WaferID03                    = new PlcAddr(PlcMemType.S, 621, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_LotID04                      = new PlcAddr(PlcMemType.S, 641, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_WaferID04                    = new PlcAddr(PlcMemType.S, 661, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_LotID05                      = new PlcAddr(PlcMemType.S, 681, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_WaferID05                    = new PlcAddr(PlcMemType.S, 701, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_LotID06                      = new PlcAddr(PlcMemType.S, 721, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_WaferID06                    = new PlcAddr(PlcMemType.S, 741, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_LotID07                      = new PlcAddr(PlcMemType.S, 761, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_WaferID07                    = new PlcAddr(PlcMemType.S, 781, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_LotID08                      = new PlcAddr(PlcMemType.S, 801, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_WaferID08                    = new PlcAddr(PlcMemType.S, 821, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_LotID09                      = new PlcAddr(PlcMemType.S, 841, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_WaferID09                    = new PlcAddr(PlcMemType.S, 861, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_LotID10                      = new PlcAddr(PlcMemType.S, 881, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_WaferID10                    = new PlcAddr(PlcMemType.S, 901, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_LotID11                      = new PlcAddr(PlcMemType.S, 921, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_WaferID11                    = new PlcAddr(PlcMemType.S, 941, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_LotID12                      = new PlcAddr(PlcMemType.S, 961, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_WaferID12                    = new PlcAddr(PlcMemType.S, 981, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_LotID13                      = new PlcAddr(PlcMemType.S, 1001, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_WaferID13                    = new PlcAddr(PlcMemType.S, 1021, 0, 20, PlcValueType.ASCII);

        //public static PlcAddr XW_ProcessJobID                 = new PlcAddr(PlcMemType.S, 1600, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_ProcessJobCstID              = new PlcAddr(PlcMemType.S, 1620, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_ProcessJobRcpName            = new PlcAddr(PlcMemType.S, 1640, 0, 30, PlcValueType.ASCII);
        //public static PlcAddr XW_ProcessJobSlot               = new PlcAddr(PlcMemType.S, 1670, 0, 25, PlcValueType.ASCII);

        //public static PlcAddr XW_ControlJobID                 = new PlcAddr(PlcMemType.S, 1800, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_ControlJobCstID              = new PlcAddr(PlcMemType.S, 1820, 0, 20, PlcValueType.ASCII);
        //public static PlcAddr XW_ControlJobProcJobID          = new PlcAddr(PlcMemType.S, 1840, 0, 30, PlcValueType.ASCII);

        ////public static PlcAddr TerminalMsgCount              = new PlcAddr(PlcMemType.S, 320, 0, 001, PlcValueType.SHORT);
        ////public static PlcAddr TerminalMsg1                  = new PlcAddr(PlcMemType.S, 321, 0, 100, PlcValueType.ASCII);
        ////public static PlcAddr TerminalMsg2                  = new PlcAddr(PlcMemType.S, 421, 0, 100, PlcValueType.ASCII);
        ////public static PlcAddr TerminalMsg3                  = new PlcAddr(PlcMemType.S, 521, 0, 100, PlcValueType.ASCII);
        ////public static PlcAddr TerminalMsg4                  = new PlcAddr(PlcMemType.S, 621, 0, 100, PlcValueType.ASCII);
        ////public static PlcAddr TerminalMsg5                  = new PlcAddr(PlcMemType.S, 721, 0, 100, PlcValueType.ASCII);
        #endregion
        #endregion Cim->Ctrl

        static CIMAW()
        {

        }

        public static void Initailize(IVirtualMem plc)
        {
            YW_CST_LOAD_REQUEST_CST_ID.PLC = plc;
            YW_CST_LOAD_REQUEST_PORT_ID.PLC = plc;
            YW_CST_LOAD_REQUEST_CUR_RECIPE.PLC = plc;
            YW_LOT_START_CST_ID.PLC = plc;
            YW_LOT_START_PORT_ID.PLC = plc;
            YW_LOT_START_PORT_RECIPE.PLC = plc;
            YW_LOT_START_WAFER_COUNT.PLC = plc;
            YW_WAFER_LOAD_REQUEST_WAFER_ID.PLC = plc;
            YW_WAFER_LOAD_REQUEST_CST_ID.PLC = plc;
            YW_WAFER_LOAD_REQUEST_PORT_ID.PLC = plc;
            YW_WAFER_LOAD_REQUEST_SLOT_NO.PLC = plc;
            YW_WAFER_LOAD_REQUEST_CURRENT_RECIPE.PLC = plc;
            YW_WAFER_MAP_REQUEST_WAFER_ID.PLC = plc;
            YW_WAFER_MAP_REQUEST_ID_TYPE.PLC = plc;
            YW_WAFER_MAP_REQUEST_MAP_FORMAT.PLC = plc;
            YW_WAFER_MAP_REQUEST_BIN_CODE.PLC = plc;
            YW_WAFER_MAP_REQUEST_NULL_BIN_CODE.PLC = plc;
            YW_WAFER_UNLOAD_WAFER_ID.PLC = plc;
            YW_WAFER_UNLOAD_CST_ID.PLC = plc;
            YW_WAFER_UNLOAD_PORT_ID.PLC = plc;
            YW_WAFER_UNLOAD_SLOT_NO.PLC = plc;
            YW_WAFER_UNLOAD_START_TIME.PLC = plc;
            YW_WAFER_UNLOAD_END_TIME.PLC = plc;
            YW_WAFER_UNLOAD_IS_LAST.PLC = plc;
            YW_CST_UNLOAD_CST_ID.PLC = plc;
            YW_CST_UNLOAD_PORT_ID.PLC = plc;
            YW_CST_UNLOAD_CUR_RECIPE.PLC = plc;
            YW_CST_UNLOAD_TOTAL_WAFER_COUNT.PLC = plc;
            YW_CST_UNLOAD_INSPECTION_WAFER_COUNT.PLC = plc;
            YW_CST_UNLOAD_SLOT_INFO1.PLC = plc;
            YW_CST_UNLOAD_SLOT_INFO2.PLC = plc;
            YW_CST_UNLOAD_SLOT_INFO3.PLC = plc;
            YW_CST_UNLOAD_SLOT_INFO4.PLC = plc;
            YW_CST_UNLOAD_SLOT_INFO5.PLC = plc;
            YW_CST_UNLOAD_SLOT_INFO6.PLC = plc;
            YW_CST_UNLOAD_SLOT_INFO7.PLC = plc;
            YW_CST_UNLOAD_SLOT_INFO8.PLC = plc;
            YW_CST_UNLOAD_SLOT_INFO9.PLC = plc;
            YW_CST_UNLOAD_SLOT_INFO10.PLC = plc;
            YW_CST_UNLOAD_SLOT_INFO11.PLC = plc;
            YW_CST_UNLOAD_SLOT_INFO12.PLC = plc;
            YW_CST_UNLOAD_SLOT_INFO13.PLC = plc;
            YW_ALARM_FLAG.PLC = plc;
            YW_ALARM_ID.PLC = plc;
            YW_ALARM_TEXT.PLC = plc;
            YW_RECIPE_MODE.PLC = plc;
            YW_RECIPE_ID.PLC = plc;
            YW_EQP_STATUS.PLC = plc;
            YW_EQP_STATUS_CURRENT_RECIPE.PLC = plc;
            YW_EQP_STATUS_CST_ID.PLC = plc;
            YW_OHT_MODE_AUTO_MODE.PLC = plc;
            YW_OHT_MODE_LOAD1_STATUS.PLC = plc;
            YW_OHT_MODE_UNLOAD1_STATUS.PLC = plc;
            YW_OHT_MODE_LOAD2_STATUS.PLC = plc;
            YW_OHT_MODE_UNLOAD2_STATUS.PLC = plc;
            YW_PORT_REQEST_NO.PLC = plc;
            YW_PORT_REQEST_MODE.PLC = plc;
            YW_PORT_REQEST_STATUS.PLC = plc;
            YW_PORT_REQEST_CST_ID.PLC = plc;
            YW_EQP_MODE_CHANGE.PLC = plc;
            YW_EQP_MODE_CHANGE_CTRL_MODE.PLC = plc;
            YW_OS_VERSION.PLC = plc;
            XW_DATE_TIME_SET_CHANGE_BLOCK.PLC = plc;
            XW_CST_MAP_CST_ID.PLC = plc;
            XW_CST_MAP_CONFIRM_FLAG.PLC = plc;
            XW_CST_MAP_LOT_ID.PLC = plc;
            XW_CST_MAP_OPER.PLC = plc;
            XW_CST_MAP_EQP_ID.PLC = plc;
            XW_CST_MAP_PRODUCT_CODE.PLC = plc;
            XW_CST_MAP_MCP_SALE_CD.PLC = plc;
            XW_CST_MAP_DEN_TYP.PLC = plc;
            XW_CST_MAP_TECH_NM.PLC = plc;
            XW_CST_MAP_PKG_TYP.PLC = plc;
            XW_CST_MAP_PKG_TYP2.PLC = plc;
            XW_CST_MAP_LEAD_CNT.PLC = plc;
            //XW_CST_MAP_SLOT_LIST.PLC =
            XW_CST_MAP_WAFER_ID_LIST.PLC = plc;
            XW_CST_MAP_INS_MODE.PLC = plc;
            XW_LOT_START_CONFIRM_FLAG.PLC = plc;
            XW_WAFER_START_WAFER_ID.PLC = plc;
            XW_WAFER_START_CONFIRM_FLAG.PLC = plc;
            XW_CTRL_MODE_CHANGE_STATE.PLC = plc;
            XW_WAFER_MAP_WAFER_ID.PLC = plc;
            XW_WAFER_MAP_LOT_ID.PLC = plc;
            //XW_RECIPE_CMD_MODE.PLC =
            //XW_RECIPE_CMD_RECIPE_ID.PLC =
            XW_OHT_MODE_CHANGE_MODE.PLC = plc;
            XW_OHT_MODE_CHANGE_PORT_NO.PLC = plc;
            XW_PORT_RECIPE_SELECT_PORT_NO.PLC = plc;
            XW_PORT_RECIPE_SELECT_RECIPE_NAME.PLC = plc;
            XW_PORT_RECIPE_SELECT_RECIPE_CONFIRM.PLC = plc;
            YW_CST_UNLOAD_SLOT_INFO_COUNT.PLC = plc;
            YW_ACK_RECIPE_MODE.PLC = plc;
            YW_ACK_RECIPE_ACK.PLC = plc;
            YW_ACK_PORT_RECIPE_SELECT.PLC = plc;
            YW_ACK_ECV_CHANGE_ACK.PLC = plc;
            YW_ACK_PORT_NO.PLC = plc;
            YW_ACK_OHT_CHANGE_ACK.PLC = plc;
            YW_ACK_RCMD_MODE.PLC = plc;
            YW_ACK_RCMD_ACK.PLC = plc;
            XW_RCMD_RCMD.PLC = plc;

            XW_WAFER_MAP_WAFER_ID.PLC = plc;
            XW_WAFER_MAP_LOT_ID.PLC = plc;
            XW_WAFER_MAP_ID_TYPE.PLC = plc;
            XW_WAFER_MAP_PRODUCT_CODE.PLC = plc;
            XW_WAFER_MAP_COLUMNS.PLC = plc;
            XW_WAFER_MAP_ROWS.PLC = plc;
            XW_WAFER_MAP_BIN_GOOD_QTY.PLC = plc;
            XW_WAFER_MAP_FLAT_ZONE.PLC = plc;
            XW_DEEP_LEARNING_REVIEW_CST_ID.PLC = plc;
            XW_DEEP_LEARNING_REVIEW_SLOT.PLC = plc;
            XW_MOVE_OUT_FLAG_LOT_ID.PLC = plc;
            XW_MOVE_OUT_FLAG_CONFIRM_FLAG.PLC = plc;
            XW_ACK.PLC = plc;

            XW_TerminalMsgCount.PLC = plc;
            XW_TerminalMsg1.PLC = plc;
            XW_TerminalMsg2.PLC = plc;
            XW_TerminalMsg3.PLC = plc;
            XW_TerminalMsg4.PLC = plc;
            XW_TerminalMsg5.PLC = plc;
            XW_TerminalMsg6.PLC = plc;
            XW_TerminalMsg7.PLC = plc;
            XW_TerminalMsg8.PLC = plc;
            XW_TerminalMsg9.PLC = plc;
            XW_TerminalMsg10.PLC = plc;
            XW_WAFER_AUTO_MOVE_OUT.PLC = plc;
            XW_WAFER_AUTO_MOVE_OUT_CST_ID.PLC = plc;

            YW_CST_UNLOAD_LOT_ID.PLC = plc;

            YW_KerfData_Right_Col_1.PLC = plc;
            YW_KerfData_Right_Row_1.PLC = plc;
            YW_KerfData_Right_1.PLC = plc;
            YW_KerfData_Right_Col_2.PLC = plc;
            YW_KerfData_Right_Row_2.PLC = plc;
            YW_KerfData_Right_2.PLC = plc;
            YW_KerfData_Right_Col_3.PLC = plc;
            YW_KerfData_Right_Row_3.PLC = plc;
            YW_KerfData_Right_3.PLC = plc;
            YW_KerfData_Right_Col_4.PLC = plc;
            YW_KerfData_Right_Row_4.PLC = plc;
            YW_KerfData_Right_4.PLC = plc;
            YW_KerfData_Right_Col_5.PLC = plc;
            YW_KerfData_Right_Row_5.PLC = plc;
            YW_KerfData_Right_5.PLC = plc;
            YW_KerfData_Right_Col_6.PLC = plc;
            YW_KerfData_Right_Row_6.PLC = plc;
            YW_KerfData_Right_6.PLC = plc;
            YW_KerfData_Right_Col_7.PLC = plc;
            YW_KerfData_Right_Row_7.PLC = plc;
            YW_KerfData_Right_7.PLC = plc;
            YW_KerfData_Right_Col_8.PLC = plc;
            YW_KerfData_Right_Row_8.PLC = plc;
            YW_KerfData_Right_8.PLC = plc;
            YW_KerfData_Right_Col_9.PLC = plc;
            YW_KerfData_Right_Row_9.PLC = plc;
            YW_KerfData_Right_9.PLC = plc;
            YW_KerfData_Right_Col_10.PLC = plc;
            YW_KerfData_Right_Row_10.PLC = plc;
            YW_KerfData_Right_10.PLC = plc;
            YW_KerfData_Right_Col_11.PLC = plc;
            YW_KerfData_Right_Row_11.PLC = plc;
            YW_KerfData_Right_11.PLC = plc;
            YW_KerfData_Right_Col_12.PLC = plc;
            YW_KerfData_Right_Row_12.PLC = plc;
            YW_KerfData_Right_12.PLC = plc;
            YW_KerfData_Right_Col_13.PLC = plc;
            YW_KerfData_Right_Row_13.PLC = plc;
            YW_KerfData_Right_13.PLC = plc;
            YW_KerfData_Right_Col_14.PLC = plc;
            YW_KerfData_Right_Row_14.PLC = plc;
            YW_KerfData_Right_14.PLC = plc;
            YW_KerfData_Right_Col_15.PLC = plc;
            YW_KerfData_Right_Row_15.PLC = plc;
            YW_KerfData_Right_15.PLC = plc;
            YW_KerfData_Right_Col_16.PLC = plc;
            YW_KerfData_Right_Row_16.PLC = plc;
            YW_KerfData_Right_16.PLC = plc;
            YW_KerfData_Right_Col_17.PLC = plc;
            YW_KerfData_Right_Row_17.PLC = plc;
            YW_KerfData_Right_17.PLC = plc;
            YW_KerfData_Right_Col_18.PLC = plc;
            YW_KerfData_Right_Row_18.PLC = plc;
            YW_KerfData_Right_18.PLC = plc;
            YW_KerfData_Right_Col_19.PLC = plc;
            YW_KerfData_Right_Row_19.PLC = plc;
            YW_KerfData_Right_19.PLC = plc;
            YW_KerfData_Right_Col_20.PLC = plc;
            YW_KerfData_Right_Row_20.PLC = plc;
            YW_KerfData_Right_20.PLC = plc;
            YW_KerfData_Bottom_Col_1.PLC = plc;
            YW_KerfData_Bottom_Row_1.PLC = plc;
            YW_KerfData_Bottom_1.PLC = plc;
            YW_KerfData_Bottom_Col_2.PLC = plc;
            YW_KerfData_Bottom_Row_2.PLC = plc;
            YW_KerfData_Bottom_2.PLC = plc;
            YW_KerfData_Bottom_Col_3.PLC = plc;
            YW_KerfData_Bottom_Row_3.PLC = plc;
            YW_KerfData_Bottom_3.PLC = plc;
            YW_KerfData_Bottom_Col_4.PLC = plc;
            YW_KerfData_Bottom_Row_4.PLC = plc;
            YW_KerfData_Bottom_4.PLC = plc;
            YW_KerfData_Bottom_Col_5.PLC = plc;
            YW_KerfData_Bottom_Row_5.PLC = plc;
            YW_KerfData_Bottom_5.PLC = plc;
            YW_KerfData_Bottom_Col_6.PLC = plc;
            YW_KerfData_Bottom_Row_6.PLC = plc;
            YW_KerfData_Bottom_6.PLC = plc;
            YW_KerfData_Bottom_Col_7.PLC = plc;
            YW_KerfData_Bottom_Row_7.PLC = plc;
            YW_KerfData_Bottom_7.PLC = plc;
            YW_KerfData_Bottom_Col_8.PLC = plc;
            YW_KerfData_Bottom_Row_8.PLC = plc;
            YW_KerfData_Bottom_8.PLC = plc;
            YW_KerfData_Bottom_Col_9.PLC = plc;
            YW_KerfData_Bottom_Row_9.PLC = plc;
            YW_KerfData_Bottom_9.PLC = plc;
            YW_KerfData_Bottom_Col_10.PLC = plc;
            YW_KerfData_Bottom_Row_10.PLC = plc;
            YW_KerfData_Bottom_10.PLC = plc;
            YW_KerfData_Bottom_Col_11.PLC = plc;
            YW_KerfData_Bottom_Row_11.PLC = plc;
            YW_KerfData_Bottom_11.PLC = plc;
            YW_KerfData_Bottom_Col_12.PLC = plc;
            YW_KerfData_Bottom_Row_12.PLC = plc;
            YW_KerfData_Bottom_12.PLC = plc;
            YW_KerfData_Bottom_Col_13.PLC = plc;
            YW_KerfData_Bottom_Row_13.PLC = plc;
            YW_KerfData_Bottom_13.PLC = plc;
            YW_KerfData_Bottom_Col_14.PLC = plc;
            YW_KerfData_Bottom_Row_14.PLC = plc;
            YW_KerfData_Bottom_14.PLC = plc; 
            YW_KerfData_Bottom_Col_15.PLC = plc;
            YW_KerfData_Bottom_Row_15.PLC = plc;
            YW_KerfData_Bottom_15.PLC = plc;
            YW_KerfData_Bottom_Col_16.PLC = plc;
            YW_KerfData_Bottom_Row_16.PLC = plc;
            YW_KerfData_Bottom_16.PLC = plc;
            YW_KerfData_Bottom_Col_17.PLC = plc;
            YW_KerfData_Bottom_Row_17.PLC = plc;
            YW_KerfData_Bottom_17.PLC = plc;
            YW_KerfData_Bottom_Col_18.PLC = plc;
            YW_KerfData_Bottom_Row_18.PLC = plc;
            YW_KerfData_Bottom_18.PLC = plc;
            YW_KerfData_Bottom_Col_19.PLC = plc;
            YW_KerfData_Bottom_Row_19.PLC = plc;
            YW_KerfData_Bottom_19.PLC = plc;
            YW_KerfData_Bottom_Col_20.PLC = plc;
            YW_KerfData_Bottom_Row_20.PLC = plc;
            YW_KerfData_Bottom_20.PLC = plc;

            YW_KERF_DATA_01_DIE_COL.PLC = plc;
            YW_KERF_DATA_01_DIE_ROW.PLC = plc;
            YW_KERF_DATA_01_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_01_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_01_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_01_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_02_DIE_COL.PLC = plc;
            YW_KERF_DATA_02_DIE_ROW.PLC = plc;
            YW_KERF_DATA_02_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_02_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_02_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_02_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_03_DIE_COL.PLC = plc;
            YW_KERF_DATA_03_DIE_ROW.PLC = plc;
            YW_KERF_DATA_03_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_03_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_03_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_03_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_04_DIE_COL.PLC = plc;
            YW_KERF_DATA_04_DIE_ROW.PLC = plc;
            YW_KERF_DATA_04_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_04_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_04_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_04_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_05_DIE_COL.PLC = plc;
            YW_KERF_DATA_05_DIE_ROW.PLC = plc;
            YW_KERF_DATA_05_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_05_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_05_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_05_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_06_DIE_COL.PLC = plc;
            YW_KERF_DATA_06_DIE_ROW.PLC = plc;
            YW_KERF_DATA_06_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_06_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_06_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_06_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_07_DIE_COL.PLC = plc;
            YW_KERF_DATA_07_DIE_ROW.PLC = plc;
            YW_KERF_DATA_07_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_07_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_07_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_07_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_08_DIE_COL.PLC = plc; 
            YW_KERF_DATA_08_DIE_ROW.PLC = plc;
            YW_KERF_DATA_08_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_08_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_08_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_08_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_09_DIE_COL.PLC = plc;
            YW_KERF_DATA_09_DIE_ROW.PLC = plc;
            YW_KERF_DATA_09_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_09_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_09_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_09_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_10_DIE_COL.PLC = plc;
            YW_KERF_DATA_10_DIE_ROW.PLC = plc;
            YW_KERF_DATA_10_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_10_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_10_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_10_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_11_DIE_COL.PLC = plc;
            YW_KERF_DATA_11_DIE_ROW.PLC = plc;
            YW_KERF_DATA_11_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_11_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_11_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_11_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_12_DIE_COL.PLC = plc;
            YW_KERF_DATA_12_DIE_ROW.PLC = plc;
            YW_KERF_DATA_12_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_12_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_12_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_12_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_13_DIE_COL.PLC = plc;
            YW_KERF_DATA_13_DIE_ROW.PLC = plc;
            YW_KERF_DATA_13_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_13_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_13_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_13_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_14_DIE_COL.PLC = plc;
            YW_KERF_DATA_14_DIE_ROW.PLC = plc;
            YW_KERF_DATA_14_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_14_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_14_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_14_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_15_DIE_COL.PLC = plc;
            YW_KERF_DATA_15_DIE_ROW.PLC = plc;
            YW_KERF_DATA_15_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_15_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_15_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_15_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_16_DIE_COL.PLC = plc;
            YW_KERF_DATA_16_DIE_ROW.PLC = plc;
            YW_KERF_DATA_16_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_16_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_16_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_16_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_17_DIE_COL.PLC = plc;
            YW_KERF_DATA_17_DIE_ROW.PLC = plc;
            YW_KERF_DATA_17_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_17_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_17_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_17_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_18_DIE_COL.PLC = plc;
            YW_KERF_DATA_18_DIE_ROW.PLC = plc;
            YW_KERF_DATA_18_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_18_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_18_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_18_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_19_DIE_COL.PLC = plc;
            YW_KERF_DATA_19_DIE_ROW.PLC = plc;
            YW_KERF_DATA_19_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_19_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_19_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_19_WIDTH_LEFT.PLC = plc;
            YW_KERF_DATA_20_DIE_COL.PLC = plc;
            YW_KERF_DATA_20_DIE_ROW.PLC = plc;
            YW_KERF_DATA_20_WIDTH_TOP.PLC = plc;
            YW_KERF_DATA_20_WIDTH_RIGHT.PLC = plc;
            YW_KERF_DATA_20_WIDTH_BOTTOM.PLC = plc;
            YW_KERF_DATA_20_WIDTH_LEFT.PLC = plc;
        }
    }
}
