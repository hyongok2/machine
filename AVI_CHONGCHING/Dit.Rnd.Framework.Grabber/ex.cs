using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dit.Rnd.Framework.Grabber
{
    public static class GrabberExtensions
    {
        public static Bitmap CreateBitmapDeepCopy(this Bitmap it)
        {
            Bitmap result;
            using (MemoryStream stream = new MemoryStream())
            {
                it.Save(stream, ImageFormat.Bmp);
                stream.Seek(0, SeekOrigin.Begin);
                result = new Bitmap(stream);
            }
            return result;
        }
    }
}
