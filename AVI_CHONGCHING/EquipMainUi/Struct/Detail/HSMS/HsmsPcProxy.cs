﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using Dit.Framework.Comm;
using EquipMainUi.Struct.Detail.HSMS;
using DitCim.PLC;
using DitCim.Common;
using EquipMainUi.Struct.Detail;
using EquipMainUi.Struct.Detail.HSMS.ReportStruct;
using EquipMainUi.Struct.TransferData;
using EquipMainUi.Struct.Detail.EFEM;
using System.IO;
using EquipMainUi.Setting;
using System.Windows.Forms;

namespace EquipMainUi.Struct
{

    public class HsmsPcProxy
    {
        private bool _needInitFdc = true;

        public EMMCmd MCmd { get; set; }
        public EMByWho MCmdByWho { get; set; }
        public HsmsPcCmd[] LstCmd = new HsmsPcCmd[Enum.GetValues(typeof(EmHsmsPcCommand)).Length];
        public HsmsPcEvent[] LstEvt = new HsmsPcEvent[Enum.GetValues(typeof(EmHsmsPcEvent)).Length];

        public Queue<string> OperCallMsg = new Queue<string>();
        public List<PlcAddr> TerminalMsgs = new List<PlcAddr>();
        List<PlcAddr> slotInfoPLCList;
        public bool Initailize(IVirtualMem plc)
        {
            CIMAB.Initailize(plc);
            CIMAW.Initailize(plc);

            LstCmd[(int)EmHsmsPcCommand.CASSETTE_LOAD]      = new HsmsPcCmd(EmHsmsPcCommand.CASSETTE_LOAD)      { YB_CMD = CIMAB.YB_CST_LOAD_REPORT, XB_CMD_ACK = CIMAB.XB_CST_LOAD_REPLY, OnCommand = OnCassetteLoad };
            LstCmd[(int)EmHsmsPcCommand.LOT_START]          = new HsmsPcCmd(EmHsmsPcCommand.LOT_START)          { YB_CMD = CIMAB.YB_LOT_START_REPORT, XB_CMD_ACK = CIMAB.XB_LOT_START_REPLY, OnCommand = OnLotStart };
            LstCmd[(int)EmHsmsPcCommand.WAFER_LOAD]         = new HsmsPcCmd(EmHsmsPcCommand.WAFER_LOAD)         { YB_CMD = CIMAB.YB_WAFER_LOAD_REPORT, XB_CMD_ACK = CIMAB.XB_WAFER_LOAD_REPLY, OnCommand = OnWaferLoad };
            LstCmd[(int)EmHsmsPcCommand.WAFER_MAP_REQUEST]  = new HsmsPcCmd(EmHsmsPcCommand.WAFER_MAP_REQUEST)  { YB_CMD = CIMAB.YB_WAFER_MAP_REQUEST_REPORT, XB_CMD_ACK = CIMAB.XB_WAFER_MAP_REQUEST_REPLY, OnCommand = OnWaferMapRequest };
            LstCmd[(int)EmHsmsPcCommand.WAFER_UNLOAD]       = new HsmsPcCmd(EmHsmsPcCommand.WAFER_UNLOAD)       { YB_CMD = CIMAB.YB_WAFER_UNLOAD_REPORT, XB_CMD_ACK = CIMAB.XB_WAFER_UNLOAD_REPLY, OnCommand = OnWaferUnload };
            LstCmd[(int)EmHsmsPcCommand.CASSETTE_UNLOAD]    = new HsmsPcCmd(EmHsmsPcCommand.CASSETTE_UNLOAD)    { YB_CMD = CIMAB.YB_CST_UNLOAD_REPORT, XB_CMD_ACK = CIMAB.XB_CST_UNLOAD_REPLY, OnCommand = OnCassetteUnload };
            LstCmd[(int)EmHsmsPcCommand.ALARM_REPORT]       = new HsmsPcCmd(EmHsmsPcCommand.ALARM_REPORT)       { YB_CMD = CIMAB.YB_ALARM_REPORT, XB_CMD_ACK = CIMAB.XB_ALARM_REPLY, OnCommand = OnAlarmReport };
            LstCmd[(int)EmHsmsPcCommand.RECIPE_SELECT]      = new HsmsPcCmd(EmHsmsPcCommand.RECIPE_SELECT)      { YB_CMD = CIMAB.YB_RECIPE_SELECT_REPORT, XB_CMD_ACK = CIMAB.XB_RECIPE_SELECT_REPLY, OnCommand = OnPortRecipeSelect };
            LstCmd[(int)EmHsmsPcCommand.EQP_STATUS_CHANGE]  = new HsmsPcCmd(EmHsmsPcCommand.EQP_STATUS_CHANGE)  { YB_CMD = CIMAB.YB_EQ_STATUS_CHANGE_REPORT, XB_CMD_ACK = CIMAB.XB_EQ_STATE_CHANGE_REPLY, OnCommand = OnChangeEquipStatus };
            LstCmd[(int)EmHsmsPcCommand.OS_VERSION_REPORT]  = new HsmsPcCmd(EmHsmsPcCommand.OS_VERSION_REPORT)  { YB_CMD = CIMAB.YB_OS_VERSION_REPORT, XB_CMD_ACK = CIMAB.XB_OS_VERSION_REPLY, OnCommand = OsVersionReport };
            LstCmd[(int)EmHsmsPcCommand.ECID_CHANGE]        = new HsmsPcCmd(EmHsmsPcCommand.ECID_CHANGE)        { YB_CMD = CIMAB.YB_ECID_CHANGE_REPORT, XB_CMD_ACK = CIMAB.XB_ECID_CHANGE_REPLY, OnCommand = OnECIDChange };
            LstCmd[(int)EmHsmsPcCommand.OHT_MODE_CHANGE]    = new HsmsPcCmd(EmHsmsPcCommand.OHT_MODE_CHANGE)    { YB_CMD = CIMAB.YB_OHT_MODE_CHANGE_REPORT, XB_CMD_ACK = CIMAB.XB_OHT_MODE_CHANGE_REPLY, OnCommand = OnChageOHTMode };
            LstCmd[(int)EmHsmsPcCommand.PORT_MODE_CHANGE]   = new HsmsPcCmd(EmHsmsPcCommand.PORT_MODE_CHANGE)   { YB_CMD = CIMAB.YB_PORT_MODE_CHANGE_REPORT, XB_CMD_ACK = CIMAB.XB_PORT_MODE_CHANGE_REPLY, OnCommand = OnPortModeChange };
            LstCmd[(int)EmHsmsPcCommand.EQP_MODE_CHANGE]    = new HsmsPcCmd(EmHsmsPcCommand.EQP_MODE_CHANGE)    { YB_CMD = CIMAB.YB_EQP_MODE_CHANGE_REPORT, XB_CMD_ACK = CIMAB.XB_EQP_MODE_CHANGE_REPLY, OnCommand = OnEqpModeChange };
            LstCmd[(int)EmHsmsPcCommand.CTRL_MODE_CHANGE]   = new HsmsPcCmd(EmHsmsPcCommand.CTRL_MODE_CHANGE)   { YB_CMD = CIMAB.YB_CTRL_MODE_CHANGE_REPORT, XB_CMD_ACK = CIMAB.XB_CTRL_MODE_CHANGE_REPLY, OnCommand = OnCtrlModeChange, OnAfeterAck = OnCtrlModeChangeResult};

            LstEvt[(int)EmHsmsPcEvent.CST_MAP]              = new HsmsPcEvent(EmHsmsPcEvent.CST_MAP)            { XB_EVENT = CIMAB.XB_CST_MAP_COMMAND, YB_EVENT_ACK = CIMAB.YB_CST_MAP_REPLY, OnEvent = OnCstMapEvent };
            LstEvt[(int)EmHsmsPcEvent.PP_SELECT]            = new HsmsPcEvent(EmHsmsPcEvent.PP_SELECT)          { XB_EVENT = CIMAB.XB_PORT_RECIPE_COMMAND, YB_EVENT_ACK = CIMAB.YB_PORT_RECIPE_REPLY, OnEvent = OnPPSelectEvent };
            LstEvt[(int)EmHsmsPcEvent.LOT_START]            = new HsmsPcEvent(EmHsmsPcEvent.LOT_START)          { XB_EVENT = CIMAB.XB_LOT_START_COMMAND, YB_EVENT_ACK = CIMAB.YB_LOT_START_REPLY, OnEvent = OnLotStartEvent };
            LstEvt[(int)EmHsmsPcEvent.WAFER_START]          = new HsmsPcEvent(EmHsmsPcEvent.WAFER_START)        { XB_EVENT = CIMAB.XB_WAFER_START_COMMAND, YB_EVENT_ACK = CIMAB.YB_WAFER_START_REPLY, OnEvent = OnWaferStartEvent };
            LstEvt[(int)EmHsmsPcEvent.MAP_FILE_CREATE]      = new HsmsPcEvent(EmHsmsPcEvent.MAP_FILE_CREATE)    { XB_EVENT = CIMAB.XB_WAFER_MAP_COMMAND, YB_EVENT_ACK = CIMAB.YB_WAFER_MAP_REPLY, OnEvent = OnWaferMapEvent };
            LstEvt[(int)EmHsmsPcEvent.RCMD]                 = new HsmsPcEvent(EmHsmsPcEvent.RCMD)               { XB_EVENT = CIMAB.XB_RCMD_COMMAND, YB_EVENT_ACK = CIMAB.YB_RCMD_REPLY, OnBeforeAck = OnRcmdBeforeAck };
            LstEvt[(int)EmHsmsPcEvent.ECID_EDIT]            = new HsmsPcEvent(EmHsmsPcEvent.ECID_EDIT)          { XB_EVENT = CIMAB.XB_ECID_CHANGE_COMMAND, YB_EVENT_ACK = CIMAB.YB_ECID_CHANGE_REPLY, OnEvent = OnEcidEditEvent };
            LstEvt[(int)EmHsmsPcEvent.OHT_MODE_CHANGE]      = new HsmsPcEvent(EmHsmsPcEvent.OHT_MODE_CHANGE)    { XB_EVENT = CIMAB.XB_OHT_MODE_CHANGE_COMMAND, YB_EVENT_ACK = CIMAB.YB_OHT_MODE_CHANGE_REPLY, OnBeforeAck = OnOHTModeChangeBeforeAck };
            LstEvt[(int)EmHsmsPcEvent.CTRL_MODE_CHANGE]     = new HsmsPcEvent(EmHsmsPcEvent.CTRL_MODE_CHANGE)   { XB_EVENT = CIMAB.XB_CTRL_STATE_CHANGE_COMMAND, YB_EVENT_ACK = CIMAB.YB_CTRL_STATE_CHANGE_REPLY, OnEvent = OnCtrlStateChangeEvent };
            LstEvt[(int)EmHsmsPcEvent.CST_MOVE_OUT_FLAG]    = new HsmsPcEvent(EmHsmsPcEvent.CST_MOVE_OUT_FLAG)  { XB_EVENT = CIMAB.XB_MOVE_OUT_FLAG_COMMAND, YB_EVENT_ACK = CIMAB.YB_MOVE_OUT_FLAG_REPLY, OnEvent = OnCstMoveOutFlagEvent };
            LstEvt[(int)EmHsmsPcEvent.TERMINAL_MSG]         = new HsmsPcEvent(EmHsmsPcEvent.TERMINAL_MSG)       { XB_EVENT = CIMAB.XB_TERMINA_MSG_COMMAND, YB_EVENT_ACK = CIMAB.YB_TERMINA_MSG_REPLY, OnEvent = OnTerminalMsgEvent };
            LstEvt[(int)EmHsmsPcEvent.DEEP_LEARNING_REVIEW_COMPLETE] = new HsmsPcEvent(EmHsmsPcEvent.DEEP_LEARNING_REVIEW_COMPLETE)       { XB_EVENT = CIMAB.XB_DEEP_LEARNING_REVIEW_COMPLTE_COMMAND, YB_EVENT_ACK = CIMAB.YB_DEEP_LEARNING_REVIEW_COMPLTE_REPLY, OnEvent = OnDeepLearningReviewComplete };
            LstEvt[(int)EmHsmsPcEvent.AUTO_MOVE_OUT] = new HsmsPcEvent(EmHsmsPcEvent.AUTO_MOVE_OUT) { XB_EVENT = CIMAB.XB_AUTO_MOVE_OUT_COMMAND, YB_EVENT_ACK = CIMAB.YB_AUTO_MOVE_OUT_REPLY, OnEvent = OnAutoMoveOutEvent };

            slotInfoPLCList = new List<PlcAddr>();
            slotInfoPLCList.Add(CIMAW.YW_CST_UNLOAD_SLOT_INFO1);
            slotInfoPLCList.Add(CIMAW.YW_CST_UNLOAD_SLOT_INFO2);
            slotInfoPLCList.Add(CIMAW.YW_CST_UNLOAD_SLOT_INFO3);
            slotInfoPLCList.Add(CIMAW.YW_CST_UNLOAD_SLOT_INFO4);
            slotInfoPLCList.Add(CIMAW.YW_CST_UNLOAD_SLOT_INFO5);
            slotInfoPLCList.Add(CIMAW.YW_CST_UNLOAD_SLOT_INFO6);
            slotInfoPLCList.Add(CIMAW.YW_CST_UNLOAD_SLOT_INFO7);
            slotInfoPLCList.Add(CIMAW.YW_CST_UNLOAD_SLOT_INFO8);
            slotInfoPLCList.Add(CIMAW.YW_CST_UNLOAD_SLOT_INFO9);
            slotInfoPLCList.Add(CIMAW.YW_CST_UNLOAD_SLOT_INFO10);
            slotInfoPLCList.Add(CIMAW.YW_CST_UNLOAD_SLOT_INFO11);
            slotInfoPLCList.Add(CIMAW.YW_CST_UNLOAD_SLOT_INFO12);
            slotInfoPLCList.Add(CIMAW.YW_CST_UNLOAD_SLOT_INFO13);

            TerminalMsgs.Add(CIMAW.XW_TerminalMsg1);
            TerminalMsgs.Add(CIMAW.XW_TerminalMsg2);
            TerminalMsgs.Add(CIMAW.XW_TerminalMsg3);
            TerminalMsgs.Add(CIMAW.XW_TerminalMsg4);
            TerminalMsgs.Add(CIMAW.XW_TerminalMsg5);
            TerminalMsgs.Add(CIMAW.XW_TerminalMsg6);
            TerminalMsgs.Add(CIMAW.XW_TerminalMsg7);
            TerminalMsgs.Add(CIMAW.XW_TerminalMsg8);
            TerminalMsgs.Add(CIMAW.XW_TerminalMsg9);
            TerminalMsgs.Add(CIMAW.XW_TerminalMsg10);
            return true;
        }
        public void LogicWorking(Equipment equip)
        {
            foreach (HsmsPcCmd cmd in LstCmd)
            {
                if (cmd == null) continue;
                cmd.LogicWorking(equip);
            }

            foreach (HsmsPcEvent evt in LstEvt)
            {
                if (evt == null) continue;
                evt.LogicWorking(equip);
            }

            //FillFDC(equip);
            ProcessingAlarmReport(equip);
            RecipeReportLogic(equip);
        }

