using System;
using System.Linq;
using Dit.Framework.Log;
using Dit.Framework.PLC;
using EquipSimulator.Log;

namespace EquipSimulator.Acturator
{
    public class ServoSimulUmac
    {
        public VirtualMem PLC;
        public string Name;

        public PlcAddr XB_StatusHomeCompleteBit { get; set; }
        public PlcAddr XB_StatusHomeInPosition { get; set; }
        public PlcAddr XB_StatusMotorMoving { get; set; }
        public PlcAddr XB_StatusNegativeLimitSet { get; set; }
        public PlcAddr XB_StatusPositiveLimitSet { get; set; }
        public PlcAddr XB_ErrMotorServoOn { get; set; }
        public PlcAddr XB_ErrFatalFollowingError { get; set; }
        public PlcAddr XB_ErrAmpFaultError { get; set; }
        public PlcAddr XB_ErrI2TAmpFaultError { get; set; }

        public PlcAddr XF_CurrMotorPosition { get; set; }
        public PlcAddr XF_CurrMotorSpeed { get; set; }
        public PlcAddr XF_CurrMotorStress { get; set; }

        public PlcAddr YB_HomeCmd { get; set; }
        public PlcAddr XB_HomeCmdAck { get; set; }

        public PlcAddr YB_MotorJogMinusMove { get; set; }
        public PlcAddr YB_MotorJogPlusMove { get; set; }
        public PlcAddr YF_MotorJogSpeedCmd { get; set; }
        public PlcAddr XF_MotorJogSpeedAck { get; set; }

        public PlcAddr YB_PositionWriteCmd { get; set; }
        public PlcAddr XB_PositionWriteCmdAck { get; set; }

        public PlcAddr[] YB_TargetMoveCmd { get; set; }
        public PlcAddr[] XB_TargetMoveAck { get; set; }
        public PlcAddr[] YF_TargetPosition { get; set; }
        public PlcAddr[] XF_Position0PosiAck { get; set; }
        public PlcAddr[] YF_Position0Speed { get; set; }
        public PlcAddr[] XF_Position0SpeedAck { get; set; }
        public PlcAddr[] YI_Position0Accel { get; set; }
        public PlcAddr[] XI_Position0AccelAck { get; set; }
        public PlcAddr[] XB_TargetMoveComplete { get; set; }
        
        public PlcAddr[] XB_PhysicalSensor { get; set; }

        public PlcTimer[] _moveDelay = new PlcTimer[] { new PlcTimer() };
        public PlcTimer _homeDelay = new PlcTimer();
        public PlcTimer _startPosiDelay = new PlcTimer();
        public Func<bool> InterLockFunc { get; set; }

        public int[] _moveStep = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public int _homeStep = 0;
        public int _startPosiStep = 0;
        public int MinusLimit { get; set; }
        public int PlusLimit { get; set; }

        private bool _moving = false;
        protected bool _moveComplete { get; set; }
        private bool _isMinuseGo = false;
        private int _jogGo { get; set; }
        private int _stepSetting = 0;
        protected double _inTargetPosi { get; set; }
        protected double _inSpeed { get; set; }

        public bool OutHomeAck { get; set; }

        public bool _isHomeCompleteBit = false;
        public bool NotReply { get; set; }

        private int _homePosIdx = 30;
        public int PositionCount = 1;
        public ServoSimulUmac(string name, int minusLimit, int plusLimit, int speed, int positionCount)
        {
            PositionCount = 1;
            _moveStep = new int[1];
            YB_TargetMoveCmd      = new PlcAddr[1];
            XB_TargetMoveAck      = new PlcAddr[1];
            YF_TargetPosition     = new PlcAddr[1];
            XF_Position0PosiAck   = new PlcAddr[1];
            YF_Position0Speed     = new PlcAddr[1];
            XF_Position0SpeedAck  = new PlcAddr[1];
            YI_Position0Accel     = new PlcAddr[1];
            XI_Position0AccelAck  = new PlcAddr[1];
            XB_TargetMoveComplete = new PlcAddr[1];
            XB_PhysicalSensor     = new PlcAddr[1];

            Name = name;
            MinusLimit = minusLimit;
            PlusLimit = plusLimit;

            _moveDelay = new PlcTimer[1];
            for (int iPos = 0; iPos < 1; iPos++)
                _moveDelay[iPos] = new PlcTimer();

            _inSpeed = speed;
        }
        public void Initialize()
        {
            //XF_CurrMotorPosition.vFloat = 0;
            //XB_ErrMotorServoOn.vBit = false;

            for (int iter = 0; iter < 1; ++iter)
            {
                YF_TargetPosition[iter].vFloat = iter == 0 ? 100 : iter * 100;
            }
        }
        public void LogicWorking()
        {
            MoveWorking();
            Working();
            FeedBack();
        }

