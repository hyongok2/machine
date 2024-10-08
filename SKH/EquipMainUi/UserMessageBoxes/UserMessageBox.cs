using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

public static class UserMessageBox
{
    // 대리자
    public delegate void Worker();

    // 메서드
    public static System.Windows.Forms.DialogResult ShowDialogAutoClose(string text, string caption, int seconds)
    {
        FormAutoClose f = new FormAutoClose();
        f.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        f.Message = text;
        f.Text = caption;
        f.Seconds = seconds;
        return f.ShowDialog();
    }

    // 메서드
    public static  void ShowAutoClose(string text, string caption, int seconds)
    {
        FormAutoClose f = new FormAutoClose();
        f.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        f.Message = text;
        f.Text = caption;
        f.Seconds = seconds;
        f.Show();
    }


    // 메서드
    public static System.Windows.Forms.DialogResult ShowMessageOkCanleBox(string text, string caption)
    {
        FormMessageOkCanleBox f = new FormMessageOkCanleBox();
        f.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        f.Message = text;
        f.Text = caption;
         
        return f.ShowDialog();
    }
}
