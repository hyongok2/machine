using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Detail
{
    public class PreAlignerVacuumProxy : UnitBase
    {
        public Switch Vacuum = new Switch();
        public SwitchOneWay Blower = new SwitchOneWay();
        private PlcTimerEx _blowerOnTime = new PlcTimerEx("Pre Aligner Blower On Time");

        public PreAlignerVacuumProxy()
        {

        }

        int _offStep = 0;
        public override void LogicWorking(Equipment equip)
        {
            switch (_offStep)
            {
                case 0:
                    break;
                case 10:
                    Vacuum.OnOff(equip, false);
                    _offStep = 20;
                    break;
                case 20:
                    Blower.OnOff(equip, true);
                    _blowerOnTime.Start(0, equip.CtrlSetting.Ctrl.BlowerTime);
                    _offStep = 30;
                    break;
                case 30:
                    if (_blowerOnTime)
                    {
                        Blower.OnOff(equip, false);
                        _blowerOnTime.Stop();
                        _offStep = 0;
                        Logger.Log.AppendLine(LogLevel.Info, "프리얼라이너 진공 OFF 완료");
                    }
                    break;
            }
        }

        public bool VacuumOn()
        {
            Vacuum.OnOff(GG.Equip, true);
            Logger.Log.AppendLine(LogLevel.Info, "프리 얼라이너 진공 On");
            return true;
        }
        public bool IsVacuumOn
        {
            get
            {
                return Vacuum.IsOnOff;
            }
        }
        public bool IsVacuumOff
        {
            get
            {
                return Vacuum.IsOnOff == false && Blower.IsOnOff == false && _offStep == 0;
            }
        }
        public void StartOffStep()
        {
            _offStep = 10;
            Logger.Log.AppendLine(LogLevel.Info, "프리 얼라이너 진공 Off Step Start");
        }
        public bool OffStepSync()
        {
            if (_offStep == 0 && IsVacuumOff) return true;
            else if (_offStep == 0)
                StartOffStep();
            return false;
        }
    }
}
