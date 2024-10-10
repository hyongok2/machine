using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using EquipMainUi.Setting;
using EquipMainUi.Struct.Detail;

namespace EquipMainUi.Struct
{
    public enum EmMotorType
    {
        Linear,
        DD
    }
    public enum MoveCommand
    {
        USER_PTP = -1,
        NO_1_POSI = 0,
        NO_2_POSI,
        NO_3_POSI,
        NO_4_POSI,
        NO_5_POSI,
        NO_6_POSI,
        NO_7_POSI,
        NO_8_POSI,
        NO_9_POSI,
        NO_10_POSI,
        NO_11_POSI,
        NO_12_POSI,
        NO_13_POSI,
        NO_14_POSI,
        NO_15_POSI,
        NO_16_POSI,
        NO_17_POSI,
        NO_18_POSI,
        NO_19_POSI,
        NO_20_POSI,
        NO_21_POSI,
        NO_22_POSI,
        NO_23_POSI,
        NO_24_POSI,
        NO_25_POSI,
        NO_26_POSI,
        NO_27_POSI,
        NO_28_POSI,
        NO_29_POSI,
        HOME_POSI,
        START_POSI,
        JOG,
    }
    public enum EM_SERVO_JOG
    {
        JOG_PLUS = 1,
        JOG_STOP = 0,
        JOG_MINUS = -1,
    }
    /// <summary>
    /// 
    /// date : 190325
    /// </summary>
    public class ServoMotorPmac : UnitBase
    {
        public EmMotorType MotorType;
        public int InnerAxisNo = 0;
        public int OuterAxisNo = 0;

        public static int HomePosition = 0;
        public double SoftMinusLimit = 10;
        public double SoftPlusLimit = 10;
        public int SoftSpeedLimit = 10;
        public int SoftJogSpeedLimit = 10;
        public int EnableGripJogSpeedLimit = 30;
        public float InposOffset = 0.1f;
        public int SoftAccelPlusLimit = 1000;
        public int SoftAccelMinusLimit = 200;
                
        public string Name;
        public PMacServoSetting Setting { get; set; }

        public PlcAddr XB_StatusHomeCompleteBit { get; set; }
        public PlcAddr XB_StatusHomeInPosition { get; set; }
        public PlcAddr XB_StatusMotorMoving { get; set; }
        public PlcAddr XB_StatusHWNegativeLimitSet { get; set; }
        public PlcAddr XB_StatusHWPositiveLimitSet { get; set; }
        public PlcAddr XB_StatusSWNegativeLimitSet { get; set; }
        public PlcAddr XB_StatusSWPositiveLimitSet { get; set; }
        public PlcAddr XB_StatusMotorServoOn { get; set; }

        public PlcAddr XB_TargetPosMoveComplete { get; set; }

        public PlcAddr XB_ErrFatalFollowingError { get; set; }
        public PlcAddr XB_ErrAmpFaultError { get; set; }
        public PlcAddr XB_ErrI2TAmpFaultError { get; set; }
        public PlcAddr XB_ErrCommonAlarm { get; set; }        

        public PlcAddr XF_CurrMotorPosition { get; set; }
        public PlcAddr XF_CurrMotorSpeed { get; set; }
        public PlcAddr XF_CurrMotorStress { get; set; }
        public PlcAddr XF_TargetPosition { get; set; }
        public PlcAddr XF_TargetSpeed { get; set; }
        public PlcAddr XF_TargetAccTime { get; set; }
        public PlcAddr XF_JogSpeed { get; set; }
                
        public PlcAddr YF_TargetPosition { get; set; }
        public PlcAddr YF_TargetSpeed { get; set; }
        public PlcAddr YF_TargetAccTime { get; set; }
        public PlcAddr YF_JogSpeed { get; set; }
        
        public PlcAddr YB_HomeCmd { get; set; }
        public PlcAddr XB_HomeCmdAck { get; set; }

        public PlcAddr YB_TargetPosMoveCmd { get; set; }
        public PlcAddr XB_TargetPosMoveAck { get; set; }

        public PlcAddr YB_JogMinusMove { get; set; }
        public PlcAddr XB_JogMinusMove { get; set; }
        public PlcAddr YB_JogPlusMove { get; set; }
        public PlcAddr XB_JogPlusMove { get; set; }        

        public PlcAddr YF_LoadRate { get; set; }
        public PlcAddr XF_LoadRate { get; set; }

        public MccActionItem MccHomeActionOnOff { get; set; }
        public MccActionItem[] MccMoveActionOnOff { get; set; }

        public int PositionCount { get; set; }        
        protected int _ptpStep;
        protected int _ptpTimeout;
        protected DateTime _ptpStartTime;
        protected DateTime _ptpEndTime;        
        protected int _ptpCompleteDelayms = 100;
        protected PlcTimer _ptpCompleteDelay;
        public double PositionMoveTime { get { return (_ptpEndTime - _ptpStartTime).TotalMilliseconds; } }
        public int PtpStep {  get { return _ptpStep; } }

        protected int _homeStep = 0;
        public int MOVE_HOME_OVERTIME;
        protected DateTime _homeStartTime = DateTime.Now;
        protected DateTime _homeEndTime = DateTime.Now;        
        private int _homeCompleteDelayms = 100;
        private PlcTimer _homeCompleteDelay;
        public double HomeMoveTime { get { return (_homeEndTime - _homeStartTime).TotalMilliseconds; } }
        public int HomeStep { get { return _homeStep; } }

        protected bool _isEms = false;

        protected int DefaultPtpTimeOut = 15000;
        protected int DefaultHomeTimeOver = 60000;

        #region 알람
        public EM_AL_LST HW_PLUS_LIMIT_ERROR { get; set; }
        public EM_AL_LST HW_MINUS_LIMIT_ERROR { get; set; }
        public EM_AL_LST SW_PLUS_LIMIT_ERROR { get; set; }        
        public EM_AL_LST SW_MINUS_LIMIT_ERROR { get; set; }
        public EM_AL_LST MOTOR_SERVO_ON_ERROR { get; set; }
        public EM_AL_LST PTP_WRITE_TIMEOUT_ERROR { get; set; }
        public EM_AL_LST MOVE_OVERTIME_ERROR { get; set; } 

        public EM_AL_LST FATAL_FOLLOWING_ERROR { get; set; }
        public EM_AL_LST AMP_FAULT_ERROR { get; set; }
        public EM_AL_LST I2T_AMP_FAULT_ERROR { get; set; }

