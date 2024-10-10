namespace EquipMainUi
{
    partial class FrmCheck
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
            this.panel11 = new System.Windows.Forms.Panel();
            this.lblCheckMsg = new System.Windows.Forms.Label();
            this.btnMainForward = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.lblCheckState = new System.Windows.Forms.Label();
            this.panel11.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel11
            // 
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel11.Controls.Add(this.lblCheckMsg);
            this.panel11.Controls.Add(this.btnMainForward);
            this.panel11.Controls.Add(this.lblCheckState);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(584, 562);
            this.panel11.TabIndex = 13;
            // 
            // lblCheckMsg
            // 
            this.lblCheckMsg.AutoEllipsis = true;
            this.lblCheckMsg.BackColor = System.Drawing.Color.White;
            this.lblCheckMsg.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCheckMsg.Font = new System.Drawing.Font("맑은 고딕", 18F);
            this.lblCheckMsg.ForeColor = System.Drawing.Color.Red;
            this.lblCheckMsg.Location = new System.Drawing.Point(0, 50);
            this.lblCheckMsg.Name = "lblCheckMsg";
            this.lblCheckMsg.Size = new System.Drawing.Size(582, 450);
            this.lblCheckMsg.TabIndex = 11;
            this.lblCheckMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMainForward
            // 
            this.btnMainForward.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMainForward.BackColor = System.Drawing.Color.Transparent;
            this.btnMainForward.Delay = 2;
            this.btnMainForward.Flicker = false;
            this.btnMainForward.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMainForward.ForeColor = System.Drawing.Color.Black;
            this.btnMainForward.Location = new System.Drawing.Point(0, 500);
            this.btnMainForward.Name = "btnMainForward";
            this.btnMainForward.OnOff = false;
            this.btnMainForward.Size = new System.Drawing.Size(583, 60);
            this.btnMainForward.TabIndex = 10;
            this.btnMainForward.TabStop = false;
            this.btnMainForward.Text = "확인";
            this.btnMainForward.Text2 = "";
            this.btnMainForward.UseVisualStyleBackColor = false;
            this.btnMainForward.Click += new System.EventHandler(this.btnMainForward_Click);
            // 
            // lblCheckState
            // 
            this.lblCheckState.AutoEllipsis = true;
            this.lblCheckState.BackColor = System.Drawing.Color.Gainsboro;
            this.lblCheckState.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCheckState.Font = new System.Drawing.Font("맑은 고딕", 15F);
            this.lblCheckState.ForeColor = System.Drawing.Color.Black;
            this.lblCheckState.Location = new System.Drawing.Point(0, 0);
            this.lblCheckState.Name = "lblCheckState";
            this.lblCheckState.Size = new System.Drawing.Size(582, 50);
            this.lblCheckState.TabIndex = 9;
            this.lblCheckState.Text = "■ 성공";
            this.lblCheckState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.panel11);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmInterLock";
            this.panel11.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel11;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnMainForward;
        internal System.Windows.Forms.Label lblCheckState;
        internal System.Windows.Forms.Label lblCheckMsg;
    }
}