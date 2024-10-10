using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using EquipMainUi.Setting;
using EquipMainUi.Struct.Detail;
using EquipMainUi.Struct.Detail.EziStep;

namespace EquipMainUi.Struct.Step
{
    public enum EmSF_NO
    {
        S000_WAIT,
        S010_SAFETY_RESET_START,
        S020_SAFETY_RESET_END
    };

    public class SafetyStep : StepBase
    {
        private PlcTimerEx _SafetyPlcResetTmrDelay = new PlcTimerEx("Safety Plc Reset Delay");

        public override void LogicWorking(Equipment equip)
        {
            PioEmergencyLogic(equip);
            EmergencyLogicWorking(equip);
            if (GG.TestMode == false)
            {
                if (equip.SERVO_MC_POWER_ON_1.IsOn == false)
                {
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0270_SERVO_MC_POWER_OFF_1);
                    SafetyResetWorking(equip);
                }
            }            
        }

        private bool readyToInputArm = false;
        private bool readyToAlignerInputArm = false;
        private void PioEmergencyLogic(Equipment equip)
        {
            readyToInputArm = equip.LiftPin.IsForward
                && equip.Centering.IsCenteringBackward(equip)
                && equip.Theta.IsMoveOnPosition(ThetaServo.LoadingPos)
                && equip.StageX.IsMoveOnPosition(StageXServo.LoadingPos)
                && equip.StageY.IsMoveOnPosition(StageYServo.LoadingPos)                
                ;
            equip.IsReadyToInputArm.OnOff(equip, readyToInputArm);

            if(GG.IsDitPreAligner)
            {
                readyToAlignerInputArm = GG.Equip.AlignerVac.IsVacuumOff && GG.Equip.AlignerOcrCylinder.IsUp &&
                    GG.Equip.AlignerX.IsMoveOnPosition(AlignerXEzi.UnloadingPos) &&
                    GG.Equip.AlignerY.IsMoveOnPosition(AlignerYEzi.UnloadingPos) &&
                    GG.Equip.AlignerT.IsMoveOnPosition(AlignerThetaEzi.UnloadingPos);

                GG.Equip.IsReadyToAlignInputArm.OnOff(GG.Equip, readyToAlignerInputArm);
            }


            if (GG.TestMode == false
                && equip.IsReadyToInputArm.IsSolOnOff == false
                && (equip.IsRobotArmDetect)// || equip.IsEFEMInputArm.IsOn == true)
                )
            {
                if (equip.EquipRunMode == EmEquipRunMode.Auto || equip.IsHomePositioning == true)
                {
                    if (GG.EfemNoUse == false)
                    {
                        equip.Efem.ImmediateStop(equip);
                        equip.Efem.ChangeMode(EmEfemRunMode.Stop);
                        GG.EfemNoUse = true;
                    }
                    if (equip.IsBodyMoving == true)
                        ImmediateStop(equip);                    
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0290_ROBOT_ARM_DETECT_ERROR);
                    equip.IsInterlock = true;

                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Robot Arm 投入中, Robot Arm 可投入信号被解除. Robot INIT 命令后, 在运营 Option使用 EFEM 打开 Mode" : "로봇암 투입 중 로봇암 투입 가능 신호가 해제 되었습니다. 로봇 INIT 명령 후 운영옵션에서 EFEM 사용모드를 켜주세요");
                    Logger.Log.AppendLine(LogLevel.NoLog, "신호 AVI Ready (PinUP:{0}, CenteringBack:{1}, ThetaLd:{2}, XLd:{3}, YLd:{4}",
                        equip.LiftPin.IsForward, equip.Centering.IsCenteringBackward(equip), equip.Theta.IsMoveOnPosition(ThetaServo.LoadingPos),
                        equip.StageX.IsMoveOnPosition(StageXServo.LoadingPos), equip.StageY.IsMoveOnPosition(StageYServo.LoadingPos));
                }
                else if (equip.EquipRunMode == EmEquipRunMode.Manual)
                {
                    if (GG.EfemNoUse == false)
                    {                        
                        equip.Efem.ImmediateStop(equip);
                        equip.Efem.ChangeMode(EmEfemRunMode.Stop);
                        GG.EfemNoUse = true;
                    }
                    if (equip.IsBodyMoving == true)
                        ImmediateStop(equip);
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0290_ROBOT_ARM_DETECT_ERROR);
                    equip.IsInterlock = true;

                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Robot Arm 投入中, Robot Arm 可投入信号被解除. Robot INIT 命令后, 在运营 Option使用 EFEM 打开 Mode" : "로봇암 투입 중 로봇암 투입 가능 신호가 해제 되었습니다. 로봇 INIT 명령 후 운영옵션에서 EFEM 사용모드를 켜주세요");
                    Logger.Log.AppendLine(LogLevel.NoLog, "신호 AVI Ready (PinUP:{0}, CenteringBack:{1}, ThetaLd:{2}, XLd:{3}, YLd:{4}",
                        equip.LiftPin.IsForward, equip.Centering.IsCenteringBackward(equip), equip.Theta.IsMoveOnPosition(ThetaServo.LoadingPos),
                        equip.StageX.IsMoveOnPosition(StageXServo.LoadingPos), equip.StageY.IsMoveOnPosition(StageYServo.LoadingPos));             
                }
            }
        }

        private EmGrapSw _oldGrip = EmGrapSw.NORMAL;
        public void EmergencyLogicWorking(Equipment equip)
        {
            bool isEmergency = equip.IsEmergency;
            bool isDoorOpen = equip.IsDoorOpen;
            bool isDoorSolOn = equip.IsDoorSolOn;
            bool isInnerWorkOn = equip.IsInnerWorkOn;

            if (isDoorSolOn && equip.ModeSelectKey.IsAuto)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Door Sol开启的状态下转换为Auto Mode." : "Door Sol이 켜진 상태에서 Auto Mode로\n전환 되었습니다.", GG.boChinaLanguage ? "转换为Teach Mode后，请关掉 Door Sol以后进行" : "Teach Mode로 전환 후 Door Sol을 끄고 진행 해 주세요");
            }

            //             if (GG.UseManualIonizer == false && isDoorOpen == true && GG.TestMode == false && equip.Ionizer.IsIonizerOn)
            //                 equip.Ionizer.IonizerOff();

            if ((equip.ADC.CurCPBoxTemp >= equip.CtrlSetting.AnalogSet.CpBoxAlarmTemp
                || equip.ADC.CurPCRackTemp >= equip.CtrlSetting.AnalogSet.PcRackAlarmTemp
                //|| equip.FireAlarm.XB_OnOff.vBit == true)
                )
                // && equip.TemperatureError.YB_OnOff.vBit == false
                )
            {
                if (equip.ADC.CurCPBoxTemp >= equip.AnalogSetting.Panel_Temp)
                {
                    equip.TempCtrlReseted = false;
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0286_CP_BOX_TEMPERATURE_HIGH_ERROR);
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "<Interlock>\n(CP BOX 温度发生问题 ! 需要确认状态)" : "<INTERLOCK>\nCP BOX 온도 이상! 상태 확인 필요");
                }

                if (equip.ADC.CurPCRackTemp >= equip.AnalogSetting.Pc_Rack_Temp)
                {
                    equip.TempCtrlReseted = false;
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0287_PC_RACK1_TEMPERATURE_HIGH_ERROR);
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "<Interlock>\n(PC RACK 温度发生问题 ! 需要确认状态)" : "<INTERLOCK>\nPC RACK 온도 이상! 상태 확인 필요");
                }
            }
            //else if (equip.TemperatureError.YB_OnOff.vBit == true)
            //{
            //    equip.TemperatureError.YB_OnOff.vBit = false;
            //}

            equip.ModeSelectKey.SolOn(
                isEmergency == false 
                && isDoorOpen == false 
                && isDoorSolOn == false 
                && isInnerWorkOn == false
                && equip.EquipRunMode == EmEquipRunMode.Manual                
                );

            //EmGrapSw isGripOn = equip.IsEnableGripSwOn;

            //if (GG.TestMode == false)
            //{
            //    if (isGripOn == EmGrapSw.EMERGENCY_ON
            //        || (isGripOn == EmGrapSw.NORMAL && _oldGrip == EmGrapSw.MIDDLE_ON)
            //        )
            //    {
            //        foreach (ServoMotorPmac m in equip.Motors)
            //            m.JogMove(equip, EM_SERVO_JOG.JOG_STOP, 0);
            //        equip.IsInterlock = true;
            //    }
            //}
            //_oldGrip = isGripOn;
            if (equip.IsEmergencyButtonOn)
                equip.PMac.StartCommand(equip, EmPMacmd.IMMEDIATE_STOP, 0);

            if (GG.TestMode == true || equip.IsUseInterLockOff == true || equip.IsUseDoorInterLockOff == true)
            {

            }
            else if (equip.EquipRunMode == EmEquipRunMode.Auto && (equip.IsDoorOpen == true))
                //|| (GG.EfemNoUse == false && equip.Efem.IsRunMode && (equip.Efem.Status.IsDoorClose == false)))
            {                
                ImmediateStop(equip);
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0282_DOOR_OPEN_ON_AUTO_RUN);
            }
            else if ((equip.EquipRunMode == EmEquipRunMode.Auto && equip.ModeSelectKey.IsAuto == false))
                //|| (GG.EfemNoUse == false && equip.Efem.IsRunMode && (equip.Efem.Status.IsModeSwitchAuto == false)))
            {                
                ImmediateStop(equip);
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0281_KEY_IS_CHANGED_ON_AUTO_RUN);
            }

            if (equip.EquipRunMode == EmEquipRunMode.Manual && equip.ModeSelectKey.IsAuto == false && equip.IsKeyChangeToTeach == false)
            {
                equip.IsKeyChangeToTeach = true;
                equip.IsInterlock = true;
            }
            else if (equip.ModeSelectKey.IsAuto == true)
            {
                if (equip.IsKeyChangeToTeach == true)
                    equip.IsKeyChangeToTeach = false;
            }     

            if (equip.ModeSelectKey.IsAuto == true)   //AUTO
            {
                if (isDoorOpen || isEmergency)
                {
                    if (equip.IsPause == false)
                    equip.IsPause = true;
                    if (equip.EquipRunMode != EmEquipRunMode.Manual)
                        equip.ChangeRunMode(EmEquipRunMode.Manual);
                }
            }
            else
            {
                if (isEmergency)
                {
                    if (equip.IsPause == false)
                        equip.IsPause = true;
                    if (equip.EquipRunMode != EmEquipRunMode.Manual)
                        equip.ChangeRunMode(EmEquipRunMode.Manual);
                }
            }
        }

        private static void ImmediateStop(Equipment equip)
        {
            equip.IsInterlock = true;
            equip.ChangeRunMode(EmEquipRunMode.Manual);
            equip.Efem.ChangeMode(EmEfemRunMode.Stop);
            //equip.Efem.ImmediateStop(equip);
            if (equip.PMac.IsCommandAck(equip, EmPMacmd.IMMEDIATE_STOP) == true)
                equip.PMac.StartCommand(equip, EmPMacmd.IMMEDIATE_STOP, 0);
        }

        private EmSF_NO _stepNum = EmSF_NO.S000_WAIT;
        public void SafetyResetWorking(Equipment equip)
        {
            if (_stepNum == EmSF_NO.S000_WAIT)
            {

            }
            else if (_stepNum == EmSF_NO.S010_SAFETY_RESET_START)
            {
                equip.SafetyCircuitReset.OnOff(equip, true);
                _SafetyPlcResetTmrDelay.Start(0, 2000);
                _stepNum = EmSF_NO.S020_SAFETY_RESET_END;
            }
            else if (_stepNum == EmSF_NO.S020_SAFETY_RESET_END)
            {
                if (_SafetyPlcResetTmrDelay)
                {
                    _SafetyPlcResetTmrDelay.Stop();

                    if (GG.TestMode == false)
                    {
                        if (equip.SERVO_MC_POWER_ON_1.IsOn == false)
                            AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0270_SERVO_MC_POWER_OFF_1);                        
                    }

                    equip.SafetyCircuitReset.OnOff(equip, false);
                    _stepNum = EmSF_NO.S000_WAIT;
                }
            }
        }

        public void StartReset()
        {
            _stepNum = EmSF_NO.S010_SAFETY_RESET_START;
            if (GG.EfemNoUse == false && GG.Equip.Efem.Status.SafetyPlcState != Detail.EFEM.EmEfemSafetyPLCState.NORMAL)
                GG.Equip.Efem.Proxy.StartCommand(GG.Equip, Detail.EFEM.EmEfemPort.ETC, Detail.EFEM.EmEfemCommand.RESET);
        }
    }
}
