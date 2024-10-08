using System;

namespace Dit.Framework.Log
{
    public class LoggingEventArgs : EventArgs
    {
        // 필드
        public string LogMessage { get; set; }
        public LogLevel Level { get; set; }
        public DateTime SignalTime { get; set; }
    }
}
