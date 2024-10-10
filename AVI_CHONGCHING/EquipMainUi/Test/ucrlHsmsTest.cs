using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EquipMainUi.Struct.Detail.HSMS;
using EquipMainUi.Struct.Detail.HSMS.ReportStruct;
using static EquipMainUi.Setting.PcCtrlSetting;
using static EquipMainUi.Struct.Detail.HSMS.FileInterface.ECIDDataSetting;
using System.IO;
using EquipMainUi.Struct.TransferData;
using EquipMainUi.Struct;

namespace EquipMainUi.Test
{
    public partial class ucrlHsmsTest : UserControl
    {
        private int _selCmd = -1;
        private int _selEvt = -1;

        public ucrlHsmsTest()
        {
            InitializeComponent();

            pGridHsmsSetting.SelectedObject = GG.Equip.CtrlSetting.Hsms;

            lblMemName.Text = ((Dit.Framework.PLC.VirtualShare)GG.MEM_DIT).MemoryName.ToString();

            foreach (string str in Enum.GetNames(typeof(EmHsmsPcCommand)))
                cmbCmd.Items.Add(str);

            foreach (string str in Enum.GetNames(typeof(EmHsmsPcEvent)))
                cmbEvt.Items.Add(str);

            tmrUiTimer.Enabled = true;
        }

        public void UIUpdate()
        {
            if (0 <= _selCmd && _selCmd < Enum.GetValues(typeof(EmHsmsPcCommand)).Length)
            {
                lblCmdCmd.Text  = GG.Equip.HsmsPc.LstCmd[_selCmd].YB_CMD.ToString();
                lblCmdCmd.OnOff = GG.Equip.HsmsPc.LstCmd[_selCmd].YB_CMD.vBit;
                lblCmdAck.Text  = GG.Equip.HsmsPc.LstCmd[_selCmd].XB_CMD_ACK.ToString();
                lblCmdAck.OnOff = GG.Equip.HsmsPc.LstCmd[_selCmd].XB_CMD_ACK.vBit;
                lblCmdStep.Text = GG.Equip.HsmsPc.LstCmd[_selCmd].Step.ToString();
            }
            else
            {
                lblCmdCmd.Text  = string.Empty;
                lblCmdCmd.OnOff = false;
                lblCmdAck.Text  = string.Empty;
                lblCmdAck.OnOff = false;
                lblCmdStep.Text = "NO SELECT";
            }

            if (0 <= _selEvt && _selEvt < Enum.GetValues(typeof(EmHsmsPcEvent)).Length)
            {
                lblEvtCmd.Text  = GG.Equip.HsmsPc.LstEvt[_selEvt].XB_EVENT.ToString();
                lblEvtCmd.OnOff = GG.Equip.HsmsPc.LstEvt[_selEvt].XB_EVENT.vBit;
                lblEvtAck.Text  = GG.Equip.HsmsPc.LstEvt[_selEvt].YB_EVENT_ACK.ToString();
                lblEvtAck.OnOff = GG.Equip.HsmsPc.LstEvt[_selEvt].YB_EVENT_ACK.vBit;
                lblEvtStep.Text = GG.Equip.HsmsPc.LstEvt[_selEvt].Step.ToString();
            }
            else
            {
                lblEvtCmd.Text  = string.Empty;
                lblEvtCmd.OnOff = false;
                lblEvtAck.Text  = string.Empty;
                lblEvtAck.OnOff = false;
                lblEvtStep.Text = "NO SELECT";
            }
        }

        private void cmbCmd_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selCmd = cmbCmd.SelectedIndex;
        }

