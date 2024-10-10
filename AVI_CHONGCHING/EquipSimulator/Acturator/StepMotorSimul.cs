using Dit.Framework.PLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipSimulator.Acturator
{
    public class StepMotorSimul
    {
        public VirtualMem PLC;
        public int SlaveNo;

        public PlcAddr XB_StatusHomeCompleteBit { get; set; }
        public PlcAddr XB_StatusHomeInPosition { get; set; }
        public PlcAddr XB_StatusMotorMoving { get; set; }
        public PlcAddr XB_StatusMinusLimitSet { get; set; }
        public PlcAddr XB_StatusPlusLimitSet { get; set; }
        public PlcAddr XB_StatusMotorServoOn { get; set; }
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
        public PlcAddr XF_MotorJogSpeedCmdAck { get; set; }

        public PlcAddr YB_PositionWriteCmd { get; set; }
        public PlcAddr XB_PositionWriteCmdAck { get; set; }

        public PlcAddr[] YB_PTPMoveCmd { get; set; }
        public PlcAddr[] XB_PTPMoveCmdAck { get; set; }
        public PlcAddr[] YF_PTPMovePosition { get; set; }
        public PlcAddr[] XF_PTPMovePositionAck { get; set; }
        public PlcAddr[] YF_PTPMoveSpeed { get; set; }
        public PlcAddr[] XF_PTPMoveSpeedAck { get; set; }
        public PlcAddr[] YF_PTPMoveAccel { get; set; }
        public PlcAddr[] XF_PTPMoveAccelAck { get; set; }
        public PlcAddr[] XB_StatusMotorInPosition { get; set; }

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
        public StepMotorSimul(int slave, int minusLimit, int plusLimit, int speed)
        {
            PositionCount = 1;
            _moveStep = new int[1];
            YB_PTPMoveCmd = new PlcAddr[1];
            XB_PTPMoveCmdAck = new PlcAddr[1];
            YF_PTPMovePosition = new PlcAddr[1];
            XF_PTPMovePositionAck = new PlcAddr[1];
            YF_PTPMoveSpeed = new PlcAddr[1];
            XF_PTPMoveSpeedAck = new PlcAddr[1];
            YF_PTPMoveAccel = new PlcAddr[1];
            XF_PTPMoveAccelAck = new PlcAddr[1];
            XB_StatusMotorInPosition = new PlcAddr[1];

            SlaveNo = slave;
            MinusLimit = minusLimit;
            PlusLimit = plusLimit;

            _moveDelay = new PlcTimer[1];
            for (int iPos = 0; iPos < 1; iPos++)
                _moveDelay[iPos] = new PlcTimer();

            _inSpeed = speed;
        }
        public void Initialize()
        {
            for (int iter = 0; iter < 1; ++iter)
            {
                YF_PTPMovePosition[iter].vFloat = iter == 0 ? 100 : iter * 100;
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
            XB_StatusMotorServoOn.vBit = true;
            XF_MotorJogSpeedCmdAck.vFloat = YF_MotorJogSpeedCmd.vFloat;

            XF_PTPMovePositionAck[0].vFloat = YF_PTPMovePosition[0].vFloat;
            XF_PTPMoveSpeedAck[0].vFloat = YF_PTPMoveSpeed[0].vFloat;
            XF_PTPMoveAccelAck[0].vFloat = YF_PTPMoveAccel[0].vFloat;
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
                    XF_PTPMovePositionAck[iPos].vFloat = YF_PTPMovePosition[iPos].vFloat;

                    YF_PTPMoveSpeed = new PlcAddr[1];
                    XF_PTPMoveSpeedAck = new PlcAddr[1];
                    YF_PTPMoveAccel = new PlcAddr[1];
                    XF_PTPMoveAccelAck = new PlcAddr[1];


                    result += string.Format("{0}, ", XF_PTPMovePositionAck[iPos].vFloat.ToString());
                }

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
                PositionWorking(iPos, ref _moveStep[iPos], _moveDelay[iPos], YB_PTPMoveCmd[iPos], XB_PTPMoveCmdAck[iPos], XB_StatusMotorInPosition[iPos], YF_PTPMovePosition[iPos].vFloat, _inSpeed, string.Format("POSI NO = {0}", iPos));
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
                        XB_StatusMotorInPosition[iter].vBit = false;

                    xb_Position0MoveCmdAck.vBit = false;

                    _inTargetPosi = posi;
                    _inSpeed = speed;

                    stepMove = 20;
                }
            }
            else if (stepMove == 20)
            {
                //기본. 
                XB_StatusMotorMoving.vBit = true;

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
                    if (p == _homePosIdx)
                    {
                        XB_StatusHomeCompleteBit.vBit = true;
                        XB_StatusHomeInPosition.vBit = true;
                        XB_StatusMotorInPosition[0].vBit = true;
                        XF_CurrMotorPosition.vFloat = 0;
                    }
                    stepMove = 60;
                }
            }
            else if (stepMove == 60)
            {
                if (p >= 0 && p < 1)
                {
                    for (int iPos = 0; iPos < 1; iPos++)
                        if (YF_PTPMovePosition[iPos].vFloat == YF_PTPMovePosition[p].vFloat) XB_StatusMotorInPosition[iPos].vBit = XB_StatusMotorInPosition[p].vBit;
                }

                stepMove = 0;
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
