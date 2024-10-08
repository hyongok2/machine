using Dit.Framework.Comm;
using Dit.Framework.PLC;
using EquipMainUi.Setting;
using System;

namespace EquipMainUi.Struct.Detail.EziStep
{
    public enum EM_STEP_MOTOR_MOVE_TYPE
    {
        PTP,
        JOG,
        HOME
    }
    public enum EM_STEP_MOTOR_JOG
    {
        JOG_PLUS = 1,
        JOG_STOP = 0,
        JOG_MINUS = -1,
    }
    public enum EM_STEP_MOTOR_TYPE
    {
        X_Y,
        THETA,
    }

    public class StepMotorEzi
    {
        public Func<float, float, EM_STEP_MOTOR_MOVE_TYPE, bool> CheckStartMoveInterLockFunc;
        public Func<EM_STEP_MOTOR_JOG, float, bool> CheckStartJogInterLockFunc;

        public double SoftMinusLimit = -99999;
        public double SoftPlusLimit = 99999;
        public int SoftSpeedLimit = 1000;
        public int SoftJogSpeedLimit = 1000;
        public int SoftAccelPlusLimit = 1000;
        public int SoftAccelMinusLimit = 10;
        public float InposOffset = 0.1f;

        public int ComportNumber { get; set; }
        public int InnerSlaveNo { get; set; }
        public string Name { get; set; }


        public PlcAddr XB_StatusHomeCompleteBit { get; set; }
        public PlcAddr XB_StatusMotorMoving { get; set; }
        public PlcAddr XB_StatusMinusLimitSet { get; set; }
        public PlcAddr XB_StatusPlusLimitSet { get; set; }
        public PlcAddr XB_StatusMotorServoOn { get; set; }
        public PlcAddr XB_StatusHomeInPosition { get; set; }
        public PlcAddr XB_StatusMotorInPosition { get; set; }

        public PlcAddr XF_CurrMotorPosition { get; set; }
        public PlcAddr XF_CurrMotorSpeed { get; set; }
        public PlcAddr XF_CurrMotorAccel { get; set; }

        //서보 Motor Stop
        public PlcAddr YB_MotorStopCmd { get; set; }
        public PlcAddr XB_MotorStopCmdAck { get; set; }
        //Home Cmd
        public PlcAddr YB_HomeCmd { get; set; }
        public PlcAddr XB_HomeCmdAck { get; set; }

        //Jog Command
        public PlcAddr YB_MotorJogMinusMove { get; set; }
        public PlcAddr YB_MotorJogPlusMove { get; set; }
        public PlcAddr YF_MotorJogSpeedCmd { get; set; }
        public PlcAddr XF_MotorJogSpeedCmdAck { get; set; }

        //Point To Point Move Cmd        
        public PlcAddr YB_PTPMoveCmd { get; set; }
        public PlcAddr XB_PTPMoveCmdAck { get; set; }
        public PlcAddr YF_PTPMovePosition { get; set; }
        public PlcAddr XF_PTPMovePositionAck { get; set; }
        public PlcAddr YF_PTPMoveSpeed { get; set; }
        public PlcAddr XF_PTPMoveSpeedAck { get; set; }
        public PlcAddr YF_PTPMoveAccel { get; set; }
        public PlcAddr XF_PTPMoveAccelAck { get; set; }

        public int MOVE_HOME_OVERTIME = 20000;
        public int MOVE_PTP_OVERTIME = 8000;
        public int HommingCmdStep = 0;
        public int PtpMoveCmdStep = 0;
        public int MoveStopCmdStep = 0;
        public int JogCmdStep = 0;

        public PMacServoSetting Setting { get; set; }
        public static int HomePos = 0;
        public static int LoadingPos = 1;
        public static int UnloadingPos = 2;
        public int PositionCount { get; set; }

        public EM_STEP_MOTOR_TYPE motorType;
        public StepMotorEzi(int innnerSlaveNo, string name, int posiCount)
        {
            InnerSlaveNo = innnerSlaveNo;
            Name = name;
            PositionCount = posiCount;

            Setting = new PMacServoSetting();
        }

