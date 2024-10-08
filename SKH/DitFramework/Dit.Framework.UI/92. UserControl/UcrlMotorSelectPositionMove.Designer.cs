using Dit.Framework.UI.UserComponent;
namespace Dit.Framework.UI.Motor
{
    partial class UcrlMotorSelectPositionMove
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
            this.cmbSelectPos = new System.Windows.Forms.ComboBox();
            this.btnMove = new ButtonDelay2();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPosition = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoEllipsis = true;
            this.lblSpeed.BackColor = System.Drawing.Color.Gainsboro;
            this.lblSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSpeed.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpeed.ForeColor = System.Drawing.Color.Black;
            this.lblSpeed.Location = new System.Drawing.Point(176, 20);
            this.lblSpeed.Margin = new System.Windows.Forms.Padding(0);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(74, 27);
            this.lblSpeed.TabIndex = 133;
            this.lblSpeed.Text = "0000.0mm/s";
            this.lblSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbSelectPos
            // 
            this.cmbSelectPos.FormattingEnabled = true;
            this.cmbSelectPos.Items.AddRange(new object[] {
            "위치"});
            this.cmbSelectPos.Location = new System.Drawing.Point(0, 0);
            this.cmbSelectPos.Margin = new System.Windows.Forms.Padding(0);
            this.cmbSelectPos.Name = "cmbSelectPos";
            this.cmbSelectPos.Size = new System.Drawing.Size(250, 20);
            this.cmbSelectPos.TabIndex = 131;
            this.cmbSelectPos.Text = "위치 선택";
            // 
            // btnMove
            // 
            this.btnMove.BackColor = System.Drawing.Color.Transparent;
            this.btnMove.Delay = 1;
            this.btnMove.Flicker = false;
            this.btnMove.Font = new System.Drawing.Font("맑은 고딕", 9.5F);
            this.btnMove.ForeColor = System.Drawing.Color.Black;
            this.btnMove.IsLeftLampOn = false;
            this.btnMove.IsRightLampOn = false;
            this.btnMove.LampAliveTime = 500;
            this.btnMove.LampSize = 5;
            this.btnMove.LeftLampColor = System.Drawing.Color.Red;
            this.btnMove.Location = new System.Drawing.Point(0, 20);
            this.btnMove.Margin = new System.Windows.Forms.Padding(0);
            this.btnMove.Name = "btnMove";
            this.btnMove.OnOff = false;
            this.btnMove.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnMove.Size = new System.Drawing.Size(112, 27);
            this.btnMove.TabIndex = 132;
            this.btnMove.TabStop = false;
            this.btnMove.Text = "선택위치이동";
            this.btnMove.Text2 = "";
            this.btnMove.UseVisualStyleBackColor = false;
            this.btnMove.VisibleLeftLamp = true;
            this.btnMove.VisibleRightLamp = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmbSelectPos);
            this.flowLayoutPanel1.Controls.Add(this.btnMove);
            this.flowLayoutPanel1.Controls.Add(this.lblPosition);
            this.flowLayoutPanel1.Controls.Add(this.lblSpeed);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(252, 49);
            this.flowLayoutPanel1.TabIndex = 134;
            // 
            // lblPosition
            // 
            this.lblPosition.AutoEllipsis = true;
            this.lblPosition.BackColor = System.Drawing.Color.Gainsboro;
            this.lblPosition.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPosition.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosition.ForeColor = System.Drawing.Color.Black;
            this.lblPosition.Location = new System.Drawing.Point(112, 20);
            this.lblPosition.Margin = new System.Windows.Forms.Padding(0);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(64, 27);
            this.lblPosition.TabIndex = 133;
            this.lblPosition.Text = "0000.0mm";
            this.lblPosition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UcrlMotorSelectPositionMove
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "UcrlMotorSelectPositionMove";
            this.Size = new System.Drawing.Size(252, 49);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.ComboBox cmbSelectPos;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public Dit.Framework.UI.UserComponent.ButtonDelay2 btnMove;
        internal System.Windows.Forms.Label lblPosition;
    }
}
