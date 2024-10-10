using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMainUi.Setting
{
    public class UserManager
    {
        private const string AdminID = "ditctrl";
        private const string AdminPW = "ditctrl";

        private UserInfo admin;
        public UserInfo Admin
        {
            get { return new UserInfo(admin); }
        }
        public static string DefaultPath = Path.Combine(GG.StartupPath, "Setting", "UserInfo.dat");
        private Dictionary<string, UserInfo> _users = new Dictionary<string, UserInfo>();

        public UserManager()
        {
            admin = new UserInfo(AdminID, AdminPW, EM_LV_LST.SUPERVISOR);
        }
        public Dictionary<string, UserInfo> UserList()
        {
            return _users;
        }

        public bool Exist(string id)
        {
            return _users.ContainsKey(id);
        }
        public bool CreateUser(UserInfo user)
        {
            if (Exist(user.Id) || IsAdmin(user.Id) ||
                IsFollowIDCreationRule(user.Id) == false ||
                IsFollowPWCreationRule(user.Password) == false)
                return false;
            else
            {
                _users.Add(user.Id, user);
            }
            return true;
        }
        public bool UpdateUser(string id, UserInfo newUser)
        {
            if (!Exist(id) ||
                IsFollowIDCreationRule(newUser.Id) == false ||
                IsFollowPWCreationRule(newUser.Password) == false)
                return false;
            else
            {
                _users[id] = newUser;
            }
            return true;
        }
        public bool UpdateLevel(string id, EM_LV_LST level)
        {
            if (!Exist(id))
            {
                return false;
            }
            else
            {
                _users[id].Level = level;
            }
            return true;
        }
        public bool RemoveUser(string id)
        {
            if (!Exist(id))
                return false;
            else
            {
                _users.Remove(id);
            }
            return true;
        }

        private bool IsFollowIDCreationRule(string id)
        {
            return true;
        }
        private bool IsFollowPWCreationRule(string pw)
        {
            return true;
        }
        public UserInfo FindUser(string id)
        {
            if (Exist(id))
                return _users[id];
            else
                return null;
        }

        public bool LoadInfo()
        {
            if (!File.Exists(DefaultPath))
            {
                File.Create(DefaultPath);
                FileInfo newFile = new FileInfo(DefaultPath);
                newFile.Attributes = FileAttributes.Hidden;
                return false;
            }
            StreamReader sr = new StreamReader(DefaultPath);

            string[] splited;
            string id;
            string pw;
            int level;
            string name;

            _users.Clear();
            while (!sr.EndOfStream)
            {
                splited = sr.ReadLine().Split(',');

                if (splited.Length != 4)
                    continue;

                id = splited[0];
                pw = splited[1];
                level = int.Parse(splited[2]);
                name = splited[3];

                CreateUser(new UserInfo(id, pw, name, (EM_LV_LST)level));
            }
            sr.Close();
            return true;
        }
        public bool SaveInfo()
        {
            StreamWriter sw;
            if (!File.Exists(DefaultPath))
                sw = new StreamWriter(File.Create(DefaultPath));
            else
            {
                // 파일권한 풀고
                FileInfo info = new FileInfo(DefaultPath);
                info.Attributes = FileAttributes.Normal;
                info.IsReadOnly = false;

                sw = new StreamWriter(File.Open(DefaultPath, FileMode.Create));

                // 파일권한 다시 설정
                info.Attributes = FileAttributes.Hidden;
                info.IsReadOnly = true;
            }

            foreach (KeyValuePair<string, UserInfo> user in _users)
            {
                sw.WriteLine(string.Format("{0},{1},{2},{3}",
                    user.Value.Id, user.Value.Password, (int)user.Value.Level, user.Value.Name));
            }
            sw.Close();
            return true;
        }

        public bool IsAdmin(string id)
        {
            return Admin.Id == id;
        }
    }
}
