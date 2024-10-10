using Dit.Framework.UI.UserComponent;

namespace EquipMainUi
{
    partial class FrmEfemTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbIP = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbTestMsg = new System.Windows.Forms.TextBox();
            this.gbCommand = new System.Windows.Forms.GroupBox();
            this.cbBasicCmd = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChmdm = new System.Windows.Forms.Button();
            this.btnChmda = new System.Windows.Forms.Button();
            this.btnMapping = new System.Windows.Forms.Button();
            this.btnUnload = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnUnDock = new System.Windows.Forms.Button();
            this.btnDock = new System.Windows.Forms.Button();
            this.btnUnClamp = new System.Windows.Forms.Button();
            this.btnClamp = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnResume = new System.Windows.Forms.Button();
            this.btnPardy = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStat = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnInit = new System.Windows.Forms.Button();
            this.btnSiglm = new System.Windows.Forms.Button();
            this.btnLpled = new System.Windows.Forms.Button();
            this.btnAlign = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDegree = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbSlot = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbTransfer = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbLower = new System.Windows.Forms.RadioButton();
            this.rbUpper = new System.Windows.Forms.RadioButton();
            this.btnPlace = new System.Windows.Forms.Button();
            this.btnPick = new System.Windows.Forms.Button();
            this.btnWaiting = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkOCRForward = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbbLampByte2 = new System.Windows.Forms.ComboBox();
            this.cbbLampByte1 = new System.Windows.Forms.ComboBox();
            this.cbbLampPort = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cbbBuzzer2 = new System.Windows.Forms.ComboBox();
            this.cbbBuzzer1 = new System.Windows.Forms.ComboBox();
            this.cbbBlueLamp = new System.Windows.Forms.ComboBox();
            this.cbbGreenLamp = new System.Windows.Forms.ComboBox();
            this.cbbYelloLamp = new System.Windows.Forms.ComboBox();
            this.cbbRedLamp = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rdTcp = new System.Windows.Forms.RadioButton();
            this.rdProxy = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.lblProxyStep = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tmrUiUpdate = new System.Windows.Forms.Timer(this.components);
            this.btnSaveIP = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lvRobotLog = new Dit.Framework.UI.UserComponent.ListViewEx();
            this.chTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chContent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.lvLoadPort1Log = new Dit.Framework.UI.UserComponent.ListViewEx();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.lvLoadPort2Log = new Dit.Framework.UI.UserComponent.ListViewEx();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.lvSendLog = new Dit.Framework.UI.UserComponent.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.lvAlignerLog = new Dit.Framework.UI.UserComponent.ListViewEx();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.lvEtcLog = new Dit.Framework.UI.UserComponent.ListViewEx();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.lvRecvLog = new Dit.Framework.UI.UserComponent.ListViewEx();
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.lblSeqRun = new System.Windows.Forms.Label();
            this.lblHomeRun = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblAlignerStep = new System.Windows.Forms.Label();
            this.lblLpm2Step = new System.Windows.Forms.Label();
            this.lblLpm1Step = new System.Windows.Forms.Label();
            this.lblRobotStep = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnEFEMHome = new System.Windows.Forms.Button();
            this.btnEFEMStop = new System.Windows.Forms.Button();
            this.btnEFEMStart = new System.Windows.Forms.Button();
            this.btnEFEMPause = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.txtRecipeName = new System.Windows.Forms.TextBox();
            this.gbCommand.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(10, 33);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(165, 21);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tbIP
            // 
            this.tbIP.Location = new System.Drawing.Point(10, 6);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(66, 21);
            this.tbIP.TabIndex = 1;
            this.tbIP.Text = "10.1.21.89";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(79, 6);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(39, 21);
            this.tbPort.TabIndex = 1;
            this.tbPort.Text = "11000";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(10, 87);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(165, 21);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbTestMsg
            // 
            this.tbTestMsg.Location = new System.Drawing.Point(10, 60);
            this.tbTestMsg.Name = "tbTestMsg";
            this.tbTestMsg.Size = new System.Drawing.Size(165, 21);
            this.tbTestMsg.TabIndex = 3;
            // 
            // gbCommand
            // 
            this.gbCommand.Controls.Add(this.cbBasicCmd);
            this.gbCommand.Controls.Add(this.label1);
            this.gbCommand.Controls.Add(this.btnChmdm);
            this.gbCommand.Controls.Add(this.btnChmda);
            this.gbCommand.Controls.Add(this.btnMapping);
            this.gbCommand.Controls.Add(this.btnUnload);
            this.gbCommand.Controls.Add(this.btnLoad);
            this.gbCommand.Controls.Add(this.btnClose);
            this.gbCommand.Controls.Add(this.btnOpen);
            this.gbCommand.Controls.Add(this.btnUnDock);
            this.gbCommand.Controls.Add(this.btnDock);
            this.gbCommand.Controls.Add(this.btnUnClamp);
            this.gbCommand.Controls.Add(this.btnClamp);
            this.gbCommand.Controls.Add(this.btnStop);
            this.gbCommand.Controls.Add(this.btnResume);
            this.gbCommand.Controls.Add(this.btnPardy);
            this.gbCommand.Controls.Add(this.btnPause);
            this.gbCommand.Controls.Add(this.btnStat);
            this.gbCommand.Controls.Add(this.btnReset);
            this.gbCommand.Controls.Add(this.btnHome);
            this.gbCommand.Controls.Add(this.btnInit);
            this.gbCommand.Location = new System.Drawing.Point(181, 6);
            this.gbCommand.Name = "gbCommand";
            this.gbCommand.Size = new System.Drawing.Size(412, 166);
            this.gbCommand.TabIndex = 4;
            this.gbCommand.TabStop = false;
            this.gbCommand.Text = "Basic Command";
            // 
            // cbBasicCmd
            // 
            this.cbBasicCmd.FormattingEnabled = true;
            this.cbBasicCmd.Items.AddRange(new object[] {
            "Robot",
            "LoadPort1",
            "LoadPort2",
            "Aligner",
            "Etc"});
            this.cbBasicCmd.Location = new System.Drawing.Point(51, 20);
            this.cbBasicCmd.Name = "cbBasicCmd";
            this.cbBasicCmd.Size = new System.Drawing.Size(121, 20);
            this.cbBasicCmd.TabIndex = 3;
            this.cbBasicCmd.SelectedIndexChanged += new System.EventHandler(this.cbBasicCmd_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Port";
            // 
            // btnChmdm
            // 
            this.btnChmdm.Location = new System.Drawing.Point(327, 132);
            this.btnChmdm.Name = "btnChmdm";
            this.btnChmdm.Size = new System.Drawing.Size(75, 23);
            this.btnChmdm.TabIndex = 0;
            this.btnChmdm.Text = "CHMDM";
            this.btnChmdm.UseVisualStyleBackColor = true;
            this.btnChmdm.Click += new System.EventHandler(this.btnChmdm_Click);
            // 
            // btnChmda
            // 
            this.btnChmda.Location = new System.Drawing.Point(246, 132);
            this.btnChmda.Name = "btnChmda";
            this.btnChmda.Size = new System.Drawing.Size(75, 23);
            this.btnChmda.TabIndex = 0;
            this.btnChmda.Text = "CHMDA";
            this.btnChmda.UseVisualStyleBackColor = true;
            this.btnChmda.Click += new System.EventHandler(this.btnChmada_Click);
            // 
            // btnMapping
            // 
            this.btnMapping.Location = new System.Drawing.Point(165, 132);
            this.btnMapping.Name = "btnMapping";
            this.btnMapping.Size = new System.Drawing.Size(75, 23);
            this.btnMapping.TabIndex = 0;
            this.btnMapping.Text = "MAPP";
            this.btnMapping.UseVisualStyleBackColor = true;
            this.btnMapping.Click += new System.EventHandler(this.btnMapping_Click);
            // 
            // btnUnload
            // 
            this.btnUnload.Enabled = false;
            this.btnUnload.Location = new System.Drawing.Point(84, 132);
            this.btnUnload.Name = "btnUnload";
            this.btnUnload.Size = new System.Drawing.Size(75, 23);
            this.btnUnload.TabIndex = 0;
            this.btnUnload.Text = "ULOAD";
            this.btnUnload.UseVisualStyleBackColor = true;
            this.btnUnload.Click += new System.EventHandler(this.btnUnload_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Enabled = false;
            this.btnLoad.Location = new System.Drawing.Point(3, 132);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "LOAD";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(246, 103);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "CLOSE";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(165, 103);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "OPEN";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnUnDock
            // 
            this.btnUnDock.Enabled = false;
            this.btnUnDock.Location = new System.Drawing.Point(84, 103);
            this.btnUnDock.Name = "btnUnDock";
            this.btnUnDock.Size = new System.Drawing.Size(75, 23);
            this.btnUnDock.TabIndex = 0;
            this.btnUnDock.Text = "UDOCK";
            this.btnUnDock.UseVisualStyleBackColor = true;
            this.btnUnDock.Click += new System.EventHandler(this.btnUnDock_Click);
            // 
            // btnDock
            // 
            this.btnDock.Enabled = false;
            this.btnDock.Location = new System.Drawing.Point(3, 103);
            this.btnDock.Name = "btnDock";
            this.btnDock.Size = new System.Drawing.Size(75, 23);
            this.btnDock.TabIndex = 0;
            this.btnDock.Text = "DOCK";
            this.btnDock.UseVisualStyleBackColor = true;
            this.btnDock.Click += new System.EventHandler(this.btnDock_Click);
            // 
            // btnUnClamp
            // 
            this.btnUnClamp.Enabled = false;
            this.btnUnClamp.Location = new System.Drawing.Point(246, 74);
            this.btnUnClamp.Name = "btnUnClamp";
            this.btnUnClamp.Size = new System.Drawing.Size(75, 23);
            this.btnUnClamp.TabIndex = 0;
            this.btnUnClamp.Text = "UCLAM";
            this.btnUnClamp.UseVisualStyleBackColor = true;
            this.btnUnClamp.Click += new System.EventHandler(this.btnUnClamp_Click);
            // 
            // btnClamp
            // 
            this.btnClamp.Enabled = false;
            this.btnClamp.Location = new System.Drawing.Point(165, 74);
            this.btnClamp.Name = "btnClamp";
            this.btnClamp.Size = new System.Drawing.Size(75, 23);
            this.btnClamp.TabIndex = 0;
            this.btnClamp.Text = "CLAMP";
            this.btnClamp.UseVisualStyleBackColor = true;
            this.btnClamp.Click += new System.EventHandler(this.btnClamp_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(84, 74);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnResume
            // 
            this.btnResume.Location = new System.Drawing.Point(3, 74);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(75, 23);
            this.btnResume.TabIndex = 0;
            this.btnResume.Text = "RESUM";
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // btnPardy
            // 
            this.btnPardy.Location = new System.Drawing.Point(327, 74);
            this.btnPardy.Name = "btnPardy";
            this.btnPardy.Size = new System.Drawing.Size(75, 23);
            this.btnPardy.TabIndex = 0;
            this.btnPardy.Text = "PARDY";
            this.btnPardy.UseVisualStyleBackColor = true;
            this.btnPardy.Click += new System.EventHandler(this.btnPardy_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(327, 45);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 0;
            this.btnPause.Text = "PAUSE";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStat
            // 
            this.btnStat.Location = new System.Drawing.Point(246, 45);
            this.btnStat.Name = "btnStat";
            this.btnStat.Size = new System.Drawing.Size(75, 23);
            this.btnStat.TabIndex = 0;
            this.btnStat.Text = "STAT";
            this.btnStat.UseVisualStyleBackColor = true;
            this.btnStat.Click += new System.EventHandler(this.btnStat_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(165, 45);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "RESET";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(84, 45);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(75, 23);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "HOME";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Visible = false;
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(3, 45);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(75, 23);
            this.btnInit.TabIndex = 0;
            this.btnInit.Text = "INIT";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // btnSiglm
            // 
            this.btnSiglm.Location = new System.Drawing.Point(288, 58);
            this.btnSiglm.Name = "btnSiglm";
            this.btnSiglm.Size = new System.Drawing.Size(75, 23);
            this.btnSiglm.TabIndex = 0;
            this.btnSiglm.Text = "SIGLM";
            this.btnSiglm.UseVisualStyleBackColor = true;
            this.btnSiglm.Click += new System.EventHandler(this.btnSiglm_Click);
            // 
            // btnLpled
            // 
            this.btnLpled.Location = new System.Drawing.Point(212, 46);
            this.btnLpled.Name = "btnLpled";
            this.btnLpled.Size = new System.Drawing.Size(123, 23);
            this.btnLpled.TabIndex = 0;
            this.btnLpled.Text = "LPLED";
            this.btnLpled.UseVisualStyleBackColor = true;
            this.btnLpled.Click += new System.EventHandler(this.btnLpled_Click);
            // 
            // btnAlign
            // 
            this.btnAlign.Location = new System.Drawing.Point(8, 104);
            this.btnAlign.Name = "btnAlign";
            this.btnAlign.Size = new System.Drawing.Size(75, 23);
            this.btnAlign.TabIndex = 0;
            this.btnAlign.Text = "ALIGN";
            this.btnAlign.UseVisualStyleBackColor = true;
            this.btnAlign.Click += new System.EventHandler(this.btnAlign_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Degree";
            // 
            // tbDegree
            // 
            this.tbDegree.Location = new System.Drawing.Point(54, 52);
            this.tbDegree.Name = "tbDegree";
            this.tbDegree.Size = new System.Drawing.Size(44, 21);
            this.tbDegree.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbSlot);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbbTransfer);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnPlace);
            this.groupBox1.Controls.Add(this.btnPick);
            this.groupBox1.Controls.Add(this.btnWaiting);
            this.groupBox1.Location = new System.Drawing.Point(595, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 160);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transfer  / Wait Position";
            // 
            // tbSlot
            // 
            this.tbSlot.Location = new System.Drawing.Point(143, 57);
            this.tbSlot.Name = "tbSlot";
            this.tbSlot.Size = new System.Drawing.Size(77, 21);
            this.tbSlot.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(115, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Slot";
            // 
            // cbbTransfer
            // 
            this.cbbTransfer.FormattingEnabled = true;
            this.cbbTransfer.Items.AddRange(new object[] {
            "LoadPort1",
            "LoadPort2",
            "Equipment",
            "Aligner"});
            this.cbbTransfer.Location = new System.Drawing.Point(143, 31);
            this.cbbTransfer.Name = "cbbTransfer";
            this.cbbTransfer.Size = new System.Drawing.Size(77, 20);
            this.cbbTransfer.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(115, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "Port";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbLower);
            this.groupBox2.Controls.Add(this.rbUpper);
            this.groupBox2.Location = new System.Drawing.Point(10, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(82, 68);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Robot Arm";
            // 
            // rbLower
            // 
            this.rbLower.AutoSize = true;
            this.rbLower.Location = new System.Drawing.Point(6, 42);
            this.rbLower.Name = "rbLower";
            this.rbLower.Size = new System.Drawing.Size(58, 16);
            this.rbLower.TabIndex = 0;
            this.rbLower.TabStop = true;
            this.rbLower.Text = "Lower";
            this.rbLower.UseVisualStyleBackColor = true;
            // 
            // rbUpper
            // 
            this.rbUpper.AutoSize = true;
            this.rbUpper.Location = new System.Drawing.Point(6, 20);
            this.rbUpper.Name = "rbUpper";
            this.rbUpper.Size = new System.Drawing.Size(56, 16);
            this.rbUpper.TabIndex = 0;
            this.rbUpper.TabStop = true;
            this.rbUpper.Text = "Upper";
            this.rbUpper.UseVisualStyleBackColor = true;
            // 
            // btnPlace
            // 
            this.btnPlace.Location = new System.Drawing.Point(6, 126);
            this.btnPlace.Name = "btnPlace";
            this.btnPlace.Size = new System.Drawing.Size(86, 23);
            this.btnPlace.TabIndex = 0;
            this.btnPlace.Text = "PLACE";
            this.btnPlace.UseVisualStyleBackColor = true;
            this.btnPlace.Click += new System.EventHandler(this.btnPlace_Click);
            // 
            // btnPick
            // 
            this.btnPick.Location = new System.Drawing.Point(6, 97);
            this.btnPick.Name = "btnPick";
            this.btnPick.Size = new System.Drawing.Size(86, 23);
            this.btnPick.TabIndex = 0;
            this.btnPick.Text = "PICK";
            this.btnPick.UseVisualStyleBackColor = true;
            this.btnPick.Click += new System.EventHandler(this.btnPick_Click);
            // 
            // btnWaiting
            // 
            this.btnWaiting.Location = new System.Drawing.Point(145, 97);
            this.btnWaiting.Name = "btnWaiting";
            this.btnWaiting.Size = new System.Drawing.Size(123, 23);
            this.btnWaiting.TabIndex = 0;
            this.btnWaiting.Text = "WAITR";
            this.btnWaiting.UseVisualStyleBackColor = true;
            this.btnWaiting.Click += new System.EventHandler(this.btnWaiting_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkOCRForward);
            this.groupBox3.Controls.Add(this.txtRecipeName);
            this.groupBox3.Controls.Add(this.tbDegree);
            this.groupBox3.Controls.Add(this.btnAlign);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(872, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(101, 137);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Alignment";
            // 
            // chkOCRForward
            // 
            this.chkOCRForward.AutoSize = true;
            this.chkOCRForward.Location = new System.Drawing.Point(2, 81);
            this.chkOCRForward.Name = "chkOCRForward";
            this.chkOCRForward.Size = new System.Drawing.Size(100, 16);
            this.chkOCRForward.TabIndex = 4;
            this.chkOCRForward.Text = "OCR Forward";
            this.chkOCRForward.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbbLampByte2);
            this.groupBox4.Controls.Add(this.cbbLampByte1);
            this.groupBox4.Controls.Add(this.cbbLampPort);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.btnLpled);
            this.groupBox4.Location = new System.Drawing.Point(181, 178);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(341, 85);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Lamp / Buzzer";
            // 
            // cbbLampByte2
            // 
            this.cbbLampByte2.FormattingEnabled = true;
            this.cbbLampByte2.Location = new System.Drawing.Point(240, 20);
            this.cbbLampByte2.Name = "cbbLampByte2";
            this.cbbLampByte2.Size = new System.Drawing.Size(95, 20);
            this.cbbLampByte2.TabIndex = 3;
            // 
            // cbbLampByte1
            // 
            this.cbbLampByte1.FormattingEnabled = true;
            this.cbbLampByte1.Items.AddRange(new object[] {
            "Control Mode",
            "LOAD Lamp",
            "UNLOAD Lamp",
            "AUTO Lamp",
            "MANUAL Lamp",
            "RESERVE Lamp"});
            this.cbbLampByte1.Location = new System.Drawing.Point(142, 20);
            this.cbbLampByte1.Name = "cbbLampByte1";
            this.cbbLampByte1.Size = new System.Drawing.Size(95, 20);
            this.cbbLampByte1.TabIndex = 3;
            this.cbbLampByte1.SelectedIndexChanged += new System.EventHandler(this.cbbLampByte1_SelectedIndexChanged);
            // 
            // cbbLampPort
            // 
            this.cbbLampPort.FormattingEnabled = true;
            this.cbbLampPort.Items.AddRange(new object[] {
            "LoadPort1",
            "LoadPort2"});
            this.cbbLampPort.Location = new System.Drawing.Point(43, 20);
            this.cbbLampPort.Name = "cbbLampPort";
            this.cbbLampPort.Size = new System.Drawing.Size(95, 20);
            this.cbbLampPort.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "Port";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cbbBuzzer2);
            this.groupBox6.Controls.Add(this.cbbBuzzer1);
            this.groupBox6.Controls.Add(this.cbbBlueLamp);
            this.groupBox6.Controls.Add(this.cbbGreenLamp);
            this.groupBox6.Controls.Add(this.btnSiglm);
            this.groupBox6.Controls.Add(this.cbbYelloLamp);
            this.groupBox6.Controls.Add(this.cbbRedLamp);
            this.groupBox6.Controls.Add(this.comboBox4);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Location = new System.Drawing.Point(528, 178);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(404, 85);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Signal Tower Lamp / Buzzer";
            // 
            // cbbBuzzer2
            // 
            this.cbbBuzzer2.FormattingEnabled = true;
            this.cbbBuzzer2.Items.AddRange(new object[] {
            "OFF",
            "ON",
            "",
            "KEEP"});
            this.cbbBuzzer2.Location = new System.Drawing.Point(344, 33);
            this.cbbBuzzer2.Name = "cbbBuzzer2";
            this.cbbBuzzer2.Size = new System.Drawing.Size(54, 20);
            this.cbbBuzzer2.TabIndex = 3;
            this.cbbBuzzer2.Visible = false;
            this.cbbBuzzer2.SelectedIndexChanged += new System.EventHandler(this.cbbLampByte1_SelectedIndexChanged);
            // 
            // cbbBuzzer1
            // 
            this.cbbBuzzer1.FormattingEnabled = true;
            this.cbbBuzzer1.Items.AddRange(new object[] {
            "OFF",
            "ON",
            "",
            "KEEP"});
            this.cbbBuzzer1.Location = new System.Drawing.Point(286, 33);
            this.cbbBuzzer1.Name = "cbbBuzzer1";
            this.cbbBuzzer1.Size = new System.Drawing.Size(54, 20);
            this.cbbBuzzer1.TabIndex = 3;
            this.cbbBuzzer1.SelectedIndexChanged += new System.EventHandler(this.cbbLampByte1_SelectedIndexChanged);
            // 
            // cbbBlueLamp
            // 
            this.cbbBlueLamp.FormattingEnabled = true;
            this.cbbBlueLamp.Items.AddRange(new object[] {
            "OFF",
            "ON",
            "BLINK",
            "KEEP"});
            this.cbbBlueLamp.Location = new System.Drawing.Point(217, 33);
            this.cbbBlueLamp.Name = "cbbBlueLamp";
            this.cbbBlueLamp.Size = new System.Drawing.Size(63, 20);
            this.cbbBlueLamp.TabIndex = 3;
            this.cbbBlueLamp.SelectedIndexChanged += new System.EventHandler(this.cbbLampByte1_SelectedIndexChanged);
            // 
            // cbbGreenLamp
            // 
            this.cbbGreenLamp.FormattingEnabled = true;
            this.cbbGreenLamp.Items.AddRange(new object[] {
            "OFF",
            "ON",
            "BLINK",
            "KEEP"});
            this.cbbGreenLamp.Location = new System.Drawing.Point(148, 33);
            this.cbbGreenLamp.Name = "cbbGreenLamp";
            this.cbbGreenLamp.Size = new System.Drawing.Size(66, 20);
            this.cbbGreenLamp.TabIndex = 3;
            this.cbbGreenLamp.SelectedIndexChanged += new System.EventHandler(this.cbbLampByte1_SelectedIndexChanged);
            // 
            // cbbYelloLamp
            // 
            this.cbbYelloLamp.FormattingEnabled = true;
            this.cbbYelloLamp.Items.AddRange(new object[] {
            "OFF",
            "ON",
            "BLINK",
            "KEEP"});
            this.cbbYelloLamp.Location = new System.Drawing.Point(79, 33);
            this.cbbYelloLamp.Name = "cbbYelloLamp";
            this.cbbYelloLamp.Size = new System.Drawing.Size(66, 20);
            this.cbbYelloLamp.TabIndex = 3;
            this.cbbYelloLamp.SelectedIndexChanged += new System.EventHandler(this.cbbLampByte1_SelectedIndexChanged);
            // 
            // cbbRedLamp
            // 
            this.cbbRedLamp.FormattingEnabled = true;
            this.cbbRedLamp.Items.AddRange(new object[] {
            "OFF",
            "ON",
            "BLINK",
            "KEEP"});
            this.cbbRedLamp.Location = new System.Drawing.Point(10, 33);
            this.cbbRedLamp.Name = "cbbRedLamp";
            this.cbbRedLamp.Size = new System.Drawing.Size(66, 20);
            this.cbbRedLamp.TabIndex = 3;
            this.cbbRedLamp.SelectedIndexChanged += new System.EventHandler(this.cbbLampByte1_SelectedIndexChanged);
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "LoadPort1",
            "LoadPort2"});
            this.comboBox4.Location = new System.Drawing.Point(210, 59);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(66, 20);
            this.comboBox4.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(347, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 12);
            this.label12.TabIndex = 2;
            this.label12.Text = "Buzzer2";
            this.label12.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(286, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "Buzzer1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(221, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "Blue Lamp";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(148, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "Green Lamp";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(79, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "Yello Lamp";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "Red Lamp";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(173, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "Port";
            // 
            // rdTcp
            // 
            this.rdTcp.AutoSize = true;
            this.rdTcp.Location = new System.Drawing.Point(6, 20);
            this.rdTcp.Name = "rdTcp";
            this.rdTcp.Size = new System.Drawing.Size(115, 16);
            this.rdTcp.TabIndex = 7;
            this.rdTcp.Text = "Using EFEMTcp";
            this.rdTcp.UseVisualStyleBackColor = true;
            this.rdTcp.CheckedChanged += new System.EventHandler(this.rdTcp_CheckedChanged);
            // 
            // rdProxy
            // 
            this.rdProxy.AutoSize = true;
            this.rdProxy.Checked = true;
            this.rdProxy.Location = new System.Drawing.Point(6, 42);
            this.rdProxy.Name = "rdProxy";
            this.rdProxy.Size = new System.Drawing.Size(141, 16);
            this.rdProxy.TabIndex = 8;
            this.rdProxy.TabStop = true;
            this.rdProxy.Text = "Using EFEMPcProxy";
            this.rdProxy.UseVisualStyleBackColor = true;
            this.rdProxy.CheckedChanged += new System.EventHandler(this.rdTcp_CheckedChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.rdTcp);
            this.groupBox7.Controls.Add(this.rdProxy);
            this.groupBox7.Controls.Add(this.lblProxyStep);
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Location = new System.Drawing.Point(10, 114);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(165, 93);
            this.groupBox7.TabIndex = 9;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Class Type";
            // 
            // lblProxyStep
            // 
            this.lblProxyStep.Location = new System.Drawing.Point(80, 73);
            this.lblProxyStep.Name = "lblProxyStep";
            this.lblProxyStep.Size = new System.Drawing.Size(67, 12);
            this.lblProxyStep.TabIndex = 2;
            this.lblProxyStep.Text = "123";
            this.lblProxyStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 73);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 12);
            this.label13.TabIndex = 2;
            this.label13.Text = "Proxy Step : ";
            // 
            // tmrUiUpdate
            // 
            this.tmrUiUpdate.Enabled = true;
            this.tmrUiUpdate.Tick += new System.EventHandler(this.tmrUiUpdate_Tick);
            // 
            // btnSaveIP
            // 
            this.btnSaveIP.Location = new System.Drawing.Point(119, 6);
            this.btnSaveIP.Name = "btnSaveIP";
            this.btnSaveIP.Size = new System.Drawing.Size(56, 21);
            this.btnSaveIP.TabIndex = 10;
            this.btnSaveIP.Text = "IP Save";
            this.btnSaveIP.UseVisualStyleBackColor = true;
            this.btnSaveIP.Click += new System.EventHandler(this.btnSaveIP_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lvRobotLog);
            this.groupBox8.Location = new System.Drawing.Point(10, 620);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(320, 154);
            this.groupBox8.TabIndex = 11;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Robot Log";
            // 
            // lvRobotLog
            // 
            this.lvRobotLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTime,
            this.chContent});
            this.lvRobotLog.Location = new System.Drawing.Point(6, 20);
            this.lvRobotLog.Name = "lvRobotLog";
            this.lvRobotLog.Size = new System.Drawing.Size(308, 128);
            this.lvRobotLog.TabIndex = 12;
            this.lvRobotLog.UseCompatibleStateImageBehavior = false;
            this.lvRobotLog.View = System.Windows.Forms.View.Details;
            // 
            // chTime
            // 
            this.chTime.Text = "시간";
            this.chTime.Width = 120;
            // 
            // chContent
            // 
            this.chContent.Text = "내용";
            this.chContent.Width = 183;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.lvLoadPort1Log);
            this.groupBox11.Location = new System.Drawing.Point(336, 620);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(320, 154);
            this.groupBox11.TabIndex = 11;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Load Port 1 Log";
            // 
            // lvLoadPort1Log
            // 
            this.lvLoadPort1Log.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.lvLoadPort1Log.Location = new System.Drawing.Point(6, 20);
            this.lvLoadPort1Log.Name = "lvLoadPort1Log";
            this.lvLoadPort1Log.Size = new System.Drawing.Size(308, 128);
            this.lvLoadPort1Log.TabIndex = 12;
            this.lvLoadPort1Log.UseCompatibleStateImageBehavior = false;
            this.lvLoadPort1Log.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "시간";
            this.columnHeader5.Width = 120;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "내용";
            this.columnHeader6.Width = 183;
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.lvLoadPort2Log);
            this.groupBox12.Location = new System.Drawing.Point(662, 620);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(320, 154);
            this.groupBox12.TabIndex = 11;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Load Port 2 Log";
            // 
            // lvLoadPort2Log
            // 
            this.lvLoadPort2Log.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
            this.lvLoadPort2Log.Location = new System.Drawing.Point(6, 20);
            this.lvLoadPort2Log.Name = "lvLoadPort2Log";
            this.lvLoadPort2Log.Size = new System.Drawing.Size(308, 128);
            this.lvLoadPort2Log.TabIndex = 12;
            this.lvLoadPort2Log.UseCompatibleStateImageBehavior = false;
            this.lvLoadPort2Log.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "시간";
            this.columnHeader7.Width = 120;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "내용";
            this.columnHeader8.Width = 183;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.lvSendLog);
            this.groupBox9.Location = new System.Drawing.Point(10, 269);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(320, 345);
            this.groupBox9.TabIndex = 11;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Send Log";
            // 
            // lvSendLog
            // 
            this.lvSendLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvSendLog.Location = new System.Drawing.Point(6, 20);
            this.lvSendLog.Name = "lvSendLog";
            this.lvSendLog.Size = new System.Drawing.Size(308, 319);
            this.lvSendLog.TabIndex = 12;
            this.lvSendLog.UseCompatibleStateImageBehavior = false;
            this.lvSendLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "시간";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "내용";
            this.columnHeader2.Width = 183;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.lvAlignerLog);
            this.groupBox10.Location = new System.Drawing.Point(336, 780);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(320, 154);
            this.groupBox10.TabIndex = 11;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Aligner Log";
            // 
            // lvAlignerLog
            // 
            this.lvAlignerLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lvAlignerLog.Location = new System.Drawing.Point(6, 20);
            this.lvAlignerLog.Name = "lvAlignerLog";
            this.lvAlignerLog.Size = new System.Drawing.Size(308, 128);
            this.lvAlignerLog.TabIndex = 12;
            this.lvAlignerLog.UseCompatibleStateImageBehavior = false;
            this.lvAlignerLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "시간";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "내용";
            this.columnHeader4.Width = 183;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.lvEtcLog);
            this.groupBox13.Location = new System.Drawing.Point(10, 780);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(320, 154);
            this.groupBox13.TabIndex = 11;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "ETC Log";
            // 
            // lvEtcLog
            // 
            this.lvEtcLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10});
            this.lvEtcLog.Location = new System.Drawing.Point(6, 20);
            this.lvEtcLog.Name = "lvEtcLog";
            this.lvEtcLog.Size = new System.Drawing.Size(308, 128);
            this.lvEtcLog.TabIndex = 12;
            this.lvEtcLog.UseCompatibleStateImageBehavior = false;
            this.lvEtcLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "시간";
            this.columnHeader9.Width = 120;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "내용";
            this.columnHeader10.Width = 183;
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.lvRecvLog);
            this.groupBox14.Location = new System.Drawing.Point(336, 269);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(506, 345);
            this.groupBox14.TabIndex = 11;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "All Recv Data Log";
            // 
            // lvRecvLog
            // 
            this.lvRecvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader14});
            this.lvRecvLog.Location = new System.Drawing.Point(8, 20);
            this.lvRecvLog.Name = "lvRecvLog";
            this.lvRecvLog.Size = new System.Drawing.Size(486, 319);
            this.lvRecvLog.TabIndex = 12;
            this.lvRecvLog.UseCompatibleStateImageBehavior = false;
            this.lvRecvLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "시간";
            this.columnHeader11.Width = 120;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "포트";
            this.columnHeader12.Width = 140;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "내용";
            this.columnHeader14.Width = 220;
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.lblSeqRun);
            this.groupBox15.Controls.Add(this.lblHomeRun);
            this.groupBox15.Controls.Add(this.label17);
            this.groupBox15.Controls.Add(this.label16);
            this.groupBox15.Controls.Add(this.label15);
            this.groupBox15.Controls.Add(this.lblAlignerStep);
            this.groupBox15.Controls.Add(this.lblLpm2Step);
            this.groupBox15.Controls.Add(this.lblLpm1Step);
            this.groupBox15.Controls.Add(this.lblRobotStep);
            this.groupBox15.Controls.Add(this.label14);
            this.groupBox15.Location = new System.Drawing.Point(662, 780);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(314, 154);
            this.groupBox15.TabIndex = 12;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "시퀀스";
            // 
            // lblSeqRun
            // 
            this.lblSeqRun.AutoSize = true;
            this.lblSeqRun.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblSeqRun.Location = new System.Drawing.Point(221, 0);
            this.lblSeqRun.Name = "lblSeqRun";
            this.lblSeqRun.Size = new System.Drawing.Size(62, 12);
            this.lblSeqRun.TabIndex = 4;
            this.lblSeqRun.Text = "Sequence";
            this.lblSeqRun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHomeRun
            // 
            this.lblHomeRun.AutoSize = true;
            this.lblHomeRun.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblHomeRun.Location = new System.Drawing.Point(136, 0);
            this.lblHomeRun.Name = "lblHomeRun";
            this.lblHomeRun.Size = new System.Drawing.Size(67, 12);
            this.lblHomeRun.TabIndex = 4;
            this.lblHomeRun.Text = "RunMode :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 119);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(44, 12);
            this.label17.TabIndex = 3;
            this.label17.Text = "Aligner";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 86);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(37, 12);
            this.label16.TabIndex = 3;
            this.label16.Text = "LPM2";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 53);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(37, 12);
            this.label15.TabIndex = 3;
            this.label15.Text = "LPM1";
            // 
            // lblAlignerStep
            // 
            this.lblAlignerStep.BackColor = System.Drawing.Color.Bisque;
            this.lblAlignerStep.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblAlignerStep.Location = new System.Drawing.Point(7, 131);
            this.lblAlignerStep.Name = "lblAlignerStep";
            this.lblAlignerStep.Size = new System.Drawing.Size(296, 21);
            this.lblAlignerStep.TabIndex = 3;
            this.lblAlignerStep.Text = "Robot";
            this.lblAlignerStep.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLpm2Step
            // 
            this.lblLpm2Step.BackColor = System.Drawing.Color.Bisque;
            this.lblLpm2Step.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblLpm2Step.Location = new System.Drawing.Point(7, 98);
            this.lblLpm2Step.Name = "lblLpm2Step";
            this.lblLpm2Step.Size = new System.Drawing.Size(296, 21);
            this.lblLpm2Step.TabIndex = 3;
            this.lblLpm2Step.Text = "Robot";
            this.lblLpm2Step.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLpm1Step
            // 
            this.lblLpm1Step.BackColor = System.Drawing.Color.Bisque;
            this.lblLpm1Step.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblLpm1Step.Location = new System.Drawing.Point(7, 65);
            this.lblLpm1Step.Name = "lblLpm1Step";
            this.lblLpm1Step.Size = new System.Drawing.Size(296, 21);
            this.lblLpm1Step.TabIndex = 3;
            this.lblLpm1Step.Text = "Robot";
            this.lblLpm1Step.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRobotStep
            // 
            this.lblRobotStep.BackColor = System.Drawing.Color.Bisque;
            this.lblRobotStep.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblRobotStep.Location = new System.Drawing.Point(7, 32);
            this.lblRobotStep.Name = "lblRobotStep";
            this.lblRobotStep.Size = new System.Drawing.Size(296, 21);
            this.lblRobotStep.TabIndex = 3;
            this.lblRobotStep.Text = "Robot";
            this.lblRobotStep.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 12);
            this.label14.TabIndex = 3;
            this.label14.Text = "Robot";
            // 
            // btnEFEMHome
            // 
            this.btnEFEMHome.Location = new System.Drawing.Point(848, 535);
            this.btnEFEMHome.Name = "btnEFEMHome";
            this.btnEFEMHome.Size = new System.Drawing.Size(125, 23);
            this.btnEFEMHome.TabIndex = 4;
            this.btnEFEMHome.Text = "Home";
            this.btnEFEMHome.UseVisualStyleBackColor = true;
            this.btnEFEMHome.Click += new System.EventHandler(this.btnEFEMStop_Click);
            // 
            // btnEFEMStop
            // 
            this.btnEFEMStop.Location = new System.Drawing.Point(848, 507);
            this.btnEFEMStop.Name = "btnEFEMStop";
            this.btnEFEMStop.Size = new System.Drawing.Size(125, 23);
            this.btnEFEMStop.TabIndex = 4;
            this.btnEFEMStop.Text = "Stop";
            this.btnEFEMStop.UseVisualStyleBackColor = true;
            this.btnEFEMStop.Click += new System.EventHandler(this.btnEFEMStop_Click);
            // 
            // btnEFEMStart
            // 
            this.btnEFEMStart.Location = new System.Drawing.Point(848, 591);
            this.btnEFEMStart.Name = "btnEFEMStart";
            this.btnEFEMStart.Size = new System.Drawing.Size(125, 23);
            this.btnEFEMStart.TabIndex = 4;
            this.btnEFEMStart.Text = "Start";
            this.btnEFEMStart.UseVisualStyleBackColor = true;
            this.btnEFEMStart.Click += new System.EventHandler(this.btnEFEMStop_Click);
            // 
            // btnEFEMPause
            // 
            this.btnEFEMPause.Location = new System.Drawing.Point(848, 563);
            this.btnEFEMPause.Name = "btnEFEMPause";
            this.btnEFEMPause.Size = new System.Drawing.Size(125, 23);
            this.btnEFEMPause.TabIndex = 4;
            this.btnEFEMPause.Text = "Pause";
            this.btnEFEMPause.UseVisualStyleBackColor = true;
            this.btnEFEMPause.Click += new System.EventHandler(this.btnEFEMStop_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(3, 26);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(44, 12);
            this.label18.TabIndex = 4;
            this.label18.Text = "Recipe";
            // 
            // txtRecipeName
            // 
            this.txtRecipeName.Location = new System.Drawing.Point(54, 23);
            this.txtRecipeName.Name = "txtRecipeName";
            this.txtRecipeName.Size = new System.Drawing.Size(44, 21);
            this.txtRecipeName.TabIndex = 5;
            // 
            // FrmEfemTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 934);
            this.Controls.Add(this.btnEFEMPause);
            this.Controls.Add(this.btnEFEMStart);
            this.Controls.Add(this.btnEFEMStop);
            this.Controls.Add(this.btnEFEMHome);
            this.Controls.Add(this.groupBox15);
            this.Controls.Add(this.groupBox13);
            this.Controls.Add(this.groupBox12);
            this.Controls.Add(this.groupBox14);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.btnSaveIP);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbCommand);
            this.Controls.Add(this.tbTestMsg);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.tbIP);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnConnect);
            this.Name = "FrmEfemTest";
            this.Text = "FrmEfemTest";
            this.gbCommand.ResumeLayout(false);
            this.gbCommand.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox tbIP;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbTestMsg;
        private System.Windows.Forms.GroupBox gbCommand;
        private System.Windows.Forms.ComboBox cbBasicCmd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChmdm;
        private System.Windows.Forms.Button btnSiglm;
        private System.Windows.Forms.Button btnChmda;
        private System.Windows.Forms.Button btnLpled;
        private System.Windows.Forms.Button btnMapping;
        private System.Windows.Forms.Button btnUnload;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnUnDock;
        private System.Windows.Forms.Button btnDock;
        private System.Windows.Forms.Button btnUnClamp;
        private System.Windows.Forms.Button btnClamp;
        private System.Windows.Forms.Button btnAlign;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStat;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.TextBox tbDegree;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbSlot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbTransfer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnWaiting;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbbLampByte2;
        private System.Windows.Forms.ComboBox cbbLampByte1;
        private System.Windows.Forms.ComboBox cbbLampPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cbbBuzzer2;
        private System.Windows.Forms.ComboBox cbbBuzzer1;
        private System.Windows.Forms.ComboBox cbbBlueLamp;
        private System.Windows.Forms.ComboBox cbbGreenLamp;
        private System.Windows.Forms.ComboBox cbbYelloLamp;
        private System.Windows.Forms.ComboBox cbbRedLamp;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rdTcp;
        private System.Windows.Forms.RadioButton rdProxy;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label lblProxyStep;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Timer tmrUiUpdate;
        private System.Windows.Forms.Button btnSaveIP;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.GroupBox groupBox8;
        private ListViewEx lvRobotLog;
        private System.Windows.Forms.ColumnHeader chTime;
        private System.Windows.Forms.ColumnHeader chContent;
        private System.Windows.Forms.GroupBox groupBox11;
        private ListViewEx lvLoadPort1Log;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.GroupBox groupBox12;
        private ListViewEx lvLoadPort2Log;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.GroupBox groupBox9;
        private ListViewEx lvSendLog;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox10;
        private ListViewEx lvAlignerLog;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox groupBox13;
        private ListViewEx lvEtcLog;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.GroupBox groupBox14;
        private ListViewEx lvRecvLog;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblAlignerStep;
        private System.Windows.Forms.Label lblLpm2Step;
        private System.Windows.Forms.Label lblLpm1Step;
        private System.Windows.Forms.Label lblRobotStep;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblSeqRun;
        private System.Windows.Forms.Label lblHomeRun;
        private System.Windows.Forms.Button btnEFEMHome;
        private System.Windows.Forms.Button btnEFEMStop;
        private System.Windows.Forms.Button btnEFEMStart;
        private System.Windows.Forms.Button btnEFEMPause;
        private System.Windows.Forms.CheckBox chkOCRForward;
        private System.Windows.Forms.Button btnPardy;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbLower;
        private System.Windows.Forms.RadioButton rbUpper;
        private System.Windows.Forms.Button btnPlace;
        private System.Windows.Forms.Button btnPick;
        private System.Windows.Forms.TextBox txtRecipeName;
        private System.Windows.Forms.Label label18;
    }
}