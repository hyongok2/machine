using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dit.Framework.Analog;

namespace Dit.Framework.Alalog
{
    public partial class UcrlDASetting : UserControl
    {
        [System.ComponentModel.ToolboxItem(true)]

        public string CH1_Name { get; set; }
        public string CH2_Name { get; set; }
        public string CH3_Name { get; set; }
        public string CH4_Name { get; set; }
        public string CH5_Name { get; set; }
        public string CH6_Name { get; set; }
        public string CH7_Name { get; set; }
        public string CH8_Name { get; set; }

        private Label[] lbls = null;
        private string[] names = null;

        private ADConverter_AJ65VBTCU_68DAVN _davn = null;

        public UcrlDASetting()
        {
            InitializeComponent();

            lbls = new Label[]
            {
                lblCh1, lblCh2, lblCh3, lblCh4, lblCh5, lblCh6, lblCh7, lblCh8 
            };

        }
        public void Initialize(ADConverter_AJ65VBTCU_68DAVN davn)
        {
            ucrlDAItem1.Initialize(davn, 0);
            ucrlDAItem2.Initialize(davn, 1);
            ucrlDAItem3.Initialize(davn, 2);
            ucrlDAItem4.Initialize(davn, 3);
            ucrlDAItem5.Initialize(davn, 4);
            ucrlDAItem6.Initialize(davn, 5);
            ucrlDAItem7.Initialize(davn, 6);
            ucrlDAItem8.Initialize(davn, 7);

            names = new string[]
            {
                CH1_Name, CH2_Name, CH3_Name, CH4_Name, CH5_Name, CH6_Name, CH7_Name, CH8_Name, 
            };

            for (int iPos = 0; iPos < davn.ChannelCount; iPos++)
            {
                lbls[iPos].Text = names[iPos];
            }
        }
    }
}
