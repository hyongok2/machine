using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IWshRuntimeLibrary;

namespace Dit.Framework.Util
{
    public static class UserShortCut
    {
        // 필드
        private static WshShellClass _wshShell;

        // 메서드
        public static void Create(string srcPath, Environment.SpecialFolder destFolder, string destSubFolder, string name)
        {
            string destPath = Path.Combine(Environment.GetFolderPath(destFolder), destSubFolder);
            destPath = Path.Combine(destPath, name);

            Create(srcPath, destPath);
        }
        public static void Create(string srcPath, Environment.SpecialFolder destFolder)
        {
            string destPath = Path.Combine(Environment.GetFolderPath(destFolder), Path.GetFileName(srcPath));

            Create(srcPath, destPath);
        }
        public static void Create(string srcPath, string destPath)
        {
            string destLink = Path.ChangeExtension(destPath, ".lnk");

            // 파일이 존재하면 삭제하도록 수정 [110713]
            try
            {
                if (System.IO.File.Exists(destLink) == true)
                    System.IO.File.Delete(destLink);
            }
            catch (Exception)
            {
            }

            _wshShell = new WshShellClass();
            IWshRuntimeLibrary.IWshShortcut shortCut;

            shortCut = (IWshRuntimeLibrary.IWshShortcut)_wshShell.CreateShortcut(destLink);
            shortCut.TargetPath = srcPath;
            shortCut.Description = "";
            shortCut.IconLocation = string.Format("{0}, 0", srcPath);

            shortCut.Save();
        }
    }

}
