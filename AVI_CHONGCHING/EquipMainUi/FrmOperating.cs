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
using EquipMainUi.ConvenienceClass;
using EquipMainUi.Struct.BaseUnit;
using EquipMainUi.Struct.Detail.PC;
using Dit.Framework.UI.UserComponent;
using EquipMainUi.Setting;
using EquipMainUi.Struct.Detail.EFEM;
using EquipMainUi.Struct.TransferData;
using static EquipMainUi.Struct.Step.WaferTransferLogic;

namespace EquipMainUi
{
    public partial class FrmOperating : Form
    {
        public Equipment _equip;

        private Color _clrRed = Color.FromArgb(255, 100, 100);

        public FrmOperating(Equipment equip)
        {
            _equip = equip;
            InitializeComponent();
            ExtensionUI.AddClickEventLog(this);

            tmrUiUpdate.Start();
            ChangeChinaLanguage();

            ucrlPtpX.Spd = equip.StageX.SoftJogSpeedLimit;
            ucrlPtpTheta.Spd = equip.Theta.SoftJogSpeedLimit;
            ucrlPtpY1.Spd = equip.StageY.SoftJogSpeedLimit;

            InitSelPosMoveCmb();

            tabCtrl_Operator.SelectedTab = tabp_BaseOper;
        }

        private void ChangeChinaLanguage()
        {
            if (GG.boChinaLanguage)
            {
                cmbInspXSelPos.Text = "位置选择";                   // 위치 선택
                cmbInspYSelPos.Text = "位置选择";                   // 위치 선택
                cmbThetaSelPos.Text = "位置选择";                   // 위치 선택
                btnInspXMoveLoading.Text = "位置移动";              // Ptp 이동
                btnInspYMoveLoading.Text = "位置移动";              // Ptp 이동
                btnThetaMoveLoading.Text = "位置移动";              // Ptp 이동
                label27.Text = "■ Lift Pin";               // ■ 리프트핀
                btnLiftPinUp.Text = "上升";                     // 상승
                btnLiftPinDown.Text = "下降";                   // 하강
                label7.Text = "■ Centering";               // ■ 센터링
                label62.Text = "■ Standard Centering";     // ■ 기준 센터링
                btnOrgStdForward.Text = "前进";                 // 전진
                btnOrgStdBack.Text = "后退";                    // 후진
                label5.Text = "■ Ionizer";                  // ■ 이오나이저	// ionizer
                label6.Text = "■ Ionizer remote";           // ■ 이오나이저 remote
                label10.Text = "■ Ionizer Air";             // ■ 이오나이저 Air

                lblInspX.Text = "■ Stage X";         // ■ Stage X축
                label79.Text = "■ Stage Y";      //■ Stage Y축
                label78.Text = "■ Theta";      //■ Theta 축
                label24.Text = "速度";      // 속도
                label20.Text = "速度";      // 속도
                label3.Text = "速度";      // 속도
            }
        }

        protected override void OnShown(EventArgs e)
        {
            if (this.Parent != null)
            {
                btnPin.Visible = false;
            }
            base.OnShown(e);
        }

        private void InitSelPosMoveCmb()
        {
            cmbInspXSelPos.Items.Clear();
            foreach (ServoPosiInfo pos in _equip.StageX.Setting.LstServoPosiInfo)
                cmbInspXSelPos.Items.Add(pos.Name);

            cmbInspYSelPos.Items.Clear();
            foreach (ServoPosiInfo pos in _equip.StageY.Setting.LstServoPosiInfo)
                cmbInspYSelPos.Items.Add(pos.Name);

            cmbThetaSelPos.Items.Clear();
            foreach (ServoPosiInfo pos in _equip.Theta.Setting.LstServoPosiInfo)
                cmbThetaSelPos.Items.Add(pos.Name);
        }

        #region TabControl
        private void tabOperator_SelectedIndex(object sender, EventArgs e)
        {
            TabPage curPage = (sender as TabControl).SelectedTab;

            //if (curPage == tabp_Vacuum)
            //{
            //    pnlVaccum.Dock = DockStyle.Fill;
            //}
            //else if (curPage == tabp_PmacXY)
            //{
            //    pnlUMaxXY.Dock = DockStyle.Fill;
            //}
        }
        #endregion
        #region TrackBar
        private void trbInspX_ValueChanged(object sender, EventArgs e)
        {
            int speed = _equip.StageX.SoftJogSpeedLimit * trbInspX.Value / trbInspX.Maximum;
            lblInspXJogSpeed.Text = speed.ToString();
        }
        private void trbInspY_ValueChanged(object sender, EventArgs e)
        {
            int speed = _equip.StageY.SoftJogSpeedLimit * trbInspY.Value / trbInspY.Maximum;
            lblInspYJogSpeed.Text = speed.ToString();
        }

