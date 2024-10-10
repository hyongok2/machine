using Dit.Framework.PLC;
using EquipMainUi.Struct;
using EquipMainUi.Struct.Detail;
using EquipMainUi.Struct.Detail.HSMS;
using EquipMainUi.Struct.Detail.HSMS.ReportStruct;
using EquipMainUi.Test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EquipMainUi
{
    public partial class FrmTester : Form
    {
        private Equipment Equip;

        public FrmTester(Equipment equip)
        {
            InitializeComponent();

            Equip = equip;

            foreach (string str in Enum.GetNames(typeof(EmInspPcCommand)))
                cboInspCmd.Items.Add(str);

            
            FrmEfemTest f;

            f = new FrmEfemTest(Equip);
            f.FormBorderStyle = FormBorderStyle.None;
            f.Name = "FrmEfemTest";
            f.TopLevel = false;
            tabpEFEM.Controls.Add(f);
            f.Parent = tabpEFEM;
            f.Text = string.Empty;
            f.ControlBox = false;
            f.Show();

            FrmDBTest f2;
            f2 = new FrmDBTest();
            f2.FormBorderStyle = FormBorderStyle.None;
            f2.Name = "FrmDBTest";
            f2.TopLevel = false;
            tabpDB.Controls.Add(f2);
            f2.Parent = tabpDB;
            f2.Text = string.Empty;
            f2.ControlBox = false;
            f2.Show();

            FrmRFIDTest f3;
            f3 = new FrmRFIDTest(GG.Equip.Efem.LoadPort1.RFR);
            f3.FormBorderStyle = FormBorderStyle.None;
            f3.Name = "FrmRFIDTest1";
            f3.TopLevel = false;
            tabpRFID.Controls.Add(f3);
            f3.Parent = tabpRFID;
            f3.Text = string.Empty;
            f3.ControlBox = false;
            f3.Show();

            FrmRFIDTest f33;
            f33 = new FrmRFIDTest(GG.Equip.Efem.LoadPort2.RFR);
            f33.FormBorderStyle = FormBorderStyle.None;
            f33.Name = "FrmRFIDTest2";
            f33.TopLevel = false;
            tabpRFID.Controls.Add(f3);
            f33.Parent = tabpRFID;
            f33.Text = string.Empty;
            f33.ControlBox = false;
            f33.Location = new Point(f3.Width + 10, 0);
            f33.Show();

            FrmOCRTest f4;
            f4 = new FrmOCRTest(GG.Equip.OCR, GG.Equip.InitSetting.OcrIP, GG.Equip.InitSetting.OcrPort);
            f4.FormBorderStyle = FormBorderStyle.None;
            f4.Name = "FrmOCRTest";
            f4.TopLevel = false;
            tabPageOCR.Controls.Add(f4);
            f4.Parent = tabPageOCR;
            f4.Text = string.Empty;
            f4.ControlBox = false;
            f4.Show();

            FrmBCRTest f5, f6;
            f5 = new FrmBCRTest(GG.Equip.BCR1);
            f5.FormBorderStyle = FormBorderStyle.None;
            f5.Name = "FrmBCRTest1";
            f5.TopLevel = false;
            tabPageBCR.Controls.Add(f5);
            f5.Parent = tabPageBCR;
            f5.Text = string.Empty;
            f5.ControlBox = false;
            f5.Show();

            f6 = new FrmBCRTest(GG.Equip.BCR2);
            f6.FormBorderStyle = FormBorderStyle.None;
            f6.Name = "FrmBCRTest2";
            f6.TopLevel = false;
            tabPageBCR.Controls.Add(f6);
            f6.Parent = tabPageBCR;
            f6.Location = new Point(f5.Width + 10, 0);
            f6.Text = string.Empty;
            f6.ControlBox = false;
            f6.Show();

            if(GG.IsDitPreAligner)
            {
                FrmEziStepMotorTester f7;
                f7 = new FrmEziStepMotorTester();
                f7.FormBorderStyle = FormBorderStyle.None;
                f7.Name = "FrmEziStepMotorTester";
                f7.TopLevel = false;
                tabPEziMotor.Controls.Add(f7);
                f7.Parent = tabPEziMotor;
                f7.Text = string.Empty;
                f7.ControlBox = false;
                f7.Show();
            }

            timer1.Start();
        }

        private void btnInspCommand_Click(object sender, EventArgs e)
        {
            if (cboInspCmd.SelectedIndex < 0) return;
            Equip.InspPc.StartCommand(Equip, (EmInspPcCommand)cboInspCmd.SelectedIndex, 0);
        }

        private void btnOHTLoadLpm1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if(btn == btnOHTLoadLpm1)
            {
                GG.Equip.PioLPM1.OHTStart(Struct.Detail.OHT.EmOHTtype.LOAD);
            }
            else if (btn == btnOHTLoadLpm2)
            {
                GG.Equip.PioLPM2.OHTStart(Struct.Detail.OHT.EmOHTtype.LOAD);
            }
            else if (btn == btnOHTULoadLpm1)
            {
                GG.Equip.PioLPM1.OHTStart(Struct.Detail.OHT.EmOHTtype.UNLOAD);
            }
            else if (btn == btnOHTULoadLpm2)
            {
                GG.Equip.PioLPM2.OHTStart(Struct.Detail.OHT.EmOHTtype.UNLOAD);
            }
            else if(btn == btnLpm1StepReset)
            {
                GG.Equip.PioLPM1.StepReset();
            }
            else if (btn == btnLpm1StepReset)
            {
                GG.Equip.PioLPM2.StepReset();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblOHTstep1.Text = GG.Equip.PioLPM1.Step.ToString();
            lblOHTStep2.Text = GG.Equip.PioLPM2.Step.ToString();
        }

        private void btnLpm1ULoadComplete_Click(object sender, EventArgs e)
        {
            HsmsPortInfo info = new HsmsPortInfo();
            info.CstID = tbLpm1CstID.Text;
            info.IsCstExist = false;
            info.LoadportNo = 1;
            info.PortMode = PortMode.UNLOAD_COMPLETE;

            if (GG.Equip.HsmsPc.StartCommand(GG.Equip, EmHsmsPcCommand.PORT_MODE_CHANGE, info) == false) return;
        }

        private void btnLpm1LoadComplete_Click(object sender, EventArgs e)
        {
            HsmsPortInfo info = new HsmsPortInfo();
            info.CstID = tbLpm1CstID.Text;
            info.IsCstExist = false;
            info.LoadportNo = 1;
            info.PortMode = PortMode.LOAD_COMPLETE;

            if (GG.Equip.HsmsPc.StartCommand(GG.Equip, EmHsmsPcCommand.PORT_MODE_CHANGE, info) == false) return;
        }

        private void btnLpm2LoadComplete_Click(object sender, EventArgs e)
        {
            HsmsPortInfo info = new HsmsPortInfo();
            info.CstID = tbLpm2CstID.Text;
            info.IsCstExist = false;
            info.LoadportNo = 1;
            info.PortMode = PortMode.LOAD_COMPLETE;

            if (GG.Equip.HsmsPc.StartCommand(GG.Equip, EmHsmsPcCommand.PORT_MODE_CHANGE, info) == false) return;
        }

        private void btnLpm2ULoadComplete_Click(object sender, EventArgs e)
        {
            HsmsPortInfo info = new HsmsPortInfo();
            info.CstID = tbLpm2CstID.Text;
            info.IsCstExist = false;
            info.LoadportNo = 1;
            info.PortMode = PortMode.UNLOAD_COMPLETE;

            if (GG.Equip.HsmsPc.StartCommand(GG.Equip, EmHsmsPcCommand.PORT_MODE_CHANGE, info) == false) return;
        }
    }
}
