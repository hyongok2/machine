using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Ini;
using System.Windows.Forms;
using System.IO;

namespace EquipSimulator.Setting
{

    public class PMacLiftPinServo : BaseSetting
    {
        [IniAttribute("Setting", "IMG_PATH", 0)]
        public double JOG_SPEED { get; set; }
    }

    public class PMacSetting
    {
        public string PathOfSetting = Path.Combine(Application.StartupPath, "Setting", "Setting.ini");
        public PMacLiftPinServo LiftPin = new PMacLiftPinServo();
    }
}
