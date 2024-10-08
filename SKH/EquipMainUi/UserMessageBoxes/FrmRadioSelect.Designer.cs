namespace EquipMainUi.UserMessageBoxes
{
    partial class FrmRadioSelect
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.rdUnselect = new System.Windows.Forms.RadioButton();
            this.lblMainMsg = new System.Windows.Forms.Label();
            this.btnOK = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnCancel = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.lblSelectedIndex = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSelectedIndex);
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(15, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "선택사항";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.rdUnselect);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(439, 80);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // rdUnselect
            // 
            this.rdUnselect.AutoSize = true;
            this.rdUnselect.Checked = true;
            this.rdUnselect.Location = new System.Drawing.Point(6, 6);
            this.rdUnselect.Name = "rdUnselect";
            this.rdUnselect.Size = new System.Drawing.Size(59, 16);
            this.rdUnselect.TabIndex = 0;
            this.rdUnselect.TabStop = true;
            this.rdUnselect.Tag = "-1";
            this.rdUnselect.Text = "미선택";
            this.rdUnselect.UseVisualStyleBackColor = true;
            this.rdUnselect.Click += new System.EventHandler(this.SomeRadioButton_Click);
            // 
            // lblMainMsg
            // 
            this.lblMainMsg.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblMainMsg.Location = new System.Drawing.Point(12, 9);
            this.lblMainMsg.Name = "lblMainMsg";
            this.lblMainMsg.Size = new System.Drawing.Size(448, 69);
            this.lblMainMsg.TabIndex = 112;
            this.lblMainMsg.Text = "메시지";
            this.lblMainMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.Delay = 100;
            this.btnOK.Flicker = false;
            this.btnOK.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.IsLeftLampOn = false;
            this.btnOK.IsRightLampOn = false;
            this.btnOK.LampAliveTime = 500;
            this.btnOK.LampSize = 3;
            this.btnOK.LeftLampColor = System.Drawing.Color.Red;
            this.btnOK.Location = new System.Drawing.Point(12, 187);
            this.btnOK.Name = "btnOK";
            this.btnOK.OnOff = false;
            this.btnOK.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnOK.Size = new System.Drawing.Size(131, 59);
            this.btnOK.TabIndex = 113;
            this.btnOK.TabStop = false;
            this.btnOK.Text = "확인";
            this.btnOK.Text2 = "";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.VisibleLeftLamp = false;
            this.btnOK.VisibleRightLamp = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.Delay = 100;
            this.btnCancel.Flicker = false;
            this.btnCancel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.IsLeftLampOn = false;
            this.btnCancel.IsRightLampOn = false;
            this.btnCancel.LampAliveTime = 500;
            this.btnCancel.LampSize = 3;
            this.btnCancel.LeftLampColor = System.Drawing.Color.Red;
            this.btnCancel.Location = new System.Drawing.Point(329, 187);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OnOff = false;
            this.btnCancel.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnCancel.Size = new System.Drawing.Size(131, 59);
            this.btnCancel.TabIndex = 113;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "취소";
            this.btnCancel.Text2 = "";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.VisibleLeftLamp = false;
            this.btnCancel.VisibleRightLamp = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblSelectedIndex
            // 
            this.lblSelectedIndex.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSelectedIndex.Location = new System.Drawing.Point(401, -3);
            this.lblSelectedIndex.Margin = new System.Windows.Forms.Padding(0);
            this.lblSelectedIndex.Name = "lblSelectedIndex";
            this.lblSelectedIndex.Size = new System.Drawing.Size(41, 24);
            this.lblSelectedIndex.TabIndex = 114;
            this.lblSelectedIndex.Text = "-1";
            this.lblSelectedIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmRadioSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 250);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblMainMsg);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmRadioSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "선택";
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton rdUnselect;
        private System.Windows.Forms.Label lblMainMsg;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnOK;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnCancel;
        private System.Windows.Forms.Label lblSelectedIndex;
    }
}