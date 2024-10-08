using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;

namespace EquipMainUi
{
    public class PlcTimerEx : TickTimer
    {
        public bool UseLog;
        public PlcTimerEx(string name, bool useLog = true)
            : base(name)
        {
            UseLog = useLog;
        }
        public override void Start(int interval)
        {
            if (UseLog == true)
                Logger.Log.AppendLine(LogLevel.Info, "{0} DELAY = {1}s", this.Name, interval);
            base.Start(interval);
        }
        public override void Start(int intervalSec, int intervalMilli)
        {
            if (UseLog == true)
                Logger.Log.AppendLine(LogLevel.Info, "{0} DELAY = {1}s {2}ms", this.Name, intervalSec, intervalMilli);
            base.Start(intervalSec, intervalMilli);
        }
    }
}
