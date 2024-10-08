using EquipMainUi.Struct.Detail;
using EquipMainUi.Struct.Detail.EFEM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Step
{
    public enum EmEFEMInitialLogicStep
    {
        S000_WAIT,
        S010_RESET_START,
        S020_RESET_COMPLETE_WAIT,
        S030_INITAIL_START,
        S040_INITAIL_COMPLETE_WAIT,
        S100_END,
    }
    public enum EmAVIInitialLogicStep
    {
        S000_WAIT,
        S010_HOME_START,
        S020_HOME_COMPLETE_WAIT,
        S030_LOADING_POSITION_MOVE_START,
        S040_LOADING_POSITION_MOVE_WAIT,
        S050_VACUUM_OFF_START,
        S060_VACUUM_OFF_WAIT,
        S070_LIFT_PIN_UP_START,
        S080_LIFT_PIN_UP_WAIT,
        S090_CENTERING_FORWARD_START,
        S100_CENTERING_FORWARD_WAIT,
        S110_CENTERING_BACKWARD_START,
        S120_CENTERING_BACKWARD_END,

        S130_END,
    }


    public class InitializeLogic : StepBase
    {
        private EmEFEMInitialLogicStep _seqEFEMStepNum = 0;
        private EmEFEMInitialLogicStep _seqEFEMStepNumOld = 0;
        public EmEFEMInitialLogicStep SeqEFEMStepNum { get { return _seqEFEMStepNum; } }

        private EmAVIInitialLogicStep _seqAVIStepNum = 0;
        private EmAVIInitialLogicStep _seqAVIStepNumOld = 0;
        public EmAVIInitialLogicStep SeqAVIStepNum { get { return _seqAVIStepNum; } }

        public InitializeLogic()
        {
            SetName("INITIAL STEP");
            TargetPort = EmEfemPort.NONE;//none
        }

        public override void LogicWorking(Equipment equip)
        {
            SeqLogicWorking(equip);
        }
        public override void SeqLogicWorking(Equipment equip)
        {
            //if (_seqEFEMStepNum != _seqEFEMStepNumOld)
            //    _seqEFEMStepNumOld = _seqEFEMStepNum;
            //if (_seqAVIStepNum != _seqAVIStepNumOld)
            //    _seqAVIStepNumOld = _seqAVIStepNum;

            if (GG.Equip.EquipRunMode == EmEquipRunMode.Auto)
                SetRunMode(EmEfemRunMode.Stop);
            else if (GG.Equip.IsHeavyAlarm)
                SetRunMode(EmEfemRunMode.Stop);

            if (this.RunMode == EmEfemRunMode.Stop
                || this.RunMode == EmEfemRunMode.Home
                || this.RunMode == EmEfemRunMode.Pause
                )
            {
                Stop();

                _seqEFEMStepNum = EmEFEMInitialLogicStep.S000_WAIT;
                _seqAVIStepNum = EmAVIInitialLogicStep.S000_WAIT;
            }
            else if (this.RunMode == EmEfemRunMode.Start)
            {
                AVIInitailLogic();
                EFEMInitailLogic();
            }
        }

        public EmEfemPort TargetPort;
        private void AVIInitailLogic()
        {
            switch (_seqAVIStepNum)
            {
                case EmAVIInitialLogicStep.S000_WAIT:
                    break;
                case EmAVIInitialLogicStep.S010_HOME_START:
                    {
                        if (GG.Equip.StHoming.IsHomeComplete)
                        {
                            _seqAVIStepNum = EmAVIInitialLogicStep.S030_LOADING_POSITION_MOVE_START;
                        }
                        else
                        {
                            GG.Equip.CmdHomePositioning();
                            _seqAVIStepNum = EmAVIInitialLogicStep.S020_HOME_COMPLETE_WAIT;
                        }
                    }
                    break;
                case EmAVIInitialLogicStep.S020_HOME_COMPLETE_WAIT:
                    {
                        if(GG.Equip.StHoming.IsStepComplete())
                        {
                            _seqAVIStepNum = EmAVIInitialLogicStep.S030_LOADING_POSITION_MOVE_START;
                        }
                    }
                    break;
                case EmAVIInitialLogicStep.S030_LOADING_POSITION_MOVE_START:
                    {
                        if (GG.Equip.StageX.MoveLoadingEx(GG.Equip) == false || 
                            GG.Equip.StageY.MoveLoadingEx(GG.Equip) == false ||
                            GG.Equip.Theta.MoveLoadingEx(GG.Equip) == false)
                        {
                            _seqAVIStepNum = EmAVIInitialLogicStep.S130_END;
                            return;
                        }

                        _seqAVIStepNum = EmAVIInitialLogicStep.S040_LOADING_POSITION_MOVE_WAIT;
                    }
                    break;
                case EmAVIInitialLogicStep.S040_LOADING_POSITION_MOVE_WAIT:
                    {
                        if(GG.Equip.StageX.IsMoveOnPosition(StageXServo.LoadingPos) &&
                            GG.Equip.StageY.IsMoveOnPosition(StageYServo.LoadingPos) &&
                            GG.Equip.Theta.IsMoveOnPosition(ThetaServo.LoadingPos))
                        {
                            _seqAVIStepNum = EmAVIInitialLogicStep.S050_VACUUM_OFF_START;
                        }
                    }
                    break;
                case EmAVIInitialLogicStep.S050_VACUUM_OFF_START:
                    {
                        GG.Equip.Vacuum.StartOffStep();
                        _seqAVIStepNum = EmAVIInitialLogicStep.S060_VACUUM_OFF_WAIT;
                    }
                    break;
                case EmAVIInitialLogicStep.S060_VACUUM_OFF_WAIT:
                    {
                        if(GG.Equip.Vacuum.IsVacuumOff)
                        {
                            _seqAVIStepNum = EmAVIInitialLogicStep.S070_LIFT_PIN_UP_START;
                        }
                    }
                    break;
                case EmAVIInitialLogicStep.S070_LIFT_PIN_UP_START:
                    {
                        if (GG.Equip.LiftPin.Forward(GG.Equip) == false)
                        {
                            _seqAVIStepNum = EmAVIInitialLogicStep.S130_END;
                            return;
                        }
                        _seqAVIStepNum = EmAVIInitialLogicStep.S080_LIFT_PIN_UP_WAIT;
                    }
                    break;
                case EmAVIInitialLogicStep.S080_LIFT_PIN_UP_WAIT:
                    {
                        if(GG.Equip.LiftPin.IsForward)
                        {
                            _seqAVIStepNum = EmAVIInitialLogicStep.S090_CENTERING_FORWARD_START;
                        }
                    }
                    break;
                case EmAVIInitialLogicStep.S090_CENTERING_FORWARD_START:
                    {
                        if (GG.Equip.Centering.CenteringFoward() == false)
                        {
                            _seqAVIStepNum = EmAVIInitialLogicStep.S130_END;
                            return;
                        }
                        _seqAVIStepNum = EmAVIInitialLogicStep.S100_CENTERING_FORWARD_WAIT;
                    }
                    break;
                case EmAVIInitialLogicStep.S100_CENTERING_FORWARD_WAIT:
                    {
                        if(GG.Equip.Centering.IsCenteringForward(GG.Equip))
                        {
                            _seqAVIStepNum = EmAVIInitialLogicStep.S110_CENTERING_BACKWARD_START;
                        }
                    }
                    break;
                case EmAVIInitialLogicStep.S110_CENTERING_BACKWARD_START:
                    {
                        if (GG.Equip.Centering.CenteringBackward() == false) _seqAVIStepNum = EmAVIInitialLogicStep.S130_END;
                        _seqAVIStepNum = EmAVIInitialLogicStep.S120_CENTERING_BACKWARD_END;
                    }
                    break;
                case EmAVIInitialLogicStep.S120_CENTERING_BACKWARD_END:
                    {
                        if (GG.Equip.Centering.IsCenteringBackward(GG.Equip))
                        {
                            _seqAVIStepNum = EmAVIInitialLogicStep.S130_END;
                        }
                    }
                    break;
                case EmAVIInitialLogicStep.S130_END:
                    SetRunMode(EmEfemRunMode.Stop);
                    _seqAVIStepNum = EmAVIInitialLogicStep.S000_WAIT;
                    break;
            }
        }
        private void EFEMInitailLogic()
        {
            switch (_seqEFEMStepNum)
            {
                case EmEFEMInitialLogicStep.S000_WAIT:
                    break;
                case EmEFEMInitialLogicStep.S010_RESET_START:
                    {
                        if (GG.Equip.Efem.Proxy.StartCommand(GG.Equip, TargetPort, EmEfemCommand.RESET) == false)
                        {
                            _seqEFEMStepNum = EmEFEMInitialLogicStep.S100_END;
                        }
                        _seqEFEMStepNum = EmEFEMInitialLogicStep.S020_RESET_COMPLETE_WAIT;
                    }
                    break;
                case EmEFEMInitialLogicStep.S020_RESET_COMPLETE_WAIT:
                    {
                        if(GG.Equip.Efem.Proxy.IsComplete(GG.Equip, TargetPort, EmEfemCommand.RESET))
                        {
                            _seqEFEMStepNum = EmEFEMInitialLogicStep.S030_INITAIL_START;
                        }
                    }
                    break;
                case EmEFEMInitialLogicStep.S030_INITAIL_START:
                    {
                        if (GG.Equip.Efem.Proxy.StartCommand(GG.Equip, TargetPort, EmEfemCommand.INIT_) == false)
                        {
                            _seqEFEMStepNum = EmEFEMInitialLogicStep.S100_END;
                        }
                        _seqEFEMStepNum = EmEFEMInitialLogicStep.S040_INITAIL_COMPLETE_WAIT;
                    }
                    break;
                case EmEFEMInitialLogicStep.S040_INITAIL_COMPLETE_WAIT:
                    {
                        if(GG.Equip.Efem.Proxy.IsComplete(GG.Equip, TargetPort, EmEfemCommand.INIT_))
                        {
                            _seqEFEMStepNum = EmEFEMInitialLogicStep.S100_END;
                        }
                    }
                    break;
                case EmEFEMInitialLogicStep.S100_END:
                    {
                        SetRunMode(EmEfemRunMode.Stop);
                        TargetPort = EmEfemPort.NONE;
                        _seqEFEMStepNum = EmEFEMInitialLogicStep.S000_WAIT;
                    }
                    break;
            }
        }

        public bool StartInitial(EmEfemPort target)
        {
            if(target == EmEfemPort.NONE)
            {
                return false;
            }
            if (IsRunning())
            {
                InterLockMgr.AddInterLock("다른 포트가 이미 진행 중입니다");
                return false;
            }

            TargetPort = target;
            SetRunMode(EmEfemRunMode.Start);

            if(TargetPort == EmEfemPort.EQUIPMENT)
            {
                _seqAVIStepNum = EmAVIInitialLogicStep.S010_HOME_START;
            }
            else
            {
                _seqEFEMStepNum = EmEFEMInitialLogicStep.S010_RESET_START;
            }
            return true;
        }
        public bool IsRunning()
        {
            return RunMode == EmEfemRunMode.Start;
        }
        public bool StepStop(Equipment equip)
        {
            _seqAVIStepNum = EmAVIInitialLogicStep.S000_WAIT;
            _seqEFEMStepNum = EmEFEMInitialLogicStep.S000_WAIT;
            RunMode = EmEfemRunMode.Stop;

            return true;
        }
    }
}
