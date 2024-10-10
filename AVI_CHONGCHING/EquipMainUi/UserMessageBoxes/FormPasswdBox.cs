using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EquipMainUi.Setting;

public partial class FormPasswdBox : Form
{
    // 멤버 변수 
    private string _passwd;
     
    public string Passwd
    {
        get { return _passwd; }
        set { _passwd = value; }
    }
    

    public EM_LV_LST ID { get; set; }
    
    // 생성자
    public FormPasswdBox()
    {
        InitializeComponent();

        InitializeLevel();
    }
    private void InitializeLevel()
    {
        radioBtn1.Checked = true;
    }
    // 이벤트 오버라이드
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }
    protected override void OnShown(EventArgs e)
    {
        //txtPasswd.Text = _passwd;
        base.OnShown(e);
    } 
    private void btnOK_Click(object sender, EventArgs e)
    {
        //ID = (EM_LV_LST)combLevel.SelectedIndex;
        //_passwd = 
        //if(txtPasswd.Text == null)
        //{
        //    return;
        //}
        //if (Passwd == txtPasswd.Text)
        //{
        //    this.DialogResult = DialogResult.OK;
        //}
        //else
        //{
        //    label1.Text = "비밀번호가 틀립니다.";
        //    txtPasswd.Text = "";
        //}
        this.DialogResult = DialogResult.OK;
        
        //this.Close();
    }
    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
    private void btnlPasswd_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;

        if (btn == btnlNum1)
            txtPasswd.Text += "1";
        else if (btn == btnNum2)
            txtPasswd.Text += "2";
        else if (btn == btnNum3)
            txtPasswd.Text += "3";
        else if (btn == btnNum4)
            txtPasswd.Text += "4";
        else if (btn == btnNum5)
            txtPasswd.Text += "5";
        else if (btn == btnNum6)
            txtPasswd.Text += "6";
        else if (btn == btnNum7)
            txtPasswd.Text += "7";
        else if (btn == btnNum8)
            txtPasswd.Text += "8";
        else if (btn == btnNum9)
            txtPasswd.Text += "9";
        else if (btn == btnNum0)
            txtPasswd.Text += "0";
        else if (btn == btnClear)
            txtPasswd.Text = "";
        else if (btn == btnBackspace)
        {
            if (txtPasswd.Text == "" || txtPasswd.Text == null) return;

            txtPasswd.Text = txtPasswd.Text.Substring(0, _passwd.Length - 1);
        }
    }

    private void radioBtnLevel_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton CheckedRadioBtn = sender as RadioButton;

        if (CheckedRadioBtn == radioBtn1)
            ID = EM_LV_LST.USER;
        else if (CheckedRadioBtn == radioBtn2)
            ID = EM_LV_LST.ENGINEER;
        //else if (CheckedRadioBtn == radioBtn3)
        //    ID = EM_LV_LST.DEVELOP;
        else if (CheckedRadioBtn == radioBtn4)
            ID = EM_LV_LST.SUPERVISOR;
        else
            throw new Exception("라디오 버튼 알수 없는 에러!!");
    }
    private void txtBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        //숫자,백스페이스,마이너스,소숫점 만 입력받는다.
        if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != 8) //8:백스페이스,45:마이너스,46:소수점
        {
            e.Handled = true;
        }
    }
}

