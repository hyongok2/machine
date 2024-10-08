using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EquipMainUi.Struct;

namespace EquipMainUi.Setting
{
    public partial class FormPWNeedClose : Form
    {
        private bool _isCloseEnable { get; set; }
        private string password = "1";

        public Equipment _equip;

        public FormPWNeedClose(Equipment equip)
        {
            InitializeComponent();

            //this.TopMost = true;
            _equip = equip;
            _equip.IsInnerWorkOn = true;
        }
        private void btndPWInput_Click(object sender, EventArgs e)
        {
            if(GG.PrivilegeTestMode == true)
            {
                _isCloseEnable = true;
                _equip.IsInnerWorkOn = false;
                this.Close();
                return;
            }
            FormPWInput frmLogin = new FormPWInput("비밀번호를 입력하세요");
            frmLogin.ShowDialog();

            if (frmLogin.Passwd == "0000")
            {
                _isCloseEnable = true;
                _equip.IsInnerWorkOn = false;
                this.Close();
                return;
            }
            else
                return;
        }
        private void CloseCheck_Event(object sender, FormClosingEventArgs e)
        {
            if (_isCloseEnable)
                e.Cancel = false;
            else
                e.Cancel = true;
        }
    }
}
