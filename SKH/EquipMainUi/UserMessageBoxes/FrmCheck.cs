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
    public partial class FrmCheck : Form
    {
        private bool _isOneTime = false;
        public string CheckMsg
        {
            get
            {
                return lblCheckMsg.Text;
            }
            set
            {
                lblCheckMsg.Text = value;
            }
        }
        public FrmCheck(bool isOneTime = false)
        {
            InitializeComponent();
            _isOneTime = isOneTime;
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
            if (_isOneTime)
                this.Close();
            else
                this.Hide();
        }
    }
}
