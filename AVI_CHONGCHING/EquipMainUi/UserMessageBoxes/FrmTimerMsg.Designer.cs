namespace EquipMainUi.UserMessageBoxes
{
    partial class FrmTimerMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTimerMsg));
            this.pBarRemained = new System.Windows.Forms.ProgressBar();
            this.lblRemainedTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // pBarRemained
            // 
            this.pBarRemained.Location = new System.Drawing.Point(12, 12);
            this.pBarRemained.Name = "pBarRemained";
            this.pBarRemained.Size = new System.Drawing.Size(191, 23);
            this.pBarRemained.TabIndex = 0;
            // 
            // lblRemainedTime
            // 
            this.lblRemainedTime.AutoSize = true;
            this.lblRemainedTime.Location = new System.Drawing.Point(216, 23);
            this.lblRemainedTime.Name = "lblRemainedTime";
            this.lblRemainedTime.Size = new System.Drawing.Size(23, 12);
            this.lblRemainedTime.TabIndex = 1;
            this.lblRemainedTime.Text = "130";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "s";
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.lblMsg.Location = new System.Drawing.Point(12, 44);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(260, 47);
            this.lblMsg.TabIndex = 3;
            this.lblMsg.Text = "msg";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmTimerMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 100);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblRemainedTime);
            this.Controls.Add(this.pBarRemained);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmTimerMsg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmTimerMsg";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTimerMsg_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pBarRemained;
        private System.Windows.Forms.Label lblRemainedTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Timer timer1;
    }
}