using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EquipMainUi.Struct;
using DitCim.PLC;
using EquipMainUi.ConvenienceClass;
using Dit.Framework.PLC;
using EquipMainUi.Setting;
using EquipMainUi.Struct.Detail;
using Dit.Framework.UI.UserComponent;
using EquipMainUi.Struct.Detail.PC;

namespace EquipMainUi
{
    public partial class FrmOperOption : Form
    {
        Color _clrBlue = Color.FromArgb(144, 200, 246);
        Color _clrRed = Color.FromArgb(255, 100, 100);

        private Equipment Equip;

        public FrmOperOption(Equipment equip)
        {
            InitializeComponent();
            ChangeChinaLanguage();

            Equip = equip;

            gbSetUpOption.Enabled = LoginMgr.Instance.LoginedUser.Level == EM_LV_LST.USER ? false : true;

            panel13.Visible = GG.IsDitPreAligner;

            ExtensionUI.AddClickEventLog(this);
            tmrUiUpdate.Start();
        }

        private void ChangeChinaLanguage()
        {
            if (GG.boChinaLanguage)
            {
                // 제목
                label8.Text = "■ RFID 直接输入 (Read 失败, 重复ID)";                        // ■ RF 직접입력 (실패 or 중복ID)
                label11.Text = "■ Aligner OCR 使用";                       // ■ Aligner OCR 사용
                label12.Text = "■ Aligner BCR 使用";                       // ■ Aligner BCR 사용
                label14.Text = "■ Air Knife 使用";                       // ■ Air Knife 사용 모드
                label1.Text = "■ Recipe Error 时, 使用1号 Recipe";                        // ■ 레시피 이상 시 1번 사용
                label5.Text = "■ EFEM 使用";                        // ■ EFEM 사용
                label310.Text = "■ 无视检查应答";                      // ■ 검사 응답 무시
                btnUseInspMotorControl.Text = "控制 -> 检查 Motor 控制权";        // 검사 모터 제어
                label4.Text = "■ Interlock 解除";                        // ■ InterLock 해제
                label9.Text = "■ Door Interlock 解除";                        // ■ Door InterLock 해제
                label13.Text = "■ Aligner Single Motor Auto Run";                       // ■  Aligner 단동 오토런

                // 버튼
                // 사용 버튼
                btnEFEMCstIDUserInputModeOn.Text = "使用";
                btnOCRUseOn.Text = "使用";
                btnBCRUseOn.Text = "使用";
                btnUseAirknife.Text = "使用";
                btnEFEMLongOn.Text = "使用";
                btndLongrunMode_On.Text = "使用";
                btnUseRecipeEx_On.Text = "使用";
                btnInternalTestMode_On.Text = "使用";
                btnInspAckIgnore_On.Text = "使用";
                btnInterlockOff_On.Text = "使用";
                btnBuzzerAllOn.Text = "使用";
                btnDoorInterlockOff_On.Text = "使用";
                btnAutoOnlyAlignerUse.Text = "使用";
                // 미사용 버튼
                btnEFEMCstIDUserInputModeOff.Text = "未使用";
                btnOCRUseOff.Text = "未使用";
                btnBCRUseOff.Text = "未使用";
                btnUnUseAirknife.Text = "未使用";
                btnEFEMLongOff.Text = "未使用";
                btndLongrunMode_Off.Text = "未使用";
                btnUseRecipeEx_Off.Text = "未使用";
                btnInternalTestMode_Off.Text = "未使用";
                btnInspAckIgnore_Off.Text = "未使用";
                btnInterlockOff_Off.Text = "未使用";
                btnDoorInterlockOff_Off.Text = "未使用";
                btnBuzzerAllOff.Text = "未使用";
                btnAutoOnlyAlignerUnuse.Text = "未使用";
                // 나머지                        
                btnAlignerHome.Text = "原点";                // 홈
                btnAutoAlignerStart.Text = "开始";           // 시작
                btnAutoAlignerStop.Text = "临时停止";            // 정지
            }
        }

        private void tmrUiUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                UpdateEtcState();
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
        private void btnReviewSkip_Click(object sender, EventArgs e)
        {
            if (Equip.IsReviewSkip == EmReviewSkip.None)
                Equip.IsReviewSkip = EmReviewSkip.Request;
            else
                Equip.IsReviewSkip = EmReviewSkip.None;
        }


