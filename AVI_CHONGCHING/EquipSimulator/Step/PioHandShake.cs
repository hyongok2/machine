using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using EquipSimulator.Log;
using Dit.Framework.Log;
using EquipSimulator;

namespace EquipMainUi.Struct
{
    public class PioHandShake
    {
        #region 
        //HAND SHAKE TO UPPER
        public PlcAddr LoHeartBit;
        public PlcAddr LoMachinePause;
        public PlcAddr LoMachineDown;
        public PlcAddr LoMachineAlarm;
        public PlcAddr LoReceiveAble;
        public PlcAddr LoReceiveStart;
        public PlcAddr LoReceiveComplete;
        public PlcAddr LoExchangeFlag;
        public PlcAddr LoReturnSendStart;
        public PlcAddr LoReturnSendComplete;
        public PlcAddr LoImmediatelyPauseRequest;
        public PlcAddr LoImmediatelyStopRequest;
        public PlcAddr LoReceiveAbleRemainedStep1;
        public PlcAddr LoReceiveAbleRemainedStep2;
        public PlcAddr LoReceiveAbleRemainedStep3;
        public PlcAddr LoReceiveAbleRemainedStep4;
        public PlcAddr LoGlassIDReadComplete;
        public PlcAddr LoLoadingStop;
        public PlcAddr LoTransferStop;
        public PlcAddr LoExchangeFailFlag;
        public PlcAddr LoProcessTimeUp;
        public PlcAddr LoReserved1;
        public PlcAddr LoReserved2;
        public PlcAddr LoReceiveAbleReserveRequest;
        public PlcAddr LoHandShakeCancelRequest;
        public PlcAddr LoHandShakeAbortRequest;
        public PlcAddr LoHandShakeResumeRequest;
        public PlcAddr LoHandShakeRecoveryAckReply;
        public PlcAddr LoHandShakeRecoveryNakReply;
        public PlcAddr LoReceiveJobReady;
        public PlcAddr LoReceiveActionMove;
        public PlcAddr LoReceiveActionRemove;
        //Upper	Contact	Point	                     ;
        public PlcAddr LoAbnormal;
        public PlcAddr LoTypeofArm;
        public PlcAddr LoTypeofStageConveyor;
        public PlcAddr LoArmStretchUpMoving;
        public PlcAddr LoArmStretchUpComplete;
        public PlcAddr LoArmStretchDownMoving;
        public PlcAddr LoArmStretchDownComplete;
        public PlcAddr LoArmStretching;
        public PlcAddr LoArmStretchComplete;
        public PlcAddr LoArmFolding;
        public PlcAddr LoArmFoldComplete;
        public PlcAddr LoCpReserved1;
        public PlcAddr LoCpReserved2;
        public PlcAddr LoArm1Folded;
        public PlcAddr LoArm2Folded;
        public PlcAddr LoArm1GlassDetect;
        public PlcAddr LoArm2GlassDetect;
        public PlcAddr LoArm1GlassVacuum;
        public PlcAddr LoArm2GlassVacuum;
        public PlcAddr LoRobotDirection;
        public PlcAddr LoManualOperation;
        public PlcAddr LoPinUp;
        public PlcAddr LoPinDown;
        public PlcAddr LoDoorOpen;
        public PlcAddr LoDoorClose;
        public PlcAddr LoGlassDetect;
        public PlcAddr LoBodyMoving;
        public PlcAddr LoBodyOriginPosition;
        public PlcAddr LoEmergency;
        public PlcAddr LoVertical;
        public PlcAddr LoHorizontal;
        #endregion                                   
        #region
        //Lower	EQ						             ;
        public PlcAddr UpHeartBit;
        public PlcAddr UpMachinePause;
        public PlcAddr UpMachineDown;
        public PlcAddr UpMachineAlarm;
        public PlcAddr UpSendAble;
        public PlcAddr UpSendStart;
        public PlcAddr UpSendComplete;
        public PlcAddr UpExchangeFlag;
        public PlcAddr UpReturnReceiveStart;
        public PlcAddr UpReturnReceiveComplete;
        public PlcAddr UpImmediatelyPauseRequest;
        public PlcAddr UpImmediatelyStopRequest;
        public PlcAddr UpSendAbleRemainedStep1;
        public PlcAddr UpSendAbleRemainedStep2;
        public PlcAddr UpSendAbleRemainedStep3;
        public PlcAddr UpSendAbleRemainedStep4;
        public PlcAddr UpWorkStart;
        public PlcAddr UpWorkCancel;
        public PlcAddr UpWorkSkip;
        public PlcAddr UpJobStart;
        public PlcAddr UpJobEnd;
        public PlcAddr UpHotFlow;
        public PlcAddr UpReserved;
        public PlcAddr UpSendAbleReserveRequest;
        public PlcAddr UpHandShakeCancelRequest;
        public PlcAddr UpHandShakeAbortRequest;
        public PlcAddr UpHandShakeResumeRequest;
        public PlcAddr UpHandShakeRecoveryAckReply;
        public PlcAddr UpHandShakeRecoveryNakReply;
        public PlcAddr UpSendJobReady;
        public PlcAddr UpSendActionMove;
        public PlcAddr UpSendActionRemove;
        //Lower	Contact	Point		                 ;
        public PlcAddr UpAbnormal;
        public PlcAddr UpTypeofArm;
        public PlcAddr UpTypeofStageConveyor;
        public PlcAddr UpArmStretchUpMoving;
        public PlcAddr UpArmStretchUpComplete;
        public PlcAddr UpArmStretchDownMoving;
        public PlcAddr UpArmStretchDownComplete;
        public PlcAddr UpArmStretching;
        public PlcAddr UpArmStretchComplete;
        public PlcAddr UpArmFolding;
        public PlcAddr UpArmFoldComplete;
        public PlcAddr UpCpReserved1;
        public PlcAddr UpCpReserved2;
        public PlcAddr UpArm1Folded;
        public PlcAddr UpArm2Folded;
        public PlcAddr UpArm1GlassDetect;
        public PlcAddr UpArm2GlassDetect;
        public PlcAddr UpArm1GlassVacuum;
        public PlcAddr UpArm2GlassVacuum;
        public PlcAddr UpRobotDirection;
        public PlcAddr UpManualOperation;
        public PlcAddr UpPinUp;
        public PlcAddr UpPinDown;
        public PlcAddr UpDoorOpen;
        public PlcAddr UpDoorClose;
        public PlcAddr UpGlassDetect;
        public PlcAddr UpBodyMoving;
        public PlcAddr UpBodyOriginPosition;
        public PlcAddr UpEmergency;
        public PlcAddr UpVertical;
        public PlcAddr UpHorizontal;
        #endregion
        public static PlcAddr AOI_AREA = new PlcAddr(PlcMemType.S, 0, 0, 10000, PlcValueType.SHORT);
        public static PlcAddr CIM_AREA = new PlcAddr(PlcMemType.S, 10000, 0, 10000, PlcValueType.SHORT);

