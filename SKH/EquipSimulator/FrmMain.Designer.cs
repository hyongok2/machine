namespace EquipSimulator
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.label11 = new System.Windows.Forms.Label();
            this.tmrWorker = new System.Windows.Forms.Timer(this.components);
            this.chkEmo01 = new System.Windows.Forms.CheckBox();
            this.chkEmo02 = new System.Windows.Forms.CheckBox();
            this.chkEmo03 = new System.Windows.Forms.CheckBox();
            this.chkEmo04 = new System.Windows.Forms.CheckBox();
            this.chkIsol01 = new System.Windows.Forms.CheckBox();
            this.chkIsol02 = new System.Windows.Forms.CheckBox();
            this.chkIsol04 = new System.Windows.Forms.CheckBox();
            this.chkIsol03 = new System.Windows.Forms.CheckBox();
            this.chkStageGlassSensor1 = new System.Windows.Forms.CheckBox();
            this.chkSafetyModeKeySW = new System.Windows.Forms.CheckBox();
            this.chkRobotArmCheck = new System.Windows.Forms.CheckBox();
            this.chkCoolineMainAir = new System.Windows.Forms.CheckBox();
            this.chkIonizerMainAir = new System.Windows.Forms.CheckBox();
            this.chkIonizer1On = new System.Windows.Forms.CheckBox();
            this.chkIonizer2On = new System.Windows.Forms.CheckBox();
            this.chkIonizer4On = new System.Windows.Forms.CheckBox();
            this.chkIonizer3On = new System.Windows.Forms.CheckBox();
            this.chkCameraCooling = new System.Windows.Forms.CheckBox();
            this.chkIonizerCover = new System.Windows.Forms.CheckBox();
            this.chkBlower1 = new System.Windows.Forms.CheckBox();
            this.chkBlower2 = new System.Windows.Forms.CheckBox();
            this.chkBlower3 = new System.Windows.Forms.CheckBox();
            this.chkBlower4 = new System.Windows.Forms.CheckBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.chkIonizer = new System.Windows.Forms.CheckBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblScanStep = new System.Windows.Forms.Label();
            this.lblInspGUnloadingOk = new System.Windows.Forms.Label();
            this.lblInspGUnloading = new System.Windows.Forms.Label();
            this.lblInspScanEnd = new System.Windows.Forms.Label();
            this.lblInspScanEndOk = new System.Windows.Forms.Label();
            this.lblInspScanStartOk = new System.Windows.Forms.Label();
            this.lblInspGLoadingOk = new System.Windows.Forms.Label();
            this.lblInspScanStart = new System.Windows.Forms.Label();
            this.lblInspScanReadyOk = new System.Windows.Forms.Label();
            this.lblInspScanReady = new System.Windows.Forms.Label();
            this.lblInspGLoading = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMemDetail = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnAllDoor = new System.Windows.Forms.Button();
            this.chkDoor02 = new System.Windows.Forms.CheckBox();
            this.chkDoor05 = new System.Windows.Forms.CheckBox();
            this.chkDoor04 = new System.Windows.Forms.CheckBox();
            this.chkDoor01 = new System.Windows.Forms.CheckBox();
            this.chkDoor03 = new System.Windows.Forms.CheckBox();
            this.chkDoor06 = new System.Windows.Forms.CheckBox();
            this.chkDoor15 = new System.Windows.Forms.CheckBox();
            this.chkDoor14 = new System.Windows.Forms.CheckBox();
            this.chkDoor13 = new System.Windows.Forms.CheckBox();
            this.chkDoor12 = new System.Windows.Forms.CheckBox();
            this.chkDoor11 = new System.Windows.Forms.CheckBox();
            this.chkDoor10 = new System.Windows.Forms.CheckBox();
            this.chkDoor07 = new System.Windows.Forms.CheckBox();
            this.chkDoor09 = new System.Windows.Forms.CheckBox();
            this.chkDoor08 = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.chkEmo06 = new System.Windows.Forms.CheckBox();
            this.chkEmo05 = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnGlassCrackRightOn = new System.Windows.Forms.Button();
            this.btnGlassCrackLeftOn = new System.Windows.Forms.Button();
            this.btnCrackOff = new System.Windows.Forms.Button();
            this.btnGlassCrackSepOn = new System.Windows.Forms.Button();
            this.btnGlassCrackOrgOn = new System.Windows.Forms.Button();
            this.dgvGlassEdge = new System.Windows.Forms.DataGridView();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.chkIonizer1Alarm = new System.Windows.Forms.CheckBox();
            this.chkIonizer2Alarm = new System.Windows.Forms.CheckBox();
            this.chkIonizer4Alarm = new System.Windows.Forms.CheckBox();
            this.chkIonizer3Alarm = new System.Windows.Forms.CheckBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.dgvVacuum = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label8 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.chkBlower6 = new System.Windows.Forms.CheckBox();
            this.chkBlower5 = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.dgvGlassDetect = new System.Windows.Forms.DataGridView();
            this.dataGridViewButtonColumn2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.chkEnableGripSwOn = new System.Windows.Forms.CheckBox();
            this.pnlView = new System.Windows.Forms.Panel();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.label14 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.shInspCam = new EquipSimulator.Ctrl.TransparentControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.shReviewCam = new EquipSimulator.Ctrl.TransparentControl();
            this.shStage = new EquipSimulator.Ctrl.TransparentControl();
            this.SpThelta = new EquipSimulator.Ctrl.TransparentControl();
            this.spLiftPin = new EquipSimulator.Ctrl.TransparentControl();
            this.lblScanTime = new System.Windows.Forms.Label();
            this.btnXyIo = new System.Windows.Forms.Button();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.tmrState = new System.Windows.Forms.Timer(this.components);
            this.btnCtrlPcXy = new System.Windows.Forms.Button();
            this.btnSimulPcXy = new System.Windows.Forms.Button();
            this.btnXyIoAddr = new System.Windows.Forms.Button();
            this.lstLogger = new System.Windows.Forms.ListView();
            this.btnTester = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkReviLotEndAck = new System.Windows.Forms.CheckBox();
            this.chkReviLotStartAck = new System.Windows.Forms.CheckBox();
            this.chkReviReviEnd = new System.Windows.Forms.CheckBox();
            this.chkReviReviStartAck = new System.Windows.Forms.CheckBox();
            this.chkReviAlignStartAck = new System.Windows.Forms.CheckBox();
            this.chkReviUnloadingAck = new System.Windows.Forms.CheckBox();
            this.chkReviLoadingAck = new System.Windows.Forms.CheckBox();
            this.chkReviLotEnd = new System.Windows.Forms.CheckBox();
            this.chkReviLotStart = new System.Windows.Forms.CheckBox();
            this.chkReviReviEndAck = new System.Windows.Forms.CheckBox();
            this.chkReviReviStart = new System.Windows.Forms.CheckBox();
            this.chkReviUnloading = new System.Windows.Forms.CheckBox();
            this.chkReviAlignStart = new System.Windows.Forms.CheckBox();
            this.chkReviLoading = new System.Windows.Forms.CheckBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.chkUseInterlock = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.lblLoRecvComplete = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.btnRecvAbleStart = new System.Windows.Forms.Button();
            this.btnSendAbleStart = new System.Windows.Forms.Button();
            this.label35 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.lblLoRecvStart = new System.Windows.Forms.Label();
            this.lblLoRecvAble = new System.Windows.Forms.Label();
            this.lblAoiSendComplete = new System.Windows.Forms.Label();
            this.lblAoiSendStart = new System.Windows.Forms.Label();
            this.lblAoiSendAble = new System.Windows.Forms.Label();
            this.lblAoiRecvComplete = new System.Windows.Forms.Label();
            this.lblAoiRecvStart = new System.Windows.Forms.Label();
            this.lblAoiRecvAble = new System.Windows.Forms.Label();
            this.lblUpSendComplete = new System.Windows.Forms.Label();
            this.lblUpSendStart = new System.Windows.Forms.Label();
            this.lblUpSendAble = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.chkInspXError = new System.Windows.Forms.CheckBox();
            this.ucrlServoZ3 = new EquipSimulator.Ctrl.UcrlServo();
            this.ucrlServoRY_Under = new EquipSimulator.Ctrl.UcrlServo();
            this.ucrlServoZ2 = new EquipSimulator.Ctrl.UcrlServo();
            this.ucrlServoPin = new EquipSimulator.Ctrl.UcrlServo();
            this.ucrlServoZ1 = new EquipSimulator.Ctrl.UcrlServo();
            this.ucrlServoRY = new EquipSimulator.Ctrl.UcrlServo();
            this.ucrlServoIY = new EquipSimulator.Ctrl.UcrlServo();
            this.ucrlServoIX = new EquipSimulator.Ctrl.UcrlServo();
            this.rdInputGlsRight = new System.Windows.Forms.RadioButton();
            this.rdInputGlsLeft = new System.Windows.Forms.RadioButton();
            this.rdInputGlsSep = new System.Windows.Forms.RadioButton();
            this.rdInputGlsOrg = new System.Windows.Forms.RadioButton();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGlassEdge)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVacuum)).BeginInit();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGlassDetect)).BeginInit();
            this.panel12.SuspendLayout();
            this.pnlView.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel14.SuspendLayout();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.Location = new System.Drawing.Point(7, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(186, 25);
            this.label11.TabIndex = 5;
            this.label11.Text = "EQUIP SIMULATOR";
            // 
            // tmrWorker
            // 
            this.tmrWorker.Enabled = true;
            this.tmrWorker.Tick += new System.EventHandler(this.tmrWorker_Tick);
            // 
            // chkEmo01
            // 
            this.chkEmo01.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkEmo01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkEmo01.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkEmo01.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkEmo01.Location = new System.Drawing.Point(6, 26);
            this.chkEmo01.Name = "chkEmo01";
            this.chkEmo01.Size = new System.Drawing.Size(80, 28);
            this.chkEmo01.TabIndex = 6;
            this.chkEmo01.Text = "EMO01";
            this.chkEmo01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkEmo01.UseVisualStyleBackColor = false;
            // 
            // chkEmo02
            // 
            this.chkEmo02.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkEmo02.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkEmo02.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkEmo02.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkEmo02.Location = new System.Drawing.Point(6, 54);
            this.chkEmo02.Name = "chkEmo02";
            this.chkEmo02.Size = new System.Drawing.Size(80, 28);
            this.chkEmo02.TabIndex = 7;
            this.chkEmo02.Text = "EMO02";
            this.chkEmo02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkEmo02.UseVisualStyleBackColor = false;
            // 
            // chkEmo03
            // 
            this.chkEmo03.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkEmo03.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkEmo03.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkEmo03.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkEmo03.Location = new System.Drawing.Point(6, 82);
            this.chkEmo03.Name = "chkEmo03";
            this.chkEmo03.Size = new System.Drawing.Size(80, 28);
            this.chkEmo03.TabIndex = 8;
            this.chkEmo03.Text = "EMO03";
            this.chkEmo03.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkEmo03.UseVisualStyleBackColor = false;
            // 
            // chkEmo04
            // 
            this.chkEmo04.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkEmo04.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkEmo04.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkEmo04.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkEmo04.Location = new System.Drawing.Point(6, 110);
            this.chkEmo04.Name = "chkEmo04";
            this.chkEmo04.Size = new System.Drawing.Size(80, 28);
            this.chkEmo04.TabIndex = 9;
            this.chkEmo04.Text = "EMO04";
            this.chkEmo04.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkEmo04.UseVisualStyleBackColor = false;
            // 
            // chkIsol01
            // 
            this.chkIsol01.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIsol01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkIsol01.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkIsol01.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkIsol01.Location = new System.Drawing.Point(5, 31);
            this.chkIsol01.Name = "chkIsol01";
            this.chkIsol01.Size = new System.Drawing.Size(80, 28);
            this.chkIsol01.TabIndex = 16;
            this.chkIsol01.Text = "ISOL 01";
            this.chkIsol01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkIsol01.UseVisualStyleBackColor = false;
            // 
            // chkIsol02
            // 
            this.chkIsol02.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIsol02.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkIsol02.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkIsol02.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkIsol02.Location = new System.Drawing.Point(5, 59);
            this.chkIsol02.Name = "chkIsol02";
            this.chkIsol02.Size = new System.Drawing.Size(80, 28);
            this.chkIsol02.TabIndex = 16;
            this.chkIsol02.Text = "ISOL 02";
            this.chkIsol02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkIsol02.UseVisualStyleBackColor = false;
            // 
            // chkIsol04
            // 
            this.chkIsol04.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIsol04.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkIsol04.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkIsol04.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkIsol04.Location = new System.Drawing.Point(5, 119);
            this.chkIsol04.Name = "chkIsol04";
            this.chkIsol04.Size = new System.Drawing.Size(80, 28);
            this.chkIsol04.TabIndex = 16;
            this.chkIsol04.Text = "ISOL 04";
            this.chkIsol04.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkIsol04.UseVisualStyleBackColor = false;
            // 
            // chkIsol03
            // 
            this.chkIsol03.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIsol03.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkIsol03.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkIsol03.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkIsol03.Location = new System.Drawing.Point(5, 89);
            this.chkIsol03.Name = "chkIsol03";
            this.chkIsol03.Size = new System.Drawing.Size(80, 28);
            this.chkIsol03.TabIndex = 16;
            this.chkIsol03.Text = "ISOL 03";
            this.chkIsol03.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkIsol03.UseVisualStyleBackColor = false;
            // 
            // chkStageGlassSensor1
            // 
            this.chkStageGlassSensor1.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkStageGlassSensor1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkStageGlassSensor1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkStageGlassSensor1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkStageGlassSensor1.Location = new System.Drawing.Point(3, 236);
            this.chkStageGlassSensor1.Name = "chkStageGlassSensor1";
            this.chkStageGlassSensor1.Size = new System.Drawing.Size(169, 28);
            this.chkStageGlassSensor1.TabIndex = 10;
            this.chkStageGlassSensor1.Text = "Stage Glass Sensor 1";
            this.chkStageGlassSensor1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkStageGlassSensor1.UseVisualStyleBackColor = false;
            // 
            // chkSafetyModeKeySW
            // 
            this.chkSafetyModeKeySW.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSafetyModeKeySW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkSafetyModeKeySW.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkSafetyModeKeySW.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkSafetyModeKeySW.Location = new System.Drawing.Point(3, 210);
            this.chkSafetyModeKeySW.Name = "chkSafetyModeKeySW";
            this.chkSafetyModeKeySW.Size = new System.Drawing.Size(169, 28);
            this.chkSafetyModeKeySW.TabIndex = 23;
            this.chkSafetyModeKeySW.Text = "Safety Mode Key S/W";
            this.chkSafetyModeKeySW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSafetyModeKeySW.UseVisualStyleBackColor = false;
            // 
            // chkRobotArmCheck
            // 
            this.chkRobotArmCheck.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRobotArmCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkRobotArmCheck.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkRobotArmCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkRobotArmCheck.Location = new System.Drawing.Point(2, 156);
            this.chkRobotArmCheck.Name = "chkRobotArmCheck";
            this.chkRobotArmCheck.Size = new System.Drawing.Size(82, 49);
            this.chkRobotArmCheck.TabIndex = 23;
            this.chkRobotArmCheck.Text = "Robot Arm Check";
            this.chkRobotArmCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkRobotArmCheck.UseVisualStyleBackColor = false;
            // 
            // chkCoolineMainAir
            // 
            this.chkCoolineMainAir.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkCoolineMainAir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkCoolineMainAir.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkCoolineMainAir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkCoolineMainAir.Location = new System.Drawing.Point(2, 62);
            this.chkCoolineMainAir.Name = "chkCoolineMainAir";
            this.chkCoolineMainAir.Size = new System.Drawing.Size(170, 28);
            this.chkCoolineMainAir.TabIndex = 23;
            this.chkCoolineMainAir.Text = "Cooline Main Air";
            this.chkCoolineMainAir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkCoolineMainAir.UseVisualStyleBackColor = false;
            // 
            // chkIonizerMainAir
            // 
            this.chkIonizerMainAir.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIonizerMainAir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkIonizerMainAir.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkIonizerMainAir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkIonizerMainAir.Location = new System.Drawing.Point(2, 91);
            this.chkIonizerMainAir.Name = "chkIonizerMainAir";
            this.chkIonizerMainAir.Size = new System.Drawing.Size(170, 28);
            this.chkIonizerMainAir.TabIndex = 23;
            this.chkIonizerMainAir.Text = "Ionizer Main Air";
            this.chkIonizerMainAir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkIonizerMainAir.UseVisualStyleBackColor = false;
            // 
            // chkIonizer1On
            // 
            this.chkIonizer1On.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIonizer1On.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkIonizer1On.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkIonizer1On.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkIonizer1On.Location = new System.Drawing.Point(5, 31);
            this.chkIonizer1On.Name = "chkIonizer1On";
            this.chkIonizer1On.Size = new System.Drawing.Size(80, 28);
            this.chkIonizer1On.TabIndex = 16;
            this.chkIonizer1On.Text = "Ionizer 1";
            this.chkIonizer1On.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkIonizer1On.UseVisualStyleBackColor = false;
            // 
            // chkIonizer2On
            // 
            this.chkIonizer2On.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIonizer2On.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkIonizer2On.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkIonizer2On.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkIonizer2On.Location = new System.Drawing.Point(5, 59);
            this.chkIonizer2On.Name = "chkIonizer2On";
            this.chkIonizer2On.Size = new System.Drawing.Size(80, 28);
            this.chkIonizer2On.TabIndex = 16;
            this.chkIonizer2On.Text = "Ionizer 2";
            this.chkIonizer2On.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkIonizer2On.UseVisualStyleBackColor = false;
            // 
            // chkIonizer4On
            // 
            this.chkIonizer4On.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIonizer4On.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkIonizer4On.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkIonizer4On.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkIonizer4On.Location = new System.Drawing.Point(5, 119);
            this.chkIonizer4On.Name = "chkIonizer4On";
            this.chkIonizer4On.Size = new System.Drawing.Size(80, 28);
            this.chkIonizer4On.TabIndex = 16;
            this.chkIonizer4On.Text = "Ionizer 4";
            this.chkIonizer4On.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkIonizer4On.UseVisualStyleBackColor = false;
            // 
            // chkIonizer3On
            // 
            this.chkIonizer3On.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIonizer3On.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkIonizer3On.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkIonizer3On.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkIonizer3On.Location = new System.Drawing.Point(5, 89);
            this.chkIonizer3On.Name = "chkIonizer3On";
            this.chkIonizer3On.Size = new System.Drawing.Size(80, 28);
            this.chkIonizer3On.TabIndex = 16;
            this.chkIonizer3On.Text = "Ionizer 3";
            this.chkIonizer3On.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkIonizer3On.UseVisualStyleBackColor = false;
            // 
            // chkCameraCooling
            // 
            this.chkCameraCooling.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkCameraCooling.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkCameraCooling.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkCameraCooling.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkCameraCooling.Location = new System.Drawing.Point(3, 120);
            this.chkCameraCooling.Name = "chkCameraCooling";
            this.chkCameraCooling.Size = new System.Drawing.Size(169, 28);
            this.chkCameraCooling.TabIndex = 10;
            this.chkCameraCooling.Text = "Camera Cooling";
            this.chkCameraCooling.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkCameraCooling.UseVisualStyleBackColor = false;
            // 
            // chkIonizerCover
            // 
            this.chkIonizerCover.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIonizerCover.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkIonizerCover.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkIonizerCover.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkIonizerCover.Location = new System.Drawing.Point(3, 149);
            this.chkIonizerCover.Name = "chkIonizerCover";
            this.chkIonizerCover.Size = new System.Drawing.Size(169, 28);
            this.chkIonizerCover.TabIndex = 83;
            this.chkIonizerCover.Text = "Ionizer Cover";
            this.chkIonizerCover.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkIonizerCover.UseVisualStyleBackColor = false;
            // 
            // chkBlower1
            // 
            this.chkBlower1.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBlower1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkBlower1.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBlower1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkBlower1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkBlower1.Location = new System.Drawing.Point(3, 28);
            this.chkBlower1.Name = "chkBlower1";
            this.chkBlower1.Size = new System.Drawing.Size(80, 28);
            this.chkBlower1.TabIndex = 10;
            this.chkBlower1.Text = "Blower 1";
            this.chkBlower1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBlower1.UseVisualStyleBackColor = false;
            // 
            // chkBlower2
            // 
            this.chkBlower2.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBlower2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkBlower2.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBlower2.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkBlower2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkBlower2.Location = new System.Drawing.Point(3, 57);
            this.chkBlower2.Name = "chkBlower2";
            this.chkBlower2.Size = new System.Drawing.Size(80, 28);
            this.chkBlower2.TabIndex = 84;
            this.chkBlower2.Text = "Blower 2";
            this.chkBlower2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBlower2.UseVisualStyleBackColor = false;
            // 
            // chkBlower3
            // 
            this.chkBlower3.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBlower3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkBlower3.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBlower3.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkBlower3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkBlower3.Location = new System.Drawing.Point(3, 86);
            this.chkBlower3.Name = "chkBlower3";
            this.chkBlower3.Size = new System.Drawing.Size(80, 28);
            this.chkBlower3.TabIndex = 10;
            this.chkBlower3.Text = "Blower 3";
            this.chkBlower3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBlower3.UseVisualStyleBackColor = false;
            // 
            // chkBlower4
            // 
            this.chkBlower4.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBlower4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkBlower4.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBlower4.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkBlower4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkBlower4.Location = new System.Drawing.Point(3, 116);
            this.chkBlower4.Name = "chkBlower4";
            this.chkBlower4.Size = new System.Drawing.Size(80, 28);
            this.chkBlower4.TabIndex = 84;
            this.chkBlower4.Text = "Blower 4";
            this.chkBlower4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBlower4.UseVisualStyleBackColor = false;
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            // 
            // chkIonizer
            // 
            this.chkIonizer.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIonizer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkIonizer.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkIonizer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkIonizer.Location = new System.Drawing.Point(3, 178);
            this.chkIonizer.Name = "chkIonizer";
            this.chkIonizer.Size = new System.Drawing.Size(169, 28);
            this.chkIonizer.TabIndex = 83;
            this.chkIonizer.Text = "Ionizer";
            this.chkIonizer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkIonizer.UseVisualStyleBackColor = false;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.lblScanStep);
            this.panel6.Controls.Add(this.lblInspGUnloadingOk);
            this.panel6.Controls.Add(this.lblInspGUnloading);
            this.panel6.Controls.Add(this.lblInspScanEnd);
            this.panel6.Controls.Add(this.lblInspScanEndOk);
            this.panel6.Controls.Add(this.lblInspScanStartOk);
            this.panel6.Controls.Add(this.lblInspGLoadingOk);
            this.panel6.Controls.Add(this.lblInspScanStart);
            this.panel6.Controls.Add(this.lblInspScanReadyOk);
            this.panel6.Controls.Add(this.lblInspScanReady);
            this.panel6.Controls.Add(this.lblInspGLoading);
            this.panel6.Controls.Add(this.label25);
            this.panel6.Controls.Add(this.label24);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Location = new System.Drawing.Point(1235, 641);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(194, 257);
            this.panel6.TabIndex = 89;
            // 
            // lblScanStep
            // 
            this.lblScanStep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblScanStep.Location = new System.Drawing.Point(87, 161);
            this.lblScanStep.Name = "lblScanStep";
            this.lblScanStep.Size = new System.Drawing.Size(98, 19);
            this.lblScanStep.TabIndex = 39;
            this.lblScanStep.Text = "0.0";
            this.lblScanStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspGUnloadingOk
            // 
            this.lblInspGUnloadingOk.AutoEllipsis = true;
            this.lblInspGUnloadingOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInspGUnloadingOk.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInspGUnloadingOk.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInspGUnloadingOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblInspGUnloadingOk.Location = new System.Drawing.Point(87, 136);
            this.lblInspGUnloadingOk.Name = "lblInspGUnloadingOk";
            this.lblInspGUnloadingOk.Size = new System.Drawing.Size(98, 20);
            this.lblInspGUnloadingOk.TabIndex = 77;
            this.lblInspGUnloadingOk.Text = "G Unloading Ok";
            this.lblInspGUnloadingOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspGUnloading
            // 
            this.lblInspGUnloading.AutoEllipsis = true;
            this.lblInspGUnloading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInspGUnloading.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInspGUnloading.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInspGUnloading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblInspGUnloading.Location = new System.Drawing.Point(4, 136);
            this.lblInspGUnloading.Name = "lblInspGUnloading";
            this.lblInspGUnloading.Size = new System.Drawing.Size(80, 20);
            this.lblInspGUnloading.TabIndex = 76;
            this.lblInspGUnloading.Text = "G Unloading";
            this.lblInspGUnloading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspScanEnd
            // 
            this.lblInspScanEnd.AutoEllipsis = true;
            this.lblInspScanEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInspScanEnd.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInspScanEnd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblInspScanEnd.Location = new System.Drawing.Point(4, 114);
            this.lblInspScanEnd.Name = "lblInspScanEnd";
            this.lblInspScanEnd.Size = new System.Drawing.Size(80, 20);
            this.lblInspScanEnd.TabIndex = 74;
            this.lblInspScanEnd.Text = "Scan End";
            this.lblInspScanEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspScanEndOk
            // 
            this.lblInspScanEndOk.AutoEllipsis = true;
            this.lblInspScanEndOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInspScanEndOk.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInspScanEndOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblInspScanEndOk.Location = new System.Drawing.Point(87, 114);
            this.lblInspScanEndOk.Name = "lblInspScanEndOk";
            this.lblInspScanEndOk.Size = new System.Drawing.Size(98, 20);
            this.lblInspScanEndOk.TabIndex = 75;
            this.lblInspScanEndOk.Text = "Scan End Ok";
            this.lblInspScanEndOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspScanStartOk
            // 
            this.lblInspScanStartOk.AutoEllipsis = true;
            this.lblInspScanStartOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInspScanStartOk.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInspScanStartOk.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInspScanStartOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblInspScanStartOk.Location = new System.Drawing.Point(87, 93);
            this.lblInspScanStartOk.Name = "lblInspScanStartOk";
            this.lblInspScanStartOk.Size = new System.Drawing.Size(98, 20);
            this.lblInspScanStartOk.TabIndex = 73;
            this.lblInspScanStartOk.Text = "Scan Start Ok";
            this.lblInspScanStartOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspGLoadingOk
            // 
            this.lblInspGLoadingOk.AutoEllipsis = true;
            this.lblInspGLoadingOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInspGLoadingOk.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInspGLoadingOk.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInspGLoadingOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblInspGLoadingOk.Location = new System.Drawing.Point(87, 49);
            this.lblInspGLoadingOk.Name = "lblInspGLoadingOk";
            this.lblInspGLoadingOk.Size = new System.Drawing.Size(98, 20);
            this.lblInspGLoadingOk.TabIndex = 72;
            this.lblInspGLoadingOk.Text = "G Loading Ok";
            this.lblInspGLoadingOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspScanStart
            // 
            this.lblInspScanStart.AutoEllipsis = true;
            this.lblInspScanStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInspScanStart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInspScanStart.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInspScanStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblInspScanStart.Location = new System.Drawing.Point(4, 93);
            this.lblInspScanStart.Name = "lblInspScanStart";
            this.lblInspScanStart.Size = new System.Drawing.Size(80, 20);
            this.lblInspScanStart.TabIndex = 71;
            this.lblInspScanStart.Text = "Scan Start";
            this.lblInspScanStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspScanReadyOk
            // 
            this.lblInspScanReadyOk.AutoEllipsis = true;
            this.lblInspScanReadyOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInspScanReadyOk.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInspScanReadyOk.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInspScanReadyOk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblInspScanReadyOk.Location = new System.Drawing.Point(87, 71);
            this.lblInspScanReadyOk.Name = "lblInspScanReadyOk";
            this.lblInspScanReadyOk.Size = new System.Drawing.Size(98, 20);
            this.lblInspScanReadyOk.TabIndex = 69;
            this.lblInspScanReadyOk.Text = "Scan Ready Ok";
            this.lblInspScanReadyOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspScanReady
            // 
            this.lblInspScanReady.AutoEllipsis = true;
            this.lblInspScanReady.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInspScanReady.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInspScanReady.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInspScanReady.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblInspScanReady.Location = new System.Drawing.Point(4, 71);
            this.lblInspScanReady.Name = "lblInspScanReady";
            this.lblInspScanReady.Size = new System.Drawing.Size(80, 20);
            this.lblInspScanReady.TabIndex = 70;
            this.lblInspScanReady.Text = "Scan Ready";
            this.lblInspScanReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInspGLoading
            // 
            this.lblInspGLoading.AutoEllipsis = true;
            this.lblInspGLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblInspGLoading.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInspGLoading.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInspGLoading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblInspGLoading.Location = new System.Drawing.Point(4, 49);
            this.lblInspGLoading.Name = "lblInspGLoading";
            this.lblInspGLoading.Size = new System.Drawing.Size(80, 20);
            this.lblInspGLoading.TabIndex = 68;
            this.lblInspGLoading.Text = "G Loading";
            this.lblInspGLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label25
            // 
            this.label25.AutoEllipsis = true;
            this.label25.BackColor = System.Drawing.Color.Silver;
            this.label25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label25.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.label25.ForeColor = System.Drawing.Color.Firebrick;
            this.label25.Location = new System.Drawing.Point(87, 26);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(98, 21);
            this.label25.TabIndex = 10;
            this.label25.Text = "OUT";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label24
            // 
            this.label24.AutoEllipsis = true;
            this.label24.BackColor = System.Drawing.Color.Silver;
            this.label24.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label24.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.label24.ForeColor = System.Drawing.Color.Firebrick;
            this.label24.Location = new System.Drawing.Point(4, 26);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(80, 21);
            this.label24.TabIndex = 10;
            this.label24.Text = "IN";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "■ INSP PC";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMemDetail
            // 
            this.btnMemDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnMemDetail.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnMemDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnMemDetail.Location = new System.Drawing.Point(105, 93);
            this.btnMemDetail.Name = "btnMemDetail";
            this.btnMemDetail.Size = new System.Drawing.Size(97, 50);
            this.btnMemDetail.TabIndex = 90;
            this.btnMemDetail.Text = "모니터 INSP & REV IO";
            this.btnMemDetail.UseVisualStyleBackColor = false;
            this.btnMemDetail.Click += new System.EventHandler(this.btnMemDetail_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnAllDoor);
            this.panel3.Controls.Add(this.chkDoor02);
            this.panel3.Controls.Add(this.chkDoor05);
            this.panel3.Controls.Add(this.chkDoor04);
            this.panel3.Controls.Add(this.chkDoor01);
            this.panel3.Controls.Add(this.chkDoor03);
            this.panel3.Controls.Add(this.chkDoor06);
            this.panel3.Controls.Add(this.chkDoor15);
            this.panel3.Controls.Add(this.chkDoor14);
            this.panel3.Controls.Add(this.chkDoor13);
            this.panel3.Controls.Add(this.chkDoor12);
            this.panel3.Controls.Add(this.chkDoor11);
            this.panel3.Controls.Add(this.chkDoor10);
            this.panel3.Controls.Add(this.chkDoor07);
            this.panel3.Controls.Add(this.chkDoor09);
            this.panel3.Controls.Add(this.chkDoor08);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Location = new System.Drawing.Point(1525, 235);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(267, 210);
            this.panel3.TabIndex = 89;
            // 
            // btnAllDoor
            // 
            this.btnAllDoor.Location = new System.Drawing.Point(202, 29);
            this.btnAllDoor.Name = "btnAllDoor";
            this.btnAllDoor.Size = new System.Drawing.Size(61, 28);
            this.btnAllDoor.TabIndex = 56;
            this.btnAllDoor.Text = "All";
            this.btnAllDoor.UseVisualStyleBackColor = true;
            this.btnAllDoor.Click += new System.EventHandler(this.btnAllDoor_Click);
            // 
            // chkDoor02
            // 
            this.chkDoor02.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDoor02.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDoor02.Font = new System.Drawing.Font("맑은 고딕", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDoor02.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkDoor02.Location = new System.Drawing.Point(7, 58);
            this.chkDoor02.Name = "chkDoor02";
            this.chkDoor02.Size = new System.Drawing.Size(61, 28);
            this.chkDoor02.TabIndex = 51;
            this.chkDoor02.Text = "T Door 02";
            this.chkDoor02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDoor02.UseVisualStyleBackColor = false;
            // 
            // chkDoor05
            // 
            this.chkDoor05.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDoor05.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDoor05.Font = new System.Drawing.Font("맑은 고딕", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDoor05.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkDoor05.Location = new System.Drawing.Point(7, 148);
            this.chkDoor05.Name = "chkDoor05";
            this.chkDoor05.Size = new System.Drawing.Size(61, 28);
            this.chkDoor05.TabIndex = 52;
            this.chkDoor05.Text = "T Door 05";
            this.chkDoor05.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDoor05.UseVisualStyleBackColor = false;
            // 
            // chkDoor04
            // 
            this.chkDoor04.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDoor04.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDoor04.Font = new System.Drawing.Font("맑은 고딕", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDoor04.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkDoor04.Location = new System.Drawing.Point(7, 118);
            this.chkDoor04.Name = "chkDoor04";
            this.chkDoor04.Size = new System.Drawing.Size(61, 28);
            this.chkDoor04.TabIndex = 53;
            this.chkDoor04.Text = "T Door 04";
            this.chkDoor04.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDoor04.UseVisualStyleBackColor = false;
            // 
            // chkDoor01
            // 
            this.chkDoor01.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDoor01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDoor01.Font = new System.Drawing.Font("맑은 고딕", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDoor01.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkDoor01.Location = new System.Drawing.Point(7, 30);
            this.chkDoor01.Name = "chkDoor01";
            this.chkDoor01.Size = new System.Drawing.Size(61, 28);
            this.chkDoor01.TabIndex = 55;
            this.chkDoor01.Text = "T Door 01";
            this.chkDoor01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDoor01.UseVisualStyleBackColor = false;
            // 
            // chkDoor03
            // 
            this.chkDoor03.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDoor03.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDoor03.Font = new System.Drawing.Font("맑은 고딕", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDoor03.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkDoor03.Location = new System.Drawing.Point(7, 88);
            this.chkDoor03.Name = "chkDoor03";
            this.chkDoor03.Size = new System.Drawing.Size(61, 28);
            this.chkDoor03.TabIndex = 54;
            this.chkDoor03.Text = "T Door 03";
            this.chkDoor03.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDoor03.UseVisualStyleBackColor = false;
            // 
            // chkDoor06
            // 
            this.chkDoor06.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDoor06.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDoor06.Font = new System.Drawing.Font("맑은 고딕", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDoor06.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkDoor06.Location = new System.Drawing.Point(68, 30);
            this.chkDoor06.Name = "chkDoor06";
            this.chkDoor06.Size = new System.Drawing.Size(61, 28);
            this.chkDoor06.TabIndex = 47;
            this.chkDoor06.Text = "T Door 06";
            this.chkDoor06.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDoor06.UseVisualStyleBackColor = false;
            // 
            // chkDoor15
            // 
            this.chkDoor15.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDoor15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDoor15.Font = new System.Drawing.Font("맑은 고딕", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDoor15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkDoor15.Location = new System.Drawing.Point(135, 147);
            this.chkDoor15.Name = "chkDoor15";
            this.chkDoor15.Size = new System.Drawing.Size(61, 28);
            this.chkDoor15.TabIndex = 46;
            this.chkDoor15.Text = "T Door 15";
            this.chkDoor15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDoor15.UseVisualStyleBackColor = false;
            // 
            // chkDoor14
            // 
            this.chkDoor14.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDoor14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDoor14.Font = new System.Drawing.Font("맑은 고딕", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDoor14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkDoor14.Location = new System.Drawing.Point(135, 116);
            this.chkDoor14.Name = "chkDoor14";
            this.chkDoor14.Size = new System.Drawing.Size(61, 28);
            this.chkDoor14.TabIndex = 46;
            this.chkDoor14.Text = "T Door 14";
            this.chkDoor14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDoor14.UseVisualStyleBackColor = false;
            // 
            // chkDoor13
            // 
            this.chkDoor13.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDoor13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDoor13.Font = new System.Drawing.Font("맑은 고딕", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDoor13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkDoor13.Location = new System.Drawing.Point(135, 88);
            this.chkDoor13.Name = "chkDoor13";
            this.chkDoor13.Size = new System.Drawing.Size(61, 28);
            this.chkDoor13.TabIndex = 46;
            this.chkDoor13.Text = "T Door 13";
            this.chkDoor13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDoor13.UseVisualStyleBackColor = false;
            // 
            // chkDoor12
            // 
            this.chkDoor12.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDoor12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDoor12.Font = new System.Drawing.Font("맑은 고딕", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDoor12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkDoor12.Location = new System.Drawing.Point(135, 58);
            this.chkDoor12.Name = "chkDoor12";
            this.chkDoor12.Size = new System.Drawing.Size(61, 28);
            this.chkDoor12.TabIndex = 46;
            this.chkDoor12.Text = "T Door 12";
            this.chkDoor12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDoor12.UseVisualStyleBackColor = false;
            // 
            // chkDoor11
            // 
            this.chkDoor11.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDoor11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDoor11.Font = new System.Drawing.Font("맑은 고딕", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDoor11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkDoor11.Location = new System.Drawing.Point(135, 29);
            this.chkDoor11.Name = "chkDoor11";
            this.chkDoor11.Size = new System.Drawing.Size(61, 28);
            this.chkDoor11.TabIndex = 46;
            this.chkDoor11.Text = "T Door 11";
            this.chkDoor11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDoor11.UseVisualStyleBackColor = false;
            // 
            // chkDoor10
            // 
            this.chkDoor10.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDoor10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDoor10.Font = new System.Drawing.Font("맑은 고딕", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDoor10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkDoor10.Location = new System.Drawing.Point(68, 148);
            this.chkDoor10.Name = "chkDoor10";
            this.chkDoor10.Size = new System.Drawing.Size(61, 28);
            this.chkDoor10.TabIndex = 46;
            this.chkDoor10.Text = "T Door 10";
            this.chkDoor10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDoor10.UseVisualStyleBackColor = false;
            // 
            // chkDoor07
            // 
            this.chkDoor07.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDoor07.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDoor07.Font = new System.Drawing.Font("맑은 고딕", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDoor07.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkDoor07.Location = new System.Drawing.Point(68, 58);
            this.chkDoor07.Name = "chkDoor07";
            this.chkDoor07.Size = new System.Drawing.Size(61, 28);
            this.chkDoor07.TabIndex = 48;
            this.chkDoor07.Text = "T Door 07";
            this.chkDoor07.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDoor07.UseVisualStyleBackColor = false;
            // 
            // chkDoor09
            // 
            this.chkDoor09.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDoor09.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDoor09.Font = new System.Drawing.Font("맑은 고딕", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDoor09.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkDoor09.Location = new System.Drawing.Point(68, 118);
            this.chkDoor09.Name = "chkDoor09";
            this.chkDoor09.Size = new System.Drawing.Size(61, 28);
            this.chkDoor09.TabIndex = 50;
            this.chkDoor09.Text = "T Door 09";
            this.chkDoor09.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDoor09.UseVisualStyleBackColor = false;
            // 
            // chkDoor08
            // 
            this.chkDoor08.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkDoor08.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkDoor08.Font = new System.Drawing.Font("맑은 고딕", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkDoor08.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkDoor08.Location = new System.Drawing.Point(68, 88);
            this.chkDoor08.Name = "chkDoor08";
            this.chkDoor08.Size = new System.Drawing.Size(61, 28);
            this.chkDoor08.TabIndex = 49;
            this.chkDoor08.Text = "T Door 08";
            this.chkDoor08.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDoor08.UseVisualStyleBackColor = false;
            // 
            // label15
            // 
            this.label15.AutoEllipsis = true;
            this.label15.BackColor = System.Drawing.Color.Gainsboro;
            this.label15.Dock = System.Windows.Forms.DockStyle.Top;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(0, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(265, 24);
            this.label15.TabIndex = 9;
            this.label15.Text = "■ DOOR";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.chkEmo02);
            this.panel4.Controls.Add(this.chkEmo01);
            this.panel4.Controls.Add(this.chkEmo03);
            this.panel4.Controls.Add(this.chkEmo06);
            this.panel4.Controls.Add(this.chkEmo05);
            this.panel4.Controls.Add(this.chkEmo04);
            this.panel4.Location = new System.Drawing.Point(1239, 448);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(91, 191);
            this.panel4.TabIndex = 89;
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.BackColor = System.Drawing.Color.Gainsboro;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 24);
            this.label4.TabIndex = 9;
            this.label4.Text = "■ EMO";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkEmo06
            // 
            this.chkEmo06.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkEmo06.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkEmo06.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkEmo06.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkEmo06.Location = new System.Drawing.Point(6, 165);
            this.chkEmo06.Name = "chkEmo06";
            this.chkEmo06.Size = new System.Drawing.Size(80, 28);
            this.chkEmo06.TabIndex = 9;
            this.chkEmo06.Text = "EMO06";
            this.chkEmo06.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkEmo06.UseVisualStyleBackColor = false;
            // 
            // chkEmo05
            // 
            this.chkEmo05.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkEmo05.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkEmo05.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkEmo05.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkEmo05.Location = new System.Drawing.Point(6, 138);
            this.chkEmo05.Name = "chkEmo05";
            this.chkEmo05.Size = new System.Drawing.Size(80, 28);
            this.chkEmo05.TabIndex = 9;
            this.chkEmo05.Text = "EMO05";
            this.chkEmo05.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkEmo05.UseVisualStyleBackColor = false;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.btnGlassCrackRightOn);
            this.panel5.Controls.Add(this.btnGlassCrackLeftOn);
            this.panel5.Controls.Add(this.btnCrackOff);
            this.panel5.Controls.Add(this.btnGlassCrackSepOn);
            this.panel5.Controls.Add(this.btnGlassCrackOrgOn);
            this.panel5.Controls.Add(this.dgvGlassEdge);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Location = new System.Drawing.Point(1333, 448);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(92, 191);
            this.panel5.TabIndex = 89;
            // 
            // btnGlassCrackRightOn
            // 
            this.btnGlassCrackRightOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnGlassCrackRightOn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnGlassCrackRightOn.Location = new System.Drawing.Point(42, 53);
            this.btnGlassCrackRightOn.Name = "btnGlassCrackRightOn";
            this.btnGlassCrackRightOn.Size = new System.Drawing.Size(49, 27);
            this.btnGlassCrackRightOn.TabIndex = 97;
            this.btnGlassCrackRightOn.Text = "Right";
            this.btnGlassCrackRightOn.UseVisualStyleBackColor = false;
            this.btnGlassCrackRightOn.Click += new System.EventHandler(this.btnGlassCrackRightOn_Click);
            // 
            // btnGlassCrackLeftOn
            // 
            this.btnGlassCrackLeftOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnGlassCrackLeftOn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnGlassCrackLeftOn.Location = new System.Drawing.Point(0, 53);
            this.btnGlassCrackLeftOn.Name = "btnGlassCrackLeftOn";
            this.btnGlassCrackLeftOn.Size = new System.Drawing.Size(48, 27);
            this.btnGlassCrackLeftOn.TabIndex = 96;
            this.btnGlassCrackLeftOn.Text = "Left";
            this.btnGlassCrackLeftOn.UseVisualStyleBackColor = false;
            this.btnGlassCrackLeftOn.Click += new System.EventHandler(this.btnGlassCrackLeftOn_Click);
            // 
            // btnCrackOff
            // 
            this.btnCrackOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCrackOff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnCrackOff.Location = new System.Drawing.Point(66, 26);
            this.btnCrackOff.Name = "btnCrackOff";
            this.btnCrackOff.Size = new System.Drawing.Size(25, 27);
            this.btnCrackOff.TabIndex = 95;
            this.btnCrackOff.Text = "N";
            this.btnCrackOff.UseVisualStyleBackColor = false;
            this.btnCrackOff.Click += new System.EventHandler(this.btnCrackOff_Click);
            // 
            // btnGlassCrackSepOn
            // 
            this.btnGlassCrackSepOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnGlassCrackSepOn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnGlassCrackSepOn.Location = new System.Drawing.Point(33, 26);
            this.btnGlassCrackSepOn.Name = "btnGlassCrackSepOn";
            this.btnGlassCrackSepOn.Size = new System.Drawing.Size(36, 27);
            this.btnGlassCrackSepOn.TabIndex = 94;
            this.btnGlassCrackSepOn.Text = "Sep";
            this.btnGlassCrackSepOn.UseVisualStyleBackColor = false;
            this.btnGlassCrackSepOn.Click += new System.EventHandler(this.btnGlassCrackSepOn_Click);
            // 
            // btnGlassCrackOrgOn
            // 
            this.btnGlassCrackOrgOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnGlassCrackOrgOn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnGlassCrackOrgOn.Location = new System.Drawing.Point(0, 26);
            this.btnGlassCrackOrgOn.Name = "btnGlassCrackOrgOn";
            this.btnGlassCrackOrgOn.Size = new System.Drawing.Size(36, 27);
            this.btnGlassCrackOrgOn.TabIndex = 93;
            this.btnGlassCrackOrgOn.Text = "Org";
            this.btnGlassCrackOrgOn.UseVisualStyleBackColor = false;
            this.btnGlassCrackOrgOn.Click += new System.EventHandler(this.btnGlassCrackOrgOn_Click);
            // 
            // dgvGlassEdge
            // 
            this.dgvGlassEdge.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGlassEdge.ColumnHeadersVisible = false;
            this.dgvGlassEdge.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewButtonColumn1});
            this.dgvGlassEdge.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvGlassEdge.Location = new System.Drawing.Point(0, 80);
            this.dgvGlassEdge.Name = "dgvGlassEdge";
            this.dgvGlassEdge.RowHeadersVisible = false;
            this.dgvGlassEdge.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvGlassEdge.RowTemplate.Height = 23;
            this.dgvGlassEdge.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGlassEdge.Size = new System.Drawing.Size(90, 109);
            this.dgvGlassEdge.TabIndex = 25;
            // 
            // dataGridViewButtonColumn1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.dataGridViewButtonColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewButtonColumn1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.dataGridViewButtonColumn1.HeaderText = "Vacuum";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewButtonColumn1.Width = 85;
            // 
            // label5
            // 
            this.label5.AutoEllipsis = true;
            this.label5.BackColor = System.Drawing.Color.Gainsboro;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 24);
            this.label5.TabIndex = 9;
            this.label5.Text = "■ GLASS EDGE";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.label6);
            this.panel7.Controls.Add(this.chkIsol01);
            this.panel7.Controls.Add(this.chkIsol02);
            this.panel7.Controls.Add(this.chkIsol04);
            this.panel7.Controls.Add(this.chkIsol03);
            this.panel7.Location = new System.Drawing.Point(1428, 448);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(92, 191);
            this.panel7.TabIndex = 89;
            // 
            // label6
            // 
            this.label6.AutoEllipsis = true;
            this.label6.BackColor = System.Drawing.Color.Gainsboro;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 24);
            this.label6.TabIndex = 9;
            this.label6.Text = "■ ISOL";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.label7);
            this.panel8.Controls.Add(this.chkIonizer1Alarm);
            this.panel8.Controls.Add(this.chkIonizer2Alarm);
            this.panel8.Controls.Add(this.chkIonizer1On);
            this.panel8.Controls.Add(this.chkIonizer4Alarm);
            this.panel8.Controls.Add(this.chkIonizer2On);
            this.panel8.Controls.Add(this.chkIonizer3Alarm);
            this.panel8.Controls.Add(this.chkIonizer4On);
            this.panel8.Controls.Add(this.chkIonizer3On);
            this.panel8.Location = new System.Drawing.Point(1524, 448);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(217, 191);
            this.panel8.TabIndex = 89;
            // 
            // label7
            // 
            this.label7.AutoEllipsis = true;
            this.label7.BackColor = System.Drawing.Color.Gainsboro;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(215, 24);
            this.label7.TabIndex = 9;
            this.label7.Text = "■ IONIZER";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkIonizer1Alarm
            // 
            this.chkIonizer1Alarm.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIonizer1Alarm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkIonizer1Alarm.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkIonizer1Alarm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkIonizer1Alarm.Location = new System.Drawing.Point(91, 31);
            this.chkIonizer1Alarm.Name = "chkIonizer1Alarm";
            this.chkIonizer1Alarm.Size = new System.Drawing.Size(80, 28);
            this.chkIonizer1Alarm.TabIndex = 16;
            this.chkIonizer1Alarm.Text = "Alarm 1";
            this.chkIonizer1Alarm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkIonizer1Alarm.UseVisualStyleBackColor = false;
            // 
            // chkIonizer2Alarm
            // 
            this.chkIonizer2Alarm.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIonizer2Alarm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkIonizer2Alarm.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkIonizer2Alarm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkIonizer2Alarm.Location = new System.Drawing.Point(91, 59);
            this.chkIonizer2Alarm.Name = "chkIonizer2Alarm";
            this.chkIonizer2Alarm.Size = new System.Drawing.Size(80, 28);
            this.chkIonizer2Alarm.TabIndex = 16;
            this.chkIonizer2Alarm.Text = "Alarm 2";
            this.chkIonizer2Alarm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkIonizer2Alarm.UseVisualStyleBackColor = false;
            // 
            // chkIonizer4Alarm
            // 
            this.chkIonizer4Alarm.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIonizer4Alarm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkIonizer4Alarm.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkIonizer4Alarm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkIonizer4Alarm.Location = new System.Drawing.Point(91, 119);
            this.chkIonizer4Alarm.Name = "chkIonizer4Alarm";
            this.chkIonizer4Alarm.Size = new System.Drawing.Size(80, 28);
            this.chkIonizer4Alarm.TabIndex = 16;
            this.chkIonizer4Alarm.Text = "Alarm 4";
            this.chkIonizer4Alarm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkIonizer4Alarm.UseVisualStyleBackColor = false;
            // 
            // chkIonizer3Alarm
            // 
            this.chkIonizer3Alarm.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkIonizer3Alarm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkIonizer3Alarm.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkIonizer3Alarm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkIonizer3Alarm.Location = new System.Drawing.Point(91, 89);
            this.chkIonizer3Alarm.Name = "chkIonizer3Alarm";
            this.chkIonizer3Alarm.Size = new System.Drawing.Size(80, 28);
            this.chkIonizer3Alarm.TabIndex = 16;
            this.chkIonizer3Alarm.Text = "Alarm 3";
            this.chkIonizer3Alarm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkIonizer3Alarm.UseVisualStyleBackColor = false;
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.dgvVacuum);
            this.panel9.Controls.Add(this.label8);
            this.panel9.Location = new System.Drawing.Point(1238, 234);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(92, 210);
            this.panel9.TabIndex = 89;
            // 
            // dgvVacuum
            // 
            this.dgvVacuum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVacuum.ColumnHeadersVisible = false;
            this.dgvVacuum.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dgvVacuum.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvVacuum.Location = new System.Drawing.Point(0, 27);
            this.dgvVacuum.Name = "dgvVacuum";
            this.dgvVacuum.RowHeadersVisible = false;
            this.dgvVacuum.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvVacuum.RowTemplate.Height = 23;
            this.dgvVacuum.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVacuum.Size = new System.Drawing.Size(90, 181);
            this.dgvVacuum.TabIndex = 24;
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Column1.HeaderText = "Vacuum";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.Width = 85;
            // 
            // label8
            // 
            this.label8.AutoEllipsis = true;
            this.label8.BackColor = System.Drawing.Color.Gainsboro;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 24);
            this.label8.TabIndex = 9;
            this.label8.Text = "■ VACCUM";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.chkBlower6);
            this.panel10.Controls.Add(this.chkBlower5);
            this.panel10.Controls.Add(this.label9);
            this.panel10.Controls.Add(this.chkBlower1);
            this.panel10.Controls.Add(this.chkBlower3);
            this.panel10.Controls.Add(this.chkBlower2);
            this.panel10.Controls.Add(this.chkBlower4);
            this.panel10.Location = new System.Drawing.Point(1334, 234);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(92, 210);
            this.panel10.TabIndex = 89;
            // 
            // chkBlower6
            // 
            this.chkBlower6.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBlower6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkBlower6.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBlower6.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkBlower6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkBlower6.Location = new System.Drawing.Point(4, 177);
            this.chkBlower6.Name = "chkBlower6";
            this.chkBlower6.Size = new System.Drawing.Size(80, 28);
            this.chkBlower6.TabIndex = 85;
            this.chkBlower6.Text = "Blower 6";
            this.chkBlower6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBlower6.UseVisualStyleBackColor = false;
            // 
            // chkBlower5
            // 
            this.chkBlower5.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkBlower5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkBlower5.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBlower5.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkBlower5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkBlower5.Location = new System.Drawing.Point(3, 148);
            this.chkBlower5.Name = "chkBlower5";
            this.chkBlower5.Size = new System.Drawing.Size(80, 28);
            this.chkBlower5.TabIndex = 85;
            this.chkBlower5.Text = "Blower 5";
            this.chkBlower5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBlower5.UseVisualStyleBackColor = false;
            // 
            // label9
            // 
            this.label9.AutoEllipsis = true;
            this.label9.BackColor = System.Drawing.Color.Gainsboro;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 24);
            this.label9.TabIndex = 9;
            this.label9.Text = "■ BLOWER";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel11
            // 
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel11.Controls.Add(this.dgvGlassDetect);
            this.panel11.Controls.Add(this.label12);
            this.panel11.Controls.Add(this.chkRobotArmCheck);
            this.panel11.Location = new System.Drawing.Point(1429, 234);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(92, 210);
            this.panel11.TabIndex = 89;
            // 
            // dgvGlassDetect
            // 
            this.dgvGlassDetect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGlassDetect.ColumnHeadersVisible = false;
            this.dgvGlassDetect.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewButtonColumn2});
            this.dgvGlassDetect.Location = new System.Drawing.Point(0, 27);
            this.dgvGlassDetect.Name = "dgvGlassDetect";
            this.dgvGlassDetect.RowHeadersVisible = false;
            this.dgvGlassDetect.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvGlassDetect.RowTemplate.Height = 23;
            this.dgvGlassDetect.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGlassDetect.Size = new System.Drawing.Size(90, 123);
            this.dgvGlassDetect.TabIndex = 96;
            // 
            // dataGridViewButtonColumn2
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.dataGridViewButtonColumn2.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewButtonColumn2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.dataGridViewButtonColumn2.HeaderText = "Vacuum";
            this.dataGridViewButtonColumn2.Name = "dataGridViewButtonColumn2";
            this.dataGridViewButtonColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewButtonColumn2.Width = 85;
            // 
            // label12
            // 
            this.label12.AutoEllipsis = true;
            this.label12.BackColor = System.Drawing.Color.Gainsboro;
            this.label12.Dock = System.Windows.Forms.DockStyle.Top;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 24);
            this.label12.TabIndex = 9;
            this.label12.Text = "■ GLASS CHK";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel12
            // 
            this.panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel12.Controls.Add(this.label13);
            this.panel12.Controls.Add(this.chkCoolineMainAir);
            this.panel12.Controls.Add(this.chkIonizerMainAir);
            this.panel12.Controls.Add(this.chkCameraCooling);
            this.panel12.Controls.Add(this.chkEnableGripSwOn);
            this.panel12.Controls.Add(this.chkStageGlassSensor1);
            this.panel12.Controls.Add(this.chkSafetyModeKeySW);
            this.panel12.Controls.Add(this.chkIonizerCover);
            this.panel12.Controls.Add(this.chkIonizer);
            this.panel12.Location = new System.Drawing.Point(1743, 448);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(181, 302);
            this.panel12.TabIndex = 90;
            // 
            // label13
            // 
            this.label13.AutoEllipsis = true;
            this.label13.BackColor = System.Drawing.Color.Gainsboro;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(179, 24);
            this.label13.TabIndex = 9;
            this.label13.Text = "■ GLASS CHK";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkEnableGripSwOn
            // 
            this.chkEnableGripSwOn.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkEnableGripSwOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkEnableGripSwOn.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkEnableGripSwOn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkEnableGripSwOn.Location = new System.Drawing.Point(3, 269);
            this.chkEnableGripSwOn.Name = "chkEnableGripSwOn";
            this.chkEnableGripSwOn.Size = new System.Drawing.Size(169, 28);
            this.chkEnableGripSwOn.TabIndex = 10;
            this.chkEnableGripSwOn.Text = "ENABLE_GRIB_SW_ON";
            this.chkEnableGripSwOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkEnableGripSwOn.UseVisualStyleBackColor = false;
            // 
            // pnlView
            // 
            this.pnlView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlView.Controls.Add(this.elementHost1);
            this.pnlView.Controls.Add(this.label14);
            this.pnlView.Controls.Add(this.panel1);
            this.pnlView.Location = new System.Drawing.Point(3, 45);
            this.pnlView.Name = "pnlView";
            this.pnlView.Size = new System.Drawing.Size(790, 670);
            this.pnlView.TabIndex = 89;
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 66);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(788, 602);
            this.elementHost1.TabIndex = 10;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // label14
            // 
            this.label14.AutoEllipsis = true;
            this.label14.BackColor = System.Drawing.Color.Gainsboro;
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(788, 66);
            this.label14.TabIndex = 9;
            this.label14.Text = "■ EQUIP  STATUS";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.label27);
            this.panel1.Controls.Add(this.shInspCam);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.shReviewCam);
            this.panel1.Controls.Add(this.shStage);
            this.panel1.Controls.Add(this.SpThelta);
            this.panel1.Controls.Add(this.spLiftPin);
            this.panel1.Location = new System.Drawing.Point(43, 305);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(49, 34);
            this.panel1.TabIndex = 0;
            this.panel1.Visible = false;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(23, 177);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(49, 15);
            this.label26.TabIndex = 10;
            this.label26.Text = "Litft Pin";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(189, 646);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(39, 15);
            this.label27.TabIndex = 11;
            this.label27.Text = "Thelta";
            // 
            // shInspCam
            // 
            this.shInspCam.BackColor = System.Drawing.Color.Transparent;
            this.shInspCam.Location = new System.Drawing.Point(267, 117);
            this.shInspCam.Name = "shInspCam";
            this.shInspCam.Size = new System.Drawing.Size(91, 403);
            this.shInspCam.TabIndex = 13;
            this.shInspCam.TabStop = false;
            this.shInspCam.Text = "transparentControl1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(138, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(123, 530);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // shReviewCam
            // 
            this.shReviewCam.BackColor = System.Drawing.Color.Transparent;
            this.shReviewCam.Location = new System.Drawing.Point(359, 392);
            this.shReviewCam.Name = "shReviewCam";
            this.shReviewCam.Size = new System.Drawing.Size(169, 102);
            this.shReviewCam.TabIndex = 10;
            this.shReviewCam.TabStop = false;
            this.shReviewCam.Text = "transparentControl1";
            // 
            // shStage
            // 
            this.shStage.BackColor = System.Drawing.Color.Transparent;
            this.shStage.Location = new System.Drawing.Point(177, 255);
            this.shStage.Name = "shStage";
            this.shStage.Size = new System.Drawing.Size(194, 148);
            this.shStage.TabIndex = 11;
            this.shStage.TabStop = false;
            this.shStage.Text = "transparentControl1";
            // 
            // SpThelta
            // 
            this.SpThelta.BackColor = System.Drawing.Color.DarkRed;
            this.SpThelta.Location = new System.Drawing.Point(190, 633);
            this.SpThelta.Name = "SpThelta";
            this.SpThelta.Size = new System.Drawing.Size(10, 10);
            this.SpThelta.TabIndex = 12;
            this.SpThelta.TabStop = false;
            this.SpThelta.Text = "transparentControl1";
            // 
            // spLiftPin
            // 
            this.spLiftPin.BackColor = System.Drawing.Color.Red;
            this.spLiftPin.ForeColor = System.Drawing.Color.Red;
            this.spLiftPin.Location = new System.Drawing.Point(19, 150);
            this.spLiftPin.Name = "spLiftPin";
            this.spLiftPin.Size = new System.Drawing.Size(10, 10);
            this.spLiftPin.TabIndex = 12;
            this.spLiftPin.TabStop = false;
            this.spLiftPin.Text = "transparentControl1";
            
            // 
            // lblScanTime
            // 
            this.lblScanTime.AutoEllipsis = true;
            this.lblScanTime.BackColor = System.Drawing.Color.Gainsboro;
            this.lblScanTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblScanTime.ForeColor = System.Drawing.Color.Black;
            this.lblScanTime.Location = new System.Drawing.Point(203, 15);
            this.lblScanTime.Name = "lblScanTime";
            this.lblScanTime.Size = new System.Drawing.Size(153, 16);
            this.lblScanTime.TabIndex = 85;
            this.lblScanTime.Text = "-";
            this.lblScanTime.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnXyIo
            // 
            this.btnXyIo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnXyIo.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnXyIo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnXyIo.Location = new System.Drawing.Point(208, 93);
            this.btnXyIo.Name = "btnXyIo";
            this.btnXyIo.Size = new System.Drawing.Size(97, 50);
            this.btnXyIo.TabIndex = 90;
            this.btnXyIo.Text = "모니터 XY IO";
            this.btnXyIo.UseVisualStyleBackColor = false;
            this.btnXyIo.Click += new System.EventHandler(this.btnXyIo_Click);
            // 
            // tbStatus
            // 
            this.tbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStatus.Location = new System.Drawing.Point(3, 995);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.ReadOnly = true;
            this.tbStatus.Size = new System.Drawing.Size(1962, 23);
            this.tbStatus.TabIndex = 91;
            // 
            // tmrState
            // 
            this.tmrState.Enabled = true;
            this.tmrState.Interval = 1000;
            this.tmrState.Tick += new System.EventHandler(this.tmrState_Tick);
            // 
            // btnCtrlPcXy
            // 
            this.btnCtrlPcXy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCtrlPcXy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnCtrlPcXy.Location = new System.Drawing.Point(2, 37);
            this.btnCtrlPcXy.Name = "btnCtrlPcXy";
            this.btnCtrlPcXy.Size = new System.Drawing.Size(97, 50);
            this.btnCtrlPcXy.TabIndex = 92;
            this.btnCtrlPcXy.Text = "주소 로드 CTRL PC XY";
            this.btnCtrlPcXy.UseVisualStyleBackColor = false;
            this.btnCtrlPcXy.Click += new System.EventHandler(this.btnCtrlPcXy_Click);
            // 
            // btnSimulPcXy
            // 
            this.btnSimulPcXy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSimulPcXy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnSimulPcXy.Location = new System.Drawing.Point(105, 37);
            this.btnSimulPcXy.Name = "btnSimulPcXy";
            this.btnSimulPcXy.Size = new System.Drawing.Size(97, 50);
            this.btnSimulPcXy.TabIndex = 92;
            this.btnSimulPcXy.Text = "주소 로드 SIMUL PC XY";
            this.btnSimulPcXy.UseVisualStyleBackColor = false;
            this.btnSimulPcXy.Click += new System.EventHandler(this.btnSimulPcXy_Click);
            // 
            // btnXyIoAddr
            // 
            this.btnXyIoAddr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnXyIoAddr.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnXyIoAddr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnXyIoAddr.Location = new System.Drawing.Point(2, 93);
            this.btnXyIoAddr.Name = "btnXyIoAddr";
            this.btnXyIoAddr.Size = new System.Drawing.Size(97, 50);
            this.btnXyIoAddr.TabIndex = 90;
            this.btnXyIoAddr.Text = "모니터 XY IO(ADDR)";
            this.btnXyIoAddr.UseVisualStyleBackColor = false;
            this.btnXyIoAddr.Click += new System.EventHandler(this.btnXyIoAddr_Click);
            // 
            // lstLogger
            // 
            this.lstLogger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstLogger.Location = new System.Drawing.Point(3, 719);
            this.lstLogger.Name = "lstLogger";
            this.lstLogger.Size = new System.Drawing.Size(791, 272);
            this.lstLogger.TabIndex = 94;
            this.lstLogger.UseCompatibleStateImageBehavior = false;
            this.lstLogger.View = System.Windows.Forms.View.Details;
            // 
            // btnTester
            // 
            this.btnTester.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnTester.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnTester.Location = new System.Drawing.Point(208, 37);
            this.btnTester.Name = "btnTester";
            this.btnTester.Size = new System.Drawing.Size(97, 50);
            this.btnTester.TabIndex = 92;
            this.btnTester.Text = "주소 로드 TEST XY";
            this.btnTester.UseVisualStyleBackColor = false;
            this.btnTester.Click += new System.EventHandler(this.btnTester_Click);
            // 
            // label10
            // 
            this.label10.AutoEllipsis = true;
            this.label10.BackColor = System.Drawing.Color.Gainsboro;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(202, 24);
            this.label10.TabIndex = 9;
            this.label10.Text = "■ REV PC";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.BackColor = System.Drawing.Color.Silver;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Firebrick;
            this.label3.Location = new System.Drawing.Point(4, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 21);
            this.label3.TabIndex = 10;
            this.label3.Text = "IN";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.BackColor = System.Drawing.Color.Silver;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Firebrick;
            this.label2.Location = new System.Drawing.Point(87, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 27);
            this.label2.TabIndex = 10;
            this.label2.Text = "OUT";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chkReviLotEndAck);
            this.panel2.Controls.Add(this.chkReviLotStartAck);
            this.panel2.Controls.Add(this.chkReviReviEnd);
            this.panel2.Controls.Add(this.chkReviReviStartAck);
            this.panel2.Controls.Add(this.chkReviAlignStartAck);
            this.panel2.Controls.Add(this.chkReviUnloadingAck);
            this.panel2.Controls.Add(this.chkReviLoadingAck);
            this.panel2.Controls.Add(this.chkReviLotEnd);
            this.panel2.Controls.Add(this.chkReviLotStart);
            this.panel2.Controls.Add(this.chkReviReviEndAck);
            this.panel2.Controls.Add(this.chkReviReviStart);
            this.panel2.Controls.Add(this.chkReviUnloading);
            this.panel2.Controls.Add(this.chkReviAlignStart);
            this.panel2.Controls.Add(this.chkReviLoading);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Location = new System.Drawing.Point(1434, 641);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(204, 257);
            this.panel2.TabIndex = 89;
            // 
            // chkReviLotEndAck
            // 
            this.chkReviLotEndAck.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkReviLotEndAck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkReviLotEndAck.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkReviLotEndAck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkReviLotEndAck.Location = new System.Drawing.Point(85, 185);
            this.chkReviLotEndAck.Name = "chkReviLotEndAck";
            this.chkReviLotEndAck.Size = new System.Drawing.Size(99, 27);
            this.chkReviLotEndAck.TabIndex = 36;
            this.chkReviLotEndAck.Text = "Lot End Ack";
            this.chkReviLotEndAck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkReviLotEndAck.UseVisualStyleBackColor = false;
            // 
            // chkReviLotStartAck
            // 
            this.chkReviLotStartAck.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkReviLotStartAck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkReviLotStartAck.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkReviLotStartAck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkReviLotStartAck.Location = new System.Drawing.Point(86, 158);
            this.chkReviLotStartAck.Name = "chkReviLotStartAck";
            this.chkReviLotStartAck.Size = new System.Drawing.Size(99, 27);
            this.chkReviLotStartAck.TabIndex = 37;
            this.chkReviLotStartAck.Text = "Lot Start Ack";
            this.chkReviLotStartAck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkReviLotStartAck.UseVisualStyleBackColor = false;
            // 
            // chkReviReviEnd
            // 
            this.chkReviReviEnd.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkReviReviEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkReviReviEnd.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkReviReviEnd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkReviReviEnd.Location = new System.Drawing.Point(106, 131);
            this.chkReviReviEnd.Name = "chkReviReviEnd";
            this.chkReviReviEnd.Size = new System.Drawing.Size(79, 27);
            this.chkReviReviEnd.TabIndex = 38;
            this.chkReviReviEnd.Text = "Review End";
            this.chkReviReviEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkReviReviEnd.UseVisualStyleBackColor = false;
            this.chkReviReviEnd.CheckedChanged += new System.EventHandler(this.chkReviReviEnd_CheckedChanged);
            // 
            // chkReviReviStartAck
            // 
            this.chkReviReviStartAck.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkReviReviStartAck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkReviReviStartAck.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkReviReviStartAck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkReviReviStartAck.Location = new System.Drawing.Point(86, 104);
            this.chkReviReviStartAck.Name = "chkReviReviStartAck";
            this.chkReviReviStartAck.Size = new System.Drawing.Size(99, 27);
            this.chkReviReviStartAck.TabIndex = 33;
            this.chkReviReviStartAck.Text = "Review Start Ack";
            this.chkReviReviStartAck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkReviReviStartAck.UseVisualStyleBackColor = false;
            // 
            // chkReviAlignStartAck
            // 
            this.chkReviAlignStartAck.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkReviAlignStartAck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkReviAlignStartAck.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkReviAlignStartAck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkReviAlignStartAck.Location = new System.Drawing.Point(86, 77);
            this.chkReviAlignStartAck.Name = "chkReviAlignStartAck";
            this.chkReviAlignStartAck.Size = new System.Drawing.Size(99, 27);
            this.chkReviAlignStartAck.TabIndex = 34;
            this.chkReviAlignStartAck.Text = "Align Start Ack";
            this.chkReviAlignStartAck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkReviAlignStartAck.UseVisualStyleBackColor = false;
            // 
            // chkReviUnloadingAck
            // 
            this.chkReviUnloadingAck.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkReviUnloadingAck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkReviUnloadingAck.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkReviUnloadingAck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkReviUnloadingAck.Location = new System.Drawing.Point(85, 212);
            this.chkReviUnloadingAck.Name = "chkReviUnloadingAck";
            this.chkReviUnloadingAck.Size = new System.Drawing.Size(99, 27);
            this.chkReviUnloadingAck.TabIndex = 35;
            this.chkReviUnloadingAck.Text = "Loading Ack";
            this.chkReviUnloadingAck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkReviUnloadingAck.UseVisualStyleBackColor = false;
            // 
            // chkReviLoadingAck
            // 
            this.chkReviLoadingAck.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkReviLoadingAck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkReviLoadingAck.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkReviLoadingAck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkReviLoadingAck.Location = new System.Drawing.Point(86, 50);
            this.chkReviLoadingAck.Name = "chkReviLoadingAck";
            this.chkReviLoadingAck.Size = new System.Drawing.Size(99, 27);
            this.chkReviLoadingAck.TabIndex = 35;
            this.chkReviLoadingAck.Text = "Loading Ack";
            this.chkReviLoadingAck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkReviLoadingAck.UseVisualStyleBackColor = false;
            // 
            // chkReviLotEnd
            // 
            this.chkReviLotEnd.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkReviLotEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkReviLotEnd.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkReviLotEnd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkReviLotEnd.Location = new System.Drawing.Point(6, 185);
            this.chkReviLotEnd.Name = "chkReviLotEnd";
            this.chkReviLotEnd.Size = new System.Drawing.Size(76, 27);
            this.chkReviLotEnd.TabIndex = 29;
            this.chkReviLotEnd.Text = "Lot End";
            this.chkReviLotEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkReviLotEnd.UseVisualStyleBackColor = false;
            // 
            // chkReviLotStart
            // 
            this.chkReviLotStart.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkReviLotStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkReviLotStart.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkReviLotStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkReviLotStart.Location = new System.Drawing.Point(7, 158);
            this.chkReviLotStart.Name = "chkReviLotStart";
            this.chkReviLotStart.Size = new System.Drawing.Size(76, 27);
            this.chkReviLotStart.TabIndex = 27;
            this.chkReviLotStart.Text = "Lot Start";
            this.chkReviLotStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkReviLotStart.UseVisualStyleBackColor = false;
            // 
            // chkReviReviEndAck
            // 
            this.chkReviReviEndAck.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkReviReviEndAck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkReviReviEndAck.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkReviReviEndAck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkReviReviEndAck.Location = new System.Drawing.Point(7, 131);
            this.chkReviReviEndAck.Name = "chkReviReviEndAck";
            this.chkReviReviEndAck.Size = new System.Drawing.Size(105, 27);
            this.chkReviReviEndAck.TabIndex = 28;
            this.chkReviReviEndAck.Text = "Review End Ack";
            this.chkReviReviEndAck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkReviReviEndAck.UseVisualStyleBackColor = false;
            // 
            // chkReviReviStart
            // 
            this.chkReviReviStart.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkReviReviStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkReviReviStart.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkReviReviStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkReviReviStart.Location = new System.Drawing.Point(7, 104);
            this.chkReviReviStart.Name = "chkReviReviStart";
            this.chkReviReviStart.Size = new System.Drawing.Size(76, 27);
            this.chkReviReviStart.TabIndex = 32;
            this.chkReviReviStart.Text = "Review Start";
            this.chkReviReviStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkReviReviStart.UseVisualStyleBackColor = false;
            // 
            // chkReviUnloading
            // 
            this.chkReviUnloading.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkReviUnloading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkReviUnloading.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkReviUnloading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkReviUnloading.Location = new System.Drawing.Point(6, 212);
            this.chkReviUnloading.Name = "chkReviUnloading";
            this.chkReviUnloading.Size = new System.Drawing.Size(76, 27);
            this.chkReviUnloading.TabIndex = 31;
            this.chkReviUnloading.Text = "Loading";
            this.chkReviUnloading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkReviUnloading.UseVisualStyleBackColor = false;
            // 
            // chkReviAlignStart
            // 
            this.chkReviAlignStart.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkReviAlignStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkReviAlignStart.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkReviAlignStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkReviAlignStart.Location = new System.Drawing.Point(7, 77);
            this.chkReviAlignStart.Name = "chkReviAlignStart";
            this.chkReviAlignStart.Size = new System.Drawing.Size(76, 27);
            this.chkReviAlignStart.TabIndex = 30;
            this.chkReviAlignStart.Text = "Align Start";
            this.chkReviAlignStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkReviAlignStart.UseVisualStyleBackColor = false;
            // 
            // chkReviLoading
            // 
            this.chkReviLoading.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkReviLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkReviLoading.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkReviLoading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkReviLoading.Location = new System.Drawing.Point(7, 50);
            this.chkReviLoading.Name = "chkReviLoading";
            this.chkReviLoading.Size = new System.Drawing.Size(76, 27);
            this.chkReviLoading.TabIndex = 31;
            this.chkReviLoading.Text = "Loading";
            this.chkReviLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkReviLoading.UseVisualStyleBackColor = false;
            // 
            // panel13
            // 
            this.panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel13.Controls.Add(this.label16);
            this.panel13.Controls.Add(this.btnCtrlPcXy);
            this.panel13.Controls.Add(this.btnSimulPcXy);
            this.panel13.Controls.Add(this.btnTester);
            this.panel13.Controls.Add(this.btnXyIoAddr);
            this.panel13.Controls.Add(this.btnMemDetail);
            this.panel13.Controls.Add(this.btnXyIo);
            this.panel13.Controls.Add(this.chkUseInterlock);
            this.panel13.Location = new System.Drawing.Point(1239, 45);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(312, 183);
            this.panel13.TabIndex = 89;
            // 
            // label16
            // 
            this.label16.AutoEllipsis = true;
            this.label16.BackColor = System.Drawing.Color.Gainsboro;
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(310, 24);
            this.label16.TabIndex = 9;
            this.label16.Text = "■ 버튼";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkUseInterlock
            // 
            this.chkUseInterlock.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkUseInterlock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkUseInterlock.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkUseInterlock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkUseInterlock.Location = new System.Drawing.Point(4, 142);
            this.chkUseInterlock.Name = "chkUseInterlock";
            this.chkUseInterlock.Size = new System.Drawing.Size(138, 40);
            this.chkUseInterlock.TabIndex = 12;
            this.chkUseInterlock.Text = "인터락 사용";
            this.chkUseInterlock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkUseInterlock.UseVisualStyleBackColor = false;
            this.chkUseInterlock.CheckedChanged += new System.EventHandler(this.chkUseInterlock_CheckedChanged);
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.Location = new System.Drawing.Point(731, 14);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(48, 19);
            this.label17.TabIndex = 4;
            this.label17.Text = "Pos\'";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            this.label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label22.Location = new System.Drawing.Point(780, 14);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(60, 19);
            this.label22.TabIndex = 2;
            this.label22.Text = "0.0";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel14
            // 
            this.panel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel14.Controls.Add(this.rdInputGlsRight);
            this.panel14.Controls.Add(this.rdInputGlsLeft);
            this.panel14.Controls.Add(this.rdInputGlsSep);
            this.panel14.Controls.Add(this.rdInputGlsOrg);
            this.panel14.Controls.Add(this.label23);
            this.panel14.Controls.Add(this.lblLoRecvComplete);
            this.panel14.Controls.Add(this.label33);
            this.panel14.Controls.Add(this.btnRecvAbleStart);
            this.panel14.Controls.Add(this.btnSendAbleStart);
            this.panel14.Controls.Add(this.label35);
            this.panel14.Controls.Add(this.label37);
            this.panel14.Controls.Add(this.lblLoRecvStart);
            this.panel14.Controls.Add(this.lblLoRecvAble);
            this.panel14.Controls.Add(this.lblAoiSendComplete);
            this.panel14.Controls.Add(this.lblAoiSendStart);
            this.panel14.Controls.Add(this.lblAoiSendAble);
            this.panel14.Controls.Add(this.lblAoiRecvComplete);
            this.panel14.Controls.Add(this.lblAoiRecvStart);
            this.panel14.Controls.Add(this.lblAoiRecvAble);
            this.panel14.Controls.Add(this.lblUpSendComplete);
            this.panel14.Controls.Add(this.lblUpSendStart);
            this.panel14.Controls.Add(this.lblUpSendAble);
            this.panel14.Controls.Add(this.label38);
            this.panel14.Location = new System.Drawing.Point(1557, 19);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(296, 214);
            this.panel14.TabIndex = 103;
            // 
            // label23
            // 
            this.label23.AutoEllipsis = true;
            this.label23.BackColor = System.Drawing.Color.Silver;
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label23.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.label23.ForeColor = System.Drawing.Color.Firebrick;
            this.label23.Location = new System.Drawing.Point(87, 117);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(80, 21);
            this.label23.TabIndex = 65;
            this.label23.Text = "AOI";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoRecvComplete
            // 
            this.lblLoRecvComplete.AutoEllipsis = true;
            this.lblLoRecvComplete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLoRecvComplete.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLoRecvComplete.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoRecvComplete.ForeColor = System.Drawing.Color.White;
            this.lblLoRecvComplete.Location = new System.Drawing.Point(4, 183);
            this.lblLoRecvComplete.Name = "lblLoRecvComplete";
            this.lblLoRecvComplete.Size = new System.Drawing.Size(80, 20);
            this.lblLoRecvComplete.TabIndex = 64;
            this.lblLoRecvComplete.Text = "R_COMPLETE";
            this.lblLoRecvComplete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLoRecvComplete.Click += new System.EventHandler(this.lblLoRecvComplete_Click);
            // 
            // label33
            // 
            this.label33.AutoEllipsis = true;
            this.label33.BackColor = System.Drawing.Color.Silver;
            this.label33.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label33.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.label33.ForeColor = System.Drawing.Color.Firebrick;
            this.label33.Location = new System.Drawing.Point(87, 26);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(80, 21);
            this.label33.TabIndex = 10;
            this.label33.Text = "AOI";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRecvAbleStart
            // 
            this.btnRecvAbleStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRecvAbleStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRecvAbleStart.Location = new System.Drawing.Point(173, 131);
            this.btnRecvAbleStart.Name = "btnRecvAbleStart";
            this.btnRecvAbleStart.Size = new System.Drawing.Size(118, 72);
            this.btnRecvAbleStart.TabIndex = 92;
            this.btnRecvAbleStart.Text = "배출";
            this.btnRecvAbleStart.UseVisualStyleBackColor = false;
            this.btnRecvAbleStart.Click += new System.EventHandler(this.btnRecvAbleStart_Click);
            // 
            // btnSendAbleStart
            // 
            this.btnSendAbleStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSendAbleStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnSendAbleStart.Location = new System.Drawing.Point(173, 58);
            this.btnSendAbleStart.Name = "btnSendAbleStart";
            this.btnSendAbleStart.Size = new System.Drawing.Size(118, 54);
            this.btnSendAbleStart.TabIndex = 92;
            this.btnSendAbleStart.Text = "투입";
            this.btnSendAbleStart.UseVisualStyleBackColor = false;
            this.btnSendAbleStart.Click += new System.EventHandler(this.btnSendAbleStart_Click);
            // 
            // label35
            // 
            this.label35.AutoEllipsis = true;
            this.label35.BackColor = System.Drawing.Color.Silver;
            this.label35.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label35.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.label35.ForeColor = System.Drawing.Color.Firebrick;
            this.label35.Location = new System.Drawing.Point(4, 26);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(80, 21);
            this.label35.TabIndex = 10;
            this.label35.Text = "UPPER";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label37
            // 
            this.label37.AutoEllipsis = true;
            this.label37.BackColor = System.Drawing.Color.Silver;
            this.label37.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label37.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.label37.ForeColor = System.Drawing.Color.Firebrick;
            this.label37.Location = new System.Drawing.Point(4, 117);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(80, 21);
            this.label37.TabIndex = 10;
            this.label37.Text = "LOWER";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoRecvStart
            // 
            this.lblLoRecvStart.AutoEllipsis = true;
            this.lblLoRecvStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLoRecvStart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLoRecvStart.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoRecvStart.ForeColor = System.Drawing.Color.White;
            this.lblLoRecvStart.Location = new System.Drawing.Point(4, 161);
            this.lblLoRecvStart.Name = "lblLoRecvStart";
            this.lblLoRecvStart.Size = new System.Drawing.Size(80, 20);
            this.lblLoRecvStart.TabIndex = 63;
            this.lblLoRecvStart.Text = "R_START";
            this.lblLoRecvStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLoRecvStart.Click += new System.EventHandler(this.lblLoRecvStart_Click);
            // 
            // lblLoRecvAble
            // 
            this.lblLoRecvAble.AutoEllipsis = true;
            this.lblLoRecvAble.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLoRecvAble.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLoRecvAble.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoRecvAble.ForeColor = System.Drawing.Color.White;
            this.lblLoRecvAble.Location = new System.Drawing.Point(4, 139);
            this.lblLoRecvAble.Name = "lblLoRecvAble";
            this.lblLoRecvAble.Size = new System.Drawing.Size(80, 20);
            this.lblLoRecvAble.TabIndex = 62;
            this.lblLoRecvAble.Text = "R_ABLE";
            this.lblLoRecvAble.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLoRecvAble.Click += new System.EventHandler(this.lblLoRecvAble_Click);
            // 
            // lblAoiSendComplete
            // 
            this.lblAoiSendComplete.AutoEllipsis = true;
            this.lblAoiSendComplete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAoiSendComplete.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAoiSendComplete.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAoiSendComplete.ForeColor = System.Drawing.Color.White;
            this.lblAoiSendComplete.Location = new System.Drawing.Point(87, 183);
            this.lblAoiSendComplete.Name = "lblAoiSendComplete";
            this.lblAoiSendComplete.Size = new System.Drawing.Size(80, 20);
            this.lblAoiSendComplete.TabIndex = 61;
            this.lblAoiSendComplete.Text = "S_COMPLETE";
            this.lblAoiSendComplete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAoiSendComplete.Click += new System.EventHandler(this.lblAoiSendComplete_Click);
            // 
            // lblAoiSendStart
            // 
            this.lblAoiSendStart.AutoEllipsis = true;
            this.lblAoiSendStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAoiSendStart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAoiSendStart.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAoiSendStart.ForeColor = System.Drawing.Color.White;
            this.lblAoiSendStart.Location = new System.Drawing.Point(87, 161);
            this.lblAoiSendStart.Name = "lblAoiSendStart";
            this.lblAoiSendStart.Size = new System.Drawing.Size(80, 20);
            this.lblAoiSendStart.TabIndex = 60;
            this.lblAoiSendStart.Text = "S_START";
            this.lblAoiSendStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAoiSendStart.Click += new System.EventHandler(this.lblAoiSendStart_Click);
            // 
            // lblAoiSendAble
            // 
            this.lblAoiSendAble.AutoEllipsis = true;
            this.lblAoiSendAble.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAoiSendAble.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAoiSendAble.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAoiSendAble.ForeColor = System.Drawing.Color.White;
            this.lblAoiSendAble.Location = new System.Drawing.Point(87, 139);
            this.lblAoiSendAble.Name = "lblAoiSendAble";
            this.lblAoiSendAble.Size = new System.Drawing.Size(80, 20);
            this.lblAoiSendAble.TabIndex = 59;
            this.lblAoiSendAble.Text = "S_ABLE";
            this.lblAoiSendAble.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAoiSendAble.Click += new System.EventHandler(this.lblAoiSendAble_Click);
            // 
            // lblAoiRecvComplete
            // 
            this.lblAoiRecvComplete.AutoEllipsis = true;
            this.lblAoiRecvComplete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAoiRecvComplete.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAoiRecvComplete.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAoiRecvComplete.ForeColor = System.Drawing.Color.White;
            this.lblAoiRecvComplete.Location = new System.Drawing.Point(87, 92);
            this.lblAoiRecvComplete.Name = "lblAoiRecvComplete";
            this.lblAoiRecvComplete.Size = new System.Drawing.Size(80, 20);
            this.lblAoiRecvComplete.TabIndex = 58;
            this.lblAoiRecvComplete.Text = "R_COMPLETE";
            this.lblAoiRecvComplete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAoiRecvComplete.Click += new System.EventHandler(this.lblAoiRecvComplete_Click);
            // 
            // lblAoiRecvStart
            // 
            this.lblAoiRecvStart.AutoEllipsis = true;
            this.lblAoiRecvStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAoiRecvStart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAoiRecvStart.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAoiRecvStart.ForeColor = System.Drawing.Color.White;
            this.lblAoiRecvStart.Location = new System.Drawing.Point(87, 70);
            this.lblAoiRecvStart.Name = "lblAoiRecvStart";
            this.lblAoiRecvStart.Size = new System.Drawing.Size(80, 20);
            this.lblAoiRecvStart.TabIndex = 57;
            this.lblAoiRecvStart.Text = "R_START";
            this.lblAoiRecvStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAoiRecvStart.Click += new System.EventHandler(this.lblAoiRecvStart_Click);
            // 
            // lblAoiRecvAble
            // 
            this.lblAoiRecvAble.AutoEllipsis = true;
            this.lblAoiRecvAble.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAoiRecvAble.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAoiRecvAble.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAoiRecvAble.ForeColor = System.Drawing.Color.White;
            this.lblAoiRecvAble.Location = new System.Drawing.Point(87, 48);
            this.lblAoiRecvAble.Name = "lblAoiRecvAble";
            this.lblAoiRecvAble.Size = new System.Drawing.Size(80, 20);
            this.lblAoiRecvAble.TabIndex = 56;
            this.lblAoiRecvAble.Text = "R_ABLE";
            this.lblAoiRecvAble.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAoiRecvAble.Click += new System.EventHandler(this.lblAoiRecvAble_Click);
            // 
            // lblUpSendComplete
            // 
            this.lblUpSendComplete.AutoEllipsis = true;
            this.lblUpSendComplete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUpSendComplete.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUpSendComplete.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpSendComplete.ForeColor = System.Drawing.Color.White;
            this.lblUpSendComplete.Location = new System.Drawing.Point(4, 92);
            this.lblUpSendComplete.Name = "lblUpSendComplete";
            this.lblUpSendComplete.Size = new System.Drawing.Size(80, 20);
            this.lblUpSendComplete.TabIndex = 55;
            this.lblUpSendComplete.Text = "S_COMPLETE";
            this.lblUpSendComplete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUpSendComplete.Click += new System.EventHandler(this.lblUpSendComplete_Click);
            // 
            // lblUpSendStart
            // 
            this.lblUpSendStart.AutoEllipsis = true;
            this.lblUpSendStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUpSendStart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUpSendStart.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpSendStart.ForeColor = System.Drawing.Color.White;
            this.lblUpSendStart.Location = new System.Drawing.Point(4, 70);
            this.lblUpSendStart.Name = "lblUpSendStart";
            this.lblUpSendStart.Size = new System.Drawing.Size(80, 20);
            this.lblUpSendStart.TabIndex = 54;
            this.lblUpSendStart.Text = "S_START";
            this.lblUpSendStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUpSendStart.Click += new System.EventHandler(this.lblUpSendStart_Click);
            // 
            // lblUpSendAble
            // 
            this.lblUpSendAble.AutoEllipsis = true;
            this.lblUpSendAble.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUpSendAble.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblUpSendAble.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpSendAble.ForeColor = System.Drawing.Color.White;
            this.lblUpSendAble.Location = new System.Drawing.Point(4, 48);
            this.lblUpSendAble.Name = "lblUpSendAble";
            this.lblUpSendAble.Size = new System.Drawing.Size(80, 20);
            this.lblUpSendAble.TabIndex = 53;
            this.lblUpSendAble.Text = "S_ABLE";
            this.lblUpSendAble.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUpSendAble.Click += new System.EventHandler(this.lblUpSendAble_Click);
            // 
            // label38
            // 
            this.label38.AutoEllipsis = true;
            this.label38.BackColor = System.Drawing.Color.Gainsboro;
            this.label38.Dock = System.Windows.Forms.DockStyle.Top;
            this.label38.Font = new System.Drawing.Font("휴먼모음T", 8.25F);
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(0, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(294, 24);
            this.label38.TabIndex = 9;
            this.label38.Text = "■ PIO";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkInspXError
            // 
            this.chkInspXError.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkInspXError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkInspXError.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkInspXError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.chkInspXError.Location = new System.Drawing.Point(1703, 777);
            this.chkInspXError.Name = "chkInspXError";
            this.chkInspXError.Size = new System.Drawing.Size(169, 28);
            this.chkInspXError.TabIndex = 10;
            this.chkInspXError.Text = "InspX Error";
            this.chkInspXError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkInspXError.UseVisualStyleBackColor = false;
            this.chkInspXError.CheckedChanged += new System.EventHandler(this.chkInspXError_CheckedChanged);
            // 
            // ucrlServoZ3
            // 
            this.ucrlServoZ3.Location = new System.Drawing.Point(796, 890);
            this.ucrlServoZ3.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.ucrlServoZ3.Name = "ucrlServoZ3";
            this.ucrlServoZ3.Size = new System.Drawing.Size(436, 123);
            this.ucrlServoZ3.TabIndex = 106;
            // 
            // ucrlServoRY_Under
            // 
            this.ucrlServoRY_Under.Location = new System.Drawing.Point(796, 416);
            this.ucrlServoRY_Under.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.ucrlServoRY_Under.Name = "ucrlServoRY_Under";
            this.ucrlServoRY_Under.Size = new System.Drawing.Size(436, 115);
            this.ucrlServoRY_Under.TabIndex = 105;
            // 
            // ucrlServoZ2
            // 
            this.ucrlServoZ2.Location = new System.Drawing.Point(796, 772);
            this.ucrlServoZ2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.ucrlServoZ2.Name = "ucrlServoZ2";
            this.ucrlServoZ2.Size = new System.Drawing.Size(436, 123);
            this.ucrlServoZ2.TabIndex = 104;
            // 
            // ucrlServoPin
            // 
            this.ucrlServoPin.Location = new System.Drawing.Point(796, 534);
            this.ucrlServoPin.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.ucrlServoPin.Name = "ucrlServoPin";
            this.ucrlServoPin.Size = new System.Drawing.Size(436, 118);
            this.ucrlServoPin.TabIndex = 104;
            // 
            // ucrlServoZ1
            // 
            this.ucrlServoZ1.Location = new System.Drawing.Point(796, 653);
            this.ucrlServoZ1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.ucrlServoZ1.Name = "ucrlServoZ1";
            this.ucrlServoZ1.Size = new System.Drawing.Size(436, 115);
            this.ucrlServoZ1.TabIndex = 104;
            // 
            // ucrlServoRY
            // 
            this.ucrlServoRY.Location = new System.Drawing.Point(796, 298);
            this.ucrlServoRY.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.ucrlServoRY.Name = "ucrlServoRY";
            this.ucrlServoRY.Size = new System.Drawing.Size(436, 115);
            this.ucrlServoRY.TabIndex = 104;
            // 
            // ucrlServoIY
            // 
            this.ucrlServoIY.Location = new System.Drawing.Point(796, 181);
            this.ucrlServoIY.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ucrlServoIY.Name = "ucrlServoIY";
            this.ucrlServoIY.Size = new System.Drawing.Size(436, 113);
            this.ucrlServoIY.TabIndex = 104;
            // 
            // ucrlServoIX
            // 
            this.ucrlServoIX.Location = new System.Drawing.Point(796, 46);
            this.ucrlServoIX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucrlServoIX.Name = "ucrlServoIX";
            this.ucrlServoIX.Size = new System.Drawing.Size(436, 133);
            this.ucrlServoIX.TabIndex = 104;
            this.ucrlServoIX.Load += new System.EventHandler(this.ucrlServoIX_Load);
            // 
            // rdInputGlsRight
            // 
            this.rdInputGlsRight.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdInputGlsRight.AutoSize = true;
            this.rdInputGlsRight.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rdInputGlsRight.Location = new System.Drawing.Point(270, 27);
            this.rdInputGlsRight.Name = "rdInputGlsRight";
            this.rdInputGlsRight.Size = new System.Drawing.Size(24, 25);
            this.rdInputGlsRight.TabIndex = 96;
            this.rdInputGlsRight.Text = "R";
            this.rdInputGlsRight.UseVisualStyleBackColor = true;
            // 
            // rdInputGlsLeft
            // 
            this.rdInputGlsLeft.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdInputGlsLeft.AutoSize = true;
            this.rdInputGlsLeft.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rdInputGlsLeft.Location = new System.Drawing.Point(247, 27);
            this.rdInputGlsLeft.Name = "rdInputGlsLeft";
            this.rdInputGlsLeft.Size = new System.Drawing.Size(23, 25);
            this.rdInputGlsLeft.TabIndex = 97;
            this.rdInputGlsLeft.Text = "L";
            this.rdInputGlsLeft.UseVisualStyleBackColor = true;
            // 
            // rdInputGlsSep
            // 
            this.rdInputGlsSep.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdInputGlsSep.AutoSize = true;
            this.rdInputGlsSep.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rdInputGlsSep.Location = new System.Drawing.Point(210, 27);
            this.rdInputGlsSep.Name = "rdInputGlsSep";
            this.rdInputGlsSep.Size = new System.Drawing.Size(37, 25);
            this.rdInputGlsSep.TabIndex = 94;
            this.rdInputGlsSep.Text = "Sep";
            this.rdInputGlsSep.UseVisualStyleBackColor = true;
            // 
            // rdInputGlsOrg
            // 
            this.rdInputGlsOrg.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdInputGlsOrg.AutoSize = true;
            this.rdInputGlsOrg.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rdInputGlsOrg.Checked = true;
            this.rdInputGlsOrg.Location = new System.Drawing.Point(173, 27);
            this.rdInputGlsOrg.Name = "rdInputGlsOrg";
            this.rdInputGlsOrg.Size = new System.Drawing.Size(37, 25);
            this.rdInputGlsOrg.TabIndex = 95;
            this.rdInputGlsOrg.TabStop = true;
            this.rdInputGlsOrg.Text = "Org";
            this.rdInputGlsOrg.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1904, 1022);
            this.Controls.Add(this.ucrlServoZ3);
            this.Controls.Add(this.ucrlServoRY_Under);
            this.Controls.Add(this.ucrlServoZ2);
            this.Controls.Add(this.ucrlServoPin);
            this.Controls.Add(this.ucrlServoZ1);
            this.Controls.Add(this.ucrlServoRY);
            this.Controls.Add(this.chkInspXError);
            this.Controls.Add(this.ucrlServoIY);
            this.Controls.Add(this.ucrlServoIX);
            this.Controls.Add(this.panel14);
            this.Controls.Add(this.lstLogger);
            this.Controls.Add(this.pnlView);
            this.Controls.Add(this.tbStatus);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel13);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.lblScanTime);
            this.Controls.Add(this.label11);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Text = "EQUIP SIMULATOR";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGlassEdge)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVacuum)).EndInit();
            this.panel10.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGlassDetect)).EndInit();
            this.panel12.ResumeLayout(false);
            this.pnlView.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Timer tmrWorker;
        private System.Windows.Forms.CheckBox chkEmo01;
        private System.Windows.Forms.CheckBox chkEmo02;
        private System.Windows.Forms.CheckBox chkEmo03;
        private System.Windows.Forms.CheckBox chkEmo04;
        private System.Windows.Forms.CheckBox chkIsol01;
        private System.Windows.Forms.CheckBox chkIsol02;
        private System.Windows.Forms.CheckBox chkIsol04;
        private System.Windows.Forms.CheckBox chkIsol03;
        private System.Windows.Forms.CheckBox chkStageGlassSensor1;
        private System.Windows.Forms.CheckBox chkSafetyModeKeySW;
        private System.Windows.Forms.CheckBox chkRobotArmCheck;
        private System.Windows.Forms.CheckBox chkCoolineMainAir;
        private System.Windows.Forms.CheckBox chkIonizerMainAir;
        private System.Windows.Forms.CheckBox chkIonizer1On;
        private System.Windows.Forms.CheckBox chkIonizer2On;
        private System.Windows.Forms.CheckBox chkIonizer4On;
        private System.Windows.Forms.CheckBox chkIonizer3On;
        private System.Windows.Forms.CheckBox chkCameraCooling;
        private System.Windows.Forms.CheckBox chkIonizerCover;
        private System.Windows.Forms.CheckBox chkBlower1;
        private System.Windows.Forms.CheckBox chkBlower2;
        private System.Windows.Forms.CheckBox chkBlower3;
        private System.Windows.Forms.CheckBox chkBlower4;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.CheckBox chkIonizer;
        private System.Windows.Forms.Panel panel6;
        internal System.Windows.Forms.Label label25;
        internal System.Windows.Forms.Label label24;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMemDetail;
        private System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel5;
        internal System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel7;
        internal System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel8;
        internal System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel9;
        internal System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel10;
        internal System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel11;
        internal System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel12;
        internal System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel pnlView;
        internal System.Windows.Forms.Label label14;
        internal System.Windows.Forms.Label lblScanTime;
        private System.Windows.Forms.Button btnXyIo;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.Timer tmrState;
        private Ctrl.TransparentControl shStage;
        private Ctrl.TransparentControl spLiftPin;
        private Ctrl.TransparentControl shInspCam;
        private System.Windows.Forms.Button btnCtrlPcXy;
        private System.Windows.Forms.Button btnSimulPcXy;
        private Ctrl.TransparentControl SpThelta;
        private Ctrl.TransparentControl shReviewCam;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button btnXyIoAddr;
        private System.Windows.Forms.ListView lstLogger;
        private System.Windows.Forms.Button btnTester;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.CheckBox chkDoor02;
        private System.Windows.Forms.CheckBox chkDoor05;
        private System.Windows.Forms.CheckBox chkDoor04;
        private System.Windows.Forms.CheckBox chkDoor01;
        private System.Windows.Forms.CheckBox chkDoor03;
        private System.Windows.Forms.CheckBox chkDoor06;
        private System.Windows.Forms.CheckBox chkDoor10;
        private System.Windows.Forms.CheckBox chkDoor07;
        private System.Windows.Forms.CheckBox chkDoor09;
        private System.Windows.Forms.CheckBox chkDoor08;
        private System.Windows.Forms.Panel panel13;
        internal System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox chkUseInterlock;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel14;
        internal System.Windows.Forms.Label label23;
        internal System.Windows.Forms.Label lblLoRecvComplete;
        internal System.Windows.Forms.Label label33;
        internal System.Windows.Forms.Label label35;
        internal System.Windows.Forms.Label label37;
        internal System.Windows.Forms.Label lblLoRecvStart;
        internal System.Windows.Forms.Label lblLoRecvAble;
        internal System.Windows.Forms.Label lblAoiSendComplete;
        internal System.Windows.Forms.Label lblAoiSendStart;
        internal System.Windows.Forms.Label lblAoiSendAble;
        internal System.Windows.Forms.Label lblAoiRecvComplete;
        internal System.Windows.Forms.Label lblAoiRecvStart;
        internal System.Windows.Forms.Label lblAoiRecvAble;
        internal System.Windows.Forms.Label lblUpSendComplete;
        internal System.Windows.Forms.Label lblUpSendStart;
        internal System.Windows.Forms.Label lblUpSendAble;
        internal System.Windows.Forms.Label label38;
        private System.Windows.Forms.Button btnSendAbleStart;
        private System.Windows.Forms.Button btnRecvAbleStart;
        private Ctrl.UcrlServo ucrlServoIX;
        private Ctrl.UcrlServo ucrlServoIY;
        private Ctrl.UcrlServo ucrlServoRY;
        private Ctrl.UcrlServo ucrlServoZ1;
        private Ctrl.UcrlServo ucrlServoZ2;
        private Ctrl.UcrlServo ucrlServoPin;
        private System.Windows.Forms.CheckBox chkReviLotEndAck;
        private System.Windows.Forms.CheckBox chkReviLotStartAck;
        private System.Windows.Forms.CheckBox chkReviReviEnd;
        private System.Windows.Forms.CheckBox chkReviReviStartAck;
        private System.Windows.Forms.CheckBox chkReviAlignStartAck;
        private System.Windows.Forms.CheckBox chkReviLoadingAck;
        private System.Windows.Forms.CheckBox chkReviLotEnd;
        private System.Windows.Forms.CheckBox chkReviLotStart;
        private System.Windows.Forms.CheckBox chkReviReviEndAck;
        private System.Windows.Forms.CheckBox chkReviReviStart;
        private System.Windows.Forms.CheckBox chkReviAlignStart;
        private System.Windows.Forms.CheckBox chkReviLoading;
        internal System.Windows.Forms.Label lblInspGUnloadingOk;
        internal System.Windows.Forms.Label lblInspGUnloading;
        internal System.Windows.Forms.Label lblInspScanEnd;
        internal System.Windows.Forms.Label lblInspScanEndOk;
        internal System.Windows.Forms.Label lblInspScanStartOk;
        internal System.Windows.Forms.Label lblInspGLoadingOk;
        internal System.Windows.Forms.Label lblInspScanStart;
        internal System.Windows.Forms.Label lblInspScanReadyOk;
        internal System.Windows.Forms.Label lblInspScanReady;
        internal System.Windows.Forms.Label lblInspGLoading;
        private System.Windows.Forms.Label lblScanStep;
        private System.Windows.Forms.CheckBox chkReviUnloadingAck;
        private System.Windows.Forms.CheckBox chkReviUnloading;
        private System.Windows.Forms.CheckBox chkDoor11;
        private System.Windows.Forms.CheckBox chkEmo05;
        private System.Windows.Forms.CheckBox chkEnableGripSwOn;
        private System.Windows.Forms.CheckBox chkInspXError;
        private Ctrl.UcrlServo ucrlServoRY_Under;
        private System.Windows.Forms.CheckBox chkDoor15;
        private System.Windows.Forms.CheckBox chkDoor14;
        private System.Windows.Forms.CheckBox chkDoor13;
        private System.Windows.Forms.CheckBox chkDoor12;
        private System.Windows.Forms.CheckBox chkBlower6;
        private System.Windows.Forms.CheckBox chkBlower5;
        private System.Windows.Forms.CheckBox chkEmo06;
        private Ctrl.UcrlServo ucrlServoZ3;
        private System.Windows.Forms.Button btnAllDoor;
        private System.Windows.Forms.CheckBox chkIonizer1Alarm;
        private System.Windows.Forms.CheckBox chkIonizer2Alarm;
        private System.Windows.Forms.CheckBox chkIonizer4Alarm;
        private System.Windows.Forms.CheckBox chkIonizer3Alarm;
        private System.Windows.Forms.DataGridView dgvVacuum;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.DataGridView dgvGlassEdge;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.Button btnGlassCrackSepOn;
        private System.Windows.Forms.Button btnGlassCrackOrgOn;
        private System.Windows.Forms.Button btnCrackOff;
        private System.Windows.Forms.DataGridView dgvGlassDetect;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn2;
        private System.Windows.Forms.Button btnGlassCrackRightOn;
        private System.Windows.Forms.Button btnGlassCrackLeftOn;
        private System.Windows.Forms.RadioButton rdInputGlsRight;
        private System.Windows.Forms.RadioButton rdInputGlsLeft;
        private System.Windows.Forms.RadioButton rdInputGlsSep;
        private System.Windows.Forms.RadioButton rdInputGlsOrg;
        
    }
}