        public virtual bool IsHomeCompleteBit
        {
            get { return XB_StatusHomeCompleteBit.vBit; }
        }
        public bool IsHomeComplete
        {
            get { return IsHomeCompleteBit && IsServoOnBit && !IsMoving && !IsHomming; }
        }
        public virtual bool IsMinusLimit
        {
            get { return XB_StatusMinusLimitSet.vBit; }
        }
        public virtual bool IsPlusLimit
        {
            get { return XB_StatusPlusLimitSet.vBit; }
        }
        public virtual bool IsServoOnBit
        {
            get { return XB_StatusMotorServoOn.vBit; }
        }
        public virtual bool IsServoError
        {
            get
            {
                return IsHomeCompleteBit == false || IsMinusLimit || IsPlusLimit || IsServoOnBit == false;
            }
        }
        public bool IsHomming { get { return HommingCmdStep != 0; } }

        public virtual bool IsMoving
        {
            get { return XB_StatusMotorMoving.vBit; }
        }
        public bool IsMoveOnPosition(int posiNo)
        {
            if (GG.TestMode)
                return Math.Abs(XF_CurrMotorPosition.vFloat - XF_PTPMovePositionAck.vFloat) <= InposOffset && IsMoving == false && PtpMoveCmdStep == 0;
            else
                return Math.Abs(XF_CurrMotorPosition.vFloat - this.Setting.LstServoPosiInfo[posiNo].Position) <= InposOffset
                    && IsMoving == false
                    && PtpMoveCmdStep == 0
                    && IsHomeCompleteBit;
        }
        public virtual bool IsMovingStep
        {
            get
            {
                bool isMovingStep = false;

                if (PtpMoveCmdStep != 0)
                {
                    isMovingStep = true;
                }

                return IsHomming || isMovingStep;
            }
        }
        public void LogicWorking()
        {
            CheckStepMotorStatus();

            HommingWorking();
            PtpMoveWorking();
            MoveStopWorking();
            JogMoveWorking();
        }