        public EM_AL_LST HOME_CMD_ACK_ERROR { get; set; }
        public EM_AL_LST PTP_CMD_ACK_ERROR { get; set; }        
        #endregion

        public ServoMotorPmac(int innerAxisNo, int outerAxisNo, string name, EmMotorType type, int positionCount = 0)
        {
            this.Name = name;
            InnerAxisNo = innerAxisNo;
            OuterAxisNo = outerAxisNo;
            PositionCount = positionCount;
            MotorType = type;
            Setting = new PMacServoSetting();
                        
            MccHomeActionOnOff = MccActionItem.NONE;
            MccMoveActionOnOff = new MccActionItem[PositionCount];            

            _ptpCompleteDelay = new PlcTimer();
            _homeCompleteDelay = new PlcTimer();
        }
        public void Reset()
        {
            _homeStartTime = DateTime.Now.AddDays(-1);
            _ptpStartTime = DateTime.Now.AddDays(-1);
        }

        public bool IsHomeCompleteBit { get { return XB_StatusHomeCompleteBit.vBit; } }
        public bool IsHomeInposition { get { return XB_StatusHomeInPosition.vBit; } }
        public virtual bool IsMoving { get { return XB_StatusMotorMoving.vBit; } }
        public bool IsHWNegativeLimit { get { return XB_StatusHWNegativeLimitSet.vBit; } }
        public bool IsHWPositiveLimit { get { return XB_StatusHWPositiveLimitSet.vBit; } }
        public bool IsSWNegativeLimit { get { return XB_StatusSWNegativeLimitSet.vBit; } }
        public bool IsSWPositiveLimit { get { return XB_StatusSWPositiveLimitSet.vBit; } }
        public bool IsServoOn { get { return XB_StatusMotorServoOn.vBit; } }

        public bool IsFatalFollowingError { get { return XB_ErrFatalFollowingError.vBit; } }
        public bool IsAmpFaultError { get { return XB_ErrAmpFaultError.vBit; } }
        public bool IsI2TAmpFaultError { get { return XB_ErrI2TAmpFaultError.vBit; } }
        public bool IsCommonAlarm { get { return XB_ErrCommonAlarm.vBit; } }

        public bool IsServoError { get { 
            return IsHomeCompleteBit == false
                || IsServoOn == false 
                || IsHWNegativeLimit 
                || IsHWPositiveLimit 
                || IsSWNegativeLimit 
                || IsSWPositiveLimit             
                || IsFatalFollowingError
                || IsAmpFaultError 
                || IsI2TAmpFaultError
                || IsCommonAlarm; } }
        public bool IsFatalServoError { get { return IsFatalFollowingError || IsAmpFaultError || IsI2TAmpFaultError; } }
        private bool _isHappenAlarm = false;
        public bool IsHappenAlarm { get { return _isHappenAlarm; } }

        public override void LogicWorking(Equipment equip)
        {
            LogicHome(equip);
            LogicPtpMove(equip);
            CheckServoStatus(equip);
        }

