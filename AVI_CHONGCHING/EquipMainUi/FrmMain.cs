using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EquipMainUi.Struct;
using Dit.Framework.PLC;
using System.Diagnostics;
using System.IO;
using EquipMainUi.Struct.Detail;
using EquipMainUi.Struct.Step;
using EquipMainUi.Struct.BaseUnit;
using EquipMainUi.Monitor;
using EquipMainUi.Setting;
using DitCim;
using DitCim.PLC;
using EquipMainUi.ConvenienceClass;
using EquipView;
using EquipMainUi.UserMessageBoxes;
using EquipMainUi.Struct.Detail.PC;
using Dit.Framework.PMAC;
using Dit.Framework.UI.UserComponent;
using Dit.Framework.Comm;
using EquipMainUi.Struct.Detail.EFEM;
using EquipMainUi.Struct.TransferData;
using EquipMainUi.Struct.Detail.EFEM.Step;
using EquipMainUi.RecipeManagement;
using EquipMainUi.Struct.Detail.HSMS.ReportStruct;
using EquipMainUi.Struct.Detail.HSMS;
using EquipMainUi.PreAligner;
using EquipMainUi.Struct.Detail.EziStepMotor;
using System.Threading;
using EquipMainUi.PreAligner.Recipe;

namespace EquipMainUi
{
    public partial class FrmMain : Form
    {
        private LogicWorker _logicWorker = new LogicWorker();
        FrmOperating tabCtrl_Operator;
        FrmRecipeMgr RecipeMgrForm;

        private Color _clrBlue = Color.FromArgb(144, 200, 246);
        private Color _clrRed = Color.FromArgb(255, 100, 100);
        private bool _isExpending = true;
        private FrmOperatorCall _frmOperatorCall = new FrmOperatorCall();
        private FrmTerminalMsg _frmTerminalMsg = new FrmTerminalMsg();
        private FrmTimerMsg _timeMsg;
        private bool _isTimeMsgPopup;
        private bool _isTeachFirst = false;
        private string _lastLoginedID = string.Empty;
        private ToolTip _tt = new ToolTip();

        public FrmMain()
        {
            InitializeComponent();

            FrmInit f = new FrmInit(10, "Initialize...");
            f.Show();
            this.Visible = false;

            ExtensionUI.AddClickEventLog(this);

            if (GG.Equip.InitSetting.Load(EquipInitSetting.PATH_SETTING) == false)
                GG.Equip.InitSetting.Save(EquipInitSetting.PATH_SETTING); // Default 생성   
            if (GG.Equip.InitSetting.CheckValueConsistency() == false)
            {
                GG.Equip.InitSetting.Save(EquipInitSetting.PATH_SETTING); // Default 생성   
                Application.ExitThread();
                Environment.Exit(0);
            }

            CheckForIllegalCrossThreadCalls = false;

            InitEquipView();

            //PcCtrlSetting ini = new PcCtrlSetting();
            //ini.Save(@"D:\Test.ini");

            //pnlParamSetting.Top = pnlAlarm.Top = pnlEquipDraw.Top = pnlLog.Top = 40;
            //pnlParamSetting.Left = pnlAlarm.Left = pnlEquipDraw.Left = pnlLog.Left = 5;
            //pnlParamSetting.Width = pnlAlarm.Width = pnlEquipDraw.Width = pnlLog.Width = 1424;
            //pnlParamSetting.Height = pnlAlarm.Height = pnlEquipDraw.Height = pnlLog.Height = 687;
            //pnlParamSetting.Width /= 2;
            //pnlParamSetting.Height = 49 * 7;
            //pnlMonitor.Location = pnlParamSetting.Location;
            //pnlMonitor.Size = pnlParamSetting.Size;
            //pnlMonitor.Height -= 49;

            Logger.Log = new SimpleFileLoggerMark5(Path.Combine(GG.Equip.InitSetting.ControlLogBasePath), "CLIENT", 500, 1024 * 1024 * 20, null);
            Logger.IsptLog = new SimpleFileLoggerMark5(Path.Combine(GG.Equip.InitSetting.ControlLogBasePath), "INSPECT", 500, 1024 * 1024, null);
            Logger.TacttimeLog = new SimpleFileLoggerMark5(Path.Combine(GG.Equip.InitSetting.ControlLogBasePath), "TACTTIME", 500, 1024 * 1024, null);
            Logger.EFEMCommLog = new SimpleFileLoggerMark5(Path.Combine(GG.Equip.InitSetting.ControlLogBasePath), "EFEM", 500, 1024 * 1024, lstEFEMAlarm);
            Logger.RobotLog = new SimpleFileLoggerMark5(Path.Combine(GG.Equip.InitSetting.ControlLogBasePath), "EFEM_Robot", 500, 1024 * 1024, null);
            Logger.ExceptionLog = new SimpleFileLoggerMark5(Path.Combine(GG.Equip.InitSetting.ControlLogBasePath), "Exception", 500, 1024 * 1024, null);
            Logger.TransferDataLog = new SimpleFileLoggerMark5(Path.Combine(GG.Equip.InitSetting.ControlLogBasePath), "TransferData", 500, 1024 * 1024, null);
            Logger.AlarmLog = new SimpleFileLoggerMark5(Path.Combine(GG.Equip.InitSetting.ControlLogBasePath), "Alarm", 500, 1024 * 1024, null);
            Logger.OCRBCRLog = new SimpleFileLoggerMark5(Path.Combine(GG.Equip.InitSetting.ControlLogBasePath), "OCRBCR", 500, 1024 * 1024, null);
            Logger.CIMLog = new SimpleFileLoggerMark5(Path.Combine(GG.Equip.InitSetting.ControlLogBasePath), "CIM", 500, 1024 * 1024, null);
            Logger.CSTHistoryLog = new SimpleFileLoggerMark5(Path.Combine(GG.Equip.InitSetting.ControlLogBasePath), "History", 500, 1024 * 1024, null);
            //Logger.Log.AppendLine(LogLevel.Info, "TESTES");

            AlarmMgr.Instance.LstlogAlarms.Add(lstvAlarmClone);
            AlarmMgr.Instance.InitializeAlarmView(lstvAlarmClone);

            AlarmMgr.Instance.LstlogAlarmHistory.Add(lstvAlarmHistory);
            AlarmMgr.Instance.InitializeAlarmView(lstvAlarmHistory);

            AlarmMgr.Instance.InitializeSolution();

            //AlarmMgr.Instatnce.LstvlogAlarm = lstvAlarm2;
            //AlarmMgr.Instatnce.InitializeAlarmView(lstvAlarm2);

            //lstvAlarm = lstvAlarm2;

            ////TEST ADDRESS 사용 
            //X000.SetAddr(2);
            //Y000.SetAddr(2);
            //UMAC.SetAddr(2);

            if (false == GG.TestMode)
            {
                GG.CCLINK = (IVirtualMem)new VirtualCCLinkEx(81, 0xff, -1, "CCLINK");
                //GG.CCLINK = (IVirtualMem)new VirtualMem("CCLINK"); 
                GG.PMAC = (IVirtualMem)(new VirtualPmacExcom("PMAC", GG.Equip.InitSetting.PmacIP, GG.Equip.InitSetting.PmacPort));
                GG.MEM_DIT = (IVirtualMem)new VirtualShare("DIT.CTRL.SHARE.MEM", 102400);
                //GG.HSMS = (IVirtualMem)new VirtualShare("DIT.PLC.HSMS_MEM.S", 102400);
                GG.EZI = (new VirtualEziDirect("EZI", GG.Equip.InitSetting.EziStepMotorPort));
            }
            else
            {
                GG.CCLINK = (IVirtualMem)new VirtualMem("CCLINK");
                GG.PMAC = (IVirtualMem)new VirtualMem("PMAC");
                //GG.PMAC = (IVirtualMem)(new VirtualPmacExcom("PMAC", "192.168.1.200", 1500));
                GG.MEM_DIT = (IVirtualMem)new VirtualShare("DIT.CTRL.SHARE.MEM", 102400);
                //GG.HSMS= (IVirtualMem)new VirtualShare("DIT.PLC.HSMS_MEM.S", 102400);
                GG.EZI = (IVirtualMem)new VirtualMem("EZI");
            }

            int cclinkResult = GG.CCLINK.Open();
            int pmacResult = GG.PMAC.Open();
            int memDitResult = GG.MEM_DIT.Open();

            //int hsmsResult = GG.HSMS.Open();

            AddressMgr.Load(GG.ADDRESS_CC_LINK, GG.CCLINK);
            AddressMgr.Load(GG.ADDRESS_PMAC, GG.PMAC);
            if (GG.IsDitPreAligner)
                AddressMgr.Load(GG.CreateEziAddress(), GG.EZI);

            //GG.Equip.EFUCtrler.Open();
            GG.Equip.SetAddress();
            GG.Equip.Initalize();

            GG.Equip.PMac.Initailize();
            GG.Equip.InspPc.Initialize(GG.MEM_DIT);
            GG.Equip.HsmsPc.Initailize(GG.MEM_DIT);
            GG.Equip.MccPc.Initailize(GG.MEM_DIT);

            GG.Equip.UpdateInstanceSetting();

            _logicWorker.Equip = GG.Equip;
            _logicWorker.Start();

            tmrUiUpdate.Start();

            if (GG.TestMode == false)
            {
                this.SetDesktopBounds(0, 0, 1920, 1080);
                btnExpanding.Text = GG.boChinaLanguage ? "折叠" : "접기";
                _isExpending = true;
            }
            else
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;

            ChangeView(ref pnlEquipDraw);
            GG.Equip.IsUseInterLockOff = false;
            GG.Equip.IsUseDoorInterLockOff = false;
            GG.Equip.IsLongTest = false;
            GG.Equip.IsNoGlassMode = false;
            GG.Equip.IsCycleStop = EmCycleStop.None;

            tabCtrl_Operator = new FrmOperating(GG.Equip);
            tabCtrl_Operator.Name = "FrmOpMain";
            tabCtrl_Operator.TopLevel = false;
            pnlExtend.Controls.Add(tabCtrl_Operator);
            tabCtrl_Operator.Parent = pnlExtend;
            tabCtrl_Operator.Text = string.Empty;
            tabCtrl_Operator.ControlBox = false;
            tabCtrl_Operator.Show();
            tabCtrl_Operator.Location = new Point(1, 658);
            tabCtrl_Operator.Size = new Size(810, 386);
            tabCtrl_Operator.FormBorderStyle = FormBorderStyle.None;
            tabCtrl_Operator.Show();

            tabOperator.SelectedIndex = GG.TestMode == true ? 1 : 0;

            InitMotorDataGridView(dgvMotorState);

            lblLoginID.Text = LoginMgr.Instance.LoginedUser.Id;

            ucrlLPMOption.SetProgressWay((EmProgressWay)GG.Equip.CtrlSetting.LPMProgressWay);
            GG.Equip.Efem.LoadPort1.LoadType = GG.Equip.Efem.LoadPort2.LoadType = (EmLoadType)GG.Equip.CtrlSetting.LPMLoadType;
            GG.Equip.Efem.LoadPort1.ProgressWay = GG.Equip.Efem.LoadPort2.ProgressWay = (EmProgressWay)GG.Equip.CtrlSetting.LPMProgressWay;
            ucrlLPMOption.ProgressChanged += () =>
            {
                string old = GetProgressTypeToString(GG.Equip.Efem.LoadPort2.ProgressWay);

                GG.Equip.Efem.LoadPort1.ProgressWay = GG.Equip.Efem.LoadPort2.ProgressWay = ucrlLPMOption.ProgressWay;
                GG.Equip.CtrlSetting.LPMProgressWay = (int)ucrlLPMOption.ProgressWay;

                GG.Equip.CtrlSetting.Save();

                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? string.Format("Wafer 进行方式变更\n{0} -> {1}", old, GetProgressTypeToString(GG.Equip.Efem.LoadPort2.ProgressWay)) : string.Format("웨이퍼 진행 방식 변경\n{0} -> {1}", old, GetProgressTypeToString(GG.Equip.Efem.LoadPort2.ProgressWay)));
            };
            if (GG.Equip.CtrlSetting.LPMLoadType == 1)
            {
                rdOHT.Checked = true;
            }
            else
            {
                rdManual.Checked = true;
            }
            while (GG.Equip.PMac.IsDoneSaveToUMac(GG.Equip) == false)
            {
                System.Threading.Thread.Sleep(100);
                Application.DoEvents();
            }

            f.Close();
            this.Visible = true;
            _isTeachFirst = true;

            _frmTerminalMsg.Show(); // 여기서 먼저 Show 하지 않고 GG.Equip.AddTerminalMsg 호출 시 무한로딩 발생함
            _frmTerminalMsg.Close();

            InitTerminalMsgUI();
            tabCtrlTransferData.SelectedIndex = 2;

            if (GG.TestMode == true)
            {
                gbSetUpTest.Visible = true;
            }

            tabCtrl_MainMenu.SelectTab("tabp_Progress");

            GG.Equip.PopupTimerMsg = (s, title, msg) =>
            {
                _timeMsg = new FrmTimerMsg(s, title, msg);
                _isTimeMsgPopup = true;
            };

            // China Language 1
            if (GG.boChinaLanguage)
            {
                btnExit.Text = "终止";                          //  종료
                label42.Text = "■ 进行方式";                          //  ■ 진행방식
                label37.Text = "■ 选择检查进行方式";                          //  ■ 검사 진행 방식 선택
                label28.Text = "物流";                          //  물류
                label32.Text = "非物流";                          //  비물류
                label13.Text = "仅上面张";                          //  윗장만
                label19.Text = "仅下面张";                          //  아랫장만
                label43.Text = "全部";                          //  모두
                label45.Text = "直接选择";                          //  직접선택
                btnReviewManual_Copy.Text = "被动 Review";             //수동 리뷰
                btnReviewSkipCopy.Text = "Review Skip";                //리뷰 스킵
                btnEfemDoorOpen.Text = "EFEM DOOR OPEN 可能状态";                  //EFEM 도어 OPEN 가능 상태
                btnDeleteAllInfo.Text = "cassette, wafer, DB data 初始化";                 //카세트, 웨이퍼 DB 초기화
                btnLogPath.Text = "cassette IN OUT LOG 打开文件夹";                       //카세트 IN OUT 로그
                label9.Text = "■ 重新检查";                           //■ 재검사
                btnReservReInsp.Text = "预约重新检查";                  //재검사 예약
                btnReservInsp_Stop.Text = "停止检查";               //검사정지
                btnReservReInspStart.Text = "开始重新检查";             //재검사 시작
                btnReviewManual.Text = "被动 Review";                  //수동 리뷰
                btnReviewSkip.Text = "Review Skip";                    //리뷰 스킵
                label7.Text = "■ 设备";                           //■ 설비
                label11.Text = "■ 操作";                          //■ 조작
                btnInnerWork.Text = "内部作业";                     //내부작업
                btnWorkingLight.Text = "设备内部灯光";                  //작업등
                btnLpm2OHTLdComplete.Text = "投入完毕处理";             // 투입 완료 처리
                btnLpm2OHTUldComplete.Text = "排出完毕处理";            // 배출 완료 처리
                btnLpm2OHTStepReset.Text = "OHT 投入/排出 Step Reset";              // OHT 투입 / 배출 스텝 리셋
                btnLpm2DeepReviewPass.Text = "深度学习Pass";            // 딥 러닝 패스
                btnLpm1OHTLdComplete.Text = "投入完毕处理";             // 투입 완료 처리
                btnLpm1OHTUldComplete.Text = "排出完毕处理";            // 배출 완료 처리
                btnLpm1OHTStepReset.Text = "OHT 投入/排出 Step Reset";              // OHT 투입 / 배출 스텝 리셋
                btnLpm1DeepReviewPass.Text = "深度学习Pass";            // 딥 러닝 패스

                tabp_Progress.Text = "进行现状";                    // 진행 현황
                tabp_Step.Text = "Step现状";                        // 스텝 화면
                label40.Text = "■ 设备状态";                          // ■ 설비 상태
                tabPage3.Text = "全部 Alarm";                         // 전체 알람
                tabPage4.Text = "Reset Alarm";                         // 리셋 알람
                tabPage5.Text = "EFEM Alarm";                         // EFEM 알람
                btnInfoLog.Text = "计算 LOG";                       // 전산 LOG
                tabpAutoDataLog.Text = "计算 LOG";                    // 전산 LOG
                labelWatch.Text = "Scan Time";                           // 스캔타임
            }
        }

