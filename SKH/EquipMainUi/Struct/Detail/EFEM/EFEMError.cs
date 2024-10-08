using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.Detail.EFEM
{
    public class EFEMErrorDetail
    {
        public string Desc;
        public EM_AL_LST AlarmCode;

        public EFEMErrorDetail(string desc, EM_AL_LST alarmCode = EM_AL_LST.AL_0000_NONE)
        {
            Desc = desc;
            AlarmCode = alarmCode;
        }
    }
    public static class EFEMError
    {
        public static string GetErrorDesc(EmEfemErrorModule e, int code)
        {
            EFEMErrorDetail detail;
            string desc = "CODE ERROR";

            if (code <= 0)
                return "NO ERROR CODE";

            switch (e)
            {
                case EmEfemErrorModule.E0:
                    if (E0SystemErrorInfo.TryGetValue(code, out detail) == true)
                        desc = string.Format("SystemError(E0)_{0}", detail.Desc);
                    else
                        desc = "SystemError(E0)_Undefined Error Code";
                    break;
                case EmEfemErrorModule.E1:
                    if (E1RobotErrorInfo.TryGetValue(code, out detail) == true)
                        desc = string.Format("RobotErrorInfo(E1)_{0}", detail.Desc);                    
                    else
                        desc = "RobotErrorInfo(E1)_Undefined Error Code";
                    break;
                case EmEfemErrorModule.E2:
                    if (E2LpmPortErrorInfo.TryGetValue(code, out detail) == true)
                        desc = string.Format("LpmPortErrorInfo(E2)_{0}", detail.Desc);                    
                    else
                        desc = "LpmPortErrorInfo(E2)_Undefined Error Code";
                    break;
                case EmEfemErrorModule.E3:
                    if (E3AlignerErrorInfo.TryGetValue(code, out detail) == true)
                        desc = string.Format("AlignerErrorInfo(E3)_{0}", detail.Desc);
                    else
                        desc = "Undefined Error Code";
                    break;
                default:
                    desc = "Undefined Error Code";
                    break;
            }
            return desc;
        }

        public static Dictionary<int, EFEMErrorDetail> E0SystemErrorInfo = new Dictionary<int, EFEMErrorDetail>()
        {
            {1, new EFEMErrorDetail("SERIAL_PORT_IS_FAILED_TO_OPEN", EM_AL_LST.AL_0780_EFEM_SERIAL_PORT_FAIL_TO_OPEN)},
            {2, new EFEMErrorDetail("THE_DEVICE_NET_IS_FAILED_TO_OPEN", EM_AL_LST.AL_0781_EFEM_DEVICE_NET_FAIL_TO_OPEN)},
            {3, new EFEMErrorDetail("ROBOT_NOT_INITIALIZE", EM_AL_LST.AL_0782_EFEM_ROBOT_NEED_INITIALIZE)},
            {4, new EFEMErrorDetail("THE_PORT_IS_INCORRECT", EM_AL_LST.AL_0783_EFEM_PORT_INCORRECT)},

            {9, new EFEMErrorDetail("THE_COMMAND_IS_INCORRECT", EM_AL_LST.AL_0784_EFEM_COMMAND_INCORRECT)},

            {10, new EFEMErrorDetail("INVALID_DEVICENET_PARAMETER")},
            {11, new EFEMErrorDetail("WAFER_NOT_EXIST(ROBOT_LOWER_ARM)")},
            {12, new EFEMErrorDetail("WAFER_NOT_EXIST(ROBOT_UPPER_ARM)")},
            {13, new EFEMErrorDetail("WAFER_NOT_EXIST(ROBOT_LOWER_OR_UPPER_ARM)")},
            {14, new EFEMErrorDetail("WAFER_EXIST(ROBOT_LOWER_ARM)")},
            {15, new EFEMErrorDetail("WAFER_EXIST(ROBOT_UPPER_ARM)")},
            {16, new EFEMErrorDetail("WAFER_EXIST(ROBOT_LOWER_OR_UPPER_ARM)")},
            {17, new EFEMErrorDetail("EFEM_SAFETY_PLC_ALARM_STATUS")},
            {18, new EFEMErrorDetail("EFEM EMO Button Push Status")},

            {21, new EFEMErrorDetail("THE_LOAD_PORT_1_DOOR_IS_CLOSED_WHEN_THE_ROBOT_PICK_OR_PLACE_ACTION.")},
            {22, new EFEMErrorDetail("THE_LOAD_PORT_2_DOOR_IS_CLOSED_WHEN_THE_ROBOT_PICK_OR_PLACE_ACTION.")},
            {24, new EFEMErrorDetail("THE_RIGHT_SERVICE_DOOR_IS_OPEN")},
            {26, new EFEMErrorDetail("THE_LOAD_PORT_DOOR_IS_OPENED")},
            {27, new EFEMErrorDetail("THE_LOAD_PORT_WSO_SENSOR_ON_IS_DETECTED.")},
            { 29, new EFEMErrorDetail("DeviceNet Busy" )},

            {30, new EFEMErrorDetail("MAIN_AIR_FAULT")},
            {31, new EFEMErrorDetail("MAIN_VACUUM_FAULT")},
            {32, new EFEMErrorDetail("LOAD_PORT_BUSY")},
            {33, new EFEMErrorDetail("LOAD_PORT_FAULT")},
            {34, new EFEMErrorDetail("LOAD_LOCK_DOOR_CLOSED")},
            {35, new EFEMErrorDetail("ALIGNER_BUSY")},
            {36, new EFEMErrorDetail("ALIGNER_FAULT")},
            {37, new EFEMErrorDetail("IONIZER_AIR_FAULT")},
            {39, new EFEMErrorDetail("ROBOT_BUSY")},

            {40, new EFEMErrorDetail("ALIGNER_SEND_DATA_FAIL")},
            {41, new EFEMErrorDetail("ALIGNER_READ_DATA_UNKNOWN")},
            {42, new EFEMErrorDetail("LOAD_PORT_SEND_DATA_FAIL")},
            {43, new EFEMErrorDetail("LOAD_PORT_READ_DATA_UNKNOWN")},
            {44, new EFEMErrorDetail("CTC_SEND_DATA_FAIL")},
            {45, new EFEMErrorDetail("CTC_READ_DATA_UNKNOWN")},
            {46, new EFEMErrorDetail("ROBOT_SEND_DATA_FIAL")},
            {47, new EFEMErrorDetail("ROBOT_READ_DATA_UNKNOWN")},
            {48, new EFEMErrorDetail("ROBOT_MODE_MANUAL")},
            {49, new EFEMErrorDetail("ROBOT_POWER_OFF")},

            {50, new EFEMErrorDetail("ROBOT_EXTENDED")},
            {51, new EFEMErrorDetail("MAP_NOT_EXIST_WAFER")},
            {52, new EFEMErrorDetail("MAP_EXIST_WAFER")},
            {53, new EFEMErrorDetail("ROBOT_HAVE_ALARM")},
            {54, new EFEMErrorDetail("TIME_OUT_OF_LPM_COMMAND")},
            {55, new EFEMErrorDetail("TIME_OUT_OF_ALIGNER_COMMAND")},
            {56, new EFEMErrorDetail("TIME_OUT_OF_ROBOT_COMMAND")},
            {57, new EFEMErrorDetail("EFEM_MODE_IS_OFFLINE")},
            {58, new EFEMErrorDetail("THE_SOURCE_OR_TARGET_SLOT_NUMBER_OF_ROBOT_COMMAND_IS_INVALID")},
            {59, new EFEMErrorDetail("THE_SOURCE_OR_TARGET_PORT_NUMBER_OF_ROBOT_COMMAND_IS_INVALID")},

            {60, new EFEMErrorDetail("THE_WAFER_IS_EXIST_ALREADY_ON_THE_ALIGNER")},
            {61, new EFEMErrorDetail("THE_WAFER_IS_NOT_EXIST_ON_THE_ALIGER")},
            {62, new EFEMErrorDetail("THE_ALIGNER_IS_NOT_INITIALIZED")},
            {63, new EFEMErrorDetail("THE_ALIGNER_SLOT_NUMBER_IS_WRONG.")},
            {65, new EFEMErrorDetail("THE_WAFER_IS_DOUBLE_STATUS")},
            {66, new EFEMErrorDetail("THE_WAFER_IS_CROSS_STATUS")},
            {67, new EFEMErrorDetail("THE_WAFER_IS_DOUBLE_OR_CROSS_STATUS")},
            {68, new EFEMErrorDetail("THE_ROBOT_CANNOT_USE_TRANS_COMMAND_IN_USE_OF_DUAL_ARM_ACTION_TO_ALIGNER")},

            {70, new EFEMErrorDetail("CANNOT_GET_THE_ROBOT_STATUS_INFORMATION.")},
            {71, new EFEMErrorDetail("CANNOT_GET_THE_PRE-ALIGNER_STATUS_INFORMATION")},
            {72, new EFEMErrorDetail("CANNOT_GET_THE_LPM_STATUS_INFORMATION")},
            {73, new EFEMErrorDetail("CANNOT_GET_THE_EFEM_STATUS_INFORMATION")},
            {74, new EFEMErrorDetail("ROBOT_IS_PAUSED_STATUS,_YOU_CAN_ONLY_USE_RESUME,STATUS,INIT_COMMAND")},
            {75, new EFEMErrorDetail("ROBOT_IS_STOPPED_STATUS,_YOU_CAN_ONLY_USE_INIT,HOME_COMMAND")},
            {77, new EFEMErrorDetail("ROBOT_IS_NOT_PAUSED_STATUS.")},

            { 80, new EFEMErrorDetail("Input Parameter Syntax Error")},
            { 84, new EFEMErrorDetail("Unknown Wafer Status Exist Error(Need to Mapping or RSCAN)")},
            { 87, new EFEMErrorDetail("LPM error status. Need clear the error(RESET)")},
            { 88, new EFEMErrorDetail("Aligner error status. Need clear the error(RESET)")},

            { 93, new EFEMErrorDetail("Robot is not trans(pick or place) action status.")},
            {94, new EFEMErrorDetail("ROBOT_IS_ALREADY_RUN_THE_TRANS_COMMAND.")},
            { 96, new EFEMErrorDetail("DeviceNet Send Data Fail")},
            { 98, new EFEMErrorDetail("Received Data Not Accuracy")},

            {100, new EFEMErrorDetail("Object is null(Not Created) Error")},
            {104, new EFEMErrorDetail("ROBOT_IS_NOT_DUAL_PICK/PLACE_MODE")},
            {105, new EFEMErrorDetail("THIS_ROBOT_COMMAND_CAN_NOT_USING_DUAL_PICK/PLACE_ACTION")},
            {106, new EFEMErrorDetail("ROBOT_UPPER/LOWER_HAND_WAFER_EXIST")},
            {107, new EFEMErrorDetail("ROBOT_UPPERE/LOWER_HAND_WAFER_NOT_EXIST")},
            {114, new EFEMErrorDetail("Robot Hand Not Front Side")},
            {115, new EFEMErrorDetail("ROBOT_PICK_ACTION_DESIGNATED_PORT_NOT_HAVE_WAFER_SIZE_INFO")},
            {116, new EFEMErrorDetail("MAPPING_MODULE_NOT_INITIALIZE.")},
            {117, new EFEMErrorDetail(" Mapping Module Is Not Auto Mode")},

            { 120, new EFEMErrorDetail("The Wafer is detected in the Lower Hand of Robot.So Cannot Run the TRANS(Pick) Command")},
            { 121, new EFEMErrorDetail(" The Wafer is detected in the Upper Hand of Robot.So Cannot Run the TRANS(Pick) Command")},
            { 122, new EFEMErrorDetail(" The Wafer is Not detected in the Lower Hand of Robot.So Cannot Run the TRANS(Place) Command")},
            { 123, new EFEMErrorDetail("The Wafer is Not detected in the Upper Hand of Robot.So Cannot Run the  TRANS(Place) Command")},
            { 124, new EFEMErrorDetail("The Wafer is Not detected in the Lower Hand of Robot,After Robot Trans(Pick)    Command")},
            { 125, new EFEMErrorDetail("The Wafer is Not detected in the Upper Hand of Robot,After Robot Trans(Pick)    Command")},
            { 126, new EFEMErrorDetail("The Wafer is Not detected in the Lower Hand of Robot.After Robot TRANS(Place)   Command")},
            { 127, new EFEMErrorDetail("The Wafer is Not detected in the Upper Hand of Robot.After Robot TRANS(Place)   Command")},
            { 138, new EFEMErrorDetail("Aligner Module Is Not Initialize")},

            {140, new EFEMErrorDetail(" Protocol Header Not Match Error")},
            {141, new EFEMErrorDetail("Protocol Check Sum Error")},
            {142, new EFEMErrorDetail(" AbNormal Parameter Length Error")},
            {143, new EFEMErrorDetail(" Serial/Ethernet Port Already Open/Connect")},
            {145, new EFEMErrorDetail(" Protocol_UnKnownPort_Error")},
            {148, new EFEMErrorDetail(" AbNormal Packet Length Error")},
            {149, new EFEMErrorDetail(" Invalid CTC Command Syntax Error.")},

            {151, new EFEMErrorDetail("EFEM_MODE_SWITCH_MANUAL_MODE_STATUS_ERROR.")},
            {153, new EFEMErrorDetail("EFEM_EMO_RIGHT_BUTTON_PUSHED_STATUS")},
            {154, new EFEMErrorDetail("EFEM_EMO_LEFT_BUTTON_PUSHED_STATUS")},
            {155, new EFEMErrorDetail("ROBOT_VACUUM_ALARM")},
            {157, new EFEMErrorDetail("ROBOT_UPPER_HAND_WAFER_EXIST")},
            {158, new EFEMErrorDetail("ROBOT_UPPER_HAND_WAFER_NOT_EXIST")},
            {159, new EFEMErrorDetail("ROBOT_LOWER_HAND_WAFER_EXIST")},
            {160, new EFEMErrorDetail("ROBOT_LOWER_HAND_WAFER_NOT_EXIST")},

            {190, new EFEMErrorDetail("Main Mapping Module CDA Alarm")},

            {202, new EFEMErrorDetail("Module System(EFEM) Busy")},

            {221, new EFEMErrorDetail("Light Curtain Bar Open Alarm")},

            {303, new EFEMErrorDetail("LPM NAK Error")},
            {305, new EFEMErrorDetail("This Robot Command Can Not Using Dual Arm Type Parameters")},

            {400, new EFEMErrorDetail("Module Object is Null")},

            {521, new EFEMErrorDetail("Robot Disable R equest Retransmission Command Function")},

            {550, new EFEMErrorDetail("Aligner Not Ready Status")},

            {600, new EFEMErrorDetail("RingFrameLpm Slot01 Dual Pick Error")},
            {601, new EFEMErrorDetail("RingFrameLpm Slot01 Dual Place Error")},
            {602, new EFEMErrorDetail("RingFrameLpm Not Pin Up Status Error")},
            {603, new EFEMErrorDetail("RingFrameLpm Not Pin Down Status Error")},
            {607, new EFEMErrorDetail("Robot Place Disable De s i gnated Port In Upper Hand.")},
        };
        public static Dictionary<int, EFEMErrorDetail> E1RobotErrorInfo = new Dictionary<int, EFEMErrorDetail>()
        {
            {0, new EFEMErrorDetail("NO_ALARM")},

            {201, new EFEMErrorDetail("DURING_THE_ROBOT-DRIVEN,_ANOTHER_EXECUTION_COMMAND_IS_SENT")},
            {202, new EFEMErrorDetail("THE_CONTROLLER_ALARM_IS_CONFIRMED_BEFORE_ROBOT_BEHAVIOR")},
            {203, new EFEMErrorDetail("SERVO_POWER_IS_NOT_ON.")},
            {205, new EFEMErrorDetail("CONTROLLER_NOT_INITIALIZED,_START_PROGRAM_IS_NOT_RUNNING,_OPERATE_IN_A_NON-INITIALIZEDSTATE_AND_THE_VACUUM_ON / OFF_COMMAND,_YOU_HAVE_MADE")},
            {206, new EFEMErrorDetail("WHEN_THE_STATE_DID_NOT_AUTO,WHEN_THE_COMMAND_HOLD_IS_EXECUTED_IN_AUTO_NON - STATE")},
            {207, new EFEMErrorDetail("CAUSE_THE_ACTION_COMMAND_IS_SENT_WHEN_THE_ROBOT_IS_IN_A_PAUSED_STATE")},
            {208, new EFEMErrorDetail("EMERGENCY_SWITCH_WAS_APPLIED,_THE_ACTION_COMMAND_IS_SENT")},
            {209, new EFEMErrorDetail("SERVO_POWER_IS_NOT_OFF.")},
            {211, new EFEMErrorDetail("ERROR_RESET_IS_NOT.")},
            {217, new EFEMErrorDetail("SEND_DATA_IS_NOT_COMPLETE")},
            {218, new EFEMErrorDetail("PENDANT_MODE_SELECT_SWITCH_IS_NOT_AUTO_MODE.")},
            {219, new EFEMErrorDetail("VACUUM_SENSOR_SIGNAL_IS_DETECTED_IN_THE_UPPER_HAND(ON_THE_ROBOT_MAPPING_ACTION)")},

            {300, new EFEMErrorDetail("UNIT/PORT_IDENTIFICATION_FROM_PLC(HOST)_BECOME_BY_SENDING_AN_INCORRECT_VALUEUNIT / PORT_RANGE_SETTING_VALUES_ARE_SET_INCORRECTLY.")},
            {301, new EFEMErrorDetail("THE_COMMAND_INFORMATION_RECEIVING_FROM_PLC_(HOST)_IS_SENT_BY_INCORRECT_VALUES.COMMAND_RANGE_SETTINGS_ARE_SET_INCORRECTLY.")},
            {302, new EFEMErrorDetail("THE_STAGE_INFORMATION_FROM_PLC(HOST)_BE_SENT_INCORRECTLY.STAGE_RANGE_SETTINGS_BE_SET_INCORRECTLY.")},
            {303, new EFEMErrorDetail("SLOT_INFORMATION_FROM_PLC(HOST)_IS_SENT_INCORRECTLY.RANGE_OF_SETTINGS_BE_SET_INCORRECTLY.")},
            {304, new EFEMErrorDetail("THE_HAND_INFORMATION_FROM_PLC(HOST)_IS_SENT_INCORRECTLY")},
            {305, new EFEMErrorDetail("THE_GLASS_MODEL_INFORMATION_FROM_PLC(HOST)_IS_SENT_INCORRECTLY.THE_MODEL_RANGE_OF_SETTINGS_BE_SET_INCORRECTLY.")},
            {306, new EFEMErrorDetail("THE_WAFER/GLASS_NUMBER_INFORMATION_FROM_PLC(HOST)_IS_SENT_BY_INCORRECT_VALUES WHEN_THE_EXTRACTION/STORAGE.WAFER / GLASS_COUNT_RANGE_SETTING_BE_SET_INCORRECTLY.")},
            {307, new EFEMErrorDetail("THE_REVERSAL_INFORMATION_FROM_PLC(HOST)_IS_TRANSMITTED_BY_INCORRECT_VALUES.")},
            {308, new EFEMErrorDetail("THE_BCC_INFORMATION_FROM_PLC(HOST)_IS_SENT_INCORRECTLY._BCC_(BLOCK_CHECK_CHARACTER) STANDS_FOR_THE_BLOCK_CHECK_CHARACTER._IN_DATA_COMMUNICATION_TO_TRANSMIT_DATA_FROMONE_BLOCK_TO_THE_END_OF_THE_BLOCK,_ADDING_CHARACTER_TO_THE_ERROR_CHECKING")},
            {309, new EFEMErrorDetail("WHEN_THE_STX/ETX_INFORMATION_FROM_PLC(HOST)_IS_SENT_INCORRECTLY,_TO_CONTROL_THETRANSFER_OF_INFORMATION,_OR_TO_FACILITATE_THE_FUNCTION_OF_THE_CHARACTER_COLLECTIVELY")},
            {310, new EFEMErrorDetail("WHEN_THE_GLASS_PORT_INFORMATION_FROM_PLC(HOST)_IS_SENT_INCORRECTLY.PORT_RANGE_SETTINGS_BE_SET_INCORRECTLY.")},
            {315, new EFEMErrorDetail("ALIGNER NOT READY") },
            {401, new EFEMErrorDetail("BEFORE_UNLOADING_COMMAND,_WAFER_GLASS_IS_DETECTED_IN_THE_LOWER_HAND")},
            {402, new EFEMErrorDetail("BEFORE_UNLOADING_COMMAND,_WAFER_GLASS_IS_DETECTED_IN_THE_UPPER_HAND")},
            {403, new EFEMErrorDetail("[VACUUM]_BEFORE_LOADING_COMMAND,_WAFER_GLASS_IS_NOT_DETECTED_IN_THE_LOWER_HAND")},
            {404, new EFEMErrorDetail("[VACUUM]_BEFORE_LOADING_COMMAND,_WAFER_GLASS_IS_NOT_DETECTED_IN_THE_UPPER_HAND")},
            {405, new EFEMErrorDetail("[VACUUM]_UNLOADING_COMMAND_(EXTRACTION_BEHAVIOR),_EXECUTE_THE_VACUUM_ACTION_INLOWER_HAND,_BUT_ADSORB_THE_SENSOR_DID_NOT_ON_IN_THE_LIMITED_TIME", EM_AL_LST.AL_0802_WAFER_SKIPPED_BECAUSE_ROBOT_CANNOT_VACUUM_ON)},
            {406, new EFEMErrorDetail("[VACUUM]_UNLOADING_COMMAND_(EXTRACTION_BEHAVIOR),_EXECUTE_THE_VACUUM_ACTION_INUPPER_HAND,_BUT_ADSORB_THE_SENSOR_DID_NOT_ON_IN_THE_LIMITED_TIME")},
            {407, new EFEMErrorDetail("[VACUUM]_AFTER_THE_ROBOT_COMPLETE_UNLOADING_COMMAND_(EXTRACTION_OPERATION)_IT_ISNOT_DETECTED_WAFER / GLASS_IN_THE_LOWER_HAND", EM_AL_LST.AL_0801_ROBOT_PLACE_WAFER_MISSED)},
            {408, new EFEMErrorDetail("[VACUUM]_AFTER_THE_ROBOT_COMPLETE_UNLOADING_COMMAND_(EXTRACTION_OPERATION)_IT_ISNOT_DETECTED_WAFER / GLASS_IN_THE_UPPER_HAND", EM_AL_LST.AL_0801_ROBOT_PLACE_WAFER_MISSED)},
            {409, new EFEMErrorDetail("[VACUUM]_AFTER_THE_ROBOT_COMPLETE_LOADING_COMMAND_IT_IS_DETECTED_WAFER/GLASS_INTHE_LOWER_HAND")},
            {410, new EFEMErrorDetail("[VACUUM]_AFTER_THE_ROBOT_COMPLETE_LOADING_COMMAND_IT_IS_DETECTED_WAFER/GLASS_INTHE_UPPER_HAND")},
            {411, new EFEMErrorDetail("[VACUUM]THE_WAFER/GLASS_SENSOR_IN_LOWER_HAND_IS_NOT_ON", EM_AL_LST.AL_0800_ROBOT_PICK_WAFER_MISSED)},
            {412, new EFEMErrorDetail("[VACUUM]THE_WAFER/GLASS_SENSOR_IN_UPPER_HAND_IS_NOT_ON", EM_AL_LST.AL_0800_ROBOT_PICK_WAFER_MISSED)},
            {413, new EFEMErrorDetail("[VACUUM]_VACUUM_SENSOR_SIGNAL_IS_NOT_DETECTED_IN_THE_LOWER_HAND")},
            {414, new EFEMErrorDetail("[VACUUM]_VACUUM_SENSOR_SIGNAL_IS_NOT_DETECTED_IN_THE_UPPER_HAND")},
            {415, new EFEMErrorDetail("[VACUUM]_AIR_PRESSURE_SUPPLIED_TO_THE_ROBOT_THAT_FALLS_BELOW_THE_THRESHOLD_VALUE")},
            {416, new EFEMErrorDetail("[VACUUM]_AIR_PRESSURE_SUPPLIED_TO_THE_ROBOT_THAT_FALLS_BELOW_THE_THRESHOLD_VALUE.(GRIP_TYPE_ROBOTS_ARE_APPLIED_IN)")},
            {417, new EFEMErrorDetail("[VACUUM]_THE_GLASS_IS_DETECTED_IN_THE_HAND_OF_SIMULATION_MODE_GLASS_DETECTIONROBOT,_BUT_EXECUTED_THE_COMMAND_WITH_SIMULATION_MODE")},
            {418, new EFEMErrorDetail("[VACUUM]_ THE_ OVERRUN_ SENSOR_ IS_ DETECTED_ WHEN_ THE_ LOWER_ HAND_ EXECUTE_ THE EXTRACTION_OPERATION")},
            {419, new EFEMErrorDetail("[VACUUM]_THE_OVERRUN_SENSOR_IS_DETECTED_WHEN_THE_UPPER_HAND_EXECUTE_THEEXTRACTION_OPERATION")},
            {420, new EFEMErrorDetail("THE_PORT_NUMBER_THAT_CAN_NOT_EXECUTE_THE_EXCHANGE_ACTION_IS_SENT.PORT_(CASSETTE_MODULE)_CAN_NOT_PROCEED_WITH_THE_EXCHANGE_OPERATION.")},
            {421, new EFEMErrorDetail("UNLOAD_RETRACT_COMMAND_AND_THE_STATUS_OF_ROBOT_IS_IN_AN_INCONSISTENT.LOAD_RETRACT_COMMAND_AND_THE_STATUS_OF_ROBOT__IS_IN_AN_INCONSISTENT")},
            {422, new EFEMErrorDetail("SPEED_SETTING_VALUE_EXCEEDED_RANGE")},
            {423, new EFEMErrorDetail("ARM_FOLD_SETTING_VALUE_EXCEEDED_RANGE")},
            {424, new EFEMErrorDetail("SPEED_MODE_EXCEEDED_THE_RANGE_OF_SELECTION")},
            {425, new EFEMErrorDetail("FAILED_TO_STORE_THE_RECEIVED_DATA")},
            {426, new EFEMErrorDetail("DOWN_STROKE_[EXTRACT_ENTRY_POINT]_EXCEEDED_THE_SCOPE_OF_THE_SETTING_POINT")},
            {427, new EFEMErrorDetail("UP_STROKE_SETTING_VALUE_IS_OUT_OF_RANGEIT_IS_GREATER_THAN_CASSETTE_PITCH_SETTING_VALUE")},
            {428, new EFEMErrorDetail("THE_SETTING_VALUE_IS_MORE_THAN_MAX_VALUE_OF_CASSETTE")},
            {429, new EFEMErrorDetail("AXIS_COMMAND_VALUE_HAS_BEEN_RECEIVED,_SENDING_THE_WRONG")},
            {430, new EFEMErrorDetail("AXIS_COMMAND_VALUE_HAS_BEEN_RECEIVED_IS_DIFFERENT_WITH_THE_SETTING_VALUE_OF_ROBOTTYPE")},
            {431, new EFEMErrorDetail("AXIS_COMMAND_VALUE_HAS_BEEN_RECEIVED_IS_DIFFERENT_WITH_THE_SETTING_VALUE_OF_ROBOTTYPE")},
            {432, new EFEMErrorDetail("AXIS_COMMAND_VALUE_HAS_BEEN_RECEIVED_IS_DIFFERENT_WITH_THE_SETTING_VALUE_OF_ROBOTTYPE")},
            {435, new EFEMErrorDetail("THE_DISTANCE_VALUE_RECEIVED_EXCEEDS_THE_RANGE_LIMIT.")},
            {436, new EFEMErrorDetail("MUTI_VALUE_IS_RECEIVED_THAT_IS_NOT_USED.")},
            {447, new EFEMErrorDetail("ALL_DOOR_STAGE_CLOSE_STATUS_BEFORE_ROBOT_MOVING")},

            {500, new EFEMErrorDetail("[VACUUM]_RADIAL_ALIGNMENT_EXECUTE_COMMAND_IS_RECEIVED,_BUT_THE_SETTING_VALUE_OFRADIAL_ALIGNMENT_IS_NOT_USED")},
            {501, new EFEMErrorDetail("[VACUUM]_THE_RADIAL_ALIGNMENT_SENSOR_SIGNAL_OF_THE_LOWER_HAND_IS_NOT_DETECTED")},
            {502, new EFEMErrorDetail("[VACUUM]_THE_RADIAL_ALIGNMENT_SENSOR_SIGNAL_OF_THE_UPPER_HAND_IS_NOT_DETECTED")},
            {503, new EFEMErrorDetail("[VACUUM]_ LOWER_ HAND_ RADIAL_ ALIGNMENT_ SENSOR_ SIGNAL_ HAS_ BEEN_ DETECTED_ SEVERALTIMES")},
            {504, new EFEMErrorDetail("[VACUUM]_ UPPER_ HAND_ RADIAL_ ALIGNMENT_ SENSOR_ SIGNAL_ HAS_ BEEN_ DETECTED_ SEVERALTIMES")},
            {505, new EFEMErrorDetail("[VACUUM]_AFTER_THE_RADIAL_ALIGNMENT_OF_LOWER_HAND_IS_EXECUTED,_R-AXIS_LIMIT_SETTINGVALUE_IS_LARGER_OR_SMALLER_THAN_THE_VALUE_CALCULATED")},
            {506, new EFEMErrorDetail("[VACUUM]_AFTER_THE_RADIAL_ALIGNMENT_OF_UPPER_HAND_IS_EXECUTED,_R-AXIS_LIMIT_SETTINGVALUE_IS_LARGER_OR_SMALLER_THAN_THE_VALUE_CALCULATED")},
            {507, new EFEMErrorDetail("[VACUUM]_AFTER_THE_RADIAL_ALIGNMENT_OF_LOWER_HAND_IS_EXECUTED,_THE_VALUE_IS_DETECTED IS_MORE_THAN_FORWARD_/_REVERSE_OPERATION_LIMIT_VALUE")},
            {508, new EFEMErrorDetail("[VACUUM]_AFTER_THE_RADIAL_ALIGNMENT_OF_UPPER_HAND_IS_EXECUTED,_THE_VALUE_IS_DETECTEDIS_MORE_THAN_FORWARD / REVERSE_OPERATION_LIMIT_VALUE")},
            {509, new EFEMErrorDetail("[VACUUM]_AFTER_THE_RADIAL_ALIGNMENT_OF_LOWER_HAND_IS_EXECUTED,_THE_VALUE_OF_TH(ROTATION)_AXIS_IS_LARGER_OR_SMALLER_THAN_LIMIT_VALUE")},
            {510, new EFEMErrorDetail("[VACUUM]_AFTER_THE_RADIAL_ALIGNMENT_OF_UPPER_HAND_IS_EXECUTED,_THE_VALUE_OF_TH(ROTATION)_AXIS_IS_LARGER_OR_SMALLER_THAN_LIMIT_VALUE")},
            {511, new EFEMErrorDetail("[VACUUM]_AFTER_THE_RADIAL_ALIGNMENT_OF_LOWER_HAND_IS_EXECUTED,_X(DRIVING)_AXIS_ISMORE_THAN_LIMIT_SETTING_VALUE.")},
            {512, new EFEMErrorDetail("[VACUUM]_AFTER_THE_RADIAL_ALIGNMENT_OF_UPPER_HAND_IS_EXECUTED,_X(DRIVING)_AXIS_ISMORE_THAN_LIMIT_SETTING_VALUE")},
            {513, new EFEMErrorDetail("[VACUUM]_ROBOT_POSITION_NOT_SET")},

            {600, new EFEMErrorDetail("[VACUUM]_BEFORE_TRAVERS_ALIGNMENT_IS_OPERATING,_THE_POSITION_OF_SENSOR_IS_NOT_FOUND")},
            {601, new EFEMErrorDetail("[VACUUM]_WHEN_TRAVERS_ALIGNMENT_IS_OPERATING,_THE_DIRECTION_VALUE_OF_X_AXIS_IS_NOTFOUND_WHEN_LOADING.")},
            {602, new EFEMErrorDetail("[VACUUM]_AFTER_TRAVERS_ALIGNMENT_OPERATING,_THE_DIRECTION_VALUE_OF_X_AXIS_IS_NOTFOUND_WHEN_LOADING.")},
            {603, new EFEMErrorDetail("[VACUUM]_THE_TRAVERS_SENSOR_SIGNAL_OF_LOWER_HAND_IS_NOT_DETECTED")},
            {604, new EFEMErrorDetail("[VACUUM]_THE_TRAVERS_SENSOR_SIGNAL_OF_UPPER_HAND_IS_NOT_DETECTED")},
            {605, new EFEMErrorDetail("[VACUUM]_LOWER_HAND_TRAVERS_ALIGNMENT_SENSOR_SIGNAL_HAS_BEEN_DETECTED_SEVERALTIMES")},
            {606, new EFEMErrorDetail("[VACUUM]_UPPER_HAND_TRAVERS_ALIGNMENT_SENSOR_SIGNAL_HAS_BEEN_DETECTED_SEVERALTIMES")},
            {607, new EFEMErrorDetail("[VACUUM]_TRAVERS_ALIGNMENT_LIMIT_THE_SCOPE_OF_OPERATION_HAS_EXCEEDED.")},

            {100, new EFEMErrorDetail("[VACUUM]_MAPPING_THE_BEHAVIOR_DID_NOT_INITIALIZE_THE_VALUE_OF_COUNTING_ON_PROGRESS.")},
            {101, new EFEMErrorDetail("[VACUUM]_MAPPING_SENSOR_ON/OFF_SIGNAL_IS_EQUAL_TO_THE_VALUE_OF_COUNTING.")},
            {102, new EFEMErrorDetail("[VACUUM]_AFTER_MAPPING_OPERATING,_THE_WAFER_IN_CASSETTE_IS_OVERLAPPED")},
            {103, new EFEMErrorDetail("[VACUUM]_AFTER_MAPPING_OPERATING,_THE_WAFER_IN_CASSETTE_IS_SPANNED_BETWEEN_THESLOT_AND_SLOT")},
            {104, new EFEMErrorDetail("[VACUUM]_MAPPING_SPEED_HAS_NOT_BEEN_SET.")},

            {999, new EFEMErrorDetail("UNEXPECTED_ERROR")},
        };
        public static Dictionary<int, EFEMErrorDetail> E2LpmPortErrorInfo = new Dictionary<int, EFEMErrorDetail>()
        {
            { 000, new EFEMErrorDetail("No Error : No a larm")},
            { 001, new EFEMErrorDetail("Home Initial Error 초기화 명령 수행하지 않음", EM_AL_LST.AL_0850_LPM_NEED_INITIALIZE)},
            { 004, new EFEMErrorDetail("POD(Foup) Not Detect POD(Foup) 가 검출 않됨")},
            { 005, new EFEMErrorDetail("Mode Error 전송 관련 명령어와 현재 모드가 다름")},
            { 006, new EFEMErrorDetail("Cover Is not Close 커버가 닫히지 않음")},
            { 007, new EFEMErrorDetail("Cover is not Opened 커버가 열리지 않음", EM_AL_LST.AL_0855_LPM_CANT_OPEN)},
            { 008, new EFEMErrorDetail("STOP Command Error Stop 명령 어 전송됨")},
            { 009, new EFEMErrorDetail("WSO Check Stop Mask(Foup Wafer 기타 등등) 돌출 감지 됨")},
            { 010, new EFEMErrorDetail("Mapping Count Error Mapping 실패함")},
            { 011, new EFEMErrorDetail("Wafer Extrusion Dete ction 웨이퍼 돌출이 감지됨")},
            { 014, new EFEMErrorDetail("Z Up Limit Error Z 축 Up Limit 오류")},
            { 015, new EFEMErrorDetail("Z Down Limit Error Z 축 Down Limit 오류")},
            { 016, new EFEMErrorDetail("Motor Stroke Error 모터 이상")},
            { 018, new EFEMErrorDetail("Wrong Command Error 잘못된 명령어 전송")},
            { 028, new EFEMErrorDetail("No Action Mapping Forward M apping 센서가 앞으로 움직여지지 않음")},
            { 029, new EFEMErrorDetail("No Action Mapping Home Mapping 센서가 뒤로 움직여지지 않음")},
            { 030, new EFEMErrorDetail("No Action Align Forward A lign 센서가 앞으로 움직여지지 않음")},
            { 031, new EFEMErrorDetail("No Action Align Home Align 센서가 뒤로 움직여지지 않음")},
            { 032, new EFEMErrorDetail("No Action Unit Forward – Unit 센서가 앞으로 움직여지지 않음")},
            { 033, new EFEMErrorDetail("No Action Unit Home - Unit 센서가 뒤로 움직여지지 않음")},
            { 034, new EFEMErrorDetail("No Action Pin Up – Unit 센서가 위로 움직여지지 않음")},
            { 035, new EFEMErrorDetail("No Action Pin Down – Pin 센서가 아래로 움직여지지 않음")},
        };
        public static Dictionary<int, EFEMErrorDetail> E3AlignerErrorInfo = new Dictionary<int, EFEMErrorDetail>()
        {
            {000, new EFEMErrorDetail("No Error : No alarm")},
            {001, new EFEMErrorDetail("Home Initial Error 초기화 명령 수행하지 않음", EM_AL_LST.AL_0900_PRE_ALIGNER_NEED_INITIALIZE)},
            {002, new EFEMErrorDetail("Vacuum Error")},
            {003, new EFEMErrorDetail("No Wafer Error")},
            {004, new EFEMErrorDetail("Busy Error(Aligner Is Running)")},
            {005, new EFEMErrorDetail("Aligner is error status")},
            {006, new EFEMErrorDetail("Wafer is Alrady Exist")},
            {007, new EFEMErrorDetail("No Recipe", EM_AL_LST.AL_0901_PRE_ALIGNER_NO_DEFAULT_RECIPE)},

            {011, new EFEMErrorDetail("Notch a ngle data is not received error(T Axis)")},
            {012, new EFEMErrorDetail("Eccentric angle data is not received error(T Axis)")},
            {013, new EFEMErrorDetail("Eccentric movement data is not received error(X Axis)")},
            {014, new EFEMErrorDetail("First notch not find error")},
            {015, new EFEMErrorDetail("Cmos data is wrong error")},
            {016, new EFEMErrorDetail("Wafer center range out error")},
            {017, new EFEMErrorDetail("Second search fail error")},
            {018, new EFEMErrorDetail("Exceed the max notch search count error")},
            {019, new EFEMErrorDetail("Wafer chipping find error")},

            {021, new EFEMErrorDetail("X Axis limit error in Home command")},

            {025, new EFEMErrorDetail("Z Axis is up state Error")},
            {026, new EFEMErrorDetail("T Axis limit error in Home command")},

            {029, new EFEMErrorDetail("Z Axis is down state Error")},
            {030, new EFEMErrorDetail("Z Axis is Down state Error")},

            {031, new EFEMErrorDetail("Manual mode command error")},
            {032, new EFEMErrorDetail("Auto mode command error")},
            {033, new EFEMErrorDetail("Running timeout error")},
                        
            {041, new EFEMErrorDetail("Move Distance Range Over Error")},
            {042, new EFEMErrorDetail("User Input Parameter InValid Error")},

            {050, new EFEMErrorDetail("Vacuum Not Sensiong In TRR TLL Cmd Error")},

            {061, new EFEMErrorDetail("Wafer is wrong Po s.")},
            {062, new EFEMErrorDetail("Wafer X/Y is Over Range va setting")},
            {063, new EFEMErrorDetail("Wafer Center vs RingFrame Center Is Over Range Via Setting")},

            {070, new EFEMErrorDetail("T Axis drive time out error")},
            {071, new EFEMErrorDetail("X Axis drive time out error")},
            {072, new EFEMErrorDetail("Z Axis drive time out error")},
            {073 , new EFEMErrorDetail("T Axis (+)Limit Error")},
            {074 , new EFEMErrorDetail("T Axis ( (--)Limit Error")},
            {075 , new EFEMErrorDetail("X Axis (+)Limit Error")},
            {076 , new EFEMErrorDetail("X Axis ( (--)Limit Error")},
            {077 , new EFEMErrorDetail("Y Axis (+)Limit Error")},
            {078 , new EFEMErrorDetail("Y Axis ( (--)Limit Error")},

            {080, new EFEMErrorDetail("Motion is not connected", EM_AL_LST.AL_0910_ALIGNER_MOTION_NOT_CONNECTED) },
            {081, new EFEMErrorDetail("Light is not connected", EM_AL_LST.AL_0911_ALIGNER_LIGHT_NOT_CONNECTED) },
            {082, new EFEMErrorDetail("DIO is not connected", EM_AL_LST.AL_0912_ALIGNER_DIO_NOT_CONNECTED) },
            {083, new EFEMErrorDetail("Camera is not connected", EM_AL_LST.AL_0913_ALIGNER_CAMERA_NOT_CONNECTED) },
            {084, new EFEMErrorDetail("Light Connection Error", EM_AL_LST.AL_0914_ALIGNER_LIGHT_CONNECTION_ERROR) },

            {090, new EFEMErrorDetail("Robot Arm Extend Status") },

            {099, new EFEMErrorDetail("Invalid command error")},

            #region DIT ALIGNER
            { 200, new EFEMErrorDetail("Exception"          ) },
            { 201, new EFEMErrorDetail("Ptr Null"            ) },
            { 202, new EFEMErrorDetail("Image Null"          ) },
            { 203, new EFEMErrorDetail("Param Null"          ) },
            { 204, new EFEMErrorDetail("ROI Empty"           ) },
            { 205, new EFEMErrorDetail("Fail_ImageProcess"  ) },
            { 206, new EFEMErrorDetail("Fail_FindEllipse"   ) },
            { 207, new EFEMErrorDetail("Fail_FindNotch"     ) },
            { 208, new EFEMErrorDetail("Fail"               ) },
            #endregion DIT ALIGNER
        };

        public static bool IsCriticalAlarm(EmEfemErrorModule errModule, int errCode)
        {
            switch(errModule)
            {
                case EmEfemErrorModule.E0: return E0SystemErrorInfo.ContainsKey(errCode) && E0SystemErrorInfo[errCode].AlarmCode != EM_AL_LST.AL_0000_NONE; 
                case EmEfemErrorModule.E1: return E1RobotErrorInfo.ContainsKey(errCode) && E1RobotErrorInfo[errCode].AlarmCode != EM_AL_LST.AL_0000_NONE;
                case EmEfemErrorModule.E2: return E2LpmPortErrorInfo.ContainsKey(errCode) && E2LpmPortErrorInfo[errCode].AlarmCode != EM_AL_LST.AL_0000_NONE;
                case EmEfemErrorModule.E3: return E3AlignerErrorInfo.ContainsKey(errCode) && E3AlignerErrorInfo[errCode].AlarmCode != EM_AL_LST.AL_0000_NONE;
            }
            return false;
        }
        public static EM_AL_LST GetAlarmCode(EmEfemErrorModule errModule, int errCode)
        {
            switch (errModule)
            {
                case EmEfemErrorModule.E0: return E0SystemErrorInfo[errCode].AlarmCode;
                case EmEfemErrorModule.E1: return E1RobotErrorInfo[errCode].AlarmCode;
                case EmEfemErrorModule.E2: return E2LpmPortErrorInfo[errCode].AlarmCode;
                case EmEfemErrorModule.E3: return E3AlignerErrorInfo[errCode].AlarmCode;
            }
            return EM_AL_LST.AL_0000_NONE;
        }
    }
}
