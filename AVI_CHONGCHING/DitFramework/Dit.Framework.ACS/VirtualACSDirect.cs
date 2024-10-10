using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Comm;
using SPIIPLUSCOM660Lib;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using Dit.Framework.Comm;
using Dit.Framework.PLC;

namespace Dit.Framework.ACS
{
    public static partial class ACSMotor
    {
        public const string VEL = "VEL";
        public const string DEC = "DEC";
        public const string ACC = "ACC";
        public const string JERK = "JERK";
        public const string KDEC = "KDEC";

        public const string FMASK = "FMASK";
        public const string FDEF = "FDEF";

        public const string E_TYPE = "E_TYPE";

        public const string FPOS = "FPOS";
        public const string MFLAGS = "MFLAGS";



        public const string MST = "MST";
        public const string IST = "IST";
        public const string IND = "IND";


        public const string FAULT = "FAULT";



        public static int ACSC_MST_ENABLED = (int)(int)Math.Pow(2, 0);
        public static int ACSC_MST_OPEN = (int)Math.Pow(2, 1);
        public static int ACSC_MST_INPOS = (int)Math.Pow(2, 2);
        public static int ACSC_MST_MOVE = (int)Math.Pow(2, 3);
        public static int ACSC_MST_ACC = (int)Math.Pow(2, 4);


        public static int ACSC_IST_IND = (int)Math.Pow(2, 0);
        public static int ACSC_IST_IND2 = (int)Math.Pow(2, 1);
        public static int ACSC_IST_MARK = (int)Math.Pow(2, 2);
        public static int ACSC_IST_MARK2 = (int)Math.Pow(2, 3);



        public static int ACSC_MFLAGS_DUMMY = (int)Math.Pow(2, 0);
        public static int ACSC_MFLAGS_OPEN = (int)Math.Pow(2, 1);
        public static int ACSC_MFLAGS_MICRO = (int)Math.Pow(2, 2);
        public static int ACSC_MFLAGS_HOME = (int)Math.Pow(2, 3);
        public static int ACSC_MFLAGS_STEPPER = (int)Math.Pow(2, 4);
        public static int ACSC_MFLAGS_ENCLOOP = (int)Math.Pow(2, 5);
        public static int ACSC_MFLAGS_STEPENC = (int)Math.Pow(2, 6);
        public static int ACSC_MFLAGS_NANO = (int)Math.Pow(2, 7);
        public static int ACSC_MFLAGS_BRUSHL = (int)Math.Pow(2, 8);
        public static int ACSC_MFLAGS_BRUSHOK = (int)Math.Pow(2, 9);
        public static int ACSC_MFLAGS_PHASE2 = (int)Math.Pow(2, 10);
        public static int ACSC_MFLAGS_DBRAKE = (int)Math.Pow(2, 11);
        public static int ACSC_MFLAGS_INVENC = (int)Math.Pow(2, 12);
        public static int ACSC_MFLAGS_INVDOUT = (int)Math.Pow(2, 13);
        public static int ACSC_MFLAGS_NOTCH = (int)Math.Pow(2, 14);
        public static int ACSC_MFLAGS_NOFILT = (int)Math.Pow(2, 15);
        public static int ACSC_MFLAGS_BI_QUAD = (int)Math.Pow(2, 16);
        public static int ACSC_MFLAGS_DEFCON = (int)Math.Pow(2, 17);
        public static int ACSC_MFLAGS_FASTSC = (int)Math.Pow(2, 18);
        public static int ACSC_MFLAGS_ENMOD = (int)Math.Pow(2, 19);
        public static int ACSC_MFLAGS_DUALLOOP = (int)Math.Pow(2, 20);
        public static int ACSC_MFLAGS_LINEAR = (int)Math.Pow(2, 21);
        public static int ACSC_MFLAGS_ABSCOMM = (int)Math.Pow(2, 22);
        public static int ACSC_MFLAGS_BRAKE = (int)Math.Pow(2, 23);
        public static int ACSC_MFLAGS_HSSI = (int)Math.Pow(2, 24);
        public static int ACSC_MFLAGS_GANTRY = (int)Math.Pow(2, 25);
        public static int ACSC_MFLAGS_BI_QUAD1 = (int)Math.Pow(2, 26);
        public static int ACSC_MFLAGS_HALL = (int)Math.Pow(2, 27);
        public static int ACSC_MFLAGS_INVHALL = (int)Math.Pow(2, 28);
        public static int ACSC_MFLAGS_MODULO = (int)Math.Pow(2, 29);
        public static int ACSC_MFLAGS_USER1 = (int)Math.Pow(2, 30);
        public static int ACSC_MFLAGS_USER2 = (int)Math.Pow(2, 31);

    }
    public static partial class Extension
    {
        public static bool GetCmdBit(this Channel chn, int axis, string cmd, int bitInx)
        {
            int vv = chn.ReadVariableAsScalar(cmd, chn.ACSC_NONE, axis);
            return vv.GetBit(bitInx);
        }
        public static void SetCmdBit(this Channel chn, int axis, string cmd, int bitInx, bool value)
        {
            int vv = chn.ReadVariableAsScalar(cmd, chn.ACSC_NONE, axis);
            vv = vv.SetBit(bitInx, value);
            chn.WriteVariable(new int[] { vv }, cmd, chn.ACSC_NONE, axis, axis);
        }

        public static void SetCmdBit(this Channel chn, int axis, string cmd, int value)
        {
            int vv = chn.ReadVariableAsScalar(cmd, chn.ACSC_NONE, axis);
            vv = vv | value;
            chn.WriteVariable(new int[] { vv }, cmd, chn.ACSC_NONE, axis, axis);
        }

        public static void ClearCmdBit(this Channel chn, int axis, string cmd, int value)
        {
            int vv = chn.ReadVariableAsScalar(cmd, chn.ACSC_NONE, axis);
            vv = vv & ~value;
            chn.WriteVariable(new int[] { vv }, cmd, chn.ACSC_NONE, axis, axis);
        }


        public static int GetCmdInt(this Channel chn, int axis, string cmd)
        {
            int vv = chn.ReadVariableAsScalar(cmd, chn.ACSC_NONE, axis);
            return vv;
        }
        public static void SetCmdInt(this Channel chn, int axis, string cmd, int value)
        {
            chn.WriteVariable(new int[] { value }, cmd, chn.ACSC_NONE, axis, axis);
        }

        public static double GetCmdDouble(this Channel chn, int axis, string cmd)
        {
            double vv = chn.ReadVariableAsScalar(cmd, chn.ACSC_NONE, axis);
            return vv;
        }
        public static void SetCmdDouble(this Channel chn, int axis, string cmd, double value)
        {
            chn.WriteVariable(new double[] { value }, cmd, chn.ACSC_NONE, axis, axis);
        }

    }

