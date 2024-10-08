namespace EquipMainUi.Setting
{
    partial class FrmTransferDataMgr
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbFromLPM2Slot = new System.Windows.Forms.ComboBox();
            this.cbFromLPM1Slot = new System.Windows.Forms.ComboBox();
            this.rbFromLPM2 = new System.Windows.Forms.RadioButton();
            this.rbFromLPM1 = new System.Windows.Forms.RadioButton();
            this.rbFromRobotLower = new System.Windows.Forms.RadioButton();
            this.rbFromRobotUpper = new System.Windows.Forms.RadioButton();
            this.rbFromAVI = new System.Windows.Forms.RadioButton();
            this.rbFromAligner = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbToLPM2 = new System.Windows.Forms.RadioButton();
            this.rbToLPM1 = new System.Windows.Forms.RadioButton();
            this.rbToRobotLower = new System.Windows.Forms.RadioButton();
            this.rbToRobotUpper = new System.Windows.Forms.RadioButton();
            this.rbToAVI = new System.Windows.Forms.RadioButton();
            this.rbToAligner = new System.Windows.Forms.RadioButton();
            this.lblFromPort = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.lblFromCstID = new System.Windows.Forms.Label();
            this.lblFromSlot = new System.Windows.Forms.Label();
            this.btnShift = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.label3 = new System.Windows.Forms.Label();
            this.lblToPort = new System.Windows.Forms.Label();
            this.lblToCstID = new System.Windows.Forms.Label();
            this.lblToSlot = new System.Windows.Forms.Label();
            this.pGrid = new System.Windows.Forms.PropertyGrid();
            this.btnClear = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnRestore = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.tmrUiUpdate = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnLoadPortMgrRestore = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnLoadPortMgrClear = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLoadPortMgrPort = new System.Windows.Forms.Label();
            this.lblLoadPortMgrID = new System.Windows.Forms.Label();
            this.rdLoadPortMgr2 = new System.Windows.Forms.RadioButton();
            this.rdLoadPortMgr1 = new System.Windows.Forms.RadioButton();
            this.ucrlLowerKeyInfo = new EquipMainUi.Setting.TransferData.UcrlTransferDataKeyInfo();
            this.ucrlUpperKeyInfo = new EquipMainUi.Setting.TransferData.UcrlTransferDataKeyInfo();
            this.ucrlAlignerKeyInfo = new EquipMainUi.Setting.TransferData.UcrlTransferDataKeyInfo();
            this.ucrlLPM2keyInfo = new EquipMainUi.Setting.TransferData.UcrlTransferDataKeyInfo();
            this.ucrlLPM1keyInfo = new EquipMainUi.Setting.TransferData.UcrlTransferDataKeyInfo();
            this.ucrlAVIKeyInfo = new EquipMainUi.Setting.TransferData.UcrlTransferDataKeyInfo();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbFromLPM2Slot);
            this.groupBox1.Controls.Add(this.cbFromLPM1Slot);
            this.groupBox1.Controls.Add(this.rbFromLPM2);
            this.groupBox1.Controls.Add(this.rbFromLPM1);
            this.groupBox1.Controls.Add(this.rbFromRobotLower);
            this.groupBox1.Controls.Add(this.rbFromRobotUpper);
            this.groupBox1.Controls.Add(this.rbFromAVI);
            this.groupBox1.Controls.Add(this.rbFromAligner);
            this.groupBox1.Location = new System.Drawing.Point(6, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 88);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "From";
            // 
            // cbFromLPM2Slot
            // 
            this.cbFromLPM2Slot.FormattingEnabled = true;
            this.cbFromLPM2Slot.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13"});
            this.cbFromLPM2Slot.Location = new System.Drawing.Point(263, 60);
            this.cbFromLPM2Slot.Name = "cbFromLPM2Slot";
            this.cbFromLPM2Slot.Size = new System.Drawing.Size(52, 20);
            this.cbFromLPM2Slot.TabIndex = 7;
            this.cbFromLPM2Slot.SelectedIndexChanged += new System.EventHandler(this.cbFromLPM2Slot_SelectedIndexChanged);
            // 
            // cbFromLPM1Slot
            // 
            this.cbFromLPM1Slot.FormattingEnabled = true;
            this.cbFromLPM1Slot.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13"});
            this.cbFromLPM1Slot.Location = new System.Drawing.Point(263, 37);
            this.cbFromLPM1Slot.Name = "cbFromLPM1Slot";
            this.cbFromLPM1Slot.Size = new System.Drawing.Size(52, 20);
            this.cbFromLPM1Slot.TabIndex = 6;
            this.cbFromLPM1Slot.SelectedIndexChanged += new System.EventHandler(this.cbFromLPM1Slot_SelectedIndexChanged);
            // 
            // rbFromLPM2
            // 
            this.rbFromLPM2.AutoSize = true;
            this.rbFromLPM2.Location = new System.Drawing.Point(202, 62);
            this.rbFromLPM2.Name = "rbFromLPM2";
            this.rbFromLPM2.Size = new System.Drawing.Size(55, 16);
            this.rbFromLPM2.TabIndex = 5;
            this.rbFromLPM2.TabStop = true;
            this.rbFromLPM2.Text = "LPM2";
            this.rbFromLPM2.UseVisualStyleBackColor = true;
            this.rbFromLPM2.Click += new System.EventHandler(this.rbTo_click);
            // 
            // rbFromLPM1
            // 
            this.rbFromLPM1.AutoSize = true;
            this.rbFromLPM1.Location = new System.Drawing.Point(202, 39);
            this.rbFromLPM1.Name = "rbFromLPM1";
            this.rbFromLPM1.Size = new System.Drawing.Size(55, 16);
            this.rbFromLPM1.TabIndex = 4;
            this.rbFromLPM1.TabStop = true;
            this.rbFromLPM1.Text = "LPM1";
            this.rbFromLPM1.UseVisualStyleBackColor = true;
            this.rbFromLPM1.Click += new System.EventHandler(this.rbTo_click);
            // 
            // rbFromRobotLower
            // 
            this.rbFromRobotLower.AutoSize = true;
            this.rbFromRobotLower.Location = new System.Drawing.Point(82, 62);
            this.rbFromRobotLower.Name = "rbFromRobotLower";
            this.rbFromRobotLower.Size = new System.Drawing.Size(96, 16);
            this.rbFromRobotLower.TabIndex = 3;
            this.rbFromRobotLower.TabStop = true;
            this.rbFromRobotLower.Text = "Robot-Lower";
            this.rbFromRobotLower.UseVisualStyleBackColor = true;
            this.rbFromRobotLower.Click += new System.EventHandler(this.rbFrom_click);
            // 
            // rbFromRobotUpper
            // 
            this.rbFromRobotUpper.AutoSize = true;
            this.rbFromRobotUpper.Location = new System.Drawing.Point(82, 39);
            this.rbFromRobotUpper.Name = "rbFromRobotUpper";
            this.rbFromRobotUpper.Size = new System.Drawing.Size(94, 16);
            this.rbFromRobotUpper.TabIndex = 2;
            this.rbFromRobotUpper.TabStop = true;
            this.rbFromRobotUpper.Text = "Robot-Upper";
            this.rbFromRobotUpper.UseVisualStyleBackColor = true;
            this.rbFromRobotUpper.Click += new System.EventHandler(this.rbFrom_click);
            // 
            // rbFromAVI
            // 
            this.rbFromAVI.AutoSize = true;
            this.rbFromAVI.Location = new System.Drawing.Point(6, 39);
            this.rbFromAVI.Name = "rbFromAVI";
            this.rbFromAVI.Size = new System.Drawing.Size(42, 16);
            this.rbFromAVI.TabIndex = 1;
            this.rbFromAVI.TabStop = true;
            this.rbFromAVI.Text = "AVI";
            this.rbFromAVI.UseVisualStyleBackColor = true;
            this.rbFromAVI.Click += new System.EventHandler(this.rbFrom_click);
            // 
            // rbFromAligner
            // 
            this.rbFromAligner.AutoSize = true;
            this.rbFromAligner.Location = new System.Drawing.Point(82, 17);
            this.rbFromAligner.Name = "rbFromAligner";
            this.rbFromAligner.Size = new System.Drawing.Size(62, 16);
            this.rbFromAligner.TabIndex = 0;
            this.rbFromAligner.TabStop = true;
            this.rbFromAligner.Text = "Aligner";
            this.rbFromAligner.UseVisualStyleBackColor = true;
            this.rbFromAligner.Click += new System.EventHandler(this.rbFrom_click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbToLPM2);
            this.groupBox2.Controls.Add(this.rbToLPM1);
            this.groupBox2.Controls.Add(this.rbToRobotLower);
            this.groupBox2.Controls.Add(this.rbToRobotUpper);
            this.groupBox2.Controls.Add(this.rbToAVI);
            this.groupBox2.Controls.Add(this.rbToAligner);
            this.groupBox2.Location = new System.Drawing.Point(6, 340);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(321, 88);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "To";
            // 
            // rbToLPM2
            // 
            this.rbToLPM2.AutoSize = true;
            this.rbToLPM2.Location = new System.Drawing.Point(202, 62);
            this.rbToLPM2.Name = "rbToLPM2";
            this.rbToLPM2.Size = new System.Drawing.Size(55, 16);
            this.rbToLPM2.TabIndex = 5;
            this.rbToLPM2.TabStop = true;
            this.rbToLPM2.Text = "LPM2";
            this.rbToLPM2.UseVisualStyleBackColor = true;
            // 
            // rbToLPM1
            // 
            this.rbToLPM1.AutoSize = true;
            this.rbToLPM1.Location = new System.Drawing.Point(202, 39);
            this.rbToLPM1.Name = "rbToLPM1";
            this.rbToLPM1.Size = new System.Drawing.Size(55, 16);
            this.rbToLPM1.TabIndex = 4;
            this.rbToLPM1.TabStop = true;
            this.rbToLPM1.Text = "LPM1";
            this.rbToLPM1.UseVisualStyleBackColor = true;
            // 
            // rbToRobotLower
            // 
            this.rbToRobotLower.AutoSize = true;
            this.rbToRobotLower.Location = new System.Drawing.Point(82, 62);
            this.rbToRobotLower.Name = "rbToRobotLower";
            this.rbToRobotLower.Size = new System.Drawing.Size(96, 16);
            this.rbToRobotLower.TabIndex = 3;
            this.rbToRobotLower.TabStop = true;
            this.rbToRobotLower.Text = "Robot-Lower";
            this.rbToRobotLower.UseVisualStyleBackColor = true;
            this.rbToRobotLower.Click += new System.EventHandler(this.rbTo_click);
            // 
            // rbToRobotUpper
            // 
            this.rbToRobotUpper.AutoSize = true;
            this.rbToRobotUpper.Location = new System.Drawing.Point(82, 39);
            this.rbToRobotUpper.Name = "rbToRobotUpper";
            this.rbToRobotUpper.Size = new System.Drawing.Size(94, 16);
            this.rbToRobotUpper.TabIndex = 2;
            this.rbToRobotUpper.TabStop = true;
            this.rbToRobotUpper.Text = "Robot-Upper";
            this.rbToRobotUpper.UseVisualStyleBackColor = true;
            this.rbToRobotUpper.Click += new System.EventHandler(this.rbTo_click);
            // 
            // rbToAVI
            // 
            this.rbToAVI.AutoSize = true;
            this.rbToAVI.Location = new System.Drawing.Point(6, 39);
            this.rbToAVI.Name = "rbToAVI";
            this.rbToAVI.Size = new System.Drawing.Size(42, 16);
            this.rbToAVI.TabIndex = 1;
            this.rbToAVI.TabStop = true;
            this.rbToAVI.Text = "AVI";
            this.rbToAVI.UseVisualStyleBackColor = true;
            this.rbToAVI.Click += new System.EventHandler(this.rbTo_click);
            // 
            // rbToAligner
            // 
            this.rbToAligner.AutoSize = true;
            this.rbToAligner.Location = new System.Drawing.Point(82, 17);
            this.rbToAligner.Name = "rbToAligner";
            this.rbToAligner.Size = new System.Drawing.Size(62, 16);
            this.rbToAligner.TabIndex = 0;
            this.rbToAligner.TabStop = true;
            this.rbToAligner.Text = "Aligner";
            this.rbToAligner.UseVisualStyleBackColor = true;
            this.rbToAligner.Click += new System.EventHandler(this.rbTo_click);
            // 
            // lblFromPort
            // 
            this.lblFromPort.AutoEllipsis = true;
            this.lblFromPort.BackColor = System.Drawing.Color.White;
            this.lblFromPort.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFromPort.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblFromPort.ForeColor = System.Drawing.Color.Black;
            this.lblFromPort.Location = new System.Drawing.Point(86, 111);
            this.lblFromPort.Name = "lblFromPort";
            this.lblFromPort.Size = new System.Drawing.Size(81, 21);
            this.lblFromPort.TabIndex = 65;
            this.lblFromPort.Text = "port";
            this.lblFromPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label90
            // 
            this.label90.AutoEllipsis = true;
            this.label90.BackColor = System.Drawing.Color.Gainsboro;
            this.label90.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label90.ForeColor = System.Drawing.Color.Black;
            this.label90.Location = new System.Drawing.Point(8, 111);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(78, 22);
            this.label90.TabIndex = 64;
            this.label90.Text = "■ From";
            this.label90.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFromCstID
            // 
            this.lblFromCstID.AutoEllipsis = true;
            this.lblFromCstID.BackColor = System.Drawing.Color.White;
            this.lblFromCstID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFromCstID.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblFromCstID.ForeColor = System.Drawing.Color.Black;
            this.lblFromCstID.Location = new System.Drawing.Point(167, 111);
            this.lblFromCstID.Name = "lblFromCstID";
            this.lblFromCstID.Size = new System.Drawing.Size(98, 21);
            this.lblFromCstID.TabIndex = 65;
            this.lblFromCstID.Text = "cstID";
            this.lblFromCstID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFromSlot
            // 
            this.lblFromSlot.AutoEllipsis = true;
            this.lblFromSlot.BackColor = System.Drawing.Color.White;
            this.lblFromSlot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFromSlot.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblFromSlot.ForeColor = System.Drawing.Color.Black;
            this.lblFromSlot.Location = new System.Drawing.Point(265, 111);
            this.lblFromSlot.Name = "lblFromSlot";
            this.lblFromSlot.Size = new System.Drawing.Size(61, 21);
            this.lblFromSlot.TabIndex = 65;
            this.lblFromSlot.Text = "slot";
            this.lblFromSlot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnShift
            // 
            this.btnShift.BackColor = System.Drawing.Color.Transparent;
            this.btnShift.Delay = 100;
            this.btnShift.Flicker = false;
            this.btnShift.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnShift.ForeColor = System.Drawing.Color.Black;
            this.btnShift.IsLeftLampOn = false;
            this.btnShift.IsRightLampOn = false;
            this.btnShift.LampAliveTime = 500;
            this.btnShift.LampSize = 1;
            this.btnShift.LeftLampColor = System.Drawing.Color.Red;
            this.btnShift.Location = new System.Drawing.Point(6, 456);
            this.btnShift.Name = "btnShift";
            this.btnShift.OnOff = false;
            this.btnShift.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnShift.Size = new System.Drawing.Size(321, 38);
            this.btnShift.TabIndex = 86;
            this.btnShift.TabStop = false;
            this.btnShift.Text = "Shift";
            this.btnShift.Text2 = "";
            this.btnShift.UseVisualStyleBackColor = false;
            this.btnShift.VisibleLeftLamp = false;
            this.btnShift.VisibleRightLamp = false;
            this.btnShift.Click += new System.EventHandler(this.btnShift_Click);
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.BackColor = System.Drawing.Color.Gainsboro;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(8, 431);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 22);
            this.label3.TabIndex = 64;
            this.label3.Text = "■ To";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblToPort
            // 
            this.lblToPort.AutoEllipsis = true;
            this.lblToPort.BackColor = System.Drawing.Color.White;
            this.lblToPort.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblToPort.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblToPort.ForeColor = System.Drawing.Color.Black;
            this.lblToPort.Location = new System.Drawing.Point(86, 431);
            this.lblToPort.Name = "lblToPort";
            this.lblToPort.Size = new System.Drawing.Size(81, 21);
            this.lblToPort.TabIndex = 65;
            this.lblToPort.Text = "port";
            this.lblToPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblToCstID
            // 
            this.lblToCstID.AutoEllipsis = true;
            this.lblToCstID.BackColor = System.Drawing.Color.White;
            this.lblToCstID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblToCstID.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblToCstID.ForeColor = System.Drawing.Color.Black;
            this.lblToCstID.Location = new System.Drawing.Point(167, 431);
            this.lblToCstID.Name = "lblToCstID";
            this.lblToCstID.Size = new System.Drawing.Size(98, 21);
            this.lblToCstID.TabIndex = 65;
            this.lblToCstID.Text = "cstID";
            this.lblToCstID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblToSlot
            // 
            this.lblToSlot.AutoEllipsis = true;
            this.lblToSlot.BackColor = System.Drawing.Color.White;
            this.lblToSlot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblToSlot.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblToSlot.ForeColor = System.Drawing.Color.Black;
            this.lblToSlot.Location = new System.Drawing.Point(265, 431);
            this.lblToSlot.Name = "lblToSlot";
            this.lblToSlot.Size = new System.Drawing.Size(61, 21);
            this.lblToSlot.TabIndex = 65;
            this.lblToSlot.Text = "slot";
            this.lblToSlot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pGrid
            // 
            this.pGrid.Enabled = false;
            this.pGrid.HelpVisible = false;
            this.pGrid.Location = new System.Drawing.Point(6, 133);
            this.pGrid.Margin = new System.Windows.Forms.Padding(0);
            this.pGrid.Name = "pGrid";
            this.pGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pGrid.Size = new System.Drawing.Size(321, 204);
            this.pGrid.TabIndex = 447;
            this.pGrid.ToolbarVisible = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.Delay = 100;
            this.btnClear.Flicker = false;
            this.btnClear.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.IsLeftLampOn = false;
            this.btnClear.IsRightLampOn = false;
            this.btnClear.LampAliveTime = 500;
            this.btnClear.LampSize = 1;
            this.btnClear.LeftLampColor = System.Drawing.Color.Red;
            this.btnClear.Location = new System.Drawing.Point(330, 133);
            this.btnClear.Name = "btnClear";
            this.btnClear.OnOff = false;
            this.btnClear.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnClear.Size = new System.Drawing.Size(62, 100);
            this.btnClear.TabIndex = 86;
            this.btnClear.TabStop = false;
            this.btnClear.Text = "Clear";
            this.btnClear.Text2 = "";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.VisibleLeftLamp = false;
            this.btnClear.VisibleRightLamp = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.BackColor = System.Drawing.Color.Transparent;
            this.btnRestore.Delay = 100;
            this.btnRestore.Flicker = false;
            this.btnRestore.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnRestore.ForeColor = System.Drawing.Color.Black;
            this.btnRestore.IsLeftLampOn = false;
            this.btnRestore.IsRightLampOn = false;
            this.btnRestore.LampAliveTime = 500;
            this.btnRestore.LampSize = 1;
            this.btnRestore.LeftLampColor = System.Drawing.Color.Red;
            this.btnRestore.Location = new System.Drawing.Point(330, 237);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.OnOff = false;
            this.btnRestore.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnRestore.Size = new System.Drawing.Size(62, 100);
            this.btnRestore.TabIndex = 86;
            this.btnRestore.TabStop = false;
            this.btnRestore.Text = "Restore";
            this.btnRestore.Text2 = "";
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.VisibleLeftLamp = false;
            this.btnRestore.VisibleRightLamp = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.pGrid);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.btnRestore);
            this.groupBox3.Controls.Add(this.label90);
            this.groupBox3.Controls.Add(this.btnClear);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.btnShift);
            this.groupBox3.Controls.Add(this.lblFromPort);
            this.groupBox3.Controls.Add(this.lblToSlot);
            this.groupBox3.Controls.Add(this.lblToPort);
            this.groupBox3.Controls.Add(this.lblFromSlot);
            this.groupBox3.Controls.Add(this.lblFromCstID);
            this.groupBox3.Controls.Add(this.lblToCstID);
            this.groupBox3.Location = new System.Drawing.Point(425, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(400, 500);
            this.groupBox3.TabIndex = 448;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Wafer";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnLoadPortMgrRestore);
            this.groupBox4.Controls.Add(this.btnLoadPortMgrClear);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.lblLoadPortMgrPort);
            this.groupBox4.Controls.Add(this.lblLoadPortMgrID);
            this.groupBox4.Controls.Add(this.rdLoadPortMgr2);
            this.groupBox4.Controls.Add(this.rdLoadPortMgr1);
            this.groupBox4.Location = new System.Drawing.Point(851, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(155, 500);
            this.groupBox4.TabIndex = 449;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "LoadPort";
            // 
            // btnLoadPortMgrRestore
            // 
            this.btnLoadPortMgrRestore.BackColor = System.Drawing.Color.Transparent;
            this.btnLoadPortMgrRestore.Delay = 100;
            this.btnLoadPortMgrRestore.Flicker = false;
            this.btnLoadPortMgrRestore.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnLoadPortMgrRestore.ForeColor = System.Drawing.Color.Black;
            this.btnLoadPortMgrRestore.IsLeftLampOn = false;
            this.btnLoadPortMgrRestore.IsRightLampOn = false;
            this.btnLoadPortMgrRestore.LampAliveTime = 500;
            this.btnLoadPortMgrRestore.LampSize = 1;
            this.btnLoadPortMgrRestore.LeftLampColor = System.Drawing.Color.Red;
            this.btnLoadPortMgrRestore.Location = new System.Drawing.Point(80, 85);
            this.btnLoadPortMgrRestore.Name = "btnLoadPortMgrRestore";
            this.btnLoadPortMgrRestore.OnOff = false;
            this.btnLoadPortMgrRestore.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnLoadPortMgrRestore.Size = new System.Drawing.Size(67, 38);
            this.btnLoadPortMgrRestore.TabIndex = 87;
            this.btnLoadPortMgrRestore.TabStop = false;
            this.btnLoadPortMgrRestore.Text = "Restore";
            this.btnLoadPortMgrRestore.Text2 = "";
            this.btnLoadPortMgrRestore.UseVisualStyleBackColor = false;
            this.btnLoadPortMgrRestore.VisibleLeftLamp = false;
            this.btnLoadPortMgrRestore.VisibleRightLamp = false;
            this.btnLoadPortMgrRestore.Click += new System.EventHandler(this.btnLoadPortMgrClear_Click);
            // 
            // btnLoadPortMgrClear
            // 
            this.btnLoadPortMgrClear.BackColor = System.Drawing.Color.Transparent;
            this.btnLoadPortMgrClear.Delay = 100;
            this.btnLoadPortMgrClear.Flicker = false;
            this.btnLoadPortMgrClear.Font = new System.Drawing.Font("맑은 고딕", 9.2F);
            this.btnLoadPortMgrClear.ForeColor = System.Drawing.Color.Black;
            this.btnLoadPortMgrClear.IsLeftLampOn = false;
            this.btnLoadPortMgrClear.IsRightLampOn = false;
            this.btnLoadPortMgrClear.LampAliveTime = 500;
            this.btnLoadPortMgrClear.LampSize = 1;
            this.btnLoadPortMgrClear.LeftLampColor = System.Drawing.Color.Red;
            this.btnLoadPortMgrClear.Location = new System.Drawing.Point(9, 85);
            this.btnLoadPortMgrClear.Name = "btnLoadPortMgrClear";
            this.btnLoadPortMgrClear.OnOff = false;
            this.btnLoadPortMgrClear.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnLoadPortMgrClear.Size = new System.Drawing.Size(67, 38);
            this.btnLoadPortMgrClear.TabIndex = 88;
            this.btnLoadPortMgrClear.TabStop = false;
            this.btnLoadPortMgrClear.Text = "Clear";
            this.btnLoadPortMgrClear.Text2 = "";
            this.btnLoadPortMgrClear.UseVisualStyleBackColor = false;
            this.btnLoadPortMgrClear.VisibleLeftLamp = false;
            this.btnLoadPortMgrClear.VisibleRightLamp = false;
            this.btnLoadPortMgrClear.Click += new System.EventHandler(this.btnLoadPortMgrClear_Click);
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(9, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 22);
            this.label1.TabIndex = 66;
            this.label1.Text = "■ Select";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoadPortMgrPort
            // 
            this.lblLoadPortMgrPort.AutoEllipsis = true;
            this.lblLoadPortMgrPort.BackColor = System.Drawing.Color.White;
            this.lblLoadPortMgrPort.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLoadPortMgrPort.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblLoadPortMgrPort.ForeColor = System.Drawing.Color.Black;
            this.lblLoadPortMgrPort.Location = new System.Drawing.Point(66, 39);
            this.lblLoadPortMgrPort.Name = "lblLoadPortMgrPort";
            this.lblLoadPortMgrPort.Size = new System.Drawing.Size(81, 21);
            this.lblLoadPortMgrPort.TabIndex = 67;
            this.lblLoadPortMgrPort.Text = "port";
            this.lblLoadPortMgrPort.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoadPortMgrID
            // 
            this.lblLoadPortMgrID.AutoEllipsis = true;
            this.lblLoadPortMgrID.BackColor = System.Drawing.Color.White;
            this.lblLoadPortMgrID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLoadPortMgrID.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.lblLoadPortMgrID.ForeColor = System.Drawing.Color.Black;
            this.lblLoadPortMgrID.Location = new System.Drawing.Point(9, 61);
            this.lblLoadPortMgrID.Name = "lblLoadPortMgrID";
            this.lblLoadPortMgrID.Size = new System.Drawing.Size(138, 21);
            this.lblLoadPortMgrID.TabIndex = 68;
            this.lblLoadPortMgrID.Text = "cstID";
            this.lblLoadPortMgrID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdLoadPortMgr2
            // 
            this.rdLoadPortMgr2.AutoSize = true;
            this.rdLoadPortMgr2.Location = new System.Drawing.Point(70, 20);
            this.rdLoadPortMgr2.Name = "rdLoadPortMgr2";
            this.rdLoadPortMgr2.Size = new System.Drawing.Size(55, 16);
            this.rdLoadPortMgr2.TabIndex = 7;
            this.rdLoadPortMgr2.TabStop = true;
            this.rdLoadPortMgr2.Text = "LPM2";
            this.rdLoadPortMgr2.UseVisualStyleBackColor = true;
            this.rdLoadPortMgr2.CheckedChanged += new System.EventHandler(this.rdLoadPortMgr1_CheckedChanged);
            // 
            // rdLoadPortMgr1
            // 
            this.rdLoadPortMgr1.AutoSize = true;
            this.rdLoadPortMgr1.Location = new System.Drawing.Point(9, 20);
            this.rdLoadPortMgr1.Name = "rdLoadPortMgr1";
            this.rdLoadPortMgr1.Size = new System.Drawing.Size(55, 16);
            this.rdLoadPortMgr1.TabIndex = 6;
            this.rdLoadPortMgr1.TabStop = true;
            this.rdLoadPortMgr1.Text = "LPM1";
            this.rdLoadPortMgr1.UseVisualStyleBackColor = true;
            this.rdLoadPortMgr1.CheckedChanged += new System.EventHandler(this.rdLoadPortMgr1_CheckedChanged);
            // 
            // ucrlLowerKeyInfo
            // 
            this.ucrlLowerKeyInfo.AllowDrop = true;
            this.ucrlLowerKeyInfo.Location = new System.Drawing.Point(148, 332);
            this.ucrlLowerKeyInfo.Name = "ucrlLowerKeyInfo";
            this.ucrlLowerKeyInfo.Size = new System.Drawing.Size(130, 154);
            this.ucrlLowerKeyInfo.TabIndex = 0;
            this.ucrlLowerKeyInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.ucrlKeyInfo_DragDrop);
            this.ucrlLowerKeyInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.ucrlKeyInfo_DragEnter);
            this.ucrlLowerKeyInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ucrlkeyInfo_MouseDown);
            // 
            // ucrlUpperKeyInfo
            // 
            this.ucrlUpperKeyInfo.AllowDrop = true;
            this.ucrlUpperKeyInfo.Location = new System.Drawing.Point(148, 172);
            this.ucrlUpperKeyInfo.Name = "ucrlUpperKeyInfo";
            this.ucrlUpperKeyInfo.Size = new System.Drawing.Size(130, 154);
            this.ucrlUpperKeyInfo.TabIndex = 0;
            this.ucrlUpperKeyInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.ucrlKeyInfo_DragDrop);
            this.ucrlUpperKeyInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.ucrlKeyInfo_DragEnter);
            this.ucrlUpperKeyInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ucrlkeyInfo_MouseDown);
            // 
            // ucrlAlignerKeyInfo
            // 
            this.ucrlAlignerKeyInfo.AllowDrop = true;
            this.ucrlAlignerKeyInfo.Location = new System.Drawing.Point(148, 12);
            this.ucrlAlignerKeyInfo.Name = "ucrlAlignerKeyInfo";
            this.ucrlAlignerKeyInfo.Size = new System.Drawing.Size(130, 154);
            this.ucrlAlignerKeyInfo.TabIndex = 0;
            this.ucrlAlignerKeyInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.ucrlKeyInfo_DragDrop);
            this.ucrlAlignerKeyInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.ucrlKeyInfo_DragEnter);
            this.ucrlAlignerKeyInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ucrlkeyInfo_MouseDown);
            // 
            // ucrlLPM2keyInfo
            // 
            this.ucrlLPM2keyInfo.Location = new System.Drawing.Point(284, 332);
            this.ucrlLPM2keyInfo.Name = "ucrlLPM2keyInfo";
            this.ucrlLPM2keyInfo.Size = new System.Drawing.Size(130, 154);
            this.ucrlLPM2keyInfo.TabIndex = 0;
            // 
            // ucrlLPM1keyInfo
            // 
            this.ucrlLPM1keyInfo.Location = new System.Drawing.Point(284, 172);
            this.ucrlLPM1keyInfo.Name = "ucrlLPM1keyInfo";
            this.ucrlLPM1keyInfo.Size = new System.Drawing.Size(130, 154);
            this.ucrlLPM1keyInfo.TabIndex = 0;
            // 
            // ucrlAVIKeyInfo
            // 
            this.ucrlAVIKeyInfo.AllowDrop = true;
            this.ucrlAVIKeyInfo.Location = new System.Drawing.Point(12, 172);
            this.ucrlAVIKeyInfo.Name = "ucrlAVIKeyInfo";
            this.ucrlAVIKeyInfo.Size = new System.Drawing.Size(130, 154);
            this.ucrlAVIKeyInfo.TabIndex = 0;
            this.ucrlAVIKeyInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.ucrlKeyInfo_DragDrop);
            this.ucrlAVIKeyInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.ucrlKeyInfo_DragEnter);
            this.ucrlAVIKeyInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ucrlkeyInfo_MouseDown);
            // 
            // FrmTransferDataMgr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 579);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.ucrlLowerKeyInfo);
            this.Controls.Add(this.ucrlUpperKeyInfo);
            this.Controls.Add(this.ucrlAlignerKeyInfo);
            this.Controls.Add(this.ucrlLPM2keyInfo);
            this.Controls.Add(this.ucrlLPM1keyInfo);
            this.Controls.Add(this.ucrlAVIKeyInfo);
            this.Name = "FrmTransferDataMgr";
            this.Text = "물류정보이동";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTransferDataMgr_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TransferData.UcrlTransferDataKeyInfo ucrlAVIKeyInfo;
        private TransferData.UcrlTransferDataKeyInfo ucrlAlignerKeyInfo;
        private TransferData.UcrlTransferDataKeyInfo ucrlUpperKeyInfo;
        private TransferData.UcrlTransferDataKeyInfo ucrlLowerKeyInfo;
        private TransferData.UcrlTransferDataKeyInfo ucrlLPM1keyInfo;
        private TransferData.UcrlTransferDataKeyInfo ucrlLPM2keyInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbFromLPM2Slot;
        private System.Windows.Forms.ComboBox cbFromLPM1Slot;
        private System.Windows.Forms.RadioButton rbFromLPM2;
        private System.Windows.Forms.RadioButton rbFromLPM1;
        private System.Windows.Forms.RadioButton rbFromRobotLower;
        private System.Windows.Forms.RadioButton rbFromRobotUpper;
        private System.Windows.Forms.RadioButton rbFromAVI;
        private System.Windows.Forms.RadioButton rbFromAligner;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbToLPM2;
        private System.Windows.Forms.RadioButton rbToLPM1;
        private System.Windows.Forms.RadioButton rbToRobotLower;
        private System.Windows.Forms.RadioButton rbToRobotUpper;
        private System.Windows.Forms.RadioButton rbToAVI;
        private System.Windows.Forms.RadioButton rbToAligner;
        internal System.Windows.Forms.Label lblFromPort;
        internal System.Windows.Forms.Label label90;
        internal System.Windows.Forms.Label lblFromCstID;
        internal System.Windows.Forms.Label lblFromSlot;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnShift;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label lblToPort;
        internal System.Windows.Forms.Label lblToCstID;
        internal System.Windows.Forms.Label lblToSlot;
        private System.Windows.Forms.PropertyGrid pGrid;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnClear;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnRestore;
        private System.Windows.Forms.Timer tmrUiUpdate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnLoadPortMgrRestore;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnLoadPortMgrClear;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label lblLoadPortMgrPort;
        internal System.Windows.Forms.Label lblLoadPortMgrID;
        private System.Windows.Forms.RadioButton rdLoadPortMgr2;
        private System.Windows.Forms.RadioButton rdLoadPortMgr1;
    }
}