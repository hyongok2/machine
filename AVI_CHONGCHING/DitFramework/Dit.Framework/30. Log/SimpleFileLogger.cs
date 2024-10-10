using System;
using System.IO;
using System.Text;
using System.Timers;

namespace Dit.Framework.Log
{
    public class SimpleFileLogger 
    {
        // 상수
        private int StorageDay = 10;        // 로그의 보관일수
        private static readonly string MessageSkip = "화면 출력 로그 생략...";

        // 필드 1
        private Timer _logTimer;
        private StringBuilder _logReporter;
        private int _logCounter = 0;

        // 필드 2
        private StreamWriter _streamWriter;
        private DateTime _lastWriteTime;
        private string _rootPath;
        private long _fileSizeLimit = 10 * 1024 * 1024;      // 파일을 나눌 바이트 단위
        internal string _fileName;

        // 생성자
        public SimpleFileLogger(string logPath, string baseFileName, double timerInterval, long fileSizeLimit, int storageDay)
        {
            _rootPath = logPath;
            _fileName = baseFileName;
            StorageDay = storageDay;

            if (fileSizeLimit > 0)
                _fileSizeLimit = fileSizeLimit;

            _lastWriteTime = DateTime.Now;

            CommonBaseCtor(timerInterval);
        }
        private void CommonBaseCtor(double interval)
        {
            _logReporter = new StringBuilder();
            _logTimer = new Timer(interval);
            _logTimer.Elapsed += delegate(object sender, ElapsedEventArgs e)
            {
                if (Logging == null)
                    return;

                lock (_logReporter)
                {
                    string result = _logReporter.ToString();
                    if (string.IsNullOrEmpty(result) == false)
                    {
                        _logReporter.Length = 0;
                        Logging(sender, new LoggingEventArgs { LogMessage = result, SignalTime = e.SignalTime });
                    }
                }
            };

            _logTimer.Start();
        }

        // 이벤트 [ ILogger ]
        public event Action<object, LoggingEventArgs> Logging;

        // 인터페이스 구현 [ ILogger ]
        public void AppendLine()
        {
            _logReporter.AppendLine();

            SetFileStream(DateTime.Now);
            _streamWriter.WriteLine(Environment.NewLine);
        }
        public void AppendLine(string value)
        {
           
            if (_logCounter > 20)
            {
                _logReporter.Length = 0;
                _logCounter = 0;
                _logReporter.AppendLine();
                _logReporter.AppendLine(MessageSkip);
            }

            _logReporter.AppendLine(value);
            _logCounter++;

            SetFileStream(DateTime.Now);
            _streamWriter.WriteLine(value );
        }
        public void Clear()
        {
            _logReporter.Length = 0;
            _logCounter = 0;
        }
        public string ForceGetLog()
        {
            lock (_logReporter)
            {
                string result = _logReporter.ToString();
                _logReporter.Length = 0;
                _logCounter = 0;
                return result;
            }
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

            TimeSpan ts = now.Date.AddDays(1).AddSeconds(-1) - _lastWriteTime.Date;

            if (ts.TotalDays > 1)
            {
                _streamWriter.Close();
                _streamWriter.Dispose();
                _streamWriter = CreateStreamFromDate(now);
                _lastWriteTime = now;
                return;
            }

            if (_streamWriter.BaseStream.Length > _fileSizeLimit)
            {
                _streamWriter.Close();
                _streamWriter.Dispose();
                _streamWriter = CreateStreamFromDate(now);
                _lastWriteTime = now;
                return;
            }
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
            string fileName = string.Format("{0}[{1:d2}].log", _fileName, count);

            StreamWriter fs = new StreamWriter(Path.Combine(dirPath, fileName), true, Encoding.Unicode);
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
