using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipComm.PLC;

namespace EquipMainUi.Struct
{
    public class ReviPcCmd
    {
        private int SignalTimeOut = 30;
        public string Name { get; set; }
        public EM_AL_LST TimeOver { get; set; }

        public PlcAddr YB_CMD { get; set; }
        public PlcAddr XB_CMD_ACK { get; set; }
        public PlcAddr XB_CMD_NACK { get; set; }

        public int Step = 0;
        private DateTime _stepTime = DateTime.Now;

        public void LogicWorking(Equipment equip)
        {
            if (Step > 10 && (DateTime.Now - _stepTime).TotalSeconds > SignalTimeOut)
            {

                Logger.Log.AppendLine(LogLevel.Error, "제어↔리뷰 PC {0} 시그널 TIME OVER", Name);
                AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0230_INSP_PC_EVENT_SIGNAL_TIMEOVER);

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
                Logger.Log.AppendLine(LogLevel.Info, "제어→리뷰 PC {0} 시그널 시작", Name);
                Step = 20;
            }
            else if (Step == 20)
            {
                if (XB_CMD_ACK.vBit || GG.PCTestMode)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "리뷰→제어 PC {0} 시그널 종료 - {1}", Name, XB_CMD_ACK.vBit ? "성공" : "실패");
                    YB_CMD.vBit = false;

