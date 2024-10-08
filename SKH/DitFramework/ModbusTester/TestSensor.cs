using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;

namespace ModbusTester
{
    public class TestSensor
    {
        public PlcAddr FlowAddr = null;
        public float Flow { get { return FlowAddr.vInt; } }
    }

   
}
