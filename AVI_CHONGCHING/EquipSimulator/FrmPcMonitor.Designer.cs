namespace EquipSimulator
{
    partial class FrmPcMonitor
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flpRevPcIn = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flpRevPcOut = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flpInspPcIn = new System.Windows.Forms.FlowLayoutPanel();
            this.grbInsp = new System.Windows.Forms.GroupBox();
            this.flpInspPcOut = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grbInsp.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.flpRevPcIn);
            this.groupBox3.Location = new System.Drawing.Point(845, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(275, 462);
            this.groupBox3.TabIndex = 91;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "리뷰 PC OUT";
            // 
            // flpRevPcIn
            // 
            this.flpRevPcIn.AutoScroll = true;
            this.flpRevPcIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpRevPcIn.Location = new System.Drawing.Point(3, 19);
            this.flpRevPcIn.Name = "flpRevPcIn";
            this.flpRevPcIn.Size = new System.Drawing.Size(269, 440);
            this.flpRevPcIn.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.flpRevPcOut);
            this.groupBox2.Location = new System.Drawing.Point(564, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 462);
            this.groupBox2.TabIndex = 92;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "리뷰 PC IN";
            // 
            // flpRevPcOut
            // 
            this.flpRevPcOut.AutoScroll = true;
            this.flpRevPcOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpRevPcOut.Location = new System.Drawing.Point(3, 19);
            this.flpRevPcOut.Name = "flpRevPcOut";
            this.flpRevPcOut.Size = new System.Drawing.Size(269, 440);
            this.flpRevPcOut.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.flpInspPcIn);
            this.groupBox1.Location = new System.Drawing.Point(275, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(283, 462);
            this.groupBox1.TabIndex = 90;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "INSP PC OUT";
            // 
            // flpInspPcIn
            // 
            this.flpInspPcIn.AutoScroll = true;
            this.flpInspPcIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpInspPcIn.Location = new System.Drawing.Point(3, 19);
            this.flpInspPcIn.Name = "flpInspPcIn";
            this.flpInspPcIn.Size = new System.Drawing.Size(277, 440);
            this.flpInspPcIn.TabIndex = 1;
            // 
            // grbInsp
            // 
            this.grbInsp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grbInsp.Controls.Add(this.flpInspPcOut);
            this.grbInsp.Location = new System.Drawing.Point(5, 12);
            this.grbInsp.Name = "grbInsp";
            this.grbInsp.Size = new System.Drawing.Size(254, 462);
            this.grbInsp.TabIndex = 89;
            this.grbInsp.TabStop = false;
            this.grbInsp.Text = "INSP PC IN";
            // 
            // flpInspPcOut
            // 
            this.flpInspPcOut.AutoScroll = true;
            this.flpInspPcOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpInspPcOut.Location = new System.Drawing.Point(3, 19);
            this.flpInspPcOut.Name = "flpInspPcOut";
            this.flpInspPcOut.Size = new System.Drawing.Size(248, 440);
            this.flpInspPcOut.TabIndex = 0;
            // 
            // FrmXYMonitor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1120, 481);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grbInsp);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "FrmXYMonitor";
            this.Text = "상세";
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.grbInsp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grbInsp;
        public System.Windows.Forms.FlowLayoutPanel flpRevPcIn;
        public System.Windows.Forms.FlowLayoutPanel flpRevPcOut;
        public System.Windows.Forms.FlowLayoutPanel flpInspPcIn;
        public System.Windows.Forms.FlowLayoutPanel flpInspPcOut;
    }
}