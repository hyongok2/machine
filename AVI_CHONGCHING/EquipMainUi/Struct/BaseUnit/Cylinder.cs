using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;

namespace EquipMainUi.Struct
{
    public enum EmCylinderState
    {
        Forward,
        Backward,
        Moving,
    }

    public class Cylinder : UnitBase
    {

        public int TimeOutInterval { get; set; }
        public PlcAddr YB_ForwardCmd { get; set; }
        public PlcAddr YB_BackwardCmd { get; set; }

        public PlcAddr XB_ForwardComplete { get; set; }
        public PlcAddr XB_BackwardComplete { get; set; }

        public DateTime ForwardStartTime { get; set; }
        public DateTime BackwardStartTime { get; set; }

        public EM_AL_LST AlcdForwardTimeOut { get; set; }
        public EM_AL_LST AlcdBackwardTimeOut { get; set; }

        public double ForwardTime
        {
            get
            {
                return YB_ForwardCmd.vBit == true && XB_ForwardComplete.vBit == false ?
                    (DateTime.Now - ForwardStartTime).TotalMilliseconds / 100 : 0;
            }
        }
        public double BackwardTime
        {
            get
            {
                return YB_BackwardCmd.vBit == true && XB_BackwardComplete.vBit == false ?
                    (DateTime.Now - BackwardStartTime).TotalMilliseconds / 100 : 0;
            }
        }
        public Cylinder()
        {
            TimeOutInterval = 5;
            AlcdForwardTimeOut = EM_AL_LST.AL_0000_NONE;
            AlcdBackwardTimeOut = EM_AL_LST.AL_0000_NONE;
        }

        public virtual void Forward()
        {
            ForwardStartTime = DateTime.Now;
            YB_ForwardCmd.vBit = true;
            YB_BackwardCmd.vBit = false;

        }
        public virtual void Backward()
        {
            BackwardStartTime = DateTime.Now;
            YB_ForwardCmd.vBit = false;
            YB_BackwardCmd.vBit = true;

        }
        public bool IsForward
        {
            get
            {
                return XB_ForwardComplete.vBit == true && XB_BackwardComplete.vBit == false;
            }
        }
        public bool IsForwarding
        {
            get
            {
                return IsForward == false && YB_ForwardCmd == true;
            }
        }
        public bool IsBackward
        {
            get
            {
                return XB_ForwardComplete.vBit == false && XB_BackwardComplete.vBit == true;
            }
        }
        public bool IsBackwarding
        {
            get
            {
                return IsBackward == false && YB_BackwardCmd == true;
            }
        }
        public override void LogicWorking(Equipment equip)
        {
            if (XB_ForwardComplete.vBit && XB_BackwardComplete.vBit)
            {
                Console.WriteLine("실린더 완료 동시 ON Error");
            }

            if (YB_ForwardCmd.vBit && YB_BackwardCmd.vBit)
            {
                Console.WriteLine("실린더 명령 동시 ON Error");
            }

            if (YB_ForwardCmd.vBit && XB_ForwardComplete.vBit)
            {
                YB_ForwardCmd.vBit = false;
            }
            else if (YB_BackwardCmd.vBit && XB_BackwardComplete.vBit)
            {
                YB_BackwardCmd.vBit = false;
            }
            else
            {
                if (YB_ForwardCmd.vBit == true && XB_ForwardComplete.vBit == false)
                {
                    if ((DateTime.Now - ForwardStartTime).TotalSeconds > TimeOutInterval)
                    {
                        AlarmMgr.Instance.Happen(equip, AlcdForwardTimeOut);
                    }
                }

                if (YB_BackwardCmd.vBit == true && XB_BackwardComplete.vBit == false)
                {
                    if ((DateTime.Now - BackwardStartTime).TotalSeconds > TimeOutInterval)
                    {
                        AlarmMgr.Instance.Happen(equip, AlcdBackwardTimeOut);
                    }
                }
            }
        }
    }
}
