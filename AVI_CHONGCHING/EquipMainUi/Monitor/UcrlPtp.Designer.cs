namespace EquipMainUi.Monitor
{
    partial class UcrlPtp
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblInspXHomeCompleteBit = new System.Windows.Forms.Label();
            this.txtPos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSpd = new System.Windows.Forms.TextBox();
            this.txtAcc = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInspXHomeCompleteBit
            // 
            this.lblInspXHomeCompleteBit.AutoEllipsis = true;
            this.lblInspXHomeCompleteBit.BackColor = System.Drawing.Color.Gainsboro;
            this.lblInspXHomeCompleteBit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInspXHomeCompleteBit.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInspXHomeCompleteBit.ForeColor = System.Drawing.Color.Black;
            this.lblInspXHomeCompleteBit.Location = new System.Drawing.Point(0, 0);
            this.lblInspXHomeCompleteBit.Margin = new System.Windows.Forms.Padding(0);
            this.lblInspXHomeCompleteBit.Name = "lblInspXHomeCompleteBit";
            this.lblInspXHomeCompleteBit.Size = new System.Drawing.Size(67, 17);
            this.lblInspXHomeCompleteBit.TabIndex = 64;
            this.lblInspXHomeCompleteBit.Text = "Position";
            this.lblInspXHomeCompleteBit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPos
            // 
            this.txtPos.Location = new System.Drawing.Point(0, 17);
            this.txtPos.Margin = new System.Windows.Forms.Padding(0);
            this.txtPos.Name = "txtPos";
            this.txtPos.Size = new System.Drawing.Size(67, 21);
            this.txtPos.TabIndex = 65;
            this.txtPos.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 64;
            this.label1.Text = "Speed";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 17);
            this.label2.TabIndex = 64;
            this.label2.Text = "Acc";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSpd
            // 
            this.txtSpd.Location = new System.Drawing.Point(0, 17);
            this.txtSpd.Margin = new System.Windows.Forms.Padding(0);
            this.txtSpd.Name = "txtSpd";
            this.txtSpd.Size = new System.Drawing.Size(67, 21);
            this.txtSpd.TabIndex = 65;
            this.txtSpd.Text = "201";
            // 
            // txtAcc
            // 
            this.txtAcc.Location = new System.Drawing.Point(0, 17);
            this.txtAcc.Margin = new System.Windows.Forms.Padding(0);
            this.txtAcc.Name = "txtAcc";
            this.txtAcc.Size = new System.Drawing.Size(67, 21);
            this.txtAcc.TabIndex = 65;
            this.txtAcc.Text = "200";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Controls.Add(this.panel3);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(204, 38);
            this.flowLayoutPanel1.TabIndex = 66;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblInspXHomeCompleteBit);
            this.panel1.Controls.Add(this.txtPos);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(68, 39);
            this.panel1.TabIndex = 66;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtSpd);
            this.panel2.Location = new System.Drawing.Point(68, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(68, 39);
            this.panel2.TabIndex = 67;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtAcc);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(136, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(68, 39);
            this.panel3.TabIndex = 68;
            // 
            // UcrlPtp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "UcrlPtp";
            this.Size = new System.Drawing.Size(204, 38);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lblInspXHomeCompleteBit;
        private System.Windows.Forms.TextBox txtPos;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSpd;
        private System.Windows.Forms.TextBox txtAcc;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}
