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
    /// Jog동작에 필요한 컨트롤을 모음
    /// 저중고속 라디오 버튼 추가
    /// date 170925
    /// since 170728
    /// </summary>
    [System.ComponentModel.ToolboxItem(true)]
    public partial class UcrlMotorJog : UserControl
    {
        private static int _controlCount = 6;
#region 컨트롤 설정
        private bool _useTrackBar = false;
        [Category("Custom-SpeedControl")]
        public bool UseTrackBar
        {
            get { return this._useTrackBar; }
            set { this._useTrackBar = value; this.pnlTrackbar.Visible = value; }
        }
        private bool _useRadioButton = false;
        [Category("Custom-SpeedControl")]
        public bool UseRadioButton
        {
            get { return this._useRadioButton; }
            set { this._useRadioButton = value; this.pnlRadioSpeed.Visible = value; }
        }
        private bool _useTextbox = false;
        [Category("Custom-SpeedControl")]
        public bool UseTextBox
        {
            get { return this._useTextbox; }
            set { this._useTextbox = value; this.pnlTextSpeed.Visible = value; }
        }

        [Category("Custom-Layout"), Description("Label 배열순서 변경")]
        public FlowDirection LabelFlow { get { return this.flowLayoutPanel1.FlowDirection; } set { this.flowLayoutPanel1.FlowDirection = value; } }

        [Category("Custom-Layout")]
        public int LblSpeedOrder
        {
            get { return this.flowLayoutPanel1.Controls.GetChildIndex(lblSpeed); }
            set { if (0 <= value && value < _controlCount) this.flowLayoutPanel1.Controls.SetChildIndex(lblSpeed, value); }
        }
        [Category("Custom-Layout")]
        public int SpeedTrackbarOrder
        {
            get { return this.flowLayoutPanel1.Controls.GetChildIndex(pnlTrackbar); }
            set { if (0 <= value && value < _controlCount) this.flowLayoutPanel1.Controls.SetChildIndex(pnlTrackbar, value); }
        }
        [Category("Custom-Layout")]
        public int SpeedRadioOrder
        {
            get { return this.flowLayoutPanel1.Controls.GetChildIndex(pnlRadioSpeed); }
            set { if (0 <= value && value < _controlCount) this.flowLayoutPanel1.Controls.SetChildIndex(pnlRadioSpeed, value); }
        }
        [Category("Custom-Layout")]
        public int SpeedTextOrder
        {
            get { return this.flowLayoutPanel1.Controls.GetChildIndex(pnlTextSpeed); }
            set { if (0 <= value && value < _controlCount) this.flowLayoutPanel1.Controls.SetChildIndex(pnlTextSpeed, value); }
        }
        [Category("Custom-Layout")]
        public int NegativeButtonOrder
        {
            get { return this.flowLayoutPanel1.Controls.GetChildIndex(btnNegativeJog); }
            set { if (0 <= value && value < _controlCount) this.flowLayoutPanel1.Controls.SetChildIndex(btnNegativeJog, value); }
        }
        [Category("Custom-Layout")]
        public int PositiveButtonOrder
        {
            get { return this.flowLayoutPanel1.Controls.GetChildIndex(btnPositiveJog); }
            set { if (0 <= value && value < _controlCount) this.flowLayoutPanel1.Controls.SetChildIndex(btnPositiveJog, value); }
        }
        [Category("Custom-Size")]
        public Size LblSpeedSize
        {
            get { return lblSpeed.Size; }
            set { lblSpeed.Size = value; }
        }
        [Category("Custom-Size")]
        public Size SpeedRadioSize
        {
            get { return pnlRadioSpeed.Size; }
            set { pnlRadioSpeed.Size = value; }
        }
        [Category("Custom-Size")]
        public Size SpeedTrackbarSize
        {
            get { return pnlTrackbar.Size; }
            set { pnlTrackbar.Size = value; }
        }
        [Category("Custom-Size")]
        public Size SpeedTextboxSize
        {
            get { return pnlTextSpeed.Size; }
            set { pnlTextSpeed.Size = value;}
        }
        [Category("Custom-Size")]
        public Size NegativeButtonSize
        {
            get { return btnNegativeJog.Size; }
            set { btnNegativeJog.Size = value; }
        }
        [Category("Custom-Size")]
        public Size PositiveButtonSize
        {
            get { return btnPositiveJog.Size; }
            set { btnPositiveJog.Size = value; }
        }
        [Category("Custom-Text")]
        public string NegativeButtonText
        {
            get { return btnNegativeJog.Text; }
            set { btnNegativeJog.Text = value; }
        }
        [Category("Custom-Text")]
        public string PositiveButtonText
        {
            get { return btnPositiveJog.Text; }
            set { btnPositiveJog.Text = value; }
        }
        private string _unit = "";
        [Category("Custom-Text")]
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
#endregion
#region 컨트롤 제어
        private double _speed;
        public double Speed { get { return _speed; } set { _speed = value; UpdateSpeedLabel(_speed); } }
        public double JogLowSpeedRatio { get; set; }
        public double JogMidSpeedRatio { get; set; }
        public double JogHiSpeedRatio { get; set; }
        /// <summary>
        /// 여러개 축의 이벤트 등록시 이벤트 구분용 인덱스
        /// -1 = 모두 동작
        /// 0 = Axis 1 동작,
        /// 1 = Axis 2 동작,
        /// ...
        /// </summary>
        public int AxisIdx { get; set; }
        public MouseEventHandler NegitiveJogMouseDown { set { this.btnNegativeJog.MouseDown += value; } }
        public MouseEventHandler NegitiveJogMouseUp { set { this.btnNegativeJog.MouseUp += value; } }
        public MouseEventHandler PositiveJogMouseDown { set { this.btnPositiveJog.MouseDown += value; } }
        public MouseEventHandler PositiveJogMouseUp { set { this.btnPositiveJog.MouseUp += value; } }
#endregion
        public int JogSpeedLimit { get; set; }

        public UcrlMotorJog()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
            Speed = 1;
        }        
        
        public void Initialize(int jogSpeedLimit)
        {
            JogSpeedLimit = jogSpeedLimit;
        }

        private void UpdateSpeedLabel(double speed)
        {
            lblSpeed.Text = speed.ToString() + _unit;
        }

        private void trbSpeed_ValueChanged(object sender, EventArgs e)
        {
            if (JogSpeedLimit == 0)
                throw new Exception("조그 속도 설정이 안되었습니다");
            _speed = JogSpeedLimit * trbSpeed.Value / trbSpeed.Maximum;
            UpdateSpeedLabel(_speed);
        }

        private void radioSpeedLow_CheckedChanged(object sender, EventArgs e)
        {
            if (JogSpeedLimit == 0)
                throw new Exception("조그 속도 설정이 안되었습니다");

            RadioButton rd = sender as RadioButton;

            if (radioSpeedLow.Checked)
            {
                _speed = JogSpeedLimit * JogLowSpeedRatio;
            }
            else if (radioSpeedMid.Checked)
            {
                _speed = JogSpeedLimit * JogMidSpeedRatio;
            }
            else if (radioSpeedHi.Checked)
            {
                _speed = JogSpeedLimit * JogHiSpeedRatio;
            }
            UpdateSpeedLabel(_speed);
        }

        private void txtSpeed_TextChanged(object sender, EventArgs e)
        {
            if (JogSpeedLimit == 0)
                throw new Exception("조그 속도 설정이 안되었습니다");
            double speed = 0;
            double.TryParse(txtSpeed.Text, out speed);
            if (speed < 0)
                speed = 2;
            else if (speed > JogSpeedLimit)
                speed = JogSpeedLimit/10;
            _speed = speed;
            UpdateSpeedLabel(_speed);
        }
    }
}
