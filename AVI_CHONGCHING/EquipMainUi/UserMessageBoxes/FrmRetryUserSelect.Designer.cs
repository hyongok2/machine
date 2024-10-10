namespace EquipMainUi.UserMessageBoxes
{
    partial class FrmRetryUserSelect
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
            this.btnRetry = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnOut = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.lblMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRetry
            // 
            this.btnRetry.BackColor = System.Drawing.Color.Transparent;
            this.btnRetry.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRetry.Delay = 500;
            this.btnRetry.Flicker = false;
            this.btnRetry.Font = new System.Drawing.Font("맑은 고딕", 9.75F);
            this.btnRetry.ForeColor = System.Drawing.Color.Black;
            this.btnRetry.IsLeftLampOn = false;
            this.btnRetry.IsRightLampOn = false;
            this.btnRetry.LampAliveTime = 500;
            this.btnRetry.LampSize = 1;
            this.btnRetry.LeftLampColor = System.Drawing.Color.Red;
            this.btnRetry.Location = new System.Drawing.Point(3, 72);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.OnOff = false;
            this.btnRetry.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnRetry.Size = new System.Drawing.Size(136, 53);
            this.btnRetry.TabIndex = 85;
            this.btnRetry.Text = "재시도";
            this.btnRetry.Text2 = "";
            this.btnRetry.UseVisualStyleBackColor = false;
            this.btnRetry.VisibleLeftLamp = false;
            this.btnRetry.VisibleRightLamp = false;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // btnOut
            // 
            this.btnOut.BackColor = System.Drawing.Color.Transparent;
            this.btnOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOut.Delay = 500;
            this.btnOut.Flicker = false;
            this.btnOut.Font = new System.Drawing.Font("맑은 고딕", 9.75F);
            this.btnOut.ForeColor = System.Drawing.Color.Black;
            this.btnOut.IsLeftLampOn = false;
            this.btnOut.IsRightLampOn = false;
            this.btnOut.LampAliveTime = 500;
            this.btnOut.LampSize = 1;
            this.btnOut.LeftLampColor = System.Drawing.Color.Red;
            this.btnOut.Location = new System.Drawing.Point(139, 72);
            this.btnOut.Name = "btnOut";
            this.btnOut.OnOff = false;
            this.btnOut.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnOut.Size = new System.Drawing.Size(136, 53);
            this.btnOut.TabIndex = 85;
            this.btnOut.Text = "배출";
            this.btnOut.Text2 = "";
            this.btnOut.UseVisualStyleBackColor = false;
            this.btnOut.VisibleLeftLamp = false;
            this.btnOut.VisibleRightLamp = false;
            this.btnOut.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoEllipsis = true;
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMsg.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblMsg.ForeColor = System.Drawing.Color.Black;
            this.lblMsg.Location = new System.Drawing.Point(0, 0);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(278, 69);
            this.lblMsg.TabIndex = 86;
            this.lblMsg.Text = "...";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmRetryUserSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 129);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnOut);
            this.Controls.Add(this.btnRetry);
            this.Name = "FrmRetryUserSelect";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRetryUserSelect_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnRetry;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnOut;
        internal System.Windows.Forms.Label lblMsg;
    }
}