using Dit.Framework.UI.UserComponent;

namespace EquipMainUi.Test
{
    partial class ucrlHsmsTest
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
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbEvt = new System.Windows.Forms.ComboBox();
            this.btnEvent = new System.Windows.Forms.Button();
            this.lblEvtCmd = new Dit.Framework.UI.UserComponent.LabelDelay();
            this.lblEvtStep = new Dit.Framework.UI.UserComponent.LabelDelay();
            this.lblEvtAck = new Dit.Framework.UI.UserComponent.LabelDelay();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbCmd = new System.Windows.Forms.ComboBox();
            this.btnStartCmd = new System.Windows.Forms.Button();
            this.lblCmdStep = new Dit.Framework.UI.UserComponent.LabelDelay();
            this.lblCmdAck = new Dit.Framework.UI.UserComponent.LabelDelay();
            this.lblCmdCmd = new Dit.Framework.UI.UserComponent.LabelDelay();
            this.tmrUiTimer = new System.Windows.Forms.Timer(this.components);
            this.lblMemName = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pGridHsmsSetting = new System.Windows.Forms.PropertyGrid();
            this.btnHostECIDCheck = new System.Windows.Forms.Button();
            this.btnHsmsSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rtbEcid = new System.Windows.Forms.RichTextBox();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(508, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 24);
            this.label2.TabIndex = 18;
            this.label2.Text = "Step";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(407, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 24);
            this.label1.TabIndex = 19;
            this.label1.Text = "CIM > Ctrl";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label34
            // 
            this.label34.AutoEllipsis = true;
            this.label34.BackColor = System.Drawing.Color.Gainsboro;
            this.label34.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label34.ForeColor = System.Drawing.Color.Black;
            this.label34.Location = new System.Drawing.Point(306, 40);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(95, 24);
            this.label34.TabIndex = 20;
            this.label34.Text = "Ctrl > CIM";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbEvt);
            this.groupBox3.Controls.Add(this.btnEvent);
            this.groupBox3.Controls.Add(this.lblEvtCmd);
            this.groupBox3.Controls.Add(this.lblEvtStep);
            this.groupBox3.Controls.Add(this.lblEvtAck);
            this.groupBox3.Location = new System.Drawing.Point(3, 126);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(612, 58);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Event";
            // 
            // cmbEvt
            // 
            this.cmbEvt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEvt.FormattingEnabled = true;
            this.cmbEvt.Location = new System.Drawing.Point(6, 24);
            this.cmbEvt.Name = "cmbEvt";
            this.cmbEvt.Size = new System.Drawing.Size(188, 20);
            this.cmbEvt.TabIndex = 1;
            this.cmbEvt.SelectedIndexChanged += new System.EventHandler(this.cmbEvent_SelectedIndexChanged);
            // 
            // btnEvent
            // 
            this.btnEvent.Location = new System.Drawing.Point(200, 14);
            this.btnEvent.Name = "btnEvent";
            this.btnEvent.Size = new System.Drawing.Size(90, 38);
            this.btnEvent.TabIndex = 0;
            this.btnEvent.Text = "Event 강제발생";
            this.btnEvent.UseVisualStyleBackColor = true;
            this.btnEvent.Click += new System.EventHandler(this.btnEvent_Click);
            // 
            // lblEvtCmd
            // 
            this.lblEvtCmd.AutoEllipsis = true;
            this.lblEvtCmd.BackColor = System.Drawing.Color.White;
            this.lblEvtCmd.Delay = 500;
            this.lblEvtCmd.DelayOff = false;
            this.lblEvtCmd.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblEvtCmd.ForeColor = System.Drawing.Color.Black;
            this.lblEvtCmd.Location = new System.Drawing.Point(303, 20);
            this.lblEvtCmd.Name = "lblEvtCmd";
            this.lblEvtCmd.OffColor = System.Drawing.Color.White;
            this.lblEvtCmd.OnColor = System.Drawing.Color.LimeGreen;
            this.lblEvtCmd.OnOff = false;
            this.lblEvtCmd.Size = new System.Drawing.Size(95, 24);
            this.lblEvtCmd.TabIndex = 15;
            this.lblEvtCmd.Text2 = "";
            this.lblEvtCmd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEvtStep
            // 
            this.lblEvtStep.AutoEllipsis = true;
            this.lblEvtStep.BackColor = System.Drawing.Color.White;
            this.lblEvtStep.Delay = 500;
            this.lblEvtStep.DelayOff = false;
            this.lblEvtStep.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblEvtStep.ForeColor = System.Drawing.Color.Black;
            this.lblEvtStep.Location = new System.Drawing.Point(505, 20);
            this.lblEvtStep.Name = "lblEvtStep";
            this.lblEvtStep.OffColor = System.Drawing.Color.White;
            this.lblEvtStep.OnColor = System.Drawing.Color.LimeGreen;
            this.lblEvtStep.OnOff = false;
            this.lblEvtStep.Size = new System.Drawing.Size(95, 24);
            this.lblEvtStep.TabIndex = 15;
            this.lblEvtStep.Text = "000";
            this.lblEvtStep.Text2 = "";
            this.lblEvtStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEvtAck
            // 
            this.lblEvtAck.AutoEllipsis = true;
            this.lblEvtAck.BackColor = System.Drawing.Color.White;
            this.lblEvtAck.Delay = 500;
            this.lblEvtAck.DelayOff = false;
            this.lblEvtAck.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblEvtAck.ForeColor = System.Drawing.Color.Black;
            this.lblEvtAck.Location = new System.Drawing.Point(404, 20);
            this.lblEvtAck.Name = "lblEvtAck";
            this.lblEvtAck.OffColor = System.Drawing.Color.White;
            this.lblEvtAck.OnColor = System.Drawing.Color.LimeGreen;
            this.lblEvtAck.OnOff = false;
            this.lblEvtAck.Size = new System.Drawing.Size(95, 24);
            this.lblEvtAck.TabIndex = 15;
            this.lblEvtAck.Text2 = "";
            this.lblEvtAck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbCmd);
            this.groupBox1.Controls.Add(this.btnStartCmd);
            this.groupBox1.Controls.Add(this.lblCmdStep);
            this.groupBox1.Controls.Add(this.lblCmdAck);
            this.groupBox1.Controls.Add(this.lblCmdCmd);
            this.groupBox1.Location = new System.Drawing.Point(3, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(612, 58);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Command";
            // 
            // cmbCmd
            // 
            this.cmbCmd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCmd.FormattingEnabled = true;
            this.cmbCmd.Location = new System.Drawing.Point(6, 24);
            this.cmbCmd.Name = "cmbCmd";
            this.cmbCmd.Size = new System.Drawing.Size(188, 20);
            this.cmbCmd.TabIndex = 1;
            this.cmbCmd.SelectedIndexChanged += new System.EventHandler(this.cmbCmd_SelectedIndexChanged);
            // 
            // btnStartCmd
            // 
            this.btnStartCmd.Location = new System.Drawing.Point(200, 14);
            this.btnStartCmd.Name = "btnStartCmd";
            this.btnStartCmd.Size = new System.Drawing.Size(90, 38);
            this.btnStartCmd.TabIndex = 0;
            this.btnStartCmd.Text = "COMMAND";
            this.btnStartCmd.UseVisualStyleBackColor = true;
            this.btnStartCmd.Click += new System.EventHandler(this.btnStartCmd_Click);
            // 
            // lblCmdStep
            // 
            this.lblCmdStep.AutoEllipsis = true;
            this.lblCmdStep.BackColor = System.Drawing.Color.White;
            this.lblCmdStep.Delay = 500;
            this.lblCmdStep.DelayOff = false;
            this.lblCmdStep.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblCmdStep.ForeColor = System.Drawing.Color.Black;
            this.lblCmdStep.Location = new System.Drawing.Point(505, 18);
            this.lblCmdStep.Name = "lblCmdStep";
            this.lblCmdStep.OffColor = System.Drawing.Color.White;
            this.lblCmdStep.OnColor = System.Drawing.Color.LimeGreen;
            this.lblCmdStep.OnOff = false;
            this.lblCmdStep.Size = new System.Drawing.Size(95, 24);
            this.lblCmdStep.TabIndex = 15;
            this.lblCmdStep.Text = "000";
            this.lblCmdStep.Text2 = "";
            this.lblCmdStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCmdAck
            // 
            this.lblCmdAck.AutoEllipsis = true;
            this.lblCmdAck.BackColor = System.Drawing.Color.White;
            this.lblCmdAck.Delay = 500;
            this.lblCmdAck.DelayOff = false;
            this.lblCmdAck.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblCmdAck.ForeColor = System.Drawing.Color.Black;
            this.lblCmdAck.Location = new System.Drawing.Point(404, 18);
            this.lblCmdAck.Name = "lblCmdAck";
            this.lblCmdAck.OffColor = System.Drawing.Color.White;
            this.lblCmdAck.OnColor = System.Drawing.Color.LimeGreen;
            this.lblCmdAck.OnOff = false;
            this.lblCmdAck.Size = new System.Drawing.Size(95, 24);
            this.lblCmdAck.TabIndex = 15;
            this.lblCmdAck.Text2 = "";
            this.lblCmdAck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCmdCmd
            // 
            this.lblCmdCmd.AutoEllipsis = true;
            this.lblCmdCmd.BackColor = System.Drawing.Color.White;
            this.lblCmdCmd.Delay = 500;
            this.lblCmdCmd.DelayOff = false;
            this.lblCmdCmd.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblCmdCmd.ForeColor = System.Drawing.Color.Black;
            this.lblCmdCmd.Location = new System.Drawing.Point(303, 18);
            this.lblCmdCmd.Name = "lblCmdCmd";
            this.lblCmdCmd.OffColor = System.Drawing.Color.White;
            this.lblCmdCmd.OnColor = System.Drawing.Color.LimeGreen;
            this.lblCmdCmd.OnOff = false;
            this.lblCmdCmd.Size = new System.Drawing.Size(95, 24);
            this.lblCmdCmd.TabIndex = 15;
            this.lblCmdCmd.Text2 = "";
            this.lblCmdCmd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrUiTimer
            // 
            this.tmrUiTimer.Interval = 200;
            this.tmrUiTimer.Tick += new System.EventHandler(this.tmrUiTimer_Tick);
            // 
            // lblMemName
            // 
            this.lblMemName.AutoEllipsis = true;
            this.lblMemName.BackColor = System.Drawing.Color.Gainsboro;
            this.lblMemName.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.lblMemName.ForeColor = System.Drawing.Color.Black;
            this.lblMemName.Location = new System.Drawing.Point(6, 11);
            this.lblMemName.Name = "lblMemName";
            this.lblMemName.Size = new System.Drawing.Size(609, 24);
            this.lblMemName.TabIndex = 20;
            this.lblMemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pGridHsmsSetting);
            this.groupBox2.Controls.Add(this.btnHsmsSave);
            this.groupBox2.Location = new System.Drawing.Point(3, 190);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(612, 304);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Setting";
            // 
            // pGridHsmsSetting
            // 
            this.pGridHsmsSetting.HelpVisible = false;
            this.pGridHsmsSetting.Location = new System.Drawing.Point(6, 17);
            this.pGridHsmsSetting.Name = "pGridHsmsSetting";
            this.pGridHsmsSetting.Size = new System.Drawing.Size(600, 237);
            this.pGridHsmsSetting.TabIndex = 1;
            this.pGridHsmsSetting.ToolbarVisible = false;
            // 
            // btnHostECIDCheck
            // 
            this.btnHostECIDCheck.Location = new System.Drawing.Point(836, 450);
            this.btnHostECIDCheck.Name = "btnHostECIDCheck";
            this.btnHostECIDCheck.Size = new System.Drawing.Size(90, 38);
            this.btnHostECIDCheck.TabIndex = 0;
            this.btnHostECIDCheck.Text = "ECID 체크";
            this.btnHostECIDCheck.UseVisualStyleBackColor = true;
            this.btnHostECIDCheck.Click += new System.EventHandler(this.btnHostECIDCheck_Click);
            // 
            // btnHsmsSave
            // 
            this.btnHsmsSave.Location = new System.Drawing.Point(510, 260);
            this.btnHsmsSave.Name = "btnHsmsSave";
            this.btnHsmsSave.Size = new System.Drawing.Size(90, 38);
            this.btnHsmsSave.TabIndex = 0;
            this.btnHsmsSave.Text = "Save";
            this.btnHsmsSave.UseVisualStyleBackColor = true;
            this.btnHsmsSave.Click += new System.EventHandler(this.btnHsmsSave_Click);
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.BackColor = System.Drawing.Color.Gainsboro;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(306, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 24);
            this.label3.TabIndex = 21;
            this.label3.Text = "CIM > Ctrl";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.BackColor = System.Drawing.Color.Gainsboro;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(407, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 24);
            this.label4.TabIndex = 22;
            this.label4.Text = "Ctrl > CIM";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtbEcid
            // 
            this.rtbEcid.Location = new System.Drawing.Point(621, 207);
            this.rtbEcid.Name = "rtbEcid";
            this.rtbEcid.Size = new System.Drawing.Size(305, 237);
            this.rtbEcid.TabIndex = 23;
            this.rtbEcid.Text = "";
            // 
            // ucrlHsmsTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtbEcid);
            this.Controls.Add(this.btnHostECIDCheck);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMemName);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "ucrlHsmsTest";
            this.Size = new System.Drawing.Size(941, 497);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label34;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbEvt;
        internal LabelDelay lblEvtCmd;
        internal LabelDelay lblEvtStep;
        internal LabelDelay lblEvtAck;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbCmd;
        private System.Windows.Forms.Button btnStartCmd;
        internal LabelDelay lblCmdStep;
        internal LabelDelay lblCmdAck;
        internal LabelDelay lblCmdCmd;
        private System.Windows.Forms.Timer tmrUiTimer;
        internal System.Windows.Forms.Label lblMemName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnHsmsSave;
        private System.Windows.Forms.PropertyGrid pGridHsmsSetting;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnEvent;
        private System.Windows.Forms.Button btnHostECIDCheck;
        private System.Windows.Forms.RichTextBox rtbEcid;
    }
}
