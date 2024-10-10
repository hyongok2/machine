using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EquipMainUi.Struct.TransferData;
using EquipMainUi.Struct.Detail.EFEM;

namespace EquipMainUi.Setting.TransferData
{
    public partial class UcrlTransferDataKeyInfo : UserControl
    {
        public EmEfemDBPort port;

        public UcrlTransferDataKeyInfo()
        {
            InitializeComponent();

            pGrid.Enabled = false;
        }

        public void SetInfo(EmEfemDBPort port, WaferInfoKey wafer, bool isDetect)
        {
            lblPort.Text = port.ToString();
            lblWaferExist.BackColor = isDetect ? Color.Red : Color.Transparent;
            pGrid.SelectedObject = wafer == null || wafer.SlotNo <= 0 ? null : wafer;
        }
        public void SetInfo(EmEfemDBPort port, CassetteInfoKey cst, bool isDetect)
        {
            lblPort.Text = port.ToString();
            lblWaferExist.BackColor = isDetect ? Color.Red : Color.Transparent;
            pGrid.SelectedObject = cst == null ? null : cst;
        }

        public WaferInfoKey GetInfo()
        {
            WaferInfoKey w = pGrid.SelectedObject as WaferInfoKey;
            return w;
        }
    }
}
