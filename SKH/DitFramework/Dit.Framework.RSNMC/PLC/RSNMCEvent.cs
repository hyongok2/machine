using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;

namespace Dit.Framework.RSNMC.PLC
{
    public enum EmRSNMCEvent
    {
        IMMEDIATE_STOP,
        SETTING_SAVE,
        RESET,
    }

    /// <summary>
    /// 설비에서 VirtualRSNMC통해서 오는 Event 처리
    /// date 180710
    /// since 180710
    /// </summary>
    public class RSNMCEvent
    {        
        public int Step = 0;
        private DateTime _stepTime = DateTime.Now;

        public string Name { get; set; }

        public PlcAddr XB_EVENT { get; set; }
        public PlcAddr YB_EVENT_ACK { get; set; }
        /// <summary>
        /// 입력신호 ON-OFF 후 처리 Action
        /// </summary>
        public Action<RSNMCEvent> OnEventComplete { get; set; }
        /// <summary>
        /// 입력신호 ON 시 처리 Action
        /// </summary>
        public Action<RSNMCEvent> OnEvent { get; set; }

        public void LogicWorking()
        {
            if (Step == 0)
            {
                if (XB_EVENT.vBit)
                {
                    _stepTime = DateTime.Now;

                    if (OnEvent != null)
                        OnEvent(this);

                    YB_EVENT_ACK.vBit = true;
                    Step = 10;
                }
            }
            else if (Step == 10)
            {
                if (XB_EVENT.vBit == false)
                {
                    if (OnEventComplete != null)
                        OnEventComplete(this);

                    YB_EVENT_ACK.vBit = false;

                    Step = 20;
                }
            }
            else if (Step == 20)
            {
                Step = 0;
            }
        }
    }
}
