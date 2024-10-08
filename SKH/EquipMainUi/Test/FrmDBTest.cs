using EquipMainUi.Struct.TransferData;
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
    public partial class FrmDBTest : Form
    {
        public FrmDBTest()
        {
            InitializeComponent();
        }

        private void btnCstInsert_Click(object sender, EventArgs e)
        {
            CassetteInfo cst = new CassetteInfo(tbCstCstID.Text, int.Parse(tbCstLoadPort.Text));

            TransferDataMgr.InsertCst(cst);
        }

        private void btnWaferInsert_Click(object sender, EventArgs e)
        {
            //WaferInfo wafer = new WaferInfo(tbWaferCstID.Text, int.Parse(tbWaferSlot.Text), Struct.Detail.EFEM.EmEfemDBPort.LOADPORT1, Struct.Detail.EFEM.EmEfemMappingInfo.Presence);

            //TransferDataMgr.InsertWafer(wafer);
        }

        private void btnCstDeleteAll_Click(object sender, EventArgs e)
        {
            //TransferDataMgr.DeleteCst()
        }

        private void btnWaferDeleteAll_Click(object sender, EventArgs e)
        {

        }
    }
}
