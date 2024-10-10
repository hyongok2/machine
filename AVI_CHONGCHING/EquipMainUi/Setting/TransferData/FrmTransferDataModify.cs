using EquipMainUi.Struct.TransferData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMainUi.Setting.TransferData
{
    public partial class FrmTransferDataModify : Form
    {
        List<WaferInfo> ListWaferInfo = new List<WaferInfo>();
        public FrmTransferDataModify()
        {
            InitializeComponent();

            UpdateWaferList();
        }

        public FrmTransferDataModify(WaferInfo wafer)
        {
            InitializeComponent();
            
            lvCurWaferList.BeginUpdate();
            ListViewItem lvi = new ListViewItem(wafer.CstID);
            lvi.SubItems.Add(wafer.SlotNo.ToString());

            lvCurWaferList.Items.Add(lvi);
            lvCurWaferList.EndUpdate();

            pGridInfo.SelectedObject = wafer;
        }

        public void UpdateWaferList()
        {
            
            lvCurWaferList.Items.Clear();

            var LPM1Cst = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort1.CstKey);
            var LPM2Cst = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort2.CstKey);

            //LPM1 CstID로 DB로부터 모든 wafer 받아와서 저장
            foreach (var item in TransferDataMgr.GetWafersIn(LPM1Cst.CstID))
            {
                ListWaferInfo.Add(item);
            }
            //LPM2 CstID로 DB로부터 모든 wafer 받아와서 저장
            foreach (var item in TransferDataMgr.GetWafersIn(LPM2Cst.CstID))
            {
                ListWaferInfo.Add(item);
            }

            lvCurWaferList.BeginUpdate();
            foreach (var item in ListWaferInfo)
            {
                ListViewItem lvi = new ListViewItem(item.CstID);
                lvi.SubItems.Add(item.SlotNo.ToString());

                lvCurWaferList.Items.Add(lvi);
            }
            lvCurWaferList.EndUpdate();
        }

        private void lvWaferList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvCurWaferList.SelectedItems.Count == 1)
            {
                ListViewItem lvItem = lvCurWaferList.SelectedItems[0];
                WaferInfo selectedWafer = TransferDataMgr.GetWafer(lvItem.SubItems[0].Text, int.Parse(lvItem.SubItems[1].Text));
                pGridInfo.SelectedObject = selectedWafer;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var waferInfo = (WaferInfo)pGridInfo.SelectedObject;

            TransferDataMgr.UpdateWaferInfo(waferInfo);

            UpdateWaferList();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
