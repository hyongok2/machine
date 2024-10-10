using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DitSharedMemoryClient
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            uint aa = uint.MaxValue;
            aa = aa + 1;

            bool createNew;
            System.Threading.Semaphore s = new System.Threading.Semaphore(1, 2, "DIT.WSLEE.DitSharedMemoryClient", out createNew);

            if (createNew == false)
            {
                MessageBox.Show("이미 실행중입니다.", "DIT DitSharedMemoryClient", MessageBoxButtons.OK);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
