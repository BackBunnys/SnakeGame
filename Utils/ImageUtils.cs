using System.IO;

namespace SnakeGame.Utils
{
    static class ImageUtils
    {
        public static byte[] BitmapToByteArray(System.Drawing.Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
