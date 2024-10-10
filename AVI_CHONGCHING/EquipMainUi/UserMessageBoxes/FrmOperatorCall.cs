using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EquipMainUi
{
    public partial class FrmOperatorCall : Form
    {
        public FrmOperatorCall()
        {
            InitializeComponent();
        }

        public void AddOperatorCall(int tID, string[] msgs, string moduleID)
        {
            lblOpcallMsg.Text = msgs[msgs.Length - 1]; 

            for (int iPos = 0; iPos < msgs.Length; iPos++)
            { 
                lstOperMsg.Items.Insert(0, new ListViewItem(new string[] {  
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), tID.ToString(), msgs [iPos]  }));

                if (lstOperMsg.Items.Count > 100)
                    lstOperMsg.Items.RemoveAt(100);
            } 
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;

            this.Hide();
            base.OnClosing(e);
        }
    }
}
