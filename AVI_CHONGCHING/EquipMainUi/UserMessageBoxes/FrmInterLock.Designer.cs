using Dit.Framework.UI.UserComponent;

namespace EquipMainUi
{
    partial class FrmInterLock
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
            this.txtInterlockMsg = new System.Windows.Forms.TextBox();
            this.txtDetailMsg = new System.Windows.Forms.TextBox();
            this.btnMainForward = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.label62 = new System.Windows.Forms.Label();
            this.panel11.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel11
            // 
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel11.Controls.Add(this.txtInterlockMsg);
            this.panel11.Controls.Add(this.txtDetailMsg);
            this.panel11.Controls.Add(this.btnMainForward);
            this.panel11.Controls.Add(this.label62);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(584, 562);
            this.panel11.TabIndex = 13;
            // 
            // txtInterlockMsg
            // 
            this.txtInterlockMsg.BackColor = System.Drawing.Color.White;
            this.txtInterlockMsg.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtInterlockMsg.Font = new System.Drawing.Font("Malgun Gothic", 20F);
            this.txtInterlockMsg.ForeColor = System.Drawing.Color.Red;
            this.txtInterlockMsg.Location = new System.Drawing.Point(0, 50);
            this.txtInterlockMsg.Multiline = true;
            this.txtInterlockMsg.Name = "txtInterlockMsg";
            this.txtInterlockMsg.ReadOnly = true;
            this.txtInterlockMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInterlockMsg.Size = new System.Drawing.Size(582, 283);
            this.txtInterlockMsg.TabIndex = 13;
            this.txtInterlockMsg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDetailMsg
            // 
            this.txtDetailMsg.BackColor = System.Drawing.Color.White;
            this.txtDetailMsg.Font = new System.Drawing.Font("Malgun Gothic", 14F);
            this.txtDetailMsg.ForeColor = System.Drawing.Color.Red;
            this.txtDetailMsg.Location = new System.Drawing.Point(3, 339);
            this.txtDetailMsg.Multiline = true;
            this.txtDetailMsg.Name = "txtDetailMsg";
            this.txtDetailMsg.ReadOnly = true;
            this.txtDetailMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetailMsg.Size = new System.Drawing.Size(582, 107);
            this.txtDetailMsg.TabIndex = 14;
            this.txtDetailMsg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnMainForward
            // 
            this.btnMainForward.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMainForward.BackColor = System.Drawing.Color.Transparent;
            this.btnMainForward.Delay = 200;
            this.btnMainForward.Flicker = false;
            this.btnMainForward.Font = new System.Drawing.Font("Malgun Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMainForward.ForeColor = System.Drawing.Color.Black;
            this.btnMainForward.IsLeftLampOn = false;
            this.btnMainForward.IsRightLampOn = false;
            this.btnMainForward.LampAliveTime = 500;
            this.btnMainForward.LampSize = 3;
            this.btnMainForward.LeftLampColor = System.Drawing.Color.Red;
            this.btnMainForward.Location = new System.Drawing.Point(-1, 502);
            this.btnMainForward.Name = "btnMainForward";
            this.btnMainForward.OnOff = false;
            this.btnMainForward.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnMainForward.Size = new System.Drawing.Size(583, 59);
            this.btnMainForward.TabIndex = 10;
            this.btnMainForward.TabStop = false;
            this.btnMainForward.Text = "확인";
            this.btnMainForward.Text2 = "";
            this.btnMainForward.UseVisualStyleBackColor = false;
            this.btnMainForward.VisibleLeftLamp = false;
            this.btnMainForward.VisibleRightLamp = false;
            this.btnMainForward.Click += new System.EventHandler(this.btnMainForward_Click);
            // 
            // label62
            // 
            this.label62.AutoEllipsis = true;
            this.label62.BackColor = System.Drawing.Color.Gainsboro;
            this.label62.Dock = System.Windows.Forms.DockStyle.Top;
            this.label62.Font = new System.Drawing.Font("Malgun Gothic", 15F);
            this.label62.ForeColor = System.Drawing.Color.Black;
            this.label62.Location = new System.Drawing.Point(0, 0);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(582, 50);
            this.label62.TabIndex = 9;
            this.label62.Text = "■ Interlock";
            this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmInterLock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.panel11);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInterLock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmInterLock";
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel11;
        internal ButtonDelay2 btnMainForward;
        internal System.Windows.Forms.Label label62;
        private System.Windows.Forms.TextBox txtInterlockMsg;
        private System.Windows.Forms.TextBox txtDetailMsg;
    }
}