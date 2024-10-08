using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;

namespace EquipMainUi.Struct.Detail.HSMS
{
    public class HsmsPcCmd
    {
        private int SignalTimeOut = 8;
        public string Name { get; set; }
        public EM_AL_LST TimeOver { get; set; }

        public PlcAddr YB_CMD { get; set; }
        public PlcAddr XB_CMD_ACK { get; set; }
        public PlcAddr XB_CMD_NACK { get; set; }

        public object Tag { get; set; }

        public int Step = 0;
        private DateTime _stepTime = DateTime.Now;
        public Action<Equipment, HsmsPcCmd> OnCommand { get; set; }
        public Action<Equipment, HsmsPcCmd> OnAfeterAck { get; set; }

        public HsmsPcCmd(EmHsmsPcCommand cmd)
        {
            Name = cmd.ToString();
        }

        public void LogicWorking(Equipment equip)
        {
            if (Step > 10 && (DateTime.Now - _stepTime).TotalSeconds > SignalTimeOut)
            {
                Logger.CIMLog.AppendLine(LogLevel.Error, "제어↔HSMS PC {0} 시그널 TIME OVER [현재 스텝 : {1}]", Name, Step.ToString());
                if(Name == "CTRL_MODE_CHANGE")
                {
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "<CIM MODE 变更失败>" : "<CIM MODE 변경 실패>", GG.boChinaLanguage ? "CIM Program未执行或者 CIM Program 通讯不流畅" : "CIM 프로그램이 실행되지 않고 있거나 CIM프로그램 통신이 원할하지 않습니다");
                }
                //AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0723_HSMS_PC_COMMAND_SIGNAL_TIMEOVER);
                YB_CMD.vBit = false;
                Step = 0;
                return;
            }

            if (Step == 0)
            {
                YB_CMD.vBit = false;
            }
            else if (Step == 10)
            {
                _stepTime = DateTime.Now;
                Step = 20;
            }
            else if (Step == 20)
            {
                OnCommand?.Invoke(equip, this);
                Step = 30;
            }
            else if (Step == 30)
            {
                _stepTime = DateTime.Now;
                YB_CMD.vBit = true;
                Logger.CIMLog.AppendLine(LogLevel.Info, "제어→HSMS PC {0} 시그널 시작 (응답상태 : {1})", Name, XB_CMD_ACK.vBit ? "비정상(1)" : "정상(0)");
                Step = 40;
            }
            else if (Step == 40)
            {
                if (XB_CMD_ACK.vBit || GG.CimTestMode)
                {
                    YB_CMD.vBit = false;
                    Logger.CIMLog.AppendLine(LogLevel.Info, "HSMS→제어 PC {0} 시그널 수신 - {1}", Name, XB_CMD_ACK.vBit ? "성공" : "실패");
                    Step = 50;
                }
            }
            else if(Step == 50)
            {
                if(XB_CMD_ACK.vBit == false || GG.CimTestMode)
                {
                    OnAfeterAck?.Invoke(equip, this);
                    Logger.CIMLog.AppendLine(LogLevel.Info, "HSMS→제어 PC {0} 시그널 종료 - {1}", Name, XB_CMD_ACK.vBit ? "비정상(1)" : "정상(0)");

                    Step = 0;
                }
            }
        }

    }
}
