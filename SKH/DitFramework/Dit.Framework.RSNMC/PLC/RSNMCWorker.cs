using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using NMCMotionSDK;
using System.Threading;
using Dit.Framework.Comm;
using System.Diagnostics;

namespace Dit.Framework.RSNMC.PLC
{
    /// <summary>
    /// RS Network Motor Contoller 처리
    /// date 180710
    /// since 180710
    /// </summary>
    public class RSNMCWorker : IVirtualMem
    {
        private int ONE_BYTE_SIZE = 4;
        private const int MEM_SIZE = 102400;        

        #region Common
        private ushort _boardID = 0;
        public ushort BoardID { get { return _boardID; } }
        public RSNMCErrorInfo MasterLastErrorInfo { get; set; }
        #endregion
        #region Motor
        public Dictionary<int, RSNMCErrorInfo> AxisLastErrorInfo { get; set; }
        public List<RSNMCServoController> Motors = new List<RSNMCServoController>();

        public RSNMCEvent[] LstEvent = new RSNMCEvent[10];

        private Thread _motorWorker = null;
        private bool _motorIsRunning = false;
        #endregion
        #region RS NMC Motor Interface
        public PlcAddr XB_EquipMode { get; set; }
        public PlcAddr XB_CheckAlarmStatus { get; set; }
        //         public PlcAddr YB_UpperInterfaceWorking { get; set; }
        //         public PlcAddr YB_LowerInterfaceWorking { get; set; }

        public PlcAddr XB_ImmediateStopReq { get; set; }
        public PlcAddr YB_ImmediateStopReqAck { get; set; }

        public PlcAddr XB_SettingValueSaveReq { get; set; }
        public PlcAddr YB_SettingValueSaveReqAck { get; set; }

        //         public PlcAddr XB_InposBandWidthChangeCmd { get; set; }
        //         public PlcAddr YB_InposBandWidthChangeCmdAck { get; set; }

        public PlcAddr YB_Ready { get; set; }
        public PlcAddr YB_Alive { get; set; }
        public PlcAddr YB_HeavyAlarm { get; set; }

        public PlcAddr XB_PinUpMotorInterlockOffCmd { get; set; }
        public PlcAddr YB_PinUpMotorInterlockOffCmdAck { get; set; }
        public PlcAddr YB_PinUpInterlockOff { get; set; }

        //         public PlcAddr YB_ReviewTimerOverCmd { get; set; }
        //         public PlcAddr XB_ReviewTimerOverCmdAck { get; set; }

        public PlcAddr XB_ResetReq { get; set; }
        public PlcAddr YB_ResetReqAck { get; set; }

        public PlcAddr YB_ResetComplete { get; set; }
        public PlcAddr XB_ResetCompleteAck { get; set; }

        //         public PlcAddr YB_Trigger1Cmd { get; set; }
        //         public PlcAddr YB_Trigger2Cmd { get; set; }
        //         public PlcAddr YB_Trigger3Cmd { get; set; }
        //         public PlcAddr YB_VariableSpeedCmd { get; set; }

        //        public PlcAddr XB_ReviewUsingPmac { get; set; }

        //public PlcAddr XB_ErrEthernetFaultError { get; set; }

        #endregion
        #region IO
        private Thread _ioWorker = null;
        private bool _ioIsRunning = false;
        private List<RSNMCIOIntefaceInfo> _ioInfo;        
        public RSNMCErrorInfo IOLastErrorInfo { get; set; }
        #endregion
        #region Memory
        // RS Write
        public int[] MC_INT = new int[MEM_SIZE];
        public double[] MC_REAL = new double[MEM_SIZE];
        // Control Write
        public int[] CTRL_INT = new int[MEM_SIZE];
        public double[] CTRL_REAL = new double[MEM_SIZE];

