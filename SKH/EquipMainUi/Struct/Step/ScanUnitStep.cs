using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Struct.Detail;
using Dit.Framework.PLC;
using EquipMainUi.Monitor;
using EquipMainUi.Setting;

namespace EquipMainUi.Struct.Step
{
    public enum EmSCI_NO
    {
        S000_SCAN_WAIT,
        S010_SCAN_START,
        S020_Y_AND_SIGNAL_WAIT,
        S030_SCAN_MOVE_START,
        S040_SCAN_MOVE_END_WAIT,
        S050_SCAN_END_ACK_WAIT,
        S060_SCAN_END_DELAY_SCAN_READY,
        S070_SCAN_COMPLETE,
    }
    public class ScanPosSet
    {        
        public float XPos;
        public float YPos;
        public float ZPos;
    }
    /// <summary>
    /// date 170610
    /// InspXPos도 배열로.
    /// </summary>
    public class ScanUnitStep : StepBase
    {
        //Loading Step 로직 처리. 
        private bool _isComplete = false;
        private EmSCI_NO _stepNum = 0;
        private EmSCI_NO _stepNumOld = 0;
        public EmSCI_NO StepNum { get { return _stepNum; } }
        private PlcTimerEx _scanDelay = new PlcTimerEx("검사 Scan END-READY Delay");
        private int _scanIndex = 0;
        private float _shiftPos = 0;
        private float _scanPos = 0;
        private EmInspectionType _inspType;

