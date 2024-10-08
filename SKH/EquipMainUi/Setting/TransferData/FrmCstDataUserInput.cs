using EquipMainUi.Monitor;
using EquipMainUi.Setting;
using EquipMainUi.Struct;
using EquipMainUi.Struct.Detail.EFEM;
using EquipMainUi.Struct.TransferData;
using EquipMainUi.UserMessageBoxes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EquipMainUi.Setting.TransferData
{
    public partial class FrmCstDataUserInput : Form
    {
        private bool _isNewCst;
        private EmReaderTypes ReaderType;
        private string _selectedCstID;
        private RFIDController _rf;
        private List<WaferInfo> _wafers;
        private EmEfemPort _port;
        public EmPopupFlow PopupFlow;
        public string ReturnValue;
        public int LastSlotNo = -1;

        string strChinaLanguage1 = "";
        string strChinaLanguage2 = "";

        private void CommonInit()
        {
            InitializeComponent();
            this.TopMost = true;
        }
        public FrmCstDataUserInput(Equipment equip, EmEfemPort port, RFIDController rf)
        {
            CommonInit();
            ReaderType = EmReaderTypes.RF;
            _rf = rf;
            _port = port;

            lblTitle.Text = _port.ToString() + " Cassette 정보 입력";
            ChangeChinaLanguage();
        }
        private void ChangeChinaLanguage()
        {
            if (GG.boChinaLanguage)
            {
                lblTitle.Text = _port.ToString() + " 输入Cassette 情报";         // Cassette 정보 입력
                label7.Text = "直接输入";                               // 직접입력
                btnChangeCstID.Text = "ID 变更";        	            // ID 변경
                btnDelete.Text = "删除现有情报";        	                // 기존정보 삭제
                btnRead.Text = "重新读取";                              // 다시읽기
                lblCaution.Text = "用已经进行的 Cassette，确认左侧现在的Cassette Info设置";                           // 이미 진행했던 카세트로 좌측의 현재 카세트 정보를 확인하여 설정바랍니다.
                strChinaLanguage1 = "用已经进行的CST确认左侧现有CST，在右侧进行设置\n(Load Port上没有的，以没有Check解除)";                         // 이미 진행했던 카세트로 좌측의 기존 카세트 정보를 확인하여 우측에 진행 설정바랍니다.\n(LoadPort에 없는 건 없는 것으로 체크해제)
                strChinaLanguage2 = "新的CST,请进行设置";                         // 새로운 카세트입니다. 진행 설정바랍니다.

                lblLeftTitle.Text = "现有 Cassette 状态";        		            // 기존 카세트 상태
                label12.Text = "进行设置";                              // 진행 설정

                dataGridViewCheckBoxColumn1.HeaderText = "Wafer 存在";    // Wafer있음
                // 다시 번역 요청 필요
                colComplete.HeaderText = "检查结束";                    // 검사완료
                colComaback.HeaderText = "返回完成";                    // 복귀완료
                colCurPos.HeaderText = "当前位置";                      // 현재위치

                colWaferExist.HeaderText = "Wafer 存在";                  // Wafer있음
                colNotch.HeaderText = "Notch 方向";            	        // Notch방향
                colNoComback.HeaderText = "进行";                   // 진행

                btnCopyIncompleteWafer.Text = "未检查的Wafer再检查 ▶▶";            	// 미검사 웨이퍼 재검사 ▶▶
                btnCopyAllWafer.Text = "全部再检查 ▶▶";                      // 전체 재검사 ▶▶
                btnOK.Text = "确认";                                // 확인
            }
            else
            {
                lblTitle.Text = _port.ToString() + " Cassette 정보 입력";
            }
        }
        public void RequestPopup(string selectedCstId, ref List<WaferInfo> waferInfos)
        {
            try
            {
                string rand = DateTime.Now.ToString("yyMMddHHmmssfff");
                bool exist;
                _wafers = waferInfos;
                txtRead1.Text = _rf.GetReaderReadID[0];
                //txtRead2.Text = _rf.GetReaderReadID[1]; //RFID제거
                if (_rf.GetReaderReadID[0] != string.Empty || _rf.GetReaderReadID[1] != string.Empty)
                {
                    txtInput1.Text = rand;
                }
                if (selectedCstId == string.Empty)
                    selectedCstId = rand;

                if (dgvWaferData.Rows.Count != 0)
                    dgvWaferData.Rows.Clear();

                foreach (WaferInfo w in _wafers)
                {
                    exist = w.Status != EmEfemMappingInfo.Absence;
                    dgvWaferData.Rows.Insert(0, new object[] { w.SlotNo, exist, "Bottom", exist });
                }
                ChangeCstID(selectedCstId);

                PopupFlow = EmPopupFlow.PopupRequest;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog.AppendLine(LogLevel.NoLog, "FrmCstDataUserInput.Request.Popup Excpetion");
            }
        }

        public void InitWafers(ref List<WaferInfo> waferInfos)
        {
            waferInfos.Clear();
            for (int i = 0; i < 13; ++i)
                waferInfos.Add(new WaferInfo("TEMP", i + 1, EmEfemMappingInfo.Absence));
        }

        private void ChangeCstID(string cstId)
        {
            if (cstId == string.Empty)
                return;

            EmEfemDBPort waferCurPort;
            lblCstID.Text = _selectedCstID = cstId;
            if (TransferDataMgr.IsExistCst(_selectedCstID))
            {
                _isNewCst = false;
                lblCaution.Text = GG.boChinaLanguage ? strChinaLanguage1 : "이미 진행했던 카세트로 좌측의 기존 카세트 정보를 확인하여 우측에 진행 설정바랍니다.\n(LoadPort에 없는 건 없는 것으로 체크해제)";
                btnDelete.Enabled = true;
                dgvOldWaferInfo.Rows.Clear();
                List<WaferInfo> wafers = TransferDataMgr.GetWafersIn(_selectedCstID);                
                for (int i = 0; i < wafers.Count; ++i)
                {
                    waferCurPort = GetWaferPos(wafers[i], _port);
                    dgvOldWaferInfo.Rows.Insert(0, new object[] {
                        wafers[i].SlotNo,
                        wafers[i].Status != EmEfemMappingInfo.Absence,
                        wafers[i].IsInspComplete,
                        wafers[i].IsComeBack,
                        waferCurPort
                    });
                    dgvOldWaferInfo.Rows[0].DefaultCellStyle.BackColor = Color.DarkGray;
                    if (waferCurPort != EmEfemDBPort.LOADPORT1 && waferCurPort != EmEfemDBPort.LOADPORT2)
                    {
                        dgvWaferData.Rows[13 - wafers[i].SlotNo].ReadOnly = true;
                        dgvWaferData.Rows[13 - wafers[i].SlotNo].DefaultCellStyle.BackColor = Color.DarkGray;
                    }
                    else
                    {
                        dgvWaferData.Rows[13 - wafers[i].SlotNo].ReadOnly = false;
                        dgvWaferData.Rows[13 - wafers[i].SlotNo].DefaultCellStyle.BackColor = Color.Empty;
                    }
                }
            }
            else
            {
                _isNewCst = true;
                lblCaution.Text = GG.boChinaLanguage ? strChinaLanguage2 : "새로운 카세트입니다. 진행 설정바랍니다.";
                btnDelete.Enabled = false;
            }
        }
        protected override void OnActivated(EventArgs e)
        {
            ShowOldWaferInfo(!_isNewCst);            
            base.OnActivated(e);
        }

        private void ShowOldWaferInfo(bool v)
        {
            dgvOldWaferInfo.Visible = lblLeftTitle.Visible = 
                btnCopyAllWafer.Visible = btnCopyIncompleteWafer.Visible =
                v;
        }

        public EmEfemDBPort GetWaferPos(WaferInfo w, EmEfemPort defaultPort)
        {
            WaferInfoKey a, e, l, u;
            a = GG.Equip.Efem.Aligner.LowerWaferKey;
            e = GG.Equip.TransferUnit.LowerWaferKey;
            l = GG.Equip.Efem.Robot.LowerWaferKey;
            u = GG.Equip.Efem.Robot.UpperWaferKey;

            if (a.CstID == w.CstID && a.SlotNo == w.SlotNo) return EmEfemDBPort.ALIGNER;
            if (e.CstID == w.CstID && e.SlotNo == w.SlotNo) return EmEfemDBPort.EQUIPMENT;
            if (l.CstID == w.CstID && l.SlotNo == w.SlotNo) return EmEfemDBPort.LOWERROBOT;
            if (u.CstID == w.CstID && u.SlotNo == w.SlotNo) return EmEfemDBPort.UPPERROBOT;

            if (defaultPort == EmEfemPort.LOADPORT1) return EmEfemDBPort.LOADPORT1;
            if (defaultPort == EmEfemPort.LOADPORT2) return EmEfemDBPort.LOADPORT2;
            else return EmEfemDBPort.NONE;
        }

        private Stopwatch _stopwatch = new Stopwatch();
        private int _readTimeover = 500;
        public void ReadCmd()
        {
            string a = string.Empty;
            string b = string.Empty;
            txtRead1.Text = "";
            //txtRead2.Text = ""; //RFID제거
            ReturnValue = string.Empty;
            _rf.ScanTagCmd(2);
            lblNotify.Text = "Reading";
            _stopwatch.Restart();
            tmrUiUpdate.Enabled = true;
        }
        public void UpdateReadValue()
        {
            txtInput1.Text = txtRead1.Text = _rf.GetReaderReadID[0];
            //txtRead2.Text = _rf.GetReaderReadID[1]; //RFID제거
        }
        public bool IsReadComplete(int idx)
        {
            return idx == 0 ? _rf.IsReader1Success() : _rf.IsReader1Success();
        }
        private void tmrUiUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (IsReadComplete(0) || IsReadComplete(1))
                {
                    lblNotify.Text = "Complete";
                    UpdateReadValue();
                    tmrUiUpdate.Enabled = false;
                }
                else if (_stopwatch.ElapsedMilliseconds > _readTimeover)
                {
                    _stopwatch.Stop();
                    string a = "Timeover ";
                    a += IsReadComplete(0) ? "[1]" : "";
                    a += IsReadComplete(1) ? "[2]" : "";
                    lblNotify.Text = a;
                    tmrUiUpdate.Enabled = false;
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

    private void btnRead_Click(object sender, EventArgs e)
        {
            ReadCmd();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (GG.Equip.EquipRunMode == EmEquipRunMode.Manual)
            {
                Close();
                return;
            }

            if (IsFormatOK(_selectedCstID) == false)
            {
                lblNotify.Text = GG.boChinaLanguage ? "Cassette ID Format 问题 发生" : "Cassette ID Format 이상";
                return;
            }
            ReturnValue = _selectedCstID;

            bool isNoRun = false, isNoExistYesRunError = false;
            NewDgvToWaferList(ref _wafers, ref dgvWaferData, out isNoRun, out isNoExistYesRunError);

            if (_isNewCst && (isNoRun || isNoExistYesRunError))
            {
                MessageBox.Show(GG.boChinaLanguage ? "1. 最少一个Wafer，要进行检查\n2. 无Wafer时是无法进行的" : "1. 적어도 하나의 Wafer는 검사를 진행해야합니다\n2. Wafer없음에서 진행할 수 없습니다");
            }
            else if (_isNewCst == false && (isNoExistYesRunError))
            {
                MessageBox.Show(GG.boChinaLanguage ? "1. 最少一个Wafer，要进行检查" : "1. 적어도 하나의 Wafer는 검사를 진행해야합니다");
            }
            else
            {
                StringBuilder exists = new StringBuilder(), run = new StringBuilder();
                foreach (var w in _wafers)
                {
                    exists.AppendFormat("{0} ", w.Status == EmEfemMappingInfo.Presence);
                    run.AppendFormat("{0} ", w.IsComeBack == false);
                }
                Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "{0}.{1} USER MAP EXIST : {2}", _port.ToString(), _selectedCstID, exists.ToString());
                Logger.TransferDataLog.AppendLine(LogLevel.NoLog, "{0}.{1} USER MAP RUN__ : {2}", _port.ToString(), _selectedCstID, run.ToString());
                for (int i = 0; i < _wafers.Count; i++)
                {
                    if (_wafers[i].IsComeBack == false && _wafers[i].IsOut == false)
                    {
                        // Joo // 제일 마지막 Slot
                        LastSlotNo = _wafers[i].SlotNo;
                    }
                }
                PopupFlow = EmPopupFlow.OK;
                Close();
            }
        }

        private bool IsFormatOK(string readVal)
        {
            if (readVal == null || readVal == string.Empty)
                return false;

            switch (ReaderType)
            {
                case EmReaderTypes.OCR:
                    return true;
                case EmReaderTypes.RF:                    
                    return readVal.Length <= 30;
                default:
                    return true;
            }
        }

        private void FrmCstDataUserInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
            Hide();            
        }

        private bool NewDgvToWaferList(ref List<WaferInfo> waferInfo, ref DataGridView dgv, out bool isNoRun, out bool isNoExistYesRunError)
        {
            int slot;
            bool exist;
            EmWaferNotchDir notch;
            bool isComeback;

            bool atLeast1Run = false;
            bool noExistYesRunError = false;

            foreach (DataGridViewRow r in dgv.Rows)
            {
                slot = int.Parse(r.Cells["colSlot"].Value.ToString());
                exist = bool.Parse(r.Cells["colWaferExist"].Value.ToString());
                Enum.TryParse(r.Cells["colNotch"].Value.ToString(), out notch);
                isComeback = !bool.Parse(r.Cells["colNoComback"].Value.ToString());

                atLeast1Run |= exist == true && isComeback == false;
                noExistYesRunError |= exist == false && isComeback == false;

                waferInfo[slot - 1].Status = exist ? EmEfemMappingInfo.Presence : EmEfemMappingInfo.Absence;
                waferInfo[slot - 1].Notch = notch;
                waferInfo[slot - 1].IsComeBack = isComeback;
                waferInfo[slot - 1].IsOut = isComeback;
                if (exist == true && isComeback == false)
                {
                    waferInfo[slot - 1].IsAlignComplete = false;
                    waferInfo[slot - 1].IsInspComplete = false;
                }
            }

            isNoRun = atLeast1Run == false;
            isNoExistYesRunError = noExistYesRunError == true;

            if (atLeast1Run == false || noExistYesRunError == true)
                return false;

            return true;
        }
        private bool OldDgvToWaferList(ref List<WaferInfo> waferInfo, ref DataGridView dgv)
        {
            waferInfo.Clear();
            for (int i = 0; i < 13; ++i)
                waferInfo.Add(new WaferInfo());

            int slot;
            bool exist;            
            bool isInspComplete;

            foreach (DataGridViewRow r in dgv.Rows)
            {
                slot = int.Parse(r.Cells[0].Value.ToString());
                exist = bool.Parse(r.Cells[1].Value.ToString());                
                isInspComplete = bool.Parse(r.Cells[2].Value.ToString());
                waferInfo.RemoveAt(slot - 1);
                waferInfo.Insert(slot - 1, new WaferInfo("", slot, exist ? EmEfemMappingInfo.Presence : EmEfemMappingInfo.Absence));                
                waferInfo[slot - 1].IsInspComplete = isInspComplete;
            }

            return true;
        }

        private void btnChangeCstID_Click(object sender, EventArgs e)
        {
            string id;
            if (rdUseReadData1.Checked)
                id = txtRead1.Text;
            else if (rdUseReadData2.Checked)
                id = txtRead2.Text;
            else if (rdUseInput.Checked)
                id = txtInput1.Text;
            else
                return;

            if (IsFormatOK(id) == false)
            {
                lblNotify.Text = GG.boChinaLanguage ? "Cassette ID Format 问题 发生" : "Cassette ID Format 이상";
                return;
            }

            ChangeCstID(id);
            ShowOldWaferInfo(!_isNewCst);
        }

        private void dgvWaferData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex != -1)
            {
                bool old = bool.Parse(dgvWaferData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());       
                if (dgvWaferData.Rows[e.RowIndex].ReadOnly)        
                    return; 
                dgvWaferData.Rows[e.RowIndex].Cells[3].Value = !old;
                dgvWaferData.EndEdit();                
            }
        }

        private void btnDeleteCreate_Click(object sender, EventArgs e)
        {
            TransferDataMgr.DeleteCst(_selectedCstID);
            for (int slot = 1; slot <= 13; ++slot)
                TransferDataMgr.DeleteWafer(_selectedCstID, slot);
            ChangeCstID(_selectedCstID);
            ShowOldWaferInfo(!_isNewCst);
            Logger.TransferDataLog.AppendLine(LogLevel.Info, "유저동작, CST ID:{0} 의 CST정보와 Wafer정보 삭제", _selectedCstID);
        }

        private bool waferSelectFlag = false, waferRunFlag = false;

        private void btnCopyIncompleteWafer_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            List<WaferInfo> oldwafers = new List<WaferInfo>();            
            OldDgvToWaferList(ref oldwafers, ref dgvOldWaferInfo);
            int slot;

            if (oldwafers.Count == 0)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "现有的Data有问题. 请手动选择" : "기존 데이터에 이상이 있습니다. 수동으로 선택해주세요");
                return;
            }

            if (btn == btnCopyAllWafer)
            {
                if (dgvWaferData.Rows.Count > 0 && dgvWaferData.Rows.Count == dgvOldWaferInfo.Rows.Count)
                {                    
                    for (int i = 0; i < dgvWaferData.Rows.Count; ++i)
                    {
                        slot = int.Parse(dgvWaferData.Rows[i].Cells["colSlot"].Value.ToString());
                        dgvWaferData.Rows[i].Cells["colWaferExist"].Value = oldwafers[slot - 1].Status == EmEfemMappingInfo.Presence;
                        dgvWaferData.Rows[i].Cells["colNoComback"].Value = oldwafers[slot - 1].Status == EmEfemMappingInfo.Presence;
                    }
                }
            }
            else if (btn == btnCopyIncompleteWafer)
            {
                if (dgvWaferData.Rows.Count > 0 && dgvWaferData.Rows.Count == dgvOldWaferInfo.Rows.Count)
                {
                    for (int i = 0; i < dgvWaferData.Rows.Count; ++i)
                    {
                        slot = int.Parse(dgvWaferData.Rows[i].Cells["colSlot"].Value.ToString());
                        dgvWaferData.Rows[i].Cells["colWaferExist"].Value = oldwafers[slot - 1].Status == EmEfemMappingInfo.Presence;
                        dgvWaferData.Rows[i].Cells["colNoComback"].Value = oldwafers[slot - 1].Status == EmEfemMappingInfo.Presence && oldwafers[slot - 1].IsInspComplete == false;
                    }
                }
            }
        }

        private void btnWaferSelectAll_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btnWaferSelectAll)
            {
                if (dgvWaferData.Rows.Count > 0)
                {
                    waferSelectFlag = !waferSelectFlag;
                    for (int i = 0; i < dgvWaferData.Rows.Count; ++i)
                    {
                        if (dgvWaferData.Rows[i].ReadOnly == false)
                            dgvWaferData.Rows[i].Cells[1].Value = waferSelectFlag;
                    }
                }
            }
            else if (btn == btnRunSelectAll)
            {
                if (dgvWaferData.Rows.Count > 0)
                {
                    bool isExist = false;
                    waferRunFlag = !waferRunFlag;
                    for (int i = 0; i < dgvWaferData.Rows.Count; ++i)
                    {
                        isExist = bool.Parse(dgvWaferData.Rows[i].Cells[1].Value.ToString());
                        if (isExist && dgvWaferData.Rows[i].ReadOnly == false)
                            dgvWaferData.Rows[i].Cells[3].Value = waferRunFlag;
                    }
                }
            }
        }
    }
}
