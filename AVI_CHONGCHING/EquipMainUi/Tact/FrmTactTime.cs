using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EquipMainUi.Struct.Step;
using EquipMainUi.Struct;
using EquipMainUi.Tact;

namespace EquipMainUi.Monitor
{

    public partial class FrmTactTime : Form
    {
        private const int TACK_COUNT = 10;

        public Struct.Equipment Equip { get; set; }

        public List<TactTimeSpan> LstLoadingTactTimeSpan = new List<TactTimeSpan>();
        public List<TactTimeSpan> LstScanningTactTimeSpan = new List<TactTimeSpan>();
        public List<TactTimeSpan> LstUnloadingTactTimeSpan = new List<TactTimeSpan>();

        public List<TactTimeSpan> LstMainTactTimeSpan;
        public List<EFEMTactTimeSpan> LstEFEMTactTimeSpan;
        public FrmTactTime(Struct.Equipment equip)
        {
            Equip = equip;
            InitializeComponent();
            LstMainTactTimeSpan = TactTimeMgr.Instance.LstMainTactTimeSpan;
            LstEFEMTactTimeSpan = EFEMTactMgr.Instance.LstMainTactTimeSpan;
            btnEFEMTotal_Click(null, null);
            btnInspTotal.Text = GG.boChinaLanguage ? "检查 TactTime" : "검사 TactTime";
        }
        private void btnlInspTotal_Click(object sender, EventArgs e)
        {
            lblTactTitle.Text = GG.boChinaLanguage ? "■ 检查 Tact" : "■ 검사 TACT";
            lstTact.SuspendLayout();

            lstTact.Items.Clear();
            for (int iPos = 0; iPos < LstMainTactTimeSpan.Count; iPos++)
                lstTact.Items.Add((new ListViewItem(new string[] { LstMainTactTimeSpan[iPos].Name, "", "", "", "", "", "", "", "", "", "" })));

            lstTact.ResumeLayout();

            lstGlassInfo.SuspendLayout();

            lstGlassInfo.Items.Clear();
            lstGlassInfo.Items.Add((new ListViewItem(new string[] { GG.boChinaLanguage ? "Wafer ID" : "웨이퍼 ID", "", "", "", "", "", "", "", "", "", "" })));
            lstGlassInfo.Items.Add((new ListViewItem(new string[] { GG.boChinaLanguage ? "物流 Tact" : "물류 Tact", "", "", "", "", "", "", "", "", "", "" })));
            lstGlassInfo.Items.Add((new ListViewItem(new string[] { GG.boChinaLanguage ? "检查 Tact" : "검사 Tact", "", "", "", "", "", "", "", "", "", "" })));
            lstGlassInfo.Items.Add((new ListViewItem(new string[] { GG.boChinaLanguage ? "Review Tact" : "리뷰 Tact", "", "", "", "", "", "", "", "", "", "" })));
            _showTact = 0;
        }
        private void btnEFEMTotal_Click(object sender, EventArgs e)
        {
            lblTactTitle.Text = "■ EFEM TACT";
            lstTact.SuspendLayout();

            lstTact.Items.Clear();
            for (int iPos = 0; iPos < LstEFEMTactTimeSpan.Count; iPos++)
                lstTact.Items.Add((new ListViewItem(new string[] { LstEFEMTactTimeSpan[iPos].Name, "", "", "", "", "", "", "", "", "", "" })));

            lstTact.ResumeLayout();

            lstGlassInfo.SuspendLayout();

            lstGlassInfo.Items.Clear();
            lstGlassInfo.Items.Add((new ListViewItem(new string[] { "Casette ID", "", "", "", "", "", "", "", "", "", "" })));
            lstGlassInfo.Items.Add((new ListViewItem(new string[] { "Slot No", "", "", "", "", "", "", "", "", "", "" })));
            lstGlassInfo.Items.Add((new ListViewItem(new string[] { GG.boChinaLanguage ? "Total(Open,Close包括/检查机不包含)" : "Total(Open,Close포함/검사기미포함)", "", "", "", "", "", "", "", "", "", "" })));
            _showTact = 1;
        }
        private void btnlLoading_Click(object sender, EventArgs e)
        {
            lblTactTitle.Text = "■ LOADING TACT";
            lstTact.SuspendLayout();
            lstTact.Items.Clear();
            /*
            foreach (EM_TT_LST alid in _lstLoadingTack)
            {
                lstTact.Items.Add((new ListViewItem(new string[] { alid.ToString(), "", "", "", "", "", "", "", "", "", "" })));
            }*/

            lstTact.ResumeLayout();
            _showTact = 1;
        }
        private void btnlScan_Click(object sender, EventArgs e)
        {
            lblTactTitle.Text = "■ SCAN TACT";
            lstTact.SuspendLayout();
            lstTact.Items.Clear();
            /*
            foreach (EM_TT_LST alid in _lstScanTack)
            {
                lstTact.Items.Add((new ListViewItem(new string[] { alid.ToString(), "", "", "", "", "", "", "", "", "", "" })));
            }*/
            lstTact.ResumeLayout();
            _showTact = 2;
        }
        private void btnlUnLoading_Click(object sender, EventArgs e)
        {
            lblTactTitle.Text = "■ UNLOADING TACT";

            lstTact.SuspendLayout();
            lstTact.Items.Clear();
            /*
            foreach (EM_TT_LST alid in _lstUnloadingTack)
            {
                lstTact.Items.Add((new ListViewItem(new string[] { alid.ToString(), "", "", "", "", "", "", "", "", "", "" })));
            }*/
            lstTact.ResumeLayout();

            _showTact = 3;
        }

