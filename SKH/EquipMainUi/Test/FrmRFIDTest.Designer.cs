namespace EquipMainUi
{
    partial class FrmRFIDTest
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnScan1 = new System.Windows.Forms.Button();
            this.btnScan2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(106, 29);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(83, 20);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cbPort
            // 
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4"});
            this.cbPort.Location = new System.Drawing.Point(11, 29);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(89, 20);
            this.cbPort.TabIndex = 1;
            // 
            // lbLog
            // 
            this.lbLog.FormattingEnabled = true;
            this.lbLog.ItemHeight = 12;
            this.lbLog.Location = new System.Drawing.Point(11, 136);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(396, 184);
            this.lbLog.TabIndex = 2;
            // 
            // btnScan1
            // 
            this.btnScan1.Location = new System.Drawing.Point(239, 29);
            this.btnScan1.Name = "btnScan1";
            this.btnScan1.Size = new System.Drawing.Size(75, 61);
            this.btnScan1.TabIndex = 0;
            this.btnScan1.Text = "Reader1 Scan";
            this.btnScan1.UseVisualStyleBackColor = true;
            this.btnScan1.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnScan2
            // 
            //this.btnScan2.Location = new System.Drawing.Point(320, 29); //RFID 제거
            //this.btnScan2.Name = "btnScan2";
            //this.btnScan2.Size = new System.Drawing.Size(75, 61);
            //this.btnScan2.TabIndex = 0;
            //this.btnScan2.Text = "Reader2 Scan";
            //this.btnScan2.UseVisualStyleBackColor = true;
            //this.btnScan2.Click += new System.EventHandler(this.btnScan2_Click);
            // 
            // FrmRFIDTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 362);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.cbPort);
            this.Controls.Add(this.btnScan2);
            this.Controls.Add(this.btnScan1);
            this.Controls.Add(this.btnConnect);
            this.Name = "FrmRFIDTest";
            this.Text = "FrmRFIDTest";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnScan1;
        private System.Windows.Forms.Button btnScan2;
    }
}