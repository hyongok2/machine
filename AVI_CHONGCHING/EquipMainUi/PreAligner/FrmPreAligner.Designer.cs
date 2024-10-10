namespace EquipMainUi.PreAligner
{
    partial class FrmPreAligner
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("test");
            this.panelMenu = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lstLog = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabAlignResult = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.pGridResult = new System.Windows.Forms.PropertyGrid();
            this.pGridRecipe = new EquipMainUi.Common.Convenience.PropertyGridEx();
            this.label4 = new System.Windows.Forms.Label();
            this.tabDefectResult = new System.Windows.Forms.TabPage();
            this.dgvDefect = new System.Windows.Forms.DataGridView();
            this.ColumnHead = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabRecipe = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStartNotchROISetting = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnCreateDefaultRecipe = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnDelete = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnUpdate = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnInsert = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSetRecipe = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnFixedRecipe = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.lblSelectedRecipe = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pGridSelectedRecipe = new System.Windows.Forms.PropertyGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.lstRcps = new Dit.Framework.UI.UserComponent.ListViewEx();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabManual = new System.Windows.Forms.TabPage();
            this.lblDllVersion = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblStep = new System.Windows.Forms.Label();
            this.btnStopGrabSeq = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnStartGrabSeq = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucrlLightControllerTest1 = new EquipMainUi.PreAligner.UcrlLightControllerTest();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLive = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnConnect = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnSetExposure = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.txtExposureTime = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.BtnLoadImage = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnProcessing = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnStopSnap = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnSnap = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.tabOperator = new System.Windows.Forms.TabPage();
            this.panel27 = new System.Windows.Forms.Panel();
            this.btnOcrDown = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnOcrUp = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.label80 = new System.Windows.Forms.Label();
            this.panel26 = new System.Windows.Forms.Panel();
            this.btnAlignVacOffBlowerOn = new System.Windows.Forms.Button();
            this.btnAlignVacuumOff = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnAlignBlowerOn = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnAlignVacuumOn = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.label41 = new System.Windows.Forms.Label();
            this.panel24 = new System.Windows.Forms.Panel();
            this.btnAlignYPtpMove = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.ucrlPtpY = new EquipMainUi.Monitor.UcrlPtp();
            this.label36 = new System.Windows.Forms.Label();
            this.tbAlignYJogSpd = new System.Windows.Forms.TextBox();
            this.cbAlignY = new System.Windows.Forms.ComboBox();
            this.lblAlignYError = new Dit.Framework.UI.UserComponent.LabelDelay();
            this.tbAlignYCurPosition = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.lblAlignYServoOn = new System.Windows.Forms.Label();
            this.lblAlignYPlusLimit = new System.Windows.Forms.Label();
            this.lblAlignYMinusLimit = new System.Windows.Forms.Label();
            this.lblAlignYMoving = new System.Windows.Forms.Label();
            this.btnAlignYJogMinus = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnAlignYJogPlus = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.lblAlignYHomeBit = new System.Windows.Forms.Label();
            this.btnAlignYHome = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.panel25 = new System.Windows.Forms.Panel();
            this.btnAlignTPtpMove = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.label37 = new System.Windows.Forms.Label();
            this.ucrlPtpT = new EquipMainUi.Monitor.UcrlPtp();
            this.tbAlignTJogSpd = new System.Windows.Forms.TextBox();
            this.tbAlignTCurPosition = new System.Windows.Forms.TextBox();
            this.cbAlignT = new System.Windows.Forms.ComboBox();
            this.lblAlignTError = new Dit.Framework.UI.UserComponent.LabelDelay();
            this.label61 = new System.Windows.Forms.Label();
            this.lblAlignTServoOn = new System.Windows.Forms.Label();
            this.lblAlignTOriginOn = new System.Windows.Forms.Label();
            this.lblAlignTMoving = new System.Windows.Forms.Label();
            this.btnAlignTJogMinus = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnAlignTJogPlus = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.lblAlignTHomeBit = new System.Windows.Forms.Label();
            this.btnAlignTHome = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.panel18 = new System.Windows.Forms.Panel();
            this.btnAlignXPtpMove = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.label38 = new System.Windows.Forms.Label();
            this.ucrlPtpX = new EquipMainUi.Monitor.UcrlPtp();
            this.tbAlignXJogSpd = new System.Windows.Forms.TextBox();
            this.tbAlignXCurPosition = new System.Windows.Forms.TextBox();
            this.cbAlignX = new System.Windows.Forms.ComboBox();
            this.lblAlignXError = new Dit.Framework.UI.UserComponent.LabelDelay();
            this.label39 = new System.Windows.Forms.Label();
            this.lblAlignXServoOn = new System.Windows.Forms.Label();
            this.lblAlignXPlusLimit = new System.Windows.Forms.Label();
            this.lblAlignXMinusLimit = new System.Windows.Forms.Label();
            this.lblAlignXMoving = new System.Windows.Forms.Label();
            this.btnAlignXJogMinus = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnAlignXJogPlus = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.lblAlignXHomeBit = new System.Windows.Forms.Label();
            this.btnAlignXHome = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.tmr_UiUpdate = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelMenu.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabAlignResult.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabDefectResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefect)).BeginInit();
            this.tabRecipe.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabManual.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabOperator.SuspendLayout();
            this.panel27.SuspendLayout();
            this.panel26.SuspendLayout();
            this.panel24.SuspendLayout();
            this.panel25.SuspendLayout();
            this.panel18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.Controls.Add(this.tableLayoutPanel4);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(350, 861);
            this.panelMenu.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.lstLog, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(350, 861);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // lstLog
            // 
            this.lstLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLog.HideSelection = false;
            this.lstLog.Location = new System.Drawing.Point(3, 714);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(344, 144);
            this.lstLog.TabIndex = 5;
            this.lstLog.UseCompatibleStateImageBehavior = false;
            this.lstLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Time";
            this.columnHeader1.Width = 75;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Log";
            this.columnHeader2.Width = 300;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabAlignResult);
            this.tabControl1.Controls.Add(this.tabDefectResult);
            this.tabControl1.Controls.Add(this.tabRecipe);
            this.tabControl1.Controls.Add(this.tabManual);
            this.tabControl1.Controls.Add(this.tabOperator);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(344, 705);
            this.tabControl1.TabIndex = 3;
            // 
            // tabAlignResult
            // 
            this.tabAlignResult.Controls.Add(this.tableLayoutPanel2);
            this.tabAlignResult.Location = new System.Drawing.Point(4, 22);
            this.tabAlignResult.Name = "tabAlignResult";
            this.tabAlignResult.Padding = new System.Windows.Forms.Padding(3);
            this.tabAlignResult.Size = new System.Drawing.Size(336, 679);
            this.tabAlignResult.TabIndex = 0;
            this.tabAlignResult.Text = "Align결과";
            this.tabAlignResult.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pGridResult, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.pGridRecipe, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(330, 673);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.BackColor = System.Drawing.Color.Gainsboro;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(1, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(328, 18);
            this.label3.TabIndex = 457;
            this.label3.Text = "■ Align 결과";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pGridResult
            // 
            this.pGridResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pGridResult.Location = new System.Drawing.Point(3, 23);
            this.pGridResult.Name = "pGridResult";
            this.pGridResult.Size = new System.Drawing.Size(324, 310);
            this.pGridResult.TabIndex = 0;
            // 
            // pGridRecipe
            // 
            this.pGridRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pGridRecipe.Location = new System.Drawing.Point(3, 359);
            this.pGridRecipe.Name = "pGridRecipe";
            this.pGridRecipe.Size = new System.Drawing.Size(324, 311);
            this.pGridRecipe.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.BackColor = System.Drawing.Color.Gainsboro;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(1, 337);
            this.label4.Margin = new System.Windows.Forms.Padding(1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(328, 18);
            this.label4.TabIndex = 457;
            this.label4.Text = "■ Align Recipe";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabDefectResult
            // 
            this.tabDefectResult.Controls.Add(this.dgvDefect);
            this.tabDefectResult.Location = new System.Drawing.Point(4, 22);
            this.tabDefectResult.Name = "tabDefectResult";
            this.tabDefectResult.Padding = new System.Windows.Forms.Padding(3);
            this.tabDefectResult.Size = new System.Drawing.Size(336, 679);
            this.tabDefectResult.TabIndex = 1;
            this.tabDefectResult.Text = "검사 결과";
            this.tabDefectResult.UseVisualStyleBackColor = true;
            // 
            // dgvDefect
            // 
            this.dgvDefect.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDefect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDefect.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnHead,
            this.ColumnValue});
            this.dgvDefect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDefect.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDefect.Location = new System.Drawing.Point(3, 3);
            this.dgvDefect.MultiSelect = false;
            this.dgvDefect.Name = "dgvDefect";
            this.dgvDefect.RowHeadersVisible = false;
            this.dgvDefect.RowTemplate.Height = 23;
            this.dgvDefect.Size = new System.Drawing.Size(330, 673);
            this.dgvDefect.TabIndex = 1;
            // 
            // ColumnHead
            // 
            this.ColumnHead.DividerWidth = 1;
            this.ColumnHead.HeaderText = "Item";
            this.ColumnHead.Name = "ColumnHead";
            this.ColumnHead.ReadOnly = true;
            this.ColumnHead.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnHead.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnHead.Width = 150;
            // 
            // ColumnValue
            // 
            this.ColumnValue.HeaderText = "Value";
            this.ColumnValue.Name = "ColumnValue";
            this.ColumnValue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnValue.Width = 150;
            // 
            // tabRecipe
            // 
            this.tabRecipe.Controls.Add(this.tableLayoutPanel1);
            this.tabRecipe.Location = new System.Drawing.Point(4, 22);
            this.tabRecipe.Name = "tabRecipe";
            this.tabRecipe.Size = new System.Drawing.Size(336, 679);
            this.tabRecipe.TabIndex = 3;
            this.tabRecipe.Text = "Recipe";
            this.tabRecipe.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pGridSelectedRecipe, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lstRcps, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(336, 679);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnStartNotchROISetting);
            this.panel1.Controls.Add(this.btnCreateDefaultRecipe);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.btnInsert);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 603);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(336, 76);
            this.panel1.TabIndex = 459;
            // 
            // btnStartNotchROISetting
            // 
            this.btnStartNotchROISetting.BackColor = System.Drawing.Color.Transparent;
            this.btnStartNotchROISetting.Delay = 2;
            this.btnStartNotchROISetting.Flicker = false;
            this.btnStartNotchROISetting.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnStartNotchROISetting.ForeColor = System.Drawing.Color.Black;
            this.btnStartNotchROISetting.IsLeftLampOn = false;
            this.btnStartNotchROISetting.IsRightLampOn = false;
            this.btnStartNotchROISetting.LampAliveTime = 500;
            this.btnStartNotchROISetting.LampSize = 1;
            this.btnStartNotchROISetting.LeftLampColor = System.Drawing.Color.Red;
            this.btnStartNotchROISetting.Location = new System.Drawing.Point(116, 3);
            this.btnStartNotchROISetting.Name = "btnStartNotchROISetting";
            this.btnStartNotchROISetting.OnOff = false;
            this.btnStartNotchROISetting.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnStartNotchROISetting.Size = new System.Drawing.Size(105, 22);
            this.btnStartNotchROISetting.TabIndex = 452;
            this.btnStartNotchROISetting.TabStop = false;
            this.btnStartNotchROISetting.Text = "Notch ROI 설정";
            this.btnStartNotchROISetting.Text2 = "";
            this.btnStartNotchROISetting.UseVisualStyleBackColor = false;
            this.btnStartNotchROISetting.VisibleLeftLamp = false;
            this.btnStartNotchROISetting.VisibleRightLamp = false;
            this.btnStartNotchROISetting.Click += new System.EventHandler(this.btnStartNotchROISetting_Click);
            // 
            // btnCreateDefaultRecipe
            // 
            this.btnCreateDefaultRecipe.BackColor = System.Drawing.Color.Transparent;
            this.btnCreateDefaultRecipe.Delay = 2;
            this.btnCreateDefaultRecipe.Flicker = false;
            this.btnCreateDefaultRecipe.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnCreateDefaultRecipe.ForeColor = System.Drawing.Color.Black;
            this.btnCreateDefaultRecipe.IsLeftLampOn = false;
            this.btnCreateDefaultRecipe.IsRightLampOn = false;
            this.btnCreateDefaultRecipe.LampAliveTime = 500;
            this.btnCreateDefaultRecipe.LampSize = 1;
            this.btnCreateDefaultRecipe.LeftLampColor = System.Drawing.Color.Red;
            this.btnCreateDefaultRecipe.Location = new System.Drawing.Point(6, 3);
            this.btnCreateDefaultRecipe.Name = "btnCreateDefaultRecipe";
            this.btnCreateDefaultRecipe.OnOff = false;
            this.btnCreateDefaultRecipe.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnCreateDefaultRecipe.Size = new System.Drawing.Size(105, 22);
            this.btnCreateDefaultRecipe.TabIndex = 451;
            this.btnCreateDefaultRecipe.TabStop = false;
            this.btnCreateDefaultRecipe.Text = "Default값";
            this.btnCreateDefaultRecipe.Text2 = "";
            this.btnCreateDefaultRecipe.UseVisualStyleBackColor = false;
            this.btnCreateDefaultRecipe.VisibleLeftLamp = false;
            this.btnCreateDefaultRecipe.VisibleRightLamp = false;
            this.btnCreateDefaultRecipe.Click += new System.EventHandler(this.btnCreateDefaultRecipe_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.Delay = 2;
            this.btnDelete.Flicker = false;
            this.btnDelete.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.IsLeftLampOn = false;
            this.btnDelete.IsRightLampOn = false;
            this.btnDelete.LampAliveTime = 500;
            this.btnDelete.LampSize = 1;
            this.btnDelete.LeftLampColor = System.Drawing.Color.Red;
            this.btnDelete.Location = new System.Drawing.Point(226, 29);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.OnOff = false;
            this.btnDelete.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnDelete.Size = new System.Drawing.Size(105, 44);
            this.btnDelete.TabIndex = 450;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "삭제";
            this.btnDelete.Text2 = "";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.VisibleLeftLamp = false;
            this.btnDelete.VisibleRightLamp = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Transparent;
            this.btnUpdate.Delay = 2;
            this.btnUpdate.Flicker = false;
            this.btnUpdate.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnUpdate.ForeColor = System.Drawing.Color.Black;
            this.btnUpdate.IsLeftLampOn = false;
            this.btnUpdate.IsRightLampOn = false;
            this.btnUpdate.LampAliveTime = 500;
            this.btnUpdate.LampSize = 1;
            this.btnUpdate.LeftLampColor = System.Drawing.Color.Red;
            this.btnUpdate.Location = new System.Drawing.Point(116, 29);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.OnOff = false;
            this.btnUpdate.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnUpdate.Size = new System.Drawing.Size(105, 44);
            this.btnUpdate.TabIndex = 450;
            this.btnUpdate.TabStop = false;
            this.btnUpdate.Text = "수정";
            this.btnUpdate.Text2 = "";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.VisibleLeftLamp = false;
            this.btnUpdate.VisibleRightLamp = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnInsert
            // 
            this.btnInsert.BackColor = System.Drawing.Color.Transparent;
            this.btnInsert.Delay = 2;
            this.btnInsert.Flicker = false;
            this.btnInsert.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnInsert.ForeColor = System.Drawing.Color.Black;
            this.btnInsert.IsLeftLampOn = false;
            this.btnInsert.IsRightLampOn = false;
            this.btnInsert.LampAliveTime = 500;
            this.btnInsert.LampSize = 1;
            this.btnInsert.LeftLampColor = System.Drawing.Color.Red;
            this.btnInsert.Location = new System.Drawing.Point(6, 29);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.OnOff = false;
            this.btnInsert.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnInsert.Size = new System.Drawing.Size(105, 44);
            this.btnInsert.TabIndex = 450;
            this.btnInsert.TabStop = false;
            this.btnInsert.Text = "생성";
            this.btnInsert.Text2 = "";
            this.btnInsert.UseVisualStyleBackColor = false;
            this.btnInsert.VisibleLeftLamp = false;
            this.btnInsert.VisibleRightLamp = false;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSetRecipe);
            this.panel2.Controls.Add(this.btnFixedRecipe);
            this.panel2.Controls.Add(this.lblSelectedRecipe);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 257);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(336, 20);
            this.panel2.TabIndex = 461;
            // 
            // btnSetRecipe
            // 
            this.btnSetRecipe.BackColor = System.Drawing.Color.Transparent;
            this.btnSetRecipe.Delay = 100;
            this.btnSetRecipe.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSetRecipe.Flicker = false;
            this.btnSetRecipe.IsLeftLampOn = false;
            this.btnSetRecipe.IsRightLampOn = false;
            this.btnSetRecipe.LampAliveTime = 500;
            this.btnSetRecipe.LampSize = 3;
            this.btnSetRecipe.LeftLampColor = System.Drawing.Color.Red;
            this.btnSetRecipe.Location = new System.Drawing.Point(174, 0);
            this.btnSetRecipe.Name = "btnSetRecipe";
            this.btnSetRecipe.OnOff = false;
            this.btnSetRecipe.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnSetRecipe.Size = new System.Drawing.Size(52, 20);
            this.btnSetRecipe.TabIndex = 456;
            this.btnSetRecipe.Text = "Set";
            this.btnSetRecipe.Text2 = "";
            this.btnSetRecipe.UseVisualStyleBackColor = true;
            this.btnSetRecipe.VisibleLeftLamp = false;
            this.btnSetRecipe.VisibleRightLamp = false;
            this.btnSetRecipe.Click += new System.EventHandler(this.btnSetRecipe_Click);
            // 
            // btnFixedRecipe
            // 
            this.btnFixedRecipe.BackColor = System.Drawing.Color.Transparent;
            this.btnFixedRecipe.Delay = 100;
            this.btnFixedRecipe.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFixedRecipe.Flicker = false;
            this.btnFixedRecipe.IsLeftLampOn = false;
            this.btnFixedRecipe.IsRightLampOn = false;
            this.btnFixedRecipe.LampAliveTime = 500;
            this.btnFixedRecipe.LampSize = 3;
            this.btnFixedRecipe.LeftLampColor = System.Drawing.Color.Red;
            this.btnFixedRecipe.Location = new System.Drawing.Point(226, 0);
            this.btnFixedRecipe.Name = "btnFixedRecipe";
            this.btnFixedRecipe.OnOff = false;
            this.btnFixedRecipe.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnFixedRecipe.Size = new System.Drawing.Size(110, 20);
            this.btnFixedRecipe.TabIndex = 5;
            this.btnFixedRecipe.Text = "레시피 고정";
            this.btnFixedRecipe.Text2 = "";
            this.btnFixedRecipe.UseVisualStyleBackColor = true;
            this.btnFixedRecipe.VisibleLeftLamp = false;
            this.btnFixedRecipe.VisibleRightLamp = false;
            this.btnFixedRecipe.Click += new System.EventHandler(this.btnFixedRecipe_Click);
            // 
            // lblSelectedRecipe
            // 
            this.lblSelectedRecipe.AutoEllipsis = true;
            this.lblSelectedRecipe.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblSelectedRecipe.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSelectedRecipe.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.lblSelectedRecipe.ForeColor = System.Drawing.Color.Black;
            this.lblSelectedRecipe.Location = new System.Drawing.Point(0, 0);
            this.lblSelectedRecipe.Margin = new System.Windows.Forms.Padding(1);
            this.lblSelectedRecipe.Name = "lblSelectedRecipe";
            this.lblSelectedRecipe.Size = new System.Drawing.Size(170, 20);
            this.lblSelectedRecipe.TabIndex = 455;
            this.lblSelectedRecipe.Text = "-";
            this.lblSelectedRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(1, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(334, 18);
            this.label2.TabIndex = 456;
            this.label2.Text = "■ 레시피 목록";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pGridSelectedRecipe
            // 
            this.pGridSelectedRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pGridSelectedRecipe.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.pGridSelectedRecipe.Location = new System.Drawing.Point(3, 280);
            this.pGridSelectedRecipe.Name = "pGridSelectedRecipe";
            this.pGridSelectedRecipe.Size = new System.Drawing.Size(330, 320);
            this.pGridSelectedRecipe.TabIndex = 457;
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(1, 238);
            this.label1.Margin = new System.Windows.Forms.Padding(1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(334, 18);
            this.label1.TabIndex = 454;
            this.label1.Text = "■ 선택한 레시피";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstRcps
            // 
            this.lstRcps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.lstRcps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRcps.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lstRcps.FullRowSelect = true;
            this.lstRcps.GridLines = true;
            this.lstRcps.HideSelection = false;
            this.lstRcps.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lstRcps.Location = new System.Drawing.Point(3, 23);
            this.lstRcps.Name = "lstRcps";
            this.lstRcps.Size = new System.Drawing.Size(330, 211);
            this.lstRcps.TabIndex = 458;
            this.lstRcps.UseCompatibleStateImageBehavior = false;
            this.lstRcps.View = System.Windows.Forms.View.Details;
            this.lstRcps.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstRcps_ColumnClick);
            this.lstRcps.SelectedIndexChanged += new System.EventHandler(this.lstRcps_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 240;
            // 
            // tabManual
            // 
            this.tabManual.Controls.Add(this.lblDllVersion);
            this.tabManual.Controls.Add(this.groupBox3);
            this.tabManual.Controls.Add(this.groupBox2);
            this.tabManual.Controls.Add(this.groupBox1);
            this.tabManual.Location = new System.Drawing.Point(4, 22);
            this.tabManual.Name = "tabManual";
            this.tabManual.Padding = new System.Windows.Forms.Padding(3);
            this.tabManual.Size = new System.Drawing.Size(336, 679);
            this.tabManual.TabIndex = 2;
            this.tabManual.Text = "Manual";
            this.tabManual.UseVisualStyleBackColor = true;
            // 
            // lblDllVersion
            // 
            this.lblDllVersion.AutoSize = true;
            this.lblDllVersion.Location = new System.Drawing.Point(292, 684);
            this.lblDllVersion.Name = "lblDllVersion";
            this.lblDllVersion.Size = new System.Drawing.Size(38, 12);
            this.lblDllVersion.TabIndex = 9;
            this.lblDllVersion.Text = "label5";
            this.lblDllVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblStep);
            this.groupBox3.Controls.Add(this.btnStopGrabSeq);
            this.groupBox3.Controls.Add(this.btnStartGrabSeq);
            this.groupBox3.Location = new System.Drawing.Point(6, 534);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(324, 147);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "시퀀스";
            // 
            // lblStep
            // 
            this.lblStep.BackColor = System.Drawing.Color.Gray;
            this.lblStep.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lblStep.Location = new System.Drawing.Point(218, 20);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(100, 54);
            this.lblStep.TabIndex = 120;
            this.lblStep.Text = "-";
            this.lblStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStopGrabSeq
            // 
            this.btnStopGrabSeq.BackColor = System.Drawing.Color.Transparent;
            this.btnStopGrabSeq.Delay = 100;
            this.btnStopGrabSeq.Flicker = false;
            this.btnStopGrabSeq.IsLeftLampOn = false;
            this.btnStopGrabSeq.IsRightLampOn = false;
            this.btnStopGrabSeq.LampAliveTime = 500;
            this.btnStopGrabSeq.LampSize = 3;
            this.btnStopGrabSeq.LeftLampColor = System.Drawing.Color.Red;
            this.btnStopGrabSeq.Location = new System.Drawing.Point(112, 20);
            this.btnStopGrabSeq.Name = "btnStopGrabSeq";
            this.btnStopGrabSeq.OnOff = false;
            this.btnStopGrabSeq.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnStopGrabSeq.Size = new System.Drawing.Size(100, 54);
            this.btnStopGrabSeq.TabIndex = 4;
            this.btnStopGrabSeq.Text = "Stop";
            this.btnStopGrabSeq.Text2 = "";
            this.btnStopGrabSeq.UseVisualStyleBackColor = true;
            this.btnStopGrabSeq.VisibleLeftLamp = false;
            this.btnStopGrabSeq.VisibleRightLamp = false;
            this.btnStopGrabSeq.Click += new System.EventHandler(this.btnStartGrabSeq_Click);
            // 
            // btnStartGrabSeq
            // 
            this.btnStartGrabSeq.BackColor = System.Drawing.Color.Transparent;
            this.btnStartGrabSeq.Delay = 100;
            this.btnStartGrabSeq.Flicker = false;
            this.btnStartGrabSeq.IsLeftLampOn = false;
            this.btnStartGrabSeq.IsRightLampOn = false;
            this.btnStartGrabSeq.LampAliveTime = 500;
            this.btnStartGrabSeq.LampSize = 3;
            this.btnStartGrabSeq.LeftLampColor = System.Drawing.Color.Red;
            this.btnStartGrabSeq.Location = new System.Drawing.Point(6, 20);
            this.btnStartGrabSeq.Name = "btnStartGrabSeq";
            this.btnStartGrabSeq.OnOff = false;
            this.btnStartGrabSeq.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnStartGrabSeq.Size = new System.Drawing.Size(100, 54);
            this.btnStartGrabSeq.TabIndex = 3;
            this.btnStartGrabSeq.Text = "Start";
            this.btnStartGrabSeq.Text2 = "";
            this.btnStartGrabSeq.UseVisualStyleBackColor = true;
            this.btnStartGrabSeq.VisibleLeftLamp = false;
            this.btnStartGrabSeq.VisibleRightLamp = false;
            this.btnStartGrabSeq.Click += new System.EventHandler(this.btnStartGrabSeq_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucrlLightControllerTest1);
            this.groupBox2.Location = new System.Drawing.Point(6, 279);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(324, 249);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Light Controller";
            // 
            // ucrlLightControllerTest1
            // 
            this.ucrlLightControllerTest1.Location = new System.Drawing.Point(6, 20);
            this.ucrlLightControllerTest1.Name = "ucrlLightControllerTest1";
            this.ucrlLightControllerTest1.Size = new System.Drawing.Size(312, 366);
            this.ucrlLightControllerTest1.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLive);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.btnSetExposure);
            this.groupBox1.Controls.Add(this.txtExposureTime);
            this.groupBox1.Controls.Add(this.btnDisconnect);
            this.groupBox1.Controls.Add(this.BtnLoadImage);
            this.groupBox1.Controls.Add(this.btnProcessing);
            this.groupBox1.Controls.Add(this.btnStopSnap);
            this.groupBox1.Controls.Add(this.btnSnap);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 267);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camera";
            // 
            // btnLive
            // 
            this.btnLive.BackColor = System.Drawing.Color.Transparent;
            this.btnLive.Delay = 100;
            this.btnLive.Flicker = false;
            this.btnLive.IsLeftLampOn = false;
            this.btnLive.IsRightLampOn = false;
            this.btnLive.LampAliveTime = 500;
            this.btnLive.LampSize = 3;
            this.btnLive.LeftLampColor = System.Drawing.Color.Red;
            this.btnLive.Location = new System.Drawing.Point(122, 155);
            this.btnLive.Name = "btnLive";
            this.btnLive.OnOff = false;
            this.btnLive.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnLive.Size = new System.Drawing.Size(78, 35);
            this.btnLive.TabIndex = 7;
            this.btnLive.Text = "Live";
            this.btnLive.Text2 = "";
            this.btnLive.UseVisualStyleBackColor = true;
            this.btnLive.VisibleLeftLamp = false;
            this.btnLive.VisibleRightLamp = false;
            this.btnLive.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.Transparent;
            this.btnConnect.Delay = 100;
            this.btnConnect.Flicker = false;
            this.btnConnect.IsLeftLampOn = false;
            this.btnConnect.IsRightLampOn = false;
            this.btnConnect.LampAliveTime = 500;
            this.btnConnect.LampSize = 3;
            this.btnConnect.LeftLampColor = System.Drawing.Color.Red;
            this.btnConnect.Location = new System.Drawing.Point(6, 20);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.OnOff = false;
            this.btnConnect.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnConnect.Size = new System.Drawing.Size(110, 35);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.Text2 = "";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.VisibleLeftLamp = false;
            this.btnConnect.VisibleRightLamp = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSetExposure
            // 
            this.btnSetExposure.BackColor = System.Drawing.Color.Transparent;
            this.btnSetExposure.Delay = 100;
            this.btnSetExposure.Flicker = false;
            this.btnSetExposure.IsLeftLampOn = false;
            this.btnSetExposure.IsRightLampOn = false;
            this.btnSetExposure.LampAliveTime = 500;
            this.btnSetExposure.LampSize = 3;
            this.btnSetExposure.LeftLampColor = System.Drawing.Color.Red;
            this.btnSetExposure.Location = new System.Drawing.Point(208, 61);
            this.btnSetExposure.Name = "btnSetExposure";
            this.btnSetExposure.OnOff = false;
            this.btnSetExposure.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnSetExposure.Size = new System.Drawing.Size(110, 47);
            this.btnSetExposure.TabIndex = 6;
            this.btnSetExposure.Text = "Set ExposureTime";
            this.btnSetExposure.Text2 = "";
            this.btnSetExposure.UseVisualStyleBackColor = true;
            this.btnSetExposure.VisibleLeftLamp = false;
            this.btnSetExposure.VisibleRightLamp = false;
            this.btnSetExposure.Click += new System.EventHandler(this.btnSetExposure_Click);
            // 
            // txtExposureTime
            // 
            this.txtExposureTime.Location = new System.Drawing.Point(122, 75);
            this.txtExposureTime.Name = "txtExposureTime";
            this.txtExposureTime.Size = new System.Drawing.Size(78, 21);
            this.txtExposureTime.TabIndex = 6;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.BackColor = System.Drawing.Color.Transparent;
            this.btnDisconnect.Delay = 100;
            this.btnDisconnect.Flicker = false;
            this.btnDisconnect.IsLeftLampOn = false;
            this.btnDisconnect.IsRightLampOn = false;
            this.btnDisconnect.LampAliveTime = 500;
            this.btnDisconnect.LampSize = 3;
            this.btnDisconnect.LeftLampColor = System.Drawing.Color.Red;
            this.btnDisconnect.Location = new System.Drawing.Point(122, 20);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.OnOff = false;
            this.btnDisconnect.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnDisconnect.Size = new System.Drawing.Size(78, 35);
            this.btnDisconnect.TabIndex = 4;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.Text2 = "";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.VisibleLeftLamp = false;
            this.btnDisconnect.VisibleRightLamp = false;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // BtnLoadImage
            // 
            this.BtnLoadImage.BackColor = System.Drawing.Color.Transparent;
            this.BtnLoadImage.Delay = 100;
            this.BtnLoadImage.Flicker = false;
            this.BtnLoadImage.IsLeftLampOn = false;
            this.BtnLoadImage.IsRightLampOn = false;
            this.BtnLoadImage.LampAliveTime = 500;
            this.BtnLoadImage.LampSize = 3;
            this.BtnLoadImage.LeftLampColor = System.Drawing.Color.Red;
            this.BtnLoadImage.Location = new System.Drawing.Point(6, 114);
            this.BtnLoadImage.Name = "BtnLoadImage";
            this.BtnLoadImage.OnOff = false;
            this.BtnLoadImage.RightLampColor = System.Drawing.Color.DarkGreen;
            this.BtnLoadImage.Size = new System.Drawing.Size(110, 35);
            this.BtnLoadImage.TabIndex = 2;
            this.BtnLoadImage.Text = "Load Image";
            this.BtnLoadImage.Text2 = "";
            this.BtnLoadImage.UseVisualStyleBackColor = true;
            this.BtnLoadImage.VisibleLeftLamp = false;
            this.BtnLoadImage.VisibleRightLamp = false;
            this.BtnLoadImage.Click += new System.EventHandler(this.BtnLoadImage_Click);
            // 
            // btnProcessing
            // 
            this.btnProcessing.BackColor = System.Drawing.Color.Transparent;
            this.btnProcessing.Delay = 100;
            this.btnProcessing.Flicker = false;
            this.btnProcessing.IsLeftLampOn = false;
            this.btnProcessing.IsRightLampOn = false;
            this.btnProcessing.LampAliveTime = 500;
            this.btnProcessing.LampSize = 3;
            this.btnProcessing.LeftLampColor = System.Drawing.Color.Red;
            this.btnProcessing.Location = new System.Drawing.Point(6, 214);
            this.btnProcessing.Name = "btnProcessing";
            this.btnProcessing.OnOff = false;
            this.btnProcessing.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnProcessing.Size = new System.Drawing.Size(312, 47);
            this.btnProcessing.TabIndex = 2;
            this.btnProcessing.Text = "Process";
            this.btnProcessing.Text2 = "";
            this.btnProcessing.UseVisualStyleBackColor = true;
            this.btnProcessing.VisibleLeftLamp = false;
            this.btnProcessing.VisibleRightLamp = false;
            this.btnProcessing.Click += new System.EventHandler(this.btnProcessing_Click);
            // 
            // btnStopSnap
            // 
            this.btnStopSnap.BackColor = System.Drawing.Color.Transparent;
            this.btnStopSnap.Delay = 100;
            this.btnStopSnap.Flicker = false;
            this.btnStopSnap.IsLeftLampOn = false;
            this.btnStopSnap.IsRightLampOn = false;
            this.btnStopSnap.LampAliveTime = 500;
            this.btnStopSnap.LampSize = 3;
            this.btnStopSnap.LeftLampColor = System.Drawing.Color.Red;
            this.btnStopSnap.Location = new System.Drawing.Point(206, 155);
            this.btnStopSnap.Name = "btnStopSnap";
            this.btnStopSnap.OnOff = false;
            this.btnStopSnap.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnStopSnap.Size = new System.Drawing.Size(110, 35);
            this.btnStopSnap.TabIndex = 4;
            this.btnStopSnap.Text = "Live Stop";
            this.btnStopSnap.Text2 = "";
            this.btnStopSnap.UseVisualStyleBackColor = true;
            this.btnStopSnap.VisibleLeftLamp = false;
            this.btnStopSnap.VisibleRightLamp = false;
            this.btnStopSnap.Click += new System.EventHandler(this.btnStopSnap_Click);
            // 
            // btnSnap
            // 
            this.btnSnap.BackColor = System.Drawing.Color.Transparent;
            this.btnSnap.Delay = 100;
            this.btnSnap.Flicker = false;
            this.btnSnap.IsLeftLampOn = false;
            this.btnSnap.IsRightLampOn = false;
            this.btnSnap.LampAliveTime = 500;
            this.btnSnap.LampSize = 3;
            this.btnSnap.LeftLampColor = System.Drawing.Color.Red;
            this.btnSnap.Location = new System.Drawing.Point(122, 114);
            this.btnSnap.Name = "btnSnap";
            this.btnSnap.OnOff = false;
            this.btnSnap.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnSnap.Size = new System.Drawing.Size(78, 35);
            this.btnSnap.TabIndex = 4;
            this.btnSnap.Text = "Snap";
            this.btnSnap.Text2 = "";
            this.btnSnap.UseVisualStyleBackColor = true;
            this.btnSnap.VisibleLeftLamp = false;
            this.btnSnap.VisibleRightLamp = false;
            this.btnSnap.Click += new System.EventHandler(this.btnSnap_Click);
            // 
            // tabOperator
            // 
            this.tabOperator.Controls.Add(this.panel27);
            this.tabOperator.Controls.Add(this.panel26);
            this.tabOperator.Controls.Add(this.panel24);
            this.tabOperator.Controls.Add(this.panel25);
            this.tabOperator.Controls.Add(this.panel18);
            this.tabOperator.Location = new System.Drawing.Point(4, 22);
            this.tabOperator.Name = "tabOperator";
            this.tabOperator.Padding = new System.Windows.Forms.Padding(3);
            this.tabOperator.Size = new System.Drawing.Size(336, 679);
            this.tabOperator.TabIndex = 4;
            this.tabOperator.Text = "모터 조작";
            this.tabOperator.UseVisualStyleBackColor = true;
            // 
            // panel27
            // 
            this.panel27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel27.Controls.Add(this.btnOcrDown);
            this.panel27.Controls.Add(this.btnOcrUp);
            this.panel27.Controls.Add(this.label80);
            this.panel27.Location = new System.Drawing.Point(6, 433);
            this.panel27.Margin = new System.Windows.Forms.Padding(0);
            this.panel27.Name = "panel27";
            this.panel27.Size = new System.Drawing.Size(141, 104);
            this.panel27.TabIndex = 167;
            // 
            // btnOcrDown
            // 
            this.btnOcrDown.BackColor = System.Drawing.Color.Transparent;
            this.btnOcrDown.Delay = 2;
            this.btnOcrDown.Flicker = false;
            this.btnOcrDown.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOcrDown.ForeColor = System.Drawing.Color.Black;
            this.btnOcrDown.IsLeftLampOn = false;
            this.btnOcrDown.IsRightLampOn = false;
            this.btnOcrDown.LampAliveTime = 500;
            this.btnOcrDown.LampSize = 1;
            this.btnOcrDown.LeftLampColor = System.Drawing.Color.Red;
            this.btnOcrDown.Location = new System.Drawing.Point(70, 46);
            this.btnOcrDown.Name = "btnOcrDown";
            this.btnOcrDown.OnOff = false;
            this.btnOcrDown.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnOcrDown.Size = new System.Drawing.Size(66, 29);
            this.btnOcrDown.TabIndex = 11;
            this.btnOcrDown.TabStop = false;
            this.btnOcrDown.Tag = "0";
            this.btnOcrDown.Text = "하강";
            this.btnOcrDown.Text2 = "";
            this.btnOcrDown.UseVisualStyleBackColor = false;
            this.btnOcrDown.VisibleLeftLamp = false;
            this.btnOcrDown.VisibleRightLamp = false;
            this.btnOcrDown.Click += new System.EventHandler(this.btnOcrOperate_Click);
            // 
            // btnOcrUp
            // 
            this.btnOcrUp.BackColor = System.Drawing.Color.Transparent;
            this.btnOcrUp.Delay = 2;
            this.btnOcrUp.Flicker = false;
            this.btnOcrUp.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOcrUp.ForeColor = System.Drawing.Color.Black;
            this.btnOcrUp.IsLeftLampOn = false;
            this.btnOcrUp.IsRightLampOn = false;
            this.btnOcrUp.LampAliveTime = 500;
            this.btnOcrUp.LampSize = 1;
            this.btnOcrUp.LeftLampColor = System.Drawing.Color.Red;
            this.btnOcrUp.Location = new System.Drawing.Point(2, 46);
            this.btnOcrUp.Name = "btnOcrUp";
            this.btnOcrUp.OnOff = false;
            this.btnOcrUp.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnOcrUp.Size = new System.Drawing.Size(66, 29);
            this.btnOcrUp.TabIndex = 10;
            this.btnOcrUp.TabStop = false;
            this.btnOcrUp.Tag = "1";
            this.btnOcrUp.Text = "상승";
            this.btnOcrUp.Text2 = "";
            this.btnOcrUp.UseVisualStyleBackColor = false;
            this.btnOcrUp.VisibleLeftLamp = false;
            this.btnOcrUp.VisibleRightLamp = false;
            this.btnOcrUp.Click += new System.EventHandler(this.btnOcrOperate_Click);
            // 
            // label80
            // 
            this.label80.AutoEllipsis = true;
            this.label80.BackColor = System.Drawing.Color.Gainsboro;
            this.label80.Dock = System.Windows.Forms.DockStyle.Top;
            this.label80.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.label80.ForeColor = System.Drawing.Color.Black;
            this.label80.Location = new System.Drawing.Point(0, 0);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(139, 24);
            this.label80.TabIndex = 9;
            this.label80.Text = "■ OCR 실린더";
            this.label80.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel26
            // 
            this.panel26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel26.Controls.Add(this.btnAlignVacOffBlowerOn);
            this.panel26.Controls.Add(this.btnAlignVacuumOff);
            this.panel26.Controls.Add(this.btnAlignBlowerOn);
            this.panel26.Controls.Add(this.btnAlignVacuumOn);
            this.panel26.Controls.Add(this.label41);
            this.panel26.Location = new System.Drawing.Point(150, 433);
            this.panel26.Name = "panel26";
            this.panel26.Size = new System.Drawing.Size(138, 104);
            this.panel26.TabIndex = 166;
            // 
            // btnAlignVacOffBlowerOn
            // 
            this.btnAlignVacOffBlowerOn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(187)))));
            this.btnAlignVacOffBlowerOn.Font = new System.Drawing.Font("맑은 고딕", 9.75F);
            this.btnAlignVacOffBlowerOn.ForeColor = System.Drawing.Color.Black;
            this.btnAlignVacOffBlowerOn.Location = new System.Drawing.Point(5, 66);
            this.btnAlignVacOffBlowerOn.Name = "btnAlignVacOffBlowerOn";
            this.btnAlignVacOffBlowerOn.Size = new System.Drawing.Size(126, 30);
            this.btnAlignVacOffBlowerOn.TabIndex = 85;
            this.btnAlignVacOffBlowerOn.TabStop = false;
            this.btnAlignVacOffBlowerOn.Text = "Off and Blower";
            this.btnAlignVacOffBlowerOn.UseVisualStyleBackColor = false;
            this.btnAlignVacOffBlowerOn.Click += new System.EventHandler(this.btnAlignVacOffBlowerOn_Click);
            // 
            // btnAlignVacuumOff
            // 
            this.btnAlignVacuumOff.BackColor = System.Drawing.Color.Transparent;
            this.btnAlignVacuumOff.Delay = 300;
            this.btnAlignVacuumOff.Flicker = false;
            this.btnAlignVacuumOff.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.btnAlignVacuumOff.ForeColor = System.Drawing.Color.Black;
            this.btnAlignVacuumOff.IsLeftLampOn = false;
            this.btnAlignVacuumOff.IsRightLampOn = false;
            this.btnAlignVacuumOff.LampAliveTime = 500;
            this.btnAlignVacuumOff.LampSize = 1;
            this.btnAlignVacuumOff.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlignVacuumOff.Location = new System.Drawing.Point(71, 26);
            this.btnAlignVacuumOff.Name = "btnAlignVacuumOff";
            this.btnAlignVacuumOff.OnOff = false;
            this.btnAlignVacuumOff.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlignVacuumOff.Size = new System.Drawing.Size(60, 20);
            this.btnAlignVacuumOff.TabIndex = 11;
            this.btnAlignVacuumOff.TabStop = false;
            this.btnAlignVacuumOff.Text = "Off";
            this.btnAlignVacuumOff.Text2 = "";
            this.btnAlignVacuumOff.UseVisualStyleBackColor = false;
            this.btnAlignVacuumOff.VisibleLeftLamp = false;
            this.btnAlignVacuumOff.VisibleRightLamp = false;
            this.btnAlignVacuumOff.Click += new System.EventHandler(this.btnAlignVacuumOperate_Click);
            // 
            // btnAlignBlowerOn
            // 
            this.btnAlignBlowerOn.BackColor = System.Drawing.Color.Transparent;
            this.btnAlignBlowerOn.Delay = 100;
            this.btnAlignBlowerOn.Flicker = false;
            this.btnAlignBlowerOn.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.btnAlignBlowerOn.ForeColor = System.Drawing.Color.Black;
            this.btnAlignBlowerOn.IsLeftLampOn = false;
            this.btnAlignBlowerOn.IsRightLampOn = false;
            this.btnAlignBlowerOn.LampAliveTime = 500;
            this.btnAlignBlowerOn.LampSize = 1;
            this.btnAlignBlowerOn.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlignBlowerOn.Location = new System.Drawing.Point(5, 47);
            this.btnAlignBlowerOn.Name = "btnAlignBlowerOn";
            this.btnAlignBlowerOn.OnOff = false;
            this.btnAlignBlowerOn.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlignBlowerOn.Size = new System.Drawing.Size(126, 19);
            this.btnAlignBlowerOn.TabIndex = 10;
            this.btnAlignBlowerOn.TabStop = false;
            this.btnAlignBlowerOn.Text = "Blower On";
            this.btnAlignBlowerOn.Text2 = "";
            this.btnAlignBlowerOn.UseVisualStyleBackColor = false;
            this.btnAlignBlowerOn.VisibleLeftLamp = false;
            this.btnAlignBlowerOn.VisibleRightLamp = false;
            this.btnAlignBlowerOn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAlignBlowerOn_MouseDown);
            this.btnAlignBlowerOn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAlignBlowerOn_MouseUp);
            // 
            // btnAlignVacuumOn
            // 
            this.btnAlignVacuumOn.BackColor = System.Drawing.Color.Transparent;
            this.btnAlignVacuumOn.Delay = 100;
            this.btnAlignVacuumOn.Flicker = false;
            this.btnAlignVacuumOn.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.btnAlignVacuumOn.ForeColor = System.Drawing.Color.Black;
            this.btnAlignVacuumOn.IsLeftLampOn = false;
            this.btnAlignVacuumOn.IsRightLampOn = false;
            this.btnAlignVacuumOn.LampAliveTime = 500;
            this.btnAlignVacuumOn.LampSize = 1;
            this.btnAlignVacuumOn.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlignVacuumOn.Location = new System.Drawing.Point(5, 26);
            this.btnAlignVacuumOn.Name = "btnAlignVacuumOn";
            this.btnAlignVacuumOn.OnOff = false;
            this.btnAlignVacuumOn.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlignVacuumOn.Size = new System.Drawing.Size(60, 20);
            this.btnAlignVacuumOn.TabIndex = 10;
            this.btnAlignVacuumOn.TabStop = false;
            this.btnAlignVacuumOn.Text = "On";
            this.btnAlignVacuumOn.Text2 = "";
            this.btnAlignVacuumOn.UseVisualStyleBackColor = false;
            this.btnAlignVacuumOn.VisibleLeftLamp = false;
            this.btnAlignVacuumOn.VisibleRightLamp = false;
            this.btnAlignVacuumOn.Click += new System.EventHandler(this.btnAlignVacuumOperate_Click);
            // 
            // label41
            // 
            this.label41.AutoEllipsis = true;
            this.label41.BackColor = System.Drawing.Color.Gainsboro;
            this.label41.Dock = System.Windows.Forms.DockStyle.Top;
            this.label41.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.label41.ForeColor = System.Drawing.Color.Black;
            this.label41.Location = new System.Drawing.Point(0, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(136, 24);
            this.label41.TabIndex = 9;
            this.label41.Text = "■ Vacuum";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel24
            // 
            this.panel24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel24.Controls.Add(this.btnAlignYPtpMove);
            this.panel24.Controls.Add(this.ucrlPtpY);
            this.panel24.Controls.Add(this.label36);
            this.panel24.Controls.Add(this.tbAlignYJogSpd);
            this.panel24.Controls.Add(this.cbAlignY);
            this.panel24.Controls.Add(this.lblAlignYError);
            this.panel24.Controls.Add(this.tbAlignYCurPosition);
            this.panel24.Controls.Add(this.label52);
            this.panel24.Controls.Add(this.lblAlignYServoOn);
            this.panel24.Controls.Add(this.lblAlignYPlusLimit);
            this.panel24.Controls.Add(this.lblAlignYMinusLimit);
            this.panel24.Controls.Add(this.lblAlignYMoving);
            this.panel24.Controls.Add(this.btnAlignYJogMinus);
            this.panel24.Controls.Add(this.btnAlignYJogPlus);
            this.panel24.Controls.Add(this.lblAlignYHomeBit);
            this.panel24.Controls.Add(this.btnAlignYHome);
            this.panel24.Location = new System.Drawing.Point(5, 147);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(321, 142);
            this.panel24.TabIndex = 165;
            // 
            // btnAlignYPtpMove
            // 
            this.btnAlignYPtpMove.BackColor = System.Drawing.Color.Transparent;
            this.btnAlignYPtpMove.Delay = 300;
            this.btnAlignYPtpMove.Flicker = false;
            this.btnAlignYPtpMove.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAlignYPtpMove.ForeColor = System.Drawing.Color.Black;
            this.btnAlignYPtpMove.IsLeftLampOn = false;
            this.btnAlignYPtpMove.IsRightLampOn = false;
            this.btnAlignYPtpMove.LampAliveTime = 500;
            this.btnAlignYPtpMove.LampSize = 5;
            this.btnAlignYPtpMove.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlignYPtpMove.Location = new System.Drawing.Point(208, 54);
            this.btnAlignYPtpMove.Name = "btnAlignYPtpMove";
            this.btnAlignYPtpMove.OnOff = false;
            this.btnAlignYPtpMove.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlignYPtpMove.Size = new System.Drawing.Size(74, 61);
            this.btnAlignYPtpMove.TabIndex = 10;
            this.btnAlignYPtpMove.TabStop = false;
            this.btnAlignYPtpMove.Text = "Ptp 이동";
            this.btnAlignYPtpMove.Text2 = "";
            this.btnAlignYPtpMove.UseVisualStyleBackColor = false;
            this.btnAlignYPtpMove.VisibleLeftLamp = true;
            this.btnAlignYPtpMove.VisibleRightLamp = true;
            this.btnAlignYPtpMove.DelayClick += new System.EventHandler(this.btnAlignXPtpMove_DelayClick);
            // 
            // ucrlPtpY
            // 
            this.ucrlPtpY.Location = new System.Drawing.Point(70, 75);
            this.ucrlPtpY.Name = "ucrlPtpY";
            this.ucrlPtpY.Size = new System.Drawing.Size(204, 38);
            this.ucrlPtpY.TabIndex = 127;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(169, 33);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(57, 12);
            this.label36.TabIndex = 140;
            this.label36.Text = "현재 위치";
            // 
            // tbAlignYJogSpd
            // 
            this.tbAlignYJogSpd.Location = new System.Drawing.Point(151, 118);
            this.tbAlignYJogSpd.Name = "tbAlignYJogSpd";
            this.tbAlignYJogSpd.Size = new System.Drawing.Size(43, 21);
            this.tbAlignYJogSpd.TabIndex = 139;
            this.tbAlignYJogSpd.Text = "0";
            // 
            // cbAlignY
            // 
            this.cbAlignY.FormattingEnabled = true;
            this.cbAlignY.Items.AddRange(new object[] {
            "위치"});
            this.cbAlignY.Location = new System.Drawing.Point(76, 52);
            this.cbAlignY.Name = "cbAlignY";
            this.cbAlignY.Size = new System.Drawing.Size(118, 20);
            this.cbAlignY.TabIndex = 126;
            this.cbAlignY.Text = "위치 선택";
            this.cbAlignY.SelectedIndexChanged += new System.EventHandler(this.cbPtp_SelectedIndexChanged);
            // 
            // lblAlignYError
            // 
            this.lblAlignYError.AutoEllipsis = true;
            this.lblAlignYError.BackColor = System.Drawing.Color.White;
            this.lblAlignYError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignYError.Delay = 500;
            this.lblAlignYError.DelayOff = false;
            this.lblAlignYError.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlignYError.ForeColor = System.Drawing.Color.Black;
            this.lblAlignYError.Location = new System.Drawing.Point(2, 107);
            this.lblAlignYError.Name = "lblAlignYError";
            this.lblAlignYError.OffColor = System.Drawing.Color.White;
            this.lblAlignYError.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAlignYError.OnOff = false;
            this.lblAlignYError.Size = new System.Drawing.Size(67, 17);
            this.lblAlignYError.TabIndex = 119;
            this.lblAlignYError.Text = "Error";
            this.lblAlignYError.Text2 = "";
            this.lblAlignYError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbAlignYCurPosition
            // 
            this.tbAlignYCurPosition.Location = new System.Drawing.Point(229, 29);
            this.tbAlignYCurPosition.Name = "tbAlignYCurPosition";
            this.tbAlignYCurPosition.Size = new System.Drawing.Size(53, 21);
            this.tbAlignYCurPosition.TabIndex = 139;
            // 
            // label52
            // 
            this.label52.AutoEllipsis = true;
            this.label52.BackColor = System.Drawing.Color.Gainsboro;
            this.label52.Dock = System.Windows.Forms.DockStyle.Top;
            this.label52.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.label52.ForeColor = System.Drawing.Color.Black;
            this.label52.Location = new System.Drawing.Point(0, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(319, 20);
            this.label52.TabIndex = 85;
            this.label52.Text = "■ Align Y";
            this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlignYServoOn
            // 
            this.lblAlignYServoOn.AutoEllipsis = true;
            this.lblAlignYServoOn.BackColor = System.Drawing.Color.Gainsboro;
            this.lblAlignYServoOn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignYServoOn.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlignYServoOn.ForeColor = System.Drawing.Color.Black;
            this.lblAlignYServoOn.Location = new System.Drawing.Point(3, 90);
            this.lblAlignYServoOn.Name = "lblAlignYServoOn";
            this.lblAlignYServoOn.Size = new System.Drawing.Size(67, 17);
            this.lblAlignYServoOn.TabIndex = 65;
            this.lblAlignYServoOn.Text = "SVR On";
            this.lblAlignYServoOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlignYPlusLimit
            // 
            this.lblAlignYPlusLimit.AutoEllipsis = true;
            this.lblAlignYPlusLimit.BackColor = System.Drawing.Color.Gainsboro;
            this.lblAlignYPlusLimit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignYPlusLimit.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlignYPlusLimit.ForeColor = System.Drawing.Color.Black;
            this.lblAlignYPlusLimit.Location = new System.Drawing.Point(3, 73);
            this.lblAlignYPlusLimit.Name = "lblAlignYPlusLimit";
            this.lblAlignYPlusLimit.Size = new System.Drawing.Size(67, 17);
            this.lblAlignYPlusLimit.TabIndex = 65;
            this.lblAlignYPlusLimit.Text = "Plus Limit";
            this.lblAlignYPlusLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlignYMinusLimit
            // 
            this.lblAlignYMinusLimit.AutoEllipsis = true;
            this.lblAlignYMinusLimit.BackColor = System.Drawing.Color.Gainsboro;
            this.lblAlignYMinusLimit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignYMinusLimit.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlignYMinusLimit.ForeColor = System.Drawing.Color.Black;
            this.lblAlignYMinusLimit.Location = new System.Drawing.Point(3, 56);
            this.lblAlignYMinusLimit.Name = "lblAlignYMinusLimit";
            this.lblAlignYMinusLimit.Size = new System.Drawing.Size(67, 17);
            this.lblAlignYMinusLimit.TabIndex = 65;
            this.lblAlignYMinusLimit.Text = "Minus Limit";
            this.lblAlignYMinusLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlignYMoving
            // 
            this.lblAlignYMoving.AutoEllipsis = true;
            this.lblAlignYMoving.BackColor = System.Drawing.Color.Gainsboro;
            this.lblAlignYMoving.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignYMoving.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlignYMoving.ForeColor = System.Drawing.Color.Black;
            this.lblAlignYMoving.Location = new System.Drawing.Point(3, 39);
            this.lblAlignYMoving.Name = "lblAlignYMoving";
            this.lblAlignYMoving.Size = new System.Drawing.Size(67, 17);
            this.lblAlignYMoving.TabIndex = 65;
            this.lblAlignYMoving.Text = "Moving";
            this.lblAlignYMoving.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAlignYJogMinus
            // 
            this.btnAlignYJogMinus.BackColor = System.Drawing.Color.Transparent;
            this.btnAlignYJogMinus.Delay = 2;
            this.btnAlignYJogMinus.Flicker = false;
            this.btnAlignYJogMinus.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAlignYJogMinus.ForeColor = System.Drawing.Color.Black;
            this.btnAlignYJogMinus.IsLeftLampOn = false;
            this.btnAlignYJogMinus.IsRightLampOn = false;
            this.btnAlignYJogMinus.LampAliveTime = 500;
            this.btnAlignYJogMinus.LampSize = 1;
            this.btnAlignYJogMinus.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlignYJogMinus.Location = new System.Drawing.Point(203, 113);
            this.btnAlignYJogMinus.Name = "btnAlignYJogMinus";
            this.btnAlignYJogMinus.OnOff = false;
            this.btnAlignYJogMinus.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlignYJogMinus.Size = new System.Drawing.Size(80, 29);
            this.btnAlignYJogMinus.TabIndex = 10;
            this.btnAlignYJogMinus.TabStop = false;
            this.btnAlignYJogMinus.Text = "▲▲(-)";
            this.btnAlignYJogMinus.Text2 = "";
            this.btnAlignYJogMinus.UseVisualStyleBackColor = false;
            this.btnAlignYJogMinus.VisibleLeftLamp = false;
            this.btnAlignYJogMinus.VisibleRightLamp = false;
            this.btnAlignYJogMinus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAlignJogMinus_MouseDown);
            this.btnAlignYJogMinus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAlignJog_MouseUp);
            // 
            // btnAlignYJogPlus
            // 
            this.btnAlignYJogPlus.BackColor = System.Drawing.Color.Transparent;
            this.btnAlignYJogPlus.Delay = 2;
            this.btnAlignYJogPlus.Flicker = false;
            this.btnAlignYJogPlus.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAlignYJogPlus.ForeColor = System.Drawing.Color.Black;
            this.btnAlignYJogPlus.IsLeftLampOn = false;
            this.btnAlignYJogPlus.IsRightLampOn = false;
            this.btnAlignYJogPlus.LampAliveTime = 500;
            this.btnAlignYJogPlus.LampSize = 1;
            this.btnAlignYJogPlus.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlignYJogPlus.Location = new System.Drawing.Point(69, 111);
            this.btnAlignYJogPlus.Name = "btnAlignYJogPlus";
            this.btnAlignYJogPlus.OnOff = false;
            this.btnAlignYJogPlus.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlignYJogPlus.Size = new System.Drawing.Size(80, 29);
            this.btnAlignYJogPlus.TabIndex = 10;
            this.btnAlignYJogPlus.TabStop = false;
            this.btnAlignYJogPlus.Text = "(+)▼▼";
            this.btnAlignYJogPlus.Text2 = "";
            this.btnAlignYJogPlus.UseVisualStyleBackColor = false;
            this.btnAlignYJogPlus.VisibleLeftLamp = false;
            this.btnAlignYJogPlus.VisibleRightLamp = false;
            this.btnAlignYJogPlus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAlignJogPlus_MouseDown);
            this.btnAlignYJogPlus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAlignJog_MouseUp);
            // 
            // lblAlignYHomeBit
            // 
            this.lblAlignYHomeBit.AutoEllipsis = true;
            this.lblAlignYHomeBit.BackColor = System.Drawing.Color.Gainsboro;
            this.lblAlignYHomeBit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignYHomeBit.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlignYHomeBit.ForeColor = System.Drawing.Color.Black;
            this.lblAlignYHomeBit.Location = new System.Drawing.Point(3, 22);
            this.lblAlignYHomeBit.Name = "lblAlignYHomeBit";
            this.lblAlignYHomeBit.Size = new System.Drawing.Size(67, 17);
            this.lblAlignYHomeBit.TabIndex = 63;
            this.lblAlignYHomeBit.Text = "H Bit";
            this.lblAlignYHomeBit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAlignYHome
            // 
            this.btnAlignYHome.BackColor = System.Drawing.Color.Transparent;
            this.btnAlignYHome.Delay = 300;
            this.btnAlignYHome.Flicker = false;
            this.btnAlignYHome.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAlignYHome.ForeColor = System.Drawing.Color.Black;
            this.btnAlignYHome.IsLeftLampOn = false;
            this.btnAlignYHome.IsRightLampOn = false;
            this.btnAlignYHome.LampAliveTime = 500;
            this.btnAlignYHome.LampSize = 5;
            this.btnAlignYHome.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlignYHome.Location = new System.Drawing.Point(76, 23);
            this.btnAlignYHome.Name = "btnAlignYHome";
            this.btnAlignYHome.OnOff = false;
            this.btnAlignYHome.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlignYHome.Size = new System.Drawing.Size(80, 29);
            this.btnAlignYHome.TabIndex = 10;
            this.btnAlignYHome.TabStop = false;
            this.btnAlignYHome.Text = "Home";
            this.btnAlignYHome.Text2 = "";
            this.btnAlignYHome.UseVisualStyleBackColor = false;
            this.btnAlignYHome.VisibleLeftLamp = true;
            this.btnAlignYHome.VisibleRightLamp = true;
            this.btnAlignYHome.DelayClick += new System.EventHandler(this.btnAlignXHome_DelayClick);
            // 
            // panel25
            // 
            this.panel25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel25.Controls.Add(this.btnAlignTPtpMove);
            this.panel25.Controls.Add(this.label37);
            this.panel25.Controls.Add(this.ucrlPtpT);
            this.panel25.Controls.Add(this.tbAlignTJogSpd);
            this.panel25.Controls.Add(this.tbAlignTCurPosition);
            this.panel25.Controls.Add(this.cbAlignT);
            this.panel25.Controls.Add(this.lblAlignTError);
            this.panel25.Controls.Add(this.label61);
            this.panel25.Controls.Add(this.lblAlignTServoOn);
            this.panel25.Controls.Add(this.lblAlignTOriginOn);
            this.panel25.Controls.Add(this.lblAlignTMoving);
            this.panel25.Controls.Add(this.btnAlignTJogMinus);
            this.panel25.Controls.Add(this.btnAlignTJogPlus);
            this.panel25.Controls.Add(this.lblAlignTHomeBit);
            this.panel25.Controls.Add(this.btnAlignTHome);
            this.panel25.Location = new System.Drawing.Point(6, 295);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(321, 137);
            this.panel25.TabIndex = 164;
            // 
            // btnAlignTPtpMove
            // 
            this.btnAlignTPtpMove.BackColor = System.Drawing.Color.Transparent;
            this.btnAlignTPtpMove.Delay = 300;
            this.btnAlignTPtpMove.Flicker = false;
            this.btnAlignTPtpMove.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAlignTPtpMove.ForeColor = System.Drawing.Color.Black;
            this.btnAlignTPtpMove.IsLeftLampOn = false;
            this.btnAlignTPtpMove.IsRightLampOn = false;
            this.btnAlignTPtpMove.LampAliveTime = 500;
            this.btnAlignTPtpMove.LampSize = 5;
            this.btnAlignTPtpMove.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlignTPtpMove.Location = new System.Drawing.Point(207, 50);
            this.btnAlignTPtpMove.Name = "btnAlignTPtpMove";
            this.btnAlignTPtpMove.OnOff = false;
            this.btnAlignTPtpMove.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlignTPtpMove.Size = new System.Drawing.Size(74, 61);
            this.btnAlignTPtpMove.TabIndex = 10;
            this.btnAlignTPtpMove.TabStop = false;
            this.btnAlignTPtpMove.Text = "Ptp 이동";
            this.btnAlignTPtpMove.Text2 = "";
            this.btnAlignTPtpMove.UseVisualStyleBackColor = false;
            this.btnAlignTPtpMove.VisibleLeftLamp = true;
            this.btnAlignTPtpMove.VisibleRightLamp = true;
            this.btnAlignTPtpMove.DelayClick += new System.EventHandler(this.btnAlignXPtpMove_DelayClick);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(168, 29);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(57, 12);
            this.label37.TabIndex = 140;
            this.label37.Text = "현재 위치";
            // 
            // ucrlPtpT
            // 
            this.ucrlPtpT.Location = new System.Drawing.Point(70, 74);
            this.ucrlPtpT.Name = "ucrlPtpT";
            this.ucrlPtpT.Size = new System.Drawing.Size(204, 38);
            this.ucrlPtpT.TabIndex = 127;
            // 
            // tbAlignTJogSpd
            // 
            this.tbAlignTJogSpd.Location = new System.Drawing.Point(150, 113);
            this.tbAlignTJogSpd.Name = "tbAlignTJogSpd";
            this.tbAlignTJogSpd.Size = new System.Drawing.Size(43, 21);
            this.tbAlignTJogSpd.TabIndex = 139;
            this.tbAlignTJogSpd.Text = "0";
            // 
            // tbAlignTCurPosition
            // 
            this.tbAlignTCurPosition.Location = new System.Drawing.Point(228, 25);
            this.tbAlignTCurPosition.Name = "tbAlignTCurPosition";
            this.tbAlignTCurPosition.Size = new System.Drawing.Size(53, 21);
            this.tbAlignTCurPosition.TabIndex = 139;
            // 
            // cbAlignT
            // 
            this.cbAlignT.FormattingEnabled = true;
            this.cbAlignT.Items.AddRange(new object[] {
            "위치"});
            this.cbAlignT.Location = new System.Drawing.Point(76, 51);
            this.cbAlignT.Name = "cbAlignT";
            this.cbAlignT.Size = new System.Drawing.Size(117, 20);
            this.cbAlignT.TabIndex = 126;
            this.cbAlignT.Text = "위치 선택";
            this.cbAlignT.SelectedIndexChanged += new System.EventHandler(this.cbPtp_SelectedIndexChanged);
            // 
            // lblAlignTError
            // 
            this.lblAlignTError.AutoEllipsis = true;
            this.lblAlignTError.BackColor = System.Drawing.Color.White;
            this.lblAlignTError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignTError.Delay = 500;
            this.lblAlignTError.DelayOff = false;
            this.lblAlignTError.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlignTError.ForeColor = System.Drawing.Color.Black;
            this.lblAlignTError.Location = new System.Drawing.Point(3, 93);
            this.lblAlignTError.Name = "lblAlignTError";
            this.lblAlignTError.OffColor = System.Drawing.Color.White;
            this.lblAlignTError.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAlignTError.OnOff = false;
            this.lblAlignTError.Size = new System.Drawing.Size(67, 17);
            this.lblAlignTError.TabIndex = 119;
            this.lblAlignTError.Text = "Error";
            this.lblAlignTError.Text2 = "";
            this.lblAlignTError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label61
            // 
            this.label61.AutoEllipsis = true;
            this.label61.BackColor = System.Drawing.Color.Gainsboro;
            this.label61.Dock = System.Windows.Forms.DockStyle.Top;
            this.label61.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.label61.ForeColor = System.Drawing.Color.Black;
            this.label61.Location = new System.Drawing.Point(0, 0);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(319, 20);
            this.label61.TabIndex = 85;
            this.label61.Text = "■ Align Theta";
            this.label61.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlignTServoOn
            // 
            this.lblAlignTServoOn.AutoEllipsis = true;
            this.lblAlignTServoOn.BackColor = System.Drawing.Color.Gainsboro;
            this.lblAlignTServoOn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignTServoOn.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlignTServoOn.ForeColor = System.Drawing.Color.Black;
            this.lblAlignTServoOn.Location = new System.Drawing.Point(3, 76);
            this.lblAlignTServoOn.Name = "lblAlignTServoOn";
            this.lblAlignTServoOn.Size = new System.Drawing.Size(67, 17);
            this.lblAlignTServoOn.TabIndex = 65;
            this.lblAlignTServoOn.Text = "SVR On";
            this.lblAlignTServoOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlignTOriginOn
            // 
            this.lblAlignTOriginOn.AutoEllipsis = true;
            this.lblAlignTOriginOn.BackColor = System.Drawing.Color.Gainsboro;
            this.lblAlignTOriginOn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignTOriginOn.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlignTOriginOn.ForeColor = System.Drawing.Color.Black;
            this.lblAlignTOriginOn.Location = new System.Drawing.Point(3, 59);
            this.lblAlignTOriginOn.Name = "lblAlignTOriginOn";
            this.lblAlignTOriginOn.Size = new System.Drawing.Size(67, 17);
            this.lblAlignTOriginOn.TabIndex = 65;
            this.lblAlignTOriginOn.Text = "Origin On";
            this.lblAlignTOriginOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlignTMoving
            // 
            this.lblAlignTMoving.AutoEllipsis = true;
            this.lblAlignTMoving.BackColor = System.Drawing.Color.Gainsboro;
            this.lblAlignTMoving.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignTMoving.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlignTMoving.ForeColor = System.Drawing.Color.Black;
            this.lblAlignTMoving.Location = new System.Drawing.Point(3, 41);
            this.lblAlignTMoving.Name = "lblAlignTMoving";
            this.lblAlignTMoving.Size = new System.Drawing.Size(67, 17);
            this.lblAlignTMoving.TabIndex = 65;
            this.lblAlignTMoving.Text = "Moving";
            this.lblAlignTMoving.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAlignTJogMinus
            // 
            this.btnAlignTJogMinus.BackColor = System.Drawing.Color.Transparent;
            this.btnAlignTJogMinus.Delay = 2;
            this.btnAlignTJogMinus.Flicker = false;
            this.btnAlignTJogMinus.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAlignTJogMinus.ForeColor = System.Drawing.Color.Black;
            this.btnAlignTJogMinus.IsLeftLampOn = false;
            this.btnAlignTJogMinus.IsRightLampOn = false;
            this.btnAlignTJogMinus.LampAliveTime = 500;
            this.btnAlignTJogMinus.LampSize = 1;
            this.btnAlignTJogMinus.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlignTJogMinus.Location = new System.Drawing.Point(203, 107);
            this.btnAlignTJogMinus.Name = "btnAlignTJogMinus";
            this.btnAlignTJogMinus.OnOff = false;
            this.btnAlignTJogMinus.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlignTJogMinus.Size = new System.Drawing.Size(80, 29);
            this.btnAlignTJogMinus.TabIndex = 10;
            this.btnAlignTJogMinus.TabStop = false;
            this.btnAlignTJogMinus.Text = "시계(-)";
            this.btnAlignTJogMinus.Text2 = "";
            this.btnAlignTJogMinus.UseVisualStyleBackColor = false;
            this.btnAlignTJogMinus.VisibleLeftLamp = false;
            this.btnAlignTJogMinus.VisibleRightLamp = false;
            this.btnAlignTJogMinus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAlignJogMinus_MouseDown);
            this.btnAlignTJogMinus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAlignJog_MouseUp);
            // 
            // btnAlignTJogPlus
            // 
            this.btnAlignTJogPlus.BackColor = System.Drawing.Color.Transparent;
            this.btnAlignTJogPlus.Delay = 2;
            this.btnAlignTJogPlus.Flicker = false;
            this.btnAlignTJogPlus.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAlignTJogPlus.ForeColor = System.Drawing.Color.Black;
            this.btnAlignTJogPlus.IsLeftLampOn = false;
            this.btnAlignTJogPlus.IsRightLampOn = false;
            this.btnAlignTJogPlus.LampAliveTime = 500;
            this.btnAlignTJogPlus.LampSize = 1;
            this.btnAlignTJogPlus.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlignTJogPlus.Location = new System.Drawing.Point(69, 106);
            this.btnAlignTJogPlus.Name = "btnAlignTJogPlus";
            this.btnAlignTJogPlus.OnOff = false;
            this.btnAlignTJogPlus.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlignTJogPlus.Size = new System.Drawing.Size(80, 29);
            this.btnAlignTJogPlus.TabIndex = 10;
            this.btnAlignTJogPlus.TabStop = false;
            this.btnAlignTJogPlus.Text = "(+)반시계";
            this.btnAlignTJogPlus.Text2 = "";
            this.btnAlignTJogPlus.UseVisualStyleBackColor = false;
            this.btnAlignTJogPlus.VisibleLeftLamp = false;
            this.btnAlignTJogPlus.VisibleRightLamp = false;
            this.btnAlignTJogPlus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAlignJogPlus_MouseDown);
            this.btnAlignTJogPlus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAlignJog_MouseUp);
            // 
            // lblAlignTHomeBit
            // 
            this.lblAlignTHomeBit.AutoEllipsis = true;
            this.lblAlignTHomeBit.BackColor = System.Drawing.Color.Gainsboro;
            this.lblAlignTHomeBit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignTHomeBit.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlignTHomeBit.ForeColor = System.Drawing.Color.Black;
            this.lblAlignTHomeBit.Location = new System.Drawing.Point(3, 24);
            this.lblAlignTHomeBit.Name = "lblAlignTHomeBit";
            this.lblAlignTHomeBit.Size = new System.Drawing.Size(67, 17);
            this.lblAlignTHomeBit.TabIndex = 63;
            this.lblAlignTHomeBit.Text = "H Bit";
            this.lblAlignTHomeBit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAlignTHome
            // 
            this.btnAlignTHome.BackColor = System.Drawing.Color.Transparent;
            this.btnAlignTHome.Delay = 300;
            this.btnAlignTHome.Flicker = false;
            this.btnAlignTHome.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAlignTHome.ForeColor = System.Drawing.Color.Black;
            this.btnAlignTHome.IsLeftLampOn = false;
            this.btnAlignTHome.IsRightLampOn = false;
            this.btnAlignTHome.LampAliveTime = 500;
            this.btnAlignTHome.LampSize = 5;
            this.btnAlignTHome.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlignTHome.Location = new System.Drawing.Point(76, 22);
            this.btnAlignTHome.Name = "btnAlignTHome";
            this.btnAlignTHome.OnOff = false;
            this.btnAlignTHome.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlignTHome.Size = new System.Drawing.Size(80, 29);
            this.btnAlignTHome.TabIndex = 10;
            this.btnAlignTHome.TabStop = false;
            this.btnAlignTHome.Text = "Home";
            this.btnAlignTHome.Text2 = "";
            this.btnAlignTHome.UseVisualStyleBackColor = false;
            this.btnAlignTHome.VisibleLeftLamp = true;
            this.btnAlignTHome.VisibleRightLamp = true;
            this.btnAlignTHome.DelayClick += new System.EventHandler(this.btnAlignXHome_DelayClick);
            // 
            // panel18
            // 
            this.panel18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel18.Controls.Add(this.btnAlignXPtpMove);
            this.panel18.Controls.Add(this.label38);
            this.panel18.Controls.Add(this.ucrlPtpX);
            this.panel18.Controls.Add(this.tbAlignXJogSpd);
            this.panel18.Controls.Add(this.tbAlignXCurPosition);
            this.panel18.Controls.Add(this.cbAlignX);
            this.panel18.Controls.Add(this.lblAlignXError);
            this.panel18.Controls.Add(this.label39);
            this.panel18.Controls.Add(this.lblAlignXServoOn);
            this.panel18.Controls.Add(this.lblAlignXPlusLimit);
            this.panel18.Controls.Add(this.lblAlignXMinusLimit);
            this.panel18.Controls.Add(this.lblAlignXMoving);
            this.panel18.Controls.Add(this.btnAlignXJogMinus);
            this.panel18.Controls.Add(this.btnAlignXJogPlus);
            this.panel18.Controls.Add(this.lblAlignXHomeBit);
            this.panel18.Controls.Add(this.btnAlignXHome);
            this.panel18.Location = new System.Drawing.Point(6, 6);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(321, 139);
            this.panel18.TabIndex = 163;
            // 
            // btnAlignXPtpMove
            // 
            this.btnAlignXPtpMove.BackColor = System.Drawing.Color.Transparent;
            this.btnAlignXPtpMove.Delay = 300;
            this.btnAlignXPtpMove.Flicker = false;
            this.btnAlignXPtpMove.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAlignXPtpMove.ForeColor = System.Drawing.Color.Black;
            this.btnAlignXPtpMove.IsLeftLampOn = false;
            this.btnAlignXPtpMove.IsRightLampOn = false;
            this.btnAlignXPtpMove.LampAliveTime = 500;
            this.btnAlignXPtpMove.LampSize = 5;
            this.btnAlignXPtpMove.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlignXPtpMove.Location = new System.Drawing.Point(207, 52);
            this.btnAlignXPtpMove.Name = "btnAlignXPtpMove";
            this.btnAlignXPtpMove.OnOff = false;
            this.btnAlignXPtpMove.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlignXPtpMove.Size = new System.Drawing.Size(74, 60);
            this.btnAlignXPtpMove.TabIndex = 10;
            this.btnAlignXPtpMove.TabStop = false;
            this.btnAlignXPtpMove.Text = "Ptp 이동";
            this.btnAlignXPtpMove.Text2 = "";
            this.btnAlignXPtpMove.UseVisualStyleBackColor = false;
            this.btnAlignXPtpMove.VisibleLeftLamp = true;
            this.btnAlignXPtpMove.VisibleRightLamp = true;
            this.btnAlignXPtpMove.DelayClick += new System.EventHandler(this.btnAlignXPtpMove_DelayClick);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(168, 30);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(57, 12);
            this.label38.TabIndex = 140;
            this.label38.Text = "현재 위치";
            // 
            // ucrlPtpX
            // 
            this.ucrlPtpX.Location = new System.Drawing.Point(70, 74);
            this.ucrlPtpX.Name = "ucrlPtpX";
            this.ucrlPtpX.Size = new System.Drawing.Size(204, 38);
            this.ucrlPtpX.TabIndex = 127;
            // 
            // tbAlignXJogSpd
            // 
            this.tbAlignXJogSpd.Location = new System.Drawing.Point(150, 116);
            this.tbAlignXJogSpd.Name = "tbAlignXJogSpd";
            this.tbAlignXJogSpd.Size = new System.Drawing.Size(43, 21);
            this.tbAlignXJogSpd.TabIndex = 139;
            this.tbAlignXJogSpd.Text = "0";
            // 
            // tbAlignXCurPosition
            // 
            this.tbAlignXCurPosition.Location = new System.Drawing.Point(228, 26);
            this.tbAlignXCurPosition.Name = "tbAlignXCurPosition";
            this.tbAlignXCurPosition.Size = new System.Drawing.Size(53, 21);
            this.tbAlignXCurPosition.TabIndex = 139;
            // 
            // cbAlignX
            // 
            this.cbAlignX.FormattingEnabled = true;
            this.cbAlignX.Items.AddRange(new object[] {
            "위치"});
            this.cbAlignX.Location = new System.Drawing.Point(72, 51);
            this.cbAlignX.Name = "cbAlignX";
            this.cbAlignX.Size = new System.Drawing.Size(121, 20);
            this.cbAlignX.TabIndex = 126;
            this.cbAlignX.Text = "위치 선택";
            this.cbAlignX.SelectedIndexChanged += new System.EventHandler(this.cbPtp_SelectedIndexChanged);
            // 
            // lblAlignXError
            // 
            this.lblAlignXError.AutoEllipsis = true;
            this.lblAlignXError.BackColor = System.Drawing.Color.White;
            this.lblAlignXError.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignXError.Delay = 500;
            this.lblAlignXError.DelayOff = false;
            this.lblAlignXError.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlignXError.ForeColor = System.Drawing.Color.Black;
            this.lblAlignXError.Location = new System.Drawing.Point(3, 107);
            this.lblAlignXError.Name = "lblAlignXError";
            this.lblAlignXError.OffColor = System.Drawing.Color.White;
            this.lblAlignXError.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAlignXError.OnOff = false;
            this.lblAlignXError.Size = new System.Drawing.Size(67, 17);
            this.lblAlignXError.TabIndex = 119;
            this.lblAlignXError.Text = "Error";
            this.lblAlignXError.Text2 = "";
            this.lblAlignXError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label39
            // 
            this.label39.AutoEllipsis = true;
            this.label39.BackColor = System.Drawing.Color.Gainsboro;
            this.label39.Dock = System.Windows.Forms.DockStyle.Top;
            this.label39.Font = new System.Drawing.Font("맑은 고딕", 8.25F);
            this.label39.ForeColor = System.Drawing.Color.Black;
            this.label39.Location = new System.Drawing.Point(0, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(319, 20);
            this.label39.TabIndex = 85;
            this.label39.Text = "■ Align X";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlignXServoOn
            // 
            this.lblAlignXServoOn.AutoEllipsis = true;
            this.lblAlignXServoOn.BackColor = System.Drawing.Color.Gainsboro;
            this.lblAlignXServoOn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignXServoOn.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlignXServoOn.ForeColor = System.Drawing.Color.Black;
            this.lblAlignXServoOn.Location = new System.Drawing.Point(3, 90);
            this.lblAlignXServoOn.Name = "lblAlignXServoOn";
            this.lblAlignXServoOn.Size = new System.Drawing.Size(67, 17);
            this.lblAlignXServoOn.TabIndex = 65;
            this.lblAlignXServoOn.Text = "SVR On";
            this.lblAlignXServoOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlignXPlusLimit
            // 
            this.lblAlignXPlusLimit.AutoEllipsis = true;
            this.lblAlignXPlusLimit.BackColor = System.Drawing.Color.Gainsboro;
            this.lblAlignXPlusLimit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignXPlusLimit.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlignXPlusLimit.ForeColor = System.Drawing.Color.Black;
            this.lblAlignXPlusLimit.Location = new System.Drawing.Point(3, 73);
            this.lblAlignXPlusLimit.Name = "lblAlignXPlusLimit";
            this.lblAlignXPlusLimit.Size = new System.Drawing.Size(67, 17);
            this.lblAlignXPlusLimit.TabIndex = 65;
            this.lblAlignXPlusLimit.Text = "Plus Limit";
            this.lblAlignXPlusLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlignXMinusLimit
            // 
            this.lblAlignXMinusLimit.AutoEllipsis = true;
            this.lblAlignXMinusLimit.BackColor = System.Drawing.Color.Gainsboro;
            this.lblAlignXMinusLimit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignXMinusLimit.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlignXMinusLimit.ForeColor = System.Drawing.Color.Black;
            this.lblAlignXMinusLimit.Location = new System.Drawing.Point(3, 56);
            this.lblAlignXMinusLimit.Name = "lblAlignXMinusLimit";
            this.lblAlignXMinusLimit.Size = new System.Drawing.Size(67, 17);
            this.lblAlignXMinusLimit.TabIndex = 65;
            this.lblAlignXMinusLimit.Text = "Minus Limit";
            this.lblAlignXMinusLimit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlignXMoving
            // 
            this.lblAlignXMoving.AutoEllipsis = true;
            this.lblAlignXMoving.BackColor = System.Drawing.Color.Gainsboro;
            this.lblAlignXMoving.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignXMoving.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlignXMoving.ForeColor = System.Drawing.Color.Black;
            this.lblAlignXMoving.Location = new System.Drawing.Point(3, 39);
            this.lblAlignXMoving.Name = "lblAlignXMoving";
            this.lblAlignXMoving.Size = new System.Drawing.Size(67, 17);
            this.lblAlignXMoving.TabIndex = 65;
            this.lblAlignXMoving.Text = "Moving";
            this.lblAlignXMoving.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAlignXJogMinus
            // 
            this.btnAlignXJogMinus.BackColor = System.Drawing.Color.Transparent;
            this.btnAlignXJogMinus.Delay = 2;
            this.btnAlignXJogMinus.Flicker = false;
            this.btnAlignXJogMinus.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAlignXJogMinus.ForeColor = System.Drawing.Color.Black;
            this.btnAlignXJogMinus.IsLeftLampOn = false;
            this.btnAlignXJogMinus.IsRightLampOn = false;
            this.btnAlignXJogMinus.LampAliveTime = 500;
            this.btnAlignXJogMinus.LampSize = 1;
            this.btnAlignXJogMinus.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlignXJogMinus.Location = new System.Drawing.Point(203, 111);
            this.btnAlignXJogMinus.Name = "btnAlignXJogMinus";
            this.btnAlignXJogMinus.OnOff = false;
            this.btnAlignXJogMinus.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlignXJogMinus.Size = new System.Drawing.Size(80, 29);
            this.btnAlignXJogMinus.TabIndex = 10;
            this.btnAlignXJogMinus.TabStop = false;
            this.btnAlignXJogMinus.Text = "◀◀(-)";
            this.btnAlignXJogMinus.Text2 = "";
            this.btnAlignXJogMinus.UseVisualStyleBackColor = false;
            this.btnAlignXJogMinus.VisibleLeftLamp = false;
            this.btnAlignXJogMinus.VisibleRightLamp = false;
            this.btnAlignXJogMinus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAlignJogMinus_MouseDown);
            this.btnAlignXJogMinus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAlignJog_MouseUp);
            // 
            // btnAlignXJogPlus
            // 
            this.btnAlignXJogPlus.BackColor = System.Drawing.Color.Transparent;
            this.btnAlignXJogPlus.Delay = 2;
            this.btnAlignXJogPlus.Flicker = false;
            this.btnAlignXJogPlus.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAlignXJogPlus.ForeColor = System.Drawing.Color.Black;
            this.btnAlignXJogPlus.IsLeftLampOn = false;
            this.btnAlignXJogPlus.IsRightLampOn = false;
            this.btnAlignXJogPlus.LampAliveTime = 500;
            this.btnAlignXJogPlus.LampSize = 1;
            this.btnAlignXJogPlus.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlignXJogPlus.Location = new System.Drawing.Point(69, 109);
            this.btnAlignXJogPlus.Name = "btnAlignXJogPlus";
            this.btnAlignXJogPlus.OnOff = false;
            this.btnAlignXJogPlus.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlignXJogPlus.Size = new System.Drawing.Size(80, 29);
            this.btnAlignXJogPlus.TabIndex = 10;
            this.btnAlignXJogPlus.TabStop = false;
            this.btnAlignXJogPlus.Text = "(+) ▶▶";
            this.btnAlignXJogPlus.Text2 = "";
            this.btnAlignXJogPlus.UseVisualStyleBackColor = false;
            this.btnAlignXJogPlus.VisibleLeftLamp = false;
            this.btnAlignXJogPlus.VisibleRightLamp = false;
            this.btnAlignXJogPlus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAlignJogPlus_MouseDown);
            this.btnAlignXJogPlus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAlignJog_MouseUp);
            // 
            // lblAlignXHomeBit
            // 
            this.lblAlignXHomeBit.AutoEllipsis = true;
            this.lblAlignXHomeBit.BackColor = System.Drawing.Color.Gainsboro;
            this.lblAlignXHomeBit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAlignXHomeBit.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlignXHomeBit.ForeColor = System.Drawing.Color.Black;
            this.lblAlignXHomeBit.Location = new System.Drawing.Point(3, 22);
            this.lblAlignXHomeBit.Name = "lblAlignXHomeBit";
            this.lblAlignXHomeBit.Size = new System.Drawing.Size(67, 17);
            this.lblAlignXHomeBit.TabIndex = 63;
            this.lblAlignXHomeBit.Text = "H Bit";
            this.lblAlignXHomeBit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAlignXHome
            // 
            this.btnAlignXHome.BackColor = System.Drawing.Color.Transparent;
            this.btnAlignXHome.Delay = 300;
            this.btnAlignXHome.Flicker = false;
            this.btnAlignXHome.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAlignXHome.ForeColor = System.Drawing.Color.Black;
            this.btnAlignXHome.IsLeftLampOn = false;
            this.btnAlignXHome.IsRightLampOn = false;
            this.btnAlignXHome.LampAliveTime = 500;
            this.btnAlignXHome.LampSize = 5;
            this.btnAlignXHome.LeftLampColor = System.Drawing.Color.Red;
            this.btnAlignXHome.Location = new System.Drawing.Point(76, 22);
            this.btnAlignXHome.Name = "btnAlignXHome";
            this.btnAlignXHome.OnOff = false;
            this.btnAlignXHome.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnAlignXHome.Size = new System.Drawing.Size(80, 29);
            this.btnAlignXHome.TabIndex = 10;
            this.btnAlignXHome.TabStop = false;
            this.btnAlignXHome.Text = "Home";
            this.btnAlignXHome.Text2 = "";
            this.btnAlignXHome.UseVisualStyleBackColor = false;
            this.btnAlignXHome.VisibleLeftLamp = true;
            this.btnAlignXHome.VisibleRightLamp = true;
            this.btnAlignXHome.DelayClick += new System.EventHandler(this.btnAlignXHome_DelayClick);
            // 
            // tmr_UiUpdate
            // 
            this.tmr_UiUpdate.Enabled = true;
            this.tmr_UiUpdate.Tick += new System.EventHandler(this.tmr_UiUpdate_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelMenu);
            this.splitContainer1.Size = new System.Drawing.Size(1584, 861);
            this.splitContainer1.SplitterDistance = 1230;
            this.splitContainer1.TabIndex = 1;
            this.MapView = new Dit.Rnd.Framework.MapView.MapViewPort();
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.MapView);
            // 
            // MapView
            // 
            this.MapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MapView.BackColor = System.Drawing.SystemColors.ControlDark;
            this.MapView.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MapView.EventOff = false;
            this.MapView.Location = new System.Drawing.Point(3, 3);
            this.MapView.Name = "MapView";
            this.MapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MapView.TabIndex = 3;
            // 
            // FrmPreAligner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmPreAligner";
            this.Text = "PreAligner Simulator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPreAligner_FormClosing);
            this.Load += new System.EventHandler(this.FrmPreAligner_Load);
            this.panelMenu.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabAlignResult.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabDefectResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefect)).EndInit();
            this.tabRecipe.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabManual.ResumeLayout(false);
            this.tabManual.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabOperator.ResumeLayout(false);
            this.panel27.ResumeLayout(false);
            this.panel26.ResumeLayout(false);
            this.panel24.ResumeLayout(false);
            this.panel24.PerformLayout();
            this.panel25.ResumeLayout(false);
            this.panel25.PerformLayout();
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Dit.Rnd.Framework.MapView.MapViewPort MapView;
        private System.Windows.Forms.Panel panelMenu;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 BtnLoadImage;
        private System.Windows.Forms.DataGridView dgvDefect;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnHead;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnValue;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabAlignResult;
        private System.Windows.Forms.TabPage tabDefectResult;
        private System.Windows.Forms.PropertyGrid pGridResult;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnConnect;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnSnap;
        private System.Windows.Forms.TabPage tabManual;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnDisconnect;
        private System.Windows.Forms.ListView lstLog;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnStopSnap;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnSetExposure;
        private System.Windows.Forms.TextBox txtExposureTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private UcrlLightControllerTest ucrlLightControllerTest1;
        private System.Windows.Forms.Timer tmr_UiUpdate;
        private System.Windows.Forms.GroupBox groupBox3;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnStopGrabSeq;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnStartGrabSeq;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnProcessing;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.TabPage tabRecipe;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label lblSelectedRecipe;
        private System.Windows.Forms.PropertyGrid pGridSelectedRecipe;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Dit.Framework.UI.UserComponent.ListViewEx lstRcps;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Panel panel1;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnDelete;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnUpdate;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnInsert;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Common.Convenience.PropertyGridEx pGridRecipe;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnFixedRecipe;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnCreateDefaultRecipe;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnSetRecipe;
        private System.Windows.Forms.Label lblDllVersion;
        private Dit.Framework.UI.UserComponent.ButtonDelay2 btnLive;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnStartNotchROISetting;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tabOperator;
        private System.Windows.Forms.Panel panel27;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnOcrDown;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnOcrUp;
        internal System.Windows.Forms.Label label80;
        private System.Windows.Forms.Panel panel26;
        internal System.Windows.Forms.Button btnAlignVacOffBlowerOn;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlignVacuumOff;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlignBlowerOn;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlignVacuumOn;
        internal System.Windows.Forms.Label label41;
        private System.Windows.Forms.Panel panel24;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlignYPtpMove;
        private Monitor.UcrlPtp ucrlPtpY;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox tbAlignYJogSpd;
        private System.Windows.Forms.ComboBox cbAlignY;
        internal Dit.Framework.UI.UserComponent.LabelDelay lblAlignYError;
        private System.Windows.Forms.TextBox tbAlignYCurPosition;
        internal System.Windows.Forms.Label label52;
        internal System.Windows.Forms.Label lblAlignYServoOn;
        internal System.Windows.Forms.Label lblAlignYPlusLimit;
        internal System.Windows.Forms.Label lblAlignYMinusLimit;
        internal System.Windows.Forms.Label lblAlignYMoving;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlignYJogMinus;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlignYJogPlus;
        internal System.Windows.Forms.Label lblAlignYHomeBit;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlignYHome;
        private System.Windows.Forms.Panel panel25;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlignTPtpMove;
        private System.Windows.Forms.Label label37;
        private Monitor.UcrlPtp ucrlPtpT;
        private System.Windows.Forms.TextBox tbAlignTJogSpd;
        private System.Windows.Forms.TextBox tbAlignTCurPosition;
        private System.Windows.Forms.ComboBox cbAlignT;
        internal Dit.Framework.UI.UserComponent.LabelDelay lblAlignTError;
        internal System.Windows.Forms.Label label61;
        internal System.Windows.Forms.Label lblAlignTServoOn;
        internal System.Windows.Forms.Label lblAlignTOriginOn;
        internal System.Windows.Forms.Label lblAlignTMoving;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlignTJogMinus;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlignTJogPlus;
        internal System.Windows.Forms.Label lblAlignTHomeBit;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlignTHome;
        private System.Windows.Forms.Panel panel18;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlignXPtpMove;
        private System.Windows.Forms.Label label38;
        private Monitor.UcrlPtp ucrlPtpX;
        private System.Windows.Forms.TextBox tbAlignXJogSpd;
        private System.Windows.Forms.TextBox tbAlignXCurPosition;
        private System.Windows.Forms.ComboBox cbAlignX;
        internal Dit.Framework.UI.UserComponent.LabelDelay lblAlignXError;
        internal System.Windows.Forms.Label label39;
        internal System.Windows.Forms.Label lblAlignXServoOn;
        internal System.Windows.Forms.Label lblAlignXPlusLimit;
        internal System.Windows.Forms.Label lblAlignXMinusLimit;
        internal System.Windows.Forms.Label lblAlignXMoving;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlignXJogMinus;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlignXJogPlus;
        internal System.Windows.Forms.Label lblAlignXHomeBit;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnAlignXHome;
    }
}

