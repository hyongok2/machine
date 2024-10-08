using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;

namespace Dit.Framework.Ini
{
  /// <summary>
    /// update 2019 11 27
    /// BaseSetting 파생클래스 멤버로 클래스 사용 가능
    /// 
    /// </summary>
    public static class IniFileManager
    {
        public static void SaveIni(string iniFilePath, BaseSetting data)
        {
            IniReader ini = new IniReader(iniFilePath);

            foreach (PropertyInfo info in data.GetType().GetProperties())
            {
                if (info.PropertyType.IsPublic || info.PropertyType.BaseType == typeof(IniDataSet))
                {
                    foreach (object obj in info.GetCustomAttributes(false))
                    {
                        IniAttribute iniAttri = obj as IniAttribute;
                        if (iniAttri != null)
                        {
                            string section = iniAttri.Section;
                            object value = info.GetValue(data, null);

                            if (info.PropertyType.BaseType == typeof(IniDataSet))
                            {
                                foreach (PropertyInfo nested in value.GetType().GetProperties())
                                {
                                    foreach (object nestedobj in nested.GetCustomAttributes(false))
                                    {
                                        IniAttribute nestediniAttri = nestedobj as IniAttribute;
                                        if (nestediniAttri != null)
                                        {
                                            PropertyInfo item = value.GetType().GetProperty(nested.Name);
                                            nestediniAttri.Section = section;
                                            object nestedvalue = item.GetValue(value, null);
                                            SetValue(ini, nested, nestedvalue, nestediniAttri);
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                                SetValue(ini, info, value, iniAttri);
                            break;
                        }
                    }
                }
            }
        }
        public static void SetValue(IniReader ini, PropertyInfo info, object value, IniAttribute iniAttri)
        {
            value = (value == null) ? iniAttri.DefValue : value;

            if (info.GetGetMethod().ReturnType == typeof(int))
            {
                ini.SetInteger(iniAttri.Section, iniAttri.Key, (int)value);
            }
            else if (info.GetGetMethod().ReturnType == typeof(long))
            {
                ini.SetLong(iniAttri.Section, iniAttri.Key, (long)value);
            }
            else if (info.GetGetMethod().ReturnType == typeof(short))
            {
                ini.SetInteger(iniAttri.Section, iniAttri.Key, (int)(short)value);
            }
            else if (info.GetGetMethod().ReturnType == typeof(float))
            {
                ini.SetFloat(iniAttri.Section, iniAttri.Key, (float)value);
            }
            else if (info.GetGetMethod().ReturnType == typeof(double))
            {
                ini.SetDouble(iniAttri.Section, iniAttri.Key, Convert.ToDouble(value));
            }
            else if (info.GetGetMethod().ReturnType == typeof(string))
            {
                ini.SetString(iniAttri.Section, iniAttri.Key, (string)value);
            }
            else if (info.GetGetMethod().ReturnType == typeof(bool))
            {
                ini.SetBoolean(iniAttri.Section, iniAttri.Key, (bool)value);
            }
            else if (info.GetGetMethod().ReturnType == typeof(DateTime))
            {
                ini.SetString(iniAttri.Section, iniAttri.Key, ((DateTime)value).ToString("yyyyMMddHHmmss"));
            }
        }
        public static void LoadIni(string iniFilePath, BaseSetting data)
        {
            IniReader ini = new IniReader(iniFilePath);
            foreach (PropertyInfo info in data.GetType().GetProperties())
            {
                if (info.PropertyType.IsPublic || info.PropertyType.BaseType == typeof(IniDataSet))
                {
                    foreach (object obj in info.GetCustomAttributes(false))
                    {
                        try
                        {
                            IniAttribute iniAttri = obj as IniAttribute;
                            if (iniAttri != null)
                            {
                                string section = iniAttri.Section;
                                object value = info.GetValue(data, null);
                                if (info.PropertyType.BaseType == typeof(IniDataSet))
                                {
                                    foreach (PropertyInfo nested in value.GetType().GetProperties())
                                    {
                                        foreach (object nestedobj in nested.GetCustomAttributes(false))
                                        {
                                            IniAttribute nestediniAttri = nestedobj as IniAttribute;
                                            if (nestediniAttri != null)
                                            {
                                                PropertyInfo item = value.GetType().GetProperty(nested.Name);
                                                nestediniAttri.Section = section;
                                                GetValue(ini, item, value, nestediniAttri);
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                    GetValue(ini, info, data, iniAttri);
                                break;
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            MessageBox.Show(ex.StackTrace);
                        }
                    }
                }
            }
        }
        public static void GetValue(IniReader ini, PropertyInfo info, object data, IniAttribute iniAttri)
        {
            if (info.GetGetMethod().ReturnType == typeof(int))
            {
                int value = ini.GetInteger(iniAttri.Section, iniAttri.Key, (int)iniAttri.DefValue);
                info.SetValue(data, value, null);
            }
            else if (info.GetGetMethod().ReturnType == typeof(short))
            {
                short value = (short)ini.GetInteger(iniAttri.Section, iniAttri.Key, (int)iniAttri.DefValue);
                info.SetValue(data, value, null);
            }
            else if (info.GetGetMethod().ReturnType == typeof(long))
            {
                long value = (long)ini.GetInteger(iniAttri.Section, iniAttri.Key, (int)iniAttri.DefValue);
                info.SetValue(data, value, null);
            }
            else if (info.GetGetMethod().ReturnType == typeof(float))
            {
                float value = ini.GetFloat(iniAttri.Section, iniAttri.Key, (float)iniAttri.DefValue);
                info.SetValue(data, value, null);
            }
            else if (info.GetGetMethod().ReturnType == typeof(double))
            {
                double value = ini.GetDouble(iniAttri.Section, iniAttri.Key, Convert.ToDouble(iniAttri.DefValue));
                info.SetValue(data, value, null);
            }
            else if (info.GetGetMethod().ReturnType == typeof(string))
            {
                string value = ini.GetString(iniAttri.Section, iniAttri.Key);
                info.SetValue(data, value, null);
            }
            else if (info.GetGetMethod().ReturnType == typeof(bool))
            {
                //bool value = ini.GetBoolean(iniAttri.Section, iniAttri.Key, (bool)iniAttri.DefValue);
                //info.SetValue(data, value, null);
            }
            else if (info.GetGetMethod().ReturnType == typeof(DateTime))
            {
                string date = ini.GetString(iniAttri.Section, iniAttri.Key);

                DateTime datetime;
                DateTime.TryParseExact(date, "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out datetime);
                info.SetValue(data, datetime, null);
            }
        }
    }
}
