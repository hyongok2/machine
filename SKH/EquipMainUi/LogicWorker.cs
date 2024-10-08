using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EquipMainUi.Struct;
using System.Diagnostics;

namespace EquipMainUi
{
    public class LogicWorker
    {
        private Thread _worker;
        private Thread _mongod;
        private bool _running = false;
        public Equipment Equip { get; set; }
        public Queue<long> ScanTimes = new Queue<long>();
        public DateTime _scanTimeLogTime = DateTime.Now;
        public long CurrScanTime { get; set; }
        public long MaxScanTime { get; set; }
        public long AvgScanTime { get; set; }
        public Stopwatch _stopwatch = new Stopwatch();

        public LogicWorker()
        {
        }
        public void Start()
        {
            _running = true;
            _worker = new Thread(new ThreadStart(ThreadWoking));
            _worker.Start();
            //_mongod = new Thread(new ThreadStart(MongoWorking));
            //_mongod.Start();
        }

        private void MongoWorking()
        {
            double CycleTime = 24;
            if (GG.Equip.CtrlSetting.Mongo.LastCycleTime.Year == 1)
            {
                GG.Equip.CtrlSetting.Mongo.LastCycleTime = DateTime.Now;
                GG.Equip.CtrlSetting.Save();
            }
            while (_running)
            {
                if ((DateTime.Now - GG.Equip.CtrlSetting.Mongo.LastCycleTime).TotalHours >= CycleTime)
                {
                    try
                    {
                        Struct.TransferData.TransferDataMgr.DateToDeleteCst();
                    }
                    catch (Exception ex)
                    {
                        Logger.ExceptionLog.AppendLine(LogLevel.Error, "MongoDB 자동 삭제 예외 발생");
                    }
                    
                }
                System.Threading.Thread.Sleep(100);
            }
        }

        public void Stop()
        {
            _running = false;
            _worker.Join();
        }
        private void ThreadWoking()
        {
            while (_running)
            {
                _stopwatch.Restart();
                Working();                 
                _stopwatch.Stop();
                
                CurrScanTime = _stopwatch.ElapsedMilliseconds;

                ScanTimes.Enqueue(CurrScanTime);
                if (ScanTimes.Count >= 100)
                {                    
                    AvgScanTime = ScanTimes.Sum() / ScanTimes.Count;
                    ScanTimes.Dequeue();
                }
                if (MaxScanTime < CurrScanTime)
                    MaxScanTime = CurrScanTime;
                if(MaxScanTime > 500)
                {
                    Logger.ExceptionLog.AppendLine(LogLevel.Info, string.Format("Scan Time 500ms 이상 {0}", MaxScanTime.ToString()));
                    MaxScanTime = 0;
                }
                System.Threading.Thread.Sleep(1);
            }
        }
        private void Working()
        {
            Equip.LogicWorking();
        }
        private void WorkingManual()
        {
        }
    }
}
