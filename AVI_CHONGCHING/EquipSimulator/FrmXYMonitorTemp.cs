using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using EquipComm.PLC;
using AxACTMULTILib;

namespace EquipSimulator
{
    public partial class FrmXYMonitorTemp : Form
    {
        public VirtualPlc PLC { get; set; }
        public class MonItem
        {
            public PlcAddr Addr { get; set; }
            public string Name { get; set; }
        }

        public FrmXYMonitorTemp(bool isCtrlPc)
        {
            InitializeComponent();
            axActEasyIF1.ActLogicalStationNumber = 150;
            PLC = new VirtualPlc(axActEasyIF1, "Simul");

      
            LoadAddress(isCtrlPc);
        }

        public void LoadAddress(bool isCtrlPc)
        {
            if (isCtrlPc)
            { 
                InitailizeAddress(dgvY300, AddMgr.LstMasterPCPlcAddr, 0, PLC);
                InitailizeAddress(dgvY340, AddMgr.LstMasterPCPlcAddr, 1, PLC);
                InitailizeAddress(dgvX3A0, AddMgr.LstMasterPCPlcAddr, 2, PLC);
                InitailizeAddress(dgvY3C0, AddMgr.LstMasterPCPlcAddr, 3, PLC);
                InitailizeAddress(dgvX360, AddMgr.LstMasterPCPlcAddr, 4, PLC);
                InitailizeAddress(dgvX380, AddMgr.LstMasterPCPlcAddr, 5, PLC);
            }
            else
            {
                InitailizeAddress(dgvY300, AddMgr.LstSimulatorPCPlcAddr, 0, PLC);
                InitailizeAddress(dgvY340, AddMgr.LstSimulatorPCPlcAddr, 1, PLC);
                InitailizeAddress(dgvX3A0, AddMgr.LstSimulatorPCPlcAddr, 2, PLC);
                InitailizeAddress(dgvY3C0, AddMgr.LstSimulatorPCPlcAddr, 3, PLC);
                InitailizeAddress(dgvX360, AddMgr.LstSimulatorPCPlcAddr, 4, PLC);
                InitailizeAddress(dgvX380, AddMgr.LstSimulatorPCPlcAddr, 5, PLC);
            }

        }

        private void InitailizeAddress(DataGridView dgv, List<PlcAddr> lstAddr, int no, VirtualPlc plc)
        {
            dgv.Tag = plc;

            dgv.ReadOnly = false;

            dgv.Columns[0].Width = 150;
            dgv.Columns[0].HeaderText = "Name";
            dgv.Columns[0].ReadOnly = true;

            dgv.Columns[1].Width = 70;
            dgv.Columns[1].HeaderText = "Address";
            dgv.Columns[1].ReadOnly = true;

            dgv.Columns[2].Width = 30;
            dgv.Columns[2].HeaderText = "Type";
            dgv.Columns[2].ReadOnly = true;


            dgv.Columns[3].Width = 60;
            dgv.Columns[3].HeaderText = "Value";
            dgv.Columns[3].ReadOnly = true;


            dgv.Columns[4].Width = 60;
            dgv.Columns[4].HeaderText = "Set Value";


            dgv.Columns[5].Width = 50;
            dgv.Columns[5].HeaderText = "Set";


            List<MonItem> lst = new List<MonItem>();
            //var bindingFlags = BindingFlags.Public | BindingFlags.Static;
            //foreach (FieldInfo info in idb.GetType().GetFields(bindingFlags))
            //{
            //    //if (chkHandSake.Checked == false)
            //    //{
            //    //    //if (info.Name.Substring(0, 2) == "LO") continue;
            //    //    //if (info.Name.Substring(0, 2) == "UP") continue;
            //    //}
            //    Console.WriteLine(info.Name);
            //    var plcAddr = info.GetValue(idb) as PlcAddr;
            //    if (plcAddr != null)
            //        lst.Add(new MonItem() { Addr = plcAddr, Name = info.Name });
            //}

            for (int iPos = no * 64; iPos < (no + 1) * 64; iPos++)
            {
                if (iPos >= lstAddr.Count) continue;
                lst.Add(new MonItem() { Addr = lstAddr[iPos], Name = lstAddr[iPos].Desc });
            }
            dgv.Rows.Clear();
            foreach (MonItem item in lst )
            {
                int irow = dgv.Rows.Add(new string[] 
                {
                    item.Name,
                    item.Addr.ToString(),
                    item.Addr.ValueType.ToString().Substring(0, 1),
                    "-", "-", "SET"
                });

                dgv.Rows[irow].Tag = item;
            }
        }

