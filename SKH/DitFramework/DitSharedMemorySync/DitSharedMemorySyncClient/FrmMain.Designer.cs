namespace DitSharedMemoryClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnReadTest = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnWriteTest = new System.Windows.Forms.Button();
            this.lstLogger = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstShMem = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.NofiyIconApp = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmnuApp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiShow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrState = new System.Windows.Forms.Timer(this.components);
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.tmrAutoStart = new System.Windows.Forms.Timer(this.components);
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.cmnuApp.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReadTest
            // 
            this.btnReadTest.Location = new System.Drawing.Point(597, 63);
            this.btnReadTest.Name = "btnReadTest";
            this.btnReadTest.Size = new System.Drawing.Size(93, 45);
            this.btnReadTest.TabIndex = 0;
            this.btnReadTest.Text = "READ TEST";
            this.btnReadTest.UseVisualStyleBackColor = true;
            this.btnReadTest.Click += new System.EventHandler(this.btnReadTest_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(498, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(93, 45);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "CONNECT";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnWriteTest
            // 
            this.btnWriteTest.Location = new System.Drawing.Point(597, 12);
            this.btnWriteTest.Name = "btnWriteTest";
            this.btnWriteTest.Size = new System.Drawing.Size(93, 45);
            this.btnWriteTest.TabIndex = 0;
            this.btnWriteTest.Text = "WRITE TEST";
            this.btnWriteTest.UseVisualStyleBackColor = true;
            // 
            // lstLogger
            // 
            this.lstLogger.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLogger.Location = new System.Drawing.Point(6, 227);
            this.lstLogger.Name = "lstLogger";
            this.lstLogger.Size = new System.Drawing.Size(646, 133);
            this.lstLogger.TabIndex = 3;
            this.lstLogger.UseCompatibleStateImageBehavior = false;
            this.lstLogger.View = System.Windows.Forms.View.Details;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lstShMem);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lstLogger);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(1, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(660, 365);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "클라이언트";
            // 
            // lstShMem
            // 
            this.lstShMem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstShMem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader10,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader13});
            this.lstShMem.FullRowSelect = true;
            this.lstShMem.Location = new System.Drawing.Point(5, 63);
            this.lstShMem.Name = "lstShMem";
            this.lstShMem.Size = new System.Drawing.Size(649, 135);
            this.lstShMem.TabIndex = 3;
            this.lstShMem.UseCompatibleStateImageBehavior = false;
            this.lstShMem.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 1;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "IP";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 90;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "방향";
            this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader10.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "공유 메모리 이름";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "사이즈";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 70;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "시작 위치";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "크기";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "연결 유무";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "싱크 횟수";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "싱크 평균";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "싱크 최소";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "싱크 최대";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(515, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "최소, 최대, 평균은 1초 기준";
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(244, 18);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(53, 41);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(185, 18);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(53, 41);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 41);
            this.label3.TabIndex = 0;
            this.label3.Text = "공유 메모리";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(6, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "LOG";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NofiyIconApp
            // 
            this.NofiyIconApp.ContextMenuStrip = this.cmnuApp;
            this.NofiyIconApp.Icon = ((System.Drawing.Icon)(resources.GetObject("NofiyIconApp.Icon")));
            this.NofiyIconApp.Text = "Dit Share Memory Sync Svr";
            this.NofiyIconApp.Visible = true;
            this.NofiyIconApp.DoubleClick += new System.EventHandler(this.NofiyIconApp_DoubleClick);
            // 
            // cmnuApp
            // 
            this.cmnuApp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShow,
            this.tsmiExit});
            this.cmnuApp.Name = "cmnuApp";
            this.cmnuApp.Size = new System.Drawing.Size(131, 52);
            // 
            // tsmiShow
            // 
            this.tsmiShow.Name = "tsmiShow";
            this.tsmiShow.Size = new System.Drawing.Size(130, 24);
            this.tsmiShow.Text = "열기(&O)";
            this.tsmiShow.Click += new System.EventHandler(this.tsmiShow_Click);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(130, 24);
            this.tsmiExit.Text = "종료(&E)";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tmrState
            // 
            this.tmrState.Enabled = true;
            this.tmrState.Interval = 1000;
            this.tmrState.Tick += new System.EventHandler(this.tmrState_Tick);
            // 
            // tbStatus
            // 
            this.tbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStatus.Location = new System.Drawing.Point(2, 370);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.ReadOnly = true;
            this.tbStatus.Size = new System.Drawing.Size(657, 26);
            this.tbStatus.TabIndex = 39;
            this.tbStatus.DoubleClick += new System.EventHandler(this.tbStatus_DoubleClick);
            // 
            // tmrAutoStart
            // 
            this.tmrAutoStart.Interval = 5000;
            this.tmrAutoStart.Tick += new System.EventHandler(this.tmrAutoStart_Tick);
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "초";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 394);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbStatus);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnWriteTest);
            this.Controls.Add(this.btnReadTest);
            this.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Text = "Dit Shared Memory Sync Client";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.cmnuApp.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReadTest;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnWriteTest;
        private System.Windows.Forms.ListView lstLogger;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lstShMem;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NotifyIcon NofiyIconApp;
        private System.Windows.Forms.ContextMenuStrip cmnuApp;
        private System.Windows.Forms.ToolStripMenuItem tsmiShow;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.Timer tmrState;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.Timer tmrAutoStart;
        private System.Windows.Forms.ColumnHeader columnHeader13;
    }
}

