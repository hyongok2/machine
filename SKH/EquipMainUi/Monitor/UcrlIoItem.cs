using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dit.Framework.PLC;
using EquipMainUi.Setting;
using EquipMainUi.Struct;

namespace EquipMainUi.Monitor
{
    //[System.ComponentModel.ToolboxItem(true)]
    public partial class UcrlIoItem : UserControl
    {
        private bool _useTestMode = false;
        public bool UseTestMode
        {
            get { return _useTestMode; }
            set
            {                
                _useTestMode = value;
            }
        }

        public bool IsOn = false;

        private PlcAddr _monAddress;
        public PlcAddr MonAddress
        {
            get
            {
                return _monAddress;
            }
            set
            {
                _monAddress = value;
                if (value == null)
                {
                    btnXY.Text = "";
                    lblXYName.Text = "";
                }
                else
                {
                    btnXY.Text = _monAddress.GetPlcAddressBitString();
                    lblXYName.Text = _monAddress.Desc;
                }
            }
        }

        public void SetName(string name)
        {
            lblXYName.Text = name;
        }

        public UcrlIoItem()
        {
            InitializeComponent();
            UseTestMode = false;
        }
        private void btnXY_Click(object sender, EventArgs e)
        {
            if (_monAddress == null) return;
        
            _monAddress.SetTestBit(!_monAddress.vBit);            
        }
        public void UpdateUI()
        {
            if (_monAddress == null) _useTestMode = false;

            if (_useTestMode && tableLayoutPanel1.GetColumnSpan(lblXYName) != 1)
                tableLayoutPanel1.SetColumnSpan(lblXYName, 1);
            else if (_useTestMode == false && tableLayoutPanel1.GetColumnSpan(lblXYName) != 2)
                tableLayoutPanel1.SetColumnSpan(lblXYName, 2);

            if (_monAddress == null) return;

            if (_monAddress.vBit != (lblXYLamp.BackColor == Color.Red))
                lblXYLamp.BackColor = _monAddress.vBit ? Color.Red : Color.White;
            if ((int)chkUseBitTest.CheckState != (_monAddress.UseBitTest ? 1 : 0))
                chkUseBitTest.CheckState = _monAddress.UseBitTest ? CheckState.Checked : CheckState.Unchecked;
        }

        private void chkUseBitTest_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = (sender as CheckBox).Checked;
            if (_monAddress.UseBitTest != isChecked)
                _monAddress.UseBitTest = isChecked;

        }
    }
}