    public class AcsSvoMotor
    {


        // ACS 연결 및 명령어 처리를 위한 인터페이스 클래스 생성
        public VirtualACSDirect VirAcs;

        public string Name { get; set; }
        public string SlayerName { get; set; }
        public int Axis { get; set; }
        public int SlayerAxis { get; set; }

        public PlcAddr XB_StatusHomeCompleteBit { get; set; }
        public PlcAddr XB_StatusHomeInPosition { get; set; }
        public PlcAddr XB_StatusMotorMoving { get; set; }
        public PlcAddr XB_StatusMotorInPosition { get; set; }
        public PlcAddr XB_StatusNegativeLimitSet { get; set; }
        public PlcAddr XB_StatusPositiveLimitSet { get; set; }
        public PlcAddr XB_StatusMotorServoOn { get; set; }
        public PlcAddr XB_ErrFatalFollowingError { get; set; }
        public PlcAddr XB_ErrAmpFaultError { get; set; }
        public PlcAddr XB_ErrI2TAmpFaultError { get; set; }

        public PlcAddr XF_CurrMotorPosition { get; set; }
        public PlcAddr XF_CurrMotorSpeed { get; set; }
        public PlcAddr XF_CurrMotorStress { get; set; }

        //겐츄리 및 그래버일 경우만 다름 사용=================
        public PlcAddr XB_StatusHomeCompleteBitSlayer { get; set; }
        public PlcAddr XB_StatusHomeInPositionSlayer { get; set; }
        public PlcAddr XB_StatusMotorMovingSlayer { get; set; }
        public PlcAddr XB_StatusMotorInPositionSlayer { get; set; }
        public PlcAddr XB_StatusNegativeLimitSetSlayer { get; set; }
        public PlcAddr XB_StatusPositiveLimitSetSlayer { get; set; }
        public PlcAddr XB_StatusMotorServoOnSlayer { get; set; }
        public PlcAddr XB_ErrFatalFollowingErrorSlayer { get; set; }
        public PlcAddr XB_ErrAmpFaultErrorSlayer { get; set; }
        public PlcAddr XB_ErrI2TAmpFaultErrorSlayer { get; set; }

        public PlcAddr XF_CurrMotorPositionSlayer { get; set; }
        public PlcAddr XF_CurrMotorSpeedSlayer { get; set; }
        public PlcAddr XF_CurrMotorStressSlayer { get; set; }
        //================================================

        public PlcAddr XI_CmdAckLogMsg { get; set; }
        public PlcAddr XI_CmdAckMotionLogCompleteMsg { get; set; }

        public PlcAddr YB_HomeCmd { get; set; }
        public PlcAddr XB_HomeCmdAck { get; set; }

        public PlcAddr YB_MotorJogMinusMove { get; set; }
        public PlcAddr YB_MotorJogPlusMove { get; set; }
        public PlcAddr YF_MotorJogSpeedCmd { get; set; }
        //public PlcAddr XF_MotorJogSpeedCmdAck { get; set; }

        //마크로 사용 추가=================
        public PlcAddr YF_Trigger1StartPosi { get; set; }
        public PlcAddr XF_Trigger1StartPosiAck { get; set; }

        public PlcAddr YF_Trigger2StartPosi { get; set; }
        public PlcAddr XF_Trigger2StartPosiAck { get; set; }

        public PlcAddr YF_Trigger3StartPosi { get; set; }
        public PlcAddr XF_Trigger3StartPosiAck { get; set; }

        public PlcAddr YF_Trigger1EndPosi { get; set; }
        public PlcAddr XF_Trigger1EndPosiAck { get; set; }

        public PlcAddr YF_Trigger2EndPosi { get; set; }
        public PlcAddr XF_Trigger2EndPosiAck { get; set; }

        public PlcAddr YF_Trigger3EndPosi { get; set; }
        public PlcAddr XF_Trigger3EndPosiAck { get; set; }

        public PlcAddr[] YB_Position0MoveCmd { get; set; }
        public PlcAddr[] XB_Position0MoveCmdAck { get; set; }



        public PlcAddr[] YF_Position1stPoint { get; set; }
        public PlcAddr[] XF_Position1stPointAck { get; set; }
        public PlcAddr[] YF_Position1stSpeed { get; set; }
        public PlcAddr[] XF_Position1stSpeedAck { get; set; }
        public PlcAddr[] XB_PositionComplete { get; set; }
        public PlcAddr[] YF_Position1stAccel { get; set; }
        public PlcAddr[] XF_Position1stAccelAck { get; set; }

        public AcsSvoMotor()
        {
            YB_Position0MoveCmd = new PlcAddr[PositionCount];
            XB_Position0MoveCmdAck = new PlcAddr[PositionCount];

            YF_Position1stPoint = new PlcAddr[PositionCount];
            XF_Position1stPointAck = new PlcAddr[PositionCount];
            YF_Position1stSpeed = new PlcAddr[PositionCount];
            XF_Position1stSpeedAck = new PlcAddr[PositionCount];

            XB_PositionComplete = new PlcAddr[PositionCount];

            YF_Position1stAccel = new PlcAddr[PositionCount];
            XF_Position1stAccelAck = new PlcAddr[PositionCount];

        }

        public void LogicWorking()
        {
            //임시. 
            XB_StatusMotorServoOn.vBit = true;
            XF_CurrMotorPosition.vFloat = (float)VirAcs.ACSChannel.GetFPosition(Axis);
            XF_CurrMotorSpeed.vFloat = (float)VirAcs.ACSChannel.GetFVelocity(Axis);
            int motorState = VirAcs.ACSChannel.GetMotorState(Axis);

            // 가져온 데이터는 아래와 같이 & 연산(비트연산)을
            bool isMoving = ((motorState & VirAcs.ACSChannel.ACSC_MST_MOVE) != 0);
            bool isInPosition = ((motorState & VirAcs.ACSChannel.ACSC_MST_INPOS) != 0);
            bool isMstAcc = ((motorState & VirAcs.ACSChannel.ACSC_MST_ACC) != 0);
            bool isMstEnable = ((motorState & VirAcs.ACSChannel.ACSC_MST_ENABLE) != 0);

            XB_StatusMotorInPosition.vBit = isInPosition;
            XB_StatusMotorMoving.vBit = isMoving;

            JogStep();
            HomeStep();

            for (int iPos = 0; iPos < PositionCount; iPos++)
                MoveStep(iPos);
        }

