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
    public enum EmReaderTypes
    {
        OCR,
        BCR,
        RF
    }
    public enum EmPopupFlow
    {
        OK,
        NG,
        PopupRequest,
        UserWait,        
    }
    public partial class FrmUserSelectReadData : Form
    {
        private EmReaderTypes ReaderType;
        private OCR_IS1741 _ocr;
        private BCR_DM150 _bcr1;
        private BCR_DM150 _bcr2;
        private RFIDController _rf;
        public EmPopupFlow PopupFlow;
        public string ReturnValue;
        private bool _tagFlag;

        private void CommonInit()
        {
            InitializeComponent();
            this.TopMost = true;
        }
        public FrmUserSelectReadData(Equipment equip, EmEfemPort port, OCR_IS1741 ocr)
        {
            CommonInit();
            ReaderType = EmReaderTypes.OCR;
            _ocr = ocr;
            lblCaution.Text = "";
            label2.Visible = false;
            txtRead2.Visible = false;
            rdUseReadData2.Visible = false;
            lblTitle.Text = port.ToString() + " OCR Read";
        }
        public FrmUserSelectReadData(Equipment equip, EmEfemPort port, RFIDController rf)
        {
            CommonInit();
            ReaderType = EmReaderTypes.RF;
            _rf = rf;            
            lblCaution.Text = GG.boChinaLanguage ? "Cassette ID长度 最多30字" : "CST ID 길이 최대 30자";                        
            lblTitle.Text = port.ToString() + " RF Read";
        }

        public FrmUserSelectReadData(Equipment equip, EmEfemPort port, BCR_DM150 bcr1, BCR_DM150 bcr2)
        {
            CommonInit();
            ReaderType = EmReaderTypes.BCR;
            _bcr1 = bcr1;
            _bcr2 = bcr2;
            lblCaution.Text = "";
            lblTitle.Text = port.ToString() + " BCR Read";
        }

        public void RequestPopup(string read1, string read2 = "", bool IsUserInput = false)
        {
            txtRead1.Text = read1;
            txtRead2.Text = read2;
            if(read1 == string.Empty && read2 == string.Empty)
            {
                txtInput1.Text = DateTime.Now.ToString("yyMMddHHmmssfff");
            }
            if(IsUserInput)
            {
                rdUseInput.Checked = true;
            }

            PopupFlow = EmPopupFlow.PopupRequest;
        }

        private Stopwatch _stopwatch = new Stopwatch();
        private int _readTimeover = 5000;
        public void ReadCmd()
        {
            string a = string.Empty;
            string b = string.Empty;
            txtRead1.Text = "";
            txtRead2.Text = "";
            ReturnValue = string.Empty;
            switch (ReaderType)
            {
                case EmReaderTypes.OCR:
                    if (_ocr.Read() == false)
                    {
                        lblNotify.Text = "Error";
                        return;
                    }
                    else
                        lblNotify.Text = "Reading";
                    break;
                case EmReaderTypes.RF:
                    if(_tagFlag)
                    {
                        _rf.ScanTagCmd(Monitor.RFIDCmd.READER1);
                        _tagFlag = false;
                    }
                    else
                    {
                        _rf.ScanTagCmd(Monitor.RFIDCmd.READER2);
                        _tagFlag = true;
                    }
                    lblNotify.Text = "Reading";
                    break;
                case EmReaderTypes.BCR:
                    _bcr1.GetBarcode();
                    _bcr2.GetBarcode();
                    break;
            }
            _stopwatch.Restart();
            tmrUiUpdate.Enabled = true;
        }
        public void UpdateReadValue()
        {
            switch (ReaderType)
            {
                case EmReaderTypes.OCR:
                    txtInput1.Text = txtRead1.Text = _ocr.Readvalue;
                    break;
                case EmReaderTypes.RF:
                    txtInput1.Text = GG.TestMode == true ? DateTime.Now.ToString("yyMMddHHmmssfff") : txtRead1.Text = _rf.GetReaderReadID[0];
                    txtRead2.Text = _rf.GetReaderReadID[1];
                    if (txtRead1.Text != "")
                    {
                        rdUseReadData1.Checked = true;
                    }
                    else if(txtRead2.Text != "")
                    {
                        rdUseReadData2.Checked = true;
                    }
                    else
                    {
                        rdUseInput.Checked = true;
                    }
                    break;
                case EmReaderTypes.BCR:
                    txtInput1.Text = txtRead1.Text = _bcr1.ReadValue;
                    txtRead2.Text = _bcr2.ReadValue;
                    break;
            }
        }
        public bool IsReadComplete(int idx)
        {
            switch (ReaderType)
            {
                case EmReaderTypes.OCR:
                    return idx == 0 ? _ocr.IsReadComplete : false;                
                case EmReaderTypes.RF:
                    return idx == 0 ? _rf.IsReader1Success() : _rf.IsReader2Success();
                case EmReaderTypes.BCR:
                    return idx == 0 ? _bcr1.IsReadComplete : _bcr2.IsReadComplete;
                default:
                    return true;
            }            
        }
        private void tmrUiUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (IsReadComplete(0) || IsReadComplete(1))
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
                else if (GG.TestMode)
                {
                    UpdateReadValue();
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
                lblNotify.Text = GG.boChinaLanguage ? "1号使用者输入 Data 问题 发生" : "1번 데이터 이상";
                return;
            }
            if (rdUseReadData2.Checked && IsFormatOK(txtRead2.Text) == false)
            {
                lblNotify.Text = GG.boChinaLanguage ? "1号使用者输入 Data 问题 发生" : "1번 데이터 이상";
                return;
            }
            if (rdUseInput.Checked && IsFormatOK(txtInput1.Text) == false)
            {
                lblNotify.Text = GG.boChinaLanguage ? "需输入3字以上的Recipe 形式文字(Ex: TC5, TCH)" : "3자리 이상의 레시피 형식 문자 입력 필요(예 : TC5, TCH)";
                return;
            }

            if (rdUseReadData1.Checked) ReturnValue = txtRead1.Text;
            else if (rdUseReadData2.Checked) ReturnValue = txtRead2.Text;
            else if (rdUseInput.Checked) ReturnValue = txtInput1.Text;

            txtRead1.Text = string.Empty;
            txtRead2.Text = string.Empty;

            PopupFlow = EmPopupFlow.OK;
            Close();
        }

        private bool IsFormatOK(string readVal)
        {
            if (readVal == null || readVal == string.Empty)
                return false;

            switch (ReaderType)
            {
                case EmReaderTypes.OCR:
                    return true;
                case EmReaderTypes.RF:                    
                    return readVal.Length <= 30;
                case EmReaderTypes.BCR:
                    return readVal.Length >= 3;
                default:
                    return true;
            }
        }

        private void FrmUserSelectReadData_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
            Hide();            
        }
    }
}
