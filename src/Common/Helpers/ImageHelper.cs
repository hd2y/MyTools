using System.IO;
using System.Linq;
using System.Text;

namespace MyTools.Helpers
{
    public enum ImageFormat
    {
        Bmp,
        Jpeg,
        Gif,
        Tiff,
        Png,
        Unknown
    }
    
    public static class ImageHelper
    {
        /// <summary>
        /// 获取图片格式
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static ImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM"); // BMP
            var gif = Encoding.ASCII.GetBytes("GIF"); // GIF
            var png = new byte[] {137, 80, 78, 71}; // PNG
            var tiff = new byte[] {73, 73, 42}; // TIFF
            var tiff2 = new byte[] {77, 77, 42}; // TIFF
            var jpeg = new byte[] {255, 216, 255, 224}; // jpeg
            var jpeg2 = new byte[] {255, 216, 255, 225}; // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
            {
                return ImageFormat.Bmp;
            }

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
            {
                return ImageFormat.Gif;
            }

            if (png.SequenceEqual(bytes.Take(png.Length)))
            {
                return ImageFormat.Png;
            }

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
            {
                return ImageFormat.Tiff;
            }

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
            {
                return ImageFormat.Tiff;
            }

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
            {
                return ImageFormat.Jpeg;
            }

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
            {
                return ImageFormat.Jpeg;
            }

            return ImageFormat.Unknown;
        }

        /// <summary>
        /// 获取图片格式
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static ImageFormat GetImageFormat(Stream stream)
        {
            stream.Position = 0;
            var buffer = new byte[5];
            stream.Read(buffer, 0, buffer.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return GetImageFormat(buffer);
        }
    }
}