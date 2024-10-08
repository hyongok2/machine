using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using NMCMotionSDK;

namespace Dit.Framework.RSNMC.PLC
{
    /// <summary>
    /// 한 축 제어
    /// date 180710
    /// since 180710
    /// </summary>
    public class RSNMCServoController
    {        
        private const float JogSpeedLimit = 120000;        
        private const float _movingStandardSpeed = 50;

        private const double _errorValue = -9999;
        public float Inpos = 20;        
        public const int MaxPositionCount = 32;

        private ushort BoardID { get { return _nmcWorker.BoardID; } }

        private RSNMCWorker _nmcWorker;
        public string Name;
        public ushort AxisID;
                
        private uint _readAxisStatus;
        private bool _isErrorReadAxisStatus;        

        #region Interface
        public PlcAddr YB_StatusHomeCompleteBit { get; set; }
        public PlcAddr YB_StatusHomeInPosition { get; set; }
        public PlcAddr YB_StatusMotorMoving { get; set; }
        public PlcAddr YB_StatusMotorInPosition { get; set; }
        public PlcAddr YB_StatusNegativeLimitSet { get; set; }
        public PlcAddr YB_StatusPositiveLimitSet { get; set; }
        public PlcAddr YB_StatusMotorServoOn { get; set; }
        public PlcAddr YB_ErrDriveFaultError { get; set; }
        public PlcAddr YB_ErrErrorStop { get; set; }
        public PlcAddr YB_ErrAxisWarning { get; set; }
        //         public PlcAddr XB_ErrFatalFollowingError { get; set; }
        //         public PlcAddr XB_ErrAmpFaultError { get; set; }
        //         public PlcAddr XB_ErrI2TAmpFaultError { get; set; }

        public PlcAddr YF_CurrMotorPosition { get; set; }
        public PlcAddr YF_CurrMotorSpeed { get; set; }
        public PlcAddr YF_CurrMotorStress { get; set; }

        //겐츄리 및 그래버일 경우만 다름 사용=================
        //         public PlcAddr XB_StatusHomeCompleteBitSlayer { get; set; }
        //         public PlcAddr XB_StatusHomeInPositionSlayer { get; set; }
        //         public PlcAddr XB_StatusMotorMovingSlayer { get; set; }
        //         public PlcAddr XB_StatusMotorInPositionSlayer { get; set; }
        //         public PlcAddr XB_StatusNegativeLimitSetSlayer { get; set; }
        //         public PlcAddr XB_StatusPositiveLimitSetSlayer { get; set; }
        //         public PlcAddr XB_StatusMotorServoOnSlayer { get; set; }
        //         public PlcAddr XB_ErrFatalFollowingErrorSlayer { get; set; }
        //         public PlcAddr XB_ErrAmpFaultErrorSlayer { get; set; }
        //         public PlcAddr XB_ErrI2TAmpFaultErrorSlayer { get; set; }
        //         public PlcAddr XF_CurrMotorPositionSlayer { get; set; }
        //         public PlcAddr XF_CurrMotorSpeedSlayer { get; set; }
        //         public PlcAddr XF_CurrMotorStressSlayer { get; set; }
        //================================================

        public PlcAddr XB_HomeCmd { get; set; }
        public PlcAddr YB_HomeCmdAck { get; set; }

        public PlcAddr XB_ManualResetServoOnCmd { get; set; }
        public PlcAddr XB_ManualServoOffCmd { get; set; }

        public PlcAddr XB_MotorJogMinusMove { get; set; }
        public PlcAddr XB_MotorJogPlusMove { get; set; }
        public PlcAddr XF_MotorJogSpeedCmd { get; set; }
        public PlcAddr YF_MotorJogSpeedCmdAck { get; set; }

        //마크로 사용 추가=================
        //         public PlcAddr YF_Trigger1StartPosi { get; set; }
        //         public PlcAddr XF_Trigger1StartPosiAck { get; set; }
        //         public PlcAddr YF_Trigger2StartPosi { get; set; }
        //         public PlcAddr XF_Trigger2StartPosiAck { get; set; }
        //         public PlcAddr YF_Trigger3StartPosi { get; set; }
        //         public PlcAddr XF_Trigger3StartPosiAck { get; set; }
        //         public PlcAddr YF_Trigger1EndPosi { get; set; }
        //         public PlcAddr XF_Trigger1EndPosiAck { get; set; }
        //         public PlcAddr YF_Trigger2EndPosi { get; set; }
        //         public PlcAddr XF_Trigger2EndPosiAck { get; set; }
        //         public PlcAddr YF_Trigger3EndPosi { get; set; }
        //         public PlcAddr XF_Trigger3EndPosiAck { get; set; }

