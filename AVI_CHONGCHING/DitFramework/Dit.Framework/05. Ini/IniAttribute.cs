using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Dit.Framework.Ini
{
    public class IniAttribute : Attribute
    {
        public string Section { get; set; }
        public string Key { get; set; }
        public object DefValue { get; set; }

        public IniAttribute(string section, string key, object defValue)
        {
            this.Section = section;
            this.Key = key;
            this.DefValue = defValue;
        }
    }
}
