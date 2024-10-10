using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Detail
{
    public class TerminalMsgItem
    {
        public DateTime Time { get; set; }
        public string Message { get; set; }

        public TerminalMsgItem(DateTime time, string msg)
        {
            Time = time;
            Message = msg;
        }

        public string TimeToString()
        {
            return Time.ToString("yyMMdd HH:mm:ss");
        }
    }
}
