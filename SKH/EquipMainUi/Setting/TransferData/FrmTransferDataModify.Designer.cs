namespace EquipMainUi.Setting.TransferData
{
    partial class FrmTransferDataModify
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
            this.pGridInfo = new System.Windows.Forms.PropertyGrid();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lvCurWaferList = new System.Windows.Forms.ListView();
            this.colCstID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSlot = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // pGridInfo
            // 
            this.pGridInfo.BackColor = System.Drawing.SystemColors.Control;
            this.pGridInfo.HelpVisible = false;
            this.pGridInfo.Location = new System.Drawing.Point(230, 12);
            this.pGridInfo.Name = "pGridInfo";
            this.pGridInfo.Size = new System.Drawing.Size(425, 450);
            this.pGridInfo.TabIndex = 4;
            this.pGridInfo.ToolbarVisible = false;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSave.Location = new System.Drawing.Point(137, 502);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(147, 38);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCancel.Location = new System.Drawing.Point(362, 502);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(147, 38);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "취소";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lvCurWaferList
            // 
            this.lvCurWaferList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCstID,
            this.colSlot});
            this.lvCurWaferList.FullRowSelect = true;
            this.lvCurWaferList.Location = new System.Drawing.Point(12, 12);
            this.lvCurWaferList.Name = "lvCurWaferList";
            this.lvCurWaferList.Size = new System.Drawing.Size(212, 450);
            this.lvCurWaferList.TabIndex = 6;
            this.lvCurWaferList.UseCompatibleStateImageBehavior = false;
            this.lvCurWaferList.View = System.Windows.Forms.View.Details;
            this.lvCurWaferList.SelectedIndexChanged += new System.EventHandler(this.lvWaferList_SelectedIndexChanged);
            // 
            // colCstID
            // 
            this.colCstID.Text = "카세트 아이디";
            this.colCstID.Width = 100;
            // 
            // colSlot
            // 
            this.colSlot.Text = "슬롯 넘버";
            this.colSlot.Width = 100;
            // 
            // FrmTransferDataModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 584);
            this.Controls.Add(this.lvCurWaferList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pGridInfo);
            this.Name = "FrmTransferDataModify";
            this.Text = "FrmTransferDataModify";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pGridInfo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListView lvCurWaferList;
        private System.Windows.Forms.ColumnHeader colCstID;
        private System.Windows.Forms.ColumnHeader colSlot;
    }
}