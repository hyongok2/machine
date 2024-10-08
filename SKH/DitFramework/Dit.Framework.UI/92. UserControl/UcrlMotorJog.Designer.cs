using Dit.Framework.UI.UserComponent;
namespace Dit.Framework.UI.Motor
{
    partial class UcrlMotorJog
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSpeed = new System.Windows.Forms.Label();
            this.trbSpeed = new System.Windows.Forms.TrackBar();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTrackbar = new System.Windows.Forms.Panel();
            this.pnlRadioSpeed = new System.Windows.Forms.FlowLayoutPanel();
            this.radioSpeedLow = new System.Windows.Forms.RadioButton();
            this.radioSpeedMid = new System.Windows.Forms.RadioButton();
            this.radioSpeedHi = new System.Windows.Forms.RadioButton();
            this.pnlTextSpeed = new System.Windows.Forms.Panel();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.btnNegativeJog = new ButtonDelay2();
            this.btnPositiveJog = new ButtonDelay2();
            ((System.ComponentModel.ISupportInitialize)(this.trbSpeed)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.pnlTrackbar.SuspendLayout();
            this.pnlRadioSpeed.SuspendLayout();
            this.pnlTextSpeed.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoEllipsis = true;
            this.lblSpeed.BackColor = System.Drawing.Color.Silver;
            this.lblSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSpeed.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpeed.ForeColor = System.Drawing.Color.Firebrick;
            this.lblSpeed.Location = new System.Drawing.Point(0, 0);
            this.lblSpeed.Margin = new System.Windows.Forms.Padding(0);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(60, 25);
            this.lblSpeed.TabIndex = 87;
            this.lblSpeed.Text = "0.0mm/s";
            this.lblSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trbSpeed
            // 
            this.trbSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trbSpeed.Location = new System.Drawing.Point(0, 0);
            this.trbSpeed.Margin = new System.Windows.Forms.Padding(0);
            this.trbSpeed.Maximum = 100;
            this.trbSpeed.Name = "trbSpeed";
            this.trbSpeed.Size = new System.Drawing.Size(60, 45);
            this.trbSpeed.TabIndex = 86;
            this.trbSpeed.TickFrequency = 10;
            this.trbSpeed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbSpeed.Value = 1;
            this.trbSpeed.ValueChanged += new System.EventHandler(this.trbSpeed_ValueChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lblSpeed);
            this.flowLayoutPanel1.Controls.Add(this.btnNegativeJog);
            this.flowLayoutPanel1.Controls.Add(this.pnlTrackbar);
            this.flowLayoutPanel1.Controls.Add(this.btnPositiveJog);
            this.flowLayoutPanel1.Controls.Add(this.pnlRadioSpeed);
            this.flowLayoutPanel1.Controls.Add(this.pnlTextSpeed);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(264, 57);
            this.flowLayoutPanel1.TabIndex = 88;
            // 
            // pnlTrackbar
            // 
            this.pnlTrackbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTrackbar.Controls.Add(this.trbSpeed);
            this.pnlTrackbar.Location = new System.Drawing.Point(120, 0);
            this.pnlTrackbar.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTrackbar.Name = "pnlTrackbar";
            this.pnlTrackbar.Size = new System.Drawing.Size(60, 25);
            this.pnlTrackbar.TabIndex = 88;
            // 
            // pnlRadioSpeed
            // 
            this.pnlRadioSpeed.Controls.Add(this.radioSpeedLow);
            this.pnlRadioSpeed.Controls.Add(this.radioSpeedMid);
            this.pnlRadioSpeed.Controls.Add(this.radioSpeedHi);
            this.pnlRadioSpeed.Location = new System.Drawing.Point(0, 25);
            this.pnlRadioSpeed.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRadioSpeed.Name = "pnlRadioSpeed";
            this.pnlRadioSpeed.Size = new System.Drawing.Size(60, 20);
            this.pnlRadioSpeed.TabIndex = 89;
            // 
            // radioSpeedLow
            // 
            this.radioSpeedLow.AutoSize = true;
            this.radioSpeedLow.BackgroundImage = global::Dit.Framework.UI.Properties.Resources.level1;
            this.radioSpeedLow.Location = new System.Drawing.Point(1, 1);
            this.radioSpeedLow.Margin = new System.Windows.Forms.Padding(1);
            this.radioSpeedLow.Name = "radioSpeedLow";
            this.radioSpeedLow.Size = new System.Drawing.Size(14, 13);
            this.radioSpeedLow.TabIndex = 0;
            this.radioSpeedLow.TabStop = true;
            this.radioSpeedLow.UseVisualStyleBackColor = true;
            this.radioSpeedLow.CheckedChanged += new System.EventHandler(this.radioSpeedLow_CheckedChanged);
            // 
            // radioSpeedMid
            // 
            this.radioSpeedMid.AutoSize = true;
            this.radioSpeedMid.BackgroundImage = global::Dit.Framework.UI.Properties.Resources.level2;
            this.radioSpeedMid.Location = new System.Drawing.Point(17, 1);
            this.radioSpeedMid.Margin = new System.Windows.Forms.Padding(1);
            this.radioSpeedMid.Name = "radioSpeedMid";
            this.radioSpeedMid.Size = new System.Drawing.Size(14, 13);
            this.radioSpeedMid.TabIndex = 1;
            this.radioSpeedMid.TabStop = true;
            this.radioSpeedMid.UseVisualStyleBackColor = true;
            this.radioSpeedMid.CheckedChanged += new System.EventHandler(this.radioSpeedLow_CheckedChanged);
            // 
            // radioSpeedHi
            // 
            this.radioSpeedHi.AutoSize = true;
            this.radioSpeedHi.BackgroundImage = global::Dit.Framework.UI.Properties.Resources.level3;
            this.radioSpeedHi.Location = new System.Drawing.Point(33, 1);
            this.radioSpeedHi.Margin = new System.Windows.Forms.Padding(1);
            this.radioSpeedHi.Name = "radioSpeedHi";
            this.radioSpeedHi.Size = new System.Drawing.Size(14, 13);
            this.radioSpeedHi.TabIndex = 1;
            this.radioSpeedHi.TabStop = true;
            this.radioSpeedHi.UseVisualStyleBackColor = true;
            this.radioSpeedHi.CheckedChanged += new System.EventHandler(this.radioSpeedLow_CheckedChanged);
            // 
            // pnlTextSpeed
            // 
            this.pnlTextSpeed.Controls.Add(this.txtSpeed);
            this.pnlTextSpeed.Location = new System.Drawing.Point(60, 25);
            this.pnlTextSpeed.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTextSpeed.Name = "pnlTextSpeed";
            this.pnlTextSpeed.Size = new System.Drawing.Size(60, 20);
            this.pnlTextSpeed.TabIndex = 90;
            // 
            // txtSpeed
            // 
            this.txtSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSpeed.Location = new System.Drawing.Point(0, 0);
            this.txtSpeed.Multiline = true;
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.Size = new System.Drawing.Size(60, 20);
            this.txtSpeed.TabIndex = 0;
            this.txtSpeed.Text = "000";
            this.txtSpeed.TextChanged += new System.EventHandler(this.txtSpeed_TextChanged);
            // 
            // btnNegativeJog
            // 
            this.btnNegativeJog.BackColor = System.Drawing.Color.Transparent;
            this.btnNegativeJog.Delay = 1;
            this.btnNegativeJog.Flicker = false;
            this.btnNegativeJog.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnNegativeJog.ForeColor = System.Drawing.Color.Black;
            this.btnNegativeJog.IsLeftLampOn = false;
            this.btnNegativeJog.IsRightLampOn = false;
            this.btnNegativeJog.LampAliveTime = 500;
            this.btnNegativeJog.LampSize = 1;
            this.btnNegativeJog.LeftLampColor = System.Drawing.Color.Red;
            this.btnNegativeJog.Location = new System.Drawing.Point(60, 0);
            this.btnNegativeJog.Margin = new System.Windows.Forms.Padding(0);
            this.btnNegativeJog.Name = "btnNegativeJog";
            this.btnNegativeJog.OnOff = false;
            this.btnNegativeJog.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnNegativeJog.Size = new System.Drawing.Size(60, 25);
            this.btnNegativeJog.TabIndex = 85;
            this.btnNegativeJog.TabStop = false;
            this.btnNegativeJog.Text = "(-)◀";
            this.btnNegativeJog.Text2 = "";
            this.btnNegativeJog.UseVisualStyleBackColor = false;
            this.btnNegativeJog.VisibleLeftLamp = false;
            this.btnNegativeJog.VisibleRightLamp = false;
            // 
            // btnPositiveJog
            // 
            this.btnPositiveJog.BackColor = System.Drawing.Color.Transparent;
            this.btnPositiveJog.Delay = 1;
            this.btnPositiveJog.Flicker = false;
            this.btnPositiveJog.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPositiveJog.ForeColor = System.Drawing.Color.Black;
            this.btnPositiveJog.IsLeftLampOn = false;
            this.btnPositiveJog.IsRightLampOn = false;
            this.btnPositiveJog.LampAliveTime = 500;
            this.btnPositiveJog.LampSize = 1;
            this.btnPositiveJog.LeftLampColor = System.Drawing.Color.Red;
            this.btnPositiveJog.Location = new System.Drawing.Point(180, 0);
            this.btnPositiveJog.Margin = new System.Windows.Forms.Padding(0);
            this.btnPositiveJog.Name = "btnPositiveJog";
            this.btnPositiveJog.OnOff = false;
            this.btnPositiveJog.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnPositiveJog.Size = new System.Drawing.Size(60, 25);
            this.btnPositiveJog.TabIndex = 84;
            this.btnPositiveJog.TabStop = false;
            this.btnPositiveJog.Text = "▶(+)";
            this.btnPositiveJog.Text2 = "";
            this.btnPositiveJog.UseVisualStyleBackColor = false;
            this.btnPositiveJog.VisibleLeftLamp = false;
            this.btnPositiveJog.VisibleRightLamp = false;
            // 
            // UcrlMotorJog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "UcrlMotorJog";
            this.Size = new System.Drawing.Size(264, 57);
            ((System.ComponentModel.ISupportInitialize)(this.trbSpeed)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.pnlTrackbar.ResumeLayout(false);
            this.pnlTrackbar.PerformLayout();
            this.pnlRadioSpeed.ResumeLayout(false);
            this.pnlRadioSpeed.PerformLayout();
            this.pnlTextSpeed.ResumeLayout(false);
            this.pnlTextSpeed.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.TrackBar trbSpeed;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel pnlTrackbar;
        public Dit.Framework.UI.UserComponent.ButtonDelay2 btnNegativeJog;
        public Dit.Framework.UI.UserComponent.ButtonDelay2 btnPositiveJog;
        private System.Windows.Forms.FlowLayoutPanel pnlRadioSpeed;
        private System.Windows.Forms.RadioButton radioSpeedLow;
        private System.Windows.Forms.RadioButton radioSpeedMid;
        private System.Windows.Forms.RadioButton radioSpeedHi;
        private System.Windows.Forms.Panel pnlTextSpeed;
        private System.Windows.Forms.TextBox txtSpeed;
    }
}
