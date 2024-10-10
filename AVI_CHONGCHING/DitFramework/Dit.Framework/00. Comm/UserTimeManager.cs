using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Dit.Framework.Comm
{
    public class UserTimeManager
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SystemTime
        {
            [MarshalAs(UnmanagedType.U2)]
            public short Year;
            [MarshalAs(UnmanagedType.U2)]
            public short Month;
            [MarshalAs(UnmanagedType.U2)]
            public short DayOfWeek;
            [MarshalAs(UnmanagedType.U2)]
            public short Day;
            [MarshalAs(UnmanagedType.U2)]
            public short Hour;
            [MarshalAs(UnmanagedType.U2)]
            public short Minute;
            [MarshalAs(UnmanagedType.U2)]
            public short Second;
            [MarshalAs(UnmanagedType.U2)]
            public short Milliseconds;
        }
        [DllImport("kernel32.dll")]
        public static extern bool GetLocalTime(ref SystemTime time);
        [DllImport("kernel32.dll")]
        public static extern bool SetLocalTime(ref SystemTime time);

        public static bool SetTime(short year, short month, short day, short hour, short minutes, short second)
        {
            SystemTime time = new SystemTime();
            GetLocalTime(ref time);
            time.Year = year;
            time.Month = month;
            time.Day = day;
            time.Hour = hour;
            time.Minute = minutes;
            time.Second = second;

            return SetLocalTime(ref time);
        }

        public static bool SetTime(DateTime date)
        {   
            return UserTimeManager.SetTime((short)date.Year, (short)date.Month, (short)date.Day, (short)date.Hour, (short)date.Minute, (short)date.Second);
        }
    }
     
}
