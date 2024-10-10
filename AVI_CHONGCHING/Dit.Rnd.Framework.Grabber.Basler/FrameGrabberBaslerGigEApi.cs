using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using PylonC.NET;

namespace Dit.Rnd.Framework.Grabber.Basler
{
    public class FrameGrabberBaslerGigEApi : FrameGrabber
    {
        /* Simple data class for holding image data. */
        public class Image
        {
            public Image(int newWidth, int newHeight, Byte[] newBuffer, bool color)
            {
                Width = newWidth;
                Height = newHeight;
                Buffer = newBuffer;
                Color = color;
            }

            public readonly int Width; /* The width of the image. */
            public readonly int Height; /* The height of the image. */
            public readonly Byte[] Buffer; /* The raw image data. */
            public readonly bool Color; /* If false the buffer contains a Mono8 image. Otherwise, RGBA8packed is provided. */
        }

        /* The class GrabResult is used internally to queue grab results. */
        protected class GrabResult
        {
            public Image ImageData; /* Holds the taken image. */
            public PYLON_STREAMBUFFER_HANDLE Handle; /* Holds the handle of the image registered at the stream grabber. It is used to queue the buffer associated with itself for the next grab. */
        }

        /* The members of ImageProvider: */
        protected bool m_converterOutputFormatIsColor = false;/* The output format of the format converter. */
        protected PYLON_IMAGE_FORMAT_CONVERTER_HANDLE m_hConverter; /* The format converter is used mainly for coverting color images. It is not used for Mono8 or RGBA8packed images. */
        protected Dictionary<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>> m_convertedBuffers; /* Holds handles and buffers used for converted images. It is not used for Mono8 or RGBA8packed images.*/
        protected PYLON_DEVICE_HANDLE m_hDevice;           /* Handle for the pylon device. */
        protected PYLON_STREAMGRABBER_HANDLE m_hGrabber;   /* Handle for the pylon stream grabber. */
        protected PYLON_DEVICECALLBACK_HANDLE m_hRemovalCallback;    /* Required for deregistering the callback. */
        protected PYLON_WAITOBJECT_HANDLE m_hWait;         /* Handle used for waiting for a grab to be finished. */
        protected Object m_lockObject;                     /* Lock object used for thread synchronization. */
        protected Dictionary<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>> m_buffers; /* Holds handles and buffers used for grabbing. */
        protected List<GrabResult> m_grabbedBuffers; /* List of grab results already grabbed. */
        protected uint m_numberOfBuffersUsed = 1;          /* Number of m_buffers used in grab. */

        public string Name { get; set; }
        private Stopwatch _stopWatch = new Stopwatch();
        private Thread _worker;
        private bool _grab = false;
        private bool _grabOnce = false;
        public override double ExposureTime
        {
            get
            {
                long exposureTime = -1;
                PylonC.NET.NODEMAP_HANDLE hNodemap = PylonC.NET.Pylon.DeviceGetNodeMap(m_hDevice);
                PylonC.NET.NODE_HANDLE m_hNode = PylonC.NET.GenApi.NodeMapGetNode(hNodemap, "ExposureTimeRaw");
                if (PylonC.NET.GenApi.NodeIsReadable(m_hNode))
                    exposureTime = PylonC.NET.GenApi.IntegerGetValue(m_hNode);

                return exposureTime;
            }
            set
            {


                PylonC.NET.NODEMAP_HANDLE hNodemap = PylonC.NET.Pylon.DeviceGetNodeMap(m_hDevice);
                PylonC.NET.NODE_HANDLE m_hNode = PylonC.NET.GenApi.NodeMapGetNode(hNodemap, "ExposureTimeRaw");
                if (PylonC.NET.GenApi.NodeIsWritable(m_hNode)) PylonC.NET.GenApi.IntegerSetValue(m_hNode, (long)value);

                ExposureTimeEx = value;

            }
        }
        public override double AnalogGain
        {
            get
            {
                long gain = -1;
                PylonC.NET.NODEMAP_HANDLE hNodemap = PylonC.NET.Pylon.DeviceGetNodeMap(m_hDevice);
                PylonC.NET.NODE_HANDLE m_hNode = PylonC.NET.GenApi.NodeMapGetNode(hNodemap, "Gain");
                if (PylonC.NET.GenApi.NodeIsReadable(m_hNode))
                    gain = PylonC.NET.GenApi.IntegerGetValue(m_hNode);

                return gain;
            }
            set
            {


                PylonC.NET.NODEMAP_HANDLE hNodemap = PylonC.NET.Pylon.DeviceGetNodeMap(m_hDevice);
                PylonC.NET.NODE_HANDLE m_hNode = PylonC.NET.GenApi.NodeMapGetNode(hNodemap, "GainRaw");
                if (PylonC.NET.GenApi.NodeIsWritable(m_hNode)) PylonC.NET.GenApi.IntegerSetValue(m_hNode, 500);

            }
        }


