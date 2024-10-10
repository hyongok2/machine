using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using EquipMainUi.Struct.Detail;
using DitCim.PLC;
using EquipMainUi.Struct.Step;

namespace EquipMainUi.Struct
{
    public enum PIO_R
    {
        R000_WAIT,
        R010_START,
        R020_UP_SEND_ABLE_WAIT,
        R030_UP_SEND_START_WAIT,
        R040_UP_SEND_COMPLETE_WAIT,
        R050_ROBOT_ARM_CHECK,
        R060_GLASS_CHECK,
        R070_LO_PIO_INIT,
        R080_PIO_COMPLETE,
        R075_PIO_GLASS_CHECK,
    }
    public enum PIO_REC_R
    {
        RR000_WAIT,
        RR00_START,
        RR010_MULTI_REQ_NACK,
        RR020_MULTI_REQ_NACK_COMPLETE,
        RR050_CANCEL_START,
        RR060_CANCEL_PIO_INIT,
        RR060_CANCEL_COMPLETE_WAIT,
        RR070_CANCEL_COMPLETE,

        RR100_RESUME_START,
        RR110_RESUME_COMPLETE_WAIT,
        RR110_RESUME_COMPLETE,
    }
    public class PioHsRecvStep : StepBase
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

        //메소드 - RECV PIO  
        private bool _isPioRecvComplete = false;
        public bool IsRecovery { get; set; }
        public PIO_R PrevPioStep = 0;
        public PIO_R StepPioRecv = 0;
        public PIO_REC_R StepRecovery = 0;
        public PlcTimerEx RecvDelay = new PlcTimerEx("PIO RECV DELAY");

        public bool StartPioRecv(Equipment equip)
        {
            if (StepPioRecv != 0 || equip.PioRecv.YRecvStart == true || equip.PioRecv.YRecvComplete == true)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<执行中>\n(RECEIVE PIO进行中，介入开始命令.)" : "인터락<실행중>\n(RECEIVE PIO 진행중 시작 명령이 들어왔습니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "RECV PIO 진행중 시작 명령이 들어옴.");
                equip.IsInterlock = true;
                return false;
            }

            //Glass 감지 OFF, Auto Mode ON, Heavy Alarm OFF, Pause OFF
            //Cycle Stop OFF
            IsRecovery = false;
            _isPioRecvComplete = false;
            StepPioRecv = PIO_R.R010_START;

