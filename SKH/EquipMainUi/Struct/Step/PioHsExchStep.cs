
using Dit.Framework.PLC;
using DitCim.PLC;
using System;
using EquipMainUi.Struct.Step;

namespace EquipMainUi.Struct
{
    public enum PIO_E
    {
        E000_WAIT,
        E010_START,
        E020_EXCH_FLAG_CHECK,
        E030_RECV_START_WAIT,
        E040_RECV_COMPLETE_WAIT,
        E050_GLASS_DETECT_CHECK,
        E060_PIO_RESET_WAIT,
        E070_PIO_COMPLETE,
        E080,
        E035_RECV_PIO_TEST,
        E065_PIO_GLASS_CHECK,
    }
    public enum PIO_REC_E
    {
        RE000_WAIT,
        RE00_START,
        RE010_MULTI_REQ_NACK,
        RE020_MULTI_REQ_NACK_COMPLETE,
        RE050_CANCEL_START,
        RE060_CANCEL_PIO_INIT,
        RE060_CANCEL_COMPLETE_WAIT,
        RE070_CANCEL_COMPLETE,

        RE100_RESUME_START,
        RE110_RESUME_COMPLETE_WAIT,
        RE110_RESUME_COMPLETE,
        RE200_ABORT_START,
        RE210_ABORT_COMPLETE_WAIT,
        RE220_ABORT_COMPLETE,
        RE205_ABORT_PIO_INIT,
    }
    public class PioHsExchStep : StepBase
    {
        public int T1_TIME_OUT = 3;
        public int T2_TIME_OUT = 30;
        public int T3_TIME_OUT = 3;
        public int T4_TIME_OUT = 3;
        public int T5_TIME_OUT = 3;
        public int T6_TIME_OUT = 60;

        public DateTime T1_TIME_START = DateTime.MinValue;
        public DateTime T2_TIME_START = DateTime.MinValue;
        public DateTime T3_TIME_START = DateTime.MinValue;
        public DateTime T4_TIME_START = DateTime.MinValue;
        public DateTime T5_TIME_START = DateTime.MinValue;
        public DateTime T6_TIME_START = DateTime.MinValue;

        //메소드 - EXCHAGE PIO 
        private bool _isPioExchangeStop = false;
        private bool _isPioExchangeComplete = false;
        public bool IsRecovery { get; set; }
        public PIO_E StepPioExchage = PIO_E.E000_WAIT;
        public PIO_REC_E StepRecovery = 0;
        private PlcTimerEx ExchangeDelay = new PlcTimerEx("PIO EXCH DELAY");