        private void trbTheta_ValueChanged(object sender, EventArgs e)
        {
            float speed = (float)_equip.Theta.SoftJogSpeedLimit * trbTheta.Value / trbTheta.Maximum;
            lblThetaJogSpeed.Text = speed.ToString();
        }
        #endregion
        #region vacuum / blower / ionizer
        private void btnMainVacuumOn_Click(object sender, EventArgs e)
        {
            //Button btn = sender as Button;

            //if (btn == btnMainVacuumOn)
            //    _equip.Vacuum.Stage1.OnOff(_equip, true);
            //else if (btn == btnMainVacuumOff)
            //    _equip.stava.OnOff(_equip, false);
        }
        private void btnVaccum_DelayClick(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;

            if (btn == btnVaccum1On) _equip.Vacuum.Stage1.OnOff(_equip, true);
            else if (btn == btnVaccum2On) _equip.Vacuum.Stage2.OnOff(_equip, true);
            else if (btn == btnVaccum1Off) _equip.Vacuum.Stage1.OnOff(_equip, false);
            else if (btn == btnVaccum2Off) _equip.Vacuum.Stage2.OnOff(_equip, false);
            else if (btn == btnVaccumAllOn || btn == btnVaccumAllOn2) _equip.Vacuum.AllVacuumOn();
            else if (btn == btnVaccumAllOff || btn == btnVaccumAllOff2) _equip.Vacuum.AllVacuumOff();
        }
        private void btnBlower0On_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == btnBlower1On) _equip.Blower.Stage1.OnOff(_equip, true);
            else if (btn == btnBlower2On) _equip.Blower.Stage2.OnOff(_equip, true);
            else if (btn == btnBlowerAllOn || btn == btnBlowerAllOn2) _equip.Blower.BlowerOn();
        }
        private void btnBlower0On_MouseUp(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == btnBlower1On) _equip.Blower.Stage1.OnOff(_equip, false);
            else if (btn == btnBlower2On) _equip.Blower.Stage2.OnOff(_equip, false);
            else if (btn == btnBlowerAllOn || btn == btnBlowerAllOn2) _equip.Blower.BlowerOff();
        }
        private void Ionizer_Remote_On_Button_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            int idx = int.Parse(btn.Tag.ToString());
            if (idx % 2 == 0)
            {
                _equip.PinIonizer.PowerOn(idx / 2);
            }
            else
            {
                _equip.PinIonizer.PowerOff(idx / 2);
            }
        }
        private void IonizerAir_Btn_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            int idx = int.Parse(btn.Tag.ToString());
            if (idx % 2 == 0)
            {
                _equip.PinIonizer.AirOn(idx / 2);
            }
            else
            {
                _equip.PinIonizer.AirOff(idx / 2);
            }
        }
        #endregion
        #region Jog Move
        private void btnGXMoveJogPlus_MouseDown(object sender, MouseEventArgs e)
        {
            if (_equip.StageX.IsMoving)
                return;

            int speed = _equip.StageX.SoftJogSpeedLimit * trbInspX.Value / trbInspX.Maximum;
            _equip.StageX.JogMove(_equip, EM_SERVO_JOG.JOG_PLUS, speed);
        }
        private void btnGXMoveJogPlus_MouseUp(object sender, MouseEventArgs e)
        {
            //_equip.SvmInspX.InJogPlus = false;
            int speed = _equip.StageX.SoftJogSpeedLimit * trbInspX.Value / trbInspX.Maximum;
            _equip.StageX.JogMove(_equip, EM_SERVO_JOG.JOG_STOP, speed);
        }
        private void btnGXMoveJogMinus_MouseDown(object sender, MouseEventArgs e)
        {
            //_equip.SvmInspX.InSpeed = _equip.SvmInspX.JogSpeed;
            if (_equip.StageX.IsMoving)
                return;
            int speed = _equip.StageX.SoftJogSpeedLimit * trbInspX.Value / trbInspX.Maximum;
            _equip.StageX.JogMove(_equip, EM_SERVO_JOG.JOG_MINUS, speed);
        }
        private void btnGXMoveJogMinus_MouseUp(object sender, MouseEventArgs e)
        {
            //_equip.SvmInspX.InJogMinus = false;
            int speed = _equip.StageX.SoftJogSpeedLimit * trbInspX.Value / trbInspX.Maximum;
            _equip.StageX.JogMove(_equip, EM_SERVO_JOG.JOG_STOP, speed);
        }
        private void btnStageYMoveJogMinus_MouseDown(object sender, MouseEventArgs e)
        {
            if (_equip.StageY.IsMoving)
                return;
            int speed = _equip.StageY.SoftJogSpeedLimit * trbInspY.Value / trbInspY.Maximum;
            _equip.StageY.JogMove(_equip, EM_SERVO_JOG.JOG_MINUS, speed);
        }
        private void btnStageYMoveJogMinus_MouseUp(object sender, MouseEventArgs e)
        {
            int speed = _equip.StageY.SoftJogSpeedLimit * trbInspY.Value / trbInspY.Maximum;
            _equip.StageY.JogMove(_equip, EM_SERVO_JOG.JOG_STOP, speed);
        }
        private void btnStageYMoveJogPlus_MouseDown(object sender, MouseEventArgs e)
        {
            if (_equip.StageY.IsMoving)
                return;
            int speed = _equip.StageY.SoftJogSpeedLimit * trbInspY.Value / trbInspY.Maximum;
            _equip.StageY.JogMove(_equip, EM_SERVO_JOG.JOG_PLUS, speed);
        }
        private void btnStageYMoveJogPlus_MouseUp(object sender, MouseEventArgs e)
        {
            int speed = _equip.StageY.SoftJogSpeedLimit * trbInspY.Value / trbInspY.Maximum;
            _equip.StageY.JogMove(_equip, EM_SERVO_JOG.JOG_STOP, speed);
        }

        private void btnThetaJogPlus_MouseDown(object sender, MouseEventArgs e)
        {
            if (_equip.Theta.IsMoving)
                return;
            float speed = (float)_equip.Theta.SoftJogSpeedLimit * trbTheta.Value / trbTheta.Maximum;
            _equip.Theta.JogMove(_equip, EM_SERVO_JOG.JOG_PLUS, speed);
        }

        private void btnThetaJogPlus_MouseUp(object sender, MouseEventArgs e)
        {
            _equip.Theta.JogMove(_equip, EM_SERVO_JOG.JOG_STOP, 1);
        }

        private void btnThetaJogMinus_MouseDown(object sender, MouseEventArgs e)
        {
            if (_equip.Theta.IsMoving)
                return;
            float speed = (float)_equip.Theta.SoftJogSpeedLimit * trbTheta.Value / trbTheta.Maximum;
            _equip.Theta.JogMove(_equip, EM_SERVO_JOG.JOG_MINUS, speed);
        }

        private void btnThetaJogMinus_MouseUp(object sender, MouseEventArgs e)
        {
            _equip.Theta.JogMove(_equip, EM_SERVO_JOG.JOG_STOP, 1);
        }

        #endregion
        #region StepReset
        private void btnThetaStepReset_DelayClick(object sender, EventArgs e)
        {
            ButtonDelay2 btnd = sender as ButtonDelay2;
            if (btnd == btnThetaMoveHome)
                _equip.Theta.StepReset(0);
            else if (btnd == btnThetaMoveLoading)
                _equip.Theta.StepReset(ThetaServo.HomePosition);
        }
        private void btnInspXStepReset_DelayClick(object sender, EventArgs e)
        {
            ButtonDelay2 btnd = sender as ButtonDelay2;

            if (btnd == btnInspXMoveHome)
                _equip.StageX.StepReset(0);
            else if (btnd == btnInspXMoveLoading)
                _equip.StageX.StepReset(StageXServo.HomePosition);
        }
        private void btnInspYStepReset_DelayClick(object sender, EventArgs e)
        {
            ButtonDelay2 btnd = sender as ButtonDelay2;

            if (btnd == btnInspYMoveHome)
                _equip.StageY.StepReset(0);
            else if (btnd == btnInspYMoveLoading)
                _equip.StageY.StepReset(StageYServo.HomePosition);
        }
        #endregion
        #region Position Move
        private void btnThetaMovePos_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btnd = sender as ButtonDelay2;

            if (btnd == btnThetaMoveHome)
                _equip.Theta.GoHomeOrPositionOne(_equip);
        }
        private void btnInspXMovePos_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btnd = sender as ButtonDelay2;

            if (btnd == btnInspXMoveHome)
                _equip.StageX.GoHomeOrPositionOne(_equip);

        }
        private void btnInspYMovePos_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btnd = sender as ButtonDelay2;

            if (btnd == btnInspYMoveHome)
                _equip.StageY.GoHomeOrPositionOne(_equip);

        }

        #endregion
        #region Centering

        #endregion

        public void SetEnabled(Panel pnl, bool value)
        {
            foreach (Control btn in pnl.Controls)
            {
                if (btn is ButtonDelay2 || btn is Button)
                    ((Button)btn).Enabled = value;
                else if (btn is Panel)
                    SetEnabled((Panel)btn, value);
                else if (btn is GroupBox)
                    ((GroupBox)btn).Enabled = value;
                else if (btn is UserControls.ucrlRobotEasyController)
                    btn.Enabled = value;
            }
        }
        private void tmrUiUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                bool reviewUsePmac = _equip.PMac.XB_ReviewRunning.vBit
               || IsptAddrB.YB_ReviewManualMode.vBit
               ;
                SetEnabled(pnlVaccum, ((_equip.EquipRunMode == EmEquipRunMode.Manual) && (!_equip.IsHomePositioning)) && reviewUsePmac == false);
                SetEnabled(pnlUMaxXY, ((_equip.EquipRunMode == EmEquipRunMode.Manual) && (!_equip.IsHomePositioning)) && reviewUsePmac == false);
                SetEnabled(pnlEFEMManual, (_equip.EquipRunMode == EmEquipRunMode.Manual));
                SetEnabled(pnlBaseOper, (_equip.EquipRunMode == EmEquipRunMode.Manual));

                #region Vacuum / Ionizer
                btnVaccum1On.OnOff = _equip.Vacuum.Stage1.IsOnOff;
                btnVaccum1Off.OnOff = _equip.Vacuum.Stage1.IsOnOff == false;
                lblVaccum1OnTime.Text = _equip.Vacuum.Stage1.OnOffTime;
                lblVaccum1OffTime.Text = _equip.Vacuum.Stage1.OffOnTime;
                btnBlower1On.OnOff = _equip.Blower.Stage1.IsOnOff;

                btnVaccum2On.OnOff = _equip.Vacuum.Stage2.IsOnOff;
                btnVaccum2Off.OnOff = _equip.Vacuum.Stage2.IsOnOff == false;
                lblVaccum2OnTime.Text = _equip.Vacuum.Stage2.OnOffTime;
                lblVaccum2OffTime.Text = _equip.Vacuum.Stage2.OffOnTime;
                btnBlower2On.OnOff = _equip.Blower.Stage2.IsOnOff;

                btnVaccumAllOn.OnOff = btnVaccumAllOn2.OnOff = _equip.Vacuum.IsVacuumSolOn;
                btnVaccumAllOff.OnOff = btnVaccumAllOff2.OnOff = _equip.Vacuum.IsVacuumSolOn == false;
                btnBlowerAllOn.OnOff = btnBlowerAllOn2.OnOff = _equip.Blower.IsSolOn;

                btnIonizerRemote1On.OnOff = _equip.PinIonizer.IsPowerOn(0);
                btnIonizerRemote1Off.OnOff = _equip.PinIonizer.IsPowerOn(0) == false;

                btnIonizerAir1On.OnOff = _equip.PinIonizer.IsAirOn(0);
                btnIonizerAir1Off.OnOff = _equip.PinIonizer.IsAirOn(0) == false;
                lblDecayTime.Text = _equip.PinIonizer.DecayTime.ToString();
                btnAirKnifeOn.OnOff = _equip.AirKnife.IsOnOff;
                btnAirKnifeOff.OnOff = !_equip.AirKnife.IsOnOff;
                #endregion
                #region  Pin
                //Lift Pin
                btnLiftPinUp.OnOff          /**/= _equip.LiftPin.IsForward;
                btnLiftPinUp.Flicker        /**/= _equip.LiftPin.IsForwarding;
                btnLiftPinDown.OnOff        /**/= _equip.LiftPin.IsBackward;
                btnLiftPinDown.Flicker      /**/= _equip.LiftPin.IsBackwarding;
                lblLiftPinTimeUp1.Text = _equip.LiftPin.ForwardTime[0];
                lblLiftPinTimeUp2.Text = _equip.LiftPin.ForwardTime[1];

                lblLiftPinTimeDn1.Text = _equip.LiftPin.BackwardTime[0];
                lblLiftPinTimeDn2.Text = _equip.LiftPin.BackwardTime[1];
                #endregion
                #region Command Lamp


                //THETA
                btnThetaMoveLoading.IsLeftLampOn = _equip.Theta.YB_TargetPosMoveCmd.vBit;
                btnThetaMoveHome.IsLeftLampOn = _equip.Theta.YB_TargetPosMoveCmd.vBit || _equip.Theta.YB_HomeCmd;
                btnThetaMoveLoading.IsRightLampOn = _equip.Theta.XB_TargetPosMoveAck.vBit;
                btnThetaMoveHome.IsRightLampOn = _equip.Theta.XB_TargetPosMoveAck.vBit || _equip.Theta.XB_HomeCmdAck;

                btnInspXMoveHome.IsLeftLampOn = _equip.StageX.YB_HomeCmd || _equip.StageX.YB_TargetPosMoveCmd.vBit;
                btnInspXMoveLoading.IsLeftLampOn = _equip.StageX.YB_TargetPosMoveCmd.vBit;
                btnInspXMoveHome.IsRightLampOn = _equip.StageX.XB_HomeCmdAck || _equip.StageX.XB_TargetPosMoveAck.vBit;
                btnInspXMoveLoading.IsRightLampOn = _equip.StageX.XB_TargetPosMoveAck.vBit;

                btnInspYMoveHome.IsLeftLampOn = _equip.StageY.YB_HomeCmd || _equip.StageY.YB_TargetPosMoveCmd.vBit;
                btnInspYMoveLoading.IsLeftLampOn = _equip.StageY.YB_TargetPosMoveCmd.vBit;
                btnInspYMoveHome.IsRightLampOn = _equip.StageY.XB_HomeCmdAck || _equip.StageY.XB_TargetPosMoveAck.vBit;
                btnInspYMoveLoading.IsRightLampOn = _equip.StageY.XB_TargetPosMoveAck.vBit;


                #endregion
                #region Servo Info

                // Theta
                lblThetaHomeCompleteBit.BackColor = _equip.Theta.IsHomeCompleteBit ? _clrRed : Color.Gainsboro;
                lblThetaMoving.BackColor = _equip.Theta.IsMoving ? _clrRed : Color.Gainsboro;
                lblThetaNegativeLimit.BackColor = _equip.Theta.IsHWNegativeLimit ? _clrRed : Color.Gainsboro;
                lblThetaPositiveLimit.BackColor = _equip.Theta.IsHWPositiveLimit ? _clrRed : Color.Gainsboro;
                lblThetaServoOn.BackColor = _equip.Theta.IsServoOn ? _clrRed : Color.Gainsboro;
                lblThetaErr.BackColor = _equip.Theta.IsServoError ? _clrRed : Color.Gainsboro;

                btnThetaMoveLoading.OnOff       /**/= _equip.Theta.IsMoving == false;
                btnThetaMoveLoading.Flicker     /**/= _equip.Theta.IsMoving;
                btnThetaMoveHome.OnOff       /**/= _equip.Theta.XB_StatusHomeInPosition.vBit;
                btnThetaMoveHome.Flicker     /**/= _equip.Theta.IsHomming;

                btnThetaJogMinus.OnOff = _equip.Theta.YB_JogMinusMove.vBit;
                btnThetaJogPlus.OnOff = _equip.Theta.YB_JogPlusMove.vBit;

                //INSPX 서보. 
                lblInspXHomeCompleteBit.BackColor = _equip.StageX.IsHomeCompleteBit ? _clrRed : Color.Gainsboro;
                lblInspXMoving.BackColor = _equip.StageX.IsMoving ? _clrRed : Color.Gainsboro;
                lblInspXNegativeLimit.BackColor = _equip.StageX.IsHWNegativeLimit ? _clrRed : Color.Gainsboro;
                lblInspXPositiveLimit.BackColor = _equip.StageX.IsHWPositiveLimit ? _clrRed : Color.Gainsboro;
                lblInspXServoOn.BackColor = _equip.StageX.IsServoOn ? _clrRed : Color.Gainsboro;
                lblMotorErrInspX.BackColor = _equip.StageX.IsServoError ? _clrRed : Color.Gainsboro;

                btnInspXMoveHome.OnOff = _equip.StageX.XB_StatusHomeInPosition;
                btnInspXMoveHome.Flicker = _equip.StageX.IsHomming;
                btnInspXMoveLoading.OnOff = _equip.StageX.IsMoving == false;
                btnInspXMoveLoading.Flicker = _equip.StageX.IsMoving;

                btnInspXMoveJogMinus.OnOff = _equip.StageX.YB_JogMinusMove.vBit;
                btnInspXMoveJogPlus.OnOff = _equip.StageX.YB_JogPlusMove.vBit;

                //INSPY 서보. 
                lblInspYHomeCompleteBit.BackColor = _equip.StageY.IsHomeCompleteBit ? _clrRed : Color.Gainsboro;
                lblInspYMoving.BackColor = _equip.StageY.IsMoving ? _clrRed : Color.Gainsboro;
                lblInspYNegativeLimit.BackColor = _equip.StageY.IsHWNegativeLimit ? _clrRed : Color.Gainsboro;
                lblInspYPositiveLimit.BackColor = _equip.StageY.IsHWPositiveLimit ? _clrRed : Color.Gainsboro;
                lblInspYServoOn.BackColor = _equip.StageY.IsServoOn ? _clrRed : Color.Gainsboro;
                lblMotorErrInspY.BackColor = _equip.StageY.IsServoError ? _clrRed : Color.Gainsboro;

                btnInspYMoveHome.OnOff = _equip.StageY.XB_StatusHomeInPosition;
                btnInspYMoveHome.Flicker = _equip.StageY.IsHomming;
                btnInspYMoveLoading.OnOff = _equip.StageY.IsMoving == false;
                btnInspYMoveLoading.Flicker = _equip.StageY.IsMoving;

                btnInspYMoveJogMinus.OnOff = _equip.StageY.YB_JogMinusMove.vBit;
                btnInspYMoveJogPlus.OnOff = _equip.StageY.YB_JogPlusMove.vBit;


                #endregion
                #region Centering
                btnOrgStdForward.OnOff = _equip.StandCentering.IsForward;
                btnOrgStdForward.Flicker = _equip.StandCentering.IsForwarding;

                btnOrgStdBack.OnOff = _equip.StandCentering.IsBackward;
                btnOrgStdBack.Flicker = _equip.StandCentering.IsBackwarding;

                lblStdForwardTimeP1.Text = _equip.StandCentering.ForwardTime[0];
                lblStdForwardTimeP2.Text = _equip.StandCentering.ForwardTime[1];

                lblStdBackTimeP1.Text = _equip.StandCentering.BackwardTime[0];
                lblStdBackTimeP2.Text = _equip.StandCentering.BackwardTime[1];
                #endregion
                btnMutingOn.OnOff = _equip.Efem.LPMLightCurtain.IsMuting;
                btnMutingOff.OnOff = !_equip.Efem.LPMLightCurtain.IsMuting;
                lblLightCurtainDetect.BackColor = _equip.Efem.LPMLightCurtain.Detect.IsOn ? Color.Red : Color.Transparent;
                ucrlRobotEasyController1.UIUpdate();
                UpdateEFEM();
                #region Base Operation


                btnAVIInitial.Flicker = GG.Equip.InitialLogic.IsRunning() && GG.Equip.InitialLogic.TargetPort == EmEfemPort.EQUIPMENT;
                btnRobotInitial.Flicker = GG.Equip.InitialLogic.IsRunning() && GG.Equip.InitialLogic.TargetPort == EmEfemPort.ROBOT;
                btnAlignerInitial.Flicker = GG.Equip.InitialLogic.IsRunning() && GG.Equip.InitialLogic.TargetPort == EmEfemPort.ALIGNER;
                btnLPM1Initial.Flicker = GG.Equip.InitialLogic.IsRunning() && GG.Equip.InitialLogic.TargetPort == EmEfemPort.LOADPORT1;
                btnLPM2Initial.Flicker = GG.Equip.InitialLogic.IsRunning() && GG.Equip.InitialLogic.TargetPort == EmEfemPort.LOADPORT2;
                btnWaferReturn.Flicker = GG.Equip.WaferTransLogic.IsRunning();
                btnWaferReturn.Enabled = !GG.Equip.WaferTransLogic.IsRunning() && GG.Equip.EquipRunMode == EmEquipRunMode.Manual;

                btnAVIInitial.OnOff = GG.Equip.IsReadyToInputArm.IsSolOnOff;
                btnRobotInitial.OnOff = GG.Equip.Efem.Robot.IsInitDone;
                btnAlignerInitial.OnOff = GG.Equip.Efem.Aligner.IsInitDone;
                btnLPM1Initial.OnOff = GG.Equip.Efem.LoadPort1.IsInitDone;
                btnLPM2Initial.OnOff = GG.Equip.Efem.LoadPort2.IsInitDone;

                btnLoadPort1Open.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.OPEN_].IsStepRunning;
                btnLoadPort1Close.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.CLOSE].IsStepRunning;
                btnLoadPort2Open.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.OPEN_].IsStepRunning;
                btnLoadPort2Close.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.CLOSE].IsStepRunning;

                btnLoadPort1Open.OnOff = GG.Equip.Efem.LoadPort1.Status.IsDoorOpen;
                btnLoadPort1Close.OnOff = GG.Equip.Efem.LoadPort1.Status.IsDoorClose;
                btnLoadPort2Open.OnOff = GG.Equip.Efem.LoadPort2.Status.IsDoorOpen;
                btnLoadPort2Close.OnOff = GG.Equip.Efem.LoadPort2.Status.IsDoorClose;
                #endregion

                SetCurrentRecoveryStep();
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

        private void SetCurrentRecoveryStep()
        {
            lblRecoveryStep.Text = GG.Equip.WaferTransLogic.SeqStepNum.ToString();

            string summaryStr = string.Empty;
            switch (GG.Equip.WaferTransLogic.SeqStepNum)
            {
                case Struct.Step.EmWaferTransferSeqStep.S000_END:
                    summaryStr = GG.boChinaLanguage ? "复位完毕" : "복귀 완료";
                    break;
                case Struct.Step.EmWaferTransferSeqStep.S005_INITIAL_START:
                    summaryStr = "";
                    break;
                case Struct.Step.EmWaferTransferSeqStep.S007_ROBOT_INITIAL_WAIT:
                    summaryStr = GG.boChinaLanguage ? "(Robot) 初始化进行中" : "로봇 이니셜 진행 중";
                    break;
                case Struct.Step.EmWaferTransferSeqStep.S010_ALL_INITIAL_WAIT:
                    summaryStr = GG.boChinaLanguage ? "Stage, Aligner, LPM\n初始化进行中" : "스테이지, 얼라이너, 로드포트\n이니셜 진행 중";
                    break;
                case Struct.Step.EmWaferTransferSeqStep.S020_LPM_OPEN_WAIT:
                    summaryStr = GG.boChinaLanguage ? "(Load Port Door Open) 进行中" : "로드포트 도어 오픈 중";
                    break;
                case Struct.Step.EmWaferTransferSeqStep.S030_START:
                    summaryStr = GG.boChinaLanguage ? "开始" : "시작";
                    break;
                case Struct.Step.EmWaferTransferSeqStep.S040_PICK_START:
                    summaryStr = GG.boChinaLanguage ? "(Robot) PICK 开始" : "로봇 PICK 시작";
                    break;
                case Struct.Step.EmWaferTransferSeqStep.S050_PICK_END_WAIT:
                    summaryStr = GG.boChinaLanguage ? "(Robot) PICK 进行中" : "로봇 PICK 진행 중";
                    break;
                case Struct.Step.EmWaferTransferSeqStep.S060_DELAY:
                    summaryStr = GG.boChinaLanguage ? "PICK 以后 Delay" : "PICK 이후 딜레이";
                    break;
                case Struct.Step.EmWaferTransferSeqStep.S070_PLACE_START:
                    summaryStr = GG.boChinaLanguage ? "(Robot) Place 开始" : "로봇 PLACE 시작";
                    break;
                case Struct.Step.EmWaferTransferSeqStep.S080_PLACE_END_WAIT:
                    summaryStr = GG.boChinaLanguage ? "(Robot) Place 进行中" : "로봇 PLACE 진행 중";
                    break;
                case Struct.Step.EmWaferTransferSeqStep.S090_END:
                    summaryStr = GG.boChinaLanguage ? "完毕" : "완료";
                    break;
            }
            lblRecoverySummary.Text = summaryStr;
        }

        private void UpdateSignalLamp(ButtonDelay2[] buttons, bool[] isCmdLampOn, bool[] isAckLampOn)
        {
            if (buttons.Length != isCmdLampOn.Length || isCmdLampOn.Length != isAckLampOn.Length)
                new Exception("UpdateSignalLamp Func : 배열의 수가 다름");

            for (int iter = 0; iter < buttons.Length; ++iter)
            {
                buttons[iter].IsLeftLampOn = isCmdLampOn[iter];
                buttons[iter].IsRightLampOn = isAckLampOn[iter];
            }
        }
        private void btnAllVacuumOffStep_Click(object sender, EventArgs e)
        {
            _equip.Vacuum.StartOffStep();
        }

        private void cmbTrigger_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnInspXSelPosMove_Click(object sender, EventArgs e)
        {

        }

        private void btnThetaMoveLoading_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            float pos = 0;
            float spd = 0;
            float acc = 0;

            if (btnInspXMoveLoading == btn)
            {
                if (false
                    || ucrlPtpX.GetPos(out pos) == false
                    || ucrlPtpX.GetSpd(out spd) == false
                    || ucrlPtpX.GetAcc(out acc) == false)
                {
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "输入值发生问题!" : "입력값 이상");
                    return;
                }

                if (pos == _equip.StageX.Setting.LstServoPosiInfo[StageXServo.LoadingPos].Position && spd == _equip.StageX.Setting.LstServoPosiInfo[StageXServo.LoadingPos].Speed)
                    _equip.StageX.MoveLoadingEx(_equip);
                else
                    _equip.StageX.MovePosition(_equip, pos, spd, acc);
            }
            else if (btnInspYMoveLoading == btn)
            {
                if (false
                    || ucrlPtpY1.GetPos(out pos) == false
                    || ucrlPtpY1.GetSpd(out spd) == false
                    || ucrlPtpY1.GetAcc(out acc) == false)
                {
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "输入值发生问题!" : "입력값 이상");
                    return;
                }

                if (pos == _equip.StageY.Setting.LstServoPosiInfo[StageYServo.LoadingPos].Position)
                    _equip.StageY.MoveLoadingEx(_equip);
                else
                    _equip.StageY.MovePosition(_equip, pos, spd, acc);
            }
            else if (btnThetaMoveLoading == btn)
            {
                if (false
                    || ucrlPtpTheta.GetPos(out pos) == false
                    || ucrlPtpTheta.GetSpd(out spd) == false
                    || ucrlPtpTheta.GetAcc(out acc) == false)
                {
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? "输入值发生问题!" : "입력값 이상");
                    return;
                }

                if (pos == _equip.Theta.Setting.LstServoPosiInfo[ThetaServo.LoadingPos].Position)
                    _equip.Theta.MoveLoadingEx(_equip);
                else
                    _equip.Theta.MovePosition(_equip, pos, spd, acc);
            }
        }

        private void cmbInspXSelPos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            if (cmb.SelectedIndex < 0)
                return;

            if (cmb == cmbInspXSelPos)
            {
                ucrlPtpX.Pos = _equip.StageX.Setting.LstServoPosiInfo[cmb.SelectedIndex].Position;
                ucrlPtpX.Spd = _equip.StageX.Setting.LstServoPosiInfo[cmb.SelectedIndex].Speed;
                ucrlPtpX.Acc = _equip.StageX.Setting.LstServoPosiInfo[cmb.SelectedIndex].Accel;
            }
            else if (cmb == cmbInspYSelPos)
            {
                ucrlPtpY1.Pos = _equip.StageY.Setting.LstServoPosiInfo[cmb.SelectedIndex].Position;
                ucrlPtpY1.Spd = _equip.StageY.Setting.LstServoPosiInfo[cmb.SelectedIndex].Speed;
                ucrlPtpY1.Acc = _equip.StageY.Setting.LstServoPosiInfo[cmb.SelectedIndex].Accel;
            }
            else if (cmb == cmbThetaSelPos)
            {
                ucrlPtpTheta.Pos = _equip.Theta.Setting.LstServoPosiInfo[cmb.SelectedIndex].Position;
                ucrlPtpTheta.Spd = _equip.Theta.Setting.LstServoPosiInfo[cmb.SelectedIndex].Speed;
                ucrlPtpTheta.Acc = _equip.Theta.Setting.LstServoPosiInfo[cmb.SelectedIndex].Accel;
            }
        }
        private void btnLiftPin_MouseClick(object sender, MouseEventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;

            Dictionary<ButtonDelay2, CustomCylinder> centeringBtnSet = new Dictionary<ButtonDelay2, CustomCylinder>()
            {
                {btnOrgStdForward, _equip.StandCentering},
                {btnOrgStdBack, _equip.StandCentering},

                {btnLiftPinUp, _equip.LiftPin},
                {btnLiftPinDown, _equip.LiftPin},
            };

            if (int.Parse(btn.Tag.ToString()) == 1)
                centeringBtnSet[btn].Forward(_equip);
            else
                centeringBtnSet[btn].Backward(_equip);
        }

        #region EFEM Protocol
        private void UpdateEFEM()
        {
            btnRobotInit.OnOff      /**/ = _equip.Efem.Robot.IsInitDone;
            btnRobotReset.Flicker   /**/ = GG.Equip.Efem.Proxy.HS[EmEfemPort.ROBOT].HasErrorCode();

            btnLPM1Init.OnOff       /**/ = _equip.Efem.LoadPort1.IsInitDone;
            btnLPM1Reset.Flicker    /**/ = GG.Equip.Efem.Proxy.HS[EmEfemPort.LOADPORT1].HasErrorCode();
            btnLPM1Open.OnOff       /**/ = _equip.Efem.LoadPort1.Status.IsDoorOpen;
            btnLPM1Close.OnOff      /**/ = _equip.Efem.LoadPort1.Status.IsDoorClose;

            btnLPM2Init.OnOff       /**/ = _equip.Efem.LoadPort2.IsInitDone;
            btnLPM2Reset.Flicker    /**/ = GG.Equip.Efem.Proxy.HS[EmEfemPort.LOADPORT2].HasErrorCode();
            btnLPM2Open.OnOff       /**/ = _equip.Efem.LoadPort2.Status.IsDoorOpen;
            btnLPM2Close.OnOff      /**/ = _equip.Efem.LoadPort2.Status.IsDoorClose;

            btnAlignerInit.OnOff    /**/ = _equip.Efem.Aligner.Status.IsStatus != EmEfemAlignerStatus.ERROR;
            btnAlignerReset.Flicker /**/ = GG.Equip.Efem.Proxy.HS[EmEfemPort.ALIGNER].HasErrorCode();
            btnAlignReady.OnOff = GG.Equip.Efem.Aligner.Status.IsStatus == EmEfemAlignerStatus.READY;

            btnRobotInit.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.ROBOT].LstCmd[EmEfemCommand.INIT_].IsStepRunning;
            btnLPM1Init.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.INIT_].IsStepRunning;
            btnLPM1Open.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.OPEN_].IsStepRunning;
            btnLPM1Close.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.LOADPORT1].LstCmd[EmEfemCommand.CLOSE].IsStepRunning;
            btnLPM2Init.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.INIT_].IsStepRunning;
            btnLPM2Open.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.OPEN_].IsStepRunning;
            btnLPM2Close.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.LOADPORT2].LstCmd[EmEfemCommand.CLOSE].IsStepRunning;
            btnAlignerInit.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.INIT_].IsStepRunning;
            btnAlignReady.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.PARDY].IsStepRunning;
            btnAlign.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.PARDY].IsStepRunning;
            btnAlignerRotate.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.PATRR].IsStepRunning;
            btnAlignerSendReady.Flicker = GG.Equip.Efem.Proxy.HS[EmEfemPort.ALIGNER].LstCmd[EmEfemCommand.PASRD].IsStepRunning;

            btnETCReset.OnOff = GG.Equip.Efem.Status.SafetyPlcState == EmEfemSafetyPLCState.NORMAL;
            btnETCReset.Flicker = GG.Equip.Efem.Status.SafetyPlcState != EmEfemSafetyPLCState.NORMAL;

            btnEFEMAuto.OnOff = GG.Equip.Efem.Status.IsAutoMode;
            btnEFEMManual.OnOff = !GG.Equip.Efem.Status.IsAutoMode;
        }

        //Robot
        private void btnRobotInit_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.ROBOT, EmEfemCommand.INIT_);
        }
        private void btnRobotReset_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.ROBOT, EmEfemCommand.RESET);
        }
        private void btnRobotPause_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.ROBOT, EmEfemCommand.PAUSE);
        }
        private void btnRobotResume_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.ROBOT, EmEfemCommand.RESUM);
        }
        private void btnRobotStop_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.ROBOT, EmEfemCommand.STOP_);
        }
        //LPM1
        private void btnLPM1Init_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.LOADPORT1, EmEfemCommand.INIT_);
        }
        private void btnLPM1Reset_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.LOADPORT1, EmEfemCommand.RESET);
        }
        private void btnLPM1Stop_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.LOADPORT1, EmEfemCommand.STOP_);
        }
        private void btnLPM1Open_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.LOADPORT1, EmEfemCommand.OPEN_);
        }
        private void btnLPM1Close_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.LOADPORT1, EmEfemCommand.CLOSE);
        }
        private void btnLPM1Mapping_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.LOADPORT1, EmEfemCommand.MAPP_);
        }
        //LPM2
        private void btnLPM2Init_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.LOADPORT2, EmEfemCommand.INIT_);
        }
        private void btnLPM2Reset_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.LOADPORT2, EmEfemCommand.RESET);
        }
        private void btnLPM2Stop_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.LOADPORT2, EmEfemCommand.STOP_);
        }
        private void btnLPM2Open_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.LOADPORT2, EmEfemCommand.OPEN_);
        }
        private void btnLPM2Close_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.LOADPORT2, EmEfemCommand.CLOSE);
        }
        private void btnLPM2Mapping_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.LOADPORT2, EmEfemCommand.MAPP_);
        }
        //Aliner
        private void btnAlignerInit_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.ALIGNER, EmEfemCommand.INIT_);
        }
        private void btnAlignerReset_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.ALIGNER, EmEfemCommand.RESET);
        }
        private void btnAlignReady_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.ALIGNER, EmEfemCommand.PARDY);
        }
        private void btnAlignerSendReady_Click(object sender, EventArgs e)
        {
            _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.ALIGNER, EmEfemCommand.PASRD);
        }
        //ETC
        private void btnETCReset_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btnETCReset)
                _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.ETC, EmEfemCommand.RESET);
            else if (btn == btnEFEMAuto)
                _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.ETC, EmEfemCommand.CHMDA);
            else if (btn == btnEFEMManual)
                _equip.Efem.Proxy.StartCommand(_equip, EmEfemPort.ETC, EmEfemCommand.CHMDM);
        }
        //ALIGN
        private void btnAlign_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string recipe = txtAlignerRecipe.Text;
            int degree;

            if (int.TryParse(tbDegree.Text, out degree) == false
                || degree < -359
                || degree > 359)
            {
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "PreAligner Rotation 请输入-359~359 " : "PreAligner Rotation -359~359 입력");
                return;
            }

            if (btn == btnAlign)
            {
                //if (recipe.Length != 3)
                //{
                //    InterLockMgr.AddInterLock("Recipe 명은 3자리입니다");
                //    return;
                //}

                _equip.Efem.Proxy.StartAlign(_equip, degree, chkOCRForward.Checked, recipe);
            }
            else if (btn == btnAlignerRotate)
            {
                _equip.Efem.Proxy.StartAlignerRotation(_equip, degree, chkOCRForward.Checked);
            }
        }
        #endregion

        private void btnMutingOn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btnMutingOn)
            {
                _equip.Efem.LPMLightCurtain.StartMuting();
            }
            else if (btn == btnMutingOff)
            {
                _equip.Efem.LPMLightCurtain.StopMuting();
            }
            else if (btn == btnLightCurtainReset)
            {
                _equip.Efem.LPMLightCurtain.Reset();
            }
        }
        private void btnPin_Click(object sender, EventArgs e)
        {
            if (btnPin.FlatStyle == FlatStyle.Flat)
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

        #region BASE OPERATION
        private void btnWaferReturn_Click(object sender, EventArgs e)
        {
            Queue<WaferLogicParm> queueLogic = new Queue<WaferLogicParm>();
            List<string> errPort = new List<string>();

            if (GG.Equip.WaferTransLogic.IsRunning())
            {
                cbUpper.Checked = cbLower.Checked = cbAVI.Checked = cbAligner.Checked = false;
                return;
            }

            try
            {
                for (int i = 0; i < 4; i++)
                {
                    WaferLogicParm param = new WaferLogicParm();
                    WaferInfoKey key;

                    if (i == 0 && cbUpper.Checked == true)
                    {
                        key = GG.Equip.Efem.Robot.UpperWaferKey;
                        if (IsUnNormalWaferInfo(key) || !GG.Equip.Efem.Robot.Status.IsUpperArmVacOn)
                        {
                            errPort.Add("Upper Robot");
                            continue;
                        }
                        else
                        {
                            param.FromPort = EmEfemDBPort.UPPERROBOT;
                            param.ToPort = TransferDataMgr.GetCst(key).LoadPortNo == 1 ? EmEfemDBPort.LOADPORT1 : EmEfemDBPort.LOADPORT2;
                            param.ToSlot = key.SlotNo;

                            if (CheckSameLoadPortCstID(param.ToPort, key.CstID) == false)
                            {
                                errPort.Add("Upper Arm");
                                continue;
                            }
                            queueLogic.Enqueue(param);
                        }
                    }
                    else if (i == 1 && cbLower.Checked == true)
                    {
                        key = GG.Equip.Efem.Robot.LowerWaferKey;
                        if (IsUnNormalWaferInfo(key) || !GG.Equip.Efem.Robot.Status.IsLowerArmVacOn)
                        {
                            errPort.Add("Lower Robot");
                            continue;
                        }
                        else
                        {
                            param.FromPort = EmEfemDBPort.LOWERROBOT;
                            param.ToPort = TransferDataMgr.GetCst(key).LoadPortNo == 1 ? EmEfemDBPort.LOADPORT1 : EmEfemDBPort.LOADPORT2;
                            param.ToSlot = key.SlotNo;

                            if (CheckSameLoadPortCstID(param.ToPort, key.CstID) == false)
                            {
                                errPort.Add("Lower Arm");
                                continue;
                            }
                            queueLogic.Enqueue(param);
                        }
                    }
                    else if (i == 2 && cbAVI.Checked == true)
                    {
                        if (GG.Equip.Efem.Robot.Status.IsUpperArmVacOn && cbUpper.Checked == false)
                        {
                            InterLockMgr.AddInterLock(GG.boChinaLanguage ? "Upper Arm上存在 Wafer时,从 AVI Port无法单独复位." : "Upper Arm에 웨이퍼가 존재 할 때 AVI포트로부터 단독 복귀가 불가능 합니다.");
                            return;
                        }
                        //if(GG.Equip.IsWaferUnloadingState == false)
                        //{
                        //    InterLockMgr.AddInterLock("AVI의 Wafer가 배출 위치가 아닐 때 복귀가 불가능 합니다.");
                        //    return;
                        //}
                        key = GG.Equip.TransferUnit.LowerWaferKey;
                        if (IsUnNormalWaferInfo(key) || GG.Equip.IsWaferDetect == EmGlassDetect.NOT)
                        {
                            errPort.Add("AVI");
                            continue;
                        }
                        else
                        {
                            param.FromPort = EmEfemDBPort.EQUIPMENT;
                            param.ToPort = TransferDataMgr.GetCst(key).LoadPortNo == 1 ? EmEfemDBPort.LOADPORT1 : EmEfemDBPort.LOADPORT2;
                            param.ToSlot = key.SlotNo;

                            if (CheckSameLoadPortCstID(param.ToPort, key.CstID) == false)
                            {
                                errPort.Add("AVI");
                                continue;
                            }
                            queueLogic.Enqueue(param);
                        }
                    }
                    else if (i == 3 && cbAligner.Checked == true)
                    {
                        //if (GG.Equip.Efem.Robot.Status.IsUpperArmVacOn && cbUpper.Checked == false)
                        //{
                        //    InterLockMgr.AddInterLock("Upper Arm에 웨이퍼가 존재 할 때 Aligner포트로부터 복귀가 불가능 합니다.");
                        //    return;
                        //}
                        key = GG.Equip.Efem.Aligner.LowerWaferKey;
                        if (IsUnNormalWaferInfo(key) || !GG.Equip.Efem.Aligner.Status.IsWaferExist)
                        {
                            errPort.Add("Aligner");
                            continue;
                        }
                        else
                        {
                            param.FromPort = EmEfemDBPort.ALIGNER;
                            param.ToPort = TransferDataMgr.GetCst(key).LoadPortNo == 1 ? EmEfemDBPort.LOADPORT1 : EmEfemDBPort.LOADPORT2;
                            param.ToSlot = key.SlotNo;

                            if (CheckSameLoadPortCstID(param.ToPort, key.CstID) == false)
                            {
                                errPort.Add("Aligner");
                                continue;
                            }
                            queueLogic.Enqueue(param);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                if (errPort.Count > 0)
                {
                    string temp = string.Empty;
                    for (int i = 0; i < errPort.Count; i++)
                    {
                        temp += string.Format("[{0}] ", errPort[i]);
                    }
                    InterLockMgr.AddInterLock(GG.boChinaLanguage ? string.Format("{0}\nWafer Key Info不准确或 和LoadPort Wafer Key不同无法复位\n(手动移送 Wafer或确认Wafer Key)", temp) : string.Format("{0}\nWafer Key 정보가 정확하지 않거나 LoadPort Wafer Key와 달라 복귀가 불가능합니다\n(수동으로 웨이퍼를 이송하거나 Wafer Key를 확인 해 주세요)", temp));
                }
                else
                {
                    WaferLogicParm[] clone = new WaferLogicParm[queueLogic.Count];
                    queueLogic.CopyTo(clone, 0);
                    foreach (var item in clone)
                    {
                        Logger.Log.AppendLine(LogLevel.Info, "웨이퍼 자동 복귀 시작 출발지[{0}, {1}] -> 도착지[{2}, {3}]", item.FromPort, item.FromSlot, item.ToPort, item.ToSlot);
                    }

                    GG.Equip.WaferTransLogic.StartWaferTransfer(queueLogic);
                }
            }
            catch (Exception ex)
            {
                InterLockMgr.AddInterLock("Wafer Recovery Button Exception Error");
                Logger.ExceptionLog.AppendLine(LogLevel.Error, string.Format("Wafer Recovery Exception : {0}", ex.Message));
            }
        }

        private bool IsUnNormalWaferInfo(WaferInfoKey key)
        {
            if (key == null || key.CstID == null || key.CstID == "" || key.SlotNo == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckSameLoadPortCstID(EmEfemDBPort toPort, string cstID)
        {
            if (toPort == EmEfemDBPort.LOADPORT1)
            {
                if (Equals(GG.Equip.Efem.LoadPort1.CstKey.ID, cstID))
                {
                    return true;
                }
            }
            else if (toPort == EmEfemDBPort.LOADPORT2)
            {
                if (Equals(GG.Equip.Efem.LoadPort2.CstKey.ID, cstID))
                {
                    return true;
                }
            }
            return false;
        }

        private void btnAVIInitial_Click(object sender, EventArgs e)
        {
            GG.Equip.InitialLogic.StartInitial(EmEfemPort.EQUIPMENT);
        }

        private void btnAlignerInitial_Click(object sender, EventArgs e)
        {
            GG.Equip.InitialLogic.StartInitial(EmEfemPort.ALIGNER);
        }

        private void btnLPM1Initial_Click(object sender, EventArgs e)
        {
            GG.Equip.InitialLogic.StartInitial(EmEfemPort.LOADPORT1);
        }

        private void btnLPM2Initial_Click(object sender, EventArgs e)
        {
            GG.Equip.InitialLogic.StartInitial(EmEfemPort.LOADPORT2);
        }

        private void btnRobotInitial_Click(object sender, EventArgs e)
        {
            GG.Equip.InitialLogic.StartInitial(EmEfemPort.ROBOT);
        }

        private void btnLoadPort2Open_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.LOADPORT2, EmEfemCommand.OPEN_);
        }

        private void btnLoadPort2Mapp_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.LOADPORT2, EmEfemCommand.MAPP_);
        }

        private void btnLoadPort2Close_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.LOADPORT2, EmEfemCommand.CLOSE);
        }

        private void btnLoadPort1Open_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.LOADPORT1, EmEfemCommand.OPEN_);
        }

        private void btnLoadPort1Mapp_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.LOADPORT1, EmEfemCommand.MAPP_);
        }

        private void btnLoadPort1Close_Click(object sender, EventArgs e)
        {
            GG.Equip.Efem.Proxy.StartCommand(GG.Equip, EmEfemPort.LOADPORT1, EmEfemCommand.CLOSE);
        }
        #endregion

        private void btnWaferRecoveryStop_Click(object sender, EventArgs e)
        {
            GG.Equip.WaferTransLogic.Stop();
        }

        private void btnAirKnifeOn_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;

            if (GG.Equip.CtrlSetting.AirKnifeUse == false)
            {
                InterLockMgr.AddInterLock("Air Knife", GG.boChinaLanguage ? "现在是Air Knife 未使用 Mode\nOperationOpetion里需转换 Use ." : "현재 Air Knife 미사용 모드입니다\nOperationOpetion에서 Use 전환 필요");
                return;
            }

            if (btn == btnAirKnifeOn)
            {
                GG.Equip.AirKnife.OnOff(GG.Equip, true);
            }
            else if (btn == btnAirKnifeOff)
            {
                GG.Equip.AirKnife.OnOff(GG.Equip, false);
            }
        }
    }
}
