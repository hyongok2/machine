using System;
using System.Linq;
using Dit.Framework.PLC;
using EquipMainUi.Struct.Detail;
using EquipMainUi.Struct.Step;
using EquipMainUi.Struct.BaseUnit;
using EquipMainUi.Setting;
using EquipMainUi.Monitor;
using DitCim.PLC;
using EquipMainUi.Struct.Detail.Cylinder;
using EquipMainUi.Struct.Detail.Adc;
using Dit.Framework.Comm;
using Dit.Framework.SystemAccess;
using EquipMainUi.Struct.Detail.Servo;
using System.Collections.Generic;
using EquipMainUi.Struct.Detail.EFEM;
using EquipMainUi.Struct.TransferData;
using EquipMainUi.Tact;
using System.Text;
using EquipMainUi.RecipeManagement;
using EquipMainUi.Struct.Detail.HSMS.FileInterface;
using EquipMainUi.Struct.Detail.HSMS;
using System.Threading;
using EquipMainUi.Struct.Detail.HSMS.ReportStruct;
using static EquipMainUi.Struct.Detail.HSMS.FileInterface.ECIDDataSetting;
using EquipMainUi.Struct.Detail.OHT;
using Dit.Framework.Analog;
using EquipMainUi.Struct.Detail.EFEM.Step;
using EquipMainUi.PreAligner;
using EquipMainUi.Struct.Detail.EziStep;
using EquipMainUi.Struct.Detail.EziStepMotor;
using EquipMainUi.PreAligner.Recipe;
using EquipMainUi.Log;

namespace EquipMainUi.Struct
{
    public enum EmEziStep
    {
        //2호기 기준
        AlignerT = 0,
        AlignerY,
        AlignerX,
    }
    public enum EmReserveReInsp
    {
        DISABLE,
        RESERVE,
        READY,
        START,
    }
    public enum EmReserveInsp_Stop
    {
        DISABLE,
        RESERVE,
        READY_FOR_START,
        STOP,
    }

    public enum EmGrapSw
    {
        NORMAL,
        MIDDLE_ON,
        EMERGENCY_ON,
        ERROR
    }

    //hyy
    public enum EmInspSeqMode
    {
        BACK = 0,
        FRONT = 1,
        BACK_AND_FRONT = 2,
        FRONT_AND_BACK = 3,
        UNKNOWN = -1
    }
    public enum EmInspMode
    {
        UNKNOWN = -1,
        BACK = 0,
        FRONT = 1,
    }
    public enum EmInspOrder
    {
        ORIGIN,
        FRONT_REAR,
        REARONLY,
        FRONTONLY,
        NOT
    }
    public enum EmTrigger
    {
        OneD = 0,
        TwoD,
        ThreeD
    }
    public enum EmInspectionType
    {
        OneD,
        TwoD,
        Both
    }
    public enum EmCimMode
    {
        None = 0,
        OffLine = 1,
        Local = 2,
        Remote = 3,
    }
    public class Equipment
    {
        #region IO
        public Sensor EmsOutside1                                   /**/ = new Sensor();
        public Sensor EmsOutside2                                   /**/ = new Sensor();
        public Sensor EmsOutside3                                   /**/ = new Sensor();

        public ModeSelectKeyBox ModeSelectKey = new ModeSelectKeyBox();

        public Sensor SERVO_MC_POWER_ON_1                           /**/ = new Sensor(false);

        public OffCheckSwitch TopDoor01                             /**/ = new OffCheckSwitch();
        public OffCheckSwitch TopDoor02                             /**/ = new OffCheckSwitch();
        public OffCheckSwitch TopDoor03                             /**/ = new OffCheckSwitch();
        public OffCheckSwitch TopDoor04                             /**/ = new OffCheckSwitch();

        public OffCheckSwitch EfemDoor01                             /**/ = new OffCheckSwitch();
        public OffCheckSwitch EfemDoor02                             /**/ = new OffCheckSwitch();

        public Sensor WaferDetectSensorLiftpin1                            /**/ = new Sensor();
        public Sensor WaferDetectSensorLiftpin2                            /**/ = new Sensor();
        public Sensor WaferDetectSensorStage1                            /**/ = new Sensor();

        public StandCenteringProxy StandCentering                   /**/ = new StandCenteringProxy(2);
        public LiftPinProxy LiftPin                                 /**/ = new LiftPinProxy(2);

        public Sensor SafeyPlcError                                   /**/        = new Sensor();
        public Switch SafetyCircuitReset                    /**/ = new Switch();

        public TowerLampProxy TowerLamp = new TowerLampProxy();
        public WorkingLightProxy WorkingLight = new WorkingLightProxy();

        public PinIonizerProxy PinIonizer                           /**/ = new PinIonizerProxy("PinIonizer", 1, 1);

        public Switch BuzzerK1                              /**/ = new Switch();
        public Switch BuzzerK2                              /**/ = new Switch();
        public Switch BuzzerK3                              /**/ = new Switch();
        public Switch BuzzerK4                              /**/ = new Switch();

        public DelaySensor Isolator1                                     /**/ = new DelaySensor();
        public DelaySensor Isolator2                                     /**/ = new DelaySensor();
        public DelaySensor Isolator3                                     /**/ = new DelaySensor();

        /// <summary>
        /// Centering, blow, isolator
        /// </summary>
        public DelaySensor MainAir1                                 /**/ = new DelaySensor(false);
        /// <summary>
        /// ionizer, cam cooling
        /// </summary>
        public DelaySensor MainAir2                                 /**/ = new DelaySensor(false);

        public DelaySensor MainVacuum1                                   /**/ = new DelaySensor(false);
        public DelaySensor MainVacuum2                                   /**/ = new DelaySensor(false);

        public Sensor RobotArmDefect                        /**/ = new Sensor(false);
        public Sensor PC_RACK_FAN_OFF_1 = new Sensor();
        public Sensor PC_RACK_FAN_OFF_2 = new Sensor();
        public Sensor PC_RACK_FAN_OFF_3 = new Sensor();
        public Sensor PC_RACK_FAN_OFF_4 = new Sensor();
        public Sensor PC_RACK_FAN_OFF_5 = new Sensor();
        public Sensor PC_RACK_FAN_OFF_6 = new Sensor();
        public Sensor PC_RACK_FAN_OFF_7 = new Sensor();
        public Sensor PC_RACK_FAN_OFF_8 = new Sensor();
        public Sensor CP_BOX_FAN_OFF_1 = new Sensor();
        public Sensor CP_BOX_FAN_OFF_2 = new Sensor();
        public Sensor CP_BOX_FAN_OFF_3 = new Sensor();
        public Sensor CP_BOX_FAN_OFF_4 = new Sensor();
        public Sensor CP_BOX_DOOR_OPEN = new Sensor();

        public Switch IsReadyToInputArm = new Switch();
        public Sensor IsEFEMInputArm = new Sensor(false);

        public OHTSignal OHT = new OHTSignal();
        //================================================================================================================
        public ADCBundle ADC = new ADCBundle(
           new AdcTemperature(2),
           new ADConverter_AJ65BTCU_68ADVN(0x2222, 0x2202, 7)
           );
        #region Aligner
        public Sensor AlignerWaferDetect = new Sensor(false);
        public PreAlignerVacuumProxy AlignerVac = new PreAlignerVacuumProxy();
        public PreAlignerOcrCylinder AlignerOcrCylinder = new PreAlignerOcrCylinder();

        public Switch IsReadyToAlignInputArm = new Switch();
        public Sensor IsAlignerInputArm = new Sensor();
        #endregion
        #endregion

        #region Pmac
        public PMacProxy PMac                                       /**/ = new PMacProxy();
        public StageXServo StageX                                   /**/ = new StageXServo(1, 1, "StageX", 5);
        public StageYServo StageY                                   /**/ = new StageYServo(2, 2, "StageY", 5);
        public ThetaServo Theta                                /**/ = new ThetaServo(5, 5, "Theta", 3);
        public ServoMotorPmac[] Motors { get; set; }
        #endregion
        #region Ezi Step Motor
        public StepMotorEzi[] StepMotors { get; set; }
        public AlignerXEzi AlignerX = new AlignerXEzi(2, "AlignerX", 3);
        public AlignerYEzi AlignerY = new AlignerYEzi(1, "AlignerY", 3);
        public AlignerThetaEzi AlignerT = new AlignerThetaEzi(0, "AlignTheta", 3);
        #endregion

        //Interface
        public InspPcProxy InspPc = new InspPcProxy();
        public EFEMUnit Efem = new EFEMUnit();
        public WaferTransferLogic WaferTransLogic = new WaferTransferLogic();
        public InitializeLogic InitialLogic = new InitializeLogic();
        public MainStep StMain = new MainStep() { DBPort = EmEfemDBPort.EQUIPMENT };
        public HomingStep StHoming = new HomingStep();
        public LoadingStep StLoading = new LoadingStep();
        public TTTMStep StTTTM = new TTTMStep();
        public ScanningStep StScanning = new ScanningStep();
        public ReviewStep StReviewing = new ReviewStep();
        public UnLoadingStep StUnloading = new UnLoadingStep();

        //OHT
        public PioOHTStep PioLPM1 = new PioOHTStep();
        public PioOHTStep PioLPM2 = new PioOHTStep();

        //Proxy
        public BlowerProxy Blower = new BlowerProxy();
        public VacuumProxy Vacuum = new VacuumProxy();
        public CenteringStep Centering = new CenteringStep();
        public PreAlignerBundle PreAligner = null;
        public PreAlignerSeqLogic PreAlignerSeq = new PreAlignerSeqLogic();

        //Setting
        public EquipInitSetting InitSetting = new EquipInitSetting();
        public PcCtrlSetting CtrlSetting = new PcCtrlSetting();
        public ECIDDataSetting EcidSetting = new ECIDDataSetting();

        public AlarmSolutionMgr AlarmSolutionMgr = new AlarmSolutionMgr();

        public ECIDDataSetting HostEcidSetting = new ECIDDataSetting();
        public WaferMapDataSetting WaferMapSetting = new WaferMapDataSetting();

        //Unit
        public EFUController EFUCtrler = null;
        public OCR_IS1741 OCR = null;
        public BCR_DM150 BCR1 = null;
        public BCR_DM150 BCR2 = null;


        public int LoadPort;
        public bool PioWorkingAlarm = false;
        public bool IsPauseUnldPioRunning = false;
        public bool IsPauseLdPioRunning = false;
        public bool UnldPioWorkingAlarm = false;

        public bool IsOhtLoadRunning = false;
        public bool IsOhtUnloadRunning = false;

        public bool IsTransferToLoad = false;
        public bool IsTransferFromLoad = false;
        public string LPM1Recipe { get; set; }
        public string LPM2Recipe { get; set; }

        public FrmTransferDataMgr FrmDataMgr;

        public bool UseFixedDitAlignerRecipe;
        public string FixedDitAlignerRecipeName;

        public Action<bool, string, string> AddTerminalMsg;
        public Action<int, string, string> PopupTimerMsg;
        public bool HasNewTerminalMsg;

        public bool CheckIsExistWaferInfo(string cstId)
        {
            try
            {
                if (IsWaferDetect != EmGlassDetect.NOT && this.TransferUnit.LowerWaferKey.CstID == cstId)
                    return true;
                else if (Efem.Aligner.Status.IsWaferExist && Efem.Aligner.LowerWaferKey.CstID == cstId)
                    return true;
                else if (Efem.Robot.Status.IsUpperArmVacOn && Efem.Robot.UpperWaferKey.CstID == cstId)
                    return true;
                else if (Efem.Robot.Status.IsLowerArmVacOn && Efem.Robot.LowerWaferKey.CstID == cstId)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog.AppendLine(ex.Message);
                AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0660_NO_WAFER_INFO);
                return false;
            }
        }

        //CIM
        public EmCimMode CimMode { get; private set; } = EmCimMode.OffLine;
        public bool SetCimMode(EmCimMode mode)
        {
            if (mode == EmCimMode.OffLine)
            {

                this.CimMode = mode;
                return true;
            }
            if (this.EquipRunMode == EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock("오토런 상태일 때에는 물류/비물류 모드 변경 불가, Manual로 전환 후 변경 해주세요");
                return false;
            }
            //if(GG.CimTestMode)
            //{
            //    this.CimMode = EmCimMode.OffLine;
            //    InterLockMgr.AddInterLock("CIM Test Mode일 때는 CIM 모드 변경 불가");
            //    return false;
            //}


            this.CimMode = mode;
            return true;
        }
        public bool SetOHTMode(EmLoadType type, bool IsReport = true)
        {
            if (Efem.LoadPort1.LoadType == type)
            {
                return false;
            }
            if (type == EmLoadType.OHT && (Efem.LoadPort1.OHTpio.IsRunning || Efem.LoadPort2.OHTpio.IsRunning))
            {
                InterLockMgr.AddInterLock("인터락<물류 모드 변경>", string.Format("물류 진행 중일 때 변경 불가능"));
                return false;
            }
            if (this.EquipRunMode == EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock("오토런 상태일 때에는 물류/비물류 모드 변경 불가, Manual로 전환 후 변경 해주세요");
                return false;
            }
            GG.Equip.Efem.LoadPort2.LoadType = GG.Equip.Efem.LoadPort1.LoadType = type;
            GG.Equip.CtrlSetting.LPMLoadType = (int)type;

            GG.Equip.CtrlSetting.Save();

            if (IsReport == true)
            {
                GG.Equip.LoadTypeChangeReport();
            }

            CheckMgr.AddCheckMsg(true, string.Format("카세트 로드 방식 변경\n[{0}]", type == EmLoadType.OHT ? "물류" : "비물류"));
            Logger.Log.AppendLine(LogLevel.Info, string.Format("카세트 로드 방식 변경\n[{0}]", type == EmLoadType.OHT ? "물류" : "비물류"));

            return true;
        }
        public delegate void NotifyLoadTypeChanged(EmEfemPort _port, EmLoadType _type);
        public event NotifyLoadTypeChanged LoadTypeChanged;
        public EmHsmsAck SetLpmLoadType(EmEfemPort port, EmLoadType _loadType)
        {
            if (this.EquipRunMode == EmEquipRunMode.Auto)
            {
                Logger.CIMLog.AppendLine("{1} {0} 모드 변경 실패 : Auto Mode일 때 변경 이벤트 수신", port.ToString(), _loadType.ToString());
                return EmHsmsAck.NACK;
            }

            if (this.Efem.LoadPort1.LoadType == _loadType)
            {
                Logger.CIMLog.AppendLine("이미 {0} 모드, 변경 명령 중복", _loadType.ToString());
                return EmHsmsAck.OVERLAP;
            }

            Efem.LoadPort1.LoadType = (EmLoadType)_loadType;
            Efem.LoadPort2.LoadType = (EmLoadType)_loadType;
            LoadTypeChanged((EmEfemPort)1, _loadType);
            LoadTypeChanged((EmEfemPort)2, _loadType);
            Logger.CIMLog.AppendLine("{1} {0} 모드 변경 성공", port.ToString(), _loadType.ToString());
            return EmHsmsAck.OK;

        }
        #region 미사용        
        public Sensor FireAlarm                             /**/                  = new Sensor();
        public Sensor EnableGripSwOn1                                /**/         = new Sensor();
        public Sensor EnableGripSwOn2                                /**/         = new Sensor();
        public Sensor EnableGripSwOn3                                /**/         = new Sensor();
        public Switch TemperatureError                      /**/ = new Switch();

        public HsmsPcProxy HsmsPc = new HsmsPcProxy();
        public UsingTimeChecker Lamp = new UsingTimeChecker(3);
        public MccPcProxy MccPc = new MccPcProxy();
        public PioHsSendStep PioA2ISend = new PioHsSendStep();
        public PioHsRecvStep PioI2ARecv = new PioHsRecvStep();
        public SafetyStep StSafety = new SafetyStep();
        public PcUsingTimeSetting UsingTimeSetting = new PcUsingTimeSetting();
        public PcAnalogSetting AnalogSetting = new PcAnalogSetting();
        #endregion

        public bool IsForcedComeback { get; set; }
        public bool IsCenteringStepping { get; set; }
        public bool IsHomePositioning { get; set; }
        public bool NeedHome
        {
            get
            {
                return Motors.ToList().TrueForAll(m => m.IsServoError == false) == false;
            }
        }
        public bool IsAllHomeComplete
        {
            get
            {
                return Motors.ToList().TrueForAll(m => m.IsHomeComplete() == true);
            }
        }
        public bool IsHomePosition
        {
            get
            {
                return Motors.ToList().TrueForAll(m => m.IsHomeOnPosition() == true);
            }
        }
        /// <summary>
        /// Monitor화면에서만 On/Off가능, 가상 IO사용
        /// </summary>
        public bool IsIoTestMode { get; set; }
        public bool IsGlassUnloading { get; set; }
        public int IsSkipWaferLPM { get; set; } //배출된 웨이퍼가 로드포트 1일 경우 : 1    2일 경우 : 2   아닐경우 -1
        public int IsSkipWaferSlotNo { get; set; }
        public bool IsUseInterLockOff { get; set; }
        private bool _isUseDoorInterlockOff;
        public bool IsWaferUnloadingState
        {
            get
            {
                return
                    GG.Equip.StageX.IsMoveOnPosition(StageXServo.LoadingPos) &&
                GG.Equip.StageY.IsMoveOnPosition(StageYServo.LoadingPos) &&
                GG.Equip.Theta.IsMoveOnPosition(ThetaServo.LoadingPos) &&
                    GG.Equip.IsReadyToInputArm.IsSolOnOff;
            }
        }
        public bool IsUseDoorInterLockOff
        {
            get
            {
                return _isUseDoorInterlockOff;
            }
            set
            {
                _isUseDoorInterlockOff = value;
            }
        }
        public bool IsUseInternalTestMode { get; set; }
        public bool IsUseLiftpinVacuumCenteringExMode { get; set; }
        public bool IsUseSelectInspOrder { get; set; }
        public bool IsUseExceptAutoTeachKey { get; set; }

        public int OverDefectReinspCurrentCount { get; set; }

        private bool _isNoGlassMode = false;
        public EmInspOrder NoGlassType { get; set; }
        public bool IsNoGlassMode
        {
            get
            {
                return _isNoGlassMode || GG.EfemNoWafer;
            }
            set
            {
                if (value == true)
                {
                    /*
                     * 원판
                     * 분판(좌+우)
                     * 좌측 분판
                     * 우측 분판
                     */
                    if (IsWaferDetect != EmGlassDetect.NOT)
                    {
                        InterLockMgr.AddInterLock("인터락<GLASS>\n(GLASS가 감지됨니다. NO GLASS 모드로 변경이 불가능합니다.)");
                        Logger.Log.AppendLine(LogLevel.Warning, "GLASS가 감지됨니다. NO GLASS 모든 변경 불가.");
                        return;
                    }
                }

                _isNoGlassMode = value;
            }
        }
        public bool IsInterlock
        {
            get
            {
                return IsPause;
            }
            set
            {
                IsPause = value;
            }
        }

        public Equipment()
        {
            Motors = new ServoMotorPmac[] {
                StageX,
                StageY,
                Theta,
            };
            StepMotors = new StepMotorEzi[]
            {
                AlignerX,
                AlignerY,
                AlignerT,
            };

            Blower.Equip = this;
            Vacuum.Equip = this;
            Centering.Equip = this;
            PinIonizer.Equip = this;
            WorkingLight.Equip = this;
            ModeSelectKey.Equip = this;
            Vacuum.Equip = this;
            Blower.Equip = this;
            Efem.SetEquipment(this);

            //IsInterlock = true;

            _equipMode = EmEquipRunMode.Manual;

            LongRunCount = 0;
        }

