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
    public partial class FrmManualClose : Form
    {
        public FrmManualClose()
        {
            InitializeComponent();
        }

        private DateTime _startTime;
        public void SetStartTime(DateTime time)
        {
            _startTime = time;
        }

        public void SetText(string title, string text)
        {
            Text = title;
            label1.Text = text;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value >= progressBar1.Maximum)
                progressBar1.Value = 0;
            else
                progressBar1.Value += 10;

            lblTime.Text = (DateTime.Now - _startTime).ToString("mm\\:ss\\.ff");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmManualClose_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
            Hide();
        }

        protected override void OnShown(EventArgs e)
        {
            timer1.Start();
            base.OnShown(e);
        }
    }
}
