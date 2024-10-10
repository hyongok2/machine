using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using EquipMainUi.Log;
using Dit.Framework;
using EquipMainUi.Struct.Detail.EFEM;
using EquipMainUi.Struct.Detail.HSMS;
using EquipMainUi.Struct.Detail.HSMS.ReportStruct;
using System.Threading;

namespace EquipMainUi.Struct
{
    public enum EM_AL_STATE
    {
        Heavy,
        Warn,
        Unused
    }
    #region ALARM ID LIST
    public enum EM_AL_LST
    {
        AL_0000_NONE,
        AL_0001_EMERGENCY_1_ERROR,
        AL_0002_EMERGENCY_2_ERROR,
        AL_0003_EMERGENCY_3_ERROR,
        AL_0004_,
        AL_0005_,
        AL_0006_,
        AL_0007_,
        AL_0008_,
        AL_0009_,
        AL_0010_,
        AL_0011_,
        AL_0012_,
        AL_0013_,
        AL_0014_,
        AL_0015_,
        AL_0016_,
        AL_0017_,
        AL_0018_,
        AL_0019_,
        AL_0020_EQP_TOP_DOOR_01_OPEN_ERROR,
        AL_0021_EQP_TOP_DOOR_02_OPEN_ERROR,
        AL_0022_EQP_TOP_DOOR_03_OPEN_ERROR,
        AL_0023_EQP_TOP_DOOR_04_OPEN_ERROR,
        AL_0024_TOP_DOOR_01_OPEN,
        AL_0025_TOP_DOOR_02_OPEN,
        AL_0026_TOP_DOOR_03_OPEN,
        AL_0027_TOP_DOOR_04_OPEN,
        AL_0028_,
        AL_0029_EFEM_DOOR_01_OPEN,
        AL_0030_EFEM_DOOR_02_OPEN,
        AL_0031_,
        AL_0032_,
        AL_0033_,
        AL_0034_,
        AL_0035_,
        AL_0036_,
        AL_0037_,
        AL_0038_,
        AL_0039_,
        AL_0040_,
        AL_0041_,
        AL_0042_,
        AL_0043_,
        AL_0044_,
        AL_0045_,
        AL_0046_,
        AL_0047_,
        AL_0048_,
        AL_0049_,
        AL_0050_,
        AL_0051_,
        AL_0052_,
        AL_0053_,
        AL_0054_,
        AL_0055_,
        AL_0056_,
        AL_0057_,
        AL_0058_,
        AL_0059_,
        AL_0060_,
        AL_0061_,
        AL_0062_,
        AL_0063_PIN_IONIZER_ABNORMAL_ALARM,
        AL_0064_PIN_IONIZER_DECAY_TIME_OVER,
        AL_0065_,
        AL_0066_,
        AL_0067_,
        AL_0068_,
        AL_0069_,
        AL_0070_,
        AL_0071_,
        AL_0072_,
        AL_0073_,
        AL_0074_,
        AL_0075_,
        AL_0076_,
        AL_0077_,
        AL_0078_,
        AL_0079_,
        AL_0080_ISOLATOR1_ERROR,
        AL_0081_ISOLATOR2_ERROR,
        AL_0082_ISOLATOR3_ERROR,
        AL_0083_,
        AL_0084_,
        AL_0085_,
        AL_0086_,
        AL_0087_,
        AL_0088_,
        AL_0089_,
        AL_0090_,
        AL_0091_,
        AL_0092_,
        AL_0093_,
        AL_0094_,
        AL_0095_,
        AL_0096_,
        AL_0097_,
        AL_0098_,
        AL_0099_,
        AL_0100_MAIN_AIR_1_ERROR,
        AL_0101_MAIN_AIR_2_ERROR,
        AL_0102_,
        AL_0103_,
        AL_0104_MAIN_VACUUM1_ERROR,
        AL_0105_MAIN_VACUUM2_ERROR,
        AL_0106_STAGE_VACCUM1_SOL_ON_TIME_OUT_ERROR,
        AL_0107_STAGE_VACCUM1_SOL_OFF_TIME_OUT_ERROR,
        AL_0108_RING_FRAME_VACCUM2_SOL_ON_TIME_OUT_ERROR,
        AL_0109_RING_FRAME_VACCUM2_SOL_OFF_TIME_OUT_ERROR,
        AL_0110_VACUUM_ON_LIFTPIN_CANT_UP,
        AL_0111_EMS_STOP_VACUUM_OFF_ERROR,
        AL_0112_CAM_COOLING_OFF_ERROR,
        AL_0113_,
        AL_0114_,
        AL_0115_ALIGNER_VACUUM_SOL_ON_TIME_OUT_ERROR,
        AL_0116_ALIGNER_VACUUM_SOL_OFF_TIME_OUT_ERROR,
        AL_0117_,
        AL_0118_,
        AL_0119_,
        AL_0120_,
        AL_0121_,
        AL_0122_,
        AL_0123_,
        AL_0124_,
        AL_0125_,
        AL_0126_,
        AL_0127_,
        AL_0128_,
        AL_0129_,
        AL_0130_,
        AL_0131_,
        AL_0132_,
        AL_0133_,
        AL_0134_,
        AL_0135_,
        AL_0136_,
        AL_0137_,
        AL_0138_,
        AL_0139_,
        AL_0140_,
        AL_0141_,
        AL_0142_,
        AL_0143_,
        AL_0144_,
        AL_0145_,
        AL_0146_,
        AL_0147_,
        AL_0148_,
        AL_0149_,
        AL_0150_,
        AL_0151_,
        AL_0152_,
        AL_0153_,
        AL_0154_,
        AL_0155_,
        AL_0156_,
        AL_0157_,
        AL_0158_,
        AL_0159_,
        AL_0160_,
        AL_0161_,
        AL_0162_,
        AL_0163_,
        AL_0164_,
        AL_0165_,
        AL_0166_,
        AL_0167_,
        AL_0168_,
        AL_0169_,
        AL_0170_,
        AL_0171_,
        AL_0172_,
        AL_0173_,
        AL_0174_,
        AL_0175_,
        AL_0176_,
        AL_0177_,
        AL_0178_,
        AL_0179_,
        AL_0180_,
        AL_0181_,
        AL_0182_,
        AL_0183_,
        AL_0184_,
        AL_0185_,
        AL_0186_,
        AL_0187_,
        AL_0188_,
        AL_0189_,
        AL_0190_,
        AL_0191_,
        AL_0192_,
        AL_0193_,
        AL_0194_,
        AL_0195_,
        AL_0196_,
        AL_0197_,
        AL_0198_,
        AL_0199_,
        AL_0200_LIFT_PIN_IS_NOT_DOWN_POSITION_STATE_ERROR,
        AL_0201_LIFT_PIN_IS_NOT_UP_POSITION_STATE_ERROR,
        AL_0202_LIFT_PIN_UP_OVERTIME_ERROR,
        AL_0203_LIFT_PIN_DOWN_OVERTIME_ERROR,
        AL_0204_LIFT_PIN_1_UP_ERROR,
        AL_0205_LIFT_PIN_2_UP_ERROR,
        AL_0206_,
        AL_0207_,
        AL_0208_LIFT_PIN_1_DOWN_ERROR,
        AL_0209_LIFT_PIN_2_DOWN_ERROR,
        AL_0210_,
        AL_0211_,
        AL_0212_,
        AL_0213_,
        AL_0214_,
        AL_0215_,
        AL_0216_,
        AL_0217_,
        AL_0218_,
        AL_0219_,
        AL_0220_ALL_CENTERING_IS_NOT_FORWARD_STATE_ERROR,
        AL_0221_ALL_CENTERING_IS_NOT_BACKWARD_STATE_ERROR,
        AL_0222_CENTERING_STEP_ERROR,
        AL_0223_,
        AL_0224_STAND_CENTERING1_FWD_ERROR,
        AL_0225_STAND_CENTERING2_FWD_ERROR,
        AL_0226_STAND_CENTERING1_BWD_ERROR,
        AL_0227_STAND_CENTERING2_BWD_ERROR,
        AL_0228_,
        AL_0229_,
        AL_0230_,
        AL_0231_,
        AL_0232_,
        AL_0233_,
        AL_0234_,
        AL_0235_,
        AL_0236_,
        AL_0237_,
        AL_0238_,
        AL_0239_,
        AL_0240_,
        AL_0241_,
        AL_0242_,
        AL_0243_,
        AL_0244_,
        AL_0245_,
        AL_0246_,
        AL_0247_,
        AL_0248_,
        AL_0249_,
        AL_0250_,
        AL_0251_,
        AL_0252_,
        AL_0253_,
        AL_0254_,
        AL_0255_,
        AL_0256_,
        AL_0257_,
        AL_0258_,
        AL_0259_,
        AL_0260_,
        AL_0261_,
        AL_0262_,
        AL_0263_,
        AL_0264_,
        AL_0265_,
        AL_0266_,
        AL_0267_,
        AL_0268_,
        AL_0269_,
        AL_0270_SERVO_MC_POWER_OFF_1,
        AL_0271_,
        AL_0272_SAFETY_PLC_ERROR,
        AL_0273_CP_BOX_FAN_OFF_1,
        AL_0274_CP_BOX_FAN_OFF_2,
        AL_0275_CP_BOX_FAN_OFF_3,
        AL_0276_CP_BOX_FAN_OFF_4,
        AL_0277_CP_BOX_DOOR_OPEN,
        AL_0278_,
        AL_0279_,
        AL_0280_,
        AL_0281_KEY_IS_CHANGED_ON_AUTO_RUN,
        AL_0282_DOOR_OPEN_ON_AUTO_RUN,
        AL_0283_,
        AL_0284_,
        AL_0285_,
        AL_0286_CP_BOX_TEMPERATURE_HIGH_ERROR,
        AL_0287_PC_RACK1_TEMPERATURE_HIGH_ERROR,
        AL_0288_,
        AL_0289_AUTO_STEP_OVERTIME,
        AL_0290_ROBOT_ARM_DETECT_ERROR,
        AL_0291_PC_RACK_FAN_OFF_1,
        AL_0292_PC_RACK_FAN_OFF_2,
        AL_0293_PC_RACK_FAN_OFF_3,
        AL_0294_PC_RACK_FAN_OFF_4,
        AL_0295_PC_RACK_FAN_OFF_5,
        AL_0296_PC_RACK_FAN_OFF_6,
        AL_0297_PC_RACK_FAN_OFF_7,
        AL_0298_PC_RACK_FAN_OFF_8,
        AL_0299_,
        AL_0300_INSP_PC_ALIVE_ERROR,
        AL_0301_,
        AL_0302_,
        AL_0303_INSPECTION_ALARM_FTP_SERVER_NOT_CONNECTED_ALARM,
        AL_0304_INSPECTION_ALARM_TTTM_LIGHT_VALUE_ALARM,
        AL_0305_INSPECTION_ALARM_TTTM_FOCUS_VALUE_ALARM,
        AL_0306_INSPECTION_ALARM_TTTM_SIMILARITY_VALUE_ALARM,
        AL_0307_INSPECTION_ALARM_TTTM_FAIL_ALARM,
        AL_0308_INSPECTION_ALARM_SIGNAL_WAFER_NG_WARNING_ALARM,
        AL_0309_,
        AL_0310_,
        AL_0311_INSPECTION_ALARM_SIGNAL_WAFER_ALIGN_FAIL,
        AL_0312_INSPECTION_ALARM_SIGNAL_SERVER_OVERFLOW,
        AL_0313_INSPECTION_ALARM_SIGNAL_INSPECT_OVERFLOW,
        AL_0314_INSPECTION_ALARM_SIGNAL_RECIPE_READ_FAIL,
        AL_0315_INSPECTION_ALARM_SIGNAL_RECIPE_ROI_ERROR,
        AL_0316_INSPECTION_ALARM_SIGNAL_FTP_UPLOAD_FAIL,
        AL_0317_INSPECTION_ALARM_SIGNAL_GRAB_FAIL,
        AL_0318_INSPECTION_ALARM_SIGNAL_NO_RECIPE,
        AL_0319_,
        AL_0320_INSPECTION_ALARM_SIGNAL_LIGHT_ERROR,
        AL_0321_INSPECTION_ALARM_SIGNAL_MODULE_PC,
        AL_0322_INSPECTION_ALARM_SIGNAL_NO_MASTER_IMAGE_ALARM,
        AL_0323_INSPECTION_ALARM_SIGNAL_REVIEW_SCHEDULE_FAIL,
        AL_0324_INSPECTION_ALARM_SIGNAL_REVIEW_SEQUENCE_FAIL,
        AL_0325_INAPECTION_ALARM_SIGNAL_REVIEW_PROCESS_FAIL,
        AL_0326_INSPECTION_ALARM_SIGNAL_DIE_ALIGN_WARNING,
        AL_0327_INSPECTION_ALARM_SIGNAL_DIE_ALIGN_FAIL,
        AL_0328_INSPECTION_UNLOADING_COMPLETE_TIMEOVER_ERROR,
        AL_0329_,
        AL_0330_INSP_PC_REQUEST_STEP_ERROR,
        AL_0331_INSP_ALIGN_TIMEOVER,
        AL_0332_INSP_SCAN_TIMEOVER,
        AL_0333_REVIEW_TIMEOVER,
        AL_0334_TTTM_TIMEOVER,
        AL_0335_,
        AL_0336_,
        AL_0337_,
        AL_0338_,
        AL_0339_,
        AL_0340_,
        AL_0341_,
        AL_0342_,
        AL_0343_,
        AL_0344_,
        AL_0345_,
        AL_0346_,
        AL_0347_,
        AL_0348_,
        AL_0349_,
        AL_0350_INSP_PC_LOADING_ACK_TIMEOVER,
        AL_0351_INSP_PC_ALIGN_START_ACK_TIMEOVER,
        AL_0352_INSP_PC_INSP_START_ACK_TIMEOVER,
        AL_0353_INSP_PC_REVIEW_START_ACK_TIMEOVER,
        AL_0354_,
        AL_0355_,
        AL_0356_INSP_PC_UNLOADING_ACK_TIMEOVER,
        AL_0357_,
        AL_0358_INSP_PC_TTTM_START_ACK_TIMEOVER,
        AL_0359_,
        AL_0360_INSP_PC_LOADING_COMPLETE_HS_TIMEOVER,
        AL_0361_INSP_PC_ALIGN_COMPLETE_HS_TIMEOVER,
        AL_0362_INSP_PC_INSP_COMPLETE_HS_TIMEOVER,
        AL_0363_INSP_PC_REVIEW_COMPLETE_HS_TIMEOVER,
        AL_0364_INSP_PC_VCR_READ_HS_TIMEOVER,
        AL_0365_INSP_PC_RESULT_FILE_HS_COMPLETE,
        AL_0366_INSP_PC_UNLOADING_COMPLETE_HS_TIMEOVER,
        AL_0367_,
        AL_0368_,
        AL_0369_INSP_PC_LOADING_HS_TIMEOVER,
        AL_0370_INSP_PC_ALIGN_START_HS_TIMEOVER,
        AL_0371_INSP_PC_INSP_START_HS_TIMEOVER,
        AL_0372_INSP_PC_REVIEW_START_HS_TIME_OVER,
        AL_0373_,
        AL_0374_,
        AL_0375_INSP_PC_UNLOADING_HS_TIMEOVER,
        AL_0376_INSP_PC_REQ_TIME_CHANGE_HS_TIMEOVER,
        AL_0377_INSP_PC_TTTM_START_HS_TIMEOVER,
        AL_0378_INSP_PC_TTTM_COMPLETE_HS_TIMEOVER,
        AL_0379_,
        AL_0380_INSP_PC_LOADING_ACK_IS_NOT_OFF_ERROR,
        AL_0381_INSP_PC_ALIGN_START_ACK_IS_NOT_OFF_ERROR,
        AL_0382_INSP_PC_SCAN_START_ACK_IS_NOT_OFF_ERROR,
        AL_0383_INSP_PC_REVIEW_START_ACK_IS_NOT_OFF_ERROR,
        AL_0384_INSP_PC_UNLOADING_ACK_IS_NOT_OFF_ERROR,
        AL_0385_,
        AL_0386_INSP_PC_UNLOADING_ACK_IS_NOT_OFF_ERROR,
        AL_0387_,
        AL_0388_INSP_PC_LOADING_COMPLETE_IS_NOT_OFF_ERROR,
        AL_0389_INSP_PC_ALIGN_COMPLETE_IS_NOT_OFF_ERROR,
        AL_0390_INSP_PC_INSP_START_COMPLETE_IS_NOT_OFF_ERROR,
        AL_0391_INSP_PC_REVIEW_COMPLETE_IS_NOT_OFF_ERROR,
        AL_0392_INSP_PC_RESULT_FILE_IS_NOT_OFF_ERROR,
        AL_0393_INSP_PC_UNLOADING_COMPLETE_IS_NOT_OFF_ERROR,
        AL_0394_,
        AL_0395_,
        AL_0396_INSP_PC_EVT_TTTM_START_ACK_IS_NOT_OFF_ERROR,
        AL_0397_INSP_PC_TTTM_COMPLETE_IS_NOT_OFF_ERROR,
        AL_0398_,
        AL_0399_,
        AL_0400_,
        AL_0401_,
        AL_0402_,
        AL_0403_,
        AL_0404_,
        AL_0405_,
        AL_0406_,
        AL_0407_,
        AL_0408_,
        AL_0409_,
        AL_0410_,
        AL_0411_,
        AL_0412_,
        AL_0413_,
        AL_0414_,
        AL_0415_,
        AL_0416_,
        AL_0417_,
        AL_0418_,
        AL_0419_,
        AL_0420_,
        AL_0421_,
        AL_0422_,
        AL_0423_,
        AL_0424_,
        AL_0425_,
        AL_0426_,
        AL_0427_,
        AL_0428_,
        AL_0429_,
        AL_0430_,
        AL_0431_,
        AL_0432_,
        AL_0433_,
        AL_0434_,
        AL_0435_,
        AL_0436_,
        AL_0437_,
        AL_0438_,
        AL_0439_,
        AL_0440_,
        AL_0441_,
        AL_0442_,
        AL_0443_,
        AL_0444_,
        AL_0445_,
        AL_0446_,
        AL_0447_,
        AL_0448_,
        AL_0449_,
        AL_0450_,
        AL_0451_,
        AL_0452_,
        AL_0453_,
        AL_0454_,
        AL_0455_,
        AL_0456_,
        AL_0457_,
        AL_0458_,
        AL_0459_,
        AL_0460_,
        AL_0461_,
        AL_0462_,
        AL_0463_,
        AL_0464_,
        AL_0465_,
        AL_0466_,
        AL_0467_,
        AL_0468_,
        AL_0469_,
        AL_0470_PMAC_HEAVY_ERROR,
        AL_0471_PMAC_EVENT_SIGNAL_TIMEOVER,
        AL_0472_PMAC_ALIVE_ERROR,
        AL_0473_PMAC_POSITION_SETTING_ERROR,
        AL_0474_PMAC_SPEED_SETTING_ERROR,
        AL_0475_PMAC_ACCELERATION_SETTING_ERROR,
        AL_0476_PMAC_CONNECTION_ERROR,
        AL_0477_PMAC_IMMEDIATE_STOP_ERROR,
        AL_0478_PMAC_SETTING_TIMEOUT_ERROR,
        AL_0479_PMAC_MOTOR_INTERLOCK_ERROR,
        AL_0480_,
        AL_0481_PTP_FAIL_RETRY,
        AL_0482_,
        AL_0483_,
        AL_0484_,
        AL_0485_,
        AL_0486_,
        AL_0487_,
        AL_0488_,
        AL_0489_,
        AL_0490_STAGE_X_PLUS_LIMIT_ERROR,
        AL_0491_STAGE_X_MINUS_LIMIT_ERROR,
        AL_0492_STAGE_X_MOTOR_SERVO_ON_ERROR,
        AL_0493_STAGE_X_FATAL_FOLLOWING_ERROR,
        AL_0494_STAGE_X_AMP_FAULT_ERROR,
        AL_0495_STAGE_X_I2T_AMP_FAULT_ERROR,
        AL_0496_STAGE_X_MOVE_OVERTIME_ERROR,
        AL_0497_STAGE_X_LOADING_POSITION_ERROR,
        AL_0498_STAGE_X_PTP_WRITE_TIMEOUT_ERROR,
        AL_0499_STAGE_X_HOME_CMD_ACK_ERROR,
        AL_0500_STAGE_X_PTP_CMD_ACK_ERROR,
        AL_0501_STAGE_X_ABNORMAL_LOADING_POS,
        AL_0502_,
        AL_0503_,
        AL_0504_,
        AL_0505_STAGE_Y_PLUS_LIMIT_ERROR,
        AL_0506_STAGE_Y_MINUS_LIMIT_ERROR,
        AL_0507_STAGE_Y_MOTOR_SERVO_ON_ERROR,
        AL_0508_STAGE_Y_FATAL_FOLLOWING_ERROR,
        AL_0509_STAGE_Y_AMP_FAULT_ERROR,
        AL_0510_STAGE_Y_I2T_AMP_FAULT_ERROR,
        AL_0511_STAGE_Y_MOVE_OVERTIME_ERROR,
        AL_0512_STAGE_Y_PTP_WRITE_TIMEOUT_ERROR,
        AL_0513_STAGE_Y_HOME_CMD_ACK_ERROR,
        AL_0514_STAGE_Y_PTP_CMD_ACK_ERROR,
        AL_0515_STAGE_Y_LOADING_POSITION_ERROR,
        AL_0516_STAGE_Y_ABNORMAL_LOADING_POS,
        AL_0517_,
        AL_0518_,
        AL_0519_,
        AL_0520_THETA_PLUS_LIMIT_ERROR,
        AL_0521_THETA_MINUS_LIMIT_ERROR,
        AL_0522_THETA_MOTOR_SERVO_ON_ERROR,
        AL_0523_THETA_FATAL_FOLLOWING_ERROR,
        AL_0524_THETA_AMP_FAULT_ERROR,
        AL_0525_THETA_I2T_AMP_FAULT_ERROR,
        AL_0526_THETA_MOVE_OVERTIME_ERROR,
        AL_0527_THETA_PTP_WRITE_TIMEOUT_ERROR,
        AL_0528_THETA_HOME_CMD_ACK_ERROR,
        AL_0529_THETA_PTP_CMD_ACK_ERROR,
        AL_0530_THETA_LOADING_POSITION_ERROR,
        AL_0531_THETA_ABNORMAL_LOADING_POS,
        AL_0532_,
        AL_0533_ALIGNER_X_MOVE_OVERTIME_ERROR,
        AL_0534_ALIGNER_X_SERVO_ON_ERROR,
        AL_0535_ALIGNER_Y_MOVE_OVERTIME_ERROR,
        AL_0536_ALIGNER_Y_SERVO_ON_ERROR,
        AL_0537_ALIGNER_T_MOVE_OVERTIME_ERROR,
        AL_0538_ALIGNER_T_SERVO_ON_ERROR,
        AL_0539_,
        AL_0540_AVI_RETRY_INPUT_WAIT,
        AL_0541_,
        AL_0542_,
        AL_0543_,
        AL_0544_,
        AL_0545_,
        AL_0546_,
        AL_0547_,
        AL_0548_,
        AL_0549_,
        //CIM
        AL_0550_LOT_START_CONFIRM_NG,
        AL_0551_WAFER_START_CONFIRM_NG,
        AL_0552_WAFER_MAP_DOWNLOAD_FAIL,
        AL_0553_,
        AL_0554_HOST_MAPPING_DATA_NOT_MATCH,
        AL_0555_HOST_CST_LOAD_CONFIRM_NG,
        AL_0556_PORT_RECIPE_CHANGE_FAIL,
        AL_0557_WAFER_MAP_DOWNLOAD_RETRY,
        AL_0558_,
        AL_0559_BEFORE_WAFER_LOAD_ID_HOST_DATA_NOT_MATCH,
        AL_0560_,
        AL_0561_CST_MAP_WAFER_LIST_DATA_EMPTY,
        AL_0562_CST_CONFIRM_OK_BUT_ID_NOT_MATCH_RFID_READ,
        AL_0563_NO_RECIPE_FROM_PP_SELECT_RECIPE_RECV,
        AL_0564_WAFER_START_REPORT_ID_IS_NOT_MATCH_WITH_HOST_RECV,
        AL_0565_,
        AL_0566_,
        AL_0567_,
        AL_0568_,
        AL_0569_,
        AL_0570_CASSETTE_LOAD_CONFIRM_TIMEOVER,
        AL_0571_PP_SELECT_TIMEOVER,
        AL_0572_CASSETTE_LOAD_START_CMD_TIMEOVER,
        AL_0573_CASSETTE_LOT_START_CONFIRM_TIMEOVER,
        AL_0574_WAFER_LOAD_CONFIRM_TIMEOVER,
        AL_0575_,
        AL_0576_,
        AL_0577_,
        AL_0578_,
        AL_0579_,
        AL_0580_,
        AL_0581_,
        AL_0582_,
        AL_0583_,
        AL_0584_,
        AL_0585_,
        AL_0586_,
        AL_0587_,
        AL_0588_,
        AL_0589_,
        AL_0590_,
        AL_0591_,
        AL_0592_,
        AL_0593_,
        AL_0594_,
        AL_0595_,
        AL_0596_,
        AL_0597_,
        AL_0598_,
        AL_0599_,