        private void CheckStepMotorStatus()
        {
            if (IsMinusLimit && IsHomming == false)
            {
                //마이너스 리밋
            }
            if (IsPlusLimit && IsHomming == false)
            {
                //플러스 리밋
            }
            if (this.IsServoOnBit == false && IsHomming == false)
            {
                AlarmMgr.Instance.Happen(GG.Equip, SERVO_ON_ERROR);
            }
        }
        private DateTime _hommingStartTime = PcDateTime.Now;
        private PlcTimer _homeDelay = new PlcTimer();
        public EM_AL_LST MOVE_OVERTIME_ERROR { get; set; }
        public EM_AL_LST SERVO_ON_ERROR { get; set; }
        private void HommingWorking()
        {
            if (IsMoving
                && HommingCmdStep != 0
                && CheckMovingInterLock(MoveCommand.HOME_POSI, ref HommingCmdStep) == true)
            {
                MoveStop();
                HommingCmdStep = 0;
                return;
            }
            if (HommingCmdStep > 30 && (PcDateTime.Now - _hommingStartTime).TotalMilliseconds > MOVE_HOME_OVERTIME)
            {
                //Logger.WriteMotor(this, "MOVE OVERTIME", actionName, (float)Setting.GetPosition((int)cmd), (float)Setting.GetSpeed((int)cmd), stepMove, string.Format("[NOVE TIME OUT : {0}]", moveTimeout));
                AlarmMgr.Instance.Happen(GG.Equip, MOVE_OVERTIME_ERROR);
                //equip.IsNeedSeq = EMNeedSeq.Home;
                _hommingStartTime = PcDateTime.Now;
                HommingCmdStep = 0;
                return;
            }

            if (HommingCmdStep == 0)
            {
            }
            else if (HommingCmdStep == 10)
            {
                _hommingStartTime = PcDateTime.Now;
                HommingCmdStep = 20;
            }
            else if (HommingCmdStep == 20)
            {
                YB_HomeCmd.vBit = true;
                HommingCmdStep = 30;
            }
            else if (HommingCmdStep == 30)
            {
                if (XB_HomeCmdAck.vBit == true)
                {
                    YB_HomeCmd.vBit = false;
                    HommingCmdStep = 40;
                }
            }
            else if (HommingCmdStep == 40)
            {
                _homeDelay.Start(3);
                HommingCmdStep = 50;
            }
            else if (HommingCmdStep == 50)
            {
                if (_homeDelay && this.IsMoving == false)
                {
                    if (this.IsServoOnBit && XB_StatusHomeCompleteBit.vBit == true)
                    {
                        _homeDelay.Stop();
                        _homeDelay.Start(0, 500);
                        HommingCmdStep = 60;
                    }
                }
            }
            else if (HommingCmdStep == 60)
            {
                if (_homeDelay)
                {
                    _homeDelay.Stop();
                    HommingCmdStep = 80;
                }
            }
            else if (HommingCmdStep == 80)
            {
                HommingCmdStep = 0;
            }
        }
        private DateTime _ptpStartTime = PcDateTime.Now;
        private PlcTimer _ptpDelay = new PlcTimer();
        private void PtpMoveWorking()
        {
            if (IsMoving
                && PtpMoveCmdStep != 0
                && CheckMovingInterLock(MoveCommand.USER_PTP, ref PtpMoveCmdStep) == true)
            {
                MoveStop();
                PtpMoveCmdStep = 0;
                return;
            }
            if (PtpMoveCmdStep > 30 && (PcDateTime.Now - _ptpStartTime).TotalMilliseconds > MOVE_PTP_OVERTIME)
            {
                //Logger.WriteMotor(this, "MOVE OVERTIME", actionName, (float)Setting.GetPosition((int)cmd), (float)Setting.GetSpeed((int)cmd), stepMove, string.Format("[NOVE TIME OUT : {0}]", moveTimeout));
                AlarmMgr.Instance.Happen(GG.Equip, MOVE_OVERTIME_ERROR);
                //equip.IsNeedSeq = EMNeedSeq.Home;
                _ptpStartTime = PcDateTime.Now;
                PtpMoveCmdStep = 0;
                return;
            }

            if (PtpMoveCmdStep == 0)
            {
            }
            else if (PtpMoveCmdStep == 10)
            {
                YF_PTPMovePosition.vFloat = _position;
                YF_PTPMoveSpeed.vFloat = _speed;
                YF_PTPMoveAccel.vFloat = _accel;

                _ptpStartTime = PcDateTime.Now;

                PtpMoveCmdStep = 20;
            }
            else if (PtpMoveCmdStep == 20)
            {
                Logger.Log.AppendLine(LogLevel.NoLog, string.Format("[EziStepMotor PTP Start]scale 전 Position {0}, Speed {1}, Acc {2}", _position, _speed, _accel));
                YB_PTPMoveCmd.vBit = true;
                PtpMoveCmdStep = 30;
            }
            else if (PtpMoveCmdStep == 30)
            {
                if (XB_PTPMoveCmdAck.vBit == true)
                {
                    if (Math.Abs(_position - YF_PTPMovePosition.vFloat) < InposOffset && Math.Abs(_position - XF_PTPMovePositionAck.vFloat) < InposOffset &&
                        Math.Abs(_speed - YF_PTPMoveSpeed.vFloat) < InposOffset && Math.Abs(_speed - XF_PTPMoveSpeedAck.vFloat) < InposOffset &&
                        Math.Abs(_accel - YF_PTPMoveAccel.vFloat) < InposOffset && Math.Abs(_accel - XF_PTPMoveAccelAck.vFloat) < InposOffset)
                    {
                        YB_PTPMoveCmd.vBit = false;
                        PtpMoveCmdStep = 40;
                    }
                }
            }
            else if (PtpMoveCmdStep == 40)
            {
                if (XB_PTPMoveCmdAck.vBit == false)
                {
                    PtpMoveCmdStep = 50;
                }
            }
            else if (PtpMoveCmdStep == 50)
            {
                if (Math.Abs(_position - XF_CurrMotorPosition.vFloat/*YF_PTPMovePosition.vFloat*/) < InposOffset && IsMoving == false)
                {
                    _ptpDelay.Start(0, 500);
                    PtpMoveCmdStep = 60;
                }
            }
            else if (PtpMoveCmdStep == 60)
            {
                if (_ptpDelay)
                {
                    _ptpDelay.Stop();
                    PtpMoveCmdStep = 0;
                }
            }
        }
        private void MoveStopWorking()
        {
            if (MoveStopCmdStep == 0)
            {
            }
            else if (MoveStopCmdStep == 10)
            {
                MoveStopCmdStep = 20;
            }
            else if (MoveStopCmdStep == 20)
            {
                YB_MotorStopCmd.vBit = true;
                MoveStopCmdStep = 30;
            }
            else if (MoveStopCmdStep == 30)
            {
                if (XB_MotorStopCmdAck.vBit == true)
                {
                    YB_MotorStopCmd.vBit = false;
                    MoveStopCmdStep = 40;
                }
            }
            else if (MoveStopCmdStep == 40)
            {
                if (XB_MotorStopCmdAck.vBit == false)
                {
                    MoveStopCmdStep = 50;
                }
            }
            else if (MoveStopCmdStep == 50)
            {
                HommingCmdStep = 0;
                PtpMoveCmdStep = 0;
                MoveStopCmdStep = 0;
            }
        }

