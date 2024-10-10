using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using System.Diagnostics;
using Dit.Framework;

namespace EquipMainUi.Struct.Detail
{
    /// <summary>
    /// brief   Ionizer 시간확인 또 수정
    /// date    20180222
    /// brief   알람 확인 함수 추가
    /// date    20180221
    /// brief   Ionizer 시간확인 버그 수정
    /// date    20171229        
    /// </summary>
    public class PinIonizerProxy : UnitBase
    {
        public Equipment Equip { get; set; }

        public string Name { get; set; }
        public int ContollerCount { get; set; }
        public PlcAddr[] XB_ConditionWarning { get; set; }
        public PlcAddr[] XB_IonLevelWarning { get; set; }
        public PlcAddr[] XB_IonStop { get; set; }
        public PlcAddr[] XB_Alarm { get; set; }
        public PlcAddr[] YB_RemoteOn { get; set; }
        public PlcAddr[] YB_AirOn { get; set; }

        public EM_AL_LST[] AlarmConditionWarning { get; set; }
        public EM_AL_LST[] AlarmIonLevelWarning { get; set; }
        public EM_AL_LST[] AlarmIonStop { get; set; }
        public EM_AL_LST[] AlarmCommonAlarm { get; set; }

        private DateTime[] _ionizerOnTime;
        private DateTime[] _ionizerOffTime;

        private DateTime _alarmCheckTime;
        private bool _isCheckAlarm = true;
        /// <summary>
        /// ms단위
        /// </summary>
        private int _alarmCheckDelay = 5000;

        private bool _isPowerAContact = false;
        private bool _isAirAContact = true;

        public PinIonizerProxy(string name, int contollerCount, int airSolCount)
        {
            Name = name;
            ContollerCount = contollerCount;
            XB_ConditionWarning = new PlcAddr[contollerCount];
            XB_IonLevelWarning = new PlcAddr[contollerCount];
            XB_IonStop = new PlcAddr[contollerCount];
            XB_Alarm = new PlcAddr[contollerCount];
            YB_RemoteOn = new PlcAddr[contollerCount];
            YB_AirOn = new PlcAddr[airSolCount];
            _ionizerOnTime = new DateTime[contollerCount];
            _ionizerOffTime = new DateTime[contollerCount];

            AlarmConditionWarning = new EM_AL_LST[contollerCount];
            AlarmIonLevelWarning = new EM_AL_LST[contollerCount];
            AlarmIonStop = new EM_AL_LST[contollerCount];
            AlarmCommonAlarm = new EM_AL_LST[contollerCount];
        }

        public void Initalize(Equipment equip)
        {
            Equip = equip;
            IonizerOff();
        }
        private PeriodChecker _checkAbnormalPeriod = new PeriodChecker();        
        public override void LogicWorking(Equipment equip)
        {
            if(Equip.IsUseInterLockOff == false && GG.TestMode == false)
                CheckAlarm();

            if (_checkAbnormalPeriod.IsTimeToCheck(100))
            {
                CheckAbnormal();
                _checkAbnormalPeriod.Reset = true;
            }
        }
        private void CheckAbnormal()
        {
            // Controller Power만 On인 경우 Off시킴.
            if (IsPowerOn() && Air == false)
            {
                PowerOff();
                Logger.Log.AppendLine(LogLevel.Info, "PIN 이오나이저 Power만 On된 상태, Power Off!");
            }

            DecayCheckLogic();
        }

        public int DecayTime {  get { return (int)_decayCheck.ElapsedMilliseconds * 1 / 1000; } }
        private Stopwatch _decayCheck = new Stopwatch();
        private void DecayCheckLogic()
        {
            if (_decayCheck.IsRunning
                && _decayCheck.ElapsedMilliseconds > GG.Equip.CtrlSetting.Ctrl.IonizerDecayTime)
            {
                _decayCheck.Stop();

                if (Math.Abs(GG.Equip.ADC.IonizerStaticElectricity) > GG.Equip.CtrlSetting.Ctrl.IonizerStaticElecAlarmStd && !GG.TestMode)
                    AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0064_PIN_IONIZER_DECAY_TIME_OVER);
            }
        }


        private void CheckAlarm()
        {
            if (_isCheckAlarm && IsOn())
            {
                for (int iter = 0; iter < ContollerCount; ++iter)
                {
                    if (XB_Alarm[iter].vBit == false)
                        AlarmMgr.Instance.Happen(Equip, AlarmCommonAlarm[iter]);
                    //if (XB_IonLevelWarning[iter].vBit == true)
                    //    AlarmMgr.Instance.Happen(Equip, AlarmIonLevelWarning[iter]);
                    //if (XB_IonStop[iter].vBit == true)
                    //    AlarmMgr.Instance.Happen(Equip, AlarmIonStop[iter]);
                }
            }
            else if (DateTime.Now > _alarmCheckTime)
            {
                _isCheckAlarm = true;
            }
        }

