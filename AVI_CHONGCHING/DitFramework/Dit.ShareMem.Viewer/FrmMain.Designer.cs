namespace Dit.ShareMem.Viewer
{
    partial class frmMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtShareMemName = new System.Windows.Forms.TextBox();
            this.nudShareMemSize = new System.Windows.Forms.NumericUpDown();
            this.txtStartDevice = new System.Windows.Forms.TextBox();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.btnWriteShortValue = new System.Windows.Forms.Button();
            this.nudWriteShortBitValue = new System.Windows.Forms.NumericUpDown();
            this.txtWriteAddr = new System.Windows.Forms.TextBox();
            this.dgvDevice = new System.Windows.Forms.DataGridView();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ch16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ch15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ch14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ch12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ch11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ch10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ch09 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ch08 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ch07 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ch06 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ch05 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ch04 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ch03 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ch02 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ch01 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ch00 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.tmrState = new System.Windows.Forms.Timer(this.components);
            this.btnWriteBitValue = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdoViewAscii = new System.Windows.Forms.RadioButton();
            this.rdoViewInt32 = new System.Windows.Forms.RadioButton();
            this.rdoViewShort = new System.Windows.Forms.RadioButton();
            this.dgvTagList = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chBtnCommand = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWriteAsciiValue = new System.Windows.Forms.TextBox();
            this.btnWriteAsciiValue = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudShareMemSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWriteShortBitValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevice)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTagList)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(269, 19);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(100, 50);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "연결";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtShareMemName
            // 
            this.txtShareMemName.Location = new System.Drawing.Point(90, 19);
            this.txtShareMemName.Name = "txtShareMemName";
            this.txtShareMemName.Size = new System.Drawing.Size(177, 23);
            this.txtShareMemName.TabIndex = 3;
            this.txtShareMemName.Text = "DIT.PC_CTRL_MEM.S";
            this.txtShareMemName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nudShareMemSize
            // 
            this.nudShareMemSize.Location = new System.Drawing.Point(90, 46);
            this.nudShareMemSize.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.nudShareMemSize.Name = "nudShareMemSize";
            this.nudShareMemSize.Size = new System.Drawing.Size(177, 23);
            this.nudShareMemSize.TabIndex = 4;
            this.nudShareMemSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudShareMemSize.Value = new decimal(new int[] {
            102400,
            0,
            0,
            0});
            // 
            // txtStartDevice
            // 
            this.txtStartDevice.Location = new System.Drawing.Point(96, 11);
            this.txtStartDevice.Name = "txtStartDevice";
            this.txtStartDevice.Size = new System.Drawing.Size(119, 23);
            this.txtStartDevice.TabIndex = 3;
            this.txtStartDevice.Text = "S0";
            this.txtStartDevice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStartDevice_KeyPress);
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // btnWriteShortValue
            // 
            this.btnWriteShortValue.Location = new System.Drawing.Point(607, 95);
            this.btnWriteShortValue.Name = "btnWriteShortValue";
            this.btnWriteShortValue.Size = new System.Drawing.Size(111, 50);
            this.btnWriteShortValue.TabIndex = 1;
            this.btnWriteShortValue.Text = "숫자 쓰기(Short)";
            this.btnWriteShortValue.UseVisualStyleBackColor = true;
            this.btnWriteShortValue.Click += new System.EventHandler(this.btnWriteValue_Click);
            // 
            // nudWriteShortBitValue
            // 
            this.nudWriteShortBitValue.Location = new System.Drawing.Point(607, 66);
            this.nudWriteShortBitValue.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.nudWriteShortBitValue.Name = "nudWriteShortBitValue";
            this.nudWriteShortBitValue.Size = new System.Drawing.Size(111, 23);
            this.nudWriteShortBitValue.TabIndex = 4;
            // 
            // txtWriteAddr
            // 
            this.txtWriteAddr.Location = new System.Drawing.Point(607, 37);
            this.txtWriteAddr.Name = "txtWriteAddr";
            this.txtWriteAddr.Size = new System.Drawing.Size(111, 23);
            this.txtWriteAddr.TabIndex = 3;
            this.txtWriteAddr.Text = "S0";
            // 
            // dgvDevice
            // 
            this.dgvDevice.AllowUserToAddRows = false;
            this.dgvDevice.AllowUserToDeleteRows = false;
            this.dgvDevice.AllowUserToResizeColumns = false;
            this.dgvDevice.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dgvDevice.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDevice.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDevice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDevice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTitle,
            this.ch16,
            this.ch15,
            this.ch14,
            this.ch12,
            this.ch11,
            this.ch10,
            this.ch09,
            this.ch08,
            this.ch07,
            this.ch06,
            this.ch05,
            this.ch04,
            this.ch03,
            this.ch02,
            this.ch01,
            this.ch00,
            this.Column3});
            this.dgvDevice.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvDevice.Location = new System.Drawing.Point(6, 37);
            this.dgvDevice.Name = "dgvDevice";
            this.dgvDevice.ReadOnly = true;
            this.dgvDevice.RowHeadersVisible = false;
            this.dgvDevice.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            this.dgvDevice.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDevice.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dgvDevice.RowTemplate.Height = 23;
            this.dgvDevice.Size = new System.Drawing.Size(510, 363);
            this.dgvDevice.TabIndex = 11;
            this.dgvDevice.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDevice_CellContentClick);
            // 
            // colTitle
            // 
            this.colTitle.HeaderText = "주소";
            this.colTitle.Name = "colTitle";
            this.colTitle.ReadOnly = true;
            this.colTitle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colTitle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTitle.Width = 80;
            // 
            // ch16
            // 
            this.ch16.HeaderText = "F";
            this.ch16.Name = "ch16";
            this.ch16.ReadOnly = true;
            this.ch16.Width = 20;
            // 
            // ch15
            // 
            this.ch15.HeaderText = "E";
            this.ch15.Name = "ch15";
            this.ch15.ReadOnly = true;
            this.ch15.Width = 20;
            // 
            // ch14
            // 
            this.ch14.HeaderText = "D";
            this.ch14.Name = "ch14";
            this.ch14.ReadOnly = true;
            this.ch14.Width = 20;
            // 
            // ch12
            // 
            this.ch12.HeaderText = "C";
            this.ch12.Name = "ch12";
            this.ch12.ReadOnly = true;
            this.ch12.Width = 20;
            // 
            // ch11
            // 
            this.ch11.HeaderText = "B";
            this.ch11.Name = "ch11";
            this.ch11.ReadOnly = true;
            this.ch11.Width = 20;
            // 
            // ch10
            // 
            this.ch10.HeaderText = "A";
            this.ch10.Name = "ch10";
            this.ch10.ReadOnly = true;
            this.ch10.Width = 20;
            // 
            // ch09
            // 
            this.ch09.HeaderText = "9";
            this.ch09.Name = "ch09";
            this.ch09.ReadOnly = true;
            this.ch09.Width = 20;
            // 
            // ch08
            // 
            this.ch08.HeaderText = "8";
            this.ch08.Name = "ch08";
            this.ch08.ReadOnly = true;
            this.ch08.Width = 20;
            // 
            // ch07
            // 
            this.ch07.HeaderText = "7";
            this.ch07.Name = "ch07";
            this.ch07.ReadOnly = true;
            this.ch07.Width = 20;
            // 
            // ch06
            // 
            this.ch06.HeaderText = "6";
            this.ch06.Name = "ch06";
            this.ch06.ReadOnly = true;
            this.ch06.Width = 20;
            // 
            // ch05
            // 
            this.ch05.HeaderText = "5";
            this.ch05.Name = "ch05";
            this.ch05.ReadOnly = true;
            this.ch05.Width = 20;
            // 
            // ch04
            // 
            this.ch04.HeaderText = "4";
            this.ch04.Name = "ch04";
            this.ch04.ReadOnly = true;
            this.ch04.Width = 20;
            // 
            // ch03
            // 
            this.ch03.HeaderText = "3";
            this.ch03.Name = "ch03";
            this.ch03.ReadOnly = true;
            this.ch03.Width = 20;
            // 
            // ch02
            // 
            this.ch02.HeaderText = "2";
            this.ch02.Name = "ch02";
            this.ch02.ReadOnly = true;
            this.ch02.Width = 20;
            // 
            // ch01
            // 
            this.ch01.HeaderText = "1";
            this.ch01.Name = "ch01";
            this.ch01.ReadOnly = true;
            this.ch01.Width = 20;
            // 
            // ch00
            // 
            this.ch00.HeaderText = "0";
            this.ch00.Name = "ch00";
            this.ch00.ReadOnly = true;
            this.ch00.Width = 20;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Value";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tbStatus
            // 
            this.tbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStatus.Location = new System.Drawing.Point(1, 535);
            this.tbStatus.Multiline = true;
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.ReadOnly = true;
            this.tbStatus.Size = new System.Drawing.Size(736, 21);
            this.tbStatus.TabIndex = 77;
            // 
            // tmrState
            // 
            this.tmrState.Enabled = true;
            this.tmrState.Interval = 1000;
            this.tmrState.Tick += new System.EventHandler(this.tmrState_Tick);
            // 
            // btnWriteBitValue
            // 
            this.btnWriteBitValue.Location = new System.Drawing.Point(607, 151);
            this.btnWriteBitValue.Name = "btnWriteBitValue";
            this.btnWriteBitValue.Size = new System.Drawing.Size(111, 50);
            this.btnWriteBitValue.TabIndex = 1;
            this.btnWriteBitValue.Text = "비트 쓰기";
            this.btnWriteBitValue.UseVisualStyleBackColor = true;
            this.btnWriteBitValue.Click += new System.EventHandler(this.btnWriteBitValue_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtShareMemName);
            this.groupBox1.Controls.Add(this.nudShareMemSize);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Location = new System.Drawing.Point(2, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 80);
            this.groupBox1.TabIndex = 78;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "공유 메모리 연결";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Gainsboro;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(6, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 22);
            this.label4.TabIndex = 88;
            this.label4.Text = "사이즈";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Gainsboro;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 22);
            this.label3.TabIndex = 88;
            this.label3.Text = "이  름";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdoViewAscii);
            this.groupBox3.Controls.Add(this.rdoViewInt32);
            this.groupBox3.Controls.Add(this.rdoViewShort);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(383, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(175, 80);
            this.groupBox3.TabIndex = 78;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "값 자료형";
            // 
            // rdoViewAscii
            // 
            this.rdoViewAscii.AutoSize = true;
            this.rdoViewAscii.Location = new System.Drawing.Point(100, 23);
            this.rdoViewAscii.Name = "rdoViewAscii";
            this.rdoViewAscii.Size = new System.Drawing.Size(54, 19);
            this.rdoViewAscii.TabIndex = 0;
            this.rdoViewAscii.Text = "ASCII";
            this.rdoViewAscii.UseVisualStyleBackColor = true;
            // 
            // rdoViewInt32
            // 
            this.rdoViewInt32.AutoSize = true;
            this.rdoViewInt32.Location = new System.Drawing.Point(6, 46);
            this.rdoViewInt32.Name = "rdoViewInt32";
            this.rdoViewInt32.Size = new System.Drawing.Size(57, 19);
            this.rdoViewInt32.TabIndex = 0;
            this.rdoViewInt32.Text = "INT32";
            this.rdoViewInt32.UseVisualStyleBackColor = true;
            // 
            // rdoViewShort
            // 
            this.rdoViewShort.AutoSize = true;
            this.rdoViewShort.Checked = true;
            this.rdoViewShort.Location = new System.Drawing.Point(6, 22);
            this.rdoViewShort.Name = "rdoViewShort";
            this.rdoViewShort.Size = new System.Drawing.Size(63, 19);
            this.rdoViewShort.TabIndex = 0;
            this.rdoViewShort.TabStop = true;
            this.rdoViewShort.Text = "SHORT";
            this.rdoViewShort.UseVisualStyleBackColor = true;
            // 
            // dgvTagList
            // 
            this.dgvTagList.AllowUserToAddRows = false;
            this.dgvTagList.AllowUserToDeleteRows = false;
            this.dgvTagList.AllowUserToResizeColumns = false;
            this.dgvTagList.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dgvTagList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvTagList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTagList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTagList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTagList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.chSize,
            this.chDataType,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16,
            this.Column2,
            this.chBtnCommand});
            this.dgvTagList.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvTagList.Location = new System.Drawing.Point(5, 28);
            this.dgvTagList.Name = "dgvTagList";
            this.dgvTagList.RowHeadersVisible = false;
            this.dgvTagList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("맑은 고딕", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            this.dgvTagList.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTagList.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.dgvTagList.RowTemplate.Height = 23;
            this.dgvTagList.Size = new System.Drawing.Size(638, 371);
            this.dgvTagList.TabIndex = 11;
            this.dgvTagList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTagList_CellClick);
            this.dgvTagList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTagList_CellDoubleClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "주소";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 80;
            // 
            // chSize
            // 
            this.chSize.HeaderText = "크기";
            this.chSize.Name = "chSize";
            this.chSize.Width = 60;
            // 
            // chDataType
            // 
            this.chDataType.HeaderText = "자료형";
            this.chDataType.Name = "chDataType";
            this.chDataType.Width = 50;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.HeaderText = "설명";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn15.Width = 200;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.HeaderText = "현재값";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.Width = 60;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "입력값";
            this.Column2.Name = "Column2";
            this.Column2.Width = 80;
            // 
            // chBtnCommand
            // 
            this.chBtnCommand.HeaderText = "입력";
            this.chBtnCommand.Name = "chBtnCommand";
            this.chBtnCommand.Width = 50;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Enabled = false;
            this.tabControl1.Location = new System.Drawing.Point(2, 101);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(732, 432);
            this.tabControl1.TabIndex = 79;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.dgvDevice);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.txtStartDevice);
            this.tabPage1.Controls.Add(this.txtWriteAsciiValue);
            this.tabPage1.Controls.Add(this.txtWriteAddr);
            this.tabPage1.Controls.Add(this.btnWriteAsciiValue);
            this.tabPage1.Controls.Add(this.nudWriteShortBitValue);
            this.tabPage1.Controls.Add(this.btnWriteShortValue);
            this.tabPage1.Controls.Add(this.btnWriteBitValue);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(724, 404);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "메모리 모니터";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Gainsboro;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(521, 215);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 22);
            this.label8.TabIndex = 88;
            this.label8.Text = "문자값";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Gainsboro;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Location = new System.Drawing.Point(521, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 22);
            this.label7.TabIndex = 88;
            this.label7.Text = "숫자값";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Gainsboro;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(521, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 22);
            this.label6.TabIndex = 88;
            this.label6.Text = "쓰기 주소";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Gainsboro;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(6, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 22);
            this.label5.TabIndex = 88;
            this.label5.Text = "시작 주소";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtWriteAsciiValue
            // 
            this.txtWriteAsciiValue.Location = new System.Drawing.Point(607, 214);
            this.txtWriteAsciiValue.Name = "txtWriteAsciiValue";
            this.txtWriteAsciiValue.Size = new System.Drawing.Size(111, 23);
            this.txtWriteAsciiValue.TabIndex = 3;
            this.txtWriteAsciiValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStartDevice_KeyPress);
            // 
            // btnWriteAsciiValue
            // 
            this.btnWriteAsciiValue.Location = new System.Drawing.Point(607, 240);
            this.btnWriteAsciiValue.Name = "btnWriteAsciiValue";
            this.btnWriteAsciiValue.Size = new System.Drawing.Size(111, 50);
            this.btnWriteAsciiValue.TabIndex = 1;
            this.btnWriteAsciiValue.Text = "문자열 쓰기";
            this.btnWriteAsciiValue.UseVisualStyleBackColor = true;
            this.btnWriteAsciiValue.Click += new System.EventHandler(this.btnWriteAsciiValue_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.dgvTagList);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(724, 404);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TAG 모니터";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(224, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(268, 30);
            this.label1.TabIndex = 89;
            this.label1.Text = "공유 메모리 주소는 대문자 \"S\"로 시작합니다.\r\nex) \"S10.1\" 공유 메모리 10번째 Byte의 1번 비트";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(5, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(301, 15);
            this.label2.TabIndex = 90;
            this.label2.Text = "자료형이 비트일 때, 입력값이 없으면 값이 토글됩니다.";
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(739, 559);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbStatus);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Dit Share Memory View";
            ((System.ComponentModel.ISupportInitialize)(this.nudShareMemSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWriteShortBitValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDevice)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTagList)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtShareMemName;
        private System.Windows.Forms.NumericUpDown nudShareMemSize;
        private System.Windows.Forms.TextBox txtStartDevice;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Button btnWriteShortValue;
        private System.Windows.Forms.NumericUpDown nudWriteShortBitValue;
        private System.Windows.Forms.TextBox txtWriteAddr;
        private System.Windows.Forms.DataGridView dgvDevice;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.Timer tmrState;
        private System.Windows.Forms.Button btnWriteBitValue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdoViewAscii;
        private System.Windows.Forms.RadioButton rdoViewInt32;
        private System.Windows.Forms.RadioButton rdoViewShort;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ch16;
        private System.Windows.Forms.DataGridViewTextBoxColumn ch15;
        private System.Windows.Forms.DataGridViewTextBoxColumn ch14;
        private System.Windows.Forms.DataGridViewTextBoxColumn ch12;
        private System.Windows.Forms.DataGridViewTextBoxColumn ch11;
        private System.Windows.Forms.DataGridViewTextBoxColumn ch10;
        private System.Windows.Forms.DataGridViewTextBoxColumn ch09;
        private System.Windows.Forms.DataGridViewTextBoxColumn ch08;
        private System.Windows.Forms.DataGridViewTextBoxColumn ch07;
        private System.Windows.Forms.DataGridViewTextBoxColumn ch06;
        private System.Windows.Forms.DataGridViewTextBoxColumn ch05;
        private System.Windows.Forms.DataGridViewTextBoxColumn ch04;
        private System.Windows.Forms.DataGridViewTextBoxColumn ch03;
        private System.Windows.Forms.DataGridViewTextBoxColumn ch02;
        private System.Windows.Forms.DataGridViewTextBoxColumn ch01;
        private System.Windows.Forms.DataGridViewTextBoxColumn ch00;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridView dgvTagList;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtWriteAsciiValue;
        private System.Windows.Forms.Button btnWriteAsciiValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn chSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn chDataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewButtonColumn chBtnCommand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

