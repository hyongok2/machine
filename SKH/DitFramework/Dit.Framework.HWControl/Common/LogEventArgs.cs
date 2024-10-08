using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Log;

namespace Dit.Framework.HWControl
{
    /// <summary>
    /// SJ_M 모델 컨트롤러 Class Log Event
    /// since   20180326
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        public LogLevel SetLogLevel { get; set; }
        public string Message { get; set; }

        public LogEventArgs(LogLevel logLevel, string message = "")
            : base()
        {
            SetLogLevel = logLevel;
            Message = message;
        }
    }
}
