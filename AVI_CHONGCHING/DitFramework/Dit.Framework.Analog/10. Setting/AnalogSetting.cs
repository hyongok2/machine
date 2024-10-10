using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Dit.Framework.Ini;

namespace Dit.Framework.Analog
{
    public class AnalogSetting : BaseSetting
    {
        public string PATH_SETTING { get; set; }
        
        public static string PATH_SETTING_AD1 = Path.Combine(Application.StartupPath, "Setting", "AD1_SETTING.ini");
        public static string PATH_SETTING_AD2 = Path.Combine(Application.StartupPath, "Setting", "AD2_SETTING.ini");
        public static string PATH_SETTING_AD3 = Path.Combine(Application.StartupPath, "Setting", "AD3_SETTING.ini");
        public static string PATH_SETTING_AD4 = Path.Combine(Application.StartupPath, "Setting", "AD4_SETTING.ini");
        public static string PATH_SETTING_AD5 = Path.Combine(Application.StartupPath, "Setting", "AD5_SETTING.ini");
        public static string PATH_SETTING_AD6 = Path.Combine(Application.StartupPath, "Setting", "AD6_SETTING.ini");
        public static string PATH_SETTING_AD7 = Path.Combine(Application.StartupPath, "Setting", "AD7_SETTING.ini");
        public static string PATH_SETTING_AD8 = Path.Combine(Application.StartupPath, "Setting", "AD8_SETTING.ini");
        public static string PATH_SETTING_AD9 = Path.Combine(Application.StartupPath, "Setting", "AD9_SETTING.ini");
        public static string PATH_SETTING_AD10 = Path.Combine(Application.StartupPath, "Setting", "AD10_SETTING.ini");

        public static string PATH_SETTING_TD1 = Path.Combine(Application.StartupPath, "Setting", "TD1_SETTING.ini");
        public static string PATH_SETTING_TD2 = Path.Combine(Application.StartupPath, "Setting", "TD1_SETTING.ini");
        public static string PATH_SETTING_TD3 = Path.Combine(Application.StartupPath, "Setting", "TD1_SETTING.ini");

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

        //Value -> 전압/전류 변환 비율
        [IniAttribute("Setting", "CH1_Ratio", 0.0f)]
        public float CH1_Ratio { get; set; }
        [IniAttribute("Setting", "CH2_Ratio", 0.0f)]
        public float CH2_Ratio { get; set; }
        [IniAttribute("Setting", "CH3_Ratio", 0.0f)]
        public float CH3_Ratio { get; set; }
        [IniAttribute("Setting", "CH4_Ratio", 0.0f)]
        public float CH4_Ratio { get; set; }
        [IniAttribute("Setting", "CH5_Ratio", 0.0f)]
        public float CH5_Ratio { get; set; }
        [IniAttribute("Setting", "CH6_Ratio", 0.0f)]
        public float CH6_Ratio { get; set; }
        [IniAttribute("Setting", "CH7_Ratio", 0.0f)]
        public float CH7_Ratio { get; set; }
        [IniAttribute("Setting", "CH8_Ratio", 0.0f)]
        public float CH8_Ratio { get; set; }

        public AnalogSetting()
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
                    throw new Exception("Unspecified code");
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
                    throw new Exception("Unspecified code");
            }
        }
        public void SetRatio(int nIdx, float ratio)
        {
            switch (nIdx)
            {
                case 0:
                    CH1_Ratio = ratio;
                    break;
                case 1:
                    CH2_Ratio = ratio;
                    break;
                case 2:
                    CH3_Ratio = ratio;
                    break;
                case 3:
                    CH4_Ratio = ratio;
                    break;
                case 4:
                    CH5_Ratio = ratio;
                    break;
                case 5:
                    CH6_Ratio = ratio;
                    break;
                case 6:
                    CH7_Ratio = ratio;
                    break;
                case 7:
                    CH8_Ratio = ratio;
                    break;
                default:
                    throw new Exception("Unspecified code");
            }
        }
        public float GetRatio(int nIdx)
        {
            switch (nIdx)
            {
                case 0:
                    return CH1_Ratio;
                case 1:
                    return CH2_Ratio;
                case 2:
                    return CH3_Ratio;
                case 3:
                    return CH4_Ratio;
                case 4:
                    return CH5_Ratio;
                case 5:
                    return CH6_Ratio;
                case 6:
                    return CH7_Ratio;
                case 7:
                    return CH8_Ratio;
                default:
                    throw new Exception("Unspecified code");
            }
        }
    }
}