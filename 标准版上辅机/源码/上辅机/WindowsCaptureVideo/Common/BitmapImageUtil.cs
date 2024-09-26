using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace WindowsCaptureVideo.Common
{
    public class BitmapImageUtil
    {
        private static MemoryStream globalMemoryStream = new MemoryStream();

        public static BitmapSource GetBitmapSource(Bitmap bmp)
        {
            BitmapFrame result = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bmp.Save(memoryStream, ImageFormat.Png);
                result = BitmapFrame.Create(memoryStream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
            return result;
        }

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        public static BitmapSource GetBitMapSourceFromSnapScreen()
        {
            Bitmap screenSnapshot = GetScreenSnapshot();
            IntPtr hbitmap = screenSnapshot.GetHbitmap();
            BitmapSource result = Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(hbitmap);
            return result;
        }

        public static BitmapSource GetBitMapSourceFromBitmap(Bitmap bitmap)
        {
            IntPtr hbitmap = bitmap.GetHbitmap();
            BitmapSource result = Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(hbitmap);
            return result;
        }

        public static BitmapImage GetBitmapImageFromSnapScreen()
        {
            Bitmap screenSnapshot = GetScreenSnapshot();
            screenSnapshot.Save(globalMemoryStream, ImageFormat.Bmp);
            byte[] buffer = globalMemoryStream.GetBuffer();
            screenSnapshot.Dispose();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(buffer);
            bitmapImage.EndInit();
            return bitmapImage;
        }

        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Bmp);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }

        public static Bitmap GetScreenSnapshot()
        {
            Rectangle rectangle = new Rectangle(0, 0, 1920, 1080);
            Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(rectangle.X, rectangle.Y, 0, 0, rectangle.Size, CopyPixelOperation.SourceCopy);
            }
            return bitmap;
        }
    }
}