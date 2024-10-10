namespace EquipMainUi.UserMessageBoxes
{
    partial class FrmTerminalMsg
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
            this.lstAutoDataLog = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.lstAutoDataLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAutoDataLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstAutoDataLog.FullRowSelect = true;
            this.lstAutoDataLog.GridLines = true;
            this.lstAutoDataLog.HideSelection = false;
            this.lstAutoDataLog.Location = new System.Drawing.Point(0, 420);
            this.lstAutoDataLog.MultiSelect = false;
            this.lstAutoDataLog.Name = "listView1";
            this.lstAutoDataLog.Size = new System.Drawing.Size(800, 126);
            this.lstAutoDataLog.TabIndex = 0;
            this.lstAutoDataLog.UseCompatibleStateImageBehavior = false;
            this.lstAutoDataLog.View = System.Windows.Forms.View.Details;
            this.lstAutoDataLog.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Time";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Message";
            this.columnHeader2.Width = 595;
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("맑은 고딕", 11.5F);
            this.lblMsg.Location = new System.Drawing.Point(0, 0);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(800, 417);
            this.lblMsg.TabIndex = 2;
            this.lblMsg.Text = "noMsg";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmTerminalMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 547);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lstAutoDataLog);
            this.Name = "FrmTerminalMsg";
            this.Text = "Terminal Message";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTerminalMsg_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.FrmTerminalMsg_VisibleChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstAutoDataLog;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label lblMsg;
    }
}