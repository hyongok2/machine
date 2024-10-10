using Dit.Framework.Analog;
using EquipMainUi.ConvenienceClass;
using EquipMainUi.Struct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EquipMainUi.Setting
{
    public partial class FrmAnalogSetting : Form
    {
        TabPage _showPage = null;

        Equipment _equip = null;

        public FrmAnalogSetting(Equipment equip)
        {
            InitializeComponent();
            ExtensionUI.AddClickEventLog(this);
            _equip = equip;
            ucrlSetting_AD1.ADVN = equip.ADC.Adc1;

            tabpCtrl_AnalogSetting_SelectedIndexChanged(tabpCtrl_AnalogSetting, null);

            tmrUiUpdate.Start();
        }
        private void tmrUiUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_showPage == tabp_AD1)
                {
                    ucrlSetting_AD1.UpdateUi();
                }
                if (_showPage == tabp_TD1)
                {

                }
            }
            catch (Exception ex)
            {
                if (AlarmMgr.Instance.IsHappened(GG.Equip, EM_AL_LST.AL_0946_UI_EXCEPTION) == false)
                {
                    Logger.ExceptionLog.AppendLine(LogLevel.Error, string.Format("UI 갱신 예외 발생 : {0}", ex.Message));
                    Logger.ExceptionLog.AppendLine(LogLevel.Error, Log.EquipStatusDump.CallStackLog());
                    AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0946_UI_EXCEPTION);
                }
            }
        }
        private void tabpCtrl_AnalogSetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage curPage = (sender as TabControl).SelectedTab;

            if (curPage == tabp_AD1)
            {
                ucrlSetting_AD1.LoadValue();
            }
            else if (curPage == tabp_TD1)
            {

            }
            _showPage = curPage;
        }
    }
}
