namespace EquipMainUi.Monitor
{
    partial class UcrlIoItem
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
            this.btnXY = new System.Windows.Forms.Button();
            this.lblXYName = new System.Windows.Forms.Label();
            this.lblXYLamp = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.chkUseBitTest = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnXY
            // 
            this.btnXY.BackColor = System.Drawing.Color.Transparent;
            this.btnXY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnXY.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.btnXY.ForeColor = System.Drawing.Color.Black;
            this.btnXY.Location = new System.Drawing.Point(0, 0);
            this.btnXY.Margin = new System.Windows.Forms.Padding(0);
            this.btnXY.Name = "btnXY";
            this.btnXY.Size = new System.Drawing.Size(81, 27);
            this.btnXY.TabIndex = 29;
            this.btnXY.Text = "X00";
            this.btnXY.UseVisualStyleBackColor = false;
            this.btnXY.Click += new System.EventHandler(this.btnXY_Click);
            // 
            // lblXYName
            // 
            this.lblXYName.AutoSize = true;
            this.lblXYName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblXYName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblXYName.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblXYName.ForeColor = System.Drawing.Color.White;
            this.lblXYName.Location = new System.Drawing.Point(128, 0);
            this.lblXYName.Margin = new System.Windows.Forms.Padding(0);
            this.lblXYName.Name = "lblXYName";
            this.lblXYName.Size = new System.Drawing.Size(293, 27);
            this.lblXYName.TabIndex = 28;
            this.lblXYName.Text = "-";
            this.lblXYName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblXYLamp
            // 
            this.lblXYLamp.AutoSize = true;
            this.lblXYLamp.BackColor = System.Drawing.Color.White;
            this.lblXYLamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblXYLamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblXYLamp.Font = new System.Drawing.Font("휴먼모음T", 8.25F);
            this.lblXYLamp.Location = new System.Drawing.Point(81, 0);
            this.lblXYLamp.Margin = new System.Windows.Forms.Padding(0);
            this.lblXYLamp.Name = "lblXYLamp";
            this.lblXYLamp.Padding = new System.Windows.Forms.Padding(1);
            this.lblXYLamp.Size = new System.Drawing.Size(47, 27);
            this.lblXYLamp.TabIndex = 27;
            this.lblXYLamp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnXY, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblXYName, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblXYLamp, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkUseBitTest, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(441, 27);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // chkUseBitTest
            // 
            this.chkUseBitTest.AutoSize = true;
            this.chkUseBitTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkUseBitTest.Location = new System.Drawing.Point(424, 3);
            this.chkUseBitTest.Name = "chkUseBitTest";
            this.chkUseBitTest.Size = new System.Drawing.Size(14, 21);
            this.chkUseBitTest.TabIndex = 30;
            this.chkUseBitTest.Text = "checkBox1";
            this.chkUseBitTest.UseVisualStyleBackColor = true;
            this.chkUseBitTest.CheckedChanged += new System.EventHandler(this.chkUseBitTest_CheckedChanged);
            // 
            // UcrlIoItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UcrlIoItem";
            this.Size = new System.Drawing.Size(441, 27);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnXY;
        public System.Windows.Forms.Label lblXYName;
        private System.Windows.Forms.Label lblXYLamp;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox chkUseBitTest;
    }
}
