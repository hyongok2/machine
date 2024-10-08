using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EquipMainUi.Struct;
using EquipMainUi.Struct.Detail.EFEM;

namespace EquipMainUi.UserControls
{
    public partial class ucrlLPMOption : UserControl
    {
        public EmEfemPort LoadPort { get; set; }
        public EmProgressWay ProgressWay { get; private set; }

        public delegate void NotifyOptionChanged();
        public event NotifyOptionChanged ProgressChanged;

        public ucrlLPMOption()
        {
            InitializeComponent();
        }
            public void SetProgressWay(EmProgressWay way)
        {
            ProgressWay = way;
            switch (way)
            {
                case EmProgressWay.OnlyLast: rdLastWafer.Checked = true; break;
                case EmProgressWay.OnlyFirst: rdFirstWafer.Checked = true; break;
                case EmProgressWay.Mapping: rdMapData.Checked = true; break;
                case EmProgressWay.User: rdUserData.Checked = true; break;
            }
        }

        private void chkOHT_Click(object sender, EventArgs e)
        {
            RadioButton rd = sender as RadioButton;
            
            if (rd == rdLastWafer)
            {
                if (rdLastWafer.Checked)
                    ProgressWay = EmProgressWay.OnlyLast;
                ProgressChanged?.Invoke();
            }
            else if (rd == rdFirstWafer)
            {
                if (rdFirstWafer.Checked)
                    ProgressWay = EmProgressWay.OnlyFirst;
                ProgressChanged?.Invoke();
            }
            else if (rd == rdMapData)
            {
                if (rdMapData.Checked)
                    ProgressWay = EmProgressWay.Mapping;
                ProgressChanged?.Invoke();
            }
            else if (rd == rdUserData)
            {
                if (rdUserData.Checked)
                    ProgressWay = EmProgressWay.User;
                ProgressChanged?.Invoke();
            }
        }
    }
}