        private void btnLiftpinEtcExMode_Click(object sender, EventArgs e)
        {
            Equip.IsUseLiftpinVacuumCenteringExMode = !Equip.IsUseLiftpinVacuumCenteringExMode;
        }

        private void UpdateEtcState()
        {
            //운영 옵션
            btndLongrunMode_On.OnOff = Equip.IsLongTest;
            btndLongrunMode_Off.OnOff = !Equip.IsLongTest;

            btndNoGlassMode_On.OnOff = Equip.IsNoGlassMode;
            btndNoGlassMode_Off.OnOff = !Equip.IsNoGlassMode;
            btnPPIDUseMode_On.OnOff = Equip.CtrlSetting.AutoRecipeChange == 1;
            btnPPIDUseMode_Off.OnOff = Equip.CtrlSetting.AutoRecipeChange == 0;

            btnGlassUnloadingOn.OnOff = Equip.IsGlassUnloading;
            btnGlassUnloadingOff.OnOff = !Equip.IsGlassUnloading;

            btnBuzzerAllOn.OnOff = !Equip.IsBuzzerAllOff;
            btnBuzzerAllOff.OnOff = Equip.IsBuzzerAllOff;

            btnAllServoHomPosi.OnOff = Equip.IsHomePosition;
            btnAllServoHomPosi.Flicker = Equip.IsHomePositioning;

            btnUseSkipOn.OnOff = GG.VacuumFailSkipMode;
            btnUseSkipOff.OnOff = !GG.VacuumFailSkipMode;

            btnUseAirknife.OnOff = Equip.CtrlSetting.AirKnifeUse;
            btnUnUseAirknife.OnOff = !Equip.CtrlSetting.AirKnifeUse;

            btnReviewJudgeModeOn.OnOff = GG.Equip.CtrlSetting.ReviewJudgeMode;
            btnReviewJudgeModeOff.OnOff = !GG.Equip.CtrlSetting.ReviewJudgeMode;

            //셋업 옵션
            btnInterlockOff_On.OnOff = Equip.IsUseInterLockOff;
            btnInterlockOff_Off.OnOff = !Equip.IsUseInterLockOff;

            btnDoorInterlockOff_On.OnOff = Equip.IsUseDoorInterLockOff;
            btnDoorInterlockOff_Off.OnOff = !Equip.IsUseDoorInterLockOff;

            btnInspAckIgnore_On.OnOff = GG.InspTestMode;
            btnInspAckIgnore_Off.OnOff = !GG.InspTestMode;

            btnUseRecipeEx_On.OnOff = GG.RecipeEx;
            btnUseRecipeEx_Off.OnOff = !GG.RecipeEx;

            btnEFEMCstIDUserInputModeOn.OnOff = GG.Equip.CtrlSetting.IsRFKeyIn;
            btnEFEMCstIDUserInputModeOff.OnOff = !GG.Equip.CtrlSetting.IsRFKeyIn;

            btnInternalTestMode_On.OnOff = !GG.EfemNoUse;
            btnInternalTestMode_Off.OnOff = GG.EfemNoUse;

            btnEquipManualLoading_On.OnOff = GG.ManualLdUld;
            btnEquipManualLoading_Off.OnOff = !GG.ManualLdUld;

            btnEFEMLongOn.OnOff = GG.EfemLongRun;
            btnEFEMLongOff.OnOff = !GG.EfemLongRun;

            btnNoWaferOn.OnOff = GG.EfemNoWafer;
            btnNoWaferOff.OnOff = !GG.EfemNoWafer;

            btnOCRUseOn.OnOff = GG.Equip.CtrlSetting.UseOCR;
            btnOCRUseOff.OnOff = !GG.Equip.CtrlSetting.UseOCR;

            //btnOCROnlyRead.OnOff = GG.Equip.CtrlSetting.UseOnlyReadOCR;

            btnBCRUseOn.OnOff = GG.Equip.CtrlSetting.UseBCR;
            btnBCRUseOff.OnOff = !GG.Equip.CtrlSetting.UseBCR;

            btnAutoOnlyAlignerUse.OnOff = GG.Equip.IsAutoOnlyAligner;
            btnAutoOnlyAlignerUnuse.OnOff = !GG.Equip.IsAutoOnlyAligner;
        }

