using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dit.Framework.UI.UserComponent;
using Dit.Framework.Comm;

namespace EquipMainUi.ConvenienceClass
{
    public static class ExtensionUI
    {
        public static void ChangeEnabledControl(Control parent, bool isEnable)
        {
            List<Control> childControls = new List<Control>();
            GetAllControl(parent, childControls);

            foreach (Control control in childControls)
            {
                control.Enabled = isEnable;
            }
        }
        public static void AddClickEventLog(Control parent)
        {
            List<Control> childControls = new List<Control>();
            GetAllControl(parent, childControls);
            foreach (Control control in childControls)
            {
                if (control.GetType() == typeof(ButtonLabel) ||
                    control.GetType() == typeof(Button) ||
                    control.GetType() == typeof(Label))
                {
                    control.Click += new EventHandler(SomeButton_Click);
                }
                if (control.GetType() == typeof(ButtonDelay2))
                {
                    ButtonDelay2 bd = control as ButtonDelay2;
                    bd.Click += new EventHandler(SomeButton_Click);
                    bd.DelayClick += new EventHandler(SomeButton_DelayClick);
                }
            }
        }
        public static DateTime LogoutTime = PcDateTime.Now;
        private static void SomeButton_DelayClick(Object sender, EventArgs e)
        {
            Control control = (Control)sender;
            LogoutTime = PcDateTime.Now;
            Logger.Log.AppendLine(LogLevel.NoLog, "Name:{0} Text:{1} 버튼 딜레이 클릭", control.Name, control.Text);
        }
        private static void SomeButton_Click(Object sender, EventArgs e)
        {
            LogoutTime = PcDateTime.Now;
            Control control = (Control)sender;
            Logger.Log.AppendLine(LogLevel.NoLog, "Name:{0} Text:{1} 버튼 클릭", control.Name, control.Text);
        }
        private static void GetAllControl(Control c, List<Control> list)
        {
            foreach (Control control in c.Controls)
            {
                list.Add(control);
                if (control.HasChildren)                
                    GetAllControl(control, list);                
            }
        }
    }
}
