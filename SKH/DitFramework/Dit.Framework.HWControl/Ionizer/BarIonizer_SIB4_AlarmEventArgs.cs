using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dit.Framework.HWControl.Ionizer
{
    /// <summary>
    /// SIB4 모델 컨트롤러 Class Alarm Event
    /// since   20180326
    /// </summary>
    public class BarIonizer_SIB4_AlarmEventArgs : EventArgs
    {
        public int ControllIndex { get; set; }
        public BarIonizer_SIB4.Alarms HappenedAlarm { get; set; }
        public string Message { get; set; }

        public BarIonizer_SIB4_AlarmEventArgs(BarIonizer_SIB4.Alarms happenedAlarm = BarIonizer_SIB4.Alarms.None)
            : base()
        {
            HappenedAlarm = happenedAlarm;
        }
    }
}
