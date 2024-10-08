namespace EquipMainUi
{
    partial class FrmTester
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabpInsp = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblInspRepeatCount = new System.Windows.Forms.TextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnInspRepeatModeOff = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnInspRepeatModeOn = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.label8 = new System.Windows.Forms.Label();
            this.tbLpm2CstID = new System.Windows.Forms.TextBox();
            this.tbLpm1CstID = new System.Windows.Forms.TextBox();
            this.lblOHTStep2 = new System.Windows.Forms.Label();
            this.lblOHTstep1 = new System.Windows.Forms.Label();
            this.btnOHTULoadLpm2 = new System.Windows.Forms.Button();
            this.btnLpm2StepReset = new System.Windows.Forms.Button();
            this.btnLpm2LoadComplete = new System.Windows.Forms.Button();
            this.btnLpm2ULoadComplete = new System.Windows.Forms.Button();
            this.btnLpm1ULoadComplete = new System.Windows.Forms.Button();
            this.btnLpm1LoadComplete = new System.Windows.Forms.Button();
            this.btnLpm1StepReset = new System.Windows.Forms.Button();
            this.btnOHTULoadLpm1 = new System.Windows.Forms.Button();
            this.btnOHTLoadLpm2 = new System.Windows.Forms.Button();
            this.btnOHTLoadLpm1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboInspCmd = new System.Windows.Forms.ComboBox();
            this.btnInspCommand = new System.Windows.Forms.Button();
            this.tabpHSMS = new System.Windows.Forms.TabPage();
            this.tabpEFEM = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.tabpDB = new System.Windows.Forms.TabPage();
            this.tabpRFID = new System.Windows.Forms.TabPage();
            this.tabPageOCR = new System.Windows.Forms.TabPage();
            this.tabPageBCR = new System.Windows.Forms.TabPage();
            this.tabPEziMotor = new System.Windows.Forms.TabPage();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblInspRepeatTotal = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabpInsp.SuspendLayout();
            this.panel8.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabpEFEM.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabpInsp);
            this.tabControl1.Controls.Add(this.tabpHSMS);
            this.tabControl1.Controls.Add(this.tabpEFEM);
            this.tabControl1.Controls.Add(this.tabpDB);
            this.tabControl1.Controls.Add(this.tabpRFID);
            this.tabControl1.Controls.Add(this.tabPageOCR);
            this.tabControl1.Controls.Add(this.tabPageBCR);
            this.tabControl1.Controls.Add(this.tabPEziMotor);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(100, 30);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(989, 1061);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 14;
            // 
            // tabpInsp
            // 
            this.tabpInsp.Controls.Add(this.lblInspRepeatTotal);
            this.tabpInsp.Controls.Add(this.label1);
            this.tabpInsp.Controls.Add(this.label15);
            this.tabpInsp.Controls.Add(this.lblInspRepeatCount);
            this.tabpInsp.Controls.Add(this.panel8);
            this.tabpInsp.Controls.Add(this.tbLpm2CstID);
            this.tabpInsp.Controls.Add(this.tbLpm1CstID);
            this.tabpInsp.Controls.Add(this.lblOHTStep2);
            this.tabpInsp.Controls.Add(this.lblOHTstep1);
            this.tabpInsp.Controls.Add(this.btnOHTULoadLpm2);
            this.tabpInsp.Controls.Add(this.btnLpm2StepReset);
            this.tabpInsp.Controls.Add(this.btnLpm2LoadComplete);
            this.tabpInsp.Controls.Add(this.btnLpm2ULoadComplete);
            this.tabpInsp.Controls.Add(this.btnLpm1ULoadComplete);
            this.tabpInsp.Controls.Add(this.btnLpm1LoadComplete);
            this.tabpInsp.Controls.Add(this.btnLpm1StepReset);
            this.tabpInsp.Controls.Add(this.btnOHTULoadLpm1);
            this.tabpInsp.Controls.Add(this.btnOHTLoadLpm2);
            this.tabpInsp.Controls.Add(this.btnOHTLoadLpm1);
            this.tabpInsp.Controls.Add(this.groupBox2);
            this.tabpInsp.Location = new System.Drawing.Point(4, 34);
            this.tabpInsp.Name = "tabpInsp";
            this.tabpInsp.Padding = new System.Windows.Forms.Padding(3);
            this.tabpInsp.Size = new System.Drawing.Size(981, 1023);
            this.tabpInsp.TabIndex = 0;
            this.tabpInsp.Text = "검사/리뷰";
            this.tabpInsp.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(264, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 12);
            this.label1.TabIndex = 460;
            this.label1.Text = "총 검사 횟수 : ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(264, 44);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(81, 12);
            this.label15.TabIndex = 458;
            this.label15.Text = "반복성 횟수 : ";
            // 
            // lblInspRepeatCount
            // 
            this.lblInspRepeatCount.Location = new System.Drawing.Point(351, 40);
            this.lblInspRepeatCount.Name = "lblInspRepeatCount";
            this.lblInspRepeatCount.Size = new System.Drawing.Size(71, 21);
            this.lblInspRepeatCount.TabIndex = 459;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.btnInspRepeatModeOff);
            this.panel8.Controls.Add(this.btnInspRepeatModeOn);
            this.panel8.Controls.Add(this.label8);
            this.panel8.Font = new System.Drawing.Font("휴먼모음T", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel8.Location = new System.Drawing.Point(428, 33);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(181, 53);
            this.panel8.TabIndex = 451;
            // 
            // btnInspRepeatModeOff
            // 
            this.btnInspRepeatModeOff.BackColor = System.Drawing.Color.Transparent;
            this.btnInspRepeatModeOff.Delay = 10;
            this.btnInspRepeatModeOff.Flicker = false;
            this.btnInspRepeatModeOff.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.btnInspRepeatModeOff.ForeColor = System.Drawing.Color.Black;
            this.btnInspRepeatModeOff.IsLeftLampOn = false;
            this.btnInspRepeatModeOff.IsRightLampOn = false;
            this.btnInspRepeatModeOff.LampAliveTime = 500;
            this.btnInspRepeatModeOff.LampSize = 1;
            this.btnInspRepeatModeOff.LeftLampColor = System.Drawing.Color.Red;
            this.btnInspRepeatModeOff.Location = new System.Drawing.Point(92, 18);
            this.btnInspRepeatModeOff.Name = "btnInspRepeatModeOff";
            this.btnInspRepeatModeOff.OnOff = false;
            this.btnInspRepeatModeOff.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnInspRepeatModeOff.Size = new System.Drawing.Size(84, 27);
            this.btnInspRepeatModeOff.TabIndex = 27;
            this.btnInspRepeatModeOff.TabStop = false;
            this.btnInspRepeatModeOff.Text = "미사용";
            this.btnInspRepeatModeOff.Text2 = "";
            this.btnInspRepeatModeOff.UseVisualStyleBackColor = false;
            this.btnInspRepeatModeOff.VisibleLeftLamp = false;
            this.btnInspRepeatModeOff.VisibleRightLamp = false;
            this.btnInspRepeatModeOff.Click += new System.EventHandler(this.btnInspRepeatModeOn_Click);
            // 
            // btnInspRepeatModeOn
            // 
            this.btnInspRepeatModeOn.BackColor = System.Drawing.Color.Transparent;
            this.btnInspRepeatModeOn.Delay = 10;
            this.btnInspRepeatModeOn.Flicker = false;
            this.btnInspRepeatModeOn.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.btnInspRepeatModeOn.ForeColor = System.Drawing.Color.Black;
            this.btnInspRepeatModeOn.IsLeftLampOn = false;
            this.btnInspRepeatModeOn.IsRightLampOn = false;
            this.btnInspRepeatModeOn.LampAliveTime = 500;
            this.btnInspRepeatModeOn.LampSize = 1;
            this.btnInspRepeatModeOn.LeftLampColor = System.Drawing.Color.Red;
            this.btnInspRepeatModeOn.Location = new System.Drawing.Point(3, 18);
            this.btnInspRepeatModeOn.Name = "btnInspRepeatModeOn";
            this.btnInspRepeatModeOn.OnOff = false;
            this.btnInspRepeatModeOn.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnInspRepeatModeOn.Size = new System.Drawing.Size(84, 27);
            this.btnInspRepeatModeOn.TabIndex = 27;
            this.btnInspRepeatModeOn.TabStop = false;
            this.btnInspRepeatModeOn.Text = "사용";
            this.btnInspRepeatModeOn.Text2 = "";
            this.btnInspRepeatModeOn.UseVisualStyleBackColor = false;
            this.btnInspRepeatModeOn.VisibleLeftLamp = false;
            this.btnInspRepeatModeOn.VisibleRightLamp = false;
            this.btnInspRepeatModeOn.Click += new System.EventHandler(this.btnInspRepeatModeOn_Click);
            // 
            // label8
            // 
            this.label8.AutoEllipsis = true;
            this.label8.BackColor = System.Drawing.Color.Gainsboro;
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(180, 15);
            this.label8.TabIndex = 9;
            this.label8.Text = "■ 검사 반복성 사용";
            this.label8.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // tbLpm2CstID
            // 
            this.tbLpm2CstID.Location = new System.Drawing.Point(263, 345);
            this.tbLpm2CstID.Name = "tbLpm2CstID";
            this.tbLpm2CstID.Size = new System.Drawing.Size(100, 21);
            this.tbLpm2CstID.TabIndex = 17;
            // 
            // tbLpm1CstID
            // 
            this.tbLpm1CstID.Location = new System.Drawing.Point(85, 345);
            this.tbLpm1CstID.Name = "tbLpm1CstID";
            this.tbLpm1CstID.Size = new System.Drawing.Size(100, 21);
            this.tbLpm1CstID.TabIndex = 17;
            // 
            // lblOHTStep2
            // 
            this.lblOHTStep2.AutoSize = true;
            this.lblOHTStep2.Location = new System.Drawing.Point(261, 249);
            this.lblOHTStep2.Name = "lblOHTStep2";
            this.lblOHTStep2.Size = new System.Drawing.Size(38, 12);
            this.lblOHTStep2.TabIndex = 16;
            this.lblOHTStep2.Text = "label1";
            // 
            // lblOHTstep1
            // 
            this.lblOHTstep1.AutoSize = true;
            this.lblOHTstep1.Location = new System.Drawing.Point(91, 249);
            this.lblOHTstep1.Name = "lblOHTstep1";
            this.lblOHTstep1.Size = new System.Drawing.Size(38, 12);
            this.lblOHTstep1.TabIndex = 16;
            this.lblOHTstep1.Text = "label1";
            // 
            // btnOHTULoadLpm2
            // 
            this.btnOHTULoadLpm2.Location = new System.Drawing.Point(263, 200);
            this.btnOHTULoadLpm2.Name = "btnOHTULoadLpm2";
            this.btnOHTULoadLpm2.Size = new System.Drawing.Size(96, 23);
            this.btnOHTULoadLpm2.TabIndex = 15;
            this.btnOHTULoadLpm2.Text = "ULOAD LPM2";
            this.btnOHTULoadLpm2.UseVisualStyleBackColor = true;
            this.btnOHTULoadLpm2.Click += new System.EventHandler(this.btnOHTLoadLpm1_Click);
            // 
            // btnLpm2StepReset
            // 
            this.btnLpm2StepReset.Location = new System.Drawing.Point(268, 283);
            this.btnLpm2StepReset.Name = "btnLpm2StepReset";
            this.btnLpm2StepReset.Size = new System.Drawing.Size(91, 23);
            this.btnLpm2StepReset.TabIndex = 14;
            this.btnLpm2StepReset.Text = "Step Reset";
            this.btnLpm2StepReset.UseVisualStyleBackColor = true;
            this.btnLpm2StepReset.Click += new System.EventHandler(this.btnOHTLoadLpm1_Click);
            // 
            // btnLpm2LoadComplete
            // 
            this.btnLpm2LoadComplete.Location = new System.Drawing.Point(263, 382);
            this.btnLpm2LoadComplete.Name = "btnLpm2LoadComplete";
            this.btnLpm2LoadComplete.Size = new System.Drawing.Size(91, 23);
            this.btnLpm2LoadComplete.TabIndex = 14;
            this.btnLpm2LoadComplete.Text = "Load Comp";
            this.btnLpm2LoadComplete.UseVisualStyleBackColor = true;
            this.btnLpm2LoadComplete.Click += new System.EventHandler(this.btnLpm2LoadComplete_Click);
            // 
            // btnLpm2ULoadComplete
            // 
            this.btnLpm2ULoadComplete.Location = new System.Drawing.Point(263, 411);
            this.btnLpm2ULoadComplete.Name = "btnLpm2ULoadComplete";
            this.btnLpm2ULoadComplete.Size = new System.Drawing.Size(91, 23);
            this.btnLpm2ULoadComplete.TabIndex = 14;
            this.btnLpm2ULoadComplete.Text = "ULoad Comp";
            this.btnLpm2ULoadComplete.UseVisualStyleBackColor = true;
            this.btnLpm2ULoadComplete.Click += new System.EventHandler(this.btnLpm2ULoadComplete_Click);
            // 
            // btnLpm1ULoadComplete
            // 
            this.btnLpm1ULoadComplete.Location = new System.Drawing.Point(87, 411);
            this.btnLpm1ULoadComplete.Name = "btnLpm1ULoadComplete";
            this.btnLpm1ULoadComplete.Size = new System.Drawing.Size(91, 23);
            this.btnLpm1ULoadComplete.TabIndex = 14;
            this.btnLpm1ULoadComplete.Text = "ULoad Comp";
            this.btnLpm1ULoadComplete.UseVisualStyleBackColor = true;
            this.btnLpm1ULoadComplete.Click += new System.EventHandler(this.btnLpm1ULoadComplete_Click);
            // 
            // btnLpm1LoadComplete
            // 
            this.btnLpm1LoadComplete.Location = new System.Drawing.Point(87, 382);
            this.btnLpm1LoadComplete.Name = "btnLpm1LoadComplete";
            this.btnLpm1LoadComplete.Size = new System.Drawing.Size(91, 23);
            this.btnLpm1LoadComplete.TabIndex = 14;
            this.btnLpm1LoadComplete.Text = "Load Comp";
            this.btnLpm1LoadComplete.UseVisualStyleBackColor = true;
            this.btnLpm1LoadComplete.Click += new System.EventHandler(this.btnLpm1LoadComplete_Click);
            // 
            // btnLpm1StepReset
            // 
            this.btnLpm1StepReset.Location = new System.Drawing.Point(85, 283);
            this.btnLpm1StepReset.Name = "btnLpm1StepReset";
            this.btnLpm1StepReset.Size = new System.Drawing.Size(91, 23);
            this.btnLpm1StepReset.TabIndex = 14;
            this.btnLpm1StepReset.Text = "Step Reset";
            this.btnLpm1StepReset.UseVisualStyleBackColor = true;
            this.btnLpm1StepReset.Click += new System.EventHandler(this.btnOHTLoadLpm1_Click);
            // 
            // btnOHTULoadLpm1
            // 
            this.btnOHTULoadLpm1.Location = new System.Drawing.Point(85, 200);
            this.btnOHTULoadLpm1.Name = "btnOHTULoadLpm1";
            this.btnOHTULoadLpm1.Size = new System.Drawing.Size(91, 23);
            this.btnOHTULoadLpm1.TabIndex = 14;
            this.btnOHTULoadLpm1.Text = "ULOAD Lpm1";
            this.btnOHTULoadLpm1.UseVisualStyleBackColor = true;
            this.btnOHTULoadLpm1.Click += new System.EventHandler(this.btnOHTLoadLpm1_Click);
            // 
            // btnOHTLoadLpm2
            // 
            this.btnOHTLoadLpm2.Location = new System.Drawing.Point(263, 160);
            this.btnOHTLoadLpm2.Name = "btnOHTLoadLpm2";
            this.btnOHTLoadLpm2.Size = new System.Drawing.Size(96, 23);
            this.btnOHTLoadLpm2.TabIndex = 15;
            this.btnOHTLoadLpm2.Text = "LOAD LPM2";
            this.btnOHTLoadLpm2.UseVisualStyleBackColor = true;
            this.btnOHTLoadLpm2.Click += new System.EventHandler(this.btnOHTLoadLpm1_Click);
            // 
            // btnOHTLoadLpm1
            // 
            this.btnOHTLoadLpm1.Location = new System.Drawing.Point(85, 160);
            this.btnOHTLoadLpm1.Name = "btnOHTLoadLpm1";
            this.btnOHTLoadLpm1.Size = new System.Drawing.Size(91, 23);
            this.btnOHTLoadLpm1.TabIndex = 14;
            this.btnOHTLoadLpm1.Text = "LOAD Lpm1";
            this.btnOHTLoadLpm1.UseVisualStyleBackColor = true;
            this.btnOHTLoadLpm1.Click += new System.EventHandler(this.btnOHTLoadLpm1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboInspCmd);
            this.groupBox2.Controls.Add(this.btnInspCommand);
            this.groupBox2.Location = new System.Drawing.Point(26, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(226, 58);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Inspection";
            // 
            // cboInspCmd
            // 
            this.cboInspCmd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInspCmd.FormattingEnabled = true;
            this.cboInspCmd.Location = new System.Drawing.Point(6, 24);
            this.cboInspCmd.Name = "cboInspCmd";
            this.cboInspCmd.Size = new System.Drawing.Size(97, 20);
            this.cboInspCmd.TabIndex = 1;
            // 
            // btnInspCommand
            // 
            this.btnInspCommand.Location = new System.Drawing.Point(107, 13);
            this.btnInspCommand.Name = "btnInspCommand";
            this.btnInspCommand.Size = new System.Drawing.Size(90, 38);
            this.btnInspCommand.TabIndex = 0;
            this.btnInspCommand.Text = "INSP COMMAND";
            this.btnInspCommand.UseVisualStyleBackColor = true;
            this.btnInspCommand.Click += new System.EventHandler(this.btnInspCommand_Click);
            // 
            // tabpHSMS
            // 
            this.tabpHSMS.Location = new System.Drawing.Point(4, 34);
            this.tabpHSMS.Name = "tabpHSMS";
            this.tabpHSMS.Padding = new System.Windows.Forms.Padding(3);
            this.tabpHSMS.Size = new System.Drawing.Size(981, 1023);
            this.tabpHSMS.TabIndex = 2;
            this.tabpHSMS.Text = "HSMS";
            this.tabpHSMS.UseVisualStyleBackColor = true;
            // 
            // tabpEFEM
            // 
            this.tabpEFEM.Controls.Add(this.button1);
            this.tabpEFEM.Location = new System.Drawing.Point(4, 34);
            this.tabpEFEM.Name = "tabpEFEM";
            this.tabpEFEM.Padding = new System.Windows.Forms.Padding(3);
            this.tabpEFEM.Size = new System.Drawing.Size(981, 1023);
            this.tabpEFEM.TabIndex = 3;
            this.tabpEFEM.Text = "EFEM";
            this.tabpEFEM.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(255, 102);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tabpDB
            // 
            this.tabpDB.Location = new System.Drawing.Point(4, 34);
            this.tabpDB.Name = "tabpDB";
            this.tabpDB.Padding = new System.Windows.Forms.Padding(3);
            this.tabpDB.Size = new System.Drawing.Size(981, 1023);
            this.tabpDB.TabIndex = 4;
            this.tabpDB.Text = "DB";
            this.tabpDB.UseVisualStyleBackColor = true;
            // 
            // tabpRFID
            // 
            this.tabpRFID.Location = new System.Drawing.Point(4, 34);
            this.tabpRFID.Name = "tabpRFID";
            this.tabpRFID.Padding = new System.Windows.Forms.Padding(3);
            this.tabpRFID.Size = new System.Drawing.Size(981, 1023);
            this.tabpRFID.TabIndex = 5;
            this.tabpRFID.Text = "RFID";
            this.tabpRFID.UseVisualStyleBackColor = true;
            // 
            // tabPageOCR
            // 
            this.tabPageOCR.Location = new System.Drawing.Point(4, 34);
            this.tabPageOCR.Name = "tabPageOCR";
            this.tabPageOCR.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOCR.Size = new System.Drawing.Size(981, 1023);
            this.tabPageOCR.TabIndex = 6;
            this.tabPageOCR.Text = "OCR";
            this.tabPageOCR.UseVisualStyleBackColor = true;
            // 
            // tabPageBCR
            // 
            this.tabPageBCR.Location = new System.Drawing.Point(4, 34);
            this.tabPageBCR.Name = "tabPageBCR";
            this.tabPageBCR.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBCR.Size = new System.Drawing.Size(981, 1023);
            this.tabPageBCR.TabIndex = 7;
            this.tabPageBCR.Text = "BCR";
            this.tabPageBCR.UseVisualStyleBackColor = true;
            // 
            // tabPEziMotor
            // 
            this.tabPEziMotor.Location = new System.Drawing.Point(4, 34);
            this.tabPEziMotor.Name = "tabPEziMotor";
            this.tabPEziMotor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPEziMotor.Size = new System.Drawing.Size(981, 1023);
            this.tabPEziMotor.TabIndex = 8;
            this.tabPEziMotor.Text = "EziMotor";
            this.tabPEziMotor.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblInspRepeatTotal
            // 
            this.lblInspRepeatTotal.AutoSize = true;
            this.lblInspRepeatTotal.Location = new System.Drawing.Point(352, 68);
            this.lblInspRepeatTotal.Name = "lblInspRepeatTotal";
            this.lblInspRepeatTotal.Size = new System.Drawing.Size(0, 12);
            this.lblInspRepeatTotal.TabIndex = 461;
            // 
            // FrmTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 1061);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmTester";
            this.Text = "FrmTester";
            this.tabControl1.ResumeLayout(false);
            this.tabpInsp.ResumeLayout(false);
            this.tabpInsp.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabpEFEM.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabpInsp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboInspCmd;
        private System.Windows.Forms.Button btnInspCommand;
        private System.Windows.Forms.TabPage tabpHSMS;
        private System.Windows.Forms.TabPage tabpEFEM;
        private System.Windows.Forms.TabPage tabpDB;
        private System.Windows.Forms.TabPage tabpRFID;
        private System.Windows.Forms.TabPage tabPageOCR;
        private System.Windows.Forms.TabPage tabPageBCR;
        private Test.ucrlHsmsTest ucrlHsmsTest1;
        private System.Windows.Forms.Button btnOHTULoadLpm2;
        private System.Windows.Forms.Button btnOHTULoadLpm1;
        private System.Windows.Forms.Button btnOHTLoadLpm2;
        private System.Windows.Forms.Button btnOHTLoadLpm1;
        private System.Windows.Forms.Label lblOHTstep1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblOHTStep2;
        private System.Windows.Forms.Button btnLpm2StepReset;
        private System.Windows.Forms.Button btnLpm1StepReset;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbLpm2CstID;
        private System.Windows.Forms.TextBox tbLpm1CstID;
        private System.Windows.Forms.Button btnLpm2LoadComplete;
        private System.Windows.Forms.Button btnLpm2ULoadComplete;
        private System.Windows.Forms.Button btnLpm1ULoadComplete;
        private System.Windows.Forms.Button btnLpm1LoadComplete;
        private System.Windows.Forms.TabPage tabPEziMotor;
        private System.Windows.Forms.Panel panel8;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnInspRepeatModeOff;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnInspRepeatModeOn;
        internal System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox lblInspRepeatCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInspRepeatTotal;
    }
}