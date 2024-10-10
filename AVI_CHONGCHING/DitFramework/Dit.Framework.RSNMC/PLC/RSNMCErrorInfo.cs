using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dit.Framework.RSNMC.PLC
{
    /// <summary>
    /// date 180710
    /// since 180710
    /// </summary>
    public class RSNMCErrorInfo
    {
        private string _msg;
        public DateTime Time { get; set; }
        public string Msg
        {
            get { return _msg; }
            set
            {
                Time = DateTime.Now;
                _msg = value;
            }
        }
    }
}
