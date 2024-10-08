using EquipMainUi.Struct.TransferData;
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
    public partial class FrmRetryUserSelect : Form
    {
        public EmPopupFlow PopupFlow;
        private bool _isRetry;
        public bool IsRetry { get { return _isRetry; } }
        public FrmRetryUserSelect(string retryButtonMsg = "재시도", string outButtonMsg = "배출")
        {
            InitializeComponent();
            this.TopMost = true;
            btnRetry.Text = retryButtonMsg;
            btnOut.Text = outButtonMsg;
            ChangeChinaLanguage();
        }
        private void ChangeChinaLanguage()
        {
            if (GG.boChinaLanguage)
            {
                btnRetry.Text = "重试";       // 재시도
                btnOut.Text = "排出";	      // 배출
            }
        }

        public void RequestPopup(string title, string msg)
        {
            this.Text = title;
            lblMsg.Text = msg;
            PopupFlow = EmPopupFlow.PopupRequest;
        }

        private void FrmRetryUserSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
            Hide();
        }
        private void btnRetry_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btnRetry)
            {
                _isRetry = true;
            }
            else if (btn == btnOut)
            {
                _isRetry = false;
            }

            PopupFlow = EmPopupFlow.OK;
            Close();
        }
    }
}