        private void tmrMonitor_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = DateTime.Now.ToString("HH:mm:ss.fff");

            UpdateBitWordValues(dgvY300);
            UpdateBitWordValues(dgvY340);
            UpdateBitWordValues(dgvX3A0);
            UpdateBitWordValues(dgvY3C0);
            UpdateBitWordValues(dgvX360);
        }

        private void UpdateBitWordValues(DataGridView grid)
        {
            VirtualPlc plc = grid.Tag as VirtualPlc;

            foreach (DataGridViewRow ff in grid.Rows)
            {
                MonItem item = ff.Tag as MonItem;
                if (item == null) return;

                string value = GetStringValue(plc, item.Addr);
                ff.Cells[3].Value = value;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (PLC.Open() == 0)
            {
                lblTimer.BackColor = Color.Blue;
            }
            else
            {
                lblTimer.BackColor = Color.Red;
            }



            tmrIndex.Interval = 1000;
            tmrIndex.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tmrIndex.Stop();
        }
        public string GetStringValue(VirtualPlc PLC, PlcAddr Address)
        {
            string result = string.Empty;
            if (Address.ValueType == PlcValueType.BIT)
            {
                bool value = PLC.GetBit(Address);
                result = value ? "1" : "0";
            }
            else if (Address.ValueType == PlcValueType.SHORT)
            {
                //short value = PLC.GetShort(Address);
                //result = value.ToString();
            }
            else if (Address.ValueType == PlcValueType.INT32)
            {
                int value = PLC.GetInt32(Address);
                result = value.ToString();
            }
            else if (Address.ValueType == PlcValueType.ASCII)
            {
                string value = PLC.ReadAscii(Address);
                result = value;
            }

            return result;
        }
        public void SetStringValue(VirtualPlc PLC, PlcAddr Address, string strValue)
        {
            if (Address.ValueType == PlcValueType.BIT)
            {
                bool value = strValue == "1" ? true : false;
                PLC.SetBit(Address, value);
            }
            else if (Address.ValueType == PlcValueType.SHORT)
            {
                short value = 0;
                if (short.TryParse(strValue, out  value))
                {
                    PLC.WriteShort(Address, value);
                }
                else
                {
                    //UserMessageBox.ShowAutoClose("이상값 입력함", "DIT CIM", 3);
                }
            }
            else if (Address.ValueType == PlcValueType.INT32)
            {
                int value = 0;
                if (int.TryParse(strValue, out  value))
                {
                    PLC.SetInt32(Address, value);
                }
                else
                {
                    //UserMessageBox.ShowAutoClose("이상값 입력함", "DIT CIM", 3);
                }
            }
            else if (Address.ValueType == PlcValueType.ASCII)
            {
                string value = strValue;
                PLC.WriteAscii(Address, value);
            }
        }

        private void dgvX000_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            DataGridView dgv = sender as DataGridView;
            if (dgv == null) return;

            VirtualPlc plc = dgv.Tag as VirtualPlc;

            if (e.ColumnIndex == 5)
            {
                if (dgv.Rows[e.RowIndex].Cells[4].Value != null)
                {
                    MonItem item = dgv.Rows[e.RowIndex].Tag as MonItem;

                    if (item != null)
                    {
                        string strValue = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();
                        SetStringValue(plc, item.Addr, strValue);
                    }
                }
            }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            int width = (panel1.Width - (5 * 25)) / 6;

            pnl1.Width = width;
            pnl2.Width = width;
            pnl3.Width = width;
            pnl4.Width = width;
            pnl5.Width = width;
            pnl6.Width = width;
        }
    }
}