        public EquipSimulator.GG.EmGlassType InputGlsType { get; set; }

        private bool _isAoiToIndexComplete = false;
        public void StartAoiToIndex()
        {
            _isAoiToIndexComplete = false;
            StepPioSend = 10;
        }
        public bool IsAoiToIndexComplete()
        {
            return _isAoiToIndexComplete && StepPioSend == 0;
        }

        private bool _isIndexToAoiComplete = false;
        public void StartIndexToAoi()
        {


            //Glass 감지 OFF, Auto Mode ON, Heavy Alarm OFF, Pause OFF
            //Cycle Stop OFF
            _isIndexToAoiComplete = false;
            StepPioRecv = 10;

        }
        public bool IsIndexToAoiComplete()
        {
            return _isIndexToAoiComplete && StepPioRecv == 0;
        }

        private bool _isAoiToIndexExchangeComplete = false;
        public void StartAoiToIndexExchange()
        {
            _isAoiToIndexExchangeComplete = false;
            StepPioAoiToIndexExchage = 10;
        }
        public bool IsAoiToIndexExchangeComplete()
        {
            return _isAoiToIndexExchangeComplete && StepPioAoiToIndexExchage == 0;
        }
        public int StepPioSend = 0;
        private PlcTimer UpperToLowerDelay = new PlcTimer();
        public void LogPioSend(EquipSimul equip)
        {
            //A --> I
            if (StepPioSend == 0)
            {
                
            }
            else if (StepPioSend == 10)
            {
                //if (equip.IsGlassCheckOk)
                StepPioSend = 20;
            }
            else if (StepPioSend == 20)
            {
                if (LoReceiveAble == true && LoExchangeFailFlag == false)
                {
                    UpSendAble.vBit = true;
                    StepPioSend = 30;
                }
            }
            else if (StepPioSend == 30)
            {
                if (LoReceiveAble == true && LoReceiveStart == true)
                {
                    UpSendAble.vBit = true;
                    UpSendStart.vBit = true;
                    StepPioSend = 40;
                }
            }
            else if (StepPioSend == 40)
            {
                if (LoReceiveAble == true && LoReceiveStart == true)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "인덱서 이동 시작");
                    UpperToLowerDelay.Start(2);
                    StepPioSend = 41;
                }
            }
            else if (StepPioSend == 41)
            {
                if (LoReceiveAble == true && LoReceiveStart == true && UpperToLowerDelay)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "인덱서 설비 진입 시작");
                    equip.ROBOT_ARM_DETECT.OnOff(false);
                    UpperToLowerDelay.Stop();
                    UpperToLowerDelay.Start(2);
                    StepPioSend = 42;
                }
            }
            else if (StepPioSend == 42)
            {
                if (LoReceiveAble == true && LoReceiveStart == true && UpperToLowerDelay)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "인덱서 설비 글라스 투입 TYPE : {0}", InputGlsType.ToString());

                    equip.ROBOT_ARM_DETECT.OnOff(false);

                    switch (InputGlsType)
                    {
                        case EquipSimulator.GG.EmGlassType.ORIGIN:
                            equip.GLASS_DETECT_SENSOR_1.OnOff(true);
                            equip.GLASS_DETECT_SENSOR_2.OnOff(false);
                            equip.GLASS_DETECT_SENSOR_3.OnOff(true);

                            equip.GLASS_EDGE_DETECT_SENSOR_1.OnOff(false);
                            equip.GLASS_EDGE_DETECT_SENSOR_2.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_3.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_4.OnOff(false);
                            equip.GLASS_EDGE_DETECT_SENSOR_5.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_6.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_7.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_8.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_9.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_10.OnOff(true);
                            break;
                        case EquipSimulator.GG.EmGlassType.SEPARATION:
                            equip.GLASS_DETECT_SENSOR_1.OnOff(true);
                            equip.GLASS_DETECT_SENSOR_2.OnOff(true);
                            equip.GLASS_DETECT_SENSOR_3.OnOff(false);

                            equip.GLASS_EDGE_DETECT_SENSOR_1.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_2.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_3.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_4.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_5.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_6.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_7.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_8.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_9.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_10.OnOff(true);
                            break;
                        case EquipSimulator.GG.EmGlassType.LEFTONLY:
                            equip.GLASS_DETECT_SENSOR_1.OnOff(true);
                            equip.GLASS_DETECT_SENSOR_2.OnOff(false);
                            equip.GLASS_DETECT_SENSOR_3.OnOff(false);

                            equip.GLASS_EDGE_DETECT_SENSOR_1.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_2.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_3.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_4.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_5.OnOff(false);
                            equip.GLASS_EDGE_DETECT_SENSOR_6.OnOff(false);
                            equip.GLASS_EDGE_DETECT_SENSOR_7.OnOff(false);
                            equip.GLASS_EDGE_DETECT_SENSOR_8.OnOff(false);
                            equip.GLASS_EDGE_DETECT_SENSOR_9.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_10.OnOff(true);
                            break;
                        case EquipSimulator.GG.EmGlassType.RIGHTONLY:
                            equip.GLASS_DETECT_SENSOR_1.OnOff(false);
                            equip.GLASS_DETECT_SENSOR_2.OnOff(true);
                            equip.GLASS_DETECT_SENSOR_3.OnOff(false);

                            equip.GLASS_EDGE_DETECT_SENSOR_1.OnOff(false);
                            equip.GLASS_EDGE_DETECT_SENSOR_2.OnOff(false);
                            equip.GLASS_EDGE_DETECT_SENSOR_3.OnOff(false);
                            equip.GLASS_EDGE_DETECT_SENSOR_4.OnOff(false);
                            equip.GLASS_EDGE_DETECT_SENSOR_5.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_6.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_7.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_8.OnOff(true);
                            equip.GLASS_EDGE_DETECT_SENSOR_9.OnOff(false);
                            equip.GLASS_EDGE_DETECT_SENSOR_10.OnOff(false);
                            break;
                    }

                    UpperToLowerDelay.Stop();
                    UpperToLowerDelay.Start(3);
                    StepPioSend = 50;
                }
            }
            else if (StepPioSend == 50)
            {
                if (LoReceiveAble == true && LoReceiveStart == true && UpperToLowerDelay)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "인덱서 설비 진입 종료");
                    equip.ROBOT_ARM_DETECT.OnOff(true);
                    UpperToLowerDelay.Stop();
                    UpSendComplete.vBit = true;
                    StepPioSend = 60;
                }
            }
            else if (StepPioSend == 60)
            {
                if (LoReceiveAble == true && LoReceiveStart == true && LoReceiveComplete == true)
                {
                    UpSendAble.vBit = false;
                    UpSendStart.vBit = false;
                    UpSendComplete.vBit = false;

                    StepPioSend = 70;
                }
            }
            else if (StepPioSend == 70)
            {
                if (LoReceiveAble == false && LoReceiveStart == false && LoReceiveComplete == false)
                {
                    StepPioSend = 80;
                }
            }
            else if (StepPioSend == 80)
            {
                //Logger.Log.AppendLine(LogLevel.Info, "Glass UnLoading");
                //equip.ROBOT_ARM_DETECT.OnOff(true);
                StepPioSend = 0;               
            }

        }




        public int StepPioRecv = 0;
        private PlcTimer IndexToAoiDelay = new PlcTimer();
        public void LogPioRecv(EquipSimul equip)
        {
            if (StepPioRecv == 0)
            {
            }
            else if (StepPioRecv == 10)
            {
                StepPioRecv = 20;
            }
            else if (StepPioRecv == 20)
            {
                if (UpSendAble == true)
                {
                    LoReceiveAble.vBit = true;
                    StepPioRecv = 30;
                }
            }
            else if (StepPioRecv == 30)
            {
                if (UpSendAble == true)
                {
                    LoReceiveAble.vBit = true;
                    IndexToAoiDelay.Start(2);
                    StepPioRecv = 35;
                }
            }
            else if (StepPioRecv == 35)
            {
                if (UpSendAble == true && IndexToAoiDelay)
                {
                    IndexToAoiDelay.Stop();
                    LoReceiveAble.vBit = true;
                    LoReceiveStart.vBit = true;
                    Logger.Log.AppendLine(LogLevel.Info, "인덱서 이동 시작");
                    StepPioRecv = 40;
                }

            }
            else if (StepPioRecv == 40)
            {
                if (UpSendAble == true && UpSendStart == true  )
                {   
                    LoReceiveAble.vBit = true;
                    LoReceiveStart.vBit = true;
                    Logger.Log.AppendLine(LogLevel.Info, "인덱서 이동 대기");
                    IndexToAoiDelay.Start(3);
                    StepPioRecv = 50;
                }
            }
            else if (StepPioRecv == 50)
            {
                if (UpSendAble == true && UpSendStart == true && IndexToAoiDelay)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "인덱서 설비 진입 시작");
                    equip.ROBOT_ARM_DETECT.OnOff(false);
                    IndexToAoiDelay.Stop();
                    IndexToAoiDelay.Start(2);
                    StepPioRecv = 60;
                }
            }
            else if (StepPioRecv == 60)
            {
                if (UpSendAble == true && UpSendStart == true && IndexToAoiDelay)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "인덱서 설비 진입 시작");
                    equip.ROBOT_ARM_DETECT.OnOff(false);
                    IndexToAoiDelay.Stop();
                    IndexToAoiDelay.Start(2);
                    StepPioRecv = 70;
                }
            }
            else if (StepPioRecv == 70)
            {
                if (UpSendAble == true && UpSendStart == true && IndexToAoiDelay)
                {
                    IndexToAoiDelay.Stop();
                    equip.GLASS_DETECT_SENSOR_1.OnOff(false);
                    equip.GLASS_DETECT_SENSOR_2.OnOff(false);

                    equip.GLASS_EDGE_DETECT_SENSOR_1.OnOff(false);
                    equip.GLASS_EDGE_DETECT_SENSOR_2.OnOff(false);
                    equip.GLASS_EDGE_DETECT_SENSOR_3.OnOff(false);
                    equip.GLASS_EDGE_DETECT_SENSOR_4.OnOff(false);
                    equip.GLASS_EDGE_DETECT_SENSOR_5.OnOff(false);
                    equip.GLASS_EDGE_DETECT_SENSOR_6.OnOff(false);
                    equip.GLASS_EDGE_DETECT_SENSOR_7.OnOff(false);
                    equip.GLASS_EDGE_DETECT_SENSOR_8.OnOff(false);
                    equip.GLASS_EDGE_DETECT_SENSOR_9.OnOff(false);
                    equip.GLASS_EDGE_DETECT_SENSOR_10.OnOff(false);

                    IndexToAoiDelay.Start(2);
                    StepPioRecv = 75;
                }
            }
            else if (StepPioRecv == 75)
            {
                if (UpSendAble == true && UpSendStart == true && IndexToAoiDelay)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "인덱서 설비 진입 종료");
                    equip.ROBOT_ARM_DETECT.OnOff(true);
                    
                    LoReceiveAble.vBit = true;
                    LoReceiveStart.vBit = true;
                    LoReceiveComplete.vBit = true;
                    IndexToAoiDelay.Stop();
                    IndexToAoiDelay.Start(2);
                    StepPioRecv = 80;
                }
            }
            else if (StepPioRecv == 80)
            {
                if (UpSendAble == true && UpSendStart == true && UpSendComplete == true && IndexToAoiDelay)
                {
                    IndexToAoiDelay.Stop();

                    LoReceiveAble.vBit = false;
                    LoReceiveStart.vBit = false;
                    LoReceiveComplete.vBit = false;

                    Logger.Log.AppendLine(LogLevel.Info, "인덱서 이동 종료");
                    StepPioRecv = 90;
                }
            }
            else if (StepPioRecv == 90)
            {
                if (UpSendAble == false && UpSendStart == false && UpSendComplete == false && IndexToAoiDelay)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "인덱서 이동 종료");

                    StepPioRecv = 100;
                }
            }


            else if (StepPioRecv == 100)
            {
                //Logger.Log.AppendLine(LogLevel.Info, "Glass Loading");
                StepPioRecv = 0;
            }

        }

        public int StepPioAoiToIndexExchage = 0;
        private PlcTimer ExchangeDelay = new PlcTimer();
        public void LogPioExchangeStep()
        {
            //A --> I
            if (StepPioAoiToIndexExchage == 0)
            {
            }
            else if (StepPioAoiToIndexExchage == 10)
            {
                //if (equip.IsGlassCheckOk)
                StepPioSend = 20;
            }
            else if (StepPioSend == 20)
            {
                if (LoReceiveAble == true && LoExchangeFailFlag == true)
                {
                    UpSendAble.vBit = true;
                    StepPioSend = 30;
                }
            }
            else if (StepPioSend == 30)
            {
                if (LoReceiveAble == true && LoExchangeFailFlag == true)
                {
                    UpExchangeFlag.vBit = true;
                    UpSendAble.vBit = true;
                    UpSendStart.vBit = true;

                    StepPioSend = 40;
                }
            }
            else if (StepPioSend == 40)
            {
                if (LoReceiveAble == true && LoReceiveStart == true && LoExchangeFailFlag == true)
                {
                    UpExchangeFlag.vBit = true;
                    UpSendAble.vBit = true;
                    UpSendStart.vBit = true;

                    StepPioSend = 50;
                }
            }
            else if (StepPioSend == 50)
            {
                if (LoReceiveAble == true && LoReceiveStart == true && LoReceiveComplete == true &&
                    LoExchangeFailFlag == true)
                {
                    UpExchangeFlag.vBit = true;
                    UpSendAble.vBit = true;
                    UpSendStart.vBit = true;

                    ExchangeDelay.Start(0, 100);
                    StepPioSend = 60;
                }
            }
            else if (StepPioSend == 60)
            {
                if (LoReceiveAble == true && LoReceiveStart == true && LoReceiveComplete == true)
                {
                    UpExchangeFlag.vBit = true;
                    UpSendAble.vBit = true;
                    UpSendStart.vBit = true;
                    UpSendComplete.vBit = true;
                    StepPioSend = 70;
                }
            }
            else if (StepPioSend == 60)
            {

            }
        }


    }
}