        public override void LogicWorking(Equipment equip)
        {
            if (equip.IsImmediatStop) return;

            if (_stepNum != _stepNumOld)
            {
                Logger.Log.AppendLine(LogLevel.NoLog, "[SCAN UNIT STEP] = {0} / SCAN INDEX = {1}", _stepNum.ToString(), _scanIndex.ToString());
                _stepNumOld = _stepNum;
            }

            if (_stepNum == EmSCI_NO.S000_SCAN_WAIT)
            {
            }
            else if (_stepNum == EmSCI_NO.S010_SCAN_START)
            {                
                _stepNum = EmSCI_NO.S020_Y_AND_SIGNAL_WAIT;
            }
            else if (_stepNum == EmSCI_NO.S020_Y_AND_SIGNAL_WAIT)
            {
                _shiftPos = _inspType == EmInspectionType.OneD ? 
                    equip.SeqRecipe.Insp1DInfo.CamYStart + _scanIndex * equip.SeqRecipe.Insp1DInfo.CamYShiftDistance :
                    equip.SeqRecipe.Insp2DInfo.CamYStart + _scanIndex * equip.SeqRecipe.Insp2DInfo.CamYShiftDistance;

                if (equip.StageY.IsMoveOnPosition(_shiftPos)
                    && equip.InspPc.IsCommandAck(equip, EmInspPcCommand.ALIGN_START)
                    && equip.InspPc.IsCommandAck(equip, EmInspPcCommand.INSP_START)
                    && equip.InspPc.IsEventComplete(EmInspPcEvent.ALIGN_COMPLETE)
                    && equip.InspPc.IsEventComplete(EmInspPcEvent.INSP_COMPLETE)
                    )
                {                              
                    if (_inspType == EmInspectionType.OneD)                        
                    {
                        _scanPos = _scanIndex % 2 == 0 ? equip.SeqRecipe.Insp1DInfo.ScanEndPos : equip.SeqRecipe.Insp1DInfo.ScanStartPos;
                        if (equip.StageX.MovePosition(equip,
                            _scanPos,
                            equip.SeqRecipe.Insp1DInfo.ScanSpeed,
                            equip.SeqRecipe.Insp1DInfo.ScanAccTime
                            ) == false) return;
                    }
                    else
                    {
                        _scanPos = _scanIndex % 2 == 0 ? equip.SeqRecipe.Insp2DInfo.ScanEndPos : equip.SeqRecipe.Insp2DInfo.ScanStartPos;
                        if (equip.StageX.MovePosition(equip,
                            _scanPos,
                            equip.SeqRecipe.Insp2DInfo.ScanSpeed,
                            equip.SeqRecipe.Insp2DInfo.ScanAccTime
                            ) == false) return;
                    }
                    _stepNum = EmSCI_NO.S040_SCAN_MOVE_END_WAIT;
                }
            }
            else if (_stepNum == EmSCI_NO.S040_SCAN_MOVE_END_WAIT)
            {
                if (equip.StageX.IsMoveOnPosition(_scanPos))
                {
                    _shiftPos = _inspType == EmInspectionType.OneD ?
                        equip.SeqRecipe.Insp1DInfo.CamYStart + (_scanIndex + 1) * equip.SeqRecipe.Insp1DInfo.CamYShiftDistance :
                        equip.SeqRecipe.Insp2DInfo.CamYStart + (_scanIndex + 1) * equip.SeqRecipe.Insp2DInfo.CamYShiftDistance;

                    if (equip.SeqRecipe.InspType == EmInspectionType.OneD ?
                        _scanIndex + 1 == equip.SeqRecipe.Insp1DInfo.ScanCount :
                        _scanIndex + 1 == equip.SeqRecipe.Insp2DInfo.ScanCount
                        )
                    {

                    }
                    else
                    {
                        if (_inspType == EmInspectionType.OneD)
                        {
                            if (equip.StageY.MovePosition(equip,
                                _shiftPos,
                                equip.SeqRecipe.Insp1DInfo.CamYShiftSpeed,
                                equip.SeqRecipe.Insp1DInfo.CamYShiftAccTime
                                ) == false) return;
                        }
                        else
                        {
                            if (equip.StageY.MovePosition(equip,
                                _shiftPos,
                                equip.SeqRecipe.Insp2DInfo.CamYShiftSpeed,
                                equip.SeqRecipe.Insp2DInfo.CamYShiftAccTime
                                ) == false) return;
                        }
                    }

                    if (equip.InspPc.StartCommand(equip, EmInspPcCommand.REVIEW_START, _scanIndex) == false) return;

                    _stepNum = EmSCI_NO.S050_SCAN_END_ACK_WAIT;
                }
            }
            else if (_stepNum == EmSCI_NO.S050_SCAN_END_ACK_WAIT)
            {
                if (equip.InspPc.IsCommandAck(equip, EmInspPcCommand.REVIEW_START))
                {
                    _stepNum = EmSCI_NO.S060_SCAN_END_DELAY_SCAN_READY;
                }
            }
            else if (_stepNum == EmSCI_NO.S060_SCAN_END_DELAY_SCAN_READY)
            {
                if (_scanDelay)
                {
                    _scanDelay.Stop();

                    if (equip.SeqRecipe.InspType == EmInspectionType.OneD ?
                        _scanIndex + 1 == equip.SeqRecipe.Insp1DInfo.ScanCount :
                        _scanIndex + 1 == equip.SeqRecipe.Insp2DInfo.ScanCount
                        )
                    {

                    }
                    else
                    {
                        equip.InspPc.StartCommand(equip, EmInspPcCommand.ALIGN_START, equip.StScanning.ScanIndex + 1);
                        equip.InspPc.StartCommand(equip, EmInspPcCommand.INSP_START, equip.StScanning.ScanIndex + 1);
                    }

                    _stepNum = EmSCI_NO.S070_SCAN_COMPLETE;
                }
            }
            else if (_stepNum == EmSCI_NO.S070_SCAN_COMPLETE)
            {
                _stepNum = EmSCI_NO.S000_SCAN_WAIT;
                _isComplete = true;
                _stepNum = 0;
            }
        }
        public bool StepStart(Equipment equip, int scanIndex, EmInspectionType inspType)
        {
            if (_stepNum != EmSCI_NO.S000_SCAN_WAIT)
            {
                InterLockMgr.AddInterLock("인터락<실행중>\n(SCAN ITEM STEP 진행중 시작 명령이 들어왔습니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "SCAN ITEM STEP 진행중 시작 명령이 들어옴.");
                equip.IsInterlock = true;
                return false;
            }
            _scanIndex = scanIndex;            
            _inspType = inspType;

            _isComplete = false;
            _stepNum = EmSCI_NO.S010_SCAN_START;
            return true;
        }
        public bool StepStop(Equipment equip)
        {
            _isComplete = false;
            _stepNum = EmSCI_NO.S000_SCAN_WAIT;

            return true;
        }
        public bool IsStepComplete()
        {
            return _isComplete && _stepNum == EmSCI_NO.S000_SCAN_WAIT;
        }
    }
}
