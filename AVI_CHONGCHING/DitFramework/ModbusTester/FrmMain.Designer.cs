namespace ModbusTester
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
            this.btnConnectTcp = new System.Windows.Forms.Button();
            this.btnReadDiscreteInputs = new System.Windows.Forms.Button();
            this.btnReadWrite = new System.Windows.Forms.Button();
            this.btnConnectRTU = new System.Windows.Forms.Button();
            this.tmWorker = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnZW7000TOpen = new System.Windows.Forms.Button();
            this.btnZW7000TGetData = new System.Windows.Forms.Button();
            this.txtZW7000TData = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnectTcp
            // 
            this.btnConnectTcp.Location = new System.Drawing.Point(6, 6);
            this.btnConnectTcp.Name = "btnConnectTcp";
            this.btnConnectTcp.Size = new System.Drawing.Size(115, 48);
            this.btnConnectTcp.TabIndex = 0;
            this.btnConnectTcp.Text = "Connect(TCP)";
            this.btnConnectTcp.UseVisualStyleBackColor = true;
            this.btnConnectTcp.Click += new System.EventHandler(this.btnConnectTcp_Click);
            // 
            // btnReadDiscreteInputs
            // 
            this.btnReadDiscreteInputs.Location = new System.Drawing.Point(6, 69);
            this.btnReadDiscreteInputs.Name = "btnReadDiscreteInputs";
            this.btnReadDiscreteInputs.Size = new System.Drawing.Size(115, 48);
            this.btnReadDiscreteInputs.TabIndex = 0;
            this.btnReadDiscreteInputs.Text = "ReadDiscreteInputs";
            this.btnReadDiscreteInputs.UseVisualStyleBackColor = true;
            this.btnReadDiscreteInputs.Click += new System.EventHandler(this.btnReadDiscreteInputs_Click);
            // 
            // btnReadWrite
            // 
            this.btnReadWrite.Location = new System.Drawing.Point(6, 153);
            this.btnReadWrite.Name = "btnReadWrite";
            this.btnReadWrite.Size = new System.Drawing.Size(115, 48);
            this.btnReadWrite.TabIndex = 0;
            this.btnReadWrite.Text = "Read Write";
            this.btnReadWrite.UseVisualStyleBackColor = true;
            this.btnReadWrite.Click += new System.EventHandler(this.btnReadWrite_Click);
            // 
            // btnConnectRTU
            // 
            this.btnConnectRTU.Location = new System.Drawing.Point(127, 6);
            this.btnConnectRTU.Name = "btnConnectRTU";
            this.btnConnectRTU.Size = new System.Drawing.Size(115, 48);
            this.btnConnectRTU.TabIndex = 0;
            this.btnConnectRTU.Text = "Connect(RTU)";
            this.btnConnectRTU.UseVisualStyleBackColor = true;
            this.btnConnectRTU.Click += new System.EventHandler(this.btnConnectRTU_Click);
            // 
            // tmWorker
            // 
            this.tmWorker.Tick += new System.EventHandler(this.tmWorker_Tick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(318, 145);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(131, 22);
            this.textBox1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(574, 409);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnConnectTcp);
            this.tabPage1.Controls.Add(this.btnConnectRTU);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.btnReadDiscreteInputs);
            this.tabPage1.Controls.Add(this.btnReadWrite);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(566, 383);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtZW7000TData);
            this.tabPage2.Controls.Add(this.btnZW7000TGetData);
            this.tabPage2.Controls.Add(this.btnZW7000TOpen);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(566, 383);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ZW7000T";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnZW7000TOpen
            // 
            this.btnZW7000TOpen.Location = new System.Drawing.Point(8, 8);
            this.btnZW7000TOpen.Name = "btnZW7000TOpen";
            this.btnZW7000TOpen.Size = new System.Drawing.Size(92, 23);
            this.btnZW7000TOpen.TabIndex = 0;
            this.btnZW7000TOpen.Text = "Create&&Open";
            this.btnZW7000TOpen.UseVisualStyleBackColor = true;
            this.btnZW7000TOpen.Click += new System.EventHandler(this.btnZW7000TOpen_Click);
            // 
            // btnZW7000TGetData
            // 
            this.btnZW7000TGetData.Location = new System.Drawing.Point(106, 8);
            this.btnZW7000TGetData.Name = "btnZW7000TGetData";
            this.btnZW7000TGetData.Size = new System.Drawing.Size(75, 23);
            this.btnZW7000TGetData.TabIndex = 0;
            this.btnZW7000TGetData.Text = "GetData";
            this.btnZW7000TGetData.UseVisualStyleBackColor = true;
            this.btnZW7000TGetData.Click += new System.EventHandler(this.btnZW7000TGetData_Click);
            // 
            // txtZW7000TData
            // 
            this.txtZW7000TData.Location = new System.Drawing.Point(187, 9);
            this.txtZW7000TData.Name = "txtZW7000TData";
            this.txtZW7000TData.Size = new System.Drawing.Size(100, 22);
            this.txtZW7000TData.TabIndex = 1;
            // 
            // FrmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(574, 409);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "FrmMain";
            this.Text = "Modbus Tester";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnectTcp;
        private System.Windows.Forms.Button btnReadDiscreteInputs;
        private System.Windows.Forms.Button btnReadWrite;
        private System.Windows.Forms.Button btnConnectRTU;
        private System.Windows.Forms.Timer tmWorker;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnZW7000TOpen;
        private System.Windows.Forms.Button btnZW7000TGetData;
        private System.Windows.Forms.TextBox txtZW7000TData;
    }
}