        public bool PowerOn()
        {
            _isCheckAlarm = false;
            _alarmCheckTime = DateTime.Now.AddMilliseconds(_alarmCheckDelay);
            for (int i = 0; i < _ionizerOffTime.Length; ++i)
                _ionizerOnTime[i] = _ionizerOffTime[i] = DateTime.Now;
            YB_RemoteOn.ToList().ForEach(f => f.vBit = _isPowerAContact);
            _decayCheck.Restart();
            return true;
        }
        public bool PowerOn(int idx)
        {
            _isCheckAlarm = false;
            _alarmCheckTime = DateTime.Now.AddMilliseconds(_alarmCheckDelay);
            _ionizerOnTime[idx] = _ionizerOffTime[idx] = DateTime.Now;
            YB_RemoteOn[idx].vBit = _isPowerAContact;
            _decayCheck.Restart();
            return true;
        }
        public bool PowerOff()
        {
            for (int i = 0; i < _ionizerOffTime.Length; ++i)
                _ionizerOffTime[i] = DateTime.Now;       
            YB_RemoteOn.ToList().ForEach(f => f.vBit = _isPowerAContact == false);
            _decayCheck.Stop();
            return true;
        }
        public bool PowerOff(int idx)
        {
            _ionizerOffTime[idx] = DateTime.Now;
            YB_RemoteOn[idx].vBit = _isPowerAContact == false;
            return true;
        }
        public bool Air
        {
            get
            {
                return YB_AirOn.FirstOrDefault(f => f.vBit == (_isAirAContact == false)) == null;
            }
            set
            {
                YB_AirOn.ToList().ForEach(f => f.vBit = (_isAirAContact ? value : !value));
            }
        }
        public bool IsAirOn(int idx)
        {
            if(YB_AirOn[idx].vBit == true)
            {
                return true;
            }
            return false;
            
        }
        public bool AirOn(int idx)
        {
            YB_AirOn[idx].vBit = true;
            return true;
        }
        public bool AirOff(int idx)
        {
            YB_AirOn[idx].vBit = false;
            return true;
        }
        public bool IonizerOn()
        {
            Air = true;
            PowerOn();
            Logger.Log.AppendLine(LogLevel.Info, "PIN 이오나이저 ON");
            return true;
        }
        public bool IonizerOff()
        {            
            PowerOff();
            Air = false;
            
            Logger.Log.AppendLine(LogLevel.Info, "PIN 이오나이저 OFF");
            return true;
        }
        public bool IsOn()
        {
            return IsPowerOn() && Air;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx">-1일경우 전체</param>
        /// <returns></returns>
        public bool IsPowerOn(int idx = -1)
        {
            if (idx == -1)
                return YB_RemoteOn.FirstOrDefault(f => f.vBit == (_isPowerAContact == false)) == null;
            else
                return _isPowerAContact ? YB_RemoteOn[idx] : !YB_RemoteOn[idx];
        }
        public bool IsConditionWarning(int ionizerIndex)
        {
            return IsPowerOn(ionizerIndex) && XB_ConditionWarning[ionizerIndex];
        }
        public bool IsIonLevelWarning(int ionizerIndex)
        {
            return IsPowerOn(ionizerIndex) && XB_IonLevelWarning[ionizerIndex];
        }
        public bool IsCommonAlarm(int ionizerIndex)
        {
            return _isCheckAlarm && IsPowerOn(ionizerIndex) && XB_Alarm[ionizerIndex] == false;
        }
        public bool IsIonStop(int ionizerIndex)
        {
            return XB_IonStop[ionizerIndex];
        }
        public bool IsAbnormal(int ionizerIndex)
        {
            bool[] ionizer = 
            { 
                /* A3 GBA기준.
                 * Alarm - 중알람처리.
                 * Condition - 제외
                 * LevelAlarm - 경알람 및 FDC 보고
                 */
                (
                XB_Alarm[0].vBit == false ||
                XB_ConditionWarning[0].vBit ||
                XB_IonLevelWarning[0].vBit ||
                XB_IonStop[0].vBit
                ),
                (
                XB_Alarm[1].vBit == false ||
                XB_ConditionWarning[1].vBit ||
                XB_IonLevelWarning[1].vBit ||
                XB_IonStop[1].vBit
                ),
                (
                XB_Alarm[2].vBit == false ||
                XB_ConditionWarning[2].vBit ||
                XB_IonLevelWarning[2].vBit ||
                XB_IonStop[2].vBit
                ),
                (
                XB_Alarm[3].vBit == false ||
                XB_ConditionWarning[3].vBit ||
                XB_IonLevelWarning[3].vBit ||
                XB_IonStop[3].vBit
                ),
            };
            if (IsPowerOn(ionizerIndex) == true)
                return ionizer[ionizerIndex];
            else
                return false;
        }
        public int OnOffTime(int ionizerIndex)
        {
            double sec;
            if (IsPowerOn(ionizerIndex))
                sec = (DateTime.Now - _ionizerOnTime[ionizerIndex]).TotalSeconds;
            else
                sec = (_ionizerOffTime[ionizerIndex] - _ionizerOnTime[ionizerIndex]).TotalSeconds;
            int conv = (int)Math.Floor(sec);
            return (conv == int.MaxValue || conv == int.MinValue || conv < 0) ? 0 : conv;
        }
    }
}
