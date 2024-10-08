using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DitCim.PLC;

namespace EquipMainUi.Struct.Step
{
    public enum PIO_S
    {
        S000_WAIT,
        S010_START,
        S020_LO_RECV_ABLE_WAIT,
        S030_LO_RECV_START_WAIT,
        S040_LO_RECV_COMPLETE_WAIT,
        S050_LO_RECV_INIT_WAIT,
        S060_LO_RECV_PIO_COMPLETE,
        S070,
        S080,
        S045_LO_RECV_PIO_TEST,
        S015_GLASS_CHECK,
        S055_LO_GLASS_CHECK,
    }

    public enum PIO_REC_S
    {
        RS000_WAIT,
        RS00_START,
        RS010_MULTI_REQ_NACK,
        RS020_MULTI_REQ_NACK_COMPLETE,
        RS050_CANCEL_START,
        RS060_CANCEL_COMPLETE_WAIT,
        RS070_CANCEL_COMPLETE,
        RS100_RESUME_START,
        RS110_RESUME_COMPLETE_WAIT,
        RS110_RESUME_COMPLETE,
        RS060_CANCEL_PIO_INIT,

    }
    public class PioHsSendStep : StepBase
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

        //메소드 - SEND PIO 
        private bool _isPioSendStop = false;
        private bool _isPioSendComplete = false;
        public bool IsRecovery { get; set; }
        public PIO_S StepPioSend = 0;
        public PIO_REC_S StepRecovery = 0;
        public PlcTimerEx PioTestDelay = new PlcTimerEx("PIO SEND DELAY");

