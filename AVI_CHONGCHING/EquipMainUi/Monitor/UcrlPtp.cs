using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EquipMainUi.Monitor
{
    public partial class UcrlPtp : UserControl
    {
        public UcrlPtp()
        {
            InitializeComponent();
        }

        public bool GetPos(out float pos)
        {
            if (float.TryParse(txtPos.Text, out pos) == true)
                return true;
            else
            {
                txtPos.Text = "0";
                return false;
            }
        }

        public bool GetSpd(out float spd)
        {
            if (float.TryParse(txtSpd.Text, out spd) == true)
                return true;
            else
            {
                txtSpd.Text = "0";
                return false;
            }
        }

        public bool GetAcc(out float acc)
        {
            if (float.TryParse(txtAcc.Text, out acc) == true)
                return true;
            else
            {
                txtAcc.Text = "0";
                return false;
            }
        }

        public float Pos { set { txtPos.Text = value.ToString(); } }
        public float Spd { set { txtSpd.Text = value.ToString(); } }
        public float Acc { set { txtAcc.Text = value.ToString(); } }       
    }
}
