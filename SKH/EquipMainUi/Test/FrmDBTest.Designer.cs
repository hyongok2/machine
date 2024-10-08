namespace EquipMainUi
{
    partial class FrmDBTest
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
            this.btnCstDeleteAll = new System.Windows.Forms.Button();
            this.btnCstInsert = new System.Windows.Forms.Button();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.tbCstLoadPort = new System.Windows.Forms.TextBox();
            this.tbCstCstID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCstSlotCount = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbWaferCstID = new System.Windows.Forms.TextBox();
            this.tbWaferNotch = new System.Windows.Forms.TextBox();
            this.btnWaferInsert = new System.Windows.Forms.Button();
            this.tbWaferSlot = new System.Windows.Forms.TextBox();
            this.btnWaferDeleteAll = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCstDeleteAll
            // 
            this.btnCstDeleteAll.Location = new System.Drawing.Point(187, 127);
            this.btnCstDeleteAll.Name = "btnCstDeleteAll";
            this.btnCstDeleteAll.Size = new System.Drawing.Size(75, 23);
            this.btnCstDeleteAll.TabIndex = 0;
            this.btnCstDeleteAll.Text = "Delete All";
            this.btnCstDeleteAll.UseVisualStyleBackColor = true;
            this.btnCstDeleteAll.Click += new System.EventHandler(this.btnCstDeleteAll_Click);
            // 
            // btnCstInsert
            // 
            this.btnCstInsert.Location = new System.Drawing.Point(86, 127);
            this.btnCstInsert.Name = "btnCstInsert";
            this.btnCstInsert.Size = new System.Drawing.Size(75, 23);
            this.btnCstInsert.TabIndex = 0;
            this.btnCstInsert.Text = "Insert";
            this.btnCstInsert.UseVisualStyleBackColor = true;
            this.btnCstInsert.Click += new System.EventHandler(this.btnCstInsert_Click);
            // 
            // lbLog
            // 
            this.lbLog.FormattingEnabled = true;
            this.lbLog.ItemHeight = 12;
            this.lbLog.Location = new System.Drawing.Point(12, 21);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(239, 196);
            this.lbLog.TabIndex = 1;
            // 
            // tbCstLoadPort
            // 
            this.tbCstLoadPort.Location = new System.Drawing.Point(81, 58);
            this.tbCstLoadPort.Name = "tbCstLoadPort";
            this.tbCstLoadPort.Size = new System.Drawing.Size(80, 21);
            this.tbCstLoadPort.TabIndex = 8;
            // 
            // tbCstCstID
            // 
            this.tbCstCstID.Location = new System.Drawing.Point(81, 29);
            this.tbCstCstID.Name = "tbCstCstID";
            this.tbCstCstID.Size = new System.Drawing.Size(80, 21);
            this.tbCstCstID.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "LoadPort";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "CassetteID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Slot Count";
            // 
            // tbCstSlotCount
            // 
            this.tbCstSlotCount.Location = new System.Drawing.Point(81, 88);
            this.tbCstSlotCount.Name = "tbCstSlotCount";
            this.tbCstSlotCount.Size = new System.Drawing.Size(80, 21);
            this.tbCstSlotCount.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbCstCstID);
            this.groupBox1.Controls.Add(this.tbCstSlotCount);
            this.groupBox1.Controls.Add(this.btnCstInsert);
            this.groupBox1.Controls.Add(this.tbCstLoadPort);
            this.groupBox1.Controls.Add(this.btnCstDeleteAll);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(266, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(522, 167);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cassette";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbWaferCstID);
            this.groupBox2.Controls.Add(this.tbWaferNotch);
            this.groupBox2.Controls.Add(this.btnWaferInsert);
            this.groupBox2.Controls.Add(this.tbWaferSlot);
            this.groupBox2.Controls.Add(this.btnWaferDeleteAll);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(267, 223);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(522, 167);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Wafer";
            // 
            // tbWaferCstID
            // 
            this.tbWaferCstID.Location = new System.Drawing.Point(81, 29);
            this.tbWaferCstID.Name = "tbWaferCstID";
            this.tbWaferCstID.Size = new System.Drawing.Size(80, 21);
            this.tbWaferCstID.TabIndex = 7;
            // 
            // tbWaferNotch
            // 
            this.tbWaferNotch.Location = new System.Drawing.Point(81, 88);
            this.tbWaferNotch.Name = "tbWaferNotch";
            this.tbWaferNotch.Size = new System.Drawing.Size(80, 21);
            this.tbWaferNotch.TabIndex = 8;
            // 
            // btnWaferInsert
            // 
            this.btnWaferInsert.Location = new System.Drawing.Point(86, 127);
            this.btnWaferInsert.Name = "btnWaferInsert";
            this.btnWaferInsert.Size = new System.Drawing.Size(75, 23);
            this.btnWaferInsert.TabIndex = 0;
            this.btnWaferInsert.Text = "Insert";
            this.btnWaferInsert.UseVisualStyleBackColor = true;
            this.btnWaferInsert.Click += new System.EventHandler(this.btnWaferInsert_Click);
            // 
            // tbWaferSlot
            // 
            this.tbWaferSlot.Location = new System.Drawing.Point(81, 58);
            this.tbWaferSlot.Name = "tbWaferSlot";
            this.tbWaferSlot.Size = new System.Drawing.Size(80, 21);
            this.tbWaferSlot.TabIndex = 8;
            // 
            // btnWaferDeleteAll
            // 
            this.btnWaferDeleteAll.Location = new System.Drawing.Point(186, 127);
            this.btnWaferDeleteAll.Name = "btnWaferDeleteAll";
            this.btnWaferDeleteAll.Size = new System.Drawing.Size(75, 23);
            this.btnWaferDeleteAll.TabIndex = 0;
            this.btnWaferDeleteAll.Text = "Delete All";
            this.btnWaferDeleteAll.UseVisualStyleBackColor = true;
            this.btnWaferDeleteAll.Click += new System.EventHandler(this.btnWaferDeleteAll_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "CassetteID";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "Notch";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "Slot";
            // 
            // FrmDBTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 406);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbLog);
            this.Name = "FrmDBTest";
            this.Text = "FrmDBTest";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCstDeleteAll;
        private System.Windows.Forms.Button btnCstInsert;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.TextBox tbCstLoadPort;
        private System.Windows.Forms.TextBox tbCstCstID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCstSlotCount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbWaferCstID;
        private System.Windows.Forms.TextBox tbWaferNotch;
        private System.Windows.Forms.Button btnWaferInsert;
        private System.Windows.Forms.TextBox tbWaferSlot;
        private System.Windows.Forms.Button btnWaferDeleteAll;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}