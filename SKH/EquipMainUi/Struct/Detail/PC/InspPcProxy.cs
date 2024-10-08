using System;
using System.Text;
using EquipMainUi.Struct.Detail.PC;
using System.Diagnostics;
using Dit.Framework.Comm;
using Dit.Framework.PLC;
using DitCim.PLC;
using EquipMainUi.Struct.TransferData;
using EquipMainUi.RecipeManagement;

namespace EquipMainUi.Struct
{
    public enum EmInspPcCommand
    {
        LOADING = 0,
        ALIGN_START,
        INSP_START,
        //INSP_END,
        REVIEW_START,
        //REVIEW_END,
        UNLOADING,
        //DATE_TIME_SET,
        TTTM_START,
        STATS_TOOL_START, // 검사 반복성 사용할 때 살려주는 신호 (Review End 이후 송신)
    }
    public enum EmInspPcEvent
    { 
        LOADING_COMPLETE,
        ALIGN_COMPLETE,
        INSP_COMPLETE,
        REVIEW_COMPLETE,
        UNLOADING_COMPLETE,
        TTTM_COMPLETE,
    }
    public enum EmVCR
    {
        NONE,
        VCR_READ_START,
        VCR_RESULT_CHECK,
    }
    public class InspPcCmd
    {
        public int HS_OVERTIME { get; set; }
        public string Name { get; set; }
        public EM_AL_LST ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_0000_NONE;
        public EM_AL_LST HS_OVERTIME_ERROR = EM_AL_LST.AL_0000_NONE;
        public EM_AL_LST ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0000_NONE;

        public PlcAddr YB_CMD { get; set; }
        public PlcAddr XB_CMD_ACK { get; set; }
        public bool IsComplete { get; set; }

        public object Tag { get; set; }
        public int Step = 0;
        private DateTime _stepTime = PcDateTime.Now;
        public Action<Equipment, InspPcCmd> OnCommand { get; set; }
        public Action<Equipment, InspPcCmd> OnResult { get; set; }
        public void LogicWorking(Equipment module)
        {
            if (Step > 0 && ((PcDateTime.Now - _stepTime).TotalMilliseconds > HS_OVERTIME))
            {
                string msg;
                if (module.EquipRunMode == EmEquipRunMode.Auto)
                {
                    if (Step == 30)
                    {
                        AlarmMgr.Instance.Happen(module, ACK_WAIT_OVERTIME_ERROR);
                        msg = string.Format("CONTROL↔INSP PC {0} REQ ACK TIME OVER [STEP : {1}]", Name, Step);
                    }
                    else if (Step == 40)
                    {
                        AlarmMgr.Instance.Happen(module, HS_OVERTIME_ERROR);
                        msg = string.Format("CONTROL↔INSP PC {0} REQ HANDSHAKE TIME OVER [STEP : {1}]", Name, Step);
                    }
                    else
                    {
                        AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0330_INSP_PC_REQUEST_STEP_ERROR);
                        msg = string.Format("CONTROL↔INSP PC {0} REQ STEP ERROR [STEP : {1}]", Name, Step);
                    }
                }
                else
                {
                    if (Step == 30)
                        msg = string.Format("CONTROL↔INSP PC {0} REQ ACK TIME OVER [STEP : {1}]", Name, Step);
                    else if (Step == 40)
                        msg = string.Format("CONTROL↔INSP PC {0} REQ HANDSHAKE TIME OVER [STEP : {1}]", Name, Step);
                    else
                        msg = string.Format("CONTROL↔INSP PC {0} REQ STEP ERROR [STEP : {1}]", Name, Step);

                    InterLockMgr.AddInterLock(msg,
                    string.Format("Step Name : {0}\nError Step : {1}\nCmd(CONTROL→INSP) : {2}\nAck/Nack(INSP→CONTROL) : {3}",
                    Name, Step, YB_CMD.vBit, XB_CMD_ACK.vBit));
                }

                Logger.IsptLog.AppendLine(LogLevel.Error, msg);
                _stepTime = PcDateTime.Now;
                YB_CMD.vBit = false;
                Step = 0;
                return;
            }

