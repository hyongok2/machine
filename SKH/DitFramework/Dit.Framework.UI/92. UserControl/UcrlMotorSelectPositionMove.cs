using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dit.Framework.UI.Motor
{
    /// <summary>
    /// 티칭위치 선택이동 셋
    /// 
    /// 언어변환추가
    /// date 180802
    /// 
    /// since 170930
    /// </summary>
    [System.ComponentModel.ToolboxItem(true)]
    public partial class UcrlMotorSelectPositionMove : UserControl
    {
        public string _language = "";
        [Category("Custom-Layout"), Description("Language")]
        public string Language
        {
            get { return this._language; }
            set
            {
                this._language = value;

                if (this._language == "ko-KR")
                {
                    btnMove.Text = "선택위치이동";
                    cmbSelectPos.Text = "위치 선택";
                }
                else
                {
                    btnMove.Text = "Move";
                    cmbSelectPos.Text = "Select Position";
                }
            }
        }

        [Category("Custom-Layout"), Description("Label 배열순서 변경")]
        public FlowDirection LabelFlow { get { return this.flowLayoutPanel1.FlowDirection; } set { this.flowLayoutPanel1.FlowDirection = value; } }

        private int _controlCount = 4;
        [Category("Custom-Layout")]
        public int LblPositionOrder
        {
            get { return this.flowLayoutPanel1.Controls.GetChildIndex(lblPosition); }
            set { if (0 <= value && value < _controlCount) this.flowLayoutPanel1.Controls.SetChildIndex(lblPosition, value); }
        }
        [Category("Custom-Layout")]
        public int LblSpeedOrder
        {
            get { return this.flowLayoutPanel1.Controls.GetChildIndex(lblSpeed); }
            set { if (0 <= value && value < _controlCount) this.flowLayoutPanel1.Controls.SetChildIndex(lblSpeed, value); }
        }
        [Category("Custom-Layout")]
        public int MoveButtonOrder
        {
            get { return this.flowLayoutPanel1.Controls.GetChildIndex(btnMove); }
            set { if (0 <= value && value < _controlCount) this.flowLayoutPanel1.Controls.SetChildIndex(btnMove, value); }
        }
        [Category("Custom-Layout")]
        public int ComboBoxOrder
        {
            get { return this.flowLayoutPanel1.Controls.GetChildIndex(cmbSelectPos); }
            set { if (0 <= value && value < _controlCount) this.flowLayoutPanel1.Controls.SetChildIndex(cmbSelectPos, value); }
        }

        [Category("Custom-Size")]
        public Size LblPositionSize
        {
            get { return lblPosition.Size; }
            set { lblPosition.Size = value; }
        }
        [Category("Custom-Size")]
        public Size LblSpeedSize
        {
            get { return lblSpeed.Size; }
            set { lblSpeed.Size = value; }
        }
        [Category("Custom-Size")]
        public Size MoveButtonSize
        {
            get { return btnMove.Size; }
            set { btnMove.Size = value; }
        }
        [Category("Custom-Size")]
        public Size ComboBoxSize
        {
            get { return cmbSelectPos.Size; }
            set { cmbSelectPos.Size = value; }
        }
        #region 컨트롤 제어
        /// <summary>
        /// 여러개 축의 이벤트 등록시 이벤트 구분용 인덱스
        /// -1 = 모두 동작
        /// 0 = Axis 1 동작,
        /// 1 = Axis 2 동작,
        /// ...
        /// </summary>
        public int AxisIdx { get; set; }
        public int SelectPositionIndex { get; set; }
        public double Position { set { this.lblPosition.Text = value.ToString("0.0##mm"); } }
        public double Speed { set { this.lblSpeed.Text = value.ToString("0.0##mm/s"); } }
        public string[] PositionNames { set { this.cmbSelectPos.Items.Clear(); this.cmbSelectPos.Items.AddRange(value); } }
        public EventHandler SelectPositionChanged { set { this.cmbSelectPos.SelectedIndexChanged += value; } }
        public EventHandler MoveButtonClicked { set { this.btnMove.Click += value; } }
        public bool IsMoveButtonLeftLampOn { set { this.btnMove.IsLeftLampOn = value; } }
        public bool IsMoveButtonRightLampOn { set { this.btnMove.IsRightLampOn = value; } }
        #endregion
        public UcrlMotorSelectPositionMove()
        {
            InitializeComponent();
        }
    }
}