        public FrameGrabberBaslerGigEApi()
        {

            /* Create objects used for buffer handling. */
            m_lockObject = new Object();
            m_buffers = new Dictionary<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>>();
            m_grabbedBuffers = new List<GrabResult>();

            /* Create handles. */
            m_hGrabber = new PYLON_STREAMGRABBER_HANDLE();
            m_hDevice = new PYLON_DEVICE_HANDLE();
            m_hRemovalCallback = new PYLON_DEVICECALLBACK_HANDLE();
            m_hConverter = new PYLON_IMAGE_FORMAT_CONVERTER_HANDLE();


        }

        //메소드 - 시작 종료
        public override bool Connect(FrameGrabberParam param)
        {
            GrabberParam = param;

            PylonC.NET.Pylon.Initialize();
            Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "300000" /*ms*/);

            uint numDevices = PylonC.NET.Pylon.EnumerateDevices();
            bool isExits = false;
            //if (_isException)
            //    PylonC.NET.Pylon.DeviceClose(m_hDevice);

            try
            {
                for (uint i = 0; i < numDevices; ++i)
                {
                    /* Get the device info handle of the device. */
                    PylonC.NET.PYLON_DEVICE_INFO_HANDLE hDi = PylonC.NET.Pylon.GetDeviceInfoHandle(i);
                    string cameraID = PylonC.NET.Pylon.DeviceInfoGetPropertyValueByName(hDi, PylonC.NET.Pylon.cPylonDeviceInfoUserDefinedNameKey);
                    string cameraSerialNumberKey = PylonC.NET.Pylon.DeviceInfoGetPropertyValueByName(hDi, PylonC.NET.Pylon.cPylonDeviceInfoSerialNumberKey);
                    uint index = i;

                    if (cameraID.ToUpper() == param.CameraName.ToUpper() || cameraSerialNumberKey.ToUpper() == param.CameraName.ToUpper())
                    {
                        if (!m_hDevice.IsValid)
                        {
                            m_hDevice = PylonC.NET.Pylon.CreateDeviceByIndex(index);
                        }
                        if (PylonC.NET.Pylon.DeviceIsOpen(m_hDevice) == false)
                        {
                            PylonC.NET.Pylon.DeviceOpen(m_hDevice, PylonC.NET.Pylon.cPylonAccessModeControl | PylonC.NET.Pylon.cPylonAccessModeStream);

                            /* ... Check first to see if the GigE camera packet size parameter is supported and if it is writable. */
                            if (PylonC.NET.Pylon.DeviceFeatureIsWritable(m_hDevice, "GevSCPSPacketSize"))
                            {
                                /* ... The device supports the packet size feature. Set a value. */
                                PylonC.NET.Pylon.DeviceSetIntegerFeature(m_hDevice, "GevSCPSPacketSize", 1500);
                            }

                            /* The sample does not work in chunk mode. It must be disabled. */
                            if (PylonC.NET.Pylon.DeviceFeatureIsWritable(m_hDevice, "ChunkModeActive"))
                            {
                                /* Disable the chunk mode. */
                                PylonC.NET.Pylon.DeviceSetBooleanFeature(m_hDevice, "ChunkModeActive", false);
                            }

                            /* Disable acquisition start trigger if available. */
                            if (PylonC.NET.Pylon.DeviceFeatureIsAvailable(m_hDevice, "EnumEntry_TriggerSelector_AcquisitionStart"))
                            {
                                PylonC.NET.Pylon.DeviceFeatureFromString(m_hDevice, "TriggerSelector", "AcquisitionStart");
                                PylonC.NET.Pylon.DeviceFeatureFromString(m_hDevice, "TriggerMode", "Off");
                            }

                            /* Disable frame burst start trigger if available */
                            if (PylonC.NET.Pylon.DeviceFeatureIsAvailable(m_hDevice, "EnumEntry_TriggerSelector_FrameBurstStart"))
                            {
                                PylonC.NET.Pylon.DeviceFeatureFromString(m_hDevice, "TriggerSelector", "FrameBurstStart");
                                PylonC.NET.Pylon.DeviceFeatureFromString(m_hDevice, "TriggerMode", "Off");
                            }

                            /* Disable frame start trigger if available. */
                            if (PylonC.NET.Pylon.DeviceFeatureIsAvailable(m_hDevice, "EnumEntry_TriggerSelector_FrameStart"))
                            {
                                PylonC.NET.Pylon.DeviceFeatureFromString(m_hDevice, "TriggerSelector", "FrameStart");
                                PylonC.NET.Pylon.DeviceFeatureFromString(m_hDevice, "TriggerMode", "Off");
                            }



                            //PylonC.NET.NODEMAP_HANDLE hNodemap = PylonC.NET.Pylon.DeviceGetNodeMap(m_hDevice);
                            //PylonC.NET.NODE_HANDLE m_hNode = PylonC.NET.GenApi.NodeMapGetNode(hNodemap, "ExposureTimeRaw");

                            //string sss = PylonC.NET.GenApi.NodeGetDisplayName(m_hNode) + ":";
                            ////m_hNode = GenApi.NodeGetAlias(m_hNode);
                            //if (PylonC.NET.GenApi.NodeIsWritable(m_hNode))
                            //{
                            //    PylonC.NET.GenApi.IntegerSetValue(m_hNode, 500);
                            //}

                            m_hGrabber = PylonC.NET.Pylon.DeviceGetStreamGrabber(m_hDevice, 0);
                            PylonC.NET.Pylon.StreamGrabberOpen(m_hGrabber);

                            m_hWait = PylonC.NET.Pylon.StreamGrabberGetWaitObject(m_hGrabber);

                            isExits = true;
                        }
                    }
                }

                _isException = false;
                base.Connect(param);

            }
            catch(Exception ex)
            {
                _isException = true;
            }
            return isExits;
        }

