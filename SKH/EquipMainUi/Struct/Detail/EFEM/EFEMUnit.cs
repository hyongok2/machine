using EquipMainUi.Struct.Detail.EFEM.Step;
using EquipMainUi.Struct.Step;
using EquipMainUi.Struct.TransferData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.Detail.EFEM
{
    public class EFEMUnit : StepBase
    {
        private Equipment _equip;
        public EFEMPcProxy Proxy;
        public EFEMRobotUnit Robot;
        public EFEMLPMUnit LoadPort1;
        public EFEMLPMUnit LoadPort2;
        public EFEMLPMUnit LoadPort(EmEfemPort port) { if (port == EmEfemPort.LOADPORT1) return LoadPort1; else if (port == EmEfemPort.LOADPORT2) return LoadPort2; else return null; }
        public EFEMLPMUnit OtherLoadPort(EmEfemPort port) { if (port == EmEfemPort.LOADPORT1) return LoadPort2; else if (port == EmEfemPort.LOADPORT2) return LoadPort1; else return null; }
        public EFEMAlignerUnit Aligner;

        public LightCurtain LPMLightCurtain = new LightCurtain();

        public EFEMStatus Status { get { return Proxy.EfemStat; } }

        private EFEMSIGLMDataSet _lastUserSignal;
        public EFEMSIGLMDataSet UserSignal;

        public bool IsError;

        public bool IsCstProgressing(WaferInfoKey key)
        {
            if(Robot.LowerWaferKey.CstID == key.CstID)
            {
                return true;
            }
            else if(Robot.UpperWaferKey.CstID == key.CstID)
            {
                return true;
            }
            else if(Aligner.LowerWaferKey.CstID == key.CstID)
            {
                return true;
            }
            else if (GG.Equip.TransferUnit.LowerWaferKey.CstID == key.CstID)
            {
                return true;
            }

            return false;
        }
        public EFEMLPMUnit GetCurProgressCst()
        {
            //kkt 진행 중인 카세트 정의 필요
            if(LoadPort1.Status.IsDoorOpen)
            {
                return LoadPort1;
            }
            else if(LoadPort1.Status.IsDoorOpen)
            {
                return LoadPort2;
            }
            else
            {
                return null;
            }
        }

        public override bool IsReserveStart { get { return GetAllUnits().TrueForAll(u => u.IsReserveStart == true); } set { GetAllUnits().ForEach(u => u.IsReserveStart = value); } }

        public int StatReadTime = 300;

        public bool LastRobotForward;
        public EmEfemPort LastRobotPort;
        public bool IsPickingSlot1LPM1;
        public bool IsPickingSlot1LPM2;

        public override bool IsHomeComplete {
            get
            {
                if(_equip.IsAutoOnlyAligner == false)
                {
                    return Robot.IsHomeComplete == true
                    && LoadPort1.IsHomeComplete == true
                    && LoadPort2.IsHomeComplete == true
                    && Aligner.IsHomeComplete == true;
                }
                else
                {
                    return Aligner.IsHomeComplete == true;
                }
            }
        }

        public EFEMUnit()
        {
            Proxy = new EFEMPcProxy();

            Robot = new EFEMRobotUnit(this);
            LoadPort1 = new EFEMLPMUnit(this, EmEfemPort.LOADPORT1) { DBPort = EmEfemDBPort.LOADPORT1 };
            LoadPort2 = new EFEMLPMUnit(this, EmEfemPort.LOADPORT2) { DBPort = EmEfemDBPort.LOADPORT2 };
            Aligner = new EFEMAlignerUnit(this) { DBPort = EmEfemDBPort.ALIGNER };

            Robot.SetName("ROBOT");
            LoadPort1.SetName("LOADPORT1");
            LoadPort2.SetName("LOADPORT2");
            Aligner.SetName("ALIGNER");

            _lastUserSignal = new EFEMSIGLMDataSet(EmEfemLampBuzzerState.ON, EmEfemLampBuzzerState.ON, EmEfemLampBuzzerState.ON, EmEfemLampBuzzerState.ON, EmEfemLampBuzzerState.ON);
            UserSignal = new EFEMSIGLMDataSet(EmEfemLampBuzzerState.OFF, EmEfemLampBuzzerState.OFF, EmEfemLampBuzzerState.OFF, EmEfemLampBuzzerState.OFF, EmEfemLampBuzzerState.OFF);

            UserSignal.Blue = EmEfemLampBuzzerState.OFF;
        }

        public override void LogicWorking(Equipment equip)
        {
            LightCurtainLogic(equip);            
            Proxy.LogicWorking(equip); // 일부 명령은 처리 위해 NoUseMode에서도 동작

            if (GG.EfemNoUse == false && Proxy.IsConnected == true)
            {                
                StatusLogicWorking(equip);                
                SeqLogicWorking(equip);
            }
            else if (GG.Equip.IsAutoOnlyAligner)
            {
                Aligner.LogicWorking(equip);
            }
        }

        private void LightCurtainLogic(Equipment equip)
        {
            LPMLightCurtain.LogicWorking(equip);
            if (GG.EfemLongRun == false && equip.IsBuzzerAllOff == false)
            {
                if (LPMLightCurtain.IsMuting == false && LPMLightCurtain.Detect.IsOn == true) //라이트커튼 제거
                {
                    if (GG.TestMode == false)
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0870_LPM_LIGHT_CURTAIN_DETECTED);
                }
                else if (LPMLightCurtain.IsMuting == true)
                {
                    //kkt::20 11 11  Light Curtan Muting 되고 나서 buzzer3 게속 울리는것 때문에 삭제
                    //equip.BuzzerK3.OnOff(equip, true);
                }

                else
                {
                    equip.BuzzerK3.OnOff(equip, false);
                }
            }
        }

        private void SeqLogicWorking(Equipment equip)
        {
            if (IsError == false)
            {
                if (IsHomeComplete == true && IsReserveStart == true)
                {
                    ChangeMode(EmEfemRunMode.Start);
                    IsReserveStart = false;
                }

                if (equip.IsImmediatStop == true && RunMode == EmEfemRunMode.Start)
                    ChangeMode(EmEfemRunMode.Pause);

                if (GG.TestMode == true
                    || Status.IsAutoMode == true)
                {
                    Robot.LogicWorking(equip);
                    LoadPort1.LogicWorking(equip);
                    LoadPort2.LogicWorking(equip);
                    Aligner.LogicWorking(equip);
                }
            }
        }

        public override void StatusLogicWorking(Equipment equip)
        {
            IsError = Status.IsEMO;

            GetStatus(equip);
            TowerLampLogic(equip);
            LampBuzzerLogic(equip);
            SafetyLogic(equip);
        }

        /// <summary>
        /// LAMP    Desc                        ON                      BLINK               OFF
        /// RED     Error&Maint                 Maint                   Alarm               Others
        /// YELLOW  New Carrier Acceptability   Acceptable New Carrier  Never               Not Accept
        /// GREEN   Process State               Process                 Never               Idle
        /// BLUE    Equipment Mode ??           OHT Mode                Online Remote Mode  Online Local Mode
        ///                                     Online+Remote+AMHS      Online Remote       Online Local
        /// </summary>
        /// <param name="equip"></param>
        private void TowerLampLogic(Equipment equip)
        {
            // RED
            if ((equip.IsHeavyAlarm ||/* equip.IsLightAlarm ||*/ equip.IsUseDoorInterLockOff || equip.IsUseInterLockOff || equip.EquipRunMode == EmEquipRunMode.Manual))
                UserSignal.Red = EmEfemLampBuzzerState.ON;
            //else if (equip.EquipRunMode == EmEquipRunMode.Manual)
            //    UserSignal.Red = EmEfemLampBuzzerState.ON;
            else
                UserSignal.Red = EmEfemLampBuzzerState.OFF;

            // YELLOW
            if (LoadPort1.IsLdButtonWaitStep && LoadPort2.IsLdButtonWaitStep)
                UserSignal.Yellow = EmEfemLampBuzzerState.ON;
            else if (equip.EquipRunMode == EmEquipRunMode.Manual)
                UserSignal.Yellow = EmEfemLampBuzzerState.ON;
            else if (LoadPort1.IsStandByRunState && LoadPort2.IsStandByRunState)
                UserSignal.Yellow = EmEfemLampBuzzerState.ON;
            else if(LoadPort1.OHTpio.IsRunning || LoadPort1.OHTpio.IsRunning || equip.IsLightAlarm)
                UserSignal.Yellow = EmEfemLampBuzzerState.BLINK;
            else
                UserSignal.Yellow = EmEfemLampBuzzerState.OFF;

            // GREEN
            if (equip.EquipRunMode == EmEquipRunMode.Auto && equip.IsPause == false && UserSignal.Yellow != EmEfemLampBuzzerState.ON)
                UserSignal.Green = EmEfemLampBuzzerState.ON;
            else
                UserSignal.Green = EmEfemLampBuzzerState.OFF;

            //// BLUE
            //if (equip.Efem.LoadPort1.LoadType == EmLoadType.OHT || equip.Efem.LoadPort2.LoadType == EmLoadType.OHT)
            //    UserSignal.Blue = EmEfemLampBuzzerState.ON;
            //else
            //    UserSignal.Blue = EmEfemLampBuzzerState.OFF;
        }

        Dit.Framework.PeriodChecker _statPeriod = new Dit.Framework.PeriodChecker();
        public void GetStatus(Equipment equip)
        {
            if (_statPeriod.IsTimeToCheck(this.StatReadTime))
            {
                _statPeriod.Reset = true;

                if (GG.TestMode == true)
                    return;

                if (this.Proxy.IsRunning(equip, EmEfemPort.ETC, EmEfemCommand.STAT_) == false)
                    this.Proxy.StartCommand(equip, EmEfemPort.ETC, EmEfemCommand.STAT_);
            }
        }

        private void SafetyLogic(Equipment equip)
        {
            if (GG.TestMode == true)
                return;            

            if (GG.TestMode == true || equip.IsUseInterLockOff == true || equip.IsUseDoorInterLockOff == true)
            {

            }
            else if (RunMode == EmEfemRunMode.Start
                && Status.IsModeSwitchAuto == false
                )
            {
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0670_EFEM_MODE_KEY_IS_CHANGED_ON_AUTO_RUN);
                equip.IsInterlock = true;
                ChangeMode(EmEfemRunMode.Stop);
                equip.Efem.ImmediateStop(equip);
            }
            else if (RunMode != EmEfemRunMode.Stop
               && Status.IsModeSwitchAuto == false
               && Robot.Status.IsMoving == true
               )
            {
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0670_EFEM_MODE_KEY_IS_CHANGED_ON_AUTO_RUN);                
                if (Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.STOP_) == true)
                    Proxy.StartCommand(equip, EmEfemPort.ROBOT, EmEfemCommand.STOP_);
                equip.IsInterlock = true;
                ChangeMode(EmEfemRunMode.Stop);
                equip.Efem.ImmediateStop(equip);
            }
            else if (RunMode == EmEfemRunMode.Start
                && Status.IsDoorClose == false)
            {
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0671_EFEM_DOOR_OPEN);
                if (Proxy.IsComplete(equip, EmEfemPort.ROBOT, EmEfemCommand.STOP_) == true)
                    Proxy.StartCommand(equip, EmEfemPort.ROBOT, EmEfemCommand.STOP_);
                equip.IsInterlock = true;
                ChangeMode(EmEfemRunMode.Stop);
                equip.Efem.ImmediateStop(equip);
            }
        }

        internal void ImmediateStop(Equipment equip)
        {
            Proxy.StartCommand(equip, EmEfemPort.ROBOT, EmEfemCommand.STOP_);
            Proxy.StartCommand(equip, EmEfemPort.LOADPORT1, EmEfemCommand.STOP_);
            Proxy.StartCommand(equip, EmEfemPort.LOADPORT2, EmEfemCommand.STOP_);
        }

        private PlcTimerEx _siglmDelay = new PlcTimerEx("SIGLM DELAY", false);
        private int _lampBuzzerStep = 0;
        public void LampBuzzerLogic(Equipment equip)
        {
            switch (_lampBuzzerStep)
            {

                case 0:
                    if (UserSignal.Red != _lastUserSignal.Red
                        || UserSignal.Yellow != _lastUserSignal.Yellow
                        || UserSignal.Green != _lastUserSignal.Green
                        || UserSignal.Blue != _lastUserSignal.Blue
                        || UserSignal.Buzzer1 != _lastUserSignal.Buzzer1
                        )
                    {
                        //_siglmDelay.Start(0, 100);
                        _lampBuzzerStep = 10;
                    }
                    break;
                case 10:
                   // if (_siglmDelay)
                    {
                       // _siglmDelay.Stop();
                        _lampBuzzerStep = 20;
                    }
                    break;
                case 20:
                    if (this.Proxy.StartLampBuzzerChange(equip,
                        new EFEMSIGLMDataSet(
                            (UserSignal.Red != _lastUserSignal.Red ? UserSignal.Red : EmEfemLampBuzzerState.KEEP),
                            (UserSignal.Yellow != _lastUserSignal.Yellow ? UserSignal.Yellow : EmEfemLampBuzzerState.KEEP),
                            (UserSignal.Green != _lastUserSignal.Green ? UserSignal.Green : EmEfemLampBuzzerState.KEEP),
                            (UserSignal.Blue != _lastUserSignal.Blue ? UserSignal.Blue : EmEfemLampBuzzerState.KEEP),
                            (UserSignal.Buzzer1 != _lastUserSignal.Buzzer1 ? UserSignal.Buzzer1 : EmEfemLampBuzzerState.KEEP))) == false) return;
                    _lampBuzzerStep = 30;
                    break;
                case 30:
                    if (this.Proxy.IsComplete(equip, EmEfemPort.ETC, EmEfemCommand.SIGLM) == true)
                    {
                        _siglmDelay.Start(0, 300);
                        _lampBuzzerStep = 40;
                    }
                    break;
                case 40:
                    if (_siglmDelay)
                    {
                        _siglmDelay.Stop();
                        _lastUserSignal.Red = Status.TowerLampRed;
                        _lastUserSignal.Yellow = Status.TowerLampYellow;
                        _lastUserSignal.Green = Status.TowerLampGreen;
                        _lastUserSignal.Blue = Status.TowerLampBlue;
                        _lastUserSignal.Buzzer1 = UserSignal.Buzzer1;
                        _lampBuzzerStep = 0;
                    }
                    break;
            }
        }

        public void SetEquipment(Equipment equip)
        {
            _equip = equip;
            
            Robot.PioRecvLpm1.SetUpper(LoadPort1.PioSend);
            Robot.PioSendLpm1.SetLower(LoadPort1.PioRecv);

            Robot.PioRecvLpm2.SetUpper(LoadPort2.PioSend);
            Robot.PioSendLpm2.SetLower(LoadPort2.PioRecv);

            Robot.PioRecvAligner.SetUpper(Aligner.PioSend);
            Robot.PioSendAligner.SetLower(Aligner.PioRecv);

            Robot.PioRecvAVI.SetUpper(_equip.StMain.PioSend);
            Robot.PioSendAVI.SetLower(_equip.StMain.PioRecv);

            LoadPort1.PioRecv.SetUpper(Robot.PioSendLpm1);
            LoadPort1.PioSend.SetLower(Robot.PioRecvLpm1);

            LoadPort2.PioRecv.SetUpper(Robot.PioSendLpm2);
            LoadPort2.PioSend.SetLower(Robot.PioRecvLpm2);

            Aligner.PioRecv.SetUpper(Robot.PioSendAligner);
            Aligner.PioSend.SetLower(Robot.PioRecvAligner);

            _equip.StMain.PioRecv.SetUpper(Robot.PioSendAVI);
            _equip.StMain.PioSend.SetLower(Robot.PioRecvAVI);
        }

        public List<StepBase> GetAllUnits()
        {
            List<StepBase> l = new List<StepBase>();
            l.Add(Robot);
            l.Add(LoadPort1);
            l.Add(LoadPort2);
            l.Add(Aligner);
            return l;
        }
        public bool ChangeMode(EmEfemRunMode mode)
        {
            if (GG.EfemNoUse)
            {
                GetAllUnits().ForEach(f => f.SetRunMode(EmEfemRunMode.Stop));
                return true;
            }

            if (mode == EmEfemRunMode.Start || mode == EmEfemRunMode.Home)
            {
                if (Proxy.IsConnected == false)
                {
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "因EFEM通讯状态异常无法开始 " : "EFEM 통신 상태 이상으로 시작 불가");
                    return false;
                }
                if (GG.TestMode == false && _equip.IsUseDoorInterLockOff == false)
                {
                    #region INTERLOCK
                    if (Status.IsAutoMode == false)
                    {
                        InterLockMgr.AddInterLock(GG.boChinaLanguage ? "EFEM TEACH Mode下无法开始." : "EFEM TEACH 모드에서 시작이 불가 합니다.");
                        return false;
                    }


                    if (Status.IsDoorClose == false)
                    {
                        InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Door开启状态下设备无法动作." : "도어가 열린 상태이 있는 상태에서 설비 동작이 불가합니다.");
                        return false;
                    }
                    if (Status.IsModeSwitchAuto == false)
                    {
                        InterLockMgr.AddInterLock(GG.boChinaLanguage ? "只在Mode Select Key Auto 状态时可以动作." : "Mode Select Key Auto 상태에서만 동작 가능합니다.");
                        return false;
                    }
                    //if (Status.IsError == true)
                    //{
                    //    InterLockMgr.AddInterLock("EFEM Error 상태에서 설비 동작이 불가합니다.");
                    //    return false;
                    //}
                    if (Status.IsEMO == true)
                    {
                        InterLockMgr.AddInterLock(GG.boChinaLanguage ? "EMO状态下设备无法动作." : "EMO 상태에서 설비 동작이 불가합니다.");
                        return false;
                    }
                    #endregion INTERLOCK
                }
            }
            if (mode == EmEfemRunMode.Start)
            {
                if (GG.Equip.IsHeavyAlarm == true)
                {
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<HEAVY ALARM>\n(HEAVY ALARM 状态下，不能进行 AUTO变更 .)" : "인터락<HEAVY ALARM>\n(HEAVY ALARM 상태에서 AUTO 변경을 할 수 없습니다.)");
                    return false;
                }
            }
            

            if (mode == EmEfemRunMode.Start || mode == EmEfemRunMode.Home)
            {
                
            }
            else if (mode == EmEfemRunMode.Stop)
            {
                
            }

            if (mode == EmEfemRunMode.Start && GG.Equip.Efem.IsHomeComplete == false)
            {
                mode = EmEfemRunMode.Home;
                GetAllUnits().ForEach(f => f.SetRunMode(mode));
                RunMode = mode;
                GG.Equip.Efem.IsReserveStart = true;
            }
            else
            {
                GetAllUnits().ForEach(f => f.SetRunMode(mode));
                RunMode = mode;
            }
            
            return true;
        }
    }
}
