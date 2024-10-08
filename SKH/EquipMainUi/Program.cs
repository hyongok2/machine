using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using EquipMainUi.ConvenienceClass;
using System.Diagnostics;

namespace EquipMainUi
{
    static class Program
    {
        static string EQUIPNAME;

        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            EQUIPNAME = "WaferInsp";
            bool createNew;
            System.Threading.Semaphore s = new System.Threading.Semaphore(1, 2, "DIT." + EQUIPNAME + ".EquipMainUi", out createNew);

            if (createNew == false)
            {
                MessageBox.Show("이미 실행중입니다.", "DIT DitSharedMemorySvr", MessageBoxButtons.OK);
                return;
            }

            Preprocessing();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }

        static void Preprocessing()
        {
#if DEBUG
            if (GG.TestMode)
            {
                string simulatorName = "EquipSimulator." + EQUIPNAME;
                Process[] processes = Process.GetProcessesByName(simulatorName);
                if (processes.Length == 0 &&
                    DialogResult.Yes == MessageBox.Show("시뮬레이션 프로그램을 먼저 실행하시겠습니까?", "시작하기전에", MessageBoxButtons.YesNo))
                {
                    string path = Application.StartupPath;
                    path = path.Replace("EquipMainUi", "EquipSimulator");
                    path = Path.Combine(path, simulatorName + ".exe");
                    if (File.Exists(path))
                        Process.Start(path);
                    else
                        MessageBox.Show("파일이 없습니다");
                    System.Threading.Thread.Sleep(1000);
                }
            }
#endif
        }
    }
}