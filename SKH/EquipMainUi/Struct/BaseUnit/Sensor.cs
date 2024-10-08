using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;

namespace EquipMainUi.Struct
{
    public class Sensor : UnitBase
    {
        private bool _isAContact;
        public bool IsOn
        {
            get
            {
                if (_isAContact)
                    return XB_OnOff.vBit == true;
                else
                    return XB_OnOff.vBit == false;
            }
        }
        public PlcAddr XB_OnOff { get; set; }

        public Sensor(bool isAContact = true)
        {
            this._isAContact = isAContact;
        }       
    }
}
