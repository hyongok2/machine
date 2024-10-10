using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EquipMainUi.Struct.Detail.EFEM;
using EquipMainUi.Struct;

namespace EquipMainUi.UserControls
{
    public partial class ucrlRobotEasyController : UserControl
    {
        public ucrlRobotEasyController()
        {
            InitializeComponent();
        }

        public void UIUpdate()
        {
            btnTransfer.Flicker = rbTransfer.Checked ? GG.Equip.WaferTransLogic.IsRunning() : GG.Equip.Efem.Proxy.HS[EmEfemPort.ROBOT].LstCmd[EmEfemCommand.WAITR].IsStepRunning;
            lblPick.Flicker = GG.Equip.WaferTransLogic.IsPickRunning;
            lblPlace.Flicker = GG.Equip.WaferTransLogic.IsPlaceRunning;
        }

        private void from_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb.Checked == true)
            {
                if (rb == rbFromAligner
                    || rb == rbFromAVI
                    || rb == rbFromUpper
                    || rb == rbFromLower
                    )
                {
                    tbFromSlot.Text = "1";
                    tbFromSlot.Enabled = false;
                }
                else if (rb == rbFromLoad1
                    || rb == rbFromLoad2)
                {
                    tbFromSlot.Text = "";
                    tbFromSlot.Enabled = true;
                }

                if (rb == rbFromUpper
                    || rb == rbFromLower)
                {
                    rbToAligner.Visible
                        = rbToAVI.Visible
                        = rbToUpper.Visible
                        = rbToLower.Visible
                        = rbToLoad1.Visible
                        = rbToLoad2.Visible
                        = true;

                    rbToUpper.Visible
                        = rbToLower.Visible
                        = false;
                }
                else
                {
                    rbToAligner.Visible
                        = rbToAVI.Visible
                        = rbToUpper.Visible
                        = rbToLower.Visible
                        = rbToLoad1.Visible
                        = rbToLoad2.Visible
                        = true;

                    if (rb == rbFromAligner) rbToAligner.Visible = false;
                    if (rb == rbFromAVI) rbToAVI.Visible = false;
                    if (rb == rbFromLoad1)
                    {
                        rbToLoad1.Visible = false;
                        rbToLoad2.Visible = false;
                    }
                    if (rb == rbFromLoad2)
                    {
                        rbToLoad1.Visible = false;
                        rbToLoad2.Visible = false;
                    }

                    rbToUpper.Visible
                        = rbToLower.Visible
                        = true;
                }
                gbTo.Enabled = true;
            }
        }

        private void to_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb.Checked == true)
            {
                if (rb == rbToAligner
                    || rb == rbToAVI
                    || rb == rbToUpper
                    || rb == rbToLower
                    )
                {
                    tbToSlot.Text = "1";
                    tbToSlot.Enabled = false;
                }
                else if (rb == rbToLoad1
                    || rb == rbToLoad2)
                {
                    string fromPortName = string.Empty;
                    string fromPortCstID = string.Empty;
                    int lpmSlot = 0;
                    string lpmCstID = rbToLoad1.Checked ? GG.Equip.Efem.LoadPort1.CstKey.ID : GG.Equip.Efem.LoadPort2.CstKey.ID;
                    if (rbFromAligner.Checked)
                    {
                        fromPortName = "ALIGNER";
                        fromPortCstID = GG.Equip.Efem.Aligner.LowerWaferKey.CstID;
                        lpmSlot = GG.Equip.Efem.Aligner.LowerWaferKey.SlotNo;
                    }
                    else if (rbFromAVI.Checked)
                    {
                        fromPortName = "AVI";
                        fromPortCstID = GG.Equip.TransferUnit.LowerWaferKey.CstID;
                        lpmSlot = GG.Equip.TransferUnit.LowerWaferKey.SlotNo;
                    }
                    else if (rbFromUpper.Checked)
                    {
                        fromPortName = "UPPER";
                        fromPortCstID = GG.Equip.Efem.Robot.UpperWaferKey.CstID;
                        lpmSlot = GG.Equip.Efem.Robot.UpperWaferKey.SlotNo;
                    }
                    else if (rbFromLower.Checked)
                    {
                        fromPortName = "LOWER";
                        fromPortCstID = GG.Equip.Efem.Robot.LowerWaferKey.CstID;
                        lpmSlot = GG.Equip.Efem.Robot.LowerWaferKey.SlotNo;
                    }

                    if (fromPortName != string.Empty)
                    {
                        if (fromPortCstID != lpmCstID)
                        {
                            tbToSlot.Text = "";
                            InterLockMgr.AddInterLock(GG.boChinaLanguage ? "物流情报不同请注意." : "물류정보가 다르니 주의하세요", "{2} : {0}\n{3} : {1}",
                                fromPortCstID, lpmCstID, fromPortName, rbToLoad1.Checked ? "LoadPort1" : "LoadPort2");
                        }
                        else
                        {
                            tbToSlot.Text = lpmSlot.ToString();
                        }
                    }
                    else
                    {
                        tbToSlot.Text = "";
                    }
                    tbToSlot.Enabled = true;
                }
            }
        }

        private void btnRobotStop_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.ROBOT, EmEfemCommand.STOP_);
            GG.Equip.WaferTransLogic.SetRunMode(Struct.Step.EmEfemRunMode.Stop);
            ResetRobotSelection();
        }

        private void btnWaiting_Click(object sender, EventArgs e)
        {
            if (rbFromUpper.Checked == false && rbFromLower.Checked == false)
                return;
            if (rbToAligner.Checked == false && rbToAVI.Checked == false && rbToLoad1.Checked == false && rbToLoad2.Checked == false)
                return;
            if (GG.Equip.WaferTransLogic.IsRunning())
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Wafer 移送中" : "Wafer 이송 중입니다");
                return;
            }

            EmEfemRobotArm robotArm = rbFromUpper.Checked == true ? EmEfemRobotArm.Upper : EmEfemRobotArm.Lower;
            int slot;
            if (int.TryParse(tbToSlot.Text, out slot) == false
                || slot < 1 || slot > 13)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Slot 1~13 为止可以" : "Slot 1~13 까지 가능");
                tbToSlot.Text = "";
                return;
            }
            EmEfemDBPort dbPort = GetPort(false);
            EmEfemPort port = GG.Equip.WaferTransLogic.DBtoPort(dbPort);
            if (port == EmEfemPort.NONE || port == EmEfemPort.ROBOT)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "请重新进行Port 设置" : "Port설정을 다시하세요");
                return;
            }

            GG.Equip.Efem.Proxy.StartWaitRobot(GG.Equip, new EFEMWAITRDataSet(robotArm, port, slot));
            ResetRobotSelection();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (rbWait.Checked)
            {
                btnWaiting_Click(sender, e);
                return;
            }        
            if (rbFromUpper.Checked == false && rbFromLower.Checked == false
                && rbFromAligner.Checked == false && rbFromAVI.Checked == false
                && rbFromLoad1.Checked == false && rbFromLoad2.Checked == false)
                return;
            if (tbFromSlot.Text == null || tbFromSlot.Text == "0")
                return;

            if (rbToUpper.Checked == false && rbToLower.Checked == false
                && rbToAligner.Checked == false && rbToAVI.Checked == false
                && rbToLoad1.Checked == false && rbToLoad2.Checked == false)
                return;
            if (tbFromSlot.Text == null || tbFromSlot.Text == "0")
                return;

            int fromSlot, toSlot;
            if (int.TryParse(tbFromSlot.Text, out fromSlot) == false
                || fromSlot < 1 || fromSlot > 13
                || int.TryParse(tbToSlot.Text, out toSlot) == false
                || toSlot < 1 || toSlot > 13)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Slot 1~13 为止可以" : "Slot 1~13 까지 가능");
                return;
            }
            if(GG.Equip.Efem.LoadPort1.Status.IsError || GG.Equip.Efem.LoadPort2.Status.IsError || GG.Equip.Efem.Robot.Status.IsAlarm)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Robot, LPM1 或者 LPM2是 Error 状态时, 禁止TRANS 命令.\nROBOT LPM1 LPM2 中 RESET Button点灯了的话，请按以下， Reset 后, 进行或者请使用 BASE OPERATORATION的 Wafer恢复功能 ." : "로봇, 로드포트1 또는 로드포트2가 에러 상태일 때 TRANS 명령이 금지 됩니다\nROBOT LPM1 LPM2 중 RESET 버튼이 점등되어 있다면 눌러서 리셋 후 진행 하거나 BASE OPERATORATION의 웨이퍼 복귀 기능을 사용해주세요");
            }
            EmEfemDBPort fromPort = GetPort(true);
            EmEfemDBPort toPort = GetPort(false);

            GG.Equip.WaferTransLogic.StartWaferTransfer(fromPort, fromSlot, toPort, toSlot);
            ResetRobotSelection();
        }
        private void ResetRobotSelection()
        {
            rbFromUpper.Checked = rbFromLower.Checked = false;            
            rbFromAligner.Checked = rbFromAVI.Checked = rbFromLoad1.Checked = rbFromLoad2.Checked = false;            

            rbToUpper.Checked = rbToLower.Checked = false;
            rbToAligner.Checked = rbToAVI.Checked = rbToLoad1.Checked = rbToLoad2.Checked = false;

            gbTo.Enabled = false;
        }
        private void ResetToButtonVisible()
        {
            rbToAligner.Visible
              = rbToAVI.Visible
              = rbToUpper.Visible
              = rbToLower.Visible
              = rbToLoad1.Visible
              = rbToLoad2.Visible
              = true;

            rbToUpper.Visible
                = rbToLower.Visible
                = true;
        }
        private EmEfemDBPort GetPort(bool isFrom)
        {
            if (isFrom)
            {
                if (rbFromAligner.Checked == true)
                    return EmEfemDBPort.ALIGNER;
                if (rbFromAVI.Checked == true)
                    return EmEfemDBPort.EQUIPMENT;
                if (rbFromLoad1.Checked == true)
                    return EmEfemDBPort.LOADPORT1;
                if (rbFromLoad2.Checked == true)
                    return EmEfemDBPort.LOADPORT2;
                if (rbFromUpper.Checked == true)
                    return EmEfemDBPort.UPPERROBOT;
                if (rbFromLower.Checked == true)
                    return EmEfemDBPort.LOWERROBOT;

                return EmEfemDBPort.NONE;
            }
            else
            {
                if (rbToAligner.Checked == true)
                    return EmEfemDBPort.ALIGNER;
                if (rbToAVI.Checked == true)
                    return EmEfemDBPort.EQUIPMENT;
                if (rbToLoad1.Checked == true)
                    return EmEfemDBPort.LOADPORT1;
                if (rbToLoad2.Checked == true)
                    return EmEfemDBPort.LOADPORT2;
                if (rbToUpper.Checked == true)
                    return EmEfemDBPort.UPPERROBOT;
                if (rbToLower.Checked == true)
                    return EmEfemDBPort.LOWERROBOT;

                return EmEfemDBPort.NONE;
            }
        }

        private void btnInvisible_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void rbTransfer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTransfer.Checked)
            {
                rbFromAligner.Visible = rbFromAVI.Visible = rbFromLoad1.Visible = rbFromLoad2.Visible = tbFromSlot.Visible = label25.Visible = true;
                rbToUpper.Visible = rbToLower.Visible = true;
                btnTransfer.Text = "TRANSFER";
            }
            else
            {
                rbFromAligner.Visible = rbFromAVI.Visible = rbFromLoad1.Visible = rbFromLoad2.Visible = tbFromSlot.Visible = label25.Visible = false;
                rbToUpper.Visible = rbToLower.Visible = false;
                btnTransfer.Text = "WAIT";
            }            
            ResetRobotSelection();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            rbTransfer.Checked = true;
            ResetRobotSelection();
            ResetToButtonVisible();
        }
    }
}
