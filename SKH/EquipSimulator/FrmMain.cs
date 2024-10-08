using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EquipSimulator.Acturator;
using System.Threading;
using System.Diagnostics;
using EquipSimulator;
using Dit.Framework.PLC;
using System.Drawing.Imaging;
using EquipSimulator.Log;
using System.IO;
using Dit.Framework.Log;
using Dit.Framework.UI.UserComponent;
using Dit.Framework.Comm;

namespace EquipSimulator
{
    public partial class FrmMain : Form
    {
        private VirtualMem PLC;
        private bool _isRunning = false;
        private EquipSimul _equip = new EquipSimul();
        private FrmPcMonitor _frmPcMonitor = new FrmPcMonitor();
        public static Panel ViewPanel = null;
        private EquipView.UcrlEquipView ucrlEquipView = new EquipView.UcrlEquipView();

        public FrmMain()
        {
            CheckForIllegalCrossThreadCalls = true;
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            //// C#
            
            
            //GG.PmacSetting.LiftPin.Save(@"D:\Setting.ini");

            elementHost1.Child = ucrlEquipView;


            Logger.Log = new Dit.Framework.Log.SimpleFileLoggerMark5(Path.Combine(Application.StartupPath, "Log"), "CLIENT", 500, 1024 * 1024 * 20, lstLogger);

            GG.CCLINK = (IVirtualMem)new VirtualMem("CCLINK");
            GG.MEM_INSP = (IVirtualMem)new VirtualMem("PC_CTRL_MEM") { PLC_MEM_SIZE = 2000 };
            GG.PMAC = (IVirtualMem)new VirtualMem("PMAC");
            GG.HSMS = (IVirtualMem)new VirtualShare("DIT.PLC.HSMS_MEM.S", 102400);
            GG.EZI = (IVirtualMem)new VirtualMem("EZI");

            int cclinkResult = GG.CCLINK.Open();
            int memResult = GG.MEM_INSP.Open();
            int pmacResult = GG.PMAC.Open();
            int hsmsResult = GG.HSMS.Open();
            GG.EZI.Open();

            AddressMgr.Load(GG.ADDRESS_CC_LINK, GG.CCLINK);
            AddressMgr.Load(GG.ADDRESS_PMAC, GG.PMAC);
            AddressMgr.Load(GG.ADDRESS_PIO, GG.CCLINK);
            AddressMgr.Load(GG.ADDRESS_INSP_REVI_PC, GG.MEM_INSP);
            AddressMgr.Load(GG.CreateEziAddress(), GG.EZI);

            //btnSimulPcXy_Click(null, null);
            CheckForIllegalCrossThreadCalls = false;
            ViewPanel = pnlView;

            dgvVacuum.Rows.Add("Vacuum1");
            dgvVacuum.Rows.Add("Vacuum2");
            dgvVacuum.Rows.Add("Vacuum3");
            dgvVacuum.Rows.Add("Vacuum4");
            dgvVacuum.Rows.Add("Vacuum5");
            dgvVacuum.Rows.Add("Vacuum6");
            dgvVacuum.Rows.Add("Vacuum7");
            dgvVacuum.Rows.Add("Vacuum8");
            _equip.Vacuum1                           /**/ = new SwitchSimulEx() { Name = "VACCUM 1",      /**/ Acturator = dgvVacuum, IdxInDgv = 0};
            _equip.Vacuum2                           /**/ = new SwitchSimulEx() { Name = "VACCUM 2",      /**/ Acturator = dgvVacuum, IdxInDgv = 1};
            _equip.Vacuum3                           /**/ = new SwitchSimulEx() { Name = "VACCUM 3",      /**/ Acturator = dgvVacuum, IdxInDgv = 2};
            _equip.Vacuum4                           /**/ = new SwitchSimulEx() { Name = "VACCUM 4",      /**/ Acturator = dgvVacuum, IdxInDgv = 3};
            _equip.Vacuum5                           /**/ = new SwitchSimulEx() { Name = "VACCUM 5",      /**/ Acturator = dgvVacuum, IdxInDgv = 4};

            _equip.Blower1                           /**/ = new SwitchSimulNonX() { Name = "블로워 1",   /**/ Acturator = chkBlower1 };
            _equip.Blower2                           /**/ = new SwitchSimulNonX() { Name = "블로워 2",   /**/ Acturator = chkBlower2 };
            _equip.Blower3                           /**/ = new SwitchSimulNonX() { Name = "블로워 3",   /**/ Acturator = chkBlower3 };
            _equip.Blower4                           /**/ = new SwitchSimulNonX() { Name = "블로워 4",   /**/ Acturator = chkBlower4 };
            _equip.Blower5                           /**/ = new SwitchSimulNonX() { Name = "블로워 5",   /**/ Acturator = chkBlower5 };

            _equip.CameraCooling                         /**/ = new SwitchSimul() { Name = "Camera Cooling",    /**/ Acturator = chkCameraCooling };
            _equip.IonizerCover                          /**/ = new SwitchSimul() { Name = "Ionizer Cover",     /**/ Acturator = chkIonizerCover };
            _equip.Ionizer                               /**/ = new SwitchSimul() { Name = "Ionizer",           /**/ Acturator = chkIonizer };

            _equip.EMERGENCY_STOP_1                      /**/ = new SwitchSimul() { Name = "EMERGENCY_STOP_1",    /**/ Acturator = chkEmo01 };
            _equip.EMERGENCY_STOP_2                      /**/ = new SwitchSimul() { Name = "EMERGENCY_STOP_2",    /**/ Acturator = chkEmo02 };
            _equip.EMERGENCY_STOP_3                      /**/ = new SwitchSimul() { Name = "EMERGENCY_STOP_3",    /**/ Acturator = chkEmo03 };
            _equip.EMERGENCY_STOP_4                      /**/ = new SwitchSimul() { Name = "EMERGENCY_STOP_4",    /**/ Acturator = chkEmo04 };
            _equip.EMERGENCY_STOP_5                      /**/ = new SwitchSimul() { Name = "EMERGENCY_STOP_5",    /**/ Acturator = chkEmo05 };            
            _equip.MODE_SELECT_KEY_SW_AUTOTEACH1         /**/ = new SwitchSimul() { Name = "MODE_SELECT_KEY_SW_AUTOTEACH",    /**/ Acturator = chkSafetyModeKeySW };
            _equip.ENABLE_GRIB_SW_ON                    /**/ = new SwitchSimul() { Name = "ENABLE_GRIB_SW_ON",    /**/ Acturator = chkEnableGripSwOn };

            _equip.AlignOcrCylinder                     /**/ = new SylinderSimul(true) { Name = "Aligner Ocr Cylinder", TotalLength = 50 };
            _equip.AlignVac                             /**/ = new SwitchSimul() { Name = "Aligner Vacuum", Acturator = chkBlower1 };

            _equip.OHT_LPM1_IN1 = new SenseSimul() { Name = "OHT_LPM1_IN1" };
            _equip.OHT_LPM1_IN2 = new SenseSimul() { Name = "OHT_LPM1_IN2" };
            _equip.OHT_LPM1_IN3 = new SenseSimul() { Name = "OHT_LPM1_IN3" };
            _equip.OHT_LPM1_IN4 = new SenseSimul() { Name = "OHT_LPM1_IN4" };
            _equip.OHT_LPM1_IN5 = new SenseSimul() { Name = "OHT_LPM1_IN5" };
            _equip.OHT_LPM1_IN6 = new SenseSimul() { Name = "OHT_LPM1_IN6" };
            _equip.OHT_LPM1_IN7 = new SenseSimul() { Name = "OHT_LPM1_IN7" };
            _equip.OHT_LPM1_IN8 = new SenseSimul() { Name = "OHT_LPM1_IN8" };
            _equip.OHT_LPM2_IN1 = new SenseSimul() { Name = "OHT_LPM1_IN1" };
            _equip.OHT_LPM2_IN2 = new SenseSimul() { Name = "OHT_LPM1_IN2" };
            _equip.OHT_LPM2_IN3 = new SenseSimul() { Name = "OHT_LPM1_IN3" };
            _equip.OHT_LPM2_IN4 = new SenseSimul() { Name = "OHT_LPM1_IN4" };
            _equip.OHT_LPM2_IN5 = new SenseSimul() { Name = "OHT_LPM1_IN5" };
            _equip.OHT_LPM2_IN6 = new SenseSimul() { Name = "OHT_LPM1_IN6" };
            _equip.OHT_LPM2_IN7 = new SenseSimul() { Name = "OHT_LPM1_IN7" };
            _equip.OHT_LPM2_IN8 = new SenseSimul() { Name = "OHT_LPM1_IN8" };

            _equip.OHT_LPM1_OUT1 = new SwitchSimulEx() { Name = "OHT_LPM1_OUT1" };
            _equip.OHT_LPM1_OUT2 = new SwitchSimulEx() { Name = "OHT_LPM1_OUT2" };
            _equip.OHT_LPM1_OUT3 = new SwitchSimulEx() { Name = "OHT_LPM1_OUT3" };
            _equip.OHT_LPM1_OUT4 = new SwitchSimulEx() { Name = "OHT_LPM1_OUT4" };
            _equip.OHT_LPM1_OUT5 = new SwitchSimulEx() { Name = "OHT_LPM1_OUT5" };
            _equip.OHT_LPM1_OUT6 = new SwitchSimulEx() { Name = "OHT_LPM1_OUT6" };
            _equip.OHT_LPM1_OUT7 = new SwitchSimulEx() { Name = "OHT_LPM1_OUT7" };
            _equip.OHT_LPM1_OUT8 = new SwitchSimulEx() { Name = "OHT_LPM1_OUT8" };
            _equip.OHT_LPM2_OUT1 = new SwitchSimulEx() { Name = "OHT_LPM1_OUT1" };
            _equip.OHT_LPM2_OUT2 = new SwitchSimulEx() { Name = "OHT_LPM1_OUT2" };
            _equip.OHT_LPM2_OUT3 = new SwitchSimulEx() { Name = "OHT_LPM1_OUT3" };
            _equip.OHT_LPM2_OUT4 = new SwitchSimulEx() { Name = "OHT_LPM1_OUT4" };
            _equip.OHT_LPM2_OUT5 = new SwitchSimulEx() { Name = "OHT_LPM1_OUT5" };
            _equip.OHT_LPM2_OUT6 = new SwitchSimulEx() { Name = "OHT_LPM1_OUT6" };
            _equip.OHT_LPM2_OUT7 = new SwitchSimulEx() { Name = "OHT_LPM1_OUT7" };
            _equip.OHT_LPM2_OUT8 = new SwitchSimulEx() { Name = "OHT_LPM1_OUT8" };


            _equip.DOOR01_SENSOR                        /**/ = new SenseSimul() { Name = "TOP_DOOR1_SENSOR",       /**/ Acturator = chkDoor01 };
            _equip.DOOR02_SENSOR                        /**/ = new SenseSimul() { Name = "TOP_DOOR2_SENSOR",       /**/ Acturator = chkDoor02 };
            _equip.DOOR03_SENSOR                        /**/ = new SenseSimul() { Name = "TOP_DOOR3_SENSOR",       /**/ Acturator = chkDoor03 };
            _equip.DOOR04_SENSOR                        /**/ = new SenseSimul() { Name = "BOTTOM_DOOR1_SENSOR",       /**/ Acturator = chkDoor04 };

            _equip.IsEFEMInputArm                       /**/ = new SenseSimul() { Name = "EFEM INPUT ARM", Acturator  = chkDoor04};
            _equip.IsEFEMAlignerInputArm                /**/ = new SenseSimul() { Name = "EFEM ALIGNER INPUT ARM", Acturator = chkDoor04 };

            dgvGlassDetect.Rows.Add("Detect 1");
            dgvGlassDetect.Rows.Add("Detect 2");
            dgvGlassDetect.Rows.Add("Detect 3");

            _equip.WAFER_DETECT_SENSOR_1                  /**/ = new SwitchSimulEx() { Name = "WAFER_DETECT_SENSOR_1     ",  /**/ Acturator = dgvGlassDetect, IdxInDgv = 0 };
            _equip.WAFER_DETECT_SENSOR_2                  /**/ = new SwitchSimulEx() { Name = "WAFER_DETECT_SENSOR_2     ",  /**/ Acturator = dgvGlassDetect, IdxInDgv = 1 };
            _equip.WAFER_DETECT_SENSOR_3                  /**/ = new SwitchSimulEx() { Name = "WAFER_DETECT_SENSOR_3     ",  /**/ Acturator = dgvGlassDetect, IdxInDgv = 2 };

            _equip.GLASS_DETECT_SENSOR_1                  /**/ = new SwitchSimulEx() { Name = "GLASS_DETECT_SENSOR_1     ",  /**/ Acturator = dgvGlassDetect, IdxInDgv = 0 };
            _equip.GLASS_DETECT_SENSOR_2                  /**/ = new SwitchSimulEx() { Name = "GLASS_DETECT_SENSOR_2     ",  /**/ Acturator = dgvGlassDetect, IdxInDgv = 1 };
            _equip.GLASS_DETECT_SENSOR_3                  /**/ = new SwitchSimulEx() { Name = "GLASS_DETECT_SENSOR_3     ",  /**/ Acturator = dgvGlassDetect, IdxInDgv = 2 };

            dgvGlassEdge.Rows.Add("Crack 1");
            dgvGlassEdge.Rows.Add("Crack 2");
            dgvGlassEdge.Rows.Add("Crack 3");
            dgvGlassEdge.Rows.Add("Crack 4");
            dgvGlassEdge.Rows.Add("Crack 5");
            dgvGlassEdge.Rows.Add("Crack 6");
            dgvGlassEdge.Rows.Add("Crack 7");
            dgvGlassEdge.Rows.Add("Crack 8");
            dgvGlassEdge.Rows.Add("Crack 9");
            dgvGlassEdge.Rows.Add("Crack 10");
            _equip.GLASS_EDGE_DETECT_SENSOR_1             /**/ = new SwitchSimulEx() { Name = "GLASS_EDGE_DETECT_SENSOR_1",  /**/ Acturator = dgvGlassEdge, IdxInDgv = 0 };
            _equip.GLASS_EDGE_DETECT_SENSOR_2             /**/ = new SwitchSimulEx() { Name = "GLASS_EDGE_DETECT_SENSOR_2",  /**/ Acturator = dgvGlassEdge, IdxInDgv = 1 };
            _equip.GLASS_EDGE_DETECT_SENSOR_3             /**/ = new SwitchSimulEx() { Name = "GLASS_EDGE_DETECT_SENSOR_3",  /**/ Acturator = dgvGlassEdge, IdxInDgv = 2 };
            _equip.GLASS_EDGE_DETECT_SENSOR_4             /**/ = new SwitchSimulEx() { Name = "GLASS_EDGE_DETECT_SENSOR_4",  /**/ Acturator = dgvGlassEdge, IdxInDgv = 3 };
            _equip.GLASS_EDGE_DETECT_SENSOR_5             /**/ = new SwitchSimulEx() { Name = "GLASS_EDGE_DETECT_SENSOR_5",  /**/ Acturator = dgvGlassEdge, IdxInDgv = 4 };
            _equip.GLASS_EDGE_DETECT_SENSOR_6             /**/ = new SwitchSimulEx() { Name = "GLASS_EDGE_DETECT_SENSOR_6",  /**/ Acturator = dgvGlassEdge, IdxInDgv = 5 };
            _equip.GLASS_EDGE_DETECT_SENSOR_7             /**/ = new SwitchSimulEx() { Name = "GLASS_EDGE_DETECT_SENSOR_7",  /**/ Acturator = dgvGlassEdge, IdxInDgv = 6 };
            _equip.GLASS_EDGE_DETECT_SENSOR_8             /**/ = new SwitchSimulEx() { Name = "GLASS_EDGE_DETECT_SENSOR_8",  /**/ Acturator = dgvGlassEdge, IdxInDgv = 7 };
            _equip.GLASS_EDGE_DETECT_SENSOR_9             /**/ = new SwitchSimulEx() { Name = "GLASS_EDGE_DETECT_SENSOR_9",  /**/ Acturator = dgvGlassEdge, IdxInDgv = 8 };
            _equip.GLASS_EDGE_DETECT_SENSOR_10             /**/ = new SwitchSimulEx() { Name = "GLASS_EDGE_DETECT_SENSOR_10",  /**/ Acturator = dgvGlassEdge, IdxInDgv = 9};
            _equip.ROBOT_ARM_DETECT                       /**/ = new SwitchSimul() { Name = "ROBOT_ARM_DETECT          ",  /**/ Acturator = chkRobotArmCheck };

            _equip.ISOLATOR_1_ALARM                       /**/ = new SwitchSimul() { Name = "ISOLATOR_1_ALARM",  /**/ Acturator = chkIsol01 };
            _equip.ISOLATOR_2_ALARM                       /**/ = new SwitchSimul() { Name = "ISOLATOR_2_ALARM",  /**/ Acturator = chkIsol02 };
            _equip.ISOLATOR_3_ALARM                       /**/ = new SwitchSimul() { Name = "ISOLATOR_3_ALARM",  /**/ Acturator = chkIsol03 };
            _equip.ISOLATOR_4_ALARM                       /**/ = new SwitchSimul() { Name = "ISOLATOR_4_ALARM",  /**/ Acturator = chkIsol04 };

            _equip.IONIZER_1_ON                           /**/ = new SwitchSimul() { Name = "IONIZER_1_POWER_ON",  /**/ Acturator = chkIonizer1On };
            _equip.IONIZER_2_ON                           /**/ = new SwitchSimul() { Name = "IONIZER_2_POWER_ON",  /**/ Acturator = chkIonizer2On };
            _equip.IONIZER_3_ON                           /**/ = new SwitchSimul() { Name = "IONIZER_3_POWER_ON",  /**/ Acturator = chkIonizer3On };
            _equip.IONIZER_4_ON                           /**/ = new SwitchSimul() { Name = "IONIZER_4_POWER_ON",  /**/ Acturator = chkIonizer4On };

            _equip.IONIZER_1_ALARM                           /**/ = new SwitchSimul() { Name = "IONIZER_1_ALARM",  /**/ Acturator = chkIonizer1Alarm };
            _equip.IONIZER_2_ALARM                           /**/ = new SwitchSimul() { Name = "IONIZER_2_ALARM",  /**/ Acturator = chkIonizer2Alarm };
            _equip.IONIZER_3_ALARM                           /**/ = new SwitchSimul() { Name = "IONIZER_3_ALARM",  /**/ Acturator = chkIonizer3Alarm };
            _equip.IONIZER_4_ALARM                           /**/ = new SwitchSimul() { Name = "IONIZER_4_ALARM",  /**/ Acturator = chkIonizer4Alarm };

            _equip.InspPc                                 /**/  = new InspPcSimul();
            _equip.ReviPc                                  /**/  = new RevPCSimul();

            _equip.SetAddress();

            _equip.IsEFEMInputArm.Initialize();

            _equip.Vacuum1.Initialize();
            _equip.Vacuum2.Initialize();
            _equip.Vacuum3.Initialize();
            _equip.Vacuum4.Initialize();
            _equip.Vacuum5.Initialize();
            _equip.Blower1.Initailzie();
            _equip.Blower2.Initailzie();
            _equip.Blower3.Initailzie();
            _equip.Blower4.Initailzie();
            _equip.Blower5.Initailzie();

            _equip.CameraCooling.Initialize();
            _equip.IonizerCover.Initialize();
            _equip.Ionizer.Initialize();

            _equip.EMERGENCY_STOP_1.Initialize();
            _equip.EMERGENCY_STOP_2.Initialize();
            _equip.EMERGENCY_STOP_3.Initialize();
            _equip.EMERGENCY_STOP_4.Initialize();
            _equip.EMERGENCY_STOP_5.Initialize();            
            _equip.MODE_SELECT_KEY_SW_AUTOTEACH1.Initialize();

            _equip.DOOR01_SENSOR.Initialize();
            _equip.DOOR02_SENSOR.Initialize();
            _equip.DOOR03_SENSOR.Initialize();
            _equip.DOOR04_SENSOR.Initialize();

            _equip.WAFER_DETECT_SENSOR_1.Initialize();
            _equip.WAFER_DETECT_SENSOR_2.Initialize();
            _equip.WAFER_DETECT_SENSOR_3.Initialize();

            _equip.GLASS_DETECT_SENSOR_1.Initialize();
            _equip.GLASS_DETECT_SENSOR_2.Initialize();
            _equip.GLASS_DETECT_SENSOR_3.Initialize();
            _equip.GLASS_EDGE_DETECT_SENSOR_1.Initialize();
            _equip.GLASS_EDGE_DETECT_SENSOR_2.Initialize();
            _equip.GLASS_EDGE_DETECT_SENSOR_3.Initialize();
            _equip.GLASS_EDGE_DETECT_SENSOR_4.Initialize();
            _equip.GLASS_EDGE_DETECT_SENSOR_5.Initialize();
            _equip.GLASS_EDGE_DETECT_SENSOR_6.Initialize();
            _equip.GLASS_EDGE_DETECT_SENSOR_7.Initialize();
            _equip.GLASS_EDGE_DETECT_SENSOR_8.Initialize();
            _equip.GLASS_EDGE_DETECT_SENSOR_9.Initialize();
            _equip.GLASS_EDGE_DETECT_SENSOR_10.Initialize();
            _equip.ROBOT_ARM_DETECT.Initialize();

            _equip.ISOLATOR_1_ALARM.Initialize();
            _equip.ISOLATOR_2_ALARM.Initialize();
            _equip.ISOLATOR_3_ALARM.Initialize();
            _equip.ISOLATOR_4_ALARM.Initialize();

            _equip.IONIZER_1_ON.Initialize();
            _equip.IONIZER_2_ON.Initialize();
            _equip.IONIZER_3_ON.Initialize();
            _equip.IONIZER_4_ON.Initialize();

            _equip.IONIZER_1_ALARM.Initialize();
            _equip.IONIZER_2_ALARM.Initialize();
            _equip.IONIZER_3_ALARM.Initialize();
            _equip.IONIZER_4_ALARM.Initialize();


           _equip.InspPc.Initialize();
           _equip.ReviPc.Initialize();




            _equip.StandCentering1.Initialize();
            _equip.StandCentering2.Initialize();

            _equip.AssistCentering1.Initialize();
            _equip.AssistCentering2.Initialize();

            _equip.FrontCentering1.Initialize();
            _equip.FrontCentering2.Initialize();

            _equip.RearCentering1.Initialize();
            _equip.RearCentering2.Initialize();
                       
            _equip.StageX.Initialize();
            _equip.Y1.Initialize();            
            _equip.Theta.Initialize();


            ucrlServoIX.Servo = _equip.StageX;
            ucrlServoIY.Servo = _equip.Y1;
            ucrlServoRY_Under.Servo = _equip.Theta;

            ucrlServoRY.Servo = _equip.StageX;            
            ucrlServoZ1.Servo = _equip.StageX;
            ucrlServoPin.Servo = _equip.StageX;
            ucrlServoZ2.Servo = _equip.StageX;
            ucrlServoZ3.Servo = _equip.StageX;

            ucrlServoIX.Tiltel = "X";
            ucrlServoIY.Tiltel = "Y";            
            ucrlServoRY_Under.Tiltel = "Theta";

            ucrlServoRY.Tiltel = "X";
            ucrlServoZ1.Tiltel = "X";
            ucrlServoPin.Tiltel = "X";
            ucrlServoZ2.Tiltel = "X";
            ucrlServoZ3.Tiltel = "X";

            //pictureBox2.BackgroundImage = ChangeOpacity(img, 50);
            //pictureBox2.BackColor = Color.Transparent;
            //pictureBox2.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            _isRunning = true;
            bgWorker.RunWorkerAsync();

            Logger.Log.AppendLine(LogLevel.Info, "Simulator 시작.............");
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            _isRunning = false;
            while (bgWorker.IsBusy)
                Application.DoEvents();

            base.OnClosing(e);
        }

