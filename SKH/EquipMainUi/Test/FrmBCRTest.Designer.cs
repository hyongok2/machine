namespace EquipMainUi.Test
{
    partial class FrmBCRTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBCRTest));
            this.btnScan = new System.Windows.Forms.Button();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(8, 38);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(186, 40);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "Read";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // lbLog
            // 
            this.lbLog.FormattingEnabled = true;
            this.lbLog.ItemHeight = 12;
            this.lbLog.Location = new System.Drawing.Point(8, 84);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(186, 184);
            this.lbLog.TabIndex = 6;
            // 
            // cbPort
            // 
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4"});
            this.cbPort.Location = new System.Drawing.Point(8, 12);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(89, 20);
            this.cbPort.TabIndex = 5;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(103, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(91, 20);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // FrmBCRTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(201, 280);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.cbPort);
            this.Controls.Add(this.btnConnect);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBCRTest";
            this.Text = "BCR Test";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.Button btnConnect;
    }
}