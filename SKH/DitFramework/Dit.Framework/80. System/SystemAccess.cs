using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Dit.Framework.SystemAccess
{
    public static class ProcessAccess
    {
        public static bool IsRunningProcess(string procName)
        {
            Process[] process = Process.GetProcessesByName(procName);

            if (process.Length < 1)
                return false;

            return true;
        }
    }
}
