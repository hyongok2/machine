using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Dit.Framework.PLC;

namespace EquipSimulator
{
    public class SylinderLiftPinSimul
    {
        private Timer _tmrF = new Timer();
        private Timer _tmrB = new Timer();
        public string Name { get; set; }
        private DateTime _moveStartTime = DateTime.Now;


        public int TotalLength { get; set; }
        public int CurrPosition { get; set; }

        public PlcAddr YB_UpCmd { get; set; }
        public PlcAddr YB_DownCmd { get; set; }

        public PlcAddr XB_Up1Complete { get; set; }
        public PlcAddr XB_Down1Complete { get; set; }
        public PlcAddr XB_Up2Complete { get; set; }
        public PlcAddr XB_Down2Complete { get; set; }
        public PlcAddr XB_Up3Complete { get; set; }
        public PlcAddr XB_Down3Complete { get; set; }
        public PlcAddr XB_Up4Complete { get; set; }
        public PlcAddr XB_Down4Complete { get; set; }


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
        public SylinderLiftPinSimul()
        {
            CurrPosition = 0;
        }
        public void LogicWorking()
        {
            Working();
        }
        private void Working()
        {
            if (YB_UpCmd.vBit && YB_DownCmd.vBit == false)
            {
                if (CurrPosition == 0)
                    CurrPosition = 100;
            }
            else if (YB_UpCmd.vBit == false && YB_DownCmd.vBit)
            {
                if (CurrPosition == 100)
                    CurrPosition = 0;
            }

            if (CurrPosition == 0)
            {
                XB_Down1Complete.vBit = true;
                XB_Down2Complete.vBit = true;
                XB_Up1Complete.vBit = false;
                XB_Up2Complete.vBit = false;
            }
            else if (CurrPosition == 100)
            {
                XB_Down1Complete.vBit = false;
                XB_Down2Complete.vBit = false;
                XB_Up1Complete.vBit = true;
                XB_Up2Complete.vBit = true;
            }
            else
            {
                XB_Down1Complete.vBit = false;
                XB_Down2Complete.vBit = false;
                XB_Up1Complete.vBit = false;
                XB_Up2Complete.vBit = false;
            }
        }
    }
}