        private void FeedBack()
        {
            XB_ErrMotorServoOn.vBit = true;
            XF_MotorJogSpeedAck.vFloat = YF_MotorJogSpeedCmd.vFloat;

            XF_Position0PosiAck[0].vFloat = YF_TargetPosition[0].vFloat;
            XF_Position0SpeedAck[0].vFloat = YF_Position0Speed[0].vFloat;
            XI_Position0AccelAck[0].vFloat = YI_Position0Accel[0].vFloat;
        }

        private void SettingWorking()
        {
            string result = "Save Pos ";
            if (_stepSetting == 0)
            {
                if (YB_PositionWriteCmd.vBit == true)
                    _stepSetting = 10;
            }
            else if (_stepSetting == 10)
            {
                for (int iPos = 0; iPos < 1; iPos++)
                {
                    XF_Position0PosiAck[iPos].vFloat = YF_TargetPosition[iPos].vFloat;

                    YF_Position0Speed = new PlcAddr[1];
                    XF_Position0SpeedAck = new PlcAddr[1];
                    YI_Position0Accel = new PlcAddr[1];
                    XI_Position0AccelAck = new PlcAddr[1];


                    result += string.Format("{0}, ", XF_Position0PosiAck[iPos].vFloat.ToString());
                }
                Logger.Log.AppendLine(LogLevel.Info, "{1}, 모터 설정 쓰기 : {0}", result, Name);

                XB_PositionWriteCmdAck.vBit = true;
                _stepSetting = 20;
            }
            else if (_stepSetting == 20)
            {
                if (YB_PositionWriteCmd.vBit == false)
                {
                    XB_PositionWriteCmdAck.vBit = false;
                    _stepSetting = 0;
                }
            }
        }
        private void MoveWorking()
        {            
            PositionWorking(_homePosIdx, ref _homeStep, _homeDelay, YB_HomeCmd, XB_HomeCmdAck, XB_StatusHomeCompleteBit, 10, _inSpeed, "HOME");

            for (int iPos = 0; iPos < 1; iPos++)
            {
                PositionWorking(iPos, ref _moveStep[iPos], _moveDelay[iPos], YB_TargetMoveCmd[iPos], XB_TargetMoveAck[iPos], XB_TargetMoveComplete[iPos], YF_TargetPosition[iPos].vFloat, _inSpeed, string.Format("POSI NO = {0}", iPos));
                if (_moveStep[iPos] != 0)
                    return;
            }
        }
        private void PositionWorking(int p, ref int stepMove, PlcTimer moveDelay, PlcAddr yb_Position0MoveCmd, PlcAddr xb_Position0MoveCmdAck, PlcAddr xb_Position0Complete, double posi, double speed, string desc)
        {
            if (stepMove == 0)
            {
                if (yb_Position0MoveCmd.vBit == true)
                {
                    XB_PhysicalSensor.ToList().ForEach(delegate(PlcAddr f)
                    {
                        if (f != null)
                            f.vBit = false;
                    });
                    XB_StatusHomeInPosition.vBit = false;
                    xb_Position0MoveCmdAck.vBit = true;
                    XB_StatusMotorMoving.vBit = false;
                    xb_Position0Complete.vBit = false;
                    stepMove = 10;
                }
            }
            else if (stepMove == 10)
            {
                if (yb_Position0MoveCmd.vBit == false)
                {
                    for (int iter = 0; iter < 1; ++iter)
                        XB_TargetMoveComplete[iter].vBit = false;

                    xb_Position0MoveCmdAck.vBit = false;

                    _inTargetPosi = posi;
                    _inSpeed = speed;

                    Logger.Log.AppendLine(LogLevel.Info, "{0} 모터 {1} 이동 시퀀스 시작 {2} => {3}", Name, desc, XF_CurrMotorSpeed.vFloat, posi);

                    stepMove = 20;
                }
            }
            else if (stepMove == 20)
            {
                //기본. 
                XB_StatusMotorMoving.vBit = true;

                Logger.Log.AppendLine(LogLevel.Info, "{0} 모터 {1}  이동 시작", Name, desc);

                //속도 및 셋팅
                _isMinuseGo = _inTargetPosi < XF_CurrMotorPosition.vFloat;
                _moveComplete = false;
                _moving = true;
                stepMove = 30;
            }
            else if (stepMove == 30)
            {
                moveDelay.Start(1);
                stepMove = 40;
            }
            else if (stepMove == 40)
            {
                if (moveDelay)
                {
                    moveDelay.Stop();
                    stepMove = 50;
                }
            }
            else if (stepMove == 50)
            {
                if (_moveComplete && NotReply == false)
                {
                    xb_Position0Complete.vBit = true;
                    XB_StatusMotorMoving.vBit = false;
                    if (p < XB_PhysicalSensor.Length && XB_PhysicalSensor[p] != null)
                        XB_PhysicalSensor[p].vBit = true;
                    else if (p == _homePosIdx && XB_PhysicalSensor[0] != null)
                        XB_PhysicalSensor[0].vBit = true;

                    if (p == _homePosIdx)
                    {
                        XB_StatusHomeCompleteBit.vBit = true;
                        XB_StatusHomeInPosition.vBit = true;
                        XB_TargetMoveComplete[0].vBit = true;
                        XF_CurrMotorPosition.vFloat = 0;
                    }
                    Logger.Log.AppendLine(LogLevel.Info, "{0} 모터 {1} 이동 완료", Name, desc);
                    stepMove = 60;
                }
            }
            else if (stepMove == 60)
            {
                if (p >= 0 && p < 1)
                {
                    for (int iPos = 0; iPos < 1; iPos++)
                        if (YF_TargetPosition[iPos].vFloat == YF_TargetPosition[p].vFloat) XB_TargetMoveComplete[iPos].vBit = XB_TargetMoveComplete[p].vBit;
                }

                stepMove = 0;
                Logger.Log.AppendLine(LogLevel.Info, "{0} 모터 {1} 이동 시퀀스 종료", Name, desc);
            }
        }
        private void Working()
        {
            if (YB_MotorJogPlusMove.vBit)
            {
                //if (InJogPlus)
                //    Logger.Log.AppendLine(LogLevel.Info, "{0} 모터 PLUS JOG 이동 시작", Name);
                //else
                //    Logger.Log.AppendLine(LogLevel.Info, "{0} 모터 PLUS JOG 이동 종료", Name);

                //_inJogPlus = InJogPlus;
            }
            if (YB_MotorJogMinusMove.vBit)
            {
                //if (InJogMinus)
                //    Logger.Log.AppendLine(LogLevel.Info, "{0} 모터 MINUS JOG 이동 시작", Name);
                //else
                //    Logger.Log.AppendLine(LogLevel.Info, "{0} 모터 MINUS JOG 이동 종료", Name);

                //_inJogMinus = InJogMinus;
            }
            //Console.WriteLine(YI_MotorJog.vInt);
            if (_moving || YB_MotorJogPlusMove.vBit || YB_MotorJogMinusMove.vBit)
            {
                XB_StatusHomeInPosition.vBit = false;
                if ((XF_CurrMotorPosition.vFloat > PlusLimit && YB_MotorJogPlusMove.vBit) || (XF_CurrMotorPosition.vFloat < MinusLimit && YB_MotorJogMinusMove.vBit))
                    return;


                int position = 0;
                if (YB_MotorJogPlusMove.vBit || YB_MotorJogMinusMove.vBit)
                {
                    position = Convert.ToInt32(YB_MotorJogMinusMove.vBit ? XF_CurrMotorPosition.vFloat - _inSpeed :
                                                  YB_MotorJogPlusMove.vBit ? XF_CurrMotorPosition.vFloat + _inSpeed : XF_CurrMotorPosition.vFloat);
                }
                else
                {
                    position = Convert.ToInt32(_isMinuseGo ? XF_CurrMotorPosition.vFloat - _inSpeed : XF_CurrMotorPosition.vFloat + _inSpeed);
                }

                //if (position < 0)
                //    position = 0;

                if (position > PlusLimit)
                    position = PlusLimit;

                XF_CurrMotorPosition.vFloat = position;

                if (XF_CurrMotorPosition.vFloat <= _inTargetPosi && _isMinuseGo &&
                    (YB_MotorJogPlusMove.vBit || YB_MotorJogMinusMove.vBit) == false)
                {
                    XF_CurrMotorPosition.vFloat = (float)_inTargetPosi;
                    _moveComplete = true;
                    _moving = false;

                }
                else if (XF_CurrMotorPosition.vFloat >= _inTargetPosi && _isMinuseGo == false &&
                    (YB_MotorJogPlusMove.vBit || YB_MotorJogMinusMove.vBit) == false)
                {
                    XF_CurrMotorPosition.vFloat = (float)_inTargetPosi;
                    _moveComplete = true;
                    _moving = false;

                }

            }
        }
    }
}