        private void WritStruct()
        {
        }
        private void btnGantryXMove_Click(object sender, EventArgs e)
        {
            //int x = (int)nudGantryX.Value;
            //GantryX.TargetPosi = x;
            //GantryX.GoOn = true;
        }
        private void btnGantryYMove_Click(object sender, EventArgs e)
        {
            //int y = (int)nudGantryY.Value;
            //GantryInspY.TargetPosi = y;
            //GantryInspY.GoOn = true;
        }
        private void btnMainForward_Click(object sender, EventArgs e)
        {
            //_equip.Main1Clder.MoveFront = true;
            //_equip.Main2Clder.MoveFront = true;
        }
        private void btnMainBack_Click(object sender, EventArgs e)
        {
            //_equip.Main1Clder.MoveBack = true;
            //_equip.Main2Clder.MoveBack = true;
        }
        private void btnSubForward_Click(object sender, EventArgs e)
        {
            //_equip.Sub1Clder.MoveFront = true;
            //_equip.Sub2Clder.MoveFront = true;
        }
        private void btnSubBack_Click(object sender, EventArgs e)
        {
            //_equip.Sub1Clder.MoveBack = true;
            //_equip.Sub2Clder.MoveBack = true;
        }
        private void btnFrontForward_Click(object sender, EventArgs e)
        {
            //_equip.Front1Clder.MoveFront = true;
            //_equip.Front2Clder.MoveFront = true;
        }
        private void btnFrontBack_Click(object sender, EventArgs e)
        {
            //_equip.Front1Clder.MoveBack = true;
            //_equip.Front2Clder.MoveBack = true;
        }
        private void btnBackForward_Click(object sender, EventArgs e)
        {
            //_equip.Back1Clder.MoveFront = true;
            //_equip.Back2Clder.MoveFront = true;
        }
        private void btnBackBack_Click(object sender, EventArgs e)
        {
            //Back1Clder.MoveBack = true;
            //Back2Clder.MoveBack = true;
        }
        private void btnPinUp2_Click(object sender, EventArgs e)
        {
            //PinServo.TargetPosi = 100;
            //PinServo.GoOn = true;
        }
        private void btnPinUp1_Click(object sender, EventArgs e)
        {
            //PinServo.TargetPosi = 75;
            //PinServo.GoOn = true;
        }
        private void btnPinDown1_Click(object sender, EventArgs e)
        {
            //PinServo.TargetPosi = 25;
            //PinServo.GoOn = true;
        }
        private void btnPinDown2_Click(object sender, EventArgs e)
        {
            //PinServo.TargetPosi = 0;
            //PinServo.GoOn = true;
        }

