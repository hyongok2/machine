using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using System.Threading;

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
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0471_PMAC_EVENT_SIGNAL_TIMEOVER);

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
        INSP_STOP,
        REVIEW_STOP,
        PMAC_RESET,
        IMMEDIATE_STOP,
        ECAT_RECONNECT,
    }
    public class PMacProxy
    {
        public PlcAddr YB_IsAutoMode { get; set; }
        public PlcAddr YB_CheckAlarmStatus { get; set; }
        public PlcAddr YB_UpperInterfaceWorking { get; set; }
        public PlcAddr YB_LowerInterfaceWorking { get; set; }

        public PlcAddr XB_PmacReady { get; set; }
        public PlcAddr XB_PmacAlive { get; set; }
        public PlcAddr XB_EcatCommAlive{ get; set; }
        public PlcAddr XB_ReviewRunning { get; set; }
        public PlcAddr XB_InspRunning { get; set; }

        public PlcAddr YB_EquipStatusMotorInterlockOff { get; set; }
        public PlcAddr YB_PmacResetCmd { get; set; }
        public PlcAddr YB_EmsCmd { get; set; }
        public PlcAddr YB_EcatReconnectCmd { get; set; }
        public PlcAddr YB_ReviewStopCmd { get; set; }
        public PlcAddr YB_InspStopCmd { get; set; }

        public PlcAddr XB_EquipStatusMotorInterlockOffAck { get; set; }
        public PlcAddr XB_PmacResetCmdAck { get; set; }
        public PlcAddr XB_EmsCmdAck { get; set; }
        public PlcAddr XB_EcatReconnectAck { get; set; }
        public PlcAddr XB_ReviewStopAck { get; set; }
        public PlcAddr XB_InspStopAck { get; set; }

        public PlcAddr YB_LoadRateSaveCmd { get; set; }
        public PlcAddr XB_LoadRateSaveAck { get; set; }

        public PlcAddr YB_InspServerUseCmd { get; set; }
        public PlcAddr XB_InspServerUseAck { get; set; }

        public PlcAddr XF_StageXPPCmd { get; set; }
        public PlcAddr XF_StageYPPCmd { get; set; }
        public PlcAddr XF_StageTPPCmd { get; set; }
        public PlcAddr XF_StageXHmSeq { get; set; }
        public PlcAddr XF_StageYHmSeq { get; set; }
        public PlcAddr XF_StageTHmSeq { get; set; }
        public PlcAddr XF_ScanStep { get; set; }

        public PlcAddr XF_Mtr01TargetPos { get; set; }
        public PlcAddr XF_Mtr02TargetPos { get; set; }
        public PlcAddr XF_Mtr03TargetPos { get; set; }

        public EM_AL_LST HEAVY_ERROR { get; set; }

        public PMacmd[] LstPMacCmd = new PMacmd[10];

        public bool Initailize()
        {
            LstPMacCmd[(int)EmPMacmd.INSP_STOP]  /**/ = new PMacmd() { Name = "INSP_STOP", /**/ YB_CMD = YB_InspStopCmd,       /**/ XB_CMD_ACK = XB_InspStopAck };
            LstPMacCmd[(int)EmPMacmd.REVIEW_STOP]  /**/ = new PMacmd() { Name = "REVIEW_STOP", /**/ YB_CMD = YB_ReviewStopCmd,       /**/ XB_CMD_ACK = XB_ReviewStopAck };
            LstPMacCmd[(int)EmPMacmd.PMAC_RESET]        /**/ = new PMacmd() { Name = "PMAC_RESET",       /**/ YB_CMD = YB_PmacResetCmd,       /**/ XB_CMD_ACK = XB_PmacResetCmdAck };
            LstPMacCmd[(int)EmPMacmd.IMMEDIATE_STOP]    /**/ = new PMacmd() { Name = "IMMEDIATE_STOP",   /**/ YB_CMD = YB_EmsCmd,         /**/ XB_CMD_ACK = XB_EmsCmdAck };
            LstPMacCmd[(int)EmPMacmd.ECAT_RECONNECT]    /**/ = new PMacmd() { Name = "ECAT_RECONNECT",   /**/ YB_CMD = YB_EcatReconnectCmd,         /**/ XB_CMD_ACK = XB_EcatReconnectAck };

            return true;
        }
        public void LogicWorking(Equipment equip)
        {
            YB_IsAutoMode.vBit = equip.EquipRunMode == EmEquipRunMode.Auto;
            YB_CheckAlarmStatus.vBit = equip.IsHeavyAlarm == true;
            YB_UpperInterfaceWorking.vBit = equip.PioI2ARecv.StepPioRecv != 0;
            YB_LowerInterfaceWorking.vBit = equip.PioA2ISend.StepPioSend != 0;

            YB_EquipStatusMotorInterlockOff.vBit = !IsServoInterlock(equip);

            foreach (PMacmd cmd in LstPMacCmd)
            {
                if (cmd == null) continue;
                cmd.LogicWorking(equip);
            }
            SettingLogic(equip);
            CheckServoStatus(equip);
           
            if(GG.TestMode == false)
                CheckAlive(equip);
        }

        private bool IsServoInterlock(Equipment equip)
        {
            if (equip.IsUseInterLockOff)
                return false;

            return equip.LiftPin.IsBackward == false
                ;
        }
        private bool _isHappenAlarm = false;
        private void CheckServoStatus(Equipment equip)
        {
            if (_isHappenAlarm)
            {
                if (this.XB_PmacReady == false)
                {
                    AlarmMgr.Instance.Happen(equip, HEAVY_ERROR);
                    _isHappenAlarm = true;
                }
            }
        }
        //ALIVE 처리..
        private DateTime _aliveDateTime = DateTime.Now;
        private bool _pmacPlcAlive = false;
        private int StepTimeOut = 20;
        private int retryConnCount = 0;

        private void CheckAlive(Equipment equip)
        {
            if (_pmacPlcAlive != XB_PmacAlive.vBit)
            {
                _aliveDateTime = DateTime.Now;
                _pmacPlcAlive = XB_PmacAlive.vBit;
            }

            if ((DateTime.Now - _aliveDateTime).TotalSeconds > StepTimeOut )
            {
                DateTime last = _aliveDateTime;
                _aliveDateTime = DateTime.Now.AddSeconds(StepTimeOut);
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0472_PMAC_ALIVE_ERROR);
                Logger.Log.AppendLine(LogLevel.Error, "PMAC ALIVE ERROR 발생함 (마지막 {0})", last);

                Thread connectThread = new Thread(new ThreadStart(ConnectPmac));
                connectThread.Start();
            }
        }

        public void ConnectPmac()

        {
            Logger.Log.AppendLine(LogLevel.Error, "PMAC 재연결 시도");
            GG.PMAC.Open();
        }

        public void StartReset(Equipment equip)
        {
            _isHappenAlarm = false;
        }
        public bool StartCommand(Equipment equip, EmPMacmd cmd, object tag)
        {
            if (LstPMacCmd[(int)cmd].Step != 0)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("UMAC和 {0} 当前命令进行中", cmd.ToString()) : string.Format("UMAC와 {0} 기존 명령이 진행 중", cmd.ToString()));
                Logger.Log.AppendLine(LogLevel.Warning, string.Format("UMAC와 {0} 기존 명령이 진행 중", cmd.ToString()));
                return false;
            }

            if (GG.TestMode == true)
                return true;

            LstPMacCmd[(int)cmd].Step = 10;
            return true;
        }
        public bool IsCommandAck(Equipment equip, EmPMacmd cmd)
        {
            return LstPMacCmd[(int)cmd].Step == 0;
        }

        private int _stepSetting = 0;
        private bool _IsSettingComplete = false;
        private PlcTimer _setDelay = new PlcTimer();
        private DateTime _setStartTime = DateTime.Now;
        public bool StartSaveToPmac(Equipment equip)
        {
            _stepSetting = 10;
            return true;
        }
        public bool IsCompleteSaveToUMac(Equipment equip)
        {
            return _IsSettingComplete && (_stepSetting == 0);
        }
        public bool IsDoneSaveToUMac(Equipment _equip)
        {
            return _stepSetting == 0;
        }
        protected void SettingLogic(Equipment equip)
        {
            if (_stepSetting > 10 && (DateTime.Now - _setStartTime).TotalMilliseconds > 10000)
            {
                Logger.Log.AppendLine(LogLevel.Error, "모터 설정 TIME OVER");
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0478_PMAC_SETTING_TIMEOUT_ERROR);
                _stepSetting = 0;
                return;
            }

            if (_stepSetting == 0)
            {
            }
            else if (_stepSetting == 10)
            {
                Logger.Log.AppendLine(LogLevel.Info, "모터 설정 쓰기 시작");
                _setStartTime = DateTime.Now;
                _IsSettingComplete = false;

                equip.StageX.YF_LoadRate.vInt = equip.StageX.Setting.LoadRate;
                equip.StageY.YF_LoadRate.vInt = equip.StageY.Setting.LoadRate;

                _setDelay.Start(1);
                _stepSetting = 20;
            }
            else if (_stepSetting == 20)
            {
                if (_setDelay)
                {
                    _setDelay.Stop();

                    if (GG.TestMode)
                    {
                        _IsSettingComplete = true;
                        _stepSetting = 0;
                        return;
                    }
                    else
                    {
                        this.YB_LoadRateSaveCmd.vBit = true;
                        _stepSetting = 30;
                    }
                }
            }
            else if (_stepSetting == 30)
            {
                if (this.XB_LoadRateSaveAck.vBit == true)
                {
                    this.YB_LoadRateSaveCmd.vBit = false;
                    _stepSetting = 40;
                }
            }
            else if (_stepSetting == 40)
            {
                if (this.XB_LoadRateSaveAck.vBit == false)
                {
                    _stepSetting = 50;
                }
            }
            else if (_stepSetting == 50)
            {
                if (equip.StageX.XF_LoadRate.vInt == equip.StageX.Setting.LoadRate
                    && equip.StageY.XF_LoadRate.vInt == equip.StageY.Setting.LoadRate)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "모터 설정 쓰기 완료");
                    _IsSettingComplete = true;
                    _stepSetting = 0;
                }
            }
        }

    }
}
