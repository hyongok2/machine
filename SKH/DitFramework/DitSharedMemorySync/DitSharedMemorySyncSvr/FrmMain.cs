using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Dit.FrameworkSingle.Net.ServerClinet;
using Dit.Framework.Net.AsyncSocket;
using Dit.Framework.Ini;
using DitSharedMemorySvr.Log;
using System.IO;
using Dit.Framework.Log;
using DitSharedMemoryPacket;
using Dit.Framework.Util;

namespace DitSharedMemorySvr
{
    public partial class FrmMain : Form
    {
        private SeverManager _severManager = new SeverManager();
        private bool AutoStart = false;
        public FrmMain()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            string shortCut = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), Path.GetFileName(Application.ExecutablePath));
            if (File.Exists(shortCut) == false)
            {
                UserShortCut.Create(Application.ExecutablePath, Environment.SpecialFolder.Startup);
                UserShortCut.Create(Application.ExecutablePath, Environment.SpecialFolder.Desktop);
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitializeSetting();


            string shortCut = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), Path.GetFileName(Application.ExecutablePath));

            if (File.Exists(shortCut) == false)
                UserShortCut.Create(Application.ExecutablePath, shortCut);

            if (AutoStart)
                tmrAutoStart.Start();
        }
        private void InitializeSetting()
        {
            GG.Setting.Load(GG.Setting.PathOfSetting);
            IniReader ini = new IniReader(GG.Setting.PathOfSetting);
            AutoStart = ini.GetBoolean("Setting", "AutoStart", true);
            for (int iPos = 0; iPos < GG.Setting.Count; iPos++)
            {
                string section = "Memory";
                string name = ini.GetString(section, string.Format("Name_{0}", iPos));
                int size = ini.GetInteger(section, string.Format("Size_{0}", iPos), 0);

                GG.SMMgr.LstShardMemory.Add(name, new ShardMem() { Name = name, Size = size });
            }

            //서버 접속 여부. 
            _severManager.OnAccepted += new EventHandler(SeverManager_OnAccepted);
            _severManager.OnClosed += new EventHandler(SeverManager_OnClosed);

            //로그 파일 설정. 
            Logger.FileLogger = new Dit.Framework.Log.SimpleFileLoggerMark5(Path.Combine(Application.StartupPath, "Log"), "SVR", 2000, 1024 * 1024 * 20, lstLogger);
            Logger.FileLogger.AppendLine(LogLevel.Info, "InitializeSetting");

            //등록된 공유 메모리 리스트
            lstShMem.Items.Clear();
            foreach (ShardMem sh in GG.SMMgr.LstShardMemory.Values)
                lstShMem.Items.Add(new ListViewItem(new string[] { "", sh.Name, sh.Size.ToString() }));

            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }
        public void SeverManager_OnClosed(object sender, EventArgs e)
        {
            SMSvrSession ss = sender as SMSvrSession;
            if (ss != null)
                Logger.FileLogger.AppendLine(LogLevel.Info, "CLOSED IP = {0}, PORT = {1} ", ss.RemoteEndPoint.Address.ToString(), ss.RemoteEndPoint.Port.ToString());
        }
        public void SeverManager_OnAccepted(object sender, EventArgs e)
        {
            SMSvrSession ss = sender as SMSvrSession;
            if (ss != null)
                Logger.FileLogger.AppendLine(LogLevel.Info, "OPEN IP = {0}, PORT = {1} ", ss.RemoteEndPoint.Address.ToString(), ss.RemoteEndPoint.Port.ToString());
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (btnStart.Enabled == true)
            {
                if (MessageBox.Show("종료하시겠습니까?", "공유 메모리 싱크", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                    e.Cancel = true;
            }
            else
            {
                e.Cancel = true;
                this.Hide();
            }
            base.OnClosing(e);
        }
        private void tmrState_Tick(object sender, EventArgs e)
        {
            tbStatus.Text = GetCurrentStatus();
        }
        private string GetCurrentStatus()
        {
            Process p = Process.GetCurrentProcess();
            string s = string.Format(" 스레드 {0}, 핸들 {1}, 메모리 사용 {2}kb, 피크 메모리 사용 {3}kb, 버전 {4}, DIT.WSLEE", p.Threads.Count, p.HandleCount, p.WorkingSet64 / 1024, p.PeakWorkingSet64 / 1024, Application.ProductVersion);
            p.Close();
            p.Dispose();
            return s;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
        }
        private void Start()
        {
            GG.SMMgr.Open();
            _severManager.Port = 7160;
            _severManager.Start();

            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            _severManager.Stop();

            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void NofiyIconApp_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }

        private void tsmiShow_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            if (btnStart.Enabled == false)
            {
                MessageBox.Show("먼저 프로그램을 정지하여 주세요!");
            }
            else
            {
                this.Close();
            }
        }


        private void tmrAutoStart_Tick(object sender, EventArgs e)
        {
            if (tmrAutoStart.Enabled == false) return;
            tmrAutoStart.Enabled = false;

            if (btnStart.Enabled)
                btnStart_Click(null, null);
        }

        private void tbStatus_DoubleClick(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(Application.StartupPath, "release.txt"));
        }
    }
}
