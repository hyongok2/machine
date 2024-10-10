using Dit.Rnd.Framework.Grabber;
using Dit.Rnd.Framework.Grabber.Basler;
using EquipMainUi.PreAligner.Recipe;
using EquipMainUi.Struct;
using EquipMainUi.Struct.TransferData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMainUi.PreAligner
{
    public class PreAlignerBundle
    {
        public static string DebugImagePath = Path.Combine(GG.StartupPath, "PreAlign");
        public FrameGrabberBaslerGigE FrameGrabber { get; private set; }
        public LightControllerProxy LightController = new LightControllerProxy();
        public Image SrcImage { get; private set; }
        public PreAlignerRecipe CurRecipe;
        public const int MaxDefectResult = 100;
        public PreAlignDefect[] Defects;
        public CWaferPreAlignerResult AlignParamResult;
        public string WaferID;
        
        public event EventHandler<EventArgs> CameraConnected;
        public event EventHandler<EventArgs> CameraClosed;
        public event EventHandler<FrameGrabbedEventArgs> Grabed;
        public event EventHandler<EventArgs> GrabStarted;
        public event EventHandler<EventArgs> GrabStopped;
        public event EventHandler<EventArgs> ProcessingDone;

        public PreAlignerBundle()
        {
        }   
        public void SetDebugImagePath(string path)
        {
            DebugImagePath = path;
        }
        public string GetDebugImageName(string category, string waferId)
        {
            string name = CurRecipe == null ? "NULL" : CurRecipe.Name;
            string result = CurRecipe == null ? "NULL" : ProcessingResult.ToString();
            return string.Format("{0}_{1}_{2}_{3}_{4}.bmp",
                            DateTime.Now.ToString("yyMMdd_HHmmss"),
                            category,
                            waferId,
                            name,
                            result
                            );
        }
        public bool InitCamera()
        {
            try
            {
                FrameGrabber = new FrameGrabberBaslerGigE();

                FrameGrabber.Grabed += FrameGrabber_Grabed;
                FrameGrabber.CameraConnected += FrameGrabber_CameraConnected;
                FrameGrabber.CameraClosed += FrameGrabber_CameraClosed;
                FrameGrabber.GrabStarted += FrameGrabber_GrabStarted;
                FrameGrabber.GrabStopped += FrameGrabber_GrabStopped;
                FrameGrabber.ImgChannel = EmImageChannel.Ch1;
                FrameGrabber.Buffers.MaxFrameCount = 1;
            }
            catch( Exception ex)
            {
                return false;
            }

            return true;
        }
        public void Release()
        {
            if (FrameGrabber != null)
            {
                if (FrameGrabber.IsGrabbing)
                    FrameGrabber.StopGrab();
                if (FrameGrabber.IsConnected)
                    FrameGrabber.Disconnection();
            }

            if (LightController != null)
            {
                LightController.Release();
            }
        }
        public void LogicWorking()
        {
            if (LightController != null) LightController.LogicWorking();
            if (FrameGrabber != null) ProcessingLogic();
        }

        public bool OpenLightController(string lightCtrlPort)
        {
            LightController.Initialize(lightCtrlPort);
            bool lightRet = LightController.Reopen(lightCtrlPort);
            return lightRet;
        }

        public bool OpenCamera(string camName = "PreAligner01")
        {
            if (FrameGrabber == null) return false;
            if (FrameGrabber.IsConnected) return false;

            bool camRet = FrameGrabber.Connect(new FrameGrabberParam()
            {
                CameraName = camName,
                //GrabberIndex = (int)nudGrabber.Value,
                //ChannelIndex = (int)nudCamera.Value,
                //ConfigFileName = Path.Combine(Application.StartupPath, "VC-25MX.pcf"),
                //SimulAviFileName = @"..\Image\af.avi",
                //SimulFps = 10
            });
            return camRet;
        }
        internal bool SetCurRecipe(PreAlignerRecipe recp)
        {
            CurRecipe = recp;
            if (CurRecipe == null) return false;
            return true;
        }
        public void SetRecipeAndClearResult()
        {
            if (CurRecipe == null) return;

            AlignParamResult = new CWaferPreAlignerResult();
            AlignParamResult.m_bSaveResult         = CurRecipe.SaveResult ? 1 : 0;
            AlignParamResult.m_bSetParam           = CurRecipe.SetParam;
            AlignParamResult.m_bUseGrayScaleMax    = CurRecipe.UseGrayScaleMax ? 1 : 0;
            AlignParamResult.m_nEdgeThreshold      = CurRecipe.EdgeThreshold;
            AlignParamResult.EllipseSizeThreshold  = CurRecipe.EllipseSizeThreshold;
            AlignParamResult.m_dNotchFindThreshold = CurRecipe.NotchFindThreshold;
            AlignParamResult.m_nNotchFindROILeft   = CurRecipe.NotchFindROILeft;
            AlignParamResult.m_nNotchFindROITop    = CurRecipe.NotchFindROITop;
            AlignParamResult.m_nNotchFindROIRight  = CurRecipe.NotchFindROIRight;
            AlignParamResult.m_nNotchFindROIBottom = CurRecipe.NotchFindROIBottom;
            AlignParamResult.m_dRefCenterX         = CurRecipe.RefCenterX;
            AlignParamResult.m_dRefCenterY         = CurRecipe.RefCenterY;
            AlignParamResult.m_dRefDiameter        = CurRecipe.RefDiameter;
            AlignParamResult.m_dRefNotchDegree     = CurRecipe.RefNotchDegree;
            AlignParamResult.m_bUseInspect         = CurRecipe.UseInspect ? 1 : 0;
            AlignParamResult.m_nInspectMargin      = CurRecipe.InspectMargin;
            AlignParamResult.m_nInspectFilterArea  = CurRecipe.InspectFilterArea;
            AlignParamResult.m_dInspectFilterRatio = CurRecipe.InspectFilterRatio;

            Defects = new PreAlignDefect[MaxDefectResult];
        }
        public void SetImage(Image img)
        {
            if (SrcImage != null)
            {
                SrcImage.Dispose();
                SrcImage = null;
            }
            SrcImage = img;
        }
        public void Start(string waferId)
        {
            WaferID = waferId;
            IsProcessingOvertime = false;
            IsLightControllDone = false;
            IsProcessingDone = false;
            IsProcessingSuccess = false;
            _grabRetry = false;

            ProcessingStartTime = DateTime.Now;
            _step = 10;
        }
        public void Stop()
        {
            _step = 0;
        }
        private bool _grabComplete = false;
        private int _oldStep = 0;        
        private int _step = 0;
        public int Step => _step;
        public bool IsRunning => _step != 0;
        public bool IsProcessingOvertime { get; private set; }
        public bool IsLightControllDone { get; private set; }        
        public bool IsProcessingDone { get; private set; }
        public bool IsProcessingSuccess { get; private set; }
        public WaferPreAlignerResultCode ProcessingResult { get; private set; }
        public bool IsCamReady => FrameGrabber != null && FrameGrabber.IsConnected == true;
        public bool IsLightReady => LightController != null && LightController.IsInitDone == true && LightController.IsOpen;
        public bool IsError
        {
            get
            {
                return IsCamReady == false
                    || IsLightReady == false
                    ;
            }
        }

        private bool _grabRetry = false;
        private int ProcessingOvertime = 30000;
        private DateTime ProcessingStartTime = DateTime.Now;
        private void ProcessingLogic()
        {
            if (_step > 0 && (DateTime.Now - ProcessingStartTime).TotalMilliseconds > ProcessingOvertime)
            {
                IsProcessingOvertime = true;
                IsProcessingDone = true;
                Stop();
                Logger.Log.AppendLine(LogLevel.Error, "PreAligner Timeover : StopGrab({0}), LightOff({1}), SaveOn({2}), ImgSave({3}), ImgProc({4})",
                    _stopGrabOk,
                    _lightOffOk,
                    CurRecipe.SaveResult,
                    _imgSaveOk ,
                    _imgProcOk);                    
            }

            if (_step != _oldStep)
            {
                Logger.Log.AppendLine(LogLevel.NoLog, "[SEQ] {0} Step Changed {2}\t(<-{1})", "PreAligner Processing", _oldStep, _step);
                _oldStep = _step;
            }

            switch (_step)
            {
                case 0:
                    break;
                case 10:
                    if (FrameGrabber.IsGrabbing)
                        FrameGrabber.StopGrab();
                    LightController.Remote();
                    _step = 20;
                    break;
                case 20:
                    if (LightController.IsRemote)
                    {
                        LightController.On(CurRecipe.Bright);
                        _step = 30;
                    }
                    break;
                case 30:
                    if (LightController.IsOn)
                    {
                        try
                        {
                            FrameGrabber.ExposureTime = CurRecipe.ExposureTime;
                            IsLightControllDone = true;
                            _step = 40;
                        }
                        catch (Exception ex)
                        {
                            ProcessingResult = WaferPreAlignerResultCode.WaferPreAlignerResult_ImageNull;
                            _step = 100;
                        }                        
                    }
                    break;
                case 40:
                    if (FrameGrabber.IsExposureTimeSynced)
                    {
                        _grabComplete = false;
                        FrameGrabber.Snap();

                        _stopGrabOk = false;
                        _lightOffOk = false;
                        _imgSaveOk = false;
                        _imgProcOk = false;
                        _step = 50;
                    }
                    break;
                case 50:
                    isfist = true;
                    if (_grabRetry == false 
                        && (DateTime.Now - ProcessingStartTime).TotalMilliseconds > ProcessingOvertime / 3)
                    {
                        Logger.Log.AppendLine(LogLevel.Error, "PreAligner Timeover-Retry : StopGrab({0}), LightOff({1}), SaveOn({2}), ImgSave({3}), ImgProc({4})",
                            _stopGrabOk,
                            _lightOffOk,
                            CurRecipe.SaveResult,
                            _imgSaveOk,
                            _imgProcOk);

                        _grabRetry = true;
                        ProcessingStartTime = DateTime.Now;
                        _step = 10;
                    }

                    if (_grabComplete)
                    {
                        FrameGrabber.StopGrab();
                        _stopGrabOk = true;
                        LightController.Off();
                        _lightOffOk = true;
                        if (CurRecipe.SaveResult)
                        {
                            _imgSaveOk = false;
                            SaveRawImg(WaferID);
                            _imgSaveOk = true;
                        }
                        else
                            Logger.Log.AppendLine(LogLevel.NoLog, "PreAlign Raw Image Save OFF");
                        ImageProcessingUpdate();
                        if (ProcessingResult == WaferPreAlignerResultCode.WaferPreAlignerResult_PtrNull)
                        {
                            Delay(1500);
                        }
                        _imgProcOk = false;
                        _step = 100;
                    }
                    break;
                case 100:
                    IsProcessingDone = true;
                    _step = 0;
                    break;

            }
        }

        private void FrameGrabber_GrabStopped(object sender, EventArgs e)
        {
            GrabStopped?.Invoke(sender, e);
        }

        private void FrameGrabber_GrabStarted(object sender, EventArgs e)
        {
            GrabStarted?.Invoke(sender, e);
        }

        private void FrameGrabber_CameraClosed(object sender, EventArgs e)
        {
            CameraClosed?.Invoke(sender, e);
        }

        private void FrameGrabber_CameraConnected(object sender, EventArgs e)
        {
            CameraConnected?.Invoke(sender, e);
        }

        private void FrameGrabber_Grabed(object sender, FrameGrabbedEventArgs e)
        {            
            SetBufferImage();
            Grabed?.Invoke(sender, e);            
            _grabComplete = true;
        }
        private Image GetBufferImage()
        {
            MatEx mat = GetBufferMat();
            if (mat == null) return null;
            Image img = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);
            mat.Dispose();
            return img;
        }
        private MatEx GetBufferMat()
        {
            MatEx mat = FrameGrabber.Buffers.Dequeue(EmDequeueMode.First);
            if (mat == null) return null;
            return mat;
        }
        public bool SetBufferImage()
        {
            Image img = GetBufferImage();
            if (img == null) return false;

            SrcImage = img;
            return true;
        }
        public void ImageProcessingUpdate()
        {
            var ret = ImageProcessing();
            ProcessingResult = ret;
            if (ProcessingResult == WaferPreAlignerResultCode.WaferPreAlignerResult_PtrNull)
                return;
            IsProcessingSuccess = ProcessingResult == WaferPreAlignerResultCode.WaferPreAlignerResult_Success;
            ProcessingDone?.Invoke(this, null);
        }
        private Stopwatch _processingTimer = new Stopwatch();
        private bool _stopGrabOk;
        private bool _lightOffOk;
        private bool _imgSaveOk;
        private bool _imgProcOk;

        bool isfist = true;
        public int ProcessingTime => (int)_processingTimer.ElapsedMilliseconds;
        private WaferPreAlignerResultCode ImageProcessing()
        {
            if (SrcImage == null)
            {
                return WaferPreAlignerResultCode.WaferPreAlignerResult_ImageNull;
            }
            if (CurRecipe == null)
            {
                return WaferPreAlignerResultCode.WaferPreAlignerResult_NoRecipe;
            }

            Image img = null;
            try
            {
                lock (SrcImage)
                {
                    img = SrcImage.Clone() as Image;
                }

                Bitmap bmp;
                _processingTimer.Restart();
                bmp = (Bitmap)img;
                BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
                IntPtr ptrImage = bmpData.Scan0;
                SetRecipeAndClearResult();
                WaferPreAlignerResultCode ret = (WaferPreAlignerResultCode)CWaferPreAlignerWrapper.PreAlignProcess(
                    ptrImage, bmp.Width, bmp.Height, AlignParamResult, ref Defects[0]);
                AlignParamResult.m_dDTheta = AlignParamResult.m_dNotchDegree - AlignParamResult.m_dRefNotchDegree;
                Logger.Log.AppendLine(LogLevel.Info, "PreAligner 처리 성공 (DLL Data), {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}",
                                      "DX", "DY", "DTheta", "CenterX", "CenterY", "MajorLength", "MinorLength", "NotchX", "NotchY", "NotchDegree");
                Logger.Log.AppendLine(LogLevel.Info, "PreAligner 처리 성공 (DLL Data), {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}",
                    AlignParamResult.DX,
                    AlignParamResult.DY,
                    AlignParamResult.DTheta,
                    AlignParamResult.CenterX,
                    AlignParamResult.CenterY,
                    AlignParamResult.MajorLength,
                    AlignParamResult.MinorLength,
                    AlignParamResult.NotchX,
                    AlignParamResult.NotchY,
                    AlignParamResult.NotchDegree
                    );
                Logger.Log.AppendLine(LogLevel.Info, "PreAligner 노치 탐색 점수 : {0}", AlignParamResult.NotchMatchScore.ToString("F2"));
                if (AlignParamResult.UseInspect)
                {
                    if (AlignParamResult.DefectCount > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("Idx, Type, PosX, PosY, Area, Left, Top, Right, Bottom\n");
                        for (int i = 0; i < AlignParamResult.DefectCount; ++i)
                        {
                            if (i >= Defects.Length) break;
                            var defect = Defects[i];
                            sb.AppendFormat("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}\n",
                                defect.nIdx, defect.nType, defect.dPosX, defect.dPosY, defect.nArea, defect.nLeft, defect.nTop, defect.nRight, defect.nBottom);
                        }
                        Logger.Log.AppendLine(LogLevel.Info, "PreAligner 검사 성공 (DLL Data)\n{0}", sb.ToString());
                    }
                    else
                        Logger.Log.AppendLine(LogLevel.Info, "PreAligner 검사 결함 0개");
                }
                else
                    Logger.Log.AppendLine(LogLevel.Info, "PreAligner 검사 미사용");

                _processingTimer.Stop();
                bmp.UnlockBits(bmpData);
                img.Dispose();
                return ret;
            }
            catch (Exception ex)
            {
                if(isfist)
                {
                    isfist = false;
                    Logger.Log.AppendLine(LogLevel.Info, "PreAligner SW 개체참조 1회 버그 발생 후 재시도");
                    Delay(1000);
                    ImageProcessingUpdate();
                    return WaferPreAlignerResultCode.WaferPreAlignerResult_PtrNull;
                }
                AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0902_PRE_ALIGNER_IMAGE_PROCESS_FAIL);
                Console.WriteLine(ex.StackTrace);
                img.Dispose();
                _processingTimer.Stop();
                return WaferPreAlignerResultCode.WaferPreAlignerResult_Exception;
            }
        }
        private DateTime Delay(int millsec)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, millsec);
            DateTime AfterWards = ThisMoment.Add(duration);
            while(AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }
        public void StartLive()
        {
            FrameGrabber.StartGrab();            
        }

        public void StopLive()
        {
            //kkt::EFEM Reset 명령시 호출 되면서 예외발생하여 임시로 처리
            if (GG.TestMode)
                return;
            FrameGrabber.StopGrab();            
        }

        private void SaveRawImg(string waferId)
        {
            if (waferId == "************")
                waferId = "ReadError";
            System.Threading.Thread t = new System.Threading.Thread(() =>
            {
                string destDir = Path.Combine(PreAlignerBundle.DebugImagePath);

                if (Directory.Exists(destDir) == false)
                    Directory.CreateDirectory(destDir);

                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(Path.Combine(DebugImagePath, GetDebugImageName("RawImage", waferId)), FileMode.Create, FileAccess.ReadWrite))
                    {
                        using (var srcImage = GG.Equip.PreAligner.SrcImage.Clone() as Image)
                        {
                            srcImage.Save(memory, ImageFormat.Bmp);
                            byte[] bytes = memory.ToArray();
                            fs.Write(bytes, 0, bytes.Length);
                        }
                            
                    }
                }
            });
            t.Start();
        }
        public void SaveResultImage(bool resultOnly, string waferId)
        {
            System.Threading.Thread t = new System.Threading.Thread(() =>
            {
                try
                {
                    string[] imgs = new string[] { "ResultImage", "BlobImage", "BinaryImage", "EdgeImage" };
                    foreach (string img in imgs)
                    {
                        string src = Path.Combine(System.Windows.Forms.Application.StartupPath, string.Format("{0}.bmp", img));
                        string dest = Path.Combine(PreAlignerBundle.DebugImagePath,
                            GetDebugImageName(img, waferId)
                            );
                        string destDir = Path.GetDirectoryName(dest);

                        if (File.Exists(src))
                        {
                            if (Directory.Exists(destDir) == false)
                                Directory.CreateDirectory(Path.GetDirectoryName(dest));

                            File.Copy(src, dest);
                        }
                        Logger.Log.AppendLine(LogLevel.Info, "{0} : Log 폴더로 복사 완료 후 삭제", GetDebugImageName(img, waferId));
                        File.Delete(src);

                        DeleteOldFile(destDir);

                        if (resultOnly)
                            return;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log.AppendLine(LogLevel.NoLog, "PreAligner 결과 저장 실패");
                }
            });

            t.Start();
        }

        private static void DeleteOldFile(string destDir)
        {
            int fileCountLimit = 1000;
            int deleteCount = 10;

            if (Directory.Exists(destDir))
            {
                var files = Directory.GetFiles(destDir);                

                if (files.Count() > fileCountLimit)
                {
                    List<string> deleteTarget = new List<string>();
                    int deleted = 0;

                    foreach (var path in files)
                    {
                        if (deleted < deleteCount)
                        {
                            FileInfo f = new FileInfo(path);
                            try
                            {
                                f.Delete();
                            }
                            catch (Exception ex)
                            {

                            }
                            deleted++;
                        }
                        else
                            return;
                    }
                }
            }
        }
    }
}
