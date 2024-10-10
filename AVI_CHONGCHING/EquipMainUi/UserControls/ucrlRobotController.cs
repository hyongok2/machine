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
    public partial class ucrlRobotController : UserControl
    {
        public ucrlRobotController()
        {
            InitializeComponent();
        }

        public void UIUpdate()
        {
            btnTransfer.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.ROBOT].LstCmd[EmEfemCommand.TRANS].IsStepRunning;
            btnWaiting.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.ROBOT].LstCmd[EmEfemCommand.WAITR].IsStepRunning;
        }

        private void rbAligner_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb.Checked == true)
            {
                if (rb == rbAligner)
                {
                    tbSlot.Text = "1";
                    tbSlot.Enabled = false;
                }
                else if (rb == rbAVI)
                {
                    tbSlot.Text = "1";
                    tbSlot.Enabled = false;
                }
                else if (rb == rbLoad1
                    || rb == rbLoad2)
                {
                    tbSlot.Text = "";
                    tbSlot.Enabled = true;
                }
            }
        }

        private void btnRobotStop_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.ROBOT, EmEfemCommand.STOP_);
            ResetRobotSelection();
        }

        private void btnWaiting_Click(object sender, EventArgs e)
        {
            if (rbUpper.Checked == false && rbLower.Checked == false)
                return;
            if (rbPick.Checked == false && rbPlace.Checked == false)
                return;
            if (rbAligner.Checked == false && rbAVI.Checked == false && rbLoad1.Checked == false && rbLoad2.Checked == false)
                return;

            EmEfemRobotArm robotArm = rbUpper.Checked == true ? EmEfemRobotArm.Upper : EmEfemRobotArm.Lower;
            int slot = int.Parse(tbSlot.Text);
            EmEfemPort port = GetPort();

            GG.Equip.Efem.Proxy.StartWaitRobot(GG.Equip, new EFEMWAITRDataSet(robotArm, port, slot));
            ResetRobotSelection();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (rbUpper.Checked == false && rbLower.Checked == false)
                return;
            if (rbPick.Checked == false && rbPlace.Checked == false)
                return;
            if (rbAligner.Checked == false && rbAVI.Checked == false && rbLoad1.Checked == false && rbLoad2.Checked == false)
                return;
            if (tbSlot.Text == null || tbSlot.Text == "0")
                return;

            EmEfemRobotArm robotArm = rbUpper.Checked == true ? EmEfemRobotArm.Upper : EmEfemRobotArm.Lower;
            int slot;
            if (int.TryParse(tbSlot.Text, out slot) == false
                || slot < 1 || slot > 13)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Slot 1~13 为止可以" : "Slot 1~13 까지 가능");
                tbSlot.Text = "";
                return;
            }
            EmEfemPort port = GetPort();
            EmEfemTransfer transCommand = rbPick.Checked == true ? EmEfemTransfer.PICK : EmEfemTransfer.PLACE;

            GG.Equip.Efem.Proxy.StartTransRobot(GG.Equip, new EFEMTRANSDataSet(robotArm, transCommand, port, slot));
            ResetRobotSelection();
        }
        private void ResetRobotSelection()
        {
            rbUpper.Checked = rbLower.Checked = false;
            rbPick.Checked = rbPlace.Checked = false;
            rbAligner.Checked = rbAVI.Checked = rbLoad1.Checked = rbLoad2.Checked = false;
        }
        private EmEfemPort GetPort()
        {
            if (rbAligner.Checked == false && rbAVI.Checked == false && rbLoad1.Checked == false && rbLoad2.Checked == false)
                return EmEfemPort.NONE;

            if (rbAligner.Checked == true)
                return EmEfemPort.ALIGNER;
            if (rbAVI.Checked == true)
                return EmEfemPort.EQUIPMENT;
            if (rbLoad1.Checked == true)
                return EmEfemPort.LOADPORT1;
            if (rbLoad2.Checked == true)
                return EmEfemPort.LOADPORT2;

            return EmEfemPort.NONE;
        }

        private void btnInvisible_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
