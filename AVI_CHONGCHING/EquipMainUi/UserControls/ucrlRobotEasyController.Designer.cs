namespace EquipMainUi.UserControls
{
    partial class ucrlRobotEasyController
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbTransfer = new System.Windows.Forms.RadioButton();
            this.rbWait = new System.Windows.Forms.RadioButton();
            this.lblPlace = new Dit.Framework.UI.UserComponent.LabelFlicker();
            this.lblPick = new Dit.Framework.UI.UserComponent.LabelFlicker();
            this.btnRefresh = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnRobotStop2 = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.gbTo = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbToLoad1 = new System.Windows.Forms.RadioButton();
            this.rbToLower = new System.Windows.Forms.RadioButton();
            this.rbToUpper = new System.Windows.Forms.RadioButton();
            this.rbToAligner = new System.Windows.Forms.RadioButton();
            this.rbToLoad2 = new System.Windows.Forms.RadioButton();
            this.rbToAVI = new System.Windows.Forms.RadioButton();
            this.tbToSlot = new System.Windows.Forms.TextBox();
            this.gbFrom = new System.Windows.Forms.GroupBox();
            this.tbFromSlot = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.rbFromLoad1 = new System.Windows.Forms.RadioButton();
            this.rbFromAligner = new System.Windows.Forms.RadioButton();
            this.rbFromLoad2 = new System.Windows.Forms.RadioButton();
            this.rbFromAVI = new System.Windows.Forms.RadioButton();
            this.rbFromLower = new System.Windows.Forms.RadioButton();
            this.rbFromUpper = new System.Windows.Forms.RadioButton();
            this.btnTransfer = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.groupBox1.SuspendLayout();
            this.gbTo.SuspendLayout();
            this.gbFrom.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbTransfer);
            this.groupBox1.Controls.Add(this.rbWait);
            this.groupBox1.Controls.Add(this.lblPlace);
            this.groupBox1.Controls.Add(this.lblPick);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.btnRobotStop2);
            this.groupBox1.Controls.Add(this.gbTo);
            this.groupBox1.Controls.Add(this.gbFrom);
            this.groupBox1.Controls.Add(this.btnTransfer);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(334, 240);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // rbTransfer
            // 
            this.rbTransfer.AutoSize = true;
            this.rbTransfer.Checked = true;
            this.rbTransfer.Location = new System.Drawing.Point(0, 0);
            this.rbTransfer.Name = "rbTransfer";
            this.rbTransfer.Size = new System.Drawing.Size(70, 16);
            this.rbTransfer.TabIndex = 70;
            this.rbTransfer.TabStop = true;
            this.rbTransfer.Text = "Transfer";
            this.rbTransfer.UseVisualStyleBackColor = true;
            this.rbTransfer.CheckedChanged += new System.EventHandler(this.rbTransfer_CheckedChanged);
            // 
            // rbWait
            // 
            this.rbWait.AutoSize = true;
            this.rbWait.Location = new System.Drawing.Point(76, 0);
            this.rbWait.Name = "rbWait";
            this.rbWait.Size = new System.Drawing.Size(46, 16);
            this.rbWait.TabIndex = 70;
            this.rbWait.Text = "Wait";
            this.rbWait.UseVisualStyleBackColor = true;
            this.rbWait.CheckedChanged += new System.EventHandler(this.rbTransfer_CheckedChanged);
            // 
            // lblPlace
            // 
            this.lblPlace.BackColor = System.Drawing.Color.White;
            this.lblPlace.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPlace.Delay = 2;
            this.lblPlace.Flicker = false;
            this.lblPlace.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlace.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblPlace.Location = new System.Drawing.Point(312, 103);
            this.lblPlace.Name = "lblPlace";
            this.lblPlace.OffColor = System.Drawing.Color.White;
            this.lblPlace.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblPlace.OnOff = false;
            this.lblPlace.Size = new System.Drawing.Size(19, 83);
            this.lblPlace.TabIndex = 69;
            this.lblPlace.Text = "PLACE";
            this.lblPlace.Text2 = "";
            this.lblPlace.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPick
            // 
            this.lblPick.BackColor = System.Drawing.Color.White;
            this.lblPick.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPick.Delay = 2;
            this.lblPick.Flicker = false;
            this.lblPick.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPick.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblPick.Location = new System.Drawing.Point(312, 36);
            this.lblPick.Name = "lblPick";
            this.lblPick.OffColor = System.Drawing.Color.White;
            this.lblPick.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblPick.OnOff = false;
            this.lblPick.Size = new System.Drawing.Size(19, 63);
            this.lblPick.TabIndex = 69;
            this.lblPick.Text = "P\r\nI\r\nC\r\nK";
            this.lblPick.Text2 = "";
            this.lblPick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundImage = global::EquipMainUi.Properties.Resources.refreshing_pngrepo_com;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.Delay = 100;
            this.btnRefresh.Flicker = false;
            this.btnRefresh.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRefresh.IsLeftLampOn = false;
            this.btnRefresh.IsRightLampOn = false;
            this.btnRefresh.LampAliveTime = 500;
            this.btnRefresh.LampSize = 3;
            this.btnRefresh.LeftLampColor = System.Drawing.Color.Red;
            this.btnRefresh.Location = new System.Drawing.Point(304, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.OnOff = false;
            this.btnRefresh.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnRefresh.Size = new System.Drawing.Size(30, 30);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text2 = "";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.VisibleLeftLamp = false;
            this.btnRefresh.VisibleRightLamp = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnRobotStop2
            // 
            this.btnRobotStop2.BackColor = System.Drawing.Color.Transparent;
            this.btnRobotStop2.Delay = 100;
            this.btnRobotStop2.Flicker = false;
            this.btnRobotStop2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRobotStop2.IsLeftLampOn = false;
            this.btnRobotStop2.IsRightLampOn = false;
            this.btnRobotStop2.LampAliveTime = 500;
            this.btnRobotStop2.LampSize = 3;
            this.btnRobotStop2.LeftLampColor = System.Drawing.Color.Red;
            this.btnRobotStop2.Location = new System.Drawing.Point(3, 186);
            this.btnRobotStop2.Name = "btnRobotStop2";
            this.btnRobotStop2.OnOff = false;
            this.btnRobotStop2.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnRobotStop2.Size = new System.Drawing.Size(151, 51);
            this.btnRobotStop2.TabIndex = 1;
            this.btnRobotStop2.Text = "STOP";
            this.btnRobotStop2.Text2 = "";
            this.btnRobotStop2.UseVisualStyleBackColor = false;
            this.btnRobotStop2.VisibleLeftLamp = false;
            this.btnRobotStop2.VisibleRightLamp = false;
            this.btnRobotStop2.Click += new System.EventHandler(this.btnRobotStop_Click);
            // 
            // gbTo
            // 
            this.gbTo.Controls.Add(this.label1);
            this.gbTo.Controls.Add(this.rbToLoad1);
            this.gbTo.Controls.Add(this.rbToLower);
            this.gbTo.Controls.Add(this.rbToUpper);
            this.gbTo.Controls.Add(this.rbToAligner);
            this.gbTo.Controls.Add(this.rbToLoad2);
            this.gbTo.Controls.Add(this.rbToAVI);
            this.gbTo.Controls.Add(this.tbToSlot);
            this.gbTo.Enabled = false;
            this.gbTo.Location = new System.Drawing.Point(160, 21);
            this.gbTo.Name = "gbTo";
            this.gbTo.Size = new System.Drawing.Size(151, 165);
            this.gbTo.TabIndex = 7;
            this.gbTo.TabStop = false;
            this.gbTo.Text = "To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Slot";
            // 
            // rbToLoad1
            // 
            this.rbToLoad1.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbToLoad1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbToLoad1.Location = new System.Drawing.Point(76, 107);
            this.rbToLoad1.Name = "rbToLoad1";
            this.rbToLoad1.Size = new System.Drawing.Size(70, 30);
            this.rbToLoad1.TabIndex = 0;
            this.rbToLoad1.TabStop = true;
            this.rbToLoad1.Text = "LOAD1";
            this.rbToLoad1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbToLoad1.UseVisualStyleBackColor = true;
            this.rbToLoad1.CheckedChanged += new System.EventHandler(this.to_CheckedChanged);
            // 
            // rbToLower
            // 
            this.rbToLower.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbToLower.BackColor = System.Drawing.Color.Transparent;
            this.rbToLower.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbToLower.Location = new System.Drawing.Point(76, 15);
            this.rbToLower.Name = "rbToLower";
            this.rbToLower.Size = new System.Drawing.Size(70, 30);
            this.rbToLower.TabIndex = 0;
            this.rbToLower.Text = "Lower";
            this.rbToLower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbToLower.UseVisualStyleBackColor = false;
            this.rbToLower.CheckedChanged += new System.EventHandler(this.to_CheckedChanged);
            // 
            // rbToUpper
            // 
            this.rbToUpper.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbToUpper.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbToUpper.Location = new System.Drawing.Point(6, 15);
            this.rbToUpper.Name = "rbToUpper";
            this.rbToUpper.Size = new System.Drawing.Size(70, 30);
            this.rbToUpper.TabIndex = 0;
            this.rbToUpper.Text = "Upper";
            this.rbToUpper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbToUpper.UseVisualStyleBackColor = true;
            this.rbToUpper.CheckedChanged += new System.EventHandler(this.to_CheckedChanged);
            // 
            // rbToAligner
            // 
            this.rbToAligner.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbToAligner.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbToAligner.Location = new System.Drawing.Point(40, 47);
            this.rbToAligner.Name = "rbToAligner";
            this.rbToAligner.Size = new System.Drawing.Size(70, 30);
            this.rbToAligner.TabIndex = 0;
            this.rbToAligner.TabStop = true;
            this.rbToAligner.Text = "Aligner";
            this.rbToAligner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbToAligner.UseVisualStyleBackColor = true;
            this.rbToAligner.CheckedChanged += new System.EventHandler(this.to_CheckedChanged);
            // 
            // rbToLoad2
            // 
            this.rbToLoad2.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbToLoad2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbToLoad2.Location = new System.Drawing.Point(76, 77);
            this.rbToLoad2.Name = "rbToLoad2";
            this.rbToLoad2.Size = new System.Drawing.Size(70, 30);
            this.rbToLoad2.TabIndex = 0;
            this.rbToLoad2.TabStop = true;
            this.rbToLoad2.Text = "LOAD2";
            this.rbToLoad2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbToLoad2.UseVisualStyleBackColor = true;
            this.rbToLoad2.CheckedChanged += new System.EventHandler(this.to_CheckedChanged);
            // 
            // rbToAVI
            // 
            this.rbToAVI.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbToAVI.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbToAVI.Location = new System.Drawing.Point(6, 77);
            this.rbToAVI.Name = "rbToAVI";
            this.rbToAVI.Size = new System.Drawing.Size(70, 30);
            this.rbToAVI.TabIndex = 0;
            this.rbToAVI.TabStop = true;
            this.rbToAVI.Text = "AVI";
            this.rbToAVI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbToAVI.UseVisualStyleBackColor = true;
            this.rbToAVI.CheckedChanged += new System.EventHandler(this.to_CheckedChanged);
            // 
            // tbToSlot
            // 
            this.tbToSlot.Location = new System.Drawing.Point(76, 138);
            this.tbToSlot.Name = "tbToSlot";
            this.tbToSlot.Size = new System.Drawing.Size(69, 21);
            this.tbToSlot.TabIndex = 5;
            this.tbToSlot.Text = "0";
            // 
            // gbFrom
            // 
            this.gbFrom.Controls.Add(this.tbFromSlot);
            this.gbFrom.Controls.Add(this.label25);
            this.gbFrom.Controls.Add(this.rbFromLoad1);
            this.gbFrom.Controls.Add(this.rbFromAligner);
            this.gbFrom.Controls.Add(this.rbFromLoad2);
            this.gbFrom.Controls.Add(this.rbFromAVI);
            this.gbFrom.Controls.Add(this.rbFromLower);
            this.gbFrom.Controls.Add(this.rbFromUpper);
            this.gbFrom.Location = new System.Drawing.Point(3, 21);
            this.gbFrom.Name = "gbFrom";
            this.gbFrom.Size = new System.Drawing.Size(151, 165);
            this.gbFrom.TabIndex = 7;
            this.gbFrom.TabStop = false;
            this.gbFrom.Text = "From";
            // 
            // tbFromSlot
            // 
            this.tbFromSlot.Location = new System.Drawing.Point(76, 138);
            this.tbFromSlot.Name = "tbFromSlot";
            this.tbFromSlot.Size = new System.Drawing.Size(69, 21);
            this.tbFromSlot.TabIndex = 5;
            this.tbFromSlot.Text = "0";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(44, 141);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(26, 12);
            this.label25.TabIndex = 4;
            this.label25.Text = "Slot";
            // 
            // rbFromLoad1
            // 
            this.rbFromLoad1.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbFromLoad1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbFromLoad1.Location = new System.Drawing.Point(76, 107);
            this.rbFromLoad1.Name = "rbFromLoad1";
            this.rbFromLoad1.Size = new System.Drawing.Size(70, 30);
            this.rbFromLoad1.TabIndex = 0;
            this.rbFromLoad1.TabStop = true;
            this.rbFromLoad1.Text = "LOAD1";
            this.rbFromLoad1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbFromLoad1.UseVisualStyleBackColor = true;
            this.rbFromLoad1.CheckedChanged += new System.EventHandler(this.from_CheckedChanged);
            // 
            // rbFromAligner
            // 
            this.rbFromAligner.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbFromAligner.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbFromAligner.Location = new System.Drawing.Point(40, 47);
            this.rbFromAligner.Name = "rbFromAligner";
            this.rbFromAligner.Size = new System.Drawing.Size(70, 30);
            this.rbFromAligner.TabIndex = 0;
            this.rbFromAligner.TabStop = true;
            this.rbFromAligner.Text = "Aligner";
            this.rbFromAligner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbFromAligner.UseVisualStyleBackColor = true;
            this.rbFromAligner.CheckedChanged += new System.EventHandler(this.from_CheckedChanged);
            // 
            // rbFromLoad2
            // 
            this.rbFromLoad2.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbFromLoad2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbFromLoad2.Location = new System.Drawing.Point(76, 77);
            this.rbFromLoad2.Name = "rbFromLoad2";
            this.rbFromLoad2.Size = new System.Drawing.Size(70, 30);
            this.rbFromLoad2.TabIndex = 0;
            this.rbFromLoad2.TabStop = true;
            this.rbFromLoad2.Text = "LOAD2";
            this.rbFromLoad2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbFromLoad2.UseVisualStyleBackColor = true;
            this.rbFromLoad2.CheckedChanged += new System.EventHandler(this.from_CheckedChanged);
            // 
            // rbFromAVI
            // 
            this.rbFromAVI.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbFromAVI.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbFromAVI.Location = new System.Drawing.Point(6, 77);
            this.rbFromAVI.Name = "rbFromAVI";
            this.rbFromAVI.Size = new System.Drawing.Size(70, 30);
            this.rbFromAVI.TabIndex = 0;
            this.rbFromAVI.TabStop = true;
            this.rbFromAVI.Text = "AVI";
            this.rbFromAVI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbFromAVI.UseVisualStyleBackColor = true;
            this.rbFromAVI.CheckedChanged += new System.EventHandler(this.from_CheckedChanged);
            // 
            // rbFromLower
            // 
            this.rbFromLower.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbFromLower.BackColor = System.Drawing.Color.Transparent;
            this.rbFromLower.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbFromLower.Location = new System.Drawing.Point(76, 15);
            this.rbFromLower.Name = "rbFromLower";
            this.rbFromLower.Size = new System.Drawing.Size(70, 30);
            this.rbFromLower.TabIndex = 0;
            this.rbFromLower.Text = "Lower";
            this.rbFromLower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbFromLower.UseVisualStyleBackColor = false;
            this.rbFromLower.CheckedChanged += new System.EventHandler(this.from_CheckedChanged);
            // 
            // rbFromUpper
            // 
            this.rbFromUpper.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbFromUpper.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbFromUpper.Location = new System.Drawing.Point(6, 15);
            this.rbFromUpper.Name = "rbFromUpper";
            this.rbFromUpper.Size = new System.Drawing.Size(70, 30);
            this.rbFromUpper.TabIndex = 0;
            this.rbFromUpper.Text = "Upper";
            this.rbFromUpper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbFromUpper.UseVisualStyleBackColor = true;
            this.rbFromUpper.CheckedChanged += new System.EventHandler(this.from_CheckedChanged);
            // 
            // btnTransfer
            // 
            this.btnTransfer.BackColor = System.Drawing.Color.Transparent;
            this.btnTransfer.Delay = 100;
            this.btnTransfer.Flicker = false;
            this.btnTransfer.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnTransfer.IsLeftLampOn = false;
            this.btnTransfer.IsRightLampOn = false;
            this.btnTransfer.LampAliveTime = 500;
            this.btnTransfer.LampSize = 3;
            this.btnTransfer.LeftLampColor = System.Drawing.Color.Red;
            this.btnTransfer.Location = new System.Drawing.Point(160, 188);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.OnOff = false;
            this.btnTransfer.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnTransfer.Size = new System.Drawing.Size(171, 49);
            this.btnTransfer.TabIndex = 0;
            this.btnTransfer.Text = "TRANSFER";
            this.btnTransfer.Text2 = "";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.VisibleLeftLamp = false;
            this.btnTransfer.VisibleRightLamp = false;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // ucrlRobotEasyController
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucrlRobotEasyController";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(340, 246);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbTo.ResumeLayout(false);
            this.gbTo.PerformLayout();
            this.gbFrom.ResumeLayout(false);
            this.gbFrom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnRobotStop2;
        private System.Windows.Forms.RadioButton rbFromLoad1;
        private System.Windows.Forms.TextBox tbFromSlot;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.RadioButton rbFromLoad2;
        private System.Windows.Forms.RadioButton rbFromAVI;
        private System.Windows.Forms.RadioButton rbFromAligner;
        private System.Windows.Forms.GroupBox gbFrom;
        private System.Windows.Forms.RadioButton rbFromLower;
        private System.Windows.Forms.RadioButton rbFromUpper;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnTransfer;
        private System.Windows.Forms.GroupBox gbTo;
        private System.Windows.Forms.TextBox tbToSlot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbToLoad1;
        private System.Windows.Forms.RadioButton rbToLower;
        private System.Windows.Forms.RadioButton rbToUpper;
        private System.Windows.Forms.RadioButton rbToAligner;
        private System.Windows.Forms.RadioButton rbToLoad2;
        private System.Windows.Forms.RadioButton rbToAVI;
        private Dit.Framework.UI.UserComponent.LabelFlicker lblPlace;
        private Dit.Framework.UI.UserComponent.LabelFlicker lblPick;
        private System.Windows.Forms.RadioButton rbTransfer;
        private System.Windows.Forms.RadioButton rbWait;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnRefresh;
    }
}
