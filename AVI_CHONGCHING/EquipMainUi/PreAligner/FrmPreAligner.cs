using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using Dit.Rnd.Framework.Grabber;
using Dit.Rnd.Framework.Grabber.Basler;
using OpenCvSharp;
using EquipMainUi.Struct;
using EquipMainUi.PreAligner.Recipe;
using System.Collections;
using System.Diagnostics;
using Dit.Rnd.Framework.Drawing;
using Dit.Rnd.Framework.MapView;
using Dit.Rnd.Framework.MapView.Tool;
using Dit.Framework.UI.UserComponent;
using EquipMainUi.Struct.Detail.EziStep;
using EquipMainUi.Setting;
using Dit.Framework.Comm;

namespace EquipMainUi.PreAligner
{
    public partial class FrmPreAligner : Form
    {
        public bool UpdateRequestAlignerPGrid { get; private set; }
        private BigBitmap _bigBitmap;
        private BigImageShape _cameraImg;
        Timer timer;
        PlcTimerEx AutoCloseDelay = new PlcTimerEx("프리얼라이너 세팅 창 자동 종료");
        public FrmPreAligner()
        {
            InitializeComponent();

            _bigBitmap = new BigBitmap(4024, 3036, 3, true, 0);
            InitailizeMapView(4024, 3036, 3, _bigBitmap);

            GG.Equip.PreAligner.CameraConnected += FrameGrabber_CameraConnected;
            GG.Equip.PreAligner.CameraClosed += FrameGrabber_CameraClosed;
            GG.Equip.PreAligner.Grabed += FrameGrabber_Grabed;
            GG.Equip.PreAligner.GrabStarted += FrameGrabber_GrabStarted;
            GG.Equip.PreAligner.GrabStopped += FrameGrabber_GrabStopped;
            GG.Equip.PreAligner.ProcessingDone += PreAligner_ProcessingCompelete;


            InitList();
            ucrlLightControllerTest1.Initialize();
            if (GG.TestMode == false)
            {
                try
                {
                    lblDllVersion.Text = CWaferPreAlignerWrapper.GetDllVersion().ToString("X");
                }
                catch (Exception ex)
                {
                    Logger.ExceptionLog.AppendLine(LogLevel.Exception, "DllVersion 불러오기 Error 발생 " + ex.ToString());
                }
            }

            ChangeChinaLanguage();

            timer = new Timer();
            timer.Tick += Timer_Tick;
            AutoCloseDelay.Start(60, 0);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (GG.Equip.EquipRunMode == EmEquipRunMode.Auto
                && AutoCloseDelay)
            {
                AutoCloseDelay.Stop();
                this.Close();
            }
        }

        private void ChangeChinaLanguage()
        {
            if (GG.boChinaLanguage)
            {
                tabAlignResult.Text = "Align 结果";              // Align결과
                tabDefectResult.Text = "检查结果";             // 검사 결과
                tabOperator.Text = "Motor 操作";                 // 모터 조작
                label3.Text = "■ Align 结果";                      // ■ Align 결과
                label2.Text = "Recipe 目录";                      // 레시피 목록
                label1.Text = "选择的 Recipe ";                      // 선택한 레시피
                btnFixedRecipe.Text = "Recipe 固定";              // 레시피 고정
                btnCreateDefaultRecipe.Text = "(Default) 值";      // Default 값
                btnStartNotchROISetting.Text = "(Notch ROI) 设置";     // Notch ROI 설정
                btnInsert.Text = "生成";                           // 생성
                btnUpdate.Text = "修改";                           // 수정
                btnDelete.Text = "删除";                           // 삭제
                label36.Text = "当前位置";                     // 현재위치
                label37.Text = "当前位置";                     // 현재위치
                label38.Text = "当前位置";                     // 현재위치
                cbAlignX.Text = "位置选择";                    // 위치 선택
                cbAlignY.Text = "位置选择";                    // 위치 선택
                cbAlignT.Text = "位置选择";                    // 위치 선택
                btnAlignXPtpMove.Text = "位置移动";            // Ptp 이동
                btnAlignYPtpMove.Text = "位置移动";            // Ptp 이동
                btnAlignTPtpMove.Text = "位置移动";            // Ptp 이동
                btnAlignTJogPlus.Text = "(+)逆时针";            // (+)반시계
                btnAlignTJogMinus.Text = "顺时针(-)";           // 시계(-)
                label80.Text = "OCR Cylinder";                     // OCR 실린더
                btnOcrUp.Text = "上升";                    // 상승
                btnOcrDown.Text = "下降";                  // 하강
                groupBox3.Text = "Sequence";                // 시퀀스
            }
        }

        public void InitailizeMapView(int width, int height, int channel, BigBitmap img = null)
        {
            MapView.Initialize(0, 0, 0, EmRotate.R_000, 0, 0, width, height);
            MapView.Canvas.AxisXDir = EmAxisDir.Forward;
            MapView.Canvas.AxisYDir = EmAxisDir.Forward;
            MapView.ShapeActioned += MapView_ShapeActioned;

            _cameraImg = new BigImageShape()
            {
                Location = new PointF(0, 0),
                Size = new SizeF(width, height),
                SelectMode = EmSelectMode.NonSelect,
                Locked = true,
                IsRealTime = true,
                Rotate = RotateFlipType.RotateNoneFlipNone,
                Bbmp = img,
            };

            MapView.Canvas.Shapes.Clear();
            MapView.Canvas.CanvasRound = new Rectangle(0, 0, (int)_cameraImg.Width, (int)_cameraImg.Height);

            MapView.Canvas.Shapes.Add(_cameraImg);
            MapView.Zoom(_cameraImg, 0.99f);
            MapView.Refresh();
        }

