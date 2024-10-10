using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModbusTester
{
    public class Equipment
    {
        public TestSensor Sensor = new TestSensor();

        public void LogicWorking()
        {
            Console.WriteLine(string.Format("Sensor Value = {0} ", Sensor.Flow));             
        }
    }
}