        private int _showTact = -1;

        private void tmrUiUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_showTact == 0)
                {
                    btnInspTotal.BackColor = Color.DodgerBlue;
                    btnEFEMTact.BackColor = Color.Transparent;
                    ShowTotalTact();
                }
                else if (_showTact == 1)
                {
                    btnInspTotal.BackColor = Color.Transparent;
                    btnEFEMTact.BackColor = Color.DodgerBlue;
                    ShowEFEMTact();
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

        //메소드 - SHOW TACT        
        public object _currentItem = null;

        private void ShowTotalTact()
        {
            lstTact.SuspendLayout();
            SetTackList(LstMainTactTimeSpan);
            lstTact.ResumeLayout();
            SetTacttimeGlassInfo();
        }
        private void ShowEFEMTact()
        {
            lstTact.SuspendLayout();
            SetEFEMTackList(LstEFEMTactTimeSpan);
            lstTact.ResumeLayout();
            SetTacttimeEFEMInfo();
        }

        private void SetTacttimeEFEMInfo()
        {
            lstGlassInfo.SuspendLayout();
            for (int iPos = 0; iPos < EFEMTactMgr.Instance.ListWaferID.Count; iPos++)
            {
                //Cst ID
                var CstID = lstGlassInfo.Items[0].SubItems[iPos + 1].Text = EFEMTactMgr.Instance.ListWaferID[iPos].CstID;

                if(CstID == string.Empty || CstID == "")
                {
                    lstGlassInfo.Items[1].SubItems[iPos + 1].Text = "00:00.000";
                    lstGlassInfo.Items[2].SubItems[iPos + 1].Text = "00:00.000";
                }
                else
                {
                    //Slot No
                    var SlotNo = EFEMTactMgr.Instance.ListWaferID[iPos].SlotNo;
                    if (SlotNo == 0)
                        lstGlassInfo.Items[1].SubItems[iPos + 1].Text = string.Empty;
                    else
                        lstGlassInfo.Items[1].SubItems[iPos + 1].Text = EFEMTactMgr.Instance.ListWaferID[iPos].SlotNo.ToString();

                    var key = string.Format("{0}_{1}", CstID, SlotNo.ToString());
                    var tsOpenTime = EFEMTactMgr.Instance.GetTackInterval(EFEM_TACT_VALUE.T000_LPM_OPEN_START, EFEM_TACT_VALUE.T000_LPM_OPEN_END, key);
                    var tsCloseTime = EFEMTactMgr.Instance.GetTackInterval(EFEM_TACT_VALUE.T130_LPM_CLOSE_START, EFEM_TACT_VALUE.T130_LPM_CLOSE_END, key);
                    //var tsTotalBeforeAVI = EFEMTactMgr.Instance.GetTackInterval(EFEM_TACT_VALUE.T010_ROBOT_PICK_FROM_LPM_START, EFEM_TACT_VALUE.T120_ROBOT_PLACE_TO_LPM_END, key);
                    var tsTotalBeforeAVI = EFEMTactMgr.Instance.GetTackInterval(EFEM_TACT_VALUE.T010_ROBOT_PICK_FROM_LPM_START, EFEM_TACT_VALUE.T040_ROBOT_PICK_FROM_ALIGNER_END, key);
                    var tsTotalAfterAVI = EFEMTactMgr.Instance.GetTackInterval(EFEM_TACT_VALUE.T120_ROBOT_PLACE_TO_LPM_START, EFEM_TACT_VALUE.T120_ROBOT_PLACE_TO_LPM_END, key);

                    DateTime dtOpen;
                    DateTime dtclose;
                    DateTime dtTotalBeforeAVI;
                    DateTime dtTotalAfterAVI;
                    bool isNoError = true;
                    isNoError = DateTime.TryParse(tsOpenTime.ToString(), out dtOpen);
                    isNoError &= DateTime.TryParse(tsCloseTime.ToString(), out dtclose);
                    isNoError &= DateTime.TryParse(tsTotalBeforeAVI.ToString(), out dtTotalBeforeAVI);
                    isNoError &= DateTime.TryParse(tsTotalAfterAVI.ToString(), out dtTotalAfterAVI);

                    if (isNoError == true)
                    {
                        var dtTotal = dtTotalBeforeAVI.AddTicks(dtTotalAfterAVI.TimeOfDay.Ticks);
                        var sensoredTotal = dtOpen.AddTicks(dtTotal.TimeOfDay.Ticks).AddTicks(dtclose.TimeOfDay.Ticks);

                        //sensoredTotal = sensoredTotal.AddTicks(-dtAVI.TimeOfDay.Ticks);

                        lstGlassInfo.Items[2].SubItems[iPos + 1].Text = sensoredTotal.ToString("mm:ss.fff");
                    }
                    else
                    {
                        lstGlassInfo.Items[2].SubItems[iPos + 1].Text = "파싱 오류";
                    }
                    
                }

                
            }
            lstGlassInfo.ResumeLayout();
        }

        private void SetEFEMTackList(List<EFEMTactTimeSpan> lst)
        {
            for (int iPos = 0; iPos < lst.Count; iPos++)
            {
                for (int jPos = 0; jPos < TACK_COUNT; jPos++)
                {
                    string tackStr = EFEMTactMgr.Instance.GetTackStr(lst[iPos].Start, lst[iPos].End, TACK_COUNT - jPos - 1);
                    if (tackStr != lstTact.Items[iPos].SubItems[jPos + 1].Text)
                        lstTact.Items[iPos].SubItems[jPos + 1].Text = tackStr;
                }
            }
        }

        private void ShowLoadingTact()
        {
            lstTact.SuspendLayout();
            SetTackList(LstLoadingTactTimeSpan);
            lstTact.ResumeLayout();
        }
        private void ShowScanTact()
        {
            SetTackList(LstScanningTactTimeSpan);
        }
        private void ShowUnloadingTact()
        {
            SetTackList(LstUnloadingTactTimeSpan);
        }
        private void btnlTopMost_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
        }
        private void FrmTactTime_Load(object sender, EventArgs e)
        {
        }
        private void SetTackList(List<TactTimeSpan> lst)
        {
            for (int iPos = 0; iPos < lst.Count; iPos++)
            {
                for (int jPos = 0; jPos < TACK_COUNT; jPos++)
                {
                    string tackStr = TactTimeMgr.Instance.GetTackStr(lst[iPos].Start, lst[iPos].End, jPos);
                    if (tackStr != lstTact.Items[iPos].SubItems[jPos + 1].Text)
                        lstTact.Items[iPos].SubItems[jPos + 1].Text = tackStr;
                }
            }
        }
        private void SetTacttimeGlassInfo()
        {
            lstGlassInfo.SuspendLayout();
            for (int iPos = 0; iPos < TactTimeMgr.Instance.ListWaferID.Count; iPos++)
            {
                //Wafer ID
                lstGlassInfo.Items[0].SubItems[iPos + 1].Text = TactTimeMgr.Instance.ListWaferID[iPos];

                var tsTotal = TactTimeMgr.Instance.GetTackInterval(EM_TT_LST.T010_PIO_RECEIVE_ACTUAL_START, EM_TT_LST.T170_PIO_SEND_ACTUAL_END, iPos);
                var tsInsp = TactTimeMgr.Instance.GetTackInterval(EM_TT_LST.T070_SCAN_RUN_START, EM_TT_LST.T070_SCAN_RUN_END, iPos);
                var tsReview = TactTimeMgr.Instance.GetTackInterval(EM_TT_LST.T080_REVIEW_WAIT_POS_START, EM_TT_LST.T100_MANUAL_REVIEW_END, iPos);

                DateTime dtTotal;
                DateTime dtInsp;
                DateTime dtReview;
                bool isNoError = true;
                isNoError = DateTime.TryParse(tsTotal.ToString(), out dtTotal);
                isNoError &= DateTime.TryParse(tsInsp.ToString(), out dtInsp);
                isNoError &= DateTime.TryParse(tsReview.ToString(), out dtReview);


                if (isNoError)
                {
                    //물류 택  토탈 - 검사택 - 리뷰택
                    var exceptInspRv = dtTotal.AddTicks(-dtInsp.TimeOfDay.Ticks).AddTicks(-dtReview.TimeOfDay.Ticks);
                    lstGlassInfo.Items[1].SubItems[iPos + 1].Text = exceptInspRv.ToString("mm:ss.fff");

                    //검사 택
                    lstGlassInfo.Items[2].SubItems[iPos + 1].Text = dtInsp.ToString("mm:ss.fff");

                    //리뷰 택
                    lstGlassInfo.Items[3].SubItems[iPos + 1].Text = dtReview.ToString("mm:ss.fff");
                }
                else
                {
                    lstGlassInfo.Items[1].SubItems[iPos + 1].Text = "ERROR";
                    lstGlassInfo.Items[2].SubItems[iPos + 1].Text = "ERROR";
                    lstGlassInfo.Items[3].SubItems[iPos + 1].Text = "ERROR";
                }
            }
            lstGlassInfo.ResumeLayout();
        }
        private void lstTact_KeyDown(object sender, KeyEventArgs e)
        {
            string data = string.Empty;
            if (e.Control && e.KeyValue == 'C')
            {
                foreach (ListViewItem item in lstTact.Items)
                {
                    foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                    {
                        data += subItem.Text;
                        data += "\t";                    
                    }
                    data += "\r\n";    
                }
                Clipboard.SetText(data);
            }            
        }
    }
}
