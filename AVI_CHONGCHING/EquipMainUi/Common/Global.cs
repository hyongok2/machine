using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using DitCim.Common;
using Dit.Framework.PLC;
using EquipMainUi.Setting;
using EquipMainUi.Struct;

namespace EquipMainUi
{
    public enum PreAlignerTypes
    {
        EFEM,
        DIT,
    }
    public class GG
    {
        //1호기 : PreAlignerTypes.EFEM
        //2호기 : PreAlignerTypes.DIT        
        public static PreAlignerTypes PreAlignerType = PreAlignerTypes.DIT; //미사용
        public static bool IsDitPreAligner => PreAlignerType == PreAlignerTypes.DIT; //미사용

        public const bool boChinaLanguage = true; // True = Chinese Language Use , False = Korea Language Use 
        public static bool IsChungJu = true; //청주, 이천 구분
        public static bool TestMode = false;
        public static bool InspTestMode = TestMode;
        public static bool CimTestMode = false;
        public static bool OHTTestMode = false;

        public static bool ManualLdUld = false;
        public static bool RecipeEx = false;
        public static bool EfemNoUse = false;
        public static bool EfemLongRun = false;
        public static bool EfemNoWafer = false;
        public static bool PassLightCurtain { get { return GG.TestMode == true ? true : _passLightCurtain; } set { _passLightCurtain = value; } }
        private static bool _passLightCurtain = true;
        public static bool VacuumFailSkipMode = true; //고객사 요청사항으로 상시 On

        public static bool ReviTestMode = true;

        public static bool UseManualIonizer = false;
        public static bool PioTestMode = false;  //라인에서.. 꼭 false로 되어 있어야함.
        public static bool PrivilegeTestMode = true; //자동 로그아웃 때문에 로그인 비밀번호 해제

        //S1F2  Version
        public static string SPEC_CODE = "SDC_OLED_V235_UNI";
        public static string SOFT_REV = "20150902151515";

        //운영 정보
        public const int CST_SLOT_CNT = 28;
        //public const int MAX_FULL_GLASS = 26;
        //public const int MAX_GLASS_CNT = 52;
        public const int MAX_PORT = 2;
        public const string PASSWORD = "";

        //PATH 정보         
        public static string StartupPath = TestMode == true ? Application.StartupPath : @"D:\DitCtrl\Exec\Ctrl";
        public static FileInfo Exe = new FileInfo(Path.Combine(StartupPath, "EquipMainUi.exe"));
        public static string LogPath = @"D:\DitCtrl\Log";

        public static Equipment Equip = new Equipment();

        public static IVirtualMem CCLINK;
        public static IVirtualMem PMAC;
        public static IVirtualMem MEM_DIT;
        public static IVirtualMem EZI;
        //public static IVirtualMem HSMS;
        //public static IVirtualMem MEM_REV { get; set; }         

        public static PlcAddr X_Addr = new PlcAddr(PlcMemType.X, 0X000, 0, 128);
        public static PlcAddr Y_Addr = new PlcAddr(PlcMemType.Y, 0X000, 0, 128);
        public static PlcAddr Wr_Addr = new PlcAddr(PlcMemType.Wr, 0x000, 0, 128);
        public static PlcAddr Ww_Addr = new PlcAddr(PlcMemType.Ww, 0x000, 0, 128);

        // 이미 실제 사용하는 있는 Address Map과 일치하지 않는다. 그래서 일단 임의로 필요한 부분은 읽어 올 수 있도록 처리 한다. 2024/10/10 문형옥, 정재엽
        public static PlcAddr ISPT_WRITE_MEM                                /**/= new PlcAddr(PlcMemType.W, 0, 0, 5000, PlcValueType.SHORT);
        public static PlcAddr ISPT_READ_MEM                                 /**/= new PlcAddr(PlcMemType.W, 5000, 0, 5000, PlcValueType.SHORT);
        public static PlcAddr HSMS_READ_MEM                                 /**/= new PlcAddr(PlcMemType.W, 10000, 0, 5000, PlcValueType.SHORT);
        public static PlcAddr HSMS_WRITE_MEM                                 /**/= new PlcAddr(PlcMemType.W, 20000, 0, 5000, PlcValueType.SHORT); // HSMS 쓰는 영역은 이것보다 많은데, 위에 동시에 처리하고 있음. kerf Data 추가하면서, 20000번지 이상이 필요한 상황이고.. 이렇게 추가함.2024/10/10 문형옥, 정재엽 
        //public static PlcAddr RV_WRITE_MEM                                  /**/= new PlcAddr(PlcMemType.W, 10000, 0, 5000, PlcValueType.SHORT);
        //public static PlcAddr RV_READ_MEM                                   /**/= new PlcAddr(PlcMemType.W, 15000, 0, 5000, PlcValueType.SHORT);
        //public static PlcAddr CIM_WRITE_MEM                                  /**/= new PlcAddr(PlcMemType.W, 5000, 0, 5300, PlcValueType.SHORT);
        //public static PlcAddr CIM_READ_MEM                                   /**/= new PlcAddr(PlcMemType.W, 0, 0, 5000, PlcValueType.SHORT);

        public static PlcAddr CIM_WRITE_MCC                                  /**/= new PlcAddr(PlcMemType.W, 10300, 0, 1000, PlcValueType.SHORT);

        //PMAC READ 영역...
        public static PlcAddr PMAC_REALTIME_READ_MEM = new PlcAddr(PlcMemType.P, 3000, 0, 384);
        public static PlcAddr PMAC_REALTIME_WRITE_MEM = new PlcAddr(PlcMemType.P, 2000, 0, 384);
        public static PlcAddr PMAC_X_READ_START                           /**/= new PlcAddr(PlcMemType.P, 16101, 0, 3500);
        public static PlcAddr PMAC_Y_WRITE_START                          /**/= new PlcAddr(PlcMemType.P, 6101, 0, 3500);

