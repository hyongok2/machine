using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Dit.Framework.PLC;
using EquipMainUi.Struct;
using DitCim.PLC;
using EquipMainUi.Setting;
using EquipMainUi.ConvenienceClass;
using System.Reflection;
using Dit.Framework.Ini;
using EquipMainUi.Struct.Detail.PC;
using Dit.Framework.UI.UserComponent;
using Dit.Framework.Comm;

namespace EquipMainUi.Monitor
{
    public partial class FrmMonitor : Form
    {
        private TabPage _showPage;
        private EM_LV_LST _curLevel;

        private Equipment _equip = null;

        public IVirtualMem PLC { get; set; }
        public Equipment Equip { get; set; }

        public FrmMonitor(Equipment equip)
        {
            InitializeComponent();

            ExtensionUI.AddClickEventLog(this);

            _motorBtnls = new ButtonLabel[] {
                btnl3ndStageX,
                btnl3ndInspY,
                btnl3ndAlignTheta,
            };

            _equip = equip;

            InitPmacStatusDgv(dgvEquipToPmacStatus, true);
            InitPmacStatusDgv(dgvPmacToEquipStatus, false);

            HSUpperName = HSUpperName.Replace("\r\n", "");
            HSLowerName = HSLowerName.Replace("\r\n", "");
            ContactName = ContactName.Replace("\r\n", "");
            
            #region _IO Item add to list
            _lstIoViewItems.Add(ucrlIoItem00);
            _lstIoViewItems.Add(ucrlIoItem01);
            _lstIoViewItems.Add(ucrlIoItem02);
            _lstIoViewItems.Add(ucrlIoItem03);
            _lstIoViewItems.Add(ucrlIoItem04);
            _lstIoViewItems.Add(ucrlIoItem05);
            _lstIoViewItems.Add(ucrlIoItem06);
            _lstIoViewItems.Add(ucrlIoItem07);
            _lstIoViewItems.Add(ucrlIoItem08);
            _lstIoViewItems.Add(ucrlIoItem09);
            _lstIoViewItems.Add(ucrlIoItem10);
            _lstIoViewItems.Add(ucrlIoItem11);
            _lstIoViewItems.Add(ucrlIoItem12);
            _lstIoViewItems.Add(ucrlIoItem13);
            _lstIoViewItems.Add(ucrlIoItem14);
            _lstIoViewItems.Add(ucrlIoItem15);
            _lstIoViewItems.Add(ucrlIoItem16);
            _lstIoViewItems.Add(ucrlIoItem17);
            _lstIoViewItems.Add(ucrlIoItem18);
            _lstIoViewItems.Add(ucrlIoItem19);
            _lstIoViewItems.Add(ucrlIoItem20);
            _lstIoViewItems.Add(ucrlIoItem21);
            _lstIoViewItems.Add(ucrlIoItem22);
            _lstIoViewItems.Add(ucrlIoItem23);
            _lstIoViewItems.Add(ucrlIoItem24);
            _lstIoViewItems.Add(ucrlIoItem25);
            _lstIoViewItems.Add(ucrlIoItem26);
            _lstIoViewItems.Add(ucrlIoItem27);
            _lstIoViewItems.Add(ucrlIoItem28);
            _lstIoViewItems.Add(ucrlIoItem29);
            _lstIoViewItems.Add(ucrlIoItem30);
            _lstIoViewItems.Add(ucrlIoItem31);
            #endregion            
            #region _ISPT Item add to list
            _lstPcViewItems.Add(ucrlPcIoItem1_1);
            _lstPcViewItems.Add(ucrlPcIoItem1_2);
            _lstPcViewItems.Add(ucrlPcIoItem1_3);
            _lstPcViewItems.Add(ucrlPcIoItem1_4);
            _lstPcViewItems.Add(ucrlPcIoItem1_5);
            _lstPcViewItems.Add(ucrlPcIoItem1_6);
            _lstPcViewItems.Add(ucrlPcIoItem1_7);
            _lstPcViewItems.Add(ucrlPcIoItem1_8);
            _lstPcViewItems.Add(ucrlPcIoItem1_9);
            _lstPcViewItems.Add(ucrlPcIoItem1_10);
            _lstPcViewItems.Add(ucrlPcIoItem1_11);
            _lstPcViewItems.Add(ucrlPcIoItem1_12);
            _lstPcViewItems.Add(ucrlPcIoItem1_13);
            _lstPcViewItems.Add(ucrlPcIoItem1_14);
            _lstPcViewItems.Add(ucrlPcIoItem1_15);
            _lstPcViewItems.Add(ucrlPcIoItem1_16);
            _lstPcViewItems.Add(ucrlPcIoItem2_1);
            _lstPcViewItems.Add(ucrlPcIoItem2_2);
            _lstPcViewItems.Add(ucrlPcIoItem2_3);
            _lstPcViewItems.Add(ucrlPcIoItem2_4);
            _lstPcViewItems.Add(ucrlPcIoItem2_5);
            _lstPcViewItems.Add(ucrlPcIoItem2_6);
            _lstPcViewItems.Add(ucrlPcIoItem2_7);
            _lstPcViewItems.Add(ucrlPcIoItem2_8);
            _lstPcViewItems.Add(ucrlPcIoItem2_9);
            _lstPcViewItems.Add(ucrlPcIoItem2_10);
            _lstPcViewItems.Add(ucrlPcIoItem2_11);
            _lstPcViewItems.Add(ucrlPcIoItem2_12);
            _lstPcViewItems.Add(ucrlPcIoItem2_13);
            _lstPcViewItems.Add(ucrlPcIoItem2_14);
            _lstPcViewItems.Add(ucrlPcIoItem2_15);
            _lstPcViewItems.Add(ucrlPcIoItem2_16);
            foreach (UcrlIoItem i in _lstPcViewItems)
                i.UseTestMode = false;
            #endregion
            UpdateIOTestModeUI(_equip.IsIoTestMode);

            btnShowX000_X01F.Selected = true;
            SetMonitorXY(X000_X01F);
            _showPage = tabp_XY;
            _selectedabP = tabpEFEMsStatus;

            dgvEquipToPmacStatus.ClearSelection();
            dgvPmacToEquipStatus.ClearSelection();

            //if (GG.IsDemo == true)
            //    btnShowX080_X09F.Enabled = false;
            EFEMStatInit();
            EFEMPIOInit();
            ChangeChinaLanguage();
        }
        private void ChangeChinaLanguage()
        {
            if (GG.boChinaLanguage)
            {
                label77.Text = "Ecat Reconnect (命令-回应)";	    // Ecat Reconnect(명령 - 응답)
                label74.Text = "PMac Reset (命令-回应)";	    // PMac Reset (명령-응답)
                label61.Text = "紧急停止 (命令-回应)";	    // 긴급 정지 (명령-응답)
                label130.Text = "停止检查 (命令-回应)";	    // 검사 정지 (명령-응답)
                label9.Text = "停止Review (命令-回应)";	    // 리뷰 정지 (명령-응답)
                label112.Text = "名称";	    // 명칭
                label113.Text = "状态";	    // 상태
                label139.Text = "Motor 原点命令";	    // 모터 홈 명령
                label138.Text = "Motor 原点 命令 回应";	    // 모터 홈 명령 응답
                label104.Text = "位置 (mm)";	    // 위치(mm)
                label112.Text = "速度 (mm/s)";	    // 속도(mm/s)
                label125.Text = "负荷率 (%)";	    // 부하율(%)
                label116.Text = "名称";	    // 명칭
                label111.Text = "名称";	    // 명칭
                label107.Text = "移动位置 (mm)";	    // 이동 위치 (mm)
                label103.Text = "移动速度 (mm/s)";	    // 이동 속도 (mm/s)
                label97.Text = "移动加速度 (ms)";	    // 이동 가속도 (ms)
                label95.Text = "移动命令";	    // 이동 명령
                label134.Text = "位置移动 Step";	    // 포지션 이동 스텝
                label7.Text = "Motor Jog 命令 (-)";	    // 모터 조그 명령 ( - )
                label6.Text = "Motor Jog 命令 (+)";	    // 모터 조그 명령 ( + )
                label132.Text = "Motor Jog 速度 (mm)";	    // 모터 조그 속도 (mm)
                label55.Text = "命令";	    // 명령
                label72.Text = "C : PC , P : PMac";          // C : 제어PC , P : PMac
            }
        }


