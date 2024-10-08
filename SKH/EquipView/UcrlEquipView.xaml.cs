using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace EquipView
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UcrlEquipView : System.Windows.Controls.UserControl
    {
        public enum ImgChangeType
        {
            Move,
            Size
        }

        public class ImgMoveInfo
        {
            public double Current { get; set; }
            public double Start { get; set; }
            public double End { get; set; }
            public double Stroke { get; set; }
            public ImgMoveInfo()
            {
                Current = Start = End = Stroke = 0;
            }
        }

        private ImgMoveInfo _stageX = new ImgMoveInfo() { Current = 0, Start = 0, End = 100, Stroke = 510 };
        private ImgMoveInfo _stageY = new ImgMoveInfo() { Current = 0, Start = 0, End = 100, Stroke = 620 };
        private ImgMoveInfo _theta = new ImgMoveInfo() { Current = 0, Start = 0, End = 100, Stroke = 100 };

        private ImgMoveInfo _liftPin1 = new ImgMoveInfo() { Current = 0, Start = 0, End = 50, Stroke = 50 };
        private ImgMoveInfo _liftPin2 = new ImgMoveInfo() { Current = 0, Start = 0, End = 50, Stroke = 50 };

        private ImgMoveInfo _leftCentering1 = new ImgMoveInfo { Current = 0, Start = 0, End = 100, Stroke = 50 };        
        private ImgMoveInfo _rightCentering1 = new ImgMoveInfo { Current = 0, Start = 0, End = 100, Stroke = 50 };

        private bool _WaferExist = false;
        public bool WaferExist
        {
            get { return _WaferExist; }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
            set
            {
                _WaferExist = value;
                Wafer.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            }
        }
        public void UpdateWaferPos()
        {
            double[] liftpinPos = new double[]
            {
                this.LiftPin1,
                this.LiftPin2,
            };

            int pos = (int)liftpinPos.Max();

            if (pos == 0)
                Wafer.Margin = new Thickness(0, 0, 0, 0);
            else if (pos == (int)_liftPin1.Stroke)
                Wafer.Margin = new Thickness(0, -50, 0, 0);
            else
                Wafer.Margin = new Thickness(0, -25, 0, 0);
        }

        private bool _robotArmCheck;
        public bool RobotArmCheck
        {
            get { return _robotArmCheck; }
            set
            {
                _robotArmCheck = value;
                ImgArm.Visibility = _robotArmCheck == true ? Visibility.Visible : Visibility.Hidden;
            }

        }
        public bool EMS1 { set { lblEMS1.Background = value ? Brushes.Red : Brushes.Black; } }
        public bool EMS2 { set { lblEMS2.Background = value ? Brushes.Red : Brushes.Black; } }
        public bool EMS3 { set { lblEMS3.Background = value ? Brushes.Red : Brushes.Black; } }

        public bool TDOOR1 { set { lblTopDoor1.Background = value ? Brushes.Red : Brushes.Green; } }
        public bool TDOOR2 { set { lblTopDoor2.Background = value ? Brushes.Red : Brushes.Green; } }
        public bool TDOOR3 { set { lblTopDoor3.Background = value ? Brushes.Red : Brushes.Green; } }
        public bool TDOOR4 { set { lblTopDoor4.Background = value ? Brushes.Red : Brushes.Green; } }

        private static double _opacity = 0.6;

        public bool StageVacuum1 { set { ImgVacuum1.Background = value ? Brushes.Blue : Brushes.Red; ImgVacuum1.Opacity = _opacity; } }
        public bool StageVacuum2 { set { ImgVacuum2.Background = value ? Brushes.Blue : Brushes.Red; ImgVacuum2.Opacity = _opacity; } }
        public bool WaferPinDetect1 { set { ImgPinDetect1.Background = value ? Brushes.Blue : Brushes.Red; ImgPinDetect1.Opacity = _opacity; } }
        public bool WaferPinDetect2 { set { ImgPinDetect2.Background = value ? Brushes.Blue : Brushes.Red; ImgPinDetect2.Opacity = _opacity; } }
        public bool WaferStageDetect1 { set { ImgStageDetect1.Background = value ? Brushes.Blue : Brushes.Red; ImgStageDetect1.Opacity = _opacity; } }

        public bool ShowInterlockOffMsg
        {
            set
            {
                //txtbInterlockMsg.Margin = new Thickness(0, 0, txtbInterlockMsg.Width, txtbInterlockMsg.Height);
                txtbInterlockMsg.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            }
        }
        public bool ShowReviewRunningMsg
        {
            set
            {
                //txtbInterlockMsg.Margin = new Thickness(0, 0, txtbInterlockMsg.Width, txtbInterlockMsg.Height);
                txtbReviewRunningMsg.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public bool LiftPinBackColor
        {
            set
            {
                ImgLiftPin1.Fill = value ? Brushes.Blue : Brushes.Red;
                ImgLiftPin1.Stroke = value ? Brushes.Blue : Brushes.Red;
            }
        }

        public bool IsEFEMEMO { set { lblEFEMEMO.Visibility = value ? Visibility.Visible : Visibility.Hidden; } }
        public bool IsRobotArmDetected { set { lblRobotArm.Visibility = value ? Visibility.Visible : Visibility.Hidden; } }
        public bool IsLightCurtainDetected {  set { lblLightCurtain.Visibility = value ? Visibility.Visible : Visibility.Hidden; } }
        public bool IsLightCurtainMute { set { lblLightCurtainMute.Visibility = value ? Visibility.Visible : Visibility.Hidden; } }

        public Action LPM1Pushed;
        public Action LPM2Pushed;
        public Action PreAlignerSettingBtnPushed;

        public UcrlEquipView()
        {
            InitializeComponent();

            //////////////////////////////////////////////////////////////////////////
            // Start-End만 설정하면 아래 Motor Move는 변경 X
            // Start, End에 따라 이동 방향 결정
            //////////////////////////////////////////////////////////////////////////

            _stageX.Start = gridWaferStageX.Margin.Left;
            _stageX.End = gridWaferStageX.Margin.Left - 300;

            _stageY.Start = girdWaferStageY.Margin.Top;
            _stageY.End = girdWaferStageY.Margin.Top + 300;

            ////좌
            _leftCentering1.Start = ImgStdCenter1.Margin.Left;
            _leftCentering1.End = ImgStdCenter1.Margin.Left + 16;
            ////하
            //_stdCentering2.Start = ImgStdCenter1.Margin.Bottom;
            //_stdCentering2.End = ImgStdCenter1.Margin.Bottom+35;
            ////우
            _rightCentering1.Start = ImgAssiCenter1.Margin.Right;
            _rightCentering1.End = ImgAssiCenter1.Margin.Right + 16;
            ////상
            //_assiCentering2.Start = ImgAssiCenter2.Margin.Top;
            //_assiCentering2.End = ImgAssiCenter2.Margin.Top+16;


            _liftPin1.Start = ImgLiftPin1.Margin.Top + 30; 
            _liftPin1.End   = ImgLiftPin1.Margin.Top;
            _liftPin2.Start = ImgLiftPin2.Margin.Top + 30;
            _liftPin2.End   = ImgLiftPin2.Margin.Top;

            InitLpmSlot();
            btnPreAlignerSetting.Visibility = Visibility.Hidden;
            PnlAlignerRobotInterlock.Visibility = Visibility.Hidden;
        }
        #region Motor Move
        public double StageX
        {
            get
            {
                return _stageX.Current;
            }
            set
            {
                if (double.IsNaN(value)) return;

                _stageX.Current = value;
                double scalePosi = _stageX.Start + (_stageX.End - _stageX.Start) * (_stageX.Current / _stageX.Stroke);

                gridWaferStageX.Margin = new Thickness()
                {
                    Left = scalePosi,
                    Right = 0,
                    Top = 116,
                    Bottom = 0,
                };
            }
        }
        public double StageY
        {
            get
            {
                return _stageY.Current;
            }
            set
            {
                if (double.IsNaN(value)) return;

                _stageY.Current = value;

                double scalePosi = _stageY.Start + (_stageY.End - _stageY.Start) * (_stageY.Current / _stageY.Stroke);
                girdWaferStageY.Margin = new Thickness()
                {
                    Left = girdWaferStageY.Margin.Left,
                    Right = girdWaferStageY.Margin.Right,
                    Top = scalePosi,
                    Bottom = girdWaferStageY.Margin.Bottom,
                };
            }
        }
        public double Theta
        {
            get { return _theta.Current; }
            set
            {
                if (double.IsNaN(value)) return;
                lblTheta.Content = string.Format("{000:F3} º", value);
                //lblThetaPos.Content = string.Format("{0:F3} º", value);
                RotateWafer((int)(value + 0.01f));
            }
        }
        public void RotateWafer(int rotation)
        {
            RotateTransform rotateTransform = new RotateTransform(rotation);
            Wafer.RenderTransform = rotateTransform;
            WaferStage.RenderTransform = rotateTransform;
        }
        #endregion
        #region Centering Move

        public double LeftCentering1
        {
            get
            {
                return _leftCentering1.Current;
            }
            set
            {
                _leftCentering1.Current = value;
                ImgStdCenter1.Background = value == _leftCentering1.Stroke ? Brushes.Blue : Brushes.Red;
                double scalePosiTop = _leftCentering1.Start + (_leftCentering1.End - _leftCentering1.Start) * (_leftCentering1.Current / _leftCentering1.Stroke);
                ImgStdCenter1.Margin = new Thickness()
                {
                    Left = scalePosiTop,
                    Right = ImgStdCenter1.Margin.Right,
                    Top = ImgStdCenter1.Margin.Top,
                    Bottom = ImgStdCenter1.Margin.Bottom,
                };
            }
        }
        //public double StdCentering2
        //{
        //    get
        //    {
        //        return _stdCentering2.Current;
        //    }
        //    set
        //    {
        //        _stdCentering2.Current = value;
        //        ImgStdCenter2.Background = value == _stdCentering2.Stroke ? Brushes.Blue : Brushes.Red;
        //        double scalePosiTop = _stdCentering2.Start + (_stdCentering2.End - _stdCentering2.Start) * (_stdCentering2.Current / _stdCentering2.Stroke);
        //        ImgStdCenter2.Margin = new Thickness()
        //        {
        //            Left = ImgStdCenter2.Margin.Left,
        //            Right = ImgStdCenter2.Margin.Right,
        //            Top = ImgStdCenter2.Margin.Top,
        //            Bottom = scalePosiTop,
        //        };
        //    }
        //}
        public double RightCentering1
        {
            get
            {
                return _rightCentering1.Current;
            }
            set
            {
                _rightCentering1.Current = value;
                ImgAssiCenter1.Background = value == _rightCentering1.Stroke ? Brushes.Blue : Brushes.Red;
                double scalePosiTop = _rightCentering1.Start + (_rightCentering1.End - _rightCentering1.Start) * (_rightCentering1.Current / _rightCentering1.Stroke);
                ImgAssiCenter1.Margin = new Thickness()
                {
                    Left = ImgAssiCenter1.Margin.Left,
                    Right = scalePosiTop,
                    Top = ImgAssiCenter1.Margin.Top,
                    Bottom = ImgAssiCenter1.Margin.Bottom,
                };
            }
        }
        //public double AssiCentering2
        //{
        //    get
        //    {
        //        return _assiCentering2.Current;
        //    }
        //    set
        //    {
        //        _assiCentering2.Current = value;
        //        ImgAssiCenter2.Background = value == _assiCentering2.Stroke ? Brushes.Blue : Brushes.Red;
        //        double scalePosiTop = _assiCentering2.Start + (_assiCentering2.End - _assiCentering2.Start) * (_assiCentering2.Current / _assiCentering2.Stroke);
        //        ImgAssiCenter2.Margin = new Thickness()
        //        {
        //            Left = ImgAssiCenter2.Margin.Left,
        //            Right = ImgAssiCenter2.Margin.Right,
        //            Top = scalePosiTop,
        //            Bottom = ImgAssiCenter2.Margin.Bottom,
        //        };
        //    }
        //}

        #endregion
        #region Lift Pin Move
        public double LiftPin1
        {
            get
            {
                return _liftPin1.Current;
            }
            set
            {
                CalculatePosition(ImgLiftPin1, _liftPin1, value, Orientation.Vertical, ImgChangeType.Size);
            }
        }

        public void ViewPreAlignerControls()
        {
            btnPreAlignerSetting.Visibility = Visibility.Visible;
            PnlAlignerRobotInterlock.Visibility = Visibility.Visible;
        }

        public double LiftPin2
        {
            get
            {
                return _liftPin2.Current;
            }
            set
            {
                CalculatePosition(ImgLiftPin2, _liftPin2, value, Orientation.Vertical, ImgChangeType.Size);
            }
        }
        #endregion
        
        private void CalculatePosition(Grid moveObj, ImgMoveInfo moveInfo, double value, Orientation moveAxis)
        {
            CalculatePosition((object)moveObj, moveInfo, value, moveAxis);
        }
        private void CalculatePosition(Image moveObj, ImgMoveInfo moveInfo, double value, Orientation moveAxis)
        {
            CalculatePosition((object)moveObj, moveInfo, value, moveAxis);
        }
        private void CalculatePosition(object moveObj, ImgMoveInfo moveInfo, double value, Orientation moveAxis, ImgChangeType changeType = ImgChangeType.Move)
        {
            if (double.IsNaN(value)) return;

            moveInfo.Current = value;            

            switch (changeType)
            {
                case ImgChangeType.Move:
                    double scalePosi = moveInfo.Start + (moveInfo.End - moveInfo.Start) * (moveInfo.Current / moveInfo.Stroke);
                    (moveObj as FrameworkElement).Margin = new Thickness()
                    {
                        Left = moveAxis == Orientation.Horizontal ? scalePosi : (moveObj as FrameworkElement).Margin.Left,
                        Right = 0,
                        Top = moveAxis == Orientation.Vertical ? scalePosi : (moveObj as FrameworkElement).Margin.Top,
                        Bottom = 0,
                    };
                    break;
                case ImgChangeType.Size:                    
                    double maxSize = Math.Abs(moveInfo.End - moveInfo.Start);
                    double size = Math.Abs((moveInfo.End - moveInfo.Start) * (moveInfo.Current / moveInfo.Stroke));                    
                    if (size == 0)
                        size = maxSize / 2 < 5 ? 1 : 5;
                    switch (moveAxis)
                    {
                        case Orientation.Horizontal:
                            bool isLeftDir = moveInfo.End - moveInfo.Start < 0;
                            if (isLeftDir)
                            {
                                (moveObj as FrameworkElement).Width = size;
                                (moveObj as FrameworkElement).Margin = new Thickness()
                                {
                                    Left = moveInfo.End + maxSize - size,
                                    Right = 0,
                                    Top = (moveObj as FrameworkElement).Margin.Top,
                                    Bottom = 0,
                                };
                            }
                            else
                            {
                                (moveObj as FrameworkElement).Width = size;
                            }                            
                            break;
                        case Orientation.Vertical:
                            bool isTopDir = moveInfo.End - moveInfo.Start < 0;
                            if (isTopDir)
                            {
                                (moveObj as FrameworkElement).Height = size;
                                (moveObj as FrameworkElement).Margin = new Thickness()
                                {
                                    Left = (moveObj as FrameworkElement).Margin.Left,
                                    Right = 0,
                                    Top = moveInfo.End + maxSize - size,
                                    Bottom = 0,
                                };
                            }
                            else
                            {
                                (moveObj as FrameworkElement).Height = size;
                            }
                            break;
                    }
                    break;
            }
        }
        private void CalculatePosition(Border moveObj, ImgMoveInfo moveInfo, double value, Orientation moveAxis)
        {
            moveInfo.Current = value;
            moveObj.Background = value == moveInfo.Stroke ? Brushes.Blue : Brushes.Red;
            double scalePosi = moveInfo.Start + (moveInfo.End - moveInfo.Start) * (moveInfo.Current / moveInfo.Stroke);
            moveObj.Margin = new Thickness()
            {
                Left = moveAxis == Orientation.Horizontal ? scalePosi : moveObj.Margin.Left,
                Right = 0,
                Top = moveAxis == Orientation.Vertical ? scalePosi : moveObj.Margin.Top,
                Bottom = 0,
            };
        }
        private void ShowNames(Visibility isShowName)
        {
            lblVacuum1.Visibility = isShowName;

            lblPinDetect1.Visibility = isShowName;
            lblPinDetect2.Visibility = isShowName;
            lblStageDetect1.Visibility = isShowName;
        }

        FrmHelp _frmHelp = new FrmHelp();
        private void btnShowHelp_Click(object sender, RoutedEventArgs e)
        {
            _frmHelp.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            _frmHelp.Show();
        }

        #region EFEM UI        
        public bool IsXLdPos { set { sensXLd.Background = value ? Brushes.Red : Brushes.White; } }
        public bool IsYLdPos { set { sensYLd.Background = value ? Brushes.Red : Brushes.White; } }
        public bool IsTLdPos { set { sensTLd.Background = value ? Brushes.Red : Brushes.White; } }
        public bool IsPIOReady { set { sensAVIReady.Background = value ? Brushes.Red : Brushes.White; } }
        public bool IsAlignerPIOReady {  set { AlignRecvSendAble.Background = value ? Brushes.Red : Brushes.White; } }
        public bool IsRobotInput { set { sensRobotInput.Background = value ? Brushes.Red : Brushes.White; } }

        public bool IsLPM1Exist { set { ImgLpm1Exist.Visibility = value ? Visibility.Visible : Visibility.Hidden; } }
        public bool IsLPM2Exist { set { ImgLpm2Exist.Visibility = value ? Visibility.Visible : Visibility.Hidden; } }
        public string LPM1Notify { set { txtLPM1Notify.Text = value; } }
        public string LPM2Notify { set { txtLPM2Notify.Text = value; } }
        /// <summary>
        /// 0 : x, 1 : Green, 2 : Orange
        /// </summary>
        public int IsTargetLPM1
        {
            set
            {
                switch (value)
                {
                    case 0: lblLpm1Target.Content = ""; lblLpm1Target.Background = Brushes.Transparent; break;
                    case 1: lblLpm1Target.Content = "진행"; lblLpm1Target.Background = Brushes.LawnGreen; break;
                    case 2: lblLpm1Target.Content = "예약"; lblLpm1Target.Background = Brushes.Orange; break;
                    default: lblLpm1Target.Content = ""; lblLpm1Target.Background = Brushes.Transparent; break;
                }
            }
        }
        /// <summary>
        /// 0 : x, 1 : Green, 2 : Orange
        /// </summary>
        public int IsTargetLPM2
        {
            set
            {
                switch (value)
                {
                    case 0: lblLpm2Target.Content = ""; lblLpm2Target.Background = Brushes.Transparent; break;
                    case 1: lblLpm2Target.Content = "진행"; lblLpm2Target.Background = Brushes.LawnGreen; break;
                    case 2: lblLpm2Target.Content = "예약"; lblLpm2Target.Background = Brushes.Orange; break;
                    default: lblLpm2Target.Content = ""; lblLpm2Target.Background = Brushes.Transparent; break;
                }
            }
        }
        private bool _isWaferExistInAligner;
        public bool IsWaferExistInAligner
        {
            get
            {
                return _isWaferExistInAligner;
            }
            set
            {
                if(value == true)
                {
                    ImgAlignerWafer.Visibility = Visibility.Visible;
                    ImgAlignerWaferRingFrame.Visibility = Visibility.Visible;
                    _isWaferExistInAligner = true;
                }
                else
                {
                    ImgAlignerWafer.Visibility = Visibility.Hidden;
                    ImgAlignerWaferRingFrame.Visibility = Visibility.Hidden;
                    _isWaferExistInAligner = false;
                }

            }
        }
        public bool IsLPM1OHTMode
        {
            set
            {
                btnLPM1Load.Visibility = value == false ? Visibility.Visible : Visibility.Hidden;
            }
        }
        public bool IsLPM2OHTMode
        {
            set
            {
                btnLPM2Load.Visibility = value == false ? Visibility.Visible : Visibility.Hidden;
            }
        }
        public void SetLoadButtonLamp(int idx, bool isOn)
        {
            switch (idx)
            {
                case 0: btnLPM1Load.Background = isOn ? Brushes.Red : Brushes.Transparent; break;
                case 1: btnLPM2Load.Background = isOn ? Brushes.Red : Brushes.Transparent; break;

            }
        }

        private Image[] Lpm1Slots;
        private Image[] Lpm2Slots;
        private Image[,] Lpm1CompleteCheck;
        private Image[,] Lpm2CompleteCheck;
        private void InitLpmSlot()
        {
            Lpm1Slots = new Image[13] { loadPort1.slot1, loadPort1.slot2, loadPort1.slot3, loadPort1.slot4, loadPort1.slot5, loadPort1.slot6, loadPort1.slot7, loadPort1.slot8, loadPort1.slot9, loadPort1.slot10, loadPort1.slot11, loadPort1.slot12, loadPort1.slot13 };
            Lpm2Slots = new Image[13] { loadPort2.slot1, loadPort2.slot2, loadPort2.slot3, loadPort2.slot4, loadPort2.slot5, loadPort2.slot6, loadPort2.slot7, loadPort2.slot8, loadPort2.slot9, loadPort2.slot10, loadPort2.slot11, loadPort2.slot12, loadPort2.slot13 };

            Lpm1CompleteCheck = new Image[3, 13]{ 
                { loadPort1.alignCheck1, loadPort1.alignCheck2, loadPort1.alignCheck3, loadPort1.alignCheck4, loadPort1.alignCheck5, loadPort1.alignCheck6, loadPort1.alignCheck7, loadPort1.alignCheck8, loadPort1.alignCheck9, loadPort1.alignCheck10, loadPort1.alignCheck11, loadPort1.alignCheck12, loadPort1.alignCheck13 },
                { loadPort1.inspCheck1, loadPort1.inspCheck2, loadPort1.inspCheck3, loadPort1.inspCheck4, loadPort1.inspCheck5, loadPort1.inspCheck6, loadPort1.inspCheck7, loadPort1.inspCheck8, loadPort1.inspCheck9, loadPort1.inspCheck10, loadPort1.inspCheck11, loadPort1.inspCheck12, loadPort1.inspCheck13 },
                { loadPort1.reviewCheck1, loadPort1.reviewCheck2, loadPort1.reviewCheck3, loadPort1.reviewCheck4, loadPort1.reviewCheck5, loadPort1.reviewCheck6, loadPort1.reviewCheck7, loadPort1.reviewCheck8, loadPort1.reviewCheck9, loadPort1.reviewCheck10, loadPort1.reviewCheck11, loadPort1.reviewCheck12, loadPort1.reviewCheck13 }
            };
            Lpm2CompleteCheck = new Image[3, 13]{
                { loadPort2.alignCheck1, loadPort2.alignCheck2, loadPort2.alignCheck3, loadPort2.alignCheck4, loadPort2.alignCheck5, loadPort2.alignCheck6, loadPort2.alignCheck7, loadPort2.alignCheck8, loadPort2.alignCheck9, loadPort2.alignCheck10, loadPort2.alignCheck11, loadPort2.alignCheck12, loadPort2.alignCheck13 },
                { loadPort2.inspCheck1, loadPort2.inspCheck2, loadPort2.inspCheck3, loadPort2.inspCheck4, loadPort2.inspCheck5, loadPort2.inspCheck6, loadPort2.inspCheck7, loadPort2.inspCheck8, loadPort2.inspCheck9, loadPort2.inspCheck10, loadPort2.inspCheck11, loadPort2.inspCheck12, loadPort2.inspCheck13 },
                { loadPort2.reviewCheck1, loadPort2.reviewCheck2, loadPort2.reviewCheck3, loadPort2.reviewCheck4, loadPort2.reviewCheck5, loadPort2.reviewCheck6, loadPort2.reviewCheck7, loadPort2.reviewCheck8, loadPort2.reviewCheck9, loadPort2.reviewCheck10, loadPort2.reviewCheck11, loadPort2.reviewCheck12, loadPort2.reviewCheck13 }
            };
        }
        
        Dictionary<int, ImageSource> waferInfo = new Dictionary<int, ImageSource>()
        {
            {0, new BitmapImage (new Uri(@"Images/WaferAbsence.png", UriKind.Relative))},
            {1, new BitmapImage (new Uri(@"Images/WaferPresence.png", UriKind.Relative))},
            {2, new BitmapImage (new Uri(@"Images/WaferDouble.png", UriKind.Relative))},
            {3, new BitmapImage (new Uri(@"Images/waferCross.png", UriKind.Relative))},
            {4, new BitmapImage (new Uri(@"Images/WaferUnKnown.png", UriKind.Relative))},
            {5, new BitmapImage (new Uri(@"Images/WaferComplete.png", UriKind.Relative))}, 
        };
        public void UpdateLPMDoor(bool isLPM1Open, bool isLPM2Open)
        {
            lblLPM1Door.Visibility = isLPM1Open == true ? Visibility.Hidden : Visibility.Visible;
            lblLPM2Door.Visibility = isLPM2Open == true ? Visibility.Hidden : Visibility.Visible;
        }
        public void UpdateLPM(object[] _lpm1Data, object[] _lpm2Data, bool _isLpm1CstExist, bool _isLpm2CstExist)
        {
            int[] arrLpm1Data = new int[13];
            int[] arrLpm2Data = new int[13];
            arrLpm1Data = _lpm1Data != null ? _lpm1Data[0] as int[] : new int[13];
            arrLpm2Data = _lpm2Data != null ? _lpm2Data[0] as int[] : new int[13];
            UpdateLPMWafer(arrLpm1Data, arrLpm2Data);

            arrLpm1Data = _lpm1Data != null ? _lpm1Data[1] as int[] : new int[0];
            arrLpm2Data = _lpm2Data != null ? _lpm2Data[1] as int[] : new int[0];
            ShowProgressingWafer(arrLpm1Data, arrLpm2Data);
            
            bool[,] arrLpm1InspEnd = _lpm1Data != null ? _lpm1Data[2] as bool[,] : new bool[13,13];
            bool[,] arrLpm2InspEnd = _lpm2Data != null ? _lpm2Data[2] as bool[,] : new bool[13,13];
            UpdateComplete(arrLpm1InspEnd, arrLpm2InspEnd);
        }
        
        public void UpdateLPMWafer(int [] slots1, int [] slots2)
        {
            for(int i = 0; i < 13; i++)
            {
                Lpm1Slots[i].Source = waferInfo[slots1[i]];
                if ((i + 1) == slots1.Count())
                    break;
            }
            for (int i = 0; i < 13; i++)
            {
                Lpm2Slots[i].Source = waferInfo[slots2[i]];
                if ((i + 1) == slots2.Count())
                    break;
            }
        }
        public void UpdateComplete(bool [,] isComplete1, bool [,] isComplete2)
        {
            for(int j = 0; j <3; j ++)
            {
                for (int i = 0; i < isComplete1.GetLength(1); i++)
                {
                    Lpm1CompleteCheck[j, i].Visibility = isComplete1[j, i] == true ? Visibility.Visible : Visibility.Hidden;
                    if(j == 2 && isComplete1[j, i] == true && isComplete1[0, i] == true && isComplete1[1, i])
                    {
                        Lpm1Slots[i].Source = waferInfo[5];
                    }
                }
                for (int i = 0; i < isComplete2.GetLength(1); i++)
                {
                    Lpm2CompleteCheck[j, i].Visibility = isComplete2[j, i] == true ? Visibility.Visible : Visibility.Hidden;
                    if (j == 2 && isComplete2[j, i] == true && isComplete2[0,i] == true && isComplete2[1,i])
                    {
                        Lpm2Slots[i].Source = waferInfo[5];
                    }
                }
            }
        }
        public void ShowProgressingWafer(int[] arr1, int[] arr2)
        {
        //    if (slotNo == 0)
        //        return;

            //Image[] Lpm = lmp == 1 ? Lpm1Slots : Lpm2Slots;

            int sec = int.Parse(DateTime.Now.ToString("ss"));

            foreach (int idx in arr1)
            {
                if (idx == 0)
                    continue;
                if (sec % 2 == 0)
                    Lpm1Slots[idx-1].Source = waferInfo[1]; //presence
                else
                    Lpm1Slots[idx-1].Source = waferInfo[0]; //absence
            }
            foreach (int idx in arr2)
            {
                if (sec % 2 == 0)
                    Lpm2Slots[idx - 1].Source = waferInfo[1]; //presence
                else
                    Lpm2Slots[idx - 1].Source = waferInfo[0]; //absence
            }
        }

        public void ChangeAlignerWaferState()
        {
            ImgAlignerWafer.Visibility = ImgAlignerWafer.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            ImgAlignerWaferRingFrame.Visibility = ImgAlignerWaferRingFrame.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }

        string strRemote = "예약";
        string strProgress = "진행";

        public void ChangeChinaLanguage(bool boChinaLanguage)
        {
            if (boChinaLanguage)
            {
                //gWaferInfo_Align.Text = "Align 完毕";         // Align 완료
                //gWaferInfo_Insp.Text = "检查完毕";          // 검사 완료
                //gWaferInfo_All.Text = "全体完毕";           // 전체 완료
                textBlock8.Text = "作业者";               // 작업자
                txtbInterlockMsg.Text = "互锁正在解锁 &#xA;注意冲撞";         // Interlock 해제 중 &#xA;충돌주의
                strRemote = "预约";                     // 예약
                strProgress = "进行";                   // 진행
            }
        }

        #region Robot Img
        double orgT = 0;
        public void MoveRobotFrame(int yPos, int rotation)
        {
            //상하
            ThicknessAnimation da = new ThicknessAnimation();
            da.From = gEFEMRobot.Margin;
            da.To = new Thickness(gEFEMRobot.Margin.Left, orgT + yPos, gEFEMRobot.Margin.Right, 0);
            da.Duration = new Duration(TimeSpan.FromMilliseconds(300));

            gEFEMRobot.BeginAnimation(Border.MarginProperty, da);

            //회전
            RotateTransform rotateTransform;
            rotateTransform = new RotateTransform(rotation, 180, 60);
            gEFEMRobot.RenderTransform = rotateTransform;
        }

        private int LastRobotArm = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="port">0:INIT, 1:LPM1, 2:LPM2, 9:ALIGNER, 5:AVI</param>
        /// <param name="robotarm">0:Upper, 1:Lower</param>
        public void SetRobotPos(int port)
        {
            borderLeftFrame.Visibility = Visibility.Visible;
            borderRightFrame.Visibility = Visibility.Hidden;
            switch (port)
            {
                case 9:
                    MoveRobotFrame(-230, -90);
                    break;
                case 1:
                    MoveRobotFrame(340, 0);
                    break;
                case 2:
                    MoveRobotFrame(-270, 0);
                    break;
                case 5:
                    MoveRobotFrame(-180, 0);
                    borderLeftFrame.Visibility = Visibility.Hidden;
                    borderRightFrame.Visibility = Visibility.Visible;
                    break;
                case 0:
                    MoveRobotFrame(0, 0);
                    break;
            }
        }
        /// <summary>
        /// Robot Arm 위치 변경을 위한 Event
        /// </summary>
        /// <param name="value[0]">true : Upper / false : Lower</param>
        /// <param name="value[1]">true : Tarport = AVI</param>
        public void MoveRobotArm(bool[] value)
        {
            //Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate ()
            Dispatcher.BeginInvoke(new Action(delegate()
            {
                if (value[0] == true)
                {
                    ThicknessAnimation da = new ThicknessAnimation();
                    da.BeginTime = TimeSpan.FromMilliseconds(1500);
                    da.From = borderUpperArm.Margin;
                    if (value[1] == true)
                    {
                        da.To = new Thickness(borderUpperArm.Margin.Left - 170, borderUpperArm.Margin.Top, borderUpperArm.Margin.Right, borderUpperArm.Margin.Bottom);
                    }
                    else
                    {
                        da.To = new Thickness(borderUpperArm.Margin.Left + 170, borderUpperArm.Margin.Top, borderUpperArm.Margin.Right, borderUpperArm.Margin.Bottom);
                    }
                    da.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
                    da.AutoReverse = true;

                    borderUpperArm.BeginAnimation(Border.MarginProperty, da);
                }
                else
                {
                    ThicknessAnimation da = new ThicknessAnimation();
                    da.BeginTime = TimeSpan.FromMilliseconds(1500);
                    da.From = borderLowerArm.Margin;
                    if (value[1] == true)
                    {
                        da.To = new Thickness(borderLowerArm.Margin.Left - 170, borderLowerArm.Margin.Top, borderLowerArm.Margin.Right, borderLowerArm.Margin.Bottom);
                    }
                    else
                    {
                        da.To = new Thickness(borderLowerArm.Margin.Left + 170, borderLowerArm.Margin.Top, borderLowerArm.Margin.Right, borderLowerArm.Margin.Bottom);
                    }
                    da.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
                    da.AutoReverse = true;

                    borderLowerArm.BeginAnimation(Border.MarginProperty, da);
                }
            }));
        }
        public void SetRobotVac(bool isUpperArmVacOn, bool isLowerArmVacOn)
        {
            if(isUpperArmVacOn == true)
            {
                borderUpperArm.Background = Brushes.SkyBlue;
                borderUpperArmBk.Background = Brushes.SkyBlue;
            }
            else
            {
                borderUpperArm.Background = Brushes.LightYellow;
                borderUpperArmBk.Background = Brushes.LightYellow;
            }
            if(isLowerArmVacOn == true)
            {
                borderLowerArm.Background = Brushes.SkyBlue;
                borderLowerArmBk.Background = Brushes.SkyBlue;
            }
            else
            {
                borderLowerArm.Background = Brushes.LightYellow;
                borderLowerArmBk.Background = Brushes.LightYellow;
            }
        }
        #endregion
        //State
        public TextBlock statLpm1Alarm { get { return LPM1Alarm; } }
        public TextBlock statLpm1AutoMode { get { return LPM1AutuMode; } }
        public TextBlock statLpm1Ready { get { return LPM1Ready; } }
        public TextBlock statLpm1Busy { get { return LPM1DoorUpOff; } }
        public TextBlock statLpm1WSOOff { get { return LPM1WSOOff; } }

        public TextBlock statLpm2Alarm { get { return LPM2Alarm; } }
        public TextBlock statLpm2AutoMode { get { return LPM2AutoMode; } }
        public Border statLpm2Ready { get { return LPM2Ready; } }
        public TextBlock statLpm2Busy { get { return LPM2DoorUpOff; } }
        public TextBlock statLpm2WSOOff { get { return LPM2WSOOff; } }

        public TextBlock statAlignAlarm { get { return AlignAlarm; } }
        public TextBlock statAlignReady { get { return AlignReady; } }
        public TextBlock statAlignIdle { get { return AlignIdle; } }

        public TextBlock statRobotAlarm { get { return RobotAlarm; } }
        public TextBlock statRobotServoOff { get { return RobotServoOff; } }
        public TextBlock statRobotIdle { get { return RobotIdle; } }

        private void BtnLPM1Load_Click(object sender, RoutedEventArgs e)
        {
            LPM1Pushed?.Invoke();
        }

        private void BtnLPM2Load_Click(object sender, RoutedEventArgs e)
        {
            LPM2Pushed?.Invoke();
        }

        #endregion

        private void BtnPreAlignerSetting_Click(object sender, RoutedEventArgs e)
        {
            PreAlignerSettingBtnPushed?.Invoke();
        }
    }
}