using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dit.Rnd.Framework.Grabber
{

    public class MatEx : Mat
    {
        public MatEx(Mat m, int ext)
            : base(m, m.BoundingRect())
        {
            ExposureTime = ext;
        }
        public int ExposureTime { get; set; } = 0;
    }
    public class FrameInfo
    {
        public Mat Img { get; set; }
        public DateTime GrapTime { get; set; }
    }
    public enum EmDequeueMode { First, Last }

    public class FrameBuffer
    {
        public Queue<MatEx> _lstQueue = null;
        public int MaxFrameCount { get; set; }
        public FrameBuffer()
        {
            _lstQueue = new Queue<MatEx>();
            MaxFrameCount = 2;
        }
        public int Enqueue(MatEx image)
        {
            lock (_lstQueue)
            {
                _lstQueue.Enqueue(image);
                if (_lstQueue.Count > MaxFrameCount) _lstQueue.Dequeue();
            }
            return _lstQueue.Count;
        }
        public MatEx Dequeue(EmDequeueMode deqMode)
        {
            lock (_lstQueue)
            {
                if (_lstQueue.Count <= 0) return null;

                MatEx image;
                if (deqMode == EmDequeueMode.Last)
                {
                    while (_lstQueue.Count > 1)
                    {
                        image = _lstQueue.Dequeue();
                        image.Dispose();
                    }
                }
                image = _lstQueue.Dequeue();
                return image;
            }
        }
        public int Count
        {
            get
            {
                lock (_lstQueue)
                {
                    return _lstQueue.Count;
                }
            }
        }

        public void Clear()
        {
            lock (_lstQueue)
            {
                while (true)
                {
                    if (_lstQueue.Count == 0) break;

                    Mat mm = _lstQueue.Dequeue();
                    mm.Dispose();
                }
            }
        }
    }
}