        // RS Write
        public byte[] BYTE_RsX = new byte[MEM_SIZE];
        public byte[] BYTE_AI = new byte[MEM_SIZE];
        // Control Write
        public byte[] BYTE_RsY = new byte[MEM_SIZE];
        public byte[] BYTE_AO = new byte[MEM_SIZE];
        #endregion        
        public RSNMCWorker(ushort boardID, ushort[] axisID, string motorAddr, List<RSNMCIOIntefaceInfo> ioInfo, string ioAddr)
        {
            _boardID = boardID;
            _ioInfo = ioInfo;

            IOLastErrorInfo = new RSNMCErrorInfo();
            MasterLastErrorInfo = new RSNMCErrorInfo();
            AxisLastErrorInfo = new Dictionary<int, RSNMCErrorInfo>();
            foreach (ushort idx in axisID)
            {
                Motors.Add(new RSNMCServoController(this, "NONE", idx));
                AxisLastErrorInfo[idx] = new RSNMCErrorInfo();
            }

            AddressMgr.LoadRS(motorAddr, this);
            SetAddress(motorAddr);
            Initialize();         
        }
        #region Initialize        
        private void SetAddress(string address)
        {
            #region
            RSNMCLogger.AppendLine("====== RS NMC Set Address Start ======");
            this.XB_EquipMode                     /**/ = AddressMgr.GetRsWorkerAddress("PMAC_YB_EquipState", 0);
            this.XB_CheckAlarmStatus              /**/ = AddressMgr.GetRsWorkerAddress("PMAC_YB_EquipState", 1);

            this.YB_Ready                     /**/ = AddressMgr.GetRsWorkerAddress("PMAC_XB_PmacState", 0);
            this.YB_Alive                     /**/ = AddressMgr.GetRsWorkerAddress("PMAC_XB_PmacState", 1);
            this.YB_PinUpInterlockOff             /**/ = AddressMgr.GetRsWorkerAddress("PMAC_XB_PmacState", 4);

            //this.XB_PmacHeavyAlarm                /**/ = AddressMgr.GetRsWorkerAddress("PMAC_XB_PmacMidAlarm");
            this.XB_PinUpMotorInterlockOffCmd     /**/ = AddressMgr.GetRsWorkerAddress("PMAC_YB_CommonCmd", 0);
            this.YB_PinUpMotorInterlockOffCmdAck  /**/ = AddressMgr.GetRsWorkerAddress("PMAC_XB_CommonCmdAck", 0);
            this.XB_ResetReq                  /**/ = AddressMgr.GetRsWorkerAddress("PMAC_YB_CommonCmd", 1);
            this.YB_ResetReqAck               /**/ = AddressMgr.GetRsWorkerAddress("PMAC_XB_CommonCmdAck", 1);
            this.XB_ImmediateStopReq              /**/ = AddressMgr.GetRsWorkerAddress("PMAC_YB_CommonCmd", 2);
            this.YB_ImmediateStopReqAck           /**/ = AddressMgr.GetRsWorkerAddress("PMAC_XB_CommonCmdAck", 2);
            this.XB_SettingValueSaveReq                 /**/ = AddressMgr.GetRsWorkerAddress("PMAC_YB_CommonCmd", 3);
            this.YB_SettingValueSaveReqAck              /**/ = AddressMgr.GetRsWorkerAddress("PMAC_XB_CommonCmdAck", 3);
            this.XB_ResetCompleteAck          /**/ = AddressMgr.GetRsWorkerAddress("PMAC_YB_CommonCmd", 5);
            this.YB_ResetComplete             /**/ = AddressMgr.GetRsWorkerAddress("PMAC_XB_CommonCmdAck", 5);

            //             this.YB_InposBandWidthChangeCmd       /**/ = AddressMgr.GetRsWorkerAddress("PMAC_YB_InpositionChangeCmd");
            //             this.XB_InposBandWidthChangeCmdAck    /**/ = AddressMgr.GetRsWorkerAddress("PMAC_XB_InpositionChangeCmdAck");

            //             this.XB_Trigger1Cmd                   /**/ = AddressMgr.GetRsWorkerAddress("PMAC_YB_EquipState", 4);
            //             this.XB_Trigger2Cmd                   /**/ = AddressMgr.GetRsWorkerAddress("PMAC_YB_EquipState", 5);
            //             this.XB_Trigger3Cmd                   /**/ = AddressMgr.GetRsWorkerAddress("PMAC_YB_EquipState", 6);
            //             this.XB_VariableSpeedCmd              /**/ = AddressMgr.GetRsWorkerAddress("PMAC_YB_EquipState", 8);

            //통신 에러. 
            //this.YB_ErrEthernetFaultError                       /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_ErrEthernetFaultError"));


            for (int jPos = 0; jPos < Motors.Count; jPos++)
            {
                //string motor = motors[jPos].Name;
                //string slayerMotor = motors[jPos].SlayerName;
                int axis = Motors[jPos].AxisID;
                string axisStr = (axis).ToString("D2");
                //int slayerAxis = Motors[jPos].AxisId - 1;
                //string slayerAxisStr = (slayerAxis + 1).ToString("D2");

                Motors[jPos].YB_StatusHomeCompleteBit                       /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_StatusHomeCompleteBit"), axis);
                Motors[jPos].YB_StatusHomeInPosition                        /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_StatusHomeInPosition"), axis);
                Motors[jPos].YB_StatusMotorMoving                           /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_StatusMotorMoving"), axis);
                Motors[jPos].YB_StatusMotorInPosition                       /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_StatusMotorInPosition"), axis);
                Motors[jPos].YB_StatusNegativeLimitSet                      /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_StatusNegativeLimitSet"), axis);
                Motors[jPos].YB_StatusPositiveLimitSet                      /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_StatusPositiveLimitSet"), axis);
                Motors[jPos].YB_StatusMotorServoOn                          /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_ErrMotorServoOn"), axis);
                //Motors[jPos].YB_ErrCriticalPositionError                    /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_ErrCriticalPositionError"), axis);
                Motors[jPos].YB_ErrDriveFaultError                          /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_ErrDriveFaultError"), axis);
                Motors[jPos].YB_ErrErrorStop = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_ErrErrorStop"), axis);
                Motors[jPos].YB_ErrAxisWarning = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_ErrAxisWarning"), axis);
                //Motors[jPos].YB_ErrOverCurrentError                         /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_ErrOverCurrentError"), axis);
                //Motors[jPos].YB_ErrEcallError                               /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_ErrEthernetFaultError"), axis);

                Motors[jPos].YF_CurrMotorPosition                           /**/  = AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_XF_CurrentMotorPosition", axisStr));
                Motors[jPos].YF_CurrMotorSpeed                              /**/  = AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_XF_CurrentMotorSpeed", axisStr));
                Motors[jPos].YF_CurrMotorStress                             /**/  = AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_XI_CurrentMotorStress", axisStr));

                Motors[jPos].XB_AxisImmediateStopCmd                              /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_YB_ImmediateStopCmd"), axis);
                Motors[jPos].YB_AxisImmediateStopCmdAck                              /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_ImmediateStopCmdAck"), axis);

                //Motors[jPos].XI_InposBandWidth                              /**/  = AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_YI_InpositionBandWidth", axisStr));
                //Motors[jPos].YI_InposBandWidthAck                           /**/  = AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_XI_InpositionBandWidthAck", axisStr));


                //Motors[jPos].YB_StatusHomeCompleteBitSlayer                 /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_StatusHomeCompleteBit"), slayerAxis);
                //Motors[jPos].YB_StatusHomeInPositionSlayer                  /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_StatusHomeInPosition"), slayerAxis);
                //Motors[jPos].YB_StatusMotorMovingSlayer                     /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_StatusMotorMoving"), slayerAxis);
                //Motors[jPos].YB_StatusMotorInPositionSlayer                 /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_StatusMotorInPosition"), slayerAxis);
                //Motors[jPos].YB_StatusNegativeLimitSetSlayer                /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_StatusNegativeLimitSet"), slayerAxis);
                //Motors[jPos].YB_StatusPositiveLimitSetSlayer                /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_StatusPositiveLimitSet"), slayerAxis);
                //Motors[jPos].YB_StatusMotorServoOnSlayer                    /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_ErrMotorServoOn"), slayerAxis);
                //Motors[jPos].YB_ErrCriticalPositionErrorSlayer              /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_ErrCriticalPositionError"), slayerAxis);
                //Motors[jPos].YB_ErrDriveFaultErrorSlayer                    /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_ErrDriveFaultError"), slayerAxis);
                //Motors[jPos].YB_ErrOverCurrentErrorSlayer                   /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_ErrOverCurrentError"), slayerAxis);
                //Motors[jPos].YB_ErrEcallErrorSlayer                         /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_ErrEthernetFaultError"), slayerAxis);

                //Motors[jPos].YF_CurrMotorPositionSlayer                     /**/  = AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_XF_CurrentMotorPosition", slayerAxisStr));
                //Motors[jPos].YF_CurrMotorSpeedSlayer                        /**/  = AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_XF_CurrentMotorSpeed", slayerAxisStr));
                //Motors[jPos].YF_CurrMotorStressSlayer                       /**/  = AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_XI_CurrentMotorStress", slayerAxisStr));

                Motors[jPos].XB_HomeCmd                                     /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_YB_HomeCmd"), axis);
                Motors[jPos].YB_HomeCmdAck                                  /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_HomeCmdAck"), axis);

                Motors[jPos].XB_ManualResetServoOnCmd                                  /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_YB_ManualResetServoOnCmd"), axis);
                Motors[jPos].XB_ManualServoOffCmd                                  /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_YB_ManualServoOffCmd"), axis);

                Motors[jPos].XB_MotorJogMinusMove                           /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_YB_MinusJogCmd"), axis);
                Motors[jPos].XB_MotorJogPlusMove                            /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_YB_PlusJogCmd"), axis);
                Motors[jPos].YF_MotorJogSpeedCmdAck = Motors[jPos].XF_MotorJogSpeedCmd                            /**/  = AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_YB_JogSpeed", axisStr));
                //Motors[jPos].YI_CmdAckLogMsg                                /**/  = AddressMgr.GetRsWorkerAddress(string.Format("PMAC_XB_CmdAckLogMsg"), axis);

                Motors[jPos].YF_CurrMotorPosition                           /**/  = AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_XF_CurrentMotorPosition", axisStr));
                Motors[jPos].YF_CurrMotorSpeed                              /**/  = AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_XF_CurrentMotorSpeed", axisStr));

                for (int iPos = 1; iPos <= RSNMCServoController.MaxPositionCount; iPos++)
                {
                    Motors[jPos].XB_QuickSave0PosCmd[iPos - 1] = AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_YB_QuickSavePosCmd", axisStr), iPos - 1);
                    Motors[jPos].XB_Position0MoveCmd[iPos - 1]        /**/= AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_YB_PositionMoveCmd", axisStr), iPos - 1);
                    Motors[jPos].YB_Position0MoveCmdAck[iPos - 1]     /**/= AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_XB_PositionMoveCmdAck", axisStr), iPos - 1);
                    Motors[jPos].YB_PositionComplete[iPos - 1]       /**/= AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_XB_PositionMoveComplete", axisStr), iPos - 1);
                    Motors[jPos].XF_Position1stPoint[iPos - 1]          /**/= AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_YF_Position{1}Point", axisStr, iPos.ToString("D2")));
                    Motors[jPos].YF_Position1stPointAck[iPos - 1]       /**/= AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_XF_Position{1}PointAck", axisStr, iPos.ToString("D2")));
                    Motors[jPos].XF_Position1stSpeed[iPos - 1]          /**/= AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_YF_Position{1}Speed", axisStr, iPos.ToString("D2")));
                    Motors[jPos].YF_Position1stSpeedAck[iPos - 1]       /**/= AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_XF_Position{1}SpeedAck", axisStr, iPos.ToString("D2")));
                    Motors[jPos].XF_Position1stAccel[iPos - 1]          /**/= AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_YF_Position{1}Accel", axisStr, iPos.ToString("D2")));
                    Motors[jPos].YF_Position1stAccelAck[iPos - 1]       /**/= AddressMgr.GetRsWorkerAddress(string.Format("Axis{0}_XF_Position{1}AccelAck", axisStr, iPos.ToString("D2")));
                }
            }
            RSNMCLogger.AppendLine("====== RS NMC Set Address End ======");
            #endregion
        }
        public void Initialize()
        {
            LstEvent[(int)EmRSNMCEvent.IMMEDIATE_STOP]    /**/ = new RSNMCEvent() { Name = "IMMADIATE_STOP",   /**/ XB_EVENT = XB_ImmediateStopReq,         /**/ YB_EVENT_ACK = YB_ImmediateStopReqAck, OnEvent = OnImmediateStop };            
            LstEvent[(int)EmRSNMCEvent.RESET]        /**/ = new RSNMCEvent() { Name = "RESET",       /**/ XB_EVENT = XB_ResetReq,       /**/ YB_EVENT_ACK = YB_ResetReqAck, OnEventComplete = OnReset };
        }
        #endregion
        #region Process Event
        public void OnImmediateStop(RSNMCEvent evt)
        {
            StartImmediateStep();
        }
        public void OnReset(RSNMCEvent evt)
        {
            ResetStepNum = 10;
        }
        #endregion
        #region RS NMC Board Access
        public int Open()
        {
            try
            {
                byte mode = 0;
                StringBuilder rntStr = new StringBuilder(128);
                string str = string.Empty;
                NMCSDKLib.MC_STATUS ms;

                ms = NMCSDKLib.MC_Init();
                if (ms == NMCSDKLib.MC_STATUS.MC_OK)
                {

                }
                else
                {
                    MasterLastErrorInfo.Msg = GetErrorMessage(ms);
                    return FALSE;
                }

                ms = NMCSDKLib.MasterGetCurMode(this._boardID, ref mode);

                if (ms == NMCSDKLib.MC_STATUS.MC_OK)
                {
                    
                }
                else
                {
                    MasterLastErrorInfo.Msg = GetErrorMessage(ms);
                    return FALSE;
                }
                if (mode != 2)
                {                   
                    ms = NMCSDKLib.MC_MasterRUN(this._boardID);
                    if (ms == NMCSDKLib.MC_STATUS.MC_OK)
                    {

                    }
                    else
                    {
                        MasterLastErrorInfo.Msg = GetErrorMessage(ms);
                        return FALSE;
                    }
                }
                return TRUE;
            }
            catch
            {
                return FALSE;
            }
        }
        public int Close()
        {
            try
            {
                StopIoProc();
                StopMotorProc();

                //jys:: stop하면 io꺼지고 모터 홈 잃음
                //StringBuilder rntStr = new StringBuilder(128);
                //string str = string.Empty;
                //NMCSDKLib.MC_STATUS ms;

                //ms = NMCSDKLib.MC_MasterSTOP(this._boardID);
                //if (ms == NMCSDKLib.MC_STATUS.MC_OK)
                //{

                //}
                //else
                //{
                //    MasterLastErrorInfo.Msg = GetErrorMessage(ms);
                //    return FALSE;
                //}
                return TRUE;
            }
            catch
            {
                return FALSE;
            }
        }
        private string GetErrorMessage(NMCSDKLib.MC_STATUS ms)
        {
            StringBuilder rntStr = new StringBuilder(128);
            string str = string.Empty;

            ms = NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, rntStr);
            str += String.Format("ret = {0,10:G},{0,10:X}", ms);
            str += "::" + rntStr;
            return str;
        }
        #endregion
        #region IO Thread
        public void RunIoProc()
        {
            _ioWorker = new Thread(new ThreadStart(Worker_IoDoWork));
            _ioWorker.IsBackground = true;
            _ioWorker.Priority = ThreadPriority.Highest;
            _ioWorker.Start();
            _ioIsRunning = true;
        }
        public void StopIoProc()
        {
            _ioIsRunning = false;
            _ioWorker.Join();         
        }
        private void Worker_IoDoWork()
        {
            while (_ioIsRunning)
            {                                
                IoLogicWorking();
                Thread.Sleep(1);
            }
        }
        private long IoScanTime { get { return _ioScanTime; } }
        private long _ioScanTime = 0;
        private Stopwatch _ioSw = new Stopwatch();
        private void IoLogicWorking()
        {
            _ioSw.Restart();
            _isIoError = false;
            _ioErrStr.Clear();
            ReadWriteIO();
            if (_isIoError == true)
            {
                IOLastErrorInfo.Msg = _ioErrStr.ToString();
            }
            _ioScanTime = _ioSw.ElapsedMilliseconds;
        }
        private NMCSDKLib.MC_STATUS _ioMs;
        private byte[] _dioTempArr = new byte[4];
        private byte[] _aioTempArr = new byte[16];
        private StringBuilder _ioErrStr = new StringBuilder(1000);
        private bool _isIoError = false;
        private bool _initDone = false;
        private void ReadWriteIO()
        {
            if (_initDone == false)
            {
                foreach (RSNMCIOIntefaceInfo info in _ioInfo)
                {
                    switch (info.IoType)
                    {
                        case RSNMCIOType.Y:
                            _ioMs = NMCSDKLib.MC_IO_READ(_boardID, info.ID, (ushort)NMCMotionSDK.NMCSDKLib.IOBufMode.BUF_OUT, 0, 4, _dioTempArr);
                            if (_ioMs != NMCSDKLib.MC_STATUS.MC_OK)
                            {
                                _isIoError |= true;
                                _ioErrStr.AppendFormat("Init DI {0} Err, ", info.ID);
                            }
                            Array.Copy(_dioTempArr, 0, BYTE_RsY, info.ModuleOrder * 4, 4);
                            break;
                        case RSNMCIOType.AO:
                            _ioMs = NMCSDKLib.MC_IO_READ(_boardID, info.ID, (ushort)NMCMotionSDK.NMCSDKLib.IOBufMode.BUF_OUT, 0, 16, _aioTempArr);
                            if (_ioMs != NMCSDKLib.MC_STATUS.MC_OK)
                            {
                                _isIoError |= true;
                                _ioErrStr.AppendFormat("Init AI {0} Err, ", info.ID);
                            }
                            Array.Copy(_aioTempArr, 0, BYTE_AO, info.ModuleOrder * 16, 16);
                            break;
                        default:
                            break;
                    }
                }
                if (_isIoError == false)
                    _initDone = true;
            }
            else
            {
                foreach (RSNMCIOIntefaceInfo info in _ioInfo)
                {
                    switch (info.IoType)
                    {
                        case RSNMCIOType.X:
                            _ioMs = NMCSDKLib.MC_IO_READ(_boardID, info.ID, (ushort)NMCMotionSDK.NMCSDKLib.IOBufMode.BUF_IN, 0, 4, _dioTempArr);
                            if (_ioMs != NMCSDKLib.MC_STATUS.MC_OK)
                            {
                                _isIoError |= true;
                                _ioErrStr.AppendFormat("DI {0} Err, ", info.ID);
                            }
                            //jys:: RS NMC의 X는 0이면 ON 1이면 OFF임.
                            _dioTempArr[0] = (byte)(~_dioTempArr[0] & 0xff);
                            _dioTempArr[1] = (byte)(~_dioTempArr[1] & 0xff);
                            _dioTempArr[2] = (byte)(~_dioTempArr[2] & 0xff);
                            _dioTempArr[3] = (byte)(~_dioTempArr[3] & 0xff);
                            Array.Copy(_dioTempArr, 0, BYTE_RsX, info.ModuleOrder * 4, 4);
                            break;
                        case RSNMCIOType.Y:
                            Array.Copy(BYTE_RsY, info.ModuleOrder * 4, _dioTempArr, 0, 4);
                            _ioMs = NMCSDKLib.MC_IO_WRITE(_boardID, info.ID, 0, 4, _dioTempArr);
                            if (_ioMs != NMCSDKLib.MC_STATUS.MC_OK)
                            {
                                _isIoError |= true;
                                _ioErrStr.AppendFormat("DO {0} Err, ", info.ID);
                            }
                            break;
                        case RSNMCIOType.AI:
                            _ioMs = NMCSDKLib.MC_IO_READ(_boardID, info.ID, (ushort)NMCMotionSDK.NMCSDKLib.IOBufMode.BUF_IN, 0, 16, _aioTempArr);
                            if (_ioMs != NMCSDKLib.MC_STATUS.MC_OK)
                            {
                                _isIoError |= true;
                                _ioErrStr.AppendFormat("AI {0} Err, ", info.ID);
                            }
                            Array.Copy(_aioTempArr, 0, BYTE_AI, info.ModuleOrder * 16, 16);
                            break;
                        case RSNMCIOType.AO:
                            Array.Copy(BYTE_AO, info.ModuleOrder * 16, _aioTempArr, 0, 16);
                            _ioMs = NMCSDKLib.MC_IO_WRITE(_boardID, info.ID, 0, 16, _aioTempArr);
                            if (_ioMs != NMCSDKLib.MC_STATUS.MC_OK)
                            {
                                _isIoError |= true;
                                _ioErrStr.AppendFormat("AO {0} Err, ", info.ID);
                            }
                            break;
                    }
                }
            }
        }     
        #endregion
        #region Motor Thread
        public void RunMotorProc()
        {
            _motorWorker = new Thread(new ThreadStart(Worker_MotorDoWork));
            _motorWorker.IsBackground = true;
            _motorWorker.Priority = ThreadPriority.Highest;
            _motorWorker.Start();
            _motorIsRunning = true;
        }
        private bool _existMotor = false;
        public void StopMotorProc()
        {
            _existMotor = true;  
            StartImmediateStep();                      
        }        
        private void Worker_MotorDoWork()
        {
            while (_motorIsRunning)
            {                
                LogicWorking();
                Thread.Sleep(1);
            }
        }
        private long MotorScanTime { get { return _motorScanTime; } }
        private long _motorScanTime = 0;
        private Stopwatch _motorSw = new Stopwatch();
        private void LogicWorking()
        {
            _motorSw.Restart();
            EventProcessing();
            CommonProcessing();
            MotorProcessing();
            _motorScanTime = _motorSw.ElapsedMilliseconds;
        }
        public void EventProcessing()
        {
            foreach (RSNMCEvent evt in LstEvent)
            {
                if (evt == null) continue;
                evt.LogicWorking();
            }
        }

