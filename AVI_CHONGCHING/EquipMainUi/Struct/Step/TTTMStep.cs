using EquipMainUi.Monitor;
using EquipMainUi.Struct.Detail;
using EquipMainUi.Struct.Detail.PC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.Step
{
    public enum EmTTTM_NO
    {
        S000_TTTM_WAIT,

        S010_TTTM_START,
        S020_TTTM_START_ACK_WAIT,
        S030_TTTM_COMPLETE_WAIT,

        S040_TTTM_TIME_OVER,

        S050_SCAN_START_POSITION_MOVE_START,
        S060_TTTM_END,
    }
    public class TTTMStep : StepBase
    {
        private bool _isComplete;
        private EmTTTM_NO _stepNum;
        private EmTTTM_NO _stepNumOld;
        private PlcTimerEx _tmrTTTMTimeover = new PlcTimerEx("TTTM Progress Time Over");
        public long TTTMPassTime { get { return _tmrTTTMTimeover.PassTime; } }

        private float _xTargetPos, _yTargetPos;
        public EmTTTM_NO StepNum { get { return _stepNum; } }

        public bool StepStart(Equipment equip)
        {
            if (_stepNum != EmTTTM_NO.S000_TTTM_WAIT)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<执行中>\n(TTTM STEP 进行中介入开始命令.)" : "인터락<실행중>\n(TTTM STEP 진행중 시작 명령이 들어왔습니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "TTTM STEP 진행중 시작 명령이 들어옴.");
                equip.IsInterlock = true;

                return false;
            }
            if (equip.IsTTTMMode == false)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<执行中>\n(TTTM Mode没有被激活，开始命令仍然介入.)" : "인터락<실행중>\n(TTTM Mode가 활성화 되어 있지 않은데 시작 명령이 들어왔습니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "TTTM Mode가 활성화 되어 있지 않은데 시작 명령이 들어옴.");
                equip.IsInterlock = true;
                return false;
            }

            _isComplete = false;
            _stepNum = EmTTTM_NO.S010_TTTM_START;

            return true;
        }

        public override void LogicWorking(Equipment equip)
        {
            if (_stepNum != _stepNumOld)
            {
                Logger.Log.AppendLine(LogLevel.AllLog, "★[TTTM STEP] = {0} Step 소요시간 {1}s",
                    _stepNum.ToString(), (DateTime.Now - StepStartTime).TotalSeconds);

                StepStartTime = DateTime.Now;
                _stepNumOld = _stepNum;
            }

            switch (_stepNum)
            {
                case EmTTTM_NO.S000_TTTM_WAIT:

                    break;
                case EmTTTM_NO.S010_TTTM_START:
                    if (equip.InspPc.StartCommand(equip, EmInspPcCommand.TTTM_START, 0) == false) return;
                    _stepNum = EmTTTM_NO.S020_TTTM_START_ACK_WAIT;
                    break;
                case EmTTTM_NO.S020_TTTM_START_ACK_WAIT:
                    if (equip.InspPc.IsCommandAck(equip, EmInspPcCommand.TTTM_START))
                    {
                        _tmrTTTMTimeover.Start(0, equip.CtrlSetting.Insp.TTTMOvertime);
                        _stepNum = EmTTTM_NO.S030_TTTM_COMPLETE_WAIT;
                    }
                    break;
                case EmTTTM_NO.S030_TTTM_COMPLETE_WAIT:
                    if (equip.InspPc.IsEventComplete(EmInspPcEvent.TTTM_COMPLETE) == true)
                    {
                        _tmrTTTMTimeover.Stop();
                        TactTimeMgr.Instance.Set(EM_TT_LST.T055_TTTM_END, EM_TT_LST.T060_SCAN_START_POS_START);
                        Logger.Log.AppendLine(LogLevel.Info, "TTTM 완료");
                        _stepNum = EmTTTM_NO.S050_SCAN_START_POSITION_MOVE_START;
                    }
                    else if (_tmrTTTMTimeover)
                    {
                        _tmrTTTMTimeover.Stop();
                        TactTimeMgr.Instance.Set(EM_TT_LST.T055_TTTM_END);

                        Logger.Log.AppendLine(LogLevel.Info, "TTTM Time Over");
                        AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0334_TTTM_TIMEOVER);
                        _stepNum = EmTTTM_NO.S040_TTTM_TIME_OVER;
                    }
                    break;
                case EmTTTM_NO.S040_TTTM_TIME_OVER:
                    //Timeover 처리
                    TactTimeMgr.Instance.Set(EM_TT_LST.T060_SCAN_START_POS_START);
                    //
                    _stepNum = EmTTTM_NO.S050_SCAN_START_POSITION_MOVE_START;
                    break;
                case EmTTTM_NO.S050_SCAN_START_POSITION_MOVE_START:

                    if (IsptAddrW.XF_InspectionStartX.vFloat < 0 || GG.TestMode == true)
                    {
                        _xTargetPos = equip.StageX.Setting.LstServoPosiInfo[StageXServo.InspReadyPos].Position;
                        Logger.Log.AppendLine(LogLevel.Warning, "StageX InspReady위치 제어지정위치 이동 {0}", _xTargetPos);
                    }
                    else
                        _xTargetPos = IsptAddrW.XF_InspectionStartX.vFloat;

                    if (IsptAddrW.XF_InspectionStartY.vFloat < 0 || GG.TestMode == true)
                    {
                        _yTargetPos = equip.StageY.Setting.LstServoPosiInfo[StageYServo.InspReadyPos].Position;
                        Logger.Log.AppendLine(LogLevel.Warning, "StageY InspReady위치 제어지정위치 이동 {0}", _yTargetPos);
                    }
                    else
                        _yTargetPos = IsptAddrW.XF_InspectionStartY.vFloat;

                    if(equip.IsHeavyAlarm == false)
                    {
                        if (equip.StageX.MovePosition(equip, StageXServo.InspReadyPos, _xTargetPos) == false) return;
                        if (equip.StageY.MovePosition(equip, StageYServo.InspReadyPos, _yTargetPos) == false) return;
                        _stepNum = EmTTTM_NO.S060_TTTM_END;
                    }
                    
                    break;
                case EmTTTM_NO.S060_TTTM_END:
                    if (equip.StageX.IsMoveOnPosition(_xTargetPos) == true &&
                        equip.StageY.IsMoveOnPosition(_yTargetPos) == true)
                    {
                        equip.processTTTMCount = -1;
                        TactTimeMgr.Instance.Set(EM_TT_LST.T060_SCAN_START_POS_END);
                        Logger.Log.AppendLine(LogLevel.Info, "TTTM Step 완료");

                        _isComplete = true;
                        _stepNum = EmTTTM_NO.S000_TTTM_WAIT;
                    }
                    break;
            }
        }

        public bool IsStepComplete()
        {
            return _isComplete && _stepNum == 0;
        }

        public bool StepStop(Equipment equipment)
        {
            _isComplete = false;
            _stepNum = EmTTTM_NO.S000_TTTM_WAIT;
            return true;
        }
    }
}
