using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EquipMainUi.Struct.Step;
using EquipMainUi.Struct;

namespace EquipMainUi.Monitor
{

    public partial class FrmTactTime : Form
    {
        private const int TACK_COUNT = 10;

        public Struct.Equipment Equip { get; set; }

        public List<TactTimeSpan> LstLoadingTactTimeSpan = new List<TactTimeSpan>();
        public List<TactTimeSpan> LstScanningTactTimeSpan = new List<TactTimeSpan>();
        public List<TactTimeSpan> LstUnloadingTactTimeSpan = new List<TactTimeSpan>();

        public List<TactTimeSpan> LstMainTactTimeSpan;

        public FrmTactTime(Struct.Equipment equip)
        {
            Equip = equip;
            InitializeComponent();
            LstMainTactTimeSpan = TactTimeMgr.Instance.LstMainTactTimeSpan;
            btnlTotal_Click(null, null);
        }
        private void btnlTotal_Click(object sender, EventArgs e)
        {
            lblTactTitle.Text = "■ TOTAL TACT";
            lstTact.SuspendLayout();

            lstTact.Items.Clear();
            for (int iPos = 0; iPos < LstMainTactTimeSpan.Count; iPos++)
                lstTact.Items.Add((new ListViewItem(new string[] { LstMainTactTimeSpan[iPos].Name, "", "", "", "", "", "", "", "", "", "" })));

            lstTact.ResumeLayout();

            lstGlassInfo.SuspendLayout();

            lstGlassInfo.Items.Clear();
            lstGlassInfo.Items.Add((new ListViewItem(new string[] { "Glass ID", "", "", "", "", "", "", "", "", "", "" })));
            _showTact = 0;
        }
        private void btnlLoading_Click(object sender, EventArgs e)
        {
            lblTactTitle.Text = "■ LOADING TACT";
            lstTact.SuspendLayout();
            lstTact.Items.Clear();
            /*
            foreach (EM_TT_LST alid in _lstLoadingTack)
            {
                lstTact.Items.Add((new ListViewItem(new string[] { alid.ToString(), "", "", "", "", "", "", "", "", "", "" })));
            }*/

            lstTact.ResumeLayout();
            _showTact = 1;
        }
        private void btnlScan_Click(object sender, EventArgs e)
        {
            lblTactTitle.Text = "■ SCAN TACT";
            lstTact.SuspendLayout();
            lstTact.Items.Clear();
            /*
            foreach (EM_TT_LST alid in _lstScanTack)
            {
                lstTact.Items.Add((new ListViewItem(new string[] { alid.ToString(), "", "", "", "", "", "", "", "", "", "" })));
            }*/
            lstTact.ResumeLayout();
            _showTact = 2;
        }
        private void btnlUnLoading_Click(object sender, EventArgs e)
        {
            lblTactTitle.Text = "■ UNLOADING TACT";

            lstTact.SuspendLayout();
            lstTact.Items.Clear();
            /*
            foreach (EM_TT_LST alid in _lstUnloadingTack)
            {
                lstTact.Items.Add((new ListViewItem(new string[] { alid.ToString(), "", "", "", "", "", "", "", "", "", "" })));
            }*/
            lstTact.ResumeLayout();

            _showTact = 3;
        }

        private int _showTact = -1;

        private void tmrUiUpdate_Tick(object sender, EventArgs e)
        {
            if (_showTact == 0)
            {
                ShowTotalTact();
            }
            else if (_showTact == 1)
            {
                ShowLoadingTact();
            }
            else if (_showTact == 2)
            {
                ShowScanTact();
            }
            else if (_showTact == 3)
            {
                ShowUnloadingTact();
            }
        }

        //메소드 - SHOW TACT        
        public object _currentItem = null;

        private void ShowTotalTact()
        {
            lstTact.SuspendLayout();
            SetTackList(LstMainTactTimeSpan);
            lstTact.ResumeLayout();
            SetTacttimeGlassInfo();
        }
        private void ShowLoadingTact()
        {
            lstTact.SuspendLayout();
            SetTackList(LstLoadingTactTimeSpan);
            lstTact.ResumeLayout();
        }
        private void ShowScanTact()
        {
            SetTackList(LstScanningTactTimeSpan);
        }
        private void ShowUnloadingTact()
        {
            SetTackList(LstUnloadingTactTimeSpan);
        }
        private void btnlTopMost_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
        }
        private void FrmTactTime_Load(object sender, EventArgs e)
        {
        }
        private void SetTackList(List<TactTimeSpan> lst)
        {
            for (int iPos = 0; iPos < lst.Count; iPos++)
            {
                for (int jPos = 0; jPos < TACK_COUNT; jPos++)
                {
                    string tackStr = TactTimeMgr.Instance.GetTackStr(lst[iPos].Start, lst[iPos].End, jPos);
                    if (tackStr != lstTact.Items[iPos].SubItems[jPos + 1].Text)
                        lstTact.Items[iPos].SubItems[jPos + 1].Text = tackStr;
                }
            }
        }
        private void SetTacttimeGlassInfo()
        {
            lstGlassInfo.SuspendLayout();
            for (int iPos = 0; iPos < TactTimeMgr.Instance.ListWaferID.Count; iPos++)
            {
                lstGlassInfo.Items[0].SubItems[iPos + 1].Text = TactTimeMgr.Instance.ListWaferID[iPos];
            }
            lstGlassInfo.ResumeLayout();
        }
        private void lstTact_KeyDown(object sender, KeyEventArgs e)
        {
            string data = string.Empty;
            if (e.Control && e.KeyValue == 'C')
            {
                foreach (ListViewItem item in lstTact.Items)
                {
                    foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                    {
                        data += subItem.Text;
                        data += "\t";                    
                    }
                    data += "\r\n";    
                }
                Clipboard.SetText(data);
            }            
        }
    }
}
