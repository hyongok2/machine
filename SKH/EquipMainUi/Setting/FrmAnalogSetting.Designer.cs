namespace EquipMainUi.Setting
{
    partial class FrmAnalogSetting
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
            this.label249 = new System.Windows.Forms.Label();
            this.tmrUiUpdate = new System.Windows.Forms.Timer(this.components);
            this.tabpCtrl_AnalogSetting = new System.Windows.Forms.TabControl();
            this.tabp_AD1 = new System.Windows.Forms.TabPage();
            this.ucrlSetting_AD1 = new Dit.Framework.Analog.UcrlSetting_68ADVN();
            this.tabp_AD2 = new System.Windows.Forms.TabPage();
            this.ucrlSetting_AD2 = new Dit.Framework.Analog.UcrlSetting_68ADVN();
            this.tabp_AD3 = new System.Windows.Forms.TabPage();
            this.ucrlSetting_AD3 = new Dit.Framework.Analog.UcrlSetting_68ADVN();
            this.tabp_AD4 = new System.Windows.Forms.TabPage();
            this.ucrlSetting_AD4 = new Dit.Framework.Analog.UcrlSetting_68ADVN();
            this.tabp_AD5 = new System.Windows.Forms.TabPage();
            this.ucrlSetting_AD5 = new Dit.Framework.Analog.UcrlSetting_68ADVN();
            this.tabp_AD6 = new System.Windows.Forms.TabPage();
            this.ucrlSetting_AD6 = new Dit.Framework.Analog.UcrlSetting_68ADVN();
            this.tabp_DA1 = new System.Windows.Forms.TabPage();
            this.ucrlSetting_DA1 = new Dit.Framework.Analog.UcrlSetting_68DAVN();
            this.tabp_TD1 = new System.Windows.Forms.TabPage();
            this.tabp_DA2 = new System.Windows.Forms.TabPage();
            this.tabp_AD7 = new System.Windows.Forms.TabPage();
            this.tabp_AD8 = new System.Windows.Forms.TabPage();
            this.ucrlSetting_AD7 = new Dit.Framework.Analog.UcrlSetting_68ADVN();
            this.ucrlSetting_AD8 = new Dit.Framework.Analog.UcrlSetting_68ADVN();
            this.ucrlSetting_DA2 = new Dit.Framework.Analog.UcrlSetting_68DAVN();
            this.tabpCtrl_AnalogSetting.SuspendLayout();
            this.tabp_AD1.SuspendLayout();
            this.tabp_AD2.SuspendLayout();
            this.tabp_AD3.SuspendLayout();
            this.tabp_AD4.SuspendLayout();
            this.tabp_AD5.SuspendLayout();
            this.tabp_AD6.SuspendLayout();
            this.tabp_DA1.SuspendLayout();
            this.tabp_DA2.SuspendLayout();
            this.tabp_AD7.SuspendLayout();
            this.tabp_AD8.SuspendLayout();
            this.SuspendLayout();
            // 
            // label249
            // 
            this.label249.BackColor = System.Drawing.Color.Silver;
            this.label249.Dock = System.Windows.Forms.DockStyle.Top;
            this.label249.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label249.ForeColor = System.Drawing.Color.Black;
            this.label249.Location = new System.Drawing.Point(0, 0);
            this.label249.Name = "label249";
            this.label249.Size = new System.Drawing.Size(847, 40);
            this.label249.TabIndex = 11;
            this.label249.Text = "■ Analog Setting";
            this.label249.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrUiUpdate
            // 
            this.tmrUiUpdate.Tick += new System.EventHandler(this.tmrUiUpdate_Tick);
            // 
            // tabpCtrl_AnalogSetting
            // 
            this.tabpCtrl_AnalogSetting.Controls.Add(this.tabp_AD1);
            this.tabpCtrl_AnalogSetting.Controls.Add(this.tabp_AD2);
            this.tabpCtrl_AnalogSetting.Controls.Add(this.tabp_AD3);
            this.tabpCtrl_AnalogSetting.Controls.Add(this.tabp_AD4);
            this.tabpCtrl_AnalogSetting.Controls.Add(this.tabp_AD5);
            this.tabpCtrl_AnalogSetting.Controls.Add(this.tabp_AD6);
            this.tabpCtrl_AnalogSetting.Controls.Add(this.tabp_AD7);
            this.tabpCtrl_AnalogSetting.Controls.Add(this.tabp_AD8);
            this.tabpCtrl_AnalogSetting.Controls.Add(this.tabp_DA1);
            this.tabpCtrl_AnalogSetting.Controls.Add(this.tabp_DA2);
            this.tabpCtrl_AnalogSetting.Controls.Add(this.tabp_TD1);
            this.tabpCtrl_AnalogSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabpCtrl_AnalogSetting.ItemSize = new System.Drawing.Size(76, 30);
            this.tabpCtrl_AnalogSetting.Location = new System.Drawing.Point(0, 40);
            this.tabpCtrl_AnalogSetting.Name = "tabpCtrl_AnalogSetting";
            this.tabpCtrl_AnalogSetting.SelectedIndex = 0;
            this.tabpCtrl_AnalogSetting.Size = new System.Drawing.Size(847, 408);
            this.tabpCtrl_AnalogSetting.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabpCtrl_AnalogSetting.TabIndex = 12;
            this.tabpCtrl_AnalogSetting.SelectedIndexChanged += new System.EventHandler(this.tabpCtrl_AnalogSetting_SelectedIndexChanged);
            // 
            // tabp_AD1
            // 
            this.tabp_AD1.Controls.Add(this.ucrlSetting_AD1);
            this.tabp_AD1.Location = new System.Drawing.Point(4, 34);
            this.tabp_AD1.Name = "tabp_AD1";
            this.tabp_AD1.Padding = new System.Windows.Forms.Padding(3);
            this.tabp_AD1.Size = new System.Drawing.Size(839, 370);
            this.tabp_AD1.TabIndex = 0;
            this.tabp_AD1.Text = "AD1";
            this.tabp_AD1.UseVisualStyleBackColor = true;
            // 
            // ucrlSetting_AD1
            // 
            this.ucrlSetting_AD1.ADVN = null;
            this.ucrlSetting_AD1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucrlSetting_AD1.Location = new System.Drawing.Point(3, 3);
            this.ucrlSetting_AD1.Name = "ucrlSetting_AD1";
            this.ucrlSetting_AD1.Size = new System.Drawing.Size(835, 363);
            this.ucrlSetting_AD1.TabIndex = 0;
            // 
            // tabp_AD2
            // 
            this.tabp_AD2.Controls.Add(this.ucrlSetting_AD2);
            this.tabp_AD2.Location = new System.Drawing.Point(4, 34);
            this.tabp_AD2.Name = "tabp_AD2";
            this.tabp_AD2.Padding = new System.Windows.Forms.Padding(3);
            this.tabp_AD2.Size = new System.Drawing.Size(839, 370);
            this.tabp_AD2.TabIndex = 1;
            this.tabp_AD2.Text = "AD2";
            this.tabp_AD2.UseVisualStyleBackColor = true;
            // 
            // ucrlSetting_AD2
            // 
            this.ucrlSetting_AD2.ADVN = null;
            this.ucrlSetting_AD2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucrlSetting_AD2.Location = new System.Drawing.Point(2, 4);
            this.ucrlSetting_AD2.Name = "ucrlSetting_AD2";
            this.ucrlSetting_AD2.Size = new System.Drawing.Size(835, 363);
            this.ucrlSetting_AD2.TabIndex = 1;
            // 
            // tabp_AD3
            // 
            this.tabp_AD3.Controls.Add(this.ucrlSetting_AD3);
            this.tabp_AD3.Location = new System.Drawing.Point(4, 34);
            this.tabp_AD3.Name = "tabp_AD3";
            this.tabp_AD3.Size = new System.Drawing.Size(839, 370);
            this.tabp_AD3.TabIndex = 2;
            this.tabp_AD3.Text = "AD3";
            this.tabp_AD3.UseVisualStyleBackColor = true;
            // 
            // ucrlSetting_AD3
            // 
            this.ucrlSetting_AD3.ADVN = null;
            this.ucrlSetting_AD3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucrlSetting_AD3.Location = new System.Drawing.Point(2, 4);
            this.ucrlSetting_AD3.Name = "ucrlSetting_AD3";
            this.ucrlSetting_AD3.Size = new System.Drawing.Size(835, 363);
            this.ucrlSetting_AD3.TabIndex = 1;
            // 
            // tabp_AD4
            // 
            this.tabp_AD4.Controls.Add(this.ucrlSetting_AD4);
            this.tabp_AD4.Location = new System.Drawing.Point(4, 34);
            this.tabp_AD4.Name = "tabp_AD4";
            this.tabp_AD4.Size = new System.Drawing.Size(839, 370);
            this.tabp_AD4.TabIndex = 3;
            this.tabp_AD4.Text = "AD4";
            this.tabp_AD4.UseVisualStyleBackColor = true;
            // 
            // ucrlSetting_AD4
            // 
            this.ucrlSetting_AD4.ADVN = null;
            this.ucrlSetting_AD4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucrlSetting_AD4.Location = new System.Drawing.Point(2, 4);
            this.ucrlSetting_AD4.Name = "ucrlSetting_AD4";
            this.ucrlSetting_AD4.Size = new System.Drawing.Size(835, 363);
            this.ucrlSetting_AD4.TabIndex = 1;
            // 
            // tabp_AD5
            // 
            this.tabp_AD5.Controls.Add(this.ucrlSetting_AD5);
            this.tabp_AD5.Location = new System.Drawing.Point(4, 34);
            this.tabp_AD5.Name = "tabp_AD5";
            this.tabp_AD5.Size = new System.Drawing.Size(839, 370);
            this.tabp_AD5.TabIndex = 4;
            this.tabp_AD5.Text = "AD5";
            this.tabp_AD5.UseVisualStyleBackColor = true;
            // 
            // ucrlSetting_AD5
            // 
            this.ucrlSetting_AD5.ADVN = null;
            this.ucrlSetting_AD5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucrlSetting_AD5.Location = new System.Drawing.Point(2, 4);
            this.ucrlSetting_AD5.Name = "ucrlSetting_AD5";
            this.ucrlSetting_AD5.Size = new System.Drawing.Size(835, 363);
            this.ucrlSetting_AD5.TabIndex = 1;
            // 
            // tabp_AD6
            // 
            this.tabp_AD6.Controls.Add(this.ucrlSetting_AD6);
            this.tabp_AD6.Location = new System.Drawing.Point(4, 34);
            this.tabp_AD6.Name = "tabp_AD6";
            this.tabp_AD6.Size = new System.Drawing.Size(839, 370);
            this.tabp_AD6.TabIndex = 5;
            this.tabp_AD6.Text = "AD6";
            this.tabp_AD6.UseVisualStyleBackColor = true;
            // 
            // ucrlSetting_AD6
            // 
            this.ucrlSetting_AD6.ADVN = null;
            this.ucrlSetting_AD6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucrlSetting_AD6.Location = new System.Drawing.Point(2, 4);
            this.ucrlSetting_AD6.Name = "ucrlSetting_AD6";
            this.ucrlSetting_AD6.Size = new System.Drawing.Size(835, 363);
            this.ucrlSetting_AD6.TabIndex = 1;
            // 
            // tabp_DA1
            // 
            this.tabp_DA1.Controls.Add(this.ucrlSetting_DA1);
            this.tabp_DA1.Location = new System.Drawing.Point(4, 34);
            this.tabp_DA1.Name = "tabp_DA1";
            this.tabp_DA1.Size = new System.Drawing.Size(839, 370);
            this.tabp_DA1.TabIndex = 6;
            this.tabp_DA1.Text = "DA1";
            this.tabp_DA1.UseVisualStyleBackColor = true;
            // 
            // ucrlSetting_DA1
            // 
            this.ucrlSetting_DA1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucrlSetting_DA1.DAVN = null;
            this.ucrlSetting_DA1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucrlSetting_DA1.Location = new System.Drawing.Point(0, 0);
            this.ucrlSetting_DA1.Name = "ucrlSetting_DA1";
            this.ucrlSetting_DA1.Size = new System.Drawing.Size(839, 370);
            this.ucrlSetting_DA1.TabIndex = 0;
            // 
            // tabp_TD1
            // 
            this.tabp_TD1.Location = new System.Drawing.Point(4, 34);
            this.tabp_TD1.Name = "tabp_TD1";
            this.tabp_TD1.Size = new System.Drawing.Size(839, 370);
            this.tabp_TD1.TabIndex = 7;
            this.tabp_TD1.Text = "TD1";
            this.tabp_TD1.UseVisualStyleBackColor = true;
            // 
            // tabp_DA2
            // 
            this.tabp_DA2.Controls.Add(this.ucrlSetting_DA2);
            this.tabp_DA2.Location = new System.Drawing.Point(4, 34);
            this.tabp_DA2.Name = "tabp_DA2";
            this.tabp_DA2.Size = new System.Drawing.Size(839, 370);
            this.tabp_DA2.TabIndex = 8;
            this.tabp_DA2.Text = "DA2";
            this.tabp_DA2.UseVisualStyleBackColor = true;
            // 
            // tabp_AD7
            // 
            this.tabp_AD7.Controls.Add(this.ucrlSetting_AD7);
            this.tabp_AD7.Location = new System.Drawing.Point(4, 34);
            this.tabp_AD7.Name = "tabp_AD7";
            this.tabp_AD7.Size = new System.Drawing.Size(839, 370);
            this.tabp_AD7.TabIndex = 9;
            this.tabp_AD7.Text = "AD7";
            this.tabp_AD7.UseVisualStyleBackColor = true;
            // 
            // tabp_AD8
            // 
            this.tabp_AD8.Controls.Add(this.ucrlSetting_AD8);
            this.tabp_AD8.Location = new System.Drawing.Point(4, 34);
            this.tabp_AD8.Name = "tabp_AD8";
            this.tabp_AD8.Size = new System.Drawing.Size(839, 370);
            this.tabp_AD8.TabIndex = 10;
            this.tabp_AD8.Text = "AD8";
            this.tabp_AD8.UseVisualStyleBackColor = true;
            // 
            // ucrlSetting_AD7
            // 
            this.ucrlSetting_AD7.ADVN = null;
            this.ucrlSetting_AD7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucrlSetting_AD7.Location = new System.Drawing.Point(2, 4);
            this.ucrlSetting_AD7.Name = "ucrlSetting_AD7";
            this.ucrlSetting_AD7.Size = new System.Drawing.Size(835, 363);
            this.ucrlSetting_AD7.TabIndex = 2;
            // 
            // ucrlSetting_AD8
            // 
            this.ucrlSetting_AD8.ADVN = null;
            this.ucrlSetting_AD8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucrlSetting_AD8.Location = new System.Drawing.Point(2, 4);
            this.ucrlSetting_AD8.Name = "ucrlSetting_AD8";
            this.ucrlSetting_AD8.Size = new System.Drawing.Size(835, 363);
            this.ucrlSetting_AD8.TabIndex = 3;
            // 
            // ucrlSetting_DA2
            // 
            this.ucrlSetting_DA2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ucrlSetting_DA2.DAVN = null;
            this.ucrlSetting_DA2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucrlSetting_DA2.Location = new System.Drawing.Point(0, 0);
            this.ucrlSetting_DA2.Name = "ucrlSetting_DA2";
            this.ucrlSetting_DA2.Size = new System.Drawing.Size(839, 370);
            this.ucrlSetting_DA2.TabIndex = 1;
            // 
            // FrmAnalogSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 448);
            this.Controls.Add(this.tabpCtrl_AnalogSetting);
            this.Controls.Add(this.label249);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FrmAnalogSetting";
            this.Text = "FrmADSetting";
            this.tabpCtrl_AnalogSetting.ResumeLayout(false);
            this.tabp_AD1.ResumeLayout(false);
            this.tabp_AD2.ResumeLayout(false);
            this.tabp_AD3.ResumeLayout(false);
            this.tabp_AD4.ResumeLayout(false);
            this.tabp_AD5.ResumeLayout(false);
            this.tabp_AD6.ResumeLayout(false);
            this.tabp_DA1.ResumeLayout(false);
            this.tabp_DA2.ResumeLayout(false);
            this.tabp_AD7.ResumeLayout(false);
            this.tabp_AD8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label label249;
        private System.Windows.Forms.Timer tmrUiUpdate;
        private System.Windows.Forms.TabControl tabpCtrl_AnalogSetting;
        private System.Windows.Forms.TabPage tabp_AD1;
        private System.Windows.Forms.TabPage tabp_AD2;
        private System.Windows.Forms.TabPage tabp_AD3;
        private System.Windows.Forms.TabPage tabp_AD4;
        private System.Windows.Forms.TabPage tabp_AD5;
        private System.Windows.Forms.TabPage tabp_AD6;
        private System.Windows.Forms.TabPage tabp_DA1;
        private System.Windows.Forms.TabPage tabp_TD1;
        private Dit.Framework.Analog.UcrlSetting_68ADVN ucrlSetting_AD1;
        private Dit.Framework.Analog.UcrlSetting_68ADVN ucrlSetting_AD2;
        private Dit.Framework.Analog.UcrlSetting_68ADVN ucrlSetting_AD3;
        private Dit.Framework.Analog.UcrlSetting_68ADVN ucrlSetting_AD4;
        private Dit.Framework.Analog.UcrlSetting_68ADVN ucrlSetting_AD5;
        private Dit.Framework.Analog.UcrlSetting_68ADVN ucrlSetting_AD6;
        private Dit.Framework.Analog.UcrlSetting_68DAVN ucrlSetting_DA1;
        private System.Windows.Forms.TabPage tabp_AD7;
        private System.Windows.Forms.TabPage tabp_AD8;
        private System.Windows.Forms.TabPage tabp_DA2;
        private Dit.Framework.Analog.UcrlSetting_68ADVN ucrlSetting_AD7;
        private Dit.Framework.Analog.UcrlSetting_68ADVN ucrlSetting_AD8;
        private Dit.Framework.Analog.UcrlSetting_68DAVN ucrlSetting_DA2;
    }
}