        public bool StartSendPio(Equipment equip)
        {
            if (StepPioSend != 0)
            {
                InterLockMgr.AddInterLock("인터락<실행중>\n(SEND PIO 진행중 시작 명령이 들어왔습니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "SEND PIO 진행중 시작 명령이 들어옴.");
                equip.IsInterlock = true;
                return false;
            }
            IsRecovery = false;
            _isPioSendStop = false;
            _isPioSendComplete = false;
            StepPioSend = PIO_S.S010_START;
            return true;
        }
        public bool StopSendPio(Equipment equip)
        {
            _isPioSendComplete = false;
            StepPioSend = 0;
            return true;
        }
        public bool IsSendComplete()
        {
            return _isPioSendComplete && StepPioSend == 0;
        }

        public override void LogicWorking(Equipment equip)
        {
            //Error Recovery Signal....
            //if (StepPioSend != PIO_S.S000_WAIT && StepPioSend != PIO_S.S010_START && StepPioSend != PIO_S.S015_GLASS_CHECK && StepPioSend != PIO_S.S020_LO_RECV_ABLE_WAIT)
            //    if (!(GG.TestMode || GG.PioTestMode) && 
            //        (equip.HotLinkInx.UPPER_EMERGENCY_STOP_INPUT.vBit == false || CIMAB.LOEmergency.vBit == false))
            //    {
            //        IsRecovery = true;
            //    }

            //if (IsRecovery)
            //    LogicWorkingRecovery(equip);
            //else
                LogicWorkingNormal(equip);
        }

        private PIO_S _oldPioSendStep = PIO_S.S010_START;
        private void LogicWorkingNormal(Equipment equip)
        {
            if (StepPioSend != _oldPioSendStep)
            {
                Logger.Log.AppendLine(LogLevel.AllLog, "[{0,-20}] = {1}", "PIO Recv Normal STEP", StepPioSend.ToString());
                _oldPioSendStep = StepPioSend;
            }

            //PioTimeOutCheck(equip);
            if (StepPioSend == PIO_S.S000_WAIT)
            {
                equip.PioSend.Initailize();
            }
            else if (StepPioSend == PIO_S.S010_START)
            {
                T1_TIME_START = DateTime.MinValue;
                T2_TIME_START = DateTime.MinValue;
                T3_TIME_START = DateTime.MinValue;
                T4_TIME_START = DateTime.MinValue;
                T5_TIME_START = DateTime.MinValue;
                T6_TIME_START = DateTime.MinValue;

                if (equip.IsWaferDetect != EmGlassDetect.NOT || GG.EfemNoWafer == true)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "GLASS UNLOADING PIO 시작");
                    StepPioSend = PIO_S.S015_GLASS_CHECK;
                }
                else
                {
                    equip.IsInterlock = true;
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0726_PIO_GLASS_DETECT_ERROR);
                    Logger.Log.AppendLine(LogLevel.Warning, "인덱서가 반출중 GLASS가 없거나. 센서가 일부 감지됨. ");
                    StepPioSend = PIO_S.S015_GLASS_CHECK;
                }
            }
            else if (StepPioSend == PIO_S.S015_GLASS_CHECK)
            {
                _isPioSendComplete = false;
                equip.PioSend.YSendAble = true;
                StepPioSend = PIO_S.S020_LO_RECV_ABLE_WAIT;
            }
            else if (StepPioSend == PIO_S.S020_LO_RECV_ABLE_WAIT)
            {
                if (equip.PioSend.XRecvAble == true)
                {
                    T1_TIME_START = DateTime.Now;
                    StepPioSend = PIO_S.S030_LO_RECV_START_WAIT;
                }
            }
            else if (StepPioSend == PIO_S.S030_LO_RECV_START_WAIT)
            {
                if (equip.PioSend.XRecvAble == true && equip.PioSend.XRecvStart == true)
                {
                    equip.PioSend.YSendAble = true;
                    equip.PioSend.YSendStart = true;

                    T1_TIME_START = DateTime.MinValue;
                    T2_TIME_START = DateTime.Now;

                    StepPioSend = PIO_S.S040_LO_RECV_COMPLETE_WAIT;
                }
            }
            else if (StepPioSend == PIO_S.S040_LO_RECV_COMPLETE_WAIT)
            {
                if (equip.PioSend.XRecvAble == true && equip.PioSend.XRecvStart == true && equip.PioSend.XRecvComplete == true
                    && equip.IsRobotArmDetect == false)
                {
                    T2_TIME_START = DateTime.MinValue;
                    T3_TIME_START = DateTime.Now;
                    T3_TIME_START = DateTime.MinValue;

                    T4_TIME_START = DateTime.Now;
                    equip.PioSend.YSendAble = true;
                    equip.PioSend.YSendStart = true;
                    equip.PioSend.YSendComplete = true;
                    StepPioSend = PIO_S.S050_LO_RECV_INIT_WAIT;
                }
            }
            else if (StepPioSend == PIO_S.S050_LO_RECV_INIT_WAIT)
            {
                if (equip.PioSend.XRecvAble == false && equip.PioSend.XRecvStart == false && equip.PioSend.XRecvComplete == false)
                {
                    T4_TIME_START = DateTime.MinValue;
                    T5_TIME_START = DateTime.Now;

                    equip.PioSend.Initailize();

                    StepPioSend = PIO_S.S060_LO_RECV_PIO_COMPLETE;
                }
            }            
            else if (StepPioSend == PIO_S.S060_LO_RECV_PIO_COMPLETE)
            {
                if (GG.TestMode == true)
                {
                    equip.WaferDetectSensorLiftpin1.XB_OnOff.vBit = false;
                    equip.WaferDetectSensorLiftpin2.XB_OnOff.vBit = false;
                    equip.WaferDetectSensorStage1.XB_OnOff.vBit = false;
                }
                T5_TIME_START = DateTime.MinValue;
                Logger.Log.AppendLine(LogLevel.Info, "GLASS UNLOADING PIO 완료");
                StepPioSend = PIO_S.S000_WAIT;
                _isPioSendStop = false;
                _isPioSendComplete = true;
            }
        }
        private void LogicWorkingRecovery(Equipment equip)
        {
            //if (StepRecovery == PIO_REC_S.RS000_WAIT)
            //{
            //    if (CIMAB.UPHandShakeCancelRequest.vBit == true && CIMAB.UPHandShakeResumeRequest.vBit == true)
            //    {
            //        StepRecovery = PIO_REC_S.RS010_MULTI_REQ_NACK;
            //    }
            //    else if (CIMAB.UPHandShakeCancelRequest.vBit == true && CIMAB.UPHandShakeResumeRequest.vBit == false)
            //    {
            //        StepRecovery = PIO_REC_S.RS050_CANCEL_START;
            //    }
            //    else if (CIMAB.UPHandShakeCancelRequest.vBit == false && CIMAB.UPHandShakeResumeRequest.vBit == true)
            //    {
            //        StepRecovery = PIO_REC_S.RS100_RESUME_START;
            //    }
            //}

            ////MULTI REQUEST NACK
            //else if (StepRecovery == PIO_REC_S.RS010_MULTI_REQ_NACK)
            //{
            //    AOIB.LOHandShakeRecoveryNakReply.vBit = true;
            //}
            //else if (StepRecovery == PIO_REC_S.RS020_MULTI_REQ_NACK_COMPLETE)
            //{
            //    if (CIMAB.UPHandShakeCancelRequest.vBit == false && CIMAB.UPHandShakeResumeRequest.vBit == false)
            //    {
            //        AOIB.LOHandShakeRecoveryNakReply.vBit = false;
            //        StepRecovery = PIO_REC_S.RS000_WAIT;
            //    }
            //}

            ////CANCEL RECOVERY 
            //else if (StepRecovery == PIO_REC_S.RS050_CANCEL_START)
            //{
            //    if (CIMAB.UPHandShakeCancelRequest.vBit == true)
            //    {
            //        AOIB.LOHandShakeRecoveryAckReply.vBit = true;
            //        StepRecovery = PIO_REC_S.RS060_CANCEL_PIO_INIT;
            //    }
            //}
            //else if (StepRecovery == PIO_REC_S.RS060_CANCEL_PIO_INIT)
            //{
            //    AOIB.LOSendAble.vBit = false;
            //    AOIB.LOSendStart.vBit = false;
            //    AOIB.LOSendComplete.vBit = false;
            //    AOIB.LOExchangeFlag.vBit = false;

            //    StepRecovery = PIO_REC_S.RS060_CANCEL_COMPLETE_WAIT;
            //}
            //else if (StepRecovery == PIO_REC_S.RS060_CANCEL_COMPLETE_WAIT)
            //{
            //    if (CIMAB.UPHandShakeCancelRequest.vBit == false)
            //    {
            //        AOIB.LOHandShakeRecoveryAckReply.vBit = false;
            //        StepRecovery = PIO_REC_S.RS070_CANCEL_COMPLETE;
            //    }
            //}
            //else if (StepRecovery == PIO_REC_S.RS070_CANCEL_COMPLETE)
            //{
            //    if (CIMAB.UPReceiveAble.vBit == false && CIMAB.UPReceiveStart.vBit == false && CIMAB.UPReceiveComplete.vBit == false &&
            //        CIMAB.UPExchangeFlag.vBit == false)
            //    {
            //        StepPioSend = PIO_S.S010_START;
            //        StepRecovery = PIO_REC_S.RS000_WAIT;
            //        IsRecovery = false;
            //    }
            //}

            ////RESUME RECOVERY 
            //else if (StepRecovery == PIO_REC_S.RS100_RESUME_START)
            //{
            //    if (CIMAB.UPHandShakeResumeRequest.vBit == true)
            //    {
            //        AOIB.LOHandShakeRecoveryAckReply.vBit = true;
            //        StepRecovery = PIO_REC_S.RS110_RESUME_COMPLETE_WAIT;
            //    }
            //}
            //else if (StepRecovery == PIO_REC_S.RS110_RESUME_COMPLETE_WAIT)
            //{
            //    if (CIMAB.UPHandShakeResumeRequest.vBit == false)
            //    {
            //        AOIB.LOHandShakeRecoveryAckReply.vBit = false;
            //        StepRecovery = PIO_REC_S.RS110_RESUME_COMPLETE;
            //    }
            //}
            //else if (StepRecovery == PIO_REC_S.RS110_RESUME_COMPLETE)
            //{
            //    StepRecovery = PIO_REC_S.RS000_WAIT;
            //    IsRecovery = false;
            //}
        }
    }
}
