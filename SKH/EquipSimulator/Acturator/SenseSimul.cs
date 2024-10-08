using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using EquipSimulator.Log;
using Dit.Framework.Log;
using Dit.Framework.PLC;

namespace EquipSimulator.Acturator
{
    public class SenseSimul
    {
        private int _step = 0;

        public string Name { get; set; }
        public CheckBox Acturator { get; set; }
        public PlcAddr XB_OnOff { get; set; }
        public void LogicWorking()
        {

        }
        public void Initialize()
        {
            Acturator.CheckedChanged += delegate(object sender, EventArgs e)
            {
                OnOff(!XB_OnOff.vBit);
            };

            OnOff(!XB_OnOff.vBit);
        }

        public void OnOff(bool value)
        {
            Acturator.BackColor = XB_OnOff.vBit ? Color.Red : Color.Gray;
            XB_OnOff.vBit = value;
            //Acturator.Checked = value;
        }
    }
}