        public bool Initalize()
        {
            StringBuilder connErr = new StringBuilder();

            // 셋팅 파일 확인            
            CtrlSetting.Load(PcCtrlSetting.PATH_SETTING);
            InitSetting.Load(EquipInitSetting.PATH_SETTING);
            //EcidSetting.Load(CtrlSetting.Hsms.ECIDSavePath);
            Lamp.InitilizeUsingTime(UsingTimeSetting);
            InitalizeAlarmList();
            InitalizeMccList();
            ADC.Initialize();
            Lamp.InitilizeUsingTime(UsingTimeSetting);

            int errCnt = 0;
            bool PosiResult = ServoPositionMgr.LoadPosition(this, ref errCnt);

            AlignerX.SoftPlusLimit = GG.Equip.InitSetting.EquipNo == 1 ? 29 : 15;
            AlignerT.SoftMinusLimit = GG.Equip.InitSetting.EquipNo == 1 ? -3 : -1;

            PMac.StartSaveToPmac(this);

            UsingTimeSetting.Load(PcUsingTimeSetting.PATH_SETTING);

            EFUCtrler = new EFUController(InitSetting.NumFFU, InitSetting.EFUPort, 10000);
            EFUCtrler.ecidEvent += () =>
            {
                this.HsmsPc.StartCommand(this, EmHsmsPcCommand.ECID_CHANGE, EmHsmsAck.OK);
            };
            OCR = new OCR_IS1741();
            BCR1 = new BCR_DM150(InitSetting.BCR1Port);
            BCR2 = new BCR_DM150(InitSetting.BCR2Port);
            OCR.SetReadValueRegex(@"^[0-9,A-Z,a-z]{7}-[0-9,A-Z,a-z]{4}$");
            BCR1.SetReadValueRegex(@"^[0-9,A-Z,a-z]{7}-[0-9,A-Z,a-z]{4}$", @"^[0-9,A-Z,a-z]{7}-[0-9,A-Z,a-z]{2}$");
            BCR2.SetReadValueRegex(@"^[0-9,A-Z,a-z]{7}-[0-9,A-Z,a-z]{4}$", @"^[0-9,A-Z,a-z]{7}-[0-9,A-Z,a-z]{2}$");
            Efem.LoadPort1.RFR = new RFIDController(InitSetting.RFR1Port);
            Efem.LoadPort2.RFR = new RFIDController(InitSetting.RFR2Port);
            //jjy
            AlarmSolutionMgr.Load();

            if (GG.IsDitPreAligner)
            {
                PreAligner = new PreAlignerBundle();
                //if (GG.TestMode == false)

                //jjy
                PreAligner.InitCamera();
                PreAligner.LightController.Initialize(GG.Equip.InitSetting.LightControllerPort);
                PreAligner.SetDebugImagePath(System.IO.Path.Combine(GG.StartupPath, GG.Equip.InitSetting.ControlLogBasePath, "PreAlign"));
                //if (GG.TestMode == false)
                {
                    if (PreAligner != null)
                    {
                        if (GG.Equip.PreAligner.OpenLightController(GG.Equip.InitSetting.LightControllerPort) == false)
                            connErr.AppendLine("Pre Aligner Light Controller Open 실패");
                        if (GG.Equip.PreAligner.OpenCamera() == false)
                            connErr.AppendLine("Pre Aligner Camera Connect 실패");
                    }
                }

                if (GG.EZI.Open() == 0)
                {
                    connErr.AppendLine("Pre Aligner Ezi Step Motor 연결 실패");
                }

                FixedDitAlignerRecipeName = "DEF";
            }

            if (GG.TestMode == false)
            {
                if (EFUCtrler.Start() == false)
                    connErr.AppendLine("EFU 연결 실패");
                //jjy
                //if (OCR.Open(InitSetting.OcrIP, InitSetting.OcrPort) == false)
                //    connErr.AppendLine("OCR 연결 실패");
                if (Efem.LoadPort1.RFR.Open() == false)
                    connErr.AppendLine("RF1 연결 실패");
                if (Efem.LoadPort2.RFR.Open() == false)
                    connErr.AppendLine("RF2 연결 실패");
                if (BCR1.Open() == false)
                    connErr.AppendLine("BCR1 연결 실패");
                if (BCR2.Open() == false)
                    connErr.AppendLine("BCR2 연결 실패");
            }

            PreAlignerSeq.InitUserInterface(this);
            Efem.LoadPort1.InitUserInterface(this);
            Efem.LoadPort2.InitUserInterface(this);
            Efem.Aligner.InitUserInterface(this);
            Efem.Robot.InitUserInterface(this);
            if (Efem.Proxy.Connect(this) == false)
                connErr.AppendLine("EFEM 연결 실패");
            Efem.Proxy.Initailize(this);
            PioLPM1.Initalize(this, EmEfemPort.LOADPORT1);
            PioLPM2.Initalize(this, EmEfemPort.LOADPORT2);
            StMain.InitUserInferface();
            EFEMTcp.appendDataEvent += EFEMTcp_appendDataEvent;
            TransferDataMgr.Initialize(this);
            FrmDataMgr = new FrmTransferDataMgr();
            RecipeDataMgr.Initialize(this);
            PreAlignerRecipeDataMgr.Initialize(this);
            LPM1Recipe = RecipeDataMgr.GetCurRecipeName(0);
            LPM2Recipe = RecipeDataMgr.GetCurRecipeName(1);

            this.TransferUnit.LowerWaferKey = TransferDataMgr.GetWaferOrCreateBak(EmEfemDBPort.EQUIPMENT);
            Efem.LoadPort1.CstKey = TransferDataMgr.GetCstOrCreateBak(EmEfemDBPort.LOADPORT1);
            Efem.LoadPort2.CstKey = TransferDataMgr.GetCstOrCreateBak(EmEfemDBPort.LOADPORT2);
            Efem.Robot.LowerWaferKey = TransferDataMgr.GetWaferOrCreateBak(EmEfemDBPort.LOWERROBOT);
            Efem.Robot.UpperWaferKey = TransferDataMgr.GetWaferOrCreateBak(EmEfemDBPort.UPPERROBOT);
            Efem.Aligner.LowerWaferKey = TransferDataMgr.GetWaferOrCreateBak(EmEfemDBPort.ALIGNER);

            Efem.LoadPort1.UpdateNextWaferKey();
            Efem.LoadPort2.UpdateNextWaferKey();

            Efem.LoadPort1.wafers = TransferDataMgr.FindAll(Efem.LoadPort1.CstKey);
            Efem.LoadPort2.wafers = TransferDataMgr.FindAll(Efem.LoadPort2.CstKey);

            // KYH 230913-01 : ReviewFailCount, AutoMoveOutCount 포트별로 저장값 복원
            Efem.LoadPort1.ReviewFailCount = CtrlSetting.ReviewFailCount1;
            Efem.LoadPort1.AutoMoveOutCount = CtrlSetting.AutoMoveOutCount1;
            Efem.LoadPort2.ReviewFailCount = CtrlSetting.ReviewFailCount2;
            Efem.LoadPort2.AutoMoveOutCount = CtrlSetting.AutoMoveOutCount2;

            try
            {
                if (Efem.Robot.LowerWaferKey.CstID != null && Efem.Robot.LowerWaferKey.CstID != "")
                    Efem.Robot.SetTargetLoadPort(TransferDataMgr.GetCst(Efem.Robot.LowerWaferKey.CstID).LoadPortNo);
                else if (Efem.Aligner.LowerWaferKey.CstID != null && Efem.Aligner.LowerWaferKey.CstID != "")
                    Efem.Robot.SetTargetLoadPort(TransferDataMgr.GetCst(Efem.Aligner.LowerWaferKey.CstID).LoadPortNo);
                else if (this.TransferUnit.LowerWaferKey != null && this.TransferUnit.LowerWaferKey.CstID != null && this.TransferUnit.LowerWaferKey.CstID != "")
                    Efem.Robot.SetTargetLoadPort(TransferDataMgr.GetCst(this.TransferUnit.LowerWaferKey.CstID).LoadPortNo);
                else if (Efem.Robot.UpperWaferKey.CstID != null && Efem.Robot.UpperWaferKey.CstID != "")
                    Efem.Robot.SetTargetLoadPort(TransferDataMgr.GetCst(Efem.Robot.UpperWaferKey.CstID).LoadPortNo);
            }
            catch (Exception ex)
            {
                AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0947_EXCEPTION_OCCURE);
                Logger.ExceptionLog.AppendLine("Set Target LoadPort Error: {0}", ex.Message);
                Logger.ExceptionLog.AppendLine(EquipStatusDump.CallStackLog());
            }


            if (Efem.Robot.TargetLoadPort != 0)
                CheckMgr.AddCheckMsg(true, string.Format("LoadPort{0} 진행중이었습니다. 해당 LoadPort 먼저 {1}. 내부에 Wafer가 없는 경우 시작 시 다시 설정합니다.",
                    Efem.Robot.TargetLoadPort, Efem.LoadPort((EmEfemPort)Efem.Robot.TargetLoadPort).LoadType == EmLoadType.OHT ? "시작됩니다" : "투입하세요"));

            if (GG.TestMode == true)
            {
                if (this.TransferUnit.LowerWaferKey.CstID != "")
                {
                    WaferDetectSensorLiftpin1.XB_OnOff.vBit = true;
                    WaferDetectSensorLiftpin2.XB_OnOff.vBit = true;
                    WaferDetectSensorStage1.XB_OnOff.vBit = true;
                }
                if (Efem.LoadPort1.CstKey.ID != "") Efem.LoadPort1.Status.IsFoupExist = true;
                if (Efem.LoadPort2.CstKey.ID != "") Efem.LoadPort2.Status.IsFoupExist = true;
                if (Efem.Robot.LowerWaferKey.CstID != "") Efem.Robot.Status.IsLowerArmVacOn = true;
                if (Efem.Robot.UpperWaferKey.CstID != "") Efem.Robot.Status.IsUpperArmVacOn = true;
                if (Efem.Aligner.LowerWaferKey.CstID != "") Efem.Aligner.Status.IsWaferExist = true;
            }

            if (GG.PrivilegeTestMode == true)
                LoginMgr.Instance.Login(this, "ditctrl", "ditctrl");
            else
            {

            }

            IsBuzzerAllOff = false;
            IsBottomWorkingLightAutoMode = true;
            IsTopWorkingLightAutoMode = true;

            IsReviewSkip = EmReviewSkip.None;
            InspOrder = EmInspOrder.FRONT_REAR;
            IsUseLiftpinVacuumCenteringExMode = false;
            IsUseSelectInspOrder = false;
            IsUseExceptAutoTeachKey = false;
            IsUseDoorInterLockOff = false;
            IsUseInterLockOff = true;

            InterlockSetting();

            if (connErr.Length > 0)
                InterLockMgr.AddInterLock(connErr.ToString());