        #region Address_CCLink
        public static string ADDRESS_CC_LINK = @"
X000	X000	X000	EMERGENCY_STOP_1
X001	X001	X001	EMERGENCY_OFF_2
X002	X002	X002	EMERGENCY_STOP_3
X003	X003	X003	
X004	X004	X004	MODE_SELECT_KEY_SW_AUTOTEACH1
X005	X005	X005	SAFETY_PLC_ERROR
X006	X006	X006	MC_1
X007	X007	X007	
X008	X008	X008	TOP_DOOR1_SENSOR
X009	X009	X009	TOP_DOOR2_SENSOR
X00A	X00A	X00A	TOP_DOOR3_SENSOR
X00B	X00B	X00B	TOP_DOOR4_SENSOR
X00C	X00C	X00C	
X00D	X00D	X00D	WAFER_PIN_DETECT_SENSOR_1
X00E	X00E	X00E	WAFER_STAGE_DETECT_SENSOR_1
X00F	X00F	X00F	WAFER_PIN_DETECT_SENSOR_2
X010	X010	X010	
X011	X011	X011	STANDARD_CENTERING_1_FORWARD_SENSOR
X012	X012	X012	STANDARD_CENTERING_1_BACKWARD_SENSOR
X013	X013	X013	STANDARD_CENTERING_2_FORWARD_SENSOR
X014	X014	X014	STANDARD_CENTERING_2_BACKWARD_SENSOR
X015	X015	X015	LIFT_PIN_1_UP_SENSOR
X016	X016	X016	LIFT_PIN_1_DOWN_SENSOR
X017	X017	X017	LIFT_PIN_2_UP_SENSOR
X018	X018	X018	LIFT_PIN_2_DOWN_SENSOR
X019	X019	X019	
X01A	X01A	X01A	
X01B	X01B	X01B	ROBOT_ARM_DETECT
X01C	X01C	X01C	
X01D	X01D	X01D	
X01E	X01E	X01E	
X01F	X01F	X01F	

Y020	Y020	Y020	PRE_ALIGN_VACUUM_SOL_ON
Y021	Y021	Y021	PRE_ALIGN_PURGE_SOL_ON
Y022	Y022	Y022	PRE_ALIGN_CYLINDER_UP_DOWN
Y023	Y023	Y023	SPARE_SOL_2
Y024	Y024	Y024	AUTO_TEACH_SOL1
Y025	Y025	Y025	SAFETY_CIRCUIT_RESET
Y026	Y026	Y026	EFEM_DOOR1_OPEN_SOL
Y027	Y027	Y027	EFEM_DOOR2_OPEN_SOL
Y028	Y028	Y028	TOP_DOOR1_OPEN_SOL
Y029	Y029	Y029	TOP_DOOR2_OPEN_SOL
Y02A	Y02A	Y02A	TOP_DOOR3_OPEN_SOL
Y02B	Y02B	Y02B	TOP_DOOR4_OPEN_SOL
Y02C	Y02C	Y02C	
Y02D	Y02D	Y02D	CAMERA_COOLING_ON_OFF_SOL
Y02E	Y02E	Y02E	IONIZER_1_AIR_ON_SOL
Y02F	Y02F	Y02F	
Y030	Y030	Y030	
Y031	Y031	Y031	STANDARD_CENTERING_1_FORWARD_SOL
Y032	Y032	Y032	STANDARD_CENTERING_1_BACKWARD_SOL
Y033	Y033	Y033	
Y034	Y034	Y034	
Y035	Y035	Y035	LIFT_PIN_1_UP_SOL
Y036	Y036	Y036	LIFT_PIN_1_DOWN_SOL
Y037	Y037	Y037	
Y038	Y038	Y038	
Y039	Y039	Y039	VACUUM_STAGE_SOL_1
Y03A	Y03A	Y03A	VACUUM_STAGE_SOL_2
Y03B	Y03B	Y03B	
Y03C	Y03C	Y03C	BLOWER_STAGE_SOL_1
Y03D	Y03D	Y03D	BLOWER_STAGE_SOL_2
Y03E	Y03E	Y03E	AIR_KNIFE
Y03F	Y03F	Y03F	SPARE_SOL_3

X040	X040	X040	IONIZER_ABNORMAL_ALARM
X041	X041	X041	
X042	X042	X042	
X043	X043	X043	EFEM_DOOR1_SENSOR
X044	X044	X044	EFEM_DOOR2_SENSOR
X045	X045	X045	
X046	X046	X046	PRE_ALIGN_ROBOT_READY_IN
X047	X047	X047	PRE_ALIGN_VAC_PRESURE_STATUS
X048	X048	X048	PRE_ALIGN_OCR_UP
X049	X049	X049	PRE_ALIGN_OCR_DOWN
X04A	X04A	X04A	PRE_ALIGN_WAFER_DETECT
X04B	X04B	X04B	
X04C	X04C	X04C	ISOLATOR_1
X04D	X04D	X04D	ISOLATOR_2
X04E	X04E	X04E	ISOLATOR_3
X04F	X04F	X04F	EFEM_INPUT_ARM_TO_AVI
Y050	Y050	Y050	IONIZER_1_REMOTE_ON
Y051	Y051	Y051	LIGHT_CURTAIN_MUTING_1
Y052	Y052	Y052	LIGHT_CURTAIN_MUTING_2
Y053	Y053	Y053	LIGHT_CURTAIN_RESET
Y054	Y054	Y054	BUZZER_K1
Y055	Y055	Y055	BUZZER_K2
Y056	Y056	Y056	BUZZER_K3
Y057	Y057	Y057	BUZZER_K4
Y058	Y058	Y058	TOWER_LAMP_RED
Y059	Y059	Y059	TOWER_LAMP_YELLOW
Y05A	Y05A	Y05A	TOWER_LAMP_GREEN
Y05B	Y05B	Y05B	TOWER_LAMP_BLUE
Y05C	Y05C	Y05C	AVI_READY_TO_RECV
Y05D	Y05D	Y05D	INS_TOP_WORKING_LIGHT_POWER_ON_OFF
Y05E	Y05E	Y05E	PRE_ALIGN_READY_OUT
Y05F	Y05F	Y05F	PC_RACK_TOP_WORKING_LIGHT_POWER_ON_OFF

X060	X060	X060	MAIN_AIR_1
X061	X061	X061	MAIN_AIR_2
X062	X062	X062	
X063	X063	X063	MAIN_VACCUM_1
X064	X064	X064	MAIN_VACCUM_2
X065	X065	X065	
X066	X066	X066	CHECK_VACCUM_1
X067	X067	X067	CHECK_VACCUM_2
X068	X068	X068	
X069	X069	X069	
X06A	X06A	X06A	PC_RACK_FAN_ON_OFF_1
X06B	X06B	X06B	PC_RACK_FAN_ON_OFF_2
X06C	X06C	X06C	PC_RACK_FAN_ON_OFF_3
X06D	X06D	X06D	PC_RACK_FAN_ON_OFF_4
X06E	X06E	X06E	PC_RACK_FAN_ON_OFF_5
X06F	X06F	X06F	PC_RACK_FAN_ON_OFF_6
X070	X070	X070	PC_RACK_FAN_ON_OFF_7
X071	X071	X071	PC_RACK_FAN_ON_OFF_8
X072	X072	X072	CP_BOX_FAN_ON_OFF_1
X073	X073	X073	CP_BOX_FAN_ON_OFF_2
X074	X074	X074	CP_BOX_FAN_ON_OFF_3
X075	X075	X075	CP_BOX_FAN_ON_OFF_4
X076	X076	X076	CP_BOX_DOOR_OPEN
X077	X077	X077	
X078	X078	X078	LIGHT_CURTAIN_MUTING_ON_CHECK
X079	X079	X079	LIGTH_CURTAIN_DETECT_1
X07A	X07A	X07A	
X07B	X07B	X07B	
X07C	X07C	X07C	
X07D	X07D	X07D	
X07E	X07E	X07E	
X07F	X07F	X07F	

X080	X080	X080	LP_1_IN1_VALID
X081	X081	X081	LP_1_IN2_CS_0
X082	X082	X082	LP_1_IN3_CS_1
X083	X083	X083	LP_1_IN4_AM_AVBL
X084	X084	X084	LP_1_IN5_TR_REQ
X085	X085	X085	LP_1_IN6_BUSY
X086	X086	X086	LP_1_IN7_COMPT
X087	X087	X087	LP_1_IN8_CONT
X088	X088	X088	LP_2_IN1_VALID
X089	X089	X089	LP_2_IN2_CS_0
X08A	X08A	X08A	LP_2_IN3_CS_1
X08B	X08B	X08B	LP_2_IN4_AM_AVBL
X08C	X08C	X08C	LP_2_IN5_TR_REQ
X08D	X08D	X08D	LP_2_IN6_BUSY
X08E	X08E	X08E	LP_2_IN7_COMPT
X08F	X08F	X08F	LP_2_IN8_CONT
Y090	Y090	Y090	LP_1_OUT1_L_REQ
Y091	Y091	Y091	LP_1_OUT2_U_REQ
Y092	Y092	Y092	LP_1_OUT3_VA
Y093	Y093	Y093	LP_1_OUT4_READY
Y094	Y094	Y094	LP_1_OUT5_VS_0
Y095	Y095	Y095	LP_1_OUT6_VS_1
Y096	Y096	Y096	LP_1_OUT7_HO_AVBL
Y097	Y097	Y097	LP_1_OUT8_ES
Y098	Y098	Y098	LP_2_OUT1_L_REQ
Y099	Y099	Y099	LP_2_OUT2_U_REQ
Y09A	Y09A	Y09A	LP_2_OUT3_VA
Y09B	Y09B	Y09B	LP_2_OUT4_READY
Y09C	Y09C	Y09C	LP_2_OUT5_VS_0
Y09D	Y09D	Y09D	LP_2_OUT6_VS_1
Y09E	Y09E	Y09E	LP_2_OUT7_HO_AVBL
Y09F	Y09F	Y09F	LP_2_OUT8_ES

Wr00	Wr00	Wr00	AD1_CH1_ANALOG_READ_DATA
Wr01	Wr01	Wr01	AD1_CH2_ANALOG_READ_DATA
Wr02	Wr02	Wr02	AD1_CH3_ANALOG_READ_DATA
Wr03	Wr03	Wr03	AD1_CH4_ANALOG_READ_DATA
Wr04	Wr04	Wr04	AD1_CH5_ANALOG_READ_DATA
Wr05	Wr05	Wr05	AD1_CH6_ANALOG_READ_DATA
Wr06	Wr06	Wr06	AD1_CH7_ANALOG_READ_DATA
Wr07	Wr07	Wr07	
Wr08	Wr08	Wr08	
Wr09	Wr09	Wr09	
Wr0A	Wr0A	Wr0A	
Wr0B	Wr0B	Wr0B	
Wr0C	Wr0C	Wr0C	TD_CH1_ANALOG_READ_DATA
Wr0D	Wr0D	Wr0D	TD_CH2_ANALOG_READ_DATA
Wr0E	Wr0E	Wr0E	
Wr0F	Wr0F	Wr0F	
Wr10	Wr10	Wr10	
Wr11	Wr11	Wr11	
Wr12	Wr12	Wr12	
Wr13	Wr13	Wr13	
Wr14	Wr14	Wr14	
Wr15	Wr15	Wr15	
Wr16	Wr16	Wr16	
Wr17	Wr17	Wr17	
Wr18	Wr18	Wr18	
Wr19	Wr19	Wr19	
Wr1A	Wr1A	Wr1A	
Wr1B	Wr1B	Wr1B	
Wr1C	Wr1C	Wr1C	
Wr1D	Wr1D	Wr1D	
Wr1E	Wr1E	Wr1E	
Wr1F	Wr1F	Wr1F	
Ww00	Ww00	Ww00	AD1_CHANGE_PERMIT_PROHIBIT_BIT
Ww01	Ww01	Ww01	AD1_CH1_4_RANGE_SETTING
Ww02	Ww02	Ww02	AD1_CH5_8_RANGE_SETTING
Ww03	Ww03	Ww03	AD1_AVG_PROCESS
Ww04	Ww04	Ww04	AD1_CH1_AVG_COUNT_SET
Ww05	Ww05	Ww05	AD1_CH2_AVG_COUNT_SET
Ww06	Ww06	Ww06	AD1_CH3_AVG_COUNT_SET
Ww07	Ww07	Ww07	AD1_CH4_AVG_COUNT_SET
Ww08	Ww08	Ww08	AD1_CH5_AVG_COUNT_SET
Ww09	Ww09	Ww09	AD1_CH6_AVG_COUNT_SET
Ww0A	Ww0A	Ww0A	AD1_CH7_AVG_COUNT_SET
Ww0B	Ww0B	Ww0B	TD_CH1_ANALOG_READ_DATA
Ww0C	Ww0C	Ww0C	TD_CH2_ANALOG_READ_DATA
Ww0D	Ww0D	Ww0D	
Ww0E	Ww0E	Ww0E	
Ww0F	Ww0F	Ww0F	
Ww10	Ww10	Ww10	
Ww11	Ww11	Ww11	
Ww12	Ww12	Ww12	
Ww13	Ww13	Ww13	
Ww14	Ww14	Ww14	
Ww15	Ww15	Ww15	
Ww16	Ww16	Ww16	
Ww17	Ww17	Ww17	
Ww18	Ww18	Ww18	
Ww19	Ww19	Ww19	
Ww1A	Ww1A	Ww1A	
Ww1B	Ww1B	Ww1B	
Ww1C	Ww1C	Ww1C	
Ww1D	Ww1D	Ww1D	
Ww1E	Ww1E	Ww1E	
Ww1F	Ww1F	Ww1F	
";
        #endregion Address_CCLink
        #region Address_Pmac
        public static string ADDRESS_PMAC { get { return CreatePmacAddress(); } }

