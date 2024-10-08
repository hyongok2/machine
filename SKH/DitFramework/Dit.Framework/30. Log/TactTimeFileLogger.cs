using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using System.Collections.Generic;
//using System.Drawing;
using System.Threading;

namespace Dit.Framework.Log
{
    public class TactTimeFileLogger
    {
        // 상수


        private static readonly int StorageDay = 60;        // 로그의 보관일수
        private static readonly string MessageSkip = "화면 출력 로그 생략...";


        // 필드 1
        private System.Windows.Forms.Timer _logTimer;
        private List<LoggingEventArgs> _logReporter;


        // 필드 2
        private StreamWriter _streamWriter;
        private DateTime _lastWriteTime;
        private string _rootPath;
        private long _fileSizeLimit = 10 * 1024 * 1024;      // 파일을 나눌 바이트 단위
        private ImageList _imgList = new ImageList();
        internal string _fileName;
        public ListView _logView = null;
        public string _header { get; set; }

        // 프로퍼티
        public DateTime LastWriteTime { get { return _lastWriteTime; } }
        public string FullLogPath { get; set; }
        public Object LogSender { get; set; }


        // 생성자
        public TactTimeFileLogger(string logPath, string baseFileName, double timerInterval, long fileSizeLimit, ListView logView, string header)
        {

            _header = header;
            _rootPath = logPath;
            _fileName = baseFileName;

            if (fileSizeLimit > 0)
                _fileSizeLimit = fileSizeLimit;

            _lastWriteTime = DateTime.Now;
            _logReporter = new List<LoggingEventArgs>();

            if (logView != null)
            {
                _logView = logView;
                InitializeLogView(_logView);
            }

            _logTimer = new System.Windows.Forms.Timer();
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
                                log.Level == LogLevel.Alarm ? 1 : 2;

                            if (logIcon == 3) continue;
                            if (_logView != null)
                                _logView.Items.Insert(0, new ListViewItem(new string[] { "", log.SignalTime.ToString("HH:mm:ss.fff"), log.LogMessage }, logIcon));

                        }
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    System.Threading.Monitor.Exit(_logTimer);
                }
            };

            _logTimer.Interval = (int)timerInterval;
            _logTimer.Start();

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
            logView.Columns.Add(new ColumnHeader() { Width = width + 50, Name = "chLog", Text = "LOG", TextAlign = HorizontalAlignment.Left });
            logView.Resize += delegate(object sender, EventArgs e)
            {
                logView.Columns[2].Width = logView.Width - 160 > 0 ? logView.Width - 160 : 300; ;
            };
        }

        // 인터페이스 구현 [ ILogger ]
        public void AppendLine()
        {
            _streamWriter.WriteLine(Environment.NewLine);
        }
        public void AppendLine(LogLevel lvl, string format, params object[] args)
        {
            string log = string.Format(format, args);
            AppendLine(lvl, log);
        }
        public void AppendLine(LogLevel lvl, string value)
        {
            lock (this)
            {
                if (_logReporter.Count > 100)
                {
                    _logReporter.Clear();
                    _logReporter.Add(new LoggingEventArgs() { Level = lvl, SignalTime = DateTime.Now, LogMessage = MessageSkip });
                }

                _logReporter.Add(new LoggingEventArgs() { Level = lvl, SignalTime = DateTime.Now, LogMessage = value });
                string log = string.Format("[{0:HH:mm:ss.fff}]\t{1}\t{2}", DateTime.Now, lvl.ToString(), value);
                SetFileStream(DateTime.Now);
                _streamWriter.WriteLine(log);
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
                _streamWriter.WriteLine(_header);

                _lastWriteTime = now;                
                return;
            }

            TimeSpan ts = now.Date.AddDays(1).AddSeconds(-1) - _lastWriteTime.Date;

            if (ts.TotalDays > 1)
            {
                _streamWriter.Close();
                _streamWriter.Dispose();
                _streamWriter = CreateStreamFromDate(now);
                _streamWriter.WriteLine(_header);
            }
            else if (_streamWriter.BaseStream.Length > _fileSizeLimit)
            {
                _streamWriter.Close();
                _streamWriter.Dispose();
                _streamWriter = CreateStreamFromDate(now);
                _streamWriter.WriteLine(_header);
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
            string fileName = string.Format("{0}[{1:d2}].csv", _fileName, count);
            FullLogPath = Path.Combine(dirPath, fileName);
            StreamWriter fs = new StreamWriter(FullLogPath, true);
            fs.AutoFlush = true;
            return fs;
        }

        // 인터페이스 구현 [ IDisposable ]
        public void Dispose()
        {
            if (_streamWriter != null)
            {
                _streamWriter.Close();
                _streamWriter.Dispose();
                _streamWriter = null;
            }
        }
    }
}
