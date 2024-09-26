using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WindowsCaptureVideo.Common
{
	// Token: 0x02000006 RID: 6
	public static class BitmapTool
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002E94 File Offset: 0x00001094
		public static Bitmap ToBitmap(this FrameworkElement element, int width = 0, int height = 0, int x = 0, int y = 0)
		{
			bool flag = width == 0;
			bool flag2 = flag;
			if (flag2)
			{
				width = (int)element.ActualWidth;
			}
			bool flag3 = height == 0;
			bool flag4 = flag3;
			if (flag4)
			{
				height = (int)element.ActualHeight;
			}
			RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(width, height, (double)x, (double)y, PixelFormats.Default);
			renderTargetBitmap.Render(element);
			return renderTargetBitmap.BitmapSourceToBitmap();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002EF4 File Offset: 0x000010F4
		private static Bitmap BitmapSourceToBitmap(this BitmapSource source)
		{
			return source.BitmapSourceToBitmap(source.PixelWidth, source.PixelHeight);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002F18 File Offset: 0x00001118
		private static Bitmap BitmapSourceToBitmap(this BitmapSource source, int width, int height)
		{
			Bitmap bitmap = null;
			try
			{
				System.Drawing.Imaging.PixelFormat format = System.Drawing.Imaging.PixelFormat.Format24bppRgb;
				string text = source.Format.ToString();
				string a = text;
				bool flag = !(a == "Rgb24") && !(a == "Bgr24");
				if (flag)
				{
					bool flag2 = !(a == "Bgra32");
					if (flag2)
					{
						bool flag3 = !(a == "Bgr32");
						if (flag3)
						{
							bool flag4 = a == "Pbgra32";
							if (flag4)
							{
								format = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
							}
						}
						else
						{
							format = System.Drawing.Imaging.PixelFormat.Format32bppRgb;
						}
					}
					else
					{
						format = System.Drawing.Imaging.PixelFormat.Format32bppPArgb;
					}
				}
				else
				{
					format = System.Drawing.Imaging.PixelFormat.Format24bppRgb;
				}
				bitmap = new Bitmap(width, height, format);
				BitmapData bitmapData = bitmap.LockBits(new Rectangle(System.Drawing.Point.Empty, bitmap.Size), ImageLockMode.WriteOnly, format);
				source.CopyPixels(Int32Rect.Empty, bitmapData.Scan0, bitmapData.Height * bitmapData.Stride, bitmapData.Stride);
				bitmap.UnlockBits(bitmapData);
			}
			catch
			{
				bool flag5 = bitmap != null;
				bool flag6 = flag5;
				if (flag6)
				{
					bitmap.Dispose();
					bitmap = null;
				}
			}
			return bitmap;
		}
	}
}