        private void tmrUiUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_showPage == tabp_XY)
                {
                    UpdateIOXY();
                }

                if (_showPage == tabp_PMAC)
                {
                    UpdatePMac();
                }

                if (_showPage == tabp_PC)
                {
                    UpdatePC();
                }
                if (_showPage == tabp_Analog)
                {
                    ucrlItemAD1.UpdateUi();
                    ucrlItem_64RD1.UpdateUi();
                }
                if (_showPage == tabp_EFEM)
                {
                    UpdateEFEMStat();
                }

                UpdateCommonUI();
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
        private void UpdateCommonUI()
        {
            btnUseIOTestMode.BackColor = _equip.IsIoTestMode ? Color.Red : Color.Transparent;
        }

        private void tab1St_SelectedIndex(object sender, EventArgs e)
        {
            TabPage curPage = (sender as TabControl).SelectedTab;

            if (curPage == tabp_XY)
            {
                btnlShowX300_Y31F_Click(btnShowX000_X01F, null);
            }
            else if (curPage == tabp_PMAC)
            {
                setPmacBtnEnable(false);
                btnSomeMotorTitle_Click(_motorBtnls[0], null);
            }
            else if (curPage == tabp_PC)
            {
                btnlCtoI_Click(btnlCtoI, null);
            }
            else if(curPage == tabp_Analog)
            {
                ucrlItemAD1.Initialize("AD 1", _equip.ADC.Adc1);
                ucrlItemAD1.FillMonitor(1);
                ucrlItem_64RD1.Initialize("TD 1", _equip.ADC.Temperature);
                ucrlItem_64RD1.FillMonitor(1);
            }
            else if(curPage == tabp_EFEM)
            {

            }
            _showPage = curPage;
        }

