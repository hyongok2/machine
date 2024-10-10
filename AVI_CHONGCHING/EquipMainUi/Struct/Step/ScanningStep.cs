using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Struct.Detail;
using EquipMainUi.Monitor;

using EquipMainUi.Struct.Detail.PC;
using EquipMainUi.Setting;

namespace EquipMainUi.Struct.Step
{
    public enum EmSC_NO
    {
        S000_SCAN_WAIT,

        S010_SCAN_START,
        S020_SCAN_START_ACK_WAIT,
        S030_SCAN_COMPLETE_WAIT,

        S040_SCAN_STOP,
        S050_INSP_TIME_OVER,

        S060_PMAC_SCAN_STOP_WAIT,
        S070_SCAN_COMPLETE,
    }
    public class ScanningStep : StepBase
    {
        //Loading Step 로직 처리. 
        private bool _isComplete = false;
        private EmSC_NO _stepNum = 0;
        private EmSC_NO _stepNumOld = 0;
        public EmSC_NO StepNum { get { return _stepNum; } }
        private PlcTimerEx _tmrCreateResultFileCompleteTimeOver = new PlcTimerEx("Result File Create Complete Time Over");
        private PlcTimerEx _tmrInspTimeover = new PlcTimerEx("Inspection Time Over");
        private PlcTimerEx _tmrMotorStopWait = new PlcTimerEx("Motor Stop Wait");
        public long ScanPassTime { get { return _tmrInspTimeover.PassTime; } }

        private int _scanIndex = 0;
        public int ScanIndex { get { return _scanIndex; } }

        public bool IsScanningStep { get { return _stepNum == EmSC_NO.S030_SCAN_COMPLETE_WAIT; } }

        public ScanningStep()
        {
        }
        public override void LogicWorking(Equipment equip)
        {
            if (equip.IsPause)
            {
                StepStartTime = DateTime.Now;
            }
            if (equip.IsImmediatStop) return;

            if (_stepNum != _stepNumOld)
            {
                Logger.Log.AppendLine(LogLevel.AllLog, "★[SCAN STEP] = {0} Step 소요시간 {1}s",
                 _stepNum.ToString(), (DateTime.Now - StepStartTime).TotalSeconds);

                StepStartTime = DateTime.Now;
                _stepNumOld = _stepNum;
            }
            else if (GG.TestMode == false)
            {
                if (_stepNum > 0 &&
                    (DateTime.Now - StepStartTime).TotalMilliseconds > equip.CtrlSetting.Ctrl.AutoStepTimeout)
                {
                    string err = GG.boChinaLanguage ? string.Format("★[SCAN STEP] = {0} 中，发生 Auto Step Timeover ", _stepNum.ToString()) : string.Format("★[SCAN STEP] = {0} 중 AutoStep Timeover 발생", _stepNum.ToString());
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0289_AUTO_STEP_OVERTIME);
                    InterLockMgr.AddInterLock("AutoStep Timeover\n" + err);
                    Logger.Log.AppendLine(LogLevel.Error, "{0} ({1}s)", err, (DateTime.Now - StepStartTime).TotalSeconds);
                    equip.IsInterlock = true;
                }
            }