        /* Prepares everything for grabbing. */
        protected void SetupGrab()
        {
            /* Clear the grab result queue. This is not done when cleaning up to still be able to provide the
             images, e.g. in single frame mode.*/
            lock (m_lockObject) /* Lock the grab result queue to avoid that two threads modify the same data. */
            {
                m_grabbedBuffers.Clear();
            }

            /* Set the acquisition mode */
            if (_grabOnce)
            {
                /* We will use the single frame mode, to take one image. */
                PylonC.NET.Pylon.DeviceFeatureFromString(m_hDevice, "AcquisitionMode", "SingleFrame");
            }
            else
            {
                /* We will use the Continuous frame mode, i.e., the camera delivers
                images continuously. */
                PylonC.NET.Pylon.DeviceFeatureFromString(m_hDevice, "AcquisitionMode", "Continuous");
            }

            /* Clear the grab buffers to assure proper operation (because they may
             still be filled if the last grab has thrown an exception). */
            foreach (KeyValuePair<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>> pair in m_buffers)
            {
                pair.Value.Dispose();
            }
            m_buffers.Clear();

            /* Determine the required size of the grab buffer. */
            uint payloadSize = checked((uint)PylonC.NET.Pylon.DeviceGetIntegerFeature(m_hDevice, "PayloadSize"));

            /* We must tell the stream grabber the number and size of the m_buffers
                we are using. */
            /* .. We will not use more than NUM_m_buffers for grabbing. */
            PylonC.NET.Pylon.StreamGrabberSetMaxNumBuffer(m_hGrabber, m_numberOfBuffersUsed);

            /* .. We will not use m_buffers bigger than payloadSize bytes. */
            PylonC.NET.Pylon.StreamGrabberSetMaxBufferSize(m_hGrabber, payloadSize);

            /*  Allocate the resources required for grabbing. After this, critical parameters
                that impact the payload size must not be changed until FinishGrab() is called. */
            PylonC.NET.Pylon.StreamGrabberPrepareGrab(m_hGrabber);

            /* Before using the m_buffers for grabbing, they must be registered at
               the stream grabber. For each buffer registered, a buffer handle
               is returned. After registering, these handles are used instead of the
               buffer objects pointers. The buffer objects are held in a dictionary,
               that provides access to the buffer using a handle as key.
             */
            for (uint i = 0; i < m_numberOfBuffersUsed; ++i)
            {
                PylonBuffer<Byte> buffer = new PylonBuffer<byte>(payloadSize, true);
                PYLON_STREAMBUFFER_HANDLE handle = PylonC.NET.Pylon.StreamGrabberRegisterBuffer(m_hGrabber, ref buffer);
                m_buffers.Add(handle, buffer);
            }

            /* Feed the m_buffers into the stream grabber's input queue. For each buffer, the API
               allows passing in an integer as additional context information. This integer
               will be returned unchanged when the grab is finished. In our example, we use the index of the
               buffer as context information. */
            foreach (KeyValuePair<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<Byte>> pair in m_buffers)
            {
                PylonC.NET.Pylon.StreamGrabberQueueBuffer(m_hGrabber, pair.Key, 0);
            }

            /* The stream grabber is now prepared. As soon the camera starts acquiring images,
               the image data will be grabbed into the provided m_buffers.  */

            /* Set the handle of the image converter invalid to assure proper operation (because it may
             still be valid if the last grab has thrown an exception). */
            m_hConverter.SetInvalid();

            /* Start the image acquisition engine. */
            PylonC.NET.Pylon.StreamGrabberStartStreamingIfMandatory(m_hGrabber);

            /* Let the camera acquire images. */
            PylonC.NET.Pylon.DeviceExecuteCommandFeature(m_hDevice, "AcquisitionStart");
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



        public override bool Disconnection()
        {
            try
            {
                if (PylonC.NET.Pylon.DeviceIsOpen(m_hDevice))
                {
                    _grab = false;
                    PylonC.NET.Pylon.DeviceClose(m_hDevice);
                }
                if (m_hDevice.IsValid)
                {
                    PylonC.NET.Pylon.DestroyDevice(m_hDevice);
                    m_hDevice = new PylonC.NET.PYLON_DEVICE_HANDLE();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
        public override bool StartGrab()
        {
            try
            {
                _grab = true;
                _grabOnce = false;
                m_numberOfBuffersUsed = 5;

                _worker = new Thread(new ThreadStart(Working));
                _worker.Start();
            }
            catch
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
                _grab = false;
                _grabOnce = false;
                _worker.Join();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public override bool Snap(int count = 1)
        {

            try
            {
                _grab = true;
                _grabOnce = true;

                m_numberOfBuffersUsed = 5;

                _worker = new Thread(new ThreadStart(Working));
                _worker.Start();

            }
            catch
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
                try
                {
                    return PylonC.NET.Pylon.DeviceIsOpen(m_hDevice) && _isException == false;
                }
                catch
                {
                    return false;
                }
            }
        }
        public override bool IsGrabbing
        {
            get
            {
                return _grab;
            }
        }
        public override void ExecuteSoftwareTrigger()
        {
            //_camera.ExecuteSoftwareTrigger();
        }

        public override bool SetOutLine(int line, bool value)
        {
            //if (_camera == null) return false;
            //if (_camera.IsConnected == false) return false;
            //if (_camera.IsOpen == false) return false;

            //// Select Line 2 (output line)
            //_camera.Parameters[PLCamera.LineSelector].SetValue(PLCamera.LineSelector.Line2);

            //// Set the source signal to User Output 1
            //_camera.Parameters[PLCamera.LineSource].SetValue(PLCamera.LineSource.UserOutput1);

            //// Select the User Output 1 signal
            //_camera.Parameters[PLCamera.UserOutputSelector].SetValue(PLCamera.UserOutputSelector.UserOutput1);

            // Set the User Output Value for the User Output 1 signal to true.
            // Because User Output 1 is set as the source signal for Line 2,
            // the status of Line 2 is set to high.
            //_camera.Parameters[PLCamera.UserOutputValue].SetValue(value);
            return true;
        }
        public void Working()
        {
            SetupGrab();
            while (_grab)
            {
                try
                {
                    /* Wait for the next buffer to be filled. Wait up to 15000 ms. */
                    if (!PylonC.NET.Pylon.WaitObjectWait(m_hWait, 1000))
                    {
                        lock (m_lockObject)
                        {
                            if (m_grabbedBuffers.Count != m_numberOfBuffersUsed)
                            {
                                /* A timeout occurred. This can happen if an external trigger is used or
                                   if the programmed exposure time is longer than the grab timeout. */
                                throw new Exception("A grab timeout occurred.");
                            }
                            continue;
                        }
                    }



                    PylonGrabResult_t grabResult; /* Stores the result of a grab operation. */
                    /* Since the wait operation was successful, the result of at least one grab
                       operation is available. Retrieve it. */
                    if (!PylonC.NET.Pylon.StreamGrabberRetrieveResult(m_hGrabber, out grabResult))
                    {
                        /* Oops. No grab result available? We should never have reached this point.
                           Since the wait operation above returned without a timeout, a grab result
                           should be available. */
                        throw new Exception("Failed to retrieve a grab result.");
                    }

                    /* Check to see if the image was grabbed successfully. */
                    if (grabResult.Status == EPylonGrabStatus.Grabbed)
                    {
                        /* Add result to the ready list. */
                        EnqueueTakenImage(grabResult);

                        /* Notify that an image has been added to the output queue. The receiver of the event can use GetCurrentImage() to acquire and process the image
                         and ReleaseImage() to remove the image from the queue and return it to the stream grabber.*/
                        OnImageReadyEvent();

                        /* Exit here for single frame mode. */
                        if (_grabOnce)
                        {
                            _grab = false;
                            break;
                        }
                    }
                    else if (grabResult.Status == EPylonGrabStatus.Failed)
                    {
                        /*
                            Grabbing an image can fail if the used network hardware, i.e. network adapter,
                            switch or Ethernet cable, experiences performance problems.
                            Increase the Inter-Packet Delay to reduce the required bandwidth.
                            It is recommended to enable Jumbo Frames on the network adapter and switch.
                            Adjust the Packet Size on the camera to the highest supported frame size.
                            If this did not resolve the problem, check if the recommended hardware is used.
                            Aggressive power saving settings for the CPU can also cause the image grab to fail.
                        */
                        throw new Exception(string.Format("A grab failure occurred. See the method ImageProvider::Grab for more information. The error code is {0:X08}.", grabResult.ErrorCode));
                    }
                }
                catch
                {
                    _isException = true;
                }
            }
        }
        public Image GetLatestImage()
        {
            lock (m_lockObject) /* Lock the grab result queue to avoid that two threads modify the same data. */
            {
                /* Release all images but the latest. */
                while (m_grabbedBuffers.Count > 1)
                {
                    ReleaseImage();
                }
                if (m_grabbedBuffers.Count > 0) /* If images available. */
                {
                    return m_grabbedBuffers[0].ImageData;
                }
            }
            return null; /* No image available. */
        }
        protected void OnImageReadyEvent()
        {
            Image img = GetLatestImage();
            Mat mat;
            //if (m_converterOutputFormatIsColor == false)

            mat = new Mat((int)img.Height, (int)img.Width, MatType.CV_8UC1);
            Marshal.Copy(img.Buffer, 0, mat.Data, (int)(mat.Step(0) * mat.Height));


            //포인터 전달은 고민 필요함. 
            FrameGrabbedEventArgs args = new FrameGrabbedEventArgs()
            {
                Image = mat.Clone(),
                Width = (int)img.Width,
                Height = (int)img.Height,
                Channel = m_converterOutputFormatIsColor ? 1 : 4,
                ExposureTime = 10
            };
            RasizeGrabed(args);
            ReleaseImage();
        }

        public Image GetCurrentImage()
        {
            lock (m_lockObject) /* Lock the grab result queue to avoid that two threads modify the same data. */
            {
                if (m_grabbedBuffers.Count > 0) /* If images available. */
                {
                    return m_grabbedBuffers[0].ImageData;
                }
            }
            return null; /* No image available. */
        }
        public bool ReleaseImage()
        {
            lock (m_lockObject) /* Lock the grab result queue to avoid that two threads modify the same data. */
            {
                if (m_grabbedBuffers.Count > 0) /* If images are available and grabbing is in progress.*/
                {
                    if (_grab)
                    {
                        /* Requeue the buffer. */
                        PylonC.NET.Pylon.StreamGrabberQueueBuffer(m_hGrabber, m_grabbedBuffers[0].Handle, 0);
                    }
                    /* Remove it from the grab result queue. */
                    m_grabbedBuffers.RemoveAt(0);
                    return true;
                }
            }
            return false;
        }

        protected void OnGrabbingStoppedEvent()
        {

        }

        protected void EnqueueTakenImage(PylonGrabResult_t grabResult)
        {
            PylonBuffer<Byte> buffer;  /* Reference to the buffer attached to the grab result. */

            /* Get the buffer from the dictionary. */
            if (!m_buffers.TryGetValue(grabResult.hBuffer, out buffer))
            {
                /* Oops. No buffer available? We should never have reached this point. Since all buffers are
                   in the dictionary. */
                throw new Exception("Failed to find the buffer associated with the handle returned in grab result.");
            }

            /* Create a new grab result to enqueue to the grabbed buffers list. */
            GrabResult newGrabResultInternal = new GrabResult();
            newGrabResultInternal.Handle = grabResult.hBuffer; /* Add the handle to requeue the buffer in the stream grabber queue. */

            /* If already in output format add the image data. */
            if (grabResult.PixelType == EPylonPixelType.PixelType_Mono8 || grabResult.PixelType == EPylonPixelType.PixelType_RGBA8packed)
            {
                newGrabResultInternal.ImageData = new Image(grabResult.SizeX, grabResult.SizeY, buffer.Array, grabResult.PixelType == EPylonPixelType.PixelType_RGBA8packed);
            }
            else /* Conversion is required. */
            {
                /* Create a new format converter if needed. */
                if (!m_hConverter.IsValid)
                {
                    m_convertedBuffers = new Dictionary<PYLON_STREAMBUFFER_HANDLE, PylonBuffer<byte>>(); /* Create a new dictionary for the converted buffers. */
                    m_hConverter = PylonC.NET.Pylon.ImageFormatConverterCreate(); /* Create the converter. */
                    m_converterOutputFormatIsColor = !PylonC.NET.Pylon.IsMono(grabResult.PixelType) || PylonC.NET.Pylon.IsBayer(grabResult.PixelType);
                }
                /* Reference to the buffer attached to the grab result handle. */
                PylonBuffer<Byte> convertedBuffer = null;
                /* Look up if a buffer is already attached to the handle. */
                bool bufferListed = m_convertedBuffers.TryGetValue(grabResult.hBuffer, out convertedBuffer);
                /* Perform the conversion. If the buffer is null a new one is automatically created. */
                PylonC.NET.Pylon.ImageFormatConverterSetOutputPixelFormat(m_hConverter, m_converterOutputFormatIsColor ? EPylonPixelType.PixelType_BGRA8packed : EPylonPixelType.PixelType_Mono8);
                PylonC.NET.Pylon.ImageFormatConverterConvert(m_hConverter, ref convertedBuffer, buffer, grabResult.PixelType, (uint)grabResult.SizeX, (uint)grabResult.SizeY, (uint)grabResult.PaddingX, EPylonImageOrientation.ImageOrientation_TopDown);
                if (!bufferListed) /* A new buffer has been created. Add it to the dictionary. */
                {
                    m_convertedBuffers.Add(grabResult.hBuffer, convertedBuffer);
                }
                /* Add the image data. */
                newGrabResultInternal.ImageData = new Image(grabResult.SizeX, grabResult.SizeY, convertedBuffer.Array, m_converterOutputFormatIsColor);
            }
            lock (m_lockObject) /* Lock the grab result queue to avoid that two threads modify the same data. */
            {
                m_grabbedBuffers.Add(newGrabResultInternal); /* Add the new grab result to the queue. */
            }
        }

    }
}
