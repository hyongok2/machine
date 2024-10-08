using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipSimulator.Acturator;
using EquipSimulator;
using Dit.Framework.PLC;
using EquipSimulator.Log;
using Dit.Framework.Log;
using EquipMainUi.Struct;
using Dit.Framework.Comm;

namespace EquipSimulator
{
    public class EquipSimul
    {
        public InspPcSimul InspPc;
        public RevPCSimul ReviPc;

        public SylinderSimul StandCentering1;
        public SylinderSimul StandCentering2;
        public SylinderSimul StandCentering3;
        public SylinderSimul StandCentering4;
        public SylinderSimul AssistCentering1;
        public SylinderSimul AssistCentering2;
        public SylinderSimul AssistCentering3;
        public SylinderSimul AssistCentering4;
        public SylinderSimul RearCentering1;
        public SylinderSimul RearCentering3;
        public SylinderSimul RearCentering4;
        public SylinderSimul RearCentering2;

        public SylinderSimul FrontCentering1;
        public SylinderSimul FrontCentering2;
        //         public SylinderSimul FrontCenteringUD1;
        //        public SylinderSimul FrontCenteringUD2;

        public SylinderSimul SepCenteringLeft1;
        public SylinderSimul SepCenteringLeft2;
        public SylinderSimul SepCenteringRight1;
        public SylinderSimul SepCenteringRight2;
        public SylinderSimul SepCenteringUD1;
        public SylinderSimul SepCenteringUD2;

        public SylinderLiftPinSimul LiftPin;
        
        public ServoSimulUmac StageX;
        public ServoSimulUmac Y1;
        public ServoSimulUmac Y2;
        public ServoSimulUmac Theta;
        public ServoSimulUmac WsiZ;

        public StepMotorSimul AlignX;
        public StepMotorSimul AlignY;
        public StepMotorSimul AlignT;

        public ServoSimulUmac ReviX1;
        public ServoSimulUmac ReviX2;        
        public ServoSimulUmac ReviY2;
        public ServoSimulUmac InspZ01;
        public ServoSimulUmac InspZ02;
        public ServoSimulUmac InspZ03;
        public ServoSimulUmac InspZ04;
        public ServoSimulUmac InspZ05;
        public ServoSimulUmac InspZ06;
        public ServoSimulUmac InspZ07;
        public ServoSimulUmac InspZ08;
        public ServoSimulUmac InspZ09;
        public ServoSimulUmac InspZ10;
        public ServoSimulUmac InspZ11;
        public ServoSimulUmac[] Motors { get; set; }
        public StepMotorSimul[] StepMotors { get; set; }
        public SwitchSimulEx Vacuum1;
        public SwitchSimulEx Vacuum2;
        public SwitchSimulEx Vacuum3;
        public SwitchSimulEx Vacuum4;
        public SwitchSimulEx Vacuum5;
        //         public SwitchSimulEx Vacuum6;
        //         public SwitchSimulEx Vacuum7;
        //         public SwitchSimulEx Vacuum8;

        public SwitchSimulNonX Blower1;
        public SwitchSimulNonX Blower2;
        public SwitchSimulNonX Blower3;
        public SwitchSimulNonX Blower4;
        public SwitchSimulNonX Blower5;
        //         public SwitchSimulNonX Blower6;
        //         public SwitchSimulNonX Blower7;
        //         public SwitchSimulNonX Blower8;

        public SwitchSimul CameraCooling;
        public SwitchSimul IonizerCover;
        public SwitchSimul Ionizer;

        public SwitchSimul EMERGENCY_STOP_1;
        public SwitchSimul EMERGENCY_STOP_2;
        public SwitchSimul EMERGENCY_STOP_3;
        public SwitchSimul EMERGENCY_STOP_4;
        public SwitchSimul EMERGENCY_STOP_5;

        public SwitchSimul MODE_SELECT_KEY_SW_AUTOTEACH1;
        public SwitchSimul MODE_SELECT_KEY_SW_AUTOTEACH2;


        public SwitchSimul ENABLE_GRIB_SW_ON;
        public SwitchSimul SAFETY_PLC_ERROR;

        public SenseSimul DOOR01_SENSOR;
        public SenseSimul DOOR02_SENSOR;
        public SenseSimul DOOR03_SENSOR;
        public SenseSimul DOOR04_SENSOR;
        public SenseSimul DOOR05_SENSOR;
        public SenseSimul DOOR06_SENSOR;
        public SenseSimul DOOR07_SENSOR;
        public SenseSimul DOOR08_SENSOR;
        public SenseSimul DOOR09_SENSOR;
        public SenseSimul DOOR10_SENSOR;
        public SenseSimul DOOR11_SENSOR;
        public SenseSimul DOOR12_SENSOR;
        public SenseSimul DOOR13_SENSOR;
        public SenseSimul DOOR14_SENSOR;
        public SenseSimul DOOR15_SENSOR;

        public SenseSimul LiftpinUpSensor1;
        public SenseSimul LiftpinDownSensor1;
        public SenseSimul LiftpinUpSensor2;
        public SenseSimul LiftpinDownSensor2;
        public SenseSimul LiftpinUpSensor3;
        public SenseSimul LiftpinDownSensor3;
        public SenseSimul LiftpinUpSensor4;
        public SenseSimul LiftpinDownSensor4;

        public SwitchSimulEx WAFER_DETECT_SENSOR_1;
        public SwitchSimulEx WAFER_DETECT_SENSOR_2;
        public SwitchSimulEx WAFER_DETECT_SENSOR_3;

        public SwitchSimulEx GLASS_DETECT_SENSOR_1;
        public SwitchSimulEx GLASS_DETECT_SENSOR_2;
        public SwitchSimulEx GLASS_DETECT_SENSOR_3;
        public SwitchSimulEx GLASS_EDGE_DETECT_SENSOR_1;
        public SwitchSimulEx GLASS_EDGE_DETECT_SENSOR_2;
        public SwitchSimulEx GLASS_EDGE_DETECT_SENSOR_3;
        public SwitchSimulEx GLASS_EDGE_DETECT_SENSOR_4;
        public SwitchSimulEx GLASS_EDGE_DETECT_SENSOR_5;
        public SwitchSimulEx GLASS_EDGE_DETECT_SENSOR_6;
        public SwitchSimulEx GLASS_EDGE_DETECT_SENSOR_7;
        public SwitchSimulEx GLASS_EDGE_DETECT_SENSOR_8;
        public SwitchSimulEx GLASS_EDGE_DETECT_SENSOR_9;
        public SwitchSimulEx GLASS_EDGE_DETECT_SENSOR_10;
        public SwitchSimul ROBOT_ARM_DETECT;

        public SwitchSimul ISOLATOR_1_ALARM;
        public SwitchSimul ISOLATOR_2_ALARM;
        public SwitchSimul ISOLATOR_3_ALARM;
        public SwitchSimul ISOLATOR_4_ALARM;

        public SwitchSimul IONIZER_1_ON;
        public SwitchSimul IONIZER_2_ON;
        public SwitchSimul IONIZER_3_ON;
        public SwitchSimul IONIZER_4_ON;

        public SwitchSimul IONIZER_1_ALARM;
        public SwitchSimul IONIZER_2_ALARM;
        public SwitchSimul IONIZER_3_ALARM;
        public SwitchSimul IONIZER_4_ALARM;

        public SenseSimul IsEFEMInputArm;
        public SenseSimul IsEFEMAlignerInputArm;

        //Pre Aligner 2호기 전용
        public SwitchSimul AlignVac;
        public SylinderSimul AlignOcrCylinder;

        public bool InterLock;

        public SenseSimul OHT_LPM1_IN1;
        public SenseSimul OHT_LPM1_IN2;
        public SenseSimul OHT_LPM1_IN3;
        public SenseSimul OHT_LPM1_IN4;
        public SenseSimul OHT_LPM1_IN5;
        public SenseSimul OHT_LPM1_IN6;
        public SenseSimul OHT_LPM1_IN7;
        public SenseSimul OHT_LPM1_IN8;
        public SenseSimul OHT_LPM2_IN1;
        public SenseSimul OHT_LPM2_IN2;
        public SenseSimul OHT_LPM2_IN3;
        public SenseSimul OHT_LPM2_IN4;
        public SenseSimul OHT_LPM2_IN5;
        public SenseSimul OHT_LPM2_IN6;
        public SenseSimul OHT_LPM2_IN7;
        public SenseSimul OHT_LPM2_IN8;

        public SwitchSimulEx OHT_LPM1_OUT1;
        public SwitchSimulEx OHT_LPM1_OUT2;
        public SwitchSimulEx OHT_LPM1_OUT3;
        public SwitchSimulEx OHT_LPM1_OUT4;
        public SwitchSimulEx OHT_LPM1_OUT5;
        public SwitchSimulEx OHT_LPM1_OUT6;
        public SwitchSimulEx OHT_LPM1_OUT7;
        public SwitchSimulEx OHT_LPM1_OUT8;
        public SwitchSimulEx OHT_LPM2_OUT1;
        public SwitchSimulEx OHT_LPM2_OUT2;
        public SwitchSimulEx OHT_LPM2_OUT3;
        public SwitchSimulEx OHT_LPM2_OUT4;
        public SwitchSimulEx OHT_LPM2_OUT5;
        public SwitchSimulEx OHT_LPM2_OUT6;
        public SwitchSimulEx OHT_LPM2_OUT7;
        public SwitchSimulEx OHT_LPM2_OUT8;

        public PioHandShake PioA2I = new PioHandShake();
        public PioHandShake PioI2A = new PioHandShake();
        public PMacSimul PMac = new PMacSimul();

        public EquipSimul()
        {
            StandCentering1 = new SylinderSimul() { Name = "기준센터링1", TotalLength = 100 };
            StandCentering2 = new SylinderSimul() { Name = "기준센터링2", TotalLength = 100 };
            StandCentering3 = new SylinderSimul() { Name = "기준센터링3", TotalLength = 100 };
            StandCentering4 = new SylinderSimul() { Name = "기준센터링4", TotalLength = 100 };
            AssistCentering1 = new SylinderSimul() { Name = "보조센터링1", TotalLength = 100 };
            AssistCentering2 = new SylinderSimul() { Name = "보조센터링2", TotalLength = 100 };
            AssistCentering3 = new SylinderSimul() { Name = "보조센터링3", TotalLength = 100 };
            AssistCentering4 = new SylinderSimul() { Name = "보조센터링4", TotalLength = 100 };
            RearCentering1 = new SylinderSimul() { Name = "후면센터링1", TotalLength = 100 };
            RearCentering2 = new SylinderSimul() { Name = "후면센터링2", TotalLength = 100 };
            RearCentering3 = new SylinderSimul() { Name = "후면센터링3", TotalLength = 100 };
            RearCentering4 = new SylinderSimul() { Name = "후면센터링4", TotalLength = 100 };
            FrontCentering1 = new SylinderSimul() { Name = "전면센터링1", TotalLength = 100 };
            FrontCentering2 = new SylinderSimul() { Name = "전면센터링2", TotalLength = 100 };
            //             FrontCenteringUD1 = new SylinderSimul() { Name = "전면센터링 상하1", TotalLength = 100 };
            //             FrontCenteringUD2 = new SylinderSimul() { Name = "전면센터링 상하2", TotalLength = 100 };
            SepCenteringLeft1 = new SylinderSimul() { Name = "분판센터링 좌측1", TotalLength = 100 };
            SepCenteringLeft2 = new SylinderSimul() { Name = "분판센터링 좌측2", TotalLength = 100 };
            SepCenteringRight1 = new SylinderSimul() { Name = "분판센터링 우측1", TotalLength = 100 };
            SepCenteringRight2 = new SylinderSimul() { Name = "분판센터링 우측2", TotalLength = 100 };
            SepCenteringUD1 = new SylinderSimul() { Name = "분판센터링 상하1", TotalLength = 100 };
            SepCenteringUD2 = new SylinderSimul() { Name = "분판센터링 상하2", TotalLength = 100 };

            LiftPin = new SylinderLiftPinSimul() { Name = "리프트핀", TotalLength = 100 };

            StageX = new ServoSimulUmac("Axis01", 0, 800, 200, 1);
            Y1 = new ServoSimulUmac("Axis02", 0, 500, 50, 1);           
            Theta = new ServoSimulUmac("Axis05", -90, 270, 1, 1);

            AlignX = new StepMotorSimul(2, 0, 20, 1);
            AlignY = new StepMotorSimul(1, 0, 20, 1);
            AlignT = new StepMotorSimul(0, 0, 20, 1);

            Y2      = new ServoSimulUmac("Axis03", 0, 1570, 50, 1);
            WsiZ    = new ServoSimulUmac("Axis09", 0, 50, 5, 1);           
            ReviX1  = new ServoSimulUmac("ReviX1", 0, 1850, 100, 1);
            ReviX2  = new ServoSimulUmac("ReviX2", 0, 1850, 100, 1);            
            ReviY2  = new ServoSimulUmac("ReviY2", 0, 1570, 50, 1);        
            InspZ02 = new ServoSimulUmac("InspZ02",0,  50, 5, 1);
            InspZ03 = new ServoSimulUmac("InspZ03",0,  50, 5, 1);
            InspZ04 = new ServoSimulUmac("InspZ04",0,  50, 5, 1);
            InspZ05 = new ServoSimulUmac("InspZ05",0,  50, 5, 1);
            InspZ06 = new ServoSimulUmac("InspZ06",0,  50, 5, 1);
            InspZ07 = new ServoSimulUmac("InspZ07",0,  50, 5, 1);
            InspZ08 = new ServoSimulUmac("InspZ08",0,  50, 5, 1);
            InspZ09 = new ServoSimulUmac("InspZ09",0,  50, 5, 1);
            InspZ10 = new ServoSimulUmac("InspZ10",0,  50, 5, 1);
            InspZ11 = new ServoSimulUmac("InspZ11",0,  50, 5, 1);

            IsEFEMInputArm = new SenseSimul();
            IsEFEMAlignerInputArm = new SenseSimul();

            Motors = new ServoSimulUmac[] {                  
                StageX,
                Y1,
                Theta,
            };
            StepMotors = new StepMotorSimul[]
            {
                AlignX,
                AlignY,
                AlignT
            };
        }


