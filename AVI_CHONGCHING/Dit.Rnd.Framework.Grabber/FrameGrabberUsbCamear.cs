
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

namespace Dit.Rnd.Framework.Grabber
{
    public class FrameGrabberUsbCamear : FrameGrabber
    {
        private VideoCapture _camera = null;
        private Stopwatch _stopWatch = new Stopwatch();
        private Thread _worker;
        private bool _running = false;
        public override double ExposureTime
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }
        public override double AnalogGain
        {
            get
            {
                return 0;
            }
            set
            {

            }
        }
        private bool _isGrab = false;
        private bool _isSnap = false;
        private int _snapCount = 0;
        //메소드 - 시작 종료
        public override bool Connect(FrameGrabberParam param)
        {
            try
            {
                _camera = new VideoCapture(0);
                _camera.FrameWidth = param.Width;
                _camera.FrameHeight = param.Height;

                _running = true;
                _worker = new Thread(new ThreadStart(Working));
                _worker.Start();

                RasizeGrabedCameraConnected(new EventArgs());

            }
            catch (Exception e)
            {

            }
            return true;
        }
        public override bool Disconnection()
        {
            try
            {
                _camera.Dispose();
                RasizeGrabedCameraClosed(new EventArgs());
            }
            catch (Exception exception)
            {
                return false;
            }
            return true;
        }
        public override bool StartGrab()
        {
            try
            {
                _isGrab = true;
                RasizeGrabedGrabStarted(new EventArgs());
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
                _isGrab = false;
                RasizeGrabedGrabStopped(new EventArgs());
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

                _snapCount = count;
                _isGrab = true;
                _isSnap = true;
                RasizeGrabedGrabStarted(new EventArgs());
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
                return true;
            }
        }
        public override bool IsGrabbing
        {
            get
            {
                return _isGrab;
            }
        }
        public override void ExecuteSoftwareTrigger()
        {
            throw new NotImplementedException();
        }
        public override bool SetOutLine(int line, bool value)
        {
            throw new NotImplementedException();
        }
        public void Working()
        {
            Mat frame = new Mat();
            while (_running)
            {
                try
                {
                    if (_isGrab)
                    {
                        if (_isSnap)
                        {
                            if (_snapCount < 0) _isSnap = false;
                            _snapCount--;
                        }

                        _camera.Read(frame);
                        Mat grayFrame = frame.CvtColor(ColorConversionCodes.RGB2GRAY);
                        Mat colorFrame = grayFrame.CvtColor(ColorConversionCodes.GRAY2RGB);

                        //포인터 전달은 고민 필요함. 
                        FrameGrabbedEventArgs args = new FrameGrabbedEventArgs()
                        {
                            Image = colorFrame.Clone(),
                            Width = frame.Width,
                            Height = frame.Height,
                            Channel = 3,
                        };

                        RasizeGrabed(args);

                        colorFrame.Dispose();
                        grayFrame.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Thread.Sleep(1);
            }
        }
    }
}