            return true;
        }

        private void LoadLastCimMode()
        {
            EmCimMode _mode = (EmCimMode)GG.MEM_DIT.VirGetShort(CIMAW.XW_CTRL_MODE_CHANGE_STATE).GetByte(0);

            if (_mode == EmCimMode.None && GG.CimTestMode == false)
            {
                InterLockMgr.AddInterLock("CIM 프로그램 연결 상태를 확인 하세요");
                SetOHTMode(EmLoadType.Manual, false);
                return;
            }
            if (GG.CimTestMode)
                _mode = EmCimMode.OffLine;
            SetCimMode(_mode);
        }

        private void EFEMTcp_appendDataEvent(EmEfemPort port, string msg)
        {
            Logger.EFEMCommLog.AppendLine(LogLevel.NoLog, msg);
        }
        public void EFEMTcpReconnect()
        {
            System.Threading.Thread reconnect = new System.Threading.Thread(new System.Threading.ThreadStart(Efem.Proxy.Connect));
            reconnect.Start();
        }

        public void SetAddress()
        {
            #region OHT
            OHT.LP_1_IN1.XB_OnOff = AddressMgr.GetAddress("LP_1_IN1_VALID", 0);
            OHT.LP_1_IN2.XB_OnOff = AddressMgr.GetAddress("LP_1_IN2_CS_0", 0);
            OHT.LP_1_IN3.XB_OnOff = AddressMgr.GetAddress("LP_1_IN3_CS_1", 0);
            OHT.LP_1_IN4.XB_OnOff = AddressMgr.GetAddress("LP_1_IN4_AM_AVBL", 0);
            OHT.LP_1_IN5.XB_OnOff = AddressMgr.GetAddress("LP_1_IN5_TR_REQ", 0);
            OHT.LP_1_IN6.XB_OnOff = AddressMgr.GetAddress("LP_1_IN6_BUSY", 0);
            OHT.LP_1_IN7.XB_OnOff = AddressMgr.GetAddress("LP_1_IN7_COMPT", 0);
            OHT.LP_1_IN8.XB_OnOff = AddressMgr.GetAddress("LP_1_IN8_CONT", 0);
            OHT.LP_2_IN1.XB_OnOff = AddressMgr.GetAddress("LP_2_IN1_VALID", 0);
            OHT.LP_2_IN2.XB_OnOff = AddressMgr.GetAddress("LP_2_IN2_CS_0", 0);
            OHT.LP_2_IN3.XB_OnOff = AddressMgr.GetAddress("LP_2_IN3_CS_1", 0);
            OHT.LP_2_IN4.XB_OnOff = AddressMgr.GetAddress("LP_2_IN4_AM_AVBL", 0);
            OHT.LP_2_IN5.XB_OnOff = AddressMgr.GetAddress("LP_2_IN5_TR_REQ", 0);
            OHT.LP_2_IN6.XB_OnOff = AddressMgr.GetAddress("LP_2_IN6_BUSY", 0);
            OHT.LP_2_IN7.XB_OnOff = AddressMgr.GetAddress("LP_2_IN7_COMPT", 0);
            OHT.LP_2_IN8.XB_OnOff = AddressMgr.GetAddress("LP_2_IN8_CONT", 0);

            OHT.LP_1_OUT1.YB_OnOff = AddressMgr.GetAddress("LP_1_OUT1_L_REQ", 0);
            OHT.LP_1_OUT2.YB_OnOff = AddressMgr.GetAddress("LP_1_OUT2_U_REQ", 0);
            OHT.LP_1_OUT3.YB_OnOff = AddressMgr.GetAddress("LP_1_OUT3_VA", 0);
            OHT.LP_1_OUT4.YB_OnOff = AddressMgr.GetAddress("LP_1_OUT4_READY", 0);
            OHT.LP_1_OUT5.YB_OnOff = AddressMgr.GetAddress("LP_1_OUT5_VS_0", 0);
            OHT.LP_1_OUT6.YB_OnOff = AddressMgr.GetAddress("LP_1_OUT6_VS_1", 0);
            OHT.LP_1_OUT7.YB_OnOff = AddressMgr.GetAddress("LP_1_OUT7_HO_AVBL", 0);
            OHT.LP_1_OUT8.YB_OnOff = AddressMgr.GetAddress("LP_1_OUT8_ES", 0);
            OHT.LP_2_OUT1.YB_OnOff = AddressMgr.GetAddress("LP_2_OUT1_L_REQ", 0);
            OHT.LP_2_OUT2.YB_OnOff = AddressMgr.GetAddress("LP_2_OUT2_U_REQ", 0);
            OHT.LP_2_OUT3.YB_OnOff = AddressMgr.GetAddress("LP_2_OUT3_VA", 0);
            OHT.LP_2_OUT4.YB_OnOff = AddressMgr.GetAddress("LP_2_OUT4_READY", 0);
            OHT.LP_2_OUT5.YB_OnOff = AddressMgr.GetAddress("LP_2_OUT5_VS_0", 0);
            OHT.LP_2_OUT6.YB_OnOff = AddressMgr.GetAddress("LP_2_OUT6_VS_1", 0);
            OHT.LP_2_OUT7.YB_OnOff = AddressMgr.GetAddress("LP_2_OUT7_HO_AVBL", 0);
            OHT.LP_2_OUT8.YB_OnOff = AddressMgr.GetAddress("LP_2_OUT8_ES", 0);
            #endregion
            #region CC-LINK
            EmsOutside1.XB_OnOff                                /**/ = AddressMgr.GetAddress("EMERGENCY_STOP_1", 0);
            EmsOutside2.XB_OnOff                                /**/ = AddressMgr.GetAddress("EMERGENCY_OFF_2", 0);
            EmsOutside3.XB_OnOff                                /**/ = AddressMgr.GetAddress("EMERGENCY_STOP_3", 0);

            ModeSelectKey.AutoSensor.XB_OnOff /**/                  = AddressMgr.GetAddress("MODE_SELECT_KEY_SW_AUTOTEACH1", 0);
            ModeSelectKey.Sol.YB_OnOff /**/                         = AddressMgr.GetAddress("AUTO_TEACH_SOL1", 0);

            SafeyPlcError.XB_OnOff = AddressMgr.GetAddress("SAFETY_PLC_ERROR", 0);
            SERVO_MC_POWER_ON_1.XB_OnOff                        /**/ = AddressMgr.GetAddress("MC_1", 0);

            TopDoor01.XB_OnOff = AddressMgr.GetAddress("TOP_DOOR1_SENSOR", 0);
            TopDoor02.XB_OnOff = AddressMgr.GetAddress("TOP_DOOR2_SENSOR", 0);
            TopDoor03.XB_OnOff = AddressMgr.GetAddress("TOP_DOOR3_SENSOR", 0);
            TopDoor04.XB_OnOff = AddressMgr.GetAddress("TOP_DOOR4_SENSOR", 0);
            TopDoor01.YB_OnOff = AddressMgr.GetAddress("TOP_DOOR1_OPEN_SOL", 0);
            TopDoor02.YB_OnOff = AddressMgr.GetAddress("TOP_DOOR2_OPEN_SOL", 0);
            TopDoor03.YB_OnOff = AddressMgr.GetAddress("TOP_DOOR3_OPEN_SOL", 0);
            TopDoor04.YB_OnOff = AddressMgr.GetAddress("TOP_DOOR4_OPEN_SOL", 0);

            EfemDoor01.XB_OnOff = AddressMgr.GetAddress("EFEM_DOOR1_SENSOR", 0);
            EfemDoor02.XB_OnOff = AddressMgr.GetAddress("EFEM_DOOR2_SENSOR", 0);
            EfemDoor01.YB_OnOff = AddressMgr.GetAddress("EFEM_DOOR1_OPEN_SOL", 0);
            EfemDoor02.YB_OnOff = AddressMgr.GetAddress("EFEM_DOOR2_OPEN_SOL", 0);

            WaferDetectSensorLiftpin1.XB_OnOff                         /**/ = AddressMgr.GetAddress("WAFER_PIN_DETECT_SENSOR_1", 0);
            WaferDetectSensorLiftpin2.XB_OnOff                         /**/ = AddressMgr.GetAddress("WAFER_PIN_DETECT_SENSOR_2", 0);
            WaferDetectSensorStage1.XB_OnOff                         /**/ = AddressMgr.GetAddress("WAFER_STAGE_DETECT_SENSOR_1", 0);

            StandCentering.XB_ForwardComplete[0]                /**/ = AddressMgr.GetAddress("STANDARD_CENTERING_1_FORWARD_SENSOR", 0);
            StandCentering.XB_BackwardComplete[0]               /**/ = AddressMgr.GetAddress("STANDARD_CENTERING_1_BACKWARD_SENSOR", 0);
            StandCentering.XB_ForwardComplete[1]                /**/ = AddressMgr.GetAddress("STANDARD_CENTERING_2_FORWARD_SENSOR", 0);
            StandCentering.XB_BackwardComplete[1]               /**/ = AddressMgr.GetAddress("STANDARD_CENTERING_2_BACKWARD_SENSOR", 0);
            StandCentering.YB_ForwardCmd                        /**/ = AddressMgr.GetAddress("STANDARD_CENTERING_1_FORWARD_SOL", 0);
            StandCentering.YB_BackwardCmd                       /**/ = AddressMgr.GetAddress("STANDARD_CENTERING_1_BACKWARD_SOL", 0);

            LiftPin.XB_ForwardComplete[0]                              /**/ = AddressMgr.GetAddress("LIFT_PIN_1_UP_SENSOR", 0);
            LiftPin.XB_ForwardComplete[1]                              /**/ = AddressMgr.GetAddress("LIFT_PIN_2_UP_SENSOR", 0);
            LiftPin.XB_BackwardComplete[0]                            /**/ = AddressMgr.GetAddress("LIFT_PIN_1_DOWN_SENSOR", 0);
            LiftPin.XB_BackwardComplete[1]                            /**/ = AddressMgr.GetAddress("LIFT_PIN_2_DOWN_SENSOR", 0);
            LiftPin.YB_ForwardCmd                                    /**/ = AddressMgr.GetAddress("LIFT_PIN_1_UP_SOL", 0);
            LiftPin.YB_BackwardCmd                                  /**/ = AddressMgr.GetAddress("LIFT_PIN_1_DOWN_SOL", 0);

            SafetyCircuitReset.YB_OnOff = AddressMgr.GetAddress("SAFETY_CIRCUIT_RESET", 0);

            PinIonizer.XB_Alarm[0]                              /**/ = AddressMgr.GetAddress("IONIZER_ABNORMAL_ALARM", 0);
            PinIonizer.YB_RemoteOn[0]                           /**/ = AddressMgr.GetAddress("IONIZER_1_REMOTE_ON", 0);
            PinIonizer.YB_AirOn[0]                              /**/ = AddressMgr.GetAddress("IONIZER_1_AIR_ON_SOL", 0);

            MainVacuum1.XB_OnOff                                /**/ = AddressMgr.GetAddress("MAIN_VACCUM_1", 0);
            MainVacuum2.XB_OnOff                                /**/ = AddressMgr.GetAddress("MAIN_VACCUM_2", 0);

            Vacuum.Stage1.XB_OnOff                               /**/ = AddressMgr.GetAddress("CHECK_VACCUM_1", 0);
            Vacuum.Stage2.XB_OnOff                               /**/ = AddressMgr.GetAddress("CHECK_VACCUM_2", 0);

            Vacuum.Stage1.YB_OnOff                               /**/ = AddressMgr.GetAddress("VACUUM_STAGE_SOL_1", 0);
            Vacuum.Stage2.YB_OnOff                               /**/ = AddressMgr.GetAddress("VACUUM_STAGE_SOL_2", 0);

            Blower.Stage1.YB_OnOff                                 /**/ = AddressMgr.GetAddress("BLOWER_STAGE_SOL_1", 0);
            Blower.Stage2.YB_OnOff                                 /**/ = AddressMgr.GetAddress("BLOWER_STAGE_SOL_2", 0);

            Isolator1.XB_OnOff                                  /**/ = AddressMgr.GetAddress("ISOLATOR_1", 0);
            Isolator2.XB_OnOff                                  /**/ = AddressMgr.GetAddress("ISOLATOR_2", 0);
            Isolator3.XB_OnOff                                  /**/ = AddressMgr.GetAddress("ISOLATOR_3", 0);

            BuzzerK1.YB_OnOff = AddressMgr.GetAddress("BUZZER_K1", 0);
            BuzzerK2.YB_OnOff = AddressMgr.GetAddress("BUZZER_K2", 0);
            BuzzerK3.YB_OnOff = AddressMgr.GetAddress("BUZZER_K3", 0);
            BuzzerK4.YB_OnOff = AddressMgr.GetAddress("BUZZER_K4", 0);

            TowerLamp.Red.YB_OnOff = AddressMgr.GetAddress("TOWER_LAMP_RED", 0);
            TowerLamp.Yellow.YB_OnOff = AddressMgr.GetAddress("TOWER_LAMP_YELLOW", 0);
            TowerLamp.Green.YB_OnOff = AddressMgr.GetAddress("TOWER_LAMP_GREEN", 0);
            TowerLamp.Blue.YB_OnOff = AddressMgr.GetAddress("TOWER_LAMP_BLUE", 0);

            WorkingLight.Stage.YB_OnOff = AddressMgr.GetAddress("INS_TOP_WORKING_LIGHT_POWER_ON_OFF", 0);
            WorkingLight.PcRack.YB_OnOff = AddressMgr.GetAddress("PC_RACK_TOP_WORKING_LIGHT_POWER_ON_OFF", 0);

            MainAir1.XB_OnOff                                   /**/ = AddressMgr.GetAddress("MAIN_AIR_1", 0);
            MainAir2.XB_OnOff                                   /**/ = AddressMgr.GetAddress("MAIN_AIR_2", 0);

            RobotArmDefect.XB_OnOff = AddressMgr.GetAddress("ROBOT_ARM_DETECT", 0);

            PC_RACK_FAN_OFF_1.XB_OnOff = AddressMgr.GetAddress("PC_RACK_FAN_ON_OFF_1", 0);
            PC_RACK_FAN_OFF_2.XB_OnOff = AddressMgr.GetAddress("PC_RACK_FAN_ON_OFF_2", 0);
            PC_RACK_FAN_OFF_3.XB_OnOff = AddressMgr.GetAddress("PC_RACK_FAN_ON_OFF_3", 0);
            PC_RACK_FAN_OFF_4.XB_OnOff = AddressMgr.GetAddress("PC_RACK_FAN_ON_OFF_4", 0);
            PC_RACK_FAN_OFF_5.XB_OnOff = AddressMgr.GetAddress("PC_RACK_FAN_ON_OFF_5", 0);
            PC_RACK_FAN_OFF_6.XB_OnOff = AddressMgr.GetAddress("PC_RACK_FAN_ON_OFF_6", 0);
            PC_RACK_FAN_OFF_7.XB_OnOff = AddressMgr.GetAddress("PC_RACK_FAN_ON_OFF_7", 0);
            PC_RACK_FAN_OFF_8.XB_OnOff = AddressMgr.GetAddress("PC_RACK_FAN_ON_OFF_8", 0);

            CP_BOX_FAN_OFF_1.XB_OnOff = AddressMgr.GetAddress("CP_BOX_FAN_ON_OFF_1", 0);
            CP_BOX_FAN_OFF_2.XB_OnOff = AddressMgr.GetAddress("CP_BOX_FAN_ON_OFF_2", 0);
            CP_BOX_FAN_OFF_3.XB_OnOff = AddressMgr.GetAddress("CP_BOX_FAN_ON_OFF_3", 0);
            CP_BOX_FAN_OFF_4.XB_OnOff = AddressMgr.GetAddress("CP_BOX_FAN_ON_OFF_4", 0);
            CP_BOX_DOOR_OPEN.XB_OnOff = AddressMgr.GetAddress("CP_BOX_DOOR_OPEN", 0);

            #region PreAligner
            AlignerWaferDetect.XB_OnOff = AddressMgr.GetAddress("PRE_ALIGN_WAFER_DETECT", 0);

            AlignerVac.Vacuum.XB_OnOff = AddressMgr.GetAddress("PRE_ALIGN_VAC_PRESURE_STATUS", 0);
            AlignerVac.Vacuum.YB_OnOff = AddressMgr.GetAddress("PRE_ALIGN_VACUUM_SOL_ON", 0);
            AlignerVac.Blower.YB_OnOff = AddressMgr.GetAddress("PRE_ALIGN_PURGE_SOL_ON", 0);

            AlignerOcrCylinder.XB_UpComplete = AddressMgr.GetAddress("PRE_ALIGN_OCR_UP", 0);
            AlignerOcrCylinder.XB_DownComplete = AddressMgr.GetAddress("PRE_ALIGN_OCR_DOWN", 0);
            AlignerOcrCylinder.YB_UpCmd = AddressMgr.GetAddress("PRE_ALIGN_CYLINDER_UP_DOWN", 0);

            IsAlignerInputArm.XB_OnOff = AddressMgr.GetAddress("PRE_ALIGN_ROBOT_READY_IN", 0);
            IsReadyToAlignInputArm.YB_OnOff = AddressMgr.GetAddress("PRE_ALIGN_READY_OUT", 0);
            #endregion

            //EFEM
            Efem.LPMLightCurtain.Detect.XB_OnOff = AddressMgr.GetAddress("LIGTH_CURTAIN_DETECT_1", 0);
            Efem.LPMLightCurtain.Muting1.YB_OnOff = AddressMgr.GetAddress("LIGHT_CURTAIN_MUTING_1", 0);
            Efem.LPMLightCurtain.Muting2.YB_OnOff = AddressMgr.GetAddress("LIGHT_CURTAIN_MUTING_2", 0);
            Efem.LPMLightCurtain.ResetOut.YB_OnOff = AddressMgr.GetAddress("LIGHT_CURTAIN_RESET", 0);
            Efem.LPMLightCurtain.SetAddress(AddressMgr.GetAddress("LIGHT_CURTAIN_MUTING_ON_CHECK", 0));

            IsEFEMInputArm.XB_OnOff = GG.EfemNoUse ? AddressMgr.GetAddress("ROBOT_ARM_DETECT", 0) : AddressMgr.GetAddress("EFEM_INPUT_ARM_TO_AVI", 0);
            IsReadyToInputArm.YB_OnOff = AddressMgr.GetAddress("AVI_READY_TO_RECV", 0);

            #region AD
            ADC.Adc1.SetAddress(6, 0x00, GG.CCLINK);
            ADC.Temperature.SetAddress(9, 0x0C, GG.CCLINK);
            //ADC.Adc1.Wr_AnalogReadDataBuf[0]                    /**/ = AddressMgr.GetAddress("AD1_CH1_ANALOG_READ_DATA", 0);
            //ADC.Adc1.Wr_AnalogReadDataBuf[1]                    /**/ = AddressMgr.GetAddress("AD1_CH2_ANALOG_READ_DATA", 0);
            //ADC.Adc1.Wr_AnalogReadDataBuf[2]                    /**/ = AddressMgr.GetAddress("AD1_CH3_ANALOG_READ_DATA", 0);
            //ADC.Adc1.Wr_AnalogReadDataBuf[3]                    /**/ = AddressMgr.GetAddress("AD1_CH4_ANALOG_READ_DATA", 0);
            //ADC.Adc1.Wr_AnalogReadDataBuf[3]                    /**/ = AddressMgr.GetAddress("AD1_CH5_ANALOG_READ_DATA", 0);
            //ADC.Adc1.Wr_AnalogReadDataBuf[3]                    /**/ = AddressMgr.GetAddress("AD1_CH6_ANALOG_READ_DATA", 0);

            //ADC.Temperature.Wr_AnalogReadDataBuf[0]             /**/ = AddressMgr.GetAddress("TD_CH1_ANALOG_READ_DATA", 0);
            //ADC.Temperature.Wr_AnalogReadDataBuf[1]             /**/ = AddressMgr.GetAddress("TD_CH2_ANALOG_READ_DATA", 0);

            //ADC.Adc1.Ww_ChangePerMitAndProhibitBit              /**/ = AddressMgr.GetAddress("AD1_CHANGE_PERMIT_PROHIBIT_BIT", 0);
            //ADC.Adc1.Ww_RangeSettingBit[0]                      /**/ = AddressMgr.GetAddress("AD1_CH1_4_RANGE_SETTING", 0);
            //ADC.Adc1.Ww_RangeSettingBit[1]                      /**/ = AddressMgr.GetAddress("AD1_CH5_8_RANGE_SETTING", 0);
            //ADC.Adc1.Ww_AvgProcessBit                           /**/ = AddressMgr.GetAddress("AD1_AVG_PROCESS", 0);

            //ADC.Adc1.Ww_AvgCountSetBit[0]                       /**/ = AddressMgr.GetAddress("AD1_CH1_AVG_COUNT_SET", 0);
            //ADC.Adc1.Ww_AvgCountSetBit[1]                       /**/ = AddressMgr.GetAddress("AD1_CH2_AVG_COUNT_SET", 0);
            //ADC.Adc1.Ww_AvgCountSetBit[2]                       /**/ = AddressMgr.GetAddress("AD1_CH3_AVG_COUNT_SET", 0);
            //ADC.Adc1.Ww_AvgCountSetBit[3]                       /**/ = AddressMgr.GetAddress("AD1_CH4_AVG_COUNT_SET", 0);
            //ADC.Adc1.Ww_AvgCountSetBit[4]                       /**/ = AddressMgr.GetAddress("AD1_CH5_AVG_COUNT_SET", 0);
            //ADC.Adc1.Ww_AvgCountSetBit[5]                       /**/ = AddressMgr.GetAddress("AD1_CH6_AVG_COUNT_SET", 0);
            #endregion
            #region TD IO Set
            //ADC.Temperature.XB_A2DCompleteFlag[0]                   /**/     = AddressMgr.GetAddress("TD1_CH1_TD_CONVERSION_COMPLETE_FLAG", 0);
            //ADC.Temperature.XB_A2DCompleteFlag[1]                   /**     = AddressMgr.GetAddress("TD1_CH2_TD_CONVERSION_COMPLETE_FLAG", 0);
            //ADC.Temperature.XB_InitDataProcessReqFlag                   /**/ = AddressMgr.GetAddress("TD1_INITIAL_DATA_PROCESSING_REQUEST_FLAG", 0);
            //ADC.Temperature.XB_InitDataSetCompleteFlag                  /**/ = AddressMgr.GetAddress("TD1_INITIAL_DATA_SETTING_COMPLETE_FLAG", 0);
            //ADC.Temperature.XB_ErrorStatusFlag                          /**/ = AddressMgr.GetAddress("TD1_ERROR_STATUS_FLAG", 0);
            //ADC.Temperature.YB_EnableFlag[0]                        /**/     = AddressMgr.GetAddress("TD1_CH1_TD_CONVERSION_ENABLE_FLAG", 0);
            //ADC.Temperature.YB_EnableFlag[1]                        /**/     = AddressMgr.GetAddress("TD1_CH2_TD_CONVERSION_ENABLE_FLAG", 0);
            //ADC.Temperature.YB_Sampling[0]                              /**/ = AddressMgr.GetAddress("TD1_CH1_TD_SAMPLING_FLAG", 0);
            //ADC.Temperature.YB_Sampling[1]                              /**/ = AddressMgr.GetAddress("TD1_CH2_TD_SAMPLING_FLAG", 0);
            //ADC.Temperature.YB_Offset                                   /**/ = AddressMgr.GetAddress("TD1_OFFSET_FLAG", 0);
            //ADC.Temperature.YB_InitDataProcessCompleteFlag              /**/ = AddressMgr.GetAddress("TD1_INITIAL_DATA_PROCESSING_COMPLETE_FLAG", 0);
            //ADC.Temperature.YB_InitDataSetReqFlag                       /**/ = AddressMgr.GetAddress("TD1_INITIAL_DATA_SETTING_REQUEST_FLAG", 0);
            //ADC.Temperature.YB_ErrorReset                               /**/ = AddressMgr.GetAddress("TD1_ERROR_RESET_FLAG", 0);
            #endregion
            #endregion                        
            #region PMAC
            PMac.YB_IsAutoMode                     /**/              = AddressMgr.GetAddress("PMAC_YB_EquipState", 0);
            PMac.YB_CheckAlarmStatus              /**/               = AddressMgr.GetAddress("PMAC_YB_EquipState", 1);
            PMac.YB_UpperInterfaceWorking         /**/               = AddressMgr.GetAddress("PMAC_YB_EquipState", 2);
            PMac.YB_LowerInterfaceWorking         /**/               = AddressMgr.GetAddress("PMAC_YB_EquipState", 3);

            PMac.XB_PmacReady                     /**/               = AddressMgr.GetAddress("PMAC_XB_PmacState", 0);
            PMac.XB_PmacAlive                     /**/               = AddressMgr.GetAddress("PMAC_XB_PmacState", 1);
            PMac.XB_EcatCommAlive /**/                               = AddressMgr.GetAddress("PMAC_XB_PmacState", 3);
            PMac.XB_ReviewRunning               /**/                 = AddressMgr.GetAddress("PMAC_XB_PmacState", 2);
            PMac.XB_InspRunning                /**/                  = AddressMgr.GetAddress("PMAC_XB_PmacState", 4);

            PMac.YB_EquipStatusMotorInterlockOff    /**/                = AddressMgr.GetAddress("PMAC_YB_CommonCmd", 0);
            PMac.YB_PmacResetCmd                  /**/                  = AddressMgr.GetAddress("PMAC_YB_CommonCmd", 1);
            PMac.YB_EmsCmd              /**/                            = AddressMgr.GetAddress("PMAC_YB_CommonCmd", 2);
            PMac.YB_EcatReconnectCmd            /**/                    = AddressMgr.GetAddress("PMAC_YB_CommonCmd", 3);
            PMac.YB_ReviewStopCmd            /**/                       = AddressMgr.GetAddress("PMAC_YB_CommonCmd", 4);
            PMac.YB_InspStopCmd            /**/                         = AddressMgr.GetAddress("PMAC_YB_CommonCmd", 5);
            PMac.YB_LoadRateSaveCmd            /**/                         = AddressMgr.GetAddress("PMAC_YB_CommonCmd", 6);
            PMac.YB_InspServerUseCmd            /**/                         = AddressMgr.GetAddress("PMAC_YB_CommonCmd", 7);

            PMac.XB_EquipStatusMotorInterlockOffAck  /**/               = AddressMgr.GetAddress("PMAC_XB_CommonAck", 0);
            PMac.XB_PmacResetCmdAck               /**/                  = AddressMgr.GetAddress("PMAC_XB_CommonAck", 1);
            PMac.XB_EmsCmdAck              /**/                         = AddressMgr.GetAddress("PMAC_XB_CommonAck", 2);
            PMac.XB_EcatReconnectAck         /**/                       = AddressMgr.GetAddress("PMAC_XB_CommonAck", 3);
            PMac.XB_ReviewStopAck         /**/                          = AddressMgr.GetAddress("PMAC_XB_CommonAck", 4);
            PMac.XB_InspStopAck         /**/                            = AddressMgr.GetAddress("PMAC_XB_CommonAck", 5);
            PMac.XB_LoadRateSaveAck     /**/                            = AddressMgr.GetAddress("PMAC_XB_CommonAck", 6);
            PMac.XB_InspServerUseAck    /**/                            = AddressMgr.GetAddress("PMAC_XB_CommonAck", 7);

            PMac.XF_StageXPPCmd = AddressMgr.GetAddress("XF_StageXPPCmd");
            PMac.XF_StageYPPCmd = AddressMgr.GetAddress("XF_StageYPPCmd");
            PMac.XF_StageTPPCmd = AddressMgr.GetAddress("XF_StageTPPCmd");
            PMac.XF_StageXHmSeq = AddressMgr.GetAddress("XF_StageXHmSeq");
            PMac.XF_StageYHmSeq = AddressMgr.GetAddress("XF_StageYHmSeq");
            PMac.XF_StageTHmSeq = AddressMgr.GetAddress("XF_StageTHmSeq");

            PMac.XF_ScanStep = AddressMgr.GetAddress("XF_ScanStep");

            PMac.XF_Mtr01TargetPos = AddressMgr.GetAddress("XF_Mtr01TargetPos");
            PMac.XF_Mtr02TargetPos = AddressMgr.GetAddress("XF_Mtr02TargetPos");
            PMac.XF_Mtr03TargetPos = AddressMgr.GetAddress("XF_Mtr03TargetPos");

            for (int jPos = 0; jPos < Motors.Length; jPos++)
            {
                int axisNo = this.Motors[jPos].OuterAxisNo;
                string axisStr = string.Format("Axis{0:D2}", axisNo);

                Motors[jPos].XB_StatusHomeCompleteBit = AddressMgr.GetAddress(string.Format("{0}_XB_HomeComplete", axisStr), axisNo - 1);
                Motors[jPos].XB_StatusHomeInPosition = AddressMgr.GetAddress(string.Format("{0}_XB_HomePositionOn", axisStr), axisNo - 1);
                Motors[jPos].XB_StatusMotorMoving = AddressMgr.GetAddress(string.Format("{0}_XB_Moving", axisStr), axisNo - 1);
                Motors[jPos].XB_StatusHWNegativeLimitSet = AddressMgr.GetAddress(string.Format("{0}_XB_HWMinusLimit", axisStr), axisNo - 1);
                Motors[jPos].XB_StatusHWPositiveLimitSet = AddressMgr.GetAddress(string.Format("{0}_XB_HWPlusLimit", axisStr), axisNo - 1);
                Motors[jPos].XB_StatusSWNegativeLimitSet = AddressMgr.GetAddress(string.Format("{0}_XB_SWMinusLimit", axisStr), axisNo - 1);
                Motors[jPos].XB_StatusSWPositiveLimitSet = AddressMgr.GetAddress(string.Format("{0}_XB_SWPlusLimit", axisStr), axisNo - 1);
                Motors[jPos].XB_StatusMotorServoOn = AddressMgr.GetAddress(string.Format("{0}_XB_ServoON", axisStr), axisNo - 1);
                Motors[jPos].XB_ErrFatalFollowingError = AddressMgr.GetAddress(string.Format("{0}_XB_FatalFollowingError", axisStr), axisNo - 1);
                Motors[jPos].XB_ErrAmpFaultError = AddressMgr.GetAddress(string.Format("{0}_XB_AmpFaultError", axisStr), axisNo - 1);
                Motors[jPos].XB_ErrI2TAmpFaultError = AddressMgr.GetAddress(string.Format("{0}_XB_I2TAmpFaultError", axisStr), axisNo - 1);
                Motors[jPos].XB_ErrCommonAlarm = AddressMgr.GetAddress(string.Format("{0}_XB_MotorAlarm", axisStr), axisNo - 1);
                Motors[jPos].XB_TargetPosMoveComplete = AddressMgr.GetAddress(string.Format("{0}_XB_TargetPosMoveComplete", axisStr), axisNo - 1);

                Motors[jPos].YB_HomeCmd = AddressMgr.GetAddress(string.Format("{0}_YB_HomeCmd", axisStr), axisNo - 1);
                Motors[jPos].YB_TargetPosMoveCmd = AddressMgr.GetAddress(string.Format("{0}_YB_TargetPosMoveCmd", axisStr), axisNo - 1);
                Motors[jPos].YB_JogMinusMove = AddressMgr.GetAddress(string.Format("{0}_YB_JogMinusCmd", axisStr), axisNo - 1);
                Motors[jPos].YB_JogPlusMove = AddressMgr.GetAddress(string.Format("{0}_YB_JogPlusCmd", axisStr), axisNo - 1);

                Motors[jPos].XB_HomeCmdAck = AddressMgr.GetAddress(string.Format("{0}_XB_HomeAck", axisStr), axisNo - 1);
                Motors[jPos].XB_TargetPosMoveAck = AddressMgr.GetAddress(string.Format("{0}_XB_TargetPosMoveAck", axisStr), axisNo - 1);
                Motors[jPos].XB_JogMinusMove = AddressMgr.GetAddress(string.Format("{0}_XB_JogMinusAck", axisStr), axisNo - 1);
                Motors[jPos].XB_JogPlusMove = AddressMgr.GetAddress(string.Format("{0}_XB_JogPlusAck", axisStr), axisNo - 1);

                Motors[jPos].XF_CurrMotorPosition = AddressMgr.GetAddress(string.Format("{0}_XF_CurPosition", axisStr));
                Motors[jPos].XF_CurrMotorSpeed = AddressMgr.GetAddress(string.Format("{0}_XF_CurSpeed", axisStr));
                Motors[jPos].XF_CurrMotorStress = AddressMgr.GetAddress(string.Format("{0}_XF_CurToque", axisStr));
                Motors[jPos].XF_TargetPosition = AddressMgr.GetAddress(string.Format("{0}_XF_TargetPosition", axisStr));
                Motors[jPos].XF_TargetSpeed = AddressMgr.GetAddress(string.Format("{0}_XF_TargetSpeed", axisStr));
                Motors[jPos].XF_TargetAccTime = AddressMgr.GetAddress(string.Format("{0}_XF_TargetAccTime", axisStr));
                Motors[jPos].XF_JogSpeed = AddressMgr.GetAddress(string.Format("{0}_XF_JogSpeed", axisStr));
                if (Motors[jPos].MotorType == EmMotorType.Linear)
                    Motors[jPos].XF_LoadRate = AddressMgr.GetAddress(string.Format("{0}_XF_LoadRate", axisStr));

                Motors[jPos].YF_TargetPosition = AddressMgr.GetAddress(string.Format("{0}_YF_TargetPosition", axisStr));
                Motors[jPos].YF_TargetSpeed = AddressMgr.GetAddress(string.Format("{0}_YF_TargetSpeed", axisStr));
                Motors[jPos].YF_TargetAccTime = AddressMgr.GetAddress(string.Format("{0}_YF_TargetAccTime", axisStr));
                Motors[jPos].YF_JogSpeed = AddressMgr.GetAddress(string.Format("{0}_YF_JogSpeed", axisStr));
                if (Motors[jPos].MotorType == EmMotorType.Linear)
                    Motors[jPos].YF_LoadRate = AddressMgr.GetAddress(string.Format("{0}_YF_LoadRate", axisStr));
            }
            #endregion
            #region EziStepMotor
            foreach (var motor in StepMotors)
            {
                int slaveNo = motor.InnerSlaveNo;

                motor.XB_StatusHomeCompleteBit = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusHomeCompleteBit", slaveNo));
                motor.XB_StatusMotorMoving = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusMotorMoving", slaveNo));
                motor.XB_StatusMinusLimitSet = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusMinusLimitSet", slaveNo));
                motor.XB_StatusPlusLimitSet = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusPlusLimitSet", slaveNo));
                motor.XB_StatusMotorServoOn = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusMotorServoOn", slaveNo));
                motor.XB_StatusHomeInPosition = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusHomeInPosition", slaveNo));
                motor.XB_StatusMotorInPosition = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusMotorInPosition", slaveNo));

                motor.XF_CurrMotorPosition = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_CurrMotorPosition", slaveNo));
                motor.XF_CurrMotorSpeed = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_CurrMotorSpeed", slaveNo));
                motor.XF_CurrMotorAccel = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_CurrMotorAccel", slaveNo));

                motor.YB_MotorStopCmd = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YB_MotorStopCmd", slaveNo));
                motor.XB_MotorStopCmdAck = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_MotorStopCmdAck", slaveNo));
                motor.YB_HomeCmd = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YB_HomeCmd", slaveNo));
                motor.XB_HomeCmdAck = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_HomeCmdAck", slaveNo));

                motor.YB_MotorJogMinusMove = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YB_MotorJogMinusMove", slaveNo));
                motor.YB_MotorJogPlusMove = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YB_MotorJogPlusMove", slaveNo));
                motor.YF_MotorJogSpeedCmd = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YF_MotorJogSpeedCmd", slaveNo));
                motor.XF_MotorJogSpeedCmdAck = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_MotorJogSpeedCmdAck", slaveNo));

                motor.YB_PTPMoveCmd = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YB_PTPMoveCmd", slaveNo));
                motor.XB_PTPMoveCmdAck = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_PTPMoveCmdAck", slaveNo));
                motor.YF_PTPMovePosition = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YF_PTPMovePosition", slaveNo));
                motor.XF_PTPMovePositionAck = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_PTPMovePositionAck", slaveNo));
                motor.YF_PTPMoveSpeed = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YF_PTPMoveSpeed", slaveNo));
                motor.XF_PTPMoveSpeedAck = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_PTPMoveSpeedAck", slaveNo));
                motor.YF_PTPMoveAccel = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YF_PTPMoveAccel", slaveNo));
                motor.XF_PTPMoveAccelAck = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_PTPMoveAccelAck", slaveNo));
            }

            foreach (var stepMotor in StepMotors)
            {
                int slaveNo = stepMotor.InnerSlaveNo;

                EziSvoMotorProxy motor = new EziSvoMotorProxy((EmEziStep)slaveNo) { Name = stepMotor.Name, Ezi = GG.EZI as VirtualEziDirect };

                motor.XB_StatusHomeCompleteBit = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusHomeCompleteBit", slaveNo));
                motor.XB_StatusMotorMoving = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusMotorMoving", slaveNo));
                motor.XB_StatusMinusLimitSet = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusMinusLimitSet", slaveNo));
                motor.XB_StatusPlusLimitSet = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusPlusLimitSet", slaveNo));
                motor.XB_StatusMotorServoOn = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusMotorServoOn", slaveNo));
                motor.XB_StatusHomeInPosition = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusHomeInPosition", slaveNo));
                motor.XB_StatusMotorInPosition = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_StatusMotorInPosition", slaveNo));

                motor.XF_CurrMotorPosition = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_CurrMotorPosition", slaveNo));
                motor.XF_CurrMotorSpeed = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_CurrMotorSpeed", slaveNo));
                motor.XF_CurrMotorAccel = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_CurrMotorAccel", slaveNo));

                motor.YB_MotorStopCmd = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YB_MotorStopCmd", slaveNo));
                motor.XB_MotorStopCmdAck = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_MotorStopCmdAck", slaveNo));
                motor.YB_HomeCmd = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YB_HomeCmd", slaveNo));
                motor.XB_HomeCmdAck = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_HomeCmdAck", slaveNo));

                motor.YB_MotorJogMinusMove = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YB_MotorJogMinusMove", slaveNo));
                motor.YB_MotorJogPlusMove = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YB_MotorJogPlusMove", slaveNo));
                motor.YF_MotorJogSpeedCmd = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YF_MotorJogSpeedCmd", slaveNo));
                motor.XF_MotorJogSpeedCmdAck = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_MotorJogSpeedCmdAck", slaveNo));

                motor.YB_PTPMoveCmd = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YB_PTPMoveCmd", slaveNo));
                motor.XB_PTPMoveCmdAck = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XB_PTPMoveCmdAck", slaveNo));
                motor.YF_PTPMovePosition = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YF_PTPMovePosition", slaveNo));
                motor.XF_PTPMovePositionAck = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_PTPMovePositionAck", slaveNo));
                motor.YF_PTPMoveSpeed = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YF_PTPMoveSpeed", slaveNo));
                motor.XF_PTPMoveSpeedAck = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_PTPMoveSpeedAck", slaveNo));
                motor.YF_PTPMoveAccel = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_YF_PTPMoveAccel", slaveNo));
                motor.XF_PTPMoveAccelAck = AddressMgr.GetAddress(string.Format("Step_Motor_{0}_XF_PTPMoveAccelAck", slaveNo));

                if (GG.TestMode)
                {
                    //(GG.EZI as VirtualMem).Motors.Add(motor);
                }
                else
                {
                    (GG.EZI as VirtualEziDirect).Motors.Add(motor);
                }

            }
            #endregion
        }

        public void UpdateKerfData(float[] kerfData)
        {
            WaferInfo info = TransferDataMgr.GetWafer(this.TransferUnit.LowerWaferKey);

            if (info == null || info.CstID == "")
            {
                Logger.Log.AppendLine(LogLevel.Error, "커프 데이터 갱신 실패, 웨이퍼 정보가 없음");
            }

            int idx = 0;
            info.KerfDataCh1_1 = (double)kerfData[idx++];
            info.KerfDataCh1_2 = (double)kerfData[idx++];
            info.KerfDataCh1_3 = (double)kerfData[idx++];
            info.KerfDataCh1_4 = (double)kerfData[idx++];
            info.KerfDataCh1_5 = (double)kerfData[idx++];
            info.KerfDataCh1_6 = (double)kerfData[idx++];
            info.KerfDataCh1_7 = (double)kerfData[idx++];
            info.KerfDataCh1_8 = (double)kerfData[idx++];
            info.KerfDataCh1_9 = (double)kerfData[idx++];
            info.KerfDataCh1_10 = (double)kerfData[idx++];
            info.KerfDataCh1_11 = (double)kerfData[idx++];
            info.KerfDataCh1_12 = (double)kerfData[idx++];
            info.KerfDataCh1_13 = (double)kerfData[idx++];
            info.KerfDataCh1_14 = (double)kerfData[idx++];
            info.KerfDataCh1_15 = (double)kerfData[idx++];
            info.KerfDataCh2_1 = (double)kerfData[idx++];
            info.KerfDataCh2_2 = (double)kerfData[idx++];
            info.KerfDataCh2_3 = (double)kerfData[idx++];
            info.KerfDataCh2_4 = (double)kerfData[idx++];
            info.KerfDataCh2_5 = (double)kerfData[idx++];
            info.KerfDataCh2_6 = (double)kerfData[idx++];
            info.KerfDataCh2_7 = (double)kerfData[idx++];
            info.KerfDataCh2_8 = (double)kerfData[idx++];
            info.KerfDataCh2_9 = (double)kerfData[idx++];
            info.KerfDataCh2_10 = (double)kerfData[idx++];
            info.KerfDataCh2_11 = (double)kerfData[idx++];
            info.KerfDataCh2_12 = (double)kerfData[idx++];
            info.KerfDataCh2_13 = (double)kerfData[idx++];
            info.KerfDataCh2_14 = (double)kerfData[idx++];
            info.KerfDataCh2_15 = (double)kerfData[idx++];

            string kerfLog = string.Empty;
            kerfLog += "\n";
            kerfLog += info.KerfDataCh1_1.ToString() + ", ";
            kerfLog += info.KerfDataCh1_2.ToString() + ", ";
            kerfLog += info.KerfDataCh1_3.ToString() + ", ";
            kerfLog += info.KerfDataCh1_4.ToString() + ", ";
            kerfLog += info.KerfDataCh1_5.ToString() + ", ";
            kerfLog += info.KerfDataCh1_6.ToString() + ", ";
            kerfLog += info.KerfDataCh1_7.ToString() + ", ";
            kerfLog += info.KerfDataCh1_8.ToString() + ", ";
            kerfLog += info.KerfDataCh1_9.ToString() + ", ";
            kerfLog += info.KerfDataCh1_10.ToString() + ", ";
            kerfLog += info.KerfDataCh1_11.ToString() + ", ";
            kerfLog += info.KerfDataCh1_12.ToString() + ", ";
            kerfLog += info.KerfDataCh1_13.ToString() + ", ";
            kerfLog += info.KerfDataCh1_14.ToString() + ", ";
            kerfLog += info.KerfDataCh1_15.ToString() + "\n";
            kerfLog += info.KerfDataCh2_1.ToString() + ", ";
            kerfLog += info.KerfDataCh2_2.ToString() + ", ";
            kerfLog += info.KerfDataCh2_3.ToString() + ", ";
            kerfLog += info.KerfDataCh2_4.ToString() + ", ";
            kerfLog += info.KerfDataCh2_5.ToString() + ", ";
            kerfLog += info.KerfDataCh2_6.ToString() + ", ";
            kerfLog += info.KerfDataCh2_7.ToString() + ", ";
            kerfLog += info.KerfDataCh2_8.ToString() + ", ";
            kerfLog += info.KerfDataCh2_9.ToString() + ", ";
            kerfLog += info.KerfDataCh2_10.ToString() + ", ";
            kerfLog += info.KerfDataCh2_11.ToString() + ", ";
            kerfLog += info.KerfDataCh2_12.ToString() + ", ";
            kerfLog += info.KerfDataCh2_13.ToString() + ", ";
            kerfLog += info.KerfDataCh2_14.ToString() + ", ";
            kerfLog += info.KerfDataCh2_15.ToString() + ", ";

            bool result = info.Update();

            if (result == true)
            {
                Logger.Log.AppendLine(LogLevel.Info, "kerf Data 업데이트 : {0}", kerfLog);
            }
            else
            {
                Logger.Log.AppendLine(LogLevel.Info, "kerf Data 업데이트 실패");
            }
        }

        public bool IsCanProgress(int portNo)
        {
            if (IsWaferDetect == EmGlassDetect.ALL)
            {
                if (this.TransferUnit.LowerWaferKey.CstID == (portNo == 1 ? Efem.LoadPort1.CstKey.ID : Efem.LoadPort2.CstKey.ID))
                    return true;
            }

            else if (Efem.Aligner.Status.IsWaferExist)
            {
                if (Efem.Aligner.LowerWaferKey.CstID == (portNo == 1 ? Efem.LoadPort1.CstKey.ID : Efem.LoadPort2.CstKey.ID))
                    return true;
            }

            else if (Efem.Robot.Status.IsUpperArmVacOn)
            {
                if (Efem.Robot.UpperWaferKey.CstID == (portNo == 1 ? Efem.LoadPort1.CstKey.ID : Efem.LoadPort2.CstKey.ID))
                    return true;
            }

            else if (Efem.Robot.Status.IsLowerArmVacOn)
            {
                if (Efem.Robot.LowerWaferKey.CstID == (portNo == 1 ? Efem.LoadPort1.CstKey.ID : Efem.LoadPort2.CstKey.ID))
                    return true;
            }

            return false;
        }

        public void ReportRecipeSelect(int loadPortNo)
        {
            string recipe = (loadPortNo == 1 ? LPM1Recipe : LPM2Recipe);
            HsmsRecipeInfo info = new HsmsRecipeInfo();
            info.RecipeMode = RecipeMode.RECIPE_SELECT;
            info.IsOK = Ack.OK;
            info.RecipeID = recipe;

            HsmsPc.StartCommand(this, EmHsmsPcCommand.RECIPE_SELECT, info);
            Logger.CIMLog.AppendLine("Recipe Select 변경 보고 [ Recipe Mode : Recipe Select ]");
        }

        public bool ChangeRecipe(int portNo, string recipeName)
        {
            try
            {
                if (Efem.LoadPort((EmEfemPort)portNo).IsProcessingStep && (TransferUnit.LowerWaferKey.CstID != "" || TransferUnit.LowerWaferKey.CstID != null))
                {
                    AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0556_PORT_RECIPE_CHANGE_FAIL);
                    Logger.CIMLog.AppendLine(LogLevel.Error, portNo.ToString() + "번 LPM 레시피 변경 실패 : 이미 해당 카세트의 웨이퍼가 검사 진행중입니다.");

                    HsmsRecipeInfo info = new HsmsRecipeInfo();
                    info.RecipeID = recipeName;
                    info.IsOK = Ack.NACK;
                    HsmsPc.StartCommand(this, EmHsmsPcCommand.RECIPE_SELECT, info);

                    return false;
                }
                else
                {
                    Recipe rcp = RecipeDataMgr.GetRecipe(recipeName);
                    if (rcp == null)
                    {
                        rcp = new Recipe();
                        rcp.Name = recipeName;
                        rcp.Desc = DateTime.Now.ToString("MM월 dd일") + " HOST로부터 만들어진 Recipe";
                        RecipeDataMgr.Insert(rcp);
                    }

                    CassetteInfo cst = TransferDataMgr.GetCst(Efem.LoadPort((EmEfemPort)portNo).CstKey);
                    if (cst != null)
                    {
                        cst.RecipeName = recipeName;
                        cst.Update();

                        Logger.Log.AppendLine("[PP SELECT EVENT RECV] 카세트 정보가 존재하여 새로운 레시피로 업데이트  Cst ID : {0}  Recipe Name : {1}", cst.CstID, cst.RecipeName);
                    }

                    Recipe cur = portNo == 1 ? RecipeDataMgr.GetRecipe(RecipeDataMgr.CurLPM1Recipe) : RecipeDataMgr.GetRecipe(RecipeDataMgr.CurLPM2Recipe);
                    cur.Desc = rcp.Name;
                    cur.Update();
                    Logger.Log.AppendLine("[PP SELECT EVENT RECV] 내부 DB 레시피 업데이트  Port No : {0}  Recipe Name : {1}", portNo.ToString(), rcp.Name);

                    HsmsRecipeInfo info = new HsmsRecipeInfo();
                    info.RecipeID = rcp.Name;

                    if (portNo == 1)
                    {
                        info.RecipeMode = RecipeMode.PORT_1_RECIPE_SELECT;
                        info.IsOK = Ack.OK;
                        GG.Equip.LPM1Recipe = RecipeDataMgr.GetCurRecipeName(0);
                    }
                    else
                    {
                        info.RecipeMode = RecipeMode.PORT_2_RECIPE_SELECT;
                        info.IsOK = Ack.OK;
                        GG.Equip.LPM2Recipe = RecipeDataMgr.GetCurRecipeName(1);
                    }

                    //Port Recipe Select 보고
                    GG.Equip.HsmsPc.RecipeReport(info);

                    //Recipe Select 보고
                    HsmsRecipeInfo info2 = new HsmsRecipeInfo();
                    info2.IsOK = Ack.OK;
                    info2.RecipeID = rcp.Name;
                    info2.RecipeMode = RecipeMode.RECIPE_SELECT;

                    GG.Equip.HsmsPc.RecipeReport(info2);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.CIMLog.AppendLine(LogLevel.Error, "Recipe 변경 실패" + ex.Message);
                AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0556_PORT_RECIPE_CHANGE_FAIL);
                return false;
            }
        }

        public void DoorOpenOrClose()
        {
            OffCheckSwitch[] arrDoor =
            {
                TopDoor01,
                TopDoor02,
                TopDoor03,
                TopDoor04,
            };

            if (this.ModeSelectKey.IsAuto == true)
            {
                InterLockMgr.AddInterLock("인터락<AUTO MODE>\n(AUTO MODE 상태에서는 Door Open이 불가능합니다.)");
                return;
            }
            if (this.IsHomePositioning || this.EquipRunMode == EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock("인터락<동작중>\n설비가 동작중입니다. 설비 설정을 변경 할 수 없습니다.");
                return;
            }

            bool toggle = !TopDoor03.IsSolOnOff;
            foreach (OffCheckSwitch door in arrDoor)
            {
                door.OnOff(this, toggle);
            }
        }

        public void WorkingLightOnorOff()
        {
            if (GG.TestMode == true)
                return;
            this.WorkingLight.Toggle();
        }

        private void InterlockSetting()
        {
            Func<Equipment, bool, bool> isBlowerOnInterlock = delegate (Equipment equip, bool isOn)
            {
                if (isOn)
                {
                    if (this.IsUseInterLockOff == false)
                        if (this.Vacuum.IsVacuumSolOn)
                        {
                            InterLockMgr.AddInterLock("인터락<VACUUM>\n(Vacuum Sol On상태에서 블로워를 수행 할 수 없습니다.)");
                            Logger.Log.AppendLine(LogLevel.Warning, "Vacuum Sol On상태에서 블로워를 수행 할 수 없습니다");
                            return true;

                        }
                }
                else
                {

                }
                return false;
            };
            Blower.Stage1.InterLockFunc = isBlowerOnInterlock;
            Blower.Stage2.InterLockFunc = isBlowerOnInterlock;
        }
        private void InitalizeAlarmList()
        {
            #region Door알람
            TopDoor01.OFF_TIME_OUT_ERROR = EM_AL_LST.AL_0020_EQP_TOP_DOOR_01_OPEN_ERROR;
            TopDoor02.OFF_TIME_OUT_ERROR = EM_AL_LST.AL_0021_EQP_TOP_DOOR_02_OPEN_ERROR;
            TopDoor03.OFF_TIME_OUT_ERROR = EM_AL_LST.AL_0022_EQP_TOP_DOOR_03_OPEN_ERROR;
            TopDoor04.OFF_TIME_OUT_ERROR = EM_AL_LST.AL_0023_EQP_TOP_DOOR_04_OPEN_ERROR;
            #endregion
            #region PMAC
            PMac.HEAVY_ERROR            /**/        = EM_AL_LST.AL_0470_PMAC_HEAVY_ERROR;

            StageX.HW_PLUS_LIMIT_ERROR      /**/    = EM_AL_LST.AL_0490_STAGE_X_PLUS_LIMIT_ERROR;
            StageX.HW_MINUS_LIMIT_ERROR     /**/    = EM_AL_LST.AL_0491_STAGE_X_MINUS_LIMIT_ERROR;
            StageX.SW_PLUS_LIMIT_ERROR      /**/    = EM_AL_LST.AL_0490_STAGE_X_PLUS_LIMIT_ERROR;
            StageX.SW_MINUS_LIMIT_ERROR     /**/    = EM_AL_LST.AL_0491_STAGE_X_MINUS_LIMIT_ERROR;
            StageX.MOTOR_SERVO_ON_ERROR  /**/       = EM_AL_LST.AL_0492_STAGE_X_MOTOR_SERVO_ON_ERROR;
            StageX.FATAL_FOLLOWING_ERROR /**/       = EM_AL_LST.AL_0493_STAGE_X_FATAL_FOLLOWING_ERROR;
            StageX.AMP_FAULT_ERROR       /**/       = EM_AL_LST.AL_0494_STAGE_X_AMP_FAULT_ERROR;
            StageX.I2T_AMP_FAULT_ERROR   /**/       = EM_AL_LST.AL_0495_STAGE_X_I2T_AMP_FAULT_ERROR;
            StageX.MOVE_OVERTIME_ERROR   /**/       = EM_AL_LST.AL_0496_STAGE_X_MOVE_OVERTIME_ERROR;
            StageX.PTP_WRITE_TIMEOUT_ERROR = EM_AL_LST.AL_0498_STAGE_X_PTP_WRITE_TIMEOUT_ERROR;
            StageX.HOME_CMD_ACK_ERROR = EM_AL_LST.AL_0499_STAGE_X_HOME_CMD_ACK_ERROR;
            StageX.PTP_CMD_ACK_ERROR = EM_AL_LST.AL_0500_STAGE_X_PTP_CMD_ACK_ERROR;

            StageY.HW_PLUS_LIMIT_ERROR      /**/    = EM_AL_LST.AL_0505_STAGE_Y_PLUS_LIMIT_ERROR;
            StageY.HW_MINUS_LIMIT_ERROR     /**/    = EM_AL_LST.AL_0506_STAGE_Y_MINUS_LIMIT_ERROR;
            StageY.SW_PLUS_LIMIT_ERROR      /**/    = EM_AL_LST.AL_0505_STAGE_Y_PLUS_LIMIT_ERROR;
            StageY.SW_MINUS_LIMIT_ERROR     /**/    = EM_AL_LST.AL_0506_STAGE_Y_MINUS_LIMIT_ERROR;
            StageY.MOTOR_SERVO_ON_ERROR  /**/       = EM_AL_LST.AL_0507_STAGE_Y_MOTOR_SERVO_ON_ERROR;
            StageY.FATAL_FOLLOWING_ERROR /**/       = EM_AL_LST.AL_0508_STAGE_Y_FATAL_FOLLOWING_ERROR;
            StageY.AMP_FAULT_ERROR       /**/       = EM_AL_LST.AL_0509_STAGE_Y_AMP_FAULT_ERROR;
            StageY.I2T_AMP_FAULT_ERROR   /**/       = EM_AL_LST.AL_0510_STAGE_Y_I2T_AMP_FAULT_ERROR;
            StageY.MOVE_OVERTIME_ERROR   /**/       = EM_AL_LST.AL_0511_STAGE_Y_MOVE_OVERTIME_ERROR;
            StageY.PTP_WRITE_TIMEOUT_ERROR = EM_AL_LST.AL_0512_STAGE_Y_PTP_WRITE_TIMEOUT_ERROR;
            StageY.HOME_CMD_ACK_ERROR = EM_AL_LST.AL_0513_STAGE_Y_HOME_CMD_ACK_ERROR;
            StageY.PTP_CMD_ACK_ERROR = EM_AL_LST.AL_0514_STAGE_Y_PTP_CMD_ACK_ERROR;

            Theta.HW_PLUS_LIMIT_ERROR      /**/= EM_AL_LST.AL_0520_THETA_PLUS_LIMIT_ERROR;
            Theta.HW_MINUS_LIMIT_ERROR     /**/= EM_AL_LST.AL_0521_THETA_MINUS_LIMIT_ERROR;
            Theta.MOTOR_SERVO_ON_ERROR     /**/= EM_AL_LST.AL_0522_THETA_MOTOR_SERVO_ON_ERROR;
            Theta.FATAL_FOLLOWING_ERROR    /**/= EM_AL_LST.AL_0523_THETA_FATAL_FOLLOWING_ERROR;
            Theta.AMP_FAULT_ERROR          /**/= EM_AL_LST.AL_0524_THETA_AMP_FAULT_ERROR;
            Theta.I2T_AMP_FAULT_ERROR      /**/= EM_AL_LST.AL_0525_THETA_I2T_AMP_FAULT_ERROR;
            Theta.MOVE_OVERTIME_ERROR      /**/= EM_AL_LST.AL_0526_THETA_MOVE_OVERTIME_ERROR;
            Theta.PTP_WRITE_TIMEOUT_ERROR = EM_AL_LST.AL_0527_THETA_PTP_WRITE_TIMEOUT_ERROR;
            Theta.HOME_CMD_ACK_ERROR = EM_AL_LST.AL_0528_THETA_HOME_CMD_ACK_ERROR;
            Theta.PTP_CMD_ACK_ERROR = EM_AL_LST.AL_0529_THETA_PTP_CMD_ACK_ERROR;
            #endregion PMAC
            #region EziStep
            AlignerX.MOVE_OVERTIME_ERROR = EM_AL_LST.AL_0533_ALIGNER_X_MOVE_OVERTIME_ERROR;
            AlignerX.SERVO_ON_ERROR = EM_AL_LST.AL_0534_ALIGNER_X_SERVO_ON_ERROR;
            AlignerY.MOVE_OVERTIME_ERROR = EM_AL_LST.AL_0535_ALIGNER_Y_MOVE_OVERTIME_ERROR;
            AlignerY.SERVO_ON_ERROR = EM_AL_LST.AL_0536_ALIGNER_Y_SERVO_ON_ERROR;
            AlignerT.MOVE_OVERTIME_ERROR = EM_AL_LST.AL_0537_ALIGNER_T_MOVE_OVERTIME_ERROR;
            AlignerT.SERVO_ON_ERROR = EM_AL_LST.AL_0538_ALIGNER_T_SERVO_ON_ERROR;
            #endregion
            #region  바큠_알람
            Vacuum.Stage1.ON_TIME_OUT_ERROR      /**/= EM_AL_LST.AL_0106_STAGE_VACCUM1_SOL_ON_TIME_OUT_ERROR;
            Vacuum.Stage1.OFF_TIME_OUT_ERROR     /**/= EM_AL_LST.AL_0107_STAGE_VACCUM1_SOL_OFF_TIME_OUT_ERROR;
            Vacuum.Stage2.ON_TIME_OUT_ERROR      /**/= EM_AL_LST.AL_0108_RING_FRAME_VACCUM2_SOL_ON_TIME_OUT_ERROR;
            Vacuum.Stage2.OFF_TIME_OUT_ERROR     /**/= EM_AL_LST.AL_0109_RING_FRAME_VACCUM2_SOL_OFF_TIME_OUT_ERROR;
            //StageVacuumCtrl2.ON_TIME_OUT_ERROR      /**/= EM_AL_LST.AL_0257_STAGE_VACCUM2_SOL_ON_TIME_OUT_ERROR;
            //StageVacuumCtrl2.OFF_TIME_OUT_ERROR     /**/= EM_AL_LST.AL_0258_STAGE_VACCUM2_SOL_OFF_TIME_OUT_ERROR;
            //StageVacuumCtrl3.ON_TIME_OUT_ERROR      /**/= EM_AL_LST.AL_0259_STAGE_VACCUM3_SOL_ON_TIME_OUT_ERROR;
            //StageVacuumCtrl3.OFF_TIME_OUT_ERROR     /**/= EM_AL_LST.AL_0260_STAGE_VACCUM3_SOL_OFF_TIME_OUT_ERROR;
            //StageVacuumCtrl4.ON_TIME_OUT_ERROR      /**/= EM_AL_LST.AL_0261_STAGE_VACCUM4_SOL_ON_TIME_OUT_ERROR;
            //StageVacuumCtrl4.OFF_TIME_OUT_ERROR     /**/= EM_AL_LST.AL_0262_STAGE_VACCUM4_SOL_OFF_TIME_OUT_ERROR;
            //StageVacuumCtrl5.ON_TIME_OUT_ERROR      /**/= EM_AL_LST.AL_0263_STAGE_VACCUM5_SOL_ON_TIME_OUT_ERROR;
            //StageVacuumCtrl5.OFF_TIME_OUT_ERROR     /**/= EM_AL_LST.AL_0264_STAGE_VACCUM5_SOL_OFF_TIME_OUT_ERROR;
            AlignerVac.Vacuum.ON_TIME_OUT_ERROR = EM_AL_LST.AL_0115_ALIGNER_VACUUM_SOL_ON_TIME_OUT_ERROR;
            AlignerVac.Vacuum.OFF_TIME_OUT_ERROR = EM_AL_LST.AL_0116_ALIGNER_VACUUM_SOL_OFF_TIME_OUT_ERROR;
            #endregion
            #region 실린더알람
            LiftPin.AlcdForwardTimeOut[0] = EM_AL_LST.AL_0204_LIFT_PIN_1_UP_ERROR;
            LiftPin.AlcdForwardTimeOut[1] = EM_AL_LST.AL_0205_LIFT_PIN_2_UP_ERROR;
            LiftPin.AlcdBackwardTimeOut[0] = EM_AL_LST.AL_0208_LIFT_PIN_1_DOWN_ERROR;
            LiftPin.AlcdBackwardTimeOut[1] = EM_AL_LST.AL_0209_LIFT_PIN_2_DOWN_ERROR;

            StandCentering.AlcdForwardTimeOut[0] = EM_AL_LST.AL_0224_STAND_CENTERING1_FWD_ERROR;
            StandCentering.AlcdForwardTimeOut[1] = EM_AL_LST.AL_0225_STAND_CENTERING2_FWD_ERROR;
            StandCentering.AlcdBackwardTimeOut[0] = EM_AL_LST.AL_0224_STAND_CENTERING1_FWD_ERROR;
            StandCentering.AlcdBackwardTimeOut[1] = EM_AL_LST.AL_0225_STAND_CENTERING2_FWD_ERROR;
            #endregion
            #region  LAMP_알람
            #endregion
            #region AD
            MainAir1.SENSOR_ONOFF_ERROR                        /***/= EM_AL_LST.AL_0100_MAIN_AIR_1_ERROR;
            MainAir2.SENSOR_ONOFF_ERROR                                /***/= EM_AL_LST.AL_0101_MAIN_AIR_2_ERROR;
            MainVacuum1.SENSOR_ONOFF_ERROR = EM_AL_LST.AL_0104_MAIN_VACUUM1_ERROR;
            MainVacuum2.SENSOR_ONOFF_ERROR = EM_AL_LST.AL_0105_MAIN_VACUUM2_ERROR;

            PinIonizer.AlarmCommonAlarm[0] = EM_AL_LST.AL_0063_PIN_IONIZER_ABNORMAL_ALARM;
            #endregion
            Isolator1.SENSOR_ONOFF_ERROR = EM_AL_LST.AL_0080_ISOLATOR1_ERROR;
            Isolator2.SENSOR_ONOFF_ERROR = EM_AL_LST.AL_0081_ISOLATOR2_ERROR;
            Isolator3.SENSOR_ONOFF_ERROR = EM_AL_LST.AL_0082_ISOLATOR3_ERROR;
        }

        public bool CheckCstMapData(EmEfemMappingInfo[] mappingData, string _cstID, EmEfemPort port)
        {
            if (GG.CimTestMode == true || GG.Equip.CimMode == EmCimMode.OffLine || GG.Equip.CimMode == EmCimMode.Local)
                return true;

            string recvWaferList;
            recvWaferList = GG.MEM_DIT.VirGetAscii(CIMAW.XW_CST_MAP_WAFER_ID_LIST).Replace("\0", string.Empty);

            string InspMode;
            InspMode = GG.MEM_DIT.VirGetAscii(CIMAW.XW_CST_MAP_INS_MODE).Replace("\0", string.Empty);

            string CstID;
            CstID = GG.MEM_DIT.VirGetAscii(CIMAW.XW_CST_MAP_CST_ID).Replace("\0", string.Empty);

            Logger.CIMLog.AppendLine(LogLevel.Info, string.Format("Check Cassette Map Data [1] Wafer List : {0} [2] Insp Mode : {1} [3] Cst ID : {2}", recvWaferList, InspMode, CstID));
            InspMode = InspMode.Trim();

            // 21년 5월 고객사 요청 사항으로 상위메시지로 모든 매핑 웨이퍼 검사 가능하도록 기능 추가
            if (InspMode.Equals("ALL"))
            {
                switch (port) //230905 KYH 포트별로 전체검사 셋팅 수정
                {
                    case EmEfemPort.LOADPORT1:
                        this.Efem.LoadPort1.ProgressWay = EmProgressWay.Mapping;
                        Logger.Log.AppendLine(LogLevel.Info, string.Format("상위로부터 [ALL] 수신하여 전체 검사 모드 변경 : PORT1"));
                        break;

                    case EmEfemPort.LOADPORT2:
                        GG.Equip.Efem.LoadPort2.ProgressWay = EmProgressWay.Mapping;
                        Logger.Log.AppendLine(LogLevel.Info, string.Format("상위로부터 [ALL] 수신하여 전체 검사 모드 변경 : PORT2"));
                        break;
                }
                //this.Efem.LoadPort1.ProgressWay = GG.Equip.Efem.LoadPort2.ProgressWay = EmProgressWay.Mapping;
                //Logger.Log.AppendLine(LogLevel.Info, string.Format("상위로부터 [ALL] 수신하여 전체 검사 모드 변경"));
            }
            else
            {
                switch (port) //230905 KYH 포트별로 검사 셋팅 수정
                {
                    case EmEfemPort.LOADPORT1:
                        GG.Equip.Efem.LoadPort1.ProgressWay = (EmProgressWay)CtrlSetting.LPMProgressWay;
                        break;

                    case EmEfemPort.LOADPORT2:
                        GG.Equip.Efem.LoadPort2.ProgressWay = (EmProgressWay)CtrlSetting.LPMProgressWay;
                        break;
                }
                //GG.Equip.Efem.LoadPort1.ProgressWay = GG.Equip.Efem.LoadPort2.ProgressWay = (EmProgressWay)CtrlSetting.LPMProgressWay;
            }

            CassetteInfo cstInfo = TransferDataMgr.GetCst(_cstID);
            cstInfo.HWaferIDList = recvWaferList;
            cstInfo.Update();

            return true;
        }

        public StepBase GetPort(EmEfemPort port)
        {
            switch (port)
            {
                case EmEfemPort.LOADPORT1: return Efem.LoadPort1;
                case EmEfemPort.LOADPORT2: return Efem.LoadPort2;
                case EmEfemPort.ROBOT: return Efem.Robot;
                case EmEfemPort.ALIGNER: return Efem.Aligner;
                case EmEfemPort.EQUIPMENT: return this.TransferUnit;
                default: return null;
            }
        }

        public event EventHandler MoveRobotArmEvent;
        public void MoveRobotArm(EmEfemRobotArm arm, EmEfemPort port)
        {
            EventHandler handler = MoveRobotArmEvent;
            if (handler != null)
            {
                bool[] b = new bool[2];
                b[0] = arm == EmEfemRobotArm.Upper;
                b[1] = port == EmEfemPort.EQUIPMENT;
                handler(b, null);
            }
        }

        private void InitalizeMccList()
        {

        }

        //설비 커맨드        
        public bool CmdHomePositioning()
        {
            if (IsPause == true)
            {
                InterLockMgr.AddInterLock("인터락<PAUSE>\n(Pause 상태에서 Home Position 이동을 할 수 없습니다.)");
                Logger.Log.AppendLine(LogLevel.Info, "Pause 상태에서 Home 이동 안됨");
                return false;
            }

            if (IsDoorOpen == true && GG.TestMode == false && IsUseInterLockOff == false && IsUseDoorInterLockOff == false)
            {
                InterLockMgr.AddInterLock("인터락<DOOR>\n(DOOR Open 상태에서 Home Position 이동을 할 수 없습니다.)");
                Logger.Log.AppendLine(LogLevel.Info, "DOOR Open 상태에서 Home 이동 안됨");
                return false;
            }

            if (IsEmergency == true && GG.TestMode == false)
            {
                InterLockMgr.AddInterLock("인터락<EMERGENCY>\n(EMERGENCY 상태에서 Home Position 이동을 할 수 없습니다.)");
                Logger.Log.AppendLine(LogLevel.Info, "EMERGENCY 상태에서 Home 이동 안됨");
                return false;
            }

            if (IsHomePositioning == true)
            {
                InterLockMgr.AddInterLock("인터락<실행 중>\n(Home Position 동작 중입니다.)");
                Logger.Log.AppendLine(LogLevel.Info, "Home Position 이동중 !!!!!");
                return false;
            }

            if (EquipRunMode == EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock("인터락<AUTO MODE>\n(AutoMode일때 Home Position 이동을 할 수 없습니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "AutoMode일때 Home Position 이동 안됨 ");
                return false;
            }

            this.StHoming.StepStart(this);
            return true;
        }
        public void CmdLoading()
        {
            StLoading.StepStart(this);
        }
        public void CmdScanStep()
        {
            StScanning.StepStart(this);
        }
        public void CmdUnLoadingStep()
        {
            StUnloading.StepStart(this);
        }


        //로직 처리.
        public void LogicWorking()
        {
            try
            {
                ReadFromPLC();
                if (this.IsIoTestMode == false)
                {
                    EquipStatusCheck();
                    EquipStatusReport();

                    StSafety.LogicWorking(this);

                    PMac.LogicWorking(this);

                    LiftPin.LogicWorking(this);

                    Isolator1.LogicWorking(this);
                    Isolator2.LogicWorking(this);
                    Isolator3.LogicWorking(this);

                    MainAir1.LogicWorking(this);
                    MainAir2.LogicWorking(this);
                    MainVacuum1.LogicWorking(this);
                    MainVacuum2.LogicWorking(this);

                    Vacuum.Stage1.LogicWorking(this);
                    Vacuum.Stage2.LogicWorking(this);

                    Vacuum.LogicWorking(this);
                    AlignerVac.LogicWorking(this);
                    Centering.LogicWorking(this);

                    PinIonizer.LogicWorking(this);

                    foreach (ServoMotorPmac motor in Motors)
                        motor.LogicWorking(this);
                    if (GG.IsDitPreAligner)
                    {
                        foreach (StepMotorEzi stempmotor in StepMotors)
                        {
                            stempmotor.LogicWorking();
                        }
                        PreAlignerSeq.LogicWorking(this);
                    }

                    InspPc.LogicWorking(this);
                    HsmsPc.LogicWorking(this);
                    Efem.LogicWorking(this);
                    WaferTransLogic.LogicWorking(this);
                    InitialLogic.LogicWorking(this);

                    PioA2ISend.LogicWorking(this);
                    PioI2ARecv.LogicWorking(this);
                    StHoming.LogicWorking(this);
                    StLoading.LogicWorking(this);
                    StTTTM.LogicWorking(this);
                    StUnloading.LogicWorking(this);
                    StScanning.LogicWorking(this);
                    StReviewing.LogicWorking(this);
                    StMain.LogicWorking(this);

                    PioLPM1.LogicWorking(this);
                    PioLPM2.LogicWorking(this);

                    ADC.LogicWorking(this);

                    //jys:: 190715 현재는 설치 계획 없음 TowerLamp.LogicWorking(this);
                    WorkingLight.LogicWorking();

                    if (PreAligner != null)
                        PreAligner.LogicWorking();
                }
                WriteToPLC();
            }
            catch (Exception ex)
            {
                if (AlarmMgr.Instance.IsHappened(this, EM_AL_LST.AL_0947_EXCEPTION_OCCURE) == false)
                {
                    Logger.ExceptionLog.AppendLine(LogLevel.Error, ex.ToString());
                    Logger.ExceptionLog.AppendLine(EquipStatusDump.CallStackLog());
                    AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0947_EXCEPTION_OCCURE);
                }
            }
        }

        public void LoadTypeChangeReport()
        {
            if (GG.CimTestMode == true || CimMode == EmCimMode.OffLine || CimMode == EmCimMode.Local)
                return;

            HsmsChangeOHTModeInfo info = new HsmsChangeOHTModeInfo();
            info.Mode = Efem.LoadPort1.LoadType;
            info.LPM1IsExist = Efem.LoadPort1.Status.IsFoupExist;
            info.LPM2IsExist = Efem.LoadPort2.Status.IsFoupExist;

            if (HsmsPc.StartCommand(this, EmHsmsPcCommand.OHT_MODE_CHANGE, info) == false) return;
        }

        PlcTimerEx _statusReportDelay = new PlcTimerEx("Reposrt Delay", false);
        private void EquipStatusReport()
        {
            if (IsHeavyAlarm == false && EquipRunMode == EmEquipRunMode.Auto)
            {
                if (Efem.LoadPort1.IsStandByRunState && Efem.LoadPort2.IsStandByRunState)
                {
                    CimReportStatus = EmCimReportStatus.Idle; // 사양상 StandBy가 맞으나 설비가동률이 높게나와 강창호기정 요청으로 변경함.
                }
                else
                {
                    CimReportStatus = EmCimReportStatus.Run;
                }
                CimReportAutoManual = EmHsmsEqpMode.AUTO;
            }
            else if (IsHeavyAlarm == true)
            {
                if (EquipRunMode == EmEquipRunMode.Manual)
                {
                    CimReportAutoManual = EmHsmsEqpMode.MANUAL;
                }
                CimReportStatus = EmCimReportStatus.Down;
            }
            else if (EquipRunMode == EmEquipRunMode.Manual || this.IsPause || this.IsHomePositioning
                || Efem.RunMode == EmEfemRunMode.Home)
            {
                CimReportStatus = EmCimReportStatus.Idle;
                CimReportAutoManual = EmHsmsEqpMode.MANUAL;
            }

            if (this.PioWorkingAlarm)
            {
                if(GG.Equip.PioLPM1.IsRunning == false && GG.Equip.PioLPM2.IsRunning == false)
                {
                    Logger.Log.AppendLine(LogLevel.Error, "============== Pio중 동작 끝나고 알람발생 처리(Manual전환) ==============");

                    GG.Equip.IsPause = true;
                    GG.Equip.IsHeavyAlarm = true;
                    GG.Equip.Efem.ChangeMode(Step.EmEfemRunMode.Pause);
                    GG.Equip.ChangeRunMode(EmEquipRunMode.Manual);

                    GG.Equip.CimReportStatus = EmCimReportStatus.Down;

                    this.PioWorkingAlarm = false;
                    this.IsPauseUnldPioRunning = false;
                    this.IsPauseLdPioRunning = false;

                    if (GG.Equip.UnldPioWorkingAlarm)
                    {
                        HsmsPortInfo info = new HsmsPortInfo();
                        info.CstID = "";
                        info.IsCstExist = false;
                        info.LoadportNo = GG.Equip.LoadPort;
                        info.PortMode = PortMode.UNLOAD_COMPLETE;

                        if (HsmsPc.StartCommand(this, EmHsmsPcCommand.PORT_MODE_CHANGE, info) == false) return;

                        if(GG.Equip.LoadPort == 1)
                        {
                            GG.Equip.Efem.LoadPort1.ProcessCstOut();

                            Logger.Log.AppendLine(LogLevel.Error, "============== 배출(UnLoad) Pio중 알람 발생하여 Load Port1 임의 배출완료 처리 ==============");

                        }
                        else
                        {
                            GG.Equip.Efem.LoadPort2.ProcessCstOut();

                            Logger.Log.AppendLine(LogLevel.Error, "============== 배출(UnLoad) Pio중 알람 발생하여 Load Port2 임의 배출완료 처리 ==============");
                        }

                        GG.Equip.UnldPioWorkingAlarm = false;
                    }

                }
            }
            
            //이전 상태랑 비교
            if (GG.Equip.HsmsPc._alarmReportQ.Count == 0)
            {
                if (_statusReportDelay)
                {
                    _statusReportDelay.Stop();

                    if (HsmsPc.LstCmd[(int)EmHsmsPcCommand.EQP_STATUS_CHANGE].Step == 0
                            && HsmsPc.LstCmd[(int)EmHsmsPcCommand.ALARM_REPORT].Step == 0)
                    {

                        if (CimReportStatus != OldCimReportStatus)
                        {
                            OldCimReportStatus = CimReportStatus;

                            CassetteInfo cst = TransferDataMgr.GetCst(Efem.LoadPort1.CstKey);

                            HsmsEqpStatusInfo info = new HsmsEqpStatusInfo();
                            info.CstID = cst != null ? cst.CstID : "";
                            info.CurRecipe = this.LPM1Recipe;
                            info.EqpStatus = (int)CimReportStatus;

                            HsmsPc.StartCommand(this, EmHsmsPcCommand.EQP_STATUS_CHANGE, info);
                            Logger.CIMLog.AppendLine(LogLevel.Info, string.Format("CST ID : {0}, CurRecipe : {1} EqpStatus : {2}", info.CstID, info.CurRecipe, info.EqpStatus.ToString()));
                        }

                        if (CimReportAutoManual != OldCimReportAutoManual)
                        {
                            OldCimReportAutoManual = CimReportAutoManual;

                            EmHsmsEqpMode mode = CimReportAutoManual;

                            HsmsPc.StartCommand(this, EmHsmsPcCommand.EQP_MODE_CHANGE, mode);
                            Logger.CIMLog.AppendLine(LogLevel.Info, string.Format("EqpAutoManual Report : {0}", mode.ToString()));
                        }
                    }
                }
                else if (_statusReportDelay.IsStart == false)
                {

                    _statusReportDelay.Start(0, 50);
                }
            }
        }

        // PLC IO 처리.
        public DateTime LastPmacRaedTime { get { return _pmacReadTime; } }
        private DateTime _pmacReadTime = DateTime.Now;
        private bool _isFirst = true;
        private void ReadFromPLC()
        {
            GG.MEM_DIT.ReadFromPLC(GG.ISPT_READ_MEM, GG.ISPT_READ_MEM.Length);
            GG.MEM_DIT.ReadFromPLC(GG.HSMS_READ_MEM, GG.HSMS_READ_MEM.Length);

            GG.CCLINK.ReadFromPLC(GG.X_Addr, GG.X_Addr.Length);
            GG.CCLINK.ReadFromPLC(GG.Wr_Addr, GG.Wr_Addr.Length);

            if (_isFirst == true)
            {
                GG.CCLINK.ReadFromPLC(GG.Y_Addr, 128);
                LoadLastCimMode();
                _isFirst = false;
            }

            if ((DateTime.Now - _pmacReadTime).TotalMilliseconds > 30)
            {
                GG.PMAC.ReadFromPLC(GG.PMAC_REALTIME_READ_MEM, GG.PMAC_REALTIME_READ_MEM.Length);
                _pmacReadTime = DateTime.Now;
            }
        }

        public DateTime LastPmacWriteTime { get { return _pmacWriteTime; } }
        private DateTime _pmacWriteTime = DateTime.Now;
        private void WriteToPLC()
        {
            GG.MEM_DIT.WriteToPLC(GG.ISPT_WRITE_MEM, GG.ISPT_WRITE_MEM.Length);
            //GG.HSMS.WriteToPLC(GG.CIM_WRITE_MEM, GG.CIM_WRITE_MEM.Length);

            GG.CCLINK.WriteToPLC(GG.Y_Addr, GG.Y_Addr.Length);
            GG.CCLINK.WriteToPLC(GG.Ww_Addr, GG.Ww_Addr.Length);
            if ((DateTime.Now - _pmacWriteTime).TotalMilliseconds > 30)
            {
                GG.PMAC.WriteToPLC(GG.PMAC_REALTIME_WRITE_MEM, GG.PMAC_REALTIME_WRITE_MEM.Length);
                _pmacWriteTime = DateTime.Now;
            }
        }

        public bool IsImmediatStop
        {
            get
            {
                return IsHeavyAlarm || IsInterlock || IsPause || IsEmergency;
            }
        }

        public bool IsHeavyAlarm { get; set; }
        public bool IsLightAlarm { get; set; }
        public bool IsTotalAlarm { get; set; }
        public bool IsPM { get; set; }
        public int LongRunCount { get; set; }
        private bool _isPause = false;

        public EmHsmsEqpMode CimReportAutoManual { get; set; } = EmHsmsEqpMode.MANUAL;
        public EmHsmsEqpMode OldCimReportAutoManual { get; set; } = EmHsmsEqpMode.MANUAL;
        public EmCimReportStatus CimReportStatus { get; set; } = EmCimReportStatus.Idle;
        public EmCimReportStatus OldCimReportStatus { get; set; } = EmCimReportStatus.Idle;

        public bool IsPauseByHost { get; set; }
        public bool IsPause
        {
            get
            {
                return _isPause;
            }
            set
            {
                if (value == true &&
                    (PioSend.IsRunning == true ||
                    PioRecv.IsRunning == true)
                    )
                {
                    InterLockMgr.AddInterLock("인터락<PIO 중>\nPIO 진행 중! 설비 내부를 확인하고 다음 동작을 하세요");
                }

                if (value == false && IsHeavyAlarm == true)
                {
                    InterLockMgr.AddInterLock("인터락<HEAVY ALARM>\n(중알람 발생시 PAUSE 해지가 불가능 합니다.)");
                    return;
                }
                if (value == false && EquipRunMode == EmEquipRunMode.Auto && IsCycleStop == EmCycleStop.Complete)
                    IsCycleStop = EmCycleStop.Request;
                //if (EquipRunMode == EmEquipRunMode.Manual && value == true)
                //{
                //    Logger.Log.AppendLine(LogLevel.Warning, "중알람 발생시 PAUSE 설정 불가함.");
                //    return;
                //}
                _isPause = value;
            }
        }
        public EmCycleStop IsCycleStop { get; set; }

        private EmEquipRunMode _oldEquipMode = EmEquipRunMode.Manual;
        private EmEquipRunMode _equipMode;
        public EmEquipRunMode EquipRunMode
        {
            get { return _equipMode; }
        }
        public bool IsSeparationGlass
        {
            get
            {
                if (IsNoGlassMode)
                    return NoGlassType != EmInspOrder.ORIGIN;
                return IsWaferDetect != EmGlassDetect.ALL;
            }
        }
        private EmGlassDetect _efemNoWaferModeOnly = EmGlassDetect.NOT;
        public EmGlassDetect IsWaferDetect
        {
            get
            {
                if (GG.EfemNoWafer == true)
                {
                    return _efemNoWaferModeOnly;
                }

                if (IsNoGlassMode)
                {
                    if (NoGlassType == EmInspOrder.ORIGIN)
                        return EmGlassDetect.ALL;
                    else
                        return EmGlassDetect.SOME;
                }

                if (WaferDetectSensorLiftpin1.IsOn && WaferDetectSensorLiftpin2.IsOn && WaferDetectSensorStage1.IsOn)
                    return EmGlassDetect.ALL;
                else if (WaferDetectSensorLiftpin1.IsOn == false && WaferDetectSensorLiftpin2.IsOn == false && WaferDetectSensorStage1.IsOn == false)
                    return EmGlassDetect.NOT;
                else
                    return EmGlassDetect.SOME;
            }
            set
            {
                _efemNoWaferModeOnly = value;
            }
        }
        public void SetSelectGlassCrack(bool useSelectGlassCrackMode, bool notifyWarning = true)
        {
            if (useSelectGlassCrackMode == true && this.IsUseSelectInspOrder == false)
            {
                InterLockMgr.AddInterLock("글라스 타입 설정 먼저 해야됩니다.");
                return;
            }

        }
        public void SetGlassType(bool useSelectInspOrder, int selectedCmbIdx)
        {
            this.IsUseSelectInspOrder = useSelectInspOrder;
            if (this.IsUseSelectInspOrder)
            {
                this.InspOrder = (EmInspOrder)selectedCmbIdx;

            }
        }
        public bool IsRobotArmDetect
        {
            get { return RobotArmDefect.IsOn; }
        }
        public bool ResetAllStepNo()
        {
            if (IsPause == false)
            {
                Logger.Log.AppendLine(LogLevel.Warning, "PAUSE 아닌 상태에서  STEP 초기화 시도");
                return false;
            }

            List<bool> result = new List<bool>();
            result.Add(StMain.StepStop(this));
            result.Add(StHoming.StepStop(this));
            result.Add(StLoading.StepStop(this));
            result.Add(StTTTM.StepStop(this));
            result.Add(StScanning.StepStop(this));
            result.Add(StReviewing.StepStop(this));
            result.Add(StUnloading.StepStop(this));
            result.Add(PioA2ISend.StopSendPio(this));
            result.Add(PioI2ARecv.StopRecvPio(this));
            result.Add(InitialLogic.StepStop(this));
            result.Add(Centering.CenteringFowardStop(this));
            result.Add(Centering.CenteringBackwardStop(this));
            result.Add(HsmsPc.ResetComnpleteFlag());
            result.Add(Efem.LoadPort1.OHTpio.StepReset());
            result.Add(Efem.LoadPort2.OHTpio.StepReset());

            return result.TrueForAll(i => i == true);
        }
        public bool IsFrontGlassInspect = false;

        public bool ChangeRunMode(EmEquipRunMode runMode, bool IsHostCommand = false)
        {
            bool result = false;
            if (runMode == EmEquipRunMode.Auto && _equipMode != EmEquipRunMode.Auto)
            {
                if (IsHeavyAlarm == true)
                {
                    InterLockMgr.AddInterLock("인터락<HEAVY ALARM>\n(HEAVY ALARM 상태에서 AUTO 변경을 할 수 없습니다.)");
                    Logger.Log.AppendLine(LogLevel.Info, "HEAVY ALARM 상태에서 AUTO 변경이 안됨");
                    return false;
                }

                if (IsHomePositioning == true)
                {
                    InterLockMgr.AddInterLock("인터락<HOME 진행 중>\n(HOME 진행 중 시작 불가");
                    Logger.Log.AppendLine(LogLevel.Warning, "HOME 진행 중 시작 불가");

                    IsInterlock = true;
                    return false;
                }

                if (GG.TestMode == false && IsUseInterLockOff == false && IsUseDoorInterLockOff == false)
                {
                    if (ModeSelectKey.IsAuto == false)
                    {
                        InterLockMgr.AddInterLock("인터락<TEACH MODE>\n(TEACH 모드에서 AUTO RUN 수행을 할 수 없습니다.)");
                        Logger.Log.AppendLine(LogLevel.Warning, "TEACH 모드에서 AUTO START 불가함");

                        IsInterlock = true;
                        return false;

                    }
                }

                if (GG.TestMode == false
                    && IsDoorOpen == true && IsUseInterLockOff == false && IsUseDoorInterLockOff == false)
                {
                    InterLockMgr.AddInterLock("인터락<DOOR>\n(DOOR Open 상태에서 Auto Run 시작을 할 수 없습니다.)");
                    Logger.Log.AppendLine(LogLevel.Info, "DOOR Open 상태에서 Auto Run 시작 안됨");
                    return false;
                }

                if (IsEmergency == true && GG.TestMode == false)
                {
                    InterLockMgr.AddInterLock("인터락<EMERGENCY>\n(EMERGENCY 상태에서  Auto Run 시작을 할 수 없습니다.)");
                    Logger.Log.AppendLine(LogLevel.Info, "EMERGENCY 상태에서 Auto Run 시작 안됨");
                    return false;
                }

                if (IsHomePositioning == true)
                {
                    InterLockMgr.AddInterLock("인터락<HOME>\n(HOME 중 시작 불가)");
                    Logger.Log.AppendLine(LogLevel.Info, "HOME 중 시작 불가");
                    return false;
                }

                if (GG.EfemNoUse == false && GG.Equip.Efem.Proxy.IsConnected == false)
                {
                    GG.Equip.IsInterlock = true;
                    string msg = "EFEM 통신 이상으로 설비 HOME 불가. Operation Option창에서 설비 홈 진행 후 Auto 진행하거나 개별 Start해야합니다.";
                    InterLockMgr.AddInterLock(msg);
                    return false;
                }

                if (WaferTransLogic.IsRunning())
                {
                    GG.Equip.IsInterlock = true;
                    string msg = "웨이퍼 이송 중입니다. Auto Run 시작을 할 수 없습니다.";
                    InterLockMgr.AddInterLock(msg);
                    return false;
                }

                if (Efem.LoadPort1.LoadType == EmLoadType.OHT || Efem.LoadPort2.LoadType == EmLoadType.OHT)
                {
                    if ((EfemDoor01.IsOnOff == false || EfemDoor02.IsOnOff == false) && GG.TestMode == false)
                    {
                        string msg = "OHT 모드 일 때에는 EFEM Door를 닫고 시작해야 합니다";
                        InterLockMgr.AddInterLock(msg);
                        return false;
                    }
                }

                if (Efem.Robot.Status.IsMoving || Efem.LoadPort1.Status.IsBusy || Efem.LoadPort2.Status.IsBusy ||
                    (StageX.IsMoving && !StageX.IsServoError) || (StageY.IsMoving && !StageY.IsServoError) || (Theta.IsMoving && !Theta.IsServoError))
                {
                    string msg = "설비 이니셜 도중에 오토런 전환을 할 수 없습니다 잠시 후 다시 시도해 주세요";
                    InterLockMgr.AddInterLock(msg);
                    return false;
                }

                if (GG.EfemNoUse == false)
                {
                    if (GG.Equip.RobotArmDefect.IsOn == true)
                    {
                        if (GG.Equip.Efem.ChangeMode(EmEfemRunMode.Home) == false)
                            return false;
                        GG.Equip.Efem.IsReserveStart = true;
                    }
                    else
                    {
                        if (GG.Equip.Efem.ChangeMode(EmEfemRunMode.Start) == false)
                            return false;
                    }
                }
                IsNeedRestart = false;
                IsPause = false;
                _equipMode = EmEquipRunMode.Auto;
                IsReviewSkip = EmReviewSkip.None;
                IsReviewManual = EmReviewManual.None;
                IsSkipWaferLPM = -1;
                IsSkipWaferSlotNo = -1;

                if (GG.TestMode == false)
                {
                    if (IsNoGlassMode == false && (IsWaferDetect == EmGlassDetect.SOME || IsWaferDetect == EmGlassDetect.ALL))
                        PinIonizer.IonizerOn();
                    else
                        PinIonizer.IonizerOff();
                }

                LongRunCount = 0;

                //LoginSys.Logout();
                Logger.Log.AppendLine(LogLevel.Warning, "AUTO 상태 변경 성공.... LPM1 {0}/{1}, LPM2 {2}/{3}",
                    this.Efem.LoadPort1.LoadType, this.Efem.LoadPort1.ProgressWay,
                    this.Efem.LoadPort2.LoadType, this.Efem.LoadPort2.ProgressWay
                    );


                return true;
            }
            else if (runMode == EmEquipRunMode.Manual)
            {
                //설비 상태를 먼저 변경후, SETP을 초기화. 

                //if (_equipMode == EmEquipRunMode.Auto && IsPause == false)
                //{
                //    InterLockMgr.AddInterLock("인터락<AUTO MODE>\n(AUTO 모드 또는 PAUSE 상태일 경우에만, Manual 모드로 변경 할 수 있습니다.)");
                //    Logger.Log.AppendLine(LogLevel.Warning, "AUTO 모드, PAUSE 상태만 Manual 모드로 변경 가능");
                //    return false;
                //}
                if (_equipMode == EmEquipRunMode.Auto && IsPause == false && IsHostCommand == false)
                {
                    //   InterLockMgr.AddInterLock("인터락<AUTO MODE>\n(AUTO 모드 또는 PAUSE 상태일 경우에만, Manual 모드로 변경 할 수 있습니다.)");
                    FormMessageOkCanleBox frmManual = new FormMessageOkCanleBox();
                    frmManual.Message = "AUTO 모드 또는 PAUSE 상태일 경우에만, Manual 모드로 변경 할 수 있습니다. 설비를 정지 하시겠습니까?";
                    frmManual.ShowDialog();
                    if (frmManual.Dlg_Result == true)
                    {
                        //  this.ResetError();
                        //  this.StSafety.

                        this._isPause = true;
                        Logger.Log.AppendLine(LogLevel.Warning, "매뉴얼 전환 확인");
                    }
                    else
                    {
                        Logger.Log.AppendLine(LogLevel.Warning, "매뉴얼 전환 취소");
                        return false;
                    }

                    // return false;
                }
                if (PioSend.IsRunning == true ||
                    PioRecv.IsRunning == true)
                {
                    Efem.Robot.PioSendAVI.Initailize();
                    Efem.Robot.PioRecvAVI.Initailize();
                    PioSend.Initailize();
                    PioRecv.Initailize();
                    InterLockMgr.AddInterLock("인터락<상하류 PIO>\nPIO 중 매뉴얼 전환으로 PIO신호 리셋합니다. 정지 상태 확인 후 다시 매뉴얼 전환 하세요");
                    return false;
                }

                if (EquipRunMode == EmEquipRunMode.Auto)
                {
                    TactTimeMgr.Instance.RemoveCurrentTact();
                    EFEMTactMgr.Instance.RemoveCurrentTact();

                    if (IsCycleStop == EmCycleStop.Complete)
                        IsCycleStop = EmCycleStop.Request;
                }

                result = ResetAllStepNo();

                if (result)
                {
                    PinIonizer.IonizerOff();
                    IsGlassUnloading = false;

                    _equipMode = EmEquipRunMode.Manual;
                    Efem.SetRunMode(EmEfemRunMode.Stop);
                    //short cimState = GG.MEM_DIT.VirGetShort(CIMAW.XW_CTRL_MODE_CHANGE_STATE).GetByte(0);
                    //GG.Equip.SetCimMode((EmCimMode)cimState);

                    if (IsReserveReInsp == EmReserveReInsp.START || IsReserveReInsp == EmReserveReInsp.READY)
                        IsReserveReInsp = EmReserveReInsp.RESERVE;

                    Logger.Log.AppendLine(LogLevel.Warning, "Manual 상태 변경 성공....");
                    return true;
                }
                else
                {
                    Logger.Log.AppendLine(LogLevel.Warning, "Manual 상태 변경 실패....");
                    return false;
                }
            }
            return false;
        }
        public bool IsBuzzer3OnOff { get; set; }

        public void CmdPioReset()
        {
            PioRecv.Initailize();
            PioSend.Initailize();

            PioI2ARecv.StepPioRecv = PIO_R.R000_WAIT;
            PioA2ISend.StepPioSend = PIO_S.S000_WAIT;

            IsPause = true;
        }
        /*
        public void ChangeTowerLampState()
        {
            //TowerLamp R 점등
            if (IsHeavyAlarm == true || (IsPM == true && IsLightAlarm == false
                        && CIMAB.OpcalSetCmd.vBit == false && IsHeavyAlarm == false))
                TowerLampRed.OnOff(this, true);
            else
                TowerLampRed.OnOff(this, false);

            //TowerLamp R 점멸
            if (IsHeavyAlarm == false && (CIMAB.OpcalSetCmd.vBit == true || IsLightAlarm == true))
            {
                if (TowerLampRed.Flicker == false)
                    TowerLampRed.Flicker = true;
            }
            else
                TowerLampRed.Flicker = false;

            //TowerLamp Y 점등
            if ((IsPM == true && IsLightAlarm == false && CIMAB.OpcalSetCmd.vBit == false && IsHeavyAlarm == false) ||
               (IsGlassCheckOk != EmGlassSense.ALL || IsPM == false && IsLightAlarm == false && CIMAB.OpcalSetCmd.vBit == false && IsHeavyAlarm == false) ||
               (_equipMode != EmEquipRunMode.Auto && IsPM == false && IsLightAlarm == false && CIMAB.OpcalSetCmd.vBit == false && IsHeavyAlarm == false) ||
               (IsPause == true && IsPM == false && IsLightAlarm == false && CIMAB.OpcalSetCmd.vBit == false && IsHeavyAlarm == false))
                TowerLampYellow.OnOff(this, true);
            else
                TowerLampYellow.OnOff(this, false);

            //TowerLamp Y G 점멸
            if (CIMAB.OpcalSetCmd.vBit == true && IsHeavyAlarm == false && IsLightAlarm == false)
            {
                if (TowerLampYellow.Flicker == false)
                    TowerLampYellow.Flicker = true;

                if (TowerLampGreen.Flicker == false)
                    TowerLampGreen.Flicker = true;
            }
            else
            {
                TowerLampYellow.Flicker = false;
                TowerLampGreen.Flicker = false;
            }

            //TowerLamp G 점등
            if ((IsLightAlarm == true && IsHeavyAlarm == false) ||
               (IsPM == true && IsLightAlarm == false && CIMAB.OpcalSetCmd.vBit == false && IsHeavyAlarm == false) ||
               (_equipMode == EmEquipRunMode.Auto && IsGlassCheckOk == EmGlassSense.ALL && IsPM == false && IsLightAlarm == false && CIMAB.OpcalSetCmd.vBit == false && IsHeavyAlarm == false) ||
               (IsCycleStop != EmCycleStop.Complete && IsGlassCheckOk != EmGlassSense.ALL && IsPM == false && IsLightAlarm == false && CIMAB.OpcalSetCmd.vBit == false && IsHeavyAlarm == false) ||
               (IsPause == true && IsPM == false && IsLightAlarm == false && CIMAB.OpcalSetCmd.vBit == false && IsHeavyAlarm == false))
                TowerLampGreen.OnOff(this, true);
            else
                TowerLampGreen.OnOff(this, false);

        }*/
        public bool ResetError()
        {
            StSafety.StartReset();
            foreach (ServoMotorPmac m in Motors)
                m.StartReset(this);

            AlarmMgr.Instance.Clear(this);
            InitialLogic.StepStop(this);

            if (EquipRunMode == EmEquipRunMode.Manual)
                IsPause = false;

            return true;
        }
        public bool IsBodyMoving
        {
            get
            {
                return StageX.IsMoving ||
                    StageY.IsMoving ||
                    Theta.IsMoving;
            }
        }

        public bool IsNeedRestart { get; set; }
        public bool IsLongTest { get; set; }
        public EmReserveReInsp IsReserveReInsp { get; set; }
        public EmReserveInsp_Stop IsReserveInsp_STOP { get; set; }
        public EmTrigger CurInspGlass { get; set; }
        public EmInspOrder InspOrder { get; set; }

        bool checkCpError = true;

        private bool _isOldInnerKey = false;
        private PlcTimerEx SerialCheckCycle = new PlcTimerEx("Serial Connect Check", false);
        private bool EquipStatusCheck()
        {
            EFUCtrler.StatusCheck();

            ButtonCheck();

            if (SerialCheckCycle || SerialCheckCycle.IsStart == false)
            {
                SerialCheckCycle.Start(CtrlSetting.SerialCheckCycle);
                SerialConnectCheck();
            }

            if (GG.TestMode == false)
            {
                if (SafeyPlcError.IsOn == true) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0272_SAFETY_PLC_ERROR);
                if (IsSyncSvrRunOn == false) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0728_SYNC_SERVER_RUN_ERROR);

                if (checkCpError)
                    CheckCp();
                if (ModeSelectKey.IsAuto)
                {
                    if (TopDoor01.IsOnOff) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0024_TOP_DOOR_01_OPEN);
                    if (TopDoor02.IsOnOff) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0025_TOP_DOOR_02_OPEN);
                    if (TopDoor03.IsOnOff) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0026_TOP_DOOR_03_OPEN);
                    if (TopDoor04.IsOnOff) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0027_TOP_DOOR_04_OPEN);
                }


                if (this.Efem.LoadPort1.LoadType == EmLoadType.Manual && this.Efem.LoadPort2.LoadType == EmLoadType.Manual)
                {
                    //항상 도어 열수 있는 상태
                    EfemDoorSolOnOff(false);
                }
                else if (this.Efem.LoadPort1.LoadType == EmLoadType.OHT || this.Efem.LoadPort2.LoadType == EmLoadType.OHT)
                {
                    //OHT 모드 일 때에는 메뉴얼 상태에서만 도어 열수 있음
                    if (this.EquipRunMode == EmEquipRunMode.Manual)
                    {
                        EfemDoorSolOnOff(false);
                    }
                    //OHT모드이고 메뉴얼 상태가 아닐 때에는 EFEM도어 상태 상시 체크, 그리고 도어 열리면 안됨
                    else
                    {
                        if (EfemDoor01.IsOnOff == false) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0029_EFEM_DOOR_01_OPEN);
                        if (EfemDoor02.IsOnOff == false) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0030_EFEM_DOOR_02_OPEN);

                        EfemDoorSolOnOff(true);
                    }
                }

            }
            if (GG.TestMode == false)
            {
                if (IsBuzzerAllOff == false)
                {
                    BuzzerOff();
                }
                else
                {
                    BuzzerK1.OnOff(this, false);
                    BuzzerK2.OnOff(this, false);
                    BuzzerK3.OnOff(this, false);
                    BuzzerK4.OnOff(this, false);
                }
            }



            return true;
        }

        private void EfemDoorSolOnOff(bool solOn)
        {
            EfemDoor01.OnOff(this, solOn);
            EfemDoor02.OnOff(this, solOn);
        }

        private void SerialConnectCheck()
        {
            if (GG.TestMode == false)
            {
                if (GG.Equip.Efem.LoadPort1.RFR.IsOpen() == false)
                {
                    GG.Equip.Efem.LoadPort1.RFR.Open();
                }
                if (GG.Equip.Efem.LoadPort2.RFR.IsOpen() == false)
                {
                    GG.Equip.Efem.LoadPort1.RFR.Open();
                }
                if (GG.Equip.BCR1.IsOpen() == false)
                {
                    GG.Equip.BCR1.Open();
                }
                if (GG.Equip.BCR2.IsOpen() == false)
                {
                    GG.Equip.BCR2.Open();
                }
                //if (GG.Equip.OCR.IsConnected == false)
                //{
                //    GG.Equip.OCR.Open(GG.Equip.InitSetting.OcrIP, GG.Equip.InitSetting.OcrPort);
                //}
            }
        }

        private bool _oldTempCtrlResetBtnState = false;
        public bool TempCtrlReseted = false;
        private void ButtonCheck()
        {
            if (this.EmsOutside1.IsOn)
            {
                this.PMac.StartCommand(this, EmPMacmd.IMMEDIATE_STOP, 0);
                this.Motors.ToList().ForEach(m => m.Reset());
                AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0001_EMERGENCY_1_ERROR);
                InterLockMgr.AddInterLock("인터락<EMERGENCY>\n 1 EMS 상태입니다.");
            }
            if (this.EmsOutside2.IsOn)
            {
                this.PMac.StartCommand(this, EmPMacmd.IMMEDIATE_STOP, 0);
                this.Motors.ToList().ForEach(m => m.Reset());
                AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0002_EMERGENCY_2_ERROR);
                InterLockMgr.AddInterLock("인터락<EMERGENCY>\n 2 EMS 상태입니다.");
            }
            if (this.EmsOutside3.IsOn)
            {
                this.PMac.StartCommand(this, EmPMacmd.IMMEDIATE_STOP, 0);
                this.Motors.ToList().ForEach(m => m.Reset());
                AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0003_EMERGENCY_3_ERROR);
                InterLockMgr.AddInterLock("인터락<EMERGENCY>\n 3 EMS 상태입니다.");
            }
            //190905 EFEM EMO 팝업창 추가
            if (GG.EfemNoUse == false && GG.Equip.Efem.Status.IsEMO == true)
            {
                InterLockMgr.AddInterLock("인터락<EFEM EMERGENCY>\n EFEM EMS 상태입니다.");
            }
            if (GG.TestMode == false && GG.EfemNoUse == false && GG.Equip.Efem.Status.IsDoorClose == false && GG.Equip.IsUseDoorInterLockOff == false)
            {
                InterLockMgr.AddInterLock("인터락<EFEM EMERGENCY>\n EFEM DOOR OPEN 상태입니다.");
            }
        }

        private bool[] _oldDoorOpenState = new bool[8];

        private void CheckCp()
        {
            if (SERVO_MC_POWER_ON_1.IsOn == true)
            {
                if (CP_BOX_DOOR_OPEN.IsOn == false)
                {
                    AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0277_CP_BOX_DOOR_OPEN);
                }
                if (PC_RACK_FAN_OFF_1.IsOn == false) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0291_PC_RACK_FAN_OFF_1);
                if (PC_RACK_FAN_OFF_2.IsOn == false) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0292_PC_RACK_FAN_OFF_2);
                if (PC_RACK_FAN_OFF_3.IsOn == false) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0293_PC_RACK_FAN_OFF_3);
                if (PC_RACK_FAN_OFF_4.IsOn == false) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0294_PC_RACK_FAN_OFF_4);
                if (PC_RACK_FAN_OFF_5.IsOn == false) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0295_PC_RACK_FAN_OFF_5);
                if (PC_RACK_FAN_OFF_6.IsOn == false) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0296_PC_RACK_FAN_OFF_6);
                if (PC_RACK_FAN_OFF_7.IsOn == false) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0297_PC_RACK_FAN_OFF_7);
                if (PC_RACK_FAN_OFF_8.IsOn == false) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0298_PC_RACK_FAN_OFF_8);

                if (CP_BOX_FAN_OFF_1.IsOn == false) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0273_CP_BOX_FAN_OFF_1);
                if (CP_BOX_FAN_OFF_2.IsOn == false) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0274_CP_BOX_FAN_OFF_2);
                if (CP_BOX_FAN_OFF_3.IsOn == false) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0275_CP_BOX_FAN_OFF_3);
                if (CP_BOX_FAN_OFF_4.IsOn == false) AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0276_CP_BOX_FAN_OFF_4);
            }
            else
                AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0270_SERVO_MC_POWER_OFF_1);
        }
        private string[] _pioNames;
        private bool[] _oldPio;
        private bool _oldRobotArmDetect;
        private PlcAddr[] _pios;
        private bool _needResetOldPio = true;
        private void CheckPio()
        {
            //if (PioSend.IsRunning == true ||PioRecv.IsRunning == true)
            //{
            //    if (_needResetOldPio)
            //    {
            //        _needResetOldPio = false;
            //        _pios = new PlcAddr[] {
            //            AOIB.LOSendAble,
            //            AOIB.LOSendStart,
            //            AOIB.LOSendComplete,
            //            AOIB.LOExchangeFlag,                                    
            //            CIMAB.UPReceiveAble, 
            //            CIMAB.UPReceiveStart, 
            //            CIMAB.UPReceiveComplete,
            //            CIMAB.UPExchangeFailFlag,
            //            AOIB.UPReceiveAble,
            //            AOIB.UPReceiveStart,
            //            AOIB.UPReceiveComplete,
            //            AOIB.UPExchangeFlag,
            //            CIMAB.LOSendAble,
            //            CIMAB.LOSendStart,
            //            CIMAB.LOSendComplete,
            //            CIMAB.LOExchangeFlag
            //        };
            //        _oldPio = new bool[] {
            //            AOIB.LOSendAble.vBit,
            //            AOIB.LOSendStart.vBit,
            //            AOIB.LOSendComplete.vBit,
            //            AOIB.LOExchangeFlag.vBit,       
            //            CIMAB.UPReceiveAble.vBit, 
            //            CIMAB.UPReceiveStart.vBit, 
            //            CIMAB.UPReceiveComplete.vBit,
            //            CIMAB.UPExchangeFailFlag.vBit,
            //            AOIB.UPReceiveAble.vBit,
            //            AOIB.UPReceiveStart.vBit,
            //            AOIB.UPReceiveComplete.vBit,
            //            AOIB.UPExchangeFlag.vBit,
            //            CIMAB.LOSendAble.vBit,
            //            CIMAB.LOSendStart.vBit,
            //            CIMAB.LOSendComplete.vBit,
            //            CIMAB.LOExchangeFlag.vBit
            //        };
            //        _pioNames = new string[] {
            //            "AOIB.LOSendAble.vBit",
            //            "AOIB.LOSendStart.vBit",
            //            "AOIB.LOSendComplete.vBit",
            //            "AOIB.LOExchangeFlag.vBit",       
            //            "CIMAB.UPReceiveAble.vBit", 
            //            "CIMAB.UPReceiveStart.vBit", 
            //            "CIMAB.UPReceiveComplete.vBit",
            //            "CIMAB.UPExchangeFailFlag.vBit",
            //            "AOIB.UPReceiveAble.vBit",
            //            "AOIB.UPReceiveStart.vBit",
            //            "AOIB.UPReceiveComplete.vBit",
            //            "AOIB.UPExchangeFlag.vBit",
            //            "CIMAB.LOSendAble.vBit",
            //            "CIMAB.LOSendStart.vBit",
            //            "CIMAB.LOSendComplete.vBit",
            //            "CIMAB.LOExchangeFlag.vBit"
            //        };
            //        _oldRobotArmDetect = this.IsRobotArmDetect;
            //    }

            //    for (int iter = 0; iter < _oldPio.Length; ++iter)
            //    {
            //        if (_pios[iter].vBit != _oldPio[iter])
            //        {
            //            Logger.Log.AppendLine(LogLevel.AllLog, "[{0,-20}] = {1} : {2} -> {3}", "PIO Changed", _pioNames[iter], _oldPio[iter], _pios[iter].vBit);
            //            _oldPio[iter] = _pios[iter].vBit;
            //        }
            //    }
            //    if (this.IsRobotArmDetect != _oldRobotArmDetect)
            //    {
            //        Logger.Log.AppendLine(LogLevel.AllLog, "[{0,-20}] = {1} -> {2}", "PIO RobotArm Changed", _oldRobotArmDetect, IsRobotArmDetect);
            //        _oldRobotArmDetect = IsRobotArmDetect;
            //    }
            //}
        }

        public void ChangeEquipState(EMEquipState eMEquipState)
        {
            if (this.EquipRunMode == EmEquipRunMode.Manual)
            {
                if (eMEquipState == EMEquipState.PM)
                    IsPM = true;
                else
                    IsPM = false;

                Logger.Log.AppendLine(LogLevel.Warning, "오류");
                return;
            }
        }

        public EmReviewSkip IsReviewSkip { get; set; }
        private EmReviewManual _reviewManual = EmReviewManual.None;

        public EmReviewManual IsReviewManual
        {
            set
            {
                if (value == EmReviewManual.InterLock)
                {
                    if (IsPause)
                    {
                        InterLockMgr.AddInterLock("인터락<PAUSE>\n PAUSE 상태입니다. 리뷰 수동동작이 불가능합니다.");
                        _reviewManual = EmReviewManual.None;
                        return;
                    }
                    _reviewManual = value;
                    return;
                }

                //if (value == EmReviewManual.Request)
                //{
                //    if (IsReviewSkip == EmReviewSkip.SkipOn)
                //    {
                //        InterLockMgr.AddInterLock("인터락<REVIEW SKIP>\n리뷰 스킵 모드일 경우, 수동 리뷰를 선택 할 수 없습니다.");
                //        _reviewManual = EmReviewManual.None;
                //        return;
                //    }

                //    _reviewManual = value;
                //    return;

                //}

                _reviewManual = value;

            }
            get
            {
                if (_reviewManual == EmReviewManual.InterLock || _reviewManual == EmReviewManual.ManualOn)
                {

                }

                if (_reviewManual == EmReviewManual.InterLock)
                {
                    if (IsPause)
                    {
                        _reviewManual = EmReviewManual.None;
                        return _reviewManual;
                    }
                }
                return _reviewManual;
            }
        }

        public GlassInfo CreateSampleData()
        {
            GlassInfo glassInfo = new GlassInfo();

            glassInfo.CstID = "TestCstID";
            glassInfo.RFReadCstID = "TestRFReadCstID";
            glassInfo.WaferID = "TestWaferID";
            glassInfo.BarcodeReadWaferID = "TestBarcodeReadID";
            glassInfo.RecipeID = "Default";

            glassInfo.HGlassID = "A2KR1C64ADPB3";
            glassInfo.EGlassID = "A2KR1C64ADPB3";
            glassInfo.LotID = "A2KR1C64ADA2";
            glassInfo.BatchID = "";
            glassInfo.JobID = "";
            glassInfo.PortID = "P01";
            glassInfo.SlotID = "0";

            glassInfo.ProductType = "";
            glassInfo.ProductKind = "";
            glassInfo.ProductID = "";
            glassInfo.RunSpecID = "";
            glassInfo.LayerID = "";
            glassInfo.StepID = "2025";
            glassInfo.PPID = "A2_PPUBRLCIC_KR";
            glassInfo.FlowID = "EVP";
            glassInfo.GlassSize[0] = 7500;
            glassInfo.GlassSize[1] = 8500;
            glassInfo.GlassThickness = 500;
            glassInfo.GlassState = 1;
            glassInfo.GlassOrder = "";
            glassInfo.Comment = "";

            glassInfo.UseCount = "";
            glassInfo.Judgement = "";
            glassInfo.ReasonCode = "";
            glassInfo.InsFlag = "";
            glassInfo.EncFlag = "";
            glassInfo.PrerunFlag = "";
            glassInfo.TurnDir = "";
            glassInfo.FlipState = "";
            glassInfo.WorkState = "";
            glassInfo.MultiUse = "";

            glassInfo.PairGlassID = "";
            glassInfo.PairPPID = "";

            glassInfo.OptionName1 = "";
            glassInfo.OptionValue1 = "";
            glassInfo.OptionName2 = "";
            glassInfo.OptionValue2 = "";
            glassInfo.OptionName3 = "";
            glassInfo.OptionValue3 = "";
            glassInfo.OptionName4 = "";
            glassInfo.OptionValue4 = "";
            glassInfo.OptionName5 = "";
            glassInfo.OptionValue5 = "";


            return glassInfo;
        }
        public bool IsSyncSvrRunOn
        {
            get
            {
                return ProcessAccess.IsRunningProcess("DitSharedMemorySyncSvr");
            }
        }
        public bool IsEnableGripMiddleOn { get; set; }
        public EmGrapSw IsEnableGripSwOn
        {
            get
            {
                return EmGrapSw.NORMAL;

                //if(GG.TestMode)
                //    return EmGrapSw.NORMAL;

                //bool enableGripSwMiddle = EnableGripSwOn1.XB_OnOff.vBit &&
                //                      EnableGripSwOn2.XB_OnOff.vBit &&
                //                      EnableGripSwOn3.XB_OnOff.vBit;

                //if (enableGripSwMiddle == true && IsEnableGripMiddleOn == false)
                //{
                //    Logger.Log.AppendLine(LogLevel.Info, "Enable 그립 스위치 중간 위치 감지.");
                //    IsEnableGripMiddleOn = true;
                //}

                //if (enableGripSwMiddle == true)
                //    return EmGrapSw.MIDDLE_ON;

                //bool enableGripNormal = (EnableGripSwOn1.XB_OnOff.vBit == false) &&
                //                        (EnableGripSwOn2.XB_OnOff.vBit == false) &&
                //                        (EnableGripSwOn3.XB_OnOff.vBit == true);


                ////꽉 잡은 경우
                //bool enableGripEmergency1 = ((EnableGripSwOn1.XB_OnOff.vBit == false) &&
                //                            (EnableGripSwOn2.XB_OnOff.vBit == false) &&
                //                            (EnableGripSwOn3.XB_OnOff.vBit == false));


                ////중간 위치 잡았다 놓은 경우.
                //bool enableGripEmergency2 = (enableGripNormal == true) &&
                //                            (IsEnableGripMiddleOn == true);

                //if (enableGripEmergency1 || enableGripEmergency2)
                //{
                //    IsEnableGripMiddleOn = false;
                //    return EmGrapSw.EMERGENCY_ON;
                //}

                //if (enableGripNormal)
                //    return EmGrapSw.NORMAL;
                //else
                //{
                //    AlarmMgr.Instance.Happen(this, EM_AL_LST.AL_0282_ENABLE_GRIP_IO_STATE_ERROR);
                //    return EmGrapSw.ERROR;
                //}
            }
        }
        public bool IsTopWorkingLightAutoMode { get; set; }
        public bool IsBottomWorkingLightAutoMode { get; set; }
        public bool IsKeyChangeToTeach { get; set; }
        public bool IsBuzzerStopSW { get; set; }
        public bool IsBuzzerAllOff { get; set; }
        public bool IsInnerWorkOn { get; set; }
        public bool IsAutoOnlyAligner { get; set; }
        public void BuzzerOff()
        {
            if (IsBuzzerStopSW == true)
            {
                BuzzerK1.OnOff(this, false);
                BuzzerK2.OnOff(this, false);
                BuzzerK3.OnOff(this, false);
                BuzzerK4.OnOff(this, false);

                IsBuzzerStopSW = false;
            }
        }
        public bool IsEmergencyButtonOn
        {
            get
            {
                return EmsOutside1.IsOn
                    || EmsOutside2.IsOn
                    || EmsOutside3.IsOn
                       ;
            }
        }
        public bool IsInnerEmergencyButtonOn
        {
            get
            {
                return false
                       ;
            }
        }
        public bool IsDoorOpen
        {
            get
            {
                if (IsUseDoorInterLockOff)
                    return false;
                return TopDoor01.IsOnOff ||
                       TopDoor02.IsOnOff ||
                       TopDoor03.IsOnOff ||
                       TopDoor04.IsOnOff
                       ;
            }
        }
        public bool IsDoorSolOn
        {
            get
            {
                if (IsUseDoorInterLockOff)
                    return false;
                bool doorSolOn = TopDoor01.IsSolOnOff ||
                                 TopDoor02.IsSolOnOff ||
                                 TopDoor03.IsSolOnOff ||
                                 TopDoor04.IsSolOnOff
                                 ;
                return doorSolOn;
            }
        }
        public bool IsEmergency
        {
            get
            {
                bool emergency = IsEmergencyButtonOn;

                bool keySwAutoTeach = ModeSelectKey.IsAuto;

                if (IsUseInterLockOff == true || IsCenteringStepping)
                    return emergency;
                else
                    return emergency;//|| IsEnableGripSwOn == EmGrapSw.EMERGENCY_ON;
            }
        }

        public bool IsWorkingLightOn
        {
            get
            {
                if (GG.TestMode == true)
                {
                    return false;
                }
                bool isWorkingLightOn = WorkingLight.PcRack.IsSolOnOff && WorkingLight.Stage.IsSolOnOff;
                return isWorkingLightOn;
            }
        }

        public bool IsReadyToLoading
        {
            get
            {
                return this.StHoming.IsStepComplete()
                        && this.Motors.FirstOrDefault(m => m.IsHomeComplete() == false) == null
                        && this.Centering.IsCenteringBackward(this) == true
                        && ((this.LiftPin.IsBackward == true && this.IsWaferDetect != EmGlassDetect.NOT) ? this.Vacuum.IsVacuumOn == true : true)
                        ;
            }
        }

        public PioSignalRecv PioRecv { get { return StMain.PioRecv; } set { StMain.PioRecv = value; } }
        public PioSignalSend PioSend { get { return StMain.PioSend; } set { StMain.PioSend = value; } }

        public StepBase TransferUnit { get { return StMain; } }

        public bool IsTTTMMode { get; internal set; }
        public int processTTTMCount = 0;

        public int processStatsToolCount = 1;

        public void UpdateInstanceSetting()
        {
            StageX.MOVE_HOME_OVERTIME = CtrlSetting.Motor.StageXHomeOvertime;
            StageY.MOVE_HOME_OVERTIME = CtrlSetting.Motor.InspYHomeOvertime;
            Theta.MOVE_HOME_OVERTIME = CtrlSetting.Motor.ThetaHomeOvertime;

            #region 검사리뷰_딜레이
            InspPc.SettingDelay(CtrlSetting.Insp.SignalTimeout, CtrlSetting.Insp.EventTimeout);
            #endregion

            LiftPin.ForwardOverTime = CtrlSetting.Ctrl.CylinderFWDOvertime;
            LiftPin.BackwardOverTime = CtrlSetting.Ctrl.CylinderBWDOvertime;

            //ADC.Temperature.LightAlarmOffset[0] = AnalogSetting.Panel_Temp_LightAlarm_Offset;
            //ADC.Temperature.LightAlarmOffset[1] = AnalogSetting.Pc_Rack_Temp_LigthAlarm_Offset;
        }

        public bool IsUseMotorInspSever()
        {
            return false
                || this.StLoading.StepNum == EmLD_NO.S005_PAUSE_OFF_WAIT
                || this.StLoading.StepNum == EmLD_NO.S200_ALIGN_COMPLETE_ACK_WAIT
                || this.StScanning.StepNum == EmSC_NO.S010_SCAN_START
                || this.StScanning.StepNum == EmSC_NO.S020_SCAN_START_ACK_WAIT
                || this.StScanning.StepNum == EmSC_NO.S030_SCAN_COMPLETE_WAIT
                || this.StReviewing.StepNum == EmRV_NO.S025_REVIEW_READY
                || this.StReviewing.StepNum == EmRV_NO.S030_REVIEW_START_ACK_WAIT
                || this.StReviewing.StepNum == EmRV_NO.S040_REVIEW_COMPLETE_WAIT
                || this.StReviewing.StepNum == EmRV_NO.S060_CHECK_MANUAL_REVIEW
                || this.StTTTM.StepNum == EmTTTM_NO.S020_TTTM_START_ACK_WAIT
                || this.StTTTM.StepNum == EmTTTM_NO.S030_TTTM_COMPLETE_WAIT
                ;
        }
        public bool WriteEcidFile()
        {
            EcidSetting.SetData(01, "EFU1_CUR_RPM", EFUCtrler.GetEFU()[0].SettingRPM * 10, 100, 1400, "RPM");
            EcidSetting.SetData(02, "EFU2_CUR_RPM", EFUCtrler.GetEFU()[1].SettingRPM * 10, 100, 1400, "RPM");
            EcidSetting.SetData(03, "EFU3_CUR_RPM", EFUCtrler.GetEFU()[2].SettingRPM * 10, 100, 1400, "RPM");
            EcidSetting.SetData(04, "STAGE_X_LD_POS", StageX.Setting.LstServoPosiInfo[StageXServo.LoadingPos].Position, -10, 500, "mm");
            EcidSetting.SetData(05, "STAGE_Y_LD_POS", StageY.Setting.LstServoPosiInfo[StageYServo.LoadingPos].Position, -10, 500, "mm");
            EcidSetting.SetData(06, "STAGE_T_LD_POS", Theta.Setting.LstServoPosiInfo[ThetaServo.LoadingPos].Position, -121, 216, "mm");
            EcidSetting.SetData(07, "STAGE_X_VISION_POS", StageX.Setting.LstServoPosiInfo[StageXServo.AlignPos].Position, -10, 500, "mm");
            EcidSetting.SetData(08, "STAGE_Y_VISION_POS", StageX.Setting.LstServoPosiInfo[StageYServo.AlignPos].Position, -10, 600, "mm");
            EcidSetting.SetData(09, "STAGE_T_VISION_POS", Theta.Setting.LstServoPosiInfo[ThetaServo.AlignPos].Position, -121, 216, "mm");
            EcidSetting.SetData(10, "STAGE_X_INSP_START_POS", StageX.Setting.LstServoPosiInfo[StageXServo.InspReadyPos].Position, -10, 500, "mm");
            EcidSetting.SetData(11, "STAGE_Y_INSP_START_POS", StageY.Setting.LstServoPosiInfo[StageYServo.InspReadyPos].Position, -10, 600, "mm");
            EcidSetting.SetData(12, "STAGE_X_LD_SPD", StageX.Setting.LstServoPosiInfo[StageXServo.LoadingPos].Speed, 0, 400, "mm/sec");
            EcidSetting.SetData(13, "STAGE_Y_LD_SPD", StageY.Setting.LstServoPosiInfo[StageYServo.LoadingPos].Speed, 0, 400, "mm/sec");
            EcidSetting.SetData(14, "STAGE_T_LD_SPD", StageY.Setting.LstServoPosiInfo[ThetaServo.LoadingPos].Speed, 0, 90, "mm/sec");
            EcidSetting.SetData(15, "CP_BOX_TEMP", CtrlSetting.AnalogSet.CpBoxAlarmTemp, 30, 60, "C");
            EcidSetting.SetData(16, "PC_RACK_TEMP", CtrlSetting.AnalogSet.PcRackAlarmTemp, 30, 60, "C");

            return EcidSetting.Save(CtrlSetting.Hsms.ECIDSavePath);
        }
        public Thread tECIDChangeEvent;
        public void StartMotorSaveAndWait()
        {
            IsMotorSettingSaving = true;
            tECIDChangeEvent = new Thread(new ThreadStart(MotorSaveAndWait));
            tECIDChangeEvent.Start();
        }
        public bool IsMotorSettingSaving;


        private void MotorSaveAndWait()
        {
            try
            {
                bool IsEFUChanged = false;
                ServoMotorPmac[] servo = new ServoMotorPmac[3] { StageX, StageY, Theta };
                for (int i = 1; i <= 16; i++)
                {
                    ECIDDataSet data = HostEcidSetting.GetEcid(i);
                    if (data.Name != "")
                    {
                        if (i == 1 || i == 2 || i == 3)
                        {
                            EFUCtrler.SetSpeed((uint)i - 1, (uint)i - 1, (int)data.Default);
                            IsEFUChanged = true;
                        }
                        //LD pos 변경
                        else if (i >= 4 && i <= 6)
                        {
                            PMacServoSetting setting;
                            setting = servo[i - 4].Setting.Clone() as PMacServoSetting;
                            if (i == 4)
                                setting.LstServoPosiInfo[StageXServo.LoadingPos].Position = (float)data.Default;
                            else if (i == 5)
                                setting.LstServoPosiInfo[StageYServo.LoadingPos].Position = (float)data.Default;
                            else if (i == 6)
                                setting.LstServoPosiInfo[ThetaServo.LoadingPos].Position = (float)data.Default;
                            setting.Save();
                        }
                        //Align pos 변경
                        else if (i >= 7 && i <= 9)
                        {
                            PMacServoSetting setting;
                            setting = servo[i - 7].Setting.Clone() as PMacServoSetting;
                            if (i == 7)
                                setting.LstServoPosiInfo[StageXServo.AlignPos].Position = (float)data.Default;
                            else if (i == 8)
                                setting.LstServoPosiInfo[StageYServo.AlignPos].Position = (float)data.Default;
                            else if (i == 9)
                                setting.LstServoPosiInfo[ThetaServo.AlignPos].Position = (float)data.Default;
                            setting.Save();
                        }
                        //Insp Start pos 변경
                        else if (i >= 10 && i <= 11)
                        {
                            PMacServoSetting setting;
                            setting = servo[i - 10].Setting.Clone() as PMacServoSetting;
                            setting.LstServoPosiInfo[StageXServo.InspReadyPos].Position = (float)data.Default;
                            if (i == 10)
                                setting.LstServoPosiInfo[StageXServo.InspReadyPos].Position = (float)data.Default;
                            else if (i == 11)
                                setting.LstServoPosiInfo[StageYServo.InspReadyPos].Position = (float)data.Default;
                            setting.Save();
                        }
                        //LD speed 변경
                        else if (i >= 12 && i <= 14)
                        {
                            PMacServoSetting setting;
                            setting = servo[i - 12].Setting.Clone() as PMacServoSetting;
                            if (i == 12)
                                setting.LstServoPosiInfo[StageXServo.LoadingPos].Speed = (float)data.Default;
                            else if (i == 13)
                                setting.LstServoPosiInfo[StageYServo.LoadingPos].Speed = (float)data.Default;
                            else if (i == 14)
                                setting.LstServoPosiInfo[ThetaServo.LoadingPos].Speed = (float)data.Default;
                            setting.Save();
                        }
                        //temp max 값 변경
                        else if (i == 15 || i == 16)
                        {
                            if (i == 15)
                            {
                                CtrlSetting.AnalogSet.CpBoxAlarmTemp = (int)data.Default;
                            }
                            else
                            {
                                CtrlSetting.AnalogSet.PcRackAlarmTemp = (int)data.Default;
                            }
                        }
                    }
                }
                //EFUCtrler.SetSpeed(id, id, speed);

                //PMacServoSetting[] newParam = new PMacServoSetting[3];
                //newParam[0] = this.ReadHostEcid(this.StageX);
                //newParam[1] = this.ReadHostEcid(this.StageY);
                //newParam[2] = this.ReadHostEcid(this.Theta);
                //foreach (ServoMotorPmac servo in this.Motors)
                //{
                //    servo.Setting = (PMacServoSetting)newParam[Array.IndexOf(this.Motors, servo)].Clone();
                //    servo.Setting.Save();

                //    if (servo.MotorType == EmMotorType.Linear)
                //        servo.YF_LoadRate.vInt = servo.Setting.LoadRate;
                //}
                //this.PMac.StartSaveToPmac(this);
                //PMacServoSetting.AutoBackup();
                //while (this.PMac.IsDoneSaveToUMac(this) == false)
                //{
                //                    System.Threading.Thread.Sleep(500);
                //}

                if (IsEFUChanged == false)
                    this.HsmsPc.StartCommand(this, EmHsmsPcCommand.ECID_CHANGE, EmHsmsAck.OK);
                IsMotorSettingSaving = false;
                Logger.CIMLog.AppendLine(LogLevel.Error, "ECID Change Sucess");
            }
            catch (Exception ex)
            {
                this.HsmsPc.StartCommand(this, EmHsmsPcCommand.ECID_CHANGE, EmHsmsAck.NACK);
                IsMotorSettingSaving = false;

                Logger.CIMLog.AppendLine(LogLevel.Error, "ECID Change Exception Error");
            }

        }
        //public PMacServoSetting ReadHostEcid(ServoMotorPmac servo)
        //{
        //    PMacServoSetting setting;
        //    if (servo == this.StageX)
        //    {
        //        setting = servo.Setting.Clone() as PMacServoSetting;
        //        setting.LstServoPosiInfo[StageXServo.LoadingPos].Speed     = (float)HostEcidSetting.ECID1.Default;
        //        setting.LstServoPosiInfo[StageXServo.LoadingPos].Accel     = (float)HostEcidSetting.ECID2.Default;
        //        setting.LstServoPosiInfo[StageXServo.AlignPos].Speed       = (float)HostEcidSetting.ECID3.Default;
        //        setting.LstServoPosiInfo[StageXServo.AlignPos].Accel       = (float)HostEcidSetting.ECID4.Default;
        //        setting.LstServoPosiInfo[StageXServo.InspReadyPos].Speed   = (float)HostEcidSetting.ECID5.Default;
        //        setting.LstServoPosiInfo[StageXServo.InspReadyPos].Accel   = (float)HostEcidSetting.ECID6.Default;
        //        setting.LstServoPosiInfo[StageXServo.ReviewReadyPos].Speed = (float)HostEcidSetting.ECID7.Default;
        //        setting.LstServoPosiInfo[StageXServo.ReviewReadyPos].Accel = (float)HostEcidSetting.ECID8.Default;
        //        return setting;                                                     
        //    }                                                                       
        //    else if (servo == this.StageY)                                          
        //    {                                                                       
        //        setting = servo.Setting.Clone() as PMacServoSetting;                
        //        setting.LstServoPosiInfo[StageYServo.LoadingPos].Speed     = (float)HostEcidSetting.ECID9.Default;
        //        setting.LstServoPosiInfo[StageYServo.LoadingPos].Accel     = (float)HostEcidSetting.ECID10.Default;
        //        setting.LstServoPosiInfo[StageYServo.AlignPos].Speed       = (float)HostEcidSetting.ECID11.Default;
        //        setting.LstServoPosiInfo[StageYServo.AlignPos].Accel       = (float)HostEcidSetting.ECID12.Default;
        //        setting.LstServoPosiInfo[StageYServo.InspReadyPos].Speed   = (float)HostEcidSetting.ECID13.Default;
        //        setting.LstServoPosiInfo[StageYServo.InspReadyPos].Accel   = (float)HostEcidSetting.ECID14.Default;
        //        setting.LstServoPosiInfo[StageYServo.ReviewReadyPos].Speed = (float)HostEcidSetting.ECID15.Default;
        //        setting.LstServoPosiInfo[StageYServo.ReviewReadyPos].Accel = (float)HostEcidSetting.ECID16.Default;
        //        return setting;
        //    }
        //    return null;
        //}

        public void ReadWaferMap(WaferInfoKey key)
        {
            Thread waferMapLoadThread = new Thread(() => WaferMap(key));
            waferMapLoadThread.Start();
        }

        private void WaferMap(WaferInfoKey key)
        {
            if (WaferMapSetting.Load(CtrlSetting.Hsms.WaferMapPath) == false)
                return;

            WaferInfo info = TransferDataMgr.GetWafer(key);

            //Waferinfo 변경
            info.LotID = WaferMapSetting.LotId;
            info.IDType = EmWaferIDType.WaferID;//WaferMapSetting.IdType;
            info.ProcessJob = WaferMapSetting.ProductCode;
            info.CellColCount = int.Parse(WaferMapSetting.Columns);
            info.CellRowCount = int.Parse(WaferMapSetting.Row);
            info.FloatZone = WaferMapSetting.FlatZone;
            //
            info.Update();
        }
        public bool Pause()
        {
            if (EquipRunMode == EmEquipRunMode.Auto)
            {
                if (IsPause == true)
                {
                    if (GG.Equip.IsNeedRestart == true)
                    {
                        ChangeRunMode(EmEquipRunMode.Manual);
                        Efem.ChangeMode(EmEfemRunMode.Stop);

                        IsPause = false;
                        IsNeedRestart = false;

                        InterLockMgr.AddInterLock("비정상 정지로 재시작이 필요합니다");
                        Logger.CIMLog.AppendLine(LogLevel.Info, "Pause 상태 해제 실패");
                        return false;
                    }
                    else
                    {
                        if (GG.Equip.Efem.ChangeMode(EmEfemRunMode.Start) == true)
                        {
                            GG.Equip.IsPause = false;
                            Logger.CIMLog.AppendLine(LogLevel.Info, "Pause 상태 해제 성공");
                            return true;
                        }
                        Logger.CIMLog.AppendLine(LogLevel.Info, "Pause 상태 해제 실패");
                        return false;
                    }
                }
                else
                {
                    //if (GG.Equip.PioA2ISend.StepPioSend == PIO_S.S020_LO_RECV_ABLE_WAIT ||
                    //    GG.Equip.PioI2ARecv.StepPioRecv == PIO_R.R020_UP_SEND_ABLE_WAIT)
                    //{
                    //    GG.Equip.IsPause = true;
                    //    GG.Equip.CmdPioReset();
                    //    GG.Equip.Efem.ChangeMode(EmEfemRunMode.Pause);
                    //    CheckMgr.AddCheckMsg(true, "PIO Able 대기 중이어서 Able Reset/Manual 전환 됩니다");
                    //}
                    //else
                    {
                        IsPause = true;
                        if (GG.EfemNoUse == false)
                            GG.Equip.Efem.ChangeMode(EmEfemRunMode.Pause);
                        Logger.CIMLog.AppendLine(LogLevel.Info, "Pause 상태 변경 성공");
                        return true;
                    }
                }
            }
            else
            {
                IsPause = IsPause;
                if (GG.Equip.IsPause == true)
                    GG.Equip.Efem.ChangeMode(EmEfemRunMode.Pause);

                Logger.CIMLog.AppendLine(LogLevel.Info, "오토 상태가 아니라 Pause 상태 변경 실패");
                return false;
            }
        }

        public string GetLpmProgress(int portNo)
        {
            EFEMLPMUnit LoadPort = portNo == 1 ? Efem.LoadPort1 : Efem.LoadPort2;

            if (IsHeavyAlarm)
                return "비정상 정지 발생\n알람리셋 필요";

            else if (WaferTransLogic.IsRunning())
                return "웨이퍼 리커버리 진행 중";

            else if (StMain.StepNum == EmMN_NO.WAIT || (LoadPort.SeqStepNum == Detail.EFEM.Step.EmEFEMLPMSeqStep.S000_END && Efem.Robot.HomeStepNum == Detail.EFEM.Step.EmEFEMRobotHomeStep.H000_END))
                return "오토런 시작 대기 중";

            else if (StMain.StepNum == EmMN_NO.HOME_WAIT || (Efem.Aligner.HomeStepNum > EmEFEMAlignerHomeStep.H000_END && Efem.Aligner.HomeStepNum < EmEFEMAlignerHomeStep.H100_HOME_COMPLETE)
                || LoadPort.HomeStepNum > Detail.EFEM.Step.EmEFEMLPMHomeStep.H000_END && LoadPort.HomeStepNum != Detail.EFEM.Step.EmEFEMLPMHomeStep.H100_HOME_COMPLETE
                || (Efem.Robot.HomeStepNum > Detail.EFEM.Step.EmEFEMRobotHomeStep.H005_EFEM_AUTO_CHANGE && Efem.Robot.HomeStepNum < Detail.EFEM.Step.EmEFEMRobotHomeStep.H100_HOME_COMPLETE))
                return "Initial 진행 중";

            else if (!LoadPort.OHTpio.IsRunning
                && (LoadPort.SeqStepNum == Detail.EFEM.Step.EmEFEMLPMSeqStep.S110_WAIT_LOAD_BUTTON_PUSH
                || LoadPort.SeqStepNum == Detail.EFEM.Step.EmEFEMLPMSeqStep.S200_START_OHT_COMMUNICATION
                || LoadPort.SeqStepNum == Detail.EFEM.Step.EmEFEMLPMSeqStep.S210_WAIT_CST_OHT_LOAD))
                return "카세트 투입 대기 중";

            else if (LoadPort.OHTpio.IsRunning)
                return "카세트 투입/배출 중";

            else if (LoadPort.SeqStepNum == Detail.EFEM.Step.EmEFEMLPMSeqStep.S380_OPEN_READY)
                return "카세트 검사 시작 대기 중";

            else if (!LoadPort.OHTpio.IsRunning &&
                (LoadPort.SeqStepNum == EmEFEMLPMSeqStep.S670_WAIT_UNLD_BUTTON_PUSH
                || LoadPort.SeqStepNum == EmEFEMLPMSeqStep.S650_WAIT_UNLD_BUTTON_PUSH
                || LoadPort.SeqStepNum == EmEFEMLPMSeqStep.S690_OHT_ULD_COMMUNICATION_START
                || LoadPort.SeqStepNum == EmEFEMLPMSeqStep.S700_OHT_ULD_COMPLETE_WAIT))
                return "카세트 배출 대기 중";
            else if (Efem.Aligner.SeqStepNum == EmEFEMAlignerSeqStep.S440_WAFER_MAP_DOWNLOAD_WAIT)
                return "웨이퍼 맵 다운로드 대기 중";
            else if (LoadPort.SeqStepNum == EmEFEMLPMSeqStep.S410_CST_LOAD_CONFIRM_WAIT)
                return "카세트 로드 컨펌 대기 중";
            else if (LoadPort.SeqStepNum == EmEFEMLPMSeqStep.S520_LOT_START_CONFIRM_WAIT)
                return "랏 스타트 컨펌 대기 중";

            else
                return "카세트 검사 진행 중";
        }
    }
}


