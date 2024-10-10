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
    public partial class UcrlSetting_68DAVN : UserControl
    {
        public ADConverter_AJ65VBTCU_68DAVN DAVN {get;set;}

        TextBox[] _curValue = null;
        TextBox[] _gainValue = null;
        TextBox[] _offSet = null;
        TextBox[] _ratio = null;
        TextBox[] _alarm = null;

        public UcrlSetting_68DAVN()
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

            _alarm = new TextBox[]
            {
                txtCH1AlarmRange, txtCH2AlarmRange, txtCH3AlarmRange, txtCH4AlarmRange,
                txtCH5AlarmRange, txtCH6AlarmRange, txtCH7AlarmRange, txtCH8AlarmRange
            };

        }
        public void Initialize(string title, ADConverter_AJ65VBTCU_68DAVN davn)
        {
            DAVN = davn;
            lblTitle.Text = title;
        }
        public void LoadValue()
        {
            for (int iPos = 0; iPos < ADConverter_AJ65VBTCU_68DAVN.THICKNESS_COUNT; iPos++)
            {
                if(iPos < DAVN.ChannelCount)
                {
                    _offSet[iPos].Text = DAVN.DASetting[0].GetOffset(iPos).ToString();
                    _ratio[iPos].Text = DAVN.DASetting[0].GetRatio(iPos).ToString();
                    _alarm[iPos].Text = DAVN.DASetting[0].GetAlarmRange(iPos).ToString();
                }
                else
                {
                    _offSet[iPos].Text = "";
                    _ratio[iPos].Text = "";
                    _alarm[iPos].Text = "";
                }
            }            
        }
        public void UpdateUi()
        {
            for (int iPos = 0; iPos < ADConverter_AJ65VBTCU_68DAVN.THICKNESS_COUNT; iPos++)
            {
                if (iPos < DAVN.ChannelCount)
                {
                    _curValue[iPos].Text = DAVN.Ww_AnalogWriteDataBuf[iPos].vFloat.ToString();
                    _gainValue[iPos].Text = DAVN.WriteValue[iPos].ToString();
                    
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
            Func<string, int> convert = delegate(string value)
            {
                return int.Parse(string.IsNullOrEmpty(value) == true ? "0" : value);
            };

            Func<string, float> convertF = delegate(string value)
            {
                return float.Parse(string.IsNullOrEmpty(value) == true ? "0" : value);
            };

            Button btn = sender as Button;

            if (btn == btnCH1Set)
            {
                SetValue(0, convert(txtCH1Offset.Text), convert(txtCH1Ratio.Text), convertF(txtCH1AlarmRange.Text));
            }
            else if (btn == btnCH2Set)
            {
                SetValue(1, convert(txtCH2Offset.Text), convert(txtCH2Ratio.Text), convertF(txtCH2AlarmRange.Text));
            }
            else if (btn == btnCH3Set)
            {
                SetValue(2, convert(txtCH3Offset.Text), convert(txtCH3Ratio.Text), convertF(txtCH3AlarmRange.Text));
            }
            else if (btn == btnCH4Set)
            {
                SetValue(3, convert(txtCH4Offset.Text), convert(txtCH4Ratio.Text), convertF(txtCH4AlarmRange.Text));
            }
            else if (btn == btnCH5Set)
            {
                SetValue(4, convert(txtCH5Offset.Text), convert(txtCH5Ratio.Text), convertF(txtCH5AlarmRange.Text));
            }
            else if (btn == btnCH6Set)
            {
                SetValue(5, convert(txtCH6Offset.Text), convert(txtCH6Ratio.Text), convertF(txtCH6AlarmRange.Text));
            }
            else if (btn == btnCH7Set)
            {
                SetValue(6, convert(txtCH7Offset.Text), convert(txtCH7Ratio.Text), convertF(txtCH7AlarmRange.Text));
            }
            else if (btn == btnCH8Set)
            {
                SetValue(7, convert(txtCH8Offset.Text), convert(txtCH8Ratio.Text), convertF(txtCH8AlarmRange.Text));
            }
        }
        private void SetValue(int index, int offset, int ratio, float alarmRange)
        {
            for (int iPos = 0; iPos < ADConverter_AJ65VBTCU_68DAVN.THICKNESS_COUNT; iPos++)
            {
                DAVN.DASetting[iPos].SetOffset(index, offset);
                DAVN.DASetting[iPos].SetRatio(index, ratio);
                DAVN.DASetting[iPos].SetAlarmRange(index, alarmRange);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            for (int iPos = 0; iPos < ADConverter_AJ65VBTCU_68DAVN.THICKNESS_COUNT; iPos++)
            {
                DigitalSetting.Save(DigitalSetting.GetPath(DAVN.DaNo, iPos+1), DAVN.DASetting[iPos]);
            }            
        }
    }
}
