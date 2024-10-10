using Dit.Rnd.Framework.Log;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dit.Rnd.Framework.Grabber
{
    public enum EmImageChannel
    {
        Ch1 = 1,
        Ch3 = 3,
        Ch4 = 4,
    }

    public class FrameGrabber : IDisposable
    {
        //프로퍼티


        public virtual bool _isException { get; set; } = false;
        public virtual bool IsStarted { get; set; } = false;
        public virtual bool IsConnected { get; set; } = false;
        public virtual bool IsGrabbing { get; set; } = false;
        public virtual int FrameIndex { get; set; } = 0;
        public virtual int FrameCount { get; set; } = 0;

        public virtual int TriggerMode { get; set; } = 0;
        public virtual int BinningMode { get; set; } = 0;
        public virtual int TriggerSource { get; set; } = 0;
        public virtual int ScanDirection { get; set; } = 0;
        public virtual double ExposureTime { get; set; } = 0;
        public virtual double ExposureTimeEx { get; set; } = 0;
        public virtual bool IsExposureTimeSynced { get; set; } = false;
        public virtual double FrameRate { get; set; } = 0;
        public virtual double AnalogGain { get; set; } = 0;

        public virtual double FrameWidth { get; set; } = 0;
        public virtual double FrameHeight { get; set; } = 0;

        public Timer _tmrConnector;
        public ILogger Logger { get; set; }
        public FrameBuffer Buffers { get; set; }
        public FrameGrabberParam GrabberParam { get; set; }
        public FrameGrabberStatus GrabberStatus { get; set; }

        public EmImageChannel ImgChannel { get; set; }
        public RotateFlipType Rotate { get; set; }

        private DateTime _intervalTime { get; set; }
        private int _recvCount { get; set; }
        public double FPS { get; set; }
        public int ImgGrabCount { get; set; }

        public FrameGrabber()
        {
            Logger = new ILogger();
            Buffers = new FrameBuffer();
            ImgChannel = EmImageChannel.Ch1;

            _tmrConnector = new Timer(CheckConnectStatus);
        }

        //이벤트 - 
        public event EventHandler<EventArgs> CameraConnected;
        public event EventHandler<EventArgs> CameraClosed;
        public event EventHandler<EventArgs> GrabStarted;
        public event EventHandler<EventArgs> GrabStopped;
        public event EventHandler<FrameGrabbedEventArgs> Grabed;


        //메소드 - 시작 종료
        public virtual bool Connect(FrameGrabberParam param)
        {
            _isException = false;
            IsStarted = true;
            _tmrConnector.Change(1000, 1000);
            return true;
        }
        public virtual bool Disconnection()
        {
            _tmrConnector.Change(0, 0);

            IsStarted = false;
            return false;
        }

        public virtual bool StartGrab()
        {
            return false;
        }
        public virtual bool StopGrab()
        {
            return false;
        }
        public virtual bool Snap(int count = 1)
        {
            return false;
        }
        protected void RasizeGrabedCameraConnected(EventArgs args)
        {
            if (CameraConnected != null) CameraConnected(this, args);
        }
        protected void RasizeGrabedCameraClosed(EventArgs args)
        {
            if (CameraClosed != null) CameraClosed(this, args);
        }
        protected void RasizeGrabedGrabStarted(EventArgs args)
        {
            if (GrabStarted != null) GrabStarted(this, args);
        }
        protected void RasizeGrabedGrabStopped(EventArgs args)
        {
            if (GrabStopped != null) GrabStopped(this, args);
        }
        protected void RasizeGrabed(FrameGrabbedEventArgs args)
        {
            if (_intervalTime.Second == DateTime.Now.Second)
            {
                _recvCount++;
            }
            else
            {
                FPS = _recvCount;
                _intervalTime = DateTime.Now;
                _recvCount = 0;
            }

            Buffers.Enqueue(new MatEx(args.Image, args.ExposureTime));

            if (Grabed != null) Grabed(this, args);
        }

        public virtual void Dispose()
        {

        }
        public virtual void ExecuteSoftwareTrigger()
        {
        }
        public virtual void ExecuteHardwareTrigger()
        {
        }
        public virtual bool SetOutLine(int line, bool value)
        {
            return false;
        }

        public virtual void CheckConnectStatus(object state)
        {
            if (System.Threading.Monitor.TryEnter(this))
            {
                if (IsStarted == true && IsConnected == false)
                {
                    ReConnect();
                }
                System.Threading.Monitor.Exit(this);
            }
        }
        public virtual void ReConnect()
        {

            if (IsConnected == false)
            {
                if (Connect(GrabberParam))
                {
                    StartGrab();
                }
            }

        }
    }
}
