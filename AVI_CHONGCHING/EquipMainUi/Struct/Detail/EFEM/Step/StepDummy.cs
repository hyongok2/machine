//using EquipMainUi.Struct.Step;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace EquipMainUi.Struct.Detail.EFEM.Step
//{
//    public enum EmEFEMInitStep
//    {
//        S000_WAIT,
//    }
//    public class EFEMInitStep : StepBase
//    {
//        private EmEFEMInitStep _stepNum = 0;
//        private EmEFEMInitStep _stepNumOld = 0;
//        public EmEFEMInitStep StepNum { get { return _stepNum; } }
//        private bool _isComplete = false;

//public override void LogicWorking(Equipment equip)
//{
//    if (equip.IsPause)
//    {
//        if (equip.IsHeavyAlarm == true && equip.EquipRunMode == EmEquipRunMode.Auto)
//        {
//            equip.IsNeedRestart = true;
//        }
//        return;
//    }

//    if (_stepNum != _stepNumOld)
//    {
//        Logger.Log.AppendLine(LogLevel.AllLog, "●[{0}] = {1}", "MAIN_STEP", _stepNum.ToString());
//        _stepNumOld = _stepNum;
//    }
//if (_stepNum == EmEFEMMainStep.S000_END)
//            {
//            }
//            else if (_stepNum == EmEFEMMainStep.S010_WAIT)
//            {
//                if (equip.EquipRunMode == EmEquipRunMode.Auto)
//                {
//                    _stepNum = EmEFEMMainStep.S050_CHECK_EFEM_STATUS;
//                }
//            }
//}

//        public bool StepStart(Equipment equip)
//        {
//            if (_stepNum != EmEFEMInitStep.S000_WAIT)
//            {
//                InterLockMgr.AddInterLock("인터락<실행중>\n(LOADING STEP 진행중 시작 명령이 들어왔습니다.)");
//                Logger.Log.AppendLine(LogLevel.Warning, "LOADING STEP 진행중 시작 명령이 들어옴.");
//                equip.IsInterlock = true;

//                return false;
//            }
//            _isComplete = false;
//            _stepNum = EmEFEMInitStep.S000_WAIT;

//            return true;
//        }
//        public bool StepStop(Equipment equip)
//        {
//            _isComplete = false;
//            _stepNum = EmEFEMInitStep.S000_WAIT;

//            return true;
//        }

//        //메소드 - 완료 확인 
//        public bool IsStepComplete()
//        {
//            return _isComplete && _stepNum == 0;
//        }
//    }
//}
