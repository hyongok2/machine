using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace Dit.Framework.Ini
{
    /// <summary>
    /// AutoBackup 기능 추가
    /// date 170824
    /// </summary>
    public class BaseSetting
    {
        /// <summary>
        /// 변경되기전 파일을 백업합니다.
        /// </summary>
        [Browsable(false)]
        public bool UseAutoBackup { get; set; }
        [Browsable(false)]
        public string BackupFolderName { get; set; }
        [Browsable(false)]
        public int BackupCount { get; set; }

        public BaseSetting()
        {
            BackupFolderName = "AutoBackup";
            UseAutoBackup = false;
            BackupCount = 10;
        }

        public void SetAutoBackupFolder(string folderName)
        {
            BackupFolderName = folderName;
            UseAutoBackup = true;
        }

        public virtual bool Save(string path)
        {
            try
            {
                string str = Path.GetFullPath(path);

                if (UseAutoBackup)
                {
                    str = Path.Combine(Path.GetDirectoryName(path), BackupFolderName,
                        string.Format("{0}({1}_Saved){2}", Path.GetFileNameWithoutExtension(path), DateTime.Now.ToString("yyyyMMdd_HH-mm-ss"), Path.GetExtension(path)));
                    if (Directory.Exists(Path.GetDirectoryName(str)) == false)
                        Directory.CreateDirectory(Path.GetDirectoryName(str));
                    if (File.Exists(path) == true)
                        File.Copy(path, str);
                    BaseSetting.RemoveOldBackup(str, Path.GetFileNameWithoutExtension(path), BackupCount);
                }

                IniFileManager.SaveIni(path, this);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public virtual bool Load(string path)
        {
            try
            {
                IniFileManager.LoadIni(path, this);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static void RemoveOldBackup(string rootPath, string fileName, int backupCount)
        {
            string dir = Path.GetDirectoryName(rootPath);
            string[] sameSetting = Directory.GetFiles(dir, fileName + "*");

            int deleteCount = sameSetting.Length - backupCount;
            if (deleteCount > 0)
            {
                for (int iter = 0; iter < deleteCount; ++iter)
                {
                    try
                    {
                        File.Delete(sameSetting[iter]);
                    }
                    catch
                    {
                    }
                }
            }
        }

    }
}
