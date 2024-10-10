using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Timers;
using EquipSimulator.Log;
using Dit.Framework.Log;
using Dit.Framework.PLC;

namespace EquipSimulator.Acturator
{
    public class SylinderSimul
    {
        private Timer _tmrF = new Timer();
        private Timer _tmrB = new Timer();
        public string Name { get; set; }
        private DateTime _moveStartTime = DateTime.Now;


        public int TotalLength { get; set; }
        public int CurrPosition { get; set; }

        public PlcAddr YB_ForwardCmd { get; set; }
        public PlcAddr YB_BackwardCmd { get; set; }

        public PlcAddr XB_ForwardComplete { get; set; }
        public PlcAddr XB_BackwardComplete { get; set; }


        public double Position
        {
            get
            {
                return CurrPosition;
            }
        }
        public void Initialize()
        {
        }
        bool isOneWay;
        public SylinderSimul(bool _isOneWay = false)
        {
            isOneWay = _isOneWay;
        }
        public void LogicWorking()
        {
            Working();
        }
        private void Working()
        {
            if (YB_ForwardCmd.vBit)
            {
                int tmpCurrPosition = CurrPosition + 20;
                if (tmpCurrPosition >= TotalLength)
                {
                    tmpCurrPosition = TotalLength;
                    XB_ForwardComplete.vBit = true;
                }
                else
                {
                    XB_ForwardComplete.vBit = false;
                    XB_BackwardComplete.vBit = false;
                }
                CurrPosition = tmpCurrPosition;
            }

            if (isOneWay == false && YB_BackwardCmd.vBit)
            {
                int tmpCurrPosition = CurrPosition - 20;
                if (tmpCurrPosition <= 0)
                {
                    tmpCurrPosition = 0;
                    XB_BackwardComplete.vBit = true;
                }
                else
                {
                    XB_ForwardComplete.vBit = false;
                    XB_BackwardComplete.vBit = false;
                }
                CurrPosition = tmpCurrPosition;
            }
            else if(isOneWay == true && YB_ForwardCmd.vBit == false)
            {
                int tmpCurrPosition = CurrPosition - 20;
                if (tmpCurrPosition <= 0)
                {
                    tmpCurrPosition = 0;
                    XB_BackwardComplete.vBit = true;
                }
                else
                {
                    XB_ForwardComplete.vBit = false;
                    XB_BackwardComplete.vBit = false;
                }
                CurrPosition = tmpCurrPosition;
            }

        }
    }
}
