using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.IO;
using Dit.Framework.PLC;

namespace Dit.ShareMem.Viewer
{
    public partial class frmMain : Form
    {
        private VirtualShare _plc = null;

        public PlcAddr _startAddr = new PlcAddr(PlcMemType.S, 0, 0, 1);
        public frmMain()
        {
            InitializeComponent();
        }



        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            dgvDevice.SuspendLayout();
            dgvDevice.Rows.Clear();
            for (int iPos = 0; iPos < 100; iPos++)
                dgvDevice.Rows.Add(new string[] { "", "", "" });

            dgvDevice.ResumeLayout();

            foreach (string line in File.ReadLines(Path.Combine(Application.StartupPath, "Setting", "TagList.csv"), Encoding.Default))
            {
                string[] items = line.Split(new char[] { ',', '\t' });

                if (items.Length != 4) continue;
                if (items[0].Length == 0) continue;
                if (items[0][0] == '#') continue;

                PlcAddr addr = PlcAddr.Parsing(items[0]);
                addr.ValueType = items[1].ToUpper() == "BIT" ? PlcValueType.BIT :
                                  items[1].ToUpper() == "SHORT" ? PlcValueType.SHORT :
                                  items[1].ToUpper() == "INT32" ? PlcValueType.INT32 :
                                  items[1].ToUpper() == "ASCII" ? PlcValueType.ASCII : PlcValueType.NONO;
                addr.Length = int.Parse(items[2]);
                addr.Desc = items[3];

                int iRow = dgvTagList.Rows.Add(new string[] { addr.GetPlcAddressBitString(), addr.Length.ToString(), addr.ValueType.ToString(), addr.Desc, "", "", "쓰기" });
                dgvTagList.Rows[iRow].Tag = addr;
            }



            //for (int iPos = 0; iPos < 100; iPos++)
            //    lstView.Items.Add(new ListViewItem(new string[] { "", "", "" }));
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            _plc = new VirtualShare(txtShareMemName.Text, (int)nudShareMemSize.Value);
            _plc.Open();

            tmrUpdate.Interval = 100;
            tmrUpdate.Start();
            groupBox3.Enabled = true;
            tabControl1.Enabled = true;
        }
        private void txtStartDevice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                _startAddr = PlcAddr.Parsing(txtStartDevice.Text);
            }
        }
        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {

                if (_startAddr == null) return;
                dgvDevice.SuspendLayout();
                for (int iPos = 0; iPos < 100; iPos++)
                {
                    PlcAddr addr = _startAddr + (iPos * 2);
                    short valueShort = _plc.GetShort(addr);
                    int valueInt32 = _plc.GetInt32(addr);
                    string valueAscii = _plc.GetAscii(new PlcAddr(PlcMemType.S, addr.Addr, 0, 2));
                    string str = Convert.ToString(valueShort, 2).PadLeft(16, '0');

                    dgvDevice.Rows[iPos].Cells[0].Value = addr.ToString();
                    for (int jPos = 0; jPos < 16; jPos++)
                        dgvDevice.Rows[iPos].Cells[1 + jPos].Value = str.Substring(jPos, 1);
                    dgvDevice.Rows[iPos].Cells[17].Value = rdoViewShort.Checked ? valueShort.ToString() :
                                                           rdoViewInt32.Checked ? valueInt32.ToString() :
                                                           valueAscii;
                    dgvDevice.Rows[iPos].Tag = addr;
                }

                dgvDevice.ResumeLayout();
            }
            else
            {
                dgvTagList.SuspendLayout();

                for (int iPos = 0; iPos < dgvTagList.Rows.Count; iPos++)
                {
                    PlcAddr addr = dgvTagList.Rows[iPos].Tag as PlcAddr;

                    dgvTagList.Rows[iPos].Cells[4].Value = addr.ValueType == PlcValueType.BIT ? (_plc.GetBit(addr) ? "1" : "0") :
                                                           addr.ValueType == PlcValueType.SHORT ? _plc.GetShort(addr).ToString() :
                                                           addr.ValueType == PlcValueType.INT32 ? _plc.GetInt32(addr).ToString() :
                                                           addr.ValueType == PlcValueType.ASCII ? _plc.GetAscii(addr) : "NONO";

                }
                dgvTagList.ResumeLayout();
            }
        }

        private void btnWriteValue_Click(object sender, EventArgs e)
        {
            PlcAddr addr = PlcAddr.Parsing(txtWriteAddr.Text.ToUpper());
            _plc.SetShort(addr, (short)nudWriteShortBitValue.Value);
        }
        private void btnWriteAsciiValue_Click(object sender, EventArgs e)
        {
            PlcAddr addr = PlcAddr.Parsing(txtWriteAddr.Text.ToUpper());
            _plc.SetAscii(new PlcAddr(PlcMemType.S, addr.Addr, 0, txtWriteAsciiValue.Text.Length), txtWriteAsciiValue.Text);
        }
        private void btnWriteBitValue_Click(object sender, EventArgs e)
        {
            PlcAddr addr = PlcAddr.Parsing(txtWriteAddr.Text.ToUpper());
            _plc.SetBit(addr, (short)nudWriteShortBitValue.Value == 1);
        }

        private void tmrState_Tick(object sender, EventArgs e)
        {
            tbStatus.Text = GetCurrentStatus();
        }
        private string GetCurrentStatus()
        {
            Process p = Process.GetCurrentProcess();
            string s = string.Format("Ver {4}, Thread {0}, Handle {1}, Memory Use {2}kb, Peak Memory Use {3}kb, by dit.wslee",
                p.Threads.Count, p.HandleCount, p.WorkingSet64 / 1024, p.PeakWorkingSet64 / 1024, Application.ProductVersion);
            p.Close();
            p.Dispose();
            return s;
        }

        private void dgvDevice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvTagList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                PlcAddr addr = dgvTagList.Rows[e.RowIndex].Tag as PlcAddr;
                string value = (dgvTagList.Rows[e.RowIndex].Cells[5].Value ?? "").ToString();
                if (addr.ValueType == PlcValueType.BIT)
                {
                    if (string.IsNullOrEmpty(value))
                        _plc.Toggle(addr);
                    else
                        _plc.SetBit(addr, value.Trim() == "1");
                }
                else if (addr.ValueType == PlcValueType.SHORT)
                {
                    short vv = 0;
                    if (short.TryParse(value, out vv))
                    {
                        _plc.SetShort(addr, vv);
                    }
                }
                else if (addr.ValueType == PlcValueType.INT32)
                {
                    int vv = 0;
                    if (int.TryParse(value, out vv))
                    {
                        _plc.SetInt32(addr, vv);
                    }
                }
                else if (addr.ValueType == PlcValueType.ASCII)
                {
                    _plc.SetAscii(addr, value);
                }
            }
        }

        private void dgvTagList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PlcAddr addr = dgvDevice.Rows[e.RowIndex].Tag as PlcAddr;
            txtWriteAddr.Text = (addr == null) ? string.Empty : addr.GetPlcAddressBitString();
        }


    }
}
