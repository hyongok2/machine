namespace EquipMainUi.Monitor
{
    partial class UcrlTwoLampItem
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
            this.tlp1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lamp1 = new System.Windows.Forms.Label();
            this.lamp2 = new System.Windows.Forms.Label();
            this.chkUseBitTest1 = new System.Windows.Forms.CheckBox();
            this.chkUseBitTest2 = new System.Windows.Forms.CheckBox();
            this.tlp1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp1
            // 
            this.tlp1.ColumnCount = 3;
            this.tlp1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlp1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlp1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlp1.Controls.Add(this.lblTitle, 0, 0);
            this.tlp1.Controls.Add(this.lamp1, 1, 0);
            this.tlp1.Controls.Add(this.lamp2, 2, 0);
            this.tlp1.Controls.Add(this.chkUseBitTest1, 1, 1);
            this.tlp1.Controls.Add(this.chkUseBitTest2, 2, 1);
            this.tlp1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp1.Location = new System.Drawing.Point(0, 0);
            this.tlp1.Margin = new System.Windows.Forms.Padding(0);
            this.tlp1.Name = "tlp1";
            this.tlp1.RowCount = 2;
            this.tlp1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tlp1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp1.Size = new System.Drawing.Size(360, 60);
            this.tlp1.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.tlp1.SetRowSpan(this.lblTitle, 2);
            this.lblTitle.Size = new System.Drawing.Size(252, 60);
            this.lblTitle.TabIndex = 29;
            this.lblTitle.Text = "-";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lamp1
            // 
            this.lamp1.AutoSize = true;
            this.lamp1.BackColor = System.Drawing.Color.White;
            this.lamp1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lamp1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lamp1.Location = new System.Drawing.Point(252, 0);
            this.lamp1.Margin = new System.Windows.Forms.Padding(0);
            this.lamp1.Name = "lamp1";
            this.lamp1.Size = new System.Drawing.Size(54, 45);
            this.lamp1.TabIndex = 1;
            this.lamp1.Click += new System.EventHandler(this.lamp1_Click);
            // 
            // lamp2
            // 
            this.lamp2.AutoSize = true;
            this.lamp2.BackColor = System.Drawing.Color.White;
            this.lamp2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lamp2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lamp2.Location = new System.Drawing.Point(306, 0);
            this.lamp2.Margin = new System.Windows.Forms.Padding(0);
            this.lamp2.Name = "lamp2";
            this.lamp2.Size = new System.Drawing.Size(54, 45);
            this.lamp2.TabIndex = 2;
            this.lamp2.Click += new System.EventHandler(this.lamp2_Click);
            // 
            // chkUseBitTest1
            // 
            this.chkUseBitTest1.AutoSize = true;
            this.chkUseBitTest1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkUseBitTest1.Location = new System.Drawing.Point(255, 48);
            this.chkUseBitTest1.Name = "chkUseBitTest1";
            this.chkUseBitTest1.Size = new System.Drawing.Size(48, 9);
            this.chkUseBitTest1.TabIndex = 30;
            this.chkUseBitTest1.UseVisualStyleBackColor = true;
            this.chkUseBitTest1.CheckedChanged += new System.EventHandler(this.chkUseBitTest1_CheckedChanged);
            // 
            // chkUseBitTest2
            // 
            this.chkUseBitTest2.AutoSize = true;
            this.chkUseBitTest2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkUseBitTest2.Location = new System.Drawing.Point(309, 48);
            this.chkUseBitTest2.Name = "chkUseBitTest2";
            this.chkUseBitTest2.Size = new System.Drawing.Size(48, 9);
            this.chkUseBitTest2.TabIndex = 31;
            this.chkUseBitTest2.UseVisualStyleBackColor = true;
            this.chkUseBitTest2.CheckedChanged += new System.EventHandler(this.chkUseBitTest2_CheckedChanged);
            // 
            // UcrlTwoLampItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlp1);
            this.Name = "UcrlTwoLampItem";
            this.Size = new System.Drawing.Size(360, 60);
            this.Load += new System.EventHandler(this.UcrlTwoLampItem_Load);
            this.tlp1.ResumeLayout(false);
            this.tlp1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp1;
        private System.Windows.Forms.Label lamp1;
        private System.Windows.Forms.Label lamp2;
        public System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.CheckBox chkUseBitTest1;
        private System.Windows.Forms.CheckBox chkUseBitTest2;
    }
}
