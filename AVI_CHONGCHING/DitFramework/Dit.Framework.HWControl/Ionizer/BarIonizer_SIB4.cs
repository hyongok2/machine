using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using Dit.Framework.Log;

namespace Dit.Framework.HWControl.Ionizer
{
    /// <summary>
    /// SIB4 모델 컨트롤러 제어
    /// since 180621
    /// </summary>
    public class BarIonizer_SIB4
    {
        public enum Alarms
        {
            None,
            HighVoltageAlarm,
            TipCleanAlarm,            
        }

        public string Name { get; set; }
        public int ContollerCount { get; set; }
        public PlcAddr XB_HighVoltageAlarm { get; set; }
        public PlcAddr XB_TipCleanAlarm{ get; set; }
        public PlcAddr XB_Run { get; set; }
        public PlcAddr YB_RemoteOn { get; set; }
        public PlcAddr YB_AirSol { get; set; }        

        public bool IsPowerAContact = false;
        public bool IsAirAContact = true;

        private DateTime _ionizerOnTime;
        private DateTime _ionizerOffTime;

        #region Events
        public delegate void AlarmEvent(BarIonizer_SIB4_AlarmEventArgs args);
        public delegate void LogEvent(LogEventArgs args);
        public event AlarmEvent AlarmHappened;
        public event LogEvent AppendLog;
        #endregion

        public BarIonizer_SIB4(string name)
        {
            Name = name;
        }
        public void Initalize()
        {
            IonizerOff();
        }
        /// <summary>
        /// 동작함수 1 : Check Alarm - HighVoltageAlarm, TipCleanAlarm
        /// </summary>
        /// <param name="equip"></param>
        public void LogicWorking()
        {            
            CheckAlarm();
        }

        private void CheckAlarm()
        {
            if (IsOn())
            {
                if (XB_HighVoltageAlarm.vBit == true)
                    AlarmHappened(new BarIonizer_SIB4_AlarmEventArgs(happenedAlarm: Alarms.HighVoltageAlarm));
                if (XB_TipCleanAlarm.vBit == true)
                    AlarmHappened(new BarIonizer_SIB4_AlarmEventArgs(happenedAlarm: Alarms.TipCleanAlarm));
            }
        }

        public bool Air
        {
            get
            {
                return YB_AirSol.vBit;
            }
            set
            {
                YB_AirSol.vBit = value;
            }
        }
        public bool IsOn()
        {
            return IsPowerOn() && Air;
        }
        public bool IsPowerOn()
        {
            return YB_RemoteOn.vBit;
        }
        public void PowerOn()
        {
            _ionizerOnTime = DateTime.Now;
            YB_RemoteOn.vBit = true;
        }
        public void PowerOff()
        {
            YB_RemoteOn.vBit = false;
            _ionizerOffTime = DateTime.Now;
        }
        public bool IonizerOn()
        {
            Air = true;
            PowerOn();

            AppendLog(new LogEventArgs(LogLevel.Info, "BAR 이오나이저 ON"));            
            return true;
        }
        public bool IonizerOff()
        {
            Air = false;
            PowerOff();

            AppendLog(new LogEventArgs(LogLevel.Info, "BAR 이오나이저 OFF"));
            return true;
        }
        public bool IsAbnormal()
        {
            return XB_HighVoltageAlarm.vBit == true
                || XB_TipCleanAlarm.vBit == true;
        }

        public int OnOffTime()
        {
            double sec;
            if (IsPowerOn())
                sec = (DateTime.Now - _ionizerOnTime).TotalSeconds;
            else
                sec = (_ionizerOffTime - _ionizerOnTime).TotalSeconds;
            int conv = (int)Math.Floor(sec);
            return (conv == int.MaxValue || conv == int.MinValue || conv < 0) ? 0 : conv;
        }
    }
}