        private void MapView_ShapeActioned(object sender, ShapeEventArgs e)
        {
            if (e.Action is CreatedShapeAction
                || e.Action is MovedShapeAction)
            {
                var r = (PreAlignerRecipe)pGridSelectedRecipe.SelectedObject;
                r.NotchFindROILeft = (int)e.Shape.X;
                r.NotchFindROITop = (int)e.Shape.Y;
                r.NotchFindROIRight = (int)(e.Shape.X + e.Shape.Width);
                r.NotchFindROIBottom = (int)(e.Shape.Y + e.Shape.Height);
                pGridSelectedRecipe.SelectedObject = r;

                MapView.Canvas.Shapes.Remove(e.Shape);
            }
        }

        private void UpdateAlignerPGrid()
        {
            if (UpdateRequestAlignerPGrid == true)
            {
                UpdateRequestAlignerPGrid = false;
                pGridResult.SelectedObject = new CWaferPreAlignerResultToPGrid(GG.Equip.PreAligner.AlignParamResult);
                pGridRecipe.SelectedObject = GG.Equip.PreAligner.CurRecipe;
                pGridRecipe.Refresh();
            }
        }

        private void InitList()
        {
            lstRcps.Clear();
            lstRcps.Columns.Add(new ColumnHeader() { Text = "Name", Width = 240 });
            UpdateRecipeInfo();

            cbAlignX.Items.Clear();
            foreach (ServoPosiInfo pos in GG.Equip.AlignerX.Setting.LstServoPosiInfo)
                cbAlignX.Items.Add(pos.Name);
            ucrlPtpX.Pos = 0;
            ucrlPtpX.Spd = 0;

            cbAlignY.Items.Clear();
            foreach (ServoPosiInfo pos in GG.Equip.AlignerY.Setting.LstServoPosiInfo)
                cbAlignY.Items.Add(pos.Name);
            ucrlPtpY.Pos = 0;
            ucrlPtpY.Spd = 0;

            cbAlignT.Items.Clear();
            foreach (ServoPosiInfo pos in GG.Equip.AlignerT.Setting.LstServoPosiInfo)
                cbAlignT.Items.Add(pos.Name);
            ucrlPtpT.Pos = 0;
            ucrlPtpT.Spd = 0;
        }

        private void UpdateRecipeInfo()
        {
            lstRcps.Items.Clear();

            foreach (var r in PreAlignerRecipeDataMgr.GetRecipes())
            {
                string[] row = { r.Name };
                ListViewItem newitem = new ListViewItem(row);
                lstRcps.Items.Add(newitem);
            }

            pGridSelectedRecipe.SelectedObject = new PreAlignerRecipe();

            lstRcps_ColumnClick(null, new ColumnClickEventArgs(0));
            lstRcps_ColumnClick(null, new ColumnClickEventArgs(0));
        }

        private void Release()
        {
            GG.Equip.PreAligner.CameraConnected -= FrameGrabber_CameraConnected;
            GG.Equip.PreAligner.CameraClosed -= FrameGrabber_CameraClosed;
            GG.Equip.PreAligner.Grabed -= FrameGrabber_Grabed;
            GG.Equip.PreAligner.GrabStarted -= FrameGrabber_GrabStarted;
            GG.Equip.PreAligner.GrabStopped -= FrameGrabber_GrabStopped;
            GG.Equip.PreAligner.ProcessingDone -= PreAligner_ProcessingCompelete;
        }

        private void FrmPreAligner_Load(object sender, EventArgs e)
        {
        }

        private void BtnLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image Files (*.bmp)|*.bmp;";
                dialog.FilterIndex = 1;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // 선택한 이미지를 불러온다.
                    GG.Equip.PreAligner.SetImage(Image.FromFile(dialog.FileName));

