using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Comm;

namespace Dit.Framework.PLC
{
    public class PlcTimer
    {
        public DateTime StartTime { get; set; }
        public bool IsStart { get; set; }
        public string Name { get; set; }
        public PlcTimer()
            : this(string.Empty)
        {
        }
        public PlcTimer(string name)
        {
            this.Name = name;
            Stop();
        }
        public virtual void Start(int interval)
        {
            IsStart = true;
            StartTime = PcDateTime.Now.AddSeconds(interval);
        }
        public virtual void Start(int intervalSec, int intervalMilli)
        {
            IsStart = true;
            StartTime = PcDateTime.Now.AddSeconds(intervalSec).AddMilliseconds(intervalMilli);
        }
        public void Stop()
        {
            IsStart = false;
            StartTime = DateTime.MaxValue;
        }
        public static implicit operator bool(PlcTimer tmr)
        {
            bool rt = (tmr.StartTime - PcDateTime.Now).TotalSeconds <= 0;
            return rt;
        }
    }
}