        private void btnShowBottomButton_Click(object sender, EventArgs e)
        {
            ButtonLabel btn = sender as ButtonLabel;
            if (btn == null) return;

        }

        private void nudGantryReviewMove_Click(object sender, EventArgs e)
        {

        }

        int iPos = 0;
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (_isRunning)
            {
                _equip.Working();
                Thread.Sleep(10);


                bgWorker.ReportProgress(iPos++);

                if (iPos > 10000)
                    iPos = 0;
            }
        }
        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //pnlView.Refresh();
        }

        private void btnMemDetail_Click(object sender, EventArgs e)
        {
            _frmPcMonitor.Show();
        }

        private void btnXyIo_Click(object sender, EventArgs e)
        {
            //_frmCtrl.axActEasyIF1.ActLogicalStationNumber = 150;
            //FrmXYMonitor2 ff = new FrmXYMonitor2();
            //ff.Show();
        }

        private void tmrState_Tick(object sender, EventArgs e)
        {
            tbStatus.Text = GetCurrentStatus();
        }
        private string GetCurrentStatus()
        {
            Process p = Process.GetCurrentProcess();
            string s = string.Format(" 스레드 {0}, 핸들 {1}, 메모리 사용 {2}kb, 피크 메모리 사용 {3}kb, 버전 {4}", p.Threads.Count, p.HandleCount, p.WorkingSet64 / 1024, p.PeakWorkingSet64 / 1024, Application.ProductVersion);
            p.Close();
            p.Dispose();
            return s;
        }


        public Bitmap ChangeOpacity(Image img, float opacityvalue)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            ColorMatrix colormatrix = new ColorMatrix();
            colormatrix.Matrix33 = opacityvalue;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
            graphics.Dispose();
            return bmp;
        }

        private void tmrWorker_Tick(object sender, EventArgs e) 
        {
            ucrlEquipView.StageX = (double)_equip.StageX.XF_CurrMotorPosition.vFloat;
            ucrlEquipView.StageY = (double)_equip.Y1.XF_CurrMotorPosition.vFloat;

            //ucrlEquipView.StdCentering1 = (_equip.StandCentering1.TotalLength - _equip.StandCentering1.CurrPosition) * 50f / _equip.StandCentering1.TotalLength;
            //ucrlEquipView.StdCentering2 = (_equip.StandCentering2.TotalLength - _equip.StandCentering2.CurrPosition) * 50f / _equip.StandCentering2.TotalLength;

            //ucrlEquipView.AssiCentering1 = (_equip.AssistCentering1.TotalLength - _equip.AssistCentering1.CurrPosition) * 50f / _equip.AssistCentering1.TotalLength;
            //ucrlEquipView.AssiCentering2 = (_equip.AssistCentering2.TotalLength - _equip.AssistCentering2.CurrPosition) * 50f / _equip.AssistCentering2.TotalLength;

            //ucrlEquipView.RearCentering1 = (_equip.RearCentering1.TotalLength - _equip.RearCentering1.CurrPosition) * 50f / _equip.RearCentering1.TotalLength;
            //ucrlEquipView.RearCentering2 = (_equip.RearCentering2.TotalLength - _equip.RearCentering2.CurrPosition) * 50f / _equip.RearCentering2.TotalLength;
            //ucrlEquipView.RearCentering3 = (_equip.RearCentering3.TotalLength - _equip.RearCentering3.CurrPosition) * 50f / _equip.RearCentering3.TotalLength;
            //ucrlEquipView.RearCentering4 = (_equip.RearCentering4.TotalLength - _equip.RearCentering4.CurrPosition) * 50f / _equip.RearCentering4.TotalLength;
                        
            //ucrlEquipView.FrontCenteringFb1 = (_equip.FrontCentering1.TotalLength - _equip.FrontCentering1.CurrPosition) * 50f / _equip.FrontCentering1.TotalLength;
            //ucrlEquipView.FrontCenteringFb2 = (_equip.FrontCentering2.TotalLength - _equip.FrontCentering2.CurrPosition) * 50f / _equip.FrontCentering2.TotalLength;


            //ucrlEquipView.GlassOrgExist =
            //                            _equip.GLASS_EDGE_DETECT_SENSOR_2.XB_OnOff ||
            //                            _equip.GLASS_EDGE_DETECT_SENSOR_3.XB_OnOff ||
            //                            _equip.GLASS_EDGE_DETECT_SENSOR_5.XB_OnOff ||
            //                            _equip.GLASS_EDGE_DETECT_SENSOR_6.XB_OnOff ||
            //                            _equip.GLASS_EDGE_DETECT_SENSOR_7.XB_OnOff ||
            //                            _equip.GLASS_EDGE_DETECT_SENSOR_8.XB_OnOff ||
            //                            _equip.GLASS_EDGE_DETECT_SENSOR_9.XB_OnOff ||
            //                            _equip.GLASS_EDGE_DETECT_SENSOR_10.XB_OnOff;
            //ucrlEquipView.GlassLeftExist =
            //    (_equip.GLASS_EDGE_DETECT_SENSOR_5.XB_OnOff || _equip.GLASS_EDGE_DETECT_SENSOR_8.XB_OnOff) == false && 
            //    _equip.GLASS_EDGE_DETECT_SENSOR_1.XB_OnOff ||
            //    _equip.GLASS_EDGE_DETECT_SENSOR_2.XB_OnOff ||
            //    _equip.GLASS_EDGE_DETECT_SENSOR_3.XB_OnOff ||
            //    _equip.GLASS_EDGE_DETECT_SENSOR_4.XB_OnOff;
            //ucrlEquipView.GlassRightExist =
            //    (_equip.GLASS_EDGE_DETECT_SENSOR_2.XB_OnOff || _equip.GLASS_EDGE_DETECT_SENSOR_3.XB_OnOff) == false && 
            //    _equip.GLASS_EDGE_DETECT_SENSOR_5.XB_OnOff ||
            //    _equip.GLASS_EDGE_DETECT_SENSOR_6.XB_OnOff ||
            //    _equip.GLASS_EDGE_DETECT_SENSOR_7.XB_OnOff ||
            //    _equip.GLASS_EDGE_DETECT_SENSOR_8.XB_OnOff;

            //ucrlEquipView.RobotArmCheck =! _equip.ROBOT_ARM_DETECT.XB_OnOff.vBit;
            //ucrlEquipView.Vacuum.Stage1On = _equip.Vacuum1.XB_OnOff;            
            //ucrlEquipView.Vacuum.Stage2On = _equip.Vacuum2.XB_OnOff;
            //ucrlEquipView.StageVacuum3On = _equip.Vacuum3.XB_OnOff;
            //ucrlEquipView.StageVacuum4On = _equip.Vacuum4.XB_OnOff;
            //ucrlEquipView.StageVacuum5On = _equip.Vacuum5.XB_OnOff;

            //chkEmo01.BackColor = _equip.EMERGENCY_STOP_1.YB_OnOff ? Color.Red : Color.Gray;
            //chkEmo02.BackColor = _equip.EMERGENCY_STOP_2.YB_OnOff ? Color.Red : Color.Gray;
            //chkEmo03.BackColor = _equip.EMERGENCY_STOP_3.YB_OnOff ? Color.Red : Color.Gray;
            //chkEmo04.BackColor = _equip.EMERGENCY_STOP_4.YB_OnOff ? Color.Red : Color.Gray;
            //chkEmo05.BackColor = _equip.EMERGENCY_STOP_5.YB_OnOff ? Color.Red : Color.Gray;
            
            //chkSafetyModeKeySW.BackColor = _equip.MODE_SELECT_KEY_SW_AUTOTEACH1.YB_OnOff /*|| _equip.MODE_SELECT_KEY_SW_AUTOTEACH2.YB_OnOff*/ ? Color.Red : Color.Gray;            
            //chkRobotArmCheck.BackColor = _equip.ROBOT_ARM_DETECT.YB_OnOff ? Color.Red : Color.Gray;

            //chkInspXError.BackColor = _equip.StageX.XB_ErrAmpFaultError.vBit ? Color.Red : Color.Gray;
            //lblUpSendAble.BackColor = _equip.PioI2A.UpSendAble ? Color.Red : Color.Gray;
            //lblUpSendStart.BackColor = _equip.PioI2A.UpSendStart ? Color.Red : Color.Gray;
            //lblUpSendComplete.BackColor = _equip.PioI2A.UpSendComplete ? Color.Red : Color.Gray;

            //lblAoiRecvAble.BackColor = _equip.PioI2A.LoReceiveAble ? Color.Red : Color.Gray;
            //lblAoiRecvStart.BackColor = _equip.PioI2A.LoReceiveStart ? Color.Red : Color.Gray;
            //lblAoiRecvComplete.BackColor = _equip.PioI2A.LoReceiveComplete ? Color.Red : Color.Gray;


            //lblAoiSendAble.BackColor = _equip.PioA2I.UpSendAble ? Color.Red : Color.Gray;
            //lblAoiSendStart.BackColor = _equip.PioA2I.UpSendStart ? Color.Red : Color.Gray;
            //lblAoiSendComplete.BackColor = _equip.PioA2I.UpSendComplete ? Color.Red : Color.Gray;

            //lblLoRecvAble.BackColor = _equip.PioA2I.LoReceiveAble ? Color.Red : Color.Gray;
            //lblLoRecvStart.BackColor = _equip.PioA2I.LoReceiveStart ? Color.Red : Color.Gray;
            //lblLoRecvComplete.BackColor = _equip.PioA2I.LoReceiveComplete ? Color.Red : Color.Gray;

            //chkReviLoading.BackColor = _equip.ReviPc.YB_Loading.vBit ? Color.Red : Color.Gray;
            //chkReviLoadingAck.BackColor = _equip.ReviPc.XB_LoadingAck.vBit ? Color.Red : Color.Gray;
            //chkReviAlignStart.BackColor = _equip.ReviPc.YB_AlignStart.vBit ? Color.Red : Color.Gray;
            //chkReviAlignStartAck.BackColor = _equip.ReviPc.XB_AlignStartAck.vBit ? Color.Red : Color.Gray;
            //chkReviReviStart.BackColor = _equip.ReviPc.YB_ReviewStart.vBit ? Color.Red : Color.Gray;
            //chkReviReviStartAck.BackColor = _equip.ReviPc.XB_ReviewStartAck.vBit ? Color.Red : Color.Gray;
            //chkReviReviEndAck.BackColor = _equip.ReviPc.YB_ReviewCompleteAck.vBit ? Color.Red : Color.Gray;
            //chkReviReviEnd.BackColor = _equip.ReviPc.XB_ReviewComplete.vBit ? Color.Red : Color.Gray;
            //chkReviLotStart.BackColor = _equip.ReviPc.YB_LotStart.vBit ? Color.Red : Color.Gray;
            //chkReviLotStartAck.BackColor = _equip.ReviPc.XB_LotStartAck.vBit ? Color.Red : Color.Gray;
            //chkReviLotEnd.BackColor = _equip.ReviPc.YB_LotEnd.vBit ? Color.Red : Color.Gray;
            //chkReviLotEndAck.BackColor = _equip.ReviPc.XB_LotEndAck.vBit ? Color.Red : Color.Gray;
            //chkReviUnloading.BackColor = _equip.ReviPc.YB_Unloading.vBit ? Color.Red : Color.Gray;
            //chkReviUnloadingAck.BackColor = _equip.ReviPc.XB_UnloadingAck.vBit ? Color.Red : Color.Gray;
          
            //lblInspGLoading.BackColor = _equip.InspPc.YB_Loading.vBit ? Color.Red : Color.Gray;
            //lblInspGLoadingOk.BackColor = _equip.InspPc.XB_LoadingSuccess.vBit ? Color.Red : Color.Gray;
            //lblInspScanReady.BackColor = _equip.InspPc.YB_ScanReady.vBit ? Color.Red : Color.Gray;
            //lblInspScanReadyOk.BackColor = _equip.InspPc.XB_ScanReadySuccess.vBit ? Color.Red : Color.Gray;
            //lblInspScanStart.BackColor = _equip.InspPc.YB_ScanStart.vBit ? Color.Red : Color.Gray;
            //lblInspScanStartOk.BackColor = _equip.InspPc.XB_ScanStartSuccess.vBit ? Color.Red : Color.Gray;
            //lblInspScanEnd.BackColor = _equip.InspPc.YB_ScanEnd.vBit ? Color.Red : Color.Gray;
            //lblInspScanEndOk.BackColor = _equip.InspPc.XB_ScanEndSuccess.vBit ? Color.Red : Color.Gray;
            //lblInspGUnloading.BackColor = _equip.InspPc.YB_Unloading.vBit ? Color.Red : Color.Gray;
            //lblInspGUnloadingOk.BackColor = _equip.InspPc.XB_UnloadingSuccess.vBit ? Color.Red : Color.Gray;
            //lblScanStep.Text = string.Format("{0}/{1}", _equip.InspPc.ScanIndex, _equip.InspPc.ScanCount);

            //ucrlServoIX.Update();
            //ucrlServoIY.Update();
            //ucrlServoRY.Update();
            //ucrlServoRY_Under.Update();
            //ucrlServoPin.Update();
            //ucrlServoZ1.Update();
            //ucrlServoZ2.Update();
            //ucrlServoZ3.Update();
        }


        private void btnCtrlPcXy_Click(object sender, EventArgs e)
        {
            btnCtrlPcXy.BackColor = Color.Blue;
            btnSimulPcXy.BackColor = Color.Gray;
            btnTester.BackColor = Color.Gray;

            //X000.SetAddr(0);
            //Y000.SetAddr(0);
            //UMAC.SetAddr(0);

        }
        private void btnSimulPcXy_Click(object sender, EventArgs e)
        {
            btnCtrlPcXy.BackColor = Color.Gray;
            btnSimulPcXy.BackColor = Color.Blue;
            btnTester.BackColor = Color.Gray;

            //X000.SetAddr(1);
            //Y000.SetAddr(1);
            //UMAC.SetAddr(1);

        }
        private void btnTester_Click(object sender, EventArgs e)
        {
            btnCtrlPcXy.BackColor = Color.Gray;
            btnSimulPcXy.BackColor = Color.Gray;
            btnTester.BackColor = Color.Blue;

            //X000.SetAddr(2);
            //Y000.SetAddr(2);
            //UMAC.SetAddr(2);
        }

        private void btnGXMoveJogMinus_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void btnGXMoveJogMinus_MouseUp(object sender, MouseEventArgs e)
        {

        }
        private void btnInspXMoveJogPlus_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void btnInspXMoveJogPlus_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void btnInspYMoveJogMinus_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void btnInspYMoveJogMinus_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void btnInspYMoveJogPlus_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void btnInspYMoveJogPlus_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void btnRvYMoveJogMinus_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void btnRvYMoveJogMinus_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void btnRvYMoveJogPlus_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void btnRvYMoveJogPlus_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void btnLiftPinMoveJogMinus_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void btnLiftPinMoveJogMinus_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void btnLiftPinYMoveJogPlus_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void btnLiftPinYMoveJogPlus_MouseUp(object sender, MouseEventArgs e)
        {
        }
        private void btnThetaMoveJogMinus_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void btnThetaMoveJogMinus_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void btnTheltaYMoveJogPlus_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void btnTheltaYMoveJogPlus_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void LnFrontSideF_Click(object sender, EventArgs e)
        {

        }

        private void LnLiftPin_Click(object sender, EventArgs e)
        {

        }

        private void shThelta_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnXyIoAddr_Click(object sender, EventArgs e)
        {
            //FrmXYMonitorTemp ff = new FrmXYMonitorTemp(btnCtrlPcXy.BackColor == Color.Blue);
            //ff.Show();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnInspXMoveJogMinus_Click(object sender, EventArgs e)
        {
            ucrlEquipView.StageX = ucrlEquipView.StageX - 10;
        }
        private void btnInspXMoveJogPlus_Click(object sender, EventArgs e)
        {
            ucrlEquipView.StageX = ucrlEquipView.StageX + 10;
        }
        private void btnInspYMoveJogMinus_Click(object sender, EventArgs e)
        {
        }
        private void btnInspYMoveJogPlus_Click(object sender, EventArgs e)
        {
        }
        private void btnLiftPinMoveJogMinus_Click(object sender, EventArgs e)
        {
        }
        private void btnLiftPinYMoveJogPlus_Click(object sender, EventArgs e)
        {
        }
        private void btnThetaMoveJogMinus_Click(object sender, EventArgs e)
        {
            //ucrlEquipView.Thelta = ucrlEquipView.Thelta - 50;
        }
        private void btnTheltaYMoveJogPlus_Click(object sender, EventArgs e)
        {
            //ucrlEquipView.Thelta = ucrlEquipView.Thelta + 50;
        }

        private void chkInspXReply_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkInspYReply_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkReviYReply_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkLiftPinReply_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkThetaReply_CheckedChanged(object sender, EventArgs e)
        {
        }


        private void chkUseInterlock_CheckedChanged(object sender, EventArgs e)
        {
            chkUseInterlock.BackColor = chkUseInterlock.Checked ? Color.Red : Color.Gray;

            _equip.InterLock = chkUseInterlock.Checked;
        }

        private void btnSendAbleStart_Click(object sender, EventArgs e)
        {
            RadioButton[] rd = new RadioButton[] {
                rdInputGlsOrg, rdInputGlsSep, rdInputGlsLeft, rdInputGlsRight
            };
            _equip.PioI2A.InputGlsType = (EquipSimulator.GG.EmGlassType)Array.IndexOf(rd, rd.FirstOrDefault(r => r.Checked));
            _equip.PioI2A.StepPioSend = 10;
            _equip.PioI2A.UpSendAble.vBit = true;
        }

        private void btnRecvAbleStart_Click(object sender, EventArgs e)
        {
            _equip.PioA2I.StepPioRecv = 10;
            _equip.PioA2I.LoReceiveAble.vBit = true;
        }

        private void chkInspXVeriPanelEndEnd_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkReviReviEnd_CheckedChanged(object sender, EventArgs e)
        {
            _equip.ReviPc.LstReviPcEvent[(int)EmReviPcEvent.REVIEW_COMPLETE].StartEvent();
        }

        private void chkInspXError_CheckedChanged(object sender, EventArgs e)
        {
            _equip.StageX.XB_ErrAmpFaultError.vBit = !_equip.StageX.XB_ErrAmpFaultError.vBit;
        }

        private void ucrlServoIX_Load(object sender, EventArgs e)
        {

        }

        private void btnAllDoor_Click(object sender, EventArgs e)
        {
            bool changed = !_equip.DOOR01_SENSOR.Acturator.Checked;
            _equip.DOOR01_SENSOR.Acturator.Checked = changed;
            _equip.DOOR02_SENSOR.Acturator.Checked = changed;
            _equip.DOOR03_SENSOR.Acturator.Checked = changed;
            _equip.DOOR04_SENSOR.Acturator.Checked = changed;
        }

        private void lblUpSendAble_Click(object sender, EventArgs e)
        {
            _equip.PioI2A.UpSendAble.vBit = !_equip.PioI2A.UpSendAble.vBit;
        }

        private void lblUpSendStart_Click(object sender, EventArgs e)
        {
            _equip.PioI2A.UpSendStart.vBit = !_equip.PioI2A.UpSendStart.vBit;
        }

        private void lblUpSendComplete_Click(object sender, EventArgs e)
        {
            _equip.PioI2A.UpSendComplete.vBit = true;
        }

        private void lblAoiRecvAble_Click(object sender, EventArgs e)
        {
            _equip.PioI2A.LoReceiveAble.vBit = !_equip.PioI2A.LoReceiveAble.vBit;
        }

        private void lblAoiRecvStart_Click(object sender, EventArgs e)
        {
            _equip.PioI2A.LoReceiveStart.vBit = !_equip.PioI2A.LoReceiveStart.vBit;
        }

        private void lblAoiRecvComplete_Click(object sender, EventArgs e)
        {
            _equip.PioI2A.LoReceiveComplete.vBit = !_equip.PioI2A.LoReceiveComplete.vBit;
        }

        private void lblLoRecvAble_Click(object sender, EventArgs e)
        {
            _equip.PioA2I.LoReceiveAble.vBit = !_equip.PioA2I.LoReceiveAble.vBit;
        }

        private void lblLoRecvStart_Click(object sender, EventArgs e)
        {
            _equip.PioA2I.LoReceiveStart.vBit = !_equip.PioA2I.LoReceiveStart.vBit;
        }

        private void lblLoRecvComplete_Click(object sender, EventArgs e)
        {
            _equip.PioA2I.LoReceiveComplete.vBit = !_equip.PioA2I.LoReceiveComplete.vBit;
        }

        private void lblAoiSendAble_Click(object sender, EventArgs e)
        {
            _equip.PioA2I.UpSendAble.vBit = !_equip.PioA2I.UpSendAble.vBit;
        }

        private void lblAoiSendStart_Click(object sender, EventArgs e)
        {
            _equip.PioA2I.UpSendStart.vBit = !_equip.PioA2I.UpSendStart.vBit;
        }

        private void lblAoiSendComplete_Click(object sender, EventArgs e)
        {
            _equip.PioA2I.UpSendComplete.vBit = !_equip.PioA2I.UpSendComplete.vBit;
        }

        private void btnGlassCrackOrgOn_Click(object sender, EventArgs e)
        {
            _equip.GLASS_DETECT_SENSOR_1.OnOff(true);
            _equip.GLASS_DETECT_SENSOR_2.OnOff(true);
            _equip.GLASS_DETECT_SENSOR_3.OnOff(true);

            _equip.GLASS_EDGE_DETECT_SENSOR_1.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_2.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_3.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_4.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_5.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_6.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_7.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_8.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_9.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_10.OnOff(true);
        }

        private void btnGlassCrackSepOn_Click(object sender, EventArgs e)
        {
            _equip.GLASS_DETECT_SENSOR_1.OnOff(true);
            _equip.GLASS_DETECT_SENSOR_2.OnOff(true);
            _equip.GLASS_DETECT_SENSOR_3.OnOff(false);

            _equip.GLASS_EDGE_DETECT_SENSOR_1.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_2.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_3.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_4.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_5.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_6.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_7.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_8.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_9.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_10.OnOff(true);
        }
        
        private void btnCrackOff_Click(object sender, EventArgs e)
        {
            _equip.GLASS_EDGE_DETECT_SENSOR_1.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_2.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_3.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_4.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_5.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_6.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_7.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_8.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_9.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_10.OnOff(false);
        }

        private void btnGlassCrackLeftOn_Click(object sender, EventArgs e)
        {
            _equip.GLASS_DETECT_SENSOR_1.OnOff(true);
            _equip.GLASS_DETECT_SENSOR_2.OnOff(false);
            _equip.GLASS_DETECT_SENSOR_3.OnOff(false);

            _equip.GLASS_EDGE_DETECT_SENSOR_1.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_2.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_3.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_4.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_5.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_6.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_7.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_8.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_9.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_10.OnOff(true);
        }

        private void btnGlassCrackRightOn_Click(object sender, EventArgs e)
        {
            _equip.GLASS_DETECT_SENSOR_1.OnOff(false);
            _equip.GLASS_DETECT_SENSOR_2.OnOff(true);
            _equip.GLASS_DETECT_SENSOR_3.OnOff(false);

            _equip.GLASS_EDGE_DETECT_SENSOR_1.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_2.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_3.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_4.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_5.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_6.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_7.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_8.OnOff(true);
            _equip.GLASS_EDGE_DETECT_SENSOR_9.OnOff(false);
            _equip.GLASS_EDGE_DETECT_SENSOR_10.OnOff(false);
        }
    }
}