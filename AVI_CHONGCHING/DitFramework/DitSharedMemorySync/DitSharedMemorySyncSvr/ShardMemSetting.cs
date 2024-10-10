using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Ini;
using System.IO;
using System.Windows.Forms;

namespace DitSharedMemorySvr
{
    public class ShardMemSetting : BaseSetting
    {
        public string PathOfSetting = Path.Combine(Application.StartupPath, "Setting", "Setting.ini");
        private const string SETTING_SECTION = "Setting";

        [IniAttribute("Setting", "ServerIp", "127.0.0.1")]
        public string ServerIp { get; set; }

        [IniAttribute("Setting", "ServerPort", "127.0.0.1")]
        public string ServerPort { get; set; }

        [IniAttribute("Memory", "Count", 0)]
        public int Count { get; set; }          
    }
}
