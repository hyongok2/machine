using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;


namespace Dit.Framework.Util
{
    public class PcDateTime
    {
        [DllImport("kernel32")]
        extern static UInt64 GetTickCount64();

        private static bool _first = true;
        public static DateTime StartTime;
        public static DateTime Now
        {
            get
            {
                if (_first) { StartTime = DateTime.Now; _first = true; }
                return StartTime + TimeSpan.FromMilliseconds(GetTickCount64());
            }
        }
    }
}
