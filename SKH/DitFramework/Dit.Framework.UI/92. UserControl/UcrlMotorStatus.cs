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
    /// 모터 상태 Label 모음
    /// date 170728
    /// since 170728
    /// </summary>
    [System.ComponentModel.ToolboxItem(true)]
    public partial class UcrlMotorStatus : UserControl
    {
        [Category("Custom"), Description("Label 배열순서 변경")]
        public FlowDirection LabelFlow { get { return this.flowLayoutPanel1.FlowDirection; } set { this.flowLayoutPanel1.FlowDirection = value; } }
        [Category("Custom")]
        public Color OnColor { get; set; }
        [Category("Custom")]
        public Color OffColor { get; set; }
        [Category("Custom")]
        public Size TextBoxSize { get { return lblHomeBit.Size; }
            set
            {
                lblHomeBit.Size = value;
                lblServoOn.Size = value;
                lblMoving.Size = value;
                lblNegativeLimit.Size = value;
                lblPositiveLimit.Size = value;
                lblError.Size = value;
            }
        }

        public UcrlMotorStatus()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
            OnColor = Color.FromArgb(255, 100, 100);
            OffColor = Color.Gainsboro;
        }

        public bool HomeBit { set { lblHomeBit.BackColor = value ? OnColor : OffColor; } }
        public bool ServoOn { set { lblServoOn.BackColor = value ? OnColor : OffColor; } }
        public bool Moving { set { lblMoving.BackColor = value ? OnColor : OffColor; } }
        public bool NegativeLimit { set { lblNegativeLimit.BackColor = value ? OnColor : OffColor; } }
        public bool PositiveLimit { set { lblPositiveLimit.BackColor = value ? OnColor : OffColor; } }
        public bool Error { set { lblError.BackColor = value ? OnColor : OffColor; } }
        public string Position { set { lblError.Text = value; } }
    }
}