        private void JogMoveWorking()
        {
            if (IsMoving
                && JogCmdStep != 0
                && CheckMovingInterLock(MoveCommand.JOG, ref JogCmdStep) == true)
            {
                MoveStop();
                JogCmdStep = 0;
                return;
            }
            if (JogCmdStep == 0)
            {

            }
            else if (JogCmdStep == 10)
            {
                if (this.YF_MotorJogSpeedCmd.vFloat == this.XF_MotorJogSpeedCmdAck.vFloat && this.XF_MotorJogSpeedCmdAck.vFloat == JogSpeed)
                {
                    JogCmdStep = 50;
                }
                else
                {
                    if (SERVO_JOG == EM_STEP_MOTOR_JOG.JOG_MINUS)
                    {
                        this.YF_MotorJogSpeedCmd.vFloat = JogSpeed;
                    }
                    else if (SERVO_JOG == EM_STEP_MOTOR_JOG.JOG_PLUS)
                    {
                        this.YF_MotorJogSpeedCmd.vFloat = JogSpeed;
                    }

                    JogCmdStep = 20;
                }
            }
            else if (JogCmdStep == 20)
            {
                if (this.YF_MotorJogSpeedCmd.vFloat == this.XF_MotorJogSpeedCmdAck.vFloat && this.XF_MotorJogSpeedCmdAck.vFloat == JogSpeed)
                {
                    JogCmdStep = 50;
                }
            }
            else if (JogCmdStep == 50)
            {
                if (SERVO_JOG == EM_STEP_MOTOR_JOG.JOG_MINUS)
                {
                    this.YB_MotorJogPlusMove.vBit = false;
                    this.YB_MotorJogMinusMove.vBit = true;
                }
                else if (SERVO_JOG == EM_STEP_MOTOR_JOG.JOG_PLUS)
                {
                    this.YB_MotorJogMinusMove.vBit = false;
                    this.YB_MotorJogPlusMove.vBit = true;
                }
                JogCmdStep = 60;
            }
            else if (JogCmdStep == 60)
            {

            }
        }

