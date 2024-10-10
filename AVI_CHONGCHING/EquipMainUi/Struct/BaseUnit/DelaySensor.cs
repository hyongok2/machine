using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.BaseUnit
{
    public class DelaySensor : Sensor
    {
        public EM_AL_LST SENSOR_ONOFF_ERROR { get; set; }

        DateTime _startTime = DateTime.Now;
        bool _isAlarmOn = false;
        public int TimeOutInterval = 1;
        public DelaySensor(bool IsAGround = true)
        {
            _isAlarmOn = IsAGround;
        }
        public void ResetStartTime()
        {
            _startTime = DateTime.Now;
        }
        public void LogicWorking(Equipment equip)
        {
            if (GG.TestMode) return;

            if (XB_OnOff.vBit == _isAlarmOn)
            {
                if ((DateTime.Now - _startTime).TotalSeconds > TimeOutInterval)
                    AlarmMgr.Instance.Happen(equip, SENSOR_ONOFF_ERROR);
            }
            else
                _startTime = DateTime.Now;
        }
    }
}
