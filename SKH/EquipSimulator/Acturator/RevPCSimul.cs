using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using EquipSimulator.Log;
using Dit.Framework.Log;

namespace EquipSimulator
{
    public class ReviPcCmd
    {
        private int SignalTimeOut = 5;
        public string Name { get; set; }
        
        public PlcAddr YB_CMD { get; set; }
        public PlcAddr XB_CMD_ACK { get; set; }
        public PlcAddr XB_CMD_NACK { get; set; }

        public int Step = 0;
        private DateTime _stepTime = DateTime.Now;
        public Action<ReviPcCmd> OnCommand { get; set; }

        public void LogicWorking()
        {
            if (Step == 0)
            {
                if (YB_CMD.vBit == true)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "제어→리뷰 PC {0} 신호 시작", Name);
                    _stepTime = DateTime.Now;
                    if (OnCommand != null)
                        OnCommand(this);

                    XB_CMD_ACK.vBit = true;
                    Logger.Log.AppendLine(LogLevel.Info, "검사→리뷰 PC {0} 신호 수신 {1}", Name, XB_CMD_ACK.vBit ? "성공" : "실패");
                    Step = 10;
                }
            }
            else if (Step == 10)
            {
                if (YB_CMD.vBit == false)
                {
                    XB_CMD_ACK.vBit = false;
                    Logger.Log.AppendLine(LogLevel.Info, "제어→리뷰 PC {0} 신호 종료", Name);
                    Step = 0;
                }
            }
        }              
    }

    public class ReviPcEvent
    {
        private int SignalTimeOut = 5;
        public int Step = 0;
        private DateTime _stepTime = DateTime.Now;
        public string Name { get; set; }
        public PlcAddr XB_EVENT { get; set; }
        public PlcAddr YB_EVENT_ACK { get; set; }
        public Action<ReviPcEvent>  OnEvent { get; set; }

        public void LogicWorking()
        {
            if (Step == 0)
            {
            }
            else if (Step == 10)
            {
                if (OnEvent != null)
                    OnEvent(this); 

                XB_EVENT.vBit = true;
                _stepTime = DateTime.Now;
                Logger.Log.AppendLine(LogLevel.Info, "검사→리뷰 PC Review Complete Event (true)  신호 수신");

                Step = 15;
            }
            else if (Step == 15)
            {
                if (YB_EVENT_ACK.vBit == true)
                {
                    XB_EVENT.vBit = false;
                    Logger.Log.AppendLine(LogLevel.Info, "제어→리뷰 PC Unloading (true) 신호 송신");
                    YB_EVENT_ACK.vBit = false;

                    Step = 20;
                }
            }
            else if (Step == 20)
            {
                Step = 0;
            }
        }

        internal void StartEvent()
        {
            Step = 10;
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
    public class RevPCSimul
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

        public bool Initialize()
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
        public void OnReviewComplete(ReviPcEvent sender)
        {
            _reviewComplete = true;
        }
        public bool StartCommand( EmReviPcCommand cmd, object tag)
        {            
            LstReviPcCmd[(int)cmd].Step = 10;
            return true;
        }
        public bool IsCommandAck(EmReviPcCommand cmd)
        {
            return LstReviPcCmd[(int)cmd].Step == 0;
        }

        //REVIEW COMPLETE
        private bool _reviewComplete = false;
        public bool IsReviewComplete(EmReviPcEvent cmd)
        {
            return LstReviPcEvent[(int)cmd].Step == 0 && _reviewComplete;
        }

        public void LogicWorking()
        {
            foreach (ReviPcCmd cmd in LstReviPcCmd)
            {
                if (cmd == null) continue;
                cmd.LogicWorking();
            }

            foreach (ReviPcEvent evt in LstReviPcEvent)
            {
                if (evt == null) continue;
                evt.LogicWorking();
            }
        }        
    }
}