        public PlcAddr[] XB_QuickSave0PosCmd { get; set; }        
        public PlcAddr[] XB_Position0MoveCmd { get; set; }
        public PlcAddr[] YB_Position0MoveCmdAck { get; set; }
        public PlcAddr[] YB_PositionComplete { get; set; }

        public PlcAddr XB_AxisImmediateStopCmd { get; set; }
        public PlcAddr YB_AxisImmediateStopCmdAck { get; set; }

        public PlcAddr[] XF_Position1stPoint { get; set; }
        public PlcAddr[] YF_Position1stPointAck { get; set; }
        public PlcAddr[] XF_Position1stSpeed { get; set; }
        public PlcAddr[] YF_Position1stSpeedAck { get; set; }
        public PlcAddr[] XF_Position1stAccel { get; set; }
        public PlcAddr[] YF_Position1stAccelAck { get; set; }
        #endregion

        public RSNMCServoController(RSNMCWorker nmcWorker, string name, ushort axisId)
        {
            this._nmcWorker = nmcWorker;
            this.Name = name;
            this.AxisID = axisId;            

            XB_QuickSave0PosCmd = new PlcAddr[MaxPositionCount];
            XB_Position0MoveCmd = new PlcAddr[MaxPositionCount];
            YB_Position0MoveCmdAck = new PlcAddr[MaxPositionCount];
            YB_PositionComplete = new PlcAddr[MaxPositionCount];

            XF_Position1stPoint = new PlcAddr[MaxPositionCount];
            YF_Position1stPointAck = new PlcAddr[MaxPositionCount];
            XF_Position1stSpeed = new PlcAddr[MaxPositionCount];
            YF_Position1stSpeedAck = new PlcAddr[MaxPositionCount];
            XF_Position1stAccel = new PlcAddr[MaxPositionCount];
            YF_Position1stAccelAck = new PlcAddr[MaxPositionCount];
        }              

        public void LogicWorking()
        {            
            CheckStatus();
            EmergencyStopStep();
            QuickSaveStep();            
            HomeStep();
            JogStep();

            for (int iter = 0; iter < MaxPositionCount; iter++)
                MoveStep(iter);

            OneWayCommandStep();
        }

