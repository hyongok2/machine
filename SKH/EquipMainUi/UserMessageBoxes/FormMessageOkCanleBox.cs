using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

public partial class FormMessageOkCanleBox : Form
{
    // 멤버 변수 
    private string _message;
    private bool Result = false;
    public bool Dlg_Result
    {
        get { return Result; }
        set { Result = value; }
    }
 
    public string Message
    {
        get { return _message; }
        set { _message = value; }
    }

    // 생성자
    public FormMessageOkCanleBox()
    {
        InitializeComponent();
    }

    // 이벤트 오버라이드
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }
    protected override void OnShown(EventArgs e)
    { 
       
        label1.Text = _message;
        base.OnShown(e);
    } 

    private void btnOK_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.OK;
        this.Result = true;
        this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Result = false;
        this.Close();
    }
}
