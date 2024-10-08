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
    public class SwitchSimulNonX
    {
        private int _step = 0;

        public string Name { get; set; }
        public CheckBox Acturator { get; set; }
        public PlcAddr YB_OnOff { get; set; }
        public bool XB_OnOff { get; set; }
        public void LogicWorking()
        {
            if (_step == 0)
            {
                bool b1 = YB_OnOff.vBit;
                bool b2 = XB_OnOff;
                if (YB_OnOff.vBit != XB_OnOff)
                    _step = 10;
            }
            else if (_step == 10)
            {
                Acturator.Checked = YB_OnOff;

                if (Acturator.Checked)
                    Acturator.BackColor = Color.Red;
                else
                    Acturator.BackColor = Color.Gray;

                Logger.Log.AppendLine(LogLevel.Info, "{0} {1} {2}", Name, "센서", YB_OnOff ? "ON" : "OFF");

                XB_OnOff = YB_OnOff.vBit;
                _step = 20;
            }
            else if (_step == 20)
            {
                _step = 0;
            }
        }
        public void Initailzie()
        {
            Acturator.CheckedChanged += delegate(object sender, EventArgs e)
            {
                //Acturator.BackColor = Acturator.Checked ? Color.Red : Color.Gray;
                XB_OnOff = Acturator.Checked;
            };
        }


    }
}

