using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dit.Framework.Log
{
    public class SignalLog
    {
        public string Date { get; set; }
        public bool[] IsOnOff = new bool[80];

        public SignalLog()
        {
            
        }
        public static bool TryParse(string txt, out SignalLog signal)
        {
            try
            {
                signal = new SignalLog();

                string[] items = txt.Split('\t');

                if (items.Length != 2)
                    return false;

                signal.Date = items[0];

                if(items[1].Length != 80)
                    return false;

                for (int iPos = 0; iPos < 80; iPos++)
                {
                    bool isOnOff = false;
                    if (string.Compare(items[1][iPos].ToString(),"0") == 0)
                    {
                        signal.IsOnOff[iPos] = false;
                    }
                    else
                        signal.IsOnOff[iPos] = true;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                signal = new SignalLog();
                return false;
            }   
        }
    }

}