        private static string CreatePmacAddress()
        {
            StringBuilder sb = new StringBuilder();
            #region Output
            int pcToPmacStatusHeadAddr = 2310;
            sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tPMAC_YB_EquipState", pcToPmacStatusHeadAddr, 0));

            int equipCommonCmdHeadAddr = 2330;
            sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tPMAC_YB_CommonCmd", equipCommonCmdHeadAddr, 0));
            #endregion
            #region Input            
            int pmacToPCStatusHeadAddr = 3310;
            sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tPMAC_XB_PmacState", pmacToPCStatusHeadAddr, 0));

            int equipCommonAckHeadAddr = 3330;
            sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tPMAC_XB_CommonAck", equipCommonAckHeadAddr, 0));

            sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tXF_StageXPPCmd", 3350, 0));
            sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tXF_StageYPPCmd", 3351, 0));
            sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tXF_StageTPPCmd", 3352, 0));

            sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tXF_StageXHmSeq", 3353, 0));
            sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tXF_StageYHmSeq", 3354, 0));
            sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tXF_StageTHmSeq", 3355, 0));

            sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tXF_ScanStep", 3356, 0));

            sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tXF_Mtr01TargetPos", 3357, 0));
            sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tXF_Mtr02TargetPos", 3358, 0));
            sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tXF_Mtr03TargetPos", 3359, 0));

            #endregion

            for (int axis = 0; axis < 32; ++axis)
            {
                #region Output                
                sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tAxis{1:D2}_YF_TargetPosition", 2101 + axis, axis + 1));
                sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tAxis{1:D2}_YF_TargetSpeed", 2133 + axis, axis + 1));
                sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tAxis{1:D2}_YF_TargetAccTime", 2165 + axis, axis + 1));
                sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tAxis{1:D2}_YF_JogSpeed", 2265 + axis, axis + 1));
                if (axis < 2)
                    sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tAxis{1:D2}_YF_LoadRate", 2335 + axis, axis + 1));

                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_YB_HomeCmd", 2331, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_YB_TargetPosMoveCmd", 2332, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_YB_JogMinusCmd", 2333, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_YB_JogPlusCmd", 2334, axis, axis + 1));
                #endregion
                #region Input                                
                sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tAxis{1:D2}_XF_CurPosition", 3001 + axis, axis + 1));
                sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tAxis{1:D2}_XF_CurSpeed", 3033 + axis, axis + 1));
                sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tAxis{1:D2}_XF_CurToque", 3065 + axis, axis + 1));
                sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tAxis{1:D2}_XF_TargetPosition", 3101 + axis, axis + 1));
                sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tAxis{1:D2}_XF_TargetSpeed", 3133 + axis, axis + 1));
                sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tAxis{1:D2}_XF_TargetAccTime", 3165 + axis, axis + 1));
                sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tAxis{1:D2}_XF_JogSpeed", 3265 + axis, axis + 1));
                if (axis < 2)
                    sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tAxis{1:D2}_XF_LoadRate", 3335 + axis, axis + 1));