                    //create output color image
                    Bitmap colorImage = new Bitmap(GG.Equip.PreAligner.SrcImage.Width, GG.Equip.PreAligner.SrcImage.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    using (Graphics gr = Graphics.FromImage(colorImage))
                    {
                        gr.DrawImage(GG.Equip.PreAligner.SrcImage, new Rectangle(0, 0, colorImage.Width, colorImage.Height));
                    }
                    InvokeRefreshPictureImg(colorImage);
                }
            }
        }

        private void btnProcessing_Click(object sender, EventArgs e)
        {
            // 1. 저장된 Recipe 확인
            if (lstRcps.Items.Count == 0)
            {
                // 2. default 값 생성
                pGridSelectedRecipe.SelectedObject = PreAlignerRecipe.CreateDefault();
                // 3. Recipe 생성
                PreAlignerRecipe r = (PreAlignerRecipe)pGridSelectedRecipe.SelectedObject;
                r.Name = "Defalut";
                if (PreAlignerRecipeDataMgr.IsExist(r.Name) == true)
                {
                    MessageBox.Show(GG.boChinaLanguage ? "请创建一个新配方。" : "새로운 Recipe를 생성해주세요");
                    return;
                }
                PreAlignerRecipeDataMgr.Insert((PreAlignerRecipe)r.Clone());
                UpdateRecipeInfo();
            }
            else
            {
                if (PreAlignerRecipeDataMgr.GetRecipes() != null)
                {
                    foreach (var AlignRecipe in PreAlignerRecipeDataMgr.GetRecipes())
                    {
                        var q = PreAlignerRecipeDataMgr.GetRecipe(AlignRecipe.Name);
                        GG.Equip.FixedDitAlignerRecipeName = AlignRecipe.Name;
                        GG.Equip.PreAligner.SetCurRecipe(q);
                    }
                }
            }
            GG.Equip.PreAligner.ImageProcessingUpdate();
        }

        private void InitDGVResult()
        {
        }

        private void RotatePointF(ref double dX, ref double dY, double rad, double dCX, double dCY)
        {
            double cosR = Math.Cos(rad);
            double sinR = Math.Cos(rad);

            double dRX, dRY;

            dRX = (int)((dX - dCX) * cosR - (dY - dCY) * sinR + dCX);
            dRY = (int)((dX - dCX) * sinR + (dY - dCY) * cosR + dCY);

            dX = dRX;
            dY = dRY;
        }

        private void DrawPreAlignResult()
        {
            var img = _bigBitmap.GetFullImage();
            if (img == null) return;
            var param = GG.Equip.PreAligner.AlignParamResult;
            var defect = GG.Equip.PreAligner.Defects;
            if (param == null) return;

            Bitmap bmp = img;
            using (Pen lineEllipse = new Pen(Color.Orange, 4f))
            using (Pen lineRed = new Pen(Color.Red, 2f))
            using (Pen lineBlue = new Pen(Color.Blue, 2f))
            using (Pen lineGreen = new Pen(Color.Green, 3f))
            using (Pen lineYellow = new Pen(Color.Yellow, 3f))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // 장축 방향 확인을 위해 장축 좌표 역변환
                double MajorX1 = param.m_dMajorStartX;
                double MajorY1 = param.m_dMajorStartY;
                double MajorX2 = param.m_dMajorEndX;
                double MajorY2 = param.m_dMajorEndY;
                RotatePointF(ref MajorX1, ref MajorY1, -param.m_dMajorRadian, param.m_dCenterX, param.m_dCenterY);
                RotatePointF(ref MajorX2, ref MajorY2, -param.m_dMajorRadian, param.m_dCenterX, param.m_dCenterY);

                int nX = (MajorY1 == MajorY2) ? (int)(param.m_dCenterX - param.m_dMajorLength / 2) : (int)(param.m_dCenterX - param.m_dMinorLength / 2);
                int nY = (MajorY1 == MajorY2) ? (int)(param.m_dCenterY - param.m_dMinorLength / 2) : (int)(param.m_dCenterY - param.m_dMajorLength / 2);
                int nWidth = (MajorY1 == MajorY2) ? (int)param.m_dMajorLength : (int)param.m_dMinorLength;
                int nHeight = (MajorY1 == MajorY2) ? (int)param.m_dMinorLength : (int)param.m_dMajorLength;
                Rectangle rect = new Rectangle(new System.Drawing.Point(nX, nY), new System.Drawing.Size(nWidth, nHeight));

                // Draw Fit Ellipse
                g.TranslateTransform((int)param.m_dCenterX, (int)param.m_dCenterY);
                g.RotateTransform((float)(90 + param.m_dMajorDegree));
                g.TranslateTransform((int)-param.m_dCenterX, (int)-param.m_dCenterY);
                g.DrawEllipse(lineEllipse, rect);
                g.ResetTransform();

                // Draw Major
                if (param.m_dMajorStartX != 0 && param.m_dMajorStartY != 0 && param.m_dMajorEndX != 0 && param.m_dMajorEndY != 0)
                    g.DrawLine(lineRed, (float)param.m_dMajorStartX, (float)param.m_dMajorStartY, (float)param.m_dMajorEndX, (float)param.m_dMajorEndY);

                // Draw Minor
                if (param.m_dMinorStartX != 0 && param.m_dMinorStartY != 0 && param.m_dMinorEndX != 0 && param.m_dMinorEndY != 0)
                    g.DrawLine(lineBlue, (float)param.m_dMinorStartX, (float)param.m_dMinorStartY, (float)param.m_dMinorEndX, (float)param.m_dMinorEndY);

                // Draw Center
                int nDisplaySize = 10;
                if (param.m_dCenterX != 0 && param.m_dCenterY != 0)
                {
                    g.DrawLine(lineYellow, (float)param.m_dCenterX - nDisplaySize, (float)param.m_dCenterY - nDisplaySize, (float)param.m_dCenterX + nDisplaySize, (float)param.m_dCenterY + nDisplaySize);
                    g.DrawLine(lineYellow, (float)param.m_dCenterX - nDisplaySize, (float)param.m_dCenterY + nDisplaySize, (float)param.m_dCenterX + nDisplaySize, (float)param.m_dCenterY - nDisplaySize);
                }

                // Draw Center to Notch Line
                if (param.m_dNotchX != 0 && param.m_dNotchY != 0)
                {
                    g.DrawLine(lineYellow, (float)param.m_dCenterX, (float)param.m_dCenterY, (float)param.m_dNotchX, (float)param.m_dNotchY);
                }

                // Draw Notch
                if (param.m_dNotchX != 0 && param.m_dNotchY != 0)
                {
                    g.DrawLine(lineGreen, (float)param.m_dNotchX - nDisplaySize, (float)param.m_dNotchY - nDisplaySize, (float)param.m_dNotchX + nDisplaySize, (float)param.m_dNotchY + nDisplaySize);
                    g.DrawLine(lineGreen, (float)param.m_dNotchX - nDisplaySize, (float)param.m_dNotchY + nDisplaySize, (float)param.m_dNotchX + nDisplaySize, (float)param.m_dNotchY - nDisplaySize);
                }

                // Notch ROI
                rect = new Rectangle(new System.Drawing.Point(param.m_nNotchFindROILeft, param.NotchFindROITop),
                    new System.Drawing.Size(param.m_nNotchFindROIRight - param.m_nNotchFindROILeft, param.m_nNotchFindROIBottom - param.m_nNotchFindROITop));
                g.DrawRectangle(lineGreen, rect);

                // Draw Result Text
                using (Font fontText = new Font("Times New Roman", 50, FontStyle.Bold, GraphicsUnit.Pixel))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("{0}\n", DateTime.Now.ToString("yyMMdd_HH:mm:ss.fff"));
                    sb.AppendFormat("WaferID : {0}\n", GG.Equip.PreAligner.WaferID);
                    sb.AppendFormat("Recipe : {0}\n", GG.Equip.PreAligner.CurRecipe.Name);
                    sb.AppendFormat("Result : {0}\n", GG.Equip.PreAligner.ProcessingResult.ToString());
                    sb.AppendFormat(GG.boChinaLanguage ? "检查与否 : {0}\n" : "검사여부 : {0}\n", param.UseInspect);
                    sb.AppendFormat(GG.boChinaLanguage ? "缺陷数 : {0}\n" : "결함수 : {0}\n", param.DefectCount);
                    sb.AppendFormat("Processing Time : {0}ms\n", GG.Equip.PreAligner.ProcessingTime);
                    sb.AppendFormat(GG.boChinaLanguage ? "点数 : {0} " : "점수 : {0} ", param.NotchMatchScore.ToString("F2"));

                    g.DrawString(sb.ToString(), fontText, Brushes.Red, 10, 10);
                }

                // Draw Defect
                if (param.DefectCount > 0)
                {
                    RectangleF[] rectangles = new RectangleF[param.DefectCount];
                    for (int i = 0; i < param.DefectCount; ++i)
                    {
                        rectangles[i].X = defect[i].nLeft;
                        rectangles[i].Y = defect[i].nTop;
                        rectangles[i].Width = (defect[i].nRight - defect[i].nLeft);
                        rectangles[i].Height = (defect[i].nBottom - defect[i].nTop);
                    }

                    g.DrawRectangles(lineRed, rectangles);
                }
            }
            InvokeRefreshPictureImg(bmp);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //FrmTest f = new FrmTest();
            //f.Show();
        }
        private Queue<string[]> _logQueue = new Queue<string[]>();
        public void Log(string msg)
        {
            _logQueue.Enqueue(new string[] { DateTime.Now.ToString("HH:mm:ss.fff"), msg });
        }
        public void Log(string msg, bool success)
        {
            Log(String.Format("{0} {1}", msg, success ? "Success" : "Fail"));
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (GG.Equip.PreAligner.OpenCamera() == false)
            {
                if (GG.Equip.PreAligner.FrameGrabber.IsConnected == true)
                    CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "已经连接了" : "이미 연결돼있습니다");
                else
                    CheckMgr.AddCheckMsg(false, GG.boChinaLanguage ? "无法连接，五分钟后尝试" : "연결 할 수 없습니다 5분 뒤에 시도하세요");
                return;
            }
        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Log("Disconnect", GG.Equip.PreAligner.FrameGrabber.Disconnection());
        }
        private void FrameGrabber_GrabStopped(object sender, EventArgs e)
        {
            Log("Grab Stopped");
        }

        private void FrameGrabber_GrabStarted(object sender, EventArgs e)
        {
            Log("Grab Started");
            ClearImage();
        }

        private void FrameGrabber_CameraClosed(object sender, EventArgs e)
        {
            Log("Grab Closed");
        }

        private void FrameGrabber_CameraConnected(object sender, EventArgs e)
        {
            Log("Camera Connected");
        }

        private void FrameGrabber_Grabed(object sender, FrameGrabbedEventArgs e)
        {
            Log("Grabbed");

            UpdateGrabImage();
        }

        private void PreAligner_ProcessingCompelete(object sender, EventArgs e)
        {
            Log("Proceesing Done");
            Log(GG.Equip.PreAligner.ProcessingResult.ToString());
            UpdateGrabImage();
            DrawPreAlignResult();
            UpdateRequestAlignerPGrid = true;
        }
        private void ClearImage()
        {
            InvokeRefreshPictureImg(null);
        }
        private void InvokeRefreshPictureImg(Bitmap img)
        {
            if (MapView.InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {
                    lock (_bigBitmap)
                    {
                        _bigBitmap.SetBitmap(0, 0, img);
                        MapView.Invalidate();
                    }
                });
            }
            else
            {
                lock (_bigBitmap)
                {
                    _bigBitmap.SetBitmap(0, 0, img);
                    MapView.Invalidate();
                }
            }
        }

        private DateTime _lastGrabImageUpdate = PcDateTime.Now;
        private void UpdateGrabImage()
        {
            if ((PcDateTime.Now - _lastGrabImageUpdate).TotalMilliseconds < 150)
                return;

            if (MapView.InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {
                    InvokeRefreshGrabImage();
                });
            }
            else
            {
                InvokeRefreshGrabImage();
            }
        }
        private void InvokeRefreshGrabImage()
        {
            lock (_bigBitmap)
            {
                if (GG.Equip.PreAligner.SrcImage != null)
                {
                    using (var srcImage = GG.Equip.PreAligner.SrcImage.Clone() as Image)
                    {
                        if (srcImage == null) return;
                        //create output color image
                        Bitmap colorImage = new Bitmap(srcImage.Width, srcImage.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                        using (Graphics gr = Graphics.FromImage(colorImage))
                        {
                            gr.DrawImage(srcImage, new Rectangle(0, 0, colorImage.Width, colorImage.Height));
                        }
                        _bigBitmap.SetBitmap(0, 0, colorImage);
                        MapView.Invalidate();
                        _lastGrabImageUpdate = PcDateTime.Now;
                    }
                }
            }
        }
        private void btnSnap_Click(object sender, EventArgs e)
        {
            if (GG.Equip.PreAligner.FrameGrabber.IsGrabbing)
                GG.Equip.PreAligner.FrameGrabber.StopGrab();
            else
                AsyncSnap();
        }

        public async void AsyncSnap()
        {
            var task = Task.Run(() =>
            {
                GG.Equip.PreAligner.FrameGrabber.Snap();
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (sw.ElapsedMilliseconds < 1000)
                {
                    if (GG.Equip.PreAligner.FrameGrabber.IsGrabbing == false)
                        return;
                }
            });

            await task;

            GG.Equip.PreAligner.FrameGrabber.StopGrab();
        }

        private void btnLive_Click(object sender, EventArgs e)
        {
            GG.Equip.PreAligner.StartLive();
        }

        private void btnStopSnap_Click(object sender, EventArgs e)
        {
            GG.Equip.PreAligner.StopLive();
        }

        private void FrmPreAligner_FormClosing(object sender, FormClosingEventArgs e)
        {
            Release();
        }

        private void btnSetExposure_Click(object sender, EventArgs e)
        {
            int exTime = 0;
            if (int.TryParse(txtExposureTime.Text, out exTime))
                GG.Equip.PreAligner.FrameGrabber.ExposureTime = exTime;
            else
                MessageBox.Show(GG.boChinaLanguage ? "值发生问题 (35倍数)" : "값이상 (35배수)");
        }

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
        private Color _clrRed = Color.FromArgb(255, 100, 100);
        private void tmr_UiUpdate_Tick(object sender, EventArgs e)
        {
            SetEnabled(panel18, (GG.Equip.EquipRunMode == EmEquipRunMode.Manual));
            SetEnabled(panel24, (GG.Equip.EquipRunMode == EmEquipRunMode.Manual));
            SetEnabled(panel25, (GG.Equip.EquipRunMode == EmEquipRunMode.Manual));
            SetEnabled(panel26, (GG.Equip.EquipRunMode == EmEquipRunMode.Manual));
            SetEnabled(panel27, (GG.Equip.EquipRunMode == EmEquipRunMode.Manual));

            ucrlLightControllerTest1.UpdateUI();
            if (GG.TestMode == false)
            {
                if (GG.Equip.PreAligner.FrameGrabber != null)
                {
                    btnConnect.OnOff = GG.Equip.PreAligner.FrameGrabber.IsConnected;
                    btnDisconnect.OnOff = !GG.Equip.PreAligner.FrameGrabber.IsConnected;
                }
                lblStep.Text = GG.Equip.PreAligner.Step.ToString();
            }
            btnFixedRecipe.OnOff = GG.Equip.UseFixedDitAlignerRecipe;
            btnSetRecipe.OnOff = GG.Equip.UseSetDitAlignerRecipe;
            UpdateAlignerPGrid();

            #region Operating
            btnOcrUp.OnOff = GG.Equip.AlignerOcrCylinder.IsUp;
            btnOcrDown.OnOff = GG.Equip.AlignerOcrCylinder.IsDown;

            btnAlignBlowerOn.OnOff = GG.Equip.AlignerVac.Blower.IsOnOff;
            btnAlignVacuumOn.OnOff = GG.Equip.AlignerVac.IsVacuumOn;
            btnAlignVacuumOff.OnOff = GG.Equip.AlignerVac.IsVacuumOff;

            //AlignX
            tbAlignXCurPosition.Text = GG.Equip.AlignerX.XF_CurrMotorPosition.vFloat.ToString();

            lblAlignXHomeBit.BackColor = GG.Equip.AlignerX.IsHomeCompleteBit ? _clrRed : Color.Gainsboro;
            lblAlignXMoving.BackColor = GG.Equip.AlignerX.IsMoving ? _clrRed : Color.Gainsboro;
            lblAlignXMinusLimit.BackColor = GG.Equip.AlignerX.IsMinusLimit ? _clrRed : Color.Gainsboro;
            lblAlignXPlusLimit.BackColor = GG.Equip.AlignerX.IsPlusLimit ? _clrRed : Color.Gainsboro;
            lblAlignXServoOn.BackColor = GG.Equip.AlignerX.IsServoOnBit ? _clrRed : Color.Gainsboro;

            btnAlignXHome.OnOff = GG.Equip.AlignerX.XB_StatusHomeInPosition;
            btnAlignXHome.Flicker = GG.Equip.AlignerX.IsHomming;
            btnAlignXJogMinus.OnOff = GG.Equip.AlignerX.YB_MotorJogMinusMove;
            btnAlignXJogPlus.OnOff = GG.Equip.AlignerX.YB_MotorJogPlusMove;
            btnAlignXPtpMove.OnOff = GG.Equip.AlignerX.XB_StatusMotorInPosition;

            //AlignY
            tbAlignYCurPosition.Text = GG.Equip.AlignerY.XF_CurrMotorPosition.vFloat.ToString();

            lblAlignYHomeBit.BackColor = GG.Equip.AlignerY.IsHomeCompleteBit ? _clrRed : Color.Gainsboro;
            lblAlignYMoving.BackColor = GG.Equip.AlignerY.IsMoving ? _clrRed : Color.Gainsboro;
            lblAlignYMinusLimit.BackColor = GG.Equip.AlignerY.IsMinusLimit ? _clrRed : Color.Gainsboro;
            lblAlignYPlusLimit.BackColor = GG.Equip.AlignerY.IsPlusLimit ? _clrRed : Color.Gainsboro;
            lblAlignYServoOn.BackColor = GG.Equip.AlignerY.IsServoOnBit ? _clrRed : Color.Gainsboro;

            btnAlignYHome.OnOff = GG.Equip.AlignerY.XB_StatusHomeInPosition;
            btnAlignYHome.Flicker = GG.Equip.AlignerY.IsHomming;
            btnAlignYJogMinus.OnOff = GG.Equip.AlignerY.YB_MotorJogMinusMove;
            btnAlignYJogPlus.OnOff = GG.Equip.AlignerY.YB_MotorJogPlusMove;
            btnAlignYPtpMove.OnOff = GG.Equip.AlignerY.XB_StatusMotorInPosition;

            //AlignT
            tbAlignTCurPosition.Text = GG.Equip.AlignerT.XF_CurrMotorPosition.vFloat.ToString();

            lblAlignTHomeBit.BackColor = GG.Equip.AlignerT.IsHomeCompleteBit ? _clrRed : Color.Gainsboro;
            lblAlignTMoving.BackColor = GG.Equip.AlignerT.IsMoving ? _clrRed : Color.Gainsboro;

            lblAlignTServoOn.BackColor = GG.Equip.AlignerT.IsServoOnBit ? _clrRed : Color.Gainsboro;

            btnAlignTHome.OnOff = GG.Equip.AlignerT.XB_StatusHomeInPosition;
            btnAlignTHome.Flicker = GG.Equip.AlignerT.IsHomming;
            btnAlignTJogMinus.OnOff = GG.Equip.AlignerT.YB_MotorJogMinusMove;
            btnAlignTJogPlus.OnOff = GG.Equip.AlignerT.YB_MotorJogPlusMove;
            btnAlignTPtpMove.OnOff = GG.Equip.AlignerT.XB_StatusMotorInPosition;
            #endregion

            if (_logQueue.Count > 0)
            {
                lock (_logQueue)
                {
                    lstLog.Items.Insert(0, new ListViewItem(_logQueue.Dequeue()));
                }

                if (lstLog.Items.Count > 200)
                {
                    for (int i = 0; i < 20; ++i)
                        lstLog.Items.RemoveAt(lstLog.Items.Count - 1);
                }
            }
        }

        private void btnStartGrabSeq_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn == btnStartGrabSeq)
            {
                if (GG.Equip.PreAligner.CurRecipe == null)
                {
                    MessageBox.Show(GG.boChinaLanguage ? "请把Recipe重新 Set ." : "레시피를 다시 \"SET\" 해주세요");
                    return;
                }
                GG.Equip.PreAligner.Start("Manual");
            }
            else if (btn == btnStopGrabSeq)
            {
                GG.Equip.PreAligner.Stop();
            }
        }

        private void lstRcps_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != 0)
            {
                return;
            }
            if (this.lstRcps.Sorting == SortOrder.Ascending || this.lstRcps.Sorting == SortOrder.None)
            {
                this.lstRcps.ListViewItemSorter = new ListviewItemComparer(e.Column, "desc");
                this.lstRcps.Sorting = SortOrder.Descending;
            }
            else if (this.lstRcps.Sorting == SortOrder.Descending || this.lstRcps.Sorting == SortOrder.None)
            {
                this.lstRcps.ListViewItemSorter = new ListviewItemComparer(e.Column, "acs");
                this.lstRcps.Sorting = SortOrder.Ascending;
            }

            lstRcps.Sort();
        }

        private void lstRcps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRcps.SelectedIndices.Count > 0)
            {
                string selected = lstRcps.SelectedItems[0].SubItems[0].Text;

                if (PreAlignerRecipeDataMgr.IsExist(selected) == true)
                {
                    PreAlignerRecipe cur = PreAlignerRecipeDataMgr.GetRecipe(selected);
                    pGridSelectedRecipe.SelectedObject = (PreAlignerRecipe)cur;
                    lblSelectedRecipe.Text = cur.Name;
                }
            }
        }


        private void btnCreateDefaultRecipe_Click(object sender, EventArgs e)
        {
            pGridSelectedRecipe.SelectedObject = PreAlignerRecipe.CreateDefault();
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            PreAlignerRecipe r = (PreAlignerRecipe)pGridSelectedRecipe.SelectedObject;

            if (PreAlignerRecipeDataMgr.IsExist(r.Name) == true)
            {
                MessageBox.Show(GG.boChinaLanguage ? "已经存在的 Recipe." : "이미 존재하는 레시피입니다");
                return;
            }

            if (r.Name == null || r.Name == string.Empty)
            {
                MessageBox.Show(GG.boChinaLanguage ? "请输入名称" : "이름을 입력하세요");
                return;
            }

            PreAlignerRecipeDataMgr.Insert((PreAlignerRecipe)r.Clone());
            UpdateRecipeInfo();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string sel = lblSelectedRecipe.Text;

            if (sel == string.Empty || PreAlignerRecipeDataMgr.IsExist(sel) == false)
            {
                MessageBox.Show(GG.boChinaLanguage ? "请重新选择Recipe." : "레시피를 다시 선택해주세요");
                return;
            }

            PreAlignerRecipe src = (PreAlignerRecipe)pGridSelectedRecipe.SelectedObject;
            MessageBox.Show(src.Update() ? GG.boChinaLanguage ? "成功" : "성공" : GG.boChinaLanguage ? "失败" : "실패");

            UpdateRecipeInfo();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            PreAlignerRecipe src = (PreAlignerRecipe)pGridSelectedRecipe.SelectedObject;

            if (PreAlignerRecipeDataMgr.IsExist(src.Name) == false)
            {
                MessageBox.Show(GG.boChinaLanguage ? "请重新选择Recipe." : "레시피를 다시 선택해주세요");
                return;
            }

            MessageBox.Show(PreAlignerRecipeDataMgr.Delete(src.Name) ? GG.boChinaLanguage ? "成功" : "성공" : GG.boChinaLanguage ? "失败" : "실패");

            UpdateRecipeInfo();
        }

        private void btnFixedRecipe_Click(object sender, EventArgs e)
        {
            if (GG.Equip.UseFixedDitAlignerRecipe)
            {
                GG.Equip.UseFixedDitAlignerRecipe = false;
            }
            else
            {
                var selectedRecipe = lblSelectedRecipe.Text;
                var recp = PreAlignerRecipeDataMgr.GetRecipe(selectedRecipe);
                if (recp == null)
                {
                    MessageBox.Show(GG.boChinaLanguage ? "请重新选择Recipe." : "레시피를 다시 선택해주세요");
                    return;
                }
                GG.Equip.FixedDitAlignerRecipeName = selectedRecipe;
                GG.Equip.UseFixedDitAlignerRecipe = true;
                UpdateRequestAlignerPGrid = true;
                MessageBox.Show(string.Format(GG.boChinaLanguage ? "{0} Recipe 固定" : "{0} 레시피 고정", recp.Name));
            }
        }

        private void btnSetRecipe_Click(object sender, EventArgs e)
        {
            if (GG.Equip.UseSetDitAlignerRecipe)
            {
                GG.Equip.UseSetDitAlignerRecipe = false;
            }
            else
            {
                var selectedRecipe = lblSelectedRecipe.Text;
                var recp = PreAlignerRecipeDataMgr.GetRecipe(selectedRecipe);
                if (recp == null)
                {
                    MessageBox.Show(GG.boChinaLanguage ? "请重新选择Recipe." : "레시피를 다시 선택해주세요");
                    return;
                }
                // 221130 레시피 디폴트 Name 추가 (WaferID에 맞는 Recipe Name 없음)
                GG.Equip.FixedDitAlignerRecipeName = selectedRecipe;
                GG.Equip.UseSetDitAlignerRecipe = true;
                //
                GG.Equip.PreAligner.SetCurRecipe(recp);
                UpdateRequestAlignerPGrid = true;
                MessageBox.Show(GG.boChinaLanguage ? "PreAligner Recipe Setting 完毕" : "PreAligner Recipe Setting 완료");
            }
        }

        private void btnStartNotchROISetting_Click(object sender, EventArgs e)
        {
            var a = MapView.Tool;
            MapView.Tool = new DrawRectShapeTool()
            {
                Ghost = new RectangleShape()
                {
                    Selected = true,
                    LineColor = Color.Green,
                    AllowCopy = false,
                }
            };
        }

        //private void btnAllInit_Click(object sender, EventArgs e)
        //{
        //    if (GG.Equip.PreAligner == null)
        //        GG.Equip.PreAligner.Release();

        //    string err = string.Empty;
        //    GG.Equip.PreAligner = new PreAlignerBundle();
        //    if (GG.Equip.PreAligner.OpenLightController(GG.Equip.InitSetting.LightControllerPort) == false)
        //        err += "Pre Aligner Light Controller Open 실패\n";
        //    if (GG.Equip.PreAligner.ConnectCamera() == false)
        //        err += "Pre Aligner Camera Connect 실패";

        //    InterLockMgr.AddInterLock(err);
        //}

        #region Operator
        private void btnOcrOperate_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;

            if (btn == btnOcrUp)
            {
                GG.Equip.AlignerOcrCylinder.Up();
            }
            else if (btn == btnOcrDown)
            {
                GG.Equip.AlignerOcrCylinder.Down();
            }
        }

        private void btnAlignVacuumOperate_Click(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;

            if (btn == btnAlignVacuumOn)
            {
                GG.Equip.AlignerVac.Vacuum.OnOff(GG.Equip, true);
            }
            else if (btn == btnAlignVacuumOff)
            {
                GG.Equip.AlignerVac.Vacuum.OnOff(GG.Equip, false);
            }
        }

        private void btnAlignBlowerOn_MouseDown(object sender, MouseEventArgs e)
        {
            GG.Equip.AlignerVac.Blower.OnOff(GG.Equip, true);
        }

        private void btnAlignBlowerOn_MouseUp(object sender, MouseEventArgs e)
        {
            GG.Equip.AlignerVac.Blower.OnOff(GG.Equip, false);
        }

        private void btnAlignVacOffBlowerOn_Click(object sender, EventArgs e)
        {
            GG.Equip.AlignerVac.StartOffStep();
        }

        private void btnAlignJogPlus_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            StepMotorEzi _motor;
            float jogSpeed = 0;
            if (btn == btnAlignXJogPlus)
            {
                _motor = GG.Equip.AlignerX;
                float.TryParse(tbAlignXJogSpd.Text, out jogSpeed);
            }
            else if (btn == btnAlignYJogPlus)
            {
                _motor = GG.Equip.AlignerY;
                float.TryParse(tbAlignYJogSpd.Text, out jogSpeed);
            }
            else if (btn == btnAlignTJogPlus)
            {
                _motor = GG.Equip.AlignerT;
                float.TryParse(tbAlignTJogSpd.Text, out jogSpeed);
            }
            else
                return;

            _motor.JogMove(EM_STEP_MOTOR_JOG.JOG_PLUS, jogSpeed);
        }
        private void btnAlignJogMinus_MouseDown(object sender, MouseEventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            StepMotorEzi _motor;
            float jogSpeed = 0;
            if (btn == btnAlignXJogMinus)
            {
                _motor = GG.Equip.AlignerX;
                float.TryParse(tbAlignXJogSpd.Text, out jogSpeed);
            }
            else if (btn == btnAlignYJogMinus)
            {
                _motor = GG.Equip.AlignerY;
                float.TryParse(tbAlignYJogSpd.Text, out jogSpeed);
            }
            else if (btn == btnAlignTJogMinus)
            {
                _motor = GG.Equip.AlignerT;
                float.TryParse(tbAlignTJogSpd.Text, out jogSpeed);
            }
            else
                return;

            _motor.JogMove(EM_STEP_MOTOR_JOG.JOG_MINUS, jogSpeed);
        }
        private void btnAlignJog_MouseUp(object sender, MouseEventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            StepMotorEzi _motor;
            if (btn == btnAlignXJogPlus || btn == btnAlignXJogMinus)
                _motor = GG.Equip.AlignerX;
            else if (btn == btnAlignYJogPlus || btn == btnAlignYJogMinus)
                _motor = GG.Equip.AlignerY;
            else if (btn == btnAlignTJogPlus || btn == btnAlignTJogMinus)
                _motor = GG.Equip.AlignerT;
            else
                return;

            _motor.JogMove(EM_STEP_MOTOR_JOG.JOG_STOP, 1);
        }

        private void btnAlignXHome_DelayClick(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            StepMotorEzi _motor;
            if (btn == btnAlignXHome)
                _motor = GG.Equip.AlignerX;
            else if (btn == btnAlignYHome)
                _motor = GG.Equip.AlignerY;
            else if (btn == btnAlignTHome)
                _motor = GG.Equip.AlignerT;
            else
                return;

            _motor.MoveHome();
        }

        private void btnAlignXPtpMove_DelayClick(object sender, EventArgs e)
        {
            ButtonDelay2 btn = sender as ButtonDelay2;
            StepMotorEzi _motor;
            float posi = 0;
            float speed = 0;
            if (btn == btnAlignXPtpMove)
            {
                ucrlPtpX.GetPos(out posi);
                ucrlPtpX.GetSpd(out speed);
                _motor = GG.Equip.AlignerX;
            }

            else if (btn == btnAlignYPtpMove)
            {
                ucrlPtpY.GetPos(out posi);
                ucrlPtpY.GetSpd(out speed);
                _motor = GG.Equip.AlignerY;
            }

            else if (btn == btnAlignTPtpMove)
            {
                ucrlPtpT.GetPos(out posi);
                ucrlPtpT.GetSpd(out speed);
                _motor = GG.Equip.AlignerT;
            }
            else
                return;

            _motor.MovePosition(posi, speed);
        }

        private void cbPtp_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;

            if (cmb.SelectedIndex < 0)
                return;

            if (cmb == cbAlignX)
            {
                ucrlPtpX.Pos = GG.Equip.AlignerX.Setting.LstServoPosiInfo[cmb.SelectedIndex].Position;
                ucrlPtpX.Spd = GG.Equip.AlignerX.Setting.LstServoPosiInfo[cmb.SelectedIndex].Speed;
                ucrlPtpX.Acc = GG.Equip.AlignerX.Setting.LstServoPosiInfo[cmb.SelectedIndex].Accel;
            }
            else if (cmb == cbAlignY)
            {
                ucrlPtpY.Pos = GG.Equip.AlignerY.Setting.LstServoPosiInfo[cmb.SelectedIndex].Position;
                ucrlPtpY.Spd = GG.Equip.AlignerY.Setting.LstServoPosiInfo[cmb.SelectedIndex].Speed;
                ucrlPtpY.Acc = GG.Equip.AlignerY.Setting.LstServoPosiInfo[cmb.SelectedIndex].Accel;
            }
            else if (cmb == cbAlignT)
            {
                ucrlPtpT.Pos = GG.Equip.AlignerT.Setting.LstServoPosiInfo[cmb.SelectedIndex].Position;
                ucrlPtpT.Spd = GG.Equip.AlignerT.Setting.LstServoPosiInfo[cmb.SelectedIndex].Speed;
                ucrlPtpT.Acc = GG.Equip.AlignerT.Setting.LstServoPosiInfo[cmb.SelectedIndex].Accel;
            }
        }
        #endregion
    }

    class ListviewItemComparer : IComparer
    {
        private int col;
        public string sort = "acs";
        public ListviewItemComparer()
        {
            col = 0;
        }

        public ListviewItemComparer(int column, string sort)
        {
            col = column;
            this.sort = sort;
        }

        public int Compare(object x, object y)
        {
            if (sort == "acs")
            {
                return string.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
            else
            {
                return string.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
            }
        }
    }
}
