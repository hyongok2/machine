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
        string[] strChinaLanguage = { "완료", "미완료", "미진행", "슬롯", "얼라인", "검사", "리뷰", "시작시간", "완료시간", "웨이퍼 개수", "로드 시간", "언로드 시간" };

        public FrmWaferHistory()
        {
            InitializeComponent();

            tbBackupCount.Text = GG.Equip.CtrlSetting.Mongo.MaxCstCount.ToString();
            tbCycle.Text = GG.Equip.CtrlSetting.Mongo.DeleteDays.ToString();

            timer1.Start();
            ChangeChinaLanguage();
        }

        private void ChangeChinaLanguage()
        {
            if (GG.boChinaLanguage)
            {
                label2.Text = "搜索期间";       // 검색 기간
                label5.Text = "最长储存日数";       // 저장 최대 일수
                label6.Text = "最多储存 Cassette 数量";       // 저장 최대 카세트 개수

                label3.Text = "CST ID";       // 카세트 아이디
                label4.Text = "Wafer ID";       // 웨이퍼 아이디
                groupBox1.Text = "自动删除";	// 자동 삭제

                strChinaLanguage[0] = "完成";        // 완료
                strChinaLanguage[1] = "不完整";        // 미완료
                strChinaLanguage[2] = "无进展";        // 미진행
                strChinaLanguage[3] = "Slot";        // 슬롯
                strChinaLanguage[4] = "Align";        // 얼라인
                strChinaLanguage[5] = "测试";        // 검사
                strChinaLanguage[6] = "Review";        // 리뷰
                strChinaLanguage[7] = "开始时间";        // 시작시간
                strChinaLanguage[8] = "结束时间";        // 완료시간
                strChinaLanguage[9] = "Wafer 数量";        // 웨이퍼 개수
                strChinaLanguage[10] = "Load 时间";       // 로드 시간
                strChinaLanguage[11] = "Unload 时间";       // 언로드 시간
            }
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
                lvHistory.Columns.Add(GG.boChinaLanguage ? strChinaLanguage[3] : "슬롯");
                lvHistory.Columns.Add("Notch");
                lvHistory.Columns.Add("OCR ID");
                lvHistory.Columns.Add("BCR1 ID");
                lvHistory.Columns.Add("BCR2 ID");
                lvHistory.Columns.Add(GG.boChinaLanguage ? strChinaLanguage[4] : "얼라인");
                lvHistory.Columns.Add(GG.boChinaLanguage ? strChinaLanguage[5] : "검사");
                lvHistory.Columns.Add(GG.boChinaLanguage ? strChinaLanguage[6] : "리뷰");
                lvHistory.Columns.Add(GG.boChinaLanguage ? strChinaLanguage[7] : "시작 시간");
                lvHistory.Columns.Add(GG.boChinaLanguage ? strChinaLanguage[8] : "완료 시간");

                for (int i = (curPage - 1) * 50; i < curPage * 50; i++)
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
                    if (GG.boChinaLanguage)
                    {
                        lvi.SubItems.Add(waferdata[i].IsAlignComplete == true ? strChinaLanguage[0] : strChinaLanguage[2]);
                        lvi.SubItems.Add(waferdata[i].IsInspComplete == true ? strChinaLanguage[0] : strChinaLanguage[2]);
                        lvi.SubItems.Add(waferdata[i].IsReviewComplete == true ? strChinaLanguage[0] : strChinaLanguage[2]);
                    }
                    else
                    {
                        lvi.SubItems.Add(waferdata[i].IsAlignComplete == true ? GG.boChinaLanguage ? "完成" : "완료" : GG.boChinaLanguage ? "未进步" : "미진행");
                        lvi.SubItems.Add(waferdata[i].IsInspComplete == true ? GG.boChinaLanguage ? "完成" : "완료" : GG.boChinaLanguage ? "未进步" : "미진행");
                        lvi.SubItems.Add(waferdata[i].IsReviewComplete == true ? GG.boChinaLanguage ? "完成" : "완료" : GG.boChinaLanguage ? "未进步" : "미진행");
                    }
                    lvi.SubItems.Add(waferdata[i].OutputDate.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"));
                    if (waferdata[i].InputDate.Year == 1)
                    {
                        lvi.SubItems.Add(GG.boChinaLanguage ? strChinaLanguage[1] : "미완료");
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
                lvHistory.Columns.Add(GG.boChinaLanguage ? strChinaLanguage[9] : "웨이퍼 개수");
                lvHistory.Columns.Add(GG.boChinaLanguage ? strChinaLanguage[10] : "로드 시간");
                lvHistory.Columns.Add(GG.boChinaLanguage ? strChinaLanguage[11] : "언로드 시간");

                for (int i = (curPage - 1) * 50; i < curPage * 50; i++)
                {
                    if (cstdata.Count <= i)
                        break;
                    ListViewItem lvi = new ListViewItem(cstdata[i].CstID);
                    lvi.SubItems.Add(cstdata[i].SlotCount.ToString());
                    lvi.SubItems.Add(cstdata[i].InputDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (cstdata[i].OutputDate.Year == 1)
                    {
                        lvi.SubItems.Add(GG.boChinaLanguage ? strChinaLanguage[1] : "미완료");
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

            Struct.CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "储存完毕" : "저장 완료");
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
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "无法找到Wafer 细部Data" : "Wafer 세부 데이터를 찾을 수 없습니다");
            }
        }

        int curPage = 1;
        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (clickedBtn == btnWafer)
            {
                if (waferdata == null)
                    return;
                if (curPage * 50 > waferdata.Count)
                    return;
                curPage++;
                Thread _worker = new Thread(new ThreadStart(UpdateWaferList));
                _worker.Start();
            }
            else if (clickedBtn == btnCst)
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
                if (waferdata == null)
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
