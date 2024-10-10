using Dit.Framework.UI.UserComponent;
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
    public partial class FrmDoorSetting : Form
    {
        public FrmDoorSetting()
        {
            InitializeComponent();

            timerUI.Start();
        }

        private void timerUI_Tick(object sender, EventArgs e)
        {
            try
            {
                UpdateDoorState();
            }
            catch (Exception ex)
            {
                if (AlarmMgr.Instance.IsHappened(GG.Equip, EM_AL_LST.AL_0946_UI_EXCEPTION) == false)
                {
                    Logger.ExceptionLog.AppendLine(LogLevel.Error, string.Format("UI 갱신 예외 발생 : {0}", ex.Message));
                    Logger.ExceptionLog.AppendLine(LogLevel.Error, Log.EquipStatusDump.CallStackLog());
                    AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0946_UI_EXCEPTION);
                }
            }
        }

        private void btnDoorTop4_Click(object sender, EventArgs e)
        {
            if (GG.Equip.ModeSelectKey.IsAuto == true)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<AUTO MODE>\n(AUTO MODE 状态下，无法 Door Open.)" : "인터락<AUTO MODE>\n(AUTO MODE 상태에서는 Door Open이 불가능합니다.)");
                return;
            }
            if (GG.Equip.IsHomePositioning || GG.Equip.EquipRunMode == EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<动作中>\n(设备动作中. 无法变更设备设置.)" : "인터락<동작중>\n설비가 동작중입니다. 설비 설정을 변경 할 수 없습니다.");
                return;
            }
            CmdDoorOpenSol(GG.Equip.TopDoor01, btnDoorTop1, !GG.Equip.TopDoor01.IsSolOnOff);
        }

        private void btnDoorTop1_Click(object sender, EventArgs e)
        {
            if (GG.Equip.ModeSelectKey.IsAuto == true)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<AUTO MODE>\n(AUTO MODE 状态下，无法 Door Open.)" : "인터락<AUTO MODE>\n(AUTO MODE 상태에서는 Door Open이 불가능합니다.)");
                return;
            }
            if (GG.Equip.IsHomePositioning || GG.Equip.EquipRunMode == EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<动作中>\n(设备动作中. 无法变更设备设置.)" : "인터락<동작중>\n설비가 동작중입니다. 설비 설정을 변경 할 수 없습니다.");
                return;
            }
            CmdDoorOpenSol(GG.Equip.TopDoor03, btnDoorTop4, !GG.Equip.TopDoor03.IsSolOnOff);
        }

        private void btnDelayAllDoorOpen_Click(object sender, EventArgs e)
        {
            CmdAllDoorOpenSol(true);
        }

        private void btnDelayAllDoorClose_Click(object sender, EventArgs e)
        {
            CmdAllDoorOpenSol(false);
        }

        private void CmdDoorOpenSol(EquipMainUi.Struct.Switch selDoorSol, ButtonDelay2 selDoorBtn, bool vBit)
        {
            selDoorSol.OnOff(GG.Equip, vBit);
            DoorBtnStateChange(selDoorBtn, selDoorSol.YB_OnOff.vBit);
        }
        private void CmdAllDoorOpenSol(bool _vBit)
        {
            CmdDoorOpenSol(GG.Equip.TopDoor01, btnDoorTop1, _vBit);
            //CmdDoorOpenSol(GG.Equip.TopDoor02, btnDoorTop2, _vBit);
            //CmdDoorOpenSol(GG.Equip.TopDoor03, btnDoorTop3, _vBit);
            CmdDoorOpenSol(GG.Equip.TopDoor04, btnDoorTop4, _vBit);
        }

        private void UpdateDoorState()
        {
            bool[] isOpenSol = {
                                   GG.Equip.TopDoor01.YB_OnOff.vBit,
                                   GG.Equip.TopDoor02.YB_OnOff.vBit,
                                   GG.Equip.TopDoor03.YB_OnOff.vBit,
                                   GG.Equip.TopDoor04.YB_OnOff.vBit,
                               };

            DoorBtnStateChange(btnDoorTop1, GG.Equip.TopDoor01.IsSolOnOff, GG.Equip.TopDoor01.IsOnOff);
            DoorBtnStateChange(btnDoorTop2, GG.Equip.TopDoor02.IsSolOnOff, GG.Equip.TopDoor02.IsOnOff);
            DoorBtnStateChange(btnDoorTop3, GG.Equip.TopDoor03.IsSolOnOff, GG.Equip.TopDoor03.IsOnOff);
            DoorBtnStateChange(btnDoorTop4, GG.Equip.TopDoor04.IsSolOnOff, GG.Equip.TopDoor04.IsOnOff);

            bool? isAllDoorOpenSol = GG.Equip.TopDoor01.YB_OnOff.vBit;

            for (int iPos = 0; iPos < isOpenSol.Length; iPos++)
            {
                if (isAllDoorOpenSol != isOpenSol[iPos])
                {
                    isAllDoorOpenSol = null;
                    break;
                }
            }

            if (isAllDoorOpenSol == true)
            {
                btnDelayAllDoorOpen.BackColor = Color.Red;
                btnDelayAllDoorClose.BackColor = Color.Transparent;
            }
            else if (isAllDoorOpenSol == false)
            {
                btnDelayAllDoorOpen.BackColor = Color.Transparent;
                btnDelayAllDoorClose.BackColor = Color.Red;
            }
            else
            {
                btnDelayAllDoorOpen.BackColor = Color.Transparent;
                btnDelayAllDoorClose.BackColor = Color.Transparent;
            }

            //Auto Teach
            if (GG.Equip.ModeSelectKey.IsAuto == true)
            {
                lblAutoTeachState.Text = "Auto Mode";
                lblAutoTeachState.BackColor = Color.Red;
            }
            else
            {
                lblAutoTeachState.Text = "Teach Mode";
                lblAutoTeachState.BackColor = Color.White;
            }
        }
        private void DoorBtnStateChange(ButtonDelay2 btnDelay, bool isOpenCmd, bool? isOpenState = null)
        {
            if (isOpenCmd == true)
            {
                btnDelay.BackColor = Color.Red;
                btnDelay.ForeColor = Color.Black;
            }
            else
            {
                btnDelay.BackColor = Color.Transparent;
                if (isOpenState == true) btnDelay.ForeColor = Color.Red;
            }

            if (isOpenState == null) return;

            if (isOpenState == true)
            {
                btnDelay.Text = "Open";
            }
            else
            {
                btnDelay.ForeColor = Color.Black;
                btnDelay.Text = "Close";
            }
        }
    }
}
