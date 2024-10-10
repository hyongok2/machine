using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Dit.Framework.RSNMC.PLC
{
    /// <summary>
    /// RS NMC용 Logger
    /// </summary>
    public static class RSNMCLogger
    {
        public static void AppendLine(string format, params object[] args)
        {
            AppendLine(string.Format(format, args));
        }
        public static void AppendLine(string value)
        {
            Debug.WriteLine(value);
        }
    }
}
