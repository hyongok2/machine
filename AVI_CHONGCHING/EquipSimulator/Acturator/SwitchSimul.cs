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
    public class SwitchSimul
    {
        private int _step = 0;

        public string Name { get; set; }
        public CheckBox Acturator { get; set; }
        public PlcAddr YB_OnOff { get; set; }
        public PlcAddr XB_OnOff { get; set; }
        public void LogicWorking()
        {
            if (_step == 0)
            {
                bool b1 = YB_OnOff.vBit;
                bool b2 = XB_OnOff.vBit;
                if (YB_OnOff.vBit != XB_OnOff.vBit)
                    _step = 10;

                Acturator.BackColor = XB_OnOff.vBit ? Color.Red : Color.Gray;
            }
            else if (_step == 10)
            {
                //Acturator.Checked = YB_OnOff;
                //if (Acturator.Checked)
                //    Acturator.BackColor = Color.Red;
                //else
                //    Acturator.BackColor = Color.Gray;

                Logger.Log.AppendLine(LogLevel.Info, "{0} {1} {2}", Name, "센서", YB_OnOff ? "ON" : "OFF");

                XB_OnOff.vBit = YB_OnOff.vBit;
                _step = 20;
            }
            else if (_step == 20)
            {
                _step = 0;
            }
        }
        public void Initialize()
        {
            Acturator.CheckedChanged += delegate(object sender, EventArgs e)
            {
                OnOff(!XB_OnOff.vBit);
            };
        }

        public void OnOff(bool value)
        {
            //Acturator.BackColor = Acturator.Checked ? Color.Red : Color.Gray;
            XB_OnOff.vBit = value;
            //Acturator.Checked = value;
        }
    }
}

