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
    public partial class UcrlDAItem : UserControl
    {
        private int Index { get; set; }
        private ADConverter_AJ65VBTCU_68DAVN _davn = null;

        public UcrlDAItem()
        {
            InitializeComponent();
        }
        public void Initialize(ADConverter_AJ65VBTCU_68DAVN davn, int idx)
        {
            this.Index = idx;
            lblTitle.Text = string.Format("■ T{0}", idx+1);
            _davn = davn;
            LoadValue();
        }
        private void LoadValue()
        {
            txtDaCh1Val.Text = _davn.DASetting[Index].GetValue(0).ToString();
            txtDaCh2Val.Text = _davn.DASetting[Index].GetValue(1).ToString();
            txtDaCh3Val.Text = _davn.DASetting[Index].GetValue(2).ToString();
            txtDaCh4Val.Text = _davn.DASetting[Index].GetValue(3).ToString();
            txtDaCh5Val.Text = _davn.DASetting[Index].GetValue(4).ToString();
            txtDaCh6Val.Text = _davn.DASetting[Index].GetValue(5).ToString();
            txtDaCh7Val.Text = _davn.DASetting[Index].GetValue(6).ToString();
            txtDaCh8Val.Text = _davn.DASetting[Index].GetValue(7).ToString();
        }
        private void btnDaSetValueSave_Click(object sender, EventArgs e)
        {
            Func<string, float> convert = delegate(string value)
            {
                return float.Parse(value.ToString() != "" ? value.ToString() : "0");   
            };
            float daValCh1 = DigitalSetting.CheckDALimitValue(convert(txtDaCh1Val.Text));
            float daValCh2 = DigitalSetting.CheckDALimitValue(convert(txtDaCh2Val.Text));
            float daValCh3 = DigitalSetting.CheckDALimitValue(convert(txtDaCh3Val.Text));
            float daValCh4 = DigitalSetting.CheckDALimitValue(convert(txtDaCh4Val.Text));
            float daValCh5 = DigitalSetting.CheckDALimitValue(convert(txtDaCh5Val.Text));
            float daValCh6 = DigitalSetting.CheckDALimitValue(convert(txtDaCh6Val.Text));
            float daValCh7 = DigitalSetting.CheckDALimitValue(convert(txtDaCh7Val.Text));
            float daValCh8 = DigitalSetting.CheckDALimitValue(convert(txtDaCh8Val.Text));

            _davn.DASetting[Index].SetValue(0, daValCh1);
            _davn.DASetting[Index].SetValue(1, daValCh2);
            _davn.DASetting[Index].SetValue(2, daValCh3);
            _davn.DASetting[Index].SetValue(3, daValCh4);
            _davn.DASetting[Index].SetValue(4, daValCh5);
            _davn.DASetting[Index].SetValue(5, daValCh6);
            _davn.DASetting[Index].SetValue(6, daValCh7);
            _davn.DASetting[Index].SetValue(7, daValCh8);

            DigitalSetting.Save(DigitalSetting.GetPath(_davn.DaNo, Index + 1), _davn.DASetting[Index]);
        }
    }
}
