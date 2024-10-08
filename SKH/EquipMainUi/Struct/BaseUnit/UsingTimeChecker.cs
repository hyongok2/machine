using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Setting;

namespace EquipMainUi.Struct.BaseUnit
{
    public class UsingTimeChecker
    {
        public bool[] IsUsed { get; set; }
        public DateTime[] UsingTime { get; set; }
        public int[] Deadline { get; set; }
        private int _unitCount;

        public EM_AL_LST[] LAMP_SET_TIME_OVER { get; set; }

        public UsingTimeChecker(int unitCount)
        {
            _unitCount = unitCount;

            IsUsed = new bool[_unitCount];
            UsingTime = new DateTime[_unitCount];
            Deadline = new int[_unitCount];
            LAMP_SET_TIME_OVER = new EM_AL_LST[_unitCount];
        }
        public void InitilizeUsingTime(PcUsingTimeSetting UsingTimeSetting)
        {
            bool[] isUsed = {
                               Convert.ToBoolean(UsingTimeSetting.Lamp1_IsUsing),
                               Convert.ToBoolean(UsingTimeSetting.Lamp2_IsUsing),
                               Convert.ToBoolean(UsingTimeSetting.Lamp3_IsUsing),
                            };
            DateTime[] time = {
                                  Convert.ToDateTime(UsingTimeSetting.Lamp1),
                                  Convert.ToDateTime(UsingTimeSetting.Lamp2),
                                  Convert.ToDateTime(UsingTimeSetting.Lamp3)
                              };
            int[] deadline = {
                                UsingTimeSetting.Lamp1_Deadline, 
                                UsingTimeSetting.Lamp2_Deadline, 
                                UsingTimeSetting.Lamp3_Deadline 
                             };

            for (int iPos = 0; iPos < _unitCount; iPos++)
            {
                IsUsed[iPos] = isUsed[iPos];
                UsingTime[iPos] = time[iPos];
                Deadline[iPos] = deadline[iPos];
            }
        }
        public void Reset(int index)
        {
            UsingTime[index] = new DateTime();
        }
        public void LogicWorking(Equipment equip)
        {            
            for (int iPos = 0; iPos < _unitCount; iPos++)
            {
                if (IsUsed[iPos] == false) continue;
                
                TimeSpan interval = DateTime.Now - UsingTime[iPos];
                if (interval.TotalHours >= Deadline[iPos])
                {
                   // AlarmMgr.Instatnce.Happen(equip, LAMP_SET_TIME_OVER[iPos]);
                }
            }            
        }        
    }
}