                sb.AppendLine(string.Format("P{0}\tP{0}\tP{0}\tAxis{1:D2}__XF_MotorStep", /*kkt*/33333 + axis, axis + 1));

                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_XB_HomeAck", 3331, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_XB_TargetPosMoveAck", 3332, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_XB_JogMinusAck", 3333, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_XB_JogPlusAck", 3334, axis, axis + 1));

                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_XB_HomeComplete", 3311, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_XB_HomePositionOn", 3312, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_XB_Moving", 3313, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_XB_HWMinusLimit", 3314, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_XB_HWPlusLimit", 3315, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_XB_ServoON", 3316, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_XB_FatalFollowingError", 3317, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_XB_AmpFaultError", 3318, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_XB_I2TAmpFaultError", 3319, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_XB_TargetPosMoveComplete", 3320, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_XB_SWMinusLimit", 3321, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_XB_SWPlusLimit", 3322, axis, axis + 1));
                sb.AppendLine(string.Format("S{0}.{1}\tS{0}.{1}\tS{0}.{1}\tAxis{2:D2}_XB_MotorAlarm", 3323, axis, axis + 1));
                #endregion
            }

            return sb.ToString();
        }
        #endregion Address_Pmac
        #region Address_PIO
        public static string ADDRESS_PIO = @"
