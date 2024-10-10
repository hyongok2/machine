using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dit.Rnd.Framework.Grabber
{
    public class FrameGrabberParam
    {
        public string ConfigFileName { get; set; }
        public int GrabberIndex { get; set; }
        public int ChannelIndex { get; set; }

        public string CameraName { get; set; }
        public RotateFlipType Rotate { get; set; } = RotateFlipType.RotateNoneFlipNone;
        public string SimulAviFileName { get; set; }
        public int SimulFps { get; set; }

        //설정이 필요한 경우. 
        public int Width { get; set; }
        public int Height { get; set; }

    }
}
