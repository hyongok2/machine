namespace EquipMainUi.PreAligner
{
    partial class UcrlLightControllerTest
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
            this.cmbPortNames = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpen = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnOn = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnOff = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnRemote = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnGetError = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.lblErrorMsg = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBright = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbPortNames
            // 
            this.cmbPortNames.FormattingEnabled = true;
            this.cmbPortNames.Location = new System.Drawing.Point(78, 16);
            this.cmbPortNames.Name = "cmbPortNames";
            this.cmbPortNames.Size = new System.Drawing.Size(121, 20);
            this.cmbPortNames.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(205, 14);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnOn
            // 
            this.btnOn.Location = new System.Drawing.Point(205, 72);
            this.btnOn.Name = "btnOn";
            this.btnOn.Size = new System.Drawing.Size(75, 23);
            this.btnOn.TabIndex = 3;
            this.btnOn.Text = "On";
            this.btnOn.UseVisualStyleBackColor = true;
            this.btnOn.Click += new System.EventHandler(this.btnOn_Click);
            // 
            // btnOff
            // 
            this.btnOff.Location = new System.Drawing.Point(205, 101);
            this.btnOff.Name = "btnOff";
            this.btnOff.Size = new System.Drawing.Size(75, 23);
            this.btnOff.TabIndex = 4;
            this.btnOff.Text = "Off";
            this.btnOff.UseVisualStyleBackColor = true;
            this.btnOff.Click += new System.EventHandler(this.btnOff_Click);
            // 
            // btnRemote
            // 
            this.btnRemote.Location = new System.Drawing.Point(205, 43);
            this.btnRemote.Name = "btnRemote";
            this.btnRemote.Size = new System.Drawing.Size(75, 23);
            this.btnRemote.TabIndex = 5;
            this.btnRemote.Text = "Remote";
            this.btnRemote.UseVisualStyleBackColor = true;
            this.btnRemote.Click += new System.EventHandler(this.btnRemote_Click);
            // 
            // btnGetError
            // 
            this.btnGetError.Location = new System.Drawing.Point(205, 130);
            this.btnGetError.Name = "btnGetError";
            this.btnGetError.Size = new System.Drawing.Size(75, 23);
            this.btnGetError.TabIndex = 6;
            this.btnGetError.Text = "Get Error";
            this.btnGetError.UseVisualStyleBackColor = true;
            this.btnGetError.Click += new System.EventHandler(this.btnGetError_Click);
            // 
            // lblErrorMsg
            // 
            this.lblErrorMsg.Location = new System.Drawing.Point(91, 160);
            this.lblErrorMsg.Name = "lblErrorMsg";
            this.lblErrorMsg.Size = new System.Drawing.Size(189, 134);
            this.lblErrorMsg.TabIndex = 7;
            this.lblErrorMsg.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Error Msg";
            // 
            // txtBright
            // 
            this.txtBright.Location = new System.Drawing.Point(78, 74);
            this.txtBright.Name = "txtBright";
            this.txtBright.Size = new System.Drawing.Size(121, 21);
            this.txtBright.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Bright";
            // 
            // UcrlLightControllerTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBright);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblErrorMsg);
            this.Controls.Add(this.btnGetError);
            this.Controls.Add(this.btnRemote);
            this.Controls.Add(this.btnOff);
            this.Controls.Add(this.btnOn);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPortNames);
            this.Name = "UcrlLightControllerTest";
            this.Size = new System.Drawing.Size(304, 305);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPortNames;
        private System.Windows.Forms.Label label1;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnOpen;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnOn;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnOff;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnRemote;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnGetError;
        private System.Windows.Forms.Label lblErrorMsg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBright;
        private System.Windows.Forms.Label label4;
    }
}