            if (_stepNum == EmSC_NO.S000_SCAN_WAIT)
            {
            }
            else if (_stepNum == EmSC_NO.S010_SCAN_START)
            {
                if (IsptAddrB.YB_MotorInterlockOffState.vBit == true || GG.TestMode == true)
                {                 
                    TactTimeMgr.Instance.Set(EM_TT_LST.T070_SCAN_RUN_START);
                    if (equip.InspPc.StartCommand(equip, EmInspPcCommand.INSP_START, 0) == false) return;
                    _tmrInspTimeover.Start(0, equip.CtrlSetting.Insp.ScanOvertime);
                    _stepNum = EmSC_NO.S020_SCAN_START_ACK_WAIT;
                }
            }
            else if (_stepNum == EmSC_NO.S020_SCAN_START_ACK_WAIT)
            {
                if (equip.InspPc.IsCommandAck(equip, EmInspPcCommand.INSP_START) == true)
                {
                    Logger.Log.AppendLine(LogLevel.Info, "검사시작");
                    _stepNum = EmSC_NO.S030_SCAN_COMPLETE_WAIT;
                }
            }
            else if (_stepNum == EmSC_NO.S030_SCAN_COMPLETE_WAIT)
            {
                PassStepTimeoverCheck();
                if (equip.InspPc.IsEventComplete(EmInspPcEvent.INSP_COMPLETE) == true)
                {
                    _tmrInspTimeover.Stop();
                    Logger.Log.AppendLine(LogLevel.Warning, "검사 완료");
                    _stepNum = EmSC_NO.S060_PMAC_SCAN_STOP_WAIT;
                }
                else if (_tmrInspTimeover == true)
                {
                    _tmrInspTimeover.Stop();
                    AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0332_INSP_SCAN_TIMEOVER);
                    Logger.Log.AppendLine(LogLevel.Warning, "검사 TIME OVER");
                    _stepNum = EmSC_NO.S040_SCAN_STOP;
                }
                else if(equip.IsReserveInsp_STOP == EmReserveInsp_Stop.RESERVE)
                {
                    _tmrInspTimeover.Stop();

                    equip.IsReserveInsp_STOP = EmReserveInsp_Stop.DISABLE;
                    equip.IsReserveReInsp = EmReserveReInsp.RESERVE;

                    Logger.Log.AppendLine(LogLevel.Info, "검사 중지");
                    _stepNum = EmSC_NO.S040_SCAN_STOP;
                }
            }
            #region Timeover처리
            else if (_stepNum == EmSC_NO.S040_SCAN_STOP)
            {
                //jys:: 190727 신경철J 검사에서 명령 안주면 됨
                // if (equip.PMac.StartCommand(equip, EmPMacmd.INSP_STOP, 0) == false) return;
                //190727 김태현2K : 인터락신호만 켜주면 다음 동작 안함 (-> equip.IsUseMotorInspSever() 에 스텝에 따라 바꾸게 돼있음)
                //if (equip.InspPc.StartCommand(equip, EmInspPcCommand.INSP_END, 0) == false) return; 
                Logger.IsptLog.AppendLine(LogLevel.Warning, "INSP TIME OVER");
                _stepNum = EmSC_NO.S050_INSP_TIME_OVER;
            }
            else if (_stepNum == EmSC_NO.S050_INSP_TIME_OVER)
            {
                if (true
                    //&& equip.InspPc.IsCommandAck(equip, EmInspPcCommand.INSP_END) == true
                    //&& equip.PMac.IsCommandAck(equip, EmPMacmd.INSP_STOP) == true
                    )
                {
                    _stepNum = EmSC_NO.S060_PMAC_SCAN_STOP_WAIT;
                }
            }
            #endregion
            else if (_stepNum == EmSC_NO.S060_PMAC_SCAN_STOP_WAIT)
            {
                if (equip.PMac.XB_InspRunning.vBit == true) return;

                if (equip.StageX.IsMoving == false
                    && equip.StageY.IsMoving == false
                    )
                {
                    _tmrMotorStopWait.Start(0, 2000);
                    _stepNum = EmSC_NO.S070_SCAN_COMPLETE;
                }
            }
            else if (_stepNum == EmSC_NO.S070_SCAN_COMPLETE)
            {
                if (_tmrMotorStopWait)
                {
                    _tmrMotorStopWait.Stop();
                    TactTimeMgr.Instance.Set(EM_TT_LST.T070_SCAN_RUN_END);
                    _isComplete = true;
                    _stepNum = 0;
                    _scanIndex = 0;
                }
            }
        }

        public bool StepStart(Equipment equip)
        {
            if (_stepNum != EmSC_NO.S000_SCAN_WAIT)
            {
                InterLockMgr.AddInterLock("Interlock <RUNNING> \n (A start command entered during SCAN STEP.)");
                Logger.Log.AppendLine(LogLevel.Warning, "A start command entered during SCAN STEP.");
                equip.IsInterlock = true;
                return false;
            }

            _scanIndex = 0;
            _isComplete = false;
            /*
            if (equip.IsReserveReInsp == EmReserveReInsp.SRTART)
            {
                _stepNum = EmSC_NO.S005_REINSP_SCAN_START;
                equip.IsReserveReInsp = EmReserveReInsp.RESERVE;
            }
            else
                _stepNum = EmSC_NO.S010_SCAN1_READY_ACK_WAIT;
            */

            _stepNum = EmSC_NO.S010_SCAN_START;

            return true;
        }
        public bool StepStop(Equipment equip)
        {
            _isComplete = false;
            _stepNum = EmSC_NO.S000_SCAN_WAIT;

            return true;
        }

        public bool IsStepComplete()
        {
            return _isComplete && _stepNum == EmSC_NO.S000_SCAN_WAIT;
        }
    }
}
