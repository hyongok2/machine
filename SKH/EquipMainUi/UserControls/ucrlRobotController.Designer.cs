namespace EquipMainUi.UserControls
{
    partial class ucrlRobotController
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
            this.btnRobotStop2 = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbLoad1 = new System.Windows.Forms.RadioButton();
            this.tbSlot = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.rbLoad2 = new System.Windows.Forms.RadioButton();
            this.rbAVI = new System.Windows.Forms.RadioButton();
            this.rbAligner = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbPlace = new System.Windows.Forms.RadioButton();
            this.rbPick = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbLower = new System.Windows.Forms.RadioButton();
            this.rbUpper = new System.Windows.Forms.RadioButton();
            this.btnWaiting = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnTransfer = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnInvisible = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnInvisible);
            this.groupBox1.Controls.Add(this.btnRobotStop2);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnWaiting);
            this.groupBox1.Controls.Add(this.btnTransfer);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(334, 240);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "■ Transfer  / Wait Position";
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
            this.btnRobotStop2.Location = new System.Drawing.Point(10, 126);
            this.btnRobotStop2.Name = "btnRobotStop2";
            this.btnRobotStop2.OnOff = false;
            this.btnRobotStop2.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnRobotStop2.Size = new System.Drawing.Size(144, 61);
            this.btnRobotStop2.TabIndex = 1;
            this.btnRobotStop2.Text = "STOP";
            this.btnRobotStop2.Text2 = "";
            this.btnRobotStop2.UseVisualStyleBackColor = false;
            this.btnRobotStop2.VisibleLeftLamp = false;
            this.btnRobotStop2.VisibleRightLamp = false;
            this.btnRobotStop2.Click += new System.EventHandler(this.btnRobotStop_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbLoad1);
            this.groupBox4.Controls.Add(this.tbSlot);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.rbLoad2);
            this.groupBox4.Controls.Add(this.rbAVI);
            this.groupBox4.Controls.Add(this.rbAligner);
            this.groupBox4.Location = new System.Drawing.Point(186, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(144, 167);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Port";
            // 
            // rbLoad1
            // 
            this.rbLoad1.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbLoad1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbLoad1.Location = new System.Drawing.Point(72, 93);
            this.rbLoad1.Name = "rbLoad1";
            this.rbLoad1.Size = new System.Drawing.Size(70, 36);
            this.rbLoad1.TabIndex = 0;
            this.rbLoad1.TabStop = true;
            this.rbLoad1.Text = "LOAD1";
            this.rbLoad1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbLoad1.UseVisualStyleBackColor = true;
            this.rbLoad1.CheckedChanged += new System.EventHandler(this.rbAligner_CheckedChanged);
            // 
            // tbSlot
            // 
            this.tbSlot.Location = new System.Drawing.Point(72, 135);
            this.tbSlot.Name = "tbSlot";
            this.tbSlot.Size = new System.Drawing.Size(70, 21);
            this.tbSlot.TabIndex = 5;
            this.tbSlot.Text = "0";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(43, 140);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(26, 12);
            this.label25.TabIndex = 4;
            this.label25.Text = "Slot";
            // 
            // rbLoad2
            // 
            this.rbLoad2.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbLoad2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbLoad2.Location = new System.Drawing.Point(72, 56);
            this.rbLoad2.Name = "rbLoad2";
            this.rbLoad2.Size = new System.Drawing.Size(70, 36);
            this.rbLoad2.TabIndex = 0;
            this.rbLoad2.TabStop = true;
            this.rbLoad2.Text = "LOAD2";
            this.rbLoad2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbLoad2.UseVisualStyleBackColor = true;
            this.rbLoad2.CheckedChanged += new System.EventHandler(this.rbAligner_CheckedChanged);
            // 
            // rbAVI
            // 
            this.rbAVI.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAVI.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbAVI.Location = new System.Drawing.Point(2, 56);
            this.rbAVI.Name = "rbAVI";
            this.rbAVI.Size = new System.Drawing.Size(70, 36);
            this.rbAVI.TabIndex = 0;
            this.rbAVI.TabStop = true;
            this.rbAVI.Text = "AVI";
            this.rbAVI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbAVI.UseVisualStyleBackColor = true;
            this.rbAVI.CheckedChanged += new System.EventHandler(this.rbAligner_CheckedChanged);
            // 
            // rbAligner
            // 
            this.rbAligner.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbAligner.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbAligner.Location = new System.Drawing.Point(36, 20);
            this.rbAligner.Name = "rbAligner";
            this.rbAligner.Size = new System.Drawing.Size(70, 36);
            this.rbAligner.TabIndex = 0;
            this.rbAligner.TabStop = true;
            this.rbAligner.Text = "Aligner";
            this.rbAligner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbAligner.UseVisualStyleBackColor = true;
            this.rbAligner.CheckedChanged += new System.EventHandler(this.rbAligner_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbPlace);
            this.groupBox5.Controls.Add(this.rbPick);
            this.groupBox5.Location = new System.Drawing.Point(98, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(82, 100);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "How";
            // 
            // rbPlace
            // 
            this.rbPlace.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbPlace.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbPlace.Location = new System.Drawing.Point(6, 56);
            this.rbPlace.Name = "rbPlace";
            this.rbPlace.Size = new System.Drawing.Size(70, 36);
            this.rbPlace.TabIndex = 0;
            this.rbPlace.TabStop = true;
            this.rbPlace.Text = "Place";
            this.rbPlace.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbPlace.UseVisualStyleBackColor = true;
            // 
            // rbPick
            // 
            this.rbPick.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbPick.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbPick.Location = new System.Drawing.Point(6, 20);
            this.rbPick.Name = "rbPick";
            this.rbPick.Size = new System.Drawing.Size(70, 36);
            this.rbPick.TabIndex = 0;
            this.rbPick.TabStop = true;
            this.rbPick.Text = "Pick";
            this.rbPick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbPick.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbLower);
            this.groupBox2.Controls.Add(this.rbUpper);
            this.groupBox2.Location = new System.Drawing.Point(10, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(82, 102);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Robot Arm";
            // 
            // rbLower
            // 
            this.rbLower.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbLower.BackColor = System.Drawing.Color.Transparent;
            this.rbLower.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbLower.Location = new System.Drawing.Point(6, 56);
            this.rbLower.Name = "rbLower";
            this.rbLower.Size = new System.Drawing.Size(70, 36);
            this.rbLower.TabIndex = 0;
            this.rbLower.Text = "Lower";
            this.rbLower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbLower.UseVisualStyleBackColor = false;
            // 
            // rbUpper
            // 
            this.rbUpper.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbUpper.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.rbUpper.Location = new System.Drawing.Point(6, 20);
            this.rbUpper.Name = "rbUpper";
            this.rbUpper.Size = new System.Drawing.Size(70, 36);
            this.rbUpper.TabIndex = 0;
            this.rbUpper.Text = "Upper";
            this.rbUpper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbUpper.UseVisualStyleBackColor = true;
            // 
            // btnWaiting
            // 
            this.btnWaiting.BackColor = System.Drawing.Color.Transparent;
            this.btnWaiting.Delay = 100;
            this.btnWaiting.Flicker = false;
            this.btnWaiting.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnWaiting.IsLeftLampOn = false;
            this.btnWaiting.IsRightLampOn = false;
            this.btnWaiting.LampAliveTime = 500;
            this.btnWaiting.LampSize = 3;
            this.btnWaiting.LeftLampColor = System.Drawing.Color.Red;
            this.btnWaiting.Location = new System.Drawing.Point(10, 193);
            this.btnWaiting.Name = "btnWaiting";
            this.btnWaiting.OnOff = false;
            this.btnWaiting.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnWaiting.Size = new System.Drawing.Size(144, 44);
            this.btnWaiting.TabIndex = 0;
            this.btnWaiting.Text = "WAITR";
            this.btnWaiting.Text2 = "";
            this.btnWaiting.UseVisualStyleBackColor = true;
            this.btnWaiting.VisibleLeftLamp = false;
            this.btnWaiting.VisibleRightLamp = false;
            this.btnWaiting.Click += new System.EventHandler(this.btnWaiting_Click);
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
            this.btnTransfer.Location = new System.Drawing.Point(186, 193);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.OnOff = false;
            this.btnTransfer.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnTransfer.Size = new System.Drawing.Size(144, 44);
            this.btnTransfer.TabIndex = 0;
            this.btnTransfer.Text = "TRANS";
            this.btnTransfer.Text2 = "";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.VisibleLeftLamp = false;
            this.btnTransfer.VisibleRightLamp = false;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // btnInvisible
            // 
            this.btnInvisible.BackColor = System.Drawing.Color.Transparent;
            this.btnInvisible.Delay = 100;
            this.btnInvisible.Flicker = false;
            this.btnInvisible.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnInvisible.IsLeftLampOn = false;
            this.btnInvisible.IsRightLampOn = false;
            this.btnInvisible.LampAliveTime = 500;
            this.btnInvisible.LampSize = 3;
            this.btnInvisible.LeftLampColor = System.Drawing.Color.Red;
            this.btnInvisible.Location = new System.Drawing.Point(297, 2);
            this.btnInvisible.Name = "btnInvisible";
            this.btnInvisible.OnOff = false;
            this.btnInvisible.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnInvisible.Size = new System.Drawing.Size(36, 33);
            this.btnInvisible.TabIndex = 9;
            this.btnInvisible.Text = "X";
            this.btnInvisible.Text2 = "";
            this.btnInvisible.UseVisualStyleBackColor = false;
            this.btnInvisible.VisibleLeftLamp = false;
            this.btnInvisible.VisibleRightLamp = false;
            this.btnInvisible.Click += new System.EventHandler(this.btnInvisible_Click);
            // 
            // ucrlRobotController
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucrlRobotController";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(340, 246);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnRobotStop2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbLoad1;
        private System.Windows.Forms.TextBox tbSlot;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.RadioButton rbLoad2;
        private System.Windows.Forms.RadioButton rbAVI;
        private System.Windows.Forms.RadioButton rbAligner;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbPlace;
        private System.Windows.Forms.RadioButton rbPick;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbLower;
        private System.Windows.Forms.RadioButton rbUpper;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnWaiting;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnTransfer;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnInvisible;
    }
}