        //EFEM 관련
        AL_0600_PIO_RECV_T1_TIME_OUT,
        AL_0601_PIO_RECV_T2_TIME_OUT,
        AL_0602_PIO_RECV_T3_TIME_OUT,
        AL_0603_PIO_RECV_T4_TIME_OUT,
        AL_0604_PIO_RECV_T5_TIME_OUT,
        AL_0605_PIO_RECV_T6_TIME_OUT,
        AL_0606_PIO_SEND_T1_TIME_OUT,
        AL_0607_PIO_SEND_T2_TIME_OUT,
        AL_0608_PIO_SEND_T3_TIME_OUT,
        AL_0609_PIO_SEND_T4_TIME_OUT,
        AL_0610_PIO_SEND_T5_TIME_OUT,
        AL_0611_PIO_SEND_T6_TIME_OUT,
        AL_0612_PIO_EXCH_T1_TIME_OUT,
        AL_0613_PIO_EXCH_T2_TIME_OUT,
        AL_0614_PIO_EXCH_T3_TIME_OUT,
        AL_0615_PIO_EXCH_T4_TIME_OUT,
        AL_0616_PIO_EXCH_T5_TIME_OUT,
        AL_0617_PIO_EXCH_T6_TIME_OUT,
        AL_0618_OHT_INTERFACE_TIMEOVER,
        AL_0619_,
        AL_0620_LOADING_PIO_AVI_WAFER_NO_DETECT,
        AL_0621_UNLD_PIO_AVI_WAFER_DETECT_ABNORMAL,
        AL_0622_EFEM_SEQ_ABNORMAL,
        AL_0623_,
        AL_0624_,
        AL_0625_,
        AL_0626_,
        AL_0627_,
        AL_0628_,
        AL_0629_,
        AL_0630_BCR_READ_ERROR,
        AL_0631_OCR_READ_ERROR,
        AL_0632_LOADPORT1_RF_READ_ERROR,
        AL_0633_LOADPORT2_RF_READ_ERROR,
        AL_0634_LOADPORT1_DOOR_CLOSE,
        AL_0635_LOADPORT2_DOOR_CLOSE,
        AL_0636_,
        AL_0637_,
        AL_0638_,
        AL_0639_,
        AL_0640_WAFER_ID_IS_NOT_MATCH_WITH_RECIPE,
        AL_0641_RECIPE_MODEL_DATA_IS_ABNORMAL,
        AL_0642_,
        AL_0643_,
        AL_0644_,
        AL_0645_,
        AL_0646_,
        AL_0647_,
        AL_0648_,
        AL_0649_,
        AL_0650_EFEM_LPM1_CST_POS_ERROR,
        AL_0651_EFEM_LPM2_CST_POS_ERROR,
        AL_0652_,
        AL_0653_LPM1_SLOT_ABNORMAL,
        AL_0654_LPM2_SLOT_ABNORMAL,
        // 물류이상
        AL_0655_EFEM_LPM1_NO_DATA,
        AL_0656_EFEM_LPM2_NO_DATA,
        AL_0657_LPM1_ID_NOT_MATCH_ROBOT_UPPER,
        AL_0658_LPM2_ID_NOT_MATCH_ROBOT_UPPER,
        AL_0659_NO_CST_INFO,
        AL_0660_NO_WAFER_INFO,
        AL_0661_AVI_WAFER_CST_ID_NOT_MATCH,
        AL_0662_,
        AL_0663_LPM1_WAFER_STATUS_ABNORMAL,
        AL_0664_LPM2_WAFER_STATUS_ABNORMAL,
        AL_0665_LPM1_WAFER_DUPLICATION_ERROR,
        AL_0666_LPM2_WAFER_DUPLICATION_ERROR,
        AL_0667_LPM1_READY_WAIT_OVERTIME,
        AL_0668_LPM2_READY_WAIT_OVERTIME,
        AL_0669_TARGETPORT_ABNORMAL,
        AL_0670_EFEM_MODE_KEY_IS_CHANGED_ON_AUTO_RUN,
        AL_0671_EFEM_DOOR_OPEN,
        AL_0672_,
        AL_0673_PREALIGN_X_LIMIT_OVER,
        AL_0674_PREALIGN_Y_LIMIT_OVER,
        AL_0675_PREALIGN_T_LIMIT_OVER,
        AL_0676_,
        AL_0677_LPM1_NO_WAFER_IN_CST,
        AL_0678_LPM2_NO_WAFER_IN_CST,
        AL_0679_,
        //OverTime 680~769
        AL_0680_ROBOT_STATUS_COMMAND_TIMEOVER,
        AL_0681_ROBOT_INIT_COMMAND_TIMEOVER,
        AL_0682_ROBOT_RESET_COMMAND_TIMEOVER,
        AL_0683_ROBOT_RESUME_COMMAND_TIMEOVER,
        AL_0684_ROBOT_PAUSE_COMMAND_TIMEOVER,
        AL_0685_ROBOT_STOP_COMMAND_TIMEOVER,
        AL_0686_ROBOT_WAIT_COMMAND_TIMEOVER,
        AL_0687_ROBOT_TRANS_COMMAND_TIMEOVER,
        AL_0688_,
        AL_0689_,
        AL_0690_LPM1_STATUS_COMMAND_TIMEOVER,
        AL_0691_LPM1_INIT_COMMAND_TIMEOVER,
        AL_0692_LPM1_RESET_COMMAND_TIMEOVER,
        AL_0693_LPM1_STOP_COMMAND_TIMEOVER,
        AL_0694_LPM1_OPEN_COMMAND_TIMEOVER,
        AL_0695_LPM1_CLOSE_COMMAND_TIMEOVER,
        AL_0696_LPM1_MAPPING_COMMAND_TIMEOVER,
        AL_0697_LPM1_LPLED_COMMAND_TIMEOVER,
        AL_0698_,
        AL_0699_,
        AL_0700_LPM2_STATUS_COMMAND_TIMEOVER,
        AL_0701_LPM2_INIT_COMMAND_TIMEOVER,
        AL_0702_LPM2_RESET_COMMAND_TIMEOVER,
        AL_0703_LPM2_STOP_COMMAND_TIMEOVER,
        AL_0704_LPM2_OPEN_COMMAND_TIMEOVER,
        AL_0705_LPM2_CLOSE_COMMAND_TIMEOVER,
        AL_0706_LPM2_MAPPING_COMMAND_TIMEOVER,
        AL_0707_LPM2_LPLED_COMMAND_TIMEOVER,
        AL_0708_,
        AL_0709_,
        AL_0710_ALIGNER_STATUS_COMMAND_TIMEOVER,
        AL_0711_ALIGNER_INIT_COMMAND_TIMEOVER,
        AL_0712_ALIGNER_RESET_COMMAND_TIMEOVER,
        AL_0713_ALIGNER_ALIGN_COMMAND_TIMEOVER,
        AL_0714_ALIGNER_RECV_READY_COMMAND_TIMEOVER,
        AL_0715_ALIGNER_ROTATION_COMMAND_TIMEOVER,
        AL_0716_ALIGNER_SEND_READY_COMMAND_TIMEOVER,