        public void SetAddress()
        {
            Console.WriteLine("== Start SetAddress ==");
            #region Backup

            EMERGENCY_STOP_5.YB_OnOff = EMERGENCY_STOP_5.XB_OnOff                                        /**/ = AddressMgr.GetAddress("EMERGENCY_STOP_5(INSIDE)", 0);
            MODE_SELECT_KEY_SW_AUTOTEACH1.YB_OnOff = MODE_SELECT_KEY_SW_AUTOTEACH1.XB_OnOff                /**/ = AddressMgr.GetAddress("MODE_SELECT_KEY_SW_AUTOTEACH1", 0);

            ENABLE_GRIB_SW_ON.YB_OnOff = ENABLE_GRIB_SW_ON.XB_OnOff                                      /**/ = AddressMgr.GetAddress("ENABLE_GRIB_SW_ON1", 0);

            Vacuum2.YB_OnOff                                        /**/ = AddressMgr.GetAddress("STAGE_VACUUM_SOL_2", 0);
            Vacuum3.YB_OnOff                                        /**/ = AddressMgr.GetAddress("STAGE_VACUUM_SOL_3", 0);
            Vacuum4.YB_OnOff                                        /**/ = AddressMgr.GetAddress("STAGE_VACUUM_SOL_4", 0);
            Vacuum5.YB_OnOff                                        /**/ = AddressMgr.GetAddress("STAGE_VACUUM_SOL_5", 0);

            Vacuum2.XB_OnOff                                        /**/ = AddressMgr.GetAddress("CHECK_VACUUM_2", 0);
            Vacuum3.XB_OnOff                                        /**/ = AddressMgr.GetAddress("CHECK_VACUUM_3", 0);
            Vacuum4.XB_OnOff                                        /**/ = AddressMgr.GetAddress("CHECK_VACUUM_4", 0);
            Vacuum5.XB_OnOff                                        /**/ = AddressMgr.GetAddress("CHECK_VACUUM_5", 0);

            Blower2.YB_OnOff                                        /**/ = AddressMgr.GetAddress("STAGE_BLOWER_SOL_2", 0);
            Blower3.YB_OnOff                                        /**/ = AddressMgr.GetAddress("STAGE_BLOWER_SOL_3", 0);
            Blower4.YB_OnOff                                        /**/ = AddressMgr.GetAddress("STAGE_BLOWER_SOL_4", 0);
            Blower5.YB_OnOff                                        /**/ = AddressMgr.GetAddress("STAGE_BLOWER_SOL_5", 0);

            StandCentering1.YB_ForwardCmd = AddressMgr.GetAddress("STANDARD_CENTERING_1_FORWARD_SOL", 0);
            StandCentering2.YB_ForwardCmd = AddressMgr.GetAddress("STANDARD_CENTERING_1_FORWARD_SOL", 0);
            StandCentering1.YB_BackwardCmd = AddressMgr.GetAddress("STANDARD_CENTERING_1_BACKWARD_SOL", 0);
            StandCentering2.YB_BackwardCmd = AddressMgr.GetAddress("STANDARD_CENTERING_1_BACKWARD_SOL", 0);



            StandCentering3.YB_ForwardCmd = AddressMgr.GetAddress("STANDARD_CENTERING_3_FORWARD_SOL", 0);
            StandCentering4.YB_ForwardCmd = AddressMgr.GetAddress("STANDARD_CENTERING_3_FORWARD_SOL", 0);
            AssistCentering1.YB_ForwardCmd = AddressMgr.GetAddress("ASSISTANCE_CENTERING_1_FORWARD_SOL", 0);
            AssistCentering2.YB_ForwardCmd = AddressMgr.GetAddress("ASSISTANCE_CENTERING_1_FORWARD_SOL", 0);
            AssistCentering3.YB_ForwardCmd = AddressMgr.GetAddress("ASSISTANCE_CENTERING_3_FORWARD_SOL", 0);
            AssistCentering4.YB_ForwardCmd = AddressMgr.GetAddress("ASSISTANCE_CENTERING_3_FORWARD_SOL", 0);
            RearCentering1.YB_ForwardCmd = AddressMgr.GetAddress("REAR_CENTERING_1_FORWARD_SOL", 0);
            RearCentering2.YB_ForwardCmd = AddressMgr.GetAddress("REAR_CENTERING_1_FORWARD_SOL", 0);
            RearCentering3.YB_ForwardCmd = AddressMgr.GetAddress("REAR_CENTERING_3_FORWARD_SOL", 0);
            RearCentering4.YB_ForwardCmd = AddressMgr.GetAddress("REAR_CENTERING_3_FORWARD_SOL", 0);
            FrontCentering1.YB_ForwardCmd = AddressMgr.GetAddress("FRONT_CENTERING_1_FORWARD_SOL", 0);
            FrontCentering2.YB_ForwardCmd = AddressMgr.GetAddress("FRONT_CENTERING_1_FORWARD_SOL", 0);
            SepCenteringLeft1.YB_ForwardCmd = AddressMgr.GetAddress("BEFORE_SAPARATION_CENTRING_1_FORWARD_SOL", 0);
            SepCenteringLeft2.YB_ForwardCmd = AddressMgr.GetAddress("BEFORE_SAPARATION_CENTRING_1_FORWARD_SOL", 0);
            SepCenteringRight1.YB_ForwardCmd = AddressMgr.GetAddress("AFTER_SAPARATION_CENTRING_1_FORWARD_SOL", 0);
            SepCenteringRight2.YB_ForwardCmd = AddressMgr.GetAddress("AFTER_SAPARATION_CENTRING_1_FORWARD_SOL", 0);
            SepCenteringUD1.YB_ForwardCmd = AddressMgr.GetAddress("SAPARATION_CENTRING_1_UP_SOL", 0);
            SepCenteringUD2.YB_ForwardCmd = AddressMgr.GetAddress("SAPARATION_CENTRING_1_UP_SOL", 0);

            StandCentering3.YB_BackwardCmd = AddressMgr.GetAddress("STANDARD_CENTERING_3_BACKWARD_SOL", 0);
            StandCentering4.YB_BackwardCmd = AddressMgr.GetAddress("STANDARD_CENTERING_3_BACKWARD_SOL", 0);
            AssistCentering1.YB_BackwardCmd = AddressMgr.GetAddress("ASSISTANCE_CENTERING_1_BACKWARD_SOL", 0);
            AssistCentering2.YB_BackwardCmd = AddressMgr.GetAddress("ASSISTANCE_CENTERING_1_BACKWARD_SOL", 0);
            AssistCentering3.YB_BackwardCmd = AddressMgr.GetAddress("ASSISTANCE_CENTERING_3_BACKWARD_SOL", 0);
            AssistCentering4.YB_BackwardCmd = AddressMgr.GetAddress("ASSISTANCE_CENTERING_3_BACKWARD_SOL", 0);
            RearCentering1.YB_BackwardCmd = AddressMgr.GetAddress("REAR_CENTERING_1_BACKWARD_SOL", 0);
            RearCentering2.YB_BackwardCmd = AddressMgr.GetAddress("REAR_CENTERING_1_BACKWARD_SOL", 0);
            RearCentering3.YB_BackwardCmd = AddressMgr.GetAddress("REAR_CENTERING_3_BACKWARD_SOL", 0);
            RearCentering4.YB_BackwardCmd = AddressMgr.GetAddress("REAR_CENTERING_3_BACKWARD_SOL", 0);
            FrontCentering1.YB_BackwardCmd = AddressMgr.GetAddress("FRONT_CENTERING_1_BACKWARD_SOL", 0);
            FrontCentering2.YB_BackwardCmd = AddressMgr.GetAddress("FRONT_CENTERING_1_BACKWARD_SOL", 0);
            //             FrontCenteringUD1.YB_BackwardCmd = AddressMgr.GetAddress("FRONT_CENTERING_1_DOWN_SOL", 0);
            //             FrontCenteringUD2.YB_BackwardCmd = AddressMgr.GetAddress("FRONT_CENTERING_1_DOWN_SOL", 0);
            SepCenteringLeft1.YB_BackwardCmd = AddressMgr.GetAddress("BEFORE_SAPARATION_CENTRING_1_BACKWARD_SOL", 0);
            SepCenteringLeft2.YB_BackwardCmd = AddressMgr.GetAddress("BEFORE_SAPARATION_CENTRING_1_BACKWARD_SOL", 0);
            SepCenteringRight1.YB_BackwardCmd = AddressMgr.GetAddress("AFTER_SAPARATION_CENTRING_1_BACKWARD_SOL", 0);
            SepCenteringRight2.YB_BackwardCmd = AddressMgr.GetAddress("AFTER_SAPARATION_CENTRING_1_BACKWARD_SOL", 0);
            SepCenteringUD1.YB_BackwardCmd = AddressMgr.GetAddress("SAPARATION_CENTRING_1_DOWN_SOL", 0);
            SepCenteringUD2.YB_BackwardCmd = AddressMgr.GetAddress("SAPARATION_CENTRING_1_DOWN_SOL", 0);

            StandCentering1.XB_ForwardComplete = AddressMgr.GetAddress("STANDARD_CENTERING_1_FORWARD_SENSOR", 0);
            StandCentering2.XB_ForwardComplete = AddressMgr.GetAddress("STANDARD_CENTERING_2_FORWARD_SENSOR", 0);
            StandCentering3.XB_ForwardComplete = AddressMgr.GetAddress("STANDARD_CENTERING_3_FORWARD_SENSOR", 0);
            StandCentering4.XB_ForwardComplete = AddressMgr.GetAddress("STANDARD_CENTERING_4_FORWARD_SENSOR", 0);
            AssistCentering1.XB_ForwardComplete = AddressMgr.GetAddress("ASSISTANCE_CENTERING_1_FORWARD_SENSOR", 0);
            AssistCentering2.XB_ForwardComplete = AddressMgr.GetAddress("ASSISTANCE_CENTERING_2_FORWARD_SENSOR", 0);
            AssistCentering3.XB_ForwardComplete = AddressMgr.GetAddress("ASSISTANCE_CENTERING_3_FORWARD_SENSOR", 0);
            AssistCentering4.XB_ForwardComplete = AddressMgr.GetAddress("ASSISTANCE_CENTERING_4_FORWARD_SENSOR", 0);
            RearCentering1.XB_ForwardComplete = AddressMgr.GetAddress("REAR_CENTERING_1_FORWARD_SENSOR", 0);
            RearCentering2.XB_ForwardComplete = AddressMgr.GetAddress("REAR_CENTERING_2_FORWARD_SENSOR", 0);
            RearCentering3.XB_ForwardComplete = AddressMgr.GetAddress("REAR_CENTERING_3_FORWARD_SENSOR", 0);
            RearCentering4.XB_ForwardComplete = AddressMgr.GetAddress("REAR_CENTERING_4_FORWARD_SENSOR", 0);
            FrontCentering1.XB_ForwardComplete = AddressMgr.GetAddress("FRONT_CENTERING_1_FORWARD_SENSOR", 0);
            FrontCentering2.XB_ForwardComplete = AddressMgr.GetAddress("FRONT_CENTERING_2_FORWARD_SENSOR", 0);
            SepCenteringLeft1.XB_ForwardComplete = AddressMgr.GetAddress("BEFORE_SAPARATION_CENTRING_1_FORWARD_SENSOR", 0);
            SepCenteringLeft2.XB_ForwardComplete = AddressMgr.GetAddress("BEFORE_SAPARATION_CENTRING_2_FORWARD_SENSOR", 0);
            SepCenteringRight1.XB_ForwardComplete = AddressMgr.GetAddress("AFTER_SAPARATION_CENTRING_1_FORWARD_SENSOR", 0);
            SepCenteringRight2.XB_ForwardComplete = AddressMgr.GetAddress("AFTER_SAPARATION_CENTRING_2_FORWARD_SENSOR", 0);
            SepCenteringUD1.XB_ForwardComplete = AddressMgr.GetAddress("SAPARATION_CENTRING_1_UP_SENSOR", 0);
            SepCenteringUD2.XB_ForwardComplete = AddressMgr.GetAddress("SAPARATION_CENTRING_2_UP_SENSOR", 0);

            StandCentering1.XB_BackwardComplete = AddressMgr.GetAddress("STANDARD_CENTERING_1_BACKWARD_SENSOR", 0);
            StandCentering2.XB_BackwardComplete = AddressMgr.GetAddress("STANDARD_CENTERING_2_BACKWARD_SENSOR", 0);
            StandCentering3.XB_BackwardComplete = AddressMgr.GetAddress("STANDARD_CENTERING_3_BACKWARD_SENSOR", 0);
            StandCentering4.XB_BackwardComplete = AddressMgr.GetAddress("STANDARD_CENTERING_4_BACKWARD_SENSOR", 0);
            AssistCentering1.XB_BackwardComplete = AddressMgr.GetAddress("ASSISTANCE_CENTERING_1_BACKWARD_SENSOR", 0);
            AssistCentering2.XB_BackwardComplete = AddressMgr.GetAddress("ASSISTANCE_CENTERING_2_BACKWARD_SENSOR", 0);
            AssistCentering3.XB_BackwardComplete = AddressMgr.GetAddress("ASSISTANCE_CENTERING_3_BACKWARD_SENSOR", 0);
            AssistCentering4.XB_BackwardComplete = AddressMgr.GetAddress("ASSISTANCE_CENTERING_4_BACKWARD_SENSOR", 0);
            RearCentering1.XB_BackwardComplete = AddressMgr.GetAddress("REAR_CENTERING_1_BACKWARD_SENSOR", 0);
            RearCentering2.XB_BackwardComplete = AddressMgr.GetAddress("REAR_CENTERING_2_BACKWARD_SENSOR", 0);
            RearCentering3.XB_BackwardComplete = AddressMgr.GetAddress("REAR_CENTERING_3_BACKWARD_SENSOR", 0);
            RearCentering4.XB_BackwardComplete = AddressMgr.GetAddress("REAR_CENTERING_4_BACKWARD_SENSOR", 0);
            FrontCentering1.XB_BackwardComplete = AddressMgr.GetAddress("FRONT_CENTERING_1_BACKWARD_SENSOR", 0);
            FrontCentering2.XB_BackwardComplete = AddressMgr.GetAddress("FRONT_CENTERING_2_BACKWARD_SENSOR", 0);
            //             FrontCenteringUD1.XB_BackwardComplete = AddressMgr.GetAddress("FRONT_CENTERING_1_DOWN_SENSOR", 0);
            //             FrontCenteringUD2.XB_BackwardComplete = AddressMgr.GetAddress("FRONT_CENTERING_2_DOWN_SENSOR", 0);
            SepCenteringLeft1.XB_BackwardComplete = AddressMgr.GetAddress("BEFORE_SAPARATION_CENTRING_1_BACKWARD_SENSOR", 0);
            SepCenteringLeft2.XB_BackwardComplete = AddressMgr.GetAddress("BEFORE_SAPARATION_CENTRING_2_BACKWARD_SENSOR", 0);
            SepCenteringRight1.XB_BackwardComplete = AddressMgr.GetAddress("AFTER_SAPARATION_CENTRING_1_BACKWARD_SENSOR", 0);
            SepCenteringRight2.XB_BackwardComplete = AddressMgr.GetAddress("AFTER_SAPARATION_CENTRING_2_BACKWARD_SENSOR", 0);
            SepCenteringUD1.XB_BackwardComplete = AddressMgr.GetAddress("SAPARATION_CENTRING_1_DOWN_SENSOR", 0);
            SepCenteringUD2.XB_BackwardComplete = AddressMgr.GetAddress("SAPARATION_CENTRING_2_DOWN_SENSOR", 0);

            //jys::ionizer 문제있으면 삭제할 것 
            IONIZER_1_ALARM.YB_OnOff = IONIZER_1_ALARM.XB_OnOff                                 /**/ = AddressMgr.GetAddress("IONIZER_1_ALARM", 0);
            IONIZER_2_ALARM.YB_OnOff = IONIZER_2_ALARM.XB_OnOff                                 /**/ = AddressMgr.GetAddress("IONIZER_2_ALARM", 0);
            IONIZER_3_ALARM.YB_OnOff = IONIZER_3_ALARM.XB_OnOff                                 /**/ = AddressMgr.GetAddress("IONIZER_3_ALARM", 0);
            IONIZER_4_ALARM.YB_OnOff = IONIZER_4_ALARM.XB_OnOff                                 /**/ = AddressMgr.GetAddress("IONIZER_4_ALARM", 0);

            DOOR01_SENSOR.XB_OnOff                                 /**/ = AddressMgr.GetAddress("TOP_DOOR1_SENSOR", 0);
            DOOR02_SENSOR.XB_OnOff                                 /**/ = AddressMgr.GetAddress("TOP_DOOR2_SENSOR", 0);
            DOOR03_SENSOR.XB_OnOff                                 /**/ = AddressMgr.GetAddress("TOP_DOOR3_SENSOR", 0);
            DOOR04_SENSOR.XB_OnOff                                 /**/ = AddressMgr.GetAddress("TOP_DOOR4_SENSOR", 0);

            IsEFEMInputArm.XB_OnOff                                 /**/ = AddressMgr.GetAddress("EFEM_INPUT_ARM_TO_AVI", 0);
            IsEFEMAlignerInputArm.XB_OnOff                          /**/ = AddressMgr.GetAddress("PRE_ALIGN_ROBOT_READY_IN", 0);

            ISOLATOR_1_ALARM.XB_OnOff = ISOLATOR_1_ALARM.YB_OnOff   /**/ = AddressMgr.GetAddress("ISOLATOR_1", 0);
            ISOLATOR_2_ALARM.XB_OnOff = ISOLATOR_2_ALARM.YB_OnOff   /**/ = AddressMgr.GetAddress("ISOLATOR_1", 0);
            ISOLATOR_3_ALARM.XB_OnOff = ISOLATOR_3_ALARM.YB_OnOff   /**/ = AddressMgr.GetAddress("ISOLATOR_1", 0);
            ISOLATOR_4_ALARM.XB_OnOff = ISOLATOR_4_ALARM.YB_OnOff   /**/ = AddressMgr.GetAddress("ISOLATOR_1", 0);

            GLASS_DETECT_SENSOR_1.YB_OnOff = GLASS_DETECT_SENSOR_1.XB_OnOff                       /**/ = AddressMgr.GetAddress("WAFER_DETECT_SENSOR_2", 0);
            GLASS_DETECT_SENSOR_2.YB_OnOff = GLASS_DETECT_SENSOR_2.XB_OnOff                       /**/ = AddressMgr.GetAddress("WAFER_DETECT_SENSOR_2", 0);

            GLASS_EDGE_DETECT_SENSOR_1.YB_OnOff = GLASS_EDGE_DETECT_SENSOR_1.XB_OnOff             /**/ = AddressMgr.GetAddress("GLASS_CRACK_DETECT_SENSOR_1", 0);
            GLASS_EDGE_DETECT_SENSOR_2.YB_OnOff = GLASS_EDGE_DETECT_SENSOR_2.XB_OnOff             /**/ = AddressMgr.GetAddress("GLASS_CRACK_DETECT_SENSOR_2", 0);
            GLASS_EDGE_DETECT_SENSOR_3.YB_OnOff = GLASS_EDGE_DETECT_SENSOR_3.XB_OnOff             /**/ = AddressMgr.GetAddress("GLASS_CRACK_DETECT_SENSOR_3", 0);
            GLASS_EDGE_DETECT_SENSOR_4.YB_OnOff = GLASS_EDGE_DETECT_SENSOR_4.XB_OnOff             /**/ = AddressMgr.GetAddress("GLASS_CRACK_DETECT_SENSOR_4", 0);
            GLASS_EDGE_DETECT_SENSOR_5.YB_OnOff = GLASS_EDGE_DETECT_SENSOR_5.XB_OnOff             /**/ = AddressMgr.GetAddress("GLASS_CRACK_DETECT_SENSOR_5", 0);
            GLASS_EDGE_DETECT_SENSOR_6.YB_OnOff = GLASS_EDGE_DETECT_SENSOR_6.XB_OnOff             /**/ = AddressMgr.GetAddress("GLASS_CRACK_DETECT_SENSOR_6", 0);
            GLASS_EDGE_DETECT_SENSOR_7.YB_OnOff = GLASS_EDGE_DETECT_SENSOR_7.XB_OnOff             /**/ = AddressMgr.GetAddress("GLASS_CRACK_DETECT_SENSOR_7", 0);
            GLASS_EDGE_DETECT_SENSOR_8.YB_OnOff = GLASS_EDGE_DETECT_SENSOR_8.XB_OnOff             /**/ = AddressMgr.GetAddress("GLASS_CRACK_DETECT_SENSOR_8", 0);
            GLASS_EDGE_DETECT_SENSOR_9.YB_OnOff = GLASS_EDGE_DETECT_SENSOR_9.XB_OnOff             /**/ = AddressMgr.GetAddress("GLASS_CRACK_DETECT_SENSOR_9", 0);
            GLASS_EDGE_DETECT_SENSOR_10.YB_OnOff = GLASS_EDGE_DETECT_SENSOR_10.XB_OnOff             /**/ = AddressMgr.GetAddress("GLASS_CRACK_DETECT_SENSOR_10", 0);
            ROBOT_ARM_DETECT.YB_OnOff = ROBOT_ARM_DETECT.XB_OnOff                                 /**/ = AddressMgr.GetAddress("ROBOT_ARM_DETECT", 0);
            #endregion

            #region CC-LINK
            //Pre Aligner
            AlignVac.YB_OnOff                                               /**/ = AddressMgr.GetAddress("PRE_ALIGN_VACUUM_SOL_ON", 0);
            AlignVac.XB_OnOff                                               /**/ = AddressMgr.GetAddress("PRE_ALIGN_VAC_PRESURE_STATUS", 0);

            AlignOcrCylinder.YB_ForwardCmd                                  /**/ = AddressMgr.GetAddress("PRE_ALIGN_CYLINDER_UP_DOWN", 0);
            AlignOcrCylinder.XB_ForwardComplete                            /**/ = AddressMgr.GetAddress("PRE_ALIGN_OCR_UP", 0);
            AlignOcrCylinder.XB_BackwardComplete                             /**/ = AddressMgr.GetAddress("PRE_ALIGN_OCR_DOWN", 0);


            EMERGENCY_STOP_1.XB_OnOff = EMERGENCY_STOP_1.YB_OnOff           /**/ = AddressMgr.GetAddress("EMERGENCY_STOP_1", 0);
            EMERGENCY_STOP_2.YB_OnOff = EMERGENCY_STOP_2.XB_OnOff           /**/ = AddressMgr.GetAddress("EMERGENCY_STOP_2", 0);
            EMERGENCY_STOP_3.YB_OnOff = EMERGENCY_STOP_3.XB_OnOff           /**/ = AddressMgr.GetAddress("EMERGENCY_STOP_3", 0);
            EMERGENCY_STOP_4.YB_OnOff = EMERGENCY_STOP_4.XB_OnOff           /**/ = AddressMgr.GetAddress("EMERGENCY_STOP_4", 0);

            WAFER_DETECT_SENSOR_1.XB_OnOff = WAFER_DETECT_SENSOR_1.YB_OnOff /**/ = AddressMgr.GetAddress("WAFER_PIN_DETECT_SENSOR_1", 0);
            WAFER_DETECT_SENSOR_2.XB_OnOff = WAFER_DETECT_SENSOR_2.YB_OnOff /**/ = AddressMgr.GetAddress("WAFER_STAGE_DETECT_SENSOR_1", 0);
            WAFER_DETECT_SENSOR_3.XB_OnOff = WAFER_DETECT_SENSOR_3.YB_OnOff /**/ = AddressMgr.GetAddress("WAFER_PIN_DETECT_SENSOR_2", 0);

            StandCentering1.XB_ForwardComplete                              /**/ = AddressMgr.GetAddress("STANDARD_CENTERING_1_FORWARD_SENSOR", 0);
            StandCentering1.XB_BackwardComplete                             /**/ = AddressMgr.GetAddress("STANDARD_CENTERING_1_BACKWARD_SENSOR", 0);
            StandCentering2.XB_ForwardComplete                              /**/ = AddressMgr.GetAddress("STANDARD_CENTERING_2_FORWARD_SENSOR", 0);            
            StandCentering2.XB_BackwardComplete                             /**/ = AddressMgr.GetAddress("STANDARD_CENTERING_2_BACKWARD_SENSOR", 0);

            AssistCentering1.YB_ForwardCmd                                  /**/ = AddressMgr.GetAddress("ASSISTANCE_CENTERING_1_FORWARD_SOL", 0);           
            AssistCentering1.YB_BackwardCmd                                 /**/ = AddressMgr.GetAddress("ASSISTANCE_CENTERING_1_BACKWARD_SOL", 0);            
            AssistCentering1.XB_ForwardComplete                             /**/ = AddressMgr.GetAddress("ASSISTANCE_CENTERING_1_FORWARD_SENSOR", 0);
            AssistCentering2.XB_ForwardComplete                             /**/ = AddressMgr.GetAddress("ASSISTANCE_CENTERING_2_FORWARD_SENSOR", 0);

            
            AssistCentering1.XB_BackwardComplete                            /**/ = AddressMgr.GetAddress("ASSISTANCE_CENTERING_1_BACKWARD_SENSOR", 0);
            AssistCentering2.XB_BackwardComplete                            /**/ = AddressMgr.GetAddress("ASSISTANCE_CENTERING_2_BACKWARD_SENSOR", 0);

            LiftPin.XB_Up1Complete                                          /**/ = AddressMgr.GetAddress("LIFT_PIN_1_UP_SENSOR", 0);
            LiftPin.XB_Down1Complete                                        /**/ = AddressMgr.GetAddress("LIFT_PIN_1_DOWN_SENSOR", 0);
            LiftPin.XB_Up2Complete                                          /**/ = AddressMgr.GetAddress("LIFT_PIN_2_UP_SENSOR", 0);
            LiftPin.XB_Down2Complete                                        /**/ = AddressMgr.GetAddress("LIFT_PIN_2_DOWN_SENSOR", 0);

            LiftPin.YB_UpCmd                                                /**/ = AddressMgr.GetAddress("LIFT_PIN_1_UP_SOL", 0);
            LiftPin.YB_DownCmd                                              /**/ = AddressMgr.GetAddress("LIFT_PIN_1_DOWN_SOL", 0);

            Vacuum1.YB_OnOff                                                /**/ = AddressMgr.GetAddress("VACUUM_STAGE_SOL_1", 0);
            Vacuum1.XB_OnOff                                                /**/ = AddressMgr.GetAddress("CHECK_VACCUM_1", 0);

            Vacuum2.YB_OnOff                                                /**/ = AddressMgr.GetAddress("VACUUM_STAGE_SOL_2", 0);
            Vacuum2.XB_OnOff                                                /**/ = AddressMgr.GetAddress("CHECK_VACCUM_2", 0);

            Blower1.YB_OnOff                                                /**/ = AddressMgr.GetAddress("BLOWER_STAGE_SOL_1", 0);
            Blower2.YB_OnOff                                                /**/ = AddressMgr.GetAddress("BLOWER_STAGE_SOL_2", 0);



            OHT_LPM1_IN1.XB_OnOff = AddressMgr.GetAddress("LP_1_IN1", 0);
            OHT_LPM1_IN2.XB_OnOff = AddressMgr.GetAddress("LP_1_IN2", 0);
            OHT_LPM1_IN3.XB_OnOff = AddressMgr.GetAddress("LP_1_IN3", 0);
            OHT_LPM1_IN4.XB_OnOff = AddressMgr.GetAddress("LP_1_IN4", 0);
            OHT_LPM1_IN5.XB_OnOff = AddressMgr.GetAddress("LP_1_IN5", 0);
            OHT_LPM1_IN6.XB_OnOff = AddressMgr.GetAddress("LP_1_IN6", 0);
            OHT_LPM1_IN7.XB_OnOff = AddressMgr.GetAddress("LP_1_IN7", 0);
            OHT_LPM1_IN8.XB_OnOff = AddressMgr.GetAddress("LP_1_IN8", 0);

            OHT_LPM2_IN1.XB_OnOff = AddressMgr.GetAddress("LP_2_IN1", 0);
            OHT_LPM2_IN2.XB_OnOff = AddressMgr.GetAddress("LP_2_IN2", 0);
            OHT_LPM2_IN3.XB_OnOff = AddressMgr.GetAddress("LP_2_IN3", 0);
            OHT_LPM2_IN4.XB_OnOff = AddressMgr.GetAddress("LP_2_IN4", 0);
            OHT_LPM2_IN5.XB_OnOff = AddressMgr.GetAddress("LP_2_IN5", 0);
            OHT_LPM2_IN6.XB_OnOff = AddressMgr.GetAddress("LP_2_IN6", 0);
            OHT_LPM2_IN7.XB_OnOff = AddressMgr.GetAddress("LP_2_IN7", 0);
            OHT_LPM2_IN8.XB_OnOff = AddressMgr.GetAddress("LP_2_IN8", 0);

            OHT_LPM1_OUT1.XB_OnOff = AddressMgr.GetAddress("LP_1_OUT1", 0);
            OHT_LPM1_OUT2.XB_OnOff = AddressMgr.GetAddress("LP_1_OUT2", 0);
            OHT_LPM1_OUT3.XB_OnOff = AddressMgr.GetAddress("LP_1_OUT3", 0);
            OHT_LPM1_OUT4.XB_OnOff = AddressMgr.GetAddress("LP_1_OUT4", 0);
            OHT_LPM1_OUT5.XB_OnOff = AddressMgr.GetAddress("LP_1_OUT5", 0);
            OHT_LPM1_OUT6.XB_OnOff = AddressMgr.GetAddress("LP_1_OUT6", 0);
            OHT_LPM1_OUT7.XB_OnOff = AddressMgr.GetAddress("LP_1_OUT7", 0);
            OHT_LPM1_OUT8.XB_OnOff = AddressMgr.GetAddress("LP_1_OUT8", 0);

            OHT_LPM2_OUT1.XB_OnOff = AddressMgr.GetAddress("LP_2_OUT1", 0);
            OHT_LPM2_OUT2.XB_OnOff = AddressMgr.GetAddress("LP_2_OUT2", 0);
            OHT_LPM2_OUT3.XB_OnOff = AddressMgr.GetAddress("LP_2_OUT3", 0);
            OHT_LPM2_OUT4.XB_OnOff = AddressMgr.GetAddress("LP_2_OUT4", 0);
            OHT_LPM2_OUT5.XB_OnOff = AddressMgr.GetAddress("LP_2_OUT5", 0);
            OHT_LPM2_OUT6.XB_OnOff = AddressMgr.GetAddress("LP_2_OUT6", 0);
            OHT_LPM2_OUT7.XB_OnOff = AddressMgr.GetAddress("LP_2_OUT7", 0);
            OHT_LPM2_OUT8.XB_OnOff = AddressMgr.GetAddress("LP_2_OUT8", 0);

            #endregion            

            PMac.YB_EquipMode                     /**/ = AddressMgr.GetAddress("PMAC_YB_EquipState", 0);
            PMac.YB_CheckAlarmStatus              /**/  = AddressMgr.GetAddress("PMAC_YB_EquipState", 1);
            PMac.YB_UpperInterfaceWorking         /**/  = AddressMgr.GetAddress("PMAC_YB_EquipState", 2);
            PMac.YB_LowerInterfaceWorking         /**/  = AddressMgr.GetAddress("PMAC_YB_EquipState", 3);

            PMac.XB_PmacReady                     /**/  = AddressMgr.GetAddress("PMAC_XB_PmacState", 0);
            PMac.XB_PmacAlive                     /**/  = AddressMgr.GetAddress("PMAC_XB_PmacState", 1);
            PMac.XB_PmacReviewGantryCrashCaution        = AddressMgr.GetAddress("PMAC_XB_PmacState", 3);

            PMac.YB_EquipStatusMotorInterlock           = AddressMgr.GetAddress("PMAC_YB_CommonCmd", 0);          
            PMac.YB_ReviewTimerOverCmd                  = AddressMgr.GetAddress("PMAC_YB_CommonCmd", 1);
            PMac.YB_PmacResetCmd                        = AddressMgr.GetAddress("PMAC_YB_CommonCmd", 2);

            PMac.XB_EquipStatusMotorInterlockAck        = AddressMgr.GetAddress("PMAC_XB_CommonAck", 0);
            PMac.XB_ReviewTimerOverCmdAck               = AddressMgr.GetAddress("PMAC_XB_CommonAck", 1);
            PMac.XB_PmacResetCmdAck                     = AddressMgr.GetAddress("PMAC_XB_CommonAck", 2);


            int[] motorNums = new int[] { 
                1,
                2,
                5,
            };
            for (int jPos = 0; jPos < Motors.Length; jPos++)
            {
                int axisNo = motorNums[jPos];
                string axisStr = string.Format("Axis{0:D2}", axisNo);

                Motors[jPos].XB_StatusHomeCompleteBit     = AddressMgr.GetAddress(string.Format("{0}_XB_HomeComplete", axisStr), axisNo - 1);
                Motors[jPos].XB_StatusHomeInPosition      = AddressMgr.GetAddress(string.Format("{0}_XB_HomePositionOn", axisStr), axisNo - 1);
                Motors[jPos].XB_StatusMotorMoving         = AddressMgr.GetAddress(string.Format("{0}_XB_Moving", axisStr), axisNo - 1);
                Motors[jPos].XB_StatusNegativeLimitSet    = AddressMgr.GetAddress(string.Format("{0}_XB_HWMinusLimit", axisStr), axisNo - 1);
                Motors[jPos].XB_StatusPositiveLimitSet    = AddressMgr.GetAddress(string.Format("{0}_XB_HWPlusLimit", axisStr), axisNo - 1);
                                                    
                                                    
                Motors[jPos].XB_ErrMotorServoOn           = AddressMgr.GetAddress(string.Format("{0}_XB_ServoON", axisStr), axisNo - 1);
                Motors[jPos].XB_ErrFatalFollowingError    = AddressMgr.GetAddress(string.Format("{0}_XB_FatalFollowingError", axisStr), axisNo - 1);
                Motors[jPos].XB_ErrAmpFaultError          = AddressMgr.GetAddress(string.Format("{0}_XB_AmpFaultError", axisStr), axisNo - 1);
                Motors[jPos].XB_ErrI2TAmpFaultError       = AddressMgr.GetAddress(string.Format("{0}_XB_I2TAmpFaultError", axisStr), axisNo - 1);
                Motors[jPos].XB_TargetMoveComplete[0]     = AddressMgr.GetAddress(string.Format("{0}_XB_TargetPosMoveComplete", axisStr), axisNo - 1);

                Motors[jPos].YB_HomeCmd                   = AddressMgr.GetAddress(string.Format("{0}_YB_HomeCmd", axisStr), axisNo - 1);
                Motors[jPos].YB_TargetMoveCmd[0]          = AddressMgr.GetAddress(string.Format("{0}_YB_TargetPosMoveCmd", axisStr), axisNo - 1);
                Motors[jPos].YB_MotorJogMinusMove         = AddressMgr.GetAddress(string.Format("{0}_YB_JogMinusCmd", axisStr), axisNo - 1);
                Motors[jPos].YB_MotorJogPlusMove          = AddressMgr.GetAddress(string.Format("{0}_YB_JogPlusCmd", axisStr), axisNo - 1);
                Motors[jPos].YF_MotorJogSpeedCmd          = AddressMgr.GetAddress(string.Format("{0}_YF_JogSpeed", axisStr), axisNo - 1);
                Motors[jPos].XF_MotorJogSpeedAck          = AddressMgr.GetAddress(string.Format("{0}_XF_JogSpeed", axisStr), axisNo - 1);

                Motors[jPos].XB_HomeCmdAck                = AddressMgr.GetAddress(string.Format("{0}_XB_HomeAck", axisStr), axisNo - 1);
                Motors[jPos].XB_TargetMoveAck[0]          = AddressMgr.GetAddress(string.Format("{0}_XB_TargetPosMoveAck", axisStr), axisNo - 1);

                Motors[jPos].XF_CurrMotorPosition         = AddressMgr.GetAddress(string.Format("{0}_XF_CurPosition", axisStr));
                Motors[jPos].XF_CurrMotorSpeed            = AddressMgr.GetAddress(string.Format("{0}_XF_CurSpeed", axisStr));
                Motors[jPos].XF_CurrMotorStress           = AddressMgr.GetAddress(string.Format("{0}_XF_CurToque", axisStr));                
                                
                Motors[jPos].YF_TargetPosition[0]         = AddressMgr.GetAddress(string.Format("{0}_YF_TargetPosition", axisStr));
                Motors[jPos].YF_Position0Speed[0]         = AddressMgr.GetAddress(string.Format("{0}_YF_TargetSpeed", axisStr));
                Motors[jPos].YI_Position0Accel[0]         = AddressMgr.GetAddress(string.Format("{0}_YF_TargetAccTime", axisStr));

                Motors[jPos].XF_Position0PosiAck[0]       = AddressMgr.GetAddress(string.Format("{0}_XF_TargetPosition", axisStr));            
                Motors[jPos].XF_Position0SpeedAck[0]      = AddressMgr.GetAddress(string.Format("{0}_XF_TargetSpeed", axisStr));                
                Motors[jPos].XI_Position0AccelAck[0]      = AddressMgr.GetAddress(string.Format("{0}_XF_TargetAccTime", axisStr));
            }
            /// PioA2I
            #region EziStepMotor
            foreach (var motor in StepMotors)
            {
                int slaveNo = motor.SlaveNo;

                motor.XB_StatusHomeCompleteBit = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusHomeCompleteBit", slaveNo));
                motor.XB_StatusMotorMoving = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusMotorMoving", slaveNo));
                motor.XB_StatusMinusLimitSet = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusMinusLimitSet", slaveNo));
                motor.XB_StatusPlusLimitSet = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusPlusLimitSet", slaveNo));
                motor.XB_StatusMotorServoOn = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusMotorServoOn", slaveNo));
                motor.XB_StatusHomeInPosition = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusHomeInPosition", slaveNo));
                motor.XB_StatusMotorInPosition[0] = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusMotorInPosition", slaveNo));

                motor.XF_CurrMotorPosition = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_CurrMotorPosition", slaveNo));
                motor.XF_CurrMotorSpeed = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_CurrMotorSpeed", slaveNo));
                //motor.XF_CurrMotorAccel = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_CurrMotorAccel", slaveNo));

                //motor.YB_MotorStopCmd = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YB_MotorStopCmd", slaveNo));
                //motor.XB_MotorStopCmdAck = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_MotorStopCmdAck", slaveNo));
                motor.YB_HomeCmd = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YB_HomeCmd", slaveNo));
                motor.XB_HomeCmdAck = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_HomeCmdAck", slaveNo));

                motor.YB_MotorJogMinusMove = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YB_MotorJogMinusMove", slaveNo));
                motor.YB_MotorJogPlusMove = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YB_MotorJogPlusMove", slaveNo));
                motor.YF_MotorJogSpeedCmd = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YF_MotorJogSpeedCmd", slaveNo));
                motor.XF_MotorJogSpeedCmdAck = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_MotorJogSpeedCmdAck", slaveNo));

                motor.YB_PTPMoveCmd[0] = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YB_PTPMoveCmd", slaveNo));
                motor.XB_PTPMoveCmdAck[0] = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_PTPMoveCmdAck", slaveNo));
                motor.YF_PTPMovePosition[0] = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YF_PTPMovePosition", slaveNo));
                motor.XF_PTPMovePositionAck[0] = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_PTPMovePositionAck", slaveNo));
                motor.YF_PTPMoveSpeed[0] = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YF_PTPMoveSpeed", slaveNo));
                motor.XF_PTPMoveSpeedAck[0] = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_PTPMoveSpeedAck", slaveNo));
                motor.YF_PTPMoveAccel[0] = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YF_PTPMoveAccel", slaveNo));
                motor.XF_PTPMoveAccelAck[0] = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_PTPMoveAccelAck", slaveNo));
            }
            #endregion
            InitPioA2I();
            InitPioI2A();

            //검사기 ADDRESS
            //S0.0
            InspPc.YB_ControlAlive	        /**/ = AddressMgr.GetAddress("C2A_CONTROL_ALIVE", 0);
            InspPc.YB_RunningMode	        /**/ = AddressMgr.GetAddress("C2A_RUNNING_MODE", 0);
            InspPc.YB_ReviewMode	        /**/ = AddressMgr.GetAddress("C2A_REVIEW_MODE", 0);
            InspPc.YB_ReviewManualMove	    /**/ = AddressMgr.GetAddress("C2A_REVIEW_MANUAL_MOVE", 0);
            InspPc.YB_BypassMode	        /**/ = AddressMgr.GetAddress("C2A_BYPASS_MODE", 0);
            InspPc.YB_VaripanelMode	        /**/ = AddressMgr.GetAddress("C2A_VARIPANEL_MODE", 0);
            InspPc.YB_DualMode	            /**/ = AddressMgr.GetAddress("C2A_DUAL_MODE", 0);
            InspPc.YB_PpidUseMode	        /**/ = AddressMgr.GetAddress("C2A_PPID_USE_MODE", 0);

            //S10.0
            InspPc.YB_Loading	            /**/ = AddressMgr.GetAddress("C2I_LOADING", 0);
            InspPc.YB_ScanReady	            /**/ = AddressMgr.GetAddress("C2I_SCAN_READY", 0);
            InspPc.YB_ScanStart	            /**/ = AddressMgr.GetAddress("C2I_SCAN_START", 0);
            InspPc.YB_ScanEnd	            /**/ = AddressMgr.GetAddress("C2I_SCAN_END", 0);
            InspPc.YB_Unloading	            /**/ = AddressMgr.GetAddress("C2I_UNLOADING", 0);
            InspPc.YB_InspectionEnd	        /**/ = AddressMgr.GetAddress("C2I_INSPECTION_END", 0);
            InspPc.YB_LotStart	            /**/ = AddressMgr.GetAddress("C2I_LOT_START", 0);
            InspPc.YB_LotEnd	            /**/ = AddressMgr.GetAddress("C2I_LOT_END", 0);
            InspPc.YB_Z1HomePos	            /**/ = AddressMgr.GetAddress("C2I_Z1_HOME_POS", 0);
            InspPc.YB_Z2HomePos	            /**/ = AddressMgr.GetAddress("C2I_Z2_HOME_POS", 0);
            InspPc.YB_Z1ZeroPos	            /**/ = AddressMgr.GetAddress("C2I_Z1_ZERO_POS", 0);
            InspPc.YB_Z2ZeroPos	            /**/ = AddressMgr.GetAddress("C2I_Z2_ZERO_POS", 0);
            InspPc.YB_Z1Jog	                /**/ = AddressMgr.GetAddress("C2I_Z1_JOG", 0);
            InspPc.YB_Z2Jog	                /**/ = AddressMgr.GetAddress("C2I_Z2_JOG", 0);
            InspPc.YB_Z1PosMove	            /**/ = AddressMgr.GetAddress("C2I_Z1_POS_MOVE", 0);
            InspPc.YB_Z2PosMove	            /**/ = AddressMgr.GetAddress("C2I_Z2_POS_MOVE", 0);
            InspPc.YB_InspectionCompleteAck	    /**/ = AddressMgr.GetAddress("C2I_Z1_INPOS_COMPLETE", 0);
            InspPc.YB_RpcPpidChange	        /**/ = AddressMgr.GetAddress("C2I_RPC_PPID_CHANGE", 0);
            InspPc.YB_NewPpid	            /**/ = AddressMgr.GetAddress("C2I_NEW_PPID", 0);
            InspPc.YB_JudgeCompleteAck	    /**/ = AddressMgr.GetAddress("C2I_JUDGE_COMPLETE", 0);

            //S1000.0
            InspPc.XB_InspectionAlive	    /**/ = AddressMgr.GetAddress("I2C_INSPECTION_ALIVE", 0);
            InspPc.XB_LoadingSuccess	    /**/ = AddressMgr.GetAddress("I2C_LOADING_SUCCESS", 0);
            InspPc.XB_LoadingFail	        /**/ = AddressMgr.GetAddress("I2C_LOADING_FAIL", 0);
            InspPc.XB_ScanReadySuccess	    /**/ = AddressMgr.GetAddress("I2C_SCAN_READY_SUCCESS", 0);
            InspPc.XB_ScanReadyFail	        /**/ = AddressMgr.GetAddress("I2C_SCAN_READY_FAIL", 0);
            InspPc.XB_ScanStartSuccess	    /**/ = AddressMgr.GetAddress("I2C_SCAN_START_SUCCESS", 0);
            InspPc.XB_ScanStartFail	        /**/ = AddressMgr.GetAddress("I2C_SCAN_START_FAIL", 0);
            InspPc.XB_ScanEndSuccess	    /**/ = AddressMgr.GetAddress("I2C_SCAN_END_SUCCESS", 0);
            InspPc.XB_ScanEndFail	        /**/ = AddressMgr.GetAddress("I2C_SCAN_END_FAIL", 0);
            InspPc.XB_UnloadingSuccess	    /**/ = AddressMgr.GetAddress("I2C_UNLOADING_SUCCESS", 0);
            InspPc.XB_UnloadingFail	        /**/ = AddressMgr.GetAddress("I2C_UNLOADING_FAIL", 0);
            InspPc.XB_LotStartSuccess	    /**/ = AddressMgr.GetAddress("I2C_LOT_START_SUCCESS", 0);
            InspPc.XB_LotStartFail	        /**/ = AddressMgr.GetAddress("I2C_LOT_START_FAIL", 0);
            InspPc.XB_LotEndSuccess	        /**/ = AddressMgr.GetAddress("I2C_LOT_END_SUCCESS", 0);
            InspPc.XB_LotEndFail	        /**/ = AddressMgr.GetAddress("I2C_LOT_END_FAIL", 0);
            InspPc.XB_ZxisMoveStart	        /**/ = AddressMgr.GetAddress("I2C_ZXIS_MOVE_START", 0);
            InspPc.XB_InspectionComplete	/**/ = AddressMgr.GetAddress("I2C_INSPECTION_COMPLETE", 0);
            InspPc.XB_JudgeComplete	        /**/ = AddressMgr.GetAddress("I2C_JUDGE_COMPLETE", 0);

            //S1500.0
            InspPc.XB_InspectorError	    /**/ = AddressMgr.GetAddress("I2C_INSPECTOR_ERROR", 0);
            InspPc.XB_PreAlignError	        /**/ = AddressMgr.GetAddress("I2C_PRE_ALIGN", 0);
            InspPc.XB_ServerOverflowError	/**/ = AddressMgr.GetAddress("I2C_SERVER_OVERFLOW", 0);
            InspPc.XB_InspectOverflowError	/**/ = AddressMgr.GetAddress("I2C_INSPECT_OVERFLOW", 0);
            InspPc.XB_StackLoadingFailError	/**/ = AddressMgr.GetAddress("I2C_STACK_LOADING_FAIL", 0);
            InspPc.XB_CommonDefectError	    /**/ = AddressMgr.GetAddress("I2C_COMMON_DEFECT", 0);
            InspPc.XB_MaskDefectError	    /**/ = AddressMgr.GetAddress("I2C_MASK_DEFECT", 0);
            InspPc.XB_EdgeCrackError	    /**/ = AddressMgr.GetAddress("I2C_EDGE_CRACK", 0);
            InspPc.XB_NoRecipeError	        /**/ = AddressMgr.GetAddress("I2C_NO_RECIPE", 0);
            InspPc.XB_FindEdgeFailError	    /**/ = AddressMgr.GetAddress("I2C_FIND_EDGE_FAIL", 0);
            InspPc.XB_LightErrorError	    /**/ = AddressMgr.GetAddress("I2C_LIGHT_ERROR", 0);
            InspPc.XB_UpperLimitError	    /**/ = AddressMgr.GetAddress("I2C_UPPER_LIMIT", 0);
            InspPc.XB_LowestLimitError	    /**/ = AddressMgr.GetAddress("I2C_LOWEST_LIMIT", 0);
            InspPc.XB_ZAxisFailError	    /**/ = AddressMgr.GetAddress("I2C_ZAXIS_FAIL", 0);
            InspPc.XB_NoImageError	        /**/ = AddressMgr.GetAddress("I2C_NO_IMAGE", 0);
            InspPc.XB_GlassDetectFailError	/**/ = AddressMgr.GetAddress("I2C_GLASS_DETECT_FAIL", 0);

            //추가 필요..
            InspPc.YI_ScanIndex	            /**/ = AddressMgr.GetAddress("C2I_F_ScanIndex", 0);
            InspPc.YI_ScanCount	            /**/ = AddressMgr.GetAddress("C2I_F_ScanCount", 0);

            InspPc.XF_Z1Pos	                /**/ = AddressMgr.GetAddress("I2C_F_Z1Pos", 0);
            InspPc.XF_Z1Speed	            /**/ = AddressMgr.GetAddress("I2C_F_Z1Speed", 0);
            InspPc.XF_Z1JogSpeed	        /**/ = AddressMgr.GetAddress("I2C_F_Z1JogSpeed", 0);
            InspPc.XF_Z2Pos	                /**/ = AddressMgr.GetAddress("I2C_F_Z2Pos", 0);
            InspPc.XF_Z2Speed	            /**/ = AddressMgr.GetAddress("I2C_F_Z2Speed", 0);
            InspPc.XF_Z2JogSpeed	        /**/ = AddressMgr.GetAddress("I2C_F_Z2JogSpeed", 0);

            //리뷰 ADDRESS
            ReviPc.YB_Loading	            /**/ = AddressMgr.GetAddress("C2R_LOADING", 0);
            ReviPc.YB_AlignStart	        /**/ = AddressMgr.GetAddress("C2R_ALIGN_START", 0);
            ReviPc.YB_ReviewStart	        /**/ = AddressMgr.GetAddress("C2R_REVIEW_START", 0);
            ReviPc.YB_ReviewTimeOver	    /**/ = AddressMgr.GetAddress("C2R_REVIEW_TIME_OVER", 0);
            ReviPc.YB_Unloading	            /**/ = AddressMgr.GetAddress("C2R_UNLOADING", 0);
            ReviPc.YB_LotStart	            /**/ = AddressMgr.GetAddress("C2R_LOT_START", 0);
            ReviPc.YB_LotEnd	            /**/ = AddressMgr.GetAddress("C2R_LOT_END", 0);
            ReviPc.YB_ReviewCompleteAck     /**/ = AddressMgr.GetAddress("C2R_REVIEW_COMPLETE_ACK", 0);

            ReviPc.XB_OnFocus	            /**/ = AddressMgr.GetAddress("R2C_ON_FOCUS", 0);
            ReviPc.XB_LoadingAck	        /**/ = AddressMgr.GetAddress("R2C_LOADING_ACK", 0);
            ReviPc.XB_AlignStartAck	        /**/ = AddressMgr.GetAddress("R2C_ALIGN_START_ACK", 0);
            ReviPc.XB_ReviewStartAck	    /**/ = AddressMgr.GetAddress("R2C_REVIEW_START_ACK", 0);
            ReviPc.XB_ReviewTimeOverAck	        /**/ = AddressMgr.GetAddress("R2C_TIME_OVER_ACK", 0);
            ReviPc.XB_UnloadingAck	        /**/ = AddressMgr.GetAddress("R2C_UNLOADING_ACK", 0);
            ReviPc.XB_LotStartAck	        /**/ = AddressMgr.GetAddress("R2C_LOT_START_ACK", 0);
            ReviPc.XB_LotEndAck	            /**/ = AddressMgr.GetAddress("R2C_LOT_END_ACK", 0);
            ReviPc.XB_ReviewComplete	    /**/ = AddressMgr.GetAddress("R2C_REVIEW_COMPLETE", 0);

            ReviPc.XB_LoadingFailError	    /**/ = AddressMgr.GetAddress("R2C_LOADING_FAIL", 0);
            ReviPc.XB_AlignFailError	    /**/ = AddressMgr.GetAddress("R2C_ALIGN_FAIL", 0);
            ReviPc.XB_ReviewFailError	    /**/ = AddressMgr.GetAddress("R2C_REVIEW_FAIL", 0);
            ReviPc.XB_UnloadingFailError	/**/ = AddressMgr.GetAddress("R2C_UNLOADING_FAIL", 0);


            //추가 필요..
            InspPc.YI_ScanIndex	            /**/ = AddressMgr.GetAddress("C2I_F_ScanIndex", 0);
            InspPc.YI_ScanCount	            /**/ = AddressMgr.GetAddress("C2I_F_ScanCount", 0);

            InspPc.XF_Z1Pos	                /**/ = AddressMgr.GetAddress("I2C_F_Z1Pos", 0);
            InspPc.XF_Z1Speed	            /**/ = AddressMgr.GetAddress("I2C_F_Z1Speed", 0);
            InspPc.XF_Z1JogSpeed	        /**/ = AddressMgr.GetAddress("I2C_F_Z1JogSpeed", 0);
            InspPc.XF_Z2Pos	                /**/ = AddressMgr.GetAddress("I2C_F_Z2Pos", 0);
            InspPc.XF_Z2Speed	            /**/ = AddressMgr.GetAddress("I2C_F_Z2Speed", 0);
            InspPc.XF_Z2JogSpeed	        /**/ = AddressMgr.GetAddress("I2C_F_Z2JogSpeed", 0);

            Console.WriteLine("== End SetAddress ==");
        }


