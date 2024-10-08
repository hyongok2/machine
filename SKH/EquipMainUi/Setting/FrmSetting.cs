using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EquipMainUi.Struct;
using EquipMainUi.Struct.Detail;
using EquipMainUi.Struct.Detail.HSMS;
using EquipMainUi.Monitor;
using System.Diagnostics;
using EquipMainUi.ConvenienceClass;
using EquipMainUi.Monitor;
using System.IO.Ports;
using System.Reflection;
using Dit.Framework.Ini;
using EquipMainUi.UserMessageBoxes;
using System.IO;
using Dit.Framework.UI.UserComponent;
using EquipMainUi.Struct.TransferData;
using Dit.Framework.Xml;
using System.Xml.Serialization;
using EquipMainUi.Struct.Detail.EFEM;
using Microsoft.Win32;
using System.Net;
using EquipMainUi.Setting.TransferData;
using EquipMainUi.Struct.Detail.EziStep;

namespace EquipMainUi.Setting
{
    public partial class FrmSetting : Form
    {
        private Equipment _equip;
        private int _curServoIdx = 0;
        private List<ServoMotorPmac> _lstServoMotor = new List<ServoMotorPmac>();
        private List<string> _lstMotorSetTitles = new List<string>();
        private List<string> _lstDelaySetTitles = new List<string>();
        private TextBox _focusedBox;
        private TabPage _showPage;
        private EM_LV_LST _curLevel;
        private EM_LV_LST _checkedLevel;
        FrmWaferHistory frmWaferHistory;

        public FrmSetting(Equipment equip)
        {
            InitializeComponent();

            ExtensionUI.AddClickEventLog(this);

            InitGlassDataDgv(dgvScrapGlassData);

            dgvDefinedPos.AutoGenerateColumns = false;
            dgvDefinedPos.EnableSpanColumn.Add(0);
            dgvEziDefinedPos.AutoGenerateColumns = false;
            dgvEziDefinedPos.EnableSpanColumn.Add(0);

            AlarmMgr.Instance.LstvSettingAlarm = dgvAlarmSetting;

            _equip = equip;
            _lstDelaySetTitles = new List<string>(new string[] { "PC Delay Setting", "Server Delay Setting", "Review Delay Setting" });
            _lstServoMotor = new List<ServoMotorPmac>(equip.Motors);
            
            labelLevel.Text = string.Format("LEVEL : {0}", LoginMgr.Instance.LoginedUser.Level);

            //tabSetting.TabPages.Remove(tabp1St_ETCCtrl);
            //tabSetting.TabPages.Remove(tabp1St_ETCSet);
            
            tabCtrl_Setting.TabPages.Remove(tabp2Nd_ScrapUnscrap);
            tabCtrl_Setting.TabPages.Remove(tabp2Nd_LampSetting);

            //tabCtrl_PCSetting.TabPages.Remove(tabp2Nd_Analog);
            //tabCtrl_PCSetting.TabPages.Remove(tabp2Nd_EFUSetting);

            //tabCtrl_ParamSetting.TabPages.Remove(tabp2Nd_InnerWork);
            //tabCtrl_ParamSetting.TabPages.Remove(tabp2Nd_WorkingLightOnOff);

            tmrUiUpdate.Start();
            FillPMacSetting(equip.Motors);
            _showPage = tabp2Nd_MotorSetting;

            pGridAdcTemp.SelectedObject = _equip.CtrlSetting.AnalogSet;

            selectedBtn = btnSelectDataAll;

            InitSerialPort();

            InitContextMenu();

            UpdateDataInfo();
            if (GG.TestMode == true)
            {
                btnCreateWafer.Visible = true;
            }

            SetWaferHistoryForm();

            if(GG.IsDitPreAligner == false)
            {
                tabCtrl_PCSetting.ItemSize = new Size(200, 30);
                tabCtrl_PCSetting.TabPages.RemoveAt(1);
            }
            else
            {
                tabCtrl_PCSetting.ItemSize = new Size(166, 30);
                FillEziSetting(equip.StepMotors);
            }
            ChangeChinaLanguage();
        }

        private void ChangeChinaLanguage()
        {
            if (GG.boChinaLanguage)
            {
                // Motor Setting
                dgvDefinedPos.Columns[0].HeaderText = "Motor名称";       // 모터이름
                dgvDefinedPos.Columns[1].HeaderText = "位置名称";       // 위치이름
                dgvDefinedPos.Columns[2].HeaderText = "位置";       // 위치
                dgvDefinedPos.Columns[3].HeaderText = "速度";       // 속도
                dgvDefinedPos.Columns[4].HeaderText = "加速度";       // 가속도
                dgvDefinedPos.Columns[5].HeaderText = "位置下线点";       // 위치 하한
                dgvDefinedPos.Columns[6].HeaderText = "位置上线点";       // 위치 상한
                dgvDefinedPos.Columns[7].HeaderText = "速度上线点";       // 속도 상한
                dgvDefinedPos.Columns[8].HeaderText = "加速度下线点";       // 가속도 하한
                dgvDefinedPos.Columns[9].HeaderText = "加速度上线点";       // 가속도 상한
                dgvDefinedPos.Columns[10].HeaderText = "移动";      // 이동

                // Align Motor Setting
                dgvEziDefinedPos.Columns[0].HeaderText = "Motor 名称";       // 모터이름
                dgvEziDefinedPos.Columns[1].HeaderText = "位置名称";       // 위치이름
                dgvEziDefinedPos.Columns[2].HeaderText = "位置";       // 위치
                dgvEziDefinedPos.Columns[3].HeaderText = "速度";       // 속도
                dgvEziDefinedPos.Columns[4].HeaderText = "加速度";       // 가속도
                dgvEziDefinedPos.Columns[5].HeaderText = "位置下线点";       // 위치 하한
                dgvEziDefinedPos.Columns[6].HeaderText = "位置上线点";       // 위치 상한
                dgvEziDefinedPos.Columns[7].HeaderText = "速度上线点";       // 속도 상한
                dgvEziDefinedPos.Columns[8].HeaderText = "加速度下线点";       // 가속도 하한
                dgvEziDefinedPos.Columns[9].HeaderText = "加速度上线点";       // 가속도 상한
                dgvEziDefinedPos.Columns[10].HeaderText = "移动";      // 이동

                // Analog
                btnTempSetSave.Text = "保存温度设置";                          // 온도 설정 저장

                // Alarm Setting
                label20.Text = "鼠标右键(右) Click 时, 可变更选择的报警权限(Heavy,Warn,Unused)";	// * 우 클릭 시 해당 알람 설정 변경 권한 변경 가능(User 등급 제외)
                button4.Text = "保存报警设置";	// 알람 설정 저장

                // EFU Setting
                label137.Text = "单元 : RPM";		// 단위 : RPM
                groupBox4.Text = "Port 设定";	// Port설정
                label179.Text = "正常";		// 정상
                label180.Text = "电源不良, 过电流, Motor 问题 发生";		// 전원불량, 과전류, Motor이상
                label181.Text = "发生通讯问题";		// 통신이상
                //
                label146.Text = "当前值";		// 상태
                label143.Text = "当前值";		// 현재값
                label145.Text = "设定值";		// 설정값
                label264.Text = "值确认";		// 확인
                //
                label433.Text = "当前值";		// 상태
                label263.Text = "当前值";		// 현재값
                label266.Text = "设定值";		// 설정값
                label265.Text = "值确认";		// 확인
                //
                label463.Text = "当前值";		// 상태
                label460.Text = "当前值";		// 현재값
                label462.Text = "设定值";		// 설정값
                label461.Text = "值确认";		// 확인

                label12.Text = "(右) Click 鼠标右键, 操作";      // 아래에 우클릭 하여 조작

                tabp1St_ETCCtrl.Text = "ETC 操作";      // ETC 조작
                label277.Text = "前面";		        // 전면
                label276.Text = "背面";		        // 후면
                btnSave.Text = "Motor 设置储存";              // 모터 설정 저장

                label257.Text = "温度可设置范围 (设备 Off 设置)";                 // 온도 설정 가능 범위 (설비 Off 설정)
                label241.Text = "■ 定义的位置";                 // ■ 정의된 위치
                btnEziMotorSettingSave.Text = "Motor 设置储存";     // 모터 설정 저장

                button3.Text = "Delay 设置储存";          // Delay 설정 저장
            }
        }

        private void SetWaferHistoryForm()
        {
            frmWaferHistory = new FrmWaferHistory();
            frmWaferHistory.FormBorderStyle = FormBorderStyle.None;
            frmWaferHistory.Name = "FrmEfemTest";
            frmWaferHistory.TopLevel = false;
            tabp2Nd_History.Controls.Add(frmWaferHistory);
            frmWaferHistory.Parent = tabp2Nd_History;
            frmWaferHistory.Text = string.Empty;
            frmWaferHistory.ControlBox = false;
            frmWaferHistory.Dock = DockStyle.Fill;
            frmWaferHistory.Show();
        }

        private void SettingForm_Closing(object sender, FormClosingEventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FrmMonitor")
                {
                    return;
                }
            }
            frmWaferHistory.Hide();
        }

        public void SetStartPage(string pageStr, int idx = 0)
        {
            switch (pageStr)
            {
                case "Door":
                    tabSetting.SelectedIndex = 2;
                    tabCtrl_ParamSetting.SelectedIndex = 1;
                    _showPage = tabp2Nd_DoorSetting;
                    break;
                case "WorkingLight":
                    tabSetting.SelectedIndex = 2;
                    tabCtrl_ParamSetting.SelectedIndex = 2;
                    LoadCheckState();
                    _showPage = tabp2Nd_WorkingLightOnOff;
                    break;
                case "AlarmSetting":
                    tabSetting.SelectedIndex = 0;
                    tabCtrl_PCSetting.SelectedIndex = GG.IsDitPreAligner ? 4 : 3;
                    _showPage = tabp2Nd_AlarmSetting;
                    LoadAlarmList();
                    dgvAlarmSetting.CurrentCell = dgvAlarmSetting.Rows[idx].Cells[0];
                    break;

            }
        }
        
        private void tab1St_SelectedIndex(object sender, EventArgs e)
        {
            TabPage curPage = (sender as TabControl).SelectedTab;

            if (curPage == tabp1St_PCSet)
            {
                tabCtrl_PCSetting.SelectTab(tabp2Nd_MotorSetting);
                FillPMacSetting(_equip.Motors);
                _showPage = tabp2Nd_MotorSetting;

            }
            else if (curPage == tabp1St_ETCSet)
            {
                //LoadGlassData_InEdit(_equip.LoadingWaferInfo);
            }
            else if (curPage == tabp1St_ETCCtrl)
            {
                tabCtrl_ParamSetting.SelectTab(tabp2Nd_DoorSetting);
                _showPage = tabp2Nd_InnerWork;
            }
        }
        
