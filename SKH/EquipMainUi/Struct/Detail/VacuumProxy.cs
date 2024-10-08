using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.Detail
{
    public class VacuumProxy : UnitBase
    {
        public Equipment Equip { get; set; }
        public VacuumSwitch Stage1 = new VacuumSwitch();
        public VacuumSwitch Stage2 = new VacuumSwitch();
        public bool AllVacuumOn()
        {
            Stage1.OnOff(Equip, true);
            Stage2.OnOff(Equip, true);

            Logger.Log.AppendLine(LogLevel.Info, "전체 진공 ON");
            return true;
        }
        public bool AllVacuumOff()
        {
            Stage1.OnOff(Equip, false);
            Stage2.OnOff(Equip, false);

            Logger.Log.AppendLine(LogLevel.Info, "전체 진공 OFF");
            return true;
        }

        private int _offStep = 0;
        private PlcTimerEx _blowerOnTime = new PlcTimerEx("Blower On Time");
        public void StartOffStep()
        {
            _offStep = 10;
            Logger.Log.AppendLine(LogLevel.Info, "진공 OFF Step");
        }
        public override void LogicWorking(Equipment equip)
        {
            switch (_offStep)
            {
                case 0:
                    break;
                case 10:
                    AllVacuumOff();
                    _offStep = 20;
                    break;
                case 20:
                    Equip.Blower.BlowerOn();
                    _blowerOnTime.Start(0, Equip.CtrlSetting.Ctrl.BlowerTime);
                    _offStep = 30;
                    break;
                case 30:
                    if (_blowerOnTime)
                    {
                        Equip.Blower.BlowerOff();
                        _blowerOnTime.Stop();
                        _offStep = 0;
                        Logger.Log.AppendLine(LogLevel.Info, "진공 OFF 완료");
                    }
                    break;
            }
        }
        public bool IsVacuumSolOn
        {
            get
            {
                return Equip.Vacuum.Stage1.IsSolOnOff
                    && Equip.Vacuum.Stage2.IsSolOnOff
                    ;
            }
        }

        public bool IsVacuumOn
        {
            get
            {
                return Equip.Vacuum.Stage1.IsOnOff
                    && Equip.Vacuum.Stage2.IsOnOff
                    ;
            }
        }
        public bool IsVacuumOff
        {
            get
            {
                return Equip.Vacuum.Stage1.IsOnOff == false
                    && Equip.Vacuum.Stage2.IsOnOff == false
                    ;         
            }
        }
    }
}
