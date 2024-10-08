namespace EquipMainUi.Setting.TransferData
{
    partial class UcrlTransferDataKeyInfo
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPort = new System.Windows.Forms.Label();
            this.lblWaferExist = new System.Windows.Forms.Label();
            this.pGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // lblPort
            // 
            this.lblPort.BackColor = System.Drawing.Color.Gainsboro;
            this.lblPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPort.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPort.Location = new System.Drawing.Point(0, 0);
            this.lblPort.Margin = new System.Windows.Forms.Padding(0);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(145, 20);
            this.lblPort.TabIndex = 447;
            this.lblPort.Text = "port name";
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWaferExist
            // 
            this.lblWaferExist.BackColor = System.Drawing.Color.Gainsboro;
            this.lblWaferExist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWaferExist.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaferExist.Location = new System.Drawing.Point(0, 20);
            this.lblWaferExist.Margin = new System.Windows.Forms.Padding(0);
            this.lblWaferExist.Name = "lblWaferExist";
            this.lblWaferExist.Size = new System.Drawing.Size(145, 20);
            this.lblWaferExist.TabIndex = 448;
            this.lblWaferExist.Text = "Exist (HW)";
            this.lblWaferExist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pGrid
            // 
            this.pGrid.HelpVisible = false;
            this.pGrid.Location = new System.Drawing.Point(0, 40);
            this.pGrid.Margin = new System.Windows.Forms.Padding(0);
            this.pGrid.Name = "pGrid";
            this.pGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pGrid.Size = new System.Drawing.Size(145, 110);
            this.pGrid.TabIndex = 449;
            this.pGrid.ToolbarVisible = false;
            // 
            // UcrlTransferDataKeyInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblWaferExist);
            this.Controls.Add(this.pGrid);
            this.Name = "UcrlTransferDataKeyInfo";
            this.Size = new System.Drawing.Size(147, 150);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblWaferExist;
        private System.Windows.Forms.PropertyGrid pGrid;
    }
}