        private int HomeStepNo = 0;
        private PlcTimer _plcTmrHome = new PlcTimer("Home Seq Timer");
        public void HomeStep()
        {
            if (HomeStepNo == 0)
            {
                int vv = VirAcs.ACSChannel.ACSC_SAFETY_INT;

                if (YB_HomeCmd.vBit == true)
                {
                    XB_HomeCmdAck.vBit = true;

                    HomeStepNo = 10;
                }
            }
            else if (HomeStepNo == 10)
            {
                if (YB_HomeCmd.vBit == false)
                {
                    XB_HomeCmdAck.vBit = false;
                    HomeStepNo = 20;
                }
            }
            else if (HomeStepNo == 20)
            {
                VirAcs.ACSChannel.SetCmdDouble(Axis, ACSMotor.VEL, 20);
                VirAcs.ACSChannel.SetCmdDouble(Axis, ACSMotor.ACC, 1000);
                VirAcs.ACSChannel.SetCmdDouble(Axis, ACSMotor.DEC, 1000);
                VirAcs.ACSChannel.SetCmdDouble(Axis, ACSMotor.JERK, 1000 * 3);
                VirAcs.ACSChannel.SetCmdDouble(Axis, ACSMotor.KDEC, 1000 * 3);

                //FMASK
                VirAcs.ACSChannel.SetCmdBit(Axis, ACSMotor.FMASK,
                    VirAcs.ACSChannel.ACSC_SAFETY_CPE |
                    VirAcs.ACSChannel.ACSC_SAFETY_RL |
                    VirAcs.ACSChannel.ACSC_SAFETY_LL);

                //FDEF
                VirAcs.ACSChannel.SetCmdBit(Axis, ACSMotor.FDEF, VirAcs.ACSChannel.ACSC_SAFETY_CPE);
                VirAcs.ACSChannel.ClearCmdBit(Axis, ACSMotor.FDEF,
                      VirAcs.ACSChannel.ACSC_SAFETY_RL |
                      VirAcs.ACSChannel.ACSC_SAFETY_LL |
                      VirAcs.ACSChannel.ACSC_SAFETY_SRL |
                      VirAcs.ACSChannel.ACSC_SAFETY_SLL
                );


                HomeStepNo = 30;
            }
            else if (HomeStepNo == 30)
            {
                int fdef = VirAcs.ACSChannel.GetCmdInt(Axis, ACSMotor.FDEF);
                if (fdef.OnBitValue(VirAcs.ACSChannel.ACSC_SAFETY_LL) == false && fdef.OnBitValue(VirAcs.ACSChannel.ACSC_SAFETY_RL) == false)
                {
                    VirAcs.ACSChannel.Disable(Axis);

                    //FCLEAR FAULT
                    VirAcs.ACSChannel.FaultClear(Axis);

                    //Encoder Type 설정 Sin-Cos 형 Encoder
                    VirAcs.ACSChannel.SetCmdInt(Axis, ACSMotor.E_TYPE, 4);

                    //현재 위치 설정                    
                    VirAcs.ACSChannel.SetFPosition(Axis, 0);

                    //모터 전기가 초기화

                    //#BRUSHOK
                    VirAcs.ACSChannel.ClearCmdBit(Axis, ACSMotor.MFLAGS, ACSMotor.ACSC_MFLAGS_BRUSHOK);

                    int mflags = VirAcs.ACSChannel.GetCmdInt(Axis, ACSMotor.MFLAGS);
                    if (mflags.OnBitValue(ACSMotor.ACSC_MFLAGS_HALL) == false) //Hall센서 사용 여부 확인
                    {
                        if (mflags.OnBitValue(ACSMotor.ACSC_MFLAGS_BRUSHOK) == false) //0번 모터 전기각 여부 확인
                        {
                            //0번 모터 서보 On
                            VirAcs.ACSChannel.Enable(Axis);
                            _plcTmrHome.Start(5000);
                        }
                    }
                    HomeStepNo = 32;
                }
                else
                {
                    //시퀀스 이동 실패 시 홈스텝 알람 이동 홈 시퀀스 실패
                }
            }
            else if (HomeStepNo == 32)
            {
                int mst = VirAcs.ACSChannel.GetCmdInt(Axis, ACSMotor.MST);

                if (mst.OnBitValue(ACSMotor.ACSC_MST_ENABLED))
                {
                    _plcTmrHome.Stop();

                    VirAcs.ACSChannel.Commut(Axis);

                    _plcTmrHome.Start(5000);
                    HomeStepNo = 33;
                }
                else
                {
                    if (_plcTmrHome)
                    {
                        Console.WriteLine("Time Out");
                    }
                }
            }
            else if (HomeStepNo == 33)
            {
                int mflags = VirAcs.ACSChannel.GetCmdInt(Axis, ACSMotor.MFLAGS);

                if (mflags.OnBitValue(ACSMotor.ACSC_MFLAGS_BRUSHOK))
                {
                    _plcTmrHome.Stop();

                    //0번 모터 서보 On
                    VirAcs.ACSChannel.Disable(Axis);

                    _plcTmrHome.Start(5000);
                    HomeStepNo = 34;
                }
                else
                {
                    if (_plcTmrHome)
                    {
                        Console.WriteLine("Time Out");
                    }
                }

            }
            else if (HomeStepNo == 34)
            {
                int mflags = VirAcs.ACSChannel.GetCmdInt(Axis, ACSMotor.MFLAGS);

                if (mflags.OnBitValue(ACSMotor.ACSC_MFLAGS_BRUSHOK))
                {
                    _plcTmrHome.Stop();
                    VirAcs.ACSChannel.Enable(Axis);

                    _plcTmrHome.Start(0, 500);
                    HomeStepNo = 35;
                }
                else
                {
                    if (_plcTmrHome)
                    {
                        Console.WriteLine("Time Out");
                    }
                }

            }
            else if (HomeStepNo == 35)
            {
                if (_plcTmrHome)
                {
                    _plcTmrHome.Stop();

                    int mst = VirAcs.ACSChannel.GetCmdInt(Axis, ACSMotor.MST);
                    if (mst.OnBitValue(ACSMotor.ACSC_MST_ENABLED)) //모터 모터 정지 여부 확인 
                    {
                        VirAcs.ACSChannel.Jog(0, Axis, VirAcs.ACSChannel.ACSC_NEGATIVE_DIRECTION);

                        HomeStepNo = 40;
                    }
                }
            }
            else if (HomeStepNo == 40)
            {
                if (_plcTmrHome == false)
                {
                    int falut = VirAcs.ACSChannel.GetCmdInt(Axis, ACSMotor.FAULT);
                    if (falut.OnBitValue(VirAcs.ACSChannel.ACSC_SAFETY_LL) == true)
                    {
                        _plcTmrHome.Stop();
                        VirAcs.ACSChannel.Halt(Axis);
                        HomeStepNo = 50;
                    }
                }
                else
                {
                    _plcTmrHome.Stop();
                }
            }
            else if (HomeStepNo == 50)
            {
                int mst = VirAcs.ACSChannel.GetCmdInt(Axis, ACSMotor.MST);

                if (mst.OnBitValue(ACSMotor.ACSC_MST_MOVE) == false) //모터 모터 정지 여부 확인 
                {
                    VirAcs.ACSChannel.Jog(0, Axis, VirAcs.ACSChannel.ACSC_POSITIVE_DIRECTION);

                    HomeStepNo = 60;

                }

            }
            else if (HomeStepNo == 60)
            {
                int falut = VirAcs.ACSChannel.GetCmdInt(Axis, ACSMotor.FAULT);
                if (falut.OnBitValue(VirAcs.ACSChannel.ACSC_SAFETY_LL) == true)
                {
                    _plcTmrHome.Stop();
                    VirAcs.ACSChannel.Halt(Axis);
                    HomeStepNo = 70;
                }
            }
            else if (HomeStepNo == 70)
            {
                int mst = VirAcs.ACSChannel.GetCmdInt(Axis, ACSMotor.MST);
                if (mst.OnBitValue(ACSMotor.ACSC_MST_MOVE) == false) //모터 모터 정지 여부 확인 
                {
                    VirAcs.ACSChannel.ClearCmdBit(Axis, ACSMotor.IST, ACSMotor.ACSC_IST_IND);
                    VirAcs.ACSChannel.Jog(0, Axis, VirAcs.ACSChannel.ACSC_POSITIVE_DIRECTION);

                    _plcTmrHome.Start(10);
                    HomeStepNo = 80;
                }

            }
            else if (HomeStepNo == 80)
            {
                if (_plcTmrHome == false)
                {
                    int ist = VirAcs.ACSChannel.GetCmdInt(Axis, ACSMotor.IST);
                    if (ist.OnBitValue(ACSMotor.ACSC_IST_IND) == true)
                    {
                        _plcTmrHome.Stop();
                        VirAcs.ACSChannel.Halt(Axis);
                        HomeStepNo = 90;
                    }
                }
                else
                {
                    _plcTmrHome.Stop();
                }
            }
            else if (HomeStepNo == 90)
            {
                int mst = VirAcs.ACSChannel.GetCmdInt(Axis, ACSMotor.MST);
                if (mst.OnBitValue(ACSMotor.ACSC_MST_MOVE) == false) //모터 모터 정지 여부 확인 
                {
                    double fpos = VirAcs.ACSChannel.GetCmdDouble(Axis, ACSMotor.FPOS);
                    double ind = VirAcs.ACSChannel.GetCmdDouble(Axis, ACSMotor.IND);
                    //VirAcs.ACSChannel.SetCmdDouble(Axis, ACSMotor.FPOS, fpos - ind);

                    VirAcs.ACSChannel.SetFPosition(Axis, fpos - ind);
                    VirAcs.ACSChannel.ToPoint(0, Axis, 0);

                    HomeStepNo = 100;
                }
            }
            else if (HomeStepNo == 100)
            {
                int mst = VirAcs.ACSChannel.GetCmdInt(Axis, ACSMotor.MST);

                if (mst.GetBit(ACSMotor.ACSC_MST_MOVE) == false) //모터 모터 정지 여부 확인 
                {
                    //FMASK
                    VirAcs.ACSChannel.SetCmdBit(Axis, ACSMotor.FMASK,
                        VirAcs.ACSChannel.ACSC_SAFETY_CPE |
                        VirAcs.ACSChannel.ACSC_SAFETY_RL |
                        VirAcs.ACSChannel.ACSC_SAFETY_LL);

                    //FDEF
                    VirAcs.ACSChannel.SetCmdBit(Axis, ACSMotor.FDEF, VirAcs.ACSChannel.ACSC_SAFETY_CPE | VirAcs.ACSChannel.ACSC_SAFETY_RL | VirAcs.ACSChannel.ACSC_SAFETY_LL);
                    VirAcs.ACSChannel.ClearCmdBit(Axis, ACSMotor.FDEF, VirAcs.ACSChannel.ACSC_SAFETY_SRL | VirAcs.ACSChannel.ACSC_SAFETY_SLL);

                    HomeStepNo = 110;
                }
            }
            else if (HomeStepNo == 110)
            {
                int mst = VirAcs.ACSChannel.GetCmdInt(Axis, ACSMotor.MST);
                if (mst.GetBit(ACSMotor.ACSC_MST_INPOS) == true) //모터 모터 정지 여부 확인 
                {
                    HomeStepNo = 120;
                }
            }
            else if (HomeStepNo == 120)
            {
                XB_StatusHomeInPosition.vBit = true;
                XB_StatusHomeCompleteBit.vBit = true;
                XB_StatusMotorInPosition.vBit = true;

                HomeStepNo = 0;
            }
        }

