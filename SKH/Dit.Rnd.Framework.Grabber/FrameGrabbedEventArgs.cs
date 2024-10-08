using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dit.Rnd.Framework.Grabber
{
    public class FrameGrabbedEventArgs : EventArgs, IDisposable
    {
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public int Channel { get; set; } = 0;
        public int ExposureTime { get; set; } = 0;
        //public IntPtr PtrBuffer { get; set; }        
        public Mat Image { get; set; }
        public void Dispose()
        {
        }
    }
}
