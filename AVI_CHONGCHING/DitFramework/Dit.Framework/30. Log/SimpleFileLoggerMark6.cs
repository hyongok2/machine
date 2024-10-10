using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Dit.Framework.Log
{
    public class SimpleFileLoggerMark6
    {
        // 상수
        private int StorageDay = 60;        // 로그의 보관일수
        public int StorageDays { set { StorageDay = value; } get { return StorageDay; } }
        private static readonly string MessageSkip = "화면 출력 로그 생략...";

        // 필드 1
        private System.Windows.Forms.Timer _logTimer;
        private System.Timers.Timer _logDelTimer;
        private List<LoggingEventArgs> _logReporter;
        private Queue<string> _queLog = new Queue<string>(5000);

        // 필드 2
        private StreamWriter _streamWriter;
        private DateTime _lastWriteTime;
        private string _rootPath;
        private long _fileSizeLimit = 10 * 1024 * 1024;      // 파일을 나눌 바이트 단위
        private ImageList _imgList = new ImageList();
        public string _fileName;
        public ListView _logView = null;
        private string _name = "log";
        private Thread _logWorker;
        private bool _running = false;
        private short _padRightLength = 20;
        private bool _isLevelWrite = false;
        // 프로퍼티
        public DateTime LastWriteTime { get { return _lastWriteTime; } }
        public string FullLogPath { get; set; }
        public Object LogSender { get; set; }
        public bool _createDateFolder = false;

        public bool IsViewEnable { get; set; }




        // 생성자
        public SimpleFileLoggerMark6(string logPath, string name, string baseFileName, double timerInterval, long fileSizeLimit, bool createDateFolder, bool isLevelWrite, ListView logView)
        {
            IsViewEnable = true;
            _rootPath = logPath;
            _fileName = baseFileName;
            _name = name;

            _isLevelWrite = isLevelWrite;
            if (fileSizeLimit > 0)
                _fileSizeLimit = fileSizeLimit;

            _lastWriteTime = DateTime.Now;
            _logReporter = new List<LoggingEventArgs>();
            _createDateFolder = createDateFolder;

            if (logView != null)
            {
                _logView = logView;
                InitializeLogView(_logView);
            }

            _logTimer = new System.Windows.Forms.Timer();
            _logTimer.Interval = (int)timerInterval;
            _logTimer.Tick += delegate(object sender, EventArgs e)
            {
                if (System.Threading.Monitor.TryEnter(_logTimer))
                {
                    try
                    {
                        List<LoggingEventArgs> logReporter;
                        lock (_logReporter)
                        {
                            logReporter = _logReporter;
                            _logReporter = new List<LoggingEventArgs>();
                        }

                        foreach (LoggingEventArgs log in logReporter)
                        {
                            int logIcon = log.Level == LogLevel.Interlock ? 0 :
                                log.Level == LogLevel.Alarm ? 1 : 2;;

                            if (logIcon == 3) continue;
                            if (_logView != null && IsViewEnable == true)
                            {
                                if (_logView.Items.Count > 100)
                                    _logView.Items.RemoveAt(100);

                                _logView.Items.Insert(0, new ListViewItem(new string[] { "", log.SignalTime.ToString("HH:mm:ss.fff"), name, log.LogMessage }, logIcon));
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

            _logTimer.Start();


            _logDelTimer = new System.Timers.Timer(10000);
            _logDelTimer.Elapsed += delegate(object sender, ElapsedEventArgs e)
            {
                if (System.Threading.Monitor.TryEnter(_logDelTimer))
                {
                    try
                    {
                        if (_createDateFolder == false && DateTime.Now.Day != _lastDeleteTime.Day)
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

        private void InitializeLogView(ListView logView)
        {
            _imgList.Images.Add(System.Drawing.SystemIcons.Warning);
            _imgList.Images.Add(System.Drawing.SystemIcons.Error);
            _imgList.Images.Add(System.Drawing.SystemIcons.Information);

            logView.View = View.Details;
            logView.FullRowSelect = true;
            logView.SmallImageList = _imgList;
            logView.LargeImageList = _imgList;

            int width = logView.Width - 160 > 0 ? logView.Width - 160 : 300;
            logView.Columns.Add(new ColumnHeader() { Width = 30, Name = "chNo", Text = "-" });
            logView.Columns.Add(new ColumnHeader() { Width = 100, Name = "chTime", Text = "TIME", TextAlign = HorizontalAlignment.Center });
            logView.Columns.Add(new ColumnHeader() { Width = 50, Name = "chName", Text = "ID", TextAlign = HorizontalAlignment.Center });
            logView.Columns.Add(new ColumnHeader() { Width = width + 300, Name = "chLog", Text = "LOG", TextAlign = HorizontalAlignment.Left });
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
            //_streamWriter.WriteLine(Environment.NewLine);
            _queLog.Enqueue(Environment.NewLine);
        }
        public void AppendLine(LogLevel lvl, string subject, string value)
        {
            if (subject.Length > _padRightLength)
                _padRightLength = (short)subject.Length;

            AppendLine(lvl, string.Format("[{0}] {1}", subject.PadRight(_padRightLength, ' '), value));
        }
        public void AppendLine(LogLevel lvl, string subject, string format, params object[] args)
        {
            if (subject.Length > _padRightLength)
                _padRightLength = (short)subject.Length;

            string log = string.Format(format, args);
            AppendLine(lvl, string.Format("[{0}] {1}", subject.PadRight(_padRightLength, ' '), log));
        }
        private void AppendLine(LogLevel lvl, string value)
        {
            DateTime start = DateTime.Now;
            lock (this)
            {
                if (_logReporter.Count > 100)
                {
                    _logReporter.Clear();
                    _logReporter.Add(new LoggingEventArgs() { Level = lvl, SignalTime = DateTime.Now, LogMessage = MessageSkip });
                }

                _logReporter.Add(new LoggingEventArgs() { Level = lvl, SignalTime = DateTime.Now, LogMessage = value });
            }

            TimeSpan ts1 = (DateTime.Now - start);


            //string log = string.Format("[{0:HH:mm:ss.fff}]\t{1}\t{2}", DateTime.Now, lvl.ToString(), value);
            string log = string.Empty;
            
            if(_isLevelWrite == true)
                log = string.Format("[{0:HH:mm:ss.fff}]\t{1}\t{2}\t{3}", DateTime.Now, "", lvl.ToString().PadRight(20, ' '), value.ToUpper());
            else
                log = string.Format("[{0:HH:mm:ss.fff}]\t{1}\t{2}", DateTime.Now, "", value.ToUpper());

            TimeSpan ts2 = (DateTime.Now - start);

            //lock (_queLog)
            _queLog.Enqueue(log);


            TimeSpan ts3 = (DateTime.Now - start);

            if (ts1.TotalMilliseconds > 2 || ts2.TotalMilliseconds > 2 || ts3.TotalMilliseconds > 2)
            {
                Console.WriteLine("");
            }
        }
        public void Clear()
        {
            _logReporter.Clear();
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

            if (_lastWriteTime.Day != now.Day)
            {
                _streamWriter.Close();
                _streamWriter.Dispose();
                _streamWriter = CreateStreamFromDate(now);
            }
            else if (_streamWriter.BaseStream.Length > _fileSizeLimit)
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
            string dirPath = _rootPath;
            if (_createDateFolder)
                dirPath = Path.Combine(_rootPath, now.ToString("yy-MM-dd"));

            if (Directory.Exists(dirPath) == false)
            {
                DirectoryInfo root = new DirectoryInfo(_rootPath);
                DeleteFilesAndDirectories(root, root, DateTime.Now.Date.AddDays(-StorageDay));
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