        private void SettingForm_Closing(object sender, FormClosingEventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FrmSetting")
                {
                    return;
                }
            }
            _isPmacCmdEnable = false;
            lblPmacCmdEnable.BackColor = Color.Red;
        }

        #region IO XY
        private string X000_X01F = @"X000,X001,X002,X003,X004,X005,X006,X007,X008,X009,X00A,X00B,X00C,X00D,X00E,X00F,X010,X011,X012,X013,X014,X015,X016,X017,X018,X019,X01A,X01B,X01C,X01D,X01E,X01F";
        private string Y020_Y03F = @"Y020,Y021,Y022,Y023,Y024,Y025,Y026,Y027,Y028,Y029,Y02A,Y02B,Y02C,Y02D,Y02E,Y02F,Y030,Y031,Y032,Y033,Y034,Y035,Y036,Y037,Y038,Y039,Y03A,Y03B,Y03C,Y03D,Y03E,Y03F";
        private string X040_Y05F = @"X040,X041,X042,X043,X044,X045,X046,X047,X048,X049,X04A,X04B,X04C,X04D,X04E,X04F,Y050,Y051,Y052,Y053,Y054,Y055,Y056,Y057,Y058,Y059,Y05A,Y05B,Y05C,Y05D,Y05E,Y05F";
        private string X060_X07F = @"X060,X061,X062,X063,X064,X065,X066,X067,X068,X069,X06A,X06B,X06C,X06D,X06E,X06F,X070,X071,X072,X073,X074,X075,X076,X077,X078,X079,X07A,X07B,X07C,X07D,X07E,X07F";
        private string X080_X09F = @"X080,X081,X082,X083,X084,X085,X086,X087,X088,X089,X08A,X08B,X08C,X08D,X08E,X08F,Y090,Y091,Y092,Y093,Y094,Y095,Y096,Y097,Y098,Y099,Y09A,Y09B,Y09C,Y09D,Y09E,Y09F";
        private string Y0A0_Y0BF = @"Y0A0,Y0A1,Y0A2,Y0A3,Y0A4,Y0A5,Y0A6,Y0A7,Y0A8,Y0A9,Y0AA,Y0AB,Y0AC,Y0AD,Y0AE,Y0AF,Y0B0,Y0B1,Y0B2,Y0B3,Y0B4,Y0B5,Y0B6,Y0B7,Y0B8,Y0B9,Y0BA,Y0BB,Y0BC,Y0BD,Y0BE,Y0BF";
        private string X0C0_Y0DF = @"X0C0,X0C1,X0C2,X0C3,X0C4,X0C5,X0C6,X0C7,X0C8,X0C9,X0CA,X0CB,X0CC,X0CD,X0CE,X0CF,Y0D0,Y0D1,Y0D2,Y0D3,Y0D4,Y0D5,Y0D6,Y0D7,Y0D8,Y0D9,Y0DA,Y0DB,Y0DC,Y0DD,Y0DE,Y0DF";
        private string X0E0_X0FF = @"X0E0,X0E1,X0E2,X0E3,X0E4,X0E5,X0E6,X0E7,X0E8,X0E9,X0EA,X0EB,X0EC,X0ED,X0EE,X0EF,X0F0,X0F1,X0F2,X0F3,X0F4,X0F5,X0F6,X0F7,X0F8,X0F9,X0FA,X0FB,X0FC,X0FD,X0FE,X0FF";
        private string Y100_Y11F = @"Y100,Y101,Y102,Y103,Y104,Y105,Y106,Y107,Y108,Y109,Y10A,Y10B,Y10C,Y10D,Y10E,Y10F,Y110,Y111,Y112,Y113,Y114,Y115,Y116,Y117,Y118,Y119,Y11A,Y11B,Y11C,Y11D,Y11E,Y11F";
        private string X120_X13F = @"X120,X121,X122,X123,X124,X125,X126,X127,X128,X129,X12A,X12B,X12C,X12D,X12E,X12F,X130,X131,X132,X133,X134,X135,X136,X137,X138,X139,X13A,X13B,X13C,X13D,X13E,X13F";
        private string X140_X15F = @"X140,X141,X142,X143,X144,X145,X146,X147,X148,X149,X14A,X14B,X14C,X14D,X14E,X14F,X150,X151,X152,X153,X154,X155,X156,X157,X158,X159,X15A,X15B,X15C,X15D,X15E,X15F";
        private string X160_X17F = @"X160,X161,X162,X163,X164,X165,X166,X167,X168,X169,X16A,X16B,X16C,X16D,X16E,X16F,X170,X171,X172,X173,X174,X175,X176,X177,X178,X179,X17A,X17B,X17C,X17D,X17E,X17F";

        private List<UcrlIoItem> _lstIoViewItems = new List<UcrlIoItem>();

        private void UpdateIOXY()
        {
            pnl3rdXY.SuspendLayout();
            _lstIoViewItems.ForEach(f =>
            {
                f.UpdateUI();

            });
            pnl3rdXY.ResumeLayout();
        }

        public void SetMonitorXY(string lstAddr)
        {
            int iPos = 0;
            foreach (string strAddr in lstAddr.Split(','))
            {
                PlcAddr addr = AddressMgr.GetAddress(strAddr, (short)81);
                if (addr == null)
                {
                    addr = PlcAddr.Parsing(strAddr);
                    throw new Exception("미지정 ADDRESS 있음......");
                }

                if (addr.PLC == null)
                    addr.PLC = GG.CCLINK;
                if (addr.Desc == string.Empty)
                    addr.Desc = " ";

                _lstIoViewItems[iPos].MonAddress = addr;

                iPos++;
            }
        }

        private void btnlShowX300_Y31F_Click(object sender, EventArgs e)
        {
            btnShowX000_X01F.Selected = false;
            btnShowY020_Y03F.Selected = false;
            btnShowX040_Y05F.Selected = false;
            btnShowX060_X07F.Selected = false;
            btnShowX080_X09F.Selected = false;
            btnShowY0A0_Y0BF.Selected = false;
            btnShowX0C0_Y0DF.Selected = false;
            btnShowX0E0_X0FF.Selected = false;
            btnShowY100_Y11F.Selected = false;
            btnShowX120_X13F.Selected = false;
            btnShowX140_X15F.Selected = false;
            btnShowX160_X17F.Selected = false;

            /**/
            if (btnShowX000_X01F == (ButtonLabel)sender) { SetMonitorXY(X000_X01F); }
            else if (btnShowY020_Y03F == (ButtonLabel)sender) { SetMonitorXY(Y020_Y03F); }
            else if (btnShowX040_Y05F == (ButtonLabel)sender) { SetMonitorXY(X040_Y05F); }
            else if (btnShowX060_X07F == (ButtonLabel)sender) { SetMonitorXY(X060_X07F); }
            else if (btnShowX080_X09F == (ButtonLabel)sender) { SetMonitorXY(X080_X09F); }
            else if (btnShowY0A0_Y0BF == (ButtonLabel)sender) { SetMonitorXY(Y0A0_Y0BF); }
            else if (btnShowX0C0_Y0DF == (ButtonLabel)sender) { SetMonitorXY(X0C0_Y0DF); }
            else if (btnShowX0E0_X0FF == (ButtonLabel)sender) { SetMonitorXY(X0E0_X0FF); }
            else if (btnShowY100_Y11F == (ButtonLabel)sender) { SetMonitorXY(Y100_Y11F); }
            else if (btnShowX120_X13F == (ButtonLabel)sender) { SetMonitorXY(X120_X13F); }
            else if (btnShowX140_X15F == (ButtonLabel)sender) { SetMonitorXY(X140_X15F); }
            else if (btnShowX160_X17F == (ButtonLabel)sender) { SetMonitorXY(X160_X17F); }
            //else if (btnShowX2A0_X2BF == (ButtonLabel)sender) { SetMonitorXY(Y3C0_Y3DF); }

            ((ButtonLabel)sender).Selected = true;
        }

        private void btnUseIOTestMode_Click(object sender, EventArgs e)
        {
            bool passed = false;
            if (_equip.IsIoTestMode == false)
            {
                FormPWInput frmLogin = new FormPWInput("비밀번호를 입력하세요");
                frmLogin.ShowDialog();

                if (frmLogin.Passwd == "123456")
                {
                    _equip.IsIoTestMode = true;
                }
            }
            else
            {
                _equip.IsIoTestMode = false;
            }

            UpdateIOTestModeUI(_equip.IsIoTestMode);
        }
        private void UpdateIOTestModeUI(bool isON)
        {
            btnIOTestAllOnOff.Visible = isON;
        }

        private void txtFindInput_TextChanged(object sender, EventArgs e)
        {
            string ret = string.Empty;
            try
            {
                PlcAddr[] found = AddressMgr.FindIO(txtFindInput.Text, 10);
                foreach (PlcAddr p in found)
                    ret += p.ToString() + ", ";
            }
            catch (Exception ex)
            {
                Logger.Log.AppendLine(LogLevel.NoLog, "IO FIND EXCEPTION!");
            }
            lblFindResult.Text = ret;
        }

        private void btnIOTestAllOnOff_Click(object sender, EventArgs e)
        {
            if (_showPage == tabp_XY)
            {
                pnl3rdXY.SuspendLayout();
                _lstIoViewItems.ForEach(f =>
                {
                    f.MonAddress.UseBitTest = !f.MonAddress.UseBitTest;
                });
                pnl3rdXY.ResumeLayout();
            }
            if (_showPage == tabp_PC)
            {
                this.UpdatePC();
            }
        }
        #endregion

        #region PMAC
        private ServoMotorPmac _selectedServo = null;

        private void UpdatePMac()
        {
            if (_selectedServo == null) return;

            #region Common
            //Equip To Pmac Status
            dgvEquipToPmacStatus[1, 0].Style.BackColor = _equip.PMac.YB_IsAutoMode.vBit == true ? Color.Red : Color.White;
            dgvEquipToPmacStatus[1, 1].Style.BackColor = _equip.PMac.YB_CheckAlarmStatus.vBit == true ? Color.Red : Color.White;
            dgvEquipToPmacStatus[1, 2].Style.BackColor = _equip.PMac.YB_UpperInterfaceWorking.vBit == true ? Color.Red : Color.White;
            dgvEquipToPmacStatus[1, 3].Style.BackColor = _equip.PMac.YB_LowerInterfaceWorking.vBit == true ? Color.Red : Color.White;

            //Pmac To Equip Status
            dgvPmacToEquipStatus[1, 0].Style.BackColor = _equip.PMac.XB_PmacReady.vBit == true ? Color.Red : Color.White;
            dgvPmacToEquipStatus[1, 1].Style.BackColor = _equip.PMac.XB_PmacAlive.vBit == true ? Color.Red : Color.White;
            dgvPmacToEquipStatus[1, 2].Style.BackColor = _equip.PMac.XB_EcatCommAlive.vBit == true ? Color.Red : Color.White;
            dgvPmacToEquipStatus[1, 3].Style.BackColor = _equip.PMac.XB_InspRunning.vBit == true ? Color.Red : Color.White;
            dgvPmacToEquipStatus[1, 3].Style.BackColor = _equip.PMac.XB_ReviewRunning.vBit == true ? Color.Red : Color.White;

            // Interface            
            UpdateInterfaceButton(btnMotorInterlockOff   , Equip.PMac.YB_EquipStatusMotorInterlockOff);
            UpdateInterfaceButton(btnMotorInterlockOffAck, Equip.PMac.XB_EquipStatusMotorInterlockOffAck);
            UpdateInterfaceButton(btnEcatReconnectCmd    , Equip.PMac.YB_EcatReconnectCmd);
            UpdateInterfaceButton(btnEcatReconnectCmdAck , Equip.PMac.XB_EcatReconnectAck);
            UpdateInterfaceButton(btnPmacReset           , Equip.PMac.YB_PmacResetCmd);
            UpdateInterfaceButton(btnPmacResetAck        , Equip.PMac.XB_PmacResetCmdAck);
            UpdateInterfaceButton(btnPmacImmediateStop   , Equip.PMac.YB_EmsCmd);
            UpdateInterfaceButton(btnPmacImmediateStopAck, Equip.PMac.XB_EmsCmdAck);
            UpdateInterfaceButton(btnInspStopCmd         , Equip.PMac.YB_InspStopCmd);
            UpdateInterfaceButton(btnInspStopCmdAck      , Equip.PMac.XB_InspStopAck);
            UpdateInterfaceButton(btnReviewStopCmd       , Equip.PMac.YB_ReviewStopCmd);
            UpdateInterfaceButton(btnReviewStopCmdAck    , Equip.PMac.XB_ReviewStopAck);
            #endregion
            #region Axis Data

            //Home Command
            UpdateInterfaceButton(btnHomeCmd, _selectedServo.YB_HomeCmd);
            UpdateInterfaceButton(btnHomeCmdAck, _selectedServo.XB_HomeCmdAck);

            //Position, Speed, Stress
            double stress = _selectedServo.XF_CurrMotorStress.vFloat >= 0 && _selectedServo.XF_CurrMotorStress.vFloat <= 100
                ? _selectedServo.XF_CurrMotorStress.vFloat : 0;

            txtCurPos.Text                      /**/= _selectedServo.XF_CurrMotorPosition.vFloat.ToString("0.####");
            txtCurSpeed.Text                    /**/= _selectedServo.XF_CurrMotorSpeed.vFloat.ToString("0.####");            
            txtCurrStress.Text = stress.ToString();

            // Axis Data Status
            lblHomeCompleteBit.BackColor        /**/= _selectedServo.XB_StatusHomeCompleteBit ? Color.Red : Color.Gray;
            lblHomePositionOn.BackColor         /**/= _selectedServo.XB_StatusHomeInPosition ? Color.Red : Color.Gray;
            lblMotorMoving.BackColor            /**/= _selectedServo.XB_StatusMotorMoving ? Color.Red : Color.Gray;
            lblMotorServoOn.BackColor           /**/= _selectedServo.XB_StatusMotorServoOn ? Color.Red : Color.Gray;
            
            lblSWNegativeLimitSet.BackColor     /**/= _selectedServo.XB_StatusSWNegativeLimitSet ? Color.Red : Color.Gray;
            lblSWPositiveLimitSet.BackColor     /**/= _selectedServo.XB_StatusSWPositiveLimitSet ? Color.Red : Color.Gray;
            lblHWNegativeLimitSet.BackColor     /**/= _selectedServo.XB_StatusHWNegativeLimitSet ? Color.Red : Color.Gray;
            lblHWPositiveLimitSet.BackColor     /**/= _selectedServo.XB_StatusHWPositiveLimitSet ? Color.Red : Color.Gray;

            lblFatalFollowingError.BackColor    /**/= _selectedServo.XB_ErrFatalFollowingError ? Color.Red : Color.Gray;
            lblAmpFaultError.BackColor          /**/= _selectedServo.XB_ErrAmpFaultError ? Color.Red : Color.Gray;
            lblI2TAmpFaultError.BackColor       /**/= _selectedServo.XB_ErrI2TAmpFaultError ? Color.Red : Color.Gray;
            lblCommonAlarmError.BackColor       /**/= _selectedServo.XB_ErrCommonAlarm ? Color.Red : Color.Gray;

            lblTargetPosMoveComplete.BackColor  /**/= _selectedServo.XB_TargetPosMoveComplete ? Color.Red : Color.Gray;

            //Jog Move
            UpdateInterfaceButton(btnJogMinusCmd, _selectedServo.YB_JogMinusMove);
            UpdateInterfaceButton(btnJogPlusCmd, _selectedServo.YB_JogPlusMove);

            txtJogSpdAck.Text = _selectedServo.XF_JogSpeed.vFloat.ToString("F2");

            //Ptp move
            txtPtpPosAck.Text = _selectedServo.XF_TargetPosition.vFloat.ToString("F2");
            txtPtpSpdAck.Text = _selectedServo.XF_TargetSpeed.vFloat.ToString("F2");
            txtPtpAccAck.Text = _selectedServo.XF_TargetAccTime.vFloat.ToString("F2");

            UpdateInterfaceButton(btnPtpCmd, _selectedServo.YB_TargetPosMoveCmd);
            UpdateInterfaceButton(btnPtpAck, _selectedServo.XB_TargetPosMoveAck);

            txtPtpStep.Text = _selectedServo.PtpStep.ToString();
            #endregion
        }

        private void UpdateInterfaceButton(Button btn, PlcAddr addr)
        {
            btn.BackColor      /**/= addr.vBit == true ? Color.Red : Color.Transparent;
            btn.Text           /**/= addr.GetPlcAddressBitString();
        }

        private void InitPmacStatusDgv(DataGridView dgv, bool isEqpToPmac)
        {
            // 항목 설정
            string[,] equipToPmac = new string[,]
            {
                {GG.boChinaLanguage? "设备自动状态" : "설비 자동 상태",        _equip.PMac.YB_IsAutoMode.GetPlcAddressBitString()},
                {GG.boChinaLanguage? "设备 Heavy Alarm 状态" : "설비 중알람 상태",      _equip.PMac.YB_CheckAlarmStatus.GetPlcAddressBitString()},
                {GG.boChinaLanguage? "上流PIO 中" : "상류 PIO 중",          _equip.PMac.YB_UpperInterfaceWorking.GetPlcAddressBitString()},
                {GG.boChinaLanguage? "下流 PIO 中" : "하류 PIO 중",          _equip.PMac.YB_LowerInterfaceWorking.GetPlcAddressBitString()},
            };

            string[,] pmacToEquip = new string[,]
            {
                {"PMAC READY", _equip.PMac.XB_PmacReady.GetPlcAddressBitString()},
                {"PMAC ALIVE", _equip.PMac.XB_PmacAlive.GetPlcAddressBitString()},
                {"ECAT ALIVE", _equip.PMac.XB_EcatCommAlive.GetPlcAddressBitString()},
                {GG.boChinaLanguage? "检查进行中" : "검사 진행중", _equip.PMac.XB_InspRunning.GetPlcAddressBitString()},
                {GG.boChinaLanguage? "Review 进行中" : "리뷰 진행중", _equip.PMac.XB_ReviewRunning.GetPlcAddressBitString()},
            };

            // Set Dgv
            dgv.Rows.Clear();
            string[,] selected = isEqpToPmac ? equipToPmac : pmacToEquip;
            dgv.Rows.Add(selected.GetLength(0));
            dgv.RowHeadersWidth = 190;
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("맑은 고딕", 8.25f);
            for (int iter = 0; iter < dgv.Rows.Count; ++iter)
            {
                dgv.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("맑은 고딕", 8.25f);
                dgv.Rows[iter].HeaderCell.Value = selected[iter, 0];
                dgv.Rows[iter].Cells[0].Value = isEqpToPmac ? "C → P" : "P → C";
                dgv.Rows[iter].Cells[1].Value = selected[iter, 1];
            }

            // Set dgv Event
            if (isEqpToPmac)
            {
                dgv.CellContentClick += (s, e) =>
                {
                    if (e.ColumnIndex == 1)
                    {
                        switch (e.RowIndex)
                        {
                            case 0: _equip.PMac.YB_IsAutoMode.vBit = !_equip.PMac.YB_IsAutoMode.vBit; break;
                            case 1: _equip.PMac.YB_CheckAlarmStatus.vBit = !_equip.PMac.YB_CheckAlarmStatus.vBit; break;
                            case 2: _equip.PMac.YB_UpperInterfaceWorking.vBit = !_equip.PMac.YB_UpperInterfaceWorking.vBit; break;
                            case 3: _equip.PMac.YB_LowerInterfaceWorking.vBit = !_equip.PMac.YB_LowerInterfaceWorking.vBit; break;
                        }
                        dgv.ClearSelection();
                    }
                };
            }
            else
            {
                dgv.CellContentClick += (s, e) =>
                {
                    if (e.ColumnIndex == 1)
                    {
                        switch (e.RowIndex)
                        {
                            case 0: _equip.PMac.XB_PmacReady.vBit = !_equip.PMac.XB_PmacReady.vBit; break;
                            case 1: _equip.PMac.XB_PmacAlive.vBit = !_equip.PMac.XB_PmacAlive.vBit; break;
                            case 3: _equip.PMac.XB_EcatCommAlive.vBit = !_equip.PMac.XB_EcatCommAlive.vBit; break;
                            case 4: _equip.PMac.XB_InspRunning.vBit = !_equip.PMac.XB_InspRunning.vBit; break;
                            case 5: _equip.PMac.XB_ReviewRunning.vBit = !_equip.PMac.XB_ReviewRunning.vBit; break;
                        }
                        dgv.ClearSelection();
                    }
                };
            }
        }

        private void btnSomeMotorTitle_Click(object sender, EventArgs e)
        {
            ButtonLabel btnl = sender as ButtonLabel;
            _selectedServo = Equip.Motors[Array.IndexOf(_motorBtnls, btnl)];

            btnl3ndStageX.Selected = (btnl3ndStageX == btnl);
            btnl3ndInspY.Selected = (btnl3ndInspY == btnl);
            btnl3ndAlignTheta.Selected = (btnl3ndAlignTheta == btnl);
        }

        //Pmac Enable Mode
        private const int _pmacCmdEnablePasswd = 1234;
        private bool _isPmacCmdEnable = false;

        private void btnPmacCmdEnable_DoubleClick(object sender, EventArgs e)
        {
            if ((ModifierKeys & Keys.Control) != Keys.Control) return;

            FormPWInput login = new FormPWInput(null);
            login.ShowDialog();

            if (login.DialogResult == DialogResult.OK)
            {
                if (login.Passwd == _pmacCmdEnablePasswd.ToString())
                {
                    CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "转换成PMAC 可以命令 Mode了." : "PMAC 명령 가능 모드로 전환되었습니다.");
                    login.Close();
                    _isPmacCmdEnable = true;
                    lblPmacCmdEnable.BackColor = Color.Green;
                    setPmacBtnEnable(true);
                    return;
                }
                CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "密码错误" : "비밀번호가 틀립니다.");
            }
            else if (login.DialogResult == DialogResult.Cancel)
                return;
        }
        private void setPmacBtnEnable(bool isBtnEnable)
        {
            btnMotorInterlockOff.Enabled
                = btnEcatReconnectCmd.Enabled
                = btnPmacReset.Enabled
                = btnPmacImmediateStop.Enabled
                = btnInspStopCmd.Enabled
                = btnReviewStopCmd.Enabled

                = btnHomeCmd.Enabled
                = btnJogPlusCmd.Enabled
                = btnJogMinusCmd.Enabled
                = btnSetJogSpeed.Enabled
                = btnPtpCmd.Enabled
                = isBtnEnable;

            lblPmacCmdEnable.BackColor = isBtnEnable == true ? Color.Green : Color.Red;
        }

        //Axis Data
        private void btnHomeCmd_Click(object sender, EventArgs e)
        {
            _selectedServo.YB_HomeCmd.vBit = !_selectedServo.YB_HomeCmd.vBit;
        }
        private void btnJogSomeCmd_MouseDown(object sender, MouseEventArgs e)
        {
            if ((sender as Button) == btnJogMinusCmd)
            {
                _selectedServo.YB_JogMinusMove.vBit = true;
                _selectedServo.YB_JogPlusMove.vBit = false;
            }
            else
            {
                _selectedServo.YB_JogMinusMove.vBit = false;
                _selectedServo.YB_JogPlusMove.vBit = true;
            }
        }
        private void btnJogSomeCmd_MouseUp(object sender, MouseEventArgs e)
        {
            _selectedServo.YB_JogMinusMove.vBit = false;
            _selectedServo.YB_JogPlusMove.vBit = false;
        }


        //Position Data grid view 사용으로 인한 미사용 기능 들
        private ButtonLabel[] _motorBtnls;

        private void btnPos1MoveCmd_Click(object sender, EventArgs e)
        {
            //int iPos = Array.IndexOf(btnPos0MoveCmd, (Button)sender);
            //_selectedServo.YB_Position0MoveCmd[iPos].vBit = !_selectedServo.YB_Position0MoveCmd[iPos].vBit;
        }
        #endregion

        #region PC
        private string HSUpperName = @"";
        private string HSLowerName = @" ";
        private string ContactName = @"";
        private string HotLine = @"";

        private List<UcrlIoItem> _lstPcViewItems = new List<UcrlIoItem>();

        private void btnlCtoI_Click(object sender, EventArgs e)
        {
            btnlCtoI.Selected = false;
            btnlItoC.Selected = false;
            btnlItoCAlarm.Selected = false;

            ((ButtonLabel)sender).Selected = true;

            FillMonitorIspt();
            //FillMonitorRv();
        }

        private void UpdatePC()
        {
            pnl3rdInsp.SuspendLayout();
            _lstPcViewItems.ForEach(f =>
            {
                f.UpdateUI();
            });
            pnl3rdInsp.ResumeLayout();
        }

        public void SetPcView(int iPos, string name, PlcAddr addr)
        {
            _lstPcViewItems[iPos].MonAddress = addr;
            _lstPcViewItems[iPos].SetName(name);
        }
        private void FillMonitorIspt()
        {
            //재정리 필요
            if (btnlCtoI.Selected == true)
            {
                lblConToIspt1.Text = "CONTROL → INSPECTION / REVIEW";
                lblConToIspt2.Text = "CONTROL → INSPECTION / REVIEW";

                ResetPcIoItem(true, 'Y');
            }
            else if (btnlItoC.Selected == true)
            {
                lblConToIspt1.Text = "INSPECTION / REVIEW → CONTROL";
                lblConToIspt2.Text = "INSPECTION / REVIEW → CONTROL";

                ResetPcIoItem(true, 'X');
            }
            else if (btnlItoCAlarm.Selected == true)
            {
                lblConToIspt1.Text = "INSPECTION / REVIEW → CONTROL";
                lblConToIspt2.Text = "INSPECTION / REVIEW → CONTROL";

                ResetPcIoItem(true, '_', true);
            }
        }

        private void ResetPcIoItem(bool empty1Per8, char findFirstChar, bool getAlarmIO = false)
        {
            for (int i = 0; i < _lstPcViewItems.Count; ++i)
                SetPcView(i, "", null);

            Type type = typeof(IsptAddrB);
            FieldInfo[] fields = type.GetFields();
            PlcAddr temp = new PlcAddr(PlcMemType.B, 0);
            int rowOffset = 0, oldAddr = -1, rowIdx = 0, bitOffset = -1;
            if (empty1Per8 == true)
                rowIdx = -2;
            else
                rowOffset = -8;
            foreach (FieldInfo f in fields)
            {
                var val = f.GetValue(null);
                temp = val as PlcAddr;

                bool isAlarmIO = (f.Name.Length > 5 && f.Name.Substring(f.Name.Length - 5) == "Alarm");
                if (temp != null
                    &&
                    getAlarmIO
                    ? isAlarmIO
                    : (f.Name.Length > 0 && f.Name[0] == findFirstChar && isAlarmIO == false)
                    )
                {
                    if (oldAddr != temp.Addr)
                    {
                        oldAddr = temp.Addr;
                        if (empty1Per8 == true)
                        {
                            rowOffset = rowIdx + 2;
                            bitOffset = -1;
                        }
                        else
                            rowOffset += 8;
                    }
                    bitOffset = empty1Per8 == true ? ++bitOffset : temp.Bit;
                    rowIdx = rowOffset + bitOffset;
                    if (0 <= rowIdx && rowIdx < 32)
                        SetPcView(rowIdx, f.Name, temp);
                }
            }
        }

        #endregion

        #region EFEM
        private void EFEMStatInit()
        {
            SetLabelColumnWidth(pGridEFEMStat, 200);
        }
        private void SetLabelColumnWidth(PropertyGrid grid, int width)
        {
            if (grid == null)
                throw new ArgumentNullException("grid");

            // get the grid view
            Control view = (Control)grid.GetType().GetField("gridView", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(grid);

            // set label width
            FieldInfo fi = view.GetType().GetField("labelWidth", BindingFlags.Instance | BindingFlags.NonPublic);
            fi.SetValue(view, width);

            // refresh
            view.Invalidate();
        }
        private void EFEMPIOInit()
        {
            ucrlPIOSignalSendLPM1.PIOInitialize("LPM1", GG.Equip.Efem.Robot.PioSendLpm1);
            ucrlPIOSignalSendLPM2.PIOInitialize("LPM2", GG.Equip.Efem.Robot.PioSendLpm2);
            ucrlPIOSignalSendAligner.PIOInitialize("ALIGNER", GG.Equip.Efem.Robot.PioSendAligner);
            ucrlPIOSignalSendAVI.PIOInitialize("AVI", GG.Equip.Efem.Robot.PioSendAVI);

            ucrlPIOSignalRecvLPM1.PIOInitialize("LPM1", GG.Equip.Efem.Robot.PioRecvLpm1);
            ucrlPIOSignalRecvLPM2.PIOInitialize("LPM2", GG.Equip.Efem.Robot.PioRecvLpm2);
            ucrlPIOSignalRecvAligner.PIOInitialize("ALIGNER", GG.Equip.Efem.Robot.PioRecvAligner);
            ucrlPIOSignalRecvAVI.PIOInitialize("AVI", GG.Equip.Efem.Robot.PioRecvAVI);
        }
        private void UpdateEFEMStat()
        {
            if(_selectedabP == tabpEFEMsStatus)
            {
                pGridEFEMStat.SelectedObject = GG.Equip.Efem.Status;
                pGridRobotStat.SelectedObject = GG.Equip.Efem.Robot.Status;
                pGridAligner.SelectedObject = GG.Equip.Efem.Aligner.Status;
            }
            else if(_selectedabP == tabpLoadPortStatus)
            {
                pGridLPM1.SelectedObject = GG.Equip.Efem.LoadPort1.Status;
                pGridLPM2.SelectedObject = GG.Equip.Efem.LoadPort2.Status;
            }
            else if(_selectedabP == tabpEFEMPIO)
            {
                ucrlPIOSignalSendLPM1.UpdateUI();
                ucrlPIOSignalSendLPM2.UpdateUI();
                ucrlPIOSignalSendAligner.UpdateUI();
                ucrlPIOSignalSendAVI.UpdateUI();

                ucrlPIOSignalRecvLPM1.UpdateUI();
                ucrlPIOSignalRecvLPM2.UpdateUI();
                ucrlPIOSignalRecvAligner.UpdateUI();
                ucrlPIOSignalRecvAVI.UpdateUI();    
            }
        }
        #endregion

        #region 미사용
        private void InitGlassDataDgv(DataGridView dgv)
        {
            dgv.SuspendLayout();
            dgv.Rows.Clear();
            FieldInfo[] fInfo = typeof(GlassInfoBackup).GetFields();
            int row = 0;

            foreach (PropertyInfo property in typeof(GlassInfoBackup).GetProperties())
            {
                foreach (object obj in property.GetCustomAttributes(false))
                {
                    IniAttribute iniAttri = obj as IniAttribute;
                    if (iniAttri != null)
                    {
                        dgv.Rows.Add(new string[] { row++.ToString(), iniAttri.Key, "" });
                    }
                }
            }
            dgv.ResumeLayout();
        }

        private void btnlShowAlarm_Click(object sender, EventArgs e)
        {
        }
        private void btnShowMain_Click(object sender, EventArgs e)
        {
        }

        private void UpdatePIO()
        {

        }
        private void rdbSelectGlassOrg_CheckedChanged(object sender, EventArgs e)
        {
            ShowGlassDataByCondition();
        }

        private void ShowGlassData(GlassInfo glsInfo)
        {


        }
        private void btnlShowGlassData_Click(object sender, EventArgs e)
        {
            ButtonLabel btnl = sender as ButtonLabel;

            btnl.Selected = true;

            ShowGlassDataByCondition();
        }
        private void ShowGlassDataByCondition()
        {
            //ButtonLabel[] btns = new ButtonLabel[]{
            //    btnShowLoadingGlass,btnShowResultJudgeGlass,btnShowUnloadingGlass,btnShowScrapGlass
            //};
            //ButtonLabel btnl = btns.FirstOrDefault(b => b.Selected);
            //if (btnl == btnShowLoadingGlass) ShowGlassData(rdbSelectGlassRear.Checked ? _equip.OrgRearLoadingGlassInfo : _equip.FrontLoadingGlassInfo);
            //else if (btnl == btnShowResultJudgeGlass) ShowGlassData(rdbSelectGlassRear.Checked ? _equip.OrgRearResultJudgeGlassInfo : _equip.FrontResultJudgeGlassInfo);
            //else if (btnl == btnShowUnloadingGlass) ShowGlassData(rdbSelectGlassRear.Checked ? _equip.OrgRearUnloadingGlassInfo : _equip.FrontUnloadingGlassInfo);
            //else if (btnl == btnShowScrapGlass) ShowGlassData(rdbSelectGlassRear.Checked ? _equip.OrgRearScripUnscripGlassInfo : _equip.FrontScripUnscripGlassInfo);
        }

        /*
         *  default dgv : Name, Interface dir, Position Button
         *  
         */
        private void UpdatePmacPosiStateDgv(ServoMotorPmac motor, DataGridView dgv)
        {
            for (int i = 0; i < 1; i++)
            {
                DataGridViewButtonColumn btnColumns = new DataGridViewButtonColumn();

                btnColumns.HeaderText = "Posi #" + (i + 1);
                dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("맑은 고딕", 8.25f);
                btnColumns.FlatStyle = FlatStyle.Flat;
                dgv.Columns.Add(btnColumns);
            }
        }

        private void btnlPIOSubMenu_Click(object sender, EventArgs e)
        {

        }
        private void btnMoveOrgCmd_Click(object sender, EventArgs e)
        {
            //_selectedServo.YB_StartPointCmd.vBit = !_selectedServo.YB_StartPointCmd.vBit;
        }
        private void btnlShowLoadingGlassData_Click(object sender, EventArgs e)
        {
        }
        public void SetPioHSView(int iPos, string name, PlcAddr addr1, PlcAddr addr2)
        {
            //_lstPcViewItems[iPos].SetText(name);
            //_lstPcViewItems[iPos].MonAddress1 = addr1;
            //_lstPcViewItems[iPos].MonAddress2 = addr2;
        }
        private void FillMonitorPIOHS(bool isUpper)
        {

        }
        private void FillMonitorPIOCT(bool isUpper)
        {

        }
        private void btnlShowPC_Click(object sender, EventArgs e)
        {
            //btnShowCtrl2All.Selected = false;
            //btnShowCtrl2Insp.Selected = false;
            //btnShowCtrl2Revi.Selected = false;
            //btnShowInsp2Ctrl.Selected = false;
            //btnShowInspErr2Ctrl.Selected = false;
            //btnShowRevi2Ctrl.Selected = false;
            //btnShowReviErr2Ctrl.Selected = false;

            //if (btnShowCtrl2All == (ButtonLabel)sender) { SetMonitorPC(CTRL2ALL); }
            //else if (btnShowCtrl2Insp == (ButtonLabel)sender) { SetMonitorPC(CTRL2INSP); }
            //else if (btnShowCtrl2Revi == (ButtonLabel)sender) { SetMonitorPC(CTRL2REVI); }
            //else if (btnShowInsp2Ctrl == (ButtonLabel)sender) { SetMonitorPC(INSP2CTRL); }
            //else if (btnShowRevi2Ctrl == (ButtonLabel)sender) { SetMonitorPC(REVI2CTRL); }
            //else if (btnShowInspErr2Ctrl == (ButtonLabel)sender) { SetMonitorPC(INSPERR2CTRL); }
            //else if (btnShowReviErr2Ctrl == (ButtonLabel)sender) { SetMonitorPC(REVIERR2CTRL); }

            //((ButtonLabel)sender).Selected = true;
        }

        private void btnlTopMost_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
        }
        #endregion

        private void btnMotorInterlockOff_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btnMotorInterlockOff)
            {
                Equip.PMac.YB_EquipStatusMotorInterlockOff.vBit = !Equip.PMac.YB_EquipStatusMotorInterlockOff.vBit;
            }
            else if (btn == btnEcatReconnectCmd)
            {
                Equip.PMac.YB_EcatReconnectCmd.vBit = !Equip.PMac.YB_EcatReconnectCmd.vBit;
            }
            else if (btn == btnPmacReset)
            {
                Equip.PMac.YB_PmacResetCmd.vBit = !Equip.PMac.YB_PmacResetCmd.vBit;
            }
            else if (btn == btnPmacImmediateStop)
            {
                Equip.PMac.YB_EmsCmd.vBit = !Equip.PMac.YB_EmsCmd.vBit;
            }
            else if (btn == btnInspStopCmd)
            {
                Equip.PMac.YB_InspStopCmd.vBit = !Equip.PMac.YB_InspStopCmd.vBit;
            }
            else if (btn == btnReviewStopCmd)
            {
                Equip.PMac.YB_ReviewStopCmd.vBit = !Equip.PMac.YB_ReviewStopCmd.vBit;
            }
        }

        private void btnSetJogSpeed_Click(object sender, EventArgs e)
        {
            float spd = 0;
            float.TryParse(txtJogSpdCmd.Text, out spd);
            _selectedServo.YF_JogSpeed.vFloat = spd;
            txtJogSpdCmd.Text = spd.ToString();
        }

        private void btnPtpCmd_Click(object sender, EventArgs e)
        {
            _selectedServo.YB_TargetPosMoveCmd.vBit = !_selectedServo.YB_TargetPosMoveCmd.vBit;
        }

        private void btnSetPtpParam_Click(object sender, EventArgs e)
        {
            float spd = 0, pos = 0, acc = 0;

            float.TryParse(txtPtpSpdCmd.Text, out spd);
            float.TryParse(txtPtpPosCmd.Text, out pos);
            float.TryParse(txtPtpAccCmd.Text, out acc);

            txtPtpSpdCmd.Text = spd.ToString();
            txtPtpPosCmd.Text = pos.ToString();
            txtPtpAccCmd.Text = acc.ToString();

            _selectedServo.YF_TargetSpeed.vFloat = spd;
            _selectedServo.YF_TargetPosition.vFloat = pos;
            _selectedServo.YF_TargetAccTime.vFloat = acc;
        }
        TabPage _selectedabP;
        private void tabCtrlEFEM_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage curPage = (sender as TabControl).SelectedTab;

            _selectedabP = curPage;
        }

        private void btnPin_Click(object sender, EventArgs e)
        {
            if(btnPin.FlatStyle == FlatStyle.Flat)
            {
                btnPin.FlatStyle = FlatStyle.Standard;
                this.TopMost = false;
            }
            else
            {
                btnPin.FlatStyle = FlatStyle.Flat;
                this.TopMost = true;
            }

        }
    }
}
