namespace EquipMainUi
{
    partial class FrmOperatorCall
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
            this.lblOpcallMsg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lstOperMsg = new System.Windows.Forms.ListView();
            this.ColumnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lblOpcallMsg
            // 
            this.lblOpcallMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOpcallMsg.BackColor = System.Drawing.Color.White;
            this.lblOpcallMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOpcallMsg.Font = new System.Drawing.Font("휴먼모음T", 8.25F);
            this.lblOpcallMsg.Location = new System.Drawing.Point(4, 43);
            this.lblOpcallMsg.Margin = new System.Windows.Forms.Padding(0);
            this.lblOpcallMsg.Name = "lblOpcallMsg";
            this.lblOpcallMsg.Size = new System.Drawing.Size(700, 79);
            this.lblOpcallMsg.TabIndex = 72;
            this.lblOpcallMsg.Text = "-";
            this.lblOpcallMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("휴먼모음T", 11.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(700, 33);
            this.label1.TabIndex = 71;
            this.label1.Text = "Operator Call";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstOperMsg
            // 
            this.lstOperMsg.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lstOperMsg.AutoArrange = false;
            this.lstOperMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lstOperMsg.BackgroundImageTiled = true;
            this.lstOperMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstOperMsg.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader6,
            this.ColumnHeader7,
            this.ColumnHeader10});
            this.lstOperMsg.Font = new System.Drawing.Font("휴먼모음T", 8.25F);
            this.lstOperMsg.ForeColor = System.Drawing.Color.White;
            this.lstOperMsg.FullRowSelect = true;
            this.lstOperMsg.GridLines = true;
            this.lstOperMsg.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstOperMsg.Location = new System.Drawing.Point(4, 125);
            this.lstOperMsg.MultiSelect = false;
            this.lstOperMsg.Name = "lstOperMsg";
            this.lstOperMsg.Size = new System.Drawing.Size(700, 245);
            this.lstOperMsg.TabIndex = 73;
            this.lstOperMsg.TileSize = new System.Drawing.Size(1, 1);
            this.lstOperMsg.UseCompatibleStateImageBehavior = false;
            this.lstOperMsg.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader6
            // 
            this.ColumnHeader6.Text = "Time";
            this.ColumnHeader6.Width = 70;
            // 
            // ColumnHeader7
            // 
            this.ColumnHeader7.Text = "TID";
            this.ColumnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ColumnHeader7.Width = 40;
            // 
            // ColumnHeader10
            // 
            this.ColumnHeader10.Text = "Message";
            this.ColumnHeader10.Width = 282;
            // 
            // FrmOperatorCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 374);
            this.Controls.Add(this.lstOperMsg);
            this.Controls.Add(this.lblOpcallMsg);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("휴먼모음T", 8.25F);
            this.Name = "FrmOperatorCall";
            this.Text = "Operator Call";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lblOpcallMsg;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ListView lstOperMsg;
        internal System.Windows.Forms.ColumnHeader ColumnHeader6;
        internal System.Windows.Forms.ColumnHeader ColumnHeader7;
        internal System.Windows.Forms.ColumnHeader ColumnHeader10;
    }
}