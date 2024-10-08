namespace EquipMainUi.Setting
{
    partial class FrmLogin
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
            this.cboLevel = new System.Windows.Forms.ComboBox();
            this.lblNameTitle = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblNotice = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDoSomething = new System.Windows.Forms.Button();
            this.lblPwTitle = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txtPw = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cboLevel
            // 
            this.cboLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLevel.FormattingEnabled = true;
            this.cboLevel.Location = new System.Drawing.Point(0, 166);
            this.cboLevel.Name = "cboLevel";
            this.cboLevel.Size = new System.Drawing.Size(284, 20);
            this.cboLevel.TabIndex = 122;
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.AutoEllipsis = true;
            this.lblNameTitle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblNameTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNameTitle.Font = new System.Drawing.Font("맑은 고딕", 14F);
            this.lblNameTitle.ForeColor = System.Drawing.Color.Black;
            this.lblNameTitle.Location = new System.Drawing.Point(0, 133);
            this.lblNameTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblNameTitle.Name = "lblNameTitle";
            this.lblNameTitle.Size = new System.Drawing.Size(144, 32);
            this.lblNameTitle.TabIndex = 121;
            this.lblNameTitle.Text = "Name";
            this.lblNameTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("맑은 고딕", 14F);
            this.txtName.Location = new System.Drawing.Point(144, 133);
            this.txtName.Margin = new System.Windows.Forms.Padding(0);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(140, 32);
            this.txtName.TabIndex = 2;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPw_KeyDown);
            // 
            // lblNotice
            // 
            this.lblNotice.BackColor = System.Drawing.Color.Gray;
            this.lblNotice.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNotice.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lblNotice.Location = new System.Drawing.Point(0, 0);
            this.lblNotice.Name = "lblNotice";
            this.lblNotice.Size = new System.Drawing.Size(284, 49);
            this.lblNotice.TabIndex = 119;
            this.lblNotice.Text = "message";
            this.lblNotice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Gray;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(144, 197);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 61);
            this.btnCancel.TabIndex = 116;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnDoSomething
            // 
            this.btnDoSomething.BackColor = System.Drawing.Color.Gray;
            this.btnDoSomething.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDoSomething.ForeColor = System.Drawing.Color.Black;
            this.btnDoSomething.Location = new System.Drawing.Point(0, 197);
            this.btnDoSomething.Name = "btnDoSomething";
            this.btnDoSomething.Size = new System.Drawing.Size(140, 61);
            this.btnDoSomething.TabIndex = 3;
            this.btnDoSomething.Text = "Login";
            this.btnDoSomething.UseVisualStyleBackColor = false;
            this.btnDoSomething.Click += new System.EventHandler(this.btnDoSomething_Click);
            // 
            // lblPwTitle
            // 
            this.lblPwTitle.AutoEllipsis = true;
            this.lblPwTitle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblPwTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPwTitle.Font = new System.Drawing.Font("맑은 고딕", 14F);
            this.lblPwTitle.ForeColor = System.Drawing.Color.Black;
            this.lblPwTitle.Location = new System.Drawing.Point(0, 101);
            this.lblPwTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblPwTitle.Name = "lblPwTitle";
            this.lblPwTitle.Size = new System.Drawing.Size(144, 32);
            this.lblPwTitle.TabIndex = 117;
            this.lblPwTitle.Text = "Password";
            this.lblPwTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label24
            // 
            this.label24.AutoEllipsis = true;
            this.label24.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label24.Font = new System.Drawing.Font("맑은 고딕", 14F);
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(0, 69);
            this.label24.Margin = new System.Windows.Forms.Padding(0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(144, 32);
            this.label24.TabIndex = 118;
            this.label24.Text = "ID";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPw
            // 
            this.txtPw.BackColor = System.Drawing.Color.White;
            this.txtPw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPw.Font = new System.Drawing.Font("맑은 고딕", 14F);
            this.txtPw.Location = new System.Drawing.Point(144, 101);
            this.txtPw.Margin = new System.Windows.Forms.Padding(0);
            this.txtPw.Name = "txtPw";
            this.txtPw.Size = new System.Drawing.Size(140, 32);
            this.txtPw.TabIndex = 1;
            this.txtPw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPw.UseSystemPasswordChar = true;
            this.txtPw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPw_KeyDown);
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.White;
            this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtID.Font = new System.Drawing.Font("맑은 고딕", 14F);
            this.txtID.Location = new System.Drawing.Point(144, 69);
            this.txtID.Margin = new System.Windows.Forms.Padding(0);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(140, 32);
            this.txtID.TabIndex = 0;
            this.txtID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.cboLevel);
            this.Controls.Add(this.lblNameTitle);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblNotice);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDoSomething);
            this.Controls.Add(this.lblPwTitle);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.txtPw);
            this.Controls.Add(this.txtID);
            this.Name = "FrmLogin";
            this.Text = "FrmLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboLevel;
        internal System.Windows.Forms.Label lblNameTitle;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblNotice;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDoSomething;
        internal System.Windows.Forms.Label lblPwTitle;
        internal System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtPw;
        private System.Windows.Forms.TextBox txtID;
    }
}