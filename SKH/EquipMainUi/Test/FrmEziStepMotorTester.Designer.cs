namespace EquipMainUi.Test
{
    partial class FrmEziStepMotorTester
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxPortNo = new System.Windows.Forms.ComboBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.comboBaudrate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonJogNegative = new System.Windows.Forms.Button();
            this.buttonJogPositive = new System.Windows.Forms.Button();
            this.textBoxJogSpd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tmrUiUpdate = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbPtpAcc = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbPtpSpeed = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbPtpPosi = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCurPtpStep = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnPtpMove = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblStatusMoving = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblStatusError = new System.Windows.Forms.Label();
            this.lblStatusHomeComp = new System.Windows.Forms.Label();
            this.lblStatusMinusLimit = new System.Windows.Forms.Label();
            this.lblStatusPlusLimit = new System.Windows.Forms.Label();
            this.lblStatusSvOn = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbCurSpeed = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbCurPosition = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.rbSalveNo0 = new System.Windows.Forms.RadioButton();
            this.btnMotorStop = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbSalveNo2 = new System.Windows.Forms.RadioButton();
            this.rbSalveNo1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCurStepMotorName = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnSetOriginPosition = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.lblCurHomeStep = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnAlignLogicStop = new System.Windows.Forms.Button();
            this.btnRecvPioSigOff = new System.Windows.Forms.Button();
            this.btnSendPioSigOff = new System.Windows.Forms.Button();
            this.btnWaferDetectOff = new System.Windows.Forms.Button();
            this.btnWaferDetectOn = new System.Windows.Forms.Button();
            this.btnRecvSigOn = new System.Windows.Forms.Button();
            this.btnSendPioSigOn = new System.Windows.Forms.Button();
            this.btnAlignLogicHome = new System.Windows.Forms.Button();
            this.btnSeqLogicStart = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxPortNo);
            this.groupBox1.Controls.Add(this.buttonConnect);
            this.groupBox1.Controls.Add(this.comboBaudrate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 77);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connect";
            // 
            // comboBoxPortNo
            // 
            this.comboBoxPortNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPortNo.FormattingEnabled = true;
            this.comboBoxPortNo.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600"});
            this.comboBoxPortNo.Location = new System.Drawing.Point(72, 18);
            this.comboBoxPortNo.Name = "comboBoxPortNo";
            this.comboBoxPortNo.Size = new System.Drawing.Size(85, 20);
            this.comboBoxPortNo.TabIndex = 5;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(164, 18);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(97, 47);
            this.buttonConnect.TabIndex = 4;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // comboBaudrate
            // 
            this.comboBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBaudrate.FormattingEnabled = true;
            this.comboBaudrate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "460800",
            "921600"});
            this.comboBaudrate.Location = new System.Drawing.Point(72, 45);
            this.comboBaudrate.Name = "comboBaudrate";
            this.comboBaudrate.Size = new System.Drawing.Size(85, 20);
            this.comboBaudrate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Baudrate :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port No. :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonJogNegative);
            this.groupBox3.Controls.Add(this.buttonJogPositive);
            this.groupBox3.Controls.Add(this.textBoxJogSpd);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(192, 358);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(163, 80);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Jog";
            // 
            // buttonJogNegative
            // 
            this.buttonJogNegative.Location = new System.Drawing.Point(80, 50);
            this.buttonJogNegative.Name = "buttonJogNegative";
            this.buttonJogNegative.Size = new System.Drawing.Size(66, 23);
            this.buttonJogNegative.TabIndex = 10;
            this.buttonJogNegative.Text = "- Jog";
            this.buttonJogNegative.UseVisualStyleBackColor = true;
            this.buttonJogNegative.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogNegative_MouseDown);
            this.buttonJogNegative.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJog_MouseUp);
            // 
            // buttonJogPositive
            // 
            this.buttonJogPositive.Location = new System.Drawing.Point(8, 50);
            this.buttonJogPositive.Name = "buttonJogPositive";
            this.buttonJogPositive.Size = new System.Drawing.Size(66, 23);
            this.buttonJogPositive.TabIndex = 10;
            this.buttonJogPositive.Text = "+ Jog";
            this.buttonJogPositive.UseVisualStyleBackColor = true;
            this.buttonJogPositive.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonJogPositive_MouseDown);
            this.buttonJogPositive.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonJog_MouseUp);
            // 
            // textBoxJogSpd
            // 
            this.textBoxJogSpd.Location = new System.Drawing.Point(72, 20);
            this.textBoxJogSpd.Name = "textBoxJogSpd";
            this.textBoxJogSpd.Size = new System.Drawing.Size(85, 21);
            this.textBoxJogSpd.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "Speed";
            // 
            // tmrUiUpdate
            // 
            this.tmrUiUpdate.Tick += new System.EventHandler(this.tmrUiUpdate_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbPtpAcc);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tbPtpSpeed);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.tbPtpPosi);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lblCurPtpStep);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.btnPtpMove);
            this.groupBox2.Location = new System.Drawing.Point(192, 183);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(163, 175);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Point To Point";
            // 
            // tbPtpAcc
            // 
            this.tbPtpAcc.Location = new System.Drawing.Point(69, 79);
            this.tbPtpAcc.Name = "tbPtpAcc";
            this.tbPtpAcc.Size = new System.Drawing.Size(85, 21);
            this.tbPtpAcc.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "Acc";
            // 
            // tbPtpSpeed
            // 
            this.tbPtpSpeed.Location = new System.Drawing.Point(69, 52);
            this.tbPtpSpeed.Name = "tbPtpSpeed";
            this.tbPtpSpeed.Size = new System.Drawing.Size(85, 21);
            this.tbPtpSpeed.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "Speed";
            // 
            // tbPtpPosi
            // 
            this.tbPtpPosi.Location = new System.Drawing.Point(69, 25);
            this.tbPtpPosi.Name = "tbPtpPosi";
            this.tbPtpPosi.Size = new System.Drawing.Size(85, 21);
            this.tbPtpPosi.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "Position";
            // 
            // lblCurPtpStep
            // 
            this.lblCurPtpStep.AutoSize = true;
            this.lblCurPtpStep.Location = new System.Drawing.Point(91, 152);
            this.lblCurPtpStep.Name = "lblCurPtpStep";
            this.lblCurPtpStep.Size = new System.Drawing.Size(11, 12);
            this.lblCurPtpStep.TabIndex = 12;
            this.lblCurPtpStep.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 152);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "Ptp Step : ";
            // 
            // btnPtpMove
            // 
            this.btnPtpMove.Location = new System.Drawing.Point(80, 106);
            this.btnPtpMove.Name = "btnPtpMove";
            this.btnPtpMove.Size = new System.Drawing.Size(78, 30);
            this.btnPtpMove.TabIndex = 17;
            this.btnPtpMove.Text = "Move";
            this.btnPtpMove.UseVisualStyleBackColor = true;
            this.btnPtpMove.Click += new System.EventHandler(this.btnPtpMove_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(77, 160);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(46, 12);
            this.label17.TabIndex = 20;
            this.label17.Text = "Moving";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(77, 135);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 12);
            this.label15.TabIndex = 21;
            this.label15.Text = "Error";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(77, 107);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(96, 12);
            this.label14.TabIndex = 22;
            this.label14.Text = "Home Complete";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(77, 81);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 12);
            this.label13.TabIndex = 23;
            this.label13.Text = "Minus Limit";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(77, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 12);
            this.label12.TabIndex = 24;
            this.label12.Text = "Plus Limit";
            // 
            // lblStatusMoving
            // 
            this.lblStatusMoving.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lblStatusMoving.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatusMoving.Location = new System.Drawing.Point(7, 156);
            this.lblStatusMoving.Name = "lblStatusMoving";
            this.lblStatusMoving.Size = new System.Drawing.Size(58, 20);
            this.lblStatusMoving.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(77, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 12);
            this.label11.TabIndex = 25;
            this.label11.Text = "Servo On";
            // 
            // lblStatusError
            // 
            this.lblStatusError.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lblStatusError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatusError.Location = new System.Drawing.Point(7, 129);
            this.lblStatusError.Name = "lblStatusError";
            this.lblStatusError.Size = new System.Drawing.Size(58, 20);
            this.lblStatusError.TabIndex = 15;
            // 
            // lblStatusHomeComp
            // 
            this.lblStatusHomeComp.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lblStatusHomeComp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatusHomeComp.Location = new System.Drawing.Point(7, 102);
            this.lblStatusHomeComp.Name = "lblStatusHomeComp";
            this.lblStatusHomeComp.Size = new System.Drawing.Size(58, 20);
            this.lblStatusHomeComp.TabIndex = 16;
            // 
            // lblStatusMinusLimit
            // 
            this.lblStatusMinusLimit.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lblStatusMinusLimit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatusMinusLimit.Location = new System.Drawing.Point(7, 75);
            this.lblStatusMinusLimit.Name = "lblStatusMinusLimit";
            this.lblStatusMinusLimit.Size = new System.Drawing.Size(58, 20);
            this.lblStatusMinusLimit.TabIndex = 17;
            // 
            // lblStatusPlusLimit
            // 
            this.lblStatusPlusLimit.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lblStatusPlusLimit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatusPlusLimit.Location = new System.Drawing.Point(7, 50);
            this.lblStatusPlusLimit.Name = "lblStatusPlusLimit";
            this.lblStatusPlusLimit.Size = new System.Drawing.Size(58, 20);
            this.lblStatusPlusLimit.TabIndex = 18;
            // 
            // lblStatusSvOn
            // 
            this.lblStatusSvOn.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lblStatusSvOn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatusSvOn.Location = new System.Drawing.Point(7, 25);
            this.lblStatusSvOn.Name = "lblStatusSvOn";
            this.lblStatusSvOn.Size = new System.Drawing.Size(58, 20);
            this.lblStatusSvOn.TabIndex = 19;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbCurSpeed);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.lblStatusSvOn);
            this.groupBox4.Controls.Add(this.tbCurPosition);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.lblStatusPlusLimit);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.lblStatusMinusLimit);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.lblStatusHomeComp);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.lblStatusError);
            this.groupBox4.Controls.Add(this.lblStatusMoving);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Location = new System.Drawing.Point(361, 182);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(182, 256);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Status";
            // 
            // tbCurSpeed
            // 
            this.tbCurSpeed.Location = new System.Drawing.Point(76, 221);
            this.tbCurSpeed.Name = "tbCurSpeed";
            this.tbCurSpeed.Size = new System.Drawing.Size(85, 21);
            this.tbCurSpeed.TabIndex = 16;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(11, 224);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 15;
            this.label19.Text = "Cur Speed";
            // 
            // tbCurPosition
            // 
            this.tbCurPosition.Location = new System.Drawing.Point(76, 194);
            this.tbCurPosition.Name = "tbCurPosition";
            this.tbCurPosition.Size = new System.Drawing.Size(85, 21);
            this.tbCurPosition.TabIndex = 14;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(11, 197);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(54, 12);
            this.label18.TabIndex = 13;
            this.label18.Text = "Cur Posi";
            // 
            // rbSalveNo0
            // 
            this.rbSalveNo0.AutoSize = true;
            this.rbSalveNo0.Checked = true;
            this.rbSalveNo0.Location = new System.Drawing.Point(7, 25);
            this.rbSalveNo0.Name = "rbSalveNo0";
            this.rbSalveNo0.Size = new System.Drawing.Size(29, 16);
            this.rbSalveNo0.TabIndex = 12;
            this.rbSalveNo0.TabStop = true;
            this.rbSalveNo0.Text = "0";
            this.rbSalveNo0.UseVisualStyleBackColor = true;
            this.rbSalveNo0.Click += new System.EventHandler(this.rbSalveNo0_Click);
            // 
            // btnMotorStop
            // 
            this.btnMotorStop.Location = new System.Drawing.Point(465, 125);
            this.btnMotorStop.Name = "btnMotorStop";
            this.btnMotorStop.Size = new System.Drawing.Size(78, 30);
            this.btnMotorStop.TabIndex = 13;
            this.btnMotorStop.Text = "STOP";
            this.btnMotorStop.UseVisualStyleBackColor = true;
            this.btnMotorStop.Click += new System.EventHandler(this.btnMotorStop_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbSalveNo2);
            this.groupBox5.Controls.Add(this.rbSalveNo1);
            this.groupBox5.Controls.Add(this.rbSalveNo0);
            this.groupBox5.Location = new System.Drawing.Point(12, 110);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(114, 53);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Slave";
            // 
            // rbSalveNo2
            // 
            this.rbSalveNo2.AutoSize = true;
            this.rbSalveNo2.Location = new System.Drawing.Point(73, 25);
            this.rbSalveNo2.Name = "rbSalveNo2";
            this.rbSalveNo2.Size = new System.Drawing.Size(29, 16);
            this.rbSalveNo2.TabIndex = 12;
            this.rbSalveNo2.Text = "2";
            this.rbSalveNo2.UseVisualStyleBackColor = true;
            this.rbSalveNo2.Click += new System.EventHandler(this.rbSalveNo0_Click);
            // 
            // rbSalveNo1
            // 
            this.rbSalveNo1.AutoSize = true;
            this.rbSalveNo1.Location = new System.Drawing.Point(42, 25);
            this.rbSalveNo1.Name = "rbSalveNo1";
            this.rbSalveNo1.Size = new System.Drawing.Size(29, 16);
            this.rbSalveNo1.TabIndex = 12;
            this.rbSalveNo1.Text = "1";
            this.rbSalveNo1.UseVisualStyleBackColor = true;
            this.rbSalveNo1.Click += new System.EventHandler(this.rbSalveNo0_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "현재 제어되는 스텝 모터 : ";
            // 
            // lblCurStepMotorName
            // 
            this.lblCurStepMotorName.AutoSize = true;
            this.lblCurStepMotorName.Location = new System.Drawing.Point(284, 134);
            this.lblCurStepMotorName.Name = "lblCurStepMotorName";
            this.lblCurStepMotorName.Size = new System.Drawing.Size(25, 12);
            this.lblCurStepMotorName.TabIndex = 25;
            this.lblCurStepMotorName.Text = "null";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnSetOriginPosition);
            this.groupBox6.Controls.Add(this.btnHome);
            this.groupBox6.Controls.Add(this.lblCurHomeStep);
            this.groupBox6.Controls.Add(this.label21);
            this.groupBox6.Location = new System.Drawing.Point(23, 183);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(163, 175);
            this.groupBox6.TabIndex = 11;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Home";
            // 
            // btnSetOriginPosition
            // 
            this.btnSetOriginPosition.Location = new System.Drawing.Point(6, 72);
            this.btnSetOriginPosition.Name = "btnSetOriginPosition";
            this.btnSetOriginPosition.Size = new System.Drawing.Size(156, 30);
            this.btnSetOriginPosition.TabIndex = 15;
            this.btnSetOriginPosition.Text = "현재 위치를 원점 위치로";
            this.btnSetOriginPosition.UseVisualStyleBackColor = true;
            this.btnSetOriginPosition.Click += new System.EventHandler(this.btnSetOriginPosition_Click_1);
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(6, 27);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(78, 30);
            this.btnHome.TabIndex = 14;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // lblCurHomeStep
            // 
            this.lblCurHomeStep.AutoSize = true;
            this.lblCurHomeStep.Location = new System.Drawing.Point(107, 152);
            this.lblCurHomeStep.Name = "lblCurHomeStep";
            this.lblCurHomeStep.Size = new System.Drawing.Size(11, 12);
            this.lblCurHomeStep.TabIndex = 12;
            this.lblCurHomeStep.Text = "0";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(21, 152);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(79, 12);
            this.label21.TabIndex = 13;
            this.label21.Text = "Home Step : ";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnAlignLogicStop);
            this.groupBox7.Controls.Add(this.btnRecvPioSigOff);
            this.groupBox7.Controls.Add(this.btnSendPioSigOff);
            this.groupBox7.Controls.Add(this.btnWaferDetectOff);
            this.groupBox7.Controls.Add(this.btnWaferDetectOn);
            this.groupBox7.Controls.Add(this.btnRecvSigOn);
            this.groupBox7.Controls.Add(this.btnSendPioSigOn);
            this.groupBox7.Controls.Add(this.btnSeqLogicStart);
            this.groupBox7.Controls.Add(this.btnAlignLogicHome);
            this.groupBox7.Location = new System.Drawing.Point(549, 12);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(251, 426);
            this.groupBox7.TabIndex = 11;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Align Logic";
            // 
            // btnAlignLogicStop
            // 
            this.btnAlignLogicStop.Location = new System.Drawing.Point(6, 367);
            this.btnAlignLogicStop.Name = "btnAlignLogicStop";
            this.btnAlignLogicStop.Size = new System.Drawing.Size(139, 30);
            this.btnAlignLogicStop.TabIndex = 13;
            this.btnAlignLogicStop.Text = "Auto Seq Stop";
            this.btnAlignLogicStop.UseVisualStyleBackColor = true;
            this.btnAlignLogicStop.Click += new System.EventHandler(this.btnAlignLogicStop_Click);
            // 
            // btnRecvPioSigOff
            // 
            this.btnRecvPioSigOff.Location = new System.Drawing.Point(6, 310);
            this.btnRecvPioSigOff.Name = "btnRecvPioSigOff";
            this.btnRecvPioSigOff.Size = new System.Drawing.Size(139, 30);
            this.btnRecvPioSigOff.TabIndex = 13;
            this.btnRecvPioSigOff.Text = "Pio Recv Signal Off";
            this.btnRecvPioSigOff.UseVisualStyleBackColor = true;
            this.btnRecvPioSigOff.Click += new System.EventHandler(this.btnRecvPioSigOff_Click);
            // 
            // btnSendPioSigOff
            // 
            this.btnSendPioSigOff.Location = new System.Drawing.Point(6, 176);
            this.btnSendPioSigOff.Name = "btnSendPioSigOff";
            this.btnSendPioSigOff.Size = new System.Drawing.Size(139, 30);
            this.btnSendPioSigOff.TabIndex = 13;
            this.btnSendPioSigOff.Text = "Pio Send Signal Off";
            this.btnSendPioSigOff.UseVisualStyleBackColor = true;
            this.btnSendPioSigOff.Click += new System.EventHandler(this.btnSendPioSigOff_Click);
            // 
            // btnWaferDetectOff
            // 
            this.btnWaferDetectOff.Location = new System.Drawing.Point(6, 274);
            this.btnWaferDetectOff.Name = "btnWaferDetectOff";
            this.btnWaferDetectOff.Size = new System.Drawing.Size(139, 30);
            this.btnWaferDetectOff.TabIndex = 13;
            this.btnWaferDetectOff.Text = "Wafer Detect Off";
            this.btnWaferDetectOff.UseVisualStyleBackColor = true;
            this.btnWaferDetectOff.Click += new System.EventHandler(this.btnWaferDetectOff_Click);
            // 
            // btnWaferDetectOn
            // 
            this.btnWaferDetectOn.Location = new System.Drawing.Point(6, 140);
            this.btnWaferDetectOn.Name = "btnWaferDetectOn";
            this.btnWaferDetectOn.Size = new System.Drawing.Size(139, 30);
            this.btnWaferDetectOn.TabIndex = 13;
            this.btnWaferDetectOn.Text = "Wafer Detect On";
            this.btnWaferDetectOn.UseVisualStyleBackColor = true;
            this.btnWaferDetectOn.Click += new System.EventHandler(this.btnWaferDetectOn_Click);
            // 
            // btnRecvSigOn
            // 
            this.btnRecvSigOn.Location = new System.Drawing.Point(6, 240);
            this.btnRecvSigOn.Name = "btnRecvSigOn";
            this.btnRecvSigOn.Size = new System.Drawing.Size(139, 30);
            this.btnRecvSigOn.TabIndex = 13;
            this.btnRecvSigOn.Text = "Pio Recv Signal On";
            this.btnRecvSigOn.UseVisualStyleBackColor = true;
            this.btnRecvSigOn.Click += new System.EventHandler(this.btnRecvSigOn_Click);
            // 
            // btnSendPioSigOn
            // 
            this.btnSendPioSigOn.Location = new System.Drawing.Point(6, 106);
            this.btnSendPioSigOn.Name = "btnSendPioSigOn";
            this.btnSendPioSigOn.Size = new System.Drawing.Size(139, 30);
            this.btnSendPioSigOn.TabIndex = 13;
            this.btnSendPioSigOn.Text = "Pio Send Signal On";
            this.btnSendPioSigOn.UseVisualStyleBackColor = true;
            this.btnSendPioSigOn.Click += new System.EventHandler(this.btnSendPioSigOn_Click);
            // 
            // btnAlignLogicHome
            // 
            this.btnAlignLogicHome.Location = new System.Drawing.Point(6, 26);
            this.btnAlignLogicHome.Name = "btnAlignLogicHome";
            this.btnAlignLogicHome.Size = new System.Drawing.Size(139, 30);
            this.btnAlignLogicHome.TabIndex = 13;
            this.btnAlignLogicHome.Text = "Home Start";
            this.btnAlignLogicHome.UseVisualStyleBackColor = true;
            this.btnAlignLogicHome.Click += new System.EventHandler(this.btnAlignLogicStart_Click);
            // 
            // btnSeqLogicStart
            // 
            this.btnSeqLogicStart.Location = new System.Drawing.Point(6, 62);
            this.btnSeqLogicStart.Name = "btnSeqLogicStart";
            this.btnSeqLogicStart.Size = new System.Drawing.Size(139, 30);
            this.btnSeqLogicStart.TabIndex = 13;
            this.btnSeqLogicStart.Text = "Auto Seq Start";
            this.btnSeqLogicStart.UseVisualStyleBackColor = true;
            this.btnSeqLogicStart.Click += new System.EventHandler(this.btnSeqLogicStart_Click);
            // 
            // FrmEziStepMotorTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnMotorStop);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCurStepMotorName);
            this.Controls.Add(this.label3);
            this.Name = "FrmEziStepMotorTester";
            this.Text = "FrmEziStepMotorTester";
            this.Load += new System.EventHandler(this.FrmEziStepMotorTester_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxPortNo;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.ComboBox comboBaudrate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonJogNegative;
        private System.Windows.Forms.Button buttonJogPositive;
        private System.Windows.Forms.TextBox textBoxJogSpd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer tmrUiUpdate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbPtpAcc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbPtpSpeed;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbPtpPosi;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCurPtpStep;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnPtpMove;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblStatusMoving;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblStatusError;
        private System.Windows.Forms.Label lblStatusHomeComp;
        private System.Windows.Forms.Label lblStatusMinusLimit;
        private System.Windows.Forms.Label lblStatusPlusLimit;
        private System.Windows.Forms.Label lblStatusSvOn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbCurSpeed;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbCurPosition;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.RadioButton rbSalveNo0;
        private System.Windows.Forms.Button btnMotorStop;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbSalveNo2;
        private System.Windows.Forms.RadioButton rbSalveNo1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCurStepMotorName;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnSetOriginPosition;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Label lblCurHomeStep;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnAlignLogicStop;
        private System.Windows.Forms.Button btnSendPioSigOff;
        private System.Windows.Forms.Button btnSendPioSigOn;
        private System.Windows.Forms.Button btnAlignLogicHome;
        private System.Windows.Forms.Button btnRecvPioSigOff;
        private System.Windows.Forms.Button btnWaferDetectOff;
        private System.Windows.Forms.Button btnWaferDetectOn;
        private System.Windows.Forms.Button btnRecvSigOn;
        private System.Windows.Forms.Button btnSeqLogicStart;
    }
}