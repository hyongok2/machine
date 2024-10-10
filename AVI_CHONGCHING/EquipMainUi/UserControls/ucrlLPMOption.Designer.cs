namespace EquipMainUi.UserControls
{
    partial class ucrlLPMOption
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
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.rdUserData = new System.Windows.Forms.RadioButton();
            this.rdMapData = new System.Windows.Forms.RadioButton();
            this.rdFirstWafer = new System.Windows.Forms.RadioButton();
            this.rdLastWafer = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.rdUserData, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.rdMapData, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.rdFirstWafer, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.rdLastWafer, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(366, 83);
            this.tableLayoutPanel3.TabIndex = 464;
            // 
            // rdUserData
            // 
            this.rdUserData.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdUserData.BackgroundImage = global::EquipMainUi.Properties.Resources.manualData;
            this.rdUserData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rdUserData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdUserData.Location = new System.Drawing.Point(273, 0);
            this.rdUserData.Margin = new System.Windows.Forms.Padding(0);
            this.rdUserData.Name = "rdUserData";
            this.rdUserData.Size = new System.Drawing.Size(93, 83);
            this.rdUserData.TabIndex = 463;
            this.rdUserData.TabStop = true;
            this.rdUserData.UseVisualStyleBackColor = true;
            this.rdUserData.Click += new System.EventHandler(this.chkOHT_Click);
            // 
            // rdMapData
            // 
            this.rdMapData.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdMapData.BackgroundImage = global::EquipMainUi.Properties.Resources.mapData;
            this.rdMapData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rdMapData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdMapData.Location = new System.Drawing.Point(182, 0);
            this.rdMapData.Margin = new System.Windows.Forms.Padding(0);
            this.rdMapData.Name = "rdMapData";
            this.rdMapData.Size = new System.Drawing.Size(91, 83);
            this.rdMapData.TabIndex = 462;
            this.rdMapData.TabStop = true;
            this.rdMapData.UseVisualStyleBackColor = true;
            this.rdMapData.Click += new System.EventHandler(this.chkOHT_Click);
            // 
            // rdFirstWafer
            // 
            this.rdFirstWafer.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdFirstWafer.BackgroundImage = global::EquipMainUi.Properties.Resources.WaferStart;
            this.rdFirstWafer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rdFirstWafer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdFirstWafer.Location = new System.Drawing.Point(91, 0);
            this.rdFirstWafer.Margin = new System.Windows.Forms.Padding(0);
            this.rdFirstWafer.Name = "rdFirstWafer";
            this.rdFirstWafer.Size = new System.Drawing.Size(91, 83);
            this.rdFirstWafer.TabIndex = 465;
            this.rdFirstWafer.TabStop = true;
            this.rdFirstWafer.UseVisualStyleBackColor = true;
            this.rdFirstWafer.Click += new System.EventHandler(this.chkOHT_Click);
            // 
            // rdLastWafer
            // 
            this.rdLastWafer.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdLastWafer.BackgroundImage = global::EquipMainUi.Properties.Resources.WaferEnd1;
            this.rdLastWafer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rdLastWafer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdLastWafer.Location = new System.Drawing.Point(0, 0);
            this.rdLastWafer.Margin = new System.Windows.Forms.Padding(0);
            this.rdLastWafer.Name = "rdLastWafer";
            this.rdLastWafer.Size = new System.Drawing.Size(91, 83);
            this.rdLastWafer.TabIndex = 464;
            this.rdLastWafer.TabStop = true;
            this.rdLastWafer.UseVisualStyleBackColor = true;
            this.rdLastWafer.Click += new System.EventHandler(this.chkOHT_Click);
            // 
            // ucrlLPMOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel3);
            this.Name = "ucrlLPMOption";
            this.Size = new System.Drawing.Size(366, 83);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.RadioButton rdUserData;
        private System.Windows.Forms.RadioButton rdMapData;
        private System.Windows.Forms.RadioButton rdFirstWafer;
        private System.Windows.Forms.RadioButton rdLastWafer;
    }
}
