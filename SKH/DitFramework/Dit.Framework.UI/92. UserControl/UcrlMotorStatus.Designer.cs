namespace Dit.Framework.UI.Motor
{
    partial class UcrlMotorStatus
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblHomeBit = new System.Windows.Forms.Label();
            this.lblServoOn = new System.Windows.Forms.Label();
            this.lblMoving = new System.Windows.Forms.Label();
            this.lblPositiveLimit = new System.Windows.Forms.Label();
            this.lblNegativeLimit = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lblHomeBit);
            this.flowLayoutPanel1.Controls.Add(this.lblServoOn);
            this.flowLayoutPanel1.Controls.Add(this.lblMoving);
            this.flowLayoutPanel1.Controls.Add(this.lblPositiveLimit);
            this.flowLayoutPanel1.Controls.Add(this.lblNegativeLimit);
            this.flowLayoutPanel1.Controls.Add(this.lblError);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(78, 134);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // lblHomeBit
            // 
            this.lblHomeBit.AutoEllipsis = true;
            this.lblHomeBit.BackColor = System.Drawing.Color.Gainsboro;
            this.lblHomeBit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHomeBit.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHomeBit.ForeColor = System.Drawing.Color.Black;
            this.lblHomeBit.Location = new System.Drawing.Point(0, 0);
            this.lblHomeBit.Margin = new System.Windows.Forms.Padding(0);
            this.lblHomeBit.Name = "lblHomeBit";
            this.lblHomeBit.Size = new System.Drawing.Size(67, 20);
            this.lblHomeBit.TabIndex = 120;
            this.lblHomeBit.Text = "Home Bit";
            this.lblHomeBit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblServoOn
            // 
            this.lblServoOn.AutoEllipsis = true;
            this.lblServoOn.BackColor = System.Drawing.Color.Gainsboro;
            this.lblServoOn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblServoOn.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServoOn.ForeColor = System.Drawing.Color.Black;
            this.lblServoOn.Location = new System.Drawing.Point(0, 20);
            this.lblServoOn.Margin = new System.Windows.Forms.Padding(0);
            this.lblServoOn.Name = "lblServoOn";
            this.lblServoOn.Size = new System.Drawing.Size(67, 20);
            this.lblServoOn.TabIndex = 123;
            this.lblServoOn.Text = "Servo On";
            this.lblServoOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMoving
            // 
            this.lblMoving.AutoEllipsis = true;
            this.lblMoving.BackColor = System.Drawing.Color.Gainsboro;
            this.lblMoving.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMoving.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoving.ForeColor = System.Drawing.Color.Black;
            this.lblMoving.Location = new System.Drawing.Point(0, 40);
            this.lblMoving.Margin = new System.Windows.Forms.Padding(0);
            this.lblMoving.Name = "lblMoving";
            this.lblMoving.Size = new System.Drawing.Size(67, 20);
            this.lblMoving.TabIndex = 122;
            this.lblMoving.Text = "Moving";
            this.lblMoving.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPositiveLimit
            // 
            this.lblPositiveLimit.AutoEllipsis = true;
            this.lblPositiveLimit.BackColor = System.Drawing.Color.Gainsboro;
            this.lblPositiveLimit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPositiveLimit.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPositiveLimit.ForeColor = System.Drawing.Color.Black;
            this.lblPositiveLimit.Location = new System.Drawing.Point(0, 60);
            this.lblPositiveLimit.Margin = new System.Windows.Forms.Padding(0);
            this.lblPositiveLimit.Name = "lblPositiveLimit";
            this.lblPositiveLimit.Size = new System.Drawing.Size(67, 20);
            this.lblPositiveLimit.TabIndex = 124;
            this.lblPositiveLimit.Text = "( + ) Limit";
            this.lblPositiveLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNegativeLimit
            // 
            this.lblNegativeLimit.AutoEllipsis = true;
            this.lblNegativeLimit.BackColor = System.Drawing.Color.Gainsboro;
            this.lblNegativeLimit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNegativeLimit.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNegativeLimit.ForeColor = System.Drawing.Color.Black;
            this.lblNegativeLimit.Location = new System.Drawing.Point(0, 80);
            this.lblNegativeLimit.Margin = new System.Windows.Forms.Padding(0);
            this.lblNegativeLimit.Name = "lblNegativeLimit";
            this.lblNegativeLimit.Size = new System.Drawing.Size(67, 20);
            this.lblNegativeLimit.TabIndex = 121;
            this.lblNegativeLimit.Text = "( - ) Limit";
            this.lblNegativeLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblError
            // 
            this.lblError.AutoEllipsis = true;
            this.lblError.BackColor = System.Drawing.Color.Gainsboro;
            this.lblError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblError.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Black;
            this.lblError.Location = new System.Drawing.Point(0, 100);
            this.lblError.Margin = new System.Windows.Forms.Padding(0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(67, 20);
            this.lblError.TabIndex = 125;
            this.lblError.Text = "Error";
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UcrlMotorStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "UcrlMotorStatus";
            this.Size = new System.Drawing.Size(78, 134);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        internal System.Windows.Forms.Label lblServoOn;
        internal System.Windows.Forms.Label lblPositiveLimit;
        internal System.Windows.Forms.Label lblNegativeLimit;
        internal System.Windows.Forms.Label lblMoving;
        internal System.Windows.Forms.Label lblHomeBit;
        internal System.Windows.Forms.Label lblError;
    }
}
