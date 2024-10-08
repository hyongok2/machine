using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dit.Framework.HWControl
{
    /// <summary>
    /// SJ_M 모델 컨트롤러 Class Alarm Event
    /// since   20180326
    /// </summary>
    public class PinIonizer_SJ_M_AlarmEventArgs : EventArgs
    {
        public int ControllIndex { get; set; }
        public PinIonizer_SJ_M.Alarms HappenedAlarm { get; set; }
        public string Message { get; set; }

        public PinIonizer_SJ_M_AlarmEventArgs(int controllIndex = 0, PinIonizer_SJ_M.Alarms happenedAlarm = PinIonizer_SJ_M.Alarms.None)
            : base()
        {
            ControllIndex = controllIndex;
            HappenedAlarm = happenedAlarm;
        }
    }
}
