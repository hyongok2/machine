using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dit.Framework.Log
{
    public enum LogLevel
    {
        Alarm = 1,
        Interlock = 2,
        Step = 4,
        Operation = 8,
        Action = 16,
        IF = 32,
        CIM = 64,
        Exception = 128,
        LinkSignal = 256,
        Info = 512,
        Error = 1024,
        Setting = 2048,
        State = 4096
    }
}
