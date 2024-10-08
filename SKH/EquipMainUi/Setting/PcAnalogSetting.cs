using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Ini;
using System.Windows.Forms;
using System.IO;

namespace EquipMainUi.Setting
{
    public class PcAnalogSetting : BaseSetting
    {
        public static string PATH_SETTING = Path.Combine(GG.StartupPath, "Setting", "AnalogInfo.ini");

        public int Soft_Maximum_Limit_Temp = 50;
        public int Soft_Minimum_Limit_Temp = 20;

        public float Soft_Maximum_Limit_DA = 0.9f;
        public float Soft_Minimum_Limit_DA = 0.005f;

        [IniAttribute("Setting", "PC_RACK_TEMPERATURE", 40)]
        public int Pc_Rack_Temp { get; set; }
        [IniAttribute("Setting", "PC_RACK_TEMPERATURE_LIGHTALARM_OFFSET", 3)]
        public int Pc_Rack_Temp_LigthAlarm_Offset { get; set; }
        [IniAttribute("Setting", "PANEL_TEMPERATURE", 40)]
        public int Panel_Temp { get; set; }
        [IniAttribute("Setting", "PANEL_TEMPERATURE_LIGHTALARM_OFFSET", 3)]
        public int Panel_Temp_LightAlarm_Offset { get; set; }
        [IniAttribute("Setting", "DA1_CH1_WRITE_DATA", 0.0f)]
        public float DA1_CH1_WriteData { get; set; }
        [IniAttribute("Setting", "DA1_CH2_WRITE_DATA", 0.0f)]
        public float DA1_CH2_WriteData { get; set; }
        [IniAttribute("Setting", "DA1_CH3_WRITE_DATA", 0.0f)]
        public float DA1_CH3_WriteData { get; set; }
        [IniAttribute("Setting", "DA1_CH4_WRITE_DATA", 0.0f)]
        public float DA1_CH4_WriteData { get; set; }
        [IniAttribute("Setting", "DA1_CH5_WRITE_DATA", 0.0f)]
        public float DA1_CH5_WriteData { get; set; }
        [IniAttribute("Setting", "DA1_CH6_WriteData", 0.0f)]
        public float DA1_CH6_WriteData { get; set; }
        [IniAttribute("Setting", "DA1_CH7_WriteData", 0.0f)]
        public float DA1_CH7_WriteData { get; set; }
        [IniAttribute("Setting", "DA1_CH8_WriteData", 0.0f)]
        public float DA1_CH8_WriteData { get; set; }

        public PcAnalogSetting()
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
        public void SetTemperature(int nIdx, int temperature)
        {
            int temp = temperature;
            switch (nIdx)
            {
                case 0:
                    Pc_Rack_Temp = CheckTempLimitValue(temperature); 
                    break;
                case 1:
                    Pc_Rack_Temp_LigthAlarm_Offset = temp;
                    break;                    
                case 2:
                    Panel_Temp = CheckTempLimitValue(temperature);
                    break;
                case 3:
                    Panel_Temp_LightAlarm_Offset = temp;
                    break;
                default:
                    throw new Exception("미저정 코드");
            }
        }
        public int CheckTempLimitValue(int temp)
        {
            temp = (temp < Soft_Minimum_Limit_Temp) ? Soft_Minimum_Limit_Temp : temp;
            temp = (temp > Soft_Maximum_Limit_Temp) ? Soft_Maximum_Limit_Temp : temp;
            return temp;
        }
        public void SetDAValue(int index, float writeData)
        {
            float data = CheckDALimitValue(writeData);

            switch (index)
            {
                case 0:
                    DA1_CH1_WriteData = data;
                    break;
                case 1:
                    DA1_CH2_WriteData = data;
                    break;
                case 2:
                    DA1_CH3_WriteData = data;
                    break;
                case 3:
                    DA1_CH4_WriteData = data;
                    break;
                case 4:
                    DA1_CH5_WriteData = data;
                    break;
                default:
                    throw new Exception("미저정 코드");
            }

            Save();
        }
        public float CheckDALimitValue(float writeData)
        {
            writeData = (writeData < Soft_Minimum_Limit_DA) ? Soft_Minimum_Limit_DA : writeData;
            writeData = (writeData > Soft_Maximum_Limit_DA) ? Soft_Maximum_Limit_DA : writeData;
            return writeData;
        }
    }
}
