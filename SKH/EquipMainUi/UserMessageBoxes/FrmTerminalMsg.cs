using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMainUi.UserMessageBoxes
{
    public partial class FrmTerminalMsg : Form
    {
        public FrmTerminalMsg()
        {
            InitializeComponent();

            GG.Equip.AddTerminalMsg += (isPopup, time, data) =>
            {
                if (isPopup)
                {                    
                    this.Show();                    
                }
                else if (this.Visible == true)
                {
                    this.Close();
                }

                var item = listView1.Items.Insert(0, time);
                item.SubItems.Add(data);
                listView1.Items[0].Selected = true;

                for (int i = 0; i < 5; ++i)
                {
                    if (listView1.Items.Count > 20)
                        listView1.Items.RemoveAt(20);
                    else
                        break;
                }

                GG.Equip.HasNewTerminalMsg = true;
            };
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                lblMsg.Text = listView1.SelectedItems[0].SubItems.Count > 1 ? listView1.SelectedItems[0].SubItems[1].Text : "";
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
    }
}
