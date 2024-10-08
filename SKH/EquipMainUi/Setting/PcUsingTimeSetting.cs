using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Ini;
using System.Windows.Forms;
using System.IO;

namespace EquipMainUi.Setting
{
    public class PcUsingTimeSetting : BaseSetting
    {
        public static string PATH_SETTING = Path.Combine(GG.StartupPath, "Setting", "UsingTimeInfo.ini");

        [IniAttribute("Setting", "LAMP1_USING_TIME", 0)]
        public string Lamp1 { get; set; }
        [IniAttribute("Setting", "LAMP1_DEADLINE", 1000)]
        public int Lamp1_Deadline { get; set; }
        [IniAttribute("Setting", "LAMP1_USE_ON", 0)]
        public int Lamp1_IsUsing { get; set; }

        [IniAttribute("Setting", "LAMP2_USING_TIME", 0)]
        public string Lamp2 { get; set; }
        [IniAttribute("Setting", "LAMP2_DEADLINE", 1000)]
        public int Lamp2_Deadline { get; set; }
        [IniAttribute("Setting", "LAMP2_USE_ON", 0)]
        public int Lamp2_IsUsing { get; set; }

        [IniAttribute("Setting", "LAMP3_USING_TIME", 0)]
        public string Lamp3 { get; set; }
        [IniAttribute("Setting", "LAMP3_DEADLINE", 1000)]
        public int Lamp3_Deadline { get; set; }
        [IniAttribute("Setting", "LAMP3_USE_ON", 0)]
        public int Lamp3_IsUsing { get; set; }

        [IniAttribute("Setting", "AccLongRunCount", 0)]
        public int AccLongRunCount { get; set; }
        [IniAttribute("Setting", "AccLongRunStartTime", 0)]
        public string AccLongRunStartTime { get; set; }

        public PcUsingTimeSetting()
        {   
        }
        /**
        *@param : path - null : 기본 경로
        *            not null : 경로 변경
        *               */
        public override bool Save(string path = null)
        {
            if (null != path)
                PATH_SETTING = path;
            if (!Directory.Exists(PATH_SETTING))
                Directory.CreateDirectory(PATH_SETTING.Remove(PATH_SETTING.LastIndexOf('\\')));

            return base.Save(PATH_SETTING);
        }
        /**
         *@param : path - null : 기본 경로
         *            not null : 경로 변경
         *               */
        public override bool Load(string path = null)
        {
            if (null != path)
                PATH_SETTING = path;
            return base.Load(PATH_SETTING);
        }
        public void SetTime_Lamp(int nIdx, string time)
        {
            switch (nIdx)
            {
                case 0:
                    Lamp1 = time;
                    break;
                case 1:
                    Lamp2 = time;
                    break;
                case 2:
                    Lamp3 = time;
                    break;                
                default:
                    throw new Exception("미저정 코드");
            }
        }
        public void SetTime_LampDeadline(int nIdx, int time)
        {
            switch (nIdx)
            {
                case 0:
                    Lamp1_Deadline = time;
                    break;
                case 1:
                    Lamp2_Deadline = time;
                    break;
                case 2:
                    Lamp3_Deadline = time;
                    break;
                default:
                    throw new Exception("미저정 코드");
            }
        }
        public void SetTime_LampUsing(int nIdx, bool isUsing)
        {
            switch (nIdx)
            {
                case 0:
                    Lamp1_IsUsing = Convert.ToInt32(isUsing);
                    break;
                case 1:
                    Lamp2_IsUsing = Convert.ToInt32(isUsing);
                    break;
                case 2:
                    Lamp3_IsUsing = Convert.ToInt32(isUsing);
                    break;
                default:
                    throw new Exception("미저정 코드");
            }
        }
    }
}
