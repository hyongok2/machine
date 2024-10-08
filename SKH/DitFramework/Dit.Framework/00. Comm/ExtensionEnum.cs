using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Dit.Framework
{
    public static class EnumEx
    {
        public static int GetIndexContains(Type enumType, string containsWord, bool ignoreCase = true)
        {
            bool found = false;
            foreach (string name in Enum.GetNames(enumType))
            {
                if (ignoreCase)
                    found = Regex.IsMatch(name, containsWord, RegexOptions.IgnoreCase);
                else
                    found = name.Contains(containsWord);

                if (found)
                    return (int)Enum.Parse(enumType, name);
            }
            return -1;
        }
    }
}
