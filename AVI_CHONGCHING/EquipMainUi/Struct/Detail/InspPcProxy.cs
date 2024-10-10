using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipComm.PLC;
using DitCim.PLC;

namespace EquipMainUi.Struct
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
        APC_MODE_CHAGE,
        RPC_MODE_CHAGE,
        CURRENT_PPID_REQ,
        RMM_CRUD_REQ,
    }
    public enum EmInspPcEvent
    {
        INSPECTION_COMPLETE,
        JUDGE_COMPLETE,
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
        private int SignalTimeOut = 30;
        public string Name { get; set; }
        public EM_AL_LST TimeOver { get; set; }

        public PlcAddr YB_CMD { get; set; }
        public PlcAddr XB_CMD_ACK { get; set; }
        public PlcAddr XB_CMD_NACK { get; set; }

        public object Tag { get; set; }

        public int Step = 0;
        private DateTime _stepTime = DateTime.Now;
        public Action<Equipment, InspPcCmd> OnCommand { get; set; }
        public void LogicWorking(Equipment equip)
        {
            if (Step > 10 && (DateTime.Now - _stepTime).TotalSeconds > SignalTimeOut)
            {
                Logger.Log.AppendLine(LogLevel.Error, "제어↔검사 PC {0} 시그널 TIME OVER", Name);
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
                if (OnCommand != null)
                    OnCommand(equip, this);
                Step = 15;
            }
            else if (Step == 15)
            {
                _stepTime = DateTime.Now;
                YB_CMD.vBit = true;
                Logger.Log.AppendLine(LogLevel.Info, "제어→검사 PC {0} 시그널 시작", Name);
                Step = 20;
            }
            else if (Step == 20)
            {
                if (XB_CMD_ACK.vBit || XB_CMD_NACK.vBit || GG.PCTestMode)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "검사→제어 PC {0} 시그널 종료 - {1}", Name, XB_CMD_ACK.vBit ? "성공" : "실패");
                    YB_CMD.vBit = false;
                    Step = 0;
                }
            }
        }
    }

    public class InspPcEvent
    {
        private int SignalTimeOut = 30;
        public int Step = 0;
        private DateTime _stepTime = DateTime.Now;

        public string Name { get; set; }

        public EM_AL_LST TimeOver { get; set; }
        public PlcAddr XB_EVENT { get; set; }
        public PlcAddr YB_EVENT_ACK { get; set; }
        public Action<Equipment, InspPcEvent> OnEvent { get; set; }

        public void LogicWorking(Equipment equip)
        {
            if (Step > 10 && (DateTime.Now - _stepTime).TotalSeconds > SignalTimeOut)
            {
                Logger.Log.AppendLine(LogLevel.Error, "제어↔검사 PC {0} 이벤트 TIME OVER", Name);

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
                    Logger.Log.AppendLine(LogLevel.Info, "검사→제어 {0} 이벤트 시작", Name);


                    YB_EVENT_ACK.vBit = true;
                    //Logger.Log.AppendLine(LogLevel.Info, "제어→검사 {0} 이벤트 응답 신호 송신 ");
                    Step = 10;
                }
            }
            else if (Step == 10)
            {
                if (XB_EVENT.vBit == false)
                {
                    if (OnEvent != null)
                        OnEvent(equip, this);

                    Logger.Log.AppendLine(LogLevel.Info, "검사→제어 {0} 이벤트 종료", Name);
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

    public class InspPcProxy
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
        public PlcAddr YB_CurrentSideInfoWriteComplte { get; set; }
        public PlcAddr YI_CurrentSideInfo { get; set; }

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

        public PlcAddr XF_ReviewTotalDefect { get; set; }
        public PlcAddr XF_ReviewCurDefectIndex { get; set; }

        public PlcAddr YF_Z1CurPos { get; set; }
        public PlcAddr YF_Z2CurPos { get; set; }
        public PlcAddr YF_Z3CurPos { get; set; }

        public PlcAddr[] XF_Z1Pos = new PlcAddr[3];
        public PlcAddr[] XF_Z2Pos = new PlcAddr[3];
        public PlcAddr[] XF_Z3Pos = new PlcAddr[3];

        public PlcAddr YW_GlassData { get; set; }
        public PlcAddr XW_Judgement { get; set; }            
        #endregion

        public InspPcCmd[] LstInspPcCmd = new InspPcCmd[10];
        public InspPcEvent[] LstInspPcEvent = new InspPcEvent[10];

        public int ScanIndex { get; set; }
        public const int SCAN_COUNT = 3;        

        private bool _isInspectionComplete;
        private bool _isJudgeComplete;

        public bool Initailize()
        {
            YI_CurrentSideInfo.vInt = (int)EmSIdeMode.UP;   //임시 테스트용~
            LstInspPcCmd[(int)EmInspPcCommand.LOADING]              /**/ = new InspPcCmd() { Name = "LOADING",          /**/ YB_CMD = YB_Loading,       /**/ XB_CMD_ACK = XB_LoadingSuccess,        /**/ XB_CMD_NACK = XB_LoadingFail, OnCommand = OnGlassLoading };
            LstInspPcCmd[(int)EmInspPcCommand.SCAN_READY]           /**/ = new InspPcCmd() { Name = "SCAN_READY",       /**/ YB_CMD = YB_ScanReady,     /**/ XB_CMD_ACK = XB_ScanReadySuccess,      /**/ XB_CMD_NACK = XB_ScanReadyFail };
            LstInspPcCmd[(int)EmInspPcCommand.SCAN_START]           /**/ = new InspPcCmd() { Name = "SCAN_START",       /**/ YB_CMD = YB_ScanStart,     /**/ XB_CMD_ACK = XB_ScanStartSuccess,      /**/ XB_CMD_NACK = XB_ScanStartFail, OnCommand = OnScanStartCmd };
            LstInspPcCmd[(int)EmInspPcCommand.SCAN_END]             /**/ = new InspPcCmd() { Name = "SCAN_END",         /**/ YB_CMD = YB_ScanEnd,       /**/ XB_CMD_ACK = XB_ScanEndSuccess,        /**/ XB_CMD_NACK = XB_ScanEndFail, };
            LstInspPcCmd[(int)EmInspPcCommand.UNLOADING]            /**/ = new InspPcCmd() { Name = "UNLOADING",        /**/ YB_CMD = YB_Unloading,     /**/ XB_CMD_ACK = XB_UnloadingSuccess,      /**/ XB_CMD_NACK = XB_UnloadingFail };
            LstInspPcCmd[(int)EmInspPcCommand.LOT_START]            /**/ = new InspPcCmd() { Name = "LOT_START",        /**/ YB_CMD = YB_LotStart,      /**/ XB_CMD_ACK = XB_LotStartSuccess,       /**/ XB_CMD_NACK = XB_LotStartFail };
            LstInspPcCmd[(int)EmInspPcCommand.LOT_END]              /**/ = new InspPcCmd() { Name = "LOT_END",          /**/ YB_CMD = YB_LotEnd,        /**/ XB_CMD_ACK = XB_LotEndSuccess,         /**/ XB_CMD_NACK = XB_LotEndFail };

            LstInspPcEvent[(int)EmInspPcEvent.INSPECTION_COMPLETE]  /**/ = new InspPcEvent() { Name = "INSPECTION_COMPLETE",    /**/ XB_EVENT = XB_InspectionComplete,  /**/ YB_EVENT_ACK = YB_InspectionCompleteAck,   /**/ OnEvent = OnInspectionCompleteEvent };
            LstInspPcEvent[(int)EmInspPcEvent.JUDGE_COMPLETE]       /**/ = new InspPcEvent() { Name = "JUDGE_COMPLETE",         /**/ XB_EVENT = XB_JudgeComplete,       /**/ YB_EVENT_ACK = YB_JudgeCompleteAck,        /**/ OnEvent = OnJudgeComplete };

            return true;
        }

        public bool StartCommand(Equipment equip, EmInspPcCommand cmd, object tag)
        {
            if (LstInspPcCmd[(int)cmd].Step != 0)
            {
                InterLockMgr.AddInterLock(string.Format("인터락<실행중>\n(검사 PC와 {0} 기존 명령이 진행 중입니다.)", cmd.ToString()));
                Logger.Log.AppendLine(LogLevel.Warning, string.Format("검사 PC와 {0} 기존 명령이 진행 중", cmd.ToString()));
                return false;
            }

            if (cmd == EmInspPcCommand.LOADING)
            {
                _isInspectionComplete = false;
                _isJudgeComplete = false;
            }

            LstInspPcCmd[(int)cmd].Tag = tag;
            LstInspPcCmd[(int)cmd].Step = 10;
            return true;
        }
        public bool IsCommandAck(Equipment equip, EmInspPcCommand cmd)
        {
            if (GG.TestMode || GG.PCTestMode)
                return true;

            return LstInspPcCmd[(int)cmd].Step == 0;
        }

        public void LogicWorking(Equipment equip)
        {
            YF_Z1CurPos.vFloat = equip.InspZ1.XF_CurrMotorPosition.vFloat;
            YF_Z2CurPos.vFloat = equip.InspZ2.XF_CurrMotorPosition.vFloat;
            foreach (InspPcCmd cmd in LstInspPcCmd)
            {
                if (cmd == null) continue;
                cmd.LogicWorking(equip);
            }
            foreach (InspPcEvent evt in LstInspPcEvent)
            {
                if (evt == null) continue;
                evt.LogicWorking(equip);
            }
            CheckStatus(equip);
            if(GG.TestMode == false)
                CheckAlive(equip);
        }
        private void CheckStatus(Equipment equip)
        {
            if (this.XB_InspectorError)          /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0241_INSPECTOR_ERROR);
            if (XB_PreAlignError)                /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0242_PRE_ALIGN);
            if (XB_ServerOverflowError)          /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0243_SERVER_OVERFLOW);
            if (XB_InspectOverflowError)         /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0244_INSPECT_OVERFLOW);
            if (XB_StackLoadingFailError)        /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0245_STACK_LOADING_FAIL);
            if (XB_CommonDefectError)            /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0246_COMMON_DEFECT);
            if (XB_MaskDefectError)              /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0247_MASK_DEFECT);
            if (XB_EdgeCrackError)               /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0248_EDGE_CRACK);
            if (XB_NoRecipeError)                /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0249_NO_RECIPE);
            if (XB_FindEdgeFailError)            /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0250_FIND_EDGE_FAIL);
            if (XB_LightErrorError)              /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0251_LIGHT_ERROR);
            if (XB_UpperLimitError)              /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0252_UPPER_LIMIT);
            if (XB_LowestLimitError)             /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0253_LOWEST_LIMIT);
            if (XB_ZAxisFailError)               /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0254_Z_AXIS_FAIL);
            if (XB_NoImageError)                 /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0255_NO_IMAGE);
            if (XB_GlassDetectFailError)         /**/AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0256_GLASS_DETECT_FAIL);
        }

        //ALIVE 처리..
        private DateTime _aliveDateTime = DateTime.Now;
        private bool _aoiPlcAlive = false;
        private int StepTimeOut = 10;

        private void CheckAlive(Equipment equip)
        {
            if (_aoiPlcAlive != XB_InspectionAlive.vBit)
            {
                _aliveDateTime = DateTime.Now;
                _aoiPlcAlive = XB_InspectionAlive.vBit;
            }

            if ((DateTime.Now - _aliveDateTime).TotalSeconds > StepTimeOut)
            {
                _aliveDateTime = DateTime.Now.AddHours(10);
                AlarmMgr.Instatnce.Happen(equip, EM_AL_LST.AL_0231_INSP_PC_ALIVE_ERROR);
                Logger.Log.AppendLine(LogLevel.Error, "INSP PC ALIVE ERROR 발생함");
            }
        }

        public bool IsScanComplete(EmInspPcEvent cmd)
        {
            if (GG.PCTestMode) return true;

            if (cmd == EmInspPcEvent.INSPECTION_COMPLETE)
                return LstInspPcEvent[(int)cmd].Step == 0 && _isInspectionComplete;
            else if (cmd == EmInspPcEvent.JUDGE_COMPLETE)
                return LstInspPcEvent[(int)cmd].Step == 0 && _isJudgeComplete;
            else
                return false;
        }

        //메소드 - 명령 처리..
        public void OnScanStartCmd(Equipment equip, InspPcCmd sender)
        {
            this.YI_ScanIndex.vInt = (int)sender.Tag;
            this.YI_ScanCount.vInt = SCAN_COUNT;
        }

        //메소드 - 이벤트 처리..
        public void OnInspectionCompleteEvent(Equipment equip, InspPcEvent sender)
        {
            _isInspectionComplete = true;
        }
        public void OnJudgeComplete(Equipment equip, InspPcEvent sender)
        {
            _isJudgeComplete = true;
        }
        public void OnGlassLoading(Equipment equip, InspPcCmd cmd)
        {
            //검사기에 쓰기...
            WriteGlassData(YW_GlassData, equip.LoadingGlassInfo);
        }
        public void WriteGlassData(PlcAddr addr, GlassInfo gls)
        {
            addr.PLC.SetAscii(AOIW.H_GLASSID_POS + addr.Addr, gls.HGlassID);
            addr.PLC.SetAscii(AOIW.E_GLASSID_POS + addr.Addr, gls.EGlassID);
            addr.PLC.SetAscii(AOIW.LOTID_POS + addr.Addr, gls.LotID);
            addr.PLC.SetAscii(AOIW.BATCHID_POS + addr.Addr, gls.BatchID);
            addr.PLC.SetAscii(AOIW.JOBID_POS + addr.Addr, gls.JobID);
            addr.PLC.SetAscii(AOIW.PORTID_POS + addr.Addr, gls.PortID);
            addr.PLC.SetAscii(AOIW.SLOTNO_POS + addr.Addr, gls.SlotID);
            addr.PLC.SetAscii(AOIW.PRODUCT_TYPE_POS + addr.Addr, gls.ProductType);
            addr.PLC.SetAscii(AOIW.PRODUCT_KIND_POS + addr.Addr, gls.ProductKind);
            addr.PLC.SetAscii(AOIW.PRODUCTID_POS + addr.Addr, gls.ProductID);
            addr.PLC.SetAscii(AOIW.RUNSPECID_POS + addr.Addr, gls.RunSpecID);
            addr.PLC.SetAscii(AOIW.LAYERID_POS + addr.Addr, gls.LayerID);
            addr.PLC.SetAscii(AOIW.STEPID_POS + addr.Addr, gls.StepID);
            addr.PLC.SetAscii(AOIW.PPID_POS + addr.Addr, gls.PPID);
            addr.PLC.SetAscii(AOIW.FLOWID_POS + addr.Addr, gls.FlowID); 

            addr.PLC.SetShort(AOIW.GLASS_SIZE_POS + addr.Addr, (short)gls.GlassSize[0]);
            addr.PLC.SetShort(AOIW.GLASS_SIZE_POS + addr.Addr +2, (short)gls.GlassSize[1]);
            addr.PLC.SetShort(AOIW.GLASS_THICKNESS_POS + addr.Addr, (short)gls.GlassThickness);
            addr.PLC.SetShort(AOIW.GLASS_STATE_POS + addr.Addr, (short)gls.GlassState);

            addr.PLC.SetAscii(AOIW.GLASS_ORDER_POS + addr.Addr, gls.GlassOrder);
            addr.PLC.SetAscii(AOIW.COMMENT_POS + addr.Addr, gls.Comment);
            addr.PLC.SetAscii(AOIW.USE_COUNT_POS + addr.Addr, gls.UseCount);
            addr.PLC.SetAscii(AOIW.JUDGEMENT_POS + addr.Addr, gls.Judgement);
            addr.PLC.SetAscii(AOIW.REASON_CODE_POS + addr.Addr, gls.ReasonCode);
            addr.PLC.SetAscii(AOIW.INS_FLAG_POS + addr.Addr, gls.InsFlag);
            addr.PLC.SetAscii(AOIW.ENC_FLAG_POS + addr.Addr, gls.EncFlag);
            addr.PLC.SetAscii(AOIW.PRERUN_FLAG_POS + addr.Addr, gls.PrerunFlag);
            addr.PLC.SetAscii(AOIW.TURN_DIR_POS + addr.Addr, gls.TurnDir);
            addr.PLC.SetAscii(AOIW.FLIP_STATE_POS + addr.Addr, gls.FlipState);
            addr.PLC.SetAscii(AOIW.WORK_STATE_POS + addr.Addr, gls.WorkState);
            addr.PLC.SetAscii(AOIW.MULTI_USE_POS + addr.Addr, gls.MultiUse);
            addr.PLC.SetAscii(AOIW.PAIR_GLASSID_POS + addr.Addr, gls.PairGlassID);
            addr.PLC.SetAscii(AOIW.PAIR_PPID_POS + addr.Addr, gls.PairPPID);
            addr.PLC.SetAscii(AOIW.OPTIONNAME1_POS + addr.Addr, gls.OptionName1);
            addr.PLC.SetAscii(AOIW.OPTIONVALUE1_POS + addr.Addr, gls.OptionValue1);
            addr.PLC.SetAscii(AOIW.OPTIONNAME2_POS + addr.Addr, gls.OptionName2);
            addr.PLC.SetAscii(AOIW.OPTIONVALUE2_POS + addr.Addr, gls.OptionValue2);
            addr.PLC.SetAscii(AOIW.OPTIONNAME3_POS + addr.Addr, gls.OptionName3);
            addr.PLC.SetAscii(AOIW.OPTIONVALUE3_POS + addr.Addr, gls.OptionValue3);
            addr.PLC.SetAscii(AOIW.OPTIONNAME4_POS + addr.Addr, gls.OptionName4);
            addr.PLC.SetAscii(AOIW.OPTIONVALUE4_POS + addr.Addr, gls.OptionValue4);
            addr.PLC.SetAscii(AOIW.OPTIONNAME5_POS + addr.Addr, gls.OptionName5);
            addr.PLC.SetAscii(AOIW.OPTIONVALUE5_POS + addr.Addr, gls.OptionValue5);
        }
        public GlassInfo ReadGlassData(PlcAddr addr)
        {
            GlassInfo gls = new GlassInfo();
            //glassInfo.HGlassID = this.YW_GlassData.PLC.GetAscii(new PlcAddr(PlcMemType.S, 500, 0, 16)).Trim();

            gls.HGlassID = addr.PLC.GetAscii(AOIW.H_GLASSID_POS + addr.Addr); 

            return gls;
        }
        public GlassInfo ReadJudgeData(Equipment equip)
        {
            equip.ResultJudgeGlassInfo.Judgement = XW_Judgement.vAscii;

            return equip.ResultJudgeGlassInfo;
        }
    }
}
