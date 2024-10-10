using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dit.Framework.Analog
{
    public partial class FrmSetting : Form
    {
        TabPage _showPage = null;

        public FrmSetting()
        {
            InitializeComponent();                      

            tmrUiUpdate.Start();
        }
        public void SetInstance(ADConverter_AJ65BTCU_68ADVN ad1, ADConverter_AJ65BTCU_68ADVN ad2, ADConverter_AJ65BTCU_68ADVN ad3, ADConverter_AJ65BTCU_68ADVN ad4,
                                ADConverter_AJ65BTCU_68ADVN ad5, ADConverter_AJ65BTCU_68ADVN ad6, ADConverter_AJ65VBTCU_68DAVN da1, ADConverter_AJ65BT_64RD3 rd1)
        {
            //ucrlSetting_68ADVN1.ADVN = ad1;
        }

        private void tmrUiUpdate_Tick(object sender, EventArgs e)
        {
            //ucrlSetting_68ADVN1.UpdateUi();
        }
    }
}
