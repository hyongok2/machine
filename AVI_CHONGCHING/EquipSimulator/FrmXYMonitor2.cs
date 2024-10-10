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

namespace EquipSimulator
{
    public partial class FrmXYMonitor2 : Form
    {
        public VirtualMem AOI_PLC { get; set; }
        public class MonItem
        {
            public PlcAddr Addr { get; set; }
            public string Name { get; set; }
        }

        public FrmXYMonitor2()
        {
            InitializeComponent();
            LoadAddress();
        }

        public void LoadAddress()
        {
            InitailizeAddress(dgvX000, AddMgr.LstTesterPlcAddr, GG.PLC);
            //InitailizeAddress(dgvY000, new Y000(), GG.PLC);
            //InitailizeAddress(dgvUmac, new UMAC(), GG.PLC);
            //InitailizeAddress(dgvInsp, new INSP(), GG.MEM_INSP);
            //InitailizeAddress(dgvReveiw, new REVIEW(), GG.MEM_REV);

        }

        private void InitailizeAddress(DataGridView dgv, List<PlcAddr> lstPlcAddr, VirtualMem plc)
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
            foreach (PlcAddr info in lstPlcAddr)
            {
                //Console.WriteLine(info.Name);
                //var plcAddr = info.GetValue(idb) as PlcAddr;
                //if (dgvX000 == dgv || dgvY000 == dgv || dgvUmac == dgv)
                //{
                //    if (plcAddr.Addr > 0x1000)
                //        continue;
                //}
                //if (plcAddr != null)
                lst.Add(new MonItem() { Addr = info, Name = info.Desc });
            }

            dgv.Rows.Clear();
            foreach (MonItem item in lst.OrderBy(f => f.Addr.ToString()).ThenBy(g => g.Addr.Bit))
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

            UpdateBitWordValues(dgvX000);
            UpdateBitWordValues(dgvY000);
            UpdateBitWordValues(dgvUmac);
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

        private void FrmXYMonitor_Resize(object sender, EventArgs e)
        {
            int width = (panel1.Width - (5 * 6)) / 3;

            pnl1.Width = width;
            pnl2.Width = width;
            pnl3.Width = width;            
        }
    }
}