        AL_0717_ETC_STATUS_COMMAND_TIMEOVER,
        AL_0718_ETC_SIGLM_COMMAND_TIMEOVER,
        AL_0719_ETC_RESET_COMMAND_TIMEOVER,
        AL_0720_ETC_CHMDA_COMMAND_TIMEOVER,
        AL_0721_ETC_CHMDM_COMMAND_TIMEOVER,
        AL_0722_HSMS_PC_EVENT_SIGNAL_TIMEOVER,
        AL_0723_HSMS_PC_COMMAND_SIGNAL_TIMEOVER,
        AL_0724_HSMS_PC_ALIVE_ERROR,
        AL_0725_PIO_RECV_COMPLET_NO_GLASS,
        AL_0726_PIO_GLASS_DETECT_ERROR,
        AL_0727_LOT_FLAG_EXCEPT_ERROR,
        AL_0728_SYNC_SERVER_RUN_ERROR,
        AL_0729_WAFER_INFO_ABNORMAL,
        AL_0730_DATABASE_INITIAL_FAIL,

        AL_0731_ROBOT_STATUS_COMPLETE_TIMEOVER,
        AL_0732_ROBOT_INIT_COMPLETE_TIMEOVER,
        AL_0733_ROBOT_RESET_COMPLETE_TIMEOVER,
        AL_0734_ROBOT_RESUME_COMPLETE_TIMEOVER,
        AL_0735_ROBOT_PAUSE_COMPLETE_TIMEOVER,
        AL_0736_ROBOT_STOP_COMPLETE_TIMEOVER,
        AL_0737_ROBOT_WAIT_COMPLETE_TIMEOVER,
        AL_0738_ROBOT_TRANS_COMPLETE_TIMEOVER,
        AL_0739_,
        AL_0740_,
        AL_0741_LPM1_STATUS_COMPLETE_TIMEOVER,
        AL_0742_LPM1_INIT_COMPLETE_TIMEOVER,
        AL_0743_LPM1_RESET_COMPLETE_TIMEOVER,
        AL_0744_LPM1_STOP_COMPLETE_TIMEOVER,
        AL_0745_LPM1_OPEN_COMPLETE_TIMEOVER,
        AL_0746_LPM1_CLOSE_COMPLETE_TIMEOVER,
        AL_0747_LPM1_MAPPING_COMPLETE_TIMEOVER,
        AL_0748_LPM1_LPLED_COMPLETE_TIMEOVER,
        AL_0749_,
        AL_0750_LPM2_STATUS_COMPLETE_TIMEOVER,
        AL_0751_LPM2_INIT_COMPLETE_TIMEOVER,
        AL_0752_LPM2_RESET_COMPLETE_TIMEOVER,
        AL_0753_LPM2_STOP_COMPLETE_TIMEOVER,
        AL_0754_LPM2_OPEN_COMPLETE_TIMEOVER,
        AL_0755_LPM2_CLOSE_COMPLETE_TIMEOVER,
        AL_0756_LPM2_MAPPING_COMPLETE_TIMEOVER,
        AL_0757_LPM2_LPLED_COMPLETE_TIMEOVER,
        AL_0758_,
        AL_0759_,
        AL_0760_ALIGNER_STATUS_COMPLETE_TIMEOVER,
        AL_0761_ALIGNER_INIT_COMPLETE_TIMEOVER,
        AL_0762_ALIGNER_RESET_COMPLETE_TIMEOVER,
        AL_0763_ALIGNER_ALIGN_COMPLETE_TIMEOVER,
        AL_0764_ALIGNER_RECV_READY_COMPLETE_TIMEOVER,
        AL_0765_ALIGNER_ROTATION_COMPLETE_TIMEOVER,
        AL_0766_ALIGNER_SEND_READY_COMPLETE_TIMEOVER,
        AL_0767_,
        AL_0768_ETC_STATUS_COMPLETE_TIMEOVER,
        AL_0769_ETC_SIGLM_COMPLETE_TIMEOVER,
        AL_0770_ETC_RESET_COMPLETE_TIMEOVER,
        AL_0771_ETC_CHMDA_COMPLETE_TIMEOVER,
        AL_0772_ETC_CHMDM_COMPLETE_TIMEOVER,
        AL_0773_,
        AL_0774_,
        AL_0775_,
        AL_0776_,
        AL_0777_,
        AL_0778_,
        AL_0779_,
        // 780~799 EFEM SYSTEM ERROR
        AL_0780_EFEM_SERIAL_PORT_FAIL_TO_OPEN,
        AL_0781_EFEM_DEVICE_NET_FAIL_TO_OPEN,
        AL_0782_EFEM_ROBOT_NEED_INITIALIZE,
        AL_0783_EFEM_PORT_INCORRECT,
        AL_0784_EFEM_COMMAND_INCORRECT,
        AL_0785_EFEM_PC_ALIVE_ERROR,
        AL_0786_,
        AL_0787_,
        AL_0788_,
        AL_0789_,
        AL_0790_EFEM_SYSTEM_STATUS_FAIL,
        AL_0791_EFEM_SYSTEM_SIGLM_FAIL,
        AL_0792_EFEM_SYSTEM_RESET_FAIL,
        AL_0793_EFEM_SYSTEM_CHMDA_FAIL,
        AL_0794_EFEM_SYSTEM_CHMDM_FAIL,
        AL_0795_,
        AL_0796_,
        AL_0797_,
        AL_0798_,
        AL_0799_,
        // 800~849 ROBOT        
        AL_0800_ROBOT_PICK_WAFER_MISSED,
        AL_0801_ROBOT_PLACE_WAFER_MISSED,
        AL_0802_WAFER_SKIPPED_BECAUSE_ROBOT_CANNOT_VACUUM_ON,
        AL_0803_ROBOT_FAILED_WAFER_VACUUM_ON,
        AL_0804_VACUUM_ON_FAIL_ALL_WAFER,
        AL_0805_,
        AL_0806_,
        AL_0807_,
        AL_0808_,
        AL_0809_,
        AL_0810_EFEM_ROBOT_STATUS_CMD_FAIL,
        AL_0811_EFEM_ROBOT_INITIAL_CMD_FAIL,
        AL_0812_EFEM_ROBOT_RESET_CMD_FAIL,
        AL_0813_EFEM_ROBOT_PAUSE_CMD_FAIL,
        AL_0814_EFEM_ROBOT_RESUME_CMD_FAIL,
        AL_0815_EFEM_ROBOT_STOP_CMD_FAIL,
        AL_0816_EFEM_ROBOT_WAITR_CMD_FAIL,
        AL_0817_EFEM_ROBOT_TRANS_CMD_FAIL,
        AL_0818_,
        AL_0819_,
        AL_0820_,
        AL_0821_,
        AL_0822_,
        AL_0823_,
        AL_0824_,
        AL_0825_,
        AL_0826_,
        AL_0827_,
        AL_0828_,
        AL_0829_,
        AL_0830_,
        AL_0831_,
        AL_0832_,
        AL_0833_,
        AL_0834_,
        AL_0835_,
        AL_0836_,
        AL_0837_,
        AL_0838_,
        AL_0839_,
        AL_0840_,
        AL_0841_,
        AL_0842_,
        AL_0843_,
        AL_0844_,
        AL_0845_,
        AL_0846_,
        AL_0847_,
        AL_0848_,
        AL_0849_,
        // 850~899 LPM        
        AL_0850_LPM_NEED_INITIALIZE,
        AL_0851_,
        AL_0852_,
        AL_0853_,
        AL_0854_,
        AL_0855_LPM_CANT_OPEN, // 작업자가 Wafer를 직접 제거해야함, 파손위험
        AL_0856_,
        AL_0857_,
        AL_0858_,
        AL_0859_,
        AL_0860_LPM1_USER_INPUT_MAP_ABRNOMAL,
        AL_0861_LPM2_USER_INPUT_MAP_ABRNOMAL,
        AL_0862_,
        AL_0863_,
        AL_0864_,
        AL_0865_,
        AL_0866_,
        AL_0867_,
        AL_0868_,
        AL_0869_,
        AL_0870_LPM_LIGHT_CURTAIN_DETECTED,
        AL_0871_LPM1_CASSETTE_REMOVED,
        AL_0872_LPM2_CASSETTE_REMOVED,
        AL_0873_LPM1_OHT_UNLOAD_COMPLETE_BUT_CST_EXIST,
        AL_0874_LPM2_OHT_UNLOAD_COMPLETE_BUT_CST_EXIST,
        AL_0875_,
        AL_0876_,
        AL_0877_,
        AL_0878_,
        AL_0879_,
        AL_0880_EFEM_LPM1_STATUS_CMD_FAIL,
        AL_0881_EFEM_LPM1_INITIAL_CMD_FAIL,
        AL_0882_EFEM_LPM1_STOP_CMD_FAIL,
        AL_0883_EFEM_LPM1_RESET_CMD_FAIL,
        AL_0884_EFEM_LPM1_OPEN_CMD_FAIL,
        AL_0885_EFEM_LPM1_CLOSE_CMD_FAIL,
        AL_0886_EFEM_LPM1_MAPPING_CMD_FAIL,
        AL_0887_EFEM_LPM1_LPLED_CMD_FAIL,
        AL_0888_,
        AL_0889_,
        AL_0890_EFEM_LPM2_STATUS_CMD_FAIL,
        AL_0891_EFEM_LPM2_INITIAL_CMD_FAIL,
        AL_0892_EFEM_LPM2_STOP_CMD_FAIL,
        AL_0893_EFEM_LPM2_RESET_CMD_FAIL,
        AL_0894_EFEM_LPM2_OPEN_CMD_FAIL,
        AL_0895_EFEM_LPM2_CLOSE_CMD_FAIL,
        AL_0896_EFEM_LPM2_MAPPING_CMD_FAIL,
        AL_0897_EFEM_LPM2_LPLED_CMD_FAIL,
        AL_0898_,
        AL_0899_,
        // 900~949 ALIGNER
        AL_0900_PRE_ALIGNER_NEED_INITIALIZE,
        AL_0901_PRE_ALIGNER_NO_DEFAULT_RECIPE,
        AL_0902_PRE_ALIGNER_IMAGE_PROCESS_FAIL,
        AL_0903_PRE_ALIGNER_IMAGE_GRAB_FAIL,
        AL_0904_PRE_ALIGNER_ALIGN_FAIL,
        AL_0905_,
        AL_0906_,
        AL_0907_,
        AL_0908_,
        AL_0909_,
        AL_0910_ALIGNER_MOTION_NOT_CONNECTED,
        AL_0911_ALIGNER_LIGHT_NOT_CONNECTED,
        AL_0912_ALIGNER_DIO_NOT_CONNECTED,
        AL_0913_ALIGNER_CAMERA_NOT_CONNECTED,
        AL_0914_ALIGNER_LIGHT_CONNECTION_ERROR,
        AL_0915_,
        AL_0916_,
        AL_0917_,
        AL_0918_,
        AL_0919_,
        AL_0920_ALIGNER_DATA_READ_FAIL,
        AL_0921_ALIGN_RETRY,
        AL_0922_ALIGN_RETRY_TIMES_OVER,
        AL_0923_,
        AL_0924_,
        AL_0925_,
        AL_0926_,
        AL_0927_,
        AL_0928_,
        AL_0929_,
        AL_0930_ALIGNER_STATUS_CMD_FAIL,
        AL_0931_ALIGNER_ALIGN_CMD_FAIL,
        AL_0932_ALIGNER_INITAIL_CMD_FAIL,
        AL_0933_ALIGNER_RESET_CMD_FAIL,
        AL_0934_ALIGNER_PARDY_CMD_FAIL,
        AL_0935_ALIGNER_PATRR_CMD_FAIL,
        AL_0936_ALIGNER_PASRD_CMD_FAIL,
        AL_0937_,
        AL_0938_,
        AL_0939_,
        AL_0940_,
        AL_0941_,
        AL_0942_,
        AL_0943_,
        AL_0944_,
        AL_0945_,
        AL_0946_UI_EXCEPTION,
        AL_0947_EXCEPTION_OCCURE,
    }

