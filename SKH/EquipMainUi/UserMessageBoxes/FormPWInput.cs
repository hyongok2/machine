using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

public partial class FormPWInput : Form
{
    public string Passwd { get; set; }   

    public FormPWInput(string info)
    {
        InitializeComponent();

        StartPosition = FormStartPosition.CenterParent;
        lblPwInfo.Text = info != null ? info : "비밀번호를 입력하세요.";
    }
    // 이벤트 오버라이드
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }
    protected override void OnShown(EventArgs e)
    {   
        base.OnShown(e);
    }
    private void btnOK_Click(object sender, EventArgs e)
    {
        Passwd = txtPasswd.Text;
        this.DialogResult = DialogResult.OK;
        this.Close();
    }
    private void txtPasswd_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            btnOK_Click(null, null);
    }

}