        private int[] MoveStepNo = new int[32];
        public int PositionCount = 15;
        public void MoveStep(int iPos)
        {
            //인포지션 체크. 
            XB_PositionComplete[iPos].vBit = (Math.Abs(XF_Position1stPointAck[iPos].vFloat - XF_CurrMotorPosition.vFloat) < 10f);

            if (MoveStepNo[iPos] == 0)
            {
                if (YB_Position0MoveCmd[iPos].vBit == true)
                {
                    XB_Position0MoveCmdAck[iPos].vBit = true;

                    MoveStepNo[iPos] = 10;
                }
            }
            else if (MoveStepNo[iPos] == 10)
            {
                if (YB_Position0MoveCmd[iPos].vBit == false)
                {
                    XB_Position0MoveCmdAck[iPos].vBit = false;
                    MoveStepNo[iPos] = 20;
                }
            }
            else if (MoveStepNo[iPos] == 20)
            {
                VirAcs.ACSChannel.SetCmdDouble(Axis, ACSMotor.VEL, (double)XF_Position1stSpeedAck[iPos].vFloat);
                VirAcs.ACSChannel.SetCmdDouble(Axis, ACSMotor.ACC, (double)XF_Position1stAccelAck[iPos].vFloat);
                VirAcs.ACSChannel.SetCmdDouble(Axis, ACSMotor.DEC, (double)XF_Position1stAccelAck[iPos].vFloat);
                VirAcs.ACSChannel.SetCmdDouble(Axis, ACSMotor.JERK, (double)XF_Position1stAccelAck[iPos].vFloat * 3);
                VirAcs.ACSChannel.SetCmdDouble(Axis, ACSMotor.KDEC, (double)XF_Position1stAccelAck[iPos].vFloat * 3);


                VirAcs.ACSChannel.ToPoint(0, Axis, (double)XF_Position1stPointAck[iPos].vFloat);


                MoveStepNo[iPos] = 30;
            }
            else if (MoveStepNo[iPos] == 30)
            {
                if (XB_StatusMotorInPosition.vBit == true)
                {
                    MoveStepNo[iPos] = 40;
                }
            }
            else if (MoveStepNo[iPos] == 40)
            {
                if (XB_PositionComplete[iPos].vBit == true && XB_StatusMotorInPosition.vBit == true)
                {
                    MoveStepNo[iPos] = 50;
                }
            }
            else if (MoveStepNo[iPos] == 50)
            {

                MoveStepNo[iPos] = 0;
            }
        }


