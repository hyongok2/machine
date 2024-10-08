namespace Dit.Framework.Analog
{
    partial class FrmSetting
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
            this.tabCtrl = new System.Windows.Forms.TabControl();
            this.tabp_AD1 = new System.Windows.Forms.TabPage();
            this.ucrlSetting_68ADVN1 = new UcrlSetting_68DAVN();
            this.tabp_AD2 = new System.Windows.Forms.TabPage();
            this.tmrUiUpdate = new System.Windows.Forms.Timer(this.components);
            this.tabCtrl.SuspendLayout();
            this.tabp_AD1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCtrl
            // 
            this.tabCtrl.Controls.Add(this.tabp_AD1);
            this.tabCtrl.Controls.Add(this.tabp_AD2);
            this.tabCtrl.Location = new System.Drawing.Point(1, 0);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(843, 357);
            this.tabCtrl.TabIndex = 0;
            // 
            // tabp_AD1
            // 
            this.tabp_AD1.Controls.Add(this.ucrlSetting_68ADVN1);
            this.tabp_AD1.Location = new System.Drawing.Point(4, 22);
            this.tabp_AD1.Name = "tabp_AD1";
            this.tabp_AD1.Padding = new System.Windows.Forms.Padding(3);
            this.tabp_AD1.Size = new System.Drawing.Size(835, 331);
            this.tabp_AD1.TabIndex = 0;
            this.tabp_AD1.Text = "AD1";
            this.tabp_AD1.UseVisualStyleBackColor = true;
            // 
            // ucrlSetting_68ADVN1
            // 
            this.ucrlSetting_68ADVN1.DAVN = null;
            this.ucrlSetting_68ADVN1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucrlSetting_68ADVN1.Location = new System.Drawing.Point(6, 6);
            this.ucrlSetting_68ADVN1.Name = "ucrlSetting_68ADVN1";
            this.ucrlSetting_68ADVN1.Size = new System.Drawing.Size(835, 323);
            this.ucrlSetting_68ADVN1.TabIndex = 0;
            // 
            // tabp_AD2
            // 
            this.tabp_AD2.Location = new System.Drawing.Point(4, 22);
            this.tabp_AD2.Name = "tabp_AD2";
            this.tabp_AD2.Padding = new System.Windows.Forms.Padding(3);
            this.tabp_AD2.Size = new System.Drawing.Size(835, 331);
            this.tabp_AD2.TabIndex = 1;
            this.tabp_AD2.Text = "AD2";
            this.tabp_AD2.UseVisualStyleBackColor = true;
            // 
            // tmrUiUpdate
            // 
            this.tmrUiUpdate.Tick += new System.EventHandler(this.tmrUiUpdate_Tick);
            // 
            // FrmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 355);
            this.Controls.Add(this.tabCtrl);
            this.Name = "FrmSetting";
            this.Text = "Form1";
            this.tabCtrl.ResumeLayout(false);
            this.tabp_AD1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCtrl;
        private System.Windows.Forms.TabPage tabp_AD1;
        private System.Windows.Forms.TabPage tabp_AD2;
        private System.Windows.Forms.Timer tmrUiUpdate;
        private UcrlSetting_68DAVN ucrlSetting_68ADVN1;
    }
}

