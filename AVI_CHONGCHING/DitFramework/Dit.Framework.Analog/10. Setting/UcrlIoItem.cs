using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dit.Framework.PLC;

namespace Dit.Framework.Analog
{
    public partial class UcrlIoItem : UserControl
    {
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
                }
                else
                {
                    btnXY.Text = _monAddress.GetPlcAddressBitString();
                }
            }
        }
        public string Name
        {
            set
            {
                lblXYName.Text = value;
            }
        }

        public UcrlIoItem()
        {
            InitializeComponent();
        }
        private void btnXY_Click(object sender, EventArgs e)
        {
            if (_monAddress == null) return;
            _monAddress.vBit = !_monAddress.vBit;
        }
        public void UpdateBitColor()
        {
            if (_monAddress == null) return;

            if (_monAddress.vBit != (lblXYLamp.BackColor == Color.Red))
                lblXYLamp.BackColor = _monAddress.vBit ? Color.Red : Color.White;
        }
    }
}
