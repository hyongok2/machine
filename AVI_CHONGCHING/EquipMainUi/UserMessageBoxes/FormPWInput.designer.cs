partial class FormPWInput
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
            this.btnOK = new System.Windows.Forms.Button();
            this.txtPasswd = new System.Windows.Forms.TextBox();
            this.lblPwInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.Location = new System.Drawing.Point(112, 129);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 25);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "확인(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtPasswd
            // 
            this.txtPasswd.BackColor = System.Drawing.Color.White;
            this.txtPasswd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPasswd.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.txtPasswd.Location = new System.Drawing.Point(10, 96);
            this.txtPasswd.Margin = new System.Windows.Forms.Padding(1);
            this.txtPasswd.MaxLength = 10;
            this.txtPasswd.Name = "txtPasswd";
            this.txtPasswd.PasswordChar = '*';
            this.txtPasswd.Size = new System.Drawing.Size(274, 29);
            this.txtPasswd.TabIndex = 1;
            this.txtPasswd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPasswd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPasswd_KeyUp);
            // 
            // lblPwInfo
            // 
            this.lblPwInfo.Location = new System.Drawing.Point(10, 13);
            this.lblPwInfo.Name = "lblPwInfo";
            this.lblPwInfo.Size = new System.Drawing.Size(272, 70);
            this.lblPwInfo.TabIndex = 111;
            this.lblPwInfo.Text = "비밀번호를 입력하세요.";
            this.lblPwInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormPWInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 166);
            this.Controls.Add(this.lblPwInfo);
            this.Controls.Add(this.txtPasswd);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPWInput";
            this.Text = "비밀번호 입력";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.TextBox txtPasswd;
    private System.Windows.Forms.Label lblPwInfo;
}
