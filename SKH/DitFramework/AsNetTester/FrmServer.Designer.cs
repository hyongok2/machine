namespace AsNetTester
{
    partial class FrmServer
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
            this.lblIP = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtRecv = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblIP
            // 
            this.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIP.Location = new System.Drawing.Point(10, 23);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(72, 21);
            this.lblIP.TabIndex = 11;
            this.lblIP.Text = "IP/PORT";
            this.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(189, 22);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(99, 22);
            this.txtPort.TabIndex = 10;
            this.txtPort.Text = "5000";
            this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(88, 22);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(99, 22);
            this.txtIp.TabIndex = 9;
            this.txtIp.Text = "127.0.0.1";
            this.txtIp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(291, 11);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(80, 33);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(10, 52);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(278, 22);
            this.txtSend.TabIndex = 9;
            this.txtSend.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(291, 51);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(80, 76);
            this.btnSend.TabIndex = 12;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtRecv
            // 
            this.txtRecv.Location = new System.Drawing.Point(10, 80);
            this.txtRecv.Multiline = true;
            this.txtRecv.Name = "txtRecv";
            this.txtRecv.Size = new System.Drawing.Size(278, 292);
            this.txtRecv.TabIndex = 9;
            // 
            // FrmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 375);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtRecv);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("맑은 고딕", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "FrmServer";
            this.Text = "서버";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtRecv;

    }
}