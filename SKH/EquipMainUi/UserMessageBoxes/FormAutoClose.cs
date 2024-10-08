using EquipMainUi;
using EquipMainUi.Struct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

public partial class FormAutoClose : Form
{
    // 멤버 변수
    private int _seconds = 10;
    private string _message;

    // 프로퍼티
    public int Seconds
    {
        get { return _seconds; }
        set { _seconds = value; }
    }
    public string Message
    {
        get { return _message; }
        set { _message = value; }
    }

    // 생성자
    public FormAutoClose()
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
        SetMessage();
        if (Seconds != 0)
            timer1.Start();

        label1.Text = _message;
        base.OnShown(e);
    }

    // 이벤트 처리기
    private void timer1_Tick(object sender, EventArgs e)
    {
        try
        {
            _seconds--;
            SetMessage();

            if (_seconds <= 0)
                this.Close();
        }
        catch (Exception ex)
        {
            if (AlarmMgr.Instance.IsHappened(GG.Equip, EM_AL_LST.AL_0946_UI_EXCEPTION) == false)
            {
                Logger.ExceptionLog.AppendLine(LogLevel.Error, string.Format("UI 갱신 예외 발생 : {0}", ex.Message));
                Logger.ExceptionLog.AppendLine(LogLevel.Error, EquipMainUi.Log.EquipStatusDump.CallStackLog());
                AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0946_UI_EXCEPTION);
            }
        }
    }

    // 메서드
    private void SetMessage()
    {
        btnOK.Text = string.Format("확인 = {0}", _seconds);
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.OK;
        this.Close();
    }
}
