using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipComm.PLC;

namespace EquipMainUi.Struct.Detail
{
    public class PMacmd
    {
        private int SignalTimeOut = 30;
        public string Name { get; set; }
        public EM_AL_LST TimeOver { get; set; }

        public PlcAddr YB_CMD { get; set; }
        public PlcAddr XB_CMD_ACK { get; set; }

        public int Step = 0;
        private DateTime _stepTime = DateTime.Now;

        public void LogicWorking(Equipment equip)
        {
            if (Step > 10 && (DateTime.Now - _stepTime).TotalSeconds > SignalTimeOut)
            {
                Logger.Log.AppendLine(LogLevel.Error, "제어↔UMAC {0} 시그널 TIME OVER", Name);
                AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0091_UMAC_EVENT_SIGNAL_TIMEOVER);

                YB_CMD.vBit = false;
                Step = 0;
                return;
            }

            if (Step == 0)
            {
            }
            else if (Step == 10)
            {
                _stepTime = DateTime.Now;
                YB_CMD.vBit = true;
                Logger.Log.AppendLine(LogLevel.Info, "제어→UMAC {0} 시그널 시작", Name);
                Step = 20;
            }
            else if (Step == 20)
            {
                if (XB_CMD_ACK.vBit)  //|| GG.TestMode
                {
                    Logger.Log.AppendLine(LogLevel.Info, "UMAC→제어 {0} 시그널 종료 - {1}", Name, XB_CMD_ACK.vBit ? "성공" : "실패");
                    YB_CMD.vBit = false;

                    Step = 0;
                }
            }
        }
    }

    public enum EmPMacmd
    {
        REVIEW_TIME_OVER,
        PMAC_RESET
    }
    public class PMacProxy
    {
        public PlcAddr YB_EquipMode { get; set; }
        public PlcAddr YB_CheckAlarmStatus { get; set; }
        public PlcAddr YB_UpperInterfaceWorking { get; set; }
        public PlcAddr YB_LowerInterfaceWorking { get; set; }

        public PlcAddr XB_PmacReady { get; set; }
        public PlcAddr XB_PmacAlive { get; set; }
        public PlcAddr XB_PmacHeavyAlarm { get; set; }

        public PlcAddr YB_PinUpMotorInterlockDisable { get; set; }
        public PlcAddr XB_PinUpMotorInterlockDiableAck { get; set; }

        public PlcAddr YB_ReviewTimerOverCmd { get; set; }
        public PlcAddr XB_ReviewTimerOverCmdAck { get; set; }

        public PlcAddr YB_PmacResetCmd { get; set; }
        public PlcAddr XB_PmacResetCmdAck { get; set; }

        public PlcAddr XB_ReviewUsingPmac { get; set; }

        public EM_AL_LST HEAVY_ERROR { get; set; }

        public PMacmd[] LstPMacCmd = new PMacmd[10];

        public bool Initailize()
        {
            LstPMacCmd[(int)EmPMacmd.REVIEW_TIME_OVER]  /**/ = new PMacmd() { Name = "REVIEW_TIME_OVER", /**/ YB_CMD = YB_ReviewTimerOverCmd,       /**/ XB_CMD_ACK = XB_ReviewTimerOverCmdAck };
            LstPMacCmd[(int)EmPMacmd.PMAC_RESET]        /**/ = new PMacmd() { Name = "PMAC_RESET",       /**/ YB_CMD = YB_ReviewTimerOverCmd,       /**/ XB_CMD_ACK = XB_ReviewTimerOverCmdAck };

            return true;
        }
        public void LogicWorking(Equipment equip)
        {
            YB_EquipMode.vInt = (equip.EquipRunMode == EmEquipRunMode.Manual) ? 0 : 1;
            YB_CheckAlarmStatus.vInt = (equip.IsHeavyAlarm != true) ? 0 : 1;

            YB_UpperInterfaceWorking.vInt = (equip.PioI2ARecv.StepPioRecv == 0) ? 0 : 1;
            YB_LowerInterfaceWorking.vInt = (equip.PioA2ISend.StepPioSend == 0 && equip.PioA2IExch.StepPioExchage == 0) ? 0 : 1;


            //YB_PinUpMotorInterlockDisable.vInt = equip.IsUseInterLockOff ? 1 :
            //                                     equip.LiftPin.IsMoveOnPosition(LiftPinServo.LiftPinDown) == true ||
            //                                     equip.LiftPin.IsMoveOnPosition(LiftPinServo.LiftPinHome) == true ? 1 : 0; // 1일때 제어 가능...

            //Inter Lock 확인후 적용 예정
            //YB_PinUpMotorInterlockDisable.vInt = equip.IsUseInterLockOff ? 1 : 0;
            YB_PinUpMotorInterlockDisable.vInt = 1;

            foreach (PMacmd cmd in LstPMacCmd)
            {
                if (cmd == null) continue;
                cmd.LogicWorking(equip);
            }

            CheckServoStatus(equip);
           
            if(GG.TestMode == false)
                CheckAlive(equip);
        }
        private bool _isHappenAlarm = false;
        private void CheckServoStatus(Equipment equip)
        {
            if (_isHappenAlarm)
            {
                if (this.XB_PmacHeavyAlarm)
                {
                    AlarmMgr.Instatnce.Happen(equip, HEAVY_ERROR);

                    _isHappenAlarm = true;
                }
            }
        }
        //ALIVE 처리..
        private DateTime _aliveDateTime = DateTime.Now;
        private bool _pmacPlcAlive = false;
        private int StepTimeOut = 10;

        private void CheckAlive(Equipment equip)
        {
            if (_pmacPlcAlive != XB_PmacAlive.vBit)
            {
                _aliveDateTime = DateTime.Now;
                _pmacPlcAlive = XB_PmacAlive.vBit;
            }

            if ((DateTime.Now - _aliveDateTime).TotalSeconds > StepTimeOut)
            {
                _aliveDateTime = DateTime.Now.AddHours(10);
                AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0092_PMAC_ALIVE_ERROR);
                Logger.Log.AppendLine(LogLevel.Error, "PMAC ALIVE ERROR 발생함");
            }
        }

        public void StartReset(Equipment equip)
        {
            _isHappenAlarm = false;
        }
        public bool StartCommand(Equipment equip, EmPMacmd cmd, object tag)
        {
            if (LstPMacCmd[(int)cmd].Step != 0)
            {
                InterLockMgr.AddInterLock(string.Format("UMAC와 {0} 기존 명령이 진행 중", cmd.ToString()));
                Logger.Log.AppendLine(LogLevel.Warning, string.Format("UMAC와 {0} 기존 명령이 진행 중", cmd.ToString()));
                return false;
            }

            LstPMacCmd[(int)cmd].Step = 10;
            return true;
        }
        public bool IsCommandAck(Equipment equip, EmPMacmd cmd)
        {
            return LstPMacCmd[(int)cmd].Step == 0;
        }
    }
}
