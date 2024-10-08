using Dit.Framework.UI.UserComponent;

namespace EquipMainUi.RecipeManagement
{
    partial class FrmRecipeMgr
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("test");
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lstRcps = new Dit.Framework.UI.UserComponent.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label131 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnUpdate = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnInsert = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.pGridSelectedRcp = new System.Windows.Forms.PropertyGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDown = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnSetRecipe1 = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.btnSetRecipe2 = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.lblSelectedRecipe = new System.Windows.Forms.Label();
            this.btnUp = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCurRecipe2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCurRecipe = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.81507F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.18493F));
            this.tableLayoutPanel1.Controls.Add(this.lstRcps, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label131, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.pGridSelectedRcp, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1141, 509);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lstRcps
            // 
            this.lstRcps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstRcps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRcps.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lstRcps.FullRowSelect = true;
            this.lstRcps.GridLines = true;
            this.lstRcps.HideSelection = false;
            this.lstRcps.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lstRcps.Location = new System.Drawing.Point(3, 63);
            this.lstRcps.Name = "lstRcps";
            this.tableLayoutPanel1.SetRowSpan(this.lstRcps, 5);
            this.lstRcps.Size = new System.Drawing.Size(801, 443);
            this.lstRcps.TabIndex = 0;
            this.lstRcps.UseCompatibleStateImageBehavior = false;
            this.lstRcps.View = System.Windows.Forms.View.Details;
            this.lstRcps.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstRcps_ColumnClick);
            this.lstRcps.SelectedIndexChanged += new System.EventHandler(this.lstRcps_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 172;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 70;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 417;
            // 
            // label131
            // 
            this.label131.AutoEllipsis = true;
            this.label131.BackColor = System.Drawing.Color.Gainsboro;
            this.tableLayoutPanel1.SetColumnSpan(this.label131, 2);
            this.label131.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label131.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label131.ForeColor = System.Drawing.Color.Black;
            this.label131.Location = new System.Drawing.Point(1, 1);
            this.label131.Margin = new System.Windows.Forms.Padding(1);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(1139, 28);
            this.label131.TabIndex = 453;
            this.label131.Text = "■ 현재 레시피";
            this.label131.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.btnInsert);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(807, 459);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(334, 50);
            this.panel1.TabIndex = 451;
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
            this.btnDelete.Location = new System.Drawing.Point(223, 3);
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
            this.btnUpdate.Location = new System.Drawing.Point(113, 3);
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
            this.btnInsert.Location = new System.Drawing.Point(3, 3);
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
            // pGridSelectedRcp
            // 
            this.pGridSelectedRcp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pGridSelectedRcp.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.pGridSelectedRcp.HelpVisible = false;
            this.pGridSelectedRcp.Location = new System.Drawing.Point(810, 153);
            this.pGridSelectedRcp.Name = "pGridSelectedRcp";
            this.pGridSelectedRcp.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pGridSelectedRcp.Size = new System.Drawing.Size(328, 303);
            this.pGridSelectedRcp.TabIndex = 452;
            this.pGridSelectedRcp.ToolbarVisible = false;
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(808, 61);
            this.label1.Margin = new System.Windows.Forms.Padding(1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(332, 28);
            this.label1.TabIndex = 453;
            this.label1.Text = "■ 선택한 레시피";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.Controls.Add(this.btnDown, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnSetRecipe1, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnSetRecipe2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblSelectedRecipe, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnUp, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(807, 90);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(334, 60);
            this.tableLayoutPanel2.TabIndex = 454;
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.Transparent;
            this.btnDown.Delay = 2;
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDown.Flicker = false;
            this.btnDown.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnDown.ForeColor = System.Drawing.Color.Black;
            this.btnDown.IsLeftLampOn = false;
            this.btnDown.IsRightLampOn = false;
            this.btnDown.LampAliveTime = 500;
            this.btnDown.LampSize = 1;
            this.btnDown.LeftLampColor = System.Drawing.Color.Red;
            this.btnDown.Location = new System.Drawing.Point(1, 31);
            this.btnDown.Margin = new System.Windows.Forms.Padding(1);
            this.btnDown.Name = "btnDown";
            this.btnDown.OnOff = false;
            this.btnDown.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnDown.Size = new System.Drawing.Size(48, 28);
            this.btnDown.TabIndex = 455;
            this.btnDown.TabStop = false;
            this.btnDown.Text = "▽";
            this.btnDown.Text2 = "";
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.VisibleLeftLamp = false;
            this.btnDown.VisibleRightLamp = false;
            this.btnDown.Click += new System.EventHandler(this.btnUpDown_Click);
            // 
            // btnSetRecipe1
            // 
            this.btnSetRecipe1.BackColor = System.Drawing.Color.Transparent;
            this.btnSetRecipe1.Delay = 2;
            this.btnSetRecipe1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetRecipe1.Flicker = false;
            this.btnSetRecipe1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnSetRecipe1.ForeColor = System.Drawing.Color.Black;
            this.btnSetRecipe1.IsLeftLampOn = false;
            this.btnSetRecipe1.IsRightLampOn = false;
            this.btnSetRecipe1.LampAliveTime = 500;
            this.btnSetRecipe1.LampSize = 1;
            this.btnSetRecipe1.LeftLampColor = System.Drawing.Color.Red;
            this.btnSetRecipe1.Location = new System.Drawing.Point(234, 31);
            this.btnSetRecipe1.Margin = new System.Windows.Forms.Padding(1);
            this.btnSetRecipe1.Name = "btnSetRecipe1";
            this.btnSetRecipe1.OnOff = false;
            this.btnSetRecipe1.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnSetRecipe1.Size = new System.Drawing.Size(99, 28);
            this.btnSetRecipe1.TabIndex = 454;
            this.btnSetRecipe1.TabStop = false;
            this.btnSetRecipe1.Text = "LPM1 세팅";
            this.btnSetRecipe1.Text2 = "";
            this.btnSetRecipe1.UseVisualStyleBackColor = false;
            this.btnSetRecipe1.VisibleLeftLamp = false;
            this.btnSetRecipe1.VisibleRightLamp = false;
            this.btnSetRecipe1.Click += new System.EventHandler(this.btnSetRecipe_Click);
            // 
            // btnSetRecipe2
            // 
            this.btnSetRecipe2.BackColor = System.Drawing.Color.Transparent;
            this.btnSetRecipe2.Delay = 2;
            this.btnSetRecipe2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSetRecipe2.Flicker = false;
            this.btnSetRecipe2.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnSetRecipe2.ForeColor = System.Drawing.Color.Black;
            this.btnSetRecipe2.IsLeftLampOn = false;
            this.btnSetRecipe2.IsRightLampOn = false;
            this.btnSetRecipe2.LampAliveTime = 500;
            this.btnSetRecipe2.LampSize = 1;
            this.btnSetRecipe2.LeftLampColor = System.Drawing.Color.Red;
            this.btnSetRecipe2.Location = new System.Drawing.Point(234, 1);
            this.btnSetRecipe2.Margin = new System.Windows.Forms.Padding(1);
            this.btnSetRecipe2.Name = "btnSetRecipe2";
            this.btnSetRecipe2.OnOff = false;
            this.btnSetRecipe2.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnSetRecipe2.Size = new System.Drawing.Size(99, 28);
            this.btnSetRecipe2.TabIndex = 450;
            this.btnSetRecipe2.TabStop = false;
            this.btnSetRecipe2.Text = "LPM2 세팅";
            this.btnSetRecipe2.Text2 = "";
            this.btnSetRecipe2.UseVisualStyleBackColor = false;
            this.btnSetRecipe2.VisibleLeftLamp = false;
            this.btnSetRecipe2.VisibleRightLamp = false;
            this.btnSetRecipe2.Click += new System.EventHandler(this.btnSetRecipe_Click);
            // 
            // lblSelectedRecipe
            // 
            this.lblSelectedRecipe.AutoEllipsis = true;
            this.lblSelectedRecipe.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblSelectedRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSelectedRecipe.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.lblSelectedRecipe.ForeColor = System.Drawing.Color.Black;
            this.lblSelectedRecipe.Location = new System.Drawing.Point(51, 1);
            this.lblSelectedRecipe.Margin = new System.Windows.Forms.Padding(1);
            this.lblSelectedRecipe.Name = "lblSelectedRecipe";
            this.tableLayoutPanel2.SetRowSpan(this.lblSelectedRecipe, 2);
            this.lblSelectedRecipe.Size = new System.Drawing.Size(181, 58);
            this.lblSelectedRecipe.TabIndex = 453;
            this.lblSelectedRecipe.Text = "-";
            this.lblSelectedRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.Transparent;
            this.btnUp.Delay = 2;
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUp.Flicker = false;
            this.btnUp.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.btnUp.ForeColor = System.Drawing.Color.Black;
            this.btnUp.IsLeftLampOn = false;
            this.btnUp.IsRightLampOn = false;
            this.btnUp.LampAliveTime = 500;
            this.btnUp.LampSize = 1;
            this.btnUp.LeftLampColor = System.Drawing.Color.Red;
            this.btnUp.Location = new System.Drawing.Point(1, 1);
            this.btnUp.Margin = new System.Windows.Forms.Padding(1);
            this.btnUp.Name = "btnUp";
            this.btnUp.OnOff = false;
            this.btnUp.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btnUp.Size = new System.Drawing.Size(48, 28);
            this.btnUp.TabIndex = 455;
            this.btnUp.TabStop = false;
            this.btnUp.Text = "△";
            this.btnUp.Text2 = "";
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.VisibleLeftLamp = false;
            this.btnUp.VisibleRightLamp = false;
            this.btnUp.Click += new System.EventHandler(this.btnUpDown_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel3, 2);
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.lblCurRecipe2, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblCurRecipe, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 30);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1141, 30);
            this.tableLayoutPanel3.TabIndex = 455;
            // 
            // lblCurRecipe2
            // 
            this.lblCurRecipe2.AutoEllipsis = true;
            this.lblCurRecipe2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblCurRecipe2.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblCurRecipe2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblCurRecipe2.Location = new System.Drawing.Point(651, 1);
            this.lblCurRecipe2.Margin = new System.Windows.Forms.Padding(1);
            this.lblCurRecipe2.Name = "lblCurRecipe2";
            this.lblCurRecipe2.Size = new System.Drawing.Size(316, 28);
            this.lblCurRecipe2.TabIndex = 456;
            this.lblCurRecipe2.Text = "Recipe";
            this.lblCurRecipe2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.BackColor = System.Drawing.Color.Gainsboro;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(571, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 28);
            this.label3.TabIndex = 455;
            this.label3.Text = "■ LPM2";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.label2.Size = new System.Drawing.Size(78, 28);
            this.label2.TabIndex = 454;
            this.label2.Text = "■ LPM1";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurRecipe
            // 
            this.lblCurRecipe.AutoEllipsis = true;
            this.lblCurRecipe.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblCurRecipe.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold);
            this.lblCurRecipe.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblCurRecipe.Location = new System.Drawing.Point(81, 1);
            this.lblCurRecipe.Margin = new System.Windows.Forms.Padding(1);
            this.lblCurRecipe.Name = "lblCurRecipe";
            this.lblCurRecipe.Size = new System.Drawing.Size(316, 28);
            this.lblCurRecipe.TabIndex = 453;
            this.lblCurRecipe.Text = "Recipe";
            this.lblCurRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmRecipeMgr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1141, 509);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmRecipeMgr";
            this.Text = "Recipe";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ListViewEx lstRcps;
        private System.Windows.Forms.Panel panel1;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnDelete;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnUpdate;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnInsert;
        private System.Windows.Forms.PropertyGrid pGridSelectedRcp;
        internal System.Windows.Forms.Label label131;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label lblCurRecipe;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        internal Dit.Framework.UI.UserComponent.ButtonDelay2 btnSetRecipe2;
        internal System.Windows.Forms.Label lblSelectedRecipe;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        internal ButtonDelay2 btnSetRecipe1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        internal System.Windows.Forms.Label lblCurRecipe2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label2;
        internal ButtonDelay2 btnDown;
        internal ButtonDelay2 btnUp;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}