    #endregion
    public class AlarmMgr
    {
        // 필드 1
        public static string PATH_SETTING = Path.Combine(GG.StartupPath, "Setting", "Alarm.ini");

        private System.Timers.Timer _logTimer;
        public List<ListView> LstlogAlarms = new List<ListView>();
        public List<ListView> LstlogAlarmHistory = new List<ListView>();
        public DataGridView LstvSettingAlarm = null;

        public SortedList<EM_AL_LST, Alarm> HappenAlarms = new SortedList<EM_AL_LST, Alarm>();
        private Queue<Alarm> _queShowAlarm = new Queue<Alarm>();
        public int ShownAlarmCount { get { return _queShowAlarm.Count; } }
        private Queue<ListViewItem> _quePasteItem = new Queue<ListViewItem>();
        private int timerInterval = 500;
        private ImageList _imgList = new ImageList();

        //싱클던 
        private static AlarmMgr _selfInstance = null;
        public static AlarmMgr Instance
        {
            get
            {
                if (_selfInstance == null)
                    _selfInstance = new AlarmMgr();
                return _selfInstance;
            }
        }

        //private System.Threading.Monitor _montor;
        private AlarmMgr()
        {
            InitializeAlarmList();

            _logTimer = new System.Timers.Timer(timerInterval);
            _logTimer.Elapsed += delegate (object sender, ElapsedEventArgs e)
            {
                while (_queShowAlarm.Count > 0)
                {
                    try
                    {
                        if (System.Threading.Monitor.TryEnter(_queShowAlarm))
                        {
                            Alarm al = _queShowAlarm.Dequeue();
                            foreach (ListView lstv in LstlogAlarms)
                            {
                                if (lstv != null)
                                {
                                    string desc = string.IsNullOrEmpty(al.Desc) ? al.ID.ToString() : al.Desc;
                                    ListViewItem item = new ListViewItem(new string[] {
                                                        "",
                                                        al.HappenTime.ToString("MM-dd HH:mm:ss"),
                                                        ((int)al.ID).ToString(),
                                                        desc });

                                    if (al.State == EM_AL_STATE.Heavy)
                                        item.ForeColor = Color.Red;
                                    else if (al.State == EM_AL_STATE.Warn)
                                        item.ForeColor = Color.DarkOrange;
                                    else if (al.State == EM_AL_STATE.Unused)
                                        item.ForeColor = Color.Gray;

                                    lstv.Items.Insert(0, item);

                                    if (lstv.Items.Count > 200)
                                        PasteToHistoryListView(ref LstlogAlarms, ref LstlogAlarmHistory);

                                }
                            }
                            System.Threading.Monitor.Exit(_queShowAlarm);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                while (_quePasteItem.Count > 0)
                {
                    try
                    {
                        if (System.Threading.Monitor.TryEnter(_quePasteItem))
                        {
                            foreach (ListView lstvHistory in LstlogAlarmHistory)
                            {
                                lstvHistory.Items.Insert(0, _quePasteItem.Dequeue());
                                if (lstvHistory != null)
                                {
                                    if (lstvHistory.Items.Count > 200)
                                        lstvHistory.Items.RemoveAt(200);
                                }
                            }
                            System.Threading.Monitor.Exit(_quePasteItem);
                        }
                        else
                            break;
                    }
                    catch (System.Exception ex)
                    {
                        System.Threading.Monitor.Exit(_quePasteItem);
                        Console.WriteLine(ex.Message);
                        break;
                    }
                }
            };

            _logTimer.Start();
        }
        /// <summary>
        /// startId, endId 입력하면 범위내 알람 확인
        /// endId 미입력하면 startId하나의 알람만 확인
        /// </summary>
        /// <param name="equip"></param>
        /// <param name="startId"></param>
        /// <param name="endId"></param>
        /// <returns></returns>
        public bool IsHappened(Equipment equip, EM_AL_LST startId, EM_AL_LST endId = EM_AL_LST.AL_0000_NONE)
        {
            if (HappenAlarms.ContainsKey(startId) && HappenAlarms.ContainsKey(endId))
            {
                if (endId == EM_AL_LST.AL_0000_NONE)
                    return HappenAlarms[startId].Happen;
                else
                {
                    for (EM_AL_LST id = startId; startId <= endId; id++)
                    {
                        if (HappenAlarms[id].Happen)
                            return true;
                    }
                }
            }
            return false;
        }
        public bool Happen(Equipment equip, EM_AL_LST id)
        {
            if (HappenAlarms.ContainsKey(id))
            {
                if (HappenAlarms[id].Happen == false)
                {
                    HappenAlarms[id].ID = id;
                    HappenAlarms[id].Happen = true;
                    HappenAlarms[id].HappenTime = DateTime.Now;

                    if (HappenAlarms[id].State == EM_AL_STATE.Heavy)
                    {
                        equip.BuzzerK1.OnOff(equip, true);
                        equip.IsBuzzerStopSW = false;

                        _queShowAlarm.Enqueue(new Alarm() { ID = id, Happen = true, State = EM_AL_STATE.Heavy, HappenTime = DateTime.Now, Desc = HappenAlarms[id].Desc });
                        equip.IsPause = true;
                        equip.IsHeavyAlarm = true;
                        equip.Efem.ChangeMode(Step.EmEfemRunMode.Pause);
                        equip.ChangeRunMode(EmEquipRunMode.Manual);

                        equip.CimReportStatus = EmCimReportStatus.Down;

                        Logger.Log.AppendLine(LogLevel.Warning, "중알람 발생 ID = {0}, ALARM CODE = {1} ", id, HappenAlarms[id].Desc);
                        Logger.AlarmLog.AppendLine(LogLevel.NoLog, "중알람 발생 ID = {0}, ALARM CODE = {1} ", id, HappenAlarms[id].Desc);
                        Logger.CSTHistoryLog.AppendLine(LogLevel.Warning, "중알람 발생 ID = {0}, ALARM CODE = {1} ", id, HappenAlarms[id].Desc);
                    }
                    else if (HappenAlarms[id].State == EM_AL_STATE.Warn)
                    {
                        if (equip.IsHeavyAlarm == false)
                        {
                            if (id == EM_AL_LST.AL_0630_BCR_READ_ERROR)
                            {
                                equip.BuzzerK2.OnOff(equip, true);
                            }
                            else
                            {
                                Thread buzzerThread = new Thread(new ThreadStart(AlarmThread));
                                buzzerThread.Start();
                            }
                        }

                        _queShowAlarm.Enqueue(new Alarm() { ID = id, Happen = true, State = EM_AL_STATE.Warn, HappenTime = DateTime.Now, Desc = HappenAlarms[id].Desc });
                        equip.IsLightAlarm = true;

                        Logger.Log.AppendLine(LogLevel.Warning, "경알람 발생 ID = {0}, ALARM CODE = {1} ", id, HappenAlarms[id].Desc);
                        Logger.AlarmLog.AppendLine(LogLevel.NoLog, "경알람 발생 ID = {0}, ALARM CODE = {1} ", id, HappenAlarms[id].Desc);
                        Logger.CSTHistoryLog.AppendLine(LogLevel.Warning, "경알람 발생 ID = {0}, ALARM CODE = {1} ", id, HappenAlarms[id].Desc);
                    }
                    else if (HappenAlarms[id].State == EM_AL_STATE.Unused)
                    {
                        _queShowAlarm.Enqueue(new Alarm() { ID = id, Happen = true, State = EM_AL_STATE.Unused, HappenTime = DateTime.Now, Desc = HappenAlarms[id].Desc });
                        Logger.Log.AppendLine(LogLevel.Warning, "미사용 알람 발생 ID = {0}, ALARM CODE = {1} ", id, HappenAlarms[id].Desc);
                        Logger.AlarmLog.AppendLine(LogLevel.NoLog, "미사용 알람 발생 ID = {0}, ALARM CODE = {1} ", id, HappenAlarms[id].Desc);
                    }
                    else
                    {
                        _queShowAlarm.Enqueue(new Alarm() { ID = EM_AL_LST.AL_0000_NONE, Happen = true, State = EM_AL_STATE.Heavy, HappenTime = DateTime.Now, Desc = "Alarm Code Error" });
                        Logger.Log.AppendLine(LogLevel.Warning, "미지정 알람1 발생 ID = {0}, ALARM CODE = {1} ", EM_AL_LST.AL_0000_NONE, HappenAlarms[id].Desc);
                        Logger.AlarmLog.AppendLine(LogLevel.NoLog, "미지정 알람1 발생 ID = {0}, ALARM CODE = {1} ", EM_AL_LST.AL_0000_NONE, HappenAlarms[id].Desc);
                    }

                    if (HappenAlarms[id].State != EM_AL_STATE.Unused)
                        equip.HsmsPc.AlarmReport(new HsmsAlarmInfo() { IsSet = true, ID = id, Desc = id.ToString() });

                    EquipStatusDump.LogMotorsPosition(equip);
                    EquipStatusDump.LogEquipStatus(equip);
                }
            }
            else
            {
                _queShowAlarm.Enqueue(new Alarm() { ID = EM_AL_LST.AL_0000_NONE, Happen = true, State = EM_AL_STATE.Heavy, HappenTime = DateTime.Now });
                equip.IsPause = true;
                equip.IsHeavyAlarm = true;
                equip.Efem.ChangeMode(Step.EmEfemRunMode.Pause);
                Logger.Log.AppendLine(LogLevel.Warning, "미지정 알람2 발생 ID = {0}, ALARM CODE = {1} ", EM_AL_LST.AL_0000_NONE, "미지정 알람이 발생함");
                Logger.AlarmLog.AppendLine(LogLevel.NoLog, "미지정 알람2 발생 ID = {0}, ALARM CODE = {1} ", EM_AL_LST.AL_0000_NONE, "미지정 알람이 발생함");
            }

            return true;
        }

        private void AlarmThread()
        {
            GG.Equip.BuzzerK2.OnOff(GG.Equip, true);
            Thread.Sleep(500);
            GG.Equip.BuzzerK2.OnOff(GG.Equip, false);
        }

        private void PasteToHistoryListView(ref List<ListView> source, ref List<ListView> dest)
        {
            foreach (ListView lstv in source)
            {
                try
                {
                    if (System.Threading.Monitor.TryEnter(lstv))
                    {
                        DateTime st = DateTime.Now;
                        int cnt = lstv.Items.Count;
                        if (LstlogAlarms.IndexOf(lstv) == 0)
                        {
                            for (int itemIter = lstv.Items.Count - 1; itemIter >= 0; itemIter--)
                            {
                                //lstv.Items[itemIter].ForeColor = Color.Black; 색은 바꿀 필요가?                            
                                foreach (ListView lstvHistory in dest)
                                    _quePasteItem.Enqueue(lstv.Items[itemIter].Clone() as ListViewItem);

                            }
                        }
                        lstv.Items.Clear();
                        System.Threading.Monitor.Exit(lstv);
                    }
                }
                catch (System.Exception ex)
                {
                    System.Threading.Monitor.Exit(lstv);
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public bool Clear(Equipment equip)
        {
            HappenAlarms.Values.ToList().ForEach(f =>
                {
                    if (f.Happen)
                    {
                        equip.HsmsPc.AlarmReport(new HsmsAlarmInfo() { IsSet = false, ID = f.ID, Desc = f.ID.ToString() });
                    }

                    if (f.ID == EM_AL_LST.AL_0286_CP_BOX_TEMPERATURE_HIGH_ERROR
                        || f.ID == EM_AL_LST.AL_0287_PC_RACK1_TEMPERATURE_HIGH_ERROR
                        )
                    {
                        if (equip.TempCtrlReseted == true)
                            f.Happen = false;
                    }
                    else
                        f.Happen = false;
                });
            PasteToHistoryListView(ref LstlogAlarms, ref LstlogAlarmHistory);

            if (HappenAlarms[EM_AL_LST.AL_0286_CP_BOX_TEMPERATURE_HIGH_ERROR].Happen == true)
            {
                HappenAlarms[EM_AL_LST.AL_0286_CP_BOX_TEMPERATURE_HIGH_ERROR].Happen = false;
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0286_CP_BOX_TEMPERATURE_HIGH_ERROR);
            }
            if (HappenAlarms[EM_AL_LST.AL_0287_PC_RACK1_TEMPERATURE_HIGH_ERROR].Happen == true)
            {
                HappenAlarms[EM_AL_LST.AL_0287_PC_RACK1_TEMPERATURE_HIGH_ERROR].Happen = false;
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0287_PC_RACK1_TEMPERATURE_HIGH_ERROR);
            }
            equip.IsHeavyAlarm = false;
            equip.IsLightAlarm = false;
            return false;
        }
        public void InitializeAlarmView(ListView logView)
        {
            _imgList.Images.Add(System.Drawing.SystemIcons.Warning);
            _imgList.Images.Add(System.Drawing.SystemIcons.Error);
            _imgList.Images.Add(System.Drawing.SystemIcons.Information);

            logView.View = View.Details;
            logView.FullRowSelect = true;
            logView.SmallImageList = _imgList;
            logView.LargeImageList = _imgList;

            int width = logView.Width - 210 > 0 ? logView.Width - 210 : 300;
            logView.Columns.Add(new ColumnHeader() { Width = 10, Name = "chNo", Text = "-" });
            logView.Columns.Add(new ColumnHeader() { Width = 120, Name = "chTime", Text = "TIME", TextAlign = HorizontalAlignment.Center });
            logView.Columns.Add(new ColumnHeader() { Width = 50, Name = "chID", Text = "ID", TextAlign = HorizontalAlignment.Center });
            logView.Columns.Add(new ColumnHeader() { Width = width, Name = "chLog", Text = "TEXT", TextAlign = HorizontalAlignment.Left });

            logView.DoubleClick += delegate (object sender, EventArgs e)
            {
                ListView l = (ListView)sender;
                if (l.SelectedItems.Count == 1)
                {
                    //int alarmNum = 0;
                    //int.TryParse(l.SelectedItems[0].SubItems[2].Text, out alarmNum);

                    //foreach (Form openForm in Application.OpenForms)
                    //{
                    //    if (openForm.Name == "SolutionDialog")
                    //    {
                    //        openForm.Close();
                    //        break;
                    //    }
                    //}

                    //SolutionDialog dlg = new SolutionDialog(AlarmMgr.Instance.GetSolution(alarmNum));
                    //dlg.Show();
                    string str = l.SelectedItems[0].SubItems[3].Text;
                    int idx = int.Parse(str.Substring(3, 4));


                    foreach (Form openForm in Application.OpenForms)
                    {
                        if (openForm.Name == "FrmAlarmSolution")
                        {
                            if (openForm.WindowState == FormWindowState.Minimized)
                            {
                                openForm.WindowState = FormWindowState.Normal;
                            }
                            openForm.Close();
                            break;
                        }
                    }

                    FrmAlarmSolution form = new FrmAlarmSolution(GG.Equip.AlarmSolutionMgr);
                    form.Show();
                    form.SetFocus(idx);
                }
            };
            //logView.Resize += delegate(object sender, EventArgs e)
            //{
            //    logView.Columns[2].Width = logView.Width - 210 > 0 ? logView.Width - 210 : 300; ;
            //};
        }
        private void InitializeAlarmList()
        {
            SortedList<EM_AL_LST, Alarm> lstAlarmInfo = GetAlarmInfo();
            if (lstAlarmInfo == null)
            {
                string[] lines = new string[Enum.GetNames(typeof(EM_AL_LST)).Length];
                int i = 0;
                foreach (string name in Enum.GetNames(typeof(EM_AL_LST)))
                {
                    lines[i] = string.Format("{0:D4},{1},{2},{3},{4}", i, name, name, "HEAVY", "NORMAL");
                    i++;
                }
                File.WriteAllLines(PATH_SETTING, lines, Encoding.Default);
                return;
            }

            foreach (EM_AL_LST alid in Enum.GetValues(typeof(EM_AL_LST)))
            {
                if (lstAlarmInfo.ContainsKey(alid) == false)
                    HappenAlarms.Add(alid, new Alarm() { ID = alid });
                else
                    HappenAlarms.Add(alid, lstAlarmInfo[alid]);
            }

            if (lstAlarmInfo.Count == 0) return;
        }
        public void AlarmStateChange(DataGridView dgvAlarmSetting)
        {
            foreach (DataGridViewRow dgvRow in dgvAlarmSetting.Rows)
            {
                if (dgvRow.Cells[3].Value != null)
                {
                    EM_AL_LST id = (EM_AL_LST)(int.Parse(dgvRow.Cells[0].Value.ToString()));
                    EM_AL_STATE state = GetState(dgvRow.Cells[3].Value.ToString(), dgvRow.Cells[4].Value.ToString(), dgvRow.Cells[5].Value.ToString());
                    try
                    {
                        HappenAlarms[id].State = state;
                    }
                    catch (Exception)
                    {
                        throw new Exception("존재하지 않는 알람 코드!!");
                    }
                }
            }
        }

        public bool SaveAlarmINIFile(DataGridView dgvAlarmSetting)
        {
            string[] lines = new string[dgvAlarmSetting.Rows.Count];
            int iter = 0;
            foreach (DataGridViewRow dgvRow in dgvAlarmSetting.Rows)
            {
                string stateStr = string.Empty;

                if (dgvRow.Cells[3].Value != null)
                {
                    stateStr = GetStateStr(dgvRow.Cells[3].Value.ToString(), dgvRow.Cells[4].Value.ToString(), dgvRow.Cells[5].Value.ToString());

                    lines[dgvRow.Index] = dgvRow.Cells[0].Value.ToString() + ","
                                        + dgvRow.Cells[1].Value.ToString() + ","
                                        + dgvRow.Cells[2].Value.ToString() + ","
                                        + stateStr + ","
                                        + GetLevel(iter).ToString();

                }
                else
                {
                    if (dgvRow.Cells[0].Value == null) break;
                    lines[dgvRow.Index] = dgvRow.Cells[0].Value.ToString() + ",,,";
                }
                iter++;
            }

            File.WriteAllLines(PATH_SETTING, lines, Encoding.Default);
            return true;
        }
        private EM_AL_STATE GetState(string isHeavy, string isWarn, string isUnused)
        {
            EM_AL_STATE state = EM_AL_STATE.Heavy;

            if (isHeavy.Equals("True"))
                state = EM_AL_STATE.Heavy;
            else if (isWarn.Equals("True"))
                state = EM_AL_STATE.Warn;
            else if (isUnused.Equals("True"))
                state = EM_AL_STATE.Unused;

            return state;
        }
        private string GetStateStr(string isHeavy, string isWarn, string isUnused)
        {
            string stateStr = string.Empty;

            if (isHeavy.Equals("True"))
                stateStr = "HEAVY";
            else if (isWarn.Equals("True"))
                stateStr = "WARN";
            else if (isUnused.Equals("True"))
                stateStr = "UNUSED";

            return stateStr;
        }
        private string GetLevelStr(DataGridViewCellStyle isWarn, DataGridViewCellStyle isUnused)
        {
            string levelStr = string.Empty;

            if (isWarn.BackColor == Color.LightGray)
                return levelStr = "HEAVY";
            else if (isUnused.BackColor == Color.LightGray)
                return levelStr = "WARN";
            else
                return levelStr = "NORMAL";
        }
        public void LoadAlarmSettingList(DataGridView dgvAlarmSetting)
        {
            dgvAlarmSetting.Rows.Clear();

            for (int iter = 0; iter < HappenAlarms.Count; iter++)
            {
                EM_AL_LST id = (EM_AL_LST)iter;
                bool[] isChecked = new bool[]{  HappenAlarms[id].State == EM_AL_STATE.Heavy,
                                                HappenAlarms[id].State == EM_AL_STATE.Warn,
                                                HappenAlarms[id].State == EM_AL_STATE.Unused};

                string desc = (HappenAlarms[id].Desc == null) ? null : HappenAlarms[id].Desc.ToString();
                string name = (desc == null) ? null : HappenAlarms[id].ID.ToString();

                dgvAlarmSetting.Rows.Add(new string[]
                    {
                        iter.ToString(),
                        name,
                        desc
                    });

                dgvAlarmSetting.Rows[iter].Cells[3].ReadOnly = true;
                dgvAlarmSetting.Rows[iter].Cells[4].ReadOnly = true;
                dgvAlarmSetting.Rows[iter].Cells[5].ReadOnly = true;

                if (name == null) continue;
                dgvAlarmSetting.Rows[iter].Cells[3].Value = isChecked[0];
                dgvAlarmSetting.Rows[iter].Cells[4].Value = isChecked[1];
                dgvAlarmSetting.Rows[iter].Cells[5].Value = isChecked[2];

                if (HappenAlarms[id].Level == EM_AL_LV.Heavy)
                {
                    dgvAlarmSetting.Rows[iter].Cells[4].Style.BackColor = Color.LightGray;
                    dgvAlarmSetting.Rows[iter].Cells[5].Style.BackColor = Color.LightGray;

                    if ((bool)dgvAlarmSetting.Rows[iter].Cells[3].Value == false)
                    {
                        dgvAlarmSetting.Rows[iter].Cells[3].Value = true;
                        dgvAlarmSetting.Rows[iter].Cells[4].Value = false;
                        dgvAlarmSetting.Rows[iter].Cells[5].Value = false;
                    }
                }
                else if (HappenAlarms[id].Level == EM_AL_LV.Warn)
                {
                    dgvAlarmSetting.Rows[iter].Cells[5].Style.BackColor = Color.LightGray;

                    if ((bool)dgvAlarmSetting.Rows[iter].Cells[5].Value == true)
                    {
                        dgvAlarmSetting.Rows[iter].Cells[3].Value = true;
                        dgvAlarmSetting.Rows[iter].Cells[5].Value = false;
                    }
                }

            }
        }

        //특정 알람 Level 변경
        public void ChangeAlarmLevel(int row, EM_AL_LV lvl)
        {
            EM_AL_LST id = (EM_AL_LST)row;
            HappenAlarms[id].Level = lvl;
        }
        public EM_AL_LV GetLevel(int row)
        {
            EM_AL_LST id = (EM_AL_LST)row;
            return HappenAlarms[id].Level;
        }

        public byte[] GetAlarmBytes()
        {
            //BitArray array = new BitArray(HappenAlarms.Values.Select(f => f.Happen).ToArray());            
            byte[] ret = new byte[(int)Math.Floor(HappenAlarms.Count / 8f) + 1];
            foreach (EM_AL_LST key in HappenAlarms.Keys)
            {
                int iKey = (int)key;
                int iByte = (int)Math.Truncate(iKey / 8f);
                int iBit = iKey % 8;
                if (HappenAlarms[key].Happen)
                    ret[iByte] |= (byte)(1 << iBit);
            }
            return ret;
        }
        // 조치사항 관련
        public Dictionary<int, AlarmSolution> Solutions = new Dictionary<int, AlarmSolution>();
        public void InitializeSolution()
        {
            SortedList<EM_AL_LST, Alarm> lstAlarmInfo = GetAlarmInfo();

            string strAlarmNum;
            string alarmName;
            foreach (string alarmItem in Enum.GetNames(typeof(EM_AL_LST)))
            {
                //AL_0248_REVIEW_Y2_LOADING_POSITION_ERROR
                string[] temp = alarmItem.Split('_');
                if (temp.Length < 3 || alarmItem.Length < 8)
                    continue;

                strAlarmNum = temp[1];
                alarmName = alarmItem.Remove(0, 8);
                if (alarmName == string.Empty)
                    alarmName = "NONE";

                int alarmNum = 0;
                int.TryParse(strAlarmNum, out alarmNum);
                AlarmSolution solution = new AlarmSolution(alarmNum);
                if (solution.Load() == false)
                    solution.CreateNewFile();
                if (solution.Name != alarmName)
                {
                    if (alarmName == "NONE") // 삭제된 것.
                    {
                        solution = new AlarmSolution(alarmNum);
                        solution.Save();
                    }
                    else // 추가/변경된 것.
                    {
                        solution.Name = alarmName;
                        solution.Save();
                        Solutions[alarmNum] = solution;
                    }
                }
                else
                    Solutions[alarmNum] = solution;
            }
        }
        private void ReloadSolution(int alarmNum)
        {
            if (Solutions.ContainsKey(alarmNum))
                Solutions[alarmNum].Load();
        }
        public AlarmSolution GetSolution(int alarmNum)
        {
            if (Solutions.ContainsKey(alarmNum))
            {
                ReloadSolution(alarmNum);
                return Solutions[alarmNum];
            }
            else
                return new AlarmSolution(-1);
        }

        private SortedList<EM_AL_LST, Alarm> GetAlarmInfo()
        {
            SortedList<EM_AL_LST, Alarm> lstAlarmInfo = new SortedList<EM_AL_LST, Alarm>();

            if (File.Exists(PATH_SETTING) == false) return null;
            using (StreamReader sr = new StreamReader(PATH_SETTING, Encoding.Default, true))
            {
                string fileContent = sr.ReadToEnd();
                string[] lines = fileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                sr.Close();

                foreach (string line in lines)//File.ReadAllLines(PATH_SETTING, Encoding.Default))
                {
                    if (line == "") break;
                    string[] items = line.Split(',');

                    if (items[1].Length == 0)
                    {
                        lstAlarmInfo.Add((EM_AL_LST)(int.Parse(items[0])), new Alarm());
                    }
                    else
                    {
                        Alarm alarmItem = new Alarm();
                        int emIdx = EnumEx.GetIndexContains(typeof(EM_AL_LST), string.Format("{0:D4}", items[1]));
                        if (emIdx == -1)
                            continue;
                        alarmItem.ID = (EM_AL_LST)emIdx;
                        alarmItem.Desc = items[2].Length <= 8 ? alarmItem.ID.ToString() : items[2];
                        alarmItem.SetState(items[3]);
                        string level = items.Length < 5 ? items[3] : items[4]; //jys:설정 안돼있을 경우 대비
                        alarmItem.SetLevel(level);
                        lstAlarmInfo.Add(alarmItem.ID, alarmItem);
                    }
                }
            }
            return lstAlarmInfo;
        }

        public void AlarmStateChange(string alarmID, string alarmstate)
        {
            EM_AL_LST id = (EM_AL_LST)(Enum.Parse(typeof(EM_AL_LST), alarmID));
            EM_AL_STATE state = (EM_AL_STATE)Enum.Parse(typeof(EM_AL_STATE), alarmstate);
            try
            {
                if ((int)HappenAlarms[id].Level >= (int)state)
                {
                    HappenAlarms[id].State = state;
                    //HappenAlarms[id].Level = (EM_AL_LV)(int)state;
                    SaveAlarmINIFile();
                }
                else
                {
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "无法设置为低于警报设置变更权限。请变更权限后进行设置" : "알람 설정 변경 권한 보다 낮게 설정 할 수 없습니다. 권한 변경 후 설정 하십시오");

                    foreach (Form openForm in Application.OpenForms)
                    {
                        if (openForm.Name == "FrmSetting")
                        {
                            if (openForm.WindowState == FormWindowState.Minimized)
                            {
                                openForm.WindowState = FormWindowState.Normal;
                            }
                            openForm.Dispose();
                            break;
                        }
                    }

                    Setting.FrmSetting ff = new Setting.FrmSetting(GG.Equip);
                    ff.SetStartPage("AlarmSetting", (int)id);
                    ff.Show();

                }
            }
            catch (System.Exception ex)
            {
                throw new Exception("존재하지 않는 알람 코드!!");
            }
        }
        public bool SaveAlarmINIFile()
        {
            string[] lines = new string[HappenAlarms.Count];
            for (int i = 0; i < HappenAlarms.Count; i++)
            {
                string stateStr = string.Empty;
                string levelStr = string.Empty;

                lines[i] = i.ToString() + ","
                         + HappenAlarms[(EM_AL_LST)i].ID.ToString() + ","
                         + HappenAlarms[(EM_AL_LST)i].Desc.ToString() + ","
                         + HappenAlarms[(EM_AL_LST)i].State.ToString() + ","
                         + HappenAlarms[(EM_AL_LST)i].Level.ToString();
            }

            File.WriteAllLines(PATH_SETTING, lines);
            return true;
        }
    }
}