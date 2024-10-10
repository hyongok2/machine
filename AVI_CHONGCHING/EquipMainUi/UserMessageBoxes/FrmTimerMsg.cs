using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using EquipMainUi.Struct;

namespace EquipMainUi.UserMessageBoxes
{
    public partial class FrmTimerMsg : Form
    {
        private Stopwatch _stopwatch;
        private int _showTime;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="showTime">Second</param>
        /// <param name="msg"></param>
        public FrmTimerMsg(int showTime, string title, string msg)
        {
            InitializeComponent();

            this.Text = title;
            _showTime = showTime;
            pBarRemained.Maximum = showTime * 10;            
            lblMsg.Text = msg;
            _stopwatch = new Stopwatch();
            timer1.Tick += new EventHandler(timer1_Tick);            
        }

        public new void Show()
        {
            _stopwatch.Start();
            timer1.Start();
            base.Show();
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                double remained = _showTime - (_stopwatch.ElapsedMilliseconds / 1000);
                if (remained < 0)
                    remained = 0;

                lblRemainedTime.Text = remained.ToString("0.0");
                pBarRemained.Value = (_showTime - (int)remained) * 10;

                if (remained <= 0)
                {
                    DoClose();
                }
            }
            catch (Exception ex)
            {
                if (AlarmMgr.Instance.IsHappened(GG.Equip, EM_AL_LST.AL_0946_UI_EXCEPTION) == false)
                {
                    Logger.ExceptionLog.AppendLine(LogLevel.Error, string.Format("UI 갱신 예외 발생 : {0}", ex.Message));
                    Logger.ExceptionLog.AppendLine(LogLevel.Error, Log.EquipStatusDump.CallStackLog());
                    AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0946_UI_EXCEPTION);
                }
            }
        }
        public void DoClose()
        {
            timer1.Stop();
            this.Close();
        }
        private void FrmTimerMsg_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }        
    }
}
