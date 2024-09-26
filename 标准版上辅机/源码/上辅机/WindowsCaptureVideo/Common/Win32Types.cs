using System;
using System.Runtime.InteropServices;

namespace WindowsCaptureVideo.Common
{
	// Token: 0x0200000F RID: 15
	public sealed class Win32Types
	{
		// Token: 0x0200001A RID: 26
		public struct Point
		{
			// Token: 0x0600004E RID: 78 RVA: 0x000032E5 File Offset: 0x000014E5
			public Point(int x, int y)
			{
				this.x = x;
				this.y = y;
			}

			// Token: 0x04000168 RID: 360
			public int x;

			// Token: 0x04000169 RID: 361
			public int y;
		}

		// Token: 0x0200001B RID: 27
		public struct Rect
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600004F RID: 79 RVA: 0x000032F8 File Offset: 0x000014F8
			public int Width
			{
				get
				{
					int num = this.Right - this.Left;
					bool flag = num % 2 == 1;
					bool flag2 = flag;
					if (flag2)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000050 RID: 80 RVA: 0x0000332C File Offset: 0x0000152C
			public int Height
			{
				get
				{
					int num = this.Bottom - this.Top;
					bool flag = num % 2 == 1;
					bool flag2 = flag;
					if (flag2)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x0400016A RID: 362
			public int Left;

			// Token: 0x0400016B RID: 363
			public int Top;

			// Token: 0x0400016C RID: 364
			public int Right;

			// Token: 0x0400016D RID: 365
			public int Bottom;
		}

		// Token: 0x0200001C RID: 28
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct BitmapFileHeader
		{
			// Token: 0x0400016E RID: 366
			public ushort bfType;

			// Token: 0x0400016F RID: 367
			public uint bfSize;

			// Token: 0x04000170 RID: 368
			public ushort bfReserved1;

			// Token: 0x04000171 RID: 369
			public ushort bfReserved2;

			// Token: 0x04000172 RID: 370
			public uint bfOffBits;
		}

		// Token: 0x0200001D RID: 29
		public struct BitmapInfoHeader
		{
			// Token: 0x06000051 RID: 81 RVA: 0x0000335F File Offset: 0x0000155F
			public void Init()
			{
				this.biSize = (uint)Marshal.SizeOf(this);
			}

			// Token: 0x04000173 RID: 371
			public uint biSize;

			// Token: 0x04000174 RID: 372
			public int biWidth;

			// Token: 0x04000175 RID: 373
			public int biHeight;

			// Token: 0x04000176 RID: 374
			public ushort biPlanes;

			// Token: 0x04000177 RID: 375
			public ushort biBitCount;

			// Token: 0x04000178 RID: 376
			public uint biCompression;

			// Token: 0x04000179 RID: 377
			public uint biSizeImage;

			// Token: 0x0400017A RID: 378
			public int biXPelsPerMeter;

			// Token: 0x0400017B RID: 379
			public int biYPelsPerMeter;

			// Token: 0x0400017C RID: 380
			public uint biClrUsed;

			// Token: 0x0400017D RID: 381
			public uint biClrImportant;
		}

		// Token: 0x0200001E RID: 30
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct RgbQuad
		{
			// Token: 0x0400017E RID: 382
			public byte rgbBlue;

			// Token: 0x0400017F RID: 383
			public byte rgbGreen;

			// Token: 0x04000180 RID: 384
			public byte rgbRed;

			// Token: 0x04000181 RID: 385
			public byte rgbReserved;
		}

		// Token: 0x0200001F RID: 31
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct BitmapInfo
		{
			// Token: 0x04000182 RID: 386
			public Win32Types.BitmapInfoHeader bmiHeader;

			// Token: 0x04000183 RID: 387
			public Win32Types.RgbQuad bmiColors;
		}
	}
}
