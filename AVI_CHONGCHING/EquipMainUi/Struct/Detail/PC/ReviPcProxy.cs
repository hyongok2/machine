using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Struct.Detail.PC;
using System.Diagnostics;
using Dit.Framework.PLC;
using Dit.Framework.Comm;

namespace EquipMainUi.Struct.Detail
{
    public class ReviPcCmd
    {
        public int HS_OVERTIME { get; set; }
        public string Name { get; set; }
        public EM_AL_LST ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_0000_NONE;
        public EM_AL_LST HS_OVERTIME_ERROR = EM_AL_LST.AL_0000_NONE;
        public EM_AL_LST ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_0000_NONE;

        public PlcAddr YB_CMD { get; set; }
        public PlcAddr XB_CMD_ACK { get; set; }
        public Action<Equipment, ReviPcCmd> OnCommand { get; set; }
        public Action<Equipment, ReviPcCmd> OnResult { get; set; }

        public int Step = 0;
        private DateTime _stepTime = PcDateTime.Now;

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
                        msg = string.Format("CONTROL↔REVIEW PC {0} REQ ACK TIME OVER [STEP : {1}]", Name, Step);
                    }
                    else if (Step == 40)
                    {
                        AlarmMgr.Instance.Happen(module, HS_OVERTIME_ERROR);
                        msg = string.Format("CONTROL↔REVIEW PC {0} REQ HANDSHAKE TIME OVER [STEP : {1}]", Name, Step);
                    }
                    else
                    {
                        //AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_1334_REVI_PC_REQUEST_STEP_ERROR);
                        msg = string.Format("CONTROL↔REVIEW PC {0} REQ STEP ERROR [STEP : {1}]", Name, Step);
                    }
                }
                else
                {
                    if (Step == 30)
                        msg = string.Format("CONTROL↔REVIEW PC {0} REQ ACK TIME OVER [STEP : {1}]", Name, Step);
                    else if (Step == 40)
                        msg = string.Format("CONTROL↔REVIEW PC {0} REQ HANDSHAKE TIME OVER [STEP : {1}]", Name, Step);
                    else
                        msg = string.Format("CONTROL↔REVIEW PC {0} REQ STEP ERROR [STEP : {1}]", Name, Step);

                    InterLockMgr.AddInterLock(msg,
                    string.Format("Step Name : {0}\nError Step : {1}\nCmd(CONTROL→REVIEW) : {2}\nAck/Nack(REVIEW→CONTROL) : {3}",
                    Name, Step, YB_CMD.vBit, XB_CMD_ACK.vBit));
                }

                Logger.RvLog.AppendLine(LogLevel.Error, msg);
                _stepTime = PcDateTime.Now;
                YB_CMD.vBit = false;
                Step = 0;
                return;
            }

            if (Step == 0)
            {
                _stepTime = PcDateTime.Now;
                YB_CMD.vBit = false;
                //if (XB_CMD_ACK.vBit == true)
                    //AlarmMgr.Instance.Happen(module, ACK_IS_NOT_OFF_ERROR);
            }
            else if (Step == 10)
            {
                if (OnCommand != null)
                    OnCommand(module, this);
                Step = 20;
            }
            else if (Step == 20)
            {
                _stepTime = PcDateTime.Now;
                YB_CMD.vBit = true;
                Logger.RvLog.AppendLine(LogLevel.Info, "CONTROL→REVI PC {0} SINGAL START", Name);
                Step = 30;
            }
            else if (Step == 30)
            {
                if (XB_CMD_ACK.vBit || GG.ReviTestMode)
                {
                    Logger.RvLog.AppendLine(LogLevel.Info, "REVI→CONTROL PC {0} ACK RECV-{1}-{2}", Name, XB_CMD_ACK.vBit ? "SUCCESS" : "FAIL", (PcDateTime.Now - _stepTime).TotalMilliseconds);

                    if (OnResult != null)
                        OnResult(module, this);

                    YB_CMD.vBit = false;
                    Step = 40;
                }
            }
            else if (Step == 40)
            {
                if (XB_CMD_ACK.vBit == false || GG.ReviTestMode)
                {
                    Logger.RvLog.AppendLine(LogLevel.Info, "REVI→CONTROL PC {0} SIGNAL TERMINATED-{1}-{2}", Name, XB_CMD_ACK.vBit ? "SUCCESS" : "FAIL", (PcDateTime.Now - _stepTime).TotalMilliseconds);
                    YB_CMD.vBit = false;
                    _stepTime = PcDateTime.Now;
                    Step = 0;
                }
            }
        }
    }
    public class ReviPcEvent
    {
        public int HS_OVERTIME = 30000;
        public int Step = 0;
        private DateTime _stepTime = PcDateTime.Now;

        public string Name { get; set; }

        public EM_AL_LST HS_OVERTIME_ERROR = EM_AL_LST.AL_0000_NONE;

        public PlcAddr XB_EVENT { get; set; }
        public PlcAddr YB_EVENT_ACK { get; set; }
        public Action<Equipment, int> OnEvent { get; set; }

        public void LogicWorking(Equipment module)
        {
            if (Step > 0 && ((PcDateTime.Now - _stepTime).TotalMilliseconds) > HS_OVERTIME)
            {
                string msg = string.Format("CONTROL↔REVIEW PC {0} EVENT TIME OVER", Name);

                if (module.EquipRunMode == EmEquipRunMode.Auto)
                {
                    if (Step == 10)
                        AlarmMgr.Instance.Happen(module, HS_OVERTIME_ERROR);                        
                    //else
                    //    AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_1333_REVIEW_PC_EVENT_STEP_ERROR);
                }
                else
                {
                    InterLockMgr.AddInterLock(msg,
                    string.Format("Step Name : {0}\nError Step : {1}\nEvent(REVIEW→CONTROL) : {2}\nAck(CONTROL→REVIEW) : {3}",
                    Name, Step, XB_EVENT.vBit, YB_EVENT_ACK.vBit));
                }
                _stepTime = PcDateTime.Now;
                Logger.RvLog.AppendLine(LogLevel.Error, msg);
                Step = 0;
                return;
            }

            if (Step == 0)
            {
                YB_EVENT_ACK.vBit = false;
                
                if (XB_EVENT.vBit)
                {
                    _stepTime = PcDateTime.Now;
                    Logger.RvLog.AppendLine(LogLevel.Info, "REVIEW→CONTROL {0} EVENT START", Name);

                    if (OnEvent != null)
                        OnEvent(module, 0);

                    YB_EVENT_ACK.vBit = true;
                    Step = 10;
                    return;
                }
            }
            else if (Step == 10)
            {
                if (XB_EVENT.vBit == false || GG.ReviTestMode)
                {
                    Logger.RvLog.AppendLine(LogLevel.Info, "REVIEW→CONTROL {0} EVENT END", Name);
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
    public enum EmReviPcCommand
    {
        LOADING = 0,
        ALIGN_START,
        RESULT_FILE_READ_REQ,        
        REVIEW_START,
        REVIEW_END,
        AFM_HOME_MOVE,
        WSI_HOME_MOVE,
        DATE_TIME_SET,
        EmReviPcCmdCount
    }
    public enum EmReviPcEvent
    {        
        LOADING_COMPLETE,
        ALIGN_COMPLETE,
        REVIEW_READY,
        REVIEW_COMPLETE,
        RESULT_FILE_CREATE_COMPLETE,
        ALARM_OCCUR,
        EmReviPcEvtCount
    }
    public class ReviPcProxy
    {
        public ReviPcCmd[] LstReviPcCmd = new ReviPcCmd[(int)EmReviPcCommand.EmReviPcCmdCount];
        public ReviPcEvent[] LstReviPcEvent = new ReviPcEvent[(int)EmReviPcEvent.EmReviPcEvtCount];
        public DateTime ReviStartTime = DateTime.Now;

        private bool _isLoadingComplete = false;
        private bool _isAlignComplete = false;
        private bool _isReviewReady = false;
        private bool _isReviewComplete = false;
        private bool _isResultFileCreate = false;

        public bool Initialize(IVirtualMem plc)
        {
            RvAddrB.Initialize(plc);
            //RvAddrW.Initialize(plc);

            ////알람 재정리 필요
            //LstReviPcCmd[(int)EmReviPcCommand.LOADING]                      /**/ = new ReviPcCmd() { Name = "LOADING",                      /**/ ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_1350_REVIEW_LOADING_ACK_TIMEOVER,                /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_1338_REVIEW_LOADING_ACK_HS_TIMEOVER,                 /**/ ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_1380_REVIEW_LOADING_ACK_IS_NOT_OFF_ERROR,               /**/ YB_CMD = RvAddrB.YB_Loading,               /**/ XB_CMD_ACK = RvAddrB.XB_LoadingAck, OnCommand = OnLoadingCmd };
            //LstReviPcCmd[(int)EmReviPcCommand.ALIGN_START]                  /**/ = new ReviPcCmd() { Name = "ALIGN_START",                  /**/ ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_1351_REVIEW_ALIGN_START_ACK_TIMEOVER,            /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_1339_REVIEW_ALIGN_START_ACK_HS_TIMEOVER,             /**/ ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_1381_REVIEW_ALIGN_START_ACK_IS_NOT_OFF_ERROR,           /**/ YB_CMD = RvAddrB.YB_AlignStart,            /**/ XB_CMD_ACK = RvAddrB.XB_AlignStartAck, OnCommand = OnAlignStartCmd };
            //LstReviPcCmd[(int)EmReviPcCommand.RESULT_FILE_READ_REQ]         /**/ = new ReviPcCmd() { Name = "RESULT_FILE_READ_REQ",         /**/ ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_1352_REVIEW_RESULT_FILE_READ_REQ_ACK_TIMEOVER,   /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_1340_REVIEW_RESULT_FILE_READ_REQ_ACK_HS_TIMEOVER,    /**/ ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_1382_REVIEW_RESULT_FILE_READ_REQ_ACK_IS_NOT_OFF_ERROR,  /**/ YB_CMD = RvAddrB.YB_ResultFileReadReq,     /**/ XB_CMD_ACK = RvAddrB.XB_ResultFileReadReqAck };
            //LstReviPcCmd[(int)EmReviPcCommand.REVIEW_START]                 /**/ = new ReviPcCmd() { Name = "REVIEW_START",                 /**/ ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_1353_REVIEW_REVIEW_START_ACK_TIMEOVER,           /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_1341_REVIEW_REVIEW_START_ACK_HS_TIMEOVER,            /**/ ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_1383_REVIEW_REVIEW_START_ACK_IS_NOT_OFF_ERROR,          /**/ YB_CMD = RvAddrB.YB_ReviewStart,           /**/ XB_CMD_ACK = RvAddrB.XB_ReviewStartAck, OnCommand = OnReviewStartCmd };
            //LstReviPcCmd[(int)EmReviPcCommand.REVIEW_END]                   /**/ = new ReviPcCmd() { Name = "REVIEW_END",                   /**/ ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_1354_REVIEW_REVIEW_END_ACK_TIMEOVER,             /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_1342_REVIEW_REVIEW_END_ACK_HS_TIMEOVER,              /**/ ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_1384_REVIEW_REVIEW_END_ACK_IS_NOT_OFF_ERROR,            /**/ YB_CMD = RvAddrB.YB_ReviewEnd,             /**/ XB_CMD_ACK = RvAddrB.XB_ReviewEndAck };
            //LstReviPcCmd[(int)EmReviPcCommand.AFM_HOME_MOVE]                /**/ = new ReviPcCmd() { Name = "AFM_HOME_MOVE",                /**/ ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_1355_REVIEW_AFM_HOME_MOVE_ACK_TIMEOVER,          /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_1343_REVIEW_AFM_HOME_MOVE_HS_TIMEOVER,               /**/ ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_1385_REVIEW_PC_REQ_AFM_HOME_MOVE_ACK_IS_NOT_OFF_ERROR,  /**/ YB_CMD = RvAddrB.YB_AfmHomeMove,           /**/ XB_CMD_ACK = RvAddrB.XB_AfmHomeMoveAck, };
            //LstReviPcCmd[(int)EmReviPcCommand.WSI_HOME_MOVE]                /**/ = new ReviPcCmd() { Name = "WSI_HOME_MOVE",                /**/ ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_1356_REVIEW_WSI_HOME_MOVE_ACK_TIMEOVER,          /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_1344_REVIEW_WSI_HOME_MOVE_HS_TIMEOVER,               /**/ ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_1386_REVIEW_PC_REQ_WSI_HOME_MOVE_ACK_IS_NOT_OFF_ERROR,  /**/ YB_CMD = RvAddrB.YB_WsiHomeMove,           /**/ XB_CMD_ACK = RvAddrB.XB_WsiHomeMoveAck, };
            //LstReviPcCmd[(int)EmReviPcCommand.DATE_TIME_SET]                /**/ = new ReviPcCmd() { Name = "DATE_TIME_SET",                /**/ ACK_WAIT_OVERTIME_ERROR = EM_AL_LST.AL_1357_REVIEW_DATE_TIME_SET_ACK_TIMEOVER,          /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_1345_REVIEW_DATE_TIME_SET_HS_TIMEOVER,               /**/ ACK_IS_NOT_OFF_ERROR = EM_AL_LST.AL_1387_REVIEW_PC_REQ_DATE_TIME_SET_ACK_IS_NOT_OFF_ERROR,  /**/ YB_CMD = RvAddrB.YB_DateTimeSet,           /**/ XB_CMD_ACK = RvAddrB.XB_DateTimeSetAck, OnCommand = OnDateTimeSetCmd };

            //LstReviPcEvent[(int)EmReviPcEvent.LOADING_COMPLETE]             /**/ = new ReviPcEvent() { Name = "LOADING_COMPLETE",           /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_1346_REVIEW_LOADING_COMPLETE_HS_TIMEOVER,              /**/ XB_EVENT = RvAddrB.XB_LoadingComplete,             /**/ YB_EVENT_ACK = RvAddrB.YB_LoadingCompleteAck, OnEvent = OnLoadingCompleteEvt };
            //LstReviPcEvent[(int)EmReviPcEvent.ALIGN_COMPLETE]               /**/ = new ReviPcEvent() { Name = "ALIGN_COMPLETE",             /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_1335_REVIEW_ALIGN_COMPLETE_HS_TIMEOVER,                /**/ XB_EVENT = RvAddrB.XB_AlignComplete,               /**/ YB_EVENT_ACK = RvAddrB.YB_AlignCompleteAck, OnEvent = OnAlignCompleteEvt };
            //LstReviPcEvent[(int)EmReviPcEvent.REVIEW_READY]                 /**/ = new ReviPcEvent() { Name = "REVIEW_READY",               /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_1347_REVIEW_REVIEW_READY_HS_TIMEOVER,                  /**/ XB_EVENT = RvAddrB.XB_ResultFileReadComplete,      /**/ YB_EVENT_ACK = RvAddrB.YB_ReviewReadyAck, OnEvent = OnReadyCompleteEvt };
            //LstReviPcEvent[(int)EmReviPcEvent.REVIEW_COMPLETE]              /**/ = new ReviPcEvent() { Name = "REVIEW_COMPLETE",            /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_1336_REVIEW_REVIEW_COMPLETE_HS_TIMEOVER,               /**/ XB_EVENT = RvAddrB.XB_ReviewComplete,              /**/ YB_EVENT_ACK = RvAddrB.YB_ReviewCompleteAck, OnEvent = OnReviewCompleteEvt };
            //LstReviPcEvent[(int)EmReviPcEvent.RESULT_FILE_CREATE_COMPLETE]  /**/ = new ReviPcEvent() { Name = "RESULT_FILE_CREATE_COMPLETE",/**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_1337_REVIEW_RESULT_FILE_MAKE_COMPLETE_HS_TIMEOVER,     /**/ XB_EVENT = RvAddrB.XB_ResultFileCreateComplete,    /**/ YB_EVENT_ACK = RvAddrB.YB_ResultFileCreateCompleteAck, OnEvent = OnResultFileCreateCompleteEvt };
            //LstReviPcEvent[(int)EmReviPcEvent.ALARM_OCCUR]                  /**/ = new ReviPcEvent() { Name = "ALARM_OCCUR",                /**/ HS_OVERTIME_ERROR = EM_AL_LST.AL_1318_REVIEW_ALARM_OCCUR,                               /**/ XB_EVENT = RvAddrB.XB_AlarmOccur,                  /**/ YB_EVENT_ACK = RvAddrB.YB_AlarmOccurAck, OnEvent = OnAlarmOccurEvt };

            return true;
        }
        public void LogicWorking(Equipment module)
        {
            foreach (ReviPcCmd cmd in LstReviPcCmd)
            {
                if (cmd == null) continue;
                cmd.LogicWorking(module);
            }

            foreach (ReviPcEvent evt in LstReviPcEvent)
            {
                if (evt == null) continue;
                evt.LogicWorking(module);
            }

            ChangeRunnimgMode(module);

            CheckStatus(module);
            //CheckGlassState(module);
            WriteControlAlive(module);
            CheckReviewAlive(module);            
        }

        //SIGNAL
        public bool StartCommand(Equipment module, EmReviPcCommand cmd, object tag)
        {
            if (LstReviPcCmd[(int)cmd].Step != 0)
            {
                InterLockMgr.AddInterLock(string.Format("Interlock <RUNNING> \n (review PC and {0} existing command in progress.)", cmd.ToString()));
                Logger.RvLog.AppendLine(LogLevel.Warning, string.Format("review PC and {0} existing command in progress.", cmd.ToString()));
                return false;
            }
            if (cmd == EmReviPcCommand.LOADING)
            {
                ReviStartTime = DateTime.Now;
                _isReviewComplete = false;
            }
            if (cmd == EmReviPcCommand.REVIEW_START)
            {
                ReviStartTime = DateTime.Now;
                _isReviewComplete = false;
            }

            if (GG.ReviTestMode)
                return true;

            LstReviPcCmd[(int)cmd].Step = 10;
            return true;
        }
        public bool IsCommandAck(Equipment module, EmReviPcCommand cmd)
        {
            if (GG.TestMode || GG.ReviTestMode)
                return true;

            return LstReviPcCmd[(int)cmd].Step == 0;
        }

        public void SettingDelay(int cmdOvertime, int eventOvertime)
        {
            foreach (ReviPcCmd cmd in this.LstReviPcCmd)
            {
                if (cmd != null)
                    cmd.HS_OVERTIME = cmdOvertime;
            }

            foreach (ReviPcEvent evt in this.LstReviPcEvent)
            {
                if (evt != null)
                    evt.HS_OVERTIME = eventOvertime;
            }
        }
        //STATE
        private void CheckStatus(Equipment module)
        {
        }
        private void ChangeRunnimgMode(Equipment module)
        {
            if (module.EquipRunMode == EmEquipRunMode.Auto && module.IsReviewManual == EmReviewManual.ManualOn)
            {
                RvAddrB.YB_ReviewManualMode.vBit = true;
                RvAddrB.YB_AutoMode.vBit = false;
                RvAddrB.YB_ManualMode.vBit = false;
            }
            else if (module.EquipRunMode == EmEquipRunMode.Auto && module.IsReviewManual != EmReviewManual.ManualOn)
            {
                RvAddrB.YB_ReviewManualMode.vBit = false;
                RvAddrB.YB_AutoMode.vBit = true;
                RvAddrB.YB_ManualMode.vBit = false;
            }
            else if (module.EquipRunMode == EmEquipRunMode.Manual && module.IsReviewManual == EmReviewManual.InterLock)
            {
                RvAddrB.YB_ReviewManualMode.vBit = true;
                RvAddrB.YB_AutoMode.vBit = false;
                RvAddrB.YB_ManualMode.vBit = false;
            }
            else if (module.EquipRunMode == EmEquipRunMode.Manual && module.IsReviewManual != EmReviewManual.InterLock)
            {
                RvAddrB.YB_ReviewManualMode.vBit = false;
                RvAddrB.YB_AutoMode.vBit = false;
                RvAddrB.YB_ManualMode.vBit = true;
            }
            else
            {
                //여기 들어오면 버그!! 원인 파악 필요!!
                RvAddrB.YB_ReviewManualMode.vBit = false;
                RvAddrB.YB_AutoMode.vBit = false;
                RvAddrB.YB_ManualMode.vBit = false;
            }
        }
        //private void CheckGlassState(Module module)
        //{
        //    if (module.GlassState == EmGlassSense.ALL)
        //    {
        //        RvAddrB.YB_GlassIn.vBit = true;
        //    }
        //    else
        //    {
        //        RvAddrB.YB_GlassIn.vBit = false;
        //    }
        //}

        //제어 ALIVE 처리
        private DateTime _controlAliveDateTime = PcDateTime.Now;
        private void WriteControlAlive(Equipment module)
        {
            if (Math.Abs((PcDateTime.Now - _controlAliveDateTime).TotalMilliseconds) > 1000)
            {
                _controlAliveDateTime = PcDateTime.Now;
                RvAddrB.YB_ControlAlive.vBit = !RvAddrB.YB_ControlAlive.vBit;
            }
        }

        //REVIEW ALIVE 확인
        private DateTime _reviewAliveDateTime = PcDateTime.Now;
        private bool _reviewAlive = false;
        private int _aliveTimeOver = 10000;
        private void CheckReviewAlive(Equipment module)
        {
            if (_reviewAlive != RvAddrB.XB_ReviewAlive.vBit)
            {
                _reviewAliveDateTime = PcDateTime.Now;
                _reviewAlive = RvAddrB.XB_ReviewAlive.vBit;
            }

            if (Math.Abs((PcDateTime.Now - _reviewAliveDateTime).TotalMilliseconds) > _aliveTimeOver && GG.ReviTestMode == false)
            {
                _reviewAliveDateTime = PcDateTime.Now;

                ////리뷰 알람 재정리 필요
                //if (AlarmMgr.Instance.HappenAlarms[EM_AL_LST.AL_1300_REVIEW_PC_ALIVE_ERROR].Happen == false)
                //    Logger.RvLog.AppendLine(LogLevel.Error, "REVI PC ALIVE ERROR Occurred");
                ////if (AlarmMgr.Instance.HappenAlarms[EM_AL_LST.AL_0550_REVIEW_ALIVE_ERROR].Happen == false)
                ////    Logger.Log.AppendLine(LogLevel.Error, "REVI PC ALIVE ERROR Occurred");

                ////리뷰 알람 재정리 필요
                //AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_1300_REVIEW_PC_ALIVE_ERROR);
                ////AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_0550_REVIEW_ALIVE_ERROR);
            }
        }
        

        //메소드 - 명령 처리
        public void OnLoadingCmd(Equipment module, ReviPcCmd sender)
        {
            
            _isLoadingComplete = false;
            _isAlignComplete = false;
            _isReviewComplete = false;
            _isReviewReady = false;
            _isResultFileCreate = false;
        }
        public void OnAlignStartCmd(Equipment module, ReviPcCmd sender)
        {
            _isAlignComplete = false;
        }
        public void OnReviewStartCmd(Equipment module, ReviPcCmd sender)
        {
            _isReviewComplete = false;
        }
        public void OnDateTimeSetCmd(Equipment module, ReviPcCmd sender)
        {

        }

        //메소드 - 이벤트 처리
        public void OnAlignCompleteEvt(Equipment module, int tag)
        {
            if (RvAddrW.XW_AlignResult.vAscii != "OK")
                //리뷰 알람 재정리 필요
                //AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_1311_REVIEW_ALARM_SIGNAL_ALIGN_FAIL);

            _isAlignComplete = true;
        }
        public void OnLoadingCompleteEvt(Equipment module, int tag)
        {            
            _isLoadingComplete = true;
        }
        public void OnReadyCompleteEvt(Equipment module, int tag)
        {
            _isReviewReady = true;
        }
        public void OnReviewCompleteEvt(Equipment module, int tag)
        {
            if (RvAddrW.XW_ReviewResult.vAscii != "OK")
                //리뷰 알람 재정리 필요
                //AlarmMgr.Instance.Happen(module, EM_AL_LST.AL_1312_REVIEW_ALARM_SIGNAL_REVIEW_FAIL);

            _isReviewComplete = true;
        }
        public void OnResultFileCreateCompleteEvt(Equipment module, int tag)
        {
            if (module.InspPc.IsCommandAck(module, EmInspPcCommand.UNLOADING) == true)
                if (module.InspPc.StartCommand(module, EmInspPcCommand.UNLOADING, 0) == false) return;
            _isResultFileCreate = true;
        }
        public void OnAlarmOccurEvt(Equipment module, int tag)
        {
            Logger.RvLog.AppendLine(LogLevel.Error, "REVIEW ALARM OCCUR : {0}", RvAddrW.XI_AlarmCode.vInt);
        }
        
        //EVENT COMPLET CHECK
        public bool IsEventComplete(EmReviPcEvent evt)
        {
            if (GG.ReviTestMode)
            {
                if (evt == EmReviPcEvent.REVIEW_COMPLETE)
                {
                    if ((DateTime.Now - ReviStartTime).TotalSeconds > 3) return true;
                }
                else
                    return true;
            }

            if (evt == EmReviPcEvent.ALIGN_COMPLETE)
                return _isAlignComplete;
            else if (evt == EmReviPcEvent.LOADING_COMPLETE)
                return _isLoadingComplete;
            else if (evt == EmReviPcEvent.REVIEW_READY)
                return _isReviewReady;
            else if (evt == EmReviPcEvent.REVIEW_COMPLETE)
                return _isReviewComplete;
            else if (evt == EmReviPcEvent.RESULT_FILE_CREATE_COMPLETE)
                return _isResultFileCreate;
            else
                return false;
        }
        
        //기타 함수

        //GLASS DATE WRITE
        private PlcAddr CvPlcAddrToSharAddr(PlcAddr addr)
        {
            if (addr.Bit < 8)
                return new PlcAddr(PlcMemType.S, addr.Addr * 2, addr.Bit, addr.Length * 2, addr.ValueType);
            else
                return new PlcAddr(PlcMemType.S, addr.Addr * 2 + 1, addr.Bit - 8, addr.Length * 2, addr.ValueType);
        }
        public void WriteGlassData(Equipment module, PlcAddr start, GlassInfo g)
        {
        }
    }
}