                    Step = 0;
                }
            }
        }
    }

    public class ReviPcEvent
    {
        private int SignalTimeOut = 30;
        public int Step = 0;
        private DateTime _stepTime = DateTime.Now;

        public string Name { get; set; }

        public EM_AL_LST TimeOver { get; set; }
        public PlcAddr XB_EVENT { get; set; }
        public PlcAddr YB_EVENT_ACK { get; set; }
        public Action<int> OnEvent { get; set; }

        public void LogicWorking(Equipment equip)
        {
            if (Step > 10 && (DateTime.Now - _stepTime).TotalSeconds > SignalTimeOut)
            {
                Logger.Log.AppendLine(LogLevel.Error, "제어↔리뷰 PC {0} 이벤트 TIME OVER", Name);
                if (GG.PCTestMode == false)
                    AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0230_INSP_PC_EVENT_SIGNAL_TIMEOVER);
                Step = 0;
                return;
            }

            if (Step == 0)
            {
                if (XB_EVENT.vBit)
                {
                    _stepTime = DateTime.Now;
                    Logger.Log.AppendLine(LogLevel.Info, "리뷰→제어 {0} 이벤트 시작", Name);

                    YB_EVENT_ACK.vBit = true;
                    //Logger.Log.AppendLine(LogLevel.Info, "리뷰→제어 {0} 이벤트 응답 신호 송신", Name);
                    Step = 10;
                }
            }
            else if (Step == 10)
            {
                if (XB_EVENT.vBit == false)
                {
                    if (OnEvent != null)
                        OnEvent(0);

                    Logger.Log.AppendLine(LogLevel.Info, "리뷰 →제어 {0} 이벤트 종료", Name);
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
    public enum EmReviPcCommand
    {
        LOADING = 0,
        ALIGN_START,
        REVIEW_START,
        REVIEW_TIME_OVER,
        UNLOADING,
        LOT_START,
        LOT_END,
    }
    public enum EmReviPcEvent
    {
        REVIEW_COMPLETE = 0,
    }
    public class ReviPcProxy
    {
        #region IO 리스트

        public PlcAddr YB_Loading { get; set; }
        public PlcAddr YB_AlignStart { get; set; }
        public PlcAddr YB_ReviewStart { get; set; }
        public PlcAddr YB_ReviewTimeOver { get; set; }
        public PlcAddr YB_Unloading { get; set; }
        public PlcAddr YB_LotStart { get; set; }
        public PlcAddr YB_LotEnd { get; set; }
        public PlcAddr YB_ReviewCompleteAck { get; set; }

        public PlcAddr XB_OnFocus { get; set; }
        public PlcAddr XB_LoadingAck { get; set; }
        public PlcAddr XB_AlignStartAck { get; set; }
        public PlcAddr XB_ReviewStartAck { get; set; }
        public PlcAddr XB_ReviewTimeOverAck { get; set; }
        public PlcAddr XB_UnloadingAck { get; set; }
        public PlcAddr XB_LotStartAck { get; set; }
        public PlcAddr XB_LotEndAck { get; set; }
        public PlcAddr XB_ReviewComplete { get; set; }

        public PlcAddr XB_LoadingFailError { get; set; }
        public PlcAddr XB_AlignFailError { get; set; }
        public PlcAddr XB_ReviewFailError { get; set; }
        public PlcAddr XB_UnloadingFailError { get; set; }
        public PlcAddr YW_GlassData { get; set; }
        #endregion

        public ReviPcCmd[] LstReviPcCmd = new ReviPcCmd[10];
        public ReviPcEvent[] LstReviPcEvent = new ReviPcEvent[10];
        public DateTime ReviStartTime = DateTime.Now;
        public bool Initailize()
        {
            LstReviPcCmd[(int)EmReviPcCommand.LOADING]              /**/ = new ReviPcCmd() { Name = "LOADING",          /**/ YB_CMD = YB_Loading,       /**/ XB_CMD_ACK = XB_LoadingAck };
            LstReviPcCmd[(int)EmReviPcCommand.ALIGN_START]          /**/ = new ReviPcCmd() { Name = "ALIGN_START",      /**/ YB_CMD = YB_AlignStart,    /**/ XB_CMD_ACK = XB_AlignStartAck };
            LstReviPcCmd[(int)EmReviPcCommand.REVIEW_START]         /**/ = new ReviPcCmd() { Name = "REVIEW_START",     /**/ YB_CMD = YB_ReviewStart,   /**/ XB_CMD_ACK = XB_ReviewStartAck };
            LstReviPcCmd[(int)EmReviPcCommand.REVIEW_TIME_OVER]     /**/ = new ReviPcCmd() { Name = "REVIEW_TIME_OVER", /**/ YB_CMD = YB_ReviewTimeOver,/**/ XB_CMD_ACK = XB_ReviewTimeOverAck };
            LstReviPcCmd[(int)EmReviPcCommand.UNLOADING]            /**/ = new ReviPcCmd() { Name = "UNLOADING",        /**/ YB_CMD = YB_Unloading,     /**/ XB_CMD_ACK = XB_UnloadingAck };
            LstReviPcCmd[(int)EmReviPcCommand.LOT_START]            /**/ = new ReviPcCmd() { Name = "LOT_START",        /**/ YB_CMD = YB_LotStart,      /**/ XB_CMD_ACK = XB_LotStartAck };
            LstReviPcCmd[(int)EmReviPcCommand.LOT_END]              /**/ = new ReviPcCmd() { Name = "LOT_END",          /**/ YB_CMD = YB_LotEnd,        /**/ XB_CMD_ACK = XB_LotEndAck };

            LstReviPcEvent[(int)EmReviPcEvent.REVIEW_COMPLETE]      /**/ = new ReviPcEvent() { Name = "REVIEW_COMPLETE",    /**/ XB_EVENT = XB_ReviewComplete,  /**/ YB_EVENT_ACK = YB_ReviewCompleteAck, OnEvent = OnReviewComplete };

            return true;
        }

        public void OnReviewComplete(int tag)
        {
            _reviewComplete = true;
        }
        public bool StartCommand(Equipment equip, EmReviPcCommand cmd, object tag)
        {
            if (LstReviPcCmd[(int)cmd].Step != 0)
            {
                InterLockMgr.AddInterLock(string.Format("인터락<실행중>\n(리뷰 PC와 {0} 기존 명령이 진행 중입니다.)", cmd.ToString()));
                Logger.Log.AppendLine(LogLevel.Warning, string.Format("리뷰 PC와 {0} 기존 명령이 진행 중", cmd.ToString()));
                return false;
            }
            if (cmd == EmReviPcCommand.LOADING)
            {
                ReviStartTime = DateTime.Now;
                _reviewComplete = false;
            } 
            LstReviPcCmd[(int)cmd].Step = 10;
            return true;
        }
        public bool IsCommandAck(Equipment equip, EmReviPcCommand cmd)
        {
            if (GG.TestMode || GG.PCTestMode)
                return true;

            return LstReviPcCmd[(int)cmd].Step == 0;
        }

        //REVIEW COMPLETE
        private bool _reviewComplete = false;
        public bool IsReviewComplete(EmReviPcEvent cmd)
        {
                 if (GG.PCTestMode == true)
                if ((DateTime.Now - ReviStartTime).TotalSeconds > 3) return true;

            return LstReviPcEvent[(int)cmd].Step == 0 && _reviewComplete;
        }

        public void LogicWorking(Equipment equip)
        {
            foreach (ReviPcCmd cmd in LstReviPcCmd)
            {
                if (cmd == null) continue;
                cmd.LogicWorking(equip);
            }

            foreach (ReviPcEvent evt in LstReviPcEvent)
            {
                if (evt == null) continue;
                evt.LogicWorking(equip);
            }

            CheckStatus(equip);
            CheckAlive(equip);
        }
        private void CheckStatus(Equipment equip)
        {
            if (XB_LoadingFailError)      /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0260_LOADING_FAIL);
            if (XB_AlignFailError)        /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0261_ALIGN_FAIL);
            if (XB_ReviewFailError)       /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0262_REVIEW_FAIL);
            if (XB_UnloadingFailError)    /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0263_UNLOADING_FAIL);
        }
         
        //ALIVE 처리..
        private DateTime _aliveDateTime = DateTime.Now;
        private bool _aoiPlcAlive = false;
        private int StepTimeOut = 10;

        private void CheckAlive(Equipment equip)
        {
            //if (CIMAB.PlcAlive.vBit == true)
            //    AOIB.PlcAlive.vBit = true;
            //else
            //    AOIB.PlcAlive.vBit = false;

            //if (_aoiPlcAlive != CIMAB.PlcAlive.vBit)
            //{
            //    _aliveDateTime = DateTime.Now;
            //    _aoiPlcAlive = CIMAB.PlcAlive.vBit;
            //}

            //if ((DateTime.Now - _aliveDateTime).TotalSeconds > StepTimeOut)
            //{
            //    _aliveDateTime = DateTime.Now.AddHours(10);
            //    AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0602_HSMS_PC_ALIVE_ERROR);
            //    Logger.Log.AppendLine(LogLevel.Error, "HSMS ALIVE ERROR 발생함");
            //}
        }
    }
}