        private Stopwatch _aliveCheck = new Stopwatch();
        private void CommonProcessing()
        {
            YB_Ready.vBit = GetCurMode() == 2;

            if (_aliveCheck.IsRunning == false
                || _aliveCheck.ElapsedMilliseconds > 800)
            {
                YB_Alive.vBit = !YB_Alive.vBit;
                _aliveCheck.Restart();
            }                        

            ImmediateStep();
            SettingSaveStep();
            ResetStep();
        }
        public int GetCurMode()
        {
            NMCSDKLib.MC_STATUS ms;
            StringBuilder cstrErrorMsg = new StringBuilder(128);
            byte MstMode = 0;

            ms = NMCSDKLib.MasterGetCurMode(
                BoardID,
                ref MstMode          //해당 보드의 State 리턴
                );
            if (ms != NMCSDKLib.MC_STATUS.MC_OK)
            {
                NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                MasterLastErrorInfo.Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                return -1;
            }

            return MstMode;
        }

        private int ResetStepNum = 0;
        private int OldResetStepNum = 0;
        private void ResetStep()
        {
            NMCSDKLib.MC_STATUS ms;
            StringBuilder cstrErrorMsg = new StringBuilder(128);

            if (OldResetStepNum != ResetStepNum)
            {
                RSNMCLogger.AppendLine("Board {0}:{1} ResetStep {2}", BoardID, Name, ResetStepNum);
                OldResetStepNum = ResetStepNum;
            }

            if (ResetStepNum == 0)
            {

            }
            else if (ResetStepNum == 10)
            {
                this.MasterLastErrorInfo.Msg = "";

                ms = NMCSDKLib.MC_Init();

                if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                {
                    NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                    this.MasterLastErrorInfo.Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                }
                if (GetCurMode() != 2
                    || ms == NMCSDKLib.MC_STATUS.MC_MASTER_INVALID_STATE)
                    ResetStepNum = 20;
                else
                    ResetStepNum = 60;
            }
            else if (ResetStepNum == 20)
            {
                ms = NMCSDKLib.MC_MasterSTOP(this._boardID);

                if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                {
                    NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                    this.MasterLastErrorInfo.Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                }
                else
                    ResetStepNum = 30;
            }
            else if (ResetStepNum == 30)
            {
                ms = NMCSDKLib.MC_MasterRUN(this._boardID);

                if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                {
                    NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                    this.MasterLastErrorInfo.Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                }
                else
                    ResetStepNum = 40;
            }
            else if (ResetStepNum == 40)
            {
                ms = NMCSDKLib.MC_Init();

                if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                {
                    NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                    this.MasterLastErrorInfo.Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                }
                else
                    ResetStepNum = 60;
            }
//             else if (ResetStepNum == 40)
//             {
//                 foreach (RSNMCServoController motor in Motors)
//                 {
//                     if (motor.IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcErrorStop))
//                     {
//                         ms = NMCSDKLib.MC_Reset(this._boardID, motor.AxisID);
// 
//                         if (ms != NMCSDKLib.MC_STATUS.MC_OK)
//                         {
//                             NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
//                             this.MasterLastErrorInfo.Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
//                         }
//                     }
//                 }
// 
//                 if (Motors.TrueForAll(r => r.IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcErrorStop) == false))
//                     ResetStepNum = 50;
//             }
//             else if (ResetStepNum == 50)
//             {
//                 foreach (RSNMCServoController motor in Motors)
//                 {
//                     if (motor.IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcPowerOn) == false)
//                     {
//                         ms = NMCSDKLib.MC_Power(this._boardID, motor.AxisID, true);
// 
//                         if (ms != NMCSDKLib.MC_STATUS.MC_OK)
//                         {
//                             NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
//                             this.MasterLastErrorInfo.Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
//                         }
//                     }
//                 }
// 
//                 if (Motors.TrueForAll(r => r.IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcPowerOn)))
//                     ResetStepNum = 60;
//             }
            else if (ResetStepNum == 60)
            {
                byte mst = 0;
                ms = NMCSDKLib.MasterGetCurMode(this._boardID, ref mst);
                if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                {
                    NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                    this.MasterLastErrorInfo.Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                }
                else if (mst == 2)
                {
                    ResetStepNum = 70;
                }                
                else
                    this.MasterLastErrorInfo.Msg = String.Format("Master Run (MasterGetCurMode) Error Code {0}", mst);
            }
            else if (ResetStepNum == 70)
            {
                YB_ResetComplete.vBit = true;
                ResetStepNum = 80;
            }
            else if (ResetStepNum == 80)
            {
                if (XB_ResetCompleteAck.vBit == true)
                {
                    YB_ResetComplete.vBit = false;
                    ResetStepNum = 90;
                }
            }
            else if (ResetStepNum == 90)
            {
                ResetStepNum = 0;
            }
        }

