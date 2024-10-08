using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Ini;
using System.IO;
using System.Windows.Forms;

namespace DitSharedMemoryClient
{
    public class ShardMemSetting : BaseSetting
    {
        public string PathOfSetting = Path.Combine(Application.StartupPath, "Setting", "Setting.ini");
        private const string SETTING_SECTION = "Setting";

        [IniAttribute("Memory", "Count", 0)]
        public int Count { get; set; }          
    }
}