R1000	R1000	R1000	PioI2A_LoHeartBit
R1001	R1001	R1001	PioI2A_LoMachinePause
R1002	R1002	R1002	PioI2A_LoMachineDown
R1003	R1003	R1003	PioI2A_LoMachineAlarm
R1004	R1004	R1004	PioI2A_LoReceiveAble
R1005	R1005	R1005	PioI2A_LoReceiveStart
R1006	R1006	R1006	PioI2A_LoReceiveComplete
R1007	R1007	R1007	PioI2A_LoExchangeFlag
R1008	R1008	R1008	PioI2A_LoReturnSendStart
R1009	R1009	R1009	PioI2A_LoReturnSendComplete
R1010	R1010	R1010	PioI2A_LoImmediatelyPauseRequest
R1011	R1011	R1011	PioI2A_LoImmediatelyStopRequest
R1012	R1012	R1012	PioI2A_LoReceiveAbleRemainedStep1
R1013	R1013	R1013	PioI2A_LoReceiveAbleRemainedStep2
R1014	R1014	R1014	PioI2A_LoReceiveAbleRemainedStep3
R1015	R1015	R1015	PioI2A_LoReceiveAbleRemainedStep4
R1016	R1016	R1016	
R1017	R1017	R1017	PioI2A_LoGlassIDReadComplete
R1018	R1018	R1018	PioI2A_LoLoadingStop
R1019	R1019	R1019	PioI2A_LoTransferStop
R1020	R1020	R1020	PioI2A_LoExchangeFailFlag
R1021	R1021	R1021	PioI2A_LoProcessTimeUp
R1022	R1022	R1022	PioI2A_
R1023	R1023	R1023	PioI2A_
R1024	R1024	R1024	PioI2A_LoReceiveAbleReserveRequest
R1025	R1025	R1025	PioI2A_LoHandShakeCancelRequest
R1026	R1026	R1026	PioI2A_LoHandShakeAbortRequest
R1027	R1027	R1027	PioI2A_LoHandShakeResumeRequest
R1028	R1028	R1028	PioI2A_LoHandShakeRecoveryAckReply
R1029	R1029	R1029	PioI2A_LoHandShakeRecoveryNakReply
R1030	R1030	R1030	PioI2A_LoReceiveJobReady
R1031	R1031	R1031	PioI2A_LoReceiveActionMove
R1032	R1032	R1032	PioI2A_LoReceiveActionRemove
R1033	R1033	R1033	PioI2A_
R1034	R1034	R1034	PioI2A_
R1035	R1035	R1035	PioI2A_LoAbnormal
R1036	R1036	R1036	PioI2A_LoTypeofArm
R1037	R1037	R1037	PioI2A_LoTypeofStageConveyor
R1038	R1038	R1038	PioI2A_LoArmStretchUpMoving
R1039	R1039	R1039	PioI2A_LoArmStretchUpComplete
R1040	R1040	R1040	PioI2A_LoArmStretchDownMoving
R1041	R1041	R1041	PioI2A_LoArmStretchDownComplete
R1042	R1042	R1042	PioI2A_LoArmStretching
R1043	R1043	R1043	PioI2A_LoArmStretchComplete
R1044	R1044	R1044	PioI2A_LoArmFolding
R1045	R1045	R1045	PioI2A_LoArmFoldComplete
R1046	R1046	R1046	PioI2A_
R1047	R1047	R1047	PioI2A_
R1048	R1048	R1048	PioI2A_LoArm1Folded
R1049	R1049	R1049	PioI2A_LoArm2Folded
R1050	R1050	R1050	PioI2A_LoArm1GlassDetect
R1051	R1051	R1051	PioI2A_
R1052	R1052	R1052	PioI2A_LoArm2GlassDetect
R1053	R1053	R1053	PioI2A_LoArm1GlassVacuum
R1054	R1054	R1054	PioI2A_LoArm2GlassVacuum
R1055	R1055	R1055	PioI2A_LoRobotDirection
R1056	R1056	R1056	PioI2A_LoManualOperation
R1057	R1057	R1057	PioI2A_LoPinUp
R1058	R1058	R1058	PioI2A_LoPinDown
R1059	R1059	R1059	PioI2A_LoDoorOpen
R1060	R1060	R1060	PioI2A_LoDoorClose
R1061	R1061	R1061	PioI2A_LoGlassDetect
R1062	R1062	R1062	PioI2A_LoBodyMoving
R1063	R1063	R1063	PioI2A_LoBodyOriginPosition
R1064	R1064	R1064	PioI2A_LoEmergency
R1065	R1065	R1065	PioI2A_LoVertical
R1066	R1066	R1066	PioI2A_LoHorizontal
R1067	R1067	R1067	PioI2A_
R1068	R1068	R1068	PioI2A_                                
R1069	R1069	R1069	PioI2A_UpHeartBit
R1070	R1070	R1070	PioI2A_UpMachinePause
R1071	R1071	R1071	PioI2A_UpMachineDown
R1072	R1072	R1072	PioI2A_UpMachineAlarm
R1073	R1073	R1073	PioI2A_UpSendAble
R1074	R1074	R1074	PioI2A_UpSendStart
R1075	R1075	R1075	PioI2A_UpSendComplete
R1076	R1076	R1076	PioI2A_UpExchangeFlag
R1077	R1077	R1077	PioI2A_UpReturnReceiveStart
R1078	R1078	R1078	PioI2A_UpReturnReceiveComplete
R1079	R1079	R1079	PioI2A_UpImmediatelyPauseRequest
R1080	R1080	R1080	PioI2A_UpImmediatelyStopRequest
R1081	R1081	R1081	PioI2A_UpSendAbleRemainedStep1
R1082	R1082	R1082	PioI2A_UpSendAbleRemainedStep2
R1083	R1083	R1083	PioI2A_UpSendAbleRemainedStep3
R1084	R1084	R1084	PioI2A_UpSendAbleRemainedStep4
R1085	R1085	R1085	PioI2A_
R1086	R1086	R1086	PioI2A_UpWorkStart
R1087	R1087	R1087	PioI2A_UpWorkCancel
R1088	R1088	R1088	PioI2A_UpWorkSkip
R1089	R1089	R1089	PioI2A_UpJobStart
R1090	R1090	R1090	PioI2A_UpJobEnd
R1091	R1091	R1091	PioI2A_UpHotFlow
R1092	R1092	R1092	PioI2A_
R1093	R1093	R1093	PioI2A_LOSendAbleReserveRequest
R1094	R1094	R1094	PioI2A_UpHandShakeCancelRequest
R1095	R1095	R1095	PioI2A_UpHandShakeAbortRequest
R1096	R1096	R1096	PioI2A_UpHandShakeResumeRequest
R1097	R1097	R1097	PioI2A_UpHandShakeRecoveryAckReply
R1098	R1098	R1098	PioI2A_UpHandShakeRecoveryNakReply
R1099	R1099	R1099	PioI2A_UpSendJobReady
R1100	R1100	R1100	PioI2A_UpSendActionMove
R1101	R1101	R1101	PioI2A_UpSendActionRemove
R1102	R1102	R1102	PioI2A_
R1103	R1103	R1103	PioI2A_
R1104	R1104	R1104	PioI2A_
R1105	R1105	R1105	PioI2A_UpAbnormal
R1106	R1106	R1106	PioI2A_UpTypeofArm
R1107	R1107	R1107	PioI2A_UpTypeofStageConveyor
R1108	R1108	R1108	PioI2A_UpArmStretchUpMoving
R1109	R1109	R1109	PioI2A_UpArmStretchUpComplete
R1110	R1110	R1110	PioI2A_UpArmStretchDownMoving
R1111	R1111	R1111	PioI2A_UpArmStretchDownComplete
R1112	R1112	R1112	PioI2A_UpArmStretching
R1113	R1113	R1113	PioI2A_UpArmStretchComplete
R1114	R1114	R1114	PioI2A_UpArmFolding
R1115	R1115	R1115	PioI2A_UpArmFoldComplete
R1116	R1116	R1116	PioI2A_
R1117	R1117	R1117	PioI2A_
R1118	R1118	R1118	PioI2A_UpArm1Folded
R1119	R1119	R1119	PioI2A_UpArm2Folded
R1120	R1120	R1120	PioI2A_UpArm1GlassDetect
R1121	R1121	R1121	PioI2A_
R1122	R1122	R1122	PioI2A_UpArm2GlassDetect
R1123	R1123	R1123	PioI2A_UpArm1GlassVacuum
R1124	R1124	R1124	PioI2A_UpArm2GlassVacuum
R1125	R1125	R1125	PioI2A_UpRobotDirection
R1126	R1126	R1126	PioI2A_UpManualOperation
R1127	R1127	R1127	PioI2A_UpPinUp
R1128	R1128	R1128	PioI2A_UpPinDown
R1129	R1129	R1129	PioI2A_UpDoorOpen
R1130	R1130	R1130	PioI2A_UpDoorClose
R1131	R1131	R1131	PioI2A_UpGlassDetect
R1132	R1132	R1132	PioI2A_UpBodyMoving
R1133	R1133	R1133	PioI2A_UpBodyOriginPosition
R1134	R1134	R1134	PioI2A_UpEmergency
R1135	R1135	R1135	PioI2A_UpVertical
R1136	R1136	R1136	PioI2A_UpHorizontal

