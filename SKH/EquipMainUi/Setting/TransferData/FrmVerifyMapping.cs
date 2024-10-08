using EquipMainUi.Struct;
using EquipMainUi.Struct.Detail.EFEM;
using EquipMainUi.Struct.TransferData;
using EquipMainUi.UserMessageBoxes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMainUi.Setting.TransferData
{
    public partial class FrmVerifyMapping : Form
    {
        public EmPopupFlow PopupFlow;
        private string _targetCstID;
        private EmEfemMappingInfo[] _curMap = new EmEfemMappingInfo[13];
        public FrmVerifyMapping(string title)
        {
            InitializeComponent();
            lblPortTitle.Text = title;
            pGridBakMap.Enabled = false;
            pGridCurMap.Enabled = true;
        }

        private bool IsAlignerDupl { get { return GG.Equip.Efem.Aligner.LowerWaferKey.CstID == _targetCstID && _curMap[GG.Equip.Efem.Aligner.LowerWaferKey.SlotNo - 1] != EmEfemMappingInfo.Absence; } }
        private bool IsAVIDupl { get { return GG.Equip.TransferUnit.LowerWaferKey.CstID == _targetCstID && _curMap[GG.Equip.TransferUnit.LowerWaferKey.SlotNo - 1] != EmEfemMappingInfo.Absence; } }
        private bool IsUpperDupl { get { return GG.Equip.Efem.Robot.UpperWaferKey.CstID == _targetCstID && _curMap[GG.Equip.Efem.Robot.UpperWaferKey.SlotNo - 1] != EmEfemMappingInfo.Absence; } }
        private bool IsLowerDupl { get { return GG.Equip.Efem.Robot.LowerWaferKey.CstID == _targetCstID && _curMap[GG.Equip.Efem.Robot.LowerWaferKey.SlotNo - 1] != EmEfemMappingInfo.Absence; } }
        private void _tmrUiUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                bool err = false;
                lblAligner1.Text = GG.Equip.Efem.Aligner.LowerWaferKey.CstID;
                lblAligner2.Text = GG.Equip.Efem.Aligner.LowerWaferKey.SlotNo.ToString();
                err = IsAlignerDupl;
                lblAligner4.Text = err ? "ERROR" : "NO ERROR";
                lblAligner4.BackColor = err ? Color.Red : Color.WhiteSmoke;

                lblAVI1.Text = GG.Equip.TransferUnit.LowerWaferKey.CstID;
                lblAVI2.Text = GG.Equip.TransferUnit.LowerWaferKey.SlotNo.ToString();
                err = IsAVIDupl;
                lblAVI4.Text = err ? "ERROR" : "NO ERROR";
                lblAVI4.BackColor = err ? Color.Red : Color.WhiteSmoke;

                lblUpper1.Text = GG.Equip.Efem.Robot.UpperWaferKey.CstID;
                lblUpper2.Text = GG.Equip.Efem.Robot.UpperWaferKey.SlotNo.ToString();
                err = IsUpperDupl;
                lblUpper4.Text = err ? "ERROR" : "NO ERROR";
                lblUpper4.BackColor = err ? Color.Red : Color.WhiteSmoke;

                lblLower1.Text = GG.Equip.Efem.Robot.LowerWaferKey.CstID;
                lblLower2.Text = GG.Equip.Efem.Robot.LowerWaferKey.SlotNo.ToString();
                err = IsLowerDupl;
                lblLower4.Text = err ? "ERROR" : "NO ERROR";
                lblLower4.BackColor = err ? Color.Red : Color.WhiteSmoke;
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

        public void RequestPopup(EmEfemMappingInfo[] curMap, string bakCstId)
        {
            Array.Copy(curMap, _curMap, 13);
            _targetCstID = bakCstId;

            UpdateBakMap();
            pGridCurMap.SelectedObject = new MappArrToPGrid(_curMap);
            _tmrUiUpdate.Enabled = true;
            PopupFlow = EmPopupFlow.PopupRequest;
        }

        private void UpdateBakMap()
        {
            EmEfemMappingInfo[] bakMap = new EmEfemMappingInfo[13];
            for (int i = 0; i < bakMap.Length; ++i)
            {
                try
                {
                    bakMap[i] = TransferDataMgr.GetWafer(_targetCstID, i + 1).Status;
                }
                catch (Exception ex)
                {
                    bakMap[i] = EmEfemMappingInfo.Unknown;
                }
            }

            pGridBakMap.SelectedObject = new MappArrToPGrid(bakMap);
        }

        private void btnShift_Click(object sender, EventArgs e)
        {
            GG.Equip.FrmDataMgr.Show();
        }

        private void FrmVeifyMapping_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tmrUiUpdate.Enabled = false;
            e.Cancel = true;
            base.OnClosing(e);
            Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            PopupFlow = EmPopupFlow.NG;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (IsAlignerDupl
                || IsAVIDupl
                || IsUpperDupl
                || IsLowerDupl)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "因Slot 为止重复，需修改." : "Slot 위치 중복되어 수정이 필요합니다.");
                return;
            }

            PopupFlow = EmPopupFlow.OK;
            Close();
        }

        private void btnInsertCurrentMap_Click(object sender, EventArgs e)
        {            
            StringBuilder sb = new StringBuilder();
            MappArrToPGrid map = pGridCurMap.SelectedObject as MappArrToPGrid;

            sb.Append("USER EDIT WAFER INFO 1~13 IN CST (BEFORE : ");
            foreach (EmEfemMappingInfo m in _curMap)
                sb.AppendFormat(" {0}", m.ToString());
            sb.Append(")");
            Logger.TransferDataLog.AppendLine(LogLevel.NoLog, sb.ToString());

            Array.Copy(map.Get(), _curMap, 13);

            sb.Clear();
            sb.Append("USER EDIT WAFER INFO 1~13 IN CST (_AFTER : ");
            foreach (EmEfemMappingInfo m in _curMap)
                sb.AppendFormat(" {0}", m.ToString());
            sb.Append(")");
            Logger.TransferDataLog.AppendLine(LogLevel.NoLog, sb.ToString());

            WaferInfo tempW;

            for (int i = 0; i < _curMap.Length; ++i)
            {
                try
                {
                    tempW = TransferDataMgr.GetWafer(_targetCstID, i + 1);
                    tempW.Status = _curMap[i];                    
                    if (_curMap[i] == EmEfemMappingInfo.Absence)
                        tempW.IsComeBack = true;
                    tempW.Update();
                }
                catch (Exception ex)
                {
                    Logger.ExceptionLog.AppendLine(LogLevel.NoLog, "InsertCurrentMap_Click");
                }
            }

            UpdateBakMap();
        }
    }  
}
