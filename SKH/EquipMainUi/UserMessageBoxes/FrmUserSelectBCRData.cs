using EquipMainUi.Monitor;
using EquipMainUi.Setting;
using EquipMainUi.Struct;
using EquipMainUi.Struct.Detail.EFEM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EquipMainUi.UserMessageBoxes
{
    public partial class FrmUserSelectBCRData : Form
    {
        private EmReaderTypes ReaderType;
        private OCR_IS1741 _ocr;
        private BCR_DM150 _bcr1;
        private BCR_DM150 _bcr2;
        private RFIDController _rf;
        public EmPopupFlow PopupFlow;
        public string ReturnValue1;
        public string ReturnValue2;

        private void CommonInit()
        {
            InitializeComponent();
            this.TopMost = true;
        }
        public FrmUserSelectBCRData(Equipment equip, EmEfemPort port, BCR_DM150 bcr1, BCR_DM150 bcr2)
        {
            CommonInit();
            ReaderType = EmReaderTypes.BCR;
            _bcr1 = bcr1;
            _bcr2 = bcr2;
            lblCaution.Text = "";
            lblTitle.Text = port.ToString() + " BCR Read";
        }
        public void RequestPopup(string read1, string read2 = "")
        {
            txtRead1.Text = read1;
            txtRead2.Text = read2;
            PopupFlow = EmPopupFlow.PopupRequest;
        }

        private Stopwatch _stopwatch = new Stopwatch();
        private int _readTimeover = 500;
        public void ReadCmd()
        {
            string a = string.Empty;
            string b = string.Empty;
            txtRead1.Text = "";
            txtRead2.Text = "";
            ReturnValue1 = string.Empty;
            ReturnValue2 = string.Empty;
            _bcr1.GetBarcode();
            _bcr2.GetBarcode();
            lblNotify.Text = "Reading";
            _stopwatch.Restart();
            tmrUiUpdate.Enabled = true;
        }
        public void UpdateReadValue()
        {
            txtInput1.Text = txtRead1.Text = _bcr1.ReadValue;
            txtInput2.Text = txtRead2.Text = _bcr2.ReadValue;
        }
        public bool IsReadComplete(int idx)
        {
            return idx == 0 ? _bcr1.IsReadComplete : _bcr2.IsReadComplete;
        }
        private void tmrUiUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (IsReadComplete(0) && IsReadComplete(1))
                {
                    lblNotify.Text = "Complete";
                    UpdateReadValue();
                    tmrUiUpdate.Enabled = false;
                }
                else if (_stopwatch.ElapsedMilliseconds > _readTimeover)
                {
                    _stopwatch.Stop();
                    string a = "Timeover ";
                    a += IsReadComplete(0) ? "[1]" : "";
                    a += IsReadComplete(1) ? "[2]" : "";
                    lblNotify.Text = a;
                    tmrUiUpdate.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                if (AlarmMgr.Instance.IsHappened(GG.Equip, EM_AL_LST.AL_0946_UI_EXCEPTION) == false)
                {
                    Logger.ExceptionLog.AppendLine(LogLevel.Error, string.Format("UI 갱신 예외 발생 : {0}", ex.Message));
                    Logger.ExceptionLog.AppendLine(LogLevel.Error, Log.EquipStatusDump.CallStackLog());
                    AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0946_UI_EXCEPTION);
                }
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            ReadCmd();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (rdUseReadData1.Checked && IsFormatOK(txtRead1.Text) == false)
            {
                lblNotify.Text = "1번 데이터 이상";
                return;
            }
            if (rdUseInput1.Checked && IsFormatOK(txtInput1.Text) == false)
            {
                lblNotify.Text = "1번 사용자 입력 데이터 이상";
                return;
            }
            if (rdUseReadData2.Checked && IsFormatOK(txtRead2.Text) == false)
            {
                lblNotify.Text = "2번 데이터 이상";
                return;
            }
            if (rdUseInput2.Checked && IsFormatOK(txtInput2.Text) == false)
            {
                lblNotify.Text = "2번 사용자 입력 데이터 이상";
                return;
            }

            if (rdUseReadData1.Checked) ReturnValue1 = txtRead1.Text;
            else if (rdUseInput1.Checked) ReturnValue1 = txtInput1.Text;

            if (rdUseReadData2.Checked) ReturnValue2 = txtRead2.Text;
            else if (rdUseInput2.Checked) ReturnValue2 = txtInput2.Text;

            PopupFlow = EmPopupFlow.OK;
            Close();
        }

        private bool IsFormatOK(string readVal)
        {
            if (readVal == null || readVal == string.Empty)
                return false;

            return true;
        }

        private void FrmUserSelectReadData_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
            Hide();            
        }
    }
}
