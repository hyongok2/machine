using EquipMainUi.Setting.TransferData;
using EquipMainUi.Struct;
using EquipMainUi.Struct.Detail.EFEM;
using EquipMainUi.Struct.TransferData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMainUi.Setting
{
    public partial class FrmTransferDataMgr : Form
    {
        public FrmTransferDataMgr()
        {
            InitializeComponent();

            
            
            tmrUiUpdate.Tick += tmrUiUpdate_Tick;
            tmrUiUpdate.Start();
        }

        private void tmrUiUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.Visible == true)
                    SetInfo();
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
        private void SetInfo()
        {
            ucrlAlignerKeyInfo.SetInfo(EmEfemDBPort.ALIGNER, GG.Equip.Efem.Aligner.LowerWaferKey, GG.Equip.Efem.Aligner.Status.IsWaferExist);
            ucrlUpperKeyInfo.SetInfo(EmEfemDBPort.UPPERROBOT, GG.Equip.Efem.Robot.UpperWaferKey, GG.Equip.Efem.Robot.Status.IsUpperArmVacOn);
            ucrlLowerKeyInfo.SetInfo(EmEfemDBPort.LOWERROBOT, GG.Equip.Efem.Robot.LowerWaferKey, GG.Equip.Efem.Robot.Status.IsLowerArmVacOn);
            ucrlAVIKeyInfo.SetInfo(EmEfemDBPort.EQUIPMENT, GG.Equip.TransferUnit.LowerWaferKey, GG.Equip.IsWaferDetect != Struct.EmGlassDetect.NOT);

            ucrlLPM1keyInfo.SetInfo(EmEfemDBPort.LOADPORT1, GG.Equip.Efem.LoadPort1.CstKey, false);
            ucrlLPM2keyInfo.SetInfo(EmEfemDBPort.LOADPORT2, GG.Equip.Efem.LoadPort2.CstKey, false);
        }

        private WaferInfoKey _selectedKey;
        private void rbFrom_click(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb == rbFromAligner)
            {
                lblFromPort.Text = "Aligner";
                lblFromCstID.Text = GG.Equip.Efem.Aligner.LowerWaferKey.CstID;
                lblFromSlot.Text = GG.Equip.Efem.Aligner.LowerWaferKey.SlotNo.ToString();
                pGrid.SelectedObject = TransferDataMgr.GetWafer(GG.Equip.Efem.Aligner.LowerWaferKey);
                _selectedKey = GG.Equip.Efem.Aligner.LowerWaferKey;
            }
            else if (rb == rbFromRobotUpper)
            {
                lblFromPort.Text = "Upper";
                lblFromCstID.Text = GG.Equip.Efem.Robot.UpperWaferKey.CstID;
                lblFromSlot.Text = GG.Equip.Efem.Robot.UpperWaferKey.SlotNo.ToString();
                pGrid.SelectedObject = TransferDataMgr.GetWafer(GG.Equip.Efem.Robot.UpperWaferKey);
                _selectedKey = GG.Equip.Efem.Robot.UpperWaferKey;
            }
            else if (rb == rbFromRobotLower)
            {
                lblFromPort.Text = "Lower";
                lblFromCstID.Text = GG.Equip.Efem.Robot.LowerWaferKey.CstID;
                lblFromSlot.Text = GG.Equip.Efem.Robot.LowerWaferKey.SlotNo.ToString();
                pGrid.SelectedObject = TransferDataMgr.GetWafer(GG.Equip.Efem.Robot.LowerWaferKey);
                _selectedKey = GG.Equip.Efem.Robot.LowerWaferKey;
            }
            else if (rb == rbFromAVI)
            {
                lblFromPort.Text = "AVI";
                lblFromCstID.Text = GG.Equip.TransferUnit.LowerWaferKey.CstID;
                lblFromSlot.Text = GG.Equip.TransferUnit.LowerWaferKey.SlotNo.ToString();
                pGrid.SelectedObject = TransferDataMgr.GetWafer(GG.Equip.TransferUnit.LowerWaferKey);
                _selectedKey = GG.Equip.TransferUnit.LowerWaferKey;
            }
            else if(rb==rbFromLPM1)
            {
                lblFromPort.Text = "LPM1";
                lblFromCstID.Text = GG.Equip.Efem.LoadPort1.CstKey.ID;
                lblFromSlot.Text = "";
                pGrid.SelectedObject = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort1.CstKey);
            }
            else if (rb == rbFromLPM2)
            {
                lblFromPort.Text = "LPM2";
                lblFromCstID.Text = GG.Equip.Efem.LoadPort1.CstKey.ID;
                lblFromSlot.Text = "";
                pGrid.SelectedObject = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort1.CstKey);
            }
        }
        private void rbTo_click(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb == rbToAligner)
            {
                lblToPort.Text = "Aligner";
                lblToCstID.Text = GG.Equip.Efem.Aligner.LowerWaferKey.CstID;
                lblToSlot.Text = GG.Equip.Efem.Aligner.LowerWaferKey.SlotNo.ToString();
            }
            else if (rb == rbToRobotUpper)
            {
                lblToPort.Text = "Upper";
                lblToCstID.Text = GG.Equip.Efem.Robot.UpperWaferKey.CstID;
                lblToSlot.Text = GG.Equip.Efem.Robot.UpperWaferKey.SlotNo.ToString();
            }
            else if (rb == rbToRobotLower)
            {
                lblToPort.Text = "Lower";
                lblToCstID.Text = GG.Equip.Efem.Robot.LowerWaferKey.CstID;
                lblToSlot.Text = GG.Equip.Efem.Robot.LowerWaferKey.SlotNo.ToString();
            }
            else if (rb == rbToAVI)
            {
                lblToPort.Text = "AVI";
                lblToCstID.Text = GG.Equip.TransferUnit.LowerWaferKey.CstID;
                lblToSlot.Text = GG.Equip.TransferUnit.LowerWaferKey.SlotNo.ToString();
            }
        }
        private void btnShift_Click(object sender, EventArgs e)
        {
            EmEfemDBPort FromPort = rbFromAligner.Checked ? EmEfemDBPort.ALIGNER : rbFromAVI.Checked ? EmEfemDBPort.EQUIPMENT : rbFromRobotUpper.Checked ? EmEfemDBPort.UPPERROBOT : rbFromRobotLower.Checked ? EmEfemDBPort.LOWERROBOT :
                rbFromLPM1.Checked ? EmEfemDBPort.LOADPORT1 : rbFromLPM2.Checked ? EmEfemDBPort.LOADPORT2 : EmEfemDBPort.NONE;
            EmEfemDBPort Toport = rbToAligner.Checked ? EmEfemDBPort.ALIGNER : rbToAVI.Checked ? EmEfemDBPort.EQUIPMENT : rbToRobotUpper.Checked ? EmEfemDBPort.UPPERROBOT : rbToRobotLower.Checked ? EmEfemDBPort.LOWERROBOT :
                rbToLPM1.Checked ? EmEfemDBPort.LOADPORT1 : rbToLPM2.Checked ? EmEfemDBPort.LOADPORT2 : EmEfemDBPort.NONE;
            if(pGrid.SelectedObject as WaferInfo == null)
            {
                return;
            }
            WaferInfoKey key = WaferInfoKey.GetKey(pGrid.SelectedObject as WaferInfo);
            if(rbFromLPM1.Checked || rbFromLPM2.Checked)
            {
                if(TransferDataMgr.LPMToRobotShift(key, Toport))
                {
                    TransferDataMgr.UpdateWaferKey();

                    SetInfo();

                    Logger.TransferDataLog.AppendLine(LogLevel.Info, "WaferInfo Shift [" + FromPort.ToString() + " -> " + Toport.ToString());
                }
            }
            else if(rbToLPM1.Checked)
            {
                if (TransferDataMgr.RobotToLPMShift(key, Toport))
                {
                    TransferDataMgr.UpdateWaferKey();

                    SetInfo();

                    Logger.TransferDataLog.AppendLine(LogLevel.Info, "WaferInfo Shift [" + FromPort.ToString() + " -> " + Toport.ToString());
                }
            }
            else if (rbToLPM2.Checked)
            {
                if (TransferDataMgr.RobotToLPMShift(key, Toport))
                {
                    TransferDataMgr.UpdateWaferKey();

                    SetInfo();

                    Logger.TransferDataLog.AppendLine(LogLevel.Info, "WaferInfo Shift [" + FromPort.ToString() + " -> " + Toport.ToString());
                }
            }
            else
            {
                if (TransferDataMgr.Shift(key, Toport))
                {
                    TransferDataMgr.UpdateWaferKey();

                    SetInfo();

                    Logger.TransferDataLog.AppendLine(LogLevel.Info, "WaferInfo Shift [" + FromPort.ToString() + " -> " + Toport.ToString());
                }
            }
        }

        static WaferInfoKey lstClearKey = null;
        static EmEfemDBPort lstClearPort = EmEfemDBPort.NONE;
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (pGrid.SelectedObject == null && _selectedKey != null)
            {
                if (TransferDataMgr.DeleteBackUpKey(_selectedKey) == true)
                {
                    EmEfemDBPort port = rbFromAligner.Checked ? EmEfemDBPort.ALIGNER : rbFromAVI.Checked ? EmEfemDBPort.EQUIPMENT : rbFromRobotUpper.Checked ? EmEfemDBPort.UPPERROBOT : rbFromRobotLower.Checked ? EmEfemDBPort.LOWERROBOT : EmEfemDBPort.NONE;

                    lstClearKey = _selectedKey;
                    lstClearPort = port;

                    TransferDataMgr.UpdateWaferKey();
                    OnlyNoWaferModeChangeWaferExist(port, false);
                }
                _selectedKey = null;
                
            }
            else if(pGrid.SelectedObject == null && _selectedKey == null)
            {
                EmEfemDBPort port = rbFromAligner.Checked ? EmEfemDBPort.ALIGNER : rbFromAVI.Checked ? EmEfemDBPort.EQUIPMENT : rbFromRobotUpper.Checked ? EmEfemDBPort.UPPERROBOT : rbFromRobotLower.Checked ? EmEfemDBPort.LOWERROBOT : EmEfemDBPort.NONE;
                OnlyNoWaferModeChangeWaferExist(port, false);

                MessageBox.Show("Clear 실패!");
                return;
            }
            else
            {
                WaferInfoKey key = WaferInfoKey.GetKey(pGrid.SelectedObject as WaferInfo);
                if (TransferDataMgr.DeleteBackUpKey(key) == true)
                {
                    EmEfemDBPort port = rbFromAligner.Checked ? EmEfemDBPort.ALIGNER : rbFromAVI.Checked ? EmEfemDBPort.EQUIPMENT : rbFromRobotUpper.Checked ? EmEfemDBPort.UPPERROBOT : rbFromRobotLower.Checked ? EmEfemDBPort.LOWERROBOT : EmEfemDBPort.NONE;

                    lstClearKey = key;
                    lstClearPort = port;

                    TransferDataMgr.UpdateWaferKey();
                    OnlyNoWaferModeChangeWaferExist(port, false);
                }
            }
            Logger.TransferDataLog.AppendLine(LogLevel.Info, lstClearPort.ToString() + " WaferInfo Deleted");
        }
        private void OnlyNoWaferModeChangeWaferExist(EmEfemDBPort port, bool isExist)
        {
            if (GG.EfemNoWafer == true)
            {
                switch (port)
                {
                    case EmEfemDBPort.ALIGNER: GG.Equip.Efem.Aligner.Status.IsWaferExist = isExist; break;
                    case EmEfemDBPort.EQUIPMENT: GG.Equip.IsWaferDetect = isExist ? Struct.EmGlassDetect.SOME : Struct.EmGlassDetect.NOT; break;
                    case EmEfemDBPort.LOWERROBOT: GG.Equip.Efem.Robot.Status.IsLowerArmVacOn = isExist; break;
                    case EmEfemDBPort.UPPERROBOT: GG.Equip.Efem.Robot.Status.IsUpperArmVacOn = isExist; break;
                }
            }
        }
        private void btnRestore_Click(object sender, EventArgs e)
        {
            if(lstClearKey != null)
            {
                TransferDataMgr.RecoveryData(lstClearKey, lstClearPort);

                TransferDataMgr.UpdateWaferKey();
                SetInfo();
                OnlyNoWaferModeChangeWaferExist(lstClearPort, true);
            }
            Logger.TransferDataLog.AppendLine(LogLevel.Info, lstClearPort.ToString() + " WaferInfo Restored");
        }

        private void FrmTransferDataMgr_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
            Hide();
        }

        private void btnLoadPortMgrClear_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btnLoadPortMgrClear)
            {
                if (rdLoadPortMgr1.Checked)
                {
                    _bakCstID = GG.Equip.Efem.LoadPort1.CstKey.ID;
                    _bakCstLoadPortNum = 1;
                    GG.Equip.Efem.LoadPort1.CstKey.Clear();                    
                }
                else if (rdLoadPortMgr2.Checked)
                {
                    _bakCstID = GG.Equip.Efem.LoadPort2.CstKey.ID;
                    _bakCstLoadPortNum = 2;
                    GG.Equip.Efem.LoadPort2.CstKey.Clear();
                }
            }
            else if (btn == btnLoadPortMgrRestore)
            {
                if (_bakCstLoadPortNum == 1)
                    GG.Equip.Efem.LoadPort1.CstKey.ID = _bakCstID;
                else if (_bakCstLoadPortNum == 2)
                    GG.Equip.Efem.LoadPort2.CstKey.ID = _bakCstID;
            }
        }

        private int _bakCstLoadPortNum;
        private string _bakCstID = string.Empty;
        private void rdLoadPortMgr1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = sender as RadioButton;

            if (rd.Checked)
            {
                if (rd == rdLoadPortMgr1)
                {
                    lblLoadPortMgrID.Text = GG.Equip.Efem.LoadPort1.CstKey.ID;
                    if (GG.Equip.Efem.LoadPort1.CstKey.ID != string.Empty)
                        lblLoadPortMgrPort.Text = "LoadPort1";
                }
                else if (rd == rdLoadPortMgr2)
                {
                    lblLoadPortMgrID.Text = GG.Equip.Efem.LoadPort2.CstKey.ID;
                    if (GG.Equip.Efem.LoadPort1.CstKey.ID != string.Empty)
                        lblLoadPortMgrPort.Text = "LoadPort2";
                }
            }
        }
        
        private void cbFromLPM1Slot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFromLPM1Slot.SelectedIndex >=0)
            {
                rbFromLPM1.Checked = true;
                WaferInfo waferInfo = TransferDataMgr.GetWafer(GG.Equip.Efem.LoadPort1.CstKey.ID, (cbFromLPM1Slot.SelectedIndex + 1));
                pGrid.SelectedObject = (waferInfo.CstID == "") ?  null : waferInfo;
            }
        }
        private void cbFromLPM2Slot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFromLPM2Slot.SelectedIndex >= 0)
            {
                rbFromLPM2.Checked = true;
                WaferInfo waferInfo = TransferDataMgr.GetWafer(GG.Equip.Efem.LoadPort2.CstKey.ID, (cbFromLPM2Slot.SelectedIndex + 1));
                pGrid.SelectedObject = (waferInfo.CstID == "") ? null : waferInfo;
            }
        }

        private void ucrlkeyInfo_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                UcrlTransferDataKeyInfo ucrl = sender as UcrlTransferDataKeyInfo;
                WaferInfoKey w = ucrl.GetInfo();
                if(w!= null)
                {
                    DoDragDrop(w, DragDropEffects.Move);
                }
                
            }
            else if(e.Button == MouseButtons.Right)
            {
                UcrlTransferDataKeyInfo ucrl = sender as UcrlTransferDataKeyInfo;
                WaferInfoKey w = ucrl.GetInfo();

                EventHandler eh = new EventHandler(MenuClick);
                MenuItem[] ami = {
                    new MenuItem("삭제",eh) { Tag = w},
                    new MenuItem("전체 삭제",eh) {Tag = w },
                };
                ContextMenu = new System.Windows.Forms.ContextMenu(ami);
            }
        }

        private void MenuClick(object sender, EventArgs e)
        {
            MenuItem mI = (MenuItem)sender;
            string str = mI.Text;
            WaferInfoKey wKey = mI.Tag as WaferInfoKey;
            
            if (str == "삭제" && wKey != null)
            {
                TransferDataMgr.DeleteBackUpKey(wKey);
                TransferDataMgr.UpdateWaferKey();
            }
            if (str == "전체 삭제")
            {
                TransferDataMgr.DeleteAllBackUpKey();
                TransferDataMgr.UpdateWaferKey();
            }
        }

        private void ucrlKeyInfo_DragDrop(object sender, DragEventArgs e)
        {
            WaferInfoKey key = e.Data.GetData(typeof(WaferInfoKey)) as WaferInfoKey;

            UcrlTransferDataKeyInfo ucrl = sender as UcrlTransferDataKeyInfo;
            //ucrl.SetInfo(EmEfemDBPort.UPPERROBOT, w, false);

            EmEfemDBPort targetPort = EmEfemDBPort.NONE;
            switch (ucrl.Name)
            {
                case "ucrlUpperKeyInfo":
                    targetPort = EmEfemDBPort.UPPERROBOT;
                    break;
                case "ucrlLowerKeyInfo":
                    targetPort = EmEfemDBPort.LOWERROBOT;
                    break;
                case "ucrlAlignerKeyInfo":
                    targetPort = EmEfemDBPort.ALIGNER;
                    break;
                case "ucrlAVIKeyInfo":
                    targetPort = EmEfemDBPort.EQUIPMENT;
                    break;
            }

            TransferDataMgr.Shift(key, targetPort);
            TransferDataMgr.UpdateWaferKey();
            SetInfo();

            Logger.TransferDataLog.AppendLine(LogLevel.Info, "WaferInfo Shift to " +  targetPort.ToString());
        }

        private void ucrlKeyInfo_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        
    }
}
