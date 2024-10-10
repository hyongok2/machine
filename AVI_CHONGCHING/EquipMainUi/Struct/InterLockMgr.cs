using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct
{
    public struct InterlockMsg
    {
        public string Interlock { get; set; }
        public string Detail { get; set; }
    }
    /// <summary>
    /// detail Textbox 변경
    /// 인터락 Detail 텍스트박스 하나 더 추가해서 더 자세한 정보 제공
    /// date 170608 (0.8um 부터 추가)
    /// </summary>
    public class InterLockMgr
    {
        public static Queue<InterlockMsg> LstInterLock = new Queue<InterlockMsg>();
        public static void AddInterLock(string interLock, string detailFormat = "", params object[] args)
        {
            interLock = interLock.Replace("\n", Environment.NewLine); // textbox는 \n으로 개행안됨.
            detailFormat = detailFormat.Replace("\n", Environment.NewLine); // textbox는 \n으로 개행안됨.
            lock (LstInterLock)
            {
                if (LstInterLock.Count < 1)
                {
                    LstInterLock.Enqueue(new InterlockMsg { Interlock = interLock, Detail = string.Format(detailFormat, args) });
                    Logger.Log.AppendLine(LogLevel.NoLog, "INTERLOCK MSG POPUP : {0}_{1}", interLock, string.Format(detailFormat, args));
                }
            }
        }
    }
}
