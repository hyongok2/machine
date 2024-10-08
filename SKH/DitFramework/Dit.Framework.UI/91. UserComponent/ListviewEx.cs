﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dit.Framework.UI.UserComponent
{
    public class ListViewEx : ListView
    {
        public ListViewEx()
            : base()
        {
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.EnableNotifyMessage, true);
        }
        protected override void OnNotifyMessage(Message m)
        {
            if (m.Msg != 0x14)
                base.OnNotifyMessage(m);
        }
    }
}