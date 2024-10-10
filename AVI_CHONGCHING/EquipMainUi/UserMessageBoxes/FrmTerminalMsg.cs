using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EquipMainUi.Struct.Detail;

namespace EquipMainUi.UserMessageBoxes
{
    public partial class FrmTerminalMsg : Form
    {
        public FrmTerminalMsg()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAutoDataLog.SelectedItems.Count > 0)
            {
                lblMsg.Text = lstAutoDataLog.SelectedItems[0].SubItems.Count > 1 ? lstAutoDataLog.SelectedItems[0].SubItems[1].Text : "";
            }
        }

        private void FrmTerminalMsg_FormClosing(object sender, FormClosingEventArgs e)
        {            
            e.Cancel = true;
            base.OnClosing(e);
            Hide();
        }

        private void FrmTerminalMsg_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                GG.Equip.HasNewTerminalMsg = false;
            }
        }

        internal void Add(TerminalMsgItem item)
        {
            lstAutoDataLog.Items.Insert(0, item.TimeToString()).SubItems.Add(item.Message);
            if (lstAutoDataLog.Items.Count > 30)
                lstAutoDataLog.Items.RemoveAt(lstAutoDataLog.Items.Count - 1);
        }

        internal void SelectIndex(int index)
        {
            if (index >= 0 && index < lstAutoDataLog.Items.Count)
            {                
                lstAutoDataLog.Items[index].Selected = true;
                lstAutoDataLog.Items[index].EnsureVisible();
                lstAutoDataLog.Select();
            }
        }
    }
}
