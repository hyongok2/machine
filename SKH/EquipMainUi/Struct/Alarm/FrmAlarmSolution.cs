using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EquipMainUi.Struct
{
    public partial class FrmAlarmSolution : Form
    {
        AlarmSolutionMgr solutionMgr;
        public FrmAlarmSolution(AlarmSolutionMgr mgr)
        {
            InitializeComponent();

            solutionMgr = mgr;
            LoadAlarmList();
        }

        private void LoadAlarmList()
        {
            foreach (var alarm in solutionMgr.GetAlarmList)
            {
                ListViewItem lvi = new ListViewItem(alarm.Index.ToString());
                lvi.SubItems.Add(alarm.Name);

                if (alarm.Name.Length > 8)
                    listAlarm.Items.Add(lvi);
            }
        }
        public void SetFocus(int idx)
        {
            int realIdx;
            for (int i = 0; i < listAlarm.Items.Count; i++)
            {
                realIdx = int.Parse(listAlarm.Items[i].SubItems[0].Text);
                if (realIdx == idx)
                {
                    idx = i;
                    break;
                }
            }

            listAlarm.Items[idx].EnsureVisible();
            listAlarm.Items[idx].Focused = true;
            listAlarm.Items[idx].Selected = true;
        }
        private void listAlarm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDescription(GetSelectedListIndex());
        }

        private void UpdateDescription(int idx)
        {
            tbCause.Text = solutionMgr.GetAlarmList[idx].Cause;
            tbAction.Text = solutionMgr.GetAlarmList[idx].Action;
            tbMemo.Text = solutionMgr.GetAlarmList[idx].Memo;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            solutionMgr.Modify(GetSelectedListIndex(), tbCause.Text, tbAction.Text, tbMemo.Text);
        }

        private int GetSelectedListIndex()
        {
            //int idx = listAlarm.SelectedItems[0].Index;
            int idx = listAlarm.FocusedItem.Index;
            idx = int.Parse(listAlarm.Items[idx].SubItems[0].Text);

            return idx;
        }

        private void tbMemo_Click(object sender, EventArgs e)
        {
            if ((ModifierKeys & Keys.Control) != Keys.Control) return;

            tbCause.Enabled = tbAction.Enabled = true;
        }
    }
}