        #region status
        private bool IsErrorCmdValue(double pos, double spd)
        {
            if (pos > this.SoftPlusLimit)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("{0} 位置值比最大值  {1}大 .", pos, this.SoftPlusLimit) : string.Format("{0} 위치값이 최대값({1})보다 큽니다.", pos, this.SoftPlusLimit));
                return true;
            }
            if (pos < this.SoftMinusLimit)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("{0} 位置值比最小值  {1}小 .", pos, this.SoftMinusLimit) : string.Format("{0} 위치값이 최소값({1})보다 작습니다.", pos, this.SoftMinusLimit));
                return true;
            }
            if (spd > this.SoftSpeedLimit)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("{0} 移动速度比最大值 {1}大.", spd, this.SoftSpeedLimit) : string.Format("{0} 이동 속도 최대값({1})보다 큽니다.", spd, this.SoftSpeedLimit));
                return true;
            }
            return false;
        }
        public virtual bool IsStepEnd
        {
            get
            {
                bool step = (_ptpStep == 0);
                return step && !IsMoving;
            }
        }        
        public virtual bool IsMoveOnPosition(double destPos)
        {
            return IsMoveOnPosition((float)destPos);
        }
        public virtual bool IsMoveOnPosition(float destPos)
        {
            return Math.Abs(XF_CurrMotorPosition.vFloat - destPos) <= InposOffset
                && XB_TargetPosMoveComplete.vBit
                && IsMoving == false
                && _ptpStep == 0
                && IsHomeCompleteBit;
        }
        public virtual bool IsMoveOnPosition(int posiNo)
        {
            if (GG.TestMode)
                return XB_TargetPosMoveComplete.vBit && IsMoving == false && _ptpStep == 0;
            else
                return Math.Abs(XF_CurrMotorPosition.vFloat - this.Setting.LstServoPosiInfo[posiNo].Position) <= InposOffset
                    && XB_TargetPosMoveComplete.vBit
                    && IsMoving == false
                    && _ptpStep == 0
                    && IsHomeCompleteBit;
        }
        public virtual bool IsMovingOnPosition(int posiNo)
        {
            return _ptpStep != 0;
        }
        public virtual bool IsMovingStep()
        {
            bool isMovingStep = false;

            if (_ptpStep != 0)
            {
                isMovingStep = true;
            }

            return IsMovingOnHome() || isMovingStep;
        }
        public virtual bool IsMovingOnHome()
        {
            return _homeStep != 0;
        }
        #endregion
        #region Home
        
        public virtual bool IsHomeComplete()
        {
            return IsHomeCompleteBit == true
                && IsServoOn == true
                && IsServoError == false
                && IsHappenAlarm == false
                //&& IsMoving == false
                && IsHomming == false;
        }
        public virtual bool IsHomeOnPosition()
        {
            return IsHomeInposition == true
                && IsHomeComplete();
        }
        public virtual bool GoHomeOrPositionOne(Equipment equip)
        {
            if (IsServoError == false && IsHappenAlarm == false && GG.TestMode == false)
            {
                return MovePosition(equip, 0, 50, 300);
            }
            else
            {
                return GoHome(equip);
            }
        }        
        public virtual bool GoHome(Equipment equip)
        {
            if (IsStartedStep(equip, MoveCommand.HOME_POSI, "HOME") == true) return false;
            if (CheckStartMoveInterLock(equip, MoveCommand.HOME_POSI))
            {
                //IsInterLockError = true;
                return false;
            }
            else
            {
                if (SetEquipState(equip) == false) return false;
                _homeStep = 10;
                _isHappenAlarm = false;
                return true;
            }
        }
        public void HomeStepReset()
        {
            StepReset(0);
            YB_HomeCmd.vBit = false;
            _homeStep = 0;
        }
        public bool IsHomming { get { return _homeStep != 0; } }
        public void LogicHome(Equipment equip)
        {
            if (_isEms)
            {
                if (equip.PMac.IsCommandAck(equip, EmPMacmd.IMMEDIATE_STOP))
                {
                    _isEms = false;
                }
            }
            else if (IsMoving
                && _homeStep != 0
                && CheckMovingInterLock(equip, MoveCommand.HOME_POSI, ref _homeStep) == true)
            {
                equip.PMac.StartCommand(equip, EmPMacmd.IMMEDIATE_STOP, 0);
                _homeStep = 0;
                _isEms = true;
                return;
            }
            if (equip.IsImmediatStop)
            {
                AllCmdOff();
            }

            if (_homeStep == 30 && (DateTime.Now - _homeStartTime).TotalMilliseconds > 5000)
            {
                //Logger.WriteMotor(this, "ACK OVERTIME", actionName, (float)Setting.GetPosition((int)cmd), (float)Setting.GetSpeed((int)cmd), stepMove, string.Format("[ACK SIGNAL TIME OUT : {0}]", moveTimeout));
                AlarmMgr.Instance.Happen(equip, HOME_CMD_ACK_ERROR);
                //equip.IsNeedSeq = EMNeedSeq.Home;
                _homeStartTime = DateTime.Now;
                _homeStep = 0;
                return;
            }

            if (_homeStep > 30 && (DateTime.Now - _homeStartTime).TotalMilliseconds > MOVE_HOME_OVERTIME)
            {
                //Logger.WriteMotor(this, "MOVE OVERTIME", actionName, (float)Setting.GetPosition((int)cmd), (float)Setting.GetSpeed((int)cmd), stepMove, string.Format("[NOVE TIME OUT : {0}]", moveTimeout));
                AlarmMgr.Instance.Happen(equip, MOVE_OVERTIME_ERROR);
                //equip.IsNeedSeq = EMNeedSeq.Home;
                _homeStartTime = DateTime.Now;
                _homeStep = 0;
                return;
            }

            if (_homeStep == 0)
            {
                //YB_HomeCmd.vBit = false;
            }
            else if (_homeStep == 10)
            {
                //_tempStr = string.Format("[HOME] {0} Axis Homming Move Start Current Position  {1}, CMD ACK OK",
                //    Name, XF_CurrMotorPosition.vFloat);
                //Logger.Log.AppendLine(LogLevel.Info, _tempStr);
                //Logger.ServoLog.AppendLine(LogLevel.Info, _tempStr);
                _isHappenAlarm = false;
                _homeStartTime = DateTime.Now;
                _homeStep = 20;
            }
            else if (_homeStep == 20)
            {
                YB_HomeCmd.vBit = true;
                _homeStep = 30;
            }
            else if (_homeStep == 30)
            {
                if (XB_HomeCmdAck.vBit == true)
                {
                    YB_HomeCmd.vBit = false;
                    _homeStep = 40;
                }
            }
            else if (_homeStep == 40)
            {
                if (IsMoving == false
                    && IsServoOn == true
                    && XB_StatusHomeCompleteBit.vBit == true
                    && XB_StatusHomeInPosition.vBit == true)
                {
                    _homeCompleteDelay.Start(0, _homeCompleteDelayms);
                    _homeStep = 50;
                }
            }
            else if (_homeStep == 50)
            {
                if (_homeCompleteDelay)
                {
                    _homeCompleteDelay.Stop();
                    _homeStep = 60;
                }
            }
            else if (_homeStep == 60)
            {
                _isHappenAlarm = false;
                //_tempStr = string.Format("[HOME] {0} Axis Homming Move Command End Current Position  {1} CMD ACK OK", Name, XF_CurrMotorPosition.vFloat);
                //Logger.Log.AppendLine(LogLevel.Info, _tempStr);
                //Logger.ServoLog.AppendLine(LogLevel.Info, _tempStr);
                _homeStep = 0;
            }
        }
        #endregion
        #region Jog
        protected bool JogMoveInterlock(Equipment equip, EM_SERVO_JOG jog, float speed)
        {
            if (equip.IsPause)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<PAUSE>\n(设备状态在 PAUSE 状态下， 轴={0}, 方向={1}, 速度= {2} ，禁止操作. )", Name, jog, speed) :
                    string.Format("인터락<PAUSE>\n(설비 상태가 PAUSE 상태에서 축={0},방향={1}, 속도={2} 조작이 금지됩니다.)", Name, jog, speed));
                Logger.Log.AppendLine(LogLevel.Warning, "설비 상태가 Puase 상태에서 축={0},방향={1}, 속도={2} 조작 금지됨", Name, jog, speed);
                return false;
            }

            if (equip.IsImmediatStop)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<EMERGENCY>\n(设备状态在 EMERGENCY 状态下， 轴={0} ,方向={1}, 速度= {2}，禁止操作 .)", Name, jog, speed) :
                    string.Format("인터락<EMERGENCY>\n(설비 상태가 EMERGENCY 상태에서 축={0},방향={1}, 속도={2} 조작이 금지됩니다.)", Name, jog, speed));
                Logger.Log.AppendLine(
                    LogLevel.Warning, "설비 상태가 Emegency 상태에서 축={0},방향={1}, 속도={2} 조작 금지됨", Name, jog, speed);
                return false;
            }

            if (IsStepEnd == false)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<执行中>\n(轴={0}在移动中，禁止操作.)", Name) :
                    string.Format("인터락<실행중>\n(축={0}이 이동중에 있어 조작이 금지됩니다.)", Name));
                Logger.Log.AppendLine(LogLevel.Warning, "축={0}, 이동중 조작 금지!", Name);
                return false;
            }

            if (CheckStartMoveInterLock(equip, MoveCommand.JOG))
            {
                //IsInterLockError = true;
                return false;
            }
            if (SetEquipState(equip) == false) return false;
            return true;
        }
        public bool JogMove(Equipment equip, EM_SERVO_JOG jog, float speed)
        {
            if (jog == EM_SERVO_JOG.JOG_STOP)
            {
                this.YF_JogSpeed.vFloat = speed;
                this.YB_JogMinusMove.vBit = false;
                this.YB_JogPlusMove.vBit = false;
                return true;
            }

            if (JogMoveInterlock(equip, jog, (int)speed) == false)
            {
                this.YF_JogSpeed.vFloat = speed;
                this.YB_JogMinusMove.vBit = false;
                this.YB_JogPlusMove.vBit = false;
                return false;
            }

            //             if (equip.IsEnableGripSwOn == EmGrapSw.MIDDLE_ON)
            //                 speed = (speed < EnableGripJogSpeedLimit) ? speed : EnableGripJogSpeedLimit;

            Logger.Log.AppendLine(LogLevel.Info, "{0} 모터 {1} 속도로 {2} 이동", Name, jog, speed);
            if (jog == EM_SERVO_JOG.JOG_MINUS)
            {
                this.YF_JogSpeed.vFloat = speed;
                this.YB_JogMinusMove.vBit = true;
                this.YB_JogPlusMove.vBit = false;
            }
            else if (jog == EM_SERVO_JOG.JOG_PLUS)
            {
                this.YF_JogSpeed.vFloat = speed;
                this.YB_JogMinusMove.vBit = false;
                this.YB_JogPlusMove.vBit = true;
            }
            return true;
        }
        #endregion
        #region Ptp
        private bool _ptpRetry; 
        private int _oldPtpStep = 0;
        private void LogicPtpMove(Equipment equip)
        {
            if (_isEms)
            {
                if (equip.PMac.IsCommandAck(equip, EmPMacmd.IMMEDIATE_STOP))
                {
                    _isEms = false;
                }
            }
            else if (IsMoving
                && _ptpStep != 0
                && CheckMovingInterLock(equip, MoveCommand.NO_1_POSI, ref _ptpStep) == true)
            {
                equip.PMac.StartCommand(equip, EmPMacmd.IMMEDIATE_STOP, 0);
                _ptpStep = 0;
                _isEms = true;
                return;
            }
                        
            if (equip.IsImmediatStop)
            {
                AllCmdOff();
            }

            if ((_ptpStep == 30 || _ptpStep == 40) && (DateTime.Now - _ptpStartTime).TotalMilliseconds > 5000)
            {
                Logger.Log.AppendLine(LogLevel.NoLog, "ACK OVERTIME {0}: LastPmacRead {1}, LastPmacWrite {2} (step:{3})",
                    Name, equip.LastPmacRaedTime.ToString("hh:mm:ss.fff"), equip.LastPmacWriteTime.ToString("hh:mm:ss.fff"), _ptpStep);
                AlarmMgr.Instance.Happen(equip, PTP_CMD_ACK_ERROR);
                equip.IsNeedRestart = true;
                _ptpStartTime = DateTime.Now;
                _ptpStep = 0;
                return;
            }

            if (_ptpStep == 20 && (DateTime.Now - _ptpStartTime).TotalMilliseconds > 5000)
            {
                Logger.Log.AppendLine(LogLevel.NoLog, "WRITE PTP INFO OVERTIME {0}: LastPmacRead {1}, LastPmacWrite {2}, YF_POS {3}, YF_SPD {4}, YF_ACC {5}, XF_POS {6}, XF_SPD {7}, XF_ACC {8}",
                    Name, equip.LastPmacRaedTime.ToString("hh:mm:ss.fff"), equip.LastPmacWriteTime.ToString("hh:mm:ss.fff"),
                    YF_TargetPosition.vFloat, YF_TargetSpeed.vFloat, YF_TargetAccTime.vFloat,
                    XF_TargetPosition.vFloat, XF_TargetSpeed.vFloat, XF_TargetAccTime.vFloat);
                
                Logger.Log.AppendLine(LogLevel.Info, "[pp_Cmd]\nStage X pp_Cmd : {0}\nStage Y pp_Cmd : {1}\nStage T pp_Cmd : {2}",
                    equip.PMac.XF_StageXPPCmd.vFloat, equip.PMac.XF_StageYPPCmd.vFloat, equip.PMac.XF_StageTPPCmd.vFloat);
                Logger.Log.AppendLine(LogLevel.Info, "[HmSeq]\nStage X HmSeq : {0}\nStage Y HmSeq : {1}\nStage T HmSeq : {2}",
                    equip.PMac.XF_StageXHmSeq.vFloat, equip.PMac.XF_StageYHmSeq.vFloat, equip.PMac.XF_StageTHmSeq.vFloat);
                Logger.Log.AppendLine(LogLevel.Info, "[Pmac Scan Step] : {0}",
                    equip.PMac.XF_ScanStep.vFloat);

                Logger.Log.AppendLine(LogLevel.Info, "[현재 값]\nXB_TargetPosMoveComplete.vBit : {0}\n(XF_CurrMotorPosition - _ptpPosition ) <= InposOffset: {1}", 
                    XB_TargetPosMoveComplete.vBit, Math.Abs(XF_CurrMotorPosition.vFloat - _ptpPosition) <= InposOffset);

                Logger.Log.AppendLine(LogLevel.Info, "[MtrTargetPos]\nMtr01TargetPos : {0}\nMtr02TargetPos : {1}\nMtr03TargetPos : {2}\n"
                    , equip.PMac.XF_Mtr01TargetPos.vFloat, equip.PMac.XF_Mtr02TargetPos.vFloat, equip.PMac.XF_Mtr03TargetPos.vFloat);

                AlarmMgr.Instance.Happen(equip, PTP_WRITE_TIMEOUT_ERROR);
                equip.IsNeedRestart = true;
                _ptpStartTime = DateTime.Now;
                _ptpStep = 0;

                return;
            }

            if (_ptpStep > 35 && (DateTime.Now - _ptpStartTime).TotalMilliseconds > _ptpTimeout)
            {
                Logger.Log.AppendLine(LogLevel.NoLog, "MOVE OVERTIME {0}: LastPmacRead {1}, LastPmacWrite {2}, Y Target {3}, X Target {4}, Cur Pos {5}",
                    Name, equip.LastPmacRaedTime.ToString("hh:mm:ss.fff"), equip.LastPmacWriteTime.ToString("hh:mm:ss.fff"),
                    YF_TargetPosition.vFloat, XF_TargetPosition.vFloat, XF_CurrMotorPosition.vFloat);

                Logger.Log.AppendLine(LogLevel.Info, "[pp_Cmd]\nStage X pp_Cmd : {0}\nStage Y pp_Cmd : {1}\nStage T pp_Cmd : {2}",
                                    equip.PMac.XF_StageXPPCmd.vFloat, equip.PMac.XF_StageYPPCmd.vFloat, equip.PMac.XF_StageTPPCmd.vFloat);
                Logger.Log.AppendLine(LogLevel.Info, "[HmSeq]\nStage X HmSeq : {0}\nStage Y HmSeq : {1}\nStage T HmSeq : {2}",
                    equip.PMac.XF_StageXHmSeq.vFloat, equip.PMac.XF_StageYHmSeq.vFloat, equip.PMac.XF_StageTHmSeq.vFloat);
                Logger.Log.AppendLine(LogLevel.Info, "[Pmac Scan Step] : {0}",
                    equip.PMac.XF_ScanStep.vFloat);

                Logger.Log.AppendLine(LogLevel.Info, "[현재 값]\nXB_TargetPosMoveComplete.vBit : {0}\n(XF_CurrMotorPosition - _ptpPosition ) <= InposOffset: {1}",
                    XB_TargetPosMoveComplete.vBit, Math.Abs(XF_CurrMotorPosition.vFloat - _ptpPosition) <= InposOffset);

                Logger.Log.AppendLine(LogLevel.Info, "[MtrTargetPos]\nMtr01TargetPos : {0}\nMtr02TargetPos : {1}\nMtr03TargetPos : {2}\n"
                    , equip.PMac.XF_Mtr01TargetPos.vFloat, equip.PMac.XF_Mtr02TargetPos.vFloat, equip.PMac.XF_Mtr03TargetPos.vFloat);
                if (_ptpRetry)
                {
                    _ptpRetry = false;
                    AlarmMgr.Instance.Happen(equip, MOVE_OVERTIME_ERROR);
                    equip.IsNeedRestart = true;
                    _ptpStartTime = DateTime.Now;
                    _ptpStep = 0;
                }
                else
                {
                    Logger.Log.AppendLine(LogLevel.Info, "{0}축 PTP Move Over Time 발생, PTP 재시도", Name);
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0481_PTP_FAIL_RETRY);
                    _ptpRetry = true;
                    _ptpStartTime = DateTime.Now;
                    _ptpStep = 10;
                }
                return;
            }

            if (_ptpStep != _oldPtpStep)
            {
                _oldPtpStep = _ptpStep;
            }

            if (_ptpStep == 0)
            {
                //YB_TargetPosMoveCmd.vBit = false;
            }
            else if (_ptpStep == 10)
            {
                _ptpStartTime = DateTime.Now;
                //equip.MccPc.SetMccAction(mccActionOnOff);                

                YF_TargetPosition.vFloat = _ptpPosition;
                YF_TargetSpeed.vFloat = _ptpSpeed;
                YF_TargetAccTime.vFloat = _ptpAccel;

                Logger.Log.AppendLine(LogLevel.Info, "{0}가 현재위치 {1} -> {2} 이동 시작 (spd:{3},accTime:{4}", Name,
                    XF_CurrMotorPosition.vFloat, YF_TargetPosition.vFloat, YF_TargetSpeed.vFloat, YF_TargetAccTime.vFloat);                
                
                _ptpStep = 20;
            }
            else if (_ptpStep == 20)
            {
                if (Math.Abs(_ptpPosition - YF_TargetPosition.vFloat) < InposOffset && Math.Abs(_ptpPosition - XF_TargetPosition.vFloat) < InposOffset &&
                       Math.Abs(_ptpSpeed - YF_TargetSpeed.vFloat) < InposOffset && Math.Abs(_ptpSpeed - XF_TargetSpeed.vFloat) < InposOffset &&
                       Math.Abs(_ptpAccel - YF_TargetAccTime.vFloat) < InposOffset && Math.Abs(_ptpAccel - XF_TargetAccTime.vFloat) < InposOffset)
                {
                    YB_TargetPosMoveCmd.vBit = true;
                    _ptpStep = 30;
                }
                else
                {
                    YF_TargetPosition.vFloat = _ptpPosition;
                    YF_TargetSpeed.vFloat = _ptpSpeed;
                    YF_TargetAccTime.vFloat = _ptpAccel;
                }
            }
            else if (_ptpStep == 30)
            {
                if (XB_TargetPosMoveAck.vBit == true)
                {
                    _ptpStep = 35;
                }
            }
            else if (_ptpStep == 35)
            {                
                YB_TargetPosMoveCmd.vBit = false;
                _ptpStep = 40;
            }
            else if (_ptpStep == 40)
            {
                if (XB_TargetPosMoveAck.vBit == false)
                {
                    _ptpStep = 50;
                }
            }
            else if (_ptpStep == 50)
            {
                if (IsMoving == false
                    && IsHomeCompleteBit
                    && XB_TargetPosMoveComplete.vBit
                    && IsMoving == false
                    && Math.Abs(XF_CurrMotorPosition.vFloat - _ptpPosition) <= InposOffset
                    )
                {
                    _ptpCompleteDelay.Start(0, _ptpCompleteDelayms);
                    _ptpStep = 60;
                }
            }
            else if (_ptpStep == 60)
            {
                if (_ptpCompleteDelay)
                {
                    _ptpCompleteDelay.Stop();
                    _ptpStep = 70;
                }
            }
            else if (_ptpStep == 70)
            {
                //_tempStr = string.Format("[PTP] {0} Axis Move End Current Position  {1}, Target Position = {2}, Speed ={3}, Accel = {4} CMD ACK OK", Name, XF_CurrMotorPosition.vFloat, _position, _speed, _accel);
                //Logger.Log.AppendLine(LogLevel.Info, _tempStr);
                //Logger.ServoLog.AppendLine(LogLevel.Info, _tempStr);
                _ptpEndTime = DateTime.Now;
                Logger.Log.AppendLine(LogLevel.Info, "{0} [타겟 : {1}] [현재: {2}] PTP 이동 완료", Name,
                    XF_CurrMotorPosition.vFloat, YF_TargetPosition.vFloat);
                _ptpStep = 0;
                _ptpRetry = false;
            }
        }

        
        public void StepReset(int posiNo)
        {
            YB_TargetPosMoveCmd.vBit = false;
            _ptpStep = 0;
        }
        public bool IsStartedStep(Equipment equip, MoveCommand posiNo, string actionName)
        {
            if (_homeStep != 0)
            {
                InterLockMgr.AddInterLock(string.Format(GG.boChinaLanguage ? "Interlock<执行中>\n({0} 移动中，介入了 {1} 移动命令.)" : "인터락<실행중>\n({0} 홈이동중에 {1} 이동 명령이 들어왔습니다.)", Name, actionName));
                Logger.Log.AppendLine(LogLevel.Warning, "{0} 홈이동중에 {1} 이동 명령이 들어옴.", Name, actionName);
                return true;
            }
            if (_ptpStep != 0)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<执行中>\n({0}轴往 {2}移动中介入了 {1} 移动命令)", Name, actionName, _ptpPosition) : string.Format("인터락<실행중>\n({0}축 {2}으로 이동중에 {1} 이동 명령이 들어왔습니다)", Name, actionName, _ptpPosition));
                Logger.Log.AppendLine(LogLevel.Warning, "{0}축 {2}으로 이동중에 {1} 이동 명령이 들어옴.", Name, actionName, _ptpPosition);
                return true;
            }
            return false;
        }
        
        private bool IsPtpInterlock(float _ptpPosition, float _ptpSpeed, float _ptpAccel)
        {
            string str = string.Empty;
            if (this.SoftMinusLimit > _ptpPosition || _ptpPosition > this.SoftPlusLimit)
            {
                str = GG.boChinaLanguage ? string.Format("{0} 位置值异常, 输入不可移动:{2} ~ ({1} ~ {3})", this.Name, this.SoftMinusLimit, _ptpPosition, this.SoftPlusLimit) : string.Format("{0} 위치값 이상 이동 불가 입력:{2} ~ ({1} ~ {3})", this.Name, this.SoftMinusLimit, _ptpPosition, this.SoftPlusLimit);
                InterLockMgr.AddInterLock(str);
                Logger.Log.AppendLine(LogLevel.Warning, str);
                return true;
            }

            if (0 >= _ptpSpeed || _ptpSpeed > this.SoftSpeedLimit)
            {
                str = GG.boChinaLanguage ? string.Format("{0} 速度值异常,输入不可移动 :{2} ~ ({1} ~ {3})", this.Name, 0, _ptpSpeed, this.SoftSpeedLimit) : string.Format("{0} 속도값 이상 이동 불가 입력:{2} ~ ({1} ~ {3})", this.Name, 0, _ptpSpeed, this.SoftSpeedLimit);
                InterLockMgr.AddInterLock(str);
                Logger.Log.AppendLine(LogLevel.Warning, str);
                return true;
            }

            if (this.SoftAccelMinusLimit > _ptpAccel || _ptpAccel > this.SoftAccelPlusLimit)
            {
                str = GG.boChinaLanguage ? string.Format("{0} 加速时间值异常, 输入不可移动:{2} ~ ({1} ~ {3})", this.Name, this.SoftAccelMinusLimit, _ptpAccel, this.SoftAccelPlusLimit) : string.Format("{0} 가속시간값 이상 이동 불가 입력:{2} ~ ({1} ~ {3})", this.Name, this.SoftAccelMinusLimit, _ptpAccel, this.SoftAccelPlusLimit);
                InterLockMgr.AddInterLock(str);
                Logger.Log.AppendLine(LogLevel.Warning, str);
                return true;
            }

            return false;
        }
        private float _ptpPosition = 0;
        private float _ptpSpeed = 0;
        private float _ptpAccel = 0;
        public virtual bool MovePosition(Equipment equip, double pos, double spd, double acc, int posiNo = -1)
        {
            if (IsInterlockMovePosition(equip) == false)
                return false;

            if (IsErrorCmdValue(pos, spd) == true)
                return false;

            if (IsStartedStep(equip, (MoveCommand)0, posiNo == -1 ? "지정위치이동" : this.Setting.LstServoPosiInfo[posiNo].Name) == true) return false;

            if (CheckStartMoveInterLock(equip, (MoveCommand)0))
            {
                //IsInterLockError = true;
                return false;
            }
            else if (IsMoveOnPosition(pos) == true && equip.EquipRunMode == EmEquipRunMode.Auto)
            {
                return true;
            }
            else
            {
                _ptpPosition = (float)pos;
                _ptpSpeed = (float)spd;
                _ptpAccel = (float)acc;

                if (IsPtpInterlock(_ptpPosition, _ptpSpeed, _ptpAccel) == true)
                    return false;

                if (SetEquipState(equip) == false) return false;
                int minMoveTime = 5000; // ms
                _ptpTimeout = minMoveTime + CalculateMoveTime(0, _ptpSpeed, _ptpPosition);
                _ptpStep = 10;
                return true;
            }
        }
        /// <summary>
        /// PTP 상대이동
        /// </summary>
        /// <param name="equip"></param>
        /// <param name="posiNo">속도, 가속도를 참고할 위치</param>
        /// <param name="relPos"></param>
        /// <returns></returns>
        public virtual bool MoveRelPosition(Equipment equip, int posiNo, float relPos)
        {
            return MovePosition(equip,
                   XF_CurrMotorPosition.vFloat + relPos,
                   this.Setting.LstServoPosiInfo[posiNo].Speed,
                   this.Setting.LstServoPosiInfo[posiNo].Accel,
                   posiNo
                   );
        }
        /// <summary>
        /// 정의된 위치에서 OFFSET 더해서 이동
        /// </summary>
        /// <param name="equip"></param>
        /// <param name="posiNo"></param>
        /// <param name="OffsetPos"></param>
        /// <returns></returns>
        public virtual bool MoveDefinedPositionOffset(Equipment equip, int posiNo, float OffsetPos)
        {
            return MovePosition(equip,
                   this.Setting.LstServoPosiInfo[posiNo].Position + OffsetPos,
                   this.Setting.LstServoPosiInfo[posiNo].Speed,
                   this.Setting.LstServoPosiInfo[posiNo].Accel,
                   posiNo
                   );
        }
        public virtual bool MovePosition(Equipment equip, int posiNo)
        {
            return MovePosition(equip, 
                this.Setting.LstServoPosiInfo[posiNo].Position,
                this.Setting.LstServoPosiInfo[posiNo].Speed,
                this.Setting.LstServoPosiInfo[posiNo].Accel,
                posiNo
                );
        }
        public virtual bool MovePosition(Equipment equip, int posiNo, float pos)
        {
            return MovePosition(equip,
                pos,
                this.Setting.LstServoPosiInfo[posiNo].Speed,
                this.Setting.LstServoPosiInfo[posiNo].Accel,
                posiNo
                );
        }
        public virtual int CalculateMoveTime(int posiNo, double cmdSpeed, double cmdPosition)
        {
            double currentPosition = XF_CurrMotorPosition.vFloat;
            if ((int)cmdSpeed < 1)
                cmdSpeed = 1;

            int moveTime = 0;
            if (Name == "Theta")
                moveTime = (int)(Math.Abs(cmdPosition - currentPosition) * 20 / cmdSpeed * 1000) + 10000;
            else
                moveTime = (int)(Math.Abs(cmdPosition - currentPosition) / cmdSpeed * 1000);


            return moveTime;
        }

        #endregion
        #region etc
        private void AllCmdOff()
        {
            this.YB_HomeCmd.vBit = false;
            this.YB_TargetPosMoveCmd.vBit = false;
            this.YB_JogMinusMove.vBit = false;
            this.YB_JogPlusMove.vBit = false;
        }
        protected virtual void OnSettingEx(Equipment equip)
        {

        }

        public bool StartReset(Equipment equip)
        {
            return true;
        }

        private bool IsInterlockMovePosition(Equipment equip)
        {
            if (equip.IsPause)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<PAUSE>\n(设备状态在 PAUSE时， 轴={0} 禁止移动.)", Name) : string.Format("인터락<PAUSE>\n(설비 상태가 PAUSE일 경우 축={0} 이동이 금지됩니다.)", Name));
                Logger.Log.AppendLine(LogLevel.Warning, "설비 상태가 Pause 상태에서 축={0} 이동 금지됨", Name);
                return false;
            }

            if (equip.IsDoorOpen && equip.IsUseInterLockOff == false && equip.IsUseDoorInterLockOff == false /*&& equip.IsEnableGripSwOn == EmGrapSw.MIDDLE_ON*/)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<DOOR>\n(设备状态在 DOOR OPEN状态下，轴 ={0} 禁止移动. )", Name) : string.Format("인터락<DOOR>\n(설비 상태가 DOOR OPEN 상태에서 축={0} 이동이 금지됩니다.)", Name));
                Logger.Log.AppendLine(LogLevel.Warning, "설비 상태가 Door Open 상태에서 축={0} 이동 금지됨", Name);
                return false;
            }

            if (IsHomeCompleteBit == false)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<HOME BIT ERROR>\n(轴={0} Motor HomeCompleteBit OFF 状态. 需要Motor Home 动作.)", Name)
 : string.Format("인터락<HOME BIT ERROR>\n(축={0} 모터 HomeCompleteBit OFF 상태입니다. 모터 홈 동작이 필요합니다.)", Name));
                Logger.Log.AppendLine(LogLevel.Warning, "HOME BIT ERROR 상태, 포지션 이동 불가. 축={0} 이동 금지됨", Name);
                return false;
            }

            if (equip.IsImmediatStop)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<EMERGENCY>\n(设备状态 EMERGENCY 状态下 轴={0} 禁止移动.)", Name) : string.Format("인터락<EMERGENCY>\n(설비 상태가 EMERGENCY 상태에서 축={0} 이동이 금지됩니다.)", Name));
                Logger.Log.AppendLine(LogLevel.Warning, "설비 상태가 Emergency 상태에서 축={0} 이동 금지됨", Name);
                return false;
            }

            if (IsHomeCompleteBit == false)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<HOME BIT ERROR>\n(轴={0} Motor HomeCompleteBit OFF 状态. 需要Motor Home 动作.)", Name) : string.Format("인터락<HOME BIT ERROR>\n(축={0} 모터 HomeCompleteBit OFF 상태입니다. 모터 홈 동작이 필요합니다.)", Name));
                Logger.Log.AppendLine(LogLevel.Warning, "HOME BIT ERROR 상태, 포지션 이동 불가. 축={0} 이동 금지됨", Name);
                return false;
            }

            if (IsHWNegativeLimit || IsHWPositiveLimit)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<LIMIT SENSOR>\n(轴={0} Motor 位置被 Limit Sensor感应到了.\n Limit Sensor 感应状态时，只能移动 Jog or Home .)", Name) : string.Format("인터락<LIMIT SENSOR>\n(축={0} 모터 위치가 리밋센서에 감지되었습니다.\n리밋 센서 감지 상태일 경우 조그 및 홈 이동만 가능합니다.)", Name));
                Logger.Log.AppendLine(LogLevel.Warning, "리밋 감지상태에서 포지션 이동 불가. 축={0} 이동 금지됨", Name);
                return false;
            }
            return true;
        }
        private void CheckServoStatus(Equipment equip)
        {
            if (_isHappenAlarm == false)
            {
                if (equip.StHoming.StepNum != 0) return;
                if (this.IsMovingOnHome() == true) return;

                if (this.XB_StatusHWNegativeLimitSet)
                {
                    AlarmMgr.Instance.Happen(equip, HW_MINUS_LIMIT_ERROR);
                    _isHappenAlarm = true;
                    //jys::Seq간소화 equip.IsNeedSeq = EMNeedSeq.Home;
                }
                if (this.XB_StatusHWPositiveLimitSet)
                {
                    AlarmMgr.Instance.Happen(equip, HW_PLUS_LIMIT_ERROR);
                    _isHappenAlarm = true;
                    //jys::Seq간소화 equip.IsNeedSeq = EMNeedSeq.Home;
                }
                if (this.XB_StatusSWNegativeLimitSet)
                {
                    AlarmMgr.Instance.Happen(equip, SW_MINUS_LIMIT_ERROR);
                    _isHappenAlarm = true;
                    //jys::Seq간소화 equip.IsNeedSeq = EMNeedSeq.Home;
                }
                if (this.XB_StatusSWPositiveLimitSet)
                {
                    AlarmMgr.Instance.Happen(equip, SW_PLUS_LIMIT_ERROR);
                    _isHappenAlarm = true;
                    //jys::Seq간소화 equip.IsNeedSeq = EMNeedSeq.Home;
                }
                if (this.XB_StatusMotorServoOn == false)
                {
                    AlarmMgr.Instance.Happen(equip, MOTOR_SERVO_ON_ERROR);
                    _isHappenAlarm = true;
                    //jys::Seq간소화 equip.IsNeedSeq = EMNeedSeq.Home;
                }
                if (this.XB_ErrFatalFollowingError)
                {
                    AlarmMgr.Instance.Happen(equip, FATAL_FOLLOWING_ERROR);
                    _isHappenAlarm = true;
                    //jys::Seq간소화 equip.IsNeedSeq = EMNeedSeq.Home;
                }
                if (this.XB_ErrAmpFaultError)
                {
                    AlarmMgr.Instance.Happen(equip, AMP_FAULT_ERROR);
                    _isHappenAlarm = true;
                    //jys::Seq간소화 equip.IsNeedSeq = EMNeedSeq.Home;
                }
                if (this.XB_ErrI2TAmpFaultError)
                {
                    AlarmMgr.Instance.Happen(equip, I2T_AMP_FAULT_ERROR);
                    _isHappenAlarm = true;
                    //                     equip.IsNeedSeq = EMNeedSeq.Home;
                }
            }
        }

        public virtual bool CheckStartMoveInterLock(Equipment equip, MoveCommand cmd)
        {
            #region 인터락 해제 가능
            //if (GG.TestMode == false
            //    && equip.IsUseInterLockOff == false
            //    && equip.IsUseDoorInterLockOff == false
            //    && equip.IsDoorOpen
            //    && equip.IsEnableGripSwOn == EmGrapSw.EMERGENCY_ON)
            //{
            //    InterLockMgr.AddInterLock(string.Format("인터락<GRIP SW>\n(GRIP SW 이상상태에서 축={0} 이동 금지됨)", Name));
            //    Logger.Log.AppendLine(LogLevel.Warning, "GRIP SW 이상상태에서 축={0} 이동 금지됨", Name);
            //    equip.IsInterlock = true;
            //    return true;
            //}

            if (GG.TestMode == false
                && equip.IsUseInterLockOff == false
                && equip.IsUseDoorInterLockOff == false
                && equip.IsDoorOpen
                && cmd != MoveCommand.JOG)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<Door Open>\n(Door Open状态下 轴={0} 禁止移动)", Name) : string.Format("인터락<Door Open>\n(Door Open상태에서 축={0} 이동 금지됨)", Name));
                Logger.Log.AppendLine(LogLevel.Warning, "Door Open상태에서 축={0} 이동 금지됨", Name);
                equip.IsInterlock = true;
                return true;
            }

            if (equip.IsDoorOpen
                && equip.IsUseInterLockOff == false
                && equip.IsUseDoorInterLockOff == false
                && (equip.IsEnableGripMiddleOn && equip.EquipRunMode == EmEquipRunMode.Manual) == false)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<DOOR>\n(设备状态 DOOR OPEN 状态下 轴={0} 禁止移动.)", Name) : string.Format("인터락<DOOR>\n(설비 상태가 DOOR OPEN 상태에서 축={0} 이동이 금지됩니다.)", Name));
                Logger.Log.AppendLine(LogLevel.Warning, "설비 상태가 Door Open 상태에서 축={0} 이동 금지됨", Name);
                equip.IsInterlock = true;

                return true;
            }
            if (equip.IsWaferDetect != EmGlassDetect.NOT && equip.IsNoGlassMode == false
                && equip.Vacuum.IsVacuumSolOn == true && equip.Vacuum.IsVacuumOn == false)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<VACUUM>\n(Wafer Detect Sensor感应时 Vacuum Off 状态的话，轴={0} 禁止移动.)", Name) : string.Format("인터락<VACUUM>\n(Wafer Detect Sensor가 감지 될 때 Vacuum Off 상태라면 축={0} 이동이 금지됩니다.)", Name));
                Logger.Log.AppendLine(LogLevel.Warning, "Wafer Detect Sensor가 감지 될 때 Vacuum Off 상태라면 축={0} 이동 금지됨", Name);
                equip.IsInterlock = true;

                return true;
            }
            if (equip.StandCentering.IsForward == true
                && equip.IsUseInterLockOff == false)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<Centering>\n(Centering在 前进(前) 状态时，轴={0} 禁止移动.)", Name) : string.Format("인터락<Centering>\nCentering이 전진상태 일 때 축={0} 이동이 금지됩니다.", Name));
                Logger.Log.AppendLine(LogLevel.Warning, "Centering이 전진상태 일 때 축={0} 이동이 금지됩니다. 축={0} 이동 금지됨", Name);
                equip.IsInterlock = true;

                return true;
            }

            #endregion

            if (equip.IsEFEMInputArm.IsOn == true || equip.RobotArmDefect.IsOn == true)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<ROBOT ARM DETECT>\n(ROBOT ARM 感应状态 or 投入信号 ON状态下，禁止移动 .)", Name) : string.Format("인터락<ROBOT ARM DETECT>\n(ROBOT ARM 감지 상태 or 투입신호 ON 상태에서 이동이 금지됩니다.)", Name));                
                equip.IsInterlock = true;
                return true;
            }

            if (equip.LiftPin.IsForward)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<LIFT PIN>\n(LIFT PIN 上升状态下， 轴={0} 禁止移动)", Name) : string.Format("인터락<LIFT PIN>\n(LIFT PIN UP 상태에서 축={0} 이동이 금지됩니다.)", Name));
                Logger.Log.AppendLine(LogLevel.Warning, "LIFT PIN UP 상태에서 축={0} 이동 금지됨", Name);
                equip.IsInterlock = true;

                return true;
            }

            return false;
        }
        protected virtual bool SetEquipState(Equipment equip)
        {
            return true;
        }
        public virtual bool CheckMovingInterLock(Equipment equip, MoveCommand cmd, ref int stepMove)
        {
            if(equip.IsUseInterLockOff == false)
            {                
                if (equip.IsWaferDetect != EmGlassDetect.NOT && equip.IsNoGlassMode == false
                    && equip.Vacuum.IsVacuumSolOn && equip.Vacuum.IsVacuumOff)
                {
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<VACUUM>\n({0}轴移动中 ，禁止 Vacuum Off.)", Name) : string.Format("인터락<VACUUM>\n({0}축 이동 중에 Vacuum Off가 금지 됩니다.)", Name));
                    Logger.Log.AppendLine(LogLevel.Warning, "{0}축 이동 중에 Vacuum Off가 금지됨", Name);
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0111_EMS_STOP_VACUUM_OFF_ERROR);
                    equip.IsInterlock = true;
                    return true;
                }
            }
           
            return false;
        }
        public virtual bool IsPhysicalSensorOn(int p)
        {
            return true;
        }
        #endregion               

        public static void ReadPmacFloatMemory()
        {
            GG.PMAC.ReadFromPLC(GG.PMAC_X_READ_START, GG.PMAC_X_READ_START.Length);
        }
        public static void WritePmacFloatMemory()
        {
            GG.PMAC.WriteToPLC(GG.PMAC_Y_WRITE_START, GG.PMAC_Y_WRITE_START.Length);
        }

    }
}