        public bool StartCommand(Equipment equip, EmHsmsPcCommand cmd, object tag)
        {
            if(equip.CimMode == EmCimMode.OffLine && cmd != EmHsmsPcCommand.CTRL_MODE_CHANGE)
            {
                return true;
            }
            if (LstCmd[(int)cmd].Step != 0)
            {
                //InterLockMgr.AddInterLock(string.Format("HSMS PC와 {0} 기존 명령이 진행 중", cmd.ToString()));
                Logger.CIMLog.AppendLine(LogLevel.Warning, string.Format("CIM SW와 이미 {0} 인터페이스가 진행 중", cmd.ToString()));
                return false;
            }
            LstCmd[(int)cmd].Tag = tag;
            LstCmd[(int)cmd].Step = 10;
            return true;
        }
        public bool IsCommandAck(EmHsmsPcCommand cmd)
        {
            if (GG.CimTestMode == true)
                return true;

            return LstCmd[(int)cmd].Step == 0;
        }
        public bool IsEventComplete(EmHsmsPcEvent evt)
        {
            if (GG.CimTestMode == true)
                return true;

            return LstEvt[(int)evt].Step == 0;
        }

        #region OnCommand
        private void OnCassetteLoad(Equipment equip, HsmsPcCmd cmd)
        {
            IsCstLoadConfirmOK = false;
            IsCstLoadIFComplete = false;
            IsCstMoveOutIFComplete = false;
            IsCstMoveOutOK = false;
            IsPPSelectOK = false;

            HsmsCstLoadInfo cstInfo = (HsmsCstLoadInfo)cmd.Tag;

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_CST_LOAD_REQUEST_CST_ID, cstInfo.CstID);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_CST_LOAD_REQUEST_PORT_ID, (short)cstInfo.PortNo);
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_CST_LOAD_REQUEST_CUR_RECIPE, cstInfo.CurrentRecipe);

            Logger.CIMLog.AppendLine("CST LOAD REPORT [CST ID : {0}, PORT ID : {1}, RecipeName : {2}"
                , cstInfo.CstID, cstInfo.PortNo.ToString(), cstInfo.CurrentRecipe);
        }
        private void OnLotStart(Equipment equip, HsmsPcCmd cmd)
        {
            IsLotStartIFComplete = false;
            IsLotStartConfirmOK = false;

            CassetteInfo cstInfo = (CassetteInfo)cmd.Tag;

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_LOT_START_CST_ID, cstInfo.CstID);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_LOT_START_PORT_ID, (short)cstInfo.LoadPortNo);
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_LOT_START_PORT_RECIPE, cstInfo.RecipeName);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_LOT_START_WAFER_COUNT, (short)cstInfo.SlotCount);

            Logger.CIMLog.AppendLine("LOT START REPORT [CST ID : {0}, PORT ID : {1}, RecipeName : {2}, SlotCount : {3}"
                , cstInfo.CstID, cstInfo.LoadPortNo.ToString(), cstInfo.RecipeName, cstInfo.SlotCount.ToString());
        }
        private void OnWaferLoad(Equipment equip, HsmsPcCmd cmd)
        {
            WaferInfoKey key = (WaferInfoKey)cmd.Tag;

            WaferInfo waferInfo = TransferDataMgr.GetWafer(key);
            CassetteInfo cstInfo = TransferDataMgr.GetCst(key);

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_WAFER_LOAD_REQUEST_WAFER_ID, waferInfo.WaferID);
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_WAFER_LOAD_REQUEST_CST_ID, waferInfo.CstID);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_WAFER_LOAD_REQUEST_PORT_ID, (short)cstInfo.LoadPortNo);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_WAFER_LOAD_REQUEST_SLOT_NO, (short)waferInfo.SlotNo);
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_WAFER_LOAD_REQUEST_CURRENT_RECIPE, cstInfo.RecipeName);

            Logger.CIMLog.AppendLine("WAFER LOAD REPORT [WAFER ID : {0}, CST ID : {1}, PORT ID : {2}, SlotNo : {3} RecipeName : {4}"
                , waferInfo.WaferID, waferInfo.CstID, cstInfo.LoadPortNo, waferInfo.SlotNo, cstInfo.RecipeName);
        }
        private void OnWaferMapRequest(Equipment equip, HsmsPcCmd cmd)
        {
            WaferInfoKey key = (WaferInfoKey)cmd.Tag;

            WaferInfo waferInfo = TransferDataMgr.GetWafer(key);

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_WAFER_MAP_REQUEST_WAFER_ID, waferInfo.WaferID);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_WAFER_MAP_REQUEST_ID_TYPE, (short)waferInfo.IDType);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_WAFER_MAP_REQUEST_MAP_FORMAT, (short)waferInfo.MapType);
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_WAFER_MAP_REQUEST_BIN_CODE, waferInfo.BinCode);
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_WAFER_MAP_REQUEST_NULL_BIN_CODE, waferInfo.NullBinCode);

            //Logger.CIMLog.AppendLine("WAFER MAP REQUEST REPORT [WAFER ID : {0}, ID TYPE : {1}, MAP FORMAT : {2}, BIN CODE : {3} NULL BIN CODE : {4}"
            //    , waferInfo.WaferID, waferInfo.IDType.ToString(), waferInfo.MapType.ToString(), waferInfo.BinCode.ToString(), waferInfo.NullBinCode.ToString());
        }
        private void OnWaferUnload(Equipment equip, HsmsPcCmd cmd)
        {
            WaferInfoKey key = (WaferInfoKey)cmd.Tag;
            WaferInfo waferInfo = TransferDataMgr.GetWafer(key);
            waferInfo.EndTime = DateTime.Now;

            CassetteInfo cstInfo = TransferDataMgr.GetCst(waferInfo.CstID);

            int portNo = cstInfo.LoadPortNo;

            if (portNo == 1) { GG.Equip.Efem.LoadPort1.CSTID_Clone = cstInfo.CstID; }
            if (portNo == 2) { GG.Equip.Efem.LoadPort2.CSTID_Clone = cstInfo.CstID; }

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_WAFER_UNLOAD_WAFER_ID, waferInfo.WaferID);
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_WAFER_UNLOAD_CST_ID, waferInfo.CstID);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_WAFER_UNLOAD_PORT_ID, (short)cstInfo.LoadPortNo);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_WAFER_UNLOAD_SLOT_NO, (short)waferInfo.SlotNo);
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_WAFER_UNLOAD_START_TIME, waferInfo.StartTime.ToString("yyyyMMddhhmmss"));
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_WAFER_UNLOAD_END_TIME, waferInfo.EndTime.ToString("yyyyMMddhhmmss"));

            SetMemoryKerfData(waferInfo);

            bool UserLastSlot = false;
            // Joo // 딥러닝 Review 진행 필요 모드: User, Only First, Last , 필요없는 모드: Mapping (전체 진행)
            if (GG.Equip.Efem.LoadPort1.ProgressWay == EmProgressWay.User || GG.Equip.Efem.LoadPort2.ProgressWay == EmProgressWay.User)
            {
                int LastSlotNo = -1;
                if (portNo == 1) { LastSlotNo = GG.Equip.Efem.LoadPort1.LastSlotNo; }
                if (portNo == 2) { LastSlotNo = GG.Equip.Efem.LoadPort2.LastSlotNo; }

                // Joo // 1. User Mode, 일반 시퀀스 모드, 마지막 Wafer Unload
                UserLastSlot = LastSlotNo == waferInfo.SlotNo; // Joo // 현재 Wafer Unload Slot이 User 모드에서 직접선택한 Slot과 일치하면
                // Joo // 2. User Mode, 다음장 진행 모드, Review 판정이 나와서, 두 번째 Wafer Unload (딥러닝 리뷰 판정 신호를 받기 위해서)
                if (!UserLastSlot) // Joo // 다음장 검사 진행하는 Wafer Slot을 알 수가 없음 = Review인지, MoveOut인지
                {
                    // Joo // 3. LPM1 다음장 진행모드 들어갔는데(true 상태), LPM2 정상 진행 Wafer Unload 시, 마지막 장이 아닐 때
                    if (portNo == 1)
                    {
                        UserLastSlot = GG.Equip.Efem.LoadPort1.NextWaferSlot; // Default 값 = false
                        GG.Equip.Efem.LoadPort1.NextWaferSlot = false;
                    }
                    if (portNo == 2)
                    {
                        UserLastSlot = GG.Equip.Efem.LoadPort2.NextWaferSlot;
                        GG.Equip.Efem.LoadPort2.NextWaferSlot = false;
                    }
                }
            }

            // Joo // 해당 Wafer가 마지막인지, 한 장만 검사 모드인지 확인 필요 (1 or 0)
            bool isOnlyOne = GG.Equip.Efem.LoadPort1.ProgressWay == EmProgressWay.OnlyFirst || GG.Equip.Efem.LoadPort1.ProgressWay == EmProgressWay.OnlyLast ||
                GG.Equip.Efem.LoadPort2.ProgressWay == EmProgressWay.OnlyFirst || GG.Equip.Efem.LoadPort2.ProgressWay == EmProgressWay.OnlyLast || UserLastSlot;
            if (GG.Equip.CtrlSetting.ReviewJudgeMode)
            {
                GG.MEM_DIT.VirSetShort(CIMAW.YW_WAFER_UNLOAD_IS_LAST, (short)(isOnlyOne ? 1 : 0));
                // Joo // User 모드 + 아직 선택한 Wafer 검사중이니까 false로 여기 진행 안함
                if (isOnlyOne)
                {
                    List<string> slotInfo = new List<string>();
                    int isptCompleteCount = 0;
                    for (int slotNo = 1; slotNo <= 13; slotNo++)
                    {
                        WaferInfo temp = TransferDataMgr.GetWafer(new WaferInfoKey() { CstID = cstInfo.CstID, SlotNo = slotNo });
                        //해당 슬롯 웨이퍼가 없을 떄
                        if (temp.Status == EmEfemMappingInfo.Presence)
                        {
                            //검사 완료 확인
                            if (temp.IsInspComplete == true)
                            {
                                isptCompleteCount++;
                            }
                            if (temp.WaferID != "")
                            {
                                if (temp.IsInspComplete == true)
                                {
                                    slotInfo.Add(string.Format("{0},{1},OK", slotNo.ToString(), temp.WaferID));
                                }
                            }
                        }
                    }
                    GG.MEM_DIT.VirSetAscii(CIMAW.YW_CST_UNLOAD_CST_ID, cstInfo.CstID);
                    GG.MEM_DIT.VirSetShort(CIMAW.YW_CST_UNLOAD_PORT_ID, (short)cstInfo.LoadPortNo);
                    GG.MEM_DIT.VirSetAscii(CIMAW.YW_CST_UNLOAD_CUR_RECIPE, cstInfo.RecipeName);
                    GG.MEM_DIT.VirSetShort(CIMAW.YW_CST_UNLOAD_TOTAL_WAFER_COUNT, (short)cstInfo.SlotCount);
                    GG.MEM_DIT.VirSetShort(CIMAW.YW_CST_UNLOAD_INSPECTION_WAFER_COUNT, (short)isptCompleteCount);
                    GG.MEM_DIT.VirSetShort(CIMAW.YW_CST_UNLOAD_SLOT_INFO_COUNT, (short)slotInfo.Count);

                    // Joo // 검사 완료 된 Wafer Slot 1번부터 적어야됨
                    for (int i = 0; i < 13; i++)
                    {
                        if (i < slotInfo.Count)
                        {
                            GG.MEM_DIT.VirSetAscii(slotInfoPLCList[i], slotInfo[i]);
                        }
                        else
                        {
                            GG.MEM_DIT.VirSetAscii(slotInfoPLCList[i], string.Empty);
                        }
                    }
                    GG.MEM_DIT.VirSetAscii(CIMAW.YW_CST_UNLOAD_LOT_ID, cstInfo.LotID);
                }
            }
            else
            {
                // Joo // 항상 써줘야 됨, Defalut = 0
                GG.MEM_DIT.VirSetShort(CIMAW.YW_WAFER_UNLOAD_IS_LAST, 0);
            }

            waferInfo.Update();

            //Logger.CIMLog.AppendLine("WAFER UNLOAD REPORT [WAFER ID : {0}, CST ID : {1},  PORT ID : {2}, SLOT NO : {3}, START : {4} END : {5}"
            //    , waferInfo.WaferID, waferInfo.CstID, cstInfo.LoadPortNo.ToString(), waferInfo.StartTime.ToString("yyyyMMddhhmmss"), waferInfo.EndTime.ToString("yyyyMMddhhmmss"));
        }

        private void SetMemoryKerfData(WaferInfo waferInfo)
        {
            #region KerfData (과거 Data 삭제)
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH1_1, Math.Round(waferInfo.KerfDataCh1_1, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH1_2, Math.Round(waferInfo.KerfDataCh1_2, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH1_3, Math.Round(waferInfo.KerfDataCh1_3, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH1_4, Math.Round(waferInfo.KerfDataCh1_4, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH1_5, Math.Round(waferInfo.KerfDataCh1_5, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH1_6, Math.Round(waferInfo.KerfDataCh1_6, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH1_7, Math.Round(waferInfo.KerfDataCh1_7, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH1_8, Math.Round(waferInfo.KerfDataCh1_8, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH1_9, Math.Round(waferInfo.KerfDataCh1_9, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH1_10, Math.Round(waferInfo.KerfDataCh1_10, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH1_11, Math.Round(waferInfo.KerfDataCh1_11, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH1_12, Math.Round(waferInfo.KerfDataCh1_12, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH1_13, Math.Round(waferInfo.KerfDataCh1_13, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH1_14, Math.Round(waferInfo.KerfDataCh1_14, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH1_15, Math.Round(waferInfo.KerfDataCh1_15, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH2_1, Math.Round(waferInfo.KerfDataCh2_1, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH2_2, Math.Round(waferInfo.KerfDataCh2_2, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH2_3, Math.Round(waferInfo.KerfDataCh2_3, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH2_4, Math.Round(waferInfo.KerfDataCh2_4, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH2_5, Math.Round(waferInfo.KerfDataCh2_5, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH2_6, Math.Round(waferInfo.KerfDataCh2_6, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH2_7, Math.Round(waferInfo.KerfDataCh2_7, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH2_8, Math.Round(waferInfo.KerfDataCh2_8, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH2_9, Math.Round(waferInfo.KerfDataCh2_9, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH2_10, Math.Round(waferInfo.KerfDataCh2_10, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH2_11, Math.Round(waferInfo.KerfDataCh2_11, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH2_12, Math.Round(waferInfo.KerfDataCh2_12, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH2_13, Math.Round(waferInfo.KerfDataCh2_13, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH2_14, Math.Round(waferInfo.KerfDataCh2_14, 3).ToString());
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_CH2_15, Math.Round(waferInfo.KerfDataCh2_15, 3).ToString());
            #endregion

            #region Kerf Shift Data
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_1, Math.Round(waferInfo.KerfData_Right_Col_1, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_1, Math.Round(waferInfo.KerfData_Right_Row_1, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_1, Math.Round(waferInfo.KerfData_Right_1, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_2, Math.Round(waferInfo.KerfData_Right_Col_2, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_2, Math.Round(waferInfo.KerfData_Right_Row_2, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_2, Math.Round(waferInfo.KerfData_Right_2, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_3, Math.Round(waferInfo.KerfData_Right_Col_3, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_3, Math.Round(waferInfo.KerfData_Right_Row_3, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_3, Math.Round(waferInfo.KerfData_Right_3, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_4, Math.Round(waferInfo.KerfData_Right_Col_4, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_4, Math.Round(waferInfo.KerfData_Right_Row_4, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_4, Math.Round(waferInfo.KerfData_Right_4, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_5, Math.Round(waferInfo.KerfData_Right_Col_5, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_5, Math.Round(waferInfo.KerfData_Right_Row_5, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_5, Math.Round(waferInfo.KerfData_Right_5, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_6, Math.Round(waferInfo.KerfData_Right_Col_6, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_6, Math.Round(waferInfo.KerfData_Right_Row_6, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_6, Math.Round(waferInfo.KerfData_Right_6, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_7, Math.Round(waferInfo.KerfData_Right_Col_7, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_7, Math.Round(waferInfo.KerfData_Right_Row_7, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_7, Math.Round(waferInfo.KerfData_Right_7, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_8, Math.Round(waferInfo.KerfData_Right_Col_8, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_8, Math.Round(waferInfo.KerfData_Right_Row_8, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_8, Math.Round(waferInfo.KerfData_Right_8, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_9, Math.Round(waferInfo.KerfData_Right_Col_9, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_9, Math.Round(waferInfo.KerfData_Right_Row_9, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_9, Math.Round(waferInfo.KerfData_Right_9, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_10, Math.Round(waferInfo.KerfData_Right_Col_10, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_10, Math.Round(waferInfo.KerfData_Right_Row_10, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_10, Math.Round(waferInfo.KerfData_Right_10, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_11, Math.Round(waferInfo.KerfData_Right_Col_11, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_11, Math.Round(waferInfo.KerfData_Right_Row_11, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_11, Math.Round(waferInfo.KerfData_Right_11, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_12, Math.Round(waferInfo.KerfData_Right_Col_12, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_12, Math.Round(waferInfo.KerfData_Right_Row_12, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_12, Math.Round(waferInfo.KerfData_Right_12, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_13, Math.Round(waferInfo.KerfData_Right_Col_13, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_13, Math.Round(waferInfo.KerfData_Right_Row_13, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_13, Math.Round(waferInfo.KerfData_Right_13, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_14, Math.Round(waferInfo.KerfData_Right_Col_14, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_14, Math.Round(waferInfo.KerfData_Right_Row_14, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_14, Math.Round(waferInfo.KerfData_Right_14, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_15, Math.Round(waferInfo.KerfData_Right_Col_15, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_15, Math.Round(waferInfo.KerfData_Right_Row_15, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_15, Math.Round(waferInfo.KerfData_Right_15, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_16, Math.Round(waferInfo.KerfData_Right_Col_16, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_16, Math.Round(waferInfo.KerfData_Right_Row_16, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_16, Math.Round(waferInfo.KerfData_Right_16, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_17, Math.Round(waferInfo.KerfData_Right_Col_17, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_17, Math.Round(waferInfo.KerfData_Right_Row_17, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_17, Math.Round(waferInfo.KerfData_Right_17, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_18, Math.Round(waferInfo.KerfData_Right_Col_18, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_18, Math.Round(waferInfo.KerfData_Right_Row_18, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_18, Math.Round(waferInfo.KerfData_Right_18, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_19, Math.Round(waferInfo.KerfData_Right_Col_19, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_19, Math.Round(waferInfo.KerfData_Right_Row_19, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_19, Math.Round(waferInfo.KerfData_Right_19, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Col_20, Math.Round(waferInfo.KerfData_Right_Col_20, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_Row_20, Math.Round(waferInfo.KerfData_Right_Row_20, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Right_20, Math.Round(waferInfo.KerfData_Right_20, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_1, Math.Round(waferInfo.KerfData_Bottom_Col_1, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_1, Math.Round(waferInfo.KerfData_Bottom_Row_1, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_1, Math.Round(waferInfo.KerfData_Bottom_1, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_2, Math.Round(waferInfo.KerfData_Bottom_Col_2, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_2, Math.Round(waferInfo.KerfData_Bottom_Row_2, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_2, Math.Round(waferInfo.KerfData_Bottom_2, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_3, Math.Round(waferInfo.KerfData_Bottom_Col_3, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_3, Math.Round(waferInfo.KerfData_Bottom_Row_3, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_3, Math.Round(waferInfo.KerfData_Bottom_3, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_4, Math.Round(waferInfo.KerfData_Bottom_Col_4, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_4, Math.Round(waferInfo.KerfData_Bottom_Row_4, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_4, Math.Round(waferInfo.KerfData_Bottom_4, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_5, Math.Round(waferInfo.KerfData_Bottom_Col_5, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_5, Math.Round(waferInfo.KerfData_Bottom_Row_5, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_5, Math.Round(waferInfo.KerfData_Bottom_5, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_6, Math.Round(waferInfo.KerfData_Bottom_Col_6, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_6, Math.Round(waferInfo.KerfData_Bottom_Row_6, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_6, Math.Round(waferInfo.KerfData_Bottom_6, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_7, Math.Round(waferInfo.KerfData_Bottom_Col_7, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_7, Math.Round(waferInfo.KerfData_Bottom_Row_7, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_7, Math.Round(waferInfo.KerfData_Bottom_7, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_8, Math.Round(waferInfo.KerfData_Bottom_Col_8, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_8, Math.Round(waferInfo.KerfData_Bottom_Row_8, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_8, Math.Round(waferInfo.KerfData_Bottom_8, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_9, Math.Round(waferInfo.KerfData_Bottom_Col_9, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_9, Math.Round(waferInfo.KerfData_Bottom_Row_9, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_9, Math.Round(waferInfo.KerfData_Bottom_9, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_10, Math.Round(waferInfo.KerfData_Bottom_Col_10, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_10, Math.Round(waferInfo.KerfData_Bottom_Row_10, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_10, Math.Round(waferInfo.KerfData_Bottom_10, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_11, Math.Round(waferInfo.KerfData_Bottom_Col_11, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_11, Math.Round(waferInfo.KerfData_Bottom_Row_11, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_11, Math.Round(waferInfo.KerfData_Bottom_11, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_12, Math.Round(waferInfo.KerfData_Bottom_Col_12, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_12, Math.Round(waferInfo.KerfData_Bottom_Row_12, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_12, Math.Round(waferInfo.KerfData_Bottom_12, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_13, Math.Round(waferInfo.KerfData_Bottom_Col_13, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_13, Math.Round(waferInfo.KerfData_Bottom_Row_13, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_13, Math.Round(waferInfo.KerfData_Bottom_13, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_14, Math.Round(waferInfo.KerfData_Bottom_Col_14, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_14, Math.Round(waferInfo.KerfData_Bottom_Row_14, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_14, Math.Round(waferInfo.KerfData_Bottom_14, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_15, Math.Round(waferInfo.KerfData_Bottom_Col_15, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_15, Math.Round(waferInfo.KerfData_Bottom_Row_15, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_15, Math.Round(waferInfo.KerfData_Bottom_15, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_16, Math.Round(waferInfo.KerfData_Bottom_Col_16, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_16, Math.Round(waferInfo.KerfData_Bottom_Row_16, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_16, Math.Round(waferInfo.KerfData_Bottom_16, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_17, Math.Round(waferInfo.KerfData_Bottom_Col_17, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_17, Math.Round(waferInfo.KerfData_Bottom_Row_17, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_17, Math.Round(waferInfo.KerfData_Bottom_17, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_18, Math.Round(waferInfo.KerfData_Bottom_Col_18, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_18, Math.Round(waferInfo.KerfData_Bottom_Row_18, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_18, Math.Round(waferInfo.KerfData_Bottom_18, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_19, Math.Round(waferInfo.KerfData_Bottom_Col_19, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_19, Math.Round(waferInfo.KerfData_Bottom_Row_19, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_19, Math.Round(waferInfo.KerfData_Bottom_19, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Col_20, Math.Round(waferInfo.KerfData_Bottom_Col_20, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_Row_20, Math.Round(waferInfo.KerfData_Bottom_Row_20, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KerfData_Bottom_20, Math.Round(waferInfo.KerfData_Bottom_20, 3).ToString());
            #endregion

            #region Kerf Width Data Ctrl->CIM
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_01_DIE_COL, Math.Round(waferInfo.KerfData_01_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_01_DIE_ROW, Math.Round(waferInfo.KerfData_01_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_01_WIDTH_TOP, Math.Round(waferInfo.KerfData_01_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_01_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_01_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_01_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_01_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_01_WIDTH_LEFT, Math.Round(waferInfo.KerfData_01_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_02_DIE_COL, Math.Round(waferInfo.KerfData_02_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_02_DIE_ROW, Math.Round(waferInfo.KerfData_02_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_02_WIDTH_TOP, Math.Round(waferInfo.KerfData_02_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_02_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_02_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_02_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_02_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_02_WIDTH_LEFT, Math.Round(waferInfo.KerfData_02_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_03_DIE_COL, Math.Round(waferInfo.KerfData_03_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_03_DIE_ROW, Math.Round(waferInfo.KerfData_03_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_03_WIDTH_TOP, Math.Round(waferInfo.KerfData_03_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_03_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_03_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_03_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_03_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_03_WIDTH_LEFT, Math.Round(waferInfo.KerfData_03_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_04_DIE_COL, Math.Round(waferInfo.KerfData_04_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_04_DIE_ROW, Math.Round(waferInfo.KerfData_04_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_04_WIDTH_TOP, Math.Round(waferInfo.KerfData_04_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_04_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_04_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_04_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_04_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_04_WIDTH_LEFT, Math.Round(waferInfo.KerfData_04_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_05_DIE_COL, Math.Round(waferInfo.KerfData_05_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_05_DIE_ROW, Math.Round(waferInfo.KerfData_05_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_05_WIDTH_TOP, Math.Round(waferInfo.KerfData_05_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_05_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_05_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_05_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_05_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_05_WIDTH_LEFT, Math.Round(waferInfo.KerfData_05_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_06_DIE_COL, Math.Round(waferInfo.KerfData_06_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_06_DIE_ROW, Math.Round(waferInfo.KerfData_06_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_06_WIDTH_TOP, Math.Round(waferInfo.KerfData_06_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_06_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_06_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_06_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_06_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_06_WIDTH_LEFT, Math.Round(waferInfo.KerfData_06_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_07_DIE_COL, Math.Round(waferInfo.KerfData_07_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_07_DIE_ROW, Math.Round(waferInfo.KerfData_07_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_07_WIDTH_TOP, Math.Round(waferInfo.KerfData_07_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_07_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_07_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_07_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_07_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_07_WIDTH_LEFT, Math.Round(waferInfo.KerfData_07_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_08_DIE_COL, Math.Round(waferInfo.KerfData_08_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_08_DIE_ROW, Math.Round(waferInfo.KerfData_08_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_08_WIDTH_TOP, Math.Round(waferInfo.KerfData_08_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_08_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_08_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_08_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_08_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_08_WIDTH_LEFT, Math.Round(waferInfo.KerfData_08_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_09_DIE_COL, Math.Round(waferInfo.KerfData_09_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_09_DIE_ROW, Math.Round(waferInfo.KerfData_09_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_09_WIDTH_TOP, Math.Round(waferInfo.KerfData_09_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_09_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_09_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_09_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_09_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_09_WIDTH_LEFT, Math.Round(waferInfo.KerfData_09_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_10_DIE_COL, Math.Round(waferInfo.KerfData_10_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_10_DIE_ROW, Math.Round(waferInfo.KerfData_10_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_10_WIDTH_TOP, Math.Round(waferInfo.KerfData_10_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_10_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_10_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_10_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_10_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_10_WIDTH_LEFT, Math.Round(waferInfo.KerfData_10_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_11_DIE_COL, Math.Round(waferInfo.KerfData_11_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_11_DIE_ROW, Math.Round(waferInfo.KerfData_11_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_11_WIDTH_TOP, Math.Round(waferInfo.KerfData_11_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_11_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_11_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_11_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_11_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_11_WIDTH_LEFT, Math.Round(waferInfo.KerfData_11_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_12_DIE_COL, Math.Round(waferInfo.KerfData_12_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_12_DIE_ROW, Math.Round(waferInfo.KerfData_12_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_12_WIDTH_TOP, Math.Round(waferInfo.KerfData_12_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_12_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_12_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_12_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_12_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_12_WIDTH_LEFT, Math.Round(waferInfo.KerfData_12_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_13_DIE_COL, Math.Round(waferInfo.KerfData_13_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_13_DIE_ROW, Math.Round(waferInfo.KerfData_13_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_13_WIDTH_TOP, Math.Round(waferInfo.KerfData_13_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_13_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_13_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_13_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_13_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_13_WIDTH_LEFT, Math.Round(waferInfo.KerfData_13_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_14_DIE_COL, Math.Round(waferInfo.KerfData_14_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_14_DIE_ROW, Math.Round(waferInfo.KerfData_14_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_14_WIDTH_TOP, Math.Round(waferInfo.KerfData_14_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_14_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_14_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_14_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_14_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_14_WIDTH_LEFT, Math.Round(waferInfo.KerfData_14_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_15_DIE_COL, Math.Round(waferInfo.KerfData_15_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_15_DIE_ROW, Math.Round(waferInfo.KerfData_15_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_15_WIDTH_TOP, Math.Round(waferInfo.KerfData_15_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_15_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_15_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_15_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_15_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_15_WIDTH_LEFT, Math.Round(waferInfo.KerfData_15_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_16_DIE_COL, Math.Round(waferInfo.KerfData_16_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_16_DIE_ROW, Math.Round(waferInfo.KerfData_16_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_16_WIDTH_TOP, Math.Round(waferInfo.KerfData_16_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_16_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_16_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_16_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_16_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_16_WIDTH_LEFT, Math.Round(waferInfo.KerfData_16_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_17_DIE_COL, Math.Round(waferInfo.KerfData_17_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_17_DIE_ROW, Math.Round(waferInfo.KerfData_17_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_17_WIDTH_TOP, Math.Round(waferInfo.KerfData_17_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_17_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_17_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_17_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_17_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_17_WIDTH_LEFT, Math.Round(waferInfo.KerfData_17_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_18_DIE_COL, Math.Round(waferInfo.KerfData_18_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_18_DIE_ROW, Math.Round(waferInfo.KerfData_18_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_18_WIDTH_TOP, Math.Round(waferInfo.KerfData_18_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_18_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_18_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_18_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_18_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_18_WIDTH_LEFT, Math.Round(waferInfo.KerfData_18_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_19_DIE_COL, Math.Round(waferInfo.KerfData_19_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_19_DIE_ROW, Math.Round(waferInfo.KerfData_19_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_19_WIDTH_TOP, Math.Round(waferInfo.KerfData_19_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_19_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_19_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_19_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_19_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_19_WIDTH_LEFT, Math.Round(waferInfo.KerfData_19_WIDTH_LEFT, 3).ToString());

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_20_DIE_COL, Math.Round(waferInfo.KerfData_20_DIE_COL, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_20_DIE_ROW, Math.Round(waferInfo.KerfData_20_DIE_ROW, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_20_WIDTH_TOP, Math.Round(waferInfo.KerfData_20_WIDTH_TOP, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_20_WIDTH_RIGHT, Math.Round(waferInfo.KerfData_20_WIDTH_RIGHT, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_20_WIDTH_BOTTOM, Math.Round(waferInfo.KerfData_20_WIDTH_BOTTOM, 3).ToString());
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_KERF_DATA_20_WIDTH_LEFT, Math.Round(waferInfo.KerfData_20_WIDTH_LEFT, 3).ToString());
            #endregion
        }

        private void OnCassetteUnload(Equipment equip, HsmsPcCmd cmd)
        {
            IsCstLoadConfirmOK = false;
            IsCstLoadIFComplete = false;
            IsCstMoveOutIFComplete = false;
            IsCstMoveOutOK = false;

            CassetteInfoKey key = (CassetteInfoKey)cmd.Tag;
            WaferInfoKey _key = GG.Equip.Efem.Robot.WaferKeyForCSTUldReport;

            CassetteInfo cstInfo = TransferDataMgr.GetCst(key);
            WaferInfo wfInfo = TransferDataMgr.GetWafer(_key);

            if(_key != null)
                Logger.CIMLog.AppendLine("CST UNLOAD REPORT [CST ID : {0}, LOT ID : {1}]", cstInfo.CstID, wfInfo.WaferID);

            List<string> slotInfo = new List<string>();
            int isptCompleteCount = 0;
            for (int slotNo = 1; slotNo <= 13; slotNo++)
            {
                WaferInfo temp = TransferDataMgr.GetWafer(new WaferInfoKey() { CstID = cstInfo.CstID, SlotNo = slotNo });
                //해당 슬롯 웨이퍼가 없을 떄
                if (temp.Status == EmEfemMappingInfo.Presence)
                {
                    //검사 완료 확인
                    if (temp.IsInspComplete == true)
                    {
                        isptCompleteCount++;
                    }
                    if(temp.WaferID != "")
                    {
                        if(temp.IsInspComplete == true)
                        {
                            slotInfo.Add(string.Format("{0},{1},OK", slotNo.ToString(), temp.WaferID));
                        }
                    }
                }
            }
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_CST_UNLOAD_CST_ID, cstInfo.CstID);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_CST_UNLOAD_PORT_ID, (short)cstInfo.LoadPortNo);
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_CST_UNLOAD_CUR_RECIPE, cstInfo.RecipeName);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_CST_UNLOAD_TOTAL_WAFER_COUNT, (short)cstInfo.SlotCount);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_CST_UNLOAD_INSPECTION_WAFER_COUNT, (short)isptCompleteCount);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_CST_UNLOAD_SLOT_INFO_COUNT, (short)slotInfo.Count);
            for (int i = 0; i < 13; i++)
            {
                if(i < slotInfo.Count)
                {
                    GG.MEM_DIT.VirSetAscii(slotInfoPLCList[i], slotInfo[i]);
                }
                else
                {
                    GG.MEM_DIT.VirSetAscii(slotInfoPLCList[i], string.Empty);
                }

            }
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_CST_UNLOAD_LOT_ID, wfInfo.LotID);
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_CST_UNLOAD_LOT_ID, cstInfo.LotID);
        }

        public void OnAlarmReport(Equipment equip, HsmsPcCmd cmd)
        {
            HsmsAlarmInfo info = cmd.Tag as HsmsAlarmInfo;
            if (info == null)
            {
                info = new HsmsAlarmInfo();
                info.IsSet = false;
                info.ID = 0;
                info.Desc = "Error - AlarmInfo is null";
            }
            short _isSet = info.IsSet == true ? (short)1 : (short)130;
            GG.MEM_DIT.VirSetShort(CIMAW.YW_ALARM_FLAG, _isSet);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_ALARM_ID, (short)info.ID);
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_ALARM_TEXT, info.Desc);
        }
        private void OnPortRecipeSelect(Equipment equip, HsmsPcCmd cmd)
        {
            HsmsRecipeInfo info = cmd.Tag as HsmsRecipeInfo;

            GG.MEM_DIT.VirSetShort(CIMAW.YW_RECIPE_MODE, (short)info.RecipeMode);
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_RECIPE_ID, info.RecipeID);

            GG.MEM_DIT.VirSetShort(CIMAW.YW_ACK_PORT_RECIPE_SELECT, (short)info.IsOK);
        }
        private void OnChangeEquipStatus(Equipment equip, HsmsPcCmd cmd)
        {
            HsmsEqpStatusInfo info = cmd.Tag as HsmsEqpStatusInfo;

            GG.MEM_DIT.VirSetShort(CIMAW.YW_EQP_STATUS, (byte)info.EqpStatus);
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_EQP_STATUS_CURRENT_RECIPE, info.CurRecipe);
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_EQP_STATUS_CST_ID, info.CstID);
        }
        private void OnChageOHTMode(Equipment equip, HsmsPcCmd cmd)
        {
            HsmsChangeOHTModeInfo info = (HsmsChangeOHTModeInfo)cmd.Tag;

            GG.MEM_DIT.VirSetShort(CIMAW.YW_OHT_MODE_AUTO_MODE, (short)info.Mode);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_OHT_MODE_LOAD1_STATUS, info.LPM1IsExist == true ? (short)1 : (short)0);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_OHT_MODE_UNLOAD1_STATUS, info.LPM1IsExist == true ? (short)1 : (short)0);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_OHT_MODE_LOAD2_STATUS, info.LPM2IsExist == true ? (short)1 : (short)0);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_OHT_MODE_UNLOAD2_STATUS, info.LPM2IsExist == true ? (short)1 : (short)0);
        }
        private void OnPortModeChange(Equipment equip, HsmsPcCmd cmd)
        {
            HsmsPortInfo info = cmd.Tag as HsmsPortInfo;

            GG.MEM_DIT.VirSetShort(CIMAW.YW_PORT_REQEST_NO, (short)info.LoadportNo);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_PORT_REQEST_MODE, (short)info.PortMode);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_PORT_REQEST_STATUS, info.IsCstExist == true ? (short)1 : (short)0);
            GG.MEM_DIT.VirSetAscii(CIMAW.YW_PORT_REQEST_CST_ID, info.CstID);

            Logger.CIMLog.AppendLine(LogLevel.Info, "PORT MODE CHANGE | PORT : {0} / MODE : {1}", info.LoadportNo.ToString(), info.PortMode.ToString());
        }
        private void OnEqpModeChange(Equipment equip, HsmsPcCmd cmd)
        {
            EmHsmsEqpMode eqpMode = (EmHsmsEqpMode)cmd.Tag;

            GG.MEM_DIT.VirSetShort(CIMAW.YW_EQP_MODE_CHANGE, (short)eqpMode);
        }

        private void OnCtrlModeChange(Equipment equip, HsmsPcCmd cmd)
        {
            EmHsmsCtrlMode mode = (EmHsmsCtrlMode)cmd.Tag;

            GG.MEM_DIT.VirSetShort(CIMAW.YW_EQP_MODE_CHANGE_CTRL_MODE, (short)mode);
        }
        private void OnCtrlModeChangeResult(Equipment equip, HsmsPcCmd cmd)
        {
            EmHsmsCtrlAck result = (EmHsmsCtrlAck)GG.MEM_DIT.VirGetShort(CIMAW.XW_ACK).GetByte(0);

            switch (result)
            {
                case EmHsmsCtrlAck.OK:
                    break;
                case EmHsmsCtrlAck.OVERLAP:
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "<CIM Mode 变更失败>" : "<CIM Mode 변경 실패>", GG.boChinaLanguage ? "请求变更为同样 Mode" : "동일한 모드로 변경 요청");
                    break;
                case EmHsmsCtrlAck.NOT_GEM_DEFINE:
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "<CIM Mode 变更失败>" : "<CIM Mode 변경 실패>", GG.boChinaLanguage ? "上流 GEM DEFINE没有下达的状态下，邀请变更" : "상위 GEM DEFINE이 내려오지 않은 상태에서 변경 요청");
                    break;
                case EmHsmsCtrlAck.NOT_VALUE:
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "<CIM Mode 变更失败>" : "<CIM Mode 변경 실패>", GG.boChinaLanguage ? "请求变更为不正确的 CTRL Mode" : "올바르지 않은 CTRL Mode로 변경 요청");
                    break;
                case EmHsmsCtrlAck.TERMINAL_MSG:
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "<CIM Mode 变更失败>" : "<CIM Mode 변경 실패>", GG.boChinaLanguage ? "请关掉 CIM Program Terminal Message，再进行尝试" : "CIM Program 터미널 메시지를 닫고 재시도 해주세요");
                    break;
            }
            Logger.CIMLog.AppendLine(LogLevel.Info, "CIM Mode 변경 요청 {0}", result.ToString());
        }
        private void OsVersionReport(Equipment equip, HsmsPcCmd cmd)
        {
            //string osVersion = cmd.Tag as string;
            //if (osVersion == null)
            //{
            //    osVersion = "No Data";
            //}
            //GG.MEM_DIT.VirSetAscii(CIMAW.YW_OS_VERSION, osVersion);

            GG.MEM_DIT.VirSetAscii(CIMAW.YW_OS_VERSION, "OSVERSION");
        }
        private void OnECIDChange(Equipment equip, HsmsPcCmd cmd)
        {
            EmHsmsAck ack = (EmHsmsAck)cmd.Tag;
            equip.WriteEcidFile();
        }

        #endregion
        #region OnEvent
        public void OnCstMapEvent(Equipment equip, HsmsPcEvent evt)
        {
            string confirmFlag = GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_CST_MAP_CONFIRM_FLAG);

            string CstID = GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_CST_MAP_CST_ID);
            string _lotID = GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_CST_MAP_LOT_ID);

            string _waferList = GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_CST_MAP_WAFER_ID_LIST);
            
            //if(equip.Efem.LoadPort1.LowerWaferKey.CstID == CstID)
            //{
            //    CassetteInfo info = TransferDataMgr.GetCst(equip.Efem.LoadPort1.LowerWaferKey.CstID);
            //    info.LotID = _lotID;
            //    info.Update();
            //}
            //else if (equip.Efem.LoadPort2.LowerWaferKey.CstID == CstID)
            //{
            //    CassetteInfo info = TransferDataMgr.GetCst(equip.Efem.LoadPort2.LowerWaferKey.CstID);
            //    info.LotID = _lotID;
            //    info.Update();
            //}

            if (confirmFlag == "T")
            {
                Logger.CIMLog.AppendLine(LogLevel.Info, "CstMap Confirm OK");
                if (_waferList.Length < 1)
                {
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0561_CST_MAP_WAFER_LIST_DATA_EMPTY);
                }
                IsCstLoadConfirmOK = true;
            }
            else
            {
                Logger.CIMLog.AppendLine(LogLevel.Info, "CstMap Confirm NG");
                IsCstLoadConfirmOK = false;
            }
            IsCstLoadIFComplete = true;
        }
        private void OnPPSelectEvent(Equipment equip, HsmsPcEvent evt)
        {
            short PortNo = GG.MEM_DIT.VirGetShort(CIMAW.XW_PORT_RECIPE_SELECT_PORT_NO);
            PortNo = PortNo.GetByte(0);
            string RecipeName = GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_PORT_RECIPE_SELECT_RECIPE_NAME);

            Logger.CIMLog.AppendLine("[PP SELECT EVENT RECV] Port No : {0}   Recipe Name : {1}", PortNo.ToString(), RecipeName);

            string RecipeAck = GG.MEM_DIT.VirGetAscii(CIMAW.XW_PORT_RECIPE_SELECT_RECIPE_CONFIRM);
            if(RecipeAck == "T")
            {
                if (GG.Equip.ChangeRecipe((int)PortNo, RecipeName))
                {
                    IsPPSelectOK = true;
                }
            }
            else
            {
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0563_NO_RECIPE_FROM_PP_SELECT_RECIPE_RECV);
                IsPPSelectOK = false;
            }
        }
        private void OnCstStartCommandBeforeAck(Equipment equip, HsmsPcEvent evt)
        {
            string result = GG.MEM_DIT.VirGetAsciiTrim(CIMAW.YW_ACK_RCMD_ACK);

            IsCstStartCommand = true;
        }
        private void OnLotStartEvent(Equipment equip, HsmsPcEvent evt)
        {
            IsLotStartIFComplete = false;
            IsLotStartConfirmOK = false;

            string result = GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_LOT_START_CONFIRM_FLAG);

            if(result.Equals("T"))
            {
                IsLotStartConfirmOK = true;
            }
            else
            {
                IsLotStartConfirmOK = false;
            }
            IsLotStartIFComplete = true;
        }
        private void OnWaferStartEvent(Equipment equip, HsmsPcEvent evt)
        {
            string res = GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_WAFER_START_WAFER_ID);
            string result = GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_WAFER_START_CONFIRM_FLAG);

            if (result.Equals("T"))
            {
                IsWaferStartConfirmOK = true;
            }
            else if(result.Equals("N"))
            {
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0564_WAFER_START_REPORT_ID_IS_NOT_MATCH_WITH_HOST_RECV);
                IsWaferStartConfirmOK = false;
            }
            else
            {
                IsWaferStartConfirmOK = false;
            }
            IsWaferStartIFComplete = true;
        }
        private void OnWaferMapEvent(Equipment equip, HsmsPcEvent evt)
        {
            WaferInfo info = TransferDataMgr.GetWafer(equip.Efem.Aligner.LowerWaferKey);

            info.WaferID = info.WaferID.Substring(0, 10);
            string _lotID = GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_WAFER_MAP_LOT_ID);
            if(_lotID == string.Empty || string.IsNullOrEmpty(_lotID))
            {
                CassetteInfo cst = TransferDataMgr.GetCst(info.CstID);
                Logger.Log.AppendLine(LogLevel.Info, string.Format("WAFER MAP 수신 시 LOT ID 비워져 있어 CST CONFIRM때 내려온 LOT ID로 셋팅 진행 / READ LOT ID : {0}", _lotID));
                info.LotID = cst.LotID;
            }
            else
            {
                info.LotID = _lotID;
            }
            
            info.IDType = (EmWaferIDType)GG.MEM_DIT.VirGetShort(CIMAW.XW_WAFER_MAP_ID_TYPE).GetByte(0);
            info.CellColCount = GG.MEM_DIT.VirGetShort(CIMAW.XW_WAFER_MAP_COLUMNS).GetByte(0);
            info.CellRowCount = GG.MEM_DIT.VirGetShort(CIMAW.XW_WAFER_MAP_ROWS).GetByte(0);
            info.FloatZone = GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_WAFER_MAP_FLAT_ZONE);

            info.Update();

            IsWaferMapRequestIFComplete = true;
        }
        private void OnRcmdBeforeAck(Equipment equip, HsmsPcEvent evt)
        {
            short data = GG.MEM_DIT.VirGetShort(CIMAW.XW_RCMD_RCMD);
            bool result = false;
            //Start
            if (data == 1)
            {
                if (GG.Equip.EquipRunMode == EmEquipRunMode.Auto)
                {
                    result = true;
                    IsCstStartCommand = true;
                }
                else
                {
                    if (GG.Equip.ChangeRunMode(EmEquipRunMode.Auto, true))
                        result = false;
                }
            }
            //Restart
            else if (data == 2)
            {
                if (GG.Equip.Pause())
                    result = true;
            }
            //Stop
            else if (data == 3)
            {
                if (GG.Equip.EquipRunMode == EmEquipRunMode.Manual)
                    result = true;
                if (GG.Equip.ChangeRunMode(EmEquipRunMode.Manual, true))
                    result = true;
            }
            //Pause
            else if (data == 4)
            {
                if (GG.Equip.Pause())
                    result = true;
            }
            //Start Command
            else if(data == 5)
            {
                IsCstStartCommand = true;
            }

            short resultShort = result == true ? (short)0 : (short)2;

            GG.MEM_DIT.VirSetShort(CIMAW.YW_ACK_RCMD_MODE, data);
            GG.MEM_DIT.VirSetShort(CIMAW.YW_ACK_RCMD_ACK, resultShort);
        }
        public void OnEcidEditEvent(Equipment equip, HsmsPcEvent evt)
        {
            //1.Host에서 변경보고 된 내용을 적는다.				ConfigFile / HOST / ECID / ECID.ini 파일
            //2.Host "ECID Change Report"--->Bit On
            //3. 1번 파일의 항목을 확인, 변경한다
            //4. ECID_CHANGE_CMD 명령 보낸다? - 불확실
            if (equip.EquipRunMode == EmEquipRunMode.Manual)
            {
                if (equip.HostEcidSetting.Load(equip.CtrlSetting.Hsms.HostECIDSavePath) == false) return;
                equip.StartMotorSaveAndWait();
                equip.PopupTimerMsg?.Invoke(3, "Host Command", "Change ECID by Host");
            }
        }
        public void OnOHTModeChangeBeforeAck(Equipment equip, HsmsPcEvent evt)
        {
            short mode = GG.MEM_DIT.VirGetShort(CIMAW.XW_OHT_MODE_CHANGE_MODE).GetByte(0);
            short portNo = GG.MEM_DIT.VirGetShort(CIMAW.XW_OHT_MODE_CHANGE_PORT_NO).GetByte(0);


            EmHsmsAck result = equip.SetLpmLoadType((EmEfemPort)portNo, (EmLoadType)mode);

            GG.MEM_DIT.VirSetShort(CIMAW.YW_ACK_OHT_CHANGE_ACK, (short)result);
        }
        public void OnCtrlStateChangeEvent(Equipment equip, HsmsPcEvent evt)
        {
            short result = GG.MEM_DIT.VirGetShort(CIMAW.XW_CTRL_MODE_CHANGE_STATE);
            result = result.GetByte(0);

            bool _changeResult = false;

            if(GG.Equip.Efem.LoadPort1.LoadType == EmLoadType.OHT && (EmCimMode)result != EmCimMode.Remote)
            {
                _changeResult |= GG.Equip.SetOHTMode(EmLoadType.Manual, false);
            }
            _changeResult |= GG.Equip.SetCimMode((EmCimMode)result);

            Logger.CIMLog.AppendLine(LogLevel.Info, string.Format("CIM Mode [{0}] 변경 {1}!", ((EmCimMode)result).ToString(), _changeResult == true ? "성공" : "실패"));
        }
        public void OnCstMoveOutFlagEvent(Equipment equip, HsmsPcEvent evt)
        {
            string lotID = GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_MOVE_OUT_FLAG_LOT_ID);
            string flag = GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_MOVE_OUT_FLAG_CONFIRM_FLAG);

            if(flag.Equals("Y"))
            {
                IsCstMoveOutOK = true;
            }
            else
            {
                IsCstMoveOutOK = false;
            }
            Logger.CIMLog.AppendLine(LogLevel.Info, string.Format("MOVE OUT Flag : [{0}] 수신 ", flag));

            IsCstMoveOutIFComplete = true;
        }

        public void OnTerminalMsgEvent(Equipment equip, HsmsPcEvent evt)
        {
            int msgCount = (int)CIMAW.XW_TerminalMsgCount.vByte;
            StringBuilder msg = new StringBuilder();
            if (msgCount < 1 || msgCount > 10)
            {
                msg.AppendFormat("Terminal Msg Count Error (Count:{0})", msgCount);
            }
            else
            {
                for (int i = 0; i < msgCount; ++i)
                    msg.AppendLine(TerminalMsgs[i].vAscii.Trim('\0'));
            }
            GG.Equip.TerminalMsgObserver.Add(new TerminalMsgItem(DateTime.Now, msg.ToString()));
        }

        public void OnDeepLearningReviewComplete(Equipment equip, HsmsPcEvent evt)
        {
            string cstID = GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_DEEP_LEARNING_REVIEW_CST_ID);
            //int slot = CIMAW.XW_DEEP_LEARNING_REVIEW_SLOT.vShort;

            Logger.CIMLog.AppendLine(LogLevel.Info, "DEEP Learning Complete Event Recv [READ CST ID : {0}]", cstID);
            if(GG.Equip.Efem.LoadPort1.SeqStepNum == Detail.EFEM.Step.EmEFEMLPMSeqStep.S685_DEEP_LEARNING_REVIEW_WAIT && cstID == GG.Equip.Efem.LoadPort1.CstKey.ID)
            {
                cstID = GG.Equip.Efem.LoadPort1.CstKey.ID;
                Logger.CIMLog.AppendLine(LogLevel.Info, "DEEP Learning Complete LP1, CST ID : {0}", cstID);
                var wafers = TransferDataMgr.GetWafers(cstID);
                if(wafers != null)
                {
                    foreach (var item in wafers)
                    {
                        item.IsDeepLearningReviewComplete = true;
                        item.Update();
                    }
                    Logger.CIMLog.AppendLine(LogLevel.Info, "DEEP Learning Complete LP1, IsDeepLearning Complete true");
                }
                GG.Equip.Efem.LoadPort1.PassDeepLearningReview = true;
            }
            else if(GG.Equip.Efem.LoadPort2.SeqStepNum == Detail.EFEM.Step.EmEFEMLPMSeqStep.S685_DEEP_LEARNING_REVIEW_WAIT && cstID == GG.Equip.Efem.LoadPort2.CstKey.ID)
            {
                cstID = GG.Equip.Efem.LoadPort2.CstKey.ID;
                Logger.CIMLog.AppendLine(LogLevel.Info, "DEEP Learning Complete LP2, CST ID : {0}", cstID);
                var wafers = TransferDataMgr.GetWafers(cstID);
                if (wafers != null)
                {
                    foreach (var item in wafers)
                    {
                        item.IsDeepLearningReviewComplete = true;
                        item.Update();
                    }
                    Logger.CIMLog.AppendLine(LogLevel.Info, "DEEP Learning Complete LP2, IsDeepLearning Complete true");
                }
                GG.Equip.Efem.LoadPort2.PassDeepLearningReview = true;
            }
            else
            {
                if(GG.Equip.Efem.LoadPort1.SeqStepNum == Detail.EFEM.Step.EmEFEMLPMSeqStep.S685_DEEP_LEARNING_REVIEW_WAIT)
                {
                    cstID = GG.Equip.Efem.LoadPort1.CstKey.ID;
                    Logger.CIMLog.AppendLine(LogLevel.Info, "CST ID 비어있음... DEEP Learning Complete LP1, CST ID : {0}", cstID);
                    var wafers = TransferDataMgr.GetWafers(cstID);
                    if (wafers != null)
                    {
                        foreach (var item in wafers)
                        {
                            item.IsDeepLearningReviewComplete = true;
                            item.Update();
                        }
                        Logger.CIMLog.AppendLine(LogLevel.Info, "DEEP Learning Complete LP1, IsDeepLearning Complete true");
                    }
                    GG.Equip.Efem.LoadPort1.PassDeepLearningReview = true;
                }
                else if(GG.Equip.Efem.LoadPort2.SeqStepNum == Detail.EFEM.Step.EmEFEMLPMSeqStep.S685_DEEP_LEARNING_REVIEW_WAIT)
                {
                    cstID = GG.Equip.Efem.LoadPort2.CstKey.ID;
                    Logger.CIMLog.AppendLine(LogLevel.Info, "CST ID 비어있음... DEEP Learning Complete LP2, CST ID : {0}", cstID);
                    var wafers = TransferDataMgr.GetWafers(cstID);
                    if (wafers != null)
                    {
                        foreach (var item in wafers)
                        {
                            item.IsDeepLearningReviewComplete = true;
                            item.Update();
                        }
                        Logger.CIMLog.AppendLine(LogLevel.Info, "DEEP Learning Complete LP2, IsDeepLearning Complete true");
                    }
                    GG.Equip.Efem.LoadPort2.PassDeepLearningReview = true;
                }
            }

            var cst = TransferDataMgr.GetCst(cstID);
            if (cst == null)
                Logger.CIMLog.AppendLine(LogLevel.Error, "DeepLearningReviewComplete (cstId : {0}) - No Cst Info (DB)", cstID);
            else
            {
                var wafer = TransferDataMgr.GetWafers(cstID);
                if (wafer == null)
                    Logger.CIMLog.AppendLine(LogLevel.Error, "DeepLearningReviewComplete (cstId : {0}) - No Wafer Info (DB)", cstID);
                else
                {
                    foreach (var item in wafer)
                    {
                        item.IsDeepLearningReviewComplete = true;
                        item.Update();
                    }                       
                }
            }
        }

        // Joo // Auto MoveOut , Review (Event 추가)
        // Joo // Server에서 Move Out 또는 Review 결과 나오는 시간 = 1분 미만
        // Joo // 22.12.27 Auto MoveOut, Review 말고도 CST ID까지 보내주는걸로 협의 (오정석J)
        public void OnAutoMoveOutEvent(Equipment equip, HsmsPcEvent evt)
        {
            // Joo // LPM1 딥러닝 Review 결과가 나오기 전에, LPM2 딥러닝 Review 결과가 나오면 예외 발생
            if (GG.Equip.Efem.LoadPort1.CSTID_Clone == GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_WAFER_AUTO_MOVE_OUT_CST_ID))
            {
                GG.Equip.Efem.LoadPort1.CSTID_Clone = "";
                GG.Equip.Efem.LoadPort1.DeepLearningReviewJudge = GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_WAFER_AUTO_MOVE_OUT).Substring(0, 1);
            }
            else
            {
                Logger.CIMLog.AppendLine(LogLevel.Warning, string.Format("[Auto Move Out Event] CST ID가 LPM1이랑 일치하지 않음 CST ID: {0} , LPM Clone ID: {1}", GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_WAFER_AUTO_MOVE_OUT_CST_ID), GG.Equip.Efem.LoadPort1.CSTID_Clone));
            }

            if (GG.Equip.Efem.LoadPort2.CSTID_Clone == GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_WAFER_AUTO_MOVE_OUT_CST_ID))
            {
                GG.Equip.Efem.LoadPort2.CSTID_Clone = "";
                GG.Equip.Efem.LoadPort2.DeepLearningReviewJudge = GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_WAFER_AUTO_MOVE_OUT).Substring(0, 1);
            }
            else
            {
                Logger.CIMLog.AppendLine(LogLevel.Warning, string.Format("[Auto Move Out Event] CST ID가 LPM2이랑 일치하지 않음 CST ID: {0} , LPM Clone ID: {1}", GG.MEM_DIT.VirGetAsciiTrim(CIMAW.XW_WAFER_AUTO_MOVE_OUT_CST_ID), GG.Equip.Efem.LoadPort2.CSTID_Clone));
            }
        }
        #endregion

        public Queue<HsmsAlarmInfo> _alarmReportQ = new Queue<HsmsAlarmInfo>();
        public void AlarmReport(HsmsAlarmInfo info)
        {
            _alarmReportQ.Enqueue(info);
            Logger.CIMLog.AppendLine(LogLevel.Error, "Alarm Enqueue {0}.{1}.{2}", info.IsSet ? "HAPPEN" : "RESET", info.ID, info.Desc);
        }

        PlcTimer _alarmDelay = new PlcTimer("Alarm Report Delay");
        public void ProcessingAlarmReport(Equipment equip)
        {
            
            if (_alarmReportQ.Count > 0
                && IsCommandAck(EmHsmsPcCommand.ALARM_REPORT) == true)
            {
                if (_alarmDelay)
                {
                    _alarmDelay.Stop();
                    HsmsAlarmInfo alarmInfo = _alarmReportQ.Dequeue();
                    StartCommand(equip, EmHsmsPcCommand.ALARM_REPORT, alarmInfo);
                }
                else if (_alarmDelay.IsStart == false)
                {
                    _alarmDelay.Start(0, 50);
                }
            }

            if(LstCmd[(int)EmHsmsPcCommand.ALARM_REPORT].Step == 0 && _alarmReportQ.Count == 0)
            {
                CIMAB.YB_ALARM_EXIST_REPORT.vBit = AlarmMgr.Instance.LstlogAlarms[0].Items.Count > 0;
            }
        }
        private Queue<HsmsRecipeInfo> _recipeReportQ = new Queue<HsmsRecipeInfo>();
        public void RecipeReport(HsmsRecipeInfo info)
        {
            _recipeReportQ.Enqueue(info);
        }
        private void RecipeReportLogic(Equipment equip)
        {
            if (_recipeReportQ.Count > 0
                   && IsCommandAck(EmHsmsPcCommand.RECIPE_SELECT) == true && LstCmd[(int)EmHsmsPcCommand.RECIPE_SELECT].Step == 0)
            {
                HsmsRecipeInfo recipeInfo = _recipeReportQ.Dequeue();
                StartCommand(equip, EmHsmsPcCommand.RECIPE_SELECT, recipeInfo);
            }
        }
        private DateTime _hsmsPlcAliveDateTime = DateTime.Now;
        private DateTime _lastestSendAlive = DateTime.Now;

        private DateTime HeartBitDateTime = DateTime.Now;
        private bool _isReservedModeChange;
        private EmEquipRunMode _reserveMode;
        private EmEquipRunMode _oldEquipMode;

        private bool _IsLotStartIFComplete { get; set; }
        public bool IsLotStartIFComplete
        {
            get
            {
                if (GG.Equip.CimMode == EmCimMode.OffLine || GG.Equip.CimMode == EmCimMode.Local)
                {
                    Logger.CIMLog.AppendLine(LogLevel.Info, string.Format("Lot Start Event Check Pass : Cur CIM Mode [{0}]", GG.Equip.CimMode.ToString()));
                    return true;
                }
                else
                {
                    return _IsLotStartIFComplete;
                }
            }
            set
            {
                _IsLotStartIFComplete = value;
            }
        }
        private bool _IsLotStartConfirmOK;
        public bool IsLotStartConfirmOK
        {
            get
            {
                if (GG.Equip.CimMode == EmCimMode.OffLine || GG.Equip.CimMode == EmCimMode.Local)
                {
                    Logger.CIMLog.AppendLine(LogLevel.Info, string.Format("Lot Start Confirm Check Pass : Cur CIM Mode [{0}]", GG.Equip.CimMode.ToString()));
                    return true;
                }
                else
                {
                    return _IsLotStartConfirmOK;
                }
            }
            set
            {
             _IsLotStartConfirmOK = value;
            }
        }
        private bool _IsWaferStartIFComplete;
        public bool IsWaferStartIFComplete
        {
            get
            {
                if (GG.Equip.CimMode == EmCimMode.OffLine || GG.Equip.CimMode == EmCimMode.Local)
                {
                    Logger.CIMLog.AppendLine(LogLevel.Info, string.Format("Wafer Start Event Check Pass : Cur CIM Mode [{0}]", GG.Equip.CimMode.ToString()));
                    return true;
                }
                else
                {
                    return _IsWaferStartIFComplete;
                }
            }
            set
            {
                _IsWaferStartIFComplete = value;
            }
        }
        private bool _IsWaferMapRequestIFComplete;
        public bool IsWaferMapRequestIFComplete
        {
            get
            {
                if (GG.Equip.CimMode == EmCimMode.OffLine || GG.Equip.CimMode == EmCimMode.Local)
                {
                    Logger.CIMLog.AppendLine(LogLevel.Info, string.Format("Wafer Map Request Event Check Pass : Cur CIM Mode [{0}]", GG.Equip.CimMode.ToString()));
                    return true;
                }
                else
                {
                    return _IsWaferMapRequestIFComplete;
                }
            }
            set
            {
                _IsWaferMapRequestIFComplete = value;
            }
        }
        private bool _IsWaferStarConfirmOK;
        public bool IsWaferStartConfirmOK
        {
            get
            {
                if (GG.Equip.CimMode == EmCimMode.OffLine || GG.Equip.CimMode == EmCimMode.Local)
                {
                    Logger.CIMLog.AppendLine(LogLevel.Info, string.Format("Wafer Start Confirm OK Check Pass : Cur CIM Mode [{0}]", GG.Equip.CimMode.ToString()));
                    return true;
                }
                else
                {
                    return _IsWaferStarConfirmOK;
                }
            }
            set
            {
                _IsWaferStarConfirmOK = value;
            }
        }
        private bool _IsCstLoadIFComplete { get; set; }
        public bool IsCstLoadIFComplete
        {
            get
            {
                if(GG.Equip.CimMode == EmCimMode.OffLine || GG.Equip.CimMode == EmCimMode.Local)
                {
                    Logger.CIMLog.AppendLine(LogLevel.Info, string.Format("Cst Map Event Check Pass : Cur CIM Mode [{0}]", GG.Equip.CimMode.ToString()));
                    return true;
                }
                else
                {
                    return _IsCstLoadIFComplete;
                }
            }
            set
            {
                _IsCstLoadIFComplete = value;
            }
        }
        private bool _IsCstLoadConfirmOK;
        public bool IsCstLoadConfirmOK
        {
            get
            {
                if (GG.Equip.CimMode == EmCimMode.OffLine || GG.Equip.CimMode == EmCimMode.Local)
                {
                    Logger.CIMLog.AppendLine(LogLevel.Info, string.Format("Cst Load Confirm Check Pass : Cur CIM Mode [{0}]", GG.Equip.CimMode.ToString()));
                    return true;
                }
                else
                {
                    return _IsCstLoadConfirmOK;
                }
            }
            set
            {
                _IsCstLoadConfirmOK = value;
            }
        }
        private bool _IsPPSelectOK;
        public bool IsPPSelectOK
        {
            get
            {
                if (GG.Equip.CimMode == EmCimMode.OffLine || GG.Equip.CimMode == EmCimMode.Local)
                {
                    Logger.CIMLog.AppendLine(LogLevel.Info, string.Format("Cst PP Select Event Check Pass : Cur CIM Mode [{0}]", GG.Equip.CimMode.ToString()));
                    return true;
                }
                else
                {
                    
                    return _IsPPSelectOK;
                }
            }
            set
            {
                _IsPPSelectOK = value;
            }
        }
        private bool _IsCstStartCommand;
        public bool IsCstStartCommand
        {
            get
            {
                if (GG.Equip.CimMode == EmCimMode.OffLine || GG.Equip.CimMode == EmCimMode.Local)
                {
                    Logger.CIMLog.AppendLine(LogLevel.Info, string.Format("Cst Start Command Event Check Pass : Cur CIM Mode [{0}]", GG.Equip.CimMode.ToString()));
                    return true;
                }
                else
                {
                    return _IsCstStartCommand;
                }
            }
            set
            {
                _IsCstStartCommand = value;
            }
        }
        private bool _IsCstMoveOutOK;
        public bool IsCstMoveOutOK
        {
            get
            {
                if (GG.Equip.CimMode == EmCimMode.OffLine || GG.Equip.CimMode == EmCimMode.Local)
                {
                    Logger.CIMLog.AppendLine(LogLevel.Info, string.Format("Cst Move Out Flag Pass : Cur CIM Mode [{0}]", GG.Equip.CimMode.ToString()));
                    return true;
                }
                else
                {
                    return _IsCstMoveOutOK;
                }
            }
            set
            {
                _IsCstMoveOutOK = value;
            }
        }
        private bool _IsCstMoveOutIFComplete;
        public bool IsCstMoveOutIFComplete
        {
            get
            {
                if (GG.Equip.CimMode == EmCimMode.OffLine || GG.Equip.CimMode == EmCimMode.Local)
                {
                    Logger.CIMLog.AppendLine(LogLevel.Info, string.Format("Cst Move Out Flag Event Check Pass : Cur CIM Mode [{0}]", GG.Equip.CimMode.ToString()));
                    return true;
                }
                else
                {
                    return _IsCstMoveOutIFComplete;
                }
            }
            set
            {
                _IsCstMoveOutIFComplete = value;
            }
        }


        public void SetValue(PlcAddr addr, double value)
        {
            if (int.MaxValue / 10000 < value)
                value = int.MaxValue; // overflow
            else
                value = value * 10000;
            addr.vInt = (int)value;
        }

        public bool ResetComnpleteFlag()
        {
            IsCstLoadConfirmOK = false;
            IsCstLoadIFComplete = false;
            IsLotStartIFComplete = false;
            IsLotStartConfirmOK = false;
            IsWaferStartIFComplete = false;
            IsWaferMapRequestIFComplete = false;
            IsWaferStartConfirmOK = false;
            IsCstMoveOutOK = false;
            IsCstMoveOutIFComplete = false;
            IsPPSelectOK = false;
            IsCstStartCommand = false;

            return true;
        }
    }
}
