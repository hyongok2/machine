using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace EquipMainUi.ConvenienceClass
{
    public class FileCopier
    {
        protected class CopyFileInfo
        {
            public CopyFileInfo(string fileName, string sourceDir, string destDir)
            {
                SourceDir = sourceDir;
                DestDir = destDir;
                FileName = fileName;
            }
            private string SourceDir { get; set; }
            private string DestDir { get; set; }
            public string FileName { get; set; }
            public string SourcePath
            {
                get
                {
                    return Path.Combine(SourceDir, FileName);
                }
            }
            public string DestPath
            {
                get
                {
                    return Path.Combine(DestDir, FileName);
                }
            }
        }

        private List<CopyFileInfo> _fileList = new List<CopyFileInfo>();
        public int NumCopiedFile {get;set;}
        public string result;

        public void Add(string fileName, string sourceDir, string destDir)
        {
            _fileList.Add(new CopyFileInfo(fileName, sourceDir, destDir));
        }

        public bool StartCopy()
        {
            NumCopiedFile = 0;
            result = "Dll Copy\n";
            string result1file = string.Empty;

            foreach (CopyFileInfo fileInfo in _fileList)
            {
                try
                {
                    result1file = fileInfo.FileName;
                    if (File.Exists(fileInfo.SourcePath))
                    {
                        File.Copy(fileInfo.SourcePath, fileInfo.DestPath, true);

                        result1file += string.Format(" 복사됨\n");
                        NumCopiedFile++;
                    }
                    else
                        result1file += string.Format(" 파일없음\n");
                }
                catch (Exception ex)
                {
                    if (File.Exists(fileInfo.DestPath))
                    {
                        result1file += string.Format(" 패스\n");
                        NumCopiedFile++;
                    }
                    else
                        result1file += string.Format(" 예외발생\n");
                }

                result += result1file;
            }
            result += "-------------";
            return _fileList.Count == NumCopiedFile;
        }
    }
}