        private bool _isFirtst = true;
        public void Working()
        {
            try
            {
                //GLASS_DETECT_SENSOR_3.YB_OnOff.vBit = !GLASS_DETECT_SENSOR_3.XB_OnOff.vBit; //jys:: 강제 On 테스트
                //if (InterLock)
                //    CheckInterLock();
                if (_isFirtst == true)
                {
                    ROBOT_ARM_DETECT.OnOff(true);

                    DOOR01_SENSOR.OnOff(false);
                    DOOR02_SENSOR.OnOff(false);
                    DOOR03_SENSOR.OnOff(false);
                    DOOR04_SENSOR.OnOff(false);

                    IsEFEMInputArm.OnOff(true);
                    IsEFEMAlignerInputArm.OnOff(true);

                    LiftPin.XB_Down1Complete.vBit = true;
                    LiftPin.XB_Down2Complete.vBit = true;

                    AlignOcrCylinder.XB_ForwardComplete.vBit = true;

                    StandCentering1.XB_BackwardComplete.vBit = true;
                    StandCentering2.XB_BackwardComplete.vBit = true;

                    _isFirtst = false;
                }

                ReadFromPLC();

                foreach (ServoSimulUmac motor in Motors)
                    motor.LogicWorking();
                foreach (StepMotorSimul stpmotor in StepMotors)
                {
                    stpmotor.LogicWorking();
                }
                //////////////////////////////////////////
                EMERGENCY_STOP_1.LogicWorking();
                EMERGENCY_STOP_2.LogicWorking();

                WAFER_DETECT_SENSOR_1.LogicWorking();
                WAFER_DETECT_SENSOR_2.LogicWorking();
                WAFER_DETECT_SENSOR_3.LogicWorking();

                Vacuum1.LogicWorking();
                Vacuum2.LogicWorking();
                Blower1.LogicWorking();
                Blower2.LogicWorking();

                StandCentering1.LogicWorking();
                StandCentering2.LogicWorking();
                LiftPin.LogicWorking();

                AlignOcrCylinder.LogicWorking();
                AlignVac.LogicWorking();

                //====================================
                PioI2A.LogPioSend(this);
                PioA2I.LogPioRecv(this);
                PMac.LogicWorking();

                OHT_LPM1_IN1.LogicWorking();
                OHT_LPM1_IN2.LogicWorking();
                OHT_LPM1_IN3.LogicWorking();
                OHT_LPM1_IN4.LogicWorking();
                OHT_LPM1_IN5.LogicWorking();
                OHT_LPM1_IN6.LogicWorking();
                OHT_LPM1_IN7.LogicWorking();
                OHT_LPM1_IN8.LogicWorking();
                OHT_LPM2_IN1.LogicWorking();
                OHT_LPM2_IN2.LogicWorking();
                OHT_LPM2_IN3.LogicWorking();
                OHT_LPM2_IN4.LogicWorking();
                OHT_LPM2_IN5.LogicWorking();
                OHT_LPM2_IN6.LogicWorking();
                OHT_LPM2_IN7.LogicWorking();
                OHT_LPM2_IN8.LogicWorking();
                //OHT_LPM1_OUT1.LogicWorking();
                //OHT_LPM1_OUT2.LogicWorking();
                //OHT_LPM1_OUT3.LogicWorking();
                //OHT_LPM1_OUT4.LogicWorking();
                //OHT_LPM1_OUT5.LogicWorking();
                //OHT_LPM1_OUT6.LogicWorking();
                //OHT_LPM1_OUT7.LogicWorking();
                //OHT_LPM1_OUT8.LogicWorking();
                //OHT_LPM2_OUT1.LogicWorking();
                //OHT_LPM2_OUT2.LogicWorking();
                //OHT_LPM2_OUT3.LogicWorking();
                //OHT_LPM2_OUT4.LogicWorking();
                //OHT_LPM2_OUT5.LogicWorking();
                //OHT_LPM2_OUT6.LogicWorking();
                //OHT_LPM2_OUT7.LogicWorking();
                //OHT_LPM2_OUT8.LogicWorking();

                ////MODE_SELECT_KEY_SW_AUTOTEACH.LogicWorking();

                DOOR01_SENSOR.LogicWorking();
                DOOR02_SENSOR.LogicWorking();
                DOOR03_SENSOR.LogicWorking();
                DOOR04_SENSOR.LogicWorking();

                //ROBOT_ARM_DETECT.LogicWorking();

                ISOLATOR_1_ALARM.LogicWorking();
                ISOLATOR_2_ALARM.LogicWorking();
                ISOLATOR_3_ALARM.LogicWorking();
                ISOLATOR_4_ALARM.LogicWorking();

                //InspPC.LogicWorking();
                //RevPc.LogicWorking();



                WriteToPLC();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void CheckInterLock()
        {
            Logger.Log.AppendLine(LogLevel.Error, "");
        }

        private void ReadFromPLC()
        {
            GG.HSMS.ReadFromPLC(PioHandShake.AOI_AREA, PioHandShake.AOI_AREA.Length);
        }
        private void WriteToPLC()
        {
            GG.HSMS.WriteToPLC(PioHandShake.CIM_AREA, PioHandShake.CIM_AREA.Length);
        }

        private void InitPioI2A()
        {
            #region AOIB Up의 주소를 매핑
            //HAND SHAKE TO UPPER
            PioI2A.LoHeartBit                    /*  */ = new PlcAddr(PlcMemType.S, 0, 0x0);
            PioI2A.LoMachinePause                /*  */ = new PlcAddr(PlcMemType.S, 0, 0x1);
            PioI2A.LoMachineDown                 /*  */ = new PlcAddr(PlcMemType.S, 0, 0x2);
            PioI2A.LoMachineAlarm                /*  */ = new PlcAddr(PlcMemType.S, 0, 0x3);
            PioI2A.LoReceiveAble                 /*  */ = new PlcAddr(PlcMemType.S, 0, 0x4);
            PioI2A.LoReceiveStart                /*  */ = new PlcAddr(PlcMemType.S, 0, 0x5);
            PioI2A.LoReceiveComplete             /*  */ = new PlcAddr(PlcMemType.S, 0, 0x6);
            PioI2A.LoExchangeFlag                /*  */ = new PlcAddr(PlcMemType.S, 0, 0x7);
            PioI2A.LoReturnSendStart             /*  */ = new PlcAddr(PlcMemType.S, 1, 0x0);
            PioI2A.LoReturnSendComplete          /*  */ = new PlcAddr(PlcMemType.S, 1, 0x1);
            PioI2A.LoImmediatelyPauseRequest     /*  */ = new PlcAddr(PlcMemType.S, 1, 0x2);
            PioI2A.LoImmediatelyStopRequest      /*  */ = new PlcAddr(PlcMemType.S, 1, 0x3);
            PioI2A.LoReceiveAbleRemainedStep1    /*  */ = new PlcAddr(PlcMemType.S, 1, 0x4);
            PioI2A.LoReceiveAbleRemainedStep2    /*  */ = new PlcAddr(PlcMemType.S, 1, 0x5);
            PioI2A.LoReceiveAbleRemainedStep3    /*  */ = new PlcAddr(PlcMemType.S, 1, 0x6);
            PioI2A.LoReceiveAbleRemainedStep4    /*  */ = new PlcAddr(PlcMemType.S, 1, 0x7);
            PioI2A.LoGlassIDReadComplete         /*  */ = new PlcAddr(PlcMemType.S, 2, 0x0);
            PioI2A.LoLoadingStop                 /*  */ = new PlcAddr(PlcMemType.S, 2, 0x1);
            PioI2A.LoTransferStop                /*  */ = new PlcAddr(PlcMemType.S, 2, 0x2);
            PioI2A.LoExchangeFailFlag            /*  */ = new PlcAddr(PlcMemType.S, 2, 0x3);
            PioI2A.LoProcessTimeUp               /*  */ = new PlcAddr(PlcMemType.S, 2, 0x4);
            PioI2A.LoReserved1	            /*  */      = new PlcAddr(PlcMemType.S, 2, 0x5);
            PioI2A.LoReserved2	            /*  */      = new PlcAddr(PlcMemType.S, 2, 0x6);
            PioI2A.LoReceiveAbleReserveRequest   /*  */ = new PlcAddr(PlcMemType.S, 2, 0x7);
            PioI2A.LoHandShakeCancelRequest      /*  */ = new PlcAddr(PlcMemType.S, 3, 0x0);
            PioI2A.LoHandShakeAbortRequest       /*  */ = new PlcAddr(PlcMemType.S, 3, 0x1);
            PioI2A.LoHandShakeResumeRequest      /*  */ = new PlcAddr(PlcMemType.S, 3, 0x2);
            PioI2A.LoHandShakeRecoveryAckReply   /*  */ = new PlcAddr(PlcMemType.S, 3, 0x3);
            PioI2A.LoHandShakeRecoveryNakReply   /*  */ = new PlcAddr(PlcMemType.S, 3, 0x4);
            PioI2A.LoReceiveJobReady             /*  */ = new PlcAddr(PlcMemType.S, 3, 0x5);
            PioI2A.LoReceiveActionMove           /*  */ = new PlcAddr(PlcMemType.S, 3, 0x6);
            PioI2A.LoReceiveActionRemove         /*  */ = new PlcAddr(PlcMemType.S, 3, 0x7);
            //Upper	Contact	Point	        
            PioI2A.LoAbnormal                    /*  */ = new PlcAddr(PlcMemType.S, 4, 0x0);
            PioI2A.LoTypeofArm                   /*  */ = new PlcAddr(PlcMemType.S, 4, 0x1);
            PioI2A.LoTypeofStageConveyor         /*  */ = new PlcAddr(PlcMemType.S, 4, 0x2);
            PioI2A.LoArmStretchUpMoving          /*  */ = new PlcAddr(PlcMemType.S, 4, 0x3);
            PioI2A.LoArmStretchUpComplete        /*  */ = new PlcAddr(PlcMemType.S, 4, 0x4);
            PioI2A.LoArmStretchDownMoving        /*  */ = new PlcAddr(PlcMemType.S, 4, 0x5);
            PioI2A.LoArmStretchDownComplete      /*  */ = new PlcAddr(PlcMemType.S, 4, 0x6);
            PioI2A.LoArmStretching               /*  */ = new PlcAddr(PlcMemType.S, 4, 0x7);
            PioI2A.LoArmStretchComplete          /*  */ = new PlcAddr(PlcMemType.S, 5, 0x0);
            PioI2A.LoArmFolding                  /*  */ = new PlcAddr(PlcMemType.S, 5, 0x1);
            PioI2A.LoArmFoldComplete             /*  */ = new PlcAddr(PlcMemType.S, 5, 0x2);
            PioI2A.LoCpReserved1                  /*  */= new PlcAddr(PlcMemType.S, 5, 0x3);
            PioI2A.LoCpReserved2                  /*  */= new PlcAddr(PlcMemType.S, 5, 0x4);
            PioI2A.LoArm1Folded                  /*  */ = new PlcAddr(PlcMemType.S, 5, 0x5);
            PioI2A.LoArm2Folded                  /*  */ = new PlcAddr(PlcMemType.S, 5, 0x6);
            PioI2A.LoArm1GlassDetect             /*  */ = new PlcAddr(PlcMemType.S, 5, 0x7);
            PioI2A.LoArm2GlassDetect             /*  */ = new PlcAddr(PlcMemType.S, 6, 0x0);
            PioI2A.LoArm1GlassVacuum             /*  */ = new PlcAddr(PlcMemType.S, 6, 0x1);
            PioI2A.LoArm2GlassVacuum             /*  */ = new PlcAddr(PlcMemType.S, 6, 0x2);
            PioI2A.LoRobotDirection              /*  */ = new PlcAddr(PlcMemType.S, 6, 0x3);
            PioI2A.LoManualOperation             /*  */ = new PlcAddr(PlcMemType.S, 6, 0x4);
            PioI2A.LoPinUp                       /*  */ = new PlcAddr(PlcMemType.S, 6, 0x5);
            PioI2A.LoPinDown                     /*  */ = new PlcAddr(PlcMemType.S, 6, 0x6);
            PioI2A.LoDoorOpen                    /*  */ = new PlcAddr(PlcMemType.S, 6, 0x7);
            PioI2A.LoDoorClose                   /*  */ = new PlcAddr(PlcMemType.S, 7, 0x0);
            PioI2A.LoGlassDetect                 /*  */ = new PlcAddr(PlcMemType.S, 7, 0x1);
            PioI2A.LoBodyMoving                  /*  */ = new PlcAddr(PlcMemType.S, 7, 0x2);
            PioI2A.LoBodyOriginPosition          /*  */ = new PlcAddr(PlcMemType.S, 7, 0x3);
            PioI2A.LoEmergency                   /*  */ = new PlcAddr(PlcMemType.S, 7, 0x4);
            PioI2A.LoVertical                    /*  */ = new PlcAddr(PlcMemType.S, 7, 0x5);
            PioI2A.LoHorizontal                  /*  */ = new PlcAddr(PlcMemType.S, 7, 0x6);
            #endregion
            #region CIMAB Lo의 주소를 매핑
            //Lower	EQ						         
            PioI2A.UpHeartBit                    /*  */ = new PlcAddr(PlcMemType.S, 10008, 0x0);
            PioI2A.UpMachinePause                /*  */ = new PlcAddr(PlcMemType.S, 10008, 0x1);
            PioI2A.UpMachineDown                 /*  */ = new PlcAddr(PlcMemType.S, 10008, 0x2);
            PioI2A.UpMachineAlarm                /*  */ = new PlcAddr(PlcMemType.S, 10008, 0x3);
            PioI2A.UpSendAble                    /*  */ = new PlcAddr(PlcMemType.S, 10008, 0x4);
            PioI2A.UpSendStart                   /*  */ = new PlcAddr(PlcMemType.S, 10008, 0x5);
            PioI2A.UpSendComplete                /*  */ = new PlcAddr(PlcMemType.S, 10008, 0x6);
            PioI2A.UpExchangeFlag                /*  */ = new PlcAddr(PlcMemType.S, 10008, 0x7);
            PioI2A.UpReturnReceiveStart          /*  */ = new PlcAddr(PlcMemType.S, 10009, 0x0);
            PioI2A.UpReturnReceiveComplete       /*  */ = new PlcAddr(PlcMemType.S, 10009, 0x1);
            PioI2A.UpImmediatelyPauseRequest     /*  */ = new PlcAddr(PlcMemType.S, 10009, 0x2);
            PioI2A.UpImmediatelyStopRequest      /*  */ = new PlcAddr(PlcMemType.S, 10009, 0x3);
            PioI2A.UpSendAbleRemainedStep1       /*  */ = new PlcAddr(PlcMemType.S, 10009, 0x4);
            PioI2A.UpSendAbleRemainedStep2       /*  */ = new PlcAddr(PlcMemType.S, 10009, 0x5);
            PioI2A.UpSendAbleRemainedStep3       /*  */ = new PlcAddr(PlcMemType.S, 10009, 0x6);
            PioI2A.UpSendAbleRemainedStep4       /*  */ = new PlcAddr(PlcMemType.S, 10009, 0x7);
            PioI2A.UpWorkStart                   /*  */ = new PlcAddr(PlcMemType.S, 10010, 0x0);
            PioI2A.UpWorkCancel                  /*  */ = new PlcAddr(PlcMemType.S, 10010, 0x1);
            PioI2A.UpWorkSkip                    /*  */ = new PlcAddr(PlcMemType.S, 10010, 0x2);
            PioI2A.UpJobStart                    /*  */ = new PlcAddr(PlcMemType.S, 10010, 0x3);
            PioI2A.UpJobEnd                      /*  */ = new PlcAddr(PlcMemType.S, 10010, 0x4);
            PioI2A.UpHotFlow                     /*  */ = new PlcAddr(PlcMemType.S, 10010, 0x5);
            PioI2A.UpReserved	             /*  */     = new PlcAddr(PlcMemType.S, 10010, 0x6);
            PioI2A.UpSendAbleReserveRequest      /*  */ = new PlcAddr(PlcMemType.S, 10010, 0x7);
            PioI2A.UpHandShakeCancelRequest      /*  */ = new PlcAddr(PlcMemType.S, 10011, 0x0);
            PioI2A.UpHandShakeAbortRequest       /*  */ = new PlcAddr(PlcMemType.S, 10011, 0x1);
            PioI2A.UpHandShakeResumeRequest      /*  */ = new PlcAddr(PlcMemType.S, 10011, 0x2);
            PioI2A.UpHandShakeRecoveryAckReply   /*  */ = new PlcAddr(PlcMemType.S, 10011, 0x3);
            PioI2A.UpHandShakeRecoveryNakReply   /*  */ = new PlcAddr(PlcMemType.S, 10011, 0x4);
            PioI2A.UpSendJobReady                /*  */ = new PlcAddr(PlcMemType.S, 10011, 0x5);
            PioI2A.UpSendActionMove              /*  */ = new PlcAddr(PlcMemType.S, 10011, 0x6);
            PioI2A.UpSendActionRemove            /*  */ = new PlcAddr(PlcMemType.S, 10011, 0x7);
            //Lower	Contact	Point		                                            
            PioI2A.UpAbnormal                    /*  */ = new PlcAddr(PlcMemType.S, 10012, 0x0);
            PioI2A.UpTypeofArm                   /*  */ = new PlcAddr(PlcMemType.S, 10012, 0x1);
            PioI2A.UpTypeofStageConveyor         /*  */ = new PlcAddr(PlcMemType.S, 10012, 0x2);
            PioI2A.UpArmStretchUpMoving          /*  */ = new PlcAddr(PlcMemType.S, 10012, 0x3);
            PioI2A.UpArmStretchUpComplete        /*  */ = new PlcAddr(PlcMemType.S, 10012, 0x4);
            PioI2A.UpArmStretchDownMoving        /*  */ = new PlcAddr(PlcMemType.S, 10012, 0x5);
            PioI2A.UpArmStretchDownComplete      /*  */ = new PlcAddr(PlcMemType.S, 10012, 0x6);
            PioI2A.UpArmStretching               /*  */ = new PlcAddr(PlcMemType.S, 10012, 0x7);
            PioI2A.UpArmStretchComplete          /*  */ = new PlcAddr(PlcMemType.S, 10013, 0x0);
            PioI2A.UpArmFolding                  /*  */ = new PlcAddr(PlcMemType.S, 10013, 0x1);
            PioI2A.UpArmFoldComplete             /*  */ = new PlcAddr(PlcMemType.S, 10013, 0x2);
            PioI2A.UpCpReserved1	            /*  */  = new PlcAddr(PlcMemType.S, 10013, 0x3);
            PioI2A.UpCpReserved2	            /*  */  = new PlcAddr(PlcMemType.S, 10013, 0x4);
            PioI2A.UpArm1Folded                  /*  */ = new PlcAddr(PlcMemType.S, 10013, 0x5);
            PioI2A.UpArm2Folded                  /*  */ = new PlcAddr(PlcMemType.S, 10013, 0x6);
            PioI2A.UpArm1GlassDetect             /*  */ = new PlcAddr(PlcMemType.S, 10013, 0x7);
            PioI2A.UpArm2GlassDetect             /*  */ = new PlcAddr(PlcMemType.S, 10014, 0x0);
            PioI2A.UpArm1GlassVacuum             /*  */ = new PlcAddr(PlcMemType.S, 10014, 0x1);
            PioI2A.UpArm2GlassVacuum             /*  */ = new PlcAddr(PlcMemType.S, 10014, 0x2);
            PioI2A.UpRobotDirection              /*  */ = new PlcAddr(PlcMemType.S, 10014, 0x3);
            PioI2A.UpManualOperation             /*  */ = new PlcAddr(PlcMemType.S, 10014, 0x4);
            PioI2A.UpPinUp                       /*  */ = new PlcAddr(PlcMemType.S, 10014, 0x5);
            PioI2A.UpPinDown                     /*  */ = new PlcAddr(PlcMemType.S, 10014, 0x6);
            PioI2A.UpDoorOpen                    /*  */ = new PlcAddr(PlcMemType.S, 10014, 0x7);
            PioI2A.UpDoorClose                   /*  */ = new PlcAddr(PlcMemType.S, 10015, 0x0);
            PioI2A.UpGlassDetect                 /*  */ = new PlcAddr(PlcMemType.S, 10015, 0x1);
            PioI2A.UpBodyMoving                  /*  */ = new PlcAddr(PlcMemType.S, 10015, 0x2);
            PioI2A.UpBodyOriginPosition          /*  */ = new PlcAddr(PlcMemType.S, 10015, 0x3);
            PioI2A.UpEmergency                   /*  */ = new PlcAddr(PlcMemType.S, 10015, 0x4);
            PioI2A.UpVertical                    /*  */ = new PlcAddr(PlcMemType.S, 10015, 0x5);
            PioI2A.UpHorizontal                  /*  */ = new PlcAddr(PlcMemType.S, 10015, 0x6);
            #endregion
            PioI2A.LoHeartBit.PLC = GG.HSMS;
            PioI2A.LoMachinePause.PLC = GG.HSMS;
            PioI2A.LoMachineDown.PLC = GG.HSMS;
            PioI2A.LoMachineAlarm.PLC = GG.HSMS;
            PioI2A.LoReceiveAble.PLC = GG.HSMS;
            PioI2A.LoReceiveStart.PLC = GG.HSMS;
            PioI2A.LoReceiveComplete.PLC = GG.HSMS;
            PioI2A.LoExchangeFlag.PLC = GG.HSMS;
            PioI2A.LoReturnSendStart.PLC = GG.HSMS;
            PioI2A.LoReturnSendComplete.PLC = GG.HSMS;
            PioI2A.LoImmediatelyPauseRequest.PLC = GG.HSMS;
            PioI2A.LoImmediatelyStopRequest.PLC = GG.HSMS;
            PioI2A.LoReceiveAbleRemainedStep1.PLC = GG.HSMS;
            PioI2A.LoReceiveAbleRemainedStep2.PLC = GG.HSMS;
            PioI2A.LoReceiveAbleRemainedStep3.PLC = GG.HSMS;
            PioI2A.LoReceiveAbleRemainedStep4.PLC = GG.HSMS;
            PioI2A.LoGlassIDReadComplete.PLC = GG.HSMS;
            PioI2A.LoLoadingStop.PLC = GG.HSMS;
            PioI2A.LoTransferStop.PLC = GG.HSMS;
            PioI2A.LoExchangeFailFlag.PLC = GG.HSMS;
            PioI2A.LoProcessTimeUp.PLC = GG.HSMS;
            PioI2A.LoReceiveAbleReserveRequest.PLC = GG.HSMS;
            PioI2A.LoHandShakeCancelRequest.PLC = GG.HSMS;
            PioI2A.LoHandShakeAbortRequest.PLC = GG.HSMS;
            PioI2A.LoHandShakeResumeRequest.PLC = GG.HSMS;
            PioI2A.LoHandShakeRecoveryAckReply.PLC = GG.HSMS;
            PioI2A.LoHandShakeRecoveryNakReply.PLC = GG.HSMS;
            PioI2A.LoReceiveJobReady.PLC = GG.HSMS;
            PioI2A.LoReceiveActionMove.PLC = GG.HSMS;
            PioI2A.LoReceiveActionRemove.PLC = GG.HSMS;
            PioI2A.LoAbnormal.PLC = GG.HSMS;
            PioI2A.LoTypeofArm.PLC = GG.HSMS;
            PioI2A.LoTypeofStageConveyor.PLC = GG.HSMS;
            PioI2A.LoArmStretchUpMoving.PLC = GG.HSMS;
            PioI2A.LoArmStretchUpComplete.PLC = GG.HSMS;
            PioI2A.LoArmStretchDownMoving.PLC = GG.HSMS;
            PioI2A.LoArmStretchDownComplete.PLC = GG.HSMS;
            PioI2A.LoArmStretching.PLC = GG.HSMS;
            PioI2A.LoArmStretchComplete.PLC = GG.HSMS;
            PioI2A.LoArmFolding.PLC = GG.HSMS;
            PioI2A.LoArmFoldComplete.PLC = GG.HSMS;
            PioI2A.LoArm1Folded.PLC = GG.HSMS;
            PioI2A.LoArm2Folded.PLC = GG.HSMS;
            PioI2A.LoArm1GlassDetect.PLC = GG.HSMS;
            PioI2A.LoArm2GlassDetect.PLC = GG.HSMS;
            PioI2A.LoArm1GlassVacuum.PLC = GG.HSMS;
            PioI2A.LoArm2GlassVacuum.PLC = GG.HSMS;
            PioI2A.LoRobotDirection.PLC = GG.HSMS;
            PioI2A.LoManualOperation.PLC = GG.HSMS;
            PioI2A.LoPinUp.PLC = GG.HSMS;
            PioI2A.LoPinDown.PLC = GG.HSMS;
            PioI2A.LoDoorOpen.PLC = GG.HSMS;
            PioI2A.LoDoorClose.PLC = GG.HSMS;
            PioI2A.LoGlassDetect.PLC = GG.HSMS;
            PioI2A.LoBodyMoving.PLC = GG.HSMS;
            PioI2A.LoBodyOriginPosition.PLC = GG.HSMS;
            PioI2A.LoEmergency.PLC = GG.HSMS;
            PioI2A.LoVertical.PLC = GG.HSMS;
            PioI2A.LoHorizontal.PLC = GG.HSMS;
            PioI2A.UpHeartBit.PLC = GG.HSMS;
            PioI2A.UpMachinePause.PLC = GG.HSMS;
            PioI2A.UpMachineDown.PLC = GG.HSMS;
            PioI2A.UpMachineAlarm.PLC = GG.HSMS;
            PioI2A.UpSendAble.PLC = GG.HSMS;
            PioI2A.UpSendStart.PLC = GG.HSMS;
            PioI2A.UpSendComplete.PLC = GG.HSMS;
            PioI2A.UpExchangeFlag.PLC = GG.HSMS;
            PioI2A.UpReturnReceiveStart.PLC = GG.HSMS;
            PioI2A.UpReturnReceiveComplete.PLC = GG.HSMS;
            PioI2A.UpImmediatelyPauseRequest.PLC = GG.HSMS;
            PioI2A.UpImmediatelyStopRequest.PLC = GG.HSMS;
            PioI2A.UpSendAbleRemainedStep1.PLC = GG.HSMS;
            PioI2A.UpSendAbleRemainedStep2.PLC = GG.HSMS;
            PioI2A.UpSendAbleRemainedStep3.PLC = GG.HSMS;
            PioI2A.UpSendAbleRemainedStep4.PLC = GG.HSMS;
            PioI2A.UpWorkStart.PLC = GG.HSMS;
            PioI2A.UpWorkCancel.PLC = GG.HSMS;
            PioI2A.UpWorkSkip.PLC = GG.HSMS;
            PioI2A.UpJobStart.PLC = GG.HSMS;
            PioI2A.UpJobEnd.PLC = GG.HSMS;
            PioI2A.UpHotFlow.PLC = GG.HSMS;
            PioI2A.UpSendAbleReserveRequest.PLC = GG.HSMS;
            PioI2A.UpHandShakeCancelRequest.PLC = GG.HSMS;
            PioI2A.UpHandShakeAbortRequest.PLC = GG.HSMS;
            PioI2A.UpHandShakeResumeRequest.PLC = GG.HSMS;
            PioI2A.UpHandShakeRecoveryAckReply.PLC = GG.HSMS;
            PioI2A.UpHandShakeRecoveryNakReply.PLC = GG.HSMS;
            PioI2A.UpSendJobReady.PLC = GG.HSMS;
            PioI2A.UpSendActionMove.PLC = GG.HSMS;
            PioI2A.UpSendActionRemove.PLC = GG.HSMS;
            PioI2A.UpAbnormal.PLC = GG.HSMS;
            PioI2A.UpTypeofArm.PLC = GG.HSMS;
            PioI2A.UpTypeofStageConveyor.PLC = GG.HSMS;
            PioI2A.UpArmStretchUpMoving.PLC = GG.HSMS;
            PioI2A.UpArmStretchUpComplete.PLC = GG.HSMS;
            PioI2A.UpArmStretchDownMoving.PLC = GG.HSMS;
            PioI2A.UpArmStretchDownComplete.PLC = GG.HSMS;
            PioI2A.UpArmStretching.PLC = GG.HSMS;
            PioI2A.UpArmStretchComplete.PLC = GG.HSMS;
            PioI2A.UpArmFolding.PLC = GG.HSMS;
            PioI2A.UpArmFoldComplete.PLC = GG.HSMS;
            PioI2A.UpArm1Folded.PLC = GG.HSMS;
            PioI2A.UpArm2Folded.PLC = GG.HSMS;
            PioI2A.UpArm1GlassDetect.PLC = GG.HSMS;
            PioI2A.UpArm2GlassDetect.PLC = GG.HSMS;
            PioI2A.UpArm1GlassVacuum.PLC = GG.HSMS;
            PioI2A.UpArm2GlassVacuum.PLC = GG.HSMS;
            PioI2A.UpRobotDirection.PLC = GG.HSMS;
            PioI2A.UpManualOperation.PLC = GG.HSMS;
            PioI2A.UpPinUp.PLC = GG.HSMS;
            PioI2A.UpPinDown.PLC = GG.HSMS;
            PioI2A.UpDoorOpen.PLC = GG.HSMS;
            PioI2A.UpDoorClose.PLC = GG.HSMS;
            PioI2A.UpGlassDetect.PLC = GG.HSMS;
            PioI2A.UpBodyMoving.PLC = GG.HSMS;
            PioI2A.UpBodyOriginPosition.PLC = GG.HSMS;
            PioI2A.UpEmergency.PLC = GG.HSMS;
            PioI2A.UpVertical.PLC = GG.HSMS;
            PioI2A.UpHorizontal.PLC = GG.HSMS;
        }

        private void InitPioA2I()
        {
            #region CIMAB Up의 주소를 매핑
            //HAND SHAKE TO UPPER
            PioA2I.LoHeartBit                    /*  */ = new PlcAddr(PlcMemType.S, 10000, 0x0);
            PioA2I.LoMachinePause                /*  */ = new PlcAddr(PlcMemType.S, 10000, 0x1);
            PioA2I.LoMachineDown                 /*  */ = new PlcAddr(PlcMemType.S, 10000, 0x2);
            PioA2I.LoMachineAlarm                /*  */ = new PlcAddr(PlcMemType.S, 10000, 0x3);
            PioA2I.LoReceiveAble                 /*  */ = new PlcAddr(PlcMemType.S, 10000, 0x4);
            PioA2I.LoReceiveStart                /*  */ = new PlcAddr(PlcMemType.S, 10000, 0x5);
            PioA2I.LoReceiveComplete             /*  */ = new PlcAddr(PlcMemType.S, 10000, 0x6);
            PioA2I.LoExchangeFlag                /*  */ = new PlcAddr(PlcMemType.S, 10000, 0x7);
            PioA2I.LoReturnSendStart             /*  */ = new PlcAddr(PlcMemType.S, 10001, 0x0);
            PioA2I.LoReturnSendComplete          /*  */ = new PlcAddr(PlcMemType.S, 10001, 0x1);
            PioA2I.LoImmediatelyPauseRequest     /*  */ = new PlcAddr(PlcMemType.S, 10001, 0x2);
            PioA2I.LoImmediatelyStopRequest      /*  */ = new PlcAddr(PlcMemType.S, 10001, 0x3);
            PioA2I.LoReceiveAbleRemainedStep1    /*  */ = new PlcAddr(PlcMemType.S, 10001, 0x4);
            PioA2I.LoReceiveAbleRemainedStep2    /*  */ = new PlcAddr(PlcMemType.S, 10001, 0x5);
            PioA2I.LoReceiveAbleRemainedStep3    /*  */ = new PlcAddr(PlcMemType.S, 10001, 0x6);
            PioA2I.LoReceiveAbleRemainedStep4    /*  */ = new PlcAddr(PlcMemType.S, 10001, 0x7);
            PioA2I.LoGlassIDReadComplete         /*  */ = new PlcAddr(PlcMemType.S, 10002, 0x0);
            PioA2I.LoLoadingStop                 /*  */ = new PlcAddr(PlcMemType.S, 10002, 0x1);
            PioA2I.LoTransferStop                /*  */ = new PlcAddr(PlcMemType.S, 10002, 0x2);
            PioA2I.LoExchangeFailFlag            /*  */ = new PlcAddr(PlcMemType.S, 10002, 0x3);
            PioA2I.LoProcessTimeUp               /*  */ = new PlcAddr(PlcMemType.S, 10002, 0x4);
            PioA2I.LoReserved1	            /*  */      = new PlcAddr(PlcMemType.S, 10002, 0x5);
            PioA2I.LoReserved2	            /*  */      = new PlcAddr(PlcMemType.S, 10002, 0x6);
            PioA2I.LoReceiveAbleReserveRequest   /*  */ = new PlcAddr(PlcMemType.S, 10002, 0x7);
            PioA2I.LoHandShakeCancelRequest      /*  */ = new PlcAddr(PlcMemType.S, 10003, 0x0);
            PioA2I.LoHandShakeAbortRequest       /*  */ = new PlcAddr(PlcMemType.S, 10003, 0x1);
            PioA2I.LoHandShakeResumeRequest      /*  */ = new PlcAddr(PlcMemType.S, 10003, 0x2);
            PioA2I.LoHandShakeRecoveryAckReply   /*  */ = new PlcAddr(PlcMemType.S, 10003, 0x3);
            PioA2I.LoHandShakeRecoveryNakReply   /*  */ = new PlcAddr(PlcMemType.S, 10003, 0x4);
            PioA2I.LoReceiveJobReady             /*  */ = new PlcAddr(PlcMemType.S, 10003, 0x5);
            PioA2I.LoReceiveActionMove           /*  */ = new PlcAddr(PlcMemType.S, 10003, 0x6);
            PioA2I.LoReceiveActionRemove         /*  */ = new PlcAddr(PlcMemType.S, 10003, 0x7);
            //Upper	Contact	Point	                                                
            PioA2I.LoAbnormal                    /*  */ = new PlcAddr(PlcMemType.S, 10004, 0x0);
            PioA2I.LoTypeofArm                   /*  */ = new PlcAddr(PlcMemType.S, 10004, 0x1);
            PioA2I.LoTypeofStageConveyor         /*  */ = new PlcAddr(PlcMemType.S, 10004, 0x2);
            PioA2I.LoArmStretchUpMoving          /*  */ = new PlcAddr(PlcMemType.S, 10004, 0x3);
            PioA2I.LoArmStretchUpComplete        /*  */ = new PlcAddr(PlcMemType.S, 10004, 0x4);
            PioA2I.LoArmStretchDownMoving        /*  */ = new PlcAddr(PlcMemType.S, 10004, 0x5);
            PioA2I.LoArmStretchDownComplete      /*  */ = new PlcAddr(PlcMemType.S, 10004, 0x6);
            PioA2I.LoArmStretching               /*  */ = new PlcAddr(PlcMemType.S, 10004, 0x7);
            PioA2I.LoArmStretchComplete          /*  */ = new PlcAddr(PlcMemType.S, 10005, 0x0);
            PioA2I.LoArmFolding                  /*  */ = new PlcAddr(PlcMemType.S, 10005, 0x1);
            PioA2I.LoArmFoldComplete             /*  */ = new PlcAddr(PlcMemType.S, 10005, 0x2);
            PioA2I.LoCpReserved1                  /*  */= new PlcAddr(PlcMemType.S, 10005, 0x3);
            PioA2I.LoCpReserved2                  /*  */= new PlcAddr(PlcMemType.S, 10005, 0x4);
            PioA2I.LoArm1Folded                  /*  */ = new PlcAddr(PlcMemType.S, 10005, 0x5);
            PioA2I.LoArm2Folded                  /*  */ = new PlcAddr(PlcMemType.S, 10005, 0x6);
            PioA2I.LoArm1GlassDetect             /*  */ = new PlcAddr(PlcMemType.S, 10005, 0x7);
            PioA2I.LoArm2GlassDetect             /*  */ = new PlcAddr(PlcMemType.S, 10006, 0x0);
            PioA2I.LoArm1GlassVacuum             /*  */ = new PlcAddr(PlcMemType.S, 10006, 0x1);
            PioA2I.LoArm2GlassVacuum             /*  */ = new PlcAddr(PlcMemType.S, 10006, 0x2);
            PioA2I.LoRobotDirection              /*  */ = new PlcAddr(PlcMemType.S, 10006, 0x3);
            PioA2I.LoManualOperation             /*  */ = new PlcAddr(PlcMemType.S, 10006, 0x4);
            PioA2I.LoPinUp                       /*  */ = new PlcAddr(PlcMemType.S, 10006, 0x5);
            PioA2I.LoPinDown                     /*  */ = new PlcAddr(PlcMemType.S, 10006, 0x6);
            PioA2I.LoDoorOpen                    /*  */ = new PlcAddr(PlcMemType.S, 10006, 0x7);
            PioA2I.LoDoorClose                   /*  */ = new PlcAddr(PlcMemType.S, 10007, 0x0);
            PioA2I.LoGlassDetect                 /*  */ = new PlcAddr(PlcMemType.S, 10007, 0x1);
            PioA2I.LoBodyMoving                  /*  */ = new PlcAddr(PlcMemType.S, 10007, 0x2);
            PioA2I.LoBodyOriginPosition          /*  */ = new PlcAddr(PlcMemType.S, 10007, 0x3);
            PioA2I.LoEmergency                   /*  */ = new PlcAddr(PlcMemType.S, 10007, 0x4);
            PioA2I.LoVertical                    /*  */ = new PlcAddr(PlcMemType.S, 10007, 0x5);
            PioA2I.LoHorizontal                  /*  */ = new PlcAddr(PlcMemType.S, 10007, 0x6);
            #endregion
            #region AOIB Lo의 주소를 매핑
            //Lower	EQ						         
            PioA2I.UpHeartBit                    /*  */ = new PlcAddr(PlcMemType.S, 8, 0x0);
            PioA2I.UpMachinePause                /*  */ = new PlcAddr(PlcMemType.S, 8, 0x1);
            PioA2I.UpMachineDown                 /*  */ = new PlcAddr(PlcMemType.S, 8, 0x2);
            PioA2I.UpMachineAlarm                /*  */ = new PlcAddr(PlcMemType.S, 8, 0x3);
            PioA2I.UpSendAble                    /*  */ = new PlcAddr(PlcMemType.S, 8, 0x4);
            PioA2I.UpSendStart                   /*  */ = new PlcAddr(PlcMemType.S, 8, 0x5);
            PioA2I.UpSendComplete                /*  */ = new PlcAddr(PlcMemType.S, 8, 0x6);
            PioA2I.UpExchangeFlag                /*  */ = new PlcAddr(PlcMemType.S, 8, 0x7);
            PioA2I.UpReturnReceiveStart          /*  */ = new PlcAddr(PlcMemType.S, 9, 0x0);
            PioA2I.UpReturnReceiveComplete       /*  */ = new PlcAddr(PlcMemType.S, 9, 0x1);
            PioA2I.UpImmediatelyPauseRequest     /*  */ = new PlcAddr(PlcMemType.S, 9, 0x2);
            PioA2I.UpImmediatelyStopRequest      /*  */ = new PlcAddr(PlcMemType.S, 9, 0x3);
            PioA2I.UpSendAbleRemainedStep1       /*  */ = new PlcAddr(PlcMemType.S, 9, 0x4);
            PioA2I.UpSendAbleRemainedStep2       /*  */ = new PlcAddr(PlcMemType.S, 9, 0x5);
            PioA2I.UpSendAbleRemainedStep3       /*  */ = new PlcAddr(PlcMemType.S, 9, 0x6);
            PioA2I.UpSendAbleRemainedStep4       /*  */ = new PlcAddr(PlcMemType.S, 9, 0x7);
            PioA2I.UpWorkStart                   /*  */ = new PlcAddr(PlcMemType.S, 10, 0x0);
            PioA2I.UpWorkCancel                  /*  */ = new PlcAddr(PlcMemType.S, 10, 0x1);
            PioA2I.UpWorkSkip                    /*  */ = new PlcAddr(PlcMemType.S, 10, 0x2);
            PioA2I.UpJobStart                    /*  */ = new PlcAddr(PlcMemType.S, 10, 0x3);
            PioA2I.UpJobEnd                      /*  */ = new PlcAddr(PlcMemType.S, 10, 0x4);
            PioA2I.UpHotFlow                     /*  */ = new PlcAddr(PlcMemType.S, 10, 0x5);
            PioA2I.UpReserved	             /*  */     = new PlcAddr(PlcMemType.S, 10, 0x6);
            PioA2I.UpSendAbleReserveRequest      /*  */ = new PlcAddr(PlcMemType.S, 10, 0x7);
            PioA2I.UpHandShakeCancelRequest      /*  */ = new PlcAddr(PlcMemType.S, 11, 0x0);
            PioA2I.UpHandShakeAbortRequest       /*  */ = new PlcAddr(PlcMemType.S, 11, 0x1);
            PioA2I.UpHandShakeResumeRequest      /*  */ = new PlcAddr(PlcMemType.S, 11, 0x2);
            PioA2I.UpHandShakeRecoveryAckReply   /*  */ = new PlcAddr(PlcMemType.S, 11, 0x3);
            PioA2I.UpHandShakeRecoveryNakReply   /*  */ = new PlcAddr(PlcMemType.S, 11, 0x4);
            PioA2I.UpSendJobReady                /*  */ = new PlcAddr(PlcMemType.S, 11, 0x5);
            PioA2I.UpSendActionMove              /*  */ = new PlcAddr(PlcMemType.S, 11, 0x6);
            PioA2I.UpSendActionRemove            /*  */ = new PlcAddr(PlcMemType.S, 11, 0x7);
            //Lower	Contact	Point		
            PioA2I.UpAbnormal                    /*  */ = new PlcAddr(PlcMemType.S, 12, 0x0);
            PioA2I.UpTypeofArm                   /*  */ = new PlcAddr(PlcMemType.S, 12, 0x1);
            PioA2I.UpTypeofStageConveyor         /*  */ = new PlcAddr(PlcMemType.S, 12, 0x2);
            PioA2I.UpArmStretchUpMoving          /*  */ = new PlcAddr(PlcMemType.S, 12, 0x3);
            PioA2I.UpArmStretchUpComplete        /*  */ = new PlcAddr(PlcMemType.S, 12, 0x4);
            PioA2I.UpArmStretchDownMoving        /*  */ = new PlcAddr(PlcMemType.S, 12, 0x5);
            PioA2I.UpArmStretchDownComplete      /*  */ = new PlcAddr(PlcMemType.S, 12, 0x6);
            PioA2I.UpArmStretching               /*  */ = new PlcAddr(PlcMemType.S, 12, 0x7);
            PioA2I.UpArmStretchComplete          /*  */ = new PlcAddr(PlcMemType.S, 13, 0x0);
            PioA2I.UpArmFolding                  /*  */ = new PlcAddr(PlcMemType.S, 13, 0x1);
            PioA2I.UpArmFoldComplete             /*  */ = new PlcAddr(PlcMemType.S, 13, 0x2);
            PioA2I.UpCpReserved1	            /*  */  = new PlcAddr(PlcMemType.S, 13, 0x3);
            PioA2I.UpCpReserved2	            /*  */  = new PlcAddr(PlcMemType.S, 13, 0x4);
            PioA2I.UpArm1Folded                  /*  */ = new PlcAddr(PlcMemType.S, 13, 0x5);
            PioA2I.UpArm2Folded                  /*  */ = new PlcAddr(PlcMemType.S, 13, 0x6);
            PioA2I.UpArm1GlassDetect             /*  */ = new PlcAddr(PlcMemType.S, 13, 0x7);
            PioA2I.UpArm2GlassDetect             /*  */ = new PlcAddr(PlcMemType.S, 14, 0x0);
            PioA2I.UpArm1GlassVacuum             /*  */ = new PlcAddr(PlcMemType.S, 14, 0x1);
            PioA2I.UpArm2GlassVacuum             /*  */ = new PlcAddr(PlcMemType.S, 14, 0x2);
            PioA2I.UpRobotDirection              /*  */ = new PlcAddr(PlcMemType.S, 14, 0x3);
            PioA2I.UpManualOperation             /*  */ = new PlcAddr(PlcMemType.S, 14, 0x4);
            PioA2I.UpPinUp                       /*  */ = new PlcAddr(PlcMemType.S, 14, 0x5);
            PioA2I.UpPinDown                     /*  */ = new PlcAddr(PlcMemType.S, 14, 0x6);
            PioA2I.UpDoorOpen                    /*  */ = new PlcAddr(PlcMemType.S, 14, 0x7);
            PioA2I.UpDoorClose                   /*  */ = new PlcAddr(PlcMemType.S, 15, 0x0);
            PioA2I.UpGlassDetect                 /*  */ = new PlcAddr(PlcMemType.S, 15, 0x1);
            PioA2I.UpBodyMoving                  /*  */ = new PlcAddr(PlcMemType.S, 15, 0x2);
            PioA2I.UpBodyOriginPosition          /*  */ = new PlcAddr(PlcMemType.S, 15, 0x3);
            PioA2I.UpEmergency                   /*  */ = new PlcAddr(PlcMemType.S, 15, 0x4);
            PioA2I.UpVertical                    /*  */ = new PlcAddr(PlcMemType.S, 15, 0x5);
            PioA2I.UpHorizontal                  /*  */ = new PlcAddr(PlcMemType.S, 15, 0x6);
            #endregion
            PioA2I.LoHeartBit.PLC = GG.HSMS;
            PioA2I.LoMachinePause.PLC = GG.HSMS;
            PioA2I.LoMachineDown.PLC = GG.HSMS;
            PioA2I.LoMachineAlarm.PLC = GG.HSMS;
            PioA2I.LoReceiveAble.PLC = GG.HSMS;
            PioA2I.LoReceiveStart.PLC = GG.HSMS;
            PioA2I.LoReceiveComplete.PLC = GG.HSMS;
            PioA2I.LoExchangeFlag.PLC = GG.HSMS;
            PioA2I.LoReturnSendStart.PLC = GG.HSMS;
            PioA2I.LoReturnSendComplete.PLC = GG.HSMS;
            PioA2I.LoImmediatelyPauseRequest.PLC = GG.HSMS;
            PioA2I.LoImmediatelyStopRequest.PLC = GG.HSMS;
            PioA2I.LoReceiveAbleRemainedStep1.PLC = GG.HSMS;
            PioA2I.LoReceiveAbleRemainedStep2.PLC = GG.HSMS;
            PioA2I.LoReceiveAbleRemainedStep3.PLC = GG.HSMS;
            PioA2I.LoReceiveAbleRemainedStep4.PLC = GG.HSMS;
            PioA2I.LoGlassIDReadComplete.PLC = GG.HSMS;
            PioA2I.LoLoadingStop.PLC = GG.HSMS;
            PioA2I.LoTransferStop.PLC = GG.HSMS;
            PioA2I.LoExchangeFailFlag.PLC = GG.HSMS;
            PioA2I.LoProcessTimeUp.PLC = GG.HSMS;
            PioA2I.LoReceiveAbleReserveRequest.PLC = GG.HSMS;
            PioA2I.LoHandShakeCancelRequest.PLC = GG.HSMS;
            PioA2I.LoHandShakeAbortRequest.PLC = GG.HSMS;
            PioA2I.LoHandShakeResumeRequest.PLC = GG.HSMS;
            PioA2I.LoHandShakeRecoveryAckReply.PLC = GG.HSMS;
            PioA2I.LoHandShakeRecoveryNakReply.PLC = GG.HSMS;
            PioA2I.LoReceiveJobReady.PLC = GG.HSMS;
            PioA2I.LoReceiveActionMove.PLC = GG.HSMS;
            PioA2I.LoReceiveActionRemove.PLC = GG.HSMS;
            PioA2I.LoAbnormal.PLC = GG.HSMS;
            PioA2I.LoTypeofArm.PLC = GG.HSMS;
            PioA2I.LoTypeofStageConveyor.PLC = GG.HSMS;
            PioA2I.LoArmStretchUpMoving.PLC = GG.HSMS;
            PioA2I.LoArmStretchUpComplete.PLC = GG.HSMS;
            PioA2I.LoArmStretchDownMoving.PLC = GG.HSMS;
            PioA2I.LoArmStretchDownComplete.PLC = GG.HSMS;
            PioA2I.LoArmStretching.PLC = GG.HSMS;
            PioA2I.LoArmStretchComplete.PLC = GG.HSMS;
            PioA2I.LoArmFolding.PLC = GG.HSMS;
            PioA2I.LoArmFoldComplete.PLC = GG.HSMS;
            PioA2I.LoArm1Folded.PLC = GG.HSMS;
            PioA2I.LoArm2Folded.PLC = GG.HSMS;
            PioA2I.LoArm1GlassDetect.PLC = GG.HSMS;
            PioA2I.LoArm2GlassDetect.PLC = GG.HSMS;
            PioA2I.LoArm1GlassVacuum.PLC = GG.HSMS;
            PioA2I.LoArm2GlassVacuum.PLC = GG.HSMS;
            PioA2I.LoRobotDirection.PLC = GG.HSMS;
            PioA2I.LoManualOperation.PLC = GG.HSMS;
            PioA2I.LoPinUp.PLC = GG.HSMS;
            PioA2I.LoPinDown.PLC = GG.HSMS;
            PioA2I.LoDoorOpen.PLC = GG.HSMS;
            PioA2I.LoDoorClose.PLC = GG.HSMS;
            PioA2I.LoGlassDetect.PLC = GG.HSMS;
            PioA2I.LoBodyMoving.PLC = GG.HSMS;
            PioA2I.LoBodyOriginPosition.PLC = GG.HSMS;
            PioA2I.LoEmergency.PLC = GG.HSMS;
            PioA2I.LoVertical.PLC = GG.HSMS;
            PioA2I.LoHorizontal.PLC = GG.HSMS;
            PioA2I.UpHeartBit.PLC = GG.HSMS;
            PioA2I.UpMachinePause.PLC = GG.HSMS;
            PioA2I.UpMachineDown.PLC = GG.HSMS;
            PioA2I.UpMachineAlarm.PLC = GG.HSMS;
            PioA2I.UpSendAble.PLC = GG.HSMS;
            PioA2I.UpSendStart.PLC = GG.HSMS;
            PioA2I.UpSendComplete.PLC = GG.HSMS;
            PioA2I.UpExchangeFlag.PLC = GG.HSMS;
            PioA2I.UpReturnReceiveStart.PLC = GG.HSMS;
            PioA2I.UpReturnReceiveComplete.PLC = GG.HSMS;
            PioA2I.UpImmediatelyPauseRequest.PLC = GG.HSMS;
            PioA2I.UpImmediatelyStopRequest.PLC = GG.HSMS;
            PioA2I.UpSendAbleRemainedStep1.PLC = GG.HSMS;
            PioA2I.UpSendAbleRemainedStep2.PLC = GG.HSMS;
            PioA2I.UpSendAbleRemainedStep3.PLC = GG.HSMS;
            PioA2I.UpSendAbleRemainedStep4.PLC = GG.HSMS;
            PioA2I.UpWorkStart.PLC = GG.HSMS;
            PioA2I.UpWorkCancel.PLC = GG.HSMS;
            PioA2I.UpWorkSkip.PLC = GG.HSMS;
            PioA2I.UpJobStart.PLC = GG.HSMS;
            PioA2I.UpJobEnd.PLC = GG.HSMS;
            PioA2I.UpHotFlow.PLC = GG.HSMS;
            PioA2I.UpSendAbleReserveRequest.PLC = GG.HSMS;
            PioA2I.UpHandShakeCancelRequest.PLC = GG.HSMS;
            PioA2I.UpHandShakeAbortRequest.PLC = GG.HSMS;
            PioA2I.UpHandShakeResumeRequest.PLC = GG.HSMS;
            PioA2I.UpHandShakeRecoveryAckReply.PLC = GG.HSMS;
            PioA2I.UpHandShakeRecoveryNakReply.PLC = GG.HSMS;
            PioA2I.UpSendJobReady.PLC = GG.HSMS;
            PioA2I.UpSendActionMove.PLC = GG.HSMS;
            PioA2I.UpSendActionRemove.PLC = GG.HSMS;
            PioA2I.UpAbnormal.PLC = GG.HSMS;
            PioA2I.UpTypeofArm.PLC = GG.HSMS;
            PioA2I.UpTypeofStageConveyor.PLC = GG.HSMS;
            PioA2I.UpArmStretchUpMoving.PLC = GG.HSMS;
            PioA2I.UpArmStretchUpComplete.PLC = GG.HSMS;
            PioA2I.UpArmStretchDownMoving.PLC = GG.HSMS;
            PioA2I.UpArmStretchDownComplete.PLC = GG.HSMS;
            PioA2I.UpArmStretching.PLC = GG.HSMS;
            PioA2I.UpArmStretchComplete.PLC = GG.HSMS;
            PioA2I.UpArmFolding.PLC = GG.HSMS;
            PioA2I.UpArmFoldComplete.PLC = GG.HSMS;
            PioA2I.UpArm1Folded.PLC = GG.HSMS;
            PioA2I.UpArm2Folded.PLC = GG.HSMS;
            PioA2I.UpArm1GlassDetect.PLC = GG.HSMS;
            PioA2I.UpArm2GlassDetect.PLC = GG.HSMS;
            PioA2I.UpArm1GlassVacuum.PLC = GG.HSMS;
            PioA2I.UpArm2GlassVacuum.PLC = GG.HSMS;
            PioA2I.UpRobotDirection.PLC = GG.HSMS;
            PioA2I.UpManualOperation.PLC = GG.HSMS;
            PioA2I.UpPinUp.PLC = GG.HSMS;
            PioA2I.UpPinDown.PLC = GG.HSMS;
            PioA2I.UpDoorOpen.PLC = GG.HSMS;
            PioA2I.UpDoorClose.PLC = GG.HSMS;
            PioA2I.UpGlassDetect.PLC = GG.HSMS;
            PioA2I.UpBodyMoving.PLC = GG.HSMS;
            PioA2I.UpBodyOriginPosition.PLC = GG.HSMS;
            PioA2I.UpEmergency.PLC = GG.HSMS;
            PioA2I.UpVertical.PLC = GG.HSMS;
            PioA2I.UpHorizontal.PLC = GG.HSMS;
        }

    }
}