        private void InitTerminalMsgUI()
        {
            lstAutoDataLog.Items.Clear();
            GG.Equip.TerminalMsgObserver.CollectionChanged += TerminalMsgObserver_CollectionChanged;
        }

        private void TerminalMsgObserver_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var item = (TerminalMsgItem)e.NewItems[0];
                lstAutoDataLog.Items.Insert(0, item.TimeToString()).SubItems.Add(item.Message);
                if (lstAutoDataLog.Items.Count > 30)
                    lstAutoDataLog.Items.RemoveAt(lstAutoDataLog.Items.Count - 1);

                _frmTerminalMsg.Add(item);

                try
                {
                    lstAutoDataLog.SelectedItems.Clear();
                    lstAutoDataLog.Items[0].Selected = true;
                    lstAutoDataLog.Items[0].EnsureVisible();
                    lstAutoDataLog.Select();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                lstAutoDataLog.Items.RemoveAt(lstAutoDataLog.Items.Count - 1);
            }
        }

        private string GetLoadTypeToString(EmLoadType type)
        {
            if (type == EmLoadType.Manual)
            {
                return GG.boChinaLanguage ? "使用者直接 load/unload" : "사용자 직접 로드/언로드";
            }
            else
            {
                return GG.boChinaLanguage ? "通过OHT的 load/unload" : "OHT를 통한 로드/언로드";
            }
        }
        private string GetProgressTypeToString(EmProgressWay type)
        {
            if (type == EmProgressWay.Mapping)
            {
                return GG.boChinaLanguage ? "检查全部Mapping的 Wafer " : "매핑된 웨이퍼 모두 검사";
            }
            else if (type == EmProgressWay.User)
            {
                return GG.boChinaLanguage ? "使用者直接选择要进行的Wafer" : "진행할 웨이퍼 사용자 직접 선택";
            }
            else if (type == EmProgressWay.OnlyFirst)
            {
                return GG.boChinaLanguage ? "只进行最后的 Wafer" : "마지막 웨이퍼만 진행";
            }
            else if (type == EmProgressWay.OnlyLast)
            {
                return GG.boChinaLanguage ? "只进行第一个Wafer" : "첫번째 웨이퍼만 진행";
            }
            else
            {
                return GG.boChinaLanguage ? "进行方式设置有误" : "진행방식 설정 오류";
            }
        }
        private void InitTooltip(ToolTip t, string title)
        {
            t.ShowAlways = true;
            t.InitialDelay = 100;
            t.IsBalloon = true;
            t.ToolTipTitle = title;
        }

        private void InitMotorDataGridView(DataGridView dgv)
        {
            string[] rowTitle = new string[] {
                "Position", "Speed", "Stress"
            };
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            foreach (ServoMotorPmac s in GG.Equip.Motors)
            {
                dgv.Columns.Add(s.Name, s.Name);
                dgv.Columns[Array.IndexOf(GG.Equip.Motors, s)].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv.Columns[Array.IndexOf(GG.Equip.Motors, s)].Width = 60;
                dgv.Columns[Array.IndexOf(GG.Equip.Motors, s)].ReadOnly = true;
            }
            dgv.Rows.Add(2);
            dgv.RowHeadersWidth = 60;
            foreach (DataGridViewRow r in dgv.Rows)
            {
                r.HeaderCell.Value = rowTitle[r.Index];
                r.Height = 15;
            }

        }
        private void InitEquipView()
        {
            System.Windows.Media.ScaleTransform myScaleTransform = new System.Windows.Media.ScaleTransform();
            System.Windows.Media.TransformGroup myTransformGroup = new System.Windows.Media.TransformGroup();
            System.Windows.Media.TranslateTransform myTranslate = new System.Windows.Media.TranslateTransform();

            // Main
            myScaleTransform = new System.Windows.Media.ScaleTransform();
            myScaleTransform.ScaleY = 0.8;
            myScaleTransform.ScaleX = 0.8;
            myTransformGroup = new System.Windows.Media.TransformGroup();
            myTransformGroup.Children.Add(myScaleTransform);
            ucrlEquipView.LayoutTransform = myTransformGroup;
            //             myTranslate.X = -50;
            //             myTranslate.Y = -50;
            //             myTransformGroup = new System.Windows.Media.TransformGroup();
            //             myTransformGroup.Children.Add(myTranslate);

            ucrlEquipView.LPM1Pushed = () =>
            {
                // 110,650,670 = 로드버튼 눌러서 라이트 커튼 해제
                if (GG.Equip.Efem.LoadPort1.SeqStepNum == EmEFEMLPMSeqStep.S110_WAIT_LOAD_BUTTON_PUSH || 
                GG.Equip.Efem.LoadPort1.SeqStepNum == EmEFEMLPMSeqStep.S650_WAIT_UNLD_BUTTON_PUSH || 
                GG.Equip.Efem.LoadPort1.SeqStepNum == EmEFEMLPMSeqStep.S670_WAIT_UNLD_BUTTON_PUSH)
                {
                    GG.Equip.Efem.LoadPort1.Status.IsLoadButtonPushed = true;
                    GG.Equip.Efem.LoadPort1.Status.IsUnloadButtonPushed = true;
                    Logger.Log.AppendLine(LogLevel.Info, "LPM1 Load Button Click");
                }
            };
            ucrlEquipView.LPM2Pushed = () =>
            {
                // 110,650,670 = 로드버튼 눌러서 라이트 커튼 해제
                if (GG.Equip.Efem.LoadPort2.SeqStepNum == EmEFEMLPMSeqStep.S110_WAIT_LOAD_BUTTON_PUSH ||
                GG.Equip.Efem.LoadPort2.SeqStepNum == EmEFEMLPMSeqStep.S650_WAIT_UNLD_BUTTON_PUSH ||
                GG.Equip.Efem.LoadPort2.SeqStepNum == EmEFEMLPMSeqStep.S670_WAIT_UNLD_BUTTON_PUSH)
                {
                    GG.Equip.Efem.LoadPort2.Status.IsLoadButtonPushed = true;
                    GG.Equip.Efem.LoadPort2.Status.IsUnloadButtonPushed = true;
                    Logger.Log.AppendLine(LogLevel.Info, "LPM2 Load Button Click");
                }
            };
            ucrlEquipView.StartIn = () =>
            {
                // 충칭 미사용
                if (GG.Equip.Efem.LoadPort1.IsLoadConfirmComplete)
                    GG.Equip.Efem.LoadPort1.Status.IsLoadButtonPushed = true;
                if (GG.Equip.Efem.LoadPort2.IsLoadConfirmComplete)
                    GG.Equip.Efem.LoadPort2.Status.IsLoadButtonPushed = true;
                Logger.Log.AppendLine(LogLevel.Info, "StartIn Button Click 1:{0},2:{1}",
                    GG.Equip.Efem.LoadPort1.Status.IsLoadButtonPushed, GG.Equip.Efem.LoadPort2.Status.IsLoadButtonPushed);
            };

            if (GG.IsDitPreAligner)
            {
                ucrlEquipView.ViewPreAlignerControls();
                ucrlEquipView.PreAlignerSettingBtnPushed = () =>
                {
                    foreach (Form openForm in Application.OpenForms)
                    {
                        if (openForm.Name == "FrmPreAligner")
                        {
                            if (openForm.WindowState == FormWindowState.Minimized)
                            {
                                openForm.WindowState = FormWindowState.Normal;
                            }
                            openForm.Activate();
                            return;
                        }
                    }

                    FrmPreAligner f = new FrmPreAligner();
                    f.Show();
                };
            }



            // Mini
            myScaleTransform = new System.Windows.Media.ScaleTransform();
            myScaleTransform.ScaleY = 0.5;
            myScaleTransform.ScaleX = 0.5;
            myTransformGroup = new System.Windows.Media.TransformGroup();
            myTransformGroup.Children.Add(myScaleTransform);

            myTranslate.X = -10;
            myTranslate.Y = 0;
            myTransformGroup = new System.Windows.Media.TransformGroup();
            myTransformGroup.Children.Add(myTranslate);

            GG.Equip.MoveRobotArmEvent += Equip_MoveRobotArmEvent;
        }

        private void Equip_MoveRobotArmEvent(object sender, EventArgs e)
        {
            try
            {
                bool[] value = (bool[])sender;
                ucrlEquipView.MoveRobotArm(value);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog.AppendLine(LogLevel.Error, string.Format("WPF UI 갱신(Move Robot Arm) 예외 발생 : {0}", ex.Message));
            }

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _logicWorker.Stop();
            if (GG.Equip.PreAligner != null)
                GG.Equip.PreAligner.Release();
            base.OnClosing(e);
        }
        /*
        private void btnShowBottomButton_Click(object sender, EventArgs e)
        {
            ButtonLabel btn = sender as ButtonLabel;
            if (btn == null) return;
            pnlCenteringPin.Dock = DockStyle.Fill;
            pnlVaccum.Dock = DockStyle.Fill;
            pnlIonizer.Dock = DockStyle.Fill;
            pnlUMaxXY.Dock = DockStyle.Fill;
            
            //pnlCenteringPin.Visible = (btn == btnCenteringPin);
            //pnlVaccum.Visible = (btn == btnVaccum);
           // pnlIonizer.Visible = (btn == btnIonizer);
           // pnlUMaxXY.Visible = (btn == btnUMacXY);

        }*/

        private void btnlMonitor_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FrmMonitor")
                {
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {
                        openForm.WindowState = FormWindowState.Normal;
                    }
                    openForm.Activate();
                    return;
                }
            }



            FrmMonitor ff = new FrmMonitor(GG.Equip);
            ff.PLC = GG.CCLINK;
            ff.Equip = GG.Equip;
            ff.Show();

            //ChangeView(ref pnlMonitor);
            //FrmXYMonitor f = new FrmXYMonitor();
            //f.StartPosition = FormStartPosition.CenterParent;
            //f.Show();
        }

        private void btnHomePositionOn_DelayClick(object sender, EventArgs e)
        {
            Logger.Log.AppendLine(LogLevel.Info, "{0} 버튼 클릭", "HOME");
            if (GG.Equip.IsHomePositioning == true)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<执行中>\n(Home Position 动作中.)" : "인터락<실행 중>\n(Home Position 동작 중입니다.)");
                Logger.Log.AppendLine(LogLevel.Info, "Home Position 이동중 !!!!!");
                return;
            }
            if (GG.Equip.EquipRunMode == EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<AUTO MODE>\n(AutoMode时，无法移动 Home Position .)" : "인터락<AUTO MODE>\n(AutoMode일때 Home Position 이동을 할 수 없습니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "AutoMode일때 Home Position 이동 안됨 ");
                return;
            }

            GG.Equip.CmdHomePositioning();

            //if (GG.Equip.IsUseExceptAutoTeachKey == false ? (GG.Equip.ModeSelectKey.IsAuto == true || GG.TestMode) : true)
            //{
            //    GG.Equip.CmdHomePositioning();
            //}
            //else
            //{
            //    InterLockMgr.AddInterLock("인터락<TEACH MODE>\n(TEACH 모드에서 HOME STEP을 수행할 수 없습니다.)");
            //    Logger.Log.AppendLine(LogLevel.Warning, "TEACH 모드에서 HOME START 불가함");

            //    GG.Equip.IsInterlock = true;
            //}
        }
        private void nudGantryReviewMove_Click(object sender, EventArgs e)
        {
            //int x = (int)nudGantryReviewX.Value;
            //GG.Equip.ReviY.MovePosition(x, 10);
        }

        private void btnCamCoolingOn_Click(object sender, EventArgs e)
        {
        }
        private void btnCamCoolingOff_Click(object sender, EventArgs e)
        {
        }

        //메소드 - BLOWER ON/OFF
        private void btnMain1BlowerOn_MouseDown(object sender, MouseEventArgs e)
        {
            //GG.Equip.MainBlowerOn.B_InOnOff.vBit = true;
        }
        private void btnMain1BlowerOn_MouseUp(object sender, MouseEventArgs e)
        {
            //GG.Equip.MainBlowerOn.B_InOnOff.vBit = false;
        }

        public void SetEnabled(Panel pnl, bool value)
        {
            foreach (Control btn in pnl.Controls)
            {
                if (btn is ButtonDelay2)
                    ((ButtonDelay2)btn).Enabled = value;
                else if (btn is Panel)
                    SetEnabled((Panel)btn, value);
            }
        }
        //메소드 - UI 업데이트...
        FrmInterLock _frmInterLock = new FrmInterLock();
        FrmCheck _frmCheck = new FrmCheck();

        private void UpdateModeSelectKey()
        {
            lblOuterMode.Text = GG.Equip.ModeSelectKey.IsAuto ? "Auto" : "Teach";
            lblEFEMAutoMode.Text = GG.Equip.Efem.Status.IsModeSwitchAuto ? "Auto" : "Teach";

            string autoCond = string.Empty; //jys:: 9글자 제한(한글)
            if (GG.Equip.IsEmergency) autoCond = GG.boChinaLanguage ? "设备 EMS 正常化" : "설비EMS정상화";
            else if (GG.Equip.Efem.Status.IsEMO) autoCond = GG.boChinaLanguage ? "EFEM-EMS 正常化" : "EFEM-EMS정상화";
            else if (GG.Equip.IsDoorOpen) autoCond = "Door Close";
            else if (GG.Equip.Efem.Status.IsDoorClose == false) autoCond = "EFEM Door Close";
            else if (GG.Equip.IsInnerWorkOn) autoCond = GG.boChinaLanguage ? "内部作业 OFF" : "내부작업 Off";
            else autoCond = GG.boChinaLanguage ? "可以 Auto Mode " : "AutoMode가능";
            lblAutoCondition.Text = autoCond;
        }

        private void UpdateEquipStateList()
        {
            Dictionary<string, bool> modes = new Dictionary<string, bool>();
            modes[GG.boChinaLanguage ? "PC无视应答" : "PC응답무시"] = GG.InspTestMode;
            //modes["리뷰응답무시"] = GG.ReviTestMode;
            modes["Interlock Off"] = GG.Equip.IsUseInterLockOff;
            modes["Door Interlock Off"] = GG.Equip.IsUseDoorInterLockOff;
            modes[GG.boChinaLanguage ? "Pio Test 模式" : "Pio Test 모드"] = GG.PioTestMode;
            modes[GG.boChinaLanguage ? "No Wafer 模式" : "No Wafer 모드"] = GG.EfemNoWafer;
            modes[GG.boChinaLanguage ? "Review Skip 预约" : "리뷰스킵예약"] = GG.Equip.IsReviewSkip == EmReviewSkip.Request;
            modes[GG.boChinaLanguage ? "手动 Review 预约" : "수동리뷰예약"] = GG.Equip.IsReviewManual == EmReviewManual.Request;
            //            modes["Recipe Auto Change Mode"] = GG.Equip.CtrlSetting.AutoRecipeChange == 1;
            modes[GG.boChinaLanguage ? "检查机 Long Run 模式" : "검사기 Long Run 모드"] = GG.Equip.IsLongTest;
            modes[GG.boChinaLanguage ? "IO Test 模式 (Logic 停止)" : "IO TEST 모드 (로직정지)"] = GG.Equip.IsIoTestMode;
            modes[GG.boChinaLanguage ? "Liftpin/Centering/VacuumBlower 除外" : "Liftpin/Centering/VacuumBlower 제외"] = GG.Equip.IsUseLiftpinVacuumCenteringExMode;
            modes[GG.boChinaLanguage ? "Glass Type 设置模式" : "글라스 타입 설정 모드"] = GG.Equip.IsUseSelectInspOrder;
            modes[GG.boChinaLanguage ? "EFEM 未使用模式" : "EFEM 미사용 모드"] = GG.EfemNoUse;
            modes[GG.boChinaLanguage ? "EFEM Long Run 模式" : "EFEM 롱런 모드"] = GG.EfemLongRun;
            modes[GG.boChinaLanguage ? "CST ID 使用者输入模式" : "CST ID 사용자 입력 모드"] = GG.Equip.CtrlSetting.IsRFKeyIn;
            modes[GG.boChinaLanguage ? "OCR 未使用模式" : "OCR 미사용 모드"] = !GG.Equip.CtrlSetting.UseOCR;
            modes[GG.boChinaLanguage ? "BCR 未使用模式" : "BCR 미사용 모드"] = !GG.Equip.CtrlSetting.UseBCR;
            modes[GG.boChinaLanguage ? "Air Knife 使用模式" : "Air Knife 사용 모드"] = GG.Equip.CtrlSetting.AirKnifeUse;

            KeyValuePair<string, bool> df = modes.FirstOrDefault(f => f.Value);
            bool someModeOn = modes.FirstOrDefault(f => f.Value).Value;

            foreach (KeyValuePair<string, bool> value in modes)
            {
                int idx = lstOperationMode.Items.IndexOfKey(value.Key);
                if (value.Value)
                {
                    if (idx < 0)
                        lstOperationMode.Items.Add(value.Key).Name = value.Key;
                }
                else
                {
                    if (idx >= 0)
                        lstOperationMode.Items.RemoveAt(idx);
                }
            }
        }

        Dictionary<EmCycleStop, string> _cycleStopText = new Dictionary<EmCycleStop, string>()
        {
            {EmCycleStop.None, GG.boChinaLanguage ? "Cycle Stop\n(未使用)" : "Cycle Stop\n(미사용)"},
            {EmCycleStop.Request, GG.boChinaLanguage ? "Cycle Stop\n(预约)" : "Cycle Stop\n(예약)"},
            {EmCycleStop.Complete, GG.boChinaLanguage ? "Cycle Stop\n(完毕)" : "Cycle Stop\n(완료)"}
        };
        //FrmTester - EmInspOrder 순서 모두 맞출 것.
        Dictionary<EmInspOrder, string> _glassTypeText = new Dictionary<EmInspOrder, string>()
        {
            {EmInspOrder.ORIGIN, GG.boChinaLanguage ? "原盘 Glass" : "원판 글라스"},
            {EmInspOrder.FRONT_REAR, GG.boChinaLanguage ? "分盘 Glass" : "분판 글라스"},
            {EmInspOrder.REARONLY, GG.boChinaLanguage ? "后面侧分盘" : "후면측 분판"},
            {EmInspOrder.FRONTONLY, GG.boChinaLanguage ? "前面侧分盘" : "전면측 분판"},
            {EmInspOrder.NOT, GG.boChinaLanguage ? "无Glass" : "글라스 없음"}
        };
        PlcTimerEx delayAlarmLogShow = new PlcTimerEx("", false);
        private void tmrUiUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (GG.PrivilegeTestMode == false)
                {
                    if (Math.Abs((PcDateTime.Now - ExtensionUI.LogoutTime).TotalMinutes) > 10 && LoginMgr.Instance.IsLogined())
                        LoginMgr.Instance.Logout(GG.Equip);
                }
                lock (InterLockMgr.LstInterLock)
                {
                    if (_frmInterLock.Visible == false)
                    {
                        if (InterLockMgr.LstInterLock.Count > 0)
                        {
                            InterlockMsg interlockMsg = InterLockMgr.LstInterLock.Dequeue();

                            _frmInterLock.InterlockMsg = interlockMsg.Interlock;
                            _frmInterLock.DetailMsg = interlockMsg.Detail;
                            _frmInterLock.Show();

                            InterLockMgr.LstInterLock.Clear();
                        }
                    }
                }

                lock (CheckMgr.LstCheck)
                {
                    if (_frmCheck.Visible == false)
                    {
                        if (CheckMgr.LstCheck.Count > 0)
                        {
                            CheckMsgUnit chkMsgUnit = CheckMgr.LstCheck.Dequeue();
                            string checkMsg = chkMsgUnit.CheckMsg;
                            _frmCheck.lblCheckState.Text = chkMsgUnit.IsSucceed ? "성공" : "실패";

                            _frmCheck.CheckMsg = checkMsg;
                            _frmCheck.Show();
                            CheckMgr.LstCheck.Clear();
                        }
                    }
                }
                if (LoginMgr.Instance.LoginedUser.Id != _lastLoginedID)
                {
                    ChangeUIFollowingLogin();
                    _lastLoginedID = LoginMgr.Instance.LoginedUser.Id;
                    if (LoginMgr.Instance.LoginedUser.Id == "0")
                        lblLoginID.Text = "LOGIN";
                    else
                        lblLoginID.Text = string.Format("ID : {0}{1}[{2}]", LoginMgr.Instance.LoginedUser.Id, Environment.NewLine, LoginMgr.Instance.LoginedUser.Level.ToString());
                }

                if (GG.EfemLongRun || GG.Equip.IsLongTest)
                {
                    lblLongCount.Visible = true;
                    lblLongCount.Text = GG.Equip.LongRunCount.ToString();
                }
                else
                    lblLongCount.Visible = false;

                //btnLpm1OHTComplete.Visible = btnLpm1OHTStepReset.Visible = GG.Equip.Efem.LoadPort1.OHTpio.IsRunning && GG.Equip.Efem.LoadPort1.LoadType == EmLoadType.OHT;
                //btnLpm2OHTComplete.Visible = btnLpm2OHTStepReset.Visible = GG.Equip.Efem.LoadPort2.OHTpio.IsRunning && GG.Equip.Efem.LoadPort2.LoadType == EmLoadType.OHT;

                if (GG.Equip.EquipRunMode == EmEquipRunMode.Auto
                    && GG.Equip.StLoading.IsManualLoadingWait == true
                    && GG.Equip.IsWaferDetect == EmGlassDetect.NOT)
                {
                    CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "Wafer放上后按开始， Detect Sensor全部感应时进行 " : "웨이퍼를 올리고 시작을 눌러주세요 (Detect Sensor 모두 감지 시 진행)");
                }

                lblLpm1Pogress.Text = GG.Equip.GetLpmProgress(1);
                lblLpm1.BackColor = GG.Equip.Efem.LoadPort1.Status.IsFoupExist ? _clrRed : Color.White;
                lblLpm2Pogress.Text = GG.Equip.GetLpmProgress(2);
                lblLpm2.BackColor = GG.Equip.Efem.LoadPort2.Status.IsFoupExist ? _clrRed : Color.White;

                btnEfemDoorOpen.BackColor = (GG.Equip.EfemDoor01.IsSolOnOff && GG.Equip.EfemDoor02.IsSolOnOff) == false ? _clrRed : SystemColors.Control;
                btnEfemDoorOpen.Text = btnEfemDoorOpen.BackColor == _clrRed ? GG.boChinaLanguage ? "EFEM Door Open 可能状态" : "EFEM 도어 OPEN 가능" : GG.boChinaLanguage ? "EFEM Door Open 不可能状态" : "EFEM 도어 OPEN 불가능";

                // China Language 2
                {
                    labelMode.Text = (GG.Equip.ModeSelectKey.IsAuto && GG.Equip.Efem.Status.IsModeSwitchAuto) ? GG.boChinaLanguage ? "自动模式" : "자동 모드" : GG.boChinaLanguage ? "手动模式" : "수동 모드";

                }

                lblLoginID.Flicker = LoginMgr.Instance.IsLogined() == false;

                rdOHT.Enabled = (GG.Equip.CimMode == EmCimMode.Remote && GG.Equip.EquipRunMode == EmEquipRunMode.Manual) ? true : false;
                rdManual.Enabled = GG.Equip.EquipRunMode == EmEquipRunMode.Auto ? false : true;
                rdManual.Checked = GG.Equip.Efem.LoadPort1.LoadType == EmLoadType.Manual;
                rdOHT.Checked = GG.Equip.Efem.LoadPort1.LoadType == EmLoadType.OHT;
                //test
                {
                    lblRobotWait.Text = GG.Equip.Efem.Robot.LpmWaitTime.ToString();
                }

                UpdateModeSelectKey();
                UpdateEquipStateList();

                labelDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                lblCimStatus.Text = GG.Equip.CimMode.ToString();
                lblCimStatus.BackColor = GG.Equip.CimMode == EmCimMode.OffLine ? Color.Gainsboro : GG.Equip.CimMode == EmCimMode.Local ? Color.Orange : Color.FromArgb(144, 200, 246);

                lblLPM1Recipe.Text = GG.Equip.LPM1Recipe;
                lblLPM2Recipe.Text = GG.Equip.LPM2Recipe;

                lblPMAC.BackColor = GG.Equip.PMac.XB_PmacAlive.vBit == true ? _clrRed : Color.White;
                lblInspector.BackColor = IsptAddrB.XB_ServerAlive.vBit == true ? _clrRed : Color.White;
                lblSyncSvr.BackColor = IsptAddrB.XB_ServerAlive.vBit == true ? _clrRed : Color.White;
                lblInspRun.BackColor = (GG.Equip.StScanning.IsScanningStep) ? _clrRed : Color.Gainsboro;
                lblRvRun.BackColor = (GG.Equip.StReviewing.IsReviewingStep) ? _clrRed : Color.Gainsboro;
                lblIsInspUseMotor.BackColor = (IsptAddrB.YB_MotorInterlockOffState.vBit == true && GG.Equip.PMac.XB_InspServerUseAck.vBit == true) ? _clrRed : Color.Gainsboro;
                lblEFEM.BackColor = GG.Equip.Efem.Proxy.IsConnected == true ? _clrRed : Color.Gainsboro;
                lblReviewManual.BackColor = IsptAddrB.YB_ReviewManualMode.vBit ? _clrRed : Color.Gainsboro;

                lblRFID1Stat.BackColor = (GG.Equip.Efem.LoadPort1.RFR.IsOpen() && (DateTime.Now.Millisecond >= 300)) ? _clrRed : Color.Gainsboro;
                lblRFID2Stat.BackColor = (GG.Equip.Efem.LoadPort2.RFR.IsOpen() && (DateTime.Now.Millisecond >= 300)) ? _clrRed : Color.Gainsboro;
                lblOCRStat.BackColor = (GG.Equip.OCR.IsConnected && (DateTime.Now.Millisecond >= 300)) ? _clrRed : Color.Gainsboro;
                lblBCRStat.BackColor = ((GG.Equip.BCR1.IsOpen() || GG.Equip.BCR2.IsOpen()) && (DateTime.Now.Millisecond >= 300)) ? _clrRed : Color.Gainsboro;
                lblEFUStat.BackColor = GG.Equip.EFUCtrler.IsOpen() && (DateTime.Now.Millisecond >= 300) ? _clrRed : Color.Gainsboro;

                #region Main Button
                btnAlarmReset.Flicker = GG.Equip.IsHeavyAlarm == true || GG.Equip.IsLightAlarm == true;
                btnBuzzerStop.Flicker = (GG.Equip.BuzzerK1.IsSolOnOff || GG.Equip.BuzzerK2.IsSolOnOff || GG.Equip.BuzzerK3.IsSolOnOff || GG.Equip.BuzzerK4.IsSolOnOff) && GG.TestMode == false;
                if (GG.Equip.IsReviewManual == EmReviewManual.InterLock)
                {
                    btnReviewManual_Copy.Flicker = btnReviewManual.Flicker = false;
                    btnReviewManual_Copy.BackColor = btnReviewManual.BackColor = _clrBlue;
                }
                else if (GG.Equip.IsReviewManual == EmReviewManual.ManualOn)
                {
                    btnReviewManual_Copy.Flicker = btnReviewManual.Flicker = false;
                    btnReviewManual_Copy.BackColor = btnReviewManual.BackColor = _clrRed;
                }
                else if (GG.Equip.IsReviewManual == EmReviewManual.Request)
                {
                    btnReviewManual_Copy.Flicker = btnReviewManual.Flicker = GG.Equip.IsReviewManual == EmReviewManual.Request;
                }
                else
                {
                    btnReviewManual_Copy.Flicker = btnReviewManual.Flicker = false;
                    btnReviewManual_Copy.BackColor = btnReviewManual.BackColor = Color.Transparent;
                }
                btnReviewSkipCopy.OnOff = btnReviewSkip.OnOff = GG.Equip.IsReviewSkip == EmReviewSkip.SkipOn;
                btnReviewSkipCopy.Flicker = btnReviewSkip.Flicker = GG.Equip.IsReviewSkip == EmReviewSkip.Request;

                //btnPm_Copy.OnOff = btnPm.OnOff = GG.Equip.IsPM;
                btnAutoStart_Copy.OnOff =
                    GG.Equip.Efem.RunMode == EmEfemRunMode.Start || GG.Equip.Efem.RunMode == EmEfemRunMode.Home || GG.Equip.EquipRunMode == EmEquipRunMode.Auto;
                btnAutoStart_Copy.Flicker = GG.Equip.CimMode == EmCimMode.Remote
                    && GG.Equip.Efem.LoadPort2.LoadType == EmLoadType.Manual
                    && (GG.Equip.Efem.LoadPort1.IsLoadConfirmComplete || GG.Equip.Efem.LoadPort2.IsLoadConfirmComplete);
                btnPause_Copy.OnOff =
                    (GG.Equip.IsPause && (GG.Equip.Efem.RunMode == EmEfemRunMode.Pause || GG.Equip.Efem.RunMode == EmEfemRunMode.Stop || GG.EfemNoUse))
                    || (GG.Equip.EquipRunMode == EmEquipRunMode.Manual && GG.Equip.Efem.RunMode == EmEfemRunMode.Pause);
                btnManualMode_Copy.OnOff =
                    GG.Equip.EquipRunMode == EmEquipRunMode.Manual && (GG.Equip.Efem.RunMode == EmEfemRunMode.Stop || GG.Equip.Efem.RunMode == EmEfemRunMode.Pause);

                btnAVIStartStatus.OnOff = GG.Equip.EquipRunMode == EmEquipRunMode.Auto;
                btnAVIPauseStatus.OnOff = GG.Equip.IsPause;
                btnAVIManualStatus.OnOff = GG.Equip.EquipRunMode == EmEquipRunMode.Manual;

                btnEFEMStart.OnOff = GG.Equip.Efem.RunMode == EmEfemRunMode.Start || GG.Equip.Efem.RunMode == EmEfemRunMode.Home;
                btnEFEMPause.OnOff = GG.Equip.Efem.RunMode == EmEfemRunMode.Pause;
                btnEFEMManual.OnOff = GG.Equip.Efem.RunMode == EmEfemRunMode.Stop;
                btnEFEMCycleStop.OnOff = GG.Equip.Efem.RunMode == EmEfemRunMode.CycleStop;

                btnCycleStop.OnOff = GG.Equip.IsCycleStop != EmCycleStop.None;
                btnCycleStop.Flicker = GG.Equip.IsCycleStop == EmCycleStop.Request;
                if (btnCycleStop.Text != _cycleStopText[GG.Equip.IsCycleStop])
                    btnCycleStop.Text = _cycleStopText[GG.Equip.IsCycleStop];
                btnReservReInsp.OnOff = GG.Equip.IsReserveReInsp == EmReserveReInsp.READY || GG.Equip.IsReserveReInsp == EmReserveReInsp.RESERVE /*|| GG.Equip.IsReserveReInsp == EmReserveReInsp.SRTART*/;
                btnReservReInsp.Flicker = GG.Equip.IsReserveReInsp == EmReserveReInsp.RESERVE;
                btnReservReInspStart.OnOff = GG.Equip.IsReserveReInsp == EmReserveReInsp.START;
                btnReservReInspStart.Flicker = GG.Equip.StMain.StepNum == EmMN_NO.REINSPECT_START_WAIT;
                btnReservInsp_Stop.Flicker = GG.Equip.IsReserveInsp_STOP == EmReserveInsp_Stop.RESERVE;

                if (LoginMgr.Instance.IsLogined())
                {
                    ucrlLPMOption.Enabled = GG.Equip.EquipRunMode == EmEquipRunMode.Manual;
                    lblLPM1Recipe.Enabled = true;//GG.Equip.Efem.LoadPort1.LoadType == EmLoadType.Manual;
                    lblLPM2Recipe.Enabled = true;//GG.Equip.Efem.LoadPort2.LoadType == EmLoadType.Manual;
                }
                else
                    ucrlLPMOption.Enabled = lblLPM1Recipe.Enabled = lblLPM2Recipe.Enabled = false;

                btnTTTM.BackColor = GG.Equip.IsTTTMMode ? _clrRed : Color.Gainsboro;
                #endregion
                #region 조작
                btnWorkingLight.OnOff = GG.Equip.WorkingLight.Stage.IsSolOnOff;
                btnDoorOpenClose.OnOff = GG.Equip.TopDoor03.IsSolOnOff;
                #endregion
                #region 서보 상태
                UpdateMotorStateAt(dgvMotorState);
                #endregion
                #region 설비 이미지
                UpdateEquipImage(ucrlEquipView);
                #endregion
                #region 스텝화면            


                //Interlock
                btndGlassUnloading.Flicker = GG.Equip.IsGlassUnloading;

                lblMainStep.Text = GG.Equip.StMain.StepNum.ToString();
                lblMainStep.BackColor = GG.Equip.StMain.StepNum != EmMN_NO.WAIT ? Color.Silver : Color.White;
                lblMainStep.ForeColor = GG.Equip.StMain.StepNum != EmMN_NO.WAIT ? Color.Red : Color.Black;

                lblHomingStep.Text = GG.Equip.StHoming.StepNum.ToString();
                lblHomingStep.BackColor = GG.Equip.StHoming.StepNum != EmHM_NO.S000_STEP_WAIT ? Color.Silver : Color.White;
                lblHomingStep.ForeColor = GG.Equip.StHoming.StepNum != EmHM_NO.S000_STEP_WAIT ? Color.Red : Color.Black;

                lblLoadingStep.Text = GG.Equip.StLoading.StepNum.ToString();
                lblLoadingStep.BackColor = GG.Equip.StLoading.StepNum != EmLD_NO.S000_LOADING_WAIT ? Color.Silver : Color.White;
                lblLoadingStep.ForeColor = GG.Equip.StLoading.StepNum != EmLD_NO.S000_LOADING_WAIT ? Color.Red : Color.Black;

                lblTTTMStep.Text = GG.Equip.StTTTM.StepNum.ToString();
                lblTTTMStep.BackColor = GG.Equip.StTTTM.StepNum != EmTTTM_NO.S000_TTTM_WAIT ? Color.Silver : Color.White;
                lblTTTMStep.ForeColor = GG.Equip.StTTTM.StepNum != EmTTTM_NO.S000_TTTM_WAIT ? Color.Red : Color.Black;
                lblTTTMCurTime.Text = GG.Equip.StTTTM.StepNum == EmTTTM_NO.S030_TTTM_COMPLETE_WAIT ? string.Format("{0:0.0} s", (double)GG.Equip.StTTTM.TTTMPassTime / 1000) : "-";
                lblTTTMTimeLimit.Text = string.Format("{0:0.0} s", (double)GG.Equip.CtrlSetting.Insp.TTTMOvertime / 1000);

                lblScanningStep.Text = GG.Equip.StScanning.StepNum.ToString();
                lblScanningStep.BackColor = GG.Equip.StScanning.StepNum != EmSC_NO.S000_SCAN_WAIT ? Color.Silver : Color.White;
                lblScanningStep.ForeColor = GG.Equip.StScanning.StepNum != EmSC_NO.S000_SCAN_WAIT ? Color.Red : Color.Black;
                lblScanNum.Text = (GG.Equip.StScanning.ScanIndex + 1).ToString();
                lblScanCount.Text = "";// GG.Equip.SeqRecipe.InspType == EmInspectionType.OneD ? GG.Equip.SeqRecipe.Insp1DInfo.ScanCount.ToString() : GG.Equip.SeqRecipe.Insp2DInfo.ScanCount.ToString();
                lblInspCurTime.Text = GG.Equip.StScanning.StepNum == EmSC_NO.S030_SCAN_COMPLETE_WAIT ? string.Format("{0:0.0} s", (double)GG.Equip.StScanning.ScanPassTime / 1000) : "-";
                lblInspTimeLimit.Text = string.Format("{0:0.0} s", (double)GG.Equip.CtrlSetting.Insp.ScanOvertime / 1000);

                lblReviewStep.Text = GG.Equip.StReviewing.StepNum.ToString();
                lblReviewStep.BackColor = GG.Equip.StReviewing.StepNum != EmRV_NO.S000_REVIEW_WAIT ? Color.Silver : Color.White;
                lblReviewStep.ForeColor = GG.Equip.StReviewing.StepNum != EmRV_NO.S000_REVIEW_WAIT ? Color.Red : Color.Black;

                lblReviewCurTime.Text = GG.Equip.StReviewing.StepNum == EmRV_NO.S040_REVIEW_COMPLETE_WAIT ? string.Format("{0:0.0} s", (double)GG.Equip.StReviewing.ReviewPassTime / 1000) : "-";
                lblReviewTimeLimit.Text = string.Format("{0:0.0} s", (double)GG.Equip.CtrlSetting.Insp.ReviewOvertime / 1000);

                lblUnloadingStep.Text = GG.Equip.StUnloading.StepNum.ToString();
                lblUnloadingStep.BackColor = GG.Equip.StUnloading.StepNum != EmUD_NO.S000_UNLOADING_WAIT ? Color.Silver : Color.White;
                lblUnloadingStep.ForeColor = GG.Equip.StUnloading.StepNum != EmUD_NO.S000_UNLOADING_WAIT ? Color.Red : Color.Black;

                lblRobotStep.Text = GG.Equip.Efem.RunMode == EmEfemRunMode.Home ? GG.Equip.Efem.Robot.HomeStepNum.ToString() : GG.Equip.Efem.Robot.SeqStepNum.ToString();
                lblLpm1Step.Text = GG.Equip.Efem.RunMode == EmEfemRunMode.Home ? GG.Equip.Efem.LoadPort1.HomeStepNum.ToString() : GG.Equip.Efem.LoadPort1.SeqStepNum.ToString();
                lblLpm2Step.Text = GG.Equip.Efem.RunMode == EmEfemRunMode.Home ? GG.Equip.Efem.LoadPort2.HomeStepNum.ToString() : GG.Equip.Efem.LoadPort2.SeqStepNum.ToString();
                lblAlignerStep.Text = GG.Equip.Efem.RunMode == EmEfemRunMode.Home ? GG.Equip.Efem.Aligner.HomeStepNum.ToString() : GG.Equip.Efem.Aligner.SeqStepNum.ToString();

                lblPioUpeerStep.Text = GG.Equip.PioI2ARecv.StepPioRecv.ToString();
                lblPioUpeerStep.Text = GG.Equip.PioA2ISend.StepPioSend.ToString();

                lblPioLPM1OHTStep.Text = GG.Equip.PioLPM1.Step.ToString();
                lblPioLPM2OHTStep.Text = GG.Equip.PioLPM2.Step.ToString();

                lblRobotStep.BackColor = GG.Equip.Efem.Robot.RunMode == EmEfemRunMode.Start || GG.Equip.Efem.Robot.RunMode == EmEfemRunMode.Home ? Color.Silver : Color.White;
                lblRobotStep.ForeColor = GG.Equip.Efem.Robot.RunMode == EmEfemRunMode.Start || GG.Equip.Efem.Robot.RunMode == EmEfemRunMode.Home ? Color.Red : Color.Black;
                lblLpm1Step.BackColor = GG.Equip.Efem.LoadPort1.RunMode == EmEfemRunMode.Start || GG.Equip.Efem.LoadPort1.RunMode == EmEfemRunMode.Home ? Color.Silver : Color.White;
                lblLpm1Step.ForeColor = GG.Equip.Efem.LoadPort1.RunMode == EmEfemRunMode.Start || GG.Equip.Efem.LoadPort1.RunMode == EmEfemRunMode.Home ? Color.Red : Color.Black;
                lblLpm2Step.BackColor = GG.Equip.Efem.LoadPort2.RunMode == EmEfemRunMode.Start || GG.Equip.Efem.LoadPort2.RunMode == EmEfemRunMode.Home ? Color.Silver : Color.White;
                lblLpm2Step.ForeColor = GG.Equip.Efem.LoadPort2.RunMode == EmEfemRunMode.Start || GG.Equip.Efem.LoadPort2.RunMode == EmEfemRunMode.Home ? Color.Red : Color.Black;
                lblAlignerStep.BackColor = GG.Equip.Efem.Aligner.RunMode == EmEfemRunMode.Start || GG.Equip.Efem.Aligner.RunMode == EmEfemRunMode.Home ? Color.Silver : Color.White;
                lblAlignerStep.ForeColor = GG.Equip.Efem.Aligner.RunMode == EmEfemRunMode.Start || GG.Equip.Efem.Aligner.RunMode == EmEfemRunMode.Home ? Color.Red : Color.Black;

                lblPioLPM1OHTStep.BackColor = lblPioLPM2OHTStep.BackColor = GG.Equip.EquipRunMode == EmEquipRunMode.Auto && GG.Equip.Efem.LoadPort1.LoadType == EmLoadType.OHT ? Color.Silver : Color.White;
                lblPioLPM1OHTStep.ForeColor = lblPioLPM2OHTStep.ForeColor = GG.Equip.EquipRunMode == EmEquipRunMode.Auto && GG.Equip.Efem.LoadPort1.LoadType == EmLoadType.OHT ? Color.Red : Color.Black;

                #endregion
                #region 물류정보
                SetWaferInfo(pGridWaferInfoAVI, GG.Equip.TransferUnit.LowerWaferKey);
                SetWaferInfo(pGridWaferInfoAligner, GG.Equip.Efem.Aligner.LowerWaferKey);
                pGridCstInfoLPM1.SelectedObject = GG.Equip.Efem.LoadPort1.CstKey;
                pGridCstInfoLPM2.SelectedObject = GG.Equip.Efem.LoadPort2.CstKey;
                SetWaferInfo(pGridWaferInfoLower, GG.Equip.Efem.Robot.LowerWaferKey);
                SetWaferInfo(pGridWaferInfoUpper, GG.Equip.Efem.Robot.UpperWaferKey);
                if (GG.Equip.TransferUnit.LowerWaferKey.CstID != string.Empty)
                    btnEquipmentTransferData_Copy.BackColor = GG.Equip.TransferUnit.LowerWaferKey.CstID == GG.Equip.Efem.LoadPort1.CstKey.ID ? Color.LightBlue : (GG.Equip.TransferUnit.LowerWaferKey.CstID == GG.Equip.Efem.LoadPort2.CstKey.ID ? Color.LightGreen : Color.Transparent);
                else
                    btnEquipmentTransferData_Copy.BackColor = Color.Transparent;
                if (GG.Equip.Efem.Aligner.LowerWaferKey.CstID != string.Empty)
                    btnAlignerTransferData_Copy.BackColor = GG.Equip.Efem.Aligner.LowerWaferKey.CstID == GG.Equip.Efem.LoadPort1.CstKey.ID ? Color.LightBlue : (GG.Equip.Efem.Aligner.LowerWaferKey.CstID == GG.Equip.Efem.LoadPort2.CstKey.ID ? Color.LightGreen : Color.Transparent);
                else
                    btnAlignerTransferData_Copy.BackColor = Color.Transparent;
                if (GG.Equip.Efem.Robot.LowerWaferKey.CstID != string.Empty)
                    btnLowerTransferData_Copy.BackColor = GG.Equip.Efem.Robot.LowerWaferKey.CstID == GG.Equip.Efem.LoadPort1.CstKey.ID ? Color.LightBlue : (GG.Equip.Efem.Robot.LowerWaferKey.CstID == GG.Equip.Efem.LoadPort2.CstKey.ID ? Color.LightGreen : Color.Transparent);
                else
                    btnLowerTransferData_Copy.BackColor = Color.Transparent;
                if (GG.Equip.Efem.Robot.UpperWaferKey.CstID != string.Empty)
                    btnUpperTransferData_Copy.BackColor = GG.Equip.Efem.Robot.UpperWaferKey.CstID == GG.Equip.Efem.LoadPort1.CstKey.ID ? Color.LightBlue : (GG.Equip.Efem.Robot.UpperWaferKey.CstID == GG.Equip.Efem.LoadPort2.CstKey.ID ? Color.LightGreen : Color.Transparent);
                else
                    btnUpperTransferData_Copy.BackColor = Color.Transparent;
                btnLPM1TransferData_Copy.BackColor = Color.LightBlue;
                btnLPM2TransferData_Copy.BackColor = Color.LightGreen;
                #endregion

                lblCurrScanTime.Text = string.Format("{0} ms", _logicWorker.CurrScanTime);
                lblMaxScanTime.Text = string.Format("{0} ms", _logicWorker.MaxScanTime);
                lblAvgScanTime.Text = string.Format("{0} ms", _logicWorker.AvgScanTime);

                lblEquipState.Text = GG.Equip.CimReportStatus.ToString();
                lblProcState.Text = GG.Equip.CimReportAutoManual.ToString();

                OpcallProc();
                if (GG.TestMode == false)
                    ScreenLock();

                //btnDeleteAllInfo.Visible = GG.EfemLongRun ? true : false;

                btnMotorLongRun.OnOff = GG.Equip.MotorLongRun.step != EmLONGRUN_NO.S000_LONG_RUN_WAIT;
                lblMotorLongRun.Text = GG.Equip.MotorLongRun.count.ToString();

                if (tabControlAlarm.SelectedIndex != 0 && delayAlarmLogShow.IsStart == false)
                {
                    delayAlarmLogShow.Start(300, 0); // 자동 전환 5분으로 변경 요청 21-02-04
                }
                if (delayAlarmLogShow)
                {
                    delayAlarmLogShow.Stop();
                    tabControlAlarm.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog.AppendLine(LogLevel.Error, string.Format("Timer Tick 예외 발생{0}", ex.Message));
            }
        }

        /// <summary>
        /// 우선순위에 따라 UI Enable 지정
        /// </summary>
        private void MainUIEnableProc()
        {
            // LPM 런 옵션, 레시피 버튼들.
            if (LoginMgr.Instance.IsLogined())
            {
                ucrlLPMOption.Enabled = GG.Equip.EquipRunMode == EmEquipRunMode.Manual;
                lblLPM1Recipe.Enabled = GG.Equip.Efem.LoadPort1.LoadType == EmLoadType.Manual;
                lblLPM2Recipe.Enabled = GG.Equip.Efem.LoadPort2.LoadType == EmLoadType.Manual;

                // Host의 ECID변경 요청에 따라 모터 설정 저장 중 Autorun 금지하기 위함
                btnAutoStart_Copy.Enabled = GG.Equip.IsMotorSettingSaving == false;
            }
            else
                ucrlLPMOption.Enabled = lblLPM1Recipe.Enabled = lblLPM2Recipe.Enabled = false;

            /// 로그인 여부에 따라 전체 UI 처리
            if (LoginMgr.Instance.LoginedUser.Id != _lastLoginedID)
            {
                ChangeUIFollowingLogin();
                _lastLoginedID = LoginMgr.Instance.LoginedUser.Id;
                if (LoginMgr.Instance.LoginedUser.Id == "0")
                    lblLoginID.Text = "LOGIN";
                else
                    lblLoginID.Text = string.Format("ID : {0}{1}[{2}]", LoginMgr.Instance.LoginedUser.Id, Environment.NewLine, LoginMgr.Instance.LoginedUser.Level.ToString());
            }
        }

        private void ChangeUIFollowingLogin()
        {
            if (LoginMgr.Instance.IsLogined())
            {
                SetEnabled(panel17, true);
                tabOperator.GetControl(0).Enabled = true;
                tabOperator.GetControl(1).Enabled = true;

                btnShift.Enabled = true;
                tabCtrl_Operator.Enabled = true;
                btnParameter.Enabled = true;
                btnSetupOption.Enabled = true;
                btnMonitor.Enabled = true;

                _tt.Hide(lblLoginID);
            }
            else
            {
                SetEnabled(panel17, false);
                tabOperator.GetControl(0).Enabled = false;
                tabOperator.GetControl(1).Enabled = false;

                btnShift.Enabled = false;

                btnMonitor.Enabled = false;
                btnParameter.Enabled = false;
                btnSetupOption.Enabled = false;

                tabCtrl_Operator.Enabled = false;

                //열린 창도 닫기
                string[] passFormName = new string[] { "FrmMain", "FrmOpMain", "FrmUserSelectBCRData", "FrmUserSelectReadData", "FrmCstDataUserInput", "FrmRetryUserSelect" };
                List<Form> formsToClose = new List<Form>();
                foreach (Form openForm in Application.OpenForms)
                {
                    string passName = passFormName.FirstOrDefault(f => f == openForm.Name);
                    if (passName != null)
                        continue;
                    else
                        formsToClose.Add(openForm);
                }
                foreach (Form openForm in formsToClose)
                    openForm.Close();

                _tt.IsBalloon = true;
                _tt.SetToolTip(lblLoginID, GG.boChinaLanguage ? "需登录才可操作." : "로그인을 해야 조작할 수 있습니다.");
                _tt.Show(GG.boChinaLanguage ? "需登录才可操作." : "로그인을 해야 조작할 수 있습니다.", lblLoginID, 90, 20);
            }
        }

        private void ScreenLock()
        {
            if (GG.Equip.IsKeyChangeToTeach == true && GG.Equip.ModeSelectKey.IsAuto == false && _isTeachFirst == true)
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
                new FormPWNeedClose(GG.Equip).Visible = true;
                _isTeachFirst = false;
            }
            if (GG.Equip.ModeSelectKey.IsAuto == true)
                _isTeachFirst = true;
        }

        private void OpcallProc()
        {
            if (GG.Equip.HsmsPc.OperCallMsg.Count > 0)
            {
                string msg = GG.Equip.HsmsPc.OperCallMsg.Dequeue();

                _frmOperatorCall.AddOperatorCall(0, new string[] { msg }, "");
                _frmOperatorCall.StartPosition = FormStartPosition.CenterScreen;
                _frmOperatorCall.Show();
            }
        }

        private void SetWaferInfo(PropertyGrid p, WaferInfoKey wafer)
        {
            p.SelectedObject = wafer == null || wafer.SlotNo <= 0 ? null : wafer;
        }

        private void UpdateMotorStateAt(DataGridView dgv)
        {
            dgv.SuspendLayout();
            foreach (ServoMotorPmac s in GG.Equip.Motors)
            {
                dgv.Rows[0].Cells[s.Name].Value = string.Format("{0:0.00#}", s.XF_CurrMotorPosition.vFloat);
                dgv.Rows[1].Cells[s.Name].Value = string.Format("{0:0.00#}", s.XF_CurrMotorSpeed.vFloat);
                dgv.Rows[2].Cells[s.Name].Value = string.Format("{0:0.00#}", s.XF_CurrMotorStress.vFloat);
            }
            dgv.ResumeLayout();
        }

        private void UpdateEquipImage(UcrlEquipView eqpView)
        {
            try
            {
                eqpView.ChangeChinaLanguage(GG.boChinaLanguage);
                eqpView.SetRobotPos((int)GG.Equip.Efem.LastRobotPort);

                eqpView.SetRobotVac(GG.Equip.Efem.Robot.Status.IsUpperArmVacOn, GG.Equip.Efem.Robot.Status.IsLowerArmVacOn);

                eqpView.ShowInterlockOffMsg = GG.Equip.IsUseDoorInterLockOff | GG.Equip.IsUseInterLockOff;
                eqpView.ShowReviewRunningMsg = GG.Equip.PMac.XB_ReviewRunning.vBit;
                eqpView.IsEFEMEMO = GG.EfemNoUse == false && GG.Equip.Efem.Status.IsEMO;
                eqpView.IsRobotArmDetected = GG.Equip.RobotArmDefect.IsOn;
                eqpView.IsLightCurtainDetected = GG.Equip.Efem.LPMLightCurtain.Detect.IsOn;
                eqpView.IsLightCurtainMute = GG.Equip.Efem.LPMLightCurtain.IsMuting;
                eqpView.UpdateLPMDoor(GG.Equip.Efem.LoadPort1.Status.IsDoorOpen, GG.Equip.Efem.LoadPort2.Status.IsDoorOpen);
                eqpView.IsLPM1OHTMode = GG.Equip.Efem.LoadPort1.LoadType == EmLoadType.OHT;
                eqpView.IsLPM2OHTMode = GG.Equip.Efem.LoadPort2.LoadType == EmLoadType.OHT;
                eqpView.SetLoadButtonLamp(0, (GG.Equip.Efem.LoadPort1.IsLdButtonWaitStep || GG.Equip.Efem.LoadPort1.IsUnldButtonWaitStep || GG.Equip.Efem.LoadPort1.IsRemoveFoupWaitStep) && DateTime.Now.Millisecond < 600);
                eqpView.SetLoadButtonLamp(1, (GG.Equip.Efem.LoadPort2.IsLdButtonWaitStep || GG.Equip.Efem.LoadPort2.IsUnldButtonWaitStep || GG.Equip.Efem.LoadPort2.IsRemoveFoupWaitStep) && DateTime.Now.Millisecond < 600);
                eqpView.IsRemoteManualMode = GG.Equip.CimMode == EmCimMode.Remote && GG.Equip.Efem.LoadPort2.LoadType == EmLoadType.Manual && GG.IsChungJu;

                eqpView.WaferExist = GG.Equip.IsWaferDetect == EmGlassDetect.NOT ? false : true;

                eqpView.UpdateWaferPos();
                eqpView.RobotArmCheck = GG.TestMode ? false : GG.Equip.IsRobotArmDetect;

                eqpView.WaferPinDetect1 = GG.Equip.WaferDetectSensorLiftpin1.IsOn;
                eqpView.WaferPinDetect2 = GG.Equip.WaferDetectSensorLiftpin2.IsOn;
                eqpView.WaferStageDetect1 = GG.Equip.WaferDetectSensorStage1.IsOn;

                eqpView.StageVacuum1 = GG.Equip.Vacuum.Stage1.IsOnOff;
                eqpView.StageVacuum2 = GG.Equip.Vacuum.Stage2.IsOnOff;

                eqpView.StageX = (double)GG.Equip.StageX.XF_CurrMotorPosition.vFloat;
                eqpView.StageY = (double)GG.Equip.StageY.XF_CurrMotorPosition.vFloat;
                eqpView.Theta = (double)GG.Equip.Theta.XF_CurrMotorPosition.vFloat;

                Func<CustomCylinder, int, double> getCylinderPos = delegate (CustomCylinder cld, int no)
                {
                    if (cld.XB_ForwardComplete[no])
                        return 50f;
                    else if (cld.XB_BackwardComplete[no])
                        return 0f;
                    else
                        return 25f;
                };
                eqpView.LiftPin1 = getCylinderPos(GG.Equip.LiftPin, 0);
                eqpView.LiftPin2 = getCylinderPos(GG.Equip.LiftPin, 1);

                eqpView.RightCentering1 = getCylinderPos(GG.Equip.StandCentering, 0);
                eqpView.LeftCentering1 = getCylinderPos(GG.Equip.StandCentering, 1);

                //EMS
                eqpView.EMS1 = GG.Equip.EmsOutside1.IsOn;
                eqpView.EMS2 = GG.Equip.EmsOutside2.IsOn;
                eqpView.EMS3 = GG.Equip.EmsOutside3.IsOn;

                //Door
                eqpView.TDOOR1 = GG.Equip.TopDoor01.IsOnOff;
                eqpView.TDOOR2 = GG.Equip.TopDoor02.IsOnOff;
                eqpView.TDOOR3 = GG.Equip.TopDoor03.IsOnOff;
                eqpView.TDOOR4 = GG.Equip.TopDoor04.IsOnOff;

                //Theta
                eqpView.Theta = GG.Equip.Theta.XF_CurrMotorPosition.vFloat;

                //EFEM
                eqpView.UpdateLPM(GG.Equip.Efem.LoadPort1.GetData(), GG.Equip.Efem.LoadPort2.GetData(), GG.Equip.Efem.LoadPort1.Status.IsFoupExist, GG.Equip.Efem.LoadPort2.Status.IsFoupExist);

                eqpView.statLpm1Alarm.Background = GG.Equip.Efem.LoadPort1.Status.IsAlarmLampOn ? System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.White;
                eqpView.statLpm1AutoMode.Background = GG.Equip.Efem.LoadPort1.Status.IsAuto ? System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.White;
                eqpView.statLpm1Ready.Background = (GG.Equip.Efem.LoadPort1.PioRecv.YRecvAble == true || GG.Equip.Efem.LoadPort1.PioSend.YSendAble == true) ? System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.White;
                eqpView.statLpm1Busy.Background = !GG.Equip.Efem.LoadPort1.Status.IsBusy ? System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.White;

                eqpView.statLpm2Alarm.Background = GG.Equip.Efem.LoadPort2.Status.IsError ? System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.White;
                eqpView.statLpm2AutoMode.Background = GG.Equip.Efem.LoadPort2.Status.IsAuto ? System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.White;
                eqpView.statLpm2Ready.Background = (GG.Equip.Efem.LoadPort2.PioRecv.YRecvAble == true || GG.Equip.Efem.LoadPort2.PioSend.YSendAble == true) ? System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.White;
                eqpView.statLpm2Busy.Background = !GG.Equip.Efem.LoadPort2.Status.IsBusy ? System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.White;

                eqpView.statAlignAlarm.Background = GG.Equip.Efem.Aligner.Status.IsStatus == EmEfemAlignerStatus.ERROR ? System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.White;
                eqpView.statAlignReady.Background = GG.Equip.Efem.Aligner.Status.IsStatus == EmEfemAlignerStatus.READY ? System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.White;
                eqpView.statAlignIdle.Background = GG.Equip.Efem.Aligner.Status.IsStatus == EmEfemAlignerStatus.RUNNING ? System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.White;
                eqpView.IsWaferExistInAligner = GG.Equip.Efem.Aligner.Status.IsWaferExist == true;

                eqpView.statRobotAlarm.Background = GG.Equip.Efem.Robot.Status.IsAlarm == true ? System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.White;
                eqpView.statRobotServoOff.Background = GG.Equip.Efem.Robot.Status.IsServoOn == false ? System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.White;
                eqpView.statRobotIdle.Background = GG.Equip.Efem.Robot.Status.IsMoving == false ? System.Windows.Media.Brushes.Red : System.Windows.Media.Brushes.White;

                eqpView.IsXLdPos = GG.Equip.StageX.IsMoveOnPosition(StageXServo.LoadingPos);
                eqpView.IsYLdPos = GG.Equip.StageY.IsMoveOnPosition(StageYServo.LoadingPos);
                eqpView.IsTLdPos = GG.Equip.Theta.IsMoveOnPosition(ThetaServo.LoadingPos);
                eqpView.IsPIOReady = GG.Equip.IsReadyToInputArm.IsSolOnOff;
                if (GG.IsDitPreAligner) eqpView.IsAlignerPIOReady = GG.Equip.IsReadyToAlignInputArm.IsSolOnOff;
                eqpView.IsRobotInput = GG.Equip.IsEFEMInputArm.IsOn;
                eqpView.IsLPM1Exist = GG.Equip.Efem.LoadPort1.Status.IsFoupExist;
                eqpView.IsLPM2Exist = GG.Equip.Efem.LoadPort2.Status.IsFoupExist;
                eqpView.LPM1Notify = DateTime.Now.Millisecond < 300 ? "" : CreateLPMNotifyMsg(GG.Equip.Efem.LoadPort1);
                eqpView.LPM2Notify = DateTime.Now.Millisecond < 300 ? "" : CreateLPMNotifyMsg(GG.Equip.Efem.LoadPort2);
                eqpView.IsTargetLPM1 = (GG.Equip.Efem.Robot.TargetLoadPort == 1 && DateTime.Now.Second % 2 == 0) ? 1 : (GG.Equip.Efem.LoadPort1.IsOpenReadyStep ? 2 : 0);
                eqpView.IsTargetLPM2 = (GG.Equip.Efem.Robot.TargetLoadPort == 2 && DateTime.Now.Second % 2 == 0) ? 1 : (GG.Equip.Efem.LoadPort2.IsOpenReadyStep ? 2 : 0);
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

        private string CreateLPMNotifyMsg(EFEMLPMUnit lpm)
        {
            if (lpm.IsRemoveFoupWaitStep) return GG.boChinaLanguage ? "Foup 除去后 请按 Load Button" : "Foup 제거 후 로드 버튼을 누르세요";
            if (lpm.IsUnldButtonWaitStep) return GG.boChinaLanguage ? "按Load Button解除 Light Curtain " : "로드 버튼 눌러서 라이트커튼 해제";
            if (lpm.IsLoadConfirmComplete) return GG.boChinaLanguage ? "Load Confirm 完毕, 请按投入开始 Button" : "로드 컨펌 완료, 투입 시작 버튼을 누르세요";
            if (lpm.IsLdButtonWaitStep) return GG.boChinaLanguage ? "按Load Button解除 Light Curtain " : "로드 버튼 눌러서 라이트커튼 해제";
            if (lpm.IsLdFoupWaitStep) return GG.boChinaLanguage ? "Foup要 Load后，请按 Load Button" : "Foup을 로드하고 로드 버튼을 누르세요";
            if (lpm.IsWaitRobotStep) return GG.boChinaLanguage ? "进行前排出时, 请按Load Button" : "진행 전 배출 시 로드 버튼을 누르세요";
            return "";
            // "웨이퍼가 안나간 경우 배출 가능 (로 ... < 길이 리밋
        }

        private void btnCamCoolingOn_DelayClick(object sender, EventArgs e)
        {
            //GG.Equip.CamColling.B_InOnOff.vBit = true;
        }
        private void btnCamCoolingOff_DelayClick(object sender, EventArgs e)
        {
            //GG.Equip.CamColling.B_InOnOff.vBit = false;
        }
        private void btnPm_DelayClick(object sender, EventArgs e)
        {
            GG.Equip.IsPM = !GG.Equip.IsPM;
        }

        private void tmrState_Tick(object sender, EventArgs e)
        {
            try
            {
                tbStatus.Text = GetCurrentStatus();
                lblPGVersion.Text = string.Format("{0}", GG.Exe.LastWriteTime.ToString("yyyyMMdd.HHmm"));
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
        private string GetCurrentStatus()
        {
            Process p = Process.GetCurrentProcess();
            string s = string.Format("스레드 {0}, 핸들 {1}, 메모리 사용 {2}kb, 피크 메모리 사용 {3}kb",
                p.Threads.Count, p.HandleCount, p.WorkingSet64 / 1024, p.PeakWorkingSet64 / 1024);
            p.Close();
            p.Dispose();
            return s;
        }
        private void btnCtrlSubClose_Click(object sender, EventArgs e)
        {

        }
        private void btnShowMain_Click(object sender, EventArgs e)
        {

        }

        private void btnlShowManual_Click(object sender, EventArgs e)
        {
            //pnlCtrlSub.Visible = false;

            //pnlAuto.Visible = true;
            //pnlManualCtrl.Visible = true;
            tabCtrl_Operator.Enabled = true;
        }
        private void btnlShowAuto_Click(object sender, EventArgs e)
        {
            ChangeView(ref pnlEquipDraw);

            //하단
            //pnlAuto.Visible = true;
            tabCtrl_Operator.Enabled = true;
            //pnlManualCtrl.Visible = false;
        }
        private void btnlShowManual_Click_1(object sender, EventArgs e)
        {
            ChangeView(ref pnlEquipDraw);

            //하단
            //pnlAuto.Visible = false;
            //pnlManualCtrl.Visible = true;
        }
        private void btnlShowLog_Click(object sender, EventArgs e)
        {
            //ChangeView(ref pnlLog);            
        }


        private void btnExpanding_Click(object sender, EventArgs e)
        {
            Size org = this.Size;
            Button btn = sender as Button;

            if (_isExpending == false)
            {
                btn.Text = GG.boChinaLanguage ? "折叠" : "접기";
                org.Width = 1920;
                _isExpending = true;
                foreach (Form openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FrmOperator")
                    {
                        if (openForm.WindowState == FormWindowState.Minimized)
                        {
                            openForm.WindowState = FormWindowState.Normal;
                        }
                        openForm.Close();
                        this.Size = org;
                        return;
                    }
                }
            }
            else
            {
                btn.Text = GG.boChinaLanguage ? "展开" : "펼치기";
                org.Width = 485;
                _isExpending = false;
            }
            this.Size = org;

        }
        private void ChangeView(ref Panel view)
        {
            //pnlCtrlSub.Visible = false;

            //상단
            //  pnlLog.Visible = false;
            //pnlEquipDraw.Visible = false;
            //pnlAlarm.Visible = false;
            //pnlParamSetting.Visible = false;
            //pnlMonitor.Visible = false;

            view.Visible = true;
        }

        private void mainMenu_Click(object sender, EventArgs e)
        {
            ButtonLabel btn = sender as ButtonLabel;
            if (btn == null) return;

            //pnlMainStep.Visible = (btn == btnMainStep);
            //pnlMainInterfaceView.Visible = (btn == btnMainInterfaceView);

            //하단
            //pnlAuto.Visible = false;
            tabCtrl_Operator.Enabled = true;
            //pnlManualCtrl.Visible = true;
        }
        private void lblLoadingStep_Click(object sender, EventArgs e)
        {
            GG.Equip.CmdLoading();
        }
        private void lblScanningStep_Click(object sender, EventArgs e)
        {
            GG.Equip.CmdScanStep();
        }
        private void lblUnloadingStep_Click(object sender, EventArgs e)
        {
            GG.Equip.CmdUnLoadingStep();
        }
        private void lblPioUpperStep_Click(object sender, EventArgs e)
        {
            //GG.Equip.CmdPioInd2AoiStep();
        }
        private void lblPioLowerStep_Click(object sender, EventArgs e)
        {
            //GG.Equip.CmdPioAoi2IndStep();
        }

        private void lblPioExchangeStep_Click(object sender, EventArgs e)
        {
            //GG.Equip.CmdPioAoi2IndExStep();
        }

        #region Equip Run Control
        private void btnAutoStart_Copy_Click(object sender, EventArgs e)
        {
            if ((GG.Equip.CimMode == EmCimMode.Remote && GG.Equip.Efem.LoadPort2.LoadType == EmLoadType.Manual) == false)
                return;

            if (GG.Equip.Efem.LoadPort1.IsLoadConfirmComplete)
                GG.Equip.Efem.LoadPort1.Status.IsLoadButtonPushed = true;
            if (GG.Equip.Efem.LoadPort2.IsLoadConfirmComplete)
                GG.Equip.Efem.LoadPort2.Status.IsLoadButtonPushed = true;
            Logger.Log.AppendLine(LogLevel.Info, "StartIn Button Click 1:{0},2:{1}",
                GG.Equip.Efem.LoadPort1.Status.IsLoadButtonPushed, GG.Equip.Efem.LoadPort2.Status.IsLoadButtonPushed);
        }
        //메소드 - 설비 상태 변경 
        private void btndAutoStart_DelayClick(object sender, EventArgs e)
        {
            if (GG.EfemNoUse == false)
            {
                bool dataNotMatch = false;
                if ((GG.Equip.Efem.Robot.LowerWaferKey.CstID != "") != GG.Equip.Efem.Robot.Status.IsLowerArmVacOn) dataNotMatch = true;
                else if ((GG.Equip.Efem.Robot.UpperWaferKey.CstID != "") != GG.Equip.Efem.Robot.Status.IsUpperArmVacOn) dataNotMatch = true;
                else if ((GG.Equip.Efem.Aligner.LowerWaferKey.CstID != "") != GG.Equip.Efem.Aligner.Status.IsWaferExist) dataNotMatch = true;
                else if ((GG.Equip.TransferUnit.LowerWaferKey.CstID != "") != (GG.Equip.IsWaferDetect != EmGlassDetect.NOT)) dataNotMatch = true;
                if (dataNotMatch)
                {
                    GG.Equip.IsInterlock = true;
                    string msg = "웨이퍼정보와 실제 웨이퍼유무상태가 다릅니다 확인 후 시작가능합니다";
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Wafer Info和实际 Wafer 有無不符.确认后可开始." : msg, GG.boChinaLanguage ? "Main 画面细部事项 - 在有下端 Shift 窗里可修改情报或除去Wafer " : "메인화면세부사항 - 우측 하단 Shift 창에서 정보 수정 또는 웨이퍼 제거");
                    return;
                }
            }

            if (RecipeDataMgr.GetCurRecipeName(0) == string.Empty)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Recipe 管理画面里设置 Recipe后可开始 ." : "레시피관리에서 레시피 설정 후 시작가능합니다");
                return;
            }

            // PreAligner Theta 위치값 고정이라 1개만 사용 (SKH AVI #1,2,3호기 동일)
            if (PreAlignerRecipeDataMgr.GetRecipes() != null)
            {
                foreach (var AlignRecipe in PreAlignerRecipeDataMgr.GetRecipes())
                {
                    var q = PreAlignerRecipeDataMgr.GetRecipe(AlignRecipe.Name);
                    GG.Equip.FixedDitAlignerRecipeName = AlignRecipe.Name;
                    GG.Equip.PreAligner.SetCurRecipe(q);
                }
            }

            if (GG.Equip.PreAligner.CurRecipe == null)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "PreAligner Recipe 未设置." : "PreAligner 레시피가 설정되어 있지 않습니다", GG.boChinaLanguage ? "Recipe 设置好后就可以开始了." : "Recipe 설정 후 시작 가능합니다.");
                return;
            }

            GG.Equip.ChangeRunMode(EmEquipRunMode.Auto);
        }
        private void btnManualMode_DelayClick(object sender, EventArgs e)
        {
            Logger.Log.AppendLine(LogLevel.Info, "{0} 버튼 클릭", "Manual");
            if (GG.Equip.ChangeRunMode(EmEquipRunMode.Manual) == true)
            {
                GG.Equip.Efem.ChangeMode(EmEfemRunMode.Stop);
            }

            if (GG.EfemLongRun)
            {
                GG.EfemLongRun = false;
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Manual 转换时,会解除 EFEM Long Run Mode." : "매뉴얼 전환 시 EFEM Long Run모드는 해제됩니다.");
            }
        }

        private void btnPause_DelayClick(object sender, EventArgs e)
        {
            if (GG.Equip.EquipRunMode == EmEquipRunMode.Auto)
            {
                if (GG.Equip.IsPause == true)
                {
                    if (GG.Equip.IsNeedRestart == true)
                    {
                        DialogResult isOk = UserMessageBox.ShowMessageOkCanleBox(GG.boChinaLanguage ? "非正常停止需再开始" : "비정상 정지로 재시작이 필요합니다. ", "WARING");
                        if (isOk == DialogResult.OK)
                        {
                            GG.Equip.ChangeRunMode(EmEquipRunMode.Manual);
                            GG.Equip.Efem.ChangeMode(EmEfemRunMode.Stop);

                            GG.Equip.IsPause = false;
                            GG.Equip.IsNeedRestart = false;
                        }
                    }
                    else
                    {
                        if (GG.Equip.Efem.ChangeMode(EmEfemRunMode.Start) == true)
                        {
                            GG.Equip.IsPause = false;
                        }
                    }
                }
                else
                {
                    //if (GG.Equip.PioA2ISend.StepPioSend == PIO_S.S020_LO_RECV_ABLE_WAIT ||
                    //    GG.Equip.PioI2ARecv.StepPioRecv == PIO_R.R020_UP_SEND_ABLE_WAIT)
                    //{
                    //    GG.Equip.IsPause = true;
                    //    GG.Equip.CmdPioReset();
                    //    GG.Equip.Efem.ChangeMode(EmEfemRunMode.Pause);
                    //    CheckMgr.AddCheckMsg(true, "PIO Able 대기 중이어서 Able Reset/Manual 전환 됩니다");
                    //}
                    //else
                    {
                        GG.Equip.IsPause = true;
                        if (GG.EfemNoUse == false)
                            GG.Equip.Efem.ChangeMode(EmEfemRunMode.Pause);
                    }
                }
            }
            else
            {
                GG.Equip.IsPause = !GG.Equip.IsPause;
                if (GG.Equip.IsPause == true)
                    GG.Equip.Efem.ChangeMode(EmEfemRunMode.Pause);
            }
        }
        private void btnEFEMStart_DelayClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (GG.EfemNoUse == true)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "EFEM未使用 Mode " : "EFEM 미사용 모드입니다");
                return;
            }

            if (btn == btnEFEMStart)
            {
                if (GG.Equip.Efem.IsHomeComplete == false)
                {
                    GG.Equip.Efem.ChangeMode(EmEfemRunMode.Home);
                    GG.Equip.Efem.IsReserveStart = true;
                }
                else
                    GG.Equip.Efem.ChangeMode(EmEfemRunMode.Start);
            }
            else if (btn == btnEFEMPause)
            {
                GG.Equip.Efem.ChangeMode(EmEfemRunMode.Pause);
            }
            else if (btn == btnEFEMManual)
            {
                GG.Equip.Efem.ChangeMode(EmEfemRunMode.Stop);
            }
            else if (btn == btnEFEMCycleStop)
            {
                GG.Equip.Efem.ChangeMode(EmEfemRunMode.CycleStop);
            }
        }
        #endregion Equip Run Control        
        //메소드 - 검시 관련 재검예약, 재검..
        private void btnReservReInsp_Click(object sender, EventArgs e)
        {
            if (GG.Equip.IsReserveReInsp == EmReserveReInsp.DISABLE)
            {
                GG.Equip.IsReserveReInsp = EmReserveReInsp.RESERVE;
            }
            else
            {
                GG.Equip.IsReserveReInsp = EmReserveReInsp.DISABLE;
            }
        }
        private void btnReservInsp_Stop_Click(object sender, EventArgs e)
        {
            if (GG.Equip.IsReserveReInsp == EmReserveReInsp.RESERVE)
            {
                if (GG.Equip.IsReserveInsp_STOP == EmReserveInsp_Stop.STOP)
                {
                    GG.Equip.IsReserveInsp_STOP = EmReserveInsp_Stop.DISABLE;
                }
                else
                {
                    GG.Equip.IsReserveInsp_STOP = EmReserveInsp_Stop.RESERVE;
                }
            }
            else if (GG.Equip.IsReserveInsp_STOP == EmReserveInsp_Stop.RESERVE)
            {
                GG.Equip.IsReserveInsp_STOP = EmReserveInsp_Stop.DISABLE;
            }
            else
            {
                GG.Equip.IsReserveInsp_STOP = EmReserveInsp_Stop.RESERVE;
            }
        }
        private void btnReservReInspStart_Click(object sender, EventArgs e)
        {
            if (GG.Equip.IsReserveReInsp == EmReserveReInsp.READY)
                GG.Equip.IsReserveReInsp = EmReserveReInsp.START;
        }

        private void btnAlarmReset_Click(object sender, EventArgs e)
        {
            GG.Equip.IsBuzzerStopSW = true;
            GG.Equip.ResetError();
        }

        private void btndTact_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FrmTactTime")
                {
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {
                        openForm.WindowState = FormWindowState.Normal;
                    }
                    openForm.Activate();
                    return;
                }
            }

            FrmTactTime ff = new FrmTactTime(GG.Equip);
            ff.Show();
        }
        private void btnLongTest_Click(object sender, EventArgs e)
        {
            if (GG.Equip.IsUseInternalTestMode)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "内部 Test Mode ON时, 无法变更Long Run Test" : "내부 테스트모드 ON시 LongRun Test 변경 불가능 합니다");
                return;
            }

            CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "LongRun 设置变更了." : "LongRun 설정이 변경되었습니다.");
            GG.Equip.IsLongTest = !GG.Equip.IsLongTest;
            GG.Equip.LongRunCount = 0;
        }

        private void btnDoorOpenClose_Click_1(object sender, EventArgs e)
        {
            GG.Equip.DoorOpenOrClose();
        }

        //임시
        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormPasswdBox login = new FormPasswdBox();
            login.ShowDialog();

            if (login.DialogResult == DialogResult.OK)
            {
                //if (LoginMgr.Instance.IsCorrectInfo(login.ID, login.Passwd))
                {
                    CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "登录成功." : "로그인을 성공하였습니다.");
                    login.Close();
                    return;
                }
                CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "登录失败" : "로그인을 실패하였습니다.");
            }
            else if (login.DialogResult == DialogResult.Cancel)
                return;
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FrmOperating")
                {
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {
                        openForm.WindowState = FormWindowState.Normal;
                    }
                    openForm.Activate();
                    return;
                }
            }

            FrmOperating fOperator = new FrmOperating(GG.Equip);
            fOperator.Show();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btnGlassInfo_Click(object sender, EventArgs e)
        {

        }
        private void pnlUMaxXY_Paint(object sender, PaintEventArgs e)
        {

        }
        private void lblMaxScanTime_Click(object sender, EventArgs e)
        {
            _logicWorker.MaxScanTime = 0;
        }
        private void btnPmacRestart_DoubleClick(object sender, EventArgs e)
        {
            Thread t = new Thread(() => GG.PMAC.Open());
            t.Start();
        }
        private void btnBuzzerStop_Click(object sender, EventArgs e)
        {
            if (GG.TestMode == true)
            {
                return;
            }
            GG.Equip.Efem.UserSignal.Buzzer1 = EmEfemLampBuzzerState.OFF;
            GG.Equip.IsBuzzerStopSW = true;
        }

        private void lstvAlarm_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_DelayClick(object sender, EventArgs e)
        {
            _logicWorker.Stop();
            if (GG.Equip.PreAligner != null)
                GG.Equip.PreAligner.Release();
            Application.Exit();
            Environment.Exit(0);
        }

        private void btnlShowWorkLight_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FrmSetting")
                {
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {
                        openForm.WindowState = FormWindowState.Normal;
                    }
                    openForm.Dispose();
                    break;
                }
            }


            FrmSetting ff = new FrmSetting(GG.Equip);
            ff.SetStartPage("WorkingLight");
            ff.Show();
        }
        private void buttonParam_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FrmSetting")
                {
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {
                        openForm.WindowState = FormWindowState.Normal;
                    }
                    openForm.Dispose();
                    break;
                }
            }



            FrmSetting ff = new FrmSetting(GG.Equip);
            ff.Show();
        }
        private void btnOperationOption_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FrmOperOption")
                {
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {
                        openForm.WindowState = FormWindowState.Normal;
                    }
                    openForm.Dispose();
                    break;
                }
            }

            FrmOperOption ff = new FrmOperOption(GG.Equip);
            ff.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FrmOperating frm = new FrmOperating(GG.Equip);
            frm.ShowDialog();
        }

        private void btndGlassUnloading_Click(object sender, EventArgs e)
        {
            if (GG.Equip.IsGlassUnloading == false)
            {
                if (GG.Equip.IsNoGlassMode == true ||
                    GG.Equip.IsWaferDetect == EmGlassDetect.NOT)
                {
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<GLASS DETECT ERROR>\n(不是正常的 Glass 状态时, 无法选择Glass排出 .)" : "인터락<GLASS DETECT ERROR>\n정상적인 글라스 상태가 아닐 경우 글라스 배출을 선택 할 수 없습니다.");
                    Logger.Log.AppendLine(LogLevel.Warning, "글라스 감지 에러! 글라스 배출 선택 불가!");
                    return;
                }
                if (GG.Equip.IsHomePosition == false)
                {
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<HOME POSITION>\n(只有Home Position 状态的情况下，可以选择Glass排出 .)" : "인터락<HOME POSITION>\n홈 포지션 상태일 경우만 글라스 배출 선택 가능합니다.");
                    Logger.Log.AppendLine(LogLevel.Warning, "홈 포지션 상태 아님! 글라스 배출 선택 불가!");
                    return;
                }

                GG.Equip.IsGlassUnloading = true;
                if (GG.Equip.IsCycleStop != EmCycleStop.Request)
                {
                    GG.Equip.IsCycleStop = EmCycleStop.Request;
                }
            }
            else
                GG.Equip.IsGlassUnloading = false;
        }

        private void btndInnerWork_Click(object sender, EventArgs e)
        {
            bool isServoMoving = GG.Equip.Motors.FirstOrDefault(m => m.IsMoving == true) != null;

            if (isServoMoving == true && GG.Equip.IsUseInterLockOff == false)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<SERVO MOVING>\n(SERVO 动作中. 无法执行内部作业)" : "인터락<SERVO MOVING>\n(SERVO 동작중입니다. 내부작업을 수행 할 수 없습니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "모터 동작중, 내부작업 불가!");
                return;
            }

            if (GG.Equip.EquipRunMode == EmEquipRunMode.Auto || GG.Equip.IsHomePositioning)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<进行中>\n( Auto 或者 Home 动作中. 无法执行内部作业.)" : "인터락<진행중>\n(오토 또는 홈 동작 중입니다. 내부작업을 수행 할 수 없습니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "홈 또는 오토 동작중, 내부작업 불가!");
                return;
            }
            /*
            if (GG.Equip.IsPM == false && GG.Equip.IsUseInterLockOff == false)
            {
                InterLockMgr.AddInterLock("인터락<PM MODE>\n(PM 상태일 경우만 내부작업을 수행 할 수 있습니다.)");
                Logger.Log.AppendLine(LogLevel.Warning, "PM모드 아님, 내부작업 불가!");
                return;
            }*/

            GG.Equip.IsInterlock = true;

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
            new FormPWNeedClose(GG.Equip).Visible = true;
        }

        private void btnlShowEtc_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FrmSetting")
                {
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {
                        openForm.WindowState = FormWindowState.Normal;
                    }
                    openForm.Dispose();
                    break;
                }
            }



            FrmSetting ff = new FrmSetting(GG.Equip);
            ff.SetStartPage("ETC");
            ff.Show();
        }

        private void btnExtendLogView_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FrmExtendLogView")
                {
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {
                        openForm.WindowState = FormWindowState.Normal;
                    }
                    openForm.Dispose();
                    break;
                }
            }

            FrmExtendLogView exLogView = new FrmExtendLogView();
            exLogView.Show();
        }

        private void btnPioReset_Click(object sender, EventArgs e)
        {
            if (GG.Equip.EquipRunMode == EmEquipRunMode.Auto && GG.Equip.IsPause == false)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<AUTO MODE>\n(Auto Run 动作中，需要 PAUSE才可以.)" : "인터락<AUTO MODE>\n(오토런 중에는 PAUSE 후 가능합니다.)");
                return;
            }

            GG.Equip.CmdPioReset();
        }

        private void tbStatus_DoubleClick(object sender, EventArgs e)
        {
            string[] path = Directory.GetFiles(Application.StartupPath, "releaseInfo*.txt");
            if (path.Length > 0)
                System.Diagnostics.Process.Start("Notepad.exe", path[0]);
            else
            {
                StreamWriter sr = File.CreateText("releaseInfo.txt");
                sr.WriteLine("자동생성된 버전파일 (버전이 정확하지 않을 수 있음)");
                sr.WriteLine(GG.Equip.InitSetting.SwVersion);
                sr.Flush();
                sr.Close();
                MessageBox.Show(GG.boChinaLanguage ? "请再次确认" : "다시 확인바랍니다");
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void btnImmStop_Click(object sender, EventArgs e)
        {
            GG.Equip.PMac.StartCommand(GG.Equip, EmPMacmd.IMMEDIATE_STOP, 0);
            GG.Equip.Motors.ToList().ForEach(m => m.Reset());

            GG.Equip.Efem.ImmediateStop(GG.Equip);
        }

        private void btnReviewManual_Click(object sender, EventArgs e)
        {
            if (GG.Equip.IsReviewManual == EmReviewManual.None)
            {
                if (GG.Equip.EquipRunMode == EmEquipRunMode.Auto)
                {
                    GG.Equip.IsReviewManual = EmReviewManual.Request;
                }
                else
                {
                    GG.Equip.IsReviewManual = EmReviewManual.InterLock;
                }
            }
            else
            {
                GG.Equip.IsReviewManual = EmReviewManual.None;
            }
        }

        private void btnReviewSkip_Click(object sender, EventArgs e)
        {
            if (GG.Equip.IsReviewSkip == EmReviewSkip.None)
                GG.Equip.IsReviewSkip = EmReviewSkip.Request;
            else
                GG.Equip.IsReviewSkip = EmReviewSkip.None;
        }

        private void btnUseInspMotorControl_Click(object sender, EventArgs e)
        {
            if (GG.Equip.EquipRunMode == EmEquipRunMode.Auto)
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Auto 状态下，检查  Server里是无法控制的." : "Auto 상태에선 검사 서버에서 제어할 수 없습니다");
            else
                GG.Equip.InspPc.SetInspectServerMotorInterlockOff(!IsptAddrB.YB_MotorInterlockOffState.vBit);
        }

        private void btnTransferData_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btnLPM1CassetteTransferData || btn == btnLPM1TransferData_Copy)
            {
                tabCtrlTransferData.SelectedIndex = 0;
                CassetteInfo cstinfo = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort1.CstKey);
                if (cstinfo != null)
                    TypeDescriptor.AddAttributes(cstinfo, new Attribute[] { new ReadOnlyAttribute(true) });
                pGridCstTransferInfo.SelectedObject = cstinfo;
            }
            else if (btn == btnLPM2CassetteTransferData || btn == btnLPM2TransferData_Copy)
            {
                tabCtrlTransferData.SelectedIndex = 0;
                CassetteInfo cstinfo = TransferDataMgr.GetCst(GG.Equip.Efem.LoadPort2.CstKey);
                if (cstinfo != null)
                    TypeDescriptor.AddAttributes(cstinfo, new Attribute[] { new ReadOnlyAttribute(true) });
                pGridCstTransferInfo.SelectedObject = cstinfo;
            }
            else if (btn == btnUpperRobotTransferData || btn == btnUpperTransferData_Copy)
            {
                tabCtrlTransferData.SelectedIndex = 1;
                WaferInfo waferinfo = TransferDataMgr.GetWafer(GG.Equip.Efem.Robot.UpperWaferKey);
                if (waferinfo != null)
                    TypeDescriptor.AddAttributes(waferinfo, new Attribute[] { new ReadOnlyAttribute(true) });
                pGridWaferTransferInfo.SelectedObject = waferinfo;
            }
            else if (btn == btnLowerRobotTransferData || btn == btnLowerTransferData_Copy)
            {
                tabCtrlTransferData.SelectedIndex = 1;
                WaferInfo waferinfo = TransferDataMgr.GetWafer(GG.Equip.Efem.Robot.LowerWaferKey);
                if (waferinfo != null)
                    TypeDescriptor.AddAttributes(waferinfo, new Attribute[] { new ReadOnlyAttribute(true) });
                pGridWaferTransferInfo.SelectedObject = waferinfo;
            }
            else if (btn == btnAlignerTransferData || btn == btnAlignerTransferData_Copy)
            {
                tabCtrlTransferData.SelectedIndex = 1;
                WaferInfo waferinfo = TransferDataMgr.GetWafer(GG.Equip.Efem.Aligner.LowerWaferKey);
                if (waferinfo != null)
                    TypeDescriptor.AddAttributes(waferinfo, new Attribute[] { new ReadOnlyAttribute(true) });
                pGridWaferTransferInfo.SelectedObject = waferinfo;
            }
            else if (btn == btnEquipmentTransferData || btn == btnEquipmentTransferData_Copy)
            {
                tabCtrlTransferData.SelectedIndex = 1;
                WaferInfo waferinfo = TransferDataMgr.GetWafer(GG.Equip.TransferUnit.LowerWaferKey);
                if (waferinfo != null)
                    TypeDescriptor.AddAttributes(waferinfo, new Attribute[] { new ReadOnlyAttribute(true) });
                pGridWaferTransferInfo.SelectedObject = waferinfo;
            }
            else if (btn == btnShift)
            {
                if (GG.Equip.EquipRunMode == EmEquipRunMode.Auto)
                {
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Auto状态下无法 Shift " : "Auto 상태에서 Shift 불가능합니다");
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
        }

        private void btnInnerLamp_Click(object sender, EventArgs e)
        {
            GG.Equip.WorkingLight.Toggle();
        }

        private void lstvAlarmClone_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                EventHandler eh = new EventHandler(AlarmMenuClick);

                MenuItem[] m = new MenuItem[]
                {
                    new MenuItem("Heavy", eh),
                    new MenuItem("Warn", eh),
                    new MenuItem("Unused", eh)
                };

                ContextMenu cm = new ContextMenu(m);
                cm.Show(lstvAlarmClone, new Point(e.X, e.Y));
            }
        }
        void AlarmMenuClick(object obj, EventArgs e)
        {
            MenuItem mi = (MenuItem)obj;
            AlarmMgr.Instance.AlarmStateChange(lstvAlarmClone.SelectedItems[0].SubItems[3].Text, mi.Text);
        }
        private void lstvAlarmHistory_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                EventHandler eh = new EventHandler(AlarmMenuClick2);

                MenuItem[] m = new MenuItem[]
                {
                    new MenuItem("Heavy", eh),
                    new MenuItem("Warn", eh),
                    new MenuItem("Unused", eh)
                };

                ContextMenu cm = new ContextMenu(m);
                cm.Show(lstvAlarmHistory, new Point(e.X, e.Y));
            }
        }
        void AlarmMenuClick2(object obj, EventArgs e)
        {
            MenuItem mi = (MenuItem)obj;
            AlarmMgr.Instance.AlarmStateChange(lstvAlarmHistory.SelectedItems[0].SubItems[3].Text, mi.Text);
        }
        private void lblEFEM_DoubleClick(object sender, EventArgs e)
        {
            GG.Equip.EFEMTcpReconnect();
        }

        private void lstEFEMAlarm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstEFEMAlarm.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = lstEFEMAlarm.SelectedItems;
                ListViewItem lvItem = items[0];
                string msg = lvItem.SubItems[2].Text;
                InterLockMgr.AddInterLock(msg);
            }
        }

        private void lstEFEMAlarm_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            if (lstEFEMAlarm.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = lstEFEMAlarm.SelectedItems;
                ListViewItem lvItem = items[0];
                string msg = lvItem.SubItems[2].Text;

                InterLockMgr.AddInterLock(msg);
            }
        }

        private void LOAD_Click(object sender, EventArgs e)
        {

        }

        #region TestOnly
        private void btnLowerVacOff_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Robot.Status.IsLowerArmVacOn = false;
        }
        private void btnAlignerWaferOff_Click(object sender, EventArgs e)
        {
            if (GG.IsDitPreAligner)
            {
                GG.Equip.AlignerWaferDetect.XB_OnOff.vBit = true;
            }
            else
            {
                GG.Equip.Efem.Aligner.Status.IsWaferExist = false;
            }
        }
        private void btnAlignerWaferOn_Click(object sender, EventArgs e)
        {
            if (GG.IsDitPreAligner)
            {
                GG.Equip.AlignerWaferDetect.XB_OnOff.vBit = false;
            }
            else
            {
                GG.Equip.Efem.Aligner.Status.IsWaferExist = true;
            }
        }
        private void btnLowerVacOn_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Robot.Status.IsLowerArmVacOn = true;
        }

        private void btnUpperVacOn_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Robot.Status.IsUpperArmVacOn = true;
        }

        private void btnUpperVacOff_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Robot.Status.IsUpperArmVacOn = false;
        }
        private void btnSettingOn_Click(object sender, EventArgs e)
        {
            GG.Equip.IsBuzzerAllOff = true;
            GG.EfemLongRun = true;
            GG.Equip.IsUseDoorInterLockOff = true;
        }
        private void btnAVIWaferOff_Click(object sender, EventArgs e)
        {
            GG.Equip.WaferDetectSensorLiftpin1.XB_OnOff.vBit = false;
            GG.Equip.WaferDetectSensorLiftpin2.XB_OnOff.vBit = false;
            GG.Equip.WaferDetectSensorStage1.XB_OnOff.vBit = false;
        }
        private void btnAVIWaferOn_Click(object sender, EventArgs e)
        {
            GG.Equip.WaferDetectSensorLiftpin1.XB_OnOff.vBit = true;
            GG.Equip.WaferDetectSensorLiftpin2.XB_OnOff.vBit = true;
            GG.Equip.WaferDetectSensorStage1.XB_OnOff.vBit = true;
        }
        private void btnHeavyAlarm_Click(object sender, EventArgs e)
        {
            //Button btn = sender as Button;

            //if(btn == btnHeavyAlarm)
            //{
            //    AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0001_EMERGENCY_1_ERROR);
            //}
            //else if (btn == btnWarnAlarm)
            //{
            //    AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0860_LPM1_USER_INPUT_MAP_ABRNOMAL);
            //}




        }
        #endregion




        private void btnUnPoup_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btnUnPoupLpm1)
            {
                GG.Equip.Efem.LoadPort1.Status.IsFoupExist = false;
            }
            else if (btn == btnUnPoupLpm2)
            {
                GG.Equip.Efem.LoadPort2.Status.IsFoupExist = false;
            }
            else if (btn == btnPoupLpm1)
            {
                GG.Equip.Efem.LoadPort1.Status.IsFoupExist = true;
            }
            else if (btn == btnPoupLpm2)
            {
                GG.Equip.Efem.LoadPort2.Status.IsFoupExist = true;
            }
        }

        private void btnLongRunOff_Click(object sender, EventArgs e)
        {
            GG.EfemLongRun = false;
        }
        private void lblLoginID_Click(object sender, EventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                LoginMgr.Instance.Login(GG.Equip, "ditctrl", "ditctrl");

                gbSetUpTest.Visible = true;
                return;
            }
            if (LoginMgr.Instance.IsLogined())
            {
                FormMessageOkCanleBox chk = new FormMessageOkCanleBox();
                chk.Message = GG.boChinaLanguage ? "是否要注销？" : "로그아웃 하시겠습니까??";

                chk.ShowDialog();

                if (chk.DialogResult == DialogResult.OK)
                {
                    LoginMgr.Instance.Logout(GG.Equip);
                    gbSetUpTest.Visible = false;
                }

            }
            else
            {
                FrmLogin frm = new FrmLogin(GG.Equip, FrmLogin.FrmLoginType.Login);
                frm.StartPosition = FormStartPosition.Manual;
                frm.Location = new Point(50, 400);
                frm.ShowDialog();
                ExtensionUI.LogoutTime = PcDateTime.Now;
            }
        }
        private void lblCurRecipe2_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FrmRecipeMgr")
                {
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {
                        openForm.WindowState = FormWindowState.Normal;
                    }
                    openForm.Dispose();
                    break;
                }
            }

            RecipeMgrForm = new FrmRecipeMgr();
            RecipeMgrForm.Show();
        }

        private void WaferInfoMenuClick(object sender, EventArgs e)
        {
            MenuItem mI = (MenuItem)sender;
            string str = mI.Text;
            WaferInfoKey wKey = mI.Tag as WaferInfoKey;
            if (wKey == null)
            {
                CassetteInfoKey cKey = mI.Tag as CassetteInfoKey;

                if (str == "삭제" && cKey != null)
                {
                    TransferDataMgr.DeleteBackUpKey(cKey);
                    TransferDataMgr.UpdateCstKey();
                    if (cKey.ID == GG.Equip.Efem.LoadPort1.CstKey.ID)
                        GG.Equip.Efem.LoadPort1.CstKey.Clear();
                    else if (cKey.ID == GG.Equip.Efem.LoadPort2.CstKey.ID)
                        GG.Equip.Efem.LoadPort2.CstKey.Clear();
                }
            }
            else
            {
                if (str == "삭제" && wKey != null)
                {
                    TransferDataMgr.DeleteBackUpKey(wKey);
                    TransferDataMgr.UpdateWaferKey();
                }
                if (str == "전체 삭제")
                {
                    TransferDataMgr.DeleteAllBackUpKey();
                    TransferDataMgr.UpdateWaferKey();
                }
            }
        }
        private void pnlWaferInfoDragDrop(object sender, DragEventArgs e)
        {
            WaferInfoKey wKey = (WaferInfoKey)e.Data.GetData(typeof(WaferInfoKey));
            if (wKey == null)
                return;
            string pnlName = (sender as Panel).Name;

            EmEfemDBPort targetPort = EmEfemDBPort.NONE;
            switch (pnlName)
            {
                case "pnlUpperWaferInfo":
                    targetPort = EmEfemDBPort.UPPERROBOT;
                    break;
                case "pnlLowerWaferInfo":
                    targetPort = EmEfemDBPort.LOWERROBOT;
                    break;
                case "pnlAlignWaferInfo":
                    targetPort = EmEfemDBPort.ALIGNER;
                    break;
                case "pnlAVIWaferInfo":
                    targetPort = EmEfemDBPort.EQUIPMENT;
                    break;
            }

            TransferDataMgr.Shift(wKey, targetPort);
            TransferDataMgr.UpdateWaferKey();

            Logger.TransferDataLog.AppendLine(LogLevel.Info, "WaferInfo Shift to " + targetPort.ToString());
        }
        private void pnlWaferInfoDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void pnlWaferInfo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                string pnlName = (sender as Panel).Name;

                switch (pnlName)
                {
                    case "pnlAlignWaferInfo":
                        DoDragDrop(GG.Equip.Efem.Aligner.LowerWaferKey, DragDropEffects.Move);
                        break;
                    case "pnlAVIWaferInfo":
                        DoDragDrop(GG.Equip.TransferUnit.LowerWaferKey, DragDropEffects.Move);
                        break;
                    case "pnlUpperWaferInfo":
                        DoDragDrop(GG.Equip.Efem.Robot.UpperWaferKey, DragDropEffects.Move);
                        break;
                    case "pnlLowerWaferInfo":
                        DoDragDrop(GG.Equip.Efem.Robot.LowerWaferKey, DragDropEffects.Move);
                        break;
                    default:
                        break;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                string pnlName = (sender as Panel).Name;
                PropertyGrid pGrid = null;

                switch (pnlName)
                {
                    case "pnlAlignWaferInfo":
                        pGrid = pGridWaferInfoAligner;
                        break;
                    case "pnlAVIWaferInfo":
                        pGrid = pGridWaferInfoAVI;
                        break;
                    case "pnlUpperWaferInfo":
                        pGrid = pGridWaferInfoUpper;
                        break;
                    case "pnlLowerWaferInfo":
                        pGrid = pGridWaferInfoLower;
                        break;
                    case "pnlLpm1CstInfo":
                        pGrid = pGridCstInfoLPM1;
                        break;
                    case "pnlLpm2CstInfo":
                        pGrid = pGridCstInfoLPM2;
                        break;
                    default:
                        break;
                }

                if (pGrid != null)
                {
                    if (pGrid == pGridCstInfoLPM1 || pGrid == pGridCstInfoLPM2)
                    {
                        CassetteInfoKey c = (CassetteInfoKey)pGrid.SelectedObject;

                        EventHandler eh = new EventHandler(WaferInfoMenuClick);
                        MenuItem[] ami = {
                        new MenuItem("삭제",eh) { Tag = c}, };

                        ContextMenu = new System.Windows.Forms.ContextMenu(ami);
                    }
                    else
                    {
                        WaferInfoKey w = (WaferInfoKey)pGrid.SelectedObject;
                        EventHandler eh = new EventHandler(WaferInfoMenuClick);
                        MenuItem[] ami = {
                        new MenuItem("삭제",eh) { Tag = w},
                        new MenuItem("전체 삭제",eh) {Tag = w }, };

                        ContextMenu = new System.Windows.Forms.ContextMenu(ami);
                    }
                }
            }
        }

        private void lblHasNewTerminalMsg_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FrmTerminalMsg")
                {
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {
                        openForm.WindowState = FormWindowState.Normal;
                    }
                    openForm.Activate();
                }
            }
            _frmTerminalMsg.Show();
        }

        private void btnPM_Click(object sender, EventArgs e)
        {
            if (GG.Equip.EquipRunMode == EmEquipRunMode.Manual)
            {
                GG.Equip.IsPM = !GG.Equip.IsPM;
            }
            else
            {
                InterLockMgr.AddInterLock("EQUIP STATE", GG.boChinaLanguage ? "MANUAL 状态下才可变更." : "MANUAL 상태에서만 변경 가능합니다.");
            }
        }
        private void lblRFIDStat_DoubleClick(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            if (lbl.Name == "lblRFID1Stat")
            {
                if (GG.Equip.Efem.LoadPort1.RFR.IsOpen() == false)
                {
                    GG.Equip.Efem.LoadPort1.RFR.Open();
                }
            }
            else if (lbl.Name == "lblRFID2Stat")
            {
                if (GG.Equip.Efem.LoadPort2.RFR.IsOpen() == false)
                {
                    GG.Equip.Efem.LoadPort2.RFR.Open();
                }
            }
        }

        private void lblBCRStat_DoubleClick(object sender, EventArgs e)
        {
            if (GG.Equip.BCR1.IsOpen() == false)
            {
                GG.Equip.BCR1.Open();
            }
            if (GG.Equip.BCR2.IsOpen() == false)
            {
                GG.Equip.BCR2.Open();
            }
        }

        private void lblEFUStat_DoubleClick(object sender, EventArgs e)
        {
            if (GG.Equip.EFUCtrler.IsOpen() == false)
            {
                GG.Equip.EFUCtrler.Open();
            }
        }

        private void lblOCRStat_DoubleClick(object sender, EventArgs e)
        {
            GG.Equip.OCR.Open(GG.Equip.InitSetting.OcrIP, GG.Equip.InitSetting.OcrPort);
        }

        private void btnTTTM_Click(object sender, EventArgs e)
        {
            GG.Equip.processTTTMCount = 0;
            GG.Equip.IsTTTMMode = !GG.Equip.IsTTTMMode;
        }

        private void rdCimMode_Click(object sender, EventArgs e)
        {
            RadioButton rdbtn = sender as RadioButton;

            if (rdbtn == rdOffLine)
            {
                if (GG.Equip.SetCimMode(EmCimMode.OffLine))
                {
                    GG.MEM_DIT.VirSetShort(CIMAW.XW_CTRL_MODE_CHANGE_STATE, (short)EmCimMode.OffLine);
                    rdOffLine.Checked = true;
                }
            }
            else if (rdbtn == rdLocal)
            {
                if (GG.Equip.SetCimMode(EmCimMode.Local))
                {
                    GG.MEM_DIT.VirSetShort(CIMAW.XW_CTRL_MODE_CHANGE_STATE, (short)EmCimMode.Local);
                    rdLocal.Checked = true;
                }
                else
                {
                    rdOffLine.Checked = true;
                }
            }
            else if (rdbtn == rdRemote)
            {
                if (GG.Equip.SetCimMode(EmCimMode.Remote))
                {
                    GG.MEM_DIT.VirSetShort(CIMAW.XW_CTRL_MODE_CHANGE_STATE, (short)EmCimMode.Remote);
                    rdRemote.Checked = true;
                }
            }
            GG.Equip.HsmsPc.OnCtrlStateChangeEvent(GG.Equip, null);
        }

        private void rdOHT_Click(object sender, EventArgs e)
        {
            if (GG.Equip.SetOHTMode(EmLoadType.OHT) == false)
            {
                return;
            }
        }

        private void rdManual_Click(object sender, EventArgs e)
        {
            GG.Equip.SetOHTMode(EmLoadType.Manual);
        }

        private void btnLpm1OHTComplete_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btnLpm1OHTLdComplete)
            {
                GG.Equip.Efem.LoadPort1.OHTpio.StepReset();
                GG.Equip.Efem.LoadPort1.OHTpio.OHTLoadComplete();

                HsmsPortInfo info = new HsmsPortInfo();
                info.CstID = "";
                info.IsCstExist = true;
                info.LoadportNo = 1;
                info.PortMode = PortMode.LOAD_COMPLETE;
                GG.Equip.HsmsPc.StartCommand(GG.Equip, EmHsmsPcCommand.PORT_MODE_CHANGE, info);
            }
            else if (btn == btnLpm2OHTLdComplete)
            {
                GG.Equip.Efem.LoadPort2.OHTpio.StepReset();
                GG.Equip.Efem.LoadPort2.OHTpio.OHTLoadComplete();

                HsmsPortInfo info = new HsmsPortInfo();
                info.CstID = "";
                info.IsCstExist = true;
                info.LoadportNo = 2;
                info.PortMode = PortMode.LOAD_COMPLETE;
                GG.Equip.HsmsPc.StartCommand(GG.Equip, EmHsmsPcCommand.PORT_MODE_CHANGE, info);
            }

            else if (btn == btnLpm1OHTUldComplete)
            {
                GG.Equip.Efem.LoadPort1.OHTpio.StepReset();
                GG.Equip.Efem.LoadPort1.OHTpio.OHTUnLoadComplete();

                HsmsPortInfo info = new HsmsPortInfo();
                info.CstID = "";
                info.IsCstExist = true;
                info.LoadportNo = 1;
                info.PortMode = PortMode.UNLOAD_COMPLETE;
                GG.Equip.HsmsPc.StartCommand(GG.Equip, EmHsmsPcCommand.PORT_MODE_CHANGE, info);
            }
            else if (btn == btnLpm2OHTUldComplete)
            {
                GG.Equip.Efem.LoadPort2.OHTpio.StepReset();
                GG.Equip.Efem.LoadPort2.OHTpio.OHTUnLoadComplete();

                HsmsPortInfo info = new HsmsPortInfo();
                info.CstID = "";
                info.IsCstExist = true;
                info.LoadportNo = 2;
                info.PortMode = PortMode.UNLOAD_COMPLETE;
                GG.Equip.HsmsPc.StartCommand(GG.Equip, EmHsmsPcCommand.PORT_MODE_CHANGE, info);
            }
        }
        private void btnLpm1OHTStepReset_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btnLpm1OHTUldComplete)
            {
                GG.Equip.Efem.LoadPort1.OHTpio.StepReset();
            }
            else if (btn == btnLpm2OHTUldComplete)
            {
                GG.Equip.Efem.LoadPort2.OHTpio.StepReset();
            }
        }

        private void btnPreAligner_Click(object sender, EventArgs e)
        {

        }

        private void tabOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tab = sender as TabControl;

            if (tab.SelectedTab == tabPageEngineer)
            {
                tabCtrl_MainMenu.SelectTab("tabp_Step");
            }
            else
            {
                tabCtrl_MainMenu.SelectTab("tabp_Progress");
            }
        }

        private void lblCimStatus_Click(object sender, EventArgs e)
        {
            EventHandler eh = new EventHandler(CimStatusChagne);

            MenuItem[] m = new MenuItem[]
            {
                new MenuItem("Offline", eh),
                new MenuItem("Local", eh),
                new MenuItem("Remote", eh)
            };

            ContextMenu cm = new ContextMenu(m);
            cm.Show(lblCimStatus, new Point(lblCimStatus.Location.X - 100, lblCimStatus.Location.Y));
        }

        private void CimStatusChagne(object sender, EventArgs e)
        {
            MenuItem mI = (MenuItem)sender;
            string str = mI.Text;

            if (GG.Equip.EquipRunMode == EmEquipRunMode.Auto)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Auto Run 状态时，无法变更 CIM Mode , 需转换为Manual后进行变更." : "오토런 상태일 때에는 CIM 모드 변경 불가, Manual로 전환 후 변경 해주세요");
                return;
            }

            if (str == "Offline")
            {
                if (GG.CimTestMode)
                {
                    GG.Equip.SetCimMode(EmCimMode.OffLine);
                    return;
                }

                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "<CIM MODE 变更失败>" : "<CIM MODE 변경 실패>", GG.boChinaLanguage ? "变更为Offline要通过 CIM Program进行变更" : "Offline으로 변경은 CIM 프로그램을 통해서 변경 해주세요");
                //GG.Equip.HsmsPc.StartCommand(GG.Equip, EmHsmsPcCommand.CTRL_MODE_CHANGE, EmHsmsCtrlMode.OFFLINE);
            }
            else if (str == "Local")
            {
                if (GG.CimTestMode)
                {
                    GG.Equip.SetCimMode(EmCimMode.Local);
                    return;
                }
                GG.Equip.HsmsPc.StartCommand(GG.Equip, EmHsmsPcCommand.CTRL_MODE_CHANGE, EmHsmsCtrlMode.LOCAL);
            }
            else if (str == "Remote")
            {
                if (GG.CimTestMode)
                {
                    GG.Equip.SetCimMode(EmCimMode.Remote);
                    return;
                }
                GG.Equip.HsmsPc.StartCommand(GG.Equip, EmHsmsPcCommand.CTRL_MODE_CHANGE, EmHsmsCtrlMode.REMOTE);
            }
        }

        private void btnDeleteAllInfo_Click(object sender, EventArgs e)
        {
            if (GG.Equip.Efem.Robot.Status.IsLowerArmVacOn || GG.Equip.Efem.Robot.Status.IsUpperArmVacOn || GG.Equip.Efem.Aligner.Status.IsWaferExist ||
                GG.Equip.IsWaferDetect != EmGlassDetect.NOT)
            {
                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "所有 Wafer复原到 CST是才可删除" : "모든 웨이퍼가 카세트로 복귀 되었을때만 삭제가 가능합니다");
                return;
            }

            if (TransferDataMgr.DeleteAllCassette() && TransferDataMgr.DeleteAllWafer())
            {
                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "所有 CST和Wafer情报  DB被删除" : "모든 카세트와 웨이퍼 정보 DB가 삭제되었습니다");
            }
            else
            {
                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "所有CST和 Wafer情报删除失败 " : "모든 카세트와 웨이퍼 정보 삭제 실패");
            }
        }

        private void btnLogPath_Click(object sender, EventArgs e)
        {
            string filePath = @"D:\DitCtrl\Exec\Ctrl\Log";
            filePath = Path.Combine(filePath, DateTime.Now.ToString("yy-MM-dd"));
            Process.Start(filePath);
        }

        private void btnInfoLog_Click(object sender, EventArgs e)
        {
            tabCtrlTransferData.SelectedIndex = 2;
        }

        private void lstAutoDataLog_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView lst = sender as ListView;

            if (lst.SelectedItems.Count > 0)
            {
                foreach (Form openForm in Application.OpenForms)
                {
                    if (openForm.Name == "FrmTerminalMsg")
                    {
                        if (openForm.WindowState == FormWindowState.Minimized)
                        {
                            openForm.WindowState = FormWindowState.Normal;
                        }
                        openForm.Activate();
                    }
                }
                _frmTerminalMsg.SelectIndex(lst.SelectedItems[0].Index);
                _frmTerminalMsg.Show();
            }
        }

        private void btnLpm2DeepReviewPass_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btnLpm1DeepReviewPass)
            {
                GG.Equip.Efem.LoadPort1.PassDeepLearningReview = true;
            }
            else if (btn == btnLpm2DeepReviewPass)
            {
                GG.Equip.Efem.LoadPort2.PassDeepLearningReview = true;
            }
        }

        private void btnMotorLongRun_Click(object sender, EventArgs e)
        {
            if (GG.Equip.MotorLongRun.step != EmLONGRUN_NO.S000_LONG_RUN_WAIT)
            {
                GG.Equip.MotorLongRun.StepStop();
            }
            else
            {
                GG.Equip.MotorLongRun.StepStart();

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // ECID Test
            GG.Equip.HsmsPc.StartCommand(GG.Equip, EmHsmsPcCommand.ECID_CHANGE, 0);
        }
    }
}

