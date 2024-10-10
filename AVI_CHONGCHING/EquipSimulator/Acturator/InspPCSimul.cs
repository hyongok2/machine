using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using EquipSimulator.Log;
using Dit.Framework.Log;

namespace EquipSimulator
{
    public enum EmInspPcCommand
    {
        LOADING = 0,
        SCAN_READY,
        SCAN_START,
        SCAN_END,
        UNLOADING,
        LOT_START,
        LOT_END,
    }
    public enum EmInspPcEvent
    {
        INSPECTION_COMPLETE,
        Z1_HOME_POS,
        Z2_HOME_POS,
        Z1_ZERO_POS,
        Z2_ZERO_POS,
        Z1_JOG,
        Z2_JOG,
        Z1_POS_MOVE,
        Z2_POS_MOVE,
        INSP_COMPLETE,
        RPC_PPID_CHANGE,
        NEW_PPID,
    }
    public class InspPcCmd
    {
        private int SignalTimeOut = 5;
        public string Name { get; set; }

        public PlcAddr YB_CMD { get; set; }
        public PlcAddr XB_CMD_ACK { get; set; }
        public PlcAddr XB_CMD_NACK { get; set; }

        public object Tag { get; set; }

        public int Step = 0;
        private DateTime _stepTime = DateTime.Now;
        public Action<InspPcCmd> OnCommand { get; set; }
        public void LogicWorking()
        {
            if (Step == 0)
            {
                if (YB_CMD.vBit == true)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "제어→검사 PC {0} 신호 시작", Name);
                    _stepTime = DateTime.Now;
                    if (OnCommand != null)
                        OnCommand(this);

                    XB_CMD_ACK.vBit = true;
                    Logger.Log.AppendLine(LogLevel.Info, "검사→제어 PC {0} 신호 수신 {1}", Name, XB_CMD_ACK.vBit ? "성공" : "실패");
                    Step = 10;
                }
            }
            else if (Step == 10)
            {
                if (YB_CMD.vBit == false)
                {
                    XB_CMD_ACK.vBit = false;
                    Logger.Log.AppendLine(LogLevel.Info, "제어→검사 PC {0} 신호 종료", Name);
                    Step = 0;
                }
            }
        }
    }

    public class InspPcEvent
    {
        private int SignalTimeOut = 5;
        public int Step = 0;
        private DateTime _stepTime = DateTime.Now;

        public string Name { get; set; }

        public PlcAddr XB_EVENT { get; set; }
        public PlcAddr YB_EVENT_ACK { get; set; }
        public Action<InspPcEvent> OnEvent { get; set; }

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
                Logger.Log.AppendLine(LogLevel.Info, "검사→제어 PC Review Complete Event (true)  신호 수신");

                Step = 15;
            }
            else if (Step == 15)
            {
                if (YB_EVENT_ACK.vBit == true)
                {
                    XB_EVENT.vBit = false;
                    Logger.Log.AppendLine(LogLevel.Info, "제어→검사 PC Unloading (true) 신호 송신");
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

    public class InspPcSimul
    {
        //공통 출력 모드... 
        #region IO 리스트
        public PlcAddr YB_ControlAlive { get; set; }
        public PlcAddr YB_RunningMode { get; set; }
        public PlcAddr YB_ReviewMode { get; set; }
        public PlcAddr YB_ReviewManualMove { get; set; }
        public PlcAddr YB_BypassMode { get; set; }
        public PlcAddr YB_VaripanelMode { get; set; }
        public PlcAddr YB_DualMode { get; set; }
        public PlcAddr YB_PpidUseMode { get; set; }

        public PlcAddr YB_Loading { get; set; }
        public PlcAddr YB_ScanReady { get; set; }
        public PlcAddr YB_ScanStart { get; set; }
        public PlcAddr YB_ScanEnd { get; set; }
        public PlcAddr YB_Unloading { get; set; }
        public PlcAddr YB_InspectionEnd { get; set; }
        public PlcAddr YB_LotStart { get; set; }
        public PlcAddr YB_LotEnd { get; set; }
        public PlcAddr YB_Z1HomePos { get; set; }
        public PlcAddr YB_Z2HomePos { get; set; }
        public PlcAddr YB_Z1ZeroPos { get; set; }
        public PlcAddr YB_Z2ZeroPos { get; set; }
        public PlcAddr YB_Z1Jog { get; set; }
        public PlcAddr YB_Z2Jog { get; set; }
        public PlcAddr YB_Z1PosMove { get; set; }
        public PlcAddr YB_Z2PosMove { get; set; }
        public PlcAddr YB_InspectionCompleteAck { get; set; }
        public PlcAddr YB_RpcPpidChange { get; set; }
        public PlcAddr YB_NewPpid { get; set; }        
        public PlcAddr YB_JudgeCompleteAck { get; set; }

        public PlcAddr XB_InspectionAlive { get; set; }
        public PlcAddr XB_LoadingSuccess { get; set; }
        public PlcAddr XB_LoadingFail { get; set; }
        public PlcAddr XB_ScanReadySuccess { get; set; }
        public PlcAddr XB_ScanReadyFail { get; set; }
        public PlcAddr XB_ScanStartSuccess { get; set; }
        public PlcAddr XB_ScanStartFail { get; set; }
        public PlcAddr XB_ScanEndSuccess { get; set; }
        public PlcAddr XB_ScanEndFail { get; set; }
        public PlcAddr XB_UnloadingSuccess { get; set; }
        public PlcAddr XB_UnloadingFail { get; set; }
        public PlcAddr XB_LotStartSuccess { get; set; }
        public PlcAddr XB_LotStartFail { get; set; }
        public PlcAddr XB_LotEndSuccess { get; set; }
        public PlcAddr XB_LotEndFail { get; set; }
        public PlcAddr XB_ZxisMoveStart { get; set; }
        public PlcAddr XB_InspectionComplete { get; set; }
        public PlcAddr XB_JudgeComplete { get; set; }

        public PlcAddr XB_InspectorError { get; set; }
        public PlcAddr XB_PreAlignError { get; set; }
        public PlcAddr XB_ServerOverflowError { get; set; }
        public PlcAddr XB_InspectOverflowError { get; set; }
        public PlcAddr XB_StackLoadingFailError { get; set; }
        public PlcAddr XB_CommonDefectError { get; set; }
        public PlcAddr XB_MaskDefectError { get; set; }
        public PlcAddr XB_EdgeCrackError { get; set; }
        public PlcAddr XB_NoRecipeError { get; set; }
        public PlcAddr XB_FindEdgeFailError { get; set; }
        public PlcAddr XB_LightErrorError { get; set; }
        public PlcAddr XB_UpperLimitError { get; set; }
        public PlcAddr XB_LowestLimitError { get; set; }
        public PlcAddr XB_ZAxisFailError { get; set; }
        public PlcAddr XB_NoImageError { get; set; }
        public PlcAddr XB_GlassDetectFailError { get; set; }


        public PlcAddr YI_ScanIndex { get; set; }
        public PlcAddr YI_ScanCount { get; set; }

        public PlcAddr XF_Z1Pos { get; set; }
        public PlcAddr XF_Z1Speed { get; set; }
        public PlcAddr XF_Z1JogSpeed { get; set; }
        public PlcAddr XF_Z2Pos { get; set; }
        public PlcAddr XF_Z2Speed { get; set; }
        public PlcAddr XF_Z2JogSpeed { get; set; }
        public PlcAddr YW_GlassData { get; set; }
        #endregion

        public InspPcCmd[] LstInspPcCmd = new InspPcCmd[10];
        public InspPcEvent[] LstInspPcEvent = new InspPcEvent[10];

        public int ScanIndex { get; set; }
        public int ScanCount { get; set; }


        public bool Initialize()
        {
            LstInspPcCmd[(int)EmInspPcCommand.LOADING]              /**/ = new InspPcCmd() { Name = "LOADING",          /**/ YB_CMD = YB_Loading,       /**/ XB_CMD_ACK = XB_LoadingSuccess,        /**/ XB_CMD_NACK = XB_LoadingFail };
            LstInspPcCmd[(int)EmInspPcCommand.SCAN_READY]           /**/ = new InspPcCmd() { Name = "SCAN_READY",       /**/ YB_CMD = YB_ScanReady,     /**/ XB_CMD_ACK = XB_ScanReadySuccess,      /**/ XB_CMD_NACK = XB_ScanReadyFail };
            LstInspPcCmd[(int)EmInspPcCommand.SCAN_START]           /**/ = new InspPcCmd() { Name = "SCAN_START",       /**/ YB_CMD = YB_ScanStart,     /**/ XB_CMD_ACK = XB_ScanStartSuccess,      /**/ XB_CMD_NACK = XB_ScanStartFail, OnCommand = OnScanStartCmd };
            LstInspPcCmd[(int)EmInspPcCommand.SCAN_END]             /**/ = new InspPcCmd() { Name = "SCAN_END",         /**/ YB_CMD = YB_ScanEnd,       /**/ XB_CMD_ACK = XB_ScanEndSuccess,        /**/ XB_CMD_NACK = XB_ScanEndFail, };
            LstInspPcCmd[(int)EmInspPcCommand.UNLOADING]            /**/ = new InspPcCmd() { Name = "UNLOADING",        /**/ YB_CMD = YB_Unloading,     /**/ XB_CMD_ACK = XB_UnloadingSuccess,      /**/ XB_CMD_NACK = XB_UnloadingFail };
            LstInspPcCmd[(int)EmInspPcCommand.LOT_START]            /**/ = new InspPcCmd() { Name = "LOT_START",        /**/ YB_CMD = YB_LotStart,      /**/ XB_CMD_ACK = XB_LotStartSuccess,       /**/ XB_CMD_NACK = XB_LotStartFail };
            LstInspPcCmd[(int)EmInspPcCommand.LOT_END]              /**/ = new InspPcCmd() { Name = "LOT_END",          /**/ YB_CMD = YB_LotEnd,        /**/ XB_CMD_ACK = XB_LotEndSuccess,         /**/ XB_CMD_NACK = XB_LotEndFail };

            LstInspPcEvent[(int)EmInspPcEvent.INSPECTION_COMPLETE]  /**/ = new InspPcEvent() { Name = "INSPECTION_COMPLETE",    /**/ XB_EVENT = XB_InspectionComplete,  /**/ YB_EVENT_ACK = YB_InspectionCompleteAck, OnEvent = OnInspectionCompleteEvent };


            return true;
        }

        public bool StartCommand(EmInspPcCommand cmd, object tag)
        {

            LstInspPcCmd[(int)cmd].Tag = tag;
            LstInspPcCmd[(int)cmd].Step = 10;
            return true;
        }
        public bool IsCommandAck(EmInspPcCommand cmd)
        {
            return LstInspPcCmd[(int)cmd].Step == 0;
        }

        public void LogicWorking()
        {
            foreach (InspPcCmd cmd in LstInspPcCmd)
            {
                if (cmd == null) continue;
                cmd.LogicWorking();
            }
            foreach (InspPcEvent evt in LstInspPcEvent)
            {
                if (evt == null) continue;
                evt.LogicWorking();
            }
        }

        //메소드 - 명령 처리..
        public void OnScanStartCmd(InspPcCmd sender)
        {
            ScanIndex = this.YI_ScanIndex.vInt;
            ScanCount = this.YI_ScanCount.vInt;
        }
        //메소드 - 이벤트 처리..
        public void OnInspectionCompleteEvent(InspPcEvent sender)
        {

        }

        
    }
}
