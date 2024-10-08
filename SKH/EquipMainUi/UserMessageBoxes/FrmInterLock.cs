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
    public partial class FrmInterLock : Form
    {
        public string InterlockMsg
        {
            get
            {
                return txtInterlockMsg.Text;
            }
            set
            {
                txtInterlockMsg.Text = value;
            }
        }
        public string DetailMsg
        {
            get
            {
                return txtDetailMsg.Text;
            }
            set
            {
                txtDetailMsg.Text = value;
            }
        }
        public FrmInterLock()
        {
            InitializeComponent();
            TopMost = true;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            base.OnClosing(e);
        }
        private void btnMainForward_Click(object sender, EventArgs e)
        {            
            this.Hide();
        }
    }
}
