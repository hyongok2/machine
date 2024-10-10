using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dit.Framework.Analog
{
    public partial class UcrlSetting_68ADVN : UserControl
    {
        public ADConverter_AJ65BTCU_68ADVN ADVN {get;set;}

        TextBox[] _curValue = null;
        TextBox[] _gainValue = null;
        TextBox[] _offSet = null;
        TextBox[] _ratio = null;
        TextBox[] _ratio2 = null;

        public UcrlSetting_68ADVN()
        {
            InitializeComponent();

            _curValue = new TextBox[]
            {
                txtCH1CurValue, txtCH2CurValue, txtCH3CurValue, txtCH4CurValue,
                txtCH5CurValue, txtCH6CurValue, txtCH7CurValue, txtCH8CurValue
            };

            _gainValue = new TextBox[]
            {
                txtCH1GainValue, txtCH2GainValue, txtCH3GainValue, txtCH4GainValue,
                txtCH5GainValue, txtCH6GainValue, txtCH7GainValue, txtCH8GainValue
            };

            _offSet = new TextBox[]
            {
                txtCH1Offset, txtCH2Offset, txtCH3Offset, txtCH4Offset,
                txtCH5Offset, txtCH6Offset, txtCH7Offset, txtCH8Offset
            };

            _ratio = new TextBox[]
            {
                txtCH1Ratio, txtCH2Ratio, txtCH3Ratio, txtCH4Ratio,
                txtCH5Ratio, txtCH6Ratio, txtCH7Ratio, txtCH8Ratio
            };
        }
        public void Initialize(string title, ADConverter_AJ65BTCU_68ADVN advn)
        {
            ADVN = advn;
            lblTitle.Text = title;
        }
        public void LoadValue()
        {
            for(int iPos = 0; iPos < 8; iPos++)
            {
                if(iPos < ADVN.ChannelCount)
                {
                    _offSet[iPos].Text = ADVN.ADSetting.GetOffset(iPos).ToString();
                    _ratio[iPos].Text = ADVN.ADSetting.GetRatio(iPos).ToString();
                }
                else
                {
                    _offSet[iPos].Text = "";
                    _ratio[iPos].Text = "";
                }
            }
            
        }
        public void UpdateUi()
        {
            for (int iPos = 0; iPos < 8; iPos++)
            {
                if (iPos < ADVN.ChannelCount)
                {
                    _curValue[iPos].Text = ADVN.ReadDataBuf[iPos].ToString();
                    _gainValue[iPos].Text = ADVN.Wr_AnalogReadDataBuf[iPos].vFloat.ToString();
                }
                else
                {
                    _curValue[iPos].Text = "";
                    _gainValue[iPos].Text = "";
                }
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            Func<string, float> convert = delegate(string value)
            {
                return float.Parse(string.IsNullOrEmpty(value) == true ? "0" : value);
            };

            Button btn = sender as Button;

            if (btn == btnCH1Set)
            {
                ADVN.ADSetting.SetOffset(0, convert(txtCH1Offset.Text));
                ADVN.ADSetting.SetRatio(0, convert(txtCH1Ratio.Text));
            }
            else if (btn == btnCH2Set)
            {
                ADVN.ADSetting.SetOffset(1, convert(txtCH2Offset.Text));
                ADVN.ADSetting.SetRatio(1, convert(txtCH2Ratio.Text));
            }
            else if (btn == btnCH3Set)
            {
                ADVN.ADSetting.SetOffset(2, convert(txtCH3Offset.Text));
                ADVN.ADSetting.SetRatio(2, convert(txtCH3Ratio.Text));
            }
            else if (btn == btnCH4Set)
            {
                ADVN.ADSetting.SetOffset(3, convert(txtCH4Offset.Text));
                ADVN.ADSetting.SetRatio(3, convert(txtCH4Ratio.Text));
            }
            else if (btn == btnCH5Set)
            {
                ADVN.ADSetting.SetOffset(4, convert(txtCH5Offset.Text));
                ADVN.ADSetting.SetRatio(4, convert(txtCH5Ratio.Text));
            }
            else if (btn == btnCH6Set)
            {
                ADVN.ADSetting.SetOffset(5, convert(txtCH6Offset.Text));
                ADVN.ADSetting.SetRatio(5, convert(txtCH6Ratio.Text));
            }
            else if (btn == btnCH7Set)
            {
                ADVN.ADSetting.SetOffset(6, convert(txtCH7Offset.Text));
                ADVN.ADSetting.SetRatio(6, convert(txtCH7Ratio.Text));
            }
            else if (btn == btnCH8Set)
            {
                ADVN.ADSetting.SetOffset(7, convert(txtCH8Offset.Text));
                ADVN.ADSetting.SetRatio(7, convert(txtCH8Ratio.Text));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ADVN.ADSetting.Save();
        }
    }
}
