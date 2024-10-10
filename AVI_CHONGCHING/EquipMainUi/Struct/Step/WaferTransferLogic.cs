using EquipMainUi.Struct.Detail.EFEM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Step
{
    public enum EmWaferRecoveryReadyLogic
    {
        S000_WAIT,
        S010_WAFER_RETURN_READY_START,
        S020_WAEFR_RETURN_READY_COMPLETE_WAIT,
    }

    public enum EmWaferTransferSeqStep
    {
        S000_END,

        S005_INITIAL_START,
        S007_ROBOT_INITIAL_WAIT,
        S010_ALL_INITIAL_WAIT,
        S020_LPM_OPEN_WAIT,

        S030_START,
        S040_PICK_START,
        S050_PICK_END_WAIT,
        S060_DELAY,
        S070_PLACE_START,
        S080_PLACE_END_WAIT,
        S090_END,
    }
    public class WaferTransferLogic : StepBase
    {
        private EmWaferRecoveryReadyLogic _readyStepNum = 0;

        private EmWaferTransferSeqStep _seqStepNum = 0;
        private EmWaferTransferSeqStep _seqStepNumOld = 0;
        public EmWaferTransferSeqStep SeqStepNum { get { return _seqStepNum; } }

        private EFEMTRANSDataSet PickCmd;
        private EFEMTRANSDataSet PlaceCmd;

        public bool IsPickRunning { get { return IsRunning() && EmWaferTransferSeqStep.S030_START <= _seqStepNum && _seqStepNum < EmWaferTransferSeqStep.S060_DELAY; } }
        public bool IsPlaceRunning { get { return IsRunning() && EmWaferTransferSeqStep.S060_DELAY <= _seqStepNum; } }

        public WaferTransferLogic()
        {
            SetName("WAFER TRANS");
        }

        public override void LogicWorking(Equipment equip)
        {
            //RecoveryReadyLogic(equip);
            SeqLogicWorking(equip);
        }
        public void RecoveryReadyLogic(Equipment equip)
        {
            switch (_readyStepNum)
            {
                case EmWaferRecoveryReadyLogic.S000_WAIT:
                    break;
                case EmWaferRecoveryReadyLogic.S010_WAFER_RETURN_READY_START:
                    if (PlaceCmd == null)
                    {
                        InterLockMgr.AddInterLock("<Wafer Recovery Error>", GG.boChinaLanguage ? "Data有问题 ." : "Data is abnormal");
                        GG.Equip.IsInterlock = true;
                        return;
                    }


                    if (PlaceCmd.TargetPort == EmEfemPort.LOADPORT1)
                    {
                        GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.LOADPORT1, EmEfemCommand.OPEN_);
                    }
                    else if (PlaceCmd.TargetPort == EmEfemPort.LOADPORT2)
                    {
                        GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.LOADPORT2, EmEfemCommand.OPEN_);
                    }

                    if (GG.Equip.IsWaferDetect != EmGlassDetect.NOT)
                    {
                        GG.Equip.InitialLogic.StartInitial(EmEfemPort.EQUIPMENT);
                    }
                    _readyStepNum = EmWaferRecoveryReadyLogic.S020_WAEFR_RETURN_READY_COMPLETE_WAIT;
                    break;
                case EmWaferRecoveryReadyLogic.S020_WAEFR_RETURN_READY_COMPLETE_WAIT:
                    if (PlaceCmd == null)
                    {
                        InterLockMgr.AddInterLock("<Wafer Recovery Error>", GG.boChinaLanguage ? "Data有问题 ." : "Data is abnormal");
                        GG.Equip.IsInterlock = true;
                        return;
                    }

                    if (GG.Equip.IsReadyToInputArm.IsSolOnOff)
                    {
                        if (PlaceCmd.TargetPort == EmEfemPort.LOADPORT1 && GG.Equip.Efem.LoadPort1.Status.IsDoorOpen)
                        {
                            _readyStepNum = EmWaferRecoveryReadyLogic.S000_WAIT;
                        }
                        else if (PlaceCmd.TargetPort == EmEfemPort.LOADPORT2 && GG.Equip.Efem.LoadPort2.Status.IsDoorOpen)
                        {
                            _readyStepNum = EmWaferRecoveryReadyLogic.S000_WAIT;
                        }
                    }
                    break;
            }
        }
        public override void SeqLogicWorking(Equipment equip)
        {
            SeqStepStr = _seqStepNum.ToString();

            if (_seqStepNum != _seqStepNumOld)
                _seqStepNumOld = _seqStepNum;

            base.LogicWorking(equip);

            if (GG.Equip.EquipRunMode == EmEquipRunMode.Auto)
                SetRunMode(EmEfemRunMode.Stop);

            if (queueLogic.Count > 0 && this.RunMode != EmEfemRunMode.Start)
            {
                WaferLogicParm param = queueLogic.Dequeue();
                Logger.Log.AppendLine(LogLevel.Info, string.Format("웨이퍼 복귀 시작 : {0} -> {1}의 {2}번 째 슬롯", param.FromPort, param.ToPort, param.ToSlot));
                StartWaferTransfer(param.FromPort, param.FromSlot, param.ToPort, param.ToSlot, true);
            }
            else if (this.RunMode == EmEfemRunMode.Stop
                || this.RunMode == EmEfemRunMode.Home
                || this.RunMode == EmEfemRunMode.Pause || GG.Equip.IsInterlock || GG.Equip.IsPause || GG.Equip.IsHeavyAlarm
                )
            {
                Stop();

                _seqStepNum = EmWaferTransferSeqStep.S000_END;
            }
            else if (this.RunMode == EmEfemRunMode.Start)
            {
                TransferLogic();
            }

        }

        Queue<WaferLogicParm> queueLogic = new Queue<WaferLogicParm>();
        private PlcTimerEx _cmdDelay = new PlcTimerEx("", false);
        private void TransferLogic()
        {
            switch (_seqStepNum)
            {
                case EmWaferTransferSeqStep.S000_END:

                    break;
                case EmWaferTransferSeqStep.S005_INITIAL_START:
                    bool check = false;
                    string where = string.Empty;
                    if (GG.Equip.Efem.Proxy.IsRunning(GG.Equip, EmEfemPort.ROBOT, EmEfemCommand.INIT_))
                    {
                        where += "ROBOT\n";
                        check = true;
                    }
                    if (GG.Equip.Efem.Proxy.IsRunning(GG.Equip, EmEfemPort.LOADPORT1, EmEfemCommand.INIT_))
                    {
                        where += "LOADPORT1\n";
                        check = true;
                    }
                    if (GG.Equip.Efem.Proxy.IsRunning(GG.Equip, EmEfemPort.LOADPORT2, EmEfemCommand.INIT_))
                    {
                        where += "LOADPORT2\n";
                        check = true;
                    }
                    if (GG.Equip.Efem.Proxy.IsRunning(GG.Equip, EmEfemPort.ALIGNER, EmEfemCommand.INIT_))
                    {
                        where += "PRE ALIGNER\n";
                        check = true;
                    }
                    if (check)
                    {
                        InterLockMgr.AddInterLock(GG.boChinaLanguage ? "{0} 已经 INITIAL 中. 稍后再试." : "{0} 이미 INITIAL 중입니다 잠시 후 시도해주세요", where);
                        _seqStepNum = EmWaferTransferSeqStep.S000_END;
                        break;
                    }

                    GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.ROBOT, EmEfemCommand.INIT_);
                    _seqStepNum = EmWaferTransferSeqStep.S007_ROBOT_INITIAL_WAIT;
                    break;
                case EmWaferTransferSeqStep.S007_ROBOT_INITIAL_WAIT:
                    if (GG.Equip.Efem.Robot.IsInitDone)
                    {
                        GG.Equip.InitialLogic.StartInitial(EmEfemPort.EQUIPMENT);
                        GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.LOADPORT1, EmEfemCommand.INIT_);
                        GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.LOADPORT2, EmEfemCommand.INIT_);
                        GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.ALIGNER, EmEfemCommand.INIT_);

                        _seqStepNum = EmWaferTransferSeqStep.S010_ALL_INITIAL_WAIT;
                    }
                    break;
                case EmWaferTransferSeqStep.S010_ALL_INITIAL_WAIT:
                    if (GG.Equip.IsReadyToInputArm.IsSolOnOff && (GG.TestMode || (GG.Equip.Efem.Robot.IsInitDone
                        && GG.Equip.Efem.LoadPort1.IsInitDone && GG.Equip.Efem.LoadPort2.IsInitDone &&
                        GG.Equip.Efem.Proxy.IsComplete(GG.Equip, EmEfemPort.LOADPORT1, EmEfemCommand.INIT_) && GG.Equip.Efem.Proxy.IsComplete(GG.Equip, EmEfemPort.LOADPORT2, EmEfemCommand.INIT_)
                        && GG.Equip.Efem.Aligner.IsInitDone && GG.Equip.Efem.Proxy.IsComplete(GG.Equip, EmEfemPort.ALIGNER, EmEfemCommand.INIT_))))
                    {
                        if (PlaceCmd.TargetPort == EmEfemPort.LOADPORT1)
                        {
                            GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.LOADPORT1, EmEfemCommand.OPEN_);
                        }
                        else if (PlaceCmd.TargetPort == EmEfemPort.LOADPORT2)
                        {
                            GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.LOADPORT2, EmEfemCommand.OPEN_);
                        }
                        _seqStepNum = EmWaferTransferSeqStep.S020_LPM_OPEN_WAIT;
                    }
                    break;
                case EmWaferTransferSeqStep.S020_LPM_OPEN_WAIT:
                    if (PlaceCmd.TargetPort == EmEfemPort.LOADPORT1 && GG.Equip.Efem.Proxy.IsComplete(GG.Equip, EmEfemPort.LOADPORT1, EmEfemCommand.OPEN_))
                    {
                        _cmdDelay.Start(2);
                        GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.LOADPORT1, EmEfemCommand.MAPP_);
                        _seqStepNum = EmWaferTransferSeqStep.S030_START;
                    }
                    else if (PlaceCmd.TargetPort == EmEfemPort.LOADPORT2 && GG.Equip.Efem.Proxy.IsComplete(GG.Equip, EmEfemPort.LOADPORT2, EmEfemCommand.OPEN_))
                    {
                        _cmdDelay.Start(2);
                        GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.LOADPORT2, EmEfemCommand.MAPP_);
                        _seqStepNum = EmWaferTransferSeqStep.S030_START;
                    }
                    break;
                case EmWaferTransferSeqStep.S030_START:
                    if (_cmdDelay)
                    {
                        _cmdDelay.Stop();
                        Logger.RobotLog.AppendLine(LogLevel.NoLog, "WaferTransfer {0}→{1})", PickCmd == null ? "" : PickCmd.ToString(), PlaceCmd == null ? "" : PlaceCmd.ToString());
                        _seqStepNum = EmWaferTransferSeqStep.S040_PICK_START;
                    }
                    break;
                case EmWaferTransferSeqStep.S040_PICK_START:
                    if (PickCmd != null)
                    {
                        if (GG.Equip.Efem.Proxy.StartTransRobot(GG.Equip, PickCmd) == false)
                            _seqStepNum = EmWaferTransferSeqStep.S090_END;
                        _seqStepNum = EmWaferTransferSeqStep.S050_PICK_END_WAIT;
                    }
                    else
                        _seqStepNum = EmWaferTransferSeqStep.S070_PLACE_START;
                    break;
                case EmWaferTransferSeqStep.S050_PICK_END_WAIT:
                    if (GG.Equip.Efem.Proxy.IsComplete(GG.Equip, EmEfemPort.ROBOT, EmEfemCommand.TRANS) == true)
                    {
                        _cmdDelay.Start(0, 500);
                        _seqStepNum = EmWaferTransferSeqStep.S060_DELAY;
                    }
                    break;
                case EmWaferTransferSeqStep.S060_DELAY:
                    if (_cmdDelay)
                    {
                        _cmdDelay.Stop();
                        _seqStepNum = EmWaferTransferSeqStep.S070_PLACE_START;
                    }
                    break;
                case EmWaferTransferSeqStep.S070_PLACE_START:
                    if (PlaceCmd != null)
                    {
                        if (GG.Equip.Efem.Proxy.StartTransRobot(GG.Equip, PlaceCmd) == false)
                            _seqStepNum = EmWaferTransferSeqStep.S090_END;
                        _seqStepNum = EmWaferTransferSeqStep.S080_PLACE_END_WAIT;
                    }
                    else
                    {
                        _cmdDelay.Start(0, 100);
                        _seqStepNum = EmWaferTransferSeqStep.S090_END;
                    }
                    break;
                case EmWaferTransferSeqStep.S080_PLACE_END_WAIT:
                    if (GG.Equip.Efem.Proxy.IsComplete(GG.Equip, EmEfemPort.ROBOT, EmEfemCommand.TRANS) == true)
                    {
                        _cmdDelay.Start(0, 200);
                        _seqStepNum = EmWaferTransferSeqStep.S090_END;
                    }
                    break;
                case EmWaferTransferSeqStep.S090_END:
                    if (_cmdDelay)
                    {
                        _cmdDelay.Stop();
                        SetRunMode(EmEfemRunMode.Stop);
                        _seqStepNum = EmWaferTransferSeqStep.S000_END;
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="fromSlot">LoadPort 외 무관</param>
        /// <param name="to"></param>
        /// <param name="toSlot">LoadPort 외 무관</param>
        /// <returns></returns>
        public bool StartWaferTransfer(EmEfemDBPort from, int fromSlot, EmEfemDBPort to, int toSlot, bool IsShouldInitial = false)
        {
            if (IsRunning())
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Wafer(移送)已经进行中" : "웨이퍼 이송이 이미 진행 중입니다");
                return false;
            }

            if (from == EmEfemDBPort.NONE || to == EmEfemDBPort.NONE
                || from == to
                || fromSlot < 1 || fromSlot > 13
                || toSlot < 1 || toSlot > 13
                )
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Wafer 不可(移送)" : "Wafer 이송 불가", GG.boChinaLanguage ? "出发 Port, 到达 Port 或者 Slot 指定错误" : "출발포트, 도착포트 또는 Slot 지정이 잘못되었습니다");
                return false;
            }

            if (MakeCmd(from, fromSlot, to, toSlot) == false)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Wafer 不可(移送)" : "Wafer 이송 불가", "{0}({1}) → {2}({3})", from.ToString(), fromSlot, to.ToString(), toSlot);
                return false;
            }

            if (PickCmd != null ? GG.Equip.Efem.Proxy.IsOkStartTransRobotInterlock(GG.Equip, PickCmd) == false : false
                || PlaceCmd != null ? GG.Equip.Efem.Proxy.IsOkStartTransRobotInterlock(GG.Equip, PlaceCmd) == false : false)
                return false;

            SetRunMode(EmEfemRunMode.Start);

            if (IsShouldInitial == true)
            {
                _seqStepNum = EmWaferTransferSeqStep.S005_INITIAL_START;
            }
            else
            {
                _cmdDelay.Start(0, 1);
                _seqStepNum = EmWaferTransferSeqStep.S030_START;
            }

            return true;
        }

        private bool MakeCmd(EmEfemDBPort from, int fromSlot, EmEfemDBPort to, int toSlot)
        {
            EmEfemRobotArm arm = EmEfemRobotArm.Lower;
            EmEfemPort fromPort, toPort;
            PickCmd = null;
            PlaceCmd = null;
            if ((from == EmEfemDBPort.LOADPORT1 || from == EmEfemDBPort.LOADPORT2) == false)
                fromSlot = 1;
            if ((to == EmEfemDBPort.LOADPORT1 || to == EmEfemDBPort.LOADPORT2) == false)
                toSlot = 1;

            switch (from)
            {
                case EmEfemDBPort.ALIGNER:
                    switch (to)
                    {
                        case EmEfemDBPort.ALIGNER: return false;
                        case EmEfemDBPort.EQUIPMENT: arm = EmEfemRobotArm.Lower; break;
                        case EmEfemDBPort.LOADPORT1: arm = EmEfemRobotArm.Upper; break;
                        case EmEfemDBPort.LOADPORT2: arm = EmEfemRobotArm.Upper; break;
                        case EmEfemDBPort.UPPERROBOT: arm = EmEfemRobotArm.Upper; break;
                        case EmEfemDBPort.LOWERROBOT: arm = EmEfemRobotArm.Lower; break;
                    }
                    break;
                case EmEfemDBPort.EQUIPMENT:
                    switch (to)
                    {
                        case EmEfemDBPort.ALIGNER: return false;
                        case EmEfemDBPort.EQUIPMENT: return false;
                        case EmEfemDBPort.LOADPORT1: arm = EmEfemRobotArm.Upper; break;
                        case EmEfemDBPort.LOADPORT2: arm = EmEfemRobotArm.Upper; break;
                        case EmEfemDBPort.UPPERROBOT: arm = EmEfemRobotArm.Upper; break;
                        case EmEfemDBPort.LOWERROBOT: arm = EmEfemRobotArm.Lower; break;
                    }
                    break;
                case EmEfemDBPort.LOADPORT1:
                case EmEfemDBPort.LOADPORT2:
                    switch (to)
                    {
                        case EmEfemDBPort.ALIGNER: arm = EmEfemRobotArm.Lower; break;
                        case EmEfemDBPort.EQUIPMENT: arm = EmEfemRobotArm.Lower; break;
                        case EmEfemDBPort.LOADPORT1: return false;
                        case EmEfemDBPort.LOADPORT2: return false;
                        case EmEfemDBPort.UPPERROBOT: arm = EmEfemRobotArm.Upper; break;
                        case EmEfemDBPort.LOWERROBOT: arm = EmEfemRobotArm.Lower; break;
                    }
                    break;
                case EmEfemDBPort.UPPERROBOT: arm = EmEfemRobotArm.Upper; break;
                case EmEfemDBPort.LOWERROBOT: arm = EmEfemRobotArm.Lower; break;
            }

            fromPort = DBtoPort(from);
            toPort = DBtoPort(to);

            if (fromPort == EmEfemPort.NONE || toPort == EmEfemPort.NONE)
                return false;

            if (from == EmEfemDBPort.UPPERROBOT || from == EmEfemDBPort.LOWERROBOT)
                PickCmd = null;
            else
                PickCmd = new EFEMTRANSDataSet(arm, EmEfemTransfer.PICK, fromPort, fromSlot);

            if (to == EmEfemDBPort.UPPERROBOT || to == EmEfemDBPort.LOWERROBOT)
                PlaceCmd = null;
            else
                PlaceCmd = new EFEMTRANSDataSet(arm, EmEfemTransfer.PLACE, toPort, toSlot);

            if (PickCmd == null && PlaceCmd == null)
                return false;

            return true;
        }
        public EmEfemPort DBtoPort(EmEfemDBPort db)
        {
            switch (db)
            {
                case EmEfemDBPort.ALIGNER: return EmEfemPort.ALIGNER;
                case EmEfemDBPort.EQUIPMENT: return EmEfemPort.EQUIPMENT;
                case EmEfemDBPort.LOADPORT1: return EmEfemPort.LOADPORT1;
                case EmEfemDBPort.LOADPORT2: return EmEfemPort.LOADPORT2;
                case EmEfemDBPort.LOWERROBOT: return EmEfemPort.ROBOT;
                case EmEfemDBPort.UPPERROBOT: return EmEfemPort.ROBOT;
            }
            return EmEfemPort.NONE;
        }

        public bool IsRunning()
        {
            return RunMode == EmEfemRunMode.Start;
        }

        public void StartWaferTransfer(Queue<WaferLogicParm> _queueLogic)
        {
            queueLogic = new Queue<WaferLogicParm>(_queueLogic);
        }
        public class WaferLogicParm
        {
            public EmEfemDBPort FromPort;
            public int FromSlot = 1;
            public EmEfemDBPort ToPort;
            public int ToSlot;
        }

        public void Stop()
        {
            _seqStepNum = EmWaferTransferSeqStep.S000_END;

            base.Stop();
        }
    }
}