            if (Step == 0)
            {
                IsComplete = false;
                YB_CMD.vBit = false;
                _stepTime = PcDateTime.Now;
                if (XB_CMD_ACK.vBit == true)
                    AlarmMgr.Instance.Happen(module, ACK_IS_NOT_OFF_ERROR);
            }
            else if (Step == 10)
            {
                if (OnCommand != null)
                    OnCommand(module, this);
                Step = 20;
            }
            else if (Step == 20)
            {
                IsComplete = true;
                _stepTime = PcDateTime.Now;
                YB_CMD.vBit = true;
                Logger.IsptLog.AppendLine(LogLevel.Info, "CONTROL→INSP PC {0} SIGNAL START", Name);
                Step = 30;
            }
            else if (Step == 30)
            {
                if (XB_CMD_ACK.vBit || GG.InspTestMode)
                {
                    Logger.IsptLog.AppendLine(LogLevel.Info, "INSP→CONTROL PC {0} ACK RECV - {1} - {2}", Name, XB_CMD_ACK.vBit ? "Success" : "Fail", (PcDateTime.Now - _stepTime).TotalMilliseconds);
                    if (OnResult != null)
                        OnResult(module, this);

                    YB_CMD.vBit = false;
                    Step = 40;
                }
            }
            else if (Step == 40)
            {
                if (XB_CMD_ACK.vBit == false || GG.InspTestMode)
                {
                    FinishCommand();
                    return;
                }
            }
        }
        public void FinishCommand()
        {
            Logger.IsptLog.AppendLine(LogLevel.Info, "INSP→CONTROL PC {0} SIGNAL END - {1} - {2}", Name, XB_CMD_ACK.vBit ? "Success" : "Fail", (PcDateTime.Now - _stepTime).TotalMilliseconds);
            YB_CMD.vBit = false;
            _stepTime = PcDateTime.Now;
            Step = 0;
        }
    }
    public class InspPcEvent
    {
        public int HS_OVERTIME { get; set; }
        public int Step = 0;
        private DateTime _stepTime = PcDateTime.Now;

        public string Name { get; set; }

        public EM_AL_LST HS_OVERTIME_ERROR = EM_AL_LST.AL_0000_NONE;
        public EM_AL_LST SIGNAL_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0000_NONE;

        public PlcAddr XB_EVENT { get; set; }
        public PlcAddr YB_EVENT_ACK { get; set; }
        public Action<Equipment, InspPcEvent> OnEvent { get; set; }

        public Object Tag { get; set; }

        public void LogicWorking(Equipment module)
        {
            if (Step > 0 && ((PcDateTime.Now - _stepTime).TotalMilliseconds) > HS_OVERTIME)
            {
                string msg = string.Format("CONTROL↔INSP PC {0} EVENT TIME OVER", Name);

                if (module.EquipRunMode == EmEquipRunMode.Auto)
                {
                    if (Step == 10)
                        AlarmMgr.Instance.Happen(module, HS_OVERTIME_ERROR);
                    else
                        AlarmMgr.Instance.Happen(module, HS_OVERTIME_ERROR);
                }
                else
                {
                    InterLockMgr.AddInterLock(msg,
                    string.Format("Step Name : {0}\nError Step : {1}\nEvent(INSP→CONTROL) : {2}\nAck(CONTROL→INSP) : {3}",
                    Name, Step, XB_EVENT.vBit, YB_EVENT_ACK.vBit));
                }
                _stepTime = PcDateTime.Now;
                Logger.IsptLog.AppendLine(LogLevel.Error, msg);
                Step = 0;
                return;
            }

            if (Step == 0)
            {
                YB_EVENT_ACK.vBit = false;

                _stepTime = PcDateTime.Now;

                if (XB_EVENT.vBit)
                {
                    _stepTime = PcDateTime.Now;
                    Logger.IsptLog.AppendLine(LogLevel.Info, "INSP→CONTROL {0} EVENT START", Name);

                    if (OnEvent != null)
                        OnEvent(module, this);

                    YB_EVENT_ACK.vBit = true;
                    Step = 10;
                }
            }
            else if (Step == 10)
            {
                if (XB_EVENT.vBit == false || GG.InspTestMode)
                {
                    Logger.IsptLog.AppendLine(LogLevel.Info, "INSP→CONTROL {0} EVENT END", Name);
                    YB_EVENT_ACK.vBit = false;

                    Step = 20;
                }
            }
            else if (Step == 20)
            {
                _stepTime = PcDateTime.Now;
                Step = 0;
            }
        }
    }
    public class InspPcProxy
    {
        public InspPcCmd[] LstInspPcCmd = new InspPcCmd[Enum.GetValues(typeof(EmInspPcCommand)).Length];
        public InspPcEvent[] LstInspPcEvent = new InspPcEvent[Enum.GetValues(typeof(EmInspPcEvent)).Length];

        public PlcTimerEx TmrLoadingCompleteOverTime        /**/  = new PlcTimerEx("LOADING COMPLETE OVERTIME");
        public PlcTimerEx TmrScanReadyCompleteOverTime      /**/  = new PlcTimerEx("SCAN READY COMPLETE OVERTIME");
        public PlcTimerEx TmrScanStartCompleteOverTime      /**/  = new PlcTimerEx("SCAN START COMPLETE OVERTIME");
        public PlcTimerEx TmrScanEndCompleteOverTime        /**/  = new PlcTimerEx("SCAN END COMPLETE OVERTIME");
        public PlcTimerEx TmrUnloadingCompleteOverTime      /**/  = new PlcTimerEx("UNLOADING COMPLETE OVERTIME");


        private bool _isCreateResultFileComplete;
        private bool _isAlignStartComplete;
        private bool _isLoadingComplete;
        private bool _isScanStartComplete;
        private bool _isReviewStartComplete;
        private bool _isUnloadingComplete;


        public bool Initialize(IVirtualMem plc)
        {
            IsptAddrB.Initialize(plc);
            IsptAddrW.Initialize(plc);

            LstInspPcCmd[(int)EmInspPcCommand.LOADING]                      /**/ = new InspPcCmd() { Name = "LOADING",              /**/ ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_0350_INSP_PC_LOADING_ACK_TIMEOVER,          /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_0369_INSP_PC_LOADING_HS_TIMEOVER,             /**/ ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0380_INSP_PC_LOADING_ACK_IS_NOT_OFF_ERROR,                         /**/ YB_CMD = IsptAddrB.YB_Loading,                 /**/ XB_CMD_ACK = IsptAddrB.XB_LoadingAck,          /**/ OnCommand = OnGlassLoadingCmd, };
            LstInspPcCmd[(int)EmInspPcCommand.ALIGN_START]                  /**/ = new InspPcCmd() { Name = "ALIGN_START",          /**/ ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_0351_INSP_PC_ALIGN_START_ACK_TIMEOVER,      /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_0370_INSP_PC_ALIGN_START_HS_TIMEOVER,         /**/ ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0381_INSP_PC_ALIGN_START_ACK_IS_NOT_OFF_ERROR,                     /**/ YB_CMD = IsptAddrB.YB_AlignStart,              /**/ XB_CMD_ACK = IsptAddrB.XB_AlignStartAck,       /**/ OnCommand = OnAlignStartCmd, };
            LstInspPcCmd[(int)EmInspPcCommand.INSP_START]                   /**/ = new InspPcCmd() { Name = "INSP_START",           /**/ ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_0352_INSP_PC_INSP_START_ACK_TIMEOVER,       /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_0371_INSP_PC_INSP_START_HS_TIMEOVER,          /**/ ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0382_INSP_PC_SCAN_START_ACK_IS_NOT_OFF_ERROR,                      /**/ YB_CMD = IsptAddrB.YB_InspStart,               /**/ XB_CMD_ACK = IsptAddrB.XB_InspStartAck,        /**/ OnCommand = OnInspStartCmd, };
            //LstInspPcCmd[(int)EmInspPcCommand.INSP_END]                   /**/ = new InspPcCmd() { Name = "INSP_END",             /**/ ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_0352_INSP_PC_INSP_START_ACK_TIMEOVER,       /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_0371_INSP_PC_INSP_START_HS_TIMEOVER,          /**/ ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0382_INSP_PC_SCAN_START_ACK_IS_NOT_OFF_ERROR,                      /**/ YB_CMD = IsptAddrB.YB_InspEnd,                 /**/ XB_CMD_ACK = IsptAddrB.XB_InspEndAck,          /**/ OnCommand = OnInspEndCmd, };
            LstInspPcCmd[(int)EmInspPcCommand.REVIEW_START]                 /**/ = new InspPcCmd() { Name = "REVIEW_START",         /**/ ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_0353_INSP_PC_REVIEW_START_ACK_TIMEOVER,     /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_0372_INSP_PC_REVIEW_START_HS_TIME_OVER,       /**/ ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0383_INSP_PC_REVIEW_START_ACK_IS_NOT_OFF_ERROR,                    /**/ YB_CMD = IsptAddrB.YB_ReviewStart,             /**/ XB_CMD_ACK = IsptAddrB.XB_ReviewStartAck,      /**/ OnCommand = OnReviewStartCmd, };
            //LstInspPcCmd[(int)EmInspPcCommand.REVIEW_END]                 /**/ = new InspPcCmd() { Name = "REVIEW_END",           /**/ ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_0353_INSP_PC_REVIEW_START_ACK_TIMEOVER,     /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_0372_INSP_PC_REVIEW_START_HS_TIME_OVER,       /**/ ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0385_INSP_PC_KEY_IN_COMPLETE_ACK_IS_NOT_OFF_ERROR,                 /**/ YB_CMD = IsptAddrB.YB_ReviewEnd,               /**/ XB_CMD_ACK = IsptAddrB.XB_ReviewEndAck,        /**/ OnCommand = OnReviewEndCmd, };
            LstInspPcCmd[(int)EmInspPcCommand.UNLOADING]                    /**/ = new InspPcCmd() { Name = "UNLOADING",            /**/ ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_0356_INSP_PC_UNLOADING_ACK_TIMEOVER,        /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_0375_INSP_PC_UNLOADING_HS_TIMEOVER,           /**/ ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0384_INSP_PC_UNLOADING_ACK_IS_NOT_OFF_ERROR,                       /**/ YB_CMD = IsptAddrB.YB_Unloading,               /**/ XB_CMD_ACK = IsptAddrB.XB_UnloadingAck,        /**/ OnCommand = OnGlassUnloadingCmd };
            LstInspPcCmd[(int)EmInspPcCommand.TTTM_START]                   /**/ = new InspPcCmd() { Name = "TTTM_START",           /**/ ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_0358_INSP_PC_TTTM_START_ACK_TIMEOVER,       /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_0377_INSP_PC_TTTM_START_HS_TIMEOVER,          /**/ ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0396_INSP_PC_EVT_TTTM_START_ACK_IS_NOT_OFF_ERROR,                  /**/ YB_CMD = IsptAddrB.YB_TTTMStart,               /**/ XB_CMD_ACK = IsptAddrB.XB_TTTMStartAck,        /**/ OnCommand = OnTTTMStartCmd };
            LstInspPcCmd[(int)EmInspPcCommand.STATS_TOOL_START]             /**/ = new InspPcCmd() { Name = "STATS_TOOL_START",     /**/ ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_0359_INSP_PC_STATS_TOOL_START_ACK_TIMEOVER, /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_0379_INSP_PC_STATS_TOOL_START_HS_TIMEOVER,    /**/ ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0398_INSP_PC_EVT_STATS_TOOL_START_ACK_IS_NOT_OFF_ERROR,            /**/ YB_CMD = IsptAddrB.YB_StatsToolStart,          /**/ XB_CMD_ACK = IsptAddrB.XB_StatsToolStartAck,   /**/ OnCommand = OnStatsToolStartCmd };


            LstInspPcEvent[(int)EmInspPcEvent.LOADING_COMPLETE]             /**/ = new InspPcEvent() { Name = "LOADING_COMPLETE",   /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_0360_INSP_PC_LOADING_COMPLETE_HS_TIMEOVER,    /**/ SIGNAL_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0388_INSP_PC_LOADING_COMPLETE_IS_NOT_OFF_ERROR,     /**/ XB_EVENT = IsptAddrB.XB_LoadingComplete,                   /**/ YB_EVENT_ACK = IsptAddrB.YB_LoadingCompleteAck,               /**/ OnEvent = OnLoadingComplete };
            LstInspPcEvent[(int)EmInspPcEvent.ALIGN_COMPLETE]               /**/ = new InspPcEvent() { Name = "ALIGN_COMPLETE",     /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_0361_INSP_PC_ALIGN_COMPLETE_HS_TIMEOVER,      /**/ SIGNAL_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0389_INSP_PC_ALIGN_COMPLETE_IS_NOT_OFF_ERROR,       /**/ XB_EVENT = IsptAddrB.XB_AlignComplete,                 /**/ YB_EVENT_ACK = IsptAddrB.YB_AlignCompleteAck,             /**/ OnEvent = OnAlignComplete };
            LstInspPcEvent[(int)EmInspPcEvent.INSP_COMPLETE]                /**/ = new InspPcEvent() { Name = "INSP_COMPLETE",      /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_0362_INSP_PC_INSP_COMPLETE_HS_TIMEOVER,       /**/ SIGNAL_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0390_INSP_PC_INSP_START_COMPLETE_IS_NOT_OFF_ERROR,  /**/ XB_EVENT = IsptAddrB.XB_InspComplete,                 /**/ YB_EVENT_ACK = IsptAddrB.YB_InspCompleteAck,             /**/ OnEvent = OnInspComplete };
            LstInspPcEvent[(int)EmInspPcEvent.REVIEW_COMPLETE]              /**/ = new InspPcEvent() { Name = "REVIEW_COMPLETE",    /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_0363_INSP_PC_REVIEW_COMPLETE_HS_TIMEOVER,     /**/ SIGNAL_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0391_INSP_PC_REVIEW_COMPLETE_IS_NOT_OFF_ERROR,      /**/ XB_EVENT = IsptAddrB.XB_ReviewComplete,                   /**/ YB_EVENT_ACK = IsptAddrB.YB_ReviewCompleteAck,               /**/ OnEvent = OnReviewComplete };
            LstInspPcEvent[(int)EmInspPcEvent.UNLOADING_COMPLETE]           /**/ = new InspPcEvent() { Name = "UNLOADING_COMPLETE", /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_0366_INSP_PC_UNLOADING_COMPLETE_HS_TIMEOVER,  /**/ SIGNAL_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0393_INSP_PC_UNLOADING_COMPLETE_IS_NOT_OFF_ERROR,   /**/ XB_EVENT = IsptAddrB.XB_UnloadingComplete,                 /**/ YB_EVENT_ACK = IsptAddrB.YB_UnloadingCompleteAck,             /**/ OnEvent = OnUnloadingComplete };
            LstInspPcEvent[(int)EmInspPcEvent.TTTM_COMPLETE]                /**/ = new InspPcEvent() { Name = "TTTM_COMPLETE",      /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_0378_INSP_PC_TTTM_COMPLETE_HS_TIMEOVER,       /**/ SIGNAL_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0397_INSP_PC_TTTM_COMPLETE_IS_NOT_OFF_ERROR,        /**/ XB_EVENT = IsptAddrB.XB_TTTMComplete,                 /**/ YB_EVENT_ACK = IsptAddrB.YB_TTTMCompleteAck,             /**/ OnEvent = OnTTTMComplete };

            return true;
        }

        public void SettingDelay(int cmdOvertime, int eventOvertime)
        {
            foreach (InspPcCmd cmd in this.LstInspPcCmd)
            {
                if (cmd != null)
                    cmd.HS_OVERTIME = cmdOvertime;
            }

            foreach (InspPcEvent evt in this.LstInspPcEvent)
            {
                if (evt != null)
                    evt.HS_OVERTIME = eventOvertime;
            }
        }

        public void LogicWorking(Equipment module)
        {
            VCRLogicWorking(module);

            CheckAutoPPIDChangeMode(module);
            WriteZCurrPosition(module);
            CheckCimMode(module);
            CheckVCRMode(module);
            CheckGlassState(module);
            ChangeRunningMode(module);
            CheckMotorInterlock(module);

            foreach (InspPcCmd cmd in LstInspPcCmd)
            {
                if (cmd == null) continue;
                cmd.LogicWorking(module);
            }
            foreach (InspPcEvent evt in LstInspPcEvent)
            {
                if (evt == null) continue;
                evt.LogicWorking(module);
            }
            CheckStatus(module);
            WriteControlAlive(module);
            if (GG.TestMode == false && GG.InspTestMode == false)
                CheckAlive(module);
        }
        public void SetInspectServerMotorInterlockOff(bool interlock)
        {            
            GG.Equip.PMac.YB_InspServerUseCmd.vBit = interlock;

            if (GG.Equip.PMac.XB_InspServerUseAck.vBit == true)
                IsptAddrB.YB_MotorInterlockOffState.vBit = true;
            else
                IsptAddrB.YB_MotorInterlockOffState.vBit = false;
        }
        private void CheckMotorInterlock(Equipment module)
        {            
            if (module.EquipRunMode == EmEquipRunMode.Auto)
            {
                if (module.IsUseMotorInspSever() == true)
                    SetInspectServerMotorInterlockOff(true);
                else
                    SetInspectServerMotorInterlockOff(false);
            }                
            else if (module.EquipRunMode == EmEquipRunMode.Manual)
            {
                if(module.IsPause == true)
                {
                    SetInspectServerMotorInterlockOff(false);
                }
            }
        }

        //SIGNAL
        public bool StartCommand(Equipment module, EmInspPcCommand cmd, object tag)
        {
            if (LstInspPcCmd[(int)cmd].Step != 0)
            {
                InterLockMgr.AddInterLock(string.Format("Interlock <RUNNING> \n (inspection PC and {0} existing command in progress.)", cmd.ToString()));
                Logger.IsptLog.AppendLine(LogLevel.Warning, string.Format("inspection PC and {0} existing command in progress.", cmd.ToString()));
                return false;
            }

            if (GG.InspTestMode)
                return true;

            LstInspPcCmd[(int)cmd].Tag = tag;
            LstInspPcCmd[(int)cmd].Step = 10;
            return true;
        }
        public bool IsCommandAck(Equipment module, EmInspPcCommand cmd)
        {
            if (GG.TestMode || GG.InspTestMode)
                return true;

            return LstInspPcCmd[(int)cmd].Step == 0;
        }

        //STATE        
        private void CheckAutoPPIDChangeMode(Equipment module)
        {
            if (module.CtrlSetting.AutoRecipeChange == 1 && IsptAddrB.YB_AutoRecipeChange.vBit == false)
            {
                IsptAddrB.YB_AutoRecipeChange.vBit = true;
            }
            else if (module.CtrlSetting.AutoRecipeChange == 0 && IsptAddrB.YB_AutoRecipeChange.vBit == true)
            {
                IsptAddrB.YB_AutoRecipeChange.vBit = false;
            }
        }
        private void CheckCimMode(Equipment equip)
        {
            //jys::todo
            //if (equip.HsmsPc.CimOnOff == EmCimOnOff.CimOn)
            //{
            //    IsptAddrB.YB_CimMode.vBit = true;
            //}
            //else if (equip.HsmsPc.CimOnOff == EmCimOnOff.CimOff)
            //{
            //    IsptAddrB.YB_CimMode.vBit = false;
            //}
        }
        private void WriteZCurrPosition(Equipment module)
        {
            //IsptAddrW.YF_InspZ1CurrPosition.vFloat = module.InspZ1.XF_CurrMotorPosition.vFloat;
            //IsptAddrW.YF_InspZ2CurrPosition.vFloat = module.InspZ2.XF_CurrMotorPosition.vFloat;
            //IsptAddrW.YF_InspZ3CurrPosition.vFloat = module.InspZ3.XF_CurrMotorPosition.vFloat;
            //IsptAddrW.YF_InspZ4CurrPosition.vFloat = module.InspZ4.XF_CurrMotorPosition.vFloat;
            //IsptAddrW.YF_InspZ5CurrPosition.vFloat = module.InspZ5.XF_CurrMotorPosition.vFloat;
            //IsptAddrW.YF_InspZ6CurrPosition.vFloat = module.InspZ6.XF_CurrMotorPosition.vFloat;
        }
        public void CheckVCRMode(Equipment module)
        {
           
        }
        private void CheckStatus(Equipment module)
        {
            //if (IsptAddrB.XB_PPIDErrAlarm.vBit)                     /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0319_INSPECTION_ALARM_SIGNAL_PPID_ERROR);
            if (IsptAddrB.XB_FtpIsNotConnectAlarm)                  /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0303_INSPECTION_ALARM_FTP_SERVER_NOT_CONNECTED_ALARM);

            if (IsptAddrB.XB_TTTMLightAlarm)                        /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0304_INSPECTION_ALARM_TTTM_LIGHT_VALUE_ALARM);
            if (IsptAddrB.XB_TTTMFocusAlarm)                        /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0305_INSPECTION_ALARM_TTTM_FOCUS_VALUE_ALARM);
            if (IsptAddrB.XB_TTTMMatchAlarm)                        /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0306_INSPECTION_ALARM_TTTM_SIMILARITY_VALUE_ALARM);
            if (IsptAddrB.XB_TTTMFailAlarm)                         /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0307_INSPECTION_ALARM_TTTM_FAIL_ALARM);
            if (IsptAddrB.XB_WaferNGWarningAlarm.vBit)              /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0308_INSPECTION_ALARM_SIGNAL_WAFER_NG_WARNING_ALARM);

            if (IsptAddrB.XB_GrabFailAlarm.vBit)                   /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0317_INSPECTION_ALARM_SIGNAL_GRAB_FAIL);
            if (IsptAddrB.XB_FtpUploadFailAlarm.vBit)               /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0316_INSPECTION_ALARM_SIGNAL_FTP_UPLOAD_FAIL);

            if (IsptAddrB.XB_ModulePcAlarm.vBit)                    /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0321_INSPECTION_ALARM_SIGNAL_MODULE_PC);
            if (IsptAddrB.XB_AlignAlarm.vBit)                       /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0311_INSPECTION_ALARM_SIGNAL_WAFER_ALIGN_FAIL);
            if (IsptAddrB.XB_ServerOverflowAlarm.vBit)              /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0312_INSPECTION_ALARM_SIGNAL_SERVER_OVERFLOW);
            if (IsptAddrB.XB_InspectorOverflowAlarm.vBit)           /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0313_INSPECTION_ALARM_SIGNAL_INSPECT_OVERFLOW);
            
            if (IsptAddrB.XB_LightValueAlarm.vBit)                  /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0320_INSPECTION_ALARM_SIGNAL_LIGHT_ERROR);            
            if (IsptAddrB.XB_PtOverSizeAlarm.vBit)                  /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0322_INSPECTION_ALARM_SIGNAL_NO_MASTER_IMAGE_ALARM);
            
            if (IsptAddrB.XB_ReviewScheduleFailAlarm.vBit)          /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0323_INSPECTION_ALARM_SIGNAL_REVIEW_SCHEDULE_FAIL);
            if (IsptAddrB.XB_ReviewSequenceFailAlarm.vBit)          /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0324_INSPECTION_ALARM_SIGNAL_REVIEW_SEQUENCE_FAIL);
            if (IsptAddrB.XB_ReviewProcessFailAlarm.vBit)           /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0325_INAPECTION_ALARM_SIGNAL_REVIEW_PROCESS_FAIL);
            //if (IsptAddrB.XB_AfmErrorAlarm.vBit)                    /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0326_INSPECTION_ALARM_SIGNAL_AFM_ERROR);
            
            if (IsptAddrB.XB_DieAlignFailAlarm.vBit)                /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0327_INSPECTION_ALARM_SIGNAL_DIE_ALIGN_FAIL);
            if (IsptAddrB.XB_DieAlignWarningAlarm.vBit)             /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0326_INSPECTION_ALARM_SIGNAL_DIE_ALIGN_WARNING);
            
            if (IsptAddrB.XB_RecipeReadFail.vBit)                   /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0314_INSPECTION_ALARM_SIGNAL_RECIPE_READ_FAIL);
            if (IsptAddrB.XB_RecipeRoiError.vBit)                   /**/AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0315_INSPECTION_ALARM_SIGNAL_RECIPE_ROI_ERROR);

        }
        private void CheckGlassState(Equipment equip)
        {
            if (equip.IsWaferDetect != EmGlassDetect.NOT)
            {
                IsptAddrB.YB_GlassIn.vBit = true;
            }
            else
            {
                IsptAddrB.YB_GlassIn.vBit = false;
            }
        }
        private void ChangeRunningMode(Equipment equip)
        {
            if (equip.EquipRunMode == EmEquipRunMode.Auto)
            {
                IsptAddrB.YB_AutoMode.vBit = true;
            }
            else
            {
                IsptAddrB.YB_AutoMode.vBit = false;
            }

            if (equip.EquipRunMode == EmEquipRunMode.Auto)
            {
                IsptAddrB.YB_ReviewManualMode.vBit = equip.IsReviewManual == EmReviewManual.ManualOn;
            }
            else if (equip.EquipRunMode == EmEquipRunMode.Manual)
            {
                IsptAddrB.YB_ReviewManualMode.vBit = equip.IsReviewManual == EmReviewManual.InterLock;
            }

            SetReviewMode(equip.IsReviewSkip == EmReviewSkip.None ? true : false);
        }
        public void SetReviewMode(bool runReview)
        {
            IsptAddrB.YB_ReviewMode.vBit = runReview;
        }
        //제어 ALIVE 처리
        private DateTime _controlAliveDateTime = PcDateTime.Now;
        private void WriteControlAlive(Equipment module)
        {
            if ((PcDateTime.Now - _controlAliveDateTime).TotalMilliseconds > 1000)
            {
                _controlAliveDateTime = PcDateTime.Now;
                IsptAddrB.YB_ControlAlive.vBit = !IsptAddrB.YB_ControlAlive.vBit;
            }
        }
        //검사 ALIVE 처리..
        private DateTime _inspAliveDateTime = PcDateTime.Now;
        private bool _aoiPlcAlive = false;
        private int _aliveTimeOver = 10000;
        private void CheckAlive(Equipment module)
        {
            if (_aoiPlcAlive != IsptAddrB.XB_ServerAlive.vBit)
            {
                _inspAliveDateTime = PcDateTime.Now;
                _aoiPlcAlive = IsptAddrB.XB_ServerAlive.vBit;
            }

            if (Math.Abs((PcDateTime.Now - _inspAliveDateTime).TotalMilliseconds) > _aliveTimeOver)
            {
                _inspAliveDateTime = PcDateTime.Now;

                if (AlarmMgr.Instance.HappenAlarms[EM_AL_LST.AL_0300_INSP_PC_ALIVE_ERROR].Happen == false)
                    Logger.IsptLog.AppendLine(LogLevel.Error, "INSP PC ALIVE ERROR Occurred");                

                AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0300_INSP_PC_ALIVE_ERROR);
            }
        }

        //EVENT COMPLETE CHECK
        public bool IsEventComplete(EmInspPcEvent evt)
        {
            if (GG.InspTestMode) return true;

            //else if (evt == EmInspPcEvent.CREATE_RESULT_FILE_COMPLETE)
            //    return _isCreateResultFileComplete;
            else if (evt == EmInspPcEvent.LOADING_COMPLETE)
                return _isLoadingComplete;
            else if (evt == EmInspPcEvent.ALIGN_COMPLETE)
                return _isAlignComplete;
            else if (evt == EmInspPcEvent.INSP_COMPLETE)
                return _isInspComplete;
            else if (evt == EmInspPcEvent.REVIEW_COMPLETE)
                return _isReviewComplete;
            else if (evt == EmInspPcEvent.UNLOADING_COMPLETE)
                return _isUnloadingComplete;
            else if (evt == EmInspPcEvent.TTTM_COMPLETE)
                return _isTTTMComplete;
            else
                return false;
        }
        //기타 함수
        
        //메소드 - 명령 처리..
        public void OnGlassLoadingCmd(Equipment module, InspPcCmd cmd)
        {
            _isLoadingComplete = false;            
            _isCreateResultFileComplete = false;

            try
            {
                WaferInfoKey key = cmd.Tag as WaferInfoKey;
                WaferInfo w = TransferDataMgr.GetWafer(key);

                WriteWaferData(module, IsptAddrW.YW_GlassData, ref w);
                WriteAlignData(module, ref w);
            }
            catch(Exception ex)
            {
                Logger.ExceptionLog.AppendLine(LogLevel.Error, "Ctrl->Insp Loading Wafer Info Fail {0}", ex.Message);
                AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0729_WAFER_INFO_ABNORMAL);
            }
        }

        private void WriteAlignData(Equipment module, ref WaferInfo w)
        {
            IsptAddrW.YF_NotchX.vFloat           = (float)w.WaferNotchPosX;
            IsptAddrW.YF_NotchY.vFloat           = (float)w.WaferNotchPosY;
            IsptAddrW.YF_NotchT.vFloat           = (float)w.WaferNotchPosT;
            IsptAddrW.YF_OffsetX.vFloat            = (float)w.OffsetX;
            IsptAddrW.YF_OffsetY.vFloat            = (float)w.OffsetY;
            IsptAddrW.YF_OffsetT.vFloat            = (float)w.OffsetT;
            IsptAddrW.YF_EllipseX.vFloat         = (float)w.EllipseX;
            IsptAddrW.YF_EllipseY.vFloat         = (float)w.EllipseY;
            IsptAddrW.YF_MajorLength.vFloat      = (float)w.MajorLength;
            IsptAddrW.YF_MinorLength.vFloat      = (float)w.MinorLength;
            IsptAddrW.YF_EllipseT.vFloat         = (float)w.EllipseT;
            IsptAddrW.YF_EllipseMajorX1.vFloat   = (float)w.EllipseMajorX1;
            IsptAddrW.YF_EllipseMajorY1.vFloat   = (float)w.EllipseMajorY1;
            IsptAddrW.YF_EllipseMajorX2.vFloat   = (float)w.EllipseMajorX2;
            IsptAddrW.YF_EllipseMajorY2.vFloat   = (float)w.EllipseMajorY2;
            IsptAddrW.YF_EllipseMinorX1.vFloat   = (float)w.EllipseMinorX1;
            IsptAddrW.YF_EllipseMinorY1.vFloat   = (float)w.EllipseMinorY1;
            IsptAddrW.YF_EllipseMinorX2.vFloat   = (float)w.EllipseMinorX2;
            IsptAddrW.YF_EllipseMinorY2.vFloat   = (float)w.EllipseMinorY2;
        }

        public void OnAlignStartCmd(Equipment module, InspPcCmd cmd)
        {
            //short index = 0;
            //short.TryParse(cmd.Tag.ToString(), out index);
            //IsptAddrW.YW_ScanIndex.vShort = index;
            _isAlignComplete = false;
            _isAlignStartComplete = false;
        }
        public void OnInspStartCmd(Equipment module, InspPcCmd cmd)
        {
            _isInspComplete = false;
            _isInspStartComplete = false;
        }
        public void OnInspEndCmd(Equipment module, InspPcCmd cmd)
        {
            _isInspStartComplete = false;
        }
        public void OnReviewStartCmd(Equipment module, InspPcCmd cmd)
        {
            _isReviewComplete = false;
            _isReviewStartComplete = false;
        }
        public void OnReviewEndCmd(Equipment module, InspPcCmd cmd)
        {
            _isReviewStartComplete = false;
        }
        public void OnGlassUnloadingCmd(Equipment module, InspPcCmd cmd)
        {
            _isUnloadingComplete = false;
        }
        public void OnVcrReadCompleteCmd(Equipment module, InspPcCmd sender)
        {
        }
        public void OnVcrKeyInCompleteCmd(Equipment module, InspPcCmd sender)
        {
        }
        public void OnDateTimeSetCmd(Equipment module, InspPcCmd sender)
        {
            StringBuilder sb = new StringBuilder();
            //short year = (short)(GG.HSMS.VirGetShort(CIMAW.TimeSet_yy));
            //year = (short)(year < 2000 ? year + 2000 : year);            
            //sb.Append(year);
            //sb.Append((short)(GG.HSMS.VirGetShort(CIMAW.TimeSet_MM)));
            //sb.Append((short)(GG.HSMS.VirGetShort(CIMAW.TimeSet_dd)));
            //sb.Append((short)(GG.HSMS.VirGetShort(CIMAW.TimeSet_HH)));
            //sb.Append((short)(GG.HSMS.VirGetShort(CIMAW.TimeSet_mm)));
            //sb.Append((short)(GG.HSMS.VirGetShort(CIMAW.TimeSet_ss)));

            GG.MEM_DIT.VirSetAscii(IsptAddrW.YW_TimeSync, sb.ToString());
        }
        private void OnTTTMStartCmd(Equipment module, InspPcCmd asender)
        {
            _isTTTMComplete = false;
        }

        private void OnStatsToolStartCmd(Equipment module, InspPcCmd asender)
        {
            GG.MEM_DIT.VirSetShort(IsptAddrW.YW_StatusResultCount, (short)(GG.Equip.CtrlSetting.InspRepeatCount + 1));

            _isStatToolCmplete = false;
        }

        //메소드 - 이벤트 처리..
        public void OnLoadingComplete(Equipment module, InspPcEvent sender)
        {
            _isLoadingComplete = true;
        }
        public void OnAlignComplete(Equipment module, InspPcEvent sender)
        {
            _isAlignComplete = true;
        }
        public void OnInspComplete(Equipment module, InspPcEvent sender)
        {
            _isInspComplete = true;
        }
        public void OnReviewComplete(Equipment module, InspPcEvent sender)
        {
            _isReviewComplete = true;

        }
        public void OnUnloadingComplete(Equipment module, InspPcEvent sender)
        {
            float[] kerfData = new float[30];
            int i = 0;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH1_1.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH1_2.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH1_3.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH1_4.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH1_5.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH1_6.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH1_7.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH1_8.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH1_9.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH1_10.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH1_11.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH1_12.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH1_13.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH1_14.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH1_15.vFloat;
            
            kerfData[i++] = IsptAddrW.XF_KerfDataCH2_1.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH2_2.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH2_3.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH2_4.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH2_5.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH2_6.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH2_7.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH2_8.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH2_9.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH2_10.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH2_11.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH2_12.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH2_13.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH2_14.vFloat;
            kerfData[i++] = IsptAddrW.XF_KerfDataCH2_15.vFloat;

            string kerfLog = string.Empty;
            kerfLog += "\n";
            for (int j = 0; i < kerfData.Length; i++)
            {
                kerfLog += kerfLog[j].ToString() + ", ";
                if (i == 14)
                    kerfLog += "\n";
            }
            Logger.IsptLog.AppendLine(LogLevel.Info, "kerf Data Read : {0}", kerfLog);


            GG.Equip.UpdateKerfData(kerfData);

            _isUnloadingComplete = true;
        }
        public void OnTTTMComplete(Equipment module, InspPcEvent sender)
        {
            _isTTTMComplete = true;
        }

        public void OnVcrReadComplete(Equipment module, InspPcEvent sender)
        {
            _isCreateResultFileComplete = true;
        }
        public void OnCreateResultFileComplete(Equipment module, InspPcEvent sender)
        {
            //module.LoadingGlassInfo.JobGradeCode = IsptAddrW.XW_GradeCode.vShort;
            //module.LoadingGlassInfo.JobJudgeCode = IsptAddrW.XW_JudgeCode.vShort;            
            _isCreateResultFileComplete = true;
            //if (module.ReviPc.IsCommandAck(module, EmReviPcCommand.RESULT_FILE_READ_REQ) == true)
            //    if (module.ReviPc.StartCommand(module, EmReviPcCommand.RESULT_FILE_READ_REQ, 0) == false) return;
        }
        public void OnZAxisDown(Equipment module, InspPcEvent sender)
        {
            //module.InspZ.SetPosition();
        }
        public void OnRecipeChange(Equipment module, InspPcEvent sender)
        {
            //if (IsptAddrW.XI_RecipeChangeCode.vShort == 1 || IsptAddrW.XI_RecipeChangeCode.vShort == 2 || IsptAddrW.XI_RecipeChangeCode.vShort == 3)
            //{
            //    module.HsmsPc.StartCommand(module, EmHsmsPcCommand.RECIPE_CHANGE_REQ, 0);
            //}
            //else if (IsptAddrW.XI_RecipeChangeCode.vShort == 4)
            //{
            //    RmmManager rmmMgr = new RmmManager();
            //    rmmMgr.LoadFromFile(module.CtrlSetting.InspRmmInfoFilePath);
            //    module.RmmMgr = rmmMgr;

            //    short currRecipeID = 0;
            //    short.TryParse(module.RmmMgr.ActionEqpPPID, out currRecipeID);

            //    //module.GetIsptModuleInstan().HsmsPc.StartCommand(module, EmHsmsPcCommand.CURRENT_RECIPE_NUMBER_CHANGE_REPORT, 0);
            //}
        }

        //GLASS DATE WRITE
        private PlcAddr CvPlcAddrToSharAddr(PlcAddr addr)
        {
            return new PlcAddr(PlcMemType.S, addr.Addr * 2, 0, addr.Length * 2, addr.ValueType);
        }
        public void WriteWaferData(Equipment module, PlcAddr start, ref WaferInfo w)
        {
            CassetteInfo cst = TransferDataMgr.GetCst(w.CstID);

            #region WRITE DATA

            GG.MEM_DIT.VirSetAscii(IsptAddrW.CST_ID_POS     /**/ + start, w.CstID);
            //GG.MEM_DIT.VirSetAscii(IsptAddrW.RF_READ_CST_ID_POS         /**/ + start, g.CstID);
            GG.MEM_DIT.VirSetAscii(IsptAddrW.WAFER_ID_POS   /**/ + start, w.WaferID);
            //GG.MEM_DIT.VirSetAscii(IsptAddrW.BARCODE_READ_ID_POS        /**/ + start, g.BCRID1);
            GG.MEM_DIT.VirSetAscii(IsptAddrW.RECIPE_ID_POS  /**/ + start, cst == null ? (GG.RecipeEx ? GG.Equip.LPM1Recipe : "RCP_NULL") : cst.RecipeName);
            if(GG.Equip.CimMode == EmCimMode.Remote)
            {
                GG.MEM_DIT.VirSetAscii(IsptAddrW.LOT_ID_POS     /**/ + start, w.LotID);
                GG.MEM_DIT.VirSetAscii(IsptAddrW.OPER_ID_POS    /**/ + start, GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_CST_MAP_OPER));
                GG.MEM_DIT.VirSetAscii(IsptAddrW.ID_TYPE_POS    /**/ + start, ((int)w.IDType).ToString());
                GG.MEM_DIT.VirSetAscii(IsptAddrW.PROCESS_POS    /**/ + start, w.ProcessJob);
                GG.MEM_DIT.VirSetShort(IsptAddrW.COLUMNS_POS    /**/ + start, (short)w.CellColCount);
                GG.MEM_DIT.VirSetShort(IsptAddrW.ROWS_POS       /**/ + start, (short)w.CellRowCount);
                GG.MEM_DIT.VirSetAscii(IsptAddrW.FLAT_ZONE_POS  /**/ + start, w.FloatZone);
            }
            else
            {
                GG.MEM_DIT.VirSetAscii(IsptAddrW.LOT_ID_POS     /**/ + start, string.Empty);
                GG.MEM_DIT.VirSetAscii(IsptAddrW.OPER_ID_POS    /**/ + start, string.Empty);
                GG.MEM_DIT.VirSetAscii(IsptAddrW.ID_TYPE_POS    /**/ + start, string.Empty);
                GG.MEM_DIT.VirSetAscii(IsptAddrW.PROCESS_POS    /**/ + start, string.Empty);
                GG.MEM_DIT.VirSetShort(IsptAddrW.COLUMNS_POS    /**/ + start, 0);
                GG.MEM_DIT.VirSetShort(IsptAddrW.ROWS_POS       /**/ + start, 0);
                GG.MEM_DIT.VirSetAscii(IsptAddrW.FLAT_ZONE_POS  /**/ + start, string.Empty);
            }
            #endregion
        }

        //VCR LOGIC
        private int _stepVcrRead = 0;
        private Stopwatch _stepTime = new Stopwatch();
        private EmVCR _vcrFlag = EmVCR.VCR_READ_START;
        public bool KeyIn = false;
        public string KeyIntString = string.Empty;

        public void VCRLogicWorking(Equipment module)
        {
            //if (_stepVcrRead == 0)
            //{
            //
            //}
            //else if (_stepVcrRead == 10)
            //{
            //    IsptAddrB.YB_VcrReadComplete.vBit = true;
            //    _stepVcrRead = 20;
            //}
            //else if (_stepVcrRead == 20)
            //{
            //    if (IsptAddrB.XB_VcrReadOk || IsptAddrB.XB_VcrReadFailAlarm || IsptAddrB.XB_VcrReadNg)
            //    {
            //        IsptAddrB.YB_VcrReadComplete.vBit = false;
            //        if (IsptAddrB.XB_VcrReadOk)
            //        {
            //            module.LoadingGlassInfo.VCRReadResult = 1;
            //        }
            //        else
            //        {
            //            module.LoadingGlassInfo.VCRReadResult = 0;
            //        }
            //
            //        module.LoadingGlassInfo.VCRReadGlassID = IsptAddrW.XW_VcrReadGlassID.vAscii;
            //
            //        _stepVcrRead = 40;
            //    }
            //    else if (IsptAddrB.XB_VcrReadMissmatchAlarm)
            //    {
            //        module.LoadingGlassInfo.VCRReadGlassID = IsptAddrW.XW_VcrReadGlassID.vAscii;
            //
            //        KeyIn = false;
            //        module.GlassIDKeyInStart = false;
            //        module.GlassIDKeyInEnd = false;
            //
            //        module.GlassReqStart = false;
            //        module.GlassReqEnd = false;
            //
            //        module.LoadingGlassInfo.VCRReadResult = 0;
            //
            //        IsptAddrB.YB_VcrReadComplete.vBit = false;
            //        if (module.CtrlSetting.VCRKeyInMode == 1)
            //        {
            //            module.GlassIDKeyInStart = true;
            //            _stepVcrRead = 30;
            //        }
            //        else if (_vcrFlag == EmVCR.VCR_RESULT_CHECK)
            //        {
            //            module.GlassReqStart = true;
            //            _stepVcrRead = 30;
            //        }
            //        else
            //        {
            //            _stepVcrRead = 30;
            //        }
            //    }
            //}
            //else if (_stepVcrRead == 30)
            //{
            //    if (module.CtrlSetting.VCRKeyInMode == 1)
            //    {
            //        if (module.GlassIDKeyInEnd == true)
            //        {
            //            KeyIn = true;
            //            IsptAddrW.YW_VcrKeyInGlassID.vAscii = module.GlassIDKeyIn;
            //            module.LoadingGlassInfo.KeyInGlassID = module.GlassIDKeyIn;
            //
            //            Logger.Log.AppendLine(LogLevel.Info, "VCR Glass ID(Key In) : {0}, VCR OK :{1}", module.LoadingGlassInfo.KeyInGlassID, module.LoadingGlassInfo.VCRReadResult == 1 ? "OK" : "Fail");
            //
            //            if (module.InspPc.StartCommand(module, EmInspPcCommand.VCR_KEY_IN_COMPLETE, 0) == false) return;
            //            _stepVcrRead = 40;
            //        }
            //    }
            //    else if (_vcrFlag == EmVCR.VCR_RESULT_CHECK)
            //    {
            //        if (module.GlassReqEnd == true && module.HsmsPc.LstHsmsPcCmd[(int)EmHsmsPcCommand.JOB_DATA_REQUEST].Step == 0)
            //        {
            //            Logger.Log.AppendLine(LogLevel.Info, "VCR Job Request Glass ID(Reading) : {0}, VCR OK :{1}", module.LoadingGlassInfo.VCRReadGlassID, module.LoadingGlassInfo.VCRReadResult == 1 ? "OK" : "Fail");
            //        }
            //        _stepVcrRead = 40;
            //    }
            //    else
            //    {
            //        KeyIntString = string.Empty;
            //
            //        Logger.Log.AppendLine(LogLevel.Info, "VCR Glass ID(Reading) : {0}, VCR OK :{1}", module.LoadingGlassInfo.VCRReadGlassID, module.LoadingGlassInfo.VCRReadResult == 1 ? "OK" : "Fail");
            //
            //        _stepVcrRead = 40;
            //    }
            //}
            //else if (_stepVcrRead == 40)
            //{
            //    module.HsmsPc.StartCommand(module, EmHsmsPcCommand.VCR_EVENT_REPORT, (object)module.LoadingGlassInfo);
            //    _stepVcrRead = 0;
            //}
        }
        public bool VcrReadStart(Equipment module, EmVCR vcrFlag = EmVCR.VCR_READ_START)
        {
            //if (GG.TestMode == true || GG.InspTestMode || module.CtrlSetting.VCRStatus == 3 ||
            //    vcrFlag == EmVCR.NONE)
            //    return true;

            //if (_stepVcrRead != 0)
            //{
            //    return false;
            //}

            //_vcrFlag = vcrFlag;

            //if (vcrFlag == EmVCR.VCR_RESULT_CHECK)
            //{
            //    _stepVcrRead = 20;
            //}
            //else
            //{
            //    _stepVcrRead = 10;
            //}

            return true;
        }
        public bool IsVcrReadComplete(Equipment module, EmVCR vcrFlag = EmVCR.VCR_READ_START)
        {
            // vcr 관련 재정리 필요
            //if (GG.TestMode || GG.InspTestMode || module.CtrlSetting.VCRStatus == 3 || vcrFlag == EmVCR.NONE)
            if (GG.TestMode || GG.InspTestMode)
                return true;

            return _stepVcrRead == 0;
        }

        internal double GetZ0Position(int axisIdx, int p)
        {
            throw new NotImplementedException();
        }

        internal string GetGlassJudgement()
        {
            throw new NotImplementedException();
        }

        public bool _isInspStartComplete { get; set; }

        public bool _isAlignComplete { get; set; }

        public bool _isInspComplete { get; set; }

        public bool _isReviewComplete { get; set; }

        public bool _isTTTMComplete { get; set; }

        public bool _isStatToolCmplete { get; set; }
    }
}