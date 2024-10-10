
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
using System.Threading.Tasks;

namespace Dit.Rnd.Framework.Grabber.Basler
{
    public class FrameGrabberBaslerGigE2 : FrameGrabber
    {
        private Camera _camera = null;
        private PixelDataConverter _converter = null;
        private Stopwatch _stopWatch = new Stopwatch();


        public override double ExposureTime
        {
            get
            {
                //uble exposureTime = _camera.Parameters[PLCamera.ExposureTimeRaw].GetValue();
                return 0;
            }
            set
            {

                // Determine the current exposure time
                //double d = _camera.Parameters[PLCamera.ExposureTimeRaw].GetValue();
                // Set the exposure time mode to Standard
                // Note: Available on selected camera models only
                //amera.Parameters[PLCamera.ExposureTimeMode].SetValue(PLCamera.ExposureTimeMode.Standard);
                // Set the exposure time to 3500 microseconds
                //amera.Parameters[PLCamera.ExposureTimeRaw].SetValue((long) 3500);
            }
        }
        public override double AnalogGain
        {
            get
            {
                double gain = _camera.Parameters[PLCamera.GainRaw].GetValue();
                return gain;
            }
            set
            {
                _camera.Parameters[PLCamera.GainRaw].SetValue((long)value);
            }
        }

        //메소드 - 시작 종료
        public override bool Connect(FrameGrabberParam param)
        {
            List<ICameraInfo> allCameras = CameraFinder.Enumerate();
            try
            {
                var iCam = allCameras.FirstOrDefault(f => f["UserDefinedName"].ToUpper() == param.CameraName.ToUpper());

                if (iCam == null) return false;

                // Create a new camera object.
                _camera = new Camera(iCam);
                _converter = new PixelDataConverter();
                _camera.CameraOpened += Configuration.AcquireContinuous;

                // Register for the events of the image provider needed for proper operation.
                _camera.ConnectionLost += OnConnectionLost;
                _camera.CameraOpened += OnCameraOpened;
                _camera.CameraClosed += OnCameraClosed;

                _camera.StreamGrabber.GrabStarted += OnGrabStarted;
                _camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                _camera.StreamGrabber.GrabStopped += OnGrabStopped;

                
                _camera.Open();
            }
            catch (Exception e)
            {

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
        private void OnImageGrabbed(object sender, ImageGrabbedEventArgs e)
        {
            try
            {
                // Get the grab result.
                IGrabResult grabResult = e.GrabResult;

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

                    //Place the pointer to the buffer of the bitmap.
                    _converter.OutputPixelFormat = ImgChannel == EmImageChannel.Ch1 ? PixelType.Mono8 : PixelType.BGRA8packed;  // grabResult.PixelTypeValue;                    
                    _converter.Convert(_grapImg.Data, _grapImg.Step(0) * _grapImg.Height, grabResult);


                    _grapImg.SaveImage(@"D:\test.png");

                    //Cv2.ImShow("asdf", _grapImg);
                    //Cv2.WaitKey(10);

                    if (FilterMat == null)
                    {
                        Mat gtframeRowAvg = _grapImg.Reduce(ReduceDimension.Row, ReduceTypes.Avg, -1);
                        int left = (int)(gtframeRowAvg[0, 1, 0, 100].Sum().Val0 / 100);
                        int right = (int)(gtframeRowAvg[0, 1, gtframeRowAvg.Width - 100, gtframeRowAvg.Width].Sum().Val0 / 100);
                        int span = left - right;
                        FilterMat = new Mat(new OpenCvSharp.Size(grabResult.Width, grabResult.Height), MatType.CV_8UC1);
                        int value = 0;
                        for (int iCol = 0; iCol < grabResult.Width; iCol++)
                        {
                            value = span * iCol / grabResult.Width;
                            for (int iRow = 0; iRow < grabResult.Height; iRow++)
                            {
                                FilterMat.Set<byte>(iRow, iCol, (byte)value);
                            }
                        }
                    }

                    Mat vv = _grapImg;// + FilterMat;
                    //_grapImg.Dispose();

                    //포인터 전달은 고민 필요함. 
                    FrameGrabbedEventArgs args = new FrameGrabbedEventArgs()
                    {
                        Image = ImgChannel == EmImageChannel.Ch1 ? vv.Clone() : vv.CvtColor(ColorConversionCodes.BGRA2RGB),
                        Width = grabResult.Width,
                        Height = grabResult.Height,
                        Channel = grabResult.PixelTypeValue == PixelType.Mono8 ? 1 :
                                   grabResult.PixelTypeValue == PixelType.RGB8packed ? 3 : 4,
                    };

                    //args.Image.SaveImage(@"D:\test.png");

                    // vv.Dispose();
                    RasizeGrabed(args);
                }
            }
            catch (Exception exception)
            {
            }
            finally
            {
                // Dispose the grab result if needed for returning it to the grab loop.
                e.DisposeGrabResultIfClone();
            }
        }
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
                // Start the grabbing of images until grabbing is stopped.
                _camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);

                //_camera.Parameters[PLCamera.TriggerSelector].SetValue(PLCamera.TriggerSelector.FrameStart);
                //_camera.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.On);
                _camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
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
                _camera.StreamGrabber.Start(count, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
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
        public override bool Connected
        {
            get
            {
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
    }
}