R2000	R2000	R2000	PioA2I_LoHeartBit
R2001	R2001	R2001	PioA2I_LoMachinePause
R2002	R2002	R2002	PioA2I_LoMachineDown
R2003	R2003	R2003	PioA2I_LoMachineAlarm
R2004	R2004	R2004	PioA2I_LoReceiveAble
R2005	R2005	R2005	PioA2I_LoReceiveStart
R2006	R2006	R2006	PioA2I_LoReceiveComplete
R2007	R2007	R2007	PioA2I_LoExchangeFlag
R2008	R2008	R2008	PioA2I_LoReturnSendStart
R2009	R2009	R2009	PioA2I_LoReturnSendComplete
R2010	R2010	R2010	PioA2I_LoImmediatelyPauseRequest
R2011	R2011	R2011	PioA2I_LoImmediatelyStopRequest
R2012	R2012	R2012	PioA2I_LoReceiveAbleRemainedStep1
R2013	R2013	R2013	PioA2I_LoReceiveAbleRemainedStep2
R2014	R2014	R2014	PioA2I_LoReceiveAbleRemainedStep3
R2015	R2015	R2015	PioA2I_LoReceiveAbleRemainedStep4
R2016	R2016	R2016	PioA2I_
R2017	R2017	R2017	PioA2I_LoGlassIDReadComplete
R2018	R2018	R2018	PioA2I_LoLoadingStop
R2019	R2019	R2019	PioA2I_LoTransferStop
R2020	R2020	R2020	PioA2I_LoExchangeFailFlag
R2021	R2021	R2021	PioA2I_LoProcessTimeUp
R2022	R2022	R2022	PioA2I_
R2023	R2023	R2023	PioA2I_
R2024	R2024	R2024	PioA2I_LoReceiveAbleReserveRequest
R2025	R2025	R2025	PioA2I_LoHandShakeCancelRequest
R2026	R2026	R2026	PioA2I_LoHandShakeAbortRequest
R2027	R2027	R2027	PioA2I_LoHandShakeResumeRequest
R2028	R2028	R2028	PioA2I_LoHandShakeRecoveryAckReply
R2029	R2029	R2029	PioA2I_LoHandShakeRecoveryNakReply
R2030	R2030	R2030	PioA2I_LoReceiveJobReady
R2031	R2031	R2031	PioA2I_LoReceiveActionMove
R2032	R2032	R2032	PioA2I_LoReceiveActionRemove
R2033	R2033	R2033	PioA2I_
R2034	R2034	R2034	PioA2I_
R2035	R2035	R2035	PioA2I_LoAbnormal
R2036	R2036	R2036	PioA2I_LoTypeofArm
R2037	R2037	R2037	PioA2I_LoTypeofStageConveyor
R2038	R2038	R2038	PioA2I_LoArmStretchUpMoving
R2039	R2039	R2039	PioA2I_LoArmStretchUpComplete
R2040	R2040	R2040	PioA2I_LoArmStretchDownMoving
R2041	R2041	R2041	PioA2I_LoArmStretchDownComplete
R2042	R2042	R2042	PioA2I_LoArmStretching
R2043	R2043	R2043	PioA2I_LoArmStretchComplete
R2044	R2044	R2044	PioA2I_LoArmFolding
R2045	R2045	R2045	PioA2I_LoArmFoldComplete
R2046	R2046	R2046	PioA2I_
R2047	R2047	R2047	PioA2I_
R2048	R2048	R2048	PioA2I_LoArm1Folded
R2049	R2049	R2049	PioA2I_LoArm2Folded
R2050	R2050	R2050	PioA2I_LoArm1GlassDetect
R2051	R2051	R2051	PioA2I_
R2052	R2052	R2052	PioA2I_LoArm2GlassDetect
R2053	R2053	R2053	PioA2I_LoArm1GlassVacuum
R2054	R2054	R2054	PioA2I_LoArm2GlassVacuum
R2055	R2055	R2055	PioA2I_LoRobotDirection
R2056	R2056	R2056	PioA2I_LoManualOperation
R2057	R2057	R2057	PioA2I_LoPinUp
R2058	R2058	R2058	PioA2I_LoPinDown
R2059	R2059	R2059	PioA2I_LoDoorOpen
R2060	R2060	R2060	PioA2I_LoDoorClose
R2061	R2061	R2061	PioA2I_LoGlassDetect
R2062	R2062	R2062	PioA2I_LoBodyMoving
R2063	R2063	R2063	PioA2I_LoBodyOriginPosition
R2064	R2064	R2064	PioA2I_LoEmergency
R2065	R2065	R2065	PioA2I_LoVertical
R2066	R2066	R2066	PioA2I_LoHorizontal
R2067	R2067	R2067	PioA2I_
R2068	R2068	R2068	PioA2I_                                
R2069	R2069	R2069	PioA2I_UpHeartBit
R2070	R2070	R2070	PioA2I_UpMachinePause
R2071	R2071	R2071	PioA2I_UpMachineDown
R2072	R2072	R2072	PioA2I_UpMachineAlarm
R2073	R2073	R2073	PioA2I_UpSendAble
R2074	R2074	R2074	PioA2I_UpSendStart
R2075	R2075	R2075	PioA2I_UpSendComplete
R2076	R2076	R2076	PioA2I_UpExchangeFlag
R2077	R2077	R2077	PioA2I_UpReturnReceiveStart
R2078	R2078	R2078	PioA2I_UpReturnReceiveComplete
R2079	R2079	R2079	PioA2I_UpImmediatelyPauseRequest
R2080	R2080	R2080	PioA2I_UpImmediatelyStopRequest
R2081	R2081	R2081	PioA2I_UpSendAbleRemainedStep1
R2082	R2082	R2082	PioA2I_UpSendAbleRemainedStep2
R2083	R2083	R2083	PioA2I_UpSendAbleRemainedStep3
R2084	R2084	R2084	PioA2I_UpSendAbleRemainedStep4
R2085	R2085	R2085	PioA2I_
R2086	R2086	R2086	PioA2I_UpWorkStart
R2087	R2087	R2087	PioA2I_UpWorkCancel
R2088	R2088	R2088	PioA2I_UpWorkSkip
R2089	R2089	R2089	PioA2I_UpJobStart
R2090	R2090	R2090	PioA2I_UpJobEnd
R2091	R2091	R2091	PioA2I_UpHotFlow
R2092	R2092	R2092	PioA2I_
R2093	R2093	R2093	PioA2I_LOSendAbleReserveRequest
R2094	R2094	R2094	PioA2I_UpHandShakeCancelRequest
R2095	R2095	R2095	PioA2I_UpHandShakeAbortRequest
R2096	R2096	R2096	PioA2I_UpHandShakeResumeRequest
R2097	R2097	R2097	PioA2I_UpHandShakeRecoveryAckReply
R2098	R2098	R2098	PioA2I_UpHandShakeRecoveryNakReply
R2099	R2099	R2099	PioA2I_UpSendJobReady
R2100	R2100	R2100	PioA2I_UpSendActionMove
R2101	R2101	R2101	PioA2I_UpSendActionRemove
R2102	R2102	R2102	PioA2I_
R2103	R2103	R2103	PioA2I_
R2104	R2104	R2104	PioA2I_
R2105	R2105	R2105	PioA2I_UpAbnormal
R2106	R2106	R2106	PioA2I_UpTypeofArm
R2107	R2107	R2107	PioA2I_UpTypeofStageConveyor
R2108	R2108	R2108	PioA2I_UpArmStretchUpMoving
R2109	R2109	R2109	PioA2I_UpArmStretchUpComplete
R2110	R2110	R2110	PioA2I_UpArmStretchDownMoving
R2111	R2111	R2111	PioA2I_UpArmStretchDownComplete
R2112	R2112	R2112	PioA2I_UpArmStretching
R2113	R2113	R2113	PioA2I_UpArmStretchComplete
R2114	R2114	R2114	PioA2I_UpArmFolding
R2115	R2115	R2115	PioA2I_UpArmFoldComplete
R2116	R2116	R2116	PioA2I_
R2117	R2117	R2117	PioA2I_
R2118	R2118	R2118	PioA2I_UpArm1Folded
R2119	R2119	R2119	PioA2I_UpArm2Folded
R2120	R2120	R2120	PioA2I_UpArm1GlassDetect
R2121	R2121	R2121	PioA2I_
R2122	R2122	R2122	PioA2I_UpArm2GlassDetect
R2123	R2123	R2123	PioA2I_UpArm1GlassVacuum
R2124	R2124	R2124	PioA2I_UpArm2GlassVacuum
R2125	R2125	R2125	PioA2I_UpRobotDirection
R2126	R2126	R2126	PioA2I_UpManualOperation
R2127	R2127	R2127	PioA2I_UpPinUp
R2128	R2128	R2128	PioA2I_UpPinDown
R2129	R2129	R2129	PioA2I_UpDoorOpen
R2130	R2130	R2130	PioA2I_UpDoorClose
R2131	R2131	R2131	PioA2I_UpGlassDetect
R2132	R2132	R2132	PioA2I_UpBodyMoving
R2133	R2133	R2133	PioA2I_UpBodyOriginPosition
R2134	R2134	R2134	PioA2I_UpEmergency
R2135	R2135	R2135	PioA2I_UpVertical
R2136	R2136	R2136	PioA2I_UpHorizontal
";
        #endregion Address_PIO
        #region Adress_EziStepMotor
        public static string CreateEziAddress()
        {
            StringBuilder sb = new StringBuilder();

            for (int axis = 0; axis < 3; axis++)
            {
                sb.AppendLine(string.Format("EM{0}.{1}	EM{0}.{1}	EM{0}.{1}	Step_Motor_{1}_XB_StatusHomeCompleteBit", 0, axis));
                sb.AppendLine(string.Format("EM{0}.{1}	EM{0}.{1}	EM{0}.{1}	Step_Motor_{1}_XB_StatusMotorMoving", 1, axis));
                sb.AppendLine(string.Format("EM{0}.{1}	EM{0}.{1}	EM{0}.{1}	Step_Motor_{1}_XB_StatusMinusLimitSet", 2, axis));
                sb.AppendLine(string.Format("EM{0}.{1}	EM{0}.{1}	EM{0}.{1}	Step_Motor_{1}_XB_StatusPlusLimitSet", 3, axis));
                sb.AppendLine(string.Format("EM{0}.{1}	EM{0}.{1}	EM{0}.{1}	Step_Motor_{1}_XB_StatusMotorServoOn", 4, axis));
                sb.AppendLine(string.Format("EM{0}.{1}	EM{0}.{1}	EM{0}.{1}	Step_Motor_{1}_XB_StatusHomeInPosition", 5, axis));
                sb.AppendLine(string.Format("EM{0}.{1}	EM{0}.{1}	EM{0}.{1}	Step_Motor_{1}_XB_StatusMotorInPosition", 6, axis));

                sb.AppendLine(string.Format("EM{0}.{1}	EM{0}.{1}	EM{0}.{1}	Step_Motor_{1}_YB_MotorStopCmd", 7, axis));
                sb.AppendLine(string.Format("EM{0}.{1}	EM{0}.{1}	EM{0}.{1}	Step_Motor_{1}_XB_MotorStopCmdAck", 8, axis));
                sb.AppendLine(string.Format("EM{0}.{1}	EM{0}.{1}	EM{0}.{1}	Step_Motor_{1}_YB_HomeCmd", 9, axis));
                sb.AppendLine(string.Format("EM{0}.{1}	EM{0}.{1}	EM{0}.{1}	Step_Motor_{1}_XB_HomeCmdAck", 10, axis));

                sb.AppendLine(string.Format("EM{0}.{1}	EM{0}.{1}	EM{0}.{1}	Step_Motor_{1}_YB_MotorJogMinusMove", 11, axis));
                sb.AppendLine(string.Format("EM{0}.{1}	EM{0}.{1}	EM{0}.{1}	Step_Motor_{1}_YB_MotorJogPlusMove", 12, axis));

                sb.AppendLine(string.Format("EM{0}.{1}	EM{0}.{1}	EM{0}.{1}	Step_Motor_{1}_YB_PTPMoveCmd", 13, axis));
                sb.AppendLine(string.Format("EM{0}.{1}	EM{0}.{1}	EM{0}.{1}	Step_Motor_{1}_XB_PTPMoveCmdAck", 14, axis));


                sb.AppendLine(string.Format("ER{0}	ER{0}	ER{0}	Step_Motor_{1}_XF_CurrMotorPosition", 100 + axis, axis));
                sb.AppendLine(string.Format("ER{0}	ER{0}	ER{0}	Step_Motor_{1}_XF_CurrMotorSpeed", 110 + axis, axis));
                sb.AppendLine(string.Format("ER{0}	ER{0}	ER{0}	Step_Motor_{1}_XF_CurrMotorAccel", 120 + axis, axis));

                sb.AppendLine(string.Format("ER{0}	ER{0}	ER{0}	Step_Motor_{1}_YF_MotorJogSpeedCmd", 130 + axis, axis));
                sb.AppendLine(string.Format("ER{0}	ER{0}	ER{0}	Step_Motor_{1}_XF_MotorJogSpeedCmdAck", 140 + axis, axis));

                sb.AppendLine(string.Format("ER{0}	ER{0}	ER{0}	Step_Motor_{1}_YF_PTPMovePosition", 150 + axis, axis));
                sb.AppendLine(string.Format("ER{0}	ER{0}	ER{0}	Step_Motor_{1}_XF_PTPMovePositionAck", 160 + axis, axis));
                sb.AppendLine(string.Format("ER{0}	ER{0}	ER{0}	Step_Motor_{1}_YF_PTPMoveSpeed", 170 + axis, axis));
                sb.AppendLine(string.Format("ER{0}	ER{0}	ER{0}	Step_Motor_{1}_XF_PTPMoveSpeedAck", 180 + axis, axis));
                sb.AppendLine(string.Format("ER{0}	ER{0}	ER{0}	Step_Motor_{1}_YF_PTPMoveAccel", 190 + axis, axis));
                sb.AppendLine(string.Format("ER{0}	ER{0}	ER{0}	Step_Motor_{1}_XF_PTPMoveAccelAck", 200 + axis, axis));
            }

            return sb.ToString();
        }
        #endregion
    }
}
