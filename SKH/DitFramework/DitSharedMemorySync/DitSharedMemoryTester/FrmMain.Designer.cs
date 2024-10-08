namespace DitSharedMemoryTester
{
    partial class FrmMain
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
            this.btnRead = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtShName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudShSize = new System.Windows.Forms.NumericUpDown();
            this.txtWrite = new System.Windows.Forms.TextBox();
            this.txtRead = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudShAddress = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cboShType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvMemory = new System.Windows.Forms.DataGridView();
            this.dgcAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcBit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcBitValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tmrShow = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nudShSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudShAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemory)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(196, 146);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(49, 27);
            this.btnRead.TabIndex = 0;
            this.btnRead.Text = "읽기";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(196, 118);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(49, 27);
            this.btnWrite.TabIndex = 0;
            this.btnWrite.Text = "쓰기";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(196, 7);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(49, 27);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "생성";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(8, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 22);
            this.label3.TabIndex = 1;
            this.label3.Text = "이름";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtShName
            // 
            this.txtShName.Location = new System.Drawing.Point(69, 9);
            this.txtShName.Name = "txtShName";
            this.txtShName.Size = new System.Drawing.Size(121, 22);
            this.txtShName.TabIndex = 2;
            this.txtShName.Text = "TEST";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(8, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "주소";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudShSize
            // 
            this.nudShSize.Location = new System.Drawing.Point(69, 39);
            this.nudShSize.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.nudShSize.Name = "nudShSize";
            this.nudShSize.Size = new System.Drawing.Size(121, 22);
            this.nudShSize.TabIndex = 3;
            this.nudShSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudShSize.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // txtWrite
            // 
            this.txtWrite.Location = new System.Drawing.Point(69, 120);
            this.txtWrite.Name = "txtWrite";
            this.txtWrite.Size = new System.Drawing.Size(121, 22);
            this.txtWrite.TabIndex = 2;
            // 
            // txtRead
            // 
            this.txtRead.Location = new System.Drawing.Point(69, 146);
            this.txtRead.Name = "txtRead";
            this.txtRead.Size = new System.Drawing.Size(121, 22);
            this.txtRead.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(8, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "주소";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudShAddress
            // 
            this.nudShAddress.Location = new System.Drawing.Point(69, 66);
            this.nudShAddress.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.nudShAddress.Name = "nudShAddress";
            this.nudShAddress.Size = new System.Drawing.Size(121, 22);
            this.nudShAddress.TabIndex = 3;
            this.nudShAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(8, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 22);
            this.label4.TabIndex = 4;
            this.label4.Text = "타입";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboShType
            // 
            this.cboShType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboShType.FormattingEnabled = true;
            this.cboShType.Items.AddRange(new object[] {
            "INT32"});
            this.cboShType.Location = new System.Drawing.Point(69, 95);
            this.cboShType.Name = "cboShType";
            this.cboShType.Size = new System.Drawing.Size(121, 21);
            this.cboShType.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(8, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 48);
            this.label5.TabIndex = 4;
            this.label5.Text = "읽기\r\n쓰기";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvMemory
            // 
            this.dgvMemory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMemory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcAddr,
            this.dgcBit,
            this.dgcBitValue});
            this.dgvMemory.Location = new System.Drawing.Point(8, 206);
            this.dgvMemory.Name = "dgvMemory";
            this.dgvMemory.RowHeadersVisible = false;
            this.dgvMemory.RowTemplate.Height = 23;
            this.dgvMemory.Size = new System.Drawing.Size(579, 225);
            this.dgvMemory.TabIndex = 6;
            // 
            // dgcAddr
            // 
            this.dgcAddr.HeaderText = "Addr";
            this.dgcAddr.Name = "dgcAddr";
            // 
            // dgcBit
            // 
            this.dgcBit.HeaderText = "BIT";
            this.dgcBit.Name = "dgcBit";
            this.dgcBit.Width = 300;
            // 
            // dgcBitValue
            // 
            this.dgcBitValue.HeaderText = "";
            this.dgcBitValue.Name = "dgcBitValue";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(196, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 27);
            this.button1.TabIndex = 0;
            this.button1.Text = "이동";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(196, 91);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(49, 27);
            this.button2.TabIndex = 0;
            this.button2.Text = "보기";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // tmrShow
            // 
            this.tmrShow.Tick += new System.EventHandler(this.tmrShow_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(599, 434);
            this.Controls.Add(this.dgvMemory);
            this.Controls.Add(this.cboShType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudShAddress);
            this.Controls.Add(this.nudShSize);
            this.Controls.Add(this.txtRead);
            this.Controls.Add(this.txtWrite);
            this.Controls.Add(this.txtShName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnRead);
            this.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "FrmMain";
            this.Text = "FrmMain";
            ((System.ComponentModel.ISupportInitialize)(this.nudShSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudShAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtShName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudShSize;
        private System.Windows.Forms.TextBox txtWrite;
        private System.Windows.Forms.TextBox txtRead;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudShAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboShType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvMemory;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcBit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcBitValue;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer tmrShow;
    }
}

