using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;

namespace DitCim.PLC
{
    public class CIMAB
    {
        #region CTRL->CIM
        public static PlcAddr YB_CST_MAP_REPLY                      = new PlcAddr(PlcMemType.S, 51, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_LOT_START_REPLY                    = new PlcAddr(PlcMemType.S, 52, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_WAFER_START_REPLY                  = new PlcAddr(PlcMemType.S, 53, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_WAFER_MAP_REPLY                    = new PlcAddr(PlcMemType.S, 54, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_ECID_CHANGE_REPLY                  = new PlcAddr(PlcMemType.S, 56, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_PORT_RECIPE_REPLY                  = new PlcAddr(PlcMemType.S, 57, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_RCMD_REPLY                         = new PlcAddr(PlcMemType.S, 58, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_CTRL_STATE_CHANGE_REPLY            = new PlcAddr(PlcMemType.S, 59, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_MOVE_OUT_FLAG_REPLY                = new PlcAddr(PlcMemType.S, 60, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_TERMINA_MSG_REPLY                  = new PlcAddr(PlcMemType.S, 61, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_OHT_MODE_CHANGE_REPLY              = new PlcAddr(PlcMemType.S, 62, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_DEEP_LEARNING_REVIEW_COMPLTE_REPLY = new PlcAddr(PlcMemType.S, 63, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_AUTO_MOVE_OUT_REPLY                = new PlcAddr(PlcMemType.S, 64, 0, 1, PlcValueType.BIT);

        public static PlcAddr YB_CST_LOAD_REPORT            = new PlcAddr(PlcMemType.S, 75, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_LOT_START_REPORT           = new PlcAddr(PlcMemType.S, 76, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_WAFER_LOAD_REPORT          = new PlcAddr(PlcMemType.S, 77, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_WAFER_MAP_REQUEST_REPORT   = new PlcAddr(PlcMemType.S, 78, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_WAFER_UNLOAD_REPORT        = new PlcAddr(PlcMemType.S, 79, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_CST_UNLOAD_REPORT          = new PlcAddr(PlcMemType.S, 80, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_ALARM_REPORT               = new PlcAddr(PlcMemType.S, 83, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_EQ_STATUS_CHANGE_REPORT    = new PlcAddr(PlcMemType.S, 84, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_RECIPE_SELECT_REPORT       = new PlcAddr(PlcMemType.S, 85, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_ECID_CHANGE_REPORT         = new PlcAddr(PlcMemType.S, 86, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_CTRL_MODE_CHANGE_REPORT    = new PlcAddr(PlcMemType.S, 88, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_OS_VERSION_REPORT          = new PlcAddr(PlcMemType.S, 89, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_OHT_MODE_CHANGE_REPORT     = new PlcAddr(PlcMemType.S, 90, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_PORT_MODE_CHANGE_REPORT    = new PlcAddr(PlcMemType.S, 91, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_EQP_MODE_CHANGE_REPORT     = new PlcAddr(PlcMemType.S, 92, 0, 1, PlcValueType.BIT);
        public static PlcAddr YB_ALARM_EXIST_REPORT         = new PlcAddr(PlcMemType.S, 98, 0, 1, PlcValueType.BIT);
        #region 이전
        //public static PlcAddr YB_DateTimeReply                                   = new PlcAddr(PlcMemType.S, 5000, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_TerminalMessageReply                            = new PlcAddr(PlcMemType.S, 5001, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_EcidChangeReply                                 = new PlcAddr(PlcMemType.S, 5002, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_RecipeParameterChangeReply                      = new PlcAddr(PlcMemType.S, 5003, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_PPIDCMDReply                                    = new PlcAddr(PlcMemType.S, 5004, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_APCReply                                        = new PlcAddr(PlcMemType.S, 5005, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_NormalReply                                     = new PlcAddr(PlcMemType.S, 5006, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_PMReply                                         = new PlcAddr(PlcMemType.S, 5007, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_PauseReply                                      = new PlcAddr(PlcMemType.S, 5008, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_ResumeReply                                     = new PlcAddr(PlcMemType.S, 5009, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_CassetteMapReply                                = new PlcAddr(PlcMemType.S, 5010, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_ProcessSlotReply                                = new PlcAddr(PlcMemType.S, 5011, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_ControlJobReply                                 = new PlcAddr(PlcMemType.S, 5012, 0, 1, PlcValueType.BIT);

        //public static PlcAddr YB_DateTimeSyncReport                              = new PlcAddr(PlcMemType.S, 5050, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_AlarmReport                                     = new PlcAddr(PlcMemType.S, 5051, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_RecipeCMDReport                                 = new PlcAddr(PlcMemType.S, 5052, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_WaferInReport                                   = new PlcAddr(PlcMemType.S, 5053, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_WaferOutReport                                  = new PlcAddr(PlcMemType.S, 5054, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_WaferDcollReport                                = new PlcAddr(PlcMemType.S, 5055, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_EcidChangeReport                                = new PlcAddr(PlcMemType.S, 5056, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_StateChangeReport                               = new PlcAddr(PlcMemType.S, 5057, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_WaferScrapReport                                = new PlcAddr(PlcMemType.S, 5058, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_PortModeReport                                  = new PlcAddr(PlcMemType.S, 5059, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_ProcessStepReport                               = new PlcAddr(PlcMemType.S, 5060, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_CassetteLoadReport                               = new PlcAddr(PlcMemType.S, 5061, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_CassetteSlotMappingReport                        = new PlcAddr(PlcMemType.S, 5062, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_SelectRecipeChangeReport                        = new PlcAddr(PlcMemType.S, 5063, 0, 1, PlcValueType.BIT);

        //public static PlcAddr YB_AliveBit                                        = new PlcAddr(PlcMemType.S, 5100, 0, 1, PlcValueType.BIT); // 0 : 0ff / 1 : 0n(항상 0.5sec On / 0.5 sec 0ff )
        //public static PlcAddr YB_TerminalState                                   = new PlcAddr(PlcMemType.S, 5101, 0, 1, PlcValueType.BIT);
        ////public static PlcAddr YB_EQRealTimeDataEvent                           = new PlcAddr(PlcMemType.S, 5101, 0, 1, PlcValueType.BIT); // FDC Bit(항상 0.5sec On / 0.5sec 0ff )
        //public static PlcAddr YB_AlarmExistBit                                   = new PlcAddr(PlcMemType.S, 5102, 0, 1, PlcValueType.BIT); // 0 : 알람 없음, 1: 알람 있음
        //public static PlcAddr YB_AutoMode                                        = new PlcAddr(PlcMemType.S, 5103, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_SemiAutoMode                                    = new PlcAddr(PlcMemType.S, 5104, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_ManualMode                                      = new PlcAddr(PlcMemType.S, 5105, 0, 1, PlcValueType.BIT);

        //public static PlcAddr YB_Red                                             = new PlcAddr(PlcMemType.S, 5109, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_Yellow                                          = new PlcAddr(PlcMemType.S, 5110, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_Green                                           = new PlcAddr(PlcMemType.S, 5111, 0, 1, PlcValueType.BIT);
        //public static PlcAddr YB_Buzzer                                          = new PlcAddr(PlcMemType.S, 5112, 0, 1, PlcValueType.BIT);

        //public static PlcAddr YB_RecipeCmdAckReport                              = new PlcAddr(PlcMemType.S, 5200, 0, 1, PlcValueType.BIT);

        //public static PlcAddr YB_AlarmSet                                        = new PlcAddr(PlcMemType.S, 5300, 7, 1, PlcValueType.BIT);
        #endregion
        #endregion
        #region CIM->CTRL
        public static PlcAddr XB_DATE_TIME_COMMAND                       = new PlcAddr(PlcMemType.S, 10000, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_CST_MAP_COMMAND                         = new PlcAddr(PlcMemType.S, 10001, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_LOT_START_COMMAND                       = new PlcAddr(PlcMemType.S, 10002, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_WAFER_START_COMMAND                     = new PlcAddr(PlcMemType.S, 10003, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_WAFER_MAP_COMMAND                       = new PlcAddr(PlcMemType.S, 10004, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_RECIPE_CMD_COMMAND                      = new PlcAddr(PlcMemType.S, 10005, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_ECID_CHANGE_COMMAND                     = new PlcAddr(PlcMemType.S, 10006, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_PORT_RECIPE_COMMAND                     = new PlcAddr(PlcMemType.S, 10007, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_RCMD_COMMAND                            = new PlcAddr(PlcMemType.S, 10008, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_CTRL_STATE_CHANGE_COMMAND               = new PlcAddr(PlcMemType.S, 10009, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_MOVE_OUT_FLAG_COMMAND                   = new PlcAddr(PlcMemType.S, 10010, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_TERMINA_MSG_COMMAND                     = new PlcAddr(PlcMemType.S, 10011, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_OHT_MODE_CHANGE_COMMAND                 = new PlcAddr(PlcMemType.S, 10012, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_DEEP_LEARNING_REVIEW_COMPLTE_COMMAND    = new PlcAddr(PlcMemType.S, 10014, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_AUTO_MOVE_OUT_COMMAND                   = new PlcAddr(PlcMemType.S, 10015, 0, 1, PlcValueType.BIT);

        public static PlcAddr XB_CST_LOAD_REPLY             = new PlcAddr(PlcMemType.S, 10025, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_LOT_START_REPLY            = new PlcAddr(PlcMemType.S, 10026, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_WAFER_LOAD_REPLY           = new PlcAddr(PlcMemType.S, 10027, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_WAFER_MAP_REQUEST_REPLY    = new PlcAddr(PlcMemType.S, 10028, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_WAFER_UNLOAD_REPLY         = new PlcAddr(PlcMemType.S, 10029, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_CST_UNLOAD_REPLY           = new PlcAddr(PlcMemType.S, 10030, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_WAFER_INSPECTION_END_REPLY = new PlcAddr(PlcMemType.S, 10031, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_WAFER_MAP_UPLOAD_REPLY     = new PlcAddr(PlcMemType.S, 10032, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_ALARM_REPLY                = new PlcAddr(PlcMemType.S, 10033, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_EQ_STATE_CHANGE_REPLY      = new PlcAddr(PlcMemType.S, 10034, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_RECIPE_SELECT_REPLY        = new PlcAddr(PlcMemType.S, 10035, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_ECID_CHANGE_REPLY          = new PlcAddr(PlcMemType.S, 10036, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_RECIPE_CMD_REPLY           = new PlcAddr(PlcMemType.S, 10037, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_CTRL_MODE_CHANGE_REPLY     = new PlcAddr(PlcMemType.S, 10038, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_OS_VERSION_REPLY           = new PlcAddr(PlcMemType.S, 10039, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_OHT_MODE_CHANGE_REPLY      = new PlcAddr(PlcMemType.S, 10040, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_PORT_MODE_CHANGE_REPLY     = new PlcAddr(PlcMemType.S, 10041, 0, 1, PlcValueType.BIT);
        public static PlcAddr XB_EQP_MODE_CHANGE_REPLY      = new PlcAddr(PlcMemType.S, 10042, 0, 1, PlcValueType.BIT);
        #region 이전
        //public static PlcAddr XB_DateTimeCmd                                     = new PlcAddr(PlcMemType.S, 0, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_TerminalMessageCmd                              = new PlcAddr(PlcMemType.S, 1, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_EcidChangeCmd                                   = new PlcAddr(PlcMemType.S, 2, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_RecipeParameterChangeCmd                        = new PlcAddr(PlcMemType.S, 3, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_PPIDCMDCmd                                      = new PlcAddr(PlcMemType.S, 4, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_APCCmd                                          = new PlcAddr(PlcMemType.S, 5, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_NormalCmd                                       = new PlcAddr(PlcMemType.S, 6, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_PMCmd                                           = new PlcAddr(PlcMemType.S, 7, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_PauseCmd                                        = new PlcAddr(PlcMemType.S, 8, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_ResumeCmd                                       = new PlcAddr(PlcMemType.S, 9, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_CassetteMapCmd                                   = new PlcAddr(PlcMemType.S, 10, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_ProcessSlotCmd                                  = new PlcAddr(PlcMemType.S, 11, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_ControlJobCmd                                   = new PlcAddr(PlcMemType.S, 12, 0, 1, PlcValueType.BIT);


        //public static PlcAddr XB_DateTimeSyncReply                               = new PlcAddr(PlcMemType.S, 50, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_AlarmReply                                      = new PlcAddr(PlcMemType.S, 51, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_RecipeCMDReply                                  = new PlcAddr(PlcMemType.S, 52, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_WaferInReply                                    = new PlcAddr(PlcMemType.S, 53, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_WaferOutReply                                   = new PlcAddr(PlcMemType.S, 54, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_WaferDcollReply                                 = new PlcAddr(PlcMemType.S, 55, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_EcidChangeReply                                 = new PlcAddr(PlcMemType.S, 56, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_StateChangeReply                                = new PlcAddr(PlcMemType.S, 57, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_WaferScrapReply                                 = new PlcAddr(PlcMemType.S, 58, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_PortModeReply                                   = new PlcAddr(PlcMemType.S, 59, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_ProcessStepReply                                = new PlcAddr(PlcMemType.S, 60, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_CassetteLoadReply                                = new PlcAddr(PlcMemType.S, 61, 0, 1, PlcValueType.BIT);
        //public static PlcAddr XB_CassetteSlotMappingReply                         = new PlcAddr(PlcMemType.S, 62, 0, 1, PlcValueType.BIT);


        //public static PlcAddr XB_AliveBit                                        = new PlcAddr(PlcMemType.S, 100, 0, 1, PlcValueType.BIT); // 0 : 0ff / 1 : 0n(항상 0.5sec On / 0.5 sec 0ff )
        //public static PlcAddr XB_TerminalStateBit                                = new PlcAddr(PlcMemType.S, 102, 0, 1, PlcValueType.BIT); // 0 : msg 없음, 1 : 있음
        //public static PlcAddr XB_RecipeCMDAckReply                               = new PlcAddr(PlcMemType.S, 200, 0, 1, PlcValueType.BIT);
        #endregion
        #endregion

        static CIMAB()
        {
        }

        public static void Initailize(IVirtualMem plc)
        {
            YB_CST_MAP_REPLY.PLC =
            YB_LOT_START_REPLY.PLC =
            YB_WAFER_START_REPLY.PLC =
            YB_WAFER_MAP_REPLY.PLC =
            YB_ECID_CHANGE_REPLY.PLC =
            YB_PORT_RECIPE_REPLY.PLC =
            YB_RCMD_REPLY.PLC =
            YB_CTRL_STATE_CHANGE_REPLY.PLC = 
            YB_OHT_MODE_CHANGE_REPLY.PLC =
            YB_DEEP_LEARNING_REVIEW_COMPLTE_REPLY.PLC =
            YB_AUTO_MOVE_OUT_REPLY.PLC =

            YB_CST_LOAD_REPORT.PLC =
            YB_LOT_START_REPORT.PLC =
            YB_WAFER_LOAD_REPORT.PLC =
            YB_WAFER_MAP_REQUEST_REPORT.PLC =
            YB_WAFER_UNLOAD_REPORT.PLC =
            YB_CST_UNLOAD_REPORT.PLC =
            YB_ALARM_REPORT.PLC =
            YB_EQ_STATUS_CHANGE_REPORT.PLC =
            YB_RECIPE_SELECT_REPORT.PLC =
            YB_ECID_CHANGE_REPORT.PLC =
            YB_CTRL_MODE_CHANGE_REPORT.PLC =
            YB_OS_VERSION_REPORT.PLC =
            YB_OHT_MODE_CHANGE_REPORT.PLC =
            YB_PORT_MODE_CHANGE_REPORT.PLC =
            YB_EQP_MODE_CHANGE_REPORT.PLC =
            YB_ALARM_EXIST_REPORT.PLC =
            YB_MOVE_OUT_FLAG_REPLY.PLC = 

            XB_DATE_TIME_COMMAND.PLC =
            XB_CST_MAP_COMMAND.PLC =
            XB_LOT_START_COMMAND.PLC =
            XB_WAFER_START_COMMAND.PLC =
            XB_WAFER_MAP_COMMAND.PLC =
            XB_RECIPE_CMD_COMMAND.PLC =
            XB_ECID_CHANGE_COMMAND.PLC =
            XB_PORT_RECIPE_COMMAND.PLC =
            XB_RCMD_COMMAND.PLC =
            XB_CTRL_STATE_CHANGE_COMMAND.PLC = 
            XB_OHT_MODE_CHANGE_COMMAND.PLC =
            XB_DEEP_LEARNING_REVIEW_COMPLTE_COMMAND.PLC =
            XB_AUTO_MOVE_OUT_COMMAND.PLC =

            XB_CST_LOAD_REPLY.PLC =
            XB_LOT_START_REPLY.PLC =
            XB_WAFER_LOAD_REPLY.PLC =
            XB_WAFER_MAP_REQUEST_REPLY.PLC =
            XB_WAFER_UNLOAD_REPLY.PLC =
            XB_CST_UNLOAD_REPLY.PLC =
            XB_WAFER_INSPECTION_END_REPLY.PLC =
            XB_WAFER_MAP_UPLOAD_REPLY.PLC =
            XB_ALARM_REPLY.PLC =
            XB_EQ_STATE_CHANGE_REPLY.PLC =
            XB_RECIPE_SELECT_REPLY.PLC =
            XB_ECID_CHANGE_REPLY.PLC =
            XB_RECIPE_CMD_REPLY.PLC =
            XB_CTRL_MODE_CHANGE_REPLY.PLC =
            XB_OS_VERSION_REPLY.PLC =
            XB_OHT_MODE_CHANGE_REPLY.PLC =
            XB_PORT_MODE_CHANGE_REPLY.PLC =
            XB_EQP_MODE_CHANGE_REPLY.PLC =
            XB_MOVE_OUT_FLAG_COMMAND.PLC = plc;

            YB_TERMINA_MSG_REPLY.PLC =
                XB_TERMINA_MSG_COMMAND.PLC =
                plc;
        }
    }
}