        private int JogPlusStep = 0;
        private int JogMinusStep = 0;
        public void JogStep()
        {
            if (JogPlusStep == 0)
            {
                if (YB_MotorJogPlusMove.vBit == true)
                {
                    VirAcs.ACSChannel.Jog(0, Axis, VirAcs.ACSChannel.ACSC_POSITIVE_DIRECTION);
                    JogPlusStep = 10;
                }
            }
            else if (JogPlusStep == 10)
            {
                if (YB_MotorJogPlusMove.vBit == false)
                {
                    VirAcs.ACSChannel.Halt(Axis);
                    JogPlusStep = 0;
                }
            }

            if (JogMinusStep == 0)
            {
                if (YB_MotorJogMinusMove.vBit == true)
                {
                    VirAcs.ACSChannel.Jog(0, Axis, VirAcs.ACSChannel.ACSC_NEGATIVE_DIRECTION);
                    JogMinusStep = 10;
                }
            }
            else if (JogMinusStep == 10)
            {
                if (YB_MotorJogMinusMove.vBit == false)
                {
                    VirAcs.ACSChannel.Halt(Axis);
                    JogMinusStep = 0;
                }
            }
        }

    }

    public class VirtualACSDirect : IVirtualMem
    {
        //PLC
        public PlcAddr YB_EquipMode { get; set; }
        public PlcAddr YB_CheckAlarmStatus { get; set; }
        public PlcAddr YB_UpperInterfaceWorking { get; set; }
        public PlcAddr YB_LowerInterfaceWorking { get; set; }

        public PlcAddr YB_ImmediateStopCmd { get; set; }
        public PlcAddr XB_ImmediateStopCmdAck { get; set; }

        public PlcAddr YB_PmacValueSave { get; set; }
        public PlcAddr XB_PmacValueSaveAck { get; set; }

        public PlcAddr XB_PmacReady { get; set; }
        public PlcAddr XB_PmacAlive { get; set; }
        public PlcAddr XB_PmacHeavyAlarm { get; set; }

        public PlcAddr YB_PinUpMotorInterlockOffCmd { get; set; }
        public PlcAddr XB_PinUpMotorInterlockOffCmdAck { get; set; }
        public PlcAddr XB_PinUpInterlockOff { get; set; }

        public PlcAddr YB_ReviewTimerOverCmd { get; set; }
        public PlcAddr XB_ReviewTimerOverCmdAck { get; set; }

        public PlcAddr YB_PmacResetCmd { get; set; }
        public PlcAddr XB_PmacResetCmdAck { get; set; }

        public PlcAddr YB_Trigger1Cmd { get; set; }
        public PlcAddr YB_Trigger2Cmd { get; set; }
        public PlcAddr YB_Trigger3Cmd { get; set; }

        public PlcAddr XB_ReviewUsingPmac { get; set; }



        public PlcAddr YB_ReviewStartCmd { get; set; }
        public PlcAddr XB_ReviewStartCmdAck { get; set; }

        public PlcAddr YB_ReviewCompleteCmdAck { get; set; }
        public PlcAddr XB_ReviewCompleteCmd { get; set; }

        public PlcAddr YI_ReviewPositionCount { get; set; }

        public PlcAddr[] YF_ReviewXStartPosition { get; set; }
        public PlcAddr[] YF_ReviewYStartPosition { get; set; }

        public PlcAddr[] YF_ReviewXEndPosition { get; set; }
        public PlcAddr[] YF_ReviewYEndPosition { get; set; }




        private const int MEM_SIZE = 102400;

        public int[] ACS_INT = new int[MEM_SIZE];
        public double[] ACS_REAR = new double[MEM_SIZE];

        public int[] CTRL_INT = new int[MEM_SIZE];
        public double[] CTRL_REAR = new double[MEM_SIZE];

        private string _ip = string.Empty;
        private int _port = 0;

        private bool _isRunning = false;
        // ACS 연결 및 명령어 처리를 위한 인터페이스 클래스 생성
        public Channel ACSChannel = new Channel();
        private Thread _motorWorker = null;
        private Thread _motorStatusWorker = null;

        public double CurrWorkingTime { get; set; }
        public double MaxWorkingTime { get; set; }

        public VirtualACSDirect(string name, string ip, int port)
        {
            _ip = ip;
            _port = port;
            _motorStatusWorker = new Thread(new ThreadStart(MotorStatusWorker_DoWork));
            _motorStatusWorker.Priority = ThreadPriority.Highest;


            _motorWorker = new Thread(new ThreadStart(MotorWorker_DoWork));
            _motorWorker.Priority = ThreadPriority.Highest;
        }
        private void MotorStatusWorker_DoWork()
        {
            //while (_isRunning)
            //{
            //    float vv = (float)ACSChannel.GetFPosition(0);
            //}
        }
        private void MotorWorker_DoWork()
        {
            DateTime start, end;
            while (_isRunning)
            {
                start = DateTime.Now;
                LogicWorking();

                CurrWorkingTime = (DateTime.Now - start).TotalMilliseconds;
                if (MaxWorkingTime < CurrWorkingTime)
                    MaxWorkingTime = CurrWorkingTime;
            }
        }

        //메소드 연결
        public override int Open()
        {
            try
            {
                motors.ForEach(f => f.VirAcs = this);

                ACSChannel.OpenCommEthernetTCP(_ip.ToString(), _port);

                var strTemp1 = ACSChannel.Transaction("?SYSINFO(13)");
                var strTemp2 = ACSChannel.Transaction("?SYSINFO(10)");

                _isRunning = true;
                _motorWorker.Start();
                _motorStatusWorker.Start();

                return TRUE;
            }
            catch
            {
                return FALSE;
            }
        }
        public override int Close()
        {
            try
            {
                _isRunning = false;

                _motorWorker.Join();
                _motorStatusWorker.Join();

                ACSChannel.CloseComm();

                return TRUE;
            }
            catch
            {
                return FALSE;
            }
        }

        //메소드 동기화
        public override int ReadFromPLC(PlcAddr addr, int wordSize)
        {
            return 0;
        }
        public override int WriteToPLC(PlcAddr addr, int wordSize)
        {
            return 0;
        }

