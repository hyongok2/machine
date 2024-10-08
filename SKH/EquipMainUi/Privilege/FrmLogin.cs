using EquipMainUi.Struct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMainUi.Setting
{
    public partial class FrmLogin : Form
    {
        public enum FrmLoginType
        {
            Login,
            Create,
            Update,
            Remove,
            LevelChange,
        }

        private FrmLoginType _curType;
        private Equipment _equip = null;
        public FrmLogin(Equipment equip, FrmLoginType type = FrmLoginType.Login)
        {
            InitializeComponent();
            _equip = equip;
            _curType = type;
            this.Text = _curType.ToString();

            foreach (EM_LV_LST lv in Enum.GetValues(typeof(EM_LV_LST)))
                cboLevel.Items.Add(lv);

            lblNameTitle.Visible = false;
            txtName.Visible = false;
            cboLevel.Visible = false;
            lblPwTitle.Visible = false;
            txtPw.Visible = false;

            switch (_curType)
            {
                case FrmLoginType.Login:
                    lblPwTitle.Visible = true;
                    txtPw.Visible = true;
                    lblNotice.Text = "ID와 비밀번호를 입력하세요";
                    break;
                case FrmLoginType.Create:
                    lblNameTitle.Visible = true;
                    txtName.Visible = true;
                    lblPwTitle.Visible = true;
                    txtPw.Visible = true;
                    lblNotice.Text = "새로운 ID와 비밀번호를 입력하세요";
                    break;
                case FrmLoginType.Update:
                    lblPwTitle.Visible = true;
                    txtPw.Visible = true;
                    lblNotice.Text = "새 비밀번호를 입력하세요";
                    break;
                case FrmLoginType.Remove:
                    lblNotice.Text = "해당 ID를 지우시겠습니까?";
                    break;
                case FrmLoginType.LevelChange:
                    lblNotice.Text = "해당 ID의 등급 변경";
                    cboLevel.Visible = true;
                    break;
                default:
                    break;
            }
            btnDoSomething.Text = _curType.ToString();
            this.TopMost = true;
        }
        public void SetID(string id)
        {
            txtID.Text = id;
        }
        public void SetNotice(string msg)
        {
            lblNotice.Text = msg;
        }
        private void btnDoSomething_Click(object sender, EventArgs e)
        {
            Do();
        }

        private void Do()
        {
            switch (_curType)
            {
                case FrmLoginType.Login:
                    DoLogin();
                    break;
                case FrmLoginType.Create:
                    DoCreate();
                    break;
                case FrmLoginType.Update:
                    DoUpdate();
                    break;
                case FrmLoginType.Remove:
                    DoRemove();
                    break;
                case FrmLoginType.LevelChange:
                    DoLevelChange();
                    break;
                default:
                    break;
            }
        }

        private void DoLogin()
        {
            string id, password;
            GetLoginValue(out id, out password);
            if (LoginMgr.Instance.Login(_equip, id, password))
                this.Close();
            else
                MessageBox.Show("로그인 실패");
        }
        private void DoCreate()
        {
            string id, password, name;
            GetLoginValue(out id, out password, out name);

            if (LoginMgr.Instance.UserMgr.CreateUser(new UserInfo(id, password, name)))
            {
                LoginMgr.Instance.SaveInfo();
                this.Close();
            }
            else
                MessageBox.Show("생성 실패");
        }
        private void DoUpdate()
        {
            string id, password;
            GetLoginValue(out id, out password);

            if (LoginMgr.Instance.UserMgr.UpdateUser(id, new UserInfo(id, password)))
            {
                LoginMgr.Instance.SaveInfo();
                this.Close();
            }
            else
                MessageBox.Show("수정 실패");
        }
        private void DoRemove()
        {
            string id, password;
            GetLoginValue(out id, out password);
            if (LoginMgr.Instance.UserMgr.RemoveUser(id))
            {
                if (LoginMgr.Instance.LoginedUser.Id == id)
                    LoginMgr.Instance.Logout(_equip);
                LoginMgr.Instance.SaveInfo();
                this.Close();
            }
            else
                MessageBox.Show("삭제 실패");
        }
        private void DoLevelChange()
        {
            string id;
            EM_LV_LST level;
            GetLevelValue(out id, out level);
            if (level <= LoginMgr.Instance.LoginedUser.Level)
            {
                if (LoginMgr.Instance.UserMgr.UpdateLevel(id, level))
                {
                    LoginMgr.Instance.SaveInfo();
                    this.Close();
                }
                else
                    MessageBox.Show("ID가 존재하지않음");
            }
            else
                MessageBox.Show("등급 변경 실패, 현재 등급이 낮습니다");
        }
        private void GetLevelValue(out string id, out EM_LV_LST level)
        {
            id = string.Empty;
            level = EM_LV_LST.USER;

            level = (EM_LV_LST)cboLevel.SelectedIndex;
            id = txtID.Text;
        }
        private void GetLoginValue(out string id, out string pw)
        {
            id = string.Empty;
            pw = string.Empty;

            id = txtID.Text;
            pw = txtPw.Text;
        }
        private void GetLoginValue(out string id, out string pw, out string name)
        {
            id = string.Empty;
            pw = string.Empty;
            name = string.Empty;

            id = txtID.Text;
            pw = txtPw.Text;
            name = txtName.Text;
        }

        private void txtPw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                Do();
        }
    }
}
