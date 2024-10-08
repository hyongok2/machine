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
    public partial class FrmDigitalSetting : Form
    {
        public FrmDigitalSetting(ADConverter_AJ65VBTCU_68DAVN davn)
        {
            InitializeComponent();
            ucrlDaItem1.Initialize(davn, 0);
            ucrlDaItem2.Initialize(davn, 1);
            ucrlDaItem3.Initialize(davn, 2);
            ucrlDaItem4.Initialize(davn, 3);
            ucrlDaItem5.Initialize(davn, 4);
            ucrlDaItem6.Initialize(davn, 5);
            ucrlDaItem7.Initialize(davn, 6);
            ucrlDaItem8.Initialize(davn, 7);
            ucrlDaItem9.Initialize(davn, 8);
            ucrlDaItem10.Initialize(davn, 9);
            ucrlDaItem11.Initialize(davn, 10);
            ucrlDaItem12.Initialize(davn, 11);
            ucrlDaItem13.Initialize(davn, 12);
            ucrlDaItem14.Initialize(davn, 13);
            ucrlDaItem15.Initialize(davn, 14);
        }
    }
}
