using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Dit.Framework.PLC
{
    /// <summary>
    /// OS 시간변경에 영향없는 Stopwatch로 측정하도록 변경
    /// Stop 버그 수정
    /// date 170906
    /// since 170824
    /// </summary>
    public class TickTimer
    {
        private Stopwatch _stopwatch;
        /// <summary>
        /// ms
        /// </summary>
        private long _checkTime;
        public bool IsStart { get { return _stopwatch.IsRunning; } }
        public string Name { get; set; }
        public long Remained { get { return _checkTime - _stopwatch.ElapsedMilliseconds; } }
        public long PassTime { get { return _stopwatch.ElapsedMilliseconds; } }
        public TickTimer()
            : this(string.Empty)
        {
        }
        public TickTimer(string name)
        {
            this.Name = name;
            _stopwatch = new Stopwatch();            
            Stop();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interval">Seconds</param>
        public virtual void Start(int interval)
        {
            _checkTime = interval * 1000;
            _stopwatch.Restart();
        }
        public virtual void Start(int intervalSec, int intervalMilli)
        {
            _checkTime = intervalSec * 1000 + intervalMilli;
            _stopwatch.Restart();
        }
        public void Stop()
        {
            _checkTime = -1;
            _stopwatch.Reset();
        }
        public static implicit operator bool(TickTimer tmr)
        {
            if (tmr._checkTime == -1)
                return false;

            bool rt = tmr.Remained <= 0;
            return rt;
        }
    }
}