        //메소드 비트
        public override bool GetBit(PlcAddr addr)
        {
            throw new Exception("미 구현");
        }
        public override void SetBit(PlcAddr addr)
        {
            throw new Exception("미 구현");
        }
        public override void ClearBit(PlcAddr addr)
        {
            throw new Exception("미 구현");
        }
        public override void SetBit(PlcAddr addr, bool value)
        {
            throw new Exception("미 구현");
        }
        public override void Toggle(PlcAddr addr)
        {
            throw new Exception("미 구현");
        }
        public bool[] GetBists(PlcAddr addr, int wordSize, out int result)
        {
            throw new Exception("미 구현");
            //return new bool[0];
        }

        //메소드 - STRING
        public override int SetAscii(PlcAddr addr, string text)
        {
            throw new Exception("미 구현");
            //return 0;
        }
        //메소드 - SHORT
        public override short GetShort(PlcAddr addr)
        {
            throw new Exception("미 구현");
            //return 0;
        }
        public override void SetShort(PlcAddr addr, short value)
        {
            throw new Exception("미 구현");
        }
        //메소드 - SHORT        
        public override short[] GetShorts(PlcAddr addr, int wordSize, out int result)
        {
            throw new Exception("미 구현");
        }
        public override void SetShorts(PlcAddr addr, short[] values, out int result)
        {
            throw new Exception("미 구현");
        }

        //메소드 - INT32
        public override int GetInt32(PlcAddr addr)
        {
            throw new Exception("미 구현");
        }
        public override void SetInt32(PlcAddr addr, int value)
        {
            throw new Exception("미 구현");
        }

        //읽어온 메모리에서 읽어오는 함수.
        public override bool VirGetBit(PlcAddr addr)
        {            //jys:: addr 무조건 int변환. 필요시, 변환 자료형 확인 필요            
            return VirGetInt32(addr).GetBit(addr.Bit);
        }
        public override void VirSetBit(PlcAddr addr, bool value)
        {
            int vv = VirGetInt32(addr).SetBit(addr.Bit, value);
            VirSetInt32(addr, vv);
        }
        public override short VirGetShort(PlcAddr addr)
        {
            throw new Exception("미구현 메모리");
        }
        public override void VirSetShort(PlcAddr addr, short value)
        {
            throw new Exception("미구현 메모리");
        }

