using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.Detail.HSMS
{
    public enum EmHsmsPcCommand
    {
        CASSETTE_LOAD,
        LOT_START,       
        WAFER_LOAD,
        WAFER_MAP_REQUEST,
        WAFER_UNLOAD,  
        CASSETTE_UNLOAD,
        ALARM_REPORT,    
        RECIPE_SELECT,
        EQP_STATUS_CHANGE,
        OS_VERSION_REPORT,
        ECID_CHANGE,  
        OHT_MODE_CHANGE,
        PORT_MODE_CHANGE,
        EQP_MODE_CHANGE,
        CTRL_MODE_CHANGE,
    }
    public enum EmHsmsPcEvent
    {
        CST_MAP,
        PP_SELECT,
        LOT_START,
        WAFER_START,
        MAP_FILE_CREATE,
        RCMD,
        ECID_EDIT,
        OHT_MODE_CHANGE,
        CTRL_MODE_CHANGE,
        CST_MOVE_OUT_FLAG,
        TERMINAL_MSG,
        DEEP_LEARNING_REVIEW_COMPLETE,
        AUTO_MOVE_OUT,
    }
    public enum EmHsmsEqpMode
    {
        MANUAL = 1,
        SEMI_AUTO = 2, //미사용
        AUTO,
    }
    public enum EmHsmsWaferMapNG
    {
        AutoStop,
        Continue,
        Retry,
    }

    public enum EmHsmsAck
    {
        OK,
        OVERLAP,
        NACK,
    }

    public enum EmHsmsCtrlMode
    {
        OFFLINE = 1,
        LOCAL = 2,
        REMOTE = 3,
    }

    public enum EmHsmsCtrlAck
    {
        OK = 0,
        OVERLAP = 1,
        NOT_GEM_DEFINE = 2,
        NOT_VALUE = 3,
        TERMINAL_MSG = 4,
    }
}