        private bool IsStartedHomeStep { get { return HommingCmdStep > 0; } }
        public bool MoveHome()
        {
            if (IsStartedHomeStep)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<Step Motor Home>" : "인터락 <Step Motor 홈>", GG.boChinaLanguage ? "{0} 在原点进行中时, 不能进行原点命令" : "{0} 가 홈 진행 중일 때 홈 명령을 할 수 없습니다", this.Name);
                return false;
            }
            if (IsStartedPtpStep == true)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<Step Motor Home>" : "인터락 <Step Motor 홈>", GG.boChinaLanguage ? "{0} 在PTP进行中时，不能进行原点命令" : "{0} 가 PTP 진행 중일 때 홈 명령을 할 수 없습니다", this.Name);
                return false;
            }
            if (IsMoving)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<Step Motor Home>" : "인터락 <Step Motor 홈>", GG.boChinaLanguage ? "{0} 在移动中时，不能进行原点" : "{0} 가 이동 중일 때에는 홈 진행을 할 수 없습니다", this.Name);
                return false;
            }

            if(CheckStartMoveInterLock(MoveCommand.HOME_POSI) == true)
                return false;

            if(IsHomeCompleteBit && !IsServoError)
            {
                MovePosition(0, 4);
            }
            else
            {
                HommingCmdStep = 10;
            }

            return true;
        }

        private float _position;
        private float _speed;
        private float _accel;
        private bool IsStartedPtpStep { get { return PtpMoveCmdStep > 0; } }
        public bool MovePosition(float position, float speed, float accel = 100)
        {
            if (IsStartedHomeStep)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<Step Motor PTP>" : "인터락 <Step Motor PTP>", GG.boChinaLanguage ? "{0} 在原点进行中时，不能进行PTP " : "{0} 가 홈 진행 중일 때 PTP 진행을 할 수 없습니다", this.Name);
                return false;
            }
            if (IsStartedPtpStep == true)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<Step Motor PTP>" : "인터락 <Step Motor PTP>", GG.boChinaLanguage ? "{0} 在PTP进行中时，不能进行PTP " : "{0} 가 PTP 진행 중일 때 PTP 진행을 할 수 없습니다", this.Name);
                return false;
            }
            if (IsMoving)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<Step Motor PTP>" : "인터락 <Step Motor PTP>", GG.boChinaLanguage ? "{0} 移动中时，不能进行PTP" : "{0} 가 이동 중일 때에는 PTP 진행을 할 수 없습니다", this.Name);
                return false;
            }
            if(IsServoError)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<Step Motor PTP>" : "인터락 <Step Motor PTP>", GG.boChinaLanguage ? "{0}是 Servo Error 状态时，不能进行 PTP" : "{0}가 Servo Error 상태일 때에는 PTP 진행을 할 수 없습니다", this.Name);
                return false;
            }

            if (CheckStartMoveInterLock(MoveCommand.USER_PTP) == true)
                return false;
            
            _position = position;
            _speed = speed;
            _accel = accel;


            PtpMoveCmdStep = 10;

            return true;
        }
        public bool MovePosition(int posiNo)
        {
            return MovePosition(this.Setting.LstServoPosiInfo[posiNo].Position,
                this.Setting.LstServoPosiInfo[posiNo].Speed
                );
        }


        private EM_STEP_MOTOR_JOG SERVO_JOG = EM_STEP_MOTOR_JOG.JOG_STOP;
        private float JogSpeed = 0;

        public bool JogMove(EM_STEP_MOTOR_JOG jog, float speed)
        {
            if (jog == EM_STEP_MOTOR_JOG.JOG_STOP)
            {
                this.YF_MotorJogSpeedCmd.vFloat = speed;
                this.YB_MotorJogMinusMove.vBit = false;
                this.YB_MotorJogPlusMove.vBit = false;

                JogCmdStep = 0;
                return true;
            }

            //if (CheckStartJogInterLockFunc(jog, (int)speed) == true)
            //    return false;

            if (jog == EM_STEP_MOTOR_JOG.JOG_MINUS || jog == EM_STEP_MOTOR_JOG.JOG_PLUS)
            {
                if (CheckStartMoveInterLock(MoveCommand.JOG) == true)
                    return false;

                JogCmdStep = 10;
                SERVO_JOG = jog;
                JogSpeed = speed;
            }
            return true;
        }
        public void MoveStop()
        {
            MoveStopCmdStep = 10;
        }

        public void SetOrginPosition()
        {

        }

        public virtual bool CheckStartMoveInterLock(MoveCommand cmd)
        {
            #region 인터락 해제 가능

            if (GG.TestMode == false
                && GG.Equip.IsUseInterLockOff == false
                && GG.Equip.IsUseDoorInterLockOff == false
                && GG.Equip.IsDoorOpen
                && cmd != MoveCommand.JOG)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<Door Open>\n(Door Open状态下，轴={0} 禁止移动.)", Name) : string.Format("인터락<Door Open>\n(Door Open상태에서 축={0} 이동 금지됨)", Name));
                Logger.Log.AppendLine(LogLevel.Warning, "Door Open상태에서 축={0} 이동 금지됨", Name);
                GG.Equip.IsInterlock = true;
                return true;
            }

            if (GG.Equip.IsDoorOpen
                && GG.Equip.IsUseInterLockOff == false
                && GG.Equip.IsUseDoorInterLockOff == false
                && (GG.Equip.IsEnableGripMiddleOn && GG.Equip.EquipRunMode == EmEquipRunMode.Manual) == false)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<DOOR>\n(设备状态 DOOR OPEN 状态下， 轴={0} 禁止移动.)", Name) : string.Format("인터락<DOOR>\n(설비 상태가 DOOR OPEN 상태에서 축={0} 이동이 금지됩니다.)", Name));
                Logger.Log.AppendLine(LogLevel.Warning, "설비 상태가 Door Open 상태에서 축={0} 이동 금지됨", Name);
                GG.Equip.IsInterlock = true;

                return true;
            }
            if (GG.Equip.Efem.Aligner.Status.IsWaferExist == true && GG.Equip.IsNoGlassMode == false
                && false //얼라이너 버큠 오프라면)
                && GG.Equip.IsUseInterLockOff == false)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Interlock<VACUUM>\n(Wafer Detect Sensor被感应时 Vacuum Off 状态的话，轴={0} 禁止移动.)", Name) : string.Format("인터락<VACUUM>\n(Wafer Detect Sensor가 감지 될 때 Vacuum Off 상태라면 축={0} 이동이 금지됩니다.)", Name));
                Logger.Log.AppendLine(LogLevel.Warning, "Wafer Detect Sensor가 감지 될 때 Vacuum Off 상태라면 축={0} 이동 금지됨", Name);
                GG.Equip.IsInterlock = true;

                return true;
            }

            #endregion

            if (GG.Equip.InitSetting.EquipNo == 2 // jys:: 210121 1호기는 센서 동작 하지 않음
                && GG.Equip.IsAlignerInputArm.IsOn)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<EFEM Aligner Input> EFEM Robot Input 信号关闭时不能动作" : "인터락<EFEM Aligner Input> EFEM Robot Input 신호가 꺼져 있을 때 동작이 불가능합니다");
                return true;
            }

            return false;
        }

        public virtual bool CheckMovingInterLock(MoveCommand cmd, ref int stepMove)
        {
            if (GG.Equip.IsUseInterLockOff == false)
            {
                if (GG.Equip.Efem.Aligner.Status.IsWaferExist == true && GG.Equip.IsNoGlassMode == false
                    &&false//aligner vacuum 체크
                    )
                {
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("Glass感应时 {0}轴 {1} 移动中 Vacuum Off会禁止.", Name, cmd.ToString()) : string.Format("인터락<VACUUM>\n(글라스가 감지 될 때 {0}축 {1} 이동 중 Vacuum Off가 금지 됩니다.)", Name, cmd.ToString()));
                    Logger.Log.AppendLine(LogLevel.Warning, "{0}축 이동 중에 Vacuum Off가 금지됨", Name);
                    AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0111_EMS_STOP_VACUUM_OFF_ERROR);
                    GG.Equip.IsInterlock = true;
                    return true;
                }
            }

            //Aligner InterLock 신호 추가 후 수정

            return false;
        }
    }
}
