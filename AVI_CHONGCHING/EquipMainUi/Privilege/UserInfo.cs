using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Setting
{
    public enum EM_LV_LST
    {
        USER = 0,
        ENGINEER,
        SUPERVISOR,
    }
    public class UserInfo
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public EM_LV_LST Level { get; set; }

        public UserInfo(string id, string password, EM_LV_LST level = EM_LV_LST.USER)
        {
            Id = id;
            Password = password;
            Level = level;
        }
        public UserInfo(string id, string password, string name, EM_LV_LST level = EM_LV_LST.USER)
        {
            Id = id;
            Password = password;
            Name = name;
            Level = level;
        }
        public UserInfo(UserInfo user)
        {
            Id = user.Id;
            Password = user.Password;
            Level = user.Level;
        }
        public bool IsCorrectPW(string password)
        {
            return 0 == Password.CompareTo(password);
        }
    }
}
