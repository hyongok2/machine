using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Analog;
using EquipMainUi.Struct.BaseUnit;

namespace EquipMainUi.Struct.Detail
{
    public class AdcTemperature : ADConverter_AJ65BT_64RD3
    {        
        public float AvgPanelTemp
        {
            get
            {
                if (_avgSampleTemp[0].Count > 0)
                    return _avgSampleTemp[0].Average();
                else
                    return ERROR_VALUE;
            }
        }
        public float AvgPCRackTemp
        {
            get
            {
                if (_avgSampleTemp[1].Count > 0)
                    return _avgSampleTemp[1].Average();
                else
                    return ERROR_VALUE;
            }
        }
        public EM_AL_LST[] ALARM_CODE { get; set; }
        public int[] LightAlarmOffset = null;
        private Queue<float>[] _avgSampleTemp = null;
        private int _avgParamMax = 100;
        private DateTime _lastestRecorded = DateTime.Now;

        public AdcTemperature(int unitCount)
            : base(unitCount)
        {
            ALARM_CODE = new EM_AL_LST[unitCount];
            LightAlarmOffset = new int[unitCount];
            _avgSampleTemp = new Queue<float>[unitCount];
            for (int iter = 0; iter < unitCount; ++iter)
                _avgSampleTemp[iter] = new Queue<float>(_avgParamMax);
        }

        public void LogicWorking(Equipment equip)
        {
            base.LogicWorking();

            //if ((DateTime.Now - _lastestRecorded).TotalMilliseconds > 1000)
            //{
            //    _lastestRecorded = DateTime.Now;
            //    for (int iter = 0; iter < _avgSampleTemp.Length; ++iter)
            //    {
            //        if (-50f <= ReadDataBuf[iter])
            //            _avgSampleTemp[iter].Enqueue(ReadDataBuf[iter]);
            //        if (_avgSampleTemp[iter].Count > _avgParamMax)
            //            _avgSampleTemp[iter].Dequeue();
            //    }
            //}

            //for (int iter = 0; iter < _avgSampleTemp.Length; ++iter)
            //{
            //    if (_avgSampleTemp[iter].Count > ((int)_avgParamMax / 10) &&
            //        (ReadDataBuf[iter] > _avgSampleTemp[iter].Average() + LightAlarmOffset[iter]))
            //        AlarmMgr.Instance.Happen(equip, ALARM_CODE[iter]);
            //}
        }
    }
}
