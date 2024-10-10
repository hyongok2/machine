using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DitSharedMemoryPacket;
using Dit.Framework.Ini;
using System.IO;
using DitSharedMemoryClient.Log;
using Dit.Framework.Log;
using System.Diagnostics;
using Dit.Framework.Util;

namespace DitSharedMemoryClient
{
    public partial class FrmMain : Form
    {
        private SMSyncClientMgr _sessionMgr = new SMSyncClientMgr();
        private SMSyncSession _session = new SMSyncSession("127.0.0.1", 7160);
        private bool AutoStart = false;
        public FrmMain()
        {
            CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
            InitializeSetting();


            string shortCut = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), Path.GetFileName(Application.ExecutablePath));

            if (File.Exists(shortCut) == false)
            {
                UserShortCut.Create(Application.ExecutablePath, Environment.SpecialFolder.Startup);
                UserShortCut.Create(Application.ExecutablePath, Environment.SpecialFolder.Desktop);
            }

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

                string ip = ini.GetString(section, string.Format("IP_{0}", iPos));
                string name = ini.GetString(section, string.Format("Name_{0}", iPos));
                int size = ini.GetInteger(section, string.Format("Size_{0}", iPos), 0);

                int syncCommand = ini.GetInteger(section, string.Format("SyncCommand_{0}", iPos), 0);
                int syncStart = ini.GetInteger(section, string.Format("SyncStart_{0}", iPos), 0);
                int syncSize = ini.GetInteger(section, string.Format("SyncSize_{0}", iPos), 0);
                int syncTime = ini.GetInteger(section, string.Format("SyncTime_{0}", iPos), 10);

                SMSyncSession session = new SMSyncSession(ip, 7160);
                session.SMName = name;
                session.SMSize = size;
                session.SyncCommand = syncCommand;
                session.SyncStart = syncStart;
                session.SyncSize = syncSize;
                session.SyncTime = syncTime;

                _sessionMgr.LstSession.Add(session);
            }

            //로그 파일 설정. 
            Logger.FileLogger = new Dit.Framework.Log.SimpleFileLoggerMark5(Path.Combine(Application.StartupPath, "Log"), "CLIENT", 2000, 1024 * 1024 * 20, lstLogger);
            Logger.FileLogger.AppendLine(LogLevel.Info, "InitializeSetting");

            lstShMem.Items.Clear();
            foreach (SMSyncSession sh in _sessionMgr.LstSession)
                lstShMem.Items.Add(
                    new ListViewItem(
                        new string[] { 
                            "", 
                            sh.IP, 
                            sh.SyncCommand == 1? "전송" : "수신",
                            sh.SMName, 
                            sh.SMSize.ToString(),                            
                            sh.SyncStart.ToString(),
                            sh.SyncSize.ToString(),
                            "-",
                            "-",
                            "-",
                            "-",
                            "-",
                            "-",
                }) { Tag = sh });
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
        private void btnConnect_Click(object sender, EventArgs e)
        {
        }
        private void btnReadTest_Click(object sender, EventArgs e)
        {
            PkSyncReadReq pk = new PkSyncReadReq() { Name = "TEST", Start = 0, Length = 10 };
            _session.SendPacket(pk);
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _sessionMgr.Start();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            _sessionMgr.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }
        private void tmrState_Tick(object sender, EventArgs e)
        {
            tbStatus.Text = GetCurrentStatus();
            UpdateSync();
        }


        private double MaxTime = double.MinValue;
        private double MinTime = double.MaxValue;

        private void UpdateSync()
        {
            for (int iPos = 0; iPos < lstShMem.Items.Count; iPos++)
            {
                SMSyncSession ss = lstShMem.Items[iPos].Tag as SMSyncSession;
                if (ss == null) continue;

                lock (ss.LstSyncResult)
                {
                    ss.LstSyncResult.RemoveAll((f) => { return f.ReqTime < DateTime.Now.AddSeconds(-1); });

                    double count = ss.LstSyncResult.Count;
                    double max = count > 0 ? ss.LstSyncResult.Max(f => (f.RspTime - f.ReqTime).TotalMilliseconds) : 0;
                    double min = count > 0 ? ss.LstSyncResult.Min(f => (f.RspTime - f.ReqTime).TotalMilliseconds) : 0;
                    double Average = count > 0 ? ss.LstSyncResult.Average(f => (f.RspTime - f.ReqTime).TotalMilliseconds) : 0;

                    MaxTime = MaxTime < max ? max : MaxTime;
                    MinTime = MinTime > min ? min : MinTime;

                    lstShMem.Items[iPos].SubItems[7].Text = ss.IsConnected ? "연결" : "미연결";
                    lstShMem.Items[iPos].SubItems[8].Text = Math.Round(count, 2).ToString();
                    lstShMem.Items[iPos].SubItems[9].Text = Math.Round(Average, 2).ToString();
                    lstShMem.Items[iPos].SubItems[10].Text = Math.Round(MinTime, 2).ToString();
                    lstShMem.Items[iPos].SubItems[11].Text = Math.Round(MaxTime, 2).ToString();
                    lstShMem.Items[iPos].SubItems[12].Text = ss.SyncSec.ToString();


                    if (count == 0 && ss.IsConnected == true && (PcDateTime.Now - ss.ConnectDateTime).TotalSeconds > 5)
                    {
                        Logger.FileLogger.AppendLine(LogLevel.Warning, "Sync Count Zero, Name = {0}, Start ={1}, Size ={2}", ss.SMName, ss.SyncStart, ss.SyncSize);
                        ss.Disconnect();
                    }
                }
            }
        }
        private string GetCurrentStatus()
        {
            Process p = Process.GetCurrentProcess();
            string s = string.Format(" 스레드 {0}, 핸들 {1}, 메모리 사용 {2}kb, 피크 메모리 사용 {3}kb, 버전 {4}, DIT.WSLEE", p.Threads.Count, p.HandleCount, p.WorkingSet64 / 1024, p.PeakWorkingSet64 / 1024, Application.ProductVersion);
            p.Close();
            p.Dispose();
            return s;
        }

        private void tsmiShow_Click(object sender, EventArgs e)
        {
            this.Show();
        }
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void NofiyIconApp_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }

        private void tmrAutoStart_Tick(object sender, EventArgs e)
        {
            if (tmrAutoStart.Enabled == false) return;
            tmrAutoStart.Enabled = false;
            btnStart_Click(null, null);
        }

        private void tbStatus_DoubleClick(object sender, EventArgs e)
        {
            Process.Start(Path.Combine(Application.StartupPath, "release.txt"));
        }
    }
}
