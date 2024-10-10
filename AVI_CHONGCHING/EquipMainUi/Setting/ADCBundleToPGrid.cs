using EquipMainUi.Struct;
using EquipMainUi.Struct.Detail;
using EquipMainUi.Struct.Detail.Adc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EquipMainUi.Setting
{
    class ADCBundleToPGrid
    {
        public ADCBundle AdcBundle;

        public ADCBundleToPGrid(ADCBundle adcBundle)
        {
            AdcBundle = adcBundle;
        }

        [ReadOnlyAttribute(true), DisplayName("Main Air 1 (MPa)"), DescriptionAttribute("(MPa)")]
        public float MainAir1 { get { return AdcBundle.MainAir1; } }
        [ReadOnlyAttribute(true), DisplayName("Main Air 2 (MPa)"), DescriptionAttribute("(MPa)")]
        public float MainAir2 { get { return AdcBundle.MainAir2; } }
        [ReadOnlyAttribute(true), DisplayName("Main Vacuum 1 (kPa)"), DescriptionAttribute("(kPa)")]
        public float MainVacuum1 { get { return AdcBundle.MainVacuum1; } }
        [ReadOnlyAttribute(true), DisplayName("Main Vacuum 2 (kPa)"), DescriptionAttribute("(kPa)")]
        public float MainVacuum2 { get { return AdcBundle.MainVacuum2; } }
        [ReadOnlyAttribute(true),  DisplayName("Check Vacuum 1 (kPa)"), DescriptionAttribute("(kPa)")]
        public float CheckVacuum1 { get { return AdcBundle.CheckVacuum1; } }
        [ReadOnlyAttribute(true), DisplayName("Check Vacuum 2 (kPa)"), DescriptionAttribute("(kPa)")]
        public float CheckVacuum2 { get { return AdcBundle.CheckVacuum2; } }
        [ReadOnlyAttribute(true), DisplayName("Ionizer 정전기 (V)"), DescriptionAttribute("(V)")]
        public float IonizerStaticElec { get { return AdcBundle.IonizerStaticElectricity; } }
        [ReadOnlyAttribute(true), DisplayName("CP BOX (℃)"), DescriptionAttribute("(℃)")]
        //public float PanelTemp { get { return AdcBundle.Temperature.ReadDataBuf[0]; } }
        //[ReadOnlyAttribute(true), DisplayName("PC RACK BOX (℃)"), DescriptionAttribute("(℃)")]
        //public float PCRackTemp { get { return AdcBundle.Temperature.ReadDataBuf[1]; } }

        public float PanelTemp { get { return AdcBundle.Temperature2.ReadDataBuf[0]; } }
        [ReadOnlyAttribute(true), DisplayName("PC RACK BOX (℃)"), DescriptionAttribute("(℃)")]
        public float PCRackTemp { get { return AdcBundle.Temperature2.ReadDataBuf[1]; } }
    }
}
