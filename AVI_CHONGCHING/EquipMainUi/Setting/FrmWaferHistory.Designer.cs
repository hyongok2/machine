namespace EquipMainUi.Setting
{
    partial class FrmWaferHistory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lvHistory = new System.Windows.Forms.ListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblPage = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbBackupCount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbCycle = new System.Windows.Forms.TextBox();
            this.tbWaferID = new System.Windows.Forms.TextBox();
            this.tbCstID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.btnCst = new System.Windows.Forms.Button();
            this.btnPrePage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnWafer = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvHistory
            // 
            this.lvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvHistory.FullRowSelect = true;
            this.lvHistory.HideSelection = false;
            this.lvHistory.Location = new System.Drawing.Point(0, 0);
            this.lvHistory.Margin = new System.Windows.Forms.Padding(0);
            this.lvHistory.MultiSelect = false;
            this.lvHistory.Name = "lvHistory";
            this.lvHistory.Size = new System.Drawing.Size(936, 363);
            this.lvHistory.TabIndex = 1;
            this.lvHistory.UseCompatibleStateImageBehavior = false;
            this.lvHistory.View = System.Windows.Forms.View.Details;
            this.lvHistory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvHistory_MouseDoubleClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblPage);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.tbWaferID);
            this.splitContainer1.Panel1.Controls.Add(this.tbCstID);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.dtpEnd);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dtpStart);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(936, 511);
            this.splitContainer1.SplitterDistance = 106;
            this.splitContainer1.TabIndex = 2;
            // 
            // lblPage
            // 
            this.lblPage.AutoSize = true;
            this.lblPage.Location = new System.Drawing.Point(937, 72);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(31, 12);
            this.lblPage.TabIndex = 8;
            this.lblPage.Text = "0 / 0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.tbBackupCount);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tbCycle);
            this.groupBox1.Location = new System.Drawing.Point(589, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 75);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "자동 삭제";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(281, 14);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(55, 55);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbBackupCount
            // 
            this.tbBackupCount.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbBackupCount.Location = new System.Drawing.Point(217, 43);
            this.tbBackupCount.Margin = new System.Windows.Forms.Padding(0);
            this.tbBackupCount.Name = "tbBackupCount";
            this.tbBackupCount.Size = new System.Drawing.Size(61, 26);
            this.tbBackupCount.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(25, 20);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "저장 최대 일수";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(25, 48);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(167, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "저장 최대 카세트 개수";
            // 
            // tbCycle
            // 
            this.tbCycle.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbCycle.Location = new System.Drawing.Point(217, 14);
            this.tbCycle.Margin = new System.Windows.Forms.Padding(0);
            this.tbCycle.Name = "tbCycle";
            this.tbCycle.Size = new System.Drawing.Size(61, 26);
            this.tbCycle.TabIndex = 5;
            // 
            // tbWaferID
            // 
            this.tbWaferID.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbWaferID.Location = new System.Drawing.Point(486, 41);
            this.tbWaferID.Margin = new System.Windows.Forms.Padding(0);
            this.tbWaferID.Name = "tbWaferID";
            this.tbWaferID.Size = new System.Drawing.Size(100, 26);
            this.tbWaferID.TabIndex = 3;
            // 
            // tbCstID
            // 
            this.tbCstID.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbCstID.Location = new System.Drawing.Point(128, 41);
            this.tbCstID.Margin = new System.Windows.Forms.Padding(0);
            this.tbCstID.Name = "tbCstID";
            this.tbCstID.Size = new System.Drawing.Size(214, 26);
            this.tbCstID.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(370, 46);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "웨이퍼 아이디";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Checked = false;
            this.dtpEnd.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtpEnd.Location = new System.Drawing.Point(372, 12);
            this.dtpEnd.Margin = new System.Windows.Forms.Padding(0);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(214, 26);
            this.dtpEnd.TabIndex = 2;
            this.dtpEnd.CloseUp += new System.EventHandler(this.dtpStart_CloseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(12, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "카세트 아이디";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(12, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "검색 기간";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(349, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "~";
            // 
            // dtpStart
            // 
            this.dtpStart.Checked = false;
            this.dtpStart.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtpStart.Location = new System.Drawing.Point(128, 12);
            this.dtpStart.Margin = new System.Windows.Forms.Padding(0);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(214, 26);
            this.dtpStart.TabIndex = 0;
            this.dtpStart.CloseUp += new System.EventHandler(this.dtpStart_CloseUp);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lvHistory);
            this.splitContainer2.Size = new System.Drawing.Size(936, 401);
            this.splitContainer2.SplitterDistance = 34;
            this.splitContainer2.TabIndex = 1;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.btnCst);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.btnPrePage);
            this.splitContainer3.Panel2.Controls.Add(this.btnNextPage);
            this.splitContainer3.Panel2.Controls.Add(this.btnWafer);
            this.splitContainer3.Size = new System.Drawing.Size(936, 34);
            this.splitContainer3.SplitterDistance = 432;
            this.splitContainer3.TabIndex = 2;
            // 
            // btnCst
            // 
            this.btnCst.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCst.Location = new System.Drawing.Point(0, 0);
            this.btnCst.Margin = new System.Windows.Forms.Padding(0);
            this.btnCst.Name = "btnCst";
            this.btnCst.Size = new System.Drawing.Size(430, 34);
            this.btnCst.TabIndex = 1;
            this.btnCst.Text = "Cassette";
            this.btnCst.UseVisualStyleBackColor = true;
            this.btnCst.Click += new System.EventHandler(this.btnCst_Click);
            // 
            // btnPrePage
            // 
            this.btnPrePage.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPrePage.Location = new System.Drawing.Point(430, 0);
            this.btnPrePage.Name = "btnPrePage";
            this.btnPrePage.Size = new System.Drawing.Size(35, 34);
            this.btnPrePage.TabIndex = 10;
            this.btnPrePage.Text = "◁";
            this.btnPrePage.UseVisualStyleBackColor = true;
            this.btnPrePage.Click += new System.EventHandler(this.btnPrePage_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNextPage.Location = new System.Drawing.Point(465, 0);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(35, 34);
            this.btnNextPage.TabIndex = 9;
            this.btnNextPage.Text = "▷";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnWafer
            // 
            this.btnWafer.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnWafer.Location = new System.Drawing.Point(0, 0);
            this.btnWafer.Margin = new System.Windows.Forms.Padding(0);
            this.btnWafer.Name = "btnWafer";
            this.btnWafer.Size = new System.Drawing.Size(430, 34);
            this.btnWafer.TabIndex = 8;
            this.btnWafer.Text = "Wafer";
            this.btnWafer.UseVisualStyleBackColor = true;
            this.btnWafer.Click += new System.EventHandler(this.btnWafer_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.tmrUiUpdate);
            // 
            // FrmWaferHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 511);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmWaferHistory";
            this.Text = "물류 히스토리";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvHistory;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox tbWaferID;
        private System.Windows.Forms.TextBox tbCstID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbBackupCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbCycle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button btnCst;
        private System.Windows.Forms.Button btnWafer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPrePage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Timer timer1;
    }
}