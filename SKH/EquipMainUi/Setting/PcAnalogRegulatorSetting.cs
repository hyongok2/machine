using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Ini;
using System.Windows.Forms;
using System.IO;

namespace EquipMainUi.Setting
{
    public class PcAnalogRegulatorSetting : BaseSetting
    {
        public string PATH_SETTING { get; set; }
        public static string PATH_SETTING_AD1 = Path.Combine(GG.StartupPath, "Setting", "AD1_SETTING.ini");
        public static string PATH_SETTING_AD2 = Path.Combine(GG.StartupPath, "Setting", "AD2_SETTING.ini");
        public static string PATH_SETTING_AD3 = Path.Combine(GG.StartupPath, "Setting", "AD3_SETTING.ini");
        public static string PATH_SETTING_TD1 = Path.Combine(GG.StartupPath, "Setting", "TD1_SETTING.ini");

        [IniAttribute("Setting", "CH1_Offset", 0.0f)]
        public float CH1_Offset { get; set; }

        [IniAttribute("Setting", "CH2_Offset", 0.0f)]
        public float CH2_Offset { get; set; }

        [IniAttribute("Setting", "CH3_Offset", 0.0f)]
        public float CH3_Offset { get; set; }

        [IniAttribute("Setting", "CH4_Offset", 0.0f)]
        public float CH4_Offset { get; set; }

        [IniAttribute("Setting", "CH5_Offset", 0.0f)]
        public float CH5_Offset { get; set; }

        [IniAttribute("Setting", "CH6_Offset", 0.0f)]
        public float CH6_Offset { get; set; }

        [IniAttribute("Setting", "CH7_Offset", 0.0f)]
        public float CH7_Offset { get; set; }

        [IniAttribute("Setting", "CH8_Offset", 0.0f)]
        public float CH8_Offset { get; set; }

        [IniAttribute("Setting", "CH1_Unit", "MPa")]
        public string CH1_Unit { get; set; }

        [IniAttribute("Setting", "CH2_Unit", "MPa")]
        public string CH2_Unit { get; set; }

        [IniAttribute("Setting", "CH3_Unit", "MPa")]
        public string CH3_Unit { get; set; }

        [IniAttribute("Setting", "CH4_Unit", "MPa")]
        public string CH4_Unit { get; set; }

        [IniAttribute("Setting", "CH5_Unit", "MPa")]
        public string CH5_Unit { get; set; }

        [IniAttribute("Setting", "CH6_Unit", "MPa")]
        public string CH6_Unit { get; set; }

        [IniAttribute("Setting", "CH7_Unit", "MPa")]
        public string CH7_Unit { get; set; }

        [IniAttribute("Setting", "CH8_Unit", "MPa")]
        public string CH8_Unit { get; set; }

        public PcAnalogRegulatorSetting()
        {
            UseAutoBackup = true;
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
        public void SetOffset(int nIdx, float offset)
        {
            switch (nIdx)
            {
                case 0:
                    CH1_Offset = offset;
                    break;
                case 1:
                    CH2_Offset = offset;
                    break;
                case 2:
                    CH3_Offset = offset;
                    break;
                case 3:
                    CH4_Offset = offset;
                    break;
                case 4:
                    CH5_Offset = offset;
                    break;
                case 5:
                    CH6_Offset = offset;
                    break;
                case 6:
                    CH7_Offset = offset;
                    break;
                case 7:
                    CH8_Offset = offset;
                    break;
                default:
                    throw new Exception("미저정 코드");
            }
        }
        public float GetOffset(int nIdx)
        {
            switch (nIdx)
            {
                case 0:
                    return CH1_Offset;
                case 1:
                    return CH2_Offset;
                case 2:
                    return CH3_Offset;
                case 3:
                    return CH4_Offset;
                case 4:
                    return CH5_Offset;
                case 5:
                    return CH6_Offset;
                case 6:
                    return CH7_Offset;
                case 7:
                    return CH8_Offset;
                default:
                    throw new Exception("미저정 코드");
            }
        }
        public void SetUnit(int nIdx, string unit)
        {
            switch (nIdx)
            {
                case 0:
                    CH1_Unit = unit;
                    break;
                case 1:
                    CH2_Unit = unit;
                    break;
                case 2:
                    CH3_Unit = unit;
                    break;
                case 3:
                    CH4_Unit = unit;
                    break;
                case 4:
                    CH5_Unit = unit;
                    break;
                case 5:
                    CH6_Unit = unit;
                    break;
                case 6:
                    CH7_Unit = unit;
                    break;
                case 7:
                    CH8_Unit = unit;
                    break;
                default:
                    throw new Exception("미저정 코드");
            }
        }
        public string GetUnit(int nIdx)
        {
            switch (nIdx)
            {
                case 0:
                    return CH1_Unit;
                case 1:
                    return CH2_Unit;
                case 2:
                    return CH3_Unit;
                case 3:
                    return CH4_Unit;
                case 4:
                    return CH5_Unit;
                case 5:
                    return CH6_Unit;
                case 6:
                    return CH7_Unit;
                case 7:
                    return CH8_Unit;
                default:
                    throw new Exception("미저정 코드");
            }
        }
    }
}