using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Dit.Framework.Ini
{
    public class BaseSetting
    {        
        public virtual bool Save(string path)
        {
            try
            {
                IniFileManager.SaveIni(path, this);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public virtual bool Load(string path)
        {
            try
            {
                IniFileManager.LoadIni(path, this);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
