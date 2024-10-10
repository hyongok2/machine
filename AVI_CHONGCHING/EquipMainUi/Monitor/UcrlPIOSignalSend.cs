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
    public partial class UcrlPIOSignalSend : UserControl
    {
        PioSignalSend sendsignal;
        public UcrlPIOSignalSend()
        {
            InitializeComponent();
        }

        public void PIOInitialize(string name, PioSignalSend _sendSignal)
        {
            lblName.Text = "■ ROBOT -> " + name;

            sendsignal = _sendSignal;

            lblSendAble.Text = "ROBOT " + lblSendAble.Text;
            lblSendStart.Text = "ROBOT " + lblSendStart.Text;
            lblSendComplete.Text = "ROBOT " + lblSendComplete.Text;

            lblRecvAble.Text = name + " " + lblRecvAble.Text;
            lblRecvStart.Text = name + " " + lblRecvStart.Text;
            lblRecvComplete.Text = name + " " + lblRecvComplete.Text;
        }

        public void UpdateUI()
        {
            lblXRecvAble.BackColor = sendsignal.XRecvAble == true ? Color.Red : Color.White;
            lblXRecvStart.BackColor = sendsignal.XRecvStart == true ? Color.Red : Color.White;
            lblXRecvComplete.BackColor = sendsignal.XRecvComplete == true ? Color.Red : Color.White;

            lblYSendAble.BackColor = sendsignal.YSendAble == true ? Color.Red : Color.White;
            lblYSendStart.BackColor = sendsignal.YSendStart == true ? Color.Red : Color.White;
            lblYSendComplete.BackColor = sendsignal.YSendComplete == true ? Color.Red : Color.White;
        }
    }
}
