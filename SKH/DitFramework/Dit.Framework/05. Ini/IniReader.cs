using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

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

        public string CommentDelimiter { get; set; }

        private string theFile = null;

        public string TheFile
        {
            get
            {
                return theFile;
            }
            set
            {
                theFile = null;
                dictionary.Clear();
                if (File.Exists(value))
                {
                    theFile = value;
                    using (StreamReader sr = new StreamReader(theFile))
                    {
                        string line, section = "";
                        while ((line = sr.ReadLine()) != null)
                        {
                            line = line.Trim();
                            if (line.Length == 0) continue;  // empty line
                            if (!String.IsNullOrEmpty(CommentDelimiter) && line.StartsWith(CommentDelimiter))
                                continue;  // comment

                            if (line.StartsWith("[") && line.Contains("]"))  // [section]
                            {
                                int index = line.IndexOf(']');
                                section = line.Substring(1, index - 1).Trim();
                                continue;
                            }

                            if (line.Contains("="))  // key=value
                            {
                                int index = line.IndexOf('=');
                                string key = line.Substring(0, index).Trim();
                                string val = line.Substring(index + 1).Trim();
                                string key2 = String.Format("[{0}]{1}", section, key).ToLower();

                                if (val.StartsWith("\"") && val.EndsWith("\""))  // strip quotes
                                    val = val.Substring(1, val.Length - 2);

                                if (dictionary.ContainsKey(key2))  // multiple values can share the same key
                                {
                                    index = 1;
                                    string key3;
                                    while (true)
                                    {
                                        key3 = String.Format("{0}~{1}", key2, ++index);
                                        if (!dictionary.ContainsKey(key3))
                                        {
                                            dictionary.Add(key3, val);
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    dictionary.Add(key2, val);
                                }
                            }
                        }
                    }
                }
            }
        }

        private Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public IniReader(string fileName)
        {
            IniFileName = fileName;

            TheFile = fileName;
        }

        public string this[string section, string key]
        {
            get
            {
                return GetStringTrim(section, key, "");
            }
        }
        private bool TryGetValue(string section, string key, out string value)
        {
            string key2;
            if (section.StartsWith("["))
                key2 = String.Format("{0}{1}", section, key);
            else
                key2 = String.Format("[{0}]{1}", section, key);

            return dictionary.TryGetValue(key2.ToLower(), out value);
        }

        //BOOL
        public bool GetBoolean(string Section, string Key, bool def)
        {
            string value = GetStringTrim(Section, Key, "");
            return value == "0" ? false : value == "1" ? true : def;
        }
        public void SetBoolean(string Section, string Key, bool Value)
        {
            SetString(Section, Key, Value ? "1" : "0");
        }
        public void SetBoolean(string Section, string Key, double Value)
        {
            SetString(Section, Key, Value.ToString());
        }
        public void SetBoolean(string Section, string Key, float Value)
        {
            SetString(Section, Key, Value.ToString());
        }



        //SHORT, INTEGER
        public short GetShort(string Section, string Key, short def)
        {
            string value = GetString(Section, Key, "");
            short result = 0;
            if (short.TryParse(value, out result))
                return result;
            else
                return def;
        }
        public int GetInteger(string Section, string Key, int def)
        {
            string value = GetStringTrim(Section, Key, "");
            int result = 0;
            if (int.TryParse(value, out result))
                return result;
            else
                return def;
        }
        public void SetInteger(string Section, string Key, int Value)
        {
            SetString(Section, Key, Value.ToString());
        }

        //LONG
        public long GetLong(string Section, string Key, long def)
        {
            string value = GetString(Section, Key, "");
            long result = 0;
            if (long.TryParse(value, out result))
                return result;
            else
                return def;
        }
        public void SetLong(string Section, string Key, long Value)
        {
            SetString(Section, Key, Value.ToString());
        }


        //FLOAT
        public float GetFloat(string Section, string Key, float def)
        {
            string value = GetStringTrim(Section, Key, "");
            float result = 0f;
            if (float.TryParse(value, out result))
                return result;
            else
                return def;
        }
        public void SetFloat(string Section, string Key, float Value)
        {
            SetString(Section, Key, Value.ToString());
        }

        //DOBULE
        public double GetDouble(string Section, string Key, double def)
        {
            string value = GetStringTrim(Section, Key, "");
            double result = 0f;
            if (double.TryParse(value, out result))
                return result;
            else
                return def;
        }
        public void SetDouble(string Section, string Key, double Value)
        {
            SetString(Section, Key, Value.ToString());
        }

        //DOBULE
        public string GetStringTrim(string Section, string Key)
        {
            return GetString(Section, Key).Trim();
        }
        public string GetStringTrim(string Section, string Key, string defaultValue)
        {
            defaultValue = "";
            string value;
            if (!TryGetValue(Section, Key, out value))
                return defaultValue;

            return value.Trim();
        }
        public string GetString(string Section, string Key)
        {
            StringBuilder read = new StringBuilder(256);
            IniReader.GetPrivateProfileString(Section, Key, "", read, 256, IniFileName);
            string value = read.ToString();
            return value;
        }
        public string GetString(string Section, string Key, string defaultValue)
        {
            defaultValue = "";
            string value;
            if (!TryGetValue(Section, Key, out value))
                return defaultValue;

            return value;
        }
        public void SetString(string Section, string Key, string Value)
        {
            IniReader.WritePrivateProfileString(Section, Key, Value, IniFileName);
        }
    }
}