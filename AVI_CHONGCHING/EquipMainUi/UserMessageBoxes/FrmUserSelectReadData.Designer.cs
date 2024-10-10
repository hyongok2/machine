namespace EquipMainUi.UserMessageBoxes
{
    partial class FrmUserSelectReadData
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
            this.groupBoxRead1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInput1 = new System.Windows.Forms.TextBox();
            this.txtRead2 = new System.Windows.Forms.TextBox();
            this.txtRead1 = new System.Windows.Forms.TextBox();
            this.rdUseInput = new System.Windows.Forms.RadioButton();
            this.rdUseReadData2 = new System.Windows.Forms.RadioButton();
            this.rdUseReadData1 = new System.Windows.Forms.RadioButton();
            this.lblNotify = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tmrUiUpdate = new System.Windows.Forms.Timer(this.components);
            this.lblCaution = new System.Windows.Forms.Label();
            this.groupBoxRead1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxRead1
            // 
            this.groupBoxRead1.Controls.Add(this.label2);
            this.groupBoxRead1.Controls.Add(this.label1);
            this.groupBoxRead1.Controls.Add(this.txtInput1);
            this.groupBoxRead1.Controls.Add(this.txtRead2);
            this.groupBoxRead1.Controls.Add(this.txtRead1);
            this.groupBoxRead1.Controls.Add(this.rdUseInput);
            this.groupBoxRead1.Controls.Add(this.rdUseReadData2);
            this.groupBoxRead1.Controls.Add(this.rdUseReadData1);
            this.groupBoxRead1.Location = new System.Drawing.Point(0, 66);
            this.groupBoxRead1.Name = "groupBoxRead1";
            this.groupBoxRead1.Size = new System.Drawing.Size(438, 99);
            this.groupBoxRead1.TabIndex = 0;
            this.groupBoxRead1.TabStop = false;
            this.groupBoxRead1.Text = "   ";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DarkGray;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 21);
            this.label2.TabIndex = 91;
            this.label2.Text = "2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkGray;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 21);
            this.label1.TabIndex = 91;
            this.label1.Text = "1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtInput1
            // 
            this.txtInput1.Location = new System.Drawing.Point(235, 42);
            this.txtInput1.Name = "txtInput1";
            this.txtInput1.Size = new System.Drawing.Size(191, 21);
            this.txtInput1.TabIndex = 5;
            // 
            // txtRead2
            // 
            this.txtRead2.Enabled = false;
            this.txtRead2.Location = new System.Drawing.Point(42, 69);
            this.txtRead2.Name = "txtRead2";
            this.txtRead2.ReadOnly = true;
            this.txtRead2.Size = new System.Drawing.Size(191, 21);
            this.txtRead2.TabIndex = 4;
            // 
            // txtRead1
            // 
            this.txtRead1.Enabled = false;
            this.txtRead1.Location = new System.Drawing.Point(42, 42);
            this.txtRead1.Name = "txtRead1";
            this.txtRead1.ReadOnly = true;
            this.txtRead1.Size = new System.Drawing.Size(191, 21);
            this.txtRead1.TabIndex = 3;
            this.txtRead1.Text = " ";
            // 
            // rdUseInput
            // 
            this.rdUseInput.AutoSize = true;
            this.rdUseInput.Location = new System.Drawing.Point(239, 20);
            this.rdUseInput.Name = "rdUseInput";
            this.rdUseInput.Size = new System.Drawing.Size(71, 16);
            this.rdUseInput.TabIndex = 1;
            this.rdUseInput.Text = "직접입력";
            this.rdUseInput.UseVisualStyleBackColor = true;
            // 
            // rdUseReadData2
            // 
            this.rdUseReadData2.AutoSize = true;
            this.rdUseReadData2.Location = new System.Drawing.Point(142, 20);
            this.rdUseReadData2.Name = "rdUseReadData2";
            this.rdUseReadData2.Size = new System.Drawing.Size(91, 16);
            this.rdUseReadData2.TabIndex = 0;
            this.rdUseReadData2.Text = "Read Data 2";
            this.rdUseReadData2.UseVisualStyleBackColor = true;
            // 
            // rdUseReadData1
            // 
            this.rdUseReadData1.AutoSize = true;
            this.rdUseReadData1.Checked = true;
            this.rdUseReadData1.Location = new System.Drawing.Point(45, 20);
            this.rdUseReadData1.Name = "rdUseReadData1";
            this.rdUseReadData1.Size = new System.Drawing.Size(91, 16);
            this.rdUseReadData1.TabIndex = 0;
            this.rdUseReadData1.TabStop = true;
            this.rdUseReadData1.Text = "Read Data 1";
            this.rdUseReadData1.UseVisualStyleBackColor = true;
            // 
            // lblNotify
            // 
            this.lblNotify.BackColor = System.Drawing.Color.DarkGray;
            this.lblNotify.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNotify.Location = new System.Drawing.Point(12, 168);
            this.lblNotify.Name = "lblNotify";
            this.lblNotify.Size = new System.Drawing.Size(414, 20);
            this.lblNotify.TabIndex = 89;
            this.lblNotify.Text = "Error";
            this.lblNotify.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 231);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(414, 33);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "확인";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(299, 191);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(127, 34);
            this.btnRead.TabIndex = 6;
            this.btnRead.Text = "다시읽기";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.DarkGray;
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(438, 28);
            this.lblTitle.TabIndex = 88;
            this.lblTitle.Text = "title";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrUiUpdate
            // 
            this.tmrUiUpdate.Enabled = true;
            this.tmrUiUpdate.Interval = 300;
            this.tmrUiUpdate.Tick += new System.EventHandler(this.tmrUiUpdate_Tick);
            // 
            // lblCaution
            // 
            this.lblCaution.BackColor = System.Drawing.Color.DarkGray;
            this.lblCaution.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCaution.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCaution.Location = new System.Drawing.Point(0, 28);
            this.lblCaution.Name = "lblCaution";
            this.lblCaution.Size = new System.Drawing.Size(438, 28);
            this.lblCaution.TabIndex = 90;
            this.lblCaution.Text = "title";
            this.lblCaution.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmUserSelectReadData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 269);
            this.ControlBox = false;
            this.Controls.Add(this.lblCaution);
            this.Controls.Add(this.lblNotify);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBoxRead1);
            this.Name = "FrmUserSelectReadData";
            this.Text = "사용자 선택";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmUserSelectReadData_FormClosing);
            this.groupBoxRead1.ResumeLayout(false);
            this.groupBoxRead1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxRead1;
        private System.Windows.Forms.TextBox txtInput1;
        private System.Windows.Forms.TextBox txtRead2;
        private System.Windows.Forms.TextBox txtRead1;
        private System.Windows.Forms.RadioButton rdUseInput;
        private System.Windows.Forms.RadioButton rdUseReadData1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblNotify;
        private System.Windows.Forms.Timer tmrUiUpdate;
        private System.Windows.Forms.Label lblCaution;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdUseReadData2;
    }
}