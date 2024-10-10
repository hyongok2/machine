using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Dit.Framework.PLC;
using System.Collections;


namespace Dit.Framework.Log
{
    public class LinkSignalFileLogger
    {
        // 상수
        private int StorageDay = 60;        // 로그의 보관일수
        public int StorageDays { set { StorageDay = value; } get { return StorageDay; } }
        private static readonly string MessageSkip = "화면 출력 로그 생략...";

        // 필드 1
        private System.Windows.Forms.Timer _logTimer;
        private System.Timers.Timer _logDelTimer;
        private Queue<string> _queLog = new Queue<string>(5000);

        // 필드 2
        private StreamWriter _streamWriter;
        private DateTime _lastWriteTime;
        private string _rootPath;
        public string _fileName;

        private Thread _logWorker;
        private bool _running = false;

        // 프로퍼티
        public DateTime LastWriteTime { get { return _lastWriteTime; } }
        public string FullLogPath { get; set; }
        public Object LogSender { get; set; }

        public IVirtualMem PLC { get; set; }
        private PlcAddr _upStartAddr = null;
        private PlcAddr _downStartAddr = null;

        private short[] _oldUpMem = null;
        private short[] _oldDownMem = null;

        // 생성자
        public LinkSignalFileLogger(string logPath, string baseFileName)
        {
            _rootPath = logPath;
            _fileName = baseFileName;

            _lastWriteTime = DateTime.Now;

            _logTimer = new System.Windows.Forms.Timer();
            _logTimer.Interval = (int)50;
            _logTimer.Tick += delegate(object sender, EventArgs e)
            {
                if (System.Threading.Monitor.TryEnter(_logTimer))
                {
                    try
                    {
                        if (PLC != null)
                        {
                            short[] upMem = PLC.VirGetShorts(_upStartAddr);
                            short[] downMem = PLC.VirGetShorts(_downStartAddr);

                            if (_oldUpMem == null)
                            {
                                _oldUpMem = new short[upMem.Length];
                                _oldDownMem = new short[downMem.Length];
                                Array.Copy(upMem, _oldUpMem, upMem.Length);
                                Array.Copy(downMem, _oldDownMem, downMem.Length);

                                AppendLine(upMem, downMem);
                            }
                            else if (upMem.SequenceEqual(_oldUpMem) == false || downMem.SequenceEqual(_oldDownMem) == false)
                            {
                                Array.Copy(upMem, _oldUpMem, upMem.Length);
                                Array.Copy(downMem, _oldDownMem, downMem.Length);

                                AppendLine(upMem, downMem);
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        System.Threading.Monitor.Exit(_logTimer);
                    }
                }
            };

            //_logTimer.Start();


            _logDelTimer = new System.Timers.Timer(10000);
            _logDelTimer.Elapsed += delegate(object sender, ElapsedEventArgs e)
            {
                if (System.Threading.Monitor.TryEnter(_logDelTimer))
                {
                    try
                    {
                        if (DateTime.Now.Day != _lastDeleteTime.Day)
                        {
                            DirectoryInfo root = new DirectoryInfo(_rootPath);
                            DeleteFilesAndDirectories(root, root, DateTime.Now.Date.AddDays(-StorageDay));

                            _lastDeleteTime = DateTime.Now;
                        }
                    }
                    finally
                    {
                        System.Threading.Monitor.Exit(_logDelTimer);
                    }
                }
            };
            _logDelTimer.Start();

            _running = true;
            _logWorker = new Thread(new ThreadStart(LogWorking));
            _logWorker.Start();

        }
        private void LogWorking()
        {
            while (_running)
            {
                string log = string.Empty;
                while (_queLog.Count > 0)
                {
                    lock (_queLog)
                        log = _queLog.Dequeue();

                    SetFileStream(DateTime.Now);
                    _streamWriter.WriteLine(log);
                }

                Thread.Sleep(1);
            }
        }

        // 인터페이스 구현 [ ILogger ]
        public void AppendLine()
        {
            _queLog.Enqueue(Environment.NewLine);
        }
        public void StartTimer(IVirtualMem plc, PlcAddr upStart, int upReadLength, PlcAddr downStart, int downReadLength)
        {
            this.PLC = plc;
            _upStartAddr = upStart;
            _upStartAddr.Length = upReadLength;
            _downStartAddr = downStart;
            _downStartAddr.Length = downReadLength;
            _logTimer.Start();
        }
        public void AppendLine(short[] upMem, short[] downMem)
        {
            StringBuilder sbUp = new StringBuilder();
            StringBuilder sbDown = new StringBuilder();

            for (int iPos = upMem.Length - 1; iPos >= 0; iPos--)
            {
                string upStr = Convert.ToString(upMem[iPos], 2).PadLeft(16, '0');
                string downStr = Convert.ToString(downMem[iPos], 2).PadLeft(16, '0');
                sbUp.AppendFormat(upStr);
                sbDown.AppendFormat(downStr);
            }

            string log = string.Format("[{0:HH:mm:ss.fff}]\t{1}{2}", DateTime.Now, sbDown.ToString().Substring(sbDown.Length - 40, 40), sbUp.ToString().Substring(sbUp.Length - 40, 40));
            _queLog.Enqueue(log);
        }
        public void Clear()
        {
        }
        // 메서드
        private void SetFileStream(DateTime now)
        {
            if (Directory.Exists(_rootPath) == false)
                Directory.CreateDirectory(_rootPath);

            if (_streamWriter == null)
            {
                _streamWriter = CreateStreamFromDate(now);
                _lastWriteTime = now;
                return;
            }

            //TimeSpan ts = now.AddHours(1).AddMinutes(-1) - _lastWriteTime;

            //if(ts.TotalHours > 1)
            if (_lastWriteTime.Hour != now.Hour)
            {
                _streamWriter.Close();
                _streamWriter.Dispose();
                _streamWriter = CreateStreamFromDate(now);
            }

            // [121009] 밖으로 이동
            _lastWriteTime = now;
        }        
        private void DeleteFilesAndDirectories(DirectoryInfo root, DirectoryInfo curPath, DateTime deadline)
        {
            foreach (FileInfo curFile in curPath.GetFiles())
            {
                if (curFile.LastWriteTime < deadline)
                {
                    try
                    {
                        curFile.Delete();
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (System.Security.SecurityException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (UnauthorizedAccessException e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }

            if (curPath.GetDirectories().Length > 0)
            {
                foreach (DirectoryInfo subDir in curPath.GetDirectories())
                {
                    // 재귀 호출
                    DeleteFilesAndDirectories(root, subDir, deadline);
                }
            }

            if (curPath.GetFiles().Length == 0 && curPath.GetDirectories().Length == 0)
            {
                if (curPath != root)
                {
                    try
                    {
                        curPath.Delete();
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (System.Security.SecurityException e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
        private DateTime _lastDeleteTime = DateTime.Now;
        private StreamWriter CreateStreamFromDate(DateTime now)
        {
            string dirPath = Path.Combine(_rootPath, now.ToString("yy-MM-dd"));

            if (Directory.Exists(dirPath) == false)
            {
                DirectoryInfo root = new DirectoryInfo(_rootPath);
                DeleteFilesAndDirectories(root, root, now.Date.AddDays(-StorageDay));
                Directory.CreateDirectory(dirPath);
            }

            int count = Directory.GetFiles(dirPath, _fileName + "*.*").Length;
            string fileName = string.Format("{0}[{1:d2}].log", _fileName, count);
            FullLogPath = Path.Combine(dirPath, fileName);
            StreamWriter fs = new StreamWriter(FullLogPath, true);
            fs.AutoFlush = true;
            return fs;
        }

        // 인터페이스 구현 [ IDisposable ]
        public void Dispose()
        {
            _running = false;

            if (_streamWriter != null)
            {
                _streamWriter.Close();
                _streamWriter.Dispose();
                _streamWriter = null;
            }
        }
    }
}
