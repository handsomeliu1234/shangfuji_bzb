using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WindowsCaptureVideo.Common
{
	// Token: 0x02000005 RID: 5
	public static class BitmapScaleHelper
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002C18 File Offset: 0x00000E18
		public static Bitmap ScaleToSize(this Bitmap bitmap, int width, int height)
		{
			bool flag = bitmap.Width == width && bitmap.Height == height;
			bool flag2 = flag;
			Bitmap result;
			if (flag2)
			{
				result = bitmap;
			}
			else
			{
				Bitmap bitmap2 = new Bitmap(width, height);
				using (Graphics graphics = Graphics.FromImage(bitmap2))
				{
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.DrawImage(bitmap, 0, 0, width, height);
				}
				result = bitmap2;
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002C9C File Offset: 0x00000E9C
		public static Bitmap ScaleToSize(this Bitmap bitmap, Size size)
		{
			return bitmap.ScaleToSize(size.Width, size.Height);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public static Bitmap ScaleToSize(this Bitmap bitmap, float ratio)
		{
			return bitmap.ScaleToSize((int)((float)bitmap.Width * ratio), (int)((float)bitmap.Height * ratio));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public static Bitmap ScaleProportional(this Bitmap bitmap, int width, int height)
		{
			bool flag = width.Equals(0);
			bool flag2 = flag;
			float num;
			float num2;
			if (flag2)
			{
				num = (float)height / (float)bitmap.Size.Height * (float)bitmap.Width;
				num2 = (float)height;
			}
			else
			{
				bool flag3 = height.Equals(0);
				bool flag4 = flag3;
				if (flag4)
				{
					num = (float)width;
					num2 = (float)width / (float)bitmap.Size.Width * (float)bitmap.Height;
				}
				else
				{
					bool flag5 = (float)width / (float)bitmap.Size.Width * (float)bitmap.Size.Height <= (float)height;
					bool flag6 = flag5;
					if (flag6)
					{
						num = (float)width;
						num2 = (float)width / (float)bitmap.Size.Width * (float)bitmap.Height;
					}
					else
					{
						num = (float)height / (float)bitmap.Size.Height * (float)bitmap.Width;
						num2 = (float)height;
					}
				}
			}
			return bitmap.ScaleToSize((int)num, (int)num2);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public static Bitmap ScaleToSize(this Bitmap bitmap, Color backgroundColor, int width, int height)
		{
			Bitmap bitmap2 = new Bitmap(width, height);
			using (Graphics graphics = Graphics.FromImage(bitmap2))
			{
				graphics.Clear(backgroundColor);
				Bitmap bitmap3 = bitmap.ScaleProportional(width, height);
				Point point = new Point((int)((width - bitmap3.Width) / 2m), (int)((height - bitmap3.Height) / 2m));
				graphics.DrawImage(bitmap3, point);
			}
			return bitmap2;
		}
	}
}
