using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EquipMainUi.Struct.Step;

namespace EquipMainUi.Monitor
{
    public partial class UcrlPIOSignalRecv : UserControl
    {
        PioSignalRecv recvsignal;
        public UcrlPIOSignalRecv()
        {
            InitializeComponent();
        }

        public void PIOInitialize(string name, PioSignalRecv _recvsignal)
        {
            recvsignal = _recvsignal;

            lblName.Text = "■ " + name+ "-> ROBOT";

            lblSendAble.Text = name + " " + lblSendAble.Text;
            lblSendStart.Text = name + " " + lblSendStart.Text;
            lblSendComplete.Text = name + " " + lblSendComplete.Text;

            lblRecvAble.Text = "ROBOT " + lblRecvAble.Text;
            lblRecvStart.Text = "ROBOT " + lblRecvStart.Text;
            lblRecvComplete.Text = "ROBOT " + lblSendAble.Text;
        }

        public void UpdateUI()
        {
            lblXSendAble.BackColor = recvsignal.XSendAble == true ? Color.Red : Color.White;
            lblXSendStart.BackColor = recvsignal.XSendStart == true ? Color.Red : Color.White;
            lblXSendComplete.BackColor = recvsignal.XSendComplete == true ? Color.Red : Color.White;

            lblYRecvAble.BackColor = recvsignal.YRecvAble == true ? Color.Red : Color.White;
            lblYRecvStart.BackColor = recvsignal.YRecvStart == true ? Color.Red : Color.White;
            lblYRecvComplete.BackColor = recvsignal.YRecvComplete == true ? Color.Red : Color.White;
        }
    }
}
