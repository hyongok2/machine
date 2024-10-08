using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipMainUi.Struct.BaseUnit;
using EquipMainUi.Setting;
using Dit.Framework.Analog;

namespace EquipMainUi.Struct.Detail.Adc
{
    /// <summary>
    /// adc/td/da 모두 모아서 하나의 클래스로 컨트롤
    /// date 20170604
    /// </summary>
    /// .
    public class ADCBundle
    {
        public ADCBundle(
            ADConverter_AJ65BT_64RD3 temp,
            ADConverter_AJ65BTCU_68ADVN adc1)
        {
            Temperature = temp;
            Adc1 = adc1;            
        }
        public ADConverter_AJ65BT_64RD3 Temperature { get; set; }
        public ADConverter_AJ65BTCU_68ADVN Adc1 { get; set; }

        public float CurCPBoxTemp
        {
            get
            {
                float f;
                float.TryParse(Temperature.ReadDataBuf[0].ToString("N2"), out f);

                return f;
            }
        }
        public float CurPCRackTemp
        {
            get
            {
                float f;
                float.TryParse(Temperature.ReadDataBuf[1].ToString("N2"), out f);

                return f;
            }
        }

        public float MainAir1 { get { return Adc1.ReadDataBuf[0]; } }
        public float MainAir2 { get { return Adc1.ReadDataBuf[1]; } }
        public float MainVacuum1 { get { return Adc1.ReadDataBuf[2]; } }
        public float MainVacuum2 { get { return Adc1.ReadDataBuf[3]; } }
        public float CheckVacuum1 { get { return Adc1.ReadDataBuf[4]; } }
        public float CheckVacuum2 { get { return Adc1.ReadDataBuf[5]; } }
        public float IonizerStaticElectricity { get { return Adc1.ReadDataBuf[6]; } }


        public void Initialize()
        {
            this.Adc1.ADSetting.Load(PcAnalogRegulatorSetting.PATH_SETTING_AD1);
            this.Temperature.TDSetting.Load(PcAnalogRegulatorSetting.PATH_SETTING_TD1);
        }

        public void LogicWorking(Equipment equip)
        {
            Temperature.LogicWorking();
            Adc1.LogicWorking();
        }
    }
}
