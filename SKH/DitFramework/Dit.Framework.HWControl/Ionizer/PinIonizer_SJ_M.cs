using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using Dit.Framework.Log;

namespace Dit.Framework.HWControl
{
    /// <summary>
    /// SJ_M 모델 컨트롤러 제어
    /// since   20180326
    /// </summary>
    public class PinIonizer_SJ_M
    {
        public enum Alarms
        {
            None,
            ConditionWarning,
            IonLeveltWarning,
            IonStop,
            CommonAlarm,
        }

        public string Name { get; set; }
        public int ContollerCount { get; set; }
        public PlcAddr[] XB_ConditionWarning { get; set; }
        public PlcAddr[] XB_IonLevelWarning { get; set; }
        public PlcAddr[] XB_IonStop { get; set; }
        public PlcAddr[] XB_Alarm { get; set; }
        public PlcAddr[] YB_RemoteOn { get; set; }
        public PlcAddr[] YB_AirOn { get; set; }

        private DateTime[] _ionizerOnTime;
        private DateTime[] _ionizerOffTime;        

        private DateTime _alarmCheckTime;
        private bool _isCheckAlarm = true;
        /// <summary>
        /// ms단위
        /// </summary>
        public int AlarmCheckDelay = 3000;

        public bool IsPowerAContact = false;
        public bool IsAirAContact = true;
        
        #region Events
        public delegate void AlarmEvent(PinIonizer_SJ_M_AlarmEventArgs args);
        public delegate void LogEvent(LogEventArgs args);
        public event AlarmEvent AlarmHappened;
        public event LogEvent AppendLog;
        #endregion

        public PinIonizer_SJ_M(string name, int contollerCount, int airSolCount)
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
        }

        public void Initalize()
        {
            IonizerOff();
        }        
        /// <summary>
        /// 동작함수 1 : Check Alarm - CondtionWanring, IonLevelWarning, IonStop, CommonAlarm
        /// 동작함수 2 : Check Abnormal - Air Off, Power On인 경우 Power 자동 Off
        /// </summary>
        public void LogicWorking()
        {
            CheckAlarm();
            CheckAbnormal();
        }
        private void CheckAbnormal()
        {
            // Controller Power만 On인 경우 Off시킴.
            if (IsPowerOn() && Air == false)
            {
                PowerOff();
                AppendLog(new LogEventArgs(logLevel: LogLevel.Error, message: "PIN 이오나이저 Power만 On된 상태, Power Off!"));
            }
        }

        private void CheckAlarm()
        {
            if (_isCheckAlarm && IsPowerOn())
            {
                for (int iter = 0; iter < ContollerCount; ++iter)
                {
                    if (XB_ConditionWarning[iter].vBit == true)
                        AlarmHappened(new PinIonizer_SJ_M_AlarmEventArgs(controllIndex: iter, happenedAlarm: Alarms.ConditionWarning));
                    if (XB_IonLevelWarning[iter].vBit == true)
                        AlarmHappened(new PinIonizer_SJ_M_AlarmEventArgs(controllIndex: iter, happenedAlarm: Alarms.IonLeveltWarning));
                    if (XB_IonStop[iter].vBit == true)
                        AlarmHappened(new PinIonizer_SJ_M_AlarmEventArgs(controllIndex: iter, happenedAlarm: Alarms.IonStop));
                    if (XB_Alarm[iter].vBit == false)
                        AlarmHappened(new PinIonizer_SJ_M_AlarmEventArgs(controllIndex: iter, happenedAlarm: Alarms.CommonAlarm));
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
            _alarmCheckTime = DateTime.Now.AddMilliseconds(AlarmCheckDelay);
            for (int i = 0; i < _ionizerOffTime.Length; ++i)
                _ionizerOnTime[i] = _ionizerOffTime[i] = DateTime.Now;
            YB_RemoteOn.ToList().ForEach(f => f.vBit = IsPowerAContact);            
            return true;
        }
        public bool PowerOff()
        {
            for (int i = 0; i < _ionizerOffTime.Length; ++i)
                _ionizerOffTime[i] = DateTime.Now;       
            YB_RemoteOn.ToList().ForEach(f => f.vBit = IsPowerAContact == false);                 
            return true;
        }
        public bool Air
        {
            get
            {
                return YB_AirOn.FirstOrDefault(f => f.vBit == (IsAirAContact == false)) == null;
            }
            set
            {
                YB_AirOn.ToList().ForEach(f => f.vBit = (IsAirAContact ? value : !value));
            }
        }
        public bool IonizerOn()
        {
            Air = true;
            PowerOn();

            AppendLog(new LogEventArgs(logLevel: LogLevel.Info, message: "PIN 이오나이저 ON"));            
            return true;
        }
        public bool IonizerOff()
        {
            PowerOff();
            Air = false;

            AppendLog(new LogEventArgs(logLevel: LogLevel.Info, message: "PIN 이오나이저 OFF"));            
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
                return YB_RemoteOn.FirstOrDefault(f => f.vBit == (IsPowerAContact == false)) == null;
            else
                return IsPowerAContact ? YB_RemoteOn[idx] : !YB_RemoteOn[idx];
        }
        public bool IsConditionWarning(int ionizerIndex)
        {
            return XB_ConditionWarning[ionizerIndex];
        }
        public bool IsIonLevelWarning(int ionizerIndex)
        {
            return XB_IonLevelWarning[ionizerIndex];
        }
        public bool IsCommonAlarm(int ionizerIndex)
        {
            return IsPowerOn(ionizerIndex) && XB_Alarm[ionizerIndex] == false;
        }
        public bool IsIonStop(int ionizerIndex)
        {
            return XB_IonStop[ionizerIndex];
        }
        public bool IsAbnormal(int ionizerIndex)
        {
            return IsConditionWarning(ionizerIndex)
                || IsIonLevelWarning(ionizerIndex)
                || IsCommonAlarm(ionizerIndex)
                || IsIonStop(ionizerIndex);
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
