using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace EquipMainUi.PreAligner
{
    public partial class UcrlLightControllerTest : UserControl
    {
        LightControllerProxy proxy;
        public UcrlLightControllerTest()
        {
            InitializeComponent();
            foreach (var name in SerialPort.GetPortNames())
                cmbPortNames.Items.Add(name);            
        }

        public void Initialize()
        {
            proxy = GG.Equip.PreAligner.LightController;
            if (proxy.IsInitDone)
                cmbPortNames.Text = proxy.Port;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {            
            proxy.Reopen(cmbPortNames.SelectedText);
        }

        private void btnRemote_Click(object sender, EventArgs e)
        {
            proxy.Remote();
        }

        private void btnOn_Click(object sender, EventArgs e)
        {
            int b;
            if (int.TryParse(txtBright.Text, out b))
            {
                proxy.On(b);
            }
            else
                txtBright.Text = "재입력";
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            proxy.Off();
        }

        private void btnGetError_Click(object sender, EventArgs e)
        {
            proxy.GetErrMsg();
        }

        public void UpdateUI()
        {
            if (proxy == null) return;
            if (proxy.IsInitDone == false) return;

            btnOpen.OnOff = proxy.IsOpen;
            btnOn.OnOff = proxy.IsOn;
            btnRemote.OnOff = proxy.IsRemote;
            lblErrorMsg.Text = proxy.ErrorMsg;
        }
    }
}
