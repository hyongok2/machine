using EquipMainUi.Struct;
using EquipMainUi.Struct.Detail.EFEM;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EquipMainUi
{
    public delegate void AppendDataEvent(EmEfemPort port, string msg);

    public partial class FrmEfemTest : Form
    {
        private Equipment equip;
        private bool _isUsingProxy = true;

        AppendDataEvent _textAppender;

        public FrmEfemTest(Equipment equip)
        {
            this.equip = equip;
            //EFEM 통신 확인을 위해 임시 추가
            _textAppender = new AppendDataEvent(EFEMTcp_recvDataEvent);
            EFEMTcp.appendDataEvent += EFEMTcp_recvDataEvent;

            InitializeComponent();
            tbIP.Text = equip.InitSetting.EfemIP;
            tbPort.Text = equip.InitSetting.EfemPort.ToString();
            cbBasicCmd.SelectedIndex = 0;
            cbbBlueLamp.SelectedIndex = 0;
            cbbBuzzer1.SelectedIndex = 0;
            cbbBuzzer2.SelectedIndex = 0;
            cbbGreenLamp.SelectedIndex = 0;
            cbbLampByte1.SelectedIndex = 0;
            cbbLampByte2.SelectedIndex = 0;
            cbbLampPort.SelectedIndex = 0;
            cbbRedLamp.SelectedIndex = 0;
            cbbTransfer.SelectedIndex = 0;
            cbbYelloLamp.SelectedIndex = 0;            
        }
        //EFEM 통신 확인을 위해 임시 추가
        private void EFEMTcp_recvDataEvent(EmEfemPort port, string msg)
        {
            if (lvRobotLog != null && lvRobotLog.InvokeRequired)
            {
                try
                {
                    AppendDataEvent appendevent = new AppendDataEvent(EFEMTcp_recvDataEvent);
                    this.Invoke(appendevent, new object[] { port, msg });
                }
                catch (Exception ex)
                {

                }
                //lbLog.Invoke(_textAppender, msg);
            }
            else
            {

                msg = msg.Remove(0, 13);
                if (GG.EfemNoUse == false && msg.Contains("STAT~") == true)
                    return;

                if(port == EmEfemPort.ROBOT)
                {
                    AddListViewItem(lvRobotLog, msg);
                }
                else if(port == EmEfemPort.LOADPORT1)
                {
                    AddListViewItem(lvLoadPort1Log, msg);
                }
                else if (port == EmEfemPort.LOADPORT2)
                {
                    AddListViewItem(lvLoadPort2Log, msg);
                }
                else if (port == EmEfemPort.ALIGNER)
                {
                    AddListViewItem(lvAlignerLog, msg);
                }
                else if (port == EmEfemPort.ETC)
                {
                    AddListViewItem(lvEtcLog, msg);
                }
                else
                {
                    AddListViewItem(lvSendLog, msg);
                    return;
                }

                ListViewItem lvi = new ListViewItem(DateTime.Now.ToString("MM/dd HH:mm:ss.fff"));
                lvi.SubItems.Add(port.ToString());
                lvi.SubItems.Add(msg);
                if (lvRecvLog == null)
                    return;
                lvRecvLog.Items.Add(lvi);
                lvRecvLog.Items[lvRecvLog.Items.Count - 1].EnsureVisible();
            }
        }
        private void AddListViewItem(ListView lv, string msg)
        {
            if (lv == null)
                return;
            ListViewItem lvi = new ListViewItem(DateTime.Now.ToString("MM/dd HH:mm:ss.fff"));
            lvi.SubItems.Add(msg);
            lv.Items.Add(lvi);

            lv.Items[lv.Items.Count - 1].EnsureVisible();

            if (lv.Items.Count > 200)
                lv.Items.RemoveAt(0);
        }

        #region Basic Command
        private void btnConnect_Click(object sender, EventArgs e)
        {
            bool isConnected = EFEMTcp.Connect(tbIP.Text, int.Parse(tbPort.Text));

            AddListViewItem(lvSendLog, isConnected ? "연결성공" : "연결실패");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            EFEMTcp.SendMessage(tbTestMsg.Text);

            tbTestMsg.Text = string.Empty;
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            EmEfemPort port = StringToEfemPort((string)cbBasicCmd.SelectedItem);

            if (_isUsingProxy)
                equip.Efem.Proxy.StartCommand(equip, port, EmEfemCommand.INIT_);
            else
                EFEMTcp.CmdInit(port);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            EmEfemPort port = StringToEfemPort((string)cbBasicCmd.SelectedItem);
            if (_isUsingProxy)
                equip.Efem.Proxy.StartCommand(equip, port, EmEfemCommand.RESET);
            else
                EFEMTcp.CmdReset(port);
        }

        private void btnStat_Click(object sender, EventArgs e)
        {
            EmEfemPort port = StringToEfemPort((string)cbBasicCmd.SelectedItem);
            if (_isUsingProxy)
                equip.Efem.Proxy.StartCommand(equip, port, EmEfemCommand.STAT_);
            else
                EFEMTcp.CmdCheckState(port);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {            
            //if (_isUsingProxy)
            //    equip.Efem.Proxy.StartCommand(equip, EmEfemPort.ROBOT, EmEfemCommand.PAUSE);
            //else
            //    EFEMTcp.CmdPause();
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            if (_isUsingProxy)
                equip.Efem.Proxy.StartCommand(equip, EmEfemPort.ROBOT, EmEfemCommand.RESUM);
            else
                EFEMTcp.CmdResume();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            EmEfemPort port = StringToEfemPort((string)cbBasicCmd.SelectedItem);
            if (_isUsingProxy)
                equip.Efem.Proxy.StartCommand(equip, port, EmEfemCommand.STOP_);
            else
                EFEMTcp.CmdStop(port);
        }

        private void btnAlign_Click(object sender, EventArgs e)
        {
            string recipe = txtRecipeName.Text;
            int degree = 0;
            
            if (int.TryParse(tbDegree.Text, out degree) == false)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "数试请输入0~359" : "degree 0~359");
                return;
            }

            if (_isUsingProxy)
                equip.Efem.Proxy.StartAlign(equip, degree, chkOCRForward.Checked, recipe);
            else
                EFEMTcp.CmdAlignment(degree, chkOCRForward.Checked, recipe);
        }

        private void btnClamp_Click(object sender, EventArgs e)
        {
            //EmEfemPort port = StringToEfemPort((string)cbBasicCmd.SelectedItem);
            //if (_isUsingProxy)
            //    equip.Efem.Proxy.StartCommand(equip, port, EmEfemCommand.CLAMP);
            //else
            //    EFEMTcp.CmdClamp(port);
        }

        private void btnUnClamp_Click(object sender, EventArgs e)
        {
            //EmEfemPort port = StringToEfemPort((string)cbBasicCmd.SelectedItem);
            //if (_isUsingProxy)
            //    equip.Efem.Proxy.StartCommand(equip, port, EmEfemCommand.UCLAM);
            //else
            //    EFEMTcp.CmdUnClamp(port);
        }

        private void btnDock_Click(object sender, EventArgs e)
        {
            //EmEfemPort port = StringToEfemPort((string)cbBasicCmd.SelectedItem);
            //if (_isUsingProxy)
            //    equip.Efem.Proxy.StartCommand(equip, port, EmEfemCommand.DOCK_);
            //else
            //    EFEMTcp.CmdDocking(port);
        }

        private void btnUnDock_Click(object sender, EventArgs e)
        {
            //EmEfemPort port = StringToEfemPort((string)cbBasicCmd.SelectedItem);
            //if (_isUsingProxy)
            //    equip.Efem.Proxy.StartCommand(equip, port, EmEfemCommand.UDOCK);
            //else
            //    EFEMTcp.CmdUnDocking(port);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            EmEfemPort port = StringToEfemPort((string)cbBasicCmd.SelectedItem);
            if (_isUsingProxy)
                equip.Efem.Proxy.StartCommand(equip, port, EmEfemCommand.OPEN_);
            else
                EFEMTcp.CmdOpenFoup(port);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            EmEfemPort port = StringToEfemPort((string)cbBasicCmd.SelectedItem);
            if (_isUsingProxy)
                equip.Efem.Proxy.StartCommand(equip, port, EmEfemCommand.CLOSE);
            else
                EFEMTcp.CmdCloseFoup(port);
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //EmEfemPort port = StringToEfemPort((string)cbBasicCmd.SelectedItem);
            //if (_isUsingProxy)
            //    equip.Efem.Proxy.StartCommand(equip, port, EmEfemCommand.LOAD_);
            //else
            //    EFEMTcp.CmdLoadFoup(port);
        }

        private void btnUnload_Click(object sender, EventArgs e)
        {
            //EmEfemPort port = StringToEfemPort((string)cbBasicCmd.SelectedItem);
            //if (_isUsingProxy)
            //    equip.Efem.Proxy.StartCommand(equip, port, EmEfemCommand.ULOAD);
            //else
            //    EFEMTcp.CmdUnLoadFoup(port);
        }

        private void btnMapping_Click(object sender, EventArgs e)
        {
            EmEfemPort port = StringToEfemPort((string)cbBasicCmd.SelectedItem);
            if (_isUsingProxy)
                equip.Efem.Proxy.StartCommand(equip, port, EmEfemCommand.MAPP_);
            else
                EFEMTcp.CmdAskMappingData(port);
        }

        private void btnChmada_Click(object sender, EventArgs e)
        {
            if (_isUsingProxy)
                equip.Efem.Proxy.StartCommand(equip, EmEfemPort.ETC, EmEfemCommand.CHMDA);
            else
                EFEMTcp.CmdChangeModeAuto();
        }

        private void btnChmdm_Click(object sender, EventArgs e)
        {
            if (_isUsingProxy)
                equip.Efem.Proxy.StartCommand(equip, EmEfemPort.ETC, EmEfemCommand.CHMDM);
            else
                EFEMTcp.CmdChangeModeManual();
        }

        private void btnPardy_Click(object sender, EventArgs e)
        {
            if (_isUsingProxy)
                equip.Efem.Proxy.StartCommand(equip, EmEfemPort.ALIGNER, EmEfemCommand.PARDY);
            else
                EFEMTcp.CmdPreAlignerReady();
        }
        #endregion

        #region Transfer / Lamp / Buzzer
        private void btnWaiting_Click(object sender, EventArgs e)
        {
            EmEfemRobotArm robotArm = rbUpper.Checked == true ? EmEfemRobotArm.Upper : EmEfemRobotArm.Lower;
            int slot = int.Parse(tbSlot.Text);
            EmEfemPort port = StringToEfemPort((string)cbbTransfer.SelectedItem);

            if (_isUsingProxy)
                equip.Efem.Proxy.StartWaitRobot(equip, new EFEMWAITRDataSet(robotArm, port, slot));
            else
                EFEMTcp.CmdWaitTransfer(robotArm, port, slot);
        }

        private void btnLpled_Click(object sender, EventArgs e)
        {
            EmEfemPort port = StringToEfemPort((string)cbbLampPort.SelectedItem);
            EmEfemLampType lampType = (EmEfemLampType)cbbLampByte1.SelectedIndex;
            EmEfemLampBuzzerState lampState = (EmEfemLampBuzzerState)cbbLampByte2.SelectedIndex;

            if (_isUsingProxy)
                equip.Efem.Proxy.StartLPMLedChange(equip, port, new EFEMLPLEDDataSet(lampType, lampState));
            else
                EFEMTcp.CmdChangeLedLamp(port, lampType, lampState);
        }

        private void cbbLampByte1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbLampByte2.Items.Clear();

            if (cbbLampByte1.SelectedIndex == 0)
            {
                cbbLampByte2.Items.Add("Disable");
                cbbLampByte2.Items.Add("Enable");
            }
            else
            {
                cbbLampByte2.Items.Add("OFF");
                cbbLampByte2.Items.Add("ON");
                cbbLampByte2.Items.Add("BLINK");
            }

            cbbLampByte2.Text = "";
        }


        private void btnSiglm_Click(object sender, EventArgs e)
        {
            EmEfemPort port = StringToEfemPort((string)cbbLampPort.SelectedItem);

            EmEfemLampBuzzerState redLamp = (EmEfemLampBuzzerState)cbbRedLamp.SelectedIndex;
            EmEfemLampBuzzerState yelloLamp = (EmEfemLampBuzzerState)cbbYelloLamp.SelectedIndex;
            EmEfemLampBuzzerState greenLamp = (EmEfemLampBuzzerState)cbbGreenLamp.SelectedIndex;
            EmEfemLampBuzzerState blueLamp = (EmEfemLampBuzzerState)cbbBlueLamp.SelectedIndex;
            EmEfemLampBuzzerState buzzer1 = (EmEfemLampBuzzerState)cbbBuzzer1.SelectedIndex;
            EmEfemLampBuzzerState buzzer2 = (EmEfemLampBuzzerState)cbbBuzzer2.SelectedIndex;

            if (_isUsingProxy)
                equip.Efem.Proxy.StartLampBuzzerChange(equip, new EFEMSIGLMDataSet(redLamp, yelloLamp, greenLamp, blueLamp, buzzer1));
            else
                EFEMTcp.CmdSetLampAndBuzzer(redLamp, yelloLamp, greenLamp, blueLamp, buzzer1);
        }
        #endregion


        private EmEfemPort StringToEfemPort(string port)
        {
            if(port == null)
            {
                return EmEfemPort.NONE;
            }
            if (port.Equals("None"))
            {
                return EmEfemPort.NONE;
            }
            else if (port.Equals("Robot"))
            {
                return EmEfemPort.ROBOT;
            }
            else if (port.Equals("LoadPort1"))
            {
                return EmEfemPort.LOADPORT1;
            }
            else if (port.Equals("LoadPort2"))
            {
                return EmEfemPort.LOADPORT2;
            }
            else if (port.Equals("Equipment"))
            {
                return EmEfemPort.EQUIPMENT;
            }
            else if (port.Equals("Aligner"))
            {
                return EmEfemPort.ALIGNER;
            }
            else if (port.Equals("Etc"))
            {
                return EmEfemPort.ETC;
            }
            else
            {
                return EmEfemPort.NONE;
            }
        }

        private void rdTcp_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = sender as RadioButton;

            if (rd == rdTcp)
            {
                _isUsingProxy = false;
            }
            else if (rd == rdProxy)
            {
                _isUsingProxy = true;
            }
        }

        private void tmrUiUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                EmEfemPort port = StringToEfemPort((string)cbBasicCmd.SelectedItem);

                if (equip.Efem.Proxy.HS.ContainsKey(port) == true
                    && equip.Efem.Proxy.HS[port].LstCmd.ContainsKey(equip.Efem.Proxy.LastCmd) == true)
                {
                    lblProxyStep.Text = equip.Efem.Proxy.HS[port].LstCmd[equip.Efem.Proxy.LastCmd].Step.ToString();
                }
                lblSeqRun.Text = equip.Efem.RunMode.ToString();

                lblRobotStep.Text = equip.Efem.RunMode == Struct.Step.EmEfemRunMode.Home ? equip.Efem.Robot.HomeStepNum.ToString() : equip.Efem.Robot.SeqStepNum.ToString();
                lblLpm1Step.Text = equip.Efem.RunMode == Struct.Step.EmEfemRunMode.Home ? equip.Efem.LoadPort1.HomeStepNum.ToString() : equip.Efem.LoadPort1.SeqStepNum.ToString();
                lblLpm2Step.Text = equip.Efem.RunMode == Struct.Step.EmEfemRunMode.Home ? equip.Efem.LoadPort2.HomeStepNum.ToString() : equip.Efem.LoadPort2.SeqStepNum.ToString();
                lblAlignerStep.Text = equip.Efem.RunMode == Struct.Step.EmEfemRunMode.Home ? equip.Efem.Aligner.HomeStepNum.ToString() : equip.Efem.Aligner.SeqStepNum.ToString();
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

        private void btnSaveIP_Click(object sender, EventArgs e)
        {
            IPAddress ip;
            int port = 0;

            if (IPAddress.TryParse(tbIP.Text, out ip) == false
                || int.TryParse(tbPort.Text, out port) == false)
            {
                MessageBox.Show("IP 또는 Port 입력이상으로 저장할 수 없습니다.");
            }
            else
            {
                equip.InitSetting.EfemIP = ip.ToString();
                equip.InitSetting.EfemPort = port;
                equip.InitSetting.Save();
                MessageBox.Show(string.Format("저장완료! IP:{0} Port:{1}", ip.ToString(), port));
            }

            tbIP.Text = ip.ToString();
            tbPort.Text = port.ToString();
        }

        private void cbBasicCmd_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = (sender as ComboBox).SelectedIndex;

            switch(idx)
            {
                //Robot
                case 0:
                    btnInit.Visible = true;
                    btnReset.Visible = true;
                    btnStat.Visible = true;
                    btnPause.Visible = true;
                    btnResume.Visible = true;
                    btnStop.Visible = true;
                    btnClamp.Visible = false;
                    btnUnClamp.Visible = false;
                    btnDock.Visible = false;
                    btnUnDock.Visible = false;
                    btnOpen.Visible = false;
                    btnClose.Visible = false;
                    btnLoad.Visible = false;
                    btnUnload.Visible = false;
                    btnMapping.Visible = false;
                    btnChmda.Visible = false;
                    btnChmdm.Visible = false;
                    btnPardy.Visible = false;
                    break;

                //LoadPort1
                case 1:
                    btnInit.Visible = true;
                    btnReset.Visible = true;
                    btnStat.Visible = true;
                    btnPause.Visible = false;
                    btnResume.Visible = false;
                    btnStop.Visible = true;
                    btnClamp.Visible = true;
                    btnUnClamp.Visible = true;
                    btnDock.Visible = true;
                    btnUnDock.Visible = true;
                    btnOpen.Visible = true;
                    btnClose.Visible = true;
                    btnLoad.Visible = true;
                    btnUnload.Visible = true;
                    btnMapping.Visible = true;
                    btnChmda.Visible = false;
                    btnChmdm.Visible = false;
                    btnPardy.Visible = false;
                    break;

                //LoadPort2
                case 2:
                    btnInit.Visible = true;
                    btnReset.Visible = true;
                    btnStat.Visible = true;
                    btnPause.Visible = false;
                    btnResume.Visible = false;
                    btnStop.Visible = true;
                    btnClamp.Visible = true;
                    btnUnClamp.Visible = true;
                    btnDock.Visible = true;
                    btnUnDock.Visible = true;
                    btnOpen.Visible = true;
                    btnClose.Visible = true;
                    btnLoad.Visible = true;
                    btnUnload.Visible = true;
                    btnMapping.Visible = true;
                    btnChmda.Visible = false;
                    btnChmdm.Visible = false;
                    btnPardy.Visible = false;
                    break;


                //Aligner
                case 3:
                    btnInit.Visible = true;
                    btnReset.Visible = true;
                    btnStat.Visible = true;
                    btnPause.Visible = false;
                    btnResume.Visible = false;
                    btnStop.Visible = false;
                    btnClamp.Visible = false;
                    btnUnClamp.Visible = false;
                    btnDock.Visible = false;
                    btnUnDock.Visible = false;
                    btnOpen.Visible = false;
                    btnClose.Visible = false;
                    btnLoad.Visible = false;
                    btnUnload.Visible = false;
                    btnMapping.Visible = false;
                    btnChmda.Visible = false;
                    btnChmdm.Visible = false;
                    btnPardy.Visible = true;
                    break;

                //Etc
                case 4:
                    btnInit.Visible = false;
                    btnReset.Visible = true;
                    btnStat.Visible = true;
                    btnPause.Visible = false;
                    btnResume.Visible = false;
                    btnStop.Visible = false;
                    btnClamp.Visible = false;
                    btnUnClamp.Visible = false;
                    btnDock.Visible = false;
                    btnUnDock.Visible = false;
                    btnOpen.Visible = false;
                    btnClose.Visible = false;
                    btnLoad.Visible = false;
                    btnUnload.Visible = false;
                    btnMapping.Visible = false;
                    btnChmda.Visible = true;
                    btnChmdm.Visible = true;
                    btnPardy.Visible = false;
                    break;
            }
        }

        private void btnEFEMStop_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if(btn == btnEFEMStop)
            {
                equip.Efem.ChangeMode(Struct.Step.EmEfemRunMode.Stop);
            }
            else if (btn == btnEFEMHome)
            {
                equip.Efem.ChangeMode(Struct.Step.EmEfemRunMode.Home);
            }
            else if (btn == btnEFEMPause)
            {
                equip.Efem.ChangeMode(Struct.Step.EmEfemRunMode.Pause);
            }
            else if (btn == btnEFEMStart)
            {
                equip.Efem.ChangeMode(Struct.Step.EmEfemRunMode.Start);
            }
        }

        private void btnPick_Click(object sender, EventArgs e)
        {
            if (rbUpper.Checked == false && rbLower.Checked == false)
                return;
            if (tbSlot.Text == "")
                tbSlot.Text = "0";

            EmEfemRobotArm robotArm = rbUpper.Checked == true ? EmEfemRobotArm.Upper : EmEfemRobotArm.Lower;
            int slot = tbSlot.Text == "0" ? 1 : int.Parse(tbSlot.Text);
            EmEfemPort port = StringToEfemPort((string)cbbTransfer.SelectedItem);
            EmEfemTransfer transCommand = EmEfemTransfer.PICK;
            if (port != EmEfemPort.LOADPORT1 && port != EmEfemPort.LOADPORT2)
            {
                slot = 1;
            }
            if (_isUsingProxy)
                equip.Efem.Proxy.StartTransRobot(equip, new EFEMTRANSDataSet(robotArm, transCommand, port, slot));
            else
                EFEMTcp.CmdTransferWafer(robotArm, transCommand, port, slot);

            cbbTransfer.SelectedIndex = -1;
            tbSlot.Text = "0";
        }

        private void btnPlace_Click(object sender, EventArgs e)
        {
            if (rbUpper.Checked == false && rbLower.Checked == false)
                return;
            if (tbSlot.Text == "")
                tbSlot.Text = "0";

            EmEfemRobotArm robotArm = rbUpper.Checked == true ? EmEfemRobotArm.Upper : EmEfemRobotArm.Lower;
            int slot = tbSlot.Text == "0" ? 1 : int.Parse(tbSlot.Text);
            EmEfemPort port = StringToEfemPort((string)cbbTransfer.SelectedItem);
            EmEfemTransfer transCommand = EmEfemTransfer.PLACE;
            if (port != EmEfemPort.LOADPORT1 && port != EmEfemPort.LOADPORT2)
            {
                slot = 1;
            }
            if (_isUsingProxy)
                equip.Efem.Proxy.StartTransRobot(equip, new EFEMTRANSDataSet(robotArm, transCommand, port, slot));
            else
                EFEMTcp.CmdTransferWafer(robotArm, transCommand, port, slot);
            cbbTransfer.SelectedIndex = -1;
            tbSlot.Text = "0";
        }
    }
}
