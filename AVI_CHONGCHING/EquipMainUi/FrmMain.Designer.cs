namespace EquipMainUi
{
    partial class FrmMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnEFEMCycleStop = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.label22 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btnCycleStop = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.btnBuzzerStop = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnAlarmReset = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnReservReInspStart = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnReservInsp_Stop = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnReservReInsp = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.label9 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnReviewSkip = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnReviewManual = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.label10 = new System.Windows.Forms.Label();
            this.btnInnerWork = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnPioReset = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btndGlassUnloading = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnTact = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.ButtonDelay21 = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.pnlEquipDraw = new System.Windows.Forms.Panel();
            this.btnAVIStartStatus = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.panel33 = new System.Windows.Forms.Panel();
            this.dgvMotorState = new System.Windows.Forms.DataGridView();
            this.MOTOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StageX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAVIManualStatus = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnAVIPauseStatus = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnEFEMStart = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnEFEMManual = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnEFEMPause = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.label51 = new System.Windows.Forms.Label();
            this.lstOperationMode = new System.Windows.Forms.ListView();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.ucrlEquipView = new EquipView.UcrlEquipView();
            this.label40 = new System.Windows.Forms.Label();
            this.tmrUiUpdate = new System.Windows.Forms.Timer(this.components);
            this.tmrState = new System.Windows.Forms.Timer(this.components);
            this.pnlAlarm = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.tabControlAlarm = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gbSetUpTest = new System.Windows.Forms.GroupBox();
            this.pnlCimMode = new System.Windows.Forms.Panel();
            this.rdRemote = new System.Windows.Forms.RadioButton();
            this.rdLocal = new System.Windows.Forms.RadioButton();
            this.rdOffLine = new System.Windows.Forms.RadioButton();
            this.label23 = new System.Windows.Forms.Label();
            this.lblRobotWait = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSettingOn = new System.Windows.Forms.Button();
            this.btnUpperVacOff = new System.Windows.Forms.Button();
            this.btnUpperVacOn = new System.Windows.Forms.Button();
            this.btnLongRunOff = new System.Windows.Forms.Button();
            this.btnLowerVacOff = new System.Windows.Forms.Button();
            this.btnPoupLpm2 = new System.Windows.Forms.Button();
            this.btnPoupLpm1 = new System.Windows.Forms.Button();
            this.btnUnPoupLpm1 = new System.Windows.Forms.Button();
            this.btnAVIWaferOn = new System.Windows.Forms.Button();
            this.btnAVIWaferOff = new System.Windows.Forms.Button();
            this.btnAlignerWaferOn = new System.Windows.Forms.Button();
            this.btnAlignerWaferOff = new System.Windows.Forms.Button();
            this.btnLowerVacOn = new System.Windows.Forms.Button();
            this.btnUnPoupLpm2 = new System.Windows.Forms.Button();
            this.lstvAlarmClone = new System.Windows.Forms.ListView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lstvAlarmHistory = new System.Windows.Forms.ListView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.lstEFEMAlarm = new System.Windows.Forms.ListView();
            this.btnDeleteAllInfo = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnMonitor = new Dit.Framework.UI.UserComponent.ButtonLabel();
            this.btnParameter = new Dit.Framework.UI.UserComponent.ButtonLabel();
            this.btnSetupOption = new Dit.Framework.UI.UserComponent.ButtonLabel();
            this.pnlExtend = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.pnlLowerWaferInfo = new System.Windows.Forms.Panel();
            this.pGridWaferInfoLower = new System.Windows.Forms.PropertyGrid();
            this.pnlUpperWaferInfo = new System.Windows.Forms.Panel();
            this.pGridWaferInfoUpper = new System.Windows.Forms.PropertyGrid();
            this.pnlAVIWaferInfo = new System.Windows.Forms.Panel();
            this.pGridWaferInfoAVI = new System.Windows.Forms.PropertyGrid();
            this.pnlAlignWaferInfo = new System.Windows.Forms.Panel();
            this.pGridWaferInfoAligner = new System.Windows.Forms.PropertyGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLowerTransferData_Copy = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnUpperTransferData_Copy = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnAlignerTransferData_Copy = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnEquipmentTransferData_Copy = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.label2 = new System.Windows.Forms.Label();
            this.panel22 = new System.Windows.Forms.Panel();
            this.label48 = new System.Windows.Forms.Label();
            this.pnlLpm1CstInfo = new System.Windows.Forms.Panel();
            this.pGridCstInfoLPM1 = new System.Windows.Forms.PropertyGrid();
            this.pnlLpm2CstInfo = new System.Windows.Forms.Panel();
            this.pGridCstInfoLPM2 = new System.Windows.Forms.PropertyGrid();
            this.btnLPM2TransferData_Copy = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnLPM1TransferData_Copy = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tabCtrlTransferData = new System.Windows.Forms.TabControl();
            this.tabpCassette = new System.Windows.Forms.TabPage();
            this.pGridCstTransferInfo = new System.Windows.Forms.PropertyGrid();
            this.tabpWafer = new System.Windows.Forms.TabPage();
            this.pGridWaferTransferInfo = new System.Windows.Forms.PropertyGrid();
            this.tabpAutoDataLog = new System.Windows.Forms.TabPage();
            this.lstAutoDataLog = new Dit.Framework.UI.UserComponent.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnShift = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnEquipmentTransferData = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnAlignerTransferData = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnLowerRobotTransferData = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnUpperRobotTransferData = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnInfoLog = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnLPM2CassetteTransferData = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnLPM1CassetteTransferData = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.lblGlassInfoTitle = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelMode = new System.Windows.Forms.Label();
            this.lblMaxScanTime = new System.Windows.Forms.Label();
            this.tabCtrl_MainMenu = new System.Windows.Forms.TabControl();
            this.tabp_Progress = new System.Windows.Forms.TabPage();
            this.btnLpm1DeepReviewPass = new System.Windows.Forms.Button();
            this.btnLpm2DeepReviewPass = new System.Windows.Forms.Button();
            this.btnLpm2OHTStepReset = new System.Windows.Forms.Button();
            this.btnLpm2OHTLdComplete = new System.Windows.Forms.Button();
            this.btnLpm2OHTUldComplete = new System.Windows.Forms.Button();
            this.btnLpm1OHTStepReset = new System.Windows.Forms.Button();
            this.btnLpm1OHTLdComplete = new System.Windows.Forms.Button();
            this.btnLpm1OHTUldComplete = new System.Windows.Forms.Button();
            this.lblLpm1Pogress = new System.Windows.Forms.Label();
            this.lblLpm2Pogress = new System.Windows.Forms.Label();
            this.lblLpm2 = new System.Windows.Forms.Label();
            this.lblLpm1 = new System.Windows.Forms.Label();
            this.tabp_Step = new System.Windows.Forms.TabPage();
            this.pnlMainMenus = new System.Windows.Forms.Panel();
            this.pnlMainStep = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.lblPioUpeerStep = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.lblPioLowerStep = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.lblPioLowerStepRecovery = new System.Windows.Forms.Label();
            this.lblPioUpperStepRecovery = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.lblPioLPM1OHTStep = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.lblPioLPM2OHTStep = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label41 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.lblRobotStep = new System.Windows.Forms.Label();
            this.lblAlignerStep = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.lblLpm1Step = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.lblLpm2Step = new System.Windows.Forms.Label();
            this.panel18 = new System.Windows.Forms.Panel();
            this.lblTTTMTimeLimit = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblTTTMStep = new System.Windows.Forms.Label();
            this.lblLongCount = new System.Windows.Forms.Label();
            this.lblInspTimeLimit = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblReviewTimeLimit = new System.Windows.Forms.Label();
            this.lblTTTMCurTime = new System.Windows.Forms.Label();
            this.lblInspCurTime = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lblReviewCurTime = new System.Windows.Forms.Label();
            this.lblScanCount = new System.Windows.Forms.Label();
            this.lblScanNum = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.lblReviewStep = new System.Windows.Forms.Label();
            this.lblMainStep = new System.Windows.Forms.Label();
            this.label130 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.lblHomingStep = new System.Windows.Forms.Label();
            this.Loading = new System.Windows.Forms.Label();
            this.lblUnloadingStep = new System.Windows.Forms.Label();
            this.lblLoadingStep = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.lblScanningStep = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblEFEM = new System.Windows.Forms.Label();
            this.lblSyncSvr = new Dit.Framework.UI.UserComponent.LabelFlicker();
            this.lblPMAC = new System.Windows.Forms.Label();
            this.lblInspector = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.lblEFUStat = new System.Windows.Forms.Label();
            this.lblBCRStat = new System.Windows.Forms.Label();
            this.lblOCRStat = new System.Windows.Forms.Label();
            this.lblRFID2Stat = new System.Windows.Forms.Label();
            this.lblRFID1Stat = new System.Windows.Forms.Label();
            this.labelWatch = new System.Windows.Forms.Label();
            this.lblEquipState = new System.Windows.Forms.Label();
            this.lblProcState = new System.Windows.Forms.Label();
            this.lblCurrScanTime = new System.Windows.Forms.Label();
            this.btnExit = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.lblRvRun = new System.Windows.Forms.Label();
            this.lblOuterMode = new System.Windows.Forms.Label();
            this.lblAvgScanTime = new System.Windows.Forms.Label();
            this.lblReviewManual = new System.Windows.Forms.Label();
            this.lblAutoCondition = new System.Windows.Forms.Label();
            this.tabOperator = new System.Windows.Forms.TabControl();
            this.tabPageOperator = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.lblMotorLongRun = new System.Windows.Forms.Label();
            this.btnLogPath = new System.Windows.Forms.Button();
            this.btnMotorLongRun = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnEfemDoorOpen = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.panel21 = new System.Windows.Forms.Panel();
            this.btnReviewSkipCopy = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnReviewManual_Copy = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.label34 = new System.Windows.Forms.Label();
            this.tabPageEngineer = new System.Windows.Forms.TabPage();
            this.panel14 = new System.Windows.Forms.Panel();
            this.btnDoorOpenClose = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnWorkingLight = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.label11 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.lblIsInspUseMotor = new System.Windows.Forms.Label();
            this.lblEFEMAutoMode = new System.Windows.Forms.Label();
            this.lblInspRun = new System.Windows.Forms.Label();
            this.lblLPM1Recipe = new System.Windows.Forms.Label();
            this.lblLPM2Recipe = new System.Windows.Forms.Label();
            this.panel17 = new System.Windows.Forms.Panel();
            this.label50 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.ucrlLPMOption = new EquipMainUi.UserControls.ucrlLPMOption();
            this.label32 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.rdManual = new System.Windows.Forms.RadioButton();
            this.rdOHT = new System.Windows.Forms.RadioButton();
            this.btnTTTM = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnAutoStart_Copy = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnManualMode_Copy = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnPause_Copy = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.label42 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.lblPGVersion = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblCimStatus = new System.Windows.Forms.Label();
            this.lblLoginID = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnExpanding = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlEquipDraw.SuspendLayout();
            this.panel33.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMotorState)).BeginInit();
            this.pnlAlarm.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tabControlAlarm.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.gbSetUpTest.SuspendLayout();
            this.pnlCimMode.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlExtend.SuspendLayout();
            this.panel9.SuspendLayout();
            this.pnlLowerWaferInfo.SuspendLayout();
            this.pnlUpperWaferInfo.SuspendLayout();
            this.pnlAVIWaferInfo.SuspendLayout();
            this.pnlAlignWaferInfo.SuspendLayout();
            this.panel22.SuspendLayout();
            this.pnlLpm1CstInfo.SuspendLayout();
            this.pnlLpm2CstInfo.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tabCtrlTransferData.SuspendLayout();
            this.tabpCassette.SuspendLayout();
            this.tabpWafer.SuspendLayout();
            this.tabpAutoDataLog.SuspendLayout();
            this.tabCtrl_MainMenu.SuspendLayout();
            this.tabp_Progress.SuspendLayout();
            this.tabp_Step.SuspendLayout();
            this.pnlMainMenus.SuspendLayout();
            this.pnlMainStep.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabOperator.SuspendLayout();
            this.tabPageOperator.SuspendLayout();
            this.panel21.SuspendLayout();
            this.tabPageEngineer.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel13.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEFEMCycleStop
            // 
            this.btnEFEMCycleStop.BackColor = System.Drawing.Color.Transparent;
            this.btnEFEMCycleStop.Delay = 500;
            this.btnEFEMCycleStop.Enabled = false;
            this.btnEFEMCycleStop.Flicker = false;
            this.btnEFEMCycleStop.Font = new System.Drawing.Font("맑은 고딕", 9.75F);
            this.btnEFEMCycleStop.ForeColor = System.Drawing.Color.Black;
            this.btnEFEMCycleStop.IsLeftLampOn = false;
            this.btnEFEMCycleStop.IsRightLampOn = false;
            this.btnEFEMCycleStop.LampAliveTime = 500;
            this.btnEFEMCycleStop.LampSize = 1;
            this.btnEFEMCycleStop.LeftLampColor = System.Drawing.Color.Red;
            this.btnEFEMCycleStop.Location = new System.Drawing.Point(148, 60);
            this.btnEFEMCycleStop.Name = "btnEFEMCycleStop";
            this.btnEFEMCycleStop.OnOff = false;
            this.btnEFEMCycleStop.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnEFEMCycleStop.Size = new System.Drawing.Size(29, 24);
            this.btnEFEMCycleStop.TabIndex = 89;
            this.btnEFEMCycleStop.TabStop = false;
            this.btnEFEMCycleStop.Text = "Cycle Stop";
            this.btnEFEMCycleStop.Text2 = "";
            this.btnEFEMCycleStop.UseVisualStyleBackColor = false;
            this.btnEFEMCycleStop.Visible = false;
            this.btnEFEMCycleStop.VisibleLeftLamp = false;
            this.btnEFEMCycleStop.VisibleRightLamp = false;
            this.btnEFEMCycleStop.DelayClick += new System.EventHandler(this.btnEFEMStart_DelayClick);
            // 
            // label22
            // 
            this.label22.AutoEllipsis = true;
            this.label22.BackColor = System.Drawing.Color.Gainsboro;
            this.label22.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(90, 24);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(87, 16);
            this.label22.TabIndex = 88;
            this.label22.Text = "- EFEM -";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.AutoEllipsis = true;
            this.label17.BackColor = System.Drawing.Color.Gainsboro;
            this.label17.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(3, 24);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(87, 16);
            this.label17.TabIndex = 88;
            this.label17.Text = "- AVI -";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCycleStop
            // 
            this.btnCycleStop.BackColor = System.Drawing.Color.Transparent;
            this.btnCycleStop.Delay = 500;
            this.btnCycleStop.Enabled = false;
            this.btnCycleStop.Flicker = false;
            this.btnCycleStop.Font = new System.Drawing.Font("맑은 고딕", 9.75F);
            this.btnCycleStop.ForeColor = System.Drawing.Color.Black;
            this.btnCycleStop.IsLeftLampOn = false;
            this.btnCycleStop.IsRightLampOn = false;
            this.btnCycleStop.LampAliveTime = 500;
            this.btnCycleStop.LampSize = 1;
            this.btnCycleStop.LeftLampColor = System.Drawing.Color.Red;
            this.btnCycleStop.Location = new System.Drawing.Point(61, 60);
            this.btnCycleStop.Name = "btnCycleStop";
            this.btnCycleStop.OnOff = false;
            this.btnCycleStop.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnCycleStop.Size = new System.Drawing.Size(29, 24);
            this.btnCycleStop.TabIndex = 11;
            this.btnCycleStop.TabStop = false;
            this.btnCycleStop.Text = "Cycle Stop";
            this.btnCycleStop.Text2 = "";
            this.btnCycleStop.UseVisualStyleBackColor = false;
            this.btnCycleStop.Visible = false;
            this.btnCycleStop.VisibleLeftLamp = false;
            this.btnCycleStop.VisibleRightLamp = false;
            // 
            // tbStatus
            // 
            this.tbStatus.Location = new System.Drawing.Point(172, 1041);
            this.tbStatus.Multiline = true;
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.ReadOnly = true;
            this.tbStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbStatus.Size = new System.Drawing.Size(307, 50);
            this.tbStatus.TabIndex = 76;
            this.tbStatus.DoubleClick += new System.EventHandler(this.tbStatus_DoubleClick);
            // 
            // btnBuzzerStop
            // 
            this.btnBuzzerStop.BackColor = System.Drawing.Color.Transparent;
            this.btnBuzzerStop.Delay = 2;
            this.btnBuzzerStop.Flicker = false;
            this.btnBuzzerStop.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnBuzzerStop.ForeColor = System.Drawing.Color.Black;
            this.btnBuzzerStop.IsLeftLampOn = false;
            this.btnBuzzerStop.IsRightLampOn = false;
            this.btnBuzzerStop.LampAliveTime = 500;
            this.btnBuzzerStop.LampSize = 1;
            this.btnBuzzerStop.LeftLampColor = System.Drawing.Color.Red;
            this.btnBuzzerStop.Location = new System.Drawing.Point(415, 94);
            this.btnBuzzerStop.Name = "btnBuzzerStop";
            this.btnBuzzerStop.OnOff = false;
            this.btnBuzzerStop.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnBuzzerStop.Size = new System.Drawing.Size(56, 67);
            this.btnBuzzerStop.TabIndex = 11;
            this.btnBuzzerStop.TabStop = false;
            this.btnBuzzerStop.Text = "Buzzer Stop";
            this.btnBuzzerStop.Text2 = "";
            this.btnBuzzerStop.UseVisualStyleBackColor = false;
            this.btnBuzzerStop.VisibleLeftLamp = false;
            this.btnBuzzerStop.VisibleRightLamp = false;
            this.btnBuzzerStop.Click += new System.EventHandler(this.btnBuzzerStop_Click);
            // 
            // btnAlarmReset
            // 
            this.btnAlarmReset.BackColor = System.Drawing.Color.Transparent;
            this.btnAlarmReset.Delay = 2;
            this.btnAlarmReset.Flicker = false;
            this.btnAlarmReset.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnAlarmReset.ForeColor = System.Drawing.Color.Black;
            this.btnAlarmReset.IsLeftLampOn = false;
            this.btnAlarmReset.IsRightLampOn = false;
            this.btnAlarmReset.LampAliveTime = 500;
            this.btnAlarmReset.LampSize = 1;
            this.btnAlarmReset.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlarmReset.Location = new System.Drawing.Point(364, 94);
            this.btnAlarmReset.Name = "btnAlarmReset";
            this.btnAlarmReset.OnOff = false;
            this.btnAlarmReset.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlarmReset.Size = new System.Drawing.Size(51, 67);
            this.btnAlarmReset.TabIndex = 10;
            this.btnAlarmReset.TabStop = false;
            this.btnAlarmReset.Text = "Alarm Reset";
            this.btnAlarmReset.Text2 = "";
            this.btnAlarmReset.UseVisualStyleBackColor = false;
            this.btnAlarmReset.VisibleLeftLamp = false;
            this.btnAlarmReset.VisibleRightLamp = false;
            this.btnAlarmReset.Click += new System.EventHandler(this.btnAlarmReset_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnReservReInspStart);
            this.panel3.Controls.Add(this.btnReservInsp_Stop);
            this.panel3.Controls.Add(this.btnReservReInsp);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Location = new System.Drawing.Point(8, 13);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(108, 156);
            this.panel3.TabIndex = 13;
            // 
            // btnReservReInspStart
            // 
            this.btnReservReInspStart.BackColor = System.Drawing.Color.Transparent;
            this.btnReservReInspStart.Delay = 2;
            this.btnReservReInspStart.Flicker = false;
            this.btnReservReInspStart.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnReservReInspStart.ForeColor = System.Drawing.Color.Black;
            this.btnReservReInspStart.IsLeftLampOn = false;
            this.btnReservReInspStart.IsRightLampOn = false;
            this.btnReservReInspStart.LampAliveTime = 500;
            this.btnReservReInspStart.LampSize = 1;
            this.btnReservReInspStart.LeftLampColor = System.Drawing.Color.Red;
            this.btnReservReInspStart.Location = new System.Drawing.Point(3, 108);
            this.btnReservReInspStart.Name = "btnReservReInspStart";
            this.btnReservReInspStart.OnOff = false;
            this.btnReservReInspStart.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnReservReInspStart.Size = new System.Drawing.Size(100, 41);
            this.btnReservReInspStart.TabIndex = 11;
            this.btnReservReInspStart.TabStop = false;
            this.btnReservReInspStart.Text = "재검사 시작";
            this.btnReservReInspStart.Text2 = "";
            this.btnReservReInspStart.UseVisualStyleBackColor = false;
            this.btnReservReInspStart.VisibleLeftLamp = false;
            this.btnReservReInspStart.VisibleRightLamp = false;
            this.btnReservReInspStart.Click += new System.EventHandler(this.btnReservReInspStart_Click);
            // 
            // btnReservInsp_Stop
            // 
            this.btnReservInsp_Stop.BackColor = System.Drawing.Color.Transparent;
            this.btnReservInsp_Stop.Delay = 2;
            this.btnReservInsp_Stop.Flicker = false;
            this.btnReservInsp_Stop.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnReservInsp_Stop.ForeColor = System.Drawing.Color.Black;
            this.btnReservInsp_Stop.IsLeftLampOn = false;
            this.btnReservInsp_Stop.IsRightLampOn = false;
            this.btnReservInsp_Stop.LampAliveTime = 500;
            this.btnReservInsp_Stop.LampSize = 1;
            this.btnReservInsp_Stop.LeftLampColor = System.Drawing.Color.Red;
            this.btnReservInsp_Stop.Location = new System.Drawing.Point(3, 67);
            this.btnReservInsp_Stop.Name = "btnReservInsp_Stop";
            this.btnReservInsp_Stop.OnOff = false;
            this.btnReservInsp_Stop.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnReservInsp_Stop.Size = new System.Drawing.Size(100, 41);
            this.btnReservInsp_Stop.TabIndex = 12;
            this.btnReservInsp_Stop.TabStop = false;
            this.btnReservInsp_Stop.Text = "검사정지";
            this.btnReservInsp_Stop.Text2 = "";
            this.btnReservInsp_Stop.UseVisualStyleBackColor = false;
            this.btnReservInsp_Stop.VisibleLeftLamp = false;
            this.btnReservInsp_Stop.VisibleRightLamp = false;
            this.btnReservInsp_Stop.Click += new System.EventHandler(this.btnReservInsp_Stop_Click);
            // 
            // btnReservReInsp
            // 
            this.btnReservReInsp.BackColor = System.Drawing.Color.Transparent;
            this.btnReservReInsp.Delay = 2;
            this.btnReservReInsp.Flicker = false;
            this.btnReservReInsp.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnReservReInsp.ForeColor = System.Drawing.Color.Black;
            this.btnReservReInsp.IsLeftLampOn = false;
            this.btnReservReInsp.IsRightLampOn = false;
            this.btnReservReInsp.LampAliveTime = 500;
            this.btnReservReInsp.LampSize = 1;
            this.btnReservReInsp.LeftLampColor = System.Drawing.Color.Red;
            this.btnReservReInsp.Location = new System.Drawing.Point(3, 26);
            this.btnReservReInsp.Name = "btnReservReInsp";
            this.btnReservReInsp.OnOff = false;
            this.btnReservReInsp.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnReservReInsp.Size = new System.Drawing.Size(100, 41);
            this.btnReservReInsp.TabIndex = 10;
            this.btnReservReInsp.TabStop = false;
            this.btnReservReInsp.Text = "재검사 예약";
            this.btnReservReInsp.Text2 = "";
            this.btnReservReInsp.UseVisualStyleBackColor = false;
            this.btnReservReInsp.VisibleLeftLamp = false;
            this.btnReservReInsp.VisibleRightLamp = false;
            this.btnReservReInsp.Click += new System.EventHandler(this.btnReservReInsp_Click);
            // 
            // label9
            // 
            this.label9.AutoEllipsis = true;
            this.label9.BackColor = System.Drawing.Color.Gainsboro;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 24);
            this.label9.TabIndex = 9;
            this.label9.Text = "■ 재검사";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnReviewSkip);
            this.panel4.Controls.Add(this.btnReviewManual);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Location = new System.Drawing.Point(121, 13);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(108, 156);
            this.panel4.TabIndex = 13;
            // 
            // btnReviewSkip
            // 
            this.btnReviewSkip.BackColor = System.Drawing.Color.Transparent;
            this.btnReviewSkip.Delay = 2;
            this.btnReviewSkip.Flicker = false;
            this.btnReviewSkip.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnReviewSkip.ForeColor = System.Drawing.Color.Black;
            this.btnReviewSkip.IsLeftLampOn = false;
            this.btnReviewSkip.IsRightLampOn = false;
            this.btnReviewSkip.LampAliveTime = 500;
            this.btnReviewSkip.LampSize = 1;
            this.btnReviewSkip.LeftLampColor = System.Drawing.Color.Red;
            this.btnReviewSkip.Location = new System.Drawing.Point(3, 89);
            this.btnReviewSkip.Name = "btnReviewSkip";
            this.btnReviewSkip.OnOff = false;
            this.btnReviewSkip.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnReviewSkip.Size = new System.Drawing.Size(100, 60);
            this.btnReviewSkip.TabIndex = 448;
            this.btnReviewSkip.TabStop = false;
            this.btnReviewSkip.Text = "리뷰 스킵";
            this.btnReviewSkip.Text2 = "";
            this.btnReviewSkip.UseVisualStyleBackColor = false;
            this.btnReviewSkip.VisibleLeftLamp = false;
            this.btnReviewSkip.VisibleRightLamp = false;
            this.btnReviewSkip.Click += new System.EventHandler(this.btnReviewSkip_Click);
            // 
            // btnReviewManual
            // 
            this.btnReviewManual.BackColor = System.Drawing.Color.Transparent;
            this.btnReviewManual.Delay = 2;
            this.btnReviewManual.Flicker = false;
            this.btnReviewManual.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnReviewManual.ForeColor = System.Drawing.Color.Black;
            this.btnReviewManual.IsLeftLampOn = false;
            this.btnReviewManual.IsRightLampOn = false;
            this.btnReviewManual.LampAliveTime = 500;
            this.btnReviewManual.LampSize = 1;
            this.btnReviewManual.LeftLampColor = System.Drawing.Color.Red;
            this.btnReviewManual.Location = new System.Drawing.Point(3, 27);
            this.btnReviewManual.Name = "btnReviewManual";
            this.btnReviewManual.OnOff = false;
            this.btnReviewManual.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnReviewManual.Size = new System.Drawing.Size(100, 60);
            this.btnReviewManual.TabIndex = 447;
            this.btnReviewManual.TabStop = false;
            this.btnReviewManual.Text = "수동 리뷰";
            this.btnReviewManual.Text2 = "";
            this.btnReviewManual.UseVisualStyleBackColor = false;
            this.btnReviewManual.VisibleLeftLamp = false;
            this.btnReviewManual.VisibleRightLamp = false;
            this.btnReviewManual.Click += new System.EventHandler(this.btnReviewManual_Click);
            // 
            // label10
            // 
            this.label10.AutoEllipsis = true;
            this.label10.BackColor = System.Drawing.Color.Gainsboro;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(106, 24);
            this.label10.TabIndex = 9;
            this.label10.Text = "■ Review";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnInnerWork
            // 
            this.btnInnerWork.BackColor = System.Drawing.Color.Transparent;
            this.btnInnerWork.Delay = 100;
            this.btnInnerWork.Flicker = false;
            this.btnInnerWork.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnInnerWork.ForeColor = System.Drawing.Color.Black;
            this.btnInnerWork.IsLeftLampOn = false;
            this.btnInnerWork.IsRightLampOn = false;
            this.btnInnerWork.LampAliveTime = 500;
            this.btnInnerWork.LampSize = 1;
            this.btnInnerWork.LeftLampColor = System.Drawing.Color.Red;
            this.btnInnerWork.Location = new System.Drawing.Point(5, 58);
            this.btnInnerWork.Name = "btnInnerWork";
            this.btnInnerWork.OnOff = false;
            this.btnInnerWork.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnInnerWork.Size = new System.Drawing.Size(100, 25);
            this.btnInnerWork.TabIndex = 85;
            this.btnInnerWork.TabStop = false;
            this.btnInnerWork.Text = "내부작업";
            this.btnInnerWork.Text2 = "";
            this.btnInnerWork.UseVisualStyleBackColor = false;
            this.btnInnerWork.VisibleLeftLamp = false;
            this.btnInnerWork.VisibleRightLamp = false;
            this.btnInnerWork.Click += new System.EventHandler(this.btndInnerWork_Click);
            // 
            // btnPioReset
            // 
            this.btnPioReset.BackColor = System.Drawing.Color.Transparent;
            this.btnPioReset.Delay = 2;
            this.btnPioReset.Flicker = false;
            this.btnPioReset.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnPioReset.ForeColor = System.Drawing.Color.Black;
            this.btnPioReset.IsLeftLampOn = false;
            this.btnPioReset.IsRightLampOn = false;
            this.btnPioReset.LampAliveTime = 500;
            this.btnPioReset.LampSize = 1;
            this.btnPioReset.LeftLampColor = System.Drawing.Color.Red;
            this.btnPioReset.Location = new System.Drawing.Point(3, 129);
            this.btnPioReset.Name = "btnPioReset";
            this.btnPioReset.OnOff = false;
            this.btnPioReset.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnPioReset.Size = new System.Drawing.Size(91, 34);
            this.btnPioReset.TabIndex = 445;
            this.btnPioReset.TabStop = false;
            this.btnPioReset.Text = "PIO Reset";
            this.btnPioReset.Text2 = "";
            this.btnPioReset.UseVisualStyleBackColor = false;
            this.btnPioReset.Visible = false;
            this.btnPioReset.VisibleLeftLamp = false;
            this.btnPioReset.VisibleRightLamp = false;
            this.btnPioReset.Click += new System.EventHandler(this.btnPioReset_Click);
            // 
            // btndGlassUnloading
            // 
            this.btndGlassUnloading.BackColor = System.Drawing.Color.Transparent;
            this.btndGlassUnloading.Delay = 2;
            this.btndGlassUnloading.Flicker = false;
            this.btndGlassUnloading.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btndGlassUnloading.ForeColor = System.Drawing.Color.Black;
            this.btndGlassUnloading.IsLeftLampOn = false;
            this.btndGlassUnloading.IsRightLampOn = false;
            this.btndGlassUnloading.LampAliveTime = 500;
            this.btndGlassUnloading.LampSize = 1;
            this.btndGlassUnloading.LeftLampColor = System.Drawing.Color.Red;
            this.btndGlassUnloading.Location = new System.Drawing.Point(3, 89);
            this.btndGlassUnloading.Name = "btndGlassUnloading";
            this.btndGlassUnloading.OnOff = false;
            this.btndGlassUnloading.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btndGlassUnloading.Size = new System.Drawing.Size(91, 38);
            this.btndGlassUnloading.TabIndex = 84;
            this.btndGlassUnloading.TabStop = false;
            this.btndGlassUnloading.Text = "글라스  배출";
            this.btndGlassUnloading.Text2 = "";
            this.btndGlassUnloading.UseVisualStyleBackColor = false;
            this.btndGlassUnloading.Visible = false;
            this.btndGlassUnloading.VisibleLeftLamp = false;
            this.btndGlassUnloading.VisibleRightLamp = false;
            this.btndGlassUnloading.Click += new System.EventHandler(this.btndGlassUnloading_Click);
            // 
            // btnTact
            // 
            this.btnTact.BackColor = System.Drawing.Color.Transparent;
            this.btnTact.Delay = 100;
            this.btnTact.Flicker = false;
            this.btnTact.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnTact.ForeColor = System.Drawing.Color.Black;
            this.btnTact.IsLeftLampOn = false;
            this.btnTact.IsRightLampOn = false;
            this.btnTact.LampAliveTime = 500;
            this.btnTact.LampSize = 1;
            this.btnTact.LeftLampColor = System.Drawing.Color.Red;
            this.btnTact.Location = new System.Drawing.Point(3, 27);
            this.btnTact.Name = "btnTact";
            this.btnTact.OnOff = false;
            this.btnTact.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnTact.Size = new System.Drawing.Size(98, 121);
            this.btnTact.TabIndex = 83;
            this.btnTact.TabStop = false;
            this.btnTact.Text = "TactTime";
            this.btnTact.Text2 = "";
            this.btnTact.UseVisualStyleBackColor = false;
            this.btnTact.VisibleLeftLamp = false;
            this.btnTact.VisibleRightLamp = false;
            this.btnTact.Click += new System.EventHandler(this.btndTact_Click);
            // 
            // ButtonDelay21
            // 
            this.ButtonDelay21.BackColor = System.Drawing.Color.Transparent;
            this.ButtonDelay21.Delay = 2;
            this.ButtonDelay21.Flicker = false;
            this.ButtonDelay21.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.ButtonDelay21.ForeColor = System.Drawing.Color.Black;
            this.ButtonDelay21.IsLeftLampOn = false;
            this.ButtonDelay21.IsRightLampOn = false;
            this.ButtonDelay21.LampAliveTime = 500;
            this.ButtonDelay21.LampSize = 1;
            this.ButtonDelay21.LeftLampColor = System.Drawing.Color.Red;
            this.ButtonDelay21.Location = new System.Drawing.Point(5, 27);
            this.ButtonDelay21.Name = "ButtonDelay21";
            this.ButtonDelay21.OnOff = false;
            this.ButtonDelay21.RightLampColor = System.Drawing.Color.DarkGreen;
            this.ButtonDelay21.Size = new System.Drawing.Size(100, 25);
            this.ButtonDelay21.TabIndex = 12;
            this.ButtonDelay21.TabStop = false;
            this.ButtonDelay21.Text = "Operator";
            this.ButtonDelay21.Text2 = "";
            this.ButtonDelay21.UseVisualStyleBackColor = false;
            this.ButtonDelay21.VisibleLeftLamp = false;
            this.ButtonDelay21.VisibleRightLamp = false;
            this.ButtonDelay21.Click += new System.EventHandler(this.btnOperator_Click);
            // 
            // pnlEquipDraw
            // 
            this.pnlEquipDraw.BackColor = System.Drawing.SystemColors.Control;
            this.pnlEquipDraw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEquipDraw.Controls.Add(this.btnAVIStartStatus);
            this.pnlEquipDraw.Controls.Add(this.panel33);
            this.pnlEquipDraw.Controls.Add(this.btnAVIManualStatus);
            this.pnlEquipDraw.Controls.Add(this.btnEFEMCycleStop);
            this.pnlEquipDraw.Controls.Add(this.btnAVIPauseStatus);
            this.pnlEquipDraw.Controls.Add(this.btnPioReset);
            this.pnlEquipDraw.Controls.Add(this.label22);
            this.pnlEquipDraw.Controls.Add(this.label17);
            this.pnlEquipDraw.Controls.Add(this.btnEFEMStart);
            this.pnlEquipDraw.Controls.Add(this.btnCycleStop);
            this.pnlEquipDraw.Controls.Add(this.btnEFEMManual);
            this.pnlEquipDraw.Controls.Add(this.btnEFEMPause);
            this.pnlEquipDraw.Controls.Add(this.label51);
            this.pnlEquipDraw.Controls.Add(this.lstOperationMode);
            this.pnlEquipDraw.Controls.Add(this.btndGlassUnloading);
            this.pnlEquipDraw.Controls.Add(this.elementHost1);
            this.pnlEquipDraw.Controls.Add(this.label40);
            this.pnlEquipDraw.Location = new System.Drawing.Point(1, 36);
            this.pnlEquipDraw.Name = "pnlEquipDraw";
            this.pnlEquipDraw.Size = new System.Drawing.Size(802, 622);
            this.pnlEquipDraw.TabIndex = 82;
            // 
            // btnAVIStartStatus
            // 
            this.btnAVIStartStatus.BackColor = System.Drawing.Color.Transparent;
            this.btnAVIStartStatus.BackgroundImage = global::EquipMainUi.Properties.Resources.Operator_Engineer_Auto_Run;
            this.btnAVIStartStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAVIStartStatus.Delay = 500;
            this.btnAVIStartStatus.Enabled = false;
            this.btnAVIStartStatus.Flicker = false;
            this.btnAVIStartStatus.Font = new System.Drawing.Font("맑은 고딕", 9.75F);
            this.btnAVIStartStatus.ForeColor = System.Drawing.Color.Black;
            this.btnAVIStartStatus.IsLeftLampOn = false;
            this.btnAVIStartStatus.IsRightLampOn = false;
            this.btnAVIStartStatus.LampAliveTime = 500;
            this.btnAVIStartStatus.LampSize = 1;
            this.btnAVIStartStatus.LeftLampColor = System.Drawing.Color.Red;
            this.btnAVIStartStatus.Location = new System.Drawing.Point(3, 40);
            this.btnAVIStartStatus.Name = "btnAVIStartStatus";
            this.btnAVIStartStatus.OnOff = false;
            this.btnAVIStartStatus.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAVIStartStatus.Size = new System.Drawing.Size(29, 24);
            this.btnAVIStartStatus.TabIndex = 90;
            this.btnAVIStartStatus.Text = "\r\n\r\nAuto Start";
            this.btnAVIStartStatus.Text2 = "";
            this.btnAVIStartStatus.UseVisualStyleBackColor = false;
            this.btnAVIStartStatus.VisibleLeftLamp = false;
            this.btnAVIStartStatus.VisibleRightLamp = false;
            // 
            // panel33
            // 
            this.panel33.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel33.Controls.Add(this.dgvMotorState);
            this.panel33.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel33.Location = new System.Drawing.Point(-1, 547);
            this.panel33.Name = "panel33";
            this.panel33.Size = new System.Drawing.Size(299, 74);
            this.panel33.TabIndex = 1;
            // 
            // dgvMotorState
            // 
            this.dgvMotorState.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMotorState.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MOTOR,
            this.StageX});
            this.dgvMotorState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMotorState.Location = new System.Drawing.Point(0, 0);
            this.dgvMotorState.Name = "dgvMotorState";
            this.dgvMotorState.ReadOnly = true;
            this.dgvMotorState.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvMotorState.RowHeadersWidth = 60;
            this.dgvMotorState.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvMotorState.RowTemplate.Height = 20;
            this.dgvMotorState.RowTemplate.ReadOnly = true;
            this.dgvMotorState.Size = new System.Drawing.Size(297, 72);
            this.dgvMotorState.TabIndex = 0;
            // 
            // MOTOR
            // 
            this.MOTOR.HeaderText = "MOTOR";
            this.MOTOR.Name = "MOTOR";
            this.MOTOR.ReadOnly = true;
            // 
            // StageX
            // 
            this.StageX.HeaderText = "StageX";
            this.StageX.Name = "StageX";
            this.StageX.ReadOnly = true;
            // 
            // btnAVIManualStatus
            // 
            this.btnAVIManualStatus.BackColor = System.Drawing.Color.Transparent;
            this.btnAVIManualStatus.BackgroundImage = global::EquipMainUi.Properties.Resources.Operator_Manual;
            this.btnAVIManualStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAVIManualStatus.Delay = 500;
            this.btnAVIManualStatus.Enabled = false;
            this.btnAVIManualStatus.Flicker = false;
            this.btnAVIManualStatus.Font = new System.Drawing.Font("맑은 고딕", 9.5F);
            this.btnAVIManualStatus.ForeColor = System.Drawing.Color.Black;
            this.btnAVIManualStatus.IsLeftLampOn = false;
            this.btnAVIManualStatus.IsRightLampOn = false;
            this.btnAVIManualStatus.LampAliveTime = 500;
            this.btnAVIManualStatus.LampSize = 1;
            this.btnAVIManualStatus.LeftLampColor = System.Drawing.Color.Red;
            this.btnAVIManualStatus.Location = new System.Drawing.Point(61, 40);
            this.btnAVIManualStatus.Name = "btnAVIManualStatus";
            this.btnAVIManualStatus.OnOff = false;
            this.btnAVIManualStatus.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAVIManualStatus.Size = new System.Drawing.Size(29, 24);
            this.btnAVIManualStatus.TabIndex = 88;
            this.btnAVIManualStatus.TabStop = false;
            this.btnAVIManualStatus.Text2 = "";
            this.btnAVIManualStatus.UseVisualStyleBackColor = false;
            this.btnAVIManualStatus.VisibleLeftLamp = false;
            this.btnAVIManualStatus.VisibleRightLamp = false;
            // 
            // btnAVIPauseStatus
            // 
            this.btnAVIPauseStatus.BackColor = System.Drawing.Color.Transparent;
            this.btnAVIPauseStatus.BackgroundImage = global::EquipMainUi.Properties.Resources.Operator_Pause;
            this.btnAVIPauseStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAVIPauseStatus.Delay = 500;
            this.btnAVIPauseStatus.Enabled = false;
            this.btnAVIPauseStatus.Flicker = false;
            this.btnAVIPauseStatus.Font = new System.Drawing.Font("맑은 고딕", 9.75F);
            this.btnAVIPauseStatus.ForeColor = System.Drawing.Color.Black;
            this.btnAVIPauseStatus.IsLeftLampOn = false;
            this.btnAVIPauseStatus.IsRightLampOn = false;
            this.btnAVIPauseStatus.LampAliveTime = 500;
            this.btnAVIPauseStatus.LampSize = 1;
            this.btnAVIPauseStatus.LeftLampColor = System.Drawing.Color.Red;
            this.btnAVIPauseStatus.Location = new System.Drawing.Point(32, 40);
            this.btnAVIPauseStatus.Name = "btnAVIPauseStatus";
            this.btnAVIPauseStatus.OnOff = false;
            this.btnAVIPauseStatus.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAVIPauseStatus.Size = new System.Drawing.Size(29, 24);
            this.btnAVIPauseStatus.TabIndex = 89;
            this.btnAVIPauseStatus.TabStop = false;
            this.btnAVIPauseStatus.Text = "\r\n\r\nPause";
            this.btnAVIPauseStatus.Text2 = "";
            this.btnAVIPauseStatus.UseVisualStyleBackColor = false;
            this.btnAVIPauseStatus.VisibleLeftLamp = false;
            this.btnAVIPauseStatus.VisibleRightLamp = false;
            // 
            // btnEFEMStart
            // 
            this.btnEFEMStart.BackColor = System.Drawing.Color.Transparent;
            this.btnEFEMStart.BackgroundImage = global::EquipMainUi.Properties.Resources.Operator_Engineer_Auto_Run;
            this.btnEFEMStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEFEMStart.Delay = 500;
            this.btnEFEMStart.Enabled = false;
            this.btnEFEMStart.Flicker = false;
            this.btnEFEMStart.Font = new System.Drawing.Font("맑은 고딕", 9.75F);
            this.btnEFEMStart.ForeColor = System.Drawing.Color.Black;
            this.btnEFEMStart.IsLeftLampOn = false;
            this.btnEFEMStart.IsRightLampOn = false;
            this.btnEFEMStart.LampAliveTime = 500;
            this.btnEFEMStart.LampSize = 1;
            this.btnEFEMStart.LeftLampColor = System.Drawing.Color.Red;
            this.btnEFEMStart.Location = new System.Drawing.Point(90, 40);
            this.btnEFEMStart.Name = "btnEFEMStart";
            this.btnEFEMStart.OnOff = false;
            this.btnEFEMStart.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnEFEMStart.Size = new System.Drawing.Size(29, 24);
            this.btnEFEMStart.TabIndex = 84;
            this.btnEFEMStart.Text = "\r\n\r\nAuto Start";
            this.btnEFEMStart.Text2 = "";
            this.btnEFEMStart.UseVisualStyleBackColor = false;
            this.btnEFEMStart.VisibleLeftLamp = false;
            this.btnEFEMStart.VisibleRightLamp = false;
            this.btnEFEMStart.DelayClick += new System.EventHandler(this.btnEFEMStart_DelayClick);
            // 
            // btnEFEMManual
            // 
            this.btnEFEMManual.BackColor = System.Drawing.Color.Transparent;
            this.btnEFEMManual.BackgroundImage = global::EquipMainUi.Properties.Resources.Operator_Manual;
            this.btnEFEMManual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEFEMManual.Delay = 500;
            this.btnEFEMManual.Enabled = false;
            this.btnEFEMManual.Flicker = false;
            this.btnEFEMManual.Font = new System.Drawing.Font("맑은 고딕", 9.5F);
            this.btnEFEMManual.ForeColor = System.Drawing.Color.Black;
            this.btnEFEMManual.IsLeftLampOn = false;
            this.btnEFEMManual.IsRightLampOn = false;
            this.btnEFEMManual.LampAliveTime = 500;
            this.btnEFEMManual.LampSize = 1;
            this.btnEFEMManual.LeftLampColor = System.Drawing.Color.Red;
            this.btnEFEMManual.Location = new System.Drawing.Point(148, 40);
            this.btnEFEMManual.Name = "btnEFEMManual";
            this.btnEFEMManual.OnOff = false;
            this.btnEFEMManual.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnEFEMManual.Size = new System.Drawing.Size(29, 24);
            this.btnEFEMManual.TabIndex = 10;
            this.btnEFEMManual.TabStop = false;
            this.btnEFEMManual.Text2 = "";
            this.btnEFEMManual.UseVisualStyleBackColor = false;
            this.btnEFEMManual.VisibleLeftLamp = false;
            this.btnEFEMManual.VisibleRightLamp = false;
            this.btnEFEMManual.DelayClick += new System.EventHandler(this.btnEFEMStart_DelayClick);
            // 
            // btnEFEMPause
            // 
            this.btnEFEMPause.BackColor = System.Drawing.Color.Transparent;
            this.btnEFEMPause.BackgroundImage = global::EquipMainUi.Properties.Resources.Operator_Pause;
            this.btnEFEMPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEFEMPause.Delay = 500;
            this.btnEFEMPause.Enabled = false;
            this.btnEFEMPause.Flicker = false;
            this.btnEFEMPause.Font = new System.Drawing.Font("맑은 고딕", 9.75F);
            this.btnEFEMPause.ForeColor = System.Drawing.Color.Black;
            this.btnEFEMPause.IsLeftLampOn = false;
            this.btnEFEMPause.IsRightLampOn = false;
            this.btnEFEMPause.LampAliveTime = 500;
            this.btnEFEMPause.LampSize = 1;
            this.btnEFEMPause.LeftLampColor = System.Drawing.Color.Red;
            this.btnEFEMPause.Location = new System.Drawing.Point(119, 40);
            this.btnEFEMPause.Name = "btnEFEMPause";
            this.btnEFEMPause.OnOff = false;
            this.btnEFEMPause.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnEFEMPause.Size = new System.Drawing.Size(29, 24);
            this.btnEFEMPause.TabIndex = 10;
            this.btnEFEMPause.TabStop = false;
            this.btnEFEMPause.Text = "\r\n\r\nPause";
            this.btnEFEMPause.Text2 = "";
            this.btnEFEMPause.UseVisualStyleBackColor = false;
            this.btnEFEMPause.VisibleLeftLamp = false;
            this.btnEFEMPause.VisibleRightLamp = false;
            this.btnEFEMPause.DelayClick += new System.EventHandler(this.btnEFEMStart_DelayClick);
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label51.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label51.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label51.Location = new System.Drawing.Point(297, 548);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(43, 71);
            this.label51.TabIndex = 41;
            this.label51.Text = "MODE";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstOperationMode
            // 
            this.lstOperationMode.HideSelection = false;
            this.lstOperationMode.Location = new System.Drawing.Point(340, 547);
            this.lstOperationMode.Name = "lstOperationMode";
            this.lstOperationMode.Size = new System.Drawing.Size(460, 72);
            this.lstOperationMode.TabIndex = 12;
            this.lstOperationMode.UseCompatibleStateImageBehavior = false;
            this.lstOperationMode.View = System.Windows.Forms.View.List;
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 19);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(800, 601);
            this.elementHost1.TabIndex = 11;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.ucrlEquipView;
            // 
            // label40
            // 
            this.label40.AutoEllipsis = true;
            this.label40.BackColor = System.Drawing.Color.Gainsboro;
            this.label40.Dock = System.Windows.Forms.DockStyle.Top;
            this.label40.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label40.ForeColor = System.Drawing.Color.Black;
            this.label40.Location = new System.Drawing.Point(0, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(800, 19);
            this.label40.TabIndex = 10;
            this.label40.Text = "■ 설비 상태";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrUiUpdate
            // 
            this.tmrUiUpdate.Tick += new System.EventHandler(this.tmrUiUpdate_Tick);
            // 
            // tmrState
            // 
            this.tmrState.Enabled = true;
            this.tmrState.Interval = 1000;
            this.tmrState.Tick += new System.EventHandler(this.tmrState_Tick);
            // 
            // pnlAlarm
            // 
            this.pnlAlarm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAlarm.Controls.Add(this.panel8);
            this.pnlAlarm.Controls.Add(this.tabControlAlarm);
            this.pnlAlarm.Location = new System.Drawing.Point(806, 36);
            this.pnlAlarm.Name = "pnlAlarm";
            this.pnlAlarm.Size = new System.Drawing.Size(621, 433);
            this.pnlAlarm.TabIndex = 99;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Gainsboro;
            this.panel8.Controls.Add(this.label14);
            this.panel8.Controls.Add(this.label20);
            this.panel8.Controls.Add(this.label21);
            this.panel8.Controls.Add(this.label24);
            this.panel8.Controls.Add(this.label25);
            this.panel8.Controls.Add(this.label26);
            this.panel8.Location = new System.Drawing.Point(443, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(185, 18);
            this.panel8.TabIndex = 440;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("맑은 고딕", 7F);
            this.label14.Location = new System.Drawing.Point(139, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(39, 12);
            this.label14.TabIndex = 1;
            this.label14.Text = "Unused";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("맑은 고딕", 7F);
            this.label20.Location = new System.Drawing.Point(84, 3);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(27, 12);
            this.label20.TabIndex = 1;
            this.label20.Text = "Light";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("맑은 고딕", 7F);
            this.label21.Location = new System.Drawing.Point(25, 3);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(36, 12);
            this.label21.TabIndex = 1;
            this.label21.Text = "Heavy ";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.Gray;
            this.label24.Location = new System.Drawing.Point(126, 4);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(10, 10);
            this.label24.TabIndex = 0;
            this.label24.Text = " ";
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.DarkOrange;
            this.label25.Location = new System.Drawing.Point(71, 4);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(10, 10);
            this.label25.TabIndex = 0;
            this.label25.Text = " ";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.Red;
            this.label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label26.Location = new System.Drawing.Point(13, 4);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(10, 10);
            this.label26.TabIndex = 0;
            this.label26.Text = " ";
            // 
            // tabControlAlarm
            // 
            this.tabControlAlarm.Controls.Add(this.tabPage3);
            this.tabControlAlarm.Controls.Add(this.tabPage4);
            this.tabControlAlarm.Controls.Add(this.tabPage5);
            this.tabControlAlarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAlarm.Location = new System.Drawing.Point(0, 0);
            this.tabControlAlarm.Margin = new System.Windows.Forms.Padding(0);
            this.tabControlAlarm.Name = "tabControlAlarm";
            this.tabControlAlarm.Padding = new System.Drawing.Point(0, 0);
            this.tabControlAlarm.SelectedIndex = 0;
            this.tabControlAlarm.Size = new System.Drawing.Size(619, 431);
            this.tabControlAlarm.TabIndex = 443;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gbSetUpTest);
            this.tabPage3.Controls.Add(this.lstvAlarmClone);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(611, 403);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "전체 알람";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // gbSetUpTest
            // 
            this.gbSetUpTest.Controls.Add(this.pnlCimMode);
            this.gbSetUpTest.Controls.Add(this.lblRobotWait);
            this.gbSetUpTest.Controls.Add(this.label3);
            this.gbSetUpTest.Controls.Add(this.btnSettingOn);
            this.gbSetUpTest.Controls.Add(this.btnUpperVacOff);
            this.gbSetUpTest.Controls.Add(this.btnUpperVacOn);
            this.gbSetUpTest.Controls.Add(this.btnLongRunOff);
            this.gbSetUpTest.Controls.Add(this.btnLowerVacOff);
            this.gbSetUpTest.Controls.Add(this.btnPoupLpm2);
            this.gbSetUpTest.Controls.Add(this.btnPoupLpm1);
            this.gbSetUpTest.Controls.Add(this.btnUnPoupLpm1);
            this.gbSetUpTest.Controls.Add(this.btnAVIWaferOn);
            this.gbSetUpTest.Controls.Add(this.btnAVIWaferOff);
            this.gbSetUpTest.Controls.Add(this.btnAlignerWaferOn);
            this.gbSetUpTest.Controls.Add(this.btnAlignerWaferOff);
            this.gbSetUpTest.Controls.Add(this.btnLowerVacOn);
            this.gbSetUpTest.Controls.Add(this.btnUnPoupLpm2);
            this.gbSetUpTest.Location = new System.Drawing.Point(17, 192);
            this.gbSetUpTest.Name = "gbSetUpTest";
            this.gbSetUpTest.Size = new System.Drawing.Size(490, 208);
            this.gbSetUpTest.TabIndex = 477;
            this.gbSetUpTest.TabStop = false;
            this.gbSetUpTest.Text = "TEST ONLY";
            this.gbSetUpTest.Visible = false;
            // 
            // pnlCimMode
            // 
            this.pnlCimMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCimMode.Controls.Add(this.rdRemote);
            this.pnlCimMode.Controls.Add(this.rdLocal);
            this.pnlCimMode.Controls.Add(this.rdOffLine);
            this.pnlCimMode.Controls.Add(this.label23);
            this.pnlCimMode.Location = new System.Drawing.Point(275, 22);
            this.pnlCimMode.Name = "pnlCimMode";
            this.pnlCimMode.Size = new System.Drawing.Size(205, 49);
            this.pnlCimMode.TabIndex = 478;
            // 
            // rdRemote
            // 
            this.rdRemote.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdRemote.Location = new System.Drawing.Point(134, 21);
            this.rdRemote.Name = "rdRemote";
            this.rdRemote.Size = new System.Drawing.Size(56, 25);
            this.rdRemote.TabIndex = 451;
            this.rdRemote.TabStop = true;
            this.rdRemote.Text = "Remote";
            this.rdRemote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdRemote.UseVisualStyleBackColor = true;
            this.rdRemote.Click += new System.EventHandler(this.rdCimMode_Click);
            // 
            // rdLocal
            // 
            this.rdLocal.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdLocal.Location = new System.Drawing.Point(71, 21);
            this.rdLocal.Name = "rdLocal";
            this.rdLocal.Size = new System.Drawing.Size(56, 25);
            this.rdLocal.TabIndex = 451;
            this.rdLocal.TabStop = true;
            this.rdLocal.Text = "Local";
            this.rdLocal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdLocal.UseVisualStyleBackColor = true;
            this.rdLocal.Click += new System.EventHandler(this.rdCimMode_Click);
            // 
            // rdOffLine
            // 
            this.rdOffLine.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdOffLine.Location = new System.Drawing.Point(6, 21);
            this.rdOffLine.Name = "rdOffLine";
            this.rdOffLine.Size = new System.Drawing.Size(56, 25);
            this.rdOffLine.TabIndex = 451;
            this.rdOffLine.TabStop = true;
            this.rdOffLine.Text = "OffLine";
            this.rdOffLine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdOffLine.UseVisualStyleBackColor = true;
            this.rdOffLine.Click += new System.EventHandler(this.rdCimMode_Click);
            // 
            // label23
            // 
            this.label23.AutoEllipsis = true;
            this.label23.BackColor = System.Drawing.Color.Gainsboro;
            this.label23.Dock = System.Windows.Forms.DockStyle.Top;
            this.label23.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label23.ForeColor = System.Drawing.Color.Black;
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(203, 18);
            this.label23.TabIndex = 9;
            this.label23.Text = "■ Cim Mode";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRobotWait
            // 
            this.lblRobotWait.AutoSize = true;
            this.lblRobotWait.Location = new System.Drawing.Point(216, 48);
            this.lblRobotWait.Name = "lblRobotWait";
            this.lblRobotWait.Size = new System.Drawing.Size(39, 15);
            this.lblRobotWait.TabIndex = 478;
            this.lblRobotWait.Text = "label3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(138, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 478;
            this.label3.Text = "Robot Wait";
            // 
            // btnSettingOn
            // 
            this.btnSettingOn.Location = new System.Drawing.Point(6, 22);
            this.btnSettingOn.Name = "btnSettingOn";
            this.btnSettingOn.Size = new System.Drawing.Size(126, 56);
            this.btnSettingOn.TabIndex = 447;
            this.btnSettingOn.Text = "Door InterLock Off\r\nEFEM Long Run On\r\nBuzzer Off\r\n";
            this.btnSettingOn.UseVisualStyleBackColor = true;
            this.btnSettingOn.Click += new System.EventHandler(this.btnSettingOn_Click);
            // 
            // btnUpperVacOff
            // 
            this.btnUpperVacOff.Location = new System.Drawing.Point(83, 84);
            this.btnUpperVacOff.Name = "btnUpperVacOff";
            this.btnUpperVacOff.Size = new System.Drawing.Size(75, 23);
            this.btnUpperVacOff.TabIndex = 447;
            this.btnUpperVacOff.Text = "Upper Off";
            this.btnUpperVacOff.UseVisualStyleBackColor = true;
            this.btnUpperVacOff.Click += new System.EventHandler(this.btnUpperVacOff_Click);
            // 
            // btnUpperVacOn
            // 
            this.btnUpperVacOn.Location = new System.Drawing.Point(2, 84);
            this.btnUpperVacOn.Name = "btnUpperVacOn";
            this.btnUpperVacOn.Size = new System.Drawing.Size(75, 23);
            this.btnUpperVacOn.TabIndex = 447;
            this.btnUpperVacOn.Text = "Upper On";
            this.btnUpperVacOn.UseVisualStyleBackColor = true;
            this.btnUpperVacOn.Click += new System.EventHandler(this.btnUpperVacOn_Click);
            // 
            // btnLongRunOff
            // 
            this.btnLongRunOff.Location = new System.Drawing.Point(132, 22);
            this.btnLongRunOff.Name = "btnLongRunOff";
            this.btnLongRunOff.Size = new System.Drawing.Size(123, 23);
            this.btnLongRunOff.TabIndex = 447;
            this.btnLongRunOff.Text = "EFEM Long Run Off";
            this.btnLongRunOff.UseVisualStyleBackColor = true;
            this.btnLongRunOff.Click += new System.EventHandler(this.btnLongRunOff_Click);
            // 
            // btnLowerVacOff
            // 
            this.btnLowerVacOff.Location = new System.Drawing.Point(83, 115);
            this.btnLowerVacOff.Name = "btnLowerVacOff";
            this.btnLowerVacOff.Size = new System.Drawing.Size(75, 23);
            this.btnLowerVacOff.TabIndex = 447;
            this.btnLowerVacOff.Text = "Lower Off";
            this.btnLowerVacOff.UseVisualStyleBackColor = true;
            this.btnLowerVacOff.Click += new System.EventHandler(this.btnLowerVacOff_Click);
            // 
            // btnPoupLpm2
            // 
            this.btnPoupLpm2.Location = new System.Drawing.Point(290, 85);
            this.btnPoupLpm2.Name = "btnPoupLpm2";
            this.btnPoupLpm2.Size = new System.Drawing.Size(95, 23);
            this.btnPoupLpm2.TabIndex = 447;
            this.btnPoupLpm2.Text = "LPM2 POUP";
            this.btnPoupLpm2.UseVisualStyleBackColor = true;
            this.btnPoupLpm2.Click += new System.EventHandler(this.btnUnPoup_Click);
            // 
            // btnPoupLpm1
            // 
            this.btnPoupLpm1.Location = new System.Drawing.Point(290, 114);
            this.btnPoupLpm1.Name = "btnPoupLpm1";
            this.btnPoupLpm1.Size = new System.Drawing.Size(95, 23);
            this.btnPoupLpm1.TabIndex = 447;
            this.btnPoupLpm1.Text = "LPM1 POUP";
            this.btnPoupLpm1.UseVisualStyleBackColor = true;
            this.btnPoupLpm1.Click += new System.EventHandler(this.btnUnPoup_Click);
            // 
            // btnUnPoupLpm1
            // 
            this.btnUnPoupLpm1.Location = new System.Drawing.Point(178, 114);
            this.btnUnPoupLpm1.Name = "btnUnPoupLpm1";
            this.btnUnPoupLpm1.Size = new System.Drawing.Size(106, 23);
            this.btnUnPoupLpm1.TabIndex = 447;
            this.btnUnPoupLpm1.Text = "LPM1 UNPOUP";
            this.btnUnPoupLpm1.UseVisualStyleBackColor = true;
            this.btnUnPoupLpm1.Click += new System.EventHandler(this.btnUnPoup_Click);
            // 
            // btnAVIWaferOn
            // 
            this.btnAVIWaferOn.Location = new System.Drawing.Point(164, 174);
            this.btnAVIWaferOn.Name = "btnAVIWaferOn";
            this.btnAVIWaferOn.Size = new System.Drawing.Size(130, 23);
            this.btnAVIWaferOn.TabIndex = 447;
            this.btnAVIWaferOn.Text = "AVI Wafer Detect On";
            this.btnAVIWaferOn.UseVisualStyleBackColor = true;
            this.btnAVIWaferOn.Visible = false;
            this.btnAVIWaferOn.Click += new System.EventHandler(this.btnAVIWaferOn_Click);
            // 
            // btnAVIWaferOff
            // 
            this.btnAVIWaferOff.Location = new System.Drawing.Point(164, 145);
            this.btnAVIWaferOff.Name = "btnAVIWaferOff";
            this.btnAVIWaferOff.Size = new System.Drawing.Size(130, 23);
            this.btnAVIWaferOff.TabIndex = 447;
            this.btnAVIWaferOff.Text = "AVI Wafer Detect Off";
            this.btnAVIWaferOff.UseVisualStyleBackColor = true;
            this.btnAVIWaferOff.Click += new System.EventHandler(this.btnAVIWaferOff_Click);
            // 
            // btnAlignerWaferOn
            // 
            this.btnAlignerWaferOn.Location = new System.Drawing.Point(3, 174);
            this.btnAlignerWaferOn.Name = "btnAlignerWaferOn";
            this.btnAlignerWaferOn.Size = new System.Drawing.Size(155, 23);
            this.btnAlignerWaferOn.TabIndex = 447;
            this.btnAlignerWaferOn.Text = "Aligner Wafer Detect On";
            this.btnAlignerWaferOn.UseVisualStyleBackColor = true;
            this.btnAlignerWaferOn.Visible = false;
            this.btnAlignerWaferOn.Click += new System.EventHandler(this.btnAlignerWaferOn_Click);
            // 
            // btnAlignerWaferOff
            // 
            this.btnAlignerWaferOff.Location = new System.Drawing.Point(3, 146);
            this.btnAlignerWaferOff.Name = "btnAlignerWaferOff";
            this.btnAlignerWaferOff.Size = new System.Drawing.Size(155, 23);
            this.btnAlignerWaferOff.TabIndex = 447;
            this.btnAlignerWaferOff.Text = "Aligner Wafer Detect Off";
            this.btnAlignerWaferOff.UseVisualStyleBackColor = true;
            this.btnAlignerWaferOff.Click += new System.EventHandler(this.btnAlignerWaferOff_Click);
            // 
            // btnLowerVacOn
            // 
            this.btnLowerVacOn.Location = new System.Drawing.Point(2, 115);
            this.btnLowerVacOn.Name = "btnLowerVacOn";
            this.btnLowerVacOn.Size = new System.Drawing.Size(75, 23);
            this.btnLowerVacOn.TabIndex = 447;
            this.btnLowerVacOn.Text = "Lower On";
            this.btnLowerVacOn.UseVisualStyleBackColor = true;
            this.btnLowerVacOn.Click += new System.EventHandler(this.btnLowerVacOn_Click);
            // 
            // btnUnPoupLpm2
            // 
            this.btnUnPoupLpm2.Location = new System.Drawing.Point(178, 85);
            this.btnUnPoupLpm2.Name = "btnUnPoupLpm2";
            this.btnUnPoupLpm2.Size = new System.Drawing.Size(106, 23);
            this.btnUnPoupLpm2.TabIndex = 447;
            this.btnUnPoupLpm2.Text = "LPM2 UNPOUP";
            this.btnUnPoupLpm2.UseVisualStyleBackColor = true;
            this.btnUnPoupLpm2.Click += new System.EventHandler(this.btnUnPoup_Click);
            // 
            // lstvAlarmClone
            // 
            this.lstvAlarmClone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstvAlarmClone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstvAlarmClone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lstvAlarmClone.HideSelection = false;
            this.lstvAlarmClone.Location = new System.Drawing.Point(0, 0);
            this.lstvAlarmClone.Margin = new System.Windows.Forms.Padding(0);
            this.lstvAlarmClone.Name = "lstvAlarmClone";
            this.lstvAlarmClone.Size = new System.Drawing.Size(611, 403);
            this.lstvAlarmClone.TabIndex = 96;
            this.lstvAlarmClone.UseCompatibleStateImageBehavior = false;
            this.lstvAlarmClone.View = System.Windows.Forms.View.Details;
            this.lstvAlarmClone.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstvAlarmClone_MouseClick);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lstvAlarmHistory);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(611, 405);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "리셋 알람";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lstvAlarmHistory
            // 
            this.lstvAlarmHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstvAlarmHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstvAlarmHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lstvAlarmHistory.HideSelection = false;
            this.lstvAlarmHistory.Location = new System.Drawing.Point(0, 0);
            this.lstvAlarmHistory.Name = "lstvAlarmHistory";
            this.lstvAlarmHistory.Size = new System.Drawing.Size(611, 405);
            this.lstvAlarmHistory.TabIndex = 443;
            this.lstvAlarmHistory.UseCompatibleStateImageBehavior = false;
            this.lstvAlarmHistory.View = System.Windows.Forms.View.Details;
            this.lstvAlarmHistory.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstvAlarmHistory_MouseClick);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.lstEFEMAlarm);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(611, 405);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "EFEM 알람";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // lstEFEMAlarm
            // 
            this.lstEFEMAlarm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstEFEMAlarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstEFEMAlarm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lstEFEMAlarm.HideSelection = false;
            this.lstEFEMAlarm.Location = new System.Drawing.Point(0, 0);
            this.lstEFEMAlarm.Name = "lstEFEMAlarm";
            this.lstEFEMAlarm.Size = new System.Drawing.Size(611, 405);
            this.lstEFEMAlarm.TabIndex = 444;
            this.lstEFEMAlarm.UseCompatibleStateImageBehavior = false;
            this.lstEFEMAlarm.View = System.Windows.Forms.View.Details;
            this.lstEFEMAlarm.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstEFEMAlarm_MouseDoubleClick_1);
            // 
            // btnDeleteAllInfo
            // 
            this.btnDeleteAllInfo.Location = new System.Drawing.Point(304, 6);
            this.btnDeleteAllInfo.Name = "btnDeleteAllInfo";
            this.btnDeleteAllInfo.Size = new System.Drawing.Size(160, 60);
            this.btnDeleteAllInfo.TabIndex = 479;
            this.btnDeleteAllInfo.Text = "카세트, 웨이퍼 DB 초기화";
            this.btnDeleteAllInfo.UseVisualStyleBackColor = true;
            this.btnDeleteAllInfo.Click += new System.EventHandler(this.btnDeleteAllInfo_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnMonitor, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnParameter, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSetupOption, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1430, 34);
            this.tableLayoutPanel1.TabIndex = 77;
            // 
            // btnMonitor
            // 
            this.btnMonitor.BackColor = System.Drawing.Color.Transparent;
            this.btnMonitor.ColorOnMouseLeave = System.Drawing.Color.Transparent;
            this.btnMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMonitor.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.btnMonitor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMonitor.ImageTextPadding = 4;
            this.btnMonitor.Location = new System.Drawing.Point(1, 1);
            this.btnMonitor.Margin = new System.Windows.Forms.Padding(0);
            this.btnMonitor.Name = "btnMonitor";
            this.btnMonitor.Selected = false;
            this.btnMonitor.Size = new System.Drawing.Size(468, 32);
            this.btnMonitor.TabIndex = 1;
            this.btnMonitor.Text = "Monitor";
            this.btnMonitor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMonitor.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.btnMonitor.Click += new System.EventHandler(this.btnlMonitor_Click);
            // 
            // btnParameter
            // 
            this.btnParameter.BackColor = System.Drawing.Color.Transparent;
            this.btnParameter.ColorOnMouseLeave = System.Drawing.Color.Transparent;
            this.btnParameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnParameter.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.btnParameter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnParameter.ImageTextPadding = 4;
            this.btnParameter.Location = new System.Drawing.Point(470, 1);
            this.btnParameter.Margin = new System.Windows.Forms.Padding(0);
            this.btnParameter.Name = "btnParameter";
            this.btnParameter.Selected = false;
            this.btnParameter.Size = new System.Drawing.Size(468, 32);
            this.btnParameter.TabIndex = 1;
            this.btnParameter.Text = "Parameter";
            this.btnParameter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnParameter.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.btnParameter.Click += new System.EventHandler(this.buttonParam_Click);
            // 
            // btnSetupOption
            // 
            this.btnSetupOption.BackColor = System.Drawing.Color.Transparent;
            this.btnSetupOption.ColorOnMouseLeave = System.Drawing.Color.Transparent;
            this.btnSetupOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetupOption.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.btnSetupOption.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSetupOption.ImageTextPadding = 4;
            this.btnSetupOption.Location = new System.Drawing.Point(939, 1);
            this.btnSetupOption.Margin = new System.Windows.Forms.Padding(0);
            this.btnSetupOption.Name = "btnSetupOption";
            this.btnSetupOption.Selected = false;
            this.btnSetupOption.Size = new System.Drawing.Size(468, 32);
            this.btnSetupOption.TabIndex = 1;
            this.btnSetupOption.Text = "Operation Option";
            this.btnSetupOption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSetupOption.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.btnSetupOption.Click += new System.EventHandler(this.btnOperationOption_Click);
            // 
            // pnlExtend
            // 
            this.pnlExtend.Controls.Add(this.panel9);
            this.pnlExtend.Controls.Add(this.label2);
            this.pnlExtend.Controls.Add(this.tableLayoutPanel1);
            this.pnlExtend.Controls.Add(this.panel22);
            this.pnlExtend.Controls.Add(this.panel7);
            this.pnlExtend.Controls.Add(this.pnlEquipDraw);
            this.pnlExtend.Controls.Add(this.pnlAlarm);
            this.pnlExtend.Location = new System.Drawing.Point(488, 3);
            this.pnlExtend.Name = "pnlExtend";
            this.pnlExtend.Size = new System.Drawing.Size(1430, 1059);
            this.pnlExtend.TabIndex = 100;
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.pnlLowerWaferInfo);
            this.panel9.Controls.Add(this.pnlUpperWaferInfo);
            this.panel9.Controls.Add(this.pnlAVIWaferInfo);
            this.panel9.Controls.Add(this.pnlAlignWaferInfo);
            this.panel9.Controls.Add(this.label4);
            this.panel9.Controls.Add(this.btnLowerTransferData_Copy);
            this.panel9.Controls.Add(this.btnUpperTransferData_Copy);
            this.panel9.Controls.Add(this.btnAlignerTransferData_Copy);
            this.panel9.Controls.Add(this.btnEquipmentTransferData_Copy);
            this.panel9.Location = new System.Drawing.Point(806, 471);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(409, 187);
            this.panel9.TabIndex = 101;
            // 
            // pnlLowerWaferInfo
            // 
            this.pnlLowerWaferInfo.AllowDrop = true;
            this.pnlLowerWaferInfo.Controls.Add(this.pGridWaferInfoLower);
            this.pnlLowerWaferInfo.Location = new System.Drawing.Point(231, 105);
            this.pnlLowerWaferInfo.Name = "pnlLowerWaferInfo";
            this.pnlLowerWaferInfo.Size = new System.Drawing.Size(172, 80);
            this.pnlLowerWaferInfo.TabIndex = 89;
            this.pnlLowerWaferInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlWaferInfoDragDrop);
            this.pnlLowerWaferInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlWaferInfoDragEnter);
            this.pnlLowerWaferInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlWaferInfo_MouseDown);
            // 
            // pGridWaferInfoLower
            // 
            this.pGridWaferInfoLower.AllowDrop = true;
            this.pGridWaferInfoLower.Enabled = false;
            this.pGridWaferInfoLower.HelpVisible = false;
            this.pGridWaferInfoLower.Location = new System.Drawing.Point(0, 3);
            this.pGridWaferInfoLower.Name = "pGridWaferInfoLower";
            this.pGridWaferInfoLower.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pGridWaferInfoLower.Size = new System.Drawing.Size(171, 75);
            this.pGridWaferInfoLower.TabIndex = 10;
            this.pGridWaferInfoLower.ToolbarVisible = false;
            // 
            // pnlUpperWaferInfo
            // 
            this.pnlUpperWaferInfo.AllowDrop = true;
            this.pnlUpperWaferInfo.Controls.Add(this.pGridWaferInfoUpper);
            this.pnlUpperWaferInfo.Location = new System.Drawing.Point(231, 24);
            this.pnlUpperWaferInfo.Name = "pnlUpperWaferInfo";
            this.pnlUpperWaferInfo.Size = new System.Drawing.Size(172, 80);
            this.pnlUpperWaferInfo.TabIndex = 89;
            this.pnlUpperWaferInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlWaferInfoDragDrop);
            this.pnlUpperWaferInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlWaferInfoDragEnter);
            this.pnlUpperWaferInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlWaferInfo_MouseDown);
            // 
            // pGridWaferInfoUpper
            // 
            this.pGridWaferInfoUpper.AllowDrop = true;
            this.pGridWaferInfoUpper.Enabled = false;
            this.pGridWaferInfoUpper.HelpVisible = false;
            this.pGridWaferInfoUpper.Location = new System.Drawing.Point(0, 2);
            this.pGridWaferInfoUpper.Name = "pGridWaferInfoUpper";
            this.pGridWaferInfoUpper.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pGridWaferInfoUpper.Size = new System.Drawing.Size(171, 75);
            this.pGridWaferInfoUpper.TabIndex = 10;
            this.pGridWaferInfoUpper.ToolbarVisible = false;
            // 
            // pnlAVIWaferInfo
            // 
            this.pnlAVIWaferInfo.AllowDrop = true;
            this.pnlAVIWaferInfo.Controls.Add(this.pGridWaferInfoAVI);
            this.pnlAVIWaferInfo.Location = new System.Drawing.Point(21, 105);
            this.pnlAVIWaferInfo.Name = "pnlAVIWaferInfo";
            this.pnlAVIWaferInfo.Size = new System.Drawing.Size(172, 80);
            this.pnlAVIWaferInfo.TabIndex = 89;
            this.pnlAVIWaferInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlWaferInfoDragDrop);
            this.pnlAVIWaferInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlWaferInfoDragEnter);
            this.pnlAVIWaferInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlWaferInfo_MouseDown);
            // 
            // pGridWaferInfoAVI
            // 
            this.pGridWaferInfoAVI.AllowDrop = true;
            this.pGridWaferInfoAVI.Enabled = false;
            this.pGridWaferInfoAVI.HelpVisible = false;
            this.pGridWaferInfoAVI.Location = new System.Drawing.Point(0, 3);
            this.pGridWaferInfoAVI.Name = "pGridWaferInfoAVI";
            this.pGridWaferInfoAVI.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pGridWaferInfoAVI.Size = new System.Drawing.Size(171, 75);
            this.pGridWaferInfoAVI.TabIndex = 10;
            this.pGridWaferInfoAVI.ToolbarVisible = false;
            // 
            // pnlAlignWaferInfo
            // 
            this.pnlAlignWaferInfo.AllowDrop = true;
            this.pnlAlignWaferInfo.Controls.Add(this.pGridWaferInfoAligner);
            this.pnlAlignWaferInfo.Location = new System.Drawing.Point(21, 26);
            this.pnlAlignWaferInfo.Name = "pnlAlignWaferInfo";
            this.pnlAlignWaferInfo.Size = new System.Drawing.Size(172, 80);
            this.pnlAlignWaferInfo.TabIndex = 89;
            this.pnlAlignWaferInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlWaferInfoDragDrop);
            this.pnlAlignWaferInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlWaferInfoDragEnter);
            this.pnlAlignWaferInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlWaferInfo_MouseDown);
            // 
            // pGridWaferInfoAligner
            // 
            this.pGridWaferInfoAligner.AllowDrop = true;
            this.pGridWaferInfoAligner.Enabled = false;
            this.pGridWaferInfoAligner.HelpVisible = false;
            this.pGridWaferInfoAligner.Location = new System.Drawing.Point(0, 2);
            this.pGridWaferInfoAligner.Name = "pGridWaferInfoAligner";
            this.pGridWaferInfoAligner.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pGridWaferInfoAligner.Size = new System.Drawing.Size(171, 75);
            this.pGridWaferInfoAligner.TabIndex = 10;
            this.pGridWaferInfoAligner.ToolbarVisible = false;
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.BackColor = System.Drawing.Color.Gainsboro;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(407, 24);
            this.label4.TabIndex = 9;
            this.label4.Text = "■ WAFER INFO";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLowerTransferData_Copy
            // 
            this.btnLowerTransferData_Copy.BackColor = System.Drawing.Color.Transparent;
            this.btnLowerTransferData_Copy.Delay = 0;
            this.btnLowerTransferData_Copy.Flicker = false;
            this.btnLowerTransferData_Copy.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.btnLowerTransferData_Copy.ForeColor = System.Drawing.Color.Black;
            this.btnLowerTransferData_Copy.IsLeftLampOn = false;
            this.btnLowerTransferData_Copy.IsRightLampOn = false;
            this.btnLowerTransferData_Copy.LampAliveTime = 500;
            this.btnLowerTransferData_Copy.LampSize = 1;
            this.btnLowerTransferData_Copy.LeftLampColor = System.Drawing.Color.Red;
            this.btnLowerTransferData_Copy.Location = new System.Drawing.Point(214, 107);
            this.btnLowerTransferData_Copy.Name = "btnLowerTransferData_Copy";
            this.btnLowerTransferData_Copy.OnOff = false;
            this.btnLowerTransferData_Copy.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnLowerTransferData_Copy.Size = new System.Drawing.Size(17, 75);
            this.btnLowerTransferData_Copy.TabIndex = 88;
            this.btnLowerTransferData_Copy.TabStop = false;
            this.btnLowerTransferData_Copy.Text = "LOWER";
            this.btnLowerTransferData_Copy.Text2 = "";
            this.btnLowerTransferData_Copy.UseVisualStyleBackColor = false;
            this.btnLowerTransferData_Copy.VisibleLeftLamp = false;
            this.btnLowerTransferData_Copy.VisibleRightLamp = false;
            // 
            // btnUpperTransferData_Copy
            // 
            this.btnUpperTransferData_Copy.BackColor = System.Drawing.Color.Transparent;
            this.btnUpperTransferData_Copy.Delay = 0;
            this.btnUpperTransferData_Copy.Flicker = false;
            this.btnUpperTransferData_Copy.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.btnUpperTransferData_Copy.ForeColor = System.Drawing.Color.Black;
            this.btnUpperTransferData_Copy.IsLeftLampOn = false;
            this.btnUpperTransferData_Copy.IsRightLampOn = false;
            this.btnUpperTransferData_Copy.LampAliveTime = 500;
            this.btnUpperTransferData_Copy.LampSize = 1;
            this.btnUpperTransferData_Copy.LeftLampColor = System.Drawing.Color.Red;
            this.btnUpperTransferData_Copy.Location = new System.Drawing.Point(214, 26);
            this.btnUpperTransferData_Copy.Name = "btnUpperTransferData_Copy";
            this.btnUpperTransferData_Copy.OnOff = false;
            this.btnUpperTransferData_Copy.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnUpperTransferData_Copy.Size = new System.Drawing.Size(17, 75);
            this.btnUpperTransferData_Copy.TabIndex = 88;
            this.btnUpperTransferData_Copy.TabStop = false;
            this.btnUpperTransferData_Copy.Text = "UPPER";
            this.btnUpperTransferData_Copy.Text2 = "";
            this.btnUpperTransferData_Copy.UseVisualStyleBackColor = false;
            this.btnUpperTransferData_Copy.VisibleLeftLamp = false;
            this.btnUpperTransferData_Copy.VisibleRightLamp = false;
            // 
            // btnAlignerTransferData_Copy
            // 
            this.btnAlignerTransferData_Copy.BackColor = System.Drawing.Color.Transparent;
            this.btnAlignerTransferData_Copy.Delay = 0;
            this.btnAlignerTransferData_Copy.Flicker = false;
            this.btnAlignerTransferData_Copy.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.btnAlignerTransferData_Copy.ForeColor = System.Drawing.Color.Black;
            this.btnAlignerTransferData_Copy.IsLeftLampOn = false;
            this.btnAlignerTransferData_Copy.IsRightLampOn = false;
            this.btnAlignerTransferData_Copy.LampAliveTime = 500;
            this.btnAlignerTransferData_Copy.LampSize = 1;
            this.btnAlignerTransferData_Copy.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlignerTransferData_Copy.Location = new System.Drawing.Point(3, 27);
            this.btnAlignerTransferData_Copy.Name = "btnAlignerTransferData_Copy";
            this.btnAlignerTransferData_Copy.OnOff = false;
            this.btnAlignerTransferData_Copy.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlignerTransferData_Copy.Size = new System.Drawing.Size(17, 75);
            this.btnAlignerTransferData_Copy.TabIndex = 88;
            this.btnAlignerTransferData_Copy.TabStop = false;
            this.btnAlignerTransferData_Copy.Text = "AL\r\nIGN";
            this.btnAlignerTransferData_Copy.Text2 = "";
            this.btnAlignerTransferData_Copy.UseVisualStyleBackColor = false;
            this.btnAlignerTransferData_Copy.VisibleLeftLamp = false;
            this.btnAlignerTransferData_Copy.VisibleRightLamp = false;
            // 
            // btnEquipmentTransferData_Copy
            // 
            this.btnEquipmentTransferData_Copy.BackColor = System.Drawing.Color.Transparent;
            this.btnEquipmentTransferData_Copy.Delay = 0;
            this.btnEquipmentTransferData_Copy.Flicker = false;
            this.btnEquipmentTransferData_Copy.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnEquipmentTransferData_Copy.ForeColor = System.Drawing.Color.Black;
            this.btnEquipmentTransferData_Copy.IsLeftLampOn = false;
            this.btnEquipmentTransferData_Copy.IsRightLampOn = false;
            this.btnEquipmentTransferData_Copy.LampAliveTime = 500;
            this.btnEquipmentTransferData_Copy.LampSize = 1;
            this.btnEquipmentTransferData_Copy.LeftLampColor = System.Drawing.Color.Red;
            this.btnEquipmentTransferData_Copy.Location = new System.Drawing.Point(3, 107);
            this.btnEquipmentTransferData_Copy.Name = "btnEquipmentTransferData_Copy";
            this.btnEquipmentTransferData_Copy.OnOff = false;
            this.btnEquipmentTransferData_Copy.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnEquipmentTransferData_Copy.Size = new System.Drawing.Size(17, 75);
            this.btnEquipmentTransferData_Copy.TabIndex = 88;
            this.btnEquipmentTransferData_Copy.TabStop = false;
            this.btnEquipmentTransferData_Copy.Text = "AVI";
            this.btnEquipmentTransferData_Copy.Text2 = "";
            this.btnEquipmentTransferData_Copy.UseVisualStyleBackColor = false;
            this.btnEquipmentTransferData_Copy.VisibleLeftLamp = false;
            this.btnEquipmentTransferData_Copy.VisibleRightLamp = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 806);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(287, 15);
            this.label2.TabIndex = 100;
            this.label2.Text = "Operator 화면은 FrmOperating으로 통합되었습니다.";
            this.label2.Visible = false;
            // 
            // panel22
            // 
            this.panel22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel22.Controls.Add(this.label48);
            this.panel22.Controls.Add(this.pnlLpm1CstInfo);
            this.panel22.Controls.Add(this.pnlLpm2CstInfo);
            this.panel22.Controls.Add(this.btnLPM2TransferData_Copy);
            this.panel22.Controls.Add(this.btnLPM1TransferData_Copy);
            this.panel22.Location = new System.Drawing.Point(1227, 471);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(200, 187);
            this.panel22.TabIndex = 13;
            // 
            // label48
            // 
            this.label48.AutoEllipsis = true;
            this.label48.BackColor = System.Drawing.Color.Gainsboro;
            this.label48.Dock = System.Windows.Forms.DockStyle.Top;
            this.label48.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.label48.ForeColor = System.Drawing.Color.Black;
            this.label48.Location = new System.Drawing.Point(0, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(198, 24);
            this.label48.TabIndex = 9;
            this.label48.Text = "■ CASSETTE INFO";
            this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLpm1CstInfo
            // 
            this.pnlLpm1CstInfo.AllowDrop = true;
            this.pnlLpm1CstInfo.Controls.Add(this.pGridCstInfoLPM1);
            this.pnlLpm1CstInfo.Location = new System.Drawing.Point(22, 105);
            this.pnlLpm1CstInfo.Name = "pnlLpm1CstInfo";
            this.pnlLpm1CstInfo.Size = new System.Drawing.Size(172, 80);
            this.pnlLpm1CstInfo.TabIndex = 89;
            this.pnlLpm1CstInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlWaferInfoDragDrop);
            this.pnlLpm1CstInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlWaferInfoDragEnter);
            this.pnlLpm1CstInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlWaferInfo_MouseDown);
            // 
            // pGridCstInfoLPM1
            // 
            this.pGridCstInfoLPM1.Enabled = false;
            this.pGridCstInfoLPM1.HelpVisible = false;
            this.pGridCstInfoLPM1.Location = new System.Drawing.Point(1, 2);
            this.pGridCstInfoLPM1.Name = "pGridCstInfoLPM1";
            this.pGridCstInfoLPM1.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pGridCstInfoLPM1.Size = new System.Drawing.Size(171, 75);
            this.pGridCstInfoLPM1.TabIndex = 10;
            this.pGridCstInfoLPM1.ToolbarVisible = false;
            // 
            // pnlLpm2CstInfo
            // 
            this.pnlLpm2CstInfo.AllowDrop = true;
            this.pnlLpm2CstInfo.Controls.Add(this.pGridCstInfoLPM2);
            this.pnlLpm2CstInfo.Location = new System.Drawing.Point(21, 24);
            this.pnlLpm2CstInfo.Name = "pnlLpm2CstInfo";
            this.pnlLpm2CstInfo.Size = new System.Drawing.Size(172, 80);
            this.pnlLpm2CstInfo.TabIndex = 89;
            this.pnlLpm2CstInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.pnlWaferInfoDragDrop);
            this.pnlLpm2CstInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.pnlWaferInfoDragEnter);
            this.pnlLpm2CstInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlWaferInfo_MouseDown);
            // 
            // pGridCstInfoLPM2
            // 
            this.pGridCstInfoLPM2.Enabled = false;
            this.pGridCstInfoLPM2.HelpVisible = false;
            this.pGridCstInfoLPM2.Location = new System.Drawing.Point(1, 3);
            this.pGridCstInfoLPM2.Name = "pGridCstInfoLPM2";
            this.pGridCstInfoLPM2.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pGridCstInfoLPM2.Size = new System.Drawing.Size(171, 75);
            this.pGridCstInfoLPM2.TabIndex = 10;
            this.pGridCstInfoLPM2.ToolbarVisible = false;
            // 
            // btnLPM2TransferData_Copy
            // 
            this.btnLPM2TransferData_Copy.BackColor = System.Drawing.Color.Transparent;
            this.btnLPM2TransferData_Copy.Delay = 0;
            this.btnLPM2TransferData_Copy.Flicker = false;
            this.btnLPM2TransferData_Copy.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnLPM2TransferData_Copy.ForeColor = System.Drawing.Color.Black;
            this.btnLPM2TransferData_Copy.IsLeftLampOn = false;
            this.btnLPM2TransferData_Copy.IsRightLampOn = false;
            this.btnLPM2TransferData_Copy.LampAliveTime = 500;
            this.btnLPM2TransferData_Copy.LampSize = 1;
            this.btnLPM2TransferData_Copy.LeftLampColor = System.Drawing.Color.Red;
            this.btnLPM2TransferData_Copy.Location = new System.Drawing.Point(5, 26);
            this.btnLPM2TransferData_Copy.Name = "btnLPM2TransferData_Copy";
            this.btnLPM2TransferData_Copy.OnOff = false;
            this.btnLPM2TransferData_Copy.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnLPM2TransferData_Copy.Size = new System.Drawing.Size(17, 75);
            this.btnLPM2TransferData_Copy.TabIndex = 88;
            this.btnLPM2TransferData_Copy.TabStop = false;
            this.btnLPM2TransferData_Copy.Text = "LPM2";
            this.btnLPM2TransferData_Copy.Text2 = "";
            this.btnLPM2TransferData_Copy.UseVisualStyleBackColor = false;
            this.btnLPM2TransferData_Copy.VisibleLeftLamp = false;
            this.btnLPM2TransferData_Copy.VisibleRightLamp = false;
            this.btnLPM2TransferData_Copy.Click += new System.EventHandler(this.btnTransferData_Click);
            // 
            // btnLPM1TransferData_Copy
            // 
            this.btnLPM1TransferData_Copy.BackColor = System.Drawing.Color.Transparent;
            this.btnLPM1TransferData_Copy.Delay = 0;
            this.btnLPM1TransferData_Copy.Flicker = false;
            this.btnLPM1TransferData_Copy.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnLPM1TransferData_Copy.ForeColor = System.Drawing.Color.Black;
            this.btnLPM1TransferData_Copy.IsLeftLampOn = false;
            this.btnLPM1TransferData_Copy.IsRightLampOn = false;
            this.btnLPM1TransferData_Copy.LampAliveTime = 500;
            this.btnLPM1TransferData_Copy.LampSize = 1;
            this.btnLPM1TransferData_Copy.LeftLampColor = System.Drawing.Color.Red;
            this.btnLPM1TransferData_Copy.Location = new System.Drawing.Point(5, 107);
            this.btnLPM1TransferData_Copy.Name = "btnLPM1TransferData_Copy";
            this.btnLPM1TransferData_Copy.OnOff = false;
            this.btnLPM1TransferData_Copy.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnLPM1TransferData_Copy.Size = new System.Drawing.Size(17, 75);
            this.btnLPM1TransferData_Copy.TabIndex = 88;
            this.btnLPM1TransferData_Copy.TabStop = false;
            this.btnLPM1TransferData_Copy.Text = "LPM1";
            this.btnLPM1TransferData_Copy.Text2 = "";
            this.btnLPM1TransferData_Copy.UseVisualStyleBackColor = false;
            this.btnLPM1TransferData_Copy.VisibleLeftLamp = false;
            this.btnLPM1TransferData_Copy.VisibleRightLamp = false;
            this.btnLPM1TransferData_Copy.Click += new System.EventHandler(this.btnTransferData_Click);
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.tabCtrlTransferData);
            this.panel7.Controls.Add(this.btnShift);
            this.panel7.Controls.Add(this.btnEquipmentTransferData);
            this.panel7.Controls.Add(this.btnAlignerTransferData);
            this.panel7.Controls.Add(this.btnLowerRobotTransferData);
            this.panel7.Controls.Add(this.btnUpperRobotTransferData);
            this.panel7.Controls.Add(this.btnInfoLog);
            this.panel7.Controls.Add(this.btnLPM2CassetteTransferData);
            this.panel7.Controls.Add(this.btnLPM1CassetteTransferData);
            this.panel7.Controls.Add(this.lblGlassInfoTitle);
            this.panel7.Location = new System.Drawing.Point(806, 663);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(619, 376);
            this.panel7.TabIndex = 13;
            this.panel7.Paint += new System.Windows.Forms.PaintEventHandler(this.panel7_Paint);
            // 
            // tabCtrlTransferData
            // 
            this.tabCtrlTransferData.Controls.Add(this.tabpCassette);
            this.tabCtrlTransferData.Controls.Add(this.tabpWafer);
            this.tabCtrlTransferData.Controls.Add(this.tabpAutoDataLog);
            this.tabCtrlTransferData.Location = new System.Drawing.Point(3, 22);
            this.tabCtrlTransferData.Name = "tabCtrlTransferData";
            this.tabCtrlTransferData.SelectedIndex = 0;
            this.tabCtrlTransferData.Size = new System.Drawing.Size(508, 343);
            this.tabCtrlTransferData.TabIndex = 89;
            // 
            // tabpCassette
            // 
            this.tabpCassette.Controls.Add(this.pGridCstTransferInfo);
            this.tabpCassette.Location = new System.Drawing.Point(4, 24);
            this.tabpCassette.Name = "tabpCassette";
            this.tabpCassette.Padding = new System.Windows.Forms.Padding(3);
            this.tabpCassette.Size = new System.Drawing.Size(500, 315);
            this.tabpCassette.TabIndex = 0;
            this.tabpCassette.Text = "Cassette";
            this.tabpCassette.UseVisualStyleBackColor = true;
            // 
            // pGridCstTransferInfo
            // 
            this.pGridCstTransferInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pGridCstTransferInfo.HelpVisible = false;
            this.pGridCstTransferInfo.Location = new System.Drawing.Point(3, 3);
            this.pGridCstTransferInfo.Name = "pGridCstTransferInfo";
            this.pGridCstTransferInfo.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pGridCstTransferInfo.Size = new System.Drawing.Size(494, 309);
            this.pGridCstTransferInfo.TabIndex = 10;
            this.pGridCstTransferInfo.ToolbarVisible = false;
            // 
            // tabpWafer
            // 
            this.tabpWafer.Controls.Add(this.pGridWaferTransferInfo);
            this.tabpWafer.Location = new System.Drawing.Point(4, 22);
            this.tabpWafer.Name = "tabpWafer";
            this.tabpWafer.Padding = new System.Windows.Forms.Padding(3);
            this.tabpWafer.Size = new System.Drawing.Size(500, 317);
            this.tabpWafer.TabIndex = 1;
            this.tabpWafer.Text = "Wafer";
            this.tabpWafer.UseVisualStyleBackColor = true;
            // 
            // pGridWaferTransferInfo
            // 
            this.pGridWaferTransferInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pGridWaferTransferInfo.HelpVisible = false;
            this.pGridWaferTransferInfo.Location = new System.Drawing.Point(3, 3);
            this.pGridWaferTransferInfo.Name = "pGridWaferTransferInfo";
            this.pGridWaferTransferInfo.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pGridWaferTransferInfo.Size = new System.Drawing.Size(494, 311);
            this.pGridWaferTransferInfo.TabIndex = 11;
            this.pGridWaferTransferInfo.ToolbarVisible = false;
            // 
            // tabpAutoDataLog
            // 
            this.tabpAutoDataLog.Controls.Add(this.lstAutoDataLog);
            this.tabpAutoDataLog.Location = new System.Drawing.Point(4, 22);
            this.tabpAutoDataLog.Name = "tabpAutoDataLog";
            this.tabpAutoDataLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabpAutoDataLog.Size = new System.Drawing.Size(500, 317);
            this.tabpAutoDataLog.TabIndex = 2;
            this.tabpAutoDataLog.Text = "전산 LOG";
            this.tabpAutoDataLog.UseVisualStyleBackColor = true;
            // 
            // lstAutoDataLog
            // 
            this.lstAutoDataLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstAutoDataLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstAutoDataLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstAutoDataLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lstAutoDataLog.FullRowSelect = true;
            this.lstAutoDataLog.HideSelection = false;
            this.lstAutoDataLog.Location = new System.Drawing.Point(3, 3);
            this.lstAutoDataLog.Margin = new System.Windows.Forms.Padding(0);
            this.lstAutoDataLog.Name = "lstAutoDataLog";
            this.lstAutoDataLog.Size = new System.Drawing.Size(494, 311);
            this.lstAutoDataLog.TabIndex = 97;
            this.lstAutoDataLog.UseCompatibleStateImageBehavior = false;
            this.lstAutoDataLog.View = System.Windows.Forms.View.Details;
            this.lstAutoDataLog.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstAutoDataLog_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Time";
            this.columnHeader1.Width = 113;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "LOG";
            this.columnHeader2.Width = 375;
            // 
            // btnShift
            // 
            this.btnShift.BackColor = System.Drawing.Color.Transparent;
            this.btnShift.Delay = 0;
            this.btnShift.Flicker = false;
            this.btnShift.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnShift.ForeColor = System.Drawing.Color.Black;
            this.btnShift.IsLeftLampOn = false;
            this.btnShift.IsRightLampOn = false;
            this.btnShift.LampAliveTime = 500;
            this.btnShift.LampSize = 1;
            this.btnShift.LeftLampColor = System.Drawing.Color.Red;
            this.btnShift.Location = new System.Drawing.Point(517, 324);
            this.btnShift.Name = "btnShift";
            this.btnShift.OnOff = false;
            this.btnShift.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnShift.Size = new System.Drawing.Size(97, 37);
            this.btnShift.TabIndex = 88;
            this.btnShift.TabStop = false;
            this.btnShift.Text = "Shift";
            this.btnShift.Text2 = "";
            this.btnShift.UseVisualStyleBackColor = false;
            this.btnShift.VisibleLeftLamp = false;
            this.btnShift.VisibleRightLamp = false;
            this.btnShift.Click += new System.EventHandler(this.btnTransferData_Click);
            // 
            // btnEquipmentTransferData
            // 
            this.btnEquipmentTransferData.BackColor = System.Drawing.Color.Transparent;
            this.btnEquipmentTransferData.Delay = 0;
            this.btnEquipmentTransferData.Flicker = false;
            this.btnEquipmentTransferData.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnEquipmentTransferData.ForeColor = System.Drawing.Color.Black;
            this.btnEquipmentTransferData.IsLeftLampOn = false;
            this.btnEquipmentTransferData.IsRightLampOn = false;
            this.btnEquipmentTransferData.LampAliveTime = 500;
            this.btnEquipmentTransferData.LampSize = 1;
            this.btnEquipmentTransferData.LeftLampColor = System.Drawing.Color.Red;
            this.btnEquipmentTransferData.Location = new System.Drawing.Point(517, 281);
            this.btnEquipmentTransferData.Name = "btnEquipmentTransferData";
            this.btnEquipmentTransferData.OnOff = false;
            this.btnEquipmentTransferData.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnEquipmentTransferData.Size = new System.Drawing.Size(97, 37);
            this.btnEquipmentTransferData.TabIndex = 88;
            this.btnEquipmentTransferData.TabStop = false;
            this.btnEquipmentTransferData.Text = "Equipment";
            this.btnEquipmentTransferData.Text2 = "";
            this.btnEquipmentTransferData.UseVisualStyleBackColor = false;
            this.btnEquipmentTransferData.VisibleLeftLamp = false;
            this.btnEquipmentTransferData.VisibleRightLamp = false;
            this.btnEquipmentTransferData.Click += new System.EventHandler(this.btnTransferData_Click);
            // 
            // btnAlignerTransferData
            // 
            this.btnAlignerTransferData.BackColor = System.Drawing.Color.Transparent;
            this.btnAlignerTransferData.Delay = 0;
            this.btnAlignerTransferData.Flicker = false;
            this.btnAlignerTransferData.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnAlignerTransferData.ForeColor = System.Drawing.Color.Black;
            this.btnAlignerTransferData.IsLeftLampOn = false;
            this.btnAlignerTransferData.IsRightLampOn = false;
            this.btnAlignerTransferData.LampAliveTime = 500;
            this.btnAlignerTransferData.LampSize = 1;
            this.btnAlignerTransferData.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlignerTransferData.Location = new System.Drawing.Point(517, 238);
            this.btnAlignerTransferData.Name = "btnAlignerTransferData";
            this.btnAlignerTransferData.OnOff = false;
            this.btnAlignerTransferData.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlignerTransferData.Size = new System.Drawing.Size(97, 37);
            this.btnAlignerTransferData.TabIndex = 88;
            this.btnAlignerTransferData.TabStop = false;
            this.btnAlignerTransferData.Text = "Aligner";
            this.btnAlignerTransferData.Text2 = "";
            this.btnAlignerTransferData.UseVisualStyleBackColor = false;
            this.btnAlignerTransferData.VisibleLeftLamp = false;
            this.btnAlignerTransferData.VisibleRightLamp = false;
            this.btnAlignerTransferData.Click += new System.EventHandler(this.btnTransferData_Click);
            // 
            // btnLowerRobotTransferData
            // 
            this.btnLowerRobotTransferData.BackColor = System.Drawing.Color.Transparent;
            this.btnLowerRobotTransferData.Delay = 0;
            this.btnLowerRobotTransferData.Flicker = false;
            this.btnLowerRobotTransferData.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnLowerRobotTransferData.ForeColor = System.Drawing.Color.Black;
            this.btnLowerRobotTransferData.IsLeftLampOn = false;
            this.btnLowerRobotTransferData.IsRightLampOn = false;
            this.btnLowerRobotTransferData.LampAliveTime = 500;
            this.btnLowerRobotTransferData.LampSize = 1;
            this.btnLowerRobotTransferData.LeftLampColor = System.Drawing.Color.Red;
            this.btnLowerRobotTransferData.Location = new System.Drawing.Point(517, 195);
            this.btnLowerRobotTransferData.Name = "btnLowerRobotTransferData";
            this.btnLowerRobotTransferData.OnOff = false;
            this.btnLowerRobotTransferData.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnLowerRobotTransferData.Size = new System.Drawing.Size(97, 37);
            this.btnLowerRobotTransferData.TabIndex = 88;
            this.btnLowerRobotTransferData.TabStop = false;
            this.btnLowerRobotTransferData.Text = "Lower Robot";
            this.btnLowerRobotTransferData.Text2 = "";
            this.btnLowerRobotTransferData.UseVisualStyleBackColor = false;
            this.btnLowerRobotTransferData.VisibleLeftLamp = false;
            this.btnLowerRobotTransferData.VisibleRightLamp = false;
            this.btnLowerRobotTransferData.Click += new System.EventHandler(this.btnTransferData_Click);
            // 
            // btnUpperRobotTransferData
            // 
            this.btnUpperRobotTransferData.BackColor = System.Drawing.Color.Transparent;
            this.btnUpperRobotTransferData.Delay = 0;
            this.btnUpperRobotTransferData.Flicker = false;
            this.btnUpperRobotTransferData.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnUpperRobotTransferData.ForeColor = System.Drawing.Color.Black;
            this.btnUpperRobotTransferData.IsLeftLampOn = false;
            this.btnUpperRobotTransferData.IsRightLampOn = false;
            this.btnUpperRobotTransferData.LampAliveTime = 500;
            this.btnUpperRobotTransferData.LampSize = 1;
            this.btnUpperRobotTransferData.LeftLampColor = System.Drawing.Color.Red;
            this.btnUpperRobotTransferData.Location = new System.Drawing.Point(517, 152);
            this.btnUpperRobotTransferData.Name = "btnUpperRobotTransferData";
            this.btnUpperRobotTransferData.OnOff = false;
            this.btnUpperRobotTransferData.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnUpperRobotTransferData.Size = new System.Drawing.Size(97, 37);
            this.btnUpperRobotTransferData.TabIndex = 88;
            this.btnUpperRobotTransferData.TabStop = false;
            this.btnUpperRobotTransferData.Text = "Upper Robot";
            this.btnUpperRobotTransferData.Text2 = "";
            this.btnUpperRobotTransferData.UseVisualStyleBackColor = false;
            this.btnUpperRobotTransferData.VisibleLeftLamp = false;
            this.btnUpperRobotTransferData.VisibleRightLamp = false;
            this.btnUpperRobotTransferData.Click += new System.EventHandler(this.btnTransferData_Click);
            // 
            // btnInfoLog
            // 
            this.btnInfoLog.BackColor = System.Drawing.Color.Transparent;
            this.btnInfoLog.Delay = 0;
            this.btnInfoLog.Flicker = false;
            this.btnInfoLog.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnInfoLog.ForeColor = System.Drawing.Color.Black;
            this.btnInfoLog.IsLeftLampOn = false;
            this.btnInfoLog.IsRightLampOn = false;
            this.btnInfoLog.LampAliveTime = 500;
            this.btnInfoLog.LampSize = 1;
            this.btnInfoLog.LeftLampColor = System.Drawing.Color.Red;
            this.btnInfoLog.Location = new System.Drawing.Point(517, 22);
            this.btnInfoLog.Name = "btnInfoLog";
            this.btnInfoLog.OnOff = false;
            this.btnInfoLog.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnInfoLog.Size = new System.Drawing.Size(97, 37);
            this.btnInfoLog.TabIndex = 87;
            this.btnInfoLog.TabStop = false;
            this.btnInfoLog.Text = "전산 LOG";
            this.btnInfoLog.Text2 = "";
            this.btnInfoLog.UseVisualStyleBackColor = false;
            this.btnInfoLog.VisibleLeftLamp = false;
            this.btnInfoLog.VisibleRightLamp = false;
            this.btnInfoLog.Click += new System.EventHandler(this.btnInfoLog_Click);
            // 
            // btnLPM2CassetteTransferData
            // 
            this.btnLPM2CassetteTransferData.BackColor = System.Drawing.Color.Transparent;
            this.btnLPM2CassetteTransferData.Delay = 0;
            this.btnLPM2CassetteTransferData.Flicker = false;
            this.btnLPM2CassetteTransferData.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnLPM2CassetteTransferData.ForeColor = System.Drawing.Color.Black;
            this.btnLPM2CassetteTransferData.IsLeftLampOn = false;
            this.btnLPM2CassetteTransferData.IsRightLampOn = false;
            this.btnLPM2CassetteTransferData.LampAliveTime = 500;
            this.btnLPM2CassetteTransferData.LampSize = 1;
            this.btnLPM2CassetteTransferData.LeftLampColor = System.Drawing.Color.Red;
            this.btnLPM2CassetteTransferData.Location = new System.Drawing.Point(517, 66);
            this.btnLPM2CassetteTransferData.Name = "btnLPM2CassetteTransferData";
            this.btnLPM2CassetteTransferData.OnOff = false;
            this.btnLPM2CassetteTransferData.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnLPM2CassetteTransferData.Size = new System.Drawing.Size(97, 37);
            this.btnLPM2CassetteTransferData.TabIndex = 87;
            this.btnLPM2CassetteTransferData.TabStop = false;
            this.btnLPM2CassetteTransferData.Text = "Load Port 2";
            this.btnLPM2CassetteTransferData.Text2 = "";
            this.btnLPM2CassetteTransferData.UseVisualStyleBackColor = false;
            this.btnLPM2CassetteTransferData.VisibleLeftLamp = false;
            this.btnLPM2CassetteTransferData.VisibleRightLamp = false;
            this.btnLPM2CassetteTransferData.Click += new System.EventHandler(this.btnTransferData_Click);
            // 
            // btnLPM1CassetteTransferData
            // 
            this.btnLPM1CassetteTransferData.BackColor = System.Drawing.Color.Transparent;
            this.btnLPM1CassetteTransferData.Delay = 0;
            this.btnLPM1CassetteTransferData.Flicker = false;
            this.btnLPM1CassetteTransferData.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnLPM1CassetteTransferData.ForeColor = System.Drawing.Color.Black;
            this.btnLPM1CassetteTransferData.IsLeftLampOn = false;
            this.btnLPM1CassetteTransferData.IsRightLampOn = false;
            this.btnLPM1CassetteTransferData.LampAliveTime = 500;
            this.btnLPM1CassetteTransferData.LampSize = 1;
            this.btnLPM1CassetteTransferData.LeftLampColor = System.Drawing.Color.Red;
            this.btnLPM1CassetteTransferData.Location = new System.Drawing.Point(517, 109);
            this.btnLPM1CassetteTransferData.Name = "btnLPM1CassetteTransferData";
            this.btnLPM1CassetteTransferData.OnOff = false;
            this.btnLPM1CassetteTransferData.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnLPM1CassetteTransferData.Size = new System.Drawing.Size(97, 37);
            this.btnLPM1CassetteTransferData.TabIndex = 86;
            this.btnLPM1CassetteTransferData.TabStop = false;
            this.btnLPM1CassetteTransferData.Text = "Load Port 1";
            this.btnLPM1CassetteTransferData.Text2 = "";
            this.btnLPM1CassetteTransferData.UseVisualStyleBackColor = false;
            this.btnLPM1CassetteTransferData.VisibleLeftLamp = false;
            this.btnLPM1CassetteTransferData.VisibleRightLamp = false;
            this.btnLPM1CassetteTransferData.Click += new System.EventHandler(this.btnTransferData_Click);
            // 
            // lblGlassInfoTitle
            // 
            this.lblGlassInfoTitle.AutoEllipsis = true;
            this.lblGlassInfoTitle.BackColor = System.Drawing.Color.Gainsboro;
            this.lblGlassInfoTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGlassInfoTitle.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblGlassInfoTitle.ForeColor = System.Drawing.Color.Black;
            this.lblGlassInfoTitle.Location = new System.Drawing.Point(0, 0);
            this.lblGlassInfoTitle.Name = "lblGlassInfoTitle";
            this.lblGlassInfoTitle.Size = new System.Drawing.Size(617, 24);
            this.lblGlassInfoTitle.TabIndex = 9;
            this.lblGlassInfoTitle.Text = "■ TRANSFER INFO";
            this.lblGlassInfoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDate
            // 
            this.labelDate.BackColor = System.Drawing.Color.White;
            this.labelDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDate.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.labelDate.Location = new System.Drawing.Point(4, 60);
            this.labelDate.Margin = new System.Windows.Forms.Padding(1);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(191, 36);
            this.labelDate.TabIndex = 2;
            this.labelDate.Text = "2016-01-01";
            this.labelDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMode
            // 
            this.labelMode.BackColor = System.Drawing.Color.Gainsboro;
            this.labelMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelMode.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.labelMode.Location = new System.Drawing.Point(4, 5);
            this.labelMode.Margin = new System.Windows.Forms.Padding(1);
            this.labelMode.Name = "labelMode";
            this.labelMode.Size = new System.Drawing.Size(191, 52);
            this.labelMode.TabIndex = 1;
            this.labelMode.Text = "자동 모드";
            this.labelMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMaxScanTime
            // 
            this.lblMaxScanTime.BackColor = System.Drawing.Color.White;
            this.lblMaxScanTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMaxScanTime.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblMaxScanTime.Location = new System.Drawing.Point(428, 60);
            this.lblMaxScanTime.Margin = new System.Windows.Forms.Padding(1);
            this.lblMaxScanTime.Name = "lblMaxScanTime";
            this.lblMaxScanTime.Size = new System.Drawing.Size(51, 17);
            this.lblMaxScanTime.TabIndex = 3;
            this.lblMaxScanTime.Text = "0";
            this.lblMaxScanTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMaxScanTime.Click += new System.EventHandler(this.lblMaxScanTime_Click);
            // 
            // tabCtrl_MainMenu
            // 
            this.tabCtrl_MainMenu.Controls.Add(this.tabp_Progress);
            this.tabCtrl_MainMenu.Controls.Add(this.tabp_Step);
            this.tabCtrl_MainMenu.ItemSize = new System.Drawing.Size(158, 25);
            this.tabCtrl_MainMenu.Location = new System.Drawing.Point(4, 522);
            this.tabCtrl_MainMenu.Name = "tabCtrl_MainMenu";
            this.tabCtrl_MainMenu.SelectedIndex = 0;
            this.tabCtrl_MainMenu.Size = new System.Drawing.Size(481, 495);
            this.tabCtrl_MainMenu.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabCtrl_MainMenu.TabIndex = 103;
            // 
            // tabp_Progress
            // 
            this.tabp_Progress.Controls.Add(this.btnLpm1DeepReviewPass);
            this.tabp_Progress.Controls.Add(this.btnLpm2DeepReviewPass);
            this.tabp_Progress.Controls.Add(this.btnLpm2OHTStepReset);
            this.tabp_Progress.Controls.Add(this.btnLpm2OHTLdComplete);
            this.tabp_Progress.Controls.Add(this.btnLpm2OHTUldComplete);
            this.tabp_Progress.Controls.Add(this.btnLpm1OHTStepReset);
            this.tabp_Progress.Controls.Add(this.btnLpm1OHTLdComplete);
            this.tabp_Progress.Controls.Add(this.btnLpm1OHTUldComplete);
            this.tabp_Progress.Controls.Add(this.lblLpm1Pogress);
            this.tabp_Progress.Controls.Add(this.lblLpm2Pogress);
            this.tabp_Progress.Controls.Add(this.lblLpm2);
            this.tabp_Progress.Controls.Add(this.lblLpm1);
            this.tabp_Progress.Location = new System.Drawing.Point(4, 29);
            this.tabp_Progress.Name = "tabp_Progress";
            this.tabp_Progress.Padding = new System.Windows.Forms.Padding(3);
            this.tabp_Progress.Size = new System.Drawing.Size(473, 462);
            this.tabp_Progress.TabIndex = 4;
            this.tabp_Progress.Text = "진행 현황";
            this.tabp_Progress.UseVisualStyleBackColor = true;
            // 
            // btnLpm1DeepReviewPass
            // 
            this.btnLpm1DeepReviewPass.Location = new System.Drawing.Point(288, 355);
            this.btnLpm1DeepReviewPass.Name = "btnLpm1DeepReviewPass";
            this.btnLpm1DeepReviewPass.Size = new System.Drawing.Size(156, 37);
            this.btnLpm1DeepReviewPass.TabIndex = 66;
            this.btnLpm1DeepReviewPass.Text = "딥 러닝 패스";
            this.btnLpm1DeepReviewPass.UseVisualStyleBackColor = true;
            this.btnLpm1DeepReviewPass.Click += new System.EventHandler(this.btnLpm2DeepReviewPass_Click);
            // 
            // btnLpm2DeepReviewPass
            // 
            this.btnLpm2DeepReviewPass.Location = new System.Drawing.Point(288, 139);
            this.btnLpm2DeepReviewPass.Name = "btnLpm2DeepReviewPass";
            this.btnLpm2DeepReviewPass.Size = new System.Drawing.Size(156, 37);
            this.btnLpm2DeepReviewPass.TabIndex = 66;
            this.btnLpm2DeepReviewPass.Text = "딥 러닝 패스";
            this.btnLpm2DeepReviewPass.UseVisualStyleBackColor = true;
            this.btnLpm2DeepReviewPass.Click += new System.EventHandler(this.btnLpm2DeepReviewPass_Click);
            // 
            // btnLpm2OHTStepReset
            // 
            this.btnLpm2OHTStepReset.Location = new System.Drawing.Point(8, 196);
            this.btnLpm2OHTStepReset.Name = "btnLpm2OHTStepReset";
            this.btnLpm2OHTStepReset.Size = new System.Drawing.Size(156, 23);
            this.btnLpm2OHTStepReset.TabIndex = 66;
            this.btnLpm2OHTStepReset.Text = "OHT 투입/배출 스텝 리셋";
            this.btnLpm2OHTStepReset.UseVisualStyleBackColor = true;
            this.btnLpm2OHTStepReset.Click += new System.EventHandler(this.btnLpm1OHTStepReset_Click);
            // 
            // btnLpm2OHTLdComplete
            // 
            this.btnLpm2OHTLdComplete.Location = new System.Drawing.Point(8, 139);
            this.btnLpm2OHTLdComplete.Name = "btnLpm2OHTLdComplete";
            this.btnLpm2OHTLdComplete.Size = new System.Drawing.Size(156, 23);
            this.btnLpm2OHTLdComplete.TabIndex = 66;
            this.btnLpm2OHTLdComplete.Text = "투입 완료 처리";
            this.btnLpm2OHTLdComplete.UseVisualStyleBackColor = true;
            this.btnLpm2OHTLdComplete.Click += new System.EventHandler(this.btnLpm1OHTComplete_Click);
            // 
            // btnLpm2OHTUldComplete
            // 
            this.btnLpm2OHTUldComplete.Location = new System.Drawing.Point(8, 168);
            this.btnLpm2OHTUldComplete.Name = "btnLpm2OHTUldComplete";
            this.btnLpm2OHTUldComplete.Size = new System.Drawing.Size(156, 23);
            this.btnLpm2OHTUldComplete.TabIndex = 66;
            this.btnLpm2OHTUldComplete.Text = "배출 완료 처리";
            this.btnLpm2OHTUldComplete.UseVisualStyleBackColor = true;
            this.btnLpm2OHTUldComplete.Click += new System.EventHandler(this.btnLpm1OHTComplete_Click);
            // 
            // btnLpm1OHTStepReset
            // 
            this.btnLpm1OHTStepReset.Location = new System.Drawing.Point(8, 411);
            this.btnLpm1OHTStepReset.Name = "btnLpm1OHTStepReset";
            this.btnLpm1OHTStepReset.Size = new System.Drawing.Size(156, 23);
            this.btnLpm1OHTStepReset.TabIndex = 66;
            this.btnLpm1OHTStepReset.Text = "OHT 투입/배출 스텝 리셋";
            this.btnLpm1OHTStepReset.UseVisualStyleBackColor = true;
            this.btnLpm1OHTStepReset.Click += new System.EventHandler(this.btnLpm1OHTStepReset_Click);
            // 
            // btnLpm1OHTLdComplete
            // 
            this.btnLpm1OHTLdComplete.Location = new System.Drawing.Point(8, 355);
            this.btnLpm1OHTLdComplete.Name = "btnLpm1OHTLdComplete";
            this.btnLpm1OHTLdComplete.Size = new System.Drawing.Size(156, 23);
            this.btnLpm1OHTLdComplete.TabIndex = 66;
            this.btnLpm1OHTLdComplete.Text = "투입 완료 처리";
            this.btnLpm1OHTLdComplete.UseVisualStyleBackColor = true;
            this.btnLpm1OHTLdComplete.Click += new System.EventHandler(this.btnLpm1OHTComplete_Click);
            // 
            // btnLpm1OHTUldComplete
            // 
            this.btnLpm1OHTUldComplete.Location = new System.Drawing.Point(8, 384);
            this.btnLpm1OHTUldComplete.Name = "btnLpm1OHTUldComplete";
            this.btnLpm1OHTUldComplete.Size = new System.Drawing.Size(156, 23);
            this.btnLpm1OHTUldComplete.TabIndex = 66;
            this.btnLpm1OHTUldComplete.Text = "배출 완료 처리";
            this.btnLpm1OHTUldComplete.UseVisualStyleBackColor = true;
            this.btnLpm1OHTUldComplete.Click += new System.EventHandler(this.btnLpm1OHTComplete_Click);
            // 
            // lblLpm1Pogress
            // 
            this.lblLpm1Pogress.AutoEllipsis = true;
            this.lblLpm1Pogress.BackColor = System.Drawing.Color.White;
            this.lblLpm1Pogress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLpm1Pogress.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblLpm1Pogress.ForeColor = System.Drawing.Color.Black;
            this.lblLpm1Pogress.Location = new System.Drawing.Point(134, 252);
            this.lblLpm1Pogress.Name = "lblLpm1Pogress";
            this.lblLpm1Pogress.Size = new System.Drawing.Size(310, 100);
            this.lblLpm1Pogress.TabIndex = 65;
            this.lblLpm1Pogress.Text = "Load Port 1";
            this.lblLpm1Pogress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLpm1Pogress.DoubleClick += new System.EventHandler(this.btnPmacRestart_DoubleClick);
            // 
            // lblLpm2Pogress
            // 
            this.lblLpm2Pogress.AutoEllipsis = true;
            this.lblLpm2Pogress.BackColor = System.Drawing.Color.White;
            this.lblLpm2Pogress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLpm2Pogress.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblLpm2Pogress.ForeColor = System.Drawing.Color.Black;
            this.lblLpm2Pogress.Location = new System.Drawing.Point(134, 36);
            this.lblLpm2Pogress.Name = "lblLpm2Pogress";
            this.lblLpm2Pogress.Size = new System.Drawing.Size(310, 100);
            this.lblLpm2Pogress.TabIndex = 65;
            this.lblLpm2Pogress.Text = "Load Port 2";
            this.lblLpm2Pogress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLpm2Pogress.DoubleClick += new System.EventHandler(this.btnPmacRestart_DoubleClick);
            // 
            // lblLpm2
            // 
            this.lblLpm2.AutoEllipsis = true;
            this.lblLpm2.BackColor = System.Drawing.Color.White;
            this.lblLpm2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLpm2.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblLpm2.ForeColor = System.Drawing.Color.Black;
            this.lblLpm2.Location = new System.Drawing.Point(8, 36);
            this.lblLpm2.Name = "lblLpm2";
            this.lblLpm2.Size = new System.Drawing.Size(121, 100);
            this.lblLpm2.TabIndex = 65;
            this.lblLpm2.Text = "Load Port 2";
            this.lblLpm2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLpm2.DoubleClick += new System.EventHandler(this.btnPmacRestart_DoubleClick);
            // 
            // lblLpm1
            // 
            this.lblLpm1.AutoEllipsis = true;
            this.lblLpm1.BackColor = System.Drawing.Color.White;
            this.lblLpm1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLpm1.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblLpm1.ForeColor = System.Drawing.Color.Black;
            this.lblLpm1.Location = new System.Drawing.Point(8, 252);
            this.lblLpm1.Name = "lblLpm1";
            this.lblLpm1.Size = new System.Drawing.Size(121, 100);
            this.lblLpm1.TabIndex = 65;
            this.lblLpm1.Text = "Load Port 1";
            this.lblLpm1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLpm1.DoubleClick += new System.EventHandler(this.btnPmacRestart_DoubleClick);
            // 
            // tabp_Step
            // 
            this.tabp_Step.BackColor = System.Drawing.Color.Transparent;
            this.tabp_Step.Controls.Add(this.pnlMainMenus);
            this.tabp_Step.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tabp_Step.Location = new System.Drawing.Point(4, 29);
            this.tabp_Step.Name = "tabp_Step";
            this.tabp_Step.Padding = new System.Windows.Forms.Padding(3);
            this.tabp_Step.Size = new System.Drawing.Size(473, 462);
            this.tabp_Step.TabIndex = 0;
            this.tabp_Step.Text = "스텝 화면";
            // 
            // pnlMainMenus
            // 
            this.pnlMainMenus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMainMenus.Controls.Add(this.pnlMainStep);
            this.pnlMainMenus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainMenus.Location = new System.Drawing.Point(3, 3);
            this.pnlMainMenus.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMainMenus.Name = "pnlMainMenus";
            this.pnlMainMenus.Size = new System.Drawing.Size(467, 456);
            this.pnlMainMenus.TabIndex = 103;
            // 
            // pnlMainStep
            // 
            this.pnlMainStep.Controls.Add(this.panel1);
            this.pnlMainStep.Controls.Add(this.panel10);
            this.pnlMainStep.Controls.Add(this.panel6);
            this.pnlMainStep.Controls.Add(this.panel18);
            this.pnlMainStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainStep.Location = new System.Drawing.Point(0, 0);
            this.pnlMainStep.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMainStep.Name = "pnlMainStep";
            this.pnlMainStep.Size = new System.Drawing.Size(465, 454);
            this.pnlMainStep.TabIndex = 102;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Controls.Add(this.lblPioUpeerStep);
            this.panel1.Controls.Add(this.label35);
            this.panel1.Controls.Add(this.lblPioLowerStep);
            this.panel1.Controls.Add(this.label39);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 322);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(465, 66);
            this.panel1.TabIndex = 495;
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(426, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 22);
            this.label1.TabIndex = 121;
            this.label1.Text = "-";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoEllipsis = true;
            this.label8.BackColor = System.Drawing.Color.Gainsboro;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(426, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 22);
            this.label8.TabIndex = 122;
            this.label8.Text = "-";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label31
            // 
            this.label31.AutoEllipsis = true;
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.label31.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label31.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label31.ForeColor = System.Drawing.Color.Black;
            this.label31.Location = new System.Drawing.Point(3, 40);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(91, 22);
            this.label31.TabIndex = 116;
            this.label31.Text = "Lower Pio";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPioUpeerStep
            // 
            this.lblPioUpeerStep.AutoEllipsis = true;
            this.lblPioUpeerStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lblPioUpeerStep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPioUpeerStep.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblPioUpeerStep.ForeColor = System.Drawing.Color.Red;
            this.lblPioUpeerStep.Location = new System.Drawing.Point(95, 18);
            this.lblPioUpeerStep.Name = "lblPioUpeerStep";
            this.lblPioUpeerStep.Size = new System.Drawing.Size(330, 22);
            this.lblPioUpeerStep.TabIndex = 115;
            this.lblPioUpeerStep.Text = "-";
            this.lblPioUpeerStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label35
            // 
            this.label35.AutoEllipsis = true;
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.label35.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label35.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label35.ForeColor = System.Drawing.Color.Black;
            this.label35.Location = new System.Drawing.Point(3, 18);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(91, 22);
            this.label35.TabIndex = 114;
            this.label35.Text = "Upper Pio";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPioLowerStep
            // 
            this.lblPioLowerStep.AutoEllipsis = true;
            this.lblPioLowerStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lblPioLowerStep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPioLowerStep.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblPioLowerStep.ForeColor = System.Drawing.Color.Red;
            this.lblPioLowerStep.Location = new System.Drawing.Point(95, 40);
            this.lblPioLowerStep.Name = "lblPioLowerStep";
            this.lblPioLowerStep.Size = new System.Drawing.Size(330, 22);
            this.lblPioLowerStep.TabIndex = 117;
            this.lblPioLowerStep.Text = "-";
            this.lblPioLowerStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label39
            // 
            this.label39.AutoEllipsis = true;
            this.label39.BackColor = System.Drawing.Color.Gainsboro;
            this.label39.Dock = System.Windows.Forms.DockStyle.Top;
            this.label39.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label39.ForeColor = System.Drawing.Color.Black;
            this.label39.Location = new System.Drawing.Point(0, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(463, 14);
            this.label39.TabIndex = 9;
            this.label39.Text = "■ Equip <-> Robot";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.lblPioLowerStepRecovery);
            this.panel10.Controls.Add(this.lblPioUpperStepRecovery);
            this.panel10.Controls.Add(this.label49);
            this.panel10.Controls.Add(this.lblPioLPM1OHTStep);
            this.panel10.Controls.Add(this.label52);
            this.panel10.Controls.Add(this.lblPioLPM2OHTStep);
            this.panel10.Controls.Add(this.label95);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel10.Location = new System.Drawing.Point(0, 388);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(465, 66);
            this.panel10.TabIndex = 494;
            // 
            // lblPioLowerStepRecovery
            // 
            this.lblPioLowerStepRecovery.AutoEllipsis = true;
            this.lblPioLowerStepRecovery.BackColor = System.Drawing.Color.Gainsboro;
            this.lblPioLowerStepRecovery.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPioLowerStepRecovery.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblPioLowerStepRecovery.ForeColor = System.Drawing.Color.Red;
            this.lblPioLowerStepRecovery.Location = new System.Drawing.Point(426, 40);
            this.lblPioLowerStepRecovery.Name = "lblPioLowerStepRecovery";
            this.lblPioLowerStepRecovery.Size = new System.Drawing.Size(37, 22);
            this.lblPioLowerStepRecovery.TabIndex = 121;
            this.lblPioLowerStepRecovery.Text = "-";
            this.lblPioLowerStepRecovery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPioUpperStepRecovery
            // 
            this.lblPioUpperStepRecovery.AutoEllipsis = true;
            this.lblPioUpperStepRecovery.BackColor = System.Drawing.Color.Gainsboro;
            this.lblPioUpperStepRecovery.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPioUpperStepRecovery.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblPioUpperStepRecovery.ForeColor = System.Drawing.Color.Red;
            this.lblPioUpperStepRecovery.Location = new System.Drawing.Point(426, 18);
            this.lblPioUpperStepRecovery.Name = "lblPioUpperStepRecovery";
            this.lblPioUpperStepRecovery.Size = new System.Drawing.Size(37, 22);
            this.lblPioUpperStepRecovery.TabIndex = 122;
            this.lblPioUpperStepRecovery.Text = "-";
            this.lblPioUpperStepRecovery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label49
            // 
            this.label49.AutoEllipsis = true;
            this.label49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.label49.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label49.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label49.ForeColor = System.Drawing.Color.Black;
            this.label49.Location = new System.Drawing.Point(3, 40);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(91, 22);
            this.label49.TabIndex = 116;
            this.label49.Text = "LPM2 PIO";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPioLPM1OHTStep
            // 
            this.lblPioLPM1OHTStep.AutoEllipsis = true;
            this.lblPioLPM1OHTStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lblPioLPM1OHTStep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPioLPM1OHTStep.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblPioLPM1OHTStep.ForeColor = System.Drawing.Color.Red;
            this.lblPioLPM1OHTStep.Location = new System.Drawing.Point(95, 18);
            this.lblPioLPM1OHTStep.Name = "lblPioLPM1OHTStep";
            this.lblPioLPM1OHTStep.Size = new System.Drawing.Size(330, 22);
            this.lblPioLPM1OHTStep.TabIndex = 115;
            this.lblPioLPM1OHTStep.Text = "-";
            this.lblPioLPM1OHTStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label52
            // 
            this.label52.AutoEllipsis = true;
            this.label52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.label52.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label52.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label52.ForeColor = System.Drawing.Color.Black;
            this.label52.Location = new System.Drawing.Point(3, 18);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(91, 22);
            this.label52.TabIndex = 114;
            this.label52.Text = "LPM1 PIO";
            this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPioLPM2OHTStep
            // 
            this.lblPioLPM2OHTStep.AutoEllipsis = true;
            this.lblPioLPM2OHTStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lblPioLPM2OHTStep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPioLPM2OHTStep.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblPioLPM2OHTStep.ForeColor = System.Drawing.Color.Red;
            this.lblPioLPM2OHTStep.Location = new System.Drawing.Point(95, 40);
            this.lblPioLPM2OHTStep.Name = "lblPioLPM2OHTStep";
            this.lblPioLPM2OHTStep.Size = new System.Drawing.Size(330, 22);
            this.lblPioLPM2OHTStep.TabIndex = 117;
            this.lblPioLPM2OHTStep.Text = "-";
            this.lblPioLPM2OHTStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label95
            // 
            this.label95.AutoEllipsis = true;
            this.label95.BackColor = System.Drawing.Color.Gainsboro;
            this.label95.Dock = System.Windows.Forms.DockStyle.Top;
            this.label95.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label95.ForeColor = System.Drawing.Color.Black;
            this.label95.Location = new System.Drawing.Point(0, 0);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(463, 14);
            this.label95.TabIndex = 9;
            this.label95.Text = "■ OHT PIO";
            this.label95.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label41);
            this.panel6.Controls.Add(this.label27);
            this.panel6.Controls.Add(this.label38);
            this.panel6.Controls.Add(this.lblRobotStep);
            this.panel6.Controls.Add(this.lblAlignerStep);
            this.panel6.Controls.Add(this.label33);
            this.panel6.Controls.Add(this.lblLpm1Step);
            this.panel6.Controls.Add(this.label36);
            this.panel6.Controls.Add(this.lblLpm2Step);
            this.panel6.Location = new System.Drawing.Point(0, 214);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(465, 110);
            this.panel6.TabIndex = 494;
            // 
            // label41
            // 
            this.label41.AutoEllipsis = true;
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.label41.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label41.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label41.ForeColor = System.Drawing.Color.Black;
            this.label41.Location = new System.Drawing.Point(3, 84);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(91, 22);
            this.label41.TabIndex = 116;
            this.label41.Text = "Aligner";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label27
            // 
            this.label27.AutoEllipsis = true;
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.label27.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label27.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(3, 62);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(91, 22);
            this.label27.TabIndex = 116;
            this.label27.Text = "LoadPort1";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label38
            // 
            this.label38.AutoEllipsis = true;
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.label38.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label38.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(3, 40);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(91, 22);
            this.label38.TabIndex = 114;
            this.label38.Text = "LoadPort2";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRobotStep
            // 
            this.lblRobotStep.AutoEllipsis = true;
            this.lblRobotStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lblRobotStep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRobotStep.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblRobotStep.ForeColor = System.Drawing.Color.Red;
            this.lblRobotStep.Location = new System.Drawing.Point(95, 18);
            this.lblRobotStep.Name = "lblRobotStep";
            this.lblRobotStep.Size = new System.Drawing.Size(366, 22);
            this.lblRobotStep.TabIndex = 115;
            this.lblRobotStep.Text = "-";
            this.lblRobotStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlignerStep
            // 
            this.lblAlignerStep.AutoEllipsis = true;
            this.lblAlignerStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lblAlignerStep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignerStep.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblAlignerStep.ForeColor = System.Drawing.Color.Red;
            this.lblAlignerStep.Location = new System.Drawing.Point(95, 84);
            this.lblAlignerStep.Name = "lblAlignerStep";
            this.lblAlignerStep.Size = new System.Drawing.Size(366, 22);
            this.lblAlignerStep.TabIndex = 117;
            this.lblAlignerStep.Text = "-";
            this.lblAlignerStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label33
            // 
            this.label33.AutoEllipsis = true;
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.label33.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label33.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label33.ForeColor = System.Drawing.Color.Black;
            this.label33.Location = new System.Drawing.Point(3, 18);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(91, 22);
            this.label33.TabIndex = 114;
            this.label33.Text = "Robot";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLpm1Step
            // 
            this.lblLpm1Step.AutoEllipsis = true;
            this.lblLpm1Step.BackColor = System.Drawing.Color.Gainsboro;
            this.lblLpm1Step.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLpm1Step.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblLpm1Step.ForeColor = System.Drawing.Color.Red;
            this.lblLpm1Step.Location = new System.Drawing.Point(95, 62);
            this.lblLpm1Step.Name = "lblLpm1Step";
            this.lblLpm1Step.Size = new System.Drawing.Size(366, 22);
            this.lblLpm1Step.TabIndex = 117;
            this.lblLpm1Step.Text = "-";
            this.lblLpm1Step.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label36
            // 
            this.label36.AutoEllipsis = true;
            this.label36.BackColor = System.Drawing.Color.Gainsboro;
            this.label36.Dock = System.Windows.Forms.DockStyle.Top;
            this.label36.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label36.ForeColor = System.Drawing.Color.Black;
            this.label36.Location = new System.Drawing.Point(0, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(463, 14);
            this.label36.TabIndex = 9;
            this.label36.Text = "■ EFEM Step Monitor";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLpm2Step
            // 
            this.lblLpm2Step.AutoEllipsis = true;
            this.lblLpm2Step.BackColor = System.Drawing.Color.Gainsboro;
            this.lblLpm2Step.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLpm2Step.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblLpm2Step.ForeColor = System.Drawing.Color.Red;
            this.lblLpm2Step.Location = new System.Drawing.Point(95, 40);
            this.lblLpm2Step.Name = "lblLpm2Step";
            this.lblLpm2Step.Size = new System.Drawing.Size(366, 22);
            this.lblLpm2Step.TabIndex = 115;
            this.lblLpm2Step.Text = "-";
            this.lblLpm2Step.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel18
            // 
            this.panel18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel18.Controls.Add(this.lblTTTMTimeLimit);
            this.panel18.Controls.Add(this.label16);
            this.panel18.Controls.Add(this.label12);
            this.panel18.Controls.Add(this.lblTTTMStep);
            this.panel18.Controls.Add(this.lblLongCount);
            this.panel18.Controls.Add(this.lblInspTimeLimit);
            this.panel18.Controls.Add(this.label6);
            this.panel18.Controls.Add(this.lblReviewTimeLimit);
            this.panel18.Controls.Add(this.lblTTTMCurTime);
            this.panel18.Controls.Add(this.lblInspCurTime);
            this.panel18.Controls.Add(this.label29);
            this.panel18.Controls.Add(this.lblReviewCurTime);
            this.panel18.Controls.Add(this.lblScanCount);
            this.panel18.Controls.Add(this.lblScanNum);
            this.panel18.Controls.Add(this.label30);
            this.panel18.Controls.Add(this.lblReviewStep);
            this.panel18.Controls.Add(this.lblMainStep);
            this.panel18.Controls.Add(this.label130);
            this.panel18.Controls.Add(this.label131);
            this.panel18.Controls.Add(this.label46);
            this.panel18.Controls.Add(this.lblHomingStep);
            this.panel18.Controls.Add(this.Loading);
            this.panel18.Controls.Add(this.lblUnloadingStep);
            this.panel18.Controls.Add(this.lblLoadingStep);
            this.panel18.Controls.Add(this.label55);
            this.panel18.Controls.Add(this.label44);
            this.panel18.Controls.Add(this.lblScanningStep);
            this.panel18.Controls.Add(this.label15);
            this.panel18.Location = new System.Drawing.Point(-1, -2);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(471, 222);
            this.panel18.TabIndex = 72;
            // 
            // lblTTTMTimeLimit
            // 
            this.lblTTTMTimeLimit.AutoEllipsis = true;
            this.lblTTTMTimeLimit.BackColor = System.Drawing.Color.Gainsboro;
            this.lblTTTMTimeLimit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTTTMTimeLimit.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblTTTMTimeLimit.ForeColor = System.Drawing.Color.Red;
            this.lblTTTMTimeLimit.Location = new System.Drawing.Point(387, 86);
            this.lblTTTMTimeLimit.Name = "lblTTTMTimeLimit";
            this.lblTTTMTimeLimit.Size = new System.Drawing.Size(76, 21);
            this.lblTTTMTimeLimit.TabIndex = 108;
            this.lblTTTMTimeLimit.Text = "-";
            this.lblTTTMTimeLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(374, 89);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(12, 15);
            this.label16.TabIndex = 107;
            this.label16.Text = "/";
            // 
            // label12
            // 
            this.label12.AutoEllipsis = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(3, 86);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 21);
            this.label12.TabIndex = 105;
            this.label12.Text = "TTTM";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTTTMStep
            // 
            this.lblTTTMStep.AutoEllipsis = true;
            this.lblTTTMStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lblTTTMStep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTTTMStep.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblTTTMStep.ForeColor = System.Drawing.Color.Red;
            this.lblTTTMStep.Location = new System.Drawing.Point(95, 86);
            this.lblTTTMStep.Name = "lblTTTMStep";
            this.lblTTTMStep.Size = new System.Drawing.Size(201, 21);
            this.lblTTTMStep.TabIndex = 106;
            this.lblTTTMStep.Text = "-";
            this.lblTTTMStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLongCount
            // 
            this.lblLongCount.AutoSize = true;
            this.lblLongCount.Location = new System.Drawing.Point(428, -1);
            this.lblLongCount.Name = "lblLongCount";
            this.lblLongCount.Size = new System.Drawing.Size(35, 15);
            this.lblLongCount.TabIndex = 101;
            this.lblLongCount.Text = "0000";
            this.lblLongCount.Visible = false;
            // 
            // lblInspTimeLimit
            // 
            this.lblInspTimeLimit.AutoEllipsis = true;
            this.lblInspTimeLimit.BackColor = System.Drawing.Color.Gainsboro;
            this.lblInspTimeLimit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInspTimeLimit.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblInspTimeLimit.ForeColor = System.Drawing.Color.Red;
            this.lblInspTimeLimit.Location = new System.Drawing.Point(292, 128);
            this.lblInspTimeLimit.Name = "lblInspTimeLimit";
            this.lblInspTimeLimit.Size = new System.Drawing.Size(171, 21);
            this.lblInspTimeLimit.TabIndex = 104;
            this.lblInspTimeLimit.Text = "-";
            this.lblInspTimeLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(273, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 15);
            this.label6.TabIndex = 103;
            this.label6.Text = "/";
            // 
            // lblReviewTimeLimit
            // 
            this.lblReviewTimeLimit.AutoEllipsis = true;
            this.lblReviewTimeLimit.BackColor = System.Drawing.Color.Gainsboro;
            this.lblReviewTimeLimit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblReviewTimeLimit.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblReviewTimeLimit.ForeColor = System.Drawing.Color.Red;
            this.lblReviewTimeLimit.Location = new System.Drawing.Point(292, 171);
            this.lblReviewTimeLimit.Name = "lblReviewTimeLimit";
            this.lblReviewTimeLimit.Size = new System.Drawing.Size(171, 21);
            this.lblReviewTimeLimit.TabIndex = 104;
            this.lblReviewTimeLimit.Text = "-";
            this.lblReviewTimeLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTTTMCurTime
            // 
            this.lblTTTMCurTime.AutoEllipsis = true;
            this.lblTTTMCurTime.BackColor = System.Drawing.Color.Gainsboro;
            this.lblTTTMCurTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTTTMCurTime.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblTTTMCurTime.ForeColor = System.Drawing.Color.Red;
            this.lblTTTMCurTime.Location = new System.Drawing.Point(296, 86);
            this.lblTTTMCurTime.Name = "lblTTTMCurTime";
            this.lblTTTMCurTime.Size = new System.Drawing.Size(76, 21);
            this.lblTTTMCurTime.TabIndex = 102;
            this.lblTTTMCurTime.Text = "-";
            this.lblTTTMCurTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspCurTime
            // 
            this.lblInspCurTime.AutoEllipsis = true;
            this.lblInspCurTime.BackColor = System.Drawing.Color.Gainsboro;
            this.lblInspCurTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInspCurTime.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblInspCurTime.ForeColor = System.Drawing.Color.Red;
            this.lblInspCurTime.Location = new System.Drawing.Point(95, 128);
            this.lblInspCurTime.Name = "lblInspCurTime";
            this.lblInspCurTime.Size = new System.Drawing.Size(171, 21);
            this.lblInspCurTime.TabIndex = 102;
            this.lblInspCurTime.Text = "-";
            this.lblInspCurTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(273, 175);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(12, 15);
            this.label29.TabIndex = 103;
            this.label29.Text = "/";
            // 
            // lblReviewCurTime
            // 
            this.lblReviewCurTime.AutoEllipsis = true;
            this.lblReviewCurTime.BackColor = System.Drawing.Color.Gainsboro;
            this.lblReviewCurTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblReviewCurTime.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblReviewCurTime.ForeColor = System.Drawing.Color.Red;
            this.lblReviewCurTime.Location = new System.Drawing.Point(95, 171);
            this.lblReviewCurTime.Name = "lblReviewCurTime";
            this.lblReviewCurTime.Size = new System.Drawing.Size(171, 21);
            this.lblReviewCurTime.TabIndex = 102;
            this.lblReviewCurTime.Text = "-";
            this.lblReviewCurTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScanCount
            // 
            this.lblScanCount.AutoEllipsis = true;
            this.lblScanCount.BackColor = System.Drawing.Color.Gainsboro;
            this.lblScanCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblScanCount.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblScanCount.ForeColor = System.Drawing.Color.Red;
            this.lblScanCount.Location = new System.Drawing.Point(433, 107);
            this.lblScanCount.Name = "lblScanCount";
            this.lblScanCount.Size = new System.Drawing.Size(30, 21);
            this.lblScanCount.TabIndex = 85;
            this.lblScanCount.Text = "-";
            this.lblScanCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblScanCount.Visible = false;
            // 
            // lblScanNum
            // 
            this.lblScanNum.AutoEllipsis = true;
            this.lblScanNum.BackColor = System.Drawing.Color.Gainsboro;
            this.lblScanNum.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblScanNum.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblScanNum.ForeColor = System.Drawing.Color.Red;
            this.lblScanNum.Location = new System.Drawing.Point(394, 107);
            this.lblScanNum.Name = "lblScanNum";
            this.lblScanNum.Size = new System.Drawing.Size(30, 21);
            this.lblScanNum.TabIndex = 85;
            this.lblScanNum.Text = "-";
            this.lblScanNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblScanNum.Visible = false;
            // 
            // label30
            // 
            this.label30.AutoEllipsis = true;
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.label30.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label30.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label30.ForeColor = System.Drawing.Color.Black;
            this.label30.Location = new System.Drawing.Point(3, 150);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(91, 42);
            this.label30.TabIndex = 81;
            this.label30.Text = "Review";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReviewStep
            // 
            this.lblReviewStep.AutoEllipsis = true;
            this.lblReviewStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lblReviewStep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblReviewStep.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblReviewStep.ForeColor = System.Drawing.Color.Red;
            this.lblReviewStep.Location = new System.Drawing.Point(95, 150);
            this.lblReviewStep.Name = "lblReviewStep";
            this.lblReviewStep.Size = new System.Drawing.Size(368, 21);
            this.lblReviewStep.TabIndex = 83;
            this.lblReviewStep.Text = "-";
            this.lblReviewStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMainStep
            // 
            this.lblMainStep.AutoEllipsis = true;
            this.lblMainStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lblMainStep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMainStep.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblMainStep.ForeColor = System.Drawing.Color.Red;
            this.lblMainStep.Location = new System.Drawing.Point(95, 21);
            this.lblMainStep.Name = "lblMainStep";
            this.lblMainStep.Size = new System.Drawing.Size(368, 21);
            this.lblMainStep.TabIndex = 80;
            this.lblMainStep.Text = "-";
            this.lblMainStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label130
            // 
            this.label130.AutoEllipsis = true;
            this.label130.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.label130.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label130.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label130.ForeColor = System.Drawing.Color.Black;
            this.label130.Location = new System.Drawing.Point(3, 21);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(91, 21);
            this.label130.TabIndex = 77;
            this.label130.Text = "Main";
            this.label130.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label131
            // 
            this.label131.AutoEllipsis = true;
            this.label131.BackColor = System.Drawing.Color.Gainsboro;
            this.label131.Dock = System.Windows.Forms.DockStyle.Top;
            this.label131.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label131.ForeColor = System.Drawing.Color.Black;
            this.label131.Location = new System.Drawing.Point(0, 0);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(469, 16);
            this.label131.TabIndex = 9;
            this.label131.Text = "■ AVI Step Monitor";
            this.label131.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label46
            // 
            this.label46.AutoEllipsis = true;
            this.label46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.label46.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label46.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label46.ForeColor = System.Drawing.Color.Black;
            this.label46.Location = new System.Drawing.Point(3, 43);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(91, 21);
            this.label46.TabIndex = 10;
            this.label46.Text = "Home";
            this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHomingStep
            // 
            this.lblHomingStep.AutoEllipsis = true;
            this.lblHomingStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lblHomingStep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHomingStep.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblHomingStep.ForeColor = System.Drawing.Color.Red;
            this.lblHomingStep.Location = new System.Drawing.Point(95, 43);
            this.lblHomingStep.Name = "lblHomingStep";
            this.lblHomingStep.Size = new System.Drawing.Size(368, 21);
            this.lblHomingStep.TabIndex = 57;
            this.lblHomingStep.Text = "-";
            this.lblHomingStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Loading
            // 
            this.Loading.AutoEllipsis = true;
            this.Loading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.Loading.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Loading.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Loading.ForeColor = System.Drawing.Color.Black;
            this.Loading.Location = new System.Drawing.Point(3, 65);
            this.Loading.Name = "Loading";
            this.Loading.Size = new System.Drawing.Size(91, 21);
            this.Loading.TabIndex = 58;
            this.Loading.Text = "Loading";
            this.Loading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUnloadingStep
            // 
            this.lblUnloadingStep.AutoEllipsis = true;
            this.lblUnloadingStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lblUnloadingStep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUnloadingStep.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblUnloadingStep.ForeColor = System.Drawing.Color.Red;
            this.lblUnloadingStep.Location = new System.Drawing.Point(95, 193);
            this.lblUnloadingStep.Name = "lblUnloadingStep";
            this.lblUnloadingStep.Size = new System.Drawing.Size(368, 21);
            this.lblUnloadingStep.TabIndex = 66;
            this.lblUnloadingStep.Text = "-";
            this.lblUnloadingStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoadingStep
            // 
            this.lblLoadingStep.AutoEllipsis = true;
            this.lblLoadingStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lblLoadingStep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLoadingStep.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoadingStep.ForeColor = System.Drawing.Color.Red;
            this.lblLoadingStep.Location = new System.Drawing.Point(95, 65);
            this.lblLoadingStep.Name = "lblLoadingStep";
            this.lblLoadingStep.Size = new System.Drawing.Size(368, 21);
            this.lblLoadingStep.TabIndex = 60;
            this.lblLoadingStep.Text = "-";
            this.lblLoadingStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label55
            // 
            this.label55.AutoEllipsis = true;
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.label55.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label55.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label55.ForeColor = System.Drawing.Color.Black;
            this.label55.Location = new System.Drawing.Point(3, 193);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(91, 21);
            this.label55.TabIndex = 64;
            this.label55.Text = "Unloading";
            this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label44
            // 
            this.label44.AutoEllipsis = true;
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.label44.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label44.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label44.ForeColor = System.Drawing.Color.Black;
            this.label44.Location = new System.Drawing.Point(3, 107);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(91, 42);
            this.label44.TabIndex = 61;
            this.label44.Text = "Scan";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScanningStep
            // 
            this.lblScanningStep.AutoEllipsis = true;
            this.lblScanningStep.BackColor = System.Drawing.Color.Gainsboro;
            this.lblScanningStep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblScanningStep.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblScanningStep.ForeColor = System.Drawing.Color.Red;
            this.lblScanningStep.Location = new System.Drawing.Point(95, 107);
            this.lblScanningStep.Name = "lblScanningStep";
            this.lblScanningStep.Size = new System.Drawing.Size(368, 21);
            this.lblScanningStep.TabIndex = 63;
            this.lblScanningStep.Text = "-";
            this.lblScanningStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(422, 109);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(12, 15);
            this.label15.TabIndex = 101;
            this.label15.Text = "/";
            this.label15.Visible = false;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.lblEFEM);
            this.panel5.Controls.Add(this.lblSyncSvr);
            this.panel5.Controls.Add(this.lblPMAC);
            this.panel5.Controls.Add(this.lblInspector);
            this.panel5.Controls.Add(this.label90);
            this.panel5.Controls.Add(this.lblEFUStat);
            this.panel5.Controls.Add(this.lblBCRStat);
            this.panel5.Controls.Add(this.lblOCRStat);
            this.panel5.Controls.Add(this.lblRFID2Stat);
            this.panel5.Controls.Add(this.lblRFID1Stat);
            this.panel5.Location = new System.Drawing.Point(4, 1015);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(478, 24);
            this.panel5.TabIndex = 73;
            // 
            // lblEFEM
            // 
            this.lblEFEM.AutoEllipsis = true;
            this.lblEFEM.BackColor = System.Drawing.Color.White;
            this.lblEFEM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEFEM.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblEFEM.ForeColor = System.Drawing.Color.Black;
            this.lblEFEM.Location = new System.Drawing.Point(181, -1);
            this.lblEFEM.Name = "lblEFEM";
            this.lblEFEM.Size = new System.Drawing.Size(41, 21);
            this.lblEFEM.TabIndex = 69;
            this.lblEFEM.Text = "EFEM";
            this.lblEFEM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEFEM.DoubleClick += new System.EventHandler(this.lblEFEM_DoubleClick);
            // 
            // lblSyncSvr
            // 
            this.lblSyncSvr.BackColor = System.Drawing.Color.White;
            this.lblSyncSvr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSyncSvr.Delay = 2;
            this.lblSyncSvr.Flicker = false;
            this.lblSyncSvr.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblSyncSvr.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblSyncSvr.Location = new System.Drawing.Point(422, -1);
            this.lblSyncSvr.Name = "lblSyncSvr";
            this.lblSyncSvr.OffColor = System.Drawing.Color.White;
            this.lblSyncSvr.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblSyncSvr.OnOff = false;
            this.lblSyncSvr.Size = new System.Drawing.Size(51, 21);
            this.lblSyncSvr.TabIndex = 68;
            this.lblSyncSvr.Text = "SYNC";
            this.lblSyncSvr.Text2 = "";
            this.lblSyncSvr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPMAC
            // 
            this.lblPMAC.AutoEllipsis = true;
            this.lblPMAC.BackColor = System.Drawing.Color.White;
            this.lblPMAC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPMAC.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblPMAC.ForeColor = System.Drawing.Color.Black;
            this.lblPMAC.Location = new System.Drawing.Point(129, -1);
            this.lblPMAC.Name = "lblPMAC";
            this.lblPMAC.Size = new System.Drawing.Size(51, 21);
            this.lblPMAC.TabIndex = 65;
            this.lblPMAC.Text = "MOTOR";
            this.lblPMAC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPMAC.DoubleClick += new System.EventHandler(this.btnPmacRestart_DoubleClick);
            // 
            // lblInspector
            // 
            this.lblInspector.AutoEllipsis = true;
            this.lblInspector.BackColor = System.Drawing.Color.White;
            this.lblInspector.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInspector.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblInspector.ForeColor = System.Drawing.Color.Black;
            this.lblInspector.Location = new System.Drawing.Point(78, -1);
            this.lblInspector.Name = "lblInspector";
            this.lblInspector.Size = new System.Drawing.Size(51, 21);
            this.lblInspector.TabIndex = 63;
            this.lblInspector.Text = "ISERVER";
            this.lblInspector.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label90
            // 
            this.label90.AutoEllipsis = true;
            this.label90.BackColor = System.Drawing.Color.Gainsboro;
            this.label90.Dock = System.Windows.Forms.DockStyle.Left;
            this.label90.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label90.ForeColor = System.Drawing.Color.Black;
            this.label90.Location = new System.Drawing.Point(0, 0);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(78, 22);
            this.label90.TabIndex = 9;
            this.label90.Text = "■ Interface";
            this.label90.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEFUStat
            // 
            this.lblEFUStat.AutoEllipsis = true;
            this.lblEFUStat.BackColor = System.Drawing.Color.White;
            this.lblEFUStat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEFUStat.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblEFUStat.ForeColor = System.Drawing.Color.Black;
            this.lblEFUStat.Location = new System.Drawing.Point(382, -1);
            this.lblEFUStat.Name = "lblEFUStat";
            this.lblEFUStat.Size = new System.Drawing.Size(40, 21);
            this.lblEFUStat.TabIndex = 69;
            this.lblEFUStat.Text = "EFU";
            this.lblEFUStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEFUStat.DoubleClick += new System.EventHandler(this.lblEFUStat_DoubleClick);
            // 
            // lblBCRStat
            // 
            this.lblBCRStat.AutoEllipsis = true;
            this.lblBCRStat.BackColor = System.Drawing.Color.White;
            this.lblBCRStat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBCRStat.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblBCRStat.ForeColor = System.Drawing.Color.Black;
            this.lblBCRStat.Location = new System.Drawing.Point(344, -1);
            this.lblBCRStat.Name = "lblBCRStat";
            this.lblBCRStat.Size = new System.Drawing.Size(40, 21);
            this.lblBCRStat.TabIndex = 69;
            this.lblBCRStat.Text = "BCR";
            this.lblBCRStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBCRStat.DoubleClick += new System.EventHandler(this.lblBCRStat_DoubleClick);
            // 
            // lblOCRStat
            // 
            this.lblOCRStat.AutoEllipsis = true;
            this.lblOCRStat.BackColor = System.Drawing.Color.White;
            this.lblOCRStat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOCRStat.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblOCRStat.ForeColor = System.Drawing.Color.Black;
            this.lblOCRStat.Location = new System.Drawing.Point(303, -1);
            this.lblOCRStat.Name = "lblOCRStat";
            this.lblOCRStat.Size = new System.Drawing.Size(40, 21);
            this.lblOCRStat.TabIndex = 69;
            this.lblOCRStat.Text = "OCR";
            this.lblOCRStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOCRStat.DoubleClick += new System.EventHandler(this.lblOCRStat_DoubleClick);
            // 
            // lblRFID2Stat
            // 
            this.lblRFID2Stat.AutoEllipsis = true;
            this.lblRFID2Stat.BackColor = System.Drawing.Color.White;
            this.lblRFID2Stat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRFID2Stat.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblRFID2Stat.ForeColor = System.Drawing.Color.Black;
            this.lblRFID2Stat.Location = new System.Drawing.Point(263, -1);
            this.lblRFID2Stat.Name = "lblRFID2Stat";
            this.lblRFID2Stat.Size = new System.Drawing.Size(40, 21);
            this.lblRFID2Stat.TabIndex = 69;
            this.lblRFID2Stat.Text = "RFID2";
            this.lblRFID2Stat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRFID2Stat.DoubleClick += new System.EventHandler(this.lblRFIDStat_DoubleClick);
            // 
            // lblRFID1Stat
            // 
            this.lblRFID1Stat.AutoEllipsis = true;
            this.lblRFID1Stat.BackColor = System.Drawing.Color.White;
            this.lblRFID1Stat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRFID1Stat.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblRFID1Stat.ForeColor = System.Drawing.Color.Black;
            this.lblRFID1Stat.Location = new System.Drawing.Point(222, -1);
            this.lblRFID1Stat.Name = "lblRFID1Stat";
            this.lblRFID1Stat.Size = new System.Drawing.Size(40, 21);
            this.lblRFID1Stat.TabIndex = 69;
            this.lblRFID1Stat.Text = "RFID1";
            this.lblRFID1Stat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRFID1Stat.DoubleClick += new System.EventHandler(this.lblRFIDStat_DoubleClick);
            // 
            // labelWatch
            // 
            this.labelWatch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.labelWatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelWatch.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.labelWatch.Location = new System.Drawing.Point(330, 60);
            this.labelWatch.Margin = new System.Windows.Forms.Padding(1);
            this.labelWatch.Name = "labelWatch";
            this.labelWatch.Size = new System.Drawing.Size(43, 36);
            this.labelWatch.TabIndex = 3;
            this.labelWatch.Text = "스캔 타임";
            this.labelWatch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEquipState
            // 
            this.lblEquipState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.lblEquipState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEquipState.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblEquipState.Location = new System.Drawing.Point(198, 60);
            this.lblEquipState.Margin = new System.Windows.Forms.Padding(1);
            this.lblEquipState.Name = "lblEquipState";
            this.lblEquipState.Size = new System.Drawing.Size(65, 36);
            this.lblEquipState.TabIndex = 3;
            this.lblEquipState.Text = "-";
            this.lblEquipState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProcState
            // 
            this.lblProcState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.lblProcState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProcState.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblProcState.Location = new System.Drawing.Point(264, 60);
            this.lblProcState.Margin = new System.Windows.Forms.Padding(1);
            this.lblProcState.Name = "lblProcState";
            this.lblProcState.Size = new System.Drawing.Size(65, 36);
            this.lblProcState.TabIndex = 106;
            this.lblProcState.Text = "-";
            this.lblProcState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrScanTime
            // 
            this.lblCurrScanTime.BackColor = System.Drawing.Color.White;
            this.lblCurrScanTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrScanTime.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblCurrScanTime.Location = new System.Drawing.Point(375, 60);
            this.lblCurrScanTime.Margin = new System.Windows.Forms.Padding(1);
            this.lblCurrScanTime.Name = "lblCurrScanTime";
            this.lblCurrScanTime.Size = new System.Drawing.Size(51, 36);
            this.lblCurrScanTime.TabIndex = 3;
            this.lblCurrScanTime.Text = "0";
            this.lblCurrScanTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.Delay = 500;
            this.btnExit.Flicker = false;
            this.btnExit.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnExit.ForeColor = System.Drawing.Color.Red;
            this.btnExit.IsLeftLampOn = false;
            this.btnExit.IsRightLampOn = false;
            this.btnExit.LampAliveTime = 500;
            this.btnExit.LampSize = 1;
            this.btnExit.LeftLampColor = System.Drawing.Color.Red;
            this.btnExit.Location = new System.Drawing.Point(428, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.OnOff = false;
            this.btnExit.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnExit.Size = new System.Drawing.Size(51, 53);
            this.btnExit.TabIndex = 441;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "종료";
            this.btnExit.Text2 = "";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.VisibleLeftLamp = false;
            this.btnExit.VisibleRightLamp = false;
            this.btnExit.DelayClick += new System.EventHandler(this.btnExit_DelayClick);
            // 
            // lblRvRun
            // 
            this.lblRvRun.BackColor = System.Drawing.Color.Gainsboro;
            this.lblRvRun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRvRun.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblRvRun.Location = new System.Drawing.Point(329, 5);
            this.lblRvRun.Margin = new System.Windows.Forms.Padding(1);
            this.lblRvRun.Name = "lblRvRun";
            this.lblRvRun.Size = new System.Drawing.Size(118, 18);
            this.lblRvRun.TabIndex = 443;
            this.lblRvRun.Text = "Review Run";
            this.lblRvRun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOuterMode
            // 
            this.lblOuterMode.BackColor = System.Drawing.Color.Gainsboro;
            this.lblOuterMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOuterMode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOuterMode.ForeColor = System.Drawing.Color.Black;
            this.lblOuterMode.Location = new System.Drawing.Point(4, 5);
            this.lblOuterMode.Margin = new System.Windows.Forms.Padding(1);
            this.lblOuterMode.Name = "lblOuterMode";
            this.lblOuterMode.Size = new System.Drawing.Size(100, 18);
            this.lblOuterMode.TabIndex = 1;
            this.lblOuterMode.Text = "Auto";
            this.lblOuterMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAvgScanTime
            // 
            this.lblAvgScanTime.BackColor = System.Drawing.Color.White;
            this.lblAvgScanTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAvgScanTime.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblAvgScanTime.Location = new System.Drawing.Point(428, 79);
            this.lblAvgScanTime.Margin = new System.Windows.Forms.Padding(1);
            this.lblAvgScanTime.Name = "lblAvgScanTime";
            this.lblAvgScanTime.Size = new System.Drawing.Size(51, 17);
            this.lblAvgScanTime.TabIndex = 3;
            this.lblAvgScanTime.Text = "0";
            this.lblAvgScanTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReviewManual
            // 
            this.lblReviewManual.BackColor = System.Drawing.Color.Gainsboro;
            this.lblReviewManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblReviewManual.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblReviewManual.Location = new System.Drawing.Point(329, 24);
            this.lblReviewManual.Margin = new System.Windows.Forms.Padding(1);
            this.lblReviewManual.Name = "lblReviewManual";
            this.lblReviewManual.Size = new System.Drawing.Size(118, 18);
            this.lblReviewManual.TabIndex = 443;
            this.lblReviewManual.Text = "Review Manual";
            this.lblReviewManual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAutoCondition
            // 
            this.lblAutoCondition.BackColor = System.Drawing.Color.Gainsboro;
            this.lblAutoCondition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAutoCondition.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutoCondition.Location = new System.Drawing.Point(4, 24);
            this.lblAutoCondition.Margin = new System.Windows.Forms.Padding(1);
            this.lblAutoCondition.Name = "lblAutoCondition";
            this.lblAutoCondition.Size = new System.Drawing.Size(202, 18);
            this.lblAutoCondition.TabIndex = 445;
            this.lblAutoCondition.Text = "내부Key Teach로";
            this.lblAutoCondition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabOperator
            // 
            this.tabOperator.Controls.Add(this.tabPageOperator);
            this.tabOperator.Controls.Add(this.tabPageEngineer);
            this.tabOperator.ItemSize = new System.Drawing.Size(195, 25);
            this.tabOperator.Location = new System.Drawing.Point(4, 266);
            this.tabOperator.Margin = new System.Windows.Forms.Padding(0);
            this.tabOperator.Multiline = true;
            this.tabOperator.Name = "tabOperator";
            this.tabOperator.SelectedIndex = 0;
            this.tabOperator.Size = new System.Drawing.Size(478, 256);
            this.tabOperator.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabOperator.TabIndex = 104;
            this.tabOperator.SelectedIndexChanged += new System.EventHandler(this.tabOperator_SelectedIndexChanged);
            // 
            // tabPageOperator
            // 
            this.tabPageOperator.BackColor = System.Drawing.SystemColors.MenuBar;
            this.tabPageOperator.Controls.Add(this.button1);
            this.tabPageOperator.Controls.Add(this.lblMotorLongRun);
            this.tabPageOperator.Controls.Add(this.btnLogPath);
            this.tabPageOperator.Controls.Add(this.btnDeleteAllInfo);
            this.tabPageOperator.Controls.Add(this.btnMotorLongRun);
            this.tabPageOperator.Controls.Add(this.btnEfemDoorOpen);
            this.tabPageOperator.Controls.Add(this.panel21);
            this.tabPageOperator.Location = new System.Drawing.Point(4, 29);
            this.tabPageOperator.Name = "tabPageOperator";
            this.tabPageOperator.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOperator.Size = new System.Drawing.Size(470, 223);
            this.tabPageOperator.TabIndex = 0;
            this.tabPageOperator.Text = "Operator";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(304, 147);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 60);
            this.button1.TabIndex = 482;
            this.button1.Text = "ECID TEST";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lblMotorLongRun
            // 
            this.lblMotorLongRun.AutoSize = true;
            this.lblMotorLongRun.Location = new System.Drawing.Point(197, 147);
            this.lblMotorLongRun.Name = "lblMotorLongRun";
            this.lblMotorLongRun.Size = new System.Drawing.Size(46, 15);
            this.lblMotorLongRun.TabIndex = 481;
            this.lblMotorLongRun.Text = "label53";
            this.lblMotorLongRun.Visible = false;
            // 
            // btnLogPath
            // 
            this.btnLogPath.Location = new System.Drawing.Point(304, 72);
            this.btnLogPath.Name = "btnLogPath";
            this.btnLogPath.Size = new System.Drawing.Size(160, 60);
            this.btnLogPath.TabIndex = 480;
            this.btnLogPath.Text = "카세트 IN OUT 로그";
            this.btnLogPath.UseVisualStyleBackColor = true;
            this.btnLogPath.Click += new System.EventHandler(this.btnLogPath_Click);
            // 
            // btnMotorLongRun
            // 
            this.btnMotorLongRun.BackColor = System.Drawing.Color.Transparent;
            this.btnMotorLongRun.Delay = 2;
            this.btnMotorLongRun.Flicker = false;
            this.btnMotorLongRun.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnMotorLongRun.ForeColor = System.Drawing.Color.Black;
            this.btnMotorLongRun.IsLeftLampOn = false;
            this.btnMotorLongRun.IsRightLampOn = false;
            this.btnMotorLongRun.LampAliveTime = 500;
            this.btnMotorLongRun.LampSize = 1;
            this.btnMotorLongRun.LeftLampColor = System.Drawing.Color.Red;
            this.btnMotorLongRun.Location = new System.Drawing.Point(158, 72);
            this.btnMotorLongRun.Name = "btnMotorLongRun";
            this.btnMotorLongRun.OnOff = false;
            this.btnMotorLongRun.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnMotorLongRun.Size = new System.Drawing.Size(141, 60);
            this.btnMotorLongRun.TabIndex = 449;
            this.btnMotorLongRun.TabStop = false;
            this.btnMotorLongRun.Text = "Motor Long Run Start/Stop";
            this.btnMotorLongRun.Text2 = "";
            this.btnMotorLongRun.UseVisualStyleBackColor = false;
            this.btnMotorLongRun.Visible = false;
            this.btnMotorLongRun.VisibleLeftLamp = false;
            this.btnMotorLongRun.VisibleRightLamp = false;
            this.btnMotorLongRun.Click += new System.EventHandler(this.btnMotorLongRun_Click);
            // 
            // btnEfemDoorOpen
            // 
            this.btnEfemDoorOpen.BackColor = System.Drawing.Color.Transparent;
            this.btnEfemDoorOpen.Delay = 2;
            this.btnEfemDoorOpen.Flicker = false;
            this.btnEfemDoorOpen.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnEfemDoorOpen.ForeColor = System.Drawing.Color.Black;
            this.btnEfemDoorOpen.IsLeftLampOn = false;
            this.btnEfemDoorOpen.IsRightLampOn = false;
            this.btnEfemDoorOpen.LampAliveTime = 500;
            this.btnEfemDoorOpen.LampSize = 1;
            this.btnEfemDoorOpen.LeftLampColor = System.Drawing.Color.Red;
            this.btnEfemDoorOpen.Location = new System.Drawing.Point(158, 6);
            this.btnEfemDoorOpen.Name = "btnEfemDoorOpen";
            this.btnEfemDoorOpen.OnOff = false;
            this.btnEfemDoorOpen.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnEfemDoorOpen.Size = new System.Drawing.Size(141, 60);
            this.btnEfemDoorOpen.TabIndex = 449;
            this.btnEfemDoorOpen.TabStop = false;
            this.btnEfemDoorOpen.Text = "EFEM 도어 OPEN\r\n가능 상태";
            this.btnEfemDoorOpen.Text2 = "";
            this.btnEfemDoorOpen.UseVisualStyleBackColor = false;
            this.btnEfemDoorOpen.VisibleLeftLamp = false;
            this.btnEfemDoorOpen.VisibleRightLamp = false;
            // 
            // panel21
            // 
            this.panel21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel21.Controls.Add(this.btnReviewSkipCopy);
            this.panel21.Controls.Add(this.btnReviewManual_Copy);
            this.panel21.Controls.Add(this.label34);
            this.panel21.Location = new System.Drawing.Point(4, 6);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(149, 156);
            this.panel21.TabIndex = 453;
            // 
            // btnReviewSkipCopy
            // 
            this.btnReviewSkipCopy.BackColor = System.Drawing.Color.Transparent;
            this.btnReviewSkipCopy.Delay = 2;
            this.btnReviewSkipCopy.Flicker = false;
            this.btnReviewSkipCopy.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnReviewSkipCopy.ForeColor = System.Drawing.Color.Black;
            this.btnReviewSkipCopy.IsLeftLampOn = false;
            this.btnReviewSkipCopy.IsRightLampOn = false;
            this.btnReviewSkipCopy.LampAliveTime = 500;
            this.btnReviewSkipCopy.LampSize = 1;
            this.btnReviewSkipCopy.LeftLampColor = System.Drawing.Color.Red;
            this.btnReviewSkipCopy.Location = new System.Drawing.Point(3, 90);
            this.btnReviewSkipCopy.Name = "btnReviewSkipCopy";
            this.btnReviewSkipCopy.OnOff = false;
            this.btnReviewSkipCopy.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnReviewSkipCopy.Size = new System.Drawing.Size(141, 60);
            this.btnReviewSkipCopy.TabIndex = 449;
            this.btnReviewSkipCopy.TabStop = false;
            this.btnReviewSkipCopy.Text = "리뷰 스킵";
            this.btnReviewSkipCopy.Text2 = "";
            this.btnReviewSkipCopy.UseVisualStyleBackColor = false;
            this.btnReviewSkipCopy.VisibleLeftLamp = false;
            this.btnReviewSkipCopy.VisibleRightLamp = false;
            this.btnReviewSkipCopy.Click += new System.EventHandler(this.btnReviewSkip_Click);
            // 
            // btnReviewManual_Copy
            // 
            this.btnReviewManual_Copy.BackColor = System.Drawing.Color.Transparent;
            this.btnReviewManual_Copy.Delay = 2;
            this.btnReviewManual_Copy.Flicker = false;
            this.btnReviewManual_Copy.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnReviewManual_Copy.ForeColor = System.Drawing.Color.Black;
            this.btnReviewManual_Copy.IsLeftLampOn = false;
            this.btnReviewManual_Copy.IsRightLampOn = false;
            this.btnReviewManual_Copy.LampAliveTime = 500;
            this.btnReviewManual_Copy.LampSize = 1;
            this.btnReviewManual_Copy.LeftLampColor = System.Drawing.Color.Red;
            this.btnReviewManual_Copy.Location = new System.Drawing.Point(3, 27);
            this.btnReviewManual_Copy.Name = "btnReviewManual_Copy";
            this.btnReviewManual_Copy.OnOff = false;
            this.btnReviewManual_Copy.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnReviewManual_Copy.Size = new System.Drawing.Size(141, 60);
            this.btnReviewManual_Copy.TabIndex = 447;
            this.btnReviewManual_Copy.TabStop = false;
            this.btnReviewManual_Copy.Text = "수동 리뷰";
            this.btnReviewManual_Copy.Text2 = "";
            this.btnReviewManual_Copy.UseVisualStyleBackColor = false;
            this.btnReviewManual_Copy.VisibleLeftLamp = false;
            this.btnReviewManual_Copy.VisibleRightLamp = false;
            this.btnReviewManual_Copy.Click += new System.EventHandler(this.btnReviewManual_Click);
            // 
            // label34
            // 
            this.label34.AutoEllipsis = true;
            this.label34.BackColor = System.Drawing.Color.Gainsboro;
            this.label34.Dock = System.Windows.Forms.DockStyle.Top;
            this.label34.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label34.ForeColor = System.Drawing.Color.Black;
            this.label34.Location = new System.Drawing.Point(0, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(147, 24);
            this.label34.TabIndex = 9;
            this.label34.Text = "■ Review";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPageEngineer
            // 
            this.tabPageEngineer.BackColor = System.Drawing.SystemColors.MenuBar;
            this.tabPageEngineer.Controls.Add(this.panel14);
            this.tabPageEngineer.Controls.Add(this.panel11);
            this.tabPageEngineer.Controls.Add(this.panel12);
            this.tabPageEngineer.Controls.Add(this.panel3);
            this.tabPageEngineer.Controls.Add(this.panel4);
            this.tabPageEngineer.Location = new System.Drawing.Point(4, 29);
            this.tabPageEngineer.Name = "tabPageEngineer";
            this.tabPageEngineer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEngineer.Size = new System.Drawing.Size(470, 223);
            this.tabPageEngineer.TabIndex = 1;
            this.tabPageEngineer.Text = "Engineer";
            // 
            // panel14
            // 
            this.panel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel14.Controls.Add(this.btnDoorOpenClose);
            this.panel14.Controls.Add(this.btnWorkingLight);
            this.panel14.Controls.Add(this.label11);
            this.panel14.Controls.Add(this.ButtonDelay21);
            this.panel14.Controls.Add(this.btnInnerWork);
            this.panel14.Location = new System.Drawing.Point(346, 13);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(114, 156);
            this.panel14.TabIndex = 450;
            // 
            // btnDoorOpenClose
            // 
            this.btnDoorOpenClose.BackColor = System.Drawing.Color.Transparent;
            this.btnDoorOpenClose.Delay = 100;
            this.btnDoorOpenClose.Flicker = false;
            this.btnDoorOpenClose.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnDoorOpenClose.ForeColor = System.Drawing.Color.Black;
            this.btnDoorOpenClose.IsLeftLampOn = false;
            this.btnDoorOpenClose.IsRightLampOn = false;
            this.btnDoorOpenClose.LampAliveTime = 500;
            this.btnDoorOpenClose.LampSize = 1;
            this.btnDoorOpenClose.LeftLampColor = System.Drawing.Color.Red;
            this.btnDoorOpenClose.Location = new System.Drawing.Point(5, 119);
            this.btnDoorOpenClose.Name = "btnDoorOpenClose";
            this.btnDoorOpenClose.OnOff = false;
            this.btnDoorOpenClose.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnDoorOpenClose.Size = new System.Drawing.Size(100, 25);
            this.btnDoorOpenClose.TabIndex = 479;
            this.btnDoorOpenClose.TabStop = false;
            this.btnDoorOpenClose.Text = "Door";
            this.btnDoorOpenClose.Text2 = "";
            this.btnDoorOpenClose.UseVisualStyleBackColor = false;
            this.btnDoorOpenClose.VisibleLeftLamp = false;
            this.btnDoorOpenClose.VisibleRightLamp = false;
            this.btnDoorOpenClose.Click += new System.EventHandler(this.btnDoorOpenClose_Click_1);
            // 
            // btnWorkingLight
            // 
            this.btnWorkingLight.BackColor = System.Drawing.Color.Transparent;
            this.btnWorkingLight.Delay = 100;
            this.btnWorkingLight.Flicker = false;
            this.btnWorkingLight.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnWorkingLight.ForeColor = System.Drawing.Color.Black;
            this.btnWorkingLight.IsLeftLampOn = false;
            this.btnWorkingLight.IsRightLampOn = false;
            this.btnWorkingLight.LampAliveTime = 500;
            this.btnWorkingLight.LampSize = 1;
            this.btnWorkingLight.LeftLampColor = System.Drawing.Color.Red;
            this.btnWorkingLight.Location = new System.Drawing.Point(5, 89);
            this.btnWorkingLight.Name = "btnWorkingLight";
            this.btnWorkingLight.OnOff = false;
            this.btnWorkingLight.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnWorkingLight.Size = new System.Drawing.Size(100, 25);
            this.btnWorkingLight.TabIndex = 479;
            this.btnWorkingLight.TabStop = false;
            this.btnWorkingLight.Text = "작업등";
            this.btnWorkingLight.Text2 = "";
            this.btnWorkingLight.UseVisualStyleBackColor = false;
            this.btnWorkingLight.VisibleLeftLamp = false;
            this.btnWorkingLight.VisibleRightLamp = false;
            this.btnWorkingLight.Click += new System.EventHandler(this.btnInnerLamp_Click);
            // 
            // label11
            // 
            this.label11.AutoEllipsis = true;
            this.label11.BackColor = System.Drawing.Color.Gainsboro;
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 20);
            this.label11.TabIndex = 9;
            this.label11.Text = "■ 조작";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel11
            // 
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel11.Controls.Add(this.label7);
            this.panel11.Controls.Add(this.btnTact);
            this.panel11.Location = new System.Drawing.Point(235, 13);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(106, 156);
            this.panel11.TabIndex = 449;
            // 
            // label7
            // 
            this.label7.AutoEllipsis = true;
            this.label7.BackColor = System.Drawing.Color.Gainsboro;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 24);
            this.label7.TabIndex = 9;
            this.label7.Text = "■ 설비";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel12
            // 
            this.panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel12.Controls.Add(this.lblIsInspUseMotor);
            this.panel12.Controls.Add(this.lblEFEMAutoMode);
            this.panel12.Controls.Add(this.lblOuterMode);
            this.panel12.Controls.Add(this.lblReviewManual);
            this.panel12.Controls.Add(this.lblAutoCondition);
            this.panel12.Controls.Add(this.lblInspRun);
            this.panel12.Controls.Add(this.lblRvRun);
            this.panel12.Location = new System.Drawing.Point(6, 170);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(456, 49);
            this.panel12.TabIndex = 449;
            // 
            // lblIsInspUseMotor
            // 
            this.lblIsInspUseMotor.BackColor = System.Drawing.Color.Gainsboro;
            this.lblIsInspUseMotor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIsInspUseMotor.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblIsInspUseMotor.Location = new System.Drawing.Point(209, 24);
            this.lblIsInspUseMotor.Margin = new System.Windows.Forms.Padding(1);
            this.lblIsInspUseMotor.Name = "lblIsInspUseMotor";
            this.lblIsInspUseMotor.Size = new System.Drawing.Size(118, 18);
            this.lblIsInspUseMotor.TabIndex = 447;
            this.lblIsInspUseMotor.Text = "Insp Use Motor";
            this.lblIsInspUseMotor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEFEMAutoMode
            // 
            this.lblEFEMAutoMode.BackColor = System.Drawing.Color.Gainsboro;
            this.lblEFEMAutoMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblEFEMAutoMode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEFEMAutoMode.ForeColor = System.Drawing.Color.Black;
            this.lblEFEMAutoMode.Location = new System.Drawing.Point(106, 5);
            this.lblEFEMAutoMode.Margin = new System.Windows.Forms.Padding(1);
            this.lblEFEMAutoMode.Name = "lblEFEMAutoMode";
            this.lblEFEMAutoMode.Size = new System.Drawing.Size(100, 18);
            this.lblEFEMAutoMode.TabIndex = 1;
            this.lblEFEMAutoMode.Text = "Auto";
            this.lblEFEMAutoMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspRun
            // 
            this.lblInspRun.BackColor = System.Drawing.Color.Gainsboro;
            this.lblInspRun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInspRun.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblInspRun.Location = new System.Drawing.Point(209, 5);
            this.lblInspRun.Margin = new System.Windows.Forms.Padding(1);
            this.lblInspRun.Name = "lblInspRun";
            this.lblInspRun.Size = new System.Drawing.Size(118, 18);
            this.lblInspRun.TabIndex = 443;
            this.lblInspRun.Text = "Inspection Run";
            this.lblInspRun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLPM1Recipe
            // 
            this.lblLPM1Recipe.BackColor = System.Drawing.Color.Gainsboro;
            this.lblLPM1Recipe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLPM1Recipe.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblLPM1Recipe.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblLPM1Recipe.Location = new System.Drawing.Point(344, 57);
            this.lblLPM1Recipe.Margin = new System.Windows.Forms.Padding(1);
            this.lblLPM1Recipe.Name = "lblLPM1Recipe";
            this.lblLPM1Recipe.Size = new System.Drawing.Size(129, 34);
            this.lblLPM1Recipe.TabIndex = 444;
            this.lblLPM1Recipe.Text = "R1091_R2345";
            this.lblLPM1Recipe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLPM1Recipe.Click += new System.EventHandler(this.lblCurRecipe2_Click);
            // 
            // lblLPM2Recipe
            // 
            this.lblLPM2Recipe.BackColor = System.Drawing.Color.Gainsboro;
            this.lblLPM2Recipe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLPM2Recipe.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblLPM2Recipe.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblLPM2Recipe.Location = new System.Drawing.Point(344, 22);
            this.lblLPM2Recipe.Margin = new System.Windows.Forms.Padding(1);
            this.lblLPM2Recipe.Name = "lblLPM2Recipe";
            this.lblLPM2Recipe.Size = new System.Drawing.Size(129, 34);
            this.lblLPM2Recipe.TabIndex = 444;
            this.lblLPM2Recipe.Text = "0123456789";
            this.lblLPM2Recipe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLPM2Recipe.Click += new System.EventHandler(this.lblCurRecipe2_Click);
            // 
            // panel17
            // 
            this.panel17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel17.Controls.Add(this.label50);
            this.panel17.Controls.Add(this.label47);
            this.panel17.Controls.Add(this.ucrlLPMOption);
            this.panel17.Controls.Add(this.label32);
            this.panel17.Controls.Add(this.label45);
            this.panel17.Controls.Add(this.label43);
            this.panel17.Controls.Add(this.label19);
            this.panel17.Controls.Add(this.label13);
            this.panel17.Controls.Add(this.label28);
            this.panel17.Controls.Add(this.rdManual);
            this.panel17.Controls.Add(this.rdOHT);
            this.panel17.Controls.Add(this.lblLPM1Recipe);
            this.panel17.Controls.Add(this.lblLPM2Recipe);
            this.panel17.Controls.Add(this.btnTTTM);
            this.panel17.Controls.Add(this.btnBuzzerStop);
            this.panel17.Controls.Add(this.btnAlarmReset);
            this.panel17.Controls.Add(this.btnAutoStart_Copy);
            this.panel17.Controls.Add(this.btnManualMode_Copy);
            this.panel17.Controls.Add(this.btnPause_Copy);
            this.panel17.Controls.Add(this.label42);
            this.panel17.Controls.Add(this.label37);
            this.panel17.Controls.Add(this.label5);
            this.panel17.Location = new System.Drawing.Point(4, 99);
            this.panel17.Margin = new System.Windows.Forms.Padding(0);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(475, 167);
            this.panel17.TabIndex = 451;
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.White;
            this.label50.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label50.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label50.ForeColor = System.Drawing.Color.Black;
            this.label50.Location = new System.Drawing.Point(309, 57);
            this.label50.Margin = new System.Windows.Forms.Padding(1);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(34, 34);
            this.label50.TabIndex = 468;
            this.label50.Text = "LP1";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.White;
            this.label47.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label47.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label47.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.label47.ForeColor = System.Drawing.Color.Black;
            this.label47.Location = new System.Drawing.Point(309, 22);
            this.label47.Margin = new System.Windows.Forms.Padding(1);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(34, 34);
            this.label47.TabIndex = 468;
            this.label47.Text = "LP2";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucrlLPMOption
            // 
            this.ucrlLPMOption.LoadPort = EquipMainUi.Struct.Detail.EFEM.EmEfemPort.ROBOT;
            this.ucrlLPMOption.Location = new System.Drawing.Point(112, 22);
            this.ucrlLPMOption.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.ucrlLPMOption.Name = "ucrlLPMOption";
            this.ucrlLPMOption.Size = new System.Drawing.Size(194, 49);
            this.ucrlLPMOption.TabIndex = 467;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label32.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label32.Location = new System.Drawing.Point(56, 71);
            this.label32.Margin = new System.Windows.Forms.Padding(1);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(56, 19);
            this.label32.TabIndex = 3;
            this.label32.Text = "비물류";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label45.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label45.Font = new System.Drawing.Font("맑은 고딕", 7F);
            this.label45.Location = new System.Drawing.Point(256, 71);
            this.label45.Margin = new System.Windows.Forms.Padding(1);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(50, 19);
            this.label45.TabIndex = 3;
            this.label45.Text = "직접선택";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label43.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label43.Font = new System.Drawing.Font("맑은 고딕", 7F);
            this.label43.Location = new System.Drawing.Point(208, 71);
            this.label43.Margin = new System.Windows.Forms.Padding(1);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(50, 19);
            this.label43.TabIndex = 3;
            this.label43.Text = "모두";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label19.Font = new System.Drawing.Font("맑은 고딕", 7F);
            this.label19.Location = new System.Drawing.Point(157, 71);
            this.label19.Margin = new System.Windows.Forms.Padding(1);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(50, 19);
            this.label19.TabIndex = 3;
            this.label19.Text = "아랫장만";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("맑은 고딕", 7F);
            this.label13.Location = new System.Drawing.Point(110, 71);
            this.label13.Margin = new System.Windows.Forms.Padding(1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 19);
            this.label13.TabIndex = 3;
            this.label13.Text = "윗장만";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label28.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label28.Location = new System.Drawing.Point(0, 71);
            this.label28.Margin = new System.Windows.Forms.Padding(1);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(56, 19);
            this.label28.TabIndex = 3;
            this.label28.Text = "물류";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdManual
            // 
            this.rdManual.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdManual.BackgroundImage = global::EquipMainUi.Properties.Resources.manualLoad;
            this.rdManual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rdManual.Location = new System.Drawing.Point(56, 21);
            this.rdManual.Margin = new System.Windows.Forms.Padding(0);
            this.rdManual.Name = "rdManual";
            this.rdManual.Size = new System.Drawing.Size(56, 50);
            this.rdManual.TabIndex = 466;
            this.rdManual.TabStop = true;
            this.rdManual.UseVisualStyleBackColor = true;
            this.rdManual.Click += new System.EventHandler(this.rdManual_Click);
            // 
            // rdOHT
            // 
            this.rdOHT.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdOHT.BackgroundImage = global::EquipMainUi.Properties.Resources.oht;
            this.rdOHT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rdOHT.Location = new System.Drawing.Point(0, 21);
            this.rdOHT.Margin = new System.Windows.Forms.Padding(0);
            this.rdOHT.Name = "rdOHT";
            this.rdOHT.Size = new System.Drawing.Size(56, 50);
            this.rdOHT.TabIndex = 465;
            this.rdOHT.TabStop = true;
            this.rdOHT.UseVisualStyleBackColor = true;
            this.rdOHT.Click += new System.EventHandler(this.rdOHT_Click);
            // 
            // btnTTTM
            // 
            this.btnTTTM.BackColor = System.Drawing.Color.Transparent;
            this.btnTTTM.Delay = 100;
            this.btnTTTM.Flicker = false;
            this.btnTTTM.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnTTTM.ForeColor = System.Drawing.Color.Black;
            this.btnTTTM.IsLeftLampOn = false;
            this.btnTTTM.IsRightLampOn = false;
            this.btnTTTM.LampAliveTime = 500;
            this.btnTTTM.LampSize = 1;
            this.btnTTTM.LeftLampColor = System.Drawing.Color.Red;
            this.btnTTTM.Location = new System.Drawing.Point(303, 94);
            this.btnTTTM.Name = "btnTTTM";
            this.btnTTTM.OnOff = false;
            this.btnTTTM.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnTTTM.Size = new System.Drawing.Size(61, 67);
            this.btnTTTM.TabIndex = 83;
            this.btnTTTM.TabStop = false;
            this.btnTTTM.Text = "Camera\r\nTTTM";
            this.btnTTTM.Text2 = "";
            this.btnTTTM.UseVisualStyleBackColor = false;
            this.btnTTTM.VisibleLeftLamp = false;
            this.btnTTTM.VisibleRightLamp = false;
            this.btnTTTM.Click += new System.EventHandler(this.btnTTTM_Click);
            // 
            // btnAutoStart_Copy
            // 
            this.btnAutoStart_Copy.BackColor = System.Drawing.Color.Transparent;
            this.btnAutoStart_Copy.BackgroundImage = global::EquipMainUi.Properties.Resources.Operator_Engineer_Auto_Run;
            this.btnAutoStart_Copy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAutoStart_Copy.Delay = 500;
            this.btnAutoStart_Copy.Flicker = false;
            this.btnAutoStart_Copy.Font = new System.Drawing.Font("맑은 고딕", 9.75F);
            this.btnAutoStart_Copy.ForeColor = System.Drawing.Color.Black;
            this.btnAutoStart_Copy.IsLeftLampOn = false;
            this.btnAutoStart_Copy.IsRightLampOn = false;
            this.btnAutoStart_Copy.LampAliveTime = 500;
            this.btnAutoStart_Copy.LampSize = 1;
            this.btnAutoStart_Copy.LeftLampColor = System.Drawing.Color.Red;
            this.btnAutoStart_Copy.Location = new System.Drawing.Point(1, 94);
            this.btnAutoStart_Copy.Name = "btnAutoStart_Copy";
            this.btnAutoStart_Copy.OnOff = false;
            this.btnAutoStart_Copy.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAutoStart_Copy.Size = new System.Drawing.Size(100, 67);
            this.btnAutoStart_Copy.TabIndex = 84;
            this.btnAutoStart_Copy.Text = "\r\n\r\nAuto Start";
            this.btnAutoStart_Copy.Text2 = "";
            this.btnAutoStart_Copy.UseVisualStyleBackColor = false;
            this.btnAutoStart_Copy.VisibleLeftLamp = false;
            this.btnAutoStart_Copy.VisibleRightLamp = false;
            this.btnAutoStart_Copy.DelayClick += new System.EventHandler(this.btndAutoStart_DelayClick);
            this.btnAutoStart_Copy.Click += new System.EventHandler(this.btnAutoStart_Copy_Click);
            // 
            // btnManualMode_Copy
            // 
            this.btnManualMode_Copy.BackColor = System.Drawing.Color.Transparent;
            this.btnManualMode_Copy.BackgroundImage = global::EquipMainUi.Properties.Resources.Operator_Manual;
            this.btnManualMode_Copy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnManualMode_Copy.Delay = 500;
            this.btnManualMode_Copy.Flicker = false;
            this.btnManualMode_Copy.Font = new System.Drawing.Font("맑은 고딕", 9.5F);
            this.btnManualMode_Copy.ForeColor = System.Drawing.Color.Black;
            this.btnManualMode_Copy.IsLeftLampOn = false;
            this.btnManualMode_Copy.IsRightLampOn = false;
            this.btnManualMode_Copy.LampAliveTime = 500;
            this.btnManualMode_Copy.LampSize = 1;
            this.btnManualMode_Copy.LeftLampColor = System.Drawing.Color.Red;
            this.btnManualMode_Copy.Location = new System.Drawing.Point(203, 94);
            this.btnManualMode_Copy.Name = "btnManualMode_Copy";
            this.btnManualMode_Copy.OnOff = false;
            this.btnManualMode_Copy.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnManualMode_Copy.Size = new System.Drawing.Size(100, 67);
            this.btnManualMode_Copy.TabIndex = 10;
            this.btnManualMode_Copy.TabStop = false;
            this.btnManualMode_Copy.Text = "\r\n\r\nManual\r\n";
            this.btnManualMode_Copy.Text2 = "";
            this.btnManualMode_Copy.UseVisualStyleBackColor = false;
            this.btnManualMode_Copy.VisibleLeftLamp = false;
            this.btnManualMode_Copy.VisibleRightLamp = false;
            this.btnManualMode_Copy.DelayClick += new System.EventHandler(this.btnManualMode_DelayClick);
            // 
            // btnPause_Copy
            // 
            this.btnPause_Copy.BackColor = System.Drawing.Color.Transparent;
            this.btnPause_Copy.BackgroundImage = global::EquipMainUi.Properties.Resources.Operator_Pause;
            this.btnPause_Copy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPause_Copy.Delay = 500;
            this.btnPause_Copy.Flicker = false;
            this.btnPause_Copy.Font = new System.Drawing.Font("맑은 고딕", 9.75F);
            this.btnPause_Copy.ForeColor = System.Drawing.Color.Black;
            this.btnPause_Copy.IsLeftLampOn = false;
            this.btnPause_Copy.IsRightLampOn = false;
            this.btnPause_Copy.LampAliveTime = 500;
            this.btnPause_Copy.LampSize = 1;
            this.btnPause_Copy.LeftLampColor = System.Drawing.Color.Red;
            this.btnPause_Copy.Location = new System.Drawing.Point(102, 94);
            this.btnPause_Copy.Name = "btnPause_Copy";
            this.btnPause_Copy.OnOff = false;
            this.btnPause_Copy.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnPause_Copy.Size = new System.Drawing.Size(100, 67);
            this.btnPause_Copy.TabIndex = 10;
            this.btnPause_Copy.TabStop = false;
            this.btnPause_Copy.Text = "\r\n\r\nPause";
            this.btnPause_Copy.Text2 = "";
            this.btnPause_Copy.UseVisualStyleBackColor = false;
            this.btnPause_Copy.VisibleLeftLamp = false;
            this.btnPause_Copy.VisibleRightLamp = false;
            this.btnPause_Copy.DelayClick += new System.EventHandler(this.btnPause_DelayClick);
            // 
            // label42
            // 
            this.label42.AutoEllipsis = true;
            this.label42.BackColor = System.Drawing.Color.Gainsboro;
            this.label42.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label42.ForeColor = System.Drawing.Color.Black;
            this.label42.Location = new System.Drawing.Point(0, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(112, 19);
            this.label42.TabIndex = 9;
            this.label42.Text = "■ 진행방식";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label37
            // 
            this.label37.AutoEllipsis = true;
            this.label37.BackColor = System.Drawing.Color.Gainsboro;
            this.label37.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label37.ForeColor = System.Drawing.Color.Black;
            this.label37.Location = new System.Drawing.Point(115, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(191, 19);
            this.label37.TabIndex = 9;
            this.label37.Text = "■ 검사 진행 방식 선택";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.BackColor = System.Drawing.Color.Gainsboro;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(309, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 19);
            this.label5.TabIndex = 9;
            this.label5.Text = "■ Recipe";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel13
            // 
            this.panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel13.Controls.Add(this.lblPGVersion);
            this.panel13.Controls.Add(this.label18);
            this.panel13.Location = new System.Drawing.Point(4, 1041);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(165, 24);
            this.panel13.TabIndex = 74;
            // 
            // lblPGVersion
            // 
            this.lblPGVersion.AutoEllipsis = true;
            this.lblPGVersion.BackColor = System.Drawing.Color.White;
            this.lblPGVersion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPGVersion.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblPGVersion.ForeColor = System.Drawing.Color.Black;
            this.lblPGVersion.Location = new System.Drawing.Point(78, -1);
            this.lblPGVersion.Name = "lblPGVersion";
            this.lblPGVersion.Size = new System.Drawing.Size(88, 21);
            this.lblPGVersion.TabIndex = 63;
            this.lblPGVersion.Text = "2019.00.00.0000";
            this.lblPGVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.AutoEllipsis = true;
            this.label18.BackColor = System.Drawing.Color.Gainsboro;
            this.label18.Dock = System.Windows.Forms.DockStyle.Left;
            this.label18.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(78, 22);
            this.label18.TabIndex = 9;
            this.label18.Text = "■ Version";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCimStatus
            // 
            this.lblCimStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(200)))), ((int)(((byte)(246)))));
            this.lblCimStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCimStatus.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lblCimStatus.Location = new System.Drawing.Point(198, 5);
            this.lblCimStatus.Margin = new System.Windows.Forms.Padding(1);
            this.lblCimStatus.Name = "lblCimStatus";
            this.lblCimStatus.Size = new System.Drawing.Size(131, 52);
            this.lblCimStatus.TabIndex = 3;
            this.lblCimStatus.Text = "Offline";
            this.lblCimStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCimStatus.Click += new System.EventHandler(this.lblCimStatus_Click);
            // 
            // lblLoginID
            // 
            this.lblLoginID.BackColor = System.Drawing.Color.Gainsboro;
            this.lblLoginID.Delay = 100;
            this.lblLoginID.Flicker = false;
            this.lblLoginID.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblLoginID.IsLeftLampOn = false;
            this.lblLoginID.IsRightLampOn = false;
            this.lblLoginID.LampAliveTime = 500;
            this.lblLoginID.LampSize = 3;
            this.lblLoginID.LeftLampColor = System.Drawing.Color.Red;
            this.lblLoginID.Location = new System.Drawing.Point(332, 5);
            this.lblLoginID.Margin = new System.Windows.Forms.Padding(1);
            this.lblLoginID.Name = "lblLoginID";
            this.lblLoginID.OnOff = false;
            this.lblLoginID.RightLampColor = System.Drawing.Color.DarkGreen;
            this.lblLoginID.Size = new System.Drawing.Size(94, 52);
            this.lblLoginID.TabIndex = 476;
            this.lblLoginID.Text = "ID : 0000000000";
            this.lblLoginID.Text2 = "";
            this.lblLoginID.UseVisualStyleBackColor = false;
            this.lblLoginID.VisibleLeftLamp = false;
            this.lblLoginID.VisibleRightLamp = false;
            this.lblLoginID.Click += new System.EventHandler(this.lblLoginID_Click);
            // 
            // btnExpanding
            // 
            this.btnExpanding.BackColor = System.Drawing.Color.Transparent;
            this.btnExpanding.Delay = 2;
            this.btnExpanding.Flicker = false;
            this.btnExpanding.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnExpanding.ForeColor = System.Drawing.Color.Black;
            this.btnExpanding.IsLeftLampOn = false;
            this.btnExpanding.IsRightLampOn = false;
            this.btnExpanding.LampAliveTime = 500;
            this.btnExpanding.LampSize = 1;
            this.btnExpanding.LeftLampColor = System.Drawing.Color.Red;
            this.btnExpanding.Location = new System.Drawing.Point(404, 266);
            this.btnExpanding.Name = "btnExpanding";
            this.btnExpanding.OnOff = false;
            this.btnExpanding.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnExpanding.Size = new System.Drawing.Size(73, 27);
            this.btnExpanding.TabIndex = 445;
            this.btnExpanding.TabStop = false;
            this.btnExpanding.Text = "◀◀◀";
            this.btnExpanding.Text2 = "";
            this.btnExpanding.UseVisualStyleBackColor = false;
            this.btnExpanding.VisibleLeftLamp = false;
            this.btnExpanding.VisibleRightLamp = false;
            this.btnExpanding.Click += new System.EventHandler(this.btnExpanding_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(1916, 1061);
            this.Controls.Add(this.btnExpanding);
            this.Controls.Add(this.lblLoginID);
            this.Controls.Add(this.panel17);
            this.Controls.Add(this.panel13);
            this.Controls.Add(this.tabOperator);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblProcState);
            this.Controls.Add(this.tabCtrl_MainMenu);
            this.Controls.Add(this.lblCurrScanTime);
            this.Controls.Add(this.lblAvgScanTime);
            this.Controls.Add(this.lblMaxScanTime);
            this.Controls.Add(this.lblCimStatus);
            this.Controls.Add(this.lblEquipState);
            this.Controls.Add(this.labelWatch);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.labelMode);
            this.Controls.Add(this.pnlExtend);
            this.Controls.Add(this.tbStatus);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Equip";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.pnlEquipDraw.ResumeLayout(false);
            this.panel33.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMotorState)).EndInit();
            this.pnlAlarm.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.tabControlAlarm.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.gbSetUpTest.ResumeLayout(false);
            this.gbSetUpTest.PerformLayout();
            this.pnlCimMode.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlExtend.ResumeLayout(false);
            this.pnlExtend.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.pnlLowerWaferInfo.ResumeLayout(false);
            this.pnlUpperWaferInfo.ResumeLayout(false);
            this.pnlAVIWaferInfo.ResumeLayout(false);
            this.pnlAlignWaferInfo.ResumeLayout(false);
            this.panel22.ResumeLayout(false);
            this.pnlLpm1CstInfo.ResumeLayout(false);
            this.pnlLpm2CstInfo.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.tabCtrlTransferData.ResumeLayout(false);
            this.tabpCassette.ResumeLayout(false);
            this.tabpWafer.ResumeLayout(false);
            this.tabpAutoDataLog.ResumeLayout(false);
            this.tabCtrl_MainMenu.ResumeLayout(false);
            this.tabp_Progress.ResumeLayout(false);
            this.tabp_Step.ResumeLayout(false);
            this.pnlMainMenus.ResumeLayout(false);
            this.pnlMainStep.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.tabOperator.ResumeLayout(false);
            this.tabPageOperator.ResumeLayout(false);
            this.tabPageOperator.PerformLayout();
            this.panel21.ResumeLayout(false);
            this.tabPageEngineer.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbStatus;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnCycleStop;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnBuzzerStop;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlarmReset;
        private System.Windows.Forms.Panel panel3;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnReservReInspStart;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnReservReInsp;
        internal System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel pnlEquipDraw;
        private System.Windows.Forms.Timer tmrUiUpdate;
        private System.Windows.Forms.Panel panel33;
        private System.Windows.Forms.Timer tmrState;
        private Dit.Framework.UI.UserComponent.ButtonLabel btnSetupOption;
        private Dit.Framework.UI.UserComponent.ButtonLabel btnParameter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlAlarm;
        internal System.Windows.Forms.Label label40;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private EquipView.UcrlEquipView ucrlEquipView;
        private System.Windows.Forms.Panel pnlExtend;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelMode;
        private System.Windows.Forms.Label lblMaxScanTime;
        private System.Windows.Forms.TabControl tabCtrl_MainMenu;
        private System.Windows.Forms.TabPage tabp_Step;
        private System.Windows.Forms.Panel pnlMainMenus;
        private System.Windows.Forms.Panel pnlMainStep;
        private System.Windows.Forms.Panel panel5;
        internal System.Windows.Forms.Label label90;
        private System.Windows.Forms.Panel panel18;
        internal System.Windows.Forms.Label label30;
        internal System.Windows.Forms.Label lblReviewStep;
        internal System.Windows.Forms.Label lblMainStep;
        internal System.Windows.Forms.Label label130;
        internal System.Windows.Forms.Label label131;
        internal System.Windows.Forms.Label label46;
        internal System.Windows.Forms.Label lblHomingStep;
        internal System.Windows.Forms.Label Loading;
        internal System.Windows.Forms.Label lblUnloadingStep;
        internal System.Windows.Forms.Label lblLoadingStep;
        internal System.Windows.Forms.Label label55;
        internal System.Windows.Forms.Label label44;
        internal System.Windows.Forms.Label lblScanningStep;
        private System.Windows.Forms.Label labelWatch;
        private System.Windows.Forms.Label lblEquipState;
        private System.Windows.Forms.Label lblProcState;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 ButtonDelay21;
        private System.Windows.Forms.Panel panel7;
        internal System.Windows.Forms.Label lblGlassInfoTitle;
        private System.Windows.Forms.Label lblCurrScanTime;
        private Dit.Framework.UI.UserComponent.LabelFlicker lblSyncSvr;
        internal System.Windows.Forms.Label lblPMAC;
        internal System.Windows.Forms.Label lblInspector;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnExit;
        private Dit.Framework.UI.UserComponent.ButtonLabel btnMonitor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnInnerWork;
        private System.Windows.Forms.Label lblRvRun;
        private System.Windows.Forms.Label lblOuterMode;
        private System.Windows.Forms.Label lblAvgScanTime;
        private System.Windows.Forms.Label lblReviewManual;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.ListView lstOperationMode;
        private System.Windows.Forms.DataGridView dgvMotorState;
        private System.Windows.Forms.DataGridViewTextBoxColumn MOTOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn StageX;
        internal System.Windows.Forms.Label lblScanNum;
        private System.Windows.Forms.Label lblAutoCondition;
        internal System.Windows.Forms.Label lblScanCount;
        private System.Windows.Forms.Label label15;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnPioReset;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btndGlassUnloading;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnTact;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnReservInsp_Stop;
        internal System.Windows.Forms.Label lblReviewTimeLimit;
        private System.Windows.Forms.Label label29;
        internal System.Windows.Forms.Label lblReviewCurTime;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnReviewSkip;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnReviewManual;
        internal System.Windows.Forms.Label lblInspTimeLimit;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label lblInspCurTime;
        internal System.Windows.Forms.Label lblEFEM;
        private System.Windows.Forms.Panel panel10;
        internal System.Windows.Forms.Label lblPioLowerStepRecovery;
        internal System.Windows.Forms.Label lblPioUpperStepRecovery;
        internal System.Windows.Forms.Label label49;
        internal System.Windows.Forms.Label lblPioLPM1OHTStep;
        internal System.Windows.Forms.Label label52;
        internal System.Windows.Forms.Label lblPioLPM2OHTStep;
        internal System.Windows.Forms.Label label95;
        private System.Windows.Forms.TabControl tabOperator;
        private System.Windows.Forms.TabPage tabPageOperator;
        private System.Windows.Forms.TabPage tabPageEngineer;
        private System.Windows.Forms.Panel panel17;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnManualMode_Copy;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnPause_Copy;
        private System.Windows.Forms.Panel panel21;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnReviewManual_Copy;
        internal System.Windows.Forms.Label label34;
        private System.Windows.Forms.Panel panel14;
        internal System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel11;
        internal System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label lblInspRun;
        private System.Windows.Forms.Panel panel13;
        internal System.Windows.Forms.Label lblPGVersion;
        internal System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblCimStatus;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 lblLoginID;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnWorkingLight;
        private System.Windows.Forms.Label lblIsInspUseMotor;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnUpperRobotTransferData;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnLPM2CassetteTransferData;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnLPM1CassetteTransferData;
        private System.Windows.Forms.PropertyGrid pGridCstTransferInfo;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnAutoStart_Copy;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnEquipmentTransferData;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlignerTransferData;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnLowerRobotTransferData;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TabControl tabCtrlTransferData;
        private System.Windows.Forms.TabPage tabpCassette;
        private System.Windows.Forms.TabPage tabpWafer;
        private System.Windows.Forms.PropertyGrid pGridWaferTransferInfo;
        internal System.Windows.Forms.Label label27;
        internal System.Windows.Forms.Label lblRobotStep;
        internal System.Windows.Forms.Label label33;
        internal System.Windows.Forms.Label lblLpm1Step;
        internal System.Windows.Forms.Label label36;
        internal System.Windows.Forms.Label label41;
        internal System.Windows.Forms.Label lblLpm2Step;
        internal System.Windows.Forms.Label label38;
        internal System.Windows.Forms.Label lblAlignerStep;
        private System.Windows.Forms.PropertyGrid pGridCstInfoLPM2;
        private System.Windows.Forms.PropertyGrid pGridCstInfoLPM1;
        private System.Windows.Forms.TabControl tabControlTransferInfo;
        private System.Windows.Forms.TabPage tabPageCst;
        internal System.Windows.Forms.Label lblCstName;
        private System.Windows.Forms.TabPage tabPageWafer;
        internal System.Windows.Forms.Label lblWaferID;
        private System.Windows.Forms.PropertyGrid pGridWaferInfo;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnLoadWaferInfoAVI;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnLoadWaferInfoAligner;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnLoadWaferInfoLower;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnLoadWaferInfoUpper;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnLoadWaferInfoLPM2;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnLoadWaferInfoLPM1;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnEFEMCycleStop;
        internal System.Windows.Forms.Label label22;
        internal System.Windows.Forms.Label label17;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnEFEMStart;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnEFEMManual;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnEFEMPause;
        private System.Windows.Forms.TabControl tabControlAlarm;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView lstvAlarmClone;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView lstvAlarmHistory;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ListView lstEFEMAlarm;
        private System.Windows.Forms.Panel panel22;
        internal System.Windows.Forms.Label label48;
        private System.Windows.Forms.Button btnUpperVacOff;
        private System.Windows.Forms.Button btnUpperVacOn;
        private System.Windows.Forms.Button btnLowerVacOff;
        private System.Windows.Forms.Button btnLowerVacOn;
        private System.Windows.Forms.Button btnSettingOn;
        private System.Windows.Forms.Button btnUnPoupLpm1;
        private System.Windows.Forms.Button btnLongRunOff;
        private System.Windows.Forms.Button btnUnPoupLpm2;
        private System.Windows.Forms.GroupBox gbSetUpTest;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnAVIStartStatus;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAVIManualStatus;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAVIPauseStatus;
        private System.Windows.Forms.Label lblEFEMAutoMode;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnShift;
        private System.Windows.Forms.Label lblLongCount;
        private System.Windows.Forms.Label lblRobotWait;
        private System.Windows.Forms.Label label3;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnLPM2TransferData_Copy;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnLPM1TransferData_Copy;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnReviewSkipCopy;
        private System.Windows.Forms.Button btnAlignerWaferOff;
        private System.Windows.Forms.Button btnAVIWaferOff;
        private System.Windows.Forms.Label lblLPM2Recipe;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnExpanding;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnDoorOpenClose;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel pnlLowerWaferInfo;
        private System.Windows.Forms.PropertyGrid pGridWaferInfoLower;
        private System.Windows.Forms.Panel pnlUpperWaferInfo;
        private System.Windows.Forms.PropertyGrid pGridWaferInfoUpper;
        private System.Windows.Forms.Panel pnlAVIWaferInfo;
        private System.Windows.Forms.PropertyGrid pGridWaferInfoAVI;
        private System.Windows.Forms.Panel pnlAlignWaferInfo;
        private System.Windows.Forms.PropertyGrid pGridWaferInfoAligner;
        internal System.Windows.Forms.Label label4;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnLowerTransferData_Copy;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnUpperTransferData_Copy;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlignerTransferData_Copy;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnEquipmentTransferData_Copy;
        private System.Windows.Forms.Label lblLPM1Recipe;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label lblBCRStat;
        internal System.Windows.Forms.Label lblOCRStat;
        internal System.Windows.Forms.Label lblRFID2Stat;
        internal System.Windows.Forms.Label lblRFID1Stat;
        internal System.Windows.Forms.Label lblEFUStat;
        private System.Windows.Forms.Button btnAVIWaferOn;
        private System.Windows.Forms.Button btnAlignerWaferOn;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnTTTM;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Label lblTTTMStep;
        internal System.Windows.Forms.Label lblTTTMTimeLimit;
        private System.Windows.Forms.Label label16;
        internal System.Windows.Forms.Label lblTTTMCurTime;
        private System.Windows.Forms.Panel pnlLpm1CstInfo;
        private System.Windows.Forms.Panel pnlLpm2CstInfo;
        private System.Windows.Forms.Button btnPoupLpm2;
        private System.Windows.Forms.Button btnPoupLpm1;
        private System.Windows.Forms.Panel pnlCimMode;
        private System.Windows.Forms.RadioButton rdRemote;
        private System.Windows.Forms.RadioButton rdLocal;
        private System.Windows.Forms.RadioButton rdOffLine;
        internal System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Label label31;
        internal System.Windows.Forms.Label lblPioUpeerStep;
        internal System.Windows.Forms.Label label35;
        internal System.Windows.Forms.Label lblPioLowerStep;
        internal System.Windows.Forms.Label label39;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnEfemDoorOpen;
        private System.Windows.Forms.RadioButton rdOHT;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.RadioButton rdManual;
        internal System.Windows.Forms.Label label42;
        internal System.Windows.Forms.Label label37;
        private System.Windows.Forms.TabPage tabp_Progress;
        internal System.Windows.Forms.Label lblLpm1Pogress;
        internal System.Windows.Forms.Label lblLpm2Pogress;
        internal System.Windows.Forms.Label lblLpm2;
        internal System.Windows.Forms.Label lblLpm1;
        private System.Windows.Forms.Button btnLpm1OHTStepReset;
        private System.Windows.Forms.Button btnLpm1OHTUldComplete;
        private System.Windows.Forms.Button btnLpm2OHTStepReset;
        private System.Windows.Forms.Button btnLpm2OHTUldComplete;
        private System.Windows.Forms.Button btnLpm2OHTLdComplete;
        private System.Windows.Forms.Button btnLpm1OHTLdComplete;
        private UserControls.ucrlLPMOption ucrlLPMOption;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Button btnDeleteAllInfo;
        private System.Windows.Forms.Button btnLogPath;
        private System.Windows.Forms.TabPage tabpAutoDataLog;
        private Dit.Framework.UI.UserComponent.ListViewEx lstAutoDataLog;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnInfoLog;
        private System.Windows.Forms.Button btnLpm1DeepReviewPass;
        private System.Windows.Forms.Button btnLpm2DeepReviewPass;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnMotorLongRun;
        private System.Windows.Forms.Label lblMotorLongRun;
        private System.Windows.Forms.Button button1;
    }
}

