using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct
{
    public enum EM_AL_LV
    {
        Heavy = 0,
        Warn,
        Normal
    }

    public class Alarm
    {
        public EM_AL_LST ID { get; set; }
        public DateTime HappenTime { get; set; }
        public EM_AL_STATE State { get; set; }
        //public bool IsHeavy { get; set; }
        public bool Happen { get; set; }
        public string Desc { get; set; }
        public EM_AL_LV Level { get; set; }

        public Alarm()
        {
            Happen = false;
            Desc = string.Empty;
            Level = EM_AL_LV.Heavy;
        }

        public void SetState(string vState)
        {
            switch (vState.ToUpper())
            {
                case "HEAVY":
                    State = EM_AL_STATE.Heavy;
                    break;
                case "WARN":
                    State = EM_AL_STATE.Warn;
                    break;
                case "UNUSED":
                    State = EM_AL_STATE.Unused;
                    break;
                default:
                    State = EM_AL_STATE.Heavy;
                    break;
            }       
        }

        public void SetLevel(string vLevel)
        {
            switch (vLevel.ToUpper())
            {
                case "HEAVY":
                    Level = EM_AL_LV.Heavy;
                    break;
                case "WARN":
                    Level = EM_AL_LV.Warn;
                    break;
                case "NORMAL":
                    Level = EM_AL_LV.Normal;
                    break;
                default:
                    Level = EM_AL_LV.Heavy;
                    break;
            }
        }
    }
}