        private void tmrUiUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.Visible == false)
                    tmrUiUpdate.Stop();
                if (_showPage == tabp2Nd_UnitSetting)
                {
                    UpdateSerialStatus();
                    UpdateEFU(true, true, true);
                }

                if (_showPage == tabp2Nd_MotorSetting)
                    UpdateMotorParam();
                if (_showPage == tabp2Nd_DoorSetting)
                    UpdateDoorState();
                if (_showPage == tabp2Nd_Analog)
                {
                    UpdateAirState();
                }
                if (_showPage == tabp2Nd_LampSetting)
                    UpdateLampTime();
                if (_showPage == tabp2Nd_WorkingLightOnOff)
                    UpdateWorkLightState();
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

        private void UpdateMotorParam()
        {
            txtXLoadRateAck.Text = _equip.StageX.XF_LoadRate.vInt.ToString();
            txtYLoadRateAck.Text = _equip.StageY.XF_LoadRate.vInt.ToString();
        }



        #region PC Setting
        private void tab2NdPCSetting_SelectedIndex(object sender, EventArgs e)
        {
            TabPage curPage = (sender as TabControl).SelectedTab;

            if (curPage == tabp2Nd_MotorSetting)
                FillPMacSetting(_equip.Motors);
            if (curPage == tabp2ND_EziMotorSetting)
                FillEziSetting(_equip.StepMotors);
            //else if (curPage == tabp2Nd_Analog)
            //{
            //    LoadSettingTemp();
            //}
            //else if (curPage == tabp2Nd_AirTemp)
            //{

            //}
            else if (curPage == tabp2Nd_DelaySetting)
                btnl3ndDelaySubMemu_Click(btnl3rdDelayCtrl, null);
            else if (curPage == tabp2Nd_UnitSetting)
            {
                cmbEFUPorts.Items.Clear();
                foreach (string portName in SerialPort.GetPortNames())
                    cmbEFUPorts.Items.Add(portName);
                cmbEFUPorts.Text = _equip.EFUCtrler.CurrentPort;

                UpdateEFU(true, true, true);
                LoadConnSetting();
            }
            else if (curPage == tabp2Nd_AlarmSetting)
                LoadAlarmList();
            else if (curPage == tabp2Nd_WorkingLightOnOff)
                LoadCheckState();

            _showPage = curPage;
        }

        //Motor Setting
        private void FillPMacSetting(ServoMotorPmac[] axis)
        {
            dgvDefinedPos.Rows.Clear();
            int newRowIdx = 0;
            foreach (ServoMotorPmac axisIdx in axis)
            {
                newRowIdx = dgvDefinedPos.Rows.Add(new string[] {
                            axisIdx.Name,
                            "HOME",
                            "-"
                        });
                SetReadOnlyCell(dgvDefinedPos, "axisName", newRowIdx);
                SetReadOnlyCell(dgvDefinedPos, "posName", newRowIdx);
                SetReadOnlyCell(dgvDefinedPos, "pos", newRowIdx);
                SetReadOnlyCell(dgvDefinedPos, "speed", newRowIdx);
                SetReadOnlyCell(dgvDefinedPos, "acc", newRowIdx);
                SetReadOnlyCell(dgvDefinedPos, "posNLimit", newRowIdx);
                SetReadOnlyCell(dgvDefinedPos, "posPLimit", newRowIdx);
                SetReadOnlyCell(dgvDefinedPos, "spdLimit", newRowIdx);
                SetReadOnlyCell(dgvDefinedPos, "accNLimit", newRowIdx);
                SetReadOnlyCell(dgvDefinedPos, "accPLimit", newRowIdx);

                for (int iPos = 0; iPos < axisIdx.PositionCount; ++iPos)
                {
                    int iRow = dgvDefinedPos.Rows.Add(new string[]
                            {
                                axisIdx.Name,
                            axisIdx.Setting.LstServoPosiInfo[iPos].Name,
                            axisIdx.Setting.LstServoPosiInfo[iPos].Position.ToString(),
                            axisIdx.Setting.LstServoPosiInfo[iPos].Speed.ToString(),
                            axisIdx.Setting.LstServoPosiInfo[iPos].Accel.ToString(),
                            axisIdx.SoftMinusLimit.ToString(),
                            axisIdx.SoftPlusLimit.ToString(),
                            axisIdx.SoftSpeedLimit.ToString(),
                            axisIdx.SoftAccelMinusLimit.ToString(),
                            axisIdx.SoftAccelPlusLimit.ToString(),
                            });
                    SetReadOnlyCell(dgvDefinedPos, "axisName", iRow);
                    SetReadOnlyCell(dgvDefinedPos, "posName", iRow);
                    SetReadOnlyCell(dgvDefinedPos, "posNLimit", iRow);
                    SetReadOnlyCell(dgvDefinedPos, "posPLimit", iRow);
                    SetReadOnlyCell(dgvDefinedPos, "spdLimit", iRow);
                    SetReadOnlyCell(dgvDefinedPos, "accNLimit", iRow);
                    SetReadOnlyCell(dgvDefinedPos, "accPLimit", iRow);

                    if (iPos < 1)
                        SetReadOnlyCell(dgvDefinedPos, "pos", iRow);
                }

                if (axisIdx.MotorType == EmMotorType.Linear)
                {
                    if (axisIdx == _equip.StageX)
                        txtXLoadRate.Text = axisIdx.Setting.LoadRate.ToString();
                    else if (axisIdx == _equip.StageY)
                        txtYLoadRate.Text = axisIdx.Setting.LoadRate.ToString();
                }
            }
        }
        private void FillEziSetting(StepMotorEzi[] axis)
        {
            dgvEziDefinedPos.Rows.Clear();
            int newRowIdx = 0;
            foreach (StepMotorEzi axisIdx in axis)
            {
                newRowIdx = dgvEziDefinedPos.Rows.Add(new string[] {
                            axisIdx.Name,
                            "HOME",
                            "-"
                        });
                SetReadOnlyCell(dgvEziDefinedPos, "EziaxisName", newRowIdx);
                SetReadOnlyCell(dgvEziDefinedPos, "EziposName", newRowIdx);
                SetReadOnlyCell(dgvEziDefinedPos, "Ezipos", newRowIdx);
                SetReadOnlyCell(dgvEziDefinedPos, "Ezispeed", newRowIdx);
                SetReadOnlyCell(dgvEziDefinedPos, "Eziacc", newRowIdx);
                SetReadOnlyCell(dgvEziDefinedPos, "EziposNLimit", newRowIdx);
                SetReadOnlyCell(dgvEziDefinedPos, "EziposPLimit", newRowIdx);
                SetReadOnlyCell(dgvEziDefinedPos, "EzispdLimit", newRowIdx);
                SetReadOnlyCell(dgvEziDefinedPos, "EziaccNLimit", newRowIdx);
                SetReadOnlyCell(dgvEziDefinedPos, "EziaccPLimit", newRowIdx);

                for (int iPos = 0; iPos < axisIdx.PositionCount; ++iPos)
                {
                    int iRow = dgvEziDefinedPos.Rows.Add(new string[]
                            {
                                axisIdx.Name,
                            axisIdx.Setting.LstServoPosiInfo[iPos].Name,
                            axisIdx.Setting.LstServoPosiInfo[iPos].Position.ToString(),
                            axisIdx.Setting.LstServoPosiInfo[iPos].Speed.ToString(),
                            axisIdx.Setting.LstServoPosiInfo[iPos].Accel.ToString(),
                            axisIdx.SoftMinusLimit.ToString(),
                            axisIdx.SoftPlusLimit.ToString(),
                            axisIdx.SoftSpeedLimit.ToString(),
                            axisIdx.SoftAccelMinusLimit.ToString(),
                            axisIdx.SoftAccelPlusLimit.ToString(),
                            });
                    SetReadOnlyCell(dgvEziDefinedPos, "EziaxisName", iRow);
                    SetReadOnlyCell(dgvEziDefinedPos, "EziposName", iRow);
                    SetReadOnlyCell(dgvEziDefinedPos, "EziposNLimit", iRow);
                    SetReadOnlyCell(dgvEziDefinedPos, "EziposPLimit", iRow);
                    SetReadOnlyCell(dgvEziDefinedPos, "EzispdLimit", iRow);
                    SetReadOnlyCell(dgvEziDefinedPos, "EziaccNLimit", iRow);
                    SetReadOnlyCell(dgvEziDefinedPos, "EziaccPLimit", iRow);

                    if (iPos < 1)
                        SetReadOnlyCell(dgvEziDefinedPos, "Ezipos", iRow);
                }
            }
        }
        private void SetReadOnlyCell(DataGridView dgv, string colName, int rowIdx)
        {
            dgv[colName, rowIdx].ReadOnly = true;
            dgv[colName, rowIdx].Style.BackColor = Color.DarkGray;
        }
        private ServoMotorPmac GetSelectedServo(ServoMotorPmac[] servoSet, int dgvRowIdx, out int posIdx)
        {
            int startIdx = 0;
            foreach (ServoMotorPmac servo in servoSet)
            {
                startIdx += servo.PositionCount + 1;
                if (dgvRowIdx < startIdx)
                {
                    posIdx = dgvRowIdx - (startIdx - servo.PositionCount - 1);
                    return servo;
                }
            }
            posIdx = 0;
            return servoSet[0];
        }
        private StepMotorEzi GetSelectedServo(StepMotorEzi[] servoSet, int dgvRowIdx, out int posIdx)
        {
            int startIdx = 0;
            foreach (StepMotorEzi servo in servoSet)
            {
                startIdx += servo.PositionCount + 1;
                if (dgvRowIdx < startIdx)
                {
                    posIdx = dgvRowIdx - (startIdx - servo.PositionCount - 1);
                    return servo;
                }
            }
            posIdx = 0;
            return servoSet[0];
        }
        private bool SavePMacSetting(ServoMotorPmac[] servoSet, out PMacServoSetting[] servoSettingBuff, out string[] errorState)
        {
            servoSettingBuff = new PMacServoSetting[servoSet.Length];
            errorState = new string[servoSet.Length];
            int posIdx = 0, homeItemOffset = 1;
            bool isError = false;

            foreach (DataGridViewRow row in dgvDefinedPos.Rows)
            {
                if (row.Cells[0].Value == null)
                    continue;

                ServoMotorPmac curServo = GetSelectedServo(servoSet, row.Index, out posIdx);

                if (posIdx == 0)
                    servoSettingBuff[Array.IndexOf(servoSet, curServo)] = (PMacServoSetting)curServo.Setting.Clone();

                double point = 0;
                double speed = 0;
                double accel = 0;

                if (posIdx != 0)
                {
                    double.TryParse(row.Cells["pos"].Value != null ? row.Cells["pos"].Value.ToString() : "0", out point);
                    double.TryParse(row.Cells["speed"].Value != null ? row.Cells["speed"].Value.ToString() : "0", out speed);
                    double.TryParse(row.Cells["acc"].Value != null ? row.Cells["acc"].Value.ToString() : "0", out accel);
                }
                isError = false;
                #region range interlock                
                if (point > curServo.SoftPlusLimit && 0 != posIdx)
                {
                    errorState[Array.IndexOf(servoSet, curServo)] += string.Format("{2}의 {0} 위치값이 최대값({1})보다 큽니다.\r\n",
                        curServo.Setting.LstServoPosiInfo[posIdx - homeItemOffset].Name, curServo.SoftPlusLimit, curServo.Name);
                    isError = true;
                }
                if (point < curServo.SoftMinusLimit && 0 != posIdx)
                {
                    errorState[Array.IndexOf(servoSet, curServo)] += string.Format("{2}의 {0} 위치값이 최소값({1})보다 작습니다.\r\n",
                        curServo.Setting.LstServoPosiInfo[posIdx - homeItemOffset].Name, curServo.SoftMinusLimit, curServo.Name);
                    isError = true;
                }
                if (speed > curServo.SoftSpeedLimit)
                {
                    errorState[Array.IndexOf(servoSet, curServo)] += string.Format("{2}의 {0} 이동 속도 최대값({1})보다 큽니다.\r\n",
                        0 == posIdx ? "HOME" : curServo.Setting.LstServoPosiInfo[posIdx - homeItemOffset].Name, curServo.SoftSpeedLimit, curServo.Name);
                    isError = true;
                }
                if (accel > curServo.SoftAccelPlusLimit && 0 != posIdx)
                {
                    errorState[Array.IndexOf(servoSet, curServo)] += string.Format("{2}의 {0} 가속도값이 최대값({1})보다 큽니다.\r\n",
                        curServo.Setting.LstServoPosiInfo[posIdx - homeItemOffset].Name, curServo.SoftAccelPlusLimit, curServo.Name);
                    isError = true;
                }
                if (accel < curServo.SoftAccelMinusLimit && 0 != posIdx)
                {
                    errorState[Array.IndexOf(servoSet, curServo)] += string.Format("{2}의 {0} 가속도값이 최소값({1})보다 작습니다.\r\n",
                        curServo.Setting.LstServoPosiInfo[posIdx - homeItemOffset].Name, curServo.SoftAccelMinusLimit, curServo.Name);
                    isError = true;
                }
                #endregion
                if (isError == false)
                {
                    if (0 == posIdx)
                    {
                        // jys::홈속도 조정 안됨. _equip.Motors[axisIdx].Setting.HOME_SPEED = speed;
                    }
                    else
                    {
                        servoSettingBuff[Array.IndexOf(servoSet, curServo)].LstServoPosiInfo[posIdx - homeItemOffset].Position = (float)point;
                        servoSettingBuff[Array.IndexOf(servoSet, curServo)].LstServoPosiInfo[posIdx - homeItemOffset].Speed = (float)speed;
                        servoSettingBuff[Array.IndexOf(servoSet, curServo)].LstServoPosiInfo[posIdx - homeItemOffset].Accel = (float)accel;
                    }
                }
            }

            int xRate, yRate;
            if (int.TryParse(txtXLoadRate.Text, out xRate) == false
                || xRate < 50 || xRate > 100)
            {
                errorState[0] += string.Format("X LoadRate 값 이상 (50~100 허용)");
                txtXLoadRate.Text = "0";
            }
            else
            {
                servoSettingBuff[0].LoadRate = xRate;
            }
            if (int.TryParse(txtYLoadRate.Text, out yRate) == false
             || yRate < 50 || yRate > 100)
            {
                errorState[1] += string.Format("Y LoadRate 값 이상 (50~100 허용)");
                txtYLoadRate.Text = "0";
            }
            else
            {
                servoSettingBuff[1].LoadRate = yRate;
            }

            return true;
        }
        private bool SaveEziSetting(StepMotorEzi[] servoSet, out PMacServoSetting[] servoSettingBuff, out string[] errorState)
        {
            servoSettingBuff = new PMacServoSetting[servoSet.Length];
            errorState = new string[servoSet.Length];
            int posIdx = 0, homeItemOffset = 1;
            bool isError = false;

            foreach (DataGridViewRow row in dgvEziDefinedPos.Rows)
            {
                if (row.Cells[0].Value == null)
                    continue;

                StepMotorEzi curServo = GetSelectedServo(servoSet, row.Index, out posIdx);

                if (posIdx == 0)
                    servoSettingBuff[Array.IndexOf(servoSet, curServo)] = (PMacServoSetting)curServo.Setting.Clone();

                double point = 0;
                double speed = 0;
                double accel = 0;

                if (posIdx != 0)
                {
                    double.TryParse(row.Cells["Ezipos"].Value != null ? row.Cells["Ezipos"].Value.ToString() : "0", out point);
                    double.TryParse(row.Cells["Ezispeed"].Value != null ? row.Cells["Ezispeed"].Value.ToString() : "0", out speed);
                    double.TryParse(row.Cells["Eziacc"].Value != null ? row.Cells["Eziacc"].Value.ToString() : "0", out accel);
                }
                isError = false;
                #region range interlock                
                if (point > curServo.SoftPlusLimit && 0 != posIdx)
                {
                    errorState[Array.IndexOf(servoSet, curServo)] += string.Format("{2}의 {0} 위치값이 최대값({1})보다 큽니다.\r\n",
                        curServo.Setting.LstServoPosiInfo[posIdx - homeItemOffset].Name, curServo.SoftPlusLimit, curServo.Name);
                    isError = true;
                }
                if (point < curServo.SoftMinusLimit && 0 != posIdx)
                {
                    errorState[Array.IndexOf(servoSet, curServo)] += string.Format("{2}의 {0} 위치값이 최소값({1})보다 작습니다.\r\n",
                        curServo.Setting.LstServoPosiInfo[posIdx - homeItemOffset].Name, curServo.SoftMinusLimit, curServo.Name);
                    isError = true;
                }
                if (speed > curServo.SoftSpeedLimit)
                {
                    errorState[Array.IndexOf(servoSet, curServo)] += string.Format("{2}의 {0} 이동 속도 최대값({1})보다 큽니다.\r\n",
                        0 == posIdx ? "HOME" : curServo.Setting.LstServoPosiInfo[posIdx - homeItemOffset].Name, curServo.SoftSpeedLimit, curServo.Name);
                    isError = true;
                }
                if (accel > curServo.SoftAccelPlusLimit && 0 != posIdx)
                {
                    errorState[Array.IndexOf(servoSet, curServo)] += string.Format("{2}의 {0} 가속도값이 최대값({1})보다 큽니다.\r\n",
                        curServo.Setting.LstServoPosiInfo[posIdx - homeItemOffset].Name, curServo.SoftAccelPlusLimit, curServo.Name);
                    isError = true;
                }
                if (accel < curServo.SoftAccelMinusLimit && 0 != posIdx)
                {
                    errorState[Array.IndexOf(servoSet, curServo)] += string.Format("{2}의 {0} 가속도값이 최소값({1})보다 작습니다.\r\n",
                        curServo.Setting.LstServoPosiInfo[posIdx - homeItemOffset].Name, curServo.SoftAccelMinusLimit, curServo.Name);
                    isError = true;
                }
                #endregion
                if (isError == false)
                {
                    if (0 == posIdx)
                    {
                        // jys::홈속도 조정 안됨. _equip.Motors[axisIdx].Setting.HOME_SPEED = speed;
                    }
                    else
                    {
                        servoSettingBuff[Array.IndexOf(servoSet, curServo)].LstServoPosiInfo[posIdx - homeItemOffset].Position = (float)point;
                        servoSettingBuff[Array.IndexOf(servoSet, curServo)].LstServoPosiInfo[posIdx - homeItemOffset].Speed = (float)speed;
                        servoSettingBuff[Array.IndexOf(servoSet, curServo)].LstServoPosiInfo[posIdx - homeItemOffset].Accel = (float)accel;
                    }
                }
            }

            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_equip.IsHomePositioning || _equip.EquipRunMode == EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<动作中>\n(设备动作中. 无法变更设备设置.)" : "인터락<동작중>\n설비가 동작중입니다. 설비 설정을 변경 할 수 없습니다.");
                return;
            }
            if (LoginMgr.Instance.LoginedUser.Level == EM_LV_LST.USER)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<User 权限>\n(User权限无法变更设备设置 .)" : "인터락<유저 권한>\nUser 권한으로는 설비 설정을 변경 할 수 없습니다.");
                return;
            }

            FrmTimerMsg _timeMsg = new FrmTimerMsg(2, "Setting", "Save..");
            _timeMsg.Show();

            string resultMsg = string.Empty;
            string[] errorState = null;
            PMacServoSetting[] servoSettingBuff = null;

            if (SavePMacSetting(_equip.Motors, out servoSettingBuff, out errorState) == false) return;

            foreach (ServoMotorPmac servo in _equip.Motors)
            {
                if (errorState[Array.IndexOf(_equip.Motors, servo)] == null)
                {
                    servo.Setting = (PMacServoSetting)servoSettingBuff[Array.IndexOf(_equip.Motors, servo)].Clone();
                    servo.Setting.Save();

                    if (servo.MotorType == EmMotorType.Linear)
                        servo.YF_LoadRate.vInt = servo.Setting.LoadRate;
                }
                else
                    resultMsg += errorState[Array.IndexOf(_equip.Motors, servo)];
            }
            _equip.PMac.StartSaveToPmac(_equip);
            int errorCnt = errorState.Count(f => f != null);
            if (errorCnt != errorState.Length)
                PMacServoSetting.AutoBackup();
            FillPMacSetting(_equip.Motors);

            while (_equip.PMac.IsDoneSaveToUMac(_equip) == false)
            {
                System.Threading.Thread.Sleep(100);
                Application.DoEvents();
            }
            _timeMsg.DoClose();
                        
            _timeMsg = new FrmTimerMsg(2, "Setting", "Cim Report");
            _timeMsg.Show();
            if (GG.Equip.HsmsPc.StartCommand(GG.Equip, EmHsmsPcCommand.ECID_CHANGE, 0) == false)
            {
                errorCnt++;
                resultMsg += GG.boChinaLanguage ? "[To CIM] ECID CHANGE CMD 失败" : "[To CIM] ECID CHANGE CMD 실패";
            }
            while (GG.Equip.HsmsPc.IsCommandAck(EmHsmsPcCommand.ECID_CHANGE) == false)
            {
                System.Threading.Thread.Sleep(100);
                Application.DoEvents();
            }
            _timeMsg.DoClose();

            if (errorCnt == 0)
                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "设置值应用到了 PMAC." : "설정값을 PMAC에 적용하였습니다.");
            else if (errorCnt == errorState.Length)
                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "储存设置值时发生了以下问题\n\n" : "설정값 저장 중 다음의 문제가 발생했습니다\n\n" + resultMsg);
            else
                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "设置值储存中有问题的 轴/作业除外, 只储存了一部分.\n\n" : "설정값 저장 중 문제 있는 축/작업 제외, 일부만 저장되었습니다.\n\n" + resultMsg);
            return;
        }
        private void btnEziMotorSettingSave_Click(object sender, EventArgs e)
        {
            if (_equip.IsHomePositioning || _equip.EquipRunMode == EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<动作中>\n(设备动作中. 无法变更设备设置.)" : "인터락<동작중>\n설비가 동작중입니다. 설비 설정을 변경 할 수 없습니다.");
                return;
            }
            if (LoginMgr.Instance.LoginedUser.Level == EM_LV_LST.USER)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<User 权限>\n(User权限无法变更设备设置 .)" : "인터락<유저 권한>\nUser 권한으로는 설비 설정을 변경 할 수 없습니다.");
                return;
            }

            FrmTimerMsg _timeMsg = new FrmTimerMsg(2, "Setting", "Save..");
            _timeMsg.Show();

            string resultMsg = string.Empty;
            string[] errorState = null;
            PMacServoSetting[] servoSettingBuff = null;

            if (SaveEziSetting(_equip.StepMotors, out servoSettingBuff, out errorState) == false) return;

            foreach (StepMotorEzi servo in _equip.StepMotors)
            {
                if (errorState[Array.IndexOf(_equip.StepMotors, servo)] == null)
                {
                    servo.Setting = (PMacServoSetting)servoSettingBuff[Array.IndexOf(_equip.StepMotors, servo)].Clone();
                    servo.Setting.Save();
                }
                else
                    resultMsg += errorState[Array.IndexOf(_equip.StepMotors, servo)];
            }
            int errorCnt = errorState.Count(f => f != null);
            if (errorCnt != errorState.Length)
                PMacServoSetting.AutoBackup();
            FillEziSetting(_equip.StepMotors);

            while (_equip.PMac.IsDoneSaveToUMac(_equip) == false)
            {
                System.Threading.Thread.Sleep(100);
                Application.DoEvents();
            }
            _timeMsg.DoClose();

            //_timeMsg = new FrmTimerMsg(2, "Setting", "Cim Report");
            //_timeMsg.Show();
            //if (GG.Equip.HsmsPc.StartCommand(GG.Equip, EmHsmsPcCommand.ECID_CHANGE, 0) == false)
            //{
            //    errorCnt++;
            //    resultMsg += "[To CIM] ECID CHANGE CMD 실패";
            //}
            //while (GG.Equip.HsmsPc.IsCommandAck(EmHsmsPcCommand.ECID_CHANGE) == false)
            //{
            //    System.Threading.Thread.Sleep(100);
            //    Application.DoEvents();
            //}
            //_timeMsg.DoClose();

            if (errorCnt == 0)
                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "储存了设置值" : "설정값을 저장 하였습니다");
            else if (errorCnt == errorState.Length)
                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "储存设置值时发生了以下问题\n\n" : "설정값 저장 중 다음의 문제가 발생했습니다\n\n" + resultMsg);
            else
                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "设置值储存中有问题的 轴/作业除外, 只储存了一部分.\n\n" : "설정값 저장 중 문제 있는 축/작업 제외, 일부만 저장되었습니다.\n\n" + resultMsg);
            return;
        }


        public bool WriteToPMac()
        {
            
            return true;
        }
        public void SDFSD()
        {
            //Todo:이거 구현해야됨
        }
        private void dgvDefinedPos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_equip.IsHomePositioning || _equip.EquipRunMode == EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<动作中>\n(设备动作中. 无法变更设备设置.)" : "인터락<동작중>\n설비가 동작중입니다. 설비 설정을 변경 할 수 없습니다.");
                return;
            }
            if (e.ColumnIndex == 5)
            {
                if (e.RowIndex == 0)
                    //_lstServoMotor[_curServoIdx].GoHomeOrPositionOne(_equip);
                    _lstServoMotor[_curServoIdx].GoHome(_equip);
                else
                    _lstServoMotor[_curServoIdx].MovePosition(_equip, e.RowIndex - 1);
                //Console.WriteLine(e.RowIndex);
            }
        }

        //Analog
        private void btnTempSetSave_Click(object sender, EventArgs e)
        {
            if (_equip.IsHomePositioning || _equip.EquipRunMode == EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<动作中>\n(设备动作中. 无法变更设备设置.)" : "인터락<동작중>\n설비가 동작중입니다. 설비 설정을 변경 할 수 없습니다.");
                return;
            }

            EquipMainUi.Setting.PcCtrlSetting.AnalogSetting a = pGridAdcTemp.SelectedObject as EquipMainUi.Setting.PcCtrlSetting.AnalogSetting;
            _equip.CtrlSetting.AnalogSet = a;
            CheckMgr.AddCheckMsg(true, "Analog Temp Setting saved");
        }
        private void LoadDAValue()
        {
            //txtAirSetValue1.Text = _equip.AnalogSetting.DA1_CH1_WriteData.ToString();
            //txtAirSetValue2.Text = _equip.AnalogSetting.DA1_CH2_WriteData.ToString();
            //txtAirSetValue3.Text = _equip.AnalogSetting.DA1_CH3_WriteData.ToString();
            //txtAirSetValue4.Text = _equip.AnalogSetting.DA1_CH4_WriteData.ToString();
            //txtAirSetValue5.Text = _equip.AnalogSetting.DA1_CH5_WriteData.ToString();
        }
        private void btnDA1SetValue_Click(object sender, EventArgs e)
        {
            float value = 0;
            Button btn = sender as Button;
       
            Logger.Log.AppendLine(LogLevel.Info, "{0} 설정값 {1}", btn.Name, value);
        }
        Func<float, string> convert = delegate (float value)
        {
            return value != -9999 ? value.ToString() : "Error";
        };
        private void UpdateVacuumState()
        {
            
        }
        private void UpdateAirState()
        {
            pGridAdc1.SelectedObject = new ADCBundleToPGrid(_equip.ADC);

            //lblAD1ErrState.BackColor = _equip.ADC.Adc1.XB_ErrorStatusFlag ? Color.Red : Color.Green;
        }
        private void UpdateAirFloatingZoneState()
        {
        }
        private void btnADReset_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == btnAD1Reset)
                _equip.ADC.Adc1.ErrorReset();
            else if (btn == btnADTabTempReset)
                _equip.ADC.Temperature.ErrorReset();
        }
        private void ADSetting_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                foreach (Form openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FrmAnalogSetting")
                    {
                        if (openForm.WindowState == FormWindowState.Minimized)
                        {
                            openForm.WindowState = FormWindowState.Normal;
                        }
                        openForm.Activate();
                        return;
                    }
                }

                FrmAnalogSetting ff = new FrmAnalogSetting(_equip);
                ff.Show();
            }
        }
        //Delay Setting
        private void btnl3ndDelaySubMemu_Click(object sender, EventArgs e)
        {
            ButtonLabel btnl = sender as ButtonLabel;

            List<ButtonLabel> lst = new List<ButtonLabel>()
            {
                btnl3rdDelayCtrl,
                btnl3rdDelayServer,
                btnl3rdDelayMotor,
                btnl3rdDelayCim,
            };
            lst.ForEach(a =>
            {
                a.Selected = false;
                if (a != btnl)
                    a.BackColor = Color.Transparent;
            });

            if (btnl == btnl3rdDelayCtrl)
            {
                btnl3rdDelayCtrl.Selected = true;
                btnl3rdDelayCtrl.BackColor = Color.AliceBlue;
                pDelayGrid.SelectedObject = _equip.CtrlSetting.Ctrl;
            }
            else if (btnl == btnl3rdDelayServer)
            {
                btnl3rdDelayServer.Selected = true;
                btnl3rdDelayServer.BackColor = Color.AliceBlue;
                pDelayGrid.SelectedObject = _equip.CtrlSetting.Insp;
            }
            else if (btnl == btnl3rdDelayMotor)
            {
                btnl3rdDelayMotor.Selected = true;
                btnl3rdDelayMotor.BackColor = Color.AliceBlue;
                pDelayGrid.SelectedObject = _equip.CtrlSetting.Motor;
            }
            else if (btnl == btnl3rdDelayCim)
            {
                btnl3rdDelayCim.Selected = true;
                btnl3rdDelayCim.BackColor = Color.AliceBlue;
                pDelayGrid.SelectedObject = _equip.CtrlSetting.Hsms;
            }
        }
        private void btnDelaySettingValueSave_Click(object sender, EventArgs e)
        {
            if (_equip.IsHomePositioning || _equip.EquipRunMode == EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<动作中>\n(设备动作中. 无法变更设备设置.)" : "인터락<동작중>\n설비가 동작중입니다. 설비 설정을 변경 할 수 없습니다.");
                return;
            }
            if (LoginMgr.Instance.LoginedUser.Level == EM_LV_LST.USER)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<User 权限>\n(User权限无法变更设备设置 .)" : "인터락<유저 권한>\nUser 권한으로는 설비 설정을 변경 할 수 없습니다.");
                return;
            }
            if (btnl3rdDelayCtrl.Selected == true)
            {
                _equip.CtrlSetting.Ctrl = pDelayGrid.SelectedObject as PcCtrlSetting.CtrlSetting;
                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "Control 设置储存已完毕" : "Control 설정 저장을 완료하였습니다.");
            }
            else if (btnl3rdDelayServer.Selected == true)
            {
                _equip.CtrlSetting.Insp = pDelayGrid.SelectedObject as PcCtrlSetting.InspServerSetting;
                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "Server 设置储存已完成." : "Server 설정 저장을 완료하였습니다.");
            }
            else if (btnl3rdDelayMotor.Selected == true)
            {
                _equip.CtrlSetting.Motor = pDelayGrid.SelectedObject as PcCtrlSetting.MotorSetting;
                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "Motor设置储存已完成 ." : "Motor 설정 저장을 완료하였습니다.");
            }
            else if (btnl3rdDelayCim.Selected == true)
            {
                _equip.CtrlSetting.Hsms = pDelayGrid.SelectedObject as PcCtrlSetting.HsmsSetting;
                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "CIM 设置储存已完成." : "CIM 설정 저장을 완료하였습니다.");
            }

            _equip.CtrlSetting.Save();
            _equip.UpdateInstanceSetting();
        }
        private void GetDelayInfo(int nSelType, ref string[] titles, ref int[] values, ref Label[] lblTitles, ref TextBox[] txtValues)
        {

        }


        //Alarm Setting
        private void LoadAlarmList()
        {
            AlarmMgr.Instance.LoadAlarmSettingList(dgvAlarmSetting);
        }
        private void listAlarm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 3) return;

            if (e.RowIndex < 0) return;

            if (dgvAlarmSetting.Rows[e.RowIndex].Cells[1].Value == null) return;

            if (dgvAlarmSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.LightGray /* && LoginMgr.Instance.LoginedUser.Level == EM_LV_LST.USER*/)
            {
                 
            }
            else if (LoginMgr.Instance.LoginedUser.Level == EM_LV_LST.USER)
            {
                if (e.ColumnIndex == 3)
                {
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[3].Value = true;
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[4].Value = false;
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[5].Value = false;
                }
                else if (e.ColumnIndex == 4)
                {
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[3].Value = false;
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[4].Value = true;
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[5].Value = false;
                }
                else if (e.ColumnIndex == 5)
                {
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[3].Value = false;
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[4].Value = false;
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[5].Value = true;
                }
            }
            else
            {
                if (e.ColumnIndex == 3)
                {
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[3].Value = true;
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[4].Value = false;
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[5].Value = false;
                }
                else if (e.ColumnIndex == 4)
                {
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[3].Value = false;
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[4].Value = true;
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[5].Value = false;
                }
                else if (e.ColumnIndex == 5)
                {
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[3].Value = false;
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[4].Value = false;
                    dgvAlarmSetting.Rows[e.RowIndex].Cells[5].Value = true;
                }
            }
        }
        private void btnlAlarmSettingSave_Click(object sender, EventArgs e)
        {
            AlarmMgr.Instance.AlarmStateChange(dgvAlarmSetting);

            //if (LoginMgr.Instance.LoginedUser.Level == EM_LV_LST.USER)
            //{
            //    InterLockMgr.AddInterLock("인터락<유저 권한>\nUser 권한으로는 설비 설정을 변경 할 수 없습니다.");
            //    return;
            //}
            if (AlarmMgr.Instance.SaveAlarmINIFile(dgvAlarmSetting) == true)
            {
                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "Alarm 设置储存完毕" : "알람 설정 저장 완료");
            }
            else
            {
                CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "Alarm 设置储存失败" : "알람 설정 저장 실패");
            }
        }
        private void dgvAlarmSetting_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && LoginMgr.Instance.LoginedUser.Level != EM_LV_LST.USER)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Heavy만 설정 가능"));
                m.MenuItems.Add(new MenuItem("Warn까지 설정 가능"));
                m.MenuItems.Add(new MenuItem("Unused까지 설정 가능"));

                int row = dgvAlarmSetting.HitTest(e.X, e.Y).RowIndex;

                if (row >= 0)
                {
                    //    m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                }

                switch (AlarmMgr.Instance.GetLevel(row))
                {
                    case EM_AL_LV.Heavy:
                        m.MenuItems[0].Checked = true;
                        break;
                    case EM_AL_LV.Warn:
                        m.MenuItems[1].Checked = true;
                        break;
                    case EM_AL_LV.Normal:
                        m.MenuItems[2].Checked = true;
                        break;
                }

                for (int i = 0; i < 3; i++)
                {
                    m.MenuItems[i].Click += new EventHandler(MenuItemClick);
                    m.MenuItems[i].Name = row.ToString();
                }

                m.Show(dgvAlarmSetting, new Point(e.X, e.Y));
            }
        }

        private void MenuItemClick(object sender, EventArgs e)
        {
            MenuItem m = sender as MenuItem;
            int row = int.Parse(m.Name);

            AlarmMgr.Instance.ChangeAlarmLevel(row, (EM_AL_LV)m.Index);
            LoadAlarmList();
        }

        //Unit Setting
        private void UpdateEFU(bool includeAlarm, bool includeCurRPM, bool includeSetRPM)
        {
            TextBox[] txtEFUStatus = {
                                         txtE1Status, txtE2Status, txtE3Status,
                                      };

            TextBox[] txtEFUCur = {
                                         txtE1Cur, txtE2Cur, txtE3Cur,
                                      };

            TextBox[] txtEFUIn = {
                                         txtE1InCheck, txtE2InCheck, txtE3InCheck,
                                      };

            Button[] btnEFUState = {
                                         btnEFU1, btnEFU2, btnEFU3,
                                      };

            List<EFU> efu = _equip.EFUCtrler.GetEFU();
            int EFUIndex = 0;
            foreach (EFU f in efu)
            {
                if (includeAlarm)
                {
                    txtEFUStatus[EFUIndex].Text = _equip.EFUCtrler.GetAlarmMsg(f.AlarmCode);

                    string strChinaLanguage1 = GG.boChinaLanguage ? "正常" : "정상";
                    string strChinaLanguage2 = GG.boChinaLanguage ? "发生通讯问题" : "통신이상";

                    string txtAlarmCode = txtEFUStatus[EFUIndex].Text;
                    if (txtAlarmCode == strChinaLanguage1)
                    {
                        btnEFUState[EFUIndex].BackColor = Color.Green;
                    }
                    else if (txtAlarmCode == strChinaLanguage2)
                    {
                        btnEFUState[EFUIndex].BackColor = Color.Black;
                    }
                    else
                    {
                        btnEFUState[EFUIndex].BackColor = Color.Red;
                    }
                }
                if (includeCurRPM)
                {
                    int value = Convert.ToInt32(f.CurRPM) * 10;
                    txtEFUCur[EFUIndex].Text = value.ToString();
                }
                if (includeSetRPM)
                {
                    int value = Convert.ToInt32(f.SettingRPM) * 10;
                    txtEFUIn[EFUIndex].Text = value.ToString();
                }
                EFUIndex++;
            }
        }
        private void btnlEFUSet_Click(object sender, EventArgs e)
        {
            if (LoginMgr.Instance.LoginedUser.Level == EM_LV_LST.USER)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<User 权限>\n(User权限无法变更设备设置 .)" : "인터락<유저 권한>\nUser 권한으로는 설비 설정을 변경 할 수 없습니다.");
                return;
            }
            Button btn = sender as Button;

            if (btn == btnEFUSet1)
                SetEFUSpeed(0, txtE1In);
            else if (btn == btnEFUSet2)
                SetEFUSpeed(1, txtE2In);
            else if (btn == btnEFUSet3)
                SetEFUSpeed(2, txtE3In);

        }
        private bool SetEFUSpeed(uint id, TextBox txtSetValue)
        {
            int speed = int.Parse(txtSetValue.Text != "" ? txtSetValue.Text : "10");
            if (speed < 10 || speed > 1400)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("可以 RPM 范围是 10~%d \n", EFUController.MAX_SPEED.ToString()) : string.Format("가능 RPM 범위는 10~%d 입니다\n", EFUController.MAX_SPEED.ToString()));
                return false;
            }

            _equip.EFUCtrler.SetSpeed(id, id, speed);

            return true;
        }
        private void btnEfuSettingInfo_Click(object sender, EventArgs e)
        {
            string retval = string.Format("현재 Port : {0}, \r\n\r\n-연결가능한 포트-\r\n", _equip.EFUCtrler.CurrentPort);
            MessageBox.Show(retval + EquipMainUi.Monitor.EFUController.ShowAvailablePort());
        }
        private void btnEFUOpen_Click(object sender, EventArgs e)
        {
            if (!_equip.EFUCtrler.IsOpen())
            {
                _equip.EFUCtrler.CurrentPort = cmbEFUPorts.Text;
                _equip.EFUCtrler.Start();
                _equip.InitSetting.EFUPort = _equip.EFUCtrler.CurrentPort;
            }
        }
        private void btnEFUClose_Click(object sender, EventArgs e)
        {
            _equip.EFUCtrler.Stop();
        }

        private void InitSerialPort()
        {
            SerialPort Comport = new SerialPort();
            int i;
            string strPort;

            cbRFID1Port.Items.Clear();
            cbBCR1Port.Items.Clear();
            cbBCR2Port.Items.Clear();

            foreach (var portName in SerialPort.GetPortNames())
            {
                try
                {
                    cbRFID1Port.Items.Add(portName);
                    cbBCR1Port.Items.Add(portName);
                    cbBCR2Port.Items.Add(portName);
                }
                catch (Exception)
                {

                }
                finally
                {
                }
            }
            LoadConnSetting();
        }

        private void LoadConnSetting()
        {
            cbRFID1Port.Text = _equip.InitSetting.RFR1Port;
            cbRFID2Port.Text = _equip.InitSetting.RFR2Port;
            cbBCR1Port.Text = _equip.InitSetting.BCR1Port;
            cbBCR2Port.Text = _equip.InitSetting.BCR2Port;
            txtOCRIP.Text = _equip.InitSetting.OcrIP.ToString();
            txtOCRPort.Text = _equip.InitSetting.OcrPort.ToString();
        }

        private void btnSerialOpen_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == btnRFID1Open)
            {
                if (_equip.Efem.LoadPort1.RFR.IsOpen() == true)
                {
                    CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "已经连接中" : "이미 연결 중입니다.");
                    return;
                }
                _equip.Efem.LoadPort1.RFR.Stop();

                if (_equip.Efem.LoadPort1.RFR.ReOpen(cbRFID1Port.Text) == true)
                {
                    _equip.InitSetting.RFR1Port = cbRFID1Port.Text;
                    CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "连接及储存成功" : "연결 및 저장 성공");
                    return;
                }

                CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "连接及储存失败" : "연결 및 저장 실패");
            }
            else if (btn == btnRFID1Close)
            {
                _equip.Efem.LoadPort1.RFR.Stop();
                CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "解除连接" : "연결 해제");
            }
            if (btn == btnRFID2Open)
            {
                if (_equip.Efem.LoadPort2.RFR.IsOpen() == true)
                {
                    CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "已经连接中" : "이미 연결 중입니다.");
                    return;
                }
                _equip.Efem.LoadPort2.RFR.Stop();

                if (_equip.Efem.LoadPort2.RFR.ReOpen(cbRFID2Port.Text) == true)
                {
                    _equip.InitSetting.RFR2Port = cbRFID2Port.Text;
                    CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "连接及储存成功" : "연결 및 저장 성공");
                    return;
                }

                CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "连接及储存失败" : "연결 및 저장 실패");
            }
            else if (btn == btnRFID2Close)
            {
                _equip.Efem.LoadPort2.RFR.Stop();
                CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "解除连接" : "연결 해제");
            }
            else if (btn == btnOCROpen)
            {
                if (_equip.OCR.IsConnected == true)
                {
                    CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "已经连接中" : "이미 연결 중입니다.");
                    return;
                }

                IPAddress ip;
                int port;

                if (IPAddress.TryParse(txtOCRIP.Text, out ip) == false
                    || int.TryParse(txtOCRPort.Text, out port) == false)
                {
                    CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "输入的 Data 异常" : "입력데이터 이상");
                    txtOCRIP.Text = "";
                    txtOCRPort.Text = "";
                    return;
                }


                _equip.OCR.Close();

                if (_equip.OCR.Open(ip.ToString(), port) == true)
                {
                    _equip.InitSetting.OcrIP = ip.ToString();
                    _equip.InitSetting.OcrPort = port;
                    CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "连接及储存成功" : "연결 및 저장 성공");
                    return;
                }

                CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "连接及储存失败" : "연결 및 저장 실패");
            }
            else if (btn == btnOCRClose)
            {
                _equip.OCR.Close();
                CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "解除连接" : "연결 해제");
            }
            else if (btn == btnBCR1Open)
            {
                if (_equip.BCR1.IsOpen() == true)
                {
                    CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "已经连接中" : "이미 연결 중입니다.");
                    return;
                }
                _equip.BCR1.Stop();

                if (_equip.BCR1.ReOpen(cbBCR1Port.Text) == true)
                {
                    _equip.InitSetting.BCR1Port = cbBCR1Port.Text;
                    CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "连接及储存成功" : "연결 및 저장 성공");
                    return;
                }

                CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "连接及储存失败" : "연결 및 저장 실패");
            }
            else if (btn == btnBCR1Open)
            {
                _equip.BCR1.Stop();
                CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "解除连接" : "연결 해제");
            }
            else if (btn == btnBCR2Open)
            {
                if (_equip.BCR2.IsOpen() == true)
                {
                   CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "已经连接中" : "이미 연결 중입니다.");
                   return;
                }
                _equip.BCR2.Stop();

                if (_equip.BCR2.ReOpen(cbBCR2Port.Text) == true)
                {
                    _equip.InitSetting.BCR2Port = cbBCR2Port.Text;
                    CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "连接及储存成功" : "연결 및 저장 성공");
                    return;
                }

                CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "连接及储存失败" : "연결 및 저장 실패");
            }
            else if (btn == btnBCR2Open)
            {
                _equip.BCR2.Stop();
                CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "解除连接" : "연결 해제");
            }
        }

        private void UpdateSerialStatus()
        {
            lblRFID1Status.BackColor = GG.Equip.Efem.LoadPort1.RFR.IsOpen() == true ? Color.Green : Color.Red;
            lblRFID1Status.Text = GG.Equip.Efem.LoadPort1.RFR.IsOpen() == true ? "Connected" : "Disconnected";
            lblRFID2Status.BackColor = GG.Equip.Efem.LoadPort2.RFR.IsOpen() == true ? Color.Green : Color.Red;
            lblRFID2Status.Text = GG.Equip.Efem.LoadPort2.RFR.IsOpen() == true ? "Connected" : "Disconnected";

            lblOCRStatus.BackColor = GG.Equip.OCR.IsConnected == true ? Color.Green : Color.Red;
            lblOCRStatus.Text = GG.Equip.OCR.IsConnected == true ? "Connected" : "Disconnected";

            lblBCR1Status.BackColor = GG.Equip.BCR1.IsOpen() == true ? Color.Green : Color.Red;
            lblBCR1Status.Text = GG.Equip.BCR1.IsOpen() == true ? "Connected" : "Disconnected";
            lblBCR2Status.BackColor = GG.Equip.BCR2.IsOpen() == true ? Color.Green : Color.Red;
            lblBCR2Status.Text = GG.Equip.BCR2.IsOpen() == true ? "Connected" : "Disconnected";
        }
        #endregion                                                                                                                                                                                                                                                                                    

        #region ETC Setting
        private void tab2NdSetting_SelectedIndex(object sender, EventArgs e)
        {
            TabPage curPage = (sender as TabControl).SelectedTab;
            if (curPage == tabp2Nd_LampSetting)
            {
                LoadLampTime();
            }
            else if(curPage == tabp2Nd_User)
            {
                UpdateUserPage();
            }
            else if(curPage == tabp2Nd_Data)
            {
                UpdateDataInfo();
            }
            _showPage = curPage;
        }
        
        //Scrap Unscrap
        //Glass Data
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
        private void LoadGlassData_InScrap(GlassInfo glsInfo)
        {
            SetGlassInfoToDgv(dgvScrapGlassData, glsInfo);
            #region old
            //             txtHGlassId_Scrap.Text = _equip.LoadingGlassInfo.HGlassID.ToString();
            //             txtEGlassId_Scrap.Text = _equip.LoadingGlassInfo.EGlassID.ToString();
            //             txtLotId_Scrap.Text = _equip.LoadingGlassInfo.LotID.ToString();
            //             txtBatchId_Scrap.Text = _equip.LoadingGlassInfo.BatchID.ToString();
            //             txtJobId_Scrap.Text = _equip.LoadingGlassInfo.JobID.ToString();
            //             txtPortId_Scrap.Text = _equip.LoadingGlassInfo.PortID.ToString();
            //             txtSlotId_Scrap.Text = _equip.LoadingGlassInfo.SlotID.ToString();
            //             txtProductType_Scrap.Text = _equip.LoadingGlassInfo.ProductType.ToString();
            //             txtProductKind_Scrap.Text = _equip.LoadingGlassInfo.ProductKind.ToString();
            //             txtProductId_Scrap.Text = _equip.LoadingGlassInfo.ProductID.ToString();
            //             txtRunspecId_Scrap.Text = _equip.LoadingGlassInfo.RunSpecID.ToString();
            //             txtLayerId_Scrap.Text = _equip.LoadingGlassInfo.LayerID.ToString();
            //             txtStepId_Scrap.Text = _equip.LoadingGlassInfo.StepID.ToString();
            //             txtPPID_Scrap.Text = _equip.LoadingGlassInfo.PPID.ToString();
            //             txtFlowId_Scrap.Text = _equip.LoadingGlassInfo.FlowID.ToString();
            //             txtGlassSize1_Scrap.Text = _equip.LoadingGlassInfo.GlassSize[0].ToString();
            //             txtGlassSize2_Scrap.Text = _equip.LoadingGlassInfo.GlassSize[1].ToString();
            //             txtGlassThickness_Scrap.Text = _equip.LoadingGlassInfo.GlassThickness.ToString();
            //             txtGlassState_Scrap.Text = _equip.LoadingGlassInfo.GlassState.ToString();
            //             txtGlassOrder_Scrap.Text = _equip.LoadingGlassInfo.GlassOrder.ToString();
            //             txtCommnet_Scrap.Text = _equip.LoadingGlassInfo.Comment.ToString();
            //             txtUseCount_Scrap.Text = _equip.LoadingGlassInfo.UseCount.ToString();
            //             txtJudgement_Scrap.Text = _equip.LoadingGlassInfo.Judgement.ToString();
            //             txtReasonCode_Scrap.Text = _equip.LoadingGlassInfo.ReasonCode.ToString();
            //             txtInsFlag_Scrap.Text = _equip.LoadingGlassInfo.InsFlag.ToString();
            //             txtEncFlag_Scrap.Text = _equip.LoadingGlassInfo.EncFlag.ToString();
            //             txtPrerunFlag_Scrap.Text = _equip.LoadingGlassInfo.PrerunFlag.ToString();
            //             txtTurnDir_Scrap.Text = _equip.LoadingGlassInfo.TurnDir.ToString();
            //             txtFlipState_Scrap.Text = _equip.LoadingGlassInfo.FlipState.ToString();
            //             txtWorkState_Scrap.Text = _equip.LoadingGlassInfo.WorkState.ToString();
            //             txtMultiUse_Scrap.Text = _equip.LoadingGlassInfo.MultiUse.ToString();
            //             txtPairGlassId_Scrap.Text = _equip.LoadingGlassInfo.PairGlassID.ToString();
            //             txtPairPPID_Scrap.Text = _equip.LoadingGlassInfo.PairPPID.ToString();
            //             txtOptionName1_Scrap.Text = _equip.LoadingGlassInfo.OptionName1.ToString();
            //             txtOptionValue1_Scrap.Text = _equip.LoadingGlassInfo.OptionValue1.ToString();
            //             txtOptionName2_Scrap.Text = _equip.LoadingGlassInfo.OptionName2.ToString();
            //             txtOptionValue2_Scrap.Text = _equip.LoadingGlassInfo.OptionValue2.ToString();
            //             txtOptionName3_Scrap.Text = _equip.LoadingGlassInfo.OptionName3.ToString();
            //             txtOptionValue3_Scrap.Text = _equip.LoadingGlassInfo.OptionValue3.ToString();
            //             txtOptionName4_Scrap.Text = _equip.LoadingGlassInfo.OptionName4.ToString();
            //             txtOptionValue4_Scrap.Text = _equip.LoadingGlassInfo.OptionValue4.ToString();
            //             txtOptionName5_Scrap.Text = _equip.LoadingGlassInfo.OptionName5.ToString();
            //             txtOptionValue5_Scrap.Text = _equip.LoadingGlassInfo.OptionValue5.ToString();
            //             if (_equip.LoadingGlassInfo.LotFlag == 0)
            //                 txtLotFlag.Text = "M";
            //             else if (_equip.LoadingGlassInfo.LotFlag == 1)
            //                 txtLotFlag.Text = "S";
            //             else if (_equip.LoadingGlassInfo.LotFlag == 2)
            //                 txtLotFlag.Text = "E";
            //             else if (_equip.LoadingGlassInfo.LotFlag == 3)
            //                 txtLotFlag.Text = "SE";
            //             else
            //                 txtLotFlag.Text = string.Empty;

            //--이하 원래 주석처리 돼있던 것
            // txtCSIF.Text = _equip.LoadingGlassInfo.CSIF.ToString();
            // txtAS.Text = _equip.LoadingGlassInfo.AS.ToString();
            //txtAPS.Text = _equip.LoadingGlassInfo.APS.ToString();
            //txtUniqueId.Text = _equip.LoadingGlassInfo.UniqueID.ToString();
            //txtBitSignal.Text = _equip.LoadingGlassInfo.BitSignal.ToString();
            #endregion
        }
        private void SetGlassInfoToDgv(DataGridView dgv, GlassInfo glsInfo)
        {
            int row = 0;
            dgv.Rows[row++].Cells[2].Value = glsInfo.CstID.ToString();
            dgv.Rows[row++].Cells[2].Value = glsInfo.RFReadCstID.ToString();
            dgv.Rows[row++].Cells[2].Value = glsInfo.WaferID.ToString();
            dgv.Rows[row++].Cells[2].Value = glsInfo.BarcodeReadWaferID.ToString();
            dgv.Rows[row++].Cells[2].Value = glsInfo.RecipeID.ToString();

            //dgv.Rows[row++].Cells[2].Value = glsInfo.PortID.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.SlotID.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.ProductType.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.ProductKind.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.ProductID.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.RunSpecID.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.LayerID.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.StepID.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.PPID.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.FlowID.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.GlassSize[0].ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.GlassSize[1].ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.GlassThickness.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.GlassState.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.GlassOrder.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.Comment.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.UseCount.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.Judgement.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.ReasonCode.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.InsFlag.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.EncFlag.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.PrerunFlag.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.TurnDir.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.FlipState.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.WorkState.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.MultiUse.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.PairGlassID.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.PairPPID.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.OptionName1.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.OptionValue1.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.OptionName2.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.OptionValue2.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.OptionName3.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.OptionValue3.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.OptionName4.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.OptionValue4.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.OptionName5.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.OptionValue5.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.CSIF.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.AS.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.APS.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.UniqueID.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.BitSignal.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.MainLotId.ToString();
            //dgv.Rows[row++].Cells[2].Value = _lotFlagTextToIndex.FirstOrDefault(key => key.Value == glsInfo.LotFlag).Key;
            //dgv.Rows[row++].Cells[2].Value = glsInfo.CstID.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.StagePnlAbort.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.FullPnlPosi.ToString();
            //dgv.Rows[row++].Cells[2].Value = glsInfo.OctaPnlPosi.ToString();
        }
        private void btnScrapUnscrap_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            //if (rdbSelectScrapGlassRear.Checked)
            //{
            //    _equip.ScrapUnscrapWaferInfo = _equip.LoadingWaferInfo;
            //    _equip.ScrapUnscrapWaferBak.Backup(_equip.LoadingWaferInfo);

            //    if (btn == btnScrap)
            //        _equip.HsmsPc.StartCommand(_equip, EmHsmsPcCommand.ORG_REAR_GLASS_SCRAP, null);
            //    else if (btn == btnUnscrap)
            //        _equip.HsmsPc.StartCommand(_equip, EmHsmsPcCommand.ORG_REAR_GLASS_UNSCRAP, null);
            //}
        }
        private void LoadGlassData_InEdit(GlassInfo glsInfo)
        {
            //SetGlassInfoToDgv(dgvEditGlassData, glsInfo);
            #region old
            //             txtHGlassId.Text = _equip.LoadingGlassInfo.HGlassID.ToString();
            //             txtEGlassId.Text = _equip.LoadingGlassInfo.EGlassID.ToString();
            //             txtLotId.Text = _equip.LoadingGlassInfo.LotID.ToString();
            //             txtBatchId.Text = _equip.LoadingGlassInfo.BatchID.ToString();
            //             txtJobId.Text = _equip.LoadingGlassInfo.JobID.ToString();
            //             txtPortId.Text = _equip.LoadingGlassInfo.PortID.ToString();
            //             txtSlotId.Text = _equip.LoadingGlassInfo.SlotID.ToString();
            //             txtProductType.Text = _equip.LoadingGlassInfo.ProductType.ToString();
            //             txtProductKind.Text = _equip.LoadingGlassInfo.ProductKind.ToString();
            //             txtProductId.Text = _equip.LoadingGlassInfo.ProductID.ToString();
            //             txtRunspecId.Text = _equip.LoadingGlassInfo.RunSpecID.ToString();
            //             txtLayerId.Text = _equip.LoadingGlassInfo.LayerID.ToString();
            //             txtStepId.Text = _equip.LoadingGlassInfo.StepID.ToString();
            //             txtPPID.Text = _equip.LoadingGlassInfo.PPID.ToString();
            //             txtFlowId.Text = _equip.LoadingGlassInfo.FlowID.ToString();
            //             txtGlassSize1.Text = _equip.LoadingGlassInfo.GlassSize[0].ToString();
            //             txtGlassSize2.Text = _equip.LoadingGlassInfo.GlassSize[1].ToString();
            //             txtGlassThickness.Text = _equip.LoadingGlassInfo.GlassThickness.ToString();
            //             txtGlassState.Text = _equip.LoadingGlassInfo.GlassState.ToString();
            //             txtGlassOrder.Text = _equip.LoadingGlassInfo.GlassOrder.ToString();
            //             txtCommnet.Text = _equip.LoadingGlassInfo.Comment.ToString();
            //             txtUseCount.Text = _equip.LoadingGlassInfo.UseCount.ToString();
            //             txtJudgement.Text = _equip.LoadingGlassInfo.Judgement.ToString();
            //             txtReasonCode.Text = _equip.LoadingGlassInfo.ReasonCode.ToString();
            //             txtInsFlag.Text = _equip.LoadingGlassInfo.InsFlag.ToString();
            //             txtEncFlag.Text = _equip.LoadingGlassInfo.EncFlag.ToString();
            //             txtPrerunFlag.Text = _equip.LoadingGlassInfo.PrerunFlag.ToString();
            //             txtTurnDir.Text = _equip.LoadingGlassInfo.TurnDir.ToString();
            //             txtFlipState.Text = _equip.LoadingGlassInfo.FlipState.ToString();
            //             txtWorkState.Text = _equip.LoadingGlassInfo.WorkState.ToString();
            //             txtMultiUse.Text = _equip.LoadingGlassInfo.MultiUse.ToString();
            //             txtPairGlassId.Text = _equip.LoadingGlassInfo.PairGlassID.ToString();
            //             txtPairPPID.Text = _equip.LoadingGlassInfo.PairPPID.ToString();
            //             txtOptionName1.Text = _equip.LoadingGlassInfo.OptionName1.ToString();
            //             txtOptionValue1.Text = _equip.LoadingGlassInfo.OptionValue1.ToString();
            //             txtOptionName2.Text = _equip.LoadingGlassInfo.OptionName2.ToString();
            //             txtOptionValue2.Text = _equip.LoadingGlassInfo.OptionValue2.ToString();
            //             txtOptionName3.Text = _equip.LoadingGlassInfo.OptionName3.ToString();
            //             txtOptionValue3.Text = _equip.LoadingGlassInfo.OptionValue3.ToString();
            //             txtOptionName4.Text = _equip.LoadingGlassInfo.OptionName4.ToString();
            //             txtOptionValue4.Text = _equip.LoadingGlassInfo.OptionValue4.ToString();
            //             txtOptionName5.Text = _equip.LoadingGlassInfo.OptionName5.ToString();
            //             txtOptionValue5.Text = _equip.LoadingGlassInfo.OptionValue5.ToString();
            // 
            //             if (_equip.LoadingGlassInfo.LotFlag == 0)
            //                 txtLotFlag.Text = "M";
            //             else if (_equip.LoadingGlassInfo.LotFlag == 1)
            //                 txtLotFlag.Text = "S";
            //             else if (_equip.LoadingGlassInfo.LotFlag == 2)
            //                 txtLotFlag.Text = "E";
            //             else if (_equip.LoadingGlassInfo.LotFlag == 3)
            //                 txtLotFlag.Text = "SE";
            //             else
            //                 txtLotFlag.Text = string.Empty;

            // 이하 원래 주석 처리
            // txtCSIF.Text = _equip.LoadingGlassInfo.CSIF.ToString();
            // txtAS.Text = _equip.LoadingGlassInfo.AS.ToString();
            //txtAPS.Text = _equip.LoadingGlassInfo.APS.ToString();
            //txtUniqueId.Text = _equip.LoadingGlassInfo.UniqueID.ToString();
            //txtBitSignal.Text = _equip.LoadingGlassInfo.BitSignal.ToString();
            #endregion
        }
        //Lamp
        private void LoadLampTime()
        {
            TextBox[] txtDeadline = { txtLampDeadline1, txtLampDeadline2, txtLampDeadline3 };
            int[] Deadline = { _equip.Lamp.Deadline[0],
                               _equip.Lamp.Deadline[1],
                               _equip.Lamp.Deadline[2],
                             };

            for (int iter = 0; iter < 3; iter++)
                txtDeadline[iter].Text = Deadline[iter].ToString();
        }
        private void UpdateLampTime()
        {
            TextBox[] txtUsingTime = { txtLampTime1, txtLampTime2, txtLampTime3 };
            DateTime[] UsingTime = { _equip.Lamp.UsingTime[0], _equip.Lamp.UsingTime[1], _equip.Lamp.UsingTime[2] };
            ButtonDelay2[] btnd = { btndLamp1_Unsued, btndLamp2_Unsued, btndLamp3_Unsued };

            DateTime curTime = DateTime.Now;

            for (int iter = 0; iter < 3; iter++)
            {
                TimeSpan interval = curTime - UsingTime[iter];
                txtUsingTime[iter].Text = (_equip.Lamp.IsUsed[iter] == true)
                    ? string.Format("{0}시간 {1}분 {2}초", (int)interval.TotalHours, interval.Minutes, interval.Seconds) : "";
                btnd[iter].BackColor = (_equip.Lamp.IsUsed[iter] == false) ? Color.Red : Color.Transparent;
            }
        }
        private void btndLampTimeUnusing_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btnd = sender as ButtonDelay2;

            if (btnd == btndLamp1_Unsued)
            {
                _equip.Lamp.IsUsed[0] = !_equip.Lamp.IsUsed[0];
                _equip.UsingTimeSetting.SetTime_LampUsing(0, _equip.Lamp.IsUsed[0]);
            }
            else if (btnd == btndLamp2_Unsued)
            {
                _equip.Lamp.IsUsed[1] = !_equip.Lamp.IsUsed[1];
                _equip.UsingTimeSetting.SetTime_LampUsing(1, _equip.Lamp.IsUsed[1]);
            }
            else if (btnd == btndLamp3_Unsued)
            {
                _equip.Lamp.IsUsed[2] = !_equip.Lamp.IsUsed[2];
                _equip.UsingTimeSetting.SetTime_LampUsing(2, _equip.Lamp.IsUsed[2]);
            }
            else
                throw new Exception("미지정 버튼 클릭??");

            _equip.UsingTimeSetting.Save();
        }
        private void btndLampTimeReset_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btnd = sender as ButtonDelay2;

            int idx = 0;
            if (btnd == btndLamp1_Reset)
                idx = 0;
            else if (btnd == btndLamp2_Reset)
                idx = 1;
            else if (btnd == btndLamp3_Reset)
                idx = 2;
            else
                throw new Exception("미지정 버튼 클릭??");

            _equip.Lamp.UsingTime[idx] = DateTime.Now;
            _equip.UsingTimeSetting.SetTime_Lamp(idx, _equip.Lamp.UsingTime[idx].ToString());
            _equip.UsingTimeSetting.Save();
        }
        private void btndLampDeadlineSave_Click(object sender, EventArgs e)
        {
            TextBox[] txtDeadline = { txtLampDeadline1, txtLampDeadline2, txtLampDeadline3 };

            for (int iter = 0; iter < 3; iter++)
            {
                int hour = int.Parse(txtDeadline[iter].Text != "" ? txtDeadline[iter].Text.ToString() : "0");
                _equip.UsingTimeSetting.SetTime_LampDeadline(iter, hour);
                _equip.Lamp.Deadline[iter] = hour;
            }
            if (_equip.UsingTimeSetting.Save())
                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "Lamp 设置时间已保存." : "램프 설정 시간이 저장되었습니다.");
            else
                CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "Lamp 设置时间未保存." : "램프 설정 시간이 저장되지 않았습니다.");
        }
        //User Setting
        private void InitContextMenu()
        {
            EventHandler eh = new EventHandler(LstUser_ContextMenuClick);
            MenuItem[] menu = {
                    new MenuItem("CREATE",eh),
                    new MenuItem("DELETE",eh),
                    new MenuItem("LEVEL CHANGE",eh),
                    new MenuItem("CANCEL",eh),
                };
            lstUsers.ContextMenu = new System.Windows.Forms.ContextMenu(menu);
        }
        private void UpdateUserPage()
        {
            LoginMgr.Instance.LoadInfo();

            //List 초기화
            lstUsers.Items.Clear();
            foreach (var element in LoginMgr.Instance.UserMgr.UserList())
            {
                string[] row = { element.Value.Id, element.Value.Level.ToString(), element.Value.Name };
                lstUsers.Items.Add(new ListViewItem(row));
            }
        }
        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            FrmLogin creater = new FrmLogin(_equip, FrmLogin.FrmLoginType.Create);
            creater.ShowDialog();

            UpdateUserPage();
        }
        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select the Change user");
                return;
            }

            string selectedID = lstUsers.SelectedItems[0].SubItems[0].Text;

            FrmLogin frm = new FrmLogin(_equip);
            frm.SetID(selectedID);
            frm.SetNotice("You have to login to modified ID");
            frm.ShowDialog();

            if (LoginMgr.Instance.IsLogined() && LoginMgr.Instance.LoginedUser.Id == selectedID)
            {
                frm = new FrmLogin(_equip, FrmLogin.FrmLoginType.Update);
                frm.SetID(selectedID);
                frm.ShowDialog();
            }

            UpdateUserPage();
        }
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select the delete user");
                return;
            }

            string selectedID = lstUsers.SelectedItems[0].SubItems[0].Text;

            FrmLogin frm = new FrmLogin(_equip, FrmLogin.FrmLoginType.Remove);
            frm.SetID(selectedID);
            frm.SetNotice(string.Format("ID : {0} Delete, Passward check ok", selectedID));
            frm.ShowDialog();

            UpdateUserPage();
        }
        public void LstUser_ContextMenuClick(object obj, EventArgs ea)
        {
            MenuItem mI = (MenuItem)obj;
            String str = mI.Text;

            if (str == "CREATE")
            {
                FrmLogin creater = new FrmLogin(_equip, FrmLogin.FrmLoginType.Create);
                creater.ShowDialog();

                UpdateUserPage();
            }
            else if (str == "DELETE")
            {
                if (LoginMgr.Instance.LevelCheck(EM_LV_LST.SUPERVISOR) == false) return;
                if (lstUsers.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Select the delete user");
                    return;
                }

                string selectedID = lstUsers.SelectedItems[0].SubItems[0].Text;

                FrmLogin frm = new FrmLogin(_equip, FrmLogin.FrmLoginType.Remove);
                frm.SetID(selectedID);
                frm.ShowDialog();

                UpdateUserPage();
            }
            else if (str == "PASSWORD CHANGE")
            {
                if (lstUsers.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Select the Change user");
                    return;
                }

                string selectedID = lstUsers.SelectedItems[0].SubItems[0].Text;

                FrmLogin frm = new FrmLogin(_equip);
                frm.SetID(selectedID);
                frm.SetNotice("You have to login to modified ID");
                frm.ShowDialog();

                if (LoginMgr.Instance.IsLogined() && LoginMgr.Instance.LoginedUser.Id == selectedID)
                {
                    frm = new FrmLogin(_equip, FrmLogin.FrmLoginType.Update);
                    frm.SetID(selectedID);
                    frm.ShowDialog();
                }

                UpdateUserPage();
            }
            else if (str == "LEVEL CHANGE")
            {
                if (LoginMgr.Instance.LevelCheck(EM_LV_LST.SUPERVISOR) == false) return;
                if (lstUsers.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Select the Change user");
                    return;
                }

                string selectedID = lstUsers.SelectedItems[0].SubItems[0].Text;

                FrmLogin frm = new FrmLogin(_equip, FrmLogin.FrmLoginType.LevelChange);
                frm.SetID(selectedID);
                frm.ShowDialog();

                UpdateUserPage();
            }
        }
        #endregion

        #region ETC 조작
        private void tab2NdParamSetting_SelectedIndex(object sender, EventArgs e)
        {
            TabPage curPage = (sender as TabControl).SelectedTab;

            _showPage = curPage;
        }

        //Inner Work
        private void btnd3thInnerWork_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormPWNeedClose")
                {
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {
                        openForm.WindowState = FormWindowState.Normal;
                    }
                    openForm.Activate();
                    return;
                }
            }
            new FormPWNeedClose(_equip).Visible = true;
        }
        //Door
        private void btnDelayDoor_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btnd = sender as ButtonDelay2;

            if (_equip.ModeSelectKey.IsAuto == true)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<AUTO MODE>\n(AUTO MODE 状态是无法 Door Open)" : "인터락<AUTO MODE>\n(AUTO MODE 상태에서는 Door Open이 불가능합니다.)");
                return;
            }
            if (_equip.IsHomePositioning || _equip.EquipRunMode == EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<动作中>\n(设备动作中. 无法变更设备设置.)" : "인터락<동작중>\n설비가 동작중입니다. 설비 설정을 변경 할 수 없습니다.");
                return;
            }
            if (btnd == btnDoorTop1) { CmdDoorOpenSol(_equip.TopDoor01, btnd, !_equip.TopDoor01.IsSolOnOff); }
            else if (btnd == btnDoorTop2) { CmdDoorOpenSol(_equip.TopDoor02, btnd, !_equip.TopDoor02.IsSolOnOff); }
            else if (btnd == btnDoorTop3) { CmdDoorOpenSol(_equip.TopDoor03, btnd, !_equip.TopDoor03.IsSolOnOff); }
            else if (btnd == btnDoorTop4) { CmdDoorOpenSol(_equip.TopDoor04, btnd, !_equip.TopDoor04.IsSolOnOff); }            
            
            else if (btnd == btnDelayAllDoorOpen) { CmdAllDoorOpenSol(true); }
            else if (btnd == btnDelayAllDoorClose) { CmdAllDoorOpenSol(false); }
        }
        private void CmdDoorOpenSol(EquipMainUi.Struct.Switch selDoorSol, ButtonDelay2 selDoorBtn, bool vBit)
        {
            selDoorSol.OnOff(_equip, vBit);
            DoorBtnStateChange(selDoorBtn, selDoorSol.YB_OnOff.vBit);
        }
        private void CmdAllDoorOpenSol(bool _vBit)
        {
            CmdDoorOpenSol(_equip.TopDoor01, btnDoorTop1, _vBit);
            CmdDoorOpenSol(_equip.TopDoor02, btnDoorTop2, _vBit);
            CmdDoorOpenSol(_equip.TopDoor03, btnDoorTop3, _vBit);
            CmdDoorOpenSol(_equip.TopDoor04, btnDoorTop4, _vBit);
        }
        private void UpdateDoorState()
        {
            bool[] isOpenSol = {
                                   _equip.TopDoor01.YB_OnOff.vBit,
                                   _equip.TopDoor02.YB_OnOff.vBit,
                                   _equip.TopDoor03.YB_OnOff.vBit,
                                   _equip.TopDoor04.YB_OnOff.vBit,
                               };

            DoorBtnStateChange(btnDoorTop1, _equip.TopDoor01.IsSolOnOff, _equip.TopDoor01.IsOnOff);
            DoorBtnStateChange(btnDoorTop2, _equip.TopDoor02.IsSolOnOff, _equip.TopDoor02.IsOnOff);
            DoorBtnStateChange(btnDoorTop3, _equip.TopDoor03.IsSolOnOff, _equip.TopDoor03.IsOnOff);
            DoorBtnStateChange(btnDoorTop4, _equip.TopDoor04.IsSolOnOff, _equip.TopDoor04.IsOnOff);

            bool? isAllDoorOpenSol = _equip.TopDoor01.YB_OnOff.vBit;

            for (int iPos = 0; iPos < isOpenSol.Length; iPos++)
            {
                if (isAllDoorOpenSol != isOpenSol[iPos])
                {
                    isAllDoorOpenSol = null;
                    break;
                }
            }

            if (isAllDoorOpenSol == true)
            {
                btnDelayAllDoorOpen.BackColor = Color.Red;
                btnDelayAllDoorClose.BackColor = Color.Transparent;
            }
            else if (isAllDoorOpenSol == false)
            {
                btnDelayAllDoorOpen.BackColor = Color.Transparent;
                btnDelayAllDoorClose.BackColor = Color.Red;
            }
            else
            {
                btnDelayAllDoorOpen.BackColor = Color.Transparent;
                btnDelayAllDoorClose.BackColor = Color.Transparent;
            }

            //Auto Teach
            if (_equip.ModeSelectKey.IsAuto == true)
            {
                lblAutoTeachState.Text = "Auto Mode";
                lblAutoTeachState.BackColor = Color.Red;
            }
            else
            {
                lblAutoTeachState.Text = "Teach Mode";
                lblAutoTeachState.BackColor = Color.White;
            }
        }
        private void DoorBtnStateChange(ButtonDelay2 btnDelay, bool isOpenCmd, bool? isOpenState = null)
        {
            if (isOpenCmd == true)
            {
                btnDelay.BackColor = Color.Red;
                btnDelay.ForeColor = Color.Black;
            }
            else
            {
                btnDelay.BackColor = Color.Transparent;
                if (isOpenState == true) btnDelay.ForeColor = Color.Red;
            }

            if (isOpenState == null) return;

            if (isOpenState == true)
            {
                btnDelay.Text = "Open";
            }
            else
            {
                btnDelay.ForeColor = Color.Black;
                btnDelay.Text = "Close";
            }
        }

        //Working Light
        private void UpdateWorkLightState()
        {
            WorkingLightOnOff(btnd_PcRackLight, _equip.WorkingLight.PcRack);
            WorkingLightOnOff(btnd_StageLight, _equip.WorkingLight.Stage);
        }
        private void btnd3thWorkingLightOnOff_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btnd = sender as ButtonDelay2;

            if (btnd == btnd_StageLight)
                _equip.WorkingLight.Stage.OnOff(_equip, !_equip.WorkingLight.Stage.YB_OnOff.vBit);
            else if (btnd == btnd_PcRackLight)
                _equip.WorkingLight.PcRack.OnOff(_equip, !_equip.WorkingLight.PcRack.YB_OnOff.vBit);
        }
        private void WorkingLightOnOff(ButtonDelay2 btnd, EquipMainUi.Struct.Switch light)
        {
            bool isLightOn = light.YB_OnOff.vBit;

            light.OnOff(_equip, isLightOn);

            if (isLightOn == true)
            {
                btnd.Text = "Light On";
                btnd.BackColor = Color.Red;
            }
            else
            {
                btnd.Text = "Light Off";
                btnd.BackColor = Color.Transparent;
            }
        }
        private void WorkingAutoModeChange_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkb = sender as CheckBox;

            if (chkb == chkbTopAuto && chkb.Checked == true)
            {
                _equip.WorkingLight.IsAutoMode = true;
            }
            else if (chkb == chkbTopAuto && chkb.Checked == false)
            {
                _equip.WorkingLight.IsAutoMode = false;
            }
        }
        private void LoadCheckState()
        {
            if (_equip.WorkingLight.IsAutoMode)
                chkbTopAuto.Checked = true;
            else
                chkbTopAuto.Checked = false;
        }
        #endregion

        #region Data Setting
        static Button selectedBtn;
        int clickedLPMGrid = 0;

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            foreach (Control c in this.pGridLPM1.Controls)
            {
                c.MouseClick += new MouseEventHandler(pGridLPM1_MouseClick);
            }
            foreach (Control c in this.pGridLPM2.Controls)
            {
                c.MouseClick += new MouseEventHandler(pGridLPM2_MouseClick);
            }
        }
        private void pGridLPM1_MouseClick(object sender, MouseEventArgs e)
        {
            pnlLPM2.BackColor = Color.Transparent;
            pnlLPM1.BackColor = Color.OrangeRed;
            clickedLPMGrid = 1;
            UpdateDataInfo();
        }
        private void pGridLPM2_MouseClick(object sender, MouseEventArgs e)
        {
            pnlLPM1.BackColor = Color.Transparent;
            pnlLPM2.BackColor = Color.OrangeRed;
            clickedLPMGrid = 2;
            UpdateDataInfo();
        }
        
        private void UpdateDataInfo()
        {
            Button[] btns = new Button[]
            {
                btnSelectDataAligner,
                btnSelectDataAll,
                btnSelectDataEquip,
                btnSelectDataLPM1,
                btnSelectDataLPM2,
                btnSelectDataLRobot,
                btnSelectDataURobot,
            };

            foreach (Button btn in btns)
            {
                if (selectedBtn == btn)
                {
                    btn.BackColor = Color.DodgerBlue;
                }
                    
                else
                {
                    btn.BackColor = Color.Gainsboro;
                }
            }

            FillCassttesData();
            FillWafersData();
        }
        private void FillCassttesData()
        {
            pGridLPM1.SelectedObject = null;
            pGridLPM2.SelectedObject = null;

            switch (selectedBtn.Name)
            {
                case "btnSelectDataAll":
                    CassetteInfo cstLpm1 = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort1.CstKey);
                    CassetteInfo cstLpm2 = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort2.CstKey);

                    if (cstLpm1 != null)
                        pGridLPM1.SelectedObject = cstLpm1;

                    if (cstLpm2 != null)
                        pGridLPM2.SelectedObject = cstLpm2;
                    break;
                case "btnSelectDataLPM1":
                    CassetteInfo Lpm1 = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort1.CstKey);
                    if (Lpm1 != null)
                        pGridLPM1.SelectedObject = Lpm1;
                    break;
                case "btnSelectDataLPM2":
                    CassetteInfo Lpm2 = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort2.CstKey);
                    if (Lpm2 != null)
                        pGridLPM2.SelectedObject = Lpm2;
                    break;
                case "btnSelectDataURobot":
                    CassetteInfo cstUpper = TransferDataMgr.GetCst(GG.Equip.Efem.Robot.UpperWaferKey);
                    if (cstUpper == null) return;
                    if (cstUpper.LoadPortNo == 1)
                        pGridLPM1.SelectedObject = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort1.CstKey);
                    else
                        pGridLPM2.SelectedObject = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort2.CstKey);
                    break;
                case "btnSelectDataLRobot":
                    CassetteInfo cstLower = TransferDataMgr.GetCst(GG.Equip.Efem.Robot.LowerWaferKey);
                    if (cstLower == null) return;
                    if (cstLower.LoadPortNo == 1)
                        pGridLPM1.SelectedObject = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort1.CstKey);
                    else
                        pGridLPM2.SelectedObject = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort2.CstKey);
                    break;
                case "btnSelectDataAligner":
                    CassetteInfo cstAligner = TransferDataMgr.GetCst(GG.Equip.Efem.Aligner.LowerWaferKey);
                    if (cstAligner == null) return;
                    if (cstAligner.LoadPortNo == 1)
                        pGridLPM1.SelectedObject = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort1.CstKey);
                    else
                        pGridLPM2.SelectedObject = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort2.CstKey);
                    break;
                case "btnSelectDataEquip":
                    CassetteInfo cstAVI = TransferDataMgr.GetCst(GG.Equip.TransferUnit.LowerWaferKey);
                    if (cstAVI == null) return;
                    if (cstAVI.LoadPortNo == 1)
                        pGridLPM1.SelectedObject = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort1.CstKey);
                    else
                        pGridLPM2.SelectedObject = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort2.CstKey);
                    break;
            }

        }
        private void FillWafersData()
        {
            dgvWaferData.Rows.Clear();

            int newRowIdx = 0;
            List<WaferInfo> listWaferInfo = new List<WaferInfo>();

            switch (selectedBtn.Name)
            {
                case "btnSelectDataAll":
                    if (clickedLPMGrid == 1)
                    {
                        CassetteInfo Lpm1 = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort1.CstKey);
                        if (Lpm1 == null) return;
                        listWaferInfo = TransferDataMgr.GetWafersIn(Lpm1.CstID);
                    }
                    else if (clickedLPMGrid == 2)
                    {
                        CassetteInfo Lpm2 = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort2.CstKey);
                        if (Lpm2 == null) return;
                        listWaferInfo = TransferDataMgr.GetWafersIn(Lpm2.CstID);
                    }
                    break;
                case "btnSelectDataLPM1":
                    CassetteInfo cstLpm1 = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort1.CstKey);
                    if (cstLpm1 == null) return;
                    listWaferInfo = TransferDataMgr.GetWafersIn(cstLpm1.CstID);
                    break;
                case "btnSelectDataLPM2":
                    CassetteInfo cstLpm2 = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort2.CstKey);
                    if (cstLpm2 == null) return;
                    listWaferInfo = TransferDataMgr.GetWafersIn(cstLpm2.CstID);
                    break;
                case "btnSelectDataURobot":
                    WaferInfo upperwafer = TransferDataMgr.GetWafer(GG.Equip.Efem.Robot.UpperWaferKey);
                    if (upperwafer == null) return;
                    listWaferInfo.Add(upperwafer);
                    break;
                case "btnSelectDataLRobot":
                    WaferInfo lowerwafer = TransferDataMgr.GetWafer(GG.Equip.Efem.Robot.LowerWaferKey);
                    if (lowerwafer == null) return;
                    listWaferInfo.Add(lowerwafer);
                    break;
                case "btnSelectDataAligner":
                    WaferInfo alignerwafer = TransferDataMgr.GetWafer(GG.Equip.Efem.Aligner.LowerWaferKey);
                    if (alignerwafer == null) return;
                    listWaferInfo.Add(alignerwafer);
                    break;
                case "btnSelectDataEquip":
                    WaferInfo aviwafer = TransferDataMgr.GetWafer(GG.Equip.TransferUnit.LowerWaferKey);
                    if (aviwafer == null) return;
                    listWaferInfo.Add(aviwafer);
                    break;
            }

            foreach (WaferInfo wafer in listWaferInfo)
            {
                newRowIdx = dgvWaferData.Rows.Add(new object[] {
                            wafer.CstID,
                            wafer.SlotNo,
                            wafer.Notch,
                            wafer.IsAlignComplete,
                            wafer.IsInspComplete,
                            wafer.IsComeBack,
                            wafer.OutputDate,
                            wafer.InputDate,
                        });
            }

            dgvWaferData.ClearSelection();
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            pnlLPM1.BackColor = Color.Transparent;
            pnlLPM2.BackColor = Color.Transparent;

            switch (btn.Name)
            {
                case "btnSelectDataAll":
                    clickedLPMGrid = 0;
                    selectedBtn = btnSelectDataAll;
                    btnCreateWafer.Visible = false;
                    break;
                case "btnSelectDataLPM1":
                    if(GG.Equip.Efem.LoadPort1.CstKey.ID != "") pnlLPM1.BackColor = Color.OrangeRed;
                    selectedBtn = btnSelectDataLPM1;
                    btnCreateWafer.Visible = true;
                    break;
                case "btnSelectDataLPM2":
                    if (GG.Equip.Efem.LoadPort2.CstKey.ID != "") pnlLPM2.BackColor = Color.OrangeRed;
                    selectedBtn = btnSelectDataLPM2;
                    btnCreateWafer.Visible = true;
                    break;
                case "btnSelectDataURobot":
                    selectedBtn = btnSelectDataURobot;
                    btnCreateWafer.Visible = false;
                    break;
                case "btnSelectDataLRobot":
                    selectedBtn = btnSelectDataLRobot;
                    btnCreateWafer.Visible = false;
                    break;
                case "btnSelectDataAligner":
                    selectedBtn = btnSelectDataAligner;
                    btnCreateWafer.Visible = false;
                    break;
                case "btnSelectDataEquip":
                    selectedBtn = btnSelectDataEquip;
                    btnCreateWafer.Visible = false;
                    break;
            }
            UpdateDataInfo();
        }

        private void btnCreateWafer_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(GG.StartupPath, "Setting");
            string time = DateTime.Now.ToString("yyMMddHHmmss");

            switch (selectedBtn.Name)
            {
                case "btnSelectDataLPM1":
                    WaferInfo wafer1 = null;
                    for (int i = 1; i <= 13; i++)
                    {
                        wafer1 = WaferInfo.CreateTestWaferInfo(time, i);
                        TransferDataMgr.InsertWafer(wafer1);
                    }
                    TransferDataMgr.RecoveryData(wafer1.ToKey(), EmEfemDBPort.LOADPORT1);

                    CassetteInfo Lpm1Cassette = CassetteInfo.CreateSampleCassetteInfo(time, 1);
                    TransferDataMgr.InsertCst(Lpm1Cassette);

                    GG.Equip.Efem.LoadPort1.CstKey = Lpm1Cassette.ToKey();
                    break;
                case "btnSelectDataLPM2":
                    WaferInfo wafer2 = null;
                    for (int i = 1; i <= 13; i++)
                    {
                        wafer2 = WaferInfo.CreateTestWaferInfo(time, i);
                        TransferDataMgr.InsertWafer(wafer2);
                    }
                    TransferDataMgr.RecoveryData(wafer2.ToKey(), EmEfemDBPort.LOADPORT2);

                    CassetteInfo Lpm2Cassette = CassetteInfo.CreateSampleCassetteInfo(time, 2);
                    TransferDataMgr.InsertCst(Lpm2Cassette);

                    GG.Equip.Efem.LoadPort2.CstKey = Lpm2Cassette.ToKey();
                    break;
            }
            UpdateDataInfo();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (LoginMgr.Instance.LoginedUser.Level == EM_LV_LST.USER)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<User 权限>\n(User权限无法变更设备设置 .)" : "인터락<유저 권한>\nUser 권한으로는 설비 설정을 변경 할 수 없습니다.");
                return;
            }
            string cstID;
            DialogResult result = MessageBox.Show(GG.boChinaLanguage ? "要删除选择的Data吗?" : "선택한 데이터를 삭제하시겠습니까?", "caption", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                switch (selectedBtn.Name)
                {
                    case "btnSelectDataLPM1":
                        //if(dgvCstData.SelectedRows.Count>0)
                        //{
                        //    cstID = dgvCstData.SelectedRows[0].Cells[0].Value.ToString();
                        //    if (TransferDataMgr.DeleteCst(cstID) == false) return;
                        //    //TransferDataMgr.DeleteBakFile(EmEfemPort.LOADPORT1);
                        //}
                        //if (dgvWaferData.SelectedRows.Count != 0)
                        //{
                        //    for (int i = 0; i < dgvWaferData.SelectedRows.Count; i++)
                        //    {
                        //        cstID = dgvWaferData.SelectedRows[i].Cells[0].Value.ToString();
                        //        int slotNo = int.Parse(dgvWaferData.SelectedRows[i].Cells[1].Value.ToString());

                        //        if (TransferDataMgr.DeleteWafer(cstID, slotNo) == false)
                        //        {
                        //            MessageBox.Show("데이터 삭제 실패");
                        //            return;
                        //        }
                        //    }
                        //}
                        break;
                    case "btnSelectDataLPM2":
                        //if (dgvCstData.SelectedRows.Count > 0)
                        //{
                        //    cstID = dgvCstData.SelectedRows[0].Cells[0].Value.ToString();
                        //    if (TransferDataMgr.DeleteCst(cstID) == false) return;
                        //    //TransferDataMgr.DeleteBakFile(EmEfemPort.LOADPORT2);
                        //}
                        //if (dgvWaferData.SelectedRows.Count != 0)
                        //{
                        //    for (int i = 0; i < dgvWaferData.SelectedRows.Count; i++)
                        //    {
                        //        cstID = dgvWaferData.SelectedRows[i].Cells[0].Value.ToString();
                        //        int slotNo = int.Parse(dgvWaferData.SelectedRows[i].Cells[1].Value.ToString());

                        //        if (TransferDataMgr.DeleteWafer(cstID, slotNo) == false)
                        //        {
                        //            MessageBox.Show("데이터 삭제 실패");
                        //            return;
                        //        }
                        //    }
                        //}
                        break;
                }
                UpdateDataInfo();
            }
            else if (result == DialogResult.No)
            {
                
            }
        }
        private void btnShift_Click(object sender, EventArgs e)
        {
            if (GG.Equip.EquipRunMode == EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Auto 状态下无法 Shift " : "Auto 상태에서 Shift 불가능합니다");
                return;
            }

            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FrmTransferDataMgr")
                {
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {
                        openForm.WindowState = FormWindowState.Normal;
                    }
                    openForm.Dispose();
                    break;
                }
            }
            FrmTransferDataMgr f = new FrmTransferDataMgr();
            f.Show();
        }
        private void btnWaferInfoEdit_Click(object sender, EventArgs e)
        {
            if (LoginMgr.Instance.LoginedUser.Level == EM_LV_LST.USER)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<User 权限>\n(User权限无法变更设备设置 .)" : "인터락<유저 권한>\nUser 권한으로는 설비 설정을 변경 할 수 없습니다.");
                return;
            }
            PopupFrmTransferDataEdit();
            UpdateDataInfo();
        }
        private void PopupFrmTransferDataEdit(string cstID = "", int slotno = 0)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FrmTransferDataModify")
                {
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {
                        openForm.WindowState = FormWindowState.Normal;
                    }
                    openForm.Dispose();
                    break;
                }
            }

            if (cstID != string.Empty && slotno != 0)
            {
                FrmTransferDataModify f = new FrmTransferDataModify(TransferDataMgr.GetWafer(cstID, slotno));
                f.Show();
            }
            else
            {
                FrmTransferDataModify f = new FrmTransferDataModify();
                f.Show();
            }
        }
        private void dgvWaferData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                var cstID = dgvWaferData.Rows[e.RowIndex].Cells[0].Value.ToString();
                var slotno = int.Parse(dgvWaferData.Rows[e.RowIndex].Cells[1].Value.ToString());

                PopupFrmTransferDataEdit(cstID, slotno);
            }
        }
        #endregion
    }
}
