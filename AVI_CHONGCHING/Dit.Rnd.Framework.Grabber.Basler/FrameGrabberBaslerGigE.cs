
using Basler.Pylon;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dit.Rnd.Framework.Grabber.Basler
{
    public class FrameGrabberBaslerGigE : FrameGrabber
    {
        public const int ExposureTimeMax = 100000;
        public const int ExposureTimeMin = 35;
        public string Name { get; set; }
        private Camera _camera = null;
        private PixelDataConverter _converter = null;
        private Stopwatch _stopWatch = new Stopwatch();
        private Thread _worker;
        private bool _running = false;
        private double _exposureTime;
        public override double ExposureTime
        {
            get
            {
                double exposureTime = _camera.Parameters[PLCamera.ExposureTimeRaw].GetValue();
                return exposureTime;
            }
            set
            {
                //Determine the current exposure time
                //double d = _camera.Parameters[PLCamera.ExposureTime].GetValue();
                //Set the exposure time mode to Standard
                //Note: Available on selected camera models only
                //_camera.Parameters[PLCamera.ExposureTimeMode].SetValue(PLCamera.ExposureTimeMode.Standard);
                //Set the exposure time to 3500 microseconds 
                if (value < ExposureTimeMin)
                    value = ExposureTimeMin;

                if (value > ExposureTimeMax)
                    value = ExposureTimeMax;

                _exposureTime = value = (int)(value / 35) * 35;

                //ExposureTimeEx = value; 
                //_camera.Parameters[PLCamera.Gain].SetValue((long)value);
                bool rr = _camera.Parameters[PLGigECamera.ExposureTimeRaw].TrySetValue((long)value);
                Console.WriteLine(string.Format("Set ExposureTime {0}:{1}", value, rr));
            }
        }
        public override bool IsExposureTimeSynced => _exposureTime == ExposureTime;
        public override double AnalogGain
        {
            get
            {
                double gain = _camera.Parameters[PLCamera.GainRaw].GetValue();
                return gain;
            }
            set
            {
                try
                {
                    if (value < 150)
                        value = 150;

                    if (value > 500)
                        value = 500;


                    //     _camera.Parameters[PLCamera.GainAbs].SetValue((long)value);
                    //double gain = _camera.Parameters[PLCamera.Gain].GetValue();
                    //bool result = _camera.Parameters[PLCamera.GainRaw].TrySetValue((long)value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("");
                }
            }
        }

        //메소드 - 시작 종료
        public override bool Connect(FrameGrabberParam param)
        {
            List<ICameraInfo> allCameras = CameraFinder.Enumerate();
            try
            {
                //var iCam = allCameras.FirstOrDefault(f => f["UserDefinedName"].ToUpper() == param.CameraName.ToUpper());
                //var iCam = allCameras.FirstOrDefault(f => f["SerialNumber"].ToUpper() == param.CameraName.ToUpper());

                var iCam = allCameras.FirstOrDefault(f =>
                {
                    if (f.ContainsKey("SerialNumber") == true)
                        if (f["SerialNumber"].ToUpper() == param.CameraName.ToUpper())
                            return true;
                    if (f.ContainsKey("UserDefinedName") == true)
                        if (f["UserDefinedName"].ToUpper() == param.CameraName.ToUpper())
                            return true;

                    return false;
                });

                Name = param.CameraName;

                if (iCam == null) return false;

                // Create a new camera object.
                _camera = new Camera(iCam);
                _converter = new PixelDataConverter();
                _camera.CameraOpened += Configuration.AcquireContinuous;

                // Register for the events of the image provider needed for proper operation.
                _camera.ConnectionLost += OnConnectionLost;
                _camera.CameraOpened += OnCameraOpened;
                _camera.CameraClosed += OnCameraClosed;

                //_camera.StreamGrabber.GrabStarted += OnGrabStarted;
                ////_camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                //_camera.StreamGrabber.GrabStopped += OnGrabStopped;
                _camera.Open();

                _camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(5);
                //_camera.Parameters[PLGigECamera.ExposureTimeRaw].SetValue((long)10);



                _running = true;
                _worker = new Thread(new ThreadStart(Working));
                _worker.Start();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;

            }
            return true;
        }

        private void OnConnectionLost(object sender, EventArgs e)
        {
        }
        private void OnCameraOpened(object sender, EventArgs e)
        {
            RasizeGrabedCameraConnected(e);
        }
        private void OnCameraClosed(object sender, EventArgs e)
        {
            RasizeGrabedCameraClosed(e);
        }
        private void OnGrabStarted(object sender, EventArgs e)
        {
            RasizeGrabedGrabStarted(e);
        }


        Mat _grapImg = null;
        private void OnGrabStopped(object sender, GrabStopEventArgs e)
        {
            RasizeGrabedGrabStopped(e);
        }

        public override bool Disconnection()
        {
            // Destroy the camera object.
            try
            {
                if (_camera != null)
                {
                    _running = false;

                    if (_camera.StreamGrabber.IsGrabbing == true)
                    {
                        _camera.StreamGrabber.Stop();
                    }

                    _camera.Close();
                    _camera.Dispose();
                    _camera = null;
                }
            }
            catch (Exception exception)
            {
                return false;
            }

            // Destroy the converter object.
            if (_converter != null)
            {
                _converter.Dispose();
                _converter = null;
            }

            return true;
        }

        public override bool StartGrab()
        {
            try
            {

                // Enable time stamp chunks.
                bool result = false;
                //result = _camera.Parameters[PLGigECamera.ChunkModeActive].TrySetValue(true);
                //result = _camera.Parameters[PLGigECamera.ChunkSelector].TrySetValue(PLCamera.ChunkSelector.ExposureTime);
                //result = _camera.Parameters[PLGigECamera.ChunkEnable].TrySetValue(true);

                //_camera.Parameters[PLCamera.ChunkSelector].SetValue(PLCamera.ChunkSelector.ExposureTime);
                //_camera.Parameters[PLCamera.ChunkEnable].SetValue(true);


                // Start the grabbing of images until grabbing is stopped.
                _camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);

                //_camera.Parameters[PLCamera.TriggerSelector].SetValue(PLCamera.TriggerSelector.FrameStart);
                //_camera.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.On);
                //_camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);

                _camera.StreamGrabber.Start();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public override bool StopGrab()
        {
            // Stop the grabbing.
            try
            {
                _camera.StreamGrabber.Stop();
                OnGrabStopped(null, null);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public override bool Snap(int count = 1)
        {

            try
            {
                // Starts the grabbing of one image.
                _camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
                //_camera.StreamGrabber.Start(count, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber); 
                _camera.StreamGrabber.Start();// count, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public override void Dispose()
        {
            Disconnection();
        }
        public override bool IsConnected
        {
            get
            {
                if (_camera == null) return false;
                return _camera.IsOpen;
            }
        }
        public override bool IsGrabbing
        {
            get
            {
                if (_camera == null) return false;
                if (_camera.StreamGrabber == null) return false;
                return _camera.StreamGrabber.IsGrabbing;
            }
        }
        public override void ExecuteSoftwareTrigger()
        {
            _camera.ExecuteSoftwareTrigger();
        }

        public bool SetOutLine(int line, bool value)
        {
            if (_camera == null) return false;
            if (_camera.IsConnected == false) return false;
            if (_camera.IsOpen == false) return false;

            //// Select Line 2 (output line)
            //_camera.Parameters[PLCamera.LineSelector].SetValue(PLCamera.LineSelector.Line2);

            //// Set the source signal to User Output 1
            //_camera.Parameters[PLCamera.LineSource].SetValue(PLCamera.LineSource.UserOutput1);

            //// Select the User Output 1 signal
            //_camera.Parameters[PLCamera.UserOutputSelector].SetValue(PLCamera.UserOutputSelector.UserOutput1);

            // Set the User Output Value for the User Output 1 signal to true.
            // Because User Output 1 is set as the source signal for Line 2,
            // the status of Line 2 is set to high.
            _camera.Parameters[PLCamera.UserOutputValue].SetValue(value);
            return true;
        }
        public void Working()
        {
            int exposureTime = 0;
            while (_running)
            {
                try
                {
                    IGrabResult grabResult = _camera.StreamGrabber.RetrieveResult(100, TimeoutHandling.Return);
                    if (grabResult == null) continue;
                    using (grabResult)
                    {
                        if (grabResult.GrabSucceeded)
                        {

                            // Check if the image can be displayed.
                            if (grabResult.IsValid)
                            {
                                _stopWatch.Restart();
                                //ImgChannel = EmImageChannel.Ch1;

                                if (_grapImg == null)
                                {
                                    _grapImg = new Mat(new OpenCvSharp.Size(grabResult.Width, grabResult.Height),
                                        ImgChannel == EmImageChannel.Ch1 ? MatType.CV_8UC1 : MatType.CV_8UC4);

                                }
                                if (_grapImg.CvPtr == IntPtr.Zero)
                                {
                                    Console.WriteLine("fail init mat");
                                    return;
                                }

                                //Place the pointer to the buffer of the bitmap.
                                _converter.OutputPixelFormat = ImgChannel == EmImageChannel.Ch1 ? PixelType.Mono8 : PixelType.BGRA8packed;  // grabResult.PixelTypeValue;                    
                                _converter.Convert(_grapImg.Data, _grapImg.Step(0) * _grapImg.Height, grabResult);


                                if (grabResult.ChunkData[PLChunkData.ChunkExposureTime].IsReadable)
                                {
                                    exposureTime = (int)grabResult.ChunkData[PLChunkData.ChunkExposureTime].GetValue();
                                    //Console.WriteLine("ChunkData: ChunkExposureTime = {0}", exposureTime);
                                }
                                else
                                {
                                    //Console.WriteLine("ChunkData: No TimeStamp");
                                }

                                //if (true)
                                //{
                                //    Mat matrix = Cv2.GetRotationMatrix2D(new Point2f(_grapImg.Width / 2, _grapImg.Height / 2), 28.5, 1);
                                //    Mat dst = _grapImg.WarpAffine(matrix, new OpenCvSharp.Size(_grapImg.Width, _grapImg.Height));

                                //    Cv2.ImShow("TEST", _grapImg);
                                //    Cv2.WaitKey();
                                //    matrix.Dispose();
                                //    _grapImg.Dispose();
                                //    _grapImg = dst;

                                //}
                                ////if (this.Name == "Revi")
                                ////{

                                ////    Mat matrix = Cv2.GetRotationMatrix2D(new Point2f(_grapImg.Width / 2, _grapImg.Height / 2), -31, 1);
                                ////    Mat dst = _grapImg.WarpAffine(matrix, new OpenCvSharp.Size(_grapImg.Width, _grapImg.Height));
                                ////    matrix.Dispose();
                                ////    _grapImg.Dispose();
                                ////    _grapImg = dst;
                                ////}
                                //else if (this.Name == "Revi")
                                //{

                                //    Mat matrix = Cv2.GetRotationMatrix2D(new Point2f(_grapImg.Width / 2, _grapImg.Height / 2), 4, 1);
                                //    Mat dst = _grapImg.WarpAffine(matrix, new OpenCvSharp.Size(_grapImg.Width, _grapImg.Height));
                                //    matrix.Dispose();
                                //    _grapImg.Dispose();
                                //    _grapImg = dst;
                                //}

                                //포인터 전달은 고민 필요함. 
                                FrameGrabbedEventArgs args = new FrameGrabbedEventArgs()
                                {
                                    Image = ImgChannel == EmImageChannel.Ch1 ? _grapImg.Clone() : _grapImg.CvtColor(ColorConversionCodes.BGRA2RGB),
                                    Width = grabResult.Width,
                                    Height = grabResult.Height,
                                    Channel = grabResult.PixelTypeValue == PixelType.Mono8 ? 1 :
                                               grabResult.PixelTypeValue == PixelType.RGB8packed ? 3 : 4,
                                    ExposureTime = exposureTime,
                                };

                                RasizeGrabed(args);
                            }

                        }
                        else
                        {
                            Console.WriteLine("Error: {0} {1}", grabResult.ErrorCode, grabResult.ErrorDescription);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //Thread.Sleep(1);
            }
        }
    }
}
