using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace Dit.Framework.Ini
{
    public static class IniFileManager
    {
        public static void SaveIni(string iniFilePath, BaseSetting data)
        {
            IniReader ini = new IniReader(iniFilePath);

            foreach (PropertyInfo info in data.GetType().GetProperties())
            {
                if (info.PropertyType.IsPublic)
                {
                    foreach (object obj in info.GetCustomAttributes(false))
                    {
                        IniAttribute iniAttri = obj as IniAttribute;
                        if (iniAttri != null)
                        {
                            object value = info.GetValue(data, null);
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
                                ini.SetDouble(iniAttri.Section, iniAttri.Key, (double)value);
                            }
                            else if (info.GetGetMethod().ReturnType == typeof(string))
                            {
                                ini.SetString(iniAttri.Section, iniAttri.Key, (string)value);
                            }
                            else if (info.GetGetMethod().ReturnType == typeof(bool))
                            {
                                ini.SetBoolean(iniAttri.Section, iniAttri.Key, (bool)value);
                            }
                            break;
                        }
                    }
                }
            }
        }
        public static void LoadIni(string iniFilePath, BaseSetting data)
        {
            IniReader ini = new IniReader(iniFilePath);
            foreach (PropertyInfo info in data.GetType().GetProperties())
            {
                if (info.PropertyType.IsPublic)
                {
                    foreach (object obj in info.GetCustomAttributes(false))
                    {
                        try
                        {
                            IniAttribute iniAttri = obj as IniAttribute;
                            if (iniAttri != null)
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
                                    double value = ini.GetDouble(iniAttri.Section, iniAttri.Key, (double)iniAttri.DefValue);
                                    info.SetValue(data, value, null);
                                }
                                else if (info.GetGetMethod().ReturnType == typeof(string))
                                {
                                    string value = ini.GetString(iniAttri.Section, iniAttri.Key);
                                    info.SetValue(data, value, null);
                                }
                                else if (info.GetGetMethod().ReturnType == typeof(bool))
                                {
                                    bool value = ini.GetBoolean(iniAttri.Section, iniAttri.Key, (bool)iniAttri.DefValue);
                                    info.SetValue(data, value, null);
                                }
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
    }
}