        private bool _oldManualResetServoOn = false;
        private bool _oldManualServoOff = false;
        private void OneWayCommandStep()
        {
            if (_oldManualResetServoOn == true && XB_ManualResetServoOnCmd.vBit == false)
            {
                if (_isJogStepRunning == false
                    && _isHomeRunning == false
                    && _isMoveStepRunning == false)
                {
                    NMCSDKLib.MC_STATUS ms;
                    StringBuilder cstrErrorMsg = new StringBuilder(128);

                    ms = NMCSDKLib.MC_Reset(BoardID, AxisID);
                    if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                    {
                        NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                        _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);                        
                    }

                    ms = NMCSDKLib.MC_Power(BoardID, AxisID, true);
                    if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                    {
                        NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                        _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);                        
                    }
                }
            }
            else if (_oldManualServoOff == true && XB_ManualServoOffCmd.vBit == false)
            {
                if (_isJogStepRunning == false
                    && _isHomeRunning == false
                    && _isMoveStepRunning == false)
                {
                    NMCSDKLib.MC_STATUS ms;
                    StringBuilder cstrErrorMsg = new StringBuilder(128);

                    ms = NMCSDKLib.MC_Power(BoardID, AxisID, false);
                    if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                    {
                        NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                        _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);                        
                    }
                }                
            }

            _oldManualResetServoOn = XB_ManualResetServoOnCmd.vBit;
            _oldManualServoOff = XB_ManualServoOffCmd.vBit;
        }

        private void QuickSaveStep()
        {
            for (int iter = 0; iter < XB_QuickSave0PosCmd.Length; ++iter)
            {
                if (XB_QuickSave0PosCmd[iter].vBit == true)
                {
                    YF_Position1stPointAck[iter].vFloat = XF_Position1stPoint[iter].vFloat;
                }
            }
        }

        private void CheckStatus()
        {
            NMCSDKLib.MC_STATUS ms;
            double tempDouble = 0;

            ms = NMCSDKLib.MC_ReadAxisStatus(BoardID, this.AxisID, ref _readAxisStatus);
            if (ms != NMCSDKLib.MC_STATUS.MC_OK)
            {
                _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("Fail - Read Axis Status");
                _isErrorReadAxisStatus = true;
                _readAxisStatus = 0;
            }
            else
                _isErrorReadAxisStatus = false;

            if (IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcErrorStop) == true)
            {
                ushort errID = 0, errInfo = 0, errInfoEx = 0;
                StringBuilder cstrErrorMsg = new StringBuilder(128);

                ms = NMCSDKLib.MC_ReadAxisError(BoardID,
                    AxisID,
                    ref errID,
                    ref errInfo,
                    ref errInfoEx
                    );
                if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                {
                    NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                }
                else
                {
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("ErrorStop! Find Manual - ID:{0}, Info0:{1}, INfo1:{2}",
                        errID, errInfo, errInfoEx);
                }
            }

            ms = NMCSDKLib.MC_ReadActualPosition(BoardID, this.AxisID, ref tempDouble);
            YF_CurrMotorPosition.vFloat = (float)(ms == NMCSDKLib.MC_STATUS.MC_OK ? tempDouble : _errorValue);
            ms = NMCSDKLib.MC_ReadActualVelocity(BoardID, this.AxisID, ref tempDouble);
            YF_CurrMotorSpeed.vFloat = (float)(ms == NMCSDKLib.MC_STATUS.MC_OK ? tempDouble : _errorValue);

            YB_StatusHomeCompleteBit.vBit = IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcIsHomed);
            YB_StatusHomeInPosition.vBit = true
                && IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcIsHomed)
                && Math.Abs(YF_CurrMotorPosition.vFloat - 0) < Inpos;
            YB_StatusMotorMoving.vBit = false
                || IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcConstantVelocity)
                || IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcAccelerating)
                || IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcDecelerating)
                ;
            YB_StatusMotorInPosition.vBit = IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcMotionComplete);
            YB_StatusNegativeLimitSet.vBit = IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcLimitSwitchNeg);
            YB_StatusPositiveLimitSet.vBit = IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcLimitSwitchPos);
            YB_StatusMotorServoOn.vBit = IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcPowerOn);            
            YB_ErrDriveFaultError.vBit = IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcDriveFault);            
            YB_ErrErrorStop.vBit = IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcErrorStop)
                || IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcDisabled);
            YB_ErrAxisWarning.vBit = IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcAxisWarning);
            
            YF_MotorJogSpeedCmdAck.vFloat = XF_MotorJogSpeedCmd.vFloat;
            //             ms = NMCSDKLib.MC_ReadActualPosition(BoardID, this._axisId, ref tempDouble);
            //             YF_CurrMotorStress.vFloat = (float)(ms == NMCSDKLib.MC_STATUS.MC_OK ? tempDouble : 0);
        }
        #region Home
        private int OldHomeStepNo = 0;
        private int HomeStepNo = 0;
        private bool _isHomeRunning { get { return HomeStepNo != 0; } }
        private PlcTimer _plcTmrHome = new PlcTimer("Home Seq Timer");
        private PlcTimer _ampOnDelay = new PlcTimer("Amp On Delay");
        private int _homeVelocity = 50000;
        private int _homeAccel = 500000;
        private int _homeDecel = 500000;
        private int _homeJerk = 5000000;
        private int _homeCeepVelocity = 5000; // 홈 찾을 때 속도
        
        public void HomeStep()
        {
            NMCSDKLib.MC_STATUS ms;
            StringBuilder cstrErrorMsg = new StringBuilder(128);

            if (OldHomeStepNo != HomeStepNo)
            {
                RSNMCLogger.AppendLine("Axis {0}:{1} HomeStep {2}", AxisID, Name, HomeStepNo);
                OldHomeStepNo = HomeStepNo;
            }

            if (HomeStepNo != 0
                && XB_HomeCmd.vBit == true)
            {
                HomeStepNo = 0;
            }

            switch (HomeStepNo)
            {
                case 0:
                    if (XB_HomeCmd.vBit == true)
                    {
                        _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "";
                        YB_HomeCmdAck.vBit = true;
                        HomeStepNo = 10;
                    }
                    break;
                case 10:
                    if (XB_HomeCmd.vBit == false)
                    {
                        YB_HomeCmdAck.vBit = false;
                        HomeStepNo = 15;
                    }
                    break;
                case 15:
                    if (_isJogStepRunning)
                        _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "Jog 중 동작 불가";

                    if (_isMoveStepRunning)
                        _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "Jog 중 동작 불가";

                    if (_isJogStepRunning || _isMoveStepRunning)
                        HomeStepNo = 0;
                    else
                        HomeStepNo = 20;
                    break;
                case 20:
                    if (IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcErrorStop) == true)
                    {
                        ms = NMCSDKLib.MC_Reset(BoardID, AxisID);
                        if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                        {
                            NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                            _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                            HomeStepNo = 0;
                        }
                    }
                    else
                        HomeStepNo = 40;
                    break;
                case 40:
                    if(IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcPowerOn) == false)
                    {
                        ms = NMCSDKLib.MC_Power(BoardID, AxisID, true);
                        if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                        {
                            NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                            _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                            HomeStepNo = 0;
                        }
                    }
                    else
                        HomeStepNo = 50;
                    break;
                case 50:
                    if (IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcPowerOn) == true)
                    {
                        ms = NMCSDKLib.MC_WriteParameter(BoardID, AxisID, (uint)2102, _homeVelocity);
                        if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                        {
                            NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                            _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                            HomeStepNo = 0;
                        }
                        else
                        {
                            HomeStepNo = 51;
                        }
                    }
                    break;
                case 51:
                    if (IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcPowerOn) == true)
                    {
                        ms = NMCSDKLib.MC_WriteParameter(BoardID, AxisID, (uint)2103, _homeAccel);
                        if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                        {
                            NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                            _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                            HomeStepNo = 0;
                        }
                        else
                        {
                            HomeStepNo = 52;
                        }
                    }
                    break;
                case 52:
                    if (IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcPowerOn) == true)
                    {
                        ms = NMCSDKLib.MC_WriteParameter(BoardID, AxisID, (uint)2104, _homeDecel);
                        if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                        {
                            NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                            _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                            HomeStepNo = 0;
                        }
                        else
                        {
                            HomeStepNo = 53;
                        }
                    }
                    break;
                case 53:
                    if (IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcPowerOn) == true)
                    {
                        ms = NMCSDKLib.MC_WriteParameter(BoardID, AxisID, (uint)2105, _homeJerk);
                        if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                        {
                            NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                            _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                            HomeStepNo = 0;
                        }
                        else
                        {
                            HomeStepNo = 54;
                        }
                    }
                    break;
                case 54:
                    if (IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcPowerOn) == true)
                    {
                        ms = NMCSDKLib.MC_WriteParameter(BoardID, AxisID, (uint)2106, _homeCeepVelocity);
                        if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                        {
                            NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                            _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                            HomeStepNo = 0;
                        }
                        else
                        {
                            _ampOnDelay.Start(0, 500);
                            HomeStepNo = 60;
                        }
                    }
                    break;
                case 60:
                    if (_ampOnDelay)
                    {
                        ms = NMCSDKLib.MC_Home(BoardID,
                                                AxisID,
                                                0,
                                                NMCSDKLib.MC_BUFFER_MODE.mcAborting
                                                );

                        if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                        {
                            NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                            _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                            HomeStepNo = 0;
                        }
                        else
                        {
                            _ampOnDelay.Stop();
                            _plcTmrHome.Start(0, 1000);
                            HomeStepNo = 70;
                        }
                    }
                    break;
                case 70:
                    if (_isErrorReadAxisStatus == false
                        && _plcTmrHome
                        && IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcIsHomed))
                    {
                        _plcTmrHome.Stop();
                        HomeStepNo = 80;
                    }                    
                    break;
                case 80:
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "";
                    HomeStepNo = 0;
                    break;
            }
        }
        #endregion
        #region Jog
        private int JogPlusStep = 0;
        private int JogMinusStep = 0;        
        private bool _isJogStepRunning { get { return JogPlusStep != 0 || JogMinusStep != 0; } }
        public void JogStep()
        {
            NMCSDKLib.MC_STATUS mc;
            StringBuilder cstrErrorMsg = new StringBuilder(128);
                 
            if (_isMoveStepRunning == false
                && _isHomeRunning == false
                && XB_MotorJogPlusMove.vBit == false
                && XB_MotorJogMinusMove.vBit == false
                && YB_StatusMotorMoving.vBit == true
                )                    
            {
                mc = NMCSDKLib.MC_Halt(BoardID, AxisID,
                    1000000,
                    10000000,
                    NMCSDKLib.MC_BUFFER_MODE.mcAborting);
                _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "Motor Move Status Abnormal!";
            }

            if (JogPlusStep == 0)
            {
                if (XB_MotorJogPlusMove.vBit == true)
                {
                    JogPlusStep = 5;
                }              
            }
            else if (JogPlusStep == 5)
            {
                if (_isMoveStepRunning)
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "Move Step 중 동작 불가";

                if (_isHomeRunning)
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "Home 중 동작 불가";

                if (_isMoveStepRunning || _isHomeRunning)
                    JogPlusStep = 0;
                else
                    JogPlusStep = 10;
            }
            else if (JogPlusStep == 10)
            {
                if (YF_MotorJogSpeedCmdAck.vFloat > JogSpeedLimit)
                    YF_MotorJogSpeedCmdAck.vFloat = JogSpeedLimit;

                mc = NMCSDKLib.MC_MoveVelocity(BoardID, AxisID,
                    YF_MotorJogSpeedCmdAck.vFloat,
                    YF_MotorJogSpeedCmdAck.vFloat * 10,
                    YF_MotorJogSpeedCmdAck.vFloat * 10,
                    YF_MotorJogSpeedCmdAck.vFloat * 100,
                    NMCSDKLib.MC_DIRECTION.mcPositiveDirection,
                    NMCSDKLib.MC_BUFFER_MODE.mcAborting);
                if (mc != NMCSDKLib.MC_STATUS.MC_OK)
                {
                    mc = NMCSDKLib.MC_GetErrorMessage((uint)mc, (uint)128, cstrErrorMsg);
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("ret = {0,10:G},{0,10:X}", mc);
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg += "::" + cstrErrorMsg;
                    JogPlusStep = 0;
                }
                else
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "";
                JogPlusStep = 20;                
            }
            else if (JogPlusStep == 20)
            {
                if (XB_MotorJogPlusMove.vBit == false)
                {
                    mc = NMCSDKLib.MC_Halt(BoardID, AxisID,
                        1000000,
                        10000000,
                        NMCSDKLib.MC_BUFFER_MODE.mcAborting);
                    if (mc != NMCSDKLib.MC_STATUS.MC_OK)
                    {
                        mc = NMCSDKLib.MC_GetErrorMessage((uint)mc, (uint)128, cstrErrorMsg);
                        _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("ret = {0,10:G},{0,10:X}", mc);
                        _nmcWorker.AxisLastErrorInfo[AxisID].Msg += "::" + cstrErrorMsg;
                    }
                    else
                        _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "";
                    JogPlusStep = 0;
                }
            }

            if (JogMinusStep == 0)
            {
                if (XB_MotorJogMinusMove.vBit == true)
                {
                    JogMinusStep = 5;
                }
            }
            else if (JogMinusStep == 5)
            {
                if (_isMoveStepRunning)
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "Move Step 중 동작 불가";

                if (_isHomeRunning)
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "Home 중 동작 불가";

                if (_isMoveStepRunning || _isHomeRunning)
                    JogMinusStep = 0;
                else
                    JogMinusStep = 10;
            }
            else if (JogMinusStep == 10)
            {
                if (YF_MotorJogSpeedCmdAck.vFloat > JogSpeedLimit)
                    YF_MotorJogSpeedCmdAck.vFloat = JogSpeedLimit;

                mc = NMCSDKLib.MC_MoveVelocity(BoardID, AxisID,
                        YF_MotorJogSpeedCmdAck.vFloat,
                        YF_MotorJogSpeedCmdAck.vFloat * 10,
                        YF_MotorJogSpeedCmdAck.vFloat * 10,
                        YF_MotorJogSpeedCmdAck.vFloat * 100,
                        NMCSDKLib.MC_DIRECTION.mcNegativeDirection,
                        NMCSDKLib.MC_BUFFER_MODE.mcAborting);
                if (mc != NMCSDKLib.MC_STATUS.MC_OK)
                {
                    mc = NMCSDKLib.MC_GetErrorMessage((uint)mc, (uint)128, cstrErrorMsg);
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("ret = {0,10:G},{0,10:X}", mc);
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg += "::" + cstrErrorMsg;
                    JogPlusStep = 0;
                }
                else
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "";
                JogMinusStep = 20;
            }
            else if (JogMinusStep == 20)
            {
                if (XB_MotorJogMinusMove.vBit == false)
                {
                    mc = NMCSDKLib.MC_Halt(BoardID, AxisID,
                        1000000,
                        10000000,
                        NMCSDKLib.MC_BUFFER_MODE.mcAborting);
                    if (mc != NMCSDKLib.MC_STATUS.MC_OK)
                    {
                        mc = NMCSDKLib.MC_GetErrorMessage((uint)mc, (uint)128, cstrErrorMsg);
                        _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("ret = {0,10:G},{0,10:X}", mc);
                        _nmcWorker.AxisLastErrorInfo[AxisID].Msg += "::" + cstrErrorMsg;
                    }
                    else
                        _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "";
                    JogMinusStep = 0;
                }
            }
        }
        #endregion
        #region Move
        private int[] OldMoveStepNo = new int[MaxPositionCount];
        private int[] MoveStepNo = new int[MaxPositionCount];
        private bool _isMoveStepRunning { get { return MoveStepNo.ToList().TrueForAll(m => m == 0) == false; } }
        public void MoveStep(int iter)
        {
            NMCSDKLib.MC_STATUS ms;
            StringBuilder cstrErrorMsg = new StringBuilder(128);

            YB_PositionComplete[iter].vBit = true                
                && YB_StatusMotorMoving.vBit == false
                && YB_StatusMotorInPosition.vBit == true
                && YB_StatusMotorServoOn.vBit == true
                && (Math.Abs(YF_Position1stPointAck[iter].vFloat - YF_CurrMotorPosition.vFloat) < Inpos);

            if (OldMoveStepNo[iter] != MoveStepNo[iter])
            {
                RSNMCLogger.AppendLine("Axis {0}:{1} {2} Position MoveStep {3}", AxisID, Name, iter, MoveStepNo[iter]);
                OldMoveStepNo[iter] = MoveStepNo[iter];
            }

            if (MoveStepNo[iter] == 0)
            {
                if (XB_Position0MoveCmd[iter].vBit == true)
                {
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "";
                    YB_Position0MoveCmdAck[iter].vBit = true;
                    MoveStepNo[iter] = 10;
                }
            }
            else if (MoveStepNo[iter] == 10)
            {
                if (XB_Position0MoveCmd[iter].vBit == false)
                {
                    YB_Position0MoveCmdAck[iter].vBit = false;
                    MoveStepNo[iter] = 15;
                }
            }
            else if (MoveStepNo[iter] == 15)
            {
                if (_isJogStepRunning)
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "Jog 중 동작 불가";

                if (_isHomeRunning)
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "Home 중 동작 불가";

                if (_isJogStepRunning || _isHomeRunning)
                    MoveStepNo[iter] = 0;
                else
                    MoveStepNo[iter] = 20;
            }
            else if (MoveStepNo[iter] == 20)
            {                
                ms = NMCSDKLib.MC_MoveAbsolute(BoardID,
                               AxisID,
                               (double)YF_Position1stPointAck[iter].vFloat,
                               (double)YF_Position1stSpeedAck[iter].vFloat,
                               (double)YF_Position1stAccelAck[iter].vFloat,
                               (double)YF_Position1stAccelAck[iter].vFloat,
                               (double)YF_Position1stAccelAck[iter].vFloat * 10,
                               NMCSDKLib.MC_DIRECTION.mcCurrentDirection,
                               NMCSDKLib.MC_BUFFER_MODE.mcAborting
                               );

                if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                {
                    NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                    MoveStepNo[iter] = 0;
                }
                else
                    MoveStepNo[iter] = 30;
            }
            else if (MoveStepNo[iter] == 30)
            {
                if (YB_StatusMotorInPosition.vBit == true)
                {
                    MoveStepNo[iter] = 40;
                }
            }
            else if (MoveStepNo[iter] == 40)
            {
                if (YB_PositionComplete[iter].vBit == true)
                {
                    MoveStepNo[iter] = 50;
                }
            }
            else if (MoveStepNo[iter] == 50)
            {
                _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "";
                MoveStepNo[iter] = 0;
            }
        }
        #endregion        
        #region Emergency Stop
        private float decSpd = 2000000f;
        private int OldEmergencyStopStepNo = 0;
        private int EmergencyStopStepNo = 0;
        public bool IsEmergencyStopDone { get { return EmergencyStopStepNo == 0; } }
        public void EmergencyStopStep()
        {
            NMCSDKLib.MC_STATUS ms;
            StringBuilder cstrErrorMsg = new StringBuilder(128);
            

            if (OldEmergencyStopStepNo != EmergencyStopStepNo)
            {
                RSNMCLogger.AppendLine("Axis {0}:{1} EmergencyStopStep {2}", AxisID, Name, EmergencyStopStepNo);
                OldEmergencyStopStepNo = EmergencyStopStepNo;
            }

            if (EmergencyStopStepNo == 0)
            {
                if (XB_AxisImmediateStopCmd.vBit == true)
                {
                    YB_AxisImmediateStopCmdAck.vBit = true;
                    EmergencyStopStepNo = 10;
                }
            }
            else if (EmergencyStopStepNo == 10)
            {
                _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "";

                decSpd = Math.Abs(YF_CurrMotorSpeed.vFloat);
                if (decSpd < 200000)
                    decSpd = 200000;

                ms = NMCSDKLib.MC_Stop(BoardID, AxisID,
                    true,
                    decSpd * 10,
                    decSpd * 100
                    );

                if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                {
                    NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                    _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                    EmergencyStopStepNo = 0;
                }
                else
                    EmergencyStopStepNo = 20;

            }
            else if (EmergencyStopStepNo == 20)
            {
                if (decSpd < 200000)
                    decSpd = 200000;

                if (IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcMotionComplete) == true)
                {
                    ms = NMCSDKLib.MC_Stop(BoardID, AxisID,
                    false,
                    decSpd * 10,
                    decSpd * 100
                    );

                    if (ms != NMCSDKLib.MC_STATUS.MC_OK)
                    {
                        NMCSDKLib.MC_GetErrorMessage((uint)ms, (uint)128, cstrErrorMsg);
                        _nmcWorker.AxisLastErrorInfo[AxisID].Msg = String.Format("Error :: 0x{0:x}, {1}", ms, cstrErrorMsg);
                        EmergencyStopStepNo = 0;
                    }
                    else
                        EmergencyStopStepNo = 30;
                }
            }
            else if (EmergencyStopStepNo == 30)
            {
                if (IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS.mcStandStill) == true)
                {
                    for (int iter = 0; iter < MaxPositionCount; ++iter )
                    {
                        YB_Position0MoveCmdAck[iter].vBit = false;
                        MoveStepNo[iter] = 0;
                    }

                    EmergencyStopStepNo = 40;
                }
            }
            else if (EmergencyStopStepNo == 40)
            {
                if (XB_AxisImmediateStopCmd.vBit == false)                
                    YB_AxisImmediateStopCmdAck.vBit = false;

                _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "";
                EmergencyStopStepNo = 0;
            }
        }
        #endregion
        public bool IsAxisStatusOn(NMCSDKLib.MC_AXISSTATUS axisStatus)
        {
            NMCSDKLib.MC_AXISSTATUS ret = (NMCSDKLib.MC_AXISSTATUS)_readAxisStatus;
            return ret.HasFlag(axisStatus);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Emergency Stop이 시작되면 true 반환, 진행 중이면 false 반환</returns>
        public bool StartEmergencyStop()
        {
            if (IsEmergencyStopDone == true)
            {
                _nmcWorker.AxisLastErrorInfo[AxisID].Msg = "";
                EmergencyStopStepNo = 10;
                return true;
            }
            else
                return false;
        }
        public void SaveSetting()
        {
            for (int iter = 0; iter < MaxPositionCount; ++iter )
            {
                YF_Position1stPointAck[iter].vFloat = XF_Position1stPoint[iter].vFloat;
                YF_Position1stSpeedAck[iter].vFloat = XF_Position1stSpeed[iter].vFloat;
                YF_Position1stAccelAck[iter].vFloat = XF_Position1stAccel[iter].vFloat;
            }
        }
    }
}