            return true;
        }
        public void InitRecvPio()
        {
            //LoReceiveAble.vBit = false;
            //LoReceiveStart.vBit = false;
            //LoReceiveComplete.vBit = false;
        }
        public bool StopRecvPio(Equipment equip)
        {
            _isPioRecvComplete = false;
            StepPioRecv = 0;
            return true;
        }
        public bool IsPioReciveComplete()
        {
            //임시 swChae

            //if (GG.TestMode == true)
            //{
            //    StepPioRecv = 0;
            //    return true;
            //}

            return _isPioRecvComplete && StepPioRecv == 0;
        }


        public override void LogicWorking(Equipment equip)
        {
            //Error Recovery Signal....
            //if (StepPioRecv != PIO_R.R000_WAIT && StepPioRecv != PIO_R.R010_START && StepPioRecv != PIO_R.R020_UP_SEND_ABLE_WAIT)
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

        private PIO_R _oldPioRecvStep = PIO_R.R010_START;
        public void LogicWorkingNormal(Equipment equip)
        {
            if (StepPioRecv != _oldPioRecvStep)
            {
                Logger.Log.AppendLine(LogLevel.AllLog, "[{0,-20}] = {1}", "PIO Recv Normal STEP", StepPioRecv.ToString());
                _oldPioRecvStep = StepPioRecv;
            }

            //PioTimeOutCheck(equip);
            if (StepPioRecv == PIO_R.R000_WAIT)
            {
            }
            else if (StepPioRecv == PIO_R.R010_START)
            {
                T1_TIME_START = DateTime.MinValue;
                T2_TIME_START = DateTime.MinValue;
                T3_TIME_START = DateTime.MinValue;
                T4_TIME_START = DateTime.MinValue;
                T5_TIME_START = DateTime.MinValue;
                T6_TIME_START = DateTime.MinValue;

                if ((equip.IsWaferDetect == EmGlassDetect.NOT
                    && equip.IsHeavyAlarm == false
                    && equip.IsPause == false)
                    || GG.EfemNoWafer == true
                    )
                {
                    Logger.Log.AppendLine(LogLevel.Info, "GLASS LOADING PIO 시작");
                    equip.PioRecv.YRecvAble = true;
                    StepPioRecv = PIO_R.R020_UP_SEND_ABLE_WAIT;
                }
            }
            else if (StepPioRecv == PIO_R.R020_UP_SEND_ABLE_WAIT)
            {
                if (equip.PioRecv.XSendAble == true)
                {                  
                    T1_TIME_START = DateTime.Now;
                    StepPioRecv = PIO_R.R030_UP_SEND_START_WAIT;
                }
            }
            else if (StepPioRecv == PIO_R.R030_UP_SEND_START_WAIT)
            {
                if (equip.PioRecv.XSendAble == true && equip.PioRecv.XSendStart == true)                    
                {
                    T1_TIME_START = DateTime.MinValue;
                    T2_TIME_START = DateTime.Now;
                    equip.PioRecv.YRecvAble = true;
                    equip.PioRecv.YRecvStart = true;
                    StepPioRecv = PIO_R.R040_UP_SEND_COMPLETE_WAIT;

                    /////////////////////
                    // ROBOT 투입 시점 //
                    ////////////////////
                    
                    if (GG.PioTestMode == true)
                    {
                        RecvDelay.Start(3);
                    }
                }
            }
            else if (StepPioRecv == PIO_R.R040_UP_SEND_COMPLETE_WAIT)
            {
                if (equip.PioRecv.XSendAble == true && equip.PioRecv.XSendStart == true && equip.PioRecv.XSendComplete == true
                    && equip.IsRobotArmDetect == false                    
                    )
                {
                    T2_TIME_START = DateTime.MinValue;
                    T3_TIME_START = DateTime.Now;

                    /////////////////////
                    // ROBOT 아웃 시점 //
                    ////////////////////

                    RecvDelay.Start(0, 100);
                    StepPioRecv = PIO_R.R060_GLASS_CHECK;
                }
            }
            else if (StepPioRecv == PIO_R.R060_GLASS_CHECK)
            {
                if (RecvDelay)
                {
                    RecvDelay.Stop();
                    T3_TIME_START = DateTime.MinValue;
                    T4_TIME_START = DateTime.Now;

                    equip.PioRecv.YRecvAble = true;
                    equip.PioRecv.YRecvStart = true;
                    equip.PioRecv.YRecvComplete = true;                    

                    StepPioRecv = PIO_R.R070_LO_PIO_INIT;
                }
            }
            else if (StepPioRecv == PIO_R.R070_LO_PIO_INIT)
            {
                if (equip.PioRecv.XSendAble == false && equip.PioRecv.XSendStart == false && equip.PioRecv.XSendComplete == false)
                {
                    T4_TIME_START = DateTime.MinValue;
                    T5_TIME_START = DateTime.Now;
                    equip.PioRecv.YRecvAble = false;
                    equip.PioRecv.YRecvStart = false;
                    equip.PioRecv.YRecvComplete = false;

                    StepPioRecv = PIO_R.R080_PIO_COMPLETE;
                }
            }
            else if (StepPioRecv == PIO_R.R080_PIO_COMPLETE)
            {   
                if (GG.TestMode == true)
                {
                    equip.WaferDetectSensorLiftpin1.XB_OnOff.vBit = true;
                    equip.WaferDetectSensorLiftpin2.XB_OnOff.vBit = true;
                    equip.WaferDetectSensorStage1.XB_OnOff.vBit = true;
                }

                T5_TIME_START = DateTime.MinValue;
                StepPioRecv = PIO_R.R000_WAIT;
                _isPioRecvComplete = true;
                Logger.Log.AppendLine(LogLevel.Info, "GLASS LOADING PIO 완료");
                
            }
        }
        private void LogicWorkingRecovery(Equipment equip)
        {
            //if (StepRecovery == PIO_REC_R.RR000_WAIT)
            //{
            //    if (CIMAB.LOHandShakeCancelRequest.vBit == true && CIMAB.LOHandShakeResumeRequest.vBit == true)
            //    {
            //        StepRecovery = PIO_REC_R.RR010_MULTI_REQ_NACK;
            //    }
            //    else if (CIMAB.LOHandShakeCancelRequest.vBit == true && CIMAB.LOHandShakeResumeRequest.vBit == false)
            //    {
            //        StepRecovery = PIO_REC_R.RR050_CANCEL_START;
            //    }
            //    else if (CIMAB.LOHandShakeCancelRequest.vBit == false && CIMAB.LOHandShakeResumeRequest.vBit == true)
            //    {
            //        StepRecovery = PIO_REC_R.RR100_RESUME_START;
            //    }
            //}

            ////MULTI REQUEST NACK
            //else if (StepRecovery == PIO_REC_R.RR010_MULTI_REQ_NACK)
            //{
            //    AOIB.UPHandShakeRecoveryNakReply.vBit = true;
            //}
            //else if (StepRecovery == PIO_REC_R.RR020_MULTI_REQ_NACK_COMPLETE)
            //{
            //    if (CIMAB.LOHandShakeCancelRequest.vBit == false && CIMAB.LOHandShakeResumeRequest.vBit == false)
            //    {
            //        AOIB.UPHandShakeRecoveryNakReply.vBit = false;
            //        StepRecovery = PIO_REC_R.RR000_WAIT;
            //    }
            //}

            ////CANCEL RECOVERY 
            //else if (StepRecovery == PIO_REC_R.RR050_CANCEL_START)
            //{
            //    if (CIMAB.LOHandShakeCancelRequest.vBit == true)
            //    {
            //        AOIB.UPHandShakeRecoveryAckReply.vBit = true;
            //        StepRecovery = PIO_REC_R.RR060_CANCEL_PIO_INIT;
            //    }
            //}
            //else if (StepRecovery == PIO_REC_R.RR060_CANCEL_PIO_INIT)
            //{
            //    AOIB.UPReceiveAble.vBit = false;
            //    AOIB.UPReceiveStart.vBit = false;
            //    AOIB.UPReceiveComplete.vBit = false;
            //    AOIB.UPExchangeFlag.vBit = false;

            //    StepRecovery = PIO_REC_R.RR060_CANCEL_COMPLETE_WAIT;
            //}
            //else if (StepRecovery == PIO_REC_R.RR060_CANCEL_COMPLETE_WAIT)
            //{
            //    if (CIMAB.LOHandShakeCancelRequest.vBit == false)
            //    {
            //        AOIB.UPHandShakeRecoveryAckReply.vBit = false;
            //        StepRecovery = PIO_REC_R.RR070_CANCEL_COMPLETE;
            //    }
            //}
            //else if (StepRecovery == PIO_REC_R.RR070_CANCEL_COMPLETE)
            //{
            //    if (
            //        CIMAB.LOSendAble.vBit == false && CIMAB.LOSendStart.vBit == false && CIMAB.LOSendComplete.vBit == false &&
            //        CIMAB.LOExchangeFlag.vBit == false)
            //    {
            //        StepPioRecv = PIO_R.R010_START;
            //        StepRecovery = PIO_REC_R.RR000_WAIT;
            //        IsRecovery = false;
            //    }
            //}

            ////RESUME RECOVERY 
            //else if (StepRecovery == PIO_REC_R.RR100_RESUME_START)
            //{
            //    if (CIMAB.LOHandShakeResumeRequest.vBit == true)
            //    {
            //        AOIB.UPHandShakeRecoveryAckReply.vBit = true;
            //        StepRecovery = PIO_REC_R.RR110_RESUME_COMPLETE_WAIT;
            //    }
            //}
            //else if (StepRecovery == PIO_REC_R.RR110_RESUME_COMPLETE_WAIT)
            //{
            //    if (CIMAB.LOHandShakeResumeRequest.vBit == false)
            //    {
            //        AOIB.UPHandShakeRecoveryAckReply.vBit = false;
            //        StepRecovery = PIO_REC_R.RR110_RESUME_COMPLETE;
            //    }
            //}
            //else if (StepRecovery == PIO_REC_R.RR110_RESUME_COMPLETE)
            //{
            //    StepRecovery = PIO_REC_R.RR000_WAIT;
            //    IsRecovery = false;
            //}
        }
    }
}