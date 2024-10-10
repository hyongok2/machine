using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Dit.Framework.Ini
{
    public class IniReader
    {
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        //[DllImport("kernel32")]
        //public static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        public string IniFileName { get; set; }
        public IniReader(string fileName)
        {
            IniFileName = fileName;
        }
        public bool GetBoolean(string Section, string Key, bool def)
        {
            string value = GetString(Section, Key);
            return value == "0" ? false : value == "1" ? true : def;
        }
        public double GetDouble(string Section, string Key, double def)
        {
            string value = GetString(Section, Key);
            double result = 0f;
            if (double.TryParse(value, out result))
                return result;
            else
                return def;
        }
        public float GetFloat(string Section, string Key, float def)
        {
            string value = GetString(Section, Key);
            float result = 0f;
            if (float.TryParse(value, out result))
                return result;
            else
                return def;
        }
        public long GetLong(string Section, string Key, long def)
        {
            string value = GetString(Section, Key);
            long result = 0;
            if (long.TryParse(value, out result))
                return result;
            else
                return def;
        }
        public int GetInteger(string Section, string Key, int def)
        {
            string value = GetString(Section, Key);
            int result = 0;
            if (int.TryParse(value, out result))
                return result;
            else
                return def;
        }
        public short GetShort(string Section, string Key, short def)
        {
            string value = GetString(Section, Key);
            short result = 0;
            if (short.TryParse(value, out result))
                return result;
            else
                return def;
        }
        public string GetString(string Section, string Key)
        {
            StringBuilder read = new StringBuilder(256);
            IniReader.GetPrivateProfileString(Section, Key, "", read, 256, IniFileName);
            string value = read.ToString().Trim();
            return value;
        }
        public void SetBoolean(string Section, string Key, bool Value)
        {
            SetString(Section, Key, Value ? "1" : "0");
        }
        public void SetDouble(string Section, string Key, double Value)
        {
            SetString(Section, Key, Value.ToString());
        }
        public void SetFloat(string Section, string Key, float Value)
        {
            SetString(Section, Key, Value.ToString());
        }
        public void SetInteger(string Section, string Key, int Value)
        {
            SetString(Section, Key, Value.ToString());
        }
        public void SetLong(string Section, string Key, long Value)
        {
            SetString(Section, Key, Value.ToString());
        }
        public void SetString(string Section, string Key, string Value)
        {
            IniReader.WritePrivateProfileString(Section, Key, Value, IniFileName);
        }

        
    }
}
