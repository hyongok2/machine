using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct
{
    public class CheckMsgUnit
    {
        public CheckMsgUnit(bool isSucced, string checkMsg)
        {
            IsSucceed = isSucced;
            CheckMsg = checkMsg;
        }

        public bool IsSucceed { get; set; }
        public string CheckMsg { get; set; }
    }
    public class CheckMgr
    {
        public static Queue<CheckMsgUnit> LstCheck = new Queue<CheckMsgUnit>();
        public static void AddCheckMsg(bool isSucceed, string checkMsg)
        {
            lock (LstCheck)
            {
                if (LstCheck.Count < 1)
                    LstCheck.Enqueue(new CheckMsgUnit(isSucceed, checkMsg));
            }
        }
        public static void WaitCheckMsg(string msg)
        {
            FrmCheck frmCheck = new FrmCheck(true);
            frmCheck.lblCheckState.Text = "확인";
            frmCheck.CheckMsg = msg;
            frmCheck.ShowDialog();
        }
    }
}
