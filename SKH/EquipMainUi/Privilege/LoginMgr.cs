using EquipMainUi.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Setting
{
    public class LoginMgr
    {
        private UserInfo _loginedUser;
        public bool IsUserChanged { get; set; }
        public UserManager UserMgr { get; set; }

        private static LoginMgr _selfInstance = null;
        public static LoginMgr Instance
        {
            get
            {
                if (_selfInstance == null)
                    _selfInstance = new LoginMgr();
                return _selfInstance;
            }
        }
        private LoginMgr()
        {
            IsUserChanged = true;
            UserMgr = new UserManager();
            _loginedUser = new UserInfo("0", "0", EM_LV_LST.USER);
            LoadInfo();
        }
        public UserInfo LoginedUser
        {
            get
            {
                return new UserInfo(_loginedUser == null ?
                    new UserInfo("0", "0", EM_LV_LST.USER) : _loginedUser);
            }
        }
        public bool IsLogined()
        {
            return (_loginedUser == null || _loginedUser.Id == "0") ?
                false : true;
        }
        public bool IsLoginedUser(string id)
        {
            return id == _loginedUser.Id;
        }
        public bool Login(Equipment equip, int id, string password)
        {
            return Login(equip, id.ToString(), password);
        }
        public bool Login(Equipment equip, string id, string password)
        {
            if (UserMgr.IsAdmin(id) && UserMgr.Admin.IsCorrectPW(password))
            {
                IsUserChanged = true;

                _loginedUser = UserMgr.Admin;

                //if (equip.HsmsPc.StartCommand(equip, EmHsmsPcCommand.OPERATOR_LOGIN_REPORT, id) == false) return false;

                Logger.Log.AppendLine(LogLevel.NoLog,
                    "=================================[ID] Admin LogIn=================================");

                return true;
            }

            UserInfo user = UserMgr.FindUser(id);
            if (null != user && user.IsCorrectPW(password))
            {
                IsUserChanged = true;

                //if (equip.HsmsPc.StartCommand(equip, EmHsmsPcCommand.OPERATOR_LOGIN_REPORT, id) == false) return false;

                _loginedUser = user;
                Logger.Log.AppendLine(LogLevel.NoLog,
                    "=================================[ID] {0} Login=================================", _loginedUser.Id);
                return true;
            }

            Logger.Log.AppendLine(LogLevel.NoLog,
                    "=============================[ID] {0} Fail to trying login=======================", id);
            return false;
        }
        public bool Logout(Equipment equip)
        {
            if (GG.PrivilegeTestMode) return true;

            IsUserChanged = true;

            //if (equip.HsmsPc.StartCommand(equip, EmHsmsPcCommand.OPERATOR_LOGIN_REPORT, _loginedUser.Id) == false) return false;

            Logger.Log.AppendLine(LogLevel.NoLog,
                    "=================================[ID] {0} Logout=================================", _loginedUser.Id);
            _loginedUser = new UserInfo(new UserInfo("0", "0", EM_LV_LST.USER));



            return true;
        }
        public bool IsCorrectInfo(string id, string password)
        {
            UserInfo user = UserMgr.FindUser(id);
            if (user == null)
                return false;
            else
            {
                return user.IsCorrectPW(password);
            }
        }
        public bool LoadInfo()
        {
            return UserMgr.LoadInfo();
        }

        public bool SaveInfo()
        {
            return UserMgr.SaveInfo();
        }
        public bool LevelCheck(EM_LV_LST level)
        {
            if ((int)_loginedUser.Level >= (int)level)
            {
                return true;
            }
            InterLockMgr.AddInterLock(string.Format("Interlock <Permission Level> \n (Only available for level {0} or higher. Confirmation of authority level.)", level.ToString()));

            return false;
        }
    }
}
