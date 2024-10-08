partial class FormAutoClose
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
        this.components = new System.ComponentModel.Container();
        this.label1 = new System.Windows.Forms.Label();
        this.timer1 = new System.Windows.Forms.Timer(this.components);
        this.btnOK = new System.Windows.Forms.Button();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.label1.Location = new System.Drawing.Point(12, 2);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(270, 51);
        this.label1.TabIndex = 1;
        this.label1.Text = "...";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // timer1
        // 
        this.timer1.Interval = 1000;
        this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        // 
        // btnOK
        // 
        this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.btnOK.Location = new System.Drawing.Point(112, 56);
        this.btnOK.Name = "btnOK";
        this.btnOK.Size = new System.Drawing.Size(70, 25);
        this.btnOK.TabIndex = 3;
        this.btnOK.Text = "확인(&O)";
        this.btnOK.UseVisualStyleBackColor = true;
        this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
        // 
        // FormAutoClose
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(294, 93);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.btnOK);
        this.Font = new System.Drawing.Font("맑은 고딕", 9F);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "FormAutoClose";
        this.Text = "자동 종료";
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.Button btnOK;
}
