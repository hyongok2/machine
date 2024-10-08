using EquipMainUi.Struct.Detail.EziStep;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMainUi.Test
{
    public partial class FrmEziStepMotorTester : Form
    {
        public FrmEziStepMotorTester()
        {
            InitializeComponent();

            _selectedEzi = GG.Equip.AlignerT;

            tmrUiUpdate.Start();
        }

        StepMotorEzi _selectedEzi = null;

        private void FrmEziStepMotorTester_Load(object sender, EventArgs e)
        {
            UpdateSerialPortList();
            comboBaudrate.SelectedIndex = 3;    // default baudrate: 115200 
        }
        private void UpdateSerialPortList()
        {
            comboBoxPortNo.Items.Clear();

            // Port No.
            string[] portlist = SerialPort.GetPortNames();

            List<int> PortNoList = new List<int>();

            foreach (string port in portlist)
                PortNoList.Add(int.Parse(port.Substring(3)));

            PortNoList.Sort();

            foreach (int portno in PortNoList)
                comboBoxPortNo.Items.Add(string.Format("{0}", portno));
        }

        private void tmrUiUpdate_Tick(object sender, EventArgs e)
        {
            lblCurStepMotorName.Text = _selectedEzi.Name;

            lblCurPtpStep.Text = _selectedEzi.PtpMoveCmdStep.ToString();
            lblCurHomeStep.Text = _selectedEzi.HommingCmdStep.ToString();

            tbCurPosition.Text = _selectedEzi.XF_CurrMotorPosition.vFloat.ToString();
            tbCurSpeed.Text = _selectedEzi.XF_CurrMotorSpeed.vFloat.ToString();

            lblStatusError.BackColor = _selectedEzi.IsServoError == true ? Color.Red : Color.Transparent;
            lblStatusHomeComp.BackColor = _selectedEzi.IsHomeCompleteBit == true ? Color.Red : Color.Transparent;
            lblStatusMinusLimit.BackColor = _selectedEzi.IsMinusLimit == true ? Color.Red : Color.Transparent;
            lblStatusPlusLimit.BackColor = _selectedEzi.IsPlusLimit == true ? Color.Red : Color.Transparent;
            lblStatusSvOn.BackColor = _selectedEzi.IsServoOnBit == true ? Color.Red : Color.Transparent;
            lblStatusMoving.BackColor = _selectedEzi.IsMoving == true ? Color.Red : Color.Transparent;
        }

        private void rbSalveNo0_Click(object sender, EventArgs e)
        {
            RadioButton rdb = sender as RadioButton;

            if (rdb == rbSalveNo0)
                _selectedEzi = GG.Equip.AlignerT;
            else if (rdb == rbSalveNo1)
                _selectedEzi = GG.Equip.AlignerY;
            else if (rdb == rbSalveNo2)
                _selectedEzi = GG.Equip.AlignerX;
        }

        private void buttonJogPositive_MouseDown(object sender, MouseEventArgs e)
        {
            float speed = float.Parse(textBoxJogSpd.Text);

            _selectedEzi.JogMove(EM_STEP_MOTOR_JOG.JOG_PLUS, speed);
        }

        private void buttonJog_MouseUp(object sender, MouseEventArgs e)
        {
            float speed = float.Parse(textBoxJogSpd.Text);

            _selectedEzi.JogMove(EM_STEP_MOTOR_JOG.JOG_STOP, 0);
        }

        private void buttonJogNegative_MouseDown(object sender, MouseEventArgs e)
        {
            float speed = float.Parse(textBoxJogSpd.Text);

            _selectedEzi.JogMove(EM_STEP_MOTOR_JOG.JOG_MINUS, speed);
        }
        private void btnSetOriginPosition_Click(object sender, EventArgs e)
        {
            _selectedEzi.SetOrginPosition();
        }

        private void btnMotorStop_Click(object sender, EventArgs e)
        {
            _selectedEzi.MoveStop();
        }

        private void btnPtpMove_Click(object sender, EventArgs e)
        {
            float posi = 0;
            float speed = 0;
            float acc = 0;

            float.TryParse(tbPtpPosi.Text, out posi);
            float.TryParse(tbPtpSpeed.Text, out speed);
            float.TryParse(tbPtpAcc.Text, out acc);

            _selectedEzi.MovePosition(posi, speed, acc);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            _selectedEzi.MoveHome();
        }

        private void btnSetOriginPosition_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("미구현");
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            int result = GG.EZI.Open();
        }

        private void btnAlignLogicStart_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.RunMode = Struct.Step.EmEfemRunMode.Home;
        }

        private void btnSendPioSigOn_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Robot.PioSendAligner.YSendAble = true;
            GG.Equip.Efem.Robot.PioSendAligner.YSendStart = true;
            GG.Equip.Efem.Robot.PioSendAligner.YSendComplete = true;
        }

        private void btnWaferDetectOn_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Aligner.Status.IsWaferExist = true;
        }

        private void btnSendPioSigOff_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Robot.PioSendAligner.YSendAble = false;
            GG.Equip.Efem.Robot.PioSendAligner.YSendStart = false;
            GG.Equip.Efem.Robot.PioSendAligner.YSendComplete = false;
        }

        private void btnRecvSigOn_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Robot.PioRecvAligner.YRecvAble = true;
            GG.Equip.Efem.Robot.PioRecvAligner.YRecvStart = true;
            GG.Equip.Efem.Robot.PioRecvAligner.YRecvComplete = true;
        }

        private void btnWaferDetectOff_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Aligner.Status.IsWaferExist = false;
        }

        private void btnRecvPioSigOff_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Robot.PioRecvAligner.YRecvAble = false;
            GG.Equip.Efem.Robot.PioRecvAligner.YRecvStart = false;
            GG.Equip.Efem.Robot.PioRecvAligner.YRecvComplete = false;
        }

        private void btnAlignLogicStop_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.RunMode = Struct.Step.EmEfemRunMode.Stop;
        }

        private void btnSeqLogicStart_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.RunMode = Struct.Step.EmEfemRunMode.Start;
        }
    }
}