        public override string VirGetAscii(PlcAddr addr)
        {
            throw new Exception("미구현 메모리");
        }
        public override void VirSetAscii(PlcAddr addr, string value)
        {
            throw new Exception("미구현 메모리");
        }
        public override int VirGetInt32(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.AI)
            {
                return ACS_INT[addr.Addr];
            }
            else if (addr.Type == PlcMemType.CI)
            {
                return CTRL_INT[addr.Addr];
            }
            else
                throw new Exception("ADDR TYPE ERROR");
        }
        public override void VirSetInt32(PlcAddr addr, int value)
        {
            if (addr.Type == PlcMemType.AI)
            {
                ACS_INT[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.CI)
            {
                CTRL_INT[addr.Addr] = value;
            }
            else
                throw new Exception("ADDR TYPE ERROR");
        }

        public override bool[] VirGetBits(PlcAddr addr, int wordSize)
        {
            throw new Exception("미구현 메모리");
        }
        public override short[] VirGetShorts(PlcAddr addr)
        {
            throw new Exception("미구현 메모리");
        }
        public override float VirGetFloat(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.AR)
            {
                return (float)ACS_REAR[addr.Addr];
            }
            else if (addr.Type == PlcMemType.CR)
            {
                return (float)CTRL_REAR[addr.Addr];
            }
            else
                throw new Exception("ADDR TYPE ERROR");
        }
        public override void VirSetFloat(PlcAddr addr, float value)
        {
            if (addr.Type == PlcMemType.AR)
            {
                ACS_REAR[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.CR)
            {
                CTRL_REAR[addr.Addr] = value;
            }
            else
                throw new Exception("ADDR TYPE ERROR");
        }



        public List<AcsSvoMotor> motors = new List<AcsSvoMotor>();
        public void LogicWorking()
        {

            motors.ForEach(f => f.LogicWorking());
            SettingStep();
        }

        public void LoadSetting(string line)
        {
            YB_EquipMode                     /**/ = AddressMgr.GetAddress("PMAC_YB_EquipState", 0);
            YB_CheckAlarmStatus              /**/ = AddressMgr.GetAddress("PMAC_YB_EquipState", 1);
            YB_UpperInterfaceWorking         /**/ = AddressMgr.GetAddress("PMAC_YB_EquipState", 2);
            YB_LowerInterfaceWorking         /**/ = AddressMgr.GetAddress("PMAC_YB_EquipState", 3);

            XB_PmacReady                     /**/ = AddressMgr.GetAddress("PMAC_XB_PmacState", 0);
            XB_PmacAlive                     /**/ = AddressMgr.GetAddress("PMAC_XB_PmacState", 1);
            XB_ReviewUsingPmac               /**/ = AddressMgr.GetAddress("PMAC_XB_PmacState", 2);
            XB_PinUpInterlockOff             /**/ = AddressMgr.GetAddress("PMAC_XB_PmacState", 4);

            //PMac.XB_PmacHeavyAlarm                /**/ = AddressMgr.GetAddress("PMAC_XB_PmacMidAlarm", 0);
            YB_PinUpMotorInterlockOffCmd     /**/ = AddressMgr.GetAddress("PMAC_YB_CommonCmd", 0);
            XB_PinUpMotorInterlockOffCmdAck  /**/ = AddressMgr.GetAddress("PMAC_XB_CommonCmdAck", 0);
            YB_PmacResetCmd                  /**/ = AddressMgr.GetAddress("PMAC_YB_CommonCmd", 1);
            XB_PmacResetCmdAck               /**/ = AddressMgr.GetAddress("PMAC_XB_CommonCmdAck", 1);
            YB_ImmediateStopCmd              /**/ = AddressMgr.GetAddress("PMAC_YB_CommonCmd", 2);
            XB_ImmediateStopCmdAck           /**/ = AddressMgr.GetAddress("PMAC_XB_CommonCmdAck", 2);
            YB_PmacValueSave                 /**/ = AddressMgr.GetAddress("PMAC_YB_CommonCmd", 3);
            XB_PmacValueSaveAck              /**/ = AddressMgr.GetAddress("PMAC_XB_CommonCmdAck", 3);
            YB_ReviewTimerOverCmd            /**/ = AddressMgr.GetAddress("PMAC_YB_CommonCmd", 4);
            XB_ReviewTimerOverCmdAck         /**/ = AddressMgr.GetAddress("PMAC_XB_CommonCmdAck", 4);

            YB_Trigger1Cmd                   /**/ = AddressMgr.GetAddress("PMAC_YB_EquipState", 4);
            YB_Trigger2Cmd                   /**/ = AddressMgr.GetAddress("PMAC_YB_EquipState", 5);
            YB_Trigger3Cmd                   /**/ = AddressMgr.GetAddress("PMAC_YB_EquipState", 6);

            //ScanX.YF_Trigger1StartPosi             /**/ = AddressMgr.GetAddress("Trigger1StartPosCmd", 0);
            //ScanX.XF_Trigger1StartPosiAck          /**/ = AddressMgr.GetAddress("Trigger1StartPosCmdAck", 0);
            //ScanX.YF_Trigger2StartPosi             /**/ = AddressMgr.GetAddress("Trigger2StartPosCmd", 0);
            //ScanX.XF_Trigger2StartPosiAck          /**/ = AddressMgr.GetAddress("Trigger2StartPosCmdAck", 0);
            //ScanX.YF_Trigger3StartPosi             /**/ = AddressMgr.GetAddress("Trigger3StartPosCmd", 0);
            //ScanX.XF_Trigger3StartPosiAck          /**/ = AddressMgr.GetAddress("Trigger3StartPosCmdAck", 0);

            //ScanX.YF_Trigger1EndPosi               /**/ = AddressMgr.GetAddress("Trigger1EndPosCmd", 0);
            //ScanX.XF_Trigger1EndPosiAck            /**/ = AddressMgr.GetAddress("Trigger1EndPosCmdAck", 0);
            //ScanX.YF_Trigger2EndPosi               /**/ = AddressMgr.GetAddress("Trigger2EndPosCmd", 0);
            //ScanX.XF_Trigger2EndPosiAck            /**/ = AddressMgr.GetAddress("Trigger2EndPosCmdAck", 0);
            //ScanX.YF_Trigger3EndPosi               /**/ = AddressMgr.GetAddress("Trigger3EndPosCmd", 0);
            //ScanX.XF_Trigger3EndPosiAck            /**/ = AddressMgr.GetAddress("Trigger3EndPosCmdAck", 0);


            for (int jPos = 0; jPos < motors.Count; jPos++)
            {
                //string motor = motors[jPos].Name;
                //string slayerMotor = motors[jPos].SlayerName;

                int axis = motors[jPos].Axis;
                string axisStr = (axis + 1).ToString("D2");
                int slayerAxis = motors[jPos].Axis - 1;
                string slayerAxisStr = (slayerAxis + 1).ToString("D2");

                motors[jPos].XB_StatusHomeCompleteBit                       /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_StatusHomeCompleteBit"), axis);
                motors[jPos].XB_StatusHomeInPosition                        /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_StatusHomeInPosition"), axis);
                motors[jPos].XB_StatusMotorMoving                           /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_StatusMotorMoving"), axis);
                motors[jPos].XB_StatusMotorInPosition                       /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_StatusMotorInPosition"), axis);
                motors[jPos].XB_StatusNegativeLimitSet                      /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_StatusNegativeLimitSet"), axis);
                motors[jPos].XB_StatusPositiveLimitSet                      /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_StatusPositiveLimitSet"), axis);
                motors[jPos].XB_StatusMotorServoOn                          /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_ErrMotorServoOn"), axis);
                motors[jPos].XB_ErrFatalFollowingError                      /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_ErrFatalFollowingError"), axis);
                motors[jPos].XB_ErrAmpFaultError                            /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_ErrAmpFaultError"), axis);
                motors[jPos].XB_ErrI2TAmpFaultError                         /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_ErrI2TAmpFaultError"), axis);

                motors[jPos].XF_CurrMotorPosition                           /**/  = AddressMgr.GetAddress(string.Format("Axis{0}_XF_CurrentMotorPosition", axisStr));
                motors[jPos].XF_CurrMotorSpeed                              /**/  = AddressMgr.GetAddress(string.Format("Axis{0}_XF_CurrentMotorSpeed", axisStr));
                motors[jPos].XF_CurrMotorStress                             /**/  = AddressMgr.GetAddress(string.Format("Axis{0}_XI_CurrentMotorStress", axisStr));

                motors[jPos].XB_StatusHomeCompleteBitSlayer                 /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_StatusHomeCompleteBit"), slayerAxis);
                motors[jPos].XB_StatusHomeInPositionSlayer                  /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_StatusHomeInPosition"), slayerAxis);
                motors[jPos].XB_StatusMotorMovingSlayer                     /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_StatusMotorMoving"), slayerAxis);
                motors[jPos].XB_StatusMotorInPositionSlayer                 /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_StatusMotorInPosition"), slayerAxis);
                motors[jPos].XB_StatusNegativeLimitSetSlayer                /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_StatusNegativeLimitSet"), slayerAxis);
                motors[jPos].XB_StatusPositiveLimitSetSlayer                /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_StatusPositiveLimitSet"), slayerAxis);
                motors[jPos].XB_StatusMotorServoOnSlayer                    /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_ErrMotorServoOn"), slayerAxis);
                motors[jPos].XB_ErrFatalFollowingErrorSlayer                /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_ErrFatalFollowingError"), slayerAxis);
                motors[jPos].XB_ErrAmpFaultErrorSlayer                      /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_ErrAmpFaultError"), slayerAxis);
                motors[jPos].XB_ErrI2TAmpFaultErrorSlayer                   /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_ErrI2TAmpFaultError"), slayerAxis);

                motors[jPos].XF_CurrMotorPositionSlayer                     /**/  = AddressMgr.GetAddress(string.Format("Axis{0}_XF_CurrentMotorPosition", slayerAxisStr));
                motors[jPos].XF_CurrMotorSpeedSlayer                        /**/  = AddressMgr.GetAddress(string.Format("Axis{0}_XF_CurrentMotorSpeed", slayerAxisStr));
                motors[jPos].XF_CurrMotorStressSlayer                       /**/  = AddressMgr.GetAddress(string.Format("Axis{0}_XI_CurrentMotorStress", slayerAxisStr));

                motors[jPos].YB_HomeCmd                                     /**/  = AddressMgr.GetAddress(string.Format("PMAC_YB_HomeCmd"), axis);
                motors[jPos].XB_HomeCmdAck                                  /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_HomeCmdAck"), axis);
                motors[jPos].YB_MotorJogMinusMove                           /**/  = AddressMgr.GetAddress(string.Format("PMAC_YB_MinusJogCmd"), axis);
                motors[jPos].YB_MotorJogPlusMove                            /**/  = AddressMgr.GetAddress(string.Format("PMAC_YB_PlusJogCmd"), axis);
                motors[jPos].YF_MotorJogSpeedCmd                            /**/  = AddressMgr.GetAddress(string.Format("Axis{0}_YB_JogSpeed", axisStr));
                motors[jPos].XI_CmdAckLogMsg                                /**/  = AddressMgr.GetAddress(string.Format("PMAC_XB_CmdAckLogMsg"), axis);

                for (int iPos = 1; iPos <= motors[jPos].PositionCount; iPos++)
                {

                    motors[jPos].YB_Position0MoveCmd[iPos - 1]        /**/= AddressMgr.GetAddress(string.Format("Axis{0}_YB_PositionMoveCmd", axisStr), iPos - 1);
                    motors[jPos].XB_Position0MoveCmdAck[iPos - 1]     /**/= AddressMgr.GetAddress(string.Format("Axis{0}_XB_PositionMoveCmdAck", axisStr), iPos - 1);

                    motors[jPos].XB_PositionComplete[iPos - 1]       /**/= AddressMgr.GetAddress(string.Format("Axis{0}_XB_PositionMoveComplete", axisStr), iPos - 1);


                    motors[jPos].YF_Position1stPoint[iPos - 1]          /**/= AddressMgr.GetAddress(string.Format("Axis{0}_YF_Position1st{1}Point", axisStr, iPos.ToString("D2")));
                    motors[jPos].XF_Position1stPointAck[iPos - 1]       /**/= AddressMgr.GetAddress(string.Format("Axis{0}_XF_Position1st{1}PointAck", axisStr, iPos.ToString("D2")));

                    motors[jPos].YF_Position1stSpeed[iPos - 1]          /**/= AddressMgr.GetAddress(string.Format("Axis{0}_YF_Position1st{1}Speed", axisStr, iPos.ToString("D2")));
                    motors[jPos].XF_Position1stSpeedAck[iPos - 1]       /**/= AddressMgr.GetAddress(string.Format("Axis{0}_XF_Position1st{1}SpeedAck", axisStr, iPos.ToString("D2")));

                    motors[jPos].YF_Position1stAccel[iPos - 1]          /**/= AddressMgr.GetAddress(string.Format("Axis{0}_YF_Position1st{1}Accel", axisStr, iPos.ToString("D2")));
                    motors[jPos].XF_Position1stAccelAck[iPos - 1]       /**/= AddressMgr.GetAddress(string.Format("Axis{0}_XF_Position1st{1}AccelAck", axisStr, iPos.ToString("D2")));                     

                }
            }

        }

        private int SettingStepNo = 0;
        public void SettingStep()
        {
            if (SettingStepNo == 0)
            {
                if (YB_PmacValueSave.vBit == true)
                {
                    XB_PmacValueSaveAck.vBit = true;

                    SettingStepNo = 10;
                }
            }
            else if (SettingStepNo == 10)
            {
                if (YB_PmacValueSave.vBit == false)
                {

                    for (int jPos = 0; jPos < motors.Count; jPos++)
                    {

                        for (int iPos = 1; iPos <= motors[jPos].PositionCount; iPos++)
                        {
                            motors[jPos].XF_Position1stPointAck[iPos - 1].vFloat = motors[jPos].YF_Position1stPoint[iPos - 1].vFloat;
                            motors[jPos].XF_Position1stSpeedAck[iPos - 1].vFloat = motors[jPos].YF_Position1stSpeed[iPos - 1].vFloat;
                            motors[jPos].XF_Position1stAccelAck[iPos - 1].vFloat = motors[jPos].YF_Position1stAccel[iPos - 1].vFloat;                             

                        }
                    }

                    XB_PmacValueSaveAck.vBit = false;
                    SettingStepNo = 20;
                }
            }
            else if (SettingStepNo == 20)
            {

                SettingStepNo = 30;
            }
            else if (SettingStepNo == 30)
            {
                SettingStepNo = 0;

            }
        }


        private int InterlockCmdStepNo = 0;
        private int AcsResetStepNo = 0;
        public void CommCmdLogic()
        {
            //Interlock 처리 
            if (InterlockCmdStepNo == 0)
            {
                if (YB_PinUpMotorInterlockOffCmd.vBit == true)
                {
                    XB_PinUpMotorInterlockOffCmdAck.vBit = true;
                    XB_PinUpInterlockOff.vBit = true;
                    InterlockCmdStepNo = 10;
                }
            }
            else if (InterlockCmdStepNo == 10)
            {
                if (YB_PinUpMotorInterlockOffCmd.vBit == false)
                {
                    XB_PinUpMotorInterlockOffCmdAck.vBit = false;
                    InterlockCmdStepNo = 20;
                }
            }
            else if (InterlockCmdStepNo == 20)
            {
                InterlockCmdStepNo = 0;
            }

            //Acs Reset Step No 처리 
            if (AcsResetStepNo == 0)
            {
                if (YB_PmacResetCmd.vBit == true)
                {
                    XB_PmacResetCmdAck.vBit = true;
                    AcsResetStepNo = 10;
                }
            }
            else if (AcsResetStepNo == 10)
            {
                if (YB_PmacResetCmd.vBit == false)
                {
                    XB_PmacResetCmdAck.vBit = false;
                    AcsResetStepNo = 20;
                }
            }
            else if (InterlockCmdStepNo == 20)
            {
                //ACS RESET 처리. 요함. 

                AcsResetStepNo = 30;
            }
            else if (InterlockCmdStepNo == 30)
            {
                AcsResetStepNo = 0;
            }
        }

        public void MoveInterlockLogic()
        {
            if (XB_PinUpInterlockOff.vBit == false)
            {
                //인터락 조직 추가. 
            }
        }


        public int ReviewStepNo = 0;
        public int ReviewCount = 0;
        public int ReviewCurrPosi = 0;
        public void ReviewStepLogic()
        {
            if (ReviewStepNo == 0)
            {
                if (YB_ReviewStartCmd.vBit == true)
                {
                    XB_ReviewStartCmdAck.vBit = true;
                    ReviewStepNo = 10;
                }
            }
            else if (ReviewStepNo == 10)
            {
                if (YB_ReviewStartCmd.vBit == true)
                {
                    XB_ReviewStartCmdAck.vBit = false;

                    ReviewCount = YI_ReviewPositionCount.vInt;
                    ReviewCurrPosi = 0;

                    ReviewStepNo = 20;
                }
            }
            else if (ReviewStepNo == 20)
            {
                if (ReviewCurrPosi < ReviewCount)
                {

                }
                else
                {
                    ReviewStepNo = 100;
                }
            }
            else if (ReviewStepNo == 100)
            {
                XB_ReviewCompleteCmd.vBit = true;
                ReviewStepNo = 110;
            }
            else if (ReviewStepNo == 110)
            {
                if (YB_ReviewCompleteCmdAck.vBit == true)
                {
                    XB_ReviewCompleteCmd.vBit = false;
                    ReviewStepNo = 0;
                }
            }

        }
    }
}
