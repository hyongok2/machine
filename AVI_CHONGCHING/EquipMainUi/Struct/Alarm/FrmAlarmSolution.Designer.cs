namespace EquipMainUi.Struct
{
    partial class FrmAlarmSolution
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
            this.listAlarm = new System.Windows.Forms.ListView();
            this.colIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAlarmName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbCause = new System.Windows.Forms.RichTextBox();
            this.tbAction = new System.Windows.Forms.RichTextBox();
            this.btnModify = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbMemo = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // listAlarm
            // 
            this.listAlarm.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIndex,
            this.colAlarmName});
            this.listAlarm.FullRowSelect = true;
            this.listAlarm.HideSelection = false;
            this.listAlarm.Location = new System.Drawing.Point(12, 12);
            this.listAlarm.Name = "listAlarm";
            this.listAlarm.Size = new System.Drawing.Size(944, 294);
            this.listAlarm.TabIndex = 0;
            this.listAlarm.UseCompatibleStateImageBehavior = false;
            this.listAlarm.View = System.Windows.Forms.View.Details;
            this.listAlarm.SelectedIndexChanged += new System.EventHandler(this.listAlarm_SelectedIndexChanged);
            // 
            // colIndex
            // 
            this.colIndex.Text = "번호";
            this.colIndex.Width = 53;
            // 
            // colAlarmName
            // 
            this.colAlarmName.Text = "알람명";
            this.colAlarmName.Width = 844;
            // 
            // tbCause
            // 
            this.tbCause.Enabled = false;
            this.tbCause.Location = new System.Drawing.Point(8, 20);
            this.tbCause.Name = "tbCause";
            this.tbCause.Size = new System.Drawing.Size(638, 62);
            this.tbCause.TabIndex = 2;
            this.tbCause.Text = "";
            // 
            // tbAction
            // 
            this.tbAction.Enabled = false;
            this.tbAction.Location = new System.Drawing.Point(8, 17);
            this.tbAction.Name = "tbAction";
            this.tbAction.Size = new System.Drawing.Size(638, 96);
            this.tbAction.TabIndex = 3;
            this.tbAction.Text = "";
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(881, 488);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(75, 37);
            this.btnModify.TabIndex = 1;
            this.btnModify.Text = "수정";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbCause);
            this.groupBox1.Location = new System.Drawing.Point(12, 316);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(654, 90);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "발생 원인";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbAction);
            this.groupBox2.Location = new System.Drawing.Point(12, 412);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(654, 123);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "조치 방법";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbMemo);
            this.groupBox3.Location = new System.Drawing.Point(672, 316);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(284, 166);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "메모";
            // 
            // tbMemo
            // 
            this.tbMemo.Location = new System.Drawing.Point(8, 20);
            this.tbMemo.Name = "tbMemo";
            this.tbMemo.Size = new System.Drawing.Size(270, 140);
            this.tbMemo.TabIndex = 2;
            this.tbMemo.Text = "";
            this.tbMemo.Click += new System.EventHandler(this.tbMemo_Click);
            // 
            // FrmAlarmSolution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 551);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.listAlarm);
            this.Name = "FrmAlarmSolution";
            this.Text = "Alarm Solutioin";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listAlarm;
        private System.Windows.Forms.ColumnHeader colIndex;
        private System.Windows.Forms.ColumnHeader colAlarmName;
        private System.Windows.Forms.RichTextBox tbCause;
        private System.Windows.Forms.RichTextBox tbAction;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox tbMemo;
    }
}