        private int SettingSaveStepNum = 0;
        private int OldSettingSaveStepNum = 0;
        private void SettingSaveStep()
        {
            if (OldSettingSaveStepNum != SettingSaveStepNum)
            {
                RSNMCLogger.AppendLine("Board {0}:{1} SettingSaveStep {2}", BoardID, Name, SettingSaveStepNum);
                OldSettingSaveStepNum = SettingSaveStepNum;
            }

            if (SettingSaveStepNum == 0)
            {
                if (XB_SettingValueSaveReq.vBit == true)
                {
                    SettingSaveStepNum = 10;
                }
            }
            else if (SettingSaveStepNum == 10)
            {
                YB_SettingValueSaveReqAck.vBit = true;
                Motors.ForEach(m => m.SaveSetting());
                SettingSaveStepNum = 20;
            }
            else if (SettingSaveStepNum == 20)
            {
                if (XB_SettingValueSaveReq.vBit == false)
                {
                    YB_SettingValueSaveReqAck.vBit = false;
                    SettingSaveStepNum = 30;
                }
            }
            else if (SettingSaveStepNum == 30)
            {
                SettingSaveStepNum = 0;
            }
        }

        private int ImStopStepNum = 0;
        private int OldImStopStepNum = 0;
        private void StartImmediateStep()
        {
            ImStopStepNum = 10;
        }
        private void ImmediateStep()
        {
            if (OldImStopStepNum != ImStopStepNum)
            {
                RSNMCLogger.AppendLine("Board {0}:{1} ImStopStep {2}", BoardID, Name, ImStopStepNum);
                OldImStopStepNum = ImStopStepNum;
            }

            if (ImStopStepNum == 0)
            {

            }
            else if (ImStopStepNum == 10)
            {
                foreach (RSNMCServoController motor in Motors)
                {
                    if ((motor.IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcErrorStop) || motor.IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcStandStill)) == false)
                    {
                        motor.StartEmergencyStop();
                    }
                }
                if (Motors.TrueForAll(m => m.IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcErrorStop) == true || m.IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcStandStill) == true))
                    ImStopStepNum = 20;
            }
            else if (ImStopStepNum == 20)
            {
                ImStopStepNum = 0;

                if (_existMotor == true)
                {
                    _existMotor = false;
                    _motorIsRunning = false;
                    _motorWorker.Join();
                }
            }
        }
        private void MotorProcessing()
        {
            Motors.ForEach(f => f.LogicWorking());
        }        
        public bool SetInpos(int axis, float inpos)
        {
            RSNMCServoController servo = Motors.FirstOrDefault(m => m.AxisID == axis);
            if (servo == null)
                return false;
            servo.Inpos = inpos;
            return true;
        }
        #endregion
        #region Access Memory
        public int ReadMemory(PlcAddr addr, out object[] dest)
        {
            dest = new object[addr.Length];

            if (addr.Type == PlcMemType.RS_MI)
                Array.Copy(MC_INT, addr.Addr, dest, 0, addr.Length);
            else if (addr.Type == PlcMemType.RS_MR)
                Array.Copy(MC_REAL, addr.Addr, dest, 0, addr.Length);
            else if (addr.Type == PlcMemType.RS_CI)
                Array.Copy(CTRL_INT, addr.Addr, dest, 0, addr.Length);
            else if (addr.Type == PlcMemType.RS_CR)
                Array.Copy(CTRL_REAL, addr.Addr, dest, 0, addr.Length);
            else if (addr.Type == PlcMemType.RS_X)
                Array.Copy(BYTE_RsX, addr.Addr, dest, 0, addr.Length);
            else if (addr.Type == PlcMemType.RS_Y)
                Array.Copy(BYTE_RsY, addr.Addr, dest, 0, addr.Length);
            else if (addr.Type == PlcMemType.RS_AI)
            {
                dest = new object[addr.Length * 2];
                Array.Copy(BYTE_AI, addr.Addr * 2, dest, 0, addr.Length * 2);
            }
            else if (addr.Type == PlcMemType.RS_AO)
            {
                dest = new object[addr.Length * 2];
                Array.Copy(BYTE_AO, addr.Addr * 2, dest, 0, addr.Length * 2);
            }
            return 1;
        }
        public int WriteMemory(PlcAddr addr, object src)
        {
            Array arr = src as Array;
            if (addr.Type == PlcMemType.RS_MI)
                Array.Copy(arr, 0, MC_INT, addr.Addr, addr.Length);
            else if (addr.Type == PlcMemType.RS_MR)
                Array.Copy(arr, 0, MC_REAL, addr.Addr, addr.Length);
            else if (addr.Type == PlcMemType.RS_CI)
                Array.Copy(arr, 0, CTRL_INT, addr.Addr, addr.Length);
            else if (addr.Type == PlcMemType.RS_CR)
                Array.Copy(arr, 0, CTRL_REAL, addr.Addr, addr.Length);
            else if (addr.Type == PlcMemType.RS_X)
                Array.Copy(arr, 0, BYTE_RsX, addr.Addr, addr.Length);
            else if (addr.Type == PlcMemType.RS_Y)
                Array.Copy(arr, 0, BYTE_RsY, addr.Addr, addr.Length);
            else if (addr.Type == PlcMemType.RS_AI)
                Array.Copy(arr, 0, BYTE_AI, addr.Addr * 2, addr.Length * 2);
            else if (addr.Type == PlcMemType.RS_AO)
                Array.Copy(arr, 0, BYTE_AO, addr.Addr * 2, addr.Length * 2);
            return 1;
        }
        public override bool VirGetBit(PlcAddr addr)
        {
            return VirGetInt32(addr).GetBit(addr.Bit);
        }
        public override void VirSetBit(PlcAddr addr, bool value)
        {
            int vv = VirGetInt32(addr).SetBit(addr.Bit, value);
            VirSetInt32(addr, vv);
        }
        public override int VirGetInt32(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.RS_MI)
            {
                return MC_INT[addr.Addr];
            }
            else if (addr.Type == PlcMemType.RS_CI)
            {
                return CTRL_INT[addr.Addr];
            }
            else
                throw new Exception("ADDR TYPE ERROR");
        }
        public override void VirSetInt32(PlcAddr addr, int value)
        {
            if (addr.Type == PlcMemType.RS_MI)
            {
                MC_INT[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.RS_CI)
            {
                CTRL_INT[addr.Addr] = value;
            }
            else
                throw new Exception("ADDR TYPE ERROR");
        }
        public override float VirGetFloat(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.RS_MR)
            {
                return (float)MC_REAL[addr.Addr];
            }
            else if (addr.Type == PlcMemType.RS_CR)
            {
                return (float)CTRL_REAL[addr.Addr];
            }
            else
                throw new Exception("ADDR TYPE ERROR");
        }
        public override void VirSetFloat(PlcAddr addr, float value)
        {
            if (addr.Type == PlcMemType.RS_MR)
            {
                MC_REAL[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.RS_CR)
            {
                CTRL_REAL[addr.Addr] = value;
            }
            else
                throw new Exception("ADDR TYPE ERROR");
        }
        #endregion        
    }
}
