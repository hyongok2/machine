using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Dit.Framework.Ini;
using Dit.Framework.Xml;

namespace Dit.Framework.Analog
{
    [Serializable]
    public class DA
    {
        public float WriteData { get; set; }
        public int Offset { get; set; }
        public int Ratio { get; set; }
        public float AlarmRange { get; set; }

        public DA()
        {
            WriteData = 0;
            Offset = 0;
            Ratio = 0;
            AlarmRange = 0;
        }
    }
    [Serializable]
    public class DigitalSetting
    {
        public string PATH_SETTING { get; set; }
        public static string PATH_SETTING_T1 = Path.Combine(Application.StartupPath, "Setting", "PcDASetting_T1.ini");
        public static string PATH_SETTING_T2 = Path.Combine(Application.StartupPath, "Setting", "PcDASetting_T2.ini");
        public static string PATH_SETTING_T3 = Path.Combine(Application.StartupPath, "Setting", "PcDASetting_T3.ini");
        public static string PATH_SETTING_T4 = Path.Combine(Application.StartupPath, "Setting", "PcDASetting_T4.ini");
        public static string PATH_SETTING_T5 = Path.Combine(Application.StartupPath, "Setting", "PcDASetting_T5.ini");
        public static string PATH_SETTING_T6 = Path.Combine(Application.StartupPath, "Setting", "PcDASetting_T6.ini");
        public static string PATH_SETTING_T7 = Path.Combine(Application.StartupPath, "Setting", "PcDASetting_T7.ini");
        public static string PATH_SETTING_T8 = Path.Combine(Application.StartupPath, "Setting", "PcDASetting_T8.ini");

        public const float Soft_Maximum_Limit_DA = 0.9f;
        public const float Soft_Minimum_Limit_DA = 0.000f;

        public DA[] LstDA = new DA[8];

        public DigitalSetting()
        {
            for (int iPos = 0; iPos < 8; iPos++)
                LstDA[iPos] = new DA();
        } 

        /**
        *@param : path - null : 기본 경로
        *            not null : 경로 변경
        *               */
        public static void Save(string path, DigitalSetting setting)
        {
            XmlFileManager<DigitalSetting>.TrySaveXml(path, setting);
        }
        /**
         *@param : path - null : 기본 경로
         *            not null : 경로 변경
         *               */
        public static DigitalSetting Load(string path = null)
        {
            DigitalSetting setting = new DigitalSetting();
            if (XmlFileManager<DigitalSetting>.TryLoadData(path, out setting))
            {
                return setting;
            }
            else
            {
                return new DigitalSetting();
            }
        }
        public void SetValue(int ch, float value)
        {
            LstDA[ch].WriteData = value;
        }
        public float GetValue(int ch)
        {
            return LstDA[ch].WriteData;
        }
        public void SetOffset(int ch, int offset)
        {
            LstDA[ch].Offset = offset;
        }
        public int GetOffset(int ch)
        {
            return LstDA[ch].Offset;
        }
        public void SetRatio(int ch, int ratio)
        {
            LstDA[ch].Ratio = ratio;
        }
        public int GetRatio(int ch)
        {
            return LstDA[ch].Ratio;
        }
        public void SetAlarmRange(int ch, float alarmRange)
        {
            LstDA[ch].AlarmRange = alarmRange;
        }
        public float GetAlarmRange(int ch)
        {
            return LstDA[ch].AlarmRange;
        }
        public static float CheckDALimitValue(float writeData)
        {
            writeData = (writeData < Soft_Minimum_Limit_DA) ? Soft_Minimum_Limit_DA : writeData;
            writeData = (writeData > Soft_Maximum_Limit_DA) ? Soft_Maximum_Limit_DA : writeData;
            return writeData;
        }
        public static string GetPath(int daNo, int thick)
        {
            return Path.Combine(Application.StartupPath, "Setting", string.Format("DA{0}_SETTING_T{1}.ini", daNo, thick));
        }
    }
}