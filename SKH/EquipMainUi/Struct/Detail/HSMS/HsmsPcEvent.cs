using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;

namespace EquipMainUi.Struct.Detail.HSMS
{
    public class HsmsPcEvent
    {
        private int SignalTimeOut = 8;
        public int Step = 0;
        private DateTime _stepTime = DateTime.Now;

        public string Name { get; set; }

        public EM_AL_LST TimeOver { get; set; }
        public PlcAddr XB_EVENT { get; set; }
        public PlcAddr YB_EVENT_ACK { get; set; }
        public Action<Equipment, HsmsPcEvent> OnEvent { get; set; }
        public Action<Equipment, HsmsPcEvent> OnBeforeAck { get; set; }
        public bool IsNeedConfirmEvent { get; set; }
        public bool IsConfirmOK { get; set; }
        public PlcAddr XW_CONFIRM { get; set; }
        public HsmsPcEvent(EmHsmsPcEvent evt)
        {
            Name = evt.ToString();
        }
        public void LogicWorking(Equipment equip)
        {
            if (Step == 10 && (DateTime.Now - _stepTime).TotalSeconds > SignalTimeOut)
            {
                Logger.CIMLog.AppendLine(LogLevel.Error, "제어↔HSMS PC {0} 이벤트 TIME OVER [현재 스텝 : {1}]", Name, Step.ToString());
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0722_HSMS_PC_EVENT_SIGNAL_TIMEOVER);
                Step = 0;
                return;
            } 
            if (Step == 0)
            {
                YB_EVENT_ACK.vBit = false;
                if (XB_EVENT.vBit)
                {
                    _stepTime = DateTime.Now;
                    Logger.CIMLog.AppendLine(LogLevel.Info, "HSMS→제어 {0} 이벤트 시작", Name);
                    
                    OnBeforeAck?.Invoke(equip, this);
                    YB_EVENT_ACK.vBit = true;
                    Logger.CIMLog.AppendLine(LogLevel.Info, "제어→HSMS {0} 이벤트 Ack 신호 송신 ", Name);
                    Step = 10;
                }
            }
            else if (Step == 10)
            {
                if (XB_EVENT.vBit == false)
                {
                    OnEvent?.Invoke(equip, this);
                    
                    Logger.CIMLog.AppendLine(LogLevel.Info, "HSMS→제어 {0} 이벤트 종료", Name);
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
