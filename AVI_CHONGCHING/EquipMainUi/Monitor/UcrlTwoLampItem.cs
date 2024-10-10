using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dit.Framework.PLC;

namespace EquipMainUi.Monitor
{
    //[System.ComponentModel.ToolboxItem(true)]
    /// <summary>
    /// io test mode ui 표현 이상 수정
    /// date 180925
    /// </summary>
    public partial class UcrlTwoLampItem : UserControl
    {
        private bool _useTestMode = false;
        public bool UseTestMode
        {
            get
            {
                return _useTestMode;
            }
            set
            {                
                _useTestMode = value;
            }
        }

        bool initDone = false;
        [Category("TwoLamp"), Description("램프 1~2개")]
        public bool hasTwoLamp{get;set;}
        [Category("LeftLamp"), Description("램프 위치")]
        public bool isLampLeft { get; set; }

        private PlcAddr _monAddress1;
        public PlcAddr MonAddress1
        {
            get
            {
                return _monAddress1;
            }
            set
            {
                _monAddress1 = value;
                if (value == null) return;
                _monAddress1.UseBitTest = false;

                //if (_monAddress1.Desc != "NONE")
                //    lblTitle.Text = _monAddress1.Desc.Substring(_monAddress1.Desc.LastIndexOf('_') + 3);
                //else
                    //lblTitle.Text = _monAddress1.Desc;
            }
        }
        private PlcAddr _monAddress2;
        public PlcAddr MonAddress2
        {
            get
            {
                return _monAddress2;
            }
            set
            {
                _monAddress2 = value;
                if (value == null) return;
                //lblTitle.Text = _monAddress2.Desc;
            }
        }

        public UcrlTwoLampItem()
        {
            InitializeComponent();
            tlp1.SetRowSpan(lamp1, 2);
            tlp1.SetRowSpan(lamp2, 2);
            chkUseBitTest1.Visible = false;
            chkUseBitTest2.Visible = false;
            UseTestMode = false;
        }
        public void SetText(string text)
        {
            lblTitle.Text = text;
        }
        public void Lamp(bool isOn)
        {
            lamp1.BackColor = isOn ? Color.Red : Color.White;
        }
        public void Lamp2(bool isOn)
        {
            lamp2.BackColor = isOn ? Color.Red : Color.White;
        }
        public void UpdateUI()
        {
            if (_monAddress1 != null)
            {
                if (_monAddress1.vBit != (lamp1.BackColor == Color.Red))
                    lamp1.BackColor = _monAddress1.vBit ? Color.Red : Color.White;
                if ((int)chkUseBitTest1.CheckState != (_monAddress1.UseBitTest ? 1 : 0))
                    chkUseBitTest1.CheckState = _monAddress1.UseBitTest ? CheckState.Checked : CheckState.Unchecked;

                if (_useTestMode && tlp1.GetRowSpan(lamp1) != 1)
                {
                    tlp1.SetRowSpan(lamp1, 1);
                    chkUseBitTest1.Visible = true;
                }
                else if (_useTestMode == false && tlp1.GetRowSpan(lamp1) != 2)
                {
                    tlp1.SetRowSpan(lamp1, 2);
                    chkUseBitTest1.Visible = false;
                }
            }
            else if (lamp1.BackColor == Color.Red)
                lamp1.BackColor = Color.White;

            if (_monAddress2 != null)
            {
                if (_monAddress2.vBit != (lamp2.BackColor == Color.Red))
                    lamp2.BackColor = _monAddress2.vBit ? Color.Red : Color.White;
                if ((int)chkUseBitTest2.CheckState != (_monAddress2.UseBitTest ? 1 : 0))
                    chkUseBitTest2.CheckState = _monAddress2.UseBitTest ? CheckState.Checked : CheckState.Unchecked;

                if (_useTestMode && tlp1.GetRowSpan(lamp2) != 1)
                {
                    tlp1.SetRowSpan(lamp2, 1);
                    chkUseBitTest2.Visible = true;
                }
                else if (_useTestMode == false && tlp1.GetRowSpan(lamp2) != 2)
                {
                    tlp1.SetRowSpan(lamp2, 2);
                    chkUseBitTest2.Visible = false;
                }
            }
            else if (lamp2.BackColor == Color.Red)
                lamp2.BackColor = Color.White;
        }

        private void SetStyle(bool hasTwoLamp = false, bool isLampLeft = false)
        {
            if (isLampLeft)
            {
                tlp1.SetColumn(lamp1, 0);
                tlp1.SetColumn(chkUseBitTest1, 0);
                tlp1.SetColumn(lamp2, 1);
                tlp1.SetColumn(chkUseBitTest2, 1);
                tlp1.SetColumn(lblTitle, 2);                


                float btnWidth = tlp1.ColumnStyles[0].Width;
                float lampWidth = tlp1.ColumnStyles[1].Width;
                tlp1.ColumnStyles[0].Width = lampWidth;
                tlp1.ColumnStyles[2].Width = btnWidth;
            }
            if (!hasTwoLamp)
            {
                tlp1.SetColumnSpan(lamp1, 2);
                tlp1.SetColumnSpan(chkUseBitTest1, 2);
                lamp2.Visible = false;
                chkUseBitTest2.Visible = false;
            }
        }

        private void UcrlTwoLampItem_Load(object sender, EventArgs e)
        {
            if (false == initDone)
            {
                SetStyle(hasTwoLamp, isLampLeft);
                initDone = true;
            }
        }

        private void lamp1_Click(object sender, EventArgs e)
        {
            if (_monAddress1 != null)
            {
                _monAddress1.SetTestBit(!_monAddress1.vBit);
            }
        }

        private void lamp2_Click(object sender, EventArgs e)
        {
            if (_monAddress2 != null)
            {
                _monAddress2.SetTestBit(!_monAddress2.vBit);
            }
        }

        private void chkUseBitTest1_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = (sender as CheckBox).Checked;
            if (_monAddress1.UseBitTest != isChecked)
                _monAddress1.UseBitTest = isChecked;
        }

        private void chkUseBitTest2_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = (sender as CheckBox).Checked;
            if (_monAddress2.UseBitTest != isChecked)
                _monAddress2.UseBitTest = isChecked;
        }

    }
}
