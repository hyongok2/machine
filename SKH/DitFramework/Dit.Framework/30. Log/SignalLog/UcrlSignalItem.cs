using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dit.Framework.Comm;
using Dit.Framework.PLC;

namespace EquipMainUi.Monitor
{
    public partial class UcrlSignalItem : UserControl
    {
        public UcrlSignalItem()
        {
            InitializeComponent();
        }
        public void SetText(string addr, string desc)
        {
            lblName.Text = desc;
            lblAddress.Text = addr;
        }
        public void UpdateUi(bool isOn)
        {
            if (isOn)
                lblState.BackColor = Color.Red;
            else
                lblState.BackColor = Color.White;
        }
    }
}