        private void cmbEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selEvt = cmbEvt.SelectedIndex;
        }

        private int _rcpMode;
        private bool _alarmSetToggle;
        private void btnStartCmd_Click(object sender, EventArgs e)
        {
            object data;
            if (_selCmd == (int)EmHsmsPcCommand.ALARM_REPORT)
                data = new HsmsAlarmInfo() { IsSet = _alarmSetToggle = !_alarmSetToggle, ID = Struct.EM_AL_LST.AL_0771_ETC_CHMDA_COMPLETE_TIMEOVER, Desc = EM_AL_LST.AL_0771_ETC_CHMDA_COMPLETE_TIMEOVER.ToString() };
            else if (_selCmd == (int)EmHsmsPcCommand.CASSETTE_LOAD)
            {
                data = new HsmsCstLoadInfo()
                {
                    CstID = "CST_LOAD_TEST",
                    PortNo = 1,
                    CurrentRecipe = "CST_LOAD_RECIPE_TEST",
                };
            }
            else if (_selCmd == (int)EmHsmsPcCommand.LOT_START)
            {
                data = new CassetteInfo()
                {
                    CstID = "LOTSTART_CSTID_TEST",
                    LoadPortNo = 2,
                    RecipeName = "LOTSTART_RCP_TEST",
                    SlotCount = 7,
                };
            }
            else if(_selCmd == (int)EmHsmsPcCommand.WAFER_LOAD)
            {
                data = new WaferInfoKey()
                {
                    CstID = "WAEFR_LOAD_TEST",
                    SlotNo = 3,
                };

            }
            else if(_selCmd == (int)EmHsmsPcCommand.WAFER_MAP_REQUEST)
            {
                data = new WaferInfoKey()
                {
                    CstID = "WAFER_MAP_REQUEST",
                    SlotNo = 4,
                };
            }
            else if(_selCmd == (int)EmHsmsPcCommand.RECIPE_SELECT)
            {
                data = new HsmsRecipeInfo
                {
                    RecipeID = "PP_SELECT_ID",
                    RecipeMode = RecipeMode.PORT_2_RECIPE_SELECT,
                };
            }
            else
                data = 0;

            if (0 <= _selCmd && _selCmd < Enum.GetValues(typeof(EmHsmsPcCommand)).Length)
            {
                GG.Equip.HsmsPc.StartCommand(GG.Equip, (EmHsmsPcCommand)_selCmd, data);
            }
        }

        private void tmrUiTimer_Tick(object sender, EventArgs e)
        {
            UIUpdate();
        }

        private void btnHsmsSave_Click(object sender, EventArgs e)
        {
            GG.Equip.CtrlSetting.Hsms = pGridHsmsSetting.SelectedObject as HsmsSetting;
            GG.Equip.CtrlSetting.Save();            
        }

        private void btnEvent_Click(object sender, EventArgs e)
        {
            object data;
            if (_selEvt == (int)EmHsmsPcEvent.ECID_EDIT)
            {
                GG.Equip.HsmsPc.OnEcidEditEvent(GG.Equip, new HsmsPcEvent(EmHsmsPcEvent.ECID_EDIT));
            }
            else if (_selEvt == (int)EmHsmsPcEvent.MAP_FILE_CREATE)
            {
                GG.Equip.ReadWaferMap(GG.Equip.TransferUnit.LowerWaferKey);
            }
            else if (_selEvt == (int)EmHsmsPcEvent.CST_MAP)
            {
                GG.Equip.HsmsPc.OnCstMapEvent(GG.Equip, new HsmsPcEvent(EmHsmsPcEvent.CST_MAP));
            }
            else if(_selEvt == (int)EmHsmsPcEvent.OHT_MODE_CHANGE)
            {
                GG.Equip.HsmsPc.OnOHTModeChangeBeforeAck(GG.Equip, new HsmsPcEvent(EmHsmsPcEvent.OHT_MODE_CHANGE));
            }
        }

        private void btnHostECIDCheck_Click(object sender, EventArgs e)
        {
            string path = GG.Equip.CtrlSetting.Hsms.HostECIDSavePath;

            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            StreamReader streamReader = new StreamReader(path, Encoding.UTF8);

            rtbEcid.Clear();
            while(streamReader.Peek() > -1)
            {
                rtbEcid.AppendText(streamReader.ReadLine()+"\n");
            }
        }
    }
}
