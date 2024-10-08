using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using EquipMainUi.Struct.Step;
using EquipMainUi.Struct.Detail;

namespace EquipMainUi.Struct
{
    public class HotLink : StepBase
    {
        public PlcAddr UPPER_POWER_ON_INPUT;
        public PlcAddr UPPER_EMERGENCY_STOP_INPUT;
        public PlcAddr UPPER_NETWORK_READY_INPUT;
        public PlcAddr UPPER_RESERVED_INPUT1;
        public PlcAddr UPPER_ARM1_VIOLATION_INPUT;
        public PlcAddr UPPER_ARM2_VIOLATION_INPUT;
        public PlcAddr UPPER_RESERVED_INPUT3;
        public PlcAddr UPPER_EMS_INPUT;

        public PlcAddr UPPER_POWER_ON_OUTPUT;
        public PlcAddr UPPER_EMERGENCY_STOP_OUTPUT;
        public PlcAddr UPPER_NETWORK_READY_OUTPUT;
        public PlcAddr UPPER_RESERVED_OUTPUT1;
        public PlcAddr UPPER_ARM1_VIOLATION_OUTPUT;
        public PlcAddr UPPER_ARM2_VIOLATION_OUTPUT;
        public PlcAddr UPPER_RESERVED_OUTPUT3;
        public PlcAddr UPPER_EMS_OUTPUT;

        private DateTime HeartBitDateTime = DateTime.Now;
        public override void LogicWorking(Equipment equip)
        {
          
        }
    }
}
