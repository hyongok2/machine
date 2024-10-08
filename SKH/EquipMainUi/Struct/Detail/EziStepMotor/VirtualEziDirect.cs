using Dit.Framework.Comm;
using Dit.Framework.PLC;
using EquipMainUi.Struct.Detail.EziStepMotor.Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Detail.EziStepMotor
{
    public class EziSvoMotorProxy
    {
        private int _homeInpos = 10;
        private int _ptpInpos = 10;

        public VirtualEziDirect Ezi { get; set; }
        public string Name { get; set; }
        public int SlaveNo { get; set; }

        /// <summary>
        /// 컨트롤러->ServoMotorControl 곱함, ServoMotorControl->컨트롤러 나눔
        /// </summary>
        public float PositionScale = 1;
        public double JogSpeed { get; set; }
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

        public EziSvoMotorProxy(EmEziStep slaveNo)
        {
            if (slaveNo == EmEziStep.AlignerT)
                PositionScale = 100000 / 360;
            else if(slaveNo == EmEziStep.AlignerX || slaveNo == EmEziStep.AlignerY)
                PositionScale = 10000/4;

            YB_PTPMoveCmd = null;
            XB_PTPMoveCmdAck = null;


            YF_PTPMoveSpeed = null;
            XF_PTPMoveSpeedAck = null;

            SlaveNo = (int)slaveNo;
        }

        public void LogicWorking()
        {
            if (Ezi.GetStatus(SlaveNo, out _dwInStatus, out _dwOutStatus, out _dwAxisStatus,
                out _cmdPos, out _curPos, out _posErr, out _curSpeed, out _wPosItemNo))
            {
                StatusLogic();
                JogStepLogic();
                HomeStepLogic();
                PTPMoveLogic();
            }
            else
            {
                XB_StatusMotorServoOn.vBit = false;
            }
            MotorStopLogic();
        }

        private uint _dwInStatus;
        private uint _dwOutStatus;
        private uint _dwAxisStatus;
        private int _cmdPos;
        private int _curPos;
        private int _posErr;
        private int _curSpeed;
        private ushort _wPosItemNo;

        private void StatusLogic()
        {
            XF_CurrMotorSpeed.vFloat = _curSpeed / PositionScale;
            XF_CurrMotorPosition.vFloat = _cmdPos / PositionScale; //cmd pos(Theta축일때), curpos(x, y 입고 후 확인필요) 
            
            //XF_CurrMotorActualLoad        .vFloat   = XF_CurrMotorActualLoad                 

            XB_StatusMotorServoOn.vBit = Ezi.IsServoOn(_dwAxisStatus);
            XB_StatusMotorMoving.vBit = Ezi.IsMoving(_dwAxisStatus);

            XB_StatusMinusLimitSet.vBit = Ezi.IsNegLimit(_dwAxisStatus);
            XB_StatusPlusLimitSet.vBit = Ezi.IsPosLimit(_dwAxisStatus);

            XB_StatusHomeInPosition.vBit = XB_StatusMotorServoOn.vBit
                && XB_StatusHomeCompleteBit.vBit
                && Math.Abs(_curPos - 0) < _homeInpos
                ;
            XB_StatusMotorInPosition.vBit = XB_StatusMotorServoOn.vBit
                && Math.Abs(XF_CurrMotorPosition.vFloat - XF_PTPMovePositionAck.vFloat) < _ptpInpos;
        }

        private int _motorStopTimeoverStd = 10000;
        private Stopwatch _motorStopTimeoverCheck = new Stopwatch();
        public int SetpMotorStop = 0;
        public void MotorStopLogic()
        {
            if (_motorStopTimeoverCheck.ElapsedMilliseconds > _motorStopTimeoverStd)
            {
                _motorStopTimeoverCheck.Reset();
                SetpMotorStop = 0;
            }

            if (SetpMotorStop == 0)
            {
                if (YB_MotorStopCmd.vBit)
                    SetpMotorStop = 10;
            }
            else if (SetpMotorStop == 10)
            {
                HomeStepNo = 0;
                PTPMoveStepNo = 0;
                JogPlusStep = 0;
                JogMinusStep = 0;

                XB_MotorStopCmdAck.vBit = true;

                //bool servoOnOff = YB_ServoOnOff.vBit;
                //if (servoOnOff)
                //{
                Ezi.EmergencyStop(this.SlaveNo);
                //}
                //else
                //{
                //    Ezi.MotorKill(AxisNo);
                //}
                SetpMotorStop = 20;
            }
            else if (SetpMotorStop == 20)
            {
                if (YB_MotorStopCmd.vBit == false)
                {
                    XB_MotorStopCmdAck.vBit = false;
                    SetpMotorStop = 0;
                }
            }
        }

        public void StepMoveLogic()
        {
        }

        private Stopwatch _servoOnOffTimeoverCheck = new Stopwatch();
        public int SetpServoOnOff = 0;

        private int _homeTimeoverStd = 120000;
        private Stopwatch _homeTimeoverCheck = new Stopwatch();
        private int HomeStepNo = 0;
        private PlcTimer _plcTmrHome = new PlcTimer("Home Seq Timer");
        private PlcTimer _stopWait = new PlcTimer("Home-Stop Wait");
        private int _stopWaitTime = 2000;

        public void HomeStepLogic()
        {
            if (_homeTimeoverCheck.ElapsedMilliseconds > _homeTimeoverStd)
            {
                _homeTimeoverCheck.Reset();
                HomeStepNo = 0;
            }

            if (false
                //|| HomeStepNo != 0
                || PTPMoveStepNo != 0
                || JogPlusStep != 0
                || JogMinusStep != 0
                )
            {
                return;
            }

            if (HomeStepNo == 0)
            {
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
                if (Ezi.JogStop(this.SlaveNo) == false)
                {
                    HomeStepNo = 0;
                }
                else
                    HomeStepNo = 25;
            }

            else if (HomeStepNo == 25)
            {
                if (this.XB_StatusMotorServoOn.vBit == true)
                    HomeStepNo = 40;
                else if (Ezi.MotorKill(this.SlaveNo) == false)
                {
                    HomeStepNo = 0;
                }
                else
                    HomeStepNo = 30;
            }
            else if (HomeStepNo == 30)
            {
                if (Ezi.IsError(_dwAxisStatus) == true)
                {
                    if (Ezi.ErrorReset(this.SlaveNo) == false)
                    {
                        HomeStepNo = 0;
                    }
                    else
                        HomeStepNo = 35;
                }
                else
                {
                    HomeStepNo = 35;
                }
            }
            else if (HomeStepNo == 35)
            {
                if (Ezi.MotorOn(this.SlaveNo) == false)
                {
                    HomeStepNo = 0;
                }
                else
                    HomeStepNo = 37;
            }
            else if (HomeStepNo == 37)
            {
                if (Ezi.IsMoving(_dwAxisStatus) == true)
                    _stopWait.Start(0, _stopWaitTime);
                else
                    _stopWait.Start(0, 100);

                HomeStepNo = 38;
            }
            else if (HomeStepNo == 38)
            {
                if (_stopWait)
                {
                    _stopWait.Stop();
                    HomeStepNo = 40;
                }
            }
            else if (HomeStepNo == 40)
            {
                //if (Ezi.MotorHommingSetting(this.SlaveNo, 5000, 1000, 50) == false)
                //{
                //    EziLogger.Log(string.Format("fail {0} home setting", this.SlaveNo));
                //    HomeStepNo = 0;
                //}
                //else
                {
                    _plcTmrHome.Start(0, 1000);
                    HomeStepNo = 50;
                }
            }
            else if (HomeStepNo == 50)
            {
                if (_plcTmrHome)
                {
                    _plcTmrHome.Stop();
                    if (Ezi.MotorHomming(this.SlaveNo) == false)
                    {
                        HomeStepNo = 0;
                    }
                    else
                        HomeStepNo = 60;
                }
            }
            else if (HomeStepNo == 60)
            {
                HomeStepNo = 70;
            }
            else if (HomeStepNo == 70)
            {
                if (Ezi.IsMotorHommingCompleted(_dwAxisStatus) == true)
                    HomeStepNo = 80;
            }
            else if (HomeStepNo == 80)
            {
                //jys:: pos clear 필요하면 할 것.
                //                 if (Ezi.PositionClear(this.SlaveNo) == false)
                //                 {
                //                     EziLogger.Log(string.Format("fail {0} home position clear", this.SlaveNo));
                //                     HomeStepNo = 0;
                //                 }
                //                 else
                {
                    XB_StatusHomeCompleteBit.vBit = true;
                    HomeStepNo = 0;
                }
            }
        }

        private int _ptpTimeoverStd = 120000;
        private Stopwatch _ptpTimeoverCheck = new Stopwatch();
        private int PTPMoveStepNo = 0;
        public void PTPMoveLogic()
        {
            if (_ptpTimeoverCheck.ElapsedMilliseconds > _ptpTimeoverStd)
            {
                _ptpTimeoverCheck.Reset();
                PTPMoveStepNo = 0;
            }

            if (false
                || HomeStepNo != 0
                //|| PTPMoveStepNo != 0
                || JogPlusStep != 0
                || JogMinusStep != 0
                )
            {
                return;
            }

            if (PTPMoveStepNo == 0)
            {
                if (YB_PTPMoveCmd.vBit == true)
                {
                    XF_PTPMovePositionAck.vFloat = YF_PTPMovePosition.vFloat;
                    XF_PTPMoveSpeedAck.vFloat = YF_PTPMoveSpeed.vFloat;
                    XF_PTPMoveAccelAck.vFloat = YF_PTPMoveAccel.vFloat;

                    _ptpTimeoverStd = (int)(Math.Abs(YF_PTPMovePosition.vFloat - XF_CurrMotorPosition.vFloat) / YF_PTPMoveSpeed.vFloat);
                    _ptpTimeoverStd += 10000;

                    XB_PTPMoveCmdAck.vBit = true;
                    PTPMoveStepNo = 10;
                }
            }
            else if (PTPMoveStepNo == 10)
            {
                if (YB_PTPMoveCmd.vBit == false)
                {
                    XB_PTPMoveCmdAck.vBit = false;
                    PTPMoveStepNo = 20;
                }
            }
            else if (PTPMoveStepNo == 20)
            {
                Ezi.PTPMoveSet(this.SlaveNo);
                PTPMoveStepNo = 25;
            }
            else if (PTPMoveStepNo == 25)
            {
                if (XF_PTPMoveSpeedAck.vFloat < 0)
                {
                    PTPMoveStepNo = 0;
                }
                else
                {
                    
                    int posiAfterScale = (int)(XF_PTPMovePositionAck.vFloat * PositionScale);
                    uint spdAfterScale = (uint)(XF_PTPMoveSpeedAck.vFloat * PositionScale);
                    Logger.Log.AppendLine(LogLevel.NoLog, string.Format("[EziStepMotor PTP Start]scale 후 Position {0}, Speed {1}, Acc : Default", posiAfterScale, spdAfterScale));

                    Ezi.PTPMoveSpeed(this.SlaveNo, posiAfterScale, spdAfterScale);
                    PTPMoveStepNo = 30;
                }
            }
            else if (PTPMoveStepNo == 30)
            {
                if (XB_StatusMotorInPosition.vBit == true)
                {
                    PTPMoveStepNo = 40;
                }
            }
            else if (PTPMoveStepNo == 40)
            {
                if (/*XB_PTPMoveComplete.vBit == true && */XB_StatusMotorInPosition.vBit == true)
                {
                    PTPMoveStepNo = 50;
                }
            }
            else if (PTPMoveStepNo == 50)
            {
                PTPMoveStepNo = 0;
            }
        }

        private int JogPlusStep = 0;
        private int JogMinusStep = 0;
        public void JogStepLogic()
        {
            JogSpeed = YF_MotorJogSpeedCmd.vFloat * PositionScale;
            XF_MotorJogSpeedCmdAck.vFloat = YF_MotorJogSpeedCmd.vFloat;

            if (false
                || HomeStepNo != 0
                || PTPMoveStepNo != 0
                //|| JogPlusStep != 0
                //|| JogMinusStep != 0
                )
            {
                return;
            }

            if (JogPlusStep == 0)
            {
                if (YB_MotorJogPlusMove.vBit == true)
                {
                    Ezi.JogMove(this.SlaveNo, JogSpeed, 1);
                    JogPlusStep = 10;
                }
            }
            else if (JogPlusStep == 10)
            {
                if (YB_MotorJogPlusMove.vBit == false)
                {
                    Ezi.JogStop(this.SlaveNo);
                    JogPlusStep = 0;
                }
            }

            if (JogMinusStep == 0)
            {
                if (YB_MotorJogMinusMove.vBit == true)
                {
                    Ezi.JogMove(this.SlaveNo, JogSpeed, -1);
                    JogMinusStep = 10;
                }
            }
            else if (JogMinusStep == 10)
            {
                if (YB_MotorJogMinusMove.vBit == false)
                {
                    Ezi.JogStop(this.SlaveNo);
                    JogMinusStep = 0;
                }
            }
        }

    }
    public class VirtualEziDirect : IVirtualMem
    {

        private const int MEM_SIZE = 102400;
        public byte[] BYTE_EM = new byte[MEM_SIZE];
        public int[] BYTE_ES = new int[MEM_SIZE];

        private byte PortNo = 0;
        private string Baudarate = string.Empty;

        private bool _isRunning = false;

        private Thread _eziWorker = null;


        public List<PlcAddr> LstReadAddr { get; set; }
        public List<PlcAddr> LstWriteAddr { get; set; }

        public double CurrWorkingTime { get; set; }
        public double MaxWorkingTime { get; set; }

        private string Name { get; set; }

        public VirtualEziDirect(string name, string comportNo, string baudrate = "57600")
        {
            Name = name;

            LstReadAddr = new List<PlcAddr>();
            LstWriteAddr = new List<PlcAddr>();

            int temp = int.Parse(comportNo.Remove(0, 3));
            PortNo = (byte)temp;
            Baudarate = baudrate;

            _eziWorker = new Thread(new ThreadStart(EziWorker_DoWork));
            _eziWorker.Priority = ThreadPriority.Highest;
        }

        private bool _aliveSignal = false;
        private int _aliveStd = 1000;
        public bool AliveSignal
        {
            get
            {
                lock (this)
                {
                    return _aliveSignal;
                }
            }
        }
        private Stopwatch _aliveCheck = new Stopwatch();
        private void EziWorker_DoWork()
        {
            DateTime start, end;
            while (_isRunning)
            {
                start = DateTime.Now;

                LogicWorking();

                CurrWorkingTime = (DateTime.Now - start).TotalMilliseconds;
                if (MaxWorkingTime < CurrWorkingTime)
                    MaxWorkingTime = CurrWorkingTime;

                if (_aliveCheck.IsRunning == false)
                    _aliveCheck.Restart();

                if (_aliveCheck.ElapsedMilliseconds > _aliveStd)
                {
                    _aliveSignal = !_aliveSignal;
                    _aliveCheck.Restart();
                }
            }
        }

        //메소드 연결
        public override int Open()
        {
            try
            {
                if (EziProxy.Connect(PortNo) == false)
                    return FALSE;

                _isRunning = true;
                _eziWorker.Start();
                return TRUE;
            }
            catch (Exception ex)
            {
                return FALSE;
            }
        }
        public override int Close()
        {
            try
            {
                _isRunning = false;

                _eziWorker.Join();

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
            return 1;
        }
        public override int WriteToPLC(PlcAddr addr, int wordSize)
        {
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
            byte[] intbyte = new byte[4];
            Array.Copy(BYTE_EM, addr.Addr * 4, intbyte, 0, 4);

            //intbyte = Swap4Byte(intbyte);
            return BitConverter.ToInt32(intbyte, 0);
        }
        public override void VirSetInt32(PlcAddr addr, int value)
        {
            byte[] intbyte = BitConverter.GetBytes(value);
            //intbyte = Swap4Byte(intbyte);

            Array.Copy(intbyte, 0, BYTE_EM, addr.Addr * 4, 4);
        }
        public override float VirGetFloat(PlcAddr addr)
        {
            byte[] intbyte = new byte[4];
            Array.Copy(BYTE_EM, addr.Addr * 4, intbyte, 0, 4);

            return BitConverter.ToSingle(intbyte, 0);
        }
        public override void VirSetFloat(PlcAddr addr, float value)
        {
            byte[] intbyte = BitConverter.GetBytes(value);
            Array.Copy(intbyte, 0, BYTE_EM, addr.Addr * 4, 4);
        }


        public void LogicWorking()
        {
            foreach (PlcAddr addr in LstReadAddr)
                ReadFromPLC(addr, addr.Length);

            foreach (PlcAddr addr in LstWriteAddr)
                WriteToPLC(addr, addr.Length);

            Motors.ForEach(f => f.LogicWorking());
        }

        #region Motor Access
        //모터 제어 로직 추가. 
        public List<EziSvoMotorProxy> Motors = new List<EziSvoMotorProxy>();

        #region Motor Status           

        public bool GetStatus(int slaveNo, out uint dwInStatus, out uint dwOutStatus, out uint dwAxisStatus,
            out int lCmdPos, out int lActPos, out int lPosErr, out int lActVel, out ushort wPosItemNo)
        {
            dwInStatus = dwOutStatus = dwAxisStatus = 0;
            lCmdPos = lActPos = lPosErr = lActVel = wPosItemNo = 0;
            int ret = EziMOTIONPlusRLib.FAS_GetAllStatus(PortNo, (byte)slaveNo, ref dwInStatus, ref dwOutStatus, ref dwAxisStatus, ref lCmdPos, ref lActPos, ref lPosErr, ref lActVel, ref wPosItemNo);
            if (ret != EziMOTIONPlusRLib.FMM_OK)
            {
                return false;
            }
            return true;
        }
        //public bool IsInpos(uint status)
        //{
        //    return (status & EziMOTIONPlusRLib.FFLAG_INPOSITION) != 0;
        //}
        public bool IsError(uint status)
        {
            return (status & EziMOTIONPlusRLib.FFLAG_ERRORALL) != 0;
        }
        PlcTimerEx ServoOnTimer = new PlcTimerEx("ServoOnTimer", false);
        public bool IsServoOn(uint status)
        {
            //return (status & EziMOTIONPlusRLib.FFLAG_SERVOON) != 0; ServoOn Status 없음
            if((status & EziMOTIONPlusRLib.FFLAG_ORIGINRETOK) != 0 && !IsError(status) == false)
            {
                if(ServoOnTimer)
                {
                    ServoOnTimer.Stop();
                    return false;
                }
                else
                {
                    ServoOnTimer.Start(50);
                    return true;
                }
            }
            else
            {
                ServoOnTimer.Stop();
                return true;
            }
        }
        public bool IsMoving(uint status)
        {
            return (status & EziMOTIONPlusRLib.FFLAG_MOTIONING) != 0;
        }
        public bool IsPosLimit(uint status)
        {
            return (status & EziMOTIONPlusRLib.FFLAG_HWPOSILMT) != 0;
        }
        public bool IsNegLimit(uint status)
        {
            return (status & EziMOTIONPlusRLib.FFLAG_HWNEGALMT) != 0;
        }
        #endregion


        private int InterlockCmdStepNo = 0;
        private int AcsResetStepNo = 0;

        public void MoveInterlockLogic()
        {
            //if (XB_PinUpInterlockOff.vBit == false)
            //{
            //    //인터락 조직 추가. 
            //}
        }
        public bool MotorOn(int slaveNo)
        {
            int ret = EziMOTIONPlusRLib.FAS_ServoEnable(PortNo, (byte)slaveNo, 1);
            if (ret != EziMOTIONPlusRLib.FMM_OK)
            {
                return false;
            }
            return true;
        }
        public bool MotorKill(int slaveNo)
        {
            int ret = EziMOTIONPlusRLib.FAS_ServoEnable(PortNo, (byte)slaveNo, 0);
            if (ret != EziMOTIONPlusRLib.FMM_OK)
            {
                return false;
            }
            return true;
        }
        public bool ErrorReset(int slaveNo)
        {
            int ret = EziMOTIONPlusRLib.FAS_ServoAlarmReset(PortNo, (byte)slaveNo);
            if (ret != EziMOTIONPlusRLib.FMM_OK)
            {
                return false;
            }
            return true;
        }
        public bool MotorHomming(int slaveNo)
        {
            int ret = EziMOTIONPlusRLib.FAS_MoveOriginSingleAxis(PortNo, (byte)slaveNo);
            if (ret != EziMOTIONPlusRLib.FMM_OK)
            {
                return false;
            }
            return true;
        }
        public bool PositionClear(int slaveNo)
        {
            int ret = EziMOTIONPlusRLib.FAS_ClearPosition(PortNo, (byte)slaveNo);
            if (ret != EziMOTIONPlusRLib.FMM_OK)
            {
                return false;
            }
            return true;
        }
        public bool IsMotorHommingCompleted(uint status)
        {
            return (status & EziMOTIONPlusRLib.FFLAG_ORIGINRETURNING/*원점 복귀 운전 중?*/) == 0
                && (status & EziMOTIONPlusRLib.FFLAG_ORIGINRETOK/*원점 복귀 운전 완료?*/) != 0;
        }
        public bool JogSpeed(int slaveNo, double speed)
        {
            if (speed < 0)
                speed = 1;
            EziSvoMotorProxy motor = Motors.FirstOrDefault(m => m.SlaveNo == slaveNo);
            if (motor == null)
                return false;
            motor.JogSpeed = speed;
            return true;
        }
        public bool EmergencyStop(int slaveNo)
        {
            int ret = EziMOTIONPlusRLib.FAS_EmergencyStop(PortNo, (byte)slaveNo);
            if (ret != EziMOTIONPlusRLib.FMM_OK)
            {
                return false;
            }
            return true;
        }
        public bool JogStop(int slaveNo)
        {
            int ret = EziMOTIONPlusRLib.FAS_MoveStop(PortNo, (byte)slaveNo);
            if (ret != EziMOTIONPlusRLib.FMM_OK)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="array">direction 1 = pos, 0 = neg</param>
        /// <returns></returns>
        public bool JogMove(int slaveNo, double speed, int array)
        {
            uint jogSpd = 0;
            EziSvoMotorProxy motor = Motors.FirstOrDefault(m => m.SlaveNo == slaveNo);

            if (motor == null
                || motor.JogSpeed < 0)
                return false;

            jogSpd = (uint)speed;

            //             EziMOTIONPlusRLib.VELOCITY_OPTION_EX ex = new EziMOTIONPlusRLib.VELOCITY_OPTION_EX();
            //             ex.BIT_USE_CUSTOMACCDEC = true;
            //             ex.wCustomAccDecTime = 50;
            // 
            //             int ret = EziMOTIONPlusRLib.FAS_MoveVelocityEx(SlaveNo, jogSpd, array == 1 ? EziMOTIONPlusRLib.DIR_INC : EziMOTIONPlusRLib.DIR_DEC, ex);
            int ret = EziMOTIONPlusRLib.FAS_MoveVelocity(PortNo, (byte)slaveNo, jogSpd, array == 1 ? EziMOTIONPlusRLib.DIR_INC : EziMOTIONPlusRLib.DIR_DEC);
            if (ret != EziMOTIONPlusRLib.FMM_OK)
            {
                return false;
            }
            return true;
        }

        public bool PTPMove(int SlaveNo, double position)
        {
            //             string cmd = string.Format("#{0}J={1}", axis, position);
            //             string cmdResult = string.Empty;
            //             return UMacProxy.DeviceCommand(_dwDevice, cmd, out cmdResult);
            return false;
        }
        public bool PTPMoveSet(int SlaveNo)
        {
            return true;
        }

        public bool PTPMoveSpeed(int slaveNo, int position, uint speed, int acc)
        {
            EziMOTIONPlusRLib.MOTION_OPTION_EX ex = new EziMOTIONPlusRLib.MOTION_OPTION_EX();
            ex.BIT_USE_CUSTOMACCEL = true;
            ex.BIT_USE_CUSTOMDECEL = true;
            ex.wCustomAccelTime = (ushort)acc;
            ex.wCustomDecelTime = (ushort)acc;
            int ret = EziMOTIONPlusRLib.FAS_MoveSingleAxisAbsPosEx(PortNo, (byte)slaveNo, position, speed, ex);
            if (ret != EziMOTIONPlusRLib.FMM_OK)
            {
                return false;
            }
            return true;
        }
        public bool PTPMoveSpeed(int slaveNo, int position, uint speed)
        {
            int ret = EziMOTIONPlusRLib.FAS_MoveSingleAxisAbsPos(PortNo, (byte)slaveNo, position, speed);
            if (ret != EziMOTIONPlusRLib.FMM_OK)
            {
                return false;
            }
            return true;
        }
        #endregion

    }
}
