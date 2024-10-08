using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Dit.Framework
{
    /// <summary>
    /// brief   특정 시간마다 true 처리
    /// date    180103
    /// </summary>
    public class PeriodChecker
    {
        private Stopwatch _st = new Stopwatch();
        public bool Reset;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="period">ms</param>
        /// <returns></returns>
        public bool IsTimeToCheck(int period)
        {
            if (_st.IsRunning == false || Reset)
            {
                _st.Restart();
                Reset = false;
            }

            return _st.Elapsed.TotalMilliseconds > period;
        }
    }
}
