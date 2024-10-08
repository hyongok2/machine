using EquipMainUi.Struct;
using EquipMainUi.Struct.TransferData;
using EquipMainUi.UserMessageBoxes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMainUi.Setting
{
    public partial class FrmWaferHistory : Form
    {
        public FrmWaferHistory()
        {
            InitializeComponent();

            tbBackupCount.Text = GG.Equip.CtrlSetting.Mongo.MaxCstCount.ToString();
            tbCycle.Text = GG.Equip.CtrlSetting.Mongo.DeleteDays.ToString();
            
            timer1.Start();
        }
        private void tmrUiUpdate(object sender, EventArgs e)
        {
            try
            {
                if (clickedBtn == btnWafer)
                {
                    btnWafer.BackColor = Color.DodgerBlue;
                    btnCst.BackColor = Color.Gainsboro;
                }
                else if (clickedBtn == btnCst)
                {
                    btnWafer.BackColor = Color.Gainsboro;
                    btnCst.BackColor = Color.DodgerBlue;
                }
                else
                {
                    btnWafer.BackColor = Color.Gainsboro;
                    btnCst.BackColor = Color.Gainsboro;
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

        Button clickedBtn;
        private void btnCst_Click(object sender, EventArgs e)
        {
            curPage = 1;

            clickedBtn = btnCst;

            UpdateList();

            Thread _worker = new Thread(new ThreadStart(UpdateCassetteList));
            _worker.Start();
        }
        private void btnWafer_Click(object sender, EventArgs e)
        {
            curPage = 1;
            clickedBtn = btnWafer;

            UpdateList();

            Thread _worker = new Thread(new ThreadStart(UpdateWaferList));
            _worker.Start();
        }

        private void UpdateList()
        {
            cstdata = TransferDataMgr.GetCassettes(dtpStart.Value, dtpEnd.Value, tbCstID.Text);
            waferdata = TransferDataMgr.GetWafers(dtpStart.Value, dtpEnd.Value, tbCstID.Text, tbWaferID.Text);
        }

        List<WaferInfo> waferdata;
        private void UpdateWaferList()
        {
            lblPage.Text = string.Format("{0} / {1}", curPage, waferdata.Count / 50 + 1);

            lvHistory.BeginUpdate();

            try
            {
                lvHistory.Clear();

                lvHistory.Columns.Add("CST ID");
                lvHistory.Columns.Add("Wafer ID");
                lvHistory.Columns.Add("슬롯");
                lvHistory.Columns.Add("Notch");
                lvHistory.Columns.Add("OCR ID");
                lvHistory.Columns.Add("BCR1 ID");
                lvHistory.Columns.Add("BCR2 ID");
                lvHistory.Columns.Add("얼라인");
                lvHistory.Columns.Add("검사");
                lvHistory.Columns.Add("리뷰");
                lvHistory.Columns.Add("시작 시간");
                lvHistory.Columns.Add("완료 시간");

                for (int i = (curPage-1)*50; i < curPage*50; i++)
                {
                    if (waferdata.Count <= i)
                        break;
                    ListViewItem lvi = new ListViewItem(waferdata[i].CstID);
                    lvi.SubItems.Add(waferdata[i].WaferID);
                    lvi.SubItems.Add(waferdata[i].SlotNo.ToString());
                    lvi.SubItems.Add(waferdata[i].Notch.ToString());
                    lvi.SubItems.Add(waferdata[i].OCRID);
                    lvi.SubItems.Add(waferdata[i].BCRID1);
                    lvi.SubItems.Add(waferdata[i].BCRID2);
                    lvi.SubItems.Add(waferdata[i].IsAlignComplete == true ? "완료" : "미진행");
                    lvi.SubItems.Add(waferdata[i].IsInspComplete == true ? "완료" : "미진행");
                    lvi.SubItems.Add(waferdata[i].IsReviewComplete == true ? "완료" : "미진행");
                    lvi.SubItems.Add(waferdata[i].OutputDate.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"));
                    if(waferdata[i].InputDate.Year == 1)
                    {
                        lvi.SubItems.Add("미완료");
                    }
                    else
                    {
                        lvi.SubItems.Add(waferdata[i].InputDate.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    

                    lvHistory.Items.Add(lvi);

                    Thread.Sleep(0);
                }

                lvHistory.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lvHistory.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception)
            {
                
            }
            finally
            {
                lvHistory.EndUpdate();
            }
        }

        List<CassetteInfo> cstdata;
        private void UpdateCassetteList()
        {
            lblPage.Text = string.Format("{0} / {1}", curPage, cstdata.Count / 50 + 1);

            try
            {
                lvHistory.BeginUpdate();

                lvHistory.Clear();

                lvHistory.Columns.Add("CST ID");
                lvHistory.Columns.Add("웨이퍼 개수");
                lvHistory.Columns.Add("로드 시간");
                lvHistory.Columns.Add("언로드 시간");

                for (int i = (curPage - 1) * 50; i < curPage * 50; i++)
                {
                    if (cstdata.Count <= i)
                        break;
                    ListViewItem lvi = new ListViewItem(cstdata[i].CstID);
                    lvi.SubItems.Add(cstdata[i].SlotCount.ToString());
                    lvi.SubItems.Add(cstdata[i].InputDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    if(cstdata[i].OutputDate.Year == 1)
                    {
                        lvi.SubItems.Add("미완료");
                    }
                    else
                    {
                        lvi.SubItems.Add(cstdata[i].OutputDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    }

                    lvHistory.Items.Add(lvi);
                }

                lvHistory.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lvHistory.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception)
            {

            }
            finally
            {
                lvHistory.EndUpdate();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GG.Equip.CtrlSetting.Mongo.MaxCstCount = int.Parse(tbBackupCount.Text);
            GG.Equip.CtrlSetting.Mongo.DeleteDays = int.Parse(tbCycle.Text);

            GG.Equip.CtrlSetting.Save();

            Struct.CheckMgr.AddCheckMsg(true, "저장 완료");
        }

        private void lvHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvHistory.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = lvHistory.SelectedItems;
                ListViewItem item = items[0];
                string cstId = item.SubItems[0].Text;
                int slotNo;
                if (int.TryParse(item.SubItems[2].Text, out slotNo))
                {
                    WaferInfo w = TransferDataMgr.GetWafer(cstId, slotNo);
                    if (w != null)
                    {
                        FrmPGrid f = new FrmPGrid(string.Format("{0}-{1}-{2}", cstId, slotNo, w.WaferID), (object)w);
                        f.Show();
                        return;
                    }
                }
                InterLockMgr.AddInterLock("Wafer 세부 데이터를 찾을 수 없습니다");
            }
        }

        int curPage = 1;
        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if(clickedBtn == btnWafer)
            {
                if (waferdata == null)
                    return;
                if (curPage * 50 > waferdata.Count)
                    return;
                curPage++;
                Thread _worker = new Thread(new ThreadStart(UpdateWaferList));
                _worker.Start();
            }
            else if(clickedBtn == btnCst)
            {
                if (cstdata == null)
                    return;
                if (curPage * 50 > cstdata.Count)
                    return;
                curPage++;
                Thread _worker = new Thread(new ThreadStart(UpdateCassetteList));
                _worker.Start();
            }
        }

        private void btnPrePage_Click(object sender, EventArgs e)
        {
            if (clickedBtn == btnWafer)
            {
                if(waferdata == null)
                return;
                if (curPage == 1)
                    return;
                curPage--;
                Thread _worker = new Thread(new ThreadStart(UpdateWaferList));
                _worker.Start();
            }
            else if (clickedBtn == btnCst)
            {
                if (cstdata == null)
                    return;
                if (curPage == 1)
                    return;
                curPage--;
                Thread _worker = new Thread(new ThreadStart(UpdateCassetteList));
                _worker.Start();
            }
        }

        private void dtpStart_CloseUp(object sender, EventArgs e)
        {
            lblPage.Text = "0 / 0";
            clickedBtn = null;
            lvHistory.Items.Clear();
        }
    }
}