        public bool StartExchangePio(Equipment equip)
        {
            if (StepPioExchage != PIO_E.E000_WAIT)
            {
                InterLockMgr.AddInterLock("인터락<실행중>\n(EXCHAGE PIO 진행중 시작 명령이 들어왔습니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "EXCHAGE PIO 진행중 시작 명령이 들어옴.");
                return false;
            }

            IsRecoveryAbort = false;
            IsRecovery = false;

            _isPioExchangeStop = false;
            _isPioExchangeComplete = false;
            StepPioExchage = PIO_E.E010_START;
            return true;
        }
        public bool StopExchangePio(Equipment equip)
        {
            StepPioExchage = PIO_E.E000_WAIT;

            _isPioExchangeComplete = false;

            AOIB.LOSendAble.vBit = false;
            AOIB.LOSendStart.vBit = false;
            AOIB.LOSendComplete.vBit = false;

            return true;
        }
        public bool IsExchangeFullyComplete()
        {
            return IsExchangeComplete() && _isPioExchangeStop == false;
        }
        public bool IsExchangeComplete()
        {
            return _isPioExchangeComplete && StepPioExchage == PIO_E.E000_WAIT;
        }

        public override void LogicWorking(Equipment equip)
        {
            
            //Error Recovery Signal....
            if (StepPioExchage != PIO_E.E000_WAIT && StepPioExchage != PIO_E.E010_START && StepPioExchage != PIO_E.E020_EXCH_FLAG_CHECK)
                if (!(GG.TestMode || GG.PioTestMode) && 
                    (equip.HotLinkInx.UPPER_EMERGENCY_STOP_INPUT.vBit == false || CIMAB.LOEmergency.vBit == false))
                {
                    IsRecovery = true;
                }

            if (IsRecovery)
                LogicWorkingRecovery(equip);
            else
                LogicWorkingNormal(equip);
        }
        public void PioTimeOutCheck(Equipment equip)
        {
            //A --> I
            if (StepPioExchage != PIO_E.E000_WAIT)
            {
                if (T1_TIME_START != DateTime.MinValue)
                {
                    if ((DateTime.Now - T1_TIME_START).TotalSeconds > T1_TIME_OUT)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0612_PIO_EXCH_T1_TIME_OUT + 0);
                        equip.IsInterlock = true;
                    }
                }
                if (T2_TIME_START != DateTime.MinValue)
                {
                    if ((DateTime.Now - T2_TIME_START).TotalSeconds > T2_TIME_OUT)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0612_PIO_EXCH_T1_TIME_OUT + 1);
                        equip.IsInterlock = true;
                    }
                }
                if (T3_TIME_START != DateTime.MinValue)
                {
                    if ((DateTime.Now - T3_TIME_START).TotalSeconds > T3_TIME_OUT)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0612_PIO_EXCH_T1_TIME_OUT + 2);
                        equip.IsInterlock = true;
                    }
                }
                if (T4_TIME_START != DateTime.MinValue)
                {
                    if ((DateTime.Now - T4_TIME_START).TotalSeconds > T4_TIME_OUT)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0612_PIO_EXCH_T1_TIME_OUT + 3);
                        equip.IsInterlock = true;
                    }
                }
                if (T5_TIME_START != DateTime.MinValue)
                {
                    if ((DateTime.Now - T5_TIME_START).TotalSeconds > T5_TIME_OUT)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0612_PIO_EXCH_T1_TIME_OUT + 4);
                        equip.IsInterlock = true;
                    }
                }
                if (T6_TIME_START != DateTime.MinValue)
                {
                    if ((DateTime.Now - T6_TIME_START).TotalSeconds > T6_TIME_OUT)
                    {
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0612_PIO_EXCH_T1_TIME_OUT + 5);
                        equip.IsInterlock = true;
                    }
                }
            }

        }

        public bool IsRunning
        {
            get
            {
                return CIMAB.LOSendAble == true || CIMAB.LOSendStart == true || CIMAB.LOSendComplete == true;
            }
        }
        public bool IsSignalError
        {
            get
            {
                return (CIMAB.LOSendAble == false && CIMAB.LOSendStart == false && CIMAB.LOSendComplete == false) &&
                    (AOIB.UPReceiveStart.vBit == true || AOIB.UPReceiveComplete.vBit == true);
            }
        }

        public void LogicWorkingNormal(Equipment equip)
        {
            // PioTimeOutCheck(equip);

            if (StepPioExchage == PIO_E.E000_WAIT)
            {
            }
            else if (StepPioExchage == PIO_E.E010_START)
            {
                T1_TIME_START = DateTime.MinValue;
                T2_TIME_START = DateTime.MinValue;
                T3_TIME_START = DateTime.MinValue;
                T4_TIME_START = DateTime.MinValue;
                T5_TIME_START = DateTime.MinValue;
                T6_TIME_START = DateTime.MinValue;

                if (equip.IsGlassDetect != EmGlassDetect.NOT)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "GLASS EXCHANGE PIO 시작");

                    _isPioExchangeComplete = false;
                    AOIB.LOSendAble.vBit = true;
                    StepPioExchage = PIO_E.E020_EXCH_FLAG_CHECK;
                }
                else
                {
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0704_PIO_GLASS_DETECT_ERROR);
                    equip.IsInterlock = true;
                    //InterLockMgr.AddInterLock("인터락<INDEXER/GLASS DETECT SENSOR>\n(인덱서가 EXCHANGE 중 GLASS가 없거나. 센서가 일부 감지되었습니다.) ");
                    Logger.Log.AppendLine(LogLevel.Warning, "인덱서가 EXCHANGE 중 GLASS가 없거나. 센서가 일부 감지됨. ");
                }
            }
            else if (StepPioExchage == PIO_E.E020_EXCH_FLAG_CHECK)
            {
                if (CIMAB.UPReceiveAble == true)
                {

                    if (CIMAB.UPExchangeFlag == true)
                    {
                        AOIB.LOSendAble.vBit = true;
                        AOIB.LOSendStart.vBit = true;
                        AOIB.LOExchangeFlag.vBit = true;

                        T1_TIME_START = DateTime.Now;

                        StepPioExchage = PIO_E.E030_RECV_START_WAIT;
                    }
                    else
                    {
                        _isPioExchangeStop = true;
                        _isPioExchangeComplete = true;
                        StepPioExchage = PIO_E.E000_WAIT;
                    }
                }
            }
            else if (StepPioExchage == PIO_E.E030_RECV_START_WAIT)
            {
                if (CIMAB.UPReceiveAble == true && CIMAB.UPReceiveStart == true && CIMAB.UPExchangeFlag == true)
                {
                    AOIB.LOSendAble.vBit = true;
                    AOIB.LOSendStart.vBit = true;
                    AOIB.LOExchangeFlag.vBit = true;

                    T1_TIME_START = DateTime.MinValue;
                    T2_TIME_START = DateTime.Now;

                    if (GG.PioTestMode == true)
                    {
                        StepPioExchage = PIO_E.E035_RECV_PIO_TEST;
                    }
                    else
                    {
                        StepPioExchage = PIO_E.E040_RECV_COMPLETE_WAIT;
                    }
                }
            }
            else if (StepPioExchage == PIO_E.E035_RECV_PIO_TEST)
            {
                if (GG.PioTestMode == true)
                {
                    if (CIMAB.UPReceiveAble == true && CIMAB.UPReceiveStart == true && CIMAB.UPReceiveComplete == true && CIMAB.UPExchangeFlag == true)
                    {
                        equip.RobotArmDefect.XB_OnOff.vBit = true;
                        equip.WaferDetectSensorLiftpin1.XB_OnOff.vBit = true;
                        equip.WaferDetectSensorStage2.XB_OnOff.vBit = true;

                        StepPioExchage = PIO_E.E040_RECV_COMPLETE_WAIT;
                    }
                }
            }
            else if (StepPioExchage == PIO_E.E040_RECV_COMPLETE_WAIT)
            {
                if (CIMAB.UPReceiveAble == true && CIMAB.UPReceiveStart == true && CIMAB.UPReceiveComplete == true &&
                    CIMAB.UPExchangeFlag == true)
                {
                    T2_TIME_START = DateTime.MinValue;
                    T3_TIME_START = DateTime.Now;

                    AOIB.LOExchangeFlag.vBit = true;
                    AOIB.LOSendAble.vBit = true;
                    AOIB.LOSendStart.vBit = true;

                    //톱텍 인덱서에서. 로봇암이 빠지지 않았는데.. Complete을 준적이 있음 확인 필요하여 추가. 
                    StepPioExchage = PIO_E.E050_GLASS_DETECT_CHECK;
                }
            }
            else if (StepPioExchage == PIO_E.E050_GLASS_DETECT_CHECK)
            {

                if (CIMAB.UPReceiveAble == true && CIMAB.UPReceiveStart == true && CIMAB.UPReceiveComplete == true &&
                    CIMAB.UPExchangeFlag == true
                    //&& (equip.IsGlassCrack != EmGlassCrack.NOT || equip.IsGlassDetect != EmGlassDetect.NOT)
                    && equip.IsRobotArmDetect == false)                    

                {
                    AOIB.LOExchangeFlag.vBit = true;
                    AOIB.LOSendAble.vBit = true;
                    AOIB.LOSendStart.vBit = true;
                    AOIB.LOSendComplete.vBit = true;

                    T3_TIME_START = DateTime.MinValue;
                    T4_TIME_START = DateTime.Now;

                    ExchangeDelay.Start(0, 100);
                    StepPioExchage = PIO_E.E060_PIO_RESET_WAIT;
                }
            }
            else if (StepPioExchage == PIO_E.E060_PIO_RESET_WAIT)
            {
                if (CIMAB.UPReceiveAble == false && CIMAB.UPReceiveStart == false && CIMAB.UPReceiveComplete == false && CIMAB.UPExchangeFlag == false)
                {
                    T4_TIME_START = DateTime.MinValue;
                    T5_TIME_START = DateTime.Now;

                    AOIB.LOExchangeFlag.vBit = false;
                    AOIB.LOSendAble.vBit = false;
                    AOIB.LOSendStart.vBit = false;
                    AOIB.LOSendComplete.vBit = false;
                    StepPioExchage = PIO_E.E065_PIO_GLASS_CHECK;
                }
            }
            else if (StepPioExchage == PIO_E.E065_PIO_GLASS_CHECK)
            {
                if (equip.IsGlassDetect != EmGlassDetect.NOT)
                    StepPioExchage = PIO_E.E070_PIO_COMPLETE;
            }
            else if (StepPioExchage == PIO_E.E070_PIO_COMPLETE)
            {
                T5_TIME_START = DateTime.MinValue;
                Logger.Log.AppendLine(LogLevel.Info, "GLASS EXCHANGE PIO 완료");
                _isPioExchangeStop = false;
                _isPioExchangeComplete = true;
                StepPioExchage = PIO_E.E000_WAIT;
            }
        }
        private void LogicWorkingRecovery(Equipment equip)
        {
            if (StepRecovery == PIO_REC_E.RE000_WAIT)
            {
                if (CIMAB.UPHandShakeCancelRequest.vBit == true && CIMAB.UPHandShakeResumeRequest.vBit == true)
                {
                    StepRecovery = PIO_REC_E.RE010_MULTI_REQ_NACK;
                }
                else if (CIMAB.UPHandShakeCancelRequest.vBit == true && CIMAB.UPHandShakeResumeRequest.vBit == false && CIMAB.UPHandShakeAbortRequest.vBit == false)
                {
                    StepRecovery = PIO_REC_E.RE050_CANCEL_START;
                }
                else if (CIMAB.UPHandShakeCancelRequest.vBit == false && CIMAB.UPHandShakeResumeRequest.vBit == true && CIMAB.UPHandShakeAbortRequest.vBit == false)
                {
                    StepRecovery = PIO_REC_E.RE100_RESUME_START;
                }
                else if (CIMAB.UPHandShakeCancelRequest.vBit == false && CIMAB.UPHandShakeResumeRequest.vBit == false && CIMAB.UPHandShakeAbortRequest.vBit == true)
                {
                    StepRecovery = PIO_REC_E.RE200_ABORT_START;
                }
            }

            //MULTI REQUEST NACK
            else if (StepRecovery == PIO_REC_E.RE010_MULTI_REQ_NACK)
            {
                AOIB.LOHandShakeRecoveryNakReply.vBit = true;
            }
            else if (StepRecovery == PIO_REC_E.RE020_MULTI_REQ_NACK_COMPLETE)
            {
                if (CIMAB.UPHandShakeCancelRequest.vBit == false && CIMAB.UPHandShakeResumeRequest.vBit == false)
                {
                    AOIB.LOHandShakeRecoveryNakReply.vBit = false;
                    StepRecovery = PIO_REC_E.RE000_WAIT;
                }
            }

            //CANCEL RECOVERY 
            else if (StepRecovery == PIO_REC_E.RE050_CANCEL_START)
            {
                if (CIMAB.UPHandShakeCancelRequest.vBit == true)
                {
                    AOIB.LOHandShakeRecoveryAckReply.vBit = true;
                    StepRecovery = PIO_REC_E.RE060_CANCEL_PIO_INIT;
                }
            }
            else if (StepRecovery == PIO_REC_E.RE060_CANCEL_PIO_INIT)
            {
                AOIB.LOSendAble.vBit = false;
                AOIB.LOSendStart.vBit = false;
                AOIB.LOSendComplete.vBit = false;
                AOIB.LOExchangeFlag.vBit = false;

                StepRecovery = PIO_REC_E.RE060_CANCEL_COMPLETE_WAIT;
            }
            else if (StepRecovery == PIO_REC_E.RE060_CANCEL_COMPLETE_WAIT)
            {
                if (CIMAB.UPHandShakeCancelRequest.vBit == false )
                {
                    AOIB.LOHandShakeRecoveryAckReply.vBit = false;
                    StepRecovery = PIO_REC_E.RE070_CANCEL_COMPLETE;
                }
            }
            else if (StepRecovery == PIO_REC_E.RE070_CANCEL_COMPLETE)
            {
                if( CIMAB.UPReceiveAble.vBit == false && CIMAB.UPReceiveStart.vBit == false && CIMAB.UPReceiveComplete.vBit == false &&
                    CIMAB.UPExchangeFlag.vBit == false)
                {
                   StepPioExchage = PIO_E.E010_START;
                   StepRecovery = PIO_REC_E.RE000_WAIT;
                   IsRecovery = false;
                }
            }

            //RESUME RECOVERY 
            else if (StepRecovery == PIO_REC_E.RE100_RESUME_START)
            {
                if (CIMAB.UPHandShakeResumeRequest.vBit == true)
                {
                    AOIB.LOHandShakeRecoveryAckReply.vBit = true;
                    StepRecovery = PIO_REC_E.RE110_RESUME_COMPLETE_WAIT;
                }
            }
            else if (StepRecovery == PIO_REC_E.RE110_RESUME_COMPLETE_WAIT)
            {
                if (CIMAB.UPHandShakeResumeRequest.vBit == false)
                {
                    AOIB.LOHandShakeRecoveryAckReply.vBit = false;
                    StepRecovery = PIO_REC_E.RE110_RESUME_COMPLETE;
                }
            }
            else if (StepRecovery == PIO_REC_E.RE110_RESUME_COMPLETE)
            {
                StepRecovery = PIO_REC_E.RE000_WAIT;
                IsRecovery = false;
            }

            //ABORT RECOVERY
            else if (StepRecovery == PIO_REC_E.RE200_ABORT_START)
            {
                if (CIMAB.UPHandShakeAbortRequest.vBit == true)
                {
                    AOIB.LOHandShakeRecoveryAckReply.vBit = true;
                    StepRecovery = PIO_REC_E.RE205_ABORT_PIO_INIT;
                }
            }
            else if (StepRecovery == PIO_REC_E.RE205_ABORT_PIO_INIT)
            {
                AOIB.LOSendAble.vBit = false;
                AOIB.LOSendStart.vBit = false;
                AOIB.LOSendComplete.vBit = false;
                AOIB.LOExchangeFlag.vBit = false;
                StepRecovery = PIO_REC_E.RE210_ABORT_COMPLETE_WAIT;
            }
            else if (StepRecovery == PIO_REC_E.RE210_ABORT_COMPLETE_WAIT)
            {
                if (CIMAB.UPHandShakeAbortRequest.vBit == false)
                {
                    AOIB.LOHandShakeRecoveryAckReply.vBit = false;
                    StepRecovery = PIO_REC_E.RE220_ABORT_COMPLETE;
                }
            }
            else if (StepRecovery == PIO_REC_E.RE220_ABORT_COMPLETE)
            {
                if (CIMAB.UPReceiveAble.vBit == false && CIMAB.UPReceiveStart.vBit == false && CIMAB.UPReceiveComplete.vBit == false &&
                   CIMAB.UPExchangeFlag.vBit == false)
                {
                    StepRecovery = PIO_REC_E.RE000_WAIT;
                    StepPioExchage = PIO_E.E070_PIO_COMPLETE;
                    IsRecoveryAbort = true;
                    IsRecovery = false;
                }
            }
        }

        public bool IsRecoveryAbort { get; set; }
    }
}