        #region 운영 옵션
        private void btndLongrunMode_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            if (btn == btndLongrunMode_On)
                Equip.IsLongTest = true;
            else if (btn == btndLongrunMode_Off)
                Equip.IsLongTest = false;
            Logger.Log.AppendLine(LogLevel.Info, "Long Run Mode " + (Equip.IsLongTest == true ? "On" : "Off"));
        }

        private void btndNoGlassMode_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            if (btn == btndNoGlassMode_On)
                Equip.IsNoGlassMode = true;
            else if (btn == btndNoGlassMode_Off)
                Equip.IsNoGlassMode = false;
            Logger.Log.AppendLine(LogLevel.Info, "No Glass Mode Mode " + (Equip.IsNoGlassMode == true ? "On" : "Off"));
        }

        private void btnPPIDUseMode_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            Equip.CtrlSetting.ToggleAutoRecipeChangeMode();

            Logger.Log.AppendLine(LogLevel.Info, "PPID Use Mode " + (Equip.CtrlSetting.AutoRecipeChange == 1 ? "On" : "Off"));
        }

        private void btnGlassUnloading_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            if (btn == btnGlassUnloadingOn)
            {
                if (Equip.IsNoGlassMode == true ||
                    (Equip.IsWaferDetect != EmGlassDetect.ALL))
                {
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<GLASS DETECT ERROR>\n(不是正常 Glass状态时, 无法选择Glass排出.)" : "인터락<GLASS DETECT ERROR>\n정상적인 글라스 상태가 아닐 경우 글라스 배출을 선택 할 수 없습니다.");
                    Logger.Log.AppendLine(LogLevel.Warning, "글라스 감지 에러! 글라스 배출 선택 불가!");
                    return;
                }
                if (Equip.IsHomePosition == false)
                {
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Interlock<HOME POSITION>\n(只有在Home Position 状态时，可选择 Glass排出 .)" : "인터락<HOME POSITION>\n홈 포지션 상태일 경우만 글라스 배출 선택 가능합니다.");
                    Logger.Log.AppendLine(LogLevel.Warning, "홈 포지션 상태 아님! 글라스 배출 선택 불가!");
                    return;
                }

                Equip.IsGlassUnloading = true;
                if (Equip.IsCycleStop != EmCycleStop.Request)
                {
                    Equip.IsCycleStop = EmCycleStop.Request;
                }
            }
            else if (btn == btnGlassUnloadingOff)
                Equip.IsGlassUnloading = false;

            Logger.Log.AppendLine(LogLevel.Info, "UnLoading Mode " + (Equip.IsGlassUnloading == true ? "On" : "Off"));
        }

        private void btnBuzzer3_Click(object sender, EventArgs e)
        {

        }

        private void btnBuzzerAll_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            if (btn == btnBuzzerAllOn)
                Equip.IsBuzzerAllOff = false;
            else if (btn == btnBuzzerAllOff)
                Equip.IsBuzzerAllOff = true;
            Logger.Log.AppendLine(LogLevel.Info, "Buzzer Off Mode " + (Equip.IsBuzzerAllOff == true ? "On" : "Off"));
        }

        private void btnAllServoHomPosi_Click(object sender, EventArgs e)
        {
            Equip.StHoming.StepStart(Equip);
        }

        #endregion

        #region 셋업 옵션
        private void btnInterLockOff_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            if (btn == btnInterlockOff_On)
                Equip.IsUseInterLockOff = true;
            else if (btn == btnInterlockOff_Off)
                Equip.IsUseInterLockOff = false;
        }

        private void btnDoorInterLockOff_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            if (btn == btnDoorInterlockOff_On)
                Equip.IsUseDoorInterLockOff = true;
            else if (btn == btnDoorInterlockOff_Off)
                Equip.IsUseDoorInterLockOff = false;
        }

        private void btnInspAckIgnore_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            if (btn == btnInspAckIgnore_On)
                GG.InspTestMode = true;
            else if (btn == btnInspAckIgnore_Off)
                GG.InspTestMode = false;
            else if (btn == btnUseRecipeEx_On)
                GG.RecipeEx = true;
            else if (btn == btnUseRecipeEx_Off)
                GG.RecipeEx = false;
        }

        private void btnReviAckIgnore_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            if (btn == btnEFEMCstIDUserInputModeOn)
                GG.Equip.CtrlSetting.IsRFKeyIn = true;
            else if (btn == btnEFEMCstIDUserInputModeOff)
                GG.Equip.CtrlSetting.IsRFKeyIn = false;
            GG.Equip.CtrlSetting.Save();
        }

        private void btnZAxisIgnore_Click(object sender, EventArgs e)
        {

        }

        private void btnInternalTestMode_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;

            if (btn == btnInternalTestMode_On)
            {
                GG.EfemNoUse = false;
            }
            else if (btn == btnInternalTestMode_Off)
            {
                GG.EfemNoUse = true;
            }

        }
        #endregion

        private void btnTest_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FrmTester")
                {
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {
                        openForm.WindowState = FormWindowState.Normal;
                    }
                    openForm.Dispose();
                    break;
                }
            }

            FrmTester ff = new FrmTester(Equip);
            ff.Show();
        }

        private void btnEfemTest_Click(object sender, EventArgs e)
        {
            FrmEfemTest f = new FrmEfemTest(Equip);
            f.Show();
        }

        private void btnEFEMHome_Click(object sender, EventArgs e)
        {
            Equip.Efem.ChangeMode(Struct.Step.EmEfemRunMode.Home);
        }

        private void btnEquipManualLoading_On_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btnEquipManualLoading_On)
            {
                GG.ManualLdUld = true;
            }
            else if (btn == btnEquipManualLoading_Off)
            {
                GG.ManualLdUld = false;
            }
        }

        private void btnEFEMLongOff_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btnEFEMLongOn)
            {
                if ((GG.Equip.Efem.LoadPort1.LoadType == EmLoadType.Manual && GG.Equip.Efem.LoadPort1.ProgressWay == EmProgressWay.Mapping) == false
                    || (GG.Equip.Efem.LoadPort2.LoadType == EmLoadType.Manual && GG.Equip.Efem.LoadPort2.ProgressWay == EmProgressWay.Mapping) == false)
                {
                    InterLockMgr.AddInterLock("LongRun모드", GG.boChinaLanguage ? "两个类型 LOADPORT 都 Manual 投入, Map Data Mode状态下可以" : "두 LOADPORT 모두 매뉴얼투입, Map Data 모드여야 가능합니다");
                    return;
                }
                GG.EfemLongRun = true;
            }
            else if (btn == btnEFEMLongOff)
            {
                GG.EfemLongRun = false;
                GG.Equip.IsNoGlassMode = false;
            }
        }

        private void btnNoWaferOn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btnNoWaferOn)
            {
                //jys:: 190816 no wafer 사용금지, EFEM 설정해야하고 로봇암 정상화 이후 테스트한적 없음.
                if (GG.Equip.Efem.Robot.Status.IsLowerArmVacOn == true || GG.Equip.Efem.Robot.Status.IsUpperArmVacOn == true
                    || GG.Equip.Efem.Aligner.Status.IsWaferExist == true || GG.Equip.IsWaferDetect != EmGlassDetect.NOT)
                {
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Wafer 感应状态是，无法 No Wafer Mode" : "Wafer 감지상태에서 No Wafer Mode불가능합니다");
                    return;
                }

                GG.EfemNoWafer = true;
                GG.Equip.IsWaferDetect = EmGlassDetect.NOT;
                GG.InspTestMode = true;
                CheckMgr.AddCheckMsg(true, GG.boChinaLanguage ? "无视检查应答模式会自动开启" : "검사 응답 무시는 자동으로 켜집니다");
            }
            else if (btn == btnNoWaferOff)
            {
                GG.EfemNoWafer = false;
            }
        }

        private void btnOCRUseOn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btnOCRUseOn)
            {
                GG.Equip.CtrlSetting.UseOCR = true;
            }
            else if (btn == btnOCRUseOff)
            {
                GG.Equip.CtrlSetting.UseOCR = false;
                GG.Equip.CtrlSetting.UseOnlyReadOCR = false;
            }
            else if (btn == btnBCRUseOn)
            {
                GG.Equip.CtrlSetting.UseBCR = true;
            }
            else if (btn == btnBCRUseOff)
            {
                GG.Equip.CtrlSetting.UseBCR = false;
            }

            if (GG.Equip.CtrlSetting.UseBCR == false && GG.Equip.CtrlSetting.UseOCR == false)
            {
                GG.Equip.CtrlSetting.UseBCR = true;
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "在OCR, BCR (中)得使用一个才可以" : "OCR, BCR 중 하나는 사용해야합니다");
            }

            if (GG.Equip.CtrlSetting.UseBCR == false && GG.Equip.CtrlSetting.UseOnlyReadOCR == true)
            {
                GG.Equip.CtrlSetting.UseBCR = true;
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "OCR 验证 Mode是 OCR, BCR 都使用才可以" : "OCR 검증 모드는 OCR,BCR 모두 사용으로 해야합니다.");
            }

            if (GG.Equip.CtrlSetting.UseOCR == false && GG.Equip.CtrlSetting.UseOnlyReadOCR == true)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "OCR 验证 Mode是 OCR, BCR 都使用才可以" : "OCR 검증 모드는 OCR,BCR 모두 사용으로 해야합니다.");
            }

            GG.Equip.CtrlSetting.Save();
        }

        private void btnUseInspMotorControl_Click(object sender, EventArgs e)
        {
            if (GG.Equip.EquipRunMode == EmEquipRunMode.Auto)
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Auto 状态下，检查 Server里是无法控制的." : "Auto 상태에선 검사 서버에서 제어할 수 없습니다");
            else
                GG.Equip.InspPc.SetInspectServerMotorInterlockOff(!IsptAddrB.YB_MotorInterlockOffState.vBit);
        }

        private void btbWaferSkipUse_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == btnUseSkipOn)
            {
                GG.VacuumFailSkipMode = true;
            }
            else if (btn == btnUseSkipOff)
            {
                GG.VacuumFailSkipMode = false;
            }
        }

        private void btnAutoOnlyAlignerUse_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            if (btn == btnAutoOnlyAlignerUse)
                Equip.IsAutoOnlyAligner = true;
            else if (btn == btnAutoOnlyAlignerUnuse)
                Equip.IsAutoOnlyAligner = false;
            Logger.Log.AppendLine(LogLevel.Info, "AutoRun Only ALigner " + (Equip.IsAutoOnlyAligner == true ? "Use" : "Unuse"));
        }

        private void btnAutoAlignerStart_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            if (btn == btnAlignerHome)
            {
                GG.Equip.Efem.RunMode = Struct.Step.EmEfemRunMode.Home;
            }
            else if (btn == btnAutoAlignerStart)
            {
                GG.Equip.Efem.Aligner.SetSeq(Struct.Detail.EFEM.Step.EmEFEMAlignerSeqStep.S240_ALIGN_START);
                GG.Equip.Efem.RunMode = Struct.Step.EmEfemRunMode.Start;
            }
            else if (btn == btnAutoAlignerStop)
            {
                GG.Equip.Efem.RunMode = Struct.Step.EmEfemRunMode.Stop;
            }
            Logger.Log.AppendLine(LogLevel.Info, "AutoRun Only ALigner " + (btn == btnAutoAlignerStart == true ? "Start" : "Stop"));
        }

        private void btnUseAirknife_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            if (btn == btnUseAirknife)
                Equip.CtrlSetting.AirKnifeUse = true;
            else if (btn == btnUnUseAirknife)
                Equip.CtrlSetting.AirKnifeUse = false;
            GG.Equip.CtrlSetting.Save();
            Logger.Log.AppendLine(LogLevel.Info, "Air Knife Use Mdoe : " + (Equip.IsAutoOnlyAligner == true ? "Use" : "Unuse"));
        }

        private void btnReviewJudgeModeOn_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            if (btn == btnReviewJudgeModeOn)
            {
                GG.Equip.CtrlSetting.ReviewJudgeMode = true;
            }
            else if (btn == btnReviewJudgeModeOff)
            {
                GG.Equip.CtrlSetting.ReviewJudgeMode = false;
            }
            Logger.Log.AppendLine(LogLevel.Info, "ReviewJudgeMode " + (GG.Equip.CtrlSetting.ReviewJudgeMode == true ? "Use" : "Unuse"));
            GG.Equip.CtrlSetting.Save();
        }
    